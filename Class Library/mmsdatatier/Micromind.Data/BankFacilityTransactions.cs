using Micromind.Common;
using Micromind.Common.Data;
using Micromind.Common.Libraries;
using System;
using System.Data;
using System.Data.SqlClient;

namespace Micromind.Data
{
	public sealed class BankFacilityTransactions : StoreObject
	{
		private const string SYSDOCID_PARM = "@SysDocID";

		private const string REGISTERID_PARM = "@RegisterID";

		private const string VOUCHERID_PARM = "@VoucherID";

		private const string AMOUNT_PARM = "@Amount";

		private const string AMOUNTFC_PARM = "@AmountFC";

		private const string TRANSACTIONDATE_PARM = "@TransactionDate";

		private const string CURRENCYID_PARM = "@CurrencyID";

		private const string CURRENCYRATE_PARM = "@CurrencyRate";

		private const string JOURNALID_PARM = "@JournalID";

		private const string REFERENCE_PARM = "@Reference";

		private const string DESCRIPTION_PARM = "@Description";

		private const string GLTYPE_PARM = "@GLType";

		private const string STATUS_PARM = "@TransactionStatus";

		private const string GLTYPEID_PARM = "@GLTYPEID";

		private const string FIRSTACCOUNTID_PARM = "@FirstAccountID";

		private const string SECONDACCOUNTID_PARM = "@SecondAccountID";

		private const string EMPLOYEEID_PARM = "@EmployeeID";

		private const string BANKFACILITYID_PARM = "@BankFacilityID";

		private const string FACILITYTYPE_PARM = "@FacilityType";

		private const string REQUESTSYSDOCID_PARM = "@RequestSysDocID";

		private const string REQUESTVOUCHERID_PARM = "@RequestVoucherID";

		private const string SOURCESYSDOCID_PARM = "@SourceSysDocID";

		private const string SOURCEVOUCHERID_PARM = "@SourceVoucherID";

		private const string BANKFACILITYTRANSACTION_TABLE = "Bank_Facility_Transaction";

		private const string CREATEDBY_PARM = "@CreatedBy";

		private const string DATECREATED_PARM = "@DateCreated";

		private const string UPDATEDBY_PARM = "@UpdatedBy";

		private const string DATEUPDATED_PARM = "@DateUpdated";

		private const string ACCOUNTID_PARM = "@AccountID";

		private const string ROWINDEX_PARM = "@RowIndex";

		private const string PAYEEID_PARM = "@PayeeID";

		private const string PAYEETYPE_PARM = "@PayeeType";

		private const string ANALYSISID_PARM = "@AnalysisID";

		private const string COSTCENTERID_PARM = "@CostCenterID";

		private const string ISVOID_PARM = "@IsVoid";

		private const string PAYMENTMETHODID_PARM = "@PaymentMethodID";

		private const string PAYMENTMETHODTYPE_PARM = "@PaymentMethodType";

		private const string BANKID_PARM = "@BankID";

		private const string BANKFACILITYTRANSACTIONDETAILS_TABLE = "Bank_Facility_Transaction_Details";

		private const string DUEDATE_PARM = "@DueDate";

		private const string BANKFEEDETAILS_TABLE = "Bank_Fee_Details";

		private const string GLTRANSACTIONSYSDOCID_PARM = "@GLTransactionSysDocID";

		private const string GLTRANSACTIONVOUCHERID_PARM = "@GLTransactionVoucherID";

		private const string BANKACCOUNTID_PARM = "@BankAccountID";

		private const string EXPENSEACCOUNTID_PARM = "@ExpenseAccountID";

		private const string BANKFEEID_PARM = "@BankFeeID";

		private const string ISWITHTR_PARM = "@IsWithTR";

		private const string TAXGROUPID_PARM = "@TaxGroupID";

		private const string TAXAMOUNT_PARM = "@TaxAmount";

		private const string TAXOPTION_PARM = "@TaxOption";

		private const string TRANSACTIONNUMBER_PARM = "@TransactionNumber";

		private const string PARTYONEACCOUNTID_PARM = "@PartyOneAccountID";

		private const string PARTYTWOACCOUNTID_PARM = "@PartyTwoAccountID";

		private static object syncRoot = new object();

		public BankFacilityTransactions(Config config)
			: base(config)
		{
		}

