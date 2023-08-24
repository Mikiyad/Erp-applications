using Micromind.Common;
using Micromind.Common.Data;
using Micromind.Common.Libraries;
using System;
using System.Data;
using System.Data.SqlClient;

namespace Micromind.Data
{
	public sealed class TaskTransactionStatus : StoreObject
	{
		private const string SYSDOCID_PARM = "@SysDocID";

		private const string VOUCHERID_PARM = "@VoucherID";

		private const string TASKNAME_PARM = "@TaskName";

		private const string TASKSTEPID_PARM = "@TaskStepID";

		private const string STATUS_PARM = "@Status";

		private const string ISVOID_PARM = "@IsVoid";

		private const string TASKTRANSACTIONSTATUS_TABLE = "Task_Transaction_Status";

		private const string TRANSACTIONDATE_PARM = "@TransactionDate";

		private const string SOURCESYSDOCID_PARM = "@SourceSysDocID";

		private const string SOURCEVOUCHERID_PARM = "@SourceVoucherID";

		private const string SOURCEROWINDEX_PARM = "@SourceRowIndex";

		private const string TRSYSDOCID_PARM = "@TRSysDocID";

		private const string TRVOUCHERID_PARM = "@TRVoucherID";

		private const string REMARKS_PARM = "@Remarks";

		private const string MESSAGE_PARM = "@Message";

		private const string CREATEDBY_PARM = "@CreatedBy";

		private const string DATECREATED_PARM = "@DateCreated";

		private const string UPDATEDBY_PARM = "@UpdatedBy";

		private const string DATEUPDATED_PARM = "@DateUpdated";

		public TaskTransactionStatus(Config config)
			: base(config)
		{
		}

		private string GetInsertUpdateTaskTransactionStatusText(bool isUpdate)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddInsertUpdateParameters("Task_Transaction_Status", new FieldValue("SysDocID", "@SysDocID", isUpdateConditionField: true), new FieldValue("VoucherID", "@VoucherID", isUpdateConditionField: true), new FieldValue("TaskName", "@TaskName"), new FieldValue("TaskStepID", "@TaskStepID"), new FieldValue("TransactionDate", "@TransactionDate"), new FieldValue("IsVoid", "@IsVoid"), new FieldValue("SourceSysDocID", "@SourceSysDocID"), new FieldValue("SourceVoucherID", "@SourceVoucherID"), new FieldValue("SourceRowIndex", "@SourceRowIndex"), new FieldValue("TRSysDocID", "@TRSysDocID"), new FieldValue("TRVoucherID", "@TRVoucherID"), new FieldValue("Status", "@Status"), new FieldValue("Remarks", "@Remarks"), new FieldValue("Message", "@Message"));
			if (isUpdate)
			{
				sqlBuilder.AddInsertUpdateParameters("Task_Transaction_Status", new FieldValue("DateUpdated", "@DateUpdated", isUpdateConditionField: true, checkForNullValue: true));
			}
			if (isUpdate)
			{
				return sqlBuilder.GetUpdateExpression();
			}
			return sqlBuilder.GetInsertExpression();
		}

