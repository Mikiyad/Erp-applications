using System;
using System.ComponentModel;
using System.Data;
using System.Runtime.Serialization;

namespace Micromind.Common.Data
{
	[Serializable]
	[DesignerCategory("Code")]
	public class BankAccountData : DataSet
	{
		public const string BANKACCOUNTID_FIELD = "BankAccountID";

		public const string ACCOUNTNAME_FIELD = "AccountName";

		public const string ACCOUNTNUMBER_FIELD = "AccountNumber";

		public const string BANKID_FIELD = "BankID";

		public const string ISCASHACCOUNT_FIELD = "IsCashAccount";

		public const string ISCHECKINGACCOUNT_FIELD = "IsCheckingAccount";

		public const string ISSAVINGACCOUNT_FIELD = "IsSavingAccount";

		public const string BANKACCOUNTS_TABLE = "[BankAccounts]";

		public DataTable BankAccountTable => base.Tables["[BankAccounts]"];

		public BankAccountData()
		{
			BuildDataTables();
		}

		public BankAccountData(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}

		private void BuildDataTables()
		{
			DataTable dataTable = new DataTable("[BankAccounts]");
			DataColumnCollection columns = dataTable.Columns;
			DataColumn dataColumn = columns.Add("BankAccountID", typeof(int));
			dataColumn.AllowDBNull = true;
			dataColumn.AutoIncrement = false;
			columns.Add("AccountName", typeof(string)).AllowDBNull = false;
			columns.Add("BankID", typeof(int)).AllowDBNull = false;
			columns.Add("AccountNumber", typeof(string)).AllowDBNull = false;
			columns.Add("IsCashAccount", typeof(bool)).DefaultValue = false;
			columns.Add("IsCheckingAccount", typeof(bool)).DefaultValue = false;
			columns.Add("IsSavingAccount", typeof(bool)).DefaultValue = false;
			foreach (DataColumn column in new CompanyAccountData().CompanyAccountTable.Columns)
			{
				if (!column.AutoIncrement)
				{
					DataColumn dataColumn3 = columns.Add(column.ColumnName, column.DataType);
					dataColumn3.AllowDBNull = column.AllowDBNull;
					dataColumn3.DefaultValue = column.DefaultValue;
				}
			}
			base.Tables.Add(dataTable);
		}
	}
}
