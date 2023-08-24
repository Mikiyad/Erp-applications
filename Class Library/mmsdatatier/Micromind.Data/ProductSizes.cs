using Micromind.Common;
using Micromind.Common.Data;
using Micromind.Common.Libraries;
using System;
using System.Data;
using System.Data.SqlClient;

namespace Micromind.Data
{
	public sealed class ProductSizes : StoreObject
	{
		private const string SIZEID_PARM = "@SizeID";

		private const string SIZENAME_PARM = "@SizeName";

		public const string ISINACTIVE_PARM = "@IsInactive";

		public const string PRODUCTSIZE_TABLE = "Product_Size";

		private const string CREATEDBY_PARM = "@CreatedBy";

		private const string DATECREATED_PARM = "@DateCreated";

		private const string UPDATEDBY_PARM = "@UpdatedBy";

		private const string DATEUPDATED_PARM = "@DateUpdated";

		public ProductSizes(Config config)
			: base(config)
		{
		}

		private string GetInsertUpdateText(bool isUpdate)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddInsertUpdateParameters("Product_Size", new FieldValue("SizeID", "@SizeID", isUpdateConditionField: true), new FieldValue("SizeName", "@SizeName"), new FieldValue("IsInactive", "@IsInactive"));
			if (isUpdate)
			{
				sqlBuilder.AddInsertUpdateParameters("Product_Size", new FieldValue("DateUpdated", "@DateUpdated", isUpdateConditionField: true, checkForNullValue: true));
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
			parameters.Add("@SizeID", SqlDbType.NVarChar);
			parameters.Add("@SizeName", SqlDbType.NVarChar);
			parameters.Add("@IsInactive", SqlDbType.Bit);
			parameters["@SizeID"].SourceColumn = "SizeID";
			parameters["@SizeName"].SourceColumn = "SizeName";
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

		public bool InsertProductSize(ProductSizeData accountProductSizeData)
		{
			bool result = true;
			SqlCommand insertUpdateCommand = GetInsertUpdateCommand(isUpdate: false);
			SqlTransaction sqlTransaction = null;
			try
			{
				sqlTransaction = (insertUpdateCommand.Transaction = base.DBConfig.StartNewTransaction());
				result = Insert(accountProductSizeData, "Product_Size", insertUpdateCommand);
				string text = accountProductSizeData.ProductSizeTable.Rows[0]["SizeID"].ToString();
				AddActivityLog("Product Size", text, ActivityTypes.Add, sqlTransaction);
				UpdateTableRowInsertUpdateInfo("Product_Size", "SizeID", text, sqlTransaction, isInsert: true);
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

		public bool UpdateProductSize(ProductSizeData accountProductSizeData)
		{
			bool flag = true;
			SqlCommand insertUpdateCommand = GetInsertUpdateCommand(isUpdate: true);
			SqlTransaction sqlTransaction = null;
			try
			{
				sqlTransaction = (insertUpdateCommand.Transaction = base.DBConfig.StartNewTransaction());
				flag = Update(accountProductSizeData, "Product_Size", insertUpdateCommand);
				if (!flag)
				{
					return flag;
				}
				object obj = accountProductSizeData.ProductSizeTable.Rows[0]["SizeID"];
				UpdateTableRowByID("Product_Size", "SizeID", "DateUpdated", obj, DateTime.Now, sqlTransaction);
				string entiyID = accountProductSizeData.ProductSizeTable.Rows[0]["SizeName"].ToString();
				AddActivityLog("Product Size", entiyID, ActivityTypes.Update, sqlTransaction);
				UpdateTableRowInsertUpdateInfo("Product_Size", "SizeID", obj, sqlTransaction, isInsert: false);
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

		public ProductSizeData GetProductSize()
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddTable("Product_Size");
			ProductSizeData productSizeData = new ProductSizeData();
			sqlBuilder.UseDistinct = false;
			FillDataSet(productSizeData, "Product_Size", sqlBuilder);
			return productSizeData;
		}

		public bool DeleteProductSize(string productSizeID)
		{
			bool flag = true;
			try
			{
				string commandText = "DELETE FROM Product_Size WHERE SizeID = '" + productSizeID + "'";
				flag = Delete(commandText, null);
				if (!flag)
				{
					return flag;
				}
				AddActivityLog("Product Size", productSizeID, ActivityTypes.Delete, null);
				return flag;
			}
			catch
			{
				throw;
			}
		}

		public ProductSizeData GetProductSizeByID(string id)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			CommandHelper commandHelper = null;
			commandHelper = new CommandHelper();
			commandHelper.FieldName = "SizeID";
			commandHelper.SqlFieldType = SqlDbType.NVarChar;
			commandHelper.FieldValue = id;
			commandHelper.TableName = "Product_Size";
			sqlBuilder.AddCommandHelper(commandHelper);
			sqlBuilder.IsComparing = true;
			ProductSizeData productSizeData = new ProductSizeData();
			sqlBuilder.UseDistinct = false;
			FillDataSet(productSizeData, "Product_Size", sqlBuilder);
			return productSizeData;
		}

		public DataSet GetProductSizeByFields(params string[] columns)
		{
			return GetProductSizeByFields(null, isInactive: true, columns);
		}

		public DataSet GetProductSizeByFields(string[] productSizeID, params string[] columns)
		{
			return GetProductSizeByFields(productSizeID, isInactive: true, columns);
		}

		public DataSet GetProductSizeByFields(string[] ids, bool isInactive, params string[] columns)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddTable("Product_Size");
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
				commandHelper.FieldName = "SizeID";
				commandHelper.FieldValue = ids;
				commandHelper.SqlFieldType = SqlDbType.NVarChar;
				commandHelper.TableName = "Product_Size";
				sqlBuilder.AddCommandHelper(commandHelper);
			}
			DataSet dataSet = new DataSet();
			dataSet.EnforceConstraints = false;
			sqlBuilder.UseDistinct = false;
			FillDataSet(dataSet, "Product_Size", sqlBuilder);
			return dataSet;
		}

		public DataSet GetProductSizeList()
		{
			DataSet dataSet = new DataSet();
			string textCommand = "SELECT SizeID [Size Code],SizeName [Size Name],Note, IsInactive [Inactive]\r\n                           FROM Product_Size ";
			FillDataSet(dataSet, "Product_Size", textCommand);
			return dataSet;
		}

		public DataSet GetProductSizeComboList()
		{
			DataSet dataSet = new DataSet();
			string textCommand = "SELECT SizeID [Code],SizeName [Name]\r\n                           FROM Product_Size ORDER BY SizeID,SizeName";
			FillDataSet(dataSet, "Product_Size", textCommand);
			return dataSet;
		}
	}
}
