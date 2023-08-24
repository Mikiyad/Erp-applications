using Micromind.Utilities.Checksums;
using System;
using System.IO;

namespace Micromind.Utilities.BZip2
{
	public class BZip2InputStream : Stream
	{
		private int last;

		private int origPtr;

		private int blockSize100k;

		private bool blockRandomised;

		private int bsBuff;

		private int bsLive;

		private IChecksum mCrc = new StrangeCRC();

		private bool[] inUse = new bool[256];

		private int nInUse;

		private byte[] seqToUnseq = new byte[256];

		private byte[] unseqToSeq = new byte[256];

		private byte[] selector = new byte[BZip2Constants.MAX_SELECTORS];

		private byte[] selectorMtf = new byte[BZip2Constants.MAX_SELECTORS];

		private int[] tt;

		private byte[] ll8;

		private int[] unzftab = new int[256];

		private int[][] limit = new int[BZip2Constants.N_GROUPS][];

		private int[][] baseArray = new int[BZip2Constants.N_GROUPS][];

		private int[][] perm = new int[BZip2Constants.N_GROUPS][];

		private int[] minLens = new int[BZip2Constants.N_GROUPS];

		private Stream baseStream;

		private bool streamEnd;

		private int currentChar = -1;

		private const int START_BLOCK_STATE = 1;

		private const int RAND_PART_A_STATE = 2;

		private const int RAND_PART_B_STATE = 3;

		private const int RAND_PART_C_STATE = 4;

		private const int NO_RAND_PART_A_STATE = 5;

		private const int NO_RAND_PART_B_STATE = 6;

		private const int NO_RAND_PART_C_STATE = 7;

		private int currentState = 1;

		private int storedBlockCRC;

		private int storedCombinedCRC;

		private int computedBlockCRC;

		private uint computedCombinedCRC;

		private int count;

		private int chPrev;

		private int ch2;

		private int tPos;

		private int rNToGo;

		private int rTPos;

		private int i2;

		private int j2;

		private byte z;

		public override bool CanRead => baseStream.CanRead;

		public override bool CanSeek => baseStream.CanSeek;

		public override bool CanWrite => false;

		public override long Length => baseStream.Length;

		public override long Position
		{
			get
			{
				return baseStream.Position;
			}
			set
			{
				throw new NotSupportedException("BZip2InputStream position cannot be set");
			}
		}

		public override void Flush()
		{
			if (baseStream != null)
			{
				baseStream.Flush();
			}
		}

		public override long Seek(long offset, SeekOrigin origin)
		{
			throw new NotSupportedException("BZip2InputStream Seek not supported");
		}

		public override void SetLength(long val)
		{
			throw new NotSupportedException("BZip2InputStream SetLength not supported");
		}

		public override void Write(byte[] array, int offset, int count)
		{
			throw new NotSupportedException("BZip2InputStream Write not supported");
		}

		public override void WriteByte(byte val)
		{
			throw new NotSupportedException("BZip2InputStream WriteByte not supported");
		}

		public override int Read(byte[] b, int off, int len)
		{
			for (int i = 0; i < len; i++)
			{
				int num = ReadByte();
				if (num == -1)
				{
					return i;
				}
				b[off + i] = (byte)num;
			}
			return len;
		}

		public override void Close()
		{
			if (baseStream != null)
			{
				baseStream.Close();
			}
		}

		private void MakeMaps()
		{
			nInUse = 0;
			for (int i = 0; i < 256; i++)
			{
				if (inUse[i])
				{
					seqToUnseq[nInUse] = (byte)i;
					unseqToSeq[i] = (byte)nInUse;
					nInUse++;
				}
			}
		}

