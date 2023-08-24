using Micromind.Common;
using Micromind.Common.Data;
using Micromind.Common.Libraries;
using System;
using System.Data;
using System.Data.SqlClient;

namespace Micromind.Data
{
	public sealed class CheckList : StoreObject
	{
		private const string CHECKLISTID_PARM = "@CheckListID";

		private const string CHECKLISTNAME_PARM = "@CheckListName";

		private const string CHECKLISTTYPE_PARM = "@CheckListType";

		private const string INTERVAL_PARM = "@Interval";

		private const string DEADLINEDAYS_PARM = "@DeadlineDays";

		private const string APPROVERTYPE_PARM = "@ApproverType";

		private const string APPROVERID_PARM = "@ApproverID";

		private const string STATUS_PARM = "@Status";

		private const string INACTIVE_PARM = "@IsInactive";

		private const string CREATEDBY_PARM = "@CreatedBy";

		private const string DATECREATED_PARM = "@DateCreated";

		private const string UPDATEDBY_PARM = "@UpdatedBy";

		private const string DATEUPDATED_PARM = "@DateUpdated";

		private const string STARTDATE_PARM = "@DateUpdated";

		private const string CHECKLISTTASK_TABLE = "CheckList_Task";

		private const string TASKID_PARM = "@TaskID";

		private const string DUEDATE_PARM = "@DueDate";

		private const string DEADLINEDATE_PARM = "@DeadlineDate";

		private const string ASSIGNEETYPE_PARM = "@AssigneeType";

		private const string ASSIGNEEID_PARM = "@AssigneeID";

		private const string DATECOMPLETED_PARM = "@DateCompleted";

		private const string CHECKLIST_TABLE = "CheckList";

		public CheckList(Config config)
			: base(config)
		{
		}

		private string GetInsertUpdateText(bool isUpdate)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddInsertUpdateParameters("CheckList", new FieldValue("CheckListID", "@CheckListID", isUpdateConditionField: true), new FieldValue("CheckListType", "@CheckListType", isUpdateConditionField: true), new FieldValue("CheckListName", "@CheckListName"), new FieldValue("ApproverType", "@ApproverType"), new FieldValue("ApproverID", "@ApproverID"), new FieldValue("Interval", "@Interval"), new FieldValue("DeadlineDays", "@DeadlineDays"), new FieldValue("StartDate", "@DateUpdated"), new FieldValue("Status", "@Status"), new FieldValue("IsInactive", "@IsInactive"));
			if (isUpdate)
			{
				sqlBuilder.AddInsertUpdateParameters("CheckList", new FieldValue("DateUpdated", "@DateUpdated", isUpdateConditionField: true, checkForNullValue: true));
			}
			if (isUpdate)
			{
				return sqlBuilder.GetUpdateExpression();
			}
			return sqlBuilder.GetInsertExpression();
		}

