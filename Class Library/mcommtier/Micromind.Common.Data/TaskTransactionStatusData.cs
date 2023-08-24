using System;
using System.ComponentModel;
using System.Data;
using System.Runtime.Serialization;

namespace Micromind.Common.Data
{
	[Serializable]
	[DesignerCategory("Code")]
	public class TaskTransactionStatusData : DataSet
	{
		public const string TASKTRANSACTIONSTATUS_TABLE = "Task_Transaction_Status";

		public const string SYSDOCID_FIELD = "SysDocID";

		public const string VOUCHERID_FIELD = "VoucherID";

		public const string TASKNAME_FIELD = "TaskName";

		public const string TASKSTEPID_FIELD = "TaskStepID";

		public const string STATUS_FIELD = "Status";

		public const string ISVOID_FIELD = "IsVoid";

		public const string TRANSACTIONDATE_FIELD = "TransactionDate";

		public const string SOURCESYSDOCID_FIELD = "SourceSysDocID";

		public const string SOURCEVOUCHERID_FIELD = "SourceVoucherID";

		public const string SOURCEROWINDEX_FIELD = "SourceRowIndex";

		public const string TRSYSDOCID_FIELD = "TRSysDocID";

		public const string TRVOUCHERID_FIELD = "TRVoucherID";

		public const string REMARKS_FIELD = "Remarks";

		public const string MESSAGE_FIELD = "Message";

		public const string CREATEDBY_FIELD = "CreatedBy";

		public const string DATECREATED_FIELD = "DateCreated";

		public const string UPDATEDBY_FIELD = "UpdatedBy";

		public const string DATEUPDATED_FIELD = "DateUpdated";

		public DataTable TaskTransactionStatusTable => base.Tables["Task_Transaction_Status"];

		public TaskTransactionStatusData()
		{
			BuildDataTables();
		}

		public TaskTransactionStatusData(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}

		private void BuildDataTables()
		{
			DataTable dataTable = new DataTable("Task_Transaction_Status");
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
			columns.Add("TaskName", typeof(string));
			columns.Add("TaskStepID", typeof(string));
			columns.Add("Status", typeof(string));
			columns.Add("IsVoid", typeof(string));
			columns.Add("TransactionDate", typeof(string));
			columns.Add("SourceSysDocID", typeof(string));
			columns.Add("SourceVoucherID", typeof(string));
			columns.Add("SourceRowIndex", typeof(string));
			columns.Add("TRSysDocID", typeof(string));
			columns.Add("TRVoucherID", typeof(string));
			columns.Add("Remarks", typeof(string));
			columns.Add("Message", typeof(string));
			base.Tables.Add(dataTable);
		}
	}
}
