using Micromind.Common;
using Micromind.Common.Data;
using Micromind.Common.Libraries;
using System;
using System.Data;
using System.Data.SqlClient;

namespace Micromind.Data
{
	public sealed class ExpenseCode : StoreObject
	{
		private const string EXPENSEID_PARM = "@ExpenseID";

		private const string EXPENSENAME_PARM = "@ExpenseName";

		private const string EXPENSECODE_TABLE = "Expense_Code";

		private const string DESCRIPTION_PARM = "@Description";

		private const string REAMRKS_PARM = "@Remarks";

		private const string ACCOUNTID_PARM = "@AccountID";

		private const string EXPENSETYPE_PARM = "@ExpenseType";

		private const string EXPENSERATE_PARM = "@ExpenseRate";

		private const string INACTIVE_PARM = "@Inactive";

		private const string TAXOPTION_PARM = "@TaxOption";

		private const string TAXGROUPID_PARM = "@TaxGroupID";

		private const string CREATEDBY_PARM = "@CreatedBy";

		private const string DATECREATED_PARM = "@DateCreated";

		private const string UPDATEDBY_PARM = "@UpdatedBy";

		private const string DATEUPDATED_PARM = "@DateUpdated";

		public ExpenseCode(Config config)
			: base(config)
		{
		}

