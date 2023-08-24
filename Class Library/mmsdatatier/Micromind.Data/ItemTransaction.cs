using Micromind.Common;
using Micromind.Common.Data;
using Micromind.Common.Libraries;
using System;
using System.Collections;
using System.Data;
using System.Data.SqlClient;

namespace Micromind.Data
{
	public sealed class ItemTransaction : StoreObject
	{
		private const string ITEMTRANSACTION_TABLE = "Item_transaction";

		private const string ITEMTRANSACTIONDETAIL_TABLE = "Item_Transaction_Detail";

		private const string SYSDOCID_PARM = "@SysDocID";

		private const string VOUCHERID_PARM = "@VoucherID";

		private const string PARTYID_PARM = "@PartyID";

		private const string PARTYTYPE_PARM = "@PartyType";

		private const string SYSDOCTYPE_PARM = "@SysDocType";

		private const string SALESFLOW_PARM = "@SalesFlow";

		private const string TRANSACTIONDATE_PARM = "@TransactionDate";

		private const string SALESPERSONID_PARM = "@SalespersonID";

		private const string ISEXPORT_PARM = "@IsExport";

		private const string ROWINDEX_PARM = "@RowIndex";

		private const string REQUIREDDATE_PARM = "@RequiredDate";

		private const string SHIPPINGADDRESSID_PARM = "@ShippingAddressID";

		private const string BILLINGADDRESSID_PARM = "@BillingAddressID";

		private const string SHIPTOADDRESS_PARM = "@ShipToAddress";

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

		private const string CLUSERID_PARM = "@CLUserID";

		private const string PORT_PARM = "@Port";

		private const string CONTAINERNO_PARM = "@ContainerNumber";

		private const string CONTAINERSIZEID_PARM = "@ContainerSizeID";

		private const string JOBID_PARM = "@JobID";

		private const string COSTCATEGORYID_PARM = "@CostCategoryID";

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

		public ItemTransaction(Config config)
			: base(config)
		{
		}

		private string GetInsertUpdateItemTransactionText(bool isUpdate)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddInsertUpdateParameters("Item_Transaction", new FieldValue("SysDocID", "@SysDocID", isUpdateConditionField: true), new FieldValue("VoucherID", "@VoucherID", isUpdateConditionField: true), new FieldValue("PartyID", "@PartyID"), new FieldValue("PartyType", "@PartyType"), new FieldValue("SysDocType", "@SysDocType"), new FieldValue("SalesFlow", "@SalesFlow"), new FieldValue("IsExport", "@IsExport"), new FieldValue("TransactionDate", "@TransactionDate"), new FieldValue("SalespersonID", "@SalespersonID"), new FieldValue("RequiredDate", "@RequiredDate"), new FieldValue("ShippingAddressID", "@ShippingAddressID"), new FieldValue("BillingAddressID", "@BillingAddressID"), new FieldValue("ShippingMethodID", "@ShippingMethodID"), new FieldValue("CustomerAddress", "@CustomerAddress"), new FieldValue("ShipToAddress", "@ShipToAddress"), new FieldValue("Status", "@Status"), new FieldValue("CurrencyID", "@CurrencyID"), new FieldValue("Discount", "@Discount"), new FieldValue("Total", "@Total"), new FieldValue("PONumber", "@PONumber"), new FieldValue("TermID", "@TermID"), new FieldValue("Reference", "@Reference"), new FieldValue("Reference2", "@Reference2"), new FieldValue("SourceDocType", "@SourceDocType"), new FieldValue("Note", "@Note"), new FieldValue("CLUserID", "@CLUserID"), new FieldValue("Port", "@Port"), new FieldValue("LocationID", "@LocationID"), new FieldValue("DriverID", "@DriverID"), new FieldValue("VehicleID", "@VehicleID"), new FieldValue("ContainerNumber", "@ContainerNumber"), new FieldValue("ContainerSizeID", "@ContainerSizeID"), new FieldValue("JobID", "@JobID"), new FieldValue("CostCategoryID", "@CostCategoryID"));
			if (isUpdate)
			{
				sqlBuilder.AddInsertUpdateParameters("Item_Transaction", new FieldValue("DateUpdated", "@DateUpdated", isUpdateConditionField: true, checkForNullValue: true));
			}
			if (isUpdate)
			{
				return sqlBuilder.GetUpdateExpression();
			}
			return sqlBuilder.GetInsertExpression();
		}

