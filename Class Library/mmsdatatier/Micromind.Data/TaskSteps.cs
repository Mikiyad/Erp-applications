using Micromind.Common;
using Micromind.Common.Data;
using Micromind.Common.Libraries;
using System;
using System.Data;
using System.Data.SqlClient;

namespace Micromind.Data
{
	public sealed class TaskSteps : StoreObject
	{
		private const string TASKSTEPID_PARM = "@TaskStepsID";

		private const string TASKSTEPNAME_PARM = "@Name";

		private const string TASKTYPEID_PARM = "@TaskTypeID";

		public const string ISINACTIVE_PARM = "@IsInactive";

		public const string NOTE_PARM = "@Note";

		public const string TASKSTEPS_TABLE = "Task_Steps";

		private const string CREATEDBY_PARM = "@CreatedBy";

		private const string DATECREATED_PARM = "@DateCreated";

		private const string UPDATEDBY_PARM = "@UpdatedBy";

		private const string DATEUPDATED_PARM = "@DateUpdated";

		public TaskSteps(Config config)
			: base(config)
		{
		}

		private string GetInsertUpdateText(bool isUpdate)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddInsertUpdateParameters("Task_Steps", new FieldValue("TaskStepID", "@TaskStepsID", isUpdateConditionField: true), new FieldValue("TaskTypeID", "@TaskTypeID"), new FieldValue("Name", "@Name"), new FieldValue("IsInactive", "@IsInactive"), new FieldValue("Note", "@Note"));
			if (isUpdate)
			{
				sqlBuilder.AddInsertUpdateParameters("Task_Steps", new FieldValue("DateUpdated", "@DateUpdated", isUpdateConditionField: true, checkForNullValue: true));
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
			parameters.Add("@TaskStepsID", SqlDbType.NVarChar);
			parameters.Add("@Name", SqlDbType.NVarChar);
			parameters.Add("@TaskTypeID", SqlDbType.NVarChar);
			parameters.Add("@IsInactive", SqlDbType.Bit);
			parameters.Add("@Note", SqlDbType.NVarChar);
			parameters["@TaskStepsID"].SourceColumn = "TaskStepID";
			parameters["@Name"].SourceColumn = "Name";
			parameters["@TaskTypeID"].SourceColumn = "TaskTypeID";
			parameters["@IsInactive"].SourceColumn = "IsInactive";
			parameters["@Note"].SourceColumn = "Note";
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

		public bool InsertTaskSteps(TaskStepsData accountTaskStepsData)
		{
			bool result = true;
			SqlCommand insertUpdateCommand = GetInsertUpdateCommand(isUpdate: false);
			SqlTransaction sqlTransaction = null;
			try
			{
				sqlTransaction = (insertUpdateCommand.Transaction = base.DBConfig.StartNewTransaction());
				result = Insert(accountTaskStepsData, "Task_Steps", insertUpdateCommand);
				string text = accountTaskStepsData.TaskStepsTable.Rows[0]["TaskStepID"].ToString();
				AddActivityLog("Task Steps", text, ActivityTypes.Add, sqlTransaction);
				UpdateTableRowInsertUpdateInfo("Task_Steps", "TaskStepID", text, sqlTransaction, isInsert: true);
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

		public bool UpdateTaskSteps(TaskStepsData accountTaskStepsData)
		{
			bool flag = true;
			SqlCommand insertUpdateCommand = GetInsertUpdateCommand(isUpdate: true);
			SqlTransaction sqlTransaction = null;
			try
			{
				sqlTransaction = (insertUpdateCommand.Transaction = base.DBConfig.StartNewTransaction());
				flag = Update(accountTaskStepsData, "Task_Steps", insertUpdateCommand);
				if (!flag)
				{
					return flag;
				}
				object obj = accountTaskStepsData.TaskStepsTable.Rows[0]["TaskStepID"];
				UpdateTableRowByID("Task_Steps", "TaskStepID", "DateUpdated", obj, DateTime.Now, sqlTransaction);
				string entiyID = accountTaskStepsData.TaskStepsTable.Rows[0]["Name"].ToString();
				AddActivityLog("Task Steps", entiyID, ActivityTypes.Update, sqlTransaction);
				UpdateTableRowInsertUpdateInfo("Task_Steps", "TaskStepID", obj, sqlTransaction, isInsert: false);
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

		public TaskStepsData GetTaskSteps()
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddTable("Task_Steps");
			TaskStepsData taskStepsData = new TaskStepsData();
			sqlBuilder.UseDistinct = false;
			FillDataSet(taskStepsData, "Task_Steps", sqlBuilder);
			return taskStepsData;
		}

		public bool DeleteTaskSteps(string TaskStepsID)
		{
			bool flag = true;
			try
			{
				string commandText = "DELETE FROM Task_Steps WHERE TaskStepID = '" + TaskStepsID + "'";
				flag = Delete(commandText, null);
				if (!flag)
				{
					return flag;
				}
				AddActivityLog("Task Steps", TaskStepsID, ActivityTypes.Delete, null);
				return flag;
			}
			catch
			{
				throw;
			}
		}

		public TaskStepsData GetTaskStepsByID(string id)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			CommandHelper commandHelper = null;
			commandHelper = new CommandHelper();
			commandHelper.FieldName = "TaskStepID";
			commandHelper.SqlFieldType = SqlDbType.NVarChar;
			commandHelper.FieldValue = id;
			commandHelper.TableName = "Task_Steps";
			sqlBuilder.AddCommandHelper(commandHelper);
			sqlBuilder.IsComparing = true;
			TaskStepsData taskStepsData = new TaskStepsData();
			sqlBuilder.UseDistinct = false;
			FillDataSet(taskStepsData, "Task_Steps", sqlBuilder);
			return taskStepsData;
		}

		public DataSet GetTaskStepsByFields(params string[] columns)
		{
			return GetTaskStepsByFields(null, isInactive: true, columns);
		}

		public DataSet GetTaskStepsByFields(string[] TaskStepsID, params string[] columns)
		{
			return GetTaskStepsByFields(TaskStepsID, isInactive: true, columns);
		}

		public DataSet GetTaskStepsByFields(string[] ids, bool isInactive, params string[] columns)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddTable("Task_Steps");
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
				commandHelper.FieldName = "TaskStepID";
				commandHelper.FieldValue = ids;
				commandHelper.SqlFieldType = SqlDbType.NVarChar;
				commandHelper.TableName = "Task_Steps";
				sqlBuilder.AddCommandHelper(commandHelper);
			}
			DataSet dataSet = new DataSet();
			dataSet.EnforceConstraints = false;
			sqlBuilder.UseDistinct = false;
			FillDataSet(dataSet, "Task_Steps", sqlBuilder);
			return dataSet;
		}

		public DataSet GetTaskStepsList()
		{
			DataSet dataSet = new DataSet();
			string textCommand = "SELECT TaskStepID [Code],Name [Name],Note,IsInactive [Inactive]\r\n                           FROM Task_Steps ";
			FillDataSet(dataSet, "Task_Steps", textCommand);
			return dataSet;
		}

		public DataSet GetTaskStepsComboList()
		{
			DataSet dataSet = new DataSet();
			string textCommand = "SELECT TaskStepID [Code],Name [Name]\r\n                           FROM Task_Steps ORDER BY TaskStepID,Name";
			FillDataSet(dataSet, "Task_Steps", textCommand);
			return dataSet;
		}
	}
}
