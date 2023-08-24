using System;
using System.ComponentModel;
using System.Data;
using System.Runtime.Serialization;

namespace Micromind.Common.Data
{
	[Serializable]
	[DesignerCategory("Code")]
	public class BankFacilityTransactionData : DataSet
	{
		public const string TRANSACTIONID_FIELD = "TransactionID";

		public const string SYSDOCTYPE_FIELD = "SysDocType";

		public const string SYSDOCID_FIELD = "SysDocID";

		public const string VOUCHERID_FIELD = "VoucherID";

		public const string REGISTERID_FIELD = "RegisterID";

		public const string AMOUNT_FIELD = "Amount";

		public const string AMOUNTFC_FIELD = "AmountFC";

		public const string TRANSACTIONDATE_FIELD = "TransactionDate";

		public const string DUEDATE_FIELD = "DueDate";

		public const string CURRENCYID_FIELD = "CurrencyID";

		public const string CURRENCYRATE_FIELD = "CurrencyRate";

		public const string JOURNALID_FIELD = "JournalID";

		public const string REFERENCE_FIELD = "Reference";

		public const string BANKFACILITYID_FIELD = "BankFacilityID";

		public const string FACILITYTYPE_FIELD = "FacilityType";

		public const string DESCRIPTION_FIELD = "Description";

		public const string GLTYPE_FIELD = "GLType";

		public const string STATUS_FIELD = "TransactionStatus";

		public const string FIRSTACCOUNTID_FIELD = "FirstAccountID";

		public const string SECONDACCOUNTID_FIELD = "SecondAccountID";

		public const string REQUESTSYSDOCID_FIELD = "RequestSysDocID";

		public const string REQUESTVOUCHERID_FIELD = "RequestVoucherID";

		public const string SOURCESYSDOCID_FIELD = "SourceSysDocID";

		public const string SOURCEVOUCHERID_FIELD = "SourceVoucherID";

		public const string DIVISIONID_FIELD = "DivisionID";

		public const string COMPANYID_FIELD = "CompanyID";

		public const string BANKFACILITYTRANSACTION_TABLE = "Bank_Facility_Transaction";

		public const string CREATEDBY_FIELD = "CreatedBy";

		public const string DATECREATED_FIELD = "DateCreated";

		public const string UPDATEDBY_FIELD = "UpdatedBy";

		public const string DATEUPDATED_FIELD = "DateUpdated";

		public const string ACCOUNTID_FIELD = "AccountID";

		public const string ROWINDEX_FIELD = "RowIndex";

		public const string PAYEEID_FIELD = "PayeeID";

		public const string PAYEETYPE_FIELD = "PayeeType";

		public const string ANALYSISID_FIELD = "AnalysisID";

		public const string COSTCENTERID_FIELD = "CostCenterID";

		public const string ISVOID_FIELD = "IsVoid";

		public const string PAYMENTMETHODID_FIELD = "PaymentMethodID";

		public const string PAYMENTMETHODTYPE_FIELD = "PaymentMethodType";

		public const string BANKID_FIELD = "BankID";

		public const string CHEQUEBOOKID_FIELD = "ChequebookID";

		public const string EMPLOYEEID_FIELD = "EmployeeID";

		public const string BANKFACILITYTRANSACTIONDETAILS_TABLE = "Bank_Facility_Transaction_Details";

		public const string BANKFEEDETAILS_TABLE = "Bank_Fee_Details";

		public const string GLTRANSACTIONSYSDOCID_FIELD = "GLTransactionSysDocID";

		public const string GLTRANSACTIONVOUCHERID_FIELD = "GLTransactionVoucherID";

		public const string BANKACCOUNTID_FIELD = "BankAccountID";

		public const string EXPENSEACCOUNTID_FIELD = "ExpenseAccountID";

		public const string BANKFEEID_FIELD = "BankFeeID";

		public const string ISWITHTR_FIELD = "IsWithTR";

		public const string TAXGROUPID_FIELD = "TaxGroupID";

		public const string TAXAMOUNT_FIELD = "TaxAmount";

		public const string TAXOPTION_FIELD = "TaxOption";

		public const string TRANSACTIONNUMBER_FIELD = "TransactionNumber";

		public const string DATETIMESTAMP_FIELD = "DateTimeStamp";

		public DataTable BankFacilityTransactionTable => base.Tables["Bank_Facility_Transaction"];

