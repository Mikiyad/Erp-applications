using Micromind.Common;
using Micromind.Common.Data;
using Micromind.Common.Libraries;
using System;
using System.Data;
using System.Data.SqlClient;

namespace Micromind.Data
{
	public sealed class BillOfLading : StoreObject
	{
		private const string BILLOFLADING_TABLE = "Bill_Of_Lading";

		private const string BILLOFLADINGDETAIL_TABLE = "Bill_Of_Lading_Detail";

		private const string SYSDOCID_PARM = "@SysDocID";

		private const string VOUCHERID_PARM = "@VoucherID";

		private const string COMPANYID_PARM = "@CompanyID";

		private const string DIVISIONID_PARM = "@DivisionID";

		private const string VENDORID_PARM = "@VendorID";

		private const string ISVOID_PARM = "@IsVoid";

		private const string TRANSACTIONDATE_PARM = "@TransactionDate";

		private const string STATUS_PARM = "@Status";

		private const string NOTE_PARM = "@Note";

		private const string CONTAINERNUMBER_PARM = "@ContainerNumber";

		private const string BOLNUMBER_PARM = "@BOLNumber";

		private const string CONTAINERSIZEID_PARM = "@ContainerSizeID";

		private const string REMARKS_PARM = "@Remarks";

		private const string TYPE_PARM = "@Type";

		private const string SHIPPINGMETHODID_PARM = "@ShippingMethodID";

		private const string REFERENCE_PARM = "@Reference";

		private const string PURCHASEFLOW_PARM = "@PurchaseFlow";

		private const string ATD_PARM = "@ATD";

		private const string PORT_PARM = "@Port";

		private const string LOADINGPORT_PARM = "@LoadingPort";

		private const string ETA_PARM = "@ETA";

		private const string SHIPPER_PARM = "@Shipper";

		private const string CLEARINGAGENT_PARM = "@ClearingAgent";

		private const string VALUE_PARM = "@Value";

		private const string VENDORREFERENCENUMBER_PARM = "@VendorReferenceNo";

		private const string SOURCESYSDOCID_PARM = "@SourceSysDocID";

		private const string SOURCEVOUCHERID_PARM = "@SourceVoucherID";

		private const string WEIGHT_PARM = "@Weight";

		private const string QUANTITY_PARM = "@Quantity";

		private const string UNITQUANTITY_PARM = "@UnitQuantity";

		private const string TRANSPORTERID_PARM = "@TransporterID";

		private const string CREATEDBY_PARM = "@CreatedBy";

		private const string DATECREATED_PARM = "@DateCreated";

		private const string UPDATEDBY_PARM = "@UpdatedBy";

		private const string DATEUPDATED_PARM = "@DateUpdated";

		private const string ROWINDEX_PARM = "@RowIndex";

		private int curDecPoints;

		public BillOfLading(Config config)
			: base(config)
		{
		}

		private string GetInsertUpdateBillOfLadingText(bool isUpdate)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddInsertUpdateParameters("Bill_Of_Lading", new FieldValue("SysDocID", "@SysDocID", isUpdateConditionField: true), new FieldValue("VoucherID", "@VoucherID", isUpdateConditionField: true), new FieldValue("DivisionID", "@DivisionID"), new FieldValue("CompanyID", "@CompanyID"), new FieldValue("VendorID", "@VendorID"), new FieldValue("PurchaseFlow", "@PurchaseFlow"), new FieldValue("ShippingMethodID", "@ShippingMethodID"), new FieldValue("Reference", "@Reference"), new FieldValue("Port", "@Port"), new FieldValue("LoadingPort", "@LoadingPort"), new FieldValue("ETA", "@ETA"), new FieldValue("ATD", "@ATD"), new FieldValue("Shipper", "@Shipper"), new FieldValue("ClearingAgent", "@ClearingAgent"), new FieldValue("Value", "@Value"), new FieldValue("TransporterID", "@TransporterID"), new FieldValue("TransactionDate", "@TransactionDate"), new FieldValue("IsVoid", "@IsVoid"), new FieldValue("Status", "@Status"), new FieldValue("SourceSysDocID", "@SourceSysDocID"), new FieldValue("SourceVoucherID", "@SourceVoucherID"), new FieldValue("BOLNumber", "@BOLNumber"), new FieldValue("VendorReferenceNo", "@VendorReferenceNo"), new FieldValue("Note", "@Note"));
			if (isUpdate)
			{
				sqlBuilder.AddInsertUpdateParameters("Bill_Of_Lading", new FieldValue("DateUpdated", "@DateUpdated", isUpdateConditionField: true, checkForNullValue: true));
			}
			if (isUpdate)
			{
				return sqlBuilder.GetUpdateExpression();
			}
			return sqlBuilder.GetInsertExpression();
		}

