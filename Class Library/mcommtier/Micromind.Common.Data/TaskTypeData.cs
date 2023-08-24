using System;
using System.ComponentModel;
using System.Data;
using System.Runtime.Serialization;

namespace Micromind.Common.Data
{
	[Serializable]
	[DesignerCategory("Code")]
	public class TaskTypeData : DataSet
	{
		public const string TASKTYPEID_FIELD = "TaskTypeID";

		public const string TASKTYPENAME_FIELD = "Name";

		public const string ISINACTIVE_FIELD = "IsInactive";

		public const string NOTE_FIELD = "Note";

		public const string TASKTYPE_TABLE = "Task_Type";

		public const string TASKTYPEDETAIL_TABLE = "Task_Type_Detail";

		public const string TASKSTEPID_FIELD = "TaskStepID";

		public static string DEFAULTASSIGNEEID_FIELD = "DefaultAssigneeID";

		public static string DESCRIPTION_FIELD = "Description";

		public static string DAYSALLOWED_FIELD = "DaysAllowed";

		public static string PREREQUEST_FIELD = "PreRequest";

		public static string DOCTYPEID_FIELD = "DocTypeID";

		public static string DOCTYPENAME_FIELD = "DocTypeName";

		public const string ROWINDEX_FIELD = "RowIndex";

		public const string CREATEDBY_FIELD = "CreatedBy";

		public const string DATECREATED_FIELD = "DateCreated";

		public const string UPDATEDBY_FIELD = "UpdatedBy";

		public const string DATEUPDATED_FIELD = "DateUpdated";

		public DataTable TaskTypeTable => base.Tables["Task_Type"];

		public DataTable TaskTypeDetailTable => base.Tables["Task_Type_Detail"];

		public TaskTypeData()
		{
			BuildDataTables();
		}

		public TaskTypeData(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}

		private void BuildDataTables()
		{
			DataTable dataTable = new DataTable("Task_Type");
			DataColumnCollection columns = dataTable.Columns;
			DataColumn dataColumn = columns.Add("TaskTypeID", typeof(string));
			dataColumn.AllowDBNull = false;
			dataColumn.AutoIncrement = false;
			dataTable.PrimaryKey = new DataColumn[1]
			{
				dataColumn
			};
			columns.Add("Name", typeof(string));
			columns.Add("IsInactive", typeof(bool)).DefaultValue = false;
			columns.Add("Note", typeof(string));
			base.Tables.Add(dataTable);
			dataTable = new DataTable("Task_Type_Detail");
			columns = dataTable.Columns;
			columns.Add("TaskTypeID", typeof(string));
			columns.Add("TaskStepID", typeof(string));
			columns.Add(DESCRIPTION_FIELD, typeof(string));
			columns.Add(DEFAULTASSIGNEEID_FIELD, typeof(string));
			columns.Add(DAYSALLOWED_FIELD, typeof(int));
			columns.Add(PREREQUEST_FIELD, typeof(string));
			columns.Add(DOCTYPEID_FIELD, typeof(string));
			columns.Add(DOCTYPENAME_FIELD, typeof(string));
			columns.Add("RowIndex", typeof(short)).AllowDBNull = false;
			base.Tables.Add(dataTable);
		}
	}
}