		private SqlCommand GetInsertUpdateCommand(bool isUpdate)
		{
			SqlParameterCollection parameters;
			if (isUpdate)
			{
				if (updateCommand != null)
				{
					return updateCommand;
				}
				updateCommand = new SqlCommand(GetInsertUpdateText(isUpdate: true), base.DBConfig.Connection);
				updateCommand.CommandType = CommandType.Text;
				parameters = updateCommand.Parameters;
			}
			else
			{
				if (insertCommand != null)
				{
					return insertCommand;
				}
				insertCommand = new SqlCommand(GetInsertUpdateText(isUpdate: false), base.DBConfig.Connection);
				insertCommand.CommandType = CommandType.Text;
				parameters = insertCommand.Parameters;
			}
			parameters.Add("@CheckListID", SqlDbType.NVarChar);
			parameters.Add("@CheckListName", SqlDbType.NVarChar);
			parameters.Add("@CheckListType", SqlDbType.TinyInt);
			parameters.Add("@ApproverType", SqlDbType.TinyInt);
			parameters.Add("@ApproverID", SqlDbType.NVarChar);
			parameters.Add("@Interval", SqlDbType.TinyInt);
			parameters.Add("@DeadlineDays", SqlDbType.TinyInt);
			parameters.Add("@DateUpdated", SqlDbType.DateTime);
			parameters.Add("@Status", SqlDbType.TinyInt);
			parameters.Add("@IsInactive", SqlDbType.Bit);
			parameters["@CheckListID"].SourceColumn = "CheckListID";
			parameters["@CheckListName"].SourceColumn = "CheckListName";
			parameters["@CheckListType"].SourceColumn = "CheckListType";
			parameters["@Status"].SourceColumn = "Status";
			parameters["@ApproverType"].SourceColumn = "ApproverType";
			parameters["@ApproverID"].SourceColumn = "ApproverID";
			parameters["@Interval"].SourceColumn = "Interval";
			parameters["@DeadlineDays"].SourceColumn = "DeadlineDays";
			parameters["@DateUpdated"].SourceColumn = "StartDate";
			parameters["@IsInactive"].SourceColumn = "IsInactive";
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

		private string GetInsertUpdateCheckListTaskText(bool isUpdate)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddInsertUpdateParameters("CheckList_Task", new FieldValue("CheckListID", "@CheckListID"), new FieldValue("AssigneeType", "@AssigneeType"), new FieldValue("CheckListType", "@CheckListType"), new FieldValue("AssigneeID", "@AssigneeID"), new FieldValue("DueDate", "@DueDate"), new FieldValue("DeadlineDate", "@DeadlineDate"), new FieldValue("Status", "@Status"), new FieldValue("DateCreated", "@DateCreated"));
			if (isUpdate)
			{
				return sqlBuilder.GetUpdateExpression();
			}
			return sqlBuilder.GetInsertExpression();
		}

		private SqlCommand GetInsertUpdateCheckListTaskCommand(bool isUpdate)
		{
			insertCommand = new SqlCommand(GetInsertUpdateCheckListTaskText(isUpdate: false), base.DBConfig.Connection);
			insertCommand.CommandType = CommandType.Text;
			SqlParameterCollection parameters = insertCommand.Parameters;
			parameters.Add("@CheckListID", SqlDbType.NVarChar);
			parameters.Add("@DueDate", SqlDbType.DateTime);
			parameters.Add("@DeadlineDate", SqlDbType.DateTime);
			parameters.Add("@AssigneeType", SqlDbType.TinyInt);
			parameters.Add("@CheckListType", SqlDbType.TinyInt);
			parameters.Add("@AssigneeID", SqlDbType.NVarChar);
			parameters.Add("@Status", SqlDbType.TinyInt);
			parameters.Add("@DateCreated", SqlDbType.DateTime);
			parameters["@CheckListID"].SourceColumn = "CheckListID";
			parameters["@DueDate"].SourceColumn = "DueDate";
			parameters["@DeadlineDate"].SourceColumn = "DeadlineDate";
			parameters["@AssigneeType"].SourceColumn = "AssigneeType";
			parameters["@CheckListType"].SourceColumn = "CheckListType";
			parameters["@AssigneeID"].SourceColumn = "AssigneeID";
			parameters["@Status"].SourceColumn = "Status";
			parameters["@DateCreated"].SourceColumn = "DateCreated";
			if (isUpdate)
			{
				return updateCommand;
			}
			return insertCommand;
		}

