using Micromind.Common;
using Micromind.Common.Data;
using Micromind.Common.Libraries;
using System;
using System.Data;
using System.Data.SqlClient;

namespace Micromind.Data
{
	public sealed class ProductCategories : StoreObject
	{
		private const string CATEGORYID_PARM = "@CategoryID";

		private const string CATEGORYNAME_PARM = "@CategoryName";

		private const string PARENTCATEGORYID_PARM = "@ParentCategoryID";

		private const string AGEGROUP1_PARM = "@AgeGroup1";

		private const string AGEGROUP2_PARM = "@AgeGroup2";

		private const string AGEGROUP3_PARM = "@AgeGroup3";

		private const string ISINACTIVE_PARM = "@IsInactive";

		private const string NOTE_PARM = "@Note";

		private const string STANDARDPRICEPERCENT_PARM = "@StandardPricePercent";

		private const string WHOLESALEPRICEPERCENT_PARM = "@WholesalePricePercent";

		private const string SPECIALPRICEPERCENT_PARM = "@SpecialPricePercent";

		private const string MINIMUMPRICEPERCENT_PARM = "@MinimumPricePercent";

		public const string COMMISSIONPERCENT_PARM = "@CommissionPercent";

		private const string CREATEDBY_PARM = "@CreatedBy";

		private const string DATECREATED_PARM = "@DateCreated";

		private const string UPDATEDBY_PARM = "@UpdatedBy";

		private const string DATEUPDATED_PARM = "@DateUpdated";

		public ProductCategories(Config config)
			: base(config)
		{
		}

