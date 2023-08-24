using System;
using System.ComponentModel;
using System.Data;
using System.Runtime.Serialization;

namespace Micromind.Common.Data
{
	[Serializable]
	[DesignerCategory("Code")]
	public class SalesProformaInvoiceData : DataSet
	{
		public const string SALESPROFORMA_TABLE = "SalesProforma_Invoice";

		public const string SALESPROFORMADETAIL_TABLE = "SalesProforma_Invoice_Detail";

		public const string SYSDOCID_FIELD = "SysDocID";

		public const string VOUCHERID_FIELD = "VoucherID";

		public const string DIVISIONID_FIELD = "DivisionID";

		public const string COMPANYID_FIELD = "CompanyID";

		public const string CUSTOMERID_FIELD = "CustomerID";

		public const string SALESFLOW_FIELD = "SalesFlow";

		public const string ISEXPORT_FIELD = "IsExport";

		public const string TRANSACTIONDATE_FIELD = "TransactionDate";

		public const string ROWINDEX_FIELD = "RowIndex";

		public const string SALESPERSONID_FIELD = "SalespersonID";

		public const string REQUIREDDATE_FIELD = "RequiredDate";

		public const string SHIPPINGADDRESSID_FIELD = "ShippingAddressID";

		public const string BILLINGADDRESSID_FIELD = "BillingAddressID";

		public const string CUSTOMERADDRESS_FIELD = "CustomerAddress";

		public const string SHIPTOADDRESS_FIELD = "ShipToAddress";

		public const string STATUS_FIELD = "Status";

		public const string TAXOPTION_FIELD = "TaxOption";

		public const string PRICEINCLUDETAX_FIELD = "PriceIncludeTax";

		public const string PAYEETAXGROUPID_FIELD = "PayeeTaxGroupID";

		public const string CURRENCYID_FIELD = "CurrencyID";

		public const string TERMID_FIELD = "TermID";

		public const string SHIPPINGMETHODID_FIELD = "ShippingMethodID";

		public const string REFERENCE_FIELD = "Reference";

		public const string NOTE_FIELD = "Note";

		public const string DUEDATE_FIELD = "DueDate";

		public const string PONUMBER_FIELD = "PONumber";

		public const string ISVOID_FIELD = "IsVoid";

		public const string DISCOUNT_FIELD = "Discount";

		public const string TAXAMOUNT_FIELD = "TaxAmount";

		public const string TOTAL_FIELD = "Total";

		public const string JOBID_FIELD = "JobID";

		public const string COSTCATEGORYID_FIELD = "CostCategoryID";

		public const string SHIPPINGREFERENCE_FIELD = "ShippingReference";

		public const string SOURCEPORTID_FIELD = "SourcePortID";

		public const string DESTINATIONPORTID_FIELD = "DestinationPortID";

		public const string ETD_FIELD = "ETD";

		public const string ETA_FIELD = "ETA";

		public const string WEIGHT_FIELD = "Weight";

		public const string TRANSPORTERID_FIELD = "TransporterID";

		public const string CLEARINGAGENT_FIELD = "ClearingAgent";

		public const string CREATEDBY_FIELD = "CreatedBy";

		public const string DATECREATED_FIELD = "DateCreated";

		public const string UPDATEDBY_FIELD = "UpdatedBy";

		public const string DATEUPDATED_FIELD = "DateUpdated";

		public const string PRODUCTID_FIELD = "ProductID";

		public const string QUANTITY_FIELD = "Quantity";

		public const string QUANTITYSHIPPED_FIELD = "QuantityShipped";

		public const string UNITPRICE_FIELD = "UnitPrice";

		public const string DESCRIPTION_FIELD = "Description";

		public const string REMARKS_FIELD = "Remarks";

		public const string UNITID_FIELD = "UnitID";

		public const string UNITQUANTITY_FIELD = "UnitQuantity";

		public const string UNITFACTOR_FIELD = "UnitFactor";

		public const string FACTORTYPE_FIELD = "FactorType";

		public const string SUBUNITPRICE_FIELD = "SubunitPrice";

