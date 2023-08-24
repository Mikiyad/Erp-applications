using Micromind.Common;
using Micromind.Common.Data;
using Micromind.Common.Libraries;
using System;
using System.Data;
using System.Data.SqlClient;

namespace Micromind.Data
{
	public sealed class PurchasePrepaymentInvoice : StoreObject
	{
		private const string PURCHASEPREPAYMENTINVOICE_TABLE = "Purchase_PrePayment_Invoice";

		private const string SYSDOCID_PARM = "@SysDocID";

		private const string VOUCHERID_PARM = "@VoucherID";

		private const string SOURCESYSDOCID_PARM = "@SourceSysDocID";

		private const string SOURCEVOUCHERID_PARM = "@SourceVoucherID";

		private const string VENDORID_PARM = "@VendorID";

		private const string TRANSACTIONDATE_PARM = "@TransactionDate";

		private const string POAMOUNT_PARM = "@POAmount";

		private const string PAID_PARM = "@Paid";

		private const string BALANCE_PARM = "@Balance";

		private const string ISVOID_PARM = "@IsVoid";

		private const string CURRENCYID_PARM = "@CurrencyID";

		private const string CURRENCYRATE_PARM = "@CurrencyRate";

		public const string DUEDATE_PARM = "@DueDate";

		public const string PREPAYMENTTERMID_PARM = "@PrepaymentTermID";

		public const string REMARKS_PARM = "@Remarks";

		public const string STATUS_PARM = "@Status";

		private const string TERMID_PARM = "@TermID";

		private const string SUGGESTEDUE_PARM = "@SuggestedDue";

		private const string AMOUNT_PARM = "@Amount";

		private const string AMOUNTFC_PARM = "@AmountFC";

		private const string CREATEDBY_PARM = "@CreatedBy";

		private const string DATECREATED_PARM = "@DateCreated";

		private const string UPDATEDBY_PARM = "@UpdatedBy";

		private const string DATEUPDATED_PARM = "@DateUpdated";

		public PurchasePrepaymentInvoice(Config config)
			: base(config)
		{
		}

		private string GetInsertUpdatePurchasePrepaymentInvoiceText(bool isUpdate)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddInsertUpdateParameters("Purchase_PrePayment_Invoice", new FieldValue("SysDocID", "@SysDocID", isUpdateConditionField: true), new FieldValue("VoucherID", "@VoucherID", isUpdateConditionField: true), new FieldValue("VendorID", "@VendorID"), new FieldValue("SourceSysDocID", "@SourceSysDocID"), new FieldValue("SourceVoucherID", "@SourceVoucherID"), new FieldValue("TransactionDate", "@TransactionDate"), new FieldValue("POAmount", "@POAmount"), new FieldValue("Amount", "@Amount"), new FieldValue("AmountFC", "@AmountFC"), new FieldValue("Paid", "@Paid"), new FieldValue("Balance", "@Balance"), new FieldValue("TermID", "@TermID"), new FieldValue("PrepaymentTermID", "@PrepaymentTermID"), new FieldValue("Remarks", "@Remarks"), new FieldValue("DueDate", "@DueDate"), new FieldValue("SuggestedDue", "@SuggestedDue"), new FieldValue("CurrencyID", "@CurrencyID"), new FieldValue("CurrencyRate", "@CurrencyRate"), new FieldValue("IsVoid", "@IsVoid"));
			if (isUpdate)
			{
				sqlBuilder.AddInsertUpdateParameters("Purchase_PrePayment_Invoice", new FieldValue("DateUpdated", "@DateUpdated", isUpdateConditionField: true, checkForNullValue: true));
			}
			if (isUpdate)
			{
				return sqlBuilder.GetUpdateExpression();
			}
			return sqlBuilder.GetInsertExpression();
		}

