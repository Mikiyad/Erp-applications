using System;
using System.ComponentModel;
using System.Data;
using System.Runtime.Serialization;

namespace Micromind.Common.Data
{
	[Serializable]
	[DesignerCategory("Code")]
	public class ProductPriceBulkUpdateData : DataSet
	{
		public const string PRODUCTPRICEBULKUPDATE_TABLE = "Product_Price_Bulk_Update";

		public const string PRODUCTPRICEBULKUPDATEDETAIL_TABLE = "Product_Price_Bulk_Update_Detail";

		public const string SYSDOCID_FIELD = "SysDocID";

		public const string VOUCHERID_FIELD = "VoucherID";

		public const string TRANSACTIONDATE_FIELD = "TransactionDate";

		public const string ROWINDEX_FIELD = "RowIndex";

		public const string STATUS_FIELD = "Status";

		public const string NOTE_FIELD = "Note";

		public const string ISVOID_FIELD = "IsVoid";

		public const string CREATEDBY_FIELD = "CreatedBy";

		public const string DATECREATED_FIELD = "DateCreated";

		public const string UPDATEDBY_FIELD = "UpdatedBy";

		public const string DATEUPDATED_FIELD = "DateUpdated";

		public const string PRODUCTID_FIELD = "ProductID";

		public const string DESCRIPTION_FIELD = "Description";

		public const string CATEGORYID_FIELD = "CategoryID";

		public const string LASTPURCHASEPRICE_FIELD = "LastPurchasePrice";

		public const string STANDARDCOST_FIELD = "StandardCost";

		public const string STANDARDPRICE_FIELD = "StandardPrice";

		public const string STANDARDPRICEPERCENT_FIELD = "StandardPricePercent";

		public const string WHOLESALEPRICE_FIELD = "WholesalePrice";

		public const string WHOLESALEPRICEPERCENT_FIELD = "WholesalePricePercent";

		public const string SPECIALPRICEPERCENT_FIELD = "SpecialPricePercent";

		public const string SPECIALPRICE_FIELD = "SpecialPrice";

		public const string MINIMUMPRICE_FIELD = "MinimumPrice";

		public const string MINIMUMPRICEPERCENT_FIELD = "MinimumPricePercent";

		public DataTable ProductPriceBulkUpdateTable => base.Tables["Product_Price_Bulk_Update"];

		public DataTable ProductPriceBulkUpdateDetailTable => base.Tables["Product_Price_Bulk_Update_Detail"];

		public ProductPriceBulkUpdateData()
		{
			BuildDataTables();
		}

		public ProductPriceBulkUpdateData(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}

		private void BuildDataTables()
		{
			DataTable dataTable = new DataTable("Product_Price_Bulk_Update");
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
			columns.Add("TransactionDate", typeof(DateTime)).DefaultValue = DateTime.Now;
			columns.Add("Status", typeof(byte));
			columns.Add("Note", typeof(string));
			columns.Add("IsVoid", typeof(bool));
			base.Tables.Add(dataTable);
			dataTable = new DataTable("Product_Price_Bulk_Update_Detail");
			columns = dataTable.Columns;
			columns.Add("SysDocID", typeof(string));
			columns.Add("VoucherID", typeof(string));
			columns.Add("ProductID", typeof(string));
			columns.Add("CategoryID", typeof(string));
			columns.Add("Description", typeof(string));
			columns.Add("StandardPrice", typeof(string));
			columns.Add("StandardPricePercent", typeof(string));
			columns.Add("WholesalePrice", typeof(string));
			columns.Add("WholesalePricePercent", typeof(string));
			columns.Add("SpecialPricePercent", typeof(string));
			columns.Add("SpecialPrice", typeof(string));
			columns.Add("StandardCost", typeof(string));
			columns.Add("MinimumPricePercent", typeof(string));
			columns.Add("MinimumPrice", typeof(string));
			columns.Add("LastPurchasePrice", typeof(string));
			columns.Add("RowIndex", typeof(short));
			base.Tables.Add(dataTable);
		}
	}
}
