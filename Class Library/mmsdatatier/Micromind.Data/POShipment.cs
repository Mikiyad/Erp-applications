using Micromind.Common;
using Micromind.Common.Data;
using Micromind.Common.Libraries;
using System;
using System.Collections;
using System.Data;
using System.Data.SqlClient;

namespace Micromind.Data
{
	public sealed class POShipment : StoreObject
	{
		private const string POSHIPMENT_TABLE = "PO_Shipment";

		private const string POSHIPMENTDETAIL_TABLE = "PO_Shipment_Detail";

		private const string SYSDOCID_PARM = "@SysDocID";

		private const string VOUCHERID_PARM = "@VoucherID";

		private const string COMPANYID_PARM = "@CompanyID";

		private const string DIVISIONID_PARM = "@DivisionID";

		private const string VENDORID_PARM = "@VendorID";

		private const string PURCHASEFLOW_PARM = "@PurchaseFlow";

		private const string TRANSACTIONDATE_PARM = "@TransactionDate";

		private const string STATUS_PARM = "@Status";

		private const string SHIPPINGMETHODID_PARM = "@ShippingMethodID";

		private const string REFERENCE_PARM = "@Reference";

		private const string NOTE_PARM = "@Note";

		private const string PONUMBER_PARM = "@PONumber";

		private const string ISVOID_PARM = "@IsVoid";

		private const string CONTAINERNUMBER_PARM = "@ContainerNumber";

		private const string PORT_PARM = "@Port";

		private const string LOADINGPORT_PARM = "@LoadingPort";

		private const string ETA_PARM = "@ETA";

		private const string ATD_PARM = "@ATD";

		private const string BOLNUMBER_PARM = "@BOLNumber";

		private const string SHIPPER_PARM = "@Shipper";

		private const string CLEARINGAGENT_PARM = "@ClearingAgent";

		private const string WEIGHT_PARM = "@Weight";

		private const string ISRECEIVED_PARM = "@IsReceived";

		private const string VALUE_PARM = "@Value";

		private const string TRANSPORTERID_PARM = "@TransporterID";

		private const string CONTAINERSIZEID_PARM = "@ContainerSizeID";

		private const string VENDORREFERENCENUMBER_PARM = "@VendorReferenceNo";

		private const string CREATEDBY_PARM = "@CreatedBy";

		private const string DATECREATED_PARM = "@DateCreated";

		private const string UPDATEDBY_PARM = "@UpdatedBy";

		private const string DATEUPDATED_PARM = "@DateUpdated";

		private const string PRODUCTID_PARM = "@ProductID";

		private const string QUANTITY_PARM = "@Quantity";

		private const string DESCRIPTION_PARM = "@Description";

		private const string UNITPRICE_PARM = "@UnitPrice";

		private const string UNITID_PARM = "@UnitID";

		private const string UNITQUANTITY_PARM = "@UnitQuantity";

		private const string UNITFACTOR_PARM = "@UnitFactor";

		private const string FACTORTYPE_PARM = "@FactorType";

		private const string ROWINDEX_PARM = "@RowIndex";

		private const string SOURCEVOUCHERID_PARM = "@SourceVoucherID";

		private const string SOURCESYSDOCID_PARM = "@SourceSysDocID";

		private const string SOURCEROWINDEX_PARM = "@SourceRowIndex";

		private const string ISSOURCEDROW_PARM = "@IsSourcedRow";

		private const string SOURCEDOCTYPE_PARM = "@SourceDocType";

		private const string QUANTITYRECEIVED_PARM = "@QuantityReceived";

		public POShipment(Config config)
			: base(config)
		{
		}

		private string GetInsertUpdatePOShipmentText(bool isUpdate)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddInsertUpdateParameters("PO_Shipment", new FieldValue("SysDocID", "@SysDocID", isUpdateConditionField: true), new FieldValue("VoucherID", "@VoucherID", isUpdateConditionField: true), new FieldValue("DivisionID", "@DivisionID"), new FieldValue("CompanyID", "@CompanyID"), new FieldValue("VendorID", "@VendorID"), new FieldValue("PurchaseFlow", "@PurchaseFlow"), new FieldValue("TransactionDate", "@TransactionDate"), new FieldValue("ShippingMethodID", "@ShippingMethodID"), new FieldValue("Status", "@Status"), new FieldValue("Reference", "@Reference"), new FieldValue("ContainerNumber", "@ContainerNumber"), new FieldValue("Port", "@Port"), new FieldValue("LoadingPort", "@LoadingPort"), new FieldValue("ETA", "@ETA"), new FieldValue("ATD", "@ATD"), new FieldValue("BOLNumber", "@BOLNumber"), new FieldValue("Shipper", "@Shipper"), new FieldValue("ClearingAgent", "@ClearingAgent"), new FieldValue("Weight", "@Weight"), new FieldValue("Value", "@Value"), new FieldValue("TransporterID", "@TransporterID"), new FieldValue("ContainerSizeID", "@ContainerSizeID"), new FieldValue("VendorReferenceNo", "@VendorReferenceNo"), new FieldValue("Note", "@Note"));
			if (isUpdate)
			{
				sqlBuilder.AddInsertUpdateParameters("PO_Shipment", new FieldValue("DateUpdated", "@DateUpdated", isUpdateConditionField: true, checkForNullValue: true));
			}
			if (isUpdate)
			{
				return sqlBuilder.GetUpdateExpression();
			}
			return sqlBuilder.GetInsertExpression();
		}

