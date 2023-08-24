using System;
using System.ComponentModel;
using System.Data;
using System.Runtime.Serialization;

namespace Micromind.Common.Data
{
	[Serializable]
	[DesignerCategory("Code")]
	public class CompanyAccountData : DataSet
	{
		public const string ACCOUNTID_FIELD = "AccountID";

		public const string ACCOUNTNAME_FIELD = "AccountName";

		public const string ALIAS_FIELD = "Alias";

		public const string TYPEID_FIELD = "TypeID";

		public const string SUBTYPE_FIELD = "SubType";

		public const string CURRENCYID_FIELD = "CurrencyID";

		public const string BANKACCOUNTTYPE_FIELD = "BankAccountType";

		public const string BANKACCOUNTNUMBER_FIELD = "BankAccountNumber";

		public const string DATECREATED_FIELD = "DateCreated";

		public const string ISINACTIVE_FIELD = "IsInactive";

		public const string BALANCE_FIELD = "Balance";

		public const string INITIALAMOUNT_FIELD = "InitialBalance";

		public const string GROUPID_FIELD = "GroupID";

		public const string DATEUPDATED_FIELD = "DateUpdated";

		public const string NOTE_FIELD = "Note";

		public const string USERDEFINED1_FIELD = "UserDefined1";

		public const string USERDEFINED2_FIELD = "UserDefined2";

		public const string USERDEFINED3_FIELD = "UserDefined3";

		public const string USERDEFINED4_FIELD = "UserDefined4";

		public const string ACCOUNTS_TABLE = "Account";

		public DataTable CompanyAccountTable => base.Tables["Account"];

		public CompanyAccountData()
		{
			BuildDataTables();
		}

		public CompanyAccountData(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}

		private void BuildDataTables()
		{
			DataTable dataTable = new DataTable("Account");
			DataColumnCollection columns = dataTable.Columns;
			DataColumn dataColumn = columns.Add("AccountID", typeof(string));
			dataColumn.AllowDBNull = false;
			dataColumn.AutoIncrement = false;
			dataTable.PrimaryKey = new DataColumn[1]
			{
				dataColumn
			};
			columns.Add("AccountName", typeof(string)).AllowDBNull = false;
			columns.Add("Alias", typeof(string));
			columns.Add("CurrencyID", typeof(string));
			columns.Add("BankAccountType", typeof(string));
			columns.Add("BankAccountNumber", typeof(string));
			columns.Add("SubType", typeof(byte));
			columns.Add("DateCreated", typeof(DateTime));
			columns.Add("Balance", typeof(decimal)).DefaultValue = 0;
			columns.Add("Note", typeof(string));
			columns.Add("UserDefined1", typeof(string));
			columns.Add("UserDefined2", typeof(string));
			columns.Add("UserDefined3", typeof(string));
			columns.Add("UserDefined4", typeof(string));
			columns.Add("IsInactive", typeof(bool)).DefaultValue = false;
			columns.Add("GroupID", typeof(string));
			columns.Add("DateUpdated", typeof(DateTime)).DefaultValue = DateTime.Now.ToString();
			base.Tables.Add(dataTable);
		}
	}
}