		private string GetInsertUpdateTransactionText(bool isUpdate)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddInsertUpdateParameters("Bank_Facility_Transaction", new FieldValue("SysDocID", "@SysDocID", isUpdateConditionField: true), new FieldValue("VoucherID", "@VoucherID", isUpdateConditionField: true), new FieldValue("RegisterID", "@RegisterID"), new FieldValue("CostCenterID", "@CostCenterID"), new FieldValue("BankFacilityID", "@BankFacilityID"), new FieldValue("FacilityType", "@FacilityType"), new FieldValue("PayeeType", "@PayeeType"), new FieldValue("PayeeID", "@PayeeID"), new FieldValue("TransactionDate", "@TransactionDate"), new FieldValue("Amount", "@Amount"), new FieldValue("AmountFC", "@AmountFC"), new FieldValue("TaxAmount", "@TaxAmount"), new FieldValue("CurrencyID", "@CurrencyID"), new FieldValue("CurrencyRate", "@CurrencyRate"), new FieldValue("Reference", "@Reference"), new FieldValue("Description", "@Description"), new FieldValue("FirstAccountID", "@FirstAccountID"), new FieldValue("SecondAccountID", "@SecondAccountID"), new FieldValue("RequestSysDocID", "@RequestSysDocID"), new FieldValue("RequestVoucherID", "@RequestVoucherID"), new FieldValue("SourceSysDocID", "@SourceSysDocID"), new FieldValue("SourceVoucherID", "@SourceVoucherID"), new FieldValue("EmployeeID", "@EmployeeID"), new FieldValue("TransactionStatus", "@TransactionStatus"));
			if (isUpdate)
			{
				sqlBuilder.AddInsertUpdateParameters("Bank_Facility_Transaction", new FieldValue("DateUpdated", "@DateUpdated", isUpdateConditionField: true, checkForNullValue: true));
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
			parameters.Add("@RegisterID", SqlDbType.NVarChar);
			parameters.Add("@BankFacilityID", SqlDbType.NVarChar);
			parameters.Add("@FacilityType", SqlDbType.Int);
			parameters.Add("@PayeeType", SqlDbType.NVarChar);
			parameters.Add("@PayeeID", SqlDbType.NVarChar);
			parameters.Add("@VoucherID", SqlDbType.NVarChar);
			parameters.Add("@TransactionDate", SqlDbType.DateTime);
			parameters.Add("@Amount", SqlDbType.Money);
			parameters.Add("@AmountFC", SqlDbType.Money);
			parameters.Add("@TaxAmount", SqlDbType.Money);
			parameters.Add("@CurrencyID", SqlDbType.NVarChar);
			parameters.Add("@CurrencyRate", SqlDbType.SmallMoney);
			parameters.Add("@Reference", SqlDbType.NVarChar);
			parameters.Add("@Description", SqlDbType.NVarChar);
			parameters.Add("@TransactionStatus", SqlDbType.TinyInt);
			parameters.Add("@FirstAccountID", SqlDbType.NVarChar);
			parameters.Add("@SecondAccountID", SqlDbType.NVarChar);
			parameters.Add("@EmployeeID", SqlDbType.NVarChar);
			parameters.Add("@RequestSysDocID", SqlDbType.NVarChar);
			parameters.Add("@RequestVoucherID", SqlDbType.NVarChar);
			parameters.Add("@SourceSysDocID", SqlDbType.NVarChar);
			parameters.Add("@SourceVoucherID", SqlDbType.NVarChar);
			parameters["@CostCenterID"].SourceColumn = "CostCenterID";
			parameters["@SysDocID"].SourceColumn = "SysDocID";
			parameters["@RegisterID"].SourceColumn = "RegisterID";
			parameters["@PayeeType"].SourceColumn = "PayeeType";
			parameters["@PayeeID"].SourceColumn = "PayeeID";
			parameters["@BankFacilityID"].SourceColumn = "BankFacilityID";
			parameters["@FacilityType"].SourceColumn = "FacilityType";
			parameters["@VoucherID"].SourceColumn = "VoucherID";
			parameters["@TransactionDate"].SourceColumn = "TransactionDate";
			parameters["@Amount"].SourceColumn = "Amount";
			parameters["@AmountFC"].SourceColumn = "AmountFC";
			parameters["@TaxAmount"].SourceColumn = "TaxAmount";
			parameters["@CurrencyID"].SourceColumn = "CurrencyID";
			parameters["@CurrencyRate"].SourceColumn = "CurrencyRate";
			parameters["@Reference"].SourceColumn = "Reference";
			parameters["@Description"].SourceColumn = "Description";
			parameters["@TransactionStatus"].SourceColumn = "TransactionStatus";
			parameters["@FirstAccountID"].SourceColumn = "FirstAccountID";
			parameters["@SecondAccountID"].SourceColumn = "SecondAccountID";
			parameters["@EmployeeID"].SourceColumn = "EmployeeID";
			parameters["@RequestSysDocID"].SourceColumn = "RequestSysDocID";
			parameters["@RequestVoucherID"].SourceColumn = "RequestVoucherID";
			parameters["@SourceSysDocID"].SourceColumn = "SourceSysDocID";
			parameters["@SourceVoucherID"].SourceColumn = "SourceVoucherID";
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

		private string GetInsertUpdateTransactionDetailsText(bool isUpdate)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddInsertUpdateParameters("Bank_Facility_Transaction_Details", new FieldValue("SysDocID", "@SysDocID"), new FieldValue("VoucherID", "@VoucherID"), new FieldValue("BankID", "@BankID"), new FieldValue("Description", "@Description"), new FieldValue("Reference", "@Reference"), new FieldValue("Amount", "@Amount"), new FieldValue("AmountFC", "@AmountFC"), new FieldValue("AccountID", "@AccountID"), new FieldValue("CostCenterID", "@CostCenterID"), new FieldValue("AnalysisID", "@AnalysisID"), new FieldValue("RowIndex", "@RowIndex"), new FieldValue("PayeeID", "@PayeeID"), new FieldValue("BankFacilityID", "@BankFacilityID"), new FieldValue("DueDate", "@DueDate"), new FieldValue("PayeeType", "@PayeeType"));
			if (isUpdate)
			{
				return sqlBuilder.GetUpdateExpression();
			}
			return sqlBuilder.GetInsertExpression();
		}

		private SqlCommand GetInsertUpdateTransactionDetailsCommand(bool isUpdate)
		{
			SqlParameterCollection parameters;
			if (isUpdate)
			{
				if (updateCommand != null)
				{
					updateCommand = null;
				}
				updateCommand = new SqlCommand(GetInsertUpdateTransactionDetailsText(isUpdate: true), base.DBConfig.Connection);
				updateCommand.CommandType = CommandType.Text;
				parameters = updateCommand.Parameters;
			}
			else
			{
				if (insertCommand != null)
				{
					insertCommand = null;
				}
				insertCommand = new SqlCommand(GetInsertUpdateTransactionDetailsText(isUpdate: false), base.DBConfig.Connection);
				insertCommand.CommandType = CommandType.Text;
				parameters = insertCommand.Parameters;
			}
			parameters.Add("@SysDocID", SqlDbType.NVarChar);
			parameters.Add("@VoucherID", SqlDbType.NVarChar);
			parameters.Add("@Description", SqlDbType.NVarChar);
			parameters.Add("@BankID", SqlDbType.NVarChar);
			parameters.Add("@Reference", SqlDbType.NVarChar);
			parameters.Add("@Amount", SqlDbType.Money);
			parameters.Add("@AmountFC", SqlDbType.Money);
			parameters.Add("@AccountID", SqlDbType.NVarChar);
			parameters.Add("@CostCenterID", SqlDbType.NVarChar);
			parameters.Add("@AnalysisID", SqlDbType.NVarChar);
			parameters.Add("@RowIndex", SqlDbType.SmallInt);
			parameters.Add("@PayeeID", SqlDbType.NVarChar);
			parameters.Add("@BankFacilityID", SqlDbType.NVarChar);
			parameters.Add("@DueDate", SqlDbType.DateTime);
			parameters.Add("@PayeeType", SqlDbType.NVarChar);
			parameters["@SysDocID"].SourceColumn = "SysDocID";
			parameters["@VoucherID"].SourceColumn = "VoucherID";
			parameters["@Description"].SourceColumn = "Description";
			parameters["@BankID"].SourceColumn = "BankID";
			parameters["@Reference"].SourceColumn = "Reference";
			parameters["@Amount"].SourceColumn = "Amount";
			parameters["@AmountFC"].SourceColumn = "AmountFC";
			parameters["@AccountID"].SourceColumn = "AccountID";
			parameters["@CostCenterID"].SourceColumn = "CostCenterID";
			parameters["@AnalysisID"].SourceColumn = "AnalysisID";
			parameters["@RowIndex"].SourceColumn = "RowIndex";
			parameters["@PayeeID"].SourceColumn = "PayeeID";
			parameters["@BankFacilityID"].SourceColumn = "BankFacilityID";
			parameters["@DueDate"].SourceColumn = "DueDate";
			parameters["@PayeeType"].SourceColumn = "PayeeType";
			if (isUpdate)
			{
				return updateCommand;
			}
			return insertCommand;
		}

