using Micromind.Common;
using Micromind.Common.Data;
using Micromind.Common.Libraries;
using System;
using System.Collections;
using System.Data;
using System.Data.SqlClient;

namespace Micromind.Data
{
	public sealed class SalesReceipt : StoreObject
	{
		private const string SALESRECEIPT_TABLE = "Sales_Receipt";

		private const string SALESRECEIPTDETAIL_TABLE = "Sales_Receipt_Detail";

		private const string SYSDOCID_PARM = "@SysDocID";

		private const string VOUCHERID_PARM = "@VoucherID";

		private const string CUSTOMERID_PARM = "@CustomerID";

		private const string SALESFLOW_PARM = "@SalesFlow";

		private const string TRANSACTIONDATE_PARM = "@TransactionDate";

		private const string SALESPERSONID_PARM = "@SalespersonID";

		private const string ROWINDEX_PARM = "@RowIndex";

		private const string REQUIREDDATE_PARM = "@RequiredDate";

		private const string SHIPPINGADDRESSID_PARM = "@ShippingAddressID";

		private const string CUSTOMERADDRESS_PARM = "@CustomerAddress";

		private const string STATUS_PARM = "@Status";

		private const string CURRENCYID_PARM = "@CurrencyID";

		private const string CURRENCYRATE_PARM = "@CurrencyRate";

		private const string PRICEINCLUDETAX_PARM = "@PriceIncludeTax";

		private const string TERMID_PARM = "@TermID";

		private const string SHIPPINGMETHODID_PARM = "@ShippingMethodID";

		private const string REFERENCE_PARM = "@Reference";

		private const string NOTE_PARM = "@Note";

		private const string ISVOID_PARM = "@IsVoid";

		private const string PONUMBER_PARM = "@PONumber";

		private const string DISCOUNT_PARM = "@Discount";

		private const string DISCOUNTFC_PARM = "@DiscountFC";

		private const string TAXAMOUNT_PARM = "@TaxAmount";

		private const string TAXAMOUNTFC_PARM = "@TaxAmountFC";

		private const string TOTAL_PARM = "@Total";

		private const string TOTALFC_PARM = "@TotalFC";

		private const string PRODUCTID_PARM = "@ProductID";

		private const string QUANTITY_PARM = "@Quantity";

		private const string UNITPRICE_PARM = "@UnitPrice";

		private const string UNITPRICEFC_PARM = "@UnitPriceFC";

		private const string DESCRIPTION_PARM = "@Description";

		private const string UNITID_PARM = "@UnitID";

		private const string LOCATIONID_PARM = "@LocationID";

		private const string UNITQUANTITY_PARM = "@UnitQuantity";

		private const string UNITFACTOR_PARM = "@UnitFactor";

		private const string FACTORTYPE_PARM = "@FactorType";

		private const string SUBUNITPRICE_PARM = "@SubunitPrice";

		private const string DNOTEVOUCHERID_PARM = "@DNoteVoucherID";

		private const string DNOTESYSDOCID_PARM = "@DNoteSysDocID";

		private const string ORDERROWINDEX_PARM = "@OrderRowIndex";

		private const string ACCOUNTID_PARM = "@AccountID";

		private const string AMOUNT_PARM = "@Amount";

		private const string PAYMENTMETHODTYPE_PARM = "@PaymentMethodType";

		private const string REGISTERID_PARM = "@RegisterID";

		private const string INVOICEPAYMENT_PARM = "@Invoice_Payment";

		public SalesReceipt(Config config)
			: base(config)
		{
		}

		private string GetInsertUpdateSalesReceiptText(bool isUpdate)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddInsertUpdateParameters("Sales_Receipt", new FieldValue("SysDocID", "@SysDocID", isUpdateConditionField: true), new FieldValue("VoucherID", "@VoucherID", isUpdateConditionField: true), new FieldValue("CustomerID", "@CustomerID"), new FieldValue("SalesFlow", "@SalesFlow"), new FieldValue("TransactionDate", "@TransactionDate"), new FieldValue("SalespersonID", "@SalespersonID"), new FieldValue("RequiredDate", "@RequiredDate"), new FieldValue("ShippingAddressID", "@ShippingAddressID"), new FieldValue("ShippingMethodID", "@ShippingMethodID"), new FieldValue("PriceIncludeTax", "@PriceIncludeTax"), new FieldValue("CustomerAddress", "@CustomerAddress"), new FieldValue("Status", "@Status"), new FieldValue("CurrencyID", "@CurrencyID"), new FieldValue("CurrencyRate", "@CurrencyRate"), new FieldValue("TaxAmount", "@TaxAmount"), new FieldValue("TaxAmountFC", "@TaxAmountFC"), new FieldValue("Discount", "@Discount"), new FieldValue("DiscountFC", "@DiscountFC"), new FieldValue("Total", "@Total"), new FieldValue("TotalFC", "@TotalFC"), new FieldValue("PONumber", "@PONumber"), new FieldValue("RegisterID", "@RegisterID"), new FieldValue("Reference", "@Reference"), new FieldValue("Note", "@Note"));
			if (isUpdate)
			{
				return sqlBuilder.GetUpdateExpression();
			}
			return sqlBuilder.GetInsertExpression();
		}

