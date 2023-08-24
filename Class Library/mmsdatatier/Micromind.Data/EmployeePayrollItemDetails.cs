using Micromind.Common;
using Micromind.Common.Data;
using Micromind.Common.Libraries;
using System;
using System.Data;
using System.Data.SqlClient;

namespace Micromind.Data
{
	public sealed class EmployeePayrollItemDetails : StoreObject
	{
		private const string EMPLOYEEPAYROLLITEMDETAIL_TABLE = "Employee_PayrollItem_Detail";

		private const string EMPLOYEEID_PARM = "@EmployeeID";

		private const string PAYTYPE_PARM = "@PayType";

		private const string PAYROLLITEMID_PARM = "@PayrollItemID";

		private const string STARTDATE_PARM = "@StartDate";

		private const string ENDDATE_PARM = "@EndDate";

		private const string AMOUNT_PARM = "@Amount";

		private const string LASTAMOUNT_PARM = "@LastAmount";

		private const string ROWINDEX_PARM = "@RowIndex";

		private const string CREATEDBY_PARM = "@CreatedBy";

		private const string DATECREATED_PARM = "@DateCreated";

		private const string UPDATEDBY_PARM = "@UpdatedBy";

		private const string DATEUPDATED_PARM = "@DateUpdated";

		public EmployeePayrollItemDetails(Config config)
			: base(config)
		{
		}

