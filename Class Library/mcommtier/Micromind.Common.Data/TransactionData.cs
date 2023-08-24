using System;
using System.ComponentModel;
using System.Data;
using System.Runtime.Serialization;

namespace Micromind.Common.Data
{
	[Serializable]
	[DesignerCategory("Code")]
	public class TransactionData : DataSet
	{
		public const string TRANSACTIONID_FIELD = "TransactionID";

		public const string SYSDOCTYPE_FIELD = "SysDocType";

		public const string SYSDOCID_FIELD = "SysDocID";

		public const string VOUCHERID_FIELD = "VoucherID";

		public const string REGISTERID_FIELD = "RegisterID";

		public const string ISPOS_FIELD = "IsPOS";

		public const string AMOUNT_FIELD = "Amount";

		public const string AMOUNTFC_FIELD = "AmountFC";

		public const string EXPAMOUNT_FIELD = "ExpAmount";

		public const string EXPCODE_FIELD = "ExpCode";

		public const string EXPPERCENT_FIELD = "ExpPercent";

		public const string TRANSACTIONDATE_FIELD = "TransactionDate";

		public const string DUEDATE_FIELD = "DueDate";

		public const string CURRENCYID_FIELD = "CurrencyID";

		public const string CURRENCYRATE_FIELD = "CurrencyRate";

		public const string JOURNALID_FIELD = "JournalID";

		public const string REFERENCE_FIELD = "Reference";

		public const string BANKFACILITYID_FIELD = "BankFacilityID";

		public const string REASONID_FIELD = "ReasonID";

		public const string DESCRIPTION_FIELD = "Description";

		public const string GLTYPE_FIELD = "GLType";

		public const string STATUS_FIELD = "TransactionStatus";

		public const string TRANSFERFROMTYPE_FIELD = "TransferFromType";

		public const string TRANSFERTOTYPE_FIELD = "TransferToType";

		public const string FIRSTACCOUNTID_FIELD = "FirstAccountID";

		public const string SECONDACCOUNTID_FIELD = "SecondAccountID";

		public const string SECONDREGISTERID_FIELD = "SecondRegisterID";

		public const string ISSECONDFORM_FIELD = "IsSecondForm";

		public const string REQUESTSYSDOCID_FIELD = "RequestSysDocID";

		public const string REQUESTVOUCHERID_FIELD = "RequestVoucherID";

		public const string POSSHIFTID_FIELD = "POSShiftID";

		public const string POSBATCHID_FIELD = "POSBatchID";

		public const string TAXGROUPID_FIELD = "TaxGroupID";

		public const string TAXAMOUNT_FIELD = "TaxAmount";

		public const string TAXOPTION_FIELD = "TaxOption";

		public const string CHANGESTATUS_FIELD = "ChangeStatus";

		public const string DIVISIONID_FIELD = "DivisionID";

		public const string COMPANYID_FIELD = "CompanyID";

		public const string TRANSACTION_TABLE = "GL_Transaction";

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

		public const string CHECKNUMBER_FIELD = "CheckNumber";

		public const string CHECKDATE_FIELD = "CheckDate";

		public const string CHEQUEBOOKID_FIELD = "ChequebookID";

		public const string CHEQUEID_FIELD = "ChequeID";

		public const string EMPLOYEEID_FIELD = "EmployeeID";

		public const string JOBID_FIELD = "JobID";

		public const string COSTCATEGORYID_FIELD = "CostCategoryID";

		public const string CONSIGNID_FIELD = "ConsignID";

		public const string CONSIGNEXPENSEID_FIELD = "ConsignExpenseID";

		public const string ATTRIBUTEID1_FIELD = "AttributeID1";

		public const string ATTRIBUTEID2_FIELD = "AttributeID2";

		public const string REFERENCEDATE_FIELD = "RefDate";

		public const string TRANSACTIONDETAILS_TABLE = "Transaction_Details";

		public const string BANKFEEDETAILS_TABLE = "Bank_Fee_Details";

		public const string GLTRANSACTIONSYSDOCID_FIELD = "GLTransactionSysDocID";

		public const string GLTRANSACTIONVOUCHERID_FIELD = "GLTransactionVoucherID";

		public const string BANKACCOUNTID_FIELD = "BankAccountID";

		public const string EXPENSEACCOUNTID_FIELD = "ExpenseAccountID";

		public const string BANKFEEID_FIELD = "BankFeeID";

		public const string TRANSACTIONNUMBER_FIELD = "TransactionNumber";

		public const string PARTYONEACCOUNTID_FIELD = "PartyOneAccountID";

