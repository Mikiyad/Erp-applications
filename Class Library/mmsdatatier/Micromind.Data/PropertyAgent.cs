using Micromind.Common;
using Micromind.Common.Data;
using Micromind.Common.Libraries;
using System;
using System.Data;
using System.Data.SqlClient;

namespace Micromind.Data
{
	public sealed class PropertyAgent : StoreObject
	{
		private const string PROPERTYAGENTID_PARM = "@PropertyAgentID";

		private const string PROPERTYAGENTNAME_PARM = "@PropertyAgentName";

		public const string NOTE_PARM = "@Note";

		public const string PROPERTYAGENT_TABLE = "Property_Agent";

		private const string CREATEDBY_PARM = "@CreatedBy";

		private const string DATECREATED_PARM = "@DateCreated";

		private const string UPDATEDBY_PARM = "@UpdatedBy";

		private const string DATEUPDATED_PARM = "@DateUpdated";

		public PropertyAgent(Config config)
			: base(config)
		{
		}

		private string GetInsertUpdateText(bool isUpdate)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddInsertUpdateParameters("Property_Agent", new FieldValue("PropertyAgentID", "@PropertyAgentID", isUpdateConditionField: true), new FieldValue("PropertyAgentName", "@PropertyAgentName"), new FieldValue("Note", "@Note"));
			if (isUpdate)
			{
				sqlBuilder.AddInsertUpdateParameters("Property_Agent", new FieldValue("DateUpdated", "@DateUpdated", isUpdateConditionField: true, checkForNullValue: true));
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
			parameters.Add("@PropertyAgentID", SqlDbType.NVarChar);
			parameters.Add("@PropertyAgentName", SqlDbType.NVarChar);
			parameters.Add("@Note", SqlDbType.NVarChar);
			parameters["@PropertyAgentID"].SourceColumn = "PropertyAgentID";
			parameters["@PropertyAgentName"].SourceColumn = "PropertyAgentName";
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

		public bool InsertPropertyAgent(PropertyAgentData accountPropertyAgentData)
		{
			bool result = true;
			SqlCommand insertUpdateCommand = GetInsertUpdateCommand(isUpdate: false);
			SqlTransaction sqlTransaction = null;
			try
			{
				sqlTransaction = (insertUpdateCommand.Transaction = base.DBConfig.StartNewTransaction());
				result = Insert(accountPropertyAgentData, "Property_Agent", insertUpdateCommand);
				string text = accountPropertyAgentData.PropertyAgentTable.Rows[0]["PropertyAgentID"].ToString();
				AddActivityLog("PropertyAgent", text, ActivityTypes.Add, sqlTransaction);
				UpdateTableRowInsertUpdateInfo("Property_Agent", "PropertyAgentID", text, sqlTransaction, isInsert: true);
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

		public bool UpdatePropertyAgent(PropertyAgentData accountPropertyAgentData)
		{
			bool flag = true;
			SqlCommand insertUpdateCommand = GetInsertUpdateCommand(isUpdate: true);
			SqlTransaction sqlTransaction = null;
			try
			{
				sqlTransaction = (insertUpdateCommand.Transaction = base.DBConfig.StartNewTransaction());
				flag = Update(accountPropertyAgentData, "Property_Agent", insertUpdateCommand);
				if (!flag)
				{
					return flag;
				}
				object obj = accountPropertyAgentData.PropertyAgentTable.Rows[0]["PropertyAgentID"];
				UpdateTableRowByID("Property_Agent", "PropertyAgentID", "DateUpdated", obj, DateTime.Now, sqlTransaction);
				string entiyID = accountPropertyAgentData.PropertyAgentTable.Rows[0]["PropertyAgentName"].ToString();
				AddActivityLog("PropertyAgent", entiyID, ActivityTypes.Update, sqlTransaction);
				UpdateTableRowInsertUpdateInfo("Property_Agent", "PropertyAgentID", obj, sqlTransaction, isInsert: false);
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

		public PropertyAgentData GetPropertyAgent()
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddTable("Property_Agent");
			PropertyAgentData propertyAgentData = new PropertyAgentData();
			sqlBuilder.UseDistinct = false;
			FillDataSet(propertyAgentData, "Property_Agent", sqlBuilder);
			return propertyAgentData;
		}

		public bool DeletePropertyAgent(string areaID)
		{
			bool flag = true;
			try
			{
				string commandText = "DELETE FROM Property_Agent WHERE PropertyAgentID = '" + areaID + "'";
				flag = Delete(commandText, null);
				if (!flag)
				{
					return flag;
				}
				AddActivityLog("PropertyAgent", areaID, ActivityTypes.Delete, null);
				return flag;
			}
			catch
			{
				throw;
			}
		}

		public PropertyAgentData GetPropertyAgentByID(string id)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			CommandHelper commandHelper = null;
			commandHelper = new CommandHelper();
			commandHelper.FieldName = "PropertyAgentID";
			commandHelper.SqlFieldType = SqlDbType.NVarChar;
			commandHelper.FieldValue = id;
			commandHelper.TableName = "Property_Agent";
			sqlBuilder.AddCommandHelper(commandHelper);
			sqlBuilder.IsComparing = true;
			PropertyAgentData propertyAgentData = new PropertyAgentData();
			sqlBuilder.UseDistinct = false;
			FillDataSet(propertyAgentData, "Property_Agent", sqlBuilder);
			return propertyAgentData;
		}

		public DataSet GetPropertyAgentByFields(params string[] columns)
		{
			return GetPropertyAgentByFields(null, isInactive: true, columns);
		}

		public DataSet GetPropertyAgentByFields(string[] areaID, params string[] columns)
		{
			return GetPropertyAgentByFields(areaID, isInactive: true, columns);
		}

		public DataSet GetPropertyAgentByFields(string[] ids, bool isInactive, params string[] columns)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddTable("Property_Agent");
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
				commandHelper.FieldName = "PropertyAgentID";
				commandHelper.FieldValue = ids;
				commandHelper.SqlFieldType = SqlDbType.NVarChar;
				commandHelper.TableName = "Property_Agent";
				sqlBuilder.AddCommandHelper(commandHelper);
			}
			DataSet dataSet = new DataSet();
			dataSet.EnforceConstraints = false;
			sqlBuilder.UseDistinct = false;
			FillDataSet(dataSet, "Property_Agent", sqlBuilder);
			return dataSet;
		}

		public DataSet GetPropertyAgentList()
		{
			DataSet dataSet = new DataSet();
			string textCommand = "SELECT PropertyAgentID [Agent Code],PropertyAgentName [Agent Name]\r\n                           FROM Property_Agent ";
			FillDataSet(dataSet, "Property_Agent", textCommand);
			return dataSet;
		}

		public DataSet GetPropertyAgentComboList()
		{
			DataSet dataSet = new DataSet();
			string textCommand = "SELECT PropertyAgentID [Code],PropertyAgentName [Name]\r\n                           FROM Property_Agent ORDER BY PropertyAgentID,PropertyAgentName";
			FillDataSet(dataSet, "Property_Agent", textCommand);
			return dataSet;
		}
	}
}
