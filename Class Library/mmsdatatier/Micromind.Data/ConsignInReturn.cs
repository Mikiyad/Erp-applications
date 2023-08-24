using Micromind.Common;
using Micromind.Common.Data;
using Micromind.Common.Libraries;
using System;
using System.Data;
using System.Data.SqlClient;

namespace Micromind.Data
{
	public sealed class ConsignInReturn : StoreObject
	{
		private const string CONSIGNINRETURN_TABLE = "ConsignIn_Return";

		private const string CONSIGNINRETURNDETAIL_TABLE = "ConsignIn_Return_Detail";

		private const string SYSDOCID_PARM = "@SysDocID";

		private const string VOUCHERID_PARM = "@VoucherID";

		private const string COMPANYID_PARM = "@CompanyID";

		private const string DIVISIONID_PARM = "@DivisionID";

		private const string VENDORID_PARM = "@VendorID";

		private const string TRANSACTIONDATE_PARM = "@TransactionDate";

		private const string ROWINDEX_PARM = "@RowIndex";

		private const string CONSIGNSYSDOCID_PARM = "@ConsignSysDocID";

		private const string CONSIGNVOUCHERID_PARM = "@ConsignVoucherID";

		private const string CONSIGNROWINDEX_PARM = "@ConsignRowIndex";

		private const string SHIPPINGADDRESSID_PARM = "@ShippingAddressID";

		private const string VENDORADDRESS_PARM = "@VendorAddress";

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

		public ConsignInReturn(Config config)
			: base(config)
		{
		}

		private string GetInsertUpdateConsignInReturnText(bool isUpdate)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddInsertUpdateParameters("ConsignIn_Return", new FieldValue("SysDocID", "@SysDocID", isUpdateConditionField: true), new FieldValue("VoucherID", "@VoucherID", isUpdateConditionField: true), new FieldValue("DivisionID", "@DivisionID"), new FieldValue("CompanyID", "@CompanyID"), new FieldValue("VendorID", "@VendorID"), new FieldValue("TransactionDate", "@TransactionDate"), new FieldValue("ShippingAddressID", "@ShippingAddressID"), new FieldValue("ShippingMethodID", "@ShippingMethodID"), new FieldValue("VendorAddress", "@VendorAddress"), new FieldValue("Status", "@Status"), new FieldValue("ConsignSysDocID", "@ConsignSysDocID"), new FieldValue("ConsignVoucherID", "@ConsignVoucherID"), new FieldValue("PONumber", "@PONumber"), new FieldValue("Reference", "@Reference"), new FieldValue("Note", "@Note"));
			if (isUpdate)
			{
				sqlBuilder.AddInsertUpdateParameters("ConsignIn_Return", new FieldValue("DateUpdated", "@DateUpdated", isUpdateConditionField: true, checkForNullValue: true));
			}
			if (isUpdate)
			{
				return sqlBuilder.GetUpdateExpression();
			}
			return sqlBuilder.GetInsertExpression();
		}

