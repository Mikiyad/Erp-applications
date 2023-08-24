using Micromind.Common;
using Micromind.Common.Data;
using Micromind.Common.Libraries;
using System;
using System.Data;
using System.Data.SqlClient;

namespace Micromind.Data
{
	public sealed class ProductAttributes : StoreObject
	{
		private const string ATTRIBUTEID_PARM = "@AttributeID";

		private const string ATTRIBUTENAME_PARM = "@AttributeName";

		public const string ISINACTIVE_PARM = "@IsInactive";

		public const string PRODUCTATTRIBUTE_TABLE = "Product_Attribute";

		private const string CREATEDBY_PARM = "@CreatedBy";

		private const string DATECREATED_PARM = "@DateCreated";

		private const string UPDATEDBY_PARM = "@UpdatedBy";

		private const string DATEUPDATED_PARM = "@DateUpdated";

		public ProductAttributes(Config config)
			: base(config)
		{
		}

		private string GetInsertUpdateText(bool isUpdate)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddInsertUpdateParameters("Product_Attribute", new FieldValue("AttributeID", "@AttributeID", isUpdateConditionField: true), new FieldValue("AttributeName", "@AttributeName"), new FieldValue("IsInactive", "@IsInactive"));
			if (isUpdate)
			{
				sqlBuilder.AddInsertUpdateParameters("Product_Attribute", new FieldValue("DateUpdated", "@DateUpdated", isUpdateConditionField: true, checkForNullValue: true));
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
			parameters.Add("@AttributeID", SqlDbType.NVarChar);
			parameters.Add("@AttributeName", SqlDbType.NVarChar);
			parameters.Add("@IsInactive", SqlDbType.Bit);
			parameters["@AttributeID"].SourceColumn = "AttributeID";
			parameters["@AttributeName"].SourceColumn = "AttributeName";
			parameters["@IsInactive"].SourceColumn = "IsInactive";
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

		public bool InsertProductAttribute(ProductAttributeData accountProductAttributeData)
		{
			bool result = true;
			SqlCommand insertUpdateCommand = GetInsertUpdateCommand(isUpdate: false);
			SqlTransaction sqlTransaction = null;
			try
			{
				sqlTransaction = (insertUpdateCommand.Transaction = base.DBConfig.StartNewTransaction());
				result = Insert(accountProductAttributeData, "Product_Attribute", insertUpdateCommand);
				string text = accountProductAttributeData.ProductAttributeTable.Rows[0]["AttributeID"].ToString();
				AddActivityLog("Product Attribute", text, ActivityTypes.Add, sqlTransaction);
				UpdateTableRowInsertUpdateInfo("Product_Attribute", "AttributeID", text, sqlTransaction, isInsert: true);
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

		public bool UpdateProductAttribute(ProductAttributeData accountProductAttributeData)
		{
			bool flag = true;
			SqlCommand insertUpdateCommand = GetInsertUpdateCommand(isUpdate: true);
			SqlTransaction sqlTransaction = null;
			try
			{
				sqlTransaction = (insertUpdateCommand.Transaction = base.DBConfig.StartNewTransaction());
				flag = Update(accountProductAttributeData, "Product_Attribute", insertUpdateCommand);
				if (!flag)
				{
					return flag;
				}
				object obj = accountProductAttributeData.ProductAttributeTable.Rows[0]["AttributeID"];
				UpdateTableRowByID("Product_Attribute", "AttributeID", "DateUpdated", obj, DateTime.Now, sqlTransaction);
				string entiyID = accountProductAttributeData.ProductAttributeTable.Rows[0]["AttributeName"].ToString();
				AddActivityLog("Product Attribute", entiyID, ActivityTypes.Update, sqlTransaction);
				UpdateTableRowInsertUpdateInfo("Product_Attribute", "AttributeID", obj, sqlTransaction, isInsert: false);
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

		public ProductAttributeData GetProductAttribute()
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddTable("Product_Attribute");
			ProductAttributeData productAttributeData = new ProductAttributeData();
			sqlBuilder.UseDistinct = false;
			FillDataSet(productAttributeData, "Product_Attribute", sqlBuilder);
			return productAttributeData;
		}

		public bool DeleteProductAttribute(string productAttributeID)
		{
			bool flag = true;
			try
			{
				string commandText = "DELETE FROM Product_Attribute WHERE AttributeID = '" + productAttributeID + "'";
				flag = Delete(commandText, null);
				if (!flag)
				{
					return flag;
				}
				AddActivityLog("Product Attribute", productAttributeID, ActivityTypes.Delete, null);
				return flag;
			}
			catch
			{
				throw;
			}
		}

		public ProductAttributeData GetProductAttributeByID(string id)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			CommandHelper commandHelper = null;
			commandHelper = new CommandHelper();
			commandHelper.FieldName = "AttributeID";
			commandHelper.SqlFieldType = SqlDbType.NVarChar;
			commandHelper.FieldValue = id;
			commandHelper.TableName = "Product_Attribute";
			sqlBuilder.AddCommandHelper(commandHelper);
			sqlBuilder.IsComparing = true;
			ProductAttributeData productAttributeData = new ProductAttributeData();
			sqlBuilder.UseDistinct = false;
			FillDataSet(productAttributeData, "Product_Attribute", sqlBuilder);
			return productAttributeData;
		}

		public DataSet GetProductAttributeByFields(params string[] columns)
		{
			return GetProductAttributeByFields(null, isInactive: true, columns);
		}

		public DataSet GetProductAttributeByFields(string[] productAttributeID, params string[] columns)
		{
			return GetProductAttributeByFields(productAttributeID, isInactive: true, columns);
		}

		public DataSet GetProductAttributeByFields(string[] ids, bool isInactive, params string[] columns)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddTable("Product_Attribute");
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
				commandHelper.FieldName = "AttributeID";
				commandHelper.FieldValue = ids;
				commandHelper.SqlFieldType = SqlDbType.NVarChar;
				commandHelper.TableName = "Product_Attribute";
				sqlBuilder.AddCommandHelper(commandHelper);
			}
			DataSet dataSet = new DataSet();
			dataSet.EnforceConstraints = false;
			sqlBuilder.UseDistinct = false;
			FillDataSet(dataSet, "Product_Attribute", sqlBuilder);
			return dataSet;
		}

		public DataSet GetProductAttributeList()
		{
			DataSet dataSet = new DataSet();
			string textCommand = "SELECT AttributeID [Attribute Code],AttributeName [Attribute Name],Note, IsInactive [Inactive]\r\n                           FROM Product_Attribute ";
			FillDataSet(dataSet, "Product_Attribute", textCommand);
			return dataSet;
		}

		public DataSet GetProductAttributeComboList()
		{
			DataSet dataSet = new DataSet();
			string textCommand = "SELECT AttributeID [Code],AttributeName [Name]\r\n                           FROM Product_Attribute ORDER BY AttributeID,AttributeName";
			FillDataSet(dataSet, "Product_Attribute", textCommand);
			return dataSet;
		}
	}
}
