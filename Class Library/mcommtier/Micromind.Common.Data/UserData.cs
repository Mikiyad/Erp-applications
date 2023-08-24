using System;
using System.ComponentModel;
using System.Data;
using System.Runtime.Serialization;

namespace Micromind.Common.Data
{
	[Serializable]
	[DesignerCategory("Code")]
	public class UserData : DataSet
	{
		public const string USER_TABLE = "Users";

		public const string USERID_FIELD = "UserID";

		public const string USERNAME_FIELD = "UserName";

		public const string EMPLOYEEID_FIELD = "EmployeeID";

		public const string LOCATIONID_FIELD = "LocationID";

		public const string DEFAULTSALESPERSON_FIELD = "DefaultSalesPersonID";

		public const string DEFAULTINVENTORYLOCATION_FIELD = "DefaultInventoryLocationID";

		public const string DEFAULTTRANSACTIONLOCATION_FIELD = "DefaultTransactionLocationID";

		public const string DEFAULTTRANSACTIONREGISTER_FIELD = "DefaultTransactionRegisterID";

		public const string ISCLUSER_FIELD = "IsCLUser";

		public const string CLUSERPASS_FIELD = "CLUserPass";

		public const string PHONE_FIELD = "Phone";

		public const string EMAIL_FIELD = "Email";

		public const string PASSWORD_FIELD = "Password";

		public const string NOTE_FIELD = "Note";

		public const string INACTIVE_FIELD = "Inactive";

		public const string ISADMIN_FIELD = "IsAdmin";

		public const string CREATEDBY_FIELD = "CreatedBy";

		public const string DATECREATED_FIELD = "DateCreated";

		public const string UPDATEDBY_FIELD = "UpdatedBy";

		public const string DATEUPDATED_FIELD = "DateUpdated";

		public DataTable UserTable => base.Tables["Users"];

		public UserData()
		{
			BuildDataTables();
		}

		public UserData(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}

		private void BuildDataTables()
		{
			DataTable dataTable = new DataTable("Users");
			DataColumnCollection columns = dataTable.Columns;
			DataColumn dataColumn = columns.Add("UserID", typeof(string));
			dataColumn.AllowDBNull = false;
			dataColumn.AutoIncrement = false;
			dataTable.PrimaryKey = new DataColumn[1]
			{
				dataColumn
			};
			columns.Add("UserName", typeof(string)).AllowDBNull = false;
			columns.Add("EmployeeID", typeof(string));
			columns.Add("DefaultSalesPersonID", typeof(string));
			columns.Add("DefaultInventoryLocationID", typeof(string));
			columns.Add("DefaultTransactionLocationID", typeof(string));
			columns.Add("DefaultTransactionRegisterID", typeof(string));
			columns.Add("LocationID", typeof(string));
			columns.Add("Phone", typeof(string));
			columns.Add("IsCLUser", typeof(bool));
			columns.Add("CLUserPass", typeof(string));
			columns.Add("Email", typeof(string));
			columns.Add("Password", typeof(string));
			columns.Add("Note", typeof(string));
			columns.Add("Inactive", typeof(bool)).DefaultValue = false;
			columns.Add("IsAdmin", typeof(bool)).DefaultValue = false;
			base.Tables.Add(dataTable);
		}
	}
}
