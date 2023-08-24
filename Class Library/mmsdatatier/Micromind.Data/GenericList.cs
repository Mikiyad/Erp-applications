using Micromind.Common;
using Micromind.Common.Data;
using Micromind.Common.Libraries;
using System;
using System.Data;
using System.Data.SqlClient;

namespace Micromind.Data
{
	public sealed class GenericList : StoreObject
	{
		private const string GENERICLISTTYPE_PARM = "@GenericListType";

		private const string GENERICLISTID_PARM = "@GenericListID";

		private const string GENERICLISTNAME_PARM = "@GenericListName";

		private const string GENERICLISTSHORTNAME_PARM = "@GenericListShortName";

		private const string INACTIVE_PARM = "@Inactive";

		public const string NOTE_PARM = "@Note";

		public const string GENERICLIST_TABLE = "GenericList";

		private const string CREATEDBY_PARM = "@CreatedBy";

		private const string DATECREATED_PARM = "@DateCreated";

		private const string UPDATEDBY_PARM = "@UpdatedBy";

		private const string DATEUPDATED_PARM = "@DateUpdated";

		public GenericList(Config config)
			: base(config)
		{
		}

		private string GetInsertUpdateText(bool isUpdate)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddInsertUpdateParameters("Generic_List", new FieldValue("GenericListID", "@GenericListID", isUpdateConditionField: true), new FieldValue("GenericListType", "@GenericListType", isUpdateConditionField: true), new FieldValue("GenericListName", "@GenericListName"), new FieldValue("GenericListShortName", "@GenericListShortName"), new FieldValue("Inactive", "@Inactive"), new FieldValue("Note", "@Note"));
			if (isUpdate)
			{
				sqlBuilder.AddInsertUpdateParameters("Generic_List", new FieldValue("DateUpdated", "@DateUpdated", isUpdateConditionField: true, checkForNullValue: true));
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
			parameters.Add("@GenericListType", SqlDbType.TinyInt);
			parameters.Add("@GenericListID", SqlDbType.NVarChar);
			parameters.Add("@GenericListName", SqlDbType.NVarChar);
			parameters.Add("@GenericListShortName", SqlDbType.NVarChar);
			parameters.Add("@Inactive", SqlDbType.Bit);
			parameters.Add("@Note", SqlDbType.NVarChar);
			parameters["@GenericListType"].SourceColumn = "GenericListType";
			parameters["@GenericListID"].SourceColumn = "GenericListID";
			parameters["@GenericListName"].SourceColumn = "GenericListName";
			parameters["@GenericListShortName"].SourceColumn = "GenericListShortName";
			parameters["@Inactive"].SourceColumn = "Inactive";
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

		public bool InsertGenericList(GenericListData accountGenericListData)
		{
			bool result = true;
			SqlCommand insertUpdateCommand = GetInsertUpdateCommand(isUpdate: false);
			SqlTransaction sqlTransaction = null;
			try
			{
				sqlTransaction = (insertUpdateCommand.Transaction = base.DBConfig.StartNewTransaction());
				result = Insert(accountGenericListData, "Generic_List", insertUpdateCommand);
				string text = accountGenericListData.GenericListTable.Rows[0]["GenericListID"].ToString();
				AddActivityLog("Generic List", text, ActivityTypes.Add, sqlTransaction);
				UpdateTableRowInsertUpdateInfo("Generic_List", "GenericListID", text, sqlTransaction, isInsert: true);
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

		public bool UpdateGenericList(GenericListData accountGenericListData)
		{
			bool flag = true;
			SqlCommand insertUpdateCommand = GetInsertUpdateCommand(isUpdate: true);
			SqlTransaction sqlTransaction = null;
			try
			{
				sqlTransaction = (insertUpdateCommand.Transaction = base.DBConfig.StartNewTransaction());
				flag = Update(accountGenericListData, "Generic_List", insertUpdateCommand);
				if (!flag)
				{
					return flag;
				}
				object obj = accountGenericListData.GenericListTable.Rows[0]["GenericListID"];
				UpdateTableRowByID("Generic_List", "GenericListID", "DateUpdated", obj, DateTime.Now, sqlTransaction);
				string entiyID = accountGenericListData.GenericListTable.Rows[0]["GenericListName"].ToString();
				AddActivityLog("Generic List", entiyID, ActivityTypes.Update, sqlTransaction);
				UpdateTableRowInsertUpdateInfo("Generic_List", "GenericListID", obj, sqlTransaction, isInsert: false);
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

		public GenericListData GetGenericList(GenericListTypes listType)
		{
			GenericListData genericListData = new GenericListData();
			string textCommand = "SELECT *\r\n                           FROM Generic_List WHERE GenericListType = " + (byte)listType;
			FillDataSet(genericListData, "Generic_List", textCommand);
			return genericListData;
		}

		public bool DeleteGenericList(string genericListID, GenericListTypes listType)
		{
			bool flag = true;
			try
			{
				string commandText = "DELETE FROM Generic_List WHERE GenericListID = '" + genericListID + "' AND  GenericListType = " + (byte)listType;
				flag = Delete(commandText, null);
				if (!flag)
				{
					return flag;
				}
				AddActivityLog("Generic List", genericListID, ActivityTypes.Delete, null);
				return flag;
			}
			catch
			{
				throw;
			}
		}

		public GenericListData GetGenericListByID(string id, GenericListTypes listType)
		{
			GenericListData genericListData = new GenericListData();
			string textCommand = "SELECT *\r\n                           FROM Generic_List WHERE GenericListType = " + (byte)listType + " AND GenericListID = '" + id + "' ORDER BY GenericListID,GenericListName";
			FillDataSet(genericListData, "Generic_List", textCommand);
			return genericListData;
		}

		public DataSet GetGenericListByFields(params string[] columns)
		{
			return GetGenericListByFields(null, isInactive: true, columns);
		}

		public DataSet GetGenericListByFields(string[] genericListID, params string[] columns)
		{
			return GetGenericListByFields(genericListID, isInactive: true, columns);
		}

		public DataSet GetGenericListByFields(string[] ids, bool isInactive, params string[] columns)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddTable("Generic_List");
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
				commandHelper.FieldName = "GenericListID";
				commandHelper.FieldValue = ids;
				commandHelper.SqlFieldType = SqlDbType.NVarChar;
				commandHelper.TableName = "Generic_List";
				sqlBuilder.AddCommandHelper(commandHelper);
			}
			DataSet dataSet = new DataSet();
			dataSet.EnforceConstraints = false;
			sqlBuilder.UseDistinct = false;
			FillDataSet(dataSet, "Generic_List", sqlBuilder);
			return dataSet;
		}

		public DataSet GetGenericListList(GenericListTypes listType)
		{
			DataSet dataSet = new DataSet();
			string text = "SELECT GenericListID [Code],GenericListName AS Name,GenericListShortName [ShortName], Note FROM Generic_List ";
			text = ((listType == GenericListTypes.All) ? (text + " ORDER BY Code,Name") : (text + " WHERE GenericListType = " + (byte)listType + " ORDER BY Code,Name"));
			FillDataSet(dataSet, "Generic_List", text);
			return dataSet;
		}

		public DataSet GetGenericListList(GenericListTypes listType, bool islistType)
		{
			DataSet dataSet = new DataSet();
			string text = "SELECT GenericListID [Doc Id],GenericListName AS Number, Note FROM Generic_List ";
			text = ((listType == GenericListTypes.All) ? (text + " ORDER BY [Doc Id],Number") : (text + " WHERE GenericListType = " + (byte)listType + " ORDER BY [Doc Id],Number"));
			FillDataSet(dataSet, "Generic_List", text);
			return dataSet;
		}

		public DataSet GetGenericListComboList(GenericListTypes listType)
		{
			DataSet dataSet = new DataSet();
			string text = "SELECT GenericListID [Code],GenericListName [Name],GenericListType\r\n                           FROM Generic_List ";
			if (listType == GenericListTypes.All)
			{
				text += " UNION SELECT 'PRIMARY' AS [Code],'PRIMARY' AS [Name],17 AS GenericListType\r\n                           FROM Generic_List ";
			}
			text = ((listType == GenericListTypes.All) ? (text + " ORDER BY GenericListID, GenericListName") : (text + " WHERE GenericListType = " + (byte)listType + " ORDER BY GenericListID,GenericListName"));
			FillDataSet(dataSet, "Generic_List", text);
			return dataSet;
		}
	}
}
