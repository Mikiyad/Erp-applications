using Micromind.Common;
using Micromind.Common.Data;
using Micromind.Common.Libraries;
using System;
using System.Data;
using System.Data.SqlClient;

namespace Micromind.Data
{
	public sealed class EntityCategory : StoreObject
	{
		private const string ENTITYCATEGORYID_PARM = "@CategoryID";

		private const string ENTITYID_PARM = "@EntityID";

		private const string ENTITYTYPE_PARM = "@EntityType";

		private const string ENTITYCATEGORYNAME_PARM = "@CategoryName";

		public const string NOTE_PARM = "@Note";

		public const string INACTIVE_PARM = "@Inactive";

		public const string PARENTCATEGORYID_PARM = "@ParentCategoryID";

		public const string ENTITYCATEGORY_TABLE = "Entity_Category";

		private const string CREATEDBY_PARM = "@CreatedBy";

		private const string DATECREATED_PARM = "@DateCreated";

		private const string UPDATEDBY_PARM = "@UpdatedBy";

		private const string DATEUPDATED_PARM = "@DateUpdated";

		public EntityCategory(Config config)
			: base(config)
		{
		}

		private string GetInsertUpdateText(bool isUpdate)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddInsertUpdateParameters("Entity_Category", new FieldValue("CategoryID", "@CategoryID", isUpdateConditionField: true), new FieldValue("CategoryName", "@CategoryName"), new FieldValue("EntityType", "@EntityType"), new FieldValue("Inactive", "@Inactive"), new FieldValue("ParentCategoryID", "@ParentCategoryID"), new FieldValue("Note", "@Note"));
			if (isUpdate)
			{
				sqlBuilder.AddInsertUpdateParameters("Entity_Category", new FieldValue("DateUpdated", "@DateUpdated", isUpdateConditionField: true, checkForNullValue: true));
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
			parameters.Add("@EntityType", SqlDbType.TinyInt);
			parameters.Add("@Note", SqlDbType.NVarChar);
			parameters.Add("@Inactive", SqlDbType.Bit);
			parameters.Add("@ParentCategoryID", SqlDbType.NVarChar);
			parameters["@CategoryID"].SourceColumn = "CategoryID";
			parameters["@CategoryName"].SourceColumn = "CategoryName";
			parameters["@EntityType"].SourceColumn = "EntityType";
			parameters["@Note"].SourceColumn = "Note";
			parameters["@Inactive"].SourceColumn = "Inactive";
			parameters["@ParentCategoryID"].SourceColumn = "ParentCategoryID";
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
			sqlBuilder.AddInsertUpdateParameters("Entity_Category_Detail", new FieldValue("EntityID", "@EntityID"), new FieldValue("EntityType", "@EntityType"), new FieldValue("CategoryID", "@CategoryID"));
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
			parameters.Add("@EntityID", SqlDbType.NVarChar);
			parameters.Add("@EntityType", SqlDbType.TinyInt);
			parameters["@CategoryID"].SourceColumn = "CategoryID";
			parameters["@EntityID"].SourceColumn = "EntityID";
			parameters["@EntityType"].SourceColumn = "EntityType";
			if (isUpdate)
			{
				return updateCommand;
			}
			return insertCommand;
		}

