using Micromind.Common;
using Micromind.Common.Data;
using Micromind.Common.Libraries;
using System;
using System.Data;
using System.Data.SqlClient;

namespace Micromind.Data
{
	public sealed class ProductModel : StoreObject
	{
		private const string MODELID_PARM = "@ModelID";

		private const string MODELNAME_PARM = "@ModelName";

		private const string TYPEID_PARM = "@TypeID";

		public const string NOTE_PARM = "@Note";

		public const string ISINACTIVE_PARM = "@Inactive";

		public const string PRODUCTMODEL_TABLE = "Product_Model";

		private const string CREATEDBY_PARM = "@CreatedBy";

		private const string DATECREATED_PARM = "@DateCreated";

		private const string UPDATEDBY_PARM = "@UpdatedBy";

		private const string DATEUPDATED_PARM = "@DateUpdated";

		public ProductModel(Config config)
			: base(config)
		{
		}

		private string GetInsertUpdateText(bool isUpdate)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddInsertUpdateParameters("Product_Model", new FieldValue("ModelID", "@ModelID", isUpdateConditionField: true), new FieldValue("ModelName", "@ModelName"), new FieldValue("TypeID", "@TypeID"), new FieldValue("IsInactive", "@Inactive"), new FieldValue("Note", "@Note"));
			if (isUpdate)
			{
				sqlBuilder.AddInsertUpdateParameters("Product_Model", new FieldValue("DateUpdated", "@DateUpdated", isUpdateConditionField: true, checkForNullValue: true));
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
			parameters.Add("@ModelID", SqlDbType.NVarChar);
			parameters.Add("@ModelName", SqlDbType.NVarChar);
			parameters.Add("@TypeID", SqlDbType.NVarChar);
			parameters.Add("@Inactive", SqlDbType.Bit);
			parameters.Add("@Note", SqlDbType.NVarChar);
			parameters["@ModelID"].SourceColumn = "ModelID";
			parameters["@ModelName"].SourceColumn = "ModelName";
			parameters["@TypeID"].SourceColumn = "TypeID";
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

		public bool InsertProductModel(ProductModelData accountModelData)
		{
			bool result = true;
			SqlCommand insertUpdateCommand = GetInsertUpdateCommand(isUpdate: false);
			SqlTransaction sqlTransaction = null;
			try
			{
				sqlTransaction = (insertUpdateCommand.Transaction = base.DBConfig.StartNewTransaction());
				result = Insert(accountModelData, "Product_Model", insertUpdateCommand);
				string text = accountModelData.ProductModelTable.Rows[0]["ModelID"].ToString();
				AddActivityLog("Model", text, ActivityTypes.Add, sqlTransaction);
				UpdateTableRowInsertUpdateInfo("Product_Model", "ModelID", text, sqlTransaction, isInsert: true);
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

		public bool UpdateProductModel(ProductModelData accountModelData)
		{
			bool flag = true;
			SqlCommand insertUpdateCommand = GetInsertUpdateCommand(isUpdate: true);
			SqlTransaction sqlTransaction = null;
			try
			{
				sqlTransaction = (insertUpdateCommand.Transaction = base.DBConfig.StartNewTransaction());
				flag = Update(accountModelData, "Product_Model", insertUpdateCommand);
				if (!flag)
				{
					return flag;
				}
				object obj = accountModelData.ProductModelTable.Rows[0]["ModelID"];
				UpdateTableRowByID("Product_Model", "ModelID", "DateUpdated", obj, DateTime.Now, sqlTransaction);
				string entiyID = accountModelData.ProductModelTable.Rows[0]["ModelName"].ToString();
				AddActivityLog("Product Model", entiyID, ActivityTypes.Update, sqlTransaction);
				UpdateTableRowInsertUpdateInfo("Product_Model", "ModelName", obj, sqlTransaction, isInsert: false);
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

		public ProductModelData GetProductModel()
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddTable("Product_Model");
			ProductModelData productModelData = new ProductModelData();
			sqlBuilder.UseDistinct = false;
			FillDataSet(productModelData, "Product_Model", sqlBuilder);
			return productModelData;
		}

		public bool DeleteProductModel(string productModelID)
		{
			bool flag = true;
			try
			{
				string commandText = "DELETE FROM Product_Model WHERE ModelID = '" + productModelID + "'";
				flag = Delete(commandText, null);
				if (!flag)
				{
					return flag;
				}
				AddActivityLog("Product Model", productModelID, ActivityTypes.Delete, null);
				return flag;
			}
			catch
			{
				throw;
			}
		}

		public ProductModelData GetProductModelByID(string id)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			CommandHelper commandHelper = null;
			commandHelper = new CommandHelper();
			commandHelper.FieldName = "ModelID";
			commandHelper.SqlFieldType = SqlDbType.NVarChar;
			commandHelper.FieldValue = id;
			commandHelper.TableName = "Product_Model";
			sqlBuilder.AddCommandHelper(commandHelper);
			sqlBuilder.IsComparing = true;
			ProductModelData productModelData = new ProductModelData();
			sqlBuilder.UseDistinct = false;
			FillDataSet(productModelData, "Product_Model", sqlBuilder);
			return productModelData;
		}

		public DataSet GetModelByFields(params string[] columns)
		{
			return GetModelByFields(null, isInactive: true, columns);
		}

		public DataSet GetModelByFields(string[] productModelID, params string[] columns)
		{
			return GetModelByFields(productModelID, isInactive: true, columns);
		}

		public DataSet GetModelByFields(string[] ids, bool isInactive, params string[] columns)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddTable("Product_Model");
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
				commandHelper.FieldName = "ModelID";
				commandHelper.FieldValue = ids;
				commandHelper.SqlFieldType = SqlDbType.NVarChar;
				commandHelper.TableName = "Product_Model";
				sqlBuilder.AddCommandHelper(commandHelper);
			}
			DataSet dataSet = new DataSet();
			dataSet.EnforceConstraints = false;
			sqlBuilder.UseDistinct = false;
			FillDataSet(dataSet, "Product_Model", sqlBuilder);
			return dataSet;
		}

		public DataSet GetProductModelList()
		{
			DataSet dataSet = new DataSet();
			string textCommand = "SELECT ModelID [Model Code],ModelName [Model Name],Note,IsInactive AS [Inactive]\r\n                           FROM Product_Model ";
			FillDataSet(dataSet, "Product_Model", textCommand);
			return dataSet;
		}

		public DataSet GetProductModelComboList()
		{
			DataSet dataSet = new DataSet();
			string textCommand = "SELECT ModelID [Code],ModelName [Name]\r\n                           FROM Product_Model ORDER BY ModelID,ModelName";
			FillDataSet(dataSet, "Product_Model", textCommand);
			return dataSet;
		}
	}
}
