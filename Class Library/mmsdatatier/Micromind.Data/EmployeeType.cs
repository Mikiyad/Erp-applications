using Micromind.Common;
using Micromind.Common.Data;
using Micromind.Common.Libraries;
using System;
using System.Data;
using System.Data.SqlClient;

namespace Micromind.Data
{
	public sealed class EmployeeType : StoreObject
	{
		private const string EMPLOYEETYPEID_PARM = "@TypeID";

		private const string EMPLOYEETYPENAME_PARM = "@TypeName";

		private const string NOTE_PARM = "@Note";

		private const string ACCOUNTID_PARM = "@AccountID";

		private const string EOSID_PARM = "@EOSID";

		private const string INACTIVE_PARM = "@Inactive";

		private const string ISPAYROLL_PARM = "@IsPayroll";

		private const string EMPLOYEETYPE_TABLE = "Employee_Type";

		private const string LEAVESELECTION_PARM = "@LeaveSelection";

		private const string CALENDARID_PARM = "@CalendarID";

		private const string DEFAULTOTTYPEID_PARM = "@DefaultOTTypeID";

		private const string CREATEDBY_PARM = "@CreatedBy";

		private const string DATECREATED_PARM = "@DateCreated";

		private const string UPDATEDBY_PARM = "@UpdatedBy";

		private const string DATEUPDATED_PARM = "@DateUpdated";

		private const string EMPLOYEETYPEDETAIL_PARM = "@Employee_Type_Detail";

		private const string LEAVETYPEID_PARM = "@LeaveTypeID";

		private const string OTTYPEID_PARM = "@OTTypeID";

		public EmployeeType(Config config)
			: base(config)
		{
		}

		private string GetInsertUpdateText(bool isUpdate)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddInsertUpdateParameters("Employee_Type", new FieldValue("TypeID", "@TypeID", isUpdateConditionField: true), new FieldValue("TypeName", "@TypeName"), new FieldValue("AccountID", "@AccountID"), new FieldValue("LeaveSelection", "@LeaveSelection"), new FieldValue("EOSID", "@EOSID"), new FieldValue("CalendarID", "@CalendarID"), new FieldValue("DefaultOTTypeID", "@DefaultOTTypeID"), new FieldValue("Inactive", "@Inactive"), new FieldValue("IsPayroll", "@IsPayroll"), new FieldValue("Note", "@Note"));
			if (isUpdate)
			{
				sqlBuilder.AddInsertUpdateParameters("Employee_Type", new FieldValue("DateUpdated", "@DateUpdated", isUpdateConditionField: true, checkForNullValue: true));
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
			parameters.Add("@TypeID", SqlDbType.NVarChar);
			parameters.Add("@TypeName", SqlDbType.NVarChar);
			parameters.Add("@EOSID", SqlDbType.NVarChar);
			parameters.Add("@AccountID", SqlDbType.NVarChar);
			parameters.Add("@LeaveSelection", SqlDbType.NVarChar);
			parameters.Add("@CalendarID", SqlDbType.NVarChar);
			parameters.Add("@DefaultOTTypeID", SqlDbType.NVarChar);
			parameters.Add("@Note", SqlDbType.NVarChar);
			parameters.Add("@Inactive", SqlDbType.Bit);
			parameters.Add("@IsPayroll", SqlDbType.Bit);
			parameters["@TypeID"].SourceColumn = "TypeID";
			parameters["@TypeName"].SourceColumn = "TypeName";
			parameters["@EOSID"].SourceColumn = "EOSID";
			parameters["@AccountID"].SourceColumn = "AccountID";
			parameters["@LeaveSelection"].SourceColumn = "LeaveSelection";
			parameters["@CalendarID"].SourceColumn = "CalendarID";
			parameters["@DefaultOTTypeID"].SourceColumn = "DefaultOTTypeID";
			parameters["@Note"].SourceColumn = "Note";
			parameters["@Inactive"].SourceColumn = "Inactive";
			parameters["@IsPayroll"].SourceColumn = "IsPayroll";
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

		private string GetInsertUpdateDetailText(bool isUpdate)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddInsertUpdateParameters("Employee_Type_Detail", new FieldValue("TypeID", "@TypeID", isUpdateConditionField: true), new FieldValue("LeaveTypeID", "@LeaveTypeID"), new FieldValue("OTTypeID", "@OTTypeID"));
			if (isUpdate)
			{
				sqlBuilder.AddInsertUpdateParameters("Employee_Type_Detail", new FieldValue("DateUpdated", "@DateUpdated", isUpdateConditionField: true, checkForNullValue: true));
			}
			if (isUpdate)
			{
				return sqlBuilder.GetUpdateExpression();
			}
			return sqlBuilder.GetInsertExpression();
		}