		private SqlCommand GetInsertUpdateSalesReceiptCommand(bool isUpdate)
		{
			SqlParameterCollection parameters;
			if (isUpdate)
			{
				if (updateCommand != null)
				{
					updateCommand = null;
				}
				updateCommand = new SqlCommand(GetInsertUpdateSalesReceiptText(isUpdate: true), base.DBConfig.Connection);
				updateCommand.CommandType = CommandType.Text;
				parameters = updateCommand.Parameters;
			}
			else
			{
				if (insertCommand != null)
				{
					insertCommand = null;
				}
				insertCommand = new SqlCommand(GetInsertUpdateSalesReceiptText(isUpdate: false), base.DBConfig.Connection);
				insertCommand.CommandType = CommandType.Text;
				parameters = insertCommand.Parameters;
			}
			parameters.Add("@SysDocID", SqlDbType.NVarChar);
			parameters.Add("@VoucherID", SqlDbType.NVarChar);
			parameters.Add("@SalesFlow", SqlDbType.TinyInt);
			parameters.Add("@CustomerID", SqlDbType.NVarChar);
			parameters.Add("@TransactionDate", SqlDbType.DateTime);
			parameters.Add("@SalespersonID", SqlDbType.NVarChar);
			parameters.Add("@PriceIncludeTax", SqlDbType.Bit);
			parameters.Add("@RequiredDate", SqlDbType.DateTime);
			parameters.Add("@ShippingAddressID", SqlDbType.NVarChar);
			parameters.Add("@CustomerAddress", SqlDbType.NVarChar);
			parameters.Add("@ShippingMethodID", SqlDbType.NVarChar);
			parameters.Add("@Status", SqlDbType.TinyInt);
			parameters.Add("@CurrencyID", SqlDbType.NVarChar);
			parameters.Add("@CurrencyRate", SqlDbType.Decimal);
			parameters.Add("@Reference", SqlDbType.NVarChar);
			parameters.Add("@RegisterID", SqlDbType.NVarChar);
			parameters.Add("@PONumber", SqlDbType.NVarChar);
			parameters.Add("@Discount", SqlDbType.Decimal);
			parameters.Add("@DiscountFC", SqlDbType.Decimal);
			parameters.Add("@TaxAmount", SqlDbType.Decimal);
			parameters.Add("@TaxAmountFC", SqlDbType.Decimal);
			parameters.Add("@Total", SqlDbType.Decimal);
			parameters.Add("@TotalFC", SqlDbType.Decimal);
			parameters.Add("@Note", SqlDbType.NVarChar);
			parameters["@SysDocID"].SourceColumn = "SysDocID";
			parameters["@VoucherID"].SourceColumn = "VoucherID";
			parameters["@CustomerID"].SourceColumn = "CustomerID";
			parameters["@SalesFlow"].SourceColumn = "SalesFlow";
			parameters["@TransactionDate"].SourceColumn = "TransactionDate";
			parameters["@SalespersonID"].SourceColumn = "SalespersonID";
			parameters["@RequiredDate"].SourceColumn = "RequiredDate";
			parameters["@ShippingAddressID"].SourceColumn = "ShippingAddressID";
			parameters["@CustomerAddress"].SourceColumn = "CustomerAddress";
			parameters["@ShippingMethodID"].SourceColumn = "ShippingMethodID";
			parameters["@Status"].SourceColumn = "Status";
			parameters["@PriceIncludeTax"].SourceColumn = "PriceIncludeTax";
			parameters["@CurrencyID"].SourceColumn = "CurrencyID";
			parameters["@CurrencyRate"].SourceColumn = "CurrencyRate";
			parameters["@RegisterID"].SourceColumn = "RegisterID";
			parameters["@Reference"].SourceColumn = "Reference";
			parameters["@PONumber"].SourceColumn = "PONumber";
			parameters["@TaxAmount"].SourceColumn = "TaxAmount";
			parameters["@TaxAmountFC"].SourceColumn = "TaxAmountFC";
			parameters["@Discount"].SourceColumn = "Discount";
			parameters["@DiscountFC"].SourceColumn = "DiscountFC";
			parameters["@Total"].SourceColumn = "Total";
			parameters["@TotalFC"].SourceColumn = "TotalFC";
			parameters["@Note"].SourceColumn = "Note";
			if (isUpdate)
			{
				return updateCommand;
			}
			return insertCommand;
		}

