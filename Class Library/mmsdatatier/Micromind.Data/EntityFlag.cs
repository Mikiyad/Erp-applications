using Micromind.Common;
using Micromind.Common.Data;
using Micromind.Common.Libraries;
using System;
using System.Data;
using System.Data.SqlClient;

namespace Micromind.Data
{
	public sealed class EntityFlag : StoreObject
	{
		private const string ENTITYFLAGID_PARM = "@FlagID";

		private const string ENTITYID_PARM = "@EntityID";

		private const string ENTITYTYPE_PARM = "@EntityType";

		private const string ENTITYFLAGNAME_PARM = "@FlagName";

		public const string COLOR_PARM = "@Color";

		public const string ENTITYFLAG_TABLE = "Entity_Flag";

		private const string CREATEDBY_PARM = "@CreatedBy";

		private const string DATECREATED_PARM = "@DateCreated";

		private const string UPDATEDBY_PARM = "@UpdatedBy";

		private const string DATEUPDATED_PARM = "@DateUpdated";

		public EntityFlag(Config config)
			: base(config)
		{
		}

		private string GetInsertUpdateText(bool isUpdate)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddInsertUpdateParameters("Entity_Flag", new FieldValue("FlagName", "@FlagName"), new FieldValue("EntityType", "@EntityType"), new FieldValue("Color", "@Color"));
			if (isUpdate)
			{
				sqlBuilder.AddInsertUpdateParameters("Entity_Flag", new FieldValue("DateUpdated", "@DateUpdated", isUpdateConditionField: true, checkForNullValue: true));
				sqlBuilder.AddInsertUpdateParameters("Entity_Flag", new FieldValue("FlagID", "@FlagID", isUpdateConditionField: true, checkForNullValue: true));
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
			parameters.Add("@FlagID", SqlDbType.Int);
			parameters.Add("@FlagName", SqlDbType.NVarChar);
			parameters.Add("@EntityType", SqlDbType.TinyInt);
			parameters.Add("@Color", SqlDbType.Int);
			parameters["@FlagID"].SourceColumn = "FlagID";
			parameters["@FlagName"].SourceColumn = "FlagName";
			parameters["@EntityType"].SourceColumn = "EntityType";
			parameters["@Color"].SourceColumn = "Color";
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

		private string GetInsertUpdateDetailText(bool isUpdate)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddInsertUpdateParameters("Entity_Flag_Detail", new FieldValue("EntityID", "@EntityID"), new FieldValue("EntityType", "@EntityType"), new FieldValue("FlagID", "@FlagID"));
			if (isUpdate)
			{
				return sqlBuilder.GetUpdateExpression();
			}
			return sqlBuilder.GetInsertExpression();
		}

		private SqlCommand GetInsertUpdateDetailCommand(bool isUpdate)
		{
			SqlParameterCollection parameters;
			if (isUpdate)
			{
				if (updateCommand != null)
				{
					return updateCommand;
				}
				updateCommand = new SqlCommand(GetInsertUpdateDetailText(isUpdate: true), base.DBConfig.Connection);
				updateCommand.CommandType = CommandType.Text;
				parameters = updateCommand.Parameters;
			}
			else
			{
				if (insertCommand != null)
				{
					return insertCommand;
				}
				insertCommand = new SqlCommand(GetInsertUpdateDetailText(isUpdate: false), base.DBConfig.Connection);
				insertCommand.CommandType = CommandType.Text;
				parameters = insertCommand.Parameters;
			}
			parameters.Add("@FlagID", SqlDbType.NVarChar);
			parameters.Add("@EntityID", SqlDbType.NVarChar);
			parameters.Add("@EntityType", SqlDbType.TinyInt);
			parameters["@FlagID"].SourceColumn = "FlagID";
			parameters["@EntityID"].SourceColumn = "EntityID";
			parameters["@EntityType"].SourceColumn = "EntityType";
			if (isUpdate)
			{
				return updateCommand;
			}
			return insertCommand;
		}

