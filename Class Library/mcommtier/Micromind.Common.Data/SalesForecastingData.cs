using System;
using System.ComponentModel;
using System.Data;
using System.Runtime.Serialization;

namespace Micromind.Common.Data
{
	[Serializable]
	[DesignerCategory("Code")]
	public class SalesForecastingData : DataSet
	{
		public const string SALESFORECASTING_TABLE = "Sales_Forecasting";

		public const string SALESFORECASTINGDETAIL_TABLE = "Sales_Forecasting_Detail";

		public const string SYSDOCID_FIELD = "SysDocID";

		public const string VOUCHERID_FIELD = "VoucherID";

		public const string TRANSACTIONDATE_FIELD = "TransactionDate";

		public const string ROWINDEX_FIELD = "RowIndex";

		public const string STATUS_FIELD = "Status";

		public const string PERIOD_FIELD = "Period";

		public const string NOTE_FIELD = "Note";

		public const string ISVOID_FIELD = "IsVoid";

		public const string LOCATIONFROM_FIELD = "LocationFrom";

		public const string FROMDATE_FIELD = "FromDate";

		public const string TODATE_FIELD = "ToDate";

		public const string QUANTITY_FIELD = "Quantity";

		public const string UNIT_FIELD = "UnitID";

		public const string ROWSOURCE_FIELD = "RowSource";

		public const string SOURCEVOUCHERID_FIELD = "SourceVoucherID";

		public const string SOURCESYSDOCID_FIELD = "SourceSysDocID";

		public const string SOURCEROWINDEX_FIELD = "SourceRowIndex";

		public const string CALCULATIONMETHOD_FIELD = "CalculationMethod";

		public const string CREATEDBY_FIELD = "CreatedBy";

		public const string DATECREATED_FIELD = "DateCreated";

		public const string UPDATEDBY_FIELD = "UpdatedBy";

		public const string DATEUPDATED_FIELD = "DateUpdated";

		public const string PRODUCTID_FIELD = "ProductID";

		public const string DESCRIPTION_FIELD = "Description";

		public DataTable SalesForecastingTable => base.Tables["Sales_Forecasting"];

		public DataTable SalesForecastingDetailTable => base.Tables["Sales_Forecasting_Detail"];

		public SalesForecastingData()
		{
			BuildDataTables();
		}

		public SalesForecastingData(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}

		private void BuildDataTables()
		{
			DataTable dataTable = new DataTable("Sales_Forecasting");
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
			columns.Add("Status", typeof(byte)).DefaultValue = 1;
			columns.Add("Note", typeof(string));
			columns.Add("LocationFrom", typeof(string));
			columns.Add("FromDate", typeof(DateTime)).DefaultValue = DateTime.Now;
			columns.Add("ToDate", typeof(DateTime)).DefaultValue = DateTime.Now;
			columns.Add("IsVoid", typeof(bool));
			columns.Add("CalculationMethod", typeof(byte));
			columns.Add("Period", typeof(byte));
			base.Tables.Add(dataTable);
			dataTable = new DataTable("Sales_Forecasting_Detail");
			columns = dataTable.Columns;
			columns.Add("SysDocID", typeof(string));
			columns.Add("VoucherID", typeof(string));
			columns.Add("ProductID", typeof(string));
			columns.Add("SourceVoucherID", typeof(string));
			columns.Add("SourceSysDocID", typeof(string));
			columns.Add("SourceRowIndex", typeof(int));
			columns.Add("RowSource", typeof(short));
			columns.Add("Description", typeof(string));
			columns.Add("Quantity", typeof(float)).DefaultValue = 0;
			columns.Add("UnitID", typeof(string));
			columns.Add("RowIndex", typeof(short));
			base.Tables.Add(dataTable);
		}
	}
}
