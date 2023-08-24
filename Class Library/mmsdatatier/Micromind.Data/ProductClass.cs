using Micromind.Common;
using Micromind.Common.Data;
using Micromind.Common.Libraries;
using System;
using System.Data;
using System.Data.SqlClient;

namespace Micromind.Data
{
	public sealed class ProductClass : StoreObject
	{
		private const string PRODUCTCLASS_TABLE = "@Product_Class";

		private const string PRODUCTCLASSID_PARM = "@ClassID";

		private const string PRODUCTCLASSNAME_PARM = "@ClassName";

		private const string ITEMTYPE_PARM = "@ItemType";

		private const string COSTMETHOD_PARM = "@CostMethod";

		private const string CATEGORYID_PARM = "@CategoryID";

		private const string ISINACTIVE_PARM = "@IsInactive";

		private const string UNITID_PARM = "@UnitID";

		private const string DEFAULTLOCATIONID_PARM = "@DefaultLocationID";

		private const string COGSACCOUNT_PARM = "@COGSAccount";

		private const string ASSETACCOUNT_PARM = "@AssetAccount";

		private const string INCOMEACCOUNT_PARM = "@IncomeAccount";

		private const string DIVISIONID_PARM = "@DivisionID";

		public const string TAXOPTION_PARM = "@TaxOption";

		public const string TAXGROUPID_PARM = "@TaxGroupID";

		private const string TAXPRODUCTCLASSDETAIL_TABLE = "Tax_ProductClass_Detail";

		private const string TAXID_PARM = "@TaxID";

		private const string TAXPERCENT_PARM = "@TaxPercent";

		private const string ROWINDEX_PARM = "@RowIndex";

		private const string NOTE_PARM = "@Note";

		private const string CREATEDBY_PARM = "@CreatedBy";

		private const string DATECREATED_PARM = "@DateCreated";

		private const string UPDATEDBY_PARM = "@UpdatedBy";

		private const string DATEUPDATED_PARM = "@DateUpdated";

		public ProductClass(Config config)
			: base(config)
		{
		}

		private string GetInsertUpdateText(bool isUpdate)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddInsertUpdateParameters("Product_Class", new FieldValue("ClassID", "@ClassID", isUpdateConditionField: true), new FieldValue("ClassName", "@ClassName"), new FieldValue("ItemType", "@ItemType"), new FieldValue("CostMethod", "@CostMethod"), new FieldValue("CategoryID", "@CategoryID"), new FieldValue("IsInactive", "@IsInactive"), new FieldValue("UnitID", "@UnitID"), new FieldValue("DivisionID", "@DivisionID"), new FieldValue("COGSAccount", "@COGSAccount"), new FieldValue("AssetAccount", "@AssetAccount"), new FieldValue("IncomeAccount", "@IncomeAccount"), new FieldValue("TaxOption", "@TaxOption"), new FieldValue("TaxGroupID", "@TaxGroupID"), new FieldValue("Note", "@Note"));
			if (isUpdate)
			{
				sqlBuilder.AddInsertUpdateParameters("Product_Class", new FieldValue("DateUpdated", "@DateUpdated", isUpdateConditionField: true, checkForNullValue: true));
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
			parameters.Add("@ClassID", SqlDbType.NVarChar);
			parameters.Add("@ClassName", SqlDbType.NVarChar);
			parameters.Add("@Note", SqlDbType.NVarChar);
			parameters.Add("@IsInactive", SqlDbType.Bit);
			parameters.Add("@ItemType", SqlDbType.TinyInt);
			parameters.Add("@CostMethod", SqlDbType.TinyInt);
			parameters.Add("@CategoryID", SqlDbType.NVarChar);
			parameters.Add("@UnitID", SqlDbType.NVarChar);
			parameters.Add("@COGSAccount", SqlDbType.NVarChar);
			parameters.Add("@AssetAccount", SqlDbType.NVarChar);
			parameters.Add("@IncomeAccount", SqlDbType.NVarChar);
			parameters.Add("@DivisionID", SqlDbType.NVarChar);
			parameters.Add("@TaxOption", SqlDbType.TinyInt);
			parameters.Add("@TaxGroupID", SqlDbType.NVarChar);
			parameters["@ClassID"].SourceColumn = "ClassID";
			parameters["@ClassName"].SourceColumn = "ClassName";
			parameters["@IsInactive"].SourceColumn = "IsInactive";
			parameters["@ItemType"].SourceColumn = "ItemType";
			parameters["@CostMethod"].SourceColumn = "CostMethod";
			parameters["@CategoryID"].SourceColumn = "CategoryID";
			parameters["@UnitID"].SourceColumn = "UnitID";
			parameters["@COGSAccount"].SourceColumn = "COGSAccount";
			parameters["@AssetAccount"].SourceColumn = "AssetAccount";
			parameters["@IncomeAccount"].SourceColumn = "IncomeAccount";
			parameters["@TaxOption"].SourceColumn = "TaxOption";
			parameters["@TaxGroupID"].SourceColumn = "TaxGroupID";
			parameters["@DivisionID"].SourceColumn = "DivisionID";
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

		private string GetInsertUpdateTaxDetailsText(bool isUpdate)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddInsertUpdateParameters("Tax_ProductClass_Detail", new FieldValue("ClassID", "@ClassID"), new FieldValue("TaxID", "@TaxID"), new FieldValue("TaxPercent", "@TaxPercent"), new FieldValue("RowIndex", "@RowIndex"));
			if (isUpdate)
			{
				sqlBuilder.AddInsertUpdateParameters("Tax_ProductClass_Detail", new FieldValue("DateUpdated", "@DateUpdated", isUpdateConditionField: true, checkForNullValue: true));
			}
			if (isUpdate)
			{
				return sqlBuilder.GetUpdateExpression();
			}
			return sqlBuilder.GetInsertExpression();
		}

