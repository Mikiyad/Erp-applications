using System;
using System.ComponentModel;
using System.Data;
using System.Runtime.Serialization;

namespace Micromind.Common.Data
{
	[Serializable]
	[DesignerCategory("Code")]
	public class PurchasePrepaymentInvoiceData : DataSet
	{
		public const string PURCHASEPREPAYMENTINVOICE_TABLE = "Purchase_PrePayment_Invoice";

		public const string SYSDOCID_FIELD = "SysDocID";

		public const string VOUCHERID_FIELD = "VoucherID";

		public const string SOURCESYSDOCID_FIELD = "SourceSysDocID";

		public const string SOURCEVOUCHERID_FIELD = "SourceVoucherID";

		public const string VENDORID_FIELD = "VendorID";

		public const string TRANSACTIONDATE_FIELD = "TransactionDate";

		public const string POAMOUNT_FIELD = "POAmount";

		public const string PAID_FIELD = "Paid";

		public const string BALANCE_FIELD = "Balance";

		public const string ISVOID_FIELD = "IsVoid";

		public const string CURRENCYID_FIELD = "CurrencyID";

		public const string CURRENCYRATE_FIELD = "CurrencyRate";

		public const string TERMID_FIELD = "TermID";

		public const string SUGGESTEDUE_FIELD = "SuggestedDue";

		public const string AMOUNT_FIELD = "Amount";

		public const string AMOUNTFC_FIELD = "AmountFC";

		public const string DUEDATE_FIELD = "DueDate";

		public const string PREPAYMENTTERMID_FIELD = "PrepaymentTermID";

		public const string REMARKS_FIELD = "Remarks";

		public const string STATUS_FIELD = "Status";

		public const string INVOICESYSDOCID_FIELD = "InvoiceSysDocID";

		public const string INVOICEVOUCHERID_FIELD = "InvoiceVoucherID";

		public const string CREATEDBY_FIELD = "CreatedBy";

		public const string DATECREATED_FIELD = "DateCreated";

		public const string UPDATEDBY_FIELD = "UpdatedBy";

		public const string DATEUPDATED_FIELD = "DateUpdated";

		public DataTable PurchasePrepaymentInvoiceTable => base.Tables["Purchase_PrePayment_Invoice"];

		public PurchasePrepaymentInvoiceData()
		{
			BuildDataTables();
		}

		public PurchasePrepaymentInvoiceData(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}

		private void BuildDataTables()
		{
			DataTable dataTable = new DataTable("Purchase_PrePayment_Invoice");
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
			columns.Add("SourceSysDocID", typeof(string));
			columns.Add("SourceVoucherID", typeof(string));
			columns.Add("TransactionDate", typeof(DateTime)).DefaultValue = DateTime.Now;
			columns.Add("POAmount", typeof(decimal));
			columns.Add("Paid", typeof(decimal));
			columns.Add("Balance", typeof(decimal));
			columns.Add("IsVoid", typeof(bool));
			columns.Add("CurrencyID", typeof(string));
			columns.Add("CurrencyRate", typeof(decimal));
			columns.Add("TermID", typeof(string));
			columns.Add("SuggestedDue", typeof(decimal));
			columns.Add("Amount", typeof(decimal));
			columns.Add("AmountFC", typeof(decimal));
			columns.Add("InvoiceSysDocID", typeof(string));
			columns.Add("InvoiceVoucherID", typeof(string));
			columns.Add("Status", typeof(byte));
			columns.Add("DueDate", typeof(DateTime)).DefaultValue = DateTime.Now;
			columns.Add("PrepaymentTermID", typeof(string));
			columns.Add("Remarks", typeof(string));
			columns.Add("VendorID", typeof(string));
			base.Tables.Add(dataTable);
		}
	}
}
