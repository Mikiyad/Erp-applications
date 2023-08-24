using System;
using System.IO;
using System.Security.Cryptography;

using System.Text;

namespace LicenseManager.Cryptography
{
	
	public sealed class RC2Cryptor : ICryptor
	{
		private byte[] key;

		private byte[] iv;

		private Random rand;

		private ICryptoHelper cryptoHelper = new CryptoHelper();

		private int randSeed;

		private int keySize;

		private int ivSize;

		public ICryptoHelper CryptoHelper => cryptoHelper;

		private int RandSeed => randSeed;

		private int KeySize => keySize;

		private int IVSize => ivSize;

		public RC2Cryptor()
		{
			Init();
			CreateKeys();
		}

		public RC2Cryptor(ICryptoHelper cryptoHelper)
		{
			Init();
			this.cryptoHelper = cryptoHelper;
		}

		internal RC2Cryptor(int keySize, int ivSize)
		{
			Init();
			CreateKeys();
		}

		internal RC2Cryptor(int keySize, int ivSize, int randSeed)
		{
			Init();
			this.keySize = keySize;
			this.ivSize = ivSize;
			this.randSeed = randSeed;
			CreateKeys();
		}

		private void CreateKeys()
		{
			key = new byte[KeySize];
			iv = new byte[IVSize];
			rand = new Random(RandSeed);
			for (byte b = 0; b < KeySize; b = (byte)(b + 1))
			{
				key[b] = (byte)rand.Next(255);
			}
			for (byte b2 = 0; b2 < IVSize; b2 = (byte)(b2 + 1))
			{
				iv[b2] = (byte)rand.Next(255);
			}
		}

		internal void GetEncryptedData(string data, ref string eData)
		{
			UnicodeEncoding unicodeEncoding = new UnicodeEncoding();
			RC2CryptoServiceProvider rC2CryptoServiceProvider = new RC2CryptoServiceProvider();
			_ = rC2CryptoServiceProvider.LegalKeySizes;
			ICryptoTransform transform = rC2CryptoServiceProvider.CreateEncryptor(key, iv);
			MemoryStream memoryStream = new MemoryStream();
			CryptoStream cryptoStream = new CryptoStream(memoryStream, transform, CryptoStreamMode.Write);
			byte[] bytes = unicodeEncoding.GetBytes(data);
			cryptoStream.Write(bytes, 0, bytes.Length);
			cryptoStream.FlushFinalBlock();
			eData = unicodeEncoding.GetString(memoryStream.ToArray());
			unicodeEncoding = null;
			rC2CryptoServiceProvider = null;
		}

		internal byte[] GetEncryptedData(string data)
		{
			UnicodeEncoding unicodeEncoding = new UnicodeEncoding();
			RC2CryptoServiceProvider rC2CryptoServiceProvider = new RC2CryptoServiceProvider();
			_ = rC2CryptoServiceProvider.LegalKeySizes;
			ICryptoTransform transform = rC2CryptoServiceProvider.CreateEncryptor(key, iv);
			MemoryStream memoryStream = new MemoryStream();
			CryptoStream cryptoStream = new CryptoStream(memoryStream, transform, CryptoStreamMode.Write);
			byte[] bytes = unicodeEncoding.GetBytes(data);
			cryptoStream.Write(bytes, 0, bytes.Length);
			cryptoStream.FlushFinalBlock();
			unicodeEncoding = null;
			rC2CryptoServiceProvider = null;
			return memoryStream.ToArray();
		}

		internal void GetDecryptedData(string data, ref string dData)
		{
			if (data != null && data.Length != 0)
			{
				RC2CryptoServiceProvider rC2CryptoServiceProvider = new RC2CryptoServiceProvider();
				ICryptoTransform transform = rC2CryptoServiceProvider.CreateDecryptor(key, iv);
				UnicodeEncoding unicodeEncoding = new UnicodeEncoding();
				byte[] bytes = unicodeEncoding.GetBytes(data);
				MemoryStream stream = new MemoryStream(bytes);
				CryptoStream cryptoStream = new CryptoStream(stream, transform, CryptoStreamMode.Read);
				byte[] array = new byte[bytes.Length];
				cryptoStream.Read(array, 0, array.Length);
				dData = unicodeEncoding.GetString(array);
				while (dData.IndexOf("\0") >= 0)
				{
					dData = dData.Remove(dData.IndexOf("\0"), 1);
				}
				unicodeEncoding = null;
				rC2CryptoServiceProvider = null;
			}
		}

		private void Init()
		{
			keySize = int.Parse("16");
			ivSize = int.Parse("8");
			randSeed = int.Parse("1000");
		}

		public string Encrypt(string plainText)
		{
			string eData = "";
			ivSize = cryptoHelper.RC2IVSize;
			keySize = cryptoHelper.RC2KeySize;
			randSeed = cryptoHelper.RC2RandomSeed;
			CreateKeys();
			GetEncryptedData(plainText, ref eData);
			return eData;
		}

		public string Decrypt(string cypherText)
		{
			string dData = "";
			ivSize = cryptoHelper.RC2IVSize;
			keySize = cryptoHelper.RC2KeySize;
			randSeed = cryptoHelper.RC2RandomSeed;
			CreateKeys();
			GetDecryptedData(cypherText, ref dData);
			return dData;
		}
	}
}
