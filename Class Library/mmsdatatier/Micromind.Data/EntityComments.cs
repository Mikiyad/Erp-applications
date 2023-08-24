using Micromind.Common;
using Micromind.Common.Data;
using Micromind.Common.Libraries;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace Micromind.Data
{
	public sealed class EntityComments : StoreObject
	{
		public const string ENTITYCOMMENT_TABLE = "Entity_Comments";

		public const string COMMENTID_PARM = "@CommentID";

		public const string ENTITYID_PARM = "@EntityID";

		public const string ENTITYTYPE_PARM = "@EntityType";

		private const string ENTITYSYSDOCID_PARM = "@EntitySysDocID";

		private const string NOTE_PARM = "@NOTE";

		private const string ROWINDEX_PARM = "@RowIndex";

		private const string CREATEDBY_PARM = "@CreatedBy";

		private const string DATECREATED_PARM = "@DateCreated";

		private const string UPDATEDBY_PARM = "@UpdatedBy";

		private const string DATEUPDATED_PARM = "@DateUpdated";

		private const byte COMPANY_ID = 1;

		public EntityComments(Config config)
			: base(config)
		{
		}

		public EntityCommentData GetEntityCommentByID(string id)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			CommandHelper commandHelper = null;
			commandHelper = new CommandHelper();
			commandHelper.FieldName = "EntityID";
			commandHelper.SqlFieldType = SqlDbType.NVarChar;
			commandHelper.FieldValue = id;
			commandHelper.TableName = "Entity_Comments";
			sqlBuilder.AddCommandHelper(commandHelper);
			sqlBuilder.IsComparing = true;
			EntityCommentData entityCommentData = new EntityCommentData();
			sqlBuilder.UseDistinct = false;
			FillDataSet(entityCommentData, "Entity_Comments", sqlBuilder);
			return entityCommentData;
		}

		public EntityCommentData GetEntityCommentByID(EntityTypesEnum entityType, string entityID, string commentID)
		{
			EntityCommentData entityCommentData = new EntityCommentData();
			try
			{
				string text = "SELECT *\r\n                                  FROM Entity_Comments WHERE EntityID='" + entityID + "' AND EntityType= " + (int)entityType + " AND EntityCommentName='" + commentID + "'";
				FillDataSet(entityCommentData, "Entity_Comments", text.Trim());
				return entityCommentData;
			}
			catch
			{
				return entityCommentData;
			}
		}

		public DataSet GetEntityCommentList(EntityTypesEnum entityType, string entityID)
		{
			DataSet dataSet = new DataSet();
			string text = ((byte)entityType).ToString();
			try
			{
				string textCommand = "SELECT *\r\n                                  FROM Entity_Comments WHERE EntityID='" + entityID + "' AND EntityType='" + text + "' ";
				FillDataSet(dataSet, "Entity_Comments", textCommand);
				return dataSet;
			}
			catch
			{
				return dataSet;
			}
		}

		public bool DeleteEntityComment(int commentID)
		{
			bool flag = false;
			try
			{
				SqlTransaction trans = base.DBConfig.StartNewTransaction();
				string commandText = "DELETE FROM Entity_Comments WHERE CommentID = " + commentID;
				flag = Delete(commandText, trans);
				if (flag)
				{
					AddActivityLog("Entity Comment", commentID.ToString(), ActivityTypes.Delete, null);
				}
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

		private string GetInsertUpdateText(bool isUpdate)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddInsertUpdateParameters("Entity_Comments", new FieldValue("EntityID", "@EntityID", isUpdateConditionField: false), new FieldValue("EntityType", "@EntityType", isUpdateConditionField: false), new FieldValue("EntitySysDocID", "@EntitySysDocID"), new FieldValue("Note", "@NOTE"), new FieldValue("RowIndex", "@RowIndex"));
			if (isUpdate)
			{
				sqlBuilder.AddInsertUpdateParameters("Entity_Comments", new FieldValue("CommentID", "@CommentID", isUpdateConditionField: true, checkForNullValue: true));
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
			parameters.Add("@EntityID", SqlDbType.NVarChar);
			parameters.Add("@EntityType", SqlDbType.TinyInt);
			parameters.Add("@EntitySysDocID", SqlDbType.NVarChar);
			parameters.Add("@NOTE", SqlDbType.NVarChar);
			parameters.Add("@RowIndex", SqlDbType.Int);
			parameters["@EntityID"].SourceColumn = "EntityID";
			parameters["@EntityType"].SourceColumn = "EntityType";
			parameters["@EntitySysDocID"].SourceColumn = "EntitySysDocID";
			parameters["@RowIndex"].SourceColumn = "RowIndex";
			parameters["@NOTE"].SourceColumn = "Note";
			if (isUpdate)
			{
				parameters.Add("@CommentID", SqlDbType.Int);
				parameters["@CommentID"].SourceColumn = "CommentID";
			}
			if (isUpdate)
			{
				return updateCommand;
			}
			return insertCommand;
		}

		private string GetEditUpdateText()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("UPDATE Entity_Comments");
			stringBuilder.Append(" SET ");
			stringBuilder.Append("Note = @NOTE, ");
			stringBuilder.Append("DateUpdated = @DateUpdated");
			stringBuilder.Append(" WHERE EntityType = @EntityType");
			stringBuilder.Append(" AND EntityID = @EntityID");
			return stringBuilder.ToString();
		}

		public bool SaveEntityComment(EntityCommentData entityCommentsData)
		{
			bool flag = false;
			try
			{
				entityCommentsData.EntityCommentsTable.Rows[0]["EntityID"].ToString();
				entityCommentsData.EntityCommentsTable.Rows[0]["EntitySysDocID"].ToString();
				int.Parse(entityCommentsData.EntityCommentsTable.Rows[0]["EntityType"].ToString());
				base.DBConfig.StartNewTransaction();
				return InsertEntityComment(entityCommentsData);
			}
			catch (Exception)
			{
				throw;
			}
		}

		public bool InsertEntityComment(EntityCommentData entityCommentsData)
		{
			bool result = true;
			SqlCommand insertUpdateCommand = GetInsertUpdateCommand(isUpdate: false);
			SqlTransaction sqlTransaction = null;
			try
			{
				sqlTransaction = (insertUpdateCommand.Transaction = base.DBConfig.StartNewTransaction());
				result = Insert(entityCommentsData, "Entity_Comments", insertUpdateCommand);
				int num = -1;
				object insertedRowIdentity = GetInsertedRowIdentity("Entity_Comments", insertUpdateCommand);
				if (!insertedRowIdentity.IsNullOrEmpty())
				{
					num = int.Parse(insertedRowIdentity.ToString());
				}
				AddActivityLog("EntityComment", num.ToString(), ActivityTypes.Add, sqlTransaction);
				UpdateTableRowInsertUpdateInfo("Entity_Comments", "CommentID", num.ToString(), sqlTransaction, isInsert: true);
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

		public bool UpdateEntityComment(EntityCommentData entityCommentsData)
		{
			bool flag = true;
			SqlCommand insertUpdateCommand = GetInsertUpdateCommand(isUpdate: true);
			SqlTransaction sqlTransaction = null;
			try
			{
				sqlTransaction = (insertUpdateCommand.Transaction = base.DBConfig.StartNewTransaction());
				flag = Update(entityCommentsData, "Entity_Comments", insertUpdateCommand);
				if (!flag)
				{
					return flag;
				}
				object obj = entityCommentsData.EntityCommentsTable.Rows[0]["CommentID"];
				UpdateTableRowByID("Entity_Comments", "EntityID", "DateUpdated", obj, DateTime.Now, sqlTransaction);
				AddActivityLog("Entity Comment", "", ActivityTypes.Update, sqlTransaction);
				UpdateTableRowInsertUpdateInfo("Entity_Comments", "CommentID", obj, sqlTransaction, isInsert: false);
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

		public EntityCommentData GetEntityComment()
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddTable("Entity_Comments");
			EntityCommentData entityCommentData = new EntityCommentData();
			sqlBuilder.UseDistinct = false;
			FillDataSet(entityCommentData, "Entity_Comments", sqlBuilder);
			return entityCommentData;
		}

		public DataSet GetEntityCommentByFields(params string[] columns)
		{
			return GetEntityCommentByFields(null, isInactive: true, columns);
		}

		public DataSet GetEntityCommentByFields(string[] entityCommentsID, params string[] columns)
		{
			return GetEntityCommentByFields(entityCommentsID, isInactive: true, columns);
		}

		public DataSet GetEntityCommentByFields(string[] ids, bool isInactive, params string[] columns)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddTable("Entity_Comments");
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
				commandHelper.FieldName = "EntityID";
				commandHelper.FieldValue = ids;
				commandHelper.SqlFieldType = SqlDbType.NVarChar;
				commandHelper.TableName = "Entity_Comments";
				sqlBuilder.AddCommandHelper(commandHelper);
			}
			DataSet dataSet = new DataSet();
			dataSet.EnforceConstraints = false;
			sqlBuilder.UseDistinct = false;
			FillDataSet(dataSet, "Entity_Comments", sqlBuilder);
			return dataSet;
		}
	}
}
