using Micromind.Common;
using Micromind.Common.Data;
using Micromind.Common.Libraries;
using System;
using System.Data;
using System.Data.SqlClient;

namespace Micromind.Data
{
	public sealed class EmployeeSkills : StoreObject
	{
		private const string EMPLOYEESKILL_TABLE = "Employee_Skill_Detail";

		private const string EMPLOYEEID_PARM = "@EmployeeID";

		private const string SKILLID_PARM = "@SkillID";

		private const string SKILLLEVEL_PARM = "@SkillLevel";

		private const string REMARKS_PARM = "@Remarks";

		private const string ROWINDEX_PARM = "@RowIndex";

		private const string CREATEDBY_PARM = "@CreatedBy";

		private const string DATECREATED_PARM = "@DateCreated";

		private const string UPDATEDBY_PARM = "@UpdatedBy";

		private const string DATEUPDATED_PARM = "@DateUpdated";

		public EmployeeSkills(Config config)
			: base(config)
		{
		}

		private string GetInsertUpdateText(bool isUpdate)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddInsertUpdateParameters("Employee_Skill_Detail", new FieldValue("EmployeeID", "@EmployeeID", isUpdateConditionField: true), new FieldValue("SkillID", "@SkillID", isUpdateConditionField: true), new FieldValue("SkillLevel", "@SkillLevel"), new FieldValue("RowIndex", "@RowIndex"), new FieldValue("Remarks", "@Remarks"));
			if (isUpdate)
			{
				sqlBuilder.AddInsertUpdateParameters("Employee_Skill_Detail", new FieldValue("DateUpdated", "@DateUpdated", isUpdateConditionField: true, checkForNullValue: true));
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
			parameters.Add("@EmployeeID", SqlDbType.NVarChar);
			parameters.Add("@SkillID", SqlDbType.NVarChar);
			parameters.Add("@SkillLevel", SqlDbType.NVarChar);
			parameters.Add("@Remarks", SqlDbType.NVarChar);
			parameters.Add("@RowIndex", SqlDbType.SmallInt);
			parameters["@EmployeeID"].SourceColumn = "EmployeeID";
			parameters["@SkillID"].SourceColumn = "SkillID";
			parameters["@SkillLevel"].SourceColumn = "SkillLevel";
			parameters["@Remarks"].SourceColumn = "Remarks";
			parameters["@RowIndex"].SourceColumn = "RowIndex";
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

		public bool InsertEmployeeSkill(EmployeeSkillData employeeSkillData)
		{
			bool result = true;
			SqlCommand insertUpdateCommand = GetInsertUpdateCommand(isUpdate: false);
			SqlTransaction sqlTransaction = null;
			try
			{
				string employeeID = employeeSkillData.EmployeeSkillTable.Rows[0]["EmployeeID"].ToString();
				sqlTransaction = base.DBConfig.StartNewTransaction();
				DeleteEmployeeSkills(sqlTransaction, employeeID);
				insertUpdateCommand.Transaction = sqlTransaction;
				result = Insert(employeeSkillData, "Employee_Skill_Detail", insertUpdateCommand);
				string text = employeeSkillData.EmployeeSkillTable.Rows[0]["SkillID"].ToString();
				AddActivityLog("Employee Skill", text, ActivityTypes.Add, sqlTransaction);
				UpdateTableRowInsertUpdateInfo("Employee_Skill_Detail", "SkillID", text, sqlTransaction, isInsert: true);
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

		public bool UpdateEmployeeSkill(EmployeeSkillData employeeSkillData)
		{
			bool flag = true;
			SqlCommand insertUpdateCommand = GetInsertUpdateCommand(isUpdate: true);
			SqlTransaction sqlTransaction = null;
			try
			{
				string employeeID = employeeSkillData.EmployeeSkillTable.Rows[0]["EmployeeID"].ToString();
				sqlTransaction = base.DBConfig.StartNewTransaction();
				DeleteEmployeeSkills(sqlTransaction, employeeID);
				insertUpdateCommand.Transaction = sqlTransaction;
				flag = Update(employeeSkillData, "Employee_Skill_Detail", insertUpdateCommand);
				if (!flag)
				{
					return flag;
				}
				object obj = employeeSkillData.EmployeeSkillTable.Rows[0]["SkillID"];
				UpdateTableRowByID("Employee_Skill_Detail", "SkillID", "DateUpdated", obj, DateTime.Now, sqlTransaction);
				string entiyID = employeeSkillData.EmployeeSkillTable.Rows[0]["SkillID"].ToString();
				AddActivityLog("Employee Skill", entiyID, ActivityTypes.Update, sqlTransaction);
				UpdateTableRowInsertUpdateInfo("Employee_Skill_Detail", "SkillID", obj, sqlTransaction, isInsert: false);
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

		private bool DeleteEmployeeSkills(SqlTransaction sqlTransaction, string employeeID)
		{
			bool flag = true;
			try
			{
				string commandText = "DELETE FROM Employee_Skill_Detail WHERE EmployeeID = '" + employeeID + "'";
				flag = Delete(commandText, sqlTransaction);
				if (!flag)
				{
					return flag;
				}
				AddActivityLog("Employee Skill", employeeID, ActivityTypes.Delete, null);
				return flag;
			}
			catch
			{
				throw;
			}
		}

		public EmployeeSkillData GetEmployeeSkill()
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddTable("Employee_Skill_Detail");
			EmployeeSkillData employeeSkillData = new EmployeeSkillData();
			sqlBuilder.UseDistinct = false;
			FillDataSet(employeeSkillData, "Employee_Skill_Detail", sqlBuilder);
			return employeeSkillData;
		}

		public EmployeeSkillData GetEmployeeSkillsByEmployeeID(string employeeID)
		{
			EmployeeSkillData employeeSkillData = new EmployeeSkillData();
			string textCommand = "Select EmployeeID,SkillID,Remarks,SkillLevel \r\n                            FROM Employee_Skill_Detail\r\n                            WHERE EmployeeID='" + employeeID + "' ORDER BY RowIndex";
			FillDataSet(employeeSkillData, "Employee_Skill_Detail", textCommand);
			return employeeSkillData;
		}

		public bool DeleteEmployeeSkill(string employeeSkillID)
		{
			bool flag = true;
			try
			{
				string commandText = "DELETE FROM Employee_Skill_Detail WHERE SkillID = '" + employeeSkillID + "'";
				flag = Delete(commandText, null);
				if (!flag)
				{
					return flag;
				}
				AddActivityLog("Employee Skill", employeeSkillID, ActivityTypes.Delete, null);
				return flag;
			}
			catch
			{
				throw;
			}
		}

		public EmployeeSkillData GetEmployeeSkillByID(string id)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			CommandHelper commandHelper = null;
			commandHelper = new CommandHelper();
			commandHelper.FieldName = "SkillID";
			commandHelper.SqlFieldType = SqlDbType.NVarChar;
			commandHelper.FieldValue = id;
			commandHelper.TableName = "Employee_Skill_Detail";
			sqlBuilder.AddCommandHelper(commandHelper);
			sqlBuilder.IsComparing = true;
			EmployeeSkillData employeeSkillData = new EmployeeSkillData();
			sqlBuilder.UseDistinct = false;
			FillDataSet(employeeSkillData, "Employee_Skill_Detail", sqlBuilder);
			return employeeSkillData;
		}

		public DataSet GetEmployeeSkillByFields(params string[] columns)
		{
			return GetEmployeeSkillByFields(null, isInactive: true, columns);
		}

		public DataSet GetEmployeeSkillByFields(string[] employeeSkillID, params string[] columns)
		{
			return GetEmployeeSkillByFields(employeeSkillID, isInactive: true, columns);
		}

		public DataSet GetEmployeeSkillByFields(string[] ids, bool isInactive, params string[] columns)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddTable("Employee_Skill_Detail");
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
				commandHelper.FieldName = "SkillID";
				commandHelper.FieldValue = ids;
				commandHelper.SqlFieldType = SqlDbType.NVarChar;
				commandHelper.TableName = "Employee_Skill_Detail";
				sqlBuilder.AddCommandHelper(commandHelper);
			}
			DataSet dataSet = new DataSet();
			dataSet.EnforceConstraints = false;
			sqlBuilder.UseDistinct = false;
			FillDataSet(dataSet, "Employee_Skill_Detail", sqlBuilder);
			return dataSet;
		}

		public DataSet GetEmployeeSkillList()
		{
			return null;
		}

		public DataSet GetEmployeeSkillComboList()
		{
			return null;
		}
	}
}
