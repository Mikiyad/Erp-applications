using Micromind.Common;
using Micromind.Common.Data;
using Micromind.Common.Libraries;
using System;
using System.Data;
using System.Data.SqlClient;

namespace Micromind.Data
{
	public sealed class Approval : StoreObject
	{
		private const string APPROVALID_PARM = "@ApprovalID";

		private const string APPROVALNAME_PARM = "@ApprovalName";

		private const string APPROVALTYPE_PARM = "@ApprovalType";

		private const string OBJECTTYPE_PARM = "@ObjectType";

		private const string OBJECTID_PARM = "@ObjectID";

		private const string OBJECTSYSDOCID_PARM = "@ObjectSysDocID";

		private const string STATUS_PARM = "@Status";

		private const string UPDATEFIELDNAME1_PARM = "@UpdateFieldName1";

		private const string UPDATEFIELDVALUE1_PARM = "@UpdateFieldValue1";

		private const string UPDATEFIELDNAME2_PARM = "@UpdateFieldName2";

		private const string UPDATEFIELDVALUE2_PARM = "@UpdateFieldValue2";

		private const string ACTIONSETINACTIVE_PARM = "@ActionSetInactive";

		private const string INACTIVE_PARM = "@IsInactive";

		private const string NOTIFYONPRINT_PARM = "@NotifyonPrint";

		private const string ALLOWNEXTTRANSACTION_PARM = "@AllownextTransaction";

		private const string ALLOWTOEDIT_PARM = "@AllowtoEdit";

		private const string CREATEDBY_PARM = "@CreatedBy";

		private const string DATECREATED_PARM = "@DateCreated";

		private const string UPDATEDBY_PARM = "@UpdatedBy";

		private const string DATEUPDATED_PARM = "@DateUpdated";

		private const string APPROVALLEVEL_TABLE = "Approval_Level";

		private const string ROWINDEX_PARM = "@RowIndex";

		private const string APPROVERTYPE_PARM = "@ApproverType";

		private const string APPROVERID_PARM = "@ApproverID";

		private const string PREREQUISITEINDEX_PARM = "@PreRequisiteIndex";

		private const string CONDITION_PARM = "@Condition";

		private const string APPROVALTASK_TABLE = "Approval_Task";

		private const string TASKID_PARM = "@TaskID";

		private const string LEVELID_PARM = "@LevelID";

		private const string ASSIGNEETYPE_PARM = "@AssigneeType";

		private const string ASSIGNEEID_PARM = "@AssigneeID";

		private const string DOCUMENTSYSDOCID_PARM = "@DocumentSysDocID";

		private const string DOCUMENTCODE_PARM = "@DocumentCode";

		private const string DATEAPPROVED_PARM = "@DateApproved";

		private const string APPROVAL_TABLE = "Approval";

		public Approval(Config config)
			: base(config)
		{
		}

		private string GetInsertUpdateText(bool isUpdate)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddInsertUpdateParameters("Approval", new FieldValue("ApprovalID", "@ApprovalID", isUpdateConditionField: true), new FieldValue("ApprovalType", "@ApprovalType", isUpdateConditionField: true), new FieldValue("ApprovalName", "@ApprovalName"), new FieldValue("ObjectType", "@ObjectType"), new FieldValue("ObjectID", "@ObjectID"), new FieldValue("ObjectSysDocID", "@ObjectSysDocID"), new FieldValue("Status", "@Status"), new FieldValue("UpdateFieldName1", "@UpdateFieldName1"), new FieldValue("UpdateFieldValue1", "@UpdateFieldValue1"), new FieldValue("UpdateFieldName2", "@UpdateFieldName2"), new FieldValue("UpdateFieldValue2", "@UpdateFieldValue2"), new FieldValue("ActionSetInactive", "@ActionSetInactive"), new FieldValue("NotifyonPrint", "@NotifyonPrint"), new FieldValue("AllownextTransaction", "@AllownextTransaction"), new FieldValue("AllowtoEdit", "@AllowtoEdit"), new FieldValue("IsInactive", "@IsInactive"));
			if (isUpdate)
			{
				sqlBuilder.AddInsertUpdateParameters("Approval", new FieldValue("DateUpdated", "@DateUpdated", isUpdateConditionField: true, checkForNullValue: true));
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
			parameters.Add("@ApprovalID", SqlDbType.NVarChar);
			parameters.Add("@ApprovalName", SqlDbType.NVarChar);
			parameters.Add("@ApprovalType", SqlDbType.TinyInt);
			parameters.Add("@ObjectType", SqlDbType.TinyInt);
			parameters.Add("@ObjectID", SqlDbType.NVarChar);
			parameters.Add("@ObjectSysDocID", SqlDbType.NVarChar);
			parameters.Add("@Status", SqlDbType.TinyInt);
			parameters.Add("@UpdateFieldName1", SqlDbType.NVarChar);
			parameters.Add("@UpdateFieldValue1", SqlDbType.NVarChar);
			parameters.Add("@UpdateFieldName2", SqlDbType.NVarChar);
			parameters.Add("@UpdateFieldValue2", SqlDbType.NVarChar);
			parameters.Add("@ActionSetInactive", SqlDbType.Bit);
			parameters.Add("@IsInactive", SqlDbType.Bit);
			parameters.Add("@NotifyonPrint", SqlDbType.Bit);
			parameters.Add("@AllownextTransaction", SqlDbType.Bit);
			parameters.Add("@AllowtoEdit", SqlDbType.Bit);
			parameters["@ApprovalID"].SourceColumn = "ApprovalID";
			parameters["@ApprovalName"].SourceColumn = "ApprovalName";
			parameters["@ApprovalType"].SourceColumn = "ApprovalType";
			parameters["@ObjectType"].SourceColumn = "ObjectType";
			parameters["@ObjectID"].SourceColumn = "ObjectID";
			parameters["@ObjectSysDocID"].SourceColumn = "ObjectSysDocID";
			parameters["@Status"].SourceColumn = "Status";
			parameters["@UpdateFieldName1"].SourceColumn = "UpdateFieldName1";
			parameters["@UpdateFieldValue1"].SourceColumn = "UpdateFieldValue1";
			parameters["@UpdateFieldName2"].SourceColumn = "UpdateFieldName2";
			parameters["@UpdateFieldValue2"].SourceColumn = "UpdateFieldValue2";
			parameters["@ActionSetInactive"].SourceColumn = "ActionSetInactive";
			parameters["@IsInactive"].SourceColumn = "IsInactive";
			parameters["@NotifyonPrint"].SourceColumn = "NotifyonPrint";
			parameters["@AllownextTransaction"].SourceColumn = "AllownextTransaction";
			parameters["@AllowtoEdit"].SourceColumn = "AllowtoEdit";
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

		private string GetInsertUpdateApprovalLevelText(bool isUpdate)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddInsertUpdateParameters("Approval_Level", new FieldValue("ApprovalID", "@ApprovalID"), new FieldValue("RowIndex", "@RowIndex"), new FieldValue("ApprovalType", "@ApprovalType"), new FieldValue("ApproverType", "@ApproverType"), new FieldValue("ApproverID", "@ApproverID"), new FieldValue("PreRequisiteIndex", "@PreRequisiteIndex"), new FieldValue("Condition", "@Condition"));
			if (isUpdate)
			{
				sqlBuilder.AddInsertUpdateParameters("Approval", new FieldValue("DateUpdated", "@DateUpdated", isUpdateConditionField: true, checkForNullValue: true));
			}
			if (isUpdate)
			{
				return sqlBuilder.GetUpdateExpression();
			}
			return sqlBuilder.GetInsertExpression();
		}

		private SqlCommand GetInsertUpdateApprovalLevelCommand(bool isUpdate)
		{
			SqlParameterCollection parameters;
			if (isUpdate)
			{
				updateCommand = new SqlCommand(GetInsertUpdateApprovalLevelText(isUpdate: true), base.DBConfig.Connection);
				updateCommand.CommandType = CommandType.Text;
				parameters = updateCommand.Parameters;
			}
			else
			{
				insertCommand = new SqlCommand(GetInsertUpdateApprovalLevelText(isUpdate: false), base.DBConfig.Connection);
				insertCommand.CommandType = CommandType.Text;
				parameters = insertCommand.Parameters;
			}
			parameters.Add("@ApprovalID", SqlDbType.NVarChar);
			parameters.Add("@ApprovalType", SqlDbType.TinyInt);
			parameters.Add("@RowIndex", SqlDbType.Int);
			parameters.Add("@ApproverType", SqlDbType.TinyInt);
			parameters.Add("@ApproverID", SqlDbType.NVarChar);
			parameters.Add("@PreRequisiteIndex", SqlDbType.Int);
			parameters.Add("@Condition", SqlDbType.NVarChar);
			parameters["@ApprovalID"].SourceColumn = "ApprovalID";
			parameters["@RowIndex"].SourceColumn = "RowIndex";
			parameters["@ApprovalType"].SourceColumn = "ApprovalType";
			parameters["@ApproverType"].SourceColumn = "ApproverType";
			parameters["@ApproverID"].SourceColumn = "ApproverID";
			parameters["@PreRequisiteIndex"].SourceColumn = "PreRequisiteIndex";
			parameters["@Condition"].SourceColumn = "Condition";
			if (isUpdate)
			{
				return updateCommand;
			}
			return insertCommand;
		}

		private string GetInsertUpdateApprovalTaskText(bool isUpdate)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddInsertUpdateParameters("Approval_Task", new FieldValue("ApprovalID", "@ApprovalID"), new FieldValue("LevelID", "@LevelID"), new FieldValue("AssigneeType", "@AssigneeType"), new FieldValue("ApprovalType", "@ApprovalType"), new FieldValue("AssigneeID", "@AssigneeID"), new FieldValue("DocumentSysDocID", "@DocumentSysDocID"), new FieldValue("DocumentCode", "@DocumentCode"), new FieldValue("PreRequisiteIndex", "@PreRequisiteIndex"), new FieldValue("DateApproved", "@DateApproved"), new FieldValue("Status", "@Status"), new FieldValue("DateCreated", "@DateCreated"), new FieldValue("ObjectType", "@ObjectType"), new FieldValue("ObjectID", "@ObjectID"));
			if (isUpdate)
			{
				return sqlBuilder.GetUpdateExpression();
			}
			return sqlBuilder.GetInsertExpression();
		}

		private SqlCommand GetInsertUpdateApprovalTaskCommand(bool isUpdate)
		{
			insertCommand = new SqlCommand(GetInsertUpdateApprovalTaskText(isUpdate: false), base.DBConfig.Connection);
			insertCommand.CommandType = CommandType.Text;
			SqlParameterCollection parameters = insertCommand.Parameters;
			parameters.Add("@ApprovalID", SqlDbType.NVarChar);
			parameters.Add("@LevelID", SqlDbType.Int);
			parameters.Add("@AssigneeType", SqlDbType.TinyInt);
			parameters.Add("@ApprovalType", SqlDbType.TinyInt);
			parameters.Add("@AssigneeID", SqlDbType.NVarChar);
			parameters.Add("@DocumentSysDocID", SqlDbType.NVarChar);
			parameters.Add("@DocumentCode", SqlDbType.NVarChar);
			parameters.Add("@PreRequisiteIndex", SqlDbType.Int);
			parameters.Add("@DateApproved", SqlDbType.DateTime);
			parameters.Add("@Status", SqlDbType.TinyInt);
			parameters.Add("@DateCreated", SqlDbType.DateTime);
			parameters.Add("@ObjectType", SqlDbType.TinyInt);
			parameters.Add("@ObjectID", SqlDbType.Int);
			parameters["@ApprovalID"].SourceColumn = "ApprovalID";
			parameters["@LevelID"].SourceColumn = "LevelID";
			parameters["@AssigneeType"].SourceColumn = "AssigneeType";
			parameters["@ApprovalType"].SourceColumn = "ApprovalType";
			parameters["@AssigneeID"].SourceColumn = "AssigneeID";
			parameters["@DocumentSysDocID"].SourceColumn = "DocumentSysDocID";
			parameters["@PreRequisiteIndex"].SourceColumn = "PreRequisiteIndex";
			parameters["@DocumentCode"].SourceColumn = "DocumentCode";
			parameters["@DateApproved"].SourceColumn = "DateApproved";
			parameters["@Status"].SourceColumn = "Status";
			parameters["@DateCreated"].SourceColumn = "DateCreated";
			parameters["@ObjectType"].SourceColumn = "ObjectType";
			parameters["@ObjectID"].SourceColumn = "ObjectID";
			if (isUpdate)
			{
				return updateCommand;
			}
			return insertCommand;
		}

