using Micromind.Common;
using Micromind.Common.Data;
using Micromind.Common.Libraries;
using System;
using System.Data;
using System.Data.SqlClient;

namespace Micromind.Data
{
	public sealed class OpeningEntryTransactions : StoreObject
	{
		private const string TRANSACTIONID_PARM = "@TransactionID";

		private const string SYSDOCID_PARM = "@SysDocID";

		private const string REGISTERID_PARM = "@RegisterID";

		private const string VOUCHERID_PARM = "@VoucherID";

		private const string AMOUNT_PARM = "@Amount";

		private const string AMOUNTFC_PARM = "@AmountFC";

		private const string EXPAMOUNT_PARM = "@ExpAmount";

		private const string EXPCODE_PARM = "@ExpCode";

		private const string EXPPERCENT_PARM = "@ExpPercent";

		private const string TRANSACTIONDATE_PARM = "@TransactionDate";

		private const string CURRENCYID_PARM = "@CurrencyID";

		private const string CURRENCYRATE_PARM = "@CurrencyRate";

		private const string JOURNALID_PARM = "@JournalID";

		private const string REFERENCE_PARM = "@Reference";

		private const string REASONID_PARM = "@ReasonID";

		private const string DESCRIPTION_PARM = "@Description";

		private const string GLTYPE_PARM = "@GLType";

		private const string STATUS_PARM = "@TransactionStatus";

		private const string GLTYPEID_PARM = "@GLTYPEID";

		private const string TRANSFERFROMTYPE_PARM = "@TransferFromType";

		private const string TRANSFERTOTYPE_PARM = "@TransferToType";

		private const string FIRSTACCOUNTID_PARM = "@FirstAccountID";

		private const string SECONDACCOUNTID_PARM = "@SecondAccountID";

		private const string SECONDREGISTERID_PARM = "@SecondRegisterID";

		private const string EMPLOYEEID_PARM = "@EmployeeID";

		private const string ISSECONDFORM_PARM = "@IsSecondForm";

		private const string REQUESTSYSDOCID_PARM = "@RequestSysDocID";

		private const string REQUESTVOUCHERID_PARM = "@RequestVoucherID";

		private const string CHECKDELIVEREDDATE_PARM = "@CheckDeliveredDate";

		private const string TRANSACTION_TABLE = "GL_Transaction";

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

		private const string CHECKNUMBER_PARM = "@CheckNumber";

		private const string CHEQUEBOOKID_PARM = "@ChequebookID";

		private const string CHECKDATE_PARM = "@CheckDate";

		private const string CHEQUEID_PARM = "@ChequeID";

		private const string TRANSACTIONDETAILS_TABLE = "Transaction_Details";

		private const string BANKFACILITYID_PARM = "@BankFacilityID";

		private const string DUEDATE_PARM = "@DueDate";

		private const string BANKFEEDETAILS_TABLE = "Bank_Fee_Details";

		private const string GLTRANSACTIONSYSDOCID_PARM = "@GLTransactionSysDocID";

		private const string GLTRANSACTIONVOUCHERID_PARM = "@GLTransactionVoucherID";

		private const string BANKACCOUNTID_PARM = "@BankAccountID";

		private const string EXPENSEACCOUNTID_PARM = "@ExpenseAccountID";

		private const string BANKFEEID_PARM = "@BankFeeID";

		private const string TRANSACTIONNUMBER_PARM = "@TransactionNumber";

		private const string PARTYONEACCOUNTID_PARM = "@PartyOneAccountID";

		private const string PARTYTWOACCOUNTID_PARM = "@PartyTwoAccountID";

		private const string ISACCOUNTTRANSACTION_PARM = "@IsAccountTransaction";

		private const string ISPARTNERTRANSACTION_PARM = "@IsPartnerTransaction";

		private const string ISPAYROLLTRANSACTION_PARM = "@IsPayrollTransaction";

		private const string ISDEBIT_PARM = "@IsDebit";

		private const string GLID_PARM = "@GLID";

		private const string CHECKDSCRIPTION_PARM = "@CheckDescription";

		private const string PARTNERID_PARM = "@PartnerID";

		private const string DATETIMESTAMP_PARM = "@DateTimeStamp";

		private const string PAYROLLITEMID_PARM = "@PayrollItem";

		private const string PAYPERIODBEGINDATE_PARM = "@PayPeriodBeginDate";

		private const string PAYPERIODENDDATE_PARM = "@PayPeriodEndDate";

		private const string HOURS_PARM = "@Hours";

		private const string DAYS_PARM = "@Days";

		private const string WEEKS_PARM = "@Weeks";

		private const string JOBID_PARM = "@JobID";

		private const string COSTCATEGORYID_PARM = "@CostCategoryID";

		private const string CONSIGNID_PARM = "@ConsignID";

		private const string CONSIGNEXPENSEID_PARM = "@ConsignExpenseID";

		private const string STOREID_PARM = "@StoreID";

		private const string PAYROLLID_PARM = "@PayrollID";

		private const string ACCOUNT_PARM = "@Account";

		private const string TYPE_PARM = "@Type";

		private const string TYPEID_PARM = "@TypeID";

		private const string NUNBER_PARM = "@Number";

		private const string CREDITCARDID_PARM = "@CreditCardID";

		private const string GENERALPAYMENTDETAIL_TABLE = "General_Payment_Detail";

		private const string SOURCESYSDOCID_PARM = "@SourceSysDocID";

		private const string SOURCEVOUCHERID_PARM = "@SourceVoucherID";

		private const string RECEIVEDCHEQUE_TABLE = "Cheque_Received";

		private const string CHEQUENUMBER_PARM = "@ChequeNumber";

		private const string PAYEEACCOUNTID_PARM = "@PayeeAccountID";

		private const string CHEQUEDATE_PARM = "@ChequeDate";

		private const string RECEIPTDATE_PARM = "@ReceiptDate";

		private const string EXCHANGERATE_PARM = "@ExchangeRate";

		private const string CONAMOUNTFC_PARM = "@ConAmountFC";

		private const string CONRATE_PARM = "@ConRate";

		private const string NOTE_PARM = "@Note";

		private const string PDCACCOUNTID_PARM = "@PDCAccountID";

		private const string DEPOSITDATE_PARM = "@DepositDate";

		private const string DEPOSITACCOUNTID_PARM = "@DepositAccountID";

		private const string DEPOSITBANKID_PARM = "@DepositBankID";

		private const string DEPOSITVOUCHERID_PARM = "@DepositVoucherID";

		private const string DEPOSITSYSDOCID_PARM = "@DepositSysDocID";

		private const string STATUS1_PARM = "@Status";

		private const string ISSUEDCHEQUE_TABLE = "Opening_Cheque_Issued";

		private const string CHEQUEBOOKID_FIELD = "@ChequebookID";

		private const string ISSUEDATE_PARM = "@IssueDate";

		private const string CLEARANCEDATE_PARM = "@ClearanceDate";

		private const string CLEARANCEACCOUNTID_PARM = "@ClearanceAccountID";

		private const string CLEARANCEVOUCHERID_PARM = "@ClearanceVoucherID";

		private const string SECURITYCHEQUETABLE_PARM = "@Security_Cheque";

		private static object syncRoot = new object();

		public OpeningEntryTransactions(Config config)
			: base(config)
		{
		}

