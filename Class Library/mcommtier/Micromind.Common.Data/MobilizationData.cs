using System;
using System.ComponentModel;
using System.Data;
using System.Runtime.Serialization;

namespace Micromind.Common.Data
{
	[Serializable]
	[DesignerCategory("Code")]
	public class MobilizationData : DataSet
	{
		public const string MOBILIZATION_TABLE = "EA_Mobilization";

		public const string MOBILIZATIONEQUPMENTDETAIL_TABLE = "EA_Mobilization_Equipment__Detail";

		public const string MOBILIZATIONRESOURCESDETAIL_TABLE = "EA_Mobilization_Resources__Detail";

		public const string MOBILIZATIONMANPOWERDETAIL_TABLE = "EA_Mobilization_Manpower__Detail";

		public const string SYSDOCID_FIELD = "SysDocID";

		public const string VOUCHERID_FIELD = "VoucherID";

		public const string TRANSACTIONDATE_FIELD = "TransactionDate";

		public const string PLANNEDDATEFROM_FIELD = "PlannedDateFrom";

		public const string PLANNEDDATETO_FIELD = "PlannedDateTo";

		public const string ROWINDEX_FIELD = "RowIndex";

		public const string STATUS_FIELD = "Status";

		public const string NOTE_FIELD = "Note";

		public const string ISVOID_FIELD = "IsVoid";

		public const string DISCOUNT_FIELD = "Discount";

		public const string DISCOUNTFC_FIELD = "DiscountFC";

		public const string TAXAMOUNT_FIELD = "TaxAmount";

		public const string TAXAMOUNTFC_FIELD = "TaxAmountFC";

		public const string TOTAL_FIELD = "Total";

		public const string TOTALFC_FIELD = "TotalFC";

		public const string REQUISITIONNUMBER_FIELD = "RequisitionNumber";

		public const string SOURCEVOUCHERID_FIELD = "SourceVoucherID";

		public const string SOURCESYSDOCID_FIELD = "SourceSysDocID";

		public const string SOURCEROWINDEX_FIELD = "SourceRowIndex";

		public const string ISSOURCEDROW_FIELD = "IsSourcedRow";

		public const string CREATEDBY_FIELD = "CreatedBy";

		public const string DATECREATED_FIELD = "DateCreated";

		public const string UPDATEDBY_FIELD = "UpdatedBy";

		public const string DATEUPDATED_FIELD = "DateUpdated";

		public const string EQUIPMENTID_FIELD = "EquipmentID";

		public const string SITE_FIELD = "LocationID";

		public const string PRODUCTID_FIELD = "ProductID";

		public const string QUANTITY_FIELD = "Quantity";

		public const string UNITPRICE_FIELD = "UnitPrice";

		public const string UNITPRICEFC_FIELD = "UnitPriceFC";

		public const string DESCRIPTION_FIELD = "Description";

		public const string LCOST_FIELD = "LCost";

		public const string LCOSTAMOUNT_FIELD = "LCostAmount";

		public const string UNITID_FIELD = "UnitID";

		public const string UNITQUANTITY_FIELD = "UnitQuantity";

		public const string UNITFACTOR_FIELD = "UnitFactor";

		public const string FACTORTYPE_FIELD = "FactorType";

		public const string SUBUNITPRICE_FIELD = "SubunitPrice";

		public const string LOCATIONID_FIELD = "LocationID";

		public const string QUANTITYSHIPPED_FIELD = "QuantityShipped";

		public const string ORDERVOUCHERID_FIELD = "OrderVoucherID";

		public const string ORDERSYSDOCID_FIELD = "OrderSysDocID";

		public const string PORVOUCHERID_FIELD = "PORVoucherID";

		public const string PORSYSDOCID_FIELD = "PORSysDocID";

		public const string ORDERROWINDEX_FIELD = "OrderRowIndex";

		public const string ISPORROW_FIELD = "IsPORRow";

		public const string LOTNUMBER_FIELD = "LotNumber";

		public const string AMOUNT_FIELD = "Amount";

		public const string AMOUNTFC_FIELD = "AmountFC";

		public const string ROWSOURCE_FIELD = "RowSource";

		public const string JOBID_FIELD = "JobID";

		public const string EMPLOYEEID_FIELD = "EmployeeID";

		public const string EMPLOYEENAME_FIELD = "EmployeeName";

		public const string POSITIONID_FIELD = "PositionID";

		public const string NO_FIELD = "NoOfMembers";

		public const string REMARKS_FIELD = "Remarks";

		public DataTable MobilizationTable => base.Tables["EA_Mobilization"];

		public DataTable Equipment_Detail => base.Tables["EA_Mobilization_Equipment__Detail"];

		public DataTable Resources_Detail => base.Tables["EA_Mobilization_Resources__Detail"];

		public DataTable Manpower_Detail => base.Tables["EA_Mobilization_Manpower__Detail"];

		public MobilizationData()
		{
			BuildDataTables();
		}

		public MobilizationData(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}

		private void BuildDataTables()
		{
			DataTable dataTable = new DataTable("EA_Mobilization");
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
			columns.Add("Discount", typeof(decimal));
			columns.Add("DiscountFC", typeof(decimal));
			columns.Add("TaxAmount", typeof(decimal));
			columns.Add("TaxAmountFC", typeof(decimal));
			columns.Add("Total", typeof(decimal));
			columns.Add("TotalFC", typeof(decimal));
			columns.Add("RequisitionNumber", typeof(string));
			columns.Add("PlannedDateFrom", typeof(DateTime));
			columns.Add("PlannedDateTo", typeof(DateTime));
			base.Tables.Add(dataTable);
			dataTable = new DataTable("EA_Mobilization_Resources__Detail");
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
			columns.Add("OrderVoucherID", typeof(string));
			columns.Add("OrderSysDocID", typeof(string));
			columns.Add("PORVoucherID", typeof(string));
			columns.Add("PORSysDocID", typeof(string));
			columns.Add("OrderRowIndex", typeof(int));
			columns.Add("IsPORRow", typeof(bool));
			columns.Add("LotNumber", typeof(int));
			columns.Add("RowSource", typeof(byte));
			base.Tables.Add(dataTable);
			dataTable = new DataTable("EA_Mobilization_Equipment__Detail");
			columns = dataTable.Columns;
			columns.Add("SysDocID", typeof(string));
			columns.Add("VoucherID", typeof(string));
			columns.Add("RequisitionNumber", typeof(string));
			columns.Add("EquipmentID", typeof(string));
			columns.Add("LocationID", typeof(string));
			columns.Add("JobID", typeof(string));
			columns.Add("Status", typeof(byte));
			columns.Add("Remarks", typeof(string));
			columns.Add("RowIndex", typeof(short));
			columns.Add("SourceVoucherID", typeof(string));
			columns.Add("SourceSysDocID", typeof(string));
			columns.Add("SourceRowIndex", typeof(short));
			columns.Add("IsSourcedRow", typeof(bool));
			base.Tables.Add(dataTable);
			dataTable = new DataTable("EA_Mobilization_Manpower__Detail");
			columns = dataTable.Columns;
			columns.Add("SysDocID", typeof(string));
			columns.Add("VoucherID", typeof(string));
			columns.Add("EmployeeID", typeof(string));
			columns.Add("EmployeeName", typeof(string));
			columns.Add("PositionID", typeof(string));
			columns.Add("NoOfMembers", typeof(short));
			columns.Add("RowIndex", typeof(short));
			columns.Add("Remarks", typeof(string));
			base.Tables.Add(dataTable);
			InventoryTransactionData.AddProductLotReceivingDetailTable(this);
		}
	}
}
