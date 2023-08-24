using Micromind.Common;
using Micromind.Common.Data;
using Micromind.Common.Libraries;
using System;
using System.Data;
using System.Data.SqlClient;

namespace Micromind.Data
{
	public sealed class ProductParent : StoreObject
	{
		private const string PRODUCTPARENT_TABLE = "@Product_Parent";

		private const string PRODUCTPARENTID_PARM = "@ParentID";

		private const string DESCRIPTION_PARM = "@Description";

		private const string ITEMTYPE_PARM = "@ItemType";

		private const string COSTMETHOD_PARM = "@CostMethod";

		private const string CATEGORYID_PARM = "@CategoryID";

		private const string QUANTITYPERUNIT_PARM = "@QuantityPerUnit";

		private const string ISINACTIVE_PARM = "@IsInactive";

		private const string UNITID_PARM = "@UnitID";

		private const string REORDERLEVEL_PARM = "@ReorderLevel";

		private const string EXCLUDEFROMCATALOGE_PARM = "@ExcludeFromCatalogue";

		private const string PARENTTYPE_PARM = "@ParentType";

		private const string DIMENSIONS_PARM = "@Dimensions";

		private const string DIM1_PARM = "@Dim1";

		private const string DIM2_PARM = "@Dim2";

		private const string DIM3_PARM = "@Dim3";

		private const string LOOKUPCODE_PARM = "@LookupCode";

		private const string PREFERREDVENDOR_PARM = "@PreferredVendor";

		private const string BRANDID_PARM = "@BrandID";

		private const string MANUFACTURERID_PARM = "@ManufacturerID";

		private const string ORIGIN_PARM = "@Origin";

		private const string DIMENSION_PARM = "@Dimension";

		private const string DIMENSIONID_PARM = "@DimensionID";

		private const string ATTRIBUTEID_PARM = "@AttributeID";

		private const string ATTRIBUTENAME_PARM = "@AttributeName";

		private const string ROWINDEX_PARM = "@RowIndex";

		private const string UNITPRICE1_PARM = "@UnitPrice1";

		private const string UNITPRICE2_PARM = "@UnitPrice2";

		private const string UNITPRICE3_PARM = "@UnitPrice3";

		private const string MINPRICE_PARM = "@MinPrice";

		private const string UPC_PARM = "@UPC";

		public const string PRODUCTID_PARM = "@ProductID";

		private const string ATTRIBUTE1_PARM = "@Attribute1";

		private const string ATTRIBUTE2_PARM = "@Attribute2";

		private const string ATTRIBUTE3_PARM = "@Attribute3";

		private const string NOTE_PARM = "@Note";

		private const string CREATEDBY_PARM = "@CreatedBy";

		private const string DATECREATED_PARM = "@DateCreated";

		private const string UPDATEDBY_PARM = "@UpdatedBy";

		private const string DATEUPDATED_PARM = "@DateUpdated";

		public ProductParent(Config config)
			: base(config)
		{
		}

		private string GetInsertUpdateText(bool isUpdate)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddInsertUpdateParameters("Product_Parent", new FieldValue("ProductParentID", "@ParentID", isUpdateConditionField: true), new FieldValue("Description", "@Description"), new FieldValue("ParentType", "@ParentType"), new FieldValue("Dimensions", "@Dimensions"), new FieldValue("Dim1", "@Dim1"), new FieldValue("Dim2", "@Dim2"), new FieldValue("Dim3", "@Dim3"), new FieldValue("LookupCode", "@LookupCode"), new FieldValue("UnitPrice1", "@UnitPrice1"), new FieldValue("UnitPrice2", "@UnitPrice2"), new FieldValue("UnitPrice3", "@UnitPrice3"), new FieldValue("MinPrice", "@MinPrice"), new FieldValue("ExcludeFromCatalogue", "@ExcludeFromCatalogue"), new FieldValue("ItemType", "@ItemType"), new FieldValue("CostMethod", "@CostMethod"), new FieldValue("CategoryID", "@CategoryID"), new FieldValue("QuantityPerUnit", "@QuantityPerUnit"), new FieldValue("IsInactive", "@IsInactive"), new FieldValue("UnitID", "@UnitID"), new FieldValue("PreferredVendor", "@PreferredVendor"), new FieldValue("BrandID", "@BrandID"), new FieldValue("ManufacturerID", "@ManufacturerID"), new FieldValue("Origin", "@Origin"), new FieldValue("Note", "@Note"));
			if (isUpdate)
			{
				sqlBuilder.AddInsertUpdateParameters("Product_Parent", new FieldValue("DateUpdated", "@DateUpdated", isUpdateConditionField: true, checkForNullValue: true));
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
				updateCommand = new SqlCommand(GetInsertUpdateText(isUpdate: true), base.DBConfig.Connection);
				updateCommand.CommandType = CommandType.Text;
				parameters = updateCommand.Parameters;
			}
			else
			{
				insertCommand = new SqlCommand(GetInsertUpdateText(isUpdate: false), base.DBConfig.Connection);
				insertCommand.CommandType = CommandType.Text;
				parameters = insertCommand.Parameters;
			}
			parameters.Add("@ParentID", SqlDbType.NVarChar);
			parameters.Add("@Description", SqlDbType.NVarChar);
			parameters.Add("@Note", SqlDbType.NVarChar);
			parameters.Add("@IsInactive", SqlDbType.Bit);
			parameters.Add("@ItemType", SqlDbType.TinyInt);
			parameters.Add("@CostMethod", SqlDbType.TinyInt);
			parameters.Add("@CategoryID", SqlDbType.NVarChar);
			parameters.Add("@ExcludeFromCatalogue", SqlDbType.Bit);
			parameters.Add("@UnitPrice1", SqlDbType.NVarChar);
			parameters.Add("@UnitPrice2", SqlDbType.NVarChar);
			parameters.Add("@UnitPrice3", SqlDbType.NVarChar);
			parameters.Add("@MinPrice", SqlDbType.NVarChar);
			parameters.Add("@ParentType", SqlDbType.TinyInt);
			parameters.Add("@Dimensions", SqlDbType.TinyInt);
			parameters.Add("@Dim1", SqlDbType.NVarChar);
			parameters.Add("@Dim2", SqlDbType.NVarChar);
			parameters.Add("@Dim3", SqlDbType.NVarChar);
			parameters.Add("@LookupCode", SqlDbType.NVarChar);
			parameters.Add("@QuantityPerUnit", SqlDbType.Real);
			parameters.Add("@UnitID", SqlDbType.NVarChar);
			parameters.Add("@PreferredVendor", SqlDbType.NVarChar);
			parameters.Add("@BrandID", SqlDbType.NVarChar);
			parameters.Add("@ManufacturerID", SqlDbType.NVarChar);
			parameters.Add("@Origin", SqlDbType.NVarChar);
			parameters["@ParentID"].SourceColumn = "ProductParentID";
			parameters["@Description"].SourceColumn = "Description";
			parameters["@IsInactive"].SourceColumn = "IsInactive";
			parameters["@ParentType"].SourceColumn = "ParentType";
			parameters["@Dimensions"].SourceColumn = "Dimensions";
			parameters["@Dim1"].SourceColumn = "Dim1";
			parameters["@Dim2"].SourceColumn = "Dim2";
			parameters["@Dim3"].SourceColumn = "Dim3";
			parameters["@LookupCode"].SourceColumn = "LookupCode";
			parameters["@ExcludeFromCatalogue"].SourceColumn = "ExcludeFromCatalogue";
			parameters["@UnitPrice1"].SourceColumn = "UnitPrice1";
			parameters["@UnitPrice2"].SourceColumn = "UnitPrice2";
			parameters["@UnitPrice3"].SourceColumn = "UnitPrice3";
			parameters["@MinPrice"].SourceColumn = "MinPrice";
			parameters["@ItemType"].SourceColumn = "ItemType";
			parameters["@CostMethod"].SourceColumn = "CostMethod";
			parameters["@CategoryID"].SourceColumn = "CategoryID";
			parameters["@QuantityPerUnit"].SourceColumn = "QuantityPerUnit";
			parameters["@UnitID"].SourceColumn = "UnitID";
			parameters["@PreferredVendor"].SourceColumn = "PreferredVendor";
			parameters["@BrandID"].SourceColumn = "BrandID";
			parameters["@ManufacturerID"].SourceColumn = "ManufacturerID";
			parameters["@Origin"].SourceColumn = "Origin";
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

