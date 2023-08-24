using System;
using System.ComponentModel;
using System.Data;
using System.Runtime.Serialization;

namespace Micromind.Common.Data
{
	[Serializable]
	[DesignerCategory("Code")]
	public class BuyerData : DataSet
	{
		public enum CommissionType
		{
			Sales = 1,
			Profit
		}

		public const string BUYER_TABLE = "Buyer";

		public const string BUYERID_FIELD = "BuyerID";

		public const string BUYERNAME_FIELD = "FullName";

		public const string EMPLOYEEID_FIELD = "EmployeeID";

		public const string ADDRESS_FIELD = "Address";

		public const string CITY_FIELD = "City";

		public const string COUNTRY_FIELD = "Country";

		public const string STATE_FIELD = "State";

		public const string POSTALCODE_FIELD = "PostalCode";

		public const string ADDRESSPRINTFORMAT_FIELD = "AddressPrintFormat";

		public const string PHONE1_FIELD = "Phone1";

		public const string PHONE2_FIELD = "Phone2";

		public const string MOBILE_FIELD = "Mobile";

		public const string FAX_FIELD = "Fax";

		public const string EMAIL_FIELD = "Email";

		public const string WEBSITE_FIELD = "Website";

		public const string AREAID_FIELD = "AreaID";

		public const string COUNTRYID_FIELD = "CountryID";

		public const string ISINACTIVE_FIELD = "IsInactive";

		public const string BANKNAME_FIELD = "BankName";

		public const string BANKBRANCH_FIELD = "BankBranch";

		public const string BANKACCOUNTNUMBER_FIELD = "BankAccountNumber";

		public const string NOTE_FIELD = "Note";

		public const string CREATEDBY_FIELD = "CreatedBy";

		public const string DATECREATED_FIELD = "DateCreated";

		public const string UPDATEDBY_FIELD = "UpdatedBy";

		public const string DATEUPDATED_FIELD = "DateUpdated";

		public DataTable BuyerTable => base.Tables["Buyer"];

		public BuyerData()
		{
			BuildDataTables();
		}

		public BuyerData(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}

		private void BuildDataTables()
		{
			DataTable dataTable = new DataTable("Buyer");
			DataColumnCollection columns = dataTable.Columns;
			DataColumn dataColumn = columns.Add("BuyerID", typeof(string));
			dataColumn.AllowDBNull = false;
			dataColumn.AutoIncrement = false;
			dataTable.PrimaryKey = new DataColumn[1]
			{
				dataColumn
			};
			columns.Add("FullName", typeof(string)).AllowDBNull = false;
			columns.Add("Note", typeof(string));
			columns.Add("EmployeeID", typeof(string));
			columns.Add("Address", typeof(string));
			columns.Add("City", typeof(string));
			columns.Add("Country", typeof(string));
			columns.Add("State", typeof(string));
			columns.Add("PostalCode", typeof(string));
			columns.Add("AddressPrintFormat", typeof(string));
			columns.Add("Phone1", typeof(string));
			columns.Add("Phone2", typeof(string));
			columns.Add("Mobile", typeof(string));
			columns.Add("Fax", typeof(string));
			columns.Add("Email", typeof(string));
			columns.Add("Website", typeof(string));
			columns.Add("BankName", typeof(string));
			columns.Add("BankBranch", typeof(string));
			columns.Add("BankAccountNumber", typeof(string));
			columns.Add("AreaID", typeof(string));
			columns.Add("CountryID", typeof(string));
			columns.Add("IsInactive", typeof(bool));
			base.Tables.Add(dataTable);
		}
	}
}
