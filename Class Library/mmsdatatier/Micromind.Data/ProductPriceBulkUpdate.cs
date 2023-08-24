using Micromind.Common;
using Micromind.Common.Data;
using Micromind.Common.Libraries;
using System;
using System.Data;
using System.Data.SqlClient;

namespace Micromind.Data
{
	public sealed class ProductPriceBulkUpdate : StoreObject
	{
		private const string PRODUCTPRICEBULKUPDATE_TABLE = "Product_Price_Bulk_Update";

		private const string PRODUCTPRICEBULKUPDATEDETAIL_TABLE = "Product_Price_Bulk_Update_Detail";

		private const string SYSDOCID_PARM = "@SysDocID";

		private const string VOUCHERID_PARM = "@VoucherID";

		private const string TRANSACTIONDATE_PARM = "@TransactionDate";

		private const string ROWINDEX_PARM = "@RowIndex";

		private const string STATUS_PARM = "@Status";

		private const string NOTE_PARM = "@Note";

		private const string ISVOID_PARM = "@IsVoid";

		private const string CREATEDBY_PARM = "@CreatedBy";

		private const string DATECREATED_PARM = "@DateCreated";

		private const string UPDATEDBY_PARM = "@UpdatedBy";

		private const string DATEUPDATED_PARM = "@DateUpdated";

		private const string PRODUCTID_PARM = "@ProductID";

		private const string DESCRIPTION_PARM = "@Description";

		private const string CATEGORYID_PARM = "@CategoryID";

		private const string LASTPURCHASEPRICE_PARM = "@LastPurchasePrice";

		private const string STANDARDPRICE_PARM = "@StandardPrice";

		private const string STANDARDPRICEPERCENT_PARM = "@StandardPricePercent";

		private const string STANDARDCOST_PARM = "@StandardCost";

		private const string WHOLESALEPRICE_PARM = "@WholesalePrice";

		private const string WHOLESALEPRICEPERCENT_PARM = "@WholesalePricePercent";

		private const string SPECIALPRICEPERCENT_PARM = "@SpecialPricePercent";

		private const string SPECIALPRICE_PARM = "@SpecialPrice";

		private const string MINIMUMPRICE_PARM = "@MinimumPrice";

		private const string MINIMUMPRICEPERCENT_PARM = "@MinimumPricePercent";

		public ProductPriceBulkUpdate(Config config)
			: base(config)
		{
		}

		private string GetInsertUpdateProductPriceBulkUpdateText(bool isUpdate)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddInsertUpdateParameters("Product_Price_Bulk_Update", new FieldValue("SysDocID", "@SysDocID", isUpdateConditionField: true), new FieldValue("VoucherID", "@VoucherID", isUpdateConditionField: true), new FieldValue("TransactionDate", "@TransactionDate"), new FieldValue("Status", "@Status"), new FieldValue("IsVoid", "@IsVoid"), new FieldValue("Note", "@Note"));
			if (isUpdate)
			{
				sqlBuilder.AddInsertUpdateParameters("Product_Price_Bulk_Update", new FieldValue("DateUpdated", "@DateUpdated", isUpdateConditionField: true, checkForNullValue: true));
			}
			if (isUpdate)
			{
				return sqlBuilder.GetUpdateExpression();
			}
			return sqlBuilder.GetInsertExpression();
		}