		public bool InsertUpdateCheckList(CheckListData accountCheckListData, bool isUpdate)
		{
			bool flag = true;
			SqlCommand insertUpdateCommand = GetInsertUpdateCommand(isUpdate);
			SqlTransaction sqlTransaction = null;
			try
			{
				string text = "";
				sqlTransaction = (insertUpdateCommand.Transaction = base.DBConfig.StartNewTransaction());
				if (isUpdate)
				{
					text = accountCheckListData.CheckListTable.Rows[0]["CheckListID"].ToString();
					string exp = "DELETE FROM CheckList_Task WHERE ChecklistID = '" + text + "' AND Status = 1";
					flag &= (ExecuteNonQuery(exp, sqlTransaction) >= 0);
				}
				flag = ((!isUpdate) ? Insert(accountCheckListData, "CheckList", insertUpdateCommand) : Update(accountCheckListData, "CheckList", insertUpdateCommand));
				DataRow dataRow = accountCheckListData.Tables[0].Rows[0];
				int.Parse(dataRow["Interval"].ToString());
				byte b = byte.Parse(dataRow["DeadlineDays"].ToString());
				CheckListData checkListData = new CheckListData();
				DataRow dataRow2 = checkListData.CheckListTaskTable.NewRow();
				DateTime today = DateTime.Today;
				dataRow2["CheckListID"] = dataRow["CheckListID"];
				dataRow2["CheckListType"] = dataRow["CheckListType"];
				dataRow2["Status"] = 1;
				dataRow2["AssigneeType"] = dataRow["ApproverType"];
				dataRow2["AssigneeID"] = dataRow["ApproverID"];
				dataRow2["DeadlineDate"] = today.AddDays((int)b);
				dataRow2["DueDate"] = dataRow["StartDate"];
				checkListData.CheckListTaskTable.Rows.Add(dataRow2);
				InsertUpdateCheckListTask(checkListData, isUpdate: false, sqlTransaction);
				if (isUpdate)
				{
					text = accountCheckListData.CheckListTable.Rows[0]["CheckListID"].ToString();
					AddActivityLog("CheckList", text, ActivityTypes.Update, sqlTransaction);
				}
				else
				{
					text = accountCheckListData.CheckListTable.Rows[0]["CheckListID"].ToString();
					AddActivityLog("CheckList", text, ActivityTypes.Add, sqlTransaction);
				}
				UpdateTableRowInsertUpdateInfo("CheckList", "CheckListID", text, sqlTransaction, !isUpdate);
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

		private DateTime CalculateTaskDueDate(Intervals interval, DateTime date)
		{
			switch (interval)
			{
			case Intervals.Daily:
				date = date.AddDays(1.0);
				break;
			case Intervals.Weekly:
				date.AddDays(7.0);
				break;
			}
			return date;
		}

		private CheckListStatus GetCheckListTaskStatus(int taskID, SqlTransaction sqlTransaction)
		{
			try
			{
				new DataSet();
				string exp = " SELECT Status FROM CheckList_Task WHERE  TaskID = " + taskID;
				object obj = ExecuteScalar(exp, sqlTransaction);
				if ((obj != null) & (obj.ToString() != ""))
				{
					return (CheckListStatus)int.Parse(obj.ToString());
				}
				return CheckListStatus.Pending;
			}
			catch
			{
				throw;
			}
		}

		private CheckListStatus GetCheckListTaskStatus(CheckListTypes checkListType, string checkListID, int levelID, byte objectType, string docCode, string docSysDocID, int taskID, SqlTransaction sqlTransaction)
		{
			try
			{
				new DataSet();
				string text = " SELECT Status FROM CheckList_Task WHERE CheckListType = " + (int)checkListType + " AND ISNULL(IsExpired,'False') = 'False' AND CheckListID = '" + checkListID + "' AND LevelID = " + levelID + " AND ObjectType = " + objectType + " AND DocumentCode = '" + docCode + "' ";
				if (docSysDocID != "")
				{
					text = text + " AND DocumentSysDocID = '" + docSysDocID + "'";
				}
				if (taskID != -1)
				{
					text = text + " AND TaskID = " + taskID;
				}
				object obj = ExecuteScalar(text, sqlTransaction);
				if ((obj != null) & (obj.ToString() != ""))
				{
					return (CheckListStatus)int.Parse(obj.ToString());
				}
				return CheckListStatus.Pending;
			}
			catch
			{
				throw;
			}
		}

		private DataSet GetCheckListTaskByID(int taskID)
		{
			try
			{
				DataSet dataSet = new DataSet();
				string textCommand = " SELECT * FROM CheckList_Task WHERE   TaskID = " + taskID;
				FillDataSet(dataSet, "Task", textCommand);
				return dataSet;
			}
			catch
			{
				throw;
			}
		}

		public byte GetCheckListTaskStatusByID(int taskID)
		{
			try
			{
				string exp = " SELECT Status FROM CheckList_Task WHERE  TaskID = " + taskID;
				object obj = ExecuteScalar(exp);
				if (!obj.IsNullOrEmpty())
				{
					return byte.Parse(obj.ToString());
				}
				return 0;
			}
			catch
			{
				throw;
			}
		}

		public bool ApproveRejectTask(int taskID, bool isCompleted, string tableName, string idColumnName)
		{
			bool result = true;
			try
			{
				DataSet checkListTaskByID = GetCheckListTaskByID(taskID);
				if (checkListTaskByID == null || checkListTaskByID.Tables["Task"].Rows.Count == 0)
				{
					throw new CompanyException("CheckList task not found.");
				}
				DataRow dataRow = checkListTaskByID.Tables["Task"].Rows[0];
				dataRow["CheckListID"].ToString();
				byte.Parse(dataRow["CheckListType"].ToString());
				base.DBConfig.StartNewTransaction();
				CommonLib.ToSqlDateTimeString(DateTime.Now);
				return result;
			}
			catch
			{
				result = false;
				throw;
			}
			finally
			{
				base.DBConfig.EndTransaction(result);
			}
		}

		internal bool DeleteCheckListTasks(CheckListTypes checkListType, string checkListID, SqlTransaction sqlTransaction)
		{
			bool flag = true;
			try
			{
				new PurchaseOrderData();
				string commandText = "DELETE FROM CheckList_Task WHERE CheckListTask = " + (int)checkListType + " AND CheckListID = '" + checkListID + "'";
				return Delete(commandText, sqlTransaction);
			}
			catch
			{
				throw;
			}
		}

		internal bool CreateCardCheckListTasks(DataComboType cardType, string cardID, string tableName, string idColumName, SqlTransaction sqlTransaction)
		{
			return CreateObjectsCheckListTasks(2, (int)cardType, cardID, "", tableName, idColumName, sqlTransaction);
		}

		internal bool CreateTransactionCheckListTasks(SysDocTypes sysDocType, string sysDocID, string voucherID, string tableName, SqlTransaction sqlTransaction)
		{
			return CreateObjectsCheckListTasks(1, (int)sysDocType, voucherID, sysDocID, tableName, "VoucherID", sqlTransaction);
		}

		private bool CreateObjectsCheckListTasks(int objectType, int objectID, string entityID, string sysDocID, string entityTableName, string idColumnName, SqlTransaction sqlTransaction)
		{
			bool flag = true;
			try
			{
				string textCommand = "SELECT * FROM CheckList WHERE ObjectType = " + objectType + " AND ObjectID = " + objectID + " AND ISNULL(IsInactive,'False') = 'False' ";
				CheckListData checkListData = new CheckListData();
				DataSet dataSet = new DataSet();
				FillDataSet(dataSet, "CheckList", textCommand, sqlTransaction);
				if (dataSet == null || dataSet.Tables["CheckList"].Rows.Count == 0)
				{
					return true;
				}
				foreach (DataRow row in dataSet.Tables["CheckList"].Rows)
				{
					string text = row["CheckListID"].ToString();
					int num = int.Parse(row["CheckListType"].ToString());
					DataSet dataSet2 = new DataSet();
					textCommand = "SELECT * FROM CheckList_Level WHERE CheckListType = " + num + " AND CheckListID = '" + text + "'";
					FillDataSet(dataSet2, "CheckList_Level", textCommand, sqlTransaction);
					foreach (DataRow row2 in dataSet2.Tables["CheckList_Level"].Rows)
					{
						int num2 = int.Parse(row2["ApproverType"].ToString());
						string text2 = row2["Condition"].ToString().Trim();
						if (text2 != "")
						{
							textCommand = ((objectType != 1) ? ("SELECT COUNT (*) FROM " + entityTableName + " WHERE " + idColumnName + " = '" + entityID + "' AND (" + text2 + ")") : ((!(sysDocID != "")) ? ("SELECT COUNT (*) FROM " + entityTableName + " WHERE  VoucherID = '" + entityID + "' AND (" + text2 + ")") : ("SELECT COUNT (*) FROM " + entityTableName + " WHERE SysDocID = '" + sysDocID + "' AND VoucherID = '" + entityID + "' AND (" + text2 + ")")));
							object obj2 = ExecuteScalar(textCommand, sqlTransaction);
							if (obj2.IsNullOrEmpty() || int.Parse(obj2.ToString()) == 0)
							{
								continue;
							}
						}
						DataRow dataRow2 = checkListData.Tables["CheckList_Task"].NewRow();
						dataRow2["CheckListID"] = text;
						dataRow2["CheckListType"] = num;
						if (num2 == 3)
						{
							textCommand = ((!(sysDocID != "")) ? ("SELECT TOP 1 UserID FROM Users WHERE DefaultSalespersonID = (SELECT SalespersonID FROM " + entityTableName + " WHERE VoucherID = '" + entityID + "') ") : ("SELECT TOP 1 UserID FROM Users WHERE DefaultSalespersonID = (SELECT SalespersonID FROM " + entityTableName + " WHERE SysDocID = '" + sysDocID + "' AND VoucherID = '" + entityID + "') "));
							object obj3 = ExecuteScalar(textCommand, sqlTransaction);
							if (obj3.IsNullOrEmpty())
							{
								continue;
							}
							dataRow2["AssigneeID"] = obj3.ToString();
						}
						else
						{
							dataRow2["AssigneeID"] = row2["ApproverID"];
						}
						dataRow2["DateCreated"] = DateTime.Now;
						dataRow2.EndEdit();
						checkListData.Tables["CheckList_Task"].Rows.Add(dataRow2);
					}
				}
				textCommand = ((objectType != 1 || !(sysDocID != "")) ? ("UPDATE CheckList_Task Set IsExpired= 'True' WHERE  ObjectID = " + objectID + " AND DocumentCode = '" + entityID + "' ") : ("UPDATE CheckList_Task Set IsExpired= 'True' WHERE ObjectID = " + objectID + " AND DocumentSysDocID = '" + sysDocID + "' AND DocumentCode = '" + entityID + "' "));
				flag &= (ExecuteNonQuery(textCommand, sqlTransaction) >= 0);
				if (checkListData.CheckListTaskTable.Rows.Count > 0)
				{
					return flag & InsertUpdateCheckListTask(checkListData, isUpdate: false, sqlTransaction);
				}
				return flag;
			}
			catch
			{
				flag = false;
				throw;
			}
		}

		private bool InsertUpdateCheckListTask(CheckListData accountCheckListData, bool isUpdate, SqlTransaction sqlTransaction)
		{
			bool flag = true;
			SqlCommand insertUpdateCheckListTaskCommand = GetInsertUpdateCheckListTaskCommand(isUpdate: false);
			try
			{
				sqlTransaction = base.DBConfig.StartNewTransaction();
				insertUpdateCheckListTaskCommand.Transaction = sqlTransaction;
				return Insert(accountCheckListData, "CheckList_Task", insertUpdateCheckListTaskCommand);
			}
			catch
			{
				flag = false;
				throw;
			}
		}

		public DataSet GetCheckListStatus(string cardID)
		{
			return GetCheckListStatus(2, cardID, "");
		}

		public DataSet GetCheckListStatus(string sysDocID, string voucherID)
		{
			return GetCheckListStatus(1, voucherID, sysDocID);
		}

		private DataSet GetCheckListStatus(int objectType, string entityID, string sysDocID)
		{
			try
			{
				DataSet dataSet = new DataSet();
				string text = "";
				if (sysDocID != "")
				{
					text = " AND DocumentSysDocID = '" + sysDocID + "'";
				}
				string textCommand = "SELECT TOP 1 CASE WHEN Exists(SELECT * FROM CheckList_Task WHERE ISNULL(IsExpired,'False') = 'False' AND ObjectType = " + objectType + " AND DocumentCode =  '" + entityID + "' " + text + " ) THEN 1 ELSE 0 END AS IsCheckList,\r\n                                    (SELECT CASE WHEN Exists (SELECT * FROM CheckList_Task WHERE ISNULL(IsExpired,'False') = 'False' AND ObjectType = " + objectType + " AND DocumentCode =  '" + entityID + "' " + text + " AND Status = 3)  THEN 3 \r\n\t                                WHEN Exists (SELECT * FROM CheckList_Task WHERE ISNULL(IsExpired,'False') = 'False' AND ObjectType = " + objectType + " AND DocumentCode =  '" + entityID + "' " + text + " AND Status <> 10) THEN 1 ELSE 10 END AS CheckListStatus) AS CheckListStatus ";
				FillDataSet(dataSet, "CheckList", textCommand);
				return dataSet;
			}
			catch
			{
				throw;
			}
		}

		public CheckListData GetCheckList()
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddTable("CheckList");
			CheckListData checkListData = new CheckListData();
			sqlBuilder.UseDistinct = false;
			FillDataSet(checkListData, "CheckList", sqlBuilder);
			return checkListData;
		}

		public bool DeleteCheckList(CheckListTypes checkListType, string checkListID)
		{
			bool flag = true;
			try
			{
				string commandText = "DELETE FROM CheckList_Task WHERE  CheckListType = " + (int)checkListType + " AND  CheckListID = '" + checkListID + "'";
				flag &= Delete(commandText, null);
				commandText = "DELETE FROM CheckList WHERE CheckListType = " + (int)checkListType + " AND  CheckListID = '" + checkListID + "'";
				flag &= Delete(commandText, null);
				if (!flag)
				{
					return flag;
				}
				AddActivityLog("CheckList", checkListID, ActivityTypes.Delete, null);
				return flag;
			}
			catch
			{
				throw;
			}
		}

		public CheckListData GetCheckListByID(CheckListTypes checkListType, string id)
		{
			CheckListData checkListData = new CheckListData();
			string textCommand = "SELECT * FROM CheckList WHERE CheckListType = " + (int)checkListType + " AND  CheckListID = '" + id + "'";
			FillDataSet(checkListData, "CheckList", textCommand);
			return checkListData;
		}

		public DataSet GetCheckListByFields(params string[] columns)
		{
			return GetCheckListByFields(null, isInactive: true, columns);
		}

		public DataSet GetCheckListByFields(string[] checkListID, params string[] columns)
		{
			return GetCheckListByFields(checkListID, isInactive: true, columns);
		}

		public DataSet GetCheckListByFields(string[] ids, bool isInactive, params string[] columns)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddTable("CheckList");
			foreach (string text in columns)
			{
				ApplicationAssert.CheckCondition(text.IndexOf(".") >= 0, "A table name with the column name must be specified. eg. table.column.", 0);
				string tableName = text.Substring(0, text.IndexOf("."));
				string columnName = text.Substring(text.IndexOf(".") + 1);
				sqlBuilder.AddColumn(tableName, columnName);
			}
			if (ids != null && ids.Length != 0)
			{
				CommandHelper commandHelper = new CommandHelper();
				commandHelper.FieldName = "CheckListID";
				commandHelper.FieldValue = ids;
				commandHelper.SqlFieldType = SqlDbType.NVarChar;
				commandHelper.TableName = "CheckList";
				sqlBuilder.AddCommandHelper(commandHelper);
			}
			DataSet dataSet = new DataSet();
			dataSet.EnforceConstraints = false;
			sqlBuilder.UseDistinct = false;
			FillDataSet(dataSet, "CheckList", sqlBuilder);
			return dataSet;
		}

