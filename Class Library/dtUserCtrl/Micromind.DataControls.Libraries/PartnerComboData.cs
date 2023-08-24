namespace Micromind.DataControls.Libraries
{
	public class PartnerComboData
	{
		private bool isEmployee;

		private bool isPartner = true;

		private bool isInactive;

		private string name = "";

		private string firstName = "";

		private string lastName = "";

		private string companyName = "";

		private int id;

		private bool isCustomer;

		private bool isVendor;

		private int defaultPaymentMethod = -1;

		public string CompanyName
		{
			get
			{
				return companyName;
			}
			set
			{
				companyName = value;
			}
		}

		public string FirstName
		{
			get
			{
				return firstName;
			}
			set
			{
				firstName = value;
			}
		}

		public string LastName
		{
			get
			{
				return lastName;
			}
			set
			{
				lastName = value;
			}
		}

		public bool IsInactive
		{
			get
			{
				return isInactive;
			}
			set
			{
				isInactive = value;
			}
		}

		public int DefaultPaymentMethod
		{
			get
			{
				return defaultPaymentMethod;
			}
			set
			{
				defaultPaymentMethod = value;
			}
		}

		public string Name
		{
			get
			{
				return name;
			}
			set
			{
				name = value;
			}
		}

		public bool IsCustomer
		{
			get
			{
				return isCustomer;
			}
			set
			{
				isCustomer = value;
			}
		}

		public bool IsEmployee
		{
			get
			{
				return isEmployee;
			}
			set
			{
				isEmployee = value;
			}
		}

		public bool IsPartner
		{
			get
			{
				return isPartner;
			}
			set
			{
				isPartner = value;
			}
		}

		public bool IsVendor
		{
			get
			{
				return isVendor;
			}
			set
			{
				isVendor = value;
			}
		}

		public int ID
		{
			get
			{
				return id;
			}
			set
			{
				id = value;
			}
		}

		public PartnerComboData()
		{
		}

		public PartnerComboData(string name, int id)
		{
			this.name = name;
			this.id = id;
		}

		public override string ToString()
		{
			return name;
		}
	}
}
