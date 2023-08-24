using Micromind.Common;
using Micromind.Common.Data;
using Micromind.Common.Libraries;
using System;
using System.Data;
using System.Data.SqlClient;

namespace Micromind.Data
{
	public sealed class Currencies : StoreObject
	{
		private const string CURRENCYID_PARM = "@CurrencyID";

		private const string CURRENCYNAME_PARM = "@CurrencyName";

		private const string EXCHANGERATE_PARM = "@ExchangeRate";

		private const string DESCRIPTION_PARM = "@Description";

		private const string RATETYPE_PARM = "@RateType";

		private const string RATEUPDATEDDATE_PARM = "@RateUpdatedDate";

		private const string INACTIVE_PARM = "@Inactive";

		private const string CURRENCY_TABLE = "Currency";

		private const string CREATEDBY_PARM = "@CreatedBy";

		private const string DATECREATED_PARM = "@DateCreated";

		private const string UPDATEDBY_PARM = "@UpdatedBy";

		private const string DATEUPDATED_PARM = "@DateUpdated";

		public Currencies(Config config)
			: base(config)
		{
		}

		private string GetInsertUpdateText(bool isUpdate)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddInsertUpdateParameters("Currency", new FieldValue("CurrencyID", "@CurrencyID", isUpdateConditionField: true), new FieldValue("CurrencyName", "@CurrencyName"), new FieldValue("ExchangeRate", "@ExchangeRate"), new FieldValue("RateType", "@RateType"), new FieldValue("Description", "@Description"), new FieldValue("Inactive", "@Inactive"));
			if (isUpdate)
			{
				sqlBuilder.AddInsertUpdateParameters("Currency", new FieldValue("DateUpdated", "@DateUpdated", isUpdateConditionField: true, checkForNullValue: true));
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
			parameters.Add("@CurrencyID", SqlDbType.NVarChar);
			parameters.Add("@CurrencyName", SqlDbType.NVarChar);
			parameters.Add("@ExchangeRate", SqlDbType.SmallMoney);
			parameters.Add("@Description", SqlDbType.NVarChar);
			parameters.Add("@RateType", SqlDbType.Char);
			parameters.Add("@Inactive", SqlDbType.Bit);
			parameters["@CurrencyID"].SourceColumn = "CurrencyID";
			parameters["@CurrencyName"].SourceColumn = "CurrencyName";
			parameters["@ExchangeRate"].SourceColumn = "ExchangeRate";
			parameters["@Description"].SourceColumn = "Description";
			parameters["@RateType"].SourceColumn = "RateType";
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

		public bool InsertCurrency(CurrencyData currencyData)
		{
			bool result = true;
			SqlCommand insertUpdateCommand = GetInsertUpdateCommand(isUpdate: false);
			SqlTransaction sqlTransaction = null;
			try
			{
				sqlTransaction = (insertUpdateCommand.Transaction = base.DBConfig.StartNewTransaction());
				result = Insert(currencyData, "Currency", insertUpdateCommand);
				string text = currencyData.CurrencyTable.Rows[0]["CurrencyID"].ToString();
				AddActivityLog("Currency", text, ActivityTypes.Add, sqlTransaction);
				UpdateTableRowInsertUpdateInfo("Currency", "CurrencyID", text, sqlTransaction, isInsert: true);
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

		public bool UpdateCurrency(CurrencyData currencyData)
		{
			bool flag = true;
			SqlCommand insertUpdateCommand = GetInsertUpdateCommand(isUpdate: true);
			SqlTransaction sqlTransaction = null;
			try
			{
				sqlTransaction = (insertUpdateCommand.Transaction = base.DBConfig.StartNewTransaction());
				flag = Update(currencyData, "Currency", insertUpdateCommand);
				if (!flag)
				{
					return flag;
				}
				object obj = currencyData.CurrencyTable.Rows[0]["CurrencyID"];
				UpdateTableRowByID("Currency", "CurrencyID", "DateUpdated", obj, DateTime.Now, sqlTransaction);
				string entiyID = currencyData.CurrencyTable.Rows[0]["CurrencyName"].ToString();
				AddActivityLog("Currency", entiyID, ActivityTypes.Update, sqlTransaction);
				UpdateTableRowInsertUpdateInfo("Currency", "CurrencyID", obj, sqlTransaction, isInsert: false);
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

		private string GetExchangeTableInsertUpdateText(bool isUpdate)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddInsertUpdateParameters("Currency_Exchange_Rate", new FieldValue("CurrencyID", "@CurrencyID"), new FieldValue("ExchangeRate", "@ExchangeRate"), new FieldValue("DateCreated", "@DateCreated"), new FieldValue("CreatedBy", "@CreatedBy"), new FieldValue("RateUpdatedDate", "@RateUpdatedDate"));
			if (isUpdate)
			{
				return sqlBuilder.GetUpdateExpression();
			}
			return sqlBuilder.GetInsertExpression();
		}

		private SqlCommand GetExchangeTableInsertUpdateCommand(bool isUpdate)
		{
			SqlParameterCollection parameters;
			if (isUpdate)
			{
				if (updateCommand != null)
				{
					return updateCommand;
				}
				updateCommand = new SqlCommand(GetExchangeTableInsertUpdateText(isUpdate: true), base.DBConfig.Connection);
				updateCommand.CommandType = CommandType.Text;
				parameters = updateCommand.Parameters;
			}
			else
			{
				if (insertCommand != null)
				{
					return insertCommand;
				}
				insertCommand = new SqlCommand(GetExchangeTableInsertUpdateText(isUpdate: false), base.DBConfig.Connection);
				insertCommand.CommandType = CommandType.Text;
				parameters = insertCommand.Parameters;
			}
			parameters.Add("@CurrencyID", SqlDbType.NVarChar);
			parameters.Add("@ExchangeRate", SqlDbType.SmallMoney);
			parameters.Add("@RateUpdatedDate", SqlDbType.DateTime);
			parameters.Add("@DateCreated", SqlDbType.DateTime);
			parameters.Add("@CreatedBy", SqlDbType.NVarChar);
			parameters["@CurrencyID"].SourceColumn = "CurrencyID";
			parameters["@ExchangeRate"].SourceColumn = "ExchangeRate";
			parameters["@RateUpdatedDate"].SourceColumn = "RateUpdatedDate";
			parameters["@DateCreated"].SourceColumn = "DateCreated";
			parameters["@CreatedBy"].SourceColumn = "CreatedBy";
			if (isUpdate)
			{
				return updateCommand;
			}
			return insertCommand;
		}

		public bool InsertExchangeRateTableRecord(CurrencyData currencyData)
		{
			bool result = true;
			SqlCommand exchangeTableInsertUpdateCommand = GetExchangeTableInsertUpdateCommand(isUpdate: false);
			SqlTransaction sqlTransaction = null;
			try
			{
				DateTime now = DateTime.Now;
				foreach (DataRow row in currencyData.Tables[0].Rows)
				{
					row["DateCreated"] = now;
					row["CreatedBy"] = base.DBConfig.UserID;
				}
				sqlTransaction = (exchangeTableInsertUpdateCommand.Transaction = base.DBConfig.StartNewTransaction());
				result = Insert(currencyData, "Currency_Exchange_Rate", exchangeTableInsertUpdateCommand);
				foreach (DataRow row2 in currencyData.Tables[0].Rows)
				{
					string text = row2["ExchangeRate"].ToString();
					DateTime date = DateTime.Parse(row2["RateUpdatedDate"].ToString());
					string exp = "UPDATE Currency SET ExchangeRate=" + text + ",RateUpdatedDate='" + StoreConfiguration.ToSqlDateTimeString(date) + "' WHERE CurrencyID='" + row2["CurrencyID"].ToString() + "'";
					ExecuteNonQuery(exp, sqlTransaction);
				}
				AddActivityLog("Currency Rate", "", ActivityTypes.Update, sqlTransaction);
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

		public CurrencyData GetCurrency()
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddTable("Currency");
			CurrencyData currencyData = new CurrencyData();
			sqlBuilder.UseDistinct = false;
			FillDataSet(currencyData, "Currency", sqlBuilder);
			return currencyData;
		}

		public bool DeleteCurrency(string currencyID)
		{
			bool flag = true;
			try
			{
				string commandText = "DELETE FROM Currency WHERE CurrencyID = '" + currencyID + "'";
				flag = Delete(commandText, null);
				if (!flag)
				{
					return flag;
				}
				AddActivityLog("Currency", currencyID, ActivityTypes.Delete, null);
				return flag;
			}
			catch
			{
				throw;
			}
		}

		public CurrencyData GetCurrencyByID(string id)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			CommandHelper commandHelper = null;
			commandHelper = new CommandHelper();
			commandHelper.FieldName = "CurrencyID";
			commandHelper.SqlFieldType = SqlDbType.NVarChar;
			commandHelper.FieldValue = id;
			commandHelper.TableName = "Currency";
			sqlBuilder.AddCommandHelper(commandHelper);
			sqlBuilder.IsComparing = true;
			CurrencyData currencyData = new CurrencyData();
			sqlBuilder.UseDistinct = false;
			FillDataSet(currencyData, "Currency", sqlBuilder);
			return currencyData;
		}

		public DataSet GetCurrencyByFields(params string[] columns)
		{
			return GetCurrencyByFields(null, isInactive: true, columns);
		}

		public DataSet GetCurrencyByFields(string[] currencyID, params string[] columns)
		{
			return GetCurrencyByFields(currencyID, isInactive: true, columns);
		}

		public DataSet GetCurrencyByFields(string[] ids, bool isInactive, params string[] columns)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddTable("Currency");
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
				commandHelper.FieldName = "CurrencyID";
				commandHelper.FieldValue = ids;
				commandHelper.SqlFieldType = SqlDbType.NVarChar;
				commandHelper.TableName = "Currency";
				sqlBuilder.AddCommandHelper(commandHelper);
			}
			DataSet dataSet = new DataSet();
			dataSet.EnforceConstraints = false;
			sqlBuilder.UseDistinct = false;
			FillDataSet(dataSet, "Currency", sqlBuilder);
			return dataSet;
		}

		public string GetBaseCurrencyID()
		{
			string exp = "SELECT Max(CurrencyID)\r\n                           FROM Currency WHERE IsBase=1";
			object obj = ExecuteScalar(exp);
			if (obj != null)
			{
				return obj.ToString();
			}
			return "";
		}

		public string GetCurrencyRateType(string currencyID)
		{
			string exp = "SELECT RateType\r\n                           FROM Currency WHERE currencyID='" + currencyID + "'";
			object obj = ExecuteScalar(exp);
			if (obj != null)
			{
				return obj.ToString();
			}
			return "M";
		}

		public decimal GetCurrencyRate(string currencyID)
		{
			string exp = "SELECT ISNULL(ExchangeRate,1)\r\n                           FROM Currency WHERE currencyID='" + currencyID + "'";
			object obj = ExecuteScalar(exp);
			if (obj != null)
			{
				return decimal.Parse(obj.ToString());
			}
			return 1m;
		}

		public DataSet GetCurrencyList()
		{
			DataSet dataSet = new DataSet();
			string textCommand = "SELECT CurrencyID [Currency Code],CurrencyName [Currency Name],ISNULL(Inactive,'false')  \r\n                           FROM Currency ";
			FillDataSet(dataSet, "Currency", textCommand);
			return dataSet;
		}

		public DataSet GetCurrencyComboList()
		{
			DataSet dataSet = new DataSet();
			string textCommand = "SELECT CurrencyID [Code],CurrencyName [Name],ISNULL(ExchangeRate,1) AS ExchangeRate,RateType\r\n                           FROM Currency ORDER BY CurrencyID,CurrencyName";
			FillDataSet(dataSet, "Currency", textCommand);
			return dataSet;
		}

		public DataSet GetExchangeRateTable()
		{
			DataSet dataSet = new DataSet();
			string textCommand = "SELECT CurrencyID ,ExchangeRate\r\n                           FROM Currency ORDER BY CurrencyID,CurrencyName";
			FillDataSet(dataSet, "Currency", textCommand);
			return dataSet;
		}

		public DataSet GetCurrencytables()
		{
			DataSet dataSet = new DataSet();
			string textCommand = "select * from (\r\n\r\n                        SELECT sys.columns.*, \r\n                          sys.columns.name AS ColumnName,\r\n                          tables.name AS TableName\r\n\r\n                        FROM\r\n                          sys.columns\r\n                        JOIN sys.tables ON\r\n                          sys.columns.object_id = tables.object_id\r\n\r\n                        WHERE sys.columns.name = 'CurrencyID'  )  AS Value ";
			FillDataSet(dataSet, "CurrencyData", textCommand);
			return dataSet;
		}

		public float GetCurrencyCount(string tableName, string currencyID)
		{
			string exp = "select count(1) from " + tableName + "  where CurrencyID = '" + currencyID + "' ";
			object obj = ExecuteScalar(exp);
			if (obj != null && obj.ToString() != "")
			{
				return float.Parse(obj.ToString());
			}
			return 0f;
		}
	}
}
