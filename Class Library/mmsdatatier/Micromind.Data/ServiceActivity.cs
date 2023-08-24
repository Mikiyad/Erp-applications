using Micromind.Common;
using Micromind.Common.Data;
using Micromind.Common.Libraries;
using System;
using System.Data;
using System.Data.SqlClient;

namespace Micromind.Data
{
	public sealed class ServiceActivity : StoreObject
	{
		private const string SERVICEACTIVITYID_PARM = "@ServiceActivityID";

		private const string SERVICEACTIVITYNAME_PARM = "@ServiceActivityName";

		private const string SERVICEACTIVITYDESC_PARM = "@Desc";

		private const string INACTIVE_PARM = "@Inactive";

		private const string SERVICEACTIVITY_TABLE = "Service_Activity";

		private const string CREATEDBY_PARM = "@CreatedBy";

		private const string DATECREATED_PARM = "@DateCreated";

		private const string UPDATEDBY_PARM = "@UpdatedBy";

		private const string DATEUPDATED_PARM = "@DateUpdated";

		public ServiceActivity(Config config)
			: base(config)
		{
		}

		private string GetInsertUpdateText(bool isUpdate)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddInsertUpdateParameters("Service_Activity", new FieldValue("ServiceActivityID", "@ServiceActivityID", isUpdateConditionField: true), new FieldValue("ServiceActivityName", "@ServiceActivityName"), new FieldValue("Description", "@Desc"), new FieldValue("Inactive", "@Inactive"));
			if (isUpdate)
			{
				sqlBuilder.AddInsertUpdateParameters("Service_Activity", new FieldValue("DateUpdated", "@DateUpdated", isUpdateConditionField: true, checkForNullValue: true));
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
			parameters.Add("@ServiceActivityID", SqlDbType.NVarChar);
			parameters.Add("@ServiceActivityName", SqlDbType.NVarChar);
			parameters.Add("@Desc", SqlDbType.NVarChar);
			parameters.Add("@Inactive", SqlDbType.Bit);
			parameters["@ServiceActivityID"].SourceColumn = "ServiceActivityID";
			parameters["@ServiceActivityName"].SourceColumn = "ServiceActivityName";
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

		public bool InsertServiceActivity(ServiceActivityData accountServiceActivityData)
		{
			bool result = true;
			SqlCommand insertUpdateCommand = GetInsertUpdateCommand(isUpdate: false);
			SqlTransaction sqlTransaction = null;
			try
			{
				sqlTransaction = (insertUpdateCommand.Transaction = base.DBConfig.StartNewTransaction());
				result = Insert(accountServiceActivityData, "Service_Activity", insertUpdateCommand);
				string text = accountServiceActivityData.ServiceActivityTable.Rows[0]["ServiceActivityID"].ToString();
				AddActivityLog("Job Type", text, ActivityTypes.Add, sqlTransaction);
				UpdateTableRowInsertUpdateInfo("Service_Activity", "ServiceActivityID", text, sqlTransaction, isInsert: true);
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

		public bool UpdateServiceActivity(ServiceActivityData accountServiceActivityData)
		{
			bool flag = true;
			SqlCommand insertUpdateCommand = GetInsertUpdateCommand(isUpdate: true);
			SqlTransaction sqlTransaction = null;
			try
			{
				sqlTransaction = (insertUpdateCommand.Transaction = base.DBConfig.StartNewTransaction());
				flag = Update(accountServiceActivityData, "Service_Activity", insertUpdateCommand);
				if (!flag)
				{
					return flag;
				}
				object obj = accountServiceActivityData.ServiceActivityTable.Rows[0]["ServiceActivityID"];
				UpdateTableRowByID("Service_Activity", "ServiceActivityID", "DateUpdated", obj, DateTime.Now, sqlTransaction);
				string entiyID = accountServiceActivityData.ServiceActivityTable.Rows[0]["ServiceActivityName"].ToString();
				AddActivityLog("SErvice Activity", entiyID, ActivityTypes.Update, sqlTransaction);
				UpdateTableRowInsertUpdateInfo("Service_Activity", "ServiceActivityID", obj, sqlTransaction, isInsert: false);
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

		public ServiceActivityData GetServiceActivity()
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddTable("Service_Activity");
			ServiceActivityData serviceActivityData = new ServiceActivityData();
			sqlBuilder.UseDistinct = false;
			FillDataSet(serviceActivityData, "Service_Activity", sqlBuilder);
			return serviceActivityData;
		}

		public bool DeleteServiceActivity(string jobTypeID)
		{
			bool flag = true;
			try
			{
				string commandText = "DELETE FROM Service_Activity WHERE ServiceActivityID = '" + jobTypeID + "'";
				flag = Delete(commandText, null);
				if (!flag)
				{
					return flag;
				}
				AddActivityLog("service Activity", jobTypeID, ActivityTypes.Delete, null);
				return flag;
			}
			catch
			{
				throw;
			}
		}

		public ServiceActivityData GetServiceActivityByID(string id)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			CommandHelper commandHelper = null;
			commandHelper = new CommandHelper();
			commandHelper.FieldName = "ServiceActivityID";
			commandHelper.SqlFieldType = SqlDbType.NVarChar;
			commandHelper.FieldValue = id;
			commandHelper.TableName = "Service_Activity";
			sqlBuilder.AddCommandHelper(commandHelper);
			sqlBuilder.IsComparing = true;
			ServiceActivityData serviceActivityData = new ServiceActivityData();
			sqlBuilder.UseDistinct = false;
			FillDataSet(serviceActivityData, "Service_Activity", sqlBuilder);
			return serviceActivityData;
		}

		public DataSet GetServiceActivityByFields(params string[] columns)
		{
			return GetServiceActivityByFields(null, isInactive: true, columns);
		}

		public DataSet GetServiceActivityByFields(string[] jobTypeID, params string[] columns)
		{
			return GetServiceActivityByFields(jobTypeID, isInactive: true, columns);
		}

		public DataSet GetServiceActivityByFields(string[] ids, bool isInactive, params string[] columns)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddTable("Service_Activity");
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
				commandHelper.FieldName = "ServiceActivityID";
				commandHelper.FieldValue = ids;
				commandHelper.SqlFieldType = SqlDbType.NVarChar;
				commandHelper.TableName = "Service_Activity";
				sqlBuilder.AddCommandHelper(commandHelper);
			}
			DataSet dataSet = new DataSet();
			dataSet.EnforceConstraints = false;
			sqlBuilder.UseDistinct = false;
			FillDataSet(dataSet, "Service_Activity", sqlBuilder);
			return dataSet;
		}

		public DataSet GetServiceActivityList()
		{
			DataSet dataSet = new DataSet();
			string textCommand = "SELECT ServiceActivityID [Type Code],ServiceActivityName [Type Name],Inactive\r\n                           FROM Service_Activity ";
			FillDataSet(dataSet, "Service_Activity", textCommand);
			return dataSet;
		}

		public DataSet GetServiceActivityComboList()
		{
			DataSet dataSet = new DataSet();
			string textCommand = "SELECT ServiceActivityID [Code], ServiceActivityName [Name]\r\n                           FROM Service_Activity ORDER BY ServiceActivityID,ServiceActivityName";
			FillDataSet(dataSet, "Service_Activity", textCommand);
			return dataSet;
		}
	}
}