		public BZip2InputStream(Stream stream)
		{
			for (int i = 0; i < BZip2Constants.N_GROUPS; i++)
			{
				limit[i] = new int[BZip2Constants.MAX_ALPHA_SIZE];
				baseArray[i] = new int[BZip2Constants.MAX_ALPHA_SIZE];
				perm[i] = new int[BZip2Constants.MAX_ALPHA_SIZE];
			}
			ll8 = null;
			tt = null;
			BsSetStream(stream);
			Initialize();
			InitBlock();
			SetupBlock();
		}

		public override int ReadByte()
		{
			if (streamEnd)
			{
				return -1;
			}
			int result = currentChar;
			switch (currentState)
			{
			case 3:
				SetupRandPartB();
				break;
			case 4:
				SetupRandPartC();
				break;
			case 6:
				SetupNoRandPartB();
				break;
			case 7:
				SetupNoRandPartC();
				break;
			}
			return result;
		}

		private void Initialize()
		{
			char num = BsGetUChar();
			char c = BsGetUChar();
			char c2 = BsGetUChar();
			char c3 = BsGetUChar();
			if (num != 'B' || c != 'Z' || c2 != 'h' || c3 < '1' || c3 > '9')
			{
				streamEnd = true;
				return;
			}
			SetDecompressStructureSizes(c3 - 48);
			computedCombinedCRC = 0u;
		}

		private void InitBlock()
		{
			char c = BsGetUChar();
			char c2 = BsGetUChar();
			char c3 = BsGetUChar();
			char c4 = BsGetUChar();
			char c5 = BsGetUChar();
			char c6 = BsGetUChar();
			if (c == '\u0017' && c2 == 'r' && c3 == 'E' && c4 == '8' && c5 == 'P' && c6 == '\u0090')
			{
				Complete();
				return;
			}
			if (c != '1' || c2 != 'A' || c3 != 'Y' || c4 != '&' || c5 != 'S' || c6 != 'Y')
			{
				BadBlockHeader();
				streamEnd = true;
				return;
			}
			storedBlockCRC = BsGetInt32();
			blockRandomised = (BsR(1) == 1);
			GetAndMoveToFrontDecode();
			mCrc.Reset();
			currentState = 1;
		}

		private void EndBlock()
		{
			computedBlockCRC = (int)mCrc.Value;
			if (storedBlockCRC != computedBlockCRC)
			{
				CrcError();
			}
			computedCombinedCRC = (uint)(((int)(computedCombinedCRC << 1) & -1) | (int)(computedCombinedCRC >> 31));
			computedCombinedCRC ^= (uint)computedBlockCRC;
		}

		private void Complete()
		{
			storedCombinedCRC = BsGetInt32();
			if (storedCombinedCRC != (int)computedCombinedCRC)
			{
				CrcError();
			}
			streamEnd = true;
		}

		private static void CompressedStreamEOF()
		{
			throw new ApplicationException("BZip2 input stream end of compressed stream");
		}

		private static void BlockOverrun()
		{
			throw new ApplicationException("BZip2 input stream block overrun");
		}

		private static void BadBlockHeader()
		{
			throw new ApplicationException("BZip2 input stream bad block header");
		}

		private static void CrcError()
		{
			throw new ApplicationException("BZip2 input stream crc error");
		}

		private void BsSetStream(Stream f)
		{
			baseStream = f;
			bsLive = 0;
			bsBuff = 0;
		}

		private void FillBuffer()
		{
			int num = 0;
			try
			{
				num = baseStream.ReadByte();
			}
			catch (Exception)
			{
				CompressedStreamEOF();
			}
			if (num == -1)
			{
				CompressedStreamEOF();
			}
			bsBuff = ((bsBuff << 8) | (num & 0xFF));
			bsLive += 8;
		}

		private int BsR(int n)
		{
			while (bsLive < n)
			{
				FillBuffer();
			}
			int result = (bsBuff >> bsLive - n) & ((1 << n) - 1);
			bsLive -= n;
			return result;
		}

		private char BsGetUChar()
		{
			return (char)BsR(8);
		}

		private int BsGetint()
		{
			return (((((((0 << 8) | BsR(8)) << 8) | BsR(8)) << 8) | BsR(8)) << 8) | BsR(8);
		}

