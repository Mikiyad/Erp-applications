using Micromind.Common;
using Micromind.Common.Data;
using Micromind.Common.Libraries;
using System;
using System.Collections;
using System.Data;
using System.Data.SqlClient;

namespace Micromind.Data
{
	public sealed class W3PLDelivery : StoreObject
	{
		private const string W3PLDELIVERY_TABLE = "W3PL_Delivery";

		private const string W3PLDELIVERYDETAIL_TABLE = "W3PL_Delivery_Detail";

		private const string SYSDOCID_PARM = "@SysDocID";

		private const string VOUCHERID_PARM = "@VoucherID";

		private const string COMPANYID_PARM = "@CompanyID";

		private const string DIVISIONID_PARM = "@DivisionID";

		private const string CUSTOMERID_PARM = "@CustomerID";

		private const string SALESFLOW_PARM = "@SalesFlow";

		private const string TRANSACTIONDATE_PARM = "@TransactionDate";

		private const string SALESPERSONID_PARM = "@SalespersonID";

		private const string ISEXPORT_PARM = "@IsExport";

		private const string ROWINDEX_PARM = "@RowIndex";

		private const string REQUIREDDATE_PARM = "@RequiredDate";

		private const string SHIPPINGADDRESSID_PARM = "@ShippingAddressID";

		private const string CUSTOMERADDRESS_PARM = "@CustomerAddress";

		private const string STATUS_PARM = "@Status";

		private const string CURRENCYID_PARM = "@CurrencyID";

		private const string TERMID_PARM = "@TermID";

		private const string SHIPPINGMETHODID_PARM = "@ShippingMethodID";

		private const string REFERENCE_PARM = "@Reference";

		private const string REFERENCE2_PARM = "@Reference2";

		private const string NOTE_PARM = "@Note";

		private const string ISVOID_PARM = "@IsVoid";

		private const string PONUMBER_PARM = "@PONumber";

		private const string DISCOUNT_PARM = "@Discount";

		private const string TOTAL_PARM = "@Total";

		private const string SOURCEDOCTYPE_PARM = "@SourceDocType";

		private const string DRIVERID_PARM = "@DriverID";

		private const string VEHICLEID_PARM = "@VehicleID";

		private const string CONTAINERNO_PARM = "@ContainerNumber";

		private const string CONTAINERSIZEID_PARM = "@ContainerSizeID";

		private const string INVOICESYSDOCID_PARM = "@InvoiceSysDocID";

		private const string INVOICEVOUCHERID_PARM = "@InvoiceVoucherID";

		private const string DNOTESYSDOCID_PARM = "@DNoteSysDocID";

		private const string DNOTEVOUCHERID_PARM = "@DNoteVoucherID";

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

		private const string ORDERVOUCHERID_PARM = "@OrderVoucherID";

		private const string SOURCESYSDOCID_PARM = "@SourceSysDocID";

		private const string SOURCEVOUCHERID_PARM = "@SourceVoucherID";

		private const string SOURCEROWINDEX_PARM = "@SourceRowIndex";

		private const string ROWSOURCE_PARM = "@RowSource";

		private const string CREATEDBY_PARM = "@CreatedBy";

		private const string DATECREATED_PARM = "@DateCreated";

		private const string UPDATEDBY_PARM = "@UpdatedBy";

		private const string DATEUPDATED_PARM = "@DateUpdated";

		public W3PLDelivery(Config config)
			: base(config)
		{
		}

		private string GetInsertUpdateW3PLDeliveryText(bool isUpdate)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddInsertUpdateParameters("W3PL_Delivery", new FieldValue("SysDocID", "@SysDocID", isUpdateConditionField: true), new FieldValue("VoucherID", "@VoucherID", isUpdateConditionField: true), new FieldValue("DivisionID", "@DivisionID"), new FieldValue("CompanyID", "@CompanyID"), new FieldValue("CustomerID", "@CustomerID"), new FieldValue("SalesFlow", "@SalesFlow"), new FieldValue("IsExport", "@IsExport"), new FieldValue("TransactionDate", "@TransactionDate"), new FieldValue("SalespersonID", "@SalespersonID"), new FieldValue("RequiredDate", "@RequiredDate"), new FieldValue("ShippingAddressID", "@ShippingAddressID"), new FieldValue("ShippingMethodID", "@ShippingMethodID"), new FieldValue("CustomerAddress", "@CustomerAddress"), new FieldValue("Status", "@Status"), new FieldValue("CurrencyID", "@CurrencyID"), new FieldValue("Discount", "@Discount"), new FieldValue("Total", "@Total"), new FieldValue("PONumber", "@PONumber"), new FieldValue("TermID", "@TermID"), new FieldValue("Reference", "@Reference"), new FieldValue("Reference2", "@Reference2"), new FieldValue("SourceDocType", "@SourceDocType"), new FieldValue("Note", "@Note"), new FieldValue("DriverID", "@DriverID"), new FieldValue("VehicleID", "@VehicleID"), new FieldValue("ContainerNumber", "@ContainerNumber"), new FieldValue("ContainerSizeID", "@ContainerSizeID"));
			if (isUpdate)
			{
				sqlBuilder.AddInsertUpdateParameters("W3PL_Delivery", new FieldValue("DateUpdated", "@DateUpdated", isUpdateConditionField: true, checkForNullValue: true));
			}
			if (isUpdate)
			{
				return sqlBuilder.GetUpdateExpression();
			}
			return sqlBuilder.GetInsertExpression();
		}

