using System;
using System.ComponentModel;
using System.Data;
using System.Runtime.Serialization;

namespace Micromind.Common.Data
{
	[Serializable]
	[DesignerCategory("Code")]
	public class JobInvoiceData : DataSet
	{
		public const string JOBINVOICE_TABLE = "Job_Invoice";

		public const string JOBINVOICEDETAIL_TABLE = "Job_Invoice_Detail";

		public const string JOBINVOICESODETAIL_TABLE = "Job_Invoice_SO_Detail";

		public const string INVOICEDNOTE_TABLE = "Invoice_DNote";

		public const string SYSDOCID_FIELD = "SysDocID";

		public const string VOUCHERID_FIELD = "VoucherID";

		public const string DIVISIONID_FIELD = "DivisionID";

		public const string COMPANYID_FIELD = "CompanyID";

		public const string CUSTOMERID_FIELD = "CustomerID";

		public const string TRANSACTIONDATE_FIELD = "TransactionDate";

		public const string ROWINDEX_FIELD = "RowIndex";

		public const string SALESPERSONID_FIELD = "SalespersonID";

		public const string CUSTOMERADDRESS_FIELD = "CustomerAddress";

		public const string STATUS_FIELD = "Status";

		public const string CURRENCYID_FIELD = "CurrencyID";

		public const string CURRENCYRATE_FIELD = "CurrencyRate";

		public const string TERMID_FIELD = "TermID";

		public const string REFERENCE_FIELD = "Reference";

		public const string NOTE_FIELD = "Note";

		public const string PONUMBER_FIELD = "PONumber";

		public const string ISVOID_FIELD = "IsVoid";

		public const string DISCOUNT_FIELD = "Discount";

		public const string DISCOUNTFC_FIELD = "DiscountFC";

		public const string RETENTIONAMOUNT_FIELD = "RetentionAmount";

		public const string RETENTIONAMOUNTFC_FIELD = "RetentionAmountFC";

		public const string RETENTIONPERCENT_FIELD = "RetentionPercent";

		public const string ADVANCEAMOUNT_FIELD = "AdvanceAmount";

		public const string ADVANCEAMOUNTFC_FIELD = "AdvanceAmountFC";

		public const string TAXAMOUNT_FIELD = "TaxAmount";

		public const string TAXAMOUNTFC_FIELD = "TaxAmountFC";

		public const string SUBTOTAL_FIELD = "SubTotal";

		public const string TOTAL_FIELD = "Total";

		public const string TOTALFC_FIELD = "TotalFC";

		public const string DUEDATE_FIELD = "DueDate";

		public const string PAYEETAXGROUPID_FIELD = "PayeeTaxGroupID";

		public const string TAXOPTION_FIELD = "TaxOption";

		public const string CREATEDBY_FIELD = "CreatedBy";

		public const string DATECREATED_FIELD = "DateCreated";

		public const string UPDATEDBY_FIELD = "UpdatedBy";

		public const string DATEUPDATED_FIELD = "DateUpdated";

		public const string FEEID_FIELD = "FeeID";

		public const string ITEMTYPE_FIELD = "ItemType";

		public const string DESCRIPTION_FIELD = "Description";

		public const string ROWSOURCE_FIELD = "RowSource";

		public const string SOURCESYSDOCID_FIELD = "SourceSysDocID";

		public const string SOURCEVOUCHERID_FIELD = "SourceVoucherID";

		public const string SOURCEROWINDEX_FIELD = "SourceRowIndex";

		public const string COST_FIELD = "Cost";

		public const string COSTFC_FIELD = "CostFC";

		public const string QUANTITY_FIELD = "Quantity";

		public const string LOCATIONID_FIELD = "LocationID";

		public const string UNITID_FIELD = "UnitID";

		public const string UNITQUANTITY_FIELD = "UnitQuantity";

		public const string UNITFACTOR_FIELD = "UnitFactor";

		public const string FACTORTYPE_FIELD = "FactorType";

		public const string UNITPRICE_FIELD = "UnitPrice";

		public const string JOBID_FIELD = "JobID";

		public const string COSTCATEGORYID_FIELD = "CostCategoryID";

		public const string SOURCEDOCTYPE_FIELD = "SourceDocType";

		public const string TAXGROUPID_FIELD = "TaxGroupID";

		public const string REMARKS_FIELD = "Remarks";

		public const string SUBUNITPRICE_FIELD = "SubunitPrice";

		public const string ACCOUNTID_FIELD = "AccountID";

		public const string AMOUNT_FIELD = "Amount";

		public const string AMOUNTFC_FIELD = "AmountFC";

		public DataTable JobInvoiceTable => base.Tables["Job_Invoice"];

		public DataTable JobInvoiceDetailTable => base.Tables["Job_Invoice_Detail"];

		public DataTable TaxDetailsTable => base.Tables["Tax_Detail"];

