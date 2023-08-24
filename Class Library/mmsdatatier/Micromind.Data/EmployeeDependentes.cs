using Micromind.Common;
using Micromind.Common.Data;
using Micromind.Common.Libraries;
using System;
using System.Data;
using System.Data.SqlClient;

namespace Micromind.Data
{
	public sealed class EmployeeDependentes : StoreObject
	{
		private const string EMPLOYEEDEPENDENT_TABLE = "@Employee_Dependent";

		private const string EMPLOYEEID_PARM = "@EmployeeID";

		private const string DEPENDENTNAME_PARM = "@DependentName";

		private const string GENDER_PARM = "@Gender";

		private const string BIRTHDATE_PARM = "@BirthDate";

		private const string ADDRESS_PARM = "@Dependent";

		private const string NATIONALID_PARM = "@NationalID";

		private const string RELATION_PARM = "@Relation";

		private const string PHONE_PARM = "@Phone";

		private const string COMMENT_PARM = "@Comment";

		private const string CREATEDBY_PARM = "@CreatedBy";

		private const string DATECREATED_PARM = "@DateCreated";

		private const string UPDATEDBY_PARM = "@UpdatedBy";

		private const string DATEUPDATED_PARM = "@DateUpdated";

		public EmployeeDependentes(Config config)
			: base(config)
		{
		}

		private string GetInsertUpdateText(bool isUpdate)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddInsertUpdateParameters("Employee_Dependent", new FieldValue("EmployeeID", "@EmployeeID", isUpdateConditionField: true), new FieldValue("DependentName", "@DependentName", isUpdateConditionField: true), new FieldValue("Gender", "@Gender"), new FieldValue("BirthDate", "@BirthDate"), new FieldValue("Address", "@Dependent"), new FieldValue("NationalID", "@NationalID"), new FieldValue("Relation", "@Relation"), new FieldValue("Phone", "@Phone"), new FieldValue("Comment", "@Comment"));
			if (isUpdate)
			{
				sqlBuilder.AddInsertUpdateParameters("Employee_Dependent", new FieldValue("DateUpdated", "@DateUpdated", isUpdateConditionField: true, checkForNullValue: true));
			}
			if (isUpdate)
			{
				return sqlBuilder.GetUpdateExpression();
			}
			return sqlBuilder.GetInsertExpression();
		}

		public SqlCommand GetInsertUpdateCommand(bool isUpdate)
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
			parameters.Add("@EmployeeID", SqlDbType.NVarChar);
			parameters.Add("@DependentName", SqlDbType.NVarChar);
			parameters.Add("@Gender", SqlDbType.Char);
			parameters.Add("@BirthDate", SqlDbType.SmallDateTime);
			parameters.Add("@Dependent", SqlDbType.NVarChar);
			parameters.Add("@NationalID", SqlDbType.NVarChar);
			parameters.Add("@Relation", SqlDbType.NVarChar);
			parameters.Add("@Phone", SqlDbType.NVarChar);
			parameters.Add("@Comment", SqlDbType.NVarChar);
			parameters["@EmployeeID"].SourceColumn = "EmployeeID";
			parameters["@DependentName"].SourceColumn = "DependentName";
			parameters["@Gender"].SourceColumn = "Gender";
			parameters["@BirthDate"].SourceColumn = "BirthDate";
			parameters["@Dependent"].SourceColumn = "Address";
			parameters["@NationalID"].SourceColumn = "NationalID";
			parameters["@Relation"].SourceColumn = "Relation";
			parameters["@Phone"].SourceColumn = "Phone";
			parameters["@Comment"].SourceColumn = "Comment";
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