		public DataSet GetCheckListList()
		{
			DataSet dataSet = new DataSet();
			string textCommand = "SELECT CheckListID [Type Code],CheckListName [Type Name],IsInactive\r\n                           FROM CheckList ";
			FillDataSet(dataSet, "CheckList", textCommand);
			return dataSet;
		}

		public DataSet GetCheckListComboList(CheckListTypes checkListType)
		{
			DataSet dataSet = new DataSet();
			string textCommand = "SELECT CheckListID [Code], CheckListName [Name]\r\n                           FROM CheckList WHERE CheckListType = " + (int)checkListType + " ORDER BY CheckListID,CheckListName";
			FillDataSet(dataSet, "CheckList", textCommand);
			return dataSet;
		}

		public DataSet GetUserCheckListsWithPendingTasks(CheckListTypes checkListType)
		{
			try
			{
				DataSet dataSet = new DataSet();
				string userID = base.DBConfig.UserID;
				string textCommand = "SELECT DISTINCT * FROM (SELECT AP.CheckListID,AP.CheckListName, AP.CheckListType,\r\n                                (SELECT COUNT (*) FROM CheckList_Task AT WHERE AT.CheckListType = AP.CheckListType AND AT.CheckListID = AP.CheckListID AND (AT.AssigneeType = 3 AND AT.AssigneeID = '" + userID + "' OR AT.AssigneeID = AP.ApproverID) AND \r\n                                AT.AssigneeType = AP.ApproverType \r\n                                AND AT.Status = 1 AND DueDate <= GetDate()) TaskCount FROM CheckList AP  \r\n                                 WHERE  Ap.CheckListType = " + (byte)checkListType + " AND ((AP.ApproverType = 1 AND ApproverID = '" + userID + "') OR (ApproverType = 3) OR (ApproverType =2 AND ApproverID \r\n                                 IN (SELECT GroupID FROM User_Group_Detail UG WHERE UG.UserID = '" + userID + "'))) ) AS AP WHERE TaskCount > 0 ";
				FillDataSet(dataSet, "CheckList", textCommand);
				return dataSet;
			}
			catch
			{
				throw;
			}
		}