		private string GetInsertUpdateMatrixAttributeDimensionText(bool isUpdate)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddInsertUpdateParameters("Matrix_Attribute_Dimension", new FieldValue("ProductParentID", "@ParentID", isUpdateConditionField: true), new FieldValue("Dimension", "@Dimension"), new FieldValue("DimensionID", "@DimensionID"), new FieldValue("AttributeID", "@AttributeID"), new FieldValue("AttributeName", "@AttributeName"), new FieldValue("RowIndex", "@RowIndex"));
			if (isUpdate)
			{
				return sqlBuilder.GetUpdateExpression();
			}
			return sqlBuilder.GetInsertExpression();
		}

		private SqlCommand GetInsertUpdateMatrixAttributeDimensionCommand(bool isUpdate)
		{
			SqlParameterCollection parameters;
			if (isUpdate)
			{
				updateCommand = new SqlCommand(GetInsertUpdateMatrixAttributeDimensionText(isUpdate: true), base.DBConfig.Connection);
				updateCommand.CommandType = CommandType.Text;
				parameters = updateCommand.Parameters;
			}
			else
			{
				insertCommand = new SqlCommand(GetInsertUpdateMatrixAttributeDimensionText(isUpdate: false), base.DBConfig.Connection);
				insertCommand.CommandType = CommandType.Text;
				parameters = insertCommand.Parameters;
			}
			parameters.Add("@ParentID", SqlDbType.NVarChar);
			parameters.Add("@Dimension", SqlDbType.NVarChar);
			parameters.Add("@DimensionID", SqlDbType.NVarChar);
			parameters.Add("@AttributeID", SqlDbType.NVarChar);
			parameters.Add("@AttributeName", SqlDbType.NVarChar);
			parameters.Add("@RowIndex", SqlDbType.Int);
			parameters["@ParentID"].SourceColumn = "ProductParentID";
			parameters["@Dimension"].SourceColumn = "Dimension";
			parameters["@DimensionID"].SourceColumn = "DimensionID";
			parameters["@AttributeID"].SourceColumn = "AttributeID";
			parameters["@AttributeName"].SourceColumn = "AttributeName";
			parameters["@RowIndex"].SourceColumn = "RowIndex";
			if (isUpdate)
			{
				return updateCommand;
			}
			return insertCommand;
		}

