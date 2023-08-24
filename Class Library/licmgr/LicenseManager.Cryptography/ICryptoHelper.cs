using System.Text;

namespace LicenseManager.Cryptography
{
	public interface ICryptoHelper
	{
		CryptoAlgorithm CryptoAlgorithm
		{
			get;
			set;
		}

		Encoding Encoding
		{
			get;
			set;
		}

		string AESSaltValue
		{
			get;
			set;
		}

		string AESPassPhrase
		{
			get;
			set;
		}

		string AESHashAlgorithm
		{
			get;
			set;
		}

		string AESInitVector
		{
			get;
			set;
		}

		int AESKeySize
		{
			get;
			set;
		}

		int RC2RandomSeed
		{
			get;
			set;
		}

		int RC2KeySize
		{
			get;
			set;
		}

		int RC2IVSize
		{
			get;
			set;
		}
	}
}
