using System;
using System.ComponentModel;
using System.Data;
using System.Runtime.Serialization;

namespace Micromind.Common.Data
{
	[Serializable]
	[DesignerCategory("Code")]
	public class CheckListData : DataSet
	{
		public const string CHECKLIST_TABLE = "CheckList";

		public const string CHECKLISTID_FIELD = "CheckListID";

		public const string CHECKLISTNAME_FIELD = "CheckListName";

		public const string CHECKLISTTYPE_FIELD = "CheckListType";

		public const string INTERVAL_FIELD = "Interval";

		public const string DEADLINEDAYS_FIELD = "DeadlineDays";

		public const string DEADLINEDATE_FIELD = "DeadlineDate";

		public const string STARTDATE_FIELD = "StartDate";

		public const string STATUS_FIELD = "Status";

		public const string INACTIVE_FIELD = "IsInactive";

		public const string CREATEDBY_FIELD = "CreatedBy";

		public const string DATECREATED_FIELD = "DateCreated";

		public const string UPDATEDBY_FIELD = "UpdatedBy";

		public const string DATEUPDATED_FIELD = "DateUpdated";

		public const string DUEDATE_FIELD = "DueDate";

		public const string APPROVERTYPE_FIELD = "ApproverType";

		public const string APPROVERID_FIELD = "ApproverID";

		public const string CHECKLISTTASK_TABLE = "CheckList_Task";

		public const string TASKID_FIELD = "TaskID";

		public const string ASSIGNEETYPE_FIELD = "AssigneeType";

		public const string ASSIGNEEID_FIELD = "AssigneeID";

		public const string DATECOMPLETED_FIELD = "DateCompleted";

		public const string COMPLETEDBY_FIELD = "CompletedBye";

		public DataTable CheckListTable => base.Tables["CheckList"];

		public DataTable CheckListTaskTable => base.Tables["CheckList_Task"];

		public CheckListData()
		{
			BuildDataTables();
		}

		public CheckListData(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}

		private void BuildDataTables()
		{
			DataTable dataTable = new DataTable("CheckList");
			DataColumnCollection columns = dataTable.Columns;
			DataColumn dataColumn = columns.Add("CheckListID", typeof(string));
			dataColumn.AllowDBNull = false;
			dataColumn.AutoIncrement = false;
			DataColumn dataColumn2 = columns.Add("CheckListType", typeof(byte));
			dataColumn2.AllowDBNull = false;
			dataTable.PrimaryKey = new DataColumn[2]
			{
				dataColumn,
				dataColumn2
			};
			columns.Add("CheckListName", typeof(string)).AllowDBNull = false;
			columns.Add("ApproverType", typeof(byte));
			columns.Add("ApproverID", typeof(string));
			columns.Add("Interval", typeof(byte));
			columns.Add("DeadlineDays", typeof(byte));
			columns.Add("StartDate", typeof(DateTime));
			columns.Add("Status", typeof(string));
			columns.Add("IsInactive", typeof(bool));
			base.Tables.Add(dataTable);
			dataTable = new DataTable("CheckList_Task");
			columns = dataTable.Columns;
			columns.Add("TaskID", typeof(int)).AllowDBNull = true;
			columns.Add("CheckListType", typeof(byte));
			columns.Add("Status", typeof(byte));
			columns.Add("AssigneeType", typeof(byte));
			columns.Add("AssigneeID", typeof(string));
			columns.Add("DateCreated", typeof(DateTime));
			columns.Add("DeadlineDate", typeof(DateTime));
			columns.Add("DueDate", typeof(DateTime));
			columns.Add("CheckListID", typeof(string));
			base.Tables.Add(dataTable);
		}
	}
}