		private SqlCommand GetInsertUpdateBillOfLadingCommand(bool isUpdate)
		{
			SqlParameterCollection parameters;
			if (isUpdate)
			{
				if (updateCommand != null)
				{
					updateCommand = null;
				}
				updateCommand = new SqlCommand(GetInsertUpdateBillOfLadingText(isUpdate: true), base.DBConfig.Connection);
				updateCommand.CommandType = CommandType.Text;
				parameters = updateCommand.Parameters;
			}
			else
			{
				if (insertCommand != null)
				{
					insertCommand = null;
				}
				insertCommand = new SqlCommand(GetInsertUpdateBillOfLadingText(isUpdate: false), base.DBConfig.Connection);
				insertCommand.CommandType = CommandType.Text;
				parameters = insertCommand.Parameters;
			}
			parameters.Add("@SysDocID", SqlDbType.NVarChar);
			parameters.Add("@VoucherID", SqlDbType.NVarChar);
			parameters.Add("@DivisionID", SqlDbType.NVarChar);
			parameters.Add("@CompanyID", SqlDbType.TinyInt);
			parameters.Add("@VendorID", SqlDbType.NVarChar);
			parameters.Add("@TransactionDate", SqlDbType.DateTime);
			parameters.Add("@Status", SqlDbType.TinyInt);
			parameters.Add("@PurchaseFlow", SqlDbType.TinyInt);
			parameters.Add("@ShippingMethodID", SqlDbType.NVarChar);
			parameters.Add("@Reference", SqlDbType.NVarChar);
			parameters.Add("@VendorReferenceNo", SqlDbType.NVarChar);
			parameters.Add("@Port", SqlDbType.NVarChar);
			parameters.Add("@LoadingPort", SqlDbType.NVarChar);
			parameters.Add("@ETA", SqlDbType.DateTime);
			parameters.Add("@ATD", SqlDbType.DateTime);
			parameters.Add("@Shipper", SqlDbType.NVarChar);
			parameters.Add("@ClearingAgent", SqlDbType.NVarChar);
			parameters.Add("@Value", SqlDbType.Money);
			parameters.Add("@TransporterID", SqlDbType.NVarChar);
			parameters.Add("@BOLNumber", SqlDbType.NVarChar);
			parameters.Add("@IsVoid", SqlDbType.NVarChar);
			parameters.Add("@Note", SqlDbType.NVarChar);
			parameters.Add("@SourceSysDocID", SqlDbType.NVarChar);
			parameters.Add("@SourceVoucherID", SqlDbType.NVarChar);
			parameters["@SysDocID"].SourceColumn = "SysDocID";
			parameters["@VoucherID"].SourceColumn = "VoucherID";
			parameters["@DivisionID"].SourceColumn = "DivisionID";
			parameters["@CompanyID"].SourceColumn = "CompanyID";
			parameters["@VendorID"].SourceColumn = "VendorID";
			parameters["@PurchaseFlow"].SourceColumn = "PurchaseFlow";
			parameters["@Reference"].SourceColumn = "Reference";
			parameters["@VendorReferenceNo"].SourceColumn = "VendorReferenceNo";
			parameters["@TransactionDate"].SourceColumn = "TransactionDate";
			parameters["@ShippingMethodID"].SourceColumn = "ShippingMethodID";
			parameters["@Port"].SourceColumn = "Port";
			parameters["@LoadingPort"].SourceColumn = "LoadingPort";
			parameters["@ETA"].SourceColumn = "ETA";
			parameters["@ATD"].SourceColumn = "ATD";
			parameters["@Shipper"].SourceColumn = "Shipper";
			parameters["@ClearingAgent"].SourceColumn = "ClearingAgent";
			parameters["@Value"].SourceColumn = "Value";
			parameters["@TransporterID"].SourceColumn = "TransporterID";
			parameters["@TransactionDate"].SourceColumn = "TransactionDate";
			parameters["@Status"].SourceColumn = "Status";
			parameters["@BOLNumber"].SourceColumn = "BOLNumber";
			parameters["@IsVoid"].SourceColumn = "IsVoid";
			parameters["@Note"].SourceColumn = "Note";
			parameters["@SourceSysDocID"].SourceColumn = "SourceSysDocID";
			parameters["@SourceVoucherID"].SourceColumn = "SourceVoucherID";
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

		private string GetInsertUpdateBillOfLadingDetailsText(bool isUpdate)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddInsertUpdateParameters("Bill_Of_Lading_Detail", new FieldValue("SysDocID", "@SysDocID", isUpdateConditionField: true), new FieldValue("VoucherID", "@VoucherID", isUpdateConditionField: true), new FieldValue("ContainerSizeID", "@ContainerSizeID"), new FieldValue("ContainerNumber", "@ContainerNumber"), new FieldValue("Type", "@Type"), new FieldValue("Weight", "@Weight"), new FieldValue("Quantity", "@Quantity"), new FieldValue("UnitQuantity", "@UnitQuantity"), new FieldValue("Remarks", "@Remarks"), new FieldValue("RowIndex", "@RowIndex"));
			if (isUpdate)
			{
				return sqlBuilder.GetUpdateExpression();
			}
			return sqlBuilder.GetInsertExpression();
		}