		private SqlCommand GetInsertUpdateDetailCommand(bool isUpdate)
		{
			SqlParameterCollection parameters;
			if (isUpdate)
			{
				if (updateCommand != null)
				{
					updateCommand = null;
				}
				updateCommand = new SqlCommand(GetInsertUpdateDetailText(isUpdate: true), base.DBConfig.Connection);
				updateCommand.CommandType = CommandType.Text;
				parameters = updateCommand.Parameters;
			}
			else
			{
				if (insertCommand != null)
				{
					insertCommand = null;
				}
				insertCommand = new SqlCommand(GetInsertUpdateDetailText(isUpdate: false), base.DBConfig.Connection);
				insertCommand.CommandType = CommandType.Text;
				parameters = insertCommand.Parameters;
			}
			parameters.Add("@TypeID", SqlDbType.NVarChar);
			parameters.Add("@LeaveTypeID", SqlDbType.NVarChar);
			parameters.Add("@OTTypeID", SqlDbType.NVarChar);
			parameters["@TypeID"].SourceColumn = "TypeID";
			parameters["@LeaveTypeID"].SourceColumn = "LeaveTypeID";
			parameters["@OTTypeID"].SourceColumn = "OTTypeID";
			if (isUpdate)
			{
				return updateCommand;
			}
			return insertCommand;
		}