		public const string ITEMTYPE_FIELD = "ItemType";

		public const string TAXPERCENTAGE_FIELD = "TaxPercentage";

		public const string TAXGROUPID_FIELD = "TaxGroupID";

		public const string SPECIFICATIONID_FIELD = "SpecificationID";

		public const string STYLEID_FIELD = "StyleID";

		public DataTable SalesProformaTable => base.Tables["SalesProforma_Invoice"];

		public DataTable SalesProformaDetailTable => base.Tables["SalesProforma_Invoice_Detail"];

		public DataTable TaxDetailsTable => base.Tables["Tax_Detail"];

		public SalesProformaInvoiceData()
		{
			BuildDataTables();
		}

		public SalesProformaInvoiceData(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}

		private void BuildDataTables()
		{
			DataTable dataTable = new DataTable("SalesProforma_Invoice");
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
			columns.Add("SalespersonID", typeof(string));
			columns.Add("SalesFlow", typeof(byte));
			columns.Add("IsExport", typeof(bool));
			columns.Add("RequiredDate", typeof(DateTime));
			columns.Add("DueDate", typeof(DateTime));
			columns.Add("ShippingAddressID", typeof(string));
			columns.Add("BillingAddressID", typeof(string));
			columns.Add("ShipToAddress", typeof(string));
			columns.Add("CustomerAddress", typeof(string));
			columns.Add("Status", typeof(byte)).DefaultValue = 1;
			columns.Add("PayeeTaxGroupID", typeof(string));
			columns.Add("CurrencyID", typeof(string));
			columns.Add("PriceIncludeTax", typeof(bool));
			columns.Add("TermID", typeof(string));
			columns.Add("ShippingMethodID", typeof(string));
			columns.Add("Reference", typeof(string));
			columns.Add("Note", typeof(string));
			columns.Add("PONumber", typeof(string));
			columns.Add("IsVoid", typeof(bool));
			columns.Add("Discount", typeof(decimal));
			columns.Add("Total", typeof(decimal));
			columns.Add("TaxAmount", typeof(decimal));
			columns.Add("TaxOption", typeof(byte));
			columns.Add("JobID", typeof(string));
			columns.Add("CostCategoryID", typeof(string));
			columns.Add("ShippingReference", typeof(string));
			columns.Add("SourcePortID", typeof(string));
			columns.Add("DestinationPortID", typeof(string));
			columns.Add("ETD", typeof(DateTime));
			columns.Add("ETA", typeof(DateTime));
			columns.Add("Weight", typeof(decimal));
			columns.Add("TransporterID", typeof(string));
			columns.Add("ClearingAgent", typeof(string));
			base.Tables.Add(dataTable);
			dataTable = new DataTable("SalesProforma_Invoice_Detail");
			columns = dataTable.Columns;
			columns.Add("SysDocID", typeof(string));
			columns.Add("VoucherID", typeof(string));
			columns.Add("ProductID", typeof(string));
			columns.Add("Quantity", typeof(float)).DefaultValue = 0;
			columns.Add("UnitPrice", typeof(decimal)).DefaultValue = 0;
			columns.Add("Description", typeof(string));
			columns.Add("Remarks", typeof(string));
			columns.Add("UnitID", typeof(string));
			columns.Add("UnitQuantity", typeof(float));
			columns.Add("UnitFactor", typeof(decimal));
			columns.Add("FactorType", typeof(string));
			columns.Add("SubunitPrice", typeof(decimal));
			columns.Add("ItemType", typeof(byte));
			columns.Add("RowIndex", typeof(short));
			columns.Add("JobID", typeof(string));
			columns.Add("CostCategoryID", typeof(string));
			columns.Add("TaxOption", typeof(byte));
			columns.Add("TaxGroupID", typeof(string));
			columns.Add("TaxPercentage", typeof(decimal));
			columns.Add("TaxAmount", typeof(decimal));
			columns.Add("SpecificationID", typeof(string));
			columns.Add("StyleID", typeof(string));
			base.Tables.Add(dataTable);
			Merge(new TaxTransactionData().TaxDetailTable);
		}
	}
}
