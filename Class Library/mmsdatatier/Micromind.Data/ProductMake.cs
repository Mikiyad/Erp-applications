using Micromind.Common;
using Micromind.Common.Data;
using Micromind.Common.Libraries;
using System;
using System.Data;
using System.Data.SqlClient;

namespace Micromind.Data
{
	public sealed class ProductMake : StoreObject
	{
		private const string MAKEID_PARM = "@MakeID";

		private const string MAKENAME_PARM = "@MakeName";

		public const string NOTE_PARM = "@Note";

		public const string ISINACTIVE_PARM = "@Inactive";

		public const string PRODUCTMAKE_TABLE = "Product_Make";

		private const string CREATEDBY_PARM = "@CreatedBy";

		private const string DATECREATED_PARM = "@DateCreated";

		private const string UPDATEDBY_PARM = "@UpdatedBy";

		private const string DATEUPDATED_PARM = "@DateUpdated";

		public ProductMake(Config config)
			: base(config)
		{
		}

		private string GetInsertUpdateText(bool isUpdate)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddInsertUpdateParameters("Product_Make", new FieldValue("MakeID", "@MakeID", isUpdateConditionField: true), new FieldValue("MakeName", "@MakeName"), new FieldValue("IsInactive", "@Inactive"), new FieldValue("Note", "@Note"));
			if (isUpdate)
			{
				sqlBuilder.AddInsertUpdateParameters("Product_Make", new FieldValue("DateUpdated", "@DateUpdated", isUpdateConditionField: true, checkForNullValue: true));
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
			parameters.Add("@MakeID", SqlDbType.NVarChar);
			parameters.Add("@MakeName", SqlDbType.NVarChar);
			parameters.Add("@Inactive", SqlDbType.Bit);
			parameters.Add("@Note", SqlDbType.NVarChar);
			parameters["@MakeID"].SourceColumn = "MakeID";
			parameters["@MakeName"].SourceColumn = "MakeName";
			parameters["@Inactive"].SourceColumn = "IsInactive";
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

		public bool InsertProductMake(ProductMakeData accountMakeData)
		{
			bool result = true;
			SqlCommand insertUpdateCommand = GetInsertUpdateCommand(isUpdate: false);
			SqlTransaction sqlTransaction = null;
			try
			{
				sqlTransaction = (insertUpdateCommand.Transaction = base.DBConfig.StartNewTransaction());
				result = Insert(accountMakeData, "Product_Make", insertUpdateCommand);
				string text = accountMakeData.ProductMakeTable.Rows[0]["MakeID"].ToString();
				AddActivityLog("Make", text, ActivityTypes.Add, sqlTransaction);
				UpdateTableRowInsertUpdateInfo("Product_Make", "MakeID", text, sqlTransaction, isInsert: true);
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

		public bool UpdateProductMake(ProductMakeData accountMakeData)
		{
			bool flag = true;
			SqlCommand insertUpdateCommand = GetInsertUpdateCommand(isUpdate: true);
			SqlTransaction sqlTransaction = null;
			try
			{
				sqlTransaction = (insertUpdateCommand.Transaction = base.DBConfig.StartNewTransaction());
				flag = Update(accountMakeData, "Product_Make", insertUpdateCommand);
				if (!flag)
				{
					return flag;
				}
				object obj = accountMakeData.ProductMakeTable.Rows[0]["MakeID"];
				UpdateTableRowByID("Product_Make", "MakeID", "DateUpdated", obj, DateTime.Now, sqlTransaction);
				string entiyID = accountMakeData.ProductMakeTable.Rows[0]["MakeName"].ToString();
				AddActivityLog("Product Make", entiyID, ActivityTypes.Update, sqlTransaction);
				UpdateTableRowInsertUpdateInfo("Product_Make", "MakeName", obj, sqlTransaction, isInsert: false);
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

		public ProductMakeData GetProductMake()
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddTable("Product_Make");
			ProductMakeData productMakeData = new ProductMakeData();
			sqlBuilder.UseDistinct = false;
			FillDataSet(productMakeData, "Product_Make", sqlBuilder);
			return productMakeData;
		}

		public bool DeleteProductMake(string productMakeID)
		{
			bool flag = true;
			try
			{
				string commandText = "DELETE FROM Product_Make WHERE MakeID = '" + productMakeID + "'";
				flag = Delete(commandText, null);
				if (!flag)
				{
					return flag;
				}
				AddActivityLog("Product Make", productMakeID, ActivityTypes.Delete, null);
				return flag;
			}
			catch
			{
				throw;
			}
		}

		public ProductMakeData GetProductMakeByID(string id)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			CommandHelper commandHelper = null;
			commandHelper = new CommandHelper();
			commandHelper.FieldName = "MakeID";
			commandHelper.SqlFieldType = SqlDbType.NVarChar;
			commandHelper.FieldValue = id;
			commandHelper.TableName = "Product_Make";
			sqlBuilder.AddCommandHelper(commandHelper);
			sqlBuilder.IsComparing = true;
			ProductMakeData productMakeData = new ProductMakeData();
			sqlBuilder.UseDistinct = false;
			FillDataSet(productMakeData, "Product_Make", sqlBuilder);
			return productMakeData;
		}

		public DataSet GetMakeByFields(params string[] columns)
		{
			return GetMakeByFields(null, isInactive: true, columns);
		}

		public DataSet GetMakeByFields(string[] productMakeID, params string[] columns)
		{
			return GetMakeByFields(productMakeID, isInactive: true, columns);
		}

		public DataSet GetMakeByFields(string[] ids, bool isInactive, params string[] columns)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddTable("Product_Make");
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
				commandHelper.FieldName = "MakeID";
				commandHelper.FieldValue = ids;
				commandHelper.SqlFieldType = SqlDbType.NVarChar;
				commandHelper.TableName = "Product_Make";
				sqlBuilder.AddCommandHelper(commandHelper);
			}
			DataSet dataSet = new DataSet();
			dataSet.EnforceConstraints = false;
			sqlBuilder.UseDistinct = false;
			FillDataSet(dataSet, "Product_Make", sqlBuilder);
			return dataSet;
		}

		public DataSet GetProductMakeList()
		{
			DataSet dataSet = new DataSet();
			string textCommand = "SELECT MakeID [Make Code],MakeName [Make Name],Note,IsInactive AS [Inactive]\r\n                           FROM Product_Make ";
			FillDataSet(dataSet, "Product_Make", textCommand);
			return dataSet;
		}

		public DataSet GetProductMakeComboList()
		{
			DataSet dataSet = new DataSet();
			string textCommand = "SELECT MakeID [Code],MakeName [Name]\r\n                           FROM Product_Make ORDER BY MakeID,MakeName";
			FillDataSet(dataSet, "Product_Make", textCommand);
			return dataSet;
		}
	}
}
