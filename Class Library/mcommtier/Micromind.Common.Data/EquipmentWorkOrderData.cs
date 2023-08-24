using System;
using System.ComponentModel;
using System.Data;
using System.Runtime.Serialization;

namespace Micromind.Common.Data
{
	[Serializable]
	[DesignerCategory("Code")]
	public class EquipmentWorkOrderData : DataSet
	{
		public const string WORKORDER_TABLE = "EA_Work_Order";

		public const string WORKORDEREXPENSEDETAIL_TABLE = "EA_WorkOrder_Expense_Detail";

		public const string WORKORDERRESOURCESDETAIL_TABLE = "EA_WorkOrder_Resources_Detail";

		public const string WORKORDERMANPOWERDETAIL_TABLE = "EA_WorkOrder_ManPower_Detail";

		public const string SYSDOCID_FIELD = "SysDocID";

		public const string VOUCHERID_FIELD = "VoucherID";

		public const string TRANSACTIONDATE_FIELD = "TransactionDate";

		public const string ROWINDEX_FIELD = "RowIndex";

		public const string STATUS_FIELD = "Status";

		public const string NOTE_FIELD = "Note";

		public const string REFERENCE_FIELD = "Reference";

		public const string DESCRIPTION_FIELD = "Description";

		public const string EQUIPMENTID_FIELD = "EquipmentID";

		public const string WORKORDERTYPE_FIELD = "WorkOrderTypeID";

		public const string CURRENTMETERREADING_FIELD = "CurrentMeterReading";

		public const string ISVOID_FIELD = "IsVoid";

		public const string CREATEDBY_FIELD = "CreatedBy";

		public const string DATECREATED_FIELD = "DateCreated";

		public const string UPDATEDBY_FIELD = "UpdatedBy";

		public const string DATEUPDATED_FIELD = "DateUpdated";

		public const string SITE_FIELD = "LocationID";

		public const string EXPENSEID_FIELD = "ExpenseID";

		public const string RATETYPE_FIELD = "RateType";

		public const string PCVOUCHERID_FIELD = "PCVoucherID";

		public const string PCSYSDOCID_FIELD = "PCSysDocID";

		public const string PCROWINDEX_FIELD = "PCRowIndex";

		public const string CURRENCYID_FIELD = "CurrencyID";

		public const string CURRENCYRATE_FIELD = "CurrencyRate";

		public const string PRODUCTID_FIELD = "ProductID";

		public const string QUANTITY_FIELD = "Quantity";

		public const string UNITPRICE_FIELD = "UnitPrice";

		public const string UNITPRICEFC_FIELD = "UnitPriceFC";

		public const string LCOST_FIELD = "LCost";

		public const string LCOSTAMOUNT_FIELD = "LCostAmount";

		public const string UNITID_FIELD = "UnitID";

		public const string UNITQUANTITY_FIELD = "UnitQuantity";

		public const string UNITFACTOR_FIELD = "UnitFactor";

		public const string FACTORTYPE_FIELD = "FactorType";

		public const string SUBUNITPRICE_FIELD = "SubunitPrice";

		public const string LOCATIONID_FIELD = "LocationID";

		public const string QUANTITYSHIPPED_FIELD = "QuantityShipped";

		public const string SOURCEVOUCHERID_FIELD = "SourceVoucherID";

		public const string SOURCESYSDOCID_FIELD = "SourceSysDocID";

		public const string PORVOUCHERID_FIELD = "PORVoucherID";

		public const string PORSYSDOCID_FIELD = "PORSysDocID";

		public const string SOURCEROWINDEX_FIELD = "SourceRowIndex";

		public const string ISPORROW_FIELD = "IsPORRow";

		public const string LOTNUMBER_FIELD = "LotNumber";

		public const string AMOUNT_FIELD = "Amount";

		public const string AMOUNTFC_FIELD = "AmountFC";

		public const string ROWSOURCE_FIELD = "RowSource";

		public const string JOBID_FIELD = "JobID";

		public const string EMPLOYEEID_FIELD = "EmployeeID";

		public const string EMPLOYEENAME_FIELD = "EmployeeName";

		public const string POSITIONID_FIELD = "PositionID";

		public const string NO_FIELD = "Hrs";

		public const string REMARKS_FIELD = "Remarks";

