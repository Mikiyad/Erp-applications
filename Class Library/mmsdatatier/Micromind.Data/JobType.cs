using Micromind.Common;
using Micromind.Common.Data;
using Micromind.Common.Libraries;
using System;
using System.Data;
using System.Data.SqlClient;

namespace Micromind.Data
{
	public sealed class JobType : StoreObject
	{
		private const string JOBTYPEID_PARM = "@JobTypeID";

		private const string JOBTYPENAME_PARM = "@JobTypeName";

		private const string JOBTYPEDESC_PARM = "@Desc";

		private const string INACTIVE_PARM = "@Inactive";

		private const string AMCENABLED_PARM = "@AMCEnabled";

		private const string JOBTYPE_TABLE = "Job_Type";

		private const string CREATEDBY_PARM = "@CreatedBy";

		private const string DATECREATED_PARM = "@DateCreated";

		private const string UPDATEDBY_PARM = "@UpdatedBy";

		private const string DATEUPDATED_PARM = "@DateUpdated";

		public JobType(Config config)
			: base(config)
		{
		}

		private string GetInsertUpdateText(bool isUpdate)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddInsertUpdateParameters("Job_Type", new FieldValue("JobTypeID", "@JobTypeID", isUpdateConditionField: true), new FieldValue("JobTypeName", "@JobTypeName"), new FieldValue("Description", "@Desc"), new FieldValue("AMCEnabled", "@AMCEnabled"), new FieldValue("Inactive", "@Inactive"));
			if (isUpdate)
			{
				sqlBuilder.AddInsertUpdateParameters("Job_Type", new FieldValue("DateUpdated", "@DateUpdated", isUpdateConditionField: true, checkForNullValue: true));
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
			parameters.Add("@JobTypeID", SqlDbType.NVarChar);
			parameters.Add("@JobTypeName", SqlDbType.NVarChar);
			parameters.Add("@Desc", SqlDbType.NVarChar);
			parameters.Add("@Inactive", SqlDbType.Bit);
			parameters.Add("@AMCEnabled", SqlDbType.Bit);
			parameters["@JobTypeID"].SourceColumn = "JobTypeID";
			parameters["@JobTypeName"].SourceColumn = "JobTypeName";
			parameters["@Desc"].SourceColumn = "Description";
			parameters["@Inactive"].SourceColumn = "Inactive";
			parameters["@AMCEnabled"].SourceColumn = "AMCEnabled";
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

		public bool InsertJobType(JobTypeData accountJobTypeData)
		{
			bool result = true;
			SqlCommand insertUpdateCommand = GetInsertUpdateCommand(isUpdate: false);
			SqlTransaction sqlTransaction = null;
			try
			{
				sqlTransaction = (insertUpdateCommand.Transaction = base.DBConfig.StartNewTransaction());
				result = Insert(accountJobTypeData, "Job_Type", insertUpdateCommand);
				string text = accountJobTypeData.JobTypeTable.Rows[0]["JobTypeID"].ToString();
				AddActivityLog("Job Type", text, ActivityTypes.Add, sqlTransaction);
				UpdateTableRowInsertUpdateInfo("Job_Type", "JobTypeID", text, sqlTransaction, isInsert: true);
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

		public bool UpdateJobType(JobTypeData accountJobTypeData)
		{
			bool flag = true;
			SqlCommand insertUpdateCommand = GetInsertUpdateCommand(isUpdate: true);
			SqlTransaction sqlTransaction = null;
			try
			{
				sqlTransaction = (insertUpdateCommand.Transaction = base.DBConfig.StartNewTransaction());
				flag = Update(accountJobTypeData, "Job_Type", insertUpdateCommand);
				if (!flag)
				{
					return flag;
				}
				object obj = accountJobTypeData.JobTypeTable.Rows[0]["JobTypeID"];
				UpdateTableRowByID("Job_Type", "JobTypeID", "DateUpdated", obj, DateTime.Now, sqlTransaction);
				string entiyID = accountJobTypeData.JobTypeTable.Rows[0]["JobTypeName"].ToString();
				AddActivityLog("Job Type", entiyID, ActivityTypes.Update, sqlTransaction);
				UpdateTableRowInsertUpdateInfo("Job_Type", "JobTypeID", obj, sqlTransaction, isInsert: false);
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

		public JobTypeData GetJobType()
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddTable("Job_Type");
			JobTypeData jobTypeData = new JobTypeData();
			sqlBuilder.UseDistinct = false;
			FillDataSet(jobTypeData, "Job_Type", sqlBuilder);
			return jobTypeData;
		}

		public bool DeleteJobType(string jobTypeID)
		{
			bool flag = true;
			try
			{
				string commandText = "DELETE FROM Job_Type WHERE JobTypeID = '" + jobTypeID + "'";
				flag = Delete(commandText, null);
				if (!flag)
				{
					return flag;
				}
				AddActivityLog("Job Type", jobTypeID, ActivityTypes.Delete, null);
				return flag;
			}
			catch
			{
				throw;
			}
		}

		public JobTypeData GetJobTypeByID(string id)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			CommandHelper commandHelper = null;
			commandHelper = new CommandHelper();
			commandHelper.FieldName = "JobTypeID";
			commandHelper.SqlFieldType = SqlDbType.NVarChar;
			commandHelper.FieldValue = id;
			commandHelper.TableName = "Job_Type";
			sqlBuilder.AddCommandHelper(commandHelper);
			sqlBuilder.IsComparing = true;
			JobTypeData jobTypeData = new JobTypeData();
			sqlBuilder.UseDistinct = false;
			FillDataSet(jobTypeData, "Job_Type", sqlBuilder);
			return jobTypeData;
		}

		public DataSet GetJobTypeByFields(params string[] columns)
		{
			return GetJobTypeByFields(null, isInactive: true, columns);
		}

		public DataSet GetJobTypeByFields(string[] jobTypeID, params string[] columns)
		{
			return GetJobTypeByFields(jobTypeID, isInactive: true, columns);
		}

		public DataSet GetJobTypeByFields(string[] ids, bool isInactive, params string[] columns)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddTable("Job_Type");
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
				commandHelper.TableName = "Job_Type";
				sqlBuilder.AddCommandHelper(commandHelper);
			}
			DataSet dataSet = new DataSet();
			dataSet.EnforceConstraints = false;
			sqlBuilder.UseDistinct = false;
			FillDataSet(dataSet, "Job_Type", sqlBuilder);
			return dataSet;
		}

		public DataSet GetJobTypeList()
		{
			DataSet dataSet = new DataSet();
			string textCommand = "SELECT JobTypeID [Type Code],JobTypeName [Type Name],Inactive\r\n                           FROM Job_Type ";
			FillDataSet(dataSet, "Job_Type", textCommand);
			return dataSet;
		}

		public DataSet GetJobTypeComboList()
		{
			DataSet dataSet = new DataSet();
			string textCommand = "SELECT JobTypeID [Code], JobTypeName [Name]\r\n                           FROM Job_Type ORDER BY JobTypeID,JobTypeName";
			FillDataSet(dataSet, "Job_Type", textCommand);
			return dataSet;
		}
	}
}
