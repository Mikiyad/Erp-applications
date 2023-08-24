using Micromind.Common;
using Micromind.Common.Data;
using Micromind.Common.Libraries;
using System;
using System.Data;
using System.Data.SqlClient;

namespace Micromind.Data
{
	public sealed class JobTaskGroup : StoreObject
	{
		private const string TASKGROUPID_PARM = "@TaskGroupID";

		private const string TASKGROUPNAME_PARM = "@TaskGroupName";

		private const string TASKGROUPDESC_PARM = "@Desc";

		private const string INACTIVE_PARM = "@Inactive";

		private const string TASKGROUP_TABLE = "Job_Task_Group";

		private const string CREATEDBY_PARM = "@CreatedBy";

		private const string DATECREATED_PARM = "@DateCreated";

		private const string UPDATEDBY_PARM = "@UpdatedBy";

		private const string DATEUPDATED_PARM = "@DateUpdated";

		public JobTaskGroup(Config config)
			: base(config)
		{
		}

		private string GetInsertUpdateText(bool isUpdate)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddInsertUpdateParameters("Job_Task_Group", new FieldValue("TaskGroupID", "@TaskGroupID", isUpdateConditionField: true), new FieldValue("TaskGroupName", "@TaskGroupName"), new FieldValue("Description", "@Desc"), new FieldValue("Inactive", "@Inactive"));
			if (isUpdate)
			{
				sqlBuilder.AddInsertUpdateParameters("Job_Task_Group", new FieldValue("DateUpdated", "@DateUpdated", isUpdateConditionField: true, checkForNullValue: true));
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
			parameters.Add("@TaskGroupID", SqlDbType.NVarChar);
			parameters.Add("@TaskGroupName", SqlDbType.NVarChar);
			parameters.Add("@Desc", SqlDbType.NVarChar);
			parameters.Add("@Inactive", SqlDbType.Bit);
			parameters["@TaskGroupID"].SourceColumn = "TaskGroupID";
			parameters["@TaskGroupName"].SourceColumn = "TaskGroupName";
			parameters["@Desc"].SourceColumn = "Description";
			parameters["@Inactive"].SourceColumn = "Inactive";
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

		public bool InsertJobTaskGroup(JobTaskGroupData taskGroupData)
		{
			bool result = true;
			SqlCommand insertUpdateCommand = GetInsertUpdateCommand(isUpdate: false);
			SqlTransaction sqlTransaction = null;
			try
			{
				sqlTransaction = (insertUpdateCommand.Transaction = base.DBConfig.StartNewTransaction());
				result = Insert(taskGroupData, "Job_Task_Group", insertUpdateCommand);
				string text = taskGroupData.JobTaskGroupTable.Rows[0]["TaskGroupID"].ToString();
				AddActivityLog("Job Task Group", text, ActivityTypes.Add, sqlTransaction);
				UpdateTableRowInsertUpdateInfo("Job_Task_Group", "TaskGroupID", text, sqlTransaction, isInsert: true);
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

		public bool UpdateJobTaskGroup(JobTaskGroupData taskGroupData)
		{
			bool flag = true;
			SqlCommand insertUpdateCommand = GetInsertUpdateCommand(isUpdate: true);
			SqlTransaction sqlTransaction = null;
			try
			{
				sqlTransaction = (insertUpdateCommand.Transaction = base.DBConfig.StartNewTransaction());
				flag = Update(taskGroupData, "Job_Task_Group", insertUpdateCommand);
				if (!flag)
				{
					return flag;
				}
				object obj = taskGroupData.JobTaskGroupTable.Rows[0]["TaskGroupID"];
				UpdateTableRowByID("Job_Task_Group", "TaskGroupID", "DateUpdated", obj, DateTime.Now, sqlTransaction);
				string entiyID = taskGroupData.JobTaskGroupTable.Rows[0]["TaskGroupID"].ToString();
				AddActivityLog("Job Task Group", entiyID, ActivityTypes.Update, sqlTransaction);
				UpdateTableRowInsertUpdateInfo("Job_Task_Group", "TaskGroupID", obj, sqlTransaction, isInsert: false);
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

		public JobTaskGroupData GetJobTaskGroup()
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddTable("Job_Task_Group");
			JobTaskGroupData jobTaskGroupData = new JobTaskGroupData();
			sqlBuilder.UseDistinct = false;
			FillDataSet(jobTaskGroupData, "Job_Task_Group", sqlBuilder);
			return jobTaskGroupData;
		}

		public bool DeleteJobTaskGroup(string jobTaskGroupID)
		{
			bool flag = true;
			try
			{
				string commandText = "DELETE FROM Job_Task_Group WHERE TaskGroupID = '" + jobTaskGroupID + "'";
				flag = Delete(commandText, null);
				if (!flag)
				{
					return flag;
				}
				AddActivityLog("Job Task Group", jobTaskGroupID, ActivityTypes.Delete, null);
				return flag;
			}
			catch
			{
				throw;
			}
		}

		public JobTaskGroupData GetJobTaskGroupByID(string id)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			CommandHelper commandHelper = null;
			commandHelper = new CommandHelper();
			commandHelper.FieldName = "TaskGroupID";
			commandHelper.SqlFieldType = SqlDbType.NVarChar;
			commandHelper.FieldValue = id;
			commandHelper.TableName = "Job_Task_Group";
			sqlBuilder.AddCommandHelper(commandHelper);
			sqlBuilder.IsComparing = true;
			JobTaskGroupData jobTaskGroupData = new JobTaskGroupData();
			sqlBuilder.UseDistinct = false;
			FillDataSet(jobTaskGroupData, "Job_Task_Group", sqlBuilder);
			return jobTaskGroupData;
		}

		public DataSet GetJobTaskGroupByFields(params string[] columns)
		{
			return GetJobTaskGroupByFields(null, isInactive: true, columns);
		}

		public DataSet GetJobTaskGroupByFields(string[] jobTypeID, params string[] columns)
		{
			return GetJobTaskGroupByFields(jobTypeID, isInactive: true, columns);
		}

		public DataSet GetJobTaskGroupByFields(string[] ids, bool isInactive, params string[] columns)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddTable("Job_Task_Group");
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
				commandHelper.FieldName = "JobTypeID";
				commandHelper.FieldValue = ids;
				commandHelper.SqlFieldType = SqlDbType.NVarChar;
				commandHelper.TableName = "Job_Task_Group";
				sqlBuilder.AddCommandHelper(commandHelper);
			}
			DataSet dataSet = new DataSet();
			dataSet.EnforceConstraints = false;
			sqlBuilder.UseDistinct = false;
			FillDataSet(dataSet, "Job_Task_Group", sqlBuilder);
			return dataSet;
		}

		public DataSet GetJobTaskGroupList()
		{
			DataSet dataSet = new DataSet();
			string textCommand = "SELECT TaskGroupID [Code],TaskGroupName [Name],Inactive\r\n                           FROM Job_Task_Group ";
			FillDataSet(dataSet, "Job_Task_Group", textCommand);
			return dataSet;
		}

		public DataSet GetJobTaskGroupComboList()
		{
			DataSet dataSet = new DataSet();
			string textCommand = "SELECT TaskGroupID [Code], TaskGroupName [Name]\r\n                           FROM Job_Task_Group ORDER BY TaskGroupID,TaskGroupName";
			FillDataSet(dataSet, "Job_Task_Group", textCommand);
			return dataSet;
		}
	}
}