		private SqlCommand GetInsertUpdateTaskTransactionStatusCommand(bool isUpdate)
		{
			SqlParameterCollection parameters;
			if (isUpdate)
			{
				if (updateCommand != null)
				{
					updateCommand = null;
				}
				updateCommand = new SqlCommand(GetInsertUpdateTaskTransactionStatusText(isUpdate: true), base.DBConfig.Connection);
				updateCommand.CommandType = CommandType.Text;
				parameters = updateCommand.Parameters;
			}
			else
			{
				if (insertCommand != null)
				{
					insertCommand = null;
				}
				insertCommand = new SqlCommand(GetInsertUpdateTaskTransactionStatusText(isUpdate: false), base.DBConfig.Connection);
				insertCommand.CommandType = CommandType.Text;
				parameters = insertCommand.Parameters;
			}
			parameters.Add("@SysDocID", SqlDbType.NVarChar);
			parameters.Add("@VoucherID", SqlDbType.NVarChar);
			parameters.Add("@TaskName", SqlDbType.NVarChar);
			parameters.Add("@TaskStepID", SqlDbType.NVarChar);
			parameters.Add("@TransactionDate", SqlDbType.DateTime);
			parameters.Add("@Status", SqlDbType.NVarChar);
			parameters.Add("@IsVoid", SqlDbType.NVarChar);
			parameters.Add("@SourceSysDocID", SqlDbType.NVarChar);
			parameters.Add("@SourceVoucherID", SqlDbType.NVarChar);
			parameters.Add("@SourceRowIndex", SqlDbType.NVarChar);
			parameters.Add("@TRSysDocID", SqlDbType.NVarChar);
			parameters.Add("@TRVoucherID", SqlDbType.NVarChar);
			parameters.Add("@Remarks", SqlDbType.NVarChar);
			parameters.Add("@Message", SqlDbType.NVarChar);
			parameters["@SysDocID"].SourceColumn = "SysDocID";
			parameters["@VoucherID"].SourceColumn = "VoucherID";
			parameters["@TaskName"].SourceColumn = "TaskName";
			parameters["@TaskStepID"].SourceColumn = "TaskStepID";
			parameters["@TransactionDate"].SourceColumn = "TransactionDate";
			parameters["@IsVoid"].SourceColumn = "IsVoid";
			parameters["@SourceSysDocID"].SourceColumn = "SourceSysDocID";
			parameters["@SourceVoucherID"].SourceColumn = "SourceVoucherID";
			parameters["@SourceRowIndex"].SourceColumn = "SourceRowIndex";
			parameters["@TRSysDocID"].SourceColumn = "TRSysDocID";
			parameters["@TRVoucherID"].SourceColumn = "TRVoucherID";
			parameters["@Status"].SourceColumn = "Status";
			parameters["@Remarks"].SourceColumn = "Remarks";
			parameters["@Message"].SourceColumn = "Message";
			if (isUpdate)
			{
				parameters.Add("@DateUpdated", SqlDbType.DateTime);
				parameters["@DateUpdated"].SourceColumn = "DateUpdated";
			}
			if (isUpdate)
			{
				return updateCommand;
			}
			return insertCommand;
		}

		private bool ValidateData(TaskTransactionStatusData journalData)
		{
			return true;
		}

