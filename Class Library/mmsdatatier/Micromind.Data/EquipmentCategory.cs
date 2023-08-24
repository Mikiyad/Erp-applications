using Micromind.Common;
using Micromind.Common.Data;
using Micromind.Common.Libraries;
using System;
using System.Data;
using System.Data.SqlClient;

namespace Micromind.Data
{
	public sealed class EquipmentCategory : StoreObject
	{
		private const string CATEGORYID_PARM = "@EquipmentCategoryID";

		private const string CATEGORYNAME_PARM = "@EquipmentCategoryName";

		public const string ISINACTIVE_PARM = "@IsInactive";

		public const string NOTE_PARM = "@Note";

		public const string EQUIPMENTCATEGORY_TABLE = "EA_Equipment_Category";

		private const string CREATEDBY_PARM = "@CreatedBy";

		private const string DATECREATED_PARM = "@DateCreated";

		private const string UPDATEDBY_PARM = "@UpdatedBy";

		private const string DATEUPDATED_PARM = "@DateUpdated";

		public EquipmentCategory(Config config)
			: base(config)
		{
		}

		private string GetInsertUpdateText(bool isUpdate)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddInsertUpdateParameters("EA_Equipment_Category", new FieldValue("EquipmentCategoryID", "@EquipmentCategoryID", isUpdateConditionField: true), new FieldValue("EquipmentCategoryName", "@EquipmentCategoryName"), new FieldValue("IsInactive", "@IsInactive"), new FieldValue("Note", "@Note"));
			if (isUpdate)
			{
				sqlBuilder.AddInsertUpdateParameters("EA_Equipment_Category", new FieldValue("DateUpdated", "@DateUpdated", isUpdateConditionField: true, checkForNullValue: true));
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
			parameters.Add("@EquipmentCategoryID", SqlDbType.NVarChar);
			parameters.Add("@EquipmentCategoryName", SqlDbType.NVarChar);
			parameters.Add("@IsInactive", SqlDbType.Bit);
			parameters.Add("@Note", SqlDbType.NVarChar);
			parameters["@EquipmentCategoryID"].SourceColumn = "EquipmentCategoryID";
			parameters["@EquipmentCategoryName"].SourceColumn = "EquipmentCategoryName";
			parameters["@IsInactive"].SourceColumn = "IsInactive";
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

		public bool InsertEquipmentCategory(EquipmentCategoryData accountEquipmentCategoryData)
		{
			bool result = true;
			SqlCommand insertUpdateCommand = GetInsertUpdateCommand(isUpdate: false);
			SqlTransaction sqlTransaction = null;
			try
			{
				sqlTransaction = (insertUpdateCommand.Transaction = base.DBConfig.StartNewTransaction());
				result = Insert(accountEquipmentCategoryData, "EA_Equipment_Category", insertUpdateCommand);
				string text = accountEquipmentCategoryData.EquipmentCategoryTable.Rows[0]["EquipmentCategoryID"].ToString();
				AddActivityLog("Equipment Category", text, ActivityTypes.Add, sqlTransaction);
				UpdateTableRowInsertUpdateInfo("EA_Equipment_Category", "EquipmentCategoryID", text, sqlTransaction, isInsert: true);
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

		public bool UpdateEquipmentCategory(EquipmentCategoryData accountEquipmentCategoryData)
		{
			bool flag = true;
			SqlCommand insertUpdateCommand = GetInsertUpdateCommand(isUpdate: true);
			SqlTransaction sqlTransaction = null;
			try
			{
				sqlTransaction = (insertUpdateCommand.Transaction = base.DBConfig.StartNewTransaction());
				flag = Update(accountEquipmentCategoryData, "EA_Equipment_Category", insertUpdateCommand);
				if (!flag)
				{
					return flag;
				}
				object obj = accountEquipmentCategoryData.EquipmentCategoryTable.Rows[0]["EquipmentCategoryID"];
				UpdateTableRowByID("EA_Equipment_Category", "EquipmentCategoryID", "DateUpdated", obj, DateTime.Now, sqlTransaction);
				string entiyID = accountEquipmentCategoryData.EquipmentCategoryTable.Rows[0]["EquipmentCategoryName"].ToString();
				AddActivityLog("Equipment Category", entiyID, ActivityTypes.Update, sqlTransaction);
				UpdateTableRowInsertUpdateInfo("EA_Equipment_Category", "EquipmentCategoryID", obj, sqlTransaction, isInsert: false);
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

		public EquipmentCategoryData GetEquipmentCategory()
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddTable("EA_Equipment_Category");
			EquipmentCategoryData equipmentCategoryData = new EquipmentCategoryData();
			sqlBuilder.UseDistinct = false;
			FillDataSet(equipmentCategoryData, "EA_Equipment_Category", sqlBuilder);
			return equipmentCategoryData;
		}

		public bool DeleteEquipmentCategory(string CategoryID)
		{
			bool flag = true;
			try
			{
				string commandText = "DELETE FROM EA_Equipment_Category WHERE EquipmentCategoryID = '" + CategoryID + "'";
				flag = Delete(commandText, null);
				if (!flag)
				{
					return flag;
				}
				AddActivityLog("Equipment Category", CategoryID, ActivityTypes.Delete, null);
				return flag;
			}
			catch
			{
				throw;
			}
		}

		public EquipmentCategoryData GetEquipmentCategoryByID(string id)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			CommandHelper commandHelper = null;
			commandHelper = new CommandHelper();
			commandHelper.FieldName = "EquipmentCategoryID";
			commandHelper.SqlFieldType = SqlDbType.NVarChar;
			commandHelper.FieldValue = id;
			commandHelper.TableName = "EA_Equipment_Category";
			sqlBuilder.AddCommandHelper(commandHelper);
			sqlBuilder.IsComparing = true;
			EquipmentCategoryData equipmentCategoryData = new EquipmentCategoryData();
			sqlBuilder.UseDistinct = false;
			FillDataSet(equipmentCategoryData, "EA_Equipment_Category", sqlBuilder);
			return equipmentCategoryData;
		}

		public DataSet GetEquipmentCategoryByFields(params string[] columns)
		{
			return GetEquipmentCategoryByFields(null, isInactive: true, columns);
		}

		public DataSet GetEquipmentCategoryByFields(string[] EquipmentCategoryID, params string[] columns)
		{
			return GetEquipmentCategoryByFields(EquipmentCategoryID, isInactive: true, columns);
		}

		public DataSet GetEquipmentCategoryByFields(string[] ids, bool isInactive, params string[] columns)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddTable("EA_Equipment_Category");
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
				commandHelper.FieldName = "EquipmentCategoryID";
				commandHelper.FieldValue = ids;
				commandHelper.SqlFieldType = SqlDbType.NVarChar;
				commandHelper.TableName = "EA_Equipment_Category";
				sqlBuilder.AddCommandHelper(commandHelper);
			}
			DataSet dataSet = new DataSet();
			dataSet.EnforceConstraints = false;
			sqlBuilder.UseDistinct = false;
			FillDataSet(dataSet, "EA_Equipment_Category", sqlBuilder);
			return dataSet;
		}

		public DataSet GetEquipmentCategoryList()
		{
			DataSet dataSet = new DataSet();
			string textCommand = "SELECT EquipmentCategoryID [Category Code],EquipmentCategoryName [Category Name],Note,IsInactive [Inactive]\r\n                           FROM EA_Equipment_Category ";
			FillDataSet(dataSet, "EA_Equipment_Category", textCommand);
			return dataSet;
		}

		public DataSet GetEquipmentCategoryComboList()
		{
			DataSet dataSet = new DataSet();
			string textCommand = "SELECT EquipmentCategoryID [Code],EquipmentCategoryName [Name]\r\n                           FROM EA_Equipment_Category WHERE ISINACTIVE<>1 ORDER BY EquipmentCategoryID,EquipmentCategoryName";
			FillDataSet(dataSet, "EA_Equipment_Category", textCommand);
			return dataSet;
		}
	}
}