		private string GetInsertUpdateSalesReceiptDetailsText(bool isUpdate)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddInsertUpdateParameters("Sales_Receipt_Detail", new FieldValue("SysDocID", "@SysDocID"), new FieldValue("VoucherID", "@VoucherID"), new FieldValue("ProductID", "@ProductID"), new FieldValue("RowIndex", "@RowIndex"), new FieldValue("Quantity", "@Quantity"), new FieldValue("UnitPrice", "@UnitPrice"), new FieldValue("UnitPriceFC", "@UnitPriceFC"), new FieldValue("Discount", "@Discount"), new FieldValue("Description", "@Description"), new FieldValue("UnitID", "@UnitID"), new FieldValue("LocationID", "@LocationID"), new FieldValue("UnitQuantity", "@UnitQuantity"), new FieldValue("UnitFactor", "@UnitFactor"), new FieldValue("FactorType", "@FactorType"), new FieldValue("DNoteSysDocID", "@DNoteSysDocID"), new FieldValue("DNoteVoucherID", "@DNoteVoucherID"), new FieldValue("OrderRowIndex", "@OrderRowIndex"), new FieldValue("SubunitPrice", "@SubunitPrice"));
			if (isUpdate)
			{
				return sqlBuilder.GetUpdateExpression();
			}
			return sqlBuilder.GetInsertExpression();
		}

		private SqlCommand GetInsertUpdateSalesReceiptDetailsCommand(bool isUpdate)
		{
			SqlParameterCollection parameters;
			if (isUpdate)
			{
				if (updateCommand != null)
				{
					updateCommand = null;
				}
				updateCommand = new SqlCommand(GetInsertUpdateSalesReceiptDetailsText(isUpdate: true), base.DBConfig.Connection);
				updateCommand.CommandType = CommandType.Text;
				parameters = updateCommand.Parameters;
			}
			else
			{
				if (insertCommand != null)
				{
					insertCommand = null;
				}
				insertCommand = new SqlCommand(GetInsertUpdateSalesReceiptDetailsText(isUpdate: false), base.DBConfig.Connection);
				insertCommand.CommandType = CommandType.Text;
				parameters = insertCommand.Parameters;
			}
			parameters.Add("@SysDocID", SqlDbType.NVarChar);
			parameters.Add("@VoucherID", SqlDbType.NVarChar);
			parameters.Add("@ProductID", SqlDbType.NVarChar);
			parameters.Add("@RowIndex", SqlDbType.Int);
			parameters.Add("@Quantity", SqlDbType.Real);
			parameters.Add("@UnitPrice", SqlDbType.Decimal);
			parameters.Add("@UnitPriceFC", SqlDbType.Decimal);
			parameters.Add("@Discount", SqlDbType.Decimal);
			parameters.Add("@Description", SqlDbType.NVarChar);
			parameters.Add("@UnitID", SqlDbType.NVarChar);
			parameters.Add("@LocationID", SqlDbType.NVarChar);
			parameters.Add("@UnitQuantity", SqlDbType.Real);
			parameters.Add("@UnitFactor", SqlDbType.Decimal);
			parameters.Add("@FactorType", SqlDbType.NVarChar);
			parameters.Add("@SubunitPrice", SqlDbType.Decimal);
			parameters.Add("@DNoteSysDocID", SqlDbType.NVarChar);
			parameters.Add("@DNoteVoucherID", SqlDbType.NVarChar);
			parameters.Add("@OrderRowIndex", SqlDbType.Int);
			parameters["@SysDocID"].SourceColumn = "SysDocID";
			parameters["@VoucherID"].SourceColumn = "VoucherID";
			parameters["@ProductID"].SourceColumn = "ProductID";
			parameters["@RowIndex"].SourceColumn = "RowIndex";
			parameters["@Quantity"].SourceColumn = "Quantity";
			parameters["@UnitPrice"].SourceColumn = "UnitPrice";
			parameters["@UnitPriceFC"].SourceColumn = "UnitPriceFC";
			parameters["@Discount"].SourceColumn = "Discount";
			parameters["@Description"].SourceColumn = "Description";
			parameters["@UnitID"].SourceColumn = "UnitID";
			parameters["@LocationID"].SourceColumn = "LocationID";
			parameters["@UnitQuantity"].SourceColumn = "UnitQuantity";
			parameters["@UnitFactor"].SourceColumn = "UnitFactor";
			parameters["@FactorType"].SourceColumn = "FactorType";
			parameters["@SubunitPrice"].SourceColumn = "SubunitPrice";
			parameters["@DNoteSysDocID"].SourceColumn = "DNoteSysDocID";
			parameters["@DNoteVoucherID"].SourceColumn = "DNoteVoucherID";
			parameters["@OrderRowIndex"].SourceColumn = "OrderRowIndex";
			if (isUpdate)
			{
				return updateCommand;
			}
			return insertCommand;
		}

		private string GetInsertUpdatePaymentText(bool isUpdate)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddInsertUpdateParameters("Invoice_Payment", new FieldValue("SysDocID", "@SysDocID"), new FieldValue("VoucherID", "@VoucherID"), new FieldValue("TransactionDate", "@TransactionDate"), new FieldValue("AccountID", "@AccountID"), new FieldValue("RegisterID", "@RegisterID"), new FieldValue("Amount", "@Amount"), new FieldValue("PaymentMethodType", "@PaymentMethodType"));
			if (isUpdate)
			{
				return sqlBuilder.GetUpdateExpression();
			}
			return sqlBuilder.GetInsertExpression();
		}

		private SqlCommand GetInsertUpdatePaymentCommand(bool isUpdate)
		{
			SqlParameterCollection parameters;
			if (isUpdate)
			{
				if (updateCommand != null)
				{
					updateCommand = null;
				}
				updateCommand = new SqlCommand(GetInsertUpdatePaymentText(isUpdate: true), base.DBConfig.Connection);
				updateCommand.CommandType = CommandType.Text;
				parameters = updateCommand.Parameters;
			}
			else
			{
				if (insertCommand != null)
				{
					insertCommand = null;
				}
				insertCommand = new SqlCommand(GetInsertUpdatePaymentText(isUpdate: false), base.DBConfig.Connection);
				insertCommand.CommandType = CommandType.Text;
				parameters = insertCommand.Parameters;
			}
			parameters.Add("@SysDocID", SqlDbType.NVarChar);
			parameters.Add("@VoucherID", SqlDbType.NVarChar);
			parameters.Add("@TransactionDate", SqlDbType.DateTime);
			parameters.Add("@AccountID", SqlDbType.NVarChar);
			parameters.Add("@RegisterID", SqlDbType.NVarChar);
			parameters.Add("@Amount", SqlDbType.Decimal);
			parameters.Add("@PaymentMethodType", SqlDbType.TinyInt);
			parameters["@SysDocID"].SourceColumn = "SysDocID";
			parameters["@VoucherID"].SourceColumn = "VoucherID";
			parameters["@TransactionDate"].SourceColumn = "TransactionDate";
			parameters["@AccountID"].SourceColumn = "AccountID";
			parameters["@RegisterID"].SourceColumn = "RegisterID";
			parameters["@Amount"].SourceColumn = "Amount";
			parameters["@PaymentMethodType"].SourceColumn = "PaymentMethodType";
			if (isUpdate)
			{
				return updateCommand;
			}
			return insertCommand;
		}

		private bool ValidateData(SalesReceiptData journalData)
		{
			return true;
		}

		public bool InsertUpdateSalesReceipt(SalesReceiptData salesReceiptData, bool isUpdate)
		{
			bool flag = true;
			SqlCommand insertUpdateSalesReceiptCommand = GetInsertUpdateSalesReceiptCommand(isUpdate);
			string text = "";
			try
			{
				DataRow dataRow = salesReceiptData.SalesReceiptTable.Rows[0];
				SqlTransaction sqlTransaction = base.DBConfig.StartNewTransaction();
				string text2 = dataRow["VoucherID"].ToString();
				string sysDocID = dataRow["SysDocID"].ToString();
				if (!isUpdate && new SystemDocuments(base.DBConfig).ExistDocumentNumber("Sales_Receipt", "VoucherID", dataRow["SysDocID"].ToString(), text2, sqlTransaction))
				{
					throw new CompanyException("Document number already exist.", 1046);
				}
				string baseCurrencyID = new Currencies(base.DBConfig).GetBaseCurrencyID();
				bool flag2 = false;
				decimal result = 1m;
				string a = "M";
				if (dataRow["CurrencyID"] != DBNull.Value && baseCurrencyID != dataRow["CurrencyID"].ToString())
				{
					flag2 = true;
					decimal.TryParse(dataRow["CurrencyRate"].ToString(), out result);
					a = new Currencies(base.DBConfig).GetCurrencyRateType(dataRow["CurrencyID"].ToString());
				}
				if (flag2)
				{
					decimal result2 = default(decimal);
					dataRow["TotalFC"] = dataRow["Total"];
					decimal.TryParse(dataRow["Total"].ToString(), out result2);
					result2 = ((!(a == "M")) ? Math.Round(result2 / result, 4) : Math.Round(result2 * result, 4));
					dataRow["Total"] = result2;
					result2 = default(decimal);
					dataRow["DiscountFC"] = dataRow["Discount"];
					decimal.TryParse(dataRow["DiscountFC"].ToString(), out result2);
					result2 = ((!(a == "M")) ? Math.Round(result2 / result, 4) : Math.Round(result2 * result, 4));
					dataRow["Discount"] = result2;
					result2 = default(decimal);
					dataRow["TaxAmountFC"] = dataRow["TaxAmount"];
					decimal.TryParse(dataRow["TaxAmount"].ToString(), out result2);
					result2 = ((!(a == "M")) ? Math.Round(result2 / result, 4) : Math.Round(result2 * result, 4));
					dataRow["TaxAmount"] = result2;
				}
				foreach (DataRow row in salesReceiptData.SalesReceiptDetailTable.Rows)
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
					if (flag2)
					{
						decimal result3 = default(decimal);
						row["UnitPriceFC"] = row["UnitPrice"];
						decimal.TryParse(row["UnitPrice"].ToString(), out result3);
						result3 = ((!(a == "M")) ? Math.Round(result3 / result, 4) : Math.Round(result3 * result, 4));
						row["UnitPrice"] = result3;
					}
				}
				insertUpdateSalesReceiptCommand.Transaction = sqlTransaction;
				flag = (isUpdate ? (flag & Update(salesReceiptData, "Sales_Receipt", insertUpdateSalesReceiptCommand)) : (flag & Insert(salesReceiptData, "Sales_Receipt", insertUpdateSalesReceiptCommand)));
				insertUpdateSalesReceiptCommand = GetInsertUpdateSalesReceiptDetailsCommand(isUpdate: false);
				insertUpdateSalesReceiptCommand.Transaction = sqlTransaction;
				if (isUpdate)
				{
					flag &= DeleteSalesReceiptDetailsRows(sysDocID, text2, isDeletingTransaction: false, sqlTransaction);
					flag &= new InventoryTransaction(base.DBConfig).DeleteInventoryTransaction(26, sysDocID, text2, isDeletingTransaction: false, sqlTransaction);
				}
				if (salesReceiptData.Tables["Sales_Receipt_Detail"].Rows.Count > 0)
				{
					flag &= Insert(salesReceiptData, "Sales_Receipt_Detail", insertUpdateSalesReceiptCommand);
				}
				insertUpdateSalesReceiptCommand = GetInsertUpdatePaymentCommand(isUpdate: false);
				insertUpdateSalesReceiptCommand.Transaction = sqlTransaction;
				if (isUpdate)
				{
					flag &= DeletePaymentRows(sysDocID, text2, sqlTransaction);
				}
				DataRow dataRow3 = salesReceiptData.PaymentTable.Rows[0];
				PaymentMethodTypes paymentMethodTypes = (PaymentMethodTypes)byte.Parse(dataRow3["PaymentMethodType"].ToString());
				string registerID = dataRow3["RegisterID"].ToString();
				string text5 = "";
				text5 = (string)(dataRow3["AccountID"] = ((paymentMethodTypes != PaymentMethodTypes.CreditCard) ? new Register(base.DBConfig).GetRegisterAccountID(registerID, "CashAccountID") : new Register(base.DBConfig).GetRegisterAccountID(registerID, "CardReceivedAccountID")));
				dataRow3.EndEdit();
				if (salesReceiptData.Tables["Sales_Receipt_Detail"].Rows.Count > 0)
				{
					flag &= Insert(salesReceiptData, "Sales_Receipt_Detail", insertUpdateSalesReceiptCommand);
				}
				InventoryTransactionData inventoryTransactionData = new InventoryTransactionData();
				foreach (DataRow row2 in salesReceiptData.SalesReceiptDetailTable.Rows)
				{
					DataRow dataRow5 = inventoryTransactionData.InventoryTransactionTable.NewRow();
					dataRow5.BeginEdit();
					dataRow5["SysDocID"] = row2["SysDocID"];
					dataRow5["VoucherID"] = row2["VoucherID"];
					if (row2["LocationID"].ToString() == "")
					{
						throw new Exception("Location cannot be empty.");
					}
					dataRow5["LocationID"] = row2["LocationID"];
					dataRow5["ProductID"] = row2["ProductID"];
					dataRow5["Quantity"] = -1m * decimal.Parse(row2["Quantity"].ToString());
					dataRow5["Reference"] = dataRow["Reference"];
					dataRow5["SysDocType"] = (byte)26;
					dataRow5["TransactionDate"] = dataRow["TransactionDate"];
					dataRow5["TransactionType"] = (byte)2;
					dataRow5["SpecificationID"] = row2["SpecificationID"];
					dataRow5["StyleID"] = row2["StyleID"];
					dataRow5["UnitPrice"] = 0;
					dataRow5["RowIndex"] = row2["RowIndex"];
					dataRow5.EndEdit();
					inventoryTransactionData.InventoryTransactionTable.Rows.Add(dataRow5);
				}
				flag &= new InventoryTransaction(base.DBConfig).InsertUpdateInventoryTransaction(inventoryTransactionData, isUpdate, sqlTransaction);
				GLData journalData = CreateReceiptGLData(salesReceiptData, sqlTransaction);
				flag &= new Journal(base.DBConfig).InsertUpdateJournal(journalData, isUpdate, sqlTransaction);
				flag &= UpdateInventoryTransactionRowID(sysDocID, text2, sqlTransaction);
				if (!flag)
				{
					throw new Exception("Failed to save the transaction because of an unexpected error. Please try again.");
				}
				flag &= UpdateTableRowInsertUpdateInfo("Sales_Receipt", "SysDocID", dataRow["SysDocID"].ToString(), "VoucherID", dataRow["VoucherID"].ToString(), sqlTransaction, !isUpdate);
				string entityName = "Sales Receipt";
				flag = (isUpdate ? (flag & AddActivityLog(entityName, text2, sysDocID, ActivityTypes.Update, sqlTransaction)) : (flag & AddActivityLog(entityName, text2, sysDocID, ActivityTypes.Add, sqlTransaction)));
				if (!isUpdate)
				{
					flag &= new SystemDocuments(base.DBConfig).UpdateNextDocumentNumber(dataRow["SysDocID"].ToString(), dataRow["VoucherID"].ToString(), "Sales_Receipt", "VoucherID", sqlTransaction);
				}
				if (!flag)
				{
					return flag;
				}
				flag &= new Approval(base.DBConfig).CreateTransactionApprovalTasks(SysDocTypes.SalesReceipt, sysDocID, text2, "Sales_Receipt", sqlTransaction);
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
				string exp = "UPDATE SID SET ITRowID = (SELECT TransactionID FROM Inventory_Transactions IT WHERE IT.SysDocID = SID.SysDocID AND IT.VoucherID = SID.VoucherID AND IT.RowIndex = SID.RowIndex) \r\n                                    FROM Sales_POS_Detail SID INNER JOIN Sales_POS SI ON SI.SysDocID = SID.SysDocID AND SI.VoucherID = SID.VoucherID\r\n                                     where sid.SysDocID = '" + sysDocID + "' and sid.voucherid = '" + voucherID + "'";
				return ExecuteNonQuery(exp, sqlTransaction) > 0;
			}
			catch
			{
				throw;
			}
		}

		private GLData CreateReceiptGLData(SalesReceiptData transactionData, SqlTransaction sqlTransaction)
		{
			GLData gLData = new GLData();
			DataRow dataRow = transactionData.SalesReceiptTable.Rows[0];
			string value = dataRow["CustomerID"].ToString();
			string idFieldValue = dataRow["SysDocID"].ToString();
			string text = new Databases(base.DBConfig).GetFieldValue("System_Document", "LocationID", "SysDocID", idFieldValue, sqlTransaction).ToString();
			string text2 = new Databases(base.DBConfig).GetFieldValue("Location", "InventoryAccountID", "LocationID", text, sqlTransaction).ToString();
			string value2 = new Databases(base.DBConfig).GetFieldValue("Location", "DiscountGivenAccountID", "LocationID", text, sqlTransaction).ToString();
			string value3 = new Databases(base.DBConfig).GetFieldValue("Location", "SalesTaxAccountID", "LocationID", text, sqlTransaction).ToString();
			bool result = false;
			if (!dataRow["PriceIncludeTax"].IsDBNullOrEmpty())
			{
				bool.TryParse(dataRow["PriceIncludeTax"].ToString(), out result);
			}
			string baseCurrencyID = new Currencies(base.DBConfig).GetBaseCurrencyID();
			bool flag = false;
			decimal result2 = 1m;
			if (dataRow["CurrencyID"] != DBNull.Value && baseCurrencyID != dataRow["CurrencyID"].ToString())
			{
				flag = true;
				decimal.TryParse(dataRow["CurrencyRate"].ToString(), out result2);
			}
			DataRow dataRow2 = gLData.JournalTable.NewRow();
			SysDocTypes sysDocTypes = SysDocTypes.SalesReceipt;
			dataRow2["JournalID"] = 0;
			dataRow2["JournalDate"] = dataRow["TransactionDate"];
			dataRow2["SysDocID"] = dataRow["SysDocID"];
			dataRow2["SysDocType"] = (byte)sysDocTypes;
			dataRow2["VoucherID"] = dataRow["VoucherID"];
			dataRow2["CurrencyID"] = dataRow["CurrencyID"];
			dataRow2["CurrencyRate"] = dataRow["CurrencyRate"];
			dataRow2["Reference"] = dataRow["Reference"];
			dataRow2["Narration"] = "Sales Receipt - ";
			dataRow2.EndEdit();
			gLData.JournalTable.Rows.Add(dataRow2);
			decimal num = default(decimal);
			Hashtable hashtable = new Hashtable();
			ArrayList arrayList = new ArrayList();
			decimal num2 = default(decimal);
			decimal num3 = default(decimal);
			foreach (DataRow row in transactionData.SalesReceiptDetailTable.Rows)
			{
				text = row["LocationID"].ToString();
				decimal num4 = default(decimal);
				string text3 = row["ProductID"].ToString();
				ItemTypes itemTypes = ItemTypes.Inventory;
				object fieldValue = new Databases(base.DBConfig).GetFieldValue("Product", "ItemType", "ProductID", text3, sqlTransaction);
				if (fieldValue == null || !(fieldValue.ToString() != ""))
				{
					throw new CompanyException("Item type is not selected for the product:" + text3);
				}
				itemTypes = (ItemTypes)byte.Parse(fieldValue.ToString());
				num4 = ((row["UnitQuantity"] == DBNull.Value) ? decimal.Parse(row["Quantity"].ToString()) : decimal.Parse(row["UnitQuantity"].ToString()));
				decimal productCurrentCost = new Products(base.DBConfig).GetProductCurrentCost(text3, sqlTransaction);
				decimal result3 = default(decimal);
				if (flag)
				{
					decimal.TryParse(row["UnitPriceFC"].ToString(), out result3);
				}
				else
				{
					decimal.TryParse(row["UnitPrice"].ToString(), out result3);
				}
				if (itemTypes != ItemTypes.Inventory)
				{
					num3 += num4 * result3;
				}
				else
				{
					if (hashtable.ContainsKey(text))
					{
						num = decimal.Parse(hashtable[text].ToString());
						num += num4 * productCurrentCost;
						hashtable[text] = num;
					}
					else
					{
						hashtable.Add(text, num4 * productCurrentCost);
						arrayList.Add(text);
					}
					num2 += num4 * productCurrentCost;
					num3 += num4 * result3;
				}
			}
			DataRow dataRow4;
			if (num2 != 0m)
			{
				for (int i = 0; i < hashtable.Count; i++)
				{
					dataRow4 = gLData.JournalDetailsTable.NewRow();
					dataRow4.BeginEdit();
					text = arrayList[i].ToString();
					num = decimal.Parse(hashtable[text].ToString());
					text2 = new Databases(base.DBConfig).GetFieldValue("Location", "InventoryAccountID", "LocationID", text, sqlTransaction).ToString();
					dataRow4["JournalID"] = 0;
					dataRow4["AccountID"] = text2;
					dataRow4["PayeeID"] = value;
					dataRow4["Debit"] = DBNull.Value;
					dataRow4["Credit"] = num;
					dataRow4["Reference"] = dataRow["Reference"];
					dataRow4["JVEntryType"] = (byte)1;
					dataRow4.EndEdit();
					gLData.JournalDetailsTable.Rows.Add(dataRow4);
				}
			}
			if (num2 != 0m)
			{
				dataRow4 = gLData.JournalDetailsTable.NewRow();
				dataRow4.BeginEdit();
				text = new Databases(base.DBConfig).GetFieldValue("System_Document", "LocationID", "SysDocID", idFieldValue, sqlTransaction).ToString();
				string value4 = new Databases(base.DBConfig).GetFieldValue("Location", "COGSAccountID", "LocationID", text, sqlTransaction).ToString();
				dataRow4["JournalID"] = 0;
				dataRow4["AccountID"] = value4;
				dataRow4["PayeeID"] = value;
				dataRow4["Debit"] = num2;
				dataRow4["Credit"] = DBNull.Value;
				dataRow4["JVEntryType"] = (byte)2;
				dataRow4["Reference"] = dataRow["Reference"];
				dataRow4.EndEdit();
				gLData.JournalDetailsTable.Rows.Add(dataRow4);
			}
			dataRow4 = gLData.JournalDetailsTable.NewRow();
			dataRow4.BeginEdit();
			string value5 = new Databases(base.DBConfig).GetFieldValue("Location", "SalesAccountID", "LocationID", text, sqlTransaction).ToString();
			dataRow4["JournalID"] = 0;
			dataRow4["AccountID"] = value5;
			dataRow4["PayeeID"] = value;
			dataRow4["Debit"] = DBNull.Value;
			dataRow4["Credit"] = num3;
			dataRow4["JVEntryType"] = (byte)3;
			dataRow4["Reference"] = dataRow["Reference"];
			dataRow4.EndEdit();
			gLData.JournalDetailsTable.Rows.Add(dataRow4);
			decimal result4 = default(decimal);
			decimal result5 = default(decimal);
			if (dataRow["DiscountFC"] != DBNull.Value)
			{
				decimal.TryParse(dataRow["DiscountFC"].ToString(), out result4);
			}
			else
			{
				decimal.TryParse(dataRow["Discount"].ToString(), out result4);
			}
			if (dataRow["TaxAmountFC"] != DBNull.Value)
			{
				decimal.TryParse(dataRow["TaxAmountFC"].ToString(), out result5);
			}
			else
			{
				decimal.TryParse(dataRow["TaxAmount"].ToString(), out result5);
			}
			if (result4 > 0m)
			{
				dataRow4 = gLData.JournalDetailsTable.NewRow();
				dataRow4.BeginEdit();
				dataRow4["JournalID"] = 0;
				dataRow4["AccountID"] = value2;
				dataRow4["PayeeID"] = value;
				dataRow4["PayeeType"] = "A";
				dataRow4["Debit"] = result4;
				dataRow4["Credit"] = DBNull.Value;
				dataRow4["Reference"] = dataRow["Reference"];
				dataRow4["JVEntryType"] = (byte)5;
				dataRow4.EndEdit();
				gLData.JournalDetailsTable.Rows.Add(dataRow4);
			}
			if (result5 > 0m)
			{
				dataRow4 = gLData.JournalDetailsTable.NewRow();
				dataRow4.BeginEdit();
				dataRow4["JournalID"] = 0;
				dataRow4["AccountID"] = value3;
				dataRow4["PayeeID"] = value;
				dataRow4["PayeeType"] = "A";
				dataRow4["Debit"] = DBNull.Value;
				dataRow4["Credit"] = result5;
				dataRow4["JVEntryType"] = (byte)6;
				dataRow4["Reference"] = dataRow["Reference"];
				dataRow4.EndEdit();
				gLData.JournalDetailsTable.Rows.Add(dataRow4);
			}
			DataRow dataRow5 = transactionData.PaymentTable.Rows[0];
			dataRow4 = gLData.JournalDetailsTable.NewRow();
			dataRow4.BeginEdit();
			dataRow4["JournalID"] = 0;
			dataRow4["AccountID"] = dataRow5["AccountID"].ToString();
			dataRow4["PayeeID"] = value;
			dataRow4["PayeeType"] = "A";
			dataRow4["IsARAP"] = false;
			dataRow4["Debit"] = dataRow5["Amount"];
			dataRow4["Credit"] = DBNull.Value;
			dataRow4["Reference"] = dataRow["Reference"];
			dataRow4.EndEdit();
			gLData.JournalDetailsTable.Rows.Add(dataRow4);
			return gLData;
		}

		public SalesReceiptData GetSalesReceiptByID(string sysDocID, string voucherID)
		{
			try
			{
				SalesReceiptData salesReceiptData = new SalesReceiptData();
				string textCommand = "SELECT * FROM Sales_Receipt WHERE VoucherID='" + voucherID + "' AND SysDocID='" + sysDocID + "'";
				FillDataSet(salesReceiptData, "Sales_Receipt", textCommand);
				if (salesReceiptData == null || salesReceiptData.Tables.Count == 0 || salesReceiptData.Tables["Sales_Receipt"].Rows.Count == 0)
				{
					return null;
				}
				textCommand = "SELECT TD.*,Product.Description, Product.Attribute1,Product.Attribute2,Product.Attribute3,Product.MatrixParentID\r\n                        FROM Sales_Receipt_Detail TD INNER JOIN Product ON TD.ProductID=Product.ProductID\r\n                        WHERE VoucherID='" + voucherID + "' AND SysDocID='" + sysDocID + "' ORDER BY TD.RowIndex ";
				FillDataSet(salesReceiptData, "Sales_Receipt_Detail", textCommand);
				return salesReceiptData;
			}
			catch
			{
				throw;
			}
		}

		internal bool DeleteSalesReceiptDetailsRows(string sysDocID, string voucherID, bool isDeletingTransaction, SqlTransaction sqlTransaction)
		{
			bool flag = true;
			try
			{
				SalesReceiptData salesReceiptData = new SalesReceiptData();
				string textCommand = "SELECT SOD.*,ISVOID FROM Sales_Receipt_Detail SOD INNER JOIN Sales_Receipt SO ON SO.SysDocID=SOD.SysDocID AND SO.VOucherID=SOD.VoucherID\r\n                              WHERE SOD.SysDocID = '" + sysDocID + "' AND SOD.VoucherID = '" + voucherID + "'";
				FillDataSet(salesReceiptData, "Sales_Receipt_Detail", textCommand, sqlTransaction);
				bool result = false;
				bool.TryParse(new Databases(base.DBConfig).GetFieldValue("Company", "IsDNInventory", "CompanyID", 1, sqlTransaction).ToString(), out result);
				bool result2 = false;
				bool.TryParse(salesReceiptData.SalesReceiptDetailTable.Rows[0]["IsVoid"].ToString(), out result2);
				if (!result2 && !result)
				{
					flag &= new InventoryTransaction(base.DBConfig).DeleteInventoryTransaction(26, sysDocID, voucherID, isDeletingTransaction, sqlTransaction);
				}
				textCommand = "DELETE FROM Sales_Receipt_Detail WHERE SysDocID = '" + sysDocID + "' AND VoucherID = '" + voucherID + "'";
				return flag & Delete(textCommand, sqlTransaction);
			}
			catch
			{
				throw;
			}
		}

		internal bool DeletePaymentRows(string sysDocID, string voucherID, SqlTransaction sqlTransaction)
		{
			bool flag = true;
			try
			{
				string commandText = "DELETE FROM Invoice_Payment WHERE SysDocID = '" + sysDocID + "' AND VoucherID = '" + voucherID + "'";
				return Delete(commandText, sqlTransaction);
			}
			catch
			{
				throw;
			}
		}

		public bool VoidSalesReceipt(string sysDocID, string voucherID, bool isVoid)
		{
			bool flag = true;
			try
			{
				SqlTransaction sqlTransaction = base.DBConfig.StartNewTransaction();
				bool result = false;
				bool.TryParse(new Databases(base.DBConfig).GetFieldValue("Company", "IsDNInventory", "CompanyID", 1, sqlTransaction).ToString(), out result);
				SalesReceiptData dataSet = new SalesReceiptData();
				string textCommand = "SELECT * FROM Sales_Receipt_Detail\r\n                              WHERE SysDocID = '" + sysDocID + "' AND VoucherID = '" + voucherID + "'";
				FillDataSet(dataSet, "Sales_Receipt_Detail", textCommand, sqlTransaction);
				if (!result)
				{
					flag &= new InventoryTransaction(base.DBConfig).DeleteInventoryTransaction(26, sysDocID, voucherID, isDeletingTransaction: true, sqlTransaction);
				}
				flag &= new Journal(base.DBConfig).VoidJournal(sysDocID, voucherID, isVoid, sqlTransaction);
				textCommand = "UPDATE Sales_Receipt SET IsVoid = '" + isVoid.ToString() + "' WHERE SysDocID='" + sysDocID + "' AND VoucherID = '" + voucherID + "'";
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
				AddActivityLog("Sales Receipt", voucherID, sysDocID, activityType, sqlTransaction);
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

		public bool DeleteSalesReceipt(string sysDocID, string voucherID)
		{
			bool flag = true;
			try
			{
				string text = "";
				SqlTransaction sqlTransaction = base.DBConfig.StartNewTransaction();
				bool result = false;
				bool.TryParse(new Databases(base.DBConfig).GetFieldValue("Company", "IsDNInventory", "CompanyID", 1, sqlTransaction).ToString(), out result);
				flag &= DeleteSalesReceiptDetailsRows(sysDocID, voucherID, isDeletingTransaction: true, sqlTransaction);
				flag &= new Journal(base.DBConfig).DeleteJournal(sysDocID, voucherID, sqlTransaction);
				text = "DELETE FROM Sales_Receipt WHERE SysDocID='" + sysDocID + "' AND VoucherID = '" + voucherID + "'";
				flag &= (ExecuteNonQuery(text, sqlTransaction) >= 0);
				if (!flag)
				{
					return flag;
				}
				ActivityTypes activityType = ActivityTypes.Delete;
				AddActivityLog("Sales Receipt", voucherID, sysDocID, activityType, sqlTransaction);
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

		internal bool CloseShippedReceipt(string sysDocID, string voucherID, SqlTransaction sqlTransaction)
		{
			try
			{
				string str = "SELECT COUNT(RowIndex)FROM Sales_Receipt_Detail WHERE SysDocID='" + sysDocID + "' AND VoucherID='" + voucherID + "'";
				str += " AND CASE WHEN UnitQuantity IS NULL THEN Quantity ELSE UnitQuantity END - ISNULL(QuantityShipped,0)=0";
				object obj = ExecuteScalar(str, sqlTransaction);
				if (obj == null || int.Parse(obj.ToString()) == 0)
				{
					return true;
				}
				str = "UPDATE Sales_Receipt SET IsDelivered = 1 WHERE SysDocID='" + sysDocID + "' AND VoucherID='" + voucherID + "'";
				return ExecuteNonQuery(str, sqlTransaction) > 0;
			}
			catch
			{
				return false;
			}
		}

		internal bool UpdateRowShippedQuantity(string sysDocID, string voucherID, int rowIndex, float quantity, SqlTransaction sqlTransaction)
		{
			DataSet dataSet = new DataSet();
			float result = 0f;
			float result2 = 0f;
			try
			{
				string textCommand = "SELECT Quantity,UnitQuantity,QuantityShipped FROM Sales_Receipt_Detail WHERE SysDocID='" + sysDocID + "' AND VoucherID='" + voucherID + "' AND RowIndex=" + rowIndex.ToString();
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
					float.TryParse(dataRow["QuantityShipped"].ToString(), out result2);
				}
				result2 += quantity;
				textCommand = "UPDATE Sales_Receipt_Detail SET QuantityShipped=" + result2.ToString() + " WHERE SysDocID='" + sysDocID + "' AND VoucherID='" + voucherID + "' AND RowIndex=" + rowIndex.ToString();
				return ExecuteNonQuery(textCommand, sqlTransaction) > 0;
			}
			catch
			{
				return false;
			}
		}

		public DataSet GetReceiptsForDelivery(string customerID)
		{
			try
			{
				DataSet dataSet = new DataSet();
				string text = "SELECT SO.SysDocID [Doc ID],SO.VoucherID [Number],TransactionDate AS [Date],SO.CustomerID + '-' + C.CustomerName AS [Customer] FROM Sales_Receipt SO\r\n                             INNER JOIN Customer C ON SO.CustomerID=C.CustomerID  WHERE ISNULL(IsDelivered,0)=0";
				if (customerID != "")
				{
					text = text + " AND SO.CustomerID='" + customerID + "'";
				}
				FillDataSet(dataSet, "Sales_Receipt", text);
				return dataSet;
			}
			catch
			{
				throw;
			}
		}

		public DataSet GetList(DateTime from, DateTime to)
		{
			DataSet dataSet = new DataSet();
			string text = StoreConfiguration.ToSqlDateTimeString(from);
			string text2 = StoreConfiguration.ToSqlDateTimeString(to);
			string text3 = "SELECT     SysDocID [Doc ID],VoucherID [Doc Number],INV.CustomerID [Customer Code],CustomerName [Customer Name],TransactionDate [Invoice Date],\r\n                            INV.SalespersonID [Salesperson],Total [Amount],Reference, Reference2\r\n                            FROM         Sales_Invoice INV\r\n                            Inner JOIN Customer ON CUSTOMER.CustomerID=INV.CustomerID";
			if (from != DateTime.MinValue)
			{
				text3 = text3 + " WHERE TransactionDate Between '" + text + "' AND '" + text2 + "'";
			}
			SqlCommand sqlCommand = new SqlCommand(text3);
			FillDataSet(dataSet, "Sales_Receipt", sqlCommand);
			return dataSet;
		}

		public decimal GetProductSalesPrice(string productID, string customerID, string locationID, string UnitID)
		{
			string text = "SELECT ISNULL(PPD.UnitPrice1,P.UnitPrice1)AS UnitPrice1,ISNULL(PPD.UnitPrice2,P.UnitPrice2)AS UnitPrice2,ISNULL(PPD.UnitPrice3,P.UnitPrice3)AS \r\n                        UnitPrice3,ISNULL(PPD.MinPrice,P.MinPrice)AS MinPrice FROM Product_PriceList_Detail PPD  LEFT JOIN Product P ON \r\n                        PPD.ProductID=P.ProductID WHERE PPD.ProductID='" + productID + "'";
			if (locationID != "")
			{
				text = text + " AND PPD.LocationID = '" + locationID + "'";
			}
			if (UnitID != "")
			{
				object fieldValue = new Databases(base.DBConfig).GetFieldValue("Product_PriceList_Detail", "UnitID", "ProductID", productID, null);
				UnitID = ((fieldValue == null) ? "" : fieldValue.ToString());
			}
			if (UnitID != "")
			{
				text = text + " AND PPD.UnitID = '" + UnitID + "'";
			}
			DataSet dataSet = new DataSet();
			FillDataSet(dataSet, "PriceLevel", text);
			if (dataSet.Tables["PriceLevel"].Rows.Count == 0)
			{
				text = "SELECT UnitPrice1,UnitPrice2,UnitPrice3,MinPrice FROM Product WHERE ProductID='" + productID + "'";
				FillDataSet(dataSet, "PriceLevel", text);
			}
			string a = "";
			if (customerID != "")
			{
				a = new Databases(base.DBConfig).GetFieldValue("Customer", "PriceLevelID", "CustomerID", customerID, null).ToString();
			}
			if ((a == "" || a == "0") && dataSet.Tables[0].Rows[0]["UnitPrice1"] != DBNull.Value)
			{
				return decimal.Parse(dataSet.Tables[0].Rows[0]["UnitPrice1"].ToString());
			}
			if (a == "1" && dataSet.Tables[0].Rows[0]["UnitPrice2"] != DBNull.Value)
			{
				return decimal.Parse(dataSet.Tables[0].Rows[0]["UnitPrice2"].ToString());
			}
			if (a == "2" && dataSet.Tables[0].Rows[0]["UnitPrice3"] != DBNull.Value)
			{
				return decimal.Parse(dataSet.Tables[0].Rows[0]["UnitPrice3"].ToString());
			}
			if (a == "3" && dataSet.Tables[0].Rows[0]["MinPrice"] != DBNull.Value)
			{
				return decimal.Parse(dataSet.Tables[0].Rows[0]["MinPrice"].ToString());
			}
			return 0m;
		}
	}
}
