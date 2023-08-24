using System;
using System.ComponentModel;
using System.Data;
using System.Runtime.Serialization;

namespace Micromind.Common.Data
{
	[Serializable]
	[DesignerCategory("Code")]
	public class InventoryAdjustmentData : DataSet
	{
		public const string SYSDOCID_FIELD = "SysDocID";

		public const string VOUCHERID_FIELD = "VoucherID";

		public const string DIVISIONID_FIELD = "DivisionID";

		public const string COMPANYID_FIELD = "CompanyID";

		public const string TRANSACTIONDATE_FIELD = "TransactionDate";

		public const string REFERENCE_FIELD = "Reference";

		public const string ACCOUNTID_FIELD = "AccountID";

		public const string DESCRIPTION_FIELD = "Description";

		public const string REMARKS_FIELD = "Remarks";

		public const string LOCATIONID_FIELD = "LocationID";

		public const string ADJUSTMENTTYPEID_FIELD = "AdjustmentTypeID";

		public const string INVENTORYADJUSTMENT_TABLE = "Inventory_Adjustment";

		public const string CREATEDBY_FIELD = "CreatedBy";

		public const string DATECREATED_FIELD = "DateCreated";

		public const string UPDATEDBY_FIELD = "UpdatedBy";

		public const string DATEUPDATED_FIELD = "DateUpdated";

		public const string PRODUCTID_FIELD = "ProductID";

		public const string ROWINDEX_FIELD = "RowIndex";

		public const string COST_FIELD = "Cost";

		public const string QUANTITY_FIELD = "Quantity";

		public const string UNITID_FIELD = "UnitID";

		public const string UNITQUANTITY_FIELD = "UnitQuantity";

		public const string FACTOR_FIELD = "Factor";

		public const string FACTORTYPE_FIELD = "FactorType";

		public const string LISTVOUCHERID_FIELD = "ListVoucherID";

		public const string LISTSYSDOCID_FIELD = "ListSysDocID";

		public const string LISTROWINDEX_FIELD = "ListRowIndex";

		public const string INVENTORYADJUSTMENTDETAIL_TABLE = "Inventory_Adjustment_Detail";

		public DataTable InventoryAdjustmentTable => base.Tables["Inventory_Adjustment"];

		public DataTable InventoryAdjustmentDetailsTable => base.Tables["Inventory_Adjustment_Detail"];

		public InventoryAdjustmentData()
		{
			BuildDataTables();
		}

		public InventoryAdjustmentData(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}

		private void BuildDataTables()
		{
			DataTable dataTable = new DataTable("Inventory_Adjustment");
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
			columns.Add("TransactionDate", typeof(DateTime));
			columns.Add("AccountID", typeof(string));
			columns.Add("Reference", typeof(string));
			columns.Add("Description", typeof(string));
			columns.Add("LocationID", typeof(string));
			columns.Add("AdjustmentTypeID", typeof(string));
			base.Tables.Add(dataTable);
			dataTable = new DataTable("Inventory_Adjustment_Detail");
			columns = dataTable.Columns;
			columns.Add("SysDocID", typeof(string));
			columns.Add("VoucherID", typeof(string));
			columns.Add("ProductID", typeof(string));
			columns.Add("RowIndex", typeof(short));
			columns.Add("Cost", typeof(decimal));
			columns.Add("Quantity", typeof(float));
			columns.Add("Description", typeof(string));
			columns.Add("UnitID", typeof(string));
			columns.Add("UnitQuantity", typeof(float));
			columns.Add("Factor", typeof(float));
			columns.Add("FactorType", typeof(string));
			columns.Add("ListVoucherID", typeof(string));
			columns.Add("ListSysDocID", typeof(string));
			columns.Add("ListRowIndex", typeof(short));
			columns.Add("Remarks", typeof(string));
			base.Tables.Add(dataTable);
			InventoryTransactionData.AddProductLotReceivingDetailTable(this);
			InventoryTransactionData.AddProductLotIssueDetailTable(this);
		}
	}
}
