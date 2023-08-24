using System;
using System.ComponentModel;
using System.Data;
using System.Runtime.Serialization;

namespace Micromind.Common.Data
{
	[Serializable]
	[DesignerCategory("Code")]
	public class PurchaseReceiptData : DataSet
	{
		public const string PURCHASERECEIPT_TABLE = "Purchase_Receipt";

		public const string PURCHASERECEIPTDETAIL_TABLE = "Purchase_Receipt_Detail";

		public const string SYSDOCID_FIELD = "SysDocID";

		public const string VOUCHERID_FIELD = "VoucherID";

		public const string DIVISIONID_FIELD = "DivisionID";

		public const string COMPANYID_FIELD = "CompanyID";

		public const string VENDORID_FIELD = "VendorID";

		public const string PURCHASEFLOW_FIELD = "PurchaseFlow";

		public const string TRANSACTIONDATE_FIELD = "TransactionDate";

		public const string ROWINDEX_FIELD = "RowIndex";

		public const string BUYERID_FIELD = "BuyerID";

		public const string STATUS_FIELD = "Status";

		public const string CURRENCYID_FIELD = "CurrencyID";

		public const string CURRENCYRATE_FIELD = "CurrencyRate";

		public const string TERMID_FIELD = "TermID";

		public const string SHIPPINGMETHODID_FIELD = "ShippingMethodID";

		public const string REFERENCE_FIELD = "Reference";

		public const string REFERENCE2_FIELD = "Reference2";

		public const string VENDORREFERENCENUMBER_FIELD = "VendorReferenceNo";

		public const string NOTE_FIELD = "Note";

		public const string PONUMBER_FIELD = "PONumber";

		public const string SOURCEDOCTYPE_FIELD = "SourceDocType";

		public const string ISVOID_FIELD = "IsVoid";

		public const string DISCOUNT_FIELD = "Discount";

		public const string DISCOUNTFC_FIELD = "DiscountFC";

		public const string TAXAMOUNT_FIELD = "TaxAmount";

		public const string TAXAMOUNTFC_FIELD = "TaxAmountFC";

		public const string TOTAL_FIELD = "Total";

		public const string TOTALFC_FIELD = "TotalFC";

		public const string ISINVOICED_FIELD = "IsInvoiced";

		public const string ISIMPORT_FIELD = "IsImport";

		public const string POSYSDOCID_FIELD = "POSysDocID";

		public const string POVOUCHERID_FIELD = "POVoucherID";

		public const string SOURCESYSDOCID_FIELD = "SourceSysDocID";

		public const string SOURCEVOUCHERID_FIELD = "SourceVoucherID";

		public const string TRANSPORTERID_FIELD = "TransporterID";

		public const string VEHICLEID_FIELD = "VehicleID";

		public const string CONTAINERNO_FIELD = "ContainerNumber";

		public const string CONTAINERSIZEID_FIELD = "ContainerSizeID";

		public const string DRIVERID_FIELD = "DriverID";

		public const string PAYEETAXGROUPID_FIELD = "PayeeTaxGroupID";

		public const string TAXOPTION_FIELD = "TaxOption";

		public const string TAXGROUPID_FIELD = "TaxGroupID";

		public const string PRODUCTID_FIELD = "ProductID";

		public const string QUANTITY_FIELD = "Quantity";

		public const string UNITPRICE_FIELD = "UnitPrice";

		public const string DESCRIPTION_FIELD = "Description";

		public const string UNITID_FIELD = "UnitID";

		public const string UNITQUANTITY_FIELD = "UnitQuantity";

		public const string UNITFACTOR_FIELD = "UnitFactor";

		public const string FACTORTYPE_FIELD = "FactorType";

		public const string SUBUNITPRICE_FIELD = "SubunitPrice";

		public const string REMARKS_FIELD = "Remarks";

		public const string SPECIFICATIONID_FIELD = "SpecificationID";

		public const string STYLEID_FIELD = "StyleID";

		public const string JOBID_FIELD = "JobID";

		public const string COSTCATEGORYID_FIELD = "CostCategoryID";

		public const string LOCATIONID_FIELD = "LocationID";

		public const string QUANTITYSHIPPED_FIELD = "QuantityShipped";

		public const string ORDERVOUCHERID_FIELD = "OrderVoucherID";

		public const string ORDERSYSDOCID_FIELD = "OrderSysDocID";

		public const string ORDERROWINDEX_FIELD = "OrderRowIndex";

		public const string ISPORROW_FIELD = "IsPORRow";

		public const string ROWSOURCE_FIELD = "ROWSOURCE";

		public const string PKVOUCHERID_FIELD = "PKVoucherID";

		public const string PKSYSDOCID_FIELD = "PKSysDocID";

		public const string PKROWINDEX_FIELD = "PKRowIndex";

		public const string ACTIVATEGRNEDIT_FIELD = "ActivateGrnEdit";

		public const string LISTVOUCHERID_FIELD = "ListVoucherID";

		public const string LISTSYSDOCID_FIELD = "ListSysDocID";

		public const string LISTROWINDEX_FIELD = "ListRowIndex";

		public const string REFSLNO_FIELD = "RefSlNo";

		public const string REFTEXT1_FIELD = "RefText1";

		public const string REFTEXT2_FIELD = "RefText2";

		public const string REFNUM1_FIELD = "RefNum1";

		public const string REFNUM2_FIELD = "RefNum2";

		public const string REFDATE1_FIELD = "RefDate1";

		public const string REFDATE2_FIELD = "RefDate2";

		public const string ISNEW_FIELD = "IsNew";

		public const string CLAIMSTATUS_FIELD = "ClaimStatus";

