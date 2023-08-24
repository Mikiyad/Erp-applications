using System;
using System.IO;
using System.Security.Cryptography;

using System.Text;

namespace Micromind.Securities.Cryptography
{
	
	public sealed class AESCryptor : ICryptor
	{
		private ICryptoHelper cryptoHelper = new CryptoHelper();

		private string passPhrase = "P3fd3@2ddw";

		private string saltValue = "3s4f@dwf3de4sq26";

		private string hashAlgorithm = "SHA1";

		private string initVector = "@1B2c3D4e5F6g7H8";

		private int keySize = 256;

		private int passwordIterations;

		public ICryptoHelper CryptoHelper => cryptoHelper;

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
			Init();
		}

		public AESCryptor(ICryptoHelper cryptoHelper)
		{
			Init();
			this.cryptoHelper = cryptoHelper;
		}

		private void Init()
		{
			passwordIterations = int.Parse("2");
		}

		internal string Encrypt(string plainText, string passPhrase, string saltValue, string hashAlgorithm, int passwordIterations, string initVector, int keySize)
		{
			return Encrypt(plainText, passPhrase, saltValue, hashAlgorithm, passwordIterations, initVector, keySize, Encoding.ASCII);
		}

		internal string Encrypt(string plainText, string passPhrase, string saltValue, string hashAlgorithm, int passwordIterations, string initVector, int keySize, Encoding encoding)
		{
			if (plainText == null)
			{
				plainText = string.Empty;
			}
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
				if (hashAlgorithm != "SHA1" && hashAlgorithm != "MD5")
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
				return "";
			}
		}

		internal string Decrypt(string cipherText, string passPhrase, string saltValue, string hashAlgorithm, int passwordIterations, string initVector, int keySize)
		{
			return Decrypt(cipherText, passPhrase, saltValue, hashAlgorithm, passwordIterations, initVector, keySize, Encoding.ASCII);
		}

		internal string Decrypt(string cipherText, string passPhrase, string saltValue, string hashAlgorithm, int passwordIterations, string initVector, int keySize, Encoding encoding)
		{
			if (cipherText == null || cipherText.Trim() == string.Empty)
			{
				return string.Empty;
			}
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
				if (keySize != 256 && keySize != 192 && keySize != 128)
				{
					keySize = this.keySize;
				}
				if (hashAlgorithm != "SHA1" && hashAlgorithm != "MD5")
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
				return "";
			}
		}

		string ICryptor.Encrypt(string plainText)
		{
			if (cryptoHelper == null)
			{
				throw new NullReferenceException("Crypto helper cannot be null.");
			}
			return Encrypt(plainText, cryptoHelper.AESPassPhrase, cryptoHelper.AESSaltValue, cryptoHelper.AESHashAlgorithm, PasswordIterations, cryptoHelper.AESInitVector, cryptoHelper.AESKeySize, cryptoHelper.Encoding);
		}

		string ICryptor.Decrypt(string cypherText)
		{
			if (cryptoHelper == null)
			{
				throw new NullReferenceException("Crypto helper cannot be null.");
			}
			return Decrypt(cypherText, cryptoHelper.AESPassPhrase, cryptoHelper.AESSaltValue, cryptoHelper.AESHashAlgorithm, PasswordIterations, cryptoHelper.AESInitVector, cryptoHelper.AESKeySize, cryptoHelper.Encoding);
		}
	}
}