		private SqlCommand GetInsertUpdateW3PLDeliveryCommand(bool isUpdate)
		{
			SqlParameterCollection parameters;
			if (isUpdate)
			{
				if (updateCommand != null)
				{
					updateCommand = null;
				}
				updateCommand = new SqlCommand(GetInsertUpdateW3PLDeliveryText(isUpdate: true), base.DBConfig.Connection);
				updateCommand.CommandType = CommandType.Text;
				parameters = updateCommand.Parameters;
			}
			else
			{
				if (insertCommand != null)
				{
					insertCommand = null;
				}
				insertCommand = new SqlCommand(GetInsertUpdateW3PLDeliveryText(isUpdate: false), base.DBConfig.Connection);
				insertCommand.CommandType = CommandType.Text;
				parameters = insertCommand.Parameters;
			}
			parameters.Add("@SysDocID", SqlDbType.NVarChar);
			parameters.Add("@VoucherID", SqlDbType.NVarChar);
			parameters.Add("@DivisionID", SqlDbType.NVarChar);
			parameters.Add("@CompanyID", SqlDbType.TinyInt);
			parameters.Add("@CustomerID", SqlDbType.NVarChar);
			parameters.Add("@SalesFlow", SqlDbType.TinyInt);
			parameters.Add("@IsExport", SqlDbType.Bit);
			parameters.Add("@TransactionDate", SqlDbType.DateTime);
			parameters.Add("@SalespersonID", SqlDbType.NVarChar);
			parameters.Add("@RequiredDate", SqlDbType.DateTime);
			parameters.Add("@ShippingAddressID", SqlDbType.NVarChar);
			parameters.Add("@CustomerAddress", SqlDbType.NVarChar);
			parameters.Add("@ShippingMethodID", SqlDbType.NVarChar);
			parameters.Add("@Status", SqlDbType.TinyInt);
			parameters.Add("@CurrencyID", SqlDbType.NVarChar);
			parameters.Add("@TermID", SqlDbType.NVarChar);
			parameters.Add("@Reference", SqlDbType.NVarChar);
			parameters.Add("@Reference2", SqlDbType.NVarChar);
			parameters.Add("@PONumber", SqlDbType.NVarChar);
			parameters.Add("@Discount", SqlDbType.Decimal);
			parameters.Add("@Total", SqlDbType.Decimal);
			parameters.Add("@Note", SqlDbType.NVarChar);
			parameters.Add("@SourceDocType", SqlDbType.TinyInt);
			parameters.Add("@DriverID", SqlDbType.NVarChar);
			parameters.Add("@VehicleID", SqlDbType.NVarChar);
			parameters.Add("@ContainerNumber", SqlDbType.NVarChar);
			parameters.Add("@ContainerSizeID", SqlDbType.NVarChar);
			parameters["@SysDocID"].SourceColumn = "SysDocID";
			parameters["@VoucherID"].SourceColumn = "VoucherID";
			parameters["@DivisionID"].SourceColumn = "DivisionID";
			parameters["@CompanyID"].SourceColumn = "CompanyID";
			parameters["@CustomerID"].SourceColumn = "CustomerID";
			parameters["@IsExport"].SourceColumn = "IsExport";
			parameters["@SalesFlow"].SourceColumn = "SalesFlow";
			parameters["@TransactionDate"].SourceColumn = "TransactionDate";
			parameters["@SalespersonID"].SourceColumn = "SalespersonID";
			parameters["@RequiredDate"].SourceColumn = "RequiredDate";
			parameters["@ShippingAddressID"].SourceColumn = "ShippingAddressID";
			parameters["@CustomerAddress"].SourceColumn = "CustomerAddress";
			parameters["@ShippingMethodID"].SourceColumn = "ShippingMethodID";
			parameters["@Status"].SourceColumn = "Status";
			parameters["@CurrencyID"].SourceColumn = "CurrencyID";
			parameters["@TermID"].SourceColumn = "TermID";
			parameters["@Reference"].SourceColumn = "Reference";
			parameters["@Reference2"].SourceColumn = "Reference2";
			parameters["@PONumber"].SourceColumn = "PONumber";
			parameters["@Discount"].SourceColumn = "Discount";
			parameters["@Total"].SourceColumn = "Total";
			parameters["@Note"].SourceColumn = "Note";
			parameters["@SourceDocType"].SourceColumn = "SourceDocType";
			parameters["@DriverID"].SourceColumn = "DriverID";
			parameters["@VehicleID"].SourceColumn = "VehicleID";
			parameters["@ContainerNumber"].SourceColumn = "ContainerNumber";
			parameters["@ContainerSizeID"].SourceColumn = "ContainerSizeID";
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

		private string GetInsertUpdateW3PLDeliveryDetailsText(bool isUpdate)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddInsertUpdateParameters("W3PL_Delivery_Detail", new FieldValue("SysDocID", "@SysDocID"), new FieldValue("VoucherID", "@VoucherID"), new FieldValue("ProductID", "@ProductID"), new FieldValue("LocationID", "@LocationID"), new FieldValue("RowIndex", "@RowIndex"), new FieldValue("Quantity", "@Quantity"), new FieldValue("UnitPrice", "@UnitPrice"), new FieldValue("Description", "@Description"), new FieldValue("UnitID", "@UnitID"), new FieldValue("UnitQuantity", "@UnitQuantity"), new FieldValue("UnitFactor", "@UnitFactor"), new FieldValue("FactorType", "@FactorType"), new FieldValue("SubunitPrice", "@SubunitPrice"), new FieldValue("RowSource", "@RowSource"), new FieldValue("SourceSysDocID", "@SourceSysDocID"), new FieldValue("SourceVoucherID", "@SourceVoucherID"), new FieldValue("SourceRowIndex", "@SourceRowIndex"));
			if (isUpdate)
			{
				return sqlBuilder.GetUpdateExpression();
			}
			return sqlBuilder.GetInsertExpression();
		}

		private SqlCommand GetInsertUpdateW3PLDeliveryDetailsCommand(bool isUpdate)
		{
			SqlParameterCollection parameters;
			if (isUpdate)
			{
				if (updateCommand != null)
				{
					updateCommand = null;
				}
				updateCommand = new SqlCommand(GetInsertUpdateW3PLDeliveryDetailsText(isUpdate: true), base.DBConfig.Connection);
				updateCommand.CommandType = CommandType.Text;
				parameters = updateCommand.Parameters;
			}
			else
			{
				if (insertCommand != null)
				{
					insertCommand = null;
				}
				insertCommand = new SqlCommand(GetInsertUpdateW3PLDeliveryDetailsText(isUpdate: false), base.DBConfig.Connection);
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
			parameters.Add("@RowSource", SqlDbType.Int);
			parameters.Add("@SourceSysDocID", SqlDbType.NVarChar);
			parameters.Add("@SourceVoucherID", SqlDbType.NVarChar);
			parameters.Add("@SourceRowIndex", SqlDbType.Int);
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
			parameters["@SourceVoucherID"].SourceColumn = "SourceVoucherID";
			parameters["@SourceSysDocID"].SourceColumn = "SourceSysDocID";
			parameters["@SourceRowIndex"].SourceColumn = "SourceRowIndex";
			parameters["@RowSource"].SourceColumn = "RowSource";
			if (isUpdate)
			{
				return updateCommand;
			}
			return insertCommand;
		}

		private string GetInsertUpdateInvoiceDNoteText(bool isUpdate)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddInsertUpdateParameters("Invoice_DNote", new FieldValue("InvoiceSysDocID", "@InvoiceSysDocID"), new FieldValue("InvoiceVoucherID", "@InvoiceVoucherID"), new FieldValue("DNoteSysDocID", "@DNoteSysDocID"), new FieldValue("DNoteVoucherID", "@DNoteVoucherID"), new FieldValue("SourceDocType", "@SourceDocType"));
			return sqlBuilder.GetInsertExpression();
		}

