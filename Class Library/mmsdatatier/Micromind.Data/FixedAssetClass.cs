using Micromind.Common;
using Micromind.Common.Data;
using Micromind.Common.Libraries;
using System;
using System.Data;
using System.Data.SqlClient;

namespace Micromind.Data
{
	public sealed class FixedAssetClass : StoreObject
	{
		private const string ASSETCLASSID_PARM = "@AssetClassID";

		private const string ASSETCLASSNAME_PARM = "@AssetClassName";

		private const string ASSETACCOUNTID_PARM = "@AssetAccountID";

		private const string DEPACCOUNTID_PARM = "@DepAccountID";

		private const string PROFITLOSSACCOUNTID_PARM = "@ProfitLossAccountID";

		private const string DEPFREQUENCY_PARM = "@DepFrequency";

		private const string DEPMETHOD_PARM = "@DepMethod";

		private const string ACCUMDEPACCOUNTID_PARM = "@AccumDepAccountID";

		public const string NOTE_PARM = "@Note";

		public const string INACTIVE_PARM = "@Inactive";

		public const string ASSETCLASS_TABLE = "FixedAsset_Class";

		private const string CREATEDBY_PARM = "@CreatedBy";

		private const string DATECREATED_PARM = "@DateCreated";

		private const string UPDATEDBY_PARM = "@UpdatedBy";

		private const string DATEUPDATED_PARM = "@DateUpdated";

		public FixedAssetClass(Config config)
			: base(config)
		{
		}

