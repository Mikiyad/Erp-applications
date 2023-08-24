namespace Micromind.Securities.Cryptography
{
	public interface ICryptor
	{
		ICryptoHelper CryptoHelper
		{
			get;
		}

		string Encrypt(string plainText);

		string Decrypt(string cypherText);
	}
}
