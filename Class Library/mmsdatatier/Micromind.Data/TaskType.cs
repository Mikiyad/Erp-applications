using Micromind.Common;
using Micromind.Common.Data;
using Micromind.Common.Libraries;
using System;
using System.Data;
using System.Data.SqlClient;

namespace Micromind.Data
{
	public sealed class TaskType : StoreObject
	{
		private const string TASKTYPEID_PARM = "@TaskTypeID";

		private const string TASKTYPENAME_PARM = "@Name";

		private const string ISINACTIVE_PARM = "@IsInactive";

		private const string NOTE_PARM = "@Note";

		private const string TASKTYPE_TABLE = "Task_Type";

		private const string TASKTYPEDETAIL_TABLE = "Task_Type_Detail";

		private const string TASKSTEPID_PARM = "@TaskStepID";

		private static string DEFAULTASSIGNEEID_PARM = "@DefaultAssigneeID";

		private static string DESCRIPTION_PARM = "@Description";

		private static string DAYSALLOWED_PARM = "@DaysAllowed";

		private static string PREREQUEST_PARM = "@PreRequest";

		private static string DOCTYPEID_PARM = "@DocTypeID";

		private static string DOCTYPENAME_PARM = "@DocTypeName";

		private static string ROWINDEX_PARM = "@RowIndex";

		private const string CREATEDBY_PARM = "CreatedBy";

		private const string DATECREATED_PARM = "DateCreated";

		private const string UPDATEDBY_PARM = "UpdatedBy";

		private const string DATEUPDATED_PARM = "DateUpdated";

		public TaskType(Config config)
			: base(config)
		{
		}

		private string GetInsertUpdateText(bool isUpdate)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddInsertUpdateParameters("Task_Type", new FieldValue("TaskTypeID", "@TaskTypeID", isUpdateConditionField: true), new FieldValue("Name", "@Name"), new FieldValue("IsInactive", "@IsInactive"), new FieldValue("Note", "@Note"));
			if (isUpdate)
			{
				sqlBuilder.AddInsertUpdateParameters("Task_Type", new FieldValue("DateUpdated", "DateUpdated", isUpdateConditionField: true, checkForNullValue: true));
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
				updateCommand = new SqlCommand(GetInsertUpdateText(isUpdate: true), base.DBConfig.Connection);
				updateCommand.CommandType = CommandType.Text;
				parameters = updateCommand.Parameters;
			}
			else
			{
				insertCommand = new SqlCommand(GetInsertUpdateText(isUpdate: false), base.DBConfig.Connection);
				insertCommand.CommandType = CommandType.Text;
				parameters = insertCommand.Parameters;
			}
			parameters.Add("@TaskTypeID", SqlDbType.NVarChar);
			parameters.Add("@Name", SqlDbType.NVarChar);
			parameters.Add("@IsInactive", SqlDbType.Bit);
			parameters.Add("@Note", SqlDbType.NVarChar);
			parameters["@TaskTypeID"].SourceColumn = "TaskTypeID";
			parameters["@Name"].SourceColumn = "Name";
			parameters["@IsInactive"].SourceColumn = "IsInactive";
			parameters["@Note"].SourceColumn = "Note";
			if (isUpdate)
			{
				parameters.Add("DateUpdated", SqlDbType.DateTime);
				parameters["DateUpdated"].SourceColumn = "DateUpdated";
			}
			if (isUpdate)
			{
				return updateCommand;
			}
			return insertCommand;
		}

