using System;
using System.ComponentModel;
using System.Data;
using System.Runtime.Serialization;

namespace Micromind.Common.Data
{
	[Serializable]
	[DesignerCategory("Code")]
	public class EmployeeAddressData : DataSet
	{
		public const string EMPLOYEEADDRESS_TABLE = "Employee_Address";

		public const string EMPLOYEEID_FIELD = "EmployeeID";

		public const string ADDRESSID_FIELD = "AddressID";

		public const string CONTACTNAME_FIELD = "ContactName";

		public const string ADDRESS1_FIELD = "Address1";

		public const string ADDRESS2_FIELD = "Address2";

		public const string ADDRESS3_FIELD = "Address3";

		public const string ADDRESSPRINTFORMAT_FIELD = "AddressPrintFormat";

		public const string CITY_FIELD = "City";

		public const string STATE_FIELD = "State";

		public const string COUNTRY_FIELD = "Country";

		public const string POSTALCODE_FIELD = "PostalCode";

		public const string DEPARTMENT_FIELD = "Department";

		public const string PHONE1_FIELD = "Phone1";

		public const string PHONE2_FIELD = "Phone2";

		public const string FAX_FIELD = "Fax";

		public const string MOBILE_FIELD = "Mobile";

		public const string EMAIL_FIELD = "Email";

		public const string WEBSITE_FIELD = "Website";

		public const string COMMENT_FIELD = "Comment";

		public const string CREATEDBY_FIELD = "CreatedBy";

		public const string DATECREATED_FIELD = "DateCreated";

		public const string UPDATEDBY_FIELD = "UpdatedBy";

		public const string DATEUPDATED_FIELD = "DateUpdated";

		public DataTable EmployeeAddressTable => base.Tables["Employee_Address"];

		public EmployeeAddressData()
		{
			BuildDataTables();
		}

		public EmployeeAddressData(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}

		private void BuildDataTables()
		{
			DataTable dataTable = new DataTable("Employee_Address");
			DataColumnCollection columns = dataTable.Columns;
			DataColumn dataColumn = columns.Add("AddressID", typeof(string));
			dataColumn.AllowDBNull = false;
			dataColumn.AutoIncrement = false;
			DataColumn dataColumn2 = columns.Add("EmployeeID", typeof(string));
			dataColumn.AllowDBNull = false;
			dataColumn.AutoIncrement = false;
			dataTable.PrimaryKey = new DataColumn[2]
			{
				dataColumn,
				dataColumn2
			};
			columns.Add("ContactName", typeof(string));
			columns.Add("Address1", typeof(string));
			columns.Add("Address2", typeof(string));
			columns.Add("Address3", typeof(string));
			columns.Add("AddressPrintFormat", typeof(string));
			columns.Add("City", typeof(string));
			columns.Add("State", typeof(string));
			columns.Add("Country", typeof(string));
			columns.Add("PostalCode", typeof(string));
			columns.Add("Department", typeof(string));
			columns.Add("Phone1", typeof(string));
			columns.Add("Phone2", typeof(string));
			columns.Add("Fax", typeof(string));
			columns.Add("Mobile", typeof(string));
			columns.Add("Email", typeof(string));
			columns.Add("Website", typeof(string));
			columns.Add("Comment", typeof(string));
			base.Tables.Add(dataTable);
		}
	}
}
