using Micromind.Common;
using Micromind.Common.Data;
using Micromind.Common.Libraries;
using System;
using System.Data;
using System.Data.SqlClient;

namespace Micromind.Data
{
	public sealed class PaymentRequest : StoreObject
	{
		private const string PAYMENTREQUEST_TABLE = "Payment_Request";

		private const string SYSDOCID_PARM = "@SysDocID";

		private const string VOUCHERID_PARM = "@VoucherID";

		private const string TYPEID_PARM = "@TypeID";

		private const string PAYFROMID_PARM = "@PayFromID";

		private const string PAYEETYPE_PARM = "@PayeeType";

		private const string PAYEEID_PARM = "@PayeeID";

		private const string AMOUNT_PARM = "@Amount";

		private const string AMOUNTFC_PARM = "@AmountFC";

		private const string POSYSDOCID_PARM = "@POSysDocID";

		private const string POVOUCHERID_PARM = "@POVoucherID";

		private const string PLSYSDOCID_PARM = "@PLSysDocID";

		private const string PLVOUCHERID_PARM = "@PLVoucherID";

		private const string CURRENCYID_PARM = "@CurrencyID";

		private const string REASON_PARM = "@Reason";

		private const string STATUS_PARM = "@Status";

		private const string REFERENCE_PARM = "@Reference";

		private const string TRANSACTIONDATE_PARM = "@TransactionDate";

		private const string NOTE_PARM = "@Note";

		private const string AVAILABLEBAL_PARM = "@AvailableBal";

		private const string CURRENTBAL_PARM = "@CurrentBal";

		private const string PAYMENTREQUESTED_PARM = "@PaymentRequested";

		private const string PAYMENTREQUESTEDFC_PARM = "@PaymentRequestedFC";

		private const string INVOICENO_PARM = "@InvoiceNos";

		private const string AUTHORIZEDBY_PARM = "@Authorizedby";

		private const string NOOFINVOICES_PARM = "@NoofInvoices";

		private const string NOOFPL_PARM = "@NoofPL";

		private const string NOOFBOL_PARM = "@NoofBOL";

		private const string NOOFGOODS_PARM = "@NoofGoods";

		private const string CREATEDBY_PARM = "@CreatedBy";

		private const string DATECREATED_PARM = "@DateCreated";

		private const string UPDATEDBY_PARM = "@UpdatedBy";

		private const string DATEUPDATED_PARM = "@DateUpdated";

		public PaymentRequest(Config config)
			: base(config)
		{
		}

		private string GetInsertUpdatePaymentRequestText(bool isUpdate)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddInsertUpdateParameters("Payment_Request", new FieldValue("SysDocID", "@SysDocID", isUpdateConditionField: true), new FieldValue("VoucherID", "@VoucherID", isUpdateConditionField: true), new FieldValue("TypeID", "@TypeID"), new FieldValue("TransactionDate", "@TransactionDate"), new FieldValue("PayFromID", "@PayFromID"), new FieldValue("PayeeType", "@PayeeType"), new FieldValue("PayeeID", "@PayeeID"), new FieldValue("Amount", "@Amount"), new FieldValue("AmountFC", "@AmountFC"), new FieldValue("POSysDocID", "@POSysDocID"), new FieldValue("POVoucherID", "@POVoucherID"), new FieldValue("PLSysDocID", "@PLSysDocID"), new FieldValue("PLVoucherID", "@PLVoucherID"), new FieldValue("Status", "@Status"), new FieldValue("CurrencyID", "@CurrencyID"), new FieldValue("Reason", "@Reason"), new FieldValue("Reference", "@Reference"), new FieldValue("InvoiceNos", "@InvoiceNos"), new FieldValue("Authorizedby", "@Authorizedby"), new FieldValue("NoofInvoices", "@NoofInvoices"), new FieldValue("NoofPL", "@NoofPL"), new FieldValue("NoofBOL", "@NoofBOL"), new FieldValue("NoofGoods", "@NoofGoods"), new FieldValue("Note", "@Note"), new FieldValue("PaymentRequested", "@PaymentRequested"), new FieldValue("PaymentRequestedFC", "@PaymentRequestedFC"), new FieldValue("AvailableBal", "@AvailableBal"), new FieldValue("CurrentBal", "@CurrentBal"));
			if (isUpdate)
			{
				sqlBuilder.AddInsertUpdateParameters("Payment_Request", new FieldValue("DateUpdated", "@DateUpdated", isUpdateConditionField: true, checkForNullValue: true));
			}
			if (isUpdate)
			{
				return sqlBuilder.GetUpdateExpression();
			}
			return sqlBuilder.GetInsertExpression();
		}