		private SqlCommand GetInsertUpdateBillOfLadingDetailsCommand(bool isUpdate)
		{
			SqlParameterCollection parameters;
			if (isUpdate)
			{
				if (updateCommand != null)
				{
					updateCommand = null;
				}
				updateCommand = new SqlCommand(GetInsertUpdateBillOfLadingDetailsText(isUpdate: true), base.DBConfig.Connection);
				updateCommand.CommandType = CommandType.Text;
				parameters = updateCommand.Parameters;
			}
			else
			{
				if (insertCommand != null)
				{
					insertCommand = null;
				}
				insertCommand = new SqlCommand(GetInsertUpdateBillOfLadingDetailsText(isUpdate: false), base.DBConfig.Connection);
				insertCommand.CommandType = CommandType.Text;
				parameters = insertCommand.Parameters;
			}
			parameters.Add("@SysDocID", SqlDbType.NVarChar);
			parameters.Add("@VoucherID", SqlDbType.NVarChar);
			parameters.Add("@ContainerNumber", SqlDbType.NVarChar);
			parameters.Add("@ContainerSizeID", SqlDbType.NVarChar);
			parameters.Add("@Type", SqlDbType.NVarChar);
			parameters.Add("@Remarks", SqlDbType.NVarChar);
			parameters.Add("@RowIndex", SqlDbType.TinyInt);
			parameters.Add("@Weight", SqlDbType.Real);
			parameters.Add("@Quantity", SqlDbType.Real);
			parameters.Add("@UnitQuantity", SqlDbType.Real);
			parameters["@SysDocID"].SourceColumn = "SysDocID";
			parameters["@VoucherID"].SourceColumn = "VoucherID";
			parameters["@ContainerNumber"].SourceColumn = "ContainerNumber";
			parameters["@ContainerSizeID"].SourceColumn = "ContainerSizeID";
			parameters["@Type"].SourceColumn = "Type";
			parameters["@Quantity"].SourceColumn = "Quantity";
			parameters["@UnitQuantity"].SourceColumn = "UnitQuantity";
			parameters["@Weight"].SourceColumn = "Weight";
			parameters["@Remarks"].SourceColumn = "Remarks";
			parameters["@RowIndex"].SourceColumn = "RowIndex";
			if (isUpdate)
			{
				return updateCommand;
			}
			return insertCommand;
		}

		private bool ValidateData(BillOfLadingData journalData)
		{
			return true;
		}

		public bool CanUpdate(string sysDocID, string voucherNumber, SqlTransaction sqlTransaction)
		{
			string exp = "Select Count(*) FROM Purchase_Order_Detail POD\r\n                                WHERE SysdocID='" + sysDocID + "' AND VoucherID='" + voucherNumber + "' Having SUM(ISNULL(QuantityReceived,0))>0";
			object obj = ExecuteScalar(exp, sqlTransaction);
			if (obj != null && obj.ToString() != "" && int.Parse(obj.ToString()) > 0)
			{
				return false;
			}
			return true;
		}

