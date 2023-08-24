using Micromind.Common;
using Micromind.Common.Data;
using Micromind.Common.Libraries;
using System;
using System.Data;
using System.Data.SqlClient;

namespace Micromind.Data
{
	public sealed class TaskTransaction : StoreObject
	{
		private const string SYSDOCID_PARM = "@SysDocID";

		private const string DOCID_PARM = "@VoucherID";

		private const string TASKTYPEID_PARM = "@TaskTypeID";

		private const string SUBTASKID_PARM = "@SubTaskID";

		private const string DESCRIPTION_PARM = "@Description";

		private const string ASSIGNEDTO_PARM = "@AssignedTo";

		private const string TASKSTEPID_PARM = "@TaskStepID";

		private const string DEADLINE_PARM = "@Deadline";

		private const string TRANSACTIONDATE_PARM = "@TransactionDate";

		private const string ASSIGNEDSYSDOCID_PARM = "@AssignedSysDocID";

		private const string ASSIGNEDDOCID_PARM = "@AssignedVouherID";

		private const string STARTDATE_PARM = "@StartDate";

		private const string TASKNAME_PARM = "@TaskName";

		private static string DEFAULTASSIGNEEID_PARM = "@DefaultAssigneeID";

		private static string DAYSALLOWED_PARM = "@DaysAllowed";

		private static string PREREQUEST_PARM = "@PreRequest";

		private static string DOCTYPE_PARM = "@DocType";

		private const string STATUS_PARM = "@Status";

		private const string ROWINDEX_FIELD = "@RowIndex";

		private const string ISVOID_PARM = "@IsVoid";

		private const string TASKTRANSACTION_TABLE = "Task_Transaction";

		private const string TASKTRANSACTIONDETAIL_TABLE = "Task_Transaction_Detail";

		private const string CREATEDBY_PARM = "@CreatedBy";

		private const string DATECREATED_PARM = "@DateCreated";

		private const string UPDATEDBY_PARM = "@UpdatedBy";

		private const string DATEUPDATED_PARM = "@DateUpdated";

		public TaskTransaction(Config config)
			: base(config)
		{
		}

		private string GetInsertUpdateTaskTransactionText(bool isUpdate)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddInsertUpdateParameters("Task_Transaction", new FieldValue("SysDocID", "@SysDocID", isUpdateConditionField: true), new FieldValue("VoucherID", "@VoucherID", isUpdateConditionField: true), new FieldValue("TaskTypeID", "@TaskTypeID"), new FieldValue("TaskName", "@TaskName"), new FieldValue(TaskTransactionData.DESCRIPTION_FIELD, "@Description"), new FieldValue("AssignedSysDocID", "@AssignedSysDocID"), new FieldValue("AssignedVouherID", "@AssignedVouherID"), new FieldValue("TransactionDate", "@TransactionDate"), new FieldValue(TaskTransactionData.DOCTYPE_FIELD, DOCTYPE_PARM), new FieldValue("IsVoid", "@IsVoid"));
			if (isUpdate)
			{
				sqlBuilder.AddInsertUpdateParameters("Task_Transaction", new FieldValue("DateUpdated", "@DateUpdated", isUpdateConditionField: true, checkForNullValue: true));
			}
			if (isUpdate)
			{
				return sqlBuilder.GetUpdateExpression();
			}
			return sqlBuilder.GetInsertExpression();
		}

