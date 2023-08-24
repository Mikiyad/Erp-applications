using Micromind.Common;
using Micromind.Common.Data;
using Micromind.Common.Libraries;
using System;
using System.Data;
using System.Data.SqlClient;

namespace Micromind.Data
{
	public sealed class EmployeeLoanType : StoreObject
	{
		private const string EMPLOYEELOANTYPEID_PARM = "@LoanTypeID";

		private const string EMPLOYEELOANTYPENAME_PARM = "@LoanTypeName";

		private const string INACTIVE_PARM = "@Inactive";

		public const string NOTE_PARM = "@Note";

		public const string ACCOUNTID_PARM = "@AccountID";

		public const string EMPLOYEELOANTYPE_TABLE = "Employee_Loan_Type";

		private const string CREATEDBY_PARM = "@CreatedBy";

		private const string DATECREATED_PARM = "@DateCreated";

		private const string UPDATEDBY_PARM = "@UpdatedBy";

		private const string DATEUPDATED_PARM = "@DateUpdated";

		public EmployeeLoanType(Config config)
			: base(config)
		{
		}

		private string GetInsertUpdateText(bool isUpdate)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddInsertUpdateParameters("Employee_Loan_Type", new FieldValue("LoanTypeID", "@LoanTypeID", isUpdateConditionField: true), new FieldValue("LoanTypeName", "@LoanTypeName"), new FieldValue("Inactive", "@Inactive"), new FieldValue("AccountID", "@AccountID"), new FieldValue("Note", "@Note"));
			if (isUpdate)
			{
				sqlBuilder.AddInsertUpdateParameters("Employee_Loan_Type", new FieldValue("DateUpdated", "@DateUpdated", isUpdateConditionField: true, checkForNullValue: true));
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
			parameters.Add("@LoanTypeID", SqlDbType.NVarChar);
			parameters.Add("@LoanTypeName", SqlDbType.NVarChar);
			parameters.Add("@Inactive", SqlDbType.Bit);
			parameters.Add("@AccountID", SqlDbType.NVarChar);
			parameters.Add("@Note", SqlDbType.NVarChar);
			parameters["@LoanTypeID"].SourceColumn = "LoanTypeID";
			parameters["@LoanTypeName"].SourceColumn = "LoanTypeName";
			parameters["@Inactive"].SourceColumn = "Inactive";
			parameters["@AccountID"].SourceColumn = "AccountID";
			parameters["@Note"].SourceColumn = "Note";
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

		public bool InsertEmployeeLoanType(EmployeeLoanTypeData accountEmployeeLoanTypeData)
		{
			bool result = true;
			SqlCommand insertUpdateCommand = GetInsertUpdateCommand(isUpdate: false);
			SqlTransaction sqlTransaction = null;
			try
			{
				sqlTransaction = (insertUpdateCommand.Transaction = base.DBConfig.StartNewTransaction());
				result = Insert(accountEmployeeLoanTypeData, "Employee_Loan_Type", insertUpdateCommand);
				string text = accountEmployeeLoanTypeData.EmployeeLoanTypeTable.Rows[0]["LoanTypeID"].ToString();
				AddActivityLog("Employee Loan Type", text, ActivityTypes.Add, sqlTransaction);
				UpdateTableRowInsertUpdateInfo("Employee_Loan_Type", "LoanTypeID", text, sqlTransaction, isInsert: true);
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

		public bool UpdateEmployeeLoanType(EmployeeLoanTypeData accountEmployeeLoanTypeData)
		{
			bool flag = true;
			SqlCommand insertUpdateCommand = GetInsertUpdateCommand(isUpdate: true);
			SqlTransaction sqlTransaction = null;
			try
			{
				sqlTransaction = (insertUpdateCommand.Transaction = base.DBConfig.StartNewTransaction());
				flag = Update(accountEmployeeLoanTypeData, "Employee_Loan_Type", insertUpdateCommand);
				if (!flag)
				{
					return flag;
				}
				object obj = accountEmployeeLoanTypeData.EmployeeLoanTypeTable.Rows[0]["LoanTypeID"];
				UpdateTableRowByID("Employee_Loan_Type", "LoanTypeID", "DateUpdated", obj, DateTime.Now, sqlTransaction);
				string entiyID = accountEmployeeLoanTypeData.EmployeeLoanTypeTable.Rows[0]["LoanTypeName"].ToString();
				AddActivityLog("Employee Loan Type", entiyID, ActivityTypes.Update, sqlTransaction);
				UpdateTableRowInsertUpdateInfo("Employee_Loan_Type", "LoanTypeID", obj, sqlTransaction, isInsert: false);
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

		public EmployeeLoanTypeData GetEmployeeLoanType()
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddTable("Employee_Loan_Type");
			EmployeeLoanTypeData employeeLoanTypeData = new EmployeeLoanTypeData();
			sqlBuilder.UseDistinct = false;
			FillDataSet(employeeLoanTypeData, "Employee_Loan_Type", sqlBuilder);
			return employeeLoanTypeData;
		}

		public bool DeleteEmployeeLoanType(string loanTypeID)
		{
			bool flag = true;
			try
			{
				string commandText = "DELETE FROM Employee_Loan_Type WHERE LoanTypeID = '" + loanTypeID + "'";
				flag = Delete(commandText, null);
				if (!flag)
				{
					return flag;
				}
				AddActivityLog("Employee Loan Type", loanTypeID, ActivityTypes.Delete, null);
				return flag;
			}
			catch
			{
				throw;
			}
		}

		public EmployeeLoanTypeData GetEmployeeLoanTypeByID(string id)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			CommandHelper commandHelper = null;
			commandHelper = new CommandHelper();
			commandHelper.FieldName = "LoanTypeID";
			commandHelper.SqlFieldType = SqlDbType.NVarChar;
			commandHelper.FieldValue = id;
			commandHelper.TableName = "Employee_Loan_Type";
			sqlBuilder.AddCommandHelper(commandHelper);
			sqlBuilder.IsComparing = true;
			EmployeeLoanTypeData employeeLoanTypeData = new EmployeeLoanTypeData();
			sqlBuilder.UseDistinct = false;
			FillDataSet(employeeLoanTypeData, "Employee_Loan_Type", sqlBuilder);
			return employeeLoanTypeData;
		}

		public DataSet GetEmployeeLoanTypeByFields(params string[] columns)
		{
			return GetEmployeeLoanTypeByFields(null, isInactive: true, columns);
		}

		public DataSet GetEmployeeLoanTypeByFields(string[] loanTypeID, params string[] columns)
		{
			return GetEmployeeLoanTypeByFields(loanTypeID, isInactive: true, columns);
		}

		public DataSet GetEmployeeLoanTypeByFields(string[] ids, bool isInactive, params string[] columns)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddTable("Employee_Loan_Type");
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
				commandHelper.FieldName = "LoanTypeID";
				commandHelper.FieldValue = ids;
				commandHelper.SqlFieldType = SqlDbType.NVarChar;
				commandHelper.TableName = "Employee_Loan_Type";
				sqlBuilder.AddCommandHelper(commandHelper);
			}
			DataSet dataSet = new DataSet();
			dataSet.EnforceConstraints = false;
			sqlBuilder.UseDistinct = false;
			FillDataSet(dataSet, "Employee_Loan_Type", sqlBuilder);
			return dataSet;
		}

		public DataSet GetEmployeeLoanTypeList()
		{
			DataSet dataSet = new DataSet();
			string textCommand = "SELECT LoanTypeID [Loan Type Code],LoanTypeName [Loan Type Name],Note,Inactive  \r\n                           FROM Employee_Loan_Type ";
			FillDataSet(dataSet, "Employee_Loan_Type", textCommand);
			return dataSet;
		}

		public DataSet GetEmployeeLoanTypeComboList()
		{
			DataSet dataSet = new DataSet();
			string textCommand = "SELECT LoanTypeID [Code], LoanTypeName [Name]\r\n                           FROM Employee_Loan_Type ORDER BY LoanTypeID, LoanTypeName";
			FillDataSet(dataSet, "Employee_Loan_Type", textCommand);
			return dataSet;
		}
	}
}