		public const string CLAIMREF1_FIELD = "ClaimRef1";

		public const string CLAIMREF2_FIELD = "ClaimRef2";

		public const string CLAIMAMOUNT_FIELD = "ClaimAmount";

		public const string CLAIMAMOUNTFC_FIELD = "ClaimAmountFC";

		public const string CLAIMCURRENCYID_FIELD = "ClaimCurrencyID";

		public const string CLAIMCURRENCYRATE_FIELD = "ClaimCurrencyRate";

		public const string GROUPNAME_FIELD = "GroupName";

		public const string CLAIMREMARKS_FIELD = "ClaimRemarks";

		public DataTable PurchaseReceiptTable => base.Tables["Purchase_Receipt"];

		public DataTable PurchaseReceiptDetailTable => base.Tables["Purchase_Receipt_Detail"];

		public PurchaseReceiptData()
		{
			BuildDataTables();
		}

		public PurchaseReceiptData(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}

		private void BuildDataTables()
		{
			DataTable dataTable = new DataTable("Purchase_Receipt");
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
			columns.Add("BuyerID", typeof(string));
			columns.Add("Status", typeof(byte));
			columns.Add("CurrencyID", typeof(string));
			columns.Add("PurchaseFlow", typeof(byte));
			columns.Add("CurrencyRate", typeof(decimal));
			columns.Add("TermID", typeof(string));
			columns.Add("ShippingMethodID", typeof(string));
			columns.Add("Reference", typeof(string));
			columns.Add("Reference2", typeof(string));
			columns.Add("VendorReferenceNo", typeof(string));
			columns.Add("Note", typeof(string));
			columns.Add("PONumber", typeof(string));
			columns.Add("SourceDocType", typeof(byte));
			columns.Add("IsVoid", typeof(bool));
			columns.Add("Discount", typeof(decimal));
			columns.Add("DiscountFC", typeof(decimal));
			columns.Add("TaxAmount", typeof(decimal));
			columns.Add("TaxAmountFC", typeof(decimal));
			columns.Add("Total", typeof(decimal));
			columns.Add("TotalFC", typeof(decimal));
			columns.Add("IsImport", typeof(bool));
			columns.Add("POSysDocID", typeof(string));
			columns.Add("POVoucherID", typeof(string));
			columns.Add("SourceSysDocID", typeof(string));
			columns.Add("SourceVoucherID", typeof(string));
			columns.Add("TransporterID", typeof(string));
			columns.Add("DriverID", typeof(string));
			columns.Add("VehicleID", typeof(string));
			columns.Add("ContainerNumber", typeof(string));
			columns.Add("ContainerSizeID", typeof(string));
			columns.Add("PayeeTaxGroupID", typeof(string));
			columns.Add("TaxOption", typeof(byte));
			columns.Add("ClaimStatus", typeof(short));
			columns.Add("ClaimRef1", typeof(string));
			columns.Add("ClaimRef2", typeof(string));
			columns.Add("GroupName", typeof(string));
			columns.Add("ClaimAmount", typeof(decimal));
			columns.Add("ClaimAmountFC", typeof(decimal));
			columns.Add("ClaimRemarks", typeof(string));
			columns.Add("ClaimCurrencyID", typeof(string));
			columns.Add("ClaimCurrencyRate", typeof(decimal));
			columns.Add("ActivateGrnEdit", typeof(bool)).DefaultValue = 0;
			base.Tables.Add(dataTable);
			dataTable = new DataTable("Purchase_Receipt_Detail");
			columns = dataTable.Columns;
			columns.Add("SysDocID", typeof(string));
			columns.Add("VoucherID", typeof(string));
			columns.Add("ProductID", typeof(string));
			columns.Add("Quantity", typeof(float)).DefaultValue = 0;
			columns.Add("UnitPrice", typeof(decimal)).DefaultValue = 0;
			columns.Add("Description", typeof(string));
			columns.Add("UnitID", typeof(string));
			columns.Add("UnitQuantity", typeof(float));
			columns.Add("UnitFactor", typeof(decimal));
			columns.Add("FactorType", typeof(string));
			columns.Add("JobID", typeof(string));
			columns.Add("CostCategoryID", typeof(string));
			columns.Add("LocationID", typeof(string));
			columns.Add("RowIndex", typeof(short));
			columns.Add("OrderVoucherID", typeof(string));
			columns.Add("ROWSOURCE", typeof(short));
			columns.Add("OrderSysDocID", typeof(string));
			columns.Add("OrderRowIndex", typeof(int));
			columns.Add("IsPORRow", typeof(bool));
			columns.Add("PKVoucherID", typeof(string));
			columns.Add("PKSysDocID", typeof(string));
			columns.Add("PKRowIndex", typeof(short));
			columns.Add("SpecificationID", typeof(string));
			columns.Add("StyleID", typeof(string));
			columns.Add("ListVoucherID", typeof(string));
			columns.Add("ListSysDocID", typeof(string));
			columns.Add("ListRowIndex", typeof(short));
			columns.Add("Remarks", typeof(string));
			columns.Add("TaxGroupID", typeof(string));
			columns.Add("TaxOption", typeof(byte));
			columns.Add("RefSlNo", typeof(int));
			columns.Add("RefText1", typeof(string));
			columns.Add("RefText2", typeof(string));
			columns.Add("RefNum1", typeof(decimal));
			columns.Add("RefNum2", typeof(decimal));
			columns.Add("RefDate1", typeof(DateTime));
			columns.Add("RefDate2", typeof(DateTime));
			columns.Add("IsNew", typeof(bool));
			base.Tables.Add(dataTable);
			InventoryTransactionData.AddProductLotReceivingDetailTable(this);
		}
	}
}