		private SqlCommand GetInsertUpdateTaskTransactionCommand(bool isUpdate)
		{
			SqlParameterCollection parameters;
			if (isUpdate)
			{
				if (updateCommand != null)
				{
					updateCommand = null;
				}
				updateCommand = new SqlCommand(GetInsertUpdateTaskTransactionText(isUpdate: true), base.DBConfig.Connection);
				updateCommand.CommandType = CommandType.Text;
				parameters = updateCommand.Parameters;
			}
			else
			{
				if (insertCommand != null)
				{
					insertCommand = null;
				}
				insertCommand = new SqlCommand(GetInsertUpdateTaskTransactionText(isUpdate: false), base.DBConfig.Connection);
				insertCommand.CommandType = CommandType.Text;
				parameters = insertCommand.Parameters;
			}
			parameters.Add("@SysDocID", SqlDbType.NVarChar);
			parameters.Add("@VoucherID", SqlDbType.NVarChar);
			parameters.Add("@TaskTypeID", SqlDbType.NVarChar);
			parameters.Add("@TaskName", SqlDbType.NVarChar);
			parameters.Add("@Description", SqlDbType.NVarChar);
			parameters.Add("@AssignedSysDocID", SqlDbType.NVarChar);
			parameters.Add("@AssignedVouherID", SqlDbType.NVarChar);
			parameters.Add("@TransactionDate", SqlDbType.DateTime);
			parameters.Add("@IsVoid", SqlDbType.Bit);
			parameters.Add(DOCTYPE_PARM, SqlDbType.Int);
			if (isUpdate)
			{
				parameters.Add("@DateUpdated", SqlDbType.DateTime);
			}
			parameters["@SysDocID"].SourceColumn = "SysDocID";
			parameters["@VoucherID"].SourceColumn = "VoucherID";
			parameters["@TaskTypeID"].SourceColumn = "TaskTypeID";
			parameters["@TaskName"].SourceColumn = "TaskName";
			parameters["@Description"].SourceColumn = TaskTransactionData.DESCRIPTION_FIELD;
			parameters["@AssignedVouherID"].SourceColumn = "AssignedVouherID";
			parameters["@AssignedSysDocID"].SourceColumn = "AssignedSysDocID";
			parameters["@TransactionDate"].SourceColumn = "TransactionDate";
			parameters["@IsVoid"].SourceColumn = "IsVoid";
			parameters[DOCTYPE_PARM].SourceColumn = TaskTransactionData.DOCTYPE_FIELD;
			if (isUpdate)
			{
				parameters["@DateUpdated"].SourceColumn = "DateUpdated";
			}
			if (isUpdate)
			{
				return updateCommand;
			}
			return insertCommand;
		}

