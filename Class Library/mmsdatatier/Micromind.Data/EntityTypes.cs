using Micromind.Common;
using Micromind.Common.Data;
using Micromind.Common.Libraries;
using System;
using System.Data;
using System.Data.SqlClient;

namespace Micromind.Data
{
	public sealed class EntityTypes : StoreObject
	{
		private const string ENTITYTYPEID_PARM = "@EntityTypeID";

		private const string ENTITYTYPENAME_PARM = "@EntityTypeName";

		private const string ENTITYTYPE_PARM = "@EntityType";

		private const string NOTE_PARM = "@Note";

		private const string DATETIMESTAMP_PARM = "@DateTimeStamp";

		public bool CheckConcurrency = true;

		public EntityTypes(Config config)
			: base(config)
		{
		}

		private string GetInsertText()
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddInsertUpdateParameters("[Entity Types]", new FieldValue("EntityTypeName", "@EntityTypeName"), new FieldValue("EntityType", "@EntityType"), new FieldValue("Note", "@Note"));
			return sqlBuilder.GetInsertExpression();
		}

		private string GetUpdateText()
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddInsertUpdateParameters("[Entity Types]", new FieldValue("EntityTypeName", "@EntityTypeName"), new FieldValue("EntityType", "@EntityType"), new FieldValue("Note", "@Note"));
			if (CheckConcurrency)
			{
				sqlBuilder.AddInsertUpdateParameters("[Entity Types]", new FieldValue("DateTimeStamp", "@DateTimeStamp", isUpdateConditionField: true, checkForNullValue: true));
			}
			return sqlBuilder.GetUpdateExpression();
		}

		private SqlCommand GetInsertCommand()
		{
			if (insertCommand == null)
			{
				insertCommand = new SqlCommand(GetInsertText(), base.DBConfig.Connection);
				insertCommand.CommandType = CommandType.Text;
				SqlParameterCollection parameters = insertCommand.Parameters;
				parameters.Add("@EntityTypeName", SqlDbType.NVarChar);
				parameters.Add("@EntityType", SqlDbType.TinyInt);
				parameters.Add("@Note", SqlDbType.NText);
				parameters["@EntityTypeName"].SourceColumn = "EntityTypeName";
				parameters["@EntityType"].SourceColumn = "EntityType";
				parameters["@Note"].SourceColumn = "Note";
			}
			return insertCommand;
		}

		private SqlCommand GetUpdateCommand()
		{
			if (updateCommand == null)
			{
				insertCommand = new SqlCommand(GetInsertText(), base.DBConfig.Connection);
				insertCommand.CommandType = CommandType.Text;
				SqlParameterCollection parameters = insertCommand.Parameters;
				parameters.Add("@EntityTypeName", SqlDbType.NVarChar);
				parameters.Add("@EntityType", SqlDbType.TinyInt);
				parameters.Add("@Note", SqlDbType.NText);
				parameters.Add("@DateTimeStamp", SqlDbType.DateTime);
				parameters["@EntityTypeName"].SourceColumn = "EntityTypeName";
				parameters["@EntityType"].SourceColumn = "EntityType";
				parameters["@Note"].SourceColumn = "Note";
				parameters["@DateTimeStamp"].SourceColumn = "DateTimeStamp";
			}
			return updateCommand;
		}

		public bool InsertEntityType(EntityTypeData entityTypeData)
		{
			bool result = true;
			SqlCommand insertCommand = GetInsertCommand();
			SqlTransaction sqlTransaction = null;
			try
			{
				sqlTransaction = (insertCommand.Transaction = base.DBConfig.StartNewTransaction());
				result = Insert(entityTypeData, "[Entity Types]", insertCommand);
				object insertedRowIdentity = GetInsertedRowIdentity("[Entity Types]", insertCommand);
				entityTypeData.EntityTypeTable.Rows[0]["EntityTypeID"] = insertedRowIdentity;
				UpdateTableRowByID("[Entity Types]", "EntityTypeID", "DateTimeStamp", insertedRowIdentity, DateTime.Now, sqlTransaction);
				string entiyID = entityTypeData.EntityTypeTable.Rows[0]["EntityTypeName"].ToString();
				AddActivityLog("Entity Type", entiyID, ActivityTypes.Add, sqlTransaction);
				UpdateTableRowInsertUpdateInfo("[Entity Types]", "EntityTypeID", insertedRowIdentity, sqlTransaction, isInsert: true);
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

		public bool UpdateEntityType(EntityTypeData entityTypeData)
		{
			bool result = true;
			SqlCommand updateCommand = GetUpdateCommand();
			SqlTransaction sqlTransaction = null;
			try
			{
				sqlTransaction = (updateCommand.Transaction = base.DBConfig.StartNewTransaction());
				result = Update(entityTypeData, "[Entity Types]", updateCommand);
				object obj = entityTypeData.EntityTypeTable.Rows[0]["EntityTypeID"];
				UpdateTableRowByID("[Entity Types]", "EntityTypeID", "DateTimeStamp", obj, DateTime.Now, sqlTransaction);
				string entiyID = entityTypeData.EntityTypeTable.Rows[0]["EntityTypeName"].ToString();
				AddActivityLog("Entity Type", entiyID, ActivityTypes.Update, sqlTransaction);
				UpdateTableRowInsertUpdateInfo("[Entity Types]", "EntityTypeID", obj, sqlTransaction, isInsert: false);
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

		public DataSet GetEntityTypesByFields(EntityTypesEnum[] entityTypes, int[] entityTypeIDs, params string[] columns)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			foreach (string text in columns)
			{
				ApplicationAssert.CheckCondition(text.IndexOf(".") >= 0, "A table name with the column name must be specified. eg. table.column.", 0);
				string tableName = text.Substring(0, text.IndexOf("."));
				string columnName = text.Substring(text.IndexOf(".") + 1);
				sqlBuilder.AddColumn(tableName, columnName);
			}
			if (entityTypeIDs != null && entityTypeIDs.Length != 0)
			{
				CommandHelper commandHelper = new CommandHelper();
				commandHelper.FieldName = "EntityTypeID";
				commandHelper.FieldValue = entityTypeIDs;
				commandHelper.TableName = "[Entity Types]";
				sqlBuilder.AddCommandHelper(commandHelper);
			}
			if (entityTypes != null && entityTypes.Length != 0)
			{
				CommandHelper commandHelper2 = new CommandHelper();
				commandHelper2.FieldName = "EntityType";
				commandHelper2.FieldValue = entityTypes;
				commandHelper2.TableName = "[Entity Types]";
				sqlBuilder.AddCommandHelper(commandHelper2);
				sqlBuilder.IsComparing = true;
			}
			sqlBuilder.AddOrderByColumn("[Entity Types]", "EntityTypeName");
			sqlBuilder.UseDistinct = false;
			DataSet dataSet = new DataSet();
			FillDataSet(dataSet, "[Entity Types]", sqlBuilder);
			return dataSet;
		}

		public EntityTypeData GetEntityTypeByID(int entityTypeID)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			CommandHelper commandHelper = null;
			commandHelper = new CommandHelper();
			commandHelper.FieldName = "EntityTypeID";
			commandHelper.SqlFieldType = SqlDbType.Int;
			commandHelper.FieldValue = entityTypeID;
			commandHelper.TableName = "[Entity Types]";
			sqlBuilder.AddCommandHelper(commandHelper);
			EntityTypeData entityTypeData = new EntityTypeData();
			sqlBuilder.UseDistinct = false;
			try
			{
				FillDataSet(entityTypeData, "[Entity Types]", sqlBuilder);
				return entityTypeData;
			}
			catch
			{
				throw;
			}
		}

		public bool DeleteEntityType(int entityTypeID)
		{
			bool flag = true;
			try
			{
				string entityTypeNameByID = GetEntityTypeNameByID(entityTypeID);
				flag = DeleteTableRowByID("[Entity Types]", "EntityTypeID", entityTypeID.ToString());
				if (!flag)
				{
					return flag;
				}
				AddActivityLog("Entity Type", entityTypeNameByID, ActivityTypes.Delete, null);
				return flag;
			}
			catch
			{
				throw;
			}
		}

		public string GetEntityTypeNameByID(int id)
		{
			object obj = ExecuteSelectScalar("[Entity Types]", "EntityTypeID", id, "EntityTypeName");
			if (obj != null && obj != DBNull.Value)
			{
				return obj.ToString();
			}
			return "";
		}

		public string GetEntityTypeIDByName(string name)
		{
			try
			{
				object obj = ExecuteSelectScalar("[Entity Types]", "EntityTypeName", name, "EntityTypeID");
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

		public bool ExistEntityType(string name)
		{
			try
			{
				return IsTableFieldValueExist("[Entity Types]", "EntityTypeName", name);
			}
			catch
			{
				throw;
			}
		}
	}
}