		private string GetInsertUpdateTransactionBankFeesText(bool isUpdate)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddInsertUpdateParameters("Bank_Fee_Details", new FieldValue("GLTransactionSysDocID", "@GLTransactionSysDocID"), new FieldValue("GLTransactionVoucherID", "@GLTransactionVoucherID"), new FieldValue("RowIndex", "@RowIndex"), new FieldValue("BankFacilityID", "@BankFacilityID"), new FieldValue("Description", "@Description"), new FieldValue("Reference", "@Reference"), new FieldValue("BankAccountID", "@BankAccountID"), new FieldValue("ExpenseAccountID", "@ExpenseAccountID"), new FieldValue("BankFeeID", "@BankFeeID"), new FieldValue("IsWithTR", "@IsWithTR"), new FieldValue("TaxOption", "@TaxOption"), new FieldValue("TaxAmount", "@TaxAmount"), new FieldValue("TaxGroupID", "@TaxGroupID"), new FieldValue("Amount", "@Amount"), new FieldValue("AmountFC", "@AmountFC"), new FieldValue("CurrencyID", "@CurrencyID"), new FieldValue("CurrencyRate", "@CurrencyRate"));
			if (isUpdate)
			{
				return sqlBuilder.GetUpdateExpression();
			}
			return sqlBuilder.GetInsertExpression();
		}

		private SqlCommand GetInsertUpdateTransactionBankFeesCommand(bool isUpdate)
		{
			SqlParameterCollection parameters;
			if (isUpdate)
			{
				if (updateCommand != null)
				{
					updateCommand = null;
				}
				updateCommand = new SqlCommand(GetInsertUpdateTransactionBankFeesText(isUpdate: true), base.DBConfig.Connection);
				updateCommand.CommandType = CommandType.Text;
				parameters = updateCommand.Parameters;
			}
			else
			{
				if (insertCommand != null)
				{
					insertCommand = null;
				}
				insertCommand = new SqlCommand(GetInsertUpdateTransactionBankFeesText(isUpdate: false), base.DBConfig.Connection);
				insertCommand.CommandType = CommandType.Text;
				parameters = insertCommand.Parameters;
			}
			parameters.Add("@GLTransactionSysDocID", SqlDbType.NVarChar);
			parameters.Add("@GLTransactionVoucherID", SqlDbType.NVarChar);
			parameters.Add("@RowIndex", SqlDbType.Int);
			parameters.Add("@BankFacilityID", SqlDbType.NVarChar);
			parameters.Add("@Description", SqlDbType.NVarChar);
			parameters.Add("@Reference", SqlDbType.NVarChar);
			parameters.Add("@BankAccountID", SqlDbType.NVarChar);
			parameters.Add("@ExpenseAccountID", SqlDbType.NVarChar);
			parameters.Add("@BankFeeID", SqlDbType.NVarChar);
			parameters.Add("@Amount", SqlDbType.Money);
			parameters.Add("@AmountFC", SqlDbType.Money);
			parameters.Add("@IsWithTR", SqlDbType.Bit);
			parameters.Add("@CurrencyID", SqlDbType.NVarChar);
			parameters.Add("@CurrencyRate", SqlDbType.Money);
			parameters.Add("@TaxOption", SqlDbType.TinyInt);
			parameters.Add("@TaxGroupID", SqlDbType.VarChar);
			parameters.Add("@TaxAmount", SqlDbType.Decimal);
			parameters["@GLTransactionSysDocID"].SourceColumn = "GLTransactionSysDocID";
			parameters["@GLTransactionVoucherID"].SourceColumn = "GLTransactionVoucherID";
			parameters["@RowIndex"].SourceColumn = "RowIndex";
			parameters["@BankFacilityID"].SourceColumn = "BankFacilityID";
			parameters["@Description"].SourceColumn = "Description";
			parameters["@Reference"].SourceColumn = "Reference";
			parameters["@BankAccountID"].SourceColumn = "BankAccountID";
			parameters["@ExpenseAccountID"].SourceColumn = "ExpenseAccountID";
			parameters["@BankFeeID"].SourceColumn = "BankFeeID";
			parameters["@IsWithTR"].SourceColumn = "IsWithTR";
			parameters["@TaxOption"].SourceColumn = "TaxOption";
			parameters["@TaxAmount"].SourceColumn = "TaxAmount";
			parameters["@TaxGroupID"].SourceColumn = "TaxGroupID";
			parameters["@Amount"].SourceColumn = "Amount";
			parameters["@AmountFC"].SourceColumn = "AmountFC";
			parameters["@CurrencyID"].SourceColumn = "CurrencyID";
			parameters["@CurrencyRate"].SourceColumn = "CurrencyRate";
			if (isUpdate)
			{
				return updateCommand;
			}
			return insertCommand;
		}

