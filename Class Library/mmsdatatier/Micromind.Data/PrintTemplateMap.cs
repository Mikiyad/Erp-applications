using Micromind.Common;
using Micromind.Common.Data;
using Micromind.Common.Libraries;
using System;
using System.Data;
using System.Data.SqlClient;

namespace Micromind.Data
{
	public sealed class PrintTemplateMap : StoreObject
	{
		public const string PRINTTEMPLATEMAP_TABLE = "Print_Template_Map";

		public const string MAPID_PARM = "@MapID";

		public const string TEMPLATENAME_PARM = "@TemplateName";

		public const string SCREENTYPEID_PARM = "@ScreenType";

		public const string SCREENID_PARM = "@ScreenID";

		public const string FILENAME_PARM = "@FileName";

		public const string SCREEAREA_PARM = "@ScreenArea";

		private const string CREATEDBY_PARM = "@CreatedBy";

		private const string DATECREATED_PARM = "@DateCreated";

		private const string UPDATEDBY_PARM = "@UpdatedBy";

		private const string DATEUPDATED_PARM = "@DateUpdated";

		public PrintTemplateMap(Config config)
			: base(config)
		{
		}

		private string GetInsertUpdateText(bool isUpdate)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddInsertUpdateParameters("Print_Template_Map", new FieldValue("MapID", "@MapID", isUpdateConditionField: true), new FieldValue("TemplateName", "@TemplateName"), new FieldValue("ScreenType", "@ScreenType"), new FieldValue("ScreenID", "@ScreenID"), new FieldValue("ScreenArea", "@ScreenArea"), new FieldValue("FileName", "@FileName"));
			if (isUpdate)
			{
				sqlBuilder.AddInsertUpdateParameters("Print_Template_Map", new FieldValue("DateUpdated", "@DateUpdated", isUpdateConditionField: true, checkForNullValue: true));
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
			parameters.Add("@MapID", SqlDbType.NVarChar);
			parameters.Add("@TemplateName", SqlDbType.NVarChar);
			parameters.Add("@ScreenType", SqlDbType.TinyInt);
			parameters.Add("@ScreenID", SqlDbType.NVarChar);
			parameters.Add("@ScreenArea", SqlDbType.NVarChar);
			parameters.Add("@FileName", SqlDbType.NVarChar);
			parameters["@MapID"].SourceColumn = "MapID";
			parameters["@TemplateName"].SourceColumn = "TemplateName";
			parameters["@ScreenType"].SourceColumn = "ScreenType";
			parameters["@ScreenID"].SourceColumn = "ScreenID";
			parameters["@ScreenArea"].SourceColumn = "ScreenArea";
			parameters["@FileName"].SourceColumn = "FileName";
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

		public bool InsertPrintTemplateMap(PrintTemplateMapData PrintTemplateMapData)
		{
			bool result = true;
			SqlCommand insertUpdateCommand = GetInsertUpdateCommand(isUpdate: false);
			SqlTransaction sqlTransaction = null;
			try
			{
				sqlTransaction = (insertUpdateCommand.Transaction = base.DBConfig.StartNewTransaction());
				result = Insert(PrintTemplateMapData, "Print_Template_Map", insertUpdateCommand);
				string text = PrintTemplateMapData.PrintTemplateMapTable.Rows[0]["MapID"].ToString();
				AddActivityLog("PrintTemplateMap", text, ActivityTypes.Add, sqlTransaction);
				UpdateTableRowInsertUpdateInfo("Print_Template_Map", "MapID", text, sqlTransaction, isInsert: true);
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

		public bool UpdatePrintTemplateMap(PrintTemplateMapData accountPrintTemplateMapData)
		{
			bool flag = true;
			SqlCommand insertUpdateCommand = GetInsertUpdateCommand(isUpdate: true);
			SqlTransaction sqlTransaction = null;
			try
			{
				sqlTransaction = (insertUpdateCommand.Transaction = base.DBConfig.StartNewTransaction());
				flag = Update(accountPrintTemplateMapData, "Print_Template_Map", insertUpdateCommand);
				if (!flag)
				{
					return flag;
				}
				object obj = accountPrintTemplateMapData.PrintTemplateMapTable.Rows[0]["MapID"];
				UpdateTableRowByID("Print_Template_Map", "MapID", "DateUpdated", obj, DateTime.Now, sqlTransaction);
				string entiyID = accountPrintTemplateMapData.PrintTemplateMapTable.Rows[0]["TemplateName"].ToString();
				AddActivityLog("PrintTemplateMap", entiyID, ActivityTypes.Update, sqlTransaction);
				UpdateTableRowInsertUpdateInfo("Print_Template_Map", "MapID", obj, sqlTransaction, isInsert: false);
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

		public PrintTemplateMapData GetPrintTemplateMap()
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddTable("Print_Template_Map");
			PrintTemplateMapData printTemplateMapData = new PrintTemplateMapData();
			sqlBuilder.UseDistinct = false;
			FillDataSet(printTemplateMapData, "Print_Template_Map", sqlBuilder);
			return printTemplateMapData;
		}

		public bool DeletePrintTemplateMap(string PrintTemplateMapID)
		{
			bool flag = true;
			try
			{
				string commandText = "DELETE FROM Print_Template_Map WHERE MapID = '" + PrintTemplateMapID + "'";
				flag = Delete(commandText, null);
				if (!flag)
				{
					return flag;
				}
				AddActivityLog("PrintTEmplateMap", PrintTemplateMapID, ActivityTypes.Delete, null);
				return flag;
			}
			catch
			{
				throw;
			}
		}

		public PrintTemplateMapData GetPrintTemplateMapByID(string id)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			CommandHelper commandHelper = null;
			commandHelper = new CommandHelper();
			commandHelper.FieldName = "MapID";
			commandHelper.SqlFieldType = SqlDbType.NVarChar;
			commandHelper.FieldValue = id;
			commandHelper.TableName = "Print_Template_Map";
			sqlBuilder.AddCommandHelper(commandHelper);
			sqlBuilder.IsComparing = true;
			PrintTemplateMapData printTemplateMapData = new PrintTemplateMapData();
			sqlBuilder.UseDistinct = false;
			FillDataSet(printTemplateMapData, "Print_Template_Map", sqlBuilder);
			return printTemplateMapData;
		}

		public DataSet GetPrintTemplateMapByFields(params string[] columns)
		{
			return GetPrintTemplateMapByFields(null, isInactive: true, columns);
		}

		public DataSet GetPrintTemplateMapByFields(string[] PrintTemplateMapID, params string[] columns)
		{
			return GetPrintTemplateMapByFields(PrintTemplateMapID, isInactive: true, columns);
		}

		public DataSet GetPrintTemplateMapByFields(string[] ids, bool isInactive, params string[] columns)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddTable("Print_Template_Map");
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
				commandHelper.FieldName = "MapID";
				commandHelper.FieldValue = ids;
				commandHelper.SqlFieldType = SqlDbType.NVarChar;
				commandHelper.TableName = "Print_Template_Map";
				sqlBuilder.AddCommandHelper(commandHelper);
			}
			DataSet dataSet = new DataSet();
			dataSet.EnforceConstraints = false;
			sqlBuilder.UseDistinct = false;
			FillDataSet(dataSet, "Print_Template_Map", sqlBuilder);
			return dataSet;
		}

		public DataSet GetPrintTemplateMapList()
		{
			DataSet dataSet = new DataSet();
			string textCommand = "SELECT MapID [Code], \r\n                               TemplateName [ Name], ScreenID [Screen ID], FileName [File Name]\r\n                           FROM Print_Template_Map";
			FillDataSet(dataSet, "Print_Template_Map", textCommand);
			return dataSet;
		}

		public DataSet GetPrintTemplateMapComboList()
		{
			DataSet dataSet = new DataSet();
			string textCommand = "SELECT MapID [Code], TemplateName [Name], ScreenID,ScreenType,FileName \r\n                           FROM Print_Template_Map ORDER BY MapID, TemplateName";
			FillDataSet(dataSet, "Print_Template_Map", textCommand);
			return dataSet;
		}
	}
}
