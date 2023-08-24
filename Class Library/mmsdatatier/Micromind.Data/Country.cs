using Micromind.Common;
using Micromind.Common.Data;
using Micromind.Common.Libraries;
using System;
using System.Data;
using System.Data.SqlClient;

namespace Micromind.Data
{
	public sealed class Country : StoreObject
	{
		private const string COUNTRYID_PARM = "@CountryID";

		private const string COUNTRYNAME_PARM = "@CountryName";

		public const string PHONECODE_PARM = "@PhoneCode";

		public const string CURRENCYCODE_PARM = "@CurrencyCode";

		public const string NOTE_PARM = "@Note";

		public const string COUNTRY_TABLE = "Country";

		private const string CREATEDBY_PARM = "@CreatedBy";

		private const string DATECREATED_PARM = "@DateCreated";

		private const string UPDATEDBY_PARM = "@UpdatedBy";

		private const string DATEUPDATED_PARM = "@DateUpdated";

		public Country(Config config)
			: base(config)
		{
		}

		private string GetInsertUpdateText(bool isUpdate)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddInsertUpdateParameters("Country", new FieldValue("CountryID", "@CountryID", isUpdateConditionField: true), new FieldValue("CountryName", "@CountryName"), new FieldValue("CurrencyCode", "@CurrencyCode"), new FieldValue("PhoneCode", "@PhoneCode"), new FieldValue("Note", "@Note"));
			if (isUpdate)
			{
				sqlBuilder.AddInsertUpdateParameters("Country", new FieldValue("DateUpdated", "@DateUpdated", isUpdateConditionField: true, checkForNullValue: true));
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
			parameters.Add("@CountryID", SqlDbType.NVarChar);
			parameters.Add("@CountryName", SqlDbType.NVarChar);
			parameters.Add("@CurrencyCode", SqlDbType.NVarChar);
			parameters.Add("@PhoneCode", SqlDbType.NVarChar);
			parameters.Add("@Note", SqlDbType.NVarChar);
			parameters["@CountryID"].SourceColumn = "CountryID";
			parameters["@CountryName"].SourceColumn = "CountryName";
			parameters["@PhoneCode"].SourceColumn = "PhoneCode";
			parameters["@CurrencyCode"].SourceColumn = "CurrencyCode";
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

		public bool InsertCountry(CountryData accountCountryData)
		{
			bool result = true;
			SqlCommand insertUpdateCommand = GetInsertUpdateCommand(isUpdate: false);
			SqlTransaction sqlTransaction = null;
			try
			{
				sqlTransaction = (insertUpdateCommand.Transaction = base.DBConfig.StartNewTransaction());
				result = Insert(accountCountryData, "Country", insertUpdateCommand);
				string text = accountCountryData.CountryTable.Rows[0]["CountryID"].ToString();
				AddActivityLog("Country", text, ActivityTypes.Add, sqlTransaction);
				UpdateTableRowInsertUpdateInfo("Country", "CountryID", text, sqlTransaction, isInsert: true);
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

		public bool UpdateCountry(CountryData accountCountryData)
		{
			bool flag = true;
			SqlCommand insertUpdateCommand = GetInsertUpdateCommand(isUpdate: true);
			SqlTransaction sqlTransaction = null;
			try
			{
				sqlTransaction = (insertUpdateCommand.Transaction = base.DBConfig.StartNewTransaction());
				flag = Update(accountCountryData, "Country", insertUpdateCommand);
				if (!flag)
				{
					return flag;
				}
				object obj = accountCountryData.CountryTable.Rows[0]["CountryID"];
				UpdateTableRowByID("Country", "CountryID", "DateUpdated", obj, DateTime.Now, sqlTransaction);
				string entiyID = accountCountryData.CountryTable.Rows[0]["CountryName"].ToString();
				AddActivityLog("Country", entiyID, ActivityTypes.Update, sqlTransaction);
				UpdateTableRowInsertUpdateInfo("Country", "CountryID", obj, sqlTransaction, isInsert: false);
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

		public CountryData GetCountry()
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddTable("Country");
			CountryData countryData = new CountryData();
			sqlBuilder.UseDistinct = false;
			FillDataSet(countryData, "Country", sqlBuilder);
			return countryData;
		}

		public bool DeleteCountry(string countryID)
		{
			bool flag = true;
			try
			{
				string commandText = "DELETE FROM Country WHERE CountryID = '" + countryID + "'";
				flag = Delete(commandText, null);
				if (!flag)
				{
					return flag;
				}
				AddActivityLog("Country", countryID, ActivityTypes.Delete, null);
				return flag;
			}
			catch
			{
				throw;
			}
		}

		public CountryData GetCountryByID(string id)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			CommandHelper commandHelper = null;
			commandHelper = new CommandHelper();
			commandHelper.FieldName = "CountryID";
			commandHelper.SqlFieldType = SqlDbType.NVarChar;
			commandHelper.FieldValue = id;
			commandHelper.TableName = "Country";
			sqlBuilder.AddCommandHelper(commandHelper);
			sqlBuilder.IsComparing = true;
			CountryData countryData = new CountryData();
			sqlBuilder.UseDistinct = false;
			FillDataSet(countryData, "Country", sqlBuilder);
			return countryData;
		}

		public DataSet GetCountryByFields(params string[] columns)
		{
			return GetCountryByFields(null, isInactive: true, columns);
		}

		public DataSet GetCountryByFields(string[] countryID, params string[] columns)
		{
			return GetCountryByFields(countryID, isInactive: true, columns);
		}

		public DataSet GetCountryByFields(string[] ids, bool isInactive, params string[] columns)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddTable("Country");
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
				commandHelper.FieldName = "CountryID";
				commandHelper.FieldValue = ids;
				commandHelper.SqlFieldType = SqlDbType.NVarChar;
				commandHelper.TableName = "Country";
				sqlBuilder.AddCommandHelper(commandHelper);
			}
			DataSet dataSet = new DataSet();
			dataSet.EnforceConstraints = false;
			sqlBuilder.UseDistinct = false;
			FillDataSet(dataSet, "Country", sqlBuilder);
			return dataSet;
		}

		public DataSet GetCountryList()
		{
			DataSet dataSet = new DataSet();
			string textCommand = "SELECT CountryID [Country Code],CountryName [Country Name],PhoneCode [Phone Code],Note\r\n                           FROM Country ";
			FillDataSet(dataSet, "Country", textCommand);
			return dataSet;
		}

		public DataSet GetCountryComboList()
		{
			DataSet dataSet = new DataSet();
			string textCommand = "SELECT CountryID [Code],CountryName [Name]\r\n                           FROM Country ORDER BY CountryID,CountryName";
			FillDataSet(dataSet, "Country", textCommand);
			return dataSet;
		}
	}
}
