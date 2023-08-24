using Micromind.Common.Data;

namespace Micromind.DataControls.Libraries
{
	public class AccountComboItem
	{
		private string name;

		private string description;

		private string typeName;

		private string accountNumber;

		private string id;

		private int typeID;

		private CompanyAccountTypes accountType;

		public string Indent = "";

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

		public string AccountNumber
		{
			get
			{
				return accountNumber;
			}
			set
			{
				accountNumber = value;
			}
		}

		public CompanyAccountTypes AccountType
		{
			get
			{
				return accountType;
			}
			set
			{
				accountType = value;
			}
		}

		public string TypeName
		{
			get
			{
				return typeName;
			}
			set
			{
				typeName = value;
			}
		}

		public string ID
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

		public int TypeID
		{
			get
			{
				return typeID;
			}
			set
			{
				typeID = value;
			}
		}

		public string Description
		{
			get
			{
				return description;
			}
			set
			{
				description = value;
			}
		}

		public override string ToString()
		{
			return name;
		}
	}
}
