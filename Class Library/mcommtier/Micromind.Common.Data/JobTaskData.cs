using System;
using System.ComponentModel;
using System.Data;
using System.Runtime.Serialization;

namespace Micromind.Common.Data
{
	[Serializable]
	[DesignerCategory("Code")]
	public class JobTaskData : DataSet
	{
		public const string JOBTASK_TABLE = "Job_Task";

		public const string TASKID_FIELD = "TaskID";

		public const string DESCRIPTION_FIELD = "Description";

		public const string FEEID_FIELD = "FeeID";

		public const string JOBID_FIELD = "JobID";

		public const string STARTDATE_FIELD = "StartDate";

		public const string ENDDATE_FIELD = "EndDate";

		public const string ACTUALSTARTDATE_FIELD = "ActualStartDate";

		public const string ACTUALENDDATE_FIELD = "ActualEndDate";

		public const string ASSIGNEDTOID_FIELD = "AssignedToID";

		public const string TASKGROUPID_FIELD = "TaskGroupID";

		public const string FEEPERCENTAGE_FIELD = "FeePercentage";

		public const string COMPLETEDPERCENTAGE_FIELD = "CompletedPercentage";

		public const string TOTALHOURS_FIELD = "TotalHours";

		public const string STATUS_FIELD = "Status";

		public const string COMPLETEDDESCRIPTION_FIELD = "CompletedDescription";

		public const string NOTE_FIELD = "Note";

		public const string CREATEDBY_FIELD = "CreatedBy";

		public const string DATECREATED_FIELD = "DateCreated";

		public const string UPDATEDBY_FIELD = "UpdatedBy";

		public const string DATEUPDATED_FIELD = "DateUpdated";

		public DataTable JobTaskTable => base.Tables["Job_Task"];

		public JobTaskData()
		{
			BuildDataTables();
		}

		public JobTaskData(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}

		private void BuildDataTables()
		{
			DataTable dataTable = new DataTable("Job_Task");
			DataColumnCollection columns = dataTable.Columns;
			DataColumn dataColumn = columns.Add("TaskID", typeof(string));
			dataColumn.AllowDBNull = false;
			dataColumn.AutoIncrement = false;
			dataTable.PrimaryKey = new DataColumn[1]
			{
				dataColumn
			};
			columns.Add("Description", typeof(string)).AllowDBNull = false;
			columns.Add("FeeID", typeof(string));
			columns.Add("JobID", typeof(string));
			columns.Add("StartDate", typeof(DateTime));
			columns.Add("EndDate", typeof(DateTime));
			columns.Add("ActualStartDate", typeof(DateTime));
			columns.Add("ActualEndDate", typeof(DateTime));
			columns.Add("AssignedToID", typeof(string));
			columns.Add("FeePercentage", typeof(decimal));
			columns.Add("TotalHours", typeof(decimal));
			columns.Add("CompletedPercentage", typeof(decimal));
			columns.Add("Status", typeof(byte));
			columns.Add("CompletedDescription", typeof(string));
			columns.Add("Note", typeof(string));
			columns.Add("TaskGroupID", typeof(string));
			base.Tables.Add(dataTable);
		}
	}
}