		public bool InsertUpdateApproval(ApprovalData accountApprovalData, bool isUpdate)
		{
			bool flag = true;
			SqlCommand insertUpdateCommand = GetInsertUpdateCommand(isUpdate);
			SqlTransaction sqlTransaction = null;
			try
			{
				string text = "";
				sqlTransaction = (insertUpdateCommand.Transaction = base.DBConfig.StartNewTransaction());
				flag = ((!isUpdate) ? Insert(accountApprovalData, "Approval", insertUpdateCommand) : Update(accountApprovalData, "Approval", insertUpdateCommand));
				ApprovalTypes approvalType = (ApprovalTypes)byte.Parse(accountApprovalData.ApprovalTable.Rows[0]["ApprovalType"].ToString());
				if (isUpdate)
				{
					text = accountApprovalData.ApprovalTable.Rows[0]["ApprovalID"].ToString();
					flag &= DeleteApprovalLevels(approvalType, text, sqlTransaction);
				}
				insertUpdateCommand = GetInsertUpdateApprovalLevelCommand(isUpdate: false);
				insertUpdateCommand.Transaction = sqlTransaction;
				flag &= Insert(accountApprovalData, "Approval_Level", insertUpdateCommand);
				if (isUpdate)
				{
					text = accountApprovalData.ApprovalTable.Rows[0]["ApprovalID"].ToString();
					AddActivityLog("Approval", text, ActivityTypes.Update, sqlTransaction);
				}
				else
				{
					text = accountApprovalData.ApprovalTable.Rows[0]["ApprovalID"].ToString();
					AddActivityLog("Approval", text, ActivityTypes.Add, sqlTransaction);
				}
				UpdateTableRowInsertUpdateInfo("Approval", "ApprovalID", text, sqlTransaction, !isUpdate);
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

		private ApprovalStatus GetApprovalTaskStatus(int taskID, SqlTransaction sqlTransaction)
		{
			try
			{
				new DataSet();
				string exp = " SELECT Status FROM Approval_Task WHERE  TaskID = " + taskID;
				object obj = ExecuteScalar(exp, sqlTransaction);
				if ((obj != null) & (obj.ToString() != ""))
				{
					return (ApprovalStatus)int.Parse(obj.ToString());
				}
				return ApprovalStatus.Pending;
			}
			catch
			{
				throw;
			}
		}

		private ApprovalStatus GetApprovalTaskStatus(ApprovalTypes approvalType, string approvalID, int levelID, byte objectType, string docCode, string docSysDocID, int taskID, SqlTransaction sqlTransaction)
		{
			try
			{
				new DataSet();
				string text = " SELECT Status FROM Approval_Task WHERE ApprovalType = " + (int)approvalType + " AND ISNULL(IsExpired,'False') = 'False' AND ApprovalID = '" + approvalID + "' AND LevelID = " + levelID + " AND ObjectType = " + objectType + " AND DocumentCode = '" + docCode + "' ";
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
					return (ApprovalStatus)int.Parse(obj.ToString());
				}
				return ApprovalStatus.Pending;
			}
			catch
			{
				throw;
			}
		}

		private DataSet GetApprovalTaskByID(int taskID)
		{
			try
			{
				DataSet dataSet = new DataSet();
				string textCommand = " SELECT * FROM Approval_Task WHERE   TaskID = " + taskID;
				FillDataSet(dataSet, "Task", textCommand);
				return dataSet;
			}
			catch
			{
				throw;
			}
		}

		public byte GetApprovalTaskStatusByID(int taskID)
		{
			try
			{
				string exp = " SELECT Status FROM Approval_Task WHERE  TaskID = " + taskID;
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

		private int GetJournalID(string sysDocID, string voucherID, SqlTransaction sqlTransaction)
		{
			string exp = "SELECT JournalID FROM Journal WHERE SysDocID='" + sysDocID + "' AND VoucherID='" + voucherID + "'";
			object obj = ExecuteScalar(exp, sqlTransaction);
			if (obj != null && obj.ToString() != "")
			{
				return int.Parse(obj.ToString());
			}
			return -1;
		}

		public bool ApproveRejectTaskVerification(int taskID, bool isApproved, string tableName, string idColumnName)
		{
			bool flag = true;
			try
			{
				DataSet approvalTaskByID = GetApprovalTaskByID(taskID);
				if (approvalTaskByID == null || approvalTaskByID.Tables["Task"].Rows.Count == 0)
				{
					throw new CompanyException("Approval task not found.");
				}
				DataRow dataRow = approvalTaskByID.Tables["Task"].Rows[0];
				string text = dataRow["ApprovalID"].ToString();
				ApprovalTypes approvalTypes = (ApprovalTypes)byte.Parse(dataRow["ApprovalType"].ToString());
				int num = int.Parse(dataRow["LevelID"].ToString());
				byte b = byte.Parse(dataRow["ObjectType"].ToString());
				string text2 = dataRow["DocumentCode"].ToString();
				string text3 = dataRow["DocumentSysDocID"].ToString();
				if (!dataRow["PreRequisiteIndex"].IsDBNullOrEmpty())
				{
					int.Parse(dataRow["PreRequisiteIndex"].ToString());
				}
				ApprovalStatus approvalTaskStatus = GetApprovalTaskStatus(approvalTypes, text, num, b, text2, text3, taskID, null);
				if (approvalTaskStatus != ApprovalStatus.Pending)
				{
					throw new CompanyException("This approval task is in a status that does not allow this action. Status:" + approvalTaskStatus.ToString());
				}
				SqlTransaction sqlTransaction = base.DBConfig.StartNewTransaction();
				int num2 = 10;
				if (!isApproved)
				{
					num2 = 3;
				}
				string text4 = CommonLib.ToSqlDateTimeString(DateTime.Now);
				string exp = "UPDATE Approval_Task SET Status = " + num2 + ", DateApproved = '" + text4 + "', ApproverID = '" + base.DBConfig.UserID + "' WHERE TaskID = " + taskID;
				flag &= (ExecuteNonQuery(exp, sqlTransaction) > 0);
				int journalID = GetJournalID(text3, text2, sqlTransaction);
				switch (approvalTypes)
				{
				case ApprovalTypes.Approval:
					exp = ((b != 1) ? ("UPDATE " + tableName + " SET ApprovalStatus = " + num2.ToString() + " WHERE " + idColumnName + " = '" + text2 + "'") : ("UPDATE " + tableName + " SET ApprovalStatus = " + num2.ToString() + "  WHERE SysDocID = '" + text3 + "' AND " + idColumnName + " = '" + text2 + "'"));
					break;
				case ApprovalTypes.Verification:
					exp = ((b != 1) ? ("UPDATE " + tableName + " SET VerificationStatus = " + num2.ToString() + " WHERE " + idColumnName + " = '" + text2 + "'") : ("UPDATE " + tableName + " SET VerificationStatus = " + num2.ToString() + "  WHERE SysDocID = '" + text3 + "' AND " + idColumnName + " = '" + text2 + "'"));
					break;
				}
				flag &= (ExecuteNonQuery(exp, sqlTransaction) > 0);
				if (journalID != -1)
				{
					exp = "UPDATE Journal SET ApprovalStatus = " + num2.ToString() + "  WHERE SysDocID = '" + text3 + "' AND VoucherID = '" + text2 + "'";
					flag &= (ExecuteNonQuery(exp, sqlTransaction) > 0);
				}
				if (num2 != 10)
				{
					return flag;
				}
				exp = "UPDATE Approval_Task SET Status = " + 1 + " WHERE ApprovalType = " + (int)approvalTypes + " AND  ApprovalID = '" + text + "' AND ObjectType = " + b + " AND DocumentCode = '" + text2 + "' AND ISNULL(PreRequisiteIndex,-1) =  " + num + " AND Status = " + 2;
				if (text3 != "")
				{
					exp = exp + " AND DocumentSysDocID = '" + text3 + "'";
				}
				flag &= (ExecuteNonQuery(exp, sqlTransaction) >= 0);
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

		public bool ApproveRejectTask(int taskID, bool isApproved, string tableName, string idColumnName)
		{
			bool flag = true;
			try
			{
				DataSet approvalTaskByID = GetApprovalTaskByID(taskID);
				if (approvalTaskByID == null || approvalTaskByID.Tables["Task"].Rows.Count == 0)
				{
					throw new CompanyException("Approval task not found.");
				}
				DataRow dataRow = approvalTaskByID.Tables["Task"].Rows[0];
				string text = dataRow["ApprovalID"].ToString();
				ApprovalTypes approvalTypes = (ApprovalTypes)byte.Parse(dataRow["ApprovalType"].ToString());
				int num = int.Parse(dataRow["LevelID"].ToString());
				byte b = byte.Parse(dataRow["ObjectType"].ToString());
				string text2 = dataRow["DocumentCode"].ToString();
				string text3 = dataRow["DocumentSysDocID"].ToString();
				int num2 = -1;
				if (!dataRow["PreRequisiteIndex"].IsDBNullOrEmpty())
				{
					num2 = int.Parse(dataRow["PreRequisiteIndex"].ToString());
				}
				int num3 = 10;
				SqlTransaction sqlTransaction = base.DBConfig.StartNewTransaction();
				DataSet dataSet = new DataSet();
				string textCommand = "SELECT * FROM Approval_Level WHERE ApprovalType = " + b + " AND ApprovalID = '" + text + "'";
				FillDataSet(dataSet, "Approval_Level", textCommand, sqlTransaction);
				if (dataSet != null && dataSet.Tables.Count > 0 && dataSet.Tables[0].Rows.Count > 0)
				{
					_ = dataSet.Tables[0].Rows.Count;
				}
				int result = 0;
				int.TryParse(dataSet.Tables[0].Select("ApproverID='" + base.DBConfig.UserID + "' ")[0]["RowIndex"].ToString(), out result);
				if (result > 0 && num2 != -1)
				{
					for (int num4 = result - 1; num4 >= 0; num4--)
					{
						ApprovalStatus approvalTaskStatus = GetApprovalTaskStatus(approvalTypes, text, num4, b, text2, text3, -1, sqlTransaction);
						if (approvalTaskStatus != ApprovalStatus.Approved)
						{
							throw new CompanyException("This approval task is in a status that does not allow this action. Status:" + approvalTaskStatus.ToString());
						}
					}
				}
				DataRow[] array = dataSet.Tables[0].Select("RowIndex >" + result);
				num3 = ((!isApproved) ? 3 : ((array.Length != 0) ? 1 : 10));
				GetApprovalTaskStatus(approvalTypes, text, num, b, text2, text3, taskID, sqlTransaction);
				string text4 = CommonLib.ToSqlDateTimeString(DateTime.Now);
				if (isApproved)
				{
					textCommand = "UPDATE Approval_Task SET Status = " + 10 + ", DateApproved = '" + text4 + "', ApproverID = '" + base.DBConfig.UserID + "' WHERE TaskID = " + taskID;
					flag &= (ExecuteNonQuery(textCommand, sqlTransaction) > 0);
				}
				else
				{
					textCommand = "UPDATE Approval_Task SET Status = " + 3 + ", DateApproved = '" + text4 + "', ApproverID = '" + base.DBConfig.UserID + "' WHERE TaskID = " + taskID;
					flag &= (ExecuteNonQuery(textCommand, sqlTransaction) > 0);
				}
				int journalID = GetJournalID(text3, text2, sqlTransaction);
				if (approvalTypes == ApprovalTypes.Approval && (num3 == 10 || num3 == 3))
				{
					textCommand = ((b != 1) ? ("UPDATE " + tableName + " SET ApprovalStatus = " + num3.ToString() + " WHERE " + idColumnName + " = '" + text2 + "'") : ("UPDATE " + tableName + " SET ApprovalStatus = " + num3.ToString() + "  WHERE SysDocID = '" + text3 + "' AND " + idColumnName + " = '" + text2 + "'"));
				}
				else if (approvalTypes == ApprovalTypes.Verification)
				{
					textCommand = ((b != 1) ? ("UPDATE " + tableName + " SET VerificationStatus = " + num3.ToString() + " WHERE " + idColumnName + " = '" + text2 + "'") : ("UPDATE " + tableName + " SET VerificationStatus = " + num3.ToString() + "  WHERE SysDocID = '" + text3 + "' AND " + idColumnName + " = '" + text2 + "'"));
				}
				flag &= (ExecuteNonQuery(textCommand, sqlTransaction) > 0);
				if (journalID != -1)
				{
					textCommand = "UPDATE Journal SET ApprovalStatus = " + num3.ToString() + "  WHERE SysDocID = '" + text3 + "' AND VoucherID = '" + text2 + "'";
					flag &= (ExecuteNonQuery(textCommand, sqlTransaction) > 0);
					textCommand = "UPDATE Journal_Details SET ApprovalStatus = " + num3.ToString() + "  WHERE SysDocID = '" + text3 + "' AND VoucherID = '" + text2 + "'";
					flag &= (ExecuteNonQuery(textCommand, sqlTransaction) > 0);
				}
				if (num3 != 10)
				{
					return flag;
				}
				textCommand = "UPDATE Approval_Task SET Status = " + 1 + " WHERE ApprovalType = " + (int)approvalTypes + " AND  ApprovalID = '" + text + "' AND ObjectType = " + b + " AND DocumentCode = '" + text2 + "' AND ISNULL(PreRequisiteIndex,-1) =  " + num + " AND Status = " + 2;
				if (text3 != "")
				{
					textCommand = textCommand + " AND DocumentSysDocID = '" + text3 + "'";
				}
				flag &= (ExecuteNonQuery(textCommand, sqlTransaction) >= 0);
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

		internal bool DeleteApprovalLevels(ApprovalTypes approvalType, string approvalID, SqlTransaction sqlTransaction)
		{
			bool flag = true;
			try
			{
				new PurchaseOrderData();
				string commandText = "DELETE FROM Approval_Level WHERE ApprovalType = " + (int)approvalType + " AND  ApprovalID = '" + approvalID + "'";
				return Delete(commandText, sqlTransaction);
			}
			catch
			{
				throw;
			}
		}

		internal bool DeleteApprovalTasks(ApprovalTypes approvalType, string approvalID, SqlTransaction sqlTransaction)
		{
			bool flag = true;
			try
			{
				new PurchaseOrderData();
				string commandText = "DELETE FROM Approval_Task WHERE ApprovalTask = " + (int)approvalType + " AND ApprovalID = '" + approvalID + "'";
				return Delete(commandText, sqlTransaction);
			}
			catch
			{
				throw;
			}
		}

		internal bool CreateCardApprovalTasks(DataComboType cardType, string cardID, string tableName, string idColumName, SqlTransaction sqlTransaction)
		{
			return CreateObjectsApprovalTasks(2, (int)cardType, cardID, "", tableName, idColumName, sqlTransaction);
		}

		internal bool CreateTransactionApprovalTasks(SysDocTypes sysDocType, string sysDocID, string voucherID, string tableName, SqlTransaction sqlTransaction)
		{
			return CreateObjectsApprovalTasks(1, (int)sysDocType, voucherID, sysDocID, tableName, "VoucherID", sqlTransaction);
		}

		private bool CreateObjectsApprovalTasks(int objectType, int objectID, string entityID, string sysDocID, string entityTableName, string idColumnName, SqlTransaction sqlTransaction)
		{
			bool flag = true;
			try
			{
				string textCommand = "SELECT * FROM Approval WHERE ObjectType = " + objectType + " AND ObjectID = " + objectID + " AND ISNULL(IsInactive,'False') = 'False' ";
				ApprovalData approvalData = new ApprovalData();
				DataSet dataSet = new DataSet();
				FillDataSet(dataSet, "Approval", textCommand, sqlTransaction);
				if (dataSet == null || dataSet.Tables["Approval"].Rows.Count == 0)
				{
					return true;
				}
				foreach (DataRow row in dataSet.Tables["Approval"].Rows)
				{
					string text = row["ApprovalID"].ToString();
					int num = int.Parse(row["ApprovalType"].ToString());
					if (objectType != 1 || row["ObjectSysDocID"].IsDBNullOrEmpty() || !(sysDocID != row["ObjectSysDocID"].ToString()))
					{
						DataSet dataSet2 = new DataSet();
						textCommand = "SELECT * FROM Approval_Level WHERE ApprovalType = " + num + " AND ApprovalID = '" + text + "'";
						FillDataSet(dataSet2, "Approval_Level", textCommand, sqlTransaction);
						foreach (DataRow row2 in dataSet2.Tables["Approval_Level"].Rows)
						{
							int num2 = int.Parse(row2["ApproverType"].ToString());
							int num3 = int.Parse(row2["RowIndex"].ToString());
							int num4 = -1;
							if (!row2["PreRequisiteIndex"].IsDBNullOrEmpty())
							{
								num4 = int.Parse(row2["PreRequisiteIndex"].ToString());
							}
							string text2 = row2["Condition"].ToString().Trim();
							if (text2 != "")
							{
								textCommand = ((objectType != 1) ? ("SELECT COUNT (*) FROM " + entityTableName + " WHERE " + idColumnName + " = '" + entityID + "' AND (" + text2 + ")") : ((!(sysDocID != "")) ? ("SELECT COUNT (*) FROM " + entityTableName + " WHERE  VoucherID = '" + entityID + "' AND (" + text2 + ")") : ("SELECT COUNT (*) FROM " + entityTableName + " WHERE SysDocID = '" + sysDocID + "' AND VoucherID = '" + entityID + "' AND (" + text2 + ")")));
								object obj = ExecuteScalar(textCommand, sqlTransaction);
								if (obj.IsNullOrEmpty() || int.Parse(obj.ToString()) == 0)
								{
									continue;
								}
							}
							DataRow dataRow3 = approvalData.Tables["Approval_Task"].NewRow();
							dataRow3["ApprovalID"] = text;
							dataRow3["ApprovalType"] = num;
							dataRow3["LevelID"] = num3;
							if (num4 >= 0)
							{
								dataRow3["Status"] = 2;
							}
							else
							{
								dataRow3["Status"] = 1;
							}
							dataRow3["AssigneeType"] = row2["ApproverType"];
							if (num2 == 3)
							{
								textCommand = ((!(sysDocID != "")) ? ("SELECT TOP 1 UserID FROM Users WHERE DefaultSalespersonID = (SELECT SalespersonID FROM " + entityTableName + " WHERE VoucherID = '" + entityID + "') ") : ("SELECT TOP 1 UserID FROM Users WHERE DefaultSalespersonID = (SELECT SalespersonID FROM " + entityTableName + " WHERE SysDocID = '" + sysDocID + "' AND VoucherID = '" + entityID + "') "));
								object obj2 = ExecuteScalar(textCommand, sqlTransaction);
								if (obj2.IsNullOrEmpty())
								{
									continue;
								}
								dataRow3["AssigneeID"] = obj2.ToString();
							}
							else
							{
								dataRow3["AssigneeID"] = row2["ApproverID"];
							}
							dataRow3["DateCreated"] = DateTime.Now;
							dataRow3["ObjectType"] = objectType;
							if (num4 < 0)
							{
								dataRow3["PreRequisiteIndex"] = DBNull.Value;
							}
							else
							{
								dataRow3["PreRequisiteIndex"] = num4;
							}
							dataRow3["ObjectID"] = objectID;
							dataRow3["DocumentSysDocID"] = sysDocID;
							dataRow3["DocumentCode"] = entityID;
							dataRow3.EndEdit();
							approvalData.Tables["Approval_Task"].Rows.Add(dataRow3);
						}
						if (num == 1)
						{
							flag = ((objectType != 1 || !(sysDocID != "")) ? (flag & (new Databases(base.DBConfig).UpdateFieldValue(entityTableName, "ApprovalStatus", 1m, idColumnName, entityID, sqlTransaction) > 0)) : (flag & (new Databases(base.DBConfig).UpdateFieldValue(entityTableName, "ApprovalStatus", 1, idColumnName, entityID, "SysDocID", sysDocID, sqlTransaction) > 0)));
						}
						int journalID = GetJournalID(sysDocID, entityID, sqlTransaction);
						if (num == 1 && journalID != -1 && objectType == 1 && sysDocID != "")
						{
							flag &= (new Databases(base.DBConfig).UpdateFieldValue("Journal", "ApprovalStatus", 1, idColumnName, entityID, "SysDocID", sysDocID, sqlTransaction) > 0);
							flag &= (new Databases(base.DBConfig).UpdateFieldValue("Journal_Details", "ApprovalStatus", 1, idColumnName, entityID, "SysDocID", sysDocID, sqlTransaction) > 0);
						}
						if (num == 2)
						{
							flag = ((objectType != 1 || !(sysDocID != "")) ? (flag & (new Databases(base.DBConfig).UpdateFieldValue(entityTableName, "VerificationStatus", 1m, idColumnName, entityID, sqlTransaction) > 0)) : (flag & (new Databases(base.DBConfig).UpdateFieldValue(entityTableName, "VerificationStatus", 1, idColumnName, entityID, "SysDocID", sysDocID, sqlTransaction) > 0)));
						}
					}
				}
				textCommand = ((objectType != 1 || !(sysDocID != "")) ? ("UPDATE Approval_Task Set IsExpired= 'True' WHERE  ObjectID = " + objectID + " AND DocumentCode = '" + entityID + "' ") : ("UPDATE Approval_Task Set IsExpired= 'True' WHERE ObjectID = " + objectID + " AND DocumentSysDocID = '" + sysDocID + "' AND DocumentCode = '" + entityID + "' "));
				flag &= (ExecuteNonQuery(textCommand, sqlTransaction) >= 0);
				if (approvalData.ApprovalTaskTable.Rows.Count > 0)
				{
					return flag & InsertUpdateApprovalTask(approvalData, isUpdate: false, sqlTransaction);
				}
				return flag;
			}
			catch
			{
				flag = false;
				throw;
			}
		}

		private bool InsertUpdateApprovalTask(ApprovalData accountApprovalData, bool isUpdate, SqlTransaction sqlTransaction)
		{
			bool flag = true;
			SqlCommand insertUpdateApprovalTaskCommand = GetInsertUpdateApprovalTaskCommand(isUpdate: false);
			try
			{
				sqlTransaction = base.DBConfig.StartNewTransaction();
				insertUpdateApprovalTaskCommand.Transaction = sqlTransaction;
				return Insert(accountApprovalData, "Approval_Task", insertUpdateApprovalTaskCommand);
			}
			catch
			{
				flag = false;
				throw;
			}
		}

		public DataSet GetApprovalStatus(string cardID)
		{
			return GetApprovalStatus(2, cardID, "");
		}

		public DataSet GetApprovalStatus(string sysDocID, string voucherID)
		{
			return GetApprovalStatus(1, voucherID, sysDocID);
		}

		private DataSet GetApprovalStatus(int objectType, string entityID, string sysDocID)
		{
			try
			{
				DataSet dataSet = new DataSet();
				string text = "";
				if (sysDocID != "")
				{
					text = " AND DocumentSysDocID = '" + sysDocID + "'";
				}
				string textCommand = "SELECT TOP 1 CASE WHEN Exists(SELECT * FROM Approval_Task WHERE ISNULL(IsExpired,'False') = 'False' AND ObjectType = " + objectType + " AND DocumentCode =  '" + entityID + "' " + text + " ) THEN 1 ELSE 0 END AS IsApproval,\r\n                                    (SELECT CASE WHEN Exists (SELECT * FROM Approval_Task WHERE ISNULL(IsExpired,'False') = 'False' AND ObjectType = " + objectType + " AND DocumentCode =  '" + entityID + "' " + text + " AND Status = 3)  THEN 3 \r\n\t                                WHEN Exists (SELECT * FROM Approval_Task WHERE ISNULL(IsExpired,'False') = 'False' AND ObjectType = " + objectType + " AND DocumentCode =  '" + entityID + "' " + text + " AND Status <> 10) THEN 1 ELSE 10 END AS ApprovalStatus) AS ApprovalStatus ";
				FillDataSet(dataSet, "Approval", textCommand);
				return dataSet;
			}
			catch
			{
				throw;
			}
		}

		public ApprovalData GetApproval()
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddTable("Approval");
			ApprovalData approvalData = new ApprovalData();
			sqlBuilder.UseDistinct = false;
			FillDataSet(approvalData, "Approval", sqlBuilder);
			return approvalData;
		}

		public bool DeleteApproval(ApprovalTypes approvalType, string approvalID)
		{
			bool flag = true;
			try
			{
				string commandText = "DELETE FROM Approval_Level WHERE  ApprovalType = " + (int)approvalType + " AND  ApprovalID = '" + approvalID + "'";
				flag &= Delete(commandText, null);
				commandText = "DELETE FROM Approval WHERE ApprovalType = " + (int)approvalType + " AND  ApprovalID = '" + approvalID + "'";
				flag &= Delete(commandText, null);
				if (!flag)
				{
					return flag;
				}
				AddActivityLog("Approval", approvalID, ActivityTypes.Delete, null);
				return flag;
			}
			catch
			{
				throw;
			}
		}

		public ApprovalData GetApprovalByID(ApprovalTypes approvalType, string id)
		{
			ApprovalData approvalData = new ApprovalData();
			string textCommand = "SELECT * FROM Approval WHERE ApprovalType = " + (int)approvalType + " AND  ApprovalID = '" + id + "'";
			FillDataSet(approvalData, "Approval", textCommand);
			textCommand = "SELECT * FROM Approval_Level WHERE ApprovalType = " + (int)approvalType + " AND   ApprovalID = '" + id + "'";
			FillDataSet(approvalData, "Approval_Level", textCommand);
			return approvalData;
		}

		public DataSet GetApprovalByFields(params string[] columns)
		{
			return GetApprovalByFields(null, isInactive: true, columns);
		}

		public DataSet GetApprovalByFields(string[] approvalID, params string[] columns)
		{
			return GetApprovalByFields(approvalID, isInactive: true, columns);
		}

		public DataSet GetApprovalByFields(string[] ids, bool isInactive, params string[] columns)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddTable("Approval");
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
				commandHelper.FieldName = "ApprovalID";
				commandHelper.FieldValue = ids;
				commandHelper.SqlFieldType = SqlDbType.NVarChar;
				commandHelper.TableName = "Approval";
				sqlBuilder.AddCommandHelper(commandHelper);
			}
			DataSet dataSet = new DataSet();
			dataSet.EnforceConstraints = false;
			sqlBuilder.UseDistinct = false;
			FillDataSet(dataSet, "Approval", sqlBuilder);
			return dataSet;
		}

		public DataSet GetApprovalList()
		{
			DataSet dataSet = new DataSet();
			string textCommand = "SELECT ApprovalID [Type Code],ApprovalName [Type Name],IsInactive\r\n                           FROM Approval WHERE ApprovalType=1";
			FillDataSet(dataSet, "Approval", textCommand);
			return dataSet;
		}

		public DataSet GetVerificationList()
		{
			DataSet dataSet = new DataSet();
			string textCommand = "SELECT ApprovalID [Type Code],ApprovalName [Type Name],IsInactive\r\n                           FROM Approval WHERE ApprovalType=2";
			FillDataSet(dataSet, "Approval", textCommand);
			return dataSet;
		}

		public DataSet GetApprovalComboList(ApprovalTypes approvalType)
		{
			DataSet dataSet = new DataSet();
			string textCommand = "SELECT ApprovalID [Code], ApprovalName [Name]\r\n                           FROM Approval WHERE ApprovalType = " + (int)approvalType + " ORDER BY ApprovalID,ApprovalName";
			FillDataSet(dataSet, "Approval", textCommand);
			return dataSet;
		}

		public DataSet GetUserApprovalsWithPendingTasks(ApprovalTypes approvalType)
		{
			try
			{
				DataSet dataSet = new DataSet();
				string userID = base.DBConfig.UserID;
				string textCommand = "SELECT DISTINCT * FROM (SELECT AP.ApprovalID,AP.ApprovalName, AP.ApprovalType,\r\n                                (SELECT COUNT (*) FROM Approval_Task AT WHERE ISNULL(IsExpired,'False') = 'False'   AND AT.ApprovalType = AP.ApprovalType AND AT.ApprovalID = AP.ApprovalID AND (AT.AssigneeType = 3 AND AT.AssigneeID = '" + userID + "' OR AT.AssigneeID = APL.ApproverID) AND \r\n                                AT.AssigneeType = APL.ApproverType \r\n                                AND AT.Status IN(1,2)) TaskCount FROM Approval AP INNER JOIN Approval_Level APL ON AP.ApprovalID = APL.ApprovalID\r\n                                 WHERE  Ap.ApprovalType = " + (byte)approvalType + " AND ((APL.ApproverType = 1 AND ApproverID = '" + userID + "') OR (ApproverType = 3) OR (ApproverType =2 AND ApproverID \r\n                                 IN (SELECT GroupID FROM User_Group_Detail UG WHERE UG.UserID = '" + userID + "'))) ) AS AP WHERE TaskCount > 0 ";
				FillDataSet(dataSet, "Approval", textCommand);
				return dataSet;
			}
			catch
			{
				throw;
			}
		}

		public int GetUserPendingTasksCount()
		{
			try
			{
				new DataSet();
				string userID = base.DBConfig.UserID;
				string exp = "SELECT  (SELECT  COUNT(*) FROM Approval_Task AT WHERE ISNULL(IsExpired,'False') = 'False' AND Status = 1 AND ApprovalType = 2   \r\n                                    AND  ((AT.AssigneeType  IN (1,3)  AND AssigneeID = '" + userID + "') OR (AssigneeType =2 AND AssigneeID \r\n                                    IN (SELECT GroupID FROM User_Group_Detail UG WHERE UG.UserID = '" + userID + "')))) AS TaskID   WHERE 1=1    AND EXISTS(\r\n                                    SELECT * FROM Approval_Task AT WHERE ISNULL(IsExpired,'False') = 'False' AND ApprovalType = 2   AND ((AT.AssigneeType IN (1,3) AND AssigneeID = '" + userID + "') OR (AssigneeType =2 AND AssigneeID \r\n                                    IN (SELECT GroupID FROM User_Group_Detail UG WHERE UG.UserID = '" + userID + "')))  AND AT.Status = 1  ) ";
				object obj = ExecuteScalar(exp);
				int num = 0;
				if (!obj.IsDBNullOrEmpty())
				{
					num = int.Parse(obj.ToString());
				}
				exp = "SELECT COUNT(*) FROM CheckList_Task CLT\r\n                            INNER JOIN CheckList CL ON CL.CheckListID = CLT.CheckListID  WHERE  ((AssigneeType IN (1,3) AND AssigneeID = '" + userID + "') OR (AssigneeType = 2 AND AssigneeID \r\n                                    IN (SELECT GroupID FROM User_Group_Detail UG WHERE UG.UserID = '" + userID + "'))) AND ISNULL(CLT.Status,1) = 1  AND DueDate <= GetDate()";
				obj = ExecuteScalar(exp);
				if (!obj.IsDBNullOrEmpty())
				{
					num += int.Parse(obj.ToString());
				}
				return num;
			}
			catch
			{
				throw;
			}
		}

		public DataSet GetDocumentApprovalDetail(int objectType, int objectID, string objectCode, string sysDocID)
		{
			try
			{
				DataSet dataSet = new DataSet();
				_ = base.DBConfig.UserID;
				string str = "SELECT   AssigneeType,AssigneeID,ApproverID,DateApproved,Status FROM Approval_Task\r\n                                        WHERE ISNULL(IsExpired,'False') = 'False' AND ObjectType = " + objectType + " AND ObjectID = " + objectID + " AND DocumentCode = '" + objectCode + "'  ";
				if (objectType == 1)
				{
					str = str + " AND DocumentSysDocID = '" + sysDocID + "' ";
				}
				str += " ORDER BY LevelID";
				FillDataSet(dataSet, "Approval", str);
				return dataSet;
			}
			catch
			{
				throw;
			}
		}

		public DataSet GetUserPendingApprovalTasksold(ApprovalTypes approvalType, string approvalID)
		{
			try
			{
				DataSet dataSet = new DataSet();
				string userID = base.DBConfig.UserID;
				string text = "";
				string text2 = "";
				string text3 = "VoucherID";
				string a = "SysDocID";
				ApprovalData approvalByID = GetApprovalByID(approvalType, approvalID);
				byte b = byte.Parse(approvalByID.ApprovalTable.Rows[0]["ObjectType"].ToString());
				string text4 = approvalByID.ApprovalTable.Rows[0]["ObjectID"].ToString();
				string a2 = approvalByID.ApprovalTable.Rows[0]["ObjectSysDocID"].ToString();
				string text5 = "";
				switch (b)
				{
				case 1:
				{
					text5 = " CAST(" + text4 + " AS int) AS ObjectID, CAST(1 as int) AS ObjectType, (SELECT TOP 1 TaskID FROM Approval_Task AT WHERE ISNULL(IsExpired,'False') = 'False' AND Status IN (1,2) AND ApprovalType = " + (int)approvalType + " AND ApprovalID = '" + approvalID + "' AND AT.DocumentCode = Doc.VoucherID  ";
					if (a2 != "")
					{
						text5 += " AND AT.DocumentSysDocID = Doc.SysDocID ";
					}
					text5 = text5 + " AND ((AT.AssigneeType IN (1,3) AND AssigneeID = '" + userID + "') OR (AssigneeType =2 AND AssigneeID \r\n                                    IN (SELECT GroupID FROM User_Group_Detail UG WHERE UG.UserID = '" + userID + "')))) AS TaskID";
					SysDocTypes sysDocTypes = (SysDocTypes)int.Parse(text4);
					switch (sysDocTypes)
					{
					case SysDocTypes.PurchaseOrder:
						text = "SELECT " + text5 + " , SysDocID,VoucherID,Doc.VendorID [Vendor Code],VendorName [Vendor Name],TransactionDate [Date],Discount,(Total-Discount)AS Total,Doc.CreatedBy,Doc.DateCreated, Doc.UpdatedBy,Doc.DateUpdated FROM Purchase_Order Doc INNER JOIN Vendor V ON V.VendorID = Doc.VendorID AND ISNULL(IsImport,'False') = 'False'  WHERE 1=1  AND  ISNULL(IsVoid,'False') = 'False'  ";
						break;
					case SysDocTypes.ImportPurchaseOrder:
						text = "SELECT " + text5 + " , SysDocID,VoucherID,Doc.VendorID [Vendor Code],VendorName [Vendor Name],TransactionDate [Date],Discount,(Total-Discount)AS Total,Doc.CreatedBy,Doc.DateCreated, Doc.UpdatedBy,Doc.DateUpdated FROM Purchase_Order Doc INNER JOIN Vendor V ON V.VendorID = Doc.VendorID AND ISNULL(IsImport,'False') = 'True'   WHERE 1=1 AND  ISNULL(IsVoid,'False') = 'False' ";
						break;
					case SysDocTypes.PurchaseOrderNI:
						text = "SELECT " + text5 + " , SysDocID,VoucherID,Doc.VendorID [Vendor Code],VendorName [Vendor Name],TransactionDate [Date],Discount,(Total-Discount)AS Total,Doc.CreatedBy,Doc.DateCreated, Doc.UpdatedBy,Doc.DateUpdated FROM Purchase_Order_NonInv Doc INNER JOIN Vendor V ON V.VendorID = Doc.VendorID AND ISNULL(IsImport,'False') = 'False'  WHERE 1=1 AND  ISNULL(IsVoid,'False') = 'False' ";
						break;
					case SysDocTypes.PurchaseInvoiceNI:
						text = "SELECT " + text5 + " , SysDocID,VoucherID,Doc.VendorID [Vendor Code],VendorName [Vendor Name],TransactionDate [Date],Discount,(Total-Discount)AS Total,Doc.CreatedBy,Doc.DateCreated, Doc.UpdatedBy,Doc.DateUpdated FROM Purchase_Invoice_NonInv Doc INNER JOIN Vendor V ON V.VendorID = Doc.VendorID AND ISNULL(IsImport,'False') = 'False'  WHERE 1=1 AND  ISNULL(IsVoid,'False') = 'False' ";
						break;
					case SysDocTypes.GoodsReceivedNote:
						text = "SELECT " + text5 + " , SysDocID,VoucherID,Doc.VendorID [Vendor Code],VendorName [Vendor Name],TransactionDate [Date],Discount,(Total-Discount)AS Total,Doc.CreatedBy,Doc.DateCreated, Doc.UpdatedBy,Doc.DateUpdated FROM Purchase_Receipt Doc INNER JOIN Vendor V ON V.VendorID = Doc.VendorID AND ISNULL(IsImport,'False') = 'False'  WHERE 1=1 AND  ISNULL(IsVoid,'False') = 'False'";
						break;
					case SysDocTypes.ImportGoodsReceivedNote:
						text = "SELECT " + text5 + " , SysDocID,VoucherID,Doc.VendorID [Vendor Code],VendorName [Vendor Name],TransactionDate [Date],Discount,(Total-Discount)AS Total,Doc.CreatedBy,Doc.DateCreated, Doc.UpdatedBy,Doc.DateUpdated FROM Purchase_Receipt Doc INNER JOIN Vendor V ON V.VendorID = Doc.VendorID AND ISNULL(IsImport,'False') = 'True'   WHERE 1=1 AND  ISNULL(IsVoid,'False') = 'False'";
						break;
					case SysDocTypes.CashPurchase:
						text = "SELECT " + text5 + " , SysDocID,VoucherID,Doc.VendorID [Vendor Code],VendorName [Vendor Name],TransactionDate [Date],Discount,(Total-Discount)AS Total,Doc.CreatedBy,Doc.DateCreated, Doc.UpdatedBy,Doc.DateUpdated FROM Purchase_Invoice Doc INNER JOIN Vendor V ON V.VendorID = Doc.VendorID AND ISNULL(IsCash,'True') = 'True'  WHERE 1=1 AND  ISNULL(IsVoid,'False') = 'False'";
						break;
					case SysDocTypes.PurchaseQuote:
						text = "SELECT " + text5 + " , SysDocID,VoucherID,Doc.VendorID [Vendor Code],VendorName [Vendor Name],TransactionDate [Date],Discount,(Total-Discount)AS Total,Doc.CreatedBy,Doc.DateCreated, Doc.UpdatedBy,Doc.DateUpdated FROM Purchase_Quote Doc INNER JOIN Vendor V ON V.VendorID = Doc.VendorID AND ISNULL(IsImport,'False') = 'False'   WHERE 1=1 AND  ISNULL(IsVoid,'False') = 'False' ";
						break;
					case SysDocTypes.PurchaseInvoice:
						text = "SELECT " + text5 + " , SysDocID,VoucherID,Doc.VendorID [Vendor Code],VendorName [Vendor Name],TransactionDate [Date],Discount,(Total-Discount)AS Total,Doc.CreatedBy,Doc.DateCreated, Doc.UpdatedBy,Doc.DateUpdated FROM Purchase_Invoice Doc INNER JOIN Vendor V ON V.VendorID = Doc.VendorID AND ISNULL(IsImport,'False') = 'False'   WHERE 1=1 AND  ISNULL(IsVoid,'False') = 'False' ";
						break;
					case SysDocTypes.ImportPurchaseInvoice:
						text = "SELECT " + text5 + " , SysDocID,VoucherID,Doc.VendorID [Vendor Code],VendorName [Vendor Name],TransactionDate [Date],Discount,(Total-Discount)AS Total,Doc.CreatedBy,Doc.DateCreated, Doc.UpdatedBy,Doc.DateUpdated FROM Purchase_Invoice Doc INNER JOIN Vendor V ON V.VendorID = Doc.VendorID AND ISNULL(IsImport,'False') = 'True'  WHERE 1=1 AND  ISNULL(IsVoid,'False') = 'False'";
						break;
					case SysDocTypes.PackingList:
						text = "SELECT " + text5 + " , SysDocID,VoucherID,Doc.VendorID [Vendor Code],VendorName [Vendor Name],TransactionDate [Date],Doc.CreatedBy,Doc.DateCreated, Doc.UpdatedBy,Doc.DateUpdated FROM PO_Shipment Doc INNER JOIN Vendor V ON V.VendorID = Doc.VendorID  WHERE 1=1 ";
						break;
					case SysDocTypes.ProformaInvoice:
						text = "SELECT " + text5 + " , SysDocID,VoucherID,Doc.VendorID [Vendor Code],VendorName [Vendor Name],TransactionDate [Date],Discount,(Total-Discount)AS Total,Doc.CreatedBy,Doc.DateCreated, Doc.UpdatedBy,Doc.DateUpdated FROM Purchase_Quote Doc INNER JOIN Vendor V ON V.VendorID = Doc.VendorID AND ISNULL(IsImport,'False') = 'True' AND  ISNULL(IsVoid,'False') = 'False'  WHERE 1=1 ";
						break;
					case SysDocTypes.SalesEnquiry:
						text = "SELECT " + text5 + " , SysDocID,VoucherID,Doc.CustomerID [Customer Code],CustomerName [Customer Name],TransactionDate [Date],Discount,(Total-Discount)AS Total,Doc.CreatedBy,Doc.DateCreated, Doc.UpdatedBy,Doc.DateUpdated FROM Sales_Enquiry Doc INNER JOIN Customer C ON C.CustomerID = Doc.CustomerID AND ISNULL(IsExport,'False') = 'False'   WHERE 1=1 AND  ISNULL(IsVoid,'False') = 'False' ";
						break;
					case SysDocTypes.SalesOrder:
						text = "SELECT " + text5 + " , SysDocID,VoucherID,Doc.CustomerID [Customer Code],CustomerName [Customer Name],TransactionDate [Date],Discount,(Total-Discount)AS Total,Doc.CreatedBy,Doc.DateCreated, Doc.UpdatedBy,Doc.DateUpdated FROM Sales_Order Doc INNER JOIN Customer C ON C.CustomerID = Doc.CustomerID AND ISNULL(IsExport,'False') = 'False'   WHERE 1=1 AND  ISNULL(IsVoid,'False') = 'False' ";
						break;
					case SysDocTypes.ExportSalesOrder:
						text = "SELECT " + text5 + " , SysDocID,VoucherID,Doc.CustomerID [Customer Code],CustomerName [Customer Name],TransactionDate [Date],Discount,(Total-Discount)AS Total,Doc.CreatedBy,Doc.DateCreated, Doc.UpdatedBy,Doc.DateUpdated FROM Sales_Order Doc INNER JOIN Customer C ON C.CustomerID = Doc.CustomerID AND ISNULL(IsExport,'False') = 'True'  WHERE 1=1 AND  ISNULL(IsVoid,'False') = 'False'";
						break;
					case SysDocTypes.SalesQuote:
						text = "SELECT " + text5 + " , SysDocID,VoucherID,Doc.CustomerID [Customer Code],CustomerName [Customer Name],TransactionDate [Date],Discount,(Total-Discount)AS Total,Doc.CreatedBy,Doc.DateCreated, Doc.UpdatedBy,Doc.DateUpdated FROM Sales_Quote Doc INNER JOIN Customer C ON C.CustomerID = Doc.CustomerID  AND  ISNULL(IsVoid,'False') = 'False'  WHERE 1=1  AND  ISNULL(IsVoid,'False') = 'False'";
						break;
					case SysDocTypes.DeliveryNote:
						text = "SELECT " + text5 + " , SysDocID,VoucherID,Doc.CustomerID [Customer Code],CustomerName [Customer Name],TransactionDate [Date],Discount,(Total-Discount)AS Total,Doc.CreatedBy,Doc.DateCreated, Doc.UpdatedBy,Doc.DateUpdated FROM Delivery_Note Doc INNER JOIN Customer C ON C.CustomerID = Doc.CustomerID  AND  ISNULL(IsVoid,'False') = 'False'  WHERE 1=1  AND  ISNULL(IsVoid,'False') = 'False'";
						break;
					case SysDocTypes.InventoryNoneSale:
						text = "SELECT " + text5 + " ,SysDocID,VoucherID,LocationID,AdjustmentTypeID,TransactionDate [Date],Doc.CreatedBy,Doc.DateCreated, Doc.UpdatedBy,Doc.DateUpdated FROM Inventory_Damage Doc  WHERE 1=1 AND  ISNULL(IsVoid,'False') = 'False' ";
						break;
					case SysDocTypes.InventoryAdjustment:
						text = "SELECT " + text5 + " ,SysDocID,VoucherID,LocationID,AdjustmentTypeID,TransactionDate [Date],Doc.CreatedBy,Doc.DateCreated, Doc.UpdatedBy,Doc.DateUpdated FROM Inventory_Adjustment Doc AND  ISNULL(IsVoid,'False') = 'False' WHERE 1=1 AND  ISNULL(IsVoid,'False') = 'False' ";
						break;
					case SysDocTypes.JobMaterialRequisition:
						text = "SELECT " + text5 + " ,SysDocID,VoucherID,LocationID,TransactionDate [Date],Doc.CreatedBy,Doc.DateCreated, Doc.UpdatedBy,Doc.DateUpdated FROM Job_Material_Requisition Doc  WHERE 1=1 ";
						break;
					case SysDocTypes.InventoryRepacking:
						text = "SELECT " + text5 + " ,SysDocID,VoucherID,LocationID,TransactionDate [Date],Doc.CreatedBy,Doc.DateCreated, Doc.UpdatedBy,Doc.DateUpdated FROM Inventory_Repacking Doc    WHERE 1=1  AND  ISNULL(IsVoid,'False') = 'False'";
						break;
					case SysDocTypes.CLVoucher:
						text = "SELECT " + text5 + " ,SysDocID,VoucherID,Doc.CustomerID,Doc.Amount,Doc.CreatedBy,Doc.DateCreated, Doc.UpdatedBy,Doc.DateUpdated FROM CL_Voucher Doc  WHERE 1=1 AND  ISNULL(IsVoid,'False') = 'False'";
						break;
					case SysDocTypes.CreditLimitReview:
						text = "SELECT " + text5 + " ,SysDocID,VoucherID,Doc.CustomerID,Doc.CreditAmount,Doc.CreatedBy,Doc.DateCreated, Doc.UpdatedBy,Doc.DateUpdated FROM Credit_Limit_Review Doc  WHERE 1=1  ";
						break;
					case SysDocTypes.PurchasePrepaymentInvoice:
						text = "SELECT " + text5 + " ,SysDocID,VoucherID,Doc.VendorID,Doc.TransactionDate,Doc.CreatedBy,Doc.DateCreated, Doc.UpdatedBy,Doc.DateUpdated FROM Purchase_Prepayment_Invoice Doc  WHERE 1=1  AND  ISNULL(IsVoid,'False') = 'False'";
						break;
					case SysDocTypes.EmployeeGeneralActivity:
						text = "SELECT " + text5 + " ,SysDocID,VoucherID,TransactionDate [Date],Doc.CreatedBy,Doc.DateCreated, Doc.UpdatedBy,Doc.DateUpdated FROM Employee_GeneralActivity Doc  WHERE 1=1 AND  ISNULL(IsVoid,'False') = 'False'";
						break;
					case SysDocTypes.EmployeeAppraisal:
						text = "SELECT " + text5 + " ,SysDocID,VoucherID,TransactionDate [Date],Doc.CreatedBy,Doc.DateCreated, Doc.UpdatedBy,Doc.DateUpdated FROM Employee_Appraisal Doc    WHERE 1=1 ";
						break;
					case SysDocTypes.CashPayment:
						text = "SELECT " + text5 + " , ISNULL(IsVoid,'False') AS V,Doc.SysDocID ,VoucherID ,TransactionDate [Date],\r\n\t\t\t\t\t\t\t                                CASE SysDocType WHEN 4 THEN 'Cash' WHEN 5 THEN 'Cheque' ELSE 'TT' END AS [Type],CostCenterID [C.C.],PayeeID [Payee Code],\r\n\t\t\t\t\t\t\t                                (CASE PayeeType\r\n\t\t\t\t\t\t\t\t\t\t\t\t\t                                WHEN 'C' THEN Customer.CustomerName\r\n\t\t\t\t\t\t\t\t\t\t\t\t\t                                WHEN 'V' THEN Vendor.VendorName\r\n\t\t\t\t\t\t\t\t\t\t\t\t\t                                WHEN 'E' THEN Employee.FirstName\r\n\t\t\t\t\t\t\t\t\t\t\t\t\t                                ELSE Account.AccountName END) AS [Payee Name],Reference [Ref],\r\n\t\t\t\t\t\t\t                                RegisterID [Reg],Doc.Description [Desc],\r\n\t\t\t\t\t\t\t                                ISNULL(Doc.CurrencyID,(SELECT CurrencyID FROM Currency WHERE IsBase=1)) AS [Cur] ,ISNULL(AmountFC,Amount) AS [Amount],Doc.CreatedBy,Doc.DateCreated, Doc.UpdatedBy,Doc.DateUpdated\r\n\t\t\t\t\t\t\t                                FROM GL_Transaction Doc\r\n\t\t\t\t\t\t\t                                LEFt OUTER JOIN Account ON Doc.PayeeID=Account.AccountID LEFt OUTER JOIN\r\n\t\t\t\t\t\t\t\t\t\t\t\t\t                                Customer ON Doc.PayeeID=Customer.CustomerID LEFt OUTER JOIN\r\n\t\t\t\t\t\t\t\t\t\t\t\t\t                                Vendor ON Doc.PayeeID=Vendor.VendorID LEFt OUTER JOIN\r\n\t\t\t\t\t\t\t\t\t\t\t\t\t                                Employee ON Doc.PayeeID=Employee.EmployeeID\r\n\t\t\t\t\t\t\t                                INNER JOIN System_Document SD ON SD.SysDocID=Doc.SysDocID\r\n                                                            WHERE SysDocType IN (4) ";
						break;
					case SysDocTypes.CashExpense:
						text = "SELECT " + text5 + " , ISNULL(IsVoid,'False') AS V,Doc.SysDocID ,VoucherID ,TransactionDate [Date],\r\n\t\t\t\t\t\t\t                                CASE SysDocType WHEN 4 THEN 'Cash' WHEN 5 THEN 'Cheque' ELSE 'TT' END AS [Type],CostCenterID [C.C.],PayeeID [Payee Code],\r\n\t\t\t\t\t\t\t                                (CASE PayeeType\r\n\t\t\t\t\t\t\t\t\t\t\t\t\t                                WHEN 'C' THEN Customer.CustomerName\r\n\t\t\t\t\t\t\t\t\t\t\t\t\t                                WHEN 'V' THEN Vendor.VendorName\r\n\t\t\t\t\t\t\t\t\t\t\t\t\t                                WHEN 'E' THEN Employee.FirstName\r\n\t\t\t\t\t\t\t\t\t\t\t\t\t                                ELSE Account.AccountName END) AS [Payee Name],Reference [Ref],\r\n\t\t\t\t\t\t\t                                RegisterID [Reg],Doc.Description [Desc],\r\n\t\t\t\t\t\t\t                                ISNULL(Doc.CurrencyID,(SELECT CurrencyID FROM Currency WHERE IsBase=1)) AS [Cur] ,ISNULL(AmountFC,Amount) AS [Amount],Doc.CreatedBy,Doc.DateCreated, Doc.UpdatedBy,Doc.DateUpdated\r\n\t\t\t\t\t\t\t                                FROM GL_Transaction Doc\r\n\t\t\t\t\t\t\t                                LEFt OUTER JOIN Account ON Doc.PayeeID=Account.AccountID LEFt OUTER JOIN\r\n\t\t\t\t\t\t\t\t\t\t\t\t\t                                Customer ON Doc.PayeeID=Customer.CustomerID LEFt OUTER JOIN\r\n\t\t\t\t\t\t\t\t\t\t\t\t\t                                Vendor ON Doc.PayeeID=Vendor.VendorID LEFt OUTER JOIN\r\n\t\t\t\t\t\t\t\t\t\t\t\t\t                                Employee ON Doc.PayeeID=Employee.EmployeeID\r\n\t\t\t\t\t\t\t                                INNER JOIN System_Document SD ON SD.SysDocID=Doc.SysDocID\r\n                                                            WHERE SysDocType IN (8) ";
						break;
					case SysDocTypes.ChequePayment:
						text = "SELECT " + text5 + " , ISNULL(IsVoid,'False') AS V,Doc.SysDocID ,VoucherID,TransactionDate [Date],\r\n\t\t\t\t\t\t\t                                CASE SysDocType WHEN 4 THEN 'Cash' WHEN 5 THEN 'Cheque' ELSE 'TT' END AS [Type],CostCenterID [C.C.],PayeeID [Payee Code],\r\n\t\t\t\t\t\t\t                                (CASE PayeeType\r\n\t\t\t\t\t\t\t\t\t\t\t\t\t                                WHEN 'C' THEN Customer.CustomerName\r\n\t\t\t\t\t\t\t\t\t\t\t\t\t                                WHEN 'V' THEN Vendor.VendorName\r\n\t\t\t\t\t\t\t\t\t\t\t\t\t                                WHEN 'E' THEN Employee.FirstName\r\n\t\t\t\t\t\t\t\t\t\t\t\t\t                                ELSE Account.AccountName END) AS [Payee Name],Reference [Ref],\r\n\t\t\t\t\t\t\t                                RegisterID [Reg],Doc.Description [Desc],\r\n\t\t\t\t\t\t\t                                ISNULL(Doc.CurrencyID,(SELECT CurrencyID FROM Currency WHERE IsBase=1)) AS [Cur] ,ISNULL(AmountFC,Amount) AS [Amount],Doc.CreatedBy,Doc.DateCreated, Doc.UpdatedBy,Doc.DateUpdated\r\n\t\t\t\t\t\t\t                                FROM GL_Transaction Doc\r\n\t\t\t\t\t\t\t                                LEFt OUTER JOIN Account ON Doc.PayeeID=Account.AccountID LEFt OUTER JOIN\r\n\t\t\t\t\t\t\t\t\t\t\t\t\t                                Customer ON Doc.PayeeID=Customer.CustomerID LEFt OUTER JOIN\r\n\t\t\t\t\t\t\t\t\t\t\t\t\t                                Vendor ON Doc.PayeeID=Vendor.VendorID LEFt OUTER JOIN\r\n\t\t\t\t\t\t\t\t\t\t\t\t\t                                Employee ON Doc.PayeeID=Employee.EmployeeID\r\n\t\t\t\t\t\t\t                                INNER JOIN System_Document SD ON SD.SysDocID=Doc.SysDocID\r\n                                                            WHERE SysDocType IN (5) ";
						break;
					case SysDocTypes.CashReceipt:
						text = "SELECT " + text5 + " , ISNULL(IsVoid,'False') AS V,Doc.SysDocID ,VoucherID,TransactionDate [Date],\r\n\t\t\t\t\t\t\t                                CASE SysDocType WHEN 4 THEN 'Cash' WHEN 5 THEN 'Cheque' ELSE 'TT' END AS [Type],CostCenterID [C.C.],PayeeID [Payee Code],\r\n\t\t\t\t\t\t\t                                (CASE PayeeType\r\n\t\t\t\t\t\t\t\t\t\t\t\t\t                                WHEN 'C' THEN Customer.CustomerName\r\n\t\t\t\t\t\t\t\t\t\t\t\t\t                                WHEN 'V' THEN Vendor.VendorName\r\n\t\t\t\t\t\t\t\t\t\t\t\t\t                                WHEN 'E' THEN Employee.FirstName\r\n\t\t\t\t\t\t\t\t\t\t\t\t\t                                ELSE Account.AccountName END) AS [Payee Name],Reference [Ref],\r\n\t\t\t\t\t\t\t                                RegisterID [Reg],Doc.Description [Desc],\r\n\t\t\t\t\t\t\t                                ISNULL(Doc.CurrencyID,(SELECT CurrencyID FROM Currency WHERE IsBase=1)) AS [Cur] ,ISNULL(AmountFC,Amount) AS [Amount],Doc.CreatedBy,Doc.DateCreated, Doc.UpdatedBy,Doc.DateUpdated\r\n\t\t\t\t\t\t\t                                FROM GL_Transaction Doc\r\n\t\t\t\t\t\t\t                                LEFt OUTER JOIN Account ON Doc.PayeeID=Account.AccountID LEFt OUTER JOIN\r\n\t\t\t\t\t\t\t\t\t\t\t\t\t                                Customer ON Doc.PayeeID=Customer.CustomerID LEFt OUTER JOIN\r\n\t\t\t\t\t\t\t\t\t\t\t\t\t                                Vendor ON Doc.PayeeID=Vendor.VendorID LEFt OUTER JOIN\r\n\t\t\t\t\t\t\t\t\t\t\t\t\t                                Employee ON Doc.PayeeID=Employee.EmployeeID\r\n\t\t\t\t\t\t\t                                INNER JOIN System_Document SD ON SD.SysDocID=Doc.SysDocID\r\n                                                            WHERE SysDocType IN (3) ";
						break;
					case SysDocTypes.TTPayment:
						text = "SELECT " + text5 + " , ISNULL(IsVoid,'False') AS V,Doc.SysDocID ,VoucherID ,TransactionDate [Date],\r\n\t\t\t\t\t\t\t                                CASE SysDocType WHEN 4 THEN 'Cash' WHEN 5 THEN 'Cheque' ELSE 'TT' END AS [Type],CostCenterID [C.C.],PayeeID [Payee Code],\r\n\t\t\t\t\t\t\t                                (CASE PayeeType\r\n\t\t\t\t\t\t\t\t\t\t\t\t\t                                WHEN 'C' THEN Customer.CustomerName\r\n\t\t\t\t\t\t\t\t\t\t\t\t\t                                WHEN 'V' THEN Vendor.VendorName\r\n\t\t\t\t\t\t\t\t\t\t\t\t\t                                WHEN 'E' THEN Employee.FirstName\r\n\t\t\t\t\t\t\t\t\t\t\t\t\t                                ELSE Account.AccountName END) AS [Payee Name],Reference [Ref],\r\n\t\t\t\t\t\t\t                                RegisterID [Reg],Doc.Description [Desc],\r\n\t\t\t\t\t\t\t                                ISNULL(Doc.CurrencyID,(SELECT CurrencyID FROM Currency WHERE IsBase=1)) AS [Cur] ,ISNULL(AmountFC,Amount) AS [Amount],Doc.CreatedBy,Doc.DateCreated, Doc.UpdatedBy,Doc.DateUpdated\r\n\t\t\t\t\t\t\t                                FROM GL_Transaction Doc\r\n\t\t\t\t\t\t\t                                LEFt OUTER JOIN Account ON Doc.PayeeID=Account.AccountID LEFt OUTER JOIN\r\n\t\t\t\t\t\t\t\t\t\t\t\t\t                                Customer ON Doc.PayeeID=Customer.CustomerID LEFt OUTER JOIN\r\n\t\t\t\t\t\t\t\t\t\t\t\t\t                                Vendor ON Doc.PayeeID=Vendor.VendorID LEFt OUTER JOIN\r\n\t\t\t\t\t\t\t\t\t\t\t\t\t                                Employee ON Doc.PayeeID=Employee.EmployeeID\r\n\t\t\t\t\t\t\t                                INNER JOIN System_Document SD ON SD.SysDocID=Doc.SysDocID\r\n                                                            WHERE SysDocType IN (64) ";
						break;
					case SysDocTypes.CashReceiptMultiple:
						text = "SELECT " + text5 + " , ISNULL(IsVoid,'False') AS V,Doc.SysDocID ,VoucherID,TransactionDate [Date],\r\n\t\t\t\t\t\t\t                                CASE SysDocType WHEN 4 THEN 'Cash' WHEN 5 THEN 'Cheque' ELSE 'TT' END AS [Type],CostCenterID [C.C.],PayeeID [Payee Code],\r\n\t\t\t\t\t\t\t                                (CASE PayeeType\r\n\t\t\t\t\t\t\t\t\t\t\t\t\t                                WHEN 'C' THEN Customer.CustomerName\r\n\t\t\t\t\t\t\t\t\t\t\t\t\t                                WHEN 'V' THEN Vendor.VendorName\r\n\t\t\t\t\t\t\t\t\t\t\t\t\t                                WHEN 'E' THEN Employee.FirstName\r\n\t\t\t\t\t\t\t\t\t\t\t\t\t                                ELSE Account.AccountName END) AS [Payee Name],Reference [Ref],\r\n\t\t\t\t\t\t\t                                RegisterID [Reg],Doc.Description [Desc],\r\n\t\t\t\t\t\t\t                                ISNULL(Doc.CurrencyID,(SELECT CurrencyID FROM Currency WHERE IsBase=1)) AS [Cur] ,ISNULL(AmountFC,Amount) AS [Amount],Doc.CreatedBy,Doc.DateCreated, Doc.UpdatedBy,Doc.DateUpdated\r\n\t\t\t\t\t\t\t                                FROM GL_Transaction Doc\r\n\t\t\t\t\t\t\t                                LEFt OUTER JOIN Account ON Doc.PayeeID=Account.AccountID LEFt OUTER JOIN\r\n\t\t\t\t\t\t\t\t\t\t\t\t\t                                Customer ON Doc.PayeeID=Customer.CustomerID LEFt OUTER JOIN\r\n\t\t\t\t\t\t\t\t\t\t\t\t\t                                Vendor ON Doc.PayeeID=Vendor.VendorID LEFt OUTER JOIN\r\n\t\t\t\t\t\t\t\t\t\t\t\t\t                                Employee ON Doc.PayeeID=Employee.EmployeeID\r\n\t\t\t\t\t\t\t                                INNER JOIN System_Document SD ON SD.SysDocID=Doc.SysDocID\r\n                                                            WHERE SysDocType IN (66) ";
						break;
					case SysDocTypes.ChequeReceipt:
						text = "SELECT " + text5 + " , ISNULL(IsVoid,'False') AS V,Doc.SysDocID ,VoucherID ,TransactionDate [Date],\r\n\t\t\t\t\t\t\t                                CASE SysDocType WHEN 4 THEN 'Cash' WHEN 5 THEN 'Cheque' ELSE 'TT' END AS [Type],CostCenterID [C.C.],PayeeID [Payee Code],\r\n\t\t\t\t\t\t\t                                (CASE PayeeType\r\n\t\t\t\t\t\t\t\t\t\t\t\t\t                                WHEN 'C' THEN Customer.CustomerName\r\n\t\t\t\t\t\t\t\t\t\t\t\t\t                                WHEN 'V' THEN Vendor.VendorName\r\n\t\t\t\t\t\t\t\t\t\t\t\t\t                                WHEN 'E' THEN Employee.FirstName\r\n\t\t\t\t\t\t\t\t\t\t\t\t\t                                ELSE Account.AccountName END) AS [Payee Name],Reference [Ref],\r\n\t\t\t\t\t\t\t                                RegisterID [Reg],Doc.Description [Desc],\r\n\t\t\t\t\t\t\t                                ISNULL(Doc.CurrencyID,(SELECT CurrencyID FROM Currency WHERE IsBase=1)) AS [Cur] ,ISNULL(AmountFC,Amount) AS [Amount],Doc.CreatedBy,Doc.DateCreated, Doc.UpdatedBy,Doc.DateUpdated\r\n\t\t\t\t\t\t\t                                FROM GL_Transaction Doc\r\n\t\t\t\t\t\t\t                                LEFt OUTER JOIN Account ON Doc.PayeeID=Account.AccountID LEFt OUTER JOIN\r\n\t\t\t\t\t\t\t\t\t\t\t\t\t                                Customer ON Doc.PayeeID=Customer.CustomerID LEFt OUTER JOIN\r\n\t\t\t\t\t\t\t\t\t\t\t\t\t                                Vendor ON Doc.PayeeID=Vendor.VendorID LEFt OUTER JOIN\r\n\t\t\t\t\t\t\t\t\t\t\t\t\t                                Employee ON Doc.PayeeID=Employee.EmployeeID\r\n\t\t\t\t\t\t\t                                INNER JOIN System_Document SD ON SD.SysDocID=Doc.SysDocID\r\n                                                            WHERE SysDocType IN (2) ";
						break;
					case SysDocTypes.FundTransfer:
						text = "SELECT " + text5 + " , ISNULL(IsVoid,'False') AS V,Doc.SysDocID ,VoucherID ,TransactionDate [Date],\r\n\t\t\t\t\t\t\t                                CASE SysDocType WHEN 4 THEN 'Cash' WHEN 5 THEN 'Cheque' ELSE 'TT' END AS [Type],CostCenterID [C.C.],PayeeID [Payee Code],\r\n\t\t\t\t\t\t\t                                (CASE PayeeType\r\n\t\t\t\t\t\t\t\t\t\t\t\t\t                                WHEN 'C' THEN Customer.CustomerName\r\n\t\t\t\t\t\t\t\t\t\t\t\t\t                                WHEN 'V' THEN Vendor.VendorName\r\n\t\t\t\t\t\t\t\t\t\t\t\t\t                                WHEN 'E' THEN Employee.FirstName\r\n\t\t\t\t\t\t\t\t\t\t\t\t\t                                ELSE Account.AccountName END) AS [Payee Name],Reference [Ref],\r\n\t\t\t\t\t\t\t                                RegisterID [Reg],Doc.Description [Desc],\r\n\t\t\t\t\t\t\t                                ISNULL(Doc.CurrencyID,(SELECT CurrencyID FROM Currency WHERE IsBase=1)) AS [Cur] ,ISNULL(AmountFC,Amount) AS [Amount],Doc.CreatedBy,Doc.DateCreated, Doc.UpdatedBy,Doc.DateUpdated\r\n\t\t\t\t\t\t\t                                FROM GL_Transaction Doc\r\n\t\t\t\t\t\t\t                                LEFt OUTER JOIN Account ON Doc.PayeeID=Account.AccountID LEFt OUTER JOIN\r\n\t\t\t\t\t\t\t\t\t\t\t\t\t                                Customer ON Doc.PayeeID=Customer.CustomerID LEFt OUTER JOIN\r\n\t\t\t\t\t\t\t\t\t\t\t\t\t                                Vendor ON Doc.PayeeID=Vendor.VendorID LEFt OUTER JOIN\r\n\t\t\t\t\t\t\t\t\t\t\t\t\t                                Employee ON Doc.PayeeID=Employee.EmployeeID\r\n\t\t\t\t\t\t\t                                INNER JOIN System_Document SD ON SD.SysDocID=Doc.SysDocID\r\n                                                            WHERE SysDocType IN (6) ";
						break;
					case SysDocTypes.TTReceipt:
						text = "SELECT " + text5 + " , ISNULL(IsVoid,'False') AS V,Doc.SysDocID ,VoucherID ,TransactionDate [Date],\r\n\t\t\t\t\t\t\t                                CASE SysDocType WHEN 4 THEN 'Cash' WHEN 5 THEN 'Cheque' ELSE 'TT' END AS [Type],CostCenterID [C.C.],PayeeID [Payee Code],\r\n\t\t\t\t\t\t\t                                (CASE PayeeType\r\n\t\t\t\t\t\t\t\t\t\t\t\t\t                                WHEN 'C' THEN Customer.CustomerName\r\n\t\t\t\t\t\t\t\t\t\t\t\t\t                                WHEN 'V' THEN Vendor.VendorName\r\n\t\t\t\t\t\t\t\t\t\t\t\t\t                                WHEN 'E' THEN Employee.FirstName\r\n\t\t\t\t\t\t\t\t\t\t\t\t\t                                ELSE Account.AccountName END) AS [Payee Name],Reference [Ref],\r\n\t\t\t\t\t\t\t                                RegisterID [Reg],Doc.Description [Desc],\r\n\t\t\t\t\t\t\t                                ISNULL(Doc.CurrencyID,(SELECT CurrencyID FROM Currency WHERE IsBase=1)) AS [Cur] ,ISNULL(AmountFC,Amount) AS [Amount],Doc.CreatedBy,Doc.DateCreated, Doc.UpdatedBy,Doc.DateUpdated\r\n\t\t\t\t\t\t\t                                FROM GL_Transaction Doc\r\n\t\t\t\t\t\t\t                                LEFt OUTER JOIN Account ON Doc.PayeeID=Account.AccountID LEFt OUTER JOIN\r\n\t\t\t\t\t\t\t\t\t\t\t\t\t                                Customer ON Doc.PayeeID=Customer.CustomerID LEFt OUTER JOIN\r\n\t\t\t\t\t\t\t\t\t\t\t\t\t                                Vendor ON Doc.PayeeID=Vendor.VendorID LEFt OUTER JOIN\r\n\t\t\t\t\t\t\t\t\t\t\t\t\t                                Employee ON Doc.PayeeID=Employee.EmployeeID\r\n\t\t\t\t\t\t\t                                INNER JOIN System_Document SD ON SD.SysDocID=Doc.SysDocID\r\n                                                            WHERE SysDocType IN (65) ";
						break;
					case SysDocTypes.ChequeDeposit:
						text = "SELECT " + text5 + " , ISNULL(IsVoid,'False') AS V,Doc.SysDocID ,VoucherID ,TransactionDate [Date],\r\n\t\t\t\t\t\t\t                                CASE SysDocType WHEN 4 THEN 'Cash' WHEN 5 THEN 'Cheque' ELSE 'TT' END AS [Type],CostCenterID [C.C.],PayeeID [Payee Code],\r\n\t\t\t\t\t\t\t                                (CASE PayeeType\r\n\t\t\t\t\t\t\t\t\t\t\t\t\t                                WHEN 'C' THEN Customer.CustomerName\r\n\t\t\t\t\t\t\t\t\t\t\t\t\t                                WHEN 'V' THEN Vendor.VendorName\r\n\t\t\t\t\t\t\t\t\t\t\t\t\t                                WHEN 'E' THEN Employee.FirstName\r\n\t\t\t\t\t\t\t\t\t\t\t\t\t                                ELSE Account.AccountName END) AS [Payee Name],Reference [Ref],\r\n\t\t\t\t\t\t\t                                RegisterID [Reg],Doc.Description [Desc],\r\n\t\t\t\t\t\t\t                                ISNULL(Doc.CurrencyID,(SELECT CurrencyID FROM Currency WHERE IsBase=1)) AS [Cur] ,ISNULL(AmountFC,Amount) AS [Amount],Doc.CreatedBy,Doc.DateCreated, Doc.UpdatedBy,Doc.DateUpdated\r\n\t\t\t\t\t\t\t                                FROM GL_Transaction Doc\r\n\t\t\t\t\t\t\t                                LEFt OUTER JOIN Account ON Doc.PayeeID=Account.AccountID LEFt OUTER JOIN\r\n\t\t\t\t\t\t\t\t\t\t\t\t\t                                Customer ON Doc.PayeeID=Customer.CustomerID LEFt OUTER JOIN\r\n\t\t\t\t\t\t\t\t\t\t\t\t\t                                Vendor ON Doc.PayeeID=Vendor.VendorID LEFt OUTER JOIN\r\n\t\t\t\t\t\t\t\t\t\t\t\t\t                                Employee ON Doc.PayeeID=Employee.EmployeeID\r\n\t\t\t\t\t\t\t                                INNER JOIN System_Document SD ON SD.SysDocID=Doc.SysDocID\r\n                                                            WHERE SysDocType IN (7) ";
						break;
					case SysDocTypes.ReturnedCheque:
						text = "SELECT " + text5 + " , ISNULL(IsVoid,'False') AS V,Doc.SysDocID ,VoucherID ,TransactionDate [Date],\r\n\t\t\t\t\t\t\t                                CASE SysDocType WHEN 4 THEN 'Cash' WHEN 5 THEN 'Cheque' ELSE 'TT' END AS [Type],CostCenterID [C.C.],PayeeID [Payee Code],\r\n\t\t\t\t\t\t\t                                (CASE PayeeType\r\n\t\t\t\t\t\t\t\t\t\t\t\t\t                                WHEN 'C' THEN Customer.CustomerName\r\n\t\t\t\t\t\t\t\t\t\t\t\t\t                                WHEN 'V' THEN Vendor.VendorName\r\n\t\t\t\t\t\t\t\t\t\t\t\t\t                                WHEN 'E' THEN Employee.FirstName\r\n\t\t\t\t\t\t\t\t\t\t\t\t\t                                ELSE Account.AccountName END) AS [Payee Name],Reference [Ref],\r\n\t\t\t\t\t\t\t                                RegisterID [Reg],Doc.Description [Desc],\r\n\t\t\t\t\t\t\t                                ISNULL(Doc.CurrencyID,(SELECT CurrencyID FROM Currency WHERE IsBase=1)) AS [Cur] ,ISNULL(AmountFC,Amount) AS [Amount],Doc.CreatedBy,Doc.DateCreated, Doc.UpdatedBy,Doc.DateUpdated\r\n\t\t\t\t\t\t\t                                FROM GL_Transaction Doc\r\n\t\t\t\t\t\t\t                                LEFt OUTER JOIN Account ON Doc.PayeeID=Account.AccountID LEFt OUTER JOIN\r\n\t\t\t\t\t\t\t\t\t\t\t\t\t                                Customer ON Doc.PayeeID=Customer.CustomerID LEFt OUTER JOIN\r\n\t\t\t\t\t\t\t\t\t\t\t\t\t                                Vendor ON Doc.PayeeID=Vendor.VendorID LEFt OUTER JOIN\r\n\t\t\t\t\t\t\t\t\t\t\t\t\t                                Employee ON Doc.PayeeID=Employee.EmployeeID\r\n\t\t\t\t\t\t\t                                INNER JOIN System_Document SD ON SD.SysDocID=Doc.SysDocID\r\n                                                            WHERE SysDocType IN (12) ";
						break;
					case SysDocTypes.ReceivedChequeCancellation:
						text = "SELECT " + text5 + " , ISNULL(IsVoid,'False') AS V,Doc.SysDocID ,VoucherID ,TransactionDate [Date],\r\n\t\t\t\t\t\t\t                                CASE SysDocType WHEN 4 THEN 'Cash' WHEN 5 THEN 'Cheque' ELSE 'TT' END AS [Type],CostCenterID [C.C.],PayeeID [Payee Code],\r\n\t\t\t\t\t\t\t                                (CASE PayeeType\r\n\t\t\t\t\t\t\t\t\t\t\t\t\t                                WHEN 'C' THEN Customer.CustomerName\r\n\t\t\t\t\t\t\t\t\t\t\t\t\t                                WHEN 'V' THEN Vendor.VendorName\r\n\t\t\t\t\t\t\t\t\t\t\t\t\t                                WHEN 'E' THEN Employee.FirstName\r\n\t\t\t\t\t\t\t\t\t\t\t\t\t                                ELSE Account.AccountName END) AS [Payee Name],Reference [Ref],\r\n\t\t\t\t\t\t\t                                RegisterID [Reg],Doc.Description [Desc],\r\n\t\t\t\t\t\t\t                                ISNULL(Doc.CurrencyID,(SELECT CurrencyID FROM Currency WHERE IsBase=1)) AS [Cur] ,ISNULL(AmountFC,Amount) AS [Amount],Doc.CreatedBy,Doc.DateCreated, Doc.UpdatedBy,Doc.DateUpdated\r\n\t\t\t\t\t\t\t                                FROM GL_Transaction Doc\r\n\t\t\t\t\t\t\t                                LEFt OUTER JOIN Account ON Doc.PayeeID=Account.AccountID LEFt OUTER JOIN\r\n\t\t\t\t\t\t\t\t\t\t\t\t\t                                Customer ON Doc.PayeeID=Customer.CustomerID LEFt OUTER JOIN\r\n\t\t\t\t\t\t\t\t\t\t\t\t\t                                Vendor ON Doc.PayeeID=Vendor.VendorID LEFt OUTER JOIN\r\n\t\t\t\t\t\t\t\t\t\t\t\t\t                                Employee ON Doc.PayeeID=Employee.EmployeeID\r\n\t\t\t\t\t\t\t                                INNER JOIN System_Document SD ON SD.SysDocID=Doc.SysDocID\r\n                                                            WHERE SysDocType IN (13) ";
						break;
					case SysDocTypes.IssuedChequeCancellation:
						text = "SELECT " + text5 + " , ISNULL(IsVoid,'False') AS V,Doc.SysDocID ,VoucherID ,TransactionDate [Date],\r\n\t\t\t\t\t\t\t                                CASE SysDocType WHEN 4 THEN 'Cash' WHEN 5 THEN 'Cheque' ELSE 'TT' END AS [Type],CostCenterID [C.C.],PayeeID [Payee Code],\r\n\t\t\t\t\t\t\t                                (CASE PayeeType\r\n\t\t\t\t\t\t\t\t\t\t\t\t\t                                WHEN 'C' THEN Customer.CustomerName\r\n\t\t\t\t\t\t\t\t\t\t\t\t\t                                WHEN 'V' THEN Vendor.VendorName\r\n\t\t\t\t\t\t\t\t\t\t\t\t\t                                WHEN 'E' THEN Employee.FirstName\r\n\t\t\t\t\t\t\t\t\t\t\t\t\t                                ELSE Account.AccountName END) AS [Payee Name],Reference [Ref],\r\n\t\t\t\t\t\t\t                                RegisterID [Reg],Doc.Description [Desc],\r\n\t\t\t\t\t\t\t                                ISNULL(Doc.CurrencyID,(SELECT CurrencyID FROM Currency WHERE IsBase=1)) AS [Cur] ,ISNULL(AmountFC,Amount) AS [Amount],Doc.CreatedBy,Doc.DateCreated, Doc.UpdatedBy,Doc.DateUpdated\r\n\t\t\t\t\t\t\t                                FROM GL_Transaction Doc\r\n\t\t\t\t\t\t\t                                LEFt OUTER JOIN Account ON Doc.PayeeID=Account.AccountID LEFt OUTER JOIN\r\n\t\t\t\t\t\t\t\t\t\t\t\t\t                                Customer ON Doc.PayeeID=Customer.CustomerID LEFt OUTER JOIN\r\n\t\t\t\t\t\t\t\t\t\t\t\t\t                                Vendor ON Doc.PayeeID=Vendor.VendorID LEFt OUTER JOIN\r\n\t\t\t\t\t\t\t\t\t\t\t\t\t                                Employee ON Doc.PayeeID=Employee.EmployeeID\r\n\t\t\t\t\t\t\t                                INNER JOIN System_Document SD ON SD.SysDocID=Doc.SysDocID\r\n                                                            WHERE SysDocType IN (15) ";
						break;
					case SysDocTypes.IssuedChequeClearance:
						text = "SELECT " + text5 + " , ISNULL(IsVoid,'False') AS V,Doc.SysDocID ,VoucherID ,TransactionDate [Date],\r\n\t\t\t\t\t\t\t                                CASE SysDocType WHEN 4 THEN 'Cash' WHEN 5 THEN 'Cheque' ELSE 'TT' END AS [Type],CostCenterID [C.C.],PayeeID [Payee Code],\r\n\t\t\t\t\t\t\t                                (CASE PayeeType\r\n\t\t\t\t\t\t\t\t\t\t\t\t\t                                WHEN 'C' THEN Customer.CustomerName\r\n\t\t\t\t\t\t\t\t\t\t\t\t\t                                WHEN 'V' THEN Vendor.VendorName\r\n\t\t\t\t\t\t\t\t\t\t\t\t\t                                WHEN 'E' THEN Employee.FirstName\r\n\t\t\t\t\t\t\t\t\t\t\t\t\t                                ELSE Account.AccountName END) AS [Payee Name],Reference [Ref],\r\n\t\t\t\t\t\t\t                                RegisterID [Reg],Doc.Description [Desc],\r\n\t\t\t\t\t\t\t                                ISNULL(Doc.CurrencyID,(SELECT CurrencyID FROM Currency WHERE IsBase=1)) AS [Cur] ,ISNULL(AmountFC,Amount) AS [Amount],Doc.CreatedBy,Doc.DateCreated, Doc.UpdatedBy,Doc.DateUpdated\r\n\t\t\t\t\t\t\t                                FROM GL_Transaction Doc\r\n\t\t\t\t\t\t\t                                LEFt OUTER JOIN Account ON Doc.PayeeID=Account.AccountID LEFt OUTER JOIN\r\n\t\t\t\t\t\t\t\t\t\t\t\t\t                                Customer ON Doc.PayeeID=Customer.CustomerID LEFt OUTER JOIN\r\n\t\t\t\t\t\t\t\t\t\t\t\t\t                                Vendor ON Doc.PayeeID=Vendor.VendorID LEFt OUTER JOIN\r\n\t\t\t\t\t\t\t\t\t\t\t\t\t                                Employee ON Doc.PayeeID=Employee.EmployeeID\r\n\t\t\t\t\t\t\t                                INNER JOIN System_Document SD ON SD.SysDocID=Doc.SysDocID\r\n                                                            WHERE SysDocType IN (14) ";
						break;
					case SysDocTypes.IssuedChequeReturn:
						text = "SELECT " + text5 + " , ISNULL(IsVoid,'False') AS V,Doc.SysDocID ,VoucherID ,TransactionDate [Date],\r\n\t\t\t\t\t\t\t                                CASE SysDocType WHEN 4 THEN 'Cash' WHEN 5 THEN 'Cheque' ELSE 'TT' END AS [Type],CostCenterID [C.C.],PayeeID [Payee Code],\r\n\t\t\t\t\t\t\t                                (CASE PayeeType\r\n\t\t\t\t\t\t\t\t\t\t\t\t\t                                WHEN 'C' THEN Customer.CustomerName\r\n\t\t\t\t\t\t\t\t\t\t\t\t\t                                WHEN 'V' THEN Vendor.VendorName\r\n\t\t\t\t\t\t\t\t\t\t\t\t\t                                WHEN 'E' THEN Employee.FirstName\r\n\t\t\t\t\t\t\t\t\t\t\t\t\t                                ELSE Account.AccountName END) AS [Payee Name],Reference [Ref],\r\n\t\t\t\t\t\t\t                                RegisterID [Reg],Doc.Description [Desc],\r\n\t\t\t\t\t\t\t                                ISNULL(Doc.CurrencyID,(SELECT CurrencyID FROM Currency WHERE IsBase=1)) AS [Cur] ,ISNULL(AmountFC,Amount) AS [Amount],Doc.CreatedBy,Doc.DateCreated, Doc.UpdatedBy,Doc.DateUpdated\r\n\t\t\t\t\t\t\t                                FROM GL_Transaction Doc\r\n\t\t\t\t\t\t\t                                LEFt OUTER JOIN Account ON Doc.PayeeID=Account.AccountID LEFt OUTER JOIN\r\n\t\t\t\t\t\t\t\t\t\t\t\t\t                                Customer ON Doc.PayeeID=Customer.CustomerID LEFt OUTER JOIN\r\n\t\t\t\t\t\t\t\t\t\t\t\t\t                                Vendor ON Doc.PayeeID=Vendor.VendorID LEFt OUTER JOIN\r\n\t\t\t\t\t\t\t\t\t\t\t\t\t                                Employee ON Doc.PayeeID=Employee.EmployeeID\r\n\t\t\t\t\t\t\t                                INNER JOIN System_Document SD ON SD.SysDocID=Doc.SysDocID\r\n                                                            WHERE SysDocType IN (16) ";
						break;
					case SysDocTypes.SalarySheet:
						text = "SELECT " + text5 + ", Doc.SysDocID,Doc.VoucherID,SheetName,TransactionDate,Month,Year,StartDate,EndDate,Note FROM SalarySheet Doc                                 \r\n                                    WHERE ISNULL(IsClosed,'False') = 'False' ";
						break;
					case SysDocTypes.OverTimeEntry:
						text = "SELECT DISTINCT " + text5 + ", SysDocID ,Doc.VoucherID, Doc.Month,Doc.Year\r\n                            FROM OverTimeEntry Doc    WHERE 1 = 1 ";
						break;
					case SysDocTypes.SalesInvoice:
						text = "SELECT " + text5 + " , SysDocID,VoucherID,Doc.CustomerID [Customer Code],CustomerName [Customer Name],TransactionDate [Date],Discount,(Total-Discount)AS Total,Doc.CreatedBy,Doc.DateCreated, Doc.UpdatedBy,Doc.DateUpdated FROM Sales_Invoice Doc INNER JOIN Customer C ON C.CustomerID = Doc.CustomerID AND ISNULL(IsExport,'False') = 'False'  WHERE 1=1   AND  ISNULL(IsVoid,'False') = 'False' ";
						break;
					case SysDocTypes.SalesReceipt:
						text = "SELECT " + text5 + " , SysDocID,VoucherID,Doc.CustomerID [Customer Code],CustomerName [Customer Name],TransactionDate [Date],Discount,(Total-Discount)AS Total,Doc.CreatedBy,Doc.DateCreated, Doc.UpdatedBy,Doc.DateUpdated FROM Sales_Invoice Doc INNER JOIN Customer C ON C.CustomerID = Doc.CustomerID AND ISNULL(IsExport,'False') = 'False'  WHERE 1=1   AND  ISNULL(IsVoid,'False') = 'False' ";
						break;
					case SysDocTypes.ExportSalesInvoice:
						text = "SELECT " + text5 + " , SysDocID,VoucherID,Doc.CustomerID [Customer Code],CustomerName [Customer Name],TransactionDate [Date],Discount,(Total-Discount)AS Total,Doc.CreatedBy,Doc.DateCreated, Doc.UpdatedBy,Doc.DateUpdated FROM Sales_Invoice Doc INNER JOIN Customer C ON C.CustomerID = Doc.CustomerID AND ISNULL(IsExport,'False') = 'True'  WHERE 1=1 ";
						break;
					default:
						switch (sysDocTypes)
						{
						case SysDocTypes.ExportSalesInvoice:
							text = "SELECT " + text5 + " , SysDocID,VoucherID,Doc.VendorID [Vendor Code],VendorName [Vendor Name],ContainerNumber,Doc.CreatedBy,Doc.DateCreated, Doc.UpdatedBy,Doc.DateUpdated  FROM Arrival_Report Doc INNER JOIN Vendor V ON V.VendorID = Doc.VendorID   WHERE 1=1 ";
							break;
						case SysDocTypes.ConsignIn:
							text = "SELECT " + text5 + " ,  SysDocID,VoucherID,Doc.VendorID [Vendor Code],VendorName [Vendor Name],TransactionDate [Date],Doc.CreatedBy,Doc.DateCreated, Doc.UpdatedBy,Doc.DateUpdated FROM Consign_In Doc INNER JOIN Vendor V ON V.VendorID = Doc.VendorID   WHERE 1=1 ";
							break;
						case SysDocTypes.ConsignInSettlement:
							text = "SELECT " + text5 + " ,  SysDocID,VoucherID,Doc.VendorID [Vendor Code],VendorName [Vendor Name],TransactionDate [Date],Doc.CreatedBy,Doc.DateCreated, Doc.UpdatedBy,Doc.DateUpdated FROM ConsignIn_Settlement Doc INNER JOIN Vendor V ON V.VendorID = Doc.VendorID   WHERE 1=1 ";
							break;
						default:
							switch (sysDocTypes)
							{
							case SysDocTypes.ConsignInSettlement:
								text = "SELECT " + text5 + " ,  SysDocID,VoucherID,Doc.VendorID [Vendor Code],VendorName [Vendor Name],TransactionDate [Date],Doc.CreatedBy,Doc.DateCreated, Doc.UpdatedBy,Doc.DateUpdated FROM ConsignIn_Settlement Doc INNER JOIN Vendor V ON V.VendorID = Doc.VendorID   WHERE 1=1 ";
								break;
							case SysDocTypes.ConsignOut:
								text = "SELECT " + text5 + " , SELECT  SysDocID,VoucherID,Doc.CustomerID [Customer Code],CustomerName [Customer Name],TransactionDate [Date],Doc.CreatedBy,Doc.DateCreated, Doc.UpdatedBy,Doc.DateUpdated FROM Consign_Out Doc INNER JOIN Customer C ON C.CustomerID = Doc.CustomerID  WHERE 1=1 ";
								break;
							case SysDocTypes.ConsignOutSettlement:
								text = "SELECT " + text5 + " , SELECT  SysDocID,VoucherID,Doc.CustomerID [Customer Code],CustomerName [Customer Name],TransactionDate [Date],Doc.CreatedBy,Doc.DateCreated, Doc.UpdatedBy,Doc.DateUpdated FROM ConsignOut_Settlement Doc INNER JOIN Customer C ON C.CustomerID = Doc.CustomerID  WHERE 1=1 ";
								break;
							case SysDocTypes.CashSalesReturn:
								text = "SELECT " + text5 + " , SysDocID,VoucherID,Doc.CustomerID [Customer Code],CustomerName [Customer Name],TransactionDate [Date],Discount,(Total-Discount)AS Total,Doc.CreatedBy,Doc.DateCreated, Doc.UpdatedBy,Doc.DateUpdated FROM Sales_Return Doc INNER JOIN Customer C ON C.CustomerID = Doc.CustomerID  WHERE 1=1 ";
								break;
							case SysDocTypes.CreditSalesReturn:
								text = "SELECT " + text5 + " , SysDocID,VoucherID,Doc.CustomerID [Customer Code],CustomerName [Customer Name],TransactionDate [Date],Discount,(Total-Discount)AS Total,Doc.CreatedBy,Doc.DateCreated, Doc.UpdatedBy,Doc.DateUpdated FROM Sales_Return Doc INNER JOIN Customer C ON C.CustomerID = Doc.CustomerID  WHERE 1=1 ";
								break;
							case SysDocTypes.FixedAssetTransfer:
								text = "SELECT " + text5 + " , SysDocID,VoucherID,Doc.AssetID [Customer Code],AssetName [Asset Name],TransactionDate [Date],Doc.CreatedBy,Doc.DateCreated, Doc.UpdatedBy,Doc.DateUpdated FROM FixedAsset_Transfer Doc INNER JOIN FixedAsset C ON C.AssetID = Doc.AssetID  WHERE 1=1 ";
								break;
							case SysDocTypes.CreditNote:
								text = "SELECT " + text5 + " , ISNULL(IsVoid,'False') AS V,Doc.SysDocID ,VoucherID ,TransactionDate [Date],\r\n\t\t\t\t\t\t\t                                CASE SysDocType WHEN 4 THEN 'Cash' WHEN 5 THEN 'Cheque' ELSE 'TT' END AS [Type],CostCenterID [C.C.],PayeeID [Payee Code],\r\n\t\t\t\t\t\t\t                                (CASE PayeeType\r\n\t\t\t\t\t\t\t\t\t\t\t\t\t                                WHEN 'C' THEN Customer.CustomerName\r\n\t\t\t\t\t\t\t\t\t\t\t\t\t                                WHEN 'V' THEN Vendor.VendorName\r\n\t\t\t\t\t\t\t\t\t\t\t\t\t                                WHEN 'E' THEN Employee.FirstName\r\n\t\t\t\t\t\t\t\t\t\t\t\t\t                                ELSE Account.AccountName END) AS [Payee Name],Reference [Ref],\r\n\t\t\t\t\t\t\t                                RegisterID [Reg],Doc.Description [Desc],\r\n\t\t\t\t\t\t\t                                ISNULL(Doc.CurrencyID,(SELECT CurrencyID FROM Currency WHERE IsBase=1)) AS [Cur] ,ISNULL(AmountFC,Amount) AS [Amount],Doc.CreatedBy,Doc.DateCreated, Doc.UpdatedBy,Doc.DateUpdated\r\n\t\t\t\t\t\t\t                                FROM GL_Transaction Doc\r\n\t\t\t\t\t\t\t                                LEFt OUTER JOIN Account ON Doc.PayeeID=Account.AccountID LEFt OUTER JOIN\r\n\t\t\t\t\t\t\t\t\t\t\t\t\t                                Customer ON Doc.PayeeID=Customer.CustomerID LEFt OUTER JOIN\r\n\t\t\t\t\t\t\t\t\t\t\t\t\t                                Vendor ON Doc.PayeeID=Vendor.VendorID LEFt OUTER JOIN\r\n\t\t\t\t\t\t\t\t\t\t\t\t\t                                Employee ON Doc.PayeeID=Employee.EmployeeID\r\n\t\t\t\t\t\t\t                                INNER JOIN System_Document SD ON SD.SysDocID=Doc.SysDocID\r\n                                                            WHERE SysDocType IN (11) ";
								break;
							case SysDocTypes.DebitNote:
								text = "SELECT " + text5 + " , ISNULL(IsVoid,'False') AS V,Doc.SysDocID ,VoucherID ,TransactionDate [Date],\r\n\t\t\t\t\t\t\t                                CASE SysDocType WHEN 4 THEN 'Cash' WHEN 5 THEN 'Cheque' ELSE 'TT' END AS [Type],CostCenterID [C.C.],PayeeID [Payee Code],\r\n\t\t\t\t\t\t\t                                (CASE PayeeType\r\n\t\t\t\t\t\t\t\t\t\t\t\t\t                                WHEN 'C' THEN Customer.CustomerName\r\n\t\t\t\t\t\t\t\t\t\t\t\t\t                                WHEN 'V' THEN Vendor.VendorName\r\n\t\t\t\t\t\t\t\t\t\t\t\t\t                                WHEN 'E' THEN Employee.FirstName\r\n\t\t\t\t\t\t\t\t\t\t\t\t\t                                ELSE Account.AccountName END) AS [Payee Name],Reference [Ref],\r\n\t\t\t\t\t\t\t                                RegisterID [Reg],Doc.Description [Desc],\r\n\t\t\t\t\t\t\t                                ISNULL(Doc.CurrencyID,(SELECT CurrencyID FROM Currency WHERE IsBase=1)) AS [Cur] ,ISNULL(AmountFC,Amount) AS [Amount],Doc.CreatedBy,Doc.DateCreated, Doc.UpdatedBy,Doc.DateUpdated\r\n\t\t\t\t\t\t\t                                FROM GL_Transaction Doc\r\n\t\t\t\t\t\t\t                                LEFt OUTER JOIN Account ON Doc.PayeeID=Account.AccountID LEFt OUTER JOIN\r\n\t\t\t\t\t\t\t\t\t\t\t\t\t                                Customer ON Doc.PayeeID=Customer.CustomerID LEFt OUTER JOIN\r\n\t\t\t\t\t\t\t\t\t\t\t\t\t                                Vendor ON Doc.PayeeID=Vendor.VendorID LEFt OUTER JOIN\r\n\t\t\t\t\t\t\t\t\t\t\t\t\t                                Employee ON Doc.PayeeID=Employee.EmployeeID\r\n\t\t\t\t\t\t\t                                INNER JOIN System_Document SD ON SD.SysDocID=Doc.SysDocID\r\n                                                            WHERE SysDocType IN (10) ";
								break;
							case SysDocTypes.GJournal:
								text = "SELECT " + text5 + " , ISNULL(IsVoid,'False') AS V,Doc.SysDocID ,VoucherID , JournalDate [Date],Reference [Ref],\r\n\t\t\t\t\t\t\t                                ISNULL(Doc.CurrencyID,(SELECT CurrencyID FROM Currency WHERE IsBase=1)) AS [Cur] ,\r\n\t\t\t\t\t\t\t\t\t\t\t\t\t(SELECT SUM(Debit) FROM Journal_Details JD WHERE JD.SysDocID = Doc.SysDocID AND JD.VoucherID = Doc.VoucherID) AS Amount,Doc.CreatedBy,Doc.DateCreated, Doc.UpdatedBy,Doc.DateUpdated\r\n\t\t\t\t\t\t\t                                FROM Journal Doc  WHERE SysDocType IN (1) ";
								break;
							case SysDocTypes.ProjectSubContractPO:
								text = "SELECT " + text5 + " , SysDocID,VoucherID,Doc.VendorID [Vendor],VendorName [Vendor Name],TransactionDate [Date],Discount,(Total-Discount)AS Total,Doc.CreatedBy,Doc.DateCreated, Doc.UpdatedBy,Doc.DateUpdated FROM Project_Subcontract_PO Doc INNER JOIN Vendor V ON V.VendorID = Doc.VendorID  WHERE 1=1 ";
								break;
							case SysDocTypes.ProjectSubContractPI:
								text = "SELECT " + text5 + " , SysDocID,VoucherID,Doc.VendorID [Vendor],VendorName [Vendor Name],TransactionDate [Date],Discount,(Total-Discount)AS Total,Doc.CreatedBy,Doc.DateCreated, Doc.UpdatedBy,Doc.DateUpdated FROM Project_SubContract_PI Doc INNER JOIN Vendor V ON V.VendorID = Doc.VendorID  WHERE 1=1 ";
								break;
							case SysDocTypes.ServiceCallTrack:
								text = "SELECT " + text5 + " , SysDocID,VoucherID,Doc.CustomerID [Customer Code],CustomerName [Customer Name],TransactionDate [Date],Doc.CreatedBy,Doc.DateCreated, Doc.UpdatedBy,Doc.DateUpdated FROM Service_CallTrack Doc INNER JOIN Customer C ON C.CustomerID = Doc.CustomerID   WHERE 1=1  ";
								break;
							case SysDocTypes.JobEstimation:
								text = "SELECT " + text5 + " , SysDocID,VoucherID,J.JobName,J.CustomerID,C.CustomerName,TransactionDate [Date],Doc.CreatedBy,Doc.DateCreated, Doc.UpdatedBy,Doc.DateUpdated FROM Job_Estimation Doc INNER JOIN Job J ON J.JobID=Doc.JobID INNER JOIN Customer C ON C.CustomerID=J.CustomerID   WHERE 1=1  ";
								break;
							case SysDocTypes.JobTimesheet:
								text = "SELECT " + text5 + " , SysDocID,VoucherID,ISNULL(FirstName,'')+ ' ' + ISNULL(MiddleName,'') + ' ' + ISNULL(LastName,'') [EmpName],TransactionDate [Date],Doc.CreatedBy,Doc.DateCreated, Doc.UpdatedBy,Doc.DateUpdated FROM Job_Timesheet Doc INNER JOIN Employee E ON Doc.EmployeeID=E.EmployeeID   WHERE 1=1  ";
								break;
							case SysDocTypes.JobExpenseIssue:
								text = "SELECT " + text5 + " , SysDocID,VoucherID,TransactionDate [Date],Doc.CreatedBy,Doc.DateCreated, Doc.UpdatedBy,Doc.DateUpdated FROM Job_Expense_Issue Doc  WHERE 1=1  ";
								break;
							case SysDocTypes.JobInventoryIssue:
								text = "SELECT " + text5 + " , SysDocID,VoucherID,TransactionDate [Date],Doc.CreatedBy,Doc.DateCreated, Doc.UpdatedBy,Doc.DateUpdated FROM Job_Inventory_Issue Doc  WHERE 1=1  ";
								break;
							case SysDocTypes.JobInventoryReturn:
								text = "SELECT " + text5 + " , SysDocID,VoucherID,TransactionDate [Date],Doc.CreatedBy,Doc.DateCreated, Doc.UpdatedBy,Doc.DateUpdated FROM Job_Inventory_Return Doc  WHERE 1=1  ";
								break;
							case SysDocTypes.JobInvoice:
								text = "SELECT " + text5 + " ,SysDocID,VoucherID,TransactionDate [Date],PT.TermName,C.CustomerName,S.FullName,J.JobName,ISNULL(ISNULL(Doc.TaxAmountFC,Doc.TaxAmount) ,0) AS Tax,( SELECT SUM(JFD.Amount) FROM Job_Fee_Detail JFD WHERE JFD.JobID=Doc.JobID ) AS [Fee Value],\r\n                                (SELECT SUM(Ji1.Total) FROM Job_Invoice Ji1 WHERE Ji1.JobID=Doc.JobID AND Ji1.TransactionDate <= Doc.TransactionDate ) AS [Total Invoice],                         \r\n                                 Doc.CreatedBy,Doc.DateCreated, Doc.UpdatedBy,Doc.DateUpdated\r\n                                FROM Job_Invoice Doc LEFT JOIN Customer C ON Doc.CustomerID=C.CustomerID\r\n                                LEFT JOIN Salesperson S ON Doc.SalespersonID=S.SalespersonID    \r\n                                LEFT JOIN Job J ON J.JobID=Doc.JobID\r\n                                LEFT JOIN Payment_Term PT ON PT.PaymentTermID=Doc.TermID\r\n                                LEFT OUTER JOIN Customer_Address CA ON CA.CustomerID = C.CustomerID\r\n                                WHERE 1 = 1 ";
								break;
							case SysDocTypes.JobClosing:
								text = "SELECT " + text5 + " , SysDocID,VoucherID,TransactionDate [Date],Doc.CreatedBy,Doc.DateCreated, Doc.UpdatedBy,Doc.DateUpdated FROM Job Doc  WHERE 1=1  ";
								break;
							case SysDocTypes.JobMaintenanceServiceEntry:
								text = "SELECT " + text5 + " , SysDocID,VoucherID,Doc.JobID [Job ID],J.JobName [Job Name],Doc.LocationID,L.LocationName [Location Name],TransactionDate [Date],\r\n                                Doc.CreatedBy,Doc.DateCreated, Doc.UpdatedBy,Doc.DateUpdated FROM \r\n                                Job_Maintenance_Service Doc  INNER JOIN Location L ON L.LocationID = Doc.LocationID INNER JOIN Job J ON Doc.JobID =J.JobID  WHERE 1=1 ";
								break;
							case SysDocTypes.SalaryPaymentCash:
							case SysDocTypes.SalaryPaymentCheque:
							case SysDocTypes.SalaryPaymentBank:
								text = "SELECT " + text5 + " ,Doc.SysDocID,Doc.VoucherID,Doc.TransactionDate [Date],A.AccountName,(SELECT BankName FROM Chequebook C LEFT JOIN Bank B ON C.BankID=B.BankID WHERE C.ChequebookID=Doc.ChequebookID) AS 'PayeeBank',Doc.CreatedBy,Doc.DateCreated, Doc.UpdatedBy,Doc.DateUpdated FROM Payroll_Transaction Doc LEFT JOIN Account A ON Doc.BankAccountID=A.AccountID  WHERE 1=1 ";
								break;
							case SysDocTypes.ProjectExpenseAllocation:
								text = "SELECT " + text5 + " , SysDocID,VoucherID,Month,Year,TransactionDate [Date],Doc.CreatedBy,Doc.DateCreated, Doc.UpdatedBy,Doc.DateUpdated FROM Project_Expense_Allocation Doc  WHERE 1=1  ";
								break;
							case SysDocTypes.LPOReceipt:
								text = "SELECT " + text5 + " , SysDocID,VoucherID,TransactionDate [Date],Doc.CreatedBy,Doc.DateCreated, Doc.UpdatedBy,Doc.DateUpdated FROM LPO_Receipt Doc  WHERE 1=1  ";
								break;
							case SysDocTypes.TR:
								text = "SELECT " + text5 + " , SysDocID,VoucherID,TransactionDate [Date],Doc.CreatedBy,Doc.DateCreated, Doc.UpdatedBy,Doc.DateUpdated FROM Bank_Facility_Transaction Doc  WHERE 1=1  ";
								break;
							case SysDocTypes.TRPayment:
								text = "SELECT " + text5 + " , SysDocID,VoucherID,TransactionDate [Date],Doc.CreatedBy,Doc.DateCreated, Doc.UpdatedBy,Doc.DateUpdated FROM Bank_Facility_Payment Doc  WHERE 1=1  ";
								break;
							case SysDocTypes.TRApplication:
								text = "SELECT " + text5 + " , SysDocID,VoucherID,TransactionDate [Date],Doc.CreatedBy,Doc.DateCreated, Doc.UpdatedBy,Doc.DateUpdated FROM TR_Application Doc  WHERE 1=1  ";
								break;
							case SysDocTypes.SendChequesToBank:
								text = "SELECT " + text5 + " , SysDocID,VoucherID,TransactionDate [Date],Doc.CreatedBy,Doc.DateCreated, Doc.UpdatedBy,Doc.DateUpdated FROM Cheque_Send Doc  WHERE 1=1  ";
								break;
							case SysDocTypes.ChequeDiscount:
								text = "SELECT " + text5 + " , SysDocID,VoucherID,TransactionDate [Date],Doc.CreatedBy,Doc.DateCreated, Doc.UpdatedBy,Doc.DateUpdated FROM Cheque_Discount Doc  WHERE 1=1  ";
								break;
							case SysDocTypes.IssuedSecurityCheque:
								text = "SELECT " + text5 + " , SysDocID,VoucherID,IssueDate [Date] FROM Security_Cheque Doc  WHERE 1=1  ";
								break;
							default:
								throw new Exception("Transaction Type:" + sysDocTypes.ToString() + " is not implemented in GetUserPendingApprovalTasks() function.");
							}
							break;
						}
						break;
					}
					break;
				}
				case 2:
				{
					text5 = " CAST(" + text4 + " AS int) AS ObjectID, CAST(2 as int) AS ObjectType, (SELECT TOP 1 TaskID FROM Approval_Task AT WHERE ISNULL(IsExpired,'False') = 'False' AND Status = 1 AND ApprovalType = " + (int)approvalType + " AND ApprovalID = '" + approvalID + "' AND AT.DocumentCode = Card.Code  AND ((AT.AssigneeType  IN (1,3)  AND AssigneeID = '" + userID + "') OR (AssigneeType =2 AND AssigneeID \r\n                                    IN (SELECT GroupID FROM User_Group_Detail UG WHERE UG.UserID = '" + userID + "')))) AS TaskID";
					DataComboType dataComboType = (DataComboType)int.Parse(text4);
					switch (dataComboType)
					{
					case DataComboType.Customer:
						text = "SELECT " + text5 + ", Card.* FROM  ( SELECT CustomerID Code,CustomerName Name,CountryID Country,CreatedBy,DateCreated, UpdatedBy,DateUpdated FROM Customer ) AS Card   WHERE 1=1 ";
						break;
					case DataComboType.Vendor:
						text = "SELECT " + text5 + ", Card.* FROM  ( SELECT  VendorID Code,VendorName Name,CountryID Country,CreatedBy,DateCreated, UpdatedBy,DateUpdated FROM Vendor ) AS Card    WHERE 1=1 ";
						break;
					case DataComboType.Employee:
						text = "SELECT " + text5 + ", Card.* FROM  ( SELECT EmployeeID Code,FirstName + ' ' + LastName AS Name,PositionID Position,CreatedBy,DateCreated, UpdatedBy,DateUpdated FROM Employee ) AS Card    WHERE 1=1 ";
						break;
					case DataComboType.Product:
						text = "SELECT " + text5 + ", Card.* FROM  ( SELECT ProductID Code,Description AS Name,ClassID AS Class, CategoryID AS Category,CreatedBy,DateCreated, UpdatedBy,DateUpdated FROM Product ) AS Card    WHERE 1=1  ";
						break;
					case DataComboType.Accounts:
						text = "SELECT " + text5 + ", Card.* FROM  ( SELECT AccountID Code,AccountName AS Name,GroupID AS [Account Group],CreatedBy,DateCreated, UpdatedBy,DateUpdated    FROM Account ) AS Card     WHERE 1=1 ";
						break;
					case DataComboType.Job:
						text = "SELECT " + text5 + ", Card.* FROM  ( SELECT JobID Code,JobName AS Name,CreatedBy,DateCreated, UpdatedBy,DateUpdated   FROM Job  ) AS Card    WHERE 1=1 ";
						break;
					case DataComboType.JobBOM:
						text = "SELECT " + text5 + ", Card.* FROM  ( SELECT JobBOMID Code,BOMName AS BOMName,CreatedBy,DateCreated, UpdatedBy,DateUpdated   FROM Job_BOM ) AS Card     WHERE 1=1 ";
						break;
					default:
						throw new Exception("Card Type:" + dataComboType.ToString() + " is not implemented in GetUserPendingApprovalTasks() function.");
					}
					break;
				}
				}
				if (a != "")
				{
					text2 = "  AND AT.DocumentSysDocID = Doc.SysDocID ";
				}
				text = text + "  AND EXISTS(\r\n                                 SELECT * FROM Approval_Task AT WHERE ISNULL(IsExpired,'False') = 'False' AND ApprovalType = " + (int)approvalType + " AND ApprovalID = '" + approvalID + "' AND ((AT.AssigneeType  IN (1,3)  AND AssigneeID = '" + userID + "') OR (AssigneeType =2 AND AssigneeID \r\n                                 IN (SELECT GroupID FROM User_Group_Detail UG WHERE UG.UserID = '" + userID + "')))  AND AT.Status IN (1,2)  AND ";
				text = ((b == 1 && a2 != "") ? (text + " (AT.DocumentCode = Doc." + text3 + text2 + "))") : ((b != 1 || !(a2 == "")) ? (text + " (AT.DocumentCode = Card.Code))") : (text + " (AT.DocumentCode = Doc." + text3 + "))")));
				FillDataSet(dataSet, "Approval", text);
				return dataSet;
			}
			catch
			{
				throw;
			}
		}

		public DataSet GetUserPendingApprovalTasks(ApprovalTypes approvalType, string approvalID)
		{
			try
			{
				DataSet dataSet = new DataSet();
				string userID = base.DBConfig.UserID;
				string text = "";
				string text2 = "";
				string text3 = "VoucherID";
				string a = "SysDocID";
				ApprovalData approvalByID = GetApprovalByID(approvalType, approvalID);
				byte b = byte.Parse(approvalByID.ApprovalTable.Rows[0]["ObjectType"].ToString());
				string text4 = approvalByID.ApprovalTable.Rows[0]["ObjectID"].ToString();
				string a2 = approvalByID.ApprovalTable.Rows[0]["ObjectSysDocID"].ToString();
				string text5 = string.Empty;
				if (approvalType == ApprovalTypes.Approval)
				{
					text5 = approvalByID.ApprovalLevelTable.Select("ApproverID='" + base.DBConfig.UserID + "' ")[0]["Condition"].ToString();
				}
				SysDocTypes sysDocTypes = (SysDocTypes)int.Parse(text4);
				string text6 = "";
				switch (b)
				{
				case 1:
					text6 = " CAST(" + text4 + " AS int) AS ObjectID, CAST(1 as int) AS ObjectType, (SELECT TOP 1 TaskID FROM Approval_Task AT WHERE ISNULL(IsExpired,'False') = 'False' AND Status IN (1,2) AND ApprovalType = " + (int)approvalType + " AND ApprovalID = '" + approvalID + "' AND AT.DocumentCode = Doc.VoucherID  ";
					if (a2 != "")
					{
						text6 += " AND AT.DocumentSysDocID = Doc.SysDocID ";
					}
					text6 = text6 + " AND ((AT.AssigneeType IN (1,3) AND AssigneeID = '" + userID + "') OR (AssigneeType =2 AND AssigneeID \r\n                                    IN (SELECT GroupID FROM User_Group_Detail UG WHERE UG.UserID = '" + userID + "')))) AS TaskID";
					switch (sysDocTypes)
					{
					case SysDocTypes.PurchaseOrder:
						text = "SELECT " + text6 + " , SysDocID,VoucherID,Doc.VendorID [Vendor Code],VendorName [Vendor Name],TransactionDate [Date],Discount,(Total-Discount)AS Total,Doc.CreatedBy,Doc.DateCreated, Doc.UpdatedBy,Doc.DateUpdated FROM Purchase_Order Doc INNER JOIN Vendor V ON V.VendorID = Doc.VendorID AND ISNULL(IsImport,'False') = 'False'  WHERE 1=1  AND  ISNULL(IsVoid,'False') = 'False'  ";
						break;
					case SysDocTypes.ImportPurchaseOrder:
						text = "SELECT " + text6 + " , SysDocID,VoucherID,Doc.VendorID [Vendor Code],VendorName [Vendor Name],TransactionDate [Date],Discount,(Total-Discount)AS Total,Doc.CreatedBy,Doc.DateCreated, Doc.UpdatedBy,Doc.DateUpdated FROM Purchase_Order Doc INNER JOIN Vendor V ON V.VendorID = Doc.VendorID AND ISNULL(IsImport,'False') = 'True'   WHERE 1=1 AND  ISNULL(IsVoid,'False') = 'False' ";
						break;
					case SysDocTypes.PurchaseOrderNI:
						text = "SELECT " + text6 + " , SysDocID,VoucherID,Doc.VendorID [Vendor Code],VendorName [Vendor Name],TransactionDate [Date],Discount,(Total-Discount)AS Total,Doc.CreatedBy,Doc.DateCreated, Doc.UpdatedBy,Doc.DateUpdated FROM Purchase_Order_NonInv Doc INNER JOIN Vendor V ON V.VendorID = Doc.VendorID AND ISNULL(IsImport,'False') = 'False'  WHERE 1=1 AND  ISNULL(IsVoid,'False') = 'False' ";
						break;
					case SysDocTypes.PurchaseInvoiceNI:
						text = "SELECT " + text6 + " , SysDocID,VoucherID,Doc.VendorID [Vendor Code],VendorName [Vendor Name],TransactionDate [Date],Discount,(Total-Discount)AS Total,Doc.CreatedBy,Doc.DateCreated, Doc.UpdatedBy,Doc.DateUpdated FROM Purchase_Invoice_NonInv Doc INNER JOIN Vendor V ON V.VendorID = Doc.VendorID AND ISNULL(IsImport,'False') = 'False'  WHERE 1=1 AND  ISNULL(IsVoid,'False') = 'False' ";
						break;
					case SysDocTypes.GoodsReceivedNote:
						text = "SELECT " + text6 + " , SysDocID,VoucherID,Doc.VendorID [Vendor Code],VendorName [Vendor Name],TransactionDate [Date],Discount,(Total-Discount)AS Total,Doc.CreatedBy,Doc.DateCreated, Doc.UpdatedBy,Doc.DateUpdated FROM Purchase_Receipt Doc INNER JOIN Vendor V ON V.VendorID = Doc.VendorID AND ISNULL(IsImport,'False') = 'False'  WHERE 1=1 AND  ISNULL(IsVoid,'False') = 'False'";
						break;
					case SysDocTypes.ImportGoodsReceivedNote:
						text = "SELECT " + text6 + " , SysDocID,VoucherID,Doc.VendorID [Vendor Code],VendorName [Vendor Name],TransactionDate [Date],Discount,(Total-Discount)AS Total,Doc.CreatedBy,Doc.DateCreated, Doc.UpdatedBy,Doc.DateUpdated FROM Purchase_Receipt Doc INNER JOIN Vendor V ON V.VendorID = Doc.VendorID AND ISNULL(IsImport,'False') = 'True'   WHERE 1=1 AND  ISNULL(IsVoid,'False') = 'False'";
						break;
					case SysDocTypes.CashPurchase:
						text = "SELECT " + text6 + " , SysDocID,VoucherID,Doc.VendorID [Vendor Code],VendorName [Vendor Name],TransactionDate [Date],Discount,(Total-Discount)AS Total,Doc.CreatedBy,Doc.DateCreated, Doc.UpdatedBy,Doc.DateUpdated FROM Purchase_Invoice Doc INNER JOIN Vendor V ON V.VendorID = Doc.VendorID AND ISNULL(IsCash,'True') = 'True'  WHERE 1=1 AND  ISNULL(IsVoid,'False') = 'False'";
						break;
					case SysDocTypes.PurchaseQuote:
						text = "SELECT " + text6 + " , SysDocID,VoucherID,Doc.VendorID [Vendor Code],VendorName [Vendor Name],TransactionDate [Date],Discount,(Total-Discount)AS Total,Doc.CreatedBy,Doc.DateCreated, Doc.UpdatedBy,Doc.DateUpdated FROM Purchase_Quote Doc INNER JOIN Vendor V ON V.VendorID = Doc.VendorID AND ISNULL(IsImport,'False') = 'False'   WHERE 1=1 AND  ISNULL(IsVoid,'False') = 'False' ";
						break;
					case SysDocTypes.PurchaseInvoice:
						text = "SELECT " + text6 + " , SysDocID,VoucherID,Doc.VendorID [Vendor Code],VendorName [Vendor Name],TransactionDate [Date],Discount,(Total-Discount)AS Total,Doc.CreatedBy,Doc.DateCreated, Doc.UpdatedBy,Doc.DateUpdated FROM Purchase_Invoice Doc INNER JOIN Vendor V ON V.VendorID = Doc.VendorID AND ISNULL(IsImport,'False') = 'False'   WHERE 1=1 AND  ISNULL(IsVoid,'False') = 'False' ";
						break;
					case SysDocTypes.ImportPurchaseInvoice:
						text = "SELECT " + text6 + " , SysDocID,VoucherID,Doc.VendorID [Vendor Code],VendorName [Vendor Name],TransactionDate [Date],Discount,(Total-Discount)AS Total,Doc.CreatedBy,Doc.DateCreated, Doc.UpdatedBy,Doc.DateUpdated FROM Purchase_Invoice Doc INNER JOIN Vendor V ON V.VendorID = Doc.VendorID AND ISNULL(IsImport,'False') = 'True'  WHERE 1=1 AND  ISNULL(IsVoid,'False') = 'False'";
						break;
					case SysDocTypes.PackingList:
						text = "SELECT " + text6 + " , SysDocID,VoucherID,Doc.VendorID [Vendor Code],VendorName [Vendor Name],TransactionDate [Date],Doc.CreatedBy,Doc.DateCreated, Doc.UpdatedBy,Doc.DateUpdated FROM PO_Shipment Doc INNER JOIN Vendor V ON V.VendorID = Doc.VendorID  WHERE 1=1 ";
						break;
					case SysDocTypes.ProformaInvoice:
						text = "SELECT " + text6 + " , SysDocID,VoucherID,Doc.VendorID [Vendor Code],VendorName [Vendor Name],TransactionDate [Date],Discount,(Total-Discount)AS Total,Doc.CreatedBy,Doc.DateCreated, Doc.UpdatedBy,Doc.DateUpdated FROM Purchase_Quote Doc INNER JOIN Vendor V ON V.VendorID = Doc.VendorID AND ISNULL(IsImport,'False') = 'True' AND  ISNULL(IsVoid,'False') = 'False'  WHERE 1=1 ";
						break;
					case SysDocTypes.SalesEnquiry:
						text = "SELECT " + text6 + " , SysDocID,VoucherID,Doc.CustomerID [Customer Code],CustomerName [Customer Name],TransactionDate [Date],Discount,(Total-Discount)AS Total,Doc.CreatedBy,Doc.DateCreated, Doc.UpdatedBy,Doc.DateUpdated FROM Sales_Enquiry Doc INNER JOIN Customer C ON C.CustomerID = Doc.CustomerID AND ISNULL(IsExport,'False') = 'False'   WHERE 1=1 AND  ISNULL(IsVoid,'False') = 'False' ";
						break;
					case SysDocTypes.SalesOrder:
						text = "SELECT " + text6 + " , SysDocID,VoucherID,Doc.CustomerID [Customer Code],CustomerName [Customer Name],TransactionDate [Date],Discount,(Total-Discount)AS Total,Doc.CreatedBy,Doc.DateCreated, Doc.UpdatedBy,Doc.DateUpdated FROM Sales_Order Doc INNER JOIN Customer C ON C.CustomerID = Doc.CustomerID AND ISNULL(IsExport,'False') = 'False'   WHERE 1=1 AND  ISNULL(IsVoid,'False') = 'False' ";
						break;
					case SysDocTypes.ExportSalesOrder:
						text = "SELECT " + text6 + " , SysDocID,VoucherID,Doc.CustomerID [Customer Code],CustomerName [Customer Name],TransactionDate [Date],Discount,(Total-Discount)AS Total,Doc.CreatedBy,Doc.DateCreated, Doc.UpdatedBy,Doc.DateUpdated FROM Sales_Order Doc INNER JOIN Customer C ON C.CustomerID = Doc.CustomerID AND ISNULL(IsExport,'False') = 'True'  WHERE 1=1 AND  ISNULL(IsVoid,'False') = 'False'";
						break;
					case SysDocTypes.SalesQuote:
						text = "SELECT " + text6 + " , SysDocID,VoucherID,Doc.CustomerID [Customer Code],CustomerName [Customer Name],TransactionDate [Date],Discount,(Total-Discount)AS Total,Doc.CreatedBy,Doc.DateCreated, Doc.UpdatedBy,Doc.DateUpdated FROM Sales_Quote Doc INNER JOIN Customer C ON C.CustomerID = Doc.CustomerID  AND  ISNULL(IsVoid,'False') = 'False'  WHERE 1=1  AND  ISNULL(IsVoid,'False') = 'False'";
						break;
					case SysDocTypes.DeliveryNote:
						text = "SELECT " + text6 + " , SysDocID,VoucherID,Doc.CustomerID [Customer Code],CustomerName [Customer Name],TransactionDate [Date],Discount,(Total-Discount)AS Total,Doc.CreatedBy,Doc.DateCreated, Doc.UpdatedBy,Doc.DateUpdated FROM Delivery_Note Doc INNER JOIN Customer C ON C.CustomerID = Doc.CustomerID  AND  ISNULL(IsVoid,'False') = 'False'  WHERE 1=1  AND  ISNULL(IsVoid,'False') = 'False'";
						break;
					case SysDocTypes.InventoryNoneSale:
						text = "SELECT " + text6 + " ,SysDocID,VoucherID,LocationID,AdjustmentTypeID,TransactionDate [Date],Doc.CreatedBy,Doc.DateCreated, Doc.UpdatedBy,Doc.DateUpdated FROM Inventory_Damage Doc  WHERE 1=1 AND  ISNULL(IsVoid,'False') = 'False' ";
						break;
					case SysDocTypes.InventoryAdjustment:
						text = "SELECT " + text6 + " ,SysDocID,VoucherID,LocationID,AdjustmentTypeID,TransactionDate [Date],Doc.CreatedBy,Doc.DateCreated, Doc.UpdatedBy,Doc.DateUpdated FROM Inventory_Adjustment Doc AND  ISNULL(IsVoid,'False') = 'False' WHERE 1=1 AND  ISNULL(IsVoid,'False') = 'False' ";
						break;
					case SysDocTypes.JobMaterialRequisition:
						text = "SELECT " + text6 + " ,SysDocID,VoucherID,LocationID,TransactionDate [Date],Doc.CreatedBy,Doc.DateCreated, Doc.UpdatedBy,Doc.DateUpdated FROM Job_Material_Requisition Doc  WHERE 1=1 ";
						break;
					case SysDocTypes.InventoryRepacking:
						text = "SELECT " + text6 + " ,SysDocID,VoucherID,LocationID,TransactionDate [Date],Doc.CreatedBy,Doc.DateCreated, Doc.UpdatedBy,Doc.DateUpdated FROM Inventory_Repacking Doc    WHERE 1=1  AND  ISNULL(IsVoid,'False') = 'False'";
						break;
					case SysDocTypes.CLVoucher:
						text = "SELECT " + text6 + " ,SysDocID,VoucherID,Doc.CustomerID,Doc.Amount,Doc.CreatedBy,Doc.DateCreated, Doc.UpdatedBy,Doc.DateUpdated FROM CL_Voucher Doc  WHERE 1=1 AND  ISNULL(IsVoid,'False') = 'False'";
						break;
					case SysDocTypes.CreditLimitReview:
						text = "SELECT " + text6 + " ,SysDocID,VoucherID,Doc.CustomerID,Doc.CreditAmount,Doc.CreatedBy,Doc.DateCreated, Doc.UpdatedBy,Doc.DateUpdated FROM Credit_Limit_Review Doc  WHERE 1=1  ";
						break;
					case SysDocTypes.PurchasePrepaymentInvoice:
						text = "SELECT " + text6 + " ,SysDocID,VoucherID,Doc.VendorID,Doc.TransactionDate,Doc.CreatedBy,Doc.DateCreated, Doc.UpdatedBy,Doc.DateUpdated FROM Purchase_Prepayment_Invoice Doc  WHERE 1=1  AND  ISNULL(IsVoid,'False') = 'False'";
						break;
					case SysDocTypes.EmployeeGeneralActivity:
						text = "SELECT " + text6 + " ,SysDocID,VoucherID,TransactionDate [Date],Doc.CreatedBy,Doc.DateCreated, Doc.UpdatedBy,Doc.DateUpdated FROM Employee_GeneralActivity Doc  WHERE 1=1 AND  ISNULL(IsVoid,'False') = 'False'";
						break;
					case SysDocTypes.EmployeeAppraisal:
						text = "SELECT " + text6 + " ,SysDocID,VoucherID,TransactionDate [Date],Doc.CreatedBy,Doc.DateCreated, Doc.UpdatedBy,Doc.DateUpdated FROM Employee_Appraisal Doc    WHERE 1=1 ";
						break;
					case SysDocTypes.CashPayment:
						text = "SELECT " + text6 + " , ISNULL(IsVoid,'False') AS V,Doc.SysDocID ,VoucherID ,TransactionDate [Date],\r\n\t\t\t\t\t\t\t                                CASE SysDocType WHEN 4 THEN 'Cash' WHEN 5 THEN 'Cheque' ELSE 'TT' END AS [Type],CostCenterID [C.C.],PayeeID [Payee Code],\r\n\t\t\t\t\t\t\t                                (CASE PayeeType\r\n\t\t\t\t\t\t\t\t\t\t\t\t\t                                WHEN 'C' THEN Customer.CustomerName\r\n\t\t\t\t\t\t\t\t\t\t\t\t\t                                WHEN 'V' THEN Vendor.VendorName\r\n\t\t\t\t\t\t\t\t\t\t\t\t\t                                WHEN 'E' THEN Employee.FirstName\r\n\t\t\t\t\t\t\t\t\t\t\t\t\t                                ELSE Account.AccountName END) AS [Payee Name],Reference [Ref],\r\n\t\t\t\t\t\t\t                                RegisterID [Reg],Doc.Description [Desc],\r\n\t\t\t\t\t\t\t                                ISNULL(Doc.CurrencyID,(SELECT CurrencyID FROM Currency WHERE IsBase=1)) AS [Cur] ,ISNULL(AmountFC,Amount) AS [Amount],Doc.CreatedBy,Doc.DateCreated, Doc.UpdatedBy,Doc.DateUpdated\r\n\t\t\t\t\t\t\t                                FROM GL_Transaction Doc\r\n\t\t\t\t\t\t\t                                LEFt OUTER JOIN Account ON Doc.PayeeID=Account.AccountID LEFt OUTER JOIN\r\n\t\t\t\t\t\t\t\t\t\t\t\t\t                                Customer ON Doc.PayeeID=Customer.CustomerID LEFt OUTER JOIN\r\n\t\t\t\t\t\t\t\t\t\t\t\t\t                                Vendor ON Doc.PayeeID=Vendor.VendorID LEFt OUTER JOIN\r\n\t\t\t\t\t\t\t\t\t\t\t\t\t                                Employee ON Doc.PayeeID=Employee.EmployeeID\r\n\t\t\t\t\t\t\t                                INNER JOIN System_Document SD ON SD.SysDocID=Doc.SysDocID\r\n                                                            WHERE SysDocType IN (4) ";
						break;
					case SysDocTypes.CashExpense:
						text = "SELECT " + text6 + " , ISNULL(IsVoid,'False') AS V,Doc.SysDocID ,VoucherID ,TransactionDate [Date],\r\n\t\t\t\t\t\t\t                                CASE SysDocType WHEN 4 THEN 'Cash' WHEN 5 THEN 'Cheque' ELSE 'TT' END AS [Type],CostCenterID [C.C.],PayeeID [Payee Code],\r\n\t\t\t\t\t\t\t                                (CASE PayeeType\r\n\t\t\t\t\t\t\t\t\t\t\t\t\t                                WHEN 'C' THEN Customer.CustomerName\r\n\t\t\t\t\t\t\t\t\t\t\t\t\t                                WHEN 'V' THEN Vendor.VendorName\r\n\t\t\t\t\t\t\t\t\t\t\t\t\t                                WHEN 'E' THEN Employee.FirstName\r\n\t\t\t\t\t\t\t\t\t\t\t\t\t                                ELSE Account.AccountName END) AS [Payee Name],Reference [Ref],\r\n\t\t\t\t\t\t\t                                RegisterID [Reg],Doc.Description [Desc],\r\n\t\t\t\t\t\t\t                                ISNULL(Doc.CurrencyID,(SELECT CurrencyID FROM Currency WHERE IsBase=1)) AS [Cur] ,ISNULL(AmountFC,Amount) AS [Amount],Doc.CreatedBy,Doc.DateCreated, Doc.UpdatedBy,Doc.DateUpdated\r\n\t\t\t\t\t\t\t                                FROM GL_Transaction Doc\r\n\t\t\t\t\t\t\t                                LEFt OUTER JOIN Account ON Doc.PayeeID=Account.AccountID LEFt OUTER JOIN\r\n\t\t\t\t\t\t\t\t\t\t\t\t\t                                Customer ON Doc.PayeeID=Customer.CustomerID LEFt OUTER JOIN\r\n\t\t\t\t\t\t\t\t\t\t\t\t\t                                Vendor ON Doc.PayeeID=Vendor.VendorID LEFt OUTER JOIN\r\n\t\t\t\t\t\t\t\t\t\t\t\t\t                                Employee ON Doc.PayeeID=Employee.EmployeeID\r\n\t\t\t\t\t\t\t                                INNER JOIN System_Document SD ON SD.SysDocID=Doc.SysDocID\r\n                                                            WHERE SysDocType IN (8) ";
						break;
					case SysDocTypes.ChequePayment:
						text = "SELECT " + text6 + " , ISNULL(IsVoid,'False') AS V,Doc.SysDocID ,VoucherID,TransactionDate [Date],\r\n\t\t\t\t\t\t\t                                CASE SysDocType WHEN 4 THEN 'Cash' WHEN 5 THEN 'Cheque' ELSE 'TT' END AS [Type],CostCenterID [C.C.],PayeeID [Payee Code],\r\n\t\t\t\t\t\t\t                                (CASE PayeeType\r\n\t\t\t\t\t\t\t\t\t\t\t\t\t                                WHEN 'C' THEN Customer.CustomerName\r\n\t\t\t\t\t\t\t\t\t\t\t\t\t                                WHEN 'V' THEN Vendor.VendorName\r\n\t\t\t\t\t\t\t\t\t\t\t\t\t                                WHEN 'E' THEN Employee.FirstName\r\n\t\t\t\t\t\t\t\t\t\t\t\t\t                                ELSE Account.AccountName END) AS [Payee Name],Reference [Ref],\r\n\t\t\t\t\t\t\t                                RegisterID [Reg],Doc.Description [Desc],\r\n\t\t\t\t\t\t\t                                ISNULL(Doc.CurrencyID,(SELECT CurrencyID FROM Currency WHERE IsBase=1)) AS [Cur] ,ISNULL(AmountFC,Amount) AS [Amount],Doc.CreatedBy,Doc.DateCreated, Doc.UpdatedBy,Doc.DateUpdated\r\n\t\t\t\t\t\t\t                                FROM GL_Transaction Doc\r\n\t\t\t\t\t\t\t                                LEFt OUTER JOIN Account ON Doc.PayeeID=Account.AccountID LEFt OUTER JOIN\r\n\t\t\t\t\t\t\t\t\t\t\t\t\t                                Customer ON Doc.PayeeID=Customer.CustomerID LEFt OUTER JOIN\r\n\t\t\t\t\t\t\t\t\t\t\t\t\t                                Vendor ON Doc.PayeeID=Vendor.VendorID LEFt OUTER JOIN\r\n\t\t\t\t\t\t\t\t\t\t\t\t\t                                Employee ON Doc.PayeeID=Employee.EmployeeID\r\n\t\t\t\t\t\t\t                                INNER JOIN System_Document SD ON SD.SysDocID=Doc.SysDocID\r\n                                                            WHERE SysDocType IN (5) ";
						break;
					case SysDocTypes.CashReceipt:
						text = "SELECT " + text6 + " , ISNULL(IsVoid,'False') AS V,Doc.SysDocID ,VoucherID,TransactionDate [Date],\r\n\t\t\t\t\t\t\t                                CASE SysDocType WHEN 4 THEN 'Cash' WHEN 5 THEN 'Cheque' ELSE 'TT' END AS [Type],CostCenterID [C.C.],PayeeID [Payee Code],\r\n\t\t\t\t\t\t\t                                (CASE PayeeType\r\n\t\t\t\t\t\t\t\t\t\t\t\t\t                                WHEN 'C' THEN Customer.CustomerName\r\n\t\t\t\t\t\t\t\t\t\t\t\t\t                                WHEN 'V' THEN Vendor.VendorName\r\n\t\t\t\t\t\t\t\t\t\t\t\t\t                                WHEN 'E' THEN Employee.FirstName\r\n\t\t\t\t\t\t\t\t\t\t\t\t\t                                ELSE Account.AccountName END) AS [Payee Name],Reference [Ref],\r\n\t\t\t\t\t\t\t                                RegisterID [Reg],Doc.Description [Desc],\r\n\t\t\t\t\t\t\t                                ISNULL(Doc.CurrencyID,(SELECT CurrencyID FROM Currency WHERE IsBase=1)) AS [Cur] ,ISNULL(AmountFC,Amount) AS [Amount],Doc.CreatedBy,Doc.DateCreated, Doc.UpdatedBy,Doc.DateUpdated\r\n\t\t\t\t\t\t\t                                FROM GL_Transaction Doc\r\n\t\t\t\t\t\t\t                                LEFt OUTER JOIN Account ON Doc.PayeeID=Account.AccountID LEFt OUTER JOIN\r\n\t\t\t\t\t\t\t\t\t\t\t\t\t                                Customer ON Doc.PayeeID=Customer.CustomerID LEFt OUTER JOIN\r\n\t\t\t\t\t\t\t\t\t\t\t\t\t                                Vendor ON Doc.PayeeID=Vendor.VendorID LEFt OUTER JOIN\r\n\t\t\t\t\t\t\t\t\t\t\t\t\t                                Employee ON Doc.PayeeID=Employee.EmployeeID\r\n\t\t\t\t\t\t\t                                INNER JOIN System_Document SD ON SD.SysDocID=Doc.SysDocID\r\n                                                            WHERE SysDocType IN (3) ";
						break;
					case SysDocTypes.TTPayment:
						text = "SELECT " + text6 + " , ISNULL(IsVoid,'False') AS V,Doc.SysDocID ,VoucherID ,TransactionDate [Date],\r\n\t\t\t\t\t\t\t                                CASE SysDocType WHEN 4 THEN 'Cash' WHEN 5 THEN 'Cheque' ELSE 'TT' END AS [Type],CostCenterID [C.C.],PayeeID [Payee Code],\r\n\t\t\t\t\t\t\t                                (CASE PayeeType\r\n\t\t\t\t\t\t\t\t\t\t\t\t\t                                WHEN 'C' THEN Customer.CustomerName\r\n\t\t\t\t\t\t\t\t\t\t\t\t\t                                WHEN 'V' THEN Vendor.VendorName\r\n\t\t\t\t\t\t\t\t\t\t\t\t\t                                WHEN 'E' THEN Employee.FirstName\r\n\t\t\t\t\t\t\t\t\t\t\t\t\t                                ELSE Account.AccountName END) AS [Payee Name],Reference [Ref],\r\n\t\t\t\t\t\t\t                                RegisterID [Reg],Doc.Description [Desc],\r\n\t\t\t\t\t\t\t                                ISNULL(Doc.CurrencyID,(SELECT CurrencyID FROM Currency WHERE IsBase=1)) AS [Cur] ,ISNULL(AmountFC,Amount) AS [Amount],Doc.CreatedBy,Doc.DateCreated, Doc.UpdatedBy,Doc.DateUpdated\r\n\t\t\t\t\t\t\t                                FROM GL_Transaction Doc\r\n\t\t\t\t\t\t\t                                LEFt OUTER JOIN Account ON Doc.PayeeID=Account.AccountID LEFt OUTER JOIN\r\n\t\t\t\t\t\t\t\t\t\t\t\t\t                                Customer ON Doc.PayeeID=Customer.CustomerID LEFt OUTER JOIN\r\n\t\t\t\t\t\t\t\t\t\t\t\t\t                                Vendor ON Doc.PayeeID=Vendor.VendorID LEFt OUTER JOIN\r\n\t\t\t\t\t\t\t\t\t\t\t\t\t                                Employee ON Doc.PayeeID=Employee.EmployeeID\r\n\t\t\t\t\t\t\t                                INNER JOIN System_Document SD ON SD.SysDocID=Doc.SysDocID\r\n                                                            WHERE SysDocType IN (64) ";
						break;
					case SysDocTypes.CashReceiptMultiple:
						text = "SELECT " + text6 + " , ISNULL(IsVoid,'False') AS V,Doc.SysDocID ,VoucherID,TransactionDate [Date],\r\n\t\t\t\t\t\t\t                                CASE SysDocType WHEN 4 THEN 'Cash' WHEN 5 THEN 'Cheque' ELSE 'TT' END AS [Type],CostCenterID [C.C.],PayeeID [Payee Code],\r\n\t\t\t\t\t\t\t                                (CASE PayeeType\r\n\t\t\t\t\t\t\t\t\t\t\t\t\t                                WHEN 'C' THEN Customer.CustomerName\r\n\t\t\t\t\t\t\t\t\t\t\t\t\t                                WHEN 'V' THEN Vendor.VendorName\r\n\t\t\t\t\t\t\t\t\t\t\t\t\t                                WHEN 'E' THEN Employee.FirstName\r\n\t\t\t\t\t\t\t\t\t\t\t\t\t                                ELSE Account.AccountName END) AS [Payee Name],Reference [Ref],\r\n\t\t\t\t\t\t\t                                RegisterID [Reg],Doc.Description [Desc],\r\n\t\t\t\t\t\t\t                                ISNULL(Doc.CurrencyID,(SELECT CurrencyID FROM Currency WHERE IsBase=1)) AS [Cur] ,ISNULL(AmountFC,Amount) AS [Amount],Doc.CreatedBy,Doc.DateCreated, Doc.UpdatedBy,Doc.DateUpdated\r\n\t\t\t\t\t\t\t                                FROM GL_Transaction Doc\r\n\t\t\t\t\t\t\t                                LEFt OUTER JOIN Account ON Doc.PayeeID=Account.AccountID LEFt OUTER JOIN\r\n\t\t\t\t\t\t\t\t\t\t\t\t\t                                Customer ON Doc.PayeeID=Customer.CustomerID LEFt OUTER JOIN\r\n\t\t\t\t\t\t\t\t\t\t\t\t\t                                Vendor ON Doc.PayeeID=Vendor.VendorID LEFt OUTER JOIN\r\n\t\t\t\t\t\t\t\t\t\t\t\t\t                                Employee ON Doc.PayeeID=Employee.EmployeeID\r\n\t\t\t\t\t\t\t                                INNER JOIN System_Document SD ON SD.SysDocID=Doc.SysDocID\r\n                                                            WHERE SysDocType IN (66) ";
						break;
					case SysDocTypes.ChequeReceipt:
						text = "SELECT " + text6 + " , ISNULL(IsVoid,'False') AS V,Doc.SysDocID ,VoucherID ,TransactionDate [Date],\r\n\t\t\t\t\t\t\t                                CASE SysDocType WHEN 4 THEN 'Cash' WHEN 5 THEN 'Cheque' ELSE 'TT' END AS [Type],CostCenterID [C.C.],PayeeID [Payee Code],\r\n\t\t\t\t\t\t\t                                (CASE PayeeType\r\n\t\t\t\t\t\t\t\t\t\t\t\t\t                                WHEN 'C' THEN Customer.CustomerName\r\n\t\t\t\t\t\t\t\t\t\t\t\t\t                                WHEN 'V' THEN Vendor.VendorName\r\n\t\t\t\t\t\t\t\t\t\t\t\t\t                                WHEN 'E' THEN Employee.FirstName\r\n\t\t\t\t\t\t\t\t\t\t\t\t\t                                ELSE Account.AccountName END) AS [Payee Name],Reference [Ref],\r\n\t\t\t\t\t\t\t                                RegisterID [Reg],Doc.Description [Desc],\r\n\t\t\t\t\t\t\t                                ISNULL(Doc.CurrencyID,(SELECT CurrencyID FROM Currency WHERE IsBase=1)) AS [Cur] ,ISNULL(AmountFC,Amount) AS [Amount],Doc.CreatedBy,Doc.DateCreated, Doc.UpdatedBy,Doc.DateUpdated\r\n\t\t\t\t\t\t\t                                FROM GL_Transaction Doc\r\n\t\t\t\t\t\t\t                                LEFt OUTER JOIN Account ON Doc.PayeeID=Account.AccountID LEFt OUTER JOIN\r\n\t\t\t\t\t\t\t\t\t\t\t\t\t                                Customer ON Doc.PayeeID=Customer.CustomerID LEFt OUTER JOIN\r\n\t\t\t\t\t\t\t\t\t\t\t\t\t                                Vendor ON Doc.PayeeID=Vendor.VendorID LEFt OUTER JOIN\r\n\t\t\t\t\t\t\t\t\t\t\t\t\t                                Employee ON Doc.PayeeID=Employee.EmployeeID\r\n\t\t\t\t\t\t\t                                INNER JOIN System_Document SD ON SD.SysDocID=Doc.SysDocID\r\n                                                            WHERE SysDocType IN (2) ";
						break;
					case SysDocTypes.FundTransfer:
						text = "SELECT " + text6 + " , ISNULL(IsVoid,'False') AS V,Doc.SysDocID ,VoucherID ,TransactionDate [Date],\r\n\t\t\t\t\t\t\t                                CASE SysDocType WHEN 4 THEN 'Cash' WHEN 5 THEN 'Cheque' ELSE 'TT' END AS [Type],CostCenterID [C.C.],PayeeID [Payee Code],\r\n\t\t\t\t\t\t\t                                (CASE PayeeType\r\n\t\t\t\t\t\t\t\t\t\t\t\t\t                                WHEN 'C' THEN Customer.CustomerName\r\n\t\t\t\t\t\t\t\t\t\t\t\t\t                                WHEN 'V' THEN Vendor.VendorName\r\n\t\t\t\t\t\t\t\t\t\t\t\t\t                                WHEN 'E' THEN Employee.FirstName\r\n\t\t\t\t\t\t\t\t\t\t\t\t\t                                ELSE Account.AccountName END) AS [Payee Name],Reference [Ref],\r\n\t\t\t\t\t\t\t                                RegisterID [Reg],Doc.Description [Desc],\r\n\t\t\t\t\t\t\t                                ISNULL(Doc.CurrencyID,(SELECT CurrencyID FROM Currency WHERE IsBase=1)) AS [Cur] ,ISNULL(AmountFC,Amount) AS [Amount],Doc.CreatedBy,Doc.DateCreated, Doc.UpdatedBy,Doc.DateUpdated\r\n\t\t\t\t\t\t\t                                FROM GL_Transaction Doc\r\n\t\t\t\t\t\t\t                                LEFt OUTER JOIN Account ON Doc.PayeeID=Account.AccountID LEFt OUTER JOIN\r\n\t\t\t\t\t\t\t\t\t\t\t\t\t                                Customer ON Doc.PayeeID=Customer.CustomerID LEFt OUTER JOIN\r\n\t\t\t\t\t\t\t\t\t\t\t\t\t                                Vendor ON Doc.PayeeID=Vendor.VendorID LEFt OUTER JOIN\r\n\t\t\t\t\t\t\t\t\t\t\t\t\t                                Employee ON Doc.PayeeID=Employee.EmployeeID\r\n\t\t\t\t\t\t\t                                INNER JOIN System_Document SD ON SD.SysDocID=Doc.SysDocID\r\n                                                            WHERE SysDocType IN (6) ";
						break;
					case SysDocTypes.TTReceipt:
						text = "SELECT " + text6 + " , ISNULL(IsVoid,'False') AS V,Doc.SysDocID ,VoucherID ,TransactionDate [Date],\r\n\t\t\t\t\t\t\t                                CASE SysDocType WHEN 4 THEN 'Cash' WHEN 5 THEN 'Cheque' ELSE 'TT' END AS [Type],CostCenterID [C.C.],PayeeID [Payee Code],\r\n\t\t\t\t\t\t\t                                (CASE PayeeType\r\n\t\t\t\t\t\t\t\t\t\t\t\t\t                                WHEN 'C' THEN Customer.CustomerName\r\n\t\t\t\t\t\t\t\t\t\t\t\t\t                                WHEN 'V' THEN Vendor.VendorName\r\n\t\t\t\t\t\t\t\t\t\t\t\t\t                                WHEN 'E' THEN Employee.FirstName\r\n\t\t\t\t\t\t\t\t\t\t\t\t\t                                ELSE Account.AccountName END) AS [Payee Name],Reference [Ref],\r\n\t\t\t\t\t\t\t                                RegisterID [Reg],Doc.Description [Desc],\r\n\t\t\t\t\t\t\t                                ISNULL(Doc.CurrencyID,(SELECT CurrencyID FROM Currency WHERE IsBase=1)) AS [Cur] ,ISNULL(AmountFC,Amount) AS [Amount],Doc.CreatedBy,Doc.DateCreated, Doc.UpdatedBy,Doc.DateUpdated\r\n\t\t\t\t\t\t\t                                FROM GL_Transaction Doc\r\n\t\t\t\t\t\t\t                                LEFt OUTER JOIN Account ON Doc.PayeeID=Account.AccountID LEFt OUTER JOIN\r\n\t\t\t\t\t\t\t\t\t\t\t\t\t                                Customer ON Doc.PayeeID=Customer.CustomerID LEFt OUTER JOIN\r\n\t\t\t\t\t\t\t\t\t\t\t\t\t                                Vendor ON Doc.PayeeID=Vendor.VendorID LEFt OUTER JOIN\r\n\t\t\t\t\t\t\t\t\t\t\t\t\t                                Employee ON Doc.PayeeID=Employee.EmployeeID\r\n\t\t\t\t\t\t\t                                INNER JOIN System_Document SD ON SD.SysDocID=Doc.SysDocID\r\n                                                            WHERE SysDocType IN (65) ";
						break;
					case SysDocTypes.ChequeDeposit:
						text = "SELECT " + text6 + " , ISNULL(IsVoid,'False') AS V,Doc.SysDocID ,VoucherID ,TransactionDate [Date],\r\n\t\t\t\t\t\t\t                                CASE SysDocType WHEN 4 THEN 'Cash' WHEN 5 THEN 'Cheque' ELSE 'TT' END AS [Type],CostCenterID [C.C.],PayeeID [Payee Code],\r\n\t\t\t\t\t\t\t                                (CASE PayeeType\r\n\t\t\t\t\t\t\t\t\t\t\t\t\t                                WHEN 'C' THEN Customer.CustomerName\r\n\t\t\t\t\t\t\t\t\t\t\t\t\t                                WHEN 'V' THEN Vendor.VendorName\r\n\t\t\t\t\t\t\t\t\t\t\t\t\t                                WHEN 'E' THEN Employee.FirstName\r\n\t\t\t\t\t\t\t\t\t\t\t\t\t                                ELSE Account.AccountName END) AS [Payee Name],Reference [Ref],\r\n\t\t\t\t\t\t\t                                RegisterID [Reg],Doc.Description [Desc],\r\n\t\t\t\t\t\t\t                                ISNULL(Doc.CurrencyID,(SELECT CurrencyID FROM Currency WHERE IsBase=1)) AS [Cur] ,ISNULL(AmountFC,Amount) AS [Amount],Doc.CreatedBy,Doc.DateCreated, Doc.UpdatedBy,Doc.DateUpdated\r\n\t\t\t\t\t\t\t                                FROM GL_Transaction Doc\r\n\t\t\t\t\t\t\t                                LEFt OUTER JOIN Account ON Doc.PayeeID=Account.AccountID LEFt OUTER JOIN\r\n\t\t\t\t\t\t\t\t\t\t\t\t\t                                Customer ON Doc.PayeeID=Customer.CustomerID LEFt OUTER JOIN\r\n\t\t\t\t\t\t\t\t\t\t\t\t\t                                Vendor ON Doc.PayeeID=Vendor.VendorID LEFt OUTER JOIN\r\n\t\t\t\t\t\t\t\t\t\t\t\t\t                                Employee ON Doc.PayeeID=Employee.EmployeeID\r\n\t\t\t\t\t\t\t                                INNER JOIN System_Document SD ON SD.SysDocID=Doc.SysDocID\r\n                                                            WHERE SysDocType IN (7) ";
						break;
					case SysDocTypes.ReturnedCheque:
						text = "SELECT " + text6 + " , ISNULL(IsVoid,'False') AS V,Doc.SysDocID ,VoucherID ,TransactionDate [Date],\r\n\t\t\t\t\t\t\t                                CASE SysDocType WHEN 4 THEN 'Cash' WHEN 5 THEN 'Cheque' ELSE 'TT' END AS [Type],CostCenterID [C.C.],PayeeID [Payee Code],\r\n\t\t\t\t\t\t\t                                (CASE PayeeType\r\n\t\t\t\t\t\t\t\t\t\t\t\t\t                                WHEN 'C' THEN Customer.CustomerName\r\n\t\t\t\t\t\t\t\t\t\t\t\t\t                                WHEN 'V' THEN Vendor.VendorName\r\n\t\t\t\t\t\t\t\t\t\t\t\t\t                                WHEN 'E' THEN Employee.FirstName\r\n\t\t\t\t\t\t\t\t\t\t\t\t\t                                ELSE Account.AccountName END) AS [Payee Name],Reference [Ref],\r\n\t\t\t\t\t\t\t                                RegisterID [Reg],Doc.Description [Desc],\r\n\t\t\t\t\t\t\t                                ISNULL(Doc.CurrencyID,(SELECT CurrencyID FROM Currency WHERE IsBase=1)) AS [Cur] ,ISNULL(AmountFC,Amount) AS [Amount],Doc.CreatedBy,Doc.DateCreated, Doc.UpdatedBy,Doc.DateUpdated\r\n\t\t\t\t\t\t\t                                FROM GL_Transaction Doc\r\n\t\t\t\t\t\t\t                                LEFt OUTER JOIN Account ON Doc.PayeeID=Account.AccountID LEFt OUTER JOIN\r\n\t\t\t\t\t\t\t\t\t\t\t\t\t                                Customer ON Doc.PayeeID=Customer.CustomerID LEFt OUTER JOIN\r\n\t\t\t\t\t\t\t\t\t\t\t\t\t                                Vendor ON Doc.PayeeID=Vendor.VendorID LEFt OUTER JOIN\r\n\t\t\t\t\t\t\t\t\t\t\t\t\t                                Employee ON Doc.PayeeID=Employee.EmployeeID\r\n\t\t\t\t\t\t\t                                INNER JOIN System_Document SD ON SD.SysDocID=Doc.SysDocID\r\n                                                            WHERE SysDocType IN (12) ";
						break;
					case SysDocTypes.ReceivedChequeCancellation:
						text = "SELECT " + text6 + " , ISNULL(IsVoid,'False') AS V,Doc.SysDocID ,VoucherID ,TransactionDate [Date],\r\n\t\t\t\t\t\t\t                                CASE SysDocType WHEN 4 THEN 'Cash' WHEN 5 THEN 'Cheque' ELSE 'TT' END AS [Type],CostCenterID [C.C.],PayeeID [Payee Code],\r\n\t\t\t\t\t\t\t                                (CASE PayeeType\r\n\t\t\t\t\t\t\t\t\t\t\t\t\t                                WHEN 'C' THEN Customer.CustomerName\r\n\t\t\t\t\t\t\t\t\t\t\t\t\t                                WHEN 'V' THEN Vendor.VendorName\r\n\t\t\t\t\t\t\t\t\t\t\t\t\t                                WHEN 'E' THEN Employee.FirstName\r\n\t\t\t\t\t\t\t\t\t\t\t\t\t                                ELSE Account.AccountName END) AS [Payee Name],Reference [Ref],\r\n\t\t\t\t\t\t\t                                RegisterID [Reg],Doc.Description [Desc],\r\n\t\t\t\t\t\t\t                                ISNULL(Doc.CurrencyID,(SELECT CurrencyID FROM Currency WHERE IsBase=1)) AS [Cur] ,ISNULL(AmountFC,Amount) AS [Amount],Doc.CreatedBy,Doc.DateCreated, Doc.UpdatedBy,Doc.DateUpdated\r\n\t\t\t\t\t\t\t                                FROM GL_Transaction Doc\r\n\t\t\t\t\t\t\t                                LEFt OUTER JOIN Account ON Doc.PayeeID=Account.AccountID LEFt OUTER JOIN\r\n\t\t\t\t\t\t\t\t\t\t\t\t\t                                Customer ON Doc.PayeeID=Customer.CustomerID LEFt OUTER JOIN\r\n\t\t\t\t\t\t\t\t\t\t\t\t\t                                Vendor ON Doc.PayeeID=Vendor.VendorID LEFt OUTER JOIN\r\n\t\t\t\t\t\t\t\t\t\t\t\t\t                                Employee ON Doc.PayeeID=Employee.EmployeeID\r\n\t\t\t\t\t\t\t                                INNER JOIN System_Document SD ON SD.SysDocID=Doc.SysDocID\r\n                                                            WHERE SysDocType IN (13) ";
						break;
					case SysDocTypes.IssuedChequeCancellation:
						text = "SELECT " + text6 + " , ISNULL(IsVoid,'False') AS V,Doc.SysDocID ,VoucherID ,TransactionDate [Date],\r\n\t\t\t\t\t\t\t                                CASE SysDocType WHEN 4 THEN 'Cash' WHEN 5 THEN 'Cheque' ELSE 'TT' END AS [Type],CostCenterID [C.C.],PayeeID [Payee Code],\r\n\t\t\t\t\t\t\t                                (CASE PayeeType\r\n\t\t\t\t\t\t\t\t\t\t\t\t\t                                WHEN 'C' THEN Customer.CustomerName\r\n\t\t\t\t\t\t\t\t\t\t\t\t\t                                WHEN 'V' THEN Vendor.VendorName\r\n\t\t\t\t\t\t\t\t\t\t\t\t\t                                WHEN 'E' THEN Employee.FirstName\r\n\t\t\t\t\t\t\t\t\t\t\t\t\t                                ELSE Account.AccountName END) AS [Payee Name],Reference [Ref],\r\n\t\t\t\t\t\t\t                                RegisterID [Reg],Doc.Description [Desc],\r\n\t\t\t\t\t\t\t                                ISNULL(Doc.CurrencyID,(SELECT CurrencyID FROM Currency WHERE IsBase=1)) AS [Cur] ,ISNULL(AmountFC,Amount) AS [Amount],Doc.CreatedBy,Doc.DateCreated, Doc.UpdatedBy,Doc.DateUpdated\r\n\t\t\t\t\t\t\t                                FROM GL_Transaction Doc\r\n\t\t\t\t\t\t\t                                LEFt OUTER JOIN Account ON Doc.PayeeID=Account.AccountID LEFt OUTER JOIN\r\n\t\t\t\t\t\t\t\t\t\t\t\t\t                                Customer ON Doc.PayeeID=Customer.CustomerID LEFt OUTER JOIN\r\n\t\t\t\t\t\t\t\t\t\t\t\t\t                                Vendor ON Doc.PayeeID=Vendor.VendorID LEFt OUTER JOIN\r\n\t\t\t\t\t\t\t\t\t\t\t\t\t                                Employee ON Doc.PayeeID=Employee.EmployeeID\r\n\t\t\t\t\t\t\t                                INNER JOIN System_Document SD ON SD.SysDocID=Doc.SysDocID\r\n                                                            WHERE SysDocType IN (15) ";
						break;
					case SysDocTypes.IssuedChequeClearance:
						text = "SELECT " + text6 + " , ISNULL(IsVoid,'False') AS V,Doc.SysDocID ,VoucherID ,TransactionDate [Date],\r\n\t\t\t\t\t\t\t                                CASE SysDocType WHEN 4 THEN 'Cash' WHEN 5 THEN 'Cheque' ELSE 'TT' END AS [Type],CostCenterID [C.C.],PayeeID [Payee Code],\r\n\t\t\t\t\t\t\t                                (CASE PayeeType\r\n\t\t\t\t\t\t\t\t\t\t\t\t\t                                WHEN 'C' THEN Customer.CustomerName\r\n\t\t\t\t\t\t\t\t\t\t\t\t\t                                WHEN 'V' THEN Vendor.VendorName\r\n\t\t\t\t\t\t\t\t\t\t\t\t\t                                WHEN 'E' THEN Employee.FirstName\r\n\t\t\t\t\t\t\t\t\t\t\t\t\t                                ELSE Account.AccountName END) AS [Payee Name],Reference [Ref],\r\n\t\t\t\t\t\t\t                                RegisterID [Reg],Doc.Description [Desc],\r\n\t\t\t\t\t\t\t                                ISNULL(Doc.CurrencyID,(SELECT CurrencyID FROM Currency WHERE IsBase=1)) AS [Cur] ,ISNULL(AmountFC,Amount) AS [Amount],Doc.CreatedBy,Doc.DateCreated, Doc.UpdatedBy,Doc.DateUpdated\r\n\t\t\t\t\t\t\t                                FROM GL_Transaction Doc\r\n\t\t\t\t\t\t\t                                LEFt OUTER JOIN Account ON Doc.PayeeID=Account.AccountID LEFt OUTER JOIN\r\n\t\t\t\t\t\t\t\t\t\t\t\t\t                                Customer ON Doc.PayeeID=Customer.CustomerID LEFt OUTER JOIN\r\n\t\t\t\t\t\t\t\t\t\t\t\t\t                                Vendor ON Doc.PayeeID=Vendor.VendorID LEFt OUTER JOIN\r\n\t\t\t\t\t\t\t\t\t\t\t\t\t                                Employee ON Doc.PayeeID=Employee.EmployeeID\r\n\t\t\t\t\t\t\t                                INNER JOIN System_Document SD ON SD.SysDocID=Doc.SysDocID\r\n                                                            WHERE SysDocType IN (14) ";
						break;
					case SysDocTypes.IssuedChequeReturn:
						text = "SELECT " + text6 + " , ISNULL(IsVoid,'False') AS V,Doc.SysDocID ,VoucherID ,TransactionDate [Date],\r\n\t\t\t\t\t\t\t                                CASE SysDocType WHEN 4 THEN 'Cash' WHEN 5 THEN 'Cheque' ELSE 'TT' END AS [Type],CostCenterID [C.C.],PayeeID [Payee Code],\r\n\t\t\t\t\t\t\t                                (CASE PayeeType\r\n\t\t\t\t\t\t\t\t\t\t\t\t\t                                WHEN 'C' THEN Customer.CustomerName\r\n\t\t\t\t\t\t\t\t\t\t\t\t\t                                WHEN 'V' THEN Vendor.VendorName\r\n\t\t\t\t\t\t\t\t\t\t\t\t\t                                WHEN 'E' THEN Employee.FirstName\r\n\t\t\t\t\t\t\t\t\t\t\t\t\t                                ELSE Account.AccountName END) AS [Payee Name],Reference [Ref],\r\n\t\t\t\t\t\t\t                                RegisterID [Reg],Doc.Description [Desc],\r\n\t\t\t\t\t\t\t                                ISNULL(Doc.CurrencyID,(SELECT CurrencyID FROM Currency WHERE IsBase=1)) AS [Cur] ,ISNULL(AmountFC,Amount) AS [Amount],Doc.CreatedBy,Doc.DateCreated, Doc.UpdatedBy,Doc.DateUpdated\r\n\t\t\t\t\t\t\t                                FROM GL_Transaction Doc\r\n\t\t\t\t\t\t\t                                LEFt OUTER JOIN Account ON Doc.PayeeID=Account.AccountID LEFt OUTER JOIN\r\n\t\t\t\t\t\t\t\t\t\t\t\t\t                                Customer ON Doc.PayeeID=Customer.CustomerID LEFt OUTER JOIN\r\n\t\t\t\t\t\t\t\t\t\t\t\t\t                                Vendor ON Doc.PayeeID=Vendor.VendorID LEFt OUTER JOIN\r\n\t\t\t\t\t\t\t\t\t\t\t\t\t                                Employee ON Doc.PayeeID=Employee.EmployeeID\r\n\t\t\t\t\t\t\t                                INNER JOIN System_Document SD ON SD.SysDocID=Doc.SysDocID\r\n                                                            WHERE SysDocType IN (16) ";
						break;
					case SysDocTypes.SalarySheet:
						text = "SELECT " + text6 + ", Doc.SysDocID,Doc.VoucherID,SheetName,TransactionDate,Month,Year,StartDate,EndDate,Note FROM SalarySheet Doc                                 \r\n                                    WHERE ISNULL(IsClosed,'False') = 'False' ";
						break;
					case SysDocTypes.OverTimeEntry:
						text = "SELECT DISTINCT " + text6 + ", SysDocID ,Doc.VoucherID, Doc.Month,Doc.Year\r\n                            FROM OverTimeEntry Doc    WHERE 1 = 1 ";
						break;
					case SysDocTypes.SalesInvoice:
						text = "SELECT " + text6 + " , SysDocID,VoucherID,Doc.CustomerID [Customer Code],CustomerName [Customer Name],TransactionDate [Date],Discount,(Total-Discount)AS Total,Doc.CreatedBy,Doc.DateCreated, Doc.UpdatedBy,Doc.DateUpdated FROM Sales_Invoice Doc INNER JOIN Customer C ON C.CustomerID = Doc.CustomerID AND ISNULL(IsExport,'False') = 'False'  WHERE 1=1   AND  ISNULL(IsVoid,'False') = 'False' ";
						break;
					case SysDocTypes.SalesReceipt:
						text = "SELECT " + text6 + " , SysDocID,VoucherID,Doc.CustomerID [Customer Code],CustomerName [Customer Name],TransactionDate [Date],Discount,(Total-Discount)AS Total,Doc.CreatedBy,Doc.DateCreated, Doc.UpdatedBy,Doc.DateUpdated FROM Sales_Invoice Doc INNER JOIN Customer C ON C.CustomerID = Doc.CustomerID AND ISNULL(IsExport,'False') = 'False'  WHERE 1=1   AND  ISNULL(IsVoid,'False') = 'False' ";
						break;
					case SysDocTypes.ExportSalesInvoice:
						text = "SELECT " + text6 + " , SysDocID,VoucherID,Doc.CustomerID [Customer Code],CustomerName [Customer Name],TransactionDate [Date],Discount,(Total-Discount)AS Total,Doc.CreatedBy,Doc.DateCreated, Doc.UpdatedBy,Doc.DateUpdated FROM Sales_Invoice Doc INNER JOIN Customer C ON C.CustomerID = Doc.CustomerID AND ISNULL(IsExport,'False') = 'True'  WHERE 1=1 ";
						break;
					default:
						switch (sysDocTypes)
						{
						case SysDocTypes.ExportSalesInvoice:
							text = "SELECT " + text6 + " , SysDocID,VoucherID,Doc.VendorID [Vendor Code],VendorName [Vendor Name],ContainerNumber,Doc.CreatedBy,Doc.DateCreated, Doc.UpdatedBy,Doc.DateUpdated  FROM Arrival_Report Doc INNER JOIN Vendor V ON V.VendorID = Doc.VendorID   WHERE 1=1 ";
							break;
						case SysDocTypes.ConsignIn:
							text = "SELECT " + text6 + " ,  SysDocID,VoucherID,Doc.VendorID [Vendor Code],VendorName [Vendor Name],TransactionDate [Date],Doc.CreatedBy,Doc.DateCreated, Doc.UpdatedBy,Doc.DateUpdated FROM Consign_In Doc INNER JOIN Vendor V ON V.VendorID = Doc.VendorID   WHERE 1=1 ";
							break;
						case SysDocTypes.ConsignInSettlement:
							text = "SELECT " + text6 + " ,  SysDocID,VoucherID,Doc.VendorID [Vendor Code],VendorName [Vendor Name],TransactionDate [Date],Doc.CreatedBy,Doc.DateCreated, Doc.UpdatedBy,Doc.DateUpdated FROM ConsignIn_Settlement Doc INNER JOIN Vendor V ON V.VendorID = Doc.VendorID   WHERE 1=1 ";
							break;
						default:
							switch (sysDocTypes)
							{
							case SysDocTypes.ConsignInSettlement:
								text = "SELECT " + text6 + " ,  SysDocID,VoucherID,Doc.VendorID [Vendor Code],VendorName [Vendor Name],TransactionDate [Date],Doc.CreatedBy,Doc.DateCreated, Doc.UpdatedBy,Doc.DateUpdated FROM ConsignIn_Settlement Doc INNER JOIN Vendor V ON V.VendorID = Doc.VendorID   WHERE 1=1 ";
								break;
							case SysDocTypes.ConsignOut:
								text = "SELECT " + text6 + " , SELECT  SysDocID,VoucherID,Doc.CustomerID [Customer Code],CustomerName [Customer Name],TransactionDate [Date],Doc.CreatedBy,Doc.DateCreated, Doc.UpdatedBy,Doc.DateUpdated FROM Consign_Out Doc INNER JOIN Customer C ON C.CustomerID = Doc.CustomerID  WHERE 1=1 ";
								break;
							case SysDocTypes.ConsignOutSettlement:
								text = "SELECT " + text6 + " , SELECT  SysDocID,VoucherID,Doc.CustomerID [Customer Code],CustomerName [Customer Name],TransactionDate [Date],Doc.CreatedBy,Doc.DateCreated, Doc.UpdatedBy,Doc.DateUpdated FROM ConsignOut_Settlement Doc INNER JOIN Customer C ON C.CustomerID = Doc.CustomerID  WHERE 1=1 ";
								break;
							case SysDocTypes.CashSalesReturn:
								text = "SELECT " + text6 + " , SysDocID,VoucherID,Doc.CustomerID [Customer Code],CustomerName [Customer Name],TransactionDate [Date],Discount,(Total-Discount)AS Total,Doc.CreatedBy,Doc.DateCreated, Doc.UpdatedBy,Doc.DateUpdated FROM Sales_Return Doc INNER JOIN Customer C ON C.CustomerID = Doc.CustomerID  WHERE 1=1 ";
								break;
							case SysDocTypes.CreditSalesReturn:
								text = "SELECT " + text6 + " , SysDocID,VoucherID,Doc.CustomerID [Customer Code],CustomerName [Customer Name],TransactionDate [Date],Discount,(Total-Discount)AS Total,Doc.CreatedBy,Doc.DateCreated, Doc.UpdatedBy,Doc.DateUpdated FROM Sales_Return Doc INNER JOIN Customer C ON C.CustomerID = Doc.CustomerID  WHERE 1=1 ";
								break;
							case SysDocTypes.FixedAssetTransfer:
								text = "SELECT " + text6 + " , SysDocID,VoucherID,Doc.AssetID [Customer Code],AssetName [Asset Name],TransactionDate [Date],Doc.CreatedBy,Doc.DateCreated, Doc.UpdatedBy,Doc.DateUpdated FROM FixedAsset_Transfer Doc INNER JOIN FixedAsset C ON C.AssetID = Doc.AssetID  WHERE 1=1 ";
								break;
							case SysDocTypes.CreditNote:
								text = "SELECT " + text6 + " , ISNULL(IsVoid,'False') AS V,Doc.SysDocID ,VoucherID ,TransactionDate [Date],\r\n\t\t\t\t\t\t\t                                CASE SysDocType WHEN 4 THEN 'Cash' WHEN 5 THEN 'Cheque' ELSE 'TT' END AS [Type],CostCenterID [C.C.],PayeeID [Payee Code],\r\n\t\t\t\t\t\t\t                                (CASE PayeeType\r\n\t\t\t\t\t\t\t\t\t\t\t\t\t                                WHEN 'C' THEN Customer.CustomerName\r\n\t\t\t\t\t\t\t\t\t\t\t\t\t                                WHEN 'V' THEN Vendor.VendorName\r\n\t\t\t\t\t\t\t\t\t\t\t\t\t                                WHEN 'E' THEN Employee.FirstName\r\n\t\t\t\t\t\t\t\t\t\t\t\t\t                                ELSE Account.AccountName END) AS [Payee Name],Reference [Ref],\r\n\t\t\t\t\t\t\t                                RegisterID [Reg],Doc.Description [Desc],\r\n\t\t\t\t\t\t\t                                ISNULL(Doc.CurrencyID,(SELECT CurrencyID FROM Currency WHERE IsBase=1)) AS [Cur] ,ISNULL(AmountFC,Amount) AS [Amount],Doc.CreatedBy,Doc.DateCreated, Doc.UpdatedBy,Doc.DateUpdated\r\n\t\t\t\t\t\t\t                                FROM GL_Transaction Doc\r\n\t\t\t\t\t\t\t                                LEFt OUTER JOIN Account ON Doc.PayeeID=Account.AccountID LEFt OUTER JOIN\r\n\t\t\t\t\t\t\t\t\t\t\t\t\t                                Customer ON Doc.PayeeID=Customer.CustomerID LEFt OUTER JOIN\r\n\t\t\t\t\t\t\t\t\t\t\t\t\t                                Vendor ON Doc.PayeeID=Vendor.VendorID LEFt OUTER JOIN\r\n\t\t\t\t\t\t\t\t\t\t\t\t\t                                Employee ON Doc.PayeeID=Employee.EmployeeID\r\n\t\t\t\t\t\t\t                                INNER JOIN System_Document SD ON SD.SysDocID=Doc.SysDocID\r\n                                                            WHERE SysDocType IN (11) ";
								break;
							case SysDocTypes.DebitNote:
								text = "SELECT " + text6 + " , ISNULL(IsVoid,'False') AS V,Doc.SysDocID ,VoucherID ,TransactionDate [Date],\r\n\t\t\t\t\t\t\t                                CASE SysDocType WHEN 4 THEN 'Cash' WHEN 5 THEN 'Cheque' ELSE 'TT' END AS [Type],CostCenterID [C.C.],PayeeID [Payee Code],\r\n\t\t\t\t\t\t\t                                (CASE PayeeType\r\n\t\t\t\t\t\t\t\t\t\t\t\t\t                                WHEN 'C' THEN Customer.CustomerName\r\n\t\t\t\t\t\t\t\t\t\t\t\t\t                                WHEN 'V' THEN Vendor.VendorName\r\n\t\t\t\t\t\t\t\t\t\t\t\t\t                                WHEN 'E' THEN Employee.FirstName\r\n\t\t\t\t\t\t\t\t\t\t\t\t\t                                ELSE Account.AccountName END) AS [Payee Name],Reference [Ref],\r\n\t\t\t\t\t\t\t                                RegisterID [Reg],Doc.Description [Desc],\r\n\t\t\t\t\t\t\t                                ISNULL(Doc.CurrencyID,(SELECT CurrencyID FROM Currency WHERE IsBase=1)) AS [Cur] ,ISNULL(AmountFC,Amount) AS [Amount],Doc.CreatedBy,Doc.DateCreated, Doc.UpdatedBy,Doc.DateUpdated\r\n\t\t\t\t\t\t\t                                FROM GL_Transaction Doc\r\n\t\t\t\t\t\t\t                                LEFt OUTER JOIN Account ON Doc.PayeeID=Account.AccountID LEFt OUTER JOIN\r\n\t\t\t\t\t\t\t\t\t\t\t\t\t                                Customer ON Doc.PayeeID=Customer.CustomerID LEFt OUTER JOIN\r\n\t\t\t\t\t\t\t\t\t\t\t\t\t                                Vendor ON Doc.PayeeID=Vendor.VendorID LEFt OUTER JOIN\r\n\t\t\t\t\t\t\t\t\t\t\t\t\t                                Employee ON Doc.PayeeID=Employee.EmployeeID\r\n\t\t\t\t\t\t\t                                INNER JOIN System_Document SD ON SD.SysDocID=Doc.SysDocID\r\n                                                            WHERE SysDocType IN (10) ";
								break;
							case SysDocTypes.GJournal:
								text = "SELECT " + text6 + " , ISNULL(IsVoid,'False') AS V,Doc.SysDocID ,VoucherID , JournalDate [Date],Reference [Ref],\r\n\t\t\t\t\t\t\t                                ISNULL(Doc.CurrencyID,(SELECT CurrencyID FROM Currency WHERE IsBase=1)) AS [Cur] ,\r\n\t\t\t\t\t\t\t\t\t\t\t\t\t(SELECT SUM(Debit) FROM Journal_Details JD WHERE JD.SysDocID = Doc.SysDocID AND JD.VoucherID = Doc.VoucherID) AS Amount,Doc.CreatedBy,Doc.DateCreated, Doc.UpdatedBy,Doc.DateUpdated\r\n\t\t\t\t\t\t\t                                FROM Journal Doc  WHERE SysDocType IN (1) ";
								break;
							case SysDocTypes.ProjectSubContractPO:
								text = "SELECT " + text6 + " , SysDocID,VoucherID,Doc.VendorID [Vendor],VendorName [Vendor Name],TransactionDate [Date],Discount,(Total-Discount)AS Total,Doc.CreatedBy,Doc.DateCreated, Doc.UpdatedBy,Doc.DateUpdated FROM Project_Subcontract_PO Doc INNER JOIN Vendor V ON V.VendorID = Doc.VendorID  WHERE 1=1 ";
								break;
							case SysDocTypes.ProjectSubContractPI:
								text = "SELECT " + text6 + " , SysDocID,VoucherID,Doc.VendorID [Vendor],VendorName [Vendor Name],TransactionDate [Date],Discount,(Total-Discount)AS Total,Doc.CreatedBy,Doc.DateCreated, Doc.UpdatedBy,Doc.DateUpdated FROM Project_SubContract_PI Doc INNER JOIN Vendor V ON V.VendorID = Doc.VendorID  WHERE 1=1 ";
								break;
							case SysDocTypes.ServiceCallTrack:
								text = "SELECT " + text6 + " , SysDocID,VoucherID,Doc.CustomerID [Customer Code],CustomerName [Customer Name],TransactionDate [Date],Doc.CreatedBy,Doc.DateCreated, Doc.UpdatedBy,Doc.DateUpdated FROM Service_CallTrack Doc INNER JOIN Customer C ON C.CustomerID = Doc.CustomerID   WHERE 1=1  ";
								break;
							case SysDocTypes.JobEstimation:
								text = "SELECT " + text6 + " , SysDocID,VoucherID,J.JobName,J.CustomerID,C.CustomerName,TransactionDate [Date],Doc.CreatedBy,Doc.DateCreated, Doc.UpdatedBy,Doc.DateUpdated FROM Job_Estimation Doc INNER JOIN Job J ON J.JobID=Doc.JobID INNER JOIN Customer C ON C.CustomerID=J.CustomerID   WHERE 1=1  ";
								break;
							case SysDocTypes.JobTimesheet:
								text = "SELECT " + text6 + " , SysDocID,VoucherID,ISNULL(FirstName,'')+ ' ' + ISNULL(MiddleName,'') + ' ' + ISNULL(LastName,'') [EmpName],TransactionDate [Date],Doc.CreatedBy,Doc.DateCreated, Doc.UpdatedBy,Doc.DateUpdated FROM Job_Timesheet Doc INNER JOIN Employee E ON Doc.EmployeeID=E.EmployeeID   WHERE 1=1  ";
								break;
							case SysDocTypes.JobExpenseIssue:
								text = "SELECT " + text6 + " , SysDocID,VoucherID,TransactionDate [Date],Doc.CreatedBy,Doc.DateCreated, Doc.UpdatedBy,Doc.DateUpdated FROM Job_Expense_Issue Doc  WHERE 1=1  ";
								break;
							case SysDocTypes.JobInventoryIssue:
								text = "SELECT " + text6 + " , SysDocID,VoucherID,TransactionDate [Date],Doc.CreatedBy,Doc.DateCreated, Doc.UpdatedBy,Doc.DateUpdated FROM Job_Inventory_Issue Doc  WHERE 1=1  ";
								break;
							case SysDocTypes.JobInventoryReturn:
								text = "SELECT " + text6 + " , SysDocID,VoucherID,TransactionDate [Date],Doc.CreatedBy,Doc.DateCreated, Doc.UpdatedBy,Doc.DateUpdated FROM Job_Inventory_Return Doc  WHERE 1=1  ";
								break;
							case SysDocTypes.JobInvoice:
								text = "SELECT " + text6 + " ,SysDocID,VoucherID,TransactionDate [Date],PT.TermName,C.CustomerName,S.FullName,J.JobName,ISNULL(ISNULL(Doc.TaxAmountFC,Doc.TaxAmount) ,0) AS Tax,( SELECT SUM(JFD.Amount) FROM Job_Fee_Detail JFD WHERE JFD.JobID=Doc.JobID ) AS [Fee Value],\r\n                                (SELECT SUM(Ji1.Total) FROM Job_Invoice Ji1 WHERE Ji1.JobID=Doc.JobID AND Ji1.TransactionDate <= Doc.TransactionDate ) AS [Total Invoice],                         \r\n                                 Doc.CreatedBy,Doc.DateCreated, Doc.UpdatedBy,Doc.DateUpdated\r\n                                FROM Job_Invoice Doc LEFT JOIN Customer C ON Doc.CustomerID=C.CustomerID\r\n                                LEFT JOIN Salesperson S ON Doc.SalespersonID=S.SalespersonID    \r\n                                LEFT JOIN Job J ON J.JobID=Doc.JobID\r\n                                LEFT JOIN Payment_Term PT ON PT.PaymentTermID=Doc.TermID\r\n                                LEFT OUTER JOIN Customer_Address CA ON CA.CustomerID = C.CustomerID\r\n                                WHERE 1 = 1 ";
								break;
							case SysDocTypes.JobClosing:
								text = "SELECT " + text6 + " , SysDocID,VoucherID,TransactionDate [Date],Doc.CreatedBy,Doc.DateCreated, Doc.UpdatedBy,Doc.DateUpdated FROM Job Doc  WHERE 1=1  ";
								break;
							case SysDocTypes.JobMaintenanceServiceEntry:
								text = "SELECT " + text6 + " , SysDocID,VoucherID,Doc.JobID [Job ID],J.JobName [Job Name],Doc.LocationID,L.LocationName [Location Name],TransactionDate [Date],\r\n                                Doc.CreatedBy,Doc.DateCreated, Doc.UpdatedBy,Doc.DateUpdated FROM \r\n                                Job_Maintenance_Service Doc  INNER JOIN Location L ON L.LocationID = Doc.LocationID INNER JOIN Job J ON Doc.JobID =J.JobID  WHERE 1=1 ";
								break;
							case SysDocTypes.SalaryPaymentCash:
							case SysDocTypes.SalaryPaymentCheque:
							case SysDocTypes.SalaryPaymentBank:
								text = "SELECT " + text6 + " ,Doc.SysDocID,Doc.VoucherID,Doc.TransactionDate [Date],A.AccountName,(SELECT BankName FROM Chequebook C LEFT JOIN Bank B ON C.BankID=B.BankID WHERE C.ChequebookID=Doc.ChequebookID) AS 'PayeeBank',Doc.CreatedBy,Doc.DateCreated, Doc.UpdatedBy,Doc.DateUpdated FROM Payroll_Transaction Doc LEFT JOIN Account A ON Doc.BankAccountID=A.AccountID  WHERE 1=1 ";
								break;
							case SysDocTypes.ProjectExpenseAllocation:
								text = "SELECT " + text6 + " , SysDocID,VoucherID,Month,Year,TransactionDate [Date],Doc.CreatedBy,Doc.DateCreated, Doc.UpdatedBy,Doc.DateUpdated FROM Project_Expense_Allocation Doc  WHERE 1=1  ";
								break;
							case SysDocTypes.LPOReceipt:
								text = "SELECT " + text6 + " , SysDocID,VoucherID,TransactionDate [Date],Doc.CreatedBy,Doc.DateCreated, Doc.UpdatedBy,Doc.DateUpdated FROM LPO_Receipt Doc  WHERE 1=1  ";
								break;
							case SysDocTypes.TR:
								text = "SELECT " + text6 + " , SysDocID,VoucherID,TransactionDate [Date],Doc.CreatedBy,Doc.DateCreated, Doc.UpdatedBy,Doc.DateUpdated FROM Bank_Facility_Transaction Doc  WHERE 1=1  ";
								break;
							case SysDocTypes.TRPayment:
								text = "SELECT " + text6 + " , SysDocID,VoucherID,TransactionDate [Date],Doc.CreatedBy,Doc.DateCreated, Doc.UpdatedBy,Doc.DateUpdated FROM Bank_Facility_Payment Doc  WHERE 1=1  ";
								break;
							case SysDocTypes.TRApplication:
								text = "SELECT " + text6 + " , SysDocID,VoucherID,TransactionDate [Date],Doc.CreatedBy,Doc.DateCreated, Doc.UpdatedBy,Doc.DateUpdated FROM TR_Application Doc  WHERE 1=1  ";
								break;
							case SysDocTypes.SendChequesToBank:
								text = "SELECT " + text6 + " , SysDocID,VoucherID,TransactionDate [Date],Doc.CreatedBy,Doc.DateCreated, Doc.UpdatedBy,Doc.DateUpdated FROM Cheque_Send Doc  WHERE 1=1  ";
								break;
							case SysDocTypes.ChequeDiscount:
								text = "SELECT " + text6 + " , SysDocID,VoucherID,TransactionDate [Date],Doc.CreatedBy,Doc.DateCreated, Doc.UpdatedBy,Doc.DateUpdated FROM Cheque_Discount Doc  WHERE 1=1  ";
								break;
							case SysDocTypes.IssuedSecurityCheque:
								text = "SELECT " + text6 + " , SysDocID,VoucherID,IssueDate [Date] FROM Security_Cheque Doc  WHERE 1=1  ";
								break;
							default:
								throw new Exception("Transaction Type:" + sysDocTypes.ToString() + " is not implemented in GetUserPendingApprovalTasks() function.");
							}
							break;
						}
						break;
					}
					break;
				case 2:
				{
					text6 = " CAST(" + text4 + " AS int) AS ObjectID, CAST(2 as int) AS ObjectType, (SELECT TOP 1 TaskID FROM Approval_Task AT WHERE ISNULL(IsExpired,'False') = 'False' AND Status = 1 AND ApprovalType = " + (int)approvalType + " AND ApprovalID = '" + approvalID + "' AND AT.DocumentCode = Card.Code  AND ((AT.AssigneeType  IN (1,3)  AND AssigneeID = '" + userID + "') OR (AssigneeType =2 AND AssigneeID \r\n                                    IN (SELECT GroupID FROM User_Group_Detail UG WHERE UG.UserID = '" + userID + "')))) AS TaskID";
					DataComboType dataComboType = (DataComboType)int.Parse(text4);
					switch (dataComboType)
					{
					case DataComboType.Customer:
						text = "SELECT " + text6 + ", Card.* FROM  ( SELECT CustomerID Code,CustomerName Name,CountryID Country,CreatedBy,DateCreated, UpdatedBy,DateUpdated FROM Customer ) AS Card   WHERE 1=1 ";
						break;
					case DataComboType.Vendor:
						text = "SELECT " + text6 + ", Card.* FROM  ( SELECT  VendorID Code,VendorName Name,CountryID Country,CreatedBy,DateCreated, UpdatedBy,DateUpdated FROM Vendor ) AS Card    WHERE 1=1 ";
						break;
					case DataComboType.Employee:
						text = "SELECT " + text6 + ", Card.* FROM  ( SELECT EmployeeID Code,FirstName + ' ' + LastName AS Name,PositionID Position,CreatedBy,DateCreated, UpdatedBy,DateUpdated FROM Employee ) AS Card    WHERE 1=1 ";
						break;
					case DataComboType.Product:
						text = "SELECT " + text6 + ", Card.* FROM  ( SELECT ProductID Code,Description AS Name,ClassID AS Class, CategoryID AS Category,CreatedBy,DateCreated, UpdatedBy,DateUpdated FROM Product ) AS Card    WHERE 1=1  ";
						break;
					case DataComboType.Accounts:
						text = "SELECT " + text6 + ", Card.* FROM  ( SELECT AccountID Code,AccountName AS Name,GroupID AS [Account Group],CreatedBy,DateCreated, UpdatedBy,DateUpdated    FROM Account ) AS Card     WHERE 1=1 ";
						break;
					case DataComboType.Job:
						text = "SELECT " + text6 + ", Card.* FROM  ( SELECT JobID Code,JobName AS Name,CreatedBy,DateCreated, UpdatedBy,DateUpdated   FROM Job  ) AS Card    WHERE 1=1 ";
						break;
					case DataComboType.JobBOM:
						text = "SELECT " + text6 + ", Card.* FROM  ( SELECT JobBOMID Code,BOMName AS BOMName,CreatedBy,DateCreated, UpdatedBy,DateUpdated   FROM Job_BOM ) AS Card     WHERE 1=1 ";
						break;
					default:
						throw new Exception("Card Type:" + dataComboType.ToString() + " is not implemented in GetUserPendingApprovalTasks() function.");
					}
					break;
				}
				}
				if (a != "")
				{
					text2 = "  AND AT.DocumentSysDocID = Doc.SysDocID ";
				}
				text = text + "  AND EXISTS(\r\n                                 SELECT * FROM Approval_Task AT WHERE ISNULL(IsExpired,'False') = 'False' AND ApprovalType = " + (int)approvalType + " AND ApprovalID = '" + approvalID + "' AND ((AT.AssigneeType  IN (1,3)  AND AssigneeID = '" + userID + "') OR (AssigneeType =2 AND AssigneeID \r\n                                 IN (SELECT GroupID FROM User_Group_Detail UG WHERE UG.UserID = '" + userID + "')))  AND AT.Status IN (1,2)  AND ";
				text = ((b == 1 && a2 != "") ? (text + " (AT.DocumentCode = Doc." + text3 + text2 + "))") : ((b != 1 || !(a2 == "")) ? (text + " (AT.DocumentCode = Card.Code))") : (text + " (AT.DocumentCode = Doc." + text3 + "))")));
				if ((sysDocTypes == SysDocTypes.SalesInvoice || sysDocTypes == SysDocTypes.SalesReceipt || sysDocTypes == SysDocTypes.PurchaseOrder) && text5 != "")
				{
					text = text + "AND " + text5;
				}
				FillDataSet(dataSet, "Approval", text);
				return dataSet;
			}
			catch
			{
				throw;
			}
		}
	}
}