		private string GetInsertUpdateTransactionText(bool isUpdate)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddInsertUpdateParameters("GL_Transaction", new FieldValue("TransactionID", "@TransactionID"), new FieldValue("SysDocID", "@SysDocID", isUpdateConditionField: true), new FieldValue("VoucherID", "@VoucherID", isUpdateConditionField: true), new FieldValue("RegisterID", "@RegisterID"), new FieldValue("CostCenterID", "@CostCenterID"), new FieldValue("PayeeType", "@PayeeType"), new FieldValue("PayeeID", "@PayeeID"), new FieldValue("TransactionDate", "@TransactionDate"), new FieldValue("Amount", "@Amount"), new FieldValue("AmountFC", "@AmountFC"), new FieldValue("ExpAmount", "@ExpAmount"), new FieldValue("ExpPercent", "@ExpPercent"), new FieldValue("ExpCode", "@ExpCode"), new FieldValue("CurrencyID", "@CurrencyID"), new FieldValue("CurrencyRate", "@CurrencyRate"), new FieldValue("Reference", "@Reference"), new FieldValue("ReasonID", "@ReasonID"), new FieldValue("Description", "@Description"), new FieldValue("TransferFromType", "@TransferFromType"), new FieldValue("TransferToType", "@TransferToType"), new FieldValue("FirstAccountID", "@FirstAccountID"), new FieldValue("SecondAccountID", "@SecondAccountID"), new FieldValue("SecondRegisterID", "@SecondRegisterID"), new FieldValue("ChequeID", "@ChequeID"), new FieldValue("RequestSysDocID", "@RequestSysDocID"), new FieldValue("RequestVoucherID", "@RequestVoucherID"), new FieldValue("ChequebookID", "@ChequebookID"), new FieldValue("CheckNumber", "@CheckNumber"), new FieldValue("CheckDate", "@CheckDate"), new FieldValue("CheckDeliveredDate", "@CheckDeliveredDate"), new FieldValue("EmployeeID", "@EmployeeID"), new FieldValue("TransactionStatus", "@TransactionStatus"), new FieldValue("IsSecondForm", "@IsSecondForm"), new FieldValue("AnalysisID", "@AnalysisID"));
			if (isUpdate)
			{
				sqlBuilder.AddInsertUpdateParameters("GL_Transaction", new FieldValue("DateUpdated", "@DateUpdated", isUpdateConditionField: true, checkForNullValue: true));
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
			parameters.Add("@TransactionID", SqlDbType.Int);
			parameters.Add("@CostCenterID", SqlDbType.NVarChar);
			parameters.Add("@SysDocID", SqlDbType.NVarChar);
			parameters.Add("@RegisterID", SqlDbType.NVarChar);
			parameters.Add("@PayeeType", SqlDbType.NVarChar);
			parameters.Add("@PayeeID", SqlDbType.NVarChar);
			parameters.Add("@VoucherID", SqlDbType.NVarChar);
			parameters.Add("@TransactionDate", SqlDbType.DateTime);
			parameters.Add("@Amount", SqlDbType.Money);
			parameters.Add("@AmountFC", SqlDbType.Money);
			parameters.Add("@ExpAmount", SqlDbType.Money);
			parameters.Add("@ExpPercent", SqlDbType.Decimal);
			parameters.Add("@ExpCode", SqlDbType.NVarChar);
			parameters.Add("@CurrencyID", SqlDbType.NVarChar);
			parameters.Add("@CurrencyRate", SqlDbType.SmallMoney);
			parameters.Add("@Reference", SqlDbType.NVarChar);
			parameters.Add("@ReasonID", SqlDbType.NVarChar);
			parameters.Add("@Description", SqlDbType.NVarChar);
			parameters.Add("@TransactionStatus", SqlDbType.TinyInt);
			parameters.Add("@TransferFromType", SqlDbType.Char);
			parameters.Add("@TransferToType", SqlDbType.Char);
			parameters.Add("@FirstAccountID", SqlDbType.NVarChar);
			parameters.Add("@SecondAccountID", SqlDbType.NVarChar);
			parameters.Add("@SecondRegisterID", SqlDbType.NVarChar);
			parameters.Add("@ChequeID", SqlDbType.Int);
			parameters.Add("@ChequebookID", SqlDbType.NVarChar);
			parameters.Add("@CheckNumber", SqlDbType.NVarChar);
			parameters.Add("@CheckDate", SqlDbType.DateTime);
			parameters.Add("@CheckDeliveredDate", SqlDbType.DateTime);
			parameters.Add("@EmployeeID", SqlDbType.NVarChar);
			parameters.Add("@AnalysisID", SqlDbType.NVarChar);
			parameters.Add("@IsSecondForm", SqlDbType.Bit);
			parameters.Add("@RequestSysDocID", SqlDbType.NVarChar);
			parameters.Add("@RequestVoucherID", SqlDbType.NVarChar);
			parameters["@TransactionID"].SourceColumn = "TransactionID";
			parameters["@CostCenterID"].SourceColumn = "CostCenterID";
			parameters["@SysDocID"].SourceColumn = "SysDocID";
			parameters["@RegisterID"].SourceColumn = "RegisterID";
			parameters["@PayeeType"].SourceColumn = "PayeeType";
			parameters["@PayeeID"].SourceColumn = "PayeeID";
			parameters["@VoucherID"].SourceColumn = "VoucherID";
			parameters["@TransactionDate"].SourceColumn = "TransactionDate";
			parameters["@Amount"].SourceColumn = "Amount";
			parameters["@AmountFC"].SourceColumn = "AmountFC";
			parameters["@ExpCode"].SourceColumn = "ExpCode";
			parameters["@ExpAmount"].SourceColumn = "ExpAmount";
			parameters["@ExpPercent"].SourceColumn = "ExpPercent";
			parameters["@CurrencyID"].SourceColumn = "CurrencyID";
			parameters["@CurrencyRate"].SourceColumn = "CurrencyRate";
			parameters["@Reference"].SourceColumn = "Reference";
			parameters["@ReasonID"].SourceColumn = "ReasonID";
			parameters["@Description"].SourceColumn = "Description";
			parameters["@TransactionStatus"].SourceColumn = "TransactionStatus";
			parameters["@TransferFromType"].SourceColumn = "TransferFromType";
			parameters["@TransferToType"].SourceColumn = "TransferToType";
			parameters["@FirstAccountID"].SourceColumn = "FirstAccountID";
			parameters["@SecondAccountID"].SourceColumn = "SecondAccountID";
			parameters["@SecondRegisterID"].SourceColumn = "SecondRegisterID";
			parameters["@ChequeID"].SourceColumn = "ChequeID";
			parameters["@ChequebookID"].SourceColumn = "ChequebookID";
			parameters["@CheckNumber"].SourceColumn = "CheckNumber";
			parameters["@CheckDate"].SourceColumn = "CheckDate";
			parameters["@CheckDeliveredDate"].SourceColumn = "CheckDeliveredDate";
			parameters["@EmployeeID"].SourceColumn = "EmployeeID";
			parameters["@AnalysisID"].SourceColumn = "AnalysisID";
			parameters["@IsSecondForm"].SourceColumn = "IsSecondForm";
			parameters["@RequestSysDocID"].SourceColumn = "RequestSysDocID";
			parameters["@RequestVoucherID"].SourceColumn = "RequestVoucherID";
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
			sqlBuilder.AddInsertUpdateParameters("Transaction_Details", new FieldValue("SysDocID", "@SysDocID"), new FieldValue("VoucherID", "@VoucherID"), new FieldValue("BankID", "@BankID"), new FieldValue("Description", "@Description"), new FieldValue("JobID", "@JobID"), new FieldValue("CostCategoryID", "@CostCategoryID"), new FieldValue("ConsignID", "@ConsignID"), new FieldValue("ConsignExpenseID", "@ConsignExpenseID"), new FieldValue("Reference", "@Reference"), new FieldValue("Amount", "@Amount"), new FieldValue("AmountFC", "@AmountFC"), new FieldValue("ExpAmount", "@ExpAmount"), new FieldValue("ExpPercent", "@ExpPercent"), new FieldValue("ExpCode", "@ExpCode"), new FieldValue("AccountID", "@AccountID"), new FieldValue("CostCenterID", "@CostCenterID"), new FieldValue("AnalysisID", "@AnalysisID"), new FieldValue("RowIndex", "@RowIndex"), new FieldValue("PayeeID", "@PayeeID"), new FieldValue("DueDate", "@DueDate"), new FieldValue("PaymentMethodID", "@PaymentMethodID"), new FieldValue("PaymentMethodType", "@PaymentMethodType"), new FieldValue("CheckNumber", "@CheckNumber"), new FieldValue("ChequebookID", "@ChequebookID"), new FieldValue("ChequeID", "@ChequeID"), new FieldValue("CheckDate", "@CheckDate"), new FieldValue("PayeeType", "@PayeeType"));
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
			parameters.Add("@JobID", SqlDbType.NVarChar);
			parameters.Add("@CostCategoryID", SqlDbType.NVarChar);
			parameters.Add("@ConsignID", SqlDbType.NVarChar);
			parameters.Add("@ConsignExpenseID", SqlDbType.NVarChar);
			parameters.Add("@BankID", SqlDbType.NVarChar);
			parameters.Add("@Reference", SqlDbType.NVarChar);
			parameters.Add("@Amount", SqlDbType.Money);
			parameters.Add("@AmountFC", SqlDbType.Money);
			parameters.Add("@ExpAmount", SqlDbType.Money);
			parameters.Add("@ExpPercent", SqlDbType.Decimal);
			parameters.Add("@ExpCode", SqlDbType.NVarChar);
			parameters.Add("@AccountID", SqlDbType.NVarChar);
			parameters.Add("@CostCenterID", SqlDbType.NVarChar);
			parameters.Add("@AnalysisID", SqlDbType.NVarChar);
			parameters.Add("@RowIndex", SqlDbType.SmallInt);
			parameters.Add("@PayeeID", SqlDbType.NVarChar);
			parameters.Add("@DueDate", SqlDbType.DateTime);
			parameters.Add("@PaymentMethodID", SqlDbType.NVarChar);
			parameters.Add("@PaymentMethodType", SqlDbType.TinyInt);
			parameters.Add("@CheckNumber", SqlDbType.NVarChar);
			parameters.Add("@ChequebookID", SqlDbType.NVarChar);
			parameters.Add("@CheckDate", SqlDbType.DateTime);
			parameters.Add("@ChequeID", SqlDbType.Int);
			parameters.Add("@PayeeType", SqlDbType.NVarChar);
			parameters["@SysDocID"].SourceColumn = "SysDocID";
			parameters["@VoucherID"].SourceColumn = "VoucherID";
			parameters["@Description"].SourceColumn = "Description";
			parameters["@JobID"].SourceColumn = "JobID";
			parameters["@CostCategoryID"].SourceColumn = "CostCategoryID";
			parameters["@ConsignID"].SourceColumn = "ConsignID";
			parameters["@ConsignExpenseID"].SourceColumn = "ConsignExpenseID";
			parameters["@BankID"].SourceColumn = "BankID";
			parameters["@Reference"].SourceColumn = "Reference";
			parameters["@Amount"].SourceColumn = "Amount";
			parameters["@AmountFC"].SourceColumn = "AmountFC";
			parameters["@ExpCode"].SourceColumn = "ExpCode";
			parameters["@ExpAmount"].SourceColumn = "ExpAmount";
			parameters["@ExpPercent"].SourceColumn = "ExpPercent";
			parameters["@AccountID"].SourceColumn = "AccountID";
			parameters["@CostCenterID"].SourceColumn = "CostCenterID";
			parameters["@AnalysisID"].SourceColumn = "AnalysisID";
			parameters["@RowIndex"].SourceColumn = "RowIndex";
			parameters["@PayeeID"].SourceColumn = "PayeeID";
			parameters["@DueDate"].SourceColumn = "DueDate";
			parameters["@PaymentMethodID"].SourceColumn = "PaymentMethodID";
			parameters["@PaymentMethodType"].SourceColumn = "PaymentMethodType";
			parameters["@CheckNumber"].SourceColumn = "CheckNumber";
			parameters["@ChequebookID"].SourceColumn = "ChequebookID";
			parameters["@ChequeID"].SourceColumn = "ChequeID";
			parameters["@CheckDate"].SourceColumn = "CheckDate";
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
			sqlBuilder.AddInsertUpdateParameters("Bank_Fee_Details", new FieldValue("GLTransactionSysDocID", "@GLTransactionSysDocID"), new FieldValue("GLTransactionVoucherID", "@GLTransactionVoucherID"), new FieldValue("RowIndex", "@RowIndex"), new FieldValue("BankFacilityID", "@BankFacilityID"), new FieldValue("ChequebookID", "@ChequebookID"), new FieldValue("Description", "@Description"), new FieldValue("Reference", "@Reference"), new FieldValue("BankAccountID", "@BankAccountID"), new FieldValue("ExpenseAccountID", "@ExpenseAccountID"), new FieldValue("BankFeeID", "@BankFeeID"), new FieldValue("Amount", "@Amount"), new FieldValue("AmountFC", "@AmountFC"), new FieldValue("CurrencyID", "@CurrencyID"), new FieldValue("CurrencyRate", "@CurrencyRate"));
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
			parameters.Add("@ChequebookID", SqlDbType.NVarChar);
			parameters.Add("@Description", SqlDbType.NVarChar);
			parameters.Add("@Reference", SqlDbType.NVarChar);
			parameters.Add("@BankAccountID", SqlDbType.NVarChar);
			parameters.Add("@ExpenseAccountID", SqlDbType.NVarChar);
			parameters.Add("@BankFeeID", SqlDbType.NVarChar);
			parameters.Add("@Amount", SqlDbType.Money);
			parameters.Add("@AmountFC", SqlDbType.Money);
			parameters.Add("@CurrencyID", SqlDbType.NVarChar);
			parameters.Add("@CurrencyRate", SqlDbType.Money);
			parameters["@GLTransactionSysDocID"].SourceColumn = "GLTransactionSysDocID";
			parameters["@GLTransactionVoucherID"].SourceColumn = "GLTransactionVoucherID";
			parameters["@RowIndex"].SourceColumn = "RowIndex";
			parameters["@BankFacilityID"].SourceColumn = "BankFacilityID";
			parameters["@ChequebookID"].SourceColumn = "ChequebookID";
			parameters["@Description"].SourceColumn = "Description";
			parameters["@Reference"].SourceColumn = "Reference";
			parameters["@BankAccountID"].SourceColumn = "BankAccountID";
			parameters["@ExpenseAccountID"].SourceColumn = "ExpenseAccountID";
			parameters["@BankFeeID"].SourceColumn = "BankFeeID";
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

		private SqlCommand GetInsertUpdateGeneralPaymentCommand(bool isUpdate)
		{
			SqlParameterCollection parameters;
			if (isUpdate)
			{
				if (updateCommand != null)
				{
					updateCommand = null;
				}
				updateCommand = new SqlCommand(GetInsertUpdateGeneralPaymentText(isUpdate: true), base.DBConfig.Connection);
				updateCommand.CommandType = CommandType.Text;
				parameters = updateCommand.Parameters;
			}
			else
			{
				if (insertCommand != null)
				{
					insertCommand = null;
				}
				insertCommand = new SqlCommand(GetInsertUpdateGeneralPaymentText(isUpdate: false), base.DBConfig.Connection);
				insertCommand.CommandType = CommandType.Text;
				parameters = insertCommand.Parameters;
			}
			parameters.Add("@SysDocID", SqlDbType.NVarChar);
			parameters.Add("@VoucherID", SqlDbType.NVarChar);
			parameters.Add("@SourceSysDocID", SqlDbType.NVarChar);
			parameters.Add("@SourceVoucherID", SqlDbType.NVarChar);
			parameters.Add("@Description", SqlDbType.NVarChar);
			parameters.Add("@Amount", SqlDbType.Decimal);
			parameters.Add("@PayeeID", SqlDbType.NVarChar);
			parameters.Add("@PayeeType", SqlDbType.NVarChar);
			parameters.Add("@TransactionDate", SqlDbType.DateTime);
			parameters.Add("@Reference", SqlDbType.NVarChar);
			parameters["@SysDocID"].SourceColumn = "SysDocID";
			parameters["@VoucherID"].SourceColumn = "VoucherID";
			parameters["@SourceSysDocID"].SourceColumn = "SourceSysDocID";
			parameters["@SourceVoucherID"].SourceColumn = "SourceVoucherID";
			parameters["@Description"].SourceColumn = "Description";
			parameters["@Amount"].SourceColumn = "Amount";
			parameters["@PayeeID"].SourceColumn = "PayeeID";
			parameters["@PayeeType"].SourceColumn = "PayeeType";
			parameters["@TransactionDate"].SourceColumn = "TransactionDate";
			parameters["@Reference"].SourceColumn = "Reference";
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

		private string GetInsertUpdateGeneralPaymentText(bool isUpdate)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddInsertUpdateParameters("General_Payment_Detail", new FieldValue("SysDocID", "@SysDocID"), new FieldValue("VoucherID", "@VoucherID"), new FieldValue("SourceSysDocID", "@SourceSysDocID"), new FieldValue("SourceVoucherID", "@SourceVoucherID"), new FieldValue("Description", "@Description"), new FieldValue("Amount", "@Amount"), new FieldValue("PayeeID", "@PayeeID"), new FieldValue("PayeeType", "@PayeeType"), new FieldValue("TransactionDate", "@TransactionDate"), new FieldValue("Reference", "@Reference"));
			if (isUpdate)
			{
				sqlBuilder.AddInsertUpdateParameters("General_Payment_Detail", new FieldValue("DateUpdated", "@DateUpdated", isUpdateConditionField: true, checkForNullValue: true));
			}
			if (isUpdate)
			{
				return sqlBuilder.GetUpdateExpression();
			}
			return sqlBuilder.GetInsertExpression();
		}

		public bool InsertUpdateTransaction(OpenEntryTransactionData transactionData, bool isUpdate)
		{
			bool flag = bool.Parse(new CompanyOption(base.DBConfig).GetCompanyOptionValue(CompanyOptionsEnum.PDCByMaturity, true).ToString());
			bool flag2 = bool.Parse(new CompanyOption(base.DBConfig).GetCompanyOptionValue(CompanyOptionsEnum.PDCIssuedByMaturity, false).ToString());
			int currencyDecimalPoints = new CompanyInformations(base.DBConfig).CurrencyDecimalPoints;
			bool flag3 = true;
			SqlCommand insertUpdateTransactionCommand = GetInsertUpdateTransactionCommand(isUpdate);
			try
			{
				DataRow dataRow = transactionData.TransactionTable.Rows[0];
				SysDocTypes sysDocTypes = (SysDocTypes)byte.Parse(dataRow["SysDocType"].ToString());
				SqlTransaction sqlTransaction = base.DBConfig.StartNewTransaction();
				string text = dataRow["VoucherID"].ToString();
				string text2 = dataRow["SysDocID"].ToString();
				string registerID = dataRow["RegisterID"].ToString();
				if (!isUpdate && new SystemDocuments(base.DBConfig).ExistDocumentNumber("GL_Transaction", "VoucherID", dataRow["SysDocID"].ToString(), text, sqlTransaction))
				{
					throw new CompanyException("Document number already exist.", 1046);
				}
				string text3 = dataRow["PayeeID"].ToString();
				string text4 = dataRow["PayeeType"].ToString();
				string text5 = "";
				if (text4 == "C")
				{
					text5 = new Customers(base.DBConfig).GetCustomerARAccountID(text2, text3);
				}
				else if (text4 == "V")
				{
					text5 = new Vendors(base.DBConfig).GetVendorAPAccountID(text2, text3);
				}
				else if (text4 == "E")
				{
					text5 = new Employees(base.DBConfig).GetEmployeeAccountID(text2, text3);
				}
				else if (text4 == "A")
				{
					text5 = text3;
				}
				if (text5 == "")
				{
					throw new CompanyException("AccountID should be selected for the payee if a payee is in transaction.", 1028);
				}
				dataRow["AccountID"] = text5;
				switch (sysDocTypes)
				{
				case SysDocTypes.OpeningChequeReceipt:
					if (!flag)
					{
						string registerAccountID3 = new Register(base.DBConfig).GetRegisterAccountID(registerID, "PDCReceivedAccountID");
						if (registerAccountID3 == "")
						{
							throw new CompanyException("PDC Received Account is not selected. Please set the PDC Received account for the selected Register.");
						}
						foreach (DataRow row in transactionData.TransactionDetailsTable.Rows)
						{
							row["AccountID"] = registerAccountID3;
							row["SysDocID"] = dataRow["SysDocID"];
							row["VoucherID"] = dataRow["VoucherID"];
						}
					}
					else
					{
						foreach (DataRow row2 in transactionData.TransactionDetailsTable.Rows)
						{
							row2["SysDocID"] = dataRow["SysDocID"];
							row2["VoucherID"] = dataRow["VoucherID"];
						}
					}
					break;
				case SysDocTypes.CashReceipt:
				case SysDocTypes.CashPayment:
				{
					string registerAccountID = new Register(base.DBConfig).GetRegisterAccountID(registerID, "CashAccountID");
					string registerAccountID2 = new Register(base.DBConfig).GetRegisterAccountID(registerID, "CardReceivedAccountID");
					foreach (DataRow row3 in transactionData.TransactionDetailsTable.Rows)
					{
						if (byte.Parse(row3["PaymentMethodType"].ToString()) == 3)
						{
							row3["AccountID"] = registerAccountID2;
							if (registerAccountID2 == "")
							{
								throw new CompanyException("Card Account is not selected for this register. Please set the Card account for the selected register.");
							}
						}
						else
						{
							row3["AccountID"] = registerAccountID;
							if (registerAccountID2 == "")
							{
								throw new CompanyException("Cash Account is not selected for this register. Please set the Cash account for the selected register.");
							}
						}
						row3["SysDocID"] = dataRow["SysDocID"];
						row3["VoucherID"] = dataRow["VoucherID"];
					}
					break;
				}
				case SysDocTypes.TTPayment:
				case SysDocTypes.TTReceipt:
					foreach (DataRow row4 in transactionData.TransactionDetailsTable.Rows)
					{
						row4["SysDocID"] = dataRow["SysDocID"];
						row4["VoucherID"] = dataRow["VoucherID"];
					}
					break;
				case SysDocTypes.OpeningChequePayment:
					if (!flag2)
					{
						foreach (DataRow row5 in transactionData.TransactionDetailsTable.Rows)
						{
							row5["ChequebookID"].ToString();
							object fieldValue = new Databases(base.DBConfig).GetFieldValue("Chequebook", "PDCIssuedAccountID", "ChequebookID", row5["ChequebookID"].ToString(), sqlTransaction);
							if (fieldValue == null || fieldValue.ToString() == "")
							{
								throw new CompanyException("'PDC Issued Account' is not assigned to this chequebook. Please select an account for this chequebook.");
							}
							string text7 = (string)(row5["AccountID"] = fieldValue.ToString());
							row5["SysDocID"] = dataRow["SysDocID"];
							row5["VoucherID"] = dataRow["VoucherID"];
						}
					}
					else
					{
						foreach (DataRow row6 in transactionData.TransactionDetailsTable.Rows)
						{
							row6["SysDocID"] = dataRow["SysDocID"];
							row6["VoucherID"] = dataRow["VoucherID"];
						}
					}
					break;
				}
				if (dataRow["CurrencyID"] != DBNull.Value && dataRow["CurrencyID"].ToString() != GetBaseCurrencyID())
				{
					decimal d = decimal.Parse(dataRow["CurrencyRate"].ToString());
					decimal num = default(decimal);
					string text8 = "M";
					text8 = new Currencies(base.DBConfig).GetCurrencyRateType(dataRow["CurrencyID"].ToString());
					foreach (DataRow row7 in transactionData.TransactionDetailsTable.Rows)
					{
						decimal result = default(decimal);
						decimal.TryParse(row7["AmountFC"].ToString(), out result);
						if (text8 == "M")
						{
							row7["Amount"] = Math.Round(result * d, currencyDecimalPoints);
							num += Math.Round(result * d, currencyDecimalPoints);
						}
						else
						{
							row7["Amount"] = Math.Round(result / d, currencyDecimalPoints);
							num += Math.Round(result / d, currencyDecimalPoints);
						}
					}
					dataRow["Amount"] = num;
				}
				insertUpdateTransactionCommand.Transaction = sqlTransaction;
				flag3 = (isUpdate ? (flag3 & Update(transactionData, "GL_Transaction", insertUpdateTransactionCommand)) : (flag3 & Insert(transactionData, "GL_Transaction", insertUpdateTransactionCommand)));
				insertUpdateTransactionCommand = GetInsertUpdateTransactionDetailsCommand(isUpdate: false);
				insertUpdateTransactionCommand.Transaction = sqlTransaction;
				if (isUpdate)
				{
					flag3 &= DeleteTransactionDetailsRows(text2, text, sqlTransaction);
				}
				if (transactionData.Tables["Transaction_Details"].Rows.Count > 0)
				{
					flag3 &= Insert(transactionData, "Transaction_Details", insertUpdateTransactionCommand);
				}
				if (transactionData.Tables["Bank_Fee_Details"].Rows.Count > 0)
				{
					insertUpdateTransactionCommand = GetInsertUpdateTransactionBankFeesCommand(isUpdate: false);
					insertUpdateTransactionCommand.Transaction = sqlTransaction;
					flag3 &= Insert(transactionData, "Bank_Fee_Details", insertUpdateTransactionCommand);
				}
				if (transactionData.Tables["General_Payment_Detail"].Rows.Count > 0)
				{
					insertUpdateTransactionCommand = GetInsertUpdateGeneralPaymentCommand(isUpdate: false);
					insertUpdateTransactionCommand.Transaction = sqlTransaction;
					flag3 &= Insert(transactionData, "General_Payment_Detail", insertUpdateTransactionCommand);
				}
				if (sysDocTypes == SysDocTypes.OpeningChequeReceipt && flag)
				{
					GLData journalData = CreateGLData(transactionData);
					flag3 &= new ARJournal(base.DBConfig).InsertARJournalForPDC(journalData, isUpdate, sqlTransaction);
				}
				else if (sysDocTypes == SysDocTypes.OpeningChequePayment && flag2)
				{
					GLData journalData2 = CreateGLData(transactionData);
					flag3 &= new APJournal(base.DBConfig).InsertAPJournalForPDC(journalData2, isUpdate, sqlTransaction);
				}
				else
				{
					GLData journalData3 = CreateGLData(transactionData);
					flag3 &= new Journal(base.DBConfig).InsertUpdateJournal(journalData3, isUpdate, sqlTransaction);
				}
				bool flag4 = TransactionHasProcessedReceivedCheques(text2, text, sqlTransaction);
				if (sysDocTypes == SysDocTypes.OpeningChequeReceipt && (!isUpdate || !flag4))
				{
					transactionData.ReceivedChequeTable.Clear();
					foreach (DataRow row8 in transactionData.TransactionDetailsTable.Rows)
					{
						DataRow dataRow6 = transactionData.ReceivedChequeTable.NewRow();
						dataRow6["SysDocID"] = text2;
						dataRow6["VoucherID"] = text;
						dataRow6["PayeeType"] = text4;
						dataRow6["PayeeID"] = text3;
						dataRow6["PayeeAccountID"] = text5;
						dataRow6["BankID"] = row8["BankID"];
						dataRow6["ChequeNumber"] = row8["CheckNumber"];
						dataRow6["ChequeDate"] = row8["CheckDate"];
						dataRow6["Amount"] = row8["Amount"];
						dataRow6["AmountFC"] = row8["AmountFC"];
						dataRow6["ExchangeRate"] = dataRow["CurrencyRate"];
						dataRow6["Note"] = row8["Description"];
						dataRow6["ReceiptDate"] = dataRow["TransactionDate"];
						dataRow6["Reference"] = dataRow["Reference"];
						dataRow6["PDCAccountID"] = row8["AccountID"];
						dataRow6["Status"] = (byte)1;
						dataRow6.EndEdit();
						transactionData.ReceivedChequeTable.Rows.Add(dataRow6);
					}
					flag3 &= DeleteChequeRows(text2, text, sqlTransaction);
					if (transactionData.ReceivedChequeTable.Rows.Count > 0)
					{
						flag3 &= InsertUpdateReceivedCheque(transactionData, isUpdate: false, sqlTransaction);
					}
				}
				if (sysDocTypes == SysDocTypes.OpeningChequePayment && (!isUpdate || !TransactionHasProcessedIssuedCheques(text2, text, sqlTransaction)))
				{
					transactionData.IssuedChequeTable.Clear();
					foreach (DataRow row9 in transactionData.TransactionDetailsTable.Rows)
					{
						DataRow dataRow8 = transactionData.IssuedChequeTable.NewRow();
						dataRow8["SysDocID"] = text2;
						dataRow8["VoucherID"] = text;
						dataRow8["PayeeType"] = text4;
						dataRow8["PayeeID"] = text3;
						dataRow8["PayeeAccountID"] = text5;
						dataRow8["BankID"] = row9["BankID"];
						dataRow8["ChequeNumber"] = row9["CheckNumber"];
						dataRow8["ChequebookID"] = row9["ChequebookID"];
						dataRow8["ChequeDate"] = row9["CheckDate"];
						dataRow8["Amount"] = row9["Amount"];
						dataRow8["AmountFC"] = row9["AmountFC"];
						dataRow8["ExchangeRate"] = dataRow["CurrencyRate"];
						dataRow8["Note"] = row9["Description"];
						dataRow8["IssueDate"] = dataRow["TransactionDate"];
						dataRow8["Reference"] = dataRow["Reference"];
						dataRow8["PDCAccountID"] = row9["AccountID"];
						dataRow8["Status"] = (byte)2;
						dataRow8.EndEdit();
						transactionData.IssuedChequeTable.Rows.Add(dataRow8);
					}
					if (transactionData.IssuedChequeTable.Rows.Count > 0)
					{
						flag3 &= InsertUpdateIssuedCheque(transactionData, isUpdate, sqlTransaction);
					}
				}
				if (!flag3)
				{
					throw new Exception("Failed to save the transaction because of an unexpected error. Please try again.");
				}
				flag3 &= UpdateTableRowInsertUpdateInfo("GL_Transaction", "SysDocID", dataRow["SysDocID"].ToString(), "VoucherID", dataRow["VoucherID"].ToString(), sqlTransaction, !isUpdate);
				string entityName = "Tranaction";
				switch (sysDocTypes)
				{
				case SysDocTypes.OpeningChequeReceipt:
					entityName = "Opening Cheque Receipt";
					break;
				case SysDocTypes.CashReceipt:
					entityName = "Cash Receipt";
					break;
				case SysDocTypes.CashPayment:
					entityName = "Cash Payment";
					break;
				case SysDocTypes.OpeningChequePayment:
					entityName = "Opening Cheque Payment";
					break;
				case SysDocTypes.TTPayment:
					entityName = "TT Payment";
					break;
				case SysDocTypes.TTReceipt:
					entityName = "TT Receipt";
					break;
				}
				flag3 = (isUpdate ? (flag3 & AddActivityLog(entityName, text, text2, ActivityTypes.Update, sqlTransaction)) : (flag3 & AddActivityLog(entityName, text, text2, ActivityTypes.Add, sqlTransaction)));
				if (isUpdate)
				{
					return flag3;
				}
				flag3 &= new SystemDocuments(base.DBConfig).UpdateNextDocumentNumber(dataRow["SysDocID"].ToString(), dataRow["VoucherID"].ToString(), "GL_Transaction", "VoucherID", sqlTransaction);
				return flag3;
			}
			catch
			{
				flag3 = false;
				throw;
			}
			finally
			{
				base.DBConfig.EndTransaction(flag3);
			}
		}

		private string GetInsertUpdateIssuedChequeText(bool isUpdate)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddInsertUpdateParameters("Opening_Cheque_Issued", new FieldValue("SysDocID", "@SysDocID", isUpdateConditionField: true), new FieldValue("VoucherID", "@VoucherID", isUpdateConditionField: true), new FieldValue("ChequeNumber", "@ChequeNumber", isUpdateConditionField: true), new FieldValue("BankID", "@BankID", isUpdateConditionField: true), new FieldValue("PayeeType", "@PayeeType", isUpdateConditionField: true), new FieldValue("PayeeID", "@PayeeID", isUpdateConditionField: true), new FieldValue("PayeeAccountID", "@PayeeAccountID"), new FieldValue("ChequebookID", "@ChequebookID"), new FieldValue("ChequeDate", "@ChequeDate"), new FieldValue("IssueDate", "@IssueDate"), new FieldValue("CurrencyID", "@CurrencyID"), new FieldValue("ExchangeRate", "@ExchangeRate"), new FieldValue("Amount", "@Amount"), new FieldValue("AmountFC", "@AmountFC"), new FieldValue("Note", "@Note"), new FieldValue("Status", "@Status"), new FieldValue("Reference", "@Reference"), new FieldValue("PDCAccountID", "@AccountID"), new FieldValue("ClearanceDate", "@ClearanceDate"), new FieldValue("ClearanceAccountID", "@ClearanceAccountID"), new FieldValue("BankAccountID", "@BankAccountID"), new FieldValue("ClearanceVoucherID", "@ClearanceVoucherID"));
			if (isUpdate)
			{
				sqlBuilder.AddInsertUpdateParameters("Opening_Cheque_Issued", new FieldValue("DateUpdated", "@DateUpdated", isUpdateConditionField: true, checkForNullValue: true));
			}
			if (isUpdate)
			{
				return sqlBuilder.GetUpdateExpression();
			}
			return sqlBuilder.GetInsertExpression();
		}

		private SqlCommand GetInsertUpdateIssuedChequeCommand(bool isUpdate)
		{
			updateCommand = null;
			insertCommand = null;
			SqlParameterCollection parameters;
			if (isUpdate)
			{
				if (updateCommand != null)
				{
					return updateCommand;
				}
				updateCommand = new SqlCommand(GetInsertUpdateIssuedChequeText(isUpdate: true), base.DBConfig.Connection);
				updateCommand.CommandType = CommandType.Text;
				parameters = updateCommand.Parameters;
			}
			else
			{
				if (insertCommand != null)
				{
					return insertCommand;
				}
				insertCommand = new SqlCommand(GetInsertUpdateIssuedChequeText(isUpdate: false), base.DBConfig.Connection);
				insertCommand.CommandType = CommandType.Text;
				parameters = insertCommand.Parameters;
			}
			parameters.Add("@SysDocID", SqlDbType.NVarChar);
			parameters.Add("@VoucherID", SqlDbType.NVarChar);
			parameters.Add("@ChequeNumber", SqlDbType.NVarChar);
			parameters.Add("@BankID", SqlDbType.NVarChar);
			parameters.Add("@PayeeType", SqlDbType.NVarChar);
			parameters.Add("@PayeeID", SqlDbType.NVarChar);
			parameters.Add("@PayeeAccountID", SqlDbType.NVarChar);
			parameters.Add("@ChequebookID", SqlDbType.NVarChar);
			parameters.Add("@ChequeDate", SqlDbType.DateTime);
			parameters.Add("@IssueDate", SqlDbType.DateTime);
			parameters.Add("@CurrencyID", SqlDbType.NVarChar);
			parameters.Add("@ExchangeRate", SqlDbType.SmallMoney);
			parameters.Add("@Amount", SqlDbType.Money);
			parameters.Add("@AmountFC", SqlDbType.Money);
			parameters.Add("@Note", SqlDbType.NVarChar);
			parameters.Add("@Status", SqlDbType.TinyInt);
			parameters.Add("@Reference", SqlDbType.NVarChar);
			parameters.Add("@AccountID", SqlDbType.NVarChar);
			parameters.Add("@ClearanceDate", SqlDbType.DateTime);
			parameters.Add("@ClearanceAccountID", SqlDbType.NVarChar);
			parameters.Add("@BankAccountID", SqlDbType.NVarChar);
			parameters.Add("@ClearanceVoucherID", SqlDbType.NVarChar);
			parameters["@SysDocID"].SourceColumn = "SysDocID";
			parameters["@VoucherID"].SourceColumn = "VoucherID";
			parameters["@ChequeNumber"].SourceColumn = "ChequeNumber";
			parameters["@BankID"].SourceColumn = "BankID";
			parameters["@PayeeType"].SourceColumn = "PayeeType";
			parameters["@PayeeID"].SourceColumn = "PayeeID";
			parameters["@PayeeAccountID"].SourceColumn = "PayeeAccountID";
			parameters["@ChequebookID"].SourceColumn = "ChequebookID";
			parameters["@ChequeDate"].SourceColumn = "ChequeDate";
			parameters["@IssueDate"].SourceColumn = "IssueDate";
			parameters["@CurrencyID"].SourceColumn = "CurrencyID";
			parameters["@ExchangeRate"].SourceColumn = "ExchangeRate";
			parameters["@Amount"].SourceColumn = "Amount";
			parameters["@AmountFC"].SourceColumn = "AmountFC";
			parameters["@Note"].SourceColumn = "Note";
			parameters["@Status"].SourceColumn = "Status";
			parameters["@Reference"].SourceColumn = "Reference";
			parameters["@AccountID"].SourceColumn = "PDCAccountID";
			parameters["@ClearanceDate"].SourceColumn = "ClearanceDate";
			parameters["@ClearanceAccountID"].SourceColumn = "ClearanceAccountID";
			parameters["@BankAccountID"].SourceColumn = "BankAccountID";
			parameters["@ClearanceVoucherID"].SourceColumn = "ClearanceVoucherID";
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

		public bool InsertUpdateIssuedCheque(OpenEntryTransactionData OpenEntryTransactionData, bool isUpdate, SqlTransaction sqlTransaction)
		{
			bool flag = true;
			SqlCommand insertUpdateIssuedChequeCommand = GetInsertUpdateIssuedChequeCommand(isUpdate: false);
			try
			{
				if (sqlTransaction == null)
				{
					sqlTransaction = base.DBConfig.StartNewTransaction();
				}
				string text = OpenEntryTransactionData.IssuedChequeTable.Rows[0]["SysDocID"].ToString();
				string text2 = OpenEntryTransactionData.IssuedChequeTable.Rows[0]["VoucherID"].ToString();
				insertUpdateIssuedChequeCommand.Transaction = sqlTransaction;
				if (isUpdate)
				{
					flag &= DeleteChequeRows(text, text2, sqlTransaction);
				}
				foreach (DataRow row in OpenEntryTransactionData.IssuedChequeTable.Rows)
				{
					string text3 = row["ChequebookID"].ToString();
					string value = new Databases(base.DBConfig).GetFieldValue("Chequebook", "AccountID", "ChequebookID", text3, sqlTransaction).ToString();
					string text4 = row["ChequeNumber"].ToString();
					row["BankAccountID"] = value;
					if (ValidateBlankCheque(text3, text4, "", "") != 0)
					{
						throw new CompanyException("Cheque number:" + text4 + " is already in use.", 2006);
					}
				}
				if (OpenEntryTransactionData.Tables["Opening_Cheque_Issued"].Rows.Count > 0)
				{
					flag &= Insert(OpenEntryTransactionData, "Opening_Cheque_Issued", insertUpdateIssuedChequeCommand);
				}
				if (flag)
				{
					foreach (DataRow row2 in OpenEntryTransactionData.IssuedChequeTable.Rows)
					{
						string text5 = row2["ChequeNumber"].ToString();
						string text6 = row2["ChequebookID"].ToString();
						object obj3 = ExecuteScalar("SELECT MAX(ChequeID) FROM Cheque_Issued WHERE VoucherID='" + text2 + "' AND SysDocID='" + text + "' AND ChequeNumber='" + text5 + "'", sqlTransaction);
						if (obj3 != null && obj3.ToString() != "")
						{
							flag &= (ExecuteNonQuery("UPDATE Cheque_Register SET ChequeID=" + obj3.ToString() + ", Status = 2 WHERE ChequeNumber='" + text5 + "' AND ChequebookID='" + text6 + "'", sqlTransaction) > 0);
						}
					}
				}
				decimal result = default(decimal);
				if (OpenEntryTransactionData.Tables["Opening_Cheque_Issued"].Rows.Count <= 0)
				{
					return flag;
				}
				string a = OpenEntryTransactionData.Tables["Opening_Cheque_Issued"].Rows[0]["PayeeType"].ToString();
				string text7 = OpenEntryTransactionData.Tables["Opening_Cheque_Issued"].Rows[0]["PayeeID"].ToString();
				if (a == "V")
				{
					decimal.TryParse(new Databases(base.DBConfig).GetFieldValue("Vendor", "PDCAmount", "VendorID", text7, sqlTransaction).ToString(), out result);
					foreach (DataRow row3 in OpenEntryTransactionData.Tables["Opening_Cheque_Issued"].Rows)
					{
						decimal result2 = default(decimal);
						decimal.TryParse(row3["Amount"].ToString(), out result2);
						result += result2;
					}
					string commandText = "UPDATE Vendor SET PDCAmount=" + result.ToString() + " WHERE VendorID='" + text7 + "'";
					return flag & Update(commandText, sqlTransaction);
				}
				return flag;
			}
			catch
			{
				flag = false;
				throw;
			}
		}

		private string GetInsertUpdateTextReceivedCheque(bool isUpdate)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddInsertUpdateParameters("Cheque_Received", new FieldValue("SysDocID", "@SysDocID", isUpdateConditionField: true), new FieldValue("VoucherID", "@VoucherID", isUpdateConditionField: true), new FieldValue("ChequeNumber", "@ChequeNumber", isUpdateConditionField: true), new FieldValue("BankID", "@BankID", isUpdateConditionField: true), new FieldValue("PayeeType", "@PayeeType", isUpdateConditionField: true), new FieldValue("PayeeID", "@PayeeID", isUpdateConditionField: true), new FieldValue("ChequeDate", "@ChequeDate"), new FieldValue("ReceiptDate", "@ReceiptDate"), new FieldValue("CurrencyID", "@CurrencyID"), new FieldValue("ExchangeRate", "@ExchangeRate"), new FieldValue("Amount", "@Amount"), new FieldValue("AmountFC", "@AmountFC"), new FieldValue("ConAmountFC", "@ConAmountFC"), new FieldValue("ConRate", "@ConRate"), new FieldValue("Note", "@Note"), new FieldValue("Status", "@Status"), new FieldValue("Reference", "@Reference"), new FieldValue("PDCAccountID", "@PDCAccountID"), new FieldValue("PayeeAccountID", "@PayeeAccountID"), new FieldValue("DepositDate", "@DepositDate"), new FieldValue("DepositAccountID", "@DepositAccountID"), new FieldValue("DepositBankID", "@DepositBankID"), new FieldValue("DepositSysDocID", "@DepositSysDocID"), new FieldValue("DepositVoucherID", "@DepositVoucherID"));
			if (isUpdate)
			{
				sqlBuilder.AddInsertUpdateParameters("Cheque_Received", new FieldValue("DateUpdated", "@DateUpdated", isUpdateConditionField: true, checkForNullValue: true));
			}
			if (isUpdate)
			{
				return sqlBuilder.GetUpdateExpression();
			}
			return sqlBuilder.GetInsertExpression();
		}