		private SqlCommand GetInsertUpdateItemTransactionCommand(bool isUpdate)
		{
			SqlParameterCollection parameters;
			if (isUpdate)
			{
				if (updateCommand != null)
				{
					updateCommand = null;
				}
				updateCommand = new SqlCommand(GetInsertUpdateItemTransactionText(isUpdate: true), base.DBConfig.Connection);
				updateCommand.CommandType = CommandType.Text;
				parameters = updateCommand.Parameters;
			}
			else
			{
				if (insertCommand != null)
				{
					insertCommand = null;
				}
				insertCommand = new SqlCommand(GetInsertUpdateItemTransactionText(isUpdate: false), base.DBConfig.Connection);
				insertCommand.CommandType = CommandType.Text;
				parameters = insertCommand.Parameters;
			}
			parameters.Add("@SysDocID", SqlDbType.NVarChar);
			parameters.Add("@VoucherID", SqlDbType.NVarChar);
			parameters.Add("@PartyID", SqlDbType.NVarChar);
			parameters.Add("@PartyType", SqlDbType.NVarChar);
			parameters.Add("@SysDocType", SqlDbType.TinyInt);
			parameters.Add("@SalesFlow", SqlDbType.TinyInt);
			parameters.Add("@IsExport", SqlDbType.Bit);
			parameters.Add("@TransactionDate", SqlDbType.DateTime);
			parameters.Add("@SalespersonID", SqlDbType.NVarChar);
			parameters.Add("@RequiredDate", SqlDbType.DateTime);
			parameters.Add("@ShippingAddressID", SqlDbType.NVarChar);
			parameters.Add("@BillingAddressID", SqlDbType.NVarChar);
			parameters.Add("@CustomerAddress", SqlDbType.NVarChar);
			parameters.Add("@ShipToAddress", SqlDbType.NVarChar);
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
			parameters.Add("@JobID", SqlDbType.NVarChar);
			parameters.Add("@CLUserID", SqlDbType.NVarChar);
			parameters.Add("@Port", SqlDbType.NVarChar);
			parameters.Add("@CostCategoryID", SqlDbType.NVarChar);
			parameters.Add("@LocationID", SqlDbType.NVarChar);
			parameters["@SysDocID"].SourceColumn = "SysDocID";
			parameters["@VoucherID"].SourceColumn = "VoucherID";
			parameters["@PartyID"].SourceColumn = "PartyID";
			parameters["@PartyType"].SourceColumn = "PartyType";
			parameters["@SysDocType"].SourceColumn = "SysDocType";
			parameters["@IsExport"].SourceColumn = "IsExport";
			parameters["@SalesFlow"].SourceColumn = "SalesFlow";
			parameters["@TransactionDate"].SourceColumn = "TransactionDate";
			parameters["@SalespersonID"].SourceColumn = "SalespersonID";
			parameters["@RequiredDate"].SourceColumn = "RequiredDate";
			parameters["@ShippingAddressID"].SourceColumn = "ShippingAddressID";
			parameters["@BillingAddressID"].SourceColumn = "BillingAddressID";
			parameters["@CustomerAddress"].SourceColumn = "CustomerAddress";
			parameters["@ShipToAddress"].SourceColumn = "ShipToAddress";
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
			parameters["@JobID"].SourceColumn = "JobID";
			parameters["@CLUserID"].SourceColumn = "CLUserID";
			parameters["@CostCategoryID"].SourceColumn = "CostCategoryID";
			parameters["@Port"].SourceColumn = "Port";
			parameters["@LocationID"].SourceColumn = "LocationID";
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

		private string GetInsertUpdateItemTransactionDetailsText(bool isUpdate)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddInsertUpdateParameters("Item_Transaction_Detail", new FieldValue("SysDocID", "@SysDocID"), new FieldValue("VoucherID", "@VoucherID"), new FieldValue("ProductID", "@ProductID"), new FieldValue("LocationID", "@LocationID"), new FieldValue("RowIndex", "@RowIndex"), new FieldValue("Quantity", "@Quantity"), new FieldValue("UnitPrice", "@UnitPrice"), new FieldValue("Description", "@Description"), new FieldValue("UnitID", "@UnitID"), new FieldValue("UnitQuantity", "@UnitQuantity"), new FieldValue("UnitFactor", "@UnitFactor"), new FieldValue("FactorType", "@FactorType"), new FieldValue("SubunitPrice", "@SubunitPrice"), new FieldValue("RowSource", "@RowSource"), new FieldValue("SourceSysDocID", "@SourceSysDocID"), new FieldValue("SourceVoucherID", "@SourceVoucherID"), new FieldValue("SourceRowIndex", "@SourceRowIndex"), new FieldValue("JobID", "@JobID"), new FieldValue("CostCategoryID", "@CostCategoryID"));
			if (isUpdate)
			{
				return sqlBuilder.GetUpdateExpression();
			}
			return sqlBuilder.GetInsertExpression();
		}