		public const string PARTYTWOACCOUNTID_FIELD = "PartyTwoAccountID";

		public const string PARTYONEACCOUNTNAME_FIELD = "PartyOneAccountName";

		public const string PARTYTWOACCOUNTNAME_FIELD = "PartyTwoAccountName";

		public const string ISACCOUNTTRANSACTION_FIELD = "IsAccountTransaction";

		public const string ISPARTNERTRANSACTION_FIELD = "IsPartnerTransaction";

		public const string ISPAYROLLTRANSACTION_FIELD = "IsPayrollTransaction";

		public const string ISDEBIT_FIELD = "IsDebit";

		public const string BANKRECONCILIATIONID_FIELD = "BankReconciliationID";

		public const string JOBNAME_FIELD = "JobName";

		public const string STOREID_FIELD = "StoreID";

		public const string STORENAME_FIELD = "StoreName";

		public const string PAYEE_FIELD = "PAYEE";

		public const string PAYEENAME_FIELD = "PayeeName";

		public const string PARTNERID_FIELD = "PartnerID";

		public const string BANKNAME_FIELD = "BankName";

		public const string CHECKDESCRIPTIONS_FIELD = "CheckDescriptions";

		public const string PARTNERTRANSACTION_TABLE = "[Partner Transactions]";

		public const string ACCOUNTTRANSACTION_TABLE = "[Account Transactions]";

		public const string PAYROLLTRANSACTION_TABLE = "[Payroll Transactions]";

		public const string PAYROLLTRANSACTIONDETAILS_TABLE = "[Payroll Transaction Details]";

		public const string DATETIMESTAMP_FIELD = "DateTimeStamp";

		public const string GENERALPAYMENTDETAIL_TABLE = "General_Payment_Detail";

		public const string SOURCESYSDOCID_FIELD = "SourceSysDocID";

		public const string SOURCEVOUCHERID_FIELD = "SourceVoucherID";

		public const string EXPENSES_TABLE = "Expenses";

		public const string EXPENSEENTRYID_FIELD = "ExpenseEntryID";

		public const string ACCOUNT_FIELD = "Account";

		public const string EXPENSEACCOUNTNAME_FIELD = "AccountName";

		public const string EMPLOEEID_FIELD = "EmployeeID";

		public const string TYPE_FIELD = "Type";

		public const string TYPEID_FIELD = "TypeID";

		public const string NUNBER_FIELD = "Number";

		public const string CREDITCARDID_FIELD = "CreditCardID";

		public const string CHECKDELIVEREDDATE_FIELD = "CheckDeliveredDate";

		public DataTable TransactionTable => base.Tables["GL_Transaction"];

		public DataTable TransactionDetailsTable => base.Tables["Transaction_Details"];

		public DataTable BankFeeDetailsTable => base.Tables["Bank_Fee_Details"];

		public DataTable GeneralPaymentDetailsTable => base.Tables["General_Payment_Detail"];

		public DataTable TaxDetailsTable => base.Tables["Tax_Detail"];

		public TransactionData()
		{
			BuildDataTables();
		}

		public TransactionData(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}

