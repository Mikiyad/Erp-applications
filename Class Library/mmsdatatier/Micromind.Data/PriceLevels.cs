using Micromind.Common;
using Micromind.Common.Data;
using Micromind.Common.Libraries;
using System;
using System.Data;
using System.Data.SqlClient;

namespace Micromind.Data
{
	public sealed class PriceLevels : StoreObject
	{
		private const string PRICELEVELID_PARM = "@PriceLevelID";

		private const string PRICELEVELNAME_PARM = "@PriceLevelName";

		private const string NOTE_PARM = "@Note";

		private const string ISINACTIVE_PARM = "@IsInactive";

		private const string DATEUPDATED_PARM = "@DateUpdated";

		public bool CheckConcurrency = true;

		public PriceLevels(Config config)
			: base(config)
		{
		}

		private string GetInsertUpdateText(bool isUpdate)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddInsertUpdateParameters("Price_Level", new FieldValue("PriceLevelName", "@PriceLevelName"), new FieldValue("IsInactive", "@IsInactive"), new FieldValue("Note", "@Note"));
			if (isUpdate)
			{
				sqlBuilder.AddInsertUpdateParameters("Price_Level", new FieldValue("DateUpdated", "@DateUpdated", isUpdateConditionField: true, checkForNullValue: true));
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
			parameters.Add("@PriceLevelID", SqlDbType.NVarChar);
			parameters.Add("@PriceLevelName", SqlDbType.NVarChar);
			parameters.Add("@Note", SqlDbType.NText);
			parameters.Add("@IsInactive", SqlDbType.Bit);
			parameters["@PriceLevelID"].SourceColumn = "PriceLevelID";
			parameters["@PriceLevelName"].SourceColumn = "PriceLevelName";
			parameters["@Note"].SourceColumn = "Note";
			parameters["@IsInactive"].SourceColumn = "IsInactive";
			if (isUpdate)
			{
				return updateCommand;
			}
			return insertCommand;
		}