		private string GetInsertUpdateText(bool isUpdate)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddInsertUpdateParameters("Expense_Code", new FieldValue("ExpenseID", "@ExpenseID", isUpdateConditionField: true), new FieldValue("Description", "@Description"), new FieldValue("Remarks", "@Remarks"), new FieldValue("AccountID", "@AccountID"), new FieldValue("ExpenseType", "@ExpenseType"), new FieldValue("ExpenseRate", "@ExpenseRate"), new FieldValue("TaxOption", "@TaxOption"), new FieldValue("TaxGroupID", "@TaxGroupID"), new FieldValue("Inactive", "@Inactive"), new FieldValue("ExpenseName", "@ExpenseName"));
			if (isUpdate)
			{
				sqlBuilder.AddInsertUpdateParameters("Expense_Code", new FieldValue("DateUpdated", "@DateUpdated", isUpdateConditionField: true, checkForNullValue: true));
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
			parameters.Add("@ExpenseID", SqlDbType.NVarChar);
			parameters.Add("@ExpenseName", SqlDbType.NVarChar);
			parameters.Add("@Description", SqlDbType.NVarChar);
			parameters.Add("@Remarks", SqlDbType.NVarChar);
			parameters.Add("@AccountID", SqlDbType.NVarChar);
			parameters.Add("@TaxOption", SqlDbType.TinyInt);
			parameters.Add("@TaxGroupID", SqlDbType.NVarChar);
			parameters.Add("@ExpenseType", SqlDbType.TinyInt);
			parameters.Add("@ExpenseRate", SqlDbType.Decimal);
			parameters.Add("@Inactive", SqlDbType.Bit);
			parameters["@ExpenseID"].SourceColumn = "ExpenseID";
			parameters["@ExpenseName"].SourceColumn = "ExpenseName";
			parameters["@Description"].SourceColumn = "Description";
			parameters["@Remarks"].SourceColumn = "Remarks";
			parameters["@AccountID"].SourceColumn = "AccountID";
			parameters["@TaxOption"].SourceColumn = "TaxOption";
			parameters["@TaxGroupID"].SourceColumn = "TaxGroupID";
			parameters["@ExpenseType"].SourceColumn = "ExpenseType";
			parameters["@ExpenseRate"].SourceColumn = "ExpenseRate";
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

		public bool InsertExpenseCode(ExpenseCodeData accountExpenseCodeData)
		{
			bool result = true;
			SqlCommand insertUpdateCommand = GetInsertUpdateCommand(isUpdate: false);
			SqlTransaction sqlTransaction = null;
			try
			{
				sqlTransaction = (insertUpdateCommand.Transaction = base.DBConfig.StartNewTransaction());
				result = Insert(accountExpenseCodeData, "Expense_Code", insertUpdateCommand);
				string text = accountExpenseCodeData.ExpenseCodeTable.Rows[0]["ExpenseID"].ToString();
				AddActivityLog("Expense", text, ActivityTypes.Add, sqlTransaction);
				UpdateTableRowInsertUpdateInfo("Expense_Code", "ExpenseID", text, sqlTransaction, isInsert: true);
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

		public bool UpdateExpenseCode(ExpenseCodeData accountExpenseCodeData)
		{
			bool flag = true;
			SqlCommand insertUpdateCommand = GetInsertUpdateCommand(isUpdate: true);
			SqlTransaction sqlTransaction = null;
			try
			{
				sqlTransaction = (insertUpdateCommand.Transaction = base.DBConfig.StartNewTransaction());
				flag = Update(accountExpenseCodeData, "Expense_Code", insertUpdateCommand);
				if (!flag)
				{
					return flag;
				}
				object obj = accountExpenseCodeData.ExpenseCodeTable.Rows[0]["ExpenseID"];
				UpdateTableRowByID("Expense_Code", "ExpenseID", "DateUpdated", obj, DateTime.Now, sqlTransaction);
				string entiyID = accountExpenseCodeData.ExpenseCodeTable.Rows[0]["ExpenseName"].ToString();
				AddActivityLog("ExpenseCode", entiyID, ActivityTypes.Update, sqlTransaction);
				UpdateTableRowInsertUpdateInfo("Expense_Code", "ExpenseID", obj, sqlTransaction, isInsert: false);
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

		public ExpenseCodeData GetExpenseCode()
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddTable("Expense_Code");
			ExpenseCodeData expenseCodeData = new ExpenseCodeData();
			sqlBuilder.UseDistinct = false;
			FillDataSet(expenseCodeData, "Expense_Code", sqlBuilder);
			return expenseCodeData;
		}

		public bool DeleteExpenseCode(string degreeID)
		{
			bool flag = true;
			try
			{
				string commandText = "DELETE FROM Expense_Code WHERE ExpenseID = '" + degreeID + "'";
				flag = Delete(commandText, null);
				if (!flag)
				{
					return flag;
				}
				AddActivityLog("Expense", degreeID, ActivityTypes.Delete, null);
				return flag;
			}
			catch
			{
				throw;
			}
		}

		public ExpenseCodeData GetExpenseCodeByID(string id)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			CommandHelper commandHelper = null;
			commandHelper = new CommandHelper();
			commandHelper.FieldName = "ExpenseID";
			commandHelper.SqlFieldType = SqlDbType.NVarChar;
			commandHelper.FieldValue = id;
			commandHelper.TableName = "Expense_Code";
			sqlBuilder.AddCommandHelper(commandHelper);
			sqlBuilder.IsComparing = true;
			ExpenseCodeData expenseCodeData = new ExpenseCodeData();
			sqlBuilder.UseDistinct = false;
			FillDataSet(expenseCodeData, "Expense_Code", sqlBuilder);
			return expenseCodeData;
		}

		public DataSet GetExpenseCodeByFields(params string[] columns)
		{
			return GetExpenseCodeByFields(null, isInactive: true, columns);
		}

		public DataSet GetExpenseCodeByFields(string[] degreeID, params string[] columns)
		{
			return GetExpenseCodeByFields(degreeID, isInactive: true, columns);
		}

		public DataSet GetExpenseCodeByFields(string[] ids, bool isInactive, params string[] columns)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddTable("Expense_Code");
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
				commandHelper.FieldName = "ExpenseID";
				commandHelper.FieldValue = ids;
				commandHelper.SqlFieldType = SqlDbType.NVarChar;
				commandHelper.TableName = "Expense_Code";
				sqlBuilder.AddCommandHelper(commandHelper);
			}
			DataSet dataSet = new DataSet();
			dataSet.EnforceConstraints = false;
			sqlBuilder.UseDistinct = false;
			FillDataSet(dataSet, "Expense_Code", sqlBuilder);
			return dataSet;
		}

		public string GetExpenseAccountID(string expenseID, SqlTransaction sqlTransaction)
		{
			new DataSet();
			string exp = "SELECT AccountID  \r\n                           FROM Expense_Code WHERE ExpenseID = '" + expenseID + "'";
			return ExecuteScalar(exp, sqlTransaction).ToString();
		}

		public DataSet GetExpenseCodeList()
		{
			DataSet dataSet = new DataSet();
			string textCommand = "SELECT ExpenseID [Expense Code],ExpenseName [Expense Name]\r\n                           FROM Expense_Code ";
			FillDataSet(dataSet, "Expense_Code", textCommand);
			return dataSet;
		}

		public DataSet GetExpenseCodeComboList()
		{
			DataSet dataSet = new DataSet();
			string textCommand = "SELECT ExpenseID [Code], ExpenseName [Name],ExpenseType,AccountID, ExpenseRate,TaxOption,TaxGroupID\r\n                           FROM Expense_Code ORDER BY ExpenseID, ExpenseName";
			FillDataSet(dataSet, "Expense_Code", textCommand);
			return dataSet;
		}
	}
}
