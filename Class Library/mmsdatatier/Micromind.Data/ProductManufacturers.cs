using Micromind.Common;
using Micromind.Common.Data;
using Micromind.Common.Libraries;
using System;
using System.Data;
using System.Data.SqlClient;

namespace Micromind.Data
{
	public sealed class ProductManufacturers : StoreObject
	{
		private const string MANUFACTURERID_PARM = "@ManufacturerID";

		private const string MANUFACTURERNAME_PARM = "@ManufacturerName";

		public const string ISINACTIVE_PARM = "@IsInactive";

		public const string NOTE_PARM = "@Note";

		public const string PRODUCTMANUFACTURER_TABLE = "Product_Manufacturer";

		private const string CREATEDBY_PARM = "@CreatedBy";

		private const string DATECREATED_PARM = "@DateCreated";

		private const string UPDATEDBY_PARM = "@UpdatedBy";

		private const string DATEUPDATED_PARM = "@DateUpdated";

		public ProductManufacturers(Config config)
			: base(config)
		{
		}

		private string GetInsertUpdateText(bool isUpdate)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddInsertUpdateParameters("Product_Manufacturer", new FieldValue("ManufacturerID", "@ManufacturerID", isUpdateConditionField: true), new FieldValue("ManufacturerName", "@ManufacturerName"), new FieldValue("IsInactive", "@IsInactive"), new FieldValue("Note", "@Note"));
			if (isUpdate)
			{
				sqlBuilder.AddInsertUpdateParameters("Product_Manufacturer", new FieldValue("DateUpdated", "@DateUpdated", isUpdateConditionField: true, checkForNullValue: true));
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
			parameters.Add("@ManufacturerID", SqlDbType.NVarChar);
			parameters.Add("@ManufacturerName", SqlDbType.NVarChar);
			parameters.Add("@IsInactive", SqlDbType.Bit);
			parameters.Add("@Note", SqlDbType.NVarChar);
			parameters["@ManufacturerID"].SourceColumn = "ManufacturerID";
			parameters["@ManufacturerName"].SourceColumn = "ManufacturerName";
			parameters["@IsInactive"].SourceColumn = "IsInactive";
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

		public bool InsertProductManufacturer(ProductManufacturerData accountProductManufacturerData)
		{
			bool result = true;
			SqlCommand insertUpdateCommand = GetInsertUpdateCommand(isUpdate: false);
			SqlTransaction sqlTransaction = null;
			try
			{
				sqlTransaction = (insertUpdateCommand.Transaction = base.DBConfig.StartNewTransaction());
				result = Insert(accountProductManufacturerData, "Product_Manufacturer", insertUpdateCommand);
				string text = accountProductManufacturerData.ProductManufacturerTable.Rows[0]["ManufacturerID"].ToString();
				AddActivityLog("Product Manufacturer", text, ActivityTypes.Add, sqlTransaction);
				UpdateTableRowInsertUpdateInfo("Product_Manufacturer", "ManufacturerID", text, sqlTransaction, isInsert: true);
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

		public bool UpdateProductManufacturer(ProductManufacturerData accountProductManufacturerData)
		{
			bool flag = true;
			SqlCommand insertUpdateCommand = GetInsertUpdateCommand(isUpdate: true);
			SqlTransaction sqlTransaction = null;
			try
			{
				sqlTransaction = (insertUpdateCommand.Transaction = base.DBConfig.StartNewTransaction());
				flag = Update(accountProductManufacturerData, "Product_Manufacturer", insertUpdateCommand);
				if (!flag)
				{
					return flag;
				}
				object obj = accountProductManufacturerData.ProductManufacturerTable.Rows[0]["ManufacturerID"];
				UpdateTableRowByID("Product_Manufacturer", "ManufacturerID", "DateUpdated", obj, DateTime.Now, sqlTransaction);
				string entiyID = accountProductManufacturerData.ProductManufacturerTable.Rows[0]["ManufacturerName"].ToString();
				AddActivityLog("Product Manufacturer", entiyID, ActivityTypes.Update, sqlTransaction);
				UpdateTableRowInsertUpdateInfo("Product_Manufacturer", "ManufacturerID", obj, sqlTransaction, isInsert: false);
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

		public ProductManufacturerData GetProductManufacturer()
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddTable("Product_Manufacturer");
			ProductManufacturerData productManufacturerData = new ProductManufacturerData();
			sqlBuilder.UseDistinct = false;
			FillDataSet(productManufacturerData, "Product_Manufacturer", sqlBuilder);
			return productManufacturerData;
		}

		public bool DeleteProductManufacturer(string productManufacturerID)
		{
			bool flag = true;
			try
			{
				string commandText = "DELETE FROM Product_Manufacturer WHERE ManufacturerID = '" + productManufacturerID + "'";
				flag = Delete(commandText, null);
				if (!flag)
				{
					return flag;
				}
				AddActivityLog("Product Manufacturer", productManufacturerID, ActivityTypes.Delete, null);
				return flag;
			}
			catch
			{
				throw;
			}
		}

		public ProductManufacturerData GetProductManufacturerByID(string id)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			CommandHelper commandHelper = null;
			commandHelper = new CommandHelper();
			commandHelper.FieldName = "ManufacturerID";
			commandHelper.SqlFieldType = SqlDbType.NVarChar;
			commandHelper.FieldValue = id;
			commandHelper.TableName = "Product_Manufacturer";
			sqlBuilder.AddCommandHelper(commandHelper);
			sqlBuilder.IsComparing = true;
			ProductManufacturerData productManufacturerData = new ProductManufacturerData();
			sqlBuilder.UseDistinct = false;
			FillDataSet(productManufacturerData, "Product_Manufacturer", sqlBuilder);
			return productManufacturerData;
		}

		public DataSet GetProductManufacturerByFields(params string[] columns)
		{
			return GetProductManufacturerByFields(null, isInactive: true, columns);
		}

		public DataSet GetProductManufacturerByFields(string[] productManufacturerID, params string[] columns)
		{
			return GetProductManufacturerByFields(productManufacturerID, isInactive: true, columns);
		}

		public DataSet GetProductManufacturerByFields(string[] ids, bool isInactive, params string[] columns)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddTable("Product_Manufacturer");
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
				commandHelper.FieldName = "ManufacturerID";
				commandHelper.FieldValue = ids;
				commandHelper.SqlFieldType = SqlDbType.NVarChar;
				commandHelper.TableName = "Product_Manufacturer";
				sqlBuilder.AddCommandHelper(commandHelper);
			}
			DataSet dataSet = new DataSet();
			dataSet.EnforceConstraints = false;
			sqlBuilder.UseDistinct = false;
			FillDataSet(dataSet, "Product_Manufacturer", sqlBuilder);
			return dataSet;
		}

		public DataSet GetProductManufacturerList()
		{
			DataSet dataSet = new DataSet();
			string textCommand = "SELECT ManufacturerID [Manufacturer Code],ManufacturerName [Manufacturer Name],Note,IsInactive [Inactive]\r\n                           FROM Product_Manufacturer ";
			FillDataSet(dataSet, "Product_Manufacturer", textCommand);
			return dataSet;
		}

		public DataSet GetProductManufacturerComboList()
		{
			DataSet dataSet = new DataSet();
			string textCommand = "SELECT ManufacturerID [Code],ManufacturerName [Name]\r\n                           FROM Product_Manufacturer ORDER BY ManufacturerID,ManufacturerName";
			FillDataSet(dataSet, "Product_Manufacturer", textCommand);
			return dataSet;
		}
	}
}