		private SqlCommand GetInsertUpdateInvoiceDNoteCommand(bool isUpdate)
		{
			if (insertCommand != null)
			{
				insertCommand = null;
			}
			insertCommand = new SqlCommand(GetInsertUpdateInvoiceDNoteText(isUpdate: false), base.DBConfig.Connection);
			insertCommand.CommandType = CommandType.Text;
			SqlParameterCollection parameters = insertCommand.Parameters;
			parameters.Add("@InvoiceSysDocID", SqlDbType.NVarChar);
			parameters.Add("@InvoiceVoucherID", SqlDbType.NVarChar);
			parameters.Add("@DNoteSysDocID", SqlDbType.NVarChar);
			parameters.Add("@DNoteVoucherID", SqlDbType.NVarChar);
			parameters.Add("@SourceDocType", SqlDbType.TinyInt);
			parameters["@InvoiceSysDocID"].SourceColumn = "InvoiceSysDocID";
			parameters["@InvoiceVoucherID"].SourceColumn = "InvoiceVoucherID";
			parameters["@DNoteSysDocID"].SourceColumn = "DNoteSysDocID";
			parameters["@DNoteVoucherID"].SourceColumn = "DNoteVoucherID";
			parameters["@SourceDocType"].SourceColumn = "SourceDocType";
			return insertCommand;
		}

		private bool ValidateData(W3PLDeliveryData journalData)
		{
			return true;
		}