		public bool InsertUpdateTaskTransactionStatus(TaskTransactionStatusData TaskTransactionStatusData, bool isUpdate)
		{
			bool flag = true;
			SqlCommand insertUpdateTaskTransactionStatusCommand = GetInsertUpdateTaskTransactionStatusCommand(isUpdate);
			try
			{
				DataRow dataRow = TaskTransactionStatusData.TaskTransactionStatusTable.Rows[0];
				SqlTransaction sqlTransaction = base.DBConfig.StartNewTransaction();
				string text = "";
				string text2 = "";
				string text3 = dataRow["VoucherID"].ToString();
				string sysDocID = dataRow["SysDocID"].ToString();
				if (!isUpdate && new SystemDocuments(base.DBConfig).ExistDocumentNumber("Task_Transaction_Status", "VoucherID", dataRow["SysDocID"].ToString(), text3, sqlTransaction))
				{
					throw new CompanyException("Document number already exist.", 1046);
				}
				insertUpdateTaskTransactionStatusCommand.Transaction = sqlTransaction;
				if (!isUpdate)
				{
					if (TaskTransactionStatusData.Tables["Task_Transaction_Status"].Rows.Count > 0)
					{
						flag &= Insert(TaskTransactionStatusData, "Task_Transaction_Status", insertUpdateTaskTransactionStatusCommand);
					}
				}
				else
				{
					flag &= Update(TaskTransactionStatusData, "Task_Transaction_Status", insertUpdateTaskTransactionStatusCommand);
				}
				if (flag)
				{
					text2 = TaskTransactionStatusData.TaskTransactionStatusTable.Rows[0]["SourceVoucherID"].ToString();
					text = TaskTransactionStatusData.TaskTransactionStatusTable.Rows[0]["SourceSysDocID"].ToString();
					int rowIndex = int.Parse(dataRow["SourceRowIndex"].ToString());
					int status = int.Parse(dataRow["Status"].ToString());
					if (text2 != "")
					{
						flag &= new TaskTransaction(base.DBConfig).UpdateTaskStatus(text, text2, status, rowIndex);
					}
				}
				if (!flag)
				{
					throw new Exception("Failed to save the transaction because of an unexpected error. Please try again.");
				}
				flag &= UpdateTableRowInsertUpdateInfo("Task_Transaction_Status", "SysDocID", dataRow["SysDocID"].ToString(), "VoucherID", dataRow["VoucherID"].ToString(), sqlTransaction, !isUpdate);
				string entityName = "TaskTransactionStatus";
				flag = (isUpdate ? (flag & AddActivityLog(entityName, text3, sysDocID, ActivityTypes.Update, sqlTransaction)) : (flag & AddActivityLog(entityName, text3, sysDocID, ActivityTypes.Add, sqlTransaction)));
				if (isUpdate)
				{
					return flag;
				}
				flag &= new SystemDocuments(base.DBConfig).UpdateNextDocumentNumber(dataRow["SysDocID"].ToString(), dataRow["VoucherID"].ToString(), "Task_Transaction_Status", "VoucherID", sqlTransaction);
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

		public TaskTransactionStatusData GetTaskTransactionStatusByID(string sysDocID, string voucherID)
		{
			try
			{
				TaskTransactionStatusData taskTransactionStatusData = new TaskTransactionStatusData();
				string textCommand = "SELECT * FROM Task_Transaction_Status WHERE VoucherID='" + voucherID + "' AND SysDocID='" + sysDocID + "'";
				FillDataSet(taskTransactionStatusData, "Task_Transaction_Status", textCommand);
				return taskTransactionStatusData;
			}
			catch
			{
				throw;
			}
		}

		public DataSet GetTaskTransactionStatus()
		{
			try
			{
				DataSet dataSet = new DataSet();
				string textCommand = "SELECT SysDocID [Doc ID],VoucherID [Number],TransactionDate AS [Date] FROM EA_TaskTransactionStatus where Status=1 ";
				FillDataSet(dataSet, "TaskTransactionStatus", textCommand);
				return dataSet;
			}
			catch
			{
				throw;
			}
		}

		private bool VoidTaskTransactionStatus(string sysDocID, string voucherID, bool isVoid, SqlTransaction sqlTransaction)
		{
			return false;
		}

		public bool DeleteTaskTransactionStatus(string sysDocID, string voucherID)
		{
			bool flag = true;
			try
			{
				string text = "";
				SqlTransaction sqlTransaction = base.DBConfig.StartNewTransaction();
				text = "DELETE FROM EA_TaskTransactionStatus WHERE SysDocID='" + sysDocID + "' AND VoucherID = '" + voucherID + "'";
				flag &= (ExecuteNonQuery(text, sqlTransaction) >= 0);
				if (!flag)
				{
					return flag;
				}
				ActivityTypes activityType = ActivityTypes.Delete;
				AddActivityLog("TaskTransactionStatus", voucherID, sysDocID, activityType, sqlTransaction);
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

		public DataSet GetTaskTransactionStatusToPrint(string sysDocID, string[] voucherID)
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
				SqlCommand sqlCommand = new SqlCommand("select EAR.*, EC.EquipmentCategoryName, E.Description,ERT.TaskTransactionStatusTypeName , E.Capacity, E.CapacityType, E.Model, E.RegistrationNumber, E.Color, E.Fuel, E.Power, E.Year, E.OwnedBy \r\n                                from EA_TaskTransactionStatus EAR LEFT JOIN EA_Equipment_Category EC \r\n                                ON EAR.EquipmentCategoryID=EC.EquipmentCategoryID \r\n                                LEFT JOIN EA_Equipment E ON EAR.EquipmentID=E.EquipmentID\r\n                                LEFT JOIN EA_TaskTransactionStatus_Type ERT ON EAR.TaskTransactionStatusTypeID=ERT.TaskTransactionStatusTypeID\r\n                                WHERE SysDocID = '" + sysDocID + "' AND VoucherID IN (" + text + ")");
				FillDataSet(dataSet, "Task_Transaction_Status", sqlCommand);
				if (dataSet == null || dataSet.Tables.Count == 0 || dataSet.Tables["Task_Transaction_Status"].Rows.Count == 0)
				{
					return null;
				}
				return dataSet;
			}
			catch
			{
				throw;
			}
		}

		public DataSet GetTaskTransactionStatusReport(DateTime from, DateTime to, string fromWarehouse, string toWarehouse, string fromItem, string toItem, string fromClass, string toClass, string fromCategory, string toCategory)
		{
			try
			{
				string text = CommonLib.ToSqlDateTimeString(from);
				string text2 = CommonLib.ToSqlDateTimeString(to);
				string text3 = " SELECT IA.SysDocID, IA.VoucherID, IA.TransactionDate, IA.LocationID, \r\n                                IA.Reference, IA.AdjustmentTypeID, IAD.ProductID, IAD.Description, \r\n                                IAD.Quantity, IAD.UnitID, IAD.RowIndex\r\n                                FROM Inventory_Adjustment IA INNER JOIN Inventory_Adjustment_Detail IAD ON IA.SysDocID = IAD.SysDocID AND                             \r\n                                IA.VoucherID = IAD.VoucherID \r\n                                INNER JOIN PRODUCT P ON IAD.ProductID=P.ProductID\r\n                                WHERE 1=1 ";
				text3 = text3 + " AND P.ItemType NOT IN ('3') AND TransactionDate BETWEEN '" + text + "' AND '" + text2 + "' ";
				if (fromItem != "")
				{
					text3 = text3 + " AND IAD.ProductID BETWEEN '" + fromItem + "' AND '" + toItem + "' ";
				}
				if (fromClass != "")
				{
					text3 = text3 + " AND IAD.ProductID IN (SELECT ProductID FROM Product WHERE ClassID BETWEEN '" + fromClass + "' AND '" + toClass + "') ";
				}
				if (fromCategory != "")
				{
					text3 = text3 + " AND IAD.ProductID IN (SELECT ProductID FROM Product WHERE CategoryID BETWEEN '" + fromCategory + "' AND '" + toCategory + "') ";
				}
				if (fromWarehouse != "" || toWarehouse != "")
				{
					text3 = text3 + " AND IA.LocationID BETWEEN '" + fromWarehouse + "' AND '" + toWarehouse + "' ";
				}
				text3 += " ORDER BY IAD.RowIndex";
				DataSet dataSet = new DataSet();
				FillDataSet(dataSet, "Product", text3);
				return dataSet;
			}
			catch
			{
				throw;
			}
		}

		public DataSet GetTaskTransactionStatusList(string sysDocID)
		{
			DataSet dataSet = new DataSet();
			string str = "SELECT SysDocID [Doc ID], VoucherID [Number], TransactionDate AS [Date] FROM EA_TaskTransactionStatus   ";
			if (!string.IsNullOrEmpty(sysDocID))
			{
				str = str + "  WHERE  SysDocID='" + sysDocID + "'";
			}
			str += " ORDER BY TransactionDate, VoucherID ";
			FillDataSet(dataSet, "Task_Transaction_Status", str);
			return dataSet;
		}

		public DataSet GetList(DateTime from, DateTime to, bool showVoid)
		{
			DataSet dataSet = new DataSet();
			string text = StoreConfiguration.ToSqlDateTimeString(from);
			string text2 = StoreConfiguration.ToSqlDateTimeString(to);
			string text3 = "SELECT 'False' AS V, SysDocID,VoucherID,TransactionDate,LocationID FROM EA_TaskTransactionStatus ";
			if (from != DateTime.MinValue)
			{
				text3 = text3 + " WHERE TransactionDate Between '" + text + "' AND '" + text2 + "'";
			}
			SqlCommand sqlCommand = new SqlCommand(text3);
			FillDataSet(dataSet, "Task_Transaction_Status", sqlCommand);
			return dataSet;
		}

		public DataSet GetTaskTransactionStatusByEquipmentLocationProjectReport(DateTime fromDate, DateTime toDate, string fromEquipment, string toEquipment, string fromType, string toType, string fromCategory, string toCategory, string fromLocation, string toLocation, string fromJob, string toJob)
		{
			try
			{
				string text = CommonLib.ToSqlDateTimeString(fromDate);
				string text2 = CommonLib.ToSqlDateTimeString(toDate);
				string text3 = "select RE.SysDocID, RE.VoucherID, RE.EquipmentID, \r\n                            RE.EquipmentCategoryID, RE.RequiredOn, RE.RequiredTill, RE.TaskTransactionStatusTypeID, RE.JobID, RE.LocationID, RE.Remarks,RE.TransactionDate,\r\n                             E.Description, E.Model, E.RegistrationNumber, E.ParentEquipmentID, E.Year, E.fuel, E.Capacity,CASE E.CapacityType WHEN 1 THEN 'SEAT' WHEN 2 THEN 'TON' END AS [CAPACITY TYPE], E.Power, E.OwnedBy,TaskTransactionStatusTypeName\r\n                             from EA_TaskTransactionStatus RE LEFT JOIN EA_Equipment E ON RE.EquipmentID=E.EquipmentID\r\n                             LEFT JOIN EA_TaskTransactionStatus_Type RT On RE.TaskTransactionStatusTypeID=RT.TaskTransactionStatusTypeID\r\n\t\t\t\t\t\t     WHERE TransactionDate BETWEEN '" + text + "' AND '" + text2 + "' ";
				if (fromEquipment != "")
				{
					text3 = text3 + " AND RE.EquipmentID BETWEEN '" + fromEquipment + "' AND '" + toEquipment + "' ";
				}
				if (fromType != "")
				{
					text3 = text3 + " AND RE.TaskTransactionStatusTypeID IN (SELECT TaskTransactionStatusTypeID FROM EA_TaskTransactionStatus WHERE TaskTransactionStatusTypeID BETWEEN '" + fromType + "' AND '" + toType + "') ";
				}
				if (fromCategory != "")
				{
					text3 = text3 + " AND RE.EquipmentCategoryID IN (SELECT EquipmentCategoryID FROM EA_TaskTransactionStatus WHERE EquipmentCategoryID BETWEEN '" + fromCategory + "' AND '" + toCategory + "') ";
				}
				if (fromLocation != "")
				{
					text3 = text3 + " AND RE.LocationID BETWEEN '" + fromLocation + "' AND '" + toLocation + "' ";
				}
				if (fromJob != "")
				{
					text3 = text3 + " AND RE.JobID BETWEEN '" + fromJob + "' AND '" + toJob + "' ";
				}
				DataSet dataSet = new DataSet();
				FillDataSet(dataSet, "TaskTransactionStatus", text3);
				return dataSet;
			}
			catch
			{
				throw;
			}
		}

		public bool UpdateTaskTransactionStatusStatus(string sysDocID, string voucherID, SqlTransaction sqlTransaction)
		{
			try
			{
				string exp = "Select Count(*) FROM EA_TaskTransactionStatus REQ\r\n                                WHERE SysdocID='" + sysDocID + "' AND VoucherID='" + voucherID + "' ";
				object obj = ExecuteScalar(exp, sqlTransaction);
				if (obj == null || int.Parse(obj.ToString()) == 0)
				{
					return true;
				}
				exp = "UPDATE EA_TaskTransactionStatus SET Status=0 WHERE SysDocID='" + sysDocID + "' AND VoucherID='" + voucherID + "'";
				return ExecuteNonQuery(exp, sqlTransaction) > 0;
			}
			catch
			{
				return false;
			}
		}
	}
}