		public DataSet GetDocumentCheckListDetail(int objectType, int objectID, string objectCode, string sysDocID)
		{
			try
			{
				DataSet dataSet = new DataSet();
				_ = base.DBConfig.UserID;
				string str = "SELECT   AssigneeType,AssigneeID,ApproverID,DateCompleted,Status FROM CheckList_Task\r\n                                        WHERE ISNULL(IsExpired,'False') = 'False' AND ObjectType = " + objectType + " AND ObjectID = " + objectID + " AND DocumentCode = '" + objectCode + "'  ";
				if (objectType == 1)
				{
					str = str + " AND DocumentSysDocID = '" + sysDocID + "' ";
				}
				str += " ORDER BY LevelID";
				FillDataSet(dataSet, "CheckList", str);
				return dataSet;
			}
			catch
			{
				throw;
			}
		}

		public bool CloseTask(string taskID, string remarks)
		{
			bool flag = true;
			try
			{
				new DataSet();
				string userID = base.DBConfig.UserID;
				string text = CommonLib.ToSqlDateTimeString(DateTime.Now);
				string text2 = "";
				SqlTransaction sqlTransaction = base.DBConfig.StartNewTransaction();
				if (int.Parse(new Databases(base.DBConfig).GetFieldValue("CheckList_Task", "Status", "TaskID", taskID, sqlTransaction).ToString()) != 1)
				{
					throw new CompanyException("This task is not open.", 1057);
				}
				text2 = "UPDATE CheckList_Task SET CompletedBy = '" + userID + "',DateCompleted = '" + text + "' , CompletedRemarks = '" + remarks + "',Status = 10 WHERE TaskID = " + taskID;
				flag &= (ExecuteNonQuery(text2, sqlTransaction) > 0);
				DataSet dataSet = new DataSet();
				text2 = "SELECT CL.CheckListID,ApproverType,ApproverID,Interval,CL.CheckListType,DeadLineDays,ISNULL(CL.IsInactive,'False') AS IsInactive,CLT.DueDate AS LastDate\r\n                            FROM CheckList CL INNER JOIN CheckList_Task CLT ON CL.CheckListID = CLT.CheckListID\r\n                            WHERE CLT.taskID = " + taskID;
				FillDataSet(dataSet, "CheckList", text2, sqlTransaction);
				DataRow dataRow = dataSet.Tables["CheckList"].Rows[0];
				CheckListData checkListData = new CheckListData();
				DateTime dateTime = DateTime.Parse(dataRow["LastDate"].ToString());
				Intervals intervals = (Intervals)int.Parse(dataRow["Interval"].ToString());
				int num = int.Parse(dataRow["DeadlineDays"].ToString());
				if (!bool.Parse(dataRow["IsInactive"].ToString()))
				{
					switch (intervals)
					{
					case Intervals.Daily:
						dateTime = dateTime.AddDays(1.0);
						break;
					case Intervals.Weekly:
						dateTime.AddDays(7.0);
						break;
					case Intervals.Biweekly:
						dateTime = dateTime.AddDays(14.0);
						break;
					case Intervals.Monthly:
						dateTime = new DateTime(dateTime.AddMonths(1).Year, dateTime.AddMonths(1).Month, dateTime.Day);
						break;
					case Intervals.Quarterly:
						dateTime = new DateTime(dateTime.AddMonths(3).Year, dateTime.AddMonths(3).Month, dateTime.Day);
						break;
					case Intervals.Yearly:
						dateTime = new DateTime(dateTime.AddYears(1).Year, dateTime.AddYears(1).Month, dateTime.Day);
						break;
					default:
						throw new CompanyException("interval not implimented.");
					}
					DataRow dataRow2 = checkListData.CheckListTaskTable.NewRow();
					dataRow2["CheckListID"] = dataRow["CheckListID"];
					dataRow2["CheckListType"] = dataRow["CheckListType"];
					dataRow2["Status"] = 1;
					dataRow2["AssigneeType"] = dataRow["ApproverType"];
					dataRow2["AssigneeID"] = dataRow["ApproverID"];
					dataRow2["DueDate"] = dateTime;
					dataRow2["DeadlineDate"] = dateTime.AddDays(num);
					checkListData.CheckListTaskTable.Rows.Add(dataRow2);
					InsertUpdateCheckListTask(checkListData, isUpdate: false, sqlTransaction);
				}
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

		public DataSet GetUserPendingCheckListTasks(CheckListTypes checkListType, string checkListID)
		{
			try
			{
				DataSet dataSet = new DataSet();
				string userID = base.DBConfig.UserID;
				string text = "";
				text = "SELECT TaskID,CL.CheckListName [Description],CLT.Status,DueDate,DeadlineDate,CASE WHEN CLT.Status='1' THEN 'TRUE' ELSE 'FALSE' END AS [TaskStatus] FROM CheckList_Task CLT\r\n                            INNER JOIN CheckList CL ON CL.CheckListID = CLT.CheckListID  WHERE  ((AssigneeType IN (1,3) AND AssigneeID = '" + userID + "') OR (AssigneeType = 2 AND AssigneeID \r\n                                    IN (SELECT GroupID FROM User_Group_Detail UG WHERE UG.UserID = '" + userID + "')))  AND DueDate <= GetDate()";
				FillDataSet(dataSet, "CheckList", text);
				return dataSet;
			}
			catch
			{
				throw;
			}
		}
	}
}