		private int BsGetIntVS(int numBits)
		{
			return BsR(numBits);
		}

		private int BsGetInt32()
		{
			return BsGetint();
		}

		private void HbCreateDecodeTables(int[] limit, int[] baseArray, int[] perm, char[] length, int minLen, int maxLen, int alphaSize)
		{
			int num = 0;
			for (int i = minLen; i <= maxLen; i++)
			{
				for (int j = 0; j < alphaSize; j++)
				{
					if (length[j] == i)
					{
						perm[num] = j;
						num++;
					}
				}
			}
			for (int k = 0; k < BZip2Constants.MAX_CODE_LEN; k++)
			{
				baseArray[k] = 0;
			}
			for (int l = 0; l < alphaSize; l++)
			{
				baseArray[length[l] + 1]++;
			}
			for (int m = 1; m < BZip2Constants.MAX_CODE_LEN; m++)
			{
				baseArray[m] += baseArray[m - 1];
			}
			for (int n = 0; n < BZip2Constants.MAX_CODE_LEN; n++)
			{
				limit[n] = 0;
			}
			int num2 = 0;
			for (int num3 = minLen; num3 <= maxLen; num3++)
			{
				num2 += baseArray[num3 + 1] - baseArray[num3];
				limit[num3] = num2 - 1;
				num2 <<= 1;
			}
			for (int num4 = minLen + 1; num4 <= maxLen; num4++)
			{
				baseArray[num4] = (limit[num4 - 1] + 1 << 1) - baseArray[num4];
			}
		}

		private void RecvDecodingTables()
		{
			char[][] array = new char[BZip2Constants.N_GROUPS][];
			for (int i = 0; i < BZip2Constants.N_GROUPS; i++)
			{
				array[i] = new char[BZip2Constants.MAX_ALPHA_SIZE];
			}
			bool[] array2 = new bool[16];
			for (int j = 0; j < 16; j++)
			{
				array2[j] = (BsR(1) == 1);
			}
			for (int k = 0; k < 16; k++)
			{
				if (array2[k])
				{
					for (int l = 0; l < 16; l++)
					{
						inUse[k * 16 + l] = (BsR(1) == 1);
					}
				}
				else
				{
					for (int m = 0; m < 16; m++)
					{
						inUse[k * 16 + m] = false;
					}
				}
			}
			MakeMaps();
			int num = nInUse + 2;
			int num2 = BsR(3);
			int num3 = BsR(15);
			for (int n = 0; n < num3; n++)
			{
				int num4 = 0;
				while (BsR(1) == 1)
				{
					num4++;
				}
				selectorMtf[n] = (byte)num4;
			}
			byte[] array3 = new byte[BZip2Constants.N_GROUPS];
			for (int num5 = 0; num5 < num2; num5++)
			{
				array3[num5] = (byte)num5;
			}
			for (int num6 = 0; num6 < num3; num6++)
			{
				int num7 = selectorMtf[num6];
				byte b = array3[num7];
				while (num7 > 0)
				{
					array3[num7] = array3[num7 - 1];
					num7--;
				}
				array3[0] = b;
				selector[num6] = b;
			}
			for (int num8 = 0; num8 < num2; num8++)
			{
				int num9 = BsR(5);
				for (int num10 = 0; num10 < num; num10++)
				{
					while (BsR(1) == 1)
					{
						num9 = ((BsR(1) != 0) ? (num9 - 1) : (num9 + 1));
					}
					array[num8][num10] = (char)num9;
				}
			}
			for (int num11 = 0; num11 < num2; num11++)
			{
				int num12 = 32;
				int num13 = 0;
				for (int num14 = 0; num14 < num; num14++)
				{
					num13 = Math.Max(num13, array[num11][num14]);
					num12 = Math.Min(num12, array[num11][num14]);
				}
				HbCreateDecodeTables(limit[num11], baseArray[num11], perm[num11], array[num11], num12, num13, num);
				minLens[num11] = num12;
			}
		}

