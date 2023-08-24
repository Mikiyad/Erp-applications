using Micromind.Common;
using Micromind.Common.Data;
using Micromind.Common.Libraries;
using System;
using System.Data;
using System.Data.SqlClient;

namespace Micromind.Data
{
	public sealed class PropertyIncomeCode : StoreObject
	{
		private const string INCOMEID_PARM = "@IncomeID";

		private const string INCOMENAME_PARM = "@IncomeName";

		private const string PROPERTYINCOMECODE_TABLE = "PropertyIncome_Code";

		private const string DESCRIPTION_PARM = "@Description";

		private const string ACCOUNTID_PARM = "@AccountID";

		private const string INCOMETYPE_PARM = "@IncomeType";

		private const string INCOMERATE_PARM = "@IncomeRate";

		private const string INACTIVE_PARM = "@Inactive";

		public const string TAXOPTION_PARM = "@TaxOption";

		public const string TAXGROUPID_PARM = "@TaxGroupID";

		private const string CREATEDBY_PARM = "@CreatedBy";

		private const string DATECREATED_PARM = "@DateCreated";

		private const string UPDATEDBY_PARM = "@UpdatedBy";

		private const string DATEUPDATED_PARM = "@DateUpdated";

		public PropertyIncomeCode(Config config)
			: base(config)
		{
		}

