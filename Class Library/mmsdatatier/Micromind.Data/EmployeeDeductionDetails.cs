using Micromind.Common;
using Micromind.Common.Data;
using Micromind.Common.Libraries;
using System;
using System.Data;
using System.Data.SqlClient;

namespace Micromind.Data
{
	public sealed class EmployeeDeductionDetails : StoreObject
	{
		private const string EMPLOYEEDEDUCTIONDETAIL_TABLE = "Employee_Deduction_Detail";

		private const string EMPLOYEEID_PARM = "@EmployeeID";

		private const string DEDUCTIONID_PARM = "@DeductionID";

		private const string STARTDATE_PARM = "@StartDate";

		private const string ENDDATE_PARM = "@EndDate";

		private const string REMARKS_PARM = "@Remarks";

		private const string AMOUNT_PARM = "@Amount";

		private const string ROWINDEX_PARM = "@RowIndex";

		private const string CREATEDBY_PARM = "@CreatedBy";

		private const string DATECREATED_PARM = "@DateCreated";

		private const string UPDATEDBY_PARM = "@UpdatedBy";

		private const string DATEUPDATED_PARM = "@DateUpdated";

		public EmployeeDeductionDetails(Config config)
			: base(config)
		{
		}

		private string GetInsertUpdateText(bool isUpdate)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddInsertUpdateParameters("Employee_Deduction_Detail", new FieldValue("EmployeeID", "@EmployeeID", isUpdateConditionField: true), new FieldValue("DeductionID", "@DeductionID", isUpdateConditionField: true), new FieldValue("StartDate", "@StartDate"), new FieldValue("EndDate", "@EndDate"), new FieldValue("Remarks", "@Remarks"), new FieldValue("RowIndex", "@RowIndex"), new FieldValue("Amount", "@Amount"));
			if (isUpdate)
			{
				sqlBuilder.AddInsertUpdateParameters("Employee_Deduction_Detail", new FieldValue("DateUpdated", "@DateUpdated", isUpdateConditionField: true, checkForNullValue: true));
			}
			if (isUpdate)
			{
				return sqlBuilder.GetUpdateExpression();
			}
			return sqlBuilder.GetInsertExpression();
		}

		internal SqlCommand GetInsertUpdateCommand(bool isUpdate)
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
			parameters.Add("@DeductionID", SqlDbType.NVarChar);
			parameters.Add("@StartDate", SqlDbType.SmallDateTime);
			parameters.Add("@EndDate", SqlDbType.SmallDateTime);
			parameters.Add("@Remarks", SqlDbType.NVarChar);
			parameters.Add("@Amount", SqlDbType.Money);
			parameters.Add("@RowIndex", SqlDbType.SmallInt);
			parameters["@EmployeeID"].SourceColumn = "EmployeeID";
			parameters["@DeductionID"].SourceColumn = "DeductionID";
			parameters["@StartDate"].SourceColumn = "StartDate";
			parameters["@EndDate"].SourceColumn = "EndDate";
			parameters["@Remarks"].SourceColumn = "Remarks";
			parameters["@Amount"].SourceColumn = "Amount";
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