		public bool InsertUpdateDelivery(W3PLDeliveryData deliveryNoteData, bool isUpdate)
		{
			bool flag = true;
			SqlCommand insertUpdateW3PLDeliveryCommand = GetInsertUpdateW3PLDeliveryCommand(isUpdate);
			string text = "";
			try
			{
				DataRow dataRow = deliveryNoteData.W3PLDeliveryTable.Rows[0];
				string text2 = dataRow["VoucherID"].ToString();
				string sysDocID = dataRow["SysDocID"].ToString();
				if (isUpdate && !AllowModify(sysDocID, text2))
				{
					throw new CompanyException("Some items in this transaction has been already invoiced. You are not able to modify.", 1047);
				}
				SqlTransaction sqlTransaction = base.DBConfig.StartNewTransaction();
				if (dataRow["IsExport"] != DBNull.Value)
				{
					bool.Parse(dataRow["IsExport"].ToString());
				}
				if (dataRow["SourceDocType"] != DBNull.Value)
				{
					byte.Parse(dataRow["SourceDocType"].ToString());
				}
				if (!isUpdate && new SystemDocuments(base.DBConfig).ExistDocumentNumber("W3PL_Delivery", "VoucherID", dataRow["SysDocID"].ToString(), text2, sqlTransaction))
				{
					throw new CompanyException("Document number already exist.", 1046);
				}
				foreach (DataRow row in deliveryNoteData.W3PLDeliveryDetailTable.Rows)
				{
					row["SysDocID"] = dataRow["SysDocID"];
					row["VoucherID"] = dataRow["VoucherID"];
					text = row["ProductID"].ToString();
					string text3 = "";
					object fieldValue = new Databases(base.DBConfig).GetFieldValue("Product", "UnitID", "ProductID", text, sqlTransaction);
					if (fieldValue != null)
					{
						text3 = fieldValue.ToString();
					}
					if (text3 != "" && row["UnitID"] != DBNull.Value && row["UnitID"].ToString() != text3)
					{
						DataRow obj = new Products(base.DBConfig).GetProductUnitRow(text, row["UnitID"].ToString()) ?? throw new CompanyException("One of the selected units is not assigned to item.\nItem:" + text + "\nUnit:" + row["UnitID"].ToString());
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
				insertUpdateW3PLDeliveryCommand.Transaction = sqlTransaction;
				flag = (isUpdate ? (flag & Update(deliveryNoteData, "W3PL_Delivery", insertUpdateW3PLDeliveryCommand)) : (flag & Insert(deliveryNoteData, "W3PL_Delivery", insertUpdateW3PLDeliveryCommand)));
				insertUpdateW3PLDeliveryCommand = GetInsertUpdateW3PLDeliveryDetailsCommand(isUpdate: false);
				insertUpdateW3PLDeliveryCommand.Transaction = sqlTransaction;
				if (isUpdate)
				{
					flag &= DeleteDeliveryDetailsRows(sysDocID, text2, isDeletingTransaction: false, sqlTransaction);
				}
				if (deliveryNoteData.Tables["W3PL_Delivery_Detail"].Rows.Count > 0)
				{
					flag &= Insert(deliveryNoteData, "W3PL_Delivery_Detail", insertUpdateW3PLDeliveryCommand);
				}
				InventoryTransactionData inventoryTransactionData = new InventoryTransactionData();
				foreach (DataRow row2 in deliveryNoteData.W3PLDeliveryDetailTable.Rows)
				{
					DataRow dataRow4 = inventoryTransactionData.InventoryTransactionTable.NewRow();
					dataRow4.BeginEdit();
					dataRow4["SysDocID"] = row2["SysDocID"];
					dataRow4["VoucherID"] = row2["VoucherID"];
					dataRow4["LocationID"] = row2["LocationID"];
					dataRow4["ProductID"] = row2["ProductID"];
					dataRow4["Quantity"] = -1m * decimal.Parse(row2["Quantity"].ToString());
					dataRow4["Reference"] = dataRow["Reference"];
					dataRow4["SysDocType"] = (byte)106;
					dataRow4["TransactionDate"] = dataRow["TransactionDate"];
					dataRow4["TransactionType"] = (byte)21;
					dataRow4["UnitPrice"] = 0;
					dataRow4["RowIndex"] = row2["RowIndex"];
					dataRow4["PayeeType"] = "C";
					dataRow4["PayeeID"] = dataRow["CustomerID"];
					dataRow4["DivisionID"] = dataRow["DivisionID"];
					dataRow4["CompanyID"] = dataRow["CompanyID"];
					if (row2["UnitQuantity"] != DBNull.Value && row2["UnitFactor"] != DBNull.Value)
					{
						dataRow4["UnitQuantity"] = row2["UnitQuantity"];
						dataRow4["Factor"] = row2["UnitFactor"];
						dataRow4["FactorType"] = row2["FactorType"];
						decimal.Parse(row2["UnitFactor"].ToString());
						row2["FactorType"].ToString();
						decimal d = decimal.Parse(row2["UnitQuantity"].ToString());
						decimal num3 = decimal.Parse(row2["Quantity"].ToString());
						decimal d2 = decimal.Parse(row2["UnitPrice"].ToString());
						decimal num4 = default(decimal);
						num4 = ((!(num3 != 0m)) ? default(decimal) : (d * d2 / num3));
						dataRow4["UnitPrice"] = num4;
					}
					dataRow4.EndEdit();
					inventoryTransactionData.InventoryTransactionTable.Rows.Add(dataRow4);
				}
				inventoryTransactionData.Merge(deliveryNoteData.Tables["Product_Lot_Issue_Detail"]);
				flag &= new Products(base.DBConfig).InsertUpdateProductLotIssueDetail(deliveryNoteData, isUpdate: false, sqlTransaction);
				flag &= new InventoryTransaction(base.DBConfig).InsertUpdateInventoryTransaction(inventoryTransactionData, isUpdate, sqlTransaction);
				if (!flag)
				{
					throw new Exception("Failed to save the transaction because of an unexpected error. Please try again.");
				}
				flag &= UpdateTableRowInsertUpdateInfo("W3PL_Delivery", "SysDocID", dataRow["SysDocID"].ToString(), "VoucherID", dataRow["VoucherID"].ToString(), sqlTransaction, !isUpdate);
				string entityName = "3PL Delivery Note";
				flag = (isUpdate ? (flag & AddActivityLog(entityName, text2, sysDocID, ActivityTypes.Update, sqlTransaction)) : (flag & AddActivityLog(entityName, text2, sysDocID, ActivityTypes.Add, sqlTransaction)));
				if (isUpdate)
				{
					return flag;
				}
				flag &= new SystemDocuments(base.DBConfig).UpdateNextDocumentNumber(dataRow["SysDocID"].ToString(), dataRow["VoucherID"].ToString(), "W3PL_Delivery", "VoucherID", sqlTransaction);
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

		private GLData CreateDNGLData(W3PLDeliveryData transactionData, SqlTransaction sqlTransaction)
		{
			try
			{
				GLData gLData = new GLData();
				DataRow dataRow = transactionData.W3PLDeliveryTable.Rows[0];
				int currencyDecimalPoints = new CompanyInformations(base.DBConfig).CurrencyDecimalPoints;
				string value = dataRow["CustomerID"].ToString();
				string text = dataRow["SysDocID"].ToString();
				string text2 = dataRow["VoucherID"].ToString();
				string textCommand = "SELECT SD.LocationID,  LOC.UnInvoicedInventoryAccountID, LOC.InventoryAccountID\r\n                                FROM System_Document SD INNER JOIN Location LOC ON SD.LocationID = LOC.LocationID\r\n                                WHERE SysDocID = '" + text + "'";
				DataSet dataSet = new DataSet();
				FillDataSet(dataSet, "Accounts", textCommand, sqlTransaction);
				if (dataSet == null || dataSet.Tables.Count == 0 || dataSet.Tables[0].Rows.Count == 0)
				{
					throw new CompanyException("There is no location assigned to this system document or location record is missing.");
				}
				DataRow dataRow2 = dataSet.Tables["Accounts"].Rows[0];
				string docLocationID = dataRow2["LocationID"].ToString();
				string text3 = dataRow2["UnInvoicedInventoryAccountID"].ToString();
				bool result = false;
				bool.TryParse(dataRow["IsExport"].ToString(), out result);
				DataRow dataRow3 = gLData.JournalTable.NewRow();
				SysDocTypes sysDocTypes = SysDocTypes.W3PLDelivery;
				dataRow3["JournalID"] = 0;
				dataRow3["JournalDate"] = dataRow["TransactionDate"];
				dataRow3["SysDocID"] = dataRow["SysDocID"];
				dataRow3["SysDocType"] = (byte)sysDocTypes;
				dataRow3["VoucherID"] = dataRow["VoucherID"];
				dataRow3["Reference"] = dataRow["Reference"];
				dataRow3["Narration"] = "Delivery Note - " + text2;
				dataRow3.EndEdit();
				gLData.JournalTable.Rows.Add(dataRow3);
				decimal num = default(decimal);
				Hashtable hashtable = new Hashtable();
				ArrayList arrayList = new ArrayList();
				decimal d = default(decimal);
				foreach (DataRow row in transactionData.W3PLDeliveryDetailTable.Rows)
				{
					string text4 = row["ProductID"].ToString();
					string warehouseLocationID = row["LocationID"].ToString();
					int rowIndex = int.Parse(row["RowIndex"].ToString());
					dataSet = new Products(base.DBConfig).GetProductTransactionAccounts(text4, docLocationID, warehouseLocationID, text, sqlTransaction);
					if (dataSet == null || dataSet.Tables.Count == 0 || dataSet.Tables[0].Rows.Count == 0)
					{
						throw new CompanyException("Product accounts information not found for product or location.");
					}
					dataRow2 = dataSet.Tables[0].Rows[0];
					string text5 = dataRow2["InventoryAssetAccountID"].ToString();
					if (!(text5 == text3))
					{
						ItemTypes itemTypes = ItemTypes.Inventory;
						object obj2 = dataRow2["ItemType"].ToString();
						if (obj2 == null || !(obj2.ToString() != ""))
						{
							throw new CompanyException("Item type is not selected for the product:" + text4);
						}
						itemTypes = (ItemTypes)byte.Parse(obj2.ToString());
						decimal result2 = default(decimal);
						decimal.TryParse(dataRow2["AverageCost"].ToString(), out result2);
						decimal num2 = default(decimal);
						num2 = ((itemTypes == ItemTypes.ConsignmentItem) ? default(decimal) : Math.Abs(new InventoryTransaction(base.DBConfig).GetRowAssetValue(text4, text, text2, rowIndex, sqlTransaction)));
						if (itemTypes == ItemTypes.Inventory || itemTypes == ItemTypes.Assembly)
						{
							string text6 = text5;
							if (hashtable.ContainsKey(text6))
							{
								num = decimal.Parse(hashtable[text6].ToString());
								num += Math.Round(num2, currencyDecimalPoints);
								hashtable[text6] = num;
							}
							else
							{
								hashtable.Add(text6, Math.Round(num2, currencyDecimalPoints));
								arrayList.Add(text6);
							}
							d += Math.Round(num2, currencyDecimalPoints);
						}
					}
				}
				if (d != 0m)
				{
					for (int i = 0; i < hashtable.Count; i++)
					{
						DataRow dataRow4 = gLData.JournalDetailsTable.NewRow();
						dataRow4.BeginEdit();
						string text6 = arrayList[i].ToString();
						num = decimal.Parse(hashtable[text6].ToString());
						dataRow4["JournalID"] = 0;
						dataRow4["AccountID"] = text6;
						dataRow4["PayeeID"] = value;
						dataRow4["Debit"] = DBNull.Value;
						dataRow4["Credit"] = num;
						dataRow4["IsBaseOnly"] = true;
						dataRow4["Reference"] = dataRow["Reference"];
						dataRow4.EndEdit();
						gLData.JournalDetailsTable.Rows.Add(dataRow4);
					}
				}
				if (d != 0m)
				{
					if (text3 == "")
					{
						throw new CompanyException("Inventory on delivery account is not set.");
					}
					DataRow dataRow4 = gLData.JournalDetailsTable.NewRow();
					dataRow4.BeginEdit();
					dataRow4["JournalID"] = 0;
					dataRow4["AccountID"] = text3;
					dataRow4["Debit"] = num;
					dataRow4["Credit"] = DBNull.Value;
					dataRow4["IsBaseOnly"] = true;
					dataRow4["Reference"] = dataRow["Reference"];
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

		public W3PLDeliveryData GetDeliveryByID(string sysDocID, string voucherID)
		{
			try
			{
				W3PLDeliveryData w3PLDeliveryData = new W3PLDeliveryData();
				string textCommand = "SELECT DN.*, PaymentTermID FROM W3PL_Delivery DN INNER JOIN Customer C ON DN.CustomerID = C.CustomerID  WHERE VoucherID='" + voucherID + "' AND SysDocID='" + sysDocID + "'";
				FillDataSet(w3PLDeliveryData, "W3PL_Delivery", textCommand);
				if (w3PLDeliveryData == null || w3PLDeliveryData.Tables.Count == 0 || w3PLDeliveryData.Tables["W3PL_Delivery"].Rows.Count == 0)
				{
					return null;
				}
				textCommand = "SELECT DISTINCT TD.*,Product.Description,Product.ItemType,Product.Attribute1,Product.Attribute2,Product.ItemType,BrandName AS Brand,\r\n                        Product.Attribute3,Product.MatrixParentID,IsTrackLot,IsTrackSerial, \r\n                        (SELECT TOP 1 \r\n\t\t\t\t\t\t \r\n                                             CASE WHEN PL.SourceLotNumber IS NULL THEN PL.ReceiptNumber ELSE \r\n                            (SELECT CASE WHEN PL2.SourceLotNumber IS NULL THEN PL2.ReceiptNumber ELSE \r\n                            (SELECT CASE WHEN PL3.SourceLotNumber IS NULL THEN PL3.ReceiptNumber ELSE \r\n                            (SELECT CASE WHEN PL4.SourceLotNumber IS NULL THEN PL4.ReceiptNumber ELSE \r\n                            (SELECT CASE WHEN PL5.SourceLotNumber IS NULL THEN PL5.ReceiptNumber ELSE \r\n                            (SELECT CASE WHEN PL6.SourceLotNumber IS NULL THEN PL6.ReceiptNumber ELSE \r\n                            (SELECT CASE WHEN PL7.SourceLotNumber IS NULL THEN PL7.ReceiptNumber ELSE \r\n                            (SELECT CASE WHEN PL8.SourceLotNumber IS NULL THEN PL8.ReceiptNumber ELSE \r\n                            (SELECT CASE WHEN PL6.SourceLotNumber IS NULL THEN PL9.ReceiptNumber ELSE \r\n                            PL9.ReceiptNumber END \r\n                            FROM Product_LOT PL9 WHERE PL9.LotNumber = PL8.SourceLotNumber) END \r\n                            FROM Product_LOT PL8 WHERE PL8.LotNumber = PL7.SourceLotNumber) END \r\n                            FROM Product_LOT PL7 WHERE PL7.LotNumber = PL6.SourceLotNumber) END \r\n                            FROM Product_LOT PL6 WHERE PL6.LotNumber = PL5.SourceLotNumber) END \r\n                            FROM Product_LOT PL5 WHERE PL5.LotNumber = PL4.SourceLotNumber) END \r\n                            FROM Product_LOT PL4 WHERE PL4.LotNumber = PL3.SourceLotNumber) END \r\n                            FROM Product_LOT PL3 WHERE PL3.LotNumber = PL2.SourceLotNumber) END \r\n                            FROM Product_LOT PL2 WHERE PL2.LotNumber = PL.SourceLotNumber) END\r\n\t\t\t\t\t\t\t FROM Product_Lot_Sales PLS\r\n\t\t\t\t\t\tLEFT OUTER JOIN Product_Lot PL \r\n                        ON  PLS.ItemCode =  PL.ItemCode AND PLS.LotNo = PL.LotNumber\r\n\t\t\t\t\t\tWHERE  ( PLS.LotNo = pl.LotNumber  or PLS.LotNo = pl.SourceLotNumber) \r\n                        and td.ProductID = pls.ItemCode and  PLS.DocID='" + sysDocID + "' AND PLS.InvoiceNumber='" + voucherID + "' ) AS [Consign#]\r\n                        FROM W3PL_Delivery_Detail TD INNER JOIN Product ON TD.ProductID=Product.ProductID\r\n                        LEFT OUTER JOIN Product_Brand Brand ON Brand.BrandID = Product.BrandID\r\n                        WHERE VoucherID='" + voucherID + "'  AND SysDocID='" + sysDocID + "' ORDER BY RowIndex";
				FillDataSet(w3PLDeliveryData, "W3PL_Delivery_Detail", textCommand);
				textCommand = "SELECT * FROM Invoice_DNote\r\n                        WHERE DNoteVoucherID='" + voucherID + "' AND DNoteSysDocID='" + sysDocID + "'";
				FillDataSet(w3PLDeliveryData, "Invoice_DNote", textCommand);
				DataSet transactionIssuesProductLots = new Products(base.DBConfig).GetTransactionIssuesProductLots(sysDocID, voucherID);
				if (w3PLDeliveryData.Tables.Contains("Product_Lot_Issue_Detail"))
				{
					w3PLDeliveryData.Tables.Remove("Product_Lot_Issue_Detail");
				}
				w3PLDeliveryData.Merge(transactionIssuesProductLots, preserveChanges: false);
				return w3PLDeliveryData;
			}
			catch
			{
				throw;
			}
		}

		internal bool DeleteDeliveryDetailsRows(string sysDocID, string voucherID, bool isDeletingTransaction, SqlTransaction sqlTransaction)
		{
			bool flag = true;
			try
			{
				W3PLDeliveryData w3PLDeliveryData = new W3PLDeliveryData();
				string textCommand = "SELECT SOD.*,ISVOID,IsExport FROM W3PL_Delivery_Detail SOD INNER JOIN W3PL_Delivery SO ON SO.SysDocID=SOD.SysDocID AND SO.VOucherID=SOD.VoucherID\r\n                              WHERE SOD.SysDocID = '" + sysDocID + "' AND SOD.VoucherID = '" + voucherID + "'";
				FillDataSet(w3PLDeliveryData, "W3PL_Delivery_Detail", textCommand, sqlTransaction);
				bool result = false;
				bool.TryParse(w3PLDeliveryData.W3PLDeliveryDetailTable.Rows[0]["IsExport"].ToString(), out result);
				string text = "";
				text = ((!result) ? new CompanyOption(base.DBConfig).GetCompanyOptionValue(56.ToString()).ToString() : new CompanyOption(base.DBConfig).GetCompanyOptionValue(57.ToString()).ToString());
				SalesFlows salesFlows = SalesFlows.DirectInvoice;
				if (text != "")
				{
					salesFlows = (SalesFlows)int.Parse(text.ToString());
				}
				bool result2 = false;
				bool.TryParse(w3PLDeliveryData.W3PLDeliveryDetailTable.Rows[0]["IsVoid"].ToString(), out result2);
				if (!result2)
				{
					if (salesFlows == SalesFlows.SOThenDNThenInvoice)
					{
						flag &= new InventoryTransaction(base.DBConfig).DeleteInventoryTransaction(106, sysDocID, voucherID, isDeletingTransaction, sqlTransaction);
						string text2 = "";
						string text3 = "";
						string text4 = "";
						foreach (DataRow row in w3PLDeliveryData.W3PLDeliveryDetailTable.Rows)
						{
							text4 = row["ProductID"].ToString();
							text2 = row["SourceVoucherID"].ToString();
							text3 = row["SourceSysDocID"].ToString();
							int result3 = 0;
							if (!(text2 == "") && !(text3 == ""))
							{
								int.TryParse(row["SourceRowIndex"].ToString(), out result3);
								float result4 = 0f;
								if (row["UnitQuantity"] != DBNull.Value)
								{
									float.TryParse(row["UnitQuantity"].ToString(), out result4);
								}
								else
								{
									float.TryParse(row["Quantity"].ToString(), out result4);
								}
								float num = new Products(base.DBConfig).GetReservedQuantity(text4, sqlTransaction) + result4;
								if (num < 0f)
								{
									num = 0f;
								}
								flag &= new Products(base.DBConfig).UpdateReservedQuantity(text4, num, sqlTransaction);
								flag &= new SalesOrder(base.DBConfig).UpdateRowShippedQuantity(text3, text2, result3, -1f * result4, sqlTransaction);
							}
						}
					}
					else
					{
						string text5 = "";
						string text6 = "";
						if (salesFlows != SalesFlows.SOThenDNThenInvoice)
						{
							foreach (DataRow row2 in w3PLDeliveryData.W3PLDeliveryDetailTable.Rows)
							{
								row2["ProductID"].ToString();
								text5 = row2["SourceVoucherID"].ToString();
								text6 = row2["SourceSysDocID"].ToString();
								int result5 = 0;
								if (!(text5 == "") && !(text6 == ""))
								{
									int.TryParse(row2["SourceRowIndex"].ToString(), out result5);
									float result6 = 0f;
									if (row2["UnitQuantity"] != DBNull.Value)
									{
										float.TryParse(row2["UnitQuantity"].ToString(), out result6);
									}
									else
									{
										float.TryParse(row2["Quantity"].ToString(), out result6);
									}
									flag &= new SalesInvoice(base.DBConfig).UpdateRowShippedQuantity(text6, text5, result5, -1f * result6, sqlTransaction);
									flag &= new SalesInvoice(base.DBConfig).ReOpenInvoice(text6, text5, sqlTransaction);
								}
							}
						}
					}
				}
				textCommand = "DELETE FROM Product_Lot_Issue_Detail WHERE SysDocID = '" + sysDocID + "' AND VoucherID = '" + voucherID + "'";
				flag &= Delete(textCommand, sqlTransaction);
				textCommand = "DELETE FROM W3PL_Delivery_Detail WHERE SysDocID = '" + sysDocID + "' AND VoucherID = '" + voucherID + "'";
				return flag & Delete(textCommand, sqlTransaction);
			}
			catch
			{
				throw;
			}
		}

		public bool IsDNoteInvoiced(string sysDocID, string voucherID, SqlTransaction sqlTransaction)
		{
			string exp = "SELECT COUNT(*) FROM Sales_Invoice_Detail SID INNER JOIN Sales_Invoice SI \r\n                            ON SID.SysDocID = SI.SysDOcID AND SID.VoucherID = SI.VoucherID WHERE ISNULL(IsVoid,'False')='False' AND RowSource = 5 AND OrderSysDocID = '" + sysDocID + "' AND OrderVoucherID = '" + voucherID + "'";
			object obj = ExecuteScalar(exp, sqlTransaction);
			if (obj != null && obj.ToString() != "" && int.Parse(obj.ToString()) > 0)
			{
				return true;
			}
			return false;
		}

		public bool VoidDelivery(string sysDocID, string voucherID, bool isVoid)
		{
			bool flag = true;
			try
			{
				SqlTransaction sqlTransaction = base.DBConfig.StartNewTransaction();
				if (!AllowModify(sysDocID, voucherID))
				{
					throw new CompanyException("Some items in this transaction has been already invoiced. You are not able to modify.", 1047);
				}
				string exp = "UPDATE W3PL_Delivery SET IsVoid = '" + isVoid.ToString() + "' WHERE SysDocID='" + sysDocID + "' AND VoucherID = '" + voucherID + "'";
				flag &= (ExecuteNonQuery(exp, sqlTransaction) > 0);
				W3PLDeliveryData dataSet = new W3PLDeliveryData();
				exp = "SELECT * FROM W3PL_Delivery_Detail\r\n                              WHERE SysDocID = '" + sysDocID + "' AND VoucherID = '" + voucherID + "'";
				FillDataSet(dataSet, "W3PL_Delivery_Detail", exp, sqlTransaction);
				flag &= new InventoryTransaction(base.DBConfig).DeleteInventoryTransaction(106, sysDocID, voucherID, isDeletingTransaction: true, sqlTransaction);
				if (!flag)
				{
					return flag;
				}
				ActivityTypes activityType = ActivityTypes.Void;
				if (!isVoid)
				{
					activityType = ActivityTypes.Unvoid;
				}
				AddActivityLog("3PL Delivery Note", voucherID, sysDocID, activityType, sqlTransaction);
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

		public bool DeleteDelivery(string sysDocID, string voucherID)
		{
			bool flag = true;
			try
			{
				string text = "";
				SqlTransaction sqlTransaction = base.DBConfig.StartNewTransaction();
				if (!AllowModify(sysDocID, voucherID))
				{
					throw new CompanyException("Some items in this transaction has been already invoiced. You are not able to modify.", 1047);
				}
				flag &= DeleteDeliveryDetailsRows(sysDocID, voucherID, isDeletingTransaction: true, sqlTransaction);
				flag &= new Journal(base.DBConfig).DeleteJournal(sysDocID, voucherID, sqlTransaction);
				text = "DELETE FROM W3PL_Delivery WHERE SysDocID='" + sysDocID + "' AND VoucherID = '" + voucherID + "'";
				flag &= (ExecuteNonQuery(text, sqlTransaction) >= 0);
				if (!flag)
				{
					return flag;
				}
				ActivityTypes activityType = ActivityTypes.Delete;
				AddActivityLog("Delivery Note", voucherID, sysDocID, activityType, sqlTransaction);
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

		internal bool UpdateRowReturnedQuantity(string sysDocID, string voucherID, int rowIndex, float quantity, SqlTransaction sqlTransaction)
		{
			DataSet dataSet = new DataSet();
			float result = 0f;
			float result2 = 0f;
			try
			{
				string textCommand = "SELECT Quantity,UnitQuantity,QuantityReturned,SalesFlow FROM W3PL_Delivery_Detail DND INNER JOIN W3PL_Delivery DN ON DN.SysDocID=DND.SysDocID AND DN.VoucherID=DND.VoucherID\r\n                                WHERE DND.SysDocID='" + sysDocID + "' AND DND.VoucherID='" + voucherID + "' AND RowIndex=" + rowIndex.ToString();
				FillDataSet(dataSet, "Product", textCommand);
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
					SalesFlows salesFlows = SalesFlows.DirectInvoice;
					if (dataRow["SalesFlow"] != DBNull.Value && dataRow["SalesFlow"].ToString() != "")
					{
						salesFlows = (SalesFlows)int.Parse(dataRow["SalesFlow"].ToString());
					}
					if (salesFlows != SalesFlows.SOThenDNThenInvoice)
					{
						throw new CompanyException("Sales return is not allowed from a delivery note because selected delivery note is created in an unsupported Sales Flow.");
					}
				}
				result2 += quantity;
				textCommand = "UPDATE W3PL_Delivery_Detail SET QuantityReturned=" + result2.ToString() + " WHERE SysDocID='" + sysDocID + "' AND VoucherID='" + voucherID + "' AND RowIndex=" + rowIndex.ToString();
				return ExecuteNonQuery(textCommand, sqlTransaction) > 0;
			}
			catch
			{
				return false;
			}
		}

		public DataSet GetUninvoicedDeliverys(string customerID, bool isExport)
		{
			return GetUninvoicedDeliverys("", customerID, isExport);
		}

		public DataSet GetUninvoicedDeliverys(string sysDocID, string customerID, bool isExport)
		{
			try
			{
				DataSet dataSet = new DataSet();
				string text = "";
				if (sysDocID != "")
				{
					DataSet entityLinks = new SystemDocuments(base.DBConfig).GetEntityLinks(sysDocID, SysDocEntityTypes.CustomerClass);
					if (entityLinks != null && entityLinks.Tables["System_Doc_Entity_Link"].Rows.Count > 0)
					{
						foreach (DataRow row in entityLinks.Tables["System_Doc_Entity_Link"].Rows)
						{
							if (text != "")
							{
								text += ",";
							}
							text = text + "'" + row["EntityID"].ToString() + "'";
						}
					}
				}
				string str = "SELECT DN.SysDocID [Doc ID],DN.VoucherID [Number], DN.TransactionDate AS [Date],DN.CustomerID + '-' + C.CustomerName AS [Customer]\r\n                              FROM W3PL_Delivery DN\r\n                              INNER JOIN W3PL_Delivery_Detail DND ON DND.SysDocID = DN.SysDocID AND DND.VoucherID = DN.VoucherID \r\n                              INNER JOIN Customer C ON DN.CustomerID=C.CustomerID                                 \r\n                              WHERE ISNULL(DN.IsVoid,'False')='False' AND ISNULL(DN.IsInvoiced,'False')='False' AND ISNULL(DN.SalesFlow,0) = 2 ";
				if (customerID != "")
				{
					str = str + " AND DN.CustomerID='" + customerID + "' ";
				}
				if (!string.IsNullOrEmpty(text))
				{
					str = str + " AND C.CustomerClassID IN (" + text + ") ";
				}
				str += "  GROUP BY DN.SysDocID ,DN.VoucherID, DN.TransactionDate ,DN.CustomerID , C.CustomerName \r\n\t\t\t\t\t\t\t  HAVING SUM(Quantity - ISNULL(QuantityReturned,0)) > 0  ";
				FillDataSet(dataSet, "W3PL_Delivery", str);
				return dataSet;
			}
			catch
			{
				throw;
			}
		}

		public DataSet GetDeliveryToPrint(string sysDocID, string voucherID)
		{
			return GetDeliveryToPrint(sysDocID, new string[1]
			{
				voucherID
			});
		}

		public DataSet GetDeliveryToPrint(string sysDocID, string[] voucherID)
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
				string cmdText = "SELECT  DISTINCT   SysDocID,VoucherID,SI.CustomerID,CustomerName,CustomerAddress,TransactionDate,\r\n                                SI.SalesPersonID,RequiredDate,CA.AddressPrintFormat AS ShippingAddress,ShippingMethodName,\r\n                                ISNULL(SI.CurrencyID,(SELECT CurrencyID FROM Currency WHERE IsBase='True')) AS CurrencyID,\r\n                                SI.TermID,TermName,IsVoid,Reference,Discount AS Discount,\r\n                                Total AS Total,PONumber,SI.Note, SI.InvoiceVoucherID, (SELECT TOP 1 DriverName FROM Driver WHERE DriverID = SI.DriverID) DriverName, \r\n\t\t\t\t\t\t\t\t(SELECT TOP 1 RegistrationNumber FROM Vehicle WHERE VehicleID = SI.VehicleID) RegistrationNumber, SI.Reference2,\r\n                                SI.DateCreated,SI.DateUpdated,SI.CreatedBy,SI.UpdatedBy \r\n                                FROM  W3PL_Delivery SI INNER JOIN Customer ON SI.CustomerID=Customer.CustomerID\r\n                                LEFT OUTER JOIN Payment_Term PT ON SI.TermID=PT.PaymentTermID\r\n                                LEFT OUTER JOIN Customer_Address CA ON CA.AddressID=ShippingAddressID AND CA.CustomerID=SI.CustomerID\r\n                                LEFT OUTER JOIN Shipping_Method SM ON SM.ShippingMethodID=SI.ShippingMethodID\r\n                                WHERE SysDocID = '" + sysDocID + "' AND VoucherID IN (" + text + ")";
				SqlCommand sqlCommand = new SqlCommand(cmdText);
				FillDataSet(dataSet, "W3PL_Delivery", sqlCommand);
				if (dataSet == null || dataSet.Tables.Count == 0 || dataSet.Tables["W3PL_Delivery"].Rows.Count == 0)
				{
					return null;
				}
				cmdText = "SELECT DISTINCT DND.SysDocID, DND.VoucherID, DND.ProductID, DND.Description, DND.LocationID, ISNULL(DND.UnitQuantity, DND.Quantity) AS Quantity, Product.BrandID, Product.CategoryID, \r\n                    Product.ClassID, Product.QuantityPerUnit,\r\n                        DND.UnitPrice, Product.Size, ISNULL(DND.UnitQuantity, DND.Quantity) * DND.UnitPrice AS Total, DND.UnitID, DND.RowIndex\r\n                        FROM   W3PL_Delivery_Detail DND INNER JOIN Product ON DND.ProductID = Product.ProductID\r\n                                         LEFT OUTER JOIN Product_Lot_Sales PL ON  Product.ProductID =  PL.ItemCode \r\n                                                                      AND PL.DocID = DND.SysDocID AND PL.InvoiceNumber = DND.VoucherID\r\n                                                                     LEFT  OUTER JOIN Product_Lot PLt ON  PLT.LotNumber =  PL.LotNo \r\n                        WHERE DND.SysDocID='" + sysDocID + "' AND DND.VoucherID IN (" + text + ")  ORDER BY DND.RowIndex";
				FillDataSet(dataSet, "W3PL_Delivery_Detail", cmdText);
				dataSet.Relations.Add("CustomerW3PLDelivery", new DataColumn[2]
				{
					dataSet.Tables["W3PL_Delivery"].Columns["SysDocID"],
					dataSet.Tables["W3PL_Delivery"].Columns["VoucherID"]
				}, new DataColumn[2]
				{
					dataSet.Tables["W3PL_Delivery_Detail"].Columns["SysDocID"],
					dataSet.Tables["W3PL_Delivery_Detail"].Columns["VoucherID"]
				}, createConstraints: false);
				dataSet.Tables["W3PL_Delivery"].Columns.Add("TotalInWords", typeof(string));
				foreach (DataRow row in dataSet.Tables["W3PL_Delivery"].Rows)
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

		public bool AllowModify(string sysDocID, string voucherNumber)
		{
			string exp = "SELECT COUNT(*) FROM W3PL_Delivery WHERE (ISNULL(IsInvoiced,'False')= 'True' OR ISNULL(IsShipped,'False')='True') AND (SysDocID = '" + sysDocID + "' AND VoucherID = '" + voucherNumber + "')";
			object obj = ExecuteScalar(exp);
			if (obj != null && obj.ToString() != "" && int.Parse(obj.ToString()) > 0)
			{
				return false;
			}
			exp = "SELECT COUNT(*) FROM W3PL_Delivery WHERE (ISNULL(IsInvoiced,'False')= 'True' OR ISNULL(IsShipped,'False')='True') AND (SysDocID = '" + sysDocID + "' AND VoucherID = '" + voucherNumber + "')";
			obj = ExecuteScalar(exp);
			if (obj != null && obj.ToString() != "" && int.Parse(obj.ToString()) > 0)
			{
				return false;
			}
			exp = "SELECT ISNULL(SUM(ISNULL(QuantityReturned,0) + ISNULL(QuantityShipped,0)),0) AS Quantity FROM W3PL_Delivery_Detail\r\n                        WHERE SysDocID = '" + sysDocID + "' AND VoucherID = '" + voucherNumber + "'";
			obj = ExecuteScalar(exp);
			if (obj != null && obj.ToString() != "" && decimal.Parse(obj.ToString()) != 0m)
			{
				return false;
			}
			return true;
		}

		public DataSet GetList(DateTime from, DateTime to, bool showVoid)
		{
			DataSet dataSet = new DataSet();
			string text = StoreConfiguration.ToSqlDateTimeString(from);
			string text2 = StoreConfiguration.ToSqlDateTimeString(to);
			string text3 = "SELECT    ISNULL(IsVoid,'False') AS V, INV.SysDocID [Doc ID],INV.VoucherID [Doc Number],INV.CustomerID [Customer Code],CustomerName [Customer Name],TransactionDate [Delivery Date],\r\n                            INV.SalespersonID [Salesperson],SUM(DND.Quantity) AS Quantity\r\n                            FROM         Delivery_note INV\r\n                            INNER JOIN W3PL_Delivery_Detail DND ON DND.SysDocID=INV.SysDocID AND DND.VoucherID=INV.VoucherID\r\n                            Inner JOIN Customer ON CUSTOMER.CustomerID=INV.CustomerID ";
			if (from != DateTime.MinValue)
			{
				text3 = text3 + " WHERE TransactionDate Between '" + text + "' AND '" + text2 + "'";
			}
			if (!showVoid)
			{
				text3 += " AND ISNULL(IsVoid,'False')='False'";
			}
			text3 += " GROUP BY IsVoid,INV.SysDocID,INV.VoucherID,INV.CustomerID ,CustomerName,TransactionDate,INV.SalespersonID";
			SqlCommand sqlCommand = new SqlCommand(text3);
			FillDataSet(dataSet, "W3PL_Delivery", sqlCommand);
			return dataSet;
		}

		public DataSet GetDOsForPackingList(string customerID, bool isExport)
		{
			try
			{
				DataSet dataSet = new DataSet();
				string text = "SELECT DO.SysDocID [Doc ID],DO.VoucherID [Number], TransactionDate AS [Date],DO.CustomerID + '-' + CUS.CustomerName AS Customer \r\n                                FROM W3PL_Delivery DO\r\n                                INNER JOIN Customer CUS ON DO.CustomerID=CUS.CustomerID  WHERE ISNULL(IsVoid,'False')='False' AND ISNULL(IsShipped,'False')='False'  \r\n                                AND ISNULL(IsExport,'False')='" + isExport.ToString() + "' ";
				if (customerID != "")
				{
					text = text + " AND (DO.CustomerID = '" + customerID + "')";
				}
				FillDataSet(dataSet, "W3PL_Delivery", text);
				return dataSet;
			}
			catch
			{
				throw;
			}
		}

		public DataSet GetDOItemsToShip(string sysDocID, string voucherID)
		{
			try
			{
				DataSet dataSet = new DataSet();
				string textCommand = "SELECT DO.customerID, DOD.SysDocID [Doc ID], DOD.VoucherID,ProductID,Description,DOD.UnitID,RowIndex,UnitQuantity,LocationID,\r\n                                ISNULL(UnitQuantity,Quantity) - ISNULL(QuantityReturned,0) AS Quantity,ISNULL(QuantityShipped,0) AS QuantityShipped\r\n                                FROM W3PL_Delivery_Detail DOD INNER JOIN W3PL_Delivery DO ON DO.SysDocID=DOD.SysDocID AND DO.VoucherID=DOD.VoucherID\r\n                              \r\n                             WHERE DOD.SysDocID='" + sysDocID + "' AND DOD.VoucherID='" + voucherID + "'";
				FillDataSet(dataSet, "W3PL_Delivery_Detail", textCommand);
				return dataSet;
			}
			catch
			{
				throw;
			}
		}

		public DataSet GetPackingListItemsToInvoice(string sysDocID, string voucherID)
		{
			try
			{
				DataSet dataSet = new DataSet();
				string textCommand = "SELECT DISTINCT DO.customerID, DOD.SysDocID [Doc ID], DOD.VoucherID,DOD.ProductID,DOD.Description,DOD.UnitID,DOD.RowIndex,DOD.UnitQuantity,DOD.LocationID,\r\n                                ISNULL(DOD.UnitQuantity,DOD.Quantity) - ISNULL(QuantityReturned,0) AS Quantity,ISNULL(QuantityShipped,0) AS QuantityShipped\r\n                                FROM W3PL_Delivery_Detail DOD INNER JOIN Export_PackingList_Detail PKD ON PKD.SourceSysDocID=DOD.SysDocID AND PKD.SourceVoucherID=DOD.VoucherID\r\n                                INNER JOIN W3PL_Delivery DO ON DO.SysDocID=DOD.SysDocID AND DO.VoucherID=DOD.VoucherID\r\n\t\t\t\t\t\t\t\tWHERE PKD.SysDocID= '" + sysDocID + "' AND PKD.VoucherID = '" + voucherID + "' AND ISNULL(DO.IsInvoiced,'False') = 'False' ";
				FillDataSet(dataSet, "W3PL_Delivery_Detail", textCommand);
				return dataSet;
			}
			catch
			{
				throw;
			}
		}
	}
}
