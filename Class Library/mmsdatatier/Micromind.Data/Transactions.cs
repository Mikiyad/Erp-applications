using Micromind.Common;
using Micromind.Common.Data;
using Micromind.Common.Libraries;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace Micromind.Data
{
	public sealed class Transactions : StoreObject
	{
		private const string TRANSACTIONID_PARM = "@TransactionID";

		private const string SYSDOCID_PARM = "@SysDocID";

		private const string REGISTERID_PARM = "@RegisterID";

		private const string VOUCHERID_PARM = "@VoucherID";

		private const string AMOUNT_PARM = "@Amount";

		private const string AMOUNTFC_PARM = "@AmountFC";

		private const string ISPOS_PARM = "@IsPOS";

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

		private const string TAXGROUPID_PARM = "@TaxGroupID";

		private const string TAXAMOUNT_PARM = "@TaxAmount";

		private const string TAXOPTION_PARM = "@TaxOption";

		private const string POSSHIFTID_PARM = "@POSShiftID";

		private const string POSBATCHID_PARM = "@POSBatchID";

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

		private const string REFERENCEDATE_PARM = "@RefDate";

		private const string ATTRIBUTEID1_PARM = "@AttributeID1";

		private const string ATTRIBUTEID2_PARM = "@AttributeID2";

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

		private static object syncRoot = new object();

		public Transactions(Config config)
			: base(config)
		{
		}

		private string GetInsertUpdateTransactionText(bool isUpdate)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddInsertUpdateParameters("GL_Transaction", new FieldValue("TransactionID", "@TransactionID"), new FieldValue("SysDocID", "@SysDocID", isUpdateConditionField: true), new FieldValue("VoucherID", "@VoucherID", isUpdateConditionField: true), new FieldValue("RegisterID", "@RegisterID"), new FieldValue("CostCenterID", "@CostCenterID"), new FieldValue("PayeeType", "@PayeeType"), new FieldValue("PayeeID", "@PayeeID"), new FieldValue("TransactionDate", "@TransactionDate"), new FieldValue("IsPOS", "@IsPOS"), new FieldValue("POSShiftID", "@POSShiftID"), new FieldValue("POSBatchID", "@POSBatchID"), new FieldValue("Amount", "@Amount"), new FieldValue("AmountFC", "@AmountFC"), new FieldValue("ExpAmount", "@ExpAmount"), new FieldValue("ExpPercent", "@ExpPercent"), new FieldValue("ExpCode", "@ExpCode"), new FieldValue("CurrencyID", "@CurrencyID"), new FieldValue("CurrencyRate", "@CurrencyRate"), new FieldValue("Reference", "@Reference"), new FieldValue("ReasonID", "@ReasonID"), new FieldValue("Description", "@Description"), new FieldValue("TransferFromType", "@TransferFromType"), new FieldValue("TransferToType", "@TransferToType"), new FieldValue("FirstAccountID", "@FirstAccountID"), new FieldValue("SecondAccountID", "@SecondAccountID"), new FieldValue("SecondRegisterID", "@SecondRegisterID"), new FieldValue("ChequeID", "@ChequeID"), new FieldValue("RequestSysDocID", "@RequestSysDocID"), new FieldValue("RequestVoucherID", "@RequestVoucherID"), new FieldValue("TaxGroupID", "@TaxGroupID"), new FieldValue("TaxAmount", "@TaxAmount"), new FieldValue("ChequebookID", "@ChequebookID"), new FieldValue("CheckNumber", "@CheckNumber"), new FieldValue("CheckDate", "@CheckDate"), new FieldValue("CheckDeliveredDate", "@CheckDeliveredDate"), new FieldValue("EmployeeID", "@EmployeeID"), new FieldValue("TransactionStatus", "@TransactionStatus"), new FieldValue("IsSecondForm", "@IsSecondForm"), new FieldValue("AnalysisID", "@AnalysisID"));
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
			parameters.Add("@POSShiftID", SqlDbType.Int);
			parameters.Add("@POSBatchID", SqlDbType.Int);
			parameters.Add("@VoucherID", SqlDbType.NVarChar);
			parameters.Add("@IsPOS", SqlDbType.Bit);
			parameters.Add("@TransactionDate", SqlDbType.DateTime);
			parameters.Add("@Amount", SqlDbType.Money);
			parameters.Add("@AmountFC", SqlDbType.Money);
			parameters.Add("@ExpAmount", SqlDbType.Money);
			parameters.Add("@ExpPercent", SqlDbType.Decimal);
			parameters.Add("@ExpCode", SqlDbType.NVarChar);
			parameters.Add("@CurrencyID", SqlDbType.NVarChar);
			parameters.Add("@CurrencyRate", SqlDbType.Decimal);
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
			parameters.Add("@TaxGroupID", SqlDbType.NVarChar);
			parameters.Add("@TaxAmount", SqlDbType.Decimal);
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
			parameters["@IsPOS"].SourceColumn = "IsPOS";
			parameters["@POSShiftID"].SourceColumn = "POSShiftID";
			parameters["@POSBatchID"].SourceColumn = "POSBatchID";
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
			parameters["@TaxGroupID"].SourceColumn = "TaxGroupID";
			parameters["@TaxAmount"].SourceColumn = "TaxAmount";
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
			sqlBuilder.AddInsertUpdateParameters("Transaction_Details", new FieldValue("SysDocID", "@SysDocID"), new FieldValue("VoucherID", "@VoucherID"), new FieldValue("BankID", "@BankID"), new FieldValue("Description", "@Description"), new FieldValue("JobID", "@JobID"), new FieldValue("CostCategoryID", "@CostCategoryID"), new FieldValue("ConsignID", "@ConsignID"), new FieldValue("ConsignExpenseID", "@ConsignExpenseID"), new FieldValue("Reference", "@Reference"), new FieldValue("Amount", "@Amount"), new FieldValue("AmountFC", "@AmountFC"), new FieldValue("ExpAmount", "@ExpAmount"), new FieldValue("ExpPercent", "@ExpPercent"), new FieldValue("ExpCode", "@ExpCode"), new FieldValue("TaxOption", "@TaxOption"), new FieldValue("TaxAmount", "@TaxAmount"), new FieldValue("TaxGroupID", "@TaxGroupID"), new FieldValue("AccountID", "@AccountID"), new FieldValue("CostCenterID", "@CostCenterID"), new FieldValue("AnalysisID", "@AnalysisID"), new FieldValue("RowIndex", "@RowIndex"), new FieldValue("PayeeID", "@PayeeID"), new FieldValue("DueDate", "@DueDate"), new FieldValue("PaymentMethodID", "@PaymentMethodID"), new FieldValue("PaymentMethodType", "@PaymentMethodType"), new FieldValue("CheckNumber", "@CheckNumber"), new FieldValue("ChequebookID", "@ChequebookID"), new FieldValue("ChequeID", "@ChequeID"), new FieldValue("CheckDate", "@CheckDate"), new FieldValue("RefDate", "@RefDate"), new FieldValue("AttributeID1", "@AttributeID1"), new FieldValue("AttributeID2", "@AttributeID2"), new FieldValue("PayeeType", "@PayeeType"));
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
			parameters.Add("@RowIndex", SqlDbType.BigInt);
			parameters.Add("@PayeeID", SqlDbType.NVarChar);
			parameters.Add("@DueDate", SqlDbType.DateTime);
			parameters.Add("@PaymentMethodID", SqlDbType.NVarChar);
			parameters.Add("@PaymentMethodType", SqlDbType.TinyInt);
			parameters.Add("@CheckNumber", SqlDbType.NVarChar);
			parameters.Add("@ChequebookID", SqlDbType.NVarChar);
			parameters.Add("@CheckDate", SqlDbType.DateTime);
			parameters.Add("@RefDate", SqlDbType.DateTime);
			parameters.Add("@ChequeID", SqlDbType.Int);
			parameters.Add("@PayeeType", SqlDbType.NVarChar);
			parameters.Add("@TaxOption", SqlDbType.TinyInt);
			parameters.Add("@TaxGroupID", SqlDbType.VarChar);
			parameters.Add("@TaxAmount", SqlDbType.Decimal);
			parameters.Add("@AttributeID1", SqlDbType.VarChar);
			parameters.Add("@AttributeID2", SqlDbType.VarChar);
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
			parameters["@TaxOption"].SourceColumn = "TaxOption";
			parameters["@TaxAmount"].SourceColumn = "TaxAmount";
			parameters["@TaxGroupID"].SourceColumn = "TaxGroupID";
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
			parameters["@RefDate"].SourceColumn = "RefDate";
			parameters["@AttributeID1"].SourceColumn = "AttributeID1";
			parameters["@AttributeID2"].SourceColumn = "AttributeID2";
			if (isUpdate)
			{
				return updateCommand;
			}
			return insertCommand;
		}

		private string GetInsertUpdateTransactionBankFeesText(bool isUpdate)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddInsertUpdateParameters("Bank_Fee_Details", new FieldValue("GLTransactionSysDocID", "@GLTransactionSysDocID"), new FieldValue("GLTransactionVoucherID", "@GLTransactionVoucherID"), new FieldValue("RowIndex", "@RowIndex"), new FieldValue("BankFacilityID", "@BankFacilityID"), new FieldValue("ChequebookID", "@ChequebookID"), new FieldValue("Description", "@Description"), new FieldValue("Reference", "@Reference"), new FieldValue("BankAccountID", "@BankAccountID"), new FieldValue("ExpenseAccountID", "@ExpenseAccountID"), new FieldValue("BankFeeID", "@BankFeeID"), new FieldValue("Amount", "@Amount"), new FieldValue("AmountFC", "@AmountFC"), new FieldValue("TaxGroupID", "@TaxGroupID"), new FieldValue("TaxAmount", "@TaxAmount"), new FieldValue("TaxOption", "@TaxOption"), new FieldValue("CurrencyID", "@CurrencyID"), new FieldValue("CurrencyRate", "@CurrencyRate"));
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
			parameters.Add("@TaxOption", SqlDbType.TinyInt);
			parameters.Add("@TaxGroupID", SqlDbType.VarChar);
			parameters.Add("@TaxAmount", SqlDbType.Decimal);
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
			parameters["@TaxOption"].SourceColumn = "TaxOption";
			parameters["@TaxAmount"].SourceColumn = "TaxAmount";
			parameters["@TaxGroupID"].SourceColumn = "TaxGroupID";
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

		public bool InsertUpdateFundTransferTransaction(TransactionData transactionData, bool isUpdate)
		{
			bool flag = true;
			SqlCommand insertUpdateTransactionCommand = GetInsertUpdateTransactionCommand(isUpdate);
			try
			{
				int currencyDecimalPoints = new CompanyInformations(base.DBConfig).CurrencyDecimalPoints;
				DataRow dataRow = transactionData.TransactionTable.Rows[0];
				SqlTransaction sqlTransaction = base.DBConfig.StartNewTransaction();
				string text = dataRow["VoucherID"].ToString();
				string text2 = dataRow["SysDocID"].ToString();
				string registerID = dataRow["RegisterID"].ToString();
				string registerID2 = dataRow["SecondRegisterID"].ToString();
				string idFieldValue = dataRow["ChequebookID"].ToString();
				SysDocTypes sysDocTypes = (SysDocTypes)byte.Parse(dataRow["SysDocType"].ToString());
				if (!isUpdate && new SystemDocuments(base.DBConfig).ExistDocumentNumber("GL_Transaction", "VoucherID", dataRow["SysDocID"].ToString(), text, sqlTransaction))
				{
					throw new CompanyException("Document number already exist.", 1046);
				}
				string a = dataRow["TransferFromType"].ToString();
				string a2 = dataRow["TransferToType"].ToString();
				string text3 = dataRow["FirstAccountID"].ToString();
				string text4 = dataRow["SecondAccountID"].ToString();
				if (text3 != "" || text3 == "")
				{
					if (a == "R")
					{
						text3 = new Register(base.DBConfig).GetRegisterAccountID(registerID, "CashAccountID");
					}
					else if (a == "C")
					{
						text3 = new Databases(base.DBConfig).GetFieldValue("Chequebook", "AccountID", "ChequebookID", idFieldValue, sqlTransaction).ToString();
					}
					else
					{
						if (!(a == "A"))
						{
							throw new Exception("AccountID should be filled for transfer from.");
						}
						text3 = dataRow["FirstAccountID"].ToString();
					}
				}
				if (text4 != "" || text4 == "")
				{
					if (a2 == "R")
					{
						text4 = new Register(base.DBConfig).GetRegisterAccountID(registerID2, "CashAccountID");
					}
					else
					{
						if (!(a2 == "A"))
						{
							throw new Exception("AccountID should be filled for transfer to.");
						}
						text4 = dataRow["SecondAccountID"].ToString();
					}
				}
				dataRow["FirstAccountID"] = text3;
				dataRow["SecondAccountID"] = text4;
				if (dataRow["CurrencyID"] != DBNull.Value && dataRow["CurrencyID"].ToString() != GetBaseCurrencyID())
				{
					decimal d = decimal.Parse(dataRow["CurrencyRate"].ToString());
					decimal result = default(decimal);
					decimal.TryParse(dataRow["AmountFC"].ToString(), out result);
					if (new Currencies(base.DBConfig).GetCurrencyRateType(dataRow["CurrencyID"].ToString()) == "M")
					{
						dataRow["Amount"] = Math.Round(result * d, currencyDecimalPoints);
					}
					else
					{
						dataRow["Amount"] = Math.Round(result / d, currencyDecimalPoints);
					}
				}
				insertUpdateTransactionCommand.Transaction = sqlTransaction;
				flag = (isUpdate ? (flag & Update(transactionData, "GL_Transaction", insertUpdateTransactionCommand)) : (flag & Insert(transactionData, "GL_Transaction", insertUpdateTransactionCommand)));
				GLData journalData = CreateTransferTransactionGLData(transactionData);
				flag &= new Journal(base.DBConfig).InsertUpdateJournal(journalData, isUpdate, sqlTransaction);
				if (a == "C")
				{
					IssuedChequeData issuedChequeData = new IssuedChequeData();
					DataRow dataRow2 = issuedChequeData.IssuedChequeTable.NewRow();
					dataRow2["SysDocID"] = text2;
					dataRow2["VoucherID"] = text;
					dataRow2["PayeeType"] = "A";
					dataRow2["PayeeID"] = text4;
					dataRow2["ChequeNumber"] = dataRow["CheckNumber"];
					dataRow2["ChequebookID"] = dataRow["ChequebookID"];
					dataRow2["ChequeDate"] = dataRow["CheckDate"];
					dataRow2["Amount"] = dataRow["Amount"];
					dataRow2["AmountFC"] = dataRow["AmountFC"];
					dataRow2["ExchangeRate"] = dataRow["CurrencyRate"];
					dataRow2["Note"] = dataRow["Description"];
					dataRow2["IssueDate"] = dataRow["TransactionDate"];
					dataRow2["Reference"] = dataRow["Reference"];
					dataRow2["PDCAccountID"] = text3;
					dataRow2["Status"] = (byte)4;
					dataRow2["ClearanceDate"] = dataRow["CheckDate"];
					dataRow2.EndEdit();
					issuedChequeData.IssuedChequeTable.Rows.Add(dataRow2);
					flag &= new IssuedCheques(base.DBConfig).DeleteChequeRows(text2, text, sqlTransaction);
					if (issuedChequeData.IssuedChequeTable.Rows.Count > 0)
					{
						flag &= new IssuedCheques(base.DBConfig).InsertUpdateIssuedCheque(issuedChequeData, isUpdate: false, sqlTransaction);
					}
				}
				if (!flag)
				{
					throw new Exception("Failed to save the transaction because of an unexpected error. Please try again.");
				}
				flag &= UpdateTableRowInsertUpdateInfo("GL_Transaction", "SysDocID", dataRow["SysDocID"].ToString(), "VoucherID", dataRow["VoucherID"].ToString(), sqlTransaction, !isUpdate);
				flag = (isUpdate ? (flag & AddActivityLog("Fund Transfer", text, text2, ActivityTypes.Update, sqlTransaction)) : (flag & AddActivityLog("Fund Transfer", text, text2, ActivityTypes.Add, sqlTransaction)));
				if (!isUpdate)
				{
					flag &= new SystemDocuments(base.DBConfig).UpdateNextDocumentNumber(dataRow["SysDocID"].ToString(), dataRow["VoucherID"].ToString(), "GL_Transaction", "VoucherID", sqlTransaction);
				}
				if (!flag)
				{
					return flag;
				}
				if (sysDocTypes != SysDocTypes.FundTransfer)
				{
					return flag;
				}
				new Approval(base.DBConfig).CreateTransactionApprovalTasks(SysDocTypes.FundTransfer, text2, text, "GL_Transaction", sqlTransaction);
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

		public bool InsertUpdateCancelledReceivedCheque(TransactionData transactionData)
		{
			bool flag = true;
			bool flag2 = bool.Parse(new CompanyOption(base.DBConfig).GetCompanyOptionValue(CompanyOptionsEnum.PDCByMaturity, true).ToString());
			SqlCommand insertUpdateTransactionCommand = GetInsertUpdateTransactionCommand(isUpdate: false);
			bool flag3 = false;
			try
			{
				int currencyDecimalPoints = new CompanyInformations(base.DBConfig).CurrencyDecimalPoints;
				DataRow dataRow = transactionData.TransactionTable.Rows[0];
				string text = dataRow["ChequeID"].ToString();
				SqlTransaction sqlTransaction = base.DBConfig.StartNewTransaction();
				DataRow dataRow2 = new ReceivedCheques(base.DBConfig).GetChequeByID(text).Tables[0].Rows[0];
				string text2 = dataRow2["SysDocID"].ToString();
				string text3 = dataRow2["VoucherID"].ToString();
				if (dataRow2 == null)
				{
					throw new CompanyException("Cheque information not found.\nCheque ID:" + text);
				}
				switch (byte.Parse(dataRow2["Status"].ToString()))
				{
				case 9:
					throw new CompanyException("This cheque is already cancelled.", 1005);
				default:
					throw new CompanyException("Only cheques that are in 'Undeposited' or 'Returned' status can be cancelled.", 1004);
				case 1:
				case 8:
				{
					dataRow["PayeeID"] = dataRow2["PayeeID"];
					dataRow["PayeeType"] = dataRow2["PayeeType"];
					string text4 = dataRow["VoucherID"].ToString();
					string sysDocID = dataRow["SysDocID"].ToString();
					dataRow["RegisterID"].ToString();
					dataRow["SecondRegisterID"].ToString();
					dataRow["ChequebookID"].ToString();
					if (!flag3 && new SystemDocuments(base.DBConfig).ExistDocumentNumber("GL_Transaction", "VoucherID", dataRow["SysDocID"].ToString(), text4, sqlTransaction))
					{
						throw new CompanyException("Document number already exist.", 1046);
					}
					dataRow["CurrencyID"] = dataRow2["CurrencyID"];
					dataRow["CurrencyRate"] = dataRow2["ExchangeRate"];
					dataRow["AmountFC"] = dataRow2["AmountFC"];
					dataRow["Amount"] = dataRow2["Amount"];
					dataRow["CheckDate"] = dataRow2["ChequeDate"];
					dataRow["CheckNumber"] = dataRow2["ChequeNumber"];
					string text5 = dataRow2["PDCAccountID"].ToString();
					string text6 = dataRow2["PayeeAccountID"].ToString();
					if (!flag2 && (text6 == "" || text5 == ""))
					{
						throw new CompanyException("Either PDC account or payee account is not set for the cheque.", 999);
					}
					if (flag2)
					{
						dataRow["FirstAccountID"] = text6;
						dataRow["SecondAccountID"] = text6;
					}
					else
					{
						dataRow["FirstAccountID"] = text5;
						dataRow["SecondAccountID"] = text6;
					}
					if (dataRow["CurrencyID"] != DBNull.Value && dataRow["CurrencyID"].ToString() != GetBaseCurrencyID())
					{
						decimal d = decimal.Parse(dataRow["CurrencyRate"].ToString());
						decimal result = default(decimal);
						decimal.TryParse(dataRow["AmountFC"].ToString(), out result);
						if (new Currencies(base.DBConfig).GetCurrencyRateType(dataRow["CurrencyID"].ToString()) == "M")
						{
							dataRow["Amount"] = Math.Round(result * d, currencyDecimalPoints);
						}
						else
						{
							dataRow["Amount"] = Math.Round(result / d, currencyDecimalPoints);
						}
					}
					insertUpdateTransactionCommand.Transaction = sqlTransaction;
					flag = (flag3 ? (flag & Update(transactionData, "GL_Transaction", insertUpdateTransactionCommand)) : (flag & Insert(transactionData, "GL_Transaction", insertUpdateTransactionCommand)));
					insertUpdateTransactionCommand = GetInsertUpdateTransactionDetailsCommand(isUpdate: false);
					insertUpdateTransactionCommand.Transaction = sqlTransaction;
					if (flag3)
					{
						flag &= DeleteTransactionDetailsRows(sysDocID, text4, sqlTransaction);
					}
					if (transactionData.Tables["Transaction_Details"].Rows.Count > 0)
					{
						flag &= Insert(transactionData, "Transaction_Details", insertUpdateTransactionCommand);
					}
					if (!flag2)
					{
						GLData journalData = CreateCancelledReceivedChequeTransactionGLData(transactionData);
						flag &= new Journal(base.DBConfig).InsertUpdateJournal(journalData, flag3, sqlTransaction);
					}
					string exp = "UPDATE Cheque_Received SET Status= 9 WHERE ChequeID IN (" + text + ")";
					flag &= (ExecuteNonQuery(exp, sqlTransaction) > 0);
					if (flag2)
					{
						exp = "DELETE FROM AR_Payment_Allocation WHERE PaymentSysDocID = '" + text2 + "' AND PaymentVoucherID = '" + text3 + "'";
						flag &= (ExecuteNonQuery(exp, sqlTransaction) >= 0);
					}
					if (!flag)
					{
						throw new Exception("Failed to save the transaction because of an unexpected error. Please try again.");
					}
					flag &= UpdateTableRowInsertUpdateInfo("GL_Transaction", "SysDocID", dataRow["SysDocID"].ToString(), "VoucherID", dataRow["VoucherID"].ToString(), sqlTransaction, !flag3);
					flag = (flag3 ? (flag & AddActivityLog("Cancelled Cheque", text4, sysDocID, ActivityTypes.Update, sqlTransaction)) : (flag & AddActivityLog("Cancelled Cheque", text4, sysDocID, ActivityTypes.Add, sqlTransaction)));
					if (!flag3)
					{
						flag &= new SystemDocuments(base.DBConfig).UpdateNextDocumentNumber(dataRow["SysDocID"].ToString(), dataRow["VoucherID"].ToString(), "GL_Transaction", "VoucherID", sqlTransaction);
					}
					if (!flag)
					{
						return flag;
					}
					new Approval(base.DBConfig).CreateTransactionApprovalTasks(SysDocTypes.ReceivedChequeCancellation, sysDocID, text4, "GL_Transaction", sqlTransaction);
					return flag;
				}
				}
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

		public bool CancellIssuedCheque(TransactionData transactionData, bool isUpdate)
		{
			bool flag = true;
			SqlCommand insertUpdateTransactionCommand = GetInsertUpdateTransactionCommand(isUpdate);
			try
			{
				bool num = bool.Parse(new CompanyOption(base.DBConfig).GetCompanyOptionValue(CompanyOptionsEnum.PDCIssuedByMaturity, false).ToString());
				int currencyDecimalPoints = new CompanyInformations(base.DBConfig).CurrencyDecimalPoints;
				DataRow dataRow = transactionData.TransactionTable.Rows[0];
				string text = dataRow["ChequeID"].ToString();
				SqlTransaction sqlTransaction = base.DBConfig.StartNewTransaction();
				if (string.IsNullOrEmpty(new Databases(base.DBConfig).GetFieldValue("Cheque_Issued", "PayeeAccountID", "ChequeID", text, sqlTransaction).ToString()))
				{
					throw new CompanyException("PayeeAccountID not found.", 1021);
				}
				DataRow dataRow2 = new IssuedCheques(base.DBConfig).GetChequeByID(text).Tables[0].Rows[0];
				string text2 = dataRow2["SysDocID"].ToString();
				string text3 = dataRow2["VoucherID"].ToString();
				switch (byte.Parse(dataRow2["Status"].ToString()))
				{
				case 9:
					if (!isUpdate)
					{
						throw new CompanyException("This cheque is already cancelled.", 1005);
					}
					break;
				default:
					throw new CompanyException("Only cheques that are in 'Issued' or 'Voided' status can be cancelled.", 1004);
				case 2:
				case 5:
					break;
				}
				dataRow["PayeeID"] = dataRow2["PayeeID"];
				dataRow["PayeeType"] = dataRow2["PayeeType"];
				string text4 = dataRow["VoucherID"].ToString();
				string sysDocID = dataRow["SysDocID"].ToString();
				dataRow["RegisterID"].ToString();
				dataRow["SecondRegisterID"].ToString();
				dataRow["ChequebookID"].ToString();
				if (!isUpdate && new SystemDocuments(base.DBConfig).ExistDocumentNumber("GL_Transaction", "VoucherID", dataRow["SysDocID"].ToString(), text4, sqlTransaction))
				{
					throw new CompanyException("Document number already exist.", 1046);
				}
				dataRow["CurrencyID"] = dataRow2["CurrencyID"];
				dataRow["CurrencyRate"] = dataRow2["ExchangeRate"];
				dataRow["AmountFC"] = dataRow2["AmountFC"];
				dataRow["Amount"] = dataRow2["Amount"];
				string text5 = dataRow2["PDCAccountID"].ToString();
				string text6 = dataRow2["PayeeAccountID"].ToString();
				dataRow["FirstAccountID"] = text5;
				dataRow["SecondAccountID"] = text6;
				if (!num && (text6 == "" || text5 == ""))
				{
					throw new CompanyException("Either PDC account or payee account is not set for the cheque.", 999);
				}
				if (num)
				{
					dataRow["FirstAccountID"] = text6;
					dataRow["SecondAccountID"] = text6;
				}
				else
				{
					dataRow["FirstAccountID"] = text5;
					dataRow["SecondAccountID"] = text6;
				}
				if (dataRow["CurrencyID"] != DBNull.Value && dataRow["CurrencyID"].ToString() != GetBaseCurrencyID())
				{
					decimal d = decimal.Parse(dataRow["CurrencyRate"].ToString());
					decimal result = default(decimal);
					decimal.TryParse(dataRow["AmountFC"].ToString(), out result);
					if (new Currencies(base.DBConfig).GetCurrencyRateType(dataRow["CurrencyID"].ToString()) == "M")
					{
						dataRow["Amount"] = Math.Round(result * d, currencyDecimalPoints);
					}
					else
					{
						dataRow["Amount"] = Math.Round(result / d, currencyDecimalPoints);
					}
				}
				insertUpdateTransactionCommand.Transaction = sqlTransaction;
				flag = (isUpdate ? (flag & Update(transactionData, "GL_Transaction", insertUpdateTransactionCommand)) : (flag & Insert(transactionData, "GL_Transaction", insertUpdateTransactionCommand)));
				insertUpdateTransactionCommand = GetInsertUpdateTransactionDetailsCommand(isUpdate: false);
				insertUpdateTransactionCommand.Transaction = sqlTransaction;
				if (isUpdate)
				{
					flag &= DeleteTransactionDetailsRows(sysDocID, text4, sqlTransaction);
				}
				if (transactionData.Tables["Transaction_Details"].Rows.Count > 0)
				{
					flag &= Insert(transactionData, "Transaction_Details", insertUpdateTransactionCommand);
				}
				if (!num)
				{
					GLData journalData = CreateCancelledIssuedChequeTransactionGLData(transactionData);
					flag &= new Journal(base.DBConfig).InsertUpdateJournal(journalData, isUpdate, sqlTransaction);
				}
				string exp = "UPDATE Cheque_Issued SET Status= 9 WHERE ChequeID IN (" + text + ")";
				flag &= (ExecuteNonQuery(exp, sqlTransaction) > 0);
				exp = "DELETE FROM AP_Payment_Allocation WHERE PaymentSysDocID = '" + text2 + "' AND PaymentVoucherID = '" + text3 + "'";
				flag &= (ExecuteNonQuery(exp, sqlTransaction) >= 0);
				if (!flag)
				{
					throw new Exception("Failed to save the transaction because of an unexpected error. Please try again.");
				}
				flag &= UpdateTableRowInsertUpdateInfo("GL_Transaction", "SysDocID", dataRow["SysDocID"].ToString(), "VoucherID", dataRow["VoucherID"].ToString(), sqlTransaction, !isUpdate);
				flag = (isUpdate ? (flag & AddActivityLog("Cancelled Cheque", text4, ActivityTypes.Update, sqlTransaction)) : (flag & AddActivityLog("Cancelled Cheque", text4, ActivityTypes.Add, sqlTransaction)));
				if (!isUpdate)
				{
					flag &= new SystemDocuments(base.DBConfig).UpdateNextDocumentNumber(dataRow["SysDocID"].ToString(), dataRow["VoucherID"].ToString(), "GL_Transaction", "VoucherID", sqlTransaction);
				}
				if (!flag)
				{
					return flag;
				}
				new Approval(base.DBConfig).CreateTransactionApprovalTasks(SysDocTypes.IssuedChequeCancellation, sysDocID, text4, "GL_Transaction", sqlTransaction);
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

		public bool InsertUpdateReturnedCheque(TransactionData transactionData)
		{
			bool flag = true;
			bool flag2 = bool.Parse(new CompanyOption(base.DBConfig).GetCompanyOptionValue(CompanyOptionsEnum.PDCByMaturity, true).ToString());
			SqlCommand insertUpdateTransactionCommand = GetInsertUpdateTransactionCommand(isUpdate: false);
			bool flag3 = false;
			try
			{
				int currencyDecimalPoints = new CompanyInformations(base.DBConfig).CurrencyDecimalPoints;
				bool flag4 = bool.Parse(new CompanyOption(base.DBConfig).GetCompanyOptionValue(CompanyOptionsEnum.DirectChequeReturn, false).ToString());
				DataRow dataRow = transactionData.TransactionTable.Rows[0];
				string text = dataRow["ChequeID"].ToString();
				SqlTransaction sqlTransaction = base.DBConfig.StartNewTransaction();
				DataRow dataRow2 = new ReceivedCheques(base.DBConfig).GetChequeByID(text).Tables[0].Rows[0];
				dataRow2["PayeeID"].ToString();
				ReceivedChequeStatus receivedChequeStatus = (ReceivedChequeStatus)byte.Parse(dataRow2["Status"].ToString());
				if (receivedChequeStatus != ReceivedChequeStatus.Deposited && receivedChequeStatus != ReceivedChequeStatus.SentToBank && flag4)
				{
					throw new CompanyException("Only cheques that are in deposited status can be returned.", 1003);
				}
				if (dataRow2 == null)
				{
					throw new CompanyException("Cheque information not found.\nCheque ID:" + text);
				}
				string text2 = dataRow["VoucherID"].ToString();
				string sysDocID = dataRow["SysDocID"].ToString();
				dataRow["RegisterID"].ToString();
				dataRow["SecondRegisterID"].ToString();
				dataRow["ChequebookID"].ToString();
				if (!flag3 && new SystemDocuments(base.DBConfig).ExistDocumentNumber("GL_Transaction", "VoucherID", dataRow["SysDocID"].ToString(), text2, sqlTransaction))
				{
					throw new CompanyException("Document number already exist.", 1046);
				}
				dataRow["CurrencyID"] = dataRow2["CurrencyID"];
				dataRow["CurrencyRate"] = dataRow2["ExchangeRate"];
				dataRow["AmountFC"] = dataRow2["AmountFC"];
				dataRow["Amount"] = dataRow2["Amount"];
				dataRow["PayeeType"] = dataRow2["PayeeType"];
				dataRow["PayeeID"] = dataRow2["PayeeID"];
				dataRow["CheckDate"] = dataRow2["ChequeDate"];
				dataRow["CheckNumber"] = dataRow2["ChequeNumber"];
				string text3 = dataRow2["DepositAccountID"].ToString();
				if (receivedChequeStatus == ReceivedChequeStatus.SentToBank && !flag4)
				{
					text3 = null;
				}
				string text4 = (!flag2) ? dataRow2["PDCAccountID"].ToString() : dataRow2["PayeeAccountID"].ToString();
				if (text4 == "" || text3 == "")
				{
					throw new Exception("Either first account or second account is empty.");
				}
				dataRow["FirstAccountID"] = text3;
				dataRow["SecondAccountID"] = text4;
				if (dataRow["CurrencyID"] != DBNull.Value && dataRow["CurrencyID"].ToString() != GetBaseCurrencyID())
				{
					decimal d = decimal.Parse(dataRow["CurrencyRate"].ToString());
					decimal result = default(decimal);
					decimal.TryParse(dataRow["AmountFC"].ToString(), out result);
					if (new Currencies(base.DBConfig).GetCurrencyRateType(dataRow["CurrencyID"].ToString()) == "M")
					{
						dataRow["Amount"] = Math.Round(result * d, currencyDecimalPoints);
					}
					else
					{
						dataRow["Amount"] = Math.Round(result * d, currencyDecimalPoints);
					}
				}
				insertUpdateTransactionCommand.Transaction = sqlTransaction;
				flag = (flag3 ? (flag & Update(transactionData, "GL_Transaction", insertUpdateTransactionCommand)) : (flag & Insert(transactionData, "GL_Transaction", insertUpdateTransactionCommand)));
				insertUpdateTransactionCommand = GetInsertUpdateTransactionDetailsCommand(isUpdate: false);
				insertUpdateTransactionCommand.Transaction = sqlTransaction;
				if (flag3)
				{
					flag &= DeleteTransactionDetailsRows(sysDocID, text2, sqlTransaction);
				}
				if (transactionData.Tables["Transaction_Details"].Rows.Count > 0)
				{
					flag &= Insert(transactionData, "Transaction_Details", insertUpdateTransactionCommand);
				}
				if (receivedChequeStatus != ReceivedChequeStatus.SentToBank)
				{
					GLData journalData = CreateReceivedReturnedChequeTransactionGLData(transactionData);
					flag &= new Journal(base.DBConfig).InsertUpdateJournal(journalData, flag3, sqlTransaction);
				}
				if (receivedChequeStatus != ReceivedChequeStatus.SentToBank)
				{
					string exp = "UPDATE Cheque_Received SET Status= 8 WHERE ChequeID IN (" + text + ")";
					flag &= (ExecuteNonQuery(exp, sqlTransaction) > 0);
				}
				if (receivedChequeStatus == ReceivedChequeStatus.SentToBank)
				{
					string exp2 = "UPDATE Cheque_Send_Detail SET Status= 1 WHERE ChequeID IN (" + text + ") ";
					flag &= (ExecuteNonQuery(exp2, sqlTransaction) > 0);
				}
				if (!flag)
				{
					throw new Exception("Failed to save the transaction because of an unexpected error. Please try again.");
				}
				flag &= UpdateTableRowInsertUpdateInfo("GL_Transaction", "SysDocID", dataRow["SysDocID"].ToString(), "VoucherID", dataRow["VoucherID"].ToString(), sqlTransaction, !flag3);
				flag = (flag3 ? (flag & AddActivityLog("Returned Cheque", text2, sysDocID, ActivityTypes.Update, sqlTransaction)) : (flag & AddActivityLog("Returned Cheque", text2, sysDocID, ActivityTypes.Add, sqlTransaction)));
				if (flag3)
				{
					return flag;
				}
				flag &= new SystemDocuments(base.DBConfig).UpdateNextDocumentNumber(dataRow["SysDocID"].ToString(), dataRow["VoucherID"].ToString(), "GL_Transaction", "VoucherID", sqlTransaction);
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

		public bool InsertUpdateIssuedReturnedCheque(TransactionData transactionData, bool isUpdate)
		{
			bool flag = true;
			SqlCommand insertUpdateTransactionCommand = GetInsertUpdateTransactionCommand(isUpdate);
			bool flag2 = bool.Parse(new CompanyOption(base.DBConfig).GetCompanyOptionValue(CompanyOptionsEnum.PDCIssuedByMaturity, false).ToString());
			try
			{
				int currencyDecimalPoints = new CompanyInformations(base.DBConfig).CurrencyDecimalPoints;
				DataRow dataRow = transactionData.TransactionTable.Rows[0];
				string text = dataRow["ChequeID"].ToString();
				SqlTransaction sqlTransaction = base.DBConfig.StartNewTransaction();
				DataRow dataRow2 = new IssuedCheques(base.DBConfig).GetChequeByID(text).Tables[0].Rows[0];
				IssuedChequeStatus issuedChequeStatus = (IssuedChequeStatus)byte.Parse(dataRow2["Status"].ToString());
				if (!isUpdate && issuedChequeStatus != IssuedChequeStatus.Cleared)
				{
					throw new CompanyException("Only cheques that are in 'Cleared' status can be returned.", 1009);
				}
				if (isUpdate && issuedChequeStatus != IssuedChequeStatus.Bounced)
				{
					throw new CompanyException("Cannot update this transaction because the cheque is modified by another transaction.", 1058);
				}
				string text2 = dataRow["VoucherID"].ToString();
				string text3 = dataRow["SysDocID"].ToString();
				dataRow["RegisterID"].ToString();
				dataRow["SecondRegisterID"].ToString();
				dataRow["ChequebookID"].ToString();
				if (!isUpdate && new SystemDocuments(base.DBConfig).ExistDocumentNumber("GL_Transaction", "VoucherID", dataRow["SysDocID"].ToString(), text2, sqlTransaction))
				{
					throw new CompanyException("Document number already exist.", 1046);
				}
				dataRow["CurrencyID"] = dataRow2["CurrencyID"];
				dataRow["CurrencyRate"] = dataRow2["ExchangeRate"];
				dataRow["AmountFC"] = dataRow2["AmountFC"];
				dataRow["Amount"] = dataRow2["Amount"];
				dataRow["PayeeType"] = dataRow2["PayeeType"];
				dataRow["PayeeID"] = dataRow2["PayeeID"];
				string idFieldValue = dataRow2["ChequeNumber"].ToString();
				object fieldValue = new Databases(base.DBConfig).GetFieldValue("Cheque_Issued", "SysDocID", "ChequeNumber", idFieldValue, sqlTransaction);
				_ = string.Empty;
				if (fieldValue != null)
				{
					text3 = fieldValue.ToString();
				}
				int num = -1;
				object fieldValue2 = new Databases(base.DBConfig).GetFieldValue("System_Document", "SysDocType", "SysDocID", text3, sqlTransaction);
				if (fieldValue2 != null)
				{
					num = int.Parse(fieldValue2.ToString());
				}
				string text4 = dataRow2["BankAccountID"].ToString();
				string text5 = (!flag2) ? dataRow2["PDCAccountID"].ToString() : dataRow2["PayeeAccountID"].ToString();
				if (num == 6 && num != -1)
				{
					text5 = dataRow2["PayeeAccountID"].ToString();
				}
				if (text5 == "" || text4 == "")
				{
					throw new Exception("Either first account or second account is empty.");
				}
				dataRow["FirstAccountID"] = text4;
				dataRow["SecondAccountID"] = text5;
				if (dataRow["CurrencyID"] != DBNull.Value && dataRow["CurrencyID"].ToString() != GetBaseCurrencyID())
				{
					decimal d = decimal.Parse(dataRow["CurrencyRate"].ToString());
					decimal result = default(decimal);
					decimal.TryParse(dataRow["AmountFC"].ToString(), out result);
					if (new Currencies(base.DBConfig).GetCurrencyRateType(dataRow["CurrencyID"].ToString()) == "M")
					{
						dataRow["Amount"] = Math.Round(result * d, currencyDecimalPoints);
					}
					else
					{
						dataRow["Amount"] = Math.Round(result / d, currencyDecimalPoints);
					}
				}
				insertUpdateTransactionCommand.Transaction = sqlTransaction;
				flag = (isUpdate ? (flag & Update(transactionData, "GL_Transaction", insertUpdateTransactionCommand)) : (flag & Insert(transactionData, "GL_Transaction", insertUpdateTransactionCommand)));
				insertUpdateTransactionCommand = GetInsertUpdateTransactionDetailsCommand(isUpdate: false);
				insertUpdateTransactionCommand.Transaction = sqlTransaction;
				if (isUpdate)
				{
					flag &= DeleteTransactionDetailsRows(text3, text2, sqlTransaction);
				}
				if (transactionData.Tables["Transaction_Details"].Rows.Count > 0)
				{
					flag &= Insert(transactionData, "Transaction_Details", insertUpdateTransactionCommand);
				}
				GLData journalData = CreateIssuedReturnedChequeTransactionGLData(transactionData);
				flag &= new Journal(base.DBConfig).InsertUpdateJournal(journalData, isUpdate, sqlTransaction);
				string exp = "UPDATE Cheque_Issued SET Status= 5 WHERE ChequeID IN (" + text + ")";
				flag &= (ExecuteNonQuery(exp, sqlTransaction) > 0);
				if (!flag)
				{
					throw new Exception("Failed to save the transaction because of an unexpected error. Please try again.");
				}
				flag &= UpdateTableRowInsertUpdateInfo("GL_Transaction", "SysDocID", dataRow["SysDocID"].ToString(), "VoucherID", dataRow["VoucherID"].ToString(), sqlTransaction, !isUpdate);
				flag = (isUpdate ? (flag & AddActivityLog("Returned Cheque", text2, ActivityTypes.Update, sqlTransaction)) : (flag & AddActivityLog("Returned Cheque", text2, ActivityTypes.Add, sqlTransaction)));
				if (!isUpdate)
				{
					flag &= new SystemDocuments(base.DBConfig).UpdateNextDocumentNumber(dataRow["SysDocID"].ToString(), dataRow["VoucherID"].ToString(), "GL_Transaction", "VoucherID", sqlTransaction);
				}
				if (!flag)
				{
					return flag;
				}
				new Approval(base.DBConfig).CreateTransactionApprovalTasks(SysDocTypes.IssuedChequeReturn, text3, text2, "GL_Transaction", sqlTransaction);
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

		public bool InsertUpdateChequeChangeStatus(TransactionData transactionData)
		{
			bool flag = true;
			try
			{
				DataRow dataRow = transactionData.TransactionTable.Rows[0];
				string text = dataRow["ChequeID"].ToString();
				SqlTransaction sqlTransaction = base.DBConfig.StartNewTransaction();
				DataRow dataRow2 = new ReceivedCheques(base.DBConfig).GetChequeByID(text).Tables[0].Rows[0];
				ReceivedChequeStatus receivedChequeStatus = (ReceivedChequeStatus)byte.Parse(dataRow2["Status"].ToString());
				string text2 = dataRow["ChangeStatus"].ToString();
				if (dataRow2 == null)
				{
					throw new CompanyException("Cheque information not found.\nCheque ID:" + text);
				}
				string entiyID = dataRow2["ChequeNumber"].ToString();
				string exp = "UPDATE Cheque_Received SET Status= '" + text2 + "',SendDate= NULL,SendBankAccountID = NULL, SendReference = NULL WHERE ChequeID IN (" + text + ")";
				flag &= (ExecuteNonQuery(exp, sqlTransaction) > 0);
				if (receivedChequeStatus == ReceivedChequeStatus.SentToBank)
				{
					string exp2 = "UPDATE Cheque_Send_Detail SET Status= 1 WHERE ChequeID IN (" + text + ") ";
					flag &= (ExecuteNonQuery(exp2, sqlTransaction) > 0);
				}
				if (!flag)
				{
					throw new Exception("Failed to save the transaction because of an unexpected error. Please try again.");
				}
				flag &= AddActivityLog("Status Changed for Chq. No:", entiyID, ActivityTypes.Update, sqlTransaction);
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

		public bool DepositCheques(TransactionData transactionData, int[] chequeIDs, bool isUpdate)
		{
			bool flag = true;
			bool.Parse(new CompanyOption(base.DBConfig).GetCompanyOptionValue(CompanyOptionsEnum.PDCByMaturity, true).ToString());
			SqlCommand insertUpdateTransactionCommand = GetInsertUpdateTransactionCommand(isUpdate);
			try
			{
				int currencyDecimalPoints = new CompanyInformations(base.DBConfig).CurrencyDecimalPoints;
				DataRow dataRow = transactionData.TransactionTable.Rows[0];
				SqlTransaction sqlTransaction = base.DBConfig.StartNewTransaction();
				string text = "";
				for (int i = 0; i < chequeIDs.Length; i++)
				{
					if (text != "")
					{
						text += ",";
					}
					text = text + "'" + chequeIDs[i].ToString() + "'";
				}
				string text2 = "";
				if (!isUpdate)
				{
					text2 = "SELECT COUNT(ChequeID) FROM Cheque_Received WHERE ChequeID IN (" + text + ") AND Status NOT IN (1,3,4,8)";
					object obj = ExecuteScalar(text2);
					if (obj != null && int.Parse(obj.ToString()) > 0)
					{
						throw new CompanyException("Only 'Undeposited' , 'Bounced' or 'Discounted' cheques can be matured. One or more cheques may already being deposited or in another status.", 1008);
					}
				}
				else
				{
					text2 = "SELECT COUNT(ChequeID) FROM Cheque_Received WHERE ChequeID IN (" + text + ") AND Status IN (3,8,9)";
					object obj2 = ExecuteScalar(text2);
					if (obj2 != null && int.Parse(obj2.ToString()) > 0)
					{
						throw new CompanyException("There are cheques that are bounced or cancelled. Transaction cannot be modifed", 1008);
					}
				}
				string text3 = dataRow["VoucherID"].ToString();
				string sysDocID = dataRow["SysDocID"].ToString();
				string text4 = "";
				object obj3 = new Databases(base.DBConfig).GetFieldValue("Account", "BankID", "AccountID", dataRow["FirstAccountID"].ToString(), sqlTransaction).ToString();
				if (obj3 != null)
				{
					text4 = obj3.ToString();
				}
				if (!isUpdate && new SystemDocuments(base.DBConfig).ExistDocumentNumber("GL_Transaction", "VoucherID", dataRow["SysDocID"].ToString(), text3, sqlTransaction))
				{
					throw new CompanyException("Document number already exist.", 1046);
				}
				if (dataRow["CurrencyID"] != DBNull.Value && dataRow["CurrencyID"].ToString() != GetBaseCurrencyID())
				{
					decimal d = decimal.Parse(dataRow["CurrencyRate"].ToString());
					decimal result = default(decimal);
					decimal.TryParse(dataRow["AmountFC"].ToString(), out result);
					if (new Currencies(base.DBConfig).GetCurrencyRateType(dataRow["CurrencyID"].ToString()) == "M")
					{
						dataRow["Amount"] = Math.Round(result * d, currencyDecimalPoints);
					}
					else
					{
						dataRow["Amount"] = Math.Round(result / d, currencyDecimalPoints);
					}
				}
				insertUpdateTransactionCommand.Transaction = sqlTransaction;
				flag = (isUpdate ? (flag & Update(transactionData, "GL_Transaction", insertUpdateTransactionCommand)) : (flag & Insert(transactionData, "GL_Transaction", insertUpdateTransactionCommand)));
				insertUpdateTransactionCommand = GetInsertUpdateTransactionDetailsCommand(isUpdate: false);
				insertUpdateTransactionCommand.Transaction = sqlTransaction;
				if (isUpdate)
				{
					flag &= new ReceivedCheques(base.DBConfig).UpdateChequeStatus(sysDocID, text3, ReceivedChequeStatus.Undeposited, sqlTransaction);
					flag &= DeleteTransactionDetailsRows(sysDocID, text3, sqlTransaction);
				}
				if (transactionData.Tables["Transaction_Details"].Rows.Count > 0)
				{
					flag &= Insert(transactionData, "Transaction_Details", insertUpdateTransactionCommand);
				}
				GLData journalData = CreateChequeDepositGLData(transactionData);
				flag &= new Journal(base.DBConfig).InsertUpdateJournal(journalData, isUpdate, sqlTransaction);
				text2 = "UPDATE Cheque_Received SET DepositDate='" + CommonLib.ToSqlDateTimeString(DateTime.Parse(dataRow["TransactionDate"].ToString())) + "',DepositAccountID='" + dataRow["FirstAccountID"].ToString() + "',DepositBankID= '" + text4 + "',DepositVoucherID = '" + dataRow["VoucherID"].ToString() + "',DepositSysDocID = '" + dataRow["SysDocID"].ToString() + "' ,Status= 2 WHERE ChequeID IN (" + text + ")";
				flag &= (ExecuteNonQuery(text2, sqlTransaction) >= chequeIDs.Length);
				if (!flag)
				{
					throw new Exception("Failed to save the transaction because of an unexpected error. Please try again.");
				}
				flag &= UpdateTableRowInsertUpdateInfo("GL_Transaction", "SysDocID", dataRow["SysDocID"].ToString(), "VoucherID", dataRow["VoucherID"].ToString(), sqlTransaction, !isUpdate);
				flag = (isUpdate ? (flag & AddActivityLog("Cheque Maturity", text3, sysDocID, ActivityTypes.Update, sqlTransaction)) : (flag & AddActivityLog("Cheque Maturity", text3, sysDocID, ActivityTypes.Add, sqlTransaction)));
				if (!isUpdate)
				{
					flag &= new SystemDocuments(base.DBConfig).UpdateNextDocumentNumber(dataRow["SysDocID"].ToString(), dataRow["VoucherID"].ToString(), "GL_Transaction", "VoucherID", sqlTransaction);
				}
				if (!flag)
				{
					return flag;
				}
				new Approval(base.DBConfig).CreateTransactionApprovalTasks(SysDocTypes.ChequeDeposit, sysDocID, text3, "GL_Transaction", sqlTransaction);
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

		public bool ClearIssuedCheques(TransactionData transactionData, int[] chequeIDs)
		{
			bool flag = true;
			SqlCommand insertUpdateTransactionCommand = GetInsertUpdateTransactionCommand(isUpdate: false);
			bool flag2 = false;
			try
			{
				bool.Parse(new CompanyOption(base.DBConfig).GetCompanyOptionValue(CompanyOptionsEnum.PDCIssuedByMaturity, false).ToString());
				int currencyDecimalPoints = new CompanyInformations(base.DBConfig).CurrencyDecimalPoints;
				DataRow dataRow = transactionData.TransactionTable.Rows[0];
				SqlTransaction sqlTransaction = base.DBConfig.StartNewTransaction();
				string text = "";
				for (int i = 0; i < chequeIDs.Length; i++)
				{
					if (text != "")
					{
						text += ",";
					}
					text = text + "'" + chequeIDs[i].ToString() + "'";
				}
				string exp = "SELECT COUNT(ChequeID) FROM Cheque_Issued WHERE ChequeID IN (" + text + ") AND Status NOT IN (2,5)";
				object obj = ExecuteScalar(exp);
				if (obj != null && int.Parse(obj.ToString()) > 0)
				{
					throw new CompanyException("Only cheques that are in issued status can be cleared. One or more chequs may already cleared or in another status.", 1007);
				}
				DataSet issuedChequeList = new IssuedCheques(base.DBConfig).GetIssuedChequeList(chequeIDs, sqlTransaction);
				transactionData.TransactionDetailsTable.Rows.Clear();
				foreach (DataRow row in issuedChequeList.Tables[0].Rows)
				{
					DataRow dataRow3 = transactionData.TransactionDetailsTable.NewRow();
					dataRow3["AccountID"] = row["PDCAccountID"];
					dataRow3["Amount"] = row["Amount"];
					dataRow3["AmountFC"] = row["AmountFC"];
					dataRow3["SysDocID"] = dataRow["SysDocID"];
					dataRow3["VoucherID"] = dataRow["VoucherID"];
					dataRow3["ChequebookID"] = row["ChequebookID"];
					dataRow3["CheckNumber"] = row["ChequeNumber"];
					dataRow3["CheckDate"] = row["ChequeDate"];
					dataRow3["ChequeID"] = row["ChequeID"];
					transactionData.TransactionDetailsTable.Rows.Add(dataRow3);
				}
				string text2 = dataRow["VoucherID"].ToString();
				string sysDocID = dataRow["SysDocID"].ToString();
				if (!flag2 && new SystemDocuments(base.DBConfig).ExistDocumentNumber("GL_Transaction", "VoucherID", dataRow["SysDocID"].ToString(), text2, sqlTransaction))
				{
					throw new CompanyException("Document number already exist.", 1046);
				}
				if (dataRow["CurrencyID"] != DBNull.Value && dataRow["CurrencyID"].ToString() != GetBaseCurrencyID())
				{
					decimal d = decimal.Parse(dataRow["CurrencyRate"].ToString());
					decimal result = default(decimal);
					decimal.TryParse(dataRow["AmountFC"].ToString(), out result);
					if (new Currencies(base.DBConfig).GetCurrencyRateType(dataRow["CurrencyID"].ToString()) == "M")
					{
						dataRow["Amount"] = Math.Round(result * d, currencyDecimalPoints);
					}
					else
					{
						dataRow["Amount"] = Math.Round(result / d, currencyDecimalPoints);
					}
				}
				insertUpdateTransactionCommand.Transaction = sqlTransaction;
				flag = (flag2 ? (flag & Update(transactionData, "GL_Transaction", insertUpdateTransactionCommand)) : (flag & Insert(transactionData, "GL_Transaction", insertUpdateTransactionCommand)));
				insertUpdateTransactionCommand = GetInsertUpdateTransactionDetailsCommand(isUpdate: false);
				insertUpdateTransactionCommand.Transaction = sqlTransaction;
				if (flag2)
				{
					flag &= DeleteTransactionDetailsRows(sysDocID, text2, sqlTransaction);
				}
				if (transactionData.Tables["Transaction_Details"].Rows.Count > 0)
				{
					flag &= Insert(transactionData, "Transaction_Details", insertUpdateTransactionCommand);
				}
				GLData journalData = CreateIssuedChequeClearanceGLData(transactionData, issuedChequeList, sqlTransaction);
				flag &= new Journal(base.DBConfig).InsertUpdateJournal(journalData, flag2, sqlTransaction);
				exp = "UPDATE Cheque_Issued SET ClearanceDate='" + CommonLib.ToSqlDateTimeString(DateTime.Parse(dataRow["TransactionDate"].ToString())) + "',ClearanceVoucherID = '" + dataRow["VoucherID"].ToString() + "',ClearanceSysDocID = '" + dataRow["SysDocID"].ToString() + "', Status= 4 WHERE ChequeID IN (" + text + ")";
				flag &= (ExecuteNonQuery(exp, sqlTransaction) > 0);
				if (!flag)
				{
					throw new Exception("Failed to save the transaction because of an unexpected error. Please try again.");
				}
				flag &= UpdateTableRowInsertUpdateInfo("GL_Transaction", "SysDocID", dataRow["SysDocID"].ToString(), "VoucherID", dataRow["VoucherID"].ToString(), sqlTransaction, !flag2);
				flag = (flag2 ? (flag & AddActivityLog("Issued Cheque Clearance", text2, ActivityTypes.Update, sqlTransaction)) : (flag & AddActivityLog("Issued Cheque Clearance", text2, ActivityTypes.Add, sqlTransaction)));
				if (!flag2)
				{
					flag &= new SystemDocuments(base.DBConfig).UpdateNextDocumentNumber(dataRow["SysDocID"].ToString(), dataRow["VoucherID"].ToString(), "GL_Transaction", "VoucherID", sqlTransaction);
				}
				if (!flag)
				{
					return flag;
				}
				new Approval(base.DBConfig).CreateTransactionApprovalTasks(SysDocTypes.IssuedChequeClearance, sysDocID, text2, "GL_Transaction", sqlTransaction);
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

		public bool InsertUpdateDebitCreditNoteTransaction(TransactionData transactionData, bool isUpdate)
		{
			bool flag = true;
			SqlCommand insertUpdateTransactionCommand = GetInsertUpdateTransactionCommand(isUpdate);
			try
			{
				int currencyDecimalPoints = new CompanyInformations(base.DBConfig).CurrencyDecimalPoints;
				DataRow dataRow = transactionData.TransactionTable.Rows[0];
				SysDocTypes sysDocTypes = (SysDocTypes)byte.Parse(dataRow["SysDocType"].ToString());
				SqlTransaction sqlTransaction = base.DBConfig.StartNewTransaction();
				string text = dataRow["VoucherID"].ToString();
				string sysDocID = dataRow["SysDocID"].ToString();
				if (!isUpdate && new SystemDocuments(base.DBConfig).ExistDocumentNumber("GL_Transaction", "VoucherID", dataRow["SysDocID"].ToString(), text, sqlTransaction))
				{
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
				foreach (DataRow row in transactionData.TransactionDetailsTable.Rows)
				{
					row["SysDocID"] = dataRow["SysDocID"];
					row["VoucherID"] = dataRow["VoucherID"];
				}
				if (dataRow["CurrencyID"] != DBNull.Value && dataRow["CurrencyID"].ToString() != GetBaseCurrencyID())
				{
					decimal d = decimal.Parse(dataRow["CurrencyRate"].ToString());
					decimal num = default(decimal);
					foreach (DataRow row2 in transactionData.TransactionDetailsTable.Rows)
					{
						decimal result = default(decimal);
						decimal.TryParse(row2["AmountFC"].ToString(), out result);
						string currencyRateType = new Currencies(base.DBConfig).GetCurrencyRateType(dataRow["CurrencyID"].ToString());
						if (currencyRateType == "M")
						{
							row2["Amount"] = Math.Round(result * d, currencyDecimalPoints);
						}
						else
						{
							row2["Amount"] = Math.Round(result / d, currencyDecimalPoints);
						}
						if (currencyRateType == "M")
						{
							num += Math.Round(result * d, currencyDecimalPoints);
						}
						else
						{
							num += Math.Round(result / d, currencyDecimalPoints);
						}
					}
					dataRow["Amount"] = num;
				}
				insertUpdateTransactionCommand.Transaction = sqlTransaction;
				flag = (isUpdate ? (flag & Update(transactionData, "GL_Transaction", insertUpdateTransactionCommand)) : (flag & Insert(transactionData, "GL_Transaction", insertUpdateTransactionCommand)));
				insertUpdateTransactionCommand = GetInsertUpdateTransactionDetailsCommand(isUpdate: false);
				insertUpdateTransactionCommand.Transaction = sqlTransaction;
				if (isUpdate)
				{
					flag &= DeleteTransactionDetailsRows(sysDocID, text, sqlTransaction);
				}
				if (transactionData.Tables["Transaction_Details"].Rows.Count > 0)
				{
					flag &= Insert(transactionData, "Transaction_Details", insertUpdateTransactionCommand);
				}
				if (transactionData.Tables.Contains("Tax_Detail") && transactionData.Tables["Tax_Detail"].Rows.Count > 0)
				{
					flag &= new TaxTransaction(base.DBConfig).InsertUpdateTaxTransaction(transactionData, sysDocID, text, isUpdate, sqlTransaction);
				}
				GLData journalData = CreateDebitCreditNoteGLData(transactionData);
				flag &= new Journal(base.DBConfig).InsertUpdateJournal(journalData, isUpdate, sqlTransaction);
				if (!flag)
				{
					throw new Exception("Failed to save the transaction because of an unexpected error. Please try again.");
				}
				flag &= UpdateTableRowInsertUpdateInfo("GL_Transaction", "SysDocID", dataRow["SysDocID"].ToString(), "VoucherID", dataRow["VoucherID"].ToString(), sqlTransaction, !isUpdate);
				string entityName = "Tranaction";
				switch (sysDocTypes)
				{
				case SysDocTypes.CreditNote:
					entityName = "Credit Note";
					break;
				case SysDocTypes.DebitNote:
					entityName = "Debit Note";
					break;
				}
				flag = (isUpdate ? (flag & AddActivityLog(entityName, text, sysDocID, ActivityTypes.Update, sqlTransaction)) : (flag & AddActivityLog(entityName, text, sysDocID, ActivityTypes.Add, sqlTransaction)));
				if (isUpdate)
				{
					return flag;
				}
				flag &= new SystemDocuments(base.DBConfig).UpdateNextDocumentNumber(dataRow["SysDocID"].ToString(), dataRow["VoucherID"].ToString(), "GL_Transaction", "VoucherID", sqlTransaction);
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

		public bool InsertUpdateExpenseTransaction(TransactionData transactionData, bool isUpdate)
		{
			bool flag = true;
			SqlCommand insertUpdateTransactionCommand = GetInsertUpdateTransactionCommand(isUpdate);
			try
			{
				int currencyDecimalPoints = new CompanyInformations(base.DBConfig).CurrencyDecimalPoints;
				bool flag2 = bool.Parse(new CompanyOption(base.DBConfig).GetCompanyOptionValue(CompanyOptionsEnum.PDCIssuedByMaturity, false).ToString());
				DataRow dataRow = transactionData.TransactionTable.Rows[0];
				SysDocTypes sysDocTypes = (SysDocTypes)byte.Parse(dataRow["SysDocType"].ToString());
				SqlTransaction sqlTransaction = base.DBConfig.StartNewTransaction();
				string text = dataRow["VoucherID"].ToString();
				string text2 = dataRow["SysDocID"].ToString();
				string text3 = "";
				if (dataRow["RegisterID"] != DBNull.Value)
				{
					text3 = dataRow["RegisterID"].ToString();
				}
				if (!isUpdate && new SystemDocuments(base.DBConfig).ExistDocumentNumber("GL_Transaction", "VoucherID", dataRow["SysDocID"].ToString(), text, sqlTransaction))
				{
					throw new CompanyException("Document number already exist.", 1046);
				}
				switch (sysDocTypes)
				{
				case SysDocTypes.CashExpense:
				{
					bool result = false;
					if (!dataRow["IsPOS"].IsDBNullOrEmpty())
					{
						bool.TryParse(dataRow["IsPOS"].ToString(), out result);
					}
					if (result)
					{
						string cashRegisterAccountID = new POSCashRegister(base.DBConfig).GetCashRegisterAccountID(text3, "PettyCashAccountID");
						if (cashRegisterAccountID == "")
						{
							throw new CompanyException("Cash account is not set for the register.", 2008);
						}
						dataRow["AccountID"] = cashRegisterAccountID;
					}
					else
					{
						string registerAccountID2 = new Register(base.DBConfig).GetRegisterAccountID(text3, "CashAccountID");
						if (registerAccountID2 == "")
						{
							throw new CompanyException("Cash account is not set for the register.", 2008);
						}
						dataRow["AccountID"] = registerAccountID2;
					}
					break;
				}
				case SysDocTypes.CashReceiptMultiple:
				{
					string text5 = (string)(dataRow["AccountID"] = new Register(base.DBConfig).GetRegisterAccountID(text3, "CashAccountID"));
					break;
				}
				case SysDocTypes.ChequePayment:
					if (!flag2)
					{
						dataRow["ChequebookID"].ToString();
						object fieldValue = new Databases(base.DBConfig).GetFieldValue("Chequebook", "PDCIssuedAccountID", "ChequebookID", dataRow["ChequebookID"].ToString(), sqlTransaction);
						if (fieldValue == null || fieldValue.ToString() == "")
						{
							throw new CompanyException("'PDC Issued Account' is not assigned to this chequebook. Please select an account for this chequebook.");
						}
						string text4 = fieldValue.ToString();
						if (text4 == "")
						{
							throw new CompanyException("PDC Issued account is empty", 1002);
						}
						dataRow["AccountID"] = text4;
					}
					if (isUpdate && new IssuedCheques(base.DBConfig).TransactionHasProcessedCheques(text2, text, sqlTransaction))
					{
						throw new CompanyException("You cannot edit this transaction because the cheque is in use by another transaction.", 999);
					}
					break;
				}
				foreach (DataRow row in transactionData.TransactionDetailsTable.Rows)
				{
					row["SysDocID"] = dataRow["SysDocID"];
					row["VoucherID"] = dataRow["VoucherID"];
					string text6 = row["PayeeID"].ToString();
					string a = row["PayeeType"].ToString();
					string text7 = "";
					if (a == "C")
					{
						text7 = new Customers(base.DBConfig).GetCustomerARAccountID(text2, text6);
					}
					else if (a == "V")
					{
						text7 = new Vendors(base.DBConfig).GetVendorAPAccountID(text2, text6);
					}
					else if (a == "E")
					{
						text7 = new Employees(base.DBConfig).GetEmployeeAccountID(text2, text6);
					}
					else if (a == "A")
					{
						text7 = row["AccountID"].ToString();
					}
					if (text7 == "")
					{
						throw new CompanyException("AccountID should be selected for the payee if a payee is in transaction.", 1028);
					}
					row["AccountID"] = text7;
				}
				if (dataRow["CurrencyID"] != DBNull.Value && dataRow["CurrencyID"].ToString() != GetBaseCurrencyID())
				{
					decimal d = decimal.Parse(dataRow["CurrencyRate"].ToString());
					decimal num = default(decimal);
					string text8 = "M";
					text8 = new Currencies(base.DBConfig).GetCurrencyRateType(dataRow["CurrencyID"].ToString());
					foreach (DataRow row2 in transactionData.TransactionDetailsTable.Rows)
					{
						decimal result2 = default(decimal);
						decimal.TryParse(row2["AmountFC"].ToString(), out result2);
						if (text8 == "M")
						{
							row2["Amount"] = Math.Round(result2 * d, currencyDecimalPoints);
							num += Math.Round(result2 * d, currencyDecimalPoints);
						}
						else
						{
							row2["Amount"] = Math.Round(result2 / d, currencyDecimalPoints);
							num += Math.Round(result2 / d, currencyDecimalPoints);
						}
					}
					dataRow["Amount"] = num;
				}
				insertUpdateTransactionCommand.Transaction = sqlTransaction;
				flag = (isUpdate ? (flag & Update(transactionData, "GL_Transaction", insertUpdateTransactionCommand)) : (flag & Insert(transactionData, "GL_Transaction", insertUpdateTransactionCommand)));
				insertUpdateTransactionCommand = GetInsertUpdateTransactionDetailsCommand(isUpdate: false);
				insertUpdateTransactionCommand.Transaction = sqlTransaction;
				if (isUpdate)
				{
					flag &= DeleteTransactionDetailsRows(text2, text, sqlTransaction);
				}
				if (transactionData.Tables["Transaction_Details"].Rows.Count > 0)
				{
					flag &= Insert(transactionData, "Transaction_Details", insertUpdateTransactionCommand);
				}
				if (transactionData.Tables.Contains("Tax_Detail"))
				{
					flag &= new TaxTransaction(base.DBConfig).InsertUpdateTaxTransaction(transactionData, text2, text, isUpdate, sqlTransaction);
				}
				if (sysDocTypes == SysDocTypes.ChequePayment || sysDocTypes == SysDocTypes.CashExpense)
				{
					if (sysDocTypes == SysDocTypes.ChequePayment && flag2)
					{
						GLData journalData = CreateExpenseMultipleAccountGLData(transactionData);
						flag &= new APJournal(base.DBConfig).InsertAPJournalForPDC(journalData, isUpdate, sqlTransaction);
					}
					else
					{
						GLData journalData = CreateExpenseMultipleAccountGLData(transactionData);
						flag &= new Journal(base.DBConfig).InsertUpdateJournal(journalData, isUpdate, sqlTransaction);
					}
				}
				else
				{
					GLData journalData = CreateExpenseGLData(transactionData);
					flag &= new Journal(base.DBConfig).InsertUpdateJournal(journalData, isUpdate, sqlTransaction);
				}
				if (sysDocTypes == SysDocTypes.ChequePayment)
				{
					IssuedChequeData issuedChequeData = new IssuedChequeData();
					DataRow dataRow4 = issuedChequeData.IssuedChequeTable.NewRow();
					dataRow4["SysDocID"] = text2;
					dataRow4["VoucherID"] = text;
					dataRow4["PayeeType"] = dataRow["PayeeType"];
					dataRow4["PayeeID"] = dataRow["PayeeID"];
					dataRow4["PayeeAccountID"] = DBNull.Value;
					dataRow4["ChequeNumber"] = dataRow["CheckNumber"];
					dataRow4["ChequebookID"] = dataRow["ChequebookID"];
					dataRow4["ChequeDate"] = dataRow["CheckDate"];
					dataRow4["Amount"] = dataRow["Amount"];
					dataRow4["AmountFC"] = dataRow["AmountFC"];
					dataRow4["ExchangeRate"] = dataRow["CurrencyRate"];
					dataRow4["Note"] = dataRow["Description"];
					dataRow4["IssueDate"] = dataRow["TransactionDate"];
					dataRow4["Reference"] = dataRow["Reference"];
					dataRow4["PDCAccountID"] = dataRow["AccountID"];
					dataRow4["Status"] = (byte)2;
					dataRow4.EndEdit();
					issuedChequeData.IssuedChequeTable.Rows.Add(dataRow4);
					if (issuedChequeData.IssuedChequeTable.Rows.Count > 0)
					{
						flag &= new IssuedCheques(base.DBConfig).InsertUpdateIssuedCheque(issuedChequeData, isUpdate, sqlTransaction);
					}
				}
				if (!flag)
				{
					throw new Exception("Failed to save the transaction because of an unexpected error. Please try again.");
				}
				flag &= UpdateTableRowInsertUpdateInfo("GL_Transaction", "SysDocID", dataRow["SysDocID"].ToString(), "VoucherID", dataRow["VoucherID"].ToString(), sqlTransaction, !isUpdate);
				string entityName = "Tranaction";
				switch (sysDocTypes)
				{
				case SysDocTypes.CashExpense:
					entityName = "Cash Expense";
					break;
				case SysDocTypes.ChequePayment:
					entityName = "Cheque Payment";
					break;
				case SysDocTypes.CashReceipt:
					entityName = "Cash Receipt";
					break;
				case SysDocTypes.CashReceiptMultiple:
					entityName = "Cash Receipt";
					break;
				}
				flag = (isUpdate ? (flag & AddActivityLog(entityName, text, text2, ActivityTypes.Update, sqlTransaction)) : (flag & AddActivityLog(entityName, text, text2, ActivityTypes.Add, sqlTransaction)));
				if (!isUpdate)
				{
					flag &= new SystemDocuments(base.DBConfig).UpdateNextDocumentNumber(dataRow["SysDocID"].ToString(), dataRow["VoucherID"].ToString(), "GL_Transaction", "VoucherID", sqlTransaction);
				}
				if (!flag)
				{
					return flag;
				}
				if (sysDocTypes == SysDocTypes.CashExpense)
				{
					new Approval(base.DBConfig).CreateTransactionApprovalTasks(SysDocTypes.CashExpense, text2, text, "GL_Transaction", sqlTransaction);
				}
				if (sysDocTypes != SysDocTypes.CashReceiptMultiple)
				{
					return flag;
				}
				new Approval(base.DBConfig).CreateTransactionApprovalTasks(SysDocTypes.CashReceiptMultiple, text2, text, "GL_Transaction", sqlTransaction);
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

		public bool InsertUpdateTransaction(TransactionData transactionData, bool isUpdate)
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
				string text3 = dataRow["RegisterID"].ToString();
				if (!isUpdate && new SystemDocuments(base.DBConfig).ExistDocumentNumber("GL_Transaction", "VoucherID", dataRow["SysDocID"].ToString(), text, sqlTransaction))
				{
					throw new CompanyException("Document number already exist.", 1046);
				}
				string text4 = dataRow["PayeeID"].ToString();
				string text5 = dataRow["PayeeType"].ToString();
				string text6 = "";
				if (text5 == "C")
				{
					text6 = new Customers(base.DBConfig).GetCustomerARAccountID(text2, text4);
				}
				else if (text5 == "V")
				{
					text6 = new Vendors(base.DBConfig).GetVendorAPAccountID(text2, text4);
				}
				else if (text5 == "E")
				{
					text6 = new Employees(base.DBConfig).GetEmployeeAccountID(text2, text4);
				}
				else if (text5 == "A")
				{
					text6 = text4;
				}
				if (text6 == "")
				{
					throw new CompanyException("AccountID should be selected for the payee if a payee is in transaction.", 1028);
				}
				dataRow["AccountID"] = text6;
				switch (sysDocTypes)
				{
				case SysDocTypes.ChequeReceipt:
					if (!flag)
					{
						string registerAccountID = new Register(base.DBConfig).GetRegisterAccountID(text3, "PDCReceivedAccountID");
						if (registerAccountID == "")
						{
							throw new CompanyException("PDC Received Account is not selected. Please set the PDC Received account for the selected Register.");
						}
						foreach (DataRow row in transactionData.TransactionDetailsTable.Rows)
						{
							row["AccountID"] = registerAccountID;
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
					string text9 = "";
					string text10 = "";
					bool result = false;
					if (!dataRow["IsPOS"].IsDBNullOrEmpty())
					{
						bool.TryParse(dataRow["IsPOS"].ToString(), out result);
					}
					if (result)
					{
						text9 = new POSCashRegister(base.DBConfig).GetCashRegisterAccountID(text3, "PettyCashAccountID");
						if (text9 == "")
						{
							throw new CompanyException("Cash account is not set for the register.", 2008);
						}
					}
					else
					{
						text9 = new Register(base.DBConfig).GetRegisterAccountID(text3, "CashAccountID");
						text10 = new Register(base.DBConfig).GetRegisterAccountID(text3, "CardReceivedAccountID");
					}
					foreach (DataRow row3 in transactionData.TransactionDetailsTable.Rows)
					{
						if (byte.Parse(row3["PaymentMethodType"].ToString()) == 3)
						{
							row3["AccountID"] = text10;
							if (text10 == "")
							{
								throw new CompanyException("Card Account is not selected for this register. Please set the Card account for the selected register.");
							}
						}
						else
						{
							row3["AccountID"] = text9;
							if (text9 == "")
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
				case SysDocTypes.ChequePayment:
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
							string text8 = (string)(row5["AccountID"] = fieldValue.ToString());
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
					string text11 = "M";
					text11 = new Currencies(base.DBConfig).GetCurrencyRateType(dataRow["CurrencyID"].ToString());
					foreach (DataRow row7 in transactionData.TransactionDetailsTable.Rows)
					{
						decimal result2 = default(decimal);
						decimal.TryParse(row7["AmountFC"].ToString(), out result2);
						if (text11 == "M")
						{
							row7["Amount"] = Math.Round(result2 * d, currencyDecimalPoints);
							num += Math.Round(result2 * d, currencyDecimalPoints);
						}
						else
						{
							row7["Amount"] = Math.Round(result2 / d, currencyDecimalPoints);
							num += Math.Round(result2 / d, currencyDecimalPoints);
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
				if (transactionData.Tables.Contains("Tax_Detail") && transactionData.Tables["Tax_Detail"].Rows.Count > 0)
				{
					flag3 &= new TaxTransaction(base.DBConfig).InsertUpdateTaxTransaction(transactionData, text2, text, isUpdate, sqlTransaction);
				}
				if (sysDocTypes == SysDocTypes.ChequeReceipt && flag)
				{
					GLData journalData = CreateGLData(transactionData);
					flag3 &= new ARJournal(base.DBConfig).InsertARJournalForPDC(journalData, isUpdate, sqlTransaction);
				}
				else if (sysDocTypes == SysDocTypes.ChequePayment && flag2)
				{
					GLData journalData2 = CreateGLData(transactionData);
					flag3 &= new APJournal(base.DBConfig).InsertAPJournalForPDC(journalData2, isUpdate, sqlTransaction);
				}
				else
				{
					GLData journalData3 = CreateGLData(transactionData);
					flag3 &= new Journal(base.DBConfig).InsertUpdateJournal(journalData3, isUpdate, sqlTransaction);
				}
				bool flag4 = new ReceivedCheques(base.DBConfig).TransactionHasProcessedCheques(text2, text, sqlTransaction);
				if (sysDocTypes == SysDocTypes.ChequeReceipt && (!isUpdate || !flag4))
				{
					ReceivedChequeData receivedChequeData = new ReceivedChequeData();
					foreach (DataRow row8 in transactionData.TransactionDetailsTable.Rows)
					{
						DataRow dataRow6 = receivedChequeData.ReceivedChequeTable.NewRow();
						dataRow6["SysDocID"] = text2;
						dataRow6["VoucherID"] = text;
						dataRow6["PayeeType"] = text5;
						dataRow6["PayeeID"] = text4;
						dataRow6["PayeeAccountID"] = text6;
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
						receivedChequeData.ReceivedChequeTable.Rows.Add(dataRow6);
					}
					flag3 &= new ReceivedCheques(base.DBConfig).DeleteChequeRows(text2, text, sqlTransaction);
					if (receivedChequeData.ReceivedChequeTable.Rows.Count > 0)
					{
						flag3 &= new ReceivedCheques(base.DBConfig).InsertUpdateReceivedCheque(receivedChequeData, isUpdate: false, sqlTransaction);
					}
				}
				if (sysDocTypes == SysDocTypes.ChequePayment && (!isUpdate || !new IssuedCheques(base.DBConfig).TransactionHasProcessedCheques(text2, text, sqlTransaction)))
				{
					IssuedChequeData issuedChequeData = new IssuedChequeData();
					foreach (DataRow row9 in transactionData.TransactionDetailsTable.Rows)
					{
						DataRow dataRow8 = issuedChequeData.IssuedChequeTable.NewRow();
						dataRow8["SysDocID"] = text2;
						dataRow8["VoucherID"] = text;
						dataRow8["PayeeType"] = text5;
						dataRow8["PayeeID"] = text4;
						dataRow8["PayeeAccountID"] = text6;
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
						issuedChequeData.IssuedChequeTable.Rows.Add(dataRow8);
					}
					if (issuedChequeData.IssuedChequeTable.Rows.Count > 0)
					{
						flag3 &= new IssuedCheques(base.DBConfig).InsertUpdateIssuedCheque(issuedChequeData, isUpdate, sqlTransaction);
					}
				}
				if ((sysDocTypes == SysDocTypes.CashPayment || sysDocTypes == SysDocTypes.ChequePayment || sysDocTypes == SysDocTypes.TTPayment) && transactionData.Tables["AP_Payment_Advice"].Rows.Count > 0)
				{
					flag3 &= new APJournal(base.DBConfig).InsertPaymentAdvice(transactionData);
				}
				if ((sysDocTypes == SysDocTypes.CashReceipt || (sysDocTypes == SysDocTypes.ChequeReceipt && !isUpdate)) && transactionData.Tables.Contains("AR_Payment_Allocation") && transactionData.Tables["AR_Payment_Allocation"].Rows.Count > 0)
				{
					string value = "";
					object obj5 = new Databases(base.DBConfig).GetFieldValue("ARJournal", "ARID", "SysDocID", text2, "VoucherID", text, sqlTransaction).ToString();
					if (obj5 != null && obj5.ToString() != "")
					{
						value = obj5.ToString();
					}
					foreach (DataRow row10 in transactionData.Tables["AR_Payment_Allocation"].Rows)
					{
						row10["PaymentARID"] = value;
					}
					ARJournalData aRJournalData = new ARJournalData();
					DataTable dataTable = new DataTable();
					dataTable = transactionData.Tables["AR_Payment_Allocation"];
					aRJournalData.Clear();
					aRJournalData.Tables.Remove("AR_Payment_Allocation");
					aRJournalData.Tables.Add(dataTable.Copy());
					flag3 &= new ARJournal(base.DBConfig).InsertPaymentAllocation(aRJournalData);
				}
				if (!flag3)
				{
					throw new Exception("Failed to save the transaction because of an unexpected error. Please try again.");
				}
				flag3 &= UpdateTableRowInsertUpdateInfo("GL_Transaction", "SysDocID", dataRow["SysDocID"].ToString(), "VoucherID", dataRow["VoucherID"].ToString(), sqlTransaction, !isUpdate);
				string entityName = "Tranaction";
				switch (sysDocTypes)
				{
				case SysDocTypes.ChequeReceipt:
					entityName = "Cheque Receipt";
					break;
				case SysDocTypes.CashReceipt:
					entityName = "Cash Receipt";
					break;
				case SysDocTypes.CashPayment:
					entityName = "Cash Payment";
					break;
				case SysDocTypes.ChequePayment:
					entityName = "Cheque Payment";
					break;
				case SysDocTypes.TTPayment:
					entityName = "TT Payment";
					break;
				case SysDocTypes.TTReceipt:
					entityName = "TT Receipt";
					break;
				}
				flag3 = (isUpdate ? (flag3 & AddActivityLog(entityName, text, text2, ActivityTypes.Update, sqlTransaction)) : (flag3 & AddActivityLog(entityName, text, text2, ActivityTypes.Add, sqlTransaction)));
				if (!isUpdate)
				{
					flag3 &= new SystemDocuments(base.DBConfig).UpdateNextDocumentNumber(dataRow["SysDocID"].ToString(), dataRow["VoucherID"].ToString(), "GL_Transaction", "VoucherID", sqlTransaction);
				}
				string text12 = dataRow["RequestSysDocID"].ToString();
				string text13 = dataRow["RequestVoucherID"].ToString();
				if (text12 != "" && text13 != "")
				{
					string exp = "UPDATE Payment_Request Set Status = 2 WHERE SysDocID = '" + text12 + "' AND VoucherID = '" + text13 + "'";
					flag3 &= (ExecuteNonQuery(exp, sqlTransaction) > 0);
				}
				if (flag3)
				{
					switch (sysDocTypes)
					{
					case SysDocTypes.CashPayment:
						new Approval(base.DBConfig).CreateTransactionApprovalTasks(SysDocTypes.CashPayment, text2, text, "GL_Transaction", sqlTransaction);
						return flag3;
					case SysDocTypes.ChequePayment:
						new Approval(base.DBConfig).CreateTransactionApprovalTasks(SysDocTypes.ChequePayment, text2, text, "GL_Transaction", sqlTransaction);
						return flag3;
					case SysDocTypes.TTPayment:
						new Approval(base.DBConfig).CreateTransactionApprovalTasks(SysDocTypes.TTPayment, text2, text, "GL_Transaction", sqlTransaction);
						return flag3;
					case SysDocTypes.CashReceipt:
						new Approval(base.DBConfig).CreateTransactionApprovalTasks(SysDocTypes.CashReceipt, text2, text, "GL_Transaction", sqlTransaction);
						return flag3;
					case SysDocTypes.ChequeReceipt:
						new Approval(base.DBConfig).CreateTransactionApprovalTasks(SysDocTypes.ChequeReceipt, text2, text, "GL_Transaction", sqlTransaction);
						return flag3;
					case SysDocTypes.TTReceipt:
						new Approval(base.DBConfig).CreateTransactionApprovalTasks(SysDocTypes.TTReceipt, text2, text, "GL_Transaction", sqlTransaction);
						return flag3;
					default:
						return flag3;
					}
				}
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

		public bool InsertUpdateChequeReceiptMultipleTransaction(TransactionData transactionData, bool isUpdate)
		{
			bool flag = true;
			SqlCommand insertUpdateTransactionCommand = GetInsertUpdateTransactionCommand(isUpdate);
			try
			{
				int currencyDecimalPoints = new CompanyInformations(base.DBConfig).CurrencyDecimalPoints;
				DataRow dataRow = transactionData.TransactionTable.Rows[0];
				SysDocTypes sysDocTypes = (SysDocTypes)byte.Parse(dataRow["SysDocType"].ToString());
				SqlTransaction sqlTransaction = base.DBConfig.StartNewTransaction();
				string text = dataRow["VoucherID"].ToString();
				string text2 = dataRow["SysDocID"].ToString();
				if (dataRow["RegisterID"] != DBNull.Value)
				{
					dataRow["RegisterID"].ToString();
				}
				if (!isUpdate && new SystemDocuments(base.DBConfig).ExistDocumentNumber("GL_Transaction", "VoucherID", dataRow["SysDocID"].ToString(), text, sqlTransaction))
				{
					throw new CompanyException("Document number already exist.", 1046);
				}
				foreach (DataRow row in transactionData.TransactionDetailsTable.Rows)
				{
					row["SysDocID"] = dataRow["SysDocID"];
					row["VoucherID"] = dataRow["VoucherID"];
					string text3 = row["PayeeID"].ToString();
					string a = row["PayeeType"].ToString();
					string text4 = "";
					if (a == "C")
					{
						text4 = new Customers(base.DBConfig).GetCustomerARAccountID(text2, text3);
					}
					else if (a == "V")
					{
						text4 = new Vendors(base.DBConfig).GetVendorAPAccountID(text2, text3);
					}
					else if (a == "E")
					{
						text4 = new Employees(base.DBConfig).GetEmployeeAccountID(text2, text3);
					}
					else if (a == "A")
					{
						text4 = row["AccountID"].ToString();
					}
					if (text4 == "")
					{
						throw new CompanyException("AccountID should be selected for the payee if a payee is in transaction.", 1028);
					}
					row["AccountID"] = text4;
				}
				if (dataRow["CurrencyID"] != DBNull.Value && dataRow["CurrencyID"].ToString() != GetBaseCurrencyID())
				{
					decimal d = decimal.Parse(dataRow["CurrencyRate"].ToString());
					decimal num = default(decimal);
					string text5 = "M";
					text5 = new Currencies(base.DBConfig).GetCurrencyRateType(dataRow["CurrencyID"].ToString());
					foreach (DataRow row2 in transactionData.TransactionDetailsTable.Rows)
					{
						decimal result = default(decimal);
						decimal.TryParse(row2["AmountFC"].ToString(), out result);
						if (text5 == "M")
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
				flag = (isUpdate ? (flag & Update(transactionData, "GL_Transaction", insertUpdateTransactionCommand)) : (flag & Insert(transactionData, "GL_Transaction", insertUpdateTransactionCommand)));
				insertUpdateTransactionCommand = GetInsertUpdateTransactionDetailsCommand(isUpdate: false);
				insertUpdateTransactionCommand.Transaction = sqlTransaction;
				if (isUpdate)
				{
					flag &= DeleteTransactionDetailsRows(text2, text, sqlTransaction);
				}
				if (transactionData.Tables["Transaction_Details"].Rows.Count > 0)
				{
					flag &= Insert(transactionData, "Transaction_Details", insertUpdateTransactionCommand);
				}
				if (transactionData.Tables.Contains("Tax_Detail"))
				{
					flag &= new TaxTransaction(base.DBConfig).InsertUpdateTaxTransaction(transactionData, text2, text, isUpdate, sqlTransaction);
				}
				bool flag2 = new ReceivedCheques(base.DBConfig).TransactionHasProcessedCheques(text2, text, sqlTransaction);
				if (sysDocTypes == SysDocTypes.ChequeReceiptMultiple && (!isUpdate || !flag2))
				{
					ReceivedChequeData receivedChequeData = new ReceivedChequeData();
					foreach (DataRow row3 in transactionData.TransactionDetailsTable.Rows)
					{
						string text6 = row3["PayeeID"].ToString();
						string text7 = row3["PayeeType"].ToString();
						string text8 = "";
						if (text7 == "C")
						{
							text8 = new Customers(base.DBConfig).GetCustomerARAccountID(text2, text6);
						}
						else if (text7 == "V")
						{
							text8 = new Vendors(base.DBConfig).GetVendorAPAccountID(text2, text6);
						}
						else if (text7 == "E")
						{
							text8 = new Employees(base.DBConfig).GetEmployeeAccountID(text2, text6);
						}
						else if (text7 == "A")
						{
							text8 = text6;
						}
						if (text8 == "")
						{
							throw new CompanyException("AccountID should be selected for the payee if a payee is in transaction.", 1028);
						}
						DataRow dataRow5 = receivedChequeData.ReceivedChequeTable.NewRow();
						dataRow5["SysDocID"] = text2;
						dataRow5["VoucherID"] = text;
						dataRow5["PayeeType"] = text7;
						dataRow5["PayeeID"] = text6;
						dataRow5["PayeeAccountID"] = text8;
						dataRow5["BankID"] = row3["BankID"];
						dataRow5["ChequeNumber"] = row3["CheckNumber"];
						dataRow5["ChequeDate"] = row3["CheckDate"];
						dataRow5["Amount"] = row3["Amount"];
						dataRow5["AmountFC"] = row3["AmountFC"];
						dataRow5["ExchangeRate"] = dataRow["CurrencyRate"];
						dataRow5["Note"] = row3["Description"];
						dataRow5["ReceiptDate"] = dataRow["TransactionDate"];
						dataRow5["Reference"] = dataRow["Reference"];
						dataRow5["PDCAccountID"] = row3["AccountID"];
						dataRow5["Status"] = (byte)1;
						dataRow5.EndEdit();
						receivedChequeData.ReceivedChequeTable.Rows.Add(dataRow5);
					}
					flag &= new ReceivedCheques(base.DBConfig).DeleteChequeRows(text2, text, sqlTransaction);
					if (receivedChequeData.ReceivedChequeTable.Rows.Count > 0)
					{
						flag &= new ReceivedCheques(base.DBConfig).InsertUpdateReceivedCheque(receivedChequeData, isUpdate: false, sqlTransaction);
					}
				}
				if (sysDocTypes == SysDocTypes.ChequeReceiptMultiple)
				{
					GLData journalData = CreateMultipleChequeGLData(transactionData);
					flag &= new ARJournal(base.DBConfig).InsertARJournalForPDC(journalData, isUpdate, sqlTransaction);
				}
				if (!flag)
				{
					throw new Exception("Failed to save the transaction because of an unexpected error. Please try again.");
				}
				flag &= UpdateTableRowInsertUpdateInfo("GL_Transaction", "SysDocID", dataRow["SysDocID"].ToString(), "VoucherID", dataRow["VoucherID"].ToString(), sqlTransaction, !isUpdate);
				string entityName = "Transaction";
				if (sysDocTypes == SysDocTypes.ChequeReceiptMultiple)
				{
					entityName = "ChequeReceiptMultiple";
				}
				flag = (isUpdate ? (flag & AddActivityLog(entityName, text, text2, ActivityTypes.Update, sqlTransaction)) : (flag & AddActivityLog(entityName, text, text2, ActivityTypes.Add, sqlTransaction)));
				if (!isUpdate)
				{
					flag &= new SystemDocuments(base.DBConfig).UpdateNextDocumentNumber(dataRow["SysDocID"].ToString(), dataRow["VoucherID"].ToString(), "GL_Transaction", "VoucherID", sqlTransaction);
				}
				if (!flag)
				{
					return flag;
				}
				if (sysDocTypes != SysDocTypes.ChequeReceiptMultiple)
				{
					return flag;
				}
				new Approval(base.DBConfig).CreateTransactionApprovalTasks(SysDocTypes.ChequeReceiptMultiple, text2, text, "GL_Transaction", sqlTransaction);
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

		private decimal GetEquivalantFCAmount(decimal baseAmount, string currencyID, decimal rate)
		{
			int currencyDecimalPoints = new CompanyInformations(base.DBConfig).CurrencyDecimalPoints;
			if (new Currencies(base.DBConfig).GetCurrencyRateType(currencyID) == "M")
			{
				return Math.Round(baseAmount / rate, currencyDecimalPoints);
			}
			return Math.Round(baseAmount * rate, currencyDecimalPoints);
		}

		private GLData CreateIssuedChequeClearanceGLData(TransactionData transactionData, DataSet chequeData, SqlTransaction sqlTransaction)
		{
			GLData gLData = new GLData();
			bool flag = bool.Parse(new CompanyOption(base.DBConfig).GetCompanyOptionValue(CompanyOptionsEnum.PDCIssuedByMaturity, false).ToString());
			DataRow dataRow = transactionData.TransactionTable.Rows[0];
			string value = dataRow["CompanyID"].ToString();
			string value2 = dataRow["DivisionID"].ToString();
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
				string value3 = "Issued Chq. No:" + row["ChequeNumber"].ToString() + ", IV:" + row["VoucherID"].ToString();
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
				dataRow4["Description"] = value3;
				dataRow4["CompanyID"] = value;
				dataRow4["DivisionID"] = value2;
				dataRow4.EndEdit();
				gLData.JournalDetailsTable.Rows.Add(dataRow4);
				if (flag)
				{
					string text = row["SysDocID"].ToString();
					string voucherID = row["VoucherID"].ToString();
					bool flag2 = false;
					SysDocTypes systemDocumentType = new SystemDocuments(base.DBConfig).GetSystemDocumentType(text, sqlTransaction);
					TransactionData transactionData2 = null;
					if (systemDocumentType == SysDocTypes.ChequePayment)
					{
						transactionData2 = GetTransactionByID(text, voucherID, sqlTransaction);
						if (!transactionData2.TransactionTable.Rows[0]["IsSecondForm"].IsDBNullOrEmpty())
						{
							flag2 = bool.Parse(transactionData2.TransactionTable.Rows[0]["IsSecondForm"].ToString());
						}
					}
					if (systemDocumentType == SysDocTypes.ChequePayment && flag2)
					{
						foreach (DataRow row2 in transactionData2.Tables["Transaction_Details"].Rows)
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
							dataRow4["Description"] = value3;
							dataRow4["CompanyID"] = value;
							dataRow4["DivisionID"] = value2;
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
						dataRow4["Description"] = value3;
						dataRow4["CompanyID"] = value;
						dataRow4["DivisionID"] = value2;
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
					dataRow4["Description"] = value3;
					dataRow4["CompanyID"] = value;
					dataRow4["DivisionID"] = value2;
					dataRow4.EndEdit();
					gLData.JournalDetailsTable.Rows.Add(dataRow4);
				}
			}
			return gLData;
		}

		private GLData CreateChequeDepositGLData(TransactionData transactionData)
		{
			bool flag = bool.Parse(new CompanyOption(base.DBConfig).GetCompanyOptionValue(CompanyOptionsEnum.PDCByMaturity, true).ToString());
			GLData gLData = new GLData();
			DataRow dataRow = transactionData.TransactionTable.Rows[0];
			string value = dataRow["CompanyID"].ToString();
			string value2 = dataRow["DivisionID"].ToString();
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
				string value3 = "Chq.Mat No:" + dataRow5["ChequeNumber"].ToString() + ", RV:" + dataRow5["VoucherID"].ToString();
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
						dataRow3["Description"] = value3;
						dataRow3["Reference"] = dataRow["Reference"];
						dataRow3["RowIndex"] = -1;
						dataRow3["CompanyID"] = value;
						dataRow3["DivisionID"] = value2;
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
					dataRow3["Description"] = value3;
					dataRow3["Reference"] = dataRow["Reference"];
					dataRow3["RowIndex"] = -1;
					dataRow3["CompanyID"] = value;
					dataRow3["DivisionID"] = value2;
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
					dataRow3["Description"] = value3;
					dataRow3["Reference"] = dataRow["Reference"];
					dataRow3["RowIndex"] = -1;
					dataRow3["CompanyID"] = value;
					dataRow3["DivisionID"] = value2;
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
					dataRow3["Description"] = value3;
					dataRow3["Reference"] = dataRow["Reference"];
					dataRow3["RowIndex"] = 0;
					dataRow3["CompanyID"] = value;
					dataRow3["DivisionID"] = value2;
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
					dataRow3["Description"] = value3;
					dataRow3["Reference"] = dataRow["Reference"];
					dataRow3["RowIndex"] = 0;
					dataRow3["CompanyID"] = value;
					dataRow3["DivisionID"] = value2;
					dataRow3.EndEdit();
					gLData.JournalDetailsTable.Rows.Add(dataRow3);
				}
			}
			return gLData;
		}

		private GLData CreateReceivedReturnedChequeTransactionGLData(TransactionData transactionData)
		{
			bool flag = bool.Parse(new CompanyOption(base.DBConfig).GetCompanyOptionValue(CompanyOptionsEnum.PDCByMaturity, true).ToString());
			GLData gLData = new GLData();
			DataRow dataRow = transactionData.TransactionTable.Rows[0];
			string value = dataRow["CompanyID"].ToString();
			string value2 = dataRow["DivisionID"].ToString();
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
			dataRow3["CompanyID"] = value;
			dataRow3["DivisionID"] = value2;
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
			dataRow3["CompanyID"] = value;
			dataRow3["DivisionID"] = value2;
			dataRow3.EndEdit();
			gLData.JournalDetailsTable.Rows.Add(dataRow3);
			return gLData;
		}

		private GLData CreateIssuedReturnedChequeTransactionGLData(TransactionData transactionData)
		{
			bool flag = bool.Parse(new CompanyOption(base.DBConfig).GetCompanyOptionValue(CompanyOptionsEnum.PDCIssuedByMaturity, false).ToString());
			GLData gLData = new GLData();
			DataRow dataRow = transactionData.TransactionTable.Rows[0];
			string value = dataRow["CompanyID"].ToString();
			string value2 = dataRow["DivisionID"].ToString();
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
			dataRow3["CompanyID"] = value;
			dataRow3["DivisionID"] = value2;
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
			dataRow3["CompanyID"] = value;
			dataRow3["DivisionID"] = value2;
			dataRow3.EndEdit();
			gLData.JournalDetailsTable.Rows.Add(dataRow3);
			return gLData;
		}

		private GLData CreateCancelledReceivedChequeTransactionGLData(TransactionData transactionData)
		{
			GLData gLData = new GLData();
			DataRow dataRow = transactionData.TransactionTable.Rows[0];
			string value = dataRow["CompanyID"].ToString();
			string value2 = dataRow["DivisionID"].ToString();
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
			dataRow3["CompanyID"] = value;
			dataRow3["DivisionID"] = value2;
			dataRow3.EndEdit();
			gLData.JournalDetailsTable.Rows.Add(dataRow3);
			dataRow3 = gLData.JournalDetailsTable.NewRow();
			dataRow3.BeginEdit();
			dataRow3["JournalID"] = 0;
			dataRow3["AccountID"] = dataRow["SecondAccountID"];
			dataRow3["PayeeType"] = dataRow["PayeeType"];
			dataRow3["CurrencyID"] = dataRow["CurrencyID"];
			dataRow3["PayeeID"] = dataRow["PayeeID"];
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
			dataRow3["CompanyID"] = value;
			dataRow3["DivisionID"] = value2;
			dataRow3.EndEdit();
			gLData.JournalDetailsTable.Rows.Add(dataRow3);
			return gLData;
		}

		private GLData CreateChangedStatusChequeTransactionGLData(TransactionData transactionData)
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

		private GLData CreateCancelledIssuedChequeTransactionGLData(TransactionData transactionData)
		{
			GLData gLData = new GLData();
			DataRow dataRow = transactionData.TransactionTable.Rows[0];
			string value = dataRow["CompanyID"].ToString();
			string value2 = dataRow["DivisionID"].ToString();
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
			dataRow3["CompanyID"] = value;
			dataRow3["DivisionID"] = value2;
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
			dataRow3["CompanyID"] = value;
			dataRow3["DivisionID"] = value2;
			dataRow3.EndEdit();
			gLData.JournalDetailsTable.Rows.Add(dataRow3);
			return gLData;
		}

		private GLData CreateTransferTransactionGLData(TransactionData transactionData)
		{
			GLData gLData = new GLData();
			DataRow dataRow = transactionData.TransactionTable.Rows[0];
			string value = dataRow["CompanyID"].ToString();
			string value2 = dataRow["DivisionID"].ToString();
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
			dataRow3["CompanyID"] = value;
			dataRow3["DivisionID"] = value2;
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
			dataRow3["CompanyID"] = value;
			dataRow3["DivisionID"] = value2;
			dataRow3.EndEdit();
			gLData.JournalDetailsTable.Rows.Add(dataRow3);
			return gLData;
		}

		private GLData CreateDebitCreditNoteGLData(TransactionData transactionData)
		{
			GLData gLData = new GLData();
			DataRow dataRow = transactionData.TransactionTable.Rows[0];
			DataRow dataRow2 = transactionData.TransactionDetailsTable.Rows[0];
			string value = dataRow["CompanyID"].ToString();
			string value2 = dataRow["DivisionID"].ToString();
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
			decimal result = default(decimal);
			decimal result2 = default(decimal);
			decimal result3 = default(decimal);
			if (dataRow["Amount"] != DBNull.Value)
			{
				decimal.TryParse(dataRow["Amount"].ToString(), out result2);
			}
			if (dataRow["AmountFC"] != DBNull.Value)
			{
				decimal.TryParse(dataRow["AmountFC"].ToString(), out result3);
			}
			if (dataRow["TaxAmount"] != DBNull.Value)
			{
				decimal.TryParse(dataRow["TaxAmount"].ToString(), out result);
			}
			if (dataRow["PayeeType"] != DBNull.Value)
			{
				dataRow4["IsARAP"] = true;
			}
			switch (sysDocTypes)
			{
			case SysDocTypes.DebitNote:
				dataRow4["Debit"] = result2 + result;
				dataRow4["DebitFC"] = result3;
				dataRow4["Credit"] = DBNull.Value;
				dataRow4["CreditFC"] = DBNull.Value;
				break;
			case SysDocTypes.CreditNote:
				dataRow4["Debit"] = DBNull.Value;
				dataRow4["DebitFC"] = DBNull.Value;
				dataRow4["Credit"] = result2 + result;
				dataRow4["CreditFC"] = result3;
				break;
			}
			dataRow4["Description"] = dataRow["Description"];
			dataRow4["Reference"] = dataRow["Reference"];
			dataRow4["CostCenterID"] = dataRow["CostCenterID"];
			dataRow4["AnalysisID"] = dataRow["AnalysisID"];
			dataRow4["JobID"] = dataRow2["JobID"];
			dataRow4["AttributeID1"] = dataRow2["AttributeID1"];
			dataRow4["AttributeID2"] = dataRow2["AttributeID2"];
			dataRow4["RowIndex"] = -1;
			dataRow4["CompanyID"] = value;
			dataRow4["DivisionID"] = value2;
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
				dataRow4["AttributeID1"] = row["AttributeID1"];
				dataRow4["AttributeID2"] = row["AttributeID2"];
				dataRow4["ConsignID"] = row["ConsignID"];
				dataRow4["ConsignExpenseID"] = row["ConsignExpenseID"];
				dataRow4["AnalysisID"] = row["AnalysisID"];
				dataRow4["RowIndex"] = row["RowIndex"];
				dataRow4["CompanyID"] = value;
				dataRow4["DivisionID"] = value2;
				dataRow4.EndEdit();
				gLData.JournalDetailsTable.Rows.Add(dataRow4);
			}
			if (result > 0m)
			{
				if (transactionData.Tables["Tax_Detail"].Rows.Count <= 0)
				{
					throw new CompanyException("Tax details not found for the transaction.");
				}
				DataRow[] array = transactionData.Tables["Tax_Detail"].Select("RowIndex = -1");
				decimal num = default(decimal);
				for (int i = 0; i < array.Length; i++)
				{
					num = default(decimal);
					DataRow obj = array[i];
					dataRow4 = gLData.JournalDetailsTable.NewRow();
					dataRow4.BeginEdit();
					dataRow4["JournalID"] = 0;
					string text = "";
					text = obj["TaxItemID"].ToString();
					string text2 = "";
					string text3 = "";
					string exp = "SELECT SalesTaxAccountID FROM Tax WHERE  TaxCode = '" + text.Trim() + "'";
					object obj2 = ExecuteScalar(exp);
					if (obj2 != null)
					{
						text2 = obj2.ToString();
					}
					if (text2 == "" && sysDocTypes == SysDocTypes.DebitNote)
					{
						throw new CompanyException("AccountID is not set for tax item: " + text + ".");
					}
					exp = "SELECT PurchaseTaxAccountID FROM Tax WHERE  TaxCode='" + text.Trim() + "'";
					obj2 = ExecuteScalar(exp);
					if (obj2 != null)
					{
						text3 = obj2.ToString();
					}
					if (text3 == "" && sysDocTypes == SysDocTypes.CreditNote)
					{
						throw new CompanyException("AccountID is not set for tax item: " + text + ".");
					}
					decimal.TryParse(obj["TaxAmount"].ToString(), out num);
					dataRow4["PayeeID"] = dataRow["PayeeID"];
					dataRow4["PayeeType"] = "A";
					int currencyDecimalPoints = new CompanyInformations(base.DBConfig).CurrencyDecimalPoints;
					switch (sysDocTypes)
					{
					case SysDocTypes.DebitNote:
						dataRow4["AccountID"] = text2;
						dataRow4["Credit"] = Math.Round(num, currencyDecimalPoints, MidpointRounding.AwayFromZero);
						dataRow4["Debit"] = DBNull.Value;
						break;
					case SysDocTypes.CreditNote:
						dataRow4["AccountID"] = text3;
						dataRow4["Debit"] = Math.Round(num, currencyDecimalPoints, MidpointRounding.AwayFromZero);
						dataRow4["Credit"] = DBNull.Value;
						break;
					}
					dataRow4["Reference"] = dataRow["Reference"];
					dataRow4["CompanyID"] = value;
					dataRow4["DivisionID"] = value2;
					dataRow4.EndEdit();
					gLData.JournalDetailsTable.Rows.Add(dataRow4);
				}
			}
			return gLData;
		}

		private GLData CreateReceiptMultiGLData(TransactionData transactionData)
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

		private GLData CreateExpenseMultipleAccountGLData(TransactionData transactionData)
		{
			GLData gLData = new GLData();
			DataRow dataRow = transactionData.TransactionTable.Rows[0];
			DataRow dataRow2 = transactionData.TransactionDetailsTable.Rows[0];
			dataRow["VoucherID"].ToString();
			string sysDocID = dataRow["SysDocID"].ToString();
			string value = dataRow["CompanyID"].ToString();
			string value2 = dataRow["DivisionID"].ToString();
			int currencyDecimalPoints = new CompanyInformations(base.DBConfig).CurrencyDecimalPoints;
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
			decimal result = default(decimal);
			decimal result2 = default(decimal);
			decimal result3 = default(decimal);
			if (dataRow["Amount"] != DBNull.Value)
			{
				decimal.TryParse(dataRow["Amount"].ToString(), out result2);
			}
			if (dataRow["AmountFC"] != DBNull.Value)
			{
				decimal.TryParse(dataRow["AmountFC"].ToString(), out result3);
			}
			if (dataRow["TaxAmount"] != DBNull.Value)
			{
				decimal.TryParse(dataRow["TaxAmount"].ToString(), out result);
			}
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
			dataRow4["Credit"] = result2 + result;
			dataRow4["CreditFC"] = result3;
			dataRow4["CheckID"] = dataRow["ChequeID"];
			dataRow4["CheckNumber"] = dataRow["CheckNumber"];
			dataRow4["CheckDate"] = dataRow["CheckDate"];
			dataRow4["CheckbookID"] = dataRow["ChequebookID"];
			dataRow4["Description"] = dataRow["Description"];
			dataRow4["Reference"] = dataRow["Reference"];
			dataRow4["CostCenterID"] = dataRow["CostCenterID"];
			dataRow4["JobID"] = dataRow2["JobID"];
			dataRow4["CostCategoryID"] = dataRow2["CostCategoryID"];
			dataRow4["AttributeID1"] = dataRow2["AttributeID1"];
			dataRow4["AttributeID2"] = dataRow2["AttributeID2"];
			dataRow4["ConsignID"] = dataRow2["ConsignID"];
			dataRow4["ConsignExpenseID"] = dataRow2["ConsignExpenseID"];
			dataRow4["AnalysisID"] = dataRow["AnalysisID"];
			dataRow4["RowIndex"] = -1;
			dataRow4["CompanyID"] = value;
			dataRow4["DivisionID"] = value2;
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
				dataRow4["AttributeID1"] = row["AttributeID1"];
				dataRow4["AttributeID2"] = row["AttributeID2"];
				dataRow4["ConsignID"] = dataRow2["ConsignID"];
				dataRow4["ConsignExpenseID"] = dataRow2["ConsignExpenseID"];
				dataRow4["AnalysisID"] = row["AnalysisID"];
				dataRow4["RowIndex"] = row["RowIndex"];
				dataRow4["CompanyID"] = value;
				dataRow4["DivisionID"] = value2;
				dataRow4.EndEdit();
				gLData.JournalDetailsTable.Rows.Add(dataRow4);
			}
			if (result > 0m)
			{
				if (transactionData.Tables["Tax_Detail"].Rows.Count <= 0)
				{
					throw new CompanyException("Tax details not found for the transaction.");
				}
				DataRow[] array = transactionData.Tables["Tax_Detail"].Select("RowIndex = -1");
				decimal num = default(decimal);
				for (int i = 0; i < array.Length; i++)
				{
					num = default(decimal);
					DataRow obj = array[i];
					dataRow4 = gLData.JournalDetailsTable.NewRow();
					dataRow4.BeginEdit();
					dataRow4["JournalID"] = 0;
					string text4 = "";
					text4 = obj["TaxItemID"].ToString();
					string text5 = "";
					string exp = "SELECT PurchaseTaxAccountID FROM Tax WHERE  TaxCode = '" + text4.Trim() + "'";
					object obj2 = ExecuteScalar(exp);
					if (obj2 != null)
					{
						text5 = obj2.ToString();
					}
					if (text5 == "")
					{
						throw new CompanyException("AccountID is not set for tax item: " + text4 + ".");
					}
					decimal.TryParse(obj["TaxAmount"].ToString(), out num);
					dataRow4["AccountID"] = text5;
					dataRow4["PayeeID"] = dataRow["PayeeID"];
					dataRow4["PayeeType"] = "A";
					dataRow4["Debit"] = Math.Round(num, currencyDecimalPoints, MidpointRounding.AwayFromZero);
					dataRow4["Credit"] = DBNull.Value;
					dataRow4["Reference"] = dataRow["Reference"];
					dataRow4["CompanyID"] = value;
					dataRow4["DivisionID"] = value2;
					dataRow4.EndEdit();
					gLData.JournalDetailsTable.Rows.Add(dataRow4);
				}
			}
			return gLData;
		}

		private GLData CreateMultipleChequeGLData(TransactionData transactionData)
		{
			GLData gLData = new GLData();
			DataRow dataRow = transactionData.TransactionTable.Rows[0];
			DataRow dataRow2 = transactionData.TransactionDetailsTable.Rows[0];
			dataRow["VoucherID"].ToString();
			string sysDocID = dataRow["SysDocID"].ToString();
			string value = dataRow["CompanyID"].ToString();
			string value2 = dataRow["DivisionID"].ToString();
			int currencyDecimalPoints = new CompanyInformations(base.DBConfig).CurrencyDecimalPoints;
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
			decimal result = default(decimal);
			decimal result2 = default(decimal);
			decimal result3 = default(decimal);
			if (dataRow["Amount"] != DBNull.Value)
			{
				decimal.TryParse(dataRow["Amount"].ToString(), out result2);
			}
			if (dataRow["AmountFC"] != DBNull.Value)
			{
				decimal.TryParse(dataRow["AmountFC"].ToString(), out result3);
			}
			if (dataRow["TaxAmount"] != DBNull.Value)
			{
				decimal.TryParse(dataRow["TaxAmount"].ToString(), out result);
			}
			dataRow3.EndEdit();
			gLData.JournalTable.Rows.Add(dataRow3);
			DataRow dataRow4 = gLData.JournalDetailsTable.NewRow();
			dataRow4.BeginEdit();
			dataRow4["JournalID"] = 0;
			dataRow4["PayeeType"] = dataRow["PayeeType"];
			dataRow4["PayeeID"] = dataRow["PayeeID"];
			dataRow4["AccountID"] = dataRow["AccountID"];
			dataRow4["Credit"] = DBNull.Value;
			dataRow4["CreditFC"] = DBNull.Value;
			dataRow4["Debit"] = result2 + result;
			dataRow4["DebitFC"] = result3;
			dataRow4["CheckID"] = dataRow["ChequeID"];
			dataRow4["CheckNumber"] = dataRow["CheckNumber"];
			dataRow4["CheckDate"] = dataRow["CheckDate"];
			dataRow4["CheckbookID"] = dataRow["ChequebookID"];
			dataRow4["Description"] = dataRow["Description"];
			dataRow4["Reference"] = dataRow["Reference"];
			dataRow4["CostCenterID"] = dataRow["CostCenterID"];
			dataRow4["JobID"] = dataRow2["JobID"];
			dataRow4["CostCategoryID"] = dataRow2["CostCategoryID"];
			dataRow4["AttributeID1"] = dataRow2["AttributeID1"];
			dataRow4["AttributeID2"] = dataRow2["AttributeID2"];
			dataRow4["ConsignID"] = dataRow2["ConsignID"];
			dataRow4["ConsignExpenseID"] = dataRow2["ConsignExpenseID"];
			dataRow4["AnalysisID"] = dataRow["AnalysisID"];
			dataRow4["RowIndex"] = -1;
			dataRow4["CompanyID"] = value;
			dataRow4["DivisionID"] = value2;
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
				dataRow4["Credit"] = row["Amount"];
				dataRow4["CreditFC"] = row["AmountFC"];
				dataRow4["Debit"] = DBNull.Value;
				dataRow4["DebitFC"] = DBNull.Value;
				dataRow4["CheckID"] = row["ChequeID"];
				dataRow4["CheckNumber"] = row["CheckNumber"];
				dataRow4["CheckDate"] = row["CheckDate"];
				dataRow4["CheckbookID"] = row["ChequebookID"];
				dataRow4["Description"] = row["Description"];
				dataRow4["Reference"] = row["Reference"];
				dataRow4["CostCenterID"] = row["CostCenterID"];
				dataRow4["JobID"] = row["JobID"];
				dataRow4["CostCategoryID"] = row["CostCategoryID"];
				dataRow4["AttributeID1"] = row["AttributeID1"];
				dataRow4["AttributeID2"] = row["AttributeID2"];
				dataRow4["ConsignID"] = dataRow2["ConsignID"];
				dataRow4["ConsignExpenseID"] = dataRow2["ConsignExpenseID"];
				dataRow4["AnalysisID"] = row["AnalysisID"];
				dataRow4["RowIndex"] = row["RowIndex"];
				dataRow4["CompanyID"] = value;
				dataRow4["DivisionID"] = value2;
				dataRow4.EndEdit();
				gLData.JournalDetailsTable.Rows.Add(dataRow4);
			}
			if (result > 0m)
			{
				if (transactionData.Tables["Tax_Detail"].Rows.Count <= 0)
				{
					throw new CompanyException("Tax details not found for the transaction.");
				}
				DataRow[] array = transactionData.Tables["Tax_Detail"].Select("RowIndex = -1");
				decimal num = default(decimal);
				for (int i = 0; i < array.Length; i++)
				{
					num = default(decimal);
					DataRow obj = array[i];
					dataRow4 = gLData.JournalDetailsTable.NewRow();
					dataRow4.BeginEdit();
					dataRow4["JournalID"] = 0;
					string text4 = "";
					text4 = obj["TaxItemID"].ToString();
					string text5 = "";
					string exp = "SELECT PurchaseTaxAccountID FROM Tax WHERE  TaxCode = '" + text4.Trim() + "'";
					object obj2 = ExecuteScalar(exp);
					if (obj2 != null)
					{
						text5 = obj2.ToString();
					}
					if (text5 == "")
					{
						throw new CompanyException("AccountID is not set for tax item: " + text4 + ".");
					}
					decimal.TryParse(obj["TaxAmount"].ToString(), out num);
					dataRow4["AccountID"] = text5;
					dataRow4["PayeeID"] = dataRow["PayeeID"];
					dataRow4["PayeeType"] = "A";
					dataRow4["Debit"] = Math.Round(num, currencyDecimalPoints, MidpointRounding.AwayFromZero);
					dataRow4["Credit"] = DBNull.Value;
					dataRow4["Reference"] = dataRow["Reference"];
					dataRow4["CompanyID"] = value;
					dataRow4["DivisionID"] = value2;
					dataRow4.EndEdit();
					gLData.JournalDetailsTable.Rows.Add(dataRow4);
				}
			}
			return gLData;
		}

		private GLData CreateExpenseGLData(TransactionData transactionData)
		{
			GLData gLData = new GLData();
			DataRow dataRow = transactionData.TransactionTable.Rows[0];
			DataRow dataRow2 = transactionData.TransactionDetailsTable.Rows[0];
			dataRow["VoucherID"].ToString();
			string sysDocID = dataRow["SysDocID"].ToString();
			string value = dataRow["CompanyID"].ToString();
			string value2 = dataRow["DivisionID"].ToString();
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
			dataRow4["AttributeID1"] = dataRow2["AttributeID1"];
			dataRow4["AttributeID2"] = dataRow2["AttributeID2"];
			dataRow4["ConsignID"] = dataRow2["ConsignID"];
			dataRow4["ConsignExpenseID"] = dataRow2["ConsignExpenseID"];
			dataRow4["AnalysisID"] = dataRow["AnalysisID"];
			dataRow4["RowIndex"] = -1;
			dataRow4["CompanyID"] = value;
			dataRow4["DivisionID"] = value2;
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
				dataRow4["AttributeID1"] = row["AttributeID1"];
				dataRow4["AttributeID2"] = row["AttributeID2"];
				dataRow4["ConsignID"] = dataRow2["ConsignID"];
				dataRow4["ConsignExpenseID"] = dataRow2["ConsignExpenseID"];
				dataRow4["AnalysisID"] = row["AnalysisID"];
				dataRow4["RowIndex"] = row["RowIndex"];
				dataRow4["CompanyID"] = value;
				dataRow4["DivisionID"] = value2;
				dataRow4.EndEdit();
				gLData.JournalDetailsTable.Rows.Add(dataRow4);
			}
			return gLData;
		}

		private GLData CreateGLData(TransactionData transactionData)
		{
			GLData gLData = new GLData();
			bool flag = false;
			DataRow dataRow = transactionData.TransactionTable.Rows[0];
			DataRow dataRow2 = transactionData.TransactionDetailsTable.Rows[0];
			string value = dataRow["CompanyID"].ToString();
			string value2 = dataRow["DivisionID"].ToString();
			int currencyDecimalPoints = new CompanyInformations(base.DBConfig).CurrencyDecimalPoints;
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
			decimal result = default(decimal);
			if (dataRow["TaxAmount"] != DBNull.Value)
			{
				decimal.TryParse(dataRow["TaxAmount"].ToString(), out result);
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
			{
				decimal result2 = default(decimal);
				decimal result3 = default(decimal);
				if (!dataRow["AmountFC"].IsDBNullOrEmpty())
				{
					decimal.TryParse(dataRow["AmountFC"].ToString(), out result3);
					dataRow4["DebitFC"] = result3;
				}
				if (!dataRow["Amount"].IsDBNullOrEmpty())
				{
					decimal.TryParse(dataRow["Amount"].ToString(), out result2);
					dataRow4["Debit"] = result2;
				}
				dataRow4["Credit"] = DBNull.Value;
				dataRow4["CreditFC"] = DBNull.Value;
				break;
			}
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
			dataRow4["AttributeID1"] = dataRow2["AttributeID1"];
			dataRow4["AttributeID2"] = dataRow2["AttributeID2"];
			dataRow4["ConsignID"] = dataRow2["ConsignID"];
			dataRow4["ConsignExpenseID"] = dataRow2["ConsignExpenseID"];
			dataRow4["AnalysisID"] = dataRow["AnalysisID"];
			dataRow4["RowIndex"] = -1;
			dataRow4["CompanyID"] = value;
			dataRow4["DivisionID"] = value2;
			dataRow4.EndEdit();
			gLData.JournalDetailsTable.Rows.Add(dataRow4);
			foreach (DataRow row in transactionData.TransactionDetailsTable.Rows)
			{
				decimal d = Convert.ToDecimal(row["Amount"].ToString());
				string text = row["ExpCode"].ToString();
				decimal result4 = default(decimal);
				decimal.TryParse(row["ExpAmount"].ToString(), out result4);
				decimal result5 = default(decimal);
				decimal.TryParse(row["ExpPercent"].ToString(), out result5);
				string value3 = "";
				decimal num = default(decimal);
				if (text != "" && result5 == 0m)
				{
					num = d - result4;
				}
				else if (text != "" && result5 > 0m)
				{
					num = result4;
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
					value3 = new ExpenseCode(base.DBConfig).GetExpenseAccountID(text, sqlTransaction);
					base.DBConfig.EndTransaction(result: true);
				}
				switch (sysDocTypes)
				{
				case SysDocTypes.ChequeReceipt:
				case SysDocTypes.CashReceipt:
				case SysDocTypes.TTReceipt:
					dataRow4["DebitFC"] = row["AmountFC"];
					dataRow4["Debit"] = row["Amount"];
					if (text != "" && result5 == 0m)
					{
						dataRow4["Debit"] = num;
					}
					else if (text != "" && result5 > 0m)
					{
						dataRow4["Debit"] = num;
						dataRow4["AccountID"] = value3;
					}
					dataRow4["Credit"] = DBNull.Value;
					dataRow4["CreditFC"] = DBNull.Value;
					break;
				case SysDocTypes.CashPayment:
				case SysDocTypes.ChequePayment:
				{
					decimal result8 = default(decimal);
					decimal result9 = default(decimal);
					if (!row["AmountFC"].IsDBNullOrEmpty())
					{
						decimal.TryParse(row["AmountFC"].ToString(), out result9);
						dataRow4["CreditFC"] = result9 + result;
					}
					if (!row["Amount"].IsDBNullOrEmpty())
					{
						decimal.TryParse(row["Amount"].ToString(), out result8);
						dataRow4["Credit"] = result8 + result;
					}
					dataRow4["Debit"] = DBNull.Value;
					dataRow4["DebitFC"] = DBNull.Value;
					break;
				}
				case SysDocTypes.TTPayment:
				{
					dataRow4["Debit"] = DBNull.Value;
					dataRow4["DebitFC"] = DBNull.Value;
					decimal result6 = default(decimal);
					decimal result7 = default(decimal);
					if (!row["AmountFC"].IsDBNullOrEmpty())
					{
						decimal.TryParse(row["AmountFC"].ToString(), out result7);
						dataRow4["CreditFC"] = result7;
					}
					if (!row["Amount"].IsDBNullOrEmpty())
					{
						decimal.TryParse(row["Amount"].ToString(), out result6);
						dataRow4["Credit"] = result6;
					}
					break;
				}
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
				dataRow4["AttributeID1"] = row["AttributeID1"];
				dataRow4["AttributeID2"] = row["AttributeID2"];
				dataRow4["ConsignID"] = dataRow2["ConsignID"];
				dataRow4["ConsignExpenseID"] = dataRow2["ConsignExpenseID"];
				dataRow4["AnalysisID"] = row["AnalysisID"];
				dataRow4["RowIndex"] = row["RowIndex"];
				dataRow4["CompanyID"] = value;
				dataRow4["DivisionID"] = value2;
				dataRow4.EndEdit();
				gLData.JournalDetailsTable.Rows.Add(dataRow4);
			}
			string bankAccountID = "";
			foreach (DataRow row2 in transactionData.BankFeeDetailsTable.Rows)
			{
				dataRow4 = gLData.JournalDetailsTable.NewRow();
				dataRow4.BeginEdit();
				dataRow4["JournalID"] = 0;
				dataRow4["AccountID"] = row2["ExpenseAccountID"];
				dataRow4["DebitFC"] = row2["AmountFC"];
				dataRow4["Debit"] = row2["Amount"];
				dataRow4["IsBaseOnly"] = true;
				dataRow4["Credit"] = DBNull.Value;
				dataRow4["CreditFC"] = DBNull.Value;
				dataRow4["Description"] = row2["Description"];
				dataRow4["Reference"] = row2["Reference"];
				dataRow4["RowIndex"] = row2["RowIndex"];
				dataRow4["CompanyID"] = value;
				dataRow4["DivisionID"] = value2;
				bankAccountID = row2["BankAccountID"].ToString();
				dataRow4.EndEdit();
				gLData.JournalDetailsTable.Rows.Add(dataRow4);
			}
			bool flag2 = false;
			foreach (DataRow row3 in transactionData.BankFeeDetailsTable.Rows)
			{
				dataRow4 = gLData.JournalDetailsTable.NewRow();
				dataRow4.BeginEdit();
				dataRow4["JournalID"] = 0;
				dataRow4["IsBaseOnly"] = true;
				bankAccountID = row3["BankAccountID"].ToString();
				transactionData.BankFeeDetailsTable.DefaultView.RowFilter = "BankAccountID = '" + bankAccountID + "'";
				DataTable dataTable = transactionData.BankFeeDetailsTable.DefaultView.ToTable();
				List<string> list = (from table in dataTable.AsEnumerable()
					where table.Field<string>("BankAccountID") == bankAccountID
					select table.Field<string>("Description")).ToList();
				string.Join(",", list.ToArray());
				string filterExpression = "AccountID = '" + bankAccountID + "'";
				gLData.JournalDetailsTable.Select(filterExpression);
				decimal result10 = default(decimal);
				decimal result11 = default(decimal);
				object obj = dataTable.Compute("Sum(Amount)", "");
				object obj2 = dataTable.Compute("Sum(AmountFC)", "");
				if (obj != null)
				{
					decimal.TryParse(obj.ToString(), out result10);
				}
				if (obj2 != null)
				{
					decimal.TryParse(obj2.ToString(), out result11);
				}
				dataRow4["AccountID"] = row3["BankAccountID"];
				dataRow4["DebitFC"] = DBNull.Value;
				dataRow4["Debit"] = DBNull.Value;
				dataRow4["Credit"] = Math.Round(result10 + result, currencyDecimalPoints, MidpointRounding.AwayFromZero);
				dataRow4["CreditFC"] = Math.Round(result11 + result, currencyDecimalPoints, MidpointRounding.AwayFromZero);
				dataRow4["Description"] = row3["Description"];
				dataRow4["Reference"] = row3["Reference"];
				dataRow4["RowIndex"] = row3["RowIndex"];
				dataRow4["CompanyID"] = value;
				dataRow4["DivisionID"] = value2;
				dataRow4.EndEdit();
				if (!flag2)
				{
					gLData.JournalDetailsTable.Rows.Add(dataRow4);
					flag2 = true;
				}
			}
			if (result > 0m)
			{
				if (transactionData.Tables["Tax_Detail"].Rows.Count <= 0)
				{
					throw new CompanyException("Tax details not found for the transaction.");
				}
				DataRow[] array = transactionData.Tables["Tax_Detail"].Select("RowIndex = -1");
				decimal num2 = default(decimal);
				for (int i = 0; i < array.Length; i++)
				{
					num2 = default(decimal);
					DataRow obj3 = array[i];
					dataRow4 = gLData.JournalDetailsTable.NewRow();
					dataRow4.BeginEdit();
					dataRow4["JournalID"] = 0;
					string text2 = "";
					text2 = obj3["TaxItemID"].ToString();
					string text3 = "";
					string exp = "SELECT PurchaseTaxAccountID FROM Tax WHERE  TaxCode = '" + text2.Trim() + "'";
					object obj4 = ExecuteScalar(exp);
					if (obj4 != null)
					{
						text3 = obj4.ToString();
					}
					if (text3 == "")
					{
						throw new CompanyException("AccountID is not set for tax item: " + text2 + ".");
					}
					decimal.TryParse(obj3["TaxAmount"].ToString(), out num2);
					dataRow4["AccountID"] = text3;
					dataRow4["PayeeID"] = dataRow["PayeeID"];
					dataRow4["PayeeType"] = "A";
					dataRow4["Debit"] = Math.Round(num2, currencyDecimalPoints, MidpointRounding.AwayFromZero);
					dataRow4["Credit"] = DBNull.Value;
					dataRow4["Reference"] = dataRow["Reference"];
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
				string commandText = "DELETE FROM Transaction_Details WHERE SysDocID = '" + sysDocID + "' AND VoucherID = '" + voucherID + "'";
				flag &= Delete(commandText, sqlTransaction);
				commandText = "DELETE FROM Bank_Fee_Details WHERE GLTransactionSysDocID = '" + sysDocID + "' AND GLTransactionVoucherID = '" + voucherID + "'";
				flag &= Delete(commandText, sqlTransaction);
				commandText = "DELETE FROM General_Payment_Detail WHERE SysDocID = '" + sysDocID + "' AND VoucherID = '" + voucherID + "'";
				flag &= Delete(commandText, sqlTransaction);
				return flag & new TaxTransaction(base.DBConfig).DeleteTaxTransactionDetailsRows(sysDocID, voucherID, sqlTransaction);
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
				if (systemDocumentType == SysDocTypes.ChequeReceipt)
				{
					if (new ReceivedCheques(base.DBConfig).TransactionHasProcessedCheques(sysDocID, voucherID, sqlTransaction))
					{
						throw new CompanyException("One or more cheques in this transaction are already in use by another transaction.", 1030);
					}
				}
				else if ((systemDocumentType == SysDocTypes.ChequePayment || systemDocumentType == SysDocTypes.ChequePayment) && new IssuedCheques(base.DBConfig).TransactionHasProcessedCheques(sysDocID, voucherID, sqlTransaction))
				{
					throw new CompanyException("One or more cheques in this transaction are already in use by another transaction.", 1030);
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
				bool num = bool.Parse(new CompanyOption(base.DBConfig).GetCompanyOptionValue(CompanyOptionsEnum.PDCByMaturity, true).ToString()) && systemDocumentType == SysDocTypes.ChequeReceipt;
				bool flag3 = new Journal(base.DBConfig).GetJournalID(sysDocID, voucherID, sqlTransaction).ToString() == "-1";
				if (!num && !flag3)
				{
					flag &= new Journal(base.DBConfig).VoidJournal(sysDocID, voucherID, isVoid, sqlTransaction);
				}
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
				if (systemDocumentType == SysDocTypes.ChequeReceipt)
				{
					if (new ReceivedCheques(base.DBConfig).TransactionHasProcessedCheques(sysDocID, voucherID, sqlTransaction))
					{
						throw new CompanyException("One or more cheques in this transaction are already in use by another transaction.", 1030);
					}
				}
				else if ((systemDocumentType == SysDocTypes.ChequePayment || systemDocumentType == SysDocTypes.ChequePayment) && new IssuedCheques(base.DBConfig).TransactionHasProcessedCheques(sysDocID, voucherID, sqlTransaction))
				{
					throw new CompanyException("One or more cheques in this transaction are already in use by another transaction.", 1030);
				}
				flag &= DeleteTransactionDetailsRows(sysDocID, voucherID, sqlTransaction);
				string exp = "DELETE FROM GL_Transaction WHERE SysDocID='" + sysDocID + "' AND VoucherID='" + voucherID + "'";
				flag &= (ExecuteNonQuery(exp, sqlTransaction) > 0);
				flag &= new Journal(base.DBConfig).DeleteJournal(sysDocID, voucherID, sqlTransaction);
				switch (systemDocumentType)
				{
				case SysDocTypes.ChequeReceipt:
				case SysDocTypes.ChequeReceiptMultiple:
					flag &= new ARJournal(base.DBConfig).DeleteARJournal(sysDocID, voucherID, sqlTransaction);
					break;
				case SysDocTypes.ChequePayment:
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

		public bool DeleteChequeDepositTransaction(string sysDocID, string voucherID)
		{
			bool flag = true;
			try
			{
				bool flag2 = bool.Parse(new CompanyOption(base.DBConfig).GetCompanyOptionValue(CompanyOptionsEnum.PDCDirectMaturity, true).ToString());
				SqlTransaction sqlTransaction = base.DBConfig.StartNewTransaction();
				string exp = "SELECT Count(ChequeID) FROM Cheque_Received WHERE Status<>2 AND DepositSysDocID='" + sysDocID + "' AND DepositVoucherID='" + voucherID + "'";
				object obj = ExecuteScalar(exp, sqlTransaction);
				if (obj != null && obj.ToString() != string.Empty && int.Parse(obj.ToString()) > 0)
				{
					throw new CompanyException("The transaction cannot be deleted. One or more cheques are not in 'Deposited' status.", 1001);
				}
				flag = ((!flag2) ? (flag & new ReceivedCheques(base.DBConfig).UpdateChequeStatus(sysDocID, voucherID, ReceivedChequeStatus.SentToBank, sqlTransaction)) : (flag & new ReceivedCheques(base.DBConfig).UpdateChequeStatus(sysDocID, voucherID, ReceivedChequeStatus.Undeposited, sqlTransaction)));
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

		public TransactionData GetTransactionByID(string sysDocID, string voucherID)
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

		public TransactionData GetTransactionByID(string sysDocID, string voucherID, SqlTransaction sqlTransaction)
		{
			try
			{
				TransactionData transactionData = new TransactionData();
				string textCommand = "SELECT GL.*, ACC.AccountName FROM GL_Transaction GL LEFT JOIN Account ACC ON GL.FirstAccountID=ACC.AccountID WHERE VoucherID='" + voucherID + "' AND SysDocID='" + sysDocID + "'";
				FillDataSet(transactionData, "GL_Transaction", textCommand, sqlTransaction);
				if (transactionData == null || transactionData.Tables.Count == 0 || transactionData.Tables["GL_Transaction"].Rows.Count == 0)
				{
					return null;
				}
				transactionData.TransactionTable.Rows[0]["TransactionID"].ToString();
				textCommand = "SELECT TD.*,\r\n\t\t\t\t\t\t(CASE PayeeType\r\n\t\t\t\t\t\tWHEN 'C' THEN Customer.CustomerName\r\n\t\t\t\t\t\tWHEN 'V' THEN Vendor.VendorName\r\n\t\t\t\t\t\tWHEN 'E' THEN Employee.FirstName +' '+ Employee.MiddleName+ ' '+Employee.LastName\r\n\t\t\t\t\t\tELSE Account.AccountName END) AS AccountName, Account.Alias\r\n\t\t\t\t\t\tFROM TRANSACTION_DETAILS TD LEFt OUTER JOIN \r\n\t\t\t\t\t\tAccount ON TD.AccountID=Account.AccountID LEFt OUTER JOIN\r\n\t\t\t\t\t\tCustomer ON TD.PayeeID=Customer.CustomerID LEFt OUTER JOIN\r\n\t\t\t\t\t\tVendor ON TD.PayeeID=Vendor.VendorID LEFt OUTER JOIN\r\n\t\t\t\t\t\tEmployee ON TD.PayeeID=Employee.EmployeeID\r\n\t\t\t\t\t\tWHERE VoucherID='" + voucherID + "' AND SysDocID='" + sysDocID + "'";
				FillDataSet(transactionData, "Transaction_Details", textCommand, sqlTransaction);
				switch (new SystemDocuments(base.DBConfig).GetSystemDocumentType(sysDocID, sqlTransaction))
				{
				case SysDocTypes.ChequeReceipt:
				case SysDocTypes.ChequeReceiptMultiple:
					textCommand = "SELECT * FROM Cheque_Received WHERE \r\n\t\t\t\t\t\t\t\tSysDocID='" + sysDocID + "' AND VoucherID = '" + voucherID + "'";
					FillDataSet(transactionData, "Cheque_Received", textCommand, sqlTransaction);
					break;
				case SysDocTypes.ChequePayment:
					textCommand = "SELECT * FROM Cheque_Issued WHERE \r\n\t\t\t\t\t\t\t\tSysDocID='" + sysDocID + "' AND VoucherID = '" + voucherID + "'";
					FillDataSet(transactionData, "Cheque_Issued", textCommand, sqlTransaction);
					break;
				case SysDocTypes.IssuedChequeReturn:
				{
					int num = -1;
					if (!transactionData.Tables["GL_Transaction"].Rows[0]["ChequeID"].IsDBNullOrEmpty())
					{
						num = int.Parse(transactionData.Tables["GL_Transaction"].Rows[0]["ChequeID"].ToString());
					}
					textCommand = "SELECT * FROM Cheque_Issued WHERE \r\n\t\t\t\t\t\t\tChequeID = " + num;
					FillDataSet(transactionData, "Cheque_Issued", textCommand, sqlTransaction);
					break;
				}
				}
				textCommand = "SELECT * FROM AP_Payment_Advice WHERE PaymentSysDocID = '" + sysDocID + "' AND PaymentVoucherID = '" + voucherID + "'";
				FillDataSet(transactionData, "AP_Payment_Advice", textCommand, sqlTransaction);
				textCommand = "SELECT BFD.*,Cur.RateType FROM Bank_Fee_Details BFD LEFT OUTER JOIN Currency Cur ON BFD.CurrencyID = Cur.CurrencyID WHERE \r\n\t\t\t\t\t\t\t\tGLTransactionSysDocID='" + sysDocID + "' AND GLTransactionVoucherID = '" + voucherID + "'";
				FillDataSet(transactionData, "Bank_Fee_Details", textCommand, sqlTransaction);
				textCommand = "SELECT GPD.* FROM General_Payment_Detail GPD  WHERE SysDocID='" + sysDocID + "' AND VoucherID = '" + voucherID + "'";
				FillDataSet(transactionData, "General_Payment_Detail", textCommand, sqlTransaction);
				textCommand = "SELECT * FROM   Tax_Detail\r\n\t\t\t\t\t\tWHERE VoucherID='" + voucherID + "' AND SysDocID='" + sysDocID + "'";
				FillDataSet(transactionData, "Tax_Detail", textCommand);
				return transactionData;
			}
			catch
			{
				throw;
			}
		}

		public TransactionData GetChequeDepositTransactionByID(string sysDocID, string voucherID)
		{
			try
			{
				TransactionData transactionData = new TransactionData();
				string textCommand = "SELECT * FROM GL_Transaction WHERE VoucherID='" + voucherID + "' AND SysDocID='" + sysDocID + "'";
				FillDataSet(transactionData, "GL_Transaction", textCommand);
				if (transactionData == null || transactionData.Tables.Count == 0 || transactionData.Tables["GL_Transaction"].Rows.Count == 0)
				{
					return null;
				}
				transactionData.Tables["GL_Transaction"].Rows[0]["TransactionID"].ToString();
				textCommand = "SELECT  'True' AS 'D', TD.ChequeID,TD.SysDocID,TD.VoucherID,TD.CheckDate AS [Chq Date],SendDate,TD.CheckNumber [Chq #],TD.BankID,CQR.Reference,\r\n\t\t\t\t\t\t\tCQR.PayeeID,CQR.PayeeType,CQR.PayeeAccountID,CQR.PDCAccountID,Bank.BankName [Bank Name], DiscountBankAccountID,\r\n\t\t\t\t\t\t\t\r\n\t\t\t\t\t\t\tCASE CQR.PayeeType WHEN 'C' THEN Customer.CustomerName\r\n\t\t\t\t\t\t\tWHEN 'V' THEN Vendor.VendorName\r\n\t\t\t\t\t\t\tWHEN 'A' THEN Account.AccountName\r\n\t\t\t\t\t\t\tWHEN 'E' THEN Employee.FirstName + ' ' + Employee.LastName END AS [Payee Name] ,\r\n\t\t\t\t\t\t\tISNULL(TR.CurrencyID,(SELECT CurrencyID FROM Currency WHERE Currency.IsBase='True')) AS Currency,ISNULL(TD.AmountFC,TD.Amount) AS Amount, CQR.Status\r\n\t\t\t\t\t\t\tFROM         Transaction_Details TD INNER JOIN Bank ON TD.BankID=Bank.BankID\r\n\t\t\t\t\t\t\tINNER JOIN Cheque_Received CQR ON CQR.ChequeID = TD.ChequeID\r\n\t\t\t\t\t\t\tLEFT OUTER JOIN GL_Transaction TR ON TD.VoucherID=TR.VoucherID AND TD.SysDocID=TR.SysDocID\r\n\t\t\t\t\t\t\tLEFT OUTER JOIN Customer ON Customer.CustomerID=CQR.PayeeID \r\n\t\t\t\t\t\t\tLEFT OUTER JOIN Vendor ON Vendor.VendorID=CQR.PayeeID\r\n\t\t\t\t\t\t\tLEFT Outer JOIN Employee ON Employee.EmployeeID=CQR.PayeeID\r\n\t\t\t\t\t\t\tLEFT Outer JOIN Account ON Account.AccountID=CQR.PayeeID\r\n\t\t\t\t\t\t\tWHERE TD.VoucherID='" + voucherID + "' AND TD.SysDocID='" + sysDocID + "'";
				FillDataSet(transactionData, "Cheque_Received", textCommand);
				return transactionData;
			}
			catch
			{
				throw;
			}
		}

		public TransactionData GetIssuedChequeClearanceTransactionByID(string sysDocID, string voucherID)
		{
			try
			{
				TransactionData transactionData = new TransactionData();
				string textCommand = "SELECT * FROM GL_Transaction WHERE VoucherID='" + voucherID + "' AND SysDocID='" + sysDocID + "'";
				FillDataSet(transactionData, "GL_Transaction", textCommand);
				if (transactionData == null || transactionData.Tables.Count == 0 || transactionData.Tables["GL_Transaction"].Rows.Count == 0)
				{
					return null;
				}
				transactionData.Tables["GL_Transaction"].Rows[0]["TransactionID"].ToString();
				textCommand = "SELECT     'True' AS 'D',TD.CheckDate AS [Chq Date],TD.CheckNumber [Chq #],Bank.BankName [Bank Name],\r\n\t\t\t\t\t\t\t\r\n\t\t\t\t\t\t\tCASE TD.PayeeType WHEN 'C' THEN Customer.CustomerName\r\n\t\t\t\t\t\t\tWHEN 'V' THEN Vendor.VendorName\r\n\t\t\t\t\t\t\tWHEN 'A' THEN Account.AccountName\r\n\t\t\t\t\t\t\tWHEN 'E' THEN Employee.FirstName + ' ' + Employee.LastName END AS [Payee Name] ,\r\n\t\t\t\t\t\t\tISNULL(TR.CurrencyID,(SELECT CurrencyID FROM Currency WHERE Currency.IsBase='True')) AS Currency,ISNULL(TD.AmountFC,TD.Amount) AS Amount\r\n\t\t\t\t\t\t\tFROM         Transaction_Details TD INNER JOIN Bank ON TD.BankID=Bank.BankID\r\n\t\t\t\t\t\t\tLEFT OUTER JOIN GL_Transaction TR ON TD.VoucherID=TR.VoucherID AND TD.SysDocID=TR.SysDocID\r\n\t\t\t\t\t\t\tLEFT OUTER JOIN Customer ON Customer.CustomerID=TD.PayeeID \r\n\t\t\t\t\t\t\tLEFT OUTER JOIN Vendor ON Vendor.VendorID=TD.PayeeID\r\n\t\t\t\t\t\t\tLEFT Outer JOIN Employee ON Employee.EmployeeID=TD.PayeeID\r\n\t\t\t\t\t\t\tLEFT Outer JOIN Account ON Account.AccountID=TD.PayeeID\r\n\t\t\t\t\t\t\tWHERE TD.VoucherID='" + voucherID + "' AND TD.SysDocID='" + sysDocID + "'";
				transactionData.Tables.Remove("Transaction_Details");
				FillDataSet(transactionData, "Transaction_Details", textCommand);
				textCommand = "SELECT 'True' AS C,SysDocID,VoucherID, ChequeDate AS [Chq Date],ChequeNumber AS [Cheque #], CurrencyID AS Currency, BankID AS Bank,PayeeID AS Payee, ISNULL(AmountFC,Amount) AS Amount FROM Cheque_Issued\r\n\t\t\t\t\t\t\tWHERE ClearanceVoucherID='" + voucherID + "' AND ClearanceSysDocID='" + sysDocID + "'";
				FillDataSet(transactionData, "Cheque_Issued", textCommand);
				return transactionData;
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
				string textCommand = "SELECT TransactionID,SysDocID,VoucherID,CostCenterID,PayeeType,PayeeID,GLT.RegisterID,R.RegisterName, SecondRegisterID,\r\n                                (SELECT Bank.BankName FROM Cheque_Received CR LEFT JOIN Bank ON Bank.BankID= CR.BankID WHERE CR.ChequeID=GLT.ChequeID) AS BankName,\r\n\t\t\t\t\t\t\t\t(CASE PayeeType\r\n\t\t\t\t\t\t\t\tWHEN 'C' THEN Customer.CustomerName\r\n\t\t\t\t\t\t\t\tWHEN 'V' THEN Vendor.VendorName\r\n\t\t\t\t\t\t\t\tWHEN 'E' THEN Emp2.FirstName + ' ' + Emp2.LastName\r\n\t\t\t\t\t\t\t\tELSE Account.AccountName END) AS PayeeName,\r\n\t\t\t\t\t\t\t\tISNULL(AmountFC,Amount) AS Amount,TransactionDate,IsVoid,CurrencyRate,GLType,ChequebookID,CheckNumber,ChequeID,\r\n\t\t\t\t\t\t\t\tCheckDate,Reference,FirstAccountID,SecondAccountID,GLT.EmployeeID,Emp.FirstName + ' ' + Emp.LastName as EmployeeName,GLT.Description,\r\n\t\t\t\t\t\t\t\tISNULL(GLT.CurrencyID,(SELECT CurrencyID FROM Currency WHERE IsBase='True')) AS CurrencyID ,\r\n\t\t\t\t\t\t\t\tISNULL(FirstAccountID,GLT.RegisterID) AS TransferFromID,ISNULL(SecondAccountID,SecondRegisterID) AS TransferToID,\r\n\t\t\t\t\t\t\t\tTAcc.AccountName AS TransferFromName,TAcc2.AccountName AS TransferToName,\r\n                                GLT.DateCreated,GLT.DateUpdated,GLT.CreatedBy,GLT.UpdatedBy,RCR.ReasonName, GLT.ExpCode, GLT.ExpAmount, GLT.ExpPercent,GLT.TaxAmount, \r\n                                EX.ExpenseName,Vendor.TaxIDNumber as VTaxIDNo,Customer.TaxIDNumber as CTaxIDNo,(SELECT AddressPrintFormat FROM Customer_Address C WHERE C.CustomerID=PayeeID AND PayeeType ='C'  And AddressID='PRIMARY')AS [Customer Address]\r\n\t\t\t\t\t\t\t\t,(SELECT AddressPrintFormat FROM Vendor_Address V WHERE V.VendorId=PayeeID AND PayeeType ='V'  And AddressID='PRIMARY')AS [Vendor Address]\r\n\t\t\t\t\t\t\t\tFROM GL_Transaction GLT LEFT OUTER JOIN Employee Emp ON Emp.EmployeeID=GLT.EmployeeID\r\n\t\t\t\t\t\t\t\tLEFT OUTER JOIN Account ON GLT.PayeeID=Account.AccountID \r\n\t\t\t\t\t\t\t\tLEFt OUTER JOIN Customer ON GLT.PayeeID=Customer.CustomerID \r\n\t\t\t\t\t\t\t\tLEFt OUTER JOIN Vendor ON GLT.PayeeID=Vendor.VendorID \r\n\t\t\t\t\t\t\t\tLEFt OUTER JOIN Employee Emp2 ON GLT.PayeeID=Emp2.EmployeeID\r\n\t\t\t\t\t\t\t\tLEFt OUTER JOIN Account TAcc ON GLT.FirstAccountID=TAcc.AccountID\r\n\t\t\t\t\t\t\t\tLEFt OUTER JOIN Account TAcc2 ON GLT.SecondAccountID=TAcc2.AccountID\r\n                                LEFT OUTER JOIN Returned_Cheque_Reason RCR ON GLT.ReasonID=RCR.ReasonID\r\n\t\t\t\t\t\t\t\tLEFT JOIN Expense_Code EX ON GLT.ExpCode=EX.ExpenseID\r\n\t\t\t\t\t\t\t\tLEFT OUTER JOIN Register R ON GLT.RegisterID=R.RegisterID\r\n\t\t\t\t\t\t\t\tWHERE SysDocID = '" + sysDocID + "' AND VoucherID IN (" + text + ")";
				FillDataSet(dataSet, "GL_Transaction", textCommand);
				if (dataSet == null || dataSet.Tables.Count == 0 || dataSet.Tables["GL_Transaction"].Rows.Count == 0)
				{
					return null;
				}
				dataSet.Tables[0].Rows[0]["TransactionID"].ToString();
				textCommand = "SELECT TD.*,Chequebook.BankID,Bank.BankName,Vendor.TaxIDNumber as VTaxIDNo,Customer.TaxIDNumber as CTaxIDNo,\r\n                                   (CASE PayeeType\r\n                                    WHEN 'C' THEN Customer.CustomerName\r\n                                    WHEN 'V' THEN Vendor.VendorName\r\n                                    WHEN 'E' THEN Employee.FirstName + ' ' + Employee.LastName\r\n                                    ELSE Account.AccountName END) AS AccountName,JB.JobName,Account.Alias\r\n                                    FROM TRANSACTION_DETAILS TD LEFt OUTER JOIN \r\n                                    Account ON TD.AccountID=Account.AccountID LEFt OUTER JOIN\r\n                                    Customer ON TD.PayeeID=Customer.CustomerID LEFt OUTER JOIN\r\n                                    Vendor ON TD.PayeeID=Vendor.VendorID LEFt OUTER JOIN\r\n                                    Employee ON TD.PayeeID=Employee.EmployeeID\r\n                                    LEFT OUTER JOIN Chequebook ON Chequebook.ChequebookID=TD.ChequebookID\r\n                                    LEFT OUTER JOIN Bank ON Chequebook.BankID=Bank.BankID\r\n                                    LEFt OUTER JOIN Job JB ON TD.JobID=JB.JobID\r\n\t\t\t\t\t\tWHERE SysDocID = '" + sysDocID + "' AND VoucherID IN (" + text + ")";
				FillDataSet(dataSet, "Transaction_Details", textCommand);
				SysDocTypes systemDocumentType = new SystemDocuments(base.DBConfig).GetSystemDocumentType(sysDocID, null);
				if (systemDocumentType == SysDocTypes.IssuedChequeReturn || systemDocumentType == SysDocTypes.IssuedChequeCancellation)
				{
					int num = 0;
					if (!dataSet.Tables["GL_Transaction"].Rows[0]["ChequeID"].IsDBNullOrEmpty())
					{
						num = int.Parse(dataSet.Tables["GL_Transaction"].Rows[0]["ChequeID"].ToString());
					}
					textCommand = "SELECT ChequeID,ChequeNumber,AC.AccountName as [BankName],AC.Alias, Cheque_Issued.Status,Cheque_Issued.CurrencyID,PayeeAccountID,IsPrinted,PrintName,\r\n                            Cheque_Issued.ExchangeRate,AmountFC,BankAccountID,Cheque_Issued.PDCAccountID,ChequebookName,\r\n                            PayeeType,PayeeID,ChequeDate,Amount,\r\n                            CASE PayeeType WHEN 'C' THEN Customer.CustomerName\r\n                            WHEN 'V' THEN Vendor.VendorName\r\n                            WHEN 'A' THEN Account.AccountName\r\n                            WHEN 'E' THEN Employee.FirstName + ' ' + Employee.LastName END AS [PayeeName],\r\n                            CASE PayeeType WHEN 'C' THEN ISNULL(Customer.LegalName,ISNULL(Customer.CompanyName,Customer.CustomerName))\r\n                            WHEN 'V' THEN ISNULL(Vendor.LegalName,ISNULL(Vendor.CompanyName,Vendor.VendorName)) \r\n                            WHEN 'A' THEN Account.AccountName\r\n                            WHEN 'E' THEN Employee.FirstName + ' ' + Employee.LastName END AS [NameOnCheck],\r\n                            'A/C PAYEE ONLY' AS Stamp1,'NON NEGOTIABLE' AS Stamp2\r\n                            FROM Cheque_Issued LEFT OUTER JOIN Bank ON Bank.BankID= Cheque_Issued.BankID\r\n                            LEFT OUTER JOIN Chequebook ON Chequebook.ChequebookID=Cheque_Issued.ChequebookID\r\n                            LEFT OUTER JOIN Customer ON Customer.CustomerID=PayeeID \r\n                            LEFT OUTER JOIN Vendor ON Vendor.VendorID=PayeeID\r\n                            LEFT Outer JOIN Employee ON Employee.EmployeeID=PayeeID\r\n                            LEFT Outer JOIN Account ON Account.AccountID=PayeeID\r\n                            LEFT OUTER JOIN Account AC ON AC.AccountID = BankAccountID\r\n                            WHERE ChequeID=" + num;
					FillDataSet(dataSet, "Cheque_Issued", textCommand);
				}
				textCommand = "SELECT * FROM Bank_Fee_Details\r\n\t\t\t\t\t\tWHERE GLTransactionSysDocID = '" + sysDocID + "' AND GLTransactionVoucherID IN (" + text + ")";
				FillDataSet(dataSet, "Bank_Fee_Details", textCommand);
				textCommand = "SELECT *,(select TransactionDate from Sales_Invoice PI where PI.SysDocID=ARP.InvoiceSysDocID and PI.VoucherID=ARP.InvoiceVoucherID) as Invoice_Date,\r\n                    (select Reference from Sales_Invoice PI where PI.SysDocID=ARP.InvoiceSysDocID and PI.VoucherID=ARP.InvoiceVoucherID) as RefNo,(select Reference2 from Sales_Invoice PI where PI.SysDocID=ARP.InvoiceSysDocID and PI.VoucherID=ARP.InvoiceVoucherID) as Ref2,'' AS VendorRef FROM AR_Payment_Allocation ARP\r\n\t\t\t\t\tWHERE PaymentSysDocID = '" + sysDocID + "' AND PaymentVoucherID IN (" + text + ") UNION  SELECT *,(select TransactionDate from Purchase_Invoice PI where PI.SysDocID=APP.InvoiceSysDocID and PI.VoucherID=APP.InvoiceVoucherID) as Invoice_Date,\r\n                    (select Reference from Purchase_Invoice PI where PI.SysDocID = APP.InvoiceSysDocID and PI.VoucherID = APP.InvoiceVoucherID) as RefNo,(select Reference2 from Purchase_Invoice PI where PI.SysDocID = APP.InvoiceSysDocID and PI.VoucherID = APP.InvoiceVoucherID) as Ref2,(select VendorReferenceNo from Purchase_Invoice PI where PI.SysDocID = APP.InvoiceSysDocID and PI.VoucherID = APP.InvoiceVoucherID) as VendorRef FROM AP_Payment_Allocation APP WHERE PaymentSysDocID = '" + sysDocID + "' AND PaymentVoucherID IN (" + text + ")";
				FillDataSet(dataSet, "Customer", textCommand);
				decimal result = default(decimal);
				decimal d = default(decimal);
				foreach (DataRow row in dataSet.Tables["Bank_Fee_Details"].Rows)
				{
					decimal.TryParse(row["Amount"].ToString(), out result);
					d += result;
				}
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
				dataSet.Tables["GL_Transaction"].Columns.Add("AmountExpInWords", typeof(string));
				foreach (DataRow row2 in dataSet.Tables["GL_Transaction"].Rows)
				{
					decimal result2 = default(decimal);
					decimal result3 = default(decimal);
					decimal.TryParse(row2["Amount"].ToString(), out result2);
					decimal.TryParse(row2["TaxAmount"].ToString(), out result3);
					row2["AmountInWords"] = NumToWord.GetNumInWords(decimalPoints: new CompanyInformations(base.DBConfig).CurrencyDecimalPoints, amount: result2 + result3);
				}
				foreach (DataRow row3 in dataSet.Tables["GL_Transaction"].Rows)
				{
					decimal result4 = default(decimal);
					decimal result5 = default(decimal);
					decimal.TryParse(row3["Amount"].ToString(), out result4);
					decimal.TryParse(row3["TaxAmount"].ToString(), out result5);
					row3["AmountExpInWords"] = NumToWord.GetNumInWords(decimalPoints: new CompanyInformations(base.DBConfig).CurrencyDecimalPoints, amount: result4 + result5 + d);
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
				FillDataSet(dataSet, "Cheque_Issued", textCommand);
				return dataSet;
			}
			catch
			{
				throw;
			}
		}

		public DataSet GetExpenseList(DateTime from, DateTime to, bool showVoid, string sysDocID)
		{
			DataSet dataSet = new DataSet();
			string text = StoreConfiguration.ToSqlDateTimeString(from);
			string text2 = StoreConfiguration.ToSqlDateTimeString(to);
			string text3 = "SELECT ISNULL(GLT.IsVoid,'False') AS V,GLT.SysDocID [Doc ID],GLT.VoucherID [Doc Number],TransactionDate [Date],\r\n                            CASE SysDocType WHEN 8 THEN 'Cash' ELSE 'Cheque' END AS [Type],GLT.CostCenterID [C.C.],GLT.PayeeID [Payee Code],\r\n                            (CASE GLT.PayeeType\r\n                            WHEN 'C' THEN Customer.CustomerName\r\n                            WHEN 'V' THEN Vendor.VendorName\r\n                            WHEN 'E' THEN Employee.FirstName\r\n                            ELSE Account.AccountName END) AS [Payee Name],GLT.Reference [Ref],\r\n                            RegisterID [Reg],GLT.Description [Desc],\r\n                            ISNULL(GLT.CurrencyID,(SELECT CurrencyID FROM Currency WHERE IsBase=1)) AS [Cur] ,ISNULL(GLt.AmountFC +ISNULL(GLT.TaxAmount,0),GLT.Amount+ ISNULL(GLT.TaxAmount,0)) AS [Doc Amount],TD.Amount,TD.AttributeID1 AS [AttributeID1ID], TD.AttributeID2 AS [AttributeID2ID], PropertyName AS [AttributeID1Name], PropertyUnitName AS [AttributeID2Name]\r\n                            FROM GL_Transaction GLT\r\n                            LEFT OUTER JOIN Transaction_Details TD ON GLT.SysDocID=TD.SysDocID AND GLT.VoucherID=TD.VoucherID\r\n                            LEFT OUTER JOIN Property P ON P.PropertyID=TD.AttributeID1\r\n                            LEFT OUTER JOIN Property_Unit PU ON PU.PropertyUnitID=TD.AttributeID2\r\n\t\t\t\t\t\t\t \r\n                            LEFt OUTER JOIN Account ON GLT.PayeeID=Account.AccountID LEFt OUTER JOIN\r\n                            Customer ON GLT.PayeeID=Customer.CustomerID LEFt OUTER JOIN\r\n                            Vendor ON GLT.PayeeID=Vendor.VendorID LEFt OUTER JOIN\r\n                            Employee ON GLT.PayeeID=Employee.EmployeeID\r\n                            INNER JOIN System_Document SD ON SD.SysDocID=GLT.SysDocID\r\n\t\t\t\t\t\t\tWHERE SysDocType IN (8,9) AND Isnull(IsSecondForm,0)=0 ";
			if (from != DateTime.MinValue)
			{
				text3 = text3 + " AND TransactionDate Between '" + text + "' AND '" + text2 + "'";
			}
			if (!showVoid)
			{
				text3 += " AND ISNULL(GLT.IsVoid,'False')='False'";
			}
			if (sysDocID != "")
			{
				text3 = text3 + " AND SD.SysDocID = '" + sysDocID + "'";
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
			string text3 = "SELECT ISNULL(GLT.IsVoid,'False') AS V,GLT.SysDocID [Doc ID],GLT.VoucherID [Doc Number],TransactionDate [Date],\r\n\t\t\t\t\t\t\tGLT.CostCenterID [C.C.],GLT.PayeeID [Payee Code],\r\n\t\t\t\t\t\t\t(CASE GLT.PayeeType\r\n\t\t\t\t\t\t\t\t\t\t\t\t\tWHEN 'C' THEN Customer.CustomerName\r\n\t\t\t\t\t\t\t\t\t\t\t\t\tWHEN 'V' THEN Vendor.VendorName\r\n\t\t\t\t\t\t\t\t\t\t\t\t\tWHEN 'E' THEN Employee.FirstName\r\n\t\t\t\t\t\t\t\t\t\t\t\t\tELSE Account.AccountName END) AS [Payee Name],GLT.Reference [Ref],\r\n\t\t\t\t\t\t\tRegisterID [Reg],GLT.Description [Desc],\r\n\t\t\t\t\t\t\tISNULL(GLT.CurrencyID,(SELECT CurrencyID FROM Currency WHERE IsBase=1)) AS [Cur] ,ISNULL(GLT.AmountFC,GLT.Amount) AS [Amount],TD.AttributeID1 AS [AttributeID1ID], TD.AttributeID2 AS [AttributeID2ID], PropertyName AS [AttributeID1Name], PropertyUnitName AS [AttributeID2Name]\r\n\t\t\t\t\t\t\tFROM GL_Transaction GLT\r\n\t\t\t\t\t\t\tLEFT OUTER JOIN Transaction_Details TD ON GLT.SysDocID=TD.SysDocID AND GLT.VoucherID=TD.VoucherID\r\n\t\t\t\t\t\t\tLEFT OUTER JOIN Property P ON P.PropertyID=TD.AttributeID1\r\n\t\t\t\t\t\t\tLEFT OUTER JOIN Property_Unit PU ON PU.PropertyUnitID=TD.AttributeID2\r\n\t\t\t\t\t\t\t \r\n\t\t\t\t\t\t\tLEFt OUTER JOIN Account ON GLT.PayeeID=Account.AccountID LEFt OUTER JOIN\r\n\t\t\t\t\t\t\t\t\t\t\t\t\tCustomer ON GLT.PayeeID=Customer.CustomerID LEFt OUTER JOIN\r\n\t\t\t\t\t\t\t\t\t\t\t\t\tVendor ON GLT.PayeeID=Vendor.VendorID LEFt OUTER JOIN\r\n\t\t\t\t\t\t\t\t\t\t\t\t\tEmployee ON GLT.PayeeID=Employee.EmployeeID\r\n\t\t\t\t\t\t\tINNER JOIN System_Document SD ON SD.SysDocID=GLT.SysDocID\r\n\t\t\t\t\t\t\tWHERE SysDocType IN (10) ";
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
			string text3 = "SELECT DISTINCT GLT.VoucherID [Doc Number], ISNULL(GLT.IsVoid,'False') AS V,GLT.SysDocID [Doc ID],TransactionDate [Date],\r\n                            CASE SysDocType WHEN 3 THEN 'Cash'  WHEN 2 THEN 'Cheque'  ELSE 'TT' END AS [Type],GLT.CostCenterID [C.C.],GLT.PayeeID [Payee Code],\r\n                            (CASE GLT.PayeeType\r\n                            WHEN 'C' THEN Customer.CustomerName\r\n                            WHEN 'V' THEN Vendor.VendorName\r\n                            WHEN 'E' THEN Employee.FirstName\r\n                            ELSE Account.AccountName END) AS [Payee Name],GLT.Reference [Ref],\r\n                            RegisterID [Reg],GLT.Description [Desc],\r\n                            ISNULL(GLT.CurrencyID,(SELECT CurrencyID FROM Currency WHERE IsBase=1)) AS [Cur] ,ISNULL(GLT.AmountFC,GLT.Amount) AS [Amount], \r\n                            GLT.CheckNumber [Chq #], TD.AttributeID1 AS [AttributeID1ID], TD.AttributeID2 AS [AttributeID2ID], PropertyName AS [AttributeID1Name], PropertyUnitName AS [AttributeID2Name]\r\n                            FROM GL_Transaction GLT\r\n                            LEFT OUTER JOIN Transaction_Details TD ON GLT.SysDocID=TD.SysDocID AND GLT.VoucherID=TD.VoucherID\r\n\t                            LEFT OUTER JOIN Property P ON P.PropertyID=TD.AttributeID1\r\n                            LEFT OUTER JOIN Property_Unit PU ON PU.PropertyUnitID=TD.AttributeID2\r\n                            LEFt OUTER JOIN Account ON GLT.PayeeID=Account.AccountID LEFt OUTER JOIN\r\n                            Customer ON GLT.PayeeID=Customer.CustomerID LEFt OUTER JOIN\r\n                            Vendor ON GLT.PayeeID=Vendor.VendorID LEFt OUTER JOIN\r\n                            Employee ON GLT.PayeeID=Employee.EmployeeID\r\n                            INNER JOIN System_Document SD ON SD.SysDocID=GLT.SysDocID\r\n\t\t\t\t\t\t\tWHERE SysDocType IN (2,3,65) ";
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

		public DataSet GetReceiptVoucherMultipleList(DateTime from, DateTime to, bool showVoid)
		{
			DataSet dataSet = new DataSet();
			string text = StoreConfiguration.ToSqlDateTimeString(from);
			string text2 = StoreConfiguration.ToSqlDateTimeString(to);
			string text3 = "SELECT ISNULL(IsVoid,'False') AS V,GLT.SysDocID [Doc ID],VoucherID [Doc Number],TransactionDate [Date],\r\n\t\t\t\t\t\t\tCASE SysDocType WHEN 3 THEN 'Cash'  WHEN 2 THEN 'Cheque'  ELSE 'TT' END AS [Type],CostCenterID [C.C.],PayeeID [Payee Code],\r\n\t\t\t\t\t\t\t(CASE PayeeType\r\n\t\t\t\t\t\t\t\t\t\t\t\t\tWHEN 'C' THEN Customer.CustomerName\r\n\t\t\t\t\t\t\t\t\t\t\t\t\tWHEN 'V' THEN Vendor.VendorName\r\n\t\t\t\t\t\t\t\t\t\t\t\t\tWHEN 'E' THEN Employee.FirstName\r\n\t\t\t\t\t\t\t\t\t\t\t\t\tELSE Account.AccountName END) AS [Payee Name],Reference [Ref],\r\n\t\t\t\t\t\t\tRegisterID [Reg],GLT.Description [Desc],\r\n\t\t\t\t\t\t\tISNULL(GLT.CurrencyID,(SELECT CurrencyID FROM Currency WHERE IsBase=1)) AS [Cur] ,ISNULL(AmountFC,Amount) AS [Amount], CheckNumber [Chq #]\r\n\t\t\t\t\t\t\tFROM GL_Transaction GLT\r\n\t\t\t\t\t\t\t \r\n\t\t\t\t\t\t\tLEFt OUTER JOIN Account ON GLT.PayeeID=Account.AccountID LEFt OUTER JOIN\r\n\t\t\t\t\t\t\t\t\t\t\t\t\tCustomer ON GLT.PayeeID=Customer.CustomerID LEFt OUTER JOIN\r\n\t\t\t\t\t\t\t\t\t\t\t\t\tVendor ON GLT.PayeeID=Vendor.VendorID LEFt OUTER JOIN\r\n\t\t\t\t\t\t\t\t\t\t\t\t\tEmployee ON GLT.PayeeID=Employee.EmployeeID\r\n\t\t\t\t\t\t\tINNER JOIN System_Document SD ON SD.SysDocID=GLT.SysDocID\r\n\t\t\t\t\t\t\tWHERE SysDocType IN (66) ";
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
				b = 5;
				break;
			}
			string text3 = "SELECT ISNULL(GLT.IsVoid,'False') AS V,GLT.SysDocID [Doc ID],GLT.VoucherID [Doc Number],TransactionDate [Date],\r\n                    CASE SysDocType WHEN 4 THEN 'Cash' WHEN 5 THEN 'Cheque' WHEN 8 THEN 'Cash' ELSE 'TT' END AS [Type],GLT.CostCenterID [C.C.],GLT.PayeeID [Payee Code], CI.ChequeNumber [ChequeNumber],CI.ChequeDate[ChequeDate],\r\n                    (CASE GLT.PayeeType\r\n                    WHEN 'C' THEN Customer.CustomerName\r\n                    WHEN 'V' THEN Vendor.VendorName\r\n                    WHEN 'E' THEN Employee.FirstName\r\n                    ELSE Account.AccountName END) AS [Payee Name],GLT.Reference [Ref],\r\n                    RegisterID [Reg],td.Description [Desc],\r\n                    ISNULL(GLT.CurrencyID,(SELECT CurrencyID FROM Currency WHERE IsBase=1)) AS [Cur] ,  td.amount as [Cheque Amount] ,   ISNULL(GLT.AmountFC,GLT.Amount) AS [Total Amount],\r\n                    TD.AttributeID1 AS [AttributeID1ID], TD.AttributeID2 AS [AttributeID2ID], PropertyName AS [AttributeID1Name], PropertyUnitName AS [AttributeID2Name]\r\n                    FROM Transaction_Details TD \r\n                   LEFT OUTER JOIN GL_Transaction GLT ON GLT.SysDocID=TD.SysDocID AND GLT.VoucherID=TD.VoucherID\r\n                    LEFT OUTER JOIN Property P ON P.PropertyID=TD.AttributeID1\r\n                    LEFT OUTER JOIN Property_Unit PU ON PU.PropertyUnitID=TD.AttributeID2\t\t\t\t\t\t\t \r\n                    LEFt OUTER JOIN Account ON GLT.PayeeID=Account.AccountID LEFt OUTER JOIN\r\n                    Customer ON GLT.PayeeID=Customer.CustomerID LEFt OUTER JOIN\r\n                    Vendor ON GLT.PayeeID=Vendor.VendorID LEFt OUTER JOIN\r\n                    Employee ON GLT.PayeeID=Employee.EmployeeID LEFT OUTER JOIN \t\t\t\t\t\t\t\t\t\t\t\t\t\t\r\n                   Cheque_Issued CI ON CI.SysDocID=GLT.SysDocID and CI.VoucherID=GLT.VoucherID  and CI.ChequeNumber  = td.CheckNumber \r\n                      LEFT OUTER JOIN \t System_Document SD ON SD.SysDocID=GLT.SysDocID";
			text3 = ((b != 4) ? (text3 + " WHERE SysDocType IN(" + b + ")") : (text3 + " WHERE SysDocType IN(" + b + ",8) AND Isnull(IsSecondform,0)=1"));
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

		public DataSet GetChequeReceiptMultipleList(DateTime from, DateTime to, bool showVoid)
		{
			DataSet dataSet = new DataSet();
			string text = StoreConfiguration.ToSqlDateTimeString(from);
			string text2 = StoreConfiguration.ToSqlDateTimeString(to);
			string text3 = "SELECT ISNULL(IsVoid,'False') AS V,GLT.SysDocID [Doc ID],VoucherID [Doc Number],TransactionDate [Date],\r\n\t\t\t\t\t\t\tCASE SysDocType WHEN 3 THEN 'Cash'  WHEN 2 THEN 'Cheque'  ELSE 'TT' END AS [Type],CostCenterID [C.C.],PayeeID [Payee Code],\r\n\t\t\t\t\t\t\t(CASE PayeeType\r\n\t\t\t\t\t\t\t\t\t\t\t\t\tWHEN 'C' THEN Customer.CustomerName\r\n\t\t\t\t\t\t\t\t\t\t\t\t\tWHEN 'V' THEN Vendor.VendorName\r\n\t\t\t\t\t\t\t\t\t\t\t\t\tWHEN 'E' THEN Employee.FirstName\r\n\t\t\t\t\t\t\t\t\t\t\t\t\tELSE Account.AccountName END) AS [Payee Name],Reference [Ref],\r\n\t\t\t\t\t\t\tRegisterID [Reg],GLT.Description [Desc],\r\n\t\t\t\t\t\t\tISNULL(GLT.CurrencyID,(SELECT CurrencyID FROM Currency WHERE IsBase=1)) AS [Cur] ,ISNULL(AmountFC,Amount) AS [Amount], CheckNumber [Chq #]\r\n\t\t\t\t\t\t\tFROM GL_Transaction GLT\r\n\t\t\t\t\t\t\t \r\n\t\t\t\t\t\t\tLEFt OUTER JOIN Account ON GLT.PayeeID=Account.AccountID LEFt OUTER JOIN\r\n\t\t\t\t\t\t\t\t\t\t\t\t\tCustomer ON GLT.PayeeID=Customer.CustomerID LEFt OUTER JOIN\r\n\t\t\t\t\t\t\t\t\t\t\t\t\tVendor ON GLT.PayeeID=Vendor.VendorID LEFt OUTER JOIN\r\n\t\t\t\t\t\t\t\t\t\t\t\t\tEmployee ON GLT.PayeeID=Employee.EmployeeID\r\n\t\t\t\t\t\t\tINNER JOIN System_Document SD ON SD.SysDocID=GLT.SysDocID\r\n\t\t\t\t\t\t\tWHERE SysDocType IN (253) ";
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
	}
}