		public bool InsertEmployeeDeduction(EmployeeDeductionDetailData accountEmployeeDeductionDetailData)
		{
			bool result = true;
			SqlCommand insertUpdateCommand = GetInsertUpdateCommand(isUpdate: false);
			SqlTransaction sqlTransaction = null;
			try
			{
				string employeeID = accountEmployeeDeductionDetailData.EmployeeDeductionDetailTable.Rows[0]["EmployeeID"].ToString();
				sqlTransaction = base.DBConfig.StartNewTransaction();
				DeleteEmployeeDeductions(sqlTransaction, employeeID);
				insertUpdateCommand.Transaction = sqlTransaction;
				result = Insert(accountEmployeeDeductionDetailData, "Employee_Deduction_Detail", insertUpdateCommand);
				string text = accountEmployeeDeductionDetailData.EmployeeDeductionDetailTable.Rows[0]["DeductionID"].ToString();
				AddActivityLog(" Employee Deduction", text, ActivityTypes.Add, sqlTransaction);
				UpdateTableRowInsertUpdateInfo("Employee_Deduction_Detail", "DeductionID", text, sqlTransaction, isInsert: true);
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

		public bool UpdateEmployeeDeduction(EmployeeDeductionDetailData accountEmployeeDeductionDetailData)
		{
			bool flag = true;
			SqlCommand insertUpdateCommand = GetInsertUpdateCommand(isUpdate: true);
			SqlTransaction sqlTransaction = null;
			try
			{
				string employeeID = accountEmployeeDeductionDetailData.EmployeeDeductionDetailTable.Rows[0]["EmployeeID"].ToString();
				sqlTransaction = base.DBConfig.StartNewTransaction();
				DeleteEmployeeDeductions(sqlTransaction, employeeID);
				insertUpdateCommand.Transaction = sqlTransaction;
				flag = Update(accountEmployeeDeductionDetailData, "Employee_Deduction_Detail", insertUpdateCommand);
				if (!flag)
				{
					return flag;
				}
				object obj = accountEmployeeDeductionDetailData.EmployeeDeductionDetailTable.Rows[0]["DeductionID"];
				UpdateTableRowByID("Employee_Deduction_Detail", "DeductionID", "DateUpdated", obj, DateTime.Now, sqlTransaction);
				string entiyID = accountEmployeeDeductionDetailData.EmployeeDeductionDetailTable.Rows[0]["DeductionID"].ToString();
				AddActivityLog("Employee Deduction", entiyID, ActivityTypes.Update, sqlTransaction);
				UpdateTableRowInsertUpdateInfo("Employee_Deduction_Detail", "DeductionID", obj, sqlTransaction, isInsert: false);
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

		internal bool DeleteEmployeeDeductions(SqlTransaction sqlTransaction, string employeeID, bool isLog = true)
		{
			bool flag = true;
			try
			{
				string commandText = "DELETE FROM Employee_Deduction_Detail WHERE EmployeeID = '" + employeeID + "'";
				flag = Delete(commandText, sqlTransaction);
				if (!(flag && isLog))
				{
					return flag;
				}
				AddActivityLog("Employee Deduction", employeeID, ActivityTypes.Delete, null);
				return flag;
			}
			catch
			{
				throw;
			}
		}

		public EmployeeDeductionDetailData GetEmployeeDeduction()
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddTable("Employee_Deduction_Detail");
			EmployeeDeductionDetailData employeeDeductionDetailData = new EmployeeDeductionDetailData();
			sqlBuilder.UseDistinct = false;
			FillDataSet(employeeDeductionDetailData, "Employee_Deduction_Detail", sqlBuilder);
			return employeeDeductionDetailData;
		}

		public EmployeeDeductionDetailData GetEmployeeDeductionsByEmployeeID(string employeeID)
		{
			EmployeeDeductionDetailData employeeDeductionDetailData = new EmployeeDeductionDetailData();
			string textCommand = "Select EmployeeID,EDD.DeductionID,DeductionName,StartDate,EndDate,Remarks,Amount \r\n                            FROM Employee_Deduction_Detail EDD INNER JOIN\r\n                            Deduction ON Deduction.DeductionID=EDD.DeductionID\r\n                            WHERE EmployeeID='" + employeeID + "' ORDER BY RowIndex";
			FillDataSet(employeeDeductionDetailData, "Employee_Deduction_Detail", textCommand);
			return employeeDeductionDetailData;
		}

		public bool DeleteEmployeeDeduction(string employeeDeductionID)
		{
			bool flag = true;
			try
			{
				string commandText = "DELETE FROM Employee_Deduction_Detail WHERE DeductionID = '" + employeeDeductionID + "'";
				flag = Delete(commandText, null);
				if (!flag)
				{
					return flag;
				}
				AddActivityLog("Employee Deduction", employeeDeductionID, ActivityTypes.Delete, null);
				return flag;
			}
			catch
			{
				throw;
			}
		}

		public EmployeeDeductionDetailData GetEmployeeDeductionByID(string id)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			CommandHelper commandHelper = null;
			commandHelper = new CommandHelper();
			commandHelper.FieldName = "DeductionID";
			commandHelper.SqlFieldType = SqlDbType.NVarChar;
			commandHelper.FieldValue = id;
			commandHelper.TableName = "Employee_Deduction_Detail";
			sqlBuilder.AddCommandHelper(commandHelper);
			sqlBuilder.IsComparing = true;
			EmployeeDeductionDetailData employeeDeductionDetailData = new EmployeeDeductionDetailData();
			sqlBuilder.UseDistinct = false;
			FillDataSet(employeeDeductionDetailData, "Employee_Deduction_Detail", sqlBuilder);
			return employeeDeductionDetailData;
		}

		public DataSet GetEmployeeDeductionByFields(params string[] columns)
		{
			return GetEmployeeDeductionByFields(null, isInactive: true, columns);
		}

		public DataSet GetEmployeeDeductionByFields(string[] employeeDeductionID, params string[] columns)
		{
			return GetEmployeeDeductionByFields(employeeDeductionID, isInactive: true, columns);
		}

		public DataSet GetEmployeeDeductionByFields(string[] ids, bool isInactive, params string[] columns)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddTable("Employee_Deduction_Detail");
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
				commandHelper.FieldName = "DeductionID";
				commandHelper.FieldValue = ids;
				commandHelper.SqlFieldType = SqlDbType.NVarChar;
				commandHelper.TableName = "Employee_Deduction_Detail";
				sqlBuilder.AddCommandHelper(commandHelper);
			}
			DataSet dataSet = new DataSet();
			dataSet.EnforceConstraints = false;
			sqlBuilder.UseDistinct = false;
			FillDataSet(dataSet, "Employee_Deduction_Detail", sqlBuilder);
			return dataSet;
		}

		public DataSet GetEmployeeDeductionList()
		{
			return null;
		}

		public DataSet GetEmployeeDeductionComboList()
		{
			return null;
		}
	}
}