		private SqlCommand GetInsertUpdatePOShipmentCommand(bool isUpdate)
		{
			SqlParameterCollection parameters;
			if (isUpdate)
			{
				if (updateCommand != null)
				{
					updateCommand = null;
				}
				updateCommand = new SqlCommand(GetInsertUpdatePOShipmentText(isUpdate: true), base.DBConfig.Connection);
				updateCommand.CommandType = CommandType.Text;
				parameters = updateCommand.Parameters;
			}
			else
			{
				if (insertCommand != null)
				{
					insertCommand = null;
				}
				insertCommand = new SqlCommand(GetInsertUpdatePOShipmentText(isUpdate: false), base.DBConfig.Connection);
				insertCommand.CommandType = CommandType.Text;
				parameters = insertCommand.Parameters;
			}
			parameters.Add("@SysDocID", SqlDbType.NVarChar);
			parameters.Add("@VoucherID", SqlDbType.NVarChar);
			parameters.Add("@DivisionID", SqlDbType.NVarChar);
			parameters.Add("@CompanyID", SqlDbType.TinyInt);
			parameters.Add("@VendorID", SqlDbType.NVarChar);
			parameters.Add("@PurchaseFlow", SqlDbType.TinyInt);
			parameters.Add("@TransactionDate", SqlDbType.DateTime);
			parameters.Add("@ShippingMethodID", SqlDbType.NVarChar);
			parameters.Add("@Status", SqlDbType.TinyInt);
			parameters.Add("@Reference", SqlDbType.NVarChar);
			parameters.Add("@ContainerNumber", SqlDbType.NVarChar);
			parameters.Add("@Port", SqlDbType.NVarChar);
			parameters.Add("@LoadingPort", SqlDbType.NVarChar);
			parameters.Add("@ETA", SqlDbType.DateTime);
			parameters.Add("@ATD", SqlDbType.DateTime);
			parameters.Add("@BOLNumber", SqlDbType.NVarChar);
			parameters.Add("@Shipper", SqlDbType.NVarChar);
			parameters.Add("@ClearingAgent", SqlDbType.NVarChar);
			parameters.Add("@Weight", SqlDbType.Real);
			parameters.Add("@Value", SqlDbType.Money);
			parameters.Add("@TransporterID", SqlDbType.NVarChar);
			parameters.Add("@ContainerSizeID", SqlDbType.NVarChar);
			parameters.Add("@Note", SqlDbType.NVarChar);
			parameters.Add("@VendorReferenceNo", SqlDbType.NVarChar);
			parameters["@SysDocID"].SourceColumn = "SysDocID";
			parameters["@VoucherID"].SourceColumn = "VoucherID";
			parameters["@DivisionID"].SourceColumn = "DivisionID";
			parameters["@CompanyID"].SourceColumn = "CompanyID";
			parameters["@VendorID"].SourceColumn = "VendorID";
			parameters["@PurchaseFlow"].SourceColumn = "PurchaseFlow";
			parameters["@TransactionDate"].SourceColumn = "TransactionDate";
			parameters["@ShippingMethodID"].SourceColumn = "ShippingMethodID";
			parameters["@Status"].SourceColumn = "Status";
			parameters["@Reference"].SourceColumn = "Reference";
			parameters["@ContainerNumber"].SourceColumn = "ContainerNumber";
			parameters["@Port"].SourceColumn = "Port";
			parameters["@LoadingPort"].SourceColumn = "LoadingPort";
			parameters["@ETA"].SourceColumn = "ETA";
			parameters["@ATD"].SourceColumn = "ATD";
			parameters["@BOLNumber"].SourceColumn = "BOLNumber";
			parameters["@Shipper"].SourceColumn = "Shipper";
			parameters["@ClearingAgent"].SourceColumn = "ClearingAgent";
			parameters["@Weight"].SourceColumn = "Weight";
			parameters["@Note"].SourceColumn = "Note";
			parameters["@Value"].SourceColumn = "Value";
			parameters["@TransporterID"].SourceColumn = "TransporterID";
			parameters["@ContainerSizeID"].SourceColumn = "ContainerSizeID";
			parameters["@VendorReferenceNo"].SourceColumn = "VendorReferenceNo";
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

		private string GetInsertUpdatePOShipmentDetailsText(bool isUpdate)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddInsertUpdateParameters("PO_Shipment_Detail", new FieldValue("SysDocID", "@SysDocID"), new FieldValue("VoucherID", "@VoucherID"), new FieldValue("ProductID", "@ProductID"), new FieldValue("RowIndex", "@RowIndex"), new FieldValue("Quantity", "@Quantity"), new FieldValue("UnitPrice", "@UnitPrice"), new FieldValue("Description", "@Description"), new FieldValue("UnitID", "@UnitID"), new FieldValue("UnitQuantity", "@UnitQuantity"), new FieldValue("UnitFactor", "@UnitFactor"), new FieldValue("SourceVoucherID", "@SourceVoucherID"), new FieldValue("SourceSysDocID", "@SourceSysDocID"), new FieldValue("SourceRowIndex", "@SourceRowIndex"), new FieldValue("IsSourcedRow", "@IsSourcedRow"), new FieldValue("SourceDocType", "@SourceDocType"), new FieldValue("QuantityReceived", "@QuantityReceived"), new FieldValue("FactorType", "@FactorType"));
			if (isUpdate)
			{
				return sqlBuilder.GetUpdateExpression();
			}
			return sqlBuilder.GetInsertExpression();
		}