		public bool InsertEmployeeType(EmployeeTypeData accountEmployeeTypeData)
		{
			bool flag = true;
			SqlCommand insertUpdateCommand = GetInsertUpdateCommand(isUpdate: false);
			SqlTransaction sqlTransaction = null;
			try
			{
				sqlTransaction = (insertUpdateCommand.Transaction = base.DBConfig.StartNewTransaction());
				flag = Insert(accountEmployeeTypeData, "Employee_Type", insertUpdateCommand);
				if (accountEmployeeTypeData.Tables["Employee_Type_Detail"].Rows.Count > 0)
				{
					insertUpdateCommand = GetInsertUpdateDetailCommand(isUpdate: false);
					insertUpdateCommand.Transaction = sqlTransaction;
					flag &= Insert(accountEmployeeTypeData, "Employee_Type_Detail", insertUpdateCommand);
				}
				string text = accountEmployeeTypeData.EmployeeTypeTable.Rows[0]["TypeID"].ToString();
				AddActivityLog("Employee Type", text, ActivityTypes.Add, sqlTransaction);
				UpdateTableRowInsertUpdateInfo("Employee_Type", "TypeID", text, sqlTransaction, isInsert: true);
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

		public bool UpdateEmployeeType(EmployeeTypeData accountEmployeeTypeData)
		{
			bool flag = true;
			SqlCommand insertUpdateCommand = GetInsertUpdateCommand(isUpdate: true);
			SqlTransaction sqlTransaction = null;
			try
			{
				sqlTransaction = (insertUpdateCommand.Transaction = base.DBConfig.StartNewTransaction());
				flag = Update(accountEmployeeTypeData, "Employee_Type", insertUpdateCommand);
				DeleteEmployeeTypeDetailsRows(accountEmployeeTypeData.EmployeeTypeTable.Rows[0]["TypeID"].ToString(), sqlTransaction);
				if (accountEmployeeTypeData.Tables["Employee_Type_Detail"].Rows.Count > 0)
				{
					insertUpdateCommand = GetInsertUpdateDetailCommand(isUpdate: false);
					insertUpdateCommand.Transaction = sqlTransaction;
					flag &= Insert(accountEmployeeTypeData, "Employee_Type_Detail", insertUpdateCommand);
				}
				if (!flag)
				{
					return flag;
				}
				object obj = accountEmployeeTypeData.EmployeeTypeTable.Rows[0]["TypeID"];
				UpdateTableRowByID("Employee_Type", "TypeID", "DateUpdated", obj, DateTime.Now, sqlTransaction);
				string entiyID = accountEmployeeTypeData.EmployeeTypeTable.Rows[0]["TypeName"].ToString();
				AddActivityLog("Employee Type", entiyID, ActivityTypes.Update, sqlTransaction);
				UpdateTableRowInsertUpdateInfo("Employee_Type", "TypeID", obj, sqlTransaction, isInsert: false);
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

		public EmployeeTypeData GetEmployeeType()
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddTable("Employee_Type");
			EmployeeTypeData employeeTypeData = new EmployeeTypeData();
			sqlBuilder.UseDistinct = false;
			FillDataSet(employeeTypeData, "Employee_Type", sqlBuilder);
			return employeeTypeData;
		}

		public bool DeleteEmployeeType(string EmployeeTypeID)
		{
			bool flag = true;
			try
			{
				SqlTransaction sqlTransaction = base.DBConfig.StartNewTransaction();
				flag = DeleteEmployeeTypeDetailsRows(EmployeeTypeID, sqlTransaction);
				if (flag)
				{
					string commandText = "DELETE FROM Employee_Type WHERE TypeID = '" + EmployeeTypeID + "'";
					flag = Delete(commandText, null);
				}
				if (!flag)
				{
					return flag;
				}
				AddActivityLog("Employee Type", EmployeeTypeID, ActivityTypes.Delete, null);
				return flag;
			}
			catch
			{
				throw;
			}
		}

		internal bool DeleteEmployeeTypeDetailsRows(string EmployeeTypeID, SqlTransaction sqlTransaction)
		{
			bool result = true;
			try
			{
				string commandText = "DELETE FROM Employee_Type_Detail WHERE TypeID = '" + EmployeeTypeID + "'";
				result = Delete(commandText, sqlTransaction);
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

		public EmployeeTypeData GetEmployeeTypeByID(string id)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			CommandHelper commandHelper = null;
			commandHelper = new CommandHelper();
			commandHelper.FieldName = "TypeID";
			commandHelper.SqlFieldType = SqlDbType.NVarChar;
			commandHelper.FieldValue = id;
			commandHelper.TableName = "Employee_Type";
			sqlBuilder.AddCommandHelper(commandHelper);
			sqlBuilder.IsComparing = true;
			EmployeeTypeData employeeTypeData = new EmployeeTypeData();
			sqlBuilder.UseDistinct = false;
			FillDataSet(employeeTypeData, "Employee_Type", sqlBuilder);
			if (employeeTypeData == null || employeeTypeData.Tables.Count == 0 || employeeTypeData.Tables[0].Rows.Count == 0)
			{
				return employeeTypeData;
			}
			string textCommand = "SELECT ETD.TypeID,ETD.LeaveTypeID,ETD.OTTypeID,LT.LeaveTypeName,EOT.OverTimeName FROM Employee_Type_Detail ETD LEFT JOIN Leave_Type LT ON  ETD.LeaveTypeID =LT.LeaveTypeID \r\n                            LEFT JOIN Employee_OverTime EOT ON  EOT.OverTimeID =  ETD.OTTypeID                        \r\n                            WHERE TypeID='" + id + "'";
			FillDataSet(employeeTypeData, "Employee_Type_Detail", textCommand);
			return employeeTypeData;
		}

		public DataSet GetEmployeeTypeByFields(params string[] columns)
		{
			return GetEmployeeTypeByFields(null, isInactive: true, columns);
		}

		public DataSet GetEmployeeTypeByFields(string[] EmployeeTypeID, params string[] columns)
		{
			return GetEmployeeTypeByFields(EmployeeTypeID, isInactive: true, columns);
		}

		public DataSet GetEmployeeTypeByFields(string[] ids, bool isInactive, params string[] columns)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddTable("Employee_Type");
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
				commandHelper.FieldName = "TypeID";
				commandHelper.FieldValue = ids;
				commandHelper.SqlFieldType = SqlDbType.NVarChar;
				commandHelper.TableName = "Employee_Type";
				sqlBuilder.AddCommandHelper(commandHelper);
			}
			DataSet dataSet = new DataSet();
			dataSet.EnforceConstraints = false;
			sqlBuilder.UseDistinct = false;
			FillDataSet(dataSet, "Employee_Type", sqlBuilder);
			return dataSet;
		}

		public DataSet GetEmployeeTypeList()
		{
			DataSet dataSet = new DataSet();
			string textCommand = "SELECT TypeID [Code],TypeName [Name]\r\n                           FROM Employee_Type ";
			FillDataSet(dataSet, "Employee_Type", textCommand);
			return dataSet;
		}

		public DataSet GetEmployeeTypeComboList()
		{
			DataSet dataSet = new DataSet();
			string textCommand = "SELECT TypeID [Code],TypeName [Name]\r\n                           FROM Employee_Type ORDER BY TypeID,TypeName";
			FillDataSet(dataSet, "Employee_Type", textCommand);
			return dataSet;
		}
	}
}
