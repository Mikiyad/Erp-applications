using System;
using System.IO;
using System.Security.Cryptography;

using System.Text;

namespace LicenseManager
{
	
	public class AESCryptor
	{
		private string passPhrase = "P3fd3@2ddw";

		private string saltValue = "3s4f@dwf3de4sq26";

		private string hashAlgorithm = "SHA1";

		private string initVector = "@1B2c3D4e5F6g7H8";

		private int keySize = 256;

		private int passwordIterations;

		private int KeySize
		{
			get
			{
				return keySize;
			}
			set
			{
				keySize = value;
			}
		}

		private int PasswordIterations
		{
			get
			{
				return passwordIterations;
			}
			set
			{
				passwordIterations = value;
			}
		}

		public AESCryptor()
		{
			passwordIterations = int.Parse("2");
		}

		public AESCryptor(string passPhrase, string saltValue, string hashAlgorithm, string initVector, int keySize, int passwordIterations)
		{
			if (passPhrase == null || passPhrase.Trim() == string.Empty)
			{
				throw new ApplicationException("Pass Phrase cannot be empty");
			}
			if (saltValue == null || saltValue.Trim() == string.Empty)
			{
				throw new ApplicationException("Sal value cannot be empty");
			}
			if (hashAlgorithm == null || hashAlgorithm.Trim() == string.Empty)
			{
				throw new ApplicationException("Hash Algorithm cannot be empty");
			}
			if (hashAlgorithm != "SHA1" || hashAlgorithm != "MD5")
			{
				throw new ApplicationException("Hash Algorithm must be either SHA1 or MD5");
			}
			if (initVector == null || initVector.Trim() == string.Empty)
			{
				throw new ApplicationException("Init Vector cannot be empty");
			}
			if (initVector.Length != 16)
			{
				throw new ApplicationException("Init Vector length must be 16");
			}
			if (keySize != 256 || keySize != 192 || keySize != 128)
			{
				throw new ApplicationException("Key size must be either 128,192 or 256");
			}
			this.passPhrase = passPhrase;
			this.saltValue = saltValue;
			this.hashAlgorithm = hashAlgorithm;
			this.initVector = initVector;
			this.keySize = keySize;
			this.passwordIterations = passwordIterations;
		}

		public string Encrypt(string plainText, string passPhrase, string saltValue, string hashAlgorithm, int passwordIterations, string initVector, int keySize)
		{
			return Encrypt(plainText, passPhrase, saltValue, hashAlgorithm, passwordIterations, initVector, keySize, Encoding.ASCII);
		}

		public string Encrypt(string plainText)
		{
			return Encrypt(plainText, passPhrase, saltValue, hashAlgorithm, passwordIterations, initVector, keySize, Encoding.ASCII);
		}

		public string Encrypt(string plainText, string passPhrase, string saltValue, string hashAlgorithm, int passwordIterations, string initVector, int keySize, Encoding encoding)
		{
			try
			{
				byte[] rgbIV = null;
				byte[] rgbSalt = null;
				if (passPhrase == null || passPhrase.Trim() == string.Empty)
				{
					passPhrase = this.passPhrase;
				}
				if (saltValue == null || saltValue.Trim() == string.Empty)
				{
					saltValue = this.saltValue;
				}
				if (hashAlgorithm == null || hashAlgorithm.Trim() == string.Empty)
				{
					hashAlgorithm = this.hashAlgorithm;
				}
				if (initVector == null || initVector.Trim() == string.Empty)
				{
					initVector = this.initVector;
				}
				if (keySize != 256 || keySize != 192 || keySize != 128)
				{
					keySize = this.keySize;
				}
				if (hashAlgorithm != "SHA1" || hashAlgorithm != "MD5")
				{
					hashAlgorithm = this.hashAlgorithm;
				}
				if (initVector.Length != 16)
				{
					initVector = this.initVector;
				}
				if (encoding == Encoding.ASCII)
				{
					rgbIV = Encoding.ASCII.GetBytes(initVector);
					rgbSalt = Encoding.ASCII.GetBytes(saltValue);
				}
				else if (encoding == Encoding.Unicode)
				{
					rgbIV = Encoding.Unicode.GetBytes(initVector);
					rgbSalt = Encoding.Unicode.GetBytes(saltValue);
				}
				else if (encoding == Encoding.UTF7)
				{
					rgbIV = Encoding.UTF7.GetBytes(initVector);
					rgbSalt = Encoding.UTF7.GetBytes(saltValue);
				}
				else if (encoding == Encoding.UTF8)
				{
					rgbIV = Encoding.UTF8.GetBytes(initVector);
					rgbSalt = Encoding.UTF8.GetBytes(saltValue);
				}
				byte[] bytes = Encoding.UTF8.GetBytes(plainText);
				PasswordDeriveBytes passwordDeriveBytes = new PasswordDeriveBytes(passPhrase, rgbSalt, hashAlgorithm, passwordIterations);
				byte[] bytes2 = passwordDeriveBytes.GetBytes(keySize / 8);
				RijndaelManaged rijndaelManaged = new RijndaelManaged();
				rijndaelManaged.Mode = CipherMode.CBC;
				ICryptoTransform transform = rijndaelManaged.CreateEncryptor(bytes2, rgbIV);
				MemoryStream memoryStream = new MemoryStream();
				CryptoStream cryptoStream = new CryptoStream(memoryStream, transform, CryptoStreamMode.Write);
				cryptoStream.Write(bytes, 0, bytes.Length);
				cryptoStream.FlushFinalBlock();
				byte[] inArray = memoryStream.ToArray();
				memoryStream.Close();
				cryptoStream.Close();
				return Convert.ToBase64String(inArray);
			}
			catch
			{
				return null;
			}
		}