		public bool UpdateTREntryTransaction(BankFacilityTransactionData transactionData)
		{
			bool flag = true;
			SqlCommand insertUpdateTransactionCommand = GetInsertUpdateTransactionCommand(isUpdate: true);
			try
			{
				DataRow dataRow = transactionData.BankFacilityTransactionTable.Rows[0];
				int currencyDecimalPoints = new CompanyInformations(base.DBConfig).CurrencyDecimalPoints;
				SysDocTypes sysDocTypes = (SysDocTypes)byte.Parse(dataRow["SysDocType"].ToString());
				SqlTransaction sqlTransaction = base.DBConfig.StartNewTransaction();
				string text = dataRow["VoucherID"].ToString();
				string sysDocID = dataRow["SysDocID"].ToString();
				dataRow["RegisterID"].ToString();
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
					foreach (DataRow row in transactionData.BankFacilityTransactionDetailsTable.Rows)
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
					foreach (DataRow row2 in transactionData.BankFacilityTransactionDetailsTable.Rows)
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
				flag &= Update(transactionData, "Bank_Facility_Transaction", insertUpdateTransactionCommand);
				insertUpdateTransactionCommand = GetInsertUpdateTransactionDetailsCommand(isUpdate: false);
				insertUpdateTransactionCommand.Transaction = sqlTransaction;
				flag &= DeleteTransactionDetailsRows(sysDocID, text, sqlTransaction);
				if (transactionData.Tables["Bank_Facility_Transaction_Details"].Rows.Count > 0)
				{
					flag &= Insert(transactionData, "Bank_Facility_Transaction_Details", insertUpdateTransactionCommand);
				}
				if (!flag)
				{
					throw new Exception("Failed to save the transaction because of an unexpected error. Please try again.");
				}
				flag &= UpdateTableRowInsertUpdateInfo("Bank_Facility_Transaction", "SysDocID", dataRow["SysDocID"].ToString(), "VoucherID", dataRow["VoucherID"].ToString(), sqlTransaction, isInsert: false);
				string entityName = "Tranaction";
				if (sysDocTypes == SysDocTypes.TR)
				{
					entityName = "TR";
				}
				flag &= AddActivityLog(entityName, text, sysDocID, ActivityTypes.Update, sqlTransaction);
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

		public bool InsertUpdateTransaction(BankFacilityTransactionData transactionData, bool isUpdate)
		{
			bool flag = true;
			SqlCommand insertUpdateTransactionCommand = GetInsertUpdateTransactionCommand(isUpdate);
			try
			{
				DataRow dataRow = transactionData.BankFacilityTransactionTable.Rows[0];
				int currencyDecimalPoints = new CompanyInformations(base.DBConfig).CurrencyDecimalPoints;
				SysDocTypes sysDocTypes = (SysDocTypes)byte.Parse(dataRow["SysDocType"].ToString());
				SqlTransaction sqlTransaction = base.DBConfig.StartNewTransaction();
				string text = dataRow["VoucherID"].ToString();
				string sysDocID = dataRow["SysDocID"].ToString();
				dataRow["RegisterID"].ToString();
				if (!isUpdate && new SystemDocuments(base.DBConfig).ExistDocumentNumber("Bank_Facility_Transaction", "VoucherID", dataRow["SysDocID"].ToString(), text, sqlTransaction))
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
					foreach (DataRow row in transactionData.BankFacilityTransactionDetailsTable.Rows)
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
					foreach (DataRow row2 in transactionData.BankFacilityTransactionDetailsTable.Rows)
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
					if (transactionData.Tables["Bank_Facility_Transaction"].Rows.Count > 0)
					{
						flag &= Insert(transactionData, "Bank_Facility_Transaction", insertUpdateTransactionCommand);
					}
				}
				else
				{
					flag &= Update(transactionData, "Bank_Facility_Transaction", insertUpdateTransactionCommand);
				}
				insertUpdateTransactionCommand = GetInsertUpdateTransactionDetailsCommand(isUpdate: false);
				insertUpdateTransactionCommand.Transaction = sqlTransaction;
				if (isUpdate)
				{
					flag &= DeleteTransactionDetailsRows(sysDocID, text, sqlTransaction);
				}
				if (transactionData.Tables["Bank_Facility_Transaction_Details"].Rows.Count > 0)
				{
					flag &= Insert(transactionData, "Bank_Facility_Transaction_Details", insertUpdateTransactionCommand);
				}
				insertUpdateTransactionCommand = GetInsertUpdateTransactionBankFeesCommand(isUpdate: false);
				insertUpdateTransactionCommand.Transaction = sqlTransaction;
				if (transactionData.Tables["Bank_Fee_Details"].Rows.Count > 0)
				{
					flag &= Insert(transactionData, "Bank_Fee_Details", insertUpdateTransactionCommand);
				}
				GLData journalData = CreateGLData(transactionData, sqlTransaction);
				flag &= new Journal(base.DBConfig).InsertUpdateJournal(journalData, isUpdate, sqlTransaction);
				string text5 = dataRow["RequestSysDocID"].ToString();
				string text6 = dataRow["RequestVoucherID"].ToString();
				if (text5 != "" && text6 != "")
				{
					string exp = "UPDATE Payment_Request Set Status = 2 WHERE SysDocID = '" + text5 + "' AND VoucherID = '" + text6 + "'";
					flag &= (ExecuteNonQuery(exp, sqlTransaction) > 0);
				}
				string text7 = dataRow["SourceSysDocID"].ToString();
				string text8 = dataRow["SourceVoucherID"].ToString();
				if (!text7.IsNullOrEmpty() && !text8.IsNullOrEmpty())
				{
					string text9 = "DELETE FROM AP_Payment_Advice WHERE PaymentSysDocID='" + text7 + "' AND PaymentVoucherID='" + text8 + "'";
					text9 = "DELETE FROM AP_Payment_Allocation WHERE (PaymentSysDocID='" + text7 + "' AND PaymentVoucherID='" + text8 + "')";
					flag &= Delete(text9, sqlTransaction);
					text9 = "DELETE FROM APJOURNAL WHERE SysDocID='" + text7 + "' AND VoucherID = '" + text8 + "'";
					flag &= (ExecuteNonQuery(text9, sqlTransaction) >= 0);
				}
				if (sysDocTypes == SysDocTypes.TR && transactionData.Tables.Contains("AP_Payment_Advice") && transactionData.Tables["AP_Payment_Advice"].Rows.Count > 0)
				{
					flag &= new APJournal(base.DBConfig).InsertPaymentAdvice(transactionData);
				}
				if (!flag)
				{
					throw new Exception("Failed to save the transaction because of an unexpected error. Please try again.");
				}
				flag &= UpdateTableRowInsertUpdateInfo("Bank_Facility_Transaction", "SysDocID", dataRow["SysDocID"].ToString(), "VoucherID", dataRow["VoucherID"].ToString(), sqlTransaction, !isUpdate);
				string entityName = "Tranaction";
				if (sysDocTypes == SysDocTypes.TR)
				{
					entityName = "TR";
				}
				flag = (isUpdate ? (flag & AddActivityLog(entityName, text, sysDocID, ActivityTypes.Update, sqlTransaction)) : (flag & AddActivityLog(entityName, text, sysDocID, ActivityTypes.Add, sqlTransaction)));
				if (!isUpdate)
				{
					flag &= new SystemDocuments(base.DBConfig).UpdateNextDocumentNumber(dataRow["SysDocID"].ToString(), dataRow["VoucherID"].ToString(), "Bank_Facility_Transaction", "VoucherID", sqlTransaction);
				}
				if (!flag)
				{
					return flag;
				}
				new Approval(base.DBConfig).CreateTransactionApprovalTasks(SysDocTypes.TR, sysDocID, text, "Bank_Facility_Transaction", sqlTransaction);
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

		private GLData CreateGLData(BankFacilityTransactionData transactionData, SqlTransaction sqlTransaction)
		{
			GLData gLData = new GLData();
			DataRow dataRow = transactionData.BankFacilityTransactionTable.Rows[0];
			_ = transactionData.BankFacilityTransactionDetailsTable.Rows[0];
			string text = dataRow["CurrencyID"].ToString();
			decimal d = decimal.Parse(dataRow["CurrencyRate"].ToString());
			string text2 = "M";
			text2 = new Currencies(base.DBConfig).GetCurrencyRateType(text);
			int currencyDecimalPoints = new CompanyInformations(base.DBConfig).CurrencyDecimalPoints;
			string value = dataRow["CompanyID"].ToString();
			string value2 = dataRow["DivisionID"].ToString();
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
			foreach (DataRow row in transactionData.BankFeeDetailsTable.Rows)
			{
				bool flag = false;
				decimal result = default(decimal);
				if (!row["IsWithTR"].IsDBNullOrEmpty())
				{
					flag = bool.Parse(row["IsWithTR"].ToString());
				}
				decimal.TryParse(row["TaxAmount"].ToString(), out result);
				if (flag)
				{
					num += decimal.Parse(row["Amount"].ToString());
					num += result;
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
			switch (sysDocTypes)
			{
			case SysDocTypes.ChequeReceipt:
			case SysDocTypes.CashReceipt:
			case SysDocTypes.TTReceipt:
				dataRow4["Debit"] = DBNull.Value;
				dataRow4["DebitFC"] = DBNull.Value;
				dataRow4["CreditFC"] = dataRow["AmountFC"];
				dataRow4["Credit"] = dataRow["Amount"];
				break;
			case SysDocTypes.CashPayment:
			case SysDocTypes.ChequePayment:
			case SysDocTypes.TTPayment:
			case SysDocTypes.TR:
				dataRow4["Debit"] = dataRow["Amount"];
				dataRow4["DebitFC"] = dataRow["AmountFC"];
				dataRow4["Credit"] = DBNull.Value;
				dataRow4["CreditFC"] = DBNull.Value;
				break;
			}
			dataRow4["Description"] = dataRow["Description"];
			dataRow4["Reference"] = dataRow["Reference"];
			dataRow4["CostCenterID"] = dataRow["CostCenterID"];
			dataRow4["AnalysisID"] = dataRow["AnalysisID"];
			dataRow4["RowIndex"] = -1;
			dataRow4["CompanyID"] = value;
			dataRow4["DivisionID"] = value2;
			dataRow4.EndEdit();
			gLData.JournalDetailsTable.Rows.Add(dataRow4);
			foreach (DataRow row2 in transactionData.BankFacilityTransactionDetailsTable.Rows)
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
				switch (sysDocTypes)
				{
				case SysDocTypes.ChequeReceipt:
				case SysDocTypes.CashReceipt:
				case SysDocTypes.TTReceipt:
					dataRow4["DebitFC"] = row2["AmountFC"];
					dataRow4["Debit"] = row2["Amount"];
					dataRow4["Credit"] = DBNull.Value;
					dataRow4["CreditFC"] = DBNull.Value;
					break;
				case SysDocTypes.CashPayment:
				case SysDocTypes.ChequePayment:
				case SysDocTypes.TTPayment:
				case SysDocTypes.TR:
					dataRow4["Debit"] = DBNull.Value;
					dataRow4["DebitFC"] = DBNull.Value;
					if (sysDocTypes == SysDocTypes.TR && num > 0m)
					{
						decimal d2 = decimal.Parse(dataRow["Amount"].ToString());
						dataRow4["Credit"] = d2 + num;
						if (text != GetBaseCurrencyID())
						{
							decimal result2 = default(decimal);
							decimal.TryParse(row2["AmountFC"].ToString(), out result2);
							if (text2 == "M")
							{
								result2 += Math.Round(num / d, currencyDecimalPoints);
							}
							else
							{
								result2 += Math.Round(num * d, currencyDecimalPoints);
							}
							dataRow4["CreditFC"] = result2;
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
					break;
				}
				dataRow4["Description"] = row2["Description"];
				dataRow4["Reference"] = row2["Reference"];
				dataRow4["CostCenterID"] = row2["CostCenterID"];
				dataRow4["AnalysisID"] = row2["AnalysisID"];
				dataRow4["RowIndex"] = row2["RowIndex"];
				dataRow4["CompanyID"] = value;
				dataRow4["DivisionID"] = value2;
				dataRow4.EndEdit();
				gLData.JournalDetailsTable.Rows.Add(dataRow4);
			}
			foreach (DataRow row3 in transactionData.BankFeeDetailsTable.Rows)
			{
				dataRow4 = gLData.JournalDetailsTable.NewRow();
				dataRow4.BeginEdit();
				dataRow4["JournalID"] = 0;
				dataRow4["AccountID"] = row3["ExpenseAccountID"];
				dataRow4["DebitFC"] = DBNull.Value;
				dataRow4["Debit"] = row3["Amount"];
				dataRow4["IsBaseOnly"] = true;
				dataRow4["Credit"] = DBNull.Value;
				dataRow4["CreditFC"] = DBNull.Value;
				dataRow4["Description"] = row3["Description"];
				dataRow4["Reference"] = row3["Reference"];
				dataRow4["RowIndex"] = row3["RowIndex"];
				dataRow4["CompanyID"] = value;
				dataRow4["DivisionID"] = value2;
				row3["BankAccountID"].ToString();
				dataRow4.EndEdit();
				gLData.JournalDetailsTable.Rows.Add(dataRow4);
				bool flag2 = false;
				if (!row3["IsWithTR"].IsDBNullOrEmpty())
				{
					flag2 = bool.Parse(row3["IsWithTR"].ToString());
				}
				decimal result3 = default(decimal);
				decimal.TryParse(row3["TaxAmount"].ToString(), out result3);
				if (!flag2)
				{
					dataRow4 = gLData.JournalDetailsTable.NewRow();
					dataRow4.BeginEdit();
					dataRow4["JournalID"] = 0;
					dataRow4["IsBaseOnly"] = true;
					dataRow4["AccountID"] = row3["BankAccountID"];
					dataRow4["DebitFC"] = DBNull.Value;
					dataRow4["Debit"] = DBNull.Value;
					dataRow4["Credit"] = row3["Amount"];
					dataRow4["CreditFC"] = DBNull.Value;
					dataRow4["Description"] = row3["Description"];
					dataRow4["Reference"] = row3["Reference"];
					dataRow4["RowIndex"] = row3["RowIndex"];
					dataRow4["CompanyID"] = value;
					dataRow4["DivisionID"] = value2;
					dataRow4.EndEdit();
					gLData.JournalDetailsTable.Rows.Add(dataRow4);
				}
				if (result3 > 0m)
				{
					dataRow4 = gLData.JournalDetailsTable.NewRow();
					dataRow4.BeginEdit();
					dataRow4["JournalID"] = 0;
					dataRow4["IsBaseOnly"] = true;
					dataRow4["AccountID"] = row3["ExpenseAccountID"];
					dataRow4["DebitFC"] = DBNull.Value;
					dataRow4["Debit"] = row3["TaxAmount"];
					dataRow4["Credit"] = DBNull.Value;
					dataRow4["CreditFC"] = DBNull.Value;
					dataRow4["Description"] = row3["Description"];
					dataRow4["Reference"] = row3["Reference"];
					dataRow4["RowIndex"] = row3["RowIndex"];
					dataRow4["CompanyID"] = value;
					dataRow4["DivisionID"] = value2;
					dataRow4.EndEdit();
					gLData.JournalDetailsTable.Rows.Add(dataRow4);
				}
			}
			return gLData;
		}

		internal bool DeleteTransactionDetailsRows(string sysDocID, string voucherID, SqlTransaction sqlTransaction)
		{
			bool flag = true;
			try
			{
				string commandText = "DELETE FROM Bank_Facility_Transaction_Details WHERE SysDocID = '" + sysDocID + "' AND VoucherID = '" + voucherID + "'";
				flag = Delete(commandText, sqlTransaction);
				commandText = "DELETE FROM Bank_Fee_Details WHERE GLTransactionSysDocID = '" + sysDocID + "' AND GLTransactionVoucherID = '" + voucherID + "'";
				return flag & Delete(commandText, sqlTransaction);
			}
			catch
			{
				throw;
			}
		}

		public bool VoidTransaction(string sysDocID, string voucherID, bool isVoid)
		{
			bool flag = true;
			try
			{
				SqlTransaction sqlTransaction = base.DBConfig.StartNewTransaction();
				string exp = "SELECT IsVoid FROM Bank_Facility_Transaction WHERE SysDocID='" + sysDocID + "' AND VoucherID='" + voucherID + "'";
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
				switch (new SystemDocuments(base.DBConfig).GetSystemDocumentType(sysDocID, sqlTransaction))
				{
				case SysDocTypes.ChequeReceipt:
					if (new ReceivedCheques(base.DBConfig).TransactionHasProcessedCheques(sysDocID, voucherID, sqlTransaction))
					{
						throw new CompanyException("One or more cheques in this transaction are already in use by another transaction.", 1030);
					}
					break;
				case SysDocTypes.ChequePayment:
				case SysDocTypes.ChequeExpense:
					if (new IssuedCheques(base.DBConfig).TransactionHasProcessedCheques(sysDocID, voucherID, sqlTransaction))
					{
						throw new CompanyException("One or more cheques in this transaction are already in use by another transaction.", 1030);
					}
					break;
				}
				exp = "UPDATE Bank_Facility_Transaction_Details SET IsVoid='" + isVoid.ToString() + "'  WHERE SysDocID='" + sysDocID + "' ";
				exp = exp + " AND VoucherID='" + voucherID + "'";
				flag = (ExecuteNonQuery(exp, sqlTransaction) >= 0);
				if (flag)
				{
					exp = "UPDATE Bank_Facility_Transaction SET IsVoid = '" + isVoid.ToString() + "' WHERE SysDocID='" + sysDocID + "' ";
					exp = exp + " AND VoucherID='" + voucherID + "'";
					flag &= (ExecuteNonQuery(exp, sqlTransaction) > 0);
				}
				flag &= new Journal(base.DBConfig).VoidJournal(sysDocID, voucherID, isVoid, sqlTransaction);
				flag &= new ReceivedCheques(base.DBConfig).VoidChequeTransaction(sysDocID, voucherID, isVoid, sqlTransaction);
				flag &= new IssuedCheques(base.DBConfig).VoidChequeTransaction(sysDocID, voucherID, isVoid, sqlTransaction);
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
				string textCommand = "SELECT SourceSysDocID,SourceVoucherID FROM Bank_Facility_Transaction WHERE SysDocID = '" + sysDocID + "' AND VoucherID = '" + voucherID + "'";
				DataSet dataSet = new DataSet();
				FillDataSet(dataSet, "Vendor", textCommand);
				string text = "";
				string text2 = "";
				if (dataSet.Tables[0].Rows.Count > 0)
				{
					text = dataSet.Tables[0].Rows[0]["SourceSysDocID"].ToString();
					text2 = dataSet.Tables[0].Rows[0]["SourceVoucherID"].ToString();
				}
				if (!text.IsNullOrEmpty() && !text2.IsNullOrEmpty())
				{
					textCommand = "UPDATE APJOURNAL Set SysDocID='" + text + "' ,VoucherID='" + text2 + "' WHERE SysDocID = '" + sysDocID + "' AND VoucherID = '" + voucherID + "'";
					flag &= (ExecuteNonQuery(textCommand, sqlTransaction) > 0);
					textCommand = "DELETE FROM AP_Payment_Allocation WHERE PaymentSysDocID='" + sysDocID + "' AND PaymentVoucherID='" + voucherID + "'";
					flag &= (ExecuteNonQuery(textCommand, sqlTransaction) > 0);
					textCommand = "DELETE FROM AP_Payment_Advice WHERE PaymentSysDocID='" + sysDocID + "' AND PaymentVoucherID='" + voucherID + "'";
					flag &= (ExecuteNonQuery(textCommand, sqlTransaction) > 0);
				}
				flag &= DeleteTransactionDetailsRows(sysDocID, voucherID, sqlTransaction);
				textCommand = "DELETE FROM Bank_Facility_Transaction WHERE SysDocID='" + sysDocID + "' AND VoucherID='" + voucherID + "'";
				flag &= (ExecuteNonQuery(textCommand, sqlTransaction) > 0);
				flag &= new Journal(base.DBConfig).DeleteJournal(sysDocID, voucherID, sqlTransaction);
				flag &= new SystemDocuments(base.DBConfig).UpdateNextToDeletedDocumentNumber(sysDocID, voucherID, "Bank_Facility_Transaction", "VoucherID", sqlTransaction);
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

		public BankFacilityTransactionData GetTransactionByID(string sysDocID, string voucherID)
		{
			try
			{
				BankFacilityTransactionData bankFacilityTransactionData = new BankFacilityTransactionData();
				string textCommand = "SELECT * FROM Bank_Facility_Transaction WHERE VoucherID='" + voucherID + "' AND SysDocID='" + sysDocID + "'";
				FillDataSet(bankFacilityTransactionData, "Bank_Facility_Transaction", textCommand);
				if (bankFacilityTransactionData == null || bankFacilityTransactionData.Tables.Count == 0 || bankFacilityTransactionData.Tables["Bank_Facility_Transaction"].Rows.Count == 0)
				{
					return null;
				}
				bankFacilityTransactionData.BankFacilityTransactionTable.Rows[0]["TransactionID"].ToString();
				textCommand = "SELECT TD.*,\r\n                        (CASE PayeeType\r\n                        WHEN 'C' THEN Customer.CustomerName\r\n                        WHEN 'V' THEN Vendor.VendorName\r\n                        WHEN 'E' THEN Employee.FirstName\r\n                        ELSE Account.AccountName END) AS AccountName\r\n                        FROM BANK_FACILITY_TRANSACTION_DETAILS TD LEFt OUTER JOIN \r\n                        Account ON TD.AccountID=Account.AccountID LEFt OUTER JOIN\r\n                        Customer ON TD.PayeeID=Customer.CustomerID LEFt OUTER JOIN\r\n                        Vendor ON TD.PayeeID=Vendor.VendorID LEFt OUTER JOIN\r\n                        Employee ON TD.PayeeID=Employee.EmployeeID\r\n                        WHERE VoucherID='" + voucherID + "' AND SysDocID='" + sysDocID + "'";
				FillDataSet(bankFacilityTransactionData, "Bank_Facility_Transaction_Details", textCommand);
				new SystemDocuments(base.DBConfig).GetSystemDocumentType(sysDocID, null);
				textCommand = "SELECT BFD.*,Cur.RateType FROM Bank_Fee_Details BFD LEFT OUTER JOIN Currency Cur ON BFD.CurrencyID = Cur.CurrencyID WHERE \r\n                                GLTransactionSysDocID='" + sysDocID + "' AND GLTransactionVoucherID = '" + voucherID + "'";
				FillDataSet(bankFacilityTransactionData, "Bank_Fee_Details", textCommand);
				return bankFacilityTransactionData;
			}
			catch
			{
				throw;
			}
		}

		public DataSet GetTransactionToPrint(string sysDocID, string[] voucherID)
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
				string textCommand = "SELECT '' TransactionID,SysDocID,VoucherID,CostCenterID,PayeeType,PayeeID,RegisterID,'' SecondRegisterID,\r\n                                (CASE PayeeType\r\n                                WHEN 'C' THEN Customer.CustomerName\r\n                                WHEN 'V' THEN Vendor.VendorName\r\n                                WHEN 'E' THEN Emp2.FirstName + ' ' + Emp2.LastName\r\n                                ELSE Account.AccountName END) AS PayeeName,\r\n                                ISNULL(AmountFC,Amount) AS Amount,TransactionDate,IsVoid,CurrencyRate,GLType, '' ChequebookID, ''CheckNumber,\r\n                                '' CheckDate,Reference,FirstAccountID,SecondAccountID,GLT.EmployeeID,Emp.FirstName + ' ' + Emp.LastName as EmployeeName,Description,\r\n                                ISNULL(GLT.CurrencyID,(SELECT CurrencyID FROM Currency WHERE IsBase='True')) AS CurrencyID ,\r\n                                ISNULL(FirstAccountID,RegisterID) AS TransferFromID,ISNULL(SecondAccountID,'') AS TransferToID,\r\n                                TAcc.AccountName AS TransferFromName,TAcc2.AccountName AS TransferToName\r\n                                FROM Bank_Facility_Transaction GLT LEFT OUTER JOIN Employee Emp ON Emp.EmployeeID=GLT.EmployeeID\r\n                                LEFT OUTER JOIN Account ON GLT.PayeeID=Account.AccountID \r\n                                LEFt OUTER JOIN Customer ON GLT.PayeeID=Customer.CustomerID \r\n                                LEFt OUTER JOIN Vendor ON GLT.PayeeID=Vendor.VendorID \r\n                                LEFt OUTER JOIN Employee Emp2 ON GLT.PayeeID=Emp2.EmployeeID \r\n                                LEFt OUTER JOIN Account TAcc ON GLT.FirstAccountID=TAcc.AccountID\r\n                                LEFt OUTER JOIN Account TAcc2 ON GLT.SecondAccountID=TAcc2.AccountID\r\n                                WHERE SysDocID = '" + sysDocID + "' AND VoucherID IN (" + text + ")";
				FillDataSet(dataSet, "Bank_Facility_Transaction", textCommand);
				if (dataSet == null || dataSet.Tables.Count == 0 || dataSet.Tables["Bank_Facility_Transaction"].Rows.Count == 0)
				{
					return null;
				}
				dataSet.Tables[0].Rows[0]["TransactionID"].ToString();
				textCommand = "SELECT TD.*,Bank.BankName,\r\n                        (CASE PayeeType\r\n                        WHEN 'C' THEN Customer.CustomerName\r\n                        WHEN 'V' THEN Vendor.VendorName\r\n                        WHEN 'E' THEN Employee.FirstName + ' ' + Employee.LastName\r\n                        ELSE Account.AccountName END) AS AccountName\r\n                        FROM BANK_FACILITY_TRANSACTION_DETAILS TD LEFt OUTER JOIN \r\n                        Account ON TD.AccountID=Account.AccountID LEFt OUTER JOIN\r\n                        Customer ON TD.PayeeID=Customer.CustomerID LEFt OUTER JOIN\r\n                        Vendor ON TD.PayeeID=Vendor.VendorID LEFt OUTER JOIN\r\n                        Employee ON TD.PayeeID=Employee.EmployeeID\r\n                        LEFT OUTER JOIN Bank ON Bank.BankID=TD.BankID\r\n                        WHERE SysDocID = '" + sysDocID + "' AND VoucherID IN (" + text + ")";
				FillDataSet(dataSet, "Bank_Facility_Transaction_Details", textCommand);
				textCommand = "SELECT * FROM Bank_Fee_Details WHERE GLTransactionSysDocID = '" + sysDocID + "' AND GLTransactionVoucherID IN (" + text + ")";
				FillDataSet(dataSet, "Bank_Facility_Transaction_Details", textCommand);
				dataSet.Relations.Add("TransactionDetails", new DataColumn[2]
				{
					dataSet.Tables["Bank_Facility_Transaction"].Columns["SysDocID"],
					dataSet.Tables["Bank_Facility_Transaction"].Columns["VoucherID"]
				}, new DataColumn[2]
				{
					dataSet.Tables["Bank_Facility_Transaction_Details"].Columns["SysDocID"],
					dataSet.Tables["Bank_Facility_Transaction_Details"].Columns["VoucherID"]
				}, createConstraints: false);
				dataSet.Relations.Add("TransactionFeeDetails", new DataColumn[2]
				{
					dataSet.Tables["Bank_Facility_Transaction"].Columns["SysDocID"],
					dataSet.Tables["Bank_Facility_Transaction"].Columns["VoucherID"]
				}, new DataColumn[2]
				{
					dataSet.Tables["Bank_Facility_Transaction_Details"].Columns["GLTransactionSysDocID"],
					dataSet.Tables["Bank_Facility_Transaction_Details"].Columns["GLTransactionVoucherID"]
				}, createConstraints: false);
				dataSet.Tables["Bank_Facility_Transaction"].Columns.Add("AmountInWords", typeof(string));
				foreach (DataRow row in dataSet.Tables["Bank_Facility_Transaction"].Rows)
				{
					decimal result = default(decimal);
					decimal.TryParse(row["Amount"].ToString(), out result);
					row["AmountInWords"] = NumToWord.GetNumInWords(result);
				}
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
			string text3 = "SELECT SysDocID,VoucherID,TransactionDate,BankFacilityID,PayeeType,PayeeID,Reference,Amount, AmountFC\r\n                                FROM Bank_Facility_Transaction\r\n                                WHERE FacilityType = " + (int)facilityType;
			if (from != DateTime.MinValue)
			{
				text3 = text3 + " AND TransactionDate Between '" + text + "' AND '" + text2 + "'";
			}
			if (!showVoid)
			{
				text3 += " AND ISNULL(IsVoid,'False')='False' ";
			}
			SqlCommand sqlCommand = new SqlCommand(text3);
			FillDataSet(dataSet, "Bank_Facility_Transaction", sqlCommand);
			return dataSet;
		}

		public DataSet GetOpenTransactions(BankFacilityTypes type)
		{
			DataSet dataSet = new DataSet();
			string str = "SELECT  SysDocID,VoucherID,BFT.FacilityType,BankFacilityID,Reference,BF.CurrentAccountID,BF.PayableAccountID,CurrencyID, ISNULL(AmountFC,Amount) AS Amount, Amount AS AmountLC,Amount-ISNULL(PaidAmount,0) AS Balance\r\n                                    FROM Bank_Facility_Transaction BFT INNER JOIN Bank_Facility BF  ON BF.FacilityID=BFT.BankFacilityID WHERE Amount-ISNULL(PaidAmount,0)>0  AND BFT.FacilityType = " + (int)type;
			str += " UNION\r\n\r\n                            SELECT  SysDocID,VoucherID,BFT.FacilityType,BankFacilityID,Reference,BF.CurrentAccountID,BF.PayableAccountID,\r\n                            CurrencyID, ISNULL(AmountFC, Amount) AS Amount, Amount AS AmountLC, Amount-ISNULL(PaidAmount, 0) AS Balance\r\n                            FROM Bill_Discount BFT INNER JOIN Bank_Facility BF ON BF.FacilityID = BFT.BankFacilityID\r\n                             WHERE Amount-ISNULL(PaidAmount, 0) > 0  AND BFT.FacilityType = 4";
			FillDataSet(dataSet, "Bank_Facility_Transaction", str);
			return dataSet;
		}

		public DataSet GetList(BankFacilityTypes facilityType, bool showVoid)
		{
			DataSet dataSet = new DataSet();
			string text = "SELECT SysDocID,VoucherID,TransactionDate,BankFacilityID,PayeeType,PayeeID,Reference,Amount, AmountFC\r\n                                FROM Bank_Facility_Transaction\r\n                                WHERE FacilityType = " + (int)facilityType;
			if (!showVoid)
			{
				text += " AND ISNULL(IsVoid,'False')='False' ";
			}
			SqlCommand sqlCommand = new SqlCommand(text);
			FillDataSet(dataSet, "Bank_Facility_Transaction", sqlCommand);
			return dataSet;
		}
	}
}
