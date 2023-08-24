using Micromind.Common;
using Micromind.Common.Data;
using Micromind.Common.Libraries;
using System;
using System.Collections;
using System.Data;
using System.Data.SqlClient;

namespace Micromind.Data
{
	public sealed class W3PLGRN : StoreObject
	{
		private const string W3PLGRN_TABLE = "W3PL_GRN";

		private const string W3PLGRNDETAIL_TABLE = "W3PL_GRN_Detail";

		private const string SYSDOCID_PARM = "@SysDocID";

		private const string VOUCHERID_PARM = "@VoucherID";

		private const string COMPANYID_PARM = "@CompanyID";

		private const string DIVISIONID_PARM = "@DivisionID";

		private const string CUSTOMERID_PARM = "@CustomerID";

		private const string TRANSACTIONDATE_PARM = "@TransactionDate";

		private const string SALESPERSONID_PARM = "@SalespersonID";

		private const string ROWINDEX_PARM = "@RowIndex";

		private const string REQUIREDDATE_PARM = "@RequiredDate";

		private const string CUSTOMERADDRESS_PARM = "@CustomerAddress";

		private const string STATUS_PARM = "@Status";

		private const string REFERENCE_PARM = "@Reference";

		private const string NOTE_PARM = "@Note";

		private const string ISVOID_PARM = "@IsVoid";

		private const string PONUMBER_PARM = "@PONumber";

		private const string CLOSEDATE_PARM = "@CloseDate";

		private const string CLOSENOTE_PARM = "@CloseNote";

		private const string TRANSPORTERID_PARM = "@TransporterID";

		private const string ARRIVALPORT_PARM = "@ArrivalPortID";

		private const string ARRIVALDATE_PARM = "@ArrivalDate";

		private const string CONTAINERNO_PARM = "@ContainerNo";

		private const string BLNO_PARM = "@BLNo";

		private const string PRODUCTID_PARM = "@ProductID";

		private const string QUANTITY_PARM = "@Quantity";

		private const string UNITPRICE_PARM = "@UnitPrice";

		private const string DESCRIPTION_PARM = "@Description";

		private const string UNITID_PARM = "@UnitID";

		private const string UNITQUANTITY_PARM = "@UnitQuantity";

		private const string UNITFACTOR_PARM = "@UnitFactor";

		private const string FACTORTYPE_PARM = "@FactorType";

		private const string SUBUNITPRICE_PARM = "@SubunitPrice";

		private const string LOCATIONID_PARM = "@LocationID";

		private const string CONSIGNLOCATIONID_PARM = "@ConsignLocationID";

		private const string ORDERVOUCHERID_PARM = "@OrderVoucherID";

		private const string ORDERSYSDOCID_PARM = "@OrderSysDocID";

		private const string INVOICEVOUCHERID_PARM = "@InvoiceVoucherID";

		private const string INVOICESYSDOCID_PARM = "@InvoiceSysDocID";

		private const string ORDERROWINDEX_PARM = "@OrderRowIndex";

		private const string CREATEDBY_PARM = "@CreatedBy";

		private const string DATECREATED_PARM = "@DateCreated";

		private const string UPDATEDBY_PARM = "@UpdatedBy";

		private const string DATEUPDATED_PARM = "@DateUpdated";

		public W3PLGRN(Config config)
			: base(config)
		{
		}

		private string GetInsertUpdateGRNText(bool isUpdate)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddInsertUpdateParameters("W3PL_GRN", new FieldValue("SysDocID", "@SysDocID", isUpdateConditionField: true), new FieldValue("VoucherID", "@VoucherID", isUpdateConditionField: true), new FieldValue("DivisionID", "@DivisionID"), new FieldValue("CompanyID", "@CompanyID"), new FieldValue("CustomerID", "@CustomerID"), new FieldValue("TransactionDate", "@TransactionDate"), new FieldValue("SalespersonID", "@SalespersonID"), new FieldValue("RequiredDate", "@RequiredDate"), new FieldValue("CustomerAddress", "@CustomerAddress"), new FieldValue("Status", "@Status"), new FieldValue("PONumber", "@PONumber"), new FieldValue("Reference", "@Reference"), new FieldValue("TransporterID", "@TransporterID"), new FieldValue("ArrivalPort", "@ArrivalPortID"), new FieldValue("ArrivalDate", "@ArrivalDate"), new FieldValue("ContainerNo", "@ContainerNo"), new FieldValue("BLNo", "@BLNo"), new FieldValue("Note", "@Note"));
			if (isUpdate)
			{
				sqlBuilder.AddInsertUpdateParameters("W3PL_GRN", new FieldValue("DateUpdated", "@DateUpdated", isUpdateConditionField: true, checkForNullValue: true));
			}
			if (isUpdate)
			{
				return sqlBuilder.GetUpdateExpression();
			}
			return sqlBuilder.GetInsertExpression();
		}

		private SqlCommand GetInsertUpdateGRNCommand(bool isUpdate)
		{
			SqlParameterCollection parameters;
			if (isUpdate)
			{
				if (updateCommand != null)
				{
					updateCommand = null;
				}
				updateCommand = new SqlCommand(GetInsertUpdateGRNText(isUpdate: true), base.DBConfig.Connection);
				updateCommand.CommandType = CommandType.Text;
				parameters = updateCommand.Parameters;
			}
			else
			{
				if (insertCommand != null)
				{
					insertCommand = null;
				}
				insertCommand = new SqlCommand(GetInsertUpdateGRNText(isUpdate: false), base.DBConfig.Connection);
				insertCommand.CommandType = CommandType.Text;
				parameters = insertCommand.Parameters;
			}
			parameters.Add("@SysDocID", SqlDbType.NVarChar);
			parameters.Add("@VoucherID", SqlDbType.NVarChar);
			parameters.Add("@DivisionID", SqlDbType.NVarChar);
			parameters.Add("@CompanyID", SqlDbType.TinyInt);
			parameters.Add("@CustomerID", SqlDbType.NVarChar);
			parameters.Add("@TransactionDate", SqlDbType.DateTime);
			parameters.Add("@SalespersonID", SqlDbType.NVarChar);
			parameters.Add("@RequiredDate", SqlDbType.DateTime);
			parameters.Add("@CustomerAddress", SqlDbType.NVarChar);
			parameters.Add("@Status", SqlDbType.TinyInt);
			parameters.Add("@Reference", SqlDbType.NVarChar);
			parameters.Add("@PONumber", SqlDbType.NVarChar);
			parameters.Add("@TransporterID", SqlDbType.NVarChar);
			parameters.Add("@ArrivalPortID", SqlDbType.NVarChar);
			parameters.Add("@ArrivalDate", SqlDbType.DateTime);
			parameters.Add("@ContainerNo", SqlDbType.NVarChar);
			parameters.Add("@BLNo", SqlDbType.NVarChar);
			parameters.Add("@Note", SqlDbType.NVarChar);
			parameters["@SysDocID"].SourceColumn = "SysDocID";
			parameters["@VoucherID"].SourceColumn = "VoucherID";
			parameters["@DivisionID"].SourceColumn = "DivisionID";
			parameters["@CompanyID"].SourceColumn = "CompanyID";
			parameters["@CustomerID"].SourceColumn = "CustomerID";
			parameters["@TransactionDate"].SourceColumn = "TransactionDate";
			parameters["@SalespersonID"].SourceColumn = "SalespersonID";
			parameters["@RequiredDate"].SourceColumn = "RequiredDate";
			parameters["@CustomerAddress"].SourceColumn = "CustomerAddress";
			parameters["@Status"].SourceColumn = "Status";
			parameters["@Reference"].SourceColumn = "Reference";
			parameters["@PONumber"].SourceColumn = "PONumber";
			parameters["@TransporterID"].SourceColumn = "TransporterID";
			parameters["@ArrivalPortID"].SourceColumn = "ArrivalPort";
			parameters["@ArrivalDate"].SourceColumn = "ArrivalDate";
			parameters["@ContainerNo"].SourceColumn = "ContainerNo";
			parameters["@BLNo"].SourceColumn = "BLNo";
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

		private string GetGRNCloseText(bool isUpdate)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddInsertUpdateParameters("W3PL_GRN", new FieldValue("SysDocID", "@SysDocID", isUpdateConditionField: true), new FieldValue("VoucherID", "@VoucherID", isUpdateConditionField: true), new FieldValue("CloseDate", "@CloseDate"), new FieldValue("CloseNote", "@CloseNote"), new FieldValue("Status", "@Status"));
			if (isUpdate)
			{
				return sqlBuilder.GetUpdateExpression();
			}
			return sqlBuilder.GetInsertExpression();
		}

		private SqlCommand GetGRNCloseCommand()
		{
			if (updateCommand != null)
			{
				updateCommand = null;
			}
			updateCommand = new SqlCommand(GetGRNCloseText(isUpdate: true), base.DBConfig.Connection);
			updateCommand.CommandType = CommandType.Text;
			SqlParameterCollection parameters = updateCommand.Parameters;
			parameters.Add("@SysDocID", SqlDbType.NVarChar);
			parameters.Add("@VoucherID", SqlDbType.NVarChar);
			parameters.Add("@CloseDate", SqlDbType.DateTime);
			parameters.Add("@CloseNote", SqlDbType.NVarChar);
			parameters.Add("@Status", SqlDbType.TinyInt);
			parameters["@SysDocID"].SourceColumn = "SysDocID";
			parameters["@VoucherID"].SourceColumn = "VoucherID";
			parameters["@CloseDate"].SourceColumn = "CloseDate";
			parameters["@CloseNote"].SourceColumn = "CloseNote";
			parameters["@Status"].SourceColumn = "Status";
			return updateCommand;
		}

