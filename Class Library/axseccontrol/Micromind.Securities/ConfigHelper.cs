using Micromind.Securities.Cryptography;
using System;
using System.Text;

namespace Micromind.Securities
{
	public sealed class ConfigHelper : IDisposable
	{
		private string accessUser = "";

		private string accessPassword = "";

		private string dbUserID = "";

		private string dbPassword = "";

		private string dbAdminUser = "";

		private ICryptor cryptor;

		private ConnectionTypes connectionType;

		private string ID => "D38E3CAB-5C9B-4808-AEF7-8DCE167061B8";

		public ICryptor Cryptor
		{
			get
			{
				if (cryptor == null)
				{
					CryptoHelper cryptoHelper = new CryptoHelper();
					cryptor = new AESCryptor(cryptoHelper);
				}
				cryptor.CryptoHelper.AESSaltValue = "3@s4fdwd5de4wq&3w";
				cryptor.CryptoHelper.AESPassPhrase = "sD3Fr57fthoet4";
				cryptor.CryptoHelper.AESKeySize = int.Parse("256");
				cryptor.CryptoHelper.AESInitVector = "@1C2c3r5e5G6g7H8";
				cryptor.CryptoHelper.AESHashAlgorithm = "SHA1";
				cryptor.CryptoHelper.Encoding = Encoding.ASCII;
				return cryptor;
			}
			set
			{
				cryptor = value;
			}
		}

		public ConnectionTypes ConnectionType
		{
			get
			{
				return connectionType;
			}
			set
			{
				connectionType = value;
			}
		}

		public string AdminUserName
		{
			get
			{
				switch (connectionType)
				{
				default:
					return "sa";
				case ConnectionTypes.ExternalServer:
					return dbAdminUser;
				}
			}
			set
			{
				if (value == null || value == string.Empty)
				{
					dbAdminUser = "sa";
				}
				else
				{
					dbAdminUser = value;
				}
			}
		}

		public string AccessPassword
		{
			get
			{
				return accessPassword;
			}
			set
			{
				accessPassword = value;
			}
		}

		public string AccessUser
		{
			get
			{
				return accessUser;
			}
			set
			{
				accessUser = value;
			}
		}

		public string UserID
		{
			get
			{
				return dbUserID;
			}
			set
			{
				dbUserID = value;
			}
		}

		public string Password
		{
			get
			{
				return dbPassword;
			}
			set
			{
				dbPassword = value;
			}
		}

		public ConfigHelper(string str)
		{
			if (str != ID)
			{
				throw new ApplicationException("Wrong UserID/Password combination.");
			}
		}

		public bool IsAllwedDBSystemModification()
		{
			return connectionType == ConnectionTypes.LocalServer;
		}

		public bool IsCurrentUserAdministrator()
		{
			return dbUserID.ToLower() == AdminUserName.ToLower();
		}

		public void ResetPassword(string userID, string password)
		{
			if (dbUserID.ToLower() == AdminUserName.ToLower() && userID.ToLower() == AdminUserName.ToLower())
			{
				dbPassword = password;
			}
			else if (userID == accessUser)
			{
				accessPassword = password;
			}
		}

		public void Dispose()
		{
			accessUser = (accessPassword = (dbUserID = (dbPassword = "")));
		}
	}
}