		private SqlCommand GetInsertUpdatePurchasePrepaymentInvoiceCommand(bool isUpdate)
		{
			SqlParameterCollection parameters;
			if (isUpdate)
			{
				if (updateCommand != null)
				{
					updateCommand = null;
				}
				updateCommand = new SqlCommand(GetInsertUpdatePurchasePrepaymentInvoiceText(isUpdate: true), base.DBConfig.Connection);
				updateCommand.CommandType = CommandType.Text;
				parameters = updateCommand.Parameters;
			}
			else
			{
				if (insertCommand != null)
				{
					insertCommand = null;
				}
				insertCommand = new SqlCommand(GetInsertUpdatePurchasePrepaymentInvoiceText(isUpdate: false), base.DBConfig.Connection);
				insertCommand.CommandType = CommandType.Text;
				parameters = insertCommand.Parameters;
			}
			parameters.Add("@SysDocID", SqlDbType.NVarChar);
			parameters.Add("@VoucherID", SqlDbType.NVarChar);
			parameters.Add("@SourceSysDocID", SqlDbType.NVarChar);
			parameters.Add("@SourceVoucherID", SqlDbType.NVarChar);
			parameters.Add("@TransactionDate", SqlDbType.DateTime);
			parameters.Add("@POAmount", SqlDbType.Decimal);
			parameters.Add("@Paid", SqlDbType.Decimal);
			parameters.Add("@Amount", SqlDbType.Decimal);
			parameters.Add("@AmountFC", SqlDbType.Decimal);
			parameters.Add("@Balance", SqlDbType.Decimal);
			parameters.Add("@TermID", SqlDbType.NVarChar);
			parameters.Add("@SuggestedDue", SqlDbType.Decimal);
			parameters.Add("@IsVoid", SqlDbType.TinyInt);
			parameters.Add("@CurrencyID", SqlDbType.NVarChar);
			parameters.Add("@CurrencyRate", SqlDbType.Decimal);
			parameters.Add("@VendorID", SqlDbType.NVarChar);
			parameters.Add("@PrepaymentTermID", SqlDbType.NVarChar);
			parameters.Add("@DueDate", SqlDbType.DateTime);
			parameters.Add("@Remarks", SqlDbType.NVarChar);
			parameters["@SysDocID"].SourceColumn = "SysDocID";
			parameters["@VoucherID"].SourceColumn = "VoucherID";
			parameters["@SourceSysDocID"].SourceColumn = "SourceSysDocID";
			parameters["@SourceVoucherID"].SourceColumn = "SourceVoucherID";
			parameters["@TransactionDate"].SourceColumn = "TransactionDate";
			parameters["@POAmount"].SourceColumn = "POAmount";
			parameters["@Paid"].SourceColumn = "Paid";
			parameters["@Balance"].SourceColumn = "Balance";
			parameters["@TermID"].SourceColumn = "TermID";
			parameters["@SuggestedDue"].SourceColumn = "SuggestedDue";
			parameters["@IsVoid"].SourceColumn = "IsVoid";
			parameters["@CurrencyID"].SourceColumn = "CurrencyID";
			parameters["@CurrencyRate"].SourceColumn = "CurrencyRate";
			parameters["@Amount"].SourceColumn = "Amount";
			parameters["@AmountFC"].SourceColumn = "AmountFC";
			parameters["@VendorID"].SourceColumn = "VendorID";
			parameters["@PrepaymentTermID"].SourceColumn = "PrepaymentTermID";
			parameters["@DueDate"].SourceColumn = "DueDate";
			parameters["@Remarks"].SourceColumn = "Remarks";
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

		private bool ValidateData(PurchasePrepaymentInvoiceData journalData)
		{
			return true;
		}

		public bool VoidPurchasePrepaymentInvoice(string sysDocID, string voucherID, bool isVoid)
		{
			bool flag = true;
			try
			{
				SqlTransaction sqlTransaction = base.DBConfig.StartNewTransaction();
				if (CanUpdate(sysDocID, voucherID, sqlTransaction) == -1)
				{
					throw new CompanyException("This prepayment invoice is closed and cant be updated.", 1064);
				}
				string exp = "UPDATE Purchase_Prepayment_Invoice SET IsVoid = '" + isVoid.ToString() + "' WHERE SysDocID='" + sysDocID + "' AND VoucherID = '" + voucherID + "'";
				flag &= (ExecuteNonQuery(exp, sqlTransaction) > 0);
				flag &= new Journal(base.DBConfig).VoidJournal(sysDocID, voucherID, isVoid, sqlTransaction);
				if (!flag)
				{
					return flag;
				}
				ActivityTypes activityType = ActivityTypes.Void;
				if (!isVoid)
				{
					activityType = ActivityTypes.Unvoid;
				}
				AddActivityLog("Purchase Prepayment Invoice", voucherID, sysDocID, activityType, sqlTransaction);
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

		public bool DeletePurchasePrepaymentInvoice(string sysDocID, string voucherID)
		{
			bool flag = true;
			try
			{
				string text = "";
				SqlTransaction sqlTransaction = base.DBConfig.StartNewTransaction();
				if (CanUpdate(sysDocID, voucherID, sqlTransaction) == -1)
				{
					throw new CompanyException("This prepayment invoice is closed and cant be updated.", 1064);
				}
				flag &= new Journal(base.DBConfig).DeleteJournal(sysDocID, voucherID, sqlTransaction);
				text = "DELETE FROM Purchase_Prepayment_Invoice WHERE SysDocID='" + sysDocID + "' AND VoucherID = '" + voucherID + "'";
				flag &= (ExecuteNonQuery(text, sqlTransaction) >= 0);
				if (!flag)
				{
					return flag;
				}
				ActivityTypes activityType = ActivityTypes.Delete;
				AddActivityLog("Purchase Prepayment Invoice", voucherID, sysDocID, activityType, sqlTransaction);
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

		private GLData CreateClosePrepaymentGLData(DataRow prepayRow, SqlTransaction sqlTransaction)
		{
			GLData gLData = new GLData();
			decimal num = default(decimal);
			string text = prepayRow["VendorID"].ToString();
			prepayRow["VoucherID"].ToString();
			string text2 = prepayRow["SysDocID"].ToString();
			num = ((!prepayRow["AmountFC"].IsDBNullOrEmpty()) ? decimal.Parse(prepayRow["AmountFC"].ToString()) : decimal.Parse(prepayRow["Amount"].ToString()));
			string textCommand = "SELECT SD.LocationID,Cur.CurrencyID AS BaseCurrencyID,LOC.PurchasePrePaymentAccountID,LOC.PrepaymentAPAccountID\r\n                                FROM System_Document SD INNER JOIN Location LOC ON SD.LocationID = LOC.LocationID\r\n                                LEFT OUTER JOIN Vendor VEN ON VendorID='" + text + "'\r\n                                 LEFT OUTER JOIN Vendor_Class CLS ON VEN.VendorClassID = CLS.ClassID\r\n                                LEFT OUTER JOIN Currency CUR ON CUR.IsBase = 'True'\r\n                                WHERE SysDocID = '" + text2 + "'";
			DataSet dataSet = new DataSet();
			FillDataSet(dataSet, "Accounts", textCommand, sqlTransaction);
			if (dataSet == null || dataSet.Tables.Count == 0 || dataSet.Tables[0].Rows.Count == 0)
			{
				throw new CompanyException("There is no location assigned to this system document or location record is missing.");
			}
			DataRow dataRow = dataSet.Tables["Accounts"].Rows[0];
			dataRow["LocationID"].ToString();
			string value = dataRow["PurchasePrePaymentAccountID"].ToString();
			string value2 = dataRow["PrepaymentAPAccountID"].ToString();
			SysDocTypes sysDocTypes = SysDocTypes.PurchasePrepaymentApplied;
			string a = dataRow["BaseCurrencyID"].ToString();
			bool flag = false;
			decimal result = 1m;
			if (prepayRow["CurrencyID"] != DBNull.Value && a != prepayRow["CurrencyID"].ToString())
			{
				flag = true;
				decimal.TryParse(prepayRow["CurrencyRate"].ToString(), out result);
			}
			DataRow dataRow2 = gLData.JournalTable.NewRow();
			dataRow2["JournalID"] = 0;
			dataRow2["JournalDate"] = DateTime.Now;
			dataRow2["SysDocID"] = "SYS_017";
			dataRow2["SysDocType"] = (byte)sysDocTypes;
			dataRow2["VoucherID"] = prepayRow["VoucherID"];
			dataRow2["CurrencyID"] = prepayRow["CurrencyID"];
			dataRow2["CurrencyRate"] = prepayRow["CurrencyRate"];
			dataRow2.EndEdit();
			gLData.JournalTable.Rows.Add(dataRow2);
			DataRow dataRow3 = gLData.JournalDetailsTable.NewRow();
			dataRow3.BeginEdit();
			dataRow3["JournalID"] = 0;
			dataRow3["PayeeType"] = "V";
			dataRow3["PayeeID"] = text;
			dataRow3["AccountID"] = value2;
			dataRow3["IsARAP"] = true;
			dataRow3["Credit"] = DBNull.Value;
			dataRow3["CreditFC"] = DBNull.Value;
			if (!flag)
			{
				dataRow3["Debit"] = prepayRow["Amount"];
			}
			else
			{
				dataRow3["DebitFC"] = prepayRow["AmountFC"];
			}
			dataRow3["RowIndex"] = -1;
			dataRow3.EndEdit();
			gLData.JournalDetailsTable.Rows.Add(dataRow3);
			dataRow3 = gLData.JournalDetailsTable.NewRow();
			dataRow3.BeginEdit();
			dataRow3["JournalID"] = 0;
			dataRow3["PayeeType"] = "V";
			dataRow3["PayeeID"] = text;
			dataRow3["AccountID"] = value;
			dataRow3["IsARAP"] = false;
			if (!flag)
			{
				dataRow3["Credit"] = num;
			}
			else
			{
				dataRow3["CreditFC"] = num;
			}
			dataRow3["Debit"] = DBNull.Value;
			dataRow3["DebitFC"] = DBNull.Value;
			dataRow3.EndEdit();
			gLData.JournalDetailsTable.Rows.Add(dataRow3);
			return gLData;
		}

		private int CanUpdate(string sysDocID, string voucherID, SqlTransaction sqlTransaction)
		{
			object fieldValue = new Databases(base.DBConfig).GetFieldValue("Purchase_PrePayment_Invoice", "Status", "SysDocID", sysDocID, "VoucherID", voucherID, sqlTransaction);
			byte b = 0;
			if (!fieldValue.IsNullOrEmpty())
			{
				b = byte.Parse(fieldValue.ToString());
			}
			if (b != 0)
			{
				return -1;
			}
			return 0;
		}

		public bool InsertUpdatePurchasePrepaymentInvoice(PurchasePrepaymentInvoiceData PurchasePrepaymentInvoiceData, bool isUpdate)
		{
			bool flag = true;
			SqlCommand insertUpdatePurchasePrepaymentInvoiceCommand = GetInsertUpdatePurchasePrepaymentInvoiceCommand(isUpdate);
			try
			{
				DataRow dataRow = PurchasePrepaymentInvoiceData.PurchasePrepaymentInvoiceTable.Rows[0];
				string text = "";
				_ = new CompanyInformations(base.DBConfig).CurrencyDecimalPoints;
				SqlTransaction sqlTransaction = base.DBConfig.StartNewTransaction();
				string text2 = dataRow["VoucherID"].ToString();
				string text3 = dataRow["SysDocID"].ToString();
				if (!isUpdate)
				{
					if (new SystemDocuments(base.DBConfig).ExistDocumentNumber("Purchase_PrePayment_Invoice", "VoucherID", dataRow["SysDocID"].ToString(), text2, sqlTransaction))
					{
						throw new CompanyException("Document number already exist.", 1046);
					}
					text = " select COUNT(*) FROM AP_Payment_Allocation APP WHERE APP.InvoiceSysDocID = '" + text3 + "' AND APP.InvoiceVoucherID = '" + text2 + "'";
					if (int.Parse(ExecuteScalar(text, sqlTransaction).ToString()) > 0)
					{
						throw new CompanyException("Prepayment invoice is already paid and can't be modified.");
					}
				}
				else if (CanUpdate(text3, text2, sqlTransaction) == -1)
				{
					throw new CompanyException("This prepayment invoice is closed and cant be updated.", 1064);
				}
				insertUpdatePurchasePrepaymentInvoiceCommand.Transaction = sqlTransaction;
				flag = (isUpdate ? (flag & Update(PurchasePrepaymentInvoiceData, "Purchase_PrePayment_Invoice", insertUpdatePurchasePrepaymentInvoiceCommand)) : (flag & Insert(PurchasePrepaymentInvoiceData, "Purchase_PrePayment_Invoice", insertUpdatePurchasePrepaymentInvoiceCommand)));
				GLData journalData = CreatePurchasePrepaymentInvoiceGLData(PurchasePrepaymentInvoiceData, sqlTransaction);
				flag &= new Journal(base.DBConfig).InsertUpdateJournal(journalData, isUpdate, sqlTransaction);
				if (!flag)
				{
					throw new Exception("Failed to save the transaction because of an unexpected error. Please try again.");
				}
				flag &= UpdateTableRowInsertUpdateInfo("Purchase_PrePayment_Invoice", "SysDocID", dataRow["SysDocID"].ToString(), "VoucherID", dataRow["VoucherID"].ToString(), sqlTransaction, !isUpdate);
				string entityName = "Purchase Prepayment Invoice";
				flag = (isUpdate ? (flag & AddActivityLog(entityName, text2, text3, ActivityTypes.Update, sqlTransaction)) : (flag & AddActivityLog(entityName, text2, text3, ActivityTypes.Add, sqlTransaction)));
				if (!isUpdate)
				{
					flag &= new SystemDocuments(base.DBConfig).UpdateNextDocumentNumber(dataRow["SysDocID"].ToString(), dataRow["VoucherID"].ToString(), "Purchase_PrePayment_Invoice", "VoucherID", sqlTransaction);
				}
				if (!flag)
				{
					return flag;
				}
				new Approval(base.DBConfig).CreateTransactionApprovalTasks(SysDocTypes.PurchasePrepaymentInvoice, text3, text2, "Purchase_PrePayment_Invoice", sqlTransaction);
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

		private GLData CreatePurchasePrepaymentInvoiceGLData(PurchasePrepaymentInvoiceData transactionData, SqlTransaction sqlTransaction)
		{
			GLData gLData = new GLData();
			DataRow dataRow = transactionData.PurchasePrepaymentInvoiceTable.Rows[0];
			string text = dataRow["VendorID"].ToString();
			dataRow["VoucherID"].ToString();
			string text2 = dataRow["SysDocID"].ToString();
			string textCommand = "SELECT SD.LocationID,ISNULL(VEN.APAccountID,ISNULL(CLS.APAccountID, LOC.APAccountID)) AS APAccountID ,LOC.COGSAccountID,LOC.DiscountReceivedAccountID,\r\n                                LOC.InventoryAccountID,LOC.SalesAccountID,LOC.SalesTaxAccountID,Cur.CurrencyID AS BaseCurrencyID,LOC.PurchasePrePaymentAccountID\r\n                                FROM System_Document SD INNER JOIN Location LOC ON SD.LocationID = LOC.LocationID\r\n                                LEFT OUTER JOIN Vendor VEN ON VendorID='" + text + "'\r\n                                 LEFT OUTER JOIN Vendor_Class CLS ON VEN.VendorClassID = CLS.ClassID\r\n                                LEFT OUTER JOIN Currency CUR ON CUR.IsBase = 'True'\r\n                                WHERE SysDocID = '" + text2 + "'";
			DataSet dataSet = new DataSet();
			FillDataSet(dataSet, "Accounts", textCommand, sqlTransaction);
			if (dataSet == null || dataSet.Tables.Count == 0 || dataSet.Tables[0].Rows.Count == 0)
			{
				throw new CompanyException("There is no location assigned to this system document or location record is missing.");
			}
			DataRow dataRow2 = dataSet.Tables["Accounts"].Rows[0];
			dataRow2["LocationID"].ToString();
			string value = dataRow2["PurchasePrePaymentAccountID"].ToString();
			SysDocTypes sysDocTypes = SysDocTypes.PurchasePrepaymentInvoice;
			string value2 = dataRow2["APAccountID"].ToString();
			string a = dataRow2["BaseCurrencyID"].ToString();
			bool flag = false;
			decimal result = 1m;
			if (dataRow["CurrencyID"] != DBNull.Value && a != dataRow["CurrencyID"].ToString())
			{
				flag = true;
				decimal.TryParse(dataRow["CurrencyRate"].ToString(), out result);
			}
			DataRow dataRow3 = gLData.JournalTable.NewRow();
			dataRow3["JournalID"] = 0;
			dataRow3["JournalDate"] = dataRow["TransactionDate"];
			dataRow3["SysDocID"] = dataRow["SysDocID"];
			dataRow3["SysDocType"] = (byte)sysDocTypes;
			dataRow3["VoucherID"] = dataRow["VoucherID"];
			dataRow3["CurrencyID"] = dataRow["CurrencyID"];
			dataRow3["CurrencyRate"] = dataRow["CurrencyRate"];
			dataRow3.EndEdit();
			gLData.JournalTable.Rows.Add(dataRow3);
			DataRow dataRow4 = gLData.JournalDetailsTable.NewRow();
			dataRow4.BeginEdit();
			dataRow4["JournalID"] = 0;
			dataRow4["PayeeType"] = "V";
			dataRow4["PayeeID"] = text;
			dataRow4["AccountID"] = value2;
			dataRow4["IsARAP"] = true;
			dataRow4["Debit"] = DBNull.Value;
			dataRow4["DebitFC"] = DBNull.Value;
			if (!flag)
			{
				dataRow4["Credit"] = dataRow["Amount"];
			}
			else
			{
				dataRow4["CreditFC"] = dataRow["AmountFC"];
			}
			dataRow4["RowIndex"] = -1;
			dataRow4.EndEdit();
			gLData.JournalDetailsTable.Rows.Add(dataRow4);
			foreach (DataRow row in transactionData.PurchasePrepaymentInvoiceTable.Rows)
			{
				dataRow4 = gLData.JournalDetailsTable.NewRow();
				dataRow4.BeginEdit();
				dataRow4["JournalID"] = 0;
				dataRow4["PayeeType"] = "V";
				dataRow4["PayeeID"] = row["VendorID"];
				dataRow4["AccountID"] = value;
				dataRow4["IsARAP"] = false;
				if (!flag)
				{
					dataRow4["Debit"] = row["Amount"];
				}
				else
				{
					dataRow4["DebitFC"] = row["AmountFC"];
				}
				dataRow4["Credit"] = DBNull.Value;
				dataRow4["CreditFC"] = DBNull.Value;
				dataRow4.EndEdit();
				gLData.JournalDetailsTable.Rows.Add(dataRow4);
			}
			return gLData;
		}

		public DataSet GetUnallocatedPurchasePrePayments(bool includeApplied)
		{
			try
			{
				DataSet dataSet = new DataSet();
				string text = "SELECT PPI.SYSDOCID,PPI.VOUCHERID,V.VendorID,V.VendorName,PPI.SOURCEVOUCHERID as PO_NUMBER ,PPI.POAMOUNT AS PO_AMOUNT,ISNULL(PPI.AMOUNTFC, PPI.Amount) AS InvoiceAmount,\r\n                                PPI.TransactionDate AS [Invoice Date],ISNULL(PPI.CurrencyID,(SELECT CurrencyID FROM Currency WHERE IsBase='True')) Cur,\r\n                                 (  ISNULL((SELECT CASE WHEN CurrencyID IS NULL OR CurrencyID =(SELECT CurrencyID FROM Currency WHERE IsBase='True')\r\n                                THEN SUM(PaymentAmount) + SUM(ISNULL(DiscountAmount,0)) ELSE SUM(ISNULL(ISNULL(PaymentAmountFC,PaymentAmount),0)) + SUM(ISNULL(ISNULL(DiscountAmountFC,DiscountAmount),0)) END   \r\n\t\t\t\t\t\t\t\tFROM AP_Payment_Allocation PA WHERE Pa.InvoiceVoucherID= PPI.VoucherID AND PA.InvoiceSysDocID = PPI.SysDocID AND PA.VendorID= PPI.VendorID   GROUP BY CurrencyID),0)) AS Paid,\r\n\r\n                                 ( ISNULL(PPI.AMOUNTFC, PPI.Amount) - ISNULL((SELECT CASE WHEN CurrencyID IS NULL OR CurrencyID =(SELECT CurrencyID FROM Currency WHERE IsBase='True')\r\n                                THEN SUM(PaymentAmount) + SUM(ISNULL(DiscountAmount,0)) ELSE SUM(ISNULL(ISNULL(PaymentAmountFC,PaymentAmount),0)) + SUM(ISNULL(ISNULL(DiscountAmountFC,DiscountAmount),0)) END   \r\n\t\t\t\t\t\t\t\tFROM AP_Payment_Allocation PA WHERE Pa.PaymentVoucherID= PPI.VoucherID AND PA.VendorID= PPI.VendorID   GROUP BY CurrencyID),0)) AS Unallocated,\r\n\t\t\t\t\t\t\t\tISNULL(Status,0) as Status\r\n                                FROM PURCHASE_PREPAYMENT_INVOICE  PPI  INNER JOIN Vendor V ON V.VendorID = PPI.VendorID ";
				if (!includeApplied)
				{
					text += " WHERE ISNULL(Status,0) <> 2";
				}
				SqlCommand sqlCommand = new SqlCommand(text);
				FillDataSet(dataSet, "AR_Payment_Allocation", sqlCommand);
				return dataSet;
			}
			catch
			{
				throw;
			}
		}

		public PurchasePrepaymentInvoiceData GetPurchasePrepaymentInvoiceByID(string sysDocID, string voucherID)
		{
			try
			{
				PurchasePrepaymentInvoiceData purchasePrepaymentInvoiceData = new PurchasePrepaymentInvoiceData();
				string textCommand = "SELECT PC.*,PT.TermName,PO.CurrencyID as POCurrencyID from Purchase_Prepayment_Invoice PC\r\n                                LEFT OUTER JOIN Payment_Term PT ON PT.PaymentTermID = PC.TermID\r\n                                LEFT OUTER JOIN purchase_order PO ON PO.SysDocID=PC.SourceSysDocID AND PO.VoucherID=PC.SourceVoucherID\r\n                                WHERE PC.SysDocID = '" + sysDocID + "' AND PC.VoucherID = '" + voucherID + "'";
				FillDataSet(purchasePrepaymentInvoiceData, "Purchase_PrePayment_Invoice", textCommand);
				return purchasePrepaymentInvoiceData;
			}
			catch
			{
				throw;
			}
		}

		public string HasPendingPrepayments(DataSet data)
		{
			try
			{
				foreach (DataRow row in data.Tables[0].Rows)
				{
					string invoiceSysDocID = row["SysDocID"].ToString();
					string text = row["VoucherID"].ToString();
					DataSet invoicePrepayments = GetInvoicePrepayments(invoiceSysDocID, text);
					if (invoicePrepayments != null && invoicePrepayments.Tables[0].Rows.Count > 0)
					{
						return text;
					}
				}
				return "";
			}
			catch
			{
				throw;
			}
		}

		public DataSet GetPurchasePrepaymentInvoiceToPrint(string sysDocID, string voucherID)
		{
			return GetPurchasePrepaymentInvoiceToPrint(sysDocID, new string[1]
			{
				voucherID
			});
		}

		public DataSet GetPurchasePrepaymentInvoiceToPrint(string sysDocID, string[] voucherID)
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
				SqlCommand sqlCommand = new SqlCommand("SELECT PC.*,V.Vendorname, PO.*, PT.TermName\r\n                                FROM Purchase_Prepayment_Invoice  PC   \r\n\t\t\t\t\t\t\t\tLEFT OUTER JOIN Purchase_Order PO ON PC.SourceSysDocID = PO.SysDocID AND PC.SourceVoucherID = PO.VoucherID\r\n                                INNER JOIN Vendor V ON V.VendorID=PC.VendorID\r\n\t\t\t\t\t\t\t\tLEFT OUTER JOIN Payment_Term PT ON PC.PrepaymentTermID =PT.PaymentTermID\r\n                                                                WHERE PC.SysDocID = '" + sysDocID + "' AND PC.VoucherID IN (" + text + ")");
				FillDataSet(dataSet, "Purchase_PrePayment_Invoice", sqlCommand);
				if (dataSet == null || dataSet.Tables.Count == 0 || dataSet.Tables["Purchase_PrePayment_Invoice"].Rows.Count == 0)
				{
					return null;
				}
				return dataSet;
			}
			catch
			{
				throw;
			}
		}

		public bool ClosePrepaymentInvoice(DataSet data)
		{
			bool flag = true;
			try
			{
				string text = "";
				SqlTransaction sqlTransaction = base.DBConfig.StartNewTransaction();
				foreach (DataRow row in data.Tables[0].Rows)
				{
					string text2 = row["SysDocID"].ToString();
					string text3 = row["VoucherID"].ToString();
					row["VendorID"].ToString();
					decimal num = default(decimal);
					if (CanUpdate(text2, text3, sqlTransaction) == -1)
					{
						throw new CompanyException("Prepayment invoice '" + text3 + "' is already closed.");
					}
					DataRow dataRow = GetPurchasePrepaymentInvoiceByID(text2, text3).Tables[0].Rows[0];
					num = ((!dataRow["AmountFC"].IsDBNullOrEmpty()) ? decimal.Parse(dataRow["AmountFC"].ToString()) : decimal.Parse(dataRow["Amount"].ToString()));
					_ = DateTime.Now;
					text = "SELECT CASE WHEN SUM(ISNULL(APA.PaymentAmountFC, APA.PaymentAmount)) = " + num + " THEN 'True' ELSE 'False' END AS VAL FROM AP_Payment_Allocation APA WHERE InvoiceSysDocID = '" + text2 + "' AND InvoiceVoucherID = '" + text3 + "'";
					if (!bool.Parse(ExecuteScalar(text, sqlTransaction).ToString()))
					{
						throw new CompanyException("The prepayment invoice '" + text3 + "' is not fully paid.", 1063);
					}
					text = "UPDATE Purchase_Prepayment_Invoice  SET Status = 1  WHERE SysDocID = '" + text2 + "' AND VoucherID = '" + text3 + "'";
					flag &= (ExecuteNonQuery(text, sqlTransaction) > 0);
					text = "\t UPDATE  APJournal SET ExcludeInPayment = 'True' WHERE SysDocID  = '" + text2 + "' AND VoucherID = '" + text3 + "'";
					flag &= (ExecuteNonQuery(text, sqlTransaction) > 0);
					GLData journalData = CreateClosePrepaymentGLData(dataRow, sqlTransaction);
					flag &= new Journal(base.DBConfig).InsertUpdateJournal(journalData, isUpdate: false, sqlTransaction);
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

		public DataSet GetPurchasePrepaymentInvoiceToPrintTR(string sysDocID, string voucherID)
		{
			return GetPurchasePrepaymentInvoiceToPrintTR(sysDocID, new string[1]
			{
				voucherID
			});
		}

		public DataSet GetPurchasePrepaymentInvoiceToPrintTR(string sysDocID, string[] voucherID)
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
				SqlCommand sqlCommand = new SqlCommand("SELECT ISNULL(PR.AmountFC,PR.Amount) Amount,BF.PrintTemplateName as [TemplateName],V.VendorName,VA.Address1,VA.Address2,VA.Address3,V.CountryID,PR.TransactionDate,\r\n                            ( SELECT BankAccountNumber FROM Account WHERE Accountid IN (BF.CurrentAccountID)) AS [Account Number], \r\n                            (SELECT CountryName FROM Country WHERE CountryID IN (V.CountryID)) AS [Vendor Country],\r\n                            VA.AddressPrintFormat,V.BankName AS [Vendor Bank],V.BankBranch AS [Vendor Bank Address],V.BankAccountNumber AS [Vendor Account],PR.Reference,\r\n                            PT1.TermName AS PayeeTerm,V.CurrencyID AS PayeeCurrencyID,'' as AmountInWords  \r\n                            FROM Payment_Request PR LEFt OUTER JOIN Account ON PR.PayFromID=Account.AccountID \r\n                            LEFt OUTER JOIN Chequebook ON PR.PayFromID=Chequebook.ChequebookID\r\n                            LEFt OUTER JOIN Bank_Facility BF ON PR.PayFromID=BF.FacilityID  \r\n                            INNER JOIN Vendor V ON V.VendorID  = PR.PayeeID\r\n                            LEFT JOIN Vendor_Address VA ON V.VendorID=VA.VendorID\r\n                            LEFT OUTER JOIN Payment_Term PT1 ON PT1.PaymentTermID = V.PaymentTermID\r\n                            LEFT OUTER JOIN Purchase_Order PO ON PO.SysDocID = POSysDocID AND PO.VoucherID = POVoucherID\r\n                            LEFT OUTER JOIN Payment_Term POTerm ON POTerm.PaymentTermID = PO.TermID\r\n                             WHERE PR.SysDocID = '" + sysDocID + "' AND PR.VoucherID IN (" + text + ")");
				FillDataSet(dataSet, "Purchase_PrePayment_Invoice", sqlCommand);
				if (dataSet == null || dataSet.Tables.Count == 0 || dataSet.Tables["Purchase_PrePayment_Invoice"].Rows.Count == 0)
				{
					return null;
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
			string text3 = "SELECT  PC.SysDocID,PC.VoucherID, PC.SourceVoucherID AS [GRN],PC.TransactionDate as [Date],V.VendorName[Supplier]\r\n                            FROM Purchase_Prepayment_Invoice PC   \r\n\t\t                     LEFT JOIN Vendor V ON V.VendorID=PC.VendorID  ";
			if (from != DateTime.MinValue)
			{
				text3 = text3 + " WHERE PC.TransactionDate Between '" + text + "' AND '" + text2 + "'";
			}
			if (!showVoid)
			{
				text3 += " AND ISNULL(PC.IsVoid,'False')='False'";
			}
			text3 += " ORDER BY PC.SysDocID,PC.VoucherID";
			SqlCommand sqlCommand = new SqlCommand(text3);
			FillDataSet(dataSet, "Purchase_PrePayment_Invoice", sqlCommand);
			return dataSet;
		}

		public DataSet GetOpenPurchasePrepaymentInvoices(string vendorID)
		{
			try
			{
				DataSet dataSet = new DataSet();
				SqlCommand sqlCommand = new SqlCommand("SELECT PC.SysDocID [Doc ID],PC.VoucherID [Number],PC.SourceVoucherID as GRN,PC.TransactionDate as Date FROM Purchase_claim PC\r\n                    LEFT JOIN Purchase_Receipt PR ON PR.SysDocID=PC.SourceSysdocid AND PR.VoucherID=PC.SourceVoucherID\r\n                    WHERE PC.ClaimStatus=1 AND PR.VendorID ='" + vendorID + "'");
				FillDataSet(dataSet, "Purchase_PrePayment_Invoice", sqlCommand);
				return dataSet;
			}
			catch
			{
				throw;
			}
		}

		public DataSet GetInvoicePrepayments(string invoiceSysDocID, string invoiceVoucherID)
		{
			try
			{
				DataSet dataSet = new DataSet();
				SqlCommand sqlCommand = new SqlCommand("  SELECT * FROM (\r\n                                 SELECT PPI.SysDocID,PPI.VoucherID,ISNULL(amountFC,Amount) AS Invoiced,ISNULL((SELECT SUM(ISNULL(ISNULL(PaymentAmountFC,PaymentAmount),0)) FROM AP_Payment_Allocation ARP WHERE ARP.InvoiceSysDocID = PPI.SysDocID AND ARP.InvoiceVoucherID = PPI.VoucherID ),0) AS PaidAmount,\r\n                                ISNULL((SELECT SUM(ISNULL(ISNULL(PaymentAmountFC,PaymentAmount),0)) FROM AP_Payment_Allocation ARP WHERE ARP.PaymentSysDocID = 'SYS_017' AND ARP.PaymentVoucherID = PPI.VoucherID ),0) AS Allocated\r\n                                FROM Purchase_Prepayment_Invoice PPI INNER JOIN \r\n                                (SELECT distinct  posd.SourceSysDocID , posd.SourceVoucherID from Purchase_Invoice_Detail PID with (nolock)  inner join Purchase_Receipt PR on pid.OrderSysDocID = pr.SysDocID and pid.OrderVoucherID  = pr.VoucherID \r\n                                        inner join PO_Shipment   POS with (nolock) on pos.SysDocID = PR.SourceSysDocID and pos.VoucherID =pr.SourceVoucherID \r\n                                        inner join PO_Shipment_Detail POSD  with (nolock)  on POSD.SysDocID = pos.SysDocID and POSD.VoucherID =POS.VoucherID \r\n                                        inner join System_Document   SD on posd.SourceSysDocID = sd.SysDocID  and sd.SysDocType in( '38', '31') \r\n\r\n                                         /*making sure only POS are loading */\r\n                                        Where pid.SysDocID = '" + invoiceSysDocID + "' and pid.VoucherID = '" + invoiceVoucherID + "'  \r\n\r\n                                         UNION \r\n \r\n                                         /*Purchase Invoice Created From GRN < PO */\r\n\r\n                                        select distinct  PR.SourceSysDocID , PR.SourceVoucherID from Purchase_Invoice_Detail PID with (nolock) inner join Purchase_Receipt PR  with (nolock) on pid.OrderSysDocID = pr.SysDocID and pid.OrderVoucherID  = pr.VoucherID \r\n                                        inner join System_Document   SD on PR.SourceSysDocID = sd.SysDocID  and sd.SysDocType  in( '38', '31')  \r\n                                        Where pid.SysDocID = '" + invoiceSysDocID + "' and pid.VoucherID = '" + invoiceVoucherID + "' \r\n                                        UNION\r\n                                        /* PI created  from Purchase Order */\r\n                                        select distinct  PID.OrderSysDocID   , PID.OrderVoucherID from Purchase_Invoice_Detail PID  with (nolock)\r\n                                        inner join System_Document   SD on PID.OrderSysDocID = sd.SysDocID  and sd.SysDocType  in( '38', '31')  \r\n                                        Where pid.SysDocID = '" + invoiceSysDocID + "' and pid.VoucherID = '" + invoiceVoucherID + "' ) T\r\n\r\n                                        ON T.SourceSysDocID = PPI.SourceSysDocID AND  T.SourceVoucherID = PPI.SourceVoucherID\r\n                                        WHERE ISNULL(IsVoid,'False')='False' AND InvoiceVoucherID IS NULL) X\r\n\t\t\t\t\t\t\t\t\t\tWHERE Allocated<Invoiced");
				FillDataSet(dataSet, "Purchase_PrePayment_Invoice", sqlCommand);
				return dataSet;
			}
			catch
			{
				throw;
			}
		}

		public DataSet GetInvoicesToAllocate(string prepaymentSysDocID, string prepaymentVoucherID)
		{
			try
			{
				DataSet dataSet = new DataSet();
				SqlCommand sqlCommand = new SqlCommand("SELECT  distinct  SysDocID  ,  VoucherID  FROM   Purchase_Invoice_Detail  WHERE  OrderSysDocID+OrderVoucherID IN  ( \r\n                            /* For Prpayment > PO >  GRN> Purchace invoice */\r\n                            SELECT sysdocid+voucherid FROM Purchase_Receipt_Detail   WHERE OrderSysDocID +OrderVoucherID   IN(\r\n                                      SELECT SourceSysDocID + SourceVoucherID  FROM Purchase_Prepayment_Invoice PPI    WHERE  VoucherID = '" + prepaymentVoucherID + "')\r\n                            union all\r\n                            /* For Prpayment > PO > pakinglist > GRN> Purchace invoice */\r\n                                  SELECT sysdocid+voucherid FROM Purchase_Receipt_Detail   WHERE PKSysDocID +PKVoucherID\r\n                                    IN(SELECT  sysdocid + voucherid FROM PO_Shipment_Detail    WHERE     SourceSysDocID + SourceVoucherID    IN(\r\n                                            SELECT SourceSysDocID + SourceVoucherID  FROM Purchase_Prepayment_Invoice PPI    WHERE  VoucherID = '" + prepaymentVoucherID + "'))\r\n\r\n                                            Union ALL\r\n\t\t\t\t\t\t\t\t\t\t\t/* For Prpayment > PONI > pakinglist > Purchace invoice_NI */\r\n\t\t\t\t\t\t\t\t\t\t\tSELECT  sysdocid+ voucherid FROM Purchase_Invoice_NonInv_Detail    WHERE     OrderSysDocID +OrderVoucherID   IN (\r\n                   \r\n\t\t\t\t\t                            SELECT SourceSysDocID + SourceVoucherID  FROM Purchase_Prepayment_Invoice PPI    WHERE  VoucherID ='" + prepaymentVoucherID + "'))");
				FillDataSet(dataSet, "Invoice", sqlCommand);
				return dataSet;
			}
			catch
			{
				throw;
			}
		}

		public bool OrderHasShippedQuantity(string sysDocID, string voucherNumber, SqlTransaction sqlTransaction)
		{
			string exp = "Select Count(*) FROM Sales_Order_Detail SOD\r\n                                WHERE SysdocID='" + sysDocID + "' AND VoucherID='" + voucherNumber + "' Having SUM(ISNULL(QuantityShipped,0))>0";
			object obj = ExecuteScalar(exp, sqlTransaction);
			if (obj != null && obj.ToString() != "" && int.Parse(obj.ToString()) > 0)
			{
				return true;
			}
			return false;
		}
	}
}