		private SqlCommand GetInsertUpdateCommandReceivedCheque(bool isUpdate)
		{
			insertCommand = null;
			updateCommand = null;
			SqlParameterCollection parameters;
			if (isUpdate)
			{
				if (updateCommand != null)
				{
					return updateCommand;
				}
				updateCommand = new SqlCommand(GetInsertUpdateTextReceivedCheque(isUpdate: true), base.DBConfig.Connection);
				updateCommand.CommandType = CommandType.Text;
				parameters = updateCommand.Parameters;
			}
			else
			{
				if (insertCommand != null)
				{
					return insertCommand;
				}
				insertCommand = new SqlCommand(GetInsertUpdateTextReceivedCheque(isUpdate: false), base.DBConfig.Connection);
				insertCommand.CommandType = CommandType.Text;
				parameters = insertCommand.Parameters;
			}
			parameters.Add("@SysDocID", SqlDbType.NVarChar);
			parameters.Add("@VoucherID", SqlDbType.NVarChar);
			parameters.Add("@ChequeNumber", SqlDbType.NVarChar);
			parameters.Add("@BankID", SqlDbType.NVarChar);
			parameters.Add("@PayeeType", SqlDbType.NVarChar);
			parameters.Add("@PayeeID", SqlDbType.NVarChar);
			parameters.Add("@PayeeAccountID", SqlDbType.NVarChar);
			parameters.Add("@ChequeDate", SqlDbType.DateTime);
			parameters.Add("@ReceiptDate", SqlDbType.DateTime);
			parameters.Add("@CurrencyID", SqlDbType.NVarChar);
			parameters.Add("@ExchangeRate", SqlDbType.SmallMoney);
			parameters.Add("@Amount", SqlDbType.Money);
			parameters.Add("@AmountFC", SqlDbType.Money);
			parameters.Add("@ConAmountFC", SqlDbType.Money);
			parameters.Add("@ConRate", SqlDbType.Decimal);
			parameters.Add("@Note", SqlDbType.NVarChar);
			parameters.Add("@Status", SqlDbType.TinyInt);
			parameters.Add("@Reference", SqlDbType.NVarChar);
			parameters.Add("@PDCAccountID", SqlDbType.NVarChar);
			parameters.Add("@DepositDate", SqlDbType.DateTime);
			parameters.Add("@DepositAccountID", SqlDbType.NVarChar);
			parameters.Add("@DepositBankID", SqlDbType.NVarChar);
			parameters.Add("@DepositVoucherID", SqlDbType.NVarChar);
			parameters.Add("@DepositSysDocID", SqlDbType.NVarChar);
			parameters["@SysDocID"].SourceColumn = "SysDocID";
			parameters["@VoucherID"].SourceColumn = "VoucherID";
			parameters["@ChequeNumber"].SourceColumn = "ChequeNumber";
			parameters["@BankID"].SourceColumn = "BankID";
			parameters["@PayeeType"].SourceColumn = "PayeeType";
			parameters["@PayeeID"].SourceColumn = "PayeeID";
			parameters["@PayeeAccountID"].SourceColumn = "PayeeAccountID";
			parameters["@ChequeDate"].SourceColumn = "ChequeDate";
			parameters["@ReceiptDate"].SourceColumn = "ReceiptDate";
			parameters["@CurrencyID"].SourceColumn = "CurrencyID";
			parameters["@ExchangeRate"].SourceColumn = "ExchangeRate";
			parameters["@Amount"].SourceColumn = "Amount";
			parameters["@AmountFC"].SourceColumn = "AmountFC";
			parameters["@ConAmountFC"].SourceColumn = "ConAmountFC";
			parameters["@ConRate"].SourceColumn = "ConRate";
			parameters["@Note"].SourceColumn = "Note";
			parameters["@Status"].SourceColumn = "Status";
			parameters["@Reference"].SourceColumn = "Reference";
			parameters["@PDCAccountID"].SourceColumn = "PDCAccountID";
			parameters["@DepositDate"].SourceColumn = "DepositDate";
			parameters["@DepositAccountID"].SourceColumn = "DepositAccountID";
			parameters["@DepositBankID"].SourceColumn = "DepositBankID";
			parameters["@DepositSysDocID"].SourceColumn = "DepositSysDocID";
			parameters["@DepositVoucherID"].SourceColumn = "DepositVoucherID";
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

		public bool InsertUpdateReceivedCheque(OpenEntryTransactionData receivedChequeData, bool isUpdate, SqlTransaction sqlTransaction)
		{
			bool flag = true;
			SqlCommand insertUpdateCommandReceivedCheque = GetInsertUpdateCommandReceivedCheque(isUpdate: false);
			try
			{
				string sysDocID = receivedChequeData.ReceivedChequeTable.Rows[0]["SysDocID"].ToString();
				string voucherID = receivedChequeData.ReceivedChequeTable.Rows[0]["VoucherID"].ToString();
				if (sqlTransaction == null)
				{
					sqlTransaction = base.DBConfig.StartNewTransaction();
				}
				insertUpdateCommandReceivedCheque.Transaction = sqlTransaction;
				if (isUpdate)
				{
					flag &= DeleteChequeRows(sysDocID, voucherID, sqlTransaction);
				}
				_ = receivedChequeData.ReceivedChequeTable.Rows[0];
				string baseCurrencyID = new APJournal(base.DBConfig).GetBaseCurrencyID();
				string commaSeperatedIDs = GetCommaSeperatedIDs(receivedChequeData, "Cheque_Received", "PayeeID");
				string textCommand = "SELECT CustomerID,ISNULL(CurrencyID,'" + baseCurrencyID + "') AS CurrencyID FROM Customer WHERE CustomerID IN (" + commaSeperatedIDs + ")";
				DataSet dataSet = new DataSet();
				FillDataSet(dataSet, "Customer", textCommand);
				DataSet dataSet2 = new DataSet();
				textCommand = "SELECT AccountID,ISNULL(CurrencyID,'" + baseCurrencyID + "') AS CurrencyID FROM Account WHERE AccountID IN (" + commaSeperatedIDs + ")";
				FillDataSet(dataSet2, "Account", textCommand);
				DataSet dataSet3 = new DataSet();
				textCommand = "SELECT VendorID,ISNULL(CurrencyID,'" + baseCurrencyID + "') AS CurrencyID FROM Vendor WHERE VendorID IN (" + commaSeperatedIDs + ")";
				FillDataSet(dataSet3, "Vendor", textCommand);
				foreach (DataRow row in receivedChequeData.Tables["Cheque_Received"].Rows)
				{
					string a = row["CurrencyID"].ToString();
					string str = row["PayeeID"].ToString();
					string text = "AED";
					if (dataSet.Tables[0].Rows.Count > 0)
					{
						text = dataSet.Tables[0].Select("CustomerID = '" + str + "'")[0]["CurrencyID"].ToString();
					}
					if (dataSet2.Tables[0].Rows.Count > 0)
					{
						text = dataSet2.Tables[0].Select("AccountID = '" + str + "'")[0]["CurrencyID"].ToString();
					}
					if (dataSet3.Tables[0].Rows.Count > 0)
					{
						text = dataSet3.Tables[0].Select("VendorID = '" + str + "'")[0]["CurrencyID"].ToString();
					}
					if (text != baseCurrencyID && a != text)
					{
						string text2 = CommonLib.ToSqlDateTimeString(DateTime.Parse(row["ReceiptDate"].ToString()));
						decimal d = default(decimal);
						decimal num = 1m;
						if (receivedChequeData.Tables["Cheque_Received"].Columns.Contains("Debit") && row["Debit"] != DBNull.Value)
						{
							d = decimal.Parse(row["Debit"].ToString());
						}
						if (receivedChequeData.Tables["Cheque_Received"].Columns.Contains("Credit") && row["Credit"] != DBNull.Value)
						{
							decimal.Parse(row["Credit"].ToString());
						}
						textCommand = " SELECT DISTINCT ISNULL((SELECT TOP 1 Ex.ExchangeRate FROM Currency_Exchange_Rate EX WHERE Ex.CurrencyID = '" + text + "' AND Ex.RateUpdatedDate < '" + text2 + "'),\r\n                                     (SELECT Cur.ExchangeRate FROM Currency Cur WHERE Cur.CurrencyID = '" + text + "'))  AS CurRate FROM Currency";
						object obj = ExecuteScalar(textCommand, sqlTransaction);
						if (!obj.IsNullOrEmpty())
						{
							num = decimal.Parse(obj.ToString());
						}
						string currencyRateType = new Currencies(base.DBConfig).GetCurrencyRateType(text);
						row["ConRate"] = num;
						if (d != 0m)
						{
							if (currencyRateType == "M")
							{
								row["ConAmountFC"] = Math.Round(d / num, 4);
							}
							else
							{
								row["ConAmountFC"] = Math.Round(d * num, 4);
							}
						}
					}
				}
				if (receivedChequeData.Tables["Cheque_Received"].Rows.Count > 0)
				{
					flag &= Insert(receivedChequeData, "Cheque_Received", insertUpdateCommandReceivedCheque);
				}
				decimal result = default(decimal);
				if (receivedChequeData.Tables["Cheque_Received"].Rows.Count <= 0)
				{
					return flag;
				}
				string a2 = receivedChequeData.Tables["Cheque_Received"].Rows[0]["PayeeType"].ToString();
				string text3 = receivedChequeData.Tables["Cheque_Received"].Rows[0]["PayeeID"].ToString();
				if (a2 == "C")
				{
					decimal.TryParse(new Databases(base.DBConfig).GetFieldValue("Customer", "PDCAmount", "CustomerID", text3, sqlTransaction).ToString(), out result);
					foreach (DataRow row2 in receivedChequeData.Tables["Cheque_Received"].Rows)
					{
						decimal result2 = default(decimal);
						decimal.TryParse(row2["Amount"].ToString(), out result2);
						result += result2;
					}
					textCommand = "UPDATE Customer SET PDCAmount=" + result.ToString() + " WHERE CustomerID='" + text3 + "'";
					return flag & Update(textCommand, sqlTransaction);
				}
				return flag;
			}
			catch
			{
				flag = false;
				throw;
			}
		}

		public bool DeleteChequeRows(string sysDocID, string voucherID, SqlTransaction sqlTransaction)
		{
			bool flag = true;
			try
			{
				decimal result = default(decimal);
				decimal result2 = default(decimal);
				string a = "";
				string text = "";
				string exp = "SELECT TOP 1 PayeeType FROM Cheque_Received WHERE SysDocID='" + sysDocID + "' AND VoucherID='" + voucherID + "'";
				object obj = ExecuteScalar(exp, sqlTransaction);
				if (obj != null)
				{
					a = obj.ToString();
				}
				exp = "SELECT TOP 1 PayeeID FROM Cheque_Received WHERE SysDocID='" + sysDocID + "' AND VoucherID='" + voucherID + "'";
				obj = ExecuteScalar(exp, sqlTransaction);
				if (obj != null)
				{
					text = obj.ToString();
				}
				if (a == "C" && text != "")
				{
					decimal.TryParse(new Databases(base.DBConfig).GetFieldValue("Customer", "PDCAmount", "CustomerID", text, sqlTransaction).ToString(), out result);
					exp = "SELECT SUM(ISNULL(Amount,0)) FROM Cheque_Received WHERE SysDocID='" + sysDocID + "' AND VoucherID = '" + voucherID + "'";
					decimal.TryParse(ExecuteScalar(exp, sqlTransaction).ToString(), out result2);
					if (result2 != 0m)
					{
						result -= result2;
						exp = "UPDATE Customer SET PDCAmount=" + result.ToString() + " WHERE CustomerID='" + text + "'";
						flag &= Update(exp, sqlTransaction);
					}
				}
				string commandText = "DELETE FROM Cheque_Received WHERE SysDocID = '" + sysDocID + "' AND VoucherID = '" + voucherID + "'";
				return flag & Delete(commandText, sqlTransaction);
			}
			catch
			{
				throw;
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

		private decimal GetEquivalantFCAmount(decimal baseAmount, string currencyID, decimal rate)
		{
			int currencyDecimalPoints = new CompanyInformations(base.DBConfig).CurrencyDecimalPoints;
			if (new Currencies(base.DBConfig).GetCurrencyRateType(currencyID) == "M")
			{
				return Math.Round(baseAmount / rate, currencyDecimalPoints);
			}
			return Math.Round(baseAmount * rate, currencyDecimalPoints);
		}

		private GLData CreateIssuedChequeClearanceGLData(OpenEntryTransactionData transactionData, DataSet chequeData, SqlTransaction sqlTransaction)
		{
			GLData gLData = new GLData();
			bool flag = bool.Parse(new CompanyOption(base.DBConfig).GetCompanyOptionValue(CompanyOptionsEnum.PDCIssuedByMaturity, false).ToString());
			DataRow dataRow = transactionData.TransactionTable.Rows[0];
			DataRow dataRow2 = gLData.JournalTable.NewRow();
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
			foreach (DataRow row in chequeData.Tables[0].Rows)
			{
				string value = "Issued Chq. No:" + row["ChequeNumber"].ToString() + ", IV:" + row["VoucherID"].ToString();
				DataRow dataRow4 = gLData.JournalDetailsTable.NewRow();
				dataRow4.BeginEdit();
				dataRow4["JournalID"] = 0;
				dataRow4["AccountID"] = row["BankAccountID"];
				dataRow4["Debit"] = DBNull.Value;
				dataRow4["DebitFC"] = DBNull.Value;
				dataRow4["Credit"] = row["Amount"];
				dataRow4["CreditFC"] = row["AmountFC"];
				dataRow4["RowIndex"] = -1;
				dataRow4["CheckID"] = row["ChequeID"];
				dataRow4["CheckNumber"] = row["ChequeNumber"];
				dataRow4["CheckDate"] = row["ChequeDate"];
				dataRow4["CheckbookID"] = row["ChequebookID"];
				dataRow4["Description"] = value;
				dataRow4.EndEdit();
				gLData.JournalDetailsTable.Rows.Add(dataRow4);
				if (flag)
				{
					string text = row["SysDocID"].ToString();
					string voucherID = row["VoucherID"].ToString();
					bool flag2 = false;
					SysDocTypes systemDocumentType = new SystemDocuments(base.DBConfig).GetSystemDocumentType(text, sqlTransaction);
					OpenEntryTransactionData openEntryTransactionData = null;
					if (systemDocumentType == SysDocTypes.OpeningChequePayment)
					{
						openEntryTransactionData = GetTransactionByID(text, voucherID, sqlTransaction);
						if (!openEntryTransactionData.TransactionTable.Rows[0]["IsSecondForm"].IsDBNullOrEmpty())
						{
							flag2 = bool.Parse(openEntryTransactionData.TransactionTable.Rows[0]["IsSecondForm"].ToString());
						}
					}
					if (systemDocumentType == SysDocTypes.OpeningChequePayment && flag2)
					{
						foreach (DataRow row2 in openEntryTransactionData.Tables["Transaction_Details"].Rows)
						{
							string text2 = row2["PayeeType"].ToString();
							string text3 = row2["PayeeID"].ToString();
							dataRow4 = gLData.JournalDetailsTable.NewRow();
							dataRow4.BeginEdit();
							dataRow4["JournalID"] = 0;
							string text4 = "";
							if (text2 == "C")
							{
								text4 = new Customers(base.DBConfig).GetCustomerARAccountID(text, text3);
							}
							else if (text2 == "V")
							{
								text4 = new Vendors(base.DBConfig).GetVendorAPAccountID(text, text3);
							}
							else if (text2 == "E")
							{
								text4 = new Employees(base.DBConfig).GetEmployeeAccountID(text, text3);
							}
							else if (text2 == "A")
							{
								text4 = row2["AccountID"].ToString();
							}
							if (text4 == "")
							{
								throw new CompanyException("AccountID should be selected for the payee if a payee is in transaction.", 1028);
							}
							dataRow4["AccountID"] = text4;
							dataRow4["PayeeID"] = row2["PayeeID"];
							switch (text2)
							{
							case "C":
							case "V":
							case "E":
								dataRow4["IsARAP"] = true;
								break;
							}
							dataRow4["PayeeType"] = text2;
							dataRow4["Debit"] = row2["Amount"];
							dataRow4["DebitFC"] = row2["AmountFC"];
							dataRow4["Credit"] = DBNull.Value;
							dataRow4["CreditFC"] = DBNull.Value;
							dataRow4["RowIndex"] = 0;
							dataRow4["CheckID"] = row["ChequeID"];
							dataRow4["CheckNumber"] = row["ChequeNumber"];
							dataRow4["CheckDate"] = row["ChequeDate"];
							dataRow4["CheckbookID"] = row["ChequebookID"];
							dataRow4["Description"] = value;
							dataRow4.EndEdit();
							gLData.JournalDetailsTable.Rows.Add(dataRow4);
						}
					}
					else
					{
						dataRow4 = gLData.JournalDetailsTable.NewRow();
						dataRow4.BeginEdit();
						dataRow4["JournalID"] = 0;
						dataRow4["AccountID"] = row["PayeeAccountID"];
						dataRow4["PayeeID"] = row["PayeeID"];
						if (row["PayeeType"] != DBNull.Value)
						{
							dataRow4["IsARAP"] = true;
						}
						dataRow4["PayeeType"] = row["PayeeType"];
						dataRow4["Debit"] = row["Amount"];
						dataRow4["DebitFC"] = row["AmountFC"];
						dataRow4["Credit"] = DBNull.Value;
						dataRow4["CreditFC"] = DBNull.Value;
						dataRow4["RowIndex"] = 0;
						dataRow4["CheckID"] = row["ChequeID"];
						dataRow4["CheckNumber"] = row["ChequeNumber"];
						dataRow4["CheckDate"] = row["ChequeDate"];
						dataRow4["CheckbookID"] = row["ChequebookID"];
						dataRow4["Description"] = value;
						dataRow4.EndEdit();
						gLData.JournalDetailsTable.Rows.Add(dataRow4);
					}
				}
				else
				{
					dataRow4 = gLData.JournalDetailsTable.NewRow();
					dataRow4.BeginEdit();
					dataRow4["JournalID"] = 0;
					dataRow4["AccountID"] = row["PDCAccountID"];
					dataRow4["Debit"] = row["Amount"];
					dataRow4["DebitFC"] = row["AmountFC"];
					dataRow4["Credit"] = DBNull.Value;
					dataRow4["CreditFC"] = DBNull.Value;
					dataRow4["RowIndex"] = 0;
					dataRow4["CheckID"] = row["ChequeID"];
					dataRow4["CheckNumber"] = row["ChequeNumber"];
					dataRow4["CheckDate"] = row["ChequeDate"];
					dataRow4["CheckbookID"] = row["ChequebookID"];
					dataRow4["Description"] = value;
					dataRow4.EndEdit();
					gLData.JournalDetailsTable.Rows.Add(dataRow4);
				}
			}
			return gLData;
		}

		private GLData CreateChequeDepositGLData(OpenEntryTransactionData transactionData)
		{
			bool flag = bool.Parse(new CompanyOption(base.DBConfig).GetCompanyOptionValue(CompanyOptionsEnum.PDCByMaturity, true).ToString());
			GLData gLData = new GLData();
			DataRow dataRow = transactionData.TransactionTable.Rows[0];
			DataRow dataRow2 = gLData.JournalTable.NewRow();
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
			DataRow dataRow3 = null;
			foreach (DataRow row in transactionData.TransactionDetailsTable.Rows)
			{
				int num = int.Parse(row["ChequeID"].ToString());
				DataRow dataRow5 = new ReceivedCheques(base.DBConfig).GetChequeByID(num.ToString()).Tables[0].Rows[0];
				int num2 = int.Parse(dataRow5["Status"].ToString());
				string value = "Chq.Mat No:" + dataRow5["ChequeNumber"].ToString() + ", RV:" + dataRow5["VoucherID"].ToString();
				string a = dataRow5["DiscountVoucherID"].ToString();
				if (num2 == 3 && a != "")
				{
					decimal d = decimal.Parse(dataRow5["Amount"].ToString());
					decimal result = default(decimal);
					decimal.TryParse(dataRow5["DiscountAmount"].ToString(), out result);
					decimal num3 = d - result;
					if (num3 > 0m)
					{
						dataRow3 = gLData.JournalDetailsTable.NewRow();
						dataRow3.BeginEdit();
						dataRow3["JournalID"] = 0;
						dataRow3["AccountID"] = dataRow["FirstAccountID"];
						dataRow3["Debit"] = num3;
						dataRow3["DebitFC"] = DBNull.Value;
						dataRow3["Credit"] = DBNull.Value;
						dataRow3["CreditFC"] = DBNull.Value;
						dataRow3["CheckID"] = num;
						dataRow3["CheckNumber"] = dataRow5["ChequeNumber"];
						dataRow3["CheckDate"] = dataRow5["ChequeDate"];
						dataRow3["Description"] = value;
						dataRow3["Reference"] = dataRow["Reference"];
						dataRow3["RowIndex"] = -1;
						dataRow3.EndEdit();
						gLData.JournalDetailsTable.Rows.Add(dataRow3);
					}
					dataRow3 = gLData.JournalDetailsTable.NewRow();
					dataRow3.BeginEdit();
					dataRow3["JournalID"] = 0;
					string text2 = (string)(dataRow3["AccountID"] = dataRow5["DiscountAccountID"].ToString());
					dataRow3["Debit"] = result;
					dataRow3["DebitFC"] = DBNull.Value;
					dataRow3["Credit"] = DBNull.Value;
					dataRow3["CreditFC"] = DBNull.Value;
					dataRow3["CheckID"] = num;
					dataRow3["CheckNumber"] = dataRow5["ChequeNumber"];
					dataRow3["CheckDate"] = dataRow5["ChequeDate"];
					dataRow3["Description"] = value;
					dataRow3["Reference"] = dataRow["Reference"];
					dataRow3["RowIndex"] = -1;
					dataRow3.EndEdit();
					gLData.JournalDetailsTable.Rows.Add(dataRow3);
				}
				else
				{
					dataRow3 = gLData.JournalDetailsTable.NewRow();
					dataRow3.BeginEdit();
					dataRow3["JournalID"] = 0;
					dataRow3["AccountID"] = dataRow["FirstAccountID"];
					dataRow3["Debit"] = row["Amount"];
					dataRow3["DebitFC"] = row["AmountFC"];
					dataRow3["Credit"] = DBNull.Value;
					dataRow3["CreditFC"] = DBNull.Value;
					dataRow3["CheckID"] = num;
					dataRow3["CheckNumber"] = dataRow5["ChequeNumber"];
					dataRow3["CheckDate"] = dataRow5["ChequeDate"];
					dataRow3["Description"] = value;
					dataRow3["Reference"] = dataRow["Reference"];
					dataRow3["RowIndex"] = -1;
					dataRow3.EndEdit();
					gLData.JournalDetailsTable.Rows.Add(dataRow3);
				}
				if (flag)
				{
					dataRow3 = gLData.JournalDetailsTable.NewRow();
					dataRow3.BeginEdit();
					dataRow3["JournalID"] = 0;
					dataRow3["PayeeType"] = row["PayeeType"];
					dataRow3["PayeeID"] = row["PayeeID"];
					dataRow3["AccountID"] = row["AccountID"];
					if (row["PayeeType"] != DBNull.Value)
					{
						dataRow3["IsARAP"] = true;
					}
					dataRow3["Debit"] = DBNull.Value;
					dataRow3["DebitFC"] = DBNull.Value;
					dataRow3["CreditFC"] = row["AmountFC"];
					dataRow3["Credit"] = row["Amount"];
					dataRow3["CheckID"] = num;
					dataRow3["CheckNumber"] = dataRow5["ChequeNumber"];
					dataRow3["CheckDate"] = dataRow5["ChequeDate"];
					dataRow3["Description"] = value;
					dataRow3["Reference"] = dataRow["Reference"];
					dataRow3["RowIndex"] = 0;
					dataRow3.EndEdit();
					gLData.JournalDetailsTable.Rows.Add(dataRow3);
				}
				else
				{
					dataRow3 = gLData.JournalDetailsTable.NewRow();
					dataRow3.BeginEdit();
					dataRow3["JournalID"] = 0;
					dataRow3["AccountID"] = row["AccountID"];
					dataRow3["Debit"] = DBNull.Value;
					dataRow3["DebitFC"] = DBNull.Value;
					dataRow3["Credit"] = row["Amount"];
					dataRow3["CreditFC"] = row["AmountFC"];
					dataRow3["CheckID"] = num;
					dataRow3["CheckNumber"] = dataRow5["ChequeNumber"];
					dataRow3["CheckDate"] = dataRow5["ChequeDate"];
					dataRow3["Description"] = value;
					dataRow3["Reference"] = dataRow["Reference"];
					dataRow3["RowIndex"] = 0;
					dataRow3.EndEdit();
					gLData.JournalDetailsTable.Rows.Add(dataRow3);
				}
			}
			return gLData;
		}

		private GLData CreateReceivedReturnedChequeTransactionGLData(OpenEntryTransactionData transactionData)
		{
			bool flag = bool.Parse(new CompanyOption(base.DBConfig).GetCompanyOptionValue(CompanyOptionsEnum.PDCByMaturity, true).ToString());
			GLData gLData = new GLData();
			DataRow dataRow = transactionData.TransactionTable.Rows[0];
			DataRow dataRow2 = gLData.JournalTable.NewRow();
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
			DataRow dataRow3 = gLData.JournalDetailsTable.NewRow();
			dataRow3.BeginEdit();
			dataRow3["JournalID"] = 0;
			dataRow3["AccountID"] = dataRow["FirstAccountID"];
			dataRow3["Debit"] = DBNull.Value;
			dataRow3["DebitFC"] = DBNull.Value;
			dataRow3["Credit"] = dataRow["Amount"];
			dataRow3["CreditFC"] = dataRow["AmountFC"];
			dataRow3["CheckID"] = dataRow["ChequeID"];
			dataRow3["CheckNumber"] = dataRow["CheckNumber"];
			dataRow3["CheckDate"] = dataRow["CheckDate"];
			dataRow3["Description"] = dataRow["Description"];
			dataRow3["Reference"] = dataRow["Reference"];
			dataRow3["RowIndex"] = -1;
			dataRow3.EndEdit();
			gLData.JournalDetailsTable.Rows.Add(dataRow3);
			dataRow3 = gLData.JournalDetailsTable.NewRow();
			dataRow3.BeginEdit();
			dataRow3["JournalID"] = 0;
			dataRow3["AccountID"] = dataRow["SecondAccountID"];
			if (flag)
			{
				dataRow3["PayeeType"] = dataRow["PayeeType"];
				dataRow3["PayeeID"] = dataRow["PayeeID"];
				if (dataRow3["PayeeType"] != DBNull.Value)
				{
					dataRow3["IsARAP"] = true;
				}
			}
			dataRow3["Debit"] = dataRow["Amount"];
			dataRow3["DebitFC"] = dataRow["AmountFC"];
			dataRow3["Credit"] = DBNull.Value;
			dataRow3["CreditFC"] = DBNull.Value;
			dataRow3["CheckID"] = dataRow["ChequeID"];
			dataRow3["CheckNumber"] = dataRow["CheckNumber"];
			dataRow3["CheckDate"] = dataRow["CheckDate"];
			dataRow3["Description"] = dataRow["Description"];
			dataRow3["Reference"] = dataRow["Reference"];
			dataRow3["RowIndex"] = 0;
			dataRow3.EndEdit();
			gLData.JournalDetailsTable.Rows.Add(dataRow3);
			return gLData;
		}

		private GLData CreateIssuedReturnedChequeTransactionGLData(OpenEntryTransactionData transactionData)
		{
			bool flag = bool.Parse(new CompanyOption(base.DBConfig).GetCompanyOptionValue(CompanyOptionsEnum.PDCIssuedByMaturity, false).ToString());
			GLData gLData = new GLData();
			DataRow dataRow = transactionData.TransactionTable.Rows[0];
			DataRow dataRow2 = gLData.JournalTable.NewRow();
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
			DataRow dataRow3 = gLData.JournalDetailsTable.NewRow();
			dataRow3.BeginEdit();
			dataRow3["JournalID"] = 0;
			dataRow3["AccountID"] = dataRow["FirstAccountID"];
			dataRow3["Debit"] = dataRow["Amount"];
			dataRow3["DebitFC"] = dataRow["AmountFC"];
			dataRow3["Credit"] = DBNull.Value;
			dataRow3["CreditFC"] = DBNull.Value;
			dataRow3["CheckID"] = dataRow["ChequeID"];
			dataRow3["CheckNumber"] = dataRow["CheckNumber"];
			dataRow3["CheckDate"] = dataRow["CheckDate"];
			dataRow3["Description"] = dataRow["Description"];
			dataRow3["Reference"] = dataRow["Reference"];
			dataRow3["RowIndex"] = -1;
			dataRow3.EndEdit();
			gLData.JournalDetailsTable.Rows.Add(dataRow3);
			dataRow3 = gLData.JournalDetailsTable.NewRow();
			dataRow3.BeginEdit();
			dataRow3["JournalID"] = 0;
			dataRow3["AccountID"] = dataRow["SecondAccountID"];
			if (flag)
			{
				dataRow3["PayeeType"] = dataRow["PayeeType"];
				dataRow3["PayeeID"] = dataRow["PayeeID"];
				if (dataRow3["PayeeType"] != DBNull.Value)
				{
					dataRow3["IsARAP"] = true;
				}
			}
			dataRow3["Debit"] = DBNull.Value;
			dataRow3["DebitFC"] = DBNull.Value;
			dataRow3["Credit"] = dataRow["Amount"];
			dataRow3["CreditFC"] = dataRow["AmountFC"];
			dataRow3["CheckID"] = dataRow["ChequeID"];
			dataRow3["CheckNumber"] = dataRow["CheckNumber"];
			dataRow3["CheckDate"] = dataRow["CheckDate"];
			dataRow3["Description"] = dataRow["Description"];
			dataRow3["Reference"] = dataRow["Reference"];
			dataRow3["RowIndex"] = 0;
			dataRow3.EndEdit();
			gLData.JournalDetailsTable.Rows.Add(dataRow3);
			return gLData;
		}

		private GLData CreateCancelledReceivedChequeTransactionGLData(OpenEntryTransactionData transactionData)
		{
			GLData gLData = new GLData();
			DataRow dataRow = transactionData.TransactionTable.Rows[0];
			DataRow dataRow2 = gLData.JournalTable.NewRow();
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
			DataRow dataRow3 = gLData.JournalDetailsTable.NewRow();
			dataRow3.BeginEdit();
			dataRow3["JournalID"] = 0;
			dataRow3["AccountID"] = dataRow["FirstAccountID"];
			dataRow3["Debit"] = DBNull.Value;
			dataRow3["DebitFC"] = DBNull.Value;
			dataRow3["Credit"] = dataRow["Amount"];
			dataRow3["CreditFC"] = dataRow["AmountFC"];
			dataRow3["CheckID"] = dataRow["ChequeID"];
			dataRow3["CheckNumber"] = dataRow["CheckNumber"];
			dataRow3["CheckDate"] = dataRow["CheckDate"];
			dataRow3["Description"] = dataRow["Description"];
			dataRow3["Reference"] = dataRow["Reference"];
			dataRow3["RowIndex"] = -1;
			dataRow3.EndEdit();
			gLData.JournalDetailsTable.Rows.Add(dataRow3);
			dataRow3 = gLData.JournalDetailsTable.NewRow();
			dataRow3.BeginEdit();
			dataRow3["JournalID"] = 0;
			dataRow3["AccountID"] = dataRow["SecondAccountID"];
			if (dataRow["PayeeType"] != DBNull.Value)
			{
				dataRow3["IsARAP"] = true;
			}
			dataRow3["Debit"] = dataRow["Amount"];
			dataRow3["DebitFC"] = dataRow["AmountFC"];
			dataRow3["Credit"] = DBNull.Value;
			dataRow3["CreditFC"] = DBNull.Value;
			dataRow3["CheckID"] = dataRow["ChequeID"];
			dataRow3["CheckNumber"] = dataRow["CheckNumber"];
			dataRow3["CheckDate"] = dataRow["CheckDate"];
			dataRow3["Description"] = dataRow["Description"];
			dataRow3["Reference"] = dataRow["Reference"];
			dataRow3["RowIndex"] = 0;
			dataRow3.EndEdit();
			gLData.JournalDetailsTable.Rows.Add(dataRow3);
			return gLData;
		}

		private GLData CreateChangedStatusChequeTransactionGLData(OpenEntryTransactionData transactionData)
		{
			bool flag = bool.Parse(new CompanyOption(base.DBConfig).GetCompanyOptionValue(CompanyOptionsEnum.PDCByMaturity, true).ToString());
			GLData gLData = new GLData();
			DataRow dataRow = transactionData.TransactionTable.Rows[0];
			DataRow dataRow2 = gLData.JournalTable.NewRow();
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
			DataRow dataRow3 = gLData.JournalDetailsTable.NewRow();
			dataRow3.BeginEdit();
			dataRow3["JournalID"] = 0;
			dataRow3["AccountID"] = dataRow["FirstAccountID"];
			dataRow3["Debit"] = DBNull.Value;
			dataRow3["DebitFC"] = DBNull.Value;
			dataRow3["Credit"] = dataRow["Amount"];
			dataRow3["CreditFC"] = dataRow["AmountFC"];
			dataRow3["CheckID"] = dataRow["ChequeID"];
			dataRow3["CheckNumber"] = dataRow["CheckNumber"];
			dataRow3["CheckDate"] = dataRow["CheckDate"];
			dataRow3["Description"] = dataRow["Description"];
			dataRow3["Reference"] = dataRow["Reference"];
			dataRow3["RowIndex"] = -1;
			dataRow3.EndEdit();
			gLData.JournalDetailsTable.Rows.Add(dataRow3);
			dataRow3 = gLData.JournalDetailsTable.NewRow();
			dataRow3.BeginEdit();
			dataRow3["JournalID"] = 0;
			dataRow3["AccountID"] = dataRow["SecondAccountID"];
			if (flag)
			{
				dataRow3["PayeeType"] = dataRow["PayeeType"];
				dataRow3["PayeeID"] = dataRow["PayeeID"];
				if (dataRow3["PayeeType"] != DBNull.Value)
				{
					dataRow3["IsARAP"] = true;
				}
			}
			dataRow3["Debit"] = dataRow["Amount"];
			dataRow3["DebitFC"] = dataRow["AmountFC"];
			dataRow3["Credit"] = DBNull.Value;
			dataRow3["CreditFC"] = DBNull.Value;
			dataRow3["CheckID"] = dataRow["ChequeID"];
			dataRow3["CheckNumber"] = dataRow["CheckNumber"];
			dataRow3["CheckDate"] = dataRow["CheckDate"];
			dataRow3["Description"] = dataRow["Description"];
			dataRow3["Reference"] = dataRow["Reference"];
			dataRow3["RowIndex"] = 0;
			dataRow3.EndEdit();
			gLData.JournalDetailsTable.Rows.Add(dataRow3);
			return gLData;
		}

		private GLData CreateCancelledIssuedChequeTransactionGLData(OpenEntryTransactionData transactionData)
		{
			GLData gLData = new GLData();
			DataRow dataRow = transactionData.TransactionTable.Rows[0];
			DataRow dataRow2 = gLData.JournalTable.NewRow();
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
			DataRow dataRow3 = gLData.JournalDetailsTable.NewRow();
			dataRow3.BeginEdit();
			dataRow3["JournalID"] = 0;
			dataRow3["AccountID"] = dataRow["FirstAccountID"];
			dataRow3["Debit"] = dataRow["Amount"];
			dataRow3["DebitFC"] = dataRow["AmountFC"];
			dataRow3["Credit"] = DBNull.Value;
			dataRow3["CreditFC"] = DBNull.Value;
			dataRow3["CheckID"] = dataRow["ChequeID"];
			dataRow3["CheckNumber"] = dataRow["CheckNumber"];
			dataRow3["CheckDate"] = dataRow["CheckDate"];
			dataRow3["CheckbookID"] = dataRow["ChequebookID"];
			dataRow3["PayeeType"] = dataRow["PayeeType"];
			dataRow3["PayeeID"] = dataRow["PayeeID"];
			if (dataRow["Description"] == null || dataRow["Description"].ToString() == "")
			{
				dataRow3["Description"] = "Cancelled Cheque No:" + dataRow["CheckNumber"] + "Amnt:" + dataRow["Amount"];
			}
			else
			{
				dataRow3["Description"] = dataRow["Description"] + "Cheque No:" + dataRow["CheckNumber"] + "Amnt:" + dataRow["Amount"];
			}
			dataRow3["Reference"] = dataRow["Reference"];
			dataRow3["RowIndex"] = -1;
			dataRow3.EndEdit();
			gLData.JournalDetailsTable.Rows.Add(dataRow3);
			dataRow3 = gLData.JournalDetailsTable.NewRow();
			dataRow3.BeginEdit();
			dataRow3["JournalID"] = 0;
			dataRow3["AccountID"] = dataRow["SecondAccountID"];
			if (dataRow["PayeeType"] != DBNull.Value)
			{
				dataRow3["IsARAP"] = true;
			}
			dataRow3["Debit"] = DBNull.Value;
			dataRow3["DebitFC"] = DBNull.Value;
			dataRow3["Credit"] = dataRow["Amount"];
			dataRow3["CreditFC"] = dataRow["AmountFC"];
			dataRow3["PayeeType"] = dataRow["PayeeType"];
			dataRow3["PayeeID"] = dataRow["PayeeID"];
			dataRow3["CheckID"] = dataRow["ChequeID"];
			dataRow3["CheckNumber"] = dataRow["CheckNumber"];
			dataRow3["CheckDate"] = dataRow["CheckDate"];
			dataRow3["CheckbookID"] = dataRow["ChequebookID"];
			dataRow3["Description"] = dataRow["Description"];
			dataRow3["Reference"] = dataRow["Reference"];
			dataRow3["RowIndex"] = 0;
			dataRow3.EndEdit();
			gLData.JournalDetailsTable.Rows.Add(dataRow3);
			return gLData;
		}

		private GLData CreateTransferTransactionGLData(OpenEntryTransactionData transactionData)
		{
			GLData gLData = new GLData();
			DataRow dataRow = transactionData.TransactionTable.Rows[0];
			DataRow dataRow2 = gLData.JournalTable.NewRow();
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
			DataRow dataRow3 = gLData.JournalDetailsTable.NewRow();
			dataRow3.BeginEdit();
			dataRow3["JournalID"] = 0;
			dataRow3["AccountID"] = dataRow["FirstAccountID"];
			dataRow3["Debit"] = DBNull.Value;
			dataRow3["DebitFC"] = DBNull.Value;
			dataRow3["Credit"] = dataRow["Amount"];
			dataRow3["CreditFC"] = dataRow["AmountFC"];
			dataRow3["CheckID"] = dataRow["ChequeID"];
			dataRow3["CheckNumber"] = dataRow["CheckNumber"];
			dataRow3["CheckDate"] = dataRow["CheckDate"];
			dataRow3["CheckbookID"] = dataRow["ChequebookID"];
			dataRow3["Description"] = dataRow["Description"];
			dataRow3["Reference"] = dataRow["Reference"];
			dataRow3["RowIndex"] = -1;
			dataRow3.EndEdit();
			gLData.JournalDetailsTable.Rows.Add(dataRow3);
			dataRow3 = gLData.JournalDetailsTable.NewRow();
			dataRow3.BeginEdit();
			dataRow3["JournalID"] = 0;
			dataRow3["AccountID"] = dataRow["SecondAccountID"];
			dataRow3["Debit"] = dataRow["Amount"];
			dataRow3["DebitFC"] = dataRow["AmountFC"];
			dataRow3["Credit"] = DBNull.Value;
			dataRow3["CreditFC"] = DBNull.Value;
			dataRow3["CheckID"] = dataRow["ChequeID"];
			dataRow3["CheckNumber"] = dataRow["CheckNumber"];
			dataRow3["CheckDate"] = dataRow["CheckDate"];
			dataRow3["CheckbookID"] = dataRow["ChequebookID"];
			dataRow3["Description"] = dataRow["Description"];
			dataRow3["Reference"] = dataRow["Reference"];
			dataRow3["RowIndex"] = 0;
			dataRow3.EndEdit();
			gLData.JournalDetailsTable.Rows.Add(dataRow3);
			return gLData;
		}

		private GLData CreateDebitCreditNoteGLData(OpenEntryTransactionData transactionData)
		{
			GLData gLData = new GLData();
			DataRow dataRow = transactionData.TransactionTable.Rows[0];
			DataRow dataRow2 = transactionData.TransactionDetailsTable.Rows[0];
			DataRow dataRow3 = gLData.JournalTable.NewRow();
			SysDocTypes sysDocTypes = (SysDocTypes)byte.Parse(dataRow["SysDocType"].ToString());
			dataRow3["JournalID"] = 0;
			dataRow3["JournalDate"] = dataRow["TransactionDate"];
			dataRow3["SysDocID"] = dataRow["SysDocID"];
			dataRow3["SysDocType"] = dataRow["SysDocType"];
			dataRow3["VoucherID"] = dataRow["VoucherID"];
			dataRow3["Reference"] = dataRow["Reference"];
			dataRow3["CurrencyID"] = dataRow["CurrencyID"];
			dataRow3["CurrencyRate"] = dataRow["CurrencyRate"];
			dataRow3["Note"] = dataRow["Description"];
			dataRow3.EndEdit();
			gLData.JournalTable.Rows.Add(dataRow3);
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
			case SysDocTypes.DebitNote:
				dataRow4["Debit"] = dataRow["Amount"];
				dataRow4["DebitFC"] = dataRow["AmountFC"];
				dataRow4["Credit"] = DBNull.Value;
				dataRow4["CreditFC"] = DBNull.Value;
				break;
			case SysDocTypes.CreditNote:
				dataRow4["Debit"] = DBNull.Value;
				dataRow4["DebitFC"] = DBNull.Value;
				dataRow4["Credit"] = dataRow["Amount"];
				dataRow4["CreditFC"] = dataRow["AmountFC"];
				break;
			}
			dataRow4["Description"] = dataRow["Description"];
			dataRow4["Reference"] = dataRow["Reference"];
			dataRow4["CostCenterID"] = dataRow["CostCenterID"];
			dataRow4["AnalysisID"] = dataRow["AnalysisID"];
			dataRow4["JobID"] = dataRow2["JobID"];
			dataRow4["RowIndex"] = -1;
			dataRow4.EndEdit();
			gLData.JournalDetailsTable.Rows.Add(dataRow4);
			foreach (DataRow row in transactionData.TransactionDetailsTable.Rows)
			{
				dataRow4 = gLData.JournalDetailsTable.NewRow();
				dataRow4.BeginEdit();
				dataRow4["JournalID"] = 0;
				dataRow4["PayeeType"] = dataRow["PayeeType"];
				dataRow4["PayeeID"] = dataRow["PayeeID"];
				dataRow4["AccountID"] = row["AccountID"];
				switch (sysDocTypes)
				{
				case SysDocTypes.DebitNote:
					dataRow4["Debit"] = DBNull.Value;
					dataRow4["DebitFC"] = DBNull.Value;
					dataRow4["Credit"] = row["Amount"];
					dataRow4["CreditFC"] = row["AmountFC"];
					break;
				case SysDocTypes.CreditNote:
					dataRow4["Debit"] = row["Amount"];
					dataRow4["DebitFC"] = row["AmountFC"];
					dataRow4["Credit"] = DBNull.Value;
					dataRow4["CreditFC"] = DBNull.Value;
					break;
				}
				dataRow4["Description"] = row["Description"];
				dataRow4["Reference"] = row["Reference"];
				dataRow4["CostCenterID"] = row["CostCenterID"];
				dataRow4["JobID"] = row["JobID"];
				dataRow4["CostCategoryID"] = row["CostCategoryID"];
				dataRow4["ConsignID"] = row["ConsignID"];
				dataRow4["ConsignExpenseID"] = row["ConsignExpenseID"];
				dataRow4["AnalysisID"] = row["AnalysisID"];
				dataRow4["RowIndex"] = row["RowIndex"];
				dataRow4.EndEdit();
				gLData.JournalDetailsTable.Rows.Add(dataRow4);
			}
			return gLData;
		}

		private GLData CreateReceiptMultiGLData(OpenEntryTransactionData transactionData)
		{
			GLData gLData = new GLData();
			DataRow dataRow = transactionData.TransactionTable.Rows[0];
			dataRow["VoucherID"].ToString();
			string sysDocID = dataRow["SysDocID"].ToString();
			DataRow dataRow2 = gLData.JournalTable.NewRow();
			byte.Parse(dataRow["SysDocType"].ToString());
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
			DataRow dataRow3 = gLData.JournalDetailsTable.NewRow();
			dataRow3.BeginEdit();
			dataRow3["JournalID"] = 0;
			dataRow3["PayeeType"] = dataRow["PayeeType"];
			dataRow3["PayeeID"] = dataRow["PayeeID"];
			dataRow3["AccountID"] = dataRow["AccountID"];
			dataRow3["Debit"] = dataRow["Amount"];
			dataRow3["DebitFC"] = dataRow["AmountFC"];
			dataRow3["Credit"] = DBNull.Value;
			dataRow3["CreditFC"] = DBNull.Value;
			dataRow3["CheckID"] = dataRow["ChequeID"];
			dataRow3["CheckNumber"] = dataRow["CheckNumber"];
			dataRow3["CheckDate"] = dataRow["CheckDate"];
			dataRow3["CheckbookID"] = dataRow["ChequebookID"];
			dataRow3["Description"] = dataRow["Description"];
			dataRow3["Reference"] = dataRow["Reference"];
			dataRow3["CostCenterID"] = dataRow["CostCenterID"];
			dataRow3["AnalysisID"] = dataRow["AnalysisID"];
			dataRow3["RowIndex"] = -1;
			dataRow3.EndEdit();
			gLData.JournalDetailsTable.Rows.Add(dataRow3);
			foreach (DataRow row in transactionData.TransactionDetailsTable.Rows)
			{
				dataRow3 = gLData.JournalDetailsTable.NewRow();
				dataRow3.BeginEdit();
				dataRow3["JournalID"] = 0;
				string text = row["PayeeID"].ToString();
				string text2 = "";
				string text3 = row["PayeeType"].ToString();
				dataRow3["PayeeType"] = row["PayeeType"];
				if (text3 == "A")
				{
					text2 = row["AccountID"].ToString();
				}
				else if (text3 == "C")
				{
					text2 = new Customers(base.DBConfig).GetCustomerARAccountID(sysDocID, text);
				}
				else if (text3 == "V")
				{
					text2 = new Vendors(base.DBConfig).GetVendorAPAccountID(sysDocID, text);
				}
				else if (text3 == "E")
				{
					text2 = new Employees(base.DBConfig).GetEmployeeAccountID(sysDocID, text);
				}
				if (text2 == "")
				{
					throw new CompanyException("AccountID should be selected for the payee if a payee is in transaction.", 1028);
				}
				dataRow3["PayeeID"] = text;
				dataRow3["AccountID"] = text2;
				switch (text3)
				{
				case "C":
				case "V":
				case "E":
					dataRow3["IsARAP"] = true;
					break;
				}
				dataRow3["PayeeType"] = dataRow["PayeeType"];
				dataRow3["PayeeID"] = dataRow["PayeeID"];
				dataRow3["AccountID"] = row["AccountID"];
				dataRow3["Debit"] = DBNull.Value;
				dataRow3["DebitFC"] = DBNull.Value;
				dataRow3["Credit"] = row["Amount"];
				dataRow3["CreditFC"] = row["AmountFC"];
				dataRow3["CheckID"] = row["ChequeID"];
				dataRow3["CheckNumber"] = row["CheckNumber"];
				dataRow3["CheckDate"] = row["CheckDate"];
				dataRow3["CheckbookID"] = row["ChequebookID"];
				dataRow3["Description"] = row["Description"];
				dataRow3["Reference"] = row["Reference"];
				dataRow3["CostCenterID"] = row["CostCenterID"];
				dataRow3["AnalysisID"] = row["AnalysisID"];
				dataRow3["RowIndex"] = row["RowIndex"];
				dataRow3.EndEdit();
				gLData.JournalDetailsTable.Rows.Add(dataRow3);
			}
			return gLData;
		}

		private GLData CreateExpenseMultipleAccountGLData(OpenEntryTransactionData transactionData)
		{
			GLData gLData = new GLData();
			DataRow dataRow = transactionData.TransactionTable.Rows[0];
			DataRow dataRow2 = transactionData.TransactionDetailsTable.Rows[0];
			dataRow["VoucherID"].ToString();
			string sysDocID = dataRow["SysDocID"].ToString();
			DataRow dataRow3 = gLData.JournalTable.NewRow();
			byte.Parse(dataRow["SysDocType"].ToString());
			dataRow3["JournalID"] = 0;
			dataRow3["JournalDate"] = dataRow["TransactionDate"];
			dataRow3["SysDocID"] = dataRow["SysDocID"];
			dataRow3["SysDocType"] = dataRow["SysDocType"];
			dataRow3["VoucherID"] = dataRow["VoucherID"];
			dataRow3["Reference"] = dataRow["Reference"];
			dataRow3["CurrencyID"] = dataRow["CurrencyID"];
			dataRow3["CurrencyRate"] = dataRow["CurrencyRate"];
			dataRow3["Note"] = dataRow["Description"];
			dataRow3.EndEdit();
			gLData.JournalTable.Rows.Add(dataRow3);
			DataRow dataRow4 = gLData.JournalDetailsTable.NewRow();
			dataRow4.BeginEdit();
			dataRow4["JournalID"] = 0;
			dataRow4["PayeeType"] = dataRow["PayeeType"];
			dataRow4["PayeeID"] = dataRow["PayeeID"];
			dataRow4["AccountID"] = dataRow["AccountID"];
			dataRow4["Debit"] = DBNull.Value;
			dataRow4["DebitFC"] = DBNull.Value;
			dataRow4["Credit"] = dataRow["Amount"];
			dataRow4["CreditFC"] = dataRow["AmountFC"];
			dataRow4["CheckID"] = dataRow["ChequeID"];
			dataRow4["CheckNumber"] = dataRow["CheckNumber"];
			dataRow4["CheckDate"] = dataRow["CheckDate"];
			dataRow4["CheckbookID"] = dataRow["ChequebookID"];
			dataRow4["Description"] = dataRow["Description"];
			dataRow4["Reference"] = dataRow["Reference"];
			dataRow4["CostCenterID"] = dataRow["CostCenterID"];
			dataRow4["JobID"] = dataRow2["JobID"];
			dataRow4["CostCategoryID"] = dataRow2["CostCategoryID"];
			dataRow4["ConsignID"] = dataRow2["ConsignID"];
			dataRow4["ConsignExpenseID"] = dataRow2["ConsignExpenseID"];
			dataRow4["AnalysisID"] = dataRow["AnalysisID"];
			dataRow4["RowIndex"] = -1;
			dataRow4.EndEdit();
			gLData.JournalDetailsTable.Rows.Add(dataRow4);
			foreach (DataRow row in transactionData.TransactionDetailsTable.Rows)
			{
				dataRow4 = gLData.JournalDetailsTable.NewRow();
				dataRow4.BeginEdit();
				dataRow4["JournalID"] = 0;
				string text = row["PayeeID"].ToString();
				string text2 = "";
				string text3 = row["PayeeType"].ToString();
				dataRow4["PayeeType"] = row["PayeeType"];
				if (text3 == "A")
				{
					text2 = row["AccountID"].ToString();
				}
				else if (text3 == "C")
				{
					text2 = new Customers(base.DBConfig).GetCustomerARAccountID(sysDocID, text);
				}
				else if (text3 == "V")
				{
					text2 = new Vendors(base.DBConfig).GetVendorAPAccountID(sysDocID, text);
				}
				else if (text3 == "E")
				{
					text2 = new Employees(base.DBConfig).GetEmployeeAccountID(sysDocID, text);
				}
				if (text2 == "")
				{
					throw new CompanyException("AccountID should be selected for the payee if a payee is in transaction.", 1028);
				}
				dataRow4["PayeeID"] = text;
				dataRow4["AccountID"] = text2;
				switch (text3)
				{
				case "C":
				case "V":
				case "E":
					dataRow4["IsARAP"] = true;
					break;
				}
				dataRow4["Debit"] = row["Amount"];
				dataRow4["DebitFC"] = row["AmountFC"];
				dataRow4["Credit"] = DBNull.Value;
				dataRow4["CreditFC"] = DBNull.Value;
				dataRow4["CheckID"] = row["ChequeID"];
				dataRow4["CheckNumber"] = row["CheckNumber"];
				dataRow4["CheckDate"] = row["CheckDate"];
				dataRow4["CheckbookID"] = row["ChequebookID"];
				dataRow4["Description"] = row["Description"];
				dataRow4["Reference"] = row["Reference"];
				dataRow4["CostCenterID"] = row["CostCenterID"];
				dataRow4["JobID"] = row["JobID"];
				dataRow4["CostCategoryID"] = row["CostCategoryID"];
				dataRow4["ConsignID"] = dataRow2["ConsignID"];
				dataRow4["ConsignExpenseID"] = dataRow2["ConsignExpenseID"];
				dataRow4["AnalysisID"] = row["AnalysisID"];
				dataRow4["RowIndex"] = row["RowIndex"];
				dataRow4.EndEdit();
				gLData.JournalDetailsTable.Rows.Add(dataRow4);
			}
			return gLData;
		}

		private GLData CreateExpenseGLData(OpenEntryTransactionData transactionData)
		{
			GLData gLData = new GLData();
			DataRow dataRow = transactionData.TransactionTable.Rows[0];
			DataRow dataRow2 = transactionData.TransactionDetailsTable.Rows[0];
			dataRow["VoucherID"].ToString();
			string sysDocID = dataRow["SysDocID"].ToString();
			DataRow dataRow3 = gLData.JournalTable.NewRow();
			byte.Parse(dataRow["SysDocType"].ToString());
			dataRow3["JournalID"] = 0;
			dataRow3["JournalDate"] = dataRow["TransactionDate"];
			dataRow3["SysDocID"] = dataRow["SysDocID"];
			dataRow3["SysDocType"] = dataRow["SysDocType"];
			dataRow3["VoucherID"] = dataRow["VoucherID"];
			dataRow3["Reference"] = dataRow["Reference"];
			dataRow3["CurrencyID"] = dataRow["CurrencyID"];
			dataRow3["CurrencyRate"] = dataRow["CurrencyRate"];
			dataRow3["Note"] = dataRow["Description"];
			dataRow3.EndEdit();
			gLData.JournalTable.Rows.Add(dataRow3);
			DataRow dataRow4 = gLData.JournalDetailsTable.NewRow();
			dataRow4.BeginEdit();
			dataRow4["JournalID"] = 0;
			dataRow4["PayeeType"] = dataRow["PayeeType"];
			dataRow4["PayeeID"] = dataRow["PayeeID"];
			dataRow4["AccountID"] = dataRow["AccountID"];
			dataRow4["Debit"] = dataRow["Amount"];
			dataRow4["DebitFC"] = dataRow["AmountFC"];
			dataRow4["Credit"] = DBNull.Value;
			dataRow4["CreditFC"] = DBNull.Value;
			dataRow4["CheckID"] = dataRow["ChequeID"];
			dataRow4["CheckNumber"] = dataRow["CheckNumber"];
			dataRow4["CheckDate"] = dataRow["CheckDate"];
			dataRow4["CheckbookID"] = dataRow["ChequebookID"];
			dataRow4["Description"] = dataRow["Description"];
			dataRow4["Reference"] = dataRow["Reference"];
			dataRow4["CostCenterID"] = dataRow["CostCenterID"];
			dataRow4["JobID"] = dataRow2["JobID"];
			dataRow4["CostCategoryID"] = dataRow2["CostCategoryID"];
			dataRow4["ConsignID"] = dataRow2["ConsignID"];
			dataRow4["ConsignExpenseID"] = dataRow2["ConsignExpenseID"];
			dataRow4["AnalysisID"] = dataRow["AnalysisID"];
			dataRow4["RowIndex"] = -1;
			dataRow4.EndEdit();
			gLData.JournalDetailsTable.Rows.Add(dataRow4);
			foreach (DataRow row in transactionData.TransactionDetailsTable.Rows)
			{
				dataRow4 = gLData.JournalDetailsTable.NewRow();
				dataRow4.BeginEdit();
				dataRow4["JournalID"] = 0;
				string text = row["PayeeID"].ToString();
				string text2 = "";
				string text3 = row["PayeeType"].ToString();
				dataRow4["PayeeType"] = row["PayeeType"];
				if (text3 == "A")
				{
					text2 = row["AccountID"].ToString();
				}
				else if (text3 == "C")
				{
					text2 = new Customers(base.DBConfig).GetCustomerARAccountID(sysDocID, text);
				}
				else if (text3 == "V")
				{
					text2 = new Vendors(base.DBConfig).GetVendorAPAccountID(sysDocID, text);
				}
				else if (text3 == "E")
				{
					text2 = new Employees(base.DBConfig).GetEmployeeAccountID(sysDocID, text);
				}
				if (text2 == "")
				{
					throw new CompanyException("AccountID should be selected for the payee if a payee is in transaction.", 1028);
				}
				dataRow4["PayeeID"] = text;
				dataRow4["AccountID"] = text2;
				switch (text3)
				{
				case "C":
				case "V":
				case "E":
					dataRow4["IsARAP"] = true;
					break;
				}
				dataRow4["Debit"] = DBNull.Value;
				dataRow4["DebitFC"] = DBNull.Value;
				dataRow4["Credit"] = row["Amount"];
				dataRow4["CreditFC"] = row["AmountFC"];
				dataRow4["CheckID"] = row["ChequeID"];
				dataRow4["CheckNumber"] = row["CheckNumber"];
				dataRow4["CheckDate"] = row["CheckDate"];
				dataRow4["CheckbookID"] = row["ChequebookID"];
				dataRow4["Description"] = row["Description"];
				dataRow4["Reference"] = row["Reference"];
				dataRow4["CostCenterID"] = row["CostCenterID"];
				dataRow4["JobID"] = row["JobID"];
				dataRow4["CostCategoryID"] = row["CostCategoryID"];
				dataRow4["ConsignID"] = dataRow2["ConsignID"];
				dataRow4["ConsignExpenseID"] = dataRow2["ConsignExpenseID"];
				dataRow4["AnalysisID"] = row["AnalysisID"];
				dataRow4["RowIndex"] = row["RowIndex"];
				dataRow4.EndEdit();
				gLData.JournalDetailsTable.Rows.Add(dataRow4);
			}
			return gLData;
		}

		private GLData CreateGLData(OpenEntryTransactionData transactionData)
		{
			GLData gLData = new GLData();
			bool flag = false;
			DataRow dataRow = transactionData.TransactionTable.Rows[0];
			DataRow dataRow2 = transactionData.TransactionDetailsTable.Rows[0];
			if (dataRow["CurrencyID"] != DBNull.Value && dataRow["CurrencyID"].ToString() != GetBaseCurrencyID())
			{
				flag = true;
			}
			DataRow dataRow3 = gLData.JournalTable.NewRow();
			SysDocTypes sysDocTypes = (SysDocTypes)byte.Parse(dataRow["SysDocType"].ToString());
			dataRow3["JournalID"] = 0;
			dataRow3["JournalDate"] = dataRow["TransactionDate"];
			dataRow3["SysDocID"] = dataRow["SysDocID"];
			dataRow3["SysDocType"] = dataRow["SysDocType"];
			dataRow3["VoucherID"] = dataRow["VoucherID"];
			dataRow3["Reference"] = dataRow["Reference"];
			dataRow3["CurrencyID"] = dataRow["CurrencyID"];
			dataRow3["CurrencyRate"] = dataRow["CurrencyRate"];
			if (flag)
			{
				Convert.ToDecimal(dataRow["CurrencyRate"].ToString());
				dataRow["CurrencyID"].ToString();
			}
			dataRow3["Note"] = dataRow["Description"];
			dataRow3.EndEdit();
			gLData.JournalTable.Rows.Add(dataRow3);
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
			case SysDocTypes.CashReceipt:
			case SysDocTypes.TTReceipt:
			case SysDocTypes.OpeningChequeReceipt:
				dataRow4["Debit"] = DBNull.Value;
				dataRow4["DebitFC"] = DBNull.Value;
				dataRow4["CreditFC"] = dataRow["AmountFC"];
				dataRow4["Credit"] = dataRow["Amount"];
				break;
			case SysDocTypes.CashPayment:
			case SysDocTypes.TTPayment:
			case SysDocTypes.OpeningChequePayment:
				dataRow4["Debit"] = dataRow["Amount"];
				dataRow4["DebitFC"] = dataRow["AmountFC"];
				dataRow4["Credit"] = DBNull.Value;
				dataRow4["CreditFC"] = DBNull.Value;
				break;
			}
			dataRow4["CheckID"] = dataRow["ChequeID"];
			dataRow4["CheckNumber"] = dataRow["CheckNumber"];
			dataRow4["CheckDate"] = dataRow["CheckDate"];
			dataRow4["CheckbookID"] = dataRow["ChequebookID"];
			dataRow4["Description"] = dataRow["Description"];
			dataRow4["Reference"] = dataRow["Reference"];
			dataRow4["CostCenterID"] = dataRow["CostCenterID"];
			dataRow4["JobID"] = dataRow2["JobID"];
			dataRow4["CostCategoryID"] = dataRow2["CostCategoryID"];
			dataRow4["ConsignID"] = dataRow2["ConsignID"];
			dataRow4["ConsignExpenseID"] = dataRow2["ConsignExpenseID"];
			dataRow4["AnalysisID"] = dataRow["AnalysisID"];
			dataRow4["RowIndex"] = -1;
			dataRow4.EndEdit();
			gLData.JournalDetailsTable.Rows.Add(dataRow4);
			foreach (DataRow row in transactionData.TransactionDetailsTable.Rows)
			{
				decimal d = Convert.ToDecimal(row["Amount"].ToString());
				string text = row["ExpCode"].ToString();
				decimal result = default(decimal);
				decimal.TryParse(row["ExpAmount"].ToString(), out result);
				decimal result2 = default(decimal);
				decimal.TryParse(row["ExpPercent"].ToString(), out result2);
				string value = "";
				decimal num = default(decimal);
				if (text != "" && result2 == 0m)
				{
					num = d - result;
				}
				else if (text != "" && result2 > 0m)
				{
					num = result;
				}
				if (flag)
				{
					Convert.ToDecimal(row["AmountFC"].ToString());
				}
				dataRow4 = gLData.JournalDetailsTable.NewRow();
				dataRow4.BeginEdit();
				dataRow4["JournalID"] = 0;
				dataRow4["PayeeType"] = dataRow["PayeeType"];
				dataRow4["PayeeID"] = dataRow["PayeeID"];
				dataRow4["AccountID"] = row["AccountID"];
				if (text != "")
				{
					SqlTransaction sqlTransaction = null;
					sqlTransaction = base.DBConfig.StartNewTransaction();
					value = new ExpenseCode(base.DBConfig).GetExpenseAccountID(text, sqlTransaction);
					base.DBConfig.EndTransaction(result: true);
				}
				switch (sysDocTypes)
				{
				case SysDocTypes.CashReceipt:
				case SysDocTypes.TTReceipt:
				case SysDocTypes.OpeningChequeReceipt:
					dataRow4["DebitFC"] = row["AmountFC"];
					dataRow4["Debit"] = row["Amount"];
					if (text != "" && result2 == 0m)
					{
						dataRow4["Debit"] = num;
					}
					else if (text != "" && result2 > 0m)
					{
						dataRow4["Debit"] = num;
						dataRow4["AccountID"] = value;
					}
					dataRow4["Credit"] = DBNull.Value;
					dataRow4["CreditFC"] = DBNull.Value;
					break;
				case SysDocTypes.CashPayment:
				case SysDocTypes.OpeningChequePayment:
					dataRow4["Debit"] = DBNull.Value;
					dataRow4["DebitFC"] = DBNull.Value;
					dataRow4["Credit"] = row["Amount"];
					dataRow4["CreditFC"] = row["AmountFC"];
					break;
				case SysDocTypes.TTPayment:
					dataRow4["Debit"] = DBNull.Value;
					dataRow4["DebitFC"] = DBNull.Value;
					dataRow4["Credit"] = row["Amount"];
					dataRow4["CreditFC"] = row["AmountFC"];
					break;
				}
				dataRow4["CheckID"] = row["ChequeID"];
				dataRow4["CheckNumber"] = row["CheckNumber"];
				dataRow4["CheckDate"] = row["CheckDate"];
				dataRow4["CheckbookID"] = row["ChequebookID"];
				dataRow4["Description"] = row["Description"];
				dataRow4["Reference"] = row["Reference"];
				dataRow4["CostCenterID"] = row["CostCenterID"];
				dataRow4["JobID"] = row["JobID"];
				dataRow4["CostCategoryID"] = row["CostCategoryID"];
				dataRow4["ConsignID"] = dataRow2["ConsignID"];
				dataRow4["ConsignExpenseID"] = dataRow2["ConsignExpenseID"];
				dataRow4["AnalysisID"] = row["AnalysisID"];
				dataRow4["RowIndex"] = row["RowIndex"];
				dataRow4.EndEdit();
				gLData.JournalDetailsTable.Rows.Add(dataRow4);
			}
			foreach (DataRow row2 in transactionData.BankFeeDetailsTable.Rows)
			{
				dataRow4 = gLData.JournalDetailsTable.NewRow();
				dataRow4.BeginEdit();
				dataRow4["JournalID"] = 0;
				dataRow4["AccountID"] = row2["ExpenseAccountID"];
				dataRow4["DebitFC"] = DBNull.Value;
				dataRow4["Debit"] = row2["Amount"];
				dataRow4["IsBaseOnly"] = true;
				dataRow4["Credit"] = DBNull.Value;
				dataRow4["CreditFC"] = DBNull.Value;
				dataRow4["Description"] = row2["Description"];
				dataRow4["Reference"] = row2["Reference"];
				dataRow4["RowIndex"] = row2["RowIndex"];
				row2["BankAccountID"].ToString();
				dataRow4.EndEdit();
				gLData.JournalDetailsTable.Rows.Add(dataRow4);
				dataRow4 = gLData.JournalDetailsTable.NewRow();
				dataRow4.BeginEdit();
				dataRow4["JournalID"] = 0;
				dataRow4["IsBaseOnly"] = true;
				dataRow4["AccountID"] = row2["BankAccountID"];
				dataRow4["DebitFC"] = DBNull.Value;
				dataRow4["Debit"] = DBNull.Value;
				dataRow4["Credit"] = row2["Amount"];
				dataRow4["CreditFC"] = DBNull.Value;
				dataRow4["Description"] = row2["Description"];
				dataRow4["Reference"] = row2["Reference"];
				dataRow4["RowIndex"] = row2["RowIndex"];
				dataRow4.EndEdit();
				gLData.JournalDetailsTable.Rows.Add(dataRow4);
			}
			return gLData;
		}

		internal bool DeleteTransactionDetailsRows(string sysDocID, string voucherID, SqlTransaction sqlTransaction)
		{
			bool flag = true;
			try
			{
				string commandText = "DELETE FROM Transaction_Details WHERE SysDocID = '" + sysDocID + "' AND VoucherID = '" + voucherID + "'";
				flag &= Delete(commandText, sqlTransaction);
				commandText = "DELETE FROM Bank_Fee_Details WHERE GLTransactionSysDocID = '" + sysDocID + "' AND GLTransactionVoucherID = '" + voucherID + "'";
				flag &= Delete(commandText, sqlTransaction);
				commandText = "DELETE FROM General_Payment_Detail WHERE SysDocID = '" + sysDocID + "' AND VoucherID = '" + voucherID + "'";
				return flag & Delete(commandText, sqlTransaction);
			}
			catch
			{
				throw;
			}
		}

		public bool VoidIssuedChequeClearanceTransaction(string sysDocID, string voucherID)
		{
			bool flag = true;
			try
			{
				SqlTransaction sqlTransaction = base.DBConfig.StartNewTransaction();
				string exp = "SELECT IsVoid FROM GL_Transaction WHERE SysDocID='" + sysDocID + "' AND VoucherID='" + voucherID + "'";
				object obj = ExecuteScalar(exp, sqlTransaction);
				bool flag2 = false;
				if (obj != null && obj.ToString() != string.Empty)
				{
					flag2 = bool.Parse(obj.ToString());
				}
				if (flag2)
				{
					throw new Exception("This transaction is already voided.");
				}
				exp = "UPDATE Transaction_Details SET IsVoid='" + true.ToString() + "'  WHERE SysDocID='" + sysDocID + "' ";
				exp = exp + " AND VoucherID='" + voucherID + "'";
				flag &= (ExecuteNonQuery(exp, sqlTransaction) >= 0);
				if (flag)
				{
					exp = "UPDATE GL_Transaction SET IsVoid = '" + true.ToString() + "' WHERE SysDocID='" + sysDocID + "' ";
					exp = exp + " AND VoucherID='" + voucherID + "'";
					flag &= (ExecuteNonQuery(exp, sqlTransaction) >= 0);
				}
				flag &= new Journal(base.DBConfig).VoidJournal(sysDocID, voucherID, isVoid: true, sqlTransaction);
				flag &= new IssuedCheques(base.DBConfig).SetChequeStatus(sysDocID, voucherID, IssuedChequeStatus.Issued, sqlTransaction);
				if (!flag)
				{
					return flag;
				}
				AddActivityLog("Issued Cheque Clearance", voucherID, sysDocID, ActivityTypes.Void, sqlTransaction);
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

		public bool VoidChequeDepositTransaction(string sysDocID, string voucherID)
		{
			bool flag = true;
			try
			{
				SqlTransaction sqlTransaction = base.DBConfig.StartNewTransaction();
				string exp = "SELECT IsVoid FROM GL_Transaction WHERE SysDocID='" + sysDocID + "' AND VoucherID='" + voucherID + "'";
				object obj = ExecuteScalar(exp, sqlTransaction);
				bool flag2 = false;
				if (obj != null && obj.ToString() != string.Empty)
				{
					flag2 = bool.Parse(obj.ToString());
				}
				if (flag2)
				{
					throw new Exception("This transaction is already voided.");
				}
				exp = "SELECT Count(ChequeID) FROM Cheque_Received WHERE Status<>2 AND SysDocID='" + sysDocID + "' AND VoucherID='" + voucherID + "'";
				obj = ExecuteScalar(exp, sqlTransaction);
				if (obj != null && obj.ToString() != string.Empty && int.Parse(obj.ToString()) > 0)
				{
					throw new CompanyException("The transaction cannot be voided. One or more cheques are not in 'Deposited' status.", 1001);
				}
				exp = "UPDATE Transaction_Details SET IsVoid='" + true.ToString() + "'  WHERE SysDocID='" + sysDocID + "' ";
				exp = exp + " AND VoucherID='" + voucherID + "'";
				flag &= (ExecuteNonQuery(exp, sqlTransaction) >= 0);
				if (flag)
				{
					exp = "UPDATE GL_Transaction SET IsVoid = '" + true.ToString() + "' WHERE SysDocID='" + sysDocID + "' ";
					exp = exp + " AND VoucherID='" + voucherID + "'";
					flag &= (ExecuteNonQuery(exp, sqlTransaction) >= 0);
				}
				flag &= new Journal(base.DBConfig).VoidJournal(sysDocID, voucherID, isVoid: true, sqlTransaction);
				flag &= new ReceivedCheques(base.DBConfig).UpdateChequeStatus(sysDocID, voucherID, ReceivedChequeStatus.Undeposited, sqlTransaction);
				if (!flag)
				{
					return flag;
				}
				ActivityTypes activityType = ActivityTypes.Void;
				AddActivityLog("Cheque Deposit", voucherID, sysDocID, activityType, sqlTransaction);
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

		public bool VoidTransaction(string sysDocID, string voucherID, bool isVoid)
		{
			bool flag = true;
			try
			{
				SqlTransaction sqlTransaction = base.DBConfig.StartNewTransaction();
				string exp = "SELECT IsVoid FROM GL_Transaction WHERE SysDocID='" + sysDocID + "' AND VoucherID='" + voucherID + "'";
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
				SysDocTypes systemDocumentType = new SystemDocuments(base.DBConfig).GetSystemDocumentType(sysDocID, sqlTransaction);
				switch (systemDocumentType)
				{
				case SysDocTypes.OpeningChequeReceipt:
					if (TransactionHasProcessedReceivedCheques(sysDocID, voucherID, sqlTransaction))
					{
						throw new CompanyException("One or more cheques in this transaction are already in use by another transaction.", 1030);
					}
					break;
				case SysDocTypes.ChequePayment:
				case SysDocTypes.OpeningChequePayment:
					if (TransactionHasProcessedIssuedCheques(sysDocID, voucherID, sqlTransaction))
					{
						throw new CompanyException("One or more cheques in this transaction are already in use by another transaction.", 1030);
					}
					break;
				}
				exp = "UPDATE Transaction_Details SET IsVoid='" + isVoid.ToString() + "'  WHERE SysDocID='" + sysDocID + "' ";
				exp = exp + " AND VoucherID='" + voucherID + "'";
				flag &= (ExecuteNonQuery(exp, sqlTransaction) >= 0);
				if (flag)
				{
					exp = "UPDATE GL_Transaction SET IsVoid = '" + isVoid.ToString() + "' WHERE SysDocID='" + sysDocID + "' ";
					exp = exp + " AND VoucherID='" + voucherID + "'";
					flag &= (ExecuteNonQuery(exp, sqlTransaction) > 0);
				}
				if ((systemDocumentType == SysDocTypes.PropertyRental || systemDocumentType == SysDocTypes.PropertyRenew || systemDocumentType == SysDocTypes.PropertyCancel) && flag)
				{
					exp = "UPDATE General_Payment_Detail SET IsVoid = '" + isVoid.ToString() + "' WHERE SysDocID='" + sysDocID + "' ";
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
				SysDocTypes systemDocumentType = new SystemDocuments(base.DBConfig).GetSystemDocumentType(sysDocID, sqlTransaction);
				switch (systemDocumentType)
				{
				case SysDocTypes.OpeningChequeReceipt:
					if (TransactionHasProcessedReceivedCheques(sysDocID, voucherID, sqlTransaction))
					{
						throw new CompanyException("One or more cheques in this transaction are already in use by another transaction.", 1030);
					}
					break;
				case SysDocTypes.ChequePayment:
				case SysDocTypes.OpeningChequePayment:
					if (TransactionHasProcessedIssuedCheques(sysDocID, voucherID, sqlTransaction))
					{
						throw new CompanyException("One or more cheques in this transaction are already in use by another transaction.", 1030);
					}
					break;
				}
				flag &= DeleteTransactionDetailsRows(sysDocID, voucherID, sqlTransaction);
				string exp = "DELETE FROM GL_Transaction WHERE SysDocID='" + sysDocID + "' AND VoucherID='" + voucherID + "'";
				flag &= (ExecuteNonQuery(exp, sqlTransaction) > 0);
				flag &= new Journal(base.DBConfig).DeleteJournal(sysDocID, voucherID, sqlTransaction);
				switch (systemDocumentType)
				{
				case SysDocTypes.ChequeReceipt:
					flag &= new ARJournal(base.DBConfig).DeleteARJournal(sysDocID, voucherID, sqlTransaction);
					break;
				case SysDocTypes.OpeningChequePayment:
					flag &= new APJournal(base.DBConfig).DeleteAPJournal(sysDocID, voucherID, sqlTransaction);
					break;
				}
				flag &= new ReceivedCheques(base.DBConfig).DeleteChequeRows(sysDocID, voucherID, sqlTransaction);
				flag &= new IssuedCheques(base.DBConfig).DeleteChequeRows(sysDocID, voucherID, sqlTransaction);
				flag &= new SystemDocuments(base.DBConfig).UpdateNextToDeletedDocumentNumber(sysDocID, voucherID, "GL_Transaction", "VoucherID", sqlTransaction);
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

		public bool UpdateChequeStatus(string sysDocID, string VoucherID, ReceivedChequeStatus status, SqlTransaction sqlTransaction)
		{
			bool flag = true;
			try
			{
				string text = "UPDATE Cheque_Received SET Status = " + (byte)status;
				if (status == ReceivedChequeStatus.Undeposited)
				{
					text += ",DepositSysDocID = NULL, DepositVoucherID = NULL ";
				}
				text = text + " WHERE ChequeID IN (Select TRD.ChequeID FROM Transaction_Details TRD INNER JOIN GL_Transaction TR ON TRD.SysDocID=TR.SysDocID AND TRD.VoucherID=TR.VoucherID WHERE TRD.SysDocID='" + sysDocID + "' AND TRD.VoucherID = '" + VoucherID + "')";
				return flag & (ExecuteNonQuery(text, sqlTransaction) >= 0);
			}
			catch
			{
				throw;
			}
		}

		public bool DeleteChequeDepositTransaction(string sysDocID, string voucherID)
		{
			bool flag = true;
			try
			{
				bool flag2 = bool.Parse(new CompanyOption(base.DBConfig).GetCompanyOptionValue(CompanyOptionsEnum.PDCDirectMaturity, true).ToString());
				SqlTransaction sqlTransaction = base.DBConfig.StartNewTransaction();
				string exp = "SELECT Count(ChequeID) FROM Cheque_Received WHERE Status<>2 AND SysDocID='" + sysDocID + "' AND VoucherID='" + voucherID + "'";
				object obj = ExecuteScalar(exp, sqlTransaction);
				if (obj != null && obj.ToString() != string.Empty && int.Parse(obj.ToString()) > 0)
				{
					throw new CompanyException("The transaction cannot be deleted. One or more cheques are not in 'Deposited' status.", 1001);
				}
				flag = ((!flag2) ? (flag & UpdateChequeStatus(sysDocID, voucherID, ReceivedChequeStatus.SentToBank, sqlTransaction)) : (flag & UpdateChequeStatus(sysDocID, voucherID, ReceivedChequeStatus.Undeposited, sqlTransaction)));
				flag &= DeleteTransactionDetailsRows(sysDocID, voucherID, sqlTransaction);
				exp = "DELETE FROM GL_Transaction WHERE SysDocID='" + sysDocID + "' AND VoucherID='" + voucherID + "'";
				flag &= (ExecuteNonQuery(exp, sqlTransaction) > 0);
				flag &= new Journal(base.DBConfig).DeleteJournal(sysDocID, voucherID, sqlTransaction);
				if (!flag)
				{
					return flag;
				}
				AddActivityLog("Cheque Deposit", voucherID, sysDocID, ActivityTypes.Delete, sqlTransaction);
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

		public bool DeleteIssuedChequeClearanceTransaction(string sysDocID, string voucherID)
		{
			bool flag = true;
			try
			{
				SqlTransaction sqlTransaction = base.DBConfig.StartNewTransaction();
				string text = "";
				string text2 = "";
				string text3 = "";
				text3 = "SELECT ChequebookID,ChequeNumber FROM Cheque_Issued WHERE ClearanceSysDocID='" + sysDocID + "' AND ClearanceVoucherID = '" + voucherID + "'";
				DataSet dataSet = new DataSet();
				FillDataSet(dataSet, "Cheque_Issued", text3, sqlTransaction);
				if (dataSet != null && dataSet.Tables.Count > 0 && dataSet.Tables[0].Rows.Count > 0)
				{
					foreach (DataRow row in dataSet.Tables[0].Rows)
					{
						text = row["ChequeNumber"].ToString();
						text2 = row["ChequebookID"].ToString();
						flag &= new IssuedCheques(base.DBConfig).SetChequeStatus(text2, text, IssuedChequeStatus.Issued, sqlTransaction);
					}
				}
				flag &= DeleteTransactionDetailsRows(sysDocID, voucherID, sqlTransaction);
				text3 = "DELETE FROM GL_Transaction WHERE SysDocID='" + sysDocID + "' AND VoucherID='" + voucherID + "'";
				flag &= (ExecuteNonQuery(text3, sqlTransaction) > 0);
				flag &= new Journal(base.DBConfig).DeleteJournal(sysDocID, voucherID, sqlTransaction);
				if (!flag)
				{
					return flag;
				}
				AddActivityLog("Issued Cheque Clearance", voucherID, sysDocID, ActivityTypes.Delete, sqlTransaction);
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

		public OpenEntryTransactionData GetTransactionByID(string sysDocID, string voucherID)
		{
			try
			{
				return GetTransactionByID(sysDocID, voucherID, null);
			}
			catch
			{
				throw;
			}
		}

		public OpenEntryTransactionData GetTransactionByID(string sysDocID, string voucherID, SqlTransaction sqlTransaction)
		{
			try
			{
				OpenEntryTransactionData openEntryTransactionData = new OpenEntryTransactionData();
				string textCommand = "SELECT * FROM GL_Transaction WHERE VoucherID='" + voucherID + "' AND SysDocID='" + sysDocID + "'";
				FillDataSet(openEntryTransactionData, "GL_Transaction", textCommand, sqlTransaction);
				if (openEntryTransactionData == null || openEntryTransactionData.Tables.Count == 0 || openEntryTransactionData.Tables["GL_Transaction"].Rows.Count == 0)
				{
					return null;
				}
				openEntryTransactionData.TransactionTable.Rows[0]["TransactionID"].ToString();
				textCommand = "SELECT TD.*,\r\n\t\t\t\t\t\t(CASE PayeeType\r\n\t\t\t\t\t\tWHEN 'C' THEN Customer.CustomerName\r\n\t\t\t\t\t\tWHEN 'V' THEN Vendor.VendorName\r\n\t\t\t\t\t\tWHEN 'E' THEN Employee.FirstName\r\n\t\t\t\t\t\tELSE Account.AccountName END) AS AccountName\r\n\t\t\t\t\t\tFROM TRANSACTION_DETAILS TD LEFt OUTER JOIN \r\n\t\t\t\t\t\tAccount ON TD.AccountID=Account.AccountID LEFt OUTER JOIN\r\n\t\t\t\t\t\tCustomer ON TD.PayeeID=Customer.CustomerID LEFt OUTER JOIN\r\n\t\t\t\t\t\tVendor ON TD.PayeeID=Vendor.VendorID LEFt OUTER JOIN\r\n\t\t\t\t\t\tEmployee ON TD.PayeeID=Employee.EmployeeID\r\n\t\t\t\t\t\tWHERE VoucherID='" + voucherID + "' AND SysDocID='" + sysDocID + "'";
				FillDataSet(openEntryTransactionData, "Transaction_Details", textCommand, sqlTransaction);
				switch (new SystemDocuments(base.DBConfig).GetSystemDocumentType(sysDocID, sqlTransaction))
				{
				case SysDocTypes.OpeningChequeReceipt:
					textCommand = "SELECT * FROM Cheque_Received WHERE \r\n\t\t\t\t\t\t\t\tSysDocID='" + sysDocID + "' AND VoucherID = '" + voucherID + "'";
					FillDataSet(openEntryTransactionData, "Cheque_Received", textCommand, sqlTransaction);
					break;
				case SysDocTypes.OpeningChequePayment:
					textCommand = "SELECT * FROM Opening_Cheque_Issued WHERE \r\n\t\t\t\t\t\t\t\tSysDocID='" + sysDocID + "' AND VoucherID = '" + voucherID + "'";
					FillDataSet(openEntryTransactionData, "Opening_Cheque_Issued", textCommand, sqlTransaction);
					break;
				case SysDocTypes.IssuedChequeReturn:
				{
					int num = -1;
					if (!openEntryTransactionData.Tables["GL_Transaction"].Rows[0]["ChequeID"].IsDBNullOrEmpty())
					{
						num = int.Parse(openEntryTransactionData.Tables["GL_Transaction"].Rows[0]["ChequeID"].ToString());
					}
					textCommand = "SELECT * FROM Cheque_Issued WHERE \r\n\t\t\t\t\t\t\tChequeID = " + num;
					FillDataSet(openEntryTransactionData, "Opening_Cheque_Issued", textCommand, sqlTransaction);
					break;
				}
				}
				textCommand = "SELECT BFD.*,Cur.RateType FROM Bank_Fee_Details BFD LEFT OUTER JOIN Currency Cur ON BFD.CurrencyID = Cur.CurrencyID WHERE \r\n\t\t\t\t\t\t\t\tGLTransactionSysDocID='" + sysDocID + "' AND GLTransactionVoucherID = '" + voucherID + "'";
				FillDataSet(openEntryTransactionData, "Bank_Fee_Details", textCommand, sqlTransaction);
				textCommand = "SELECT GPD.* FROM General_Payment_Detail GPD  WHERE SysDocID='" + sysDocID + "' AND VoucherID = '" + voucherID + "'";
				FillDataSet(openEntryTransactionData, "General_Payment_Detail", textCommand, sqlTransaction);
				return openEntryTransactionData;
			}
			catch
			{
				throw;
			}
		}

		public OpenEntryTransactionData GetChequeDepositTransactionByID(string sysDocID, string voucherID)
		{
			try
			{
				OpenEntryTransactionData openEntryTransactionData = new OpenEntryTransactionData();
				string textCommand = "SELECT * FROM GL_Transaction WHERE VoucherID='" + voucherID + "' AND SysDocID='" + sysDocID + "'";
				FillDataSet(openEntryTransactionData, "GL_Transaction", textCommand);
				if (openEntryTransactionData == null || openEntryTransactionData.Tables.Count == 0 || openEntryTransactionData.Tables["GL_Transaction"].Rows.Count == 0)
				{
					return null;
				}
				openEntryTransactionData.Tables["GL_Transaction"].Rows[0]["TransactionID"].ToString();
				textCommand = "SELECT  'True' AS 'D', TD.ChequeID,TD.SysDocID,TD.VoucherID,TD.CheckDate AS [Chq Date],SendDate,TD.CheckNumber [Chq #],TD.BankID,CQR.Reference,\r\n\t\t\t\t\t\t\tCQR.PayeeID,CQR.PayeeType,CQR.PayeeAccountID,CQR.PDCAccountID,Bank.BankName [Bank Name], DiscountBankAccountID,\r\n\t\t\t\t\t\t\t\r\n\t\t\t\t\t\t\tCASE CQR.PayeeType WHEN 'C' THEN Customer.CustomerName\r\n\t\t\t\t\t\t\tWHEN 'V' THEN Vendor.VendorName\r\n\t\t\t\t\t\t\tWHEN 'A' THEN Account.AccountName\r\n\t\t\t\t\t\t\tWHEN 'E' THEN Employee.FirstName + ' ' + Employee.LastName END AS [Payee Name] ,\r\n\t\t\t\t\t\t\tISNULL(TR.CurrencyID,(SELECT CurrencyID FROM Currency WHERE Currency.IsBase='True')) AS Currency,ISNULL(TD.AmountFC,TD.Amount) AS Amount, CQR.Status\r\n\t\t\t\t\t\t\tFROM         Transaction_Details TD INNER JOIN Bank ON TD.BankID=Bank.BankID\r\n\t\t\t\t\t\t\tINNER JOIN Cheque_Received CQR ON CQR.ChequeID = TD.ChequeID\r\n\t\t\t\t\t\t\tLEFT OUTER JOIN GL_Transaction TR ON TD.VoucherID=TR.VoucherID AND TD.SysDocID=TR.SysDocID\r\n\t\t\t\t\t\t\tLEFT OUTER JOIN Customer ON Customer.CustomerID=CQR.PayeeID \r\n\t\t\t\t\t\t\tLEFT OUTER JOIN Vendor ON Vendor.VendorID=CQR.PayeeID\r\n\t\t\t\t\t\t\tLEFT Outer JOIN Employee ON Employee.EmployeeID=CQR.PayeeID\r\n\t\t\t\t\t\t\tLEFT Outer JOIN Account ON Account.AccountID=CQR.PayeeID\r\n\t\t\t\t\t\t\tWHERE TD.VoucherID='" + voucherID + "' AND TD.SysDocID='" + sysDocID + "'";
				FillDataSet(openEntryTransactionData, "Cheque_Received", textCommand);
				return openEntryTransactionData;
			}
			catch
			{
				throw;
			}
		}

		public OpenEntryTransactionData GetIssuedChequeClearanceTransactionByID(string sysDocID, string voucherID)
		{
			try
			{
				OpenEntryTransactionData openEntryTransactionData = new OpenEntryTransactionData();
				string textCommand = "SELECT * FROM GL_Transaction WHERE VoucherID='" + voucherID + "' AND SysDocID='" + sysDocID + "'";
				FillDataSet(openEntryTransactionData, "GL_Transaction", textCommand);
				if (openEntryTransactionData == null || openEntryTransactionData.Tables.Count == 0 || openEntryTransactionData.Tables["GL_Transaction"].Rows.Count == 0)
				{
					return null;
				}
				openEntryTransactionData.Tables["GL_Transaction"].Rows[0]["TransactionID"].ToString();
				textCommand = "SELECT     'True' AS 'D',TD.CheckDate AS [Chq Date],TD.CheckNumber [Chq #],Bank.BankName [Bank Name],\r\n\t\t\t\t\t\t\t\r\n\t\t\t\t\t\t\tCASE TD.PayeeType WHEN 'C' THEN Customer.CustomerName\r\n\t\t\t\t\t\t\tWHEN 'V' THEN Vendor.VendorName\r\n\t\t\t\t\t\t\tWHEN 'A' THEN Account.AccountName\r\n\t\t\t\t\t\t\tWHEN 'E' THEN Employee.FirstName + ' ' + Employee.LastName END AS [Payee Name] ,\r\n\t\t\t\t\t\t\tISNULL(TR.CurrencyID,(SELECT CurrencyID FROM Currency WHERE Currency.IsBase='True')) AS Currency,ISNULL(TD.AmountFC,TD.Amount) AS Amount\r\n\t\t\t\t\t\t\tFROM         Transaction_Details TD INNER JOIN Bank ON TD.BankID=Bank.BankID\r\n\t\t\t\t\t\t\tLEFT OUTER JOIN GL_Transaction TR ON TD.VoucherID=TR.VoucherID AND TD.SysDocID=TR.SysDocID\r\n\t\t\t\t\t\t\tLEFT OUTER JOIN Customer ON Customer.CustomerID=TD.PayeeID \r\n\t\t\t\t\t\t\tLEFT OUTER JOIN Vendor ON Vendor.VendorID=TD.PayeeID\r\n\t\t\t\t\t\t\tLEFT Outer JOIN Employee ON Employee.EmployeeID=TD.PayeeID\r\n\t\t\t\t\t\t\tLEFT Outer JOIN Account ON Account.AccountID=TD.PayeeID\r\n\t\t\t\t\t\t\tWHERE TD.VoucherID='" + voucherID + "' AND TD.SysDocID='" + sysDocID + "'";
				openEntryTransactionData.Tables.Remove("Transaction_Details");
				FillDataSet(openEntryTransactionData, "Transaction_Details", textCommand);
				textCommand = "SELECT 'True' AS C,SysDocID,VoucherID, ChequeDate AS [Chq Date],ChequeNumber AS [Cheque #], CurrencyID AS Currency, BankID AS Bank,PayeeID AS Payee, ISNULL(AmountFC,Amount) AS Amount FROM Cheque_Issued\r\n\t\t\t\t\t\t\tWHERE ClearanceVoucherID='" + voucherID + "' AND ClearanceSysDocID='" + sysDocID + "'";
				FillDataSet(openEntryTransactionData, "Opening_Cheque_Issued", textCommand);
				return openEntryTransactionData;
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
				string textCommand = "SELECT TransactionID,SysDocID,VoucherID,CostCenterID,PayeeType,PayeeID,RegisterID,SecondRegisterID,\r\n                                (SELECT Bank.BankName FROM Cheque_Received CR LEFT JOIN Bank ON Bank.BankID= CR.BankID WHERE CR.ChequeID=GLT.ChequeID) AS BankName,\r\n\t\t\t\t\t\t\t\t(CASE PayeeType\r\n\t\t\t\t\t\t\t\tWHEN 'C' THEN Customer.CustomerName\r\n\t\t\t\t\t\t\t\tWHEN 'V' THEN Vendor.VendorName\r\n\t\t\t\t\t\t\t\tWHEN 'E' THEN Emp2.FirstName + ' ' + Emp2.LastName\r\n\t\t\t\t\t\t\t\tELSE Account.AccountName END) AS PayeeName,\r\n\t\t\t\t\t\t\t\tISNULL(AmountFC,Amount) AS Amount,TransactionDate,IsVoid,CurrencyRate,GLType,ChequebookID,CheckNumber,ChequeID,\r\n\t\t\t\t\t\t\t\tCheckDate,Reference,FirstAccountID,SecondAccountID,GLT.EmployeeID,Emp.FirstName + ' ' + Emp.LastName as EmployeeName,GLT.Description,\r\n\t\t\t\t\t\t\t\tISNULL(GLT.CurrencyID,(SELECT CurrencyID FROM Currency WHERE IsBase='True')) AS CurrencyID ,\r\n\t\t\t\t\t\t\t\tISNULL(FirstAccountID,RegisterID) AS TransferFromID,ISNULL(SecondAccountID,SecondRegisterID) AS TransferToID,\r\n\t\t\t\t\t\t\t\tTAcc.AccountName AS TransferFromName,TAcc2.AccountName AS TransferToName,\r\n                                GLT.DateCreated,GLT.DateUpdated,GLT.CreatedBy,GLT.UpdatedBy,RCR.ReasonName, GLT.ExpCode, GLT.ExpAmount, GLT.ExpPercent, EX.ExpenseName\r\n\t\t\t\t\t\t\t\tFROM GL_Transaction GLT LEFT OUTER JOIN Employee Emp ON Emp.EmployeeID=GLT.EmployeeID\r\n\t\t\t\t\t\t\t\tLEFT OUTER JOIN Account ON GLT.PayeeID=Account.AccountID \r\n\t\t\t\t\t\t\t\tLEFt OUTER JOIN Customer ON GLT.PayeeID=Customer.CustomerID \r\n\t\t\t\t\t\t\t\tLEFt OUTER JOIN Vendor ON GLT.PayeeID=Vendor.VendorID \r\n\t\t\t\t\t\t\t\tLEFt OUTER JOIN Employee Emp2 ON GLT.PayeeID=Emp2.EmployeeID\r\n\t\t\t\t\t\t\t\tLEFt OUTER JOIN Account TAcc ON GLT.FirstAccountID=TAcc.AccountID\r\n\t\t\t\t\t\t\t\tLEFt OUTER JOIN Account TAcc2 ON GLT.SecondAccountID=TAcc2.AccountID\r\n                                LEFT OUTER JOIN Returned_Cheque_Reason RCR ON GLT.ReasonID=RCR.ReasonID\r\n\t\t\t\t\t\t\t\tLEFT JOIN Expense_Code EX ON GLT.ExpCode=EX.ExpenseID\r\n\t\t\t\t\t\t\t\tWHERE SysDocID = '" + sysDocID + "' AND VoucherID IN (" + text + ")";
				FillDataSet(dataSet, "GL_Transaction", textCommand);
				if (dataSet == null || dataSet.Tables.Count == 0 || dataSet.Tables["GL_Transaction"].Rows.Count == 0)
				{
					return null;
				}
				dataSet.Tables[0].Rows[0]["TransactionID"].ToString();
				textCommand = "SELECT TD.*,Chequebook.BankID,Bank.BankName,\r\n                                   (CASE PayeeType\r\n                                    WHEN 'C' THEN Customer.CustomerName\r\n                                    WHEN 'V' THEN Vendor.VendorName\r\n                                    WHEN 'E' THEN Employee.FirstName + ' ' + Employee.LastName\r\n                                    ELSE Account.AccountName END) AS AccountName,JB.JobName\r\n                                    FROM TRANSACTION_DETAILS TD LEFt OUTER JOIN \r\n                                    Account ON TD.AccountID=Account.AccountID LEFt OUTER JOIN\r\n                                    Customer ON TD.PayeeID=Customer.CustomerID LEFt OUTER JOIN\r\n                                    Vendor ON TD.PayeeID=Vendor.VendorID LEFt OUTER JOIN\r\n                                    Employee ON TD.PayeeID=Employee.EmployeeID\r\n                                    LEFT OUTER JOIN Chequebook ON Chequebook.ChequebookID=TD.ChequebookID\r\n                                    LEFT OUTER JOIN Bank ON Chequebook.BankID=Bank.BankID\r\n                                    LEFt OUTER JOIN Job JB ON TD.JobID=JB.JobID\r\n\t\t\t\t\t\tWHERE SysDocID = '" + sysDocID + "' AND VoucherID IN (" + text + ")";
				FillDataSet(dataSet, "Transaction_Details", textCommand);
				SysDocTypes systemDocumentType = new SystemDocuments(base.DBConfig).GetSystemDocumentType(sysDocID, null);
				if (systemDocumentType == SysDocTypes.IssuedChequeReturn || systemDocumentType == SysDocTypes.IssuedChequeCancellation)
				{
					int num = 0;
					if (!dataSet.Tables["GL_Transaction"].Rows[0]["ChequeID"].IsDBNullOrEmpty())
					{
						num = int.Parse(dataSet.Tables["GL_Transaction"].Rows[0]["ChequeID"].ToString());
					}
					textCommand = "SELECT ChequeID,ChequeNumber,AC.AccountName as [BankName],Cheque_Issued.Status,Cheque_Issued.CurrencyID,PayeeAccountID,IsPrinted,PrintName,\r\n                            Cheque_Issued.ExchangeRate,AmountFC,BankAccountID,Cheque_Issued.PDCAccountID,ChequebookName,\r\n                            PayeeType,PayeeID,ChequeDate,Amount,\r\n                            CASE PayeeType WHEN 'C' THEN Customer.CustomerName\r\n                            WHEN 'V' THEN Vendor.VendorName\r\n                            WHEN 'A' THEN Account.AccountName\r\n                            WHEN 'E' THEN Employee.FirstName + ' ' + Employee.LastName END AS [PayeeName],\r\n                            CASE PayeeType WHEN 'C' THEN ISNULL(Customer.LegalName,ISNULL(Customer.CompanyName,Customer.CustomerName))\r\n                            WHEN 'V' THEN ISNULL(Vendor.LegalName,ISNULL(Vendor.CompanyName,Vendor.VendorName)) \r\n                            WHEN 'A' THEN Account.AccountName\r\n                            WHEN 'E' THEN Employee.FirstName + ' ' + Employee.LastName END AS [NameOnCheck],\r\n                            'A/C PAYEE ONLY' AS Stamp1,'NON NEGOTIABLE' AS Stamp2\r\n                            FROM Cheque_Issued LEFT OUTER JOIN Bank ON Bank.BankID= Cheque_Issued.BankID\r\n                            LEFT OUTER JOIN Chequebook ON Chequebook.ChequebookID=Cheque_Issued.ChequebookID\r\n                            LEFT OUTER JOIN Customer ON Customer.CustomerID=PayeeID \r\n                            LEFT OUTER JOIN Vendor ON Vendor.VendorID=PayeeID\r\n                            LEFT Outer JOIN Employee ON Employee.EmployeeID=PayeeID\r\n                            LEFT Outer JOIN Account ON Account.AccountID=PayeeID\r\n                            LEFT OUTER JOIN Account AC ON AC.AccountID = BankAccountID\r\n                            WHERE ChequeID=" + num;
					FillDataSet(dataSet, "Opening_Cheque_Issued", textCommand);
				}
				textCommand = "SELECT * FROM Bank_Fee_Details\r\n\t\t\t\t\t\tWHERE GLTransactionSysDocID = '" + sysDocID + "' AND GLTransactionVoucherID IN (" + text + ")";
				FillDataSet(dataSet, "Bank_Fee_Details", textCommand);
				textCommand = "SELECT * FROM AR_Payment_Allocation\r\n\t\t\t\t\tWHERE PaymentSysDocID = '" + sysDocID + "' AND PaymentVoucherID IN (" + text + ") UNION  SELECT * FROM AP_Payment_Allocation WHERE PaymentSysDocID = '" + sysDocID + "' AND PaymentVoucherID IN (" + text + ")";
				FillDataSet(dataSet, "Customer", textCommand);
				dataSet.Relations.Add("TransactionDetails", new DataColumn[2]
				{
					dataSet.Tables["GL_Transaction"].Columns["SysDocID"],
					dataSet.Tables["GL_Transaction"].Columns["VoucherID"]
				}, new DataColumn[2]
				{
					dataSet.Tables["Transaction_Details"].Columns["SysDocID"],
					dataSet.Tables["Transaction_Details"].Columns["VoucherID"]
				}, createConstraints: false);
				dataSet.Relations.Add("TransactionFeeDetails", new DataColumn[2]
				{
					dataSet.Tables["GL_Transaction"].Columns["SysDocID"],
					dataSet.Tables["GL_Transaction"].Columns["VoucherID"]
				}, new DataColumn[2]
				{
					dataSet.Tables["Bank_Fee_Details"].Columns["GLTransactionSysDocID"],
					dataSet.Tables["Bank_Fee_Details"].Columns["GLTransactionVoucherID"]
				}, createConstraints: false);
				dataSet.Relations.Add("PaymentAllocationDetails", new DataColumn[2]
				{
					dataSet.Tables["GL_Transaction"].Columns["SysDocID"],
					dataSet.Tables["GL_Transaction"].Columns["VoucherID"]
				}, new DataColumn[2]
				{
					dataSet.Tables["Customer"].Columns["PaymentSysDocID"],
					dataSet.Tables["Customer"].Columns["PaymentVoucherID"]
				}, createConstraints: false);
				dataSet.Tables["GL_Transaction"].Columns.Add("AmountInWords", typeof(string));
				foreach (DataRow row in dataSet.Tables["GL_Transaction"].Rows)
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

		public DataSet GetChequeDiscountTransactionToPrint(string sysDocID, string voucherID)
		{
			try
			{
				DataSet dataSet = new DataSet();
				string textCommand = "SELECT CD.*,BF.CurrentAccountID,AC.AccountName,BF.LimitAmount FROM Cheque_Discount CD\r\n\t\t\t\t\t\t\t\tINNER JOIN Bank_Facility BF ON CD.BankFacilityID = BF.FacilityID\r\n\t\t\t\t\t\t\t\tINNER JOIN Account AC ON AC.AccountID = BF.CurrentAccountID WHERE VoucherID='" + voucherID + "' AND SysDocID='" + sysDocID + "'";
				FillDataSet(dataSet, "Cheque_Discount", textCommand);
				if (dataSet == null || dataSet.Tables.Count == 0 || dataSet.Tables["Cheque_Discount"].Rows.Count == 0)
				{
					return null;
				}
				textCommand = "SELECT  'True' AS 'D', TD.ChequeID,TD.SysDocID,TD.VoucherID,CQR.ChequeDate AS [Chq Date],CQR.ChequeNumber [Chq #],CQR.BankID,\r\n\t\t\t\t\t\t\tCQR.PayeeID,CQR.PayeeType,CQR.PayeeAccountID,CQR.PDCAccountID,Bank.BankName [Bank Name],TD.BankChargeAmount,TD.DiscountAmount,\r\n\t\t\t\t\t\t\t\r\n\t\t\t\t\t\t\tCASE CQR.PayeeType WHEN 'C' THEN Customer.CustomerName\r\n\t\t\t\t\t\t\tWHEN 'V' THEN Vendor.VendorName\r\n\t\t\t\t\t\t\tWHEN 'A' THEN Account.AccountName\r\n\t\t\t\t\t\t\tWHEN 'E' THEN Employee.FirstName + ' ' + Employee.LastName END AS [Payee Name] ,\r\n\t\t\t\t\t\t\tISNULL(CQR.CurrencyID,(SELECT CurrencyID FROM Currency WHERE Currency.IsBase='True')) AS Currency,ISNULL(CQR.AmountFC,CQR.Amount) AS Amount\r\n\t\t\t\t\t\t\tFROM   Cheque_Discount_Detail TD \r\n\t\t\t\t\t\t\tINNER JOIN Cheque_Received CQR ON CQR.ChequeID = TD.ChequeID\r\n\t\t\t\t\t\t\tINNER JOIN Bank ON CQR.BankID=Bank.BankID\r\n\t\t\t\t\t\t\tLEFT OUTER JOIN Cheque_Discount TR ON TD.VoucherID=TR.VoucherID AND TD.SysDocID=TR.SysDocID\r\n\t\t\t\t\t\t\tLEFT OUTER JOIN Customer ON Customer.CustomerID=CQR.PayeeID \r\n\t\t\t\t\t\t\tLEFT OUTER JOIN Vendor ON Vendor.VendorID=CQR.PayeeID\r\n\t\t\t\t\t\t\tLEFT Outer JOIN Employee ON Employee.EmployeeID=CQR.PayeeID\r\n\t\t\t\t\t\t\tLEFT Outer JOIN Account ON Account.AccountID=CQR.PayeeID\r\n\t\t\t\t\t\t\tWHERE TD.VoucherID='" + voucherID + "' AND TD.SysDocID='" + sysDocID + "'";
				FillDataSet(dataSet, "Cheque_Discount_Detail", textCommand);
				return dataSet;
			}
			catch
			{
				throw;
			}
		}

		public DataSet GetIssuedChequeClearanceTransactionToPrint(string sysDocID, string voucherID)
		{
			try
			{
				DataSet dataSet = new DataSet();
				string textCommand = "SELECT * FROM GL_Transaction WHERE VoucherID='" + voucherID + "' AND SysDocID='" + sysDocID + "'";
				FillDataSet(dataSet, "GL_Transaction", textCommand);
				if (dataSet == null || dataSet.Tables.Count == 0 || dataSet.Tables["GL_Transaction"].Rows.Count == 0)
				{
					return null;
				}
				dataSet.Tables["GL_Transaction"].Rows[0]["TransactionID"].ToString();
				textCommand = "SELECT     'True' AS 'D',TD.CheckDate AS [Chq Date],TD.CheckNumber [Chq #],Bank.BankName [Bank Name],\r\n\t\t\t\t\t\t\t\r\n\t\t\t\t\t\t\tCASE TD.PayeeType WHEN 'C' THEN Customer.CustomerName\r\n\t\t\t\t\t\t\tWHEN 'V' THEN Vendor.VendorName\r\n\t\t\t\t\t\t\tWHEN 'A' THEN Account.AccountName\r\n\t\t\t\t\t\t\tWHEN 'E' THEN Employee.FirstName + ' ' + Employee.LastName END AS [Payee Name] ,\r\n\t\t\t\t\t\t\tISNULL(TR.CurrencyID,(SELECT CurrencyID FROM Currency WHERE Currency.IsBase='True')) AS Currency,ISNULL(TD.AmountFC,TD.Amount) AS Amount\r\n\t\t\t\t\t\t\tFROM         Transaction_Details TD INNER JOIN Bank ON TD.BankID=Bank.BankID\r\n\t\t\t\t\t\t\tLEFT OUTER JOIN GL_Transaction TR ON TD.VoucherID=TR.VoucherID AND TD.SysDocID=TR.SysDocID\r\n\t\t\t\t\t\t\tLEFT OUTER JOIN Customer ON Customer.CustomerID=TD.PayeeID \r\n\t\t\t\t\t\t\tLEFT OUTER JOIN Vendor ON Vendor.VendorID=TD.PayeeID\r\n\t\t\t\t\t\t\tLEFT Outer JOIN Employee ON Employee.EmployeeID=TD.PayeeID\r\n\t\t\t\t\t\t\tLEFT Outer JOIN Account ON Account.AccountID=TD.PayeeID\r\n\t\t\t\t\t\t\tWHERE TD.VoucherID='" + voucherID + "' AND TD.SysDocID='" + sysDocID + "'";
				dataSet.Tables.Remove("Transaction_Details");
				FillDataSet(dataSet, "Transaction_Details", textCommand);
				textCommand = "SELECT 'True' AS C,SysDocID,VoucherID, ChequeDate AS [Chq Date],ChequeNumber AS [Cheque #], CurrencyID AS Currency, BankID AS Bank,PayeeID AS Payee, ISNULL(AmountFC,Amount) AS Amount FROM Cheque_Issued\r\n\t\t\t\t\t\t\tWHERE ClearanceVoucherID='" + voucherID + "' AND ClearanceSysDocID='" + sysDocID + "'";
				FillDataSet(dataSet, "Opening_Cheque_Issued", textCommand);
				return dataSet;
			}
			catch
			{
				throw;
			}
		}

		public DataSet GetExpenseList(DateTime from, DateTime to, bool showVoid)
		{
			DataSet dataSet = new DataSet();
			string text = StoreConfiguration.ToSqlDateTimeString(from);
			string text2 = StoreConfiguration.ToSqlDateTimeString(to);
			string text3 = "SELECT ISNULL(IsVoid,'False') AS V,GLT.SysDocID [Doc ID],VoucherID [Doc Number],TransactionDate [Date],\r\n\t\t\t\t\t\t\tCASE SysDocType WHEN 8 THEN 'Cash' ELSE 'Cheque' END AS [Type],CostCenterID [C.C.],PayeeID [Payee Code],\r\n\t\t\t\t\t\t\t(CASE PayeeType\r\n\t\t\t\t\t\t\t\t\t\t\t\t\tWHEN 'C' THEN Customer.CustomerName\r\n\t\t\t\t\t\t\t\t\t\t\t\t\tWHEN 'V' THEN Vendor.VendorName\r\n\t\t\t\t\t\t\t\t\t\t\t\t\tWHEN 'E' THEN Employee.FirstName\r\n\t\t\t\t\t\t\t\t\t\t\t\t\tELSE Account.AccountName END) AS [Payee Name],Reference [Ref],\r\n\t\t\t\t\t\t\tRegisterID [Reg],GLT.Description [Desc],\r\n\t\t\t\t\t\t\tISNULL(GLT.CurrencyID,(SELECT CurrencyID FROM Currency WHERE IsBase=1)) AS [Cur] ,ISNULL(AmountFC,Amount) AS [Amount]\r\n\t\t\t\t\t\t\tFROM GL_Transaction GLT\r\n\t\t\t\t\t\t\t \r\n\t\t\t\t\t\t\tLEFt OUTER JOIN Account ON GLT.PayeeID=Account.AccountID LEFt OUTER JOIN\r\n\t\t\t\t\t\t\t\t\t\t\t\t\tCustomer ON GLT.PayeeID=Customer.CustomerID LEFt OUTER JOIN\r\n\t\t\t\t\t\t\t\t\t\t\t\t\tVendor ON GLT.PayeeID=Vendor.VendorID LEFt OUTER JOIN\r\n\t\t\t\t\t\t\t\t\t\t\t\t\tEmployee ON GLT.PayeeID=Employee.EmployeeID\r\n\t\t\t\t\t\t\tINNER JOIN System_Document SD ON SD.SysDocID=GLT.SysDocID\r\n\t\t\t\t\t\t\tWHERE SysDocType IN (8,9) ";
			if (from != DateTime.MinValue)
			{
				text3 = text3 + " AND TransactionDate Between '" + text + "' AND '" + text2 + "'";
			}
			if (!showVoid)
			{
				text3 += " AND ISNULL(IsVoid,'False')='False'";
			}
			SqlCommand sqlCommand = new SqlCommand(text3);
			FillDataSet(dataSet, "GL_Transaction", sqlCommand);
			return dataSet;
		}

		public DataSet GetDebitNoteList(DateTime from, DateTime to, bool showVoid)
		{
			DataSet dataSet = new DataSet();
			string text = StoreConfiguration.ToSqlDateTimeString(from);
			string text2 = StoreConfiguration.ToSqlDateTimeString(to);
			string text3 = "SELECT ISNULL(IsVoid,'False') AS V,GLT.SysDocID [Doc ID],VoucherID [Doc Number],TransactionDate [Date],\r\n\t\t\t\t\t\t\tCostCenterID [C.C.],PayeeID [Payee Code],\r\n\t\t\t\t\t\t\t(CASE PayeeType\r\n\t\t\t\t\t\t\t\t\t\t\t\t\tWHEN 'C' THEN Customer.CustomerName\r\n\t\t\t\t\t\t\t\t\t\t\t\t\tWHEN 'V' THEN Vendor.VendorName\r\n\t\t\t\t\t\t\t\t\t\t\t\t\tWHEN 'E' THEN Employee.FirstName\r\n\t\t\t\t\t\t\t\t\t\t\t\t\tELSE Account.AccountName END) AS [Payee Name],Reference [Ref],\r\n\t\t\t\t\t\t\tRegisterID [Reg],GLT.Description [Desc],\r\n\t\t\t\t\t\t\tISNULL(GLT.CurrencyID,(SELECT CurrencyID FROM Currency WHERE IsBase=1)) AS [Cur] ,ISNULL(AmountFC,Amount) AS [Amount]\r\n\t\t\t\t\t\t\tFROM GL_Transaction GLT\r\n\t\t\t\t\t\t\t \r\n\t\t\t\t\t\t\tLEFt OUTER JOIN Account ON GLT.PayeeID=Account.AccountID LEFt OUTER JOIN\r\n\t\t\t\t\t\t\t\t\t\t\t\t\tCustomer ON GLT.PayeeID=Customer.CustomerID LEFt OUTER JOIN\r\n\t\t\t\t\t\t\t\t\t\t\t\t\tVendor ON GLT.PayeeID=Vendor.VendorID LEFt OUTER JOIN\r\n\t\t\t\t\t\t\t\t\t\t\t\t\tEmployee ON GLT.PayeeID=Employee.EmployeeID\r\n\t\t\t\t\t\t\tINNER JOIN System_Document SD ON SD.SysDocID=GLT.SysDocID\r\n\t\t\t\t\t\t\tWHERE SysDocType IN (10) ";
			if (from != DateTime.MinValue)
			{
				text3 = text3 + " AND TransactionDate Between '" + text + "' AND '" + text2 + "'";
			}
			if (!showVoid)
			{
				text3 += " AND ISNULL(IsVoid,'False')='False'";
			}
			SqlCommand sqlCommand = new SqlCommand(text3);
			FillDataSet(dataSet, "GL_Transaction", sqlCommand);
			return dataSet;
		}

		public DataSet GetCreditNoteList(DateTime from, DateTime to, bool showVoid)
		{
			DataSet dataSet = new DataSet();
			string text = StoreConfiguration.ToSqlDateTimeString(from);
			string text2 = StoreConfiguration.ToSqlDateTimeString(to);
			string text3 = "SELECT ISNULL(IsVoid,'False') AS V,GLT.SysDocID [Doc ID],VoucherID [Doc Number],TransactionDate [Date],\r\n\t\t\t\t\t\t\tCostCenterID [C.C.],PayeeID [Payee Code],\r\n\t\t\t\t\t\t\t(CASE PayeeType\r\n\t\t\t\t\t\t\t\t\t\t\t\t\tWHEN 'C' THEN Customer.CustomerName\r\n\t\t\t\t\t\t\t\t\t\t\t\t\tWHEN 'V' THEN Vendor.VendorName\r\n\t\t\t\t\t\t\t\t\t\t\t\t\tWHEN 'E' THEN Employee.FirstName\r\n\t\t\t\t\t\t\t\t\t\t\t\t\tELSE Account.AccountName END) AS [Payee Name],Reference [Ref],\r\n\t\t\t\t\t\t\tRegisterID [Reg],GLT.Description [Desc],\r\n\t\t\t\t\t\t\tISNULL(GLT.CurrencyID,(SELECT CurrencyID FROM Currency WHERE IsBase=1)) AS [Cur] ,ISNULL(AmountFC,Amount) AS [Amount]\r\n\t\t\t\t\t\t\tFROM GL_Transaction GLT\r\n\t\t\t\t\t\t\t \r\n\t\t\t\t\t\t\tLEFt OUTER JOIN Account ON GLT.PayeeID=Account.AccountID LEFt OUTER JOIN\r\n\t\t\t\t\t\t\t\t\t\t\t\t\tCustomer ON GLT.PayeeID=Customer.CustomerID LEFt OUTER JOIN\r\n\t\t\t\t\t\t\t\t\t\t\t\t\tVendor ON GLT.PayeeID=Vendor.VendorID LEFt OUTER JOIN\r\n\t\t\t\t\t\t\t\t\t\t\t\t\tEmployee ON GLT.PayeeID=Employee.EmployeeID\r\n\t\t\t\t\t\t\tINNER JOIN System_Document SD ON SD.SysDocID=GLT.SysDocID\r\n\t\t\t\t\t\t\tWHERE SysDocType IN (11) ";
			if (from != DateTime.MinValue)
			{
				text3 = text3 + " AND TransactionDate Between '" + text + "' AND '" + text2 + "'";
			}
			if (!showVoid)
			{
				text3 += " AND ISNULL(IsVoid,'False')='False'";
			}
			SqlCommand sqlCommand = new SqlCommand(text3);
			FillDataSet(dataSet, "GL_Transaction", sqlCommand);
			return dataSet;
		}

		public DataSet GetReceiptVoucherList(DateTime from, DateTime to, bool showVoid)
		{
			DataSet dataSet = new DataSet();
			string text = StoreConfiguration.ToSqlDateTimeString(from);
			string text2 = StoreConfiguration.ToSqlDateTimeString(to);
			string text3 = "SELECT ISNULL(IsVoid,'False') AS V,GLT.SysDocID [Doc ID],VoucherID [Doc Number],TransactionDate [Date],\r\n\t\t\t\t\t\t\tCASE SysDocType WHEN 3 THEN 'Cash'  WHEN 2 THEN 'Cheque'  ELSE 'TT' END AS [Type],CostCenterID [C.C.],PayeeID [Payee Code],\r\n\t\t\t\t\t\t\t(CASE PayeeType\r\n\t\t\t\t\t\t\t\t\t\t\t\t\tWHEN 'C' THEN Customer.CustomerName\r\n\t\t\t\t\t\t\t\t\t\t\t\t\tWHEN 'V' THEN Vendor.VendorName\r\n\t\t\t\t\t\t\t\t\t\t\t\t\tWHEN 'E' THEN Employee.FirstName\r\n\t\t\t\t\t\t\t\t\t\t\t\t\tELSE Account.AccountName END) AS [Payee Name],Reference [Ref],\r\n\t\t\t\t\t\t\tRegisterID [Reg],GLT.Description [Desc],\r\n\t\t\t\t\t\t\tISNULL(GLT.CurrencyID,(SELECT CurrencyID FROM Currency WHERE IsBase=1)) AS [Cur] ,ISNULL(AmountFC,Amount) AS [Amount], CheckNumber [Chq #]\r\n\t\t\t\t\t\t\tFROM GL_Transaction GLT\r\n\t\t\t\t\t\t\t \r\n\t\t\t\t\t\t\tLEFt OUTER JOIN Account ON GLT.PayeeID=Account.AccountID LEFt OUTER JOIN\r\n\t\t\t\t\t\t\t\t\t\t\t\t\tCustomer ON GLT.PayeeID=Customer.CustomerID LEFt OUTER JOIN\r\n\t\t\t\t\t\t\t\t\t\t\t\t\tVendor ON GLT.PayeeID=Vendor.VendorID LEFt OUTER JOIN\r\n\t\t\t\t\t\t\t\t\t\t\t\t\tEmployee ON GLT.PayeeID=Employee.EmployeeID\r\n\t\t\t\t\t\t\tINNER JOIN System_Document SD ON SD.SysDocID=GLT.SysDocID\r\n\t\t\t\t\t\t\tWHERE SysDocType IN (234) ";
			if (from != DateTime.MinValue)
			{
				text3 = text3 + " AND TransactionDate Between '" + text + "' AND '" + text2 + "'";
			}
			if (!showVoid)
			{
				text3 += " AND ISNULL(IsVoid,'False')='False'";
			}
			SqlCommand sqlCommand = new SqlCommand(text3);
			FillDataSet(dataSet, "GL_Transaction", sqlCommand);
			return dataSet;
		}

		public DataSet GetReturnVoucherList(DateTime from, DateTime to, bool showVoid)
		{
			DataSet dataSet = new DataSet();
			string text = StoreConfiguration.ToSqlDateTimeString(from);
			string text2 = StoreConfiguration.ToSqlDateTimeString(to);
			string text3 = "SELECT ISNULL(IsVoid,'False') AS V,GLT.SysDocID [Doc ID],VoucherID [Doc Number],TransactionDate [Date],\r\n\t\t\t\t\t\t\tCASE SysDocType WHEN 3 THEN 'Cash'  WHEN 2 THEN 'Cheque'  ELSE 'TT' END AS [Type],CostCenterID [C.C.],PayeeID [Payee Code],\r\n\t\t\t\t\t\t\t(CASE PayeeType\r\n\t\t\t\t\t\t\t\t\t\t\t\t\tWHEN 'C' THEN Customer.CustomerName\r\n\t\t\t\t\t\t\t\t\t\t\t\t\tWHEN 'V' THEN Vendor.VendorName\r\n\t\t\t\t\t\t\t\t\t\t\t\t\tWHEN 'E' THEN Employee.FirstName\r\n\t\t\t\t\t\t\t\t\t\t\t\t\tELSE Account.AccountName END) AS [Payee Name],Reference [Ref],\r\n\t\t\t\t\t\t\tRegisterID [Reg],GLT.Description [Desc],\r\n\t\t\t\t\t\t\tISNULL(GLT.CurrencyID,(SELECT CurrencyID FROM Currency WHERE IsBase=1)) AS [Cur] ,ISNULL(AmountFC,Amount) AS [Amount], CheckNumber [Chq #]\r\n\t\t\t\t\t\t\tFROM GL_Transaction GLT\r\n\t\t\t\t\t\t\t \r\n\t\t\t\t\t\t\tLEFt OUTER JOIN Account ON GLT.PayeeID=Account.AccountID LEFt OUTER JOIN\r\n\t\t\t\t\t\t\t\t\t\t\t\t\tCustomer ON GLT.PayeeID=Customer.CustomerID LEFt OUTER JOIN\r\n\t\t\t\t\t\t\t\t\t\t\t\t\tVendor ON GLT.PayeeID=Vendor.VendorID LEFt OUTER JOIN\r\n\t\t\t\t\t\t\t\t\t\t\t\t\tEmployee ON GLT.PayeeID=Employee.EmployeeID\r\n\t\t\t\t\t\t\tINNER JOIN System_Document SD ON SD.SysDocID=GLT.SysDocID\r\n\t\t\t\t\t\t\tWHERE SysDocType IN (12) ";
			if (from != DateTime.MinValue)
			{
				text3 = text3 + " AND TransactionDate Between '" + text + "' AND '" + text2 + "'";
			}
			if (!showVoid)
			{
				text3 += " AND ISNULL(IsVoid,'False')='False'";
			}
			SqlCommand sqlCommand = new SqlCommand(text3);
			FillDataSet(dataSet, "GL_Transaction", sqlCommand);
			return dataSet;
		}

		public DataSet GetChequeReceiptList(DateTime from, DateTime to, bool showVoid)
		{
			DataSet dataSet = new DataSet();
			string text = StoreConfiguration.ToSqlDateTimeString(from);
			string text2 = StoreConfiguration.ToSqlDateTimeString(to);
			string text3 = "SELECT SysDocID [Doc ID], VoucherID [Doc Number], ReceiptDate [Date], PayeeID,  ChequeNumber [Cheque #], ChequeDate [Cheque Date], BankID [Bank], Amount, \r\n                            CASE Status WHEN 1 THEN 'Undeposited' WHEN 2 THEN 'Deposited'  WHEN 3 THEN 'Discounted' WHEN 4 THEN 'SentToBank' WHEN 7 THEN 'Cleared' WHEN 8 THEN 'Bounced' WHEN 9 THEN 'Cancelled'  END AS [Status]\r\n                            FROM Cheque_Received WHERE 1=1";
			if (from != DateTime.MinValue)
			{
				text3 = text3 + " AND ReceiptDate Between '" + text + "' AND '" + text2 + "'";
			}
			if (!showVoid)
			{
				text3 += " AND ISNULL(IsVoid,'False')='False'";
			}
			SqlCommand sqlCommand = new SqlCommand(text3);
			FillDataSet(dataSet, "GL_Transaction", sqlCommand);
			return dataSet;
		}

		public DataSet GetChequePaymentList(DateTime from, DateTime to, bool showVoid)
		{
			DataSet dataSet = new DataSet();
			string text = StoreConfiguration.ToSqlDateTimeString(from);
			string text2 = StoreConfiguration.ToSqlDateTimeString(to);
			string text3 = "SELECT SysDocID [Doc ID], VoucherID [Doc Number], IssueDate [Date], PayeeID,  ChequeNumber [Cheque #], ChequeDate [Cheque Date], BankID [Bank], Amount, \r\n                            CASE Status WHEN 1 THEN 'Bank' WHEN 2 THEN 'Issued'  WHEN 4 THEN 'Cleared' WHEN 5 THEN 'Bounced' WHEN 8 THEN 'Voided' WHEN 9 THEN 'Cancelled'  END AS [Status]\r\n                            FROM Cheque_Issued WHERE 1=1";
			if (from != DateTime.MinValue)
			{
				text3 = text3 + " AND IssueDate Between '" + text + "' AND '" + text2 + "'";
			}
			if (!showVoid)
			{
				text3 += " AND ISNULL(IsVoid,'False')='False'";
			}
			SqlCommand sqlCommand = new SqlCommand(text3);
			FillDataSet(dataSet, "GL_Transaction", sqlCommand);
			return dataSet;
		}

		public DataSet GetPaymentVoucherList(DateTime from, DateTime to, bool showVoid, TransactionPaymentType paymentType)
		{
			DataSet dataSet = new DataSet();
			string text = StoreConfiguration.ToSqlDateTimeString(from);
			string text2 = StoreConfiguration.ToSqlDateTimeString(to);
			byte b = 4;
			switch (paymentType)
			{
			case TransactionPaymentType.TT:
				b = 64;
				break;
			case TransactionPaymentType.TR:
				b = 79;
				break;
			case TransactionPaymentType.Cheque:
				b = 235;
				break;
			}
			string text3 = "SELECT ISNULL(GLT.IsVoid,'False') AS V,GLT.SysDocID [Doc ID],GLT.VoucherID [Doc Number],TransactionDate [Date],\r\n\t\t\t\t\t\t\tCASE SysDocType WHEN 4 THEN 'Cash' WHEN 5 THEN 'Cheque' ELSE 'TT' END AS [Type],CostCenterID [C.C.],GLT.PayeeID [Payee Code],CI.ChequeNumber [ChequeNumber],CI.ChequeDate[ChequeDate],\r\n\t\t\t\t\t\t\t(CASE GLT.PayeeType\r\n\t\t\t\t\t\t\t\t\t\t\t\t\tWHEN 'C' THEN Customer.CustomerName\r\n\t\t\t\t\t\t\t\t\t\t\t\t\tWHEN 'V' THEN Vendor.VendorName\r\n\t\t\t\t\t\t\t\t\t\t\t\t\tWHEN 'E' THEN Employee.FirstName\r\n\t\t\t\t\t\t\t\t\t\t\t\t\tELSE Account.AccountName END) AS [Payee Name],GLT.Reference [Ref],\r\n\t\t\t\t\t\t\tRegisterID [Reg],GLT.Description [Desc],\r\n\t\t\t\t\t\t\tISNULL(GLT.CurrencyID,(SELECT CurrencyID FROM Currency WHERE IsBase=1)) AS [Cur] ,ISNULL(GLT.AmountFC,GLT.Amount) AS [Amount]\r\n\t\t\t\t\t\t\tFROM GL_Transaction GLT\r\n\t\t\t\t\t\t\t \r\n\t\t\t\t\t\t\tLEFt OUTER JOIN Account ON GLT.PayeeID=Account.AccountID LEFt OUTER JOIN\r\n\t\t\t\t\t\t\t\t\t\t\t\t\tCustomer ON GLT.PayeeID=Customer.CustomerID LEFt OUTER JOIN\r\n\t\t\t\t\t\t\t\t\t\t\t\t\tVendor ON GLT.PayeeID=Vendor.VendorID LEFt OUTER JOIN\r\n\t\t\t\t\t\t\t\t\t\t\t\t\tEmployee ON GLT.PayeeID=Employee.EmployeeID LEFT OUTER JOIN \t\t\t\t\t\t\t\t\t\t\t\t\t\r\n\t\t\t\t\t\t\t\t\t\t\t\t\tCheque_Issued CI ON CI.SysDocID=GLT.SysDocID and CI.VoucherID=GLT.VoucherID\r\n\t\t\t\t\t\t\tINNER JOIN System_Document SD ON SD.SysDocID=GLT.SysDocID\r\n                            WHERE SysDocType IN (" + b + ")";
			if (from != DateTime.MinValue)
			{
				text3 = text3 + " AND TransactionDate Between '" + text + "' AND '" + text2 + "'";
			}
			if (!showVoid)
			{
				text3 += " AND ISNULL(GLT.IsVoid,'False')='False'";
			}
			SqlCommand sqlCommand = new SqlCommand(text3);
			FillDataSet(dataSet, "GL_Transaction", sqlCommand);
			return dataSet;
		}

		public DataSet GetTransferList(DateTime from, DateTime to, bool showVoid)
		{
			DataSet dataSet = new DataSet();
			string text = StoreConfiguration.ToSqlDateTimeString(from);
			string text2 = StoreConfiguration.ToSqlDateTimeString(to);
			string text3 = "SELECT ISNULL(IsVoid,'False') AS V,GLT.SysDocID [Doc ID],VoucherID [Doc Number],TransactionDate [Date],\r\n\t\t\t\t\t\t\tReference [Ref],Acc1.AccountName [From],Acc2.AccountName [To],\r\n\t\t\t\t\t\t\tGLT.Description [Desc],\r\n\t\t\t\t\t\t\tISNULL(GLT.CurrencyID,(SELECT CurrencyID FROM Currency WHERE IsBase=1)) AS [Cur] ,ISNULL(AmountFC,Amount) AS [Amount]\r\n\t\t\t\t\t\t\tFROM GL_Transaction GLT\r\n\t\t\t\t\t\t\tLEFt OUTER JOIN Account ACC1 ON GLT.FirstAccountID=ACC1.AccountID LEFt OUTER JOIN\r\n\t\t\t\t\t\t\tAccount ACC2 ON GLT.SecondAccountID=ACC2.AccountID \r\n\t\t\t\t\t\t\tINNER JOIN System_Document SD ON SD.SysDocID=GLT.SysDocID\r\n\t\t\t\t\t\t\tWHERE SysDocType IN (6)";
			if (from != DateTime.MinValue)
			{
				text3 = text3 + " AND TransactionDate Between '" + text + "' AND '" + text2 + "'";
			}
			if (!showVoid)
			{
				text3 += " AND ISNULL(IsVoid,'False')='False'";
			}
			SqlCommand sqlCommand = new SqlCommand(text3);
			FillDataSet(dataSet, "GL_Transaction", sqlCommand);
			return dataSet;
		}

		public DataSet GetChequeDepositList(DateTime from, DateTime to, bool showVoid)
		{
			DataSet dataSet = new DataSet();
			string text = StoreConfiguration.ToSqlDateTimeString(from);
			string text2 = StoreConfiguration.ToSqlDateTimeString(to);
			string text3 = "SELECT ISNULL(IsVoid,'False') AS V,GLT.SysDocID [Doc ID],VoucherID [Doc Number],TransactionDate [Date],\r\n\t\t\t\t\t\t\tReference [Ref],Acc1.AccountName [From],Acc2.AccountName [To],\r\n\t\t\t\t\t\t\tGLT.Description [Desc],\r\n\t\t\t\t\t\t\tISNULL(GLT.CurrencyID,(SELECT CurrencyID FROM Currency WHERE IsBase=1)) AS [Cur] ,ISNULL(AmountFC,Amount) AS [Amount]\r\n\t\t\t\t\t\t\tFROM GL_Transaction GLT\r\n\t\t\t\t\t\t\tLEFt OUTER JOIN Account ACC1 ON GLT.FirstAccountID=ACC1.AccountID LEFt OUTER JOIN\r\n\t\t\t\t\t\t\tAccount ACC2 ON GLT.SecondAccountID=ACC2.AccountID \r\n\t\t\t\t\t\t\tINNER JOIN System_Document SD ON SD.SysDocID=GLT.SysDocID\r\n\t\t\t\t\t\t\tWHERE SysDocType IN (7)";
			if (from != DateTime.MinValue)
			{
				text3 = text3 + " AND TransactionDate Between '" + text + "' AND '" + text2 + "'";
			}
			if (!showVoid)
			{
				text3 += " AND ISNULL(IsVoid,'False')='False'";
			}
			SqlCommand sqlCommand = new SqlCommand(text3);
			FillDataSet(dataSet, "GL_Transaction", sqlCommand);
			return dataSet;
		}

		public DataSet GetReceivedChequeReturnList(DateTime from, DateTime to, bool showVoid)
		{
			DataSet dataSet = new DataSet();
			string text = StoreConfiguration.ToSqlDateTimeString(from);
			string text2 = StoreConfiguration.ToSqlDateTimeString(to);
			string text3 = "SELECT ISNULL(IsVoid,'False') AS V,GLT.SysDocID [Doc ID],VoucherID [Doc Number],TransactionDate [Date],\r\n\t\t\t\t\t\t\tCASE SysDocType WHEN 3 THEN 'Cash'  WHEN 2 THEN 'Cheque'  ELSE 'TT' END AS [Type],CostCenterID [C.C.],PayeeID [Payee Code],\r\n\t\t\t\t\t\t\t(CASE PayeeType\r\n\t\t\t\t\t\t\t\t\t\t\t\t\tWHEN 'C' THEN Customer.CustomerName\r\n\t\t\t\t\t\t\t\t\t\t\t\t\tWHEN 'V' THEN Vendor.VendorName\r\n\t\t\t\t\t\t\t\t\t\t\t\t\tWHEN 'E' THEN Employee.FirstName\r\n\t\t\t\t\t\t\t\t\t\t\t\t\tELSE Account.AccountName END) AS [Payee Name],Reference [Ref],\r\n\t\t\t\t\t\t\tRegisterID [Reg],GLT.Description [Desc],\r\n\t\t\t\t\t\t\tISNULL(GLT.CurrencyID,(SELECT CurrencyID FROM Currency WHERE IsBase=1)) AS [Cur] ,ISNULL(AmountFC,Amount) AS [Amount], CheckNumber [Chq #]\r\n\t\t\t\t\t\t\tFROM GL_Transaction GLT\r\n\t\t\t\t\t\t\t \r\n\t\t\t\t\t\t\tLEFt OUTER JOIN Account ON GLT.PayeeID=Account.AccountID LEFt OUTER JOIN\r\n\t\t\t\t\t\t\t\t\t\t\t\t\tCustomer ON GLT.PayeeID=Customer.CustomerID LEFt OUTER JOIN\r\n\t\t\t\t\t\t\t\t\t\t\t\t\tVendor ON GLT.PayeeID=Vendor.VendorID LEFt OUTER JOIN\r\n\t\t\t\t\t\t\t\t\t\t\t\t\tEmployee ON GLT.PayeeID=Employee.EmployeeID\r\n\t\t\t\t\t\t\tINNER JOIN System_Document SD ON SD.SysDocID=GLT.SysDocID\r\n\t\t\t\t\t\t\tWHERE SysDocType IN (13) ";
			if (from != DateTime.MinValue)
			{
				text3 = text3 + " AND TransactionDate Between '" + text + "' AND '" + text2 + "'";
			}
			if (!showVoid)
			{
				text3 += " AND ISNULL(IsVoid,'False')='False'";
			}
			SqlCommand sqlCommand = new SqlCommand(text3);
			FillDataSet(dataSet, "GL_Transaction", sqlCommand);
			return dataSet;
		}

		public DataSet GetChequeMaturityReport(DateTime from, DateTime to, string fromBank, string toBank)
		{
			DataSet dataSet = new DataSet();
			string text = StoreConfiguration.ToSqlDateTimeString(from);
			string text2 = StoreConfiguration.ToSqlDateTimeString(to);
			string text3 = "SELECT * FROM GL_Transaction WHERE TransactionDate Between '" + text + "' AND '" + text2 + "'";
			if (fromBank != "")
			{
				text3 = text3 + " AND Cheque_Issued.BankID BETWEEN '" + fromBank + "' AND '" + toBank + "' ";
			}
			FillDataSet(dataSet, "GL_Transaction", text3);
			text3 = text3 + "SELECT TD.*,\r\n\t\t\t\t\t\t(CASE PayeeType\r\n\t\t\t\t\t\tWHEN 'C' THEN Customer.CustomerName\r\n\t\t\t\t\t\tWHEN 'V' THEN Vendor.VendorName\r\n\t\t\t\t\t\tWHEN 'E' THEN Employee.FirstName\r\n\t\t\t\t\t\tELSE Account.AccountName END) AS AccountName\r\n\t\t\t\t\t\tFROM TRANSACTION_DETAILS TD LEFt OUTER JOIN \r\n\t\t\t\t\t\tAccount ON TD.AccountID=Account.AccountID LEFt OUTER JOIN\r\n\t\t\t\t\t\tCustomer ON TD.PayeeID=Customer.CustomerID LEFt OUTER JOIN\r\n\t\t\t\t\t\tVendor ON TD.PayeeID=Vendor.VendorID LEFt OUTER JOIN\r\n\t\t\t\t\t\tEmployee ON TD.PayeeID=Employee.EmployeeID\r\n\t\t\t\t\t\tWHERE TransactionDate Between '" + text + "' AND '" + text2 + "'";
			FillDataSet(dataSet, "Transaction_Details", text3);
			return dataSet;
		}

		public int ValidateBlankCheque(string chequebookID, string chequeNumber, string sysDocID, string voucherID)
		{
			string exp = "SELECT Status FROM Cheque_Register WHERE ChequebookID = '" + chequebookID + "' AND ChequeNumber ='" + chequeNumber + "' ";
			object obj = ExecuteScalar(exp);
			if (obj == null || obj.ToString() == "")
			{
				return -1;
			}
			if (int.Parse(obj.ToString()) != 1)
			{
				if (sysDocID != "")
				{
					exp = "SELECT COUNT(*) FROM dbo.Opening_Cheque_Issued \r\n                                WHERE ChequeNumber = '" + chequeNumber + "' AND ChequebookID = '" + chequebookID + "' AND SysDocID = '" + sysDocID + "' AND VoucherID = '" + voucherID + "'";
					obj = ExecuteScalar(exp);
					if (obj != null && obj.ToString() != "" && int.Parse(obj.ToString()) > 0)
					{
						return 0;
					}
					return -2;
				}
				return -2;
			}
			return 0;
		}

		public bool TransactionHasProcessedIssuedCheques(string sysDocID, string VoucherID, SqlTransaction sqlTransaction)
		{
			try
			{
				string exp = "SELECT COUNT(*) FROM Opening_Cheque_Issued WHERE \r\n                                SysDocID='" + sysDocID + "' AND VoucherID = '" + VoucherID + "' AND Status > 2 ";
				object obj = ExecuteScalar(exp, sqlTransaction);
				if (obj == null || obj.ToString() == "")
				{
					return false;
				}
				return int.Parse(obj.ToString()) > 0;
			}
			catch
			{
				throw;
			}
		}

		public bool TransactionHasProcessedReceivedCheques(string sysDocID, string VoucherID, SqlTransaction sqlTransaction)
		{
			try
			{
				string exp = "SELECT COUNT(*) FROM Cheque_Received WHERE \r\n                                SysDocID='" + sysDocID + "' AND VoucherID = '" + VoucherID + "' AND Status <>1";
				object obj = ExecuteScalar(exp, sqlTransaction);
				if (obj == null || obj.ToString() == "")
				{
					return false;
				}
				return int.Parse(obj.ToString()) > 0;
			}
			catch
			{
				throw;
			}
		}
	}
}
