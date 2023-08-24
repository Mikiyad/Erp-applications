using System;
using System.ComponentModel;
using System.Data;
using System.Runtime.Serialization;

namespace Micromind.Common.Data
{
	[Serializable]
	[DesignerCategory("Code")]
	public class PurchaseCostEntryData : DataSet
	{
		public const string PURCHASECOSTENTRY_TABLE = "Purchase_Cost_Entry";

		public const string PURCHASECOSTENTRYDETAIL_TABLE = "Purchase_Cost_Entry_Detail";

		public const string SYSDOCID_FIELD = "SysDocID";

		public const string VOUCHERID_FIELD = "VoucherID";

		public const string DIVISIONID_FIELD = "DivisionID";

		public const string COMPANYID_FIELD = "CompanyID";

		public const string VENDORID_FIELD = "VendorID";

		public const string PURCHASEFLOW_FIELD = "PurchaseFlow";

		public const string TRANSACTIONDATE_FIELD = "TransactionDate";

		public const string STATUS_FIELD = "Status";

		public const string SHIPPINGMETHODID_FIELD = "ShippingMethodID";

		public const string REFERENCE_FIELD = "Reference";

		public const string NOTE_FIELD = "Note";

		public const string PONUMBER_FIELD = "PONumber";

		public const string ISVOID_FIELD = "IsVoid";

		public const string CONTAINERNUMBER_FIELD = "ContainerNumber";

		public const string CONTAINERSIZEID_FIELD = "ContainerSizeID";

		public const string ATD_FIELD = "ATD";

		public const string PORT_FIELD = "Port";

		public const string LOADINGPORT_FIELD = "LoadingPort";

		public const string ETA_FIELD = "ETA";

		public const string BOLNUMBER_FIELD = "BOLNumber";

		public const string SHIPPER_FIELD = "Shipper";

		public const string CLEARINGAGENT_FIELD = "ClearingAgent";

		public const string WEIGHT_FIELD = "Weight";

		public const string ISRECEIVED_FIELD = "IsReceived";

		public const string VALUE_FIELD = "Value";

		public const string DISCOUNT_FIELD = "Discount";

		public const string DISCOUNTFC_FIELD = "DiscountFC";

		public const string TAXAMOUNT_FIELD = "TaxAmount";

		public const string TAXAMOUNTFC_FIELD = "TaxAmountFC";

		public const string TOTAL_FIELD = "Total";

		public const string TOTALFC_FIELD = "TotalFC";

		public const string TRANSPORTERID_FIELD = "TransporterID";

		public const string PAYEETAXGROUPID_FIELD = "PayeeTaxGroupID";

		public const string TAXGROUPID_FIELD = "TaxGroupID";

		public const string TAXOPTION_FIELD = "TaxOption";

		public const string SOURCESYSDOCID_FIELD = "SourceSysDocID";

		public const string SOURCEVOUCHERID_FIELD = "SourceVoucherID";

		public const string CREATEDBY_FIELD = "CreatedBy";

		public const string DATECREATED_FIELD = "DateCreated";

		public const string UPDATEDBY_FIELD = "UpdatedBy";

		public const string DATEUPDATED_FIELD = "DateUpdated";

		public const string EXPENSEID_FIELD = "ExpenseID";

		public const string SUPPLIERID_FIELD = "SupplierID";

		public const string DUEDATE_FIELD = "DueDate";

		public const string RATETYPE_FIELD = "RateType";

		public const string DESCRIPTION_FIELD = "Description";

		public const string AMOUNT_FIELD = "Amount";

		public const string AMOUNTFC_FIELD = "AmountFC";

		public const string CURRENCYID_FIELD = "CurrencyID";

		public const string CURRENCYRATE_FIELD = "CurrencyRate";

		public const string COST_FIELD = "Cost";

		public const string QUANTITY_FIELD = "Quantity";

		public const string ROWINDEX_FIELD = "RowIndex";

		public const string REMARKS_FIELD = "Remarks";

		public const string ALLOCATEDCOST_FIELD = "AllocatedCost";