		private SqlCommand GetInsertUpdateItemTransactionDetailsCommand(bool isUpdate)
		{
			SqlParameterCollection parameters;
			if (isUpdate)
			{
				if (updateCommand != null)
				{
					updateCommand = null;
				}
				updateCommand = new SqlCommand(GetInsertUpdateItemTransactionDetailsText(isUpdate: true), base.DBConfig.Connection);
				updateCommand.CommandType = CommandType.Text;
				parameters = updateCommand.Parameters;
			}
			else
			{
				if (insertCommand != null)
				{
					insertCommand = null;
				}
				insertCommand = new SqlCommand(GetInsertUpdateItemTransactionDetailsText(isUpdate: false), base.DBConfig.Connection);
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
			parameters.Add("@JobID", SqlDbType.NVarChar);
			parameters.Add("@CostCategoryID", SqlDbType.NVarChar);
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
			parameters["@JobID"].SourceColumn = "JobID";
			parameters["@CostCategoryID"].SourceColumn = "CostCategoryID";
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

		private bool ValidateData(ItemTransactionData journalData)
		{
			return true;
		}

		public bool InsertUpdateItemTransaction(ItemTransactionData ItemTransactionData, bool isUpdate)
		{
			bool flag = true;
			SqlCommand insertUpdateItemTransactionCommand = GetInsertUpdateItemTransactionCommand(isUpdate);
			string text = "";
			try
			{
				DataRow dataRow = ItemTransactionData.ItemTransactionTable.Rows[0];
				string text2 = dataRow["VoucherID"].ToString();
				string sysDocID = dataRow["SysDocID"].ToString();
				if (isUpdate && !AllowModify(sysDocID, text2))
				{
					throw new CompanyException("Some items in this transaction has been already invoiced. You are not able to modify.", 1047);
				}
				SqlTransaction sqlTransaction = base.DBConfig.StartNewTransaction();
				bool flag2 = false;
				if (dataRow["IsExport"] != DBNull.Value)
				{
					flag2 = bool.Parse(dataRow["IsExport"].ToString());
				}
				if (dataRow["SourceDocType"] != DBNull.Value)
				{
					byte.Parse(dataRow["SourceDocType"].ToString());
				}
				string text3 = "";
				text3 = ((!flag2) ? new CompanyOption(base.DBConfig).GetCompanyOptionValue(56.ToString()).ToString() : new CompanyOption(base.DBConfig).GetCompanyOptionValue(57.ToString()).ToString());
				bool result = false;
				object obj = null;
				obj = new CompanyOption(base.DBConfig).GetCompanyOptionValue(CompanyOptionsEnum.AllowESCreatefromPickList, null);
				if (obj != null)
				{
					bool.TryParse(obj.ToString(), out result);
				}
				SalesFlows salesFlows = SalesFlows.DirectInvoice;
				if (text3 != "")
				{
					salesFlows = (SalesFlows)int.Parse(text3.ToString());
				}
				_ = 2;
				if (!isUpdate && new SystemDocuments(base.DBConfig).ExistDocumentNumber("Item_Transaction", "VoucherID", dataRow["SysDocID"].ToString(), text2, sqlTransaction))
				{
					base.DBConfig.EndTransaction(result: false);
					throw new CompanyException("Document number already exist.", 1046);
				}
				foreach (DataRow row in ItemTransactionData.ItemTransactionDetailTable.Rows)
				{
					row["SysDocID"] = dataRow["SysDocID"];
					row["VoucherID"] = dataRow["VoucherID"];
					text = row["ProductID"].ToString();
					string text4 = "";
					object fieldValue = new Databases(base.DBConfig).GetFieldValue("Product", "UnitID", "ProductID", text, sqlTransaction);
					if (fieldValue != null)
					{
						text4 = fieldValue.ToString();
					}
					if (text4 != "" && row["UnitID"] != DBNull.Value && row["UnitID"].ToString() != text4)
					{
						DataRow obj2 = new Products(base.DBConfig).GetProductUnitRow(text, row["UnitID"].ToString()) ?? throw new CompanyException("One of the selected units is not assigned to item.\nItem:" + text + "\nUnit:" + row["UnitID"].ToString());
						float num = float.Parse(obj2["Factor"].ToString());
						string text5 = obj2["FactorType"].ToString();
						float num2 = float.Parse(row["Quantity"].ToString());
						row["UnitFactor"] = num;
						row["FactorType"] = text5;
						row["UnitQuantity"] = row["Quantity"];
						num2 = ((!(text5 == "M")) ? float.Parse(Math.Round(num2 * num, 5).ToString()) : float.Parse(Math.Round(num2 / num, 5).ToString()));
						row["Quantity"] = num2;
					}
				}
				insertUpdateItemTransactionCommand.Transaction = sqlTransaction;
				flag = (isUpdate ? (flag & Update(ItemTransactionData, "Item_Transaction", insertUpdateItemTransactionCommand)) : (flag & Insert(ItemTransactionData, "Item_Transaction", insertUpdateItemTransactionCommand)));
				insertUpdateItemTransactionCommand = GetInsertUpdateItemTransactionDetailsCommand(isUpdate: false);
				insertUpdateItemTransactionCommand.Transaction = sqlTransaction;
				if (isUpdate)
				{
					flag &= DeleteItemTransactionDetailsRows(sysDocID, text2, isDeletingTransaction: false, sqlTransaction);
				}
				if (ItemTransactionData.Tables["Item_Transaction_Detail"].Rows.Count > 0)
				{
					flag &= Insert(ItemTransactionData, "Item_Transaction_Detail", insertUpdateItemTransactionCommand);
				}
				if (!flag)
				{
					throw new Exception("Failed to save the transaction because of an unexpected error. Please try again.");
				}
				flag &= UpdateTableRowInsertUpdateInfo("Item_Transaction", "SysDocID", dataRow["SysDocID"].ToString(), "VoucherID", dataRow["VoucherID"].ToString(), sqlTransaction, !isUpdate);
				string entityName = "Item Transaction";
				if (isUpdate)
				{
					flag &= AddActivityLog(entityName, text2, sysDocID, ActivityTypes.Update, sqlTransaction);
					return flag;
				}
				flag &= AddActivityLog(entityName, text2, sysDocID, ActivityTypes.Add, sqlTransaction);
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

		private GLData CreateDNGLData(ItemTransactionData transactionData, SqlTransaction sqlTransaction)
		{
			try
			{
				GLData gLData = new GLData();
				DataRow dataRow = transactionData.ItemTransactionTable.Rows[0];
				int currencyDecimalPoints = new CompanyInformations(base.DBConfig).CurrencyDecimalPoints;
				string value = dataRow["PartyID"].ToString();
				string text = dataRow["SysDocID"].ToString();
				string text2 = dataRow["VoucherID"].ToString();
				string value2 = dataRow["JobID"].ToString();
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
				dataRow3["JournalID"] = 0;
				dataRow3["JournalDate"] = dataRow["TransactionDate"];
				dataRow3["SysDocID"] = dataRow["SysDocID"];
				dataRow3["VoucherID"] = dataRow["VoucherID"];
				dataRow3["Reference"] = dataRow["Reference"];
				dataRow3["Narration"] = "Delivery Note - " + text2;
				dataRow3.EndEdit();
				gLData.JournalTable.Rows.Add(dataRow3);
				decimal num = default(decimal);
				Hashtable hashtable = new Hashtable();
				ArrayList arrayList = new ArrayList();
				decimal d = default(decimal);
				foreach (DataRow row in transactionData.ItemTransactionDetailTable.Rows)
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
						dataRow4["JobID"] = value2;
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
					dataRow4["JobID"] = value2;
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

		public ItemTransactionData GetItemTransactionByID(string sysDocID, string voucherID)
		{
			try
			{
				ItemTransactionData itemTransactionData = new ItemTransactionData();
				string textCommand = "SELECT IT.*  FROM Item_Transaction IT LEFT JOIN Customer C ON IT.PartyID = C.CustomerID  LEFT JOIN Vendor V ON IT.PartyID = V.VendorID  \r\n               WHERE IT.VoucherID='" + voucherID + "' AND IT.SysDocID='" + sysDocID + "'";
				FillDataSet(itemTransactionData, "Item_Transaction", textCommand);
				if (itemTransactionData == null || itemTransactionData.Tables.Count == 0 || itemTransactionData.Tables["Item_Transaction"].Rows.Count == 0)
				{
					return null;
				}
				textCommand = "SELECT ITD.ProductID [Item Code],ITD.* from Item_Transaction_Detail ITD INNER JOIN Item_Transaction IT ON IT.SysDocID=ITD.SysDocID AND IT.VoucherID=ITD.VoucherID\r\n\t\t\t\t\t\tWHERE ITD.VoucherID='" + voucherID + "' AND ITD.SysDocID='" + sysDocID + "'";
				FillDataSet(itemTransactionData, "Item_Transaction_Detail", textCommand);
				return itemTransactionData;
			}
			catch
			{
				throw;
			}
		}

		internal bool DeleteItemTransactionDetailsRows(string sysDocID, string voucherID, bool isDeletingTransaction, SqlTransaction sqlTransaction)
		{
			bool flag = true;
			try
			{
				new ItemTransactionData();
				string commandText = "DELETE FROM Item_Transaction_Detail WHERE SysDocID = '" + sysDocID + "' AND VoucherID = '" + voucherID + "'";
				return flag & Delete(commandText, sqlTransaction);
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

		public bool VoidItemTransaction(string sysDocID, string voucherID, bool isVoid)
		{
			bool flag = true;
			try
			{
				SqlTransaction sqlTransaction = base.DBConfig.StartNewTransaction();
				bool flag2 = false;
				object fieldValue = new Databases(base.DBConfig).GetFieldValue("Item_Transaction", "IsExport", "SysDocID", sysDocID, "VoucherID", voucherID, sqlTransaction);
				if (fieldValue != null && fieldValue.ToString() != "")
				{
					flag2 = bool.Parse(fieldValue.ToString());
				}
				bool result = false;
				object obj = null;
				obj = new CompanyOption(base.DBConfig).GetCompanyOptionValue(CompanyOptionsEnum.AllowESCreatefromPickList, null);
				if (obj != null)
				{
					bool.TryParse(obj.ToString(), out result);
				}
				string text = "";
				text = ((!flag2) ? new CompanyOption(base.DBConfig).GetCompanyOptionValue(56.ToString()).ToString() : new CompanyOption(base.DBConfig).GetCompanyOptionValue(57.ToString()).ToString());
				SalesFlows salesFlows = SalesFlows.DirectInvoice;
				if (text != "")
				{
					salesFlows = (SalesFlows)int.Parse(text.ToString());
				}
				if (!AllowModify(sysDocID, voucherID))
				{
					throw new CompanyException("Some items in this transaction has been already invoiced. You are not able to modify.", 1047);
				}
				flag &= new Journal(base.DBConfig).VoidJournal(sysDocID, voucherID, isVoid, sqlTransaction);
				string exp = "UPDATE Delivery_Note SET IsVoid = '" + isVoid.ToString() + "' WHERE SysDocID='" + sysDocID + "' AND VoucherID = '" + voucherID + "'";
				flag &= (ExecuteNonQuery(exp, sqlTransaction) > 0);
				ItemTransactionData itemTransactionData = new ItemTransactionData();
				exp = "SELECT * FROM Delivery_Note_Detail\r\n                              WHERE SysDocID = '" + sysDocID + "' AND VoucherID = '" + voucherID + "'";
				FillDataSet(itemTransactionData, "Delivery_Note_Detail", exp, sqlTransaction);
				if (salesFlows != SalesFlows.SOThenDNThenInvoice)
				{
					string text2 = "";
					string text3 = "";
					foreach (DataRow row in itemTransactionData.ItemTransactionDetailTable.Rows)
					{
						row["ProductID"].ToString();
						text2 = row["SourceVoucherID"].ToString();
						text3 = row["SourceSysDocID"].ToString();
						int result2 = 0;
						if (!(text2 == "") && !(text3 == ""))
						{
							int.TryParse(row["SourceRowIndex"].ToString(), out result2);
							float result3 = 0f;
							if (row["UnitQuantity"] != DBNull.Value)
							{
								float.TryParse(row["UnitQuantity"].ToString(), out result3);
							}
							else
							{
								float.TryParse(row["Quantity"].ToString(), out result3);
							}
							flag &= new SalesInvoice(base.DBConfig).UpdateRowShippedQuantity(text3, text2, result2, -1f * result3, sqlTransaction);
							flag &= new SalesInvoice(base.DBConfig).ReOpenInvoice(text3, text2, sqlTransaction);
						}
					}
				}
				else
				{
					string text4 = "";
					string text5 = "";
					string text6 = "";
					foreach (DataRow row2 in itemTransactionData.ItemTransactionDetailTable.Rows)
					{
						text6 = row2["ProductID"].ToString();
						text4 = row2["SourceVoucherID"].ToString();
						text5 = row2["SourceSysDocID"].ToString();
						int result4 = 0;
						if (!(text4 == "") && !(text5 == ""))
						{
							int.TryParse(row2["SourceRowIndex"].ToString(), out result4);
							float result5 = 0f;
							if (row2["UnitQuantity"] != DBNull.Value)
							{
								float.TryParse(row2["UnitQuantity"].ToString(), out result5);
							}
							else
							{
								float.TryParse(row2["Quantity"].ToString(), out result5);
							}
							float num = new Products(base.DBConfig).GetReservedQuantity(text6, sqlTransaction) + result5;
							if (num < 0f)
							{
								num = 0f;
							}
							flag &= new Products(base.DBConfig).UpdateReservedQuantity(text6, num, sqlTransaction);
							if (flag2)
							{
								if (!result)
								{
									flag &= new SalesOrder(base.DBConfig).UpdateRowShippedQuantity(text5, text4, result4, -1f * result5, sqlTransaction);
								}
							}
							else
							{
								flag &= new SalesOrder(base.DBConfig).UpdateRowShippedQuantity(text5, text4, result4, -1f * result5, sqlTransaction);
							}
							if (flag)
							{
								exp = (result ? ("UPDATE Export_PickList SET Status=1 WHERE SysDocID='" + text5 + "' AND VoucherID='" + text4 + "' ") : ("UPDATE Sales_Order SET Status=1 WHERE SysDocID='" + text5 + "' AND VoucherID='" + text4 + "' "));
								flag &= (ExecuteNonQuery(exp, sqlTransaction) > 0);
							}
						}
					}
				}
				exp = "DELETE FROM Invoice_DNote WHERE DnoteSysDocID='" + sysDocID + "' AND DNoteVoucherID='" + voucherID + "'";
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

		public bool DeleteItemTransaction(string sysDocID, string voucherID)
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
				bool result = false;
				bool.TryParse(new Databases(base.DBConfig).GetFieldValue("Delivery_Note", "IsVoid", "SysDocID", sysDocID, "VoucherID", voucherID, sqlTransaction).ToString(), out result);
				if (!result)
				{
					flag &= VoidItemTransaction(sysDocID, voucherID, isVoid: true);
				}
				flag &= DeleteItemTransactionDetailsRows(sysDocID, voucherID, isDeletingTransaction: true, sqlTransaction);
				flag &= new Journal(base.DBConfig).DeleteJournal(sysDocID, voucherID, sqlTransaction);
				text = "DELETE FROM Delivery_Note WHERE SysDocID='" + sysDocID + "' AND VoucherID = '" + voucherID + "'";
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
				string textCommand = "SELECT Quantity,UnitQuantity,QuantityReturned,SalesFlow FROM Delivery_Note_Detail DND INNER JOIN Delivery_Note DN ON DN.SysDocID=DND.SysDocID AND DN.VoucherID=DND.VoucherID\r\n                                WHERE DND.SysDocID='" + sysDocID + "' AND DND.VoucherID='" + voucherID + "' AND RowIndex=" + rowIndex.ToString();
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
				textCommand = "UPDATE Delivery_Note_Detail SET QuantityReturned=" + result2.ToString() + " WHERE SysDocID='" + sysDocID + "' AND VoucherID='" + voucherID + "' AND RowIndex=" + rowIndex.ToString();
				return ExecuteNonQuery(textCommand, sqlTransaction) > 0;
			}
			catch
			{
				return false;
			}
		}

		public DataSet GetMaxVoucherID()
		{
			DataSet dataSet = new DataSet();
			try
			{
				string textCommand = "select ROW_NUMBER() OVER(ORDER BY IT.SysDocID ASC) AS ID,MAX (IT.VoucherID)[VoucherID],IT.SysDocID from Item_Transaction IT LEFT JOIN Item_Transaction IT1 ON IT.SysDocID=IT1.SysDocID  group by IT.SysDocID";
				FillDataSet(dataSet, "MAXVoucherIDS", textCommand);
				dataSet.Tables["MAXVoucherIDS"].PrimaryKey = new DataColumn[1]
				{
					dataSet.Tables["MAXVoucherIDS"].Columns["ID"]
				};
				return dataSet;
			}
			catch
			{
				return dataSet;
			}
		}

		public DataSet GetUninvoicedItemTransactions(string customerID, bool isExport)
		{
			return GetUninvoicedItemTransactions("", customerID, isExport);
		}

		public DataSet GetUninvoicedItemTransactions(string sysDocID, string customerID, bool isExport)
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
				string str = "SELECT DN.SysDocID [Doc ID],DN.VoucherID [Number],J.JobName, DN.TransactionDate AS [Date],DN.CustomerID + '-' + C.CustomerName AS [Customer], DN.Reference AS [SO Number]\r\n                              FROM Delivery_Note DN\r\n                              INNER JOIN Delivery_Note_Detail DND ON DND.SysDocID = DN.SysDocID AND DND.VoucherID = DN.VoucherID \r\n                              INNER JOIN Customer C ON DN.CustomerID=C.CustomerID    \r\n\t\t\t\t\t\t\t  LEFT JOIN JOB J ON DN.JobID=J.JobID                         \r\n                              WHERE ISNULL(DN.IsVoid,'False')='False' AND ISNULL(DN.IsInvoiced,'False')='False' AND ISNULL(DN.SalesFlow,0) = 2 \r\n                                 ";
				if (customerID != "")
				{
					str = str + " AND DN.CustomerID='" + customerID + "' ";
				}
				if (!string.IsNullOrEmpty(text))
				{
					str = str + " AND C.CustomerClassID IN (" + text + ") ";
				}
				str += "  GROUP BY DN.SysDocID ,DN.VoucherID, DN.TransactionDate ,DN.CustomerID , C.CustomerName ,J.JobName , DN.Reference \r\n\t\t\t\t\t\t\t  HAVING SUM(Quantity - ISNULL(QuantityReturned,0)) > 0  ";
				FillDataSet(dataSet, "Item_transaction", str);
				return dataSet;
			}
			catch
			{
				throw;
			}
		}

		public DataSet GetItemTransactionToPrint(string sysDocID, string voucherID, bool showLotDetail)
		{
			return GetItemTransactionToPrint(sysDocID, new string[1]
			{
				voucherID
			}, showLotDetail);
		}

		public DataSet GetItemTransactionToPrint(string sysDocID, string[] voucherID, bool showLotDetail)
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
				string cmdText = "SELECT DISTINCT SysDocID,VoucherID,SI.CustomerID,CustomerName,CustomerAddress,CA.ContactName,TransactionDate,SI.ContainerNumber,\r\n                            SI.SalesPersonID,RequiredDate,CA.AddressPrintFormat AS ShippingAddress,ShippingMethodName,\r\n                            ISNULL(SI.CurrencyID,(SELECT CurrencyID FROM Currency WHERE IsBase='True')) AS CurrencyID,\r\n                            SI.TermID,TermName,IsVoid,SI.Reference,Discount AS Discount,CA.Phone1,CA.Mobile,CA.Fax,CA.ContactName,\r\n                            Total AS Total,SI.PONumber,SI.Note, SI.InvoiceVoucherID, D.Drivername,D.Note AS [Driver No.],\r\n                            (SELECT TOP 1 RegistrationNumber FROM Vehicle WHERE VehicleID = SI.VehicleID) RegistrationNumber, SI.Reference2,\r\n                            SI.DateCreated,SI.DateUpdated,SI.CreatedBy,SI.UpdatedBy,V.VehicleName,V.VehicleID,J.JobName,JC.CostCategoryName,P.PortName,SI.VehicleID AS OutVehicle\r\n                            FROM  Delivery_Note SI INNER JOIN Customer ON SI.CustomerID=Customer.CustomerID\r\n                            LEFT OUTER JOIN Payment_Term PT ON SI.TermID=PT.PaymentTermID\r\n                            LEFT OUTER JOIN Customer_Address CA ON CA.AddressID=ShippingAddressID AND CA.CustomerID=SI.CustomerID\r\n                            LEFT OUTER JOIN Shipping_Method SM ON SM.ShippingMethodID=SI.ShippingMethodID\r\n                            LEFT OUTER JOIN Driver D ON D.DriverID=SI.DriverID\r\n                            LEFT OUTER JOIN Vehicle V ON V.VehicleID=SI.VehicleID\r\n                            LEFT JOIN Job J ON J.JobID=SI.JobID\r\n                            LEFT JOIN Job_Cost_Category JC ON JC.CostCategoryID=SI.CostCategoryID\r\n                            LEFT JOIN Port P ON P.PortID=SI.Port\r\n                            WHERE SysDocID = '" + sysDocID + "' AND VoucherID IN (" + text + ")";
				SqlCommand sqlCommand = new SqlCommand(cmdText);
				FillDataSet(dataSet, "Item_transaction", sqlCommand);
				if (dataSet == null || dataSet.Tables.Count == 0 || dataSet.Tables["Item_transaction"].Rows.Count == 0)
				{
					return null;
				}
				cmdText = "SELECT DISTINCT DND.SysDocID, DND.VoucherID, DND.ProductID, DND.Description, DND.LocationID, ISNULL(DND.UnitQuantity, DND.Quantity) AS Quantity, \r\n                        Product.BrandID, Product.CategoryID,Product.ClassID, Product.QuantityPerUnit,\r\n                        DND.UnitPrice, Product.Size, ISNULL(DND.UnitQuantity, DND.Quantity) * DND.UnitPrice AS Total, DND.UnitID,\r\n                        (SELECT CASE WHEN FactorType='D' THEN ISNULL(UnitQuantity,DND.Quantity)*Factor ELSE ISNULL(UnitQuantity,DND.Quantity)/Factor END FROM Product_Unit PU WHERE PU.UnitID=DND.UnitID AND PU.ProductID= DND.ProductID ) AS Weight,  \r\n                        DND.RowIndex,DND.SysDocID as OrderSysdocID,DND.VoucherID as OrderVoucherID,C.CountryName,PC.CategoryName,J.JobName,JC.CostCategoryName\r\n                        FROM   Delivery_Note_Detail DND INNER JOIN Product ON DND.ProductID = Product.ProductID\r\n                        LEFT OUTER JOIN Product_Lot_Sales PL ON  Product.ProductID =  PL.ItemCode \r\n                        AND PL.DocID = DND.SysDocID AND PL.InvoiceNumber = DND.VoucherID\r\n                        LEFT  OUTER JOIN Product_Lot PLt ON  PLT.LotNumber =  PL.LotNo\r\n                        LEFT OUTER JOIN Product_Category PC ON PC.CategoryID=Product.CategoryID\r\n                        LEFT OUTER JOIN Country C ON Product.Origin=C.CountryID \r\n                        LEFT JOIN Job J ON J.JobID=DND.JobID\r\n                        LEFT JOIN Job_Cost_Category JC ON JC.CostCategoryID=DND.CostCategoryID\r\n                        WHERE DND.SysDocID='" + sysDocID + "' AND DND.VoucherID IN (" + text + ")  ORDER BY DND.RowIndex";
				FillDataSet(dataSet, "Item_Transaction_Detail", cmdText);
				dataSet.Relations.Add("CustomerItemTransaction", new DataColumn[2]
				{
					dataSet.Tables["Item_transaction"].Columns["SysDocID"],
					dataSet.Tables["Item_transaction"].Columns["VoucherID"]
				}, new DataColumn[2]
				{
					dataSet.Tables["Item_Transaction_Detail"].Columns["SysDocID"],
					dataSet.Tables["Item_Transaction_Detail"].Columns["VoucherID"]
				}, createConstraints: false);
				dataSet.Tables["Item_transaction"].Columns.Add("TotalInWords", typeof(string));
				foreach (DataRow row in dataSet.Tables["Item_transaction"].Rows)
				{
					decimal result = default(decimal);
					decimal.TryParse(row["Total"].ToString(), out result);
					row["TotalInWords"] = NumToWord.GetNumInWords(result);
				}
				if (showLotDetail)
				{
					string text2 = "";
					string text3 = "";
					if (dataSet.Tables["Item_Transaction_Detail"].Rows.Count > 0)
					{
						text2 = dataSet.Tables["Item_Transaction_Detail"].Rows[0]["OrderSysdocID"].ToString();
						text3 = dataSet.Tables["Item_Transaction_Detail"].Rows[0]["OrderVoucherID"].ToString();
					}
					cmdText = "SELECT PL.*,PLS.SoldQty FROM Product_Lot PL LEFT JOIN Product_Lot_Sales PLS ON PL.LotNumber=PLS.LotNo WHERE PLS.DocID=\r\n                            '" + text2 + "' AND PLS.InvoiceNumber IN ('" + text3 + "')";
					FillDataSet(dataSet, "ProductLot", cmdText);
					dataSet.Relations.Add("ProductLotRel", new DataColumn[1]
					{
						dataSet.Tables["Item_Transaction_Detail"].Columns["ProductID"]
					}, new DataColumn[1]
					{
						dataSet.Tables["ProductLot"].Columns["ItemCode"]
					}, createConstraints: false);
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
			string exp = "SELECT COUNT(*) FROM Invoice_DNote ID WHERE SourceDocType = 6 AND DnoteSysDocID = '" + sysDocID + "' AND DNOTEVoucherID = '" + voucherNumber + "'";
			object obj = ExecuteScalar(exp);
			if (obj != null && obj.ToString() != "" && int.Parse(obj.ToString()) > 0)
			{
				return false;
			}
			exp = "SELECT COUNT(*) FROM Delivery_Note WHERE (ISNULL(IsInvoiced,'False')= 'True' OR ISNULL(IsShipped,'False')='True') AND (SysDocID = '" + sysDocID + "' AND VoucherID = '" + voucherNumber + "')";
			obj = ExecuteScalar(exp);
			if (obj != null && obj.ToString() != "" && int.Parse(obj.ToString()) > 0)
			{
				return false;
			}
			exp = "SELECT COUNT(*) FROM Delivery_Note WHERE (ISNULL(IsInvoiced,'False')= 'True' OR ISNULL(IsShipped,'False')='True') AND (SysDocID = '" + sysDocID + "' AND VoucherID = '" + voucherNumber + "')";
			obj = ExecuteScalar(exp);
			if (obj != null && obj.ToString() != "" && int.Parse(obj.ToString()) > 0)
			{
				return false;
			}
			exp = "SELECT ISNULL(SUM(ISNULL(QuantityReturned,0) + ISNULL(QuantityShipped,0)),0) AS Quantity FROM Delivery_Note_Detail\r\n                        WHERE SysDocID = '" + sysDocID + "' AND VoucherID = '" + voucherNumber + "'";
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
			string text3 = "SELECT    INV.SysDocID [Doc ID],INV.VoucherID [Doc Number],INV.PartyID [ Party ID],\r\n                            case PartyType when 'C' THEN  CustomerName WHEN 'V' THEN V.VendorName  END AS [PARTY NAME], TransactionDate,\r\n                            INV.SalespersonID [Salesperson],SUM(DND.Quantity) AS Quantity, INV.Reference AS Ref1,INV.Reference2 AS Ref2\r\n                            FROM   Item_Transaction INV\r\n                            INNER JOIN Item_Transaction_Detail DND ON DND.SysDocID=INV.SysDocID AND DND.VoucherID=INV.VoucherID\r\n                            LEFT JOIN Customer ON CUSTOMER.CustomerID=INV.PartyID\r\n                            LEFT JOIN Vendor V ON V.VendorID=INV.PartyID ";
			if (from != DateTime.MinValue)
			{
				text3 = text3 + " WHERE TransactionDate Between '" + text + "' AND '" + text2 + "'";
			}
			if (!showVoid)
			{
				text3 += " AND ISNULL(IsVoid,'False')='False'";
			}
			text3 += " GROUP BY IsVoid,INV.SysDocID,INV.VoucherID,INV.PartyID ,CustomerName,VendorName,TransactionDate,INV.SalespersonID, INV.Reference, INV.Reference2, PartyType";
			SqlCommand sqlCommand = new SqlCommand(text3);
			FillDataSet(dataSet, "Item_transaction", sqlCommand);
			return dataSet;
		}

		public DataSet GetItemTransactionList(string customerID, SysDocTypes docType)
		{
			try
			{
				DataSet dataSet = new DataSet();
				int num = (int)docType;
				string text = "SELECT DO.SysDocID [Doc ID],DO.VoucherID [Number], TransactionDate AS [Date],DO.PartyType, \r\n                                CASE WHEN DO.PartyType='C' THEN DO.PartyID + '-' + CUS.CustomerName\r\n                                 WHEN  DO.PartyType='V' THEN DO.PartyID + '-' + VEN.VendorName END \r\n                                 AS Agent,\r\n                                 CASE WHEN DO.PartyType='C' THEN DO.PartyID\r\n                                 ELSE NULL END  AS Customer\r\n                                FROM Item_Transaction DO  LEFT JOIN Customer CUS ON DO.PartyID=CUS.CustomerID\r\n                                LEFT JOIN Vendor VEN ON DO.PartyID=VEN.VendorID \r\n\t\t\t\t\t\t\t\t WHERE  NOT\r\n\t\t\t\t\t\t\t\t EXISTS(select ListVoucherID,ListSysDocID from Delivery_Note_Detail DN where DO.VoucherID=DN.ListVoucherID and DO.SysDocID=DN.ListSysDocID)\r\n\t\t\t\t\t\t\t\t AND \r\n\t\t\t\t\t\t\t\t   NOT\r\n\t\t\t\t\t\t\t\t EXISTS(select ListVoucherID,ListSysDocID from Purchase_Receipt_Detail PR where DO.VoucherID=PR.ListVoucherID and DO.SysDocID=PR.ListSysDocID)\r\n\t\t\t\t\t\t\t\t  \r\n\t\t\t\t\t\t\t\t  AND \r\n\t\t\t\t\t\t\t\t   NOT\r\n\t\t\t\t\t\t\t\t EXISTS(select ListVoucherID,ListSysDocID from Inventory_Adjustment_Detail AD where DO.VoucherID=AD.ListVoucherID and DO.SysDocID=AD.ListSysDocID)\r\n\t\t\t\t\t\t\t\t   AND \r\n\t\t\t\t\t\t\t\t   NOT\r\n\t\t\t\t\t\t\t\t EXISTS(select ListVoucherID,ListSysDocID from Inventory_Transfer_Detail AD where DO.VoucherID=AD.ListVoucherID and DO.SysDocID=AD.ListSysDocID)\r\n\r\n\t\t\t\t\t\t\t\t  AND \r\n\t\t\t\t\t\t\t\t   NOT\r\n\t\t\t\t\t\t\t\t EXISTS(select ListVoucherID,ListSysDocID from Sales_Invoice_Detail AD where DO.VoucherID=AD.ListVoucherID and DO.SysDocID=AD.ListSysDocID) ";
				if (docType != SysDocTypes.None)
				{
					text = text + " AND DO.SysDocType IN ('" + num.ToString() + "')";
				}
				if (customerID != "")
				{
					text = text + " AND (DO.PartyID = '" + customerID + "')";
				}
				FillDataSet(dataSet, "Item_Transaction", text);
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
				string textCommand = "SELECT DO.customerID, DOD.SysDocID [Doc ID], DOD.VoucherID,ProductID,Description,DOD.UnitID,RowIndex,UnitQuantity,LocationID,\r\n                                ISNULL(UnitQuantity,Quantity) - ISNULL(QuantityReturned,0) AS Quantity,ISNULL(QuantityShipped,0) AS QuantityShipped\r\n                                FROM Delivery_Note_Detail DOD INNER JOIN Delivery_Note DO ON DO.SysDocID=DOD.SysDocID AND DO.VoucherID=DOD.VoucherID                              \r\n                             WHERE DOD.SysDocID='" + sysDocID + "' AND DOD.VoucherID='" + voucherID + "'";
				FillDataSet(dataSet, "Item_Transaction_Detail", textCommand);
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
				string textCommand = "SELECT DISTINCT DO.customerID, DOD.SysDocID [Doc ID], DOD.VoucherID,DOD.ProductID,DOD.Description,DOD.UnitID,DOD.RowIndex,DOD.UnitQuantity,DOD.LocationID,\r\n                                ISNULL(DOD.UnitQuantity,DOD.Quantity) - ISNULL(QuantityReturned,0) AS Quantity,ISNULL(QuantityShipped,0) AS QuantityShipped\r\n                                FROM Delivery_Note_Detail DOD INNER JOIN Export_PackingList_Detail PKD ON PKD.SourceSysDocID=DOD.SysDocID AND PKD.SourceVoucherID=DOD.VoucherID\r\n                                INNER JOIN Delivery_Note DO ON DO.SysDocID=DOD.SysDocID AND DO.VoucherID=DOD.VoucherID\r\n\t\t\t\t\t\t\t\tWHERE PKD.SysDocID= '" + sysDocID + "' AND PKD.VoucherID = '" + voucherID + "' AND ISNULL(DO.IsInvoiced,'False') = 'False' ";
				FillDataSet(dataSet, "Item_Transaction_Detail", textCommand);
				return dataSet;
			}
			catch
			{
				throw;
			}
		}

		public string GetNextDocNumber(string SysDocID)
		{
			DataSet dataSet = new DataSet();
			string empty = string.Empty;
			string empty2 = string.Empty;
			string result = "";
			empty2 = "SELECT MAX(ISNULL(VoucherID, 0)) AS ID FROM Item_Transaction where SysDocID ='" + SysDocID + "' Group BY VoucherID, DateCreated Order By DateCreated Desc ";
			FillDataSet(dataSet, "VOUCHER_TABLE", empty2);
			if (dataSet != null && dataSet.Tables.Count > 0 && dataSet.Tables[0].Rows.Count > 0)
			{
				empty = dataSet.Tables[0].Rows[0]["ID"].ToString();
				int indexofNumber = getIndexofNumber(empty);
				int num = Convert.ToInt32(empty.Substring(indexofNumber, empty.Length - indexofNumber)) + 1;
				string text = empty.Substring(0, indexofNumber);
				int num2 = empty.Length - text.Length - num.ToString().Length;
				string text2 = "";
				for (int i = 0; i < num2; i++)
				{
					text2 += "0";
				}
				result = text + text2 + num.ToString();
			}
			return result;
		}

		private int getIndexofNumber(string cell)
		{
			int num = -1;
			foreach (char c in cell)
			{
				num++;
				if (char.IsDigit(c))
				{
					return num;
				}
			}
			return num;
		}
	}
}
