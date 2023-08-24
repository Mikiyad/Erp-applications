using System;
using System.ComponentModel;
using System.Data;
using System.Runtime.Serialization;

namespace Micromind.Common.Data
{
	[Serializable]
	[DesignerCategory("Code")]
	public class EmployeeJournalData : DataSet
	{
		public const string EMPLOYEEJOURNAL_TABLE = "Employee_Journal";

		public const string EMPLOYEEJOURNALID_FIELD = "EmpJournalID";

		public const string SYSDOCID_FIELD = "SysDocID";

		public const string VOUCHERID_FIELD = "VoucherID";

		public const string EMPLOYEEID_FIELD = "EmployeeID";

		public const string REFERENCE_FIELD = "Reference";

		public const string ACCOUNTID_FIELD = "AccountID";

		public const string DEBIT_FIELD = "Debit";

		public const string CREDIT_FIELD = "Credit";

		public const string DEBITFC_FIELD = "DebitFC";

		public const string CREDITFC_FIELD = "CreditFC";

		public const string COSTCENTERID_FIELD = "CostCenterID";

		public const string CURRENCYID_FIELD = "CurrencyID";

		public const string CURRENCYRATE_FIELD = "CurrencyRate";

		public const string DESCRIPTION_FIELD = "Description";

		public const string JOURNALID_FIELD = "JournalID";

		public const string JOURNALDATE_FIELD = "JournalDate";

		public const string CHEQUENUMBER_FIELD = "ChequeNumber";

		public const string BANKID_FIELD = "BankID";

		public const string CHEQUEDATE_FIELD = "ChequeDate";

		public const string PAYMENTMETHODID_FIELD = "PaymentMethodID";

		public const string PAYMENTMETHODTYPE_FIELD = "PaymentMethodType";

		public DataTable EmployeeJournalTable => base.Tables["Employee_Journal"];

		public EmployeeJournalData()
		{
			BuildDataTables();
		}

		public EmployeeJournalData(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}

		private void BuildDataTables()
		{
			DataTable dataTable = new DataTable("Employee_Journal");
			DataColumnCollection columns = dataTable.Columns;
			DataColumn dataColumn = columns.Add("EmpJournalID", typeof(int));
			dataColumn.AllowDBNull = false;
			dataColumn.AutoIncrement = true;
			dataTable.PrimaryKey = new DataColumn[1]
			{
				dataColumn
			};
			columns.Add("SysDocID", typeof(string));
			columns.Add("VoucherID", typeof(string));
			columns.Add("EmployeeID", typeof(string));
			columns.Add("Reference", typeof(string));
			columns.Add("AccountID", typeof(string));
			columns.Add("Debit", typeof(decimal));
			columns.Add("Credit", typeof(decimal));
			columns.Add("DebitFC", typeof(decimal));
			columns.Add("CreditFC", typeof(decimal));
			columns.Add("CostCenterID", typeof(string));
			columns.Add("CurrencyID", typeof(string));
			columns.Add("CurrencyRate", typeof(decimal));
			columns.Add("Description", typeof(string));
			columns.Add("JournalID", typeof(int));
			columns.Add("JournalDate", typeof(DateTime));
			columns.Add("ChequeNumber", typeof(string));
			columns.Add("BankID", typeof(string));
			columns.Add("ChequeDate", typeof(DateTime));
			columns.Add("PaymentMethodType", typeof(byte));
			base.Tables.Add(dataTable);
		}
	}
}
