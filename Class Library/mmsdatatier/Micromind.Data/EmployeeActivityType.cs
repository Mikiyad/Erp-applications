using Micromind.Common;
using Micromind.Common.Data;
using Micromind.Common.Libraries;
using System;
using System.Data;
using System.Data.SqlClient;

namespace Micromind.Data
{
	public sealed class EmployeeActivityType : StoreObject
	{
		private const string ACTIVITYTYPEID_PARM = "@ActivityTypeID";

		private const string ACTIVITYTYPENAME_PARM = "@ActivityTypeName";

		private const string INACTIVE_PARM = "@Inactive";

		private const string NOTE_PARM = "@Note";

		private const string ACTIVITYNATURE_PARM = "@ActivityNature";

		private const string ACTIVITYTYPE_TABLE = "Employee_Activity_Type";

		private const string CREATEDBY_PARM = "@CreatedBy";

		private const string DATECREATED_PARM = "@DateCreated";

		private const string UPDATEDBY_PARM = "@UpdatedBy";

		private const string DATEUPDATED_PARM = "@DateUpdated";

		public EmployeeActivityType(Config config)
			: base(config)
		{
		}

		private string GetInsertUpdateText(bool isUpdate)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddInsertUpdateParameters("Employee_Activity_Type", new FieldValue("ActivityTypeID", "@ActivityTypeID", isUpdateConditionField: true), new FieldValue("ActivityTypeName", "@ActivityTypeName"), new FieldValue("Inactive", "@Inactive"), new FieldValue("ActivityNature", "@ActivityNature"), new FieldValue("Note", "@Note"));
			if (isUpdate)
			{
				sqlBuilder.AddInsertUpdateParameters("Employee_Activity_Type", new FieldValue("DateUpdated", "@DateUpdated", isUpdateConditionField: true, checkForNullValue: true));
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
			parameters.Add("@ActivityTypeID", SqlDbType.NVarChar);
			parameters.Add("@ActivityTypeName", SqlDbType.NVarChar);
			parameters.Add("@Inactive", SqlDbType.Bit);
			parameters.Add("@Note", SqlDbType.NVarChar);
			parameters.Add("@ActivityNature", SqlDbType.NVarChar);
			parameters["@ActivityTypeID"].SourceColumn = "ActivityTypeID";
			parameters["@ActivityTypeName"].SourceColumn = "ActivityTypeName";
			parameters["@Inactive"].SourceColumn = "Inactive";
			parameters["@Note"].SourceColumn = "Note";
			parameters["@ActivityNature"].SourceColumn = "ActivityNature";
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

		public bool InsertActivityType(EmployeeActivityTypeData employeeActivityTypeData)
		{
			bool result = true;
			SqlCommand insertUpdateCommand = GetInsertUpdateCommand(isUpdate: false);
			SqlTransaction sqlTransaction = null;
			try
			{
				sqlTransaction = (insertUpdateCommand.Transaction = base.DBConfig.StartNewTransaction());
				result = Insert(employeeActivityTypeData, "Employee_Activity_Type", insertUpdateCommand);
				string text = employeeActivityTypeData.ActivityTypeTable.Rows[0]["ActivityTypeID"].ToString();
				AddActivityLog("ActivityType", text, ActivityTypes.Add, sqlTransaction);
				UpdateTableRowInsertUpdateInfo("Employee_Activity_Type", "ActivityTypeID", text, sqlTransaction, isInsert: true);
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

		public bool UpdateActivityType(EmployeeActivityTypeData employeeActivityTypeData)
		{
			bool flag = true;
			SqlCommand insertUpdateCommand = GetInsertUpdateCommand(isUpdate: true);
			SqlTransaction sqlTransaction = null;
			try
			{
				sqlTransaction = (insertUpdateCommand.Transaction = base.DBConfig.StartNewTransaction());
				flag = Update(employeeActivityTypeData, "Employee_Activity_Type", insertUpdateCommand);
				if (!flag)
				{
					return flag;
				}
				object obj = employeeActivityTypeData.ActivityTypeTable.Rows[0]["ActivityTypeID"];
				UpdateTableRowByID("Employee_Activity_Type", "ActivityTypeID", "DateUpdated", obj, DateTime.Now, sqlTransaction);
				string entiyID = employeeActivityTypeData.ActivityTypeTable.Rows[0]["ActivityTypeName"].ToString();
				AddActivityLog("ActivityType", entiyID, ActivityTypes.Update, sqlTransaction);
				UpdateTableRowInsertUpdateInfo("Employee_Activity_Type", "ActivityTypeID", obj, sqlTransaction, isInsert: false);
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

		public EmployeeActivityTypeData GetActivityType()
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddTable("Employee_Activity_Type");
			EmployeeActivityTypeData employeeActivityTypeData = new EmployeeActivityTypeData();
			sqlBuilder.UseDistinct = false;
			FillDataSet(employeeActivityTypeData, "Employee_Activity_Type", sqlBuilder);
			return employeeActivityTypeData;
		}

		public bool DeleteActivityType(string actionTypeID)
		{
			bool flag = true;
			try
			{
				string commandText = "DELETE FROM Employee_Activity_Type WHERE ActivityTypeID = '" + actionTypeID + "'";
				flag = Delete(commandText, null);
				if (!flag)
				{
					return flag;
				}
				AddActivityLog("ActivityType", actionTypeID, ActivityTypes.Delete, null);
				return flag;
			}
			catch
			{
				throw;
			}
		}

		public EmployeeActivityTypeData GetActivityTypeByID(string id)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			CommandHelper commandHelper = null;
			commandHelper = new CommandHelper();
			commandHelper.FieldName = "ActivityTypeID";
			commandHelper.SqlFieldType = SqlDbType.NVarChar;
			commandHelper.FieldValue = id;
			commandHelper.TableName = "Employee_Activity_Type";
			sqlBuilder.AddCommandHelper(commandHelper);
			sqlBuilder.IsComparing = true;
			EmployeeActivityTypeData employeeActivityTypeData = new EmployeeActivityTypeData();
			sqlBuilder.UseDistinct = false;
			FillDataSet(employeeActivityTypeData, "Employee_Activity_Type", sqlBuilder);
			return employeeActivityTypeData;
		}

		public DataSet GetActivityTypeByFields(params string[] columns)
		{
			return GetActivityTypeByFields(null, isInactive: true, columns);
		}

		public DataSet GetActivityTypeByFields(string[] actionTypeID, params string[] columns)
		{
			return GetActivityTypeByFields(actionTypeID, isInactive: true, columns);
		}

		public DataSet GetActivityTypeByFields(string[] ids, bool isInactive, params string[] columns)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddTable("Employee_Activity_Type");
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
				commandHelper.FieldName = "ActivityTypeID";
				commandHelper.FieldValue = ids;
				commandHelper.SqlFieldType = SqlDbType.NVarChar;
				commandHelper.TableName = "Employee_Activity_Type";
				sqlBuilder.AddCommandHelper(commandHelper);
			}
			DataSet dataSet = new DataSet();
			dataSet.EnforceConstraints = false;
			sqlBuilder.UseDistinct = false;
			FillDataSet(dataSet, "Employee_Activity_Type", sqlBuilder);
			return dataSet;
		}

		public DataSet GetActivityTypeList()
		{
			DataSet dataSet = new DataSet();
			string textCommand = "SELECT ActivityTypeID [Type Code],ActivityTypeName [Activity Type Name],Note,Inactive  \r\n                           FROM Employee_Activity_Type ";
			FillDataSet(dataSet, "Employee_Activity_Type", textCommand);
			return dataSet;
		}

		public DataSet GetActivityTypeComboList()
		{
			DataSet dataSet = new DataSet();
			string textCommand = "SELECT ActivityTypeID [Code],ActivityTypeName [Name]\r\n                           FROM Employee_Activity_Type ORDER BY ActivityTypeID,ActivityTypeName";
			FillDataSet(dataSet, "Employee_Activity_Type", textCommand);
			return dataSet;
		}
	}
}