		private void GetAndMoveToFrontDecode()
		{
			byte[] array = new byte[256];
			int num = BZip2Constants.baseBlockSize * blockSize100k;
			origPtr = BsGetIntVS(24);
			RecvDecodingTables();
			int num2 = nInUse + 1;
			int num3 = -1;
			int num4 = 0;
			for (int i = 0; i <= 255; i++)
			{
				unzftab[i] = 0;
			}
			for (int j = 0; j <= 255; j++)
			{
				array[j] = (byte)j;
			}
			last = -1;
			if (num4 == 0)
			{
				num3++;
				num4 = BZip2Constants.G_SIZE;
			}
			num4--;
			int num5 = selector[num3];
			int num6 = minLens[num5];
			int num7 = BsR(num6);
			while (num7 > limit[num5][num6])
			{
				if (num6 > 20)
				{
					throw new ApplicationException("Bzip data error");
				}
				num6++;
				while (bsLive < 1)
				{
					FillBuffer();
				}
				int num8 = (bsBuff >> bsLive - 1) & 1;
				bsLive--;
				num7 = ((num7 << 1) | num8);
			}
			if (num7 - baseArray[num5][num6] < 0 || num7 - baseArray[num5][num6] >= BZip2Constants.MAX_ALPHA_SIZE)
			{
				throw new ApplicationException("Bzip data error");
			}
			int num9 = perm[num5][num7 - baseArray[num5][num6]];
			while (num9 != num2)
			{
				if (num9 == BZip2Constants.RUNA || num9 == BZip2Constants.RUNB)
				{
					int num10 = -1;
					int num11 = 1;
					do
					{
						if (num9 == BZip2Constants.RUNA)
						{
							num10 += num11;
						}
						else if (num9 == BZip2Constants.RUNB)
						{
							num10 += 2 * num11;
						}
						num11 <<= 1;
						if (num4 == 0)
						{
							num3++;
							num4 = BZip2Constants.G_SIZE;
						}
						num4--;
						num5 = selector[num3];
						num6 = minLens[num5];
						num7 = BsR(num6);
						while (num7 > limit[num5][num6])
						{
							num6++;
							while (bsLive < 1)
							{
								FillBuffer();
							}
							int num8 = (bsBuff >> bsLive - 1) & 1;
							bsLive--;
							num7 = ((num7 << 1) | num8);
						}
						num9 = perm[num5][num7 - baseArray[num5][num6]];
					}
					while (num9 == BZip2Constants.RUNA || num9 == BZip2Constants.RUNB);
					num10++;
					byte b = seqToUnseq[array[0]];
					unzftab[b] += num10;
					while (num10 > 0)
					{
						last++;
						ll8[last] = b;
						num10--;
					}
					if (last >= num)
					{
						BlockOverrun();
					}
					continue;
				}
				last++;
				if (last >= num)
				{
					BlockOverrun();
				}
				byte b2 = array[num9 - 1];
				unzftab[seqToUnseq[b2]]++;
				ll8[last] = seqToUnseq[b2];
				for (int num12 = num9 - 1; num12 > 0; num12--)
				{
					array[num12] = array[num12 - 1];
				}
				array[0] = b2;
				if (num4 == 0)
				{
					num3++;
					num4 = BZip2Constants.G_SIZE;
				}
				num4--;
				num5 = selector[num3];
				num6 = minLens[num5];
				num7 = BsR(num6);
				while (num7 > limit[num5][num6])
				{
					num6++;
					while (bsLive < 1)
					{
						FillBuffer();
					}
					int num8 = (bsBuff >> bsLive - 1) & 1;
					bsLive--;
					num7 = ((num7 << 1) | num8);
				}
				num9 = perm[num5][num7 - baseArray[num5][num6]];
			}
		}