		public DataTable WorkOrderTable => base.Tables["EA_Work_Order"];

		public DataTable Expense_Detail => base.Tables["EA_WorkOrder_Expense_Detail"];

		public DataTable Resources_Detail => base.Tables["EA_WorkOrder_Resources_Detail"];

		public DataTable Manpower_Detail => base.Tables["EA_WorkOrder_ManPower_Detail"];

		public EquipmentWorkOrderData()
		{
			BuildDataTables();
		}

		public EquipmentWorkOrderData(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}

		private void BuildDataTables()
		{
			DataTable dataTable = new DataTable("EA_Work_Order");
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
			columns.Add("TransactionDate", typeof(DateTime));
			columns.Add("Status", typeof(byte));
			columns.Add("Note", typeof(string));
			columns.Add("IsVoid", typeof(bool));
			columns.Add("EquipmentID", typeof(string));
			columns.Add("Reference", typeof(string));
			columns.Add("WorkOrderTypeID", typeof(byte));
			columns.Add("CurrentMeterReading", typeof(string));
			base.Tables.Add(dataTable);
			dataTable = new DataTable("EA_WorkOrder_Resources_Detail");
			columns = dataTable.Columns;
			columns.Add("SysDocID", typeof(string));
			columns.Add("VoucherID", typeof(string));
			columns.Add("ProductID", typeof(string));
			columns.Add("Quantity", typeof(float)).DefaultValue = 0;
			columns.Add("UnitPrice", typeof(decimal)).DefaultValue = 0;
			columns.Add("UnitPriceFC", typeof(decimal));
			columns.Add("Description", typeof(string));
			columns.Add("LCost", typeof(decimal));
			columns.Add("LCostAmount", typeof(decimal));
			columns.Add("UnitID", typeof(string));
			columns.Add("UnitQuantity", typeof(float));
			columns.Add("UnitFactor", typeof(decimal));
			columns.Add("Amount", typeof(decimal)).DefaultValue = 0;
			columns.Add("AmountFC", typeof(decimal)).DefaultValue = 0;
			columns.Add("FactorType", typeof(string));
			columns.Add("JobID", typeof(string));
			columns.Add("LocationID", typeof(string));
			columns.Add("SubunitPrice", typeof(decimal));
			columns.Add("RowIndex", typeof(short));
			columns.Add("SourceVoucherID", typeof(string));
			columns.Add("SourceSysDocID", typeof(string));
			columns.Add("PORVoucherID", typeof(string));
			columns.Add("PORSysDocID", typeof(string));
			columns.Add("SourceRowIndex", typeof(int));
			columns.Add("IsPORRow", typeof(bool));
			columns.Add("LotNumber", typeof(int));
			columns.Add("RowSource", typeof(byte));
			base.Tables.Add(dataTable);
			dataTable = new DataTable("EA_WorkOrder_Expense_Detail");
			columns = dataTable.Columns;
			columns.Add("SysDocID", typeof(string));
			columns.Add("VoucherID", typeof(string));
			columns.Add("ExpenseID", typeof(string));
			columns.Add("Description", typeof(string));
			columns.Add("Amount", typeof(decimal)).DefaultValue = 0;
			columns.Add("AmountFC", typeof(decimal));
			columns.Add("Reference", typeof(string));
			columns.Add("CurrencyID", typeof(string));
			columns.Add("CurrencyRate", typeof(decimal));
			columns.Add("RateType", typeof(string));
			columns.Add("RowIndex", typeof(int));
			columns.Add("PCVoucherID", typeof(string));
			columns.Add("PCSysDocID", typeof(string));
			columns.Add("PCRowIndex", typeof(int));
			base.Tables.Add(dataTable);
			dataTable = new DataTable("EA_WorkOrder_ManPower_Detail");
			columns = dataTable.Columns;
			columns.Add("SysDocID", typeof(string));
			columns.Add("VoucherID", typeof(string));
			columns.Add("EmployeeID", typeof(string));
			columns.Add("EmployeeName", typeof(string));
			columns.Add("PositionID", typeof(string));
			columns.Add("Hrs", typeof(short));
			columns.Add("RowIndex", typeof(short));
			columns.Add("Remarks", typeof(string));
			base.Tables.Add(dataTable);
			InventoryTransactionData.AddProductLotReceivingDetailTable(this);
		}
	}
}
