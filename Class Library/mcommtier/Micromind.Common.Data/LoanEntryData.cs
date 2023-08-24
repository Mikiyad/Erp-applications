using System;
using System.ComponentModel;
using System.Data;
using System.Runtime.Serialization;

namespace Micromind.Common.Data
{
	[Serializable]
	[DesignerCategory("Code")]
	public class LoanEntryData : DataSet
	{
		public const string VOUCHERID_FIELD = "VoucherID";

		public const string SYSDOCID_FIELD = "SysDocID";

		public const string LOANACCOUNTID_FIELD = "LoanAccountID";

		public const string INTERESTACCOUNTID_FIELD = "InterestAccountID";

		public const string LOANREPAYMENTACCOUNTID_FIELD = "LoanRepaymentAccountID";

		public const string LOANDATE_FIELD = "LoanDate";

		public const string INSTALLMENTNUMBER_FIELD = "InstallmentNumber";

		public const string LOANAMOUNT_FIELD = "LoanAmount";

		public const string DEDSTARTDATE_FIELD = "DedStartDate";

		public const string INTERESTRATE_FIELD = "InterestRate";

		public const string DESCRIPTION_FIELD = "Description";

		public const string NOTE_FIELD = "Note";

		public const string LOANTERMTYPE_FIELD = "LoanTermType";

		public const string MONTHLYEMI_FIELD = "MonthlyEMI";

		public const string CREATEDBY_FIELD = "CreatedBy";

		public const string DATECREATED_FIELD = "DateCreated";

		public const string UPDATEDBY_FIELD = "UpdatedBy";

		public const string DATEUPDATED_FIELD = "DateUpdated";

		public const string ISVOID_FIELD = "IsVoid";

		public const string ISCLOSED_FIELD = "IsClosed";

		public const string LOANENTRY_TABLE = "Loan_Entry";

		public const string LOANENTRYDETAIL_TABLE = "Loan_Entry_Detail";

		public const string LOANSYSDOCID_FIELD = "LoanSysDocID";

		public const string LOANVOUCHERID_FIELD = "LoanVoucherID";

		public const string INSTALLMENT_FIELD = "Installment";

		public const string TRANSACTIONDATE_FIELD = "TransactionDate";

		public const string INSTALLMENTAMOUNT_FIELD = "InstallmentAmount";

		public const string PRINCIPLE_FIELD = "Principle";

		public const string INTEREST_FIELD = "Interest";

		public const string OUTSTANDINGPAYMENT_FIELD = "OutStandingPayment";

		public DataTable LoanEntryTable => base.Tables["Loan_Entry"];

		public DataTable LoanEntryDetailTable => base.Tables["Loan_Entry_Detail"];

		public LoanEntryData()
		{
			BuildDataTables();
		}

		public LoanEntryData(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}

		private void BuildDataTables()
		{
			DataTable dataTable = new DataTable("Loan_Entry");
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
			columns.Add("LoanAccountID", typeof(string));
			columns.Add("InterestAccountID", typeof(string));
			columns.Add("LoanRepaymentAccountID", typeof(string));
			columns.Add("InstallmentNumber", typeof(string));
			columns.Add("LoanDate", typeof(DateTime));
			columns.Add("DedStartDate", typeof(DateTime));
			columns.Add("LoanAmount", typeof(decimal));
			columns.Add("InterestRate", typeof(decimal));
			columns.Add("Description", typeof(string));
			columns.Add("Note", typeof(string));
			columns.Add("IsVoid", typeof(bool));
			columns.Add("IsClosed", typeof(bool));
			columns.Add("LoanTermType", typeof(string));
			columns.Add("MonthlyEMI", typeof(decimal));
			base.Tables.Add(dataTable);
			dataTable = new DataTable("Loan_Entry_Detail");
			columns = dataTable.Columns;
			columns.Add("LoanSysDocID", typeof(string));
			columns.Add("LoanVoucherID", typeof(string));
			columns.Add("Installment", typeof(string));
			columns.Add("TransactionDate", typeof(DateTime));
			columns.Add("InstallmentAmount", typeof(decimal));
			columns.Add("Interest", typeof(decimal));
			columns.Add("Principle", typeof(decimal));
			columns.Add("OutStandingPayment", typeof(decimal));
			base.Tables.Add(dataTable);
		}
	}
}
