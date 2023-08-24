using Micromind.Common;
using Micromind.Common.Data;
using Micromind.Common.Libraries;
using System;
using System.Data;
using System.Data.SqlClient;

namespace Micromind.Data
{
	public sealed class EmployeeBenefitDetails : StoreObject
	{
		private const string EMPLOYEEBENEFITDETAIL_TABLE = "Employee_Benefit_Detail";

		private const string EMPLOYEEID_PARM = "@EmployeeID";

		private const string BENEFITID_PARM = "@BenefitID";

		private const string STARTDATE_PARM = "@StartDate";

		private const string ENDDATE_PARM = "@EndDate";

		private const string REMARKS_PARM = "@Remarks";

		private const string AMOUNT_PARM = "@Amount";

		private const string LASTAMOUNT_PARM = "@LastAmount";

		private const string ROWINDEX_PARM = "@RowIndex";

		private const string CREATEDBY_PARM = "@CreatedBy";

		private const string DATECREATED_PARM = "@DateCreated";

		private const string UPDATEDBY_PARM = "@UpdatedBy";

		private const string DATEUPDATED_PARM = "@DateUpdated";

		public EmployeeBenefitDetails(Config config)
			: base(config)
		{
		}

		private string GetInsertUpdateText(bool isUpdate)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddInsertUpdateParameters("Employee_Benefit_Detail", new FieldValue("EmployeeID", "@EmployeeID", isUpdateConditionField: true), new FieldValue("BenefitID", "@BenefitID", isUpdateConditionField: true), new FieldValue("StartDate", "@StartDate"), new FieldValue("EndDate", "@EndDate"), new FieldValue("Remarks", "@Remarks"), new FieldValue("RowIndex", "@RowIndex"), new FieldValue("Amount", "@Amount"), new FieldValue("LastAmount", "@LastAmount"));
			if (isUpdate)
			{
				sqlBuilder.AddInsertUpdateParameters("Employee_Benefit_Detail", new FieldValue("DateUpdated", "@DateUpdated", isUpdateConditionField: true, checkForNullValue: true));
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
			parameters.Add("@BenefitID", SqlDbType.NVarChar);
			parameters.Add("@StartDate", SqlDbType.SmallDateTime);
			parameters.Add("@EndDate", SqlDbType.SmallDateTime);
			parameters.Add("@Remarks", SqlDbType.NVarChar);
			parameters.Add("@Amount", SqlDbType.Money);
			parameters.Add("@LastAmount", SqlDbType.Money);
			parameters.Add("@RowIndex", SqlDbType.SmallInt);
			parameters["@EmployeeID"].SourceColumn = "EmployeeID";
			parameters["@BenefitID"].SourceColumn = "BenefitID";
			parameters["@StartDate"].SourceColumn = "StartDate";
			parameters["@EndDate"].SourceColumn = "EndDate";
			parameters["@Remarks"].SourceColumn = "Remarks";
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

		public bool InsertEmployeeBenefit(EmployeeBenefitDetailData accountEmployeeBenefitDetailData)
		{
			bool result = true;
			SqlCommand insertUpdateCommand = GetInsertUpdateCommand(isUpdate: false);
			SqlTransaction sqlTransaction = null;
			try
			{
				string employeeID = accountEmployeeBenefitDetailData.EmployeeBenefitDetailTable.Rows[0]["EmployeeID"].ToString();
				sqlTransaction = base.DBConfig.StartNewTransaction();
				DeleteEmployeeBenefits(sqlTransaction, employeeID);
				insertUpdateCommand.Transaction = sqlTransaction;
				result = Insert(accountEmployeeBenefitDetailData, "Employee_Benefit_Detail", insertUpdateCommand);
				string text = accountEmployeeBenefitDetailData.EmployeeBenefitDetailTable.Rows[0]["BenefitID"].ToString();
				AddActivityLog("Employee Benefit", text, ActivityTypes.Add, sqlTransaction);
				UpdateTableRowInsertUpdateInfo("Employee_Benefit_Detail", "BenefitID", text, sqlTransaction, isInsert: true);
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

		public bool UpdateEmployeeBenefit(EmployeeBenefitDetailData accountEmployeeBenefitDetailData)
		{
			bool flag = true;
			SqlCommand insertUpdateCommand = GetInsertUpdateCommand(isUpdate: true);
			SqlTransaction sqlTransaction = null;
			try
			{
				string employeeID = accountEmployeeBenefitDetailData.EmployeeBenefitDetailTable.Rows[0]["EmployeeID"].ToString();
				sqlTransaction = base.DBConfig.StartNewTransaction();
				DeleteEmployeeBenefits(sqlTransaction, employeeID);
				insertUpdateCommand.Transaction = sqlTransaction;
				flag = Update(accountEmployeeBenefitDetailData, "Employee_Benefit_Detail", insertUpdateCommand);
				if (!flag)
				{
					return flag;
				}
				object obj = accountEmployeeBenefitDetailData.EmployeeBenefitDetailTable.Rows[0]["BenefitID"];
				UpdateTableRowByID("Employee_Benefit_Detail", "BenefitID", "DateUpdated", obj, DateTime.Now, sqlTransaction);
				string entiyID = accountEmployeeBenefitDetailData.EmployeeBenefitDetailTable.Rows[0]["BenefitID"].ToString();
				AddActivityLog("Employee Benefit", entiyID, ActivityTypes.Update, sqlTransaction);
				UpdateTableRowInsertUpdateInfo("Employee_Benefit_Detail", "BenefitID", obj, sqlTransaction, isInsert: false);
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

		internal bool DeleteEmployeeBenefits(SqlTransaction sqlTransaction, string employeeID)
		{
			bool flag = true;
			try
			{
				string commandText = "DELETE FROM Employee_Benefit_Detail WHERE EmployeeID = '" + employeeID + "'";
				flag = Delete(commandText, sqlTransaction);
				if (!flag)
				{
					return flag;
				}
				AddActivityLog("Employee Benefit", employeeID, ActivityTypes.Delete, null);
				return flag;
			}
			catch
			{
				throw;
			}
		}

		public EmployeeBenefitDetailData GetEmployeeBenefit()
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddTable("Employee_Benefit_Detail");
			EmployeeBenefitDetailData employeeBenefitDetailData = new EmployeeBenefitDetailData();
			sqlBuilder.UseDistinct = false;
			FillDataSet(employeeBenefitDetailData, "Employee_Benefit_Detail", sqlBuilder);
			return employeeBenefitDetailData;
		}

		public EmployeeBenefitDetailData GetEmployeeBenefitsByEmployeeID(string employeeID)
		{
			EmployeeBenefitDetailData employeeBenefitDetailData = new EmployeeBenefitDetailData();
			string textCommand = "Select EmployeeID,EBD.BenefitID,BenefitName,StartDate,EndDate,Remarks,Amount \r\n                            FROM Employee_Benefit_Detail EBD INNER JOIN\r\n                            Benefit ON Benefit.BenefitID=EBD.BenefitID\r\n                            WHERE EmployeeID='" + employeeID + "' ORDER BY RowIndex";
			FillDataSet(employeeBenefitDetailData, "Employee_Benefit_Detail", textCommand);
			return employeeBenefitDetailData;
		}

		public bool DeleteEmployeeBenefit(string employeeBenefitID)
		{
			bool flag = true;
			try
			{
				string commandText = "DELETE FROM Employee_Benefit_Detail WHERE BenefitID = '" + employeeBenefitID + "'";
				flag = Delete(commandText, null);
				if (!flag)
				{
					return flag;
				}
				AddActivityLog("Employee Benefit", employeeBenefitID, ActivityTypes.Delete, null);
				return flag;
			}
			catch
			{
				throw;
			}
		}

		public EmployeeBenefitDetailData GetEmployeeBenefitByID(string id)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			CommandHelper commandHelper = null;
			commandHelper = new CommandHelper();
			commandHelper.FieldName = "BenefitID";
			commandHelper.SqlFieldType = SqlDbType.NVarChar;
			commandHelper.FieldValue = id;
			commandHelper.TableName = "Employee_Benefit_Detail";
			sqlBuilder.AddCommandHelper(commandHelper);
			sqlBuilder.IsComparing = true;
			EmployeeBenefitDetailData employeeBenefitDetailData = new EmployeeBenefitDetailData();
			sqlBuilder.UseDistinct = false;
			FillDataSet(employeeBenefitDetailData, "Employee_Benefit_Detail", sqlBuilder);
			return employeeBenefitDetailData;
		}

		public DataSet GetEmployeeBenefitByFields(params string[] columns)
		{
			return GetEmployeeBenefitByFields(null, isInactive: true, columns);
		}

		public DataSet GetEmployeeBenefitByFields(string[] employeeBenefitID, params string[] columns)
		{
			return GetEmployeeBenefitByFields(employeeBenefitID, isInactive: true, columns);
		}

		public DataSet GetEmployeeBenefitByFields(string[] ids, bool isInactive, params string[] columns)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddTable("Employee_Benefit_Detail");
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
				commandHelper.FieldName = "BenefitID";
				commandHelper.FieldValue = ids;
				commandHelper.SqlFieldType = SqlDbType.NVarChar;
				commandHelper.TableName = "Employee_Benefit_Detail";
				sqlBuilder.AddCommandHelper(commandHelper);
			}
			DataSet dataSet = new DataSet();
			dataSet.EnforceConstraints = false;
			sqlBuilder.UseDistinct = false;
			FillDataSet(dataSet, "Employee_Benefit_Detail", sqlBuilder);
			return dataSet;
		}

		public DataSet GetEmployeeBenefitList()
		{
			return null;
		}

		public DataSet GetEmployeeBenefitComboList()
		{
			return null;
		}
	}
}
