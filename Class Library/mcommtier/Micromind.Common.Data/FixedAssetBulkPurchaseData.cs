using System;
using System.ComponentModel;
using System.Data;
using System.Runtime.Serialization;

namespace Micromind.Common.Data
{
	[Serializable]
	[DesignerCategory("Code")]
	public class FixedAssetBulkPurchaseData : DataSet
	{
		public const string FIXEDASSETBULKPURCHASE_TABLE = "FixedAsset_BulkPurchase";

		public const string FIXEDASSETBULKPURCHASEDETAIL_TABLE = "FixedAsset_BulkPurchase_Detail";

		public const string SYSDOCID_FIELD = "SysDocID";

		public const string VOUCHERID_FIELD = "VoucherID";

		public const string VENDORID_FIELD = "VendorID";

		public const string PAYEEID_FIELD = "PayeeID";

		public const string PAYEETYPE_FIELD = "PayeeType";

		public const string TRANSACTIONDATE_FIELD = "TransactionDate";

		public const string REFERENCE_FIELD = "Reference";

		public const string NOTE_FIELD = "Note";

		public const string BUYERID_FIELD = "BuyerID";

		public const string CURRENCYID_FIELD = "CurrencyID";

		public const string QUANTITY_FIELD = "Quantity";

		public const string ITEMAMOUNT_FIELD = "ItemAmount";

		public const string CURRENCYRATE_FIELD = "CurrencyRate";

		public const string ISVOID_FIELD = "IsVoid";

		public const string LOCATIONID_FIELD = "LocationID";

		public const string ASSETCLASSID_FIELD = "AssetClassID";

		public const string CREATEDBY_FIELD = "CreatedBy";

		public const string DATECREATED_FIELD = "DateCreated";

		public const string UPDATEDBY_FIELD = "UpdatedBy";

		public const string DATEUPDATED_FIELD = "DateUpdated";

		public const string ROWINDEX_FIELD = "RowIndex";

		public const string ASSETID_FIELD = "AssetID";

		public const string DESCRIPTION_FIELD = "Description";

		public const string AMOUNT_FIELD = "Amount";

		public const string AMOUNTFC_FIELD = "AmountFC";

		public const string ASSETNAME_FIELD = "AssetName";

		public const string SERIALNUMBER_FIELD = "SerialNumber";

		public const string BARCODENUMBER_FIELD = "BarcodeNumber";

		public DataTable FixedAssetBulkPurchaseTable => base.Tables["FixedAsset_BulkPurchase"];

		public DataTable FixedAssetBulkPurchaseDetailTable => base.Tables["FixedAsset_BulkPurchase_Detail"];

		public FixedAssetBulkPurchaseData()
		{
			BuildDataTables();
		}

		public FixedAssetBulkPurchaseData(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}

		private void BuildDataTables()
		{
			DataTable dataTable = new DataTable("FixedAsset_BulkPurchase");
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
			columns.Add("VendorID", typeof(string));
			columns.Add("TransactionDate", typeof(DateTime)).DefaultValue = DateTime.Now;
			columns.Add("BuyerID", typeof(string));
			columns.Add("PayeeID", typeof(string));
			columns.Add("PayeeType", typeof(string));
			columns.Add("CurrencyID", typeof(string));
			columns.Add("CurrencyRate", typeof(decimal));
			columns.Add("Quantity", typeof(decimal));
			columns.Add("ItemAmount", typeof(decimal));
			columns.Add("LocationID", typeof(string));
			columns.Add("AssetClassID", typeof(string));
			columns.Add("Amount", typeof(decimal)).DefaultValue = 0;
			columns.Add("AmountFC", typeof(decimal)).DefaultValue = 0;
			columns.Add("Reference", typeof(string));
			columns.Add("Note", typeof(string));
			columns.Add("IsVoid", typeof(bool));
			base.Tables.Add(dataTable);
			dataTable = new DataTable("FixedAsset_BulkPurchase_Detail");
			columns = dataTable.Columns;
			columns.Add("SysDocID", typeof(string));
			columns.Add("VoucherID", typeof(string));
			columns.Add("AssetID", typeof(string));
			columns.Add("AssetName", typeof(string));
			columns.Add("SerialNumber", typeof(string));
			columns.Add("BarcodeNumber", typeof(string));
			columns.Add("Description", typeof(string));
			columns.Add("Amount", typeof(decimal)).DefaultValue = 0;
			columns.Add("AmountFC", typeof(decimal)).DefaultValue = 0;
			columns.Add("RowIndex", typeof(short));
			base.Tables.Add(dataTable);
		}
	}
}
