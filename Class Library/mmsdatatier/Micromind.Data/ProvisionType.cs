using Micromind.Common;
using Micromind.Common.Data;
using Micromind.Common.Libraries;
using System;
using System.Data;
using System.Data.SqlClient;

namespace Micromind.Data
{
	public sealed class ProvisionType : StoreObject
	{
		private const string PROVISIONTYPEID_PARM = "@ProvisionTypeID";

		private const string PROVISIONTYPENAME_PARM = "@ProvisionTypeName";

		private const string INACTIVE_PARM = "@Inactive";

		public const string EXPENSEACCOUNTID_PARM = "@ExpenseAccountID";

		public const string PROVISIONACCOUNTID_PARM = "@ProvisionAccountID";

		public const string PROVISIONFOR_PARM = "@ProvisionFor";

		public const string EMPLOYEEPROVISIONTYPE_TABLE = "Employee_Provision_Type";

		private const string CREATEDBY_PARM = "@CreatedBy";

		private const string DATECREATED_PARM = "@DateCreated";

		private const string UPDATEDBY_PARM = "@UpdatedBy";

		private const string DATEUPDATED_PARM = "@DateUpdated";

		public ProvisionType(Config config)
			: base(config)
		{
		}

		private string GetInsertUpdateText(bool isUpdate)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddInsertUpdateParameters("Employee_Provision_Type", new FieldValue("ProvisionTypeID", "@ProvisionTypeID", isUpdateConditionField: true), new FieldValue("ProvisionTypeName", "@ProvisionTypeName"), new FieldValue("ExpenseAccountID", "@ExpenseAccountID"), new FieldValue("ProvisionAccountID", "@ProvisionAccountID"), new FieldValue("Inactive", "@Inactive"), new FieldValue("ProvisionFor", "@ProvisionFor"));
			if (isUpdate)
			{
				sqlBuilder.AddInsertUpdateParameters("Employee_Provision_Type", new FieldValue("DateUpdated", "@DateUpdated", isUpdateConditionField: true, checkForNullValue: true));
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
			parameters.Add("@ProvisionTypeID", SqlDbType.NVarChar);
			parameters.Add("@ProvisionTypeName", SqlDbType.NVarChar);
			parameters.Add("@Inactive", SqlDbType.Bit);
			parameters.Add("@ExpenseAccountID", SqlDbType.NVarChar);
			parameters.Add("@ProvisionAccountID", SqlDbType.NVarChar);
			parameters.Add("@ProvisionFor", SqlDbType.Int);
			parameters["@ProvisionTypeID"].SourceColumn = "ProvisionTypeID";
			parameters["@ProvisionTypeName"].SourceColumn = "ProvisionTypeName";
			parameters["@Inactive"].SourceColumn = "Inactive";
			parameters["@ExpenseAccountID"].SourceColumn = "ExpenseAccountID";
			parameters["@ProvisionAccountID"].SourceColumn = "ProvisionAccountID";
			parameters["@ProvisionFor"].SourceColumn = "ProvisionFor";
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

		public bool InsertProvisionType(EmployeeProvisionTypeData provisionTypeData)
		{
			bool result = true;
			SqlCommand insertUpdateCommand = GetInsertUpdateCommand(isUpdate: false);
			SqlTransaction sqlTransaction = null;
			try
			{
				sqlTransaction = (insertUpdateCommand.Transaction = base.DBConfig.StartNewTransaction());
				result = Insert(provisionTypeData, "Employee_Provision_Type", insertUpdateCommand);
				string text = provisionTypeData.EmployeeProvisionTypeTable.Rows[0]["ProvisionTypeID"].ToString();
				AddActivityLog("Employee Provision Type", text, ActivityTypes.Add, sqlTransaction);
				UpdateTableRowInsertUpdateInfo("Employee_Provision_Type", "ProvisionTypeID", text, sqlTransaction, isInsert: true);
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

		public bool UpdateProvisionType(EmployeeProvisionTypeData provisionTypeData)
		{
			bool flag = true;
			SqlCommand insertUpdateCommand = GetInsertUpdateCommand(isUpdate: true);
			SqlTransaction sqlTransaction = null;
			try
			{
				sqlTransaction = (insertUpdateCommand.Transaction = base.DBConfig.StartNewTransaction());
				flag = Update(provisionTypeData, "Employee_Provision_Type", insertUpdateCommand);
				if (!flag)
				{
					return flag;
				}
				object obj = provisionTypeData.EmployeeProvisionTypeTable.Rows[0]["ProvisionTypeID"];
				UpdateTableRowByID("Employee_Provision_Type", "ProvisionTypeID", "DateUpdated", obj, DateTime.Now, sqlTransaction);
				string entiyID = provisionTypeData.EmployeeProvisionTypeTable.Rows[0]["ProvisionTypeName"].ToString();
				AddActivityLog("Employee Provision Type", entiyID, ActivityTypes.Update, sqlTransaction);
				UpdateTableRowInsertUpdateInfo("Employee_Provision_Type", "ProvisionTypeID", obj, sqlTransaction, isInsert: false);
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

		public EmployeeProvisionTypeData GetProvisionType()
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddTable("Employee_Provision_Type");
			EmployeeProvisionTypeData employeeProvisionTypeData = new EmployeeProvisionTypeData();
			sqlBuilder.UseDistinct = false;
			FillDataSet(employeeProvisionTypeData, "Employee_Provision_Type", sqlBuilder);
			return employeeProvisionTypeData;
		}

		public bool DeleteProvisionType(string provisionTypeID)
		{
			bool flag = true;
			try
			{
				string commandText = "DELETE FROM Employee_Provision_Type WHERE ProvisionTypeID = '" + provisionTypeID + "'";
				flag = Delete(commandText, null);
				if (!flag)
				{
					return flag;
				}
				AddActivityLog("Employee Provision Type", provisionTypeID, ActivityTypes.Delete, null);
				return flag;
			}
			catch
			{
				throw;
			}
		}

		public EmployeeProvisionTypeData GetProvisionTypeByID(string id)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			CommandHelper commandHelper = null;
			commandHelper = new CommandHelper();
			commandHelper.FieldName = "ProvisionTypeID";
			commandHelper.SqlFieldType = SqlDbType.NVarChar;
			commandHelper.FieldValue = id;
			commandHelper.TableName = "Employee_Provision_Type";
			sqlBuilder.AddCommandHelper(commandHelper);
			sqlBuilder.IsComparing = true;
			EmployeeProvisionTypeData employeeProvisionTypeData = new EmployeeProvisionTypeData();
			sqlBuilder.UseDistinct = false;
			FillDataSet(employeeProvisionTypeData, "Employee_Provision_Type", sqlBuilder);
			return employeeProvisionTypeData;
		}

		public DataSet GetProvisionTypeByFields(params string[] columns)
		{
			return GetProvisionTypeByFields(null, isInactive: true, columns);
		}

		public DataSet GetProvisionTypeByFields(string[] provisionTypeID, params string[] columns)
		{
			return GetProvisionTypeByFields(provisionTypeID, isInactive: true, columns);
		}

		public DataSet GetProvisionTypeByFields(string[] ids, bool isInactive, params string[] columns)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddTable("Employee_Provision_Type");
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
				commandHelper.FieldName = "ProvisionTypeID";
				commandHelper.FieldValue = ids;
				commandHelper.SqlFieldType = SqlDbType.NVarChar;
				commandHelper.TableName = "Employee_Provision_Type";
				sqlBuilder.AddCommandHelper(commandHelper);
			}
			DataSet dataSet = new DataSet();
			dataSet.EnforceConstraints = false;
			sqlBuilder.UseDistinct = false;
			FillDataSet(dataSet, "Employee_Provision_Type", sqlBuilder);
			return dataSet;
		}

		public DataSet GetProvisionTypeList()
		{
			DataSet dataSet = new DataSet();
			string textCommand = "SELECT ProvisionTypeID [Code],ProvisionTypeName [Name],EAC.AccountName [Expense A/c],PAC.AccountName [Provision A/c],Inactive  \r\n                           FROM Employee_Provision_Type EPT LEFT JOIN Account EAC ON EAC.AccountID=EPT.ExpenseAccountID \r\n                                                            LEFT JOIN Account PAC ON PAC.AccountID=EPT.ProvisionAccountID";
			FillDataSet(dataSet, "Employee_Provision_Type", textCommand);
			return dataSet;
		}

		public DataSet GetProvisionTypeComboList()
		{
			DataSet dataSet = new DataSet();
			string textCommand = "SELECT ProvisionTypeID [Code],ProvisionTypeName [Name],ProvisionFor \r\n                           FROM Employee_Provision_Type ORDER BY ProvisionTypeID,ProvisionTypeName";
			FillDataSet(dataSet, "Employee_Provision_Type", textCommand);
			return dataSet;
		}
	}
}
