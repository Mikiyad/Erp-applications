using System;
using System.ComponentModel;
using System.Data;
using System.Runtime.Serialization;

namespace Micromind.Common.Data
{
	[Serializable]
	[DesignerCategory("Code")]
	public class ARJournalData : DataSet
	{
		public const string ARJOURNAL_TABLE = "ARJournal";

		public const string ARID_FIELD = "ARID";

		public const string SYSDOCID_FIELD = "SysDocID";

		public const string VOUCHERID_FIELD = "VoucherID";

		public const string CUSTOMERID_FIELD = "CustomerID";

		public const string REFERENCE_FIELD = "Reference";

		public const string ARACCOUNTID_FIELD = "ARAccountID";

		public const string DEBIT_FIELD = "Debit";

		public const string CREDIT_FIELD = "Credit";

		public const string DEBITFC_FIELD = "DebitFC";

		public const string CREDITFC_FIELD = "CreditFC";

		public const string CONDEBITFC_FIELD = "ConDebitFC";

		public const string CONCREDITFC_FIELD = "ConCreditFC";

		public const string CONRATE_FIELD = "ConRate";

		public const string COSTCENTERID_FIELD = "CostCenterID";

		public const string ISPDCROW_FIELD = "IsPDCRow";

		public const string CURRENCYID_FIELD = "CurrencyID";

		public const string CURRENCYRATE_FIELD = "CurrencyRate";

		public const string DESCRIPTION_FIELD = "Description";

		public const string JOURNALID_FIELD = "JournalID";

		public const string ARDATE_FIELD = "ARDate";

		public const string ARDUEDATE_FIELD = "ARDueDate";

		public const string CHEQUENUMBER_FIELD = "ChequeNumber";

		public const string BANKID_FIELD = "BankID";

		public const string CHEQUEDATE_FIELD = "ChequeDate";

		public const string PAYMENTMETHODID_FIELD = "PaymentMethodID";

		public const string PAYMENTMETHODTYPE_FIELD = "PaymentMethodType";

		public const string ALLOCATIONID_FIELD = "AllocationID";

		public const string JOBID_FIELD = "JobID";

		public const string COSTCATEGORYID_FIELD = "CostCategoryID";

		public const string ATTRIBUTEID1_FIELD = "AttributeID1";

		public const string ATTRIBUTEID2_FIELD = "AttributeID2";

		public const string ARPAYMENTALLOCATION_TABLE = "AR_Payment_Allocation";

		public const string INVOICESYSDOCID_FIELD = "InvoiceSysDocID";

		public const string INVOICEVOUCHERID_FIELD = "InvoiceVoucherID";

		public const string ARJOURNALID_FIELD = "ARJournalID";

		public const string PAYMENTARID_FIELD = "PaymentARID";

		public const string PAYMENTSYSDOCID_FIELD = "PaymentSysDocID";

		public const string PAYMENTVOUCHERID_FIELD = "PaymentVoucherID";

		public const string ALLOCATIONDATE_FIELD = "AllocationDate";

		public const string PAYMENTAMOUNT_FIELD = "PaymentAmount";

		public const string PAYMENTAMOUNTFC_FIELD = "PaymentAmountFC";

		public const string DISCOUNTAMOUNT_FIELD = "DiscountAmount";

		public const string DISCOUNTAMOUNTFC_FIELD = "DiscountAmountFC";

		public const string REALIZEDGAINLOSS_FIELD = "RealizedGainLoss";

		public const string ISAR_FIELD = "IsAR";

		public const string UNALLOCATEDAMOUNT_FIELD = "UnAllocatedAmount";

		public const string PAYMENTALLOCATIONBATCH_TABLE = "Payment_Allocation_Batch";

		public const string BATCHID_FIELD = "BatchID";

		public const string BATCHDATE_FIELD = "BatchDate";

		public const string PARTYTYPE_FIELD = "PartyType";

		public const string PARTYID_FIELD = "PartyID";

		public const string ACCOUNTRECEIVABLEID_FIELD = "ARID";

		public const string INVOICEID_FIELD = "InvoiceID";

		public const string TRANSACTIONID_FIELD = "TransactionID";

		public const string ARTYPE_FIELD = "ARType";

		public const string ORIGINALAMOUNT_FIELD = "OriginalAmount";

		public const string AMOUNTDUE_FIELD = "AmountDue";

		public const string DEPOSITACCOUNTID_FIELD = "DepositAccountID";

		public const string ACCOUNTRECEIVABLE_TABLE = "[AR Transactions]";

		public const string ARTYPEDESCRIPTION_FIELD = "Description";

		public const string PAIDINVOICEAMOUNT_FIELD = "Amount";

		public const string ARDEBIT_FIELD = "ARDebit";

		public const string ARCREDIT_FIELD = "ARCredit";

		public const string DISCOUNT_FIELD = "Discount";

		public const string DISCOUNTACCOUNT_FIELD = "DiscountAccount";