		public DataTable JobInvoiceSODetailTable => base.Tables["Job_Invoice_SO_Detail"];

		public JobInvoiceData()
		{
			BuildDataTables();
		}

		public JobInvoiceData(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}

		private void BuildDataTables()
		{
			DataTable dataTable = new DataTable("Job_Invoice");
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
			columns.Add("CustomerID", typeof(string));
			columns.Add("TransactionDate", typeof(DateTime)).DefaultValue = DateTime.Now;
			columns.Add("DueDate", typeof(DateTime)).DefaultValue = DateTime.Now;
			columns.Add("SalespersonID", typeof(string));
			columns.Add("JobID", typeof(string));
			columns.Add("CustomerAddress", typeof(string));
			columns.Add("Status", typeof(byte));
			columns.Add("CurrencyID", typeof(string));
			columns.Add("CurrencyRate", typeof(decimal));
			columns.Add("TermID", typeof(string));
			columns.Add("Reference", typeof(string));
			columns.Add("Note", typeof(string));
			columns.Add("PONumber", typeof(string));
			columns.Add("IsVoid", typeof(bool));
			columns.Add("Discount", typeof(decimal));
			columns.Add("DiscountFC", typeof(decimal));
			columns.Add("RetentionAmount", typeof(decimal));
			columns.Add("RetentionAmountFC", typeof(decimal));
			columns.Add("RetentionPercent", typeof(decimal));
			columns.Add("AdvanceAmount", typeof(decimal));
			columns.Add("AdvanceAmountFC", typeof(decimal));
			columns.Add("TaxAmount", typeof(decimal));
			columns.Add("TaxAmountFC", typeof(decimal));
			columns.Add("SubTotal", typeof(decimal));
			columns.Add("Total", typeof(decimal));
			columns.Add("TotalFC", typeof(decimal));
			columns.Add("PayeeTaxGroupID", typeof(string));
			columns.Add("TaxOption", typeof(byte));
			base.Tables.Add(dataTable);
			dataTable = new DataTable("Job_Invoice_Detail");
			columns = dataTable.Columns;
			columns.Add("SysDocID", typeof(string));
			columns.Add("VoucherID", typeof(string));
			columns.Add("FeeID", typeof(string));
			columns.Add("SourceSysDocID", typeof(string));
			columns.Add("SourceVoucherID", typeof(string));
			columns.Add("SourceRowIndex", typeof(int));
			columns.Add("Cost", typeof(decimal)).DefaultValue = 0;
			columns.Add("CostFC", typeof(decimal)).DefaultValue = 0;
			columns.Add("Amount", typeof(decimal)).DefaultValue = 0;
			columns.Add("AmountFC", typeof(decimal)).DefaultValue = 0;
			columns.Add("Description", typeof(string));
			columns.Add("RowIndex", typeof(short));
			columns.Add("ItemType", typeof(byte));
			columns.Add("RowSource", typeof(short));
			columns.Add("Quantity", typeof(float)).DefaultValue = 0;
			columns.Add("TaxOption", typeof(byte));
			columns.Add("TaxGroupID", typeof(string));
			columns.Add("TaxAmount", typeof(decimal));
			base.Tables.Add(dataTable);
			dataTable = new DataTable("Job_Invoice_SO_Detail");
			columns = dataTable.Columns;
			columns.Add("SysDocID", typeof(string));
			columns.Add("VoucherID", typeof(string));
			columns.Add("FeeID", typeof(string));
			columns.Add("Quantity", typeof(float)).DefaultValue = 0;
			columns.Add("UnitPrice", typeof(decimal)).DefaultValue = 0;
			columns.Add("Description", typeof(string));
			columns.Add("Remarks", typeof(string));
			columns.Add("LocationID", typeof(string));
			columns.Add("UnitID", typeof(string));
			columns.Add("UnitQuantity", typeof(float));
			columns.Add("UnitFactor", typeof(decimal));
			columns.Add("FactorType", typeof(string));
			columns.Add("SubunitPrice", typeof(decimal));
			columns.Add("ItemType", typeof(byte));
			columns.Add("RowIndex", typeof(short));
			columns.Add("JobID", typeof(string));
			columns.Add("SourceVoucherID", typeof(string));
			columns.Add("SourceSysDocID", typeof(string));
			columns.Add("SourceRowIndex", typeof(int));
			columns.Add("SourceDocType", typeof(byte));
			columns.Add("TaxOption", typeof(byte));
			columns.Add("TaxGroupID", typeof(string));
			columns.Add("TaxAmount", typeof(decimal));
			columns.Add("Cost", typeof(decimal));
			columns.Add("Discount", typeof(decimal));
			columns.Add("CostCategoryID", typeof(string));
			base.Tables.Add(dataTable);
			Merge(new TaxTransactionData().TaxDetailTable);
		}
	}
}