		public bool InsertUpdateBillOfLading(BillOfLadingData BillOfLadingData, bool isUpdate)
		{
			bool flag = true;
			SqlCommand insertUpdateBillOfLadingCommand = GetInsertUpdateBillOfLadingCommand(isUpdate);
			try
			{
				_ = new CompanyInformations(base.DBConfig).CurrencyDecimalPoints;
				DataRow dataRow = BillOfLadingData.BillofLadingTable.Rows[0];
				SqlTransaction sqlTransaction = base.DBConfig.StartNewTransaction();
				string text = dataRow["VoucherID"].ToString();
				string sysDocID = dataRow["SysDocID"].ToString();
				if (isUpdate && !CanUpdate(sysDocID, text, sqlTransaction))
				{
					throw new CompanyException("Unable to update. Some of the items has been already ordered.", 1037);
				}
				if (!isUpdate && new SystemDocuments(base.DBConfig).ExistDocumentNumber("Bill_Of_Lading", "VoucherID", dataRow["SysDocID"].ToString(), text, sqlTransaction))
				{
					throw new CompanyException("Document number already exist.", 1046);
				}
				foreach (DataRow row in BillOfLadingData.BillOfLadingDetailTable.Rows)
				{
					row["SysDocID"] = dataRow["SysDocID"];
					row["VoucherID"] = dataRow["VoucherID"];
				}
				insertUpdateBillOfLadingCommand.Transaction = sqlTransaction;
				flag = (isUpdate ? (flag & Update(BillOfLadingData, "Bill_Of_Lading", insertUpdateBillOfLadingCommand)) : (flag & Insert(BillOfLadingData, "Bill_Of_Lading", insertUpdateBillOfLadingCommand)));
				insertUpdateBillOfLadingCommand = GetInsertUpdateBillOfLadingDetailsCommand(isUpdate: false);
				insertUpdateBillOfLadingCommand.Transaction = sqlTransaction;
				if (isUpdate)
				{
					flag &= DeleteBillOfLadingDetailsRows(sysDocID, text, sqlTransaction);
				}
				if (BillOfLadingData.Tables["Bill_Of_Lading_Detail"].Rows.Count > 0)
				{
					flag &= Insert(BillOfLadingData, "Bill_Of_Lading_Detail", insertUpdateBillOfLadingCommand);
				}
				if (!flag)
				{
					throw new Exception("Failed to save the transaction because of an unexpected error. Please try again.");
				}
				flag &= UpdateTableRowInsertUpdateInfo("Bill_Of_Lading", "SysDocID", dataRow["SysDocID"].ToString(), "VoucherID", dataRow["VoucherID"].ToString(), sqlTransaction, !isUpdate);
				string entityName = "Bill Of Lading";
				flag = (isUpdate ? (flag & AddActivityLog(entityName, text, sysDocID, ActivityTypes.Update, sqlTransaction)) : (flag & AddActivityLog(entityName, text, sysDocID, ActivityTypes.Add, sqlTransaction)));
				if (isUpdate)
				{
					return flag;
				}
				flag &= new SystemDocuments(base.DBConfig).UpdateNextDocumentNumber(dataRow["SysDocID"].ToString(), dataRow["VoucherID"].ToString(), "Bill_Of_Lading", "VoucherID", sqlTransaction);
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

		public BillOfLadingData GetBillOfLadingByID(string sysDocID, string voucherID)
		{
			try
			{
				BillOfLadingData billOfLadingData = new BillOfLadingData();
				string textCommand = "select * from Bill_Of_Lading WHERE VoucherID='" + voucherID + "' AND SysDocID='" + sysDocID + "'";
				FillDataSet(billOfLadingData, "Bill_Of_Lading", textCommand);
				if (billOfLadingData == null || billOfLadingData.Tables.Count == 0 || billOfLadingData.Tables["Bill_Of_Lading"].Rows.Count == 0)
				{
					return null;
				}
				textCommand = "select * from Bill_Of_Lading_Detail PCD WHERE VoucherID='" + voucherID + "' AND SysDocID='" + sysDocID + "' ORDER BY PCD.RowIndex";
				FillDataSet(billOfLadingData, "Bill_Of_Lading_Detail", textCommand);
				return billOfLadingData;
			}
			catch
			{
				throw;
			}
		}

		public DataSet GetBillOfLadingDetailReport(DateTime from, DateTime to, string jobID, string vendorID, string fromItem, string toItem, string fromClass, string toClass, string fromCategory, string toCategory)
		{
			try
			{
				DataSet dataSet = new DataSet();
				string text = CommonLib.ToSqlDateTimeString(from);
				string text2 = CommonLib.ToSqlDateTimeString(to);
				string text3 = "SELECT DISTINCT PO.TransactionDate,PO.VoucherID,V.VendorName,J.JobName,POD.UnitPrice,POD.ProductID,POD.UnitID,POD.Description,POD.Quantity,\r\n                                POD.QuantityReceived,POD.UnitPrice  FROM Purchase_Order PO LEFT JOIN Purchase_Order_Detail POD ON PO.SysDocID=POD.SysDocID AND PO.VoucherID=POD.VoucherID\r\n                                LEFT JOIN JOB J ON J.JobID=POD.JobID LEFT JOIN Vendor V ON V.VendorID=PO.VendorID LEFT JOIN Product P ON POD.ProductID=P.ProductID\r\n                                LEFT JOIN Product ON POD.ProductID=Product.ProductID ";
				text3 = text3 + "WHERE PO.TransactionDate BETWEEN '" + text + "' AND '" + text2 + "' AND ISNULL(PO.IsVoid,'False')='False' ";
				if (jobID != "")
				{
					text3 = text3 + " AND POD.JobID='" + jobID + "'";
				}
				if (vendorID != "")
				{
					text3 = text3 + " AND PO.VendorID= '" + vendorID + "'";
				}
				if (fromItem != "")
				{
					text3 = text3 + " AND POD.ProductID BETWEEN '" + fromItem + "' AND '" + toItem + "' ";
				}
				if (fromClass != "")
				{
					text3 = text3 + " AND POD.ProductID IN (SELECT ProductID FROM Product WHERE ClassID BETWEEN '" + fromClass + "' AND '" + toClass + "') ";
				}
				if (fromCategory != "")
				{
					text3 = text3 + " AND POD.ProductID IN (SELECT ProductID FROM Product WHERE CategoryID BETWEEN '" + fromCategory + "' AND '" + toCategory + "') ";
				}
				FillDataSet(dataSet, "Bill_Of_Lading", text3);
				return dataSet;
			}
			catch
			{
				throw;
			}
		}

		public DataSet GetPOsForPackingList(bool isImport)
		{
			try
			{
				DataSet dataSet = new DataSet();
				string textCommand = "select PS.SysDocID  [Doc ID],PS.VoucherID [Number], PS.BOLNumber, PS.Reference,PS.ContainerNumber, PS.ShippingMethodID, ps.Shipper,ps.ATD,PS.ETA, PS.Value,PS.TransporterID, PS.ContainerSizeID, PS.ClearingAgent from PO_Shipment PS";
				FillDataSet(dataSet, "Bill_Of_Lading", textCommand);
				return dataSet;
			}
			catch
			{
				throw;
			}
		}

		public DataSet GetPOsItemsToShip()
		{
			try
			{
				DataSet dataSet = new DataSet();
				string textCommand = "SELECT  POD.SysDocID [Doc ID], POD.VoucherID AS [PO Number],ProductID,Description,POD.UnitID,RowIndex,UnitQuantity,PO.BOLNumber,PO.ATD,PO.ClearingAgent,PO.ContainerNumber,PO.ContainerSizeID,PO.LoadingPort,PO.Port,PO.TransporterID,po.Value,\r\n                                ISNULL(UnitQuantity,Quantity) AS Quantity,\r\n                               ISNULL(QuantityReceived,0) AS QuantityReceived, PO.ShippingMethodID, PO.ETA\r\n                                FROM PO_Shipment_Detail POD INNER JOIN PO_Shipment PO ON PO.SysDocID=POD.SysDocID AND PO.VoucherID=POD.VoucherID ";
				FillDataSet(dataSet, "Bill_Of_Lading_Detail", textCommand);
				return dataSet;
			}
			catch
			{
				throw;
			}
		}

		public DataSet GetBOLListForPayment()
		{
			try
			{
				DataSet dataSet = new DataSet();
				string textCommand = "SELECT   SysDocID [Doc ID],VoucherID [Doc Number],BOLNumber [BOL No],ContainerNumber [Container No],INV.VendorID [Vendor Code],VendorName [Vendor Name],TransactionDate [Invoice Date], INV.ETA                            \r\n                            FROM   PO_Shipment INV\r\n                            Inner JOIN Vendor ON VENDOR.VendorID=INV.VendorID WHERE ISNULL(IsVoid,'False')='False' AND NOT BOLNumber IN (SELECT  ISNULL(BOLNo,'') FROM Purchase_Order_NonInv)";
				FillDataSet(dataSet, "PO_Shipment", textCommand);
				return dataSet;
			}
			catch
			{
				throw;
			}
		}

		public DataSet GetOpenOrdersSummary(string vendorID, bool isImport)
		{
			try
			{
				DataSet dataSet = new DataSet();
				string str = "SELECT SO.SysDocID [Doc ID],SO.VoucherID [Number],TransactionDate AS [Date],SO.VendorID + '-' + C.VendorName AS [Vendor],Reference,Total AS Amount  FROM Purchase_Order SO\r\n                             INNER JOIN Vendor C ON SO.VendorID=C.VendorID  WHERE ISNULL(IsVoid,'False')='False' AND Status=1";
				if (vendorID != "")
				{
					str = str + " AND SO.VendorID='" + vendorID + "'";
				}
				str = str + " AND ISNULL(IsImport,'False') = '" + isImport.ToString() + "'";
				FillDataSet(dataSet, "Bill_Of_Lading", str);
				return dataSet;
			}
			catch
			{
				throw;
			}
		}

		public DataSet GetOpenPOComboData(string vendorID)
		{
			try
			{
				DataSet dataSet = new DataSet();
				string text = "SELECT PO.VoucherID  ,PO.SysDocID  , TransactionDate AS [Date],PO.VendorID + '-' + V.VendorName AS [Vendor],Reference,Total AS Amount,\r\n                                ISNULL((SELECT SUM(Amount) FROM Payment_Request PR WHERE POSysDocID = PO.SysDocID AND POVoucherID = PO.VoucherID),0) AS Paid\r\n                                 FROM Purchase_Order PO\r\n                                     INNER JOIN Vendor V ON PO.VendorID= V.VendorID  WHERE ISNULL(IsVoid,'False')='False' AND Status=1 ";
				if (vendorID != "")
				{
					text = text + " AND PO.VendorID='" + vendorID + "'";
				}
				FillDataSet(dataSet, "Bill_Of_Lading", text);
				return dataSet;
			}
			catch
			{
				throw;
			}
		}

		public DataSet GetPOListForPayment(string vendorID)
		{
			try
			{
				DataSet dataSet = new DataSet();
				string text = "SELECT PO.VoucherID  ,PO.SysDocID  , TransactionDate AS [Date],PO.VendorID + '-' + V.VendorName AS [Vendor],ETA as [ETA],Reference,Total AS Amount,\r\n                                ISNULL((SELECT SUM(Amount) FROM Payment_Request PR WHERE POSysDocID = PO.SysDocID AND POVoucherID = PO.VoucherID),0) AS Paid\r\n                                 FROM Purchase_Order PO\r\n                                     INNER JOIN Vendor V ON PO.VendorID= V.VendorID  WHERE ISNULL(IsVoid,'False')='False' AND Status IN (1,2,3) ";
				if (vendorID != "")
				{
					text = text + " AND PO.VendorID='" + vendorID + "'";
				}
				FillDataSet(dataSet, "Bill_Of_Lading", text);
				return dataSet;
			}
			catch
			{
				throw;
			}
		}

		public DataSet GetBillOfLadingAll()
		{
			try
			{
				DataSet dataSet = new DataSet();
				string textCommand = "SELECT SO.SysDocID [Doc ID],SO.VoucherID [Number],TransactionDate AS [Date],SO.VendorID + '-' + C.VendorName AS [Vendor],Reference,Total AS Amount  FROM Purchase_Order SO\r\n                             INNER JOIN Vendor C ON SO.VendorID=C.VendorID  WHERE ISNULL(IsVoid,'False')='False' AND Status=1";
				FillDataSet(dataSet, "Bill_Of_Lading", textCommand);
				return dataSet;
			}
			catch
			{
				throw;
			}
		}

		internal bool CloseShippedOrder(string sysDocID, string voucherID, SqlTransaction sqlTransaction)
		{
			return true;
		}

		public bool SetOrderStatus(string sysDocID, string voucherID, PurchaseOrderStatus status)
		{
			try
			{
				string exp = "UPDATE Purchase_Order SET Status= " + (int)status + " WHERE SysDocID='" + sysDocID + "' AND VoucherID='" + voucherID + "'";
				return ExecuteNonQuery(exp) > 0;
			}
			catch
			{
				throw;
			}
		}

		internal bool CloseReceivedOrder(string sysDocID, string voucherID, SqlTransaction sqlTransaction)
		{
			try
			{
				string exp = "SELECT COUNT(RowIndex)FROM Purchase_Order_Detail POD WHERE SysDocID='" + sysDocID + "' AND VoucherID='" + voucherID + "'\r\n                 AND (SELECT SUM((CASE WHEN UnitQuantity IS NULL \r\n                                THEN Quantity ELSE UnitQuantity END) ) FROM Purchase_Order_Detail POD2 WHERE POD.SysDocID=POD2.SysDocID AND POD.VoucherID=POD2.VoucherID)-\r\n                                 (SELECT SUM(ISNULL(QuantityReceived,0))  FROM Purchase_Order_Detail POD3 WHERE POD.SysDocID=POD3.SysDocID AND POD.VoucherID=POD3.VoucherID) <= 0";
				object obj = ExecuteScalar(exp, sqlTransaction);
				if (obj == null || int.Parse(obj.ToString()) == 0)
				{
					return true;
				}
				exp = "UPDATE Purchase_Order SET Status= 2 WHERE SysDocID='" + sysDocID + "' AND VoucherID='" + voucherID + "'";
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
				string exp = "SELECT COUNT(RowIndex)FROM Purchase_Order_Detail POD WHERE SysDocID='" + sysDocID + "' AND VoucherID='" + voucherID + "'\r\n                 AND (SELECT SUM((CASE WHEN UnitQuantity IS NULL \r\n                                THEN Quantity ELSE UnitQuantity END) ) FROM Purchase_Order_Detail POD2 WHERE POD.SysDocID=POD2.SysDocID AND POD.VoucherID=POD2.VoucherID)-\r\n                                 (SELECT SUM(ISNULL(QuantityReceived,0) ) FROM Purchase_Order_Detail POD3 WHERE POD.SysDocID=POD3.SysDocID AND POD.VoucherID=POD3.VoucherID) <= 0";
				object obj = ExecuteScalar(exp, sqlTransaction);
				if (obj == null || int.Parse(obj.ToString()) > 0)
				{
					return true;
				}
				exp = "UPDATE Purchase_Order SET Status= 1 WHERE SysDocID='" + sysDocID + "' AND VoucherID='" + voucherID + "'";
				return ExecuteNonQuery(exp, sqlTransaction) > 0;
			}
			catch
			{
				return false;
			}
		}

		internal bool DeleteBillOfLadingDetailsRows(string sysDocID, string voucherID, SqlTransaction sqlTransaction)
		{
			bool flag = true;
			try
			{
				string commandText = "DELETE FROM Bill_Of_Lading_Detail WHERE SysDocID = '" + sysDocID + "' AND VoucherID = '" + voucherID + "'";
				return Delete(commandText, sqlTransaction);
			}
			catch
			{
				throw;
			}
		}

		public bool VoidBillOfLading(string sysDocID, string voucherID, bool isVoid)
		{
			bool flag = true;
			try
			{
				SqlTransaction sqlTransaction = base.DBConfig.StartNewTransaction();
				if (!CanUpdate(sysDocID, voucherID, sqlTransaction))
				{
					throw new CompanyException("Unable to update. Some of the items in this order has been already received.", 1037);
				}
				BillOfLadingData billOfLadingData = new BillOfLadingData();
				string textCommand = "SELECT * FROM Bill_Of_Lading_Detail\r\n                              WHERE SysDocID = '" + sysDocID + "' AND VoucherID = '" + voucherID + "'";
				FillDataSet(billOfLadingData, "Bill_Of_Lading_Detail", textCommand, sqlTransaction);
				foreach (DataRow row in billOfLadingData.BillOfLadingDetailTable.Rows)
				{
					_ = row;
				}
				textCommand = "UPDATE Bill_Of_Lading SET IsVoid = '" + isVoid.ToString() + "' WHERE SysDocID='" + sysDocID + "' AND VoucherID = '" + voucherID + "'";
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
				AddActivityLog("Bill_Of_Lading", voucherID, sysDocID, activityType, sqlTransaction);
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

		public bool DeleteBillOfLading(string sysDocID, string voucherID)
		{
			bool flag = true;
			try
			{
				string text = "";
				SqlTransaction sqlTransaction = base.DBConfig.StartNewTransaction();
				if (!CanUpdate(sysDocID, voucherID, sqlTransaction))
				{
					throw new CompanyException("Unable to change or delete. Some of the items in this order has been already received.", 1037);
				}
				flag &= DeleteBillOfLadingDetailsRows(sysDocID, voucherID, sqlTransaction);
				text = "DELETE FROM Bill_Of_Lading WHERE SysDocID='" + sysDocID + "' AND VoucherID = '" + voucherID + "'";
				flag &= (ExecuteNonQuery(text, sqlTransaction) >= 0);
				if (!flag)
				{
					return flag;
				}
				ActivityTypes activityType = ActivityTypes.Delete;
				AddActivityLog("Bill_Of_Lading", voucherID, sysDocID, activityType, sqlTransaction);
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

		public DataSet GetBillOfLadingListReport()
		{
			DataSet dataSet = new DataSet();
			string textCommand = "SELECT SO.* ,SOD.Remarks, SOD.ContainerNumber,SOD.ContainerSizeID,  SO.VendorID+'-'+ VendorName AS Vendor, ContainerSizeName\r\n                            FROM Bill_Of_Lading SO INNER JOIN Bill_Of_Lading_Detail SOD  \r\n\t\t\t\t\t\t\tON SO.SysDocID=SOD.SysDocID and SO.VoucherID=SOD.VoucherID INNER JOIN Vendor ON SO.VendorID=Vendor.VendorID INNER JOIN ContainerSize CS ON SOD.ContainerSizeID=CS.ContainerSizeID\r\n                            WHERE ISNULL(IsVoid,'False')='False'  ";
			FillDataSet(dataSet, "BOL", textCommand);
			return dataSet;
		}

		public DataSet GetBillOfLadingToPrint(string sysDocID, string[] voucherID)
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
				string textCommand = "SELECT SO.* , SO.VendorID+'-'+ VendorName AS Vendor, SO.ClearingAgent+'-'+VendorName AS ClearingAgentName\r\n                            FROM Bill_Of_Lading SO INNER JOIN Vendor  ON SO.VendorID=Vendor.VendorID\r\n                            WHERE ISNULL(IsVoid,'False')='False'  and SO.SysDocID = '" + sysDocID + "' AND SO.VoucherID IN (" + text + ")";
				DataSet dataSet = new DataSet();
				FillDataSet(dataSet, "Bill_Of_Lading", textCommand);
				textCommand = "SELECT SOD.* , ContainerSizeName\r\n                            FROM Bill_Of_Lading_Detail SOD LEFT JOIn ContainerSize CS ON SOD.ContainerSizeID=CS.ContainerSizeID \r\n                            WHERE SOD.SysDocID = '" + sysDocID + "' AND SOD.VoucherID IN (" + text + ")";
				FillDataSet(dataSet, "Bill_Of_Lading_Detail", textCommand);
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
			string text3 = "SELECT   DISTINCT SysDocID[Doc ID], VoucherID, t.BOLNumber,STUFF((SELECT distinct ', ' + ContainerNumber\r\n                                 from Bill_Of_Lading_Detail t1\r\n                                 where t.SysDocID = t1.SysDocID and t.VoucherID=t1.VoucherID\r\n                                    FOR XML PATH(''), TYPE\r\n                                    ).value('.', 'NVARCHAR(MAX)') \r\n                                ,1,2,'') [Container Number], Vendor.VendorName as Vendor, t.TransactionDate [Date]\r\n                           FROM  Bill_Of_Lading  t LEFT JOIN Vendor ON VENDOR.VendorID=t.VendorID";
			if (from != DateTime.MinValue)
			{
				text3 = text3 + " where t.TransactionDate Between '" + text + "' AND '" + text2 + " ' Group By t.VoucherID, t.SysDocID, t.BOLNumber, t.TransactionDate,Vendor.VendorName";
			}
			SqlCommand sqlCommand = new SqlCommand(text3);
			FillDataSet(dataSet, "Bill_Of_Lading", sqlCommand);
			return dataSet;
		}

		public DataSet XGetPendingApprovalList(string approvalID)
		{
			DataSet dataSet = new DataSet();
			SqlCommand sqlCommand = new SqlCommand("SELECT     ISNULL(IsVoid,'False') AS V,SysDocID [Doc ID],VoucherID [Doc Number],INV.VendorID [Vendor Code],VendorName [Vendor Name],TransactionDate [Order Date],Status,\r\n                            CASE ISNULL(IsImport,'False') WHEN 'True' THEN 'Import' ELSE 'Local' END AS [Type],INV.BuyerID [Buyer],Reference,INV.CurrencyID Currency,Total [Amount]\r\n                            FROM         Purchase_Order INV\r\n                            INNER JOIN Vendor ON VENDOR.VendorID=INV.VendorID WHERE 1=1 ");
			FillDataSet(dataSet, "Bill_Of_Lading", sqlCommand);
			return dataSet;
		}

		public DataSet GetCostEntryList()
		{
			try
			{
				DataSet dataSet = new DataSet();
				string textCommand = "SELECT pce.SysDocID,pce.VoucherID,pce.BOLNumber from Purchase_Cost_Entry pce ";
				FillDataSet(dataSet, "Bill_Of_Lading", textCommand);
				return dataSet;
			}
			catch
			{
				throw;
			}
		}

		public BillOfLadingData GetCostEntryByID(string sysDocID, string voucherID)
		{
			try
			{
				BillOfLadingData billOfLadingData = new BillOfLadingData();
				string textCommand = "SELECT pce.SysDocID,pce.VoucherID,pce.BOLNumber from Purchase_Cost_Entry pce\r\n                                 WHERE pce.VoucherID='" + voucherID + "' AND pce.SysDocID='" + sysDocID + "'";
				FillDataSet(billOfLadingData, "Bill_Of_Lading", textCommand);
				if (billOfLadingData == null || billOfLadingData.Tables.Count == 0 || billOfLadingData.Tables["Bill_Of_Lading"].Rows.Count == 0)
				{
					return null;
				}
				textCommand = "SELECT TD.*  FROM Purchase_Cost_Entry_Detail TD  WHERE VoucherID='" + voucherID + "' AND SysDocID='" + sysDocID + "'";
				FillDataSet(billOfLadingData, "Bill_Of_Lading_Detail", textCommand);
				return billOfLadingData;
			}
			catch
			{
				throw;
			}
		}

		public DataSet GetOpenExpenseDetails(string scheduleSysDocID, string schedulevoucherID, string RowIndexStr, bool tochk, bool isupdate, string invoiceSysDocID, string invoicevoucherID)
		{
			try
			{
				DataSet dataSet = new DataSet();
				string str = "SELECT DISTINCT PCD.ExpenseID,PCD.SysDocID [Doc ID],CAST((1) AS BIT) AS C,PCD.VoucherID [Number],PCD.BOLNumber,SUM(ISNULL(PIE.Amount,0)) AS 'Allocated',PCD.Cost,\r\n                                       PCD.RowIndex,PCD.Description,PCD.AmountFC,PCD.CurrencyID,PCD.CurrencyRate,PCD.RateType,PCD.Amount AS Amount,(PCD.Amount-SUM(ISNULL(PIE.Amount,0))) AS 'Cost to Allocate'\r\n                                        FROM Purchase_Cost_Entry_Detail PCD \r\n                LEFT JOIN Purchase_Invoice_Expense PIE ON PCD.VoucherID=PIE.PCVoucherID AND PCD.SysDocID=PIE.PCSysDocID AND PCD.RowIndex=PIE.PCRowIndex  WHERE  PCD.SysDocID='" + scheduleSysDocID + "' AND PCD.VoucherID = '" + schedulevoucherID + "' ";
				if (tochk)
				{
					str = str + " AND PCD.RowIndex IN (" + RowIndexStr + ")";
				}
				str += " GROUP BY  PCD.ExpenseID,PCD.SysDocID ,PCD.VoucherID ,PCD.BOLNumber,PCD.Cost,PCD.RowIndex,PCD.Description,PCD.AmountFC,PCD.CurrencyID,PCD.CurrencyRate,PCD.RateType,PCD.Amount";
				FillDataSet(dataSet, "Bill_Of_Lading_Detail", str);
				return dataSet;
			}
			catch
			{
				throw;
			}
		}

		public DataSet GetCostEntryBOLList(string BOL)
		{
			try
			{
				DataSet dataSet = new DataSet();
				string str = "SELECT pce.SysDocID,pce.VoucherID,pce.BOLNumber from Purchase_Cost_Entry pce ";
				str = str + " WHERE pce.BOLNumber ='" + BOL + "'";
				FillDataSet(dataSet, "Bill_Of_Lading", str);
				return dataSet;
			}
			catch
			{
				throw;
			}
		}

		public DataSet GetVendorExpenseList(string VendorID)
		{
			try
			{
				DataSet dataSet = new DataSet();
				string text = "SELECT DISTINCT pd.ExpenseID,pd.Description,pd.CurrencyID,pd.CurrencyRate,pd.RateType from Purchase_Cost_Entry_Detail pd inner join Purchase_Cost_Entry pe ON pe.SysDocID=pd.SysDocID AND  pe.VoucherID=pd.VoucherID ";
				if (VendorID != "")
				{
					text = text + " WHERE pe.VendorID ='" + VendorID + "'";
				}
				FillDataSet(dataSet, "ExpenseDetails", text);
				return dataSet;
			}
			catch
			{
				throw;
			}
		}

		public DataSet GetBOLForPackingList(string vendorID)
		{
			try
			{
				DataSet dataSet = new DataSet();
				string text = "SELECT PO.SysDocID[DOC ID], PO.VoucherID[DOC Number], PO.BOLNumber,POD.ContainerNumber [Container Number],\r\n                    PO.VendorID,POD.ContainerSizeID AS [Container Size],\r\n                    PO.VendorID + '-' + V.VendorName AS [Vendor], PO.ClearingAgent,POD.Weight, PO.ETA, PO.ETA, PO.Shipper,\r\n                     PO.ShippingMethodID, PO.ATD, PO.Port, PO.LoadingPort, PO.TransporterID FROM Bill_Of_Lading PO\r\n                    INNER JOIN Bill_Of_Lading_Detail POD ON Po.SysDocID=POD.SysDocID and PO.VoucherID=POD.VoucherID\r\n                    INNER JOIN Vendor V ON PO.VendorID=V.VendorID\t\t\t\t\t\t\t\t\t\t\t\t\t\t\r\n                    WHERE ISNULL(IsVoid,'False')='False' \r\n                    AND POD.ContainerNumber+'-'+PO.BOLNumber NOT IN (SELECT  ISNULL(ContainerNumber,'')+'-'+BOLNumber FROM PO_Shipment WHERE ISNULL(IsVoid,'False')='False' )";
				if (vendorID != "")
				{
					text = text + " AND PO.VendorID='" + vendorID + "'";
				}
				FillDataSet(dataSet, "Bill_Of_Lading", text);
				return dataSet;
			}
			catch
			{
				throw;
			}
		}

		public DataSet GetBOLList(string BOLNumber)
		{
			try
			{
				DataSet dataSet = new DataSet();
				string textCommand = "SELECT PO.VendorID,Ven.ParentVendorID, POD.SysDocID[Doc ID], POD.VoucherID AS[PO Number],RowIndex, POD.Quantity, POD.ContainerSizeID,POD.ContainerNumber,PO.BOLNumber\r\n                              ,  PO.ShippingMethodID, PO.ETA, PO.Port, PO.LoadingPort,PO.ATD\r\n                                  FROM  Bill_Of_Lading_Detail POD INNER JOIN Bill_Of_Lading PO ON PO.SysDocID = POD.SysDocID AND PO.VoucherID = POD.VoucherID\r\n                                    INNER JOIN Vendor V ON PO.VendorID=V.VendorID\r\n\r\n                                    WHERE PO.BOLNumber='" + BOLNumber + "'";
				FillDataSet(dataSet, "Bill_Of_Lading", textCommand);
				return dataSet;
			}
			catch
			{
				throw;
			}
		}
	}
}
