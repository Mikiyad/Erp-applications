using System;
using System.ComponentModel;
using System.Data;
using System.Runtime.Serialization;

namespace Micromind.Common.Data
{
	[Serializable]
	[DesignerCategory("Code")]
	public class JobTaskGroupData : DataSet
	{
		public const string TASKGROUP_TABLE = "Job_Task_Group";

		public const string TASKGROUPID_FIELD = "TaskGroupID";

		public const string TASKGROUPNAME_FIELD = "TaskGroupName";

		public const string TASKGROUPEDESC_FIELD = "Description";

		public const string INACTIVE_FIELD = "Inactive";

		public const string CREATEDBY_FIELD = "CreatedBy";

		public const string DATECREATED_FIELD = "DateCreated";

		public const string UPDATEDBY_FIELD = "UpdatedBy";

		public const string DATEUPDATED_FIELD = "DateUpdated";

		public DataTable JobTaskGroupTable => base.Tables["Job_Task_Group"];

		public JobTaskGroupData()
		{
			BuildDataTables();
		}

		public JobTaskGroupData(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}

		private void BuildDataTables()
		{
			DataTable dataTable = new DataTable("Job_Task_Group");
			DataColumnCollection columns = dataTable.Columns;
			DataColumn dataColumn = columns.Add("TaskGroupID", typeof(string));
			dataColumn.AllowDBNull = false;
			dataColumn.AutoIncrement = false;
			dataTable.PrimaryKey = new DataColumn[1]
			{
				dataColumn
			};
			columns.Add("TaskGroupName", typeof(string)).AllowDBNull = false;
			columns.Add("Description", typeof(string));
			columns.Add("Inactive", typeof(bool));
			base.Tables.Add(dataTable);
		}
	}
}