		public bool InsertPriceLevel(PriceLevelData priceLevelData)
		{
			bool flag = true;
			SqlCommand insertUpdateCommand = GetInsertUpdateCommand(isUpdate: false);
			SqlTransaction sqlTransaction = null;
			try
			{
				sqlTransaction = (insertUpdateCommand.Transaction = base.DBConfig.StartNewTransaction());
				flag = Insert(priceLevelData, "Price_Level", insertUpdateCommand);
				object insertedRowIdentity = GetInsertedRowIdentity("Price_Level", insertUpdateCommand);
				priceLevelData.PriceLevelTable.Rows[0]["PriceLevelID"] = insertedRowIdentity;
				UpdateTableRowByID("Price_Level", "PriceLevelID", "DateUpdated", insertedRowIdentity, DateTime.Now, sqlTransaction);
				priceLevelData.PriceLevelTable.Rows[0]["PriceLevelName"].ToString();
				if (flag)
				{
					AddActivityLog("Price Level", insertedRowIdentity.ToString(), ActivityTypes.Add, sqlTransaction);
				}
				if (!flag)
				{
					return flag;
				}
				UpdateTableRowInsertUpdateInfo("Price_Level", "PriceLevelID", insertedRowIdentity, sqlTransaction, isInsert: true);
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

		public bool UpdatePriceLevel(PriceLevelData priceLevelData)
		{
			bool flag = true;
			SqlCommand insertUpdateCommand = GetInsertUpdateCommand(isUpdate: true);
			SqlTransaction sqlTransaction = null;
			try
			{
				sqlTransaction = (insertUpdateCommand.Transaction = base.DBConfig.StartNewTransaction());
				flag = Update(priceLevelData, "Price_Level", insertUpdateCommand);
				if (!flag)
				{
					return flag;
				}
				object obj = priceLevelData.PriceLevelTable.Rows[0]["PriceLevelID"];
				UpdateTableRowByID("Price_Level", "PriceLevelID", "DateUpdated", obj, DateTime.Now, sqlTransaction);
				priceLevelData.PriceLevelTable.Rows[0]["PriceLevelName"].ToString();
				if (flag)
				{
					AddActivityLog("Price Level", obj.ToString(), ActivityTypes.Update, sqlTransaction);
				}
				if (!flag)
				{
					return flag;
				}
				UpdateTableRowInsertUpdateInfo("Price_Level", "PriceLevelID", obj, sqlTransaction, isInsert: false);
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

		public PriceLevelData GetPriceLevelByID(string levelID)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			CommandHelper commandHelper = null;
			commandHelper = new CommandHelper();
			commandHelper.FieldName = "PriceLevelID";
			commandHelper.SqlFieldType = SqlDbType.NVarChar;
			commandHelper.FieldValue = levelID;
			commandHelper.TableName = "Price_Level";
			sqlBuilder.AddCommandHelper(commandHelper);
			PriceLevelData priceLevelData = new PriceLevelData();
			sqlBuilder.UseDistinct = false;
			try
			{
				FillDataSet(priceLevelData, "Price_Level", sqlBuilder);
				return priceLevelData;
			}
			catch
			{
				throw;
			}
			finally
			{
				commandHelper = null;
				sqlBuilder = null;
			}
		}

		public PriceLevelData GetPriceLevels()
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			CommandHelper commandHelper = null;
			commandHelper = new CommandHelper();
			commandHelper.TableName = "Price_Level";
			sqlBuilder.AddCommandHelper(commandHelper);
			sqlBuilder.UseDistinct = false;
			sqlBuilder.IsComparing = false;
			PriceLevelData priceLevelData = new PriceLevelData();
			try
			{
				FillDataSet(priceLevelData, "Price_Level", sqlBuilder);
				return priceLevelData;
			}
			catch
			{
				throw;
			}
			finally
			{
				commandHelper = null;
				sqlBuilder = null;
			}
		}

		public bool DeletePriceLevel(string levelID)
		{
			bool flag = true;
			try
			{
				string priceLevelNameByID = GetPriceLevelNameByID(levelID);
				flag = DeleteTableRowByID("Price_Level", "PriceLevelID", levelID);
				if (!flag)
				{
					return flag;
				}
				AddActivityLog("Price Level", priceLevelNameByID, ActivityTypes.Delete, null);
				return flag;
			}
			catch
			{
				throw;
			}
		}

		public string GetPriceLevelNameByID(object id)
		{
			try
			{
				object obj = ExecuteSelectScalar("Price_Level", "PriceLevelID", id, "PriceLevelName");
				if (obj != null && obj != DBNull.Value)
				{
					return obj.ToString();
				}
			}
			catch
			{
				throw;
			}
			return "";
		}

		public bool ActivatePriceLevel(object id, bool activate)
		{
			activate = !activate;
			bool num = UpdateTableRowByID("Price_Level", "PriceLevelID", "IsInactive", id, Convert.ToByte(activate));
			if (num)
			{
				if (!activate)
				{
					AddActivityLog("Price Level", id.ToString(), ActivityTypes.Activate, null);
					return num;
				}
				AddActivityLog("Price Level", id.ToString(), ActivityTypes.Inactivate, null);
			}
			return num;
		}

		public DataSet GetPriceLevelsByFields(params string[] columns)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			foreach (string text in columns)
			{
				ApplicationAssert.CheckCondition(text.IndexOf(".") >= 0, "A table name with the column name must be specified. eg. table.column.", 0);
				string tableName = text.Substring(0, text.IndexOf("."));
				string columnName = text.Substring(text.IndexOf(".") + 1);
				sqlBuilder.AddColumn(tableName, columnName);
			}
			DataSet dataSet = new DataSet();
			dataSet.EnforceConstraints = false;
			sqlBuilder.UseDistinct = false;
			try
			{
				FillDataSet(dataSet, "Price_Level", sqlBuilder);
				return dataSet;
			}
			catch
			{
				throw;
			}
		}

		public DataSet GetPriceLevelsByFields(int[] levelID, params string[] columns)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			foreach (string text in columns)
			{
				ApplicationAssert.CheckCondition(text.IndexOf(".") >= 0, "A table name with the column name must be specified. eg. table.column.", 0);
				string tableName = text.Substring(0, text.IndexOf("."));
				string columnName = text.Substring(text.IndexOf(".") + 1);
				sqlBuilder.AddColumn(tableName, columnName);
			}
			if (levelID != null && levelID.Length != 0)
			{
				CommandHelper commandHelper = new CommandHelper();
				commandHelper.FieldName = "PriceLevelID";
				commandHelper.FieldValue = levelID;
				commandHelper.TableName = "Price_Level";
				sqlBuilder.AddCommandHelper(commandHelper);
			}
			DataSet dataSet = new DataSet();
			dataSet.EnforceConstraints = false;
			sqlBuilder.UseDistinct = false;
			try
			{
				FillDataSet(dataSet, "Price_Level", sqlBuilder);
				return dataSet;
			}
			catch
			{
				throw;
			}
		}