		private string GetInsertUpdateText(bool isUpdate)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddInsertUpdateParameters("Product_Category", new FieldValue("CategoryID", "@CategoryID", isUpdateConditionField: true), new FieldValue("CategoryName", "@CategoryName"), new FieldValue("ParentCategoryID", "@ParentCategoryID"), new FieldValue("AgeGroup1", "@AgeGroup1"), new FieldValue("AgeGroup2", "@AgeGroup2"), new FieldValue("AgeGroup3", "@AgeGroup3"), new FieldValue("IsInactive", "@IsInactive"), new FieldValue("StandardPricePercent", "@StandardPricePercent"), new FieldValue("WholesalePricePercent", "@WholesalePricePercent"), new FieldValue("SpecialPricePercent", "@SpecialPricePercent"), new FieldValue("MinimumPricePercent", "@MinimumPricePercent"), new FieldValue("CommissionPercent", "@CommissionPercent"), new FieldValue("Note", "@Note"));
			if (isUpdate)
			{
				sqlBuilder.AddInsertUpdateParameters("Product_Category", new FieldValue("DateUpdated", "@DateUpdated", isUpdateConditionField: true, checkForNullValue: true));
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
			parameters.Add("@ParentCategoryID", SqlDbType.NVarChar);
			parameters.Add("@AgeGroup1", SqlDbType.SmallInt);
			parameters.Add("@AgeGroup2", SqlDbType.SmallInt);
			parameters.Add("@AgeGroup3", SqlDbType.SmallInt);
			parameters.Add("@Note", SqlDbType.NVarChar);
			parameters.Add("@StandardPricePercent", SqlDbType.Real);
			parameters.Add("@WholesalePricePercent", SqlDbType.Real);
			parameters.Add("@SpecialPricePercent", SqlDbType.Real);
			parameters.Add("@MinimumPricePercent", SqlDbType.Real);
			parameters.Add("@CommissionPercent", SqlDbType.Real);
			parameters.Add("@IsInactive", SqlDbType.Bit);
			parameters["@CategoryID"].SourceColumn = "CategoryID";
			parameters["@CategoryName"].SourceColumn = "CategoryName";
			parameters["@ParentCategoryID"].SourceColumn = "ParentCategoryID";
			parameters["@AgeGroup1"].SourceColumn = "AgeGroup1";
			parameters["@AgeGroup2"].SourceColumn = "AgeGroup2";
			parameters["@AgeGroup3"].SourceColumn = "AgeGroup3";
			parameters["@Note"].SourceColumn = "Note";
			parameters["@StandardPricePercent"].SourceColumn = "StandardPricePercent";
			parameters["@WholesalePricePercent"].SourceColumn = "WholesalePricePercent";
			parameters["@SpecialPricePercent"].SourceColumn = "SpecialPricePercent";
			parameters["@MinimumPricePercent"].SourceColumn = "MinimumPricePercent";
			parameters["@CommissionPercent"].SourceColumn = "CommissionPercent";
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

		public bool InsertProductCategory(ProductCategoryData accountProductCategoryData)
		{
			bool result = true;
			SqlCommand insertUpdateCommand = GetInsertUpdateCommand(isUpdate: false);
			SqlTransaction sqlTransaction = null;
			try
			{
				sqlTransaction = (insertUpdateCommand.Transaction = base.DBConfig.StartNewTransaction());
				result = Insert(accountProductCategoryData, "Product_Category", insertUpdateCommand);
				string text = accountProductCategoryData.ProductCategoryTable.Rows[0]["CategoryID"].ToString();
				AddActivityLog("Product Category", text, ActivityTypes.Add, sqlTransaction);
				UpdateTableRowInsertUpdateInfo("Product_Category", "CategoryID", text, sqlTransaction, isInsert: true);
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

		public bool UpdateProductCategory(ProductCategoryData accountProductCategoryData)
		{
			bool flag = true;
			SqlCommand insertUpdateCommand = GetInsertUpdateCommand(isUpdate: true);
			SqlTransaction sqlTransaction = null;
			try
			{
				sqlTransaction = (insertUpdateCommand.Transaction = base.DBConfig.StartNewTransaction());
				flag = Update(accountProductCategoryData, "Product_Category", insertUpdateCommand);
				if (!flag)
				{
					return flag;
				}
				object obj = accountProductCategoryData.ProductCategoryTable.Rows[0]["CategoryID"];
				UpdateTableRowByID("Product_Category", "CategoryID", "DateUpdated", obj, DateTime.Now, sqlTransaction);
				string entiyID = accountProductCategoryData.ProductCategoryTable.Rows[0]["CategoryName"].ToString();
				AddActivityLog("Product Category", entiyID, ActivityTypes.Update, sqlTransaction);
				UpdateTableRowInsertUpdateInfo("Product_Category", "CategoryID", obj, sqlTransaction, isInsert: false);
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

		public ProductCategoryData GetProductCategory()
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddTable("Product_Category");
			ProductCategoryData productCategoryData = new ProductCategoryData();
			sqlBuilder.UseDistinct = false;
			FillDataSet(productCategoryData, "Product_Category", sqlBuilder);
			return productCategoryData;
		}

		public bool DeleteProductCategory(string productCategoryID)
		{
			bool flag = true;
			try
			{
				string commandText = "DELETE FROM Product_Category WHERE CategoryID = '" + productCategoryID + "'";
				flag = Delete(commandText, null);
				if (!flag)
				{
					return flag;
				}
				AddActivityLog("Product Category", productCategoryID, ActivityTypes.Delete, null);
				return flag;
			}
			catch
			{
				throw;
			}
		}

		public ProductCategoryData GetProductCategoryByID(string id)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			CommandHelper commandHelper = null;
			commandHelper = new CommandHelper();
			commandHelper.FieldName = "CategoryID";
			commandHelper.SqlFieldType = SqlDbType.NVarChar;
			commandHelper.FieldValue = id;
			commandHelper.TableName = "Product_Category";
			sqlBuilder.AddCommandHelper(commandHelper);
			sqlBuilder.IsComparing = true;
			ProductCategoryData productCategoryData = new ProductCategoryData();
			sqlBuilder.UseDistinct = false;
			FillDataSet(productCategoryData, "Product_Category", sqlBuilder);
			return productCategoryData;
		}

		public DataSet GetProductCategoryByFields(params string[] columns)
		{
			return GetProductCategoryByFields(null, isInactive: true, columns);
		}

		public DataSet GetProductCategoryByFields(string[] productCategoryID, params string[] columns)
		{
			return GetProductCategoryByFields(productCategoryID, isInactive: true, columns);
		}

		public DataSet GetProductCategoryByFields(string[] ids, bool isInactive, params string[] columns)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddTable("Product_Category");
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
				commandHelper.TableName = "Product_Category";
				sqlBuilder.AddCommandHelper(commandHelper);
			}
			DataSet dataSet = new DataSet();
			dataSet.EnforceConstraints = false;
			sqlBuilder.UseDistinct = false;
			FillDataSet(dataSet, "Product_Category", sqlBuilder);
			return dataSet;
		}

		public DataSet GetProductCategoryList()
		{
			DataSet dataSet = new DataSet();
			string textCommand = "SELECT CategoryID [Category Code],CategoryName [Category Name],ParentCategoryID Parent,AgeGroup1 [Age Group 1],AgeGroup2 [Age Group 2],AgeGroup3 [Age Group 3], CommissionPercent\r\n                           FROM Product_Category ";
			FillDataSet(dataSet, "Product_Category", textCommand);
			return dataSet;
		}

		public DataSet GetProductCategoryComboList()
		{
			DataSet dataSet = new DataSet();
			string textCommand = "SELECT CategoryID [Code],CategoryName [Name]\r\n                           FROM Product_Category WHERE ISNULL(IsInactive,'False') = 'False' ORDER BY CategoryID,CategoryName";
			FillDataSet(dataSet, "Product_Category", textCommand);
			return dataSet;
		}
	}
}
