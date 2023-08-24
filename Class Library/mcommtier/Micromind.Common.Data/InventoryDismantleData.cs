using System;
using System.ComponentModel;
using System.Data;
using System.Runtime.Serialization;

namespace Micromind.Common.Data
{
	[Serializable]
	[DesignerCategory("Code")]
	public class InventoryDismantleData : DataSet
	{
		public const string INVENTORYDISMANTLE_TABLE = "Inventory_Dismantle";

		public const string SYSDOCID_FIELD = "SysDocID";

		public const string VOUCHERID_FIELD = "VoucherID";

		public const string DIVISIONID_FIELD = "DivisionID";

		public const string COMPANYID_FIELD = "CompanyID";

		public const string TRANSACTIONDATE_FIELD = "TransactionDate";

		public const string REFERENCE_FIELD = "Reference";

		public const string DESCRIPTION_FIELD = "Description";

		public const string LOCATIONID_FIELD = "LocationID";

		public const string QUANTITYDISMANTLED_FIELD = "QuantityDismantled";

		public const string UNITCOST_FIELD = "UnitCost";

		public const string DISMANTLEDPRODUCTID_FIELD = "DimantledProductID";

		public const string NOTE_FIELD = "Note";

		public const string CREATEDBY_FIELD = "CreatedBy";

		public const string DATECREATED_FIELD = "DateCreated";

		public const string UPDATEDBY_FIELD = "UpdatedBy";

		public const string DATEUPDATED_FIELD = "DateUpdated";

		public const string INVENTORYDISMANTLEDETAIL_TABLE = "Inventory_Dismantle_Detail";

		public const string PRODUCTID_FIELD = "ProductID";

		public const string ROWINDEX_FIELD = "RowIndex";

		public const string COST_FIELD = "Cost";

		public const string PERCENT_FIELD = "Cost_Percent";

		public const string SUBUNITCOST_FIELD = "SubUnitCost";

		public const string COGS_FIELD = "COGS";

		public const string QUANTITY_FIELD = "Quantity";

		public const string UNITQUANTITY_FIELD = "UnitQuantity";

		public const string UNITID_FIELD = "UnitID";

		public const string FACTOR_FIELD = "UnitFactor";

		public const string FACTORTYPE_FIELD = "FactorType";

		public const string TOTAL_FIELD = "Total";

		public const string INVENTORYDISMANTLEEXPENSE_TABLE = "Inventory_Dismantle_Expense";

		public const string EXPENSEID_FIELD = "ExpenseID";

		public const string RATETYPE_FIELD = "RateType";

		public const string AMOUNT_FIELD = "Amount";

		public const string CURRENCYID_FIELD = "CurrencyID";

		public const string CURRENCYRATE_FIELD = "CurrencyRate";

		public DataTable InventoryDismantleTable => base.Tables["Inventory_Dismantle"];

		public DataTable InventoryDismantleDetailTable => base.Tables["Inventory_Dismantle_Detail"];

		public DataTable InventoryDismantleExpenseTable => base.Tables["Inventory_Dismantle_Expense"];

		public InventoryDismantleData()
		{
			BuildDataTables();
		}

		public InventoryDismantleData(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}

		private void BuildDataTables()
		{
			DataTable dataTable = new DataTable("Inventory_Dismantle");
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
			columns.Add("DimantledProductID", typeof(string));
			columns.Add("TransactionDate", typeof(DateTime));
			columns.Add("Reference", typeof(string));
			columns.Add("Description", typeof(string));
			columns.Add("LocationID", typeof(string));
			columns.Add("QuantityDismantled", typeof(decimal));
			columns.Add("UnitCost", typeof(decimal));
			columns.Add("Note", typeof(string));
			base.Tables.Add(dataTable);
			dataTable = new DataTable("Inventory_Dismantle_Detail");
			columns = dataTable.Columns;
			columns.Add("SysDocID", typeof(string));
			columns.Add("VoucherID", typeof(string));
			columns.Add("LocationID", typeof(string));
			columns.Add("ProductID", typeof(string));
			columns.Add("Description", typeof(string));
			columns.Add("RowIndex", typeof(short));
			columns.Add("Cost", typeof(decimal));
			columns.Add("Cost_Percent", typeof(decimal));
			columns.Add("SubUnitCost", typeof(decimal));
			columns.Add("COGS", typeof(decimal));
			columns.Add("Quantity", typeof(decimal));
			columns.Add("UnitQuantity", typeof(decimal));
			columns.Add("UnitID", typeof(string));
			columns.Add("UnitFactor", typeof(float));
			columns.Add("FactorType", typeof(string));
			columns.Add("Total", typeof(decimal));
			base.Tables.Add(dataTable);
			dataTable = new DataTable("Inventory_Dismantle_Expense");
			columns = dataTable.Columns;
			columns.Add("SysDocID", typeof(string));
			columns.Add("VoucherID", typeof(string));
			columns.Add("ExpenseID", typeof(string));
			columns.Add("ProductID", typeof(string));
			columns.Add("Description", typeof(string));
			columns.Add("Amount", typeof(decimal));
			columns.Add("Reference", typeof(string));
			columns.Add("CurrencyID", typeof(string));
			columns.Add("CurrencyRate", typeof(decimal));
			columns.Add("RateType", typeof(string));
			columns.Add("RowIndex", typeof(int));
			base.Tables.Add(dataTable);
			InventoryTransactionData.AddProductLotIssueDetailTable(this);
			InventoryTransactionData.AddProductLotReceivingDetailTable(this);
		}
	}
}
