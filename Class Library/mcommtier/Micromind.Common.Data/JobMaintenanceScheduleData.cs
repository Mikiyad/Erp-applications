using System;
using System.ComponentModel;
using System.Data;
using System.Runtime.Serialization;

namespace Micromind.Common.Data
{
	[Serializable]
	[DesignerCategory("Code")]
	public class JobMaintenanceScheduleData : DataSet
	{
		public const string SYSDOCID_FIELD = "SysDocID";

		public const string VOUCHERID_FIELD = "VoucherID";

		public const string TRANSACTIONDATE_FIELD = "TransactionDate";

		public const string REFERENCE_FIELD = "Reference";

		public const string JOBID_FIELD = "JobID";

		public const string REFERENCE2_FIELD = "Reference2";

		public const string DESCRIPTION_FIELD = "Description";

		public const string JOBMAINTENANCESCHEDULE_TABLE = "Job_Maintenance_Schedule";

		public const string LOCATIONID_FIELD = "LocationID";

		public const string CREATEDBY_FIELD = "CreatedBy";

		public const string DATECREATED_FIELD = "DateCreated";

		public const string UPDATEDBY_FIELD = "UpdatedBy";

		public const string DATEUPDATED_FIELD = "DateUpdated";

		public const string ASSETID_FIELD = "AssetID";

		public const string ROWINDEX_FIELD = "RowIndex";

		public const string ACTIVITYID_FIELD = "ActivityID";

		public const string STARTDATE_FIELD = "StartDate";

		public const string ENDDATE_FIELD = "EndDate";

		public const string SCHEDULEDON_FIELD = "ScheduledOn";

		public const string NEXTSCHEDULEDATE_FIELD = "NextScheduleDate";

		public const string JOBMAINTENANCESCHEDULEDETAIL_TABLE = "Job_Maintenance_Schedule_Detail";

		public DataTable JobMaintenanceScheduleTable => base.Tables["Job_Maintenance_Schedule"];

		public DataTable JobMaintenanceScheduleDetailTable => base.Tables["Job_Maintenance_Schedule_Detail"];

		public JobMaintenanceScheduleData()
		{
			BuildDataTables();
		}

		public JobMaintenanceScheduleData(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}

		private void BuildDataTables()
		{
			DataTable dataTable = new DataTable("Job_Maintenance_Schedule");
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
			columns.Add("Reference", typeof(string));
			columns.Add("LocationID", typeof(string));
			columns.Add("JobID", typeof(string));
			columns.Add("StartDate", typeof(DateTime));
			columns.Add("EndDate", typeof(DateTime));
			columns.Add("Reference2", typeof(string));
			columns.Add("Description", typeof(string));
			base.Tables.Add(dataTable);
			dataTable = new DataTable("Job_Maintenance_Schedule_Detail");
			columns = dataTable.Columns;
			columns.Add("SysDocID", typeof(string));
			columns.Add("VoucherID", typeof(string));
			columns.Add("AssetID", typeof(string));
			columns.Add("RowIndex", typeof(short));
			columns.Add("ActivityID", typeof(string));
			columns.Add("StartDate", typeof(DateTime));
			columns.Add("EndDate", typeof(DateTime));
			columns.Add("ScheduledOn", typeof(string));
			columns.Add("NextScheduleDate", typeof(DateTime));
			base.Tables.Add(dataTable);
		}
	}
}