		public DataTable BankFacilityTransactionDetailsTable => base.Tables["Bank_Facility_Transaction_Details"];

		public DataTable BankFeeDetailsTable => base.Tables["Bank_Fee_Details"];

		public BankFacilityTransactionData()
		{
			BuildDataTables();
		}

		public BankFacilityTransactionData(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}

		private void BuildDataTables()
		{
			DataTable dataTable = new DataTable("Bank_Facility_Transaction");
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
			columns.Add("DivisionID", typeof(string));
			columns.Add("CompanyID", typeof(string));
			columns.Add("SysDocType", typeof(byte));
			columns.Add("RegisterID", typeof(string));
			columns.Add("TransactionID", typeof(int));
			columns.Add("Amount", typeof(decimal));
			columns.Add("AmountFC", typeof(decimal));
			columns.Add("TransactionDate", typeof(DateTime));
			columns.Add("AccountID", typeof(string));
			columns.Add("AnalysisID", typeof(string));
			columns.Add("CurrencyID", typeof(string));
			columns.Add("CurrencyRate", typeof(decimal));
			columns.Add("JournalID", typeof(string));
			columns.Add("PayeeID", typeof(string));
			columns.Add("PayeeType", typeof(string));
			columns.Add("Reference", typeof(string));
			columns.Add("BankFacilityID", typeof(string));
			columns.Add("FacilityType", typeof(int));
			columns.Add("Description", typeof(string));
			columns.Add("CostCenterID", typeof(string));
			columns.Add("GLType", typeof(int));
			columns.Add("TransactionStatus", typeof(int));
			columns.Add("FirstAccountID", typeof(string));
			columns.Add("SecondAccountID", typeof(string));
			columns.Add("ChequebookID", typeof(string));
			columns.Add("EmployeeID", typeof(string));
			columns.Add("RequestSysDocID", typeof(string));
			columns.Add("RequestVoucherID", typeof(string));
			columns.Add("SourceSysDocID", typeof(string));
			columns.Add("SourceVoucherID", typeof(string));
			columns.Add("TaxAmount", typeof(decimal));
			base.Tables.Add(dataTable);
			dataTable = new DataTable("Bank_Facility_Transaction_Details");
			columns = dataTable.Columns;
			columns.Add("SysDocID", typeof(string));
			columns.Add("VoucherID", typeof(string));
			columns.Add("AccountID", typeof(string));
			columns.Add("Amount", typeof(decimal)).DefaultValue = 0;
			columns.Add("AmountFC", typeof(decimal));
			columns.Add("RowIndex", typeof(short));
			columns.Add("Description", typeof(string));
			columns.Add("BankFacilityID", typeof(string));
			columns.Add("FacilityType", typeof(int));
			columns.Add("Reference", typeof(string));
			columns.Add("BankID", typeof(string));
			columns.Add("DueDate", typeof(DateTime));
			columns.Add("PayeeID", typeof(string));
			columns.Add("PayeeType", typeof(string));
			columns.Add("AnalysisID", typeof(string));
			columns.Add("CostCenterID", typeof(string));
			columns.Add("ChequebookID", typeof(string));
			base.Tables.Add(dataTable);
			dataTable = new DataTable("Bank_Fee_Details");
			columns = dataTable.Columns;
			columns.Add("GLTransactionSysDocID", typeof(string));
			columns.Add("GLTransactionVoucherID", typeof(string));
			columns.Add("RowIndex", typeof(int));
			columns.Add("BankFacilityID", typeof(string));
			columns.Add("FacilityType", typeof(int));
			columns.Add("ChequebookID", typeof(string));
			columns.Add("Description", typeof(string));
			columns.Add("Reference", typeof(string));
			columns.Add("BankAccountID", typeof(string));
			columns.Add("ExpenseAccountID", typeof(string));
			columns.Add("BankFeeID", typeof(string));
			columns.Add("Amount", typeof(decimal));
			columns.Add("AmountFC", typeof(decimal));
			columns.Add("CurrencyID", typeof(string));
			columns.Add("IsWithTR", typeof(bool));
			columns.Add("CurrencyRate", typeof(decimal));
			columns.Add("TaxOption", typeof(byte));
			columns.Add("TaxGroupID", typeof(string));
			columns.Add("TaxAmount", typeof(decimal));
			base.Tables.Add(dataTable);
			Merge(new TaxTransactionData().TaxDetailTable);
		}
	}
}
