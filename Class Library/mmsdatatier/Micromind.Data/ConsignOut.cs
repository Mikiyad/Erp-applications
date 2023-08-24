using Micromind.Common;
using Micromind.Common.Data;
using Micromind.Common.Libraries;
using System;
using System.Collections;
using System.Data;
using System.Data.SqlClient;

namespace Micromind.Data
{
	public sealed class ConsignOut : StoreObject
	{
		private const string CONSIGNOUT_TABLE = "Consign_Out";

		private const string CONSIGNOUTDETAIL_TABLE = "Consign_Out_Detail";

		private const string SYSDOCID_PARM = "@SysDocID";

		private const string VOUCHERID_PARM = "@VoucherID";

		private const string COMPANYID_PARM = "@CompanyID";

		private const string DIVISIONID_PARM = "@DivisionID";

		private const string CUSTOMERID_PARM = "@CustomerID";

		private const string TRANSACTIONDATE_PARM = "@TransactionDate";

		private const string SALESPERSONID_PARM = "@SalespersonID";

		private const string ROWINDEX_PARM = "@RowIndex";

		private const string REQUIREDDATE_PARM = "@RequiredDate";

		private const string SHIPPINGADDRESSID_PARM = "@ShippingAddressID";

		private const string CUSTOMERADDRESS_PARM = "@CustomerAddress";

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

		private const string SOURCEVOUCHERID_PARM = "@SourceVoucherID";

		private const string SOURCESYSDOCID_PARM = "@SourceSysDocID";

		private const string SOURCEROWINDEX_PARM = "@SourceRowIndex";

		private const string SOURCEDOCTYPE_PARM = "@SourceDocType";

		private const string CREATEDBY_PARM = "@CreatedBy";

		private const string DATECREATED_PARM = "@DateCreated";

		private const string UPDATEDBY_PARM = "@UpdatedBy";

		private const string DATEUPDATED_PARM = "@DateUpdated";

		private const string MARKETPRICE_PARM = "@MarketPrice";

		public ConsignOut(Config config)
			: base(config)
		{
		}

		private string GetInsertUpdateConsignOutText(bool isUpdate)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddInsertUpdateParameters("Consign_Out", new FieldValue("SysDocID", "@SysDocID", isUpdateConditionField: true), new FieldValue("VoucherID", "@VoucherID", isUpdateConditionField: true), new FieldValue("DivisionID", "@DivisionID"), new FieldValue("CompanyID", "@CompanyID"), new FieldValue("CustomerID", "@CustomerID"), new FieldValue("TransactionDate", "@TransactionDate"), new FieldValue("SalespersonID", "@SalespersonID"), new FieldValue("RequiredDate", "@RequiredDate"), new FieldValue("ShippingAddressID", "@ShippingAddressID"), new FieldValue("ShippingMethodID", "@ShippingMethodID"), new FieldValue("CustomerAddress", "@CustomerAddress"), new FieldValue("Status", "@Status"), new FieldValue("CurrencyID", "@CurrencyID"), new FieldValue("Discount", "@Discount"), new FieldValue("Total", "@Total"), new FieldValue("PONumber", "@PONumber"), new FieldValue("TermID", "@TermID"), new FieldValue("Reference", "@Reference"), new FieldValue("Note", "@Note"), new FieldValue("SourceDocType", "@SourceDocType"), new FieldValue("SourceSysDocID", "@SourceSysDocID"), new FieldValue("SourceVoucherID", "@SourceVoucherID"));
			if (isUpdate)
			{
				sqlBuilder.AddInsertUpdateParameters("Consign_Out", new FieldValue("DateUpdated", "@DateUpdated", isUpdateConditionField: true, checkForNullValue: true));
			}
			if (isUpdate)
			{
				return sqlBuilder.GetUpdateExpression();
			}
			return sqlBuilder.GetInsertExpression();
		}

