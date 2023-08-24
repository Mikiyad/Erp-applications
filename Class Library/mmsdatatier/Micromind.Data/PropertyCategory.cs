using Micromind.Common;
using Micromind.Common.Data;
using Micromind.Common.Libraries;
using System;
using System.Data;
using System.Data.SqlClient;

namespace Micromind.Data
{
	public sealed class PropertyCategory : StoreObject
	{
		private const string PROPERTYCATEGORYID_PARM = "@CategoryID";

		private const string PROPERTYID_PARM = "@CustomerID";

		private const string ENTITYTYPE_PARM = "@EntityType";

		private const string PROPERTYCATEGORYNAME_PARM = "@CategoryName";

		public const string NOTE_PARM = "@Note";

		public const string INACTIVE_PARM = "@Inactive";

		public const string PROPERTYCATEGORY_TABLE = "Property_Category";

		private const string CREATEDBY_PARM = "@CreatedBy";

		private const string DATECREATED_PARM = "@DateCreated";

		private const string UPDATEDBY_PARM = "@UpdatedBy";

		private const string DATEUPDATED_PARM = "@DateUpdated";

		public PropertyCategory(Config config)
			: base(config)
		{
		}

		private string GetInsertUpdateText(bool isUpdate)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddInsertUpdateParameters("Property_Category", new FieldValue("CategoryID", "@CategoryID", isUpdateConditionField: true), new FieldValue("CategoryName", "@CategoryName"), new FieldValue("Inactive", "@Inactive"), new FieldValue("Note", "@Note"));
			if (isUpdate)
			{
				sqlBuilder.AddInsertUpdateParameters("Property_Category", new FieldValue("DateUpdated", "@DateUpdated", isUpdateConditionField: true, checkForNullValue: true));
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
			parameters.Add("@CategoryID", SqlDbType.NVarChar);
			parameters.Add("@CategoryName", SqlDbType.NVarChar);
			parameters.Add("@Note", SqlDbType.NVarChar);
			parameters.Add("@Inactive", SqlDbType.Bit);
			parameters["@CategoryID"].SourceColumn = "CategoryID";
			parameters["@CategoryName"].SourceColumn = "CategoryName";
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

		private string GetInsertUpdateDetailText(bool isUpdate)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddInsertUpdateParameters("Property_Category_Detail", new FieldValue("PropertyID", "@CustomerID"), new FieldValue("EntityType", "@EntityType"), new FieldValue("CategoryID", "@CategoryID"));
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
			parameters.Add("@CategoryID", SqlDbType.NVarChar);
			parameters.Add("@CustomerID", SqlDbType.NVarChar);
			parameters.Add("@EntityType", SqlDbType.TinyInt);
			parameters["@CategoryID"].SourceColumn = "CategoryID";
			parameters["@CustomerID"].SourceColumn = "PropertyID";
			parameters["@EntityType"].SourceColumn = "EntityType";
			if (isUpdate)
			{
				return updateCommand;
			}
			return insertCommand;
		}

