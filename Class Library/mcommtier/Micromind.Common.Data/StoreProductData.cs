using System;
using System.ComponentModel;
using System.Data;
using System.Runtime.Serialization;

namespace Micromind.Common.Data
{
	[Serializable]
	[DesignerCategory("Code")]
	public class StoreProductData : DataSet
	{
		public const string STOREID_FIELD = "StoreID";

		public const string PRODUCTID_FIELD = "ProductID";

		public const string UNITSINSTOCK_FIELD = "UnitsInStock";

		public const string REORDERLEVEL_FIELD = "ReorderLevel";

		public const string UNITSONORDER_FIELD = "UnitsOnOrder";

		public const string UNITSRESERVED_FIELD = "UnitsReserved";

		public const string TOTALUNITSSOLD_FIELD = "TotalUnitsSold";

		public const string STOREPRODUCT_TABLE = "[Store Products]";

		public const string INVENTORYTRANSACTIONTYPES_TABLE = "[Inventory Transaction Types]";

		public const string TYPEDESCRIPTION_FIELD = "TypeDescription";

		public const string INVENTORYTRANSACTIONS_TABLE = "[Inventory Transactions]";

		public const string INVENTORYTRANSACTIONID_FIELD = "TransactionID";

		public const string QUANTITY_FIELD = "Quantity";

		public const string UNITPRICE_FIELD = "UnitPrice";

		public const string COST_FIELD = "Cost";

		public const string TYPE_FIELD = "Type";

		public const string TYPEID_FIELD = "TypeID";

		public const string ADJUSTMENTACCOUNT_FIELD = "AdjustmentAccount";

		public const string REFERENCE_FIELD = "Reference";

		public const string TRANSACTIONDATE_FIELD = "TransactionDate";

		public const string AVERAGECOST_FIELD = "AverageCost";

		public const string QUANTITYONHAND_FIELD = "QuantityOnHand";

		public const string STOREQUANTITYONHAND_FIELD = "StoreQuantityOnHand";

		public const string DESCRIPTION_FIELD = "Description";

		public const string ISINACTIVE_FIELD = "IsInactive";

		public const string JOURNALID_FIELD = "GLID";

		public const string STOREASSETVALUE_FIELD = "StoreAssetValue";

		public const string TOTALASSETVALUE_FIELD = "TotalAssetValue";

		public const string TRANSACTIONASSETVALUE_FIELD = "TransactionAssetValue";

		public const string NEWQUANTITY_FIELD = "NewQuantity";

		public DataTable StoreProductTable => base.Tables["[Store Products]"];

		public DataTable InventoryTransactionTable => base.Tables["[Inventory Transactions]"];

		public StoreProductData()
		{
			BuildDataTables();
		}

		public StoreProductData(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}

		private void BuildDataTables()
		{
			DataTable dataTable = new DataTable("[Store Products]");
			DataColumnCollection columns = dataTable.Columns;
			DataColumn dataColumn = columns.Add("StoreID", typeof(int));
			dataColumn.AllowDBNull = false;
			dataColumn.AutoIncrement = true;
			DataColumn dataColumn2 = columns.Add("ProductID", typeof(int));
			dataColumn2.AllowDBNull = false;
			dataColumn2.AutoIncrement = true;
			columns.Add("UnitsInStock", typeof(float)).DefaultValue = 0;
			columns.Add("ReorderLevel", typeof(float)).DefaultValue = 0;
			columns.Add("UnitsOnOrder", typeof(float)).DefaultValue = 0;
			columns.Add("UnitsReserved", typeof(float)).DefaultValue = 0;
			columns.Add("TotalUnitsSold", typeof(float)).DefaultValue = 0;
			base.Tables.Add(dataTable);
			dataTable = new DataTable("[Inventory Transactions]");
			DataColumnCollection columns2 = dataTable.Columns;
			columns2.Add("[Inventory Transactions]", typeof(int));
			columns2.Add("StoreID", typeof(int));
			columns2.Add("ProductID", typeof(int));
			columns2.Add("UnitsInStock", typeof(float)).DefaultValue = 0;
			columns2.Add("Quantity", typeof(float)).DefaultValue = 0;
			columns2.Add("Cost", typeof(decimal)).DefaultValue = 0;
			columns2.Add("UnitPrice", typeof(decimal)).DefaultValue = 0;
			columns2.Add("Type", typeof(byte));
			columns2.Add("AdjustmentAccount", typeof(int));
			columns2.Add("NewQuantity", typeof(float)).DefaultValue = 0;
			columns2.Add("Reference", typeof(string));
			columns2.Add("TransactionDate", typeof(DateTime)).DefaultValue = DateTime.Now;
			columns2.Add("AverageCost", typeof(decimal)).DefaultValue = 0;
			columns2.Add("QuantityOnHand", typeof(float)).DefaultValue = 0;
			columns2.Add("StoreQuantityOnHand", typeof(float)).DefaultValue = 0;
			columns2.Add("Description", typeof(string));
			columns2.Add("IsInactive", typeof(bool)).DefaultValue = false;
			columns2.Add("GLID", typeof(int));
			columns2.Add("StoreAssetValue", typeof(decimal)).DefaultValue = 0;
			columns2.Add("TotalAssetValue", typeof(decimal)).DefaultValue = 0;
			columns2.Add("TransactionAssetValue", typeof(decimal)).DefaultValue = 0;
			base.Tables.Add(dataTable);
		}
	}
}