		private SqlCommand GetInsertUpdateConsignOutCommand(bool isUpdate)
		{
			SqlParameterCollection parameters;
			if (isUpdate)
			{
				if (updateCommand != null)
				{
					updateCommand = null;
				}
				updateCommand = new SqlCommand(GetInsertUpdateConsignOutText(isUpdate: true), base.DBConfig.Connection);
				updateCommand.CommandType = CommandType.Text;
				parameters = updateCommand.Parameters;
			}
			else
			{
				if (insertCommand != null)
				{
					insertCommand = null;
				}
				insertCommand = new SqlCommand(GetInsertUpdateConsignOutText(isUpdate: false), base.DBConfig.Connection);
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
			parameters.Add("@ShippingAddressID", SqlDbType.NVarChar);
			parameters.Add("@CustomerAddress", SqlDbType.NVarChar);
			parameters.Add("@ShippingMethodID", SqlDbType.NVarChar);
			parameters.Add("@Status", SqlDbType.TinyInt);
			parameters.Add("@CurrencyID", SqlDbType.NVarChar);
			parameters.Add("@TermID", SqlDbType.NVarChar);
			parameters.Add("@Reference", SqlDbType.NVarChar);
			parameters.Add("@PONumber", SqlDbType.NVarChar);
			parameters.Add("@SourceDocType", SqlDbType.TinyInt);
			parameters.Add("@SourceSysDocID", SqlDbType.NVarChar);
			parameters.Add("@SourceVoucherID", SqlDbType.NVarChar);
			parameters.Add("@Discount", SqlDbType.Decimal);
			parameters.Add("@Total", SqlDbType.Decimal);
			parameters.Add("@Note", SqlDbType.NVarChar);
			parameters["@SysDocID"].SourceColumn = "SysDocID";
			parameters["@VoucherID"].SourceColumn = "VoucherID";
			parameters["@DivisionID"].SourceColumn = "DivisionID";
			parameters["@CompanyID"].SourceColumn = "CompanyID";
			parameters["@CustomerID"].SourceColumn = "CustomerID";
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
			parameters["@PONumber"].SourceColumn = "PONumber";
			parameters["@SourceSysDocID"].SourceColumn = "SourceSysDocID";
			parameters["@SourceVoucherID"].SourceColumn = "SourceVoucherID";
			parameters["@SourceDocType"].SourceColumn = "SourceDocType";
			parameters["@Discount"].SourceColumn = "Discount";
			parameters["@Total"].SourceColumn = "Total";
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

		private string GetInsertUpdateConsignOutDetailsText(bool isUpdate)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddInsertUpdateParameters("Consign_Out_Detail", new FieldValue("SysDocID", "@SysDocID"), new FieldValue("VoucherID", "@VoucherID"), new FieldValue("ProductID", "@ProductID"), new FieldValue("LocationID", "@LocationID"), new FieldValue("ConsignLocationID", "@ConsignLocationID"), new FieldValue("RowIndex", "@RowIndex"), new FieldValue("Quantity", "@Quantity"), new FieldValue("UnitPrice", "@UnitPrice"), new FieldValue("Description", "@Description"), new FieldValue("UnitID", "@UnitID"), new FieldValue("UnitQuantity", "@UnitQuantity"), new FieldValue("MarketPrice", "@MarketPrice"), new FieldValue("UnitFactor", "@UnitFactor"), new FieldValue("FactorType", "@FactorType"), new FieldValue("SubunitPrice", "@SubunitPrice"), new FieldValue("SourceDocType", "@SourceDocType"), new FieldValue("SourceSysDocID", "@SourceSysDocID"), new FieldValue("SourceVoucherID", "@SourceVoucherID"), new FieldValue("SourceRowIndex", "@SourceRowIndex"));
			if (isUpdate)
			{
				return sqlBuilder.GetUpdateExpression();
			}
			return sqlBuilder.GetInsertExpression();
		}

		private SqlCommand GetInsertUpdateConsignOutDetailsCommand(bool isUpdate)
		{
			SqlParameterCollection parameters;
			if (isUpdate)
			{
				if (updateCommand != null)
				{
					updateCommand = null;
				}
				updateCommand = new SqlCommand(GetInsertUpdateConsignOutDetailsText(isUpdate: true), base.DBConfig.Connection);
				updateCommand.CommandType = CommandType.Text;
				parameters = updateCommand.Parameters;
			}
			else
			{
				if (insertCommand != null)
				{
					insertCommand = null;
				}
				insertCommand = new SqlCommand(GetInsertUpdateConsignOutDetailsText(isUpdate: false), base.DBConfig.Connection);
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
			parameters.Add("@SourceDocType", SqlDbType.TinyInt);
			parameters.Add("@SourceSysDocID", SqlDbType.NVarChar);
			parameters.Add("@SourceVoucherID", SqlDbType.NVarChar);
			parameters.Add("@SourceRowIndex", SqlDbType.Int);
			parameters.Add("@SubunitPrice", SqlDbType.Decimal);
			parameters.Add("@MarketPrice", SqlDbType.Decimal);
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
			parameters["@SourceSysDocID"].SourceColumn = "SourceSysDocID";
			parameters["@SourceVoucherID"].SourceColumn = "SourceVoucherID";
			parameters["@SourceRowIndex"].SourceColumn = "SourceRowIndex";
			parameters["@SourceDocType"].SourceColumn = "SourceDocType";
			parameters["@MarketPrice"].SourceColumn = "MarketPrice";
			if (isUpdate)
			{
				return updateCommand;
			}
			return insertCommand;
		}

		private bool ValidateData(ConsignOutData journalData)
		{
			return true;
		}

		public bool InsertUpdateConsignOut(ConsignOutData consignOutData, bool isUpdate)
		{
			object obj = null;
			bool flag = true;
			SqlCommand insertUpdateConsignOutCommand = GetInsertUpdateConsignOutCommand(isUpdate);
			try
			{
				DataRow dataRow = consignOutData.ConsignOutTable.Rows[0];
				SqlTransaction sqlTransaction = base.DBConfig.StartNewTransaction();
				string text = dataRow["VoucherID"].ToString();
				string text2 = dataRow["SysDocID"].ToString();
				string value = "";
				obj = new Databases(base.DBConfig).GetFieldValue("System_Document", "ConsignOutLocationID", "SysDocID", text2, sqlTransaction);
				if (obj != null && obj.ToString() != "")
				{
					value = obj.ToString();
				}
				if (string.IsNullOrEmpty(value))
				{
					throw new CompanyException("Consignment out location is not assigned to the document.");
				}
				if (!isUpdate && new SystemDocuments(base.DBConfig).ExistDocumentNumber("Consign_Out", "VoucherID", dataRow["SysDocID"].ToString(), text, sqlTransaction))
				{
					base.DBConfig.EndTransaction(result: false);
					throw new CompanyException("Document number already exist.", 1046);
				}
				bool flag2 = false;
				if (dataRow["IsExport"] != DBNull.Value)
				{
					flag2 = bool.Parse(dataRow["IsExport"].ToString());
				}
				string text3 = "";
				if (flag2)
				{
					obj = new CompanyOption(base.DBConfig).GetCompanyOptionValue(57.ToString());
					if (obj != null)
					{
						text3 = obj.ToString();
					}
				}
				else
				{
					obj = new CompanyOption(base.DBConfig).GetCompanyOptionValue(56.ToString());
					if (obj != null)
					{
						text3 = obj.ToString();
					}
				}
				SalesFlows salesFlows = SalesFlows.DirectInvoice;
				if (text3 != "")
				{
					salesFlows = (SalesFlows)int.Parse(text3.ToString());
				}
				bool flag3 = false;
				if (salesFlows == SalesFlows.SOThenDNThenInvoice)
				{
					flag3 = true;
				}
				foreach (DataRow row in consignOutData.ConsignOutDetailTable.Rows)
				{
					row["SysDocID"] = dataRow["SysDocID"];
					row["VoucherID"] = dataRow["VoucherID"];
					string text4 = row["ProductID"].ToString();
					row["ConsignLocationID"] = value;
					string text5 = "";
					obj = new Databases(base.DBConfig).GetFieldValue("Product", "UnitID", "ProductID", text4, sqlTransaction);
					if (obj != null)
					{
						text5 = obj.ToString();
					}
					if (text5 != "" && row["UnitID"] != DBNull.Value && row["UnitID"].ToString() != text5)
					{
						DataRow obj2 = new Products(base.DBConfig).GetProductUnitRow(text4, row["UnitID"].ToString()) ?? throw new CompanyException("One of the selected units is not assigned to item.\nItem:" + text4 + "\nUnit:" + row["UnitID"].ToString());
						float num = float.Parse(obj2["Factor"].ToString());
						string text6 = obj2["FactorType"].ToString();
						float num2 = float.Parse(row["Quantity"].ToString());
						row["UnitFactor"] = num;
						row["FactorType"] = text6;
						row["UnitQuantity"] = row["Quantity"];
						num2 = ((!(text6 == "M")) ? float.Parse(Math.Round(num2 * num, 5).ToString()) : float.Parse(Math.Round(num2 / num, 5).ToString()));
						row["Quantity"] = num2;
					}
				}
				insertUpdateConsignOutCommand.Transaction = sqlTransaction;
				flag = (isUpdate ? (flag & Update(consignOutData, "Consign_Out", insertUpdateConsignOutCommand)) : (flag & Insert(consignOutData, "Consign_Out", insertUpdateConsignOutCommand)));
				insertUpdateConsignOutCommand = GetInsertUpdateConsignOutDetailsCommand(isUpdate: false);
				insertUpdateConsignOutCommand.Transaction = sqlTransaction;
				if (isUpdate)
				{
					flag &= DeleteConsignOutDetailsRows(text2, text, isDeletingTransaction: false, sqlTransaction);
					flag &= new InventoryTransaction(base.DBConfig).DeleteInventoryTransaction(47, text2, text, isDeletingTransaction: false, sqlTransaction);
				}
				if (consignOutData.Tables["Consign_Out_Detail"].Rows.Count > 0)
				{
					flag &= Insert(consignOutData, "Consign_Out_Detail", insertUpdateConsignOutCommand);
				}
				InventoryTransactionData inventoryTransactionData = null;
				if (!flag3)
				{
					inventoryTransactionData = new InventoryTransactionData();
					foreach (DataRow row2 in consignOutData.ConsignOutDetailTable.Rows)
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
						dataRow4["UnitPrice"] = 0;
						dataRow4["Factor"] = row2["UnitFactor"];
						dataRow4["FactorType"] = row2["FactorType"];
						dataRow4["Reference"] = dataRow["Reference"];
						dataRow4["RowIndex"] = row2["RowIndex"];
						dataRow4["SysDocType"] = (byte)47;
						dataRow4["TransactionDate"] = dataRow["TransactionDate"];
						dataRow4["TransactionType"] = (byte)9;
						dataRow4["DivisionID"] = dataRow["DivisionID"];
						dataRow4["CompanyID"] = dataRow["CompanyID"];
						dataRow4.EndEdit();
						inventoryTransactionData.InventoryTransactionTable.Rows.Add(dataRow4);
						dataRow4 = inventoryTransactionData.InventoryTransactionTable.NewRow();
						dataRow4.BeginEdit();
						dataRow4["SysDocID"] = row2["SysDocID"];
						dataRow4["VoucherID"] = row2["VoucherID"];
						dataRow4["LocationID"] = value;
						dataRow4["ProductID"] = row2["ProductID"];
						dataRow4["UnitID"] = row2["UnitID"];
						dataRow4["Quantity"] = float.Parse(row2["Quantity"].ToString());
						if (row2["UnitQuantity"] != DBNull.Value)
						{
							dataRow4["UnitQuantity"] = float.Parse(row2["UnitQuantity"].ToString());
						}
						else
						{
							dataRow4["UnitQuantity"] = DBNull.Value;
						}
						dataRow4["UnitPrice"] = 0;
						dataRow4["Factor"] = row2["UnitFactor"];
						dataRow4["FactorType"] = row2["FactorType"];
						dataRow4["Reference"] = dataRow["Reference"];
						dataRow4["RowIndex"] = row2["RowIndex"];
						dataRow4["SysDocType"] = (byte)47;
						dataRow4["TransactionDate"] = dataRow["TransactionDate"];
						dataRow4["TransactionType"] = (byte)9;
						dataRow4["DivisionID"] = dataRow["DivisionID"];
						dataRow4["CompanyID"] = dataRow["CompanyID"];
						dataRow4.EndEdit();
						inventoryTransactionData.InventoryTransactionTable.Rows.Add(dataRow4);
					}
					inventoryTransactionData.Merge(consignOutData.Tables["Product_Lot_Issue_Detail"]);
					flag &= new Products(base.DBConfig).InsertUpdateProductLotIssueDetail(consignOutData, isUpdate: false, sqlTransaction);
					flag &= new InventoryTransaction(base.DBConfig).InsertUpdateInventoryTransaction(inventoryTransactionData, isUpdate, sqlTransaction);
				}
				else
				{
					inventoryTransactionData = new InventoryTransactionData();
					foreach (DataRow row3 in consignOutData.ConsignOutDetailTable.Rows)
					{
						DataRow dataRow6 = inventoryTransactionData.InventoryTransactionTable.NewRow();
						dataRow6.BeginEdit();
						dataRow6["SysDocID"] = row3["SysDocID"];
						dataRow6["VoucherID"] = row3["VoucherID"];
						dataRow6["Description"] = row3["Description"];
						dataRow6["LocationID"] = value;
						dataRow6["ProductID"] = row3["ProductID"];
						dataRow6["UnitID"] = row3["UnitID"];
						dataRow6["Quantity"] = row3["Quantity"];
						dataRow6["UnitQuantity"] = row3["UnitQuantity"];
						dataRow6["Factor"] = row3["UnitFactor"];
						dataRow6["FactorType"] = row3["FactorType"];
						dataRow6["Reference"] = dataRow["Reference"];
						dataRow6["RowIndex"] = row3["RowIndex"];
						dataRow6["SysDocType"] = (byte)47;
						dataRow6["TransactionDate"] = dataRow["TransactionDate"];
						dataRow6["TransactionType"] = (byte)9;
						dataRow6["DivisionID"] = dataRow["DivisionID"];
						dataRow6["CompanyID"] = dataRow["CompanyID"];
						dataRow6.EndEdit();
						inventoryTransactionData.InventoryTransactionTable.Rows.Add(dataRow6);
					}
					string exp = "UPDATE  DN  SET IsInvoiced = 'TRUE', InvoiceSysDocID = '" + text2 + "' , InvoiceVoucherID = '" + text + "'\r\n\t\t\t\t\t\t\t\t    FROM Delivery_Note DN INNER JOIN Consign_Out_Detail COD ON DN.SysDocID = COD.SourceSysDocID AND DN.VoucherID = COD.SourceVoucherID\r\n\t\t\t\t\t\t\t\t    WHERE COD.SysDocID = '" + text2 + "' AND COD.VoucherID = '" + text + "'";
					flag &= (ExecuteNonQuery(exp, sqlTransaction) >= 0);
					flag &= new InventoryTransaction(base.DBConfig).InsertUpdateInventoryTransaction(inventoryTransactionData, isUpdate, sqlTransaction);
				}
				GLData journalData = CreateGLData(consignOutData, flag3, sqlTransaction);
				flag &= new Journal(base.DBConfig).InsertUpdateJournal(journalData, isUpdate, sqlTransaction);
				flag &= UpdateInventoryTransactionRowID(text2, text, sqlTransaction);
				if (!flag)
				{
					throw new Exception("Failed to save the transaction because of an unexpected error. Please try again.");
				}
				flag &= UpdateTableRowInsertUpdateInfo("Consign_Out", "SysDocID", dataRow["SysDocID"].ToString(), "VoucherID", dataRow["VoucherID"].ToString(), sqlTransaction, !isUpdate);
				string entityName = "Consignment Out";
				flag = (isUpdate ? (flag & AddActivityLog(entityName, text, text2, ActivityTypes.Update, sqlTransaction)) : (flag & AddActivityLog(entityName, text, text2, ActivityTypes.Add, sqlTransaction)));
				if (!isUpdate)
				{
					flag &= new SystemDocuments(base.DBConfig).UpdateNextDocumentNumber(dataRow["SysDocID"].ToString(), dataRow["VoucherID"].ToString(), "Consign_Out", "VoucherID", sqlTransaction);
				}
				if (!flag)
				{
					return flag;
				}
				flag &= new Approval(base.DBConfig).CreateTransactionApprovalTasks(SysDocTypes.ConsignOut, text2, text, "Consign_Out", sqlTransaction);
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
				string exp = "UPDATE SID SET ITRowID = (SELECT TransactionID FROM Inventory_Transactions IT WHERE IT.SysDocID = SID.SysDocID AND IT.VoucherID = SID.VoucherID AND IT.RowIndex = SID.RowIndex) \r\n                                    FROM Consign_Out_Detail SID  \r\n                                     where sid.SysDocID = '" + sysDocID + "' and sid.voucherid = '" + voucherID + "'";
				return ExecuteNonQuery(exp, sqlTransaction) > 0;
			}
			catch
			{
				throw;
			}
		}

		public bool ReCostTransaction(string sysDocID, string voucherID)
		{
			bool flag = true;
			try
			{
				ConsignOutData consignOutByID = GetConsignOutByID(sysDocID, voucherID);
				SqlTransaction sqlTransaction = base.DBConfig.StartNewTransaction();
				object obj = null;
				DataRow dataRow = consignOutByID.ConsignOutTable.Rows[0];
				bool flag2 = false;
				if (dataRow["IsExport"] != DBNull.Value)
				{
					flag2 = bool.Parse(dataRow["IsExport"].ToString());
				}
				string text = "";
				if (flag2)
				{
					obj = new CompanyOption(base.DBConfig).GetCompanyOptionValue(57.ToString());
					if (obj != null)
					{
						text = obj.ToString();
					}
				}
				else
				{
					obj = new CompanyOption(base.DBConfig).GetCompanyOptionValue(56.ToString());
					if (obj != null)
					{
						text = obj.ToString();
					}
				}
				SalesFlows salesFlows = SalesFlows.DirectInvoice;
				if (text != "")
				{
					salesFlows = (SalesFlows)int.Parse(text.ToString());
				}
				bool isDNInventory = false;
				if (salesFlows == SalesFlows.SOThenDNThenInvoice)
				{
					isDNInventory = true;
				}
				GLData gLData = CreateGLData(consignOutByID, isDNInventory, sqlTransaction);
				string exp = "SELECT JournalID FROM Journal WHERE SysDocID = '" + sysDocID + "' AND VOucherID = '" + voucherID + "'";
				obj = ExecuteScalar(exp, sqlTransaction);
				if (obj.IsDBNullOrEmpty())
				{
					throw new CompanyException("JournalID not found for invoice '" + voucherID + "'");
				}
				int.Parse(obj.ToString());
				if (!new Journal(base.DBConfig).IsJournalInOpenPeriod(sysDocID, voucherID, sqlTransaction))
				{
					throw new CompanyException("Transaction date is in a period which is locked or closed.");
				}
				exp = " SELECT JournalID,JournalDetailID,SysDocID,VoucherID,JD.AccountID,AC.AccountName,Debit,Credit FROM Journal_Details JD INNER JOIN Account AC ON JD.AccountID = AC.AccountID\r\n                         WHERE AC.subType IN (6, 8) AND SysDocID = '" + sysDocID + "' AND VoucherID = '" + voucherID + "'";
				DataSet dataSet = new DataSet();
				FillDataSet(dataSet, "Invoice", exp, sqlTransaction);
				if (dataSet.Tables[0].Rows.Count != 2)
				{
					throw new CompanyException("Could not match the COGS and Inventory Asset accounts.");
				}
				decimal d = default(decimal);
				foreach (DataRow row in dataSet.Tables[0].Rows)
				{
					int num = int.Parse(row["JournalDetailID"].ToString());
					string text2 = row["AccountID"].ToString();
					DataRow[] array = gLData.JournalDetailsTable.Select("AccountID = '" + text2 + "'");
					if (array.Length == 0)
					{
						throw new CompanyException("Accounts not found. maybe changed.");
					}
					DataRow dataRow2 = array[0];
					if (!dataRow2["Debit"].IsDBNullOrEmpty())
					{
						d += decimal.Parse(dataRow2["Debit"].ToString());
					}
					if (!dataRow2["Credit"].IsDBNullOrEmpty())
					{
						d -= decimal.Parse(dataRow2["Credit"].ToString());
					}
					exp = " UPDATE Journal_Details SET Debit = ";
					exp = ((!dataRow2["Debit"].IsDBNullOrEmpty()) ? (exp + dataRow2["Debit"].ToString()) : (exp + " NULL "));
					exp = ((!dataRow2["Credit"].IsDBNullOrEmpty()) ? (exp + " , Credit = " + dataRow2["Credit"].ToString()) : (exp + " , Credit = NULL "));
					exp = exp + "  WHERE JournalDetailID = " + num + " and AccountID = '" + text2 + "'";
					flag &= (ExecuteNonQuery(exp, sqlTransaction) > 0);
				}
				bool flag3 = new Journal(base.DBConfig).IsJournalInBalance(sysDocID, voucherID, sqlTransaction);
				if (d != 0m || !flag3)
				{
					flag = false;
					throw new CompanyException("Debit and credit not in balance.");
				}
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

		private GLData CreateGLData(ConsignOutData transactionData, bool isDNInventory, SqlTransaction sqlTransaction)
		{
			try
			{
				GLData gLData = new GLData();
				DataRow dataRow = transactionData.ConsignOutTable.Rows[0];
				int currencyDecimalPoints = new CompanyInformations(base.DBConfig).CurrencyDecimalPoints;
				string value = dataRow["CustomerID"].ToString();
				string text = dataRow["SysDocID"].ToString();
				string text2 = dataRow["VoucherID"].ToString();
				string value2 = dataRow["CompanyID"].ToString();
				string value3 = dataRow["DivisionID"].ToString();
				string textCommand = "SELECT SD.LocationID,  LOC2.UnInvoicedInventoryAccountID, LOC.InventoryAccountID ConsignAssetAccountID\r\n                                FROM System_Document SD INNER JOIN Location LOC ON SD.ConsignOutLocationID = LOC.LocationID\r\n\t\t\t\t\t\t\t\tINNER JOIN Location LOC2 ON SD.LocationID = LOC2.LocationID\r\n                                WHERE SysDocID = '" + text + "'";
				DataSet dataSet = new DataSet();
				FillDataSet(dataSet, "Accounts", textCommand, sqlTransaction);
				if (dataSet == null || dataSet.Tables.Count == 0 || dataSet.Tables[0].Rows.Count == 0)
				{
					throw new CompanyException("There is no location assigned to this system document or location record is missing.");
				}
				DataRow dataRow2 = dataSet.Tables["Accounts"].Rows[0];
				string docLocationID = dataRow2["LocationID"].ToString();
				string text3 = dataRow2["UnInvoicedInventoryAccountID"].ToString();
				string text4 = dataRow2["ConsignAssetAccountID"].ToString();
				DataRow dataRow3 = gLData.JournalTable.NewRow();
				SysDocTypes sysDocTypes = SysDocTypes.ConsignOut;
				dataRow3["JournalID"] = 0;
				dataRow3["JournalDate"] = dataRow["TransactionDate"];
				dataRow3["SysDocID"] = dataRow["SysDocID"];
				dataRow3["SysDocType"] = (byte)sysDocTypes;
				dataRow3["VoucherID"] = dataRow["VoucherID"];
				dataRow3["Reference"] = dataRow["Reference"];
				dataRow3["Narration"] = "Consign Out - " + text2;
				dataRow3.EndEdit();
				gLData.JournalTable.Rows.Add(dataRow3);
				if (isDNInventory)
				{
					decimal value4 = default(decimal);
					textCommand = "SELECT SUM(AssetValue) FROM Inventory_Transactions IT WHERE SysDocID = '" + text + "' AND VoucherID = '" + text2 + "'";
					object obj = ExecuteScalar(textCommand, sqlTransaction);
					if (!obj.IsNullOrEmpty())
					{
						value4 += decimal.Parse(obj.ToString());
					}
					DataRow dataRow4 = gLData.JournalDetailsTable.NewRow();
					dataRow4.BeginEdit();
					dataRow4["JournalID"] = 0;
					dataRow4["AccountID"] = text3;
					dataRow4["PayeeID"] = value;
					dataRow4["Debit"] = DBNull.Value;
					dataRow4["Credit"] = Math.Abs(value4);
					dataRow4["IsBaseOnly"] = true;
					dataRow4["Reference"] = dataRow["Reference"];
					dataRow4["JVEntryType"] = (byte)1;
					dataRow4["CompanyID"] = value2;
					dataRow4["DivisionID"] = value3;
					dataRow4.EndEdit();
					gLData.JournalDetailsTable.Rows.Add(dataRow4);
					dataRow4 = gLData.JournalDetailsTable.NewRow();
					dataRow4.BeginEdit();
					dataRow4["JournalID"] = 0;
					dataRow4["AccountID"] = text4;
					dataRow4["PayeeID"] = value;
					dataRow4["Debit"] = Math.Abs(value4);
					dataRow4["Credit"] = DBNull.Value;
					dataRow4["IsBaseOnly"] = true;
					dataRow4["JVEntryType"] = (byte)1;
					dataRow4["Reference"] = dataRow["Reference"];
					dataRow4["CompanyID"] = value2;
					dataRow4["DivisionID"] = value3;
					dataRow4.EndEdit();
					gLData.JournalDetailsTable.Rows.Add(dataRow4);
				}
				else
				{
					decimal num = default(decimal);
					Hashtable hashtable = new Hashtable();
					ArrayList arrayList = new ArrayList();
					decimal d = default(decimal);
					foreach (DataRow row in transactionData.ConsignOutDetailTable.Rows)
					{
						string text5 = row["ProductID"].ToString();
						string warehouseLocationID = row["LocationID"].ToString();
						int rowIndex = int.Parse(row["RowIndex"].ToString());
						dataSet = new Products(base.DBConfig).GetProductTransactionAccounts(text5, docLocationID, warehouseLocationID, text, sqlTransaction);
						if (dataSet == null || dataSet.Tables.Count == 0 || dataSet.Tables[0].Rows.Count == 0)
						{
							throw new CompanyException("Product accounts information not found for product or location.");
						}
						DataRow dataRow6 = dataSet.Tables[0].Rows[0];
						string text6 = dataRow6["InventoryAssetAccountID"].ToString();
						if (isDNInventory)
						{
							text6 = text3;
						}
						ItemTypes itemTypes = ItemTypes.Inventory;
						object obj2 = dataRow6["ItemType"].ToString();
						if (obj2 == null || !(obj2.ToString() != ""))
						{
							throw new CompanyException("Item type is not selected for the product:" + text5);
						}
						itemTypes = (ItemTypes)byte.Parse(obj2.ToString());
						decimal result = default(decimal);
						decimal.TryParse(dataRow6["AverageCost"].ToString(), out result);
						if (row["UnitQuantity"] != DBNull.Value)
						{
							decimal.Parse(row["UnitQuantity"].ToString());
						}
						else
						{
							decimal.Parse(row["Quantity"].ToString());
						}
						decimal num2 = default(decimal);
						num2 = ((itemTypes == ItemTypes.ConsignmentItem) ? default(decimal) : Math.Abs(new InventoryTransaction(base.DBConfig).GetRowAssetValue(text5, text, text2, rowIndex, sqlTransaction)));
						if (itemTypes == ItemTypes.Inventory || itemTypes == ItemTypes.Assembly)
						{
							string text7 = text6;
							if (hashtable.ContainsKey(text7))
							{
								num = decimal.Parse(hashtable[text7].ToString());
								num += Math.Round(num2, currencyDecimalPoints);
								hashtable[text7] = num;
							}
							else
							{
								hashtable.Add(text7, Math.Round(num2, currencyDecimalPoints));
								arrayList.Add(text7);
							}
							d += Math.Round(num2, currencyDecimalPoints);
						}
					}
					if (d != 0m)
					{
						for (int i = 0; i < hashtable.Count; i++)
						{
							DataRow dataRow4 = gLData.JournalDetailsTable.NewRow();
							dataRow4.BeginEdit();
							string text7 = arrayList[i].ToString();
							num = decimal.Parse(hashtable[text7].ToString());
							dataRow4["JournalID"] = 0;
							dataRow4["AccountID"] = text7;
							dataRow4["PayeeID"] = value;
							dataRow4["JVEntryType"] = (byte)1;
							dataRow4["Debit"] = DBNull.Value;
							dataRow4["Credit"] = num;
							dataRow4["IsBaseOnly"] = true;
							dataRow4["Reference"] = dataRow["Reference"];
							dataRow4["CompanyID"] = value2;
							dataRow4["DivisionID"] = value3;
							dataRow4.EndEdit();
							gLData.JournalDetailsTable.Rows.Add(dataRow4);
						}
					}
					if (d != 0m)
					{
						if (text4 == "")
						{
							throw new CompanyException("Inventory Asset account is not set for the consignment location.");
						}
						DataRow dataRow4 = gLData.JournalDetailsTable.NewRow();
						dataRow4.BeginEdit();
						dataRow4["JournalID"] = 0;
						dataRow4["AccountID"] = text4;
						dataRow4["Debit"] = num;
						dataRow4["Credit"] = DBNull.Value;
						dataRow4["JVEntryType"] = (byte)1;
						dataRow4["IsBaseOnly"] = true;
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

		public ConsignOutData GetConsignOutByID(string sysDocID, string voucherID)
		{
			try
			{
				ConsignOutData consignOutData = new ConsignOutData();
				string textCommand = "SELECT * FROM Consign_Out WHERE VoucherID='" + voucherID + "' AND SysDocID='" + sysDocID + "'";
				FillDataSet(consignOutData, "Consign_Out", textCommand);
				if (consignOutData == null || consignOutData.Tables.Count == 0 || consignOutData.Tables["Consign_Out"].Rows.Count == 0)
				{
					return null;
				}
				textCommand = "SELECT TD.*,TD.Quantity-ISNULL(QuantitySettled,0) - ISNULL(QuantityReturned,0) AS QuantityBalance,Product.Description,Product.ItemType,\r\n                        CASE WHEN ItemType = 5 THEN 'True' ELSE IsTrackLot END  AS IsTrackLot,IsTrackSerial\r\n                        FROM Consign_Out_Detail TD INNER JOIN Product ON TD.ProductID=Product.ProductID\r\n                        WHERE VoucherID='" + voucherID + "' AND SysDocID='" + sysDocID + "'";
				FillDataSet(consignOutData, "Consign_Out_Detail", textCommand);
				DataSet transactionIssuesProductLots = new Products(base.DBConfig).GetTransactionIssuesProductLots(sysDocID, voucherID);
				if (consignOutData.Tables.Contains("Product_Lot_Issue_Detail"))
				{
					consignOutData.Tables.Remove("Product_Lot_Issue_Detail");
				}
				consignOutData.Merge(transactionIssuesProductLots, preserveChanges: false);
				return consignOutData;
			}
			catch
			{
				throw;
			}
		}

		public DataSet GetConsignOutList(string sysDocID, DateTime from, DateTime to)
		{
			string text = CommonLib.ToSqlDateTimeString(from);
			string text2 = CommonLib.ToSqlDateTimeString(to);
			DataSet dataSet = new DataSet();
			string str = "SELECT Coo.SysDocID [Doc ID], Coo.VoucherID [Number],Cos.VoucherID [SettleNo], Coo.TransactionDate AS [Date],Cus.CustomerName AS [Vendor]\r\n                             FROM Consign_Out Coo INNER JOIN Customer CUS ON Coo.CustomerID = Cus.CustomerID\r\n\t\t\t\t\t\t\t INNER JOIN ConsignOut_Settlement Cos ON Coo.VoucherID=Cos.ConsignVoucherID\r\n                             WHERE ISNULL(Coo.IsVoid,'False')='False'  AND Coo.TransactionDate BETWEEN '" + text + "' AND '" + text2 + "' ";
			if (!string.IsNullOrEmpty(sysDocID))
			{
				str = str + " AND Coo.SysDocID='" + sysDocID + "'";
			}
			str += " ORDER BY Coo.TransactionDate, Coo.VoucherID ";
			FillDataSet(dataSet, "Consign_Out", str);
			return dataSet;
		}

		internal bool DeleteConsignOutDetailsRows(string sysDocID, string voucherID, bool isDeletingTransaction, SqlTransaction sqlTransaction)
		{
			bool flag = true;
			try
			{
				ConsignOutData consignOutData = new ConsignOutData();
				string textCommand = "SELECT SOD.*,ISVOID FROM Consign_Out_Detail SOD INNER JOIN Consign_Out SO ON SO.SysDocID=SOD.SysDocID AND SO.VOucherID=SOD.VoucherID\r\n                              WHERE SOD.SysDocID = '" + sysDocID + "' AND SOD.VoucherID = '" + voucherID + "'";
				FillDataSet(consignOutData, "Consign_Out_Detail", textCommand, sqlTransaction);
				bool result = false;
				bool.TryParse(consignOutData.ConsignOutDetailTable.Rows[0]["IsVoid"].ToString(), out result);
				if (!result)
				{
					flag &= new InventoryTransaction(base.DBConfig).DeleteInventoryTransaction(47, sysDocID, voucherID, isDeletingTransaction, sqlTransaction);
				}
				textCommand = "DELETE FROM Product_Lot_Issue_Detail WHERE SysDocID = '" + sysDocID + "' AND VoucherID = '" + voucherID + "'";
				flag &= Delete(textCommand, sqlTransaction);
				textCommand = "DELETE FROM Product_Lot_Receiving_Detail WHERE SysDocID = '" + sysDocID + "' AND VoucherID = '" + voucherID + "'";
				flag &= Delete(textCommand, sqlTransaction);
				textCommand = "DELETE FROM Consign_Out_Detail WHERE SysDocID = '" + sysDocID + "' AND VoucherID = '" + voucherID + "'";
				return flag & Delete(textCommand, sqlTransaction);
			}
			catch
			{
				throw;
			}
		}

		public bool VoidConsignOut(string sysDocID, string voucherID, bool isVoid)
		{
			bool result = true;
			try
			{
				result = VoidConsignOut(sysDocID, voucherID, isVoid, null);
				return result;
			}
			catch
			{
				result = false;
				throw;
			}
			finally
			{
				base.DBConfig.EndTransaction(result);
			}
		}

		public bool VoidConsignOut(string sysDocID, string voucherID, bool isVoid, SqlTransaction sqlTransaction)
		{
			bool flag = true;
			try
			{
				bool flag2 = true;
				if (sqlTransaction == null)
				{
					sqlTransaction = base.DBConfig.StartNewTransaction();
				}
				else
				{
					flag2 = false;
				}
				string exp = "UPDATE Consign_Out SET IsVoid = '" + isVoid.ToString() + "' WHERE SysDocID='" + sysDocID + "' AND VoucherID = '" + voucherID + "'";
				flag &= (ExecuteNonQuery(exp, sqlTransaction) > 0);
				ConsignOutData dataSet = new ConsignOutData();
				exp = "SELECT * FROM Consign_Out_Detail\r\n                              WHERE SysDocID = '" + sysDocID + "' AND VoucherID = '" + voucherID + "'";
				FillDataSet(dataSet, "Consign_Out_Detail", exp, sqlTransaction);
				flag &= new InventoryTransaction(base.DBConfig).DeleteInventoryTransaction(47, sysDocID, voucherID, isDeletingTransaction: true, sqlTransaction);
				exp = "UPDATE Delivery_Note SET IsInvoiced='False',InvoiceSysDocID=NULL,InvoiceVoucherID=NULL WHERE InvoiceSysDocID='" + sysDocID + "' AND InvoiceVoucherID='" + voucherID + "'";
				flag &= (ExecuteNonQuery(exp, sqlTransaction) >= 0);
				flag &= new Journal(base.DBConfig).VoidJournal(sysDocID, voucherID, isVoid, sqlTransaction);
				if (!flag || !flag2)
				{
					return flag;
				}
				ActivityTypes activityType = ActivityTypes.Void;
				if (!isVoid)
				{
					activityType = ActivityTypes.Unvoid;
				}
				AddActivityLog("Consign Out", voucherID, sysDocID, activityType, sqlTransaction);
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

		public bool DeleteConsignOut(string sysDocID, string voucherID)
		{
			bool flag = true;
			try
			{
				string text = "";
				SqlTransaction sqlTransaction = base.DBConfig.StartNewTransaction();
				bool result = false;
				bool.TryParse(new Databases(base.DBConfig).GetFieldValue("Consign_Out", "IsVoid", "SysDocID", sysDocID, "VoucherID", voucherID, sqlTransaction).ToString(), out result);
				if (!result)
				{
					flag &= VoidConsignOut(sysDocID, voucherID, isVoid: true, sqlTransaction);
				}
				flag &= DeleteConsignOutDetailsRows(sysDocID, voucherID, isDeletingTransaction: true, sqlTransaction);
				flag &= new Journal(base.DBConfig).DeleteJournal(sysDocID, voucherID, sqlTransaction);
				text = "DELETE FROM Consign_Out WHERE SysDocID='" + sysDocID + "' AND VoucherID = '" + voucherID + "'";
				flag &= (ExecuteNonQuery(text, sqlTransaction) >= 0);
				if (!flag)
				{
					return flag;
				}
				AddActivityLog("Consign Out", voucherID, sysDocID, ActivityTypes.Delete, sqlTransaction);
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
				FillDataSet(dataSet, "Consign_Out", text);
				return dataSet;
			}
			catch
			{
				throw;
			}
		}

		public DataSet GetConsignOutToPrint(string sysDocID, string voucherID)
		{
			return GetConsignOutToPrint(sysDocID, new string[1]
			{
				voucherID
			});
		}

		public DataSet GetConsignOutToPrint(string sysDocID, string[] voucherID)
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
				string cmdText = "SELECT  DISTINCT   SysDocID,VoucherID,SI.CustomerID,CustomerName,CustomerAddress,TransactionDate,\r\n                                SI.SalesPersonID,RequiredDate,CA.AddressPrintFormat AS ShippingAddress,ShippingMethodName,\r\n                                ISNULL(SI.CurrencyID,(SELECT CurrencyID FROM Currency WHERE IsBase='True')) AS CurrencyID,\r\n                                SI.TermID,TermName,IsVoid,Reference,Discount AS Discount,\r\n                                Total AS Total,PONumber,SI.Note\r\n                                FROM  Consign_Out SI INNER JOIN Customer ON SI.CustomerID=Customer.CustomerID\r\n                                LEFT OUTER JOIN Payment_Term PT ON SI.TermID=PT.PaymentTermID\r\n                                LEFT OUTER JOIN Customer_Address CA ON CA.AddressID=ShippingAddressID AND CA.CustomerID=SI.CustomerID\r\n                                LEFT OUTER JOIN Shipping_Method SM ON SM.ShippingMethodID=SI.ShippingMethodID\r\n                                WHERE SysDocID = '" + sysDocID + "' AND VoucherID IN (" + text + ")";
				SqlCommand sqlCommand = new SqlCommand(cmdText);
				FillDataSet(dataSet, "Consign_Out", sqlCommand);
				if (dataSet == null || dataSet.Tables.Count == 0 || dataSet.Tables["Consign_Out"].Rows.Count == 0)
				{
					return null;
				}
				cmdText = "SELECT     SysDocID,VoucherID,ProductID,Description,LocationID,ISNULL(UnitQuantity,Quantity) AS Quantity,\r\n                        UnitPrice AS UnitPrice,\r\n                        ISNULL(UnitQuantity,Quantity)*UnitPrice AS Total,UnitID,ISNULL(MarketPrice,0) as MarketPrice\r\n                        FROM   Consign_Out_Detail\r\n                        WHERE SysDocID='" + sysDocID + "' AND VoucherID IN (" + text + ")";
				FillDataSet(dataSet, "Consign_Out_Detail", cmdText);
				dataSet.Relations.Add("CustomerConsignOut", new DataColumn[2]
				{
					dataSet.Tables["Consign_Out"].Columns["SysDocID"],
					dataSet.Tables["Consign_Out"].Columns["VoucherID"]
				}, new DataColumn[2]
				{
					dataSet.Tables["Consign_Out_Detail"].Columns["SysDocID"],
					dataSet.Tables["Consign_Out_Detail"].Columns["VoucherID"]
				}, createConstraints: false);
				dataSet.Tables["Consign_Out"].Columns.Add("TotalInWords", typeof(string));
				foreach (DataRow row in dataSet.Tables["Consign_Out"].Rows)
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
			string text3 = "SELECT    ISNULL(IsVoid,'False') AS V, INV.SysDocID [Doc ID],INV.VoucherID [Doc Number],INV.CustomerID [Customer Code],CustomerName [Customer Name],TransactionDate [Delivery Date],\r\n                            INV.SalespersonID [Salesperson],SUM(DND.Quantity) AS Quantity\r\n                            FROM  Consign_Out INV\r\n                            INNER JOIN Consign_Out_Detail DND ON DND.SysDocID=INV.SysDocID AND DND.VoucherID=INV.VoucherID\r\n                            Inner JOIN Customer ON CUSTOMER.CustomerID=INV.CustomerID ";
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
			FillDataSet(dataSet, "Consign_Out", sqlCommand);
			return dataSet;
		}

		public DataSet GetOpenConsignments(string customerID)
		{
			try
			{
				DataSet dataSet = new DataSet();
				string str = "SELECT CO.SysDocID [Doc ID], CO.VoucherID [Number],CO.CustomerID [Customer Code], CUS.CustomerName [Customer Name],TransactionDate [Date]\r\n                            FROM Consign_Out CO INNER JOIN Consign_Out_Detail COD\r\n                            ON CO.SysDocID=COD.SysDocID AND CO.VoucherID=COD.VoucherID\r\n                            INNER JOIN Customer CUS ON CO.CustomerID=CUS.CustomerID  WHERE ISNULL(IsClosed,'False')='False' AND ISNULL(IsVoid,'False')='False' ";
				if (customerID != "")
				{
					str = str + " AND CO.CustomerID='" + customerID + "'";
				}
				str += " GROUP BY CO.SysDocID, CO.VoucherID,CO.CustomerID,TransactionDate, CUS.CustomerName\r\n                                HAVING\r\n                                SUM(ISNULL(COD.Quantity,0)-ISNULL(COD.QuantitySettled,0))>0 ";
				SqlCommand sqlCommand = new SqlCommand(str);
				FillDataSet(dataSet, "Consign_Out", sqlCommand);
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
				string exp = "SELECT SUM((CASE WHEN UnitQuantity IS NULL \r\n                                THEN Quantity ELSE UnitQuantity END))- SUM(ISNULL(QuantitySettled,0) ) - SUM(ISNULL(QuantityReturned,0) )\r\n\t                             FROM Consign_Out_Detail SOD WHERE SysDocID='" + sysDocID + "' AND VoucherID='" + voucherID + "'";
				object obj = ExecuteScalar(exp, sqlTransaction);
				if (obj == null || obj.ToString() == "")
				{
					return true;
				}
				exp = ((!(decimal.Parse(obj.ToString()) <= 0m)) ? ("UPDATE Consign_Out SET IsClosed = 'False' WHERE SysDocID='" + sysDocID + "' AND VoucherID='" + voucherID + "'") : ("UPDATE Consign_Out SET IsClosed = 'True' WHERE SysDocID='" + sysDocID + "' AND VoucherID='" + voucherID + "'"));
				return ExecuteNonQuery(exp, sqlTransaction) > 0;
			}
			catch
			{
				throw;
			}
		}

		internal bool UpdateConsignmentStatus(string sysDocID, string voucherID, SqlTransaction sqlTransaction)
		{
			try
			{
				string exp = "Update CO SET Status = CASE  WHEN BalanceQty <= 0 THEN  2 WHEN BalanceQty>0 THEN  1 END\r\n                                FROM Consign_Out CO INNER JOIN\r\n                                (select SysDocID,VoucherID, SUM(ISNULL(Quantity,0)-ISNULL(QuantityReturned,0)- ISNULL(QuantitySettled,0)) AS BalanceQty\r\n                                FROM Consign_Out_Detail COD WHERE COD.SysDocID = '" + sysDocID + "' AND COD.VoucherID = '" + voucherID + "'\r\n                                GROUP BY SysDocID,VOucherID) AS COD ON CO.SysDocID = COD.SysDocID AND CO.VoucherID = COD.VoucherID\r\n                                WHERE CO.SysDocID = '" + sysDocID + "' AND CO.VoucherID = '" + voucherID + "'";
				return ExecuteNonQuery(exp, sqlTransaction) > 0;
			}
			catch
			{
				throw;
			}
		}

		public bool AllowModify(string sysDocID, string voucherNumber, SqlTransaction sqlTransaction)
		{
			string exp = "Select Count(*) FROM Consign_Out_Detail POD\r\n                                WHERE SysdocID='" + sysDocID + "' AND VoucherID='" + voucherNumber + "' Having SUM(ISNULL(QuantitySettled,0) + ISNULL(QuantityReturned,0)) >0";
			object obj = ExecuteScalar(exp, sqlTransaction);
			if (obj != null && obj.ToString() != "" && int.Parse(obj.ToString()) > 0)
			{
				return false;
			}
			return true;
		}

		public DataSet GetConsignmentOutIssuedReport(DateTime settleto, DateTime from, DateTime to, string fromCustomer, string toCustomer, int intstatus, string customerIDs, string fromItem, string toItem, string fromClass, string toClass, string fromCategory, string toCategory, string fromBrand, string toBrand, string fromManufacturer, string toManufacturer, string fromStyle, string toStyle, string fromOrigin, string toOrigin)
		{
			try
			{
				string text = CommonLib.ToSqlDateTimeString(from);
				string text2 = CommonLib.ToSqlDateTimeString(to);
				string text3 = "SELECT T.SysDocID,T.VoucherID,T.[Set ID],T.[Set No.],T.CustomerID,T.CustomerName,T.[Set Date],T.[Con Date],CASE WHEN T.Status=2 THEN 'Settled' ELSE 'UnSettlted' END AS Status,\r\n                                SUM(COD.Quantity) AS [Con Qty],ISNULL(T.[Set Qty],0)AS [Set Qty],ISNULL(T.Amount,0) AS Amount,ISNULL(T.Cost,0) AS Cost,\r\n                                (ISNULL(T.Amount,0)-ISNULL(T.Cost,0)) AS GrossProfit,\r\n                                ISNULL(T.[Total Expense],0) AS Expense,\r\n                                (ISNULL(T.Amount,0)-ISNULL(T.Cost,0)-ISNULL(T.[Total Expense],0)) AS NetProfit,T.Category\r\n                                FROM Consign_Out_Detail COD INNER JOIN \r\n                                (SELECT DISTINCT COD.SysDocID,COD.VoucherID,CO.TransactionDate [Con Date],C.CustomerID,C.CustomerName,CO.Status,\r\n                                CSD.SysDocID AS [Set ID],CSD.VoucherID AS [Set No.],CS.TransactionDate AS [Set Date],SUM(CSD.Quantity) AS [Set Qty],\r\n                                (SELECT SUM(CE.Amount) FROM ConsignOut_Expense CE WHERE CE.SysDocID=CS.SysDocID AND CE.VoucherID=CS.VoucherID) AS [Total Expense],\r\n                                --(SELECT -1*SUM(IT.Assetvalue) FROM Inventory_Transactions IT WHERE IT.SysDocID=CSD.SysDocID AND IT.VoucherID=CSD.VoucherID) AS [Cost],\r\n                                --(SELECT -1*SUM(IT.UnitPrice*IT.Quantity) FROM Inventory_Transactions IT WHERE IT.SysDocID=CSD.SysDocID AND IT.VoucherID=CSD.VoucherID) AS [Amount],\r\n                               -1*SUM(IT1.UnitPrice*IT1.Quantity) AS [Amount],-1*SUM(IT1.Assetvalue) [Cost],\r\n                                (SELECT SUBSTRING(\r\n                                (SELECT  ',' + CategoryName FROM  Product_Category WHERE CategoryID IN (SELECT DISTINCT CategoryID FROM Product P LEFT JOIN ConsignOut_Settlement_Detail COS1 ON P.ProductID=COS1.ProductID WHERE COS1.SysDocID=CSD.SysDocID AND COS1.VoucherID=CSD.VoucherID )   FOR XML PATH('')),2,20000)) AS  Category\r\n                                FROM  Consign_Out CO LEFT OUTER JOIN  Consign_Out_Detail COD ON CO.SysDocID = COD.SysDocID AND CO.VoucherID = COD.VoucherID\r\n                                LEFT OUTER JOIN ConsignOut_Settlement_Detail CSD ON CSD.ConsignSysDocID = COD.SysDocID AND CSD.ConsignVoucherID = COD.VoucherID AND CSD.ConsignRowIndex = COD.RowIndex\r\n                                LEFT OUTER JOIN Inventory_Transactions IT ON IT.SysDocID = COD.SysDocID AND IT.VoucherID = COD.VoucherID AND IT.RowIndex = COD.RowIndex\r\n\r\n                                LEFT OUTER JOIN ConsignOut_Settlement CS ON CS.SysDocID=CSD.SysDocID AND CS.VoucherID=CSD.VoucherID\r\n                                LEFT OUTER JOIN Customer C ON C.CustomerID=CO.CustomerID \r\n                                INNER JOIN Inventory_Transactions IT1 ON IT1.SysDocID = CSD.SysDocID AND IT1.VoucherID = CSD.VoucherID AND IT1.RowIndex = CSD.RowIndex";
				if (fromItem != "")
				{
					text3 = text3 + " AND IT1.ProductID BETWEEN '" + fromItem + "' AND '" + toItem + "' ";
				}
				if (fromClass != "")
				{
					text3 = text3 + " AND IT1.ProductID IN (SELECT ProductID FROM Product WHERE ClassID BETWEEN '" + fromClass + "' AND '" + toClass + "') ";
				}
				if (fromCategory != "")
				{
					text3 = text3 + " AND IT1.ProductID IN (SELECT ProductID FROM Product WHERE CategoryID BETWEEN '" + fromCategory + "' AND '" + toCategory + "') ";
				}
				if (fromManufacturer != "")
				{
					text3 = text3 + " AND IT1.ProductID IN (SELECT ProductID FROM Product WHERE ManufacturerID BETWEEN '" + fromManufacturer + "' AND '" + toManufacturer + "') ";
				}
				if (fromStyle != "")
				{
					text3 = text3 + " AND IT1.ProductID IN (SELECT ProductID FROM Product WHERE StyleID BETWEEN '" + fromStyle + "' AND '" + toStyle + "') ";
				}
				if (fromOrigin != "")
				{
					text3 = text3 + " AND IT1.ProductID IN (SELECT ProductID FROM Product WHERE Origin BETWEEN '" + fromOrigin + "' AND '" + toOrigin + "') ";
				}
				if (fromBrand != "")
				{
					text3 = text3 + " AND IT1.ProductID IN (SELECT ProductID FROM Product WHERE BrandID BETWEEN '" + fromBrand + "' AND '" + toBrand + "') ";
				}
				text3 = text3 + " WHERE CS.TransactionDate BETWEEN '" + text + "' AND '" + text2 + "' AND CS.IsVoid IS NULL ";
				if (customerIDs != "")
				{
					text3 = text3 + " AND CO.CustomerID IN(" + customerIDs + ")";
				}
				if (fromCustomer != "" || toCustomer != "")
				{
					text3 = text3 + " AND CO.CustomerID BETWEEN '" + fromCustomer + "' AND '" + toCustomer + "' ";
				}
				if (fromItem != "")
				{
					text3 = text3 + " AND CSD.ProductID BETWEEN '" + fromItem + "' AND '" + toItem + "' ";
				}
				if (fromClass != "")
				{
					text3 = text3 + " AND CSD.ProductID IN (SELECT ProductID FROM Product WHERE ClassID BETWEEN '" + fromClass + "' AND '" + toClass + "') ";
				}
				if (fromCategory != "")
				{
					text3 = text3 + " AND CSD.ProductID IN (SELECT ProductID FROM Product WHERE CategoryID BETWEEN '" + fromCategory + "' AND '" + toCategory + "') ";
				}
				if (fromManufacturer != "")
				{
					text3 = text3 + " AND CSD.ProductID IN (SELECT ProductID FROM Product WHERE ManufacturerID BETWEEN '" + fromManufacturer + "' AND '" + toManufacturer + "') ";
				}
				if (fromStyle != "")
				{
					text3 = text3 + " AND CSD.ProductID IN (SELECT ProductID FROM Product WHERE StyleID BETWEEN '" + fromStyle + "' AND '" + toStyle + "') ";
				}
				if (fromOrigin != "")
				{
					text3 = text3 + " AND CSD.ProductID IN (SELECT ProductID FROM Product WHERE Origin BETWEEN '" + fromOrigin + "' AND '" + toOrigin + "') ";
				}
				if (fromBrand != "")
				{
					text3 = text3 + " AND CSD.ProductID IN (SELECT ProductID FROM Product WHERE BrandID BETWEEN '" + fromBrand + "' AND '" + toBrand + "') ";
				}
				text3 += "GROUP BY COD.SysDocID,COD.VoucherID,CO.TransactionDate,C.CustomerID,C.CustomerName,CO.Status,CSD.SysDocID,CSD.VoucherID,CS.TransactionDate,CS.SysDocID,CS.VoucherID) T ON COD.SysDocID=T.SysDocID AND COD.VoucherID=T.VoucherID   WHERE T.CustomerID IS NOT NULL AND T.Status  IN (1,2)  ";
				if (fromItem != "")
				{
					text3 = text3 + " AND COD.ProductID BETWEEN '" + fromItem + "' AND '" + toItem + "' ";
				}
				if (fromClass != "")
				{
					text3 = text3 + " AND COD.ProductID IN (SELECT ProductID FROM Product WHERE ClassID BETWEEN '" + fromClass + "' AND '" + toClass + "') ";
				}
				if (fromCategory != "")
				{
					text3 = text3 + " AND COD.ProductID IN (SELECT ProductID FROM Product WHERE CategoryID BETWEEN '" + fromCategory + "' AND '" + toCategory + "') ";
				}
				if (fromManufacturer != "")
				{
					text3 = text3 + " AND COD.ProductID IN (SELECT ProductID FROM Product WHERE ManufacturerID BETWEEN '" + fromManufacturer + "' AND '" + toManufacturer + "') ";
				}
				if (fromStyle != "")
				{
					text3 = text3 + " AND COD.ProductID IN (SELECT ProductID FROM Product WHERE StyleID BETWEEN '" + fromStyle + "' AND '" + toStyle + "') ";
				}
				if (fromOrigin != "")
				{
					text3 = text3 + " AND COD.ProductID IN (SELECT ProductID FROM Product WHERE Origin BETWEEN '" + fromOrigin + "' AND '" + toOrigin + "') ";
				}
				if (fromBrand != "")
				{
					text3 = text3 + " AND COD.ProductID IN (SELECT ProductID FROM Product WHERE BrandID BETWEEN '" + fromBrand + "' AND '" + toBrand + "') ";
				}
				text3 += " GROUP BY T.SysDocID,T.VoucherID,T.CustomerID,T.CustomerName,T.Category,T.Status,T.[Set Qty],T.Amount,T.Cost,T.[Total Expense],T.[Set ID],T.[Set No.],T.[Set Date],T.[Con Date] ORDER BY T.VoucherID,T.[Set No.]";
				DataSet dataSet = new DataSet();
				FillDataSet(dataSet, "Consign_Out", text3);
				return dataSet;
			}
			catch
			{
				throw;
			}
		}

		public DataSet GetConsignmentOutSettlementReport(DateTime to, string fromCustomer, string toCustomer, string customerIDs)
		{
			try
			{
				string text = "SELECT CO.SysDocID [Doc ID], CO.VoucherID [Number],CO.CustomerID [Customer Code], CUS.CustomerName [Customer Name],CO.TransactionDate [Date],DATEDIFF(DAY,CO.TransactionDate,GETDATE()) AGE,\r\n                            (SELECT SUBSTRING(\r\n                             (SELECT  ',' + CategoryName FROM  Product_Category WHERE CategoryID IN (SELECT DISTINCT CategoryID FROM Product P LEFT JOIN Inventory_Transactions IT1 ON P.ProductID=IT1.ProductID WHERE IT1.SysDocID=CO.SysDocID AND IT1.VoucherID=CO.VoucherID )   FOR XML PATH('')),2,20000)) AS  Category,\r\n                            (SELECT V.VehicleName FROM Delivery_Note DN LEFT JOIN Vehicle V ON DN.VehicleID=V.VehicleID WHERE DN.SysDocID=CO.SourceSysDocID AND DN.VoucherID=CO.SourceVoucherID) AS Truck,\r\n                            COD.ProductID,COD.Description,COD.UnitID,COD.Quantity,COD.UnitPrice,(IT.AssetValue/IT.Quantity) AS COST\r\n                            FROM Consign_Out CO INNER JOIN Consign_Out_Detail COD\r\n                            ON CO.SysDocID=COD.SysDocID AND CO.VoucherID=COD.VoucherID\r\n                            LEFT JOIN Inventory_Transactions IT ON COD.SourceSysDocID=IT.SysDocID AND COD.SourceVoucherID=It.VoucherID AND COD.SourceRowIndex=IT.RowIndex\r\n                            INNER JOIN Customer CUS ON CO.CustomerID=CUS.CustomerID  WHERE ISNULL(IsClosed,'False')='False'  \r\n                            GROUP BY CO.SysDocID, CO.VoucherID,CO.CustomerID,CO.TransactionDate, IT.AssetValue,IT.Quantity,CUS.CustomerName,COD.ProductID,COD.Description,COD.UnitID,COD.Quantity,COD.UnitPrice,CO.SourceSysDocID,CO.SourceVoucherID\r\n                            HAVING\r\n                            SUM(ISNULL(COD.Quantity,0)-ISNULL(COD.QuantitySettled,0))>0 ";
				if (customerIDs != "")
				{
					text = text + " AND CO.CustomerID IN(" + customerIDs + ")";
				}
				if (fromCustomer != "" || toCustomer != "")
				{
					text = text + " AND CO.CustomerID BETWEEN '" + fromCustomer + "' AND '" + toCustomer + "' ";
				}
				text = text + "AND CO.TransactionDate <= '" + to + "'";
				text += " ORDER BY CO.TransactionDate ";
				DataSet dataSet = new DataSet();
				FillDataSet(dataSet, "Consign_Out", text);
				return dataSet;
			}
			catch
			{
				throw;
			}
		}

		public DataSet GetPendingConsignmentsReport(string fromCustomer, string toCustomer, string customerIDs)
		{
			try
			{
				if (fromCustomer != "" || toCustomer != "")
				{
					_ = " AND CustomerID BETWEEN '" + fromCustomer + "' AND '" + toCustomer + "' ";
				}
				string text = "SELECT DISTINCT CI.SysDocID,CI.VoucherID,CI.CustomerID,CustomerName,TransactionDate\r\n                                    FROM Consign_Out CI INNER JOIN Consign_Out_Detail CID\r\n                                    ON CI.SysDocID = CID.SysDocID AND CI.VoucherID = CID.VoucherID \r\n                                    INNER JOIN Customer V ON V.CustomerID = CI.CustomerID\r\n                                    WHERE ISNULL(IsVoid,'False') = 'False' \r\n\t\t\t\t\t\t\t\t\tGROUP BY CI.SysDocID,CI.VoucherID,CI.CustomerID,CustomerName,TransactionDate\r\n\t\t\t\t\t\t\t\t\tHAVING SUM(Quantity - ISNULL(QuantityReturned,0) - ISNULL(CID.QuantitySettled,0)) > 0 ";
				if (customerIDs != "")
				{
					text = text + " AND CI.CustomerID IN(" + customerIDs + ")";
				}
				if (fromCustomer != "")
				{
					text = text + " AND CI.CustomerID BETWEEN '" + fromCustomer + "' AND '" + toCustomer + "' ";
				}
				text += " ORDER BY TransactionDate ";
				DataSet dataSet = new DataSet();
				FillDataSet(dataSet, "Consign_Out", text);
				DataSet dataSet2 = new DataSet();
				text = "SELECT CiD.SysDocID,CID.VoucherID,CID.Description,ProductID,ISNULL(Quantity,0) AS Quantity,ISNULL(QuantitySettled,0) AS QuantitySettled,ISNULL(QuantityReturned,0) AS QuantityReturned,\r\n                            Quantity-ISNULL(QuantitySettled,0)-ISNULL(QuantityReturned,0) AS BalanceQty\r\n                            FROM Consign_Out CI INNER JOIN Consign_Out_Detail CID\r\n                            ON CI.SysDocID = CID.SysDocID AND CI.VoucherID = CID.VoucherID \r\n                            WHERE ISNULL(IsClosed,'False') = 'False' ";
				if (customerIDs != "")
				{
					text = text + " AND CI.CustomerID IN(" + customerIDs + ")";
				}
				if (fromCustomer != "")
				{
					text = text + " AND CI.CustomerID BETWEEN '" + fromCustomer + "' AND '" + toCustomer + "' ";
				}
				text += " ORDER BY TransactionDate";
				FillDataSet(dataSet2, "Consign_Out_Detail", text);
				dataSet.Merge(dataSet2);
				dataSet.Relations.Add("Consign_REL", new DataColumn[2]
				{
					dataSet.Tables["Consign_Out"].Columns["SysDocID"],
					dataSet.Tables["Consign_Out"].Columns["VoucherID"]
				}, new DataColumn[2]
				{
					dataSet.Tables["Consign_Out_Detail"].Columns["SysDocID"],
					dataSet.Tables["Consign_Out_Detail"].Columns["VoucherID"]
				}, createConstraints: false);
				return dataSet;
			}
			catch
			{
				throw;
			}
		}

		public DataSet GetConsignOutSummaryReport_Old(string sysDocID, string voucherID, DateTime from, DateTime to, string fromCustomer, string toCustomer, string fromCustomerClass, string toCustomerClass, string fromGroup, string toGroup)
		{
			try
			{
				string text = CommonLib.ToSqlDateTimeString(from);
				string text2 = CommonLib.ToSqlDateTimeString(to);
				DataSet dataSet = new DataSet();
				string text3 = " SELECT DISTINCT COD.SysDocID+'-'+COD.VoucherID AS [Con No],C.CustomerID+'-'+C.CustomerName AS [Customer],\r\n                                DATEdIFF(day,CO.TransactionDate,CS.TransactionDate) AS Days,\r\n                                CSD.SysDocID+'-'+CSD.VoucherID AS [Set Id],CO.TransactionDate [Con Date],CS.TransactionDate AS [Set Date],\r\n                                (SELECT SUM(CE.Amount) FROM ConsignOut_Expense CE WHERE CE.SysDocID=CS.SysDocID AND CE.VoucherID=CS.VoucherID) AS [Total Expense],\r\n                                (SELECT -1*SUM(IT.Assetvalue) FROM Inventory_Transactions IT WHERE IT.SysDocID=CSD.SysDocID AND IT.VoucherID=CSD.VoucherID) AS [Cost],\r\n                                (SELECT -1*SUM(IT.UnitPrice*IT.Quantity) FROM Inventory_Transactions IT WHERE IT.SysDocID=CSD.SysDocID AND IT.VoucherID=CSD.VoucherID) AS [Amount]\r\n                                FROM  Consign_Out CO LEFT OUTER JOIN  Consign_Out_Detail COD ON CO.SysDocID = COD.SysDocID AND CO.VoucherID = COD.VoucherID\r\n                                LEFT OUTER JOIN ConsignOut_Settlement_Detail CSD ON CSD.ConsignSysDocID = COD.SysDocID AND CSD.ConsignVoucherID = COD.VoucherID AND CSD.ConsignRowIndex = COD.RowIndex\r\n                                LEFT OUTER JOIN Inventory_Transactions IT ON IT.SysDocID = COD.SysDocID AND IT.VoucherID = COD.VoucherID AND IT.RowIndex = COD.RowIndex\r\n                                LEFT JOIN ConsignOut_Settlement CS ON CS.SysDocID=CSD.SysDocID AND CS.VoucherID=CSD.VoucherID\r\n                                LEFT JOIN Customer C ON C.CustomerID=CO.CustomerID\r\n                                WHERE COD.SysDocID= '" + sysDocID + "' AND COD.VoucherID= '" + voucherID + "'";
				if (fromCustomer != "")
				{
					text3 = text3 + " AND C.CustomerID >= '" + fromCustomer + "' ";
				}
				if (toCustomer != "")
				{
					text3 = text3 + " AND C.CustomerID <= '" + toCustomer + "' ";
				}
				if (fromCustomerClass != "")
				{
					text3 = text3 + " AND C.CustomerClassID IN (SELECT ClassID FROM Customer_Class WHERE ClassID >= '" + fromCustomerClass + "')";
				}
				if (toCustomerClass != "")
				{
					text3 = text3 + " AND C.CustomerClassID IN (SELECT ClassID FROM Customer_Class WHERE ClassID <= '" + toCustomerClass + "')";
				}
				if (fromGroup != "")
				{
					text3 = text3 + " AND C.CustomerGroupID IN (SELECT GroupID FROM Customer_Group WHERE GroupID >= '" + fromGroup + "')";
				}
				if (toGroup != "")
				{
					text3 = text3 + " AND C.CustomerGroupID IN (SELECT GroupID FROM Customer_Group WHERE GroupID <= '" + toGroup + "') ";
				}
				text3 = text3 + "AND CO. TransactionDate BETWEEN '" + text + "' AND '" + text2 + "' ";
				FillDataSet(dataSet, "Consign_Out", text3);
				text3 = "SELECT '" + sysDocID + "' AS DocID, '" + voucherID + " AS ReceiptNumber' , TEMP.ProductID,TEMP.Description, TEMP.UnitID,\r\n                         TEMP.Quantity,TEMP.[QTY Settled],TEMP.[QTY Returned],Temp.Cost,TEMP.[Total Cost],TEMP.Price,Temp.Amount,\r\n                        SUM(Temp.Expense) AS [Item Expense]\r\n                        FROM \r\n                        (SELECT DISTINCT COD.ProductID,COD.Description, COD.UnitID,\r\n                        ISNULL(COD.UnitQuantity,COD.Quantity) AS Quantity, (COD.QuantitySettled) [QTY Settled],(COD.QuantityReturned) [QTY Returned],\r\n                        (SELECT (SUM(IT.Assetvalue)/SUM(IT.Quantity)) FROM Inventory_Transactions IT WHERE IT.SysDocID=CSD.SysDocID AND IT.VoucherID=CSD.VoucherID AND IT.ProductID=COD.ProductID) AS [Cost],\r\n                        (SELECT -1*SUM(IT.Assetvalue) FROM Inventory_Transactions IT WHERE IT.SysDocID=CSD.SysDocID AND IT.VoucherID=CSD.VoucherID AND IT.RowIndex=CSD.RowIndex AND ABS(IT.Quantity)=CSD.Quantity) AS [Total Cost],\r\n                        (SELECT (SUM(IT.UnitPrice*IT.Quantity)/SUM(IT.Quantity)) FROM Inventory_Transactions IT WHERE IT.SysDocID=CSD.SysDocID AND IT.VoucherID=CSD.VoucherID AND IT.ProductID=COD.ProductID) AS [Price],\r\n                        (SELECT -1*(SUM(IT.UnitPrice*IT.Quantity)) FROM Inventory_Transactions IT WHERE IT.SysDocID=CSD.SysDocID AND IT.VoucherID=CSD.VoucherID AND IT.ProductID=COD.ProductID AND IT.RowIndex = CSD.RowIndex) AS [Amount],\r\n                        (SELECT (CSD.Amount*(SELECT SUM(CE.Amount) FROM ConsignOut_Expense CE WHERE CE.SysDocID=CS.SysDocID AND CE.VoucherID=CS.VoucherID)/(SELECT -1*(SUM(IT.UnitPrice*IT.Quantity)) FROM Inventory_Transactions IT WHERE IT.SysDocID=CSD.SysDocID AND IT.VoucherID=CSD.VoucherID))) AS Expense\r\n                        FROM  Consign_Out CO LEFT OUTER JOIN  Consign_Out_Detail COD ON CO.SysDocID = COD.SysDocID AND CO.VoucherID = COD.VoucherID\r\n                        LEFT OUTER JOIN ConsignOut_Settlement_Detail CSD ON CSD.ConsignSysDocID = COD.SysDocID AND CSD.ConsignVoucherID = COD.VoucherID AND CSD.ConsignRowIndex = COD.RowIndex\r\n                        LEFT OUTER JOIN Inventory_Transactions IT ON IT.SysDocID = COD.SysDocID AND IT.VoucherID = COD.VoucherID AND IT.RowIndex = COD.RowIndex\r\n                        LEFT JOIN ConsignOut_Settlement CS ON CS.SysDocID=CSD.SysDocID AND CS.VoucherID=CSD.VoucherID\r\n                        LEFT JOIN Customer C ON C.CustomerID=CO.CustomerID\r\n                        WHERE COD.SysDocID= '" + sysDocID + "' AND COD.VoucherID= '" + voucherID + "' ";
				text3 = text3 + "AND CO. TransactionDate BETWEEN '" + text + "' AND '" + text2 + "' ) AS TEMP WHERE 1=1";
				text3 += " GROUP BY TEMP.ProductID,TEMP.Description, TEMP.UnitID,TEMP.Quantity,TEMP.[QTY Settled],TEMP.[QTY Returned],Temp.Cost,TEMP.[Total Cost],TEMP.Price,Temp.Amount";
				FillDataSet(dataSet, "Consign_Out_Detail", text3);
				return dataSet;
			}
			catch
			{
				throw;
			}
		}

		public DataSet GetConsignOutSummaryReport(string sysDocID, string voucherID, DateTime from, DateTime to, string fromCustomer, string toCustomer, string fromCustomerClass, string toCustomerClass, string fromGroup, string toGroup)
		{
			try
			{
				string text = CommonLib.ToSqlDateTimeString(from);
				string text2 = CommonLib.ToSqlDateTimeString(to);
				DataSet dataSet = new DataSet();
				string text3 = " SELECT DISTINCT COD.SysDocID+'-'+COD.VoucherID AS [Con No],C.CustomerID+'-'+C.CustomerName AS [Customer],\r\n                                DATEDIFF(day,CO.TransactionDate,CS.TransactionDate) AS Days,\r\n                                CSD.SysDocID+'-'+CSD.VoucherID AS [Set Id],CSD.SysDocID,CSD.VoucherID,CO.TransactionDate [Con Date],CS.TransactionDate AS [Set Date],\r\n                                (SELECT SUM(CE.Amount) FROM ConsignOut_Expense CE WHERE CE.SysDocID=CS.SysDocID AND CE.VoucherID=CS.VoucherID) AS [Total Expense],\r\n                                (SELECT -1*SUM(IT.Assetvalue) FROM Inventory_Transactions IT WHERE IT.SysDocID=CSD.SysDocID AND IT.VoucherID=CSD.VoucherID) AS [Cost],\r\n                                (SELECT -1*SUM(IT.UnitPrice*IT.Quantity) FROM Inventory_Transactions IT WHERE IT.SysDocID=CSD.SysDocID AND IT.VoucherID=CSD.VoucherID) AS [Amount],\r\n((SELECT -1*SUM(IT.UnitPrice*IT.Quantity) FROM Inventory_Transactions IT WHERE IT.SysDocID=CSD.SysDocID AND IT.VoucherID=CSD.VoucherID)-(SELECT ISNULL(SUM(CE.Amount),0) FROM ConsignOut_Expense CE WHERE CE.SysDocID=CS.SysDocID AND CE.VoucherID=CS.VoucherID)) AS Receivable\r\n                                FROM  Consign_Out CO LEFT OUTER JOIN  Consign_Out_Detail COD ON CO.SysDocID = COD.SysDocID AND CO.VoucherID = COD.VoucherID\r\n                                LEFT OUTER JOIN ConsignOut_Settlement_Detail CSD ON CSD.ConsignSysDocID = COD.SysDocID AND CSD.ConsignVoucherID = COD.VoucherID AND CSD.ConsignRowIndex = COD.RowIndex\r\n                                LEFT OUTER JOIN Inventory_Transactions IT ON IT.SysDocID = COD.SysDocID AND IT.VoucherID = COD.VoucherID AND IT.RowIndex = COD.RowIndex\r\n                                LEFT JOIN ConsignOut_Settlement CS ON CS.SysDocID=CSD.SysDocID AND CS.VoucherID=CSD.VoucherID\r\n                                LEFT JOIN Customer C ON C.CustomerID=CO.CustomerID\r\n                                WHERE COD.SysDocID= '" + sysDocID + "' AND COD.VoucherID= '" + voucherID + "'";
				if (fromCustomer != "")
				{
					text3 = text3 + " AND C.CustomerID >= '" + fromCustomer + "' ";
				}
				if (toCustomer != "")
				{
					text3 = text3 + " AND C.CustomerID <= '" + toCustomer + "' ";
				}
				if (fromCustomerClass != "")
				{
					text3 = text3 + " AND C.CustomerClassID IN (SELECT ClassID FROM Customer_Class WHERE ClassID >= '" + fromCustomerClass + "')";
				}
				if (toCustomerClass != "")
				{
					text3 = text3 + " AND C.CustomerClassID IN (SELECT ClassID FROM Customer_Class WHERE ClassID <= '" + toCustomerClass + "')";
				}
				if (fromGroup != "")
				{
					text3 = text3 + " AND C.CustomerGroupID IN (SELECT GroupID FROM Customer_Group WHERE GroupID >= '" + fromGroup + "')";
				}
				if (toGroup != "")
				{
					text3 = text3 + " AND C.CustomerGroupID IN (SELECT GroupID FROM Customer_Group WHERE GroupID <= '" + toGroup + "') ";
				}
				text3 = text3 + "AND CO. TransactionDate BETWEEN '" + text + "' AND '" + text2 + "' ";
				FillDataSet(dataSet, "Consign_Out", text3);
				new DataTable();
				foreach (DataRow row in dataSet.Tables["Consign_Out"].Rows)
				{
					sysDocID = row["sysDocID"].ToString();
					voucherID = row["voucherID"].ToString();
					text3 = "SELECT '" + sysDocID + "' AS DocID, '" + voucherID + "' AS ReceiptNumber , TEMP.ProductID,TEMP.Description, TEMP.UnitID,TEMP.SysDocID,TEMP.VoucherID,\r\n                        TEMP.Quantity,TEMP.[QTY Settled],TEMP.[QTY Returned],Temp.Cost,TEMP.[Total Cost],TEMP.Price,Temp.Amount,\r\n                        SUM(Temp.Expense) AS [Item Expense]\r\n                        FROM \r\n                        (SELECT  COD.ProductID,COD.Description, COD.UnitID,CSD.SysDocID,CSD.VoucherID,\r\n                        ISNULL(COD.UnitQuantity,COD.Quantity) AS Quantity, (CSD.Quantity) [QTY Settled],(COD.QuantityReturned) [QTY Returned],\r\n                        (SELECT (SUM(IT.Assetvalue)/SUM(IT.Quantity)) FROM Inventory_Transactions IT WHERE IT.SysDocID=CSD.SysDocID AND IT.VoucherID=CSD.VoucherID AND IT.ProductID=CSD.ProductID AND IT.RowIndex = CSD.RowIndex) AS [Cost],\r\n                        (SELECT (SUM(IT.Assetvalue)/SUM(IT.Quantity))*(CSD.Quantity) FROM Inventory_Transactions IT WHERE IT.SysDocID=CSD.SysDocID AND IT.VoucherID=CSD.VoucherID AND IT.RowIndex=CSD.RowIndex  AND ABS(IT.Quantity)=CSD.Quantity AND IT.ProductID=CSD.ProductID ) AS [Total Cost],\r\n                        CSD.UnitPrice AS [Price],\r\n                        (SELECT (SUM(IT.UnitPrice*IT.Quantity))/SUM(IT.Quantity)*(CSD.Quantity) FROM Inventory_Transactions IT WHERE IT.SysDocID=CSD.SysDocID AND IT.VoucherID=CSD.VoucherID AND IT.ProductID=CSD.ProductID  AND IT.RowIndex = CSD.RowIndex AND ABS(IT.Quantity) = CSD.Quantity) AS [Amount],\r\n                        (SELECT (CSD.Amount*(SELECT SUM(cast (CE.Amount as decimal(18,5))) FROM ConsignOut_Expense CE WHERE CE.SysDocID=CS.SysDocID AND CE.VoucherID=CS.VoucherID)/(SELECT -1*(SUM(IT.UnitPrice*IT.Quantity)) FROM Inventory_Transactions IT WHERE IT.SysDocID=CSD.SysDocID AND IT.VoucherID=CSD.VoucherID))) AS Expense\r\n                        FROM  Consign_Out CO LEFT OUTER JOIN  Consign_Out_Detail COD ON CO.SysDocID = COD.SysDocID AND CO.VoucherID = COD.VoucherID\r\n                        LEFT OUTER JOIN ConsignOut_Settlement_Detail CSD ON CSD.ConsignSysDocID = COD.SysDocID AND CSD.ConsignVoucherID = COD.VoucherID AND CSD.ConsignRowIndex = COD.RowIndex\r\n                        LEFT OUTER JOIN Inventory_Transactions IT ON IT.SysDocID = COD.SysDocID AND IT.VoucherID = COD.VoucherID AND IT.RowIndex = COD.RowIndex\r\n                        LEFT JOIN ConsignOut_Settlement CS ON CS.SysDocID=CSD.SysDocID AND CS.VoucherID=CSD.VoucherID\r\n                        LEFT JOIN Customer C ON C.CustomerID=CO.CustomerID\r\n                        WHERE CSD.SysDocID='" + sysDocID + "' AND CSD.VoucherID= '" + voucherID + "' ";
					text3 = text3 + "AND CO. TransactionDate BETWEEN '" + text + "' AND '" + text2 + "' ) AS TEMP WHERE 1=1";
					text3 += "GROUP BY TEMP.ProductID,TEMP.Description,TEMP.SysDocID,TEMP.VoucherID, TEMP.UnitID,TEMP.Quantity,TEMP.[QTY Settled],TEMP.[QTY Returned],Temp.Cost,TEMP.[Total Cost],TEMP.Price,Temp.Amount";
					FillDataSet(dataSet, "Consign_Out_Detail", text3);
				}
				dataSet.Relations.Add("ConsignOut_Rel", new DataColumn[2]
				{
					dataSet.Tables["Consign_Out"].Columns["SysDocID"],
					dataSet.Tables["Consign_Out"].Columns["VoucherID"]
				}, new DataColumn[2]
				{
					dataSet.Tables["Consign_Out_Detail"].Columns["SysDocID"],
					dataSet.Tables["Consign_Out_Detail"].Columns["VoucherID"]
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
