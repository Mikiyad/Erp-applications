using System;
using System.ComponentModel;
using System.Data;
using System.Runtime.Serialization;

namespace Micromind.Common.Data
{
	[Serializable]
	[DesignerCategory("Code")]
	public class InventoryTransactionData : DataSet
	{
		public const string SYSDOCID_FIELD = "SysDocID";

		public const string VOUCHERID_FIELD = "VoucherID";

		public const string TRANSACTIONDATE_FIELD = "TransactionDate";

		public const string PRODUCTID_FIELD = "ProductID";

		public const string COMPANYID_FIELD = "CompanyID";

		public const string DIVISIONID_FIELD = "DivisionID";

		public const string LOCATIONID_FIELD = "LocationID";

		public const string JOBID_FIELD = "JobID";

		public const string COSTCATEGORYID_FIELD = "CostCategoryID";

		public const string EQWORKORDERID = "EqWorkOrderID";

		public const string QUANTITY_FIELD = "Quantity";

		public const string FOCQUANTITY_FIELD = "FOCQuantity";

		public const string REFERENCE_FIELD = "Reference";

		public const string DESCRIPTION_FIELD = "Description";

		public const string COST_FIELD = "Cost";

		public const string ROWINDEX_FIELD = "RowIndex";

		public const string SYSDOCTYPE_FIELD = "SysDocType";

		public const string LOTNUMBER_FIELD = "LotNumber";

		public const string UNITID_FIELD = "UnitID";

		public const string UNITQUANTITY_FIELD = "UnitQuantity";

		public const string FACTOR_FIELD = "Factor";

		public const string FACTORTYPE_FIELD = "FactorType";

		public const string UNITPRICE_FIELD = "UnitPrice";

		public const string AVERAGECOST_FIELD = "AverageCost";

		public const string ASSETVALUE_FIELD = "AssetValue";

		public const string PAYEETYPE_FIELD = "PayeeType";

		public const string PAYEEID_FIELD = "PayeeID";

		public const string TRANSACTIONTYPE_FIELD = "TransactionType";

		public const string ISNONCOSTEDGRN_FIELD = "IsNonCostedGRN";

		public const string SPECIFICATIONID_FIELD = "SpecificationID";

		public const string STYLEID_FIELD = "StyleID";

		public const string ISRECOST_FIELD = "IsRecost";

		public const string RETURNEDQUANTITY_FIELD = "ReturnedQuantity";

		public const string REFSYSDOCID_FIELD = "RefSysDocID";

		public const string REFVOUCHERID_FIELD = "RefVoucherID";

		public const string REFROWINDEX_FIELD = "RefRowIndex";

		public const string REFTRANSACTIONID_FIELD = "RefTransactionID";

		public const string DISCOUNT_FIELD = "Discount";

		public const string CREATEDBY_FIELD = "CreatedBy";

		public const string DATECREATED_FIELD = "DateCreated";

		public const string UPDATEDBY_FIELD = "UpdatedBy";

		public const string DATEUPDATED_FIELD = "DateUpdated";

		public const string LOTQTY_FIELD = "LotQty";

		public const string SOURCELOTNUMBER_FIELD = "SourceLotNumber";

		public const string SOLDQTY_FIELD = "SoldQty";

		public const string PRODUCTIONDATE_FIELD = "ProductionDate";

		public const string EXPIRYDATE_FIELD = "ExpiryDate";

		public const string BINID_FIELD = "BinID";

		public const string RACKID_FIELD = "RackID";

		public const string PRODUCTLOTRECEIVINGDETAIL_TABLE = "Product_Lot_Receiving_Detail";

		public const string PRODUCTLOTISSUEDETAIL_TABLE = "Product_Lot_Issue_Detail";

		public const string RECEIPTDATE_FIELD = "ReceiptDate";

		public const string REFERENCE2_FIELD = "Reference2";

		public const string INVENTORYTRANSACTIONS_TABLE = "Inventory_Transactions";

		public DataTable InventoryTransactionTable => base.Tables["Inventory_Transactions"];

		public DataTable ProductLotReceivingDetail => base.Tables["Product_Lot_Receiving_Detail"];

		public InventoryTransactionData()
		{
			BuildDataTables();
		}

		public InventoryTransactionData(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}

		public static DataSet AddProductLotReceivingDetailTable(DataSet data)
		{
			DataTable dataTable = new DataTable("Product_Lot_Receiving_Detail");
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
			columns.Add("LotQty", typeof(float));
			columns.Add("SoldQty", typeof(float));
			columns.Add("ProductionDate", typeof(DateTime));
			columns.Add("ExpiryDate", typeof(DateTime));
			columns.Add("ReceiptDate", typeof(DateTime));
			columns.Add("Reference2", typeof(string));
			data.Tables.Add(dataTable);
			return data;
		}

		public static DataSet AddProductLotIssueDetailTable(DataSet data)
		{
			DataTable dataTable = new DataTable("Product_Lot_Issue_Detail");
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
			DataTable dataTable = new DataTable("Inventory_Transactions");
			DataColumnCollection columns = dataTable.Columns;
			DataColumn dataColumn = columns.Add("SysDocID", typeof(string));
			DataColumn dataColumn2 = columns.Add("VoucherID", typeof(string));
			dataColumn.AllowDBNull = false;
			dataColumn2.AllowDBNull = false;
			columns.Add("TransactionDate", typeof(DateTime));
			columns.Add("Reference", typeof(string));
			columns.Add("Description", typeof(string));
			columns.Add("CompanyID", typeof(string));
			columns.Add("DivisionID", typeof(string));
			columns.Add("LocationID", typeof(string));
			columns.Add("JobID", typeof(string));
			columns.Add("CostCategoryID", typeof(string));
			columns.Add("EqWorkOrderID", typeof(string));
			columns.Add("RowIndex", typeof(int));
			columns.Add("ProductID", typeof(string));
			columns.Add("ItemType", typeof(byte));
			columns.Add("UnitID", typeof(string));
			columns.Add("Quantity", typeof(decimal));
			columns.Add("ReturnedQuantity", typeof(decimal));
			columns.Add("FOCQuantity", typeof(decimal));
			columns.Add("Factor", typeof(decimal));
			columns.Add("FactorType", typeof(string));
			columns.Add("Cost", typeof(decimal));
			columns.Add("SysDocType", typeof(string));
			columns.Add("LotNumber", typeof(string));
			columns.Add("UnitQuantity", typeof(decimal));
			columns.Add("UnitPrice", typeof(decimal));
			columns.Add("Discount", typeof(decimal)).DefaultValue = 0;
			columns.Add("AverageCost", typeof(decimal));
			columns.Add("AssetValue", typeof(decimal));
			columns.Add("PayeeType", typeof(string));
			columns.Add("PayeeID", typeof(string));
			columns.Add("IsNonCostedGRN", typeof(bool));
			columns.Add("SpecificationID", typeof(string));
			columns.Add("IsRecost", typeof(bool));
			columns.Add("StyleID", typeof(string));
			columns.Add("RefSysDocID", typeof(string));
			columns.Add("RefVoucherID", typeof(string));
			columns.Add("RefRowIndex", typeof(int));
			columns.Add("RefTransactionID", typeof(int));
			columns.Add("CreatedBy", typeof(string));
			columns.Add("DateCreated", typeof(DateTime));
			columns.Add("TransactionType", typeof(byte));
			base.Tables.Add(dataTable);
			AddProductLotReceivingDetailTable(this);
			AddProductLotIssueDetailTable(this);
		}
	}
}