		private string GetInsertUpdateTaskTransasctionDetailsText(bool isUpdate)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddInsertUpdateParameters("Task_Transaction_Detail", new FieldValue("SysDocID", "@SysDocID"), new FieldValue("VoucherID", "@VoucherID"), new FieldValue("TaskStepID", "@TaskStepID"), new FieldValue(TaskTransactionData.DESCRIPTION_FIELD, "@Description"), new FieldValue(TaskTransactionData.DEFAULTASSIGNEEID_FIELD, DEFAULTASSIGNEEID_PARM), new FieldValue("StartDate", "@StartDate"), new FieldValue("DeadLine", "@Deadline"), new FieldValue("Status", "@Status"), new FieldValue(TaskTransactionData.DAYSALLOWED_FIELD, DAYSALLOWED_PARM), new FieldValue(TaskTransactionData.DOCTYPE_FIELD, DOCTYPE_PARM), new FieldValue(TaskTransactionData.PREREQUEST_FIELD, PREREQUEST_PARM), new FieldValue("RowIndex", "@RowIndex"));
			if (isUpdate)
			{
				return sqlBuilder.GetUpdateExpression();
			}
			return sqlBuilder.GetInsertExpression();
		}

		private SqlCommand GetInsertUpdateaskTransasctionDetailsCommand(bool isUpdate)
		{
			SqlParameterCollection parameters;
			if (isUpdate)
			{
				if (updateCommand != null)
				{
					updateCommand = null;
				}
				updateCommand = new SqlCommand(GetInsertUpdateTaskTransasctionDetailsText(isUpdate: true), base.DBConfig.Connection);
				updateCommand.CommandType = CommandType.Text;
				parameters = updateCommand.Parameters;
			}
			else
			{
				if (insertCommand != null)
				{
					insertCommand = null;
				}
				insertCommand = new SqlCommand(GetInsertUpdateTaskTransasctionDetailsText(isUpdate: false), base.DBConfig.Connection);
				insertCommand.CommandType = CommandType.Text;
				parameters = insertCommand.Parameters;
			}
			parameters.Add("@SysDocID", SqlDbType.NVarChar);
			parameters.Add("@VoucherID", SqlDbType.NVarChar);
			parameters.Add("@TaskStepID", SqlDbType.NVarChar);
			parameters.Add("@Description", SqlDbType.NVarChar);
			parameters.Add(DEFAULTASSIGNEEID_PARM, SqlDbType.NVarChar);
			parameters.Add("@StartDate", SqlDbType.DateTime);
			parameters.Add("@Deadline", SqlDbType.DateTime);
			parameters.Add("@Status", SqlDbType.NVarChar);
			parameters.Add(DOCTYPE_PARM, SqlDbType.Int);
			parameters.Add(DAYSALLOWED_PARM, SqlDbType.Int);
			parameters.Add(PREREQUEST_PARM, SqlDbType.NVarChar);
			parameters.Add("@RowIndex", SqlDbType.Int);
			parameters["@SysDocID"].SourceColumn = "SysDocID";
			parameters["@VoucherID"].SourceColumn = "VoucherID";
			parameters["@TaskStepID"].SourceColumn = "TaskStepID";
			parameters["@Description"].SourceColumn = TaskTransactionData.DESCRIPTION_FIELD;
			parameters[DEFAULTASSIGNEEID_PARM].SourceColumn = TaskTransactionData.DEFAULTASSIGNEEID_FIELD;
			parameters["@StartDate"].SourceColumn = "StartDate";
			parameters["@Deadline"].SourceColumn = "DeadLine";
			parameters["@Status"].SourceColumn = "Status";
			parameters[DOCTYPE_PARM].SourceColumn = TaskTransactionData.DOCTYPE_FIELD;
			parameters[DAYSALLOWED_PARM].SourceColumn = TaskTransactionData.DAYSALLOWED_FIELD;
			parameters[PREREQUEST_PARM].SourceColumn = TaskTransactionData.PREREQUEST_FIELD;
			parameters["@RowIndex"].SourceColumn = "RowIndex";
			if (isUpdate)
			{
				return updateCommand;
			}
			return insertCommand;
		}

		private bool ValidateData(TaskTransactionData journalData)
		{
			return true;
		}

		public bool InsertUpdateTaskTransaction(TaskTransactionData TaskTransactionData, bool isUpdate)
		{
			bool flag = true;
			SqlCommand insertUpdateTaskTransactionCommand = GetInsertUpdateTaskTransactionCommand(isUpdate);
			try
			{
				DataRow dataRow = TaskTransactionData.TaskTransactionTable.Rows[0];
				SqlTransaction sqlTransaction = base.DBConfig.StartNewTransaction();
				string text = dataRow["VoucherID"].ToString();
				string text2 = dataRow["SysDocID"].ToString();
				if (!isUpdate && new SystemDocuments(base.DBConfig).ExistDocumentNumber("Task_Transaction", "VoucherID", dataRow["SysDocID"].ToString(), text, sqlTransaction))
				{
					base.DBConfig.EndTransaction(result: false);
					throw new CompanyException("Document number already exist.", 1046);
				}
				foreach (DataRow row in TaskTransactionData.TaskTransactionDetailTable.Rows)
				{
					row["VoucherID"] = dataRow["VoucherID"];
					row["SysDocID"] = dataRow["SysDocID"];
				}
				if (isUpdate)
				{
					flag &= DeleteTaskTransactionDetailsRows(text2, text, sqlTransaction);
				}
				insertUpdateTaskTransactionCommand.Transaction = sqlTransaction;
				flag = (isUpdate ? (flag & Update(TaskTransactionData, "Task_Transaction", insertUpdateTaskTransactionCommand)) : (flag & Insert(TaskTransactionData, "Task_Transaction", insertUpdateTaskTransactionCommand)));
				insertUpdateTaskTransactionCommand = GetInsertUpdateaskTransasctionDetailsCommand(isUpdate: false);
				insertUpdateTaskTransactionCommand.Transaction = sqlTransaction;
				if (TaskTransactionData.Tables["Task_Transaction_Detail"].Rows.Count > 0)
				{
					flag &= Insert(TaskTransactionData, "Task_Transaction_Detail", insertUpdateTaskTransactionCommand);
				}
				if (flag)
				{
					flag &= UpdateTableRowInsertUpdateInfo("Task_Transaction", "SysDocID", text2, "VoucherID", text, sqlTransaction, !isUpdate);
					string entityName = "Task Transaction";
					flag = (isUpdate ? (flag & AddActivityLog(entityName, text, text2, ActivityTypes.Update, sqlTransaction)) : (flag & AddActivityLog(entityName, text, text2, ActivityTypes.Add, sqlTransaction)));
					if (!isUpdate)
					{
						flag &= new SystemDocuments(base.DBConfig).UpdateNextDocumentNumber(dataRow["SysDocID"].ToString(), dataRow["VoucherID"].ToString(), "Task_Transaction", "VoucherID", sqlTransaction);
					}
				}
				if (!flag)
				{
					return flag;
				}
				new Approval(base.DBConfig).CreateTransactionApprovalTasks(SysDocTypes.TaskTransaction, text2, text, "Task_Transaction", sqlTransaction);
				return flag;
			}
			catch
			{
				flag = false;
				throw;
			}
			finally
			{
				base.DBConfig.EndTransaction(flag);
			}
		}

		public TaskTransactionData GetTaskTransactionByID(string sysDocID, string voucherID)
		{
			try
			{
				TaskTransactionData taskTransactionData = new TaskTransactionData();
				string textCommand = "SELECT * FROM Task_Transaction WHERE SysDocID='" + sysDocID + "' AND VoucherID='" + voucherID + "'";
				FillDataSet(taskTransactionData, "Task_Transaction", textCommand);
				if (taskTransactionData == null || taskTransactionData.Tables.Count == 0 || taskTransactionData.Tables["Task_Transaction"].Rows.Count == 0)
				{
					return null;
				}
				textCommand = "SELECT TD.*  FROM Task_Transaction_Detail TD \t\t\t\t\t\t\r\n                        WHERE VoucherID='" + voucherID + "' AND SysDocID='" + sysDocID + "'";
				FillDataSet(taskTransactionData, "Task_Transaction_Detail", textCommand);
				return taskTransactionData;
			}
			catch
			{
				throw;
			}
		}

		public bool DeleteTaskTransaction(string sysDocID, string voucherID)
		{
			bool flag = true;
			try
			{
				string text = "";
				SqlTransaction sqlTransaction = base.DBConfig.StartNewTransaction();
				flag &= DeleteTaskTransactionDetailsRows(sysDocID, voucherID, sqlTransaction);
				text = "DELETE FROM Task_Transaction WHERE SysDocID = '" + sysDocID + "' AND  VoucherID = '" + voucherID + "'";
				flag &= (ExecuteNonQuery(text, sqlTransaction) >= 0);
				if (!flag)
				{
					return flag;
				}
				ActivityTypes activityType = ActivityTypes.Delete;
				AddActivityLog("Task Transaction", voucherID, activityType, sqlTransaction);
				return flag;
			}
			catch
			{
				flag = false;
				throw;
			}
			finally
			{
				base.DBConfig.EndTransaction(flag);
			}
		}

		internal bool DeleteTaskTransactionDetailsRows(string sysDocID, string voucherID, SqlTransaction sqlTransaction)
		{
			bool flag = true;
			try
			{
				string commandText = "DELETE FROM Task_Transaction_Detail WHERE SysDocID = '" + sysDocID + "' AND VoucherID = '" + voucherID + "'";
				return Delete(commandText, sqlTransaction);
			}
			catch
			{
				throw;
			}
		}

		public DataSet GetTaskTransactionToPrint(string sysDocID, string[] voucherID)
		{
			try
			{
				string text = "";
				for (int i = 0; i < voucherID.Length; i++)
				{
					text = "'" + voucherID[i] + "'";
					if (i < voucherID.Length - 1)
					{
						text += ",";
					}
				}
				DataSet dataSet = new DataSet();
				SqlCommand sqlCommand = new SqlCommand("select TR.*, TY.Name AS [TASK TYPE], TS.Name AS [Task Step], Users.UserName from Task_Transaction TR \r\n\r\n                                INNER JOIN Task_Transaction_Detail TD ON  TR.SysDocID=TD.SysDocID AND TR.VoucherID=TD.VoucherID\r\n\r\n                                LEFT JOIN Task_Type TY ON TR.TaskTypeID=TY.TaskTypeID LEFT JOIN Task_Steps TS ON TD.TaskStepID=TS.TaskStepID \r\n                                LEFT JOIN Users ON TD.DefaultAssigneeID=Users.UserID\r\n                                 \r\n                                WHERE TR.SysDocID = '" + sysDocID + "' AND TR.VoucherID IN (" + text + ")");
				FillDataSet(dataSet, "Task_Transaction", sqlCommand);
				return dataSet;
			}
			catch
			{
				throw;
			}
		}

		public DataSet GetTaskTransactionReport(DateTime from, DateTime to, string fromVehicle, string toVehicle)
		{
			string text = CommonLib.ToSqlDateTimeString(from);
			string text2 = CommonLib.ToSqlDateTimeString(to);
			string text3 = "";
			text3 = text3 + " where Maintenance_Scheduler.MaintenanceDate BETWEEN '" + text + "' AND '" + text2 + "' ";
			if (fromVehicle != "")
			{
				text3 = text3 + " AND Maintenance_Scheduler.VehicleNumber BETWEEN '" + fromVehicle + "' AND '" + toVehicle + "' ";
			}
			DataSet dataSet = new DataSet();
			FillDataSet(dataSet, "Task_Transaction", text3);
			return dataSet;
		}

		public DataSet GetTaskTransactionSummary(string vehicle)
		{
			string text = "SELECT MA.SysDocID [Doc ID], MA.VoucherID [Number],MA.Amount,MA.Odometer,MA.ServiceProvider,\r\n                        MA.Status,MA.TrackMaintenance,MA.MaintenanceDate, ma.VehicleNumber,ma.CreatedBy,ma.DateUpdated\r\n                               FROM    Vehicle_Maintenance_Scheduler MA LEFT JOIN\r\n                         Vehicle V ON MA.VehicleNumber = V.VehicleID INNER JOIN VENDOR VR ON VR.VendorID=MA.ServiceProvider where ISNULL(IsVoid,'False')='False' and Status=0 ";
			if (vehicle != "")
			{
				text = text + " AND MA.VehicleNumber='" + vehicle + "'";
			}
			DataSet dataSet = new DataSet();
			FillDataSet(dataSet, "Task_Transaction", text);
			return dataSet;
		}

		public DataSet GetList(DateTime from, DateTime to, bool showVoid)
		{
			DataSet dataSet = new DataSet();
			StoreConfiguration.ToSqlDateTimeString(from);
			StoreConfiguration.ToSqlDateTimeString(to);
			SqlCommand sqlCommand = new SqlCommand("SELECT * FROM Task_Transaction ");
			FillDataSet(dataSet, "Task_Transaction", sqlCommand);
			return dataSet;
		}

		public bool VoidMaintenance(string sysDocID, string voucherID, bool isVoid)
		{
			bool flag = true;
			try
			{
				SqlTransaction sqlTransaction = base.DBConfig.StartNewTransaction();
				TaskTransactionData dataSet = new TaskTransactionData();
				string textCommand = "SELECT * FROM Task_Transaction\r\n                              WHERE SysDocID = '" + sysDocID + "' AND VoucherID = '" + voucherID + "'";
				FillDataSet(dataSet, "Task_Transaction", textCommand);
				textCommand = "UPDATE Task_Transaction SET IsVoid = '" + isVoid.ToString() + "' WHERE SysDocID='" + sysDocID + "' AND VoucherID = '" + voucherID + "'";
				flag &= (ExecuteNonQuery(textCommand, sqlTransaction) > 0);
				if (!flag)
				{
					return flag;
				}
				ActivityTypes activityType = ActivityTypes.Void;
				if (!isVoid)
				{
					activityType = ActivityTypes.Unvoid;
				}
				AddActivityLog("Task Transaction", voucherID, sysDocID, activityType, sqlTransaction);
				return flag;
			}
			catch
			{
				flag = false;
				throw;
			}
			finally
			{
				base.DBConfig.EndTransaction(flag);
			}
		}

		public bool UpdateMaintenanceStatus(string sysDocID, string voucherID, int status)
		{
			bool flag = true;
			try
			{
				SqlTransaction sqlTransaction = base.DBConfig.StartNewTransaction();
				TaskTransactionData dataSet = new TaskTransactionData();
				if (sysDocID != "" && voucherID != "")
				{
					string textCommand = "SELECT * FROM Vehicle_Maintenance_Scheduler\r\n                              WHERE SysDocID = '" + sysDocID + "' AND VoucherID = '" + voucherID + "'";
					FillDataSet(dataSet, "Task_Transaction", textCommand);
					textCommand = "UPDATE Vehicle_Maintenance_Scheduler SET Status = '" + status + "' WHERE SysDocID='" + sysDocID + "' AND VoucherID = '" + voucherID + "'";
					flag &= (ExecuteNonQuery(textCommand, sqlTransaction) > 0);
					return flag;
				}
				if (!(voucherID != ""))
				{
					return flag;
				}
				string textCommand2 = "SELECT * FROM Vehicle_Maintenance_Scheduler\r\n                              WHERE VoucherID = '" + voucherID + "'";
				FillDataSet(dataSet, "Task_Transaction", textCommand2);
				textCommand2 = "UPDATE Vehicle_Maintenance_Scheduler SET Status = '" + status + "' WHERE  VoucherID = '" + voucherID + "'";
				flag &= (ExecuteNonQuery(textCommand2, sqlTransaction) > 0);
				return flag;
			}
			catch
			{
				flag = false;
				throw;
			}
			finally
			{
				base.DBConfig.EndTransaction(flag);
			}
		}

		public bool SetAssignee(string sysDocID, string voucherID, string Assignee)
		{
			try
			{
				string exp = "UPDATE Task_Transaction SET AssignedTo= '" + Assignee + "' WHERE SysDocID='" + sysDocID + "' AND VoucherID='" + voucherID + "'";
				return ExecuteNonQuery(exp) > 0;
			}
			catch
			{
				throw;
			}
		}

		public bool SetTaskStep(string sysDocID, string voucherID, string step)
		{
			try
			{
				string exp = "UPDATE Task_Transaction SET TaskStepID= '" + step + "' WHERE SysDocID='" + sysDocID + "' AND VoucherID='" + voucherID + "'";
				return ExecuteNonQuery(exp) > 0;
			}
			catch
			{
				throw;
			}
		}

		public bool UpdateTaskStatus(string sysDocID, string voucherID, int status, int rowIndex)
		{
			try
			{
				string exp = ("UPDATE Task_Transaction_Detail SET Status= '" + status + "' WHERE SysDocID='" + sysDocID + "' AND VoucherID='" + voucherID + "' AND RowIndex=" + rowIndex) ?? "";
				return ExecuteNonQuery(exp) > 0;
			}
			catch
			{
				throw;
			}
		}
	}
}