		public bool SetFlag(int entityType, string entityID, int flagID, bool removeFlag)
		{
			bool result = true;
			GetInsertUpdateCommand(isUpdate: false);
			SqlTransaction sqlTransaction = null;
			try
			{
				string text = "INSERT INTO   Entity_Flag_Detail  (EntityID,EntityType,FlagID) SELECT '" + entityID + "'," + entityType + "," + flagID;
				text = text + " WHERE NOT Exists(SELECT * FROM Entity_Flag_Detail WHERE EntityID = '" + entityID + "' AND EntityType = " + entityType + " AND FlagID = " + flagID + ")";
				if (removeFlag)
				{
					text = "DELETE FROM Entity_Flag_Detail WHERE EntityID = '" + entityID + "' AND EntityType = " + entityType;
					if (flagID != -1)
					{
						text = text + " AND FlagID = " + flagID;
					}
				}
				result = (ExecuteNonQuery(text, sqlTransaction) >= 0);
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

		public bool InsertEntityFlag(EntityFlagData accountEntityFlagData)
		{
			bool result = true;
			SqlCommand insertUpdateCommand = GetInsertUpdateCommand(isUpdate: false);
			SqlTransaction sqlTransaction = null;
			try
			{
				sqlTransaction = (insertUpdateCommand.Transaction = base.DBConfig.StartNewTransaction());
				result = Insert(accountEntityFlagData, "Entity_Flag", insertUpdateCommand);
				string text = accountEntityFlagData.EntityFlagTable.Rows[0]["FlagID"].ToString();
				AddActivityLog("Entity Flag", text, ActivityTypes.Add, sqlTransaction);
				UpdateTableRowInsertUpdateInfo("Entity_Flag", "FlagID", text, sqlTransaction, isInsert: true);
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

		public bool InsertEntityFlagAssignment(EntityFlagData data, string entityID)
		{
			bool flag = true;
			SqlCommand insertUpdateDetailCommand = GetInsertUpdateDetailCommand(isUpdate: false);
			SqlTransaction sqlTransaction = null;
			try
			{
				sqlTransaction = base.DBConfig.StartNewTransaction();
				string exp = "DELETE FROM ENTITY_FLAG_DETAIL WHERE ENTITYid = '" + entityID + "'";
				flag = (ExecuteNonQuery(exp, sqlTransaction) >= 0);
				if (data.EntityFlagDetailTable.Rows.Count <= 0)
				{
					return flag;
				}
				insertUpdateDetailCommand.Transaction = sqlTransaction;
				flag &= Insert(data, "Entity_Flag_Detail", insertUpdateDetailCommand);
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

		public bool UpdateEntityFlag(EntityFlagData accountEntityFlagData)
		{
			bool flag = true;
			SqlCommand insertUpdateCommand = GetInsertUpdateCommand(isUpdate: true);
			SqlTransaction sqlTransaction = null;
			try
			{
				sqlTransaction = (insertUpdateCommand.Transaction = base.DBConfig.StartNewTransaction());
				flag = Update(accountEntityFlagData, "Entity_Flag", insertUpdateCommand);
				if (!flag)
				{
					return flag;
				}
				object obj = accountEntityFlagData.EntityFlagTable.Rows[0]["FlagID"];
				UpdateTableRowByID("Entity_Flag", "FlagID", "DateUpdated", obj, DateTime.Now, sqlTransaction);
				string entiyID = accountEntityFlagData.EntityFlagTable.Rows[0]["FlagName"].ToString();
				AddActivityLog("Entity Flag", entiyID, ActivityTypes.Update, sqlTransaction);
				UpdateTableRowInsertUpdateInfo("Entity_Flag", "FlagID", obj, sqlTransaction, isInsert: false);
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

		public EntityFlagData GetEntityFlag()
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddTable("Entity_Flag");
			EntityFlagData entityFlagData = new EntityFlagData();
			sqlBuilder.UseDistinct = false;
			FillDataSet(entityFlagData, "Entity_Flag", sqlBuilder);
			return entityFlagData;
		}

		public bool DeleteEntityFlag(string entityFlagID, EntityTypesEnum entityType)
		{
			bool flag = true;
			try
			{
				string commandText = "DELETE FROM Entity_Flag WHERE FlagID = '" + entityFlagID + "' AND EntityType = " + (int)entityType;
				flag = Delete(commandText, null);
				if (!flag)
				{
					return flag;
				}
				AddActivityLog("Entity Flag", entityFlagID, ActivityTypes.Delete, null);
				return flag;
			}
			catch
			{
				throw;
			}
		}

		public EntityFlagData GetEntityFlagByID(string id)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			CommandHelper commandHelper = null;
			commandHelper = new CommandHelper();
			commandHelper.FieldName = "FlagID";
			commandHelper.SqlFieldType = SqlDbType.NVarChar;
			commandHelper.FieldValue = id;
			commandHelper.TableName = "Entity_Flag";
			sqlBuilder.AddCommandHelper(commandHelper);
			sqlBuilder.IsComparing = true;
			EntityFlagData entityFlagData = new EntityFlagData();
			sqlBuilder.UseDistinct = false;
			FillDataSet(entityFlagData, "Entity_Flag", sqlBuilder);
			return entityFlagData;
		}

		public DataSet GetEntityFlagByFields(params string[] columns)
		{
			return GetEntityFlagByFields(null, isInactive: true, columns);
		}

		public DataSet GetEntityFlagByFields(string[] entityFlagID, params string[] columns)
		{
			return GetEntityFlagByFields(entityFlagID, isInactive: true, columns);
		}

		public DataSet GetEntityFlagByFields(string[] ids, bool isInactive, params string[] columns)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddTable("Entity_Flag");
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
				commandHelper.FieldName = "FlagID";
				commandHelper.FieldValue = ids;
				commandHelper.SqlFieldType = SqlDbType.NVarChar;
				commandHelper.TableName = "Entity_Flag";
				sqlBuilder.AddCommandHelper(commandHelper);
			}
			DataSet dataSet = new DataSet();
			dataSet.EnforceConstraints = false;
			sqlBuilder.UseDistinct = false;
			FillDataSet(dataSet, "Entity_Flag", sqlBuilder);
			return dataSet;
		}

		public DataSet GetEntityFlagList(EntityTypesEnum entityType)
		{
			DataSet dataSet = new DataSet();
			string textCommand = "SELECT FlagID [Code],FlagName [Name],Color\r\n                           FROM Entity_Flag WHERE EntityType = " + (int)entityType;
			FillDataSet(dataSet, "Entity_Flag", textCommand);
			return dataSet;
		}

		public DataSet GetActiveEntityFlagList(EntityTypesEnum entityType)
		{
			DataSet dataSet = new DataSet();
			string textCommand = "SELECT FlagID [Code],FlagName [Name] \r\n                           FROM Entity_Flag WHERE IsNull(Inactive,'False') = 'False' ";
			FillDataSet(dataSet, "Entity_Flag", textCommand);
			return dataSet;
		}

		public DataSet GetEntityAssignedFlagsList(string entityID, EntityTypesEnum entityType)
		{
			DataSet dataSet = new DataSet();
			string text = "";
			text = "SELECT EFD.*,EF.FlagName,EF.Color FROM Entity_Flag_Detail EFD INNER JOIN Entity_Flag EF ON EF.FlagID = EFD.FlagID WHERE EFD.EntityType =   " + (int)entityType;
			FillDataSet(dataSet, "Entity_Flag_Detail", text);
			return dataSet;
		}

		public DataSet GetEntityAssignedFlagsList(EntityTypesEnum entityType)
		{
			DataSet dataSet = new DataSet();
			string text = "";
			text = "SELECT EFD.*,EF.FlagName,EF.Color FROM Entity_Flag_Detail EFD INNER JOIN Entity_Flag EF ON EF.FlagID = EFD.FlagID WHERE EFD.EntityType =   " + (int)entityType;
			FillDataSet(dataSet, "Entity_Flag_Detail", text);
			return dataSet;
		}

		public DataSet GetEntityFlagComboList()
		{
			DataSet dataSet = new DataSet();
			string textCommand = "SELECT FlagID [Code],FlagName [Name]\r\n                           FROM Entity_Flag ORDER BY FlagID,FlagName";
			FillDataSet(dataSet, "Entity_Flag", textCommand);
			return dataSet;
		}
	}
}
