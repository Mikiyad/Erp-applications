using System;
using System.ComponentModel;
using System.Data;
using System.Runtime.Serialization;

namespace Micromind.Common.Data
{
	[Serializable]
	[DesignerCategory("Code")]
	public class TaskStepsData : DataSet
	{
		public const string TASKSTEPID_FIELD = "TaskStepID";

		public const string TASKSTEPNAME_FIELD = "Name";

		public const string TASKTYPEID_FIELD = "TaskTypeID";

		public const string ISINACTIVE_FIELD = "IsInactive";

		public const string NOTE_FIELD = "Note";

		public const string TASKSTEPS_TABLE = "Task_Steps";

		public const string CREATEDBY_FIELD = "CreatedBy";

		public const string DATECREATED_FIELD = "DateCreated";

		public const string UPDATEDBY_FIELD = "UpdatedBy";

		public const string DATEUPDATED_FIELD = "DateUpdated";

		public DataTable TaskStepsTable => base.Tables["Task_Steps"];

		public TaskStepsData()
		{
			BuildDataTables();
		}

		public TaskStepsData(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}

		private void BuildDataTables()
		{
			DataTable dataTable = new DataTable("Task_Steps");
			DataColumnCollection columns = dataTable.Columns;
			DataColumn dataColumn = columns.Add("TaskStepID", typeof(string));
			dataColumn.AllowDBNull = false;
			dataColumn.AutoIncrement = false;
			dataTable.PrimaryKey = new DataColumn[1]
			{
				dataColumn
			};
			columns.Add("Name", typeof(string));
			columns.Add("TaskTypeID", typeof(string));
			columns.Add("IsInactive", typeof(bool)).DefaultValue = false;
			columns.Add("Note", typeof(string));
			base.Tables.Add(dataTable);
		}
	}
}
