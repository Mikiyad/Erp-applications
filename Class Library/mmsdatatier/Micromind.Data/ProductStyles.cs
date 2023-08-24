using Micromind.Common;
using Micromind.Common.Data;
using Micromind.Common.Libraries;
using System;
using System.Data;
using System.Data.SqlClient;

namespace Micromind.Data
{
	public sealed class ProductStyles : StoreObject
	{
		private const string STYLEID_PARM = "@StyleID";

		private const string STYLENAME_PARM = "@StyleName";

		public const string ISINACTIVE_PARM = "@IsInactive";

		public const string PRODUCTSTYLE_TABLE = "Product_Style";

		private const string CREATEDBY_PARM = "@CreatedBy";

		private const string DATECREATED_PARM = "@DateCreated";

		private const string UPDATEDBY_PARM = "@UpdatedBy";

		private const string DATEUPDATED_PARM = "@DateUpdated";

		public ProductStyles(Config config)
			: base(config)
		{
		}

		private string GetInsertUpdateText(bool isUpdate)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddInsertUpdateParameters("Product_Style", new FieldValue("StyleID", "@StyleID", isUpdateConditionField: true), new FieldValue("StyleName", "@StyleName"), new FieldValue("IsInactive", "@IsInactive"));
			if (isUpdate)
			{
				sqlBuilder.AddInsertUpdateParameters("Product_Style", new FieldValue("DateUpdated", "@DateUpdated", isUpdateConditionField: true, checkForNullValue: true));
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
			parameters.Add("@StyleID", SqlDbType.NVarChar);
			parameters.Add("@StyleName", SqlDbType.NVarChar);
			parameters.Add("@IsInactive", SqlDbType.Bit);
			parameters["@StyleID"].SourceColumn = "StyleID";
			parameters["@StyleName"].SourceColumn = "StyleName";
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

		public bool InsertProductStyle(ProductStyleData accountProductStyleData)
		{
			bool result = true;
			SqlCommand insertUpdateCommand = GetInsertUpdateCommand(isUpdate: false);
			SqlTransaction sqlTransaction = null;
			try
			{
				sqlTransaction = (insertUpdateCommand.Transaction = base.DBConfig.StartNewTransaction());
				result = Insert(accountProductStyleData, "Product_Style", insertUpdateCommand);
				string text = accountProductStyleData.ProductStyleTable.Rows[0]["StyleID"].ToString();
				AddActivityLog("Product Style", text, ActivityTypes.Add, sqlTransaction);
				UpdateTableRowInsertUpdateInfo("Product_Style", "StyleID", text, sqlTransaction, isInsert: true);
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

		public bool UpdateProductStyle(ProductStyleData accountProductStyleData)
		{
			bool flag = true;
			SqlCommand insertUpdateCommand = GetInsertUpdateCommand(isUpdate: true);
			SqlTransaction sqlTransaction = null;
			try
			{
				sqlTransaction = (insertUpdateCommand.Transaction = base.DBConfig.StartNewTransaction());
				flag = Update(accountProductStyleData, "Product_Style", insertUpdateCommand);
				if (!flag)
				{
					return flag;
				}
				object obj = accountProductStyleData.ProductStyleTable.Rows[0]["StyleID"];
				UpdateTableRowByID("Product_Style", "StyleID", "DateUpdated", obj, DateTime.Now, sqlTransaction);
				string entiyID = accountProductStyleData.ProductStyleTable.Rows[0]["StyleName"].ToString();
				AddActivityLog("Product Style", entiyID, ActivityTypes.Update, sqlTransaction);
				UpdateTableRowInsertUpdateInfo("Product_Style", "StyleID", obj, sqlTransaction, isInsert: false);
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

		public ProductStyleData GetProductStyle()
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddTable("Product_Style");
			ProductStyleData productStyleData = new ProductStyleData();
			sqlBuilder.UseDistinct = false;
			FillDataSet(productStyleData, "Product_Style", sqlBuilder);
			return productStyleData;
		}

		public bool DeleteProductStyle(string productStyleID)
		{
			bool flag = true;
			try
			{
				string commandText = "DELETE FROM Product_Style WHERE StyleID = '" + productStyleID + "'";
				flag = Delete(commandText, null);
				if (!flag)
				{
					return flag;
				}
				AddActivityLog("Product Style", productStyleID, ActivityTypes.Delete, null);
				return flag;
			}
			catch
			{
				throw;
			}
		}

		public ProductStyleData GetProductStyleByID(string id)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			CommandHelper commandHelper = null;
			commandHelper = new CommandHelper();
			commandHelper.FieldName = "StyleID";
			commandHelper.SqlFieldType = SqlDbType.NVarChar;
			commandHelper.FieldValue = id;
			commandHelper.TableName = "Product_Style";
			sqlBuilder.AddCommandHelper(commandHelper);
			sqlBuilder.IsComparing = true;
			ProductStyleData productStyleData = new ProductStyleData();
			sqlBuilder.UseDistinct = false;
			FillDataSet(productStyleData, "Product_Style", sqlBuilder);
			return productStyleData;
		}

		public DataSet GetProductStyleByFields(params string[] columns)
		{
			return GetProductStyleByFields(null, isInactive: true, columns);
		}

		public DataSet GetProductStyleByFields(string[] productStyleID, params string[] columns)
		{
			return GetProductStyleByFields(productStyleID, isInactive: true, columns);
		}

		public DataSet GetProductStyleByFields(string[] ids, bool isInactive, params string[] columns)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddTable("Product_Style");
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
				commandHelper.FieldName = "StyleID";
				commandHelper.FieldValue = ids;
				commandHelper.SqlFieldType = SqlDbType.NVarChar;
				commandHelper.TableName = "Product_Style";
				sqlBuilder.AddCommandHelper(commandHelper);
			}
			DataSet dataSet = new DataSet();
			dataSet.EnforceConstraints = false;
			sqlBuilder.UseDistinct = false;
			FillDataSet(dataSet, "Product_Style", sqlBuilder);
			return dataSet;
		}

		public DataSet GetProductStyleList()
		{
			DataSet dataSet = new DataSet();
			string textCommand = "SELECT StyleID [Style Code],StyleName [Style Name], IsInactive [Inactive]\r\n                           FROM Product_Style ";
			FillDataSet(dataSet, "Product_Style", textCommand);
			return dataSet;
		}

		public DataSet GetProductStyleComboList()
		{
			DataSet dataSet = new DataSet();
			string textCommand = "SELECT StyleID [Code],StyleName [Name]\r\n                           FROM Product_Style ORDER BY StyleID,StyleName";
			FillDataSet(dataSet, "Product_Style", textCommand);
			return dataSet;
		}
	}
}
