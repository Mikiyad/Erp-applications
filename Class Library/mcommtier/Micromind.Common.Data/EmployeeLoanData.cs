using System;
using System.ComponentModel;
using System.Data;
using System.Runtime.Serialization;

namespace Micromind.Common.Data
{
	[Serializable]
	[DesignerCategory("Code")]
	public class EmployeeLoanData : DataSet
	{
		public const string LOANID_FIELD = "LoanID";

		public const string VOUCHERID_FIELD = "VoucherID";

		public const string SYSDOCID_FIELD = "SysDocID";

		public const string EMPLOYEEID_FIELD = "EmployeeID";

		public const string TRANSACTIONDATE_FIELD = "TransactionDate";

		public const string LOANTYPE_FIELD = "LoanType";

		public const string AMOUNT_FIELD = "Amount";

		public const string INSTALLMENTAMOUNT_FIELD = "InstallmentAmount";

		public const string DEDSTARTDATE_FIELD = "DedStartDate";

		public const string DEDUCTEDAMOUNT_FIELD = "DeductedAmount";

		public const string DISCOUNTDATE_FIELD = "DiscountDate";

		public const string DISCOUNTAMOUNT_FIELD = "DiscountAmount";

		public const string REASON_FIELD = "Reason";

		public const string REFERENCE_FIELD = "Reference";

		public const string NOTE_FIELD = "Note";

		public const string CREATEDBY_FIELD = "CreatedBy";

		public const string DATECREATED_FIELD = "DateCreated";

		public const string UPDATEDBY_FIELD = "UpdatedBy";

		public const string DATEUPDATED_FIELD = "DateUpdated";

		public const string LOANACCOUNTID_FIELD = "LoanAccountID";

		public const string EMPLOYEEACCOUNTID_FIELD = "EmployeeAccountID";

		public const string ISVOID_FIELD = "IsVoid";

		public const string DIVISIONID_FIELD = "DivisionID";

		public const string COMPANYID_FIELD = "CompanyID";

		public const string EMPLOYEELOAN_TABLE = "Employee_Loan";

		public const string EMPLOYEELOANDETAIL_TABLE = "Employee_Loan_Detail";

		public const string DEDUCTIONDATE_FIELD = "DeductionDate";

		public const string LOANSYSDOCID_FIELD = "LoanSysDocID";

		public const string LOANVOUCHERID_FIELD = "LoanVoucherID";

		public const string PAYMENTSYSDOCID_FIELD = "PaymentSysDocID";

		public const string PAYMENTVOUCHERID_FIELD = "PaymentVoucherID";

		public const string DESCRIPTION_FIELD = "Description";

		public const string DEBIT_FIELD = "Debit";

		public const string CREDIT_FIELD = "Credit";

		public const string EMPLOYEELOANPAYMENT_TABLE = "Employee_Loan_Payment";

		public const string SETTLEMENTAMOUNT_FIELD = "SettlementAmount";

		public const string EMPLOYEELOANSETTLEMENT_TABLE = "Employee_Loan_Settlement";

		public DataTable EmployeeLoanTable => base.Tables["Employee_Loan"];

		public DataTable EmployeeLoanDetailTable => base.Tables["Employee_Loan_Detail"];

		public DataTable EmployeeLoanPaymentTable => base.Tables["Employee_Loan_Payment"];

		public DataTable EmployeeLoanSettlementTable => base.Tables["Employee_Loan_Settlement"];

		public EmployeeLoanData()
		{
			BuildDataTables();
		}

		public EmployeeLoanData(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}

		private void BuildDataTables()
		{
			DataTable dataTable = new DataTable("Employee_Loan");
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
			columns.Add("EmployeeID", typeof(string));
			columns.Add("TransactionDate", typeof(DateTime));
			columns.Add("LoanType", typeof(string));
			columns.Add("Amount", typeof(decimal));
			columns.Add("InstallmentAmount", typeof(decimal));
			columns.Add("DedStartDate", typeof(DateTime));
			columns.Add("DeductedAmount", typeof(decimal));
			columns.Add("DiscountAmount", typeof(decimal));
			columns.Add("DiscountDate", typeof(DateTime));
			columns.Add("Reason", typeof(string));
			columns.Add("Reference", typeof(string));
			columns.Add("Note", typeof(string));
			columns.Add("LoanAccountID", typeof(string));
			columns.Add("EmployeeAccountID", typeof(string));
			columns.Add("IsVoid", typeof(bool));
			columns.Add("CompanyID", typeof(string));
			columns.Add("DivisionID", typeof(string));
			base.Tables.Add(dataTable);
			dataTable = new DataTable("Employee_Loan_Detail");
			columns = dataTable.Columns;
			columns.Add("LoanSysDocID", typeof(string));
			columns.Add("LoanVoucherID", typeof(string));
			columns.Add("PaymentSysDocID", typeof(string));
			columns.Add("PaymentVoucherID", typeof(string));
			columns.Add("Description", typeof(string));
			columns.Add("EmployeeID", typeof(string));
			columns.Add("TransactionDate", typeof(DateTime));
			columns.Add("Reference", typeof(string));
			columns.Add("Debit", typeof(decimal));
			columns.Add("Credit", typeof(decimal));
			base.Tables.Add(dataTable);
			dataTable = new DataTable("Employee_Loan_Payment");
			columns = dataTable.Columns;
			columns.Add("SysDocID", typeof(string));
			columns.Add("VoucherID", typeof(string));
			columns.Add("LoanSysDocID", typeof(string));
			columns.Add("LoanVoucherID", typeof(string));
			columns.Add("EmployeeID", typeof(string));
			columns.Add("Amount", typeof(decimal));
			columns.Add("Description", typeof(string));
			columns.Add("TransactionDate", typeof(DateTime));
			columns.Add("Reference", typeof(string));
			columns.Add("Note", typeof(string));
			columns.Add("CompanyID", typeof(string));
			columns.Add("DivisionID", typeof(string));
			base.Tables.Add(dataTable);
			dataTable = new DataTable("Employee_Loan_Settlement");
			columns = dataTable.Columns;
			columns.Add("SysDocID", typeof(string));
			columns.Add("VoucherID", typeof(string));
			columns.Add("LoanSysDocID", typeof(string));
			columns.Add("LoanVoucherID", typeof(string));
			columns.Add("EmployeeID", typeof(string));
			columns.Add("TransactionDate", typeof(DateTime));
			columns.Add("LoanType", typeof(string));
			columns.Add("Amount", typeof(decimal));
			columns.Add("SettlementAmount", typeof(decimal));
			columns.Add("Reference", typeof(string));
			columns.Add("Note", typeof(string));
			columns.Add("LoanAccountID", typeof(string));
			columns.Add("EmployeeAccountID", typeof(string));
			columns.Add("CompanyID", typeof(string));
			columns.Add("DivisionID", typeof(string));
			base.Tables.Add(dataTable);
		}
	}
}
