using Micromind.Common;
using Micromind.Common.Data;
using Micromind.Common.Libraries;
using System;
using System.Data;
using System.Data.SqlClient;

namespace Micromind.Data
{
	public sealed class Documents : StoreObject
	{
		private static string DOCUMENTID_PARM = "@DocumentID";

		private static string CREATEDBY_PARM = "@CreatedBy";

		private static string DATECREATED_PARM = "@DateCreated";

		private static string LASTVERIFIED_PARM = "@LastVerified";

		private static string SUBJECT_PARM = "@Subject";

		private static string TITLE_PARM = "@Title";

		private static string DESCRIPTION_PARM = "@Description";

		private static string PUBLISHER_PARM = "@Publisher";

		private static string CONTRIBUTOR_PARM = "@Contributor";

		private static string PUBLISHEDDATE_PARM = "@PublishedDate";

		private static string REVISEDDATE_PARM = "@RevisedDate";

		private static string TYPE_PARM = "@Type";

		private static string DOCLANGUAGE_PARM = "@DocLanguage";

		private static string SOURCEURL_PARM = "@SourceURL";

		private static string FILEPATH_PARM = "@FilePath";

		private static string SOURCERELIABILITY_PARM = "@SourceReliability";

		private static string SUMMARY_PARM = "@Summary";

		private static string KEYWORDS_PARM = "@KeyWords";

		private static string NOTE_PARM = "@Note";

		private const string ISINACTIVE_PARM = "@IsInactive";

		private static string DATETIMESTAMP_PARM = "@DateTimeStamp";

		private static string CATEGORYID_PARM = "@CategoryID";

		private static string NAME_PARM = "@Name";

		public Documents(Config config)
			: base(config)
		{
		}

		private string GetInsertText()
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddInsertUpdateParameters(DocumentData.DOCUMENTS_TABLE, new FieldValue(DocumentData.CREATEDBY_FIELD, CREATEDBY_PARM), new FieldValue(DocumentData.DATECREATED_FIELD, DATECREATED_PARM), new FieldValue(DocumentData.LASTVERIFIED_FIELD, LASTVERIFIED_PARM), new FieldValue(DocumentData.SUBJECT_FIELD, SUBJECT_PARM), new FieldValue(DocumentData.TITLE_FIELD, TITLE_PARM), new FieldValue(DocumentData.DESCRIPTION_FIELD, DESCRIPTION_PARM), new FieldValue(DocumentData.PUBLISHER_FIELD, PUBLISHER_PARM), new FieldValue(DocumentData.CONTRIBUTOR_FIELD, CONTRIBUTOR_PARM), new FieldValue(DocumentData.PUBLISHEDDATE_FIELD, PUBLISHEDDATE_PARM), new FieldValue(DocumentData.REVISEDDATE_FIELD, REVISEDDATE_PARM), new FieldValue(DocumentData.TYPE_FIELD, TYPE_PARM), new FieldValue(DocumentData.DOCLANGUAGE_FIELD, DOCLANGUAGE_PARM), new FieldValue(DocumentData.SOURCEURL_FIELD, SOURCEURL_PARM), new FieldValue(DocumentData.FILEPATH_FIELD, FILEPATH_PARM), new FieldValue(DocumentData.SOURCERELIABILITY_FIELD, SOURCERELIABILITY_PARM), new FieldValue(DocumentData.SUMMARY_FIELD, SUMMARY_PARM), new FieldValue(DocumentData.KEYWORDS_FIELD, KEYWORDS_PARM), new FieldValue(DocumentData.NOTE_FIELD, NOTE_PARM), new FieldValue("IsInactive", "@IsInactive"), new FieldValue(DocumentData.DATETIMESTAMP_FIELD, DATETIMESTAMP_PARM));
			return sqlBuilder.GetInsertExpression();
		}