		private string GetInsertUpdateText(bool isUpdate)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddInsertUpdateParameters("FixedAsset_Class", new FieldValue("AssetClassID", "@AssetClassID", isUpdateConditionField: true), new FieldValue("AssetClassName", "@AssetClassName"), new FieldValue("AssetAccountID", "@AssetAccountID"), new FieldValue("DepAccountID", "@DepAccountID"), new FieldValue("AccumDepAccountID", "@AccumDepAccountID"), new FieldValue("ProfitLossAccountID", "@ProfitLossAccountID"), new FieldValue("DepFrequency", "@DepFrequency"), new FieldValue("DepMethod", "@DepMethod"), new FieldValue("Inactive", "@Inactive"), new FieldValue("Note", "@Note"));
			if (isUpdate)
			{
				sqlBuilder.AddInsertUpdateParameters("FixedAsset_Class", new FieldValue("DateUpdated", "@DateUpdated", isUpdateConditionField: true, checkForNullValue: true));
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
			parameters.Add("@AssetClassID", SqlDbType.NVarChar);
			parameters.Add("@AssetClassName", SqlDbType.NVarChar);
			parameters.Add("@AssetAccountID", SqlDbType.NVarChar);
			parameters.Add("@DepAccountID", SqlDbType.NVarChar);
			parameters.Add("@AccumDepAccountID", SqlDbType.NVarChar);
			parameters.Add("@ProfitLossAccountID", SqlDbType.NVarChar);
			parameters.Add("@DepFrequency", SqlDbType.TinyInt);
			parameters.Add("@DepMethod", SqlDbType.TinyInt);
			parameters.Add("@Note", SqlDbType.NVarChar);
			parameters.Add("@Inactive", SqlDbType.Bit);
			parameters["@AssetClassID"].SourceColumn = "AssetClassID";
			parameters["@AssetClassName"].SourceColumn = "AssetClassName";
			parameters["@AssetAccountID"].SourceColumn = "AssetAccountID";
			parameters["@DepAccountID"].SourceColumn = "DepAccountID";
			parameters["@AccumDepAccountID"].SourceColumn = "AccumDepAccountID";
			parameters["@ProfitLossAccountID"].SourceColumn = "ProfitLossAccountID";
			parameters["@DepFrequency"].SourceColumn = "DepFrequency";
			parameters["@DepMethod"].SourceColumn = "DepMethod";
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

		public bool InsertAssetClass(FixedAssetClassData accountAssetClassData)
		{
			bool result = true;
			SqlCommand insertUpdateCommand = GetInsertUpdateCommand(isUpdate: false);
			SqlTransaction sqlTransaction = null;
			try
			{
				sqlTransaction = (insertUpdateCommand.Transaction = base.DBConfig.StartNewTransaction());
				result = Insert(accountAssetClassData, "FixedAsset_Class", insertUpdateCommand);
				string text = accountAssetClassData.AssetClassTable.Rows[0]["AssetClassID"].ToString();
				AddActivityLog("Fixed Asset Class", text, ActivityTypes.Add, sqlTransaction);
				UpdateTableRowInsertUpdateInfo("FixedAsset_Class", "AssetClassID", text, sqlTransaction, isInsert: true);
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

		public bool UpdateAssetClass(FixedAssetClassData accountAssetClassData)
		{
			bool flag = true;
			SqlCommand insertUpdateCommand = GetInsertUpdateCommand(isUpdate: true);
			SqlTransaction sqlTransaction = null;
			try
			{
				sqlTransaction = (insertUpdateCommand.Transaction = base.DBConfig.StartNewTransaction());
				flag = Update(accountAssetClassData, "FixedAsset_Class", insertUpdateCommand);
				if (!flag)
				{
					return flag;
				}
				object obj = accountAssetClassData.AssetClassTable.Rows[0]["AssetClassID"];
				UpdateTableRowByID("FixedAsset_Class", "AssetClassID", "DateUpdated", obj, DateTime.Now, sqlTransaction);
				string entiyID = accountAssetClassData.AssetClassTable.Rows[0]["AssetClassName"].ToString();
				AddActivityLog("Fixed Asset Class", entiyID, ActivityTypes.Update, sqlTransaction);
				UpdateTableRowInsertUpdateInfo("FixedAsset_Class", "AssetClassID", obj, sqlTransaction, isInsert: false);
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

		public FixedAssetClassData GetAssetClass()
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddTable("FixedAsset_Class");
			FixedAssetClassData fixedAssetClassData = new FixedAssetClassData();
			sqlBuilder.UseDistinct = false;
			FillDataSet(fixedAssetClassData, "FixedAsset_Class", sqlBuilder);
			return fixedAssetClassData;
		}

		public bool DeleteAssetClass(string assetClassID)
		{
			bool flag = true;
			try
			{
				string commandText = "DELETE FROM FixedAsset_Class WHERE AssetClassID = '" + assetClassID + "'";
				flag = Delete(commandText, null);
				if (!flag)
				{
					return flag;
				}
				AddActivityLog("Fixed Asset Class", assetClassID, ActivityTypes.Delete, null);
				return flag;
			}
			catch
			{
				throw;
			}
		}

		public FixedAssetClassData GetAssetClassByID(string id)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			CommandHelper commandHelper = null;
			commandHelper = new CommandHelper();
			commandHelper.FieldName = "AssetClassID";
			commandHelper.SqlFieldType = SqlDbType.NVarChar;
			commandHelper.FieldValue = id;
			commandHelper.TableName = "FixedAsset_Class";
			sqlBuilder.AddCommandHelper(commandHelper);
			sqlBuilder.IsComparing = true;
			FixedAssetClassData fixedAssetClassData = new FixedAssetClassData();
			sqlBuilder.UseDistinct = false;
			FillDataSet(fixedAssetClassData, "FixedAsset_Class", sqlBuilder);
			return fixedAssetClassData;
		}

		public DataSet GetAssetClassByFields(params string[] columns)
		{
			return GetAssetClassByFields(null, isInactive: true, columns);
		}

		public DataSet GetAssetClassByFields(string[] assetClassID, params string[] columns)
		{
			return GetAssetClassByFields(assetClassID, isInactive: true, columns);
		}

		public DataSet GetAssetClassByFields(string[] ids, bool isInactive, params string[] columns)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddTable("FixedAsset_Class");
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
				commandHelper.FieldName = "AssetClassID";
				commandHelper.FieldValue = ids;
				commandHelper.SqlFieldType = SqlDbType.NVarChar;
				commandHelper.TableName = "FixedAsset_Class";
				sqlBuilder.AddCommandHelper(commandHelper);
			}
			DataSet dataSet = new DataSet();
			dataSet.EnforceConstraints = false;
			sqlBuilder.UseDistinct = false;
			FillDataSet(dataSet, "FixedAsset_Class", sqlBuilder);
			return dataSet;
		}

		public DataSet GetAssetClassList()
		{
			DataSet dataSet = new DataSet();
			string textCommand = "SELECT AssetClassID [Class Code],AssetClassName [Class Name],Note,Inactive\r\n                           FROM FixedAsset_Class ";
			FillDataSet(dataSet, "FixedAsset_Class", textCommand);
			return dataSet;
		}

		public DataSet GetAssetClassComboList()
		{
			DataSet dataSet = new DataSet();
			string textCommand = "SELECT AssetClassID [Code],AssetClassName [Name]\r\n                           FROM FixedAsset_Class ORDER BY AssetClassID,AssetClassName";
			FillDataSet(dataSet, "FixedAsset_Class", textCommand);
			return dataSet;
		}
	}
}