		public DataTable PurchaseCostEntryTable => base.Tables["Purchase_Cost_Entry"];

		public DataTable PurchaseCostEntryDetailTable => base.Tables["Purchase_Cost_Entry_Detail"];

		public DataTable TaxDetailsTable => base.Tables["Tax_Detail"];

		public PurchaseCostEntryData()
		{
			BuildDataTables();
		}

		public PurchaseCostEntryData(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}

		private void BuildDataTables()
		{
			DataTable dataTable = new DataTable("Purchase_Cost_Entry");
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
			columns.Add("VendorID", typeof(string));
			columns.Add("TransactionDate", typeof(DateTime)).DefaultValue = DateTime.Now;
			columns.Add("Status", typeof(byte)).DefaultValue = 1;
			columns.Add("PurchaseFlow", typeof(byte));
			columns.Add("ShippingMethodID", typeof(string));
			columns.Add("Reference", typeof(string));
			columns.Add("Note", typeof(string));
			columns.Add("PONumber", typeof(string));
			columns.Add("CurrencyID", typeof(string));
			columns.Add("CurrencyRate", typeof(decimal));
			columns.Add("IsVoid", typeof(bool));
			columns.Add("ContainerNumber", typeof(string));
			columns.Add("Port", typeof(string));
			columns.Add("LoadingPort", typeof(string));
			columns.Add("ETA", typeof(DateTime));
			columns.Add("ATD", typeof(DateTime));
			columns.Add("BOLNumber", typeof(string));
			columns.Add("Shipper", typeof(string));
			columns.Add("ClearingAgent", typeof(string));
			columns.Add("Weight", typeof(decimal));
			columns.Add("Value", typeof(decimal));
			columns.Add("IsReceived", typeof(bool));
			columns.Add("TransporterID", typeof(string));
			columns.Add("ContainerSizeID", typeof(string));
			columns.Add("SourceSysDocID", typeof(string));
			columns.Add("SourceVoucherID", typeof(string));
			columns.Add("Discount", typeof(decimal));
			columns.Add("DiscountFC", typeof(decimal));
			columns.Add("TaxAmount", typeof(decimal));
			columns.Add("TaxAmountFC", typeof(decimal));
			columns.Add("Total", typeof(decimal));
			columns.Add("TotalFC", typeof(decimal));
			columns.Add("TaxGroupID", typeof(string));
			columns.Add("TaxOption", typeof(byte));
			base.Tables.Add(dataTable);
			dataTable = new DataTable("Purchase_Cost_Entry_Detail");
			columns = dataTable.Columns;
			dataColumn = columns.Add("SysDocID", typeof(string));
			dataColumn2 = columns.Add("VoucherID", typeof(string));
			columns.Add("BOLNumber", typeof(string));
			columns.Add("ExpenseID", typeof(string));
			columns.Add("SupplierID", typeof(string));
			columns.Add("DueDate", typeof(DateTime));
			columns.Add("Description", typeof(string));
			columns.Add("Amount", typeof(decimal)).DefaultValue = 0;
			columns.Add("AmountFC", typeof(decimal)).DefaultValue = 0;
			columns.Add("CurrencyID", typeof(string));
			columns.Add("Cost", typeof(decimal)).DefaultValue = 0;
			columns.Add("Quantity", typeof(float)).DefaultValue = 0;
			columns.Add("CurrencyRate", typeof(decimal));
			columns.Add("SourceSysDocID", typeof(string));
			columns.Add("SourceVoucherID", typeof(string));
			columns.Add("RateType", typeof(string));
			columns.Add("RowIndex", typeof(int));
			columns.Add("AllocatedCost", typeof(double));
			columns.Add("TaxOption", typeof(byte));
			columns.Add("TaxGroupID", typeof(string));
			columns.Add("TaxAmount", typeof(decimal));
			columns.Add("Remarks", typeof(string));
			base.Tables.Add(dataTable);
			Merge(new TaxTransactionData().TaxDetailTable);
		}
	}
}
