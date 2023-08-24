using System;
using System.ComponentModel;
using System.Data;
using System.Runtime.Serialization;

namespace Micromind.Common.Data
{
	[Serializable]
	[DesignerCategory("Code")]
	public class PayrollTransactionData : DataSet
	{
		public const string PAYROLLTRANSACTION_TABLE = "Payroll_Transaction";

		public const string PAYROLLTRANSACTIONDETAIL_TABLE = "Payroll_Transaction_Detail";

		public const string SYSDOCID_FIELD = "SysDocID";

		public const string VOUCHERID_FIELD = "VoucherID";

		public const string REGISTERID_FIELD = "RegisterID";

		public const string MONTH_FIELD = "Month";

		public const string STARTDATE_FIELD = "StartDate";

		public const string ENDDATE_FIELD = "EndDate";

		public const string AMOUNT_FIELD = "Amount";

		public const string AMOUNTFC_FIELD = "AmountFC";

		public const string TRANSACTIONDATE_FIELD = "TransactionDate";

		public const string ISVOID_FIELD = "IsVoid";

		public const string CURRENCYID_FIELD = "CurrencyID";

		public const string CURRENCYRATE_FIELD = "CurrencyRate";

		public const string GLTYPE_FIELD = "GLType";

		public const string JOURNALID_FIELD = "JournalID";

		public const string CHEQUEID_FIELD = "ChequeID";

		public const string CHEQUEBOOKID_FIELD = "ChequebookID";

		public const string CHECKNUMBER_FIELD = "CheckNumber";

		public const string CHECKDATE_FIELD = "CheckDate";

		public const string REFERENCE_FIELD = "Reference";

		public const string BANKACCOUNTID_FIELD = "BankAccountID";

		public const string DESCRIPTION_FIELD = "Description";

		public const string TYPEID_FIELD = "TypeID";

		public const string PAYMENTMETHODTYPE_FIELD = "PaymentMethodType";

		public const string PAYTYPE_FIELD = "PayType";

		public const string CHEQUETOTAL_FIELD = "ChequeTotal";

		public const string OTHERCHARGES_FIELD = "OtherCharges";

		public const string OTHERACCOUNTID_FIELD = "OtherAccountID";

		public const string TAXGROUPID_FIELD = "TaxGroupID";

		public const string TAXAMOUNT_FIELD = "TaxAmount";

		public const string DIVISIONID_FIELD = "DivisionID";

		public const string COMPANYID_FIELD = "CompanyID";

		public const string EMPLOYEEID_FIELD = "EmployeeID";

		public const string PAYROLLITEMID_FIELD = "PayrollItemID";

		public const string ACCOUNTID_FIELD = "AccountID";

		public const string ROWINDEX_FIELD = "RowIndex";

		public const string ANALYSISID_FIELD = "AnalysisID";

		public const string COSTCENTERID_FIELD = "CostCenterID";

		public const string DAYS_FIELD = "Days";

		public const string SHEETSYSDOCID_FIELD = "SheetSysDocID";

		public const string SHEETVOUCHERID_FIELD = "SheetVoucherID";

		public const string LOANSYSDOCID_FIELD = "LoanSysDocID";

		public const string LOANVOUCHERID_FIELD = "LoanVoucherID";

		public const string SHEETROWINDEX_FIELD = "SheetRowIndex";

		public const string YEAR_FIELD = "Year";

		public DataTable PayrollTransactionTable => base.Tables["Payroll_Transaction"];

		public DataTable PayrollTransactionDetailTable => base.Tables["Payroll_Transaction_Detail"];

		public DataTable TaxDetailsTable => base.Tables["Tax_Detail"];

		public PayrollTransactionData()
		{
			BuildDataTables();
		}

		public PayrollTransactionData(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}

		private void BuildDataTables()
		{
			DataTable dataTable = new DataTable("Payroll_Transaction");
			DataColumnCollection columns = dataTable.Columns;
			DataColumn dataColumn = columns.Add("SysDocID", typeof(string));
			DataColumn dataColumn2 = columns.Add("VoucherID", typeof(string));
			dataColumn.AllowDBNull = false;
			dataColumn2.AllowDBNull = false;
			dataTable.PrimaryKey = new DataColumn[2]
			{
				dataColumn,
				dataColumn2
			};
			columns.Add("CompanyID", typeof(string));
			columns.Add("DivisionID", typeof(string));
			columns.Add("RegisterID", typeof(string));
			columns.Add("TransactionDate", typeof(DateTime)).DefaultValue = DateTime.Now;
			columns.Add("Amount", typeof(decimal));
			columns.Add("AmountFC", typeof(decimal));
			columns.Add("Month", typeof(byte));
			columns.Add("Year", typeof(byte));
			columns.Add("IsVoid", typeof(bool));
			columns.Add("CurrencyID", typeof(string));
			columns.Add("CurrencyRate", typeof(decimal));
			columns.Add("GLType", typeof(byte));
			columns.Add("ChequeID", typeof(int));
			columns.Add("ChequebookID", typeof(string));
			columns.Add("CheckNumber", typeof(string));
			columns.Add("Reference", typeof(string));
			columns.Add("Description", typeof(string));
			columns.Add("CheckDate", typeof(DateTime));
			columns.Add("BankAccountID", typeof(string));
			columns.Add("CostCenterID", typeof(string));
			columns.Add("TypeID", typeof(byte));
			columns.Add("PaymentMethodType", typeof(byte));
			columns.Add("TaxGroupID", typeof(string));
			columns.Add("TaxAmount", typeof(decimal));
			columns.Add("ChequeTotal", typeof(decimal));
			columns.Add("OtherCharges", typeof(decimal));
			columns.Add("OtherAccountID", typeof(string));
			base.Tables.Add(dataTable);
			dataTable = new DataTable("Payroll_Transaction_Detail");
			columns = dataTable.Columns;
			columns.Add("SysDocID", typeof(string));
			columns.Add("VoucherID", typeof(string));
			columns.Add("SheetSysDocID", typeof(string));
			columns.Add("SheetVoucherID", typeof(string));
			columns.Add("SheetRowIndex", typeof(int));
			columns.Add("EmployeeID", typeof(string));
			columns.Add("AccountID", typeof(string));
			columns.Add("Days", typeof(decimal));
			columns.Add("PayType", typeof(byte));
			columns.Add("Description", typeof(string));
			columns.Add("Amount", typeof(decimal));
			columns.Add("AmountFC", typeof(decimal));
			columns.Add("RowIndex", typeof(short));
			base.Tables.Add(dataTable);
			Merge(new TaxTransactionData().TaxDetailTable);
		}
	}
}
