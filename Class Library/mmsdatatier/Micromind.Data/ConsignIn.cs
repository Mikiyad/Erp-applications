using Micromind.Common;
using Micromind.Common.Data;
using Micromind.Common.Libraries;
using System;
using System.Collections;
using System.Data;
using System.Data.SqlClient;

namespace Micromind.Data
{
	public sealed class ConsignIn : StoreObject
	{
		private const string CONSIGNIN_TABLE = "Consign_In";

		private const string CONSIGNINDETAIL_TABLE = "Consign_In_Detail";

		private const string SYSDOCID_PARM = "@SysDocID";

		private const string VOUCHERID_PARM = "@VoucherID";

		private const string COMPANYID_PARM = "@CompanyID";

		private const string DIVISIONID_PARM = "@DivisionID";

		private const string VENDORID_PARM = "@VendorID";

		private const string TRANSACTIONDATE_PARM = "@TransactionDate";

		private const string SALESPERSONID_PARM = "@SalespersonID";

		private const string ROWINDEX_PARM = "@RowIndex";

		private const string REQUIREDDATE_PARM = "@RequiredDate";

		private const string SHIPPINGADDRESSID_PARM = "@ShippingAddressID";

		private const string VENDORADDRESS_PARM = "@VendorAddress";

		private const string STATUS_PARM = "@Status";

		private const string CURRENCYID_PARM = "@CurrencyID";

		private const string TERMID_PARM = "@TermID";

		private const string SHIPPINGMETHODID_PARM = "@ShippingMethodID";

		private const string REFERENCE_PARM = "@Reference";

		private const string NOTE_PARM = "@Note";

		private const string ISVOID_PARM = "@IsVoid";

		private const string PONUMBER_PARM = "@PONumber";

		private const string DISCOUNT_PARM = "@Discount";

		private const string TOTAL_PARM = "@Total";

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

		private const string TARGETPRICE_PARM = "@TargetPrice";

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

		public ConsignIn(Config config)
			: base(config)
		{
		}

		private string GetInsertUpdateConsignInText(bool isUpdate)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddInsertUpdateParameters("Consign_In", new FieldValue("SysDocID", "@SysDocID", isUpdateConditionField: true), new FieldValue("VoucherID", "@VoucherID", isUpdateConditionField: true), new FieldValue("DivisionID", "@DivisionID"), new FieldValue("CompanyID", "@CompanyID"), new FieldValue("VendorID", "@VendorID"), new FieldValue("TransactionDate", "@TransactionDate"), new FieldValue("SalespersonID", "@SalespersonID"), new FieldValue("RequiredDate", "@RequiredDate"), new FieldValue("ShippingAddressID", "@ShippingAddressID"), new FieldValue("ShippingMethodID", "@ShippingMethodID"), new FieldValue("VendorAddress", "@VendorAddress"), new FieldValue("Status", "@Status"), new FieldValue("CurrencyID", "@CurrencyID"), new FieldValue("Discount", "@Discount"), new FieldValue("Total", "@Total"), new FieldValue("PONumber", "@PONumber"), new FieldValue("TermID", "@TermID"), new FieldValue("Reference", "@Reference"), new FieldValue("TransporterID", "@TransporterID"), new FieldValue("ArrivalPort", "@ArrivalPortID"), new FieldValue("ArrivalDate", "@ArrivalDate"), new FieldValue("ContainerNo", "@ContainerNo"), new FieldValue("BLNo", "@BLNo"), new FieldValue("Note", "@Note"));
			if (isUpdate)
			{
				sqlBuilder.AddInsertUpdateParameters("Consign_In", new FieldValue("DateUpdated", "@DateUpdated", isUpdateConditionField: true, checkForNullValue: true));
			}
			if (isUpdate)
			{
				return sqlBuilder.GetUpdateExpression();
			}
			return sqlBuilder.GetInsertExpression();
		}

		private SqlCommand GetInsertUpdateConsignInCommand(bool isUpdate)
		{
			SqlParameterCollection parameters;
			if (isUpdate)
			{
				if (updateCommand != null)
				{
					updateCommand = null;
				}
				updateCommand = new SqlCommand(GetInsertUpdateConsignInText(isUpdate: true), base.DBConfig.Connection);
				updateCommand.CommandType = CommandType.Text;
				parameters = updateCommand.Parameters;
			}
			else
			{
				if (insertCommand != null)
				{
					insertCommand = null;
				}
				insertCommand = new SqlCommand(GetInsertUpdateConsignInText(isUpdate: false), base.DBConfig.Connection);
				insertCommand.CommandType = CommandType.Text;
				parameters = insertCommand.Parameters;
			}
			parameters.Add("@SysDocID", SqlDbType.NVarChar);
			parameters.Add("@VoucherID", SqlDbType.NVarChar);
			parameters.Add("@DivisionID", SqlDbType.NVarChar);
			parameters.Add("@CompanyID", SqlDbType.TinyInt);
			parameters.Add("@VendorID", SqlDbType.NVarChar);
			parameters.Add("@TransactionDate", SqlDbType.DateTime);
			parameters.Add("@SalespersonID", SqlDbType.NVarChar);
			parameters.Add("@RequiredDate", SqlDbType.DateTime);
			parameters.Add("@ShippingAddressID", SqlDbType.NVarChar);
			parameters.Add("@VendorAddress", SqlDbType.NVarChar);
			parameters.Add("@ShippingMethodID", SqlDbType.NVarChar);
			parameters.Add("@Status", SqlDbType.TinyInt);
			parameters.Add("@CurrencyID", SqlDbType.NVarChar);
			parameters.Add("@TermID", SqlDbType.NVarChar);
			parameters.Add("@Reference", SqlDbType.NVarChar);
			parameters.Add("@PONumber", SqlDbType.NVarChar);
			parameters.Add("@Discount", SqlDbType.Decimal);
			parameters.Add("@Total", SqlDbType.Decimal);
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
			parameters["@VendorID"].SourceColumn = "VendorID";
			parameters["@TransactionDate"].SourceColumn = "TransactionDate";
			parameters["@SalespersonID"].SourceColumn = "SalespersonID";
			parameters["@RequiredDate"].SourceColumn = "RequiredDate";
			parameters["@ShippingAddressID"].SourceColumn = "ShippingAddressID";
			parameters["@VendorAddress"].SourceColumn = "VendorAddress";
			parameters["@ShippingMethodID"].SourceColumn = "ShippingMethodID";
			parameters["@Status"].SourceColumn = "Status";
			parameters["@CurrencyID"].SourceColumn = "CurrencyID";
			parameters["@TermID"].SourceColumn = "TermID";
			parameters["@Reference"].SourceColumn = "Reference";
			parameters["@PONumber"].SourceColumn = "PONumber";
			parameters["@Discount"].SourceColumn = "Discount";
			parameters["@Total"].SourceColumn = "Total";
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

		private string GetConsignInCloseText(bool isUpdate)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddInsertUpdateParameters("Consign_In", new FieldValue("SysDocID", "@SysDocID", isUpdateConditionField: true), new FieldValue("VoucherID", "@VoucherID", isUpdateConditionField: true), new FieldValue("CloseDate", "@CloseDate"), new FieldValue("CloseNote", "@CloseNote"), new FieldValue("Status", "@Status"));
			if (isUpdate)
			{
				return sqlBuilder.GetUpdateExpression();
			}
			return sqlBuilder.GetInsertExpression();
		}

		private SqlCommand GetConsignInCloseCommand()
		{
			if (updateCommand != null)
			{
				updateCommand = null;
			}
			updateCommand = new SqlCommand(GetConsignInCloseText(isUpdate: true), base.DBConfig.Connection);
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

		private string GetInsertUpdateConsignInDetailsText(bool isUpdate)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddInsertUpdateParameters("Consign_In_Detail", new FieldValue("SysDocID", "@SysDocID"), new FieldValue("VoucherID", "@VoucherID"), new FieldValue("ProductID", "@ProductID"), new FieldValue("LocationID", "@LocationID"), new FieldValue("ConsignLocationID", "@ConsignLocationID"), new FieldValue("RowIndex", "@RowIndex"), new FieldValue("Quantity", "@Quantity"), new FieldValue("UnitPrice", "@UnitPrice"), new FieldValue("Description", "@Description"), new FieldValue("UnitID", "@UnitID"), new FieldValue("UnitQuantity", "@UnitQuantity"), new FieldValue("UnitFactor", "@UnitFactor"), new FieldValue("FactorType", "@FactorType"), new FieldValue("SubunitPrice", "@SubunitPrice"), new FieldValue("TargetPrice", "@TargetPrice"), new FieldValue("OrderSysDocID", "@OrderSysDocID"), new FieldValue("OrderVoucherID", "@OrderVoucherID"), new FieldValue("InvoiceRowIndex", "@OrderRowIndex"));
			if (isUpdate)
			{
				return sqlBuilder.GetUpdateExpression();
			}
			return sqlBuilder.GetInsertExpression();
		}