		private SqlCommand GetInsertUpdateConsignInReturnCommand(bool isUpdate)
		{
			SqlParameterCollection parameters;
			if (isUpdate)
			{
				if (updateCommand != null)
				{
					updateCommand = null;
				}
				updateCommand = new SqlCommand(GetInsertUpdateConsignInReturnText(isUpdate: true), base.DBConfig.Connection);
				updateCommand.CommandType = CommandType.Text;
				parameters = updateCommand.Parameters;
			}
			else
			{
				if (insertCommand != null)
				{
					insertCommand = null;
				}
				insertCommand = new SqlCommand(GetInsertUpdateConsignInReturnText(isUpdate: false), base.DBConfig.Connection);
				insertCommand.CommandType = CommandType.Text;
				parameters = insertCommand.Parameters;
			}
			parameters.Add("@SysDocID", SqlDbType.NVarChar);
			parameters.Add("@VoucherID", SqlDbType.NVarChar);
			parameters.Add("@DivisionID", SqlDbType.NVarChar);
			parameters.Add("@CompanyID", SqlDbType.TinyInt);
			parameters.Add("@VendorID", SqlDbType.NVarChar);
			parameters.Add("@TransactionDate", SqlDbType.DateTime);
			parameters.Add("@ShippingAddressID", SqlDbType.NVarChar);
			parameters.Add("@VendorAddress", SqlDbType.NVarChar);
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
			parameters["@VendorID"].SourceColumn = "VendorID";
			parameters["@TransactionDate"].SourceColumn = "TransactionDate";
			parameters["@ConsignVoucherID"].SourceColumn = "ConsignVoucherID";
			parameters["@ConsignSysDocID"].SourceColumn = "ConsignSysDocID";
			parameters["@ShippingAddressID"].SourceColumn = "ShippingAddressID";
			parameters["@VendorAddress"].SourceColumn = "VendorAddress";
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

		private string GetInsertUpdateConsignInReturnDetailsText(bool isUpdate)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddInsertUpdateParameters("ConsignIn_Return_Detail", new FieldValue("SysDocID", "@SysDocID"), new FieldValue("VoucherID", "@VoucherID"), new FieldValue("ProductID", "@ProductID"), new FieldValue("LocationID", "@LocationID"), new FieldValue("ConsignLocationID", "@ConsignLocationID"), new FieldValue("RowIndex", "@RowIndex"), new FieldValue("Quantity", "@Quantity"), new FieldValue("Description", "@Description"), new FieldValue("UnitID", "@UnitID"), new FieldValue("UnitQuantity", "@UnitQuantity"), new FieldValue("UnitFactor", "@UnitFactor"), new FieldValue("FactorType", "@FactorType"), new FieldValue("ConsignVoucherID", "@ConsignVoucherID"), new FieldValue("ConsignSysDocID", "@ConsignSysDocID"), new FieldValue("ConsignRowIndex", "@ConsignRowIndex"));
			if (isUpdate)
			{
				return sqlBuilder.GetUpdateExpression();
			}
			return sqlBuilder.GetInsertExpression();
		}