		public DataSet GetPriceLevelsByFields(bool isInactive, int[] levelID, params string[] columns)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			foreach (string text in columns)
			{
				ApplicationAssert.CheckCondition(text.IndexOf(".") >= 0, "A table name with the column name must be specified. eg. table.column.", 0);
				string tableName = text.Substring(0, text.IndexOf("."));
				string columnName = text.Substring(text.IndexOf(".") + 1);
				sqlBuilder.AddColumn(tableName, columnName);
			}
			if (levelID != null && levelID.Length != 0)
			{
				CommandHelper commandHelper = new CommandHelper();
				commandHelper.FieldName = "PriceLevelID";
				commandHelper.FieldValue = levelID;
				commandHelper.SqlFieldType = SqlDbType.NVarChar;
				commandHelper.TableName = "Price_Level";
				sqlBuilder.AddCommandHelper(commandHelper);
			}
			if (!isInactive)
			{
				CommandHelper commandHelper2 = new CommandHelper();
				commandHelper2.FieldName = "IsInactive";
				commandHelper2.FieldValue = 0;
				commandHelper2.AllowNull = true;
				commandHelper2.TableName = "Price_Level";
				sqlBuilder.AddCommandHelper(commandHelper2);
			}
			DataSet dataSet = new DataSet();
			dataSet.EnforceConstraints = false;
			sqlBuilder.UseDistinct = false;
			try
			{
				FillDataSet(dataSet, "Price_Level", sqlBuilder);
				return dataSet;
			}
			catch
			{
				throw;
			}
		}

		public bool ExistPriceLevel(string levelName)
		{
			try
			{
				return IsTableFieldValueExist("Price_Level", "PriceLevelName", levelName);
			}
			catch
			{
				throw;
			}
		}

		public string GetPriceLevelIDByName(string levelName)
		{
			try
			{
				object obj = ExecuteSelectScalar("Price_Level", "PriceLevelName", levelName, "PriceLevelID");
				if (obj != null && obj != DBNull.Value)
				{
					return obj.ToString();
				}
			}
			catch
			{
				throw;
			}
			return "-1";
		}

		public DataSet GetPriceLevelList()
		{
			DataSet dataSet = new DataSet();
			string textCommand = "SELECT PriceLevelID [Code],PriceLevelName [Name]\r\n                           FROM Price_Level ORDER BY PriceLevelID,PriceLevelName";
			FillDataSet(dataSet, "Area", textCommand);
			return dataSet;
		}

		public DataSet GetPriceLevelComboList()
		{
			DataSet dataSet = new DataSet();
			string textCommand = "SELECT PriceLevelID [Code],PriceLevelName [Name]\r\n                           FROM Price_Level WHERE (ISINACTIVE=0 OR ISINACTIVE IS NULL) ORDER BY PriceLevelID,PriceLevelName";
			FillDataSet(dataSet, "Area", textCommand);
			return dataSet;
		}
	}
}