		private void BuildDataTables()
		{
			DataTable dataTable = new DataTable("GL_Transaction");
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
			columns.Add("ExpAmount", typeof(decimal));
			columns.Add("ExpPercent", typeof(decimal));
			columns.Add("ExpCode", typeof(string));
			columns.Add("IsPOS", typeof(bool));
			columns.Add("POSShiftID", typeof(int));
			columns.Add("POSBatchID", typeof(int));
			columns.Add("TransactionDate", typeof(DateTime));
			columns.Add("AccountID", typeof(string));
			columns.Add("AnalysisID", typeof(string));
			columns.Add("CurrencyID", typeof(string));
			columns.Add("CurrencyRate", typeof(decimal));
			columns.Add("JournalID", typeof(string));
			columns.Add("PayeeID", typeof(string));
			columns.Add("PayeeType", typeof(string));
			columns.Add("Reference", typeof(string));
			columns.Add("ReasonID", typeof(string));
			columns.Add("Description", typeof(string));
			columns.Add("CostCenterID", typeof(string));
			columns.Add("RequestSysDocID", typeof(string));
			columns.Add("RequestVoucherID", typeof(string));
			columns.Add("TaxGroupID", typeof(string));
			columns.Add("TaxAmount", typeof(decimal));
			columns.Add("GLType", typeof(int));
			columns.Add("TransactionStatus", typeof(int));
			columns.Add("TransferFromType", typeof(char));
			columns.Add("TransferToType", typeof(char));
			columns.Add("FirstAccountID", typeof(string));
			columns.Add("SecondAccountID", typeof(string));
			columns.Add("SecondRegisterID", typeof(string));
			columns.Add("ChequeID", typeof(int));
			columns.Add("CheckNumber", typeof(string));
			columns.Add("ChequebookID", typeof(string));
			columns.Add("CheckDate", typeof(DateTime));
			columns.Add("EmployeeID", typeof(string));
			columns.Add("IsSecondForm", typeof(bool));
			columns.Add("ChangeStatus", typeof(int));
			columns.Add("CheckDeliveredDate", typeof(DateTime));
			base.Tables.Add(dataTable);
			dataTable = new DataTable("Transaction_Details");
			columns = dataTable.Columns;
			columns.Add("SysDocID", typeof(string));
			columns.Add("VoucherID", typeof(string));
			columns.Add("AccountID", typeof(string));
			columns.Add("Amount", typeof(decimal)).DefaultValue = 0;
			columns.Add("AmountFC", typeof(decimal));
			columns.Add("RowIndex", typeof(long));
			columns.Add("ExpAmount", typeof(decimal));
			columns.Add("ExpPercent", typeof(decimal));
			columns.Add("ExpCode", typeof(string));
			columns.Add("Description", typeof(string));
			columns.Add("JobID", typeof(string));
			columns.Add("CostCategoryID", typeof(string));
			columns.Add("ConsignID", typeof(string));
			columns.Add("ConsignExpenseID", typeof(string));
			columns.Add("Reference", typeof(string));
			columns.Add("BankID", typeof(string));
			columns.Add("BankFacilityID", typeof(string));
			columns.Add("TaxOption", typeof(byte));
			columns.Add("TaxGroupID", typeof(string));
			columns.Add("TaxAmount", typeof(decimal));
			columns.Add("DueDate", typeof(DateTime));
			columns.Add("PayeeID", typeof(string));
			columns.Add("PayeeType", typeof(string));
			columns.Add("AnalysisID", typeof(string));
			columns.Add("PaymentMethodID", typeof(string));
			columns.Add("PaymentMethodType", typeof(byte));
			columns.Add("CostCenterID", typeof(string));
			columns.Add("CheckNumber", typeof(string));
			columns.Add("ChequebookID", typeof(string));
			columns.Add("ChequeID", typeof(int));
			columns.Add("CheckDate", typeof(DateTime));
			columns.Add("RefDate", typeof(DateTime));
			columns.Add("AttributeID1", typeof(string));
			columns.Add("AttributeID2", typeof(string));
			base.Tables.Add(dataTable);
			dataTable = new DataTable("Bank_Fee_Details");
			columns = dataTable.Columns;
			columns.Add("GLTransactionSysDocID", typeof(string));
			columns.Add("GLTransactionVoucherID", typeof(string));
			columns.Add("RowIndex", typeof(int));
			columns.Add("BankFacilityID", typeof(string));
			columns.Add("ChequebookID", typeof(string));
			columns.Add("Description", typeof(string));
			columns.Add("Reference", typeof(string));
			columns.Add("BankAccountID", typeof(string));
			columns.Add("ExpenseAccountID", typeof(string));
			columns.Add("BankFeeID", typeof(string));
			columns.Add("Amount", typeof(decimal));
			columns.Add("AmountFC", typeof(decimal));
			columns.Add("CurrencyID", typeof(string));
			columns.Add("CurrencyRate", typeof(decimal));
			columns.Add("TaxOption", typeof(byte));
			columns.Add("TaxGroupID", typeof(string));
			columns.Add("TaxAmount", typeof(decimal));
			base.Tables.Add(dataTable);
			dataTable = new DataTable("General_Payment_Detail");
			columns = dataTable.Columns;
			columns.Add("SysDocID", typeof(string));
			columns.Add("VoucherID", typeof(string));
			columns.Add("SourceSysDocID", typeof(string));
			columns.Add("SourceVoucherID", typeof(string));
			columns.Add("Description", typeof(string));
			columns.Add("Amount", typeof(decimal));
			columns.Add("PayeeID", typeof(string));
			columns.Add("PayeeType", typeof(string));
			columns.Add("TransactionDate", typeof(DateTime));
			columns.Add("Reference", typeof(string));
			base.Tables.Add(dataTable);
			Merge(new TaxTransactionData().TaxDetailTable);
			new APJournalData().AddPaymentAdviceTable(this);
		}
	}
}