		private string GetDocumentCategoryInsertText()
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddInsertUpdateParameters(DocumentCategoryData.DOCUMENTCATEGORIES_TABLE, new FieldValue(DocumentCategoryData.NAME_FIELD, NAME_PARM), new FieldValue(DocumentCategoryData.DESCRIPTION_FIELD, DESCRIPTION_PARM), new FieldValue(DocumentCategoryData.NOTE_FIELD, NOTE_PARM), new FieldValue(DocumentCategoryData.DATETIMESTAMP_FIELD, DATETIMESTAMP_PARM));
			return sqlBuilder.GetInsertExpression();
		}

		private string GetUpdateText()
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddInsertUpdateParameters(DocumentData.DOCUMENTS_TABLE, new FieldValue(DocumentData.DOCUMENTID_FIELD, DOCUMENTID_PARM, isUpdateConditionField: true), new FieldValue(DocumentData.CREATEDBY_FIELD, CREATEDBY_PARM), new FieldValue(DocumentData.DATECREATED_FIELD, DATECREATED_PARM), new FieldValue(DocumentData.LASTVERIFIED_FIELD, LASTVERIFIED_PARM), new FieldValue(DocumentData.SUBJECT_FIELD, SUBJECT_PARM), new FieldValue(DocumentData.TITLE_FIELD, TITLE_PARM), new FieldValue(DocumentData.DESCRIPTION_FIELD, DESCRIPTION_PARM), new FieldValue(DocumentData.PUBLISHER_FIELD, PUBLISHER_PARM), new FieldValue(DocumentData.CONTRIBUTOR_FIELD, CONTRIBUTOR_PARM), new FieldValue(DocumentData.PUBLISHEDDATE_FIELD, PUBLISHEDDATE_PARM), new FieldValue(DocumentData.REVISEDDATE_FIELD, REVISEDDATE_PARM), new FieldValue(DocumentData.TYPE_FIELD, TYPE_PARM), new FieldValue(DocumentData.DOCLANGUAGE_FIELD, DOCLANGUAGE_PARM), new FieldValue(DocumentData.SOURCEURL_FIELD, SOURCEURL_PARM), new FieldValue(DocumentData.FILEPATH_FIELD, FILEPATH_PARM), new FieldValue(DocumentData.SOURCERELIABILITY_FIELD, SOURCERELIABILITY_PARM), new FieldValue(DocumentData.SUMMARY_FIELD, SUMMARY_PARM), new FieldValue(DocumentData.KEYWORDS_FIELD, KEYWORDS_PARM), new FieldValue(DocumentData.NOTE_FIELD, NOTE_PARM), new FieldValue("IsInactive", "@IsInactive"), new FieldValue(DocumentData.DATETIMESTAMP_FIELD, DATETIMESTAMP_PARM, isUpdateConditionField: true, checkForNullValue: true));
			return sqlBuilder.GetUpdateExpression();
		}

		private string GetDocumentCategoryUpdateText()
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddInsertUpdateParameters(DocumentCategoryData.DOCUMENTCATEGORIES_TABLE, new FieldValue(DocumentCategoryData.CATEGORYID_FIELD, CATEGORYID_PARM, isUpdateConditionField: true), new FieldValue(DocumentCategoryData.NAME_FIELD, NAME_PARM), new FieldValue(DocumentCategoryData.DESCRIPTION_FIELD, DESCRIPTION_PARM), new FieldValue(DocumentCategoryData.NOTE_FIELD, NOTE_PARM), new FieldValue("IsInactive", "@IsInactive"), new FieldValue(DocumentCategoryData.DATETIMESTAMP_FIELD, DATETIMESTAMP_PARM, isUpdateConditionField: true, checkForNullValue: true));
			return sqlBuilder.GetUpdateExpression();
		}

		private SqlCommand GetInsertCommand()
		{
			if (insertCommand != null)
			{
				insertCommand.Dispose();
				insertCommand = null;
			}
			insertCommand = new SqlCommand(GetInsertText(), base.DBConfig.Connection);
			insertCommand.CommandType = CommandType.Text;
			SqlParameterCollection parameters = insertCommand.Parameters;
			parameters.Add(CREATEDBY_PARM, SqlDbType.SmallInt);
			parameters.Add(DATECREATED_PARM, SqlDbType.DateTime);
			parameters.Add(LASTVERIFIED_PARM, SqlDbType.DateTime);
			parameters.Add(SUBJECT_PARM, SqlDbType.NVarChar);
			parameters.Add(TITLE_PARM, SqlDbType.NVarChar);
			parameters.Add(DESCRIPTION_PARM, SqlDbType.NVarChar);
			parameters.Add(PUBLISHER_PARM, SqlDbType.Int);
			parameters.Add(CONTRIBUTOR_PARM, SqlDbType.Int);
			parameters.Add(PUBLISHEDDATE_PARM, SqlDbType.DateTime);
			parameters.Add(REVISEDDATE_PARM, SqlDbType.DateTime);
			parameters.Add(TYPE_PARM, SqlDbType.SmallInt);
			parameters.Add(DOCLANGUAGE_PARM, SqlDbType.SmallInt);
			parameters.Add(SOURCEURL_PARM, SqlDbType.NVarChar);
			parameters.Add(FILEPATH_PARM, SqlDbType.NVarChar);
			parameters.Add(SOURCERELIABILITY_PARM, SqlDbType.TinyInt);
			parameters.Add(SUMMARY_PARM, SqlDbType.NText);
			parameters.Add(KEYWORDS_PARM, SqlDbType.NText);
			parameters.Add(NOTE_PARM, SqlDbType.NText);
			parameters.Add("@IsInactive", SqlDbType.Bit);
			parameters.Add(DATETIMESTAMP_PARM, SqlDbType.DateTime);
			parameters[CREATEDBY_PARM].SourceColumn = DocumentData.CREATEDBY_FIELD;
			parameters[DATECREATED_PARM].SourceColumn = DocumentData.DATECREATED_FIELD;
			parameters[LASTVERIFIED_PARM].SourceColumn = DocumentData.LASTVERIFIED_FIELD;
			parameters[SUBJECT_PARM].SourceColumn = DocumentData.SUBJECT_FIELD;
			parameters[TITLE_PARM].SourceColumn = DocumentData.TITLE_FIELD;
			parameters[DESCRIPTION_PARM].SourceColumn = DocumentData.DESCRIPTION_FIELD;
			parameters[PUBLISHER_PARM].SourceColumn = DocumentData.PUBLISHER_FIELD;
			parameters[CONTRIBUTOR_PARM].SourceColumn = DocumentData.CONTRIBUTOR_FIELD;
			parameters[PUBLISHEDDATE_PARM].SourceColumn = DocumentData.PUBLISHEDDATE_FIELD;
			parameters[REVISEDDATE_PARM].SourceColumn = DocumentData.REVISEDDATE_FIELD;
			parameters[TYPE_PARM].SourceColumn = DocumentData.TYPE_FIELD;
			parameters[DOCLANGUAGE_PARM].SourceColumn = DocumentData.DOCLANGUAGE_FIELD;
			parameters[SOURCEURL_PARM].SourceColumn = DocumentData.SOURCEURL_FIELD;
			parameters[FILEPATH_PARM].SourceColumn = DocumentData.FILEPATH_FIELD;
			parameters[SOURCERELIABILITY_PARM].SourceColumn = DocumentData.SOURCERELIABILITY_FIELD;
			parameters[SUMMARY_PARM].SourceColumn = DocumentData.SUMMARY_FIELD;
			parameters[KEYWORDS_PARM].SourceColumn = DocumentData.KEYWORDS_FIELD;
			parameters[NOTE_PARM].SourceColumn = DocumentData.NOTE_FIELD;
			parameters["@IsInactive"].SourceColumn = "IsInactive";
			parameters[DATETIMESTAMP_PARM].SourceColumn = DocumentData.DATETIMESTAMP_FIELD;
			return insertCommand;
		}

		private SqlCommand GetDocumentCategoryInsertCommand()
		{
			if (insertCommand != null)
			{
				insertCommand.Dispose();
				insertCommand = null;
			}
			insertCommand = new SqlCommand(GetDocumentCategoryInsertText(), base.DBConfig.Connection);
			insertCommand.CommandType = CommandType.Text;
			SqlParameterCollection parameters = insertCommand.Parameters;
			parameters.Add(NAME_PARM, SqlDbType.NVarChar);
			parameters.Add(DESCRIPTION_PARM, SqlDbType.NVarChar);
			parameters.Add(NOTE_PARM, SqlDbType.NText);
			parameters.Add(DATETIMESTAMP_PARM, SqlDbType.DateTime);
			parameters[NAME_PARM].SourceColumn = DocumentCategoryData.NAME_FIELD;
			parameters[DESCRIPTION_PARM].SourceColumn = DocumentCategoryData.DESCRIPTION_FIELD;
			parameters[NOTE_PARM].SourceColumn = DocumentCategoryData.NOTE_FIELD;
			parameters[DATETIMESTAMP_PARM].SourceColumn = DocumentCategoryData.DATETIMESTAMP_FIELD;
			return insertCommand;
		}

		private SqlCommand GetUpdateCommand()
		{
			if (updateCommand == null)
			{
				updateCommand = new SqlCommand(GetUpdateText(), base.DBConfig.Connection);
				updateCommand.CommandType = CommandType.Text;
				SqlParameterCollection parameters = updateCommand.Parameters;
				parameters.Add(DOCUMENTID_PARM, SqlDbType.Int);
				parameters.Add(CREATEDBY_PARM, SqlDbType.SmallInt);
				parameters.Add(DATECREATED_PARM, SqlDbType.DateTime);
				parameters.Add(LASTVERIFIED_PARM, SqlDbType.DateTime);
				parameters.Add(SUBJECT_PARM, SqlDbType.NVarChar);
				parameters.Add(TITLE_PARM, SqlDbType.NVarChar);
				parameters.Add(DESCRIPTION_PARM, SqlDbType.NVarChar);
				parameters.Add(PUBLISHER_PARM, SqlDbType.Int);
				parameters.Add(CONTRIBUTOR_PARM, SqlDbType.Int);
				parameters.Add(PUBLISHEDDATE_PARM, SqlDbType.DateTime);
				parameters.Add(REVISEDDATE_PARM, SqlDbType.DateTime);
				parameters.Add(TYPE_PARM, SqlDbType.SmallInt);
				parameters.Add(DOCLANGUAGE_PARM, SqlDbType.SmallInt);
				parameters.Add(SOURCEURL_PARM, SqlDbType.NVarChar);
				parameters.Add(FILEPATH_PARM, SqlDbType.NVarChar);
				parameters.Add(SOURCERELIABILITY_PARM, SqlDbType.TinyInt);
				parameters.Add(SUMMARY_PARM, SqlDbType.NText);
				parameters.Add(KEYWORDS_PARM, SqlDbType.NText);
				parameters.Add(NOTE_PARM, SqlDbType.NText);
				parameters.Add("@IsInactive", SqlDbType.Bit);
				parameters.Add(DATETIMESTAMP_PARM, SqlDbType.DateTime);
				parameters[DOCUMENTID_PARM].SourceColumn = DocumentData.DOCUMENTID_FIELD;
				parameters[CREATEDBY_PARM].SourceColumn = DocumentData.CREATEDBY_FIELD;
				parameters[DATECREATED_PARM].SourceColumn = DocumentData.DATECREATED_FIELD;
				parameters[LASTVERIFIED_PARM].SourceColumn = DocumentData.LASTVERIFIED_FIELD;
				parameters[SUBJECT_PARM].SourceColumn = DocumentData.SUBJECT_FIELD;
				parameters[TITLE_PARM].SourceColumn = DocumentData.TITLE_FIELD;
				parameters[DESCRIPTION_PARM].SourceColumn = DocumentData.DESCRIPTION_FIELD;
				parameters[PUBLISHER_PARM].SourceColumn = DocumentData.PUBLISHER_FIELD;
				parameters[CONTRIBUTOR_PARM].SourceColumn = DocumentData.CONTRIBUTOR_FIELD;
				parameters[PUBLISHEDDATE_PARM].SourceColumn = DocumentData.PUBLISHEDDATE_FIELD;
				parameters[REVISEDDATE_PARM].SourceColumn = DocumentData.REVISEDDATE_FIELD;
				parameters[TYPE_PARM].SourceColumn = DocumentData.TYPE_FIELD;
				parameters[DOCLANGUAGE_PARM].SourceColumn = DocumentData.DOCLANGUAGE_FIELD;
				parameters[SOURCEURL_PARM].SourceColumn = DocumentData.SOURCEURL_FIELD;
				parameters[FILEPATH_PARM].SourceColumn = DocumentData.FILEPATH_FIELD;
				parameters[SOURCERELIABILITY_PARM].SourceColumn = DocumentData.SOURCERELIABILITY_FIELD;
				parameters[SUMMARY_PARM].SourceColumn = DocumentData.SUMMARY_FIELD;
				parameters[KEYWORDS_PARM].SourceColumn = DocumentData.KEYWORDS_FIELD;
				parameters[NOTE_PARM].SourceColumn = DocumentData.NOTE_FIELD;
				parameters["@IsInactive"].SourceColumn = "IsInactive";
				parameters[DATETIMESTAMP_PARM].SourceColumn = DocumentData.DATETIMESTAMP_FIELD;
			}
			return updateCommand;
		}

		private SqlCommand GetDocumentCategoryUpdateCommand()
		{
			if (updateCommand != null)
			{
				updateCommand.Dispose();
				updateCommand = null;
			}
			updateCommand = new SqlCommand(GetDocumentCategoryUpdateText(), base.DBConfig.Connection);
			updateCommand.CommandType = CommandType.Text;
			SqlParameterCollection parameters = updateCommand.Parameters;
			parameters.Add(CATEGORYID_PARM, SqlDbType.SmallInt);
			parameters.Add(NAME_PARM, SqlDbType.NVarChar);
			parameters.Add(DESCRIPTION_PARM, SqlDbType.NVarChar);
			parameters.Add(NOTE_PARM, SqlDbType.NText);
			parameters.Add("@IsInactive", SqlDbType.Bit);
			parameters.Add(DATETIMESTAMP_PARM, SqlDbType.DateTime);
			parameters[CATEGORYID_PARM].SourceColumn = DocumentCategoryData.CATEGORYID_FIELD;
			parameters[NAME_PARM].SourceColumn = DocumentCategoryData.NAME_FIELD;
			parameters[DESCRIPTION_PARM].SourceColumn = DocumentCategoryData.DESCRIPTION_FIELD;
			parameters[NOTE_PARM].SourceColumn = DocumentCategoryData.NOTE_FIELD;
			parameters["@IsInactive"].SourceColumn = "IsInactive";
			parameters[DATETIMESTAMP_PARM].SourceColumn = DocumentCategoryData.DATETIMESTAMP_FIELD;
			return updateCommand;
		}

		public bool InsertDocument(DocumentData documentData)
		{
			bool result = true;
			SqlCommand insertCommand = GetInsertCommand();
			SqlTransaction sqlTransaction = null;
			try
			{
				sqlTransaction = (insertCommand.Transaction = base.DBConfig.StartNewTransaction());
				Insert(documentData, DocumentData.DOCUMENTS_TABLE, insertCommand);
				if (documentData.DocumentTable.Rows.Count <= 0)
				{
					return result;
				}
				object insertedRowIdentity = GetInsertedRowIdentity(DocumentData.DOCUMENTS_TABLE, insertCommand);
				documentData.DocumentTable.Rows[0][DocumentData.DOCUMENTID_FIELD] = insertedRowIdentity;
				UpdateTableRowByID(DocumentData.DOCUMENTS_TABLE, DocumentData.DOCUMENTID_FIELD, DocumentData.DATETIMESTAMP_FIELD, insertedRowIdentity, DateTime.Now, sqlTransaction);
				UpdateTableRowInsertUpdateInfo(DocumentData.DOCUMENTS_TABLE, DocumentData.DOCUMENTID_FIELD, insertedRowIdentity, sqlTransaction, isInsert: true);
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

		public bool InsertDocumentCategory(DocumentCategoryData documentCategoryData)
		{
			bool result = true;
			SqlCommand documentCategoryInsertCommand = GetDocumentCategoryInsertCommand();
			SqlTransaction sqlTransaction = null;
			try
			{
				sqlTransaction = (documentCategoryInsertCommand.Transaction = base.DBConfig.StartNewTransaction());
				Insert(documentCategoryData, DocumentCategoryData.DOCUMENTCATEGORIES_TABLE, documentCategoryInsertCommand);
				if (documentCategoryData.DocumentCategoryTable.Rows.Count <= 0)
				{
					return result;
				}
				object insertedRowIdentity = GetInsertedRowIdentity(DocumentCategoryData.DOCUMENTCATEGORIES_TABLE, documentCategoryInsertCommand);
				documentCategoryData.DocumentCategoryTable.Rows[0][DocumentCategoryData.CATEGORYID_FIELD] = insertedRowIdentity;
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

		public bool UpdateDocument(DocumentData documentData)
		{
			bool flag = true;
			SqlCommand updateCommand = GetUpdateCommand();
			SqlTransaction sqlTransaction = null;
			try
			{
				sqlTransaction = (updateCommand.Transaction = base.DBConfig.StartNewTransaction());
				flag = Update(documentData, DocumentData.DOCUMENTS_TABLE, updateCommand);
				if (!flag)
				{
					return flag;
				}
				object obj = documentData.DocumentTable.Rows[0][DocumentData.DOCUMENTID_FIELD];
				UpdateTableRowByID(DocumentData.DOCUMENTS_TABLE, DocumentData.DOCUMENTID_FIELD, DocumentData.DATETIMESTAMP_FIELD, obj, DateTime.Now, sqlTransaction);
				UpdateTableRowInsertUpdateInfo(DocumentData.DOCUMENTS_TABLE, DocumentData.DOCUMENTID_FIELD, obj, sqlTransaction, isInsert: false);
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

		public bool UpdateDocumentCategory(DocumentCategoryData documentCategoryData)
		{
			bool flag = true;
			SqlCommand documentCategoryUpdateCommand = GetDocumentCategoryUpdateCommand();
			SqlTransaction sqlTransaction = null;
			try
			{
				sqlTransaction = (documentCategoryUpdateCommand.Transaction = base.DBConfig.StartNewTransaction());
				flag = Update(documentCategoryData, DocumentCategoryData.DOCUMENTCATEGORIES_TABLE, documentCategoryUpdateCommand);
				if (!flag)
				{
					return flag;
				}
				object id = documentCategoryData.DocumentCategoryTable.Rows[0][DocumentCategoryData.CATEGORYID_FIELD];
				UpdateTableRowByID(DocumentCategoryData.DOCUMENTCATEGORIES_TABLE, DocumentCategoryData.CATEGORYID_FIELD, DocumentCategoryData.DATETIMESTAMP_FIELD, id, DateTime.Now, sqlTransaction);
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

		public DocumentData GetDocuments()
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			CommandHelper commandHelper = null;
			commandHelper = new CommandHelper();
			commandHelper.TableName = DocumentData.DOCUMENTS_TABLE;
			sqlBuilder.AddCommandHelper(commandHelper);
			sqlBuilder.IsComparing = false;
			sqlBuilder.AddOrderByColumn(DocumentData.DOCUMENTS_TABLE, DocumentData.SUBJECT_FIELD);
			DocumentData documentData = new DocumentData();
			FillDataSet(documentData, DocumentData.DOCUMENTS_TABLE, sqlBuilder);
			return documentData;
		}

		public DocumentCategoryData GetDocumentCategories()
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			CommandHelper commandHelper = null;
			commandHelper = new CommandHelper();
			commandHelper.TableName = DocumentCategoryData.DOCUMENTCATEGORIES_TABLE;
			sqlBuilder.AddCommandHelper(commandHelper);
			sqlBuilder.IsComparing = false;
			sqlBuilder.AddOrderByColumn(DocumentCategoryData.DOCUMENTCATEGORIES_TABLE, DocumentCategoryData.NAME_FIELD);
			DocumentCategoryData documentCategoryData = new DocumentCategoryData();
			FillDataSet(documentCategoryData, DocumentCategoryData.DOCUMENTCATEGORIES_TABLE, sqlBuilder);
			return documentCategoryData;
		}

		public DataSet GetDocumentsByFields(params string[] columns)
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
			FillDataSet(dataSet, DocumentData.DOCUMENTS_TABLE, sqlBuilder);
			return dataSet;
		}

		public DataSet GetDocumentCategoriesByFields(params string[] columns)
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
			FillDataSet(dataSet, DocumentCategoryData.DOCUMENTCATEGORIES_TABLE, sqlBuilder);
			return dataSet;
		}

		public DocumentData GetDocumentByID(int documentID)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			CommandHelper commandHelper = null;
			commandHelper = new CommandHelper();
			commandHelper.FieldName = DocumentData.DOCUMENTID_FIELD;
			commandHelper.SqlFieldType = SqlDbType.Int;
			commandHelper.FieldValue = documentID;
			commandHelper.TableName = DocumentData.DOCUMENTS_TABLE;
			sqlBuilder.AddCommandHelper(commandHelper);
			DocumentData documentData = new DocumentData();
			FillDataSet(documentData, DocumentData.DOCUMENTS_TABLE, sqlBuilder);
			return documentData;
		}

		public DocumentCategoryData GetDocumentCategoryByID(int documentCategoryID)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			CommandHelper commandHelper = null;
			commandHelper = new CommandHelper();
			commandHelper.FieldName = DocumentCategoryData.CATEGORYID_FIELD;
			commandHelper.SqlFieldType = SqlDbType.Int;
			commandHelper.FieldValue = documentCategoryID;
			commandHelper.TableName = DocumentCategoryData.DOCUMENTCATEGORIES_TABLE;
			sqlBuilder.AddCommandHelper(commandHelper);
			DocumentCategoryData documentCategoryData = new DocumentCategoryData();
			FillDataSet(documentCategoryData, DocumentCategoryData.DOCUMENTCATEGORIES_TABLE, sqlBuilder);
			return documentCategoryData;
		}

		public bool DeleteDocument(int documentID)
		{
			bool flag = true;
			try
			{
				string commandText = "DELETE FROM " + DocumentData.DOCUMENTS_TABLE + " WHERE " + DocumentData.DOCUMENTID_FIELD + " = " + documentID.ToString();
				flag = Delete(commandText, null);
				if (!flag)
				{
					return flag;
				}
				AddActivityLog("Document", documentID.ToString(), ActivityTypes.Delete, null);
				return flag;
			}
			catch
			{
				throw;
			}
		}

		public bool DeleteDocumentCategory(int documentCategoryID)
		{
			bool flag = true;
			try
			{
				string commandText = "DELETE FROM " + DocumentCategoryData.DOCUMENTCATEGORIES_TABLE + " WHERE " + DocumentCategoryData.CATEGORYID_FIELD + " = " + documentCategoryID.ToString();
				flag = Delete(commandText, null);
				if (!flag)
				{
					return flag;
				}
				AddActivityLog("Document Category", documentCategoryID.ToString(), ActivityTypes.Delete, null);
				return flag;
			}
			catch
			{
				throw;
			}
		}

		public bool ActivateDocument(object id, bool activate)
		{
			activate = !activate;
			return UpdateTableRowByID(DocumentData.DOCUMENTS_TABLE, DocumentData.DOCUMENTID_FIELD, "IsInactive", id, Convert.ToByte(activate));
		}

		public bool ActivateDocumentCategory(object id, bool activate)
		{
			activate = !activate;
			return UpdateTableRowByID(DocumentCategoryData.DOCUMENTCATEGORIES_TABLE, DocumentCategoryData.CATEGORYID_FIELD, "IsInactive", id, Convert.ToByte(activate));
		}

		public string GetFilePath(int id)
		{
			string text = null;
			try
			{
				return ExecuteSelectScalar(DocumentData.DOCUMENTS_TABLE, DocumentData.DOCUMENTID_FIELD, id, DocumentData.FILEPATH_FIELD).ToString();
			}
			catch
			{
				throw;
			}
		}

		public string GetSourceURL(int id)
		{
			string text = null;
			try
			{
				return ExecuteSelectScalar(DocumentData.DOCUMENTS_TABLE, DocumentData.DOCUMENTID_FIELD, id, DocumentData.SOURCEURL_FIELD).ToString();
			}
			catch
			{
				throw;
			}
		}

		public bool ExistDocumentCategory(string name)
		{
			try
			{
				return IsTableFieldValueExist(DocumentCategoryData.DOCUMENTCATEGORIES_TABLE, DocumentCategoryData.NAME_FIELD, name);
			}
			catch
			{
				throw;
			}
		}
	}
}
