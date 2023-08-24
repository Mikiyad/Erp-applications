using Micromind.Common;
using Micromind.Common.Data;
using Micromind.Common.Libraries;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace Micromind.Data
{
	public sealed class SalesForecasting : StoreObject
	{
		private const string SALESFORECASTING_TABLE = "Sales_Forecasting";

		private const string SALESFORECASTINGDETAIL_TABLE = "Sales_Forecasting_Detail";

		private const string SYSDOCID_PARM = "@SysDocID";

		private const string VOUCHERID_PARM = "@VoucherID";

		private const string TRANSACTIONDATE_PARM = "@FromDate";

		private const string FROMDATE_PARM = "@ToDate";

		private const string TODATE_PARM = "@TransactionDate";

		private const string ROWINDEX_PARM = "@RowIndex";

		private const string PERIOD_PARM = "@Period";

		private const string STATUS_PARM = "@Status";

		private const string NOTE_PARM = "@Note";

		private const string ISVOID_PARM = "@IsVoid";

		private const string LOCATIONFROM_PARM = "@LocationFrom";

		private const string CALCULATIONMETHOD_PARM = "@CalculationMethod";

		private const string CREATEDBY_PARM = "CreatedBy";

		private const string DATECREATED_PARM = "DateCreated";

		private const string UPDATEDBY_PARM = "UpdatedBy";

		private const string DATEUPDATED_PARM = "DateUpdated";

		private const string PRODUCTID_PARM = "@ProductID";

		private const string DESCRIPTION_PARM = "@Description";

		private const string QUANTITY_PARM = "@Quantity";

		private const string UNIT_PARM = "@UnitID";

		private const string ROWSOURCE_PARM = "@RowSource";

		private const string SOURCEVOUCHERID_PARM = "@SourceVoucherID";

		private const string SOURCESYSDOCID_PARM = "@SourceSysDocID";

		private const string SOURCEROWINDEX_PARM = "@SourceRowIndex";

		public SalesForecasting(Config config)
			: base(config)
		{
		}

		private string GetInsertUpdateSalesForecastingText(bool isUpdate)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddInsertUpdateParameters("Sales_Forecasting", new FieldValue("SysDocID", "@SysDocID", isUpdateConditionField: true), new FieldValue("VoucherID", "@VoucherID", isUpdateConditionField: true), new FieldValue("TransactionDate", "@FromDate"), new FieldValue("FromDate", "@ToDate"), new FieldValue("ToDate", "@TransactionDate"), new FieldValue("CalculationMethod", "@CalculationMethod"), new FieldValue("Status", "@Status"), new FieldValue("Period", "@Period"), new FieldValue("Note", "@Note"), new FieldValue("LocationFrom", "@LocationFrom"));
			if (isUpdate)
			{
				sqlBuilder.AddInsertUpdateParameters("Sales_Forecasting", new FieldValue("DateUpdated", "DateUpdated", isUpdateConditionField: true, checkForNullValue: true));
			}
			if (isUpdate)
			{
				return sqlBuilder.GetUpdateExpression();
			}
			return sqlBuilder.GetInsertExpression();
		}

		private SqlCommand GetInsertUpdateSalesForecastingCommand(bool isUpdate)
		{
			SqlParameterCollection parameters;
			if (isUpdate)
			{
				if (updateCommand != null)
				{
					updateCommand = null;
				}
				updateCommand = new SqlCommand(GetInsertUpdateSalesForecastingText(isUpdate: true), base.DBConfig.Connection);
				updateCommand.CommandType = CommandType.Text;
				parameters = updateCommand.Parameters;
			}
			else
			{
				if (insertCommand != null)
				{
					insertCommand = null;
				}
				insertCommand = new SqlCommand(GetInsertUpdateSalesForecastingText(isUpdate: false), base.DBConfig.Connection);
				insertCommand.CommandType = CommandType.Text;
				parameters = insertCommand.Parameters;
			}
			parameters.Add("@SysDocID", SqlDbType.NVarChar);
			parameters.Add("@VoucherID", SqlDbType.NVarChar);
			parameters.Add("@FromDate", SqlDbType.DateTime);
			parameters.Add("@ToDate", SqlDbType.DateTime);
			parameters.Add("@TransactionDate", SqlDbType.DateTime);
			parameters.Add("@CalculationMethod", SqlDbType.TinyInt);
			parameters.Add("@Period", SqlDbType.TinyInt);
			parameters.Add("@Status", SqlDbType.TinyInt);
			parameters.Add("@Note", SqlDbType.NVarChar);
			parameters.Add("@LocationFrom", SqlDbType.NVarChar);
			parameters["@SysDocID"].SourceColumn = "SysDocID";
			parameters["@VoucherID"].SourceColumn = "VoucherID";
			parameters["@FromDate"].SourceColumn = "TransactionDate";
			parameters["@ToDate"].SourceColumn = "FromDate";
			parameters["@TransactionDate"].SourceColumn = "ToDate";
			parameters["@CalculationMethod"].SourceColumn = "CalculationMethod";
			parameters["@Period"].SourceColumn = "Period";
			parameters["@Status"].SourceColumn = "Status";
			parameters["@Note"].SourceColumn = "Note";
			parameters["@LocationFrom"].SourceColumn = "LocationFrom";
			if (isUpdate)
			{
				parameters.Add("DateUpdated", SqlDbType.DateTime);
				parameters["DateUpdated"].SourceColumn = "DateUpdated";
			}
			if (isUpdate)
			{
				return updateCommand;
			}
			return insertCommand;
		}

		private string GetInsertUpdateSalesForecastingDetailsText(bool isUpdate)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddInsertUpdateParameters("Sales_Forecasting_Detail", new FieldValue("SysDocID", "@SysDocID"), new FieldValue("VoucherID", "@VoucherID"), new FieldValue("ProductID", "@ProductID"), new FieldValue("RowIndex", "@RowIndex"), new FieldValue("RowSource", "@RowSource"), new FieldValue("SourceSysDocID", "@SourceSysDocID"), new FieldValue("SourceVoucherID", "@SourceVoucherID"), new FieldValue("SourceRowIndex", "@SourceRowIndex"), new FieldValue("Quantity", "@Quantity"), new FieldValue("UnitID", "@UnitID"), new FieldValue("Description", "@Description"));
			if (isUpdate)
			{
				return sqlBuilder.GetUpdateExpression();
			}
			return sqlBuilder.GetInsertExpression();
		}

		private SqlCommand GetInsertUpdateSalesForecastingDetailsCommand(bool isUpdate)
		{
			SqlParameterCollection parameters;
			if (isUpdate)
			{
				if (updateCommand != null)
				{
					updateCommand = null;
				}
				updateCommand = new SqlCommand(GetInsertUpdateSalesForecastingDetailsText(isUpdate: true), base.DBConfig.Connection);
				updateCommand.CommandType = CommandType.Text;
				parameters = updateCommand.Parameters;
			}
			else
			{
				if (insertCommand != null)
				{
					insertCommand = null;
				}
				insertCommand = new SqlCommand(GetInsertUpdateSalesForecastingDetailsText(isUpdate: false), base.DBConfig.Connection);
				insertCommand.CommandType = CommandType.Text;
				parameters = insertCommand.Parameters;
			}
			parameters.Add("@SysDocID", SqlDbType.NVarChar);
			parameters.Add("@VoucherID", SqlDbType.NVarChar);
			parameters.Add("@ProductID", SqlDbType.NVarChar);
			parameters.Add("@RowIndex", SqlDbType.Int);
			parameters.Add("@RowSource", SqlDbType.Int);
			parameters.Add("@SourceSysDocID", SqlDbType.NVarChar);
			parameters.Add("@SourceVoucherID", SqlDbType.NVarChar);
			parameters.Add("@SourceRowIndex", SqlDbType.Int);
			parameters.Add("@Quantity", SqlDbType.Real);
			parameters.Add("@UnitID", SqlDbType.NVarChar);
			parameters.Add("@Description", SqlDbType.NVarChar);
			parameters["@SysDocID"].SourceColumn = "SysDocID";
			parameters["@VoucherID"].SourceColumn = "VoucherID";
			parameters["@ProductID"].SourceColumn = "ProductID";
			parameters["@RowIndex"].SourceColumn = "RowIndex";
			parameters["@SourceVoucherID"].SourceColumn = "SourceVoucherID";
			parameters["@SourceSysDocID"].SourceColumn = "SourceSysDocID";
			parameters["@SourceRowIndex"].SourceColumn = "SourceRowIndex";
			parameters["@RowSource"].SourceColumn = "RowSource";
			parameters["@Quantity"].SourceColumn = "Quantity";
			parameters["@UnitID"].SourceColumn = "UnitID";
			parameters["@Description"].SourceColumn = "Description";
			if (isUpdate)
			{
				return updateCommand;
			}
			return insertCommand;
		}

		private bool ValidateData(SalesForecastingData journalData)
		{
			return true;
		}

		public bool InsertUpdateSalesForecasting(SalesForecastingData SalesForecastingData, bool isUpdate)
		{
			bool flag = true;
			SqlCommand insertUpdateSalesForecastingCommand = GetInsertUpdateSalesForecastingCommand(isUpdate);
			try
			{
				DataRow dataRow = SalesForecastingData.SalesForecastingTable.Rows[0];
				_ = new CompanyInformations(base.DBConfig).CurrencyDecimalPoints;
				SqlTransaction sqlTransaction = base.DBConfig.StartNewTransaction();
				string text = dataRow["VoucherID"].ToString();
				string sysDocID = dataRow["SysDocID"].ToString();
				if (!isUpdate && new SystemDocuments(base.DBConfig).ExistDocumentNumber("Sales_Forecasting", "VoucherID", dataRow["SysDocID"].ToString(), text, sqlTransaction))
				{
					throw new CompanyException("Document number already exist.", 1046);
				}
				foreach (DataRow row in SalesForecastingData.SalesForecastingDetailTable.Rows)
				{
					row["SysDocID"] = dataRow["SysDocID"];
					row["VoucherID"] = dataRow["VoucherID"];
					row["ProductID"].ToString();
				}
				insertUpdateSalesForecastingCommand.Transaction = sqlTransaction;
				flag = (isUpdate ? (flag & Update(SalesForecastingData, "Sales_Forecasting", insertUpdateSalesForecastingCommand)) : (flag & Insert(SalesForecastingData, "Sales_Forecasting", insertUpdateSalesForecastingCommand)));
				insertUpdateSalesForecastingCommand = GetInsertUpdateSalesForecastingDetailsCommand(isUpdate: false);
				insertUpdateSalesForecastingCommand.Transaction = sqlTransaction;
				if (isUpdate)
				{
					flag &= DeleteSalesForecastingDetailsRows(sysDocID, text, sqlTransaction);
				}
				if (SalesForecastingData.Tables["Sales_Forecasting_Detail"].Rows.Count > 0)
				{
					flag &= Insert(SalesForecastingData, "Sales_Forecasting_Detail", insertUpdateSalesForecastingCommand);
				}
				if (!flag)
				{
					throw new Exception("Failed to save the transaction because of an unexpected error. Please try again.");
				}
				flag &= UpdateTableRowInsertUpdateInfo("Sales_Forecasting", "SysDocID", dataRow["SysDocID"].ToString(), "VoucherID", dataRow["VoucherID"].ToString(), sqlTransaction, !isUpdate);
				string entityName = "Sales Forecasting";
				flag = (isUpdate ? (flag & AddActivityLog(entityName, text, sysDocID, ActivityTypes.Update, sqlTransaction)) : (flag & AddActivityLog(entityName, text, sysDocID, ActivityTypes.Add, sqlTransaction)));
				if (!isUpdate)
				{
					flag &= new SystemDocuments(base.DBConfig).UpdateNextDocumentNumber(dataRow["SysDocID"].ToString(), dataRow["VoucherID"].ToString(), "Sales_Forecasting", "VoucherID", sqlTransaction);
				}
				if (!flag)
				{
					return flag;
				}
				new Approval(base.DBConfig).CreateTransactionApprovalTasks(SysDocTypes.SalesForecasting, sysDocID, text, "Sales_Forecasting", sqlTransaction);
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

		public SalesForecastingData GetSalesForecastingByID(string sysDocID, string voucherID)
		{
			try
			{
				SalesForecastingData salesForecastingData = new SalesForecastingData();
				string textCommand = "SELECT * FROM Sales_Forecasting WHERE VoucherID='" + voucherID + "' AND SysDocID='" + sysDocID + "'";
				FillDataSet(salesForecastingData, "Sales_Forecasting", textCommand);
				if (salesForecastingData == null || salesForecastingData.Tables.Count == 0 || salesForecastingData.Tables["Sales_Forecasting"].Rows.Count == 0)
				{
					return null;
				}
				textCommand = "SELECT TD.*,Product.Description,Product.ItemType,Product.Attribute1,Product.Attribute2,Product.Attribute3,Product.MatrixParentID\r\n                        FROM Sales_Forecasting_Detail TD INNER JOIN Product ON TD.ProductID=Product.ProductID\r\n                        WHERE VoucherID='" + voucherID + "' AND SysDocID='" + sysDocID + "' ORDER BY TD.RowIndex ";
				FillDataSet(salesForecastingData, "Sales_Forecasting_Detail", textCommand);
				return salesForecastingData;
			}
			catch
			{
				throw;
			}
		}

		public DataSet GetOpenOrdersSummary(string customerID, bool isExport)
		{
			try
			{
				DataSet dataSet = new DataSet();
				string text = "SELECT SO.SysDocID [Doc ID],SO.VoucherID [Number],TransactionDate AS [Date],SO.CustomerID + '-' + C.CustomerName AS [Customer] FROM Sales_Forecasting SO\r\n                             INNER JOIN Customer C ON SO.CustomerID=C.CustomerID  WHERE ISNULL(IsVoid,'False')='False' AND ISNULL(IsExport,'False')= '" + isExport.ToString() + "' AND Status=1 ";
				if (customerID != "")
				{
					text = text + " AND SO.CustomerID='" + customerID + "'";
				}
				FillDataSet(dataSet, "Sales_Forecasting", text);
				return dataSet;
			}
			catch
			{
				throw;
			}
		}

		internal bool UpdateRowShippedQuantity(string sysDocID, string voucherID, int rowIndex, float quantity, SqlTransaction sqlTransaction)
		{
			return true;
		}

		internal bool CloseShippedOrder(string sysDocID, string voucherID, SqlTransaction sqlTransaction)
		{
			try
			{
				string exp = "SELECT COUNT(RowIndex)FROM Sales_Enquiry_Detail SOD WHERE SysDocID='" + sysDocID + "' AND VoucherID='" + voucherID + "'\r\n                                AND (SELECT SUM((CASE WHEN UnitQuantity IS NULL \r\n                                THEN Quantity ELSE UnitQuantity END) ) FROM Sales_Enquiry_Detail SOD2 WHERE SOD.SysDocID=SOD2.SysDocID AND SOD.VoucherID=SOD2.VoucherID)-\r\n                                 (SELECT SUM(ISNULL(QuantityShipped,0) ) FROM Sales_Enquiry_Detail SOD3 WHERE SOD.SysDocID=SOD3.SysDocID AND SOD.VoucherID=SOD3.VoucherID) <= 0";
				object obj = ExecuteScalar(exp, sqlTransaction);
				if (obj == null || int.Parse(obj.ToString()) == 0)
				{
					return true;
				}
				exp = "UPDATE Sales_Enquiry SET Status= 2 WHERE SysDocID='" + sysDocID + "' AND VoucherID='" + voucherID + "'";
				return ExecuteNonQuery(exp, sqlTransaction) > 0;
			}
			catch
			{
				return false;
			}
		}

		internal bool ReOpenOrder(string sysDocID, string voucherID, SqlTransaction sqlTransaction)
		{
			try
			{
				string exp = "SELECT COUNT(RowIndex)FROM Sales_Enquiry_Detail SOD WHERE SysDocID='" + sysDocID + "' AND VoucherID='" + voucherID + "'\r\n                                AND (SELECT SUM((CASE WHEN UnitQuantity IS NULL \r\n                                THEN Quantity ELSE UnitQuantity END) ) FROM Sales_Enquiry_Detail SOD2 WHERE SOD.SysDocID=SOD2.SysDocID AND SOD.VoucherID=SOD2.VoucherID)-\r\n                                 (SELECT SUM(ISNULL(QuantityShipped,0) ) FROM Sales_Enquiry_Detail SOD3 WHERE SOD.SysDocID=SOD3.SysDocID AND SOD.VoucherID=SOD3.VoucherID) <= 0";
				object obj = ExecuteScalar(exp, sqlTransaction);
				if (obj == null || int.Parse(obj.ToString()) > 0)
				{
					return true;
				}
				exp = "UPDATE Sales_Enquiry SET Status= 1 WHERE SysDocID='" + sysDocID + "' AND VoucherID='" + voucherID + "'";
				return ExecuteNonQuery(exp, sqlTransaction) > 0;
			}
			catch
			{
				return false;
			}
		}

		internal bool DeleteSalesForecastingDetailsRows(string sysDocID, string voucherID, SqlTransaction sqlTransaction)
		{
			bool flag = true;
			try
			{
				SalesForecastingData dataSet = new SalesForecastingData();
				string textCommand = "SELECT SOD.*,ISVOID FROM Sales_Forecasting_Detail SOD INNER JOIN Sales_Enquiry SO ON SO.SysDocID=SOD.SysDocID AND SO.VOucherID=SOD.VoucherID\r\n                              WHERE SOD.SysDocID = '" + sysDocID + "' AND SOD.VoucherID = '" + voucherID + "'";
				FillDataSet(dataSet, "Sales_Forecasting_Detail", textCommand, sqlTransaction);
				textCommand = "DELETE FROM Sales_Forecasting_Detail WHERE SysDocID = '" + sysDocID + "' AND VoucherID = '" + voucherID + "'";
				return flag & Delete(textCommand, sqlTransaction);
			}
			catch
			{
				throw;
			}
		}

		public bool VoidSalesForecasting(string sysDocID, string voucherID, bool isVoid)
		{
			bool flag = true;
			try
			{
				SqlTransaction sqlTransaction = base.DBConfig.StartNewTransaction();
				SalesForecastingData dataSet = new SalesForecastingData();
				string textCommand = "SELECT * FROM Sales_Forecasting_Detail\r\n                              WHERE SysDocID = '" + sysDocID + "' AND VoucherID = '" + voucherID + "'";
				FillDataSet(dataSet, "Sales_Enquiry_Detail", textCommand, sqlTransaction);
				textCommand = "UPDATE Sales_Forecasting SET IsVoid = '" + isVoid.ToString() + "' WHERE SysDocID='" + sysDocID + "' AND VoucherID = '" + voucherID + "'";
				flag &= (ExecuteNonQuery(textCommand, sqlTransaction) > 0);
				if (!flag)
				{
					return flag;
				}
				ActivityTypes activityType = ActivityTypes.Void;
				if (!isVoid)
				{
					activityType = ActivityTypes.Unvoid;
				}
				AddActivityLog("Sales Forecasting", voucherID, sysDocID, activityType, sqlTransaction);
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

		public bool DeleteSalesForecasting(string sysDocID, string voucherID)
		{
			bool flag = true;
			try
			{
				string text = "";
				SqlTransaction sqlTransaction = base.DBConfig.StartNewTransaction();
				flag &= DeleteSalesForecastingDetailsRows(sysDocID, voucherID, sqlTransaction);
				text = "DELETE FROM Sales_Forecasting WHERE SysDocID='" + sysDocID + "' AND VoucherID = '" + voucherID + "'";
				flag &= (ExecuteNonQuery(text, sqlTransaction) >= 0);
				if (!flag)
				{
					return flag;
				}
				ActivityTypes activityType = ActivityTypes.Delete;
				AddActivityLog("Sales Forecasting", voucherID, sysDocID, activityType, sqlTransaction);
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

		public DataSet GetOpenOrderListReport()
		{
			DataSet dataSet = new DataSet();
			string textCommand = "SELECT SysDocID,VoucherID,SO.CustomerID,CustomerName,TransactionDate,SO.SalespersonID,Total\r\n                            FROM Sales_Enquiry SO INNER JOIN Customer ON SO.CustomerID=Customer.CustomerID\r\n                            WHERE ISNULL(IsVoid,'False')='False' AND Status=1";
			FillDataSet(dataSet, "Orders", textCommand);
			return dataSet;
		}

		public DataSet GetSalesForecastingToPrint(string sysDocID, string voucherID)
		{
			return GetSalesForecastingToPrint(sysDocID, new string[1]
			{
				voucherID
			});
		}

		public DataSet GetSalesForecastingToPrint(string sysDocID, string[] voucherID)
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
				string cmdText = "SELECT  * from Sales_Forecasting\r\n                                WHERE SysDocID = '" + sysDocID + "' AND VoucherID IN (" + text + ")";
				SqlCommand sqlCommand = new SqlCommand(cmdText);
				FillDataSet(dataSet, "Sales_Forecasting", sqlCommand);
				if (dataSet == null || dataSet.Tables.Count == 0 || dataSet.Tables["Sales_Forecasting"].Rows.Count == 0)
				{
					return null;
				}
				cmdText = "SELECT     * from Sales_Forecasting_Detail\r\n                        WHERE SysDocID='" + sysDocID + "' AND VoucherID IN (" + text + ") ORDER BY RowIndex";
				FillDataSet(dataSet, "Sales_Forecasting_Detail", cmdText);
				List<Tuple<string, decimal>> list = new List<Tuple<string, decimal>>();
				string text2 = "";
				string text3 = "";
				DateTime from = DateTime.Now;
				DateTime to = DateTime.Now;
				int mnths = 1;
				if (dataSet != null && dataSet.Tables.Count > 0 && dataSet.Tables["Sales_Forecasting_Detail"].Rows.Count > 0)
				{
					foreach (DataRow row in dataSet.Tables["Sales_Forecasting_Detail"].Rows)
					{
						text2 = ((!string.IsNullOrEmpty(text2)) ? (text2 + ",'" + row["ProductID"] + "'") : (text2 + "'" + row["ProductID"] + "'"));
						list.Add(new Tuple<string, decimal>(row["ProductID"].ToString(), 1m));
					}
				}
				if (!string.IsNullOrEmpty(text2))
				{
					foreach (DataRow row2 in GetPRoductLocationData(text2).Tables[0].Rows)
					{
						text3 = ((!string.IsNullOrEmpty(text3)) ? (text3 + ",'" + row2["LocationID"] + "'") : (text3 + "'" + row2["LocationID"] + "'"));
					}
				}
				foreach (DataRow row3 in dataSet.Tables[0].Rows)
				{
					from = DateTime.Parse(row3["FromDate"].ToString());
					to = DateTime.Parse(row3["ToDate"].ToString());
					mnths = int.Parse(row3["Period"].ToString());
				}
				DataSet productForecastData = GetProductForecastData(from, to, list, text3, mnths);
				dataSet.Merge(productForecastData);
				dataSet.Relations.Add("CastingData", new DataColumn[2]
				{
					dataSet.Tables["Sales_Forecasting"].Columns["SysDocID"],
					dataSet.Tables["Sales_Forecasting"].Columns["VoucherID"]
				}, new DataColumn[2]
				{
					dataSet.Tables["Sales_Forecasting_Detail"].Columns["SysDocID"],
					dataSet.Tables["Sales_Forecasting_Detail"].Columns["VoucherID"]
				}, createConstraints: false);
				return dataSet;
			}
			catch
			{
				throw;
			}
		}

		public DataSet GetPRoductLocationData(string items)
		{
			DataSet dataSet = new DataSet();
			string textCommand = "SELECT DISTINCT PL.LocationID FROM Product_Location PL LEFT JOIN Location L ON  L.LocationID=pL.LocationID WHERE L.IsWarehouse = 1 AND ProductID IN (" + items.ToString() + ") ";
			FillDataSet(dataSet, "Product_Location", textCommand);
			return dataSet;
		}

		public DataSet GetProductLocWiseData(DateTime from, DateTime to, List<Tuple<string, decimal>> list, string locIDs)
		{
			string text = StoreConfiguration.ToSqlDateTimeString(from);
			string text2 = StoreConfiguration.ToSqlDateTimeString(to);
			DataSet dataSet = new DataSet();
			string text3 = "SELECT   MAX(ST.ProductID) AS [ItemCode],MAX(ST.Description) AS Description, Sum(Quantity) AS QTY ,(select Sum(Quantity) from axo_stemp ST1 where ST1.ProductID=ST.ProductID AND ST1.TransactionDate Between '" + text + "' AND '" + text2 + "') AS Total, SUM(Quantity) / NULLIF((SELECT  SUM(Quantity) AS Expr1 FROM   axo_stemp AS SID WHERE    SID.TransactionDate Between '" + text + "' AND '" + text2 + "' AND(SID.ProductID = ST.ProductID)), 0) AS PercentValue, ST.LocationID FROM   axo_stemp ST where 1 = 1";
			if (from != DateTime.MinValue)
			{
				text3 = text3 + " AND ST.TransactionDate Between  '" + text + "' AND '" + text2 + "'";
			}
			string text4 = "";
			for (int i = 0; i < list.Count; i++)
			{
				text4 = text4 + "'" + list[i].Item1 + "'";
				if (i < list.Count - 1)
				{
					text4 += ",";
				}
			}
			if (text4 != "")
			{
				text3 = text3 + "  AND ST.ProductID IN (" + text4 + ") ";
			}
			if (locIDs != "")
			{
				text3 = text3 + " AND ST.LocationID In (" + locIDs.ToString() + ")";
			}
			text3 += " Group By ST.LocationID,ST.ProductID ";
			FillDataSet(dataSet, "Temp_Data", text3);
			foreach (Tuple<string, decimal> item3 in list)
			{
				string item = item3.Item1;
				decimal item2 = item3.Item2;
				foreach (DataRow row in dataSet.Tables[0].Rows)
				{
					decimal d = decimal.Parse(row["PercentValue"].ToString());
					string text5 = row["LocationID"].ToString();
					if (!dataSet.Tables[0].Columns.Contains(text5))
					{
						dataSet.Tables[0].Columns.Add(text5, typeof(string));
					}
					if (item == row["ItemCode"].ToString())
					{
						decimal num = item2 * d;
						if (dataSet.Tables[0].Columns.Contains(text5))
						{
							row[text5] = num;
						}
						row["Qty"] = num;
						row["Total"] = item2;
					}
				}
			}
			return dataSet;
		}

		public DataSet GetProductForecastData(DateTime from, DateTime to, List<Tuple<string, decimal>> list, string locIDs, int mnths)
		{
			string text = StoreConfiguration.ToSqlDateTimeString(from);
			string text2 = StoreConfiguration.ToSqlDateTimeString(to);
			DataSet dataSet = new DataSet();
			string text3 = "SELECT SD.ProductID AS [ItemCode],P.Description,P.UnitID,SD.LocationID ,SUM(SD.Quantity) SALE,(SELECT Quantity FROM Product_Location PL \r\n                            LEFT JOIN Location L ON  L.LocationID=pL.LocationID WHERE L.IsWarehouse = 1 AND PL.ProductID=SD.ProductID AND PL.LocationID=SD.LocationID ) AS [Stock],\r\n                            Round(((SUM(SD.Quantity) / DATEDIFF(MONTH, '" + text + "','" + text2 + "') + 1) * " + mnths + " -\r\n                            ((SELECT Quantity FROM Product_Location PL   LEFT JOIN Location L ON  L.LocationID=pL.LocationID WHERE  L.IsWarehouse = 1 AND PL.ProductID = SD.ProductID AND PL.LocationID = SD.LocationID))),5) AS Qty\r\n                             FROM Sales_Invoice S\r\n                            INNER JOIN Sales_Invoice_Detail SD On S.SysDocID = SD.SysDocID AND S.VoucherID = SD.VoucherID\r\n                            INNER JOIN Product P ON P.ProductID = SD.ProductID\r\n                             where 1 = 1  ";
			text3 = text3 + " AND S.TransactionDate Between  '" + text + "' AND '" + text2 + "'";
			string text4 = "";
			for (int i = 0; i < list.Count; i++)
			{
				text4 = text4 + "'" + list[i].Item1 + "'";
				if (i < list.Count - 1)
				{
					text4 += ",";
				}
			}
			if (text4 != "")
			{
				text3 = text3 + "  AND SD.ProductID IN (" + text4 + ") ";
			}
			if (locIDs != "")
			{
				text3 = text3 + " AND SD.LocationID In (" + locIDs.ToString() + ")";
			}
			text3 += " GROUP BY SD.ProductID,P.Description,P.UnitID,SD.LocationID";
			FillDataSet(dataSet, "Temp_Data", text3);
			foreach (Tuple<string, decimal> item2 in list)
			{
				string item = item2.Item1;
				foreach (DataRow row in dataSet.Tables[0].Rows)
				{
					string text5 = row["LocationID"].ToString();
					if (!dataSet.Tables[0].Columns.Contains(text5))
					{
						dataSet.Tables[0].Columns.Add(text5, typeof(string));
					}
					if (item == row["ItemCode"].ToString() && dataSet.Tables[0].Columns.Contains(text5))
					{
						row[text5] = row["Qty"].ToString();
					}
				}
			}
			return dataSet;
		}

		public DataSet GetList(DateTime from, DateTime to, bool showVoid)
		{
			DataSet dataSet = new DataSet();
			string text = StoreConfiguration.ToSqlDateTimeString(from);
			string text2 = StoreConfiguration.ToSqlDateTimeString(to);
			string text3 = "SELECT     ISNULL(IsVoid,'False') AS V,SysDocID [Doc ID],VoucherID [Doc Number],INV.LocationFrom+'-'+LocationName AS Location, TransactionDate [Order Date]\r\n                            FROM         Sales_Forecasting INV\r\n\t\t\t\t\t\t\tLEFT JOIN Location L ON INV.LocationFrom=L.LocationID WHERE  1=1 ";
			if (from != DateTime.MinValue)
			{
				text3 = text3 + " AND TransactionDate Between '" + text + "' AND '" + text2 + "'";
			}
			if (!showVoid)
			{
				text3 += " AND ISNULL(IsVoid,'False')='False'";
			}
			SqlCommand sqlCommand = new SqlCommand(text3);
			FillDataSet(dataSet, "Sales_Forecasting", sqlCommand);
			return dataSet;
		}

		public bool OrderHasShippedQuantity(string sysDocID, string voucherNumber, SqlTransaction sqlTransaction)
		{
			string exp = "Select Count(*) FROM Sales_Enquiry_Detail SOD\r\n                                WHERE SysdocID='" + sysDocID + "' AND VoucherID='" + voucherNumber + "' Having SUM(ISNULL(QuantityShipped,0))>0";
			object obj = ExecuteScalar(exp, sqlTransaction);
			if (obj != null && obj.ToString() != "" && int.Parse(obj.ToString()) > 0)
			{
				return true;
			}
			return false;
		}
	}
}