		private SqlCommand GetInsertUpdatePaymentRequestCommand(bool isUpdate)
		{
			SqlParameterCollection parameters;
			if (isUpdate)
			{
				if (updateCommand != null)
				{
					updateCommand = null;
				}
				updateCommand = new SqlCommand(GetInsertUpdatePaymentRequestText(isUpdate: true), base.DBConfig.Connection);
				updateCommand.CommandType = CommandType.Text;
				parameters = updateCommand.Parameters;
			}
			else
			{
				if (insertCommand != null)
				{
					insertCommand = null;
				}
				insertCommand = new SqlCommand(GetInsertUpdatePaymentRequestText(isUpdate: false), base.DBConfig.Connection);
				insertCommand.CommandType = CommandType.Text;
				parameters = insertCommand.Parameters;
			}
			parameters.Add("@SysDocID", SqlDbType.NVarChar);
			parameters.Add("@VoucherID", SqlDbType.NVarChar);
			parameters.Add("@TypeID", SqlDbType.TinyInt);
			parameters.Add("@PayFromID", SqlDbType.NVarChar);
			parameters.Add("@PayeeType", SqlDbType.Char);
			parameters.Add("@TransactionDate", SqlDbType.DateTime);
			parameters.Add("@PayeeID", SqlDbType.NVarChar);
			parameters.Add("@Amount", SqlDbType.Money);
			parameters.Add("@AmountFC", SqlDbType.Money);
			parameters.Add("@POSysDocID", SqlDbType.NVarChar);
			parameters.Add("@POVoucherID", SqlDbType.NVarChar);
			parameters.Add("@PLSysDocID", SqlDbType.NVarChar);
			parameters.Add("@PLVoucherID", SqlDbType.NVarChar);
			parameters.Add("@Reason", SqlDbType.TinyInt);
			parameters.Add("@Status", SqlDbType.TinyInt);
			parameters.Add("@CurrencyID", SqlDbType.NVarChar);
			parameters.Add("@Reference", SqlDbType.NVarChar);
			parameters.Add("@Note", SqlDbType.NVarChar);
			parameters.Add("@AvailableBal", SqlDbType.Money);
			parameters.Add("@PaymentRequested", SqlDbType.Money);
			parameters.Add("@PaymentRequestedFC", SqlDbType.Money);
			parameters.Add("@CurrentBal", SqlDbType.Money);
			parameters.Add("@InvoiceNos", SqlDbType.NVarChar);
			parameters.Add("@Authorizedby", SqlDbType.NVarChar);
			parameters.Add("@NoofInvoices", SqlDbType.Int);
			parameters.Add("@NoofPL", SqlDbType.Int);
			parameters.Add("@NoofBOL", SqlDbType.Int);
			parameters.Add("@NoofGoods", SqlDbType.NVarChar);
			parameters["@SysDocID"].SourceColumn = "SysDocID";
			parameters["@VoucherID"].SourceColumn = "VoucherID";
			parameters["@TypeID"].SourceColumn = "TypeID";
			parameters["@PayFromID"].SourceColumn = "PayFromID";
			parameters["@PayeeType"].SourceColumn = "PayeeType";
			parameters["@TransactionDate"].SourceColumn = "TransactionDate";
			parameters["@PayeeID"].SourceColumn = "PayeeID";
			parameters["@Amount"].SourceColumn = "Amount";
			parameters["@AmountFC"].SourceColumn = "AmountFC";
			parameters["@POSysDocID"].SourceColumn = "POSysDocID";
			parameters["@POVoucherID"].SourceColumn = "POVoucherID";
			parameters["@PLSysDocID"].SourceColumn = "PLSysDocID";
			parameters["@PLVoucherID"].SourceColumn = "PLVoucherID";
			parameters["@Status"].SourceColumn = "Status";
			parameters["@CurrencyID"].SourceColumn = "CurrencyID";
			parameters["@Reason"].SourceColumn = "Reason";
			parameters["@Reference"].SourceColumn = "Reference";
			parameters["@Note"].SourceColumn = "Note";
			parameters["@InvoiceNos"].SourceColumn = "InvoiceNos";
			parameters["@Authorizedby"].SourceColumn = "Authorizedby";
			parameters["@NoofInvoices"].SourceColumn = "NoofInvoices";
			parameters["@NoofPL"].SourceColumn = "NoofPL";
			parameters["@NoofBOL"].SourceColumn = "NoofBOL";
			parameters["@NoofGoods"].SourceColumn = "NoofGoods";
			parameters["@AvailableBal"].SourceColumn = "AvailableBal";
			parameters["@PaymentRequested"].SourceColumn = "PaymentRequested";
			parameters["@PaymentRequestedFC"].SourceColumn = "PaymentRequestedFC";
			parameters["@CurrentBal"].SourceColumn = "CurrentBal";
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

		private bool ValidateData(PaymentRequestData journalData)
		{
			return true;
		}

		public bool VoidPaymentRequest(string sysDocID, string voucherID, bool isVoid)
		{
			bool flag = true;
			try
			{
				SqlTransaction sqlTransaction = base.DBConfig.StartNewTransaction();
				string exp = "UPDATE Payment_Request SET IsVoid = '" + isVoid.ToString() + "' WHERE SysDocID='" + sysDocID + "' AND VoucherID = '" + voucherID + "'";
				flag &= (ExecuteNonQuery(exp, sqlTransaction) > 0);
				if (!flag)
				{
					return flag;
				}
				ActivityTypes activityType = ActivityTypes.Void;
				if (!isVoid)
				{
					activityType = ActivityTypes.Unvoid;
				}
				AddActivityLog("Payment Request", voucherID, sysDocID, activityType, sqlTransaction);
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

		public bool DeletePaymentRequest(string sysDocID, string voucherID)
		{
			bool flag = true;
			try
			{
				string text = "";
				SqlTransaction sqlTransaction = base.DBConfig.StartNewTransaction();
				text = "DELETE FROM Payment_Request WHERE SysDocID='" + sysDocID + "' AND VoucherID = '" + voucherID + "'";
				flag &= (ExecuteNonQuery(text, sqlTransaction) >= 0);
				if (!flag)
				{
					return flag;
				}
				ActivityTypes activityType = ActivityTypes.Delete;
				AddActivityLog("Payment Request", voucherID, sysDocID, activityType, sqlTransaction);
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

		public bool InsertUpdatePaymentRequest(PaymentRequestData paymentRequestData, bool isUpdate)
		{
			bool flag = true;
			SqlCommand insertUpdatePaymentRequestCommand = GetInsertUpdatePaymentRequestCommand(isUpdate);
			try
			{
				DataRow dataRow = paymentRequestData.PaymentRequestTable.Rows[0];
				_ = new CompanyInformations(base.DBConfig).CurrencyDecimalPoints;
				SqlTransaction sqlTransaction = base.DBConfig.StartNewTransaction();
				string text = dataRow["VoucherID"].ToString();
				string sysDocID = dataRow["SysDocID"].ToString();
				if (isUpdate && OrderHasShippedQuantity(sysDocID, text, sqlTransaction))
				{
					throw new CompanyException("Unable to update. Some of the items in this order has been already shipped.");
				}
				if (!isUpdate && new SystemDocuments(base.DBConfig).ExistDocumentNumber("Payment_Request", "VoucherID", dataRow["SysDocID"].ToString(), text, sqlTransaction))
				{
					throw new CompanyException("Document number already exist.", 1046);
				}
				insertUpdatePaymentRequestCommand.Transaction = sqlTransaction;
				flag = (isUpdate ? (flag & Update(paymentRequestData, "Payment_Request", insertUpdatePaymentRequestCommand)) : (flag & Insert(paymentRequestData, "Payment_Request", insertUpdatePaymentRequestCommand)));
				if (!flag)
				{
					throw new Exception("Failed to save the transaction because of an unexpected error. Please try again.");
				}
				flag &= UpdateTableRowInsertUpdateInfo("Payment_Request", "SysDocID", dataRow["SysDocID"].ToString(), "VoucherID", dataRow["VoucherID"].ToString(), sqlTransaction, !isUpdate);
				string entityName = "Payment Request";
				flag = (isUpdate ? (flag & AddActivityLog(entityName, text, sysDocID, ActivityTypes.Update, sqlTransaction)) : (flag & AddActivityLog(entityName, text, sysDocID, ActivityTypes.Add, sqlTransaction)));
				if (!isUpdate)
				{
					flag &= new SystemDocuments(base.DBConfig).UpdateNextDocumentNumber(dataRow["SysDocID"].ToString(), dataRow["VoucherID"].ToString(), "Payment_Request", "VoucherID", sqlTransaction);
				}
				if (!flag)
				{
					return flag;
				}
				new Approval(base.DBConfig).CreateTransactionApprovalTasks(SysDocTypes.PaymentRequest, sysDocID, text, "Payment_Request", sqlTransaction);
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

		public PaymentRequestData GetPaymentRequestByID(string sysDocID, string voucherID)
		{
			try
			{
				PaymentRequestData paymentRequestData = new PaymentRequestData();
				string textCommand = "SELECT * FROM Payment_Request WHERE VoucherID='" + voucherID + "' AND SysDocID='" + sysDocID + "'";
				FillDataSet(paymentRequestData, "Payment_Request", textCommand);
				DataSet dataSet = new DataSet();
				textCommand = "select SUM(Amount) as PaymentRequested,SUM(AmountFC) as PaymentRequestedFC FROM Payment_Request WHERE PAYEEID IN (SELECT PAYEEID FROM Payment_Request WHERE VoucherID='" + voucherID + "' AND SysDocID='" + sysDocID + "') AND Status=1";
				FillDataSet(dataSet, "tempds", textCommand);
				paymentRequestData.Merge(dataSet);
				return paymentRequestData;
			}
			catch
			{
				throw;
			}
		}

		public DataSet GetOpenOrdersSummary(string customerID, bool isExport)
		{
			try
			{
				DataSet dataSet = new DataSet();
				string text = "SELECT SO.SysDocID [Doc ID],SO.VoucherID [Number],TransactionDate AS [Date],SO.CustomerID + '-' + C.CustomerName AS [Customer] FROM Sales_Order SO\r\n                             INNER JOIN Customer C ON SO.CustomerID=C.CustomerID  WHERE ISNULL(IsVoid,'False')='False' AND ISNULL(IsExport,'False')= '" + isExport.ToString() + "' AND Status=1 ";
				if (customerID != "")
				{
					text = text + " AND SO.CustomerID='" + customerID + "'";
				}
				FillDataSet(dataSet, "Payment_Request", text);
				return dataSet;
			}
			catch
			{
				throw;
			}
		}

		public DataSet GetOpenOrderListReport()
		{
			DataSet dataSet = new DataSet();
			string textCommand = "SELECT SysDocID,VoucherID,SO.CustomerID,CustomerName,TransactionDate,SO.SalespersonID,Total\r\n                            FROM Sales_Order SO INNER JOIN Customer ON SO.CustomerID=Customer.CustomerID\r\n                            WHERE ISNULL(IsVoid,'False')='False' AND Status=1";
			FillDataSet(dataSet, "Orders", textCommand);
			return dataSet;
		}

		public DataSet GetPaymentRequestToPrint(string sysDocID, string voucherID)
		{
			return GetPaymentRequestToPrint(sysDocID, new string[1]
			{
				voucherID
			});
		}

		public DataSet GetPaymentRequestToPrint(string sysDocID, string[] voucherID)
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
				string cmdText = "SELECT  PR.*,PO.CurrencyID AS [POCurrency],\r\n\t\t\t\t\t\t(CASE TypeID\r\n\t\t\t\t\t\tWHEN 1 THEN Chequebook.ChequebookName\r\n\t\t\t\t\t\tWHEN 2 THEN Account.AccountName\r\n\t\t\t\t\t\tWHEN 3 THEN Bank_Facility.FacilityName\r\n                        WHEN 4 THEN Register.RegisterName\r\n\t\t\t\t\t\tELSE 'None' END) AS PayFromName, CASE PR.[Status] WHEN 1 THEN 'Open' WHEN 2 THEN 'Paid' WHEN 3 THEN 'Cancelled' END AS StatusName,\r\n                        (CASE TypeID\r\n                        WHEN 1 THEN 'Cheque Payment'\r\n                        WHEN 2 THEN 'Bank Payment'\r\n                        WHEN 3 THEN 'Trust Receipt'\r\n                        WHEN 4 THEN 'Cash Payment'\r\n                        ELSE 'None' END) AS TypeName, V.VendorName,\r\n                        PT1.TermName AS PayeeTerm,0.0 AS PayeeBalance,0.0 AS PayeeTotalDue,V.CurrencyID AS PayeeCurrencyID,\r\n\t\t\t\t\t\tCASE PR.Reason WHEN 1 THEN 'Advance' WHEN 2 THEN 'Outstanding Balance' END AS ReasonName,POTerm.TermName AS POTerm,PO.ETA,\r\n\t\t\t\t\t\tPO.Total AS POAmount,ISNULL((SELECT SUM(ISNULL(AmountFC,Amount)) FROM Payment_Request WHERE POSysDocID = PO.SysDocID AND POVoucherID = PO.VoucherID),0) AS POPaidAmount,\r\n\t\t\t\t\t\tPO.Total - ISNULL((SELECT SUM(ISNULL(AmountFC,Amount)) FROM Payment_Request WHERE POSysDocID = PO.SysDocID AND POVoucherID = PO.VoucherID),0) AS POBalance ,0.0 AS PurchaseClaim,0.0 as QualityClaim,0.0 AS PCCount,0.0 as QCCount\r\n\t\t\t\t\t\tFROM Payment_Request PR LEFt OUTER JOIN \r\n\t\t\t\t\t\tAccount ON PR.PayFromID=Account.AccountID \r\n\t\t\t\t\t\tLEFt OUTER JOIN Chequebook ON PR.PayFromID=Chequebook.ChequebookID\r\n\t\t\t\t\t\tLEFt OUTER JOIN Bank_Facility ON PR.PayFromID=Bank_Facility.FacilityID \r\n                        LEFt OUTER JOIN Register ON PR.PayFromID=Register.RegisterID \r\n\t\t\t\t\t\tINNER JOIN Vendor V ON V.VendorID  = PR.PayeeID\r\n\t\t\t\t\t\tLEFT OUTER JOIN Payment_Term PT1 ON PT1.PaymentTermID = V.PaymentTermID\r\n\t\t\t\t\t    LEFT OUTER JOIN Purchase_Order PO ON PO.SysDocID = POSysDocID AND PO.VoucherID = POVoucherID\r\n\t\t\t\t\t\tLEFT OUTER JOIN Payment_Term POTerm ON POTerm.PaymentTermID = PO.TermID\r\n                                WHERE PR.SysDocID = '" + sysDocID + "' AND PR.VoucherID IN (" + text + ")";
				SqlCommand sqlCommand = new SqlCommand(cmdText);
				FillDataSet(dataSet, "Payment_Request", sqlCommand);
				if (dataSet == null || dataSet.Tables.Count == 0 || dataSet.Tables["Payment_Request"].Rows.Count == 0)
				{
					return null;
				}
				DataRow dataRow = dataSet.Tables["Payment_Request"].Rows[0];
				string text2 = dataRow["POSysDocID"].ToString();
				string text3 = dataRow["POVoucherID"].ToString();
				cmdText = "SELECT  PR.*,PO.CurrencyID AS [POCurrency],\r\n\t\t\t\t\t\t(CASE TypeID\r\n\t\t\t\t\t\tWHEN 1 THEN Chequebook.ChequebookName\r\n\t\t\t\t\t\tWHEN 2 THEN Account.AccountName\r\n\t\t\t\t\t\tWHEN 3 THEN Bank_Facility.FacilityName\r\n                        WHEN 4 THEN Register.RegisterName\r\n\t\t\t\t\t\tELSE 'None' END) AS PayFromName, CASE PR.[Status] WHEN 1 THEN 'Open' WHEN 2 THEN 'Paid' WHEN 3 THEN 'Cancelled' END AS StatusName,\r\n                        (CASE TypeID\r\n                        WHEN 1 THEN 'Cheque Payment'\r\n                        WHEN 2 THEN 'Bank Payment'\r\n                        WHEN 3 THEN 'Trust Receipt'\r\n                        WHEN 4 THEN 'Cash Payment'\r\n                        ELSE 'None' END) AS TypeName, V.VendorName,\r\n                        PT1.TermName AS PayeeTerm,0.0 AS PayeeBalance,0.0 AS PayeeTotalDue,V.CurrencyID AS PayeeCurrencyID,\r\n\t\t\t\t\t\tCASE PR.Reason WHEN 1 THEN 'Advance' WHEN 2 THEN 'Outstanding Balance' END AS ReasonName,POTerm.TermName AS POTerm,PO.ETA,\r\n\t\t\t\t\t\tPO.Total AS POAmount,ISNULL((SELECT SUM(ISNULL(AmountFC,Amount)) FROM Payment_Request WHERE POSysDocID = PO.SysDocID AND POVoucherID = PO.VoucherID),0) AS POPaidAmount,\r\n\t\t\t\t\t\tPO.Total - ISNULL((SELECT SUM(ISNULL(AmountFC,Amount)) FROM Payment_Request WHERE POSysDocID = PO.SysDocID AND POVoucherID = PO.VoucherID),0) AS POBalance ,0.0 AS PurchaseClaim,0.0 as QualityClaim,0.0 AS PCCount,0.0 as QCCount\r\n\t\t\t\t\t\tFROM Payment_Request PR LEFt OUTER JOIN \r\n\t\t\t\t\t\tAccount ON PR.PayFromID=Account.AccountID \r\n\t\t\t\t\t\tLEFt OUTER JOIN Chequebook ON PR.PayFromID=Chequebook.ChequebookID\r\n\t\t\t\t\t\tLEFt OUTER JOIN Bank_Facility ON PR.PayFromID=Bank_Facility.FacilityID \r\n                        LEFt OUTER JOIN Register ON PR.PayFromID=Register.RegisterID \r\n\t\t\t\t\t\tINNER JOIN Vendor V ON V.VendorID  = PR.PayeeID\r\n\t\t\t\t\t\tLEFT OUTER JOIN Payment_Term PT1 ON PT1.PaymentTermID = V.PaymentTermID\r\n\t\t\t\t\t    LEFT OUTER JOIN Purchase_Order PO ON PO.SysDocID = POSysDocID AND PO.VoucherID = POVoucherID\r\n\t\t\t\t\t\tLEFT OUTER JOIN Payment_Term POTerm ON POTerm.PaymentTermID = PO.TermID\r\n                                WHERE PO.SysDocID = '" + text2 + "' AND PO.VoucherID ='" + text3 + "' AND NOT PR.VoucherID IN (" + text + ")";
				FillDataSet(dataSet, "EarlierPaymentReqDetails", cmdText);
				Vendors vendors = new Vendors(base.DBConfig);
				foreach (DataRow row in dataSet.Tables["Payment_Request"].Rows)
				{
					DateTime asOfDate = DateTime.Parse(row["TransactionDate"].ToString());
					row["VoucherID"].ToString();
					string vendorID = row["PayeeID"].ToString();
					string currencyID = row["PayeeCurrencyID"].ToString();
					DataSet vendorDueBalanceSummary = vendors.GetVendorDueBalanceSummary(vendorID, currencyID, asOfDate);
					decimal result = default(decimal);
					decimal result2 = default(decimal);
					decimal result3 = default(decimal);
					DataRow dataRow2 = vendorDueBalanceSummary.Tables[0].Rows[0];
					if (dataRow2 != null)
					{
						decimal.TryParse(dataRow2["Balance"].ToString(), out result);
						decimal.TryParse(dataRow2["BalanceDue"].ToString(), out result2);
						decimal.TryParse(dataRow2["Unallocated"].ToString(), out result3);
					}
					row["PayeeBalance"] = result;
					row["PayeeTotalDue"] = result2 - result3;
				}
				return dataSet;
			}
			catch
			{
				throw;
			}
		}

		public DataSet GetPaymentRequestToPrintTR(string sysDocID, string voucherID)
		{
			return GetPaymentRequestToPrintTR(sysDocID, new string[1]
			{
				voucherID
			});
		}

		public DataSet GetPaymentRequestToPrintTR(string sysDocID, string[] voucherID)
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
				SqlCommand sqlCommand = new SqlCommand("SELECT ISNULL(PR.AmountFC,PR.Amount) Amount,BF.PrintTemplateName as [TemplateName],V.VendorName,VA.Address1,VA.Address2,VA.Address3,V.CountryID,PR.TransactionDate,\r\n                            ( SELECT BankAccountNumber FROM Account WHERE Accountid IN (BF.CurrentAccountID)) AS [Account Number], \r\n                            (SELECT CountryName FROM Country WHERE CountryID IN (V.CountryID)) AS [Vendor Country],\r\n                            VA.AddressPrintFormat,V.BankName AS [Vendor Bank],V.BankBranch AS [Vendor Bank Address],V.BankAccountNumber AS [Vendor Account],PR.Reference,\r\n                            PT1.TermName AS PayeeTerm,V.CurrencyID AS PayeeCurrencyID,'' as AmountInWords,PR.InvoiceNos,PR.Authorizedby,PR.NoofInvoices,PR.NoofPL,PR.NoofBOL,PR.NoofGoods\r\n                            FROM Payment_Request PR LEFt OUTER JOIN Account ON PR.PayFromID=Account.AccountID \r\n                            LEFt OUTER JOIN Chequebook ON PR.PayFromID=Chequebook.ChequebookID\r\n                            LEFt OUTER JOIN Bank_Facility BF ON PR.PayFromID=BF.FacilityID  \r\n                            INNER JOIN Vendor V ON V.VendorID  = PR.PayeeID\r\n                            LEFT JOIN Vendor_Address VA ON V.VendorID=VA.VendorID\r\n                            LEFT OUTER JOIN Payment_Term PT1 ON PT1.PaymentTermID = V.PaymentTermID\r\n                            LEFT OUTER JOIN Purchase_Order PO ON PO.SysDocID = POSysDocID AND PO.VoucherID = POVoucherID\r\n                            LEFT OUTER JOIN Payment_Term POTerm ON POTerm.PaymentTermID = PO.TermID\r\n                             WHERE PR.SysDocID = '" + sysDocID + "' AND PR.VoucherID IN (" + text + ")");
				FillDataSet(dataSet, "Payment_Request", sqlCommand);
				if (dataSet == null || dataSet.Tables.Count == 0 || dataSet.Tables["Payment_Request"].Rows.Count == 0)
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
			string text3 = "SELECT  PR.SysDocID,PR.VoucherID,V.VendorName,PR.TransactionDate,                          \r\n                            (SELECT CountryName FROM Country WHERE CountryID IN (V.CountryID)) AS [Vendor Country],                      \r\n                            PT1.TermName AS PayeeTerm,V.CurrencyID AS PayeeCurrencyID ,  ISNULL(PR.AmountFC,PR.Amount) Amount\r\n                            FROM Payment_Request PR LEFt OUTER JOIN Account ON PR.PayFromID=Account.AccountID                       \r\n                            INNER JOIN Vendor V ON V.VendorID  = PR.PayeeID                         \r\n                           LEFT OUTER JOIN Payment_Term PT1 ON PT1.PaymentTermID = V.PaymentTermID\r\n                            LEFT OUTER JOIN Purchase_Order PO ON PO.SysDocID = POSysDocID AND PO.VoucherID = POVoucherID\r\n                            LEFT OUTER JOIN Payment_Term POTerm ON POTerm.PaymentTermID = PO.TermID";
			if (from != DateTime.MinValue)
			{
				text3 = text3 + " WHERE PR.TransactionDate Between '" + text + "' AND '" + text2 + "'";
			}
			if (!showVoid)
			{
				text3 += " AND ISNULL(PR.IsVoid,'False')='False'";
			}
			text3 += " ORDER BY PR.SysDocID,PR.VoucherID";
			SqlCommand sqlCommand = new SqlCommand(text3);
			FillDataSet(dataSet, "Payment_Request", sqlCommand);
			return dataSet;
		}

		public DataSet GetOpenPaymentRequests(int typeID)
		{
			try
			{
				DataSet dataSet = new DataSet();
				SqlCommand sqlCommand = new SqlCommand("SELECT SysDocID [DocID],VoucherID AS Number,TransactionDate,PayFromID,\r\n                            (CASE TypeID\r\n\t\t\t\t\t\t                            WHEN 1 THEN Chequebook.ChequebookName\r\n\t\t\t\t\t\t                            WHEN 2 THEN Account.AccountName\r\n\t\t\t\t\t\t                            WHEN 3 THEN Bank_Facility.FacilityName\r\n\t\t\t\t\t\t                            ELSE 'None' END) AS PayFromName,\r\n                            PR.CurrencyID AS Cur,PayeeID,ISNULL(AmountFC,Amount) AS Amount FROM Payment_Request PR\r\n                            LEFt OUTER JOIN Account ON PR.PayFromID=Account.AccountID \r\n\t\t\t\t\t\t                            LEFt OUTER JOIN Chequebook ON PR.PayFromID=Chequebook.ChequebookID\r\n\t\t\t\t\t\t                            LEFt OUTER JOIN Bank_Facility ON PR.PayFromID=Bank_Facility.FacilityID  \r\n\t\t\t\t\t\t                            INNER JOIN Vendor V ON V.VendorID  = PR.PayeeID\r\n                            WHERE TypeID = " + typeID + " AND PR.Status = 1 AND ISNULL(IsVoid,'False')='False' ");
				FillDataSet(dataSet, "Payment_Request", sqlCommand);
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
