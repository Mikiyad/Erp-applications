using Micromind.Common;
using Micromind.Common.Data;
using Micromind.Common.Libraries;
using System;
using System.Data;
using System.Data.SqlClient;

namespace Micromind.Data
{
	public sealed class FixedAssetLocation : StoreObject
	{
		private const string ASSETLOCATIONID_PARM = "@AssetLocationID";

		private const string ASSETLOCATIONNAME_PARM = "@AssetLocationName";

		private const string NOTE_PARM = "@Note";

		private const string INACTIVE_PARM = "@Inactive";

		private const string ASSETLOCATION_TABLE = "FixedAsset_Location";

		private const string DEPACCOUNTID_PARM = "@DepAccountID";

		private const string COMPANYID_PARM = "@CompanyID";

		private const string COUNTRYID_PARM = "@CountryID";

		private const string CREATEDBY_PARM = "@CreatedBy";

		private const string DATECREATED_PARM = "@DateCreated";

		private const string UPDATEDBY_PARM = "@UpdatedBy";

		private const string DATEUPDATED_PARM = "@DateUpdated";

		public FixedAssetLocation(Config config)
			: base(config)
		{
		}

		private string GetInsertUpdateText(bool isUpdate)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddInsertUpdateParameters("FixedAsset_Location", new FieldValue("AssetLocationID", "@AssetLocationID", isUpdateConditionField: true), new FieldValue("AssetLocationName", "@AssetLocationName"), new FieldValue("DepAccountID", "@DepAccountID"), new FieldValue("CompanyID", "@CompanyID"), new FieldValue("CountryID", "@CountryID"), new FieldValue("Inactive", "@Inactive"), new FieldValue("Note", "@Note"));
			if (isUpdate)
			{
				sqlBuilder.AddInsertUpdateParameters("FixedAsset_Location", new FieldValue("DateUpdated", "@DateUpdated", isUpdateConditionField: true, checkForNullValue: true));
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
			parameters.Add("@AssetLocationID", SqlDbType.NVarChar);
			parameters.Add("@DepAccountID", SqlDbType.NVarChar);
			parameters.Add("@AssetLocationName", SqlDbType.NVarChar);
			parameters.Add("@Note", SqlDbType.NVarChar);
			parameters.Add("@CompanyID", SqlDbType.NVarChar);
			parameters.Add("@CountryID", SqlDbType.NVarChar);
			parameters.Add("@Inactive", SqlDbType.Bit);
			parameters["@AssetLocationID"].SourceColumn = "AssetLocationID";
			parameters["@DepAccountID"].SourceColumn = "DepAccountID";
			parameters["@AssetLocationName"].SourceColumn = "AssetLocationName";
			parameters["@CompanyID"].SourceColumn = "CompanyID";
			parameters["@CountryID"].SourceColumn = "CountryID";
			parameters["@Note"].SourceColumn = "Note";
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

		public bool InsertAssetLocation(FixedAssetLocationData accountAssetLocationData)
		{
			bool result = true;
			SqlCommand insertUpdateCommand = GetInsertUpdateCommand(isUpdate: false);
			SqlTransaction sqlTransaction = null;
			try
			{
				sqlTransaction = (insertUpdateCommand.Transaction = base.DBConfig.StartNewTransaction());
				result = Insert(accountAssetLocationData, "FixedAsset_Location", insertUpdateCommand);
				string text = accountAssetLocationData.AssetLocationTable.Rows[0]["AssetLocationID"].ToString();
				AddActivityLog("Fixed Asset Location", text, ActivityTypes.Add, sqlTransaction);
				UpdateTableRowInsertUpdateInfo("FixedAsset_Location", "AssetLocationID", text, sqlTransaction, isInsert: true);
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

		public bool UpdateAssetLocation(FixedAssetLocationData accountAssetLocationData)
		{
			bool flag = true;
			SqlCommand insertUpdateCommand = GetInsertUpdateCommand(isUpdate: true);
			SqlTransaction sqlTransaction = null;
			try
			{
				sqlTransaction = (insertUpdateCommand.Transaction = base.DBConfig.StartNewTransaction());
				flag = Update(accountAssetLocationData, "FixedAsset_Location", insertUpdateCommand);
				if (!flag)
				{
					return flag;
				}
				object obj = accountAssetLocationData.AssetLocationTable.Rows[0]["AssetLocationID"];
				UpdateTableRowByID("FixedAsset_Location", "AssetLocationID", "DateUpdated", obj, DateTime.Now, sqlTransaction);
				string entiyID = accountAssetLocationData.AssetLocationTable.Rows[0]["AssetLocationName"].ToString();
				AddActivityLog("Fixed Asset Location", entiyID, ActivityTypes.Update, sqlTransaction);
				UpdateTableRowInsertUpdateInfo("FixedAsset_Location", "AssetLocationID", obj, sqlTransaction, isInsert: false);
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

		public FixedAssetLocationData GetAssetLocation()
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddTable("FixedAsset_Location");
			FixedAssetLocationData fixedAssetLocationData = new FixedAssetLocationData();
			sqlBuilder.UseDistinct = false;
			FillDataSet(fixedAssetLocationData, "FixedAsset_Location", sqlBuilder);
			return fixedAssetLocationData;
		}

		public bool DeleteAssetLocation(string assetLocationID)
		{
			bool flag = true;
			try
			{
				string commandText = "DELETE FROM FixedAsset_Location WHERE AssetLocationID = '" + assetLocationID + "'";
				flag = Delete(commandText, null);
				if (!flag)
				{
					return flag;
				}
				AddActivityLog("Fixed Asset Location", assetLocationID, ActivityTypes.Delete, null);
				return flag;
			}
			catch
			{
				throw;
			}
		}

		public FixedAssetLocationData GetAssetLocationByID(string id)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			CommandHelper commandHelper = null;
			commandHelper = new CommandHelper();
			commandHelper.FieldName = "AssetLocationID";
			commandHelper.SqlFieldType = SqlDbType.NVarChar;
			commandHelper.FieldValue = id;
			commandHelper.TableName = "FixedAsset_Location";
			sqlBuilder.AddCommandHelper(commandHelper);
			sqlBuilder.IsComparing = true;
			FixedAssetLocationData fixedAssetLocationData = new FixedAssetLocationData();
			sqlBuilder.UseDistinct = false;
			FillDataSet(fixedAssetLocationData, "FixedAsset_Location", sqlBuilder);
			return fixedAssetLocationData;
		}

		public DataSet GetAssetLocationByFields(params string[] columns)
		{
			return GetAssetLocationByFields(null, isInactive: true, columns);
		}

		public DataSet GetAssetLocationByFields(string[] assetLocationID, params string[] columns)
		{
			return GetAssetLocationByFields(assetLocationID, isInactive: true, columns);
		}

		public DataSet GetAssetLocationByFields(string[] ids, bool isInactive, params string[] columns)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddTable("FixedAsset_Location");
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
				commandHelper.FieldName = "AssetLocationID";
				commandHelper.FieldValue = ids;
				commandHelper.SqlFieldType = SqlDbType.NVarChar;
				commandHelper.TableName = "FixedAsset_Location";
				sqlBuilder.AddCommandHelper(commandHelper);
			}
			DataSet dataSet = new DataSet();
			dataSet.EnforceConstraints = false;
			sqlBuilder.UseDistinct = false;
			FillDataSet(dataSet, "FixedAsset_Location", sqlBuilder);
			return dataSet;
		}

		public DataSet GetAssetLocationList()
		{
			DataSet dataSet = new DataSet();
			string textCommand = "SELECT AssetLocationID [Location Code],AssetLocationName [Location Name],Note,Inactive\r\n                           FROM FixedAsset_Location ";
			FillDataSet(dataSet, "FixedAsset_Location", textCommand);
			return dataSet;
		}

		public DataSet GetAssetLocationComboList()
		{
			DataSet dataSet = new DataSet();
			string textCommand = "SELECT AssetLocationID [Code],AssetLocationName [Name]\r\n                           FROM FixedAsset_Location ORDER BY AssetLocationID,AssetLocationName";
			FillDataSet(dataSet, "FixedAsset_Location", textCommand);
			return dataSet;
		}
	}
}
