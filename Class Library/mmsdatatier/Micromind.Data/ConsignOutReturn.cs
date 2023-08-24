using Micromind.Common;
using Micromind.Common.Data;
using Micromind.Common.Libraries;
using System;
using System.Collections;
using System.Data;
using System.Data.SqlClient;

namespace Micromind.Data
{
	public sealed class ConsignOutReturn : StoreObject
	{
		private const string CONSIGNOUTRETURN_TABLE = "ConsignOut_Return";

		private const string CONSIGNOUTRETURNDETAIL_TABLE = "ConsignOut_Return_Detail";

		private const string SYSDOCID_PARM = "@SysDocID";

		private const string VOUCHERID_PARM = "@VoucherID";

		private const string COMPANYID_PARM = "@CompanyID";

		private const string DIVISIONID_PARM = "@DivisionID";

		private const string CUSTOMERID_PARM = "@CustomerID";

		private const string TRANSACTIONDATE_PARM = "@TransactionDate";

		private const string ROWINDEX_PARM = "@RowIndex";

		private const string CONSIGNSYSDOCID_PARM = "@ConsignSysDocID";

		private const string CONSIGNVOUCHERID_PARM = "@ConsignVoucherID";

		private const string CONSIGNROWINDEX_PARM = "@ConsignRowIndex";

		private const string SHIPPINGADDRESSID_PARM = "@ShippingAddressID";

		private const string CUSTOMERADDRESS_PARM = "@CustomerAddress";

		private const string STATUS_PARM = "@Status";

		private const string SHIPPINGMETHODID_PARM = "@ShippingMethodID";

		private const string REFERENCE_PARM = "@Reference";

		private const string NOTE_PARM = "@Note";

		private const string PONUMBER_PARM = "@PONumber";

		private const string ISVOID_PARM = "@IsVoid";

		private const string PRODUCTID_PARM = "@ProductID";

		private const string QUANTITY_PARM = "@Quantity";

		private const string QUANTITYRETURNED_PARM = "@QuantityReturned";

		private const string DESCRIPTION_PARM = "@Description";

		private const string UNITID_PARM = "@UnitID";

		private const string UNITQUANTITY_PARM = "@UnitQuantity";

		private const string UNITFACTOR_PARM = "@UnitFactor";

		private const string FACTORTYPE_PARM = "@FactorType";

		private const string LOCATIONID_PARM = "@LocationID";

		private const string CONSIGNLOCATIONID_PARM = "@ConsignLocationID";

		private const string ORDERVOUCHERID_PARM = "@ConsignSysDocID";

		private const string ORDERSYSDOCID_PARM = "@ConsignVoucherID";

		private const string ORDERROWINDEX_PARM = "@ConsignRowIndex";

		private const string CREATEDBY_PARM = "@CreatedBy";

		private const string DATECREATED_PARM = "@DateCreated";

		private const string UPDATEDBY_PARM = "@UpdatedBy";

		private const string DATEUPDATED_PARM = "@DateUpdated";

		public ConsignOutReturn(Config config)
			: base(config)
		{
		}

		private string GetInsertUpdateConsignOutReturnText(bool isUpdate)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddInsertUpdateParameters("ConsignOut_Return", new FieldValue("SysDocID", "@SysDocID", isUpdateConditionField: true), new FieldValue("VoucherID", "@VoucherID", isUpdateConditionField: true), new FieldValue("DivisionID", "@DivisionID"), new FieldValue("CompanyID", "@CompanyID"), new FieldValue("CustomerID", "@CustomerID"), new FieldValue("TransactionDate", "@TransactionDate"), new FieldValue("ShippingAddressID", "@ShippingAddressID"), new FieldValue("ShippingMethodID", "@ShippingMethodID"), new FieldValue("CustomerAddress", "@CustomerAddress"), new FieldValue("Status", "@Status"), new FieldValue("ConsignSysDocID", "@ConsignSysDocID"), new FieldValue("ConsignVoucherID", "@ConsignVoucherID"), new FieldValue("PONumber", "@PONumber"), new FieldValue("Reference", "@Reference"), new FieldValue("Note", "@Note"));
			if (isUpdate)
			{
				sqlBuilder.AddInsertUpdateParameters("ConsignOut_Return", new FieldValue("DateUpdated", "@DateUpdated", isUpdateConditionField: true, checkForNullValue: true));
			}
			if (isUpdate)
			{
				return sqlBuilder.GetUpdateExpression();
			}
			return sqlBuilder.GetInsertExpression();
		}