		public string Decrypt(string cipherText, string passPhrase, string saltValue, string hashAlgorithm, int passwordIterations, string initVector, int keySize)
		{
			return Decrypt(cipherText, passPhrase, saltValue, hashAlgorithm, passwordIterations, initVector, keySize, Encoding.ASCII);
		}

		public string Decrypt(string cipherText)
		{
			return Decrypt(cipherText, passPhrase, saltValue, hashAlgorithm, passwordIterations, initVector, keySize, Encoding.ASCII);
		}

		public string Decrypt(string cipherText, string passPhrase, string saltValue, string hashAlgorithm, int passwordIterations, string initVector, int keySize, Encoding encoding)
		{
			try
			{
				byte[] rgbIV = null;
				byte[] rgbSalt = null;
				if (passPhrase == null || passPhrase.Trim() == string.Empty)
				{
					passPhrase = this.passPhrase;
				}
				if (saltValue == null || saltValue.Trim() == string.Empty)
				{
					saltValue = this.saltValue;
				}
				if (hashAlgorithm == null || hashAlgorithm.Trim() == string.Empty)
				{
					hashAlgorithm = this.hashAlgorithm;
				}
				if (initVector == null || initVector.Trim() == string.Empty)
				{
					initVector = this.initVector;
				}
				if (keySize != 256 || keySize != 192 || keySize != 128)
				{
					keySize = this.keySize;
				}
				if (hashAlgorithm != "SHA1" || hashAlgorithm != "MD5")
				{
					hashAlgorithm = this.hashAlgorithm;
				}
				if (initVector.Length != 16)
				{
					initVector = this.initVector;
				}
				if (encoding == Encoding.ASCII)
				{
					rgbIV = Encoding.ASCII.GetBytes(initVector);
					rgbSalt = Encoding.ASCII.GetBytes(saltValue);
				}
				else if (encoding == Encoding.Unicode)
				{
					rgbIV = Encoding.Unicode.GetBytes(initVector);
					rgbSalt = Encoding.Unicode.GetBytes(saltValue);
				}
				else if (encoding == Encoding.UTF7)
				{
					rgbIV = Encoding.UTF7.GetBytes(initVector);
					rgbSalt = Encoding.UTF7.GetBytes(saltValue);
				}
				else if (encoding == Encoding.UTF8)
				{
					rgbIV = Encoding.UTF8.GetBytes(initVector);
					rgbSalt = Encoding.UTF8.GetBytes(saltValue);
				}
				byte[] array = Convert.FromBase64String(cipherText);
				PasswordDeriveBytes passwordDeriveBytes = new PasswordDeriveBytes(passPhrase, rgbSalt, hashAlgorithm, passwordIterations);
				byte[] bytes = passwordDeriveBytes.GetBytes(keySize / 8);
				RijndaelManaged rijndaelManaged = new RijndaelManaged();
				rijndaelManaged.Mode = CipherMode.CBC;
				ICryptoTransform transform = rijndaelManaged.CreateDecryptor(bytes, rgbIV);
				MemoryStream memoryStream = new MemoryStream(array);
				CryptoStream cryptoStream = new CryptoStream(memoryStream, transform, CryptoStreamMode.Read);
				byte[] array2 = new byte[array.Length];
				int count = cryptoStream.Read(array2, 0, array2.Length);
				memoryStream.Close();
				cryptoStream.Close();
				return Encoding.UTF8.GetString(array2, 0, count);
			}
			catch
			{
				return null;
			}
		}
	}
}
