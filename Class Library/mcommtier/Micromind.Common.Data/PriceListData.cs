using System;
using System.ComponentModel;
using System.Data;
using System.Runtime.Serialization;

namespace Micromind.Common.Data
{
	[Serializable]
	[DesignerCategory("Code")]
	public class PriceListData : DataSet
	{
		public const string PRICELIST_TABLE = "Price_List";

		public const string PRICELISTDETAIL_TABLE = "Price_List_Detail";

		public const string VENDORPRICELIST_TABLE = "Vendor_Price_List";

		public const string VENDORPRICELISTDETAIL_TABLE = "Vendor_Price_List_Detail";

		public const string SYSDOCID_FIELD = "SysDocID";

		public const string VOUCHERID_FIELD = "VoucherID";

		public const string SALESFLOW_FIELD = "SalesFlow";

		public const string CUSTOMERID_FIELD = "CustomerID";

		public const string VENDORID_FIELD = "VendorID";

		public const string TRANSACTIONDATE_FIELD = "TransactionDate";

		public const string VALIDDATEFROM_FIELD = "ValidDateFrom";

		public const string VALIDDATETO_FIELD = "ValidDateTo";

		public const string ROWINDEX_FIELD = "RowIndex";

		public const string SALESPERSONID_FIELD = "SalespersonID";

		public const string BUYERID_FIELD = "BuyerID";

		public const string REQUIREDDATE_FIELD = "RequiredDate";

		public const string SHIPPINGADDRESSID_FIELD = "ShippingAddressID";

		public const string CUSTOMERADDRESS_FIELD = "CustomerAddress";

		public const string STATUS_FIELD = "Status";

		public const string CURRENCYID_FIELD = "CurrencyID";

		public const string TERMID_FIELD = "TermID";

		public const string SHIPPINGMETHODID_FIELD = "ShippingMethodID";

		public const string REFERENCE_FIELD = "Reference";

		public const string NOTE_FIELD = "Note";

		public const string PONUMBER_FIELD = "PONumber";

		public const string ISVOID_FIELD = "IsVoid";

		public const string DISCOUNT_FIELD = "Discount";

		public const string TAXAMOUNT_FIELD = "TaxAmount";

		public const string TOTAL_FIELD = "Total";

		public const string INACTIVE_FIELD = "Inactive";

		public const string ISAPPLICABLETOCHILD_FIELD = "ApplicableToChild";

		public const string CREATEDBY_FIELD = "CreatedBy";

		public const string DATECREATED_FIELD = "DateCreated";

		public const string UPDATEDBY_FIELD = "UpdatedBy";

		public const string DATEUPDATED_FIELD = "DateUpdated";

		public const string PRODUCTID_FIELD = "ProductID";

		public const string CUSTOMERPRODUCTID_FIELD = "CustomerProductID";

		public const string VENDORPRODUCTID_FIELD = "VendorProductID";

		public const string QUANTITY_FIELD = "Quantity";

		public const string UNITPRICE_FIELD = "UnitPrice";

		public const string DESCRIPTION_FIELD = "Description";

		public const string UNITID_FIELD = "UnitID";

		public const string UNITQUANTITY_FIELD = "UnitQuantity";

		public const string UNITFACTOR_FIELD = "UnitFactor";

		public const string FACTORTYPE_FIELD = "FactorType";

		public const string SUBUNITPRICE_FIELD = "SubunitPrice";

		public const string REMARKS_FIELD = "Remarks";

		public DataTable PriceListTable => base.Tables["Price_List"];

		public DataTable PriceListDetailTable => base.Tables["Price_List_Detail"];

		public DataTable VendorPriceListTable => base.Tables["Vendor_Price_List"];

		public DataTable VendorPriceListDetailTable => base.Tables["Vendor_Price_List_Detail"];

		public PriceListData()
		{
			BuildDataTables();
		}

		public PriceListData(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}

