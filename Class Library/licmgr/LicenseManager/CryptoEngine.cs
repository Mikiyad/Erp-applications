using System;
using System.IO;
using System.Security.Cryptography;

using System.Text;

namespace LicenseManager
{
	
	public class CryptoEngine
	{
		private string password = "";

		private byte[] key;

		private byte[] iv;

		private Random rand;

		private int randSeed => int.Parse("1000");

		private int keySize => int.Parse("16");

		private int ivSize => int.Parse("8");

		public CryptoEngine()
		{
			Init();
			key = new byte[keySize];
			iv = new byte[ivSize];
			rand = new Random(randSeed);
			for (byte b = 0; b < keySize; b = (byte)(b + 1))
			{
				key[b] = (byte)rand.Next(255);
			}
			for (byte b2 = 0; b2 < ivSize; b2 = (byte)(b2 + 1))
			{
				iv[b2] = (byte)rand.Next(255);
			}
		}

		public CryptoEngine(byte[] key, byte[] iv)
		{
			Init();
			this.key = key;
			this.iv = iv;
		}

		public void GetEncryptedData(string data, ref string eData)
		{
			try
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
			catch
			{
				eData = "";
			}
		}

		public byte[] GetEncryptedData(string data)
		{
			try
			{
				UnicodeEncoding unicodeEncoding = new UnicodeEncoding();
				RC2CryptoServiceProvider rC2CryptoServiceProvider = new RC2CryptoServiceProvider();
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
			catch
			{
				return new byte[0];
			}
		}

		public byte[] GetEncryptedData(char[] data)
		{
			try
			{
				UnicodeEncoding unicodeEncoding = new UnicodeEncoding();
				RC2CryptoServiceProvider rC2CryptoServiceProvider = new RC2CryptoServiceProvider();
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
			catch
			{
				return new byte[0];
			}
		}

		public byte[] GetEncryptedData(byte[] data)
		{
			try
			{
				UnicodeEncoding unicodeEncoding = new UnicodeEncoding();
				RC2CryptoServiceProvider rC2CryptoServiceProvider = new RC2CryptoServiceProvider();
				ICryptoTransform transform = rC2CryptoServiceProvider.CreateEncryptor(key, iv);
				MemoryStream memoryStream = new MemoryStream();
				CryptoStream cryptoStream = new CryptoStream(memoryStream, transform, CryptoStreamMode.Write);
				byte[] bytes = unicodeEncoding.GetBytes(unicodeEncoding.GetString(data));
				cryptoStream.Write(bytes, 0, bytes.Length);
				cryptoStream.FlushFinalBlock();
				unicodeEncoding = null;
				rC2CryptoServiceProvider = null;
				return memoryStream.ToArray();
			}
			catch
			{
				return new byte[0];
			}
		}

		public byte[] GetDecryptedData(byte[] data, string p)
		{
			try
			{
				if (p != password)
				{
					return new UnicodeEncoding().GetBytes("0");
				}
				if (data == null || data.Length == 0)
				{
					return new UnicodeEncoding().GetBytes("0");
				}
				RC2CryptoServiceProvider rC2CryptoServiceProvider = new RC2CryptoServiceProvider();
				ICryptoTransform transform = rC2CryptoServiceProvider.CreateDecryptor(key, iv);
				MemoryStream stream = new MemoryStream(data);
				CryptoStream cryptoStream = new CryptoStream(stream, transform, CryptoStreamMode.Read);
				byte[] array = new byte[data.Length];
				cryptoStream.Read(array, 0, array.Length);
				rC2CryptoServiceProvider = null;
				return array;
			}
			catch
			{
				return data;
			}
		}

		public byte[] GetDecryptedData(string data, string p)
		{
			try
			{
				if (p != password)
				{
					return new UnicodeEncoding().GetBytes("0");
				}
				if (data == null || data.Length == 0)
				{
					return new UnicodeEncoding().GetBytes("0");
				}
				RC2CryptoServiceProvider rC2CryptoServiceProvider = new RC2CryptoServiceProvider();
				ICryptoTransform transform = rC2CryptoServiceProvider.CreateDecryptor(key, iv);
				UnicodeEncoding unicodeEncoding = new UnicodeEncoding();
				byte[] bytes = unicodeEncoding.GetBytes(data);
				unicodeEncoding = null;
				MemoryStream stream = new MemoryStream(bytes);
				CryptoStream cryptoStream = new CryptoStream(stream, transform, CryptoStreamMode.Read);
				byte[] array = new byte[bytes.Length];
				cryptoStream.Read(array, 0, array.Length);
				rC2CryptoServiceProvider = null;
				return array;
			}
			catch
			{
				return new UnicodeEncoding().GetBytes(data);
			}
		}

		public void GetDecryptedData(string data, ref string dData, string p)
		{
			try
			{
				if (!(p != password) && data != null && data.Length != 0)
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
			catch
			{
				dData = data;
			}
		}

		private void Init()
		{
			password = "passqbm7446610";
		}
	}
}