		public const string ARPAYMENTDETAILS_TABLE = "[AR Transaction Details]";

		public const string UNPAIDINVOICES_TABLE = "Unpaid Invoices";

		public const string UNUSEDCREDITS_TABLE = "Unused Credits";

		public const string DATETIMESTAMP_FIELD = "DateTimeStamp";

		public DataTable ARPaymentDetailTable => base.Tables["[AR Transaction Details]"];

		public DataTable AccountReceivableTable => base.Tables["[AR Transactions]"];

		public DataTable PaymentAllocationTable => base.Tables["AR_Payment_Allocation"];

		public DataTable PaymentAllocationBatchTable => base.Tables["Payment_Allocation_Batch"];

		public ARJournalData()
		{
			BuildDataTables();
		}

		public ARJournalData(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}

		private void BuildDataTables()
		{
			DataTable dataTable = new DataTable("ARJournal");
			DataColumnCollection columns = dataTable.Columns;
			DataColumn dataColumn = columns.Add("ARID", typeof(int));
			dataColumn.AllowDBNull = false;
			dataColumn.AutoIncrement = true;
			dataTable.PrimaryKey = new DataColumn[1]
			{
				dataColumn
			};
			columns.Add("SysDocID", typeof(string));
			columns.Add("VoucherID", typeof(string));
			columns.Add("CustomerID", typeof(string));
			columns.Add("Reference", typeof(string));
			columns.Add("IsPDCRow", typeof(bool));
			columns.Add("ARAccountID", typeof(string));
			columns.Add("Debit", typeof(decimal));
			columns.Add("Credit", typeof(decimal));
			columns.Add("DebitFC", typeof(decimal));
			columns.Add("CreditFC", typeof(decimal));
			columns.Add("ConDebitFC", typeof(decimal));
			columns.Add("ConCreditFC", typeof(decimal));
			columns.Add("ConRate", typeof(decimal));
			columns.Add("CostCenterID", typeof(string));
			columns.Add("CurrencyID", typeof(string));
			columns.Add("CurrencyRate", typeof(decimal));
			columns.Add("Description", typeof(string));
			columns.Add("JournalID", typeof(int));
			columns.Add("ARDate", typeof(DateTime));
			columns.Add("ARDueDate", typeof(DateTime));
			columns.Add("ChequeNumber", typeof(string));
			columns.Add("BankID", typeof(string));
			columns.Add("ChequeDate", typeof(DateTime));
			columns.Add("PaymentMethodType", typeof(byte));
			columns.Add("AllocationID", typeof(int));
			columns.Add("JobID", typeof(string));
			columns.Add("CostCategoryID", typeof(string));
			columns.Add("AttributeID1", typeof(string));
			columns.Add("AttributeID2", typeof(string));
			base.Tables.Add(dataTable);
			dataTable = new DataTable("[AR Transaction Details]");
			columns = dataTable.Columns;
			dataColumn = columns.Add("ARDebit", typeof(int));
			dataColumn.AllowDBNull = true;
			dataColumn.AutoIncrement = false;
			columns.Add("ARCredit", typeof(int)).AllowDBNull = true;
			columns.Add("Amount", typeof(decimal)).DefaultValue = 0;
			columns.Add("Discount", typeof(decimal)).DefaultValue = 0;
			columns.Add("DiscountAccount", typeof(int));
			dataTable = new DataTable("AR_Payment_Allocation");
			columns = dataTable.Columns;
			columns.Add("InvoiceSysDocID", typeof(string));
			columns.Add("InvoiceVoucherID", typeof(string));
			columns.Add("ARJournalID", typeof(int));
			columns.Add("PaymentARID", typeof(int));
			columns.Add("BatchID", typeof(int));
			columns.Add("PaymentSysDocID", typeof(string));
			columns.Add("PaymentVoucherID", typeof(string));
			columns.Add("AllocationDate", typeof(DateTime));
			columns.Add("PaymentAmount", typeof(decimal));
			columns.Add("PaymentAmountFC", typeof(decimal));
			columns.Add("DiscountAmount", typeof(decimal));
			columns.Add("DiscountAmountFC", typeof(decimal));
			columns.Add("RealizedGainLoss", typeof(decimal));
			columns.Add("CurrencyID", typeof(string));
			columns.Add("CustomerID", typeof(string));
			columns.Add("CurrencyRate", typeof(decimal));
			columns.Add("UnAllocatedAmount", typeof(decimal));
			base.Tables.Add(dataTable);
			dataTable = new DataTable("Payment_Allocation_Batch");
			columns = dataTable.Columns;
			dataColumn = columns.Add("BatchID", typeof(int));
			dataColumn.AllowDBNull = true;
			dataColumn.AutoIncrement = false;
			columns.Add("BatchDate", typeof(DateTime));
			columns.Add("PartyType", typeof(char));
			columns.Add("PartyID", typeof(string));
			base.Tables.Add(dataTable);
		}
	}
}