		private string GetInsertUpdateText(bool isUpdate)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddInsertUpdateParameters("Employee_PayrollItem_Detail", new FieldValue("EmployeeID", "@EmployeeID", isUpdateConditionField: true), new FieldValue("PayrollItemID", "@PayrollItemID", isUpdateConditionField: true), new FieldValue("PayType", "@PayType"), new FieldValue("StartDate", "@StartDate"), new FieldValue("EndDate", "@EndDate"), new FieldValue("RowIndex", "@RowIndex"), new FieldValue("Amount", "@Amount"), new FieldValue("LastAmount", "@LastAmount"));
			if (isUpdate)
			{
				sqlBuilder.AddInsertUpdateParameters("Employee_PayrollItem_Detail", new FieldValue("DateUpdated", "@DateUpdated", isUpdateConditionField: true, checkForNullValue: true));
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
			parameters.Add("@PayrollItemID", SqlDbType.NVarChar);
			parameters.Add("@PayType", SqlDbType.NVarChar);
			parameters.Add("@StartDate", SqlDbType.SmallDateTime);
			parameters.Add("@EndDate", SqlDbType.SmallDateTime);
			parameters.Add("@Amount", SqlDbType.Money);
			parameters.Add("@LastAmount", SqlDbType.Money);
			parameters.Add("@RowIndex", SqlDbType.SmallInt);
			parameters["@EmployeeID"].SourceColumn = "EmployeeID";
			parameters["@PayrollItemID"].SourceColumn = "PayrollItemID";
			parameters["@PayType"].SourceColumn = "PayType";
			parameters["@StartDate"].SourceColumn = "StartDate";
			parameters["@EndDate"].SourceColumn = "EndDate";
			parameters["@Amount"].SourceColumn = "Amount";
			parameters["@LastAmount"].SourceColumn = "LastAmount";
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

		public bool InsertEmployeePayrollItem(EmployeePayrollItemDetailData accountEmployeePayrollItemDetailData)
		{
			bool result = true;
			SqlCommand insertUpdateCommand = GetInsertUpdateCommand(isUpdate: false);
			SqlTransaction sqlTransaction = null;
			try
			{
				string employeeID = accountEmployeePayrollItemDetailData.EmployeePayrollItemDetailTable.Rows[0]["EmployeeID"].ToString();
				sqlTransaction = base.DBConfig.StartNewTransaction();
				DeleteEmployeePayrollItems(sqlTransaction, employeeID);
				insertUpdateCommand.Transaction = sqlTransaction;
				result = Insert(accountEmployeePayrollItemDetailData, "Employee_PayrollItem_Detail", insertUpdateCommand);
				string text = accountEmployeePayrollItemDetailData.EmployeePayrollItemDetailTable.Rows[0]["PayrollItemID"].ToString();
				AddActivityLog("Employee Payroll Item", text, ActivityTypes.Add, sqlTransaction);
				UpdateTableRowInsertUpdateInfo("Employee_PayrollItem_Detail", "PayrollItemID", text, sqlTransaction, isInsert: true);
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

		public bool UpdateEmployeePayrollItem(EmployeePayrollItemDetailData accountEmployeePayrollItemDetailData)
		{
			bool flag = true;
			SqlCommand insertUpdateCommand = GetInsertUpdateCommand(isUpdate: true);
			SqlTransaction sqlTransaction = null;
			try
			{
				string employeeID = accountEmployeePayrollItemDetailData.EmployeePayrollItemDetailTable.Rows[0]["EmployeeID"].ToString();
				sqlTransaction = base.DBConfig.StartNewTransaction();
				DeleteEmployeePayrollItems(sqlTransaction, employeeID);
				insertUpdateCommand.Transaction = sqlTransaction;
				flag = Update(accountEmployeePayrollItemDetailData, "Employee_PayrollItem_Detail", insertUpdateCommand);
				if (!flag)
				{
					return flag;
				}
				object obj = accountEmployeePayrollItemDetailData.EmployeePayrollItemDetailTable.Rows[0]["PayrollItemID"];
				UpdateTableRowByID("Employee_PayrollItem_Detail", "PayrollItemID", "DateUpdated", obj, DateTime.Now, sqlTransaction);
				string entiyID = accountEmployeePayrollItemDetailData.EmployeePayrollItemDetailTable.Rows[0]["PayrollItemID"].ToString();
				AddActivityLog("Employee Payroll Item", entiyID, ActivityTypes.Update, sqlTransaction);
				UpdateTableRowInsertUpdateInfo("Employee_PayrollItem_Detail", "PayrollItemID", obj, sqlTransaction, isInsert: false);
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

		internal bool DeleteEmployeePayrollItems(SqlTransaction sqlTransaction, string employeeID, bool isLog = true)
		{
			bool flag = true;
			try
			{
				string commandText = "DELETE FROM Employee_PayrollItem_Detail WHERE EmployeeID = '" + employeeID + "'";
				flag = Delete(commandText, sqlTransaction);
				if (!(flag && isLog))
				{
					return flag;
				}
				AddActivityLog("Employee Payroll Item", employeeID, ActivityTypes.Delete, null);
				return flag;
			}
			catch
			{
				throw;
			}
		}

		public EmployeePayrollItemDetailData GetEmployeePayrollItem()
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddTable("Employee_PayrollItem_Detail");
			EmployeePayrollItemDetailData employeePayrollItemDetailData = new EmployeePayrollItemDetailData();
			sqlBuilder.UseDistinct = false;
			FillDataSet(employeePayrollItemDetailData, "Employee_PayrollItem_Detail", sqlBuilder);
			return employeePayrollItemDetailData;
		}

		public EmployeePayrollItemDetailData GetEmployeePayrollItemsByEmployeeID(string employeeID)
		{
			EmployeePayrollItemDetailData employeePayrollItemDetailData = new EmployeePayrollItemDetailData();
			string textCommand = "Select EmployeeID,EAD.PayrollItemID,PayType,PayrollItemName,StartDate,EndDate,Amount \r\n                            FROM Employee_PayrollItem_Detail EAD INNER JOIN\r\n                            PayrollItem ON PayrollItem.PayrollItemID=EAD.PayrollItemID\r\n                            WHERE EmployeeID='" + employeeID + "' ORDER BY RowIndex";
			FillDataSet(employeePayrollItemDetailData, "Employee_PayrollItem_Detail", textCommand);
			return employeePayrollItemDetailData;
		}

		public bool DeleteEmployeePayrollItem(string employeePayrollItemID)
		{
			bool flag = true;
			try
			{
				string commandText = "DELETE FROM Employee_PayrollItem_Detail WHERE PayrollItemID = '" + employeePayrollItemID + "'";
				flag = Delete(commandText, null);
				if (!flag)
				{
					return flag;
				}
				AddActivityLog("Employee Payroll Item", employeePayrollItemID, ActivityTypes.Delete, null);
				return flag;
			}
			catch
			{
				throw;
			}
		}

		public EmployeePayrollItemDetailData GetEmployeePayrollItemByID(string id)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			CommandHelper commandHelper = null;
			commandHelper = new CommandHelper();
			commandHelper.FieldName = "PayrollItemID";
			commandHelper.SqlFieldType = SqlDbType.NVarChar;
			commandHelper.FieldValue = id;
			commandHelper.TableName = "Employee_PayrollItem_Detail";
			sqlBuilder.AddCommandHelper(commandHelper);
			sqlBuilder.IsComparing = true;
			EmployeePayrollItemDetailData employeePayrollItemDetailData = new EmployeePayrollItemDetailData();
			sqlBuilder.UseDistinct = false;
			FillDataSet(employeePayrollItemDetailData, "Employee_PayrollItem_Detail", sqlBuilder);
			return employeePayrollItemDetailData;
		}

		public DataSet GetEmployeePayrollItemByFields(params string[] columns)
		{
			return GetEmployeePayrollItemByFields(null, isInactive: true, columns);
		}

		public DataSet GetEmployeePayrollItemByFields(string[] employeePayrollItemID, params string[] columns)
		{
			return GetEmployeePayrollItemByFields(employeePayrollItemID, isInactive: true, columns);
		}

		public DataSet GetEmployeePayrollItemByFields(string[] ids, bool isInactive, params string[] columns)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddTable("Employee_PayrollItem_Detail");
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
				commandHelper.FieldName = "PayrollItemID";
				commandHelper.FieldValue = ids;
				commandHelper.SqlFieldType = SqlDbType.NVarChar;
				commandHelper.TableName = "Employee_PayrollItem_Detail";
				sqlBuilder.AddCommandHelper(commandHelper);
			}
			DataSet dataSet = new DataSet();
			dataSet.EnforceConstraints = false;
			sqlBuilder.UseDistinct = false;
			FillDataSet(dataSet, "Employee_PayrollItem_Detail", sqlBuilder);
			return dataSet;
		}

		public DataSet GetEmployeePayrollItemList()
		{
			return null;
		}

		public DataSet GetEmployeePayrollItemComboList()
		{
			return null;
		}
	}
}
