using Micromind.Common;
using Micromind.Common.Data;
using Micromind.Common.Libraries;
using System;
using System.Data;
using System.Data.SqlClient;

namespace Micromind.Data
{
	public sealed class Department : StoreObject
	{
		private const string DEPARTMENTID_PARM = "@DepartmentID";

		private const string DEPARTMENTNAME_PARM = "@DepartmentName";

		public const string NOTE_PARM = "@Note";

		public const string INACTIVE_PARM = "@Inactive";

		public const string DEPARTMENT_TABLE = "Department";

		private const string CREATEDBY_PARM = "@CreatedBy";

		private const string DATECREATED_PARM = "@DateCreated";

		private const string UPDATEDBY_PARM = "@UpdatedBy";

		private const string DATEUPDATED_PARM = "@DateUpdated";

		public Department(Config config)
			: base(config)
		{
		}

		private string GetInsertUpdateText(bool isUpdate)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddInsertUpdateParameters("Department", new FieldValue("DepartmentID", "@DepartmentID", isUpdateConditionField: true), new FieldValue("DepartmentName", "@DepartmentName"), new FieldValue("Inactive", "@Inactive"), new FieldValue("Note", "@Note"));
			if (isUpdate)
			{
				sqlBuilder.AddInsertUpdateParameters("Department", new FieldValue("DateUpdated", "@DateUpdated", isUpdateConditionField: true, checkForNullValue: true));
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
			parameters.Add("@DepartmentID", SqlDbType.NVarChar);
			parameters.Add("@DepartmentName", SqlDbType.NVarChar);
			parameters.Add("@Note", SqlDbType.NVarChar);
			parameters.Add("@Inactive", SqlDbType.Bit);
			parameters["@DepartmentID"].SourceColumn = "DepartmentID";
			parameters["@DepartmentName"].SourceColumn = "DepartmentName";
			parameters["@Note"].SourceColumn = "Note";
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

		public bool InsertDepartment(DepartmentData accountDepartmentData)
		{
			bool result = true;
			SqlCommand insertUpdateCommand = GetInsertUpdateCommand(isUpdate: false);
			SqlTransaction sqlTransaction = null;
			try
			{
				sqlTransaction = (insertUpdateCommand.Transaction = base.DBConfig.StartNewTransaction());
				result = Insert(accountDepartmentData, "Department", insertUpdateCommand);
				string text = accountDepartmentData.DepartmentTable.Rows[0]["DepartmentID"].ToString();
				AddActivityLog("Department", text, ActivityTypes.Add, sqlTransaction);
				UpdateTableRowInsertUpdateInfo("Department", "DepartmentID", text, sqlTransaction, isInsert: true);
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

		public bool UpdateDepartment(DepartmentData accountDepartmentData)
		{
			bool flag = true;
			SqlCommand insertUpdateCommand = GetInsertUpdateCommand(isUpdate: true);
			SqlTransaction sqlTransaction = null;
			try
			{
				sqlTransaction = (insertUpdateCommand.Transaction = base.DBConfig.StartNewTransaction());
				flag = Update(accountDepartmentData, "Department", insertUpdateCommand);
				if (!flag)
				{
					return flag;
				}
				object obj = accountDepartmentData.DepartmentTable.Rows[0]["DepartmentID"];
				UpdateTableRowByID("Department", "DepartmentID", "DateUpdated", obj, DateTime.Now, sqlTransaction);
				string entiyID = accountDepartmentData.DepartmentTable.Rows[0]["DepartmentName"].ToString();
				AddActivityLog("Department", entiyID, ActivityTypes.Update, sqlTransaction);
				UpdateTableRowInsertUpdateInfo("Department", "DepartmentID", obj, sqlTransaction, isInsert: false);
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

		public DepartmentData GetDepartment()
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddTable("Department");
			DepartmentData departmentData = new DepartmentData();
			sqlBuilder.UseDistinct = false;
			FillDataSet(departmentData, "Department", sqlBuilder);
			return departmentData;
		}

		public bool DeleteDepartment(string departmentID)
		{
			bool flag = true;
			try
			{
				string commandText = "DELETE FROM Department WHERE DepartmentID = '" + departmentID + "'";
				flag = Delete(commandText, null);
				if (!flag)
				{
					return flag;
				}
				AddActivityLog("Department", departmentID, ActivityTypes.Delete, null);
				return flag;
			}
			catch
			{
				throw;
			}
		}

		public DepartmentData GetDepartmentByID(string id)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			CommandHelper commandHelper = null;
			commandHelper = new CommandHelper();
			commandHelper.FieldName = "DepartmentID";
			commandHelper.SqlFieldType = SqlDbType.NVarChar;
			commandHelper.FieldValue = id;
			commandHelper.TableName = "Department";
			sqlBuilder.AddCommandHelper(commandHelper);
			sqlBuilder.IsComparing = true;
			DepartmentData departmentData = new DepartmentData();
			sqlBuilder.UseDistinct = false;
			FillDataSet(departmentData, "Department", sqlBuilder);
			return departmentData;
		}

		public DataSet GetDepartmentByFields(params string[] columns)
		{
			return GetDepartmentByFields(null, isInactive: true, columns);
		}

		public DataSet GetDepartmentByFields(string[] departmentID, params string[] columns)
		{
			return GetDepartmentByFields(departmentID, isInactive: true, columns);
		}

		public DataSet GetDepartmentByFields(string[] ids, bool isInactive, params string[] columns)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddTable("Department");
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
				commandHelper.FieldName = "DepartmentID";
				commandHelper.FieldValue = ids;
				commandHelper.SqlFieldType = SqlDbType.NVarChar;
				commandHelper.TableName = "Department";
				sqlBuilder.AddCommandHelper(commandHelper);
			}
			DataSet dataSet = new DataSet();
			dataSet.EnforceConstraints = false;
			sqlBuilder.UseDistinct = false;
			FillDataSet(dataSet, "Department", sqlBuilder);
			return dataSet;
		}

		public DataSet GetDepartmentList()
		{
			DataSet dataSet = new DataSet();
			string textCommand = "SELECT DepartmentID [Department Code],DepartmentName [Department Name],Note,Inactive\r\n                           FROM Department ";
			FillDataSet(dataSet, "Department", textCommand);
			return dataSet;
		}

		public DataSet GetDepartmentComboList()
		{
			DataSet dataSet = new DataSet();
			string textCommand = "SELECT DepartmentID [Code],DepartmentName [Name]\r\n                           FROM Department ORDER BY DepartmentID,DepartmentName";
			FillDataSet(dataSet, "Department", textCommand);
			return dataSet;
		}
	}
}
