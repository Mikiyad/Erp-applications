using Micromind.Common;
using Micromind.Common.Data;
using Micromind.Common.Libraries;
using System;
using System.Data;
using System.Data.SqlClient;

namespace Micromind.Data
{
	public sealed class City : StoreObject
	{
		private const string CITYID_PARM = "@CityID";

		private const string CITYNAME_PARM = "@CityName";

		private const string COUNTRYID_PARM = "@CountryID";

		private const string INACTIVE_PARM = "@IsInactive";

		private const string CITY_TABLE = "City";

		private const string CREATEDBY_PARM = "@CreatedBy";

		private const string DATECREATED_PARM = "@DateCreated";

		private const string UPDATEDBY_PARM = "@UpdatedBy";

		private const string DATEUPDATED_PARM = "@DateUpdated";

		public City(Config config)
			: base(config)
		{
		}

		private string GetInsertUpdateText(bool isUpdate)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddInsertUpdateParameters("City", new FieldValue("CityID", "@CityID", isUpdateConditionField: true), new FieldValue("CityName", "@CityName"), new FieldValue("CountryID", "@CountryID"), new FieldValue("IsInactive", "@IsInactive"));
			if (isUpdate)
			{
				sqlBuilder.AddInsertUpdateParameters("City", new FieldValue("DateUpdated", "@DateUpdated", isUpdateConditionField: true, checkForNullValue: true));
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
			parameters.Add("@CityID", SqlDbType.NVarChar);
			parameters.Add("@CityName", SqlDbType.NVarChar);
			parameters.Add("@CountryID", SqlDbType.NVarChar);
			parameters.Add("@IsInactive", SqlDbType.Bit);
			parameters["@CityID"].SourceColumn = "CityID";
			parameters["@CityName"].SourceColumn = "CityName";
			parameters["@CountryID"].SourceColumn = "CountryID";
			parameters["@IsInactive"].SourceColumn = "IsInactive";
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

		public bool InsertCity(CityData accountCityData)
		{
			bool result = true;
			SqlCommand insertUpdateCommand = GetInsertUpdateCommand(isUpdate: false);
			SqlTransaction sqlTransaction = null;
			try
			{
				sqlTransaction = (insertUpdateCommand.Transaction = base.DBConfig.StartNewTransaction());
				result = Insert(accountCityData, "City", insertUpdateCommand);
				string text = accountCityData.CityTable.Rows[0]["CityID"].ToString();
				AddActivityLog("City", text, ActivityTypes.Add, sqlTransaction);
				UpdateTableRowInsertUpdateInfo("City", "CityID", text, sqlTransaction, isInsert: true);
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

		public bool UpdateCity(CityData accountCityData)
		{
			bool flag = true;
			SqlCommand insertUpdateCommand = GetInsertUpdateCommand(isUpdate: true);
			SqlTransaction sqlTransaction = null;
			try
			{
				sqlTransaction = (insertUpdateCommand.Transaction = base.DBConfig.StartNewTransaction());
				flag = Update(accountCityData, "City", insertUpdateCommand);
				if (!flag)
				{
					return flag;
				}
				object obj = accountCityData.CityTable.Rows[0]["CityID"];
				UpdateTableRowByID("City", "CityID", "DateUpdated", obj, DateTime.Now, sqlTransaction);
				string entiyID = accountCityData.CityTable.Rows[0]["CityName"].ToString();
				AddActivityLog("City", entiyID, ActivityTypes.Update, sqlTransaction);
				UpdateTableRowInsertUpdateInfo("City", "CityID", obj, sqlTransaction, isInsert: false);
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

		public CityData GetCity()
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddTable("City");
			CityData cityData = new CityData();
			sqlBuilder.UseDistinct = false;
			FillDataSet(cityData, "City", sqlBuilder);
			return cityData;
		}

		public bool DeleteCity(string cityID)
		{
			bool flag = true;
			try
			{
				string commandText = "DELETE FROM City WHERE CityID = '" + cityID + "'";
				flag = Delete(commandText, null);
				if (!flag)
				{
					return flag;
				}
				AddActivityLog("City", cityID, ActivityTypes.Delete, null);
				return flag;
			}
			catch
			{
				throw;
			}
		}

		public CityData GetCityByID(string id)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			CommandHelper commandHelper = null;
			commandHelper = new CommandHelper();
			commandHelper.FieldName = "CityID";
			commandHelper.SqlFieldType = SqlDbType.NVarChar;
			commandHelper.FieldValue = id;
			commandHelper.TableName = "City";
			sqlBuilder.AddCommandHelper(commandHelper);
			sqlBuilder.IsComparing = true;
			CityData cityData = new CityData();
			sqlBuilder.UseDistinct = false;
			FillDataSet(cityData, "City", sqlBuilder);
			return cityData;
		}

		public DataSet GetCityByFields(params string[] columns)
		{
			return GetCityByFields(null, isInactive: true, columns);
		}

		public DataSet GetCityByFields(string[] cityID, params string[] columns)
		{
			return GetCityByFields(cityID, isInactive: true, columns);
		}

		public DataSet GetCityByFields(string[] ids, bool isInactive, params string[] columns)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddTable("City");
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
				commandHelper.FieldName = "CityID";
				commandHelper.FieldValue = ids;
				commandHelper.SqlFieldType = SqlDbType.NVarChar;
				commandHelper.TableName = "City";
				sqlBuilder.AddCommandHelper(commandHelper);
			}
			DataSet dataSet = new DataSet();
			dataSet.EnforceConstraints = false;
			sqlBuilder.UseDistinct = false;
			FillDataSet(dataSet, "City", sqlBuilder);
			return dataSet;
		}

		public DataSet GetCityList()
		{
			DataSet dataSet = new DataSet();
			string textCommand = "SELECT CityID [Type Code],CityName [Type Name],IsInactive\r\n                           FROM City ";
			FillDataSet(dataSet, "City", textCommand);
			return dataSet;
		}

		public DataSet GetCityComboList()
		{
			DataSet dataSet = new DataSet();
			string textCommand = "SELECT CityID [Code], CityName [Name]\r\n                           FROM City ORDER BY CityID,CityName";
			FillDataSet(dataSet, "City", textCommand);
			return dataSet;
		}
	}
}
