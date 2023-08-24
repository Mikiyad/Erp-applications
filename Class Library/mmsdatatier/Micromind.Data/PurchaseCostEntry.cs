using Micromind.Common;
using Micromind.Common.Data;
using Micromind.Common.Libraries;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace Micromind.Data
{
	public sealed class PurchaseCostEntry : StoreObject
	{
		private const string PURCHASECOSTENTRY_TABLE = "Purchase_Cost_Entry";

		private const string PURCHASECOSTENTRYDETAIL_TABLE = "Purchase_Cost_Entry_Detail";

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

		private const string DISCOUNT_PARM = "@Discount";

		private const string DISCOUNTFC_PARM = "@DiscountFC";

		private const string TAXAMOUNT_PARM = "@TaxAmount";

		private const string TAXAMOUNTFC_PARM = "@TaxAmountFC";

		private const string TOTAL_PARM = "@Total";

		private const string TOTALFC_PARM = "@TotalFC";

		private const string SOURCESYSDOCID_PARM = "@SourceSysDocID";

		private const string SOURCEVOUCHERID_PARM = "@SourceVoucherID";

		private const string CREATEDBY_PARM = "@CreatedBy";

		private const string DATECREATED_PARM = "@DateCreated";

		private const string UPDATEDBY_PARM = "@UpdatedBy";

		private const string DATEUPDATED_PARM = "@DateUpdated";

		private const string EXPENSEID_PARM = "@ExpenseID";

		private const string SUPPLIERID_PARM = "@SupplierID";

		private const string DUEDATE_PARM = "@DueDate";

		private const string RATETYPE_PARM = "@RateType";

		private const string AMOUNT_PARM = "@Amount";

		private const string AMOUNTFC_PARM = "@AmountFC";

		private const string DESCRIPTION_PARM = "@Description";

		private const string CURRENCYID_PARM = "@CurrencyID";

		private const string CURRENCYRATE_PARM = "@CurrencyRate";

		private const string COST_PARAM = "@Cost";

		private const string QUANTITY_PARM = "@Quantity";

		private const string REMARKS_PARM = "@Remarks";

		private const string TAXGROUPID_PARM = "@TaxGroupID";

		private const string TAXOPTION_PARM = "@TaxOption";

		private const string ROWINDEX_PARM = "@RowIndex";

		private int curDecPoints;

		public PurchaseCostEntry(Config config)
			: base(config)
		{
		}

		private string GetInsertUpdatePurchaseCostEntryText(bool isUpdate)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddInsertUpdateParameters("Purchase_Cost_Entry", new FieldValue("SysDocID", "@SysDocID", isUpdateConditionField: true), new FieldValue("VoucherID", "@VoucherID", isUpdateConditionField: true), new FieldValue("DivisionID", "@DivisionID"), new FieldValue("CompanyID", "@CompanyID"), new FieldValue("VendorID", "@VendorID", isUpdateConditionField: true), new FieldValue("PurchaseFlow", "@PurchaseFlow"), new FieldValue("TransactionDate", "@TransactionDate"), new FieldValue("ShippingMethodID", "@ShippingMethodID"), new FieldValue("Status", "@Status"), new FieldValue("Reference", "@Reference"), new FieldValue("ContainerNumber", "@ContainerNumber"), new FieldValue("Port", "@Port"), new FieldValue("LoadingPort", "@LoadingPort"), new FieldValue("ETA", "@ETA"), new FieldValue("ATD", "@ATD"), new FieldValue("BOLNumber", "@BOLNumber"), new FieldValue("Shipper", "@Shipper"), new FieldValue("ClearingAgent", "@ClearingAgent"), new FieldValue("Weight", "@Weight"), new FieldValue("Value", "@Value"), new FieldValue("TaxAmount", "@TaxAmount"), new FieldValue("TaxAmountFC", "@TaxAmountFC"), new FieldValue("Discount", "@Discount"), new FieldValue("DiscountFC", "@DiscountFC"), new FieldValue("Total", "@Total"), new FieldValue("TotalFC", "@TotalFC"), new FieldValue("TransporterID", "@TransporterID"), new FieldValue("ContainerSizeID", "@ContainerSizeID"), new FieldValue("SourceSysDocID", "@SourceSysDocID"), new FieldValue("SourceVoucherID", "@SourceVoucherID"), new FieldValue("TaxGroupID", "@TaxGroupID"), new FieldValue("Note", "@Note"));
			if (isUpdate)
			{
				sqlBuilder.AddInsertUpdateParameters("Purchase_Cost_Entry", new FieldValue("DateUpdated", "@DateUpdated", isUpdateConditionField: true, checkForNullValue: true));
			}
			if (isUpdate)
			{
				return sqlBuilder.GetUpdateExpression();
			}
			return sqlBuilder.GetInsertExpression();
		}

		private SqlCommand GetInsertUpdatePurchaseCostEntryCommand(bool isUpdate)
		{
			SqlParameterCollection parameters;
			if (isUpdate)
			{
				if (updateCommand != null)
				{
					updateCommand = null;
				}
				updateCommand = new SqlCommand(GetInsertUpdatePurchaseCostEntryText(isUpdate: true), base.DBConfig.Connection);
				updateCommand.CommandType = CommandType.Text;
				parameters = updateCommand.Parameters;
			}
			else
			{
				if (insertCommand != null)
				{
					insertCommand = null;
				}
				insertCommand = new SqlCommand(GetInsertUpdatePurchaseCostEntryText(isUpdate: false), base.DBConfig.Connection);
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
			parameters.Add("@Discount", SqlDbType.Decimal);
			parameters.Add("@DiscountFC", SqlDbType.Decimal);
			parameters.Add("@TaxAmount", SqlDbType.Decimal);
			parameters.Add("@TaxAmountFC", SqlDbType.Decimal);
			parameters.Add("@Total", SqlDbType.Decimal);
			parameters.Add("@TotalFC", SqlDbType.Decimal);
			parameters.Add("@SourceSysDocID", SqlDbType.NVarChar);
			parameters.Add("@SourceVoucherID", SqlDbType.NVarChar);
			parameters.Add("@Note", SqlDbType.NVarChar);
			parameters.Add("@TaxGroupID", SqlDbType.NVarChar);
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
			parameters["@TaxAmount"].SourceColumn = "TaxAmount";
			parameters["@TaxAmountFC"].SourceColumn = "TaxAmountFC";
			parameters["@Discount"].SourceColumn = "Discount";
			parameters["@DiscountFC"].SourceColumn = "DiscountFC";
			parameters["@Total"].SourceColumn = "Total";
			parameters["@TotalFC"].SourceColumn = "TotalFC";
			parameters["@SourceVoucherID"].SourceColumn = "SourceVoucherID";
			parameters["@SourceSysDocID"].SourceColumn = "SourceSysDocID";
			parameters["@TaxGroupID"].SourceColumn = "TaxGroupID";
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

		private string GetInsertUpdatePurchaseCostEntryDetailsText(bool isUpdate)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddInsertUpdateParameters("Purchase_Cost_Entry_Detail", new FieldValue("SysDocID", "@SysDocID", isUpdateConditionField: true), new FieldValue("VoucherID", "@VoucherID", isUpdateConditionField: true), new FieldValue("BOLNumber", "@BOLNumber"), new FieldValue("SupplierID", "@SupplierID"), new FieldValue("DueDate", "@DueDate"), new FieldValue("Cost", "@Cost"), new FieldValue("Quantity", "@Quantity"), new FieldValue("ExpenseID", "@ExpenseID"), new FieldValue("Description", "@Description"), new FieldValue("Amount", "@Amount"), new FieldValue("AmountFC", "@AmountFC"), new FieldValue("CurrencyID", "@CurrencyID"), new FieldValue("RateType", "@RateType"), new FieldValue("TaxOption", "@TaxOption"), new FieldValue("TaxAmount", "@TaxAmount"), new FieldValue("TaxGroupID", "@TaxGroupID"), new FieldValue("RowIndex", "@RowIndex"), new FieldValue("Remarks", "@Remarks"), new FieldValue("SourceSysDocID", "@SourceSysDocID"), new FieldValue("SourceVoucherID", "@SourceVoucherID"), new FieldValue("CurrencyRate", "@CurrencyRate"));
			if (isUpdate)
			{
				return sqlBuilder.GetUpdateExpression();
			}
			return sqlBuilder.GetInsertExpression();
		}

		private SqlCommand GetInsertUpdatePurchaseCostEntryDetailsCommand(bool isUpdate)
		{
			SqlParameterCollection parameters;
			if (isUpdate)
			{
				if (updateCommand != null)
				{
					updateCommand = null;
				}
				updateCommand = new SqlCommand(GetInsertUpdatePurchaseCostEntryDetailsText(isUpdate: true), base.DBConfig.Connection);
				updateCommand.CommandType = CommandType.Text;
				parameters = updateCommand.Parameters;
			}
			else
			{
				if (insertCommand != null)
				{
					insertCommand = null;
				}
				insertCommand = new SqlCommand(GetInsertUpdatePurchaseCostEntryDetailsText(isUpdate: false), base.DBConfig.Connection);
				insertCommand.CommandType = CommandType.Text;
				parameters = insertCommand.Parameters;
			}
			parameters.Add("@SysDocID", SqlDbType.NVarChar);
			parameters.Add("@VoucherID", SqlDbType.NVarChar);
			parameters.Add("@BOLNumber", SqlDbType.NVarChar);
			parameters.Add("@SupplierID", SqlDbType.NVarChar);
			parameters.Add("@DueDate", SqlDbType.DateTime);
			parameters.Add("@Cost", SqlDbType.NVarChar);
			parameters.Add("@Quantity", SqlDbType.Decimal);
			parameters.Add("@ExpenseID", SqlDbType.NVarChar);
			parameters.Add("@Description", SqlDbType.NVarChar);
			parameters.Add("@Amount", SqlDbType.Money);
			parameters.Add("@AmountFC", SqlDbType.Money);
			parameters.Add("@CurrencyID", SqlDbType.NVarChar);
			parameters.Add("@RateType", SqlDbType.NVarChar);
			parameters.Add("@CurrencyRate", SqlDbType.Decimal);
			parameters.Add("@RowIndex", SqlDbType.TinyInt);
			parameters.Add("@SourceSysDocID", SqlDbType.NVarChar);
			parameters.Add("@SourceVoucherID", SqlDbType.NVarChar);
			parameters.Add("@Remarks", SqlDbType.NVarChar);
			parameters.Add("@TaxOption", SqlDbType.TinyInt);
			parameters.Add("@TaxGroupID", SqlDbType.VarChar);
			parameters.Add("@TaxAmount", SqlDbType.Decimal);
			parameters["@SysDocID"].SourceColumn = "SysDocID";
			parameters["@VoucherID"].SourceColumn = "VoucherID";
			parameters["@BOLNumber"].SourceColumn = "BOLNumber";
			parameters["@SupplierID"].SourceColumn = "SupplierID";
			parameters["@DueDate"].SourceColumn = "DueDate";
			parameters["@Cost"].SourceColumn = "Cost";
			parameters["@Quantity"].SourceColumn = "Quantity";
			parameters["@ExpenseID"].SourceColumn = "ExpenseID";
			parameters["@Description"].SourceColumn = "Description";
			parameters["@Amount"].SourceColumn = "Amount";
			parameters["@AmountFC"].SourceColumn = "AmountFC";
			parameters["@CurrencyID"].SourceColumn = "CurrencyID";
			parameters["@RateType"].SourceColumn = "RateType";
			parameters["@CurrencyRate"].SourceColumn = "CurrencyRate";
			parameters["@RowIndex"].SourceColumn = "RowIndex";
			parameters["@SourceVoucherID"].SourceColumn = "SourceVoucherID";
			parameters["@SourceSysDocID"].SourceColumn = "SourceSysDocID";
			parameters["@Remarks"].SourceColumn = "Remarks";
			parameters["@TaxOption"].SourceColumn = "TaxOption";
			parameters["@TaxAmount"].SourceColumn = "TaxAmount";
			parameters["@TaxGroupID"].SourceColumn = "TaxGroupID";
			if (isUpdate)
			{
				return updateCommand;
			}
			return insertCommand;
		}

		private bool ValidateData(PurchaseCostEntryData journalData)
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

		public bool InsertUpdatePurchaseCostEntry(PurchaseCostEntryData PurchaseCostEntryData, bool isUpdate)
		{
			bool flag = true;
			SqlCommand insertUpdatePurchaseCostEntryCommand = GetInsertUpdatePurchaseCostEntryCommand(isUpdate);
			try
			{
				int currencyDecimalPoints = new CompanyInformations(base.DBConfig).CurrencyDecimalPoints;
				DataRow dataRow = PurchaseCostEntryData.PurchaseCostEntryTable.Rows[0];
				SqlTransaction sqlTransaction = base.DBConfig.StartNewTransaction();
				string text = dataRow["VoucherID"].ToString();
				string sysDocID = dataRow["SysDocID"].ToString();
				if (isUpdate && !CanUpdate(sysDocID, text, sqlTransaction))
				{
					throw new CompanyException("Unable to update. Some of the items has been already ordered.", 1037);
				}
				if (!isUpdate && new SystemDocuments(base.DBConfig).ExistDocumentNumber("Purchase_Cost_Entry", "VoucherID", dataRow["SysDocID"].ToString(), text, sqlTransaction))
				{
					throw new CompanyException("Document number already exist.", 1046);
				}
				string baseCurrencyID = new Currencies(base.DBConfig).GetBaseCurrencyID();
				decimal result = 1m;
				foreach (DataRow row in PurchaseCostEntryData.PurchaseCostEntryDetailTable.Rows)
				{
					row["SysDocID"] = dataRow["SysDocID"];
					row["VoucherID"] = dataRow["VoucherID"];
					if (row["CurrencyID"] != DBNull.Value && baseCurrencyID != row["CurrencyID"].ToString())
					{
						decimal.TryParse(row["CurrencyRate"].ToString(), out result);
						new Currencies(base.DBConfig).GetCurrencyRateType(row["CurrencyID"].ToString());
					}
					string a = row["CurrencyID"].ToString();
					if (a != "" && a != baseCurrencyID)
					{
						decimal d = decimal.Parse(row["Amount"].ToString());
						row["AmountFC"] = row["Amount"];
						decimal result2 = 1m;
						decimal.TryParse(row["CurrencyRate"].ToString(), out result2);
						d = ((!(row["RateType"].ToString() == "M")) ? Math.Round(d / result2, currencyDecimalPoints) : Math.Round(d * result2, currencyDecimalPoints));
						row["Amount"] = d;
					}
					else
					{
						row["CurrencyRate"] = 1;
					}
				}
				decimal num = default(decimal);
				foreach (DataRow row2 in PurchaseCostEntryData.PurchaseCostEntryDetailTable.Rows)
				{
					decimal result3 = default(decimal);
					decimal result4 = default(decimal);
					decimal result5 = default(decimal);
					decimal.TryParse(row2["Quantity"].ToString(), out result4);
					decimal.TryParse(row2["Cost"].ToString(), out result5);
					decimal.TryParse(row2["Amount"].ToString(), out result3);
					num += result3;
				}
				dataRow["Total"] = num;
				insertUpdatePurchaseCostEntryCommand.Transaction = sqlTransaction;
				flag = (isUpdate ? (flag & Update(PurchaseCostEntryData, "Purchase_Cost_Entry", insertUpdatePurchaseCostEntryCommand)) : (flag & Insert(PurchaseCostEntryData, "Purchase_Cost_Entry", insertUpdatePurchaseCostEntryCommand)));
				insertUpdatePurchaseCostEntryCommand = GetInsertUpdatePurchaseCostEntryDetailsCommand(isUpdate: false);
				insertUpdatePurchaseCostEntryCommand.Transaction = sqlTransaction;
				if (isUpdate)
				{
					flag &= DeletePurchaseCostEntryDetailsRows(sysDocID, text, sqlTransaction);
				}
				if (PurchaseCostEntryData.Tables["Purchase_Cost_Entry_Detail"].Rows.Count > 0)
				{
					flag &= Insert(PurchaseCostEntryData, "Purchase_Cost_Entry_Detail", insertUpdatePurchaseCostEntryCommand);
				}
				if (PurchaseCostEntryData.Tables.Contains("Tax_Detail"))
				{
					flag &= new TaxTransaction(base.DBConfig).InsertUpdateTaxTransaction(PurchaseCostEntryData, sysDocID, text, isUpdate, sqlTransaction);
				}
				GLData journalData = CreateInvoiceGLData(PurchaseCostEntryData, sqlTransaction);
				flag &= new Journal(base.DBConfig).InsertUpdateJournal(journalData, isUpdate, sqlTransaction);
				if (!flag)
				{
					throw new Exception("Failed to save the transaction because of an unexpected error. Please try again.");
				}
				flag &= UpdateTableRowInsertUpdateInfo("Purchase_Cost_Entry", "SysDocID", dataRow["SysDocID"].ToString(), "VoucherID", dataRow["VoucherID"].ToString(), sqlTransaction, !isUpdate);
				string entityName = "Purchase Cost Entry";
				flag = (isUpdate ? (flag & AddActivityLog(entityName, text, sysDocID, ActivityTypes.Update, sqlTransaction)) : (flag & AddActivityLog(entityName, text, sysDocID, ActivityTypes.Add, sqlTransaction)));
				if (!isUpdate)
				{
					flag &= new SystemDocuments(base.DBConfig).UpdateNextDocumentNumber(dataRow["SysDocID"].ToString(), dataRow["VoucherID"].ToString(), "Purchase_Cost_Entry", "VoucherID", sqlTransaction);
				}
				if (!flag)
				{
					return flag;
				}
				new Approval(base.DBConfig).CreateTransactionApprovalTasks(SysDocTypes.PurchaseQuote, sysDocID, text, "Purchase_Cost_Entry", sqlTransaction);
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

		private GLData CreateInvoiceGLData(PurchaseCostEntryData transactionData, SqlTransaction sqlTransaction)
		{
			try
			{
				GLData gLData = new GLData();
				DataRow dataRow = transactionData.PurchaseCostEntryTable.Rows[0];
				_ = new CompanyInformations(base.DBConfig).CurrencyDecimalPoints;
				string vendorID = dataRow["VendorID"].ToString();
				string text = dataRow["SysDocID"].ToString();
				string value = dataRow["CompanyID"].ToString();
				string value2 = dataRow["DivisionID"].ToString();
				string text2 = "";
				int num = 0;
				foreach (DataRow row in transactionData.PurchaseCostEntryDetailTable.Rows)
				{
					string text3 = "";
					num++;
					text3 = row["SupplierID"].ToString();
					text2 = text2 + "'" + text3 + "'";
					if (num < transactionData.PurchaseCostEntryDetailTable.Rows.Count)
					{
						text2 += ",";
					}
				}
				string textCommand = "SELECT VEN.VendorID,SD.LocationID,ISNULL(VEN.APAccountID,ISNULL(CLS.APAccountID, LOC.APAccountID)) AS APAccountID ,LOC.COGSAccountID,LOC.DiscountReceivedAccountID,\r\n                                LOC.InventoryAccountID,LOC.SalesAccountID,LOC.SalesTaxAccountID,Cur.CurrencyID AS BaseCurrencyID\r\n                                FROM System_Document SD INNER JOIN Location LOC ON SD.LocationID = LOC.LocationID\r\n                                LEFT OUTER JOIN Vendor VEN ON VendorID IN (" + text2 + ")\r\n                                 LEFT OUTER JOIN Vendor_Class CLS ON VEN.VendorClassID = CLS.ClassID\r\n                                LEFT OUTER JOIN Currency CUR ON CUR.IsBase = 'True'\r\n                                WHERE SysDocID = '" + text + "'";
				DataSet dataSet = new DataSet();
				FillDataSet(dataSet, "Accounts", textCommand, sqlTransaction);
				if (dataSet == null || dataSet.Tables.Count == 0 || dataSet.Tables[0].Rows.Count == 0)
				{
					throw new CompanyException("There is no location assigned to this system document or location record is missing.");
				}
				DataRow dataRow2 = dataSet.Tables["Accounts"].Rows[0];
				dataRow2["LocationID"].ToString();
				string text4 = dataRow2["DiscountReceivedAccountID"].ToString();
				string value3 = dataRow2["SalesTaxAccountID"].ToString();
				string a = dataRow2["APAccountID"].ToString();
				string text5 = dataRow2["BaseCurrencyID"].ToString();
				bool flag = false;
				decimal result = 1m;
				if (dataRow["CurrencyID"] != DBNull.Value && text5 != dataRow["CurrencyID"].ToString())
				{
					flag = true;
					decimal.TryParse(dataRow["CurrencyRate"].ToString(), out result);
				}
				if (0 == 0 && a == "")
				{
					throw new CompanyException("Account payable is not selected for this vendor. Please select an account payable for the location or this vendor.", 5000);
				}
				DataRow dataRow3 = gLData.JournalTable.NewRow();
				SysDocTypes sysDocTypes = SysDocTypes.PurchaseCostEntry;
				dataRow3["JournalID"] = 0;
				dataRow3["JournalDate"] = dataRow["TransactionDate"];
				dataRow3["SysDocID"] = dataRow["SysDocID"];
				dataRow3["SysDocType"] = (byte)sysDocTypes;
				dataRow3["VoucherID"] = dataRow["VoucherID"];
				dataRow3["CurrencyID"] = dataRow["CurrencyID"];
				dataRow3["CurrencyRate"] = dataRow["CurrencyRate"];
				dataRow3["Reference"] = dataRow["Reference"];
				dataRow3["Narration"] = "Purchase Cost - ";
				dataRow3.EndEdit();
				gLData.JournalTable.Rows.Add(dataRow3);
				decimal num2 = default(decimal);
				decimal num3 = default(decimal);
				new Hashtable();
				new ArrayList();
				new Hashtable();
				new ArrayList();
				new Hashtable();
				new ArrayList();
				new Hashtable();
				new ArrayList();
				new Hashtable();
				new ArrayList();
				int num4 = 0;
				for (int i = 0; i < dataSet.Tables[0].Rows.Count; i++)
				{
					DataTable dataTable = dataSet.Tables[0];
					vendorID = dataTable.Rows[i]["VendorID"].ToString();
					transactionData.Tables[1].DefaultView.RowFilter = "SupplierID = '" + vendorID + "'";
					DataTable dataTable2 = transactionData.Tables[1].DefaultView.ToTable();
					DataRow dataRow5;
					foreach (DataRow row2 in dataTable2.Rows)
					{
						string expenseAccountID = new ExpenseCode(base.DBConfig).GetExpenseAccountID(row2["ExpenseID"].ToString(), sqlTransaction);
						num2 = default(decimal);
						num3 = default(decimal);
						if (row2["Amount"] != DBNull.Value)
						{
							decimal.TryParse(row2["Amount"].ToString(), out num2);
						}
						if (row2["AmountFC"] != DBNull.Value)
						{
							decimal.TryParse(row2["AmountFC"].ToString(), out num3);
						}
						dataRow5 = gLData.JournalDetailsTable.NewRow();
						dataRow5.BeginEdit();
						dataRow5["JournalID"] = 0;
						dataRow5["AccountID"] = expenseAccountID;
						dataRow5["PayeeID"] = vendorID;
						string a2 = (string)(dataRow5["CurrencyID"] = row2["CurrencyID"].ToString());
						dataRow5["CurrencyRate"] = row2["CurrencyRate"];
						if (a2 != text5)
						{
							dataRow5["DebitFC"] = num3;
							dataRow5["CreditFC"] = DBNull.Value;
							dataRow5["Debit"] = num2;
							dataRow5["Credit"] = DBNull.Value;
						}
						else
						{
							dataRow5["DebitFC"] = num3;
							dataRow5["CreditFC"] = DBNull.Value;
							dataRow5["Debit"] = num2;
							dataRow5["Credit"] = DBNull.Value;
						}
						dataRow5["Description"] = "cost entry- " + row2["Description"];
						dataRow5["Reference"] = row2["ExpenseID"].ToString();
						dataRow5["RowIndex"] = num4;
						dataRow5["CompanyID"] = value;
						dataRow5["DivisionID"] = value2;
						num4++;
						dataRow5.EndEdit();
						gLData.JournalDetailsTable.Rows.Add(dataRow5);
					}
					object obj2 = dataTable2.Compute("Sum(Amount)", "");
					object obj3 = dataTable2.Compute("Sum(AmountFC)", "");
					num2 = decimal.Parse(obj2.ToString());
					num3 = decimal.Parse(obj3.ToString());
					List<string> list = (from table in dataTable2.AsEnumerable()
						where table.Field<string>("SupplierID") == vendorID
						select table.Field<string>("Description")).ToList();
					string str = string.Join(",", list.ToArray());
					dataRow5 = gLData.JournalDetailsTable.NewRow();
					dataRow5.BeginEdit();
					dataRow5["JournalID"] = 0;
					dataRow5["AccountID"] = dataTable.Rows[i]["APAccountID"].ToString();
					dataRow5["PayeeID"] = vendorID;
					dataRow5["PayeeType"] = "V";
					dataRow5["IsARAP"] = true;
					if (flag)
					{
						dataRow5["CreditFC"] = num3;
						dataRow5["DebitFC"] = DBNull.Value;
					}
					else
					{
						dataRow5["Credit"] = num2;
						dataRow5["Debit"] = DBNull.Value;
					}
					dataRow5["Description"] = "cost entry- " + str;
					dataRow5["Reference"] = dataRow["Reference"];
					dataRow5["RowIndex"] = num4;
					dataRow5["CompanyID"] = value;
					dataRow5["DivisionID"] = value2;
					num4++;
					dataRow5.EndEdit();
					gLData.JournalDetailsTable.Rows.Add(dataRow5);
				}
				decimal num5 = default(decimal);
				decimal num6 = default(decimal);
				if (num5 > 0m)
				{
					DataRow dataRow5 = gLData.JournalDetailsTable.NewRow();
					dataRow5.BeginEdit();
					dataRow5["JournalID"] = 0;
					if (text4 == "")
					{
						throw new CompanyException("You have entered a discount amount for this transaction but there is no discount account selected for this location.\nPlease select the 'Discount Received' account for this location.", 1040);
					}
					dataRow5["AccountID"] = text4;
					dataRow5["PayeeID"] = vendorID;
					dataRow5["PayeeType"] = "A";
					if (flag)
					{
						dataRow5["DebitFC"] = DBNull.Value;
						dataRow5["CreditFC"] = num5;
					}
					else
					{
						dataRow5["Debit"] = DBNull.Value;
						dataRow5["Credit"] = num5;
					}
					dataRow5["Reference"] = dataRow["Reference"];
					dataRow5["CompanyID"] = value;
					dataRow5["DivisionID"] = value2;
					dataRow5.EndEdit();
					gLData.JournalDetailsTable.Rows.Add(dataRow5);
				}
				if (num6 > 0m)
				{
					DataRow dataRow5 = gLData.JournalDetailsTable.NewRow();
					dataRow5.BeginEdit();
					dataRow5["JournalID"] = 0;
					dataRow5["AccountID"] = value3;
					dataRow5["PayeeID"] = vendorID;
					dataRow5["PayeeType"] = "A";
					dataRow5["Debit"] = num6;
					dataRow5["Credit"] = DBNull.Value;
					if (flag)
					{
						dataRow5["DebitFC"] = num6;
						dataRow5["CreditFC"] = DBNull.Value;
					}
					else
					{
						dataRow5["Debit"] = num6;
						dataRow5["Credit"] = DBNull.Value;
					}
					dataRow5["Reference"] = dataRow["Reference"];
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

		public DataSet GetPOPaymentSummary(string sysDocID, string voucherID, string RequestsysDocID, string RequestvoucherID)
		{
			try
			{
				DataSet dataSet = new DataSet();
				string textCommand = "SELECT PO.CurrencyID, PO.TermID,PO.Total,PO.ETA, ISNULL((SELECT SUM(ISNULL(AmountFC,Amount)) FROM Payment_Request PR WHERE POSysDocID = PO.SysDocID AND POVoucherID = PO.VoucherID AND PR.VoucherID <" + RequestvoucherID + " ),0) AS PaidAmount                                           \r\n                                FROM Purchase_Order PO  WHERE VoucherID='" + voucherID + "' AND SysDocID='" + sysDocID + "'";
				FillDataSet(dataSet, "Purchase_Cost_Entry", textCommand);
				return dataSet;
			}
			catch
			{
				throw;
			}
		}

		public PurchaseCostEntryData GetPurchaseCostEntryByID(string sysDocID, string voucherID)
		{
			try
			{
				PurchaseCostEntryData purchaseCostEntryData = new PurchaseCostEntryData();
				string textCommand = "select * from Purchase_Cost_Entry WHERE VoucherID='" + voucherID + "' AND SysDocID='" + sysDocID + "'";
				FillDataSet(purchaseCostEntryData, "Purchase_Cost_Entry", textCommand);
				if (purchaseCostEntryData == null || purchaseCostEntryData.Tables.Count == 0 || purchaseCostEntryData.Tables["Purchase_Cost_Entry"].Rows.Count == 0)
				{
					return null;
				}
				textCommand = "select * from Purchase_Cost_Entry_Detail PCD WHERE VoucherID='" + voucherID + "' AND SysDocID='" + sysDocID + "' ORDER BY PCD.RowIndex";
				FillDataSet(purchaseCostEntryData, "Purchase_Cost_Entry_Detail", textCommand);
				textCommand = "SELECT * FROM   Tax_Detail\r\n\t\t\t\t\t\tWHERE VoucherID='" + voucherID + "' AND SysDocID='" + sysDocID + "'";
				FillDataSet(purchaseCostEntryData, "Tax_Detail", textCommand);
				return purchaseCostEntryData;
			}
			catch
			{
				throw;
			}
		}

		public DataSet GetPurchaseCostEntryDetailReport(DateTime from, DateTime to, string jobID, string vendorID, string fromItem, string toItem, string fromClass, string toClass, string fromCategory, string toCategory)
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
				FillDataSet(dataSet, "Purchase_Cost_Entry", text3);
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
				FillDataSet(dataSet, "Purchase_Cost_Entry", textCommand);
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
				FillDataSet(dataSet, "Purchase_Cost_Entry_Detail", textCommand);
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
				FillDataSet(dataSet, "Purchase_Cost_Entry", str);
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
				FillDataSet(dataSet, "Purchase_Cost_Entry", text);
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
				FillDataSet(dataSet, "Purchase_Cost_Entry", text);
				return dataSet;
			}
			catch
			{
				throw;
			}
		}

		public DataSet GetPurchaseCostEntryAll()
		{
			try
			{
				DataSet dataSet = new DataSet();
				string textCommand = "SELECT SO.SysDocID [Doc ID],SO.VoucherID [Number],TransactionDate AS [Date],SO.VendorID + '-' + C.VendorName AS [Vendor],Reference,Total AS Amount  FROM Purchase_Order SO\r\n                             INNER JOIN Vendor C ON SO.VendorID=C.VendorID  WHERE ISNULL(IsVoid,'False')='False' AND Status=1";
				FillDataSet(dataSet, "Purchase_Cost_Entry", textCommand);
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

		internal bool DeletePurchaseCostEntryDetailsRows(string sysDocID, string voucherID, SqlTransaction sqlTransaction)
		{
			bool flag = true;
			try
			{
				string commandText = "DELETE FROM Purchase_Cost_Entry_Detail WHERE SysDocID = '" + sysDocID + "' AND VoucherID = '" + voucherID + "'";
				return Delete(commandText, sqlTransaction);
			}
			catch
			{
				throw;
			}
		}

		public bool VoidPurchaseCostEntry(string sysDocID, string voucherID, bool isVoid)
		{
			bool flag = true;
			try
			{
				SqlTransaction sqlTransaction = base.DBConfig.StartNewTransaction();
				if (!CanUpdate(sysDocID, voucherID, sqlTransaction))
				{
					throw new CompanyException("Unable to update. Some of the items in this order has been already received.", 1037);
				}
				PurchaseCostEntryData purchaseCostEntryData = new PurchaseCostEntryData();
				string textCommand = "SELECT * FROM Purchase_Order_Detail\r\n                              WHERE SysDocID = '" + sysDocID + "' AND VoucherID = '" + voucherID + "'";
				FillDataSet(purchaseCostEntryData, "Purchase_Order_Detail", textCommand, sqlTransaction);
				foreach (DataRow row in purchaseCostEntryData.PurchaseCostEntryDetailTable.Rows)
				{
					_ = row;
				}
				textCommand = "UPDATE Purchase_Cost_Entry SET IsVoid = '" + isVoid.ToString() + "' WHERE SysDocID='" + sysDocID + "' AND VoucherID = '" + voucherID + "'";
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
				AddActivityLog("Purchase Order", voucherID, sysDocID, activityType, sqlTransaction);
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

		public bool DeletePurchaseCostEntry(string sysDocID, string voucherID)
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
				flag &= DeletePurchaseCostEntryDetailsRows(sysDocID, voucherID, sqlTransaction);
				text = "DELETE FROM Purchase_Cost_Entry WHERE SysDocID='" + sysDocID + "' AND VoucherID = '" + voucherID + "'";
				flag &= (ExecuteNonQuery(text, sqlTransaction) >= 0);
				flag &= new Journal(base.DBConfig).DeleteJournal(sysDocID, voucherID, sqlTransaction);
				if (!flag)
				{
					return flag;
				}
				ActivityTypes activityType = ActivityTypes.Delete;
				AddActivityLog("Purchase Cost Entry", voucherID, sysDocID, activityType, sqlTransaction);
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

		public DataSet GetOpenOrderListReport()
		{
			DataSet dataSet = new DataSet();
			string textCommand = "SELECT SysDocID,VoucherID,SO.VendorID,VendorName,TransactionDate,SO.BuyerID,Total\r\n                            FROM Purchase_Order SO INNER JOIN Vendor ON SO.VendorID=Vendor.VendorID\r\n                            WHERE ISNULL(IsVoid,'False')='False' AND Status=1";
			FillDataSet(dataSet, "Orders", textCommand);
			return dataSet;
		}

		public DataSet GetPurchaseCostEntryToPrint(string sysDocID, string[] voucherID)
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
				string cmdText = "select BOLNumber from Purchase_Cost_Entry WHERE SysDocID = '" + sysDocID + "' AND VoucherID IN (" + text + ")";
				SqlCommand sqlCommand = new SqlCommand(cmdText);
				FillDataSet(dataSet, "BOLList", sqlCommand);
				string text2 = "";
				if (dataSet.Tables["BOLList"].Rows.Count > 0)
				{
					text2 = dataSet.Tables["BOLList"].Rows[0]["BOLNumber"].ToString();
				}
				cmdText = "SELECT DISTINCT SI.*,V.VendorName,B.FullName,B.Phone1 AS BPhone,B.Mobile AS BMobile,\r\n                                (SELECT COUNT(PL.BOLNumber) FROM PO_Shipment PL WHERE PL.BOLNumber=SI.BOLNumber AND ISNULL(PL.BOLNumber,'') <>'') AS [BOL_PL],\r\n                                (SELECT COUNT(PL.ContainerNumber) FROM PO_Shipment PL WHERE PL.BOLNumber=SI.BOLNumber) AS [BOL_Con],\r\n                                (SELECT SUM(PLD.Quantity) FROM PO_Shipment_Detail PLD INNER JOIN PO_Shipment PL ON PLD.SysDocID=PL.SysDocID \r\n                                AND PLD.VoucherID=PL.VoucherID WHERE PL.BOLNumber=SI.BOLNumber AND ISNULL(PL.BOLNumber,'') <>'')AS [No. Box],\r\n                                (SELECT SUBSTRING((SELECT DISTINCT ',' + PC.CategoryName \r\n                                FROM PO_Shipment_Detail PLD INNER JOIN \r\n                                PO_Shipment PL ON PLD.SysDocID=PL.SysDocID AND PLD.VoucherID=PL.VoucherID\r\n                                INNER JOIN Product P ON P.ProductID=PLD.ProductID\r\n                                LEFT OUTER JOIN Product_Category PC ON P.CategoryID=PC.CategoryID\r\n                                WHERE PL.BOLNumber= SI.BOLNumber FOR XML PATH('')),2,20000) )AS  Category,\r\n                                (SELECT SUBSTRING((SELECT DISTINCT ',' + C.CountryName \r\n                                FROM PO_Shipment_Detail PLD INNER JOIN \r\n                                PO_Shipment PL ON PLD.SysDocID=PL.SysDocID AND PLD.VoucherID=PL.VoucherID\r\n                                INNER JOIN Product P ON P.ProductID=PLD.ProductID\r\n                                LEFT OUTER JOIN Country C ON P.Origin=C.CountryID\r\n                                WHERE PL.BOLNumber= SI.BOLNumber FOR XML PATH('')),2,20000) )AS Orgin,\r\n                                (SELECT  TOP 1  PL.VendorID +' - '+V.VendorName FROM  PO_Shipment PL INNER JOIN Vendor V ON \r\n                                PL.VendorID=V.VendorID WHERE PL.BOLNumber= SI.BOLNumber AND ISNULL(PL.BOLNumber,'') <>'')AS [Supplier],\r\n                                (SELECT TOP 1 J.JobName FROM Job J LEFT JOIN Purchase_Order_NonInv_Detail PRD ON J.JobID=PRD.JobID LEFT JOIN Purchase_Order_NonInv PR ON PRD.SysDocID=PR.SysDocID and PRD.VoucherID=PR.VoucherID WHERE PR.BOLNo ='" + text2 + "' ) AS Project,\r\n                              (SELECT TOP 1 J.SiteLocationAddress FROM Job J LEFT JOIN Purchase_Order_NonInv_Detail PRD ON J.JobID=PRD.JobID LEFT JOIN Purchase_Order_NonInv PR ON PRD.SysDocID=PR.SysDocID and PRD.VoucherID=PR.VoucherID WHERE PR.BOLNo ='" + text2 + "') AS [Site Address],\r\n                              VA.AddressPrintFormat AS VendorAddress,VA.Phone1,VA.Fax,VA.Mobile,VA.ContactName,PT.TermName,SM.ShippingMethodName,\r\n                                ISNULL(SI.CurrencyID,(SELECT CurrencyID FROM Currency WHERE IsBase='True')) AS CurrencyID,\r\n                                TermName,ISNULL(SI.Total,0) - ISNULL(SI.Discount,0) AS GrandTotal,\r\n                                ISNULL(SI.TaxAmount ,0) AS Tax,SI.Total AS Total,\r\n                                (SELECT TOP 1 J.JobID FROM Job J LEFT JOIN Purchase_Order_NonInv_Detail PRD ON J.JobID=PRD.JobID \r\n                                LEFT JOIN Purchase_Order_NonInv PR ON PRD.SysDocID=PR.SysDocID and PRD.VoucherID=PR.VoucherID WHERE PR.BOLNo ='" + text2 + "') AS JobID\r\n                              FROM  Purchase_Cost_Entry SI INNER JOIN Vendor V ON SI.VendorID=V.VendorID\r\n\t\t\t\t\t\t\t  LEFT JOIN Purchase_Order_NonInv PR ON SI.BOLNumber=pr.BOLNo \r\n                                LEFT OUTER JOIN Payment_Term PT ON PR.TermID=PT.PaymentTermID\r\n                                LEFT OUTER JOIN Vendor_Address VA ON VA.VendorID=SI.VendorID AND VA.AddressID='PRIMARY'\r\n                                LEFT OUTER JOIN Shipping_Method SM ON SM.ShippingMethodID=SI.ShippingMethodID\r\n                                LEFT OUTER JOIN Buyer B ON B.BuyerID=SI.BuyerID\r\n                             \r\n                              WHERE SI.SysDocID = '" + sysDocID + "' AND SI.VoucherID IN (" + text + ")";
				FillDataSet(dataSet, "Purchase_Cost_Entry", cmdText);
				if (dataSet == null || dataSet.Tables.Count == 0 || dataSet.Tables["Purchase_Cost_Entry"].Rows.Count == 0)
				{
					return null;
				}
				cmdText = "SELECT Distinct PCD.SysDocID, PCD.VoucherID,PCD. BOLNumber ,PCD.ExpenseID,PCD.Amount, PCD.Quantity, PCD.DueDate,PCD.Cost,VendorName, PCD.Description, PCD.RowIndex,PCD.SourceSysDocID, PCD.SourceVoucherID, PCD.SupplierID  from Purchase_Cost_Entry_Detail PCD\r\n                        LEFT JOIN Vendor V ON  PCD.SupplierID=V.VendorID\r\n                        LEFT OUTER JOIN PO_Shipment PS ON PCD.BOLNumber=PS.BOLNumber\r\n                        LEFT OUTER JOIN Purchase_Order_NonInv POD ON PCD.BOLNumber=POD.BOLNo\r\n                        WHERE PCD.SysDocID='" + sysDocID + "' AND PCD.VoucherID IN (" + text + ")  ORDER BY RowIndex";
				FillDataSet(dataSet, "Purchase_Cost_Entry_Detail", cmdText);
				cmdText = "SELECT  DISTINCT  PL.*,V.VendorName,VA.AddressPrintFormat AS VendorAddress,ShippingMethodName, PLD.SourceVoucherID, PLD.SourceSysDocID\r\n                        FROM  PO_Shipment PL \r\n\t\t\t\t\t\tINNER JOIN PO_Shipment_Detail PLD ON PL.SysDocID=PLD.SysDocID AND PL.VoucherID=PLD.VoucherID\r\n\t\t\t\t\t\tINNER JOIN Vendor V ON PL.VendorID=V.VendorID   \r\n                        LEFT OUTER JOIN Vendor_Address VA ON VA.VendorID=PL.VendorID AND VA.AddressID='PRIMARY'     \r\n                        LEFT OUTER JOIN Shipping_Method SM ON SM.ShippingMethodID=PL.ShippingMethodID   \r\n                        WHERE PL.BOLNumber='" + text2 + "' AND ISNULL(PL.BOLNumber,'') <>''";
				FillDataSet(dataSet, "PO_Shipment", cmdText);
				dataSet.Relations.Add("PurchaseCostEntry", new DataColumn[2]
				{
					dataSet.Tables["Purchase_Cost_Entry"].Columns["SysDocID"],
					dataSet.Tables["Purchase_Cost_Entry"].Columns["VoucherID"]
				}, new DataColumn[2]
				{
					dataSet.Tables["Purchase_Cost_Entry_Detail"].Columns["SysDocID"],
					dataSet.Tables["Purchase_Cost_Entry_Detail"].Columns["VoucherID"]
				}, createConstraints: false);
				dataSet.Relations.Add("PackingList_Rel", new DataColumn[1]
				{
					dataSet.Tables["PO_Shipment"].Columns["BOLNumber"]
				}, new DataColumn[1]
				{
					dataSet.Tables["Purchase_Cost_Entry"].Columns["BOLNumber"]
				}, createConstraints: false);
				dataSet.Tables["Purchase_Cost_Entry"].Columns.Add("TotalInWords", typeof(string));
				foreach (DataRow row in dataSet.Tables["Purchase_Cost_Entry"].Rows)
				{
					decimal result = default(decimal);
					decimal result2 = default(decimal);
					decimal.TryParse(row["Total"].ToString(), out result);
					decimal.TryParse(row["Discount"].ToString(), out result2);
					row["TotalInWords"] = NumToWord.GetNumInWords(result - result2);
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
			string text3 = "SELECT   DISTINCT INV.SysDocID [Doc ID],INV.VoucherID [Doc Number],INV.BOLNumber, INV.ContainerNumber,Sum(INVD.Cost ) As Total,INV.TransactionDate [Date]\r\n                           \r\n                            FROM  Purchase_Cost_Entry INV LEFT JOIN Purchase_Cost_Entry_Detail INVD ON INV.SysDocID=INVD.SysDocID and INV.VoucherID=INVD.VoucherID ";
			if (from != DateTime.MinValue)
			{
				text3 = text3 + " where INV.TransactionDate Between '" + text + "' AND '" + text2 + " ' Group By INV.VoucherID, INV.SysDocID, INV.BOLNumber, INV.ContainerNumber, INV.TransactionDate";
			}
			SqlCommand sqlCommand = new SqlCommand(text3);
			FillDataSet(dataSet, "Purchase_Cost_Entry", sqlCommand);
			return dataSet;
		}

		public DataSet XGetPendingApprovalList(string approvalID)
		{
			DataSet dataSet = new DataSet();
			SqlCommand sqlCommand = new SqlCommand("SELECT     ISNULL(IsVoid,'False') AS V,SysDocID [Doc ID],VoucherID [Doc Number],INV.VendorID [Vendor Code],VendorName [Vendor Name],TransactionDate [Order Date],Status,\r\n                            CASE ISNULL(IsImport,'False') WHEN 'True' THEN 'Import' ELSE 'Local' END AS [Type],INV.BuyerID [Buyer],Reference,INV.CurrencyID Currency,Total [Amount]\r\n                            FROM         Purchase_Order INV\r\n                            INNER JOIN Vendor ON VENDOR.VendorID=INV.VendorID WHERE 1=1 ");
			FillDataSet(dataSet, "Purchase_Cost_Entry", sqlCommand);
			return dataSet;
		}

		public DataSet GetCostEntryList()
		{
			try
			{
				DataSet dataSet = new DataSet();
				string textCommand = "SELECT pce.SysDocID,pce.VoucherID,pce.BOLNumber from Purchase_Cost_Entry pce ";
				FillDataSet(dataSet, "Purchase_Cost_Entry", textCommand);
				return dataSet;
			}
			catch
			{
				throw;
			}
		}

		public PurchaseCostEntryData GetCostEntryByID(string sysDocID, string voucherID)
		{
			try
			{
				PurchaseCostEntryData purchaseCostEntryData = new PurchaseCostEntryData();
				string textCommand = "SELECT pce.SysDocID,pce.VoucherID,pce.BOLNumber from Purchase_Cost_Entry pce\r\n                                 WHERE pce.VoucherID='" + voucherID + "' AND pce.SysDocID='" + sysDocID + "'";
				FillDataSet(purchaseCostEntryData, "Purchase_Cost_Entry", textCommand);
				if (purchaseCostEntryData == null || purchaseCostEntryData.Tables.Count == 0 || purchaseCostEntryData.Tables["Purchase_Cost_Entry"].Rows.Count == 0)
				{
					return null;
				}
				textCommand = "SELECT TD.*  FROM Purchase_Cost_Entry_Detail TD  WHERE VoucherID='" + voucherID + "' AND SysDocID='" + sysDocID + "'";
				FillDataSet(purchaseCostEntryData, "Purchase_Cost_Entry_Detail", textCommand);
				return purchaseCostEntryData;
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
				FillDataSet(dataSet, "Purchase_Cost_Entry_Detail", str);
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
				FillDataSet(dataSet, "Purchase_Cost_Entry", str);
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
				string text = "SELECT DISTINCT pd.ExpenseID,pd.Description,pd.CurrencyID,pd.CurrencyRate,pd.RateType,EC.TaxOption,EC.TaxGroupID from Purchase_Cost_Entry_Detail pd inner join Purchase_Cost_Entry pe ON pe.SysDocID=pd.SysDocID AND  pe.VoucherID=pd.VoucherID\r\n                                LEFT JOIN Expense_Code EC ON pd.ExpenseID=EC.ExpenseID ";
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

		public bool IsInvoicedEntry(string sysDocID, string voucherNumber, SqlTransaction sqlTransaction)
		{
			string exp = "Select Count(*) FROM Purchase_Invoice_Expense\r\n                                WHERE PCSysDocID='" + sysDocID + "' AND PCVoucherID='" + voucherNumber + "'";
			object obj = ExecuteScalar(exp, sqlTransaction);
			if (obj != null && obj.ToString() != "" && int.Parse(obj.ToString()) > 0)
			{
				return true;
			}
			return false;
		}

		internal bool UpdateRowReceivedAmount(string sysDocID, string voucherID, int rowIndex, double quantity, bool isDelete, SqlTransaction sqlTransaction)
		{
			DataSet dataSet = new DataSet();
			double result = 0.0;
			double result2 = 0.0;
			bool flag = true;
			try
			{
				string textCommand = "SELECT Cost,AllocatedCost FROM Purchase_Cost_Entry_Detail WHERE SysDocID='" + sysDocID + "' AND VoucherID='" + voucherID + "' AND RowIndex=" + rowIndex.ToString();
				FillDataSet(dataSet, "CostEntry", textCommand);
				if (dataSet != null && dataSet.Tables[0].Rows.Count > 0)
				{
					DataRow dataRow = dataSet.Tables[0].Rows[0];
					if (dataRow["Cost"] != DBNull.Value)
					{
						double.TryParse(dataRow["Cost"].ToString(), out result);
					}
					double.TryParse(dataRow["AllocatedCost"].ToString(), out result2);
				}
				result2 = (isDelete ? (result2 - quantity) : (result2 + quantity));
				textCommand = "UPDATE Purchase_Cost_Entry_Detail SET AllocatedCost=" + double.Parse(result2.ToString()) + " WHERE SysDocID='" + sysDocID + "' AND VoucherID='" + voucherID + "' AND RowIndex=" + rowIndex.ToString();
				return flag & (ExecuteNonQuery(textCommand, sqlTransaction) > 0);
			}
			catch
			{
				return false;
			}
		}
	}
}