		private SqlCommand GetInsertUpdateConsignOutReturnCommand(bool isUpdate)
		{
			SqlParameterCollection parameters;
			if (isUpdate)
			{
				if (updateCommand != null)
				{
					updateCommand = null;
				}
				updateCommand = new SqlCommand(GetInsertUpdateConsignOutReturnText(isUpdate: true), base.DBConfig.Connection);
				updateCommand.CommandType = CommandType.Text;
				parameters = updateCommand.Parameters;
			}
			else
			{
				if (insertCommand != null)
				{
					insertCommand = null;
				}
				insertCommand = new SqlCommand(GetInsertUpdateConsignOutReturnText(isUpdate: false), base.DBConfig.Connection);
				insertCommand.CommandType = CommandType.Text;
				parameters = insertCommand.Parameters;
			}
			parameters.Add("@SysDocID", SqlDbType.NVarChar);
			parameters.Add("@VoucherID", SqlDbType.NVarChar);
			parameters.Add("@DivisionID", SqlDbType.NVarChar);
			parameters.Add("@CompanyID", SqlDbType.TinyInt);
			parameters.Add("@CustomerID", SqlDbType.NVarChar);
			parameters.Add("@TransactionDate", SqlDbType.DateTime);
			parameters.Add("@ShippingAddressID", SqlDbType.NVarChar);
			parameters.Add("@CustomerAddress", SqlDbType.NVarChar);
			parameters.Add("@ShippingMethodID", SqlDbType.NVarChar);
			parameters.Add("@ConsignVoucherID", SqlDbType.NVarChar);
			parameters.Add("@ConsignSysDocID", SqlDbType.NVarChar);
			parameters.Add("@Status", SqlDbType.TinyInt);
			parameters.Add("@Reference", SqlDbType.NVarChar);
			parameters.Add("@PONumber", SqlDbType.NVarChar);
			parameters.Add("@Note", SqlDbType.NVarChar);
			parameters["@SysDocID"].SourceColumn = "SysDocID";
			parameters["@VoucherID"].SourceColumn = "VoucherID";
			parameters["@DivisionID"].SourceColumn = "DivisionID";
			parameters["@CompanyID"].SourceColumn = "CompanyID";
			parameters["@CustomerID"].SourceColumn = "CustomerID";
			parameters["@TransactionDate"].SourceColumn = "TransactionDate";
			parameters["@ConsignVoucherID"].SourceColumn = "ConsignVoucherID";
			parameters["@ConsignSysDocID"].SourceColumn = "ConsignSysDocID";
			parameters["@ShippingAddressID"].SourceColumn = "ShippingAddressID";
			parameters["@CustomerAddress"].SourceColumn = "CustomerAddress";
			parameters["@ShippingMethodID"].SourceColumn = "ShippingMethodID";
			parameters["@Status"].SourceColumn = "Status";
			parameters["@Reference"].SourceColumn = "Reference";
			parameters["@PONumber"].SourceColumn = "PONumber";
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

		private string GetInsertUpdateConsignOutReturnDetailsText(bool isUpdate)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddInsertUpdateParameters("ConsignOut_Return_Detail", new FieldValue("SysDocID", "@SysDocID"), new FieldValue("VoucherID", "@VoucherID"), new FieldValue("ProductID", "@ProductID"), new FieldValue("LocationID", "@LocationID"), new FieldValue("ConsignLocationID", "@ConsignLocationID"), new FieldValue("RowIndex", "@RowIndex"), new FieldValue("Quantity", "@Quantity"), new FieldValue("Description", "@Description"), new FieldValue("UnitID", "@UnitID"), new FieldValue("UnitQuantity", "@UnitQuantity"), new FieldValue("UnitFactor", "@UnitFactor"), new FieldValue("FactorType", "@FactorType"), new FieldValue("ConsignVoucherID", "@ConsignVoucherID"), new FieldValue("ConsignSysDocID", "@ConsignSysDocID"), new FieldValue("ConsignRowIndex", "@ConsignRowIndex"));
			if (isUpdate)
			{
				return sqlBuilder.GetUpdateExpression();
			}
			return sqlBuilder.GetInsertExpression();
		}

		private SqlCommand GetInsertUpdateConsignOutReturnDetailsCommand(bool isUpdate)
		{
			SqlParameterCollection parameters;
			if (isUpdate)
			{
				if (updateCommand != null)
				{
					updateCommand = null;
				}
				updateCommand = new SqlCommand(GetInsertUpdateConsignOutReturnDetailsText(isUpdate: true), base.DBConfig.Connection);
				updateCommand.CommandType = CommandType.Text;
				parameters = updateCommand.Parameters;
			}
			else
			{
				if (insertCommand != null)
				{
					insertCommand = null;
				}
				insertCommand = new SqlCommand(GetInsertUpdateConsignOutReturnDetailsText(isUpdate: false), base.DBConfig.Connection);
				insertCommand.CommandType = CommandType.Text;
				parameters = insertCommand.Parameters;
			}
			parameters.Add("@SysDocID", SqlDbType.NVarChar);
			parameters.Add("@VoucherID", SqlDbType.NVarChar);
			parameters.Add("@ProductID", SqlDbType.NVarChar);
			parameters.Add("@LocationID", SqlDbType.NVarChar);
			parameters.Add("@ConsignLocationID", SqlDbType.NVarChar);
			parameters.Add("@RowIndex", SqlDbType.Int);
			parameters.Add("@Quantity", SqlDbType.Real);
			parameters.Add("@Description", SqlDbType.NVarChar);
			parameters.Add("@UnitID", SqlDbType.NVarChar);
			parameters.Add("@UnitQuantity", SqlDbType.Real);
			parameters.Add("@UnitFactor", SqlDbType.Decimal);
			parameters.Add("@FactorType", SqlDbType.NVarChar);
			parameters.Add("@ConsignSysDocID", SqlDbType.NVarChar);
			parameters.Add("@ConsignVoucherID", SqlDbType.NVarChar);
			parameters.Add("@ConsignRowIndex", SqlDbType.Int);
			parameters["@SysDocID"].SourceColumn = "SysDocID";
			parameters["@VoucherID"].SourceColumn = "VoucherID";
			parameters["@ProductID"].SourceColumn = "ProductID";
			parameters["@LocationID"].SourceColumn = "LocationID";
			parameters["@ConsignLocationID"].SourceColumn = "ConsignLocationID";
			parameters["@RowIndex"].SourceColumn = "RowIndex";
			parameters["@Quantity"].SourceColumn = "Quantity";
			parameters["@Description"].SourceColumn = "Description";
			parameters["@UnitID"].SourceColumn = "UnitID";
			parameters["@UnitQuantity"].SourceColumn = "UnitQuantity";
			parameters["@UnitFactor"].SourceColumn = "UnitFactor";
			parameters["@FactorType"].SourceColumn = "FactorType";
			parameters["@ConsignVoucherID"].SourceColumn = "ConsignVoucherID";
			parameters["@ConsignSysDocID"].SourceColumn = "ConsignSysDocID";
			parameters["@ConsignRowIndex"].SourceColumn = "ConsignRowIndex";
			if (isUpdate)
			{
				return updateCommand;
			}
			return insertCommand;
		}

		private bool ValidateData(ConsignOutReturnData journalData)
		{
			return true;
		}

