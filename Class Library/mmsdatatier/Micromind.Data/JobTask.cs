using Micromind.Common;
using Micromind.Common.Data;
using Micromind.Common.Libraries;
using System;
using System.Data;
using System.Data.SqlClient;

namespace Micromind.Data
{
	public sealed class JobTask : StoreObject
	{
		private const string TASKID_PARM = "@TaskID";

		private const string DESCRIPTION_PARM = "@Description";

		private const string FEEID_PARM = "@FeeID";

		private const string JOBID_PARM = "@JobID";

		private const string TASKGROUPID_PARM = "@TaskGroupID";

		private const string STARTDATE_PARM = "@StartDate";

		private const string ENDDATE_PARM = "@EndDate";

		private const string ACTUALSTARTDATE_PARM = "@ActualStartDate";

		private const string ACTUALENDDATE_PARM = "@ActualEndDate";

		private const string ASSIGNEDTOID_PARM = "@AssignedToID";

		private const string FEEPERCENTAGE_PARM = "@FeePercentage";

		private const string COMPLETEDPERCENTAGE_PARM = "@CompletedPercentage";

		private const string TOTALHOURS_PARM = "@TotalHours";

		private const string STATUS_PARM = "@Status";

		private const string COMPLETEDDESCRIPTION_PARM = "@CompletedDescription";

		public const string NOTE_PARM = "@Note";

		public const string JOBTASK_TABLE = "JobTask";

		private const string CREATEDBY_PARM = "@CreatedBy";

		private const string DATECREATED_PARM = "@DateCreated";

		private const string UPDATEDBY_PARM = "@UpdatedBy";

		private const string DATEUPDATED_PARM = "@DateUpdated";

		public JobTask(Config config)
			: base(config)
		{
		}