		private SqlCommand GetInsertUpdateConsignInReturnDetailsCommand(bool isUpdate)
		{
			SqlParameterCollection parameters;
			if (isUpdate)
			{
				if (updateCommand != null)
				{
					updateCommand = null;
				}
				updateCommand = new SqlCommand(GetInsertUpdateConsignInReturnDetailsText(isUpdate: true), base.DBConfig.Connection);
				updateCommand.CommandType = CommandType.Text;
				parameters = updateCommand.Parameters;
			}
			else
			{
				if (insertCommand != null)
				{
					insertCommand = null;
				}
				insertCommand = new SqlCommand(GetInsertUpdateConsignInReturnDetailsText(isUpdate: false), base.DBConfig.Connection);
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

		private bool ValidateData(ConsignInReturnData journalData)
		{
			return true;
		}

		public bool InsertUpdateConsignInReturn(ConsignInReturnData consignInData, bool isUpdate)
		{
			bool flag = true;
			SqlCommand insertUpdateConsignInReturnCommand = GetInsertUpdateConsignInReturnCommand(isUpdate);
			try
			{
				DataRow dataRow = consignInData.ConsignInReturnTable.Rows[0];
				SqlTransaction sqlTransaction = base.DBConfig.StartNewTransaction();
				string text = dataRow["VoucherID"].ToString();
				string sysDocID = dataRow["SysDocID"].ToString();
				string text2 = dataRow["ConsignSysDocID"].ToString();
				string text3 = dataRow["ConsignVoucherID"].ToString();
				bool flag2 = false;
				object fieldValue = new Databases(base.DBConfig).GetFieldValue("Consign_In", "Status", "SysDocID", text2, "VoucherID", text3, sqlTransaction);
				if (fieldValue != null && fieldValue.ToString() != "")
				{
					flag2 = (byte.Parse(fieldValue.ToString()) == 3);
				}
				if (flag2)
				{
					throw new CompanyException("This consignment is already closed and cannot be modified.", 1061);
				}
				if (!isUpdate && new SystemDocuments(base.DBConfig).ExistDocumentNumber("ConsignIn_Return", "VoucherID", dataRow["SysDocID"].ToString(), text, sqlTransaction))
				{
					base.DBConfig.EndTransaction(result: false);
					throw new CompanyException("Document number already exist.", 1046);
				}
				if (isUpdate)
				{
					flag &= DeleteConsignInReturnDetailsRows(sysDocID, text, isDeletingTransaction: false, sqlTransaction);
				}
				float num = 0f;
				foreach (DataRow row in consignInData.ConsignInReturnDetailTable.Rows)
				{
					row["SysDocID"] = dataRow["SysDocID"];
					row["VoucherID"] = dataRow["VoucherID"];
					string text4 = row["ProductID"].ToString();
					string idFieldValue = row["ConsignSysDocID"].ToString();
					string checkFieldValue = row["ConsignVoucherID"].ToString();
					int num2 = int.Parse(row["ConsignRowIndex"].ToString());
					object fieldValue2 = new Databases(base.DBConfig).GetFieldValue("Consign_In_Detail", "QuantityReturned", "SysDocID", idFieldValue, "VoucherID", checkFieldValue, "RowIndex", num2, sqlTransaction);
					float num3 = 0f;
					if (fieldValue2 != null && fieldValue2.ToString() != "")
					{
						num3 = float.Parse(fieldValue2.ToString());
					}
					fieldValue2 = new Databases(base.DBConfig).GetFieldValue("Consign_In_Detail", "QuantitySettled", "SysDocID", idFieldValue, "VoucherID", checkFieldValue, "RowIndex", num2, sqlTransaction);
					float num4 = 0f;
					if (fieldValue2 != null && fieldValue2.ToString() != "")
					{
						num4 = float.Parse(fieldValue2.ToString());
					}
					fieldValue2 = new Databases(base.DBConfig).GetFieldValue("Consign_In_Detail", "Quantity", "SysDocID", idFieldValue, "VoucherID", checkFieldValue, "RowIndex", num2, sqlTransaction);
					float num5 = 0f;
					if (fieldValue2 != null && fieldValue2.ToString() != "")
					{
						num5 = float.Parse(fieldValue2.ToString());
					}
					if (num3 + num4 > num5)
					{
						throw new CompanyException("Returned quantity cannot be greater than balance quantity.", 1026);
					}
					num = float.Parse(row["Quantity"].ToString());
					num3 += num;
					UpdateRowReturnedQuantity(text2, text3, num2, num, sqlTransaction);
					string text5 = "";
					fieldValue = new Databases(base.DBConfig).GetFieldValue("Product", "UnitID", "ProductID", text4, sqlTransaction);
					if (fieldValue != null)
					{
						text5 = fieldValue.ToString();
					}
					if (text5 != "" && row["UnitID"] != DBNull.Value && row["UnitID"].ToString() != text5)
					{
						DataRow obj = new Products(base.DBConfig).GetProductUnitRow(text4, row["UnitID"].ToString()) ?? throw new CompanyException("One of the selected units is not assigned to item.\nItem:" + text4 + "\nUnit:" + row["UnitID"].ToString());
						float num6 = float.Parse(obj["Factor"].ToString());
						string text6 = obj["FactorType"].ToString();
						num = float.Parse(row["Quantity"].ToString());
						row["UnitFactor"] = num6;
						row["FactorType"] = text6;
						row["UnitQuantity"] = row["Quantity"];
						num = ((!(text6 == "M")) ? float.Parse(Math.Round(num * num6, 5).ToString()) : float.Parse(Math.Round(num / num6, 5).ToString()));
						row["Quantity"] = num;
					}
				}
				insertUpdateConsignInReturnCommand.Transaction = sqlTransaction;
				flag = (isUpdate ? (flag & Update(consignInData, "ConsignIn_Return", insertUpdateConsignInReturnCommand)) : (flag & Insert(consignInData, "ConsignIn_Return", insertUpdateConsignInReturnCommand)));
				insertUpdateConsignInReturnCommand = GetInsertUpdateConsignInReturnDetailsCommand(isUpdate: false);
				insertUpdateConsignInReturnCommand.Transaction = sqlTransaction;
				if (consignInData.Tables["ConsignIn_Return_Detail"].Rows.Count > 0)
				{
					flag &= Insert(consignInData, "ConsignIn_Return_Detail", insertUpdateConsignInReturnCommand);
				}
				InventoryTransactionData inventoryTransactionData = new InventoryTransactionData();
				foreach (DataRow row2 in consignInData.ConsignInReturnDetailTable.Rows)
				{
					DataRow dataRow4 = inventoryTransactionData.InventoryTransactionTable.NewRow();
					dataRow4.BeginEdit();
					dataRow4["SysDocID"] = row2["SysDocID"];
					dataRow4["VoucherID"] = row2["VoucherID"];
					dataRow4["LocationID"] = row2["LocationID"];
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
					dataRow4["SysDocType"] = (byte)57;
					dataRow4["TransactionDate"] = dataRow["TransactionDate"];
					dataRow4["TransactionType"] = (byte)14;
					dataRow4["DivisionID"] = dataRow["DivisionID"];
					dataRow4["CompanyID"] = dataRow["CompanyID"];
					dataRow4.EndEdit();
					inventoryTransactionData.InventoryTransactionTable.Rows.Add(dataRow4);
				}
				inventoryTransactionData.Merge(consignInData.Tables["Product_Lot_Issue_Detail"]);
				flag &= new Products(base.DBConfig).InsertUpdateProductLotIssueDetail(consignInData, isUpdate: false, sqlTransaction);
				flag &= new InventoryTransaction(base.DBConfig).InsertUpdateInventoryTransaction(inventoryTransactionData, isUpdate, sqlTransaction);
				flag &= new ConsignIn(base.DBConfig).CloseOpenConsignment(text2, text3, sqlTransaction);
				flag &= new ConsignIn(base.DBConfig).SetStatus(text2, text3, ConsignInStatusEnum.Open, sqlTransaction);
				if (!flag)
				{
					throw new Exception("Failed to save the transaction because of an unexpected error. Please try again.");
				}
				flag &= UpdateTableRowInsertUpdateInfo("ConsignIn_Return", "SysDocID", dataRow["SysDocID"].ToString(), "VoucherID", dataRow["VoucherID"].ToString(), sqlTransaction, !isUpdate);
				string entityName = "Consignment In";
				flag = (isUpdate ? (flag & AddActivityLog(entityName, text, sysDocID, ActivityTypes.Update, sqlTransaction)) : (flag & AddActivityLog(entityName, text, sysDocID, ActivityTypes.Add, sqlTransaction)));
				if (isUpdate)
				{
					return flag;
				}
				flag &= new SystemDocuments(base.DBConfig).UpdateNextDocumentNumber(dataRow["SysDocID"].ToString(), dataRow["VoucherID"].ToString(), "ConsignIn_Return", "VoucherID", sqlTransaction);
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

		public ConsignInReturnData GetConsignInReturnByID(string sysDocID, string voucherID)
		{
			try
			{
				ConsignInReturnData consignInReturnData = new ConsignInReturnData();
				string textCommand = "SELECT * FROM ConsignIn_Return WHERE VoucherID='" + voucherID + "' AND SysDocID='" + sysDocID + "'";
				FillDataSet(consignInReturnData, "ConsignIn_Return", textCommand);
				if (consignInReturnData == null || consignInReturnData.Tables.Count == 0 || consignInReturnData.Tables["ConsignIn_Return"].Rows.Count == 0)
				{
					return null;
				}
				textCommand = "SELECT SD.*,P.Description,P.ItemType,COD.Quantity AS DeliveredQty,COD.RowIndex AS ConsignRowIndex,QuantitySettled,QuantityReturned, COD.Quantity - ISNULL(QuantitySettled,0)-ISNULL(QuantityReturned,0) + SD.Quantity AS BalanceQty\r\n                        FROM ConsignIn_Return_Detail SD INNER JOIN Product P ON SD.ProductID=P.ProductID\r\n                        INNER JOIN Consign_In_Detail COD ON COD.SysDocID=SD.ConsignSysDocID\r\n                        AND COD.VoucherID=SD.ConsignVoucherID AND COD.RowIndex=SD.ConsignRowIndex\r\n                        WHERE SD.VoucherID='" + voucherID + "' AND SD.SysDocID='" + sysDocID + "'";
				FillDataSet(consignInReturnData, "ConsignIn_Return_Detail", textCommand);
				DataSet transactionIssuesProductLots = new Products(base.DBConfig).GetTransactionIssuesProductLots(sysDocID, voucherID);
				if (consignInReturnData.Tables.Contains("Product_Lot_Issue_Detail"))
				{
					consignInReturnData.Tables.Remove("Product_Lot_Issue_Detail");
				}
				consignInReturnData.Merge(transactionIssuesProductLots, preserveChanges: false);
				return consignInReturnData;
			}
			catch
			{
				throw;
			}
		}

		internal bool DeleteConsignInReturnDetailsRows(string sysDocID, string voucherID, bool isDeletingTransaction, SqlTransaction sqlTransaction)
		{
			bool flag = true;
			try
			{
				ConsignInReturnData consignInReturnData = new ConsignInReturnData();
				string textCommand = "SELECT SO.ConsignSysDocID,SO.ConsignVoucherID, SOD.*,ISNULL(ISVOID,'False') AS IsVoid\r\n                                    FROM ConsignIn_Return_Detail SOD INNER JOIN ConsignIn_Return SO ON SO.SysDocID=SOD.SysDocID AND SO.VOucherID=SOD.VoucherID\r\n                                    WHERE SOD.SysDocID = '" + sysDocID + "' AND SOD.VoucherID = '" + voucherID + "'";
				FillDataSet(consignInReturnData, "ConsignIn_Return_Detail", textCommand, sqlTransaction);
				if (consignInReturnData.ConsignInReturnDetailTable.Rows.Count == 0)
				{
					return true;
				}
				bool result = false;
				bool.TryParse(consignInReturnData.ConsignInReturnDetailTable.Rows[0]["IsVoid"].ToString(), out result);
				string sysDocID2 = consignInReturnData.ConsignInReturnDetailTable.Rows[0]["ConsignSysDocID"].ToString();
				string voucherID2 = consignInReturnData.ConsignInReturnDetailTable.Rows[0]["ConsignVoucherID"].ToString();
				if (!result)
				{
					flag &= new InventoryTransaction(base.DBConfig).DeleteInventoryTransaction(57, sysDocID, voucherID, isDeletingTransaction, sqlTransaction);
					foreach (DataRow row in consignInReturnData.ConsignInReturnDetailTable.Rows)
					{
						row["ProductID"].ToString();
						sysDocID2 = row["ConsignSysDocID"].ToString();
						voucherID2 = row["ConsignVoucherID"].ToString();
						string s = row["ConsignRowIndex"].ToString();
						float num = float.Parse(row["Quantity"].ToString());
						flag &= UpdateRowReturnedQuantity(sysDocID2, voucherID2, int.Parse(s), -1f * num, sqlTransaction);
					}
				}
				flag &= new ConsignIn(base.DBConfig).CloseOpenConsignment(sysDocID2, voucherID2, sqlTransaction);
				flag &= new ConsignIn(base.DBConfig).SetStatus(sysDocID2, voucherID2, ConsignInStatusEnum.Open, sqlTransaction);
				textCommand = "DELETE FROM Product_Lot_Issue_Detail WHERE SysDocID = '" + sysDocID + "' AND VoucherID = '" + voucherID + "'";
				flag &= Delete(textCommand, sqlTransaction);
				textCommand = "DELETE FROM ConsignIn_Return_Detail WHERE SysDocID = '" + sysDocID + "' AND VoucherID = '" + voucherID + "'";
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
				string textCommand = "SELECT Quantity,UnitQuantity,QuantitySettled,QuantityReturned FROM Consign_In_Detail WHERE SysDocID='" + sysDocID + "' AND VoucherID='" + voucherID + "' AND RowIndex=" + rowIndex.ToString();
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
				textCommand = "UPDATE Consign_In_Detail SET QuantityReturned=" + result2.ToString() + " WHERE SysDocID='" + sysDocID + "' AND VoucherID='" + voucherID + "' AND RowIndex=" + rowIndex.ToString();
				return ExecuteNonQuery(textCommand, sqlTransaction) > 0;
			}
			catch
			{
				return false;
			}
		}

		public bool VoidConsignInReturn(string sysDocID, string voucherID, bool isVoid)
		{
			bool flag = true;
			try
			{
				SqlTransaction sqlTransaction = base.DBConfig.StartNewTransaction();
				string exp = "UPDATE ConsignIn_Return SET IsVoid = '" + isVoid.ToString() + "' WHERE SysDocID='" + sysDocID + "' AND VoucherID = '" + voucherID + "'";
				flag &= (ExecuteNonQuery(exp, sqlTransaction) > 0);
				ConsignInReturnData consignInReturnData = new ConsignInReturnData();
				exp = "SELECT CO.ConsignSysDocID,CO.ConsignVoucherID, COD.*  FROM ConsignIn_Return_Detail COD INNER JOIN ConsignIn_Return CO ON COD.SysDocID=CO.SysDocID AND COD.VoucherID=CO.VoucherID\r\n                              WHERE COD.SysDocID = '" + sysDocID + "' AND COD.VoucherID = '" + voucherID + "'";
				FillDataSet(consignInReturnData, "ConsignIn_Return_Detail", exp, sqlTransaction);
				string text = consignInReturnData.Tables["ConsignIn_Return_Detail"].Rows[0]["ConsignSysDocID"].ToString();
				string text2 = consignInReturnData.Tables["ConsignIn_Return_Detail"].Rows[0]["ConsignVoucherID"].ToString();
				bool flag2 = false;
				object fieldValue = new Databases(base.DBConfig).GetFieldValue("Consign_In", "Status", "SysDocID", text, "VoucherID", text2, sqlTransaction);
				if (fieldValue != null && fieldValue.ToString() != "")
				{
					flag2 = (byte.Parse(fieldValue.ToString()) == 3);
				}
				if (flag2)
				{
					throw new CompanyException("This consignment is already closed and cannot be modified.", 1061);
				}
				foreach (DataRow row in consignInReturnData.ConsignInReturnDetailTable.Rows)
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
					flag &= UpdateRowReturnedQuantity(text, text2, result, -1f * result2, sqlTransaction);
				}
				flag &= new InventoryTransaction(base.DBConfig).DeleteInventoryTransaction(57, sysDocID, voucherID, isDeletingTransaction: true, sqlTransaction);
				flag &= new ConsignIn(base.DBConfig).CloseOpenConsignment(text, text2, sqlTransaction);
				flag &= new ConsignIn(base.DBConfig).SetStatus(text, text2, ConsignInStatusEnum.Open, sqlTransaction);
				exp = "DELETE FROM Unallocated_Lot_Items WHERE SysDocID = '" + sysDocID + "' AND VoucherID = '" + voucherID + "'";
				flag &= (ExecuteNonQuery(exp, sqlTransaction) >= 0);
				if (!flag)
				{
					return flag;
				}
				ActivityTypes activityType = ActivityTypes.Void;
				if (!isVoid)
				{
					activityType = ActivityTypes.Unvoid;
				}
				AddActivityLog("Consign In Return", voucherID, sysDocID, activityType, sqlTransaction);
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

		public bool DeleteConsignInReturn(string sysDocID, string voucherID)
		{
			bool flag = true;
			try
			{
				string text = "";
				SqlTransaction sqlTransaction = base.DBConfig.StartNewTransaction();
				flag &= DeleteConsignInReturnDetailsRows(sysDocID, voucherID, isDeletingTransaction: true, sqlTransaction);
				text = "DELETE FROM Consign_In WHERE SysDocID='" + sysDocID + "' AND VoucherID = '" + voucherID + "'";
				flag &= (ExecuteNonQuery(text, sqlTransaction) >= 0);
				if (!flag)
				{
					return flag;
				}
				AddActivityLog("Consign In Return", voucherID, sysDocID, ActivityTypes.Delete, sqlTransaction);
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

		public DataSet GetUninvoicedConsignIns(string vendorID)
		{
			try
			{
				DataSet dataSet = new DataSet();
				string text = "SELECT DN.SysDocID [Doc ID],DN.VoucherID [Number],TransactionDate AS [Date],DN.VendorID + '-' + C.VendorName AS [Vendor] FROM Consign_In DN\r\n                             INNER JOIN Vendor C ON DN.VendorID=C.VendorID  WHERE ISNULL(IsVoid,'False')='False' AND ISNULL(IsInvoiced,'False')='False'";
				if (vendorID != "")
				{
					text = text + " AND DN.VendorID='" + vendorID + "'";
				}
				FillDataSet(dataSet, "ConsignIn_Return", text);
				return dataSet;
			}
			catch
			{
				throw;
			}
		}

		public DataSet GetConsignInReturnToPrint(string sysDocID, string voucherID)
		{
			return GetConsignInReturnToPrint(sysDocID, new string[1]
			{
				voucherID
			});
		}

		public DataSet GetConsignInReturnToPrint(string sysDocID, string[] voucherID)
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
				string cmdText = "SELECT CR.*,VendorName,CI.TransactionDate [Con Date],\r\n                                CI.SalesPersonID,CI.RequiredDate,CA.AddressPrintFormat AS ShippingAddress,ShippingMethodName,\r\n                                ISNULL(CI.CurrencyID,(SELECT CurrencyID FROM Currency WHERE IsBase='True')) AS CurrencyID,\r\n                                CI.TermID,TermName,CI.IsVoid,CI.Reference,Discount AS Discount,\r\n                                Total AS Total,CI.PONumber,CI.Note FROM ConsignIn_Return CR LEFT JOIN Consign_In CI ON CR.ConsignSysDocID=CI.SysDocID AND CR.ConsignVoucherID=CI.VoucherID\r\n                                LEFT JOIN Vendor ON CI.VendorID=Vendor.VendorID\r\n                                LEFT JOIN Payment_Term PT ON CI.TermID=PT.PaymentTermID\r\n                                LEFT JOIN Vendor_Address CA ON CA.AddressID=CI.ShippingAddressID AND CA.VendorID=CI.VendorID\r\n                                LEFT JOIN Shipping_Method SM ON SM.ShippingMethodID=CI.ShippingMethodID\r\n                                WHERE CR.SysDocID = '" + sysDocID + "' AND CR.VoucherID IN (" + text + ")";
				SqlCommand sqlCommand = new SqlCommand(cmdText);
				FillDataSet(dataSet, "ConsignIn_Return", sqlCommand);
				if (dataSet == null || dataSet.Tables.Count == 0 || dataSet.Tables["ConsignIn_Return"].Rows.Count == 0)
				{
					return null;
				}
				cmdText = "SELECT SD.*,COD.Quantity AS DeliveredQty,COD.RowIndex AS ConsignRowIndex,QuantitySettled,QuantityReturned,\r\n                        COD.Quantity - ISNULL(QuantitySettled,0)-ISNULL(QuantityReturned,0) + SD.Quantity AS BalanceQty\r\n                        FROM ConsignIn_Return_Detail SD INNER JOIN Product P ON SD.ProductID=P.ProductID\r\n                        INNER JOIN Consign_In_Detail COD ON COD.SysDocID=SD.ConsignSysDocID\r\n                        AND COD.VoucherID=SD.ConsignVoucherID AND COD.RowIndex=SD.ConsignRowIndex\r\n                        WHERE SD.SysDocID='" + sysDocID + "' AND SD.VoucherID IN (" + text + ") ";
				FillDataSet(dataSet, "ConsignIn_Return_Detail", cmdText);
				dataSet.Relations.Add("VendorConsignIn", new DataColumn[2]
				{
					dataSet.Tables["ConsignIn_Return"].Columns["SysDocID"],
					dataSet.Tables["ConsignIn_Return"].Columns["VoucherID"]
				}, new DataColumn[2]
				{
					dataSet.Tables["ConsignIn_Return_Detail"].Columns["SysDocID"],
					dataSet.Tables["ConsignIn_Return_Detail"].Columns["VoucherID"]
				}, createConstraints: false);
				dataSet.Tables["ConsignIn_Return"].Columns.Add("TotalInWords", typeof(string));
				foreach (DataRow row in dataSet.Tables["ConsignIn_Return"].Rows)
				{
					decimal result = default(decimal);
					decimal.TryParse(row["Total"].ToString(), out result);
					row["TotalInWords"] = NumToWord.GetNumInWords(result);
				}
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
			string text3 = "SELECT    ISNULL(IsVoid,'False') AS V, INV.SysDocID [Doc ID],INV.VoucherID [Doc Number],INV.VendorID [Vendor Code],VendorName [Vendor Name],TransactionDate [Delivery Date],\r\n                            INV.SalespersonID [Salesperson],SUM(DND.Quantity) AS Quantity\r\n                            FROM         Delivery_note INV\r\n                            INNER JOIN Consign_In_Detail DND ON DND.SysDocID=INV.SysDocID AND DND.VoucherID=INV.VoucherID\r\n                            Inner JOIN Vendor ON VENDOR.VendorID=INV.VendorID ";
			if (from != DateTime.MinValue)
			{
				text3 = text3 + " WHERE TransactionDate Between '" + text + "' AND '" + text2 + "'";
			}
			if (!showVoid)
			{
				text3 += " AND ISNULL(IsVoid,'False')='False'";
			}
			text3 += " GROUP BY IsVoid,INV.SysDocID,INV.VoucherID,INV.VendorID ,VendorName,TransactionDate,INV.SalespersonID";
			SqlCommand sqlCommand = new SqlCommand(text3);
			FillDataSet(dataSet, "ConsignIn_Return", sqlCommand);
			return dataSet;
		}

		public DataSet GetOpenConsignments(string vendorID)
		{
			try
			{
				DataSet dataSet = new DataSet();
				string str = "SELECT CO.SysDocID [Doc ID], CO.VoucherID [Number],CO.VendorID [Vendor Code], CUS.VendorName [Vendor Name],TransactionDate [Date]\r\n                            FROM Consign_In CO INNER JOIN Consign_In_Detail COD\r\n                            ON CO.SysDocID=COD.SysDocID AND CO.VoucherID=COD.VoucherID\r\n                            INNER JOIN Vendor CUS ON CO.VendorID=CUS.VendorID ";
				if (vendorID != "")
				{
					str = str + " WHERE CO.VendorID='" + vendorID + "'";
				}
				str += " GROUP BY CO.SysDocID, CO.VoucherID,CO.VendorID,TransactionDate, CUS.VendorName\r\n                                HAVING\r\n                                SUM(ISNULL(COD.Quantity,0)-ISNULL(COD.QuantitySettled,0))>0 ";
				SqlCommand sqlCommand = new SqlCommand(str);
				FillDataSet(dataSet, "ConsignIn_Return", sqlCommand);
				return dataSet;
			}
			catch
			{
				throw;
			}
		}

		public bool ConsignmentHasSettlement(string sysDocID, string voucherNumber, SqlTransaction sqlTransaction)
		{
			string exp = "Select Count(*) FROM Consign_In_Detail POD\r\n                                WHERE SysdocID='" + sysDocID + "' AND VoucherID='" + voucherNumber + "' Having SUM(ISNULL(QuantitySettled,0) + ISNULL(QuantityReturned,0)) >0";
			object obj = ExecuteScalar(exp, sqlTransaction);
			if (obj != null && obj.ToString() != "" && int.Parse(obj.ToString()) > 0)
			{
				return true;
			}
			return false;
		}
	}
}
