using Micromind.Common;
using Micromind.Common.Data;
using Micromind.Common.Libraries;
using System;
using System.Data;
using System.Data.SqlClient;

namespace Micromind.Data
{
	public sealed class TRApplication : StoreObject
	{
		private const string SYSDOCID_PARM = "@SysDocID";

		private const string VOUCHERID_PARM = "@VoucherID";

		private const string COSTCENTERID_PARM = "@CostCenterID";

		private const string CURRENCYID_PARM = "@CurrencyID";

		private const string CURRENCYRATE_PARM = "@CurrencyRate";

		private const string TRANSACTIONDATE_PARM = "@TransactionDate";

		private const string PAYEETYPE_PARM = "@PayeeType";

		private const string PAYEEID_PARM = "@PayeeID";

		private const string DESCRIPTION_PARM = "@Description";

		private const string BANKFACILITYID_PARM = "@BankFacilityID";

		private const string REFERENCE_PARM = "@Reference";

		private const string DUEDATE_PARM = "@DueDate";

		private const string AMOUNT_PARM = "@Amount";

		private const string AMOUNTFC_PARM = "@AmountFC";

		private const string REQUESTSYSDOCID_PARM = "@RequestSysDocID";

		private const string REQUESTVOUCHERID_PARM = "@RequestVoucherID";

		private const string POSYSDOCID_PARM = "@POSysDocID";

		private const string POVOUCHERID_PARM = "@POVoucherID";

		private const string TRAPPLICATION_TABLE = "TR_Application";

		private const string INVOICENO_PARM = "@InvoiceNos";

		private const string AUTHORIZEDBY_PARM = "@Authorizedby";

		private const string NOOFINVOICES_PARM = "@NoofInvoices";

		private const string NOOFPL_PARM = "@NoofPL";

		private const string NOOFBOL_PARM = "@NoofBOL";

		private const string NOOFGOODS_PARM = "@NoofGoods";

		private const string NOTE_PARM = "@Note";

		private const string CREATEDBY_PARM = "@CreatedBy";

		private const string DATECREATED_PARM = "@DateCreated";

		private const string UPDATEDBY_PARM = "@UpdatedBy";

		private const string DATEUPDATED_PARM = "@DateUpdated";

		private static object syncRoot = new object();

		public TRApplication(Config config)
			: base(config)
		{
		}

		private string GetInsertUpdateTransactionText(bool isUpdate)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddInsertUpdateParameters("TR_Application", new FieldValue("SysDocID", "@SysDocID", isUpdateConditionField: true), new FieldValue("VoucherID", "@VoucherID", isUpdateConditionField: true), new FieldValue("CostCenterID", "@CostCenterID"), new FieldValue("BankFacilityID", "@BankFacilityID"), new FieldValue("PayeeType", "@PayeeType"), new FieldValue("PayeeID", "@PayeeID"), new FieldValue("TransactionDate", "@TransactionDate"), new FieldValue("Amount", "@Amount"), new FieldValue("AmountFC", "@AmountFC"), new FieldValue("CurrencyID", "@CurrencyID"), new FieldValue("CurrencyRate", "@CurrencyRate"), new FieldValue("Reference", "@Reference"), new FieldValue("Description", "@Description"), new FieldValue("DueDate", "@DueDate"), new FieldValue("POSysDocID", "@POSysDocID"), new FieldValue("POVoucherID", "@POVoucherID"), new FieldValue("InvoiceNos", "@InvoiceNos"), new FieldValue("Authorizedby", "@Authorizedby"), new FieldValue("NoofInvoices", "@NoofInvoices"), new FieldValue("NoofPL", "@NoofPL"), new FieldValue("NoofBOL", "@NoofBOL"), new FieldValue("NoofGoods", "@NoofGoods"), new FieldValue("Note", "@Note"), new FieldValue("RequestSysDocID", "@RequestSysDocID"), new FieldValue("RequestVoucherID", "@RequestVoucherID"));
			if (isUpdate)
			{
				sqlBuilder.AddInsertUpdateParameters("TR_Application", new FieldValue("DateUpdated", "@DateUpdated", isUpdateConditionField: true, checkForNullValue: true));
			}
			if (isUpdate)
			{
				return sqlBuilder.GetUpdateExpression();
			}
			return sqlBuilder.GetInsertExpression();
		}

