using Micromind.Common;
using Micromind.Common.Data;
using Micromind.Common.Libraries;
using System;
using System.Data;
using System.Data.SqlClient;

namespace Micromind.Data
{
	public sealed class PropertyClass : StoreObject
	{
		private const string PROPERTYCLASSID_PARM = "@PropertyClassID";

		private const string PROPERTYCLASSNAME_PARM = "@PropertyClassName";

		public const string NOTE_PARM = "@Note";

		public const string PROPERTYCLASS_TABLE = "Property_Class";

		private const string CREATEDBY_PARM = "@CreatedBy";

		private const string DATECREATED_PARM = "@DateCreated";

		private const string UPDATEDBY_PARM = "@UpdatedBy";

		private const string DATEUPDATED_PARM = "@DateUpdated";

		public PropertyClass(Config config)
			: base(config)
		{
		}

		private string GetInsertUpdateText(bool isUpdate)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddInsertUpdateParameters("Property_Class", new FieldValue("PropertyClassID", "@PropertyClassID", isUpdateConditionField: true), new FieldValue("PropertyClassName", "@PropertyClassName"), new FieldValue("Note", "@Note"));
			if (isUpdate)
			{
				sqlBuilder.AddInsertUpdateParameters("Property_Class", new FieldValue("DateUpdated", "@DateUpdated", isUpdateConditionField: true, checkForNullValue: true));
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
			parameters.Add("@PropertyClassID", SqlDbType.NVarChar);
			parameters.Add("@PropertyClassName", SqlDbType.NVarChar);
			parameters.Add("@Note", SqlDbType.NVarChar);
			parameters["@PropertyClassID"].SourceColumn = "PropertyClassID";
			parameters["@PropertyClassName"].SourceColumn = "PropertyClassName";
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

		public bool InsertPropertyClass(PropertyClassData accountPropertyClassData)
		{
			bool result = true;
			SqlCommand insertUpdateCommand = GetInsertUpdateCommand(isUpdate: false);
			SqlTransaction sqlTransaction = null;
			try
			{
				sqlTransaction = (insertUpdateCommand.Transaction = base.DBConfig.StartNewTransaction());
				result = Insert(accountPropertyClassData, "Property_Class", insertUpdateCommand);
				string text = accountPropertyClassData.PropertyClassTable.Rows[0]["PropertyClassID"].ToString();
				AddActivityLog("PropertyClass", text, ActivityTypes.Add, sqlTransaction);
				UpdateTableRowInsertUpdateInfo("Property_Class", "PropertyClassID", text, sqlTransaction, isInsert: true);
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

		public bool UpdatePropertyClass(PropertyClassData accountPropertyClassData)
		{
			bool flag = true;
			SqlCommand insertUpdateCommand = GetInsertUpdateCommand(isUpdate: true);
			SqlTransaction sqlTransaction = null;
			try
			{
				sqlTransaction = (insertUpdateCommand.Transaction = base.DBConfig.StartNewTransaction());
				flag = Update(accountPropertyClassData, "Property_Class", insertUpdateCommand);
				if (!flag)
				{
					return flag;
				}
				object obj = accountPropertyClassData.PropertyClassTable.Rows[0]["PropertyClassID"];
				UpdateTableRowByID("Property_Class", "PropertyClassID", "DateUpdated", obj, DateTime.Now, sqlTransaction);
				string entiyID = accountPropertyClassData.PropertyClassTable.Rows[0]["PropertyClassName"].ToString();
				AddActivityLog("PropertyClass", entiyID, ActivityTypes.Update, sqlTransaction);
				UpdateTableRowInsertUpdateInfo("Property_Class", "PropertyClassID", obj, sqlTransaction, isInsert: false);
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

		public PropertyClassData GetPropertyClass()
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddTable("Property_Class");
			PropertyClassData propertyClassData = new PropertyClassData();
			sqlBuilder.UseDistinct = false;
			FillDataSet(propertyClassData, "Property_Class", sqlBuilder);
			return propertyClassData;
		}

		public bool DeletePropertyClass(string areaID)
		{
			bool flag = true;
			try
			{
				string commandText = "DELETE FROM Property_Class WHERE PropertyClassID = '" + areaID + "'";
				flag = Delete(commandText, null);
				if (!flag)
				{
					return flag;
				}
				AddActivityLog("PropertyClass", areaID, ActivityTypes.Delete, null);
				return flag;
			}
			catch
			{
				throw;
			}
		}

		public PropertyClassData GetPropertyClassByID(string id)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			CommandHelper commandHelper = null;
			commandHelper = new CommandHelper();
			commandHelper.FieldName = "PropertyClassID";
			commandHelper.SqlFieldType = SqlDbType.NVarChar;
			commandHelper.FieldValue = id;
			commandHelper.TableName = "Property_Class";
			sqlBuilder.AddCommandHelper(commandHelper);
			sqlBuilder.IsComparing = true;
			PropertyClassData propertyClassData = new PropertyClassData();
			sqlBuilder.UseDistinct = false;
			FillDataSet(propertyClassData, "Property_Class", sqlBuilder);
			return propertyClassData;
		}

		public DataSet GetPropertyClassByFields(params string[] columns)
		{
			return GetPropertyClassByFields(null, isInactive: true, columns);
		}

		public DataSet GetPropertyClassByFields(string[] areaID, params string[] columns)
		{
			return GetPropertyClassByFields(areaID, isInactive: true, columns);
		}

		public DataSet GetPropertyClassByFields(string[] ids, bool isInactive, params string[] columns)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddTable("Property_Class");
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
				commandHelper.FieldName = "PropertyClassID";
				commandHelper.FieldValue = ids;
				commandHelper.SqlFieldType = SqlDbType.NVarChar;
				commandHelper.TableName = "Property_Class";
				sqlBuilder.AddCommandHelper(commandHelper);
			}
			DataSet dataSet = new DataSet();
			dataSet.EnforceConstraints = false;
			sqlBuilder.UseDistinct = false;
			FillDataSet(dataSet, "Property_Class", sqlBuilder);
			return dataSet;
		}

		public DataSet GetPropertyClassList()
		{
			DataSet dataSet = new DataSet();
			string textCommand = "SELECT PropertyClassID [PropertyClass Code],PropertyClassName [PropertyClass Name],Note\r\n                           FROM Property_Class ";
			FillDataSet(dataSet, "Property_Class", textCommand);
			return dataSet;
		}

		public DataSet GetPropertyClassComboList()
		{
			DataSet dataSet = new DataSet();
			string textCommand = "SELECT PropertyClassID [Code],PropertyClassName [Name]\r\n                           FROM Property_Class ORDER BY PropertyClassID,PropertyClassName";
			FillDataSet(dataSet, "Property_Class", textCommand);
			return dataSet;
		}
	}
}
