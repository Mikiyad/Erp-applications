using Micromind.Common;
using Micromind.Common.Data;
using Micromind.Common.Libraries;
using System;
using System.Data;
using System.Data.SqlClient;

namespace Micromind.Data
{
	public sealed class Deduction : StoreObject
	{
		private const string DEDUCTIONID_PARM = "@DeductionID";

		private const string DEDUCTIONNAME_PARM = "@DeductionName";

		private const string INACTIVE_PARM = "@Inactive";

		private const string ACCOUNTID_PARM = "@AccountID";

		public const string NOTE_PARM = "@Note";

		public const string DEDUCTION_TABLE = "Deduction";

		private const string CREATEDBY_PARM = "@CreatedBy";

		private const string DATECREATED_PARM = "@DateCreated";

		private const string UPDATEDBY_PARM = "@UpdatedBy";

		private const string DATEUPDATED_PARM = "@DateUpdated";

		public Deduction(Config config)
			: base(config)
		{
		}

		private string GetInsertUpdateText(bool isUpdate)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddInsertUpdateParameters("Deduction", new FieldValue("DeductionID", "@DeductionID", isUpdateConditionField: true), new FieldValue("DeductionName", "@DeductionName"), new FieldValue("Inactive", "@Inactive"), new FieldValue("AccountID", "@AccountID"), new FieldValue("Note", "@Note"));
			if (isUpdate)
			{
				sqlBuilder.AddInsertUpdateParameters("Deduction", new FieldValue("DateUpdated", "@DateUpdated", isUpdateConditionField: true, checkForNullValue: true));
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
			parameters.Add("@DeductionID", SqlDbType.NVarChar);
			parameters.Add("@DeductionName", SqlDbType.NVarChar);
			parameters.Add("@AccountID", SqlDbType.NVarChar);
			parameters.Add("@Inactive", SqlDbType.Bit);
			parameters.Add("@Note", SqlDbType.NVarChar);
			parameters["@DeductionID"].SourceColumn = "DeductionID";
			parameters["@DeductionName"].SourceColumn = "DeductionName";
			parameters["@AccountID"].SourceColumn = "AccountID";
			parameters["@Inactive"].SourceColumn = "Inactive";
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

		public bool InsertDeduction(DeductionData accountDeductionData)
		{
			bool result = true;
			SqlCommand insertUpdateCommand = GetInsertUpdateCommand(isUpdate: false);
			SqlTransaction sqlTransaction = null;
			try
			{
				sqlTransaction = (insertUpdateCommand.Transaction = base.DBConfig.StartNewTransaction());
				result = Insert(accountDeductionData, "Deduction", insertUpdateCommand);
				string text = accountDeductionData.DeductionTable.Rows[0]["DeductionID"].ToString();
				AddActivityLog("Deduction", text, ActivityTypes.Add, sqlTransaction);
				UpdateTableRowInsertUpdateInfo("Deduction", "DeductionID", text, sqlTransaction, isInsert: true);
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

		public bool UpdateDeduction(DeductionData accountDeductionData)
		{
			bool flag = true;
			SqlCommand insertUpdateCommand = GetInsertUpdateCommand(isUpdate: true);
			SqlTransaction sqlTransaction = null;
			try
			{
				sqlTransaction = (insertUpdateCommand.Transaction = base.DBConfig.StartNewTransaction());
				flag = Update(accountDeductionData, "Deduction", insertUpdateCommand);
				if (!flag)
				{
					return flag;
				}
				object obj = accountDeductionData.DeductionTable.Rows[0]["DeductionID"];
				UpdateTableRowByID("Deduction", "DeductionID", "DateUpdated", obj, DateTime.Now, sqlTransaction);
				string entiyID = accountDeductionData.DeductionTable.Rows[0]["DeductionName"].ToString();
				AddActivityLog("Deduction", entiyID, ActivityTypes.Update, sqlTransaction);
				UpdateTableRowInsertUpdateInfo("Deduction", "DeductionID", obj, sqlTransaction, isInsert: false);
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

		public DeductionData GetDeduction()
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddTable("Deduction");
			DeductionData deductionData = new DeductionData();
			sqlBuilder.UseDistinct = false;
			FillDataSet(deductionData, "Deduction", sqlBuilder);
			return deductionData;
		}

		public bool DeleteDeduction(string deductionID)
		{
			bool flag = true;
			try
			{
				string commandText = "DELETE FROM Deduction WHERE DeductionID = '" + deductionID + "'";
				flag = Delete(commandText, null);
				if (!flag)
				{
					return flag;
				}
				AddActivityLog("Deduction", deductionID, ActivityTypes.Delete, null);
				return flag;
			}
			catch
			{
				throw;
			}
		}

		public DeductionData GetDeductionByID(string id)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			CommandHelper commandHelper = null;
			commandHelper = new CommandHelper();
			commandHelper.FieldName = "DeductionID";
			commandHelper.SqlFieldType = SqlDbType.NVarChar;
			commandHelper.FieldValue = id;
			commandHelper.TableName = "Deduction";
			sqlBuilder.AddCommandHelper(commandHelper);
			sqlBuilder.IsComparing = true;
			DeductionData deductionData = new DeductionData();
			sqlBuilder.UseDistinct = false;
			FillDataSet(deductionData, "Deduction", sqlBuilder);
			return deductionData;
		}

		public DataSet GetDeductionByFields(params string[] columns)
		{
			return GetDeductionByFields(null, isInactive: true, columns);
		}

		public DataSet GetDeductionByFields(string[] deductionID, params string[] columns)
		{
			return GetDeductionByFields(deductionID, isInactive: true, columns);
		}

		public DataSet GetDeductionByFields(string[] ids, bool isInactive, params string[] columns)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddTable("Deduction");
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
				commandHelper.TableName = "Deduction";
				sqlBuilder.AddCommandHelper(commandHelper);
			}
			DataSet dataSet = new DataSet();
			dataSet.EnforceConstraints = false;
			sqlBuilder.UseDistinct = false;
			FillDataSet(dataSet, "Deduction", sqlBuilder);
			return dataSet;
		}

		public DataSet GetDeductionList()
		{
			DataSet dataSet = new DataSet();
			string textCommand = "SELECT DeductionID [Deduction Code],DeductionName [Deduction Name],Note,Inactive\r\n                           FROM Deduction ";
			FillDataSet(dataSet, "Deduction", textCommand);
			return dataSet;
		}

		public DataSet GetDeductionComboList()
		{
			DataSet dataSet = new DataSet();
			string textCommand = "SELECT DeductionID [Code],DeductionName [Name]\r\n                           FROM Deduction ORDER BY DeductionID,DeductionName";
			FillDataSet(dataSet, "Deduction", textCommand);
			return dataSet;
		}
	}
}