		public bool InsertEmployeeDependent(EmployeeDependentData accountEmployeeDependentData)
		{
			bool result = true;
			SqlCommand insertUpdateCommand = GetInsertUpdateCommand(isUpdate: false);
			SqlTransaction sqlTransaction = null;
			try
			{
				sqlTransaction = (insertUpdateCommand.Transaction = base.DBConfig.StartNewTransaction());
				result = Insert(accountEmployeeDependentData, "Employee_Dependent", insertUpdateCommand);
				string text = accountEmployeeDependentData.EmployeeDependentTable.Rows[0]["DependentName"].ToString();
				AddActivityLog("Employee Dependent", text, ActivityTypes.Add, sqlTransaction);
				UpdateTableRowInsertUpdateInfo("Employee_Dependent", "DependentName", text, sqlTransaction, isInsert: true);
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

		public bool UpdateEmployeeDependent(EmployeeDependentData accountEmployeeDependentData)
		{
			bool flag = true;
			SqlCommand insertUpdateCommand = GetInsertUpdateCommand(isUpdate: true);
			SqlTransaction sqlTransaction = null;
			try
			{
				sqlTransaction = (insertUpdateCommand.Transaction = base.DBConfig.StartNewTransaction());
				flag = Update(accountEmployeeDependentData, "Employee_Dependent", insertUpdateCommand);
				if (!flag)
				{
					return flag;
				}
				object obj = accountEmployeeDependentData.EmployeeDependentTable.Rows[0]["DependentName"];
				UpdateTableRowByID("Employee_Dependent", "DependentName", "DateUpdated", obj, DateTime.Now, sqlTransaction);
				string entiyID = accountEmployeeDependentData.EmployeeDependentTable.Rows[0]["DependentName"].ToString();
				AddActivityLog("Employee Dependent", entiyID, ActivityTypes.Update, sqlTransaction);
				UpdateTableRowInsertUpdateInfo("Employee_Dependent", "DependentName", obj, sqlTransaction, isInsert: false);
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

		public EmployeeDependentData GetEmployeeDependent()
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddTable("Employee_Dependent");
			EmployeeDependentData employeeDependentData = new EmployeeDependentData();
			sqlBuilder.UseDistinct = false;
			FillDataSet(employeeDependentData, "Employee_Dependent", sqlBuilder);
			return employeeDependentData;
		}

		public bool DeleteEmployeeDependent(string dependentID, string employeeID)
		{
			bool flag = true;
			try
			{
				string commandText = "DELETE FROM Employee_Dependent WHERE DependentName = '" + dependentID + "' AND EmployeeID ='" + employeeID + "'";
				flag = Delete(commandText, null);
				if (!flag)
				{
					return flag;
				}
				AddActivityLog("Employee Dependent", employeeID + "_" + dependentID, ActivityTypes.Delete, null);
				return flag;
			}
			catch
			{
				throw;
			}
		}

		public EmployeeDependentData GetEmployeeDependentByID(string employeeID, string dependentID)
		{
			EmployeeDependentData employeeDependentData = new EmployeeDependentData();
			string textCommand = "SELECT *\r\n                           FROM Employee_Dependent WHERE EmployeeID='" + employeeID + "' AND DependentName= N'" + dependentID + "'";
			FillDataSet(employeeDependentData, "Employee_Dependent", textCommand);
			return employeeDependentData;
		}

		public DataSet GetEmployeeDependentByFields(params string[] columns)
		{
			return GetEmployeeDependentByFields(null, isInactive: true, columns);
		}

		public DataSet GetEmployeeDependentByFields(string[] employeeDependentID, params string[] columns)
		{
			return GetEmployeeDependentByFields(employeeDependentID, isInactive: true, columns);
		}

		public DataSet GetEmployeeDependentByFields(string[] ids, bool isInactive, params string[] columns)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddTable("Employee_Dependent");
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
				commandHelper.FieldName = "DependentName";
				commandHelper.FieldValue = ids;
				commandHelper.SqlFieldType = SqlDbType.NVarChar;
				commandHelper.TableName = "Employee_Dependent";
				sqlBuilder.AddCommandHelper(commandHelper);
			}
			DataSet dataSet = new DataSet();
			dataSet.EnforceConstraints = false;
			sqlBuilder.UseDistinct = false;
			FillDataSet(dataSet, "Employee_Dependent", sqlBuilder);
			return dataSet;
		}

		public DataSet GetEmployeeDependentList()
		{
			DataSet dataSet = new DataSet();
			string textCommand = "SELECT *\r\n                           FROM Employee_Dependent ";
			FillDataSet(dataSet, "Employee_Dependent", textCommand);
			return dataSet;
		}
	}
}