		private SqlCommand GetInsertUpdatePOShipmentDetailsCommand(bool isUpdate)
		{
			SqlParameterCollection parameters;
			if (isUpdate)
			{
				if (updateCommand != null)
				{
					updateCommand = null;
				}
				updateCommand = new SqlCommand(GetInsertUpdatePOShipmentDetailsText(isUpdate: true), base.DBConfig.Connection);
				updateCommand.CommandType = CommandType.Text;
				parameters = updateCommand.Parameters;
			}
			else
			{
				if (insertCommand != null)
				{
					insertCommand = null;
				}
				insertCommand = new SqlCommand(GetInsertUpdatePOShipmentDetailsText(isUpdate: false), base.DBConfig.Connection);
				insertCommand.CommandType = CommandType.Text;
				parameters = insertCommand.Parameters;
			}
			parameters.Add("@SysDocID", SqlDbType.NVarChar);
			parameters.Add("@VoucherID", SqlDbType.NVarChar);
			parameters.Add("@ProductID", SqlDbType.NVarChar);
			parameters.Add("@RowIndex", SqlDbType.Int);
			parameters.Add("@Quantity", SqlDbType.Real);
			parameters.Add("@Description", SqlDbType.NVarChar);
			parameters.Add("@UnitID", SqlDbType.NVarChar);
			parameters.Add("@UnitQuantity", SqlDbType.Real);
			parameters.Add("@UnitPrice", SqlDbType.Decimal);
			parameters.Add("@UnitFactor", SqlDbType.Decimal);
			parameters.Add("@FactorType", SqlDbType.NVarChar);
			parameters.Add("@SourceVoucherID", SqlDbType.NVarChar);
			parameters.Add("@SourceSysDocID", SqlDbType.NVarChar);
			parameters.Add("@SourceRowIndex", SqlDbType.Int);
			parameters.Add("@IsSourcedRow", SqlDbType.Int);
			parameters.Add("@SourceDocType", SqlDbType.Int);
			parameters.Add("@QuantityReceived", SqlDbType.Real);
			parameters["@SysDocID"].SourceColumn = "SysDocID";
			parameters["@VoucherID"].SourceColumn = "VoucherID";
			parameters["@ProductID"].SourceColumn = "ProductID";
			parameters["@RowIndex"].SourceColumn = "RowIndex";
			parameters["@Quantity"].SourceColumn = "Quantity";
			parameters["@Description"].SourceColumn = "Description";
			parameters["@UnitPrice"].SourceColumn = "UnitPrice";
			parameters["@UnitID"].SourceColumn = "UnitID";
			parameters["@UnitQuantity"].SourceColumn = "UnitQuantity";
			parameters["@UnitFactor"].SourceColumn = "UnitFactor";
			parameters["@FactorType"].SourceColumn = "FactorType";
			parameters["@SourceVoucherID"].SourceColumn = "SourceVoucherID";
			parameters["@SourceSysDocID"].SourceColumn = "SourceSysDocID";
			parameters["@SourceRowIndex"].SourceColumn = "SourceRowIndex";
			parameters["@SourceDocType"].SourceColumn = "SourceDocType";
			parameters["@IsSourcedRow"].SourceColumn = "IsSourcedRow";
			parameters["@QuantityReceived"].SourceColumn = "QuantityReceived";
			if (isUpdate)
			{
				return updateCommand;
			}
			return insertCommand;
		}

		private bool ValidateData(POShipmentData journalData)
		{
			return true;
		}

		public bool CanUpdate(string sysDocID, string voucherNumber, SqlTransaction sqlTransaction)
		{
			string exp = "Select Count(*) FROM PO_Shipment_Detail POD\r\n                                WHERE SysdocID='" + sysDocID + "' AND VoucherID='" + voucherNumber + "' Having SUM(ISNULL(QuantityReceived,0))>0";
			object obj = ExecuteScalar(exp, sqlTransaction);
			if (obj != null && obj.ToString() != "" && int.Parse(obj.ToString()) > 0)
			{
				return false;
			}
			return true;
		}