		public bool InsertUpdateConsignOutReturn(ConsignOutReturnData consignOutData, bool isUpdate)
		{
			bool flag = true;
			SqlCommand insertUpdateConsignOutReturnCommand = GetInsertUpdateConsignOutReturnCommand(isUpdate);
			try
			{
				DataRow dataRow = consignOutData.ConsignOutReturnTable.Rows[0];
				SqlTransaction sqlTransaction = base.DBConfig.StartNewTransaction();
				string text = dataRow["VoucherID"].ToString();
				string text2 = dataRow["SysDocID"].ToString();
				string text3 = dataRow["ConsignSysDocID"].ToString();
				string voucherID = dataRow["ConsignVoucherID"].ToString();
				if (!isUpdate && new SystemDocuments(base.DBConfig).ExistDocumentNumber("ConsignOut_Return", "VoucherID", dataRow["SysDocID"].ToString(), text, sqlTransaction))
				{
					base.DBConfig.EndTransaction(result: false);
					throw new CompanyException("Document number already exist.", 1046);
				}
				string text4 = new Databases(base.DBConfig).GetFieldValue("System_Document", "ConsignOutLocationID", "SysDocID", text3, sqlTransaction).ToString();
				if (text4 == "")
				{
					throw new CompanyException("There is no store assigned to this consignment out document.");
				}
				if (isUpdate)
				{
					flag &= DeleteConsignOutReturnDetailsRows(text2, text, isDeletingTransaction: false, sqlTransaction);
					flag &= new InventoryTransaction(base.DBConfig).DeleteInventoryTransaction(54, text2, text, isDeletingTransaction: false, sqlTransaction);
				}
				float num = 0f;
				foreach (DataRow row in consignOutData.ConsignOutReturnDetailTable.Rows)
				{
					row["SysDocID"] = dataRow["SysDocID"];
					row["VoucherID"] = dataRow["VoucherID"];
					string text5 = row["ProductID"].ToString();
					row["ConsignLocationID"] = text4;
					string idFieldValue = row["ConsignSysDocID"].ToString();
					string checkFieldValue = row["ConsignVoucherID"].ToString();
					int num2 = int.Parse(row["ConsignRowIndex"].ToString());
					object fieldValue = new Databases(base.DBConfig).GetFieldValue("Consign_Out_Detail", "QuantityReturned", "SysDocID", idFieldValue, "VoucherID", checkFieldValue, "RowIndex", num2, sqlTransaction);
					float num3 = 0f;
					if (fieldValue != null && fieldValue.ToString() != "")
					{
						num3 = float.Parse(fieldValue.ToString());
					}
					fieldValue = new Databases(base.DBConfig).GetFieldValue("Consign_Out_Detail", "QuantitySettled", "SysDocID", idFieldValue, "VoucherID", checkFieldValue, "RowIndex", num2, sqlTransaction);
					float num4 = 0f;
					if (fieldValue != null && fieldValue.ToString() != "")
					{
						num4 = float.Parse(fieldValue.ToString());
					}
					fieldValue = new Databases(base.DBConfig).GetFieldValue("Consign_Out_Detail", "Quantity", "SysDocID", idFieldValue, "VoucherID", checkFieldValue, "RowIndex", num2, sqlTransaction);
					float num5 = 0f;
					if (fieldValue != null && fieldValue.ToString() != "")
					{
						num5 = float.Parse(fieldValue.ToString());
					}
					if (num3 + num4 > num5)
					{
						throw new CompanyException("Returned quantity cannot be greater than balance quantity.", 1026);
					}
					num = float.Parse(row["Quantity"].ToString());
					num3 += num;
					UpdateRowReturnedQuantity(text3, voucherID, num2, num, sqlTransaction);
					string text6 = "";
					object fieldValue2 = new Databases(base.DBConfig).GetFieldValue("Product", "UnitID", "ProductID", text5, sqlTransaction);
					if (fieldValue2 != null)
					{
						text6 = fieldValue2.ToString();
					}
					if (text6 != "" && row["UnitID"] != DBNull.Value && row["UnitID"].ToString() != text6)
					{
						DataRow obj = new Products(base.DBConfig).GetProductUnitRow(text5, row["UnitID"].ToString()) ?? throw new CompanyException("One of the selected units is not assigned to item.\nItem:" + text5 + "\nUnit:" + row["UnitID"].ToString());
						float num6 = float.Parse(obj["Factor"].ToString());
						string text7 = obj["FactorType"].ToString();
						num = float.Parse(row["Quantity"].ToString());
						row["UnitFactor"] = num6;
						row["FactorType"] = text7;
						row["UnitQuantity"] = row["Quantity"];
						num = ((!(text7 == "M")) ? float.Parse(Math.Round(num * num6, 5).ToString()) : float.Parse(Math.Round(num / num6, 5).ToString()));
						row["Quantity"] = num;
					}
				}
				insertUpdateConsignOutReturnCommand.Transaction = sqlTransaction;
				flag = (isUpdate ? (flag & Update(consignOutData, "ConsignOut_Return", insertUpdateConsignOutReturnCommand)) : (flag & Insert(consignOutData, "ConsignOut_Return", insertUpdateConsignOutReturnCommand)));
				insertUpdateConsignOutReturnCommand = GetInsertUpdateConsignOutReturnDetailsCommand(isUpdate: false);
				insertUpdateConsignOutReturnCommand.Transaction = sqlTransaction;
				if (consignOutData.Tables["ConsignOut_Return_Detail"].Rows.Count > 0)
				{
					flag &= Insert(consignOutData, "ConsignOut_Return_Detail", insertUpdateConsignOutReturnCommand);
				}
				InventoryTransactionData inventoryTransactionData = new InventoryTransactionData();
				foreach (DataRow row2 in consignOutData.ConsignOutReturnDetailTable.Rows)
				{
					DataRow dataRow4 = inventoryTransactionData.InventoryTransactionTable.NewRow();
					dataRow4.BeginEdit();
					dataRow4["SysDocID"] = row2["SysDocID"];
					dataRow4["VoucherID"] = row2["VoucherID"];
					dataRow4["LocationID"] = text4;
					dataRow4["ProductID"] = row2["ProductID"];
					dataRow4["UnitID"] = row2["UnitID"];
					dataRow4["Quantity"] = -1f * float.Parse(row2["Quantity"].ToString());
					if (row2["UnitQuantity"] != DBNull.Value)
					{
						dataRow4["UnitQuantity"] = -1f * float.Parse(row2["UnitQuantity"].ToString());
					}
					else
					{
						dataRow4["UnitQuantity"] = DBNull.Value;
					}
					dataRow4["Factor"] = row2["UnitFactor"];
					dataRow4["FactorType"] = row2["FactorType"];
					dataRow4["Reference"] = dataRow["Reference"];
					dataRow4["RowIndex"] = row2["RowIndex"];
					dataRow4["SysDocType"] = (byte)54;
					dataRow4["UnitPrice"] = 0;
					dataRow4["TransactionDate"] = dataRow["TransactionDate"];
					dataRow4["TransactionType"] = (byte)13;
					dataRow4["DivisionID"] = dataRow["DivisionID"];
					dataRow4["CompanyID"] = dataRow["CompanyID"];
					dataRow4.EndEdit();
					inventoryTransactionData.InventoryTransactionTable.Rows.Add(dataRow4);
				}
				inventoryTransactionData.Merge(consignOutData.Tables["Product_Lot_Issue_Detail"]);
				flag &= new Products(base.DBConfig).InsertUpdateProductLotIssueDetail(consignOutData, isUpdate: false, sqlTransaction);
				flag &= new InventoryTransaction(base.DBConfig).InsertUpdateInventoryTransaction(inventoryTransactionData, isUpdate, sqlTransaction);
				inventoryTransactionData = new InventoryTransactionData();
				foreach (DataRow row3 in consignOutData.ConsignOutReturnDetailTable.Rows)
				{
					DataRow dataRow6 = inventoryTransactionData.InventoryTransactionTable.NewRow();
					dataRow6.BeginEdit();
					dataRow6["SysDocID"] = row3["SysDocID"];
					dataRow6["VoucherID"] = row3["VoucherID"];
					dataRow6["LocationID"] = row3["LocationID"];
					dataRow6["ProductID"] = row3["ProductID"];
					dataRow6["UnitID"] = row3["UnitID"];
					dataRow6["Quantity"] = row3["Quantity"];
					dataRow6["UnitQuantity"] = row3["UnitQuantity"];
					dataRow6["Factor"] = row3["UnitFactor"];
					dataRow6["FactorType"] = row3["FactorType"];
					dataRow6["Reference"] = dataRow["Reference"];
					dataRow6["UnitPrice"] = 0;
					dataRow6["RowIndex"] = row3["RowIndex"];
					dataRow6["SysDocType"] = (byte)54;
					dataRow6["TransactionDate"] = dataRow["TransactionDate"];
					dataRow6["TransactionType"] = (byte)13;
					dataRow6["DivisionID"] = dataRow["DivisionID"];
					dataRow6["CompanyID"] = dataRow["CompanyID"];
					dataRow6.EndEdit();
					inventoryTransactionData.InventoryTransactionTable.Rows.Add(dataRow6);
				}
				string textCommand = "SELECT PLD.*,PL.ProductionDate,PL.ExpiryDate FROM Product_Lot_Issue_Detail PLD INNER JOIN Product_Lot PL ON PL.LotNumber = PLD.LotNumber WHERE SysDocID = '" + text2 + "' AND VoucherID = '" + text + "'";
				DataSet dataSet = new DataSet();
				FillDataSet(dataSet, "Product_Lot_Issue_Detail", textCommand);
				DataTable dataTable = consignOutData.Tables["Product_Lot_Receiving_Detail"];
				dataTable.Rows.Clear();
				foreach (DataRow row4 in dataSet.Tables["Product_Lot_Issue_Detail"].Rows)
				{
					DataRow dataRow8 = dataTable.NewRow();
					dataRow8["ProductID"] = row4["ProductID"];
					dataRow8["LocationID"] = text4;
					dataRow8["LotNumber"] = row4["LotNumber"];
					dataRow8["ProductionDate"] = row4["ProductionDate"];
					dataRow8["ExpiryDate"] = row4["ExpiryDate"];
					dataRow8["LotQty"] = row4["SoldQty"];
					dataRow8["SysDocID"] = text2;
					dataRow8["VoucherID"] = text;
					dataRow8["RowIndex"] = row4["RowIndex"];
					dataTable.Rows.Add(dataRow8);
				}
				inventoryTransactionData.Merge(consignOutData.Tables["Product_Lot_Receiving_Detail"]);
				flag &= new Products(base.DBConfig).InsertUpdateProductLotReceivingDetail(consignOutData, isUpdate: false, sqlTransaction);
				flag &= new InventoryTransaction(base.DBConfig).InsertUpdateInventoryTransaction(inventoryTransactionData, isUpdate: false, sqlTransaction);
				GLData journalData = CreateGLData(consignOutData, sqlTransaction);
				flag &= new Journal(base.DBConfig).InsertUpdateJournal(journalData, isUpdate, sqlTransaction);
				flag &= UpdateInventoryTransactionRowID(text2, text, sqlTransaction);
				flag &= new ConsignOut(base.DBConfig).CloseOpenConsignment(text3, voucherID, sqlTransaction);
				if (!flag)
				{
					throw new Exception("Failed to save the transaction because of an unexpected error. Please try again.");
				}
				flag &= UpdateTableRowInsertUpdateInfo("ConsignOut_Return", "SysDocID", dataRow["SysDocID"].ToString(), "VoucherID", dataRow["VoucherID"].ToString(), sqlTransaction, !isUpdate);
				string entityName = "Consignment Out";
				flag = (isUpdate ? (flag & AddActivityLog(entityName, text, text2, ActivityTypes.Update, sqlTransaction)) : (flag & AddActivityLog(entityName, text, text2, ActivityTypes.Add, sqlTransaction)));
				if (isUpdate)
				{
					return flag;
				}
				flag &= new SystemDocuments(base.DBConfig).UpdateNextDocumentNumber(dataRow["SysDocID"].ToString(), dataRow["VoucherID"].ToString(), "ConsignOut_Return", "VoucherID", sqlTransaction);
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

		private bool UpdateInventoryTransactionRowID(string sysDocID, string voucherID, SqlTransaction sqlTransaction)
		{
			try
			{
				string exp = "UPDATE SID SET ITRowID = (SELECT TransactionID FROM Inventory_Transactions IT WHERE IT.SysDocID = SID.SysDocID AND IT.VoucherID = SID.VoucherID AND IT.RowIndex = SID.RowIndex) \r\n                                    FROM ConsignOut_Return_Detail SID  \r\n                                     where sid.SysDocID = '" + sysDocID + "' and sid.voucherid = '" + voucherID + "'";
				return ExecuteNonQuery(exp, sqlTransaction) > 0;
			}
			catch
			{
				throw;
			}
		}

		public ConsignOutReturnData GetConsignOutReturnByID(string sysDocID, string voucherID)
		{
			try
			{
				ConsignOutReturnData consignOutReturnData = new ConsignOutReturnData();
				string textCommand = "SELECT * FROM ConsignOut_Return WHERE VoucherID='" + voucherID + "' AND SysDocID='" + sysDocID + "'";
				FillDataSet(consignOutReturnData, "ConsignOut_Return", textCommand);
				if (consignOutReturnData == null || consignOutReturnData.Tables.Count == 0 || consignOutReturnData.Tables["ConsignOut_Return"].Rows.Count == 0)
				{
					return null;
				}
				textCommand = "SELECT SD.*,P.Description,P.ItemType,P.IsTrackLot,P.IsTrackSerial,COD.Quantity AS DeliveredQty,COD.RowIndex AS ConsignRowIndex,QuantitySettled,QuantityReturned, COD.Quantity - ISNULL(QuantitySettled,0)-ISNULL(QuantityReturned,0) + SD.Quantity AS BalanceQty\r\n                        FROM ConsignOut_Return_Detail SD INNER JOIN Product P ON SD.ProductID=P.ProductID\r\n                        INNER JOIN Consign_Out_Detail COD ON COD.SysDocID=SD.ConsignSysDocID\r\n                        AND COD.VoucherID=SD.ConsignVoucherID AND COD.RowIndex=SD.ConsignRowIndex\r\n                        WHERE SD.VoucherID='" + voucherID + "' AND SD.SysDocID='" + sysDocID + "'";
				FillDataSet(consignOutReturnData, "ConsignOut_Return_Detail", textCommand);
				DataSet transactionIssuesProductLots = new Products(base.DBConfig).GetTransactionIssuesProductLots(sysDocID, voucherID);
				if (consignOutReturnData.Tables.Contains("Product_Lot_Issue_Detail"))
				{
					consignOutReturnData.Tables.Remove("Product_Lot_Issue_Detail");
				}
				consignOutReturnData.Merge(transactionIssuesProductLots, preserveChanges: false);
				return consignOutReturnData;
			}
			catch
			{
				throw;
			}
		}

		internal bool DeleteConsignOutReturnDetailsRows(string sysDocID, string voucherID, bool isDeletingTransaction, SqlTransaction sqlTransaction)
		{
			bool flag = true;
			try
			{
				ConsignOutReturnData consignOutReturnData = new ConsignOutReturnData();
				string textCommand = "SELECT SO.ConsignSysDocID,SO.ConsignVoucherID, SOD.*,ISNULL(ISVOID,'False') AS IsVoid\r\n                                    FROM ConsignOut_Return_Detail SOD INNER JOIN ConsignOut_Return SO ON SO.SysDocID=SOD.SysDocID AND SO.VOucherID=SOD.VoucherID\r\n                                    WHERE SOD.SysDocID = '" + sysDocID + "' AND SOD.VoucherID = '" + voucherID + "'";
				FillDataSet(consignOutReturnData, "ConsignOut_Return_Detail", textCommand, sqlTransaction);
				if (consignOutReturnData.ConsignOutReturnDetailTable.Rows.Count == 0)
				{
					return true;
				}
				bool result = false;
				bool.TryParse(consignOutReturnData.ConsignOutReturnDetailTable.Rows[0]["IsVoid"].ToString(), out result);
				string sysDocID2 = consignOutReturnData.ConsignOutReturnDetailTable.Rows[0]["ConsignSysDocID"].ToString();
				string voucherID2 = consignOutReturnData.ConsignOutReturnDetailTable.Rows[0]["ConsignVoucherID"].ToString();
				if (!result)
				{
					flag &= new InventoryTransaction(base.DBConfig).DeleteInventoryTransaction(54, sysDocID, voucherID, isDeletingTransaction, sqlTransaction);
					foreach (DataRow row in consignOutReturnData.ConsignOutReturnDetailTable.Rows)
					{
						row["ProductID"].ToString();
						sysDocID2 = row["ConsignSysDocID"].ToString();
						voucherID2 = row["ConsignVoucherID"].ToString();
						string s = row["ConsignRowIndex"].ToString();
						float num = float.Parse(row["Quantity"].ToString());
						flag &= UpdateRowReturnedQuantity(sysDocID2, voucherID2, int.Parse(s), -1f * num, sqlTransaction);
					}
				}
				flag &= new ConsignOut(base.DBConfig).CloseOpenConsignment(sysDocID2, voucherID2, sqlTransaction);
				textCommand = "DELETE FROM Product_Lot_Issue_Detail WHERE SysDocID = '" + sysDocID + "' AND VoucherID = '" + voucherID + "'";
				flag &= Delete(textCommand, sqlTransaction);
				textCommand = "DELETE FROM ConsignOut_Return_Detail WHERE SysDocID = '" + sysDocID + "' AND VoucherID = '" + voucherID + "'";
				return flag & Delete(textCommand, sqlTransaction);
			}
			catch
			{
				throw;
			}
		}

		internal bool UpdateRowReturnedQuantity(string sysDocID, string voucherID, int rowIndex, float quantity, SqlTransaction sqlTransaction)
		{
			DataSet dataSet = new DataSet();
			float result = 0f;
			float result2 = 0f;
			try
			{
				string textCommand = "SELECT Quantity,UnitQuantity,QuantitySettled,QuantityReturned FROM Consign_Out_Detail WHERE SysDocID='" + sysDocID + "' AND VoucherID='" + voucherID + "' AND RowIndex=" + rowIndex.ToString();
				FillDataSet(dataSet, "Consign", textCommand);
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
					float.TryParse(dataRow["QuantityReturned"].ToString(), out result2);
				}
				result2 += quantity;
				textCommand = "UPDATE Consign_Out_Detail SET QuantityReturned=" + result2.ToString() + " WHERE SysDocID='" + sysDocID + "' AND VoucherID='" + voucherID + "' AND RowIndex=" + rowIndex.ToString();
				return ExecuteNonQuery(textCommand, sqlTransaction) > 0;
			}
			catch
			{
				return false;
			}
		}

		public bool VoidConsignOutReturn(string sysDocID, string voucherID, bool isVoid)
		{
			bool flag = true;
			try
			{
				SqlTransaction sqlTransaction = base.DBConfig.StartNewTransaction();
				string exp = "UPDATE ConsignOut_Return SET IsVoid = '" + isVoid.ToString() + "' WHERE SysDocID='" + sysDocID + "' AND VoucherID = '" + voucherID + "'";
				flag &= (ExecuteNonQuery(exp, sqlTransaction) > 0);
				ConsignOutReturnData consignOutReturnData = new ConsignOutReturnData();
				exp = "SELECT CO.ConsignSysDocID,CO.ConsignVoucherID, COD.*  FROM ConsignOut_Return_Detail COD INNER JOIN ConsignOut_Return CO ON COD.SysDocID=CO.SysDocID AND COD.VoucherID=CO.VoucherID\r\n                              WHERE COD.SysDocID = '" + sysDocID + "' AND COD.VoucherID = '" + voucherID + "'";
				FillDataSet(consignOutReturnData, "ConsignOut_Return_Detail", exp, sqlTransaction);
				string sysDocID2 = consignOutReturnData.Tables["ConsignOut_Return_Detail"].Rows[0]["ConsignSysDocID"].ToString();
				string voucherID2 = consignOutReturnData.Tables["ConsignOut_Return_Detail"].Rows[0]["ConsignVoucherID"].ToString();
				foreach (DataRow row in consignOutReturnData.ConsignOutReturnDetailTable.Rows)
				{
					row["ProductID"].ToString();
					int result = 0;
					int.TryParse(row["ConsignRowIndex"].ToString(), out result);
					float result2 = 0f;
					if (row["UnitQuantity"] != DBNull.Value)
					{
						float.TryParse(row["UnitQuantity"].ToString(), out result2);
					}
					else
					{
						float.TryParse(row["Quantity"].ToString(), out result2);
					}
					flag &= UpdateRowReturnedQuantity(sysDocID2, voucherID2, result, -1f * result2, sqlTransaction);
				}
				flag &= new InventoryTransaction(base.DBConfig).DeleteInventoryTransaction(54, sysDocID, voucherID, isDeletingTransaction: true, sqlTransaction);
				flag &= new ConsignOut(base.DBConfig).CloseOpenConsignment(sysDocID2, voucherID2, sqlTransaction);
				if (!flag)
				{
					return flag;
				}
				ActivityTypes activityType = ActivityTypes.Void;
				if (!isVoid)
				{
					activityType = ActivityTypes.Unvoid;
				}
				AddActivityLog("Consign Out Return", voucherID, sysDocID, activityType, sqlTransaction);
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

		public bool DeleteConsignOutReturn(string sysDocID, string voucherID)
		{
			bool flag = true;
			try
			{
				string text = "";
				SqlTransaction sqlTransaction = base.DBConfig.StartNewTransaction();
				flag &= DeleteConsignOutReturnDetailsRows(sysDocID, voucherID, isDeletingTransaction: true, sqlTransaction);
				text = "DELETE FROM Consign_Out WHERE SysDocID='" + sysDocID + "' AND VoucherID = '" + voucherID + "'";
				flag &= (ExecuteNonQuery(text, sqlTransaction) >= 0);
				if (!flag)
				{
					return flag;
				}
				AddActivityLog("Consign Out Return", voucherID, sysDocID, ActivityTypes.Delete, sqlTransaction);
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

		public DataSet GetUninvoicedConsignOuts(string customerID)
		{
			try
			{
				DataSet dataSet = new DataSet();
				string text = "SELECT DN.SysDocID [Doc ID],DN.VoucherID [Number],TransactionDate AS [Date],DN.CustomerID + '-' + C.CustomerName AS [Customer] FROM Consign_Out DN\r\n                             INNER JOIN Customer C ON DN.CustomerID=C.CustomerID  WHERE ISNULL(IsVoid,'False')='False' AND ISNULL(IsInvoiced,'False')='False'";
				if (customerID != "")
				{
					text = text + " AND DN.CustomerID='" + customerID + "'";
				}
				FillDataSet(dataSet, "ConsignOut_Return", text);
				return dataSet;
			}
			catch
			{
				throw;
			}
		}

		public DataSet GetConsignOutReturnToPrint(string sysDocID, string voucherID)
		{
			return GetConsignOutReturnToPrint(sysDocID, new string[1]
			{
				voucherID
			});
		}

		public DataSet GetConsignOutReturnToPrint(string sysDocID, string[] voucherID)
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
				string cmdText = "SELECT  DISTINCT   SysDocID,VoucherID,SI.CustomerID,CustomerName,CustomerAddress,TransactionDate,\r\n                               CA.AddressPrintFormat AS ShippingAddress,ShippingMethodName,\r\n                                PONumber,SI.Note\r\n                                FROM  ConsignOut_Return SI INNER JOIN Customer ON SI.CustomerID=Customer.CustomerID\r\n                                LEFT OUTER JOIN Customer_Address CA ON CA.AddressID=ShippingAddressID AND CA.CustomerID=SI.CustomerID\r\n                                LEFT OUTER JOIN Shipping_Method SM ON SM.ShippingMethodID=SI.ShippingMethodID\r\n                                WHERE SysDocID = '" + sysDocID + "' AND VoucherID IN (" + text + ")";
				SqlCommand sqlCommand = new SqlCommand(cmdText);
				FillDataSet(dataSet, "ConsignOut_Return", sqlCommand);
				if (dataSet == null || dataSet.Tables.Count == 0 || dataSet.Tables["ConsignOut_Return"].Rows.Count == 0)
				{
					return null;
				}
				cmdText = "SELECT SD.*,P.Description,P.ItemType,P.IsTrackLot,P.IsTrackSerial,COD.Quantity AS DeliveredQty,COD.RowIndex AS ConsignRowIndex,QuantitySettled,QuantityReturned, COD.Quantity - ISNULL(QuantitySettled,0)-ISNULL(QuantityReturned,0) + SD.Quantity AS BalanceQty\r\n                        FROM ConsignOut_Return_Detail SD INNER JOIN Product P ON SD.ProductID=P.ProductID\r\n                        INNER JOIN Consign_Out_Detail COD ON COD.SysDocID=SD.ConsignSysDocID\r\n                        AND COD.VoucherID=SD.ConsignVoucherID AND COD.RowIndex=SD.ConsignRowIndex\r\n                        WHERE SD.SysDocID='" + sysDocID + "' AND SD.VoucherID IN (" + text + ")";
				FillDataSet(dataSet, "ConsignOut_Return_Detail", cmdText);
				dataSet.Relations.Add("CustomerConsignOut", new DataColumn[2]
				{
					dataSet.Tables["ConsignOut_Return"].Columns["SysDocID"],
					dataSet.Tables["ConsignOut_Return"].Columns["VoucherID"]
				}, new DataColumn[2]
				{
					dataSet.Tables["ConsignOut_Return_Detail"].Columns["SysDocID"],
					dataSet.Tables["ConsignOut_Return_Detail"].Columns["VoucherID"]
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
			string text3 = "SELECT    ISNULL(IsVoid,'False') AS V, INV.SysDocID [Doc ID],INV.VoucherID [Doc Number],INV.CustomerID [Customer Code],CustomerName [Customer Name],TransactionDate [ Date]\r\n                           \r\n                            FROM         ConsignOut_Return INV\r\n                            \r\n                            Inner JOIN Customer ON CUSTOMER.CustomerID=INV.CustomerID ";
			if (from != DateTime.MinValue)
			{
				text3 = text3 + " WHERE TransactionDate Between '" + text + "' AND '" + text2 + "'";
			}
			if (!showVoid)
			{
				text3 += " AND ISNULL(IsVoid,'False')='False'";
			}
			text3 += " GROUP BY IsVoid,INV.SysDocID,INV.VoucherID,INV.CustomerID ,CustomerName,TransactionDate";
			SqlCommand sqlCommand = new SqlCommand(text3);
			FillDataSet(dataSet, "ConsignOut_Return", sqlCommand);
			return dataSet;
		}

		public DataSet GetOpenConsignments(string customerID)
		{
			try
			{
				DataSet dataSet = new DataSet();
				string str = "SELECT CO.SysDocID [Doc ID], CO.VoucherID [Number],CO.CustomerID [Customer Code], CUS.CustomerName [Customer Name],TransactionDate [Date]\r\n                            FROM Consign_Out CO INNER JOIN Consign_Out_Detail COD\r\n                            ON CO.SysDocID=COD.SysDocID AND CO.VoucherID=COD.VoucherID\r\n                            INNER JOIN Customer CUS ON CO.CustomerID=CUS.CustomerID ";
				if (customerID != "")
				{
					str = str + " WHERE CO.CustomerID='" + customerID + "'";
				}
				str += " GROUP BY CO.SysDocID, CO.VoucherID,CO.CustomerID,TransactionDate, CUS.CustomerName\r\n                                HAVING\r\n                                SUM(ISNULL(COD.Quantity,0)-ISNULL(COD.QuantitySettled,0))>0 ";
				SqlCommand sqlCommand = new SqlCommand(str);
				FillDataSet(dataSet, "ConsignOut_Return", sqlCommand);
				return dataSet;
			}
			catch
			{
				throw;
			}
		}

		private GLData CreateGLData(ConsignOutReturnData transactionData, SqlTransaction sqlTransaction)
		{
			try
			{
				GLData gLData = new GLData();
				DataRow dataRow = transactionData.ConsignOutReturnTable.Rows[0];
				int currencyDecimalPoints = new CompanyInformations(base.DBConfig).CurrencyDecimalPoints;
				string value = dataRow["CustomerID"].ToString();
				string text = dataRow["SysDocID"].ToString();
				string text2 = dataRow["VoucherID"].ToString();
				string value2 = dataRow["CompanyID"].ToString();
				string value3 = dataRow["DivisionID"].ToString();
				string textCommand = "SELECT SD.LocationID,  LOC.InventoryAccountID ConsignAssetAccountID,SD.ConsignOutLocationID\r\n                                FROM System_Document SD INNER JOIN Location LOC ON SD.ConsignOutLocationID = LOC.LocationID\r\n\t\t\t\t\t\t\t\tINNER JOIN Location LOC2 ON SD.LocationID = LOC2.LocationID\r\n                                WHERE SysDocID = '" + text + "'";
				DataSet dataSet = new DataSet();
				FillDataSet(dataSet, "Accounts", textCommand, sqlTransaction);
				if (dataSet == null || dataSet.Tables.Count == 0 || dataSet.Tables[0].Rows.Count == 0)
				{
					throw new CompanyException("There is no location assigned to this system document or location record is missing.");
				}
				DataRow dataRow2 = dataSet.Tables["Accounts"].Rows[0];
				string docLocationID = dataRow2["LocationID"].ToString();
				string transitLocationID = dataRow2["ConsignOutLocationID"].ToString();
				string value4 = dataRow2["ConsignAssetAccountID"].ToString();
				DataRow dataRow3 = gLData.JournalTable.NewRow();
				SysDocTypes sysDocTypes = SysDocTypes.ConsignOut;
				dataRow3["JournalID"] = 0;
				dataRow3["JournalDate"] = dataRow["TransactionDate"];
				dataRow3["SysDocID"] = dataRow["SysDocID"];
				dataRow3["SysDocType"] = (byte)sysDocTypes;
				dataRow3["VoucherID"] = dataRow["VoucherID"];
				dataRow3["Reference"] = dataRow["Reference"];
				dataRow3["Narration"] = "Consign Out Return - " + text2;
				dataRow3.EndEdit();
				gLData.JournalTable.Rows.Add(dataRow3);
				if (1 == 0)
				{
					throw new CompanyException("Consignment out return does not support direct invoicing.");
				}
				decimal value5 = default(decimal);
				textCommand = "SELECT SUM(AssetValue) FROM Inventory_Transactions IT WHERE SysDocID = '" + text + "' AND VoucherID = '" + text2 + "' AND Quantity<0";
				object obj = ExecuteScalar(textCommand, sqlTransaction);
				if (!obj.IsNullOrEmpty())
				{
					value5 += decimal.Parse(obj.ToString());
				}
				DataRow dataRow4 = gLData.JournalDetailsTable.NewRow();
				dataRow4.BeginEdit();
				dataRow4["JournalID"] = 0;
				dataRow4["AccountID"] = value4;
				dataRow4["PayeeID"] = value;
				dataRow4["Debit"] = DBNull.Value;
				dataRow4["Credit"] = Math.Abs(value5);
				dataRow4["IsBaseOnly"] = true;
				dataRow4["Reference"] = dataRow["Reference"];
				dataRow4["JVEntryType"] = (byte)1;
				dataRow4["CompanyID"] = value2;
				dataRow4["DivisionID"] = value3;
				dataRow4.EndEdit();
				gLData.JournalDetailsTable.Rows.Add(dataRow4);
				decimal num = default(decimal);
				Hashtable hashtable = new Hashtable();
				ArrayList arrayList = new ArrayList();
				decimal d = default(decimal);
				foreach (DataRow row in transactionData.ConsignOutReturnDetailTable.Rows)
				{
					string text3 = row["ProductID"].ToString();
					string warehouseLocationID = row["LocationID"].ToString();
					int rowIndex = int.Parse(row["RowIndex"].ToString());
					dataSet = new Products(base.DBConfig).GetProductTransactionAccounts(text3, docLocationID, warehouseLocationID, text, sqlTransaction);
					if (dataSet == null || dataSet.Tables.Count == 0 || dataSet.Tables[0].Rows.Count == 0)
					{
						throw new CompanyException("Product accounts information not found for product or location.");
					}
					DataRow dataRow5 = dataSet.Tables[0].Rows[0];
					string text4 = dataRow5["InventoryAssetAccountID"].ToString();
					ItemTypes itemTypes = ItemTypes.Inventory;
					obj = dataRow5["ItemType"].ToString();
					if (obj == null || !(obj.ToString() != ""))
					{
						throw new CompanyException("Item type is not selected for the product:" + text3);
					}
					itemTypes = (ItemTypes)byte.Parse(obj.ToString());
					decimal num2 = default(decimal);
					num2 = ((itemTypes == ItemTypes.ConsignmentItem) ? default(decimal) : Math.Abs(new InventoryTransaction(base.DBConfig).GetRowAssetValue(text3, text, text2, rowIndex, transitLocationID, mergeWithRefRows: false, sqlTransaction)));
					if (itemTypes == ItemTypes.Inventory || itemTypes == ItemTypes.Assembly)
					{
						string text5 = text4;
						if (hashtable.ContainsKey(text5))
						{
							num = decimal.Parse(hashtable[text5].ToString());
							num += Math.Round(num2, currencyDecimalPoints);
							hashtable[text5] = num;
						}
						else
						{
							hashtable.Add(text5, Math.Round(num2, currencyDecimalPoints));
							arrayList.Add(text5);
						}
						d += Math.Round(num2, currencyDecimalPoints);
					}
				}
				if (d != 0m)
				{
					for (int i = 0; i < hashtable.Count; i++)
					{
						dataRow4 = gLData.JournalDetailsTable.NewRow();
						dataRow4.BeginEdit();
						string text5 = arrayList[i].ToString();
						num = decimal.Parse(hashtable[text5].ToString());
						dataRow4["JournalID"] = 0;
						dataRow4["AccountID"] = text5;
						dataRow4["PayeeID"] = value;
						dataRow4["Debit"] = num;
						dataRow4["Credit"] = DBNull.Value;
						dataRow4["IsBaseOnly"] = true;
						dataRow4["JVEntryType"] = (byte)1;
						dataRow4["Reference"] = dataRow["Reference"];
						dataRow4["CompanyID"] = value2;
						dataRow4["DivisionID"] = value3;
						dataRow4.EndEdit();
						gLData.JournalDetailsTable.Rows.Add(dataRow4);
					}
				}
				return gLData;
			}
			catch
			{
				throw;
			}
		}

		public bool ConsignmentHasSettlement(string sysDocID, string voucherNumber, SqlTransaction sqlTransaction)
		{
			string exp = "Select Count(*) FROM Consign_Out_Detail POD\r\n                                WHERE SysdocID='" + sysDocID + "' AND VoucherID='" + voucherNumber + "' Having SUM(ISNULL(QuantitySettled,0) + ISNULL(QuantityReturned,0)) >0";
			object obj = ExecuteScalar(exp, sqlTransaction);
			if (obj != null && obj.ToString() != "" && int.Parse(obj.ToString()) > 0)
			{
				return true;
			}
			return false;
		}
	}
}