		private string GetInsertUpdateDetailsText(bool isUpdate)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddInsertUpdateParameters("Task_Type_Detail", new FieldValue("TaskTypeID", "@TaskTypeID"), new FieldValue("TaskStepID", "@TaskStepID"), new FieldValue(TaskTypeData.DEFAULTASSIGNEEID_FIELD, DEFAULTASSIGNEEID_PARM), new FieldValue(TaskTypeData.DAYSALLOWED_FIELD, DAYSALLOWED_PARM), new FieldValue(TaskTypeData.PREREQUEST_FIELD, PREREQUEST_PARM), new FieldValue(TaskTypeData.DOCTYPEID_FIELD, DOCTYPEID_PARM), new FieldValue(TaskTypeData.DOCTYPENAME_FIELD, DOCTYPENAME_PARM), new FieldValue("RowIndex", ROWINDEX_PARM), new FieldValue(TaskTypeData.DESCRIPTION_FIELD, DESCRIPTION_PARM));
			if (isUpdate)
			{
				sqlBuilder.AddInsertUpdateParameters("Task_Type_Detail", new FieldValue("DateUpdated", "DateUpdated", isUpdateConditionField: true, checkForNullValue: true));
			}
			if (isUpdate)
			{
				return sqlBuilder.GetUpdateExpression();
			}
			return sqlBuilder.GetInsertExpression();
		}

		private SqlCommand GetInsertUpdateDetailsCommand(bool isUpdate)
		{
			SqlParameterCollection parameters;
			if (isUpdate)
			{
				updateCommand = new SqlCommand(GetInsertUpdateDetailsText(isUpdate: true), base.DBConfig.Connection);
				updateCommand.CommandType = CommandType.Text;
				parameters = updateCommand.Parameters;
			}
			else
			{
				insertCommand = new SqlCommand(GetInsertUpdateDetailsText(isUpdate: false), base.DBConfig.Connection);
				insertCommand.CommandType = CommandType.Text;
				parameters = insertCommand.Parameters;
			}
			parameters.Add("@TaskTypeID", SqlDbType.NVarChar);
			parameters.Add("@TaskStepID", SqlDbType.NVarChar);
			parameters.Add(DEFAULTASSIGNEEID_PARM, SqlDbType.NVarChar);
			parameters.Add(DAYSALLOWED_PARM, SqlDbType.Int);
			parameters.Add(PREREQUEST_PARM, SqlDbType.NVarChar);
			parameters.Add(DOCTYPEID_PARM, SqlDbType.NVarChar);
			parameters.Add(DOCTYPENAME_PARM, SqlDbType.NVarChar);
			parameters.Add(DESCRIPTION_PARM, SqlDbType.NVarChar);
			parameters.Add(ROWINDEX_PARM, SqlDbType.Int);
			parameters["@TaskTypeID"].SourceColumn = "TaskTypeID";
			parameters["@TaskStepID"].SourceColumn = "TaskStepID";
			parameters[DEFAULTASSIGNEEID_PARM].SourceColumn = TaskTypeData.DEFAULTASSIGNEEID_FIELD;
			parameters[DESCRIPTION_PARM].SourceColumn = TaskTypeData.DESCRIPTION_FIELD;
			parameters[DAYSALLOWED_PARM].SourceColumn = TaskTypeData.DAYSALLOWED_FIELD;
			parameters[PREREQUEST_PARM].SourceColumn = TaskTypeData.PREREQUEST_FIELD;
			parameters[DOCTYPEID_PARM].SourceColumn = TaskTypeData.DOCTYPEID_FIELD;
			parameters[DOCTYPENAME_PARM].SourceColumn = TaskTypeData.DOCTYPENAME_FIELD;
			parameters[ROWINDEX_PARM].SourceColumn = "RowIndex";
			if (isUpdate)
			{
				parameters.Add("DateUpdated", SqlDbType.DateTime);
				parameters["DateUpdated"].SourceColumn = "DateUpdated";
			}
			if (isUpdate)
			{
				return updateCommand;
			}
			return insertCommand;
		}

		public bool InsertTaskType(TaskTypeData accountTaskTypeData)
		{
			bool flag = true;
			SqlCommand insertUpdateCommand = GetInsertUpdateCommand(isUpdate: false);
			SqlTransaction sqlTransaction = null;
			try
			{
				sqlTransaction = (insertUpdateCommand.Transaction = base.DBConfig.StartNewTransaction());
				flag = Insert(accountTaskTypeData, "Task_Type", insertUpdateCommand);
				insertUpdateCommand = GetInsertUpdateDetailsCommand(isUpdate: false);
				insertUpdateCommand.Transaction = sqlTransaction;
				flag &= Insert(accountTaskTypeData, "Task_Type_Detail", insertUpdateCommand);
				string text = accountTaskTypeData.TaskTypeTable.Rows[0]["TaskTypeID"].ToString();
				AddActivityLog("Task Type", text, ActivityTypes.Add, sqlTransaction);
				UpdateTableRowInsertUpdateInfo("Task_Type", "TaskTypeID", text, sqlTransaction, isInsert: true);
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

		public bool UpdateTaskType(TaskTypeData accountTaskTypeData)
		{
			bool flag = true;
			SqlCommand insertUpdateCommand = GetInsertUpdateCommand(isUpdate: true);
			SqlTransaction sqlTransaction = null;
			try
			{
				sqlTransaction = (insertUpdateCommand.Transaction = base.DBConfig.StartNewTransaction());
				flag = Update(accountTaskTypeData, "Task_Type", insertUpdateCommand);
				if (!flag)
				{
					return flag;
				}
				object obj = accountTaskTypeData.TaskTypeTable.Rows[0]["TaskTypeID"];
				insertUpdateCommand = GetInsertUpdateDetailsCommand(isUpdate: false);
				insertUpdateCommand.Transaction = sqlTransaction;
				string commandText = "DELETE FROM Task_Type_Detail WHERE TaskTypeID = '" + obj + "'";
				flag &= Delete(commandText, sqlTransaction);
				flag &= Insert(accountTaskTypeData, "Task_Type_Detail", insertUpdateCommand);
				UpdateTableRowByID("Task_Type", "TaskTypeID", "DateUpdated", obj, DateTime.Now, sqlTransaction);
				string entiyID = accountTaskTypeData.TaskTypeTable.Rows[0]["Name"].ToString();
				AddActivityLog("Task Type", entiyID, ActivityTypes.Update, sqlTransaction);
				UpdateTableRowInsertUpdateInfo("Task_Type", "TaskTypeID", obj, sqlTransaction, isInsert: false);
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

		public TaskTypeData GetTaskType()
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddTable("Task_Type");
			TaskTypeData taskTypeData = new TaskTypeData();
			sqlBuilder.UseDistinct = false;
			FillDataSet(taskTypeData, "Task_Type", sqlBuilder);
			return taskTypeData;
		}

		public bool DeleteTaskType(string TaskTypeID)
		{
			bool flag = true;
			try
			{
				string commandText = "DELETE FROM Task_Type WHERE TaskTypeID = '" + TaskTypeID + "'";
				flag = Delete(commandText, null);
				if (!flag)
				{
					return flag;
				}
				AddActivityLog("Task Type", TaskTypeID, ActivityTypes.Delete, null);
				return flag;
			}
			catch
			{
				throw;
			}
		}

		public TaskTypeData GetTaskTypeByID(string id)
		{
			TaskTypeData taskTypeData = new TaskTypeData();
			string textCommand = "SELECT * from Task_Type  WHERE TaskTypeID = '" + id + "'";
			FillDataSet(taskTypeData, "Task_Type", textCommand);
			textCommand = "SELECT * from Task_Type_Detail  WHERE TaskTypeID = '" + id + "'";
			FillDataSet(taskTypeData, "Task_Type_Detail", textCommand);
			return taskTypeData;
		}

		public DataSet GetTaskTypeByFields(params string[] columns)
		{
			return GetTaskTypeByFields(null, isInactive: true, columns);
		}

		public DataSet GetTaskTypeByFields(string[] TaskTypeID, params string[] columns)
		{
			return GetTaskTypeByFields(TaskTypeID, isInactive: true, columns);
		}

		public DataSet GetTaskTypeByFields(string[] ids, bool isInactive, params string[] columns)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddTable("Task_Type");
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
				commandHelper.FieldName = "TaskTypeID";
				commandHelper.FieldValue = ids;
				commandHelper.SqlFieldType = SqlDbType.NVarChar;
				commandHelper.TableName = "Task_Type";
				sqlBuilder.AddCommandHelper(commandHelper);
			}
			DataSet dataSet = new DataSet();
			dataSet.EnforceConstraints = false;
			sqlBuilder.UseDistinct = false;
			FillDataSet(dataSet, "Task_Type", sqlBuilder);
			return dataSet;
		}

		public DataSet GetTaskTypeList()
		{
			DataSet dataSet = new DataSet();
			string textCommand = "SELECT TaskTypeID [Code],Name [Name],Note,IsInactive [Inactive]\r\n                           FROM Task_Type ";
			FillDataSet(dataSet, "Task_Type", textCommand);
			return dataSet;
		}

		public DataSet GetTaskTypeComboList()
		{
			DataSet dataSet = new DataSet();
			string textCommand = "SELECT TaskTypeID [Code],Name [Name]\r\n                           FROM Task_Type ORDER BY TaskTypeID,Name";
			FillDataSet(dataSet, "Task_Type", textCommand);
			return dataSet;
		}
	}
}
