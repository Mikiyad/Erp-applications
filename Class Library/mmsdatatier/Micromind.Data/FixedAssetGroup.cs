using Micromind.Common;
using Micromind.Common.Data;
using Micromind.Common.Libraries;
using System;
using System.Data;
using System.Data.SqlClient;

namespace Micromind.Data
{
	public sealed class FixedAssetGroup : StoreObject
	{
		private const string ASSETGROUPID_PARM = "@AssetGroupID";

		private const string ASSETGROUPNAME_PARM = "@AssetGroupName";

		public const string NOTE_PARM = "@Note";

		public const string INACTIVE_PARM = "@Inactive";

		public const string ASSETGROUP_TABLE = "FixedAsset_Group";

		private const string CREATEDBY_PARM = "@CreatedBy";

		private const string DATECREATED_PARM = "@DateCreated";

		private const string UPDATEDBY_PARM = "@UpdatedBy";

		private const string DATEUPDATED_PARM = "@DateUpdated";

		public FixedAssetGroup(Config config)
			: base(config)
		{
		}

		private string GetInsertUpdateText(bool isUpdate)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddInsertUpdateParameters("FixedAsset_Group", new FieldValue("AssetGroupID", "@AssetGroupID", isUpdateConditionField: true), new FieldValue("AssetGroupName", "@AssetGroupName"), new FieldValue("Inactive", "@Inactive"), new FieldValue("Note", "@Note"));
			if (isUpdate)
			{
				sqlBuilder.AddInsertUpdateParameters("FixedAsset_Group", new FieldValue("DateUpdated", "@DateUpdated", isUpdateConditionField: true, checkForNullValue: true));
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
			parameters.Add("@AssetGroupID", SqlDbType.NVarChar);
			parameters.Add("@AssetGroupName", SqlDbType.NVarChar);
			parameters.Add("@Note", SqlDbType.NVarChar);
			parameters.Add("@Inactive", SqlDbType.Bit);
			parameters["@AssetGroupID"].SourceColumn = "AssetGroupID";
			parameters["@AssetGroupName"].SourceColumn = "AssetGroupName";
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

		public bool InsertAssetGroup(FixedAssetGroupData accountAssetGroupData)
		{
			bool result = true;
			SqlCommand insertUpdateCommand = GetInsertUpdateCommand(isUpdate: false);
			SqlTransaction sqlTransaction = null;
			try
			{
				sqlTransaction = (insertUpdateCommand.Transaction = base.DBConfig.StartNewTransaction());
				result = Insert(accountAssetGroupData, "FixedAsset_Group", insertUpdateCommand);
				string text = accountAssetGroupData.AssetGroupTable.Rows[0]["AssetGroupID"].ToString();
				AddActivityLog("Fixed Asset Group", text, ActivityTypes.Add, sqlTransaction);
				UpdateTableRowInsertUpdateInfo("FixedAsset_Group", "AssetGroupID", text, sqlTransaction, isInsert: true);
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

		public bool UpdateAssetGroup(FixedAssetGroupData accountAssetGroupData)
		{
			bool flag = true;
			SqlCommand insertUpdateCommand = GetInsertUpdateCommand(isUpdate: true);
			SqlTransaction sqlTransaction = null;
			try
			{
				sqlTransaction = (insertUpdateCommand.Transaction = base.DBConfig.StartNewTransaction());
				flag = Update(accountAssetGroupData, "FixedAsset_Group", insertUpdateCommand);
				if (!flag)
				{
					return flag;
				}
				object obj = accountAssetGroupData.AssetGroupTable.Rows[0]["AssetGroupID"];
				UpdateTableRowByID("FixedAsset_Group", "AssetGroupID", "DateUpdated", obj, DateTime.Now, sqlTransaction);
				string entiyID = accountAssetGroupData.AssetGroupTable.Rows[0]["AssetGroupName"].ToString();
				AddActivityLog("Fixed Asset Group", entiyID, ActivityTypes.Update, sqlTransaction);
				UpdateTableRowInsertUpdateInfo("FixedAsset_Group", "AssetGroupID", obj, sqlTransaction, isInsert: false);
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

		public FixedAssetGroupData GetAssetGroup()
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddTable("FixedAsset_Group");
			FixedAssetGroupData fixedAssetGroupData = new FixedAssetGroupData();
			sqlBuilder.UseDistinct = false;
			FillDataSet(fixedAssetGroupData, "FixedAsset_Group", sqlBuilder);
			return fixedAssetGroupData;
		}

		public bool DeleteAssetGroup(string assetGroupID)
		{
			bool flag = true;
			try
			{
				string commandText = "DELETE FROM FixedAsset_Group WHERE AssetGroupID = '" + assetGroupID + "'";
				flag = Delete(commandText, null);
				if (!flag)
				{
					return flag;
				}
				AddActivityLog("Fixed Asset Group", assetGroupID, ActivityTypes.Delete, null);
				return flag;
			}
			catch
			{
				throw;
			}
		}

		public FixedAssetGroupData GetAssetGroupByID(string id)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			CommandHelper commandHelper = null;
			commandHelper = new CommandHelper();
			commandHelper.FieldName = "AssetGroupID";
			commandHelper.SqlFieldType = SqlDbType.NVarChar;
			commandHelper.FieldValue = id;
			commandHelper.TableName = "FixedAsset_Group";
			sqlBuilder.AddCommandHelper(commandHelper);
			sqlBuilder.IsComparing = true;
			FixedAssetGroupData fixedAssetGroupData = new FixedAssetGroupData();
			sqlBuilder.UseDistinct = false;
			FillDataSet(fixedAssetGroupData, "FixedAsset_Group", sqlBuilder);
			return fixedAssetGroupData;
		}

		public DataSet GetAssetGroupByFields(params string[] columns)
		{
			return GetAssetGroupByFields(null, isInactive: true, columns);
		}

		public DataSet GetAssetGroupByFields(string[] assetGroupID, params string[] columns)
		{
			return GetAssetGroupByFields(assetGroupID, isInactive: true, columns);
		}

		public DataSet GetAssetGroupByFields(string[] ids, bool isInactive, params string[] columns)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddTable("FixedAsset_Group");
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
				commandHelper.FieldName = "AssetGroupID";
				commandHelper.FieldValue = ids;
				commandHelper.SqlFieldType = SqlDbType.NVarChar;
				commandHelper.TableName = "FixedAsset_Group";
				sqlBuilder.AddCommandHelper(commandHelper);
			}
			DataSet dataSet = new DataSet();
			dataSet.EnforceConstraints = false;
			sqlBuilder.UseDistinct = false;
			FillDataSet(dataSet, "FixedAsset_Group", sqlBuilder);
			return dataSet;
		}

		public DataSet GetAssetGroupList()
		{
			DataSet dataSet = new DataSet();
			string textCommand = "SELECT AssetGroupID [Group Code],AssetGroupName [Group Name],Note,Inactive\r\n                           FROM FixedAsset_Group ";
			FillDataSet(dataSet, "FixedAsset_Group", textCommand);
			return dataSet;
		}

		public DataSet GetAssetGroupComboList()
		{
			DataSet dataSet = new DataSet();
			string textCommand = "SELECT AssetGroupID [Code],AssetGroupName [Name]\r\n                           FROM FixedAsset_Group ORDER BY AssetGroupID,AssetGroupName";
			FillDataSet(dataSet, "FixedAsset_Group", textCommand);
			return dataSet;
		}
	}
}