		private SqlCommand GetInsertUpdateTaxDetailsCommand(bool isUpdate)
		{
			SqlParameterCollection parameters;
			if (isUpdate)
			{
				updateCommand = new SqlCommand(GetInsertUpdateTaxDetailsText(isUpdate: true), base.DBConfig.Connection);
				updateCommand.CommandType = CommandType.Text;
				parameters = updateCommand.Parameters;
			}
			else
			{
				insertCommand = new SqlCommand(GetInsertUpdateTaxDetailsText(isUpdate: false), base.DBConfig.Connection);
				insertCommand.CommandType = CommandType.Text;
				parameters = insertCommand.Parameters;
			}
			parameters.Add("@ClassID", SqlDbType.NVarChar);
			parameters.Add("@TaxPercent", SqlDbType.Decimal);
			parameters.Add("@TaxID", SqlDbType.NVarChar);
			parameters.Add("@RowIndex", SqlDbType.Int);
			parameters["@ClassID"].SourceColumn = "ClassID";
			parameters["@TaxPercent"].SourceColumn = "TaxPercent";
			parameters["@TaxID"].SourceColumn = "TaxID";
			parameters["@RowIndex"].SourceColumn = "RowIndex";
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

		public bool InsertProductClass(ProductClassData accountProductClassData)
		{
			bool flag = true;
			SqlCommand insertUpdateCommand = GetInsertUpdateCommand(isUpdate: false);
			SqlTransaction sqlTransaction = null;
			try
			{
				sqlTransaction = (insertUpdateCommand.Transaction = base.DBConfig.StartNewTransaction());
				flag = Insert(accountProductClassData, "Product_Class", insertUpdateCommand);
				if (accountProductClassData.Tables["Tax_ProductClass_Detail"].Rows.Count > 0)
				{
					string text = accountProductClassData.TaxProductClassDetailTable.Rows[0]["ClassID"].ToString();
					accountProductClassData.TaxProductClassDetailTable.Rows[0]["TaxID"].ToString();
					flag &= DeleteTaxDetailsRows(text, sqlTransaction);
					foreach (DataRow row in accountProductClassData.TaxProductClassDetailTable.Rows)
					{
						row["ClassID"] = text;
					}
					if (flag)
					{
						insertUpdateCommand = GetInsertUpdateTaxDetailsCommand(isUpdate: false);
						insertUpdateCommand.Transaction = sqlTransaction;
						Insert(accountProductClassData, "Tax_ProductClass_Detail", insertUpdateCommand);
					}
				}
				string text2 = accountProductClassData.ProductClassTable.Rows[0]["ClassID"].ToString();
				AddActivityLog("Item Class", text2, ActivityTypes.Add, sqlTransaction);
				UpdateTableRowInsertUpdateInfo("Product_Class", "ClassID", text2, sqlTransaction, isInsert: true);
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

		internal bool DeleteTaxDetailsRows(string classID, SqlTransaction sqlTransaction)
		{
			bool result = true;
			try
			{
				string exp = "select count(*) from  Tax_ProductClass_Detail WHERE ClassID = '" + classID + "'";
				object obj = ExecuteScalar(exp, sqlTransaction);
				if (obj == null)
				{
					return result;
				}
				if (obj.ToString() != "")
				{
					exp = "DELETE FROM Tax_ProductClass_Detail WHERE ClassID = '" + classID + "'";
					return Delete(exp, sqlTransaction);
				}
				return result;
			}
			catch
			{
				throw;
			}
		}

		public bool UpdateProductClass(ProductClassData accountProductClassData)
		{
			bool flag = true;
			SqlCommand insertUpdateCommand = GetInsertUpdateCommand(isUpdate: true);
			SqlTransaction sqlTransaction = null;
			try
			{
				sqlTransaction = (insertUpdateCommand.Transaction = base.DBConfig.StartNewTransaction());
				flag = Update(accountProductClassData, "Product_Class", insertUpdateCommand);
				if (accountProductClassData.Tables["Tax_ProductClass_Detail"].Rows.Count > 0)
				{
					string text = accountProductClassData.TaxProductClassDetailTable.Rows[0]["ClassID"].ToString();
					accountProductClassData.TaxProductClassDetailTable.Rows[0]["TaxID"].ToString();
					flag &= DeleteTaxDetailsRows(text, sqlTransaction);
					foreach (DataRow row in accountProductClassData.TaxProductClassDetailTable.Rows)
					{
						row["ClassID"] = text;
					}
					if (flag)
					{
						insertUpdateCommand = GetInsertUpdateTaxDetailsCommand(isUpdate: false);
						insertUpdateCommand.Transaction = sqlTransaction;
						Insert(accountProductClassData, "Tax_ProductClass_Detail", insertUpdateCommand);
					}
				}
				if (!flag)
				{
					return flag;
				}
				object obj = accountProductClassData.ProductClassTable.Rows[0]["ClassID"];
				UpdateTableRowByID("Product_Class", "ClassID", "DateUpdated", obj, DateTime.Now, sqlTransaction);
				string entiyID = accountProductClassData.ProductClassTable.Rows[0]["ClassName"].ToString();
				AddActivityLog("Item Class", entiyID, ActivityTypes.Update, sqlTransaction);
				UpdateTableRowInsertUpdateInfo("Product_Class", "ClassID", obj, sqlTransaction, isInsert: false);
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

		public ProductClassData GetProductClass()
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddTable("Product_Class");
			ProductClassData productClassData = new ProductClassData();
			sqlBuilder.UseDistinct = false;
			FillDataSet(productClassData, "Product_Class", sqlBuilder);
			return productClassData;
		}

		public bool DeleteProductClass(string productClassID)
		{
			bool flag = true;
			try
			{
				string commandText = "DELETE FROM Product_Class WHERE ClassID = '" + productClassID + "'";
				flag = Delete(commandText, null);
				if (!flag)
				{
					return flag;
				}
				AddActivityLog("Item Class", productClassID, ActivityTypes.Delete, null);
				return flag;
			}
			catch
			{
				throw;
			}
		}

		public ProductClassData GetProductClassByID(string id)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			CommandHelper commandHelper = null;
			commandHelper = new CommandHelper();
			commandHelper.FieldName = "ClassID";
			commandHelper.SqlFieldType = SqlDbType.NVarChar;
			commandHelper.FieldValue = id;
			commandHelper.TableName = "Product_Class";
			sqlBuilder.AddCommandHelper(commandHelper);
			sqlBuilder.IsComparing = true;
			ProductClassData productClassData = new ProductClassData();
			sqlBuilder.UseDistinct = false;
			FillDataSet(productClassData, "Product_Class", sqlBuilder);
			return productClassData;
		}

		public DataSet GetProductClassByFields(params string[] columns)
		{
			return GetProductClassByFields(null, isInactive: true, columns);
		}

		public DataSet GetProductClassByFields(string[] productClassID, params string[] columns)
		{
			return GetProductClassByFields(productClassID, isInactive: true, columns);
		}

		public DataSet GetProductClassByFields(string[] ids, bool isInactive, params string[] columns)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddTable("Product_Class");
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
				commandHelper.FieldName = "ClassID";
				commandHelper.FieldValue = ids;
				commandHelper.SqlFieldType = SqlDbType.NVarChar;
				commandHelper.TableName = "Product_Class";
				sqlBuilder.AddCommandHelper(commandHelper);
			}
			DataSet dataSet = new DataSet();
			dataSet.EnforceConstraints = false;
			sqlBuilder.UseDistinct = false;
			FillDataSet(dataSet, "Product_Class", sqlBuilder);
			return dataSet;
		}

		public DataSet GetProductClassList()
		{
			DataSet dataSet = new DataSet();
			string textCommand = "SELECT ClassID [Class Code],ClassName [Class Name],Note,IsInactive [Inactive]\r\n                           FROM Product_Class ";
			FillDataSet(dataSet, "Product_Class", textCommand);
			return dataSet;
		}

		public DataSet GetProductClassComboList()
		{
			DataSet dataSet = new DataSet();
			string textCommand = "SELECT ClassID [Code],ClassName [Name]\r\n                           FROM Product_Class \r\n                            WHERE IsInactive<>1  ORDER BY ClassID,ClassName";
			FillDataSet(dataSet, "Product_Class", textCommand);
			return dataSet;
		}
	}
}
