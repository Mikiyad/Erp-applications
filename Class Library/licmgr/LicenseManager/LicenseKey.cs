namespace LicenseManager
{
	public class LicenseKey
	{
		private string key;

		private string userData;

		private int productID;

		private int editionID;

		private int versionMajor;

		private int versionMinor;

		private int users;

		private int expiryDays;

		private bool isTrial;

		private bool isActivated;

		private bool isTrialExpired;

		public string Key
		{
			get
			{
				return key;
			}
			set
			{
				key = value;
			}
		}

		public string UserData
		{
			get
			{
				return userData;
			}
			set
			{
				userData = value;
			}
		}

		public int ProductID
		{
			get
			{
				return productID;
			}
			set
			{
				productID = value;
			}
		}

		public int EditionID
		{
			get
			{
				return editionID;
			}
			set
			{
				editionID = value;
			}
		}

		public int VersionMajor
		{
			get
			{
				return versionMajor;
			}
			set
			{
				versionMajor = value;
			}
		}

		public int VersionMinor
		{
			get
			{
				return versionMinor;
			}
			set
			{
				versionMinor = value;
			}
		}

		public int Users
		{
			get
			{
				return users;
			}
			set
			{
				users = value;
			}
		}

		public int ExpiryDays
		{
			get
			{
				return expiryDays;
			}
			set
			{
				expiryDays = value;
			}
		}

		public bool IsTrial
		{
			get
			{
				return isTrial;
			}
			set
			{
				isTrial = value;
			}
		}

		public bool IsActivated
		{
			get
			{
				return isActivated;
			}
			set
			{
				isActivated = value;
			}
		}

		public bool IsTrialExpired
		{
			get
			{
				return isTrialExpired;
			}
			set
			{
				isTrialExpired = value;
			}
		}

		public LicenseKey()
		{
		}

		public LicenseKey(string key, int productID, int editionID, int versionMajor, int versionMinor, int users, int expiryDays, bool isTrial, string userData)
		{
			this.key = key;
			this.productID = productID;
			this.editionID = editionID;
			this.versionMajor = versionMajor;
			this.versionMinor = versionMinor;
			this.users = users;
			this.expiryDays = expiryDays;
			this.userData = userData;
			this.isTrial = isTrial;
		}
	}
}