		private SqlCommand GetInsertUpdateConsignInDetailsCommand(bool isUpdate)
		{
			SqlParameterCollection parameters;
			if (isUpdate)
			{
				if (updateCommand != null)
				{
					updateCommand = null;
				}
				updateCommand = new SqlCommand(GetInsertUpdateConsignInDetailsText(isUpdate: true), base.DBConfig.Connection);
				updateCommand.CommandType = CommandType.Text;
				parameters = updateCommand.Parameters;
			}
			else
			{
				if (insertCommand != null)
				{
					insertCommand = null;
				}
				insertCommand = new SqlCommand(GetInsertUpdateConsignInDetailsText(isUpdate: false), base.DBConfig.Connection);
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
			parameters.Add("@TargetPrice", SqlDbType.Decimal);
			parameters["@SysDocID"].SourceColumn = "SysDocID";
			parameters["@VoucherID"].SourceColumn = "VoucherID";
			parameters["@ProductID"].SourceColumn = "ProductID";
			parameters["@LocationID"].SourceColumn = "LocationID";
			parameters["@ConsignLocationID"].SourceColumn = "ConsignLocationID";
			parameters["@RowIndex"].SourceColumn = "RowIndex";
			parameters["@Quantity"].SourceColumn = "Quantity";
			parameters["@UnitPrice"].SourceColumn = "UnitPrice";
			parameters["@Description"].SourceColumn = "Description";
			parameters["@UnitID"].SourceColumn = "UnitID";
			parameters["@UnitQuantity"].SourceColumn = "UnitQuantity";
			parameters["@UnitFactor"].SourceColumn = "UnitFactor";
			parameters["@FactorType"].SourceColumn = "FactorType";
			parameters["@SubunitPrice"].SourceColumn = "SubunitPrice";
			parameters["@TargetPrice"].SourceColumn = "TargetPrice";
			parameters["@OrderVoucherID"].SourceColumn = "OrderVoucherID";
			parameters["@OrderSysDocID"].SourceColumn = "OrderSysDocID";
			parameters["@OrderRowIndex"].SourceColumn = "InvoiceRowIndex";
			if (isUpdate)
			{
				return updateCommand;
			}
			return insertCommand;
		}

		private bool ValidateData(ConsignInData journalData)
		{
			return true;
		}

		public bool InsertUpdateConsignIn(ConsignInData consignInData, bool isUpdate)
		{
			bool flag = true;
			SqlCommand insertUpdateConsignInCommand = GetInsertUpdateConsignInCommand(isUpdate);
			try
			{
				DataRow dataRow = consignInData.ConsignInTable.Rows[0];
				SqlTransaction sqlTransaction = base.DBConfig.StartNewTransaction();
				string text = dataRow["VoucherID"].ToString();
				string text2 = dataRow["SysDocID"].ToString();
				if (!isUpdate && new SystemDocuments(base.DBConfig).ExistDocumentNumber("Consign_In", "VoucherID", dataRow["SysDocID"].ToString(), text, sqlTransaction))
				{
					base.DBConfig.EndTransaction(result: false);
					throw new CompanyException("Document number already exist.", 1046);
				}
				bool flag2 = new Products(base.DBConfig).HasInUseLots(text2, text);
				foreach (DataRow row in consignInData.ConsignInDetailTable.Rows)
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
				insertUpdateConsignInCommand.Transaction = sqlTransaction;
				flag = (isUpdate ? (flag & Update(consignInData, "Consign_In", insertUpdateConsignInCommand)) : (flag & Insert(consignInData, "Consign_In", insertUpdateConsignInCommand)));
				if (isUpdate && flag2)
				{
					string textCommand = "SELECT ProductID,Quantity,RowIndex,LocationID FROM Consign_In_Detail WHERE SysDocID = '" + text2 + "' AND VoucherID = '" + text + "'";
					DataSet dataSet = new DataSet();
					FillDataSet(dataSet, "Consign_In_Detail", textCommand, sqlTransaction);
					DataTable dataTable = dataSet.Tables["Consign_In_Detail"];
					ConsignInData consignInData2 = new ConsignInData();
					foreach (DataRow row2 in consignInData.ConsignInDetailTable.Rows)
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
							DataRow dataRow4 = consignInData2.ConsignInDetailTable.NewRow();
							foreach (DataColumn column in consignInData2.ConsignInDetailTable.Columns)
							{
								dataRow4[column.ColumnName] = row2[column.ColumnName];
							}
							dataRow4["VoucherID"] = "_" + text;
							consignInData2.ConsignInDetailTable.Rows.Add(dataRow4);
							DataRow[] array = consignInData.Tables["Product_Lot_Receiving_Detail"].Select("ProductID = '" + text6 + "' AND RowIndex = " + num3);
							for (int i = 0; i < array.Length; i++)
							{
								DataRow dataRow5 = consignInData2.Tables["Product_Lot_Receiving_Detail"].NewRow();
								foreach (DataColumn column2 in consignInData2.Tables["Product_Lot_Receiving_Detail"].Columns)
								{
									dataRow5[column2.ColumnName] = array[i][column2.ColumnName];
								}
								consignInData2.Tables["Product_Lot_Receiving_Detail"].Rows.Add(dataRow5);
							}
						}
						else
						{
							DataRow[] array2 = dataTable.Select("ProductID = '" + text6 + "' AND LocationID = '" + text7 + "' AND RowIndex = " + num3);
							if (array2.Length == 0)
							{
								throw new CompanyException("Cannot remove old items from this consignment because some lots are already issued.\nItem: " + text6, 1053);
							}
							num5 = decimal.Parse(array2[0]["Quantity"].ToString());
							if (!(num5 == num4))
							{
								if (num4 < num5)
								{
									throw new CompanyException("Reducing quantity for a consignment which has used lots is not allowed.", 1054);
								}
								num6 = num4 - num5;
								textCommand = "   UPDATE Inventory_Transactions SET Quantity = Quantity + " + num6 + "\r\n                                        WHERE SysDocID = '" + text2 + "' AND VoucherID = '" + text + "' AND ProductID = '" + text6 + "' AND RowIndex = " + num3 + "\r\n\r\n\r\n                                        UPDATE Product_Lot SET LotQty = LotQty + " + num6 + "\r\n                                        WHERE DocID = '" + text2 + "' AND ReceiptNumber = '" + text + "' AND ItemCode = '" + text6 + "' AND RowIndex = " + num3 + "\r\n\r\n                                        UPDATE Consign_In_Detail SET Quantity = Quantity + " + num6 + "\r\n                                        WHERE SysDocID = '" + text2 + "' AND VoucherID = '" + text + "' AND ProductID = '" + text6 + "' AND RowIndex = " + num3 + "\r\n\r\n                                        UPDATE Product_Lot_Receiving_Detail SET LotQty = LotQty + " + num6 + "\r\n                                        WHERE SysDocID = '" + text2 + "' AND VoucherID = '" + text + "' AND ProductID = '" + text6 + "' AND RowIndex = " + num3 + "\r\n\r\n                                        UPDATE Product_Location SET Quantity = Quantity + " + num6 + "\r\n                                        WHERE ProductID = '" + text6 + "' AND LocationID = '" + text7 + "'\r\n\r\n                                        UPDATE Product SET Quantity = Quantity + " + num6 + "\r\n                                        WHERE ProductID = '" + text6 + "' ";
								flag &= (ExecuteNonQuery(textCommand, sqlTransaction) > 0);
							}
						}
					}
					if (consignInData2 != null && consignInData2.ConsignInDetailTable.Rows.Count > 0)
					{
						insertUpdateConsignInCommand = GetInsertUpdateConsignInDetailsCommand(isUpdate: false);
						insertUpdateConsignInCommand.Transaction = sqlTransaction;
						if (consignInData2.Tables["Consign_In_Detail"].Rows.Count > 0)
						{
							flag &= Insert(consignInData2, "Consign_In_Detail", insertUpdateConsignInCommand);
						}
						InventoryTransactionData inventoryTransactionData = new InventoryTransactionData();
						foreach (DataRow row3 in consignInData2.ConsignInDetailTable.Rows)
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
							dataRow7["PayeeID"] = dataRow["VendorID"];
							dataRow7["SysDocType"] = (byte)55;
							dataRow7["Cost"] = 0;
							dataRow7["TransactionDate"] = dataRow["TransactionDate"];
							dataRow7["TransactionType"] = (byte)10;
							dataRow7["DivisionID"] = dataRow["DivisionID"];
							dataRow7["CompanyID"] = dataRow["CompanyID"];
							dataRow7.EndEdit();
							inventoryTransactionData.InventoryTransactionTable.Rows.Add(dataRow7);
						}
						inventoryTransactionData.Merge(consignInData2.Tables["Product_Lot_Receiving_Detail"]);
						flag &= new Products(base.DBConfig).InsertUpdateProductLotReceivingDetail(consignInData2, isUpdate: false, sqlTransaction);
						flag &= new InventoryTransaction(base.DBConfig).InsertUpdateInventoryTransaction(inventoryTransactionData, isUpdate, sqlTransaction);
						textCommand = "    UPDATE Consign_In_Detail SET VoucherID = '" + text + "'\r\n                                WHERE SysDocID = '" + text2 + "' AND VoucherID = '_" + text + "'\r\n\r\n                                UPDATE Inventory_Transactions SET VoucherID = '" + text + "'\r\n                                WHERE SysDocID = '" + text2 + "' AND VoucherID = '_" + text + "'\r\n\r\n                                UPDATE Product_Lot SET ReceiptNumber = '" + text + "'\r\n                                WHERE DocID = '" + text2 + "' AND ReceiptNumber = '_" + text + "' \r\n\r\n                                UPDATE Product_Lot_Receiving_Detail SET VoucherID = '" + text + "'\r\n                                WHERE SysDocID = '" + text2 + "' AND VoucherID = '_" + text + "'  ";
						flag &= (ExecuteNonQuery(textCommand, sqlTransaction) > 0);
					}
				}
				else
				{
					insertUpdateConsignInCommand = GetInsertUpdateConsignInDetailsCommand(isUpdate: false);
					insertUpdateConsignInCommand.Transaction = sqlTransaction;
					if (isUpdate)
					{
						flag &= DeleteConsignInDetailsRows(text2, text, sqlTransaction);
						flag &= new InventoryTransaction(base.DBConfig).DeleteInventoryTransaction(55, text2, text, isDeletingTransaction: false, sqlTransaction);
					}
					if (consignInData.Tables["Consign_In_Detail"].Rows.Count > 0)
					{
						flag &= Insert(consignInData, "Consign_In_Detail", insertUpdateConsignInCommand);
					}
					InventoryTransactionData inventoryTransactionData2 = new InventoryTransactionData();
					foreach (DataRow row4 in consignInData.ConsignInDetailTable.Rows)
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
						dataRow9["PayeeID"] = dataRow["VendorID"];
						dataRow9["SysDocType"] = (byte)55;
						dataRow9["Cost"] = 0;
						dataRow9["TransactionDate"] = dataRow["TransactionDate"];
						dataRow9["TransactionType"] = (byte)10;
						dataRow9["DivisionID"] = dataRow["DivisionID"];
						dataRow9["CompanyID"] = dataRow["CompanyID"];
						dataRow9.EndEdit();
						inventoryTransactionData2.InventoryTransactionTable.Rows.Add(dataRow9);
					}
					inventoryTransactionData2.Merge(consignInData.Tables["Product_Lot_Receiving_Detail"]);
					flag &= new Products(base.DBConfig).InsertUpdateProductLotReceivingDetail(consignInData, isUpdate: false, sqlTransaction);
					flag &= new InventoryTransaction(base.DBConfig).InsertUpdateInventoryTransaction(inventoryTransactionData2, isUpdate, sqlTransaction);
				}
				if (!flag)
				{
					throw new Exception("Failed to save the transaction because of an unexpected error. Please try again.");
				}
				flag &= UpdateTableRowInsertUpdateInfo("Consign_In", "SysDocID", dataRow["SysDocID"].ToString(), "VoucherID", dataRow["VoucherID"].ToString(), sqlTransaction, !isUpdate);
				string entityName = "Consignment In";
				flag = (isUpdate ? (flag & AddActivityLog(entityName, text, text2, ActivityTypes.Update, sqlTransaction)) : (flag & AddActivityLog(entityName, text, text2, ActivityTypes.Add, sqlTransaction)));
				if (!isUpdate)
				{
					flag &= new SystemDocuments(base.DBConfig).UpdateNextDocumentNumber(dataRow["SysDocID"].ToString(), dataRow["VoucherID"].ToString(), "Consign_In", "VoucherID", sqlTransaction);
				}
				if (!flag)
				{
					return flag;
				}
				flag &= new Approval(base.DBConfig).CreateTransactionApprovalTasks(SysDocTypes.ConsignIn, text2, text, "Consign_In", sqlTransaction);
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

		public bool CloseConsignIn(ConsignInData consignInData)
		{
			bool flag = true;
			SqlCommand consignInCloseCommand = GetConsignInCloseCommand();
			try
			{
				DataRow dataRow = consignInData.ConsignInTable.Rows[0];
				SqlTransaction sqlTransaction = base.DBConfig.StartNewTransaction();
				string text = dataRow["VoucherID"].ToString();
				string text2 = dataRow["SysDocID"].ToString();
				bool flag2 = false;
				object fieldValue = new Databases(base.DBConfig).GetFieldValue("Consign_In", "Status", "SysDocID", text2, "VoucherID", text, sqlTransaction);
				if (fieldValue != null && fieldValue.ToString() != "")
				{
					flag2 = (byte.Parse(fieldValue.ToString()) == 3);
				}
				if (flag2)
				{
					throw new CompanyException("This consignment is already closed.");
				}
				dataRow["Status"] = 3;
				consignInCloseCommand.Transaction = sqlTransaction;
				flag &= Update(consignInData, "Consign_In", consignInCloseCommand);
				GLData journalData = CreateConsignCloseGLData(consignInData, sqlTransaction);
				flag &= new Journal(base.DBConfig).InsertUpdateJournal(journalData, isUpdate: false, sqlTransaction);
				if (!flag)
				{
					return flag;
				}
				string entityName = "Consignment In Closing";
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

		public ConsignInData GetConsignInByID(string sysDocID, string voucherID)
		{
			try
			{
				ConsignInData consignInData = new ConsignInData();
				string textCommand = "SELECT * FROM Consign_In WHERE VoucherID='" + voucherID + "' AND SysDocID='" + sysDocID + "'";
				FillDataSet(consignInData, "Consign_In", textCommand);
				if (consignInData == null || consignInData.Tables.Count == 0 || consignInData.Tables["Consign_In"].Rows.Count == 0)
				{
					return null;
				}
				textCommand = "SELECT TD.*,TD.Quantity-ISNULL(QuantitySettled,0) - ISNULL(QuantityReturned,0) AS QuantityBalance,Product.Description,Product.ItemType\r\n                        FROM Consign_In_Detail TD INNER JOIN Product ON TD.ProductID=Product.ProductID\r\n                        WHERE VoucherID='" + voucherID + "' AND SysDocID='" + sysDocID + "' ORDER BY RowIndex";
				FillDataSet(consignInData, "Consign_In_Detail", textCommand);
				textCommand = "SELECT * FROM Product_Lot_Receiving_Detail\r\n\t\t\t\t\t\tWHERE VoucherID='" + voucherID + "' AND SysDocID='" + sysDocID + "' ORDER BY RowIndex";
				FillDataSet(consignInData, "Product_Lot_Receiving_Detail", textCommand);
				textCommand = "SELECT * FROM ConsignIn_Settlement\r\n\t\t\t\t\t\tWHERE ConsignSysDocID='" + sysDocID + "' AND ConsignVoucherID='" + voucherID + "'";
				FillDataSet(consignInData, "ConsignIn_Settlement", textCommand);
				return consignInData;
			}
			catch
			{
				throw;
			}
		}

		private GLData CreateConsignCloseGLData(ConsignInData transactionData, SqlTransaction sqlTransaction)
		{
			try
			{
				GLData gLData = new GLData();
				DataRow dataRow = transactionData.ConsignInTable.Rows[0];
				int currencyDecimalPoints = new CompanyInformations(base.DBConfig).CurrencyDecimalPoints;
				dataRow["VendorID"].ToString();
				string text = dataRow["SysDocID"].ToString();
				string text2 = dataRow["VoucherID"].ToString();
				string value = dataRow["CompanyID"].ToString();
				string value2 = dataRow["DivisionID"].ToString();
				string textCommand = "SELECT Loc.ConsignInAccountID,LOC.ConsignInDiffAccountID\r\n                                FROM System_Document SD INNER JOIN Location LOC ON SD.LocationID = LOC.LocationID\r\n                                WHERE SysDocID = '" + text + "'";
				DataSet dataSet = new DataSet();
				FillDataSet(dataSet, "Accounts", textCommand, sqlTransaction);
				if (dataSet == null || dataSet.Tables.Count == 0 || dataSet.Tables[0].Rows.Count == 0)
				{
					throw new CompanyException("There is no location assigned to this system document or location record is missing.");
				}
				DataRow dataRow2 = dataSet.Tables["Accounts"].Rows[0];
				new Databases(base.DBConfig).GetFieldValue("System_Document", "LocationID", "SysDocID", text, sqlTransaction).ToString();
				string value3 = dataRow2["ConsignInAccountID"].ToString();
				string value4 = dataRow2["ConsignInDiffAccountID"].ToString();
				DataRow dataRow3 = gLData.JournalTable.NewRow();
				SysDocTypes sysDocTypes = SysDocTypes.ConsignInClosing;
				dataRow3["JournalID"] = 0;
				dataRow3["JournalDate"] = dataRow["CloseDate"];
				dataRow3["SysDocID"] = dataRow["SysDocID"];
				dataRow3["SysDocType"] = (byte)sysDocTypes;
				dataRow3["VoucherID"] = dataRow["VoucherID"];
				dataRow3["Reference"] = dataRow["Reference"];
				dataRow3["Narration"] = "Consignment-In Closing - " + text + "#" + text2;
				dataRow3.EndEdit();
				gLData.JournalTable.Rows.Add(dataRow3);
				decimal num = default(decimal);
				DataSet consignInClosingSummary = GetConsignInClosingSummary(text, text2, sqlTransaction);
				DataRow dataRow4 = consignInClosingSummary.Tables["Consign_In"].Rows[0];
				decimal d = default(decimal);
				decimal d2 = default(decimal);
				if (dataRow4["ActualSales"] != DBNull.Value)
				{
					d = decimal.Parse(dataRow4["ActualSales"].ToString());
				}
				if (dataRow4["SettledSales"] != DBNull.Value)
				{
					d2 = decimal.Parse(dataRow4["SettledSales"].ToString());
				}
				decimal num2 = d - d2;
				DataRow dataRow5 = null;
				if (num2 != 0m)
				{
					dataRow5 = gLData.JournalDetailsTable.NewRow();
					dataRow5.BeginEdit();
					dataRow5["JournalID"] = 0;
					dataRow5["AccountID"] = value4;
					if (num2 > 0m)
					{
						dataRow5["Debit"] = DBNull.Value;
						dataRow5["Credit"] = num2;
					}
					else
					{
						dataRow5["Debit"] = Math.Abs(num2);
						dataRow5["Credit"] = DBNull.Value;
					}
					dataRow5["CompanyID"] = value;
					dataRow5["DivisionID"] = value2;
					dataRow5.EndEdit();
					gLData.JournalDetailsTable.Rows.Add(dataRow5);
					dataRow5 = gLData.JournalDetailsTable.NewRow();
					dataRow5.BeginEdit();
					dataRow5["JournalID"] = 0;
					dataRow5["AccountID"] = value3;
					if (num2 > 0m)
					{
						dataRow5["Debit"] = num2;
						dataRow5["Credit"] = DBNull.Value;
					}
					else
					{
						dataRow5["Debit"] = DBNull.Value;
						dataRow5["Credit"] = Math.Abs(num2);
					}
					dataRow5["CompanyID"] = value;
					dataRow5["DivisionID"] = value2;
					dataRow5.EndEdit();
					gLData.JournalDetailsTable.Rows.Add(dataRow5);
				}
				Hashtable hashtable = new Hashtable();
				ArrayList arrayList = new ArrayList();
				string text3 = "";
				foreach (DataRow row in consignInClosingSummary.Tables["Bills"].Rows)
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
					dataRow5 = gLData.JournalDetailsTable.NewRow();
					dataRow5.BeginEdit();
					text3 = arrayList[i].ToString();
					num = decimal.Parse(hashtable[text3].ToString());
					if (!(num == 0m))
					{
						dataRow5 = gLData.JournalDetailsTable.NewRow();
						dataRow5.BeginEdit();
						dataRow5["JournalID"] = 0;
						dataRow5["AccountID"] = text3;
						if (num > 0m)
						{
							dataRow5["Debit"] = num;
							dataRow5["Credit"] = DBNull.Value;
						}
						else
						{
							dataRow5["Debit"] = DBNull.Value;
							dataRow5["Credit"] = Math.Abs(num);
						}
						dataRow5["CompanyID"] = value;
						dataRow5["DivisionID"] = value2;
						dataRow5.EndEdit();
						gLData.JournalDetailsTable.Rows.Add(dataRow5);
						num3 += num;
					}
				}
				if (num3 != 0m)
				{
					dataRow5 = gLData.JournalDetailsTable.NewRow();
					dataRow5.BeginEdit();
					dataRow5["JournalID"] = 0;
					dataRow5["AccountID"] = value4;
					if (num3 > 0m)
					{
						dataRow5["Debit"] = DBNull.Value;
						dataRow5["Credit"] = num3;
					}
					else
					{
						dataRow5["Debit"] = Math.Abs(num3);
						dataRow5["Credit"] = DBNull.Value;
					}
					dataRow5["Reference"] = dataRow["Reference"];
					dataRow5["Description"] = dataRow["Note"];
					dataRow5["CompanyID"] = value;
					dataRow5["DivisionID"] = value2;
					dataRow5.EndEdit();
					gLData.JournalDetailsTable.Rows.Add(dataRow5);
				}
				return gLData;
			}
			catch
			{
				throw;
			}
		}

		internal bool DeleteConsignInDetailsRows(string sysDocID, string voucherID, SqlTransaction sqlTransaction)
		{
			bool flag = true;
			try
			{
				ConsignInData consignInData = new ConsignInData();
				string textCommand = "SELECT SOD.*,ISVOID FROM Consign_In_Detail SOD INNER JOIN Consign_In SO ON SO.SysDocID=SOD.SysDocID AND SO.VOucherID=SOD.VoucherID\r\n                              WHERE SOD.SysDocID = '" + sysDocID + "' AND SOD.VoucherID = '" + voucherID + "'";
				FillDataSet(consignInData, "Consign_In_Detail", textCommand, sqlTransaction);
				bool result = false;
				bool.TryParse(consignInData.ConsignInDetailTable.Rows[0]["IsVoid"].ToString(), out result);
				textCommand = "DELETE FROM Product_Lot_Receiving_Detail WHERE SysDocID = '" + sysDocID + "' AND VoucherID = '" + voucherID + "'";
				flag &= Delete(textCommand, sqlTransaction);
				textCommand = "DELETE FROM Consign_In_Detail WHERE SysDocID = '" + sysDocID + "' AND VoucherID = '" + voucherID + "'";
				return flag & Delete(textCommand, sqlTransaction);
			}
			catch
			{
				throw;
			}
		}

		public bool VoidConsignIn(string sysDocID, string voucherID, bool isVoid)
		{
			bool flag = true;
			try
			{
				SqlTransaction sqlTransaction = base.DBConfig.StartNewTransaction();
				if (new Products(base.DBConfig).HasInUseLots(sysDocID, voucherID))
				{
					throw new CompanyException("This transaction cannot be modifed because some of items are refered by other transactions.");
				}
				string exp = "UPDATE Consign_In SET IsVoid = '" + isVoid.ToString() + "' WHERE SysDocID='" + sysDocID + "' AND VoucherID = '" + voucherID + "'";
				flag &= (ExecuteNonQuery(exp, sqlTransaction) > 0);
				ConsignInData dataSet = new ConsignInData();
				exp = "SELECT * FROM Consign_In_Detail\r\n                              WHERE SysDocID = '" + sysDocID + "' AND VoucherID = '" + voucherID + "'";
				FillDataSet(dataSet, "Consign_In_Detail", exp, sqlTransaction);
				flag &= new InventoryTransaction(base.DBConfig).DeleteInventoryTransaction(55, sysDocID, voucherID, isDeletingTransaction: true, sqlTransaction);
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

		public bool DeleteConsignIn(string sysDocID, string voucherID)
		{
			bool flag = true;
			try
			{
				string text = "";
				SqlTransaction sqlTransaction = base.DBConfig.StartNewTransaction();
				flag &= DeleteConsignInDetailsRows(sysDocID, voucherID, sqlTransaction);
				text = "DELETE FROM Consign_In WHERE SysDocID='" + sysDocID + "' AND VoucherID = '" + voucherID + "'";
				flag &= (ExecuteNonQuery(text, sqlTransaction) >= 0);
				flag &= new InventoryTransaction(base.DBConfig).DeleteInventoryTransaction(55, sysDocID, voucherID, isDeletingTransaction: true, sqlTransaction);
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
				FillDataSet(dataSet, "Consign_In", text);
				return dataSet;
			}
			catch
			{
				throw;
			}
		}

		public DataSet GetConsignInSummaryReport(string sysDocID, string voucherID)
		{
			try
			{
				DataSet dataSet = new DataSet();
				string textCommand = "SELECT CI.SysDocID,CI.VoucherID, CI.SysDocID + '#' + CI.VoucherID ConsignInNo, VendorName,VA.Address1,CI.ContainerNo,CI.TransactionDate,T.TransporterName,CI.PONumber,CI.BLNo,CI.Reference,P.PortName\r\n                                FROM  Consign_In CI LEFT JOIN Vendor V ON CI.VendorID=V.VendorID LEFT JOIN Vendor_Address VA ON V.VendorID=VA.VendorID\r\n                                LEFT JOIN Transporter T ON T.TransporterID=CI.TransporterID LEFT JOIN Port P ON CI.ArrivalPort=P.PortID\r\n                                WHERE SysDocID = '" + sysDocID + "' AND VoucherID = '" + voucherID + "' ";
				FillDataSet(dataSet, "Vendor", textCommand);
				textCommand = "SELECT TransactionDate FROM Consign_In WHERE SysDocID = '" + sysDocID + "' AND VoucherID = '" + voucherID + "'";
				DateTime date = DateTime.Parse(ExecuteScalar(textCommand).ToString()).AddDays(-30.0);
				string text = StoreConfiguration.ToSqlDateTimeString(date);
				textCommand = " (SELECT LotNumber into #tmp1 FROM Product_Lot PL WHERE (ReceiptNumber = '" + voucherID + "' AND DocID = '" + sysDocID + "')\r\n                             OR SourceLotNumber IN (SELECT PL.LotNumber FROM Product_Lot PL WHERE (ReceiptNumber = '" + voucherID + "' AND DocID = '" + sysDocID + "')))\r\n\r\n                   SELECT DocID,ReceiptNumber,LotNumber, ItemCode, Description, LotQty,SUM(ISNULL([Quantity Delivered],0))  AS [Quantity Delivered] ,\r\n                             SUM(ISNULL(FOC,0)) AS FOC,  SUM(ISNULL([Quantity Invoiced],0)) AS [Quantity Invoiced],\r\n                             SUM(ISNULL(Amount,0)) AS Amount \r\n\t\t\t\t\t\t\t\r\n\t\t\t\t\t\t\tFROM (\r\n                            SELECT PL.DocID,PL.ReceiptNumber,PL.LotNumber,  CON.ProductID AS ItemCode,CON.Description,PL.LotQty,SUM(ISNULL(CON.DeliveredQty,0)) AS [Quantity Delivered],\r\n                            SUM(NonSaleQty) +  SUM(ISNULL(CON.FOCQuantity,0)) AS FOC,\r\n\t\t\t\t\t\t\tSUM(ISNULL(CON.InvoiceQty,0) - ISNULL(CON.FOCQuantity,0)) AS [Quantity Invoiced]\r\n                            , SUM(ISNULL(ISNULL(Round((CON.InvoiceQty - ISNULL(FOCQuantity,0)) * Con.UnitPrice,2),0),0)) AS Amount \r\n\t\t\t\t\t\t\t\r\n\t\t\t\t\t\t\tFROM (\r\n                            --Start of Query Section 1\r\n                            SELECT PLD.*,P.Description, IT.TransactionDate,CASE WHEN IT.PayeeType = 'C' THEN IT.PayeeID ELSE NULL END AS CustomerID ,CUS.CustomerName, ISNULL(INV.VoucherID,PLD.VoucherID) AS InvoiceVoucherID, \r\n                            CASE WHEN INV.TransactionDate IS NULL AND CashInv.TransactionDate IS NULL  THEN 0 ELSE  ISNULL(INV.UnitPrice,CashInv.UnitPrice) END AS UnitPrice,\r\n\t\t\t\t\t\t\t CASE WHEN INV.TransactionDate IS NULL AND CashInv.TransactionDate IS NULL  THEN 0 ELSE ISNULL(SoldQty,0)  - (ISNULL(DRD.Quantity,0) + ISNULL(CRSR.Quantity,0) + ISNULL(SRD.Quantity,0)) END AS InvoiceQty,INV.TransactionDate AS InvoiceDate,\r\n                            ISNULL(SoldQty,0)    AS DeliveredQty \r\n\r\n                            FROM (\r\n                            --Start of Query Section 2\r\n                            SELECT SD.SysDocType,SD.DocName, PLS.DocID AS SysDocID, InvoiceNumber AS VoucherID,RowIndex,ItemCode AS ProductID,PLS.LocationID,LotNo AS LotNumber,SoldQty,ISNULL(FOCQuantity,0) AS FOCQuantity, CASE WHEN SD.SysDocType IN (87) THEN SoldQty ELSE 0 END  AS NonSaleQty\r\n                            FROM Product_Lot_Sales PLS INNER JOIN System_Document SD ON PLS.DocID = SD.SysDocID \r\n                            WHERE LotNo IN \r\n                            (SELECT LotNumber FROM #tmp1)\r\n                            AND SysDocType NOT IN (19,20,21,40)\r\n   \r\n                            UNION\r\n\r\n                            SELECT SD.SysDocType,SD.DocName,PLS.SysDocID, VoucherID,RowIndex, ProductID,PLS.LocationID,SourceLotNumber AS  LotNumber,-1 * LotQty AS SoldQty,0 AS FOCQuantity, 0 AS NonSaleQty\r\n                            FROM Product_Lot_Receiving_Detail PLS INNER JOIN System_Document SD ON PLS.SysDocID = SD.SysDocID WHERE SourceLotNumber IN \r\n                             (SELECT LotNumber FROM #tmp1)\r\n                            AND SysDocType NOT IN (19,20,21,40))  AS PLD\r\n                            --End of Query Section 2\r\n                            \r\n\r\n                            LEFT OUTER JOIN Inventory_Transactions IT ON IT.SysDocID = PLD.SysDocID AND IT.VoucherID = PLD.VoucherID AND IT.RowIndex = PLD.RowIndex\r\n                            LEFT OUTER JOIN  (SELECT SID2.Quantity,SID2.OrderSysDocID,SID2.VoucherID,SID2.OrderVoucherID,SID2.OrderRowIndex,SID2.UnitPrice,SI2.TransactionDate\r\n\t\t\t\t\t\t\t FROM Sales_Invoice_Detail SID2 INNER JOIN Sales_INVOICE SI2 ON SID2.SysDocID = SI2.SysDocID AND SID2.VoucherID = SI2.VoucherID AND ISNULL(SI2.IsVoid,'False') = 'False' AND SI2.TransactionDate>='" + text + "') AS INV ON INV.OrderSysDocID = PLD.SysDocID\r\n\t\t\t\t\t\t\t AND INV.OrderVoucherID = PLD.VoucherID AND INV.OrderRowIndex = PLD.RowIndex\r\n\r\n\t\t\t\t\t\t\t --Cash Sales\r\n\t\t\t\t\t\t\t LEFT OUTER JOIN (SELECT SID3.Quantity,SID3.SysDocID,SID3.VoucherID,SID3.RowIndex, SID3.UnitPrice,SI3.TransactionDate\r\n\t\t\t\t\t\t\t FROM Sales_Invoice_Detail SID3 INNER JOIN Sales_INVOICE SI3 ON SID3.SysDocID = SI3.SysDocID AND SID3.VoucherID = SI3.VoucherID AND ISNULL(SI3.IsVoid,'False') = 'False' AND SI3.TransactionDate>='" + text + "') AS CashInv\r\n\t\t\t\t\t\t\t ON CashInv.SysDocID = PLD.SysDocID  AND CashInv.VoucherID = PLD.VoucherID AND CashInv.RowIndex = PLD.RowIndex\r\n\r\n                            LEFT OUTER JOIN Sales_Return_Detail  SRD ON SRD.SourceSysDocID = PLD.SysDocID AND SRD.SourceVoucherID = PLD.VoucherID AND SRD.SourceRowIndex = PLD.RowIndex\r\n                           \r\n                            LEFT OUTER JOIN (SELECT SI2.OrderSysDocID,SI2.OrderVoucherID,SI2.OrderRowIndex, SRD2.Quantity FROM Sales_Return_Detail SRD2 INNER JOIN Sales_Invoice_Detail SI2 ON SRD2.SourceSysDocID=SI2.SysDocID\r\n\t\t\t\t\t\t\tAND SRD2.SourceVoucherID = SI2.voucherID AND SRD2.SourceRowIndex = SI2.rowIndex) CrSR ON CRSR.OrderSysDocID = PLD.SysDocID AND CRSR.OrderVoucherID = PLD.VoucherID AND CRSR.OrderRowIndex = PLD.RowIndex\r\n\r\n\r\n                            LEFT OUTER JOIN Product P ON P.ProductID = PLD.ProductID\r\n                             LEFT OUTER JOIN (SELECT DRD.Quantity,DR.SysDocID,DR.VoucherID,DRD.DNRowIndex,DR.DNoteSysDocID,DR.DNoteVoucherID FROM Delivery_Return DR\r\n\t\t\t\t\t                 INNER JOIN Delivery_Return_Detail DRD ON DRD.SysDocID = DR.SysDocID AND DRD.VoucherID = DR.VoucherID WHERE   ISNULL(DR.IsVoid,'False') = 'False') DRD\r\n\t\t\t\t\t\t\t\t\t ON  DRD.DNoteSysDocID = PLD.SysDocID AND DRD.DNoteVoucherID = PLD.VoucherID  AND  DRD.DNRowIndex = PLD.RowIndex \r\n                            LEFT OUTER JOIN Customer CUS ON CUS.CUstomerID = IT.PayeeID AND IT.PayeeType = 'C') CON --End of Query Section 1\r\n                            INNER JOIN Product_Lot PL ON PL.DocID = '" + sysDocID + "' AND PL.ReceiptNumber = '" + voucherID + "' AND PL.ItemCode = CON.ProductID\r\n                            GROUP BY CON.ProductID,Con.Description, PL.DocID,PL.ReceiptNumber,PL.LotNumber,PL.LotQty   \r\n\r\n\r\n                                UNION\r\n\r\n                                SELECT CID.SysDocID AS DocID, CID.VoucherID AS ReceiptNumber,PL.LotNumber, CID.ProductID AS ItemCode,P.Description,CID.Quantity AS LotQty, 0 AS [Quantity Delivered], 0 AS FOC,0  AS [Quantity Invoiced], 0 AS Amount\r\n                                FROM Consign_In_Detail CID INNER JOIN Consign_IN CI ON CID.SysDocID= CI.SysDocID AnD CID.VoucherID = CI.VoucherID\r\n                                INNER JOIN Product_LOT PL ON PL.DocID = CID.SysDocID AND PL.ReceiptNumber = CID.VoucherID AND PL.RowIndex = CID.RowIndex\r\n                                INNER JOIN Product P ON P.ProductID = CID.ProductID\r\n                                WHERE ISNULL(CI.IsVoid,'False')= 'False' AND CI.VoucherID = '" + voucherID + "' AND CI.SysDocID = '" + sysDocID + "' \r\n                                AND PL.LOTNumber  NOT  IN (SELECT LotNumber FROM Product_Lot_Issue_Detail PLI INNER JOIN System_Document SD ON SD.SysDocID = PLI.SysDocID WHERE SysDocType NOT IN (19,20,21,40))\r\n                                AND PL.LOTNumber  NOT  IN (SELECT SourceLotNumber FROM Product_Lot_Issue_Detail PLI INNER JOIN System_Document SD ON SD.SysDocID = PLI.SysDocID WHERE SOURCELotNumber IS NOT NULL AND SysDocType NOT IN (19,20,21,40))\r\n                                  ) AS X GROUP BY DocID,ReceiptNumber,LotNumber, ItemCode, Description, LotQty  ORDER BY ItemCode\r\n                                ";
				textCommand += "drop table #tmp1";
				FillDataSet(dataSet, "Consign_In", textCommand);
				textCommand = " (SELECT LotNumber into #tmp1 FROM Product_Lot PL WHERE (ReceiptNumber = '" + voucherID + "' AND DocID = '" + sysDocID + "')\r\n                             OR SourceLotNumber IN (SELECT PL.LotNumber FROM Product_Lot PL WHERE (ReceiptNumber = '" + voucherID + "' AND DocID = '" + sysDocID + "')))\r\n                        SELECT '" + sysDocID + "' AS DocID, '" + voucherID + "' AS ReceiptNumber, CON.CustomerID,ISNULL(Con.CustomerName,'Non Sale Issue') AS CustomerName,CON.Description,COn.DocName,\r\n                        CASE WHEN SysDocType IN (87) THEN SUM(CON.DeliveredQty) ELSE 0 END  + SUM(CON.FOCQuantity) AS FOC,CON.InvoiceDate,SUM(CON.InvoiceQty - FOCQuantity) AS [Quantity Invoiced],\r\n                        CON.InvoiceVoucherID,COn.LocationID, CON.ProductID AS ItemCode,SUM(CON.DeliveredQty) AS [Quantity Delivered],CON.SysDocID,CON.SysDocType,CON.TransactionDate,CON.UnitPrice AS [Unit Price],\r\n                        CON.VoucherID, ISNULL(Round(SUM(CON.InvoiceQty - FOCQuantity) * Con.UnitPrice,2),0) AS Amount FROM \r\n\r\n\r\n                        (SELECT PLD.*,P.Description, IT.TransactionDate,CASE WHEN IT.PayeeType = 'C' THEN IT.PayeeID ELSE NULL END AS CustomerID ,\r\n                        CUS.CustomerName, ISNULL(INV.VoucherID,PLD.VoucherID) AS InvoiceVoucherID, \r\n                         CASE WHEN INV.TransactionDate IS NULL AND CashInv.TransactionDate IS NULL  THEN 0 ELSE ISNULL(INV.UnitPrice,CashInv.UnitPrice)  END AS UnitPrice,ISNULL(DRD.Quantity,0) + ISNULL(SRD.Quantity,0) AS QuantityReturned,\r\n                         CASE WHEN INV.TransactionDate IS NULL AND CashInv.TransactionDate IS NULL  THEN 0 ELSE ISNULL(SoldQty,0)- (ISNULL(DRD.Quantity,0) +  ISNULL(CRSR.Quantity,0) + ISNULL(SRD.Quantity,0))  END AS InvoiceQty ,ISNULL(INV.TransactionDate,CashInv.TransactionDate) AS InvoiceDate,\r\n                         ISNULL(SoldQty,0) - (ISNULL(DRD.Quantity,0) +  ISNULL(CRSR.Quantity,0) + ISNULL(SRD.Quantity,0))  AS DeliveredQty\r\n                          FROM ( \r\n  \r\n  \r\n                          SELECT SD.SysDocType,SD.DocName, PLS.DocID AS SysDocID, InvoiceNumber AS VoucherID,RowIndex,ItemCode AS ProductID,PLS.LocationID,LotNo AS LotNumber,SoldQty,ISNULL(FOCQuantity,0) AS FOCQuantity\r\n                          FROM Product_Lot_Sales PLS INNER JOIN System_Document SD ON PLS.DocID = SD.SysDocID WHERE LotNo IN \r\n                         (SELECT LotNumber FROM #tmp1)\r\n                           AND SysDocType NOT IN (19,20,21,40)\r\n                         UNION\r\n\t\t                   SELECT SD.SysDocType,SD.DocName,PLS.SysDocID, VoucherID,RowIndex, ProductID,PLS.LocationID,SourceLotNumber AS  LotNumber,-1 * LotQty AS SoldQty,0 AS FOCQuantity\r\n\t\t                    FROM Product_Lot_Receiving_Detail PLS INNER JOIN System_Document SD ON PLS.SysDocID = SD.SysDocID WHERE SourceLotNumber IN \r\n\t\t\t                  (SELECT LotNumber FROM #tmp1)\r\n\t\t\t                   AND SysDocType NOT IN (19,20,21,40))  AS PLD\r\n\r\n\t\t\t                    LEFT OUTER JOIN Inventory_Transactions IT ON IT.SysDocID = PLD.SysDocID AND IT.VoucherID = PLD.VoucherID AND IT.RowIndex = PLD.RowIndex\r\n\t\t\t\t                 LEFT OUTER JOIN  (SELECT SID2.Quantity,SID2.OrderSysDocID,SID2.VoucherID,SID2.OrderVoucherID,SID2.OrderRowIndex,SID2.UnitPrice,SI2.TransactionDate\r\n\t\t\t\t\t\t\t\t FROM Sales_Invoice_Detail SID2 INNER JOIN Sales_INVOICE SI2 ON SID2.SysDocID = SI2.SysDocID AND SID2.VoucherID = SI2.VoucherID AND ISNULL(SI2.IsVoid,'False') = 'False' AND SI2.TransactionDate>='" + text + "') AS INV ON INV.OrderSysDocID = PLD.SysDocID\r\n\t\t\t\t\t\t\t\t AND INV.OrderVoucherID = PLD.VoucherID AND INV.OrderRowIndex = PLD.RowIndex\r\n\r\n\t\t\t\t\t\t\t\t --Cash Sales\r\n\t\t\t\t\t\t\t\t LEFT OUTER JOIN (SELECT SID3.Quantity,SID3.SysDocID,SID3.VoucherID,SID3.RowIndex, SID3.UnitPrice,SI3.TransactionDate\r\n\t\t\t\t\t\t\t\t FROM Sales_Invoice_Detail SID3 INNER JOIN Sales_INVOICE SI3 ON SID3.SysDocID = SI3.SysDocID AND SID3.VoucherID = SI3.VoucherID AND ISNULL(SI3.IsVoid,'False') = 'False' AND SI3.TransactionDate>='" + text + "' ) AS CashInv\r\n\t\t\t\t\t\t\t\t ON CashInv.SysDocID = PLD.SysDocID  AND CashInv.VoucherID = PLD.VoucherID AND CashInv.RowIndex = PLD.RowIndex\r\n\r\n\t\t\t\t                     LEFT OUTER JOIN (SELECT DRD.Quantity,DR.SysDocID,DR.VoucherID,DRD.DNRowIndex,DR.DNoteSysDocID,DR.DNoteVoucherID FROM Delivery_Return DR\r\n\t\t\t\t\t                 INNER JOIN Delivery_Return_Detail DRD ON DRD.SysDocID = DR.SysDocID AND DRD.VoucherID = DR.VoucherID WHERE   ISNULL(DR.IsVoid,'False') = 'False') DRD\r\n\t\t\t\t\t\t\t\t\t ON  DRD.DNoteSysDocID = PLD.SysDocID AND DRD.DNoteVoucherID = PLD.VoucherID  AND  DRD.DNRowIndex = PLD.RowIndex \r\n\t\t\t\t\t                  LEFT OUTER JOIN Sales_Return_Detail  SRD ON SRD.SourceSysDocID = PLD.SysDocID AND SRD.SourceVoucherID = PLD.VoucherID AND SRD.SourceRowIndex = PLD.RowIndex  \r\n\t\t\t\t\t                 LEFT OUTER JOIN (SELECT SI2.OrderSysDocID,SI2.OrderVoucherID,SI2.OrderRowIndex, SRD2.Quantity FROM Sales_Return_Detail SRD2 INNER JOIN Sales_Invoice_Detail SI2 ON SRD2.SourceSysDocID=SI2.SysDocID\r\n\t\t\t\t\t\t\t\t\t\t\tAND SRD2.SourceVoucherID = SI2.voucherID AND SRD2.SourceRowIndex = SI2.rowIndex) CrSR ON CRSR.OrderSysDocID = PLD.SysDocID AND CRSR.OrderVoucherID = PLD.VoucherID AND CRSR.OrderRowIndex = PLD.RowIndex\r\n\r\n                                            LEFT OUTER JOIN Product P ON P.ProductID = PLD.ProductID\r\n\t\t\t\t\t                      LEFT OUTER JOIN Customer CUS ON CUS.CUstomerID = IT.PayeeID AND IT.PayeeType = 'C') CON\r\n\t\t\t\t\t\t                   WHERE DeliveredQty > 0 \r\n\t\t\t\t\t\t   GROUP BY   CON.CustomerID,Con.CustomerName,CON.Description,COn.DocName, CON.InvoiceDate, CON.InvoiceVoucherID,COn.LocationID, CON.ProductID ,CON.SysDocID,CON.SysDocType,CON.TransactionDate,CON.UnitPrice  , CON.VoucherID \r\n                                 ORDER BY ItemCode,TransactionDate  ";
				textCommand += "drop table #tmp1";
				FillDataSet(dataSet, "Consign_In_Detail", textCommand);
				textCommand = "SELECT   JD.SysDocID,JD.VoucherID,J.JournalDate, JournalDetailID AS RowIndex,JD.AccountID,A.AccountName,ConsignID,ConsignExpenseID,Debit AS Amount\r\n                        FROM Journal_Details JD LEFT JOIN Account A ON JD.AccountID = A.AccountID\r\n                        LEFT JOIN Journal J ON J.JournalID=JD.JournalID\r\n                        WHERE ISNULL(IsBilled,'False')='False' AND Debit IS NOT NULL AND ConsignID =  '" + sysDocID + "#" + voucherID + "'";
				FillDataSet(dataSet, "ConsignIn_Expense", textCommand);
				dataSet.Relations.Add("VendorConsignIn", new DataColumn[2]
				{
					dataSet.Tables["Vendor"].Columns["SysDocID"],
					dataSet.Tables["Vendor"].Columns["VoucherID"]
				}, new DataColumn[2]
				{
					dataSet.Tables["Consign_In"].Columns["DocID"],
					dataSet.Tables["Consign_In"].Columns["ReceiptNumber"]
				}, createConstraints: false);
				dataSet.Relations.Add("VendorConsignInDetail", new DataColumn[2]
				{
					dataSet.Tables["Vendor"].Columns["SysDocID"],
					dataSet.Tables["Vendor"].Columns["VoucherID"]
				}, new DataColumn[2]
				{
					dataSet.Tables["Consign_In_Detail"].Columns["DocID"],
					dataSet.Tables["Consign_In_Detail"].Columns["ReceiptNumber"]
				}, createConstraints: false);
				dataSet.Relations.Add("VendorConsignInExpense", dataSet.Tables["Vendor"].Columns["ConsignInNo"], dataSet.Tables["ConsignIn_Expense"].Columns["ConsignID"], createConstraints: false);
				return dataSet;
			}
			catch
			{
				throw;
			}
		}

		public DataSet GetConsignInToPrint(string sysDocID, string voucherID)
		{
			return GetConsignInToPrint(sysDocID, new string[1]
			{
				voucherID
			});
		}

		public DataSet GetConsignInToPrint(string sysDocID, string[] voucherID)
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
				string cmdText = "SELECT SI.SysDocID, SI.VoucherID,SI.VendorID,VendorName, SI.TransactionDate,\r\n                                VA.AddressPrintFormat AS VendorAddress,ShippingMethodName,\r\n                                ISNULL(SI.CurrencyID,(SELECT CurrencyID FROM Currency WHERE IsBase='True')) AS CurrencyID,\r\n                                SI.TermID,TermName, SI.IsVoid,SI.Reference,  SI.Discount AS Discount,\r\n                                 SI.Total AS Total, SI.PONumber,SI.Note, \r\n                                (SELECT TransporterName FROM Transporter WHERE TransporterID = SI.TransporterID) AS TransporterName\r\n                                FROM  Consign_In SI INNER JOIN Vendor ON SI.VendorID=Vendor.VendorID\r\n                                LEFT OUTER JOIN Payment_Term PT ON SI.TermID=PT.PaymentTermID\r\n                                LEFT OUTER JOIN Vendor_Address VA ON VA.VendorID=SI.VendorID AND VA.AddressID='PRIMARY'\r\n                                LEFT OUTER JOIN Shipping_Method SM ON SM.ShippingMethodID=SI.ShippingMethodID   \r\n                                WHERE SI.SysDocID = '" + sysDocID + "' AND SI.VoucherID IN (" + text + ")";
				SqlCommand sqlCommand = new SqlCommand(cmdText);
				FillDataSet(dataSet, "Consign_In", sqlCommand);
				if (dataSet == null || dataSet.Tables.Count == 0 || dataSet.Tables["Consign_In"].Rows.Count == 0)
				{
					return null;
				}
				cmdText = "SELECT DISTINCT PRD.SysDocID, PRD.VoucherID,PRD.ProductID,PRD.Description,ISNULL(PRD.UnitQuantity,PRD.Quantity) AS ReceivedQuantity, \r\n\t\t\t\t\t\t    (SELECT TOP 1 ISNULL(POD.UnitQuantity, POD.Quantity) FROM Purchase_Order_Detail POD WHERE POD.SysDocID = PRD.OrderSysDocID AND POD.VoucherID = PRD.OrderVoucherID  AND POD.ProductID = PRD.ProductID) AS OrderedQuantity,\r\n\t\t\t\t\t\tP.Attribute1,P.Attribute2,P.Attribute3,P.MatrixParentID, PRD.UnitPrice AS UnitPrice,\r\n                        ISNULL(PRD.UnitQuantity,PRD.Quantity)*ISNULL(PRD.UnitPrice,0) AS Total,PRD.UnitID, P.BrandID, PRD.RowIndex,PRD.TargetPrice\r\n                        FROM   Consign_In_Detail PRD\r\n                        INNER JOIN Product P ON P.ProductID=PRD.ProductID\r\n                        WHERE PRD.SysDocID='" + sysDocID + "' AND PRD.VoucherID IN (" + text + ")";
				FillDataSet(dataSet, "Consign_In_Detail", cmdText);
				dataSet.Relations.Add("ConsignIn", new DataColumn[2]
				{
					dataSet.Tables["Consign_In"].Columns["SysDocID"],
					dataSet.Tables["Consign_In"].Columns["VoucherID"]
				}, new DataColumn[2]
				{
					dataSet.Tables["Consign_In_Detail"].Columns["SysDocID"],
					dataSet.Tables["Consign_In_Detail"].Columns["VoucherID"]
				}, createConstraints: false);
				dataSet.Tables["Consign_In"].Columns.Add("TotalInWords", typeof(string));
				foreach (DataRow row in dataSet.Tables["Consign_In"].Rows)
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
			string text3 = "SELECT   CO.SysDocID [Doc ID], CO.VoucherID [VoucherID],CO.VendorID [Vendor Code], V.VendorName [Vendor Name], CO.ContainerNo [Container #], TransactionDate [Date]\r\n                            FROM Consign_In CO                           \r\n                            INNER JOIN Consign_In_Detail COD ON CO.SysDocID=COD.SysDocID AND CO.VoucherID=COD.VoucherID\r\n                            Inner JOIN Vendor V ON V.VendorID=CO.VendorID ";
			if (from != DateTime.MinValue)
			{
				text3 = text3 + " WHERE TransactionDate Between '" + text + "' AND '" + text2 + "'";
			}
			if (!showVoid)
			{
				text3 += " AND ISNULL(IsVoid,'False')='False'";
			}
			text3 += " GROUP BY IsVoid,CO.SysDocID,CO.VoucherID,CO.VendorID ,V.VendorName,CO.ContainerNo,TransactionDate";
			SqlCommand sqlCommand = new SqlCommand(text3);
			FillDataSet(dataSet, "Consign_In", sqlCommand);
			return dataSet;
		}

		public DataSet GetConsignmentsByStatus(string vendorID, ConsignInStatusEnum status)
		{
			try
			{
				DataSet dataSet = new DataSet();
				string str = "SELECT CO.SysDocID [Doc ID], CO.VoucherID [Number],CO.VendorID [Vendor Code], CUS.VendorName [Vendor Name], CO.ContainerNo [Container #], TransactionDate [Date]\r\n                            FROM Consign_In CO INNER JOIN Consign_In_Detail COD\r\n                            ON CO.SysDocID=COD.SysDocID AND CO.VoucherID=COD.VoucherID\r\n                            INNER JOIN Vendor CUS ON CO.VendorID=CUS.VendorID  WHERE ISNULL(Status,1) =   " + (byte)status;
				if (vendorID != "")
				{
					str = str + " AND CO.VendorID='" + vendorID + "'";
				}
				str += " GROUP BY CO.SysDocID, CO.VoucherID,CO.VendorID,TransactionDate, CUS.VendorName, CO.ContainerNo";
				SqlCommand sqlCommand = new SqlCommand(str);
				FillDataSet(dataSet, "Consign_In", sqlCommand);
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
				string exp = "SELECT SUM((CASE WHEN UnitQuantity IS NULL \r\n                                THEN Quantity ELSE UnitQuantity END))- SUM(ISNULL(QuantitySettled,0) ) - SUM(ISNULL(QuantityReturned,0) )\r\n\t                             FROM Consign_In_Detail SOD WHERE SysDocID='" + sysDocID + "' AND VoucherID='" + voucherID + "'";
				object obj = ExecuteScalar(exp, sqlTransaction);
				if (obj == null)
				{
					return true;
				}
				exp = ((!(decimal.Parse(obj.ToString()) <= 0m)) ? ("UPDATE Consign_In SET IsClosed = 'False',Status = 1 WHERE SysDocID='" + sysDocID + "' AND VoucherID='" + voucherID + "'") : ("UPDATE Consign_In SET IsClosed = 'True',Status=2 WHERE SysDocID='" + sysDocID + "' AND VoucherID='" + voucherID + "'"));
				return ExecuteNonQuery(exp, sqlTransaction) > 0;
			}
			catch
			{
				return false;
			}
		}

		internal bool SetStatus(string sysDocID, string voucherID, ConsignInStatusEnum status, SqlTransaction sqlTransaction)
		{
			try
			{
				string exp = "SELECT SUM(Quantity)- SUM(ISNULL(QuantitySettled,0) ) - SUM(ISNULL(QuantityReturned,0) )\r\n\t                             FROM Consign_In_Detail SOD WHERE SysDocID='" + sysDocID + "' AND VoucherID='" + voucherID + "'";
				object obj = ExecuteScalar(exp, sqlTransaction);
				if (obj == null)
				{
					return true;
				}
				exp = ((!(decimal.Parse(obj.ToString()) <= 0m)) ? ("UPDATE Consign_In SET Status = " + (byte)1 + " WHERE SysDocID= '" + sysDocID + "' AND VoucherID='" + voucherID + "'") : ("UPDATE Consign_In SET Status = " + (byte)2 + " WHERE SysDocID= '" + sysDocID + "' AND VoucherID='" + voucherID + "'"));
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

		public bool AllowModify(string sysDocID, string voucherNumber, SqlTransaction sqlTransaction)
		{
			string exp = "Select Count(*) FROM Consign_In_Detail POD\r\n                                WHERE SysdocID='" + sysDocID + "' AND VoucherID='" + voucherNumber + "' Having SUM(ISNULL(QuantitySettled,0) + ISNULL(QuantityReturned,0)) >0";
			object obj = ExecuteScalar(exp, sqlTransaction);
			if (obj != null && obj.ToString() != "" && int.Parse(obj.ToString()) > 0)
			{
				return false;
			}
			return true;
		}

		public DataSet GetPendingConsignmentsReport(string fromVendor, string toVendor, string vendorIDs)
		{
			try
			{
				if (fromVendor != "" || toVendor != "")
				{
					_ = " AND VendorID BETWEEN '" + fromVendor + "' AND '" + toVendor + "' ";
				}
				string text = "SELECT CI.SysDocID,CI.VoucherID,CI.VendorID,VendorName,TransactionDate\r\n                                    FROM Consign_In CI INNER JOIN Consign_In_Detail CID\r\n                                    ON CI.SysDocID = CID.SysDocID AND CI.VoucherID = CID.VoucherID \r\n                                    INNER JOIN Vendor V ON V.VendorID = CI.VendorID\r\n                                    WHERE ISNULL(IsClosed,'False') = 'False' ";
				if (fromVendor != "")
				{
					text = text + " AND CI.VendorID BETWEEN '" + fromVendor + "' AND '" + toVendor + "' ";
				}
				if (vendorIDs != "")
				{
					text = text + " AND CI.VendorID IN(" + vendorIDs + ")";
				}
				text += " ORDER BY TransactionDate ";
				DataSet dataSet = new DataSet();
				FillDataSet(dataSet, "Consign_In", text);
				DataSet dataSet2 = new DataSet();
				text = "SELECT CiD.SysDocID,CID.VoucherID,CID.Description,ProductID,ISNULL(Quantity,0) AS Quantity,ISNULL(QuantitySettled,0) AS QuantitySettled,ISNULL(QuantityReturned,0) AS QuantityReturned,\r\n                            ISNULL(QuantitySold,0) AS QuantitySold,Quantity-ISNULL(QuantitySettled,0)-ISNULL(QuantityReturned,0)-ISNULL(QuantitySold,0) AS BalanceQty\r\n                            FROM Consign_In CI INNER JOIN Consign_In_Detail CID\r\n                            ON CI.SysDocID = CID.SysDocID AND CI.VoucherID = CID.VoucherID \r\n                            WHERE ISNULL(IsClosed,'False') = 'False' ";
				if (fromVendor != "")
				{
					text = text + " AND CI.VendorID BETWEEN '" + fromVendor + "' AND '" + toVendor + "' ";
				}
				if (vendorIDs != "")
				{
					text = text + " AND CI.VendorID IN(" + vendorIDs + ")";
				}
				text += " ORDER BY TransactionDate";
				FillDataSet(dataSet2, "Consign_In_Detail", text);
				dataSet.Merge(dataSet2);
				dataSet.Relations.Add("Consign_REL", new DataColumn[2]
				{
					dataSet.Tables["Consign_In"].Columns["SysDocID"],
					dataSet.Tables["Consign_In"].Columns["VoucherID"]
				}, new DataColumn[2]
				{
					dataSet.Tables["Consign_In_Detail"].Columns["SysDocID"],
					dataSet.Tables["Consign_In_Detail"].Columns["VoucherID"]
				}, createConstraints: false);
				return dataSet;
			}
			catch
			{
				throw;
			}
		}

		public DataSet GetConsignmentInSettlementSummaryReport(string fromVendor, string toVendor, string vendorIDs)
		{
			try
			{
				string str = "";
				if (fromVendor != "" || toVendor != "")
				{
					str = " AND VendorID BETWEEN '" + fromVendor + "' AND '" + toVendor + "' ";
				}
				if (vendorIDs != "")
				{
					str = str + " AND CI.VendorID IN(" + vendorIDs + ")";
				}
				string text = "SELECT DISTINCT CI.SysDocID,CI.VoucherID,CI.VendorID,VendorName,TransactionDate\r\n                                FROM Consign_In CI INNER JOIN Consign_In_Detail CID\r\n                                ON CI.SysDocID = CID.SysDocID AND CI.VoucherID = CID.VoucherID \r\n                                INNER JOIN Vendor V ON V.VendorID = CI.VendorID\r\n                                WHERE ISNULL(IsClosed,'False') = 'False' AND ISNULL(Status,1) =   1 ";
				if (fromVendor != "")
				{
					text = text + " AND CI.VendorID BETWEEN '" + fromVendor + "' AND '" + toVendor + "' ";
				}
				if (vendorIDs != "")
				{
					text = text + " AND CI.VendorID IN(" + vendorIDs + ")";
				}
				text += " ORDER BY TransactionDate ";
				DataSet dataSet = new DataSet();
				FillDataSet(dataSet, "Consign_In", text);
				DataSet dataSet2 = new DataSet();
				text = "SELECT T.SysDocID,T.VoucherID,SUM(T.Quantity) AS [Con Qty],SUM(T.[Sold Quantity]) AS [Qty Sold], SUM(T.BalanceQty) AS [Balance],\r\n                        SUM(T.[Sold Amount]) AS [Invoice Amount]\r\n                        FROM (SELECT CID.SysDocID,CID.VoucherID,CID.Description,ProductID,ISNULL(Quantity,0) AS Quantity,ISNULL(QuantitySettled,0) AS QuantitySettled,\r\n                        ISNULL(QuantityReturned,0) AS QuantityReturned,ISNULL(QuantitySold,0) AS QuantitySold,Quantity-ISNULL(QuantitySettled,0)-ISNULL(QuantityReturned,0)-ISNULL(QuantitySold,0) AS BalanceQty,\r\n                        (SELECT SUM(PLS.SoldQty-PLS.FOCQuantity) \r\n                        FROM Product_Lot_Sales PLS INNER JOIN Inventory_Transactions IT ON PLS.DocID = IT.SysDocID AND PLS.InvoiceNumber=IT.VoucherID AND PLS.RowIndex=IT.RowIndex  WHERE LotNo IN \r\n                        (SELECT LotNumber FROM Product_Lot PL WHERE (ReceiptNumber=CID.VoucherID AND DocID =CID.SysDocID) OR SourceLotNumber IN \r\n                        (SELECT PL.LotNumber FROM Product_Lot PL WHERE (ReceiptNumber =CID.VoucherID AND DocID =CID.SysDocID))) AND PLS.ItemCode=CID.ProductID) AS [Sold Quantity],\r\n                        (SELECT SUM(IT.UnitPrice*(PLS.SoldQty-PLS.FOCQuantity)) \r\n                        FROM Product_Lot_Sales PLS INNER JOIN Inventory_Transactions IT ON PLS.DocID = IT.SysDocID AND PLS.InvoiceNumber=IT.VoucherID AND PLS.RowIndex=IT.RowIndex  WHERE LotNo IN \r\n                        (SELECT LotNumber FROM Product_Lot PL WHERE (ReceiptNumber=CID.VoucherID AND DocID =CID.SysDocID) OR SourceLotNumber IN \r\n                        (SELECT PL.LotNumber FROM Product_Lot PL WHERE (ReceiptNumber =CID.VoucherID AND DocID =CID.SysDocID))) AND PLS.ItemCode=CID.ProductID) AS [Sold Amount]\r\n                        FROM Consign_In CI INNER JOIN Consign_In_Detail CID\r\n                        ON CI.SysDocID = CID.SysDocID AND CI.VoucherID = CID.VoucherID \r\n                        WHERE ISNULL(IsClosed,'False') = 'False' AND ISNULL(Status,1) =  1   ";
				if (fromVendor != "")
				{
					text = text + " AND CI.VendorID BETWEEN '" + fromVendor + "' AND '" + toVendor + "' ";
				}
				if (vendorIDs != "")
				{
					text = text + " AND CI.VendorID IN(" + vendorIDs + ")";
				}
				text += " ) AS T GROUP BY T.SysDocID,T.VoucherID";
				FillDataSet(dataSet2, "Consign_In_Detail", text);
				dataSet.Merge(dataSet2);
				dataSet.Relations.Add("Consign_REL", new DataColumn[2]
				{
					dataSet.Tables["Consign_In"].Columns["SysDocID"],
					dataSet.Tables["Consign_In"].Columns["VoucherID"]
				}, new DataColumn[2]
				{
					dataSet.Tables["Consign_In_Detail"].Columns["SysDocID"],
					dataSet.Tables["Consign_In_Detail"].Columns["VoucherID"]
				}, createConstraints: false);
				return dataSet;
			}
			catch
			{
				throw;
			}
		}

		public DataSet GetConsignmentInSettlementDetailReport(string fromVendor, string toVendor, string vendorIDs)
		{
			try
			{
				string str = "";
				if (fromVendor != "" || toVendor != "")
				{
					str = " AND VendorID BETWEEN '" + fromVendor + "' AND '" + toVendor + "' ";
				}
				if (vendorIDs != "")
				{
					str = str + " AND VendorID IN(" + vendorIDs + ")";
				}
				string text = "SELECT DISTINCT CI.SysDocID,CI.VoucherID,CI.VendorID,VendorName,TransactionDate\r\n                                FROM Consign_In CI INNER JOIN Consign_In_Detail CID\r\n                                ON CI.SysDocID = CID.SysDocID AND CI.VoucherID = CID.VoucherID \r\n                                INNER JOIN Vendor V ON V.VendorID = CI.VendorID\r\n                                WHERE ISNULL(IsClosed,'False') = 'False' AND ISNULL(Status,1) =   1 ";
				if (fromVendor != "")
				{
					text = text + " AND CI.VendorID BETWEEN '" + fromVendor + "' AND '" + toVendor + "' ";
				}
				if (vendorIDs != "")
				{
					text = text + " AND CI.VendorID IN(" + vendorIDs + ")";
				}
				text += " ORDER BY TransactionDate ";
				DataSet dataSet = new DataSet();
				FillDataSet(dataSet, "Consign_In", text);
				DataSet dataSet2 = new DataSet();
				text = "SELECT CID.SysDocID,CID.VoucherID,CID.Description,ProductID,ISNULL(Quantity,0) AS Quantity,ISNULL(QuantitySettled,0) AS QuantitySettled,\r\n                        ISNULL(QuantityReturned,0) AS QuantityReturned,ISNULL(QuantitySold,0) AS QuantitySold,Quantity-ISNULL(QuantitySettled,0)-ISNULL(QuantityReturned,0)-ISNULL(QuantitySold,0) AS BalanceQty,\r\n                        (SELECT SUM(PLS.SoldQty-PLS.FOCQuantity) \r\n                        FROM Product_Lot_Sales PLS INNER JOIN Inventory_Transactions IT ON PLS.DocID = IT.SysDocID AND PLS.InvoiceNumber=IT.VoucherID AND PLS.RowIndex=IT.RowIndex  WHERE LotNo IN \r\n                        (SELECT LotNumber FROM Product_Lot PL WHERE (ReceiptNumber=CID.VoucherID AND DocID =CID.SysDocID) OR SourceLotNumber IN \r\n                        (SELECT PL.LotNumber FROM Product_Lot PL WHERE (ReceiptNumber =CID.VoucherID AND DocID =CID.SysDocID))) AND PLS.ItemCode=CID.ProductID) AS [Sold Quantity],\r\n                        (SELECT SUM(IT.UnitPrice*(PLS.SoldQty-PLS.FOCQuantity)) \r\n                        FROM Product_Lot_Sales PLS INNER JOIN Inventory_Transactions IT ON PLS.DocID = IT.SysDocID AND PLS.InvoiceNumber=IT.VoucherID AND PLS.RowIndex=IT.RowIndex  WHERE LotNo IN \r\n                        (SELECT LotNumber FROM Product_Lot PL WHERE (ReceiptNumber=CID.VoucherID AND DocID =CID.SysDocID) OR SourceLotNumber IN \r\n                        (SELECT PL.LotNumber FROM Product_Lot PL WHERE (ReceiptNumber =CID.VoucherID AND DocID =CID.SysDocID))) AND PLS.ItemCode=CID.ProductID) AS [Sold Amount]\r\n                        FROM Consign_In CI INNER JOIN Consign_In_Detail CID\r\n                        ON CI.SysDocID = CID.SysDocID AND CI.VoucherID = CID.VoucherID \r\n                        WHERE ISNULL(IsClosed,'False') = 'False' AND ISNULL(Status,1) =  1 ";
				if (fromVendor != "")
				{
					text = text + " AND CI.VendorID BETWEEN '" + fromVendor + "' AND '" + toVendor + "' ";
				}
				if (vendorIDs != "")
				{
					text = text + " AND CI.VendorID IN(" + vendorIDs + ")";
				}
				text += " ORDER BY TransactionDate";
				FillDataSet(dataSet2, "Consign_In_Detail", text);
				dataSet.Merge(dataSet2);
				dataSet.Relations.Add("Consign_REL", new DataColumn[2]
				{
					dataSet.Tables["Consign_In"].Columns["SysDocID"],
					dataSet.Tables["Consign_In"].Columns["VoucherID"]
				}, new DataColumn[2]
				{
					dataSet.Tables["Consign_In_Detail"].Columns["SysDocID"],
					dataSet.Tables["Consign_In_Detail"].Columns["VoucherID"]
				}, createConstraints: false);
				return dataSet;
			}
			catch
			{
				throw;
			}
		}

		public DataSet GetConsignInComboList()
		{
			DataSet dataSet = new DataSet();
			string textCommand = "SELECT SysDocID + '#' + VoucherID AS  [Code], ContainerNo [Name]\r\n                           FROM Consign_In WHERE ISNULL(IsClosed,'False')='False' ORDER BY SysDocID,VoucherID";
			FillDataSet(dataSet, "Vehicle", textCommand);
			return dataSet;
		}

		public DataSet GetConsignInClosingSummary(string sysDocID, string voucherID)
		{
			return GetConsignInClosingSummary(sysDocID, voucherID, null);
		}

		public DataSet GetConsignInClosingSummary(string sysDocID, string voucherID, SqlTransaction sqlTransaction)
		{
			try
			{
				DataSet dataSet = new DataSet();
				string textCommand = "SELECT SysDocID,VoucherID,CID.ProductID, P.Description,CID.Quantity  AS QuantityReceived,ISNULL(QuantityReturned,0) AS QuantityReturned, QuantitySettled ,CID.Quantity-ISNULL(QuantityReturned,0) - ISNULL(QuantitySettled,0) AS BalanceQuantity,\r\n                                (SELECT SUM(SoldQty*UnitPrice) AS Amount FROM Product_Lot_Sales  PLS WHERE  CID.ProductID = PLS.ItemCode  AND LOTNO IN (SELECT LOTNumber FROM Product_Lot WHERE DocID = CID.SysDocID AND ReceiptNumber = CID.VoucherID\r\n                            UNION SELECT LotNumber FROM Product_Lot WHERE SourceLotNumber IN (SELECT LOTNumber FROM Product_Lot WHERE DocID = CID.SysDocID AND ReceiptNumber = CID.VoucherID))) AS ActualSales,\r\n\r\n                                (SELECT SUM(Amount) AS Amount FROM ConsignIn_Settlement_Detail CSD WHERE CSD.ProductID = CID.ProductID AND CSD.ConsignRowIndex = CID.RowIndex AND CSD.ConsignSysDocID = CID.SysDocID AND CSD.ConsignVoucherID =CID.VoucherID) AS SettledSales \r\n                                 FROM Consign_In_Detail CID INNER JOIN Product P ON CID.ProductID = P.ProductID WHERE SysDocID = '" + sysDocID + "' AND VoucherID = '" + voucherID + "'";
				FillDataSet(dataSet, "Items", textCommand, sqlTransaction);
				textCommand = "SELECT JD.JournalDetailID,JD.AccountID,ISNULL(Debit,0) AS Amount, (SELECT SUM(Amount) \r\n                        FROM ConsignIn_Expense CX WHERE CX.SourceRowIndex = JD.JournalDetailID) AS Billed\r\n                        FROM Journal_Details JD WHERE JD.ConsignID = '" + sysDocID + "#" + voucherID + "' AND JD.DEBIT IS NOT NULL";
				FillDataSet(dataSet, "Bills", textCommand, sqlTransaction);
				textCommand = "SELECT DISTINCT CI.VendorID,Ven.VendorName,TransactionDate,CloseDate,Status ,DivisionID,CompanyID,\r\n                        (SELECT SUM(SoldQty*UnitPrice) AS Amount FROM Product_Lot_Sales  PLS WHERE  LOTNO IN \r\n                            (SELECT LOTNumber FROM Product_Lot WHERE DocID = CI.SysDocID AND ReceiptNumber = CI.VoucherID\r\n                            UNION SELECT LotNumber FROM Product_Lot WHERE SourceLotNumber IN (SELECT LOTNumber FROM Product_Lot WHERE DocID = CI.SysDocID AND ReceiptNumber = CI.VoucherID)) ) AS ActualSales,\r\n                        (SELECT SUM(Amount) AS Amount FROM ConsignIn_Settlement_Detail CSD WHERE   CSD.ConsignSysDocID = CID.SysDocID AND CSD.ConsignVoucherID =CID.VoucherID) AS SettledSales,\r\n                        (SELECT SUM(CommissionAmount) AS Commission FROM ConsignIn_Settlement CIS WHERE CIS.ConsignSysDocID = CI.SysDocID AND CIS.ConsignVoucherID = CI.VoucherID) AS Commission,\r\n                        (SELECT SUM(Debit) AS Amount FROM Journal_Details JD WHERE Debit IS NOT NULL AND ConsignID = CI.SysDocID + '#' + CI.VoucherID  ) AS Expenses,\r\n                        (select SUM(Amount) FROM ConsignIn_Expense CX INNER JOIN ConsignIn_Settlement CS ON CS.SysDocID = CX.SysDocID AND CS.VoucherID = CX.VoucherID where CS.ConsignSysDocID = '" + sysDocID + "' AND CS.ConsignVoucherID = '" + voucherID + "') AS Billed\r\n                           FROM Consign_In CI INNER JOIN Consign_In_Detail CID ON CI.SysDocID = CID.SysDocID AND CI.VoucherID = CID.VoucherID INNER JOIN\r\n\t                        Vendor VEN ON  CI.VendorID = VEN.VendorID WHERE CI.Sysdocid = '" + sysDocID + "' AND CI.VoucherID = '" + voucherID + "'";
				FillDataSet(dataSet, "Consign_In", textCommand, sqlTransaction);
				return dataSet;
			}
			catch
			{
				throw;
			}
		}

		public DataSet GetConsignInReport(DateTime from, DateTime to, string fromVendor, string toVendor, string fromClass, string toClass, string fromGroup, string toGroup)
		{
			CommonLib.ToSqlDateTimeString(from);
			CommonLib.ToSqlDateTimeString(to);
			string empty = string.Empty;
			DataSet dataSet = new DataSet();
			empty = " SELECT VendorName, VA.Address1  FROM Consign_In CI\r\n\t                    INNER JOIN Vendor V ON CI.VendorID = V.VendorID\r\n\t                    INNER JOIN Vendor_Address VA ON V.VendorID = VA.VendorID \r\n                                WHERE 1=1 ";
			if (fromVendor != "")
			{
				empty = empty + " AND CI.VendorID >= '" + fromVendor + "'";
			}
			if (toVendor != "")
			{
				empty = empty + " AND CI.VendorID <= '" + toVendor + "'";
			}
			if (fromClass != "")
			{
				empty = empty + " AND CI.VendorID IN (SELECT VendorID FROM Vendor WHERE VendorClassID >= '" + fromClass + "') ";
			}
			if (toClass != "")
			{
				empty = empty + " AND CI.VendorID IN (SELECT VendorID FROM Vendor WHERE VendorClassID <= '" + toClass + "') ";
			}
			if (fromGroup != "")
			{
				empty = empty + " AND CI.VendorID IN (SELECT VendorID FROM Vendor WHERE VendorGroupID >= '" + fromGroup + "') ";
			}
			if (toGroup != "")
			{
				empty = empty + " AND CI.VendorID IN (SELECT VendorID FROM Vendor WHERE VendorGroupID <= '" + toGroup + "') ";
			}
			empty += " ORDER BY CI.VendorID ";
			FillDataSet(dataSet, "Vendor", empty);
			empty = " SELECT CE.Description,CS.Transactiondate,CE.VoucherID,CE.Amount \r\n                        FROM ConsignIn_Expense CE, ConsignIn_Settlement CS, Consign_In CI\r\n                        AND  CE.VoucherID=CS.VoucherID and CS.ConsignVoucherID=CI.VoucherID WHERE 1=1 ";
			if (fromVendor != "")
			{
				empty = empty + " AND CI.VendorID >= '" + fromVendor + "'";
			}
			if (toVendor != "")
			{
				empty = empty + " AND CI.VendorID <= '" + toVendor + "'";
			}
			if (fromClass != "")
			{
				empty = empty + " AND CI.VendorID IN (SELECT VendorID FROM Vendor WHERE VendorClassID >= '" + fromClass + "') ";
			}
			if (toClass != "")
			{
				empty = empty + " AND CI.VendorID IN (SELECT VendorID FROM Vendor WHERE VendorClassID <= '" + toClass + "') ";
			}
			if (fromGroup != "")
			{
				empty = empty + " AND CI.VendorID IN (SELECT VendorID FROM Vendor WHERE VendorGroupID >= '" + fromGroup + "') ";
			}
			if (toGroup != "")
			{
				empty = empty + " AND CI.VendorID IN (SELECT VendorID FROM Vendor WHERE VendorGroupID <= '" + toGroup + "') ";
			}
			empty += " ORDER BY CI.VendorID ";
			FillDataSet(dataSet, "ConsignIn_Expense", empty);
			empty = " SELECT Product_Lot.LotNumber,Product_Lot.ItemCode,Product_Lot.LotQty, SUM(Delivery_Note_Detail.Quantity) as [Quanity Delivered], SUM(Sales_Invoice_Detail.FOCQuantity) as FOC,\r\n                        SUM((Sales_Invoice_Detail.Quantity - Sales_Invoice_Detail.FOCQuantity)) As [Quantity Invoiced] ,\r\n                        SUM(((Sales_Invoice_Detail.Quantity - Sales_Invoice_Detail.FOCQuantity)*(Sales_Invoice_Detail.UnitPrice)))\r\n                     FROM Product_Lot_Sales\r\n                     LEFT JOIN Delivery_Note_Detail on Product_Lot_Sales.InvoiceNumber=Delivery_Note_Detail.VoucherID and Product_Lot_Sales.DocID=Delivery_Note_Detail.SysDocID\r\n                     LEFT JOIN Sales_Invoice_Detail on Delivery_Note_Detail.VoucherID=Sales_Invoice_Detail.OrderVoucherID and Delivery_Note_Detail.SysDocID=Sales_Invoice_Detail.OrderSysDocID\r\n                    LEFT JOIN  Product_Lot on Product_Lot_Sales.LotNo=Product_Lot.LotNumber  WHERE 1=1";
			if (fromVendor != "")
			{
				empty = empty + " AND CI.VendorID >= '" + fromVendor + "'";
			}
			if (toVendor != "")
			{
				empty = empty + " AND CI.VendorID <= '" + toVendor + "'";
			}
			if (fromClass != "")
			{
				empty = empty + " AND CI.VendorID IN (SELECT VendorID FROM Vendor WHERE VendorClassID >= '" + fromClass + "') ";
			}
			if (toClass != "")
			{
				empty = empty + " AND CI.VendorID IN (SELECT VendorID FROM Vendor WHERE VendorClassID <= '" + toClass + "') ";
			}
			if (fromGroup != "")
			{
				empty = empty + " AND CI.VendorID IN (SELECT VendorID FROM Vendor WHERE VendorGroupID >= '" + fromGroup + "') ";
			}
			if (toGroup != "")
			{
				empty = empty + " AND CI.VendorID IN (SELECT VendorID FROM Vendor WHERE VendorGroupID <= '" + toGroup + "') ";
			}
			empty += " GROUP BY  Product_Lot.LotNumber,Product_Lot.ItemCode,Product_Lot.LotQty ";
			empty += " ORDER BY CI.VendorID ";
			FillDataSet(dataSet, "Consign_In", empty);
			empty = " SELECT Delivery_Note.TransactionDate,Delivery_Note.VoucherID,Consign_In.VendorID,Sales_Invoice.VoucherID AS InvoiceVoucherID,\r\n                        Sales_Invoice.TransactionDate,Sales_Invoice.SysDocID,Customer.CustomerName, Delivery_Note_Detail.Quantity AS [Quanity Delivered], Sales_Invoice_Detail.FOCQuantity AS FOC,\r\n                        (Sales_Invoice_Detail.Quantity - Sales_Invoice_Detail.FOCQuantity) AS [Quantity Invoiced] ,(Sales_Invoice_Detail.UnitPrice) AS [Unit Price],\r\n                        ((Sales_Invoice_Detail.Quantity - Sales_Invoice_Detail.FOCQuantity)*(Sales_Invoice_Detail.UnitPrice)) AS [ Amount ] FROM Product_Lot_Sales \r\n                        LEFT JOIN Delivery_Note_Detail ON Product_Lot_Sales.InvoiceNumber=Delivery_Note_Detail.VoucherID and Product_Lot_Sales.DocID=Delivery_Note_Detail.SysDocID\r\n                    LEFT JOIN Sales_Invoice_Detail ON Delivery_Note_Detail.VoucherID=Sales_Invoice_Detail.OrderVoucherID and Delivery_Note_Detail.SysDocID=Sales_Invoice_Detail.OrderSysDocID\r\n                    LEFT JOIN  Product_Lot ON Product_Lot_Sales.LotNo=Product_Lot.LotNumber \r\n                    LEFT JOIN Delivery_Note ON Delivery_Note.VoucherID=Delivery_Note_Detail.VoucherID and Delivery_Note.SysDocID=Delivery_Note_Detail.SysDocID\r\n                    LEFT JOIN Sales_Invoice ON Sales_Invoice.VoucherID=Sales_Invoice_Detail.VoucherID and Sales_Invoice.SysDocID=Sales_Invoice_Detail.SysDocID\r\n                    LEFT JOIN Customer ON Customer.CustomerID=Sales_Invoice.CustomerID\r\n                    LEFT JOIN Consign_In ON Consign_In.VoucherID= Product_Lot.ReceiptNumber AND Consign_In.SysDocID = Product_Lot.DocID    WHERE 1=1";
			if (fromVendor != "")
			{
				empty = empty + " AND CI.VendorID >= '" + fromVendor + "'";
			}
			if (toVendor != "")
			{
				empty = empty + " AND CI.VendorID <= '" + toVendor + "'";
			}
			if (fromClass != "")
			{
				empty = empty + " AND CI.VendorID IN (SELECT VendorID FROM Vendor WHERE VendorClassID >= '" + fromClass + "') ";
			}
			if (toClass != "")
			{
				empty = empty + " AND CI.VendorID IN (SELECT VendorID FROM Vendor WHERE VendorClassID <= '" + toClass + "') ";
			}
			if (fromGroup != "")
			{
				empty = empty + " AND CI.VendorID IN (SELECT VendorID FROM Vendor WHERE VendorGroupID >= '" + fromGroup + "') ";
			}
			if (toGroup != "")
			{
				empty = empty + " AND CI.VendorID IN (SELECT VendorID FROM Vendor WHERE VendorGroupID <= '" + toGroup + "') ";
			}
			empty += " GROUP BY  Product_Lot.LotNumber,Product_Lot.ItemCode,Product_Lot.LotQty ";
			empty += " ORDER BY CI.VendorID ";
			FillDataSet(dataSet, "Consign_In_Detail", empty);
			return dataSet;
		}

		public DataSet GetConsignInList(string sysDocID, DateTime from, DateTime to)
		{
			string text = CommonLib.ToSqlDateTimeString(from);
			string text2 = CommonLib.ToSqlDateTimeString(to);
			DataSet dataSet = new DataSet();
			string str = "SELECT SysDocID [Doc ID], VoucherID [Number], TransactionDate AS [Date],Ven.VendorName AS [Vendor],Con.ContainerNo AS [Container#]\r\n                            FROM Consign_In CON INNER JOIN Vendor VEN ON Con.VendorID = Ven.VendorID\r\n                            WHERE ISNULL(IsVoid,'False')='False'  AND TransactionDate BETWEEN '" + text + "' AND '" + text2 + "' ";
			if (!string.IsNullOrEmpty(sysDocID))
			{
				str = str + " AND SysDocID='" + sysDocID + "'";
			}
			str += " ORDER BY TransactionDate, VoucherID ";
			FillDataSet(dataSet, "Consign_In", str);
			return dataSet;
		}

		public DataSet GetConsignInItemMovementReport(DateTime from, DateTime to, string fromItem, string toItem, string fromItemClass, string toItemClass, string fromCategory, string toCategory, string fromLocationID, string toLocationID, string sysDocID, string voucherID, string fromManufacturer, string toManufacturer, string fromStyle, string toStyle, string fromOrigin, string toOrigin)
		{
			try
			{
				CommonLib.ToSqlDateTimeString(from);
				CommonLib.ToSqlDateTimeString(to);
				string text = "";
				DataSet dataSet = new DataSet();
				string textCommand = "SELECT CI.*,V.VendorName,T.TransporterName,P.PortName FROM Consign_In CI \r\n                                LEFT JOIN Vendor V ON CI.VendorID=V.VendorID\r\n                                LEFT JOIN Transporter T ON CI.TransporterID=T.TransporterID\r\n                                LEFT JOIN Port P ON CI.ArrivalPort=P.PortID\r\n                                WHERE VoucherID='" + voucherID + "' AND SysDocID='" + sysDocID + "'";
				FillDataSet(dataSet, "Consign_In", textCommand);
				textCommand = "SELECT TransactionDate FROM Consign_In WHERE SysDocID = '" + sysDocID + "' AND VoucherID = '" + voucherID + "'";
				object obj = ExecuteScalar(textCommand);
				if (obj != null)
				{
					DateTime date = DateTime.Parse(obj.ToString()).AddDays(-30.0);
					text = StoreConfiguration.ToSqlDateTimeString(date);
				}
				textCommand = " (SELECT LotNumber into #tmp1 FROM Product_Lot PL WHERE (ReceiptNumber = '" + voucherID + "' AND DocID = '" + sysDocID + "')\r\n                             OR SourceLotNumber IN (SELECT PL.LotNumber FROM Product_Lot PL WHERE (ReceiptNumber = '" + voucherID + "' AND DocID = '" + sysDocID + "')))\r\n\r\nSELECT '" + sysDocID + "' AS DocID, '" + voucherID + "' AS ReceiptNumber, CON.CustomerID,Con.CustomerName,CON.Description,COn.DocName,\r\n                              COn.LocationID, CON.ProductID AS ItemCode,CON.SysDocID,CON.SysDocType,CON.TransactionDate,CON.UnitPrice AS [Unit Price], CON.QuantityIn,CON.QuantityOut,\r\n                               CON.VoucherID  FROM \r\n\r\n\r\n                               (SELECT PLD.*,P.Description, IT.TransactionDate,CASE WHEN IT.PayeeType = 'C' THEN IT.PayeeID ELSE NULL END AS CustomerID ,\r\n                               CUS.CustomerName, \r\n                                CASE WHEN INV.TransactionDate IS NULL THEN 0 ELSE INV.UnitPrice END AS UnitPrice,\r\n\r\n                                ISNULL(QtyIn,0) AS QuantityIn, ISNULL(QtyOut,0) AS QuantityOut\r\n                                 FROM ( \r\n\r\n\r\n                                 SELECT SD.SysDocType,SD.DocName, PLS.DocID AS SysDocID, InvoiceNumber AS VoucherID,RowIndex,ItemCode AS ProductID,PLS.LocationID,LotNo AS LotNumber, 0 AS QtyIn, SoldQty AS QtyOut\r\n                                 FROM Product_Lot_Sales PLS INNER JOIN System_Document SD ON PLS.DocID = SD.SysDocID WHERE LotNo IN (SELECT LotNumber  from #tmp1)\r\n\r\n                                UNION\r\n                            SELECT SD.SysDocType,SD.DocName,PLS.SysDocID, VoucherID,RowIndex, ProductID,PLS.LocationID,SourceLotNumber AS  LotNumber,LotQty AS QtyIN,0 AS QtyOut\r\n                             FROM Product_Lot_Receiving_Detail PLS INNER JOIN System_Document SD ON PLS.SysDocID = SD.SysDocID WHERE SourceLotNumber IN (SELECT LotNumber  from #tmp1)\r\n                           \r\n\r\n                 UNION \r\n                 SELECT SD.SysDocType,SD.DocName,PL.DocID as sysdocid, ReceiptNumber as voucherid,RowIndex, itemcode as ProductID,PL.LocationID,SourceLotNumber AS  LotNumber,LotQty AS QtyIN,0 AS QtyOut FROM Product_Lot PL\r\n                 INNER JOIN System_Document SD ON PL.DocID = SD.SysDocID WHERE ReceiptNumber = '" + voucherID + "' AND DocID = '" + sysDocID + "'\r\n\r\n                             )  AS PLD\r\n\r\n                              LEFT OUTER JOIN Inventory_Transactions IT ON IT.SysDocID = PLD.SysDocID AND IT.VoucherID = PLD.VoucherID AND IT.RowIndex = PLD.RowIndex AND IT.LocationID = PLD.LocationID\r\n                            LEFT OUTER JOIN  (SELECT SID2.Quantity,SID2.OrderSysDocID,SID2.VoucherID,SID2.OrderVoucherID,SID2.OrderRowIndex,SID2.UnitPrice,SI2.TransactionDate\r\n                FROM Sales_Invoice_Detail SID2 INNER JOIN Sales_INVOICE SI2 ON SID2.SysDocID = SI2.SysDocID AND SID2.VoucherID = SI2.VoucherID AND ISNULL(SI2.IsVoid,'False') = 'False' AND SI2.TransactionDate>='" + text + "' ) AS INV ON INV.OrderSysDocID = PLD.SysDocID\r\n                AND INV.OrderVoucherID = PLD.VoucherID AND INV.OrderRowIndex = PLD.RowIndex\r\n                               LEFT OUTER JOIN Delivery_Return DR ON DR.DNoteSysDocID = PLD.SysDocID AND DR.DNoteVoucherID = PLD.VoucherID  AND ISNULL(DR.IsVoid,'False') = 'False'\r\n                             LEFT OUTER JOIN Delivery_Return_Detail DRD ON DRD.SysDocID = DR.SysDocID AND DRD.VoucherID = DR.VoucherID AND DRD.DNRowIndex = PLD.RowIndex\r\n                              LEFT OUTER JOIN Sales_Return_Detail  SRD ON SRD.SourceSysDocID = PLD.SysDocID AND SRD.SourceVoucherID = PLD.VoucherID AND SRD.SourceRowIndex = PLD.RowIndex   \r\n                               LEFT OUTER JOIN Product P ON P.ProductID = PLD.ProductID\r\n                                  LEFT OUTER JOIN Customer CUS ON CUS.CUstomerID = IT.PayeeID AND IT.PayeeType = 'C'\r\n                                   ) CON\r\n                                WHERE 1=1 ";
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
				if (fromManufacturer != "")
				{
					textCommand = textCommand + " AND CON.ProductID IN (SELECT ProductID FROM Product WHERE ManufacturerID BETWEEN '" + fromManufacturer + "' AND '" + toManufacturer + "') ";
				}
				if (fromStyle != "")
				{
					textCommand = textCommand + " AND CON.ProductID IN (SELECT ProductID FROM Product WHERE StyleID BETWEEN '" + fromStyle + "' AND '" + toStyle + "') ";
				}
				if (fromOrigin != "")
				{
					textCommand = textCommand + " AND CON.ProductID IN (SELECT ProductID FROM Product WHERE Origin BETWEEN '" + fromOrigin + "' AND '" + toOrigin + "') ";
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
				textCommand += "drop table #tmp1";
				FillDataSet(dataSet, "Consign_In_Detail", textCommand);
				dataSet.Relations.Add("Relation", new DataColumn[2]
				{
					dataSet.Tables["Consign_In"].Columns["SysDocID"],
					dataSet.Tables["Consign_In"].Columns["VoucherID"]
				}, new DataColumn[2]
				{
					dataSet.Tables["Consign_In_Detail"].Columns["DocID"],
					dataSet.Tables["Consign_In_Detail"].Columns["ReceiptNumber"]
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