		public bool InsertEntityCategory(EntityCategoryData accountEntityCategoryData)
		{
			bool result = true;
			SqlCommand insertUpdateCommand = GetInsertUpdateCommand(isUpdate: false);
			SqlTransaction sqlTransaction = null;
			try
			{
				sqlTransaction = (insertUpdateCommand.Transaction = base.DBConfig.StartNewTransaction());
				result = Insert(accountEntityCategoryData, "Entity_Category", insertUpdateCommand);
				string text = accountEntityCategoryData.EntityCategoryTable.Rows[0]["CategoryID"].ToString();
				AddActivityLog("Entity Category", text, ActivityTypes.Add, sqlTransaction);
				UpdateTableRowInsertUpdateInfo("Entity_Category", "CategoryID", text, sqlTransaction, isInsert: true);
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

		public bool InsertEntityCategoryAssignment(EntityCategoryData data, string entityID)
		{
			bool flag = true;
			SqlCommand insertUpdateDetailCommand = GetInsertUpdateDetailCommand(isUpdate: false);
			SqlTransaction sqlTransaction = null;
			try
			{
				sqlTransaction = base.DBConfig.StartNewTransaction();
				string exp = "DELETE FROM ENTITY_CATEGORY_DETAIL WHERE ENTITYid = '" + entityID + "'";
				flag = (ExecuteNonQuery(exp, sqlTransaction) >= 0);
				if (data.EntityCategoryDetailTable.Rows.Count <= 0)
				{
					return flag;
				}
				insertUpdateDetailCommand.Transaction = sqlTransaction;
				flag &= Insert(data, "Entity_Category_Detail", insertUpdateDetailCommand);
				if (flag)
				{
					foreach (DataRow row in data.EntityCategoryDetailTable.Rows)
					{
						string text = DateTime.Now.ToString(StoreConfiguration.CurrentCulture);
						exp = "UPDATE ENTITY_CATEGORY_DETAIL SET DATEUPDATED='" + text + "'  WHERE ENTITYid = '" + entityID + "' AND CategoryID='" + row["CategoryID"] + "' AND EntityType=" + int.Parse(row["EntityType"].ToString());
						flag &= (ExecuteNonQuery(exp, sqlTransaction) >= 0);
					}
					return flag;
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

		public bool UpdateEntityCategory(EntityCategoryData accountEntityCategoryData)
		{
			bool flag = true;
			SqlCommand insertUpdateCommand = GetInsertUpdateCommand(isUpdate: true);
			SqlTransaction sqlTransaction = null;
			try
			{
				sqlTransaction = (insertUpdateCommand.Transaction = base.DBConfig.StartNewTransaction());
				flag = Update(accountEntityCategoryData, "Entity_Category", insertUpdateCommand);
				if (!flag)
				{
					return flag;
				}
				object obj = accountEntityCategoryData.EntityCategoryTable.Rows[0]["CategoryID"];
				UpdateTableRowByID("Entity_Category", "CategoryID", "DateUpdated", obj, DateTime.Now, sqlTransaction);
				string entiyID = accountEntityCategoryData.EntityCategoryTable.Rows[0]["CategoryName"].ToString();
				AddActivityLog("Entity Category", entiyID, ActivityTypes.Update, sqlTransaction);
				UpdateTableRowInsertUpdateInfo("Entity_Category", "CategoryID", obj, sqlTransaction, isInsert: false);
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

		public EntityCategoryData GetEntityCategory()
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddTable("Entity_Category");
			EntityCategoryData entityCategoryData = new EntityCategoryData();
			sqlBuilder.UseDistinct = false;
			FillDataSet(entityCategoryData, "Entity_Category", sqlBuilder);
			return entityCategoryData;
		}

		public bool DeleteEntityCategory(string entityCategoryID, EntityTypesEnum entityType)
		{
			bool flag = true;
			try
			{
				string commandText = "DELETE FROM Entity_Category WHERE CategoryID = '" + entityCategoryID + "' AND EntityType = " + (int)entityType;
				flag = Delete(commandText, null);
				if (!flag)
				{
					return flag;
				}
				AddActivityLog("Entity Category", entityCategoryID, ActivityTypes.Delete, null);
				return flag;
			}
			catch
			{
				throw;
			}
		}

		public EntityCategoryData GetEntityCategoryByID(string id)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			CommandHelper commandHelper = null;
			commandHelper = new CommandHelper();
			commandHelper.FieldName = "CategoryID";
			commandHelper.SqlFieldType = SqlDbType.NVarChar;
			commandHelper.FieldValue = id;
			commandHelper.TableName = "Entity_Category";
			sqlBuilder.AddCommandHelper(commandHelper);
			sqlBuilder.IsComparing = true;
			EntityCategoryData entityCategoryData = new EntityCategoryData();
			sqlBuilder.UseDistinct = false;
			FillDataSet(entityCategoryData, "Entity_Category", sqlBuilder);
			return entityCategoryData;
		}

		public DataSet GetEntityCategoryByFields(params string[] columns)
		{
			return GetEntityCategoryByFields(null, isInactive: true, columns);
		}

		public DataSet GetEntityCategoryByFields(string[] entityCategoryID, params string[] columns)
		{
			return GetEntityCategoryByFields(entityCategoryID, isInactive: true, columns);
		}

		public DataSet GetEntityCategoryByFields(string[] ids, bool isInactive, params string[] columns)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddTable("Entity_Category");
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
				commandHelper.TableName = "Entity_Category";
				sqlBuilder.AddCommandHelper(commandHelper);
			}
			DataSet dataSet = new DataSet();
			dataSet.EnforceConstraints = false;
			sqlBuilder.UseDistinct = false;
			FillDataSet(dataSet, "Entity_Category", sqlBuilder);
			return dataSet;
		}

		public DataSet GetEntityCategoryList(EntityTypesEnum entityType)
		{
			DataSet dataSet = new DataSet();
			string textCommand = "SELECT CategoryID [Code],CategoryName [Name],Note,Inactive\r\n                           FROM Entity_Category WHERE EntityType = " + (int)entityType;
			FillDataSet(dataSet, "Entity_Category", textCommand);
			return dataSet;
		}

		public DataSet GetEntityCategoryCombosDataList()
		{
			DataSet dataSet = new DataSet();
			string textCommand = "SELECT CategoryId as Code,CategoryName as Name,EntityType FROM Entity_Category";
			FillDataSet(dataSet, "Entity_Category", textCommand);
			return dataSet;
		}

		public DataSet GetActiveEntityCategoryList(EntityTypesEnum entityType)
		{
			DataSet dataSet = new DataSet();
			string textCommand = "SELECT CategoryID [Code],CategoryName [Name] \r\n                           FROM Entity_Category WHERE IsNull(Inactive,'False') = 'False' ";
			FillDataSet(dataSet, "Entity_Category", textCommand);
			return dataSet;
		}

		public DataSet GetEntityAssignedCategorysList(string entityID, EntityTypesEnum entityType)
		{
			DataSet dataSet = new DataSet();
			string text = "";
			text = "SELECT CONVERT(bit,(SELECT COUNT (EntityID) FROM Entity_Category_Detail WHERE EntityID = '" + entityID + "' \r\n\t\t                            AND EntityID IN (SELECT EntityID FROM Entity_Category_Detail WHERE EntityType = " + (int)entityType + " \r\n                    AND CategoryID = CG.CategoryID))) AS C, CategoryID AS Code,CategoryName Name FROM Entity_Category CG WHERE CG.EntityType = " + (int)entityType;
			FillDataSet(dataSet, "Entity_Category_Detail", text);
			return dataSet;
		}

		public DataSet GetEntityAssignedCategorysTreeViewList(string entityID, EntityTypesEnum entityType)
		{
			DataSet dataSet = new DataSet();
			string text = "";
			text = "select DISTINCT CONVERT(bit,(SELECT COUNT (EntityID) FROM Entity_Category_Detail WHERE EntityID = '" + entityID + "' \r\n\t\t                            AND EntityID IN (SELECT EntityID FROM Entity_Category_Detail WHERE EntityType = " + (int)entityType + " \r\n                    AND CategoryID = Parent.CategoryID))) AS C,Parent.CategoryID [Code], Parent.CategoryName [Name],'' as ParentID,'True' as IsParent from Entity_Category Parent LEFT JOIN Entity_Category ON Parent.ParentCategoryID=Entity_Category.CategoryId where Parent.ParentCategoryID IS  NULL AND Parent.EntityType=" + (int)entityType;
			FillDataSet(dataSet, "ParentCustomerCategory", text);
			text = "Select CONVERT(bit,(SELECT COUNT (EntityID) FROM Entity_Category_Detail WHERE EntityID = '" + entityID + "' \r\n\t\t                            AND EntityID IN (SELECT EntityID FROM Entity_Category_Detail WHERE EntityType = " + (int)entityType + " \r\n                    AND CategoryID = Entity_Category.CategoryID))) AS C,CategoryID [Code],CategoryName [Name],ParentCategoryID as ParentID,'False' as IsParent from Entity_Category Where EntityType=" + (int)entityType + " AND ParentCategoryID IS NOT NULL ";
			FillDataSet(dataSet, "CustomerCategory", text);
			dataSet.Relations.Add("Rel", dataSet.Tables["ParentCustomerCategory"].Columns["Code"], dataSet.Tables["CustomerCategory"].Columns["ParentID"], createConstraints: false);
			dataSet.Relations.Add("Rel1", dataSet.Tables["CustomerCategory"].Columns["Code"], dataSet.Tables["CustomerCategory"].Columns["ParentID"], createConstraints: false);
			return dataSet;
		}

		public DataSet GetEntityCategoryComboList()
		{
			DataSet dataSet = new DataSet();
			string textCommand = "SELECT CategoryID [Code],CategoryName [Name]\r\n                           FROM Entity_Category ORDER BY CategoryID,CategoryName";
			FillDataSet(dataSet, "Entity_Category", textCommand);
			return dataSet;
		}

		public DataSet GetCustomerCategoryTreeview(bool includeInactive, bool isHierarchy)
		{
			string text = "";
			DataSet dataSet;
			if (isHierarchy)
			{
				dataSet = new DataSet();
				text = "select DISTINCT Parent.CategoryID, Parent.CategoryName,'' as ParentID,Parent.Note,'True' as IsParent from Entity_Category Parent LEFT JOIN Entity_Category ON Parent.ParentCategoryID=Entity_Category.CategoryId where Parent.ParentCategoryID IS  NULL AND Parent.EntityType=1";
				FillDataSet(dataSet, "ParentCustomerCategory", text);
				text = "Select CategoryID,CategoryName,ParentCategoryID as ParentID,Note,'False' as IsParent from Entity_Category Where EntityType=1 AND ParentCategoryID IS NOT NULL ";
				if (!includeInactive)
				{
					text += " AND (Entity_Category.Inactive IS NULL OR Entity_Category.Inactive='False')";
				}
				FillDataSet(dataSet, "CustomerCategory", text);
				dataSet.Relations.Add("Rel", dataSet.Tables["ParentCustomerCategory"].Columns["CategoryID"], dataSet.Tables["CustomerCategory"].Columns["ParentID"], createConstraints: false);
				dataSet.Relations.Add("Rel1", dataSet.Tables["CustomerCategory"].Columns["CategoryID"], dataSet.Tables["CustomerCategory"].Columns["ParentID"], createConstraints: false);
			}
			else
			{
				dataSet = new DataSet();
				text = "Select Entity_Category.CategoryID,Entity_Category.CategoryName,Parent.CategoryName as Parent,Entity_Category.Note from Entity_Category Left join Entity_Category Parent ON Parent.CategoryId=Entity_Category.ParentCategoryID Where Entity_Category.EntityType=1 AND Entity_Category.ParentCategoryID IS NOT NULL ";
				if (!includeInactive)
				{
					text += " AND (Entity_Category.Inactive IS NULL OR Entity_Category.Inactive='False')";
				}
				FillDataSet(dataSet, "CustomerCategory", text);
			}
			return dataSet;
		}
	}
}
