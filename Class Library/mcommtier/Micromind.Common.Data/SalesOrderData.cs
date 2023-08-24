using System;
using System.ComponentModel;
using System.Data;
using System.Runtime.Serialization;

namespace Micromind.Common.Data
{
	[Serializable]
	[DesignerCategory("Code")]
	public class SalesOrderData : DataSet
	{
		public const string SALESORDER_TABLE = "Sales_Order";

		public const string SALESORDERDETAIL_TABLE = "Sales_Order_Detail";

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

		public const string TAXOPTION_FIELD = "TaxOption";

		public const string TOTAL_FIELD = "Total";

		public const string PRICEINCLUDETAX_FIELD = "PriceIncludeTax";

		public const string ALLOWSOEDIT_FIELD = "AllowSOEdit";

		public const string ROUNDOFF_FIELD = "RoundOff";

		public const string JOBID_FIELD = "JobID";

		public const string COSTCATEGORYID_FIELD = "CostCategoryID";

		public const string SOURCEDOCTYPE_FIELD = "SourceDocType";

		public const string PAYEETAXGROUPID_FIELD = "PayeeTaxGroupID";

		public const string SPECIFICATIONID_FIELD = "SpecificationID";

		public const string STYLEID_FIELD = "StyleID";

		public const string CREATEDBY_FIELD = "CreatedBy";

		public const string DATECREATED_FIELD = "DateCreated";

		public const string UPDATEDBY_FIELD = "UpdatedBy";

		public const string DATEUPDATED_FIELD = "DateUpdated";

		public const string PRODUCTID_FIELD = "ProductID";

		public const string QUANTITY_FIELD = "Quantity";

		public const string QUANTITYSHIPPED_FIELD = "QuantityShipped";

		public const string UNITPRICE_FIELD = "UnitPrice";

		public const string UNITPRICEFC_FIELD = "UnitPriceFC";

		public const string DESCRIPTION_FIELD = "Description";

		public const string REMARKS_FIELD = "Remarks";

		public const string LOCATIONID_FIELD = "LocationID";

		public const string UNITID_FIELD = "UnitID";

		public const string UNITQUANTITY_FIELD = "UnitQuantity";

		public const string UNITFACTOR_FIELD = "UnitFactor";

		public const string FACTORTYPE_FIELD = "FactorType";

		public const string SUBUNITPRICE_FIELD = "SubunitPrice";

		public const string ITEMTYPE_FIELD = "ItemType";

		public const string TAXPERCENTAGE_FIELD = "TaxPercentage";

		public const string TAXGROUPID_FIELD = "TaxGroupID";

		public const string ISNONEDIT_FIELD = "IsNonEdit";

		public const string SOURCEVOUCHERID_FIELD = "SourceVoucherID";

		public const string SOURCESYSDOCID_FIELD = "SourceSysDocID";

		public const string SOURCEROWINDEX_FIELD = "SourceRowIndex";

		public const string REFSLNO_FIELD = "RefSlNo";

		public const string REFTEXT1_FIELD = "RefText1";

		public const string REFTEXT2_FIELD = "RefText2";

		public const string REFNUM1_FIELD = "RefNum1";

		public const string REFNUM2_FIELD = "RefNum2";

		public const string REFDATE1_FIELD = "RefDate1";

		public const string REFDATE2_FIELD = "RefDate2";

		public const string SALESORDERPRODUCTLOTDETAIL_TABLE = "Sales_Order_ProductLot_Detail";

		public const string COST_FIELD = "Cost";

		public const string LOTNUMBER_FIELD = "LotNumber";

		public const string SOURCELOTNUMBER_FIELD = "SourceLotNumber";

		public const string BINID_FIELD = "BinID";

		public const string RACKID_FIELD = "RackID";

		public const string LOTQTY_FIELD = "LotQty";

		public const string SOLDQTY_FIELD = "SoldQty";

		public const string PRODUCTIONDATE_FIELD = "ProductionDate";

		public const string EXPIRYDATE_FIELD = "ExpiryDate";

		public const string RECEIPTDATE_FIELD = "ReceiptDate";

		public const string REFERENCE2_FIELD = "Reference2";