		private SqlCommand GetInsertUpdateProductPriceBulkUpdateCommand(bool isUpdate)
		{
			SqlParameterCollection parameters;
			if (isUpdate)
			{
				if (updateCommand != null)
				{
					updateCommand = null;
				}
				updateCommand = new SqlCommand(GetInsertUpdateProductPriceBulkUpdateText(isUpdate: true), base.DBConfig.Connection);
				updateCommand.CommandType = CommandType.Text;
				parameters = updateCommand.Parameters;
			}
			else
			{
				if (insertCommand != null)
				{
					insertCommand = null;
				}
				insertCommand = new SqlCommand(GetInsertUpdateProductPriceBulkUpdateText(isUpdate: false), base.DBConfig.Connection);
				insertCommand.CommandType = CommandType.Text;
				parameters = insertCommand.Parameters;
			}
			parameters.Add("@SysDocID", SqlDbType.NVarChar);
			parameters.Add("@VoucherID", SqlDbType.NVarChar);
			parameters.Add("@IsVoid", SqlDbType.Bit);
			parameters.Add("@TransactionDate", SqlDbType.DateTime);
			parameters.Add("@Status", SqlDbType.TinyInt);
			parameters.Add("@Note", SqlDbType.NVarChar);
			parameters["@SysDocID"].SourceColumn = "SysDocID";
			parameters["@VoucherID"].SourceColumn = "VoucherID";
			parameters["@TransactionDate"].SourceColumn = "TransactionDate";
			parameters["@Status"].SourceColumn = "Status";
			parameters["@IsVoid"].SourceColumn = "IsVoid";
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

		private string GetInsertUpdateProductPriceBulkUpdateDetailsText(bool isUpdate)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddInsertUpdateParameters("Product_Price_Bulk_Update_Detail", new FieldValue("SysDocID", "@SysDocID"), new FieldValue("VoucherID", "@VoucherID"), new FieldValue("ProductID", "@ProductID"), new FieldValue("RowIndex", "@RowIndex"), new FieldValue("CategoryID", "@CategoryID"), new FieldValue("LastPurchasePrice", "@LastPurchasePrice"), new FieldValue("StandardPrice", "@StandardPrice"), new FieldValue("StandardPricePercent", "@StandardPricePercent"), new FieldValue("WholesalePricePercent", "@WholesalePricePercent"), new FieldValue("WholesalePrice", "@WholesalePrice"), new FieldValue("SpecialPricePercent", "@SpecialPricePercent"), new FieldValue("SpecialPrice", "@SpecialPrice"), new FieldValue("MinimumPricePercent", "@MinimumPricePercent"), new FieldValue("MinimumPrice", "@MinimumPrice"), new FieldValue("StandardCost", "@StandardCost"), new FieldValue("Description", "@Description"));
			if (isUpdate)
			{
				return sqlBuilder.GetUpdateExpression();
			}
			return sqlBuilder.GetInsertExpression();
		}

		private SqlCommand GetInsertUpdateProductPriceBulkUpdateDetailsCommand(bool isUpdate)
		{
			SqlParameterCollection parameters;
			if (isUpdate)
			{
				if (updateCommand != null)
				{
					updateCommand = null;
				}
				updateCommand = new SqlCommand(GetInsertUpdateProductPriceBulkUpdateDetailsText(isUpdate: true), base.DBConfig.Connection);
				updateCommand.CommandType = CommandType.Text;
				parameters = updateCommand.Parameters;
			}
			else
			{
				if (insertCommand != null)
				{
					insertCommand = null;
				}
				insertCommand = new SqlCommand(GetInsertUpdateProductPriceBulkUpdateDetailsText(isUpdate: false), base.DBConfig.Connection);
				insertCommand.CommandType = CommandType.Text;
				parameters = insertCommand.Parameters;
			}
			parameters.Add("@SysDocID", SqlDbType.NVarChar);
			parameters.Add("@VoucherID", SqlDbType.NVarChar);
			parameters.Add("@ProductID", SqlDbType.NVarChar);
			parameters.Add("@RowIndex", SqlDbType.Int);
			parameters.Add("@Description", SqlDbType.NVarChar);
			parameters.Add("@CategoryID", SqlDbType.NVarChar);
			parameters.Add("@StandardPricePercent", SqlDbType.Real);
			parameters.Add("@StandardPrice", SqlDbType.Real);
			parameters.Add("@WholesalePricePercent", SqlDbType.Real);
			parameters.Add("@WholesalePrice", SqlDbType.Real);
			parameters.Add("@SpecialPricePercent", SqlDbType.Real);
			parameters.Add("@SpecialPrice", SqlDbType.Real);
			parameters.Add("@StandardCost", SqlDbType.Real);
			parameters.Add("@MinimumPricePercent", SqlDbType.Real);
			parameters.Add("@MinimumPrice", SqlDbType.Real);
			parameters.Add("@LastPurchasePrice", SqlDbType.Real);
			parameters["@SysDocID"].SourceColumn = "SysDocID";
			parameters["@VoucherID"].SourceColumn = "VoucherID";
			parameters["@ProductID"].SourceColumn = "ProductID";
			parameters["@CategoryID"].SourceColumn = "CategoryID";
			parameters["@RowIndex"].SourceColumn = "RowIndex";
			parameters["@Description"].SourceColumn = "Description";
			parameters["@StandardPricePercent"].SourceColumn = "StandardPricePercent";
			parameters["@StandardPrice"].SourceColumn = "StandardPrice";
			parameters["@WholesalePricePercent"].SourceColumn = "WholesalePricePercent";
			parameters["@WholesalePrice"].SourceColumn = "WholesalePrice";
			parameters["@SpecialPricePercent"].SourceColumn = "SpecialPricePercent";
			parameters["@SpecialPrice"].SourceColumn = "SpecialPrice";
			parameters["@MinimumPricePercent"].SourceColumn = "MinimumPricePercent";
			parameters["@MinimumPrice"].SourceColumn = "MinimumPrice";
			parameters["@LastPurchasePrice"].SourceColumn = "LastPurchasePrice";
			parameters["@StandardCost"].SourceColumn = "StandardCost";
			if (isUpdate)
			{
				return updateCommand;
			}
			return insertCommand;
		}