		public bool InsertPropertyCategory(PropertyCategoryData accountPropertyCategoryData)
		{
			bool result = true;
			SqlCommand insertUpdateCommand = GetInsertUpdateCommand(isUpdate: false);
			SqlTransaction sqlTransaction = null;
			try
			{
				sqlTransaction = (insertUpdateCommand.Transaction = base.DBConfig.StartNewTransaction());
				result = Insert(accountPropertyCategoryData, "Property_Category", insertUpdateCommand);
				string text = accountPropertyCategoryData.PropertyCategoryTable.Rows[0]["CategoryID"].ToString();
				AddActivityLog("PropertyCategory", text, ActivityTypes.Add, sqlTransaction);
				UpdateTableRowInsertUpdateInfo("Property_Category", "CategoryID", text, sqlTransaction, isInsert: true);
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

		public bool InsertPropertyCategoryAssignment(PropertyCategoryData data, string customerID)
		{
			bool flag = true;
			SqlCommand insertUpdateDetailCommand = GetInsertUpdateDetailCommand(isUpdate: false);
			SqlTransaction sqlTransaction = null;
			try
			{
				sqlTransaction = base.DBConfig.StartNewTransaction();
				string exp = "DELETE FROM PROPERTY_CATEGORY_DETAIL WHERE PROPERTYid='" + customerID + "'";
				flag &= (ExecuteNonQuery(exp, sqlTransaction) >= 0);
				insertUpdateDetailCommand.Transaction = sqlTransaction;
				if (data.PropertyCategoryDetailTable.Rows.Count <= 0)
				{
					return flag;
				}
				flag &= Insert(data, "Property_Category_Detail", insertUpdateDetailCommand);
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

		public bool UpdatePropertyCategory(PropertyCategoryData accountPropertyCategoryData)
		{
			bool flag = true;
			SqlCommand insertUpdateCommand = GetInsertUpdateCommand(isUpdate: true);
			SqlTransaction sqlTransaction = null;
			try
			{
				sqlTransaction = (insertUpdateCommand.Transaction = base.DBConfig.StartNewTransaction());
				flag = Update(accountPropertyCategoryData, "Property_Category", insertUpdateCommand);
				if (!flag)
				{
					return flag;
				}
				object obj = accountPropertyCategoryData.PropertyCategoryTable.Rows[0]["CategoryID"];
				UpdateTableRowByID("Property_Category", "CategoryID", "DateUpdated", obj, DateTime.Now, sqlTransaction);
				string entiyID = accountPropertyCategoryData.PropertyCategoryTable.Rows[0]["CategoryName"].ToString();
				AddActivityLog("PropertyCategory", entiyID, ActivityTypes.Update, sqlTransaction);
				UpdateTableRowInsertUpdateInfo("Property_Category", "CategoryID", obj, sqlTransaction, isInsert: false);
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

		public PropertyCategoryData GetPropertyCategory()
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddTable("Property_Category");
			PropertyCategoryData propertyCategoryData = new PropertyCategoryData();
			sqlBuilder.UseDistinct = false;
			FillDataSet(propertyCategoryData, "Property_Category", sqlBuilder);
			return propertyCategoryData;
		}

		public bool DeletePropertyCategory(string customerCategoryID)
		{
			bool flag = true;
			try
			{
				string commandText = "DELETE FROM Property_Category WHERE CategoryID = '" + customerCategoryID + "'";
				flag = Delete(commandText, null);
				if (!flag)
				{
					return flag;
				}
				AddActivityLog("PropertyCategory", customerCategoryID, ActivityTypes.Delete, null);
				return flag;
			}
			catch
			{
				throw;
			}
		}

		public PropertyCategoryData GetPropertyCategoryByID(string id)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			CommandHelper commandHelper = null;
			commandHelper = new CommandHelper();
			commandHelper.FieldName = "CategoryID";
			commandHelper.SqlFieldType = SqlDbType.NVarChar;
			commandHelper.FieldValue = id;
			commandHelper.TableName = "Property_Category";
			sqlBuilder.AddCommandHelper(commandHelper);
			sqlBuilder.IsComparing = true;
			PropertyCategoryData propertyCategoryData = new PropertyCategoryData();
			sqlBuilder.UseDistinct = false;
			FillDataSet(propertyCategoryData, "Property_Category", sqlBuilder);
			return propertyCategoryData;
		}

		public DataSet GetPropertyCategoryByFields(params string[] columns)
		{
			return GetPropertyCategoryByFields(null, isInactive: true, columns);
		}

		public DataSet GetPropertyCategoryByFields(string[] customerCategoryID, params string[] columns)
		{
			return GetPropertyCategoryByFields(customerCategoryID, isInactive: true, columns);
		}

		public DataSet GetPropertyCategoryByFields(string[] ids, bool isInactive, params string[] columns)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddTable("Property_Category");
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
				commandHelper.FieldName = "CategoryID";
				commandHelper.FieldValue = ids;
				commandHelper.SqlFieldType = SqlDbType.NVarChar;
				commandHelper.TableName = "Property_Category";
				sqlBuilder.AddCommandHelper(commandHelper);
			}
			DataSet dataSet = new DataSet();
			dataSet.EnforceConstraints = false;
			sqlBuilder.UseDistinct = false;
			FillDataSet(dataSet, "Property_Category", sqlBuilder);
			return dataSet;
		}

		public DataSet GetPropertyCategoryList()
		{
			DataSet dataSet = new DataSet();
			string textCommand = "SELECT CategoryID [Code],CategoryName [Name],Note,Inactive\r\n                           FROM Property_Category ";
			FillDataSet(dataSet, "Property_Category", textCommand);
			return dataSet;
		}

		public DataSet GetPropertyAssignedCategorysList(string entityID, EntityTypesEnum entityType)
		{
			DataSet dataSet = new DataSet();
			string textCommand = "";
			if (entityType == EntityTypesEnum.Properties)
			{
				textCommand = "SELECT CONVERT(bit,(SELECT COUNT (PropertyID) FROM Property WHERE PropertyID = '" + entityID + "' \r\n\t\t                            AND PropertyID IN (SELECT PropertyID FROM Property_Category_Detail WHERE EntityType = 14 AND CategoryID = CG.CategoryID))) AS C,\r\n                             CategoryID AS Code,CategoryName Name FROM Property_Category CG ";
			}
			FillDataSet(dataSet, "Property_Category_Detail", textCommand);
			return dataSet;
		}

		public DataSet GetPropertyCategoryComboList()
		{
			DataSet dataSet = new DataSet();
			string textCommand = "SELECT CategoryID [Code],CategoryName [Name]\r\n                           FROM Property_Category ORDER BY CategoryID,CategoryName";
			FillDataSet(dataSet, "Property_Category", textCommand);
			return dataSet;
		}
	}
}