		private string GetInsertUpdateGRNDetailsText(bool isUpdate)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddInsertUpdateParameters("W3PL_GRN_Detail", new FieldValue("SysDocID", "@SysDocID"), new FieldValue("VoucherID", "@VoucherID"), new FieldValue("ProductID", "@ProductID"), new FieldValue("LocationID", "@LocationID"), new FieldValue("RowIndex", "@RowIndex"), new FieldValue("Quantity", "@Quantity"), new FieldValue("UnitPrice", "@UnitPrice"), new FieldValue("Description", "@Description"), new FieldValue("UnitID", "@UnitID"), new FieldValue("UnitQuantity", "@UnitQuantity"), new FieldValue("UnitFactor", "@UnitFactor"), new FieldValue("FactorType", "@FactorType"), new FieldValue("SubunitPrice", "@SubunitPrice"), new FieldValue("OrderSysDocID", "@OrderSysDocID"), new FieldValue("OrderVoucherID", "@OrderVoucherID"), new FieldValue("InvoiceRowIndex", "@OrderRowIndex"));
			if (isUpdate)
			{
				return sqlBuilder.GetUpdateExpression();
			}
			return sqlBuilder.GetInsertExpression();
		}

		private SqlCommand GetInsertUpdateGRNDetailsCommand(bool isUpdate)
		{
			SqlParameterCollection parameters;
			if (isUpdate)
			{
				if (updateCommand != null)
				{
					updateCommand = null;
				}
				updateCommand = new SqlCommand(GetInsertUpdateGRNDetailsText(isUpdate: true), base.DBConfig.Connection);
				updateCommand.CommandType = CommandType.Text;
				parameters = updateCommand.Parameters;
			}
			else
			{
				if (insertCommand != null)
				{
					insertCommand = null;
				}
				insertCommand = new SqlCommand(GetInsertUpdateGRNDetailsText(isUpdate: false), base.DBConfig.Connection);
				insertCommand.CommandType = CommandType.Text;
				parameters = insertCommand.Parameters;
			}
			parameters.Add("@SysDocID", SqlDbType.NVarChar);
			parameters.Add("@VoucherID", SqlDbType.NVarChar);
			parameters.Add("@ProductID", SqlDbType.NVarChar);
			parameters.Add("@LocationID", SqlDbType.NVarChar);
			parameters.Add("@RowIndex", SqlDbType.Int);
			parameters.Add("@Quantity", SqlDbType.Real);
			parameters.Add("@UnitPrice", SqlDbType.Decimal);
			parameters.Add("@Description", SqlDbType.NVarChar);
			parameters.Add("@UnitID", SqlDbType.NVarChar);
			parameters.Add("@UnitQuantity", SqlDbType.Real);
			parameters.Add("@UnitFactor", SqlDbType.Decimal);
			parameters.Add("@FactorType", SqlDbType.NVarChar);
			parameters.Add("@OrderSysDocID", SqlDbType.NVarChar);
			parameters.Add("@OrderVoucherID", SqlDbType.NVarChar);
			parameters.Add("@OrderRowIndex", SqlDbType.Int);
			parameters.Add("@SubunitPrice", SqlDbType.Decimal);
			parameters["@SysDocID"].SourceColumn = "SysDocID";
			parameters["@VoucherID"].SourceColumn = "VoucherID";
			parameters["@ProductID"].SourceColumn = "ProductID";
			parameters["@LocationID"].SourceColumn = "LocationID";
			parameters["@RowIndex"].SourceColumn = "RowIndex";
			parameters["@Quantity"].SourceColumn = "Quantity";
			parameters["@UnitPrice"].SourceColumn = "UnitPrice";
			parameters["@Description"].SourceColumn = "Description";
			parameters["@UnitID"].SourceColumn = "UnitID";
			parameters["@UnitQuantity"].SourceColumn = "UnitQuantity";
			parameters["@UnitFactor"].SourceColumn = "UnitFactor";
			parameters["@FactorType"].SourceColumn = "FactorType";
			parameters["@SubunitPrice"].SourceColumn = "SubunitPrice";
			parameters["@OrderVoucherID"].SourceColumn = "OrderVoucherID";
			parameters["@OrderSysDocID"].SourceColumn = "OrderSysDocID";
			parameters["@OrderRowIndex"].SourceColumn = "InvoiceRowIndex";
			if (isUpdate)
			{
				return updateCommand;
			}
			return insertCommand;
		}

		private bool ValidateData(W3PLGRNData journalData)
		{
			return true;
		}

		public bool AllowModify(string sysDocID, string voucherNumber, SqlTransaction sqlTransaction)
		{
			string exp = "SELECT COUNT(*) FROM W3PL_Invoice_Detail WHERE SourceSysDocID = '" + sysDocID + "' AND SourceVoucherID = '" + voucherNumber + "'";
			object obj = ExecuteScalar(exp, sqlTransaction);
			if (obj != null && obj.ToString() != "" && int.Parse(obj.ToString()) > 0)
			{
				return false;
			}
			return true;
		}

		public bool InsertUpdateGRN(W3PLGRNData w3PLW3PLGRNData, bool isUpdate)
		{
			bool flag = true;
			SqlCommand insertUpdateGRNCommand = GetInsertUpdateGRNCommand(isUpdate);
			try
			{
				DataRow dataRow = w3PLW3PLGRNData.W3PLGRNTable.Rows[0];
				SqlTransaction sqlTransaction = base.DBConfig.StartNewTransaction();
				string text = dataRow["VoucherID"].ToString();
				string text2 = dataRow["SysDocID"].ToString();
				if (!isUpdate)
				{
					if (new SystemDocuments(base.DBConfig).ExistDocumentNumber("W3PL_GRN", "VoucherID", dataRow["SysDocID"].ToString(), text, sqlTransaction))
					{
						throw new CompanyException("Document number already exist.", 1046);
					}
				}
				else if (!AllowModify(text2, text, sqlTransaction))
				{
					throw new CompanyException("Unable to modify or delete the transaction. Some of the items in this transaction are in use", 1037);
				}
				bool flag2 = new Products(base.DBConfig).HasInUseLots(text2, text);
				foreach (DataRow row in w3PLW3PLGRNData.W3PLGRNDetailTable.Rows)
				{
					row["SysDocID"] = dataRow["SysDocID"];
					row["VoucherID"] = dataRow["VoucherID"];
					string text3 = row["ProductID"].ToString();
					string text4 = "";
					object fieldValue = new Databases(base.DBConfig).GetFieldValue("Product", "UnitID", "ProductID", text3, sqlTransaction);
					if (fieldValue != null)
					{
						text4 = fieldValue.ToString();
					}
					if (text4 != "" && row["UnitID"] != DBNull.Value && row["UnitID"].ToString() != text4)
					{
						DataRow obj = new Products(base.DBConfig).GetProductUnitRow(text3, row["UnitID"].ToString()) ?? throw new CompanyException("One of the selected units is not assigned to item.\nItem:" + text3 + "\nUnit:" + row["UnitID"].ToString());
						float num = float.Parse(obj["Factor"].ToString());
						string text5 = obj["FactorType"].ToString();
						float num2 = float.Parse(row["Quantity"].ToString());
						row["UnitFactor"] = num;
						row["FactorType"] = text5;
						row["UnitQuantity"] = row["Quantity"];
						num2 = ((!(text5 == "M")) ? float.Parse(Math.Round(num2 * num, 5).ToString()) : float.Parse(Math.Round(num2 / num, 5).ToString()));
						row["Quantity"] = num2;
					}
				}
				insertUpdateGRNCommand.Transaction = sqlTransaction;
				flag = (isUpdate ? (flag & Update(w3PLW3PLGRNData, "W3PL_GRN", insertUpdateGRNCommand)) : (flag & Insert(w3PLW3PLGRNData, "W3PL_GRN", insertUpdateGRNCommand)));
				if (isUpdate && flag2)
				{
					string textCommand = "SELECT ProductID,Quantity,RowIndex,LocationID FROM W3PL_GRN_Detail WHERE SysDocID = '" + text2 + "' AND VoucherID = '" + text + "'";
					DataSet dataSet = new DataSet();
					FillDataSet(dataSet, "W3PL_GRN_Detail", textCommand, sqlTransaction);
					DataTable dataTable = dataSet.Tables["W3PL_GRN_Detail"];
					W3PLGRNData w3PLGRNData = new W3PLGRNData();
					foreach (DataRow row2 in w3PLW3PLGRNData.W3PLGRNDetailTable.Rows)
					{
						bool flag3 = false;
						if (row2["IsNewRow"] != DBNull.Value)
						{
							flag3 = Convert.ToBoolean(row2["IsNewRow"].ToString());
						}
						string text6 = row2["ProductID"].ToString();
						string text7 = row2["LocationID"].ToString();
						int num3 = int.Parse(row2["RowIndex"].ToString());
						decimal num4 = decimal.Parse(row2["Quantity"].ToString());
						decimal num5 = default(decimal);
						decimal num6 = default(decimal);
						if (flag3)
						{
							DataRow dataRow4 = w3PLGRNData.W3PLGRNDetailTable.NewRow();
							foreach (DataColumn column in w3PLGRNData.W3PLGRNDetailTable.Columns)
							{
								dataRow4[column.ColumnName] = row2[column.ColumnName];
							}
							dataRow4["VoucherID"] = "_" + text;
							w3PLGRNData.W3PLGRNDetailTable.Rows.Add(dataRow4);
							DataRow[] array = w3PLW3PLGRNData.Tables["Product_Lot_Receiving_Detail"].Select("ProductID = '" + text6 + "' AND RowIndex = " + num3);
							for (int i = 0; i < array.Length; i++)
							{
								DataRow dataRow5 = w3PLGRNData.Tables["Product_Lot_Receiving_Detail"].NewRow();
								foreach (DataColumn column2 in w3PLGRNData.Tables["Product_Lot_Receiving_Detail"].Columns)
								{
									dataRow5[column2.ColumnName] = array[i][column2.ColumnName];
								}
								w3PLGRNData.Tables["Product_Lot_Receiving_Detail"].Rows.Add(dataRow5);
							}
						}
						else
						{
							DataRow[] array2 = dataTable.Select("ProductID = '" + text6 + "' AND LocationID = '" + text7 + "' AND RowIndex = " + num3);
							if (array2.Length == 0)
							{
								throw new CompanyException("Cannot remove old items from this GRN because some lots are already issued.\nItem: " + text6, 1060);
							}
							num5 = decimal.Parse(array2[0]["Quantity"].ToString());
							if (!(num5 == num4))
							{
								if (num4 < num5)
								{
									throw new CompanyException("Reducing quantity for a GRN which has used lots is not allowed.", 1059);
								}
								num6 = num4 - num5;
								textCommand = "   UPDATE Inventory_Transactions SET Quantity = Quantity + " + num6 + "\r\n                                        WHERE SysDocID = '" + text2 + "' AND VoucherID = '" + text + "' AND ProductID = '" + text6 + "' AND RowIndex = " + num3 + "\r\n\r\n\r\n                                        UPDATE Product_Lot SET LotQty = LotQty + " + num6 + "\r\n                                        WHERE DocID = '" + text2 + "' AND ReceiptNumber = '" + text + "' AND ItemCode = '" + text6 + "' AND RowIndex = " + num3 + "\r\n\r\n                                        UPDATE W3PL_GRN_Detail SET Quantity = Quantity + " + num6 + "\r\n                                        WHERE SysDocID = '" + text2 + "' AND VoucherID = '" + text + "' AND ProductID = '" + text6 + "' AND RowIndex = " + num3 + "\r\n\r\n                                        UPDATE Product_Lot_Receiving_Detail SET LotQty = LotQty + " + num6 + "\r\n                                        WHERE SysDocID = '" + text2 + "' AND VoucherID = '" + text + "' AND ProductID = '" + text6 + "' AND RowIndex = " + num3 + "\r\n\r\n                                        UPDATE Product_Location SET Quantity = Quantity + " + num6 + "\r\n                                        WHERE ProductID = '" + text6 + "' AND LocationID = '" + text7 + "'\r\n\r\n                                        UPDATE Product SET Quantity = Quantity + " + num6 + "\r\n                                        WHERE ProductID = '" + text6 + "' ";
								flag &= (ExecuteNonQuery(textCommand, sqlTransaction) > 0);
							}
						}
					}
					if (w3PLGRNData != null && w3PLGRNData.W3PLGRNDetailTable.Rows.Count > 0)
					{
						insertUpdateGRNCommand = GetInsertUpdateGRNDetailsCommand(isUpdate: false);
						insertUpdateGRNCommand.Transaction = sqlTransaction;
						if (w3PLGRNData.Tables["W3PL_GRN_Detail"].Rows.Count > 0)
						{
							flag &= Insert(w3PLGRNData, "W3PL_GRN_Detail", insertUpdateGRNCommand);
						}
						InventoryTransactionData inventoryTransactionData = new InventoryTransactionData();
						foreach (DataRow row3 in w3PLGRNData.W3PLGRNDetailTable.Rows)
						{
							DataRow dataRow7 = inventoryTransactionData.InventoryTransactionTable.NewRow();
							dataRow7.BeginEdit();
							dataRow7["SysDocID"] = row3["SysDocID"];
							dataRow7["VoucherID"] = row3["VoucherID"];
							dataRow7["LocationID"] = row3["LocationID"];
							dataRow7["ProductID"] = row3["ProductID"];
							dataRow7["UnitID"] = row3["UnitID"];
							dataRow7["Quantity"] = row3["Quantity"];
							dataRow7["UnitQuantity"] = row3["UnitQuantity"];
							dataRow7["Factor"] = row3["UnitFactor"];
							dataRow7["FactorType"] = row3["FactorType"];
							dataRow7["Reference"] = dataRow["Reference"];
							dataRow7["RowIndex"] = row3["RowIndex"];
							dataRow7["PayeeType"] = "V";
							dataRow7["PayeeID"] = dataRow["CustomerID"];
							dataRow7["SysDocType"] = (byte)105;
							dataRow7["Cost"] = 0;
							dataRow7["TransactionDate"] = dataRow["TransactionDate"];
							dataRow7["TransactionType"] = (byte)20;
							dataRow7["DivisionID"] = dataRow["DivisionID"];
							dataRow7["CompanyID"] = dataRow["CompanyID"];
							dataRow7.EndEdit();
							inventoryTransactionData.InventoryTransactionTable.Rows.Add(dataRow7);
						}
						inventoryTransactionData.Merge(w3PLGRNData.Tables["Product_Lot_Receiving_Detail"]);
						flag &= new Products(base.DBConfig).InsertUpdateProductLotReceivingDetail(w3PLGRNData, isUpdate: false, sqlTransaction);
						flag &= new InventoryTransaction(base.DBConfig).InsertUpdateInventoryTransaction(inventoryTransactionData, isUpdate, sqlTransaction);
						textCommand = "    UPDATE W3PL_GRN_Detail SET VoucherID = '" + text + "'\r\n                                WHERE SysDocID = '" + text2 + "' AND VoucherID = '_" + text + "'\r\n\r\n                                UPDATE Inventory_Transactions SET VoucherID = '" + text + "'\r\n                                WHERE SysDocID = '" + text2 + "' AND VoucherID = '_" + text + "'\r\n\r\n                                UPDATE Product_Lot SET ReceiptNumber = '" + text + "'\r\n                                WHERE DocID = '" + text2 + "' AND ReceiptNumber = '_" + text + "' \r\n\r\n                                UPDATE Product_Lot_Receiving_Detail SET VoucherID = '" + text + "'\r\n                                WHERE SysDocID = '" + text2 + "' AND VoucherID = '_" + text + "'  ";
						flag &= (ExecuteNonQuery(textCommand, sqlTransaction) > 0);
					}
				}
				else
				{
					insertUpdateGRNCommand = GetInsertUpdateGRNDetailsCommand(isUpdate: false);
					insertUpdateGRNCommand.Transaction = sqlTransaction;
					if (isUpdate)
					{
						flag &= DeleteGRNDetailsRows(text2, text, sqlTransaction);
						flag &= new InventoryTransaction(base.DBConfig).DeleteInventoryTransaction(105, text2, text, isDeletingTransaction: false, sqlTransaction);
					}
					if (w3PLW3PLGRNData.Tables["W3PL_GRN_Detail"].Rows.Count > 0)
					{
						flag &= Insert(w3PLW3PLGRNData, "W3PL_GRN_Detail", insertUpdateGRNCommand);
					}
					InventoryTransactionData inventoryTransactionData2 = new InventoryTransactionData();
					foreach (DataRow row4 in w3PLW3PLGRNData.W3PLGRNDetailTable.Rows)
					{
						DataRow dataRow9 = inventoryTransactionData2.InventoryTransactionTable.NewRow();
						dataRow9.BeginEdit();
						dataRow9["SysDocID"] = row4["SysDocID"];
						dataRow9["VoucherID"] = row4["VoucherID"];
						dataRow9["LocationID"] = row4["LocationID"];
						dataRow9["ProductID"] = row4["ProductID"];
						dataRow9["UnitID"] = row4["UnitID"];
						dataRow9["Quantity"] = row4["Quantity"];
						dataRow9["UnitQuantity"] = row4["UnitQuantity"];
						dataRow9["Factor"] = row4["UnitFactor"];
						dataRow9["FactorType"] = row4["FactorType"];
						dataRow9["Reference"] = dataRow["Reference"];
						dataRow9["RowIndex"] = row4["RowIndex"];
						dataRow9["PayeeType"] = "V";
						dataRow9["PayeeID"] = dataRow["CustomerID"];
						dataRow9["SysDocType"] = (byte)105;
						dataRow9["Cost"] = 0;
						dataRow9["TransactionDate"] = dataRow["TransactionDate"];
						dataRow9["TransactionType"] = (byte)20;
						dataRow9["DivisionID"] = dataRow["DivisionID"];
						dataRow9["CompanyID"] = dataRow["CompanyID"];
						dataRow9.EndEdit();
						inventoryTransactionData2.InventoryTransactionTable.Rows.Add(dataRow9);
					}
					inventoryTransactionData2.Merge(w3PLW3PLGRNData.Tables["Product_Lot_Receiving_Detail"]);
					flag &= new Products(base.DBConfig).InsertUpdateProductLotReceivingDetail(w3PLW3PLGRNData, isUpdate: false, sqlTransaction);
					flag &= new InventoryTransaction(base.DBConfig).InsertUpdateInventoryTransaction(inventoryTransactionData2, isUpdate, sqlTransaction);
				}
				if (!flag)
				{
					throw new Exception("Failed to save the transaction because of an unexpected error. Please try again.");
				}
				flag &= UpdateTableRowInsertUpdateInfo("W3PL_GRN", "SysDocID", dataRow["SysDocID"].ToString(), "VoucherID", dataRow["VoucherID"].ToString(), sqlTransaction, !isUpdate);
				string entityName = "3PL GRN";
				flag = (isUpdate ? (flag & AddActivityLog(entityName, text, text2, ActivityTypes.Update, sqlTransaction)) : (flag & AddActivityLog(entityName, text, text2, ActivityTypes.Add, sqlTransaction)));
				if (!isUpdate)
				{
					flag &= new SystemDocuments(base.DBConfig).UpdateNextDocumentNumber(dataRow["SysDocID"].ToString(), dataRow["VoucherID"].ToString(), "W3PL_GRN", "VoucherID", sqlTransaction);
				}
				if (!flag)
				{
					return flag;
				}
				flag &= new Approval(base.DBConfig).CreateTransactionApprovalTasks(SysDocTypes.GJournal, text2, text, "W3PL_GRN", sqlTransaction);
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

		public bool CloseGRN(W3PLGRNData w3PLW3PLGRNData)
		{
			bool flag = true;
			SqlCommand gRNCloseCommand = GetGRNCloseCommand();
			try
			{
				DataRow dataRow = w3PLW3PLGRNData.W3PLGRNTable.Rows[0];
				SqlTransaction sqlTransaction = base.DBConfig.StartNewTransaction();
				string text = dataRow["VoucherID"].ToString();
				string text2 = dataRow["SysDocID"].ToString();
				bool flag2 = false;
				object fieldValue = new Databases(base.DBConfig).GetFieldValue("W3PL_GRN", "Status", "SysDocID", text2, "VoucherID", text, sqlTransaction);
				if (fieldValue != null && fieldValue.ToString() != "")
				{
					flag2 = (byte.Parse(fieldValue.ToString()) == 3);
				}
				if (flag2)
				{
					throw new CompanyException("This GRN is already closed.");
				}
				dataRow["Status"] = W3PLGRNStatusEnum.Closed;
				gRNCloseCommand.Transaction = sqlTransaction;
				flag &= Update(w3PLW3PLGRNData, "W3PL_GRN", gRNCloseCommand);
				GLData journalData = CreateConsignCloseGLData(w3PLW3PLGRNData, sqlTransaction);
				flag &= new Journal(base.DBConfig).InsertUpdateJournal(journalData, isUpdate: false, sqlTransaction);
				if (!flag)
				{
					return flag;
				}
				string entityName = "GRN Closing";
				flag &= AddActivityLog(entityName, text, text2, ActivityTypes.Update, sqlTransaction);
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

		public W3PLGRNData GetGRNByID(string sysDocID, string voucherID)
		{
			try
			{
				W3PLGRNData w3PLGRNData = new W3PLGRNData();
				string textCommand = "SELECT * FROM W3PL_GRN WHERE VoucherID='" + voucherID + "' AND SysDocID='" + sysDocID + "'";
				FillDataSet(w3PLGRNData, "W3PL_GRN", textCommand);
				if (w3PLGRNData == null || w3PLGRNData.Tables.Count == 0 || w3PLGRNData.Tables["W3PL_GRN"].Rows.Count == 0)
				{
					return null;
				}
				textCommand = "SELECT TD.*,TD.Quantity-ISNULL(QuantityInvoiced,0)  AS QuantityBalance,Product.Description,Product.ItemType\r\n                        FROM W3PL_GRN_Detail TD INNER JOIN Product ON TD.ProductID=Product.ProductID\r\n                        WHERE VoucherID='" + voucherID + "' AND SysDocID='" + sysDocID + "' ORDER BY RowIndex";
				FillDataSet(w3PLGRNData, "W3PL_GRN_Detail", textCommand);
				textCommand = "SELECT * FROM Product_Lot_Receiving_Detail\r\n\t\t\t\t\t\tWHERE VoucherID='" + voucherID + "' AND SysDocID='" + sysDocID + "' ORDER BY RowIndex";
				FillDataSet(w3PLGRNData, "Product_Lot_Receiving_Detail", textCommand);
				textCommand = "SELECT * FROM W3PL_Invoice\r\n\t\t\t\t\t\tWHERE ConsignSysDocID='" + sysDocID + "' AND ConsignVoucherID='" + voucherID + "'";
				FillDataSet(w3PLGRNData, "W3PL_Invoice", textCommand);
				return w3PLGRNData;
			}
			catch
			{
				throw;
			}
		}

		private GLData CreateConsignCloseGLData(W3PLGRNData transactionData, SqlTransaction sqlTransaction)
		{
			try
			{
				GLData gLData = new GLData();
				DataRow dataRow = transactionData.W3PLGRNTable.Rows[0];
				int currencyDecimalPoints = new CompanyInformations(base.DBConfig).CurrencyDecimalPoints;
				dataRow["CustomerID"].ToString();
				string text = dataRow["SysDocID"].ToString();
				string text2 = dataRow["VoucherID"].ToString();
				string textCommand = "SELECT Loc.GRNAccountID,LOC.GRNDiffAccountID\r\n                                FROM System_Document SD INNER JOIN Location LOC ON SD.LocationID = LOC.LocationID\r\n                                WHERE SysDocID = '" + text + "'";
				DataSet dataSet = new DataSet();
				FillDataSet(dataSet, "Accounts", textCommand, sqlTransaction);
				if (dataSet == null || dataSet.Tables.Count == 0 || dataSet.Tables[0].Rows.Count == 0)
				{
					throw new CompanyException("There is no location assigned to this system document or location record is missing.");
				}
				_ = dataSet.Tables["Accounts"].Rows[0];
				new Databases(base.DBConfig).GetFieldValue("System_Document", "LocationID", "SysDocID", text, sqlTransaction).ToString();
				string value = "";
				string value2 = "";
				DataRow dataRow2 = gLData.JournalTable.NewRow();
				SysDocTypes sysDocTypes = SysDocTypes.W3PLDelivery;
				dataRow2["JournalID"] = 0;
				dataRow2["JournalDate"] = dataRow["TransactionDate"];
				dataRow2["SysDocID"] = dataRow["SysDocID"];
				dataRow2["SysDocType"] = (byte)sysDocTypes;
				dataRow2["VoucherID"] = dataRow["VoucherID"];
				dataRow2["Reference"] = dataRow["Reference"];
				dataRow2["Narration"] = "GRN Closing - " + text + "#" + text2;
				dataRow2.EndEdit();
				gLData.JournalTable.Rows.Add(dataRow2);
				decimal num = default(decimal);
				DataSet gRNClosingSummary = GetGRNClosingSummary(text, text2, sqlTransaction);
				DataRow dataRow3 = gRNClosingSummary.Tables["W3PL_GRN"].Rows[0];
				decimal d = default(decimal);
				decimal d2 = default(decimal);
				if (dataRow3["ActualSales"] != DBNull.Value)
				{
					d = decimal.Parse(dataRow3["ActualSales"].ToString());
				}
				if (dataRow3["SettledSales"] != DBNull.Value)
				{
					d2 = decimal.Parse(dataRow3["SettledSales"].ToString());
				}
				decimal num2 = d - d2;
				DataRow dataRow4 = null;
				if (num2 != 0m)
				{
					dataRow4 = gLData.JournalDetailsTable.NewRow();
					dataRow4.BeginEdit();
					dataRow4["JournalID"] = 0;
					dataRow4["AccountID"] = value2;
					if (num2 > 0m)
					{
						dataRow4["Debit"] = DBNull.Value;
						dataRow4["Credit"] = num2;
					}
					else
					{
						dataRow4["Debit"] = Math.Abs(num2);
						dataRow4["Credit"] = DBNull.Value;
					}
					dataRow4.EndEdit();
					gLData.JournalDetailsTable.Rows.Add(dataRow4);
					dataRow4 = gLData.JournalDetailsTable.NewRow();
					dataRow4.BeginEdit();
					dataRow4["JournalID"] = 0;
					dataRow4["AccountID"] = value;
					if (num2 > 0m)
					{
						dataRow4["Debit"] = num2;
						dataRow4["Credit"] = DBNull.Value;
					}
					else
					{
						dataRow4["Debit"] = DBNull.Value;
						dataRow4["Credit"] = Math.Abs(num2);
					}
					dataRow4.EndEdit();
					gLData.JournalDetailsTable.Rows.Add(dataRow4);
				}
				Hashtable hashtable = new Hashtable();
				ArrayList arrayList = new ArrayList();
				string text3 = "";
				foreach (DataRow row in gRNClosingSummary.Tables["Bills"].Rows)
				{
					decimal result = default(decimal);
					decimal result2 = default(decimal);
					decimal.TryParse(row["Amount"].ToString(), out result);
					decimal.TryParse(row["Billed"].ToString(), out result2);
					num2 = result2 - result;
					text3 = row["AccountID"].ToString();
					if (hashtable.ContainsKey(text3))
					{
						num = decimal.Parse(hashtable[text3].ToString());
						num += Math.Round(num2, currencyDecimalPoints);
						hashtable[text3] = num;
					}
					else
					{
						hashtable.Add(text3, Math.Round(num2, currencyDecimalPoints));
						arrayList.Add(text3);
					}
				}
				decimal num3 = default(decimal);
				for (int i = 0; i < hashtable.Count; i++)
				{
					dataRow4 = gLData.JournalDetailsTable.NewRow();
					dataRow4.BeginEdit();
					text3 = arrayList[i].ToString();
					num = decimal.Parse(hashtable[text3].ToString());
					if (!(num == 0m))
					{
						dataRow4 = gLData.JournalDetailsTable.NewRow();
						dataRow4.BeginEdit();
						dataRow4["JournalID"] = 0;
						dataRow4["AccountID"] = text3;
						if (num > 0m)
						{
							dataRow4["Debit"] = num;
							dataRow4["Credit"] = DBNull.Value;
						}
						else
						{
							dataRow4["Debit"] = DBNull.Value;
							dataRow4["Credit"] = Math.Abs(num);
						}
						dataRow4.EndEdit();
						gLData.JournalDetailsTable.Rows.Add(dataRow4);
						num3 += num;
					}
				}
				if (num3 != 0m)
				{
					dataRow4 = gLData.JournalDetailsTable.NewRow();
					dataRow4.BeginEdit();
					dataRow4["JournalID"] = 0;
					dataRow4["AccountID"] = value2;
					if (num3 > 0m)
					{
						dataRow4["Debit"] = DBNull.Value;
						dataRow4["Credit"] = num3;
					}
					else
					{
						dataRow4["Debit"] = Math.Abs(num3);
						dataRow4["Credit"] = DBNull.Value;
					}
					dataRow4["Reference"] = dataRow["Reference"];
					dataRow4["Description"] = dataRow["Note"];
					dataRow4.EndEdit();
					gLData.JournalDetailsTable.Rows.Add(dataRow4);
				}
				return gLData;
			}
			catch
			{
				throw;
			}
		}

		internal bool DeleteGRNDetailsRows(string sysDocID, string voucherID, SqlTransaction sqlTransaction)
		{
			bool flag = true;
			try
			{
				W3PLGRNData w3PLGRNData = new W3PLGRNData();
				string textCommand = "SELECT SOD.*,ISVOID FROM W3PL_GRN_Detail SOD INNER JOIN W3PL_GRN SO ON SO.SysDocID=SOD.SysDocID AND SO.VOucherID=SOD.VoucherID\r\n                              WHERE SOD.SysDocID = '" + sysDocID + "' AND SOD.VoucherID = '" + voucherID + "'";
				FillDataSet(w3PLGRNData, "W3PL_GRN_Detail", textCommand, sqlTransaction);
				bool result = false;
				bool.TryParse(w3PLGRNData.W3PLGRNDetailTable.Rows[0]["IsVoid"].ToString(), out result);
				textCommand = "DELETE FROM Product_Lot_Receiving_Detail WHERE SysDocID = '" + sysDocID + "' AND VoucherID = '" + voucherID + "'";
				flag &= Delete(textCommand, sqlTransaction);
				textCommand = "DELETE FROM W3PL_GRN_Detail WHERE SysDocID = '" + sysDocID + "' AND VoucherID = '" + voucherID + "'";
				return flag & Delete(textCommand, sqlTransaction);
			}
			catch
			{
				throw;
			}
		}

		public bool VoidGRN(string sysDocID, string voucherID, bool isVoid)
		{
			bool flag = true;
			try
			{
				SqlTransaction sqlTransaction = base.DBConfig.StartNewTransaction();
				if (!AllowModify(sysDocID, voucherID, sqlTransaction))
				{
					throw new CompanyException("Unable to modify or delete the transaction. Some of the items in this transaction are in use", 1037);
				}
				if (new Products(base.DBConfig).HasInUseLots(sysDocID, voucherID))
				{
					throw new CompanyException("This transaction cannot be modifed because some of items are refered by other transactions.");
				}
				string exp = "UPDATE W3PL_GRN SET IsVoid = '" + isVoid.ToString() + "' WHERE SysDocID='" + sysDocID + "' AND VoucherID = '" + voucherID + "'";
				flag &= (ExecuteNonQuery(exp, sqlTransaction) > 0);
				W3PLGRNData dataSet = new W3PLGRNData();
				exp = "SELECT * FROM W3PL_GRN_Detail\r\n                              WHERE SysDocID = '" + sysDocID + "' AND VoucherID = '" + voucherID + "'";
				FillDataSet(dataSet, "W3PL_GRN_Detail", exp, sqlTransaction);
				flag &= new InventoryTransaction(base.DBConfig).DeleteInventoryTransaction(105, sysDocID, voucherID, isDeletingTransaction: true, sqlTransaction);
				if (!flag)
				{
					return flag;
				}
				ActivityTypes activityType = ActivityTypes.Void;
				if (!isVoid)
				{
					activityType = ActivityTypes.Unvoid;
				}
				AddActivityLog("Consign In", voucherID, sysDocID, activityType, sqlTransaction);
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

		public bool DeleteGRN(string sysDocID, string voucherID)
		{
			bool flag = true;
			try
			{
				string text = "";
				SqlTransaction sqlTransaction = base.DBConfig.StartNewTransaction();
				flag &= DeleteGRNDetailsRows(sysDocID, voucherID, sqlTransaction);
				text = "DELETE FROM W3PL_GRN WHERE SysDocID='" + sysDocID + "' AND VoucherID = '" + voucherID + "'";
				flag &= (ExecuteNonQuery(text, sqlTransaction) >= 0);
				flag &= new InventoryTransaction(base.DBConfig).DeleteInventoryTransaction(105, sysDocID, voucherID, isDeletingTransaction: true, sqlTransaction);
				if (!flag)
				{
					return flag;
				}
				AddActivityLog("Consign In", voucherID, sysDocID, ActivityTypes.Delete, sqlTransaction);
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

		public DataSet GetGRNsItemsToInvoice(string sysDocID, string voucherID, DateTime date)
		{
			try
			{
				DataSet dataSet = new DataSet();
				string textCommand = "SELECT GRD.SysDocID,GRD.VoucherID,CustomerID,GRD.ProductID,GRD.Description,GRD.Quantity,GRD.QuantityInvoiced,GRD.Description,GRD.RowIndex,GRD.LocationID ,\r\n                                P.Weight,  GRD.Quantity - ISNULL(QuantityInvoiced,0) AS BalanceQuantity ,GR.TransactionDate AS ReceiveDate,\r\n                                (SELECT TOP 1 TransactionDate FROM W3PL_Invoice WI INNER JOIN W3PL_Invoice_Detail WID ON WI.SysDocID = WID.SysDocID AND WI.VoucherID=WID.VoucherID\r\n\t\t\t\t\t\t\t\t WHERE WID.SourceSysDocID = GRD.SysDocID AND WID.SourceVoucherID = GRD.VoucherID AND WID.ProductID = GRD.ProductID ORDER BY TransactionDate DESC) AS LastInvoiceDate\r\n                                FROM W3PL_GRN_Detail GRD INNER JOIN W3PL_GRN GR ON GR.SysDocID = GRD.SysDocID AND GR.VoucherID = GRD.VoucherID \r\n                                INNER JOIN Product P ON P.ProductID = GRD.ProductID WHERE GRD.Quantity - ISNULL(QuantityInvoiced,0) > 0 AND GR.SysDocID = '" + sysDocID + "' AND GR.VoucherID = '" + voucherID + "' ";
				FillDataSet(dataSet, "GRN", textCommand);
				if (dataSet == null || dataSet.Tables.Count == 0 || dataSet.Tables[0].Rows.Count == 0)
				{
					return null;
				}
				string text = CommonLib.ToSqlDateTimeString(date);
				DateTime dateTime = DateTime.Parse(dataSet.Tables[0].Rows[0]["ReceiveDate"].ToString());
				CommonLib.ToSqlDateTimeString(dateTime);
				DateTime date2 = dateTime;
				if (!dataSet.Tables[0].Rows[0]["LastInvoiceDate"].IsDBNullOrEmpty())
				{
					date2 = DateTime.Parse(dataSet.Tables[0].Rows[0]["LastInvoiceDate"].ToString());
				}
				CommonLib.ToSqlDateTimeString(date2);
				textCommand = "SELECT GRND.SysDocID,GRND.VoucherID,GRND.ProductID,GRND.Description,GRND.RowIndex,GRND.Quantity,\r\n                        ISNULL( (SELECT TOP 1 EndDate FROM W3PL_Invoice_Detail WID INNER JOIN W3PL_Invoice WI ON WI.SysDocID = WID.SysDocID AND WI.VoucherID=WID.VoucherID\r\n\t\t\t\t\t\t\t\t WHERE WID.SourceSysDocID = GRND.SysDocID AND WID.SourceVoucherID = GRND.VoucherID AND WID.ProductID = GRND.ProductID ORDER BY EndDate DESC),GRN.TransactionDate) AS LastRentDate,\r\n\t\t\t\t\t    ISNULL((SELECT SUM(Quantity)  FROM  W3PL_Delivery_Detail WDD  \r\n\t\t\t\t\t\t\t\t WHERE WDD.SourceSysDocID = GRND.SysDocID AND WDD.SourceVoucherID = GRND.VoucherID AND WDD.ProductID = GRND.ProductID),0) AS QuantityDelivered,P.Weight,ISNULL(QuantityInvoiced,0) AS QuantityInvoiced,\r\n                        CASE WHEN ISNULL(P.W3PLRentPrice,0) = 0 THEN ISNULL(PC.W3PLRentPrice,0) ELSE ISNULL(P.W3PLRentPrice,0) END AS RentPrice\r\n                        FROM W3PL_GRN_Detail GRND INNER JOIN W3PL_GRN GRN ON GRN.SysDocID = GRND.SysDocID AND GRN.VoucherID = GRND.VoucherID \r\n                        INNER JOIN Product P ON P.ProductID = GRND.ProductID\r\n                       \r\n                        LEFT OUTER JOIN Product_Class PC ON PC.ClassID = P.ClassID\r\n                        WHERE ISNULL(LastRentDate,GRN.TransactionDate) < '" + text + "' AND GRN.SysDocID = '" + sysDocID + "' AND GRN.VoucherID = '" + voucherID + "'";
				DataSet dataSet2 = new DataSet();
				FillDataSet(dataSet2, "Items", textCommand);
				textCommand = "SELECT INVD.SysDocID,INVD.VoucherID,ProductID,TransactionDate,Quantity,RowIndex FROM W3PL_Delivery_Detail INVD INNER JOIN W3PL_Delivery INV ON INV.SysDocID = INVD.SysDocID AND INV.VoucherID = INVD.VoucherID\r\n                        WHERE INVD.SourceSysDocID = '" + sysDocID + "' AND INVD.SourceVoucherID = '" + voucherID + "'";
				FillDataSet(dataSet2, "Invoice", textCommand);
				textCommand = "SELECT DISTINCT PLS.DocID,PLS.InvoiceNumber,PLS.RowINdex,PLS.LotNo,PLS.SoldQty AS Quantity,DN.TransactionDate,PLS.ItemCode,ISNULL(PL.DocID,PLX.DocID) AS SourceSysDocID,\r\n                            ISNULL(PL.ReceiptNumber,PLX.ReceiptNumber) AS SourceVoucherID, ISNULL(PL.RowIndex,PLX.RowIndex) AS SourceRowIndex\r\n                             FROM Product_Lot_Sales PLS INNER JOIN  W3PL_Delivery_Detail DND ON PLS.DocID = DND.SysDocID AND PLS.InvoiceNumber = DND.VoucherID \r\n                            INNER JOIN W3PL_Delivery DN ON DND.SysDocID = DN.SysDocID AND DND.VoucherID = DN.VoucherID\r\n                             INNER JOIN Product_Lot PLX ON PLX.LotNumber = PLS.LotNo\r\n                             LEFT OUTER JOIN Product_Lot PL ON PL.LotNumber = PLX.SourceLotNumber\r\n                                                    WHERE  DN.TransactionDate < '" + text + "' AND ISNULL(PL.DocID,PLX.DocID) = '" + sysDocID + "' AND ISNULL(PL.ReceiptNumber,PLX.ReceiptNumber) = '" + voucherID + "'";
				FillDataSet(dataSet2, "DNItems", textCommand);
				int num = 7;
				DataTable dataTable = dataSet.Tables.Add("Items");
				dataTable.Columns.Add("GRNSysDocID");
				dataTable.Columns.Add("GRNVoucherID");
				dataTable.Columns.Add("GRNRowIndex", typeof(int));
				dataTable.Columns.Add("ProductID");
				dataTable.Columns.Add("Description");
				dataTable.Columns.Add("StoreQuantity", typeof(decimal));
				dataTable.Columns.Add("DeliveredQuantity", typeof(decimal));
				dataTable.Columns.Add("BalanceQuantity", typeof(decimal));
				dataTable.Columns.Add("StartDate", typeof(DateTime));
				dataTable.Columns.Add("EndDate", typeof(DateTime));
				dataTable.Columns.Add("UnitWeight", typeof(decimal));
				dataTable.Columns.Add("RentPrice", typeof(decimal));
				dataTable.Columns.Add("TotalWeight", typeof(decimal));
				foreach (DataRow row in dataSet2.Tables["Items"].Rows)
				{
					decimal d = decimal.Parse(row["Quantity"].ToString());
					decimal d2 = decimal.Parse(row["QuantityInvoiced"].ToString());
					_ = d - d2;
					DateTime dateTime2 = DateTime.Parse(row["LastRentDate"].ToString()).AddDays(1.0);
					dateTime2 = new DateTime(dateTime2.Year, dateTime2.Month, dateTime2.Day, 0, 0, 0);
					string text2 = row["ProductID"].ToString();
					decimal d3 = default(decimal);
					int num2 = int.Parse(Math.Ceiling((decimal)int.Parse(Math.Ceiling((date - dateTime2).TotalDays).ToString()) / (decimal)num).ToString());
					object obj = dataSet2.Tables["DNItems"].Compute("SUM(Quantity)", " TransactionDate < '" + dateTime2 + "' AND ItemCode = '" + text2 + "'");
					if (!obj.IsNullOrEmpty())
					{
						d3 = decimal.Parse(obj.ToString());
					}
					for (int i = 0; i < num2; i++)
					{
						DateTime dateTime3 = dateTime2.AddDays(i * num);
						DateTime dateTime4 = dateTime3.AddDays(num);
						dateTime3 = new DateTime(dateTime3.Year, dateTime3.Month, dateTime3.Day);
						dateTime4 = dateTime3.AddDays(7.0).AddSeconds(-1.0);
						object obj2 = dataSet2.Tables["DNItems"].Compute("SUM(Quantity)", "TransactionDate > '" + dateTime3 + "' AND TransactionDate < '" + dateTime4 + "' AND ItemCode = '" + text2 + "'");
						decimal num3 = default(decimal);
						if (!obj2.IsNullOrEmpty())
						{
							num3 = decimal.Parse(obj2.ToString());
						}
						if (d - d3 == 0m)
						{
							break;
						}
						DataRow dataRow2 = dataTable.NewRow();
						dataRow2["GRNSysDocID"] = row["SysDocID"];
						dataRow2["GRNVoucherID"] = row["VoucherID"];
						dataRow2["GRNRowIndex"] = row["RowIndex"];
						dataRow2["StartDate"] = dateTime3;
						dataRow2["EndDate"] = dateTime4;
						dataRow2["ProductID"] = row["ProductID"];
						dataRow2["Description"] = row["Description"];
						dataRow2["StoreQuantity"] = d - d3;
						dataRow2["DeliveredQuantity"] = num3;
						dataRow2["BalanceQuantity"] = d - d3 - num3;
						dataRow2["UnitWeight"] = row["Weight"];
						dataRow2["RentPrice"] = row["RentPrice"];
						decimal d4 = default(decimal);
						if (!row["Weight"].IsDBNullOrEmpty())
						{
							d4 = decimal.Parse(row["Weight"].ToString());
						}
						dataRow2["TotalWeight"] = (d - d3) * d4;
						d3 += num3;
						dataTable.Rows.Add(dataRow2);
					}
				}
				return dataSet;
			}
			catch
			{
				throw;
			}
		}

		public DataSet GetGRNsToInvoice(string vendorID)
		{
			try
			{
				DataSet dataSet = new DataSet();
				string text = "SELECT DN.SysDocID [Doc ID],DN.VoucherID [Number],TransactionDate AS [Date],DN.CustomerID + '-' + C.CustomerName AS [Customer] FROM W3PL_GRN DN\r\n                             INNER JOIN Customer C ON DN.CustomerID=C.CustomerID  WHERE ISNULL(IsVoid,'False')='False' AND ISNULL(IsInvoiced,'False')='False'";
				if (vendorID != "")
				{
					text = text + " AND DN.CustomerID='" + vendorID + "'";
				}
				FillDataSet(dataSet, "W3PL_GRN", text);
				return dataSet;
			}
			catch
			{
				throw;
			}
		}

		public DataSet GetUninvoicedGRNs(string vendorID)
		{
			try
			{
				DataSet dataSet = new DataSet();
				string str = "SELECT DN.SysDocID [Doc ID],DN.VoucherID [Number],TransactionDate AS [Date],DN.CustomerID + '-' + C.CustomerName AS [Customer] ,\r\nSUM(DND.Quantity) AS [Received Quantity],SUM(DND.Quantity)-ISNULL(X.InvQuantity,0) AS [Balance Quantity]\r\nFROM W3PL_GRN DN\r\n                             INNER JOIN Customer C ON DN.CustomerID=C.CustomerID \r\n\t\t\t\t\t\t\t INNER JOIN W3PL_GRN_Detail DND ON DN.SysDocID = DND.SysDocID AND DN.VoucherID = DND.VoucherID \r\n\t\t\t\t\t\t\t LEFT OUTER JOIN (select SourceSysDocID AS   DocID,SourceVoucherID AS GRNNumber, SUM(DLQuantity) InvQuantity\r\n FROM W3PL_Invoice_Detail INV   \r\nGROUP BY SourceSysDocID,SourceVoucherID) X ON X.DocID = DN.SysDocID AND X.GRNNumber = DN.VoucherID\r\n\r\n\t\t\t\t\t\t\t WHERE ISNULL(IsVoid,'False')='False'\r\n\t\t\t\t\t\t\t  AND ISNULL(IsInvoiced,'False')='False' AND DN.CustomerID='" + vendorID + "' \r\n\t\t\t\t\t\t\t ";
				if (vendorID != "")
				{
					str = str + " AND DN.CustomerID='" + vendorID + "'";
				}
				str += "  GROUP BY DN.SysDocID,DN.VoucherID ,TransactionDate,DN.CustomerID, C.CustomerName ,X.InvQuantity\r\n\t\t\t\t\t\t\t HAVING   ISNULL(X.InvQuantity,0) < ISNULL(SUM(DND.Quantity),0)";
				FillDataSet(dataSet, "W3PL_GRN", str);
				return dataSet;
			}
			catch
			{
				throw;
			}
		}

		public DataSet GetGRNSummaryReport(string sysDocID, string voucherID)
		{
			try
			{
				DataSet dataSet = new DataSet();
				string textCommand = "SELECT CI.SysDocID,CI.VoucherID, CI.SysDocID + '#' + CI.VoucherID GRNNo, CustomerName,VA.Address1,CI.ContainerNo,CI.TransactionDate,T.TransporterName,CI.PONumber,CI.BLNo,CI.Reference,P.PortName\r\n                                FROM  W3PL_GRN CI LEFT JOIN Customer V ON CI.CustomerID=V.CustomerID LEFT JOIN Customer_Address VA ON V.CustomerID=VA.CustomerID\r\n                                LEFT JOIN Transporter T ON T.TransporterID=CI.TransporterID LEFT JOIN Port P ON CI.ArrivalPort=P.PortID\r\n                                WHERE SysDocID = '" + sysDocID + "' AND VoucherID = '" + voucherID + "' ";
				FillDataSet(dataSet, "Customer", textCommand);
				textCommand = "    SELECT DocID,ReceiptNumber,LotNumber, ItemCode, Description, LotQty,SUM(ISNULL([Quantity Delivered],0))  AS [Quantity Delivered] ,\r\n                             SUM(ISNULL(FOC,0)) AS FOC,  SUM(ISNULL([Quantity Invoiced],0)) AS [Quantity Invoiced],\r\n                             SUM(ISNULL(Amount,0)) AS Amount \r\n\t\t\t\t\t\t\t\r\n\t\t\t\t\t\t\tFROM (\r\n                            SELECT PL.DocID,PL.ReceiptNumber,PL.LotNumber,  CON.ProductID AS ItemCode,CON.Description,PL.LotQty,SUM(ISNULL(CON.DeliveredQty,0)) AS [Quantity Delivered],\r\n                            SUM(NonSaleQty) +  SUM(ISNULL(CON.FOCQuantity,0)) AS FOC,\r\n\t\t\t\t\t\t\tSUM(ISNULL(CON.InvoiceQty,0) - ISNULL(CON.FOCQuantity,0)) AS [Quantity Invoiced]\r\n                            , SUM(ISNULL(ISNULL(Round((CON.InvoiceQty - ISNULL(FOCQuantity,0)) * Con.UnitPrice,2),0),0)) AS Amount \r\n\t\t\t\t\t\t\t\r\n\t\t\t\t\t\t\tFROM (\r\n                            SELECT PLD.*,P.Description, IT.TransactionDate,CASE WHEN IT.PayeeType = 'C' THEN IT.PayeeID ELSE NULL END AS CustomerID ,CUS.CustomerName, ISNULL(INV.VoucherID,PLD.VoucherID) AS InvoiceVoucherID, \r\n                            CASE WHEN INV.TransactionDate IS NULL AND CashInv.TransactionDate IS NULL  THEN 0 ELSE  ISNULL(INV.UnitPrice,CashInv.UnitPrice) END AS UnitPrice,\r\n\t\t\t\t\t\t\t CASE WHEN INV.TransactionDate IS NULL AND CashInv.TransactionDate IS NULL  THEN 0 ELSE ISNULL(SoldQty,0)  - (ISNULL(DRD.Quantity,0) + ISNULL(CRSR.Quantity,0) + ISNULL(SRD.Quantity,0)) END AS InvoiceQty,INV.TransactionDate AS InvoiceDate,\r\n                            ISNULL(SoldQty,0)    AS DeliveredQty \r\n\r\n                            FROM (\r\n                            SELECT SD.SysDocType,SD.DocName, PLS.DocID AS SysDocID, InvoiceNumber AS VoucherID,RowIndex,ItemCode AS ProductID,PLS.LocationID,LotNo AS LotNumber,SoldQty,ISNULL(FOCQuantity,0) AS FOCQuantity, CASE WHEN SD.SysDocType IN (87) THEN SoldQty ELSE 0 END  AS NonSaleQty\r\n                            FROM Product_Lot_Sales PLS INNER JOIN System_Document SD ON PLS.DocID = SD.SysDocID \r\n                            WHERE LotNo IN \r\n                            (SELECT LotNumber FROM Product_Lot PL WHERE (ReceiptNumber = '" + voucherID + "' AND DocID = '" + sysDocID + "') OR SourceLotNumber IN (SELECT PL.LotNumber FROM Product_Lot PL WHERE (ReceiptNumber = '" + voucherID + "' AND DocID = '" + sysDocID + "')))\r\n                            AND SysDocType NOT IN (19,20,21,40)\r\n\r\n                            UNION\r\n\r\n                            SELECT SD.SysDocType,SD.DocName,PLS.SysDocID, VoucherID,RowIndex, ProductID,PLS.LocationID,SourceLotNumber AS  LotNumber,-1 * LotQty AS SoldQty,0 AS FOCQuantity, 0 AS NonSaleQty\r\n                            FROM Product_Lot_Receiving_Detail PLS INNER JOIN System_Document SD ON PLS.SysDocID = SD.SysDocID WHERE SourceLotNumber IN \r\n                            (SELECT LotNumber FROM Product_Lot PL WHERE (ReceiptNumber = '" + voucherID + "' AND DocID = '" + sysDocID + "') OR SourceLotNumber IN (SELECT PL.LotNumber FROM Product_Lot PL WHERE (ReceiptNumber = '" + voucherID + "' AND DocID = '" + sysDocID + "')))\r\n                            AND SysDocType NOT IN (19,20,21,40))  AS PLD\r\n                            LEFT OUTER JOIN Inventory_Transactions IT ON IT.SysDocID = PLD.SysDocID AND IT.VoucherID = PLD.VoucherID AND IT.RowIndex = PLD.RowIndex\r\n                            LEFT OUTER JOIN  (SELECT SID2.Quantity,SID2.OrderSysDocID,SID2.VoucherID,SID2.OrderVoucherID,SID2.OrderRowIndex,SID2.UnitPrice,SI2.TransactionDate\r\n\t\t\t\t\t\t\t FROM Sales_Invoice_Detail SID2 INNER JOIN Sales_INVOICE SI2 ON SID2.SysDocID = SI2.SysDocID AND SID2.VoucherID = SI2.VoucherID AND ISNULL(SI2.IsVoid,'False') = 'False' ) AS INV ON INV.OrderSysDocID = PLD.SysDocID\r\n\t\t\t\t\t\t\t AND INV.OrderVoucherID = PLD.VoucherID AND INV.OrderRowIndex = PLD.RowIndex\r\n\r\n\t\t\t\t\t\t\t --Cash Sales\r\n\t\t\t\t\t\t\t LEFT OUTER JOIN (SELECT SID3.Quantity,SID3.SysDocID,SID3.VoucherID,SID3.RowIndex, SID3.UnitPrice,SI3.TransactionDate\r\n\t\t\t\t\t\t\t FROM Sales_Invoice_Detail SID3 INNER JOIN Sales_INVOICE SI3 ON SID3.SysDocID = SI3.SysDocID AND SID3.VoucherID = SI3.VoucherID AND ISNULL(SI3.IsVoid,'False') = 'False' ) AS CashInv\r\n\t\t\t\t\t\t\t ON CashInv.SysDocID = PLD.SysDocID  AND CashInv.VoucherID = PLD.VoucherID AND CashInv.RowIndex = PLD.RowIndex\r\n\r\n                            LEFT OUTER JOIN Sales_Return_Detail  SRD ON SRD.SourceSysDocID = PLD.SysDocID AND SRD.SourceVoucherID = PLD.VoucherID AND SRD.SourceRowIndex = PLD.RowIndex\r\n                           \r\n                            LEFT OUTER JOIN (SELECT SI2.OrderSysDocID,SI2.OrderVoucherID,SI2.OrderRowIndex, SRD2.Quantity FROM Sales_Return_Detail SRD2 INNER JOIN Sales_Invoice_Detail SI2 ON SRD2.SourceSysDocID=SI2.SysDocID\r\n\t\t\t\t\t\t\tAND SRD2.SourceVoucherID = SI2.voucherID AND SRD2.SourceRowIndex = SI2.rowIndex) CrSR ON CRSR.OrderSysDocID = PLD.SysDocID AND CRSR.OrderVoucherID = PLD.VoucherID AND CRSR.OrderRowIndex = PLD.RowIndex\r\n\r\n\r\n                            LEFT OUTER JOIN Product P ON P.ProductID = PLD.ProductID\r\n                             LEFT OUTER JOIN (SELECT DRD.Quantity,DR.SysDocID,DR.VoucherID,DRD.DNRowIndex,DR.DNoteSysDocID,DR.DNoteVoucherID FROM Delivery_Return DR\r\n\t\t\t\t\t                 INNER JOIN Delivery_Return_Detail DRD ON DRD.SysDocID = DR.SysDocID AND DRD.VoucherID = DR.VoucherID WHERE   ISNULL(DR.IsVoid,'False') = 'False') DRD\r\n\t\t\t\t\t\t\t\t\t ON  DRD.DNoteSysDocID = PLD.SysDocID AND DRD.DNoteVoucherID = PLD.VoucherID  AND  DRD.DNRowIndex = PLD.RowIndex \r\n                            LEFT OUTER JOIN Customer CUS ON CUS.CUstomerID = IT.PayeeID AND IT.PayeeType = 'C') CON\r\n                            INNER JOIN Product_Lot PL ON PL.DocID = '" + sysDocID + "' AND PL.ReceiptNumber = '" + voucherID + "' AND PL.ItemCode = CON.ProductID\r\n                            GROUP BY CON.ProductID,Con.Description, PL.DocID,PL.ReceiptNumber,PL.LotNumber,PL.LotQty   \r\n\r\n\r\n                                UNION\r\n\r\n                                SELECT CID.SysDocID AS DocID, CID.VoucherID AS ReceiptNumber,PL.LotNumber, CID.ProductID AS ItemCode,P.Description,CID.Quantity AS LotQty, 0 AS [Quantity Delivered], 0 AS FOC,0  AS [Quantity Invoiced], 0 AS Amount\r\n                                FROM W3PL_GRN_Detail CID INNER JOIN Consign_IN CI ON CID.SysDocID= CI.SysDocID AnD CID.VoucherID = CI.VoucherID\r\n                                INNER JOIN Product_LOT PL ON PL.DocID = CID.SysDocID AND PL.ReceiptNumber = CID.VoucherID AND PL.RowIndex = CID.RowIndex\r\n                                INNER JOIN Product P ON P.ProductID = CID.ProductID\r\n                                WHERE ISNULL(CI.IsVoid,'False')= 'False' AND CI.VoucherID = '" + voucherID + "' AND CI.SysDocID = '" + sysDocID + "' \r\n                                AND PL.LOTNumber  NOT  IN (SELECT LotNumber FROM Product_Lot_Issue_Detail PLI INNER JOIN System_Document SD ON SD.SysDocID = PLI.SysDocID WHERE SysDocType NOT IN (19,20,21,40))\r\n                                AND PL.LOTNumber  NOT  IN (SELECT SourceLotNumber FROM Product_Lot_Issue_Detail PLI INNER JOIN System_Document SD ON SD.SysDocID = PLI.SysDocID WHERE SOURCELotNumber IS NOT NULL AND SysDocType NOT IN (19,20,21,40))\r\n                                  ) AS X GROUP BY DocID,ReceiptNumber,LotNumber, ItemCode, Description, LotQty  ORDER BY ItemCode\r\n                                ";
				FillDataSet(dataSet, "W3PL_GRN", textCommand);
				textCommand = "SELECT '" + sysDocID + "' AS DocID, '" + voucherID + "' AS ReceiptNumber, CON.CustomerID,ISNULL(Con.CustomerName,'Non Sale Issue') AS CustomerName,CON.Description,COn.DocName,\r\n                        CASE WHEN SysDocType IN (87) THEN SUM(CON.DeliveredQty) ELSE 0 END  + SUM(CON.FOCQuantity) AS FOC,CON.InvoiceDate,SUM(CON.InvoiceQty - FOCQuantity) AS [Quantity Invoiced],\r\n                        CON.InvoiceVoucherID,COn.LocationID, CON.ProductID AS ItemCode,SUM(CON.DeliveredQty) AS [Quantity Delivered],CON.SysDocID,CON.SysDocType,CON.TransactionDate,CON.UnitPrice AS [Unit Price],\r\n                        CON.VoucherID, ISNULL(Round(SUM(CON.InvoiceQty - FOCQuantity) * Con.UnitPrice,2),0) AS Amount FROM \r\n\r\n\r\n                        (SELECT PLD.*,P.Description, IT.TransactionDate,CASE WHEN IT.PayeeType = 'C' THEN IT.PayeeID ELSE NULL END AS CustomerID ,\r\n                        CUS.CustomerName, ISNULL(INV.VoucherID,PLD.VoucherID) AS InvoiceVoucherID, \r\n                         CASE WHEN INV.TransactionDate IS NULL AND CashInv.TransactionDate IS NULL  THEN 0 ELSE ISNULL(INV.UnitPrice,CashInv.UnitPrice)  END AS UnitPrice,ISNULL(DRD.Quantity,0) + ISNULL(SRD.Quantity,0) AS QuantityReturned,\r\n                         CASE WHEN INV.TransactionDate IS NULL AND CashInv.TransactionDate IS NULL  THEN 0 ELSE ISNULL(SoldQty,0)- (ISNULL(DRD.Quantity,0) +  ISNULL(CRSR.Quantity,0) + ISNULL(SRD.Quantity,0))  END AS InvoiceQty ,ISNULL(INV.TransactionDate,CashInv.TransactionDate) AS InvoiceDate,\r\n                         ISNULL(SoldQty,0) - (ISNULL(DRD.Quantity,0) +  ISNULL(CRSR.Quantity,0) + ISNULL(SRD.Quantity,0))  AS DeliveredQty\r\n                          FROM ( \r\n  \r\n  \r\n                          SELECT SD.SysDocType,SD.DocName, PLS.DocID AS SysDocID, InvoiceNumber AS VoucherID,RowIndex,ItemCode AS ProductID,PLS.LocationID,LotNo AS LotNumber,SoldQty,ISNULL(FOCQuantity,0) AS FOCQuantity\r\n                          FROM Product_Lot_Sales PLS INNER JOIN System_Document SD ON PLS.DocID = SD.SysDocID WHERE LotNo IN \r\n                          (SELECT LotNumber FROM Product_Lot PL WHERE (ReceiptNumber = '" + voucherID + "' AND DocID = '" + sysDocID + "') OR SourceLotNumber IN \r\n                          (SELECT PL.LotNumber FROM Product_Lot PL WHERE (ReceiptNumber = '" + voucherID + "' AND DocID = '" + sysDocID + "')))\r\n                           AND SysDocType NOT IN (19,20,21,40)\r\n                         UNION\r\n\t\t                   SELECT SD.SysDocType,SD.DocName,PLS.SysDocID, VoucherID,RowIndex, ProductID,PLS.LocationID,SourceLotNumber AS  LotNumber,-1 * LotQty AS SoldQty,0 AS FOCQuantity\r\n\t\t                    FROM Product_Lot_Receiving_Detail PLS INNER JOIN System_Document SD ON PLS.SysDocID = SD.SysDocID WHERE SourceLotNumber IN \r\n\t\t\t                  (SELECT LotNumber FROM Product_Lot PL WHERE (ReceiptNumber = '" + voucherID + "' AND DocID = '" + sysDocID + "')\r\n\t\t\t                   OR SourceLotNumber IN (SELECT PL.LotNumber FROM Product_Lot PL WHERE (ReceiptNumber = '" + voucherID + "' AND DocID = '" + sysDocID + "')))\r\n\t\t\t                   AND SysDocType NOT IN (19,20,21,40))  AS PLD\r\n\r\n\t\t\t                    LEFT OUTER JOIN Inventory_Transactions IT ON IT.SysDocID = PLD.SysDocID AND IT.VoucherID = PLD.VoucherID AND IT.RowIndex = PLD.RowIndex\r\n\t\t\t\t                 LEFT OUTER JOIN  (SELECT SID2.Quantity,SID2.OrderSysDocID,SID2.VoucherID,SID2.OrderVoucherID,SID2.OrderRowIndex,SID2.UnitPrice,SI2.TransactionDate\r\n\t\t\t\t\t\t\t\t FROM Sales_Invoice_Detail SID2 INNER JOIN Sales_INVOICE SI2 ON SID2.SysDocID = SI2.SysDocID AND SID2.VoucherID = SI2.VoucherID AND ISNULL(SI2.IsVoid,'False') = 'False' ) AS INV ON INV.OrderSysDocID = PLD.SysDocID\r\n\t\t\t\t\t\t\t\t AND INV.OrderVoucherID = PLD.VoucherID AND INV.OrderRowIndex = PLD.RowIndex\r\n\r\n\t\t\t\t\t\t\t\t --Cash Sales\r\n\t\t\t\t\t\t\t\t LEFT OUTER JOIN (SELECT SID3.Quantity,SID3.SysDocID,SID3.VoucherID,SID3.RowIndex, SID3.UnitPrice,SI3.TransactionDate\r\n\t\t\t\t\t\t\t\t FROM Sales_Invoice_Detail SID3 INNER JOIN Sales_INVOICE SI3 ON SID3.SysDocID = SI3.SysDocID AND SID3.VoucherID = SI3.VoucherID AND ISNULL(SI3.IsVoid,'False') = 'False' ) AS CashInv\r\n\t\t\t\t\t\t\t\t ON CashInv.SysDocID = PLD.SysDocID  AND CashInv.VoucherID = PLD.VoucherID AND CashInv.RowIndex = PLD.RowIndex\r\n\r\n\t\t\t\t                     LEFT OUTER JOIN (SELECT DRD.Quantity,DR.SysDocID,DR.VoucherID,DRD.DNRowIndex,DR.DNoteSysDocID,DR.DNoteVoucherID FROM Delivery_Return DR\r\n\t\t\t\t\t                 INNER JOIN Delivery_Return_Detail DRD ON DRD.SysDocID = DR.SysDocID AND DRD.VoucherID = DR.VoucherID WHERE   ISNULL(DR.IsVoid,'False') = 'False') DRD\r\n\t\t\t\t\t\t\t\t\t ON  DRD.DNoteSysDocID = PLD.SysDocID AND DRD.DNoteVoucherID = PLD.VoucherID  AND  DRD.DNRowIndex = PLD.RowIndex \r\n\t\t\t\t\t                  LEFT OUTER JOIN Sales_Return_Detail  SRD ON SRD.SourceSysDocID = PLD.SysDocID AND SRD.SourceVoucherID = PLD.VoucherID AND SRD.SourceRowIndex = PLD.RowIndex  \r\n\t\t\t\t\t                 LEFT OUTER JOIN (SELECT SI2.OrderSysDocID,SI2.OrderVoucherID,SI2.OrderRowIndex, SRD2.Quantity FROM Sales_Return_Detail SRD2 INNER JOIN Sales_Invoice_Detail SI2 ON SRD2.SourceSysDocID=SI2.SysDocID\r\n\t\t\t\t\t\t\t\t\t\t\tAND SRD2.SourceVoucherID = SI2.voucherID AND SRD2.SourceRowIndex = SI2.rowIndex) CrSR ON CRSR.OrderSysDocID = PLD.SysDocID AND CRSR.OrderVoucherID = PLD.VoucherID AND CRSR.OrderRowIndex = PLD.RowIndex\r\n\r\n                                            LEFT OUTER JOIN Product P ON P.ProductID = PLD.ProductID\r\n\t\t\t\t\t                      LEFT OUTER JOIN Customer CUS ON CUS.CUstomerID = IT.PayeeID AND IT.PayeeType = 'C') CON\r\n\t\t\t\t\t\t                   WHERE DeliveredQty > 0 \r\n\t\t\t\t\t\t   GROUP BY   CON.CustomerID,Con.CustomerName,CON.Description,COn.DocName, CON.InvoiceDate, CON.InvoiceVoucherID,COn.LocationID, CON.ProductID ,CON.SysDocID,CON.SysDocType,CON.TransactionDate,CON.UnitPrice  , CON.VoucherID \r\n                                 ORDER BY ItemCode,TransactionDate  ";
				FillDataSet(dataSet, "W3PL_GRN_Detail", textCommand);
				textCommand = "SELECT   JD.SysDocID,JD.VoucherID,J.JournalDate, JournalDetailID AS RowIndex,JD.AccountID,A.AccountName,ConsignID,ConsignExpenseID,Debit AS Amount\r\n                        FROM Journal_Details JD LEFT JOIN Account A ON JD.AccountID = A.AccountID\r\n                        LEFT JOIN Journal J ON J.JournalID=JD.JournalID\r\n                        WHERE ISNULL(IsBilled,'False')='False' AND Debit IS NOT NULL AND ConsignID =  '" + sysDocID + "#" + voucherID + "'";
				FillDataSet(dataSet, "W3PL_Expense", textCommand);
				dataSet.Relations.Add("CustomerGRN", new DataColumn[2]
				{
					dataSet.Tables["Customer"].Columns["SysDocID"],
					dataSet.Tables["Customer"].Columns["VoucherID"]
				}, new DataColumn[2]
				{
					dataSet.Tables["W3PL_GRN"].Columns["DocID"],
					dataSet.Tables["W3PL_GRN"].Columns["ReceiptNumber"]
				}, createConstraints: false);
				dataSet.Relations.Add("CustomerGRNDetail", new DataColumn[2]
				{
					dataSet.Tables["Customer"].Columns["SysDocID"],
					dataSet.Tables["Customer"].Columns["VoucherID"]
				}, new DataColumn[2]
				{
					dataSet.Tables["W3PL_GRN_Detail"].Columns["DocID"],
					dataSet.Tables["W3PL_GRN_Detail"].Columns["ReceiptNumber"]
				}, createConstraints: false);
				dataSet.Relations.Add("CustomerGRNExpense", dataSet.Tables["Customer"].Columns["GRNNo"], dataSet.Tables["W3PL_Expense"].Columns["ConsignID"], createConstraints: false);
				return dataSet;
			}
			catch
			{
				throw;
			}
		}

		public DataSet GetGRNToPrint(string sysDocID, string voucherID)
		{
			return GetGRNToPrint(sysDocID, new string[1]
			{
				voucherID
			});
		}

		public DataSet GetGRNToPrint(string sysDocID, string[] voucherID)
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
				string cmdText = "SELECT SI.SysDocID, SI.VoucherID,SI.CustomerID,CustomerName, SI.TransactionDate,\r\n                                VA.AddressPrintFormat AS CustomerAddress,                               \r\n                                SI.IsVoid,SI.Reference,\r\n                                 SI.PONumber,SI.Note, \r\n                                (SELECT TransporterName FROM Transporter WHERE TransporterID = SI.TransporterID) AS TransporterName\r\n                                FROM  W3PL_GRN SI INNER JOIN Customer ON SI.CustomerID=Customer.CustomerID                              \r\n                                LEFT OUTER JOIN Customer_Address VA ON VA.CustomerID=SI.CustomerID AND VA.AddressID='PRIMARY'                                  \r\n                                WHERE SI.SysDocID = '" + sysDocID + "' AND SI.VoucherID IN (" + text + ")";
				SqlCommand sqlCommand = new SqlCommand(cmdText);
				FillDataSet(dataSet, "W3PL_GRN", sqlCommand);
				if (dataSet == null || dataSet.Tables.Count == 0 || dataSet.Tables["W3PL_GRN"].Rows.Count == 0)
				{
					return null;
				}
				cmdText = "SELECT DISTINCT PRD.SysDocID, PRD.VoucherID,PRD.ProductID,PRD.Description,ISNULL(PRD.UnitQuantity,PRD.Quantity) AS ReceivedQuantity, \r\n\t\t\t\t\t\t    (SELECT TOP 1 ISNULL(POD.UnitQuantity, POD.Quantity) FROM Purchase_Order_Detail POD WHERE POD.SysDocID = PRD.OrderSysDocID AND POD.VoucherID = PRD.OrderVoucherID  AND POD.ProductID = PRD.ProductID) AS OrderedQuantity,\r\n\t\t\t\t\t\tP.Attribute1,P.Attribute2,P.Attribute3,P.MatrixParentID, PRD.UnitPrice AS UnitPrice,\r\n                        ISNULL(PRD.UnitQuantity,PRD.Quantity)*ISNULL(PRD.UnitPrice,0) AS Total,PRD.UnitID, P.BrandID, PRD.RowIndex\r\n                        FROM   W3PL_GRN_Detail PRD\r\n                        INNER JOIN Product P ON P.ProductID=PRD.ProductID\r\n                        WHERE PRD.SysDocID='" + sysDocID + "' AND PRD.VoucherID IN (" + text + ")";
				FillDataSet(dataSet, "W3PL_GRN_Detail", cmdText);
				dataSet.Relations.Add("GRN", new DataColumn[2]
				{
					dataSet.Tables["W3PL_GRN"].Columns["SysDocID"],
					dataSet.Tables["W3PL_GRN"].Columns["VoucherID"]
				}, new DataColumn[2]
				{
					dataSet.Tables["W3PL_GRN_Detail"].Columns["SysDocID"],
					dataSet.Tables["W3PL_GRN_Detail"].Columns["VoucherID"]
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
			string text3 = "SELECT   CO.SysDocID [Doc ID], CO.VoucherID [VoucherID],CO.CustomerID [Customer Code], V.CustomerName [Customer Name], CO.ContainerNo [Container #], TransactionDate [Date]\r\n                            FROM W3PL_GRN CO                           \r\n                            INNER JOIN W3PL_GRN_Detail COD ON CO.SysDocID=COD.SysDocID AND CO.VoucherID=COD.VoucherID\r\n                            Inner JOIN Customer V ON V.CustomerID=CO.CustomerID ";
			if (from != DateTime.MinValue)
			{
				text3 = text3 + " WHERE TransactionDate Between '" + text + "' AND '" + text2 + "'";
			}
			if (!showVoid)
			{
				text3 += " AND ISNULL(IsVoid,'False')='False'";
			}
			text3 += " GROUP BY IsVoid,CO.SysDocID,CO.VoucherID,CO.CustomerID ,V.CustomerName,CO.ContainerNo,TransactionDate";
			SqlCommand sqlCommand = new SqlCommand(text3);
			FillDataSet(dataSet, "W3PL_GRN", sqlCommand);
			return dataSet;
		}

		public DataSet GetConsignmentsByStatus(string vendorID, W3PLGRNStatusEnum status)
		{
			try
			{
				DataSet dataSet = new DataSet();
				string str = "SELECT CO.SysDocID [Doc ID], CO.VoucherID [Number],CO.CustomerID [Customer Code], CUS.CustomerName [Customer Name], CO.ContainerNo [Container #], TransactionDate [Date]\r\n                            FROM W3PL_GRN CO INNER JOIN W3PL_GRN_Detail COD\r\n                            ON CO.SysDocID=COD.SysDocID AND CO.VoucherID=COD.VoucherID\r\n                            INNER JOIN Customer CUS ON CO.CustomerID=CUS.CustomerID  WHERE ISNULL(Status,1) =   " + (byte)status;
				if (vendorID != "")
				{
					str = str + " AND CO.CustomerID='" + vendorID + "'";
				}
				str += " GROUP BY CO.SysDocID, CO.VoucherID,CO.CustomerID,TransactionDate, CUS.CustomerName, CO.ContainerNo";
				SqlCommand sqlCommand = new SqlCommand(str);
				FillDataSet(dataSet, "W3PL_GRN", sqlCommand);
				return dataSet;
			}
			catch
			{
				throw;
			}
		}

		internal bool CloseOpenConsignment(string sysDocID, string voucherID, SqlTransaction sqlTransaction)
		{
			try
			{
				string exp = "SELECT SUM((CASE WHEN UnitQuantity IS NULL \r\n                                THEN Quantity ELSE UnitQuantity END))- SUM(ISNULL(QuantitySettled,0) ) - SUM(ISNULL(QuantityReturned,0) )\r\n\t                             FROM W3PL_GRN_Detail SOD WHERE SysDocID='" + sysDocID + "' AND VoucherID='" + voucherID + "'";
				object obj = ExecuteScalar(exp, sqlTransaction);
				if (obj == null)
				{
					return true;
				}
				exp = ((!(decimal.Parse(obj.ToString()) <= 0m)) ? ("UPDATE W3PL_GRN SET IsClosed = 'False' WHERE SysDocID='" + sysDocID + "' AND VoucherID='" + voucherID + "'") : ("UPDATE W3PL_GRN SET IsClosed = 'True' WHERE SysDocID='" + sysDocID + "' AND VoucherID='" + voucherID + "'"));
				return ExecuteNonQuery(exp, sqlTransaction) > 0;
			}
			catch
			{
				return false;
			}
		}

		internal bool SetStatus(string sysDocID, string voucherID, W3PLGRNStatusEnum status, SqlTransaction sqlTransaction)
		{
			try
			{
				string exp = "SELECT SUM(Quantity)- SUM(ISNULL(QuantitySettled,0) ) - SUM(ISNULL(QuantityReturned,0) )\r\n\t                             FROM W3PL_GRN_Detail SOD WHERE SysDocID='" + sysDocID + "' AND VoucherID='" + voucherID + "'";
				object obj = ExecuteScalar(exp, sqlTransaction);
				if (obj == null)
				{
					return true;
				}
				exp = ((!(decimal.Parse(obj.ToString()) <= 0m)) ? ("UPDATE W3PL_GRN SET Status = " + (byte)1 + " WHERE SysDocID= '" + sysDocID + "' AND VoucherID='" + voucherID + "'") : ("UPDATE W3PL_GRN SET Status = " + (byte)2 + " WHERE SysDocID= '" + sysDocID + "' AND VoucherID='" + voucherID + "'"));
				return ExecuteNonQuery(exp, sqlTransaction) > 0;
			}
			catch
			{
				return false;
			}
		}

		public DataSet GetBillableItems(string consignSysDocID, string consignVoucherID)
		{
			DataSet dataSet = new DataSet();
			string text = "";
			text = text + "SELECT 5 AS RowType, SysDocID,VoucherID,JournalDetailID AS RowIndex,AccountID,Description,ConsignID,ConsignExpenseID,Debit AS Amount FROM Journal_Details\r\n                        WHERE ISNULL(IsBilled,'False')='False' AND Debit IS NOT NULL AND ConsignID = '" + consignSysDocID + "#" + consignVoucherID + "' ";
			FillDataSet(dataSet, "Consign", text);
			return dataSet;
		}

		public DataSet GetPendingConsignmentsReport(string fromCustomer, string toCustomer)
		{
			try
			{
				if (fromCustomer != "" || toCustomer != "")
				{
					_ = " AND CustomerID BETWEEN '" + fromCustomer + "' AND '" + toCustomer + "' ";
				}
				string text = "SELECT CI.SysDocID,CI.VoucherID,CI.CustomerID,CustomerName,TransactionDate\r\n                                    FROM W3PL_GRN CI INNER JOIN W3PL_GRN_Detail CID\r\n                                    ON CI.SysDocID = CID.SysDocID AND CI.VoucherID = CID.VoucherID \r\n                                    INNER JOIN Customer V ON V.CustomerID = CI.CustomerID\r\n                                    WHERE ISNULL(IsClosed,'False') = 'False' ";
				if (fromCustomer != "")
				{
					text = text + " AND CI.CustomerID BETWEEN '" + fromCustomer + "' AND '" + toCustomer + "' ";
				}
				text += " ORDER BY TransactionDate ";
				DataSet dataSet = new DataSet();
				FillDataSet(dataSet, "W3PL_GRN", text);
				DataSet dataSet2 = new DataSet();
				text = "SELECT CiD.SysDocID,CID.VoucherID,CID.Description,ProductID,ISNULL(Quantity,0) AS Quantity,ISNULL(QuantitySettled,0) AS QuantitySettled,ISNULL(QuantityReturned,0) AS QuantityReturned,\r\n                            ISNULL(QuantitySold,0) AS QuantitySold,Quantity-ISNULL(QuantitySettled,0)-ISNULL(QuantityReturned,0)-ISNULL(QuantitySold,0) AS BalanceQty\r\n                            FROM W3PL_GRN CI INNER JOIN W3PL_GRN_Detail CID\r\n                            ON CI.SysDocID = CID.SysDocID AND CI.VoucherID = CID.VoucherID \r\n                            WHERE ISNULL(IsClosed,'False') = 'False' ";
				if (fromCustomer != "")
				{
					text = text + " AND CI.CustomerID BETWEEN '" + fromCustomer + "' AND '" + toCustomer + "' ";
				}
				text += " ORDER BY TransactionDate";
				FillDataSet(dataSet2, "W3PL_GRN_Detail", text);
				dataSet.Merge(dataSet2);
				dataSet.Relations.Add("Consign_REL", new DataColumn[2]
				{
					dataSet.Tables["W3PL_GRN"].Columns["SysDocID"],
					dataSet.Tables["W3PL_GRN"].Columns["VoucherID"]
				}, new DataColumn[2]
				{
					dataSet.Tables["W3PL_GRN_Detail"].Columns["SysDocID"],
					dataSet.Tables["W3PL_GRN_Detail"].Columns["VoucherID"]
				}, createConstraints: false);
				return dataSet;
			}
			catch
			{
				throw;
			}
		}

		public DataSet GetConsignmentInSettlementReport(string fromCustomer, string toCustomer)
		{
			try
			{
				if (fromCustomer != "" || toCustomer != "")
				{
					_ = " AND CustomerID BETWEEN '" + fromCustomer + "' AND '" + toCustomer + "' ";
				}
				string text = "SELECT CI.SysDocID,CI.VoucherID,CI.CustomerID,CustomerName,TransactionDate\r\n                                    FROM W3PL_GRN CI INNER JOIN W3PL_GRN_Detail CID\r\n                                    ON CI.SysDocID = CID.SysDocID AND CI.VoucherID = CID.VoucherID \r\n                                    INNER JOIN Customer V ON V.CustomerID = CI.CustomerID\r\n                                    WHERE ISNULL(IsClosed,'False') = 'False' ";
				if (fromCustomer != "")
				{
					text = text + " AND CI.CustomerID BETWEEN '" + fromCustomer + "' AND '" + toCustomer + "' ";
				}
				text += " ORDER BY TransactionDate ";
				DataSet dataSet = new DataSet();
				FillDataSet(dataSet, "W3PL_GRN", text);
				DataSet dataSet2 = new DataSet();
				text = "SELECT CiD.SysDocID,CID.VoucherID,CID.Description,ProductID,ISNULL(Quantity,0) AS Quantity,ISNULL(QuantitySettled,0) AS QuantitySettled,ISNULL(QuantityReturned,0) AS QuantityReturned,\r\n                            ISNULL(QuantitySold,0) AS QuantitySold,Quantity-ISNULL(QuantitySettled,0)-ISNULL(QuantityReturned,0)-ISNULL(QuantitySold,0) AS BalanceQty\r\n                            FROM W3PL_GRN CI INNER JOIN W3PL_GRN_Detail CID\r\n                            ON CI.SysDocID = CID.SysDocID AND CI.VoucherID = CID.VoucherID \r\n                            WHERE ISNULL(IsClosed,'False') = 'False' ";
				if (fromCustomer != "")
				{
					text = text + " AND CI.CustomerID BETWEEN '" + fromCustomer + "' AND '" + toCustomer + "' ";
				}
				text += " ORDER BY TransactionDate";
				FillDataSet(dataSet2, "W3PL_GRN_Detail", text);
				dataSet.Merge(dataSet2);
				dataSet.Relations.Add("Consign_REL", new DataColumn[2]
				{
					dataSet.Tables["W3PL_GRN"].Columns["SysDocID"],
					dataSet.Tables["W3PL_GRN"].Columns["VoucherID"]
				}, new DataColumn[2]
				{
					dataSet.Tables["W3PL_GRN_Detail"].Columns["SysDocID"],
					dataSet.Tables["W3PL_GRN_Detail"].Columns["VoucherID"]
				}, createConstraints: false);
				return dataSet;
			}
			catch
			{
				throw;
			}
		}

		public DataSet GetGRNComboList()
		{
			DataSet dataSet = new DataSet();
			string textCommand = "SELECT SysDocID + '#' + VoucherID AS  [Code], ContainerNo [Name]\r\n                           FROM W3PL_GRN WHERE ISNULL(IsClosed,'False')='False' ORDER BY SysDocID,VoucherID";
			FillDataSet(dataSet, "Vehicle", textCommand);
			return dataSet;
		}

		public DataSet GetGRNClosingSummary(string sysDocID, string voucherID)
		{
			return GetGRNClosingSummary(sysDocID, voucherID, null);
		}

		public DataSet GetGRNClosingSummary(string sysDocID, string voucherID, SqlTransaction sqlTransaction)
		{
			DataSet dataSet = new DataSet();
			string textCommand = "SELECT SysDocID,VoucherID,CID.ProductID, P.Description,CID.Quantity  AS QuantityReceived,ISNULL(QuantityReturned,0) AS QuantityReturned, QuantitySettled ,CID.Quantity-ISNULL(QuantityReturned,0) - ISNULL(QuantitySettled,0) AS BalanceQuantity,\r\n                                (SELECT SUM(SoldQty*UnitPrice) AS Amount FROM Product_Lot_Sales  PLS WHERE  CID.ProductID = PLS.ItemCode  AND LOTNO IN (SELECT LOTNumber FROM Product_Lot WHERE DocID = CID.SysDocID AND ReceiptNumber = CID.VoucherID)) AS ActualSales,\r\n\r\n                                (SELECT SUM(Amount) AS Amount FROM GRN_Settlement_Detail CSD WHERE CSD.ProductID = CID.ProductID AND CSD.ConsignRowIndex = CID.RowIndex AND CSD.ConsignSysDocID = CID.SysDocID AND CSD.ConsignVoucherID =CID.VoucherID) AS SettledSales \r\n                                 FROM W3PL_GRN_Detail CID INNER JOIN Product P ON CID.ProductID = P.ProductID WHERE SysDocID = '" + sysDocID + "' AND VoucherID = '" + voucherID + "'";
			FillDataSet(dataSet, "Items", textCommand, sqlTransaction);
			textCommand = "SELECT JD.JournalDetailID,JD.AccountID,ISNULL(Debit,0) AS Amount, (SELECT SUM(Amount) \r\n                        FROM GRN_Expense CX WHERE CX.SourceRowIndex = JD.JournalDetailID) AS Billed\r\n                        FROM Journal_Details JD WHERE JD.ConsignID = '" + sysDocID + "#" + voucherID + "' AND JD.DEBIT IS NOT NULL";
			FillDataSet(dataSet, "Bills", textCommand, sqlTransaction);
			textCommand = "SELECT CI.CustomerID,Ven.CustomerName,TransactionDate,Status ,\r\n                        (SELECT SUM(SoldQty*UnitPrice) AS Amount FROM Product_Lot_Sales  PLS WHERE  CID.ProductID = PLS.ItemCode  AND LOTNO IN (SELECT LOTNumber FROM Product_Lot WHERE DocID = CID.SysDocID AND ReceiptNumber = CID.VoucherID)) AS ActualSales,\r\n                        (SELECT SUM(Amount) AS Amount FROM GRN_Settlement_Detail CSD WHERE CSD.ProductID = CID.ProductID AND CSD.ConsignRowIndex = CID.RowIndex AND CSD.ConsignSysDocID = CID.SysDocID AND CSD.ConsignVoucherID =CID.VoucherID) AS SettledSales,\r\n                        (SELECT SUM(CommissionAmount) AS Commission FROM GRN_Settlement CIS WHERE CIS.ConsignSysDocID = CI.SysDocID AND CIS.ConsignVoucherID = CI.VoucherID) AS Commission,\r\n                        (SELECT SUM(Debit) AS Amount FROM Journal_Details JD WHERE Debit IS NOT NULL AND ConsignID = CI.SysDocID + '#' + CI.VoucherID  ) AS Expenses,\r\n                        (SELECT SUM(Amount) FROM GRN_Expense  CX WHERE CX.SysDocID = CI.SysDocID AnD CX.VoucherID = CI.VoucherID) AS Billed\r\n                           FROM W3PL_GRN CI INNER JOIN W3PL_GRN_Detail CID ON CI.SysDocID = CID.SysDocID AND CI.VoucherID = CID.VoucherID INNER JOIN\r\n\t                        Customer VEN ON  CI.CustomerID = VEN.CustomerID WHERE CI.Sysdocid = '" + sysDocID + "' AND CI.VoucherID = '" + voucherID + "'";
			FillDataSet(dataSet, "W3PL_GRN", textCommand, sqlTransaction);
			return dataSet;
		}

		public DataSet GetGRNReport(DateTime from, DateTime to, string fromCustomer, string toCustomer, string fromClass, string toClass, string fromGroup, string toGroup)
		{
			CommonLib.ToSqlDateTimeString(from);
			CommonLib.ToSqlDateTimeString(to);
			string empty = string.Empty;
			DataSet dataSet = new DataSet();
			empty = " SELECT CustomerName, VA.Address1  FROM W3PL_GRN CI\r\n\t                    INNER JOIN Customer V ON CI.CustomerID = V.CustomerID\r\n\t                    INNER JOIN Customer_Address VA ON V.CustomerID = VA.CustomerID \r\n                                WHERE 1=1 ";
			if (fromCustomer != "")
			{
				empty = empty + " AND CI.CustomerID >= '" + fromCustomer + "'";
			}
			if (toCustomer != "")
			{
				empty = empty + " AND CI.CustomerID <= '" + toCustomer + "'";
			}
			if (fromClass != "")
			{
				empty = empty + " AND CI.CustomerID IN (SELECT CustomerID FROM Customer WHERE CustomerClassID >= '" + fromClass + "') ";
			}
			if (toClass != "")
			{
				empty = empty + " AND CI.CustomerID IN (SELECT CustomerID FROM Customer WHERE CustomerClassID <= '" + toClass + "') ";
			}
			if (fromGroup != "")
			{
				empty = empty + " AND CI.CustomerID IN (SELECT CustomerID FROM Customer WHERE CustomerGroupID >= '" + fromGroup + "') ";
			}
			if (toGroup != "")
			{
				empty = empty + " AND CI.CustomerID IN (SELECT CustomerID FROM Customer WHERE CustomerGroupID <= '" + toGroup + "') ";
			}
			empty += " ORDER BY CI.CustomerID ";
			FillDataSet(dataSet, "Customer", empty);
			empty = " SELECT CE.Description,CS.Transactiondate,CE.VoucherID,CE.Amount \r\n                        FROM GRN_Expense CE, GRN_Settlement CS, W3PL_GRN CI\r\n                        AND  CE.VoucherID=CS.VoucherID and CS.ConsignVoucherID=CI.VoucherID WHERE 1=1 ";
			if (fromCustomer != "")
			{
				empty = empty + " AND CI.CustomerID >= '" + fromCustomer + "'";
			}
			if (toCustomer != "")
			{
				empty = empty + " AND CI.CustomerID <= '" + toCustomer + "'";
			}
			if (fromClass != "")
			{
				empty = empty + " AND CI.CustomerID IN (SELECT CustomerID FROM Customer WHERE CustomerClassID >= '" + fromClass + "') ";
			}
			if (toClass != "")
			{
				empty = empty + " AND CI.CustomerID IN (SELECT CustomerID FROM Customer WHERE CustomerClassID <= '" + toClass + "') ";
			}
			if (fromGroup != "")
			{
				empty = empty + " AND CI.CustomerID IN (SELECT CustomerID FROM Customer WHERE CustomerGroupID >= '" + fromGroup + "') ";
			}
			if (toGroup != "")
			{
				empty = empty + " AND CI.CustomerID IN (SELECT CustomerID FROM Customer WHERE CustomerGroupID <= '" + toGroup + "') ";
			}
			empty += " ORDER BY CI.CustomerID ";
			FillDataSet(dataSet, "W3PL_Expense", empty);
			empty = " SELECT Product_Lot.LotNumber,Product_Lot.ItemCode,Product_Lot.LotQty, SUM(Delivery_Note_Detail.Quantity) as [Quanity Delivered], SUM(Sales_Invoice_Detail.FOCQuantity) as FOC,\r\n                        SUM((Sales_Invoice_Detail.Quantity - Sales_Invoice_Detail.FOCQuantity)) As [Quantity Invoiced] ,\r\n                        SUM(((Sales_Invoice_Detail.Quantity - Sales_Invoice_Detail.FOCQuantity)*(Sales_Invoice_Detail.UnitPrice)))\r\n                     FROM Product_Lot_Sales\r\n                     LEFT JOIN Delivery_Note_Detail on Product_Lot_Sales.InvoiceNumber=Delivery_Note_Detail.VoucherID and Product_Lot_Sales.DocID=Delivery_Note_Detail.SysDocID\r\n                     LEFT JOIN Sales_Invoice_Detail on Delivery_Note_Detail.VoucherID=Sales_Invoice_Detail.OrderVoucherID and Delivery_Note_Detail.SysDocID=Sales_Invoice_Detail.OrderSysDocID\r\n                    LEFT JOIN  Product_Lot on Product_Lot_Sales.LotNo=Product_Lot.LotNumber  WHERE 1=1";
			if (fromCustomer != "")
			{
				empty = empty + " AND CI.CustomerID >= '" + fromCustomer + "'";
			}
			if (toCustomer != "")
			{
				empty = empty + " AND CI.CustomerID <= '" + toCustomer + "'";
			}
			if (fromClass != "")
			{
				empty = empty + " AND CI.CustomerID IN (SELECT CustomerID FROM Customer WHERE CustomerClassID >= '" + fromClass + "') ";
			}
			if (toClass != "")
			{
				empty = empty + " AND CI.CustomerID IN (SELECT CustomerID FROM Customer WHERE CustomerClassID <= '" + toClass + "') ";
			}
			if (fromGroup != "")
			{
				empty = empty + " AND CI.CustomerID IN (SELECT CustomerID FROM Customer WHERE CustomerGroupID >= '" + fromGroup + "') ";
			}
			if (toGroup != "")
			{
				empty = empty + " AND CI.CustomerID IN (SELECT CustomerID FROM Customer WHERE CustomerGroupID <= '" + toGroup + "') ";
			}
			empty += " GROUP BY  Product_Lot.LotNumber,Product_Lot.ItemCode,Product_Lot.LotQty ";
			empty += " ORDER BY CI.CustomerID ";
			FillDataSet(dataSet, "W3PL_GRN", empty);
			empty = " SELECT Delivery_Note.TransactionDate,Delivery_Note.VoucherID,W3PL_GRN.CustomerID,Sales_Invoice.VoucherID AS InvoiceVoucherID,\r\n                        Sales_Invoice.TransactionDate,Sales_Invoice.SysDocID,Customer.CustomerName, Delivery_Note_Detail.Quantity AS [Quanity Delivered], Sales_Invoice_Detail.FOCQuantity AS FOC,\r\n                        (Sales_Invoice_Detail.Quantity - Sales_Invoice_Detail.FOCQuantity) AS [Quantity Invoiced] ,(Sales_Invoice_Detail.UnitPrice) AS [Unit Price],\r\n                        ((Sales_Invoice_Detail.Quantity - Sales_Invoice_Detail.FOCQuantity)*(Sales_Invoice_Detail.UnitPrice)) AS [ Amount ] FROM Product_Lot_Sales \r\n                        LEFT JOIN Delivery_Note_Detail ON Product_Lot_Sales.InvoiceNumber=Delivery_Note_Detail.VoucherID and Product_Lot_Sales.DocID=Delivery_Note_Detail.SysDocID\r\n                    LEFT JOIN Sales_Invoice_Detail ON Delivery_Note_Detail.VoucherID=Sales_Invoice_Detail.OrderVoucherID and Delivery_Note_Detail.SysDocID=Sales_Invoice_Detail.OrderSysDocID\r\n                    LEFT JOIN  Product_Lot ON Product_Lot_Sales.LotNo=Product_Lot.LotNumber \r\n                    LEFT JOIN Delivery_Note ON Delivery_Note.VoucherID=Delivery_Note_Detail.VoucherID and Delivery_Note.SysDocID=Delivery_Note_Detail.SysDocID\r\n                    LEFT JOIN Sales_Invoice ON Sales_Invoice.VoucherID=Sales_Invoice_Detail.VoucherID and Sales_Invoice.SysDocID=Sales_Invoice_Detail.SysDocID\r\n                    LEFT JOIN Customer ON Customer.CustomerID=Sales_Invoice.CustomerID\r\n                    LEFT JOIN W3PL_GRN ON W3PL_GRN.VoucherID= Product_Lot.ReceiptNumber AND W3PL_GRN.SysDocID = Product_Lot.DocID    WHERE 1=1";
			if (fromCustomer != "")
			{
				empty = empty + " AND CI.CustomerID >= '" + fromCustomer + "'";
			}
			if (toCustomer != "")
			{
				empty = empty + " AND CI.CustomerID <= '" + toCustomer + "'";
			}
			if (fromClass != "")
			{
				empty = empty + " AND CI.CustomerID IN (SELECT CustomerID FROM Customer WHERE CustomerClassID >= '" + fromClass + "') ";
			}
			if (toClass != "")
			{
				empty = empty + " AND CI.CustomerID IN (SELECT CustomerID FROM Customer WHERE CustomerClassID <= '" + toClass + "') ";
			}
			if (fromGroup != "")
			{
				empty = empty + " AND CI.CustomerID IN (SELECT CustomerID FROM Customer WHERE CustomerGroupID >= '" + fromGroup + "') ";
			}
			if (toGroup != "")
			{
				empty = empty + " AND CI.CustomerID IN (SELECT CustomerID FROM Customer WHERE CustomerGroupID <= '" + toGroup + "') ";
			}
			empty += " GROUP BY  Product_Lot.LotNumber,Product_Lot.ItemCode,Product_Lot.LotQty ";
			empty += " ORDER BY CI.CustomerID ";
			FillDataSet(dataSet, "W3PL_GRN_Detail", empty);
			return dataSet;
		}

		public DataSet GetGRNList(string sysDocID, DateTime from, DateTime to)
		{
			string text = CommonLib.ToSqlDateTimeString(from);
			string text2 = CommonLib.ToSqlDateTimeString(to);
			DataSet dataSet = new DataSet();
			string str = "SELECT SysDocID [Doc ID], VoucherID [Number], TransactionDate AS [Date],Ven.CustomerName AS [Customer],Con.ContainerNo AS [Container#]\r\n                            FROM W3PL_GRN CON INNER JOIN Customer VEN ON Con.CustomerID = Ven.CustomerID\r\n                            WHERE ISNULL(IsVoid,'False')='False'  AND TransactionDate BETWEEN '" + text + "' AND '" + text2 + "' ";
			if (!string.IsNullOrEmpty(sysDocID))
			{
				str = str + " AND SysDocID='" + sysDocID + "'";
			}
			str += " ORDER BY TransactionDate, VoucherID ";
			FillDataSet(dataSet, "W3PL_GRN", str);
			return dataSet;
		}

		public DataSet GetGRNItemMovementReport(DateTime from, DateTime to, string fromItem, string toItem, string fromItemClass, string toItemClass, string fromCategory, string toCategory, string fromLocationID, string toLocationID, string sysDocID, string voucherID)
		{
			try
			{
				CommonLib.ToSqlDateTimeString(from);
				CommonLib.ToSqlDateTimeString(to);
				DataSet dataSet = new DataSet();
				string textCommand = "SELECT CI.*,V.CustomerName,T.TransporterName,P.PortName FROM W3PL_GRN CI \r\n                                LEFT JOIN Customer V ON CI.CustomerID=V.CustomerID\r\n                                LEFT JOIN Transporter T ON CI.TransporterID=T.TransporterID\r\n                                LEFT JOIN Port P ON CI.ArrivalPort=P.PortID\r\n                                WHERE VoucherID='" + voucherID + "' AND SysDocID='" + sysDocID + "'";
				FillDataSet(dataSet, "W3PL_GRN", textCommand);
				textCommand = "SELECT '" + sysDocID + "' AS DocID, '" + voucherID + "' AS ReceiptNumber, CON.CustomerID,Con.CustomerName,CON.Description,COn.DocName,\r\n                       COn.LocationID, CON.ProductID AS ItemCode,CON.SysDocID,CON.SysDocType,CON.TransactionDate,CON.UnitPrice AS [Unit Price], CON.QuantityIn,CON.QuantityOut,\r\n                        CON.VoucherID FROM \r\n\r\n\r\n                        (SELECT PLD.*,P.Description, IT.TransactionDate,CASE WHEN IT.PayeeType = 'C' THEN IT.PayeeID ELSE NULL END AS CustomerID ,\r\n                        CUS.CustomerName, \r\n                         CASE WHEN INV.TransactionDate IS NULL THEN 0 ELSE INV.UnitPrice END AS UnitPrice,\r\n                       \r\n                         ISNULL(QtyIn,0) AS QuantityIn, ISNULL(QtyOut,0) AS QuantityOut\r\n                          FROM ( \r\n  \r\n  \r\n                          SELECT SD.SysDocType,SD.DocName, PLS.DocID AS SysDocID, InvoiceNumber AS VoucherID,RowIndex,ItemCode AS ProductID,PLS.LocationID,LotNo AS LotNumber, 0 AS QtyIn, SoldQty AS QtyOut\r\n                          FROM Product_Lot_Sales PLS INNER JOIN System_Document SD ON PLS.DocID = SD.SysDocID WHERE LotNo IN \r\n                          (SELECT LotNumber FROM Product_Lot PL WHERE (ReceiptNumber = '" + voucherID + "' AND DocID = '" + sysDocID + "') OR SourceLotNumber IN \r\n                          (SELECT PL.LotNumber FROM Product_Lot PL WHERE (ReceiptNumber = '" + voucherID + "' AND DocID = '" + sysDocID + "')))\r\n                         \r\n                         UNION\r\n\t\t                   SELECT SD.SysDocType,SD.DocName,PLS.SysDocID, VoucherID,RowIndex, ProductID,PLS.LocationID,SourceLotNumber AS  LotNumber,LotQty AS QtyIN,0 AS QtyOut\r\n\t\t                    FROM Product_Lot_Receiving_Detail PLS INNER JOIN System_Document SD ON PLS.SysDocID = SD.SysDocID WHERE SourceLotNumber IN \r\n\t\t\t                  (SELECT LotNumber FROM Product_Lot PL WHERE (ReceiptNumber = '" + voucherID + "' AND DocID = '" + sysDocID + "')\r\n\t\t\t                   OR SourceLotNumber IN (SELECT PL.LotNumber FROM Product_Lot PL WHERE (ReceiptNumber = '" + voucherID + "' AND DocID = '" + sysDocID + "')))\r\n\r\n\t\t\t\t\t\t\t   UNION \r\n\t\t\t\t\t\t\t   SELECT SD.SysDocType,SD.DocName,PL.DocID as sysdocid, ReceiptNumber as voucherid,RowIndex, itemcode as ProductID,PL.LocationID,SourceLotNumber AS  LotNumber,LotQty AS QtyIN,0 AS QtyOut FROM Product_Lot PL\r\n\t\t\t\t\t\t\t   INNER JOIN System_Document SD ON PL.DocID = SD.SysDocID WHERE ReceiptNumber = '" + voucherID + "' AND DocID = '" + sysDocID + "'\r\n\r\n\t\t\t                   )  AS PLD\r\n\r\n\t\t\t                    LEFT OUTER JOIN Inventory_Transactions IT ON IT.SysDocID = PLD.SysDocID AND IT.VoucherID = PLD.VoucherID AND IT.RowIndex = PLD.RowIndex AND IT.LocationID = PLD.LocationID\r\n\t\t\t\t                 LEFT OUTER JOIN  (SELECT SID2.Quantity,SID2.OrderSysDocID,SID2.VoucherID,SID2.OrderVoucherID,SID2.OrderRowIndex,SID2.UnitPrice,SI2.TransactionDate\r\n\t\t\t\t\t\t\t\t FROM Sales_Invoice_Detail SID2 INNER JOIN Sales_INVOICE SI2 ON SID2.SysDocID = SI2.SysDocID AND SID2.VoucherID = SI2.VoucherID AND ISNULL(SI2.IsVoid,'False') = 'False' ) AS INV ON INV.OrderSysDocID = PLD.SysDocID\r\n\t\t\t\t\t\t\t\t AND INV.OrderVoucherID = PLD.VoucherID AND INV.OrderRowIndex = PLD.RowIndex\r\n\t\t\t\t                    LEFT OUTER JOIN Delivery_Return DR ON DR.DNoteSysDocID = PLD.SysDocID AND DR.DNoteVoucherID = PLD.VoucherID  AND ISNULL(DR.IsVoid,'False') = 'False'\r\n\t\t\t\t\t                 LEFT OUTER JOIN Delivery_Return_Detail DRD ON DRD.SysDocID = DR.SysDocID AND DRD.VoucherID = DR.VoucherID AND DRD.DNRowIndex = PLD.RowIndex\r\n\t\t\t\t\t                  LEFT OUTER JOIN Sales_Return_Detail  SRD ON SRD.SourceSysDocID = PLD.SysDocID AND SRD.SourceVoucherID = PLD.VoucherID AND SRD.SourceRowIndex = PLD.RowIndex   \r\n\t\t\t\t\t                   LEFT OUTER JOIN Product P ON P.ProductID = PLD.ProductID\r\n\t\t\t\t\t                      LEFT OUTER JOIN Customer CUS ON CUS.CUstomerID = IT.PayeeID AND IT.PayeeType = 'C') CON\r\n\t\t\t\t\t\t                   WHERE 1=1 ";
				if (fromItem != "")
				{
					textCommand = textCommand + " AND CON.ProductID >= '" + fromItem + "' ";
				}
				if (toItem != "")
				{
					textCommand = textCommand + " AND CON.ProductID <= '" + toItem + "' ";
				}
				if (fromItemClass != "")
				{
					textCommand = textCommand + " AND CON.ProductID IN (SELECT ProductID FROM Product WHERE ClassID >= '" + fromItemClass + "')";
				}
				if (toItemClass != "")
				{
					textCommand = textCommand + " AND CON.ProductID IN (SELECT ProductID FROM Product WHERE ClassID <= '" + toItemClass + "')";
				}
				if (fromCategory != "")
				{
					textCommand = textCommand + " AND CON.ProductID IN (SELECT ProductID FROM Product WHERE CategoryID >= '" + fromCategory + "')";
				}
				if (toCategory != "")
				{
					textCommand = textCommand + " AND CON.ProductID IN (SELECT ProductID FROM Product WHERE CategoryID <= '" + toCategory + "') ";
				}
				if (fromLocationID != "")
				{
					textCommand = textCommand + " AND CON.LocationID >= '" + fromLocationID + "' ";
				}
				if (toLocationID != "")
				{
					textCommand = textCommand + " AND CON.LocationID <= '" + toLocationID + "' ";
				}
				textCommand += " ORDER BY ItemCode,TransactionDate   ";
				FillDataSet(dataSet, "W3PL_GRN_Detail", textCommand);
				dataSet.Relations.Add("Relation", new DataColumn[2]
				{
					dataSet.Tables["W3PL_GRN"].Columns["SysDocID"],
					dataSet.Tables["W3PL_GRN"].Columns["VoucherID"]
				}, new DataColumn[2]
				{
					dataSet.Tables["W3PL_GRN_Detail"].Columns["DocID"],
					dataSet.Tables["W3PL_GRN_Detail"].Columns["ReceiptNumber"]
				}, createConstraints: false);
				return dataSet;
			}
			catch
			{
				throw;
			}
		}
	}
}
