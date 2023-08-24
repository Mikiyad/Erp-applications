using Micromind.Common;
using Micromind.Common.Data;
using Micromind.Common.Libraries;
using System;
using System.Data;
using System.Data.SqlClient;

namespace Micromind.Data
{
	public sealed class ProductBrands : StoreObject
	{
		private const string BRANDID_PARM = "@BrandID";

		private const string BRANDNAME_PARM = "@BrandName";

		public const string NOTE_PARM = "@Note";

		private const string PREFERREDVENDOR_PARM = "@PreferredVendor";

		public const string ISINACTIVE_PARM = "@Inactive";

		public const string PRODUCTBRAND_TABLE = "Product_Brand";

		private const string CREATEDBY_PARM = "@CreatedBy";

		private const string DATECREATED_PARM = "@DateCreated";

		private const string UPDATEDBY_PARM = "@UpdatedBy";

		private const string DATEUPDATED_PARM = "@DateUpdated";

		public ProductBrands(Config config)
			: base(config)
		{
		}

		private string GetInsertUpdateText(bool isUpdate)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddInsertUpdateParameters("Product_Brand", new FieldValue("BrandID", "@BrandID", isUpdateConditionField: true), new FieldValue("BrandName", "@BrandName"), new FieldValue("IsInactive", "@Inactive"), new FieldValue("PreferredVendor", "@PreferredVendor"), new FieldValue("Note", "@Note"));
			if (isUpdate)
			{
				sqlBuilder.AddInsertUpdateParameters("Product_Brand", new FieldValue("DateUpdated", "@DateUpdated", isUpdateConditionField: true, checkForNullValue: true));
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
			parameters.Add("@BrandID", SqlDbType.NVarChar);
			parameters.Add("@BrandName", SqlDbType.NVarChar);
			parameters.Add("@Inactive", SqlDbType.Bit);
			parameters.Add("@PreferredVendor", SqlDbType.NVarChar);
			parameters.Add("@Note", SqlDbType.NVarChar);
			parameters["@BrandID"].SourceColumn = "BrandID";
			parameters["@BrandName"].SourceColumn = "BrandName";
			parameters["@Inactive"].SourceColumn = "IsInactive";
			parameters["@PreferredVendor"].SourceColumn = "PreferredVendor";
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

		public bool InsertProductBrand(ProductBrandData accountProductBrandData)
		{
			bool result = true;
			SqlCommand insertUpdateCommand = GetInsertUpdateCommand(isUpdate: false);
			SqlTransaction sqlTransaction = null;
			try
			{
				sqlTransaction = (insertUpdateCommand.Transaction = base.DBConfig.StartNewTransaction());
				result = Insert(accountProductBrandData, "Product_Brand", insertUpdateCommand);
				string text = accountProductBrandData.ProductBrandTable.Rows[0]["BrandID"].ToString();
				AddActivityLog("Product Brand", text, ActivityTypes.Add, sqlTransaction);
				UpdateTableRowInsertUpdateInfo("Product_Brand", "BrandID", text, sqlTransaction, isInsert: true);
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

		public bool UpdateProductBrand(ProductBrandData accountProductBrandData)
		{
			bool flag = true;
			SqlCommand insertUpdateCommand = GetInsertUpdateCommand(isUpdate: true);
			SqlTransaction sqlTransaction = null;
			try
			{
				sqlTransaction = (insertUpdateCommand.Transaction = base.DBConfig.StartNewTransaction());
				flag = Update(accountProductBrandData, "Product_Brand", insertUpdateCommand);
				if (!flag)
				{
					return flag;
				}
				object obj = accountProductBrandData.ProductBrandTable.Rows[0]["BrandID"];
				UpdateTableRowByID("Product_Brand", "BrandID", "DateUpdated", obj, DateTime.Now, sqlTransaction);
				string entiyID = accountProductBrandData.ProductBrandTable.Rows[0]["BrandName"].ToString();
				AddActivityLog("Product Brand", entiyID, ActivityTypes.Update, sqlTransaction);
				UpdateTableRowInsertUpdateInfo("Product_Brand", "BrandID", obj, sqlTransaction, isInsert: false);
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

		public ProductBrandData GetProductBrand()
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddTable("Product_Brand");
			ProductBrandData productBrandData = new ProductBrandData();
			sqlBuilder.UseDistinct = false;
			FillDataSet(productBrandData, "Product_Brand", sqlBuilder);
			return productBrandData;
		}

		public bool DeleteProductBrand(string productBrandID)
		{
			bool flag = true;
			try
			{
				string commandText = "DELETE FROM Product_Brand WHERE BrandID = '" + productBrandID + "'";
				flag = Delete(commandText, null);
				if (!flag)
				{
					return flag;
				}
				AddActivityLog("Product Brand", productBrandID, ActivityTypes.Delete, null);
				return flag;
			}
			catch
			{
				throw;
			}
		}

		public ProductBrandData GetProductBrandByID(string id)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			CommandHelper commandHelper = null;
			commandHelper = new CommandHelper();
			commandHelper.FieldName = "BrandID";
			commandHelper.SqlFieldType = SqlDbType.NVarChar;
			commandHelper.FieldValue = id;
			commandHelper.TableName = "Product_Brand";
			sqlBuilder.AddCommandHelper(commandHelper);
			sqlBuilder.IsComparing = true;
			ProductBrandData productBrandData = new ProductBrandData();
			sqlBuilder.UseDistinct = false;
			FillDataSet(productBrandData, "Product_Brand", sqlBuilder);
			return productBrandData;
		}

		public DataSet GetProductBrandByFields(params string[] columns)
		{
			return GetProductBrandByFields(null, isInactive: true, columns);
		}

		public DataSet GetProductBrandByFields(string[] productBrandID, params string[] columns)
		{
			return GetProductBrandByFields(productBrandID, isInactive: true, columns);
		}

		public DataSet GetProductBrandByFields(string[] ids, bool isInactive, params string[] columns)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddTable("Product_Brand");
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
				commandHelper.FieldName = "BrandID";
				commandHelper.FieldValue = ids;
				commandHelper.SqlFieldType = SqlDbType.NVarChar;
				commandHelper.TableName = "Product_Brand";
				sqlBuilder.AddCommandHelper(commandHelper);
			}
			DataSet dataSet = new DataSet();
			dataSet.EnforceConstraints = false;
			sqlBuilder.UseDistinct = false;
			FillDataSet(dataSet, "Product_Brand", sqlBuilder);
			return dataSet;
		}

		public DataSet GetProductBrandList()
		{
			DataSet dataSet = new DataSet();
			string textCommand = "SELECT BrandID [Brand Code],BrandName [Brand Name],Note,IsInactive AS [Inactive],PreferredVendor AS [Preferred Vendor]\r\n                           FROM Product_Brand ";
			FillDataSet(dataSet, "Product_Brand", textCommand);
			return dataSet;
		}

		public DataSet GetProductBrandComboList()
		{
			DataSet dataSet = new DataSet();
			string textCommand = "SELECT BrandID [Code],BrandName [Name]\r\n                           FROM Product_Brand ORDER BY BrandID,BrandName";
			FillDataSet(dataSet, "Product_Brand", textCommand);
			return dataSet;
		}
	}
}
