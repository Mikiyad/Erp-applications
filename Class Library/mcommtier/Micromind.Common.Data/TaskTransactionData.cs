using System;
using System.ComponentModel;
using System.Data;
using System.Runtime.Serialization;

namespace Micromind.Common.Data
{
	[Serializable]
	[DesignerCategory("Code")]
	public class TaskTransactionData : DataSet
	{
		public const string SYSDOCID_FIELD = "SysDocID";

		public const string DOCID_FIELD = "VoucherID";

		public const string TASKTYPEID_FIELD = "TaskTypeID";

		public const string NAME_FIELD = "Task_Name";

		public const string TRANSACTIONDATE_FIELD = "TransactionDate";

		public const string ASSIGNEDSYSDOCID_FIELD = "AssignedSysDocID";

		public const string ASSIGNEDDOCID_FIELD = "AssignedVouherID";

		public const string STARTDATE_FIELD = "StartDate";

		public const string DEADLINE_FIELD = "DeadLine";

		public const string TASKSTEPID_FIELD = "TaskStepID";

		public const string TASKNAME_FIELD = "TaskName";

		public static string DEFAULTASSIGNEEID_FIELD = "DefaultAssigneeID";

		public static string DESCRIPTION_FIELD = "Description";

		public static string DAYSALLOWED_FIELD = "DaysAllowed";

		public static string PREREQUEST_FIELD = "PreRequest";

		public static string DOCTYPE_FIELD = "DocType";

		public const string STATUS_FIELD = "Status";

		public const string ROWINDEX_FIELD = "RowIndex";

		public const string ISVOID_FIELD = "IsVoid";

		public const string TASKTRANSACTION_TABLE = "Task_Transaction";

		public const string TASKTRANSACTIONDETAIL_TABLE = "Task_Transaction_Detail";

		public const string CREATEDBY_FIELD = "CreatedBy";

		public const string DATECREATED_FIELD = "DateCreated";

		public const string UPDATEDBY_FIELD = "UpdatedBy";

		public const string DATEUPDATED_FIELD = "DateUpdated";

		public DataTable TaskTransactionTable => base.Tables["Task_Transaction"];

		public DataTable TaskTransactionDetailTable => base.Tables["Task_Transaction_Detail"];

		public TaskTransactionData()
		{
			BuildDataTables();
		}

		public TaskTransactionData(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}

		private void BuildDataTables()
		{
			DataTable dataTable = new DataTable("Task_Transaction");
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
			columns.Add("TaskTypeID", typeof(string));
			columns.Add("TaskName", typeof(string));
			columns.Add(DOCTYPE_FIELD, typeof(int));
			columns.Add(DESCRIPTION_FIELD, typeof(string));
			columns.Add("StartDate", typeof(DateTime));
			columns.Add("AssignedSysDocID", typeof(string));
			columns.Add("AssignedVouherID", typeof(string));
			columns.Add("TransactionDate", typeof(DateTime));
			columns.Add("IsVoid", typeof(string));
			base.Tables.Add(dataTable);
			dataTable = new DataTable("Task_Transaction_Detail");
			columns = dataTable.Columns;
			columns.Add("SysDocID", typeof(string));
			columns.Add("VoucherID", typeof(string));
			columns.Add("TaskStepID", typeof(string));
			columns.Add(DESCRIPTION_FIELD, typeof(string));
			columns.Add(DEFAULTASSIGNEEID_FIELD, typeof(string));
			columns.Add("StartDate", typeof(DateTime));
			columns.Add("DeadLine", typeof(DateTime));
			columns.Add(DAYSALLOWED_FIELD, typeof(int));
			columns.Add("Status", typeof(byte));
			columns.Add(DOCTYPE_FIELD, typeof(int));
			columns.Add(PREREQUEST_FIELD, typeof(string));
			columns.Add("RowIndex", typeof(short));
			base.Tables.Add(dataTable);
		}
	}
}