		private string GetInsertUpdateText(bool isUpdate)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddInsertUpdateParameters("PropertyIncome_Code", new FieldValue("IncomeID", "@IncomeID", isUpdateConditionField: true), new FieldValue("Description", "@Description"), new FieldValue("AccountID", "@AccountID"), new FieldValue("IncomeType", "@IncomeType"), new FieldValue("IncomeRate", "@IncomeRate"), new FieldValue("Inactive", "@Inactive"), new FieldValue("TaxGroupID", "@TaxGroupID"), new FieldValue("TaxOption", "@TaxOption"), new FieldValue("IncomeName", "@IncomeName"));
			if (isUpdate)
			{
				sqlBuilder.AddInsertUpdateParameters("PropertyIncome_Code", new FieldValue("DateUpdated", "@DateUpdated", isUpdateConditionField: true, checkForNullValue: true));
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
			parameters.Add("@IncomeID", SqlDbType.NVarChar);
			parameters.Add("@IncomeName", SqlDbType.NVarChar);
			parameters.Add("@Description", SqlDbType.NVarChar);
			parameters.Add("@AccountID", SqlDbType.NVarChar);
			parameters.Add("@IncomeType", SqlDbType.TinyInt);
			parameters.Add("@IncomeRate", SqlDbType.Decimal);
			parameters.Add("@Inactive", SqlDbType.Bit);
			parameters.Add("@TaxOption", SqlDbType.TinyInt);
			parameters.Add("@TaxGroupID", SqlDbType.NVarChar);
			parameters["@IncomeID"].SourceColumn = "IncomeID";
			parameters["@IncomeName"].SourceColumn = "IncomeName";
			parameters["@Description"].SourceColumn = "Description";
			parameters["@AccountID"].SourceColumn = "AccountID";
			parameters["@IncomeType"].SourceColumn = "IncomeType";
			parameters["@IncomeRate"].SourceColumn = "IncomeRate";
			parameters["@Inactive"].SourceColumn = "Inactive";
			parameters["@TaxOption"].SourceColumn = "TaxOption";
			parameters["@TaxGroupID"].SourceColumn = "TaxGroupID";
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

		public bool InsertPropertyIncomeCode(PropertyIncomeCodeData accountPropertyIncomeCodeData)
		{
			bool result = true;
			SqlCommand insertUpdateCommand = GetInsertUpdateCommand(isUpdate: false);
			SqlTransaction sqlTransaction = null;
			try
			{
				sqlTransaction = (insertUpdateCommand.Transaction = base.DBConfig.StartNewTransaction());
				result = Insert(accountPropertyIncomeCodeData, "PropertyIncome_Code", insertUpdateCommand);
				string text = accountPropertyIncomeCodeData.PropertyIncomeCodeTable.Rows[0]["IncomeID"].ToString();
				AddActivityLog("Income", text, ActivityTypes.Add, sqlTransaction);
				UpdateTableRowInsertUpdateInfo("PropertyIncome_Code", "IncomeID", text, sqlTransaction, isInsert: true);
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

		public bool UpdatePropertyIncomeCode(PropertyIncomeCodeData accountPropertyIncomeCodeData)
		{
			bool flag = true;
			SqlCommand insertUpdateCommand = GetInsertUpdateCommand(isUpdate: true);
			SqlTransaction sqlTransaction = null;
			try
			{
				sqlTransaction = (insertUpdateCommand.Transaction = base.DBConfig.StartNewTransaction());
				flag = Update(accountPropertyIncomeCodeData, "PropertyIncome_Code", insertUpdateCommand);
				if (!flag)
				{
					return flag;
				}
				object obj = accountPropertyIncomeCodeData.PropertyIncomeCodeTable.Rows[0]["IncomeID"];
				UpdateTableRowByID("PropertyIncome_Code", "IncomeID", "DateUpdated", obj, DateTime.Now, sqlTransaction);
				string entiyID = accountPropertyIncomeCodeData.PropertyIncomeCodeTable.Rows[0]["IncomeName"].ToString();
				AddActivityLog("PropertyIncomeCode", entiyID, ActivityTypes.Update, sqlTransaction);
				UpdateTableRowInsertUpdateInfo("PropertyIncome_Code", "IncomeID", obj, sqlTransaction, isInsert: false);
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

		public PropertyIncomeCodeData GetPropertyIncomeCode()
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddTable("PropertyIncome_Code");
			PropertyIncomeCodeData propertyIncomeCodeData = new PropertyIncomeCodeData();
			sqlBuilder.UseDistinct = false;
			FillDataSet(propertyIncomeCodeData, "PropertyIncome_Code", sqlBuilder);
			return propertyIncomeCodeData;
		}

		public bool DeletePropertyIncomeCode(string degreeID)
		{
			bool flag = true;
			try
			{
				string commandText = "DELETE FROM PropertyIncome_Code WHERE IncomeID = '" + degreeID + "'";
				flag = Delete(commandText, null);
				if (!flag)
				{
					return flag;
				}
				AddActivityLog("Income", degreeID, ActivityTypes.Delete, null);
				return flag;
			}
			catch
			{
				throw;
			}
		}

		public PropertyIncomeCodeData GetPropertyIncomeCodeByID(string id)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			CommandHelper commandHelper = null;
			commandHelper = new CommandHelper();
			commandHelper.FieldName = "IncomeID";
			commandHelper.SqlFieldType = SqlDbType.NVarChar;
			commandHelper.FieldValue = id;
			commandHelper.TableName = "PropertyIncome_Code";
			sqlBuilder.AddCommandHelper(commandHelper);
			sqlBuilder.IsComparing = true;
			PropertyIncomeCodeData propertyIncomeCodeData = new PropertyIncomeCodeData();
			sqlBuilder.UseDistinct = false;
			FillDataSet(propertyIncomeCodeData, "PropertyIncome_Code", sqlBuilder);
			return propertyIncomeCodeData;
		}

		public DataSet GetPropertyIncomeCodeByFields(params string[] columns)
		{
			return GetPropertyIncomeCodeByFields(null, isInactive: true, columns);
		}

		public DataSet GetPropertyIncomeCodeByFields(string[] degreeID, params string[] columns)
		{
			return GetPropertyIncomeCodeByFields(degreeID, isInactive: true, columns);
		}

		public DataSet GetPropertyIncomeCodeByFields(string[] ids, bool isInactive, params string[] columns)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddTable("PropertyIncome_Code");
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
				commandHelper.FieldName = "IncomeID";
				commandHelper.FieldValue = ids;
				commandHelper.SqlFieldType = SqlDbType.NVarChar;
				commandHelper.TableName = "PropertyIncome_Code";
				sqlBuilder.AddCommandHelper(commandHelper);
			}
			DataSet dataSet = new DataSet();
			dataSet.EnforceConstraints = false;
			sqlBuilder.UseDistinct = false;
			FillDataSet(dataSet, "PropertyIncome_Code", sqlBuilder);
			return dataSet;
		}

		public string GetIncomeAccountID(string incomeID, SqlTransaction sqlTransaction)
		{
			new DataSet();
			string exp = "SELECT AccountID  \r\n                           FROM PropertyIncome_Code WHERE IncomeID = '" + incomeID + "'";
			return ExecuteScalar(exp, sqlTransaction).ToString();
		}

		public string GetIncomeAccountIDByName(string incomeName, SqlTransaction sqlTransaction)
		{
			new DataSet();
			string exp = "SELECT AccountID  \r\n                           FROM PropertyIncome_Code WHERE IncomeName = '" + incomeName + "'";
			return ExecuteScalar(exp, sqlTransaction).ToString();
		}

		public DataSet GetPropertyIncomeCodeList()
		{
			DataSet dataSet = new DataSet();
			string textCommand = "SELECT IncomeID [Income Code],IncomeName [Income Name]\r\n                           FROM PropertyIncome_Code ";
			FillDataSet(dataSet, "PropertyIncome_Code", textCommand);
			return dataSet;
		}

		public DataSet GetPropertyIncomeCodeComboList()
		{
			DataSet dataSet = new DataSet();
			string textCommand = "SELECT IncomeID [Code], IncomeName [Name],IncomeType,AccountID, IncomeRate,PropertyIncome_Code.TaxOption, PropertyIncome_Code.TaxGroupID AS TaxGroupID\r\n                           FROM PropertyIncome_Code ORDER BY IncomeID, IncomeName";
			FillDataSet(dataSet, "PropertyIncome_Code", textCommand);
			return dataSet;
		}
	}
}
