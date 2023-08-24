using System.Text;

namespace LicenseManager.Cryptography
{
	public sealed class CryptoHelper : ICryptoHelper
	{
		private CryptoAlgorithm cryptoAlgorithm;

		private Encoding encoding = Encoding.ASCII;

		private string aesSaltValue;

		private string aesPassPhrase;

		private string aesHashAlgorithm;

		private string aesInitVector;

		private int aesKeySize;

		private int rc2RandomSeed;

		private int rc2KeySize;

		private int rc2IVSize;

		public CryptoAlgorithm CryptoAlgorithm
		{
			get
			{
				return cryptoAlgorithm;
			}
			set
			{
				cryptoAlgorithm = value;
			}
		}

		public Encoding Encoding
		{
			get
			{
				return encoding;
			}
			set
			{
				encoding = value;
			}
		}

		public string AESSaltValue
		{
			get
			{
				return aesSaltValue;
			}
			set
			{
				aesSaltValue = value;
			}
		}

		public string AESPassPhrase
		{
			get
			{
				return aesPassPhrase;
			}
			set
			{
				aesPassPhrase = value;
			}
		}

		public string AESHashAlgorithm
		{
			get
			{
				return aesHashAlgorithm;
			}
			set
			{
				aesHashAlgorithm = value;
			}
		}

		public string AESInitVector
		{
			get
			{
				return aesInitVector;
			}
			set
			{
				aesInitVector = value;
			}
		}

		public int AESKeySize
		{
			get
			{
				return aesKeySize;
			}
			set
			{
				aesKeySize = value;
			}
		}

		public int RC2RandomSeed
		{
			get
			{
				return rc2RandomSeed;
			}
			set
			{
				rc2RandomSeed = value;
			}
		}

		public int RC2KeySize
		{
			get
			{
				return rc2KeySize;
			}
			set
			{
				rc2KeySize = value;
			}
		}

		public int RC2IVSize
		{
			get
			{
				return rc2IVSize;
			}
			set
			{
				rc2IVSize = value;
			}
		}

		public CryptoHelper()
		{
			Init();
		}

		private void Init()
		{
			AESPassPhrase = (aesPassPhrase = "P3fd3@2ddw");
			AESSaltValue = (aesSaltValue = "3s4f@dwf3de4sq26");
			AESHashAlgorithm = (aesHashAlgorithm = "SHA1");
			AESInitVector = (aesInitVector = "@1B2c3D4e5F6g7H8");
			AESKeySize = 256;
			RC2RandomSeed = int.Parse("1000");
			RC2KeySize = int.Parse("16");
			RC2IVSize = int.Parse("8");
		}
	}
}