		private bool ValidateData(ProductPriceBulkUpdateData journalData)
		{
			return true;
		}

		public bool CanUpdate(string sysDocID, string voucherNumber, SqlTransaction sqlTransaction)
		{
			string exp = "Select Count(*) FROM Purchase_Quote_Detail POD\r\n                                WHERE SysdocID='" + sysDocID + "' AND VoucherID='" + voucherNumber + "' Having SUM(ISNULL(QuantityOrdered,0))>0";
			object obj = ExecuteScalar(exp, sqlTransaction);
			if (obj != null && obj.ToString() != "" && int.Parse(obj.ToString()) > 0)
			{
				return false;
			}
			return true;
		}

		public bool InsertUpdateProductPriceBulkUpdate(ProductPriceBulkUpdateData ProductPriceBulkUpdateData, bool isUpdate)
		{
			bool flag = true;
			SqlCommand insertUpdateProductPriceBulkUpdateCommand = GetInsertUpdateProductPriceBulkUpdateCommand(isUpdate);
			try
			{
				_ = new CompanyInformations(base.DBConfig).CurrencyDecimalPoints;
				DataRow dataRow = ProductPriceBulkUpdateData.ProductPriceBulkUpdateTable.Rows[0];
				SqlTransaction sqlTransaction = base.DBConfig.StartNewTransaction();
				string text = dataRow["VoucherID"].ToString();
				string sysDocID = dataRow["SysDocID"].ToString();
				if (isUpdate && !CanUpdate(sysDocID, text, sqlTransaction))
				{
					throw new CompanyException("Unable to update. Some of the items has been already ordered.", 1037);
				}
				if (!isUpdate && new SystemDocuments(base.DBConfig).ExistDocumentNumber("Product_Price_Bulk_Update", "VoucherID", dataRow["SysDocID"].ToString(), text, sqlTransaction))
				{
					throw new CompanyException("Document number already exist.", 1046);
				}
				foreach (DataRow row in ProductPriceBulkUpdateData.ProductPriceBulkUpdateDetailTable.Rows)
				{
					row["SysDocID"] = dataRow["SysDocID"];
					row["VoucherID"] = dataRow["VoucherID"];
				}
				insertUpdateProductPriceBulkUpdateCommand.Transaction = sqlTransaction;
				flag = (isUpdate ? (flag & Update(ProductPriceBulkUpdateData, "Product_Price_Bulk_Update", insertUpdateProductPriceBulkUpdateCommand)) : (flag & Insert(ProductPriceBulkUpdateData, "Product_Price_Bulk_Update", insertUpdateProductPriceBulkUpdateCommand)));
				insertUpdateProductPriceBulkUpdateCommand = GetInsertUpdateProductPriceBulkUpdateDetailsCommand(isUpdate: false);
				insertUpdateProductPriceBulkUpdateCommand.Transaction = sqlTransaction;
				if (isUpdate)
				{
					flag &= DeleteProductPriceBulkUpdateDetailsRows(sysDocID, text, sqlTransaction);
				}
				if (ProductPriceBulkUpdateData.Tables["Product_Price_Bulk_Update_Detail"].Rows.Count > 0)
				{
					flag &= Insert(ProductPriceBulkUpdateData, "Product_Price_Bulk_Update_Detail", insertUpdateProductPriceBulkUpdateCommand);
					flag &= new Products(base.DBConfig).UpdateProductCost(ProductPriceBulkUpdateData, sqlTransaction);
				}
				if (!flag)
				{
					throw new Exception("Failed to save the transaction because of an unexpected error. Please try again.");
				}
				flag &= UpdateTableRowInsertUpdateInfo("Product_Price_Bulk_Update", "SysDocID", dataRow["SysDocID"].ToString(), "VoucherID", dataRow["VoucherID"].ToString(), sqlTransaction, !isUpdate);
				string entityName = "Product Price Bulk Update";
				flag = (isUpdate ? (flag & AddActivityLog(entityName, text, sysDocID, ActivityTypes.Update, sqlTransaction)) : (flag & AddActivityLog(entityName, text, sysDocID, ActivityTypes.Add, sqlTransaction)));
				if (!isUpdate)
				{
					flag &= new SystemDocuments(base.DBConfig).UpdateNextDocumentNumber(dataRow["SysDocID"].ToString(), dataRow["VoucherID"].ToString(), "Product_Price_Bulk_Update", "VoucherID", sqlTransaction);
				}
				if (!flag)
				{
					return flag;
				}
				new Approval(base.DBConfig).CreateTransactionApprovalTasks(SysDocTypes.ProductPriceBulkUpdate, sysDocID, text, "Product_Price_Bulk_Update", sqlTransaction);
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

		public ProductPriceBulkUpdateData GetProductPriceBulkUpdateByID(string sysDocID, string voucherID)
		{
			try
			{
				ProductPriceBulkUpdateData productPriceBulkUpdateData = new ProductPriceBulkUpdateData();
				string textCommand = "SELECT * FROM Product_Price_Bulk_Update WHERE VoucherID='" + voucherID + "' AND SysDocID='" + sysDocID + "'";
				FillDataSet(productPriceBulkUpdateData, "Product_Price_Bulk_Update", textCommand);
				if (productPriceBulkUpdateData == null || productPriceBulkUpdateData.Tables.Count == 0 || productPriceBulkUpdateData.Tables["Product_Price_Bulk_Update"].Rows.Count == 0)
				{
					return null;
				}
				textCommand = "SELECT TD.*,Product.Description,Product.ItemType,Product.Attribute1,Product.Attribute2,Product.Attribute3,Product.MatrixParentID\r\n                        FROM Product_Price_Bulk_Update_detail TD INNER JOIN Product ON TD.ProductID=Product.ProductID\r\n                        WHERE VoucherID='" + voucherID + "' AND SysDocID='" + sysDocID + "' ORDER BY TD.RowIndex ";
				FillDataSet(productPriceBulkUpdateData, "Product_Price_Bulk_Update_Detail", textCommand);
				return productPriceBulkUpdateData;
			}
			catch
			{
				throw;
			}
		}

		internal bool DeleteProductPriceBulkUpdateDetailsRows(string sysDocID, string voucherID, SqlTransaction sqlTransaction)
		{
			bool flag = true;
			try
			{
				string commandText = "DELETE FROM Product_Price_Bulk_Update_Detail WHERE SysDocID = '" + sysDocID + "' AND VoucherID = '" + voucherID + "'";
				return Delete(commandText, sqlTransaction);
			}
			catch
			{
				throw;
			}
		}

		internal bool DeleteProductPriceBulkUpdateRows(string sysDocID, string voucherID, SqlTransaction sqlTransaction)
		{
			bool flag = true;
			try
			{
				string commandText = "DELETE FROM Product_Price_Bulk_Update WHERE SysDocID = '" + sysDocID + "' AND VoucherID = '" + voucherID + "'";
				return Delete(commandText, sqlTransaction);
			}
			catch
			{
				throw;
			}
		}

		public bool VoidProductPriceBulkUpdate(string sysDocID, string voucherID, bool isVoid)
		{
			bool flag = true;
			try
			{
				SqlTransaction sqlTransaction = base.DBConfig.StartNewTransaction();
				if (!CanUpdate(sysDocID, voucherID, sqlTransaction))
				{
					throw new CompanyException("Unable to void. Some of the items has been already ordered.", 1037);
				}
				string exp = "UPDATE Product_Price_Bulk_Update SET IsVoid = '" + isVoid.ToString() + "' WHERE SysDocID='" + sysDocID + "' AND VoucherID = '" + voucherID + "'";
				flag &= (ExecuteNonQuery(exp, sqlTransaction) > 0);
				if (!flag)
				{
					return flag;
				}
				ActivityTypes activityType = ActivityTypes.Void;
				if (!isVoid)
				{
					activityType = ActivityTypes.Unvoid;
				}
				AddActivityLog("Product Price Bulk Update", voucherID, sysDocID, activityType, sqlTransaction);
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

		public bool DeleteProductPriceBulkUpdate(string sysDocID, string voucherID)
		{
			bool flag = true;
			try
			{
				string text = "";
				SqlTransaction sqlTransaction = base.DBConfig.StartNewTransaction();
				if (!CanUpdate(sysDocID, voucherID, sqlTransaction))
				{
					throw new CompanyException("Unable to delete. Some of the items has been already ordered.", 1037);
				}
				flag &= DeleteProductPriceBulkUpdateDetailsRows(sysDocID, voucherID, sqlTransaction);
				text = "DELETE FROM Product_Price_Bulk_Update WHERE SysDocID='" + sysDocID + "' AND VoucherID = '" + voucherID + "'";
				flag &= (ExecuteNonQuery(text, sqlTransaction) >= 0);
				if (!flag)
				{
					return flag;
				}
				ActivityTypes activityType = ActivityTypes.Delete;
				AddActivityLog("Product Price Bulk Update", voucherID, sysDocID, activityType, sqlTransaction);
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

		public DataSet GetProductPriceBulkUpdateToPrint(string sysDocID, string[] voucherID, bool mergeMatrixItems)
		{
			try
			{
				string text = "";
				for (int i = 0; i < voucherID.Length; i++)
				{
					text = "'" + voucherID[i] + "'";
					if (i < voucherID.Length - 1)
					{
						text += ",";
					}
				}
				DataSet dataSet = new DataSet();
				string cmdText = "SELECT  DISTINCT   SysDocID,VoucherID, Note,TransactionDate from Product_Price_Bulk_Update\r\n                                 \r\n                                WHERE SysDocID = '" + sysDocID + "' AND VoucherID IN (" + text + ")";
				SqlCommand sqlCommand = new SqlCommand(cmdText);
				FillDataSet(dataSet, "Product_Price_Bulk_Update", sqlCommand);
				if (dataSet == null || dataSet.Tables.Count == 0 || dataSet.Tables["Product_Price_Bulk_Update"].Rows.Count == 0)
				{
					return null;
				}
				cmdText = "SELECT    * from Product_Price_Bulk_Update_Detail \r\n                        WHERE SysDocID='" + sysDocID + "' AND VoucherID IN (" + text + ")  ORDER BY RowIndex";
				FillDataSet(dataSet, "Product_Price_Bulk_Update_Detail", cmdText);
				dataSet.Relations.Add("ProductPriceBulkUpdate", new DataColumn[2]
				{
					dataSet.Tables["Product_Price_Bulk_Update"].Columns["SysDocID"],
					dataSet.Tables["Product_Price_Bulk_Update"].Columns["VoucherID"]
				}, new DataColumn[2]
				{
					dataSet.Tables["Product_Price_Bulk_Update_Detail"].Columns["SysDocID"],
					dataSet.Tables["Product_Price_Bulk_Update_Detail"].Columns["VoucherID"]
				}, createConstraints: false);
				return dataSet;
			}
			catch
			{
				throw;
			}
		}

		public DataSet GetList(DateTime from, DateTime to, bool showVoid)
		{
			DataSet dataSet = new DataSet();
			string text = StoreConfiguration.ToSqlDateTimeString(from);
			string text2 = StoreConfiguration.ToSqlDateTimeString(to);
			string text3 = "SELECT     ISNULL(IsVoid,'False') AS V,SysDocID [Doc ID],VoucherID [Doc Number],Note,TransactionDate [Date]\r\n                            \r\n                            FROM         Product_Price_Bulk_Update INV where 1=1\r\n                           ";
			if (from != DateTime.MinValue)
			{
				text3 = text3 + " AND TransactionDate Between '" + text + "' AND '" + text2 + "'";
			}
			if (!showVoid)
			{
				text3 += " AND ISNULL(IsVoid,'False')='False'";
			}
			SqlCommand sqlCommand = new SqlCommand(text3);
			FillDataSet(dataSet, "Product_Price_Bulk_Update", sqlCommand);
			return dataSet;
		}

		public DataSet GetOpenQuotesSummary(string vendorID, bool isImport)
		{
			try
			{
				DataSet dataSet = new DataSet();
				string text = "SELECT PQ.SysDocID [Doc ID],PQ.VoucherID [Number],TransactionDate AS [Date],PQ.VendorID + '-' + Ven.VendorName AS [Vendor],Reference,Total AS Amount  FROM Purchase_Quote PQ\r\n                             INNER JOIN Vendor Ven ON PQ.VendorID=Ven.VendorID  WHERE ISNULL(IsVoid,'False')='False' AND ISNULL(Status,1) = 1 ";
				if (vendorID != "")
				{
					text = text + " AND (PQ.VendorID='" + vendorID + "' OR ISNULL(Ven.ParentVendorID,'') = '" + vendorID + "') ";
				}
				text = text + " AND ISNULL(IsImport,'False') = '" + isImport.ToString() + "'";
				FillDataSet(dataSet, "Product_Price_Bulk_Update", text);
				return dataSet;
			}
			catch
			{
				throw;
			}
		}

		public DataSet GetPurchaseComparisonReport(string refNumber)
		{
			try
			{
				DataSet dataSet = new DataSet();
				string textCommand = "SELECT DISTINCT Reference FROM Purchase_Quote WHERE Reference = '" + refNumber + "'";
				FillDataSet(dataSet, "Reference_Quote", textCommand);
				textCommand = "SELECT PQ.VendorID,v.VendorName, PQ.Reference, PQD.ProductID, PQD.Quantity, PQD.VoucherID,\r\n\t\t\t\t  PQD.UnitPrice,(pqd.Quantity* PQD.UnitPrice) AS Total, PQD.Description\r\n\t\t                    FROM  Purchase_Quote PQ INNER JOIN Purchase_Quote_Detail PQD \r\n\t\t\t                ON PQ.SysDocID = PQD.SysDocID AND PQ.VoucherID = PQD.VoucherID\r\n\t\t\t\t\t\t\tLEFT JOIN VENDOR V ON PQ.VendorID=v.VendorID\r\n\t\t                    Where 1 = 1 AND PQ.Reference = '" + refNumber + "'";
				FillDataSet(dataSet, "Product_Price_Bulk_Update", textCommand);
				return dataSet;
			}
			catch
			{
				throw;
			}
		}

		internal bool UpdateRowOrderedQuantity(string sysDocID, string voucherID, int rowIndex, float quantity, SqlTransaction sqlTransaction)
		{
			new DataSet();
			return true;
		}

		internal bool CloseOrderedQuote(string sysDocID, string voucherID, SqlTransaction sqlTransaction)
		{
			try
			{
				string str = "SELECT COUNT(RowIndex)FROM Purchase_Quote_Detail WHERE SysDocID='" + sysDocID + "' AND VoucherID='" + voucherID + "'";
				str += " AND CASE WHEN UnitQuantity IS NULL THEN Quantity ELSE UnitQuantity END - ISNULL(QuantityOrdered,0) > 0";
				object obj = ExecuteScalar(str, sqlTransaction);
				if (obj == null)
				{
					return true;
				}
				bool flag = false;
				if (int.Parse(obj.ToString()) == 0)
				{
					flag = true;
				}
				if (flag)
				{
					str = "UPDATE Purchase_Quote SET Status = 2 WHERE SysDocID='" + sysDocID + "' AND VoucherID='" + voucherID + "'";
					return ExecuteNonQuery(str, sqlTransaction) > 0;
				}
				str = "UPDATE Purchase_Quote SET Status = 1 WHERE SysDocID='" + sysDocID + "' AND VoucherID='" + voucherID + "'";
				return ExecuteNonQuery(str, sqlTransaction) > 0;
			}
			catch
			{
				return false;
			}
		}

		public DataSet GettProductPriceBulkUpdateList()
		{
			DataSet dataSet = new DataSet();
			string textCommand = "SELECT PQ.Reference ,pq.VendorID,pq.VoucherID ,pq.Discount, pq.Total ,pq.DueDate,pq.TransactionDate\r\n                            FROM [Purchase_Quote] PQ\r\n                             WHERE ISNULL(IsVoid,'False')='False' ";
			FillDataSet(dataSet, "Product_Price_Bulk_Update", textCommand);
			return dataSet;
		}
	}
}