		public bool InsertUpdatePOShipment(POShipmentData poShipmentData, bool isUpdate)
		{
			bool flag = true;
			SqlCommand insertUpdatePOShipmentCommand = GetInsertUpdatePOShipmentCommand(isUpdate);
			try
			{
				DataRow dataRow = poShipmentData.POShipmentTable.Rows[0];
				SqlTransaction sqlTransaction = base.DBConfig.StartNewTransaction();
				string text = dataRow["VoucherID"].ToString();
				string sysDocID = dataRow["SysDocID"].ToString();
				if (isUpdate && !CanUpdate(sysDocID, text, sqlTransaction))
				{
					throw new CompanyException("Unable to update. Some of the items has been already received.", 1037);
				}
				if (!isUpdate && new SystemDocuments(base.DBConfig).ExistDocumentNumber("PO_Shipment", "VoucherID", dataRow["SysDocID"].ToString(), text, sqlTransaction))
				{
					throw new CompanyException("Document number already exist.", 1046);
				}
				foreach (DataRow row in poShipmentData.POShipmentDetailTable.Rows)
				{
					row["SysDocID"] = dataRow["SysDocID"];
					row["VoucherID"] = dataRow["VoucherID"];
					string text2 = row["ProductID"].ToString();
					string text3 = "";
					object fieldValue = new Databases(base.DBConfig).GetFieldValue("Product", "UnitID", "ProductID", text2, sqlTransaction);
					if (fieldValue != null)
					{
						text3 = fieldValue.ToString();
					}
					if (text3 != "" && row["UnitID"] != DBNull.Value && row["UnitID"].ToString() != text3)
					{
						DataRow obj = new Products(base.DBConfig).GetProductUnitRow(text2, row["UnitID"].ToString()) ?? throw new CompanyException("One of the selected units is not assigned to item.\nItem:" + text2 + "\nUnit:" + row["UnitID"].ToString());
						float num = float.Parse(obj["Factor"].ToString());
						string text4 = obj["FactorType"].ToString();
						float num2 = float.Parse(row["Quantity"].ToString());
						row["UnitFactor"] = num;
						row["FactorType"] = text4;
						row["UnitQuantity"] = row["Quantity"];
						num2 = ((!(text4 == "M")) ? float.Parse(Math.Round(num2 * num, 5).ToString()) : float.Parse(Math.Round(num2 / num, 5).ToString()));
						row["Quantity"] = num2;
					}
				}
				insertUpdatePOShipmentCommand.Transaction = sqlTransaction;
				flag = (isUpdate ? (flag & Update(poShipmentData, "PO_Shipment", insertUpdatePOShipmentCommand)) : (flag & Insert(poShipmentData, "PO_Shipment", insertUpdatePOShipmentCommand)));
				insertUpdatePOShipmentCommand = GetInsertUpdatePOShipmentDetailsCommand(isUpdate: false);
				insertUpdatePOShipmentCommand.Transaction = sqlTransaction;
				if (isUpdate)
				{
					flag &= DeletePOShipmentDetailsRows(sysDocID, text, sqlTransaction);
				}
				if (poShipmentData.Tables["PO_Shipment_Detail"].Rows.Count > 0)
				{
					flag &= Insert(poShipmentData, "PO_Shipment_Detail", insertUpdatePOShipmentCommand);
				}
				Hashtable hashtable = new Hashtable();
				foreach (DataRow row2 in poShipmentData.POShipmentDetailTable.Rows)
				{
					string productID = row2["ProductID"].ToString();
					string text5 = row2["SourceSysDocID"].ToString();
					string text6 = row2["SourceVoucherID"].ToString();
					int rowIndex = int.Parse(row2["SourceRowIndex"].ToString());
					if (!string.IsNullOrEmpty(row2["SourceSysDocID"].ToString()) && !string.IsNullOrEmpty(row2["IsSourcedRow"].ToString()))
					{
						float quantity = (row2["UnitQuantity"] == DBNull.Value) ? float.Parse(row2["Quantity"].ToString()) : float.Parse(row2["UnitQuantity"].ToString());
						flag &= new PurchaseOrder(base.DBConfig).UpdateRowShippedQuantity(text5, text6, rowIndex, productID, quantity, sqlTransaction);
						if (!hashtable.ContainsKey(text6))
						{
							hashtable.Add(text6, text5);
						}
					}
				}
				foreach (string key in hashtable.Keys)
				{
					flag &= new PurchaseOrder(base.DBConfig).CloseShippedOrder(hashtable[key].ToString(), key, sqlTransaction);
				}
				if (!flag)
				{
					throw new Exception("Failed to save the transaction because of an unexpected error. Please try again.");
				}
				flag &= UpdateTableRowInsertUpdateInfo("PO_Shipment", "SysDocID", dataRow["SysDocID"].ToString(), "VoucherID", dataRow["VoucherID"].ToString(), sqlTransaction, !isUpdate);
				string entityName = "Purchase Order";
				flag = (isUpdate ? (flag & AddActivityLog(entityName, text, sysDocID, ActivityTypes.Update, sqlTransaction)) : (flag & AddActivityLog(entityName, text, sysDocID, ActivityTypes.Add, sqlTransaction)));
				if (!isUpdate)
				{
					flag &= new SystemDocuments(base.DBConfig).UpdateNextDocumentNumber(dataRow["SysDocID"].ToString(), dataRow["VoucherID"].ToString(), "PO_Shipment", "VoucherID", sqlTransaction);
				}
				if (!flag)
				{
					return flag;
				}
				new Approval(base.DBConfig).CreateTransactionApprovalTasks(SysDocTypes.PackingList, sysDocID, text, "PO_Shipment", sqlTransaction);
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

		public POShipmentData GetPOShipmentByID(string sysDocID, string voucherID)
		{
			try
			{
				POShipmentData pOShipmentData = new POShipmentData();
				string textCommand = "SELECT *, CASE WHEN (SELECT COUNT(*) FROM Purchase_Receipt PR \r\n                                WHERE POS.SysDocID = PR.SourceSysDocID AND POS.VoucherID = PR.SourceVoucherID)>0 THEN 'True' ELSE 'False' END  AS IsPLReceived ,T.SysDocID AS GRNSysDocID,T.VoucherID AS GRNVoucherID\r\n                                FROM PO_Shipment POS LEFT OUTER JOIN (SELECT TOP 1 SysDocID,VoucherID,SourceSysDocID,SourceVoucherID FROM Purchase_Receipt PR \r\n                                WHERE  PR.SourceSysDocID = '" + sysDocID + "' AND   PR.SourceVoucherID = '" + voucherID + "') T ON T.SourceSysDocID = POS.SysDocID AND T.SourceVoucherID = POS.VoucherID\r\n                                WHERE POS.VoucherID='" + voucherID + "' AND POS.SysDocID='" + sysDocID + "'";
				FillDataSet(pOShipmentData, "PO_Shipment", textCommand);
				if (pOShipmentData == null || pOShipmentData.Tables.Count == 0 || pOShipmentData.Tables["PO_Shipment"].Rows.Count == 0)
				{
					return null;
				}
				textCommand = "SELECT TD.*,Product.Description,Product.ItemType,Product.Attribute1,Product.Attribute2,Product.Attribute3,Product.MatrixParentID, Product.IsTrackLot, Product.IsTrackSerial\r\n                        FROM PO_Shipment_Detail TD INNER JOIN Product ON TD.ProductID=Product.ProductID\r\n                        WHERE VoucherID='" + voucherID + "' AND SysDocID='" + sysDocID + "'";
				FillDataSet(pOShipmentData, "PO_Shipment_Detail", textCommand);
				textCommand = "SELECT DISTINCT PO.SysDocID, PO.VoucherID FROM Purchase_Order PO INNER JOIN PO_Shipment_Detail PR ON PO.SysDocID=PR.SourceSysDocID AND PO.VoucherID=PR.SourceVoucherID\r\n\t\t\t\t\t\tWHERE PR.VoucherID='" + voucherID + "' AND PR.SysDocID='" + sysDocID + "'";
				FillDataSet(pOShipmentData, "Purchase_Order", textCommand);
				textCommand = "SELECT DISTINCT PID.SysDocID, PID.VoucherID FROM Purchase_Receipt PID INNER JOIN PO_Shipment POS ON  PID.SourceSysDocID = POS.SysDocID AND PID.SourceVoucherID = POS.VoucherID\r\n\t\t\t\t\t\tWHERE POS.VoucherID='" + voucherID + "' AND POS.SysDocID='" + sysDocID + "'";
				FillDataSet(pOShipmentData, "Purchase_Receipt", textCommand);
				return pOShipmentData;
			}
			catch
			{
				throw;
			}
		}

		public DataSet GetOpenShipmentsSummary(string vendorID)
		{
			try
			{
				DataSet dataSet = new DataSet();
				string text = "SELECT SO.SysDocID [Doc ID],SO.VoucherID [Number],TransactionDate AS [Date],ContainerNumber [Container No], SO.VendorID + '-' + C.VendorName AS [Vendor] FROM PO_Shipment SO\r\n                             INNER JOIN Vendor C ON SO.VendorID=C.VendorID  WHERE ISNULL(IsVoid,'False')='False' AND Status IN (1,2) ";
				if (vendorID != "")
				{
					text = text + " AND (SO.VendorID='" + vendorID + "' OR ParentVendorID = '" + vendorID + "')";
				}
				FillDataSet(dataSet, "PO_Shipment", text);
				return dataSet;
			}
			catch
			{
				throw;
			}
		}

		internal bool UpdateRowReceivedQuantity(string sysDocID, string voucherID, int rowIndex, float quantity, SqlTransaction sqlTransaction)
		{
			DataSet dataSet = new DataSet();
			float result = 0f;
			float result2 = 0f;
			try
			{
				string textCommand = "SELECT Quantity,UnitQuantity,QuantityReceived FROM PO_Shipment_Detail WHERE SysDocID='" + sysDocID + "' AND VoucherID='" + voucherID + "' AND RowIndex=" + rowIndex.ToString();
				FillDataSet(dataSet, "Product", textCommand);
				string exp = "SELECT OptionValue FROM Company_Option WHERE OptionID = " + 130;
				object obj = ExecuteScalar(exp, sqlTransaction);
				bool result3 = false;
				if (obj != null)
				{
					bool.TryParse(obj.ToString(), out result3);
				}
				if (dataSet != null && dataSet.Tables[0].Rows.Count > 0)
				{
					DataRow dataRow = dataSet.Tables[0].Rows[0];
					if (dataRow["UnitQuantity"] != DBNull.Value)
					{
						float.TryParse(dataRow["UnitQuantity"].ToString(), out result);
					}
					else
					{
						float.TryParse(dataRow["Quantity"].ToString(), out result);
					}
					float.TryParse(dataRow["QuantityReceived"].ToString(), out result2);
					if (!result3 && result < result2 + quantity)
					{
						throw new CompanyException("Received quantity cannot be greater than packing list quantity.", 1036);
					}
				}
				result2 += quantity;
				textCommand = "UPDATE PO_Shipment_Detail SET QuantityReceived=" + result2.ToString() + " WHERE SysDocID='" + sysDocID + "' AND VoucherID='" + voucherID + "' AND RowIndex=" + rowIndex.ToString();
				return ExecuteNonQuery(textCommand, sqlTransaction) > 0;
			}
			catch
			{
				throw;
			}
		}

		internal bool CloseReceivedShipment(string sysDocID, string voucherID, SqlTransaction sqlTransaction)
		{
			try
			{
				string str = "SELECT COUNT(RowIndex)FROM PO_Shipment_Detail WHERE SysDocID='" + sysDocID + "' AND VoucherID='" + voucherID + "'";
				str += " AND CASE WHEN UnitQuantity IS NULL THEN Quantity ELSE UnitQuantity END - ISNULL(QuantityReceived,0) <= 0";
				object obj = ExecuteScalar(str, sqlTransaction);
				if (obj == null || int.Parse(obj.ToString()) == 0)
				{
					return true;
				}
				str = "UPDATE PO_Shipment SET Status= 3, IsReceived='True' WHERE SysDocID='" + sysDocID + "' AND VoucherID='" + voucherID + "'";
				return ExecuteNonQuery(str, sqlTransaction) > 0;
			}
			catch
			{
				throw;
			}
		}

		internal bool DeletePOShipmentDetailsRows(string sysDocID, string voucherID, SqlTransaction sqlTransaction)
		{
			bool flag = true;
			try
			{
				POShipmentData pOShipmentData = new POShipmentData();
				string textCommand = "SELECT SOD.*,ISVOID FROM PO_Shipment_Detail SOD INNER JOIN PO_Shipment SO ON SO.SysDocID=SOD.SysDocID AND SO.VOucherID=SOD.VoucherID\r\n                              WHERE SOD.SysDocID = '" + sysDocID + "' AND SOD.VoucherID = '" + voucherID + "'";
				FillDataSet(pOShipmentData, "PO_Shipment_Detail", textCommand, sqlTransaction);
				if (pOShipmentData.POShipmentDetailTable.Rows.Count == 0)
				{
					return true;
				}
				bool result = false;
				bool.TryParse(pOShipmentData.POShipmentDetailTable.Rows[0]["IsVoid"].ToString(), out result);
				if (!result)
				{
					foreach (DataRow row in pOShipmentData.POShipmentDetailTable.Rows)
					{
						string productID = row["ProductID"].ToString();
						string voucherID2 = row["SourceVoucherID"].ToString();
						string sysDocID2 = row["SourceSysDocID"].ToString();
						string s = row["SourceRowIndex"].ToString();
						float num = float.Parse(row["Quantity"].ToString());
						flag &= new PurchaseOrder(base.DBConfig).UpdateRowShippedQuantity(sysDocID2, voucherID2, int.Parse(s), productID, -1f * num, sqlTransaction);
					}
				}
				textCommand = "DELETE FROM PO_Shipment_Detail WHERE SysDocID = '" + sysDocID + "' AND VoucherID = '" + voucherID + "'";
				return flag & Delete(textCommand, sqlTransaction);
			}
			catch
			{
				throw;
			}
		}

		public bool VoidPOShipment(string sysDocID, string voucherID, bool isVoid)
		{
			bool flag = true;
			try
			{
				SqlTransaction sqlTransaction = base.DBConfig.StartNewTransaction();
				if (!CanUpdate(sysDocID, voucherID, sqlTransaction))
				{
					throw new CompanyException("Unable to void. Some of the items has been already received.", 1037);
				}
				POShipmentData pOShipmentData = new POShipmentData();
				string textCommand = "SELECT * FROM PO_Shipment_Detail\r\n                              WHERE SysDocID = '" + sysDocID + "' AND VoucherID = '" + voucherID + "'";
				FillDataSet(pOShipmentData, "PO_Shipment_Detail", textCommand, sqlTransaction);
				textCommand = "SELECT DISTINCT SourceSysDocID,SourceVoucherID FROM PO_Shipment_Detail WHERE VoucherID='" + voucherID + "' AND SysDocID='" + sysDocID + "'";
				DataSet dataSet = new DataSet();
				FillDataSet(dataSet, "Purchase_Order", textCommand, sqlTransaction);
				foreach (DataRow row in pOShipmentData.POShipmentDetailTable.Rows)
				{
					string productID = row["ProductID"].ToString();
					string voucherID2 = row["SourceVoucherID"].ToString();
					string sysDocID2 = row["SourceSysDocID"].ToString();
					string s = row["SourceRowIndex"].ToString();
					float num = float.Parse(row["Quantity"].ToString());
					flag &= new PurchaseOrder(base.DBConfig).UpdateRowShippedQuantity(sysDocID2, voucherID2, int.Parse(s), productID, -1f * num, sqlTransaction);
				}
				if (dataSet != null && dataSet.Tables.Count > 0)
				{
					foreach (DataRow row2 in dataSet.Tables[0].Rows)
					{
						string sysDocID3 = row2["SourceSysDocID"].ToString();
						string voucherID3 = row2["SourceVoucherID"].ToString();
						flag &= new PurchaseOrder(base.DBConfig).CloseShippedOrder(sysDocID3, voucherID3, sqlTransaction);
					}
				}
				textCommand = "UPDATE PO_Shipment SET IsVoid = '" + isVoid.ToString() + "' WHERE SysDocID='" + sysDocID + "' AND VoucherID = '" + voucherID + "'";
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
				AddActivityLog("PO Shipment", voucherID, sysDocID, activityType, sqlTransaction);
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

		public bool DeletePOShipment(string sysDocID, string voucherID)
		{
			bool flag = true;
			try
			{
				string text = "";
				SqlTransaction sqlTransaction = base.DBConfig.StartNewTransaction();
				if (!CanUpdate(sysDocID, voucherID, sqlTransaction))
				{
					throw new CompanyException("Unable to delete. Some of the items has been already received.", 1037);
				}
				text = "SELECT DISTINCT SourceSysDocID,SourceVoucherID FROM PO_Shipment_Detail WHERE VoucherID='" + voucherID + "' AND SysDocID='" + sysDocID + "'";
				DataSet dataSet = new DataSet();
				FillDataSet(dataSet, "Purchase_Order", text, sqlTransaction);
				flag &= DeletePOShipmentDetailsRows(sysDocID, voucherID, sqlTransaction);
				text = "DELETE FROM PO_Shipment WHERE SysDocID='" + sysDocID + "' AND VoucherID = '" + voucherID + "'";
				flag &= (ExecuteNonQuery(text, sqlTransaction) >= 0);
				if (dataSet != null && dataSet.Tables.Count > 0)
				{
					foreach (DataRow row in dataSet.Tables[0].Rows)
					{
						string sysDocID2 = row["SourceSysDocID"].ToString();
						string voucherID2 = row["SourceVoucherID"].ToString();
						flag &= new PurchaseOrder(base.DBConfig).CloseShippedOrder(sysDocID2, voucherID2, sqlTransaction);
					}
				}
				if (!flag)
				{
					return flag;
				}
				ActivityTypes activityType = ActivityTypes.Delete;
				AddActivityLog("PO Shipment", voucherID, sysDocID, activityType, sqlTransaction);
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

		public DataSet GetOpenShipmentsReport()
		{
			try
			{
				string text = "";
				DataSet dataSet = new DataSet();
				text = "SELECT POS.VendorID + ' - ' + VendorName AS Vendor,ContainerNumber ,PortName,PONUmber,ETA,\r\n                            ShippingMethodName,Shipper,POS.Note \r\n                            FROM PO_Shipment POS\r\n                            INNER JOIN Vendor V ON POS.VendorID=V.VendorID\r\n                            Left OUTER JOIN Port ON POS.Port=Port.PortID\r\n                            LEFT OUTER JOIN Shipping_Method SM ON SM.ShippingMethodID=POS.ShippingMethodID\r\n                            WHERE ISNULL(IsVoid,'False')='False' AND ISNULL(IsReceived,0)= 0 ORDER BY POS.VendorID,ETA";
				FillDataSet(dataSet, "Shipments", text);
				return dataSet;
			}
			catch
			{
				throw;
			}
		}

		public DataSet GetPOShipmentToPrint(string sysDocID, string[] voucherID)
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
				string cmdText = "SELECT  DISTINCT  SI.*,Vendor.Vendorname,VA.AddressPrintFormat AS VendorAddress,ShippingMethodName\r\n                                FROM  PO_Shipment SI INNER JOIN Vendor ON SI.VendorID=Vendor.VendorID   \r\n                                LEFT OUTER JOIN Vendor_Address VA ON VA.VendorID=SI.VendorID AND VA.AddressID='PRIMARY'     \r\n                                LEFT OUTER JOIN Shipping_Method SM ON SM.ShippingMethodID=SI.ShippingMethodID    \r\n                                WHERE SysDocID = '" + sysDocID + "' AND VoucherID IN (" + text + ")";
				SqlCommand sqlCommand = new SqlCommand(cmdText);
				FillDataSet(dataSet, "PO_Shipment", sqlCommand);
				if (dataSet == null || dataSet.Tables.Count == 0 || dataSet.Tables["PO_Shipment"].Rows.Count == 0)
				{
					return null;
				}
				cmdText = "SELECT     SysDocID,VoucherID,SourceVoucherID,PD.ProductID,PD.Description,ISNULL(PD.UnitQuantity,PD.Quantity) AS Quantity,\r\n                        PD.UnitPrice AS UnitPrice,\r\n                        ISNULL(PD.UnitQuantity,PD.Quantity)*ISNULL(PD.UnitPrice,0) AS Total,PD.UnitID,CAST(P.photo AS VARBINARY(Max)) AS Photo\r\n                        FROM   PO_Shipment_Detail PD\r\n                        LEFT JOIN Product P ON P.ProductID=PD.ProductID \r\n                        WHERE SysDocID='" + sysDocID + "' AND VoucherID IN (" + text + ")";
				FillDataSet(dataSet, "PO_Shipment_Detail", cmdText);
				dataSet.Relations.Add("POShipment", new DataColumn[2]
				{
					dataSet.Tables["PO_Shipment"].Columns["SysDocID"],
					dataSet.Tables["PO_Shipment"].Columns["VoucherID"]
				}, new DataColumn[2]
				{
					dataSet.Tables["PO_Shipment_Detail"].Columns["SysDocID"],
					dataSet.Tables["PO_Shipment_Detail"].Columns["VoucherID"]
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
			string text3 = "SELECT     SysDocID [Doc ID],VoucherID [Doc Number],INV.VendorID [Vendor Code],VendorName [Vendor Name],TransactionDate [Invoice Date], INV.ETA                            \r\n                            FROM         PO_Shipment INV\r\n                            Inner JOIN Vendor ON VENDOR.VendorID=INV.VendorID";
			if (from != DateTime.MinValue)
			{
				text3 = text3 + " WHERE TransactionDate Between '" + text + "' AND '" + text2 + "'";
			}
			if (!showVoid)
			{
				text3 += " AND ISNULL(IsVoid,'False')='False'";
			}
			SqlCommand sqlCommand = new SqlCommand(text3);
			FillDataSet(dataSet, "PO_Shipment", sqlCommand);
			return dataSet;
		}

		public DataSet GetLastPOShipment(string poSysDocID, string poVoucherID)
		{
			DataSet dataSet = new DataSet();
			SqlCommand sqlCommand = new SqlCommand("SELECT TOP 1 POS.* \r\n                                    FROM PO_Shipment POS INNER JOIN PO_Shipment_Detail POSD\r\n                                    ON POS.SysDocID = POSD.SysDocID AND POS.VoucherID = POSD.VoucherID\r\n                                    WHERE POSD.SourceSysDocID = '" + poSysDocID + "' AND POSD.SourceVoucherID = '" + poVoucherID + "' ORDER BY TransactionDate DESC ");
			FillDataSet(dataSet, "PO_Shipment", sqlCommand);
			return dataSet;
		}

		public bool SetOrderStatus(string sysDocID, string voucherID, PurchaseOrderStatus status)
		{
			try
			{
				string exp = "UPDATE PO_Shipment SET Status= " + (int)status + " WHERE SysDocID='" + sysDocID + "' AND VoucherID='" + voucherID + "'";
				return ExecuteNonQuery(exp) > 0;
			}
			catch
			{
				throw;
			}
		}

		public DataSet GetBOLListForPayment(bool POMultipleBOL)
		{
			try
			{
				DataSet dataSet = new DataSet();
				string textCommand = POMultipleBOL ? "SELECT   SysDocID [Doc ID],VoucherID [Doc Number],BOLNumber [BOL No],ContainerNumber [Container No],INV.VendorID [Vendor Code],VendorName [Vendor Name],TransactionDate [Invoice Date], INV.ETA                            \r\n                            FROM   PO_Shipment INV\r\n                            Inner JOIN Vendor ON VENDOR.VendorID=INV.VendorID WHERE ISNULL(IsVoid,'False')='False' AND BOLNumber IN (SELECT  ISNULL(BOLNo,'') FROM Purchase_Order_NonInv)" : "SELECT   SysDocID [Doc ID],VoucherID [Doc Number],BOLNumber [BOL No],ContainerNumber [Container No],INV.VendorID [Vendor Code],VendorName [Vendor Name],TransactionDate [Invoice Date], INV.ETA                            \r\n                            FROM   PO_Shipment INV\r\n                            Inner JOIN Vendor ON VENDOR.VendorID=INV.VendorID WHERE ISNULL(IsVoid,'False')='False' AND NOT BOLNumber IN (SELECT  ISNULL(BOLNo,'') FROM Purchase_Order_NonInv)";
				FillDataSet(dataSet, "PO_Shipment", textCommand);
				return dataSet;
			}
			catch
			{
				throw;
			}
		}

		public DataSet GetPackingList()
		{
			try
			{
				DataSet dataSet = new DataSet();
				string textCommand = "SELECT  SysDocID [Doc ID],VoucherID [Number], INV.BOLNumber,INV.VendorID [Vendor Code],VendorName [Vendor Name],INV.ContainerNumber [ContainerNumber],TransactionDate [Invoice Date]\r\n                            FROM   PO_Shipment INV\r\n                             Inner JOIN Vendor ON VENDOR.VendorID=INV.VendorID WHERE ISNULL(IsVoid,'False')='False' AND NOT BOLNumber IN (SELECT  ISNULL(BOLNumber,'') FROM Purchase_Cost_Entry)";
				FillDataSet(dataSet, "PO_Shipment", textCommand);
				return dataSet;
			}
			catch
			{
				throw;
			}
		}

		public DataSet GetContainerStatusList()
		{
			try
			{
				DataSet dataSet = new DataSet();
				string textCommand = "SELECT Distinct  SysDocID [Doc ID],VoucherID [Number], CT.BOLNumber,CT.VendorID [Vendor Code],VendorName [Vendor Name],CT.ContainerNumber [ContainerNumber]\r\n                            FROM PO_Shipment CT\r\n                             Inner JOIN Vendor ON VENDOR.VendorID=CT.VendorID WHERE ISNULL(IsVoid,'False')='False' and CT.ContainerNumber IN(Select ContainerNumber from Container_Tracking)";
				FillDataSet(dataSet, "PO_Shipment", textCommand);
				return dataSet;
			}
			catch
			{
				throw;
			}
		}

		public DataSet GetBOLList()
		{
			try
			{
				DataSet dataSet = new DataSet();
				string textCommand = "SELECT  SysDocID [Doc ID],VoucherID [Number], INV.BOLNumber,INV.VendorID [Vendor Code],VendorName [Vendor Name],INV.ContainerNumber [ContainerNumber],TransactionDate [Invoice Date]\r\n                            FROM   PO_Shipment INV\r\n                             Inner JOIN Vendor ON VENDOR.VendorID=INV.VendorID WHERE ISNULL(IsVoid,'False')='False'";
				FillDataSet(dataSet, "PO_Shipment", textCommand);
				return dataSet;
			}
			catch
			{
				throw;
			}
		}

		public DataSet GetBOLContainerList()
		{
			try
			{
				DataSet dataSet = new DataSet();
				string textCommand = "SELECT  SysDocID [Doc ID],VoucherID [Number], INV.BOLNumber,INV.VendorID [Vendor Code],VendorName [Vendor Name],INV.ContainerNumber [ContainerNumber],TransactionDate [Invoice Date]\r\n                            FROM   PO_Shipment INV\r\n                             Inner JOIN Vendor ON VENDOR.VendorID=INV.VendorID WHERE ISNULL(IsVoid,'False')='False' AND  NOT INV.ContainerNumber IN (SELECT  ISNULL(ContainerNumber,'') FROM Bill_Of_Lading_Detail)";
				FillDataSet(dataSet, "PO_Shipment", textCommand);
				return dataSet;
			}
			catch
			{
				throw;
			}
		}

		public DataSet GetContainerList()
		{
			try
			{
				DataSet dataSet = new DataSet();
				string textCommand = "SELECT Distinct  INV.SysDocID [Doc ID],INV.VoucherID [Number], INV.BOLNumber,INV.VendorID [Vendor Code],INV.ContainerNumber [ContainerNumber]\r\n                            \r\n                            FROM   PO_Shipment INV\r\n\t\t\t\t\t\t\tLEFT JOIN Container_Tracking CT ON INV.ContainerNumber=CT.ContainerNumber\r\n                             Inner JOIN Vendor ON VENDOR.VendorID=INV.VendorID WHERE ISNULL(INV.IsVoid,'False')='False'";
				FillDataSet(dataSet, "PO_Shipment", textCommand);
				return dataSet;
			}
			catch
			{
				throw;
			}
		}

		public POShipmentData GetContainerDetails(string containerNumber)
		{
			try
			{
				POShipmentData pOShipmentData = new POShipmentData();
				string text = "SELECT  INV.*,Vendor.VendorID, Vendor.VendorName\r\n                            FROM   PO_Shipment INV\r\n                             Inner JOIN Vendor ON VENDOR.VendorID=INV.VendorID WHERE ISNULL(IsVoid,'False')='False' AND NOT BOLNumber IN (SELECT  ISNULL(BOLNumber,'') FROM Purchase_Cost_Entry)";
				if (containerNumber != "")
				{
					text = text + " AND INV.ContainerNumber='" + containerNumber + "'";
				}
				FillDataSet(pOShipmentData, "PO_Shipment", text);
				return pOShipmentData;
			}
			catch
			{
				throw;
			}
		}

		public POShipmentData GetContainerDetailsNew(string containerNumber)
		{
			try
			{
				POShipmentData pOShipmentData = new POShipmentData();
				string text = "SELECT  INV.*,Vendor.VendorID, Vendor.VendorName,CT.Container_Status\r\n                            FROM   PO_Shipment INV\r\n                             Inner JOIN Vendor ON VENDOR.VendorID=INV.VendorID \r\n\t\t\t\t\t\t\t LEFT JOIN Container_Tracking CT ON INV.ContainerNumber=CT.ContainerNumber\r\n\t\t\t\t\t\t\t WHERE ISNULL(INV.IsVoid,'False')='False'";
				if (containerNumber != "")
				{
					text = text + " AND INV.ContainerNumber='" + containerNumber + "'";
				}
				FillDataSet(pOShipmentData, "PO_Shipment", text);
				return pOShipmentData;
			}
			catch
			{
				throw;
			}
		}

		public DataSet GetContainerComboList()
		{
			DataSet dataSet = new DataSet();
			string textCommand = "select ContainerNumber [Code], ContainerNumber[Name], BOLNumber from PO_Shipment WHERE ISNULL(IsVoid,'False')='False' AND  NOT ContainerNumber IN (SELECT  ISNULL(ContainerNumber,'') FROM Bill_Of_Lading_Detail) ";
			FillDataSet(dataSet, "PO_Shipment", textCommand);
			return dataSet;
		}
	}
}