		private void BuildDataTables()
		{
			DataTable dataTable = new DataTable("Price_List");
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
			columns.Add("CustomerID", typeof(string));
			columns.Add("TransactionDate", typeof(DateTime)).DefaultValue = DateTime.Now;
			columns.Add("SalespersonID", typeof(string));
			columns.Add("ValidDateFrom", typeof(DateTime));
			columns.Add("ValidDateTo", typeof(DateTime));
			columns.Add("Status", typeof(byte));
			columns.Add("Reference", typeof(string));
			columns.Add("Note", typeof(string));
			columns.Add("IsVoid", typeof(bool));
			columns.Add("Discount", typeof(decimal));
			columns.Add("TaxAmount", typeof(decimal));
			columns.Add("Total", typeof(decimal));
			columns.Add("Inactive", typeof(bool));
			columns.Add("ApplicableToChild", typeof(bool));
			base.Tables.Add(dataTable);
			dataTable = new DataTable("Price_List_Detail");
			columns = dataTable.Columns;
			columns.Add("SysDocID", typeof(string));
			columns.Add("VoucherID", typeof(string));
			columns.Add("ProductID", typeof(string));
			columns.Add("CustomerProductID", typeof(string));
			columns.Add("UnitPrice", typeof(decimal)).DefaultValue = 0;
			columns.Add("Description", typeof(string));
			columns.Add("Remarks", typeof(string));
			columns.Add("UnitID", typeof(string));
			columns.Add("UnitQuantity", typeof(float));
			columns.Add("UnitFactor", typeof(decimal));
			columns.Add("FactorType", typeof(string));
			columns.Add("SubunitPrice", typeof(decimal));
			columns.Add("RowIndex", typeof(int));
			base.Tables.Add(dataTable);
			dataTable = new DataTable("Vendor_Price_List");
			columns = dataTable.Columns;
			dataColumn = columns.Add("SysDocID", typeof(string));
			dataColumn2 = columns.Add("VoucherID", typeof(string));
			dataColumn.AllowDBNull = false;
			dataColumn2.AllowDBNull = false;
			dataTable.PrimaryKey = new DataColumn[2]
			{
				dataColumn,
				dataColumn2
			};
			columns.Add("VendorID", typeof(string));
			columns.Add("TransactionDate", typeof(DateTime)).DefaultValue = DateTime.Now;
			columns.Add("BuyerID", typeof(string));
			columns.Add("ValidDateFrom", typeof(DateTime));
			columns.Add("ValidDateTo", typeof(DateTime));
			columns.Add("Status", typeof(byte));
			columns.Add("Reference", typeof(string));
			columns.Add("Note", typeof(string));
			columns.Add("IsVoid", typeof(bool));
			columns.Add("Discount", typeof(decimal));
			columns.Add("TaxAmount", typeof(decimal));
			columns.Add("Total", typeof(decimal));
			columns.Add("Inactive", typeof(bool));
			columns.Add("ApplicableToChild", typeof(bool));
			base.Tables.Add(dataTable);
			dataTable = new DataTable("Vendor_Price_List_Detail");
			columns = dataTable.Columns;
			columns.Add("SysDocID", typeof(string));
			columns.Add("VoucherID", typeof(string));
			columns.Add("ProductID", typeof(string));
			columns.Add("VendorProductID", typeof(string));
			columns.Add("UnitPrice", typeof(decimal)).DefaultValue = 0;
			columns.Add("Description", typeof(string));
			columns.Add("Remarks", typeof(string));
			columns.Add("UnitID", typeof(string));
			columns.Add("UnitQuantity", typeof(float));
			columns.Add("UnitFactor", typeof(decimal));
			columns.Add("FactorType", typeof(string));
			columns.Add("SubunitPrice", typeof(decimal));
			columns.Add("RowIndex", typeof(int));
			base.Tables.Add(dataTable);
		}
	}
}
