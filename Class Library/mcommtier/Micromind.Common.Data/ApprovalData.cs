using System;
using System.ComponentModel;
using System.Data;
using System.Runtime.Serialization;

namespace Micromind.Common.Data
{
	[Serializable]
	[DesignerCategory("Code")]
	public class ApprovalData : DataSet
	{
		public const string APPROVAL_TABLE = "Approval";

		public const string APPROVALID_FIELD = "ApprovalID";

		public const string APPROVALNAME_FIELD = "ApprovalName";

		public const string APPROVALTYPE_FIELD = "ApprovalType";

		public const string OBJECTTYPE_FIELD = "ObjectType";

		public const string OBJECTID_FIELD = "ObjectID";

		public const string OBJECTSYSDOCID_FIELD = "ObjectSysDocID";

		public const string STATUS_FIELD = "Status";

		public const string UPDATEFIELDNAME1_FIELD = "UpdateFieldName1";

		public const string UPDATEFIELDVALUE1_FIELD = "UpdateFieldValue1";

		public const string UPDATEFIELDNAME2_FIELD = "UpdateFieldName2";

		public const string UPDATEFIELDVALUE2_FIELD = "UpdateFieldValue2";

		public const string ACTIONSETINACTIVE_FIELD = "ActionSetInactive";

		public const string INACTIVE_FIELD = "IsInactive";

		public const string NOTIFYONPRINT_FIELD = "NotifyonPrint";

		public const string ALLOWNEXTTRANSACTION_FIELD = "AllownextTransaction";

		public const string ALLOWTOEDIT_FIELD = "AllowtoEdit";

		public const string CREATEDBY_FIELD = "CreatedBy";

		public const string DATECREATED_FIELD = "DateCreated";

		public const string UPDATEDBY_FIELD = "UpdatedBy";

		public const string DATEUPDATED_FIELD = "DateUpdated";

		public const string APPROVALLEVEL_TABLE = "Approval_Level";

		public const string ROWINDEX_FIELD = "RowIndex";

		public const string APPROVERTYPE_FIELD = "ApproverType";

		public const string APPROVERID_FIELD = "ApproverID";

		public const string PREREQUISITEINDEX_FIELD = "PreRequisiteIndex";

		public const string CONDITION_FIELD = "Condition";

		public const string APPROVALTASK_TABLE = "Approval_Task";

		public const string TASKID_FIELD = "TaskID";

		public const string LEVELID_FIELD = "LevelID";

		public const string ASSIGNEETYPE_FIELD = "AssigneeType";

		public const string ASSIGNEEID_FIELD = "AssigneeID";

		public const string DOCUMENTSYSDOCID_FIELD = "DocumentSysDocID";

		public const string DOCUMENTCODE_FIELD = "DocumentCode";

		public const string DATEAPPROVED_FIELD = "DateApproved";

		public DataTable ApprovalTable => base.Tables["Approval"];

		public DataTable ApprovalTaskTable => base.Tables["Approval_Task"];

		public DataTable ApprovalLevelTable => base.Tables["Approval_Level"];

		public ApprovalData()
		{
			BuildDataTables();
		}

		public ApprovalData(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}

		private void BuildDataTables()
		{
			DataTable dataTable = new DataTable("Approval");
			DataColumnCollection columns = dataTable.Columns;
			DataColumn dataColumn = columns.Add("ApprovalID", typeof(string));
			dataColumn.AllowDBNull = false;
			dataColumn.AutoIncrement = false;
			DataColumn dataColumn2 = columns.Add("ApprovalType", typeof(byte));
			dataColumn2.AllowDBNull = false;
			dataTable.PrimaryKey = new DataColumn[2]
			{
				dataColumn,
				dataColumn2
			};
			columns.Add("ApprovalName", typeof(string)).AllowDBNull = false;
			columns.Add("ObjectType", typeof(string));
			columns.Add("ObjectID", typeof(string));
			columns.Add("ObjectSysDocID", typeof(string));
			columns.Add("Status", typeof(string));
			columns.Add("UpdateFieldName1", typeof(string));
			columns.Add("UpdateFieldValue1", typeof(string));
			columns.Add("UpdateFieldName2", typeof(string));
			columns.Add("UpdateFieldValue2", typeof(string));
			columns.Add("ActionSetInactive", typeof(string));
			columns.Add("IsInactive", typeof(bool));
			columns.Add("NotifyonPrint", typeof(bool));
			columns.Add("AllownextTransaction", typeof(bool));
			columns.Add("AllowtoEdit", typeof(bool));
			base.Tables.Add(dataTable);
			dataTable = new DataTable("Approval_Level");
			columns = dataTable.Columns;
			columns.Add("ApprovalID", typeof(string));
			columns.Add("ApprovalType", typeof(byte));
			columns.Add("RowIndex", typeof(short)).AllowDBNull = false;
			columns.Add("ApproverType", typeof(byte));
			columns.Add("ApproverID", typeof(string));
			columns.Add("PreRequisiteIndex", typeof(short));
			columns.Add("Condition", typeof(string));
			base.Tables.Add(dataTable);
			dataTable = new DataTable("Approval_Task");
			columns = dataTable.Columns;
			columns.Add("TaskID", typeof(int)).AllowDBNull = true;
			columns.Add("LevelID", typeof(byte));
			columns.Add("ApprovalType", typeof(byte));
			columns.Add("Status", typeof(byte));
			columns.Add("AssigneeType", typeof(byte));
			columns.Add("AssigneeID", typeof(string));
			columns.Add("DateCreated", typeof(DateTime));
			columns.Add("ObjectType", typeof(byte));
			columns.Add("PreRequisiteIndex", typeof(int));
			columns.Add("ObjectID", typeof(int));
			columns.Add("DocumentSysDocID", typeof(string));
			columns.Add("DocumentCode", typeof(string));
			columns.Add("DateApproved", typeof(DateTime));
			columns.Add("ApprovalID", typeof(string));
			base.Tables.Add(dataTable);
		}
	}
}