		public DataTable SalesOrderTable => base.Tables["Sales_Order"];

		public DataTable SalesOrderDetailTable => base.Tables["Sales_Order_Detail"];

		public DataTable TaxDetailsTable => base.Tables["Tax_Detail"];

		public SalesOrderData()
		{
			BuildDataTables();
		}

		public SalesOrderData(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}

		public static DataSet AddSalesOrderLotDetailTable(DataSet data)
		{
			DataTable dataTable = new DataTable("Sales_Order_ProductLot_Detail");
			DataColumnCollection columns = dataTable.Columns;
			columns.Add("LotNumber", typeof(string));
			columns.Add("SourceLotNumber", typeof(string));
			columns.Add("Reference", typeof(string));
			columns.Add("LocationID", typeof(string));
			columns.Add("BinID", typeof(string));
			columns.Add("RackID", typeof(string));
			columns.Add("ProductID", typeof(string));
			columns.Add("SysDocID", typeof(string));
			columns.Add("VoucherID", typeof(string));
			columns.Add("RowIndex", typeof(int));
			columns.Add("SoldQty", typeof(decimal));
			columns.Add("ReceiptDate", typeof(DateTime));
			columns.Add("UnitPrice", typeof(decimal));
			columns.Add("Cost", typeof(decimal));
			columns.Add("Reference2", typeof(string));
			data.Tables.Add(dataTable);
			return data;
		}

		private void BuildDataTables()
		{
			DataTable dataTable = new DataTable("Sales_Order");
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
			columns.Add("PriceIncludeTax", typeof(bool));
			columns.Add("Status", typeof(byte)).DefaultValue = 1;
			columns.Add("CurrencyID", typeof(string));
			columns.Add("TermID", typeof(string));
			columns.Add("ShippingMethodID", typeof(string));
			columns.Add("Reference", typeof(string));
			columns.Add("Reference2", typeof(string));
			columns.Add("Note", typeof(string));
			columns.Add("PONumber", typeof(string));
			columns.Add("IsVoid", typeof(bool));
			columns.Add("Discount", typeof(decimal));
			columns.Add("Total", typeof(decimal));
			columns.Add("TaxAmount", typeof(decimal));
			columns.Add("SourceDocType", typeof(byte));
			columns.Add("JobID", typeof(string));
			columns.Add("CostCategoryID", typeof(string));
			columns.Add("PayeeTaxGroupID", typeof(string));
			columns.Add("TaxOption", typeof(byte));
			columns.Add("RoundOff", typeof(decimal));
			columns.Add("AllowSOEdit", typeof(bool)).DefaultValue = 0;
			base.Tables.Add(dataTable);
			dataTable = new DataTable("Sales_Order_Detail");
			columns = dataTable.Columns;
			columns.Add("SysDocID", typeof(string));
			columns.Add("VoucherID", typeof(string));
			columns.Add("ProductID", typeof(string));
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
			columns.Add("CostCategoryID", typeof(string));
			columns.Add("SourceVoucherID", typeof(string));
			columns.Add("SourceSysDocID", typeof(string));
			columns.Add("SourceRowIndex", typeof(int));
			columns.Add("SourceDocType", typeof(byte));
			columns.Add("TaxOption", typeof(byte));
			columns.Add("TaxGroupID", typeof(string));
			columns.Add("TaxPercentage", typeof(decimal));
			columns.Add("TaxAmount", typeof(decimal));
			columns.Add("Cost", typeof(decimal));
			columns.Add("Discount", typeof(decimal));
			columns.Add("SpecificationID", typeof(string));
			columns.Add("StyleID", typeof(string));
			columns.Add("IsNonEdit", typeof(bool));
			columns.Add("RefSlNo", typeof(int));
			columns.Add("RefText1", typeof(string));
			columns.Add("RefText2", typeof(string));
			columns.Add("RefNum1", typeof(decimal));
			columns.Add("RefNum2", typeof(decimal));
			columns.Add("RefDate1", typeof(DateTime));
			columns.Add("RefDate2", typeof(DateTime));
			base.Tables.Add(dataTable);
			AddSalesOrderLotDetailTable(this);
			Merge(new TaxTransactionData().TaxDetailTable);
		}
	}
}