		private string GetInsertUpdateComponentsText(bool isUpdate)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddInsertUpdateParameters("Product_Parent_Components", new FieldValue("ProductParentID", "@ParentID"), new FieldValue("ProductID", "@ProductID"), new FieldValue("Attribute1", "@Attribute1"), new FieldValue("Attribute2", "@Attribute2"), new FieldValue("Attribute3", "@Attribute3"));
			if (isUpdate)
			{
				sqlBuilder.AddInsertUpdateParameters("Product_Parent", new FieldValue("DateUpdated", "@DateUpdated", isUpdateConditionField: true, checkForNullValue: true));
			}
			if (isUpdate)
			{
				return sqlBuilder.GetUpdateExpression();
			}
			return sqlBuilder.GetInsertExpression();
		}

		private SqlCommand GetInsertUpdateComponentsCommand(bool isUpdate)
		{
			SqlParameterCollection parameters;
			if (isUpdate)
			{
				updateCommand = new SqlCommand(GetInsertUpdateComponentsText(isUpdate: true), base.DBConfig.Connection);
				updateCommand.CommandType = CommandType.Text;
				parameters = updateCommand.Parameters;
			}
			else
			{
				insertCommand = new SqlCommand(GetInsertUpdateComponentsText(isUpdate: false), base.DBConfig.Connection);
				insertCommand.CommandType = CommandType.Text;
				parameters = insertCommand.Parameters;
			}
			parameters.Add("@ParentID", SqlDbType.NVarChar);
			parameters.Add("@ProductID", SqlDbType.NVarChar);
			parameters.Add("@Attribute1", SqlDbType.NVarChar);
			parameters.Add("@Attribute2", SqlDbType.NVarChar);
			parameters.Add("@Attribute3", SqlDbType.NVarChar);
			parameters["@ParentID"].SourceColumn = "ProductParentID";
			parameters["@ProductID"].SourceColumn = "ProductID";
			parameters["@Attribute1"].SourceColumn = "Attribute1";
			parameters["@Attribute2"].SourceColumn = "Attribute2";
			parameters["@Attribute3"].SourceColumn = "Attribute3";
			if (isUpdate)
			{
				return updateCommand;
			}
			return insertCommand;
		}

		private string GetUpdateProductText()
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddInsertUpdateParameters("Product", new FieldValue("ProductID", "@ProductID", isUpdateConditionField: true), new FieldValue("Description", "@Description"), new FieldValue("UPC", "@UPC"), new FieldValue("UnitPrice1", "@UnitPrice1"), new FieldValue("UnitPrice2", "@UnitPrice2"), new FieldValue("UnitPrice3", "@UnitPrice3"), new FieldValue("MinPrice", "@MinPrice"), new FieldValue("CategoryID", "@CategoryID"), new FieldValue("QuantityPerUnit", "@QuantityPerUnit"), new FieldValue("IsInactive", "@IsInactive"), new FieldValue("UnitID", "@UnitID"), new FieldValue("ReorderLevel", "@ReorderLevel"), new FieldValue("PreferredVendor", "@PreferredVendor"), new FieldValue("BrandID", "@BrandID"), new FieldValue("ManufacturerID", "@ManufacturerID"), new FieldValue("Origin", "@Origin"));
			return sqlBuilder.GetUpdateExpression();
		}

		private SqlCommand GetUpdateProductCommand()
		{
			updateCommand = new SqlCommand(GetUpdateProductText(), base.DBConfig.Connection);
			updateCommand.CommandType = CommandType.Text;
			SqlParameterCollection parameters = updateCommand.Parameters;
			parameters.Add("@ProductID", SqlDbType.NVarChar);
			parameters.Add("@Description", SqlDbType.NVarChar);
			parameters.Add("@UPC", SqlDbType.NVarChar);
			parameters.Add("@UnitPrice1", SqlDbType.Money);
			parameters.Add("@UnitPrice2", SqlDbType.Money);
			parameters.Add("@UnitPrice3", SqlDbType.Money);
			parameters.Add("@MinPrice", SqlDbType.Money);
			parameters.Add("@CategoryID", SqlDbType.NVarChar);
			parameters.Add("@QuantityPerUnit", SqlDbType.Real);
			parameters.Add("@IsInactive", SqlDbType.Bit);
			parameters.Add("@UnitID", SqlDbType.NVarChar);
			parameters.Add("@ReorderLevel", SqlDbType.Real);
			parameters.Add("@PreferredVendor", SqlDbType.NVarChar);
			parameters.Add("@BrandID", SqlDbType.NVarChar);
			parameters.Add("@ManufacturerID", SqlDbType.NVarChar);
			parameters.Add("@Origin", SqlDbType.NVarChar);
			parameters["@ProductID"].SourceColumn = "ProductID";
			parameters["@Description"].SourceColumn = "Description";
			parameters["@UPC"].SourceColumn = "UPC";
			parameters["@UnitPrice1"].SourceColumn = "UnitPrice1";
			parameters["@UnitPrice2"].SourceColumn = "UnitPrice2";
			parameters["@UnitPrice3"].SourceColumn = "UnitPrice3";
			parameters["@MinPrice"].SourceColumn = "MinPrice";
			parameters["@CategoryID"].SourceColumn = "CategoryID";
			parameters["@QuantityPerUnit"].SourceColumn = "QuantityPerUnit";
			parameters["@IsInactive"].SourceColumn = "IsInactive";
			parameters["@UnitID"].SourceColumn = "UnitID";
			parameters["@ReorderLevel"].SourceColumn = "ReorderLevel";
			parameters["@PreferredVendor"].SourceColumn = "PreferredVendor";
			parameters["@BrandID"].SourceColumn = "BrandID";
			parameters["@ManufacturerID"].SourceColumn = "ManufacturerID";
			parameters["@Origin"].SourceColumn = "Origin";
			parameters.Add("@DateUpdated", SqlDbType.DateTime);
			parameters["@DateUpdated"].SourceColumn = "DateUpdated";
			return updateCommand;
		}

		private bool DeleteMatrixAttributeDimensionRows(string productParentID, SqlTransaction sqlTransaction)
		{
			bool flag = true;
			try
			{
				string commandText = "DELETE FROM Matrix_Attribute_Dimension WHERE ProductParentID = '" + productParentID + "'";
				flag = Delete(commandText, sqlTransaction);
				if (!flag)
				{
					return flag;
				}
				AddActivityLog("Product Matrix Dimension", productParentID, ActivityTypes.Delete, null);
				return flag;
			}
			catch
			{
				throw;
			}
		}

		private bool DeleteComponentRows(string productParentID, SqlTransaction sqlTransaction)
		{
			bool flag = true;
			try
			{
				string commandText = "DELETE FROM Product_Parent_Components WHERE ProductParentID = '" + productParentID + "'";
				flag = Delete(commandText, sqlTransaction);
				if (!flag)
				{
					return flag;
				}
				AddActivityLog("Product Parent Component", productParentID, ActivityTypes.Delete, null);
				return flag;
			}
			catch
			{
				throw;
			}
		}

		public bool DeleteComponents(string[] productID)
		{
			bool flag = true;
			try
			{
				string text = "";
				for (int i = 0; i < productID.Length; i++)
				{
					if (i > 0)
					{
						text += ",";
					}
					text = text + "'" + productID[i] + "'";
				}
				string textCommand = "SELECT  DISTINCT ProductID FROM Inventory_Transactions\r\n                                WHERE ProductID IN \r\n                                (" + text + ")\r\n                                    UNION\r\n\r\n                                    SELECT  DISTINCT ProductID FROM Inventory_Transactions\r\n                                                    WHERE ProductID IN \r\n                                                    (" + text + ")\r\n                                    UNION\r\n                                    SELECT  DISTINCT ProductID FROM Sales_Quote_Detail\r\n                                                                    WHERE ProductID IN \r\n                                                                   (" + text + ")\r\n                                    UNION\r\n                                    SELECT  DISTINCT ProductID FROM Sales_Order_Detail\r\n                                                                    WHERE ProductID IN \r\n                                                                    (" + text + ")\r\n                                     UNION\r\n                                    SELECT  DISTINCT ProductID FROM Purchase_Order_Detail\r\n                                                                    WHERE ProductID IN \r\n                                                                   (" + text + ")\r\n                                    UNION\r\n                                    SELECT  DISTINCT ProductID FROM Purchase_Quote_Detail\r\n                                                                    WHERE ProductID IN \r\n                                                                    (" + text + ")\r\n                                    UNION\r\n                                    SELECT  DISTINCT ProductID FROM Purchase_Order_Detail\r\n                                                                    WHERE ProductID IN \r\n                                                                    (" + text + ")\r\n                                    UNION\r\n                                    SELECT  DISTINCT ProductID FROM Delivery_Note_Detail\r\n                                                                    WHERE ProductID IN \r\n                                                                   (" + text + ")";
				DataSet dataSet = new DataSet();
				FillDataSet(dataSet, "Products", textCommand);
				bool flag2 = false;
				if ((dataSet != null && dataSet.Tables.Count != 0 && dataSet.Tables[0].Rows.Count != 0) ? true : false)
				{
					string text2 = "One or more selected components are in use.\nUsed components are: \n";
					int num = 0;
					foreach (DataRow row in dataSet.Tables[0].Rows)
					{
						if (num >= 10)
						{
							break;
						}
						if (num > 0)
						{
							text2 += ", ";
						}
						text2 += row["ProductID"].ToString();
						num++;
					}
					throw new CompanyException(text2, 1029);
				}
				SqlTransaction sqlTransaction = null;
				sqlTransaction = base.DBConfig.StartNewTransaction();
				string exp = "DELETE FROM Product\r\n                                WHERE ProductID IN (" + text + ")";
				if (ExecuteNonQuery(exp, sqlTransaction) < 0)
				{
					throw new CompanyException("One or more components are used in transactions.", 1029);
				}
				exp = "DELETE FROM Product_Parent_Components  WHERE ProductID IN (" + text + ")";
				flag &= Delete(exp, sqlTransaction);
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

		public bool InsertUpdateProductParent(ProductParentData productParentData, bool isUpdate)
		{
			bool flag = true;
			SqlCommand insertUpdateCommand = GetInsertUpdateCommand(isUpdate);
			SqlTransaction sqlTransaction = null;
			try
			{
				sqlTransaction = (insertUpdateCommand.Transaction = base.DBConfig.StartNewTransaction());
				flag = (isUpdate ? Update(productParentData, "Product_Parent", insertUpdateCommand) : Insert(productParentData, "Product_Parent", insertUpdateCommand));
				string text = productParentData.ProductParentTable.Rows[0]["ProductParentID"].ToString();
				flag &= DeleteMatrixAttributeDimensionRows(text, sqlTransaction);
				if (productParentData.MatrixAttributeDimensionTable.Rows.Count > 0)
				{
					insertUpdateCommand = GetInsertUpdateMatrixAttributeDimensionCommand(isUpdate: false);
					insertUpdateCommand.Transaction = sqlTransaction;
					flag &= Insert(productParentData, "Matrix_Attribute_Dimension", insertUpdateCommand);
				}
				flag &= DeleteComponentRows(text, sqlTransaction);
				if (productParentData.ProdutParentComponentsTable.Rows.Count > 0)
				{
					insertUpdateCommand = GetInsertUpdateComponentsCommand(isUpdate: false);
					insertUpdateCommand.Transaction = sqlTransaction;
					flag &= Insert(productParentData, "Product_Parent_Components", insertUpdateCommand);
				}
				ProductData productData = new ProductData();
				if (!isUpdate)
				{
					productData.ProductTable.Merge(productParentData.ProdutParentComponentsTable, preserveChanges: true, MissingSchemaAction.AddWithKey);
					foreach (DataRow row in productData.ProductTable.Rows)
					{
						row.SetAdded();
					}
					flag &= new Products(base.DBConfig).InsertUpdateProduct(productData, isUpdate: false, sqlTransaction);
				}
				else
				{
					DataRow[] array = productParentData.ProdutParentComponentsTable.Select("IsNewRow='True'");
					if (array != null && array.Length != 0)
					{
						productData.Tables.Clear();
						productData.Merge(array);
						productData.Tables[0].TableName = "Product";
						foreach (DataRow row2 in productData.ProductTable.Rows)
						{
							row2.SetAdded();
						}
						flag &= new Products(base.DBConfig).InsertUpdateProduct(productData, isUpdate: false, sqlTransaction);
					}
					productData.ProductTable.Merge(productParentData.ProdutParentComponentsTable, preserveChanges: true, MissingSchemaAction.AddWithKey);
					foreach (DataRow row3 in productData.ProductTable.Rows)
					{
						row3.SetModified();
					}
					insertUpdateCommand = GetUpdateProductCommand();
					insertUpdateCommand.Transaction = sqlTransaction;
					flag &= Update(productData, "Product", insertUpdateCommand);
				}
				AddActivityLog("Item Parent", text, ActivityTypes.Add, sqlTransaction);
				UpdateTableRowInsertUpdateInfo("Product_Parent", "ProductParentID", text, sqlTransaction, !isUpdate);
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

		public bool UpdateProductParent(ProductParentData productParentData)
		{
			bool flag = true;
			SqlCommand insertUpdateCommand = GetInsertUpdateCommand(isUpdate: true);
			SqlTransaction sqlTransaction = null;
			try
			{
				sqlTransaction = (insertUpdateCommand.Transaction = base.DBConfig.StartNewTransaction());
				flag = Update(productParentData, "Product_Parent", insertUpdateCommand);
				if (!flag)
				{
					return flag;
				}
				object obj = productParentData.ProductParentTable.Rows[0]["ProductParentID"];
				UpdateTableRowByID("Product_Parent", "ProductParentID", "DateUpdated", obj, DateTime.Now, sqlTransaction);
				string entiyID = productParentData.ProductParentTable.Rows[0]["Description"].ToString();
				AddActivityLog("Item Parent", entiyID, ActivityTypes.Update, sqlTransaction);
				UpdateTableRowInsertUpdateInfo("Product_Parent", "ProductParentID", obj, sqlTransaction, isInsert: false);
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

		public ProductParentData GetProductParent()
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddTable("Product_Parent");
			ProductParentData productParentData = new ProductParentData();
			sqlBuilder.UseDistinct = false;
			FillDataSet(productParentData, "Product_Parent", sqlBuilder);
			return productParentData;
		}

		public bool DeleteProductParent(string productParentID, bool deleteComponents)
		{
			bool flag = true;
			try
			{
				bool flag2 = false;
				string textCommand = "SELECT  DISTINCT ProductID FROM Inventory_Transactions\r\n                                WHERE ProductID IN \r\n                                (SELECT ProductID FROM Product_Parent_Components PPC WHERE PPC.ProductParentID = '" + productParentID + "')\r\n                                    UNION\r\n\r\n                                    SELECT  DISTINCT ProductID FROM Inventory_Transactions\r\n                                                    WHERE ProductID IN \r\n                                                    (SELECT ProductID FROM Product_Parent_Components PPC WHERE PPC.ProductParentID = '" + productParentID + "')\r\n                                    UNION\r\n                                    SELECT  DISTINCT ProductID FROM Sales_Quote_Detail\r\n                                                                    WHERE ProductID IN \r\n                                                                    (SELECT ProductID FROM Product_Parent_Components PPC WHERE PPC.ProductParentID = '" + productParentID + "')\r\n                                    UNION\r\n                                    SELECT  DISTINCT ProductID FROM Sales_Order_Detail\r\n                                                                    WHERE ProductID IN \r\n                                                                    (SELECT ProductID FROM Product_Parent_Components PPC WHERE PPC.ProductParentID = '" + productParentID + "')\r\n                                     UNION\r\n                                    SELECT  DISTINCT ProductID FROM Purchase_Order_Detail\r\n                                                                    WHERE ProductID IN \r\n                                                                    (SELECT ProductID FROM Product_Parent_Components PPC WHERE PPC.ProductParentID = '" + productParentID + "')\r\n                                    UNION\r\n                                    SELECT  DISTINCT ProductID FROM Purchase_Quote_Detail\r\n                                                                    WHERE ProductID IN \r\n                                                                    (SELECT ProductID FROM Product_Parent_Components PPC WHERE PPC.ProductParentID = '" + productParentID + "')\r\n                                    UNION\r\n                                    SELECT  DISTINCT ProductID FROM Purchase_Order_Detail\r\n                                                                    WHERE ProductID IN \r\n                                                                    (SELECT ProductID FROM Product_Parent_Components PPC WHERE PPC.ProductParentID = '" + productParentID + "')\r\n                                    UNION\r\n                                    SELECT  DISTINCT ProductID FROM Delivery_Note_Detail\r\n                                                                    WHERE ProductID IN \r\n                                                                    (SELECT ProductID FROM Product_Parent_Components PPC WHERE PPC.ProductParentID = '" + productParentID + "') ";
				DataSet dataSet = new DataSet();
				FillDataSet(dataSet, "Products", textCommand);
				if ((dataSet != null && dataSet.Tables.Count != 0 && dataSet.Tables[0].Rows.Count != 0) ? true : false)
				{
					string text = "One or more components are in use.\nUsed components are: \n";
					int num = 0;
					foreach (DataRow row in dataSet.Tables[0].Rows)
					{
						if (num >= 10)
						{
							break;
						}
						if (num > 0)
						{
							text += ", ";
						}
						text += row["ProductID"].ToString();
						num++;
					}
					throw new CompanyException(text, 1029);
				}
				SqlTransaction sqlTransaction = null;
				sqlTransaction = base.DBConfig.StartNewTransaction();
				string exp;
				if (deleteComponents)
				{
					exp = "DELETE FROM Product\r\n                                WHERE ProductID IN \r\n                                (SELECT ProductID FROM Product_Parent_Components PPC WHERE PPC.ProductParentID = '" + productParentID + "')";
					if (ExecuteNonQuery(exp, sqlTransaction) < 0)
					{
						throw new CompanyException("One or more components are used in transactions.", 1029);
					}
				}
				flag &= DeleteComponentRows(productParentID, sqlTransaction);
				flag &= DeleteMatrixAttributeDimensionRows(productParentID, sqlTransaction);
				exp = "DELETE FROM Product_Parent WHERE ProductParentID = '" + productParentID + "'";
				flag &= Delete(exp, sqlTransaction);
				if (!flag)
				{
					return flag;
				}
				AddActivityLog("Product Parent", productParentID, ActivityTypes.Delete, null);
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

		public ProductParentData GetProductParentByID(string id)
		{
			ProductParentData productParentData = new ProductParentData();
			string textCommand = "SELECT *,ISNULL((SELECT 1\r\n                            FROM Product_Parent P2 WHERE Photo IS Not NULL AND P2.ProductParentID=P1.ProductParentID ),0) AS HasPhoto\r\n\r\n                           FROM Product_Parent P1 WHere ProductParentID = '" + id + "'";
			FillDataSet(productParentData, "Product_Parent", textCommand);
			textCommand = "SELECT MAD.*\r\n                        FROM Matrix_Attribute_Dimension MAD\r\n                        INNER JOIN Product_Parent PP ON PP.ProductParentID= MAD.ProductParentID\r\n                        WHERE MAD.ProductParentID = '" + id + "'\r\n                        ORDER BY Dimension,RowIndex";
			FillDataSet(productParentData, "Matrix_Attribute_Dimension", textCommand);
			textCommand = "SELECT ProductParentID,P.ProductID,ppc.Attribute1,PPC.Attribute2,PPC.Attribute3,P.Description,P.Upc,UnitPrice1,P.LastCost,\r\n\t                    P.UnitPrice2,P.UnitPrice3,P.MinPrice,P.CategoryID,P.PreferredVendor,P.BrandID,P.IsInactive,P.QuantityPerUnit,\r\n\t                    P.UnitID,P.ManufacturerID,P.Origin\r\n                        FROM Product_Parent_Components PPC \r\n                        INNER JOIN Product P ON PPC.ProductID = P.ProductID\r\n                        WHere ProductParentID = '" + id + "'";
			FillDataSet(productParentData, "Product_Parent_Components", textCommand);
			return productParentData;
		}

		public DataSet GetProductParentByFields(params string[] columns)
		{
			return GetProductParentByFields(null, isInactive: true, columns);
		}

		public DataSet GetProductParentByFields(string[] productParentID, params string[] columns)
		{
			return GetProductParentByFields(productParentID, isInactive: true, columns);
		}

		public DataSet GetProductParentByFields(string[] ids, bool isInactive, params string[] columns)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddTable("Product_Parent");
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
				commandHelper.FieldName = "ProductParentID";
				commandHelper.FieldValue = ids;
				commandHelper.SqlFieldType = SqlDbType.NVarChar;
				commandHelper.TableName = "Product_Parent";
				sqlBuilder.AddCommandHelper(commandHelper);
			}
			DataSet dataSet = new DataSet();
			dataSet.EnforceConstraints = false;
			sqlBuilder.UseDistinct = false;
			FillDataSet(dataSet, "Product_Parent", sqlBuilder);
			return dataSet;
		}

		public DataSet GetProductParentList()
		{
			DataSet dataSet = new DataSet();
			string textCommand = "SELECT ParentID [Parent Code],ParentName [Parent Name],Note,IsInactive [Inactive]\r\n                           FROM Product_Parent ";
			FillDataSet(dataSet, "Product_Parent", textCommand);
			return dataSet;
		}

		public DataSet GetProductParentComboList()
		{
			DataSet dataSet = new DataSet();
			string textCommand = "SELECT ProductParentID [Code],Description [Name],ParentType\r\n                           FROM Product_Parent \r\n                            WHERE IsInactive<>1  ORDER BY ProductParentID,Description";
			FillDataSet(dataSet, "Product_Parent", textCommand);
			return dataSet;
		}

		public DataSet GetMatrixTable(string productParentID, bool showAllComponents)
		{
			DataSet dataSet = new DataSet();
			bool flag = true;
			string text = "";
			text = " SELECT Max(ISNULL(Dimension,0)) FROM [Matrix_Attribute_Dimension]\r\n                        WHERE ProductParentID = '" + productParentID + "'";
			int result = 0;
			int.TryParse(ExecuteScalar(text).ToString(), out result);
			switch (result)
			{
			case 1:
				text = "SELECT 1 AS DimensionCount, AttributeID,DimensionID, NULL AS QTY FROM  Matrix_Attribute_Dimension MAD \r\n                            WHERE ProductParentID = '" + productParentID + "' ";
				if (!showAllComponents)
				{
					text += " AND AttributeID IN (SELECT Attribute1 FROM Product_Parent_Components PPC  WHERE PPC.ProductParentID = Mad.ProductParentID) ";
				}
				text += " ORDER BY RowIndex";
				break;
			case 2:
				if (flag)
				{
					text = " DECLARE @cols AS NVARCHAR(MAX);\r\n\r\n                                 DECLARE @query  AS NVARCHAR(MAX);\r\n\r\n                                SET @cols = STUFF((SELECT ',' + QUOTENAME(MAD.AttributeID) \r\n                                            FROM Matrix_Attribute_Dimension MAD  WHERE Dimension = 2 AND ProductParentID = '" + productParentID + "' ";
					if (!showAllComponents)
					{
						text += " AND AttributeID IN (SELECT Attribute2 FROM Product_Parent_Components PPC  WHERE PPC.ProductParentID = Mad.ProductParentID) ";
					}
					text = text + "  ORDER BY Mad.RowIndex      FOR XML PATH(''), TYPE\r\n                                            ).value('.', 'NVARCHAR(MAX)') \r\n                                        ,1,1,'') \r\n\r\n                                SET @query =  'SELECT DISTINCT 2 AS DimensionCount,DimensionID1,P.ATR1, '  + @cols + 'from \r\n                                       (SELECT MAD.ProductParentID,DimensionID AS DimensionID1,RowIndex, MAD.AttributeID,MAD.AttributeID AS ATR1,\r\n\t\t                                 0 as qty\r\n                                FROM Matrix_Attribute_Dimension MAD  WHERE MAD.ProductParentID = ''" + productParentID + "'' AND MAD.Dimension=1 ";
					if (!showAllComponents)
					{
						text += " AND AttributeID IN (SELECT Attribute1 FROM Product_Parent_Components PPC  WHERE PPC.ProductParentID = Mad.ProductParentID)";
					}
					text += " ) D  PIVOT (Max(qty) FOR  AttributeID IN('  + @cols + ')) AS P' \r\n\r\n                                 execute (@query) ";
				}
				else
				{
					text = " DECLARE @cols AS NVARCHAR(MAX);\r\n\r\n                                 DECLARE @query  AS NVARCHAR(MAX);\r\n\r\n                                SET @cols = STUFF((SELECT  ',' + QUOTENAME(MAD.AttributeID) \r\n                                            FROM Matrix_Attribute_Dimension MAD  WHERE Dimension = 1 AND ProductParentID = '" + productParentID + "' ";
					if (!showAllComponents)
					{
						text += " AND AttributeID IN (SELECT Attribute1 FROM Product_Parent_Components PPC  WHERE PPC.ProductParentID = Mad.ProductParentID) ";
					}
					text = text + "   ORDER BY Mad.RowIndex     FOR XML PATH(''), TYPE\r\n                                            ).value('.', 'NVARCHAR(MAX)') \r\n                                        ,1,1,'') \r\n\r\n                                SET @query =  'SELECT DISTINCT 2 AS DimensionCount,DimensionID2,P.ATR2, '  + @cols + 'from \r\n                                       (SELECT MAD.ProductParentID,DimensionID AS DimensionID2,RowIndex, MAD.AttributeID,MAD.AttributeID AS ATR2,\r\n\t\t                                 0 as qty\r\n                                FROM Matrix_Attribute_Dimension MAD  WHERE MAD.ProductParentID = ''" + productParentID + "'' AND MAD.Dimension=2 ";
					if (!showAllComponents)
					{
						text += " AND AttributeID IN (SELECT Attribute2 FROM Product_Parent_Components PPC  WHERE PPC.ProductParentID = Mad.ProductParentID)";
					}
					text += " ) D  PIVOT (Max(qty) FOR  AttributeID IN('  + @cols + ')) AS P' \r\n\r\n                                 execute (@query) ";
				}
				break;
			case 3:
				if (flag)
				{
					text = " DECLARE @cols AS NVARCHAR(MAX);\r\n                                 DECLARE @query  AS NVARCHAR(MAX);\r\n                       \r\n                                SET @cols = STUFF((SELECT ',' + QUOTENAME(MAD.AttributeID) \r\n                                            FROM Matrix_Attribute_Dimension MAD  WHERE Dimension = 2 AND ProductParentID='" + productParentID + "'";
					if (!showAllComponents)
					{
						text += " AND AttributeID IN (SELECT Attribute2 FROM Product_Parent_Components PPC  WHERE PPC.ProductParentID = Mad.ProductParentID) ";
					}
					text = text + "   ORDER BY Mad.RowIndex FOR XML PATH(''), TYPE\r\n                                            ).value('.', 'NVARCHAR(MAX)') \r\n                                        ,1,1,'') \r\n\r\n\r\n                                SET @query =  'SELECT DISTINCT 3 AS DimensionCount, MAD3.DimensionID AS DimensionID3,P.DimensionID AS DimensionID1, MAD3.AttributeID AS ATR3, P.ATR1, ' + @cols + 'from \r\n                                       (SELECT MAD.ProductParentID,MAD.DimensionID,RowIndex, MAD.AttributeID,MAD.AttributeID AS ATR1,\r\n\t\t                                 0 as qty\r\n                                FROM Matrix_Attribute_Dimension MAD  WHERE MAD.ProductParentID = ''" + productParentID + "'' AND MAD.Dimension=1 ";
					if (!showAllComponents)
					{
						text += " AND AttributeID IN (SELECT Attribute1 FROM Product_Parent_Components PPC  WHERE PPC.ProductParentID = Mad.ProductParentID AND Dimension = 1) ";
					}
					text += ") D\r\n                                PIVOT (Max(qty) FOR  AttributeID IN('  + @cols + ')) AS P\r\n                                INNER JOIN Matrix_Attribute_Dimension MAD3 ON MAD3.ProductParentID = P.ProductParentID AND Dimension=3";
					if (!showAllComponents)
					{
						text += "AND AttributeID IN (SELECT Attribute3 FROM Product_Parent_Components PPC  WHERE PPC.ProductParentID = Mad3.ProductParentID AND Dimension=3 AND P.ATR1 = Attribute1)";
					}
					text += " ORDER BY MAD3.RowIndex,P.RowIndex' \r\n\r\n                                execute (@query)";
				}
				else
				{
					text = " DECLARE @cols AS NVARCHAR(MAX);\r\n                                 DECLARE @query  AS NVARCHAR(MAX);\r\n                       \r\n                                SET @cols = STUFF((SELECT  ',' + QUOTENAME(MAD.AttributeID) \r\n                                            FROM Matrix_Attribute_Dimension MAD  WHERE Dimension = 1 AND ProductParentID='" + productParentID + "'";
					if (!showAllComponents)
					{
						text += " AND AttributeID IN (SELECT Attribute1 FROM Product_Parent_Components PPC  WHERE PPC.ProductParentID = Mad.ProductParentID) ";
					}
					text = text + "   ORDER BY Mad.RowIndex  FOR XML PATH(''), TYPE\r\n                                            ).value('.', 'NVARCHAR(MAX)') \r\n                                        ,1,1,'') \r\n\r\n\r\n                                SET @query =  'SELECT DISTINCT 3 AS DimensionCount, MAD3.DimensionID AS DimensionID3,P.DimensionID AS DimensionID2, MAD3.AttributeID AS ATR3, P.ATR2, ' + @cols + 'from \r\n                                       (SELECT MAD.ProductParentID,MAD.DimensionID,RowIndex, MAD.AttributeID,MAD.AttributeID AS ATR2,\r\n\t\t                                 0 as qty\r\n                                FROM Matrix_Attribute_Dimension MAD  WHERE MAD.ProductParentID = ''" + productParentID + "'' AND MAD.Dimension=2 ";
					if (!showAllComponents)
					{
						text += " AND AttributeID IN (SELECT Attribute2 FROM Product_Parent_Components PPC  WHERE PPC.ProductParentID = Mad.ProductParentID AND Dimension = 2) ";
					}
					text += ") D\r\n                                PIVOT (Max(qty) FOR  AttributeID IN('  + @cols + ')) AS P\r\n                                INNER JOIN Matrix_Attribute_Dimension MAD3 ON MAD3.ProductParentID = P.ProductParentID AND Dimension=3";
					if (!showAllComponents)
					{
						text += "AND AttributeID IN (SELECT Attribute3 FROM Product_Parent_Components PPC  WHERE PPC.ProductParentID = Mad3.ProductParentID AND Dimension=3 AND P.ATR2 = Attribute2)";
					}
					text += " ORDER BY MAD3.RowIndex,P.RowIndex' \r\n\r\n                                execute (@query)";
				}
				break;
			}
			FillDataSet(dataSet, "Matrix_Attribute_Dimension", text);
			if (dataSet != null && dataSet.Tables.Count > 0)
			{
				switch (result)
				{
				case 1:
					dataSet.Tables[0].Columns["AttributeID"].ColumnName = dataSet.Tables[0].Rows[0]["DimensionID"].ToString();
					dataSet.Tables[0].Columns.Remove("DimensionID");
					break;
				case 2:
					if (flag)
					{
						dataSet.Tables[0].Columns["ATR1"].ColumnName = dataSet.Tables[0].Rows[0]["DimensionID1"].ToString();
						dataSet.Tables[0].Columns.Remove("DimensionID1");
					}
					else
					{
						dataSet.Tables[0].Columns["ATR2"].ColumnName = dataSet.Tables[0].Rows[0]["DimensionID2"].ToString();
						dataSet.Tables[0].Columns.Remove("DimensionID2");
					}
					break;
				case 3:
					if (flag)
					{
						dataSet.Tables[0].Columns["ATR3"].ColumnName = dataSet.Tables[0].Rows[0]["DimensionID3"].ToString();
						dataSet.Tables[0].Columns["ATR1"].ColumnName = dataSet.Tables[0].Rows[0]["DimensionID1"].ToString();
						dataSet.Tables[0].Columns.Remove("DimensionID1");
						dataSet.Tables[0].Columns.Remove("DimensionID3");
					}
					else
					{
						dataSet.Tables[0].Columns["ATR3"].ColumnName = dataSet.Tables[0].Rows[0]["DimensionID3"].ToString();
						dataSet.Tables[0].Columns["ATR2"].ColumnName = dataSet.Tables[0].Rows[0]["DimensionID2"].ToString();
						dataSet.Tables[0].Columns.Remove("DimensionID2");
						dataSet.Tables[0].Columns.Remove("DimensionID3");
					}
					break;
				}
			}
			text = " SELECT DISTINCT PPC.*,PPC.Attribute1 AS AtrName1, PPC.Attribute2 AS AtrName2,PPC.Attribute3 AS AtrName3,P.Description,ISNULL(P.Quantity,0) AS Quantity,ISNULL(P.UnitPrice1,0) AS UnitPrice1,\r\n                        ISNULL(P.UnitPrice2,0) AS UnitPrice2, ISNULL(P.UnitPrice3,0) AS UnitPrice3 ,ISNULL(P.MinPrice,0) AS MinPrice, ISNULL(P.LastCost,0) AS LastCost\r\n                           \r\n                            FROM Product_Parent_Components PPC \r\n                             INNER JOIN Product P ON P.ProductID= PPC.ProductID\r\n                            WHERE PPC.ProductParentID = '" + productParentID + "' ORDER BY Attribute1";
			FillDataSet(dataSet, "Product_Parent_Components", text);
			return dataSet;
		}

		public DataSet GetMatrixQuantityTable(string productParentID, bool showAllComponents)
		{
			DataSet matrixTable = GetMatrixTable(productParentID, showAllComponents);
			bool flag = true;
			int num = 0;
			if (matrixTable != null && matrixTable.Tables.Count > 0)
			{
				if (!matrixTable.Tables[0].Columns.Contains("DimensionCount"))
				{
					return null;
				}
				num = int.Parse(matrixTable.Tables[0].Rows[0]["DimensionCount"].ToString());
			}
			DataTable dataTable = matrixTable.Tables[0];
			DataTable dataTable2 = matrixTable.Tables["Product_Parent_Components"];
			string text = "";
			if (num == 2 || num == 3)
			{
				foreach (DataRow row in dataTable.Rows)
				{
					foreach (DataColumn column in dataTable.Columns)
					{
						text = ((!flag) ? ("AtrName1= '" + column.ColumnName + "' ") : ("AtrName2= '" + column.ColumnName + "' "));
						switch (num)
						{
						case 2:
							text = ((!flag) ? (text + " AND AtrName2 = '" + row[1].ToString() + "' ") : (text + " AND AtrName1 = '" + row[1].ToString() + "' "));
							break;
						case 3:
							text = text + " AND AtrName2 = '" + row[2].ToString() + "' AND AtrName3 = '" + row[1].ToString() + "'";
							break;
						}
						DataRow[] array = dataTable2.Select(text);
						if (array.Length != 0)
						{
							decimal result = default(decimal);
							decimal.TryParse(array[0]["Quantity"].ToString(), out result);
							if (result != 0m)
							{
								row[column] = result;
							}
						}
					}
				}
				return matrixTable;
			}
			foreach (DataRow row2 in dataTable.Rows)
			{
				text = " AtrName1 = '" + row2[1].ToString() + "' ";
				DataRow[] array2 = dataTable2.Select(text);
				if (array2.Length != 0)
				{
					row2["Qty"] = array2[0]["Quantity"];
				}
			}
			return matrixTable;
		}

		public bool AddProductPhoto(string productParentID, byte[] image)
		{
			bool result = true;
			try
			{
				SqlTransaction transaction = base.DBConfig.StartNewTransaction();
				SqlCommand sqlCommand = new SqlCommand("Update Product_Parent SET Photo=@Photo WHERE ProductParentID='" + productParentID + "'");
				sqlCommand.Parameters.AddWithValue("@Photo", image);
				sqlCommand.Transaction = transaction;
				result = (ExecuteNonQuery(sqlCommand) > 0);
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

		public bool RemoveProductPhoto(string productParentID)
		{
			bool result = true;
			try
			{
				SqlTransaction transaction = base.DBConfig.StartNewTransaction();
				SqlCommand sqlCommand = new SqlCommand("Update Product_Parent SET Photo= Null WHERE ProductParentID='" + productParentID + "'");
				sqlCommand.Transaction = transaction;
				result = (ExecuteNonQuery(sqlCommand) > 0);
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

		public bool RemoveProductFromMatrix(string matrixID, string productID)
		{
			bool result = true;
			try
			{
				SqlTransaction sqlTransaction = base.DBConfig.StartNewTransaction();
				string exp = "DELETE FROM Product_Parent_Components WHERE ProductParentID = '" + matrixID + "' AND ProductID = '" + productID + "'";
				result = (ExecuteNonQuery(exp, sqlTransaction) > 0);
				exp = "UPDATE Product SET MatrixParentID = NULL WHERE ProductID = '" + productID + "'";
				result = (ExecuteNonQuery(exp, sqlTransaction) > 0);
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

		public bool AddProductToMatrix(string matrixID, string productID, string attribute1, string attribute2, string attribute3)
		{
			bool flag = true;
			try
			{
				SqlTransaction sqlTransaction = base.DBConfig.StartNewTransaction();
				SqlCommand sqlCommand = new SqlCommand();
				sqlCommand.CommandText = "INSERT INTO Product_Parent_Components  (ProductParentID, ProductID,Attribute1,Attribute2,Attribute3) VALUES (@ProductParentID,@ProductID,@Attribute1,@Attribute2,@Attribute3)";
				sqlCommand.Parameters.AddWithValue("@ProductParentID", matrixID);
				sqlCommand.Parameters.AddWithValue("@ProductID", productID);
				sqlCommand.Parameters.AddWithValue("@Attribute1", attribute1);
				if (attribute2 != "")
				{
					sqlCommand.Parameters.AddWithValue("@Attribute2", attribute2);
				}
				else
				{
					sqlCommand.Parameters.AddWithValue("@Attribute2", DBNull.Value);
				}
				if (attribute3 != "")
				{
					sqlCommand.Parameters.AddWithValue("@Attribute3", attribute3);
				}
				else
				{
					sqlCommand.Parameters.AddWithValue("@Attribute3", DBNull.Value);
				}
				sqlCommand.Transaction = sqlTransaction;
				flag &= (ExecuteNonQuery(sqlCommand) > 0);
				string exp = "UPDATE Product SET MatrixParentID = '" + matrixID + "' WHERE ProductID = '" + productID + "'";
				flag = (ExecuteNonQuery(exp, sqlTransaction) > 0);
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

		public DataSet GetProductParentCatalogReport(string fromItem, string toItem, string fromCategory, string toCategory, bool isInactive, bool zeroQuantity, string fromManufacturer, string toManufacturer, string fromStyle, string toStyle, string fromOrigin, string toOrigin)
		{
			try
			{
				string text = "";
				DataSet dataSet = new DataSet();
				text = "SELECT   P.ProductParentID AS ProductID,Description,P.ItemType,CategoryName,\r\n                        P.QuantityPerUnit,NULL AS Weight,P.UnitID,UnitPrice1,UnitPrice2,UnitPrice3,MinPrice,\r\n                         Photo, BrandName,P.Origin, P.Note\r\n                        FROM Product_Parent P\r\n                        LEFT OUTER JOIN Product_Category PCA ON PCA.CategoryID=P.CategoryID\r\n                        LEFT OUTER JOIN Product_Brand PCB ON PCB.BrandID=P.BrandID                       \r\n                        WHERE 1=1 AND ISNULL(ExcludeFromCatalogue,'False') = 'False' ";
				if (fromItem != "")
				{
					text = text + " AND P.ProductParentID >= '" + fromItem + "' ";
				}
				if (toItem != "")
				{
					text = text + " AND P.ProductParentID <= '" + toItem + "' ";
				}
				if (fromCategory != "")
				{
					text = text + " AND P.ProductParentID IN (SELECT ProductParentID FROM Product_Parent WHERE CategoryID >= '" + fromCategory + "')";
				}
				if (toCategory != "")
				{
					text = text + " AND P.ProductParentID IN (SELECT ProductParentID FROM Product_Parent WHERE CategoryID <= '" + toCategory + "')";
				}
				if (fromManufacturer != "")
				{
					text = text + " AND P.ProductID IN (SELECT ProductID FROM Product WHERE ManufacturerID BETWEEN '" + fromManufacturer + "' AND '" + toManufacturer + "') ";
				}
				if (fromStyle != "")
				{
					text = text + " AND P.ProductID IN (SELECT ProductID FROM Product WHERE StyleID BETWEEN '" + fromStyle + "' AND '" + toStyle + "') ";
				}
				if (fromOrigin != "")
				{
					text = text + " AND P.ProductID IN (SELECT ProductID FROM Product WHERE Origin BETWEEN '" + fromOrigin + "' AND '" + toOrigin + "') ";
				}
				if (!isInactive)
				{
					text += " AND ISNULL(P.IsInactive,'False')='False' ";
				}
				text += " ORDER BY P.ProductParentID";
				FillDataSet(dataSet, "Product", text);
				return dataSet;
			}
			catch
			{
				throw;
			}
		}

		public byte[] GetProductThumbnailImage(string productParentID)
		{
			string exp = "SELECT Photo \r\n                           FROM Product_Parent WHERE ProductParentID='" + productParentID + "'";
			object obj = ExecuteScalar(exp);
			if (obj != null && obj.ToString() != "")
			{
				return (byte[])obj;
			}
			return null;
		}
	}
}