		private SqlCommand GetInsertUpdateTransactionCommand(bool isUpdate)
		{
			SqlParameterCollection parameters;
			if (isUpdate)
			{
				if (updateCommand != null)
				{
					updateCommand = null;
				}
				updateCommand = new SqlCommand(GetInsertUpdateTransactionText(isUpdate: true), base.DBConfig.Connection);
				updateCommand.CommandType = CommandType.Text;
				parameters = updateCommand.Parameters;
			}
			else
			{
				if (insertCommand != null)
				{
					insertCommand = null;
				}
				insertCommand = new SqlCommand(GetInsertUpdateTransactionText(isUpdate: false), base.DBConfig.Connection);
				insertCommand.CommandType = CommandType.Text;
				parameters = insertCommand.Parameters;
			}
			parameters.Add("@CostCenterID", SqlDbType.NVarChar);
			parameters.Add("@SysDocID", SqlDbType.NVarChar);
			parameters.Add("@BankFacilityID", SqlDbType.NVarChar);
			parameters.Add("@PayeeType", SqlDbType.NVarChar);
			parameters.Add("@PayeeID", SqlDbType.NVarChar);
			parameters.Add("@VoucherID", SqlDbType.NVarChar);
			parameters.Add("@TransactionDate", SqlDbType.DateTime);
			parameters.Add("@Amount", SqlDbType.Money);
			parameters.Add("@AmountFC", SqlDbType.Money);
			parameters.Add("@CurrencyID", SqlDbType.NVarChar);
			parameters.Add("@CurrencyRate", SqlDbType.Decimal);
			parameters.Add("@Reference", SqlDbType.NVarChar);
			parameters.Add("@Description", SqlDbType.NVarChar);
			parameters.Add("@DueDate", SqlDbType.DateTime);
			parameters.Add("@RequestSysDocID", SqlDbType.NVarChar);
			parameters.Add("@RequestVoucherID", SqlDbType.NVarChar);
			parameters.Add("@Note", SqlDbType.NVarChar);
			parameters.Add("@POSysDocID", SqlDbType.NVarChar);
			parameters.Add("@POVoucherID", SqlDbType.NVarChar);
			parameters.Add("@InvoiceNos", SqlDbType.NVarChar);
			parameters.Add("@Authorizedby", SqlDbType.NVarChar);
			parameters.Add("@NoofInvoices", SqlDbType.Int);
			parameters.Add("@NoofPL", SqlDbType.Int);
			parameters.Add("@NoofBOL", SqlDbType.Int);
			parameters.Add("@NoofGoods", SqlDbType.NVarChar);
			parameters["@CostCenterID"].SourceColumn = "CostCenterID";
			parameters["@SysDocID"].SourceColumn = "SysDocID";
			parameters["@PayeeType"].SourceColumn = "PayeeType";
			parameters["@PayeeID"].SourceColumn = "PayeeID";
			parameters["@BankFacilityID"].SourceColumn = "BankFacilityID";
			parameters["@DueDate"].SourceColumn = "DueDate";
			parameters["@VoucherID"].SourceColumn = "VoucherID";
			parameters["@TransactionDate"].SourceColumn = "TransactionDate";
			parameters["@Amount"].SourceColumn = "Amount";
			parameters["@AmountFC"].SourceColumn = "AmountFC";
			parameters["@CurrencyID"].SourceColumn = "CurrencyID";
			parameters["@CurrencyRate"].SourceColumn = "CurrencyRate";
			parameters["@Reference"].SourceColumn = "Reference";
			parameters["@Description"].SourceColumn = "Description";
			parameters["@POSysDocID"].SourceColumn = "POSysDocID";
			parameters["@POVoucherID"].SourceColumn = "POVoucherID";
			parameters["@RequestSysDocID"].SourceColumn = "RequestSysDocID";
			parameters["@RequestVoucherID"].SourceColumn = "RequestVoucherID";
			parameters["@Note"].SourceColumn = "Note";
			parameters["@InvoiceNos"].SourceColumn = "InvoiceNos";
			parameters["@Authorizedby"].SourceColumn = "Authorizedby";
			parameters["@NoofInvoices"].SourceColumn = "NoofInvoices";
			parameters["@NoofPL"].SourceColumn = "NoofPL";
			parameters["@NoofBOL"].SourceColumn = "NoofBOL";
			parameters["@NoofGoods"].SourceColumn = "NoofGoods";
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

		public bool InsertUpdateTRApplication(TRApplicationData applicationData, bool isUpdate)
		{
			bool flag = true;
			SqlCommand insertUpdateTransactionCommand = GetInsertUpdateTransactionCommand(isUpdate);
			try
			{
				DataRow dataRow = applicationData.TRApplicationTable.Rows[0];
				int currencyDecimalPoints = new CompanyInformations(base.DBConfig).CurrencyDecimalPoints;
				SysDocTypes sysDocTypes = (SysDocTypes)byte.Parse(dataRow["SysDocType"].ToString());
				SqlTransaction sqlTransaction = base.DBConfig.StartNewTransaction();
				string text = dataRow["VoucherID"].ToString();
				string sysDocID = dataRow["SysDocID"].ToString();
				if (!isUpdate && new SystemDocuments(base.DBConfig).ExistDocumentNumber("TR_Application", "VoucherID", dataRow["SysDocID"].ToString(), text, sqlTransaction))
				{
					base.DBConfig.EndTransaction(result: false);
					throw new CompanyException("Document number already exist.", 1046);
				}
				string text2 = dataRow["PayeeID"].ToString();
				string a = dataRow["PayeeType"].ToString();
				string text3 = "";
				if (a == "C")
				{
					text3 = new Customers(base.DBConfig).GetCustomerARAccountID(sysDocID, text2);
				}
				else if (a == "V")
				{
					text3 = new Vendors(base.DBConfig).GetVendorAPAccountID(sysDocID, text2);
				}
				else if (a == "E")
				{
					text3 = new Employees(base.DBConfig).GetEmployeeAccountID(sysDocID, text2);
				}
				else if (a == "A")
				{
					text3 = text2;
				}
				if (text3 == "")
				{
					throw new CompanyException("AccountID should be selected for the payee if a payee is in transaction.", 1028);
				}
				dataRow["AccountID"] = text3;
				if (sysDocTypes == SysDocTypes.TR)
				{
					foreach (DataRow row in applicationData.TRApplicationTable.Rows)
					{
						row["SysDocID"] = dataRow["SysDocID"];
						row["VoucherID"] = dataRow["VoucherID"];
					}
				}
				if (dataRow["CurrencyID"] != DBNull.Value && dataRow["CurrencyID"].ToString() != GetBaseCurrencyID())
				{
					decimal d = decimal.Parse(dataRow["CurrencyRate"].ToString());
					decimal num = default(decimal);
					string text4 = "M";
					text4 = new Currencies(base.DBConfig).GetCurrencyRateType(dataRow["CurrencyID"].ToString());
					foreach (DataRow row2 in applicationData.TRApplicationTable.Rows)
					{
						decimal result = default(decimal);
						decimal.TryParse(row2["AmountFC"].ToString(), out result);
						if (text4 == "M")
						{
							row2["Amount"] = Math.Round(result * d, currencyDecimalPoints);
							num += Math.Round(result * d, currencyDecimalPoints);
						}
						else
						{
							row2["Amount"] = Math.Round(result / d, currencyDecimalPoints);
							num += Math.Round(result / d, currencyDecimalPoints);
						}
					}
					dataRow["Amount"] = num;
				}
				insertUpdateTransactionCommand.Transaction = sqlTransaction;
				if (!isUpdate)
				{
					if (applicationData.Tables["TR_Application"].Rows.Count > 0)
					{
						flag &= Insert(applicationData, "TR_Application", insertUpdateTransactionCommand);
					}
				}
				else
				{
					flag &= Update(applicationData, "TR_Application", insertUpdateTransactionCommand);
				}
				GLData journalData = CreateGLData(applicationData, sqlTransaction);
				flag &= InsertAPJournalForApplication(journalData, isUpdate, sqlTransaction);
				string text5 = dataRow["RequestSysDocID"].ToString();
				string text6 = dataRow["RequestVoucherID"].ToString();
				if (text5 != "" && text6 != "")
				{
					string exp = "UPDATE Payment_Request Set Status = 2 WHERE SysDocID = '" + text5 + "' AND VoucherID = '" + text6 + "'";
					flag &= (ExecuteNonQuery(exp, sqlTransaction) > 0);
				}
				if (sysDocTypes == SysDocTypes.TRApplication && applicationData.Tables.Contains("AP_Payment_Advice") && applicationData.Tables["AP_Payment_Advice"].Rows.Count > 0)
				{
					flag &= new APJournal(base.DBConfig).InsertPaymentAdviceTRApplication(applicationData);
				}
				if (!flag)
				{
					throw new Exception("Failed to save the transaction because of an unexpected error. Please try again.");
				}
				flag &= UpdateTableRowInsertUpdateInfo("TR_Application", "SysDocID", dataRow["SysDocID"].ToString(), "VoucherID", dataRow["VoucherID"].ToString(), sqlTransaction, !isUpdate);
				string entityName = "Tranaction";
				if (sysDocTypes == SysDocTypes.TRApplication)
				{
					entityName = "TR Application";
				}
				flag = (isUpdate ? (flag & AddActivityLog(entityName, text, sysDocID, ActivityTypes.Update, sqlTransaction)) : (flag & AddActivityLog(entityName, text, sysDocID, ActivityTypes.Add, sqlTransaction)));
				if (!isUpdate)
				{
					flag &= new SystemDocuments(base.DBConfig).UpdateNextDocumentNumber(dataRow["SysDocID"].ToString(), dataRow["VoucherID"].ToString(), "TR_Application", "VoucherID", sqlTransaction);
				}
				if (!flag)
				{
					return flag;
				}
				new Approval(base.DBConfig).CreateTransactionApprovalTasks(SysDocTypes.TRApplication, sysDocID, text, "TR_Application", sqlTransaction);
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

		public bool InsertAPJournalForApplication(GLData journalData, bool isUpdate, SqlTransaction sqlTransaction)
		{
			bool flag = true;
			try
			{
				DataRow dataRow = journalData.JournalTable.Rows[0];
				string sysDocID = dataRow["SysDocID"].ToString();
				string voucherID = dataRow["VoucherID"].ToString();
				if (isUpdate)
				{
					flag &= new APJournal(base.DBConfig).DeleteAPJournal(sysDocID, voucherID, sqlTransaction);
				}
				foreach (DataRow row in journalData.JournalDetailsTable.Rows)
				{
					APJournalData aPJournalData = new APJournalData();
					DataTable dataTable = aPJournalData.Tables["APJournal"];
					string vendorID = row["PayeeID"].ToString();
					DateTime invoiceDate = DateTime.Parse(dataRow["JournalDate"].ToString());
					string a = row["PayeeType"].ToString();
					bool result = false;
					bool.TryParse(row["IsARAP"].ToString(), out result);
					bool flag2 = false;
					if (row["Credit"] != DBNull.Value)
					{
						flag2 = true;
					}
					if (!((a != "V" || !result) | flag2))
					{
						DataRow dataRow3 = dataTable.NewRow();
						dataRow3["SysDocID"] = dataRow["SysDocID"];
						dataRow3["VoucherID"] = dataRow["VoucherID"];
						dataRow3["VendorID"] = row["PayeeID"];
						dataRow3["Reference"] = dataRow["Reference"];
						dataRow3["APAccountID"] = row["AccountID"];
						dataRow3["Debit"] = row["Debit"];
						dataRow3["Credit"] = row["Credit"];
						dataRow3["DebitFC"] = row["DebitFC"];
						dataRow3["CreditFC"] = row["CreditFC"];
						dataRow3["IsPDCRow"] = true;
						dataRow3["IsNonStatement"] = true;
						dataRow3["CostCenterID"] = row["CostCenterID"];
						dataRow3["CurrencyID"] = dataRow["CurrencyID"];
						dataRow3["CurrencyRate"] = dataRow["CurrencyRate"];
						dataRow3["Description"] = row["Description"];
						dataRow3["APDate"] = dataRow["JournalDate"];
						if (row["DueDate"] != DBNull.Value)
						{
							dataRow3["APDueDate"] = row["DueDate"];
						}
						else if (flag2)
						{
							dataRow3["APDueDate"] = new Vendors(base.DBConfig).CalculateDueDate(invoiceDate, vendorID, sqlTransaction);
						}
						dataRow3["PaymentMethodType"] = row["PaymentMethodType"];
						dataRow3["BankID"] = row["BankID"];
						dataRow3["ChequeDate"] = row["CheckDate"];
						dataRow3["ChequeNumber"] = row["CheckNumber"];
						dataRow3.EndEdit();
						dataTable.Rows.Add(dataRow3);
						flag &= new APJournal(base.DBConfig).InsertJournal(aPJournalData, sqlTransaction);
					}
				}
				return flag;
			}
			catch
			{
				flag = false;
				throw;
			}
		}

		private GLData CreateGLData(TRApplicationData transactionData, SqlTransaction sqlTransaction)
		{
			GLData gLData = new GLData();
			DataRow dataRow = transactionData.TRApplicationTable.Rows[0];
			_ = transactionData.TRApplicationTable.Rows[0];
			string text = dataRow["CurrencyID"].ToString();
			decimal d = decimal.Parse(dataRow["CurrencyRate"].ToString());
			string text2 = "M";
			text2 = new Currencies(base.DBConfig).GetCurrencyRateType(text);
			int currencyDecimalPoints = new CompanyInformations(base.DBConfig).CurrencyDecimalPoints;
			DataRow dataRow2 = gLData.JournalTable.NewRow();
			SysDocTypes sysDocTypes = (SysDocTypes)byte.Parse(dataRow["SysDocType"].ToString());
			dataRow2["JournalID"] = 0;
			dataRow2["JournalDate"] = dataRow["TransactionDate"];
			dataRow2["SysDocID"] = dataRow["SysDocID"];
			dataRow2["SysDocType"] = dataRow["SysDocType"];
			dataRow2["VoucherID"] = dataRow["VoucherID"];
			dataRow2["Reference"] = dataRow["Reference"];
			dataRow2["CurrencyID"] = dataRow["CurrencyID"];
			dataRow2["CurrencyRate"] = dataRow["CurrencyRate"];
			dataRow2["Note"] = dataRow["Description"];
			dataRow2.EndEdit();
			gLData.JournalTable.Rows.Add(dataRow2);
			decimal num = default(decimal);
			foreach (DataRow row in transactionData.TRApplicationTable.Rows)
			{
				decimal num2 = default(decimal);
				if (false)
				{
					num += decimal.Parse(row["Amount"].ToString());
					num += num2;
				}
			}
			DataRow dataRow4 = gLData.JournalDetailsTable.NewRow();
			dataRow4.BeginEdit();
			dataRow4["JournalID"] = 0;
			dataRow4["PayeeType"] = dataRow["PayeeType"];
			dataRow4["PayeeID"] = dataRow["PayeeID"];
			dataRow4["AccountID"] = dataRow["AccountID"];
			if (dataRow["PayeeType"] != DBNull.Value)
			{
				dataRow4["IsARAP"] = true;
			}
			if (sysDocTypes == SysDocTypes.TRApplication)
			{
				dataRow4["Debit"] = dataRow["Amount"];
				dataRow4["DebitFC"] = dataRow["AmountFC"];
				dataRow4["Credit"] = DBNull.Value;
				dataRow4["CreditFC"] = DBNull.Value;
			}
			dataRow4["Description"] = dataRow["Description"];
			dataRow4["Reference"] = dataRow["Reference"];
			dataRow4["CostCenterID"] = dataRow["CostCenterID"];
			dataRow4["RowIndex"] = -1;
			dataRow4.EndEdit();
			gLData.JournalDetailsTable.Rows.Add(dataRow4);
			foreach (DataRow row2 in transactionData.TRApplicationTable.Rows)
			{
				dataRow4 = gLData.JournalDetailsTable.NewRow();
				dataRow4.BeginEdit();
				dataRow4["JournalID"] = 0;
				dataRow4["PayeeType"] = dataRow["PayeeType"];
				dataRow4["PayeeID"] = dataRow["PayeeID"];
				if (row2["AccountID"].IsDBNullOrEmpty())
				{
					throw new CompanyException("One or more accounts are not set for the transaction.");
				}
				dataRow4["AccountID"] = row2["AccountID"];
				if (sysDocTypes == SysDocTypes.TRApplication)
				{
					dataRow4["Debit"] = DBNull.Value;
					dataRow4["DebitFC"] = DBNull.Value;
					if (sysDocTypes == SysDocTypes.TRApplication)
					{
						decimal d2 = decimal.Parse(dataRow["Amount"].ToString());
						dataRow4["Credit"] = d2 + num;
						if (text != GetBaseCurrencyID())
						{
							decimal result = default(decimal);
							decimal.TryParse(row2["AmountFC"].ToString(), out result);
							if (text2 == "M")
							{
								result += Math.Round(num / d, currencyDecimalPoints);
							}
							else
							{
								result += Math.Round(num * d, currencyDecimalPoints);
							}
							dataRow4["CreditFC"] = result;
						}
						else
						{
							dataRow4["CreditFC"] = row2["AmountFC"];
						}
					}
					else
					{
						dataRow4["Credit"] = row2["Amount"];
						dataRow4["CreditFC"] = row2["AmountFC"];
					}
				}
				dataRow4["Description"] = row2["Description"];
				dataRow4["Reference"] = row2["Reference"];
				dataRow4["CostCenterID"] = row2["CostCenterID"];
				dataRow4.EndEdit();
				gLData.JournalDetailsTable.Rows.Add(dataRow4);
			}
			return gLData;
		}

		private string GetBaseCurrencyID()
		{
			string exp = "SELECT TOP 1 BaseCurrencyID FROM Company";
			object obj = ExecuteScalar(exp);
			if (obj != null && obj != DBNull.Value)
			{
				return obj.ToString();
			}
			return "";
		}

		public bool VoidTransaction(string sysDocID, string voucherID, bool isVoid)
		{
			bool flag = true;
			try
			{
				SqlTransaction sqlTransaction = base.DBConfig.StartNewTransaction();
				string exp = "SELECT IsVoid FROM TR_Application WHERE SysDocID='" + sysDocID + "' AND VoucherID='" + voucherID + "'";
				object obj = ExecuteScalar(exp, sqlTransaction);
				bool flag2 = false;
				if (obj != null && obj.ToString() != string.Empty)
				{
					flag2 = bool.Parse(obj.ToString());
				}
				if (isVoid == flag2)
				{
					if (isVoid)
					{
						throw new Exception("This transaction is already voided.");
					}
					throw new Exception("This transaction is not voided.");
				}
				if (flag)
				{
					exp = "UPDATE TR_Application SET IsVoid = '" + isVoid.ToString() + "' WHERE SysDocID='" + sysDocID + "' ";
					exp = exp + " AND VoucherID='" + voucherID + "'";
					flag &= (ExecuteNonQuery(exp, sqlTransaction) > 0);
				}
				flag &= new APJournal(base.DBConfig).VoidAPJournal(sysDocID, voucherID, isVoid, sqlTransaction);
				if (!flag)
				{
					return flag;
				}
				if (!isVoid)
				{
					AddActivityLog("Transaction", voucherID, sysDocID, ActivityTypes.Unvoid, sqlTransaction);
					return flag;
				}
				AddActivityLog("Transaction", voucherID, sysDocID, ActivityTypes.Void, sqlTransaction);
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

		public bool DeleteTransaction(string sysDocID, string voucherID)
		{
			bool flag = true;
			try
			{
				SqlTransaction sqlTransaction = base.DBConfig.StartNewTransaction();
				new SystemDocuments(base.DBConfig).GetSystemDocumentType(sysDocID, sqlTransaction);
				string exp = "DELETE FROM TR_Application WHERE SysDocID='" + sysDocID + "' AND VoucherID='" + voucherID + "'";
				flag &= (ExecuteNonQuery(exp, sqlTransaction) > 0);
				exp = "DELETE FROM AP_Payment_Advice WHERE PaymentSysDocID='" + sysDocID + "' AND PaymentVoucherID='" + voucherID + "' AND IsDraft=1";
				flag &= Delete(exp, sqlTransaction);
				flag &= new Journal(base.DBConfig).DeleteJournal(sysDocID, voucherID, sqlTransaction);
				flag &= new SystemDocuments(base.DBConfig).UpdateNextToDeletedDocumentNumber(sysDocID, voucherID, "TR_Application", "VoucherID", sqlTransaction);
				if (!flag)
				{
					return flag;
				}
				AddActivityLog("Transaction", voucherID, sysDocID, ActivityTypes.Delete, sqlTransaction);
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

		public TRApplicationData GetTransactionByID(string sysDocID, string voucherID)
		{
			try
			{
				TRApplicationData tRApplicationData = new TRApplicationData();
				string textCommand = "SELECT * FROM TR_Application WHERE VoucherID='" + voucherID + "' AND SysDocID='" + sysDocID + "'";
				FillDataSet(tRApplicationData, "TR_Application", textCommand);
				if (tRApplicationData == null || tRApplicationData.Tables.Count == 0 || tRApplicationData.Tables["TR_Application"].Rows.Count == 0)
				{
					return null;
				}
				textCommand = "SELECT *,Cast(null AS datetime)AS AllocationDate FROM [AP_Payment_Advice] WHERE \r\n                                PaymentSysDocID='" + sysDocID + "' AND PaymentVoucherID = '" + voucherID + "'";
				FillDataSet(tRApplicationData, "AP_Payment_Advice", textCommand);
				return tRApplicationData;
			}
			catch
			{
				throw;
			}
		}

		public DataSet GetTRApplicationToPrint(string sysDocID, string[] voucherID)
		{
			try
			{
				DataSet dataSet = new DataSet();
				string text = "";
				for (int i = 0; i < voucherID.Length; i++)
				{
					text = "'" + voucherID[i] + "'";
					if (i < voucherID.Length - 1)
					{
						text += ",";
					}
				}
				string textCommand = "SELECT GLT.SysDocID,GLT.VoucherID,CostCenterID,PayeeType,PayeeID,'' SecondRegisterID,\r\n                            (CASE PayeeType\r\n                            WHEN 'C' THEN Customer.CustomerName\r\n                            WHEN 'V' THEN Vendor.VendorName\r\n                            WHEN 'E' THEN Emp2.FirstName + ' ' + Emp2.LastName\r\n                            ELSE Account.AccountName END) AS PayeeName,\r\n                            ISNULL(AmountFC,Amount) AS Amount,GLT.TransactionDate,GLT.IsVoid,GLT.CurrencyRate, '' ChequebookID, ''CheckNumber,\r\n                            '' CheckDate,GLT.Reference,Description,\r\n                            ISNULL(GLT.CurrencyID,(SELECT CurrencyID FROM Currency WHERE IsBase='True')) AS CurrencyID ,BankFacilityID,FacilityName,GLT.POSysDocID,\r\n                            GLT.POVoucherID,PO.CurrencyID, PO.TermID,PO.Total, PT1.TermName AS PayeeTerm,0.0 AS PayeeBalance,0.0 AS PayeeTotalDue,0.0 AS PurchaseClaim,0.0 as QualityClaim,0.0 AS PCCount,0.0 as QCCount,0.0 as AvailableBal\r\n                            FROM TR_Application GLT \r\n                            LEFT OUTER JOIN Account ON GLT.PayeeID=Account.AccountID \r\n                            LEFt OUTER JOIN Customer ON GLT.PayeeID=Customer.CustomerID \r\n                            LEFt OUTER JOIN Vendor ON GLT.PayeeID=Vendor.VendorID \r\n                            LEFt OUTER JOIN Employee Emp2 ON GLT.PayeeID=Emp2.EmployeeID \r\n                            LEFT OUTER JOIN Bank_Facility BF ON BF.FacilityID=GLT.BankFacilityID \r\n                            LEFT OUTER JOIN Payment_Term PT1 ON PT1.PaymentTermID = Vendor.PaymentTermID\r\n\t\t\t\t\t\t\tLEFT OUTER JOIN Purchase_Order PO ON PO.SysDocID=GLT.POSysDocID AND po.VoucherID=GLT.POVoucherID  WHERE GLT.SysDocID = '" + sysDocID + "' AND GLT.VoucherID IN (" + text + ")";
				FillDataSet(dataSet, "TR_Application", textCommand);
				if (dataSet == null || dataSet.Tables.Count == 0 || dataSet.Tables["TR_Application"].Rows.Count == 0)
				{
					return null;
				}
				textCommand = "SELECT DISTINCT PA.*,(SELECT P.DueDate FROM Purchase_Invoice P WHERE P.SysDocID=PA.InvoiceSysDocID AND P.VoucherID=PA.InvoiceVoucherID) AS [InvDue],\r\n                        (SELECT P.Reference FROM Purchase_Invoice P WHERE P.SysDocID = PA.InvoiceSysDocID AND P.VoucherID = PA.InvoiceVoucherID) AS[PIReference],\r\n                        (SELECT P.Reference2 FROM Purchase_Invoice P WHERE P.SysDocID = PA.InvoiceSysDocID AND P.VoucherID = PA.InvoiceVoucherID) AS[PIReference2]\r\n                        ,NULL as  [POSysDocID],NULL as [POVoucherID],Null AS Total,NULL AS TermID,0.00 AS[BalanceAmount]\r\n                        FROM AP_Payment_Advice PA INNER JOIN System_Document SD ON SD.SysDocID = InvoiceSysDocID\r\n                        where PA.PaymentSysDocID ='" + sysDocID + "' AND PA.PaymentVoucherID IN (" + text + ")  AND SD.SysDocType NOT IN(244,39)\r\n                         UNION ALL\r\n \r\n                           SELECT  PA.*, PPI.DueDate AS[InvDue], '' AS PIReference,\r\n                         '' AS PIReference2, PPI.SourceSysDocID as  [POSysDocID], PPI.SourceVoucherID as [POVoucherID], PPI.POAmount, PPI.PrepaymentTermID,\r\n                         PA.AmountDue - PA.PaymentAmount as [BalanceAmount] FROM AP_Payment_Advice PA INNER JOIN\r\n                         Purchase_Prepayment_Invoice PPI ON PA.InvoiceSysDocID = PPI.SysDocID AND PA.InvoiceVoucherID = PPI.VoucherID\r\n                         where PA.PaymentSysDocID = '" + sysDocID + "' AND PA.PaymentVoucherID IN(" + text + ")\r\n                         UNION\r\n                         SELECT DISTINCT PA.*, (SELECT P.DueDate FROM Purchase_Invoice P WHERE P.SysDocID = PA.InvoiceSysDocID AND P.VoucherID = PA.InvoiceVoucherID) AS[InvDue],\r\n                        (SELECT P.Reference FROM Purchase_Invoice P WHERE P.SysDocID = PA.InvoiceSysDocID AND P.VoucherID = PA.InvoiceVoucherID) AS[PIReference],\r\n                        (SELECT P.Reference2 FROM Purchase_Invoice P WHERE P.SysDocID = PA.InvoiceSysDocID AND P.VoucherID = PA.InvoiceVoucherID) AS[PIReference2]\r\n                        ,PO.SysDocID as  [POSysDocID],PO.VoucherID as [POVoucherID],PO.Total,PO.TermID,PA.AmountDue - PA.PaymentAmount as [BalanceAmount]\r\n                        FROM AP_Payment_Advice PA INNER JOIN\r\n                        Purchase_Receipt PR ON PA.InvoiceSysDocID = PR.InvoiceSysDocID AND PA.InvoiceVoucherID = PR.InvoiceVoucherID\r\n                        LEFT JOIN Purchase_Receipt_detail PRD ON PRD.SysDocID = PR.SysDocID AND PRD.VoucherID = PR.VoucherID\r\n                        LEFT JOIN PO_Shipment_Detail PSD ON PSD.SysDocID = PRD.PKSysDocID AND PSD.VoucherID = PRD.PKVoucherID\r\n                        LEFT JOIN Purchase_Order PO ON PO.SysDocID = PSD.SourceSysDocID AND PO.VoucherID = PSD.SourceVoucherID\r\n                        where PA.PaymentSysDocID = '" + sysDocID + "' AND PA.PaymentVoucherID IN (" + text + ")";
				FillDataSet(dataSet, "AP_Payment_Advice", textCommand);
				textCommand = "SELECT DISTINCT SI.*,V.VendorName,V.TaxIDNumber as VTaxIDNo,B.FullName,B.Phone1 AS BPhone,B.Mobile AS BMobile,                             \r\n                        VA.AddressPrintFormat AS VendorAddress,VA.Phone1,VA.Fax,VA.Mobile,VA.ContactName,PT.TermName,SM.ShippingMethodName,\r\n                        ISNULL(SI.CurrencyID,(SELECT CurrencyID FROM Currency WHERE IsBase='True')) AS CurrencyID,\r\n                        TermName,ISNULL(SI.Total,0) - ISNULL(SI.Discount,0) AS GrandTotal,\r\n                        ISNULL(SI.TaxAmount ,0) AS Tax,SI.Total AS Total                  \r\n                        FROM  Purchase_Order SI INNER JOIN Vendor V ON SI.VendorID=V.VendorID\r\n                        LEFT OUTER JOIN Payment_Term PT ON SI.TermID=PT.PaymentTermID\r\n                        LEFT OUTER JOIN Vendor_Address VA ON VA.VendorID=SI.VendorID AND VA.AddressID='PRIMARY'\r\n                        LEFT OUTER JOIN Shipping_Method SM ON SM.ShippingMethodID=SI.ShippingMethodID\r\n                        LEFT OUTER JOIN Buyer B ON B.BuyerID=SI.BuyerID\r\n                        LEFT JOIN PO_Shipment_Detail PSD ON SI.SysDocID=PSD.SourceSysDocID AND SI.VoucherID=PSD.SourceVoucherID\r\n                        LEFT JOIN Purchase_Receipt_detail PRD ON PSD.SysDocID=PRD.PKSysDocID AND PSD.VoucherID=PRD.PKVoucherID\r\n\r\n                        LEFT JOIN Purchase_Receipt PR ON PR.SysDocID=PRD.SysDocID AND PR.VoucherID=PRD.VoucherID\r\n                        LEFT JOIN AP_Payment_Advice PA ON  PA.InvoiceSysDocID=PR.InvoiceSysDocID AND PA.InvoiceVoucherID=PR.InvoiceVoucherID                               \r\n                        WHERE PA.PaymentSysDocID = '" + sysDocID + "' AND PA.PaymentVoucherID IN (" + text + ")";
				FillDataSet(dataSet, "Purchase_Order", textCommand);
				dataSet.Tables["TR_Application"].Columns.Add("AmountInWords", typeof(string));
				foreach (DataRow row in dataSet.Tables["TR_Application"].Rows)
				{
					decimal result = default(decimal);
					decimal.TryParse(row["Amount"].ToString(), out result);
					row["AmountInWords"] = NumToWord.GetNumInWords(result);
				}
				dataSet.Relations.Add("PO_Relation", new DataColumn[2]
				{
					dataSet.Tables["TR_Application"].Columns["SysDocID"],
					dataSet.Tables["TR_Application"].Columns["VoucherID"]
				}, new DataColumn[2]
				{
					dataSet.Tables["AP_Payment_Advice"].Columns["PaymentSysDocID"],
					dataSet.Tables["AP_Payment_Advice"].Columns["PaymentVoucherID"]
				}, createConstraints: false);
				return dataSet;
			}
			catch
			{
				throw;
			}
		}

		public DataSet GetList(DateTime from, DateTime to, BankFacilityTypes facilityType, bool showVoid)
		{
			DataSet dataSet = new DataSet();
			string text = StoreConfiguration.ToSqlDateTimeString(from);
			string text2 = StoreConfiguration.ToSqlDateTimeString(to);
			string text3 = "SELECT TR.SysDocID,TR.VoucherID,TR.TransactionDate,TR.BankFacilityID,TR.PayeeType,TR.PayeeID,BFA.SysDocID+'-'+ BFA.VoucherID AS [Entry Ref], TR.Reference,TR.Amount, TR.AmountFC\r\n                            FROM TR_Application TR\r\n                            LEFT OUTER JOIN Bank_Facility_Transaction BFA ON BFA.SourceSysDocID=TR.SysDocID and BFA.SourceVoucherID=TR.VoucherID \r\n                            WHERE 1=1 ";
			if (from != DateTime.MinValue)
			{
				text3 = text3 + " AND TR.TransactionDate Between '" + text + "' AND '" + text2 + "'";
			}
			if (!showVoid)
			{
				text3 += " AND ISNULL(TR.IsVoid,'False')='False' ";
			}
			SqlCommand sqlCommand = new SqlCommand(text3);
			FillDataSet(dataSet, "TR_Application", sqlCommand);
			return dataSet;
		}

		public DataSet GetOpenTransactions(string filter)
		{
			DataSet dataSet = new DataSet();
			string textCommand = "SELECT  SysDocID,VoucherID,PayeeID,BankFacilityID,Reference,BF.CurrentAccountID,BF.PayableAccountID,CurrencyID, ISNULL(AmountFC,Amount) AS Amount, Amount AS AmountLC,Amount\r\n                                    FROM TR_Application BFT INNER JOIN Bank_Facility BF  ON BF.FacilityID=BFT.BankFacilityID WHERE ISNULL(IsVoid,'False')='False' AND VoucherID NOT IN ( SELECT  SourceVoucherID from Bank_Facility_Transaction\r\n                                    TR INNER JOIN TR_Application TRA ON TR.SourceSysDocID=TRA.SysDocID AND  TR.SourceVoucherID=TRA.VoucherID  )";
			FillDataSet(dataSet, "TR_Application", textCommand);
			return dataSet;
		}

		public bool AllowModify(string sysDocID, string voucherNumber)
		{
			string exp = "select count(*) From TR_Application TRA INNER JOIN Bank_Facility_Transaction TR ON TRA.VoucherID=TR.SourceVoucherID AND TRA.SysDocID=TR.SourceSysDocID WHERE TRA.SysDocID='" + sysDocID + "' AND TRA.VoucherID = '" + voucherNumber + "'";
			object obj = ExecuteScalar(exp);
			if (obj != null && obj.ToString() != "" && decimal.Parse(obj.ToString()) != 0m)
			{
				return false;
			}
			return true;
		}
	}
}
