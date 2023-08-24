using System;
using System.ComponentModel;
using System.Data;
using System.Runtime.Serialization;

namespace Micromind.Common.Data
{
	[Serializable]
	[DesignerCategory("Code")]
	public class MaintenanceSchedulerData : DataSet
	{
		public const string SYSDOCID_FIELD = "SysDocID";

		public const string DOCID_FIELD = "VoucherID";

		public const string CREATEDBY_FIELD = "CreatedBy";

		public const string DATECREATED_FIELD = "DateCreated";

		public const string UPDATEDBY_FIELD = "UpdatedBy";

		public const string DATEUPDATED_FIELD = "DateUpdated";

		public const string VEHICLENUMBER_FIELD = "VehicleNumber";

		public const string MAINTENANCEDATE_FIELD = "MaintenanceDate";

		public const string ODOMETER_FIELD = "Odometer";

		public const string SERVICETYPE_FIELD = "ServiceType";

		public const string ISVOID_FIELD = "IsVoid";

		public const string SERVICEPROVIDER_FIELD = "ServiceProvider";

		public const string AMOUNT_FIELD = "Amount";

		public const string REQUIREDTIME_FIELD = "TimeRequired";

		public const string TRACKMAINTENANCE_FIELD = "TrackMaintenance";

		public const string STATUS_FIELD = "Status";

		public const string NOTE_FIELD = "Note";

		public const string MAINTENANCESCHEDULER_TABLE = "Vehicle_Maintenance_Scheduler";

		public DataTable MaintenanceSchedulerTable => base.Tables["Vehicle_Maintenance_Scheduler"];

		public MaintenanceSchedulerData()
		{
			BuildDataTables();
		}

		public MaintenanceSchedulerData(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}

		private void BuildDataTables()
		{
			DataTable dataTable = new DataTable("Vehicle_Maintenance_Scheduler");
			DataColumnCollection columns = dataTable.Columns;
			DataColumn dataColumn = columns.Add("VoucherID", typeof(string));
			dataColumn.AllowDBNull = false;
			DataColumn dataColumn2 = columns.Add("SysDocID", typeof(string));
			dataColumn2.AllowDBNull = false;
			dataTable.PrimaryKey = new DataColumn[2]
			{
				dataColumn,
				dataColumn2
			};
			columns.Add("CreatedBy", typeof(string));
			columns.Add("DateCreated", typeof(DateTime));
			columns = dataTable.Columns;
			columns.Add("VehicleNumber", typeof(string));
			columns.Add("MaintenanceDate", typeof(DateTime));
			columns.Add("Odometer", typeof(string));
			columns.Add("ServiceType", typeof(string));
			columns.Add("ServiceProvider", typeof(string));
			columns.Add("Amount", typeof(string));
			columns.Add("TrackMaintenance", typeof(string));
			columns.Add("TimeRequired", typeof(string));
			columns.Add("Status", typeof(string));
			columns.Add("DateUpdated", typeof(DateTime));
			columns.Add("IsVoid", typeof(string));
			columns.Add("Note", typeof(string));
			base.Tables.Add(dataTable);
		}
	}
}