		private string GetInsertUpdateText(bool isUpdate)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddInsertUpdateParameters("Job_Task", new FieldValue("TaskID", "@TaskID", isUpdateConditionField: true), new FieldValue("Description", "@Description"), new FieldValue("FeeID", "@FeeID"), new FieldValue("JobID", "@JobID"), new FieldValue("TaskGroupID", "@TaskGroupID"), new FieldValue("StartDate", "@StartDate"), new FieldValue("EndDate", "@EndDate"), new FieldValue("ActualStartDate", "@ActualStartDate"), new FieldValue("ActualEndDate", "@ActualEndDate"), new FieldValue("AssignedToID", "@AssignedToID"), new FieldValue("FeePercentage", "@FeePercentage"), new FieldValue("CompletedPercentage", "@CompletedPercentage"), new FieldValue("TotalHours", "@TotalHours"), new FieldValue("Status", "@Status"), new FieldValue("CompletedDescription", "@CompletedDescription"), new FieldValue("Note", "@Note"));
			if (isUpdate)
			{
				sqlBuilder.AddInsertUpdateParameters("Job_Task", new FieldValue("DateUpdated", "@DateUpdated", isUpdateConditionField: true, checkForNullValue: true));
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
			parameters.Add("@TaskID", SqlDbType.NVarChar);
			parameters.Add("@Description", SqlDbType.NVarChar);
			parameters.Add("@FeeID", SqlDbType.NVarChar);
			parameters.Add("@JobID", SqlDbType.NVarChar);
			parameters.Add("@TaskGroupID", SqlDbType.NVarChar);
			parameters.Add("@StartDate", SqlDbType.DateTime);
			parameters.Add("@EndDate", SqlDbType.DateTime);
			parameters.Add("@ActualStartDate", SqlDbType.DateTime);
			parameters.Add("@ActualEndDate", SqlDbType.DateTime);
			parameters.Add("@AssignedToID", SqlDbType.NVarChar);
			parameters.Add("@FeePercentage", SqlDbType.Decimal);
			parameters.Add("@CompletedPercentage", SqlDbType.Decimal);
			parameters.Add("@TotalHours", SqlDbType.Decimal);
			parameters.Add("@Status", SqlDbType.TinyInt);
			parameters.Add("@CompletedDescription", SqlDbType.NVarChar);
			parameters.Add("@Note", SqlDbType.NVarChar);
			parameters["@TaskID"].SourceColumn = "TaskID";
			parameters["@Description"].SourceColumn = "Description";
			parameters["@FeeID"].SourceColumn = "FeeID";
			parameters["@JobID"].SourceColumn = "JobID";
			parameters["@TaskGroupID"].SourceColumn = "TaskGroupID";
			parameters["@StartDate"].SourceColumn = "StartDate";
			parameters["@EndDate"].SourceColumn = "EndDate";
			parameters["@ActualStartDate"].SourceColumn = "ActualStartDate";
			parameters["@ActualEndDate"].SourceColumn = "ActualEndDate";
			parameters["@AssignedToID"].SourceColumn = "AssignedToID";
			parameters["@FeePercentage"].SourceColumn = "FeePercentage";
			parameters["@CompletedPercentage"].SourceColumn = "CompletedPercentage";
			parameters["@TotalHours"].SourceColumn = "TotalHours";
			parameters["@Status"].SourceColumn = "Status";
			parameters["@CompletedDescription"].SourceColumn = "CompletedDescription";
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

		public bool InsertJobTask(JobTaskData accountJobTaskData)
		{
			bool result = true;
			SqlCommand insertUpdateCommand = GetInsertUpdateCommand(isUpdate: false);
			SqlTransaction sqlTransaction = null;
			try
			{
				sqlTransaction = (insertUpdateCommand.Transaction = base.DBConfig.StartNewTransaction());
				result = Insert(accountJobTaskData, "Job_Task", insertUpdateCommand);
				string text = accountJobTaskData.JobTaskTable.Rows[0]["TaskID"].ToString();
				AddActivityLog("Project Task", text, ActivityTypes.Add, sqlTransaction);
				UpdateTableRowInsertUpdateInfo("Job_Task", "TaskID", text, sqlTransaction, isInsert: true);
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

		public bool UpdateJobTask(JobTaskData accountJobTaskData)
		{
			bool flag = true;
			SqlCommand insertUpdateCommand = GetInsertUpdateCommand(isUpdate: true);
			SqlTransaction sqlTransaction = null;
			try
			{
				sqlTransaction = (insertUpdateCommand.Transaction = base.DBConfig.StartNewTransaction());
				flag = Update(accountJobTaskData, "Job_Task", insertUpdateCommand);
				if (!flag)
				{
					return flag;
				}
				object obj = accountJobTaskData.JobTaskTable.Rows[0]["TaskID"];
				UpdateTableRowByID("Job_Task", "TaskID", "DateUpdated", obj, DateTime.Now, sqlTransaction);
				string entiyID = accountJobTaskData.JobTaskTable.Rows[0]["Description"].ToString();
				AddActivityLog("Project Task", entiyID, ActivityTypes.Update, sqlTransaction);
				UpdateTableRowInsertUpdateInfo("Job_Task", "TaskID", obj, sqlTransaction, isInsert: false);
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

		public JobTaskData GetJobTask()
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddTable("Job_Task");
			JobTaskData jobTaskData = new JobTaskData();
			sqlBuilder.UseDistinct = false;
			FillDataSet(jobTaskData, "Job_Task", sqlBuilder);
			return jobTaskData;
		}

		public bool DeleteJobTask(string jobTaskID)
		{
			bool flag = true;
			try
			{
				string commandText = "DELETE FROM Job_Task WHERE TaskID = '" + jobTaskID + "'";
				flag = Delete(commandText, null);
				if (!flag)
				{
					return flag;
				}
				AddActivityLog("Project Task", jobTaskID, ActivityTypes.Delete, null);
				return flag;
			}
			catch
			{
				throw;
			}
		}

		public JobTaskData GetJobTaskByID(string id)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			CommandHelper commandHelper = null;
			commandHelper = new CommandHelper();
			commandHelper.FieldName = "TaskID";
			commandHelper.SqlFieldType = SqlDbType.NVarChar;
			commandHelper.FieldValue = id;
			commandHelper.TableName = "Job_Task";
			sqlBuilder.AddCommandHelper(commandHelper);
			sqlBuilder.IsComparing = true;
			JobTaskData jobTaskData = new JobTaskData();
			sqlBuilder.UseDistinct = false;
			FillDataSet(jobTaskData, "Job_Task", sqlBuilder);
			return jobTaskData;
		}

		public DataSet GetJobTaskByFields(params string[] columns)
		{
			return GetJobTaskByFields(null, isInactive: true, columns);
		}

		public DataSet GetJobTaskByFields(string[] jobTaskID, params string[] columns)
		{
			return GetJobTaskByFields(jobTaskID, isInactive: true, columns);
		}

		public DataSet GetJobTaskByFields(string[] ids, bool isInactive, params string[] columns)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddTable("Job_Task");
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
				commandHelper.FieldName = "TaskID";
				commandHelper.FieldValue = ids;
				commandHelper.SqlFieldType = SqlDbType.NVarChar;
				commandHelper.TableName = "Job_Task";
				sqlBuilder.AddCommandHelper(commandHelper);
			}
			DataSet dataSet = new DataSet();
			dataSet.EnforceConstraints = false;
			sqlBuilder.UseDistinct = false;
			FillDataSet(dataSet, "Job_Task", sqlBuilder);
			return dataSet;
		}

		public DataSet GetJobTaskList()
		{
			DataSet dataSet = new DataSet();
			string textCommand = "Select TaskID [Task Code], JT.Description,JT.JobID [Job Code],J.JobName [Job],JTG.TaskGroupName [Task Group],JT.StartDate [Start Date],JT.EndDate [End Date],\r\n                                ActualStartDate [Act Start Date],ActualEndDate [Act End Date],\r\n                                EMP.FirstName + ' ' + Emp.LastName AS [Assigned To],FeePercentage [Fee %], CompletedPercentage [Completed %],JT.Status\r\n                                  FROM Job_Task JT INNER JOIN Job J ON JT.JobID = J.JobID\r\n                                  LEFT OUTER JOIN Employee EMP ON EMP.EmployeeID=JT.AssignedToID\r\n                                  LEFT OUTER JOIN Job_Task_Group JTG ON JTG.TaskGroupID=JT.TaskGroupID";
			FillDataSet(dataSet, "Job_Task", textCommand);
			return dataSet;
		}

		public DataSet GetJobTaskListToPrint()
		{
			DataSet dataSet = new DataSet();
			string textCommand = "Select TaskID [Task Code], JT.Description,JT.JobID [Job Code],J.JobName [Job],JTG.TaskGroupID,JTG.TaskGroupName,JT.StartDate [Start Date],JT.EndDate [End Date],\r\n                                ActualStartDate [Act Start Date],ActualEndDate [Act End Date],\r\n                                EMP.FirstName + ' ' + Emp.LastName AS [Assigned To],FeePercentage [Fee %], CompletedPercentage [Completed %],JT.Status\r\n                                  FROM Job_Task JT LEFT JOIN Job J ON JT.JobID = J.JobID\r\n                                  LEFT OUTER JOIN Employee EMP ON EMP.EmployeeID=JT.AssignedToID\r\n                                  LEFT OUTER JOIN Job_Task_Group JTG ON JTG.TaskGroupID=JT.TaskGroupID";
			FillDataSet(dataSet, "Job_Task", textCommand);
			return dataSet;
		}

		public DataSet GetJobTaskComboList()
		{
			DataSet dataSet = new DataSet();
			string textCommand = "SELECT TaskID [Code],Description [Name]\r\n                           FROM Job_Task ORDER BY TaskID,Description";
			FillDataSet(dataSet, "Job_Task", textCommand);
			return dataSet;
		}
	}
}
