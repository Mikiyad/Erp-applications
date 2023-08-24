using Micromind.Common;
using Micromind.Common.Data;
using Micromind.Common.Libraries;
using System;
using System.Data;
using System.Data.SqlClient;

namespace Micromind.Data
{
	public sealed class ProductSpecification : StoreObject
	{
		private const string SPECIFICATIONID_PARM = "@SpecificationID";

		private const string SPECIFICATIONNAME_PARM = "@SpecificationName";

		public const string ISINACTIVE_PARM = "@IsInactive";

		public const string PRODUCTSPECIFICATION_TABLE = "Product_Specification";

		private const string CREATEDBY_PARM = "@CreatedBy";

		private const string DATECREATED_PARM = "@DateCreated";

		private const string UPDATEDBY_PARM = "@UpdatedBy";

		private const string DATEUPDATED_PARM = "@DateUpdated";

		public ProductSpecification(Config config)
			: base(config)
		{
		}

		private string GetInsertUpdateText(bool isUpdate)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddInsertUpdateParameters("Product_Specification", new FieldValue("SpecificationID", "@SpecificationID", isUpdateConditionField: true), new FieldValue("SpecificationName", "@SpecificationName"), new FieldValue("IsInactive", "@IsInactive"));
			if (isUpdate)
			{
				sqlBuilder.AddInsertUpdateParameters("Product_Specification", new FieldValue("DateUpdated", "@DateUpdated", isUpdateConditionField: true, checkForNullValue: true));
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
			parameters.Add("@SpecificationID", SqlDbType.NVarChar);
			parameters.Add("@SpecificationName", SqlDbType.NVarChar);
			parameters.Add("@IsInactive", SqlDbType.Bit);
			parameters["@SpecificationID"].SourceColumn = "SpecificationID";
			parameters["@SpecificationName"].SourceColumn = "SpecificationName";
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

		public bool InsertProductSpecification(ProductSpecificationData accountProductSpecificationData)
		{
			bool result = true;
			SqlCommand insertUpdateCommand = GetInsertUpdateCommand(isUpdate: false);
			SqlTransaction sqlTransaction = null;
			try
			{
				sqlTransaction = (insertUpdateCommand.Transaction = base.DBConfig.StartNewTransaction());
				result = Insert(accountProductSpecificationData, "Product_Specification", insertUpdateCommand);
				string text = accountProductSpecificationData.ProductSpecificationTable.Rows[0]["SpecificationID"].ToString();
				AddActivityLog("Product Specification", text, ActivityTypes.Add, sqlTransaction);
				UpdateTableRowInsertUpdateInfo("Product_Specification", "SpecificationID", text, sqlTransaction, isInsert: true);
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

		public bool UpdateProductSpecification(ProductSpecificationData accountProductSpecificationData)
		{
			bool flag = true;
			SqlCommand insertUpdateCommand = GetInsertUpdateCommand(isUpdate: true);
			SqlTransaction sqlTransaction = null;
			try
			{
				sqlTransaction = (insertUpdateCommand.Transaction = base.DBConfig.StartNewTransaction());
				flag = Update(accountProductSpecificationData, "Product_Specification", insertUpdateCommand);
				if (!flag)
				{
					return flag;
				}
				object obj = accountProductSpecificationData.ProductSpecificationTable.Rows[0]["SpecificationID"];
				UpdateTableRowByID("Product_Specification", "SpecificationID", "DateUpdated", obj, DateTime.Now, sqlTransaction);
				string entiyID = accountProductSpecificationData.ProductSpecificationTable.Rows[0]["SpecificationName"].ToString();
				AddActivityLog("Product Specification", entiyID, ActivityTypes.Update, sqlTransaction);
				UpdateTableRowInsertUpdateInfo("Product_Specification", "SpecificationID", obj, sqlTransaction, isInsert: false);
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

		public ProductSpecificationData GetProductSpecification()
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddTable("Product_Specification");
			ProductSpecificationData productSpecificationData = new ProductSpecificationData();
			sqlBuilder.UseDistinct = false;
			FillDataSet(productSpecificationData, "Product_Specification", sqlBuilder);
			return productSpecificationData;
		}

		public bool DeleteProductSpecification(string productSpecificationID)
		{
			bool flag = true;
			try
			{
				string commandText = "DELETE FROM Product_Specification WHERE SpecificationID = '" + productSpecificationID + "'";
				flag = Delete(commandText, null);
				if (!flag)
				{
					return flag;
				}
				AddActivityLog("Product Specification", productSpecificationID, ActivityTypes.Delete, null);
				return flag;
			}
			catch
			{
				throw;
			}
		}

		public ProductSpecificationData GetProductSpecificationByID(string id)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			CommandHelper commandHelper = null;
			commandHelper = new CommandHelper();
			commandHelper.FieldName = "SpecificationID";
			commandHelper.SqlFieldType = SqlDbType.NVarChar;
			commandHelper.FieldValue = id;
			commandHelper.TableName = "Product_Specification";
			sqlBuilder.AddCommandHelper(commandHelper);
			sqlBuilder.IsComparing = true;
			ProductSpecificationData productSpecificationData = new ProductSpecificationData();
			sqlBuilder.UseDistinct = false;
			FillDataSet(productSpecificationData, "Product_Specification", sqlBuilder);
			return productSpecificationData;
		}

		public DataSet GetProductSpecificationByFields(params string[] columns)
		{
			return GetProductSpecificationByFields(null, isInactive: true, columns);
		}

		public DataSet GetProductSpecificationByFields(string[] productSpecificationID, params string[] columns)
		{
			return GetProductSpecificationByFields(productSpecificationID, isInactive: true, columns);
		}

		public DataSet GetProductSpecificationByFields(string[] ids, bool isInactive, params string[] columns)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddTable("Product_Specification");
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
				commandHelper.FieldName = "SpecificationID";
				commandHelper.FieldValue = ids;
				commandHelper.SqlFieldType = SqlDbType.NVarChar;
				commandHelper.TableName = "Product_Specification";
				sqlBuilder.AddCommandHelper(commandHelper);
			}
			DataSet dataSet = new DataSet();
			dataSet.EnforceConstraints = false;
			sqlBuilder.UseDistinct = false;
			FillDataSet(dataSet, "Product_Specification", sqlBuilder);
			return dataSet;
		}

		public DataSet GetProductSpecificationList()
		{
			DataSet dataSet = new DataSet();
			string textCommand = "SELECT SpecificationID [Specification Code],SpecificationName [Specification Name], IsInactive [Inactive]\r\n                           FROM Product_Specification ";
			FillDataSet(dataSet, "Product_Specification", textCommand);
			return dataSet;
		}

		public DataSet GetProductSpecificationComboList()
		{
			DataSet dataSet = new DataSet();
			string textCommand = "SELECT SpecificationID [Code],SpecificationName [Name]\r\n                           FROM Product_Specification ORDER BY SpecificationID,SpecificationName";
			FillDataSet(dataSet, "Product_Specification", textCommand);
			return dataSet;
		}
	}
}