		private void SetupBlock()
		{
			int[] array = new int[257];
			array[0] = 0;
			Array.Copy(unzftab, 0, array, 1, 256);
			for (int i = 1; i <= 256; i++)
			{
				array[i] += array[i - 1];
			}
			for (int j = 0; j <= last; j++)
			{
				byte b = ll8[j];
				tt[array[b]] = j;
				array[b]++;
			}
			array = null;
			tPos = tt[origPtr];
			count = 0;
			i2 = 0;
			ch2 = 256;
			if (blockRandomised)
			{
				rNToGo = 0;
				rTPos = 0;
				SetupRandPartA();
			}
			else
			{
				SetupNoRandPartA();
			}
		}

		private void SetupRandPartA()
		{
			if (i2 <= last)
			{
				chPrev = ch2;
				ch2 = ll8[tPos];
				tPos = tt[tPos];
				if (rNToGo == 0)
				{
					rNToGo = BZip2Constants.rNums[rTPos];
					rTPos++;
					if (rTPos == 512)
					{
						rTPos = 0;
					}
				}
				rNToGo--;
				ch2 ^= ((rNToGo == 1) ? 1 : 0);
				i2++;
				currentChar = ch2;
				currentState = 3;
				mCrc.Update(ch2);
			}
			else
			{
				EndBlock();
				InitBlock();
				SetupBlock();
			}
		}

		private void SetupNoRandPartA()
		{
			if (i2 <= last)
			{
				chPrev = ch2;
				ch2 = ll8[tPos];
				tPos = tt[tPos];
				i2++;
				currentChar = ch2;
				currentState = 6;
				mCrc.Update(ch2);
			}
			else
			{
				EndBlock();
				InitBlock();
				SetupBlock();
			}
		}

		private void SetupRandPartB()
		{
			if (ch2 != chPrev)
			{
				currentState = 2;
				count = 1;
				SetupRandPartA();
				return;
			}
			count++;
			if (count >= 4)
			{
				z = ll8[tPos];
				tPos = tt[tPos];
				if (rNToGo == 0)
				{
					rNToGo = BZip2Constants.rNums[rTPos];
					rTPos++;
					if (rTPos == 512)
					{
						rTPos = 0;
					}
				}
				rNToGo--;
				z ^= (byte)((rNToGo == 1) ? 1 : 0);
				j2 = 0;
				currentState = 4;
				SetupRandPartC();
			}
			else
			{
				currentState = 2;
				SetupRandPartA();
			}
		}

		private void SetupRandPartC()
		{
			if (j2 < z)
			{
				currentChar = ch2;
				mCrc.Update(ch2);
				j2++;
			}
			else
			{
				currentState = 2;
				i2++;
				count = 0;
				SetupRandPartA();
			}
		}

		private void SetupNoRandPartB()
		{
			if (ch2 != chPrev)
			{
				currentState = 5;
				count = 1;
				SetupNoRandPartA();
				return;
			}
			count++;
			if (count >= 4)
			{
				z = ll8[tPos];
				tPos = tt[tPos];
				currentState = 7;
				j2 = 0;
				SetupNoRandPartC();
			}
			else
			{
				currentState = 5;
				SetupNoRandPartA();
			}
		}

		private void SetupNoRandPartC()
		{
			if (j2 < z)
			{
				currentChar = ch2;
				mCrc.Update(ch2);
				j2++;
			}
			else
			{
				currentState = 5;
				i2++;
				count = 0;
				SetupNoRandPartA();
			}
		}

		private void SetDecompressStructureSizes(int newSize100k)
		{
			if (0 > newSize100k || newSize100k > 9 || 0 > blockSize100k || blockSize100k > 9)
			{
				throw new ApplicationException("Invalid block size");
			}
			blockSize100k = newSize100k;
			if (newSize100k != 0)
			{
				int num = BZip2Constants.baseBlockSize * newSize100k;
				ll8 = new byte[num];
				tt = new int[num];
			}
		}
	}
}
