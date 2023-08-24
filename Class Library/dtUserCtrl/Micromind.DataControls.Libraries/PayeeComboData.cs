using Micromind.Common.Data;
using System.Data;

namespace Micromind.DataControls.Libraries
{
	public class PayeeComboData
	{
		private bool isInactive;

		private string name = "";

		private string firstName = "";

		private string lastName = "";

		private int id;

		private PayeeTypes payeeType;

		private string email = "";

		private string email2 = "";

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

		public bool IsCustomer => payeeType == PayeeTypes.Customer;

		public bool IsOtherPartner => payeeType == PayeeTypes.OtherPartner;

		public bool IsBothCustomerAndVendor => payeeType == PayeeTypes.BothCustomerVendor;

		public bool IsEmployee => payeeType == PayeeTypes.Employee;

		public bool IsPartner => IsPartnerPayee(payeeType);

		public bool IsVendor => payeeType == PayeeTypes.Vendor;

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

		public string Email
		{
			get
			{
				return email;
			}
			set
			{
				email = value;
			}
		}

		public string Email2
		{
			get
			{
				return email2;
			}
			set
			{
				email2 = value;
			}
		}

		public PayeeTypes PayeeType
		{
			get
			{
				return payeeType;
			}
			set
			{
				payeeType = value;
			}
		}

		public static bool IsPartnerPayee(PayeeTypes payeeType)
		{
			if (payeeType != PayeeTypes.BothCustomerVendor && payeeType != PayeeTypes.Vendor && payeeType != PayeeTypes.Customer)
			{
				return payeeType == PayeeTypes.OtherPartner;
			}
			return true;
		}

		public override string ToString()
		{
			return name;
		}

		public static DataSet GetPayeeDataSet()
		{
			DataTable dataTable = new DataTable("Payees");
			dataTable.Columns.Add("ID", typeof(int));
			dataTable.Columns.Add("Name", typeof(string));
			dataTable.Columns.Add("FirstName", typeof(string));
			dataTable.Columns.Add("LastName", typeof(string));
			dataTable.Columns.Add("Email", typeof(string));
			dataTable.Columns.Add("Email2", typeof(string));
			dataTable.Columns.Add("IsInactive", typeof(bool));
			dataTable.Columns.Add("PayeeType", typeof(byte));
			return new DataSet
			{
				Tables = 
				{
					dataTable
				}
			};
		}

		public static string GetPayeeTypeName(PayeeTypes PayeeType)
		{
			switch (PayeeType)
			{
			case PayeeTypes.Customer:
				return "Customer";
			case PayeeTypes.Vendor:
				return "Vendor";
			case PayeeTypes.Employee:
				return "Employee";
			case PayeeTypes.OtherPartner:
				return "Other";
			case PayeeTypes.BothCustomerVendor:
				return "Both Customer and Vendor";
			default:
				return "Other";
			}
		}
	}
}
