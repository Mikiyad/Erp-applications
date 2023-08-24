using Micromind.Common;
using Micromind.Common.Data;
using Micromind.Common.Libraries;
using System;
using System.Data;
using System.Data.SqlClient;

namespace Micromind.Data
{
	public sealed class BankFacilityPayments : StoreObject
	{
		private const string SYSDOCID_PARM = "@SysDocID";

		private const string REGISTERID_PARM = "@RegisterID";

		private const string VOUCHERID_PARM = "@VoucherID";

		private const string TRANSACTIONSYSDOCID_PARM = "@TransactionSysDocID";

		private const string TRANSACTIONVOUCHERID_PARM = "@TranactionVoucherID";

		private const string AMOUNT_PARM = "@Amount";

		private const string AMOUNTFC_PARM = "@AmountFC";

		private const string TRANSACTIONDATE_PARM = "@PaymentDate";

		private const string CURRENCYID_PARM = "@CurrencyID";

		private const string CURRENCYRATE_PARM = "@CurrencyRate";

		private const string JOURNALID_PARM = "@JournalID";

		private const string REFERENCE_PARM = "@Reference";

		private const string DESCRIPTION_PARM = "@Description";

		private const string GLTYPE_PARM = "@GLType";

		private const string STATUS_PARM = "@PaymentStatus";

		private const string GLTYPEID_PARM = "@GLTYPEID";

		private const string PAYFROMACCOUNTID_PARM = "@PayFromAccountID";

		private const string PAYTOACCOUNTID_PARM = "@PayToAccountID";

		private const string EMPLOYEEID_PARM = "@EmployeeID";

		private const string BANKFACILITYID_PARM = "@BankFacilityID";

		private const string FACILITYTYPE_PARM = "@FacilityType";

		private const string BANKFACILITYPAYMENT_TABLE = "Bank_Facility_Payment";

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

		private const string BANKFACILITYPAYMENTDETAILS_TABLE = "Bank_Facility_Payment_Details";

		private const string DUEDATE_PARM = "@DueDate";

		private const string BANKFEEDETAILS_TABLE = "Bank_Fee_Details";

		private const string GLTRANSACTIONSYSDOCID_PARM = "@GLPaymentSysDocID";

		private const string GLTRANSACTIONVOUCHERID_PARM = "@GLPaymentVoucherID";

		private const string BANKACCOUNTID_PARM = "@BankAccountID";

		private const string EXPENSEACCOUNTID_PARM = "@ExpenseAccountID";

		private const string BANKFEEID_PARM = "@BankFeeID";

		private const string TRANSACTIONNUMBER_PARM = "@PaymentNumber";

		private const string PARTYONEACCOUNTID_PARM = "@PartyOneAccountID";

		private const string PARTYTWOACCOUNTID_PARM = "@PartyTwoAccountID";

		private const string TAXGROUPID_PARM = "@TaxGroupID";

		private const string TAXAMOUNT_PARM = "@TaxAmount";

		private const string TAXOPTION_PARM = "@TaxOption";

		private static object syncRoot = new object();

		public BankFacilityPayments(Config config)
			: base(config)
		{
		}

		private string GetInsertUpdatePaymentText(bool isUpdate)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddInsertUpdateParameters("Bank_Facility_Payment", new FieldValue("SysDocID", "@SysDocID", isUpdateConditionField: true), new FieldValue("VoucherID", "@VoucherID", isUpdateConditionField: true), new FieldValue("TransactionSysDocID", "@TransactionSysDocID"), new FieldValue("TransactionVoucherID", "@TranactionVoucherID"), new FieldValue("CostCenterID", "@CostCenterID"), new FieldValue("BankFacilityID", "@BankFacilityID"), new FieldValue("FacilityType", "@FacilityType"), new FieldValue("TransactionDate", "@PaymentDate"), new FieldValue("Amount", "@Amount"), new FieldValue("AmountFC", "@AmountFC"), new FieldValue("CurrencyID", "@CurrencyID"), new FieldValue("CurrencyRate", "@CurrencyRate"), new FieldValue("Reference", "@Reference"), new FieldValue("Description", "@Description"), new FieldValue("PayFromAccountID", "@PayFromAccountID"), new FieldValue("PayToAccountID", "@PayToAccountID"), new FieldValue("TransactionStatus", "@PaymentStatus"));
			if (isUpdate)
			{
				sqlBuilder.AddInsertUpdateParameters("Bank_Facility_Payment", new FieldValue("DateUpdated", "@DateUpdated", isUpdateConditionField: true, checkForNullValue: true));
			}
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
			parameters.Add("@CostCenterID", SqlDbType.NVarChar);
			parameters.Add("@SysDocID", SqlDbType.NVarChar);
			parameters.Add("@TransactionSysDocID", SqlDbType.NVarChar);
			parameters.Add("@TranactionVoucherID", SqlDbType.NVarChar);
			parameters.Add("@BankFacilityID", SqlDbType.NVarChar);
			parameters.Add("@FacilityType", SqlDbType.Int);
			parameters.Add("@VoucherID", SqlDbType.NVarChar);
			parameters.Add("@PaymentDate", SqlDbType.DateTime);
			parameters.Add("@Amount", SqlDbType.Money);
			parameters.Add("@AmountFC", SqlDbType.Money);
			parameters.Add("@CurrencyID", SqlDbType.NVarChar);
			parameters.Add("@CurrencyRate", SqlDbType.SmallMoney);
			parameters.Add("@Reference", SqlDbType.NVarChar);
			parameters.Add("@Description", SqlDbType.NVarChar);
			parameters.Add("@PaymentStatus", SqlDbType.TinyInt);
			parameters.Add("@PayFromAccountID", SqlDbType.NVarChar);
			parameters.Add("@PayToAccountID", SqlDbType.NVarChar);
			parameters["@CostCenterID"].SourceColumn = "CostCenterID";
			parameters["@SysDocID"].SourceColumn = "SysDocID";
			parameters["@TransactionSysDocID"].SourceColumn = "TransactionSysDocID";
			parameters["@TranactionVoucherID"].SourceColumn = "TransactionVoucherID";
			parameters["@BankFacilityID"].SourceColumn = "BankFacilityID";
			parameters["@FacilityType"].SourceColumn = "FacilityType";
			parameters["@VoucherID"].SourceColumn = "VoucherID";
			parameters["@PaymentDate"].SourceColumn = "TransactionDate";
			parameters["@Amount"].SourceColumn = "Amount";
			parameters["@AmountFC"].SourceColumn = "AmountFC";
			parameters["@CurrencyID"].SourceColumn = "CurrencyID";
			parameters["@CurrencyRate"].SourceColumn = "CurrencyRate";
			parameters["@Reference"].SourceColumn = "Reference";
			parameters["@Description"].SourceColumn = "Description";
			parameters["@PaymentStatus"].SourceColumn = "TransactionStatus";
			parameters["@PayFromAccountID"].SourceColumn = "PayFromAccountID";
			parameters["@PayToAccountID"].SourceColumn = "PayToAccountID";
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

		private string GetInsertUpdatePaymentDetailsText(bool isUpdate)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddInsertUpdateParameters("Bank_Facility_Payment_Details", new FieldValue("SysDocID", "@SysDocID"), new FieldValue("VoucherID", "@VoucherID"), new FieldValue("BankID", "@BankID"), new FieldValue("Description", "@Description"), new FieldValue("Reference", "@Reference"), new FieldValue("Amount", "@Amount"), new FieldValue("AmountFC", "@AmountFC"), new FieldValue("AccountID", "@AccountID"), new FieldValue("CostCenterID", "@CostCenterID"), new FieldValue("AnalysisID", "@AnalysisID"), new FieldValue("RowIndex", "@RowIndex"), new FieldValue("PayeeID", "@PayeeID"), new FieldValue("DueDate", "@DueDate"), new FieldValue("PayeeType", "@PayeeType"));
			if (isUpdate)
			{
				return sqlBuilder.GetUpdateExpression();
			}
			return sqlBuilder.GetInsertExpression();
		}

		private SqlCommand GetInsertUpdatePaymentDetailsCommand(bool isUpdate)
		{
			SqlParameterCollection parameters;
			if (isUpdate)
			{
				if (updateCommand != null)
				{
					updateCommand = null;
				}
				updateCommand = new SqlCommand(GetInsertUpdatePaymentDetailsText(isUpdate: true), base.DBConfig.Connection);
				updateCommand.CommandType = CommandType.Text;
				parameters = updateCommand.Parameters;
			}
			else
			{
				if (insertCommand != null)
				{
					insertCommand = null;
				}
				insertCommand = new SqlCommand(GetInsertUpdatePaymentDetailsText(isUpdate: false), base.DBConfig.Connection);
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
			parameters["@DueDate"].SourceColumn = "DueDate";
			parameters["@PayeeType"].SourceColumn = "PayeeType";
			if (isUpdate)
			{
				return updateCommand;
			}
			return insertCommand;
		}

		private string GetInsertUpdatePaymentBankFeesText(bool isUpdate)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddInsertUpdateParameters("Bank_Fee_Details", new FieldValue("GLTransactionSysDocID", "@GLPaymentSysDocID"), new FieldValue("GLTransactionVoucherID", "@GLPaymentVoucherID"), new FieldValue("RowIndex", "@RowIndex"), new FieldValue("BankFacilityID", "@BankFacilityID"), new FieldValue("Description", "@Description"), new FieldValue("Reference", "@Reference"), new FieldValue("BankAccountID", "@BankAccountID"), new FieldValue("ExpenseAccountID", "@ExpenseAccountID"), new FieldValue("BankFeeID", "@BankFeeID"), new FieldValue("TaxOption", "@TaxOption"), new FieldValue("TaxAmount", "@TaxAmount"), new FieldValue("TaxGroupID", "@TaxGroupID"), new FieldValue("Amount", "@Amount"), new FieldValue("AmountFC", "@AmountFC"), new FieldValue("CurrencyID", "@CurrencyID"), new FieldValue("CurrencyRate", "@CurrencyRate"));
			if (isUpdate)
			{
				return sqlBuilder.GetUpdateExpression();
			}
			return sqlBuilder.GetInsertExpression();
		}

		private SqlCommand GetInsertUpdatePaymentBankFeesCommand(bool isUpdate)
		{
			SqlParameterCollection parameters;
			if (isUpdate)
			{
				if (updateCommand != null)
				{
					updateCommand = null;
				}
				updateCommand = new SqlCommand(GetInsertUpdatePaymentBankFeesText(isUpdate: true), base.DBConfig.Connection);
				updateCommand.CommandType = CommandType.Text;
				parameters = updateCommand.Parameters;
			}
			else
			{
				if (insertCommand != null)
				{
					insertCommand = null;
				}
				insertCommand = new SqlCommand(GetInsertUpdatePaymentBankFeesText(isUpdate: false), base.DBConfig.Connection);
				insertCommand.CommandType = CommandType.Text;
				parameters = insertCommand.Parameters;
			}
			parameters.Add("@GLPaymentSysDocID", SqlDbType.NVarChar);
			parameters.Add("@GLPaymentVoucherID", SqlDbType.NVarChar);
			parameters.Add("@RowIndex", SqlDbType.Int);
			parameters.Add("@BankFacilityID", SqlDbType.NVarChar);
			parameters.Add("@Description", SqlDbType.NVarChar);
			parameters.Add("@Reference", SqlDbType.NVarChar);
			parameters.Add("@BankAccountID", SqlDbType.NVarChar);
			parameters.Add("@ExpenseAccountID", SqlDbType.NVarChar);
			parameters.Add("@BankFeeID", SqlDbType.NVarChar);
			parameters.Add("@Amount", SqlDbType.Money);
			parameters.Add("@AmountFC", SqlDbType.Money);
			parameters.Add("@CurrencyID", SqlDbType.NVarChar);
			parameters.Add("@CurrencyRate", SqlDbType.Money);
			parameters.Add("@TaxGroupID", SqlDbType.NVarChar);
			parameters.Add("@TaxAmount", SqlDbType.Decimal);
			parameters.Add("@TaxOption", SqlDbType.TinyInt);
			parameters["@GLPaymentSysDocID"].SourceColumn = "GLTransactionSysDocID";
			parameters["@GLPaymentVoucherID"].SourceColumn = "GLTransactionVoucherID";
			parameters["@RowIndex"].SourceColumn = "RowIndex";
			parameters["@BankFacilityID"].SourceColumn = "BankFacilityID";
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

		public bool InsertUpdatePayment(BankFacilityPaymentData paymentData, bool isUpdate)
		{
			bool flag = true;
			SqlCommand insertUpdatePaymentCommand = GetInsertUpdatePaymentCommand(isUpdate);
			try
			{
				DataRow dataRow = paymentData.BankFacilityPaymentTable.Rows[0];
				int currencyDecimalPoints = new CompanyInformations(base.DBConfig).CurrencyDecimalPoints;
				SysDocTypes sysDocTypes = (SysDocTypes)byte.Parse(dataRow["SysDocType"].ToString());
				SqlTransaction sqlTransaction = base.DBConfig.StartNewTransaction();
				string text = dataRow["VoucherID"].ToString();
				string text2 = dataRow["SysDocID"].ToString();
				dataRow["RegisterID"].ToString();
				if (!isUpdate && new SystemDocuments(base.DBConfig).ExistDocumentNumber("Bank_Facility_Payment", "VoucherID", dataRow["SysDocID"].ToString(), text, sqlTransaction))
				{
					flag = false;
					throw new CompanyException("Document number already exist.", 1046);
				}
				if (dataRow["PayFromAccountID"].ToString() == "")
				{
					throw new CompanyException("AccountID is not set for the transaction.", 1028);
				}
				if (sysDocTypes == SysDocTypes.TR)
				{
					foreach (DataRow row in paymentData.BankFacilityPaymentDetailsTable.Rows)
					{
						row["SysDocID"] = dataRow["SysDocID"];
						row["VoucherID"] = dataRow["VoucherID"];
					}
				}
				if (dataRow["CurrencyID"] != DBNull.Value && dataRow["CurrencyID"].ToString() != GetBaseCurrencyID())
				{
					decimal d = decimal.Parse(dataRow["CurrencyRate"].ToString());
					decimal num = default(decimal);
					string text3 = "M";
					text3 = new Currencies(base.DBConfig).GetCurrencyRateType(dataRow["CurrencyID"].ToString());
					foreach (DataRow row2 in paymentData.BankFacilityPaymentDetailsTable.Rows)
					{
						decimal result = default(decimal);
						decimal.TryParse(row2["AmountFC"].ToString(), out result);
						if (text3 == "M")
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
				insertUpdatePaymentCommand.Transaction = sqlTransaction;
				if (!isUpdate)
				{
					if (paymentData.Tables["Bank_Facility_Payment"].Rows.Count > 0)
					{
						flag &= Insert(paymentData, "Bank_Facility_Payment", insertUpdatePaymentCommand);
					}
				}
				else
				{
					flag &= Update(paymentData, "Bank_Facility_Payment", insertUpdatePaymentCommand);
				}
				insertUpdatePaymentCommand = GetInsertUpdatePaymentDetailsCommand(isUpdate: false);
				insertUpdatePaymentCommand.Transaction = sqlTransaction;
				if (isUpdate)
				{
					flag &= DeletePaymentDetailsRows(text2, text, sqlTransaction);
				}
				if (paymentData.Tables["Bank_Facility_Payment_Details"].Rows.Count > 0)
				{
					flag &= Insert(paymentData, "Bank_Facility_Payment_Details", insertUpdatePaymentCommand);
				}
				insertUpdatePaymentCommand = GetInsertUpdatePaymentBankFeesCommand(isUpdate: false);
				insertUpdatePaymentCommand.Transaction = sqlTransaction;
				if (paymentData.Tables["Bank_Fee_Details"].Rows.Count > 0)
				{
					flag &= Insert(paymentData, "Bank_Fee_Details", insertUpdatePaymentCommand);
				}
				GLData journalData = CreateGLData(paymentData);
				flag &= new Journal(base.DBConfig).InsertUpdateJournal(journalData, isUpdate, sqlTransaction);
				int result2 = 2;
				int.TryParse(dataRow["FacilityType"].ToString(), out result2);
				if (result2 == 2)
				{
					flag &= UpdatePaidAmount(text2, text, sqlTransaction);
				}
				if (result2 == 4)
				{
					flag &= UpdatePaidBillDiscountAmount(text2, text, sqlTransaction);
				}
				if (!flag)
				{
					throw new Exception("Failed to save the transaction because of an unexpected error. Please try again.");
				}
				flag &= UpdateTableRowInsertUpdateInfo("Bank_Facility_Payment", "SysDocID", dataRow["SysDocID"].ToString(), "VoucherID", dataRow["VoucherID"].ToString(), sqlTransaction, !isUpdate);
				string entityName = "Tranaction";
				if (sysDocTypes == SysDocTypes.TR)
				{
					entityName = "TR";
				}
				flag = (isUpdate ? (flag & AddActivityLog(entityName, text, text2, ActivityTypes.Update, sqlTransaction)) : (flag & AddActivityLog(entityName, text, text2, ActivityTypes.Add, sqlTransaction)));
				if (!isUpdate)
				{
					flag &= new SystemDocuments(base.DBConfig).UpdateNextDocumentNumber(dataRow["SysDocID"].ToString(), dataRow["VoucherID"].ToString(), "Bank_Facility_Payment", "VoucherID", sqlTransaction);
				}
				if (!flag)
				{
					return flag;
				}
				new Approval(base.DBConfig).CreateTransactionApprovalTasks(SysDocTypes.TRPayment, text2, text, "Bank_Facility_Payment", sqlTransaction);
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

		private bool UpdatePaidAmount(string paymentSysDocID, string paymentVoucherID, SqlTransaction sqlTransaction)
		{
			try
			{
				string exp = "UPDATE BFT SET PaidAmount =\r\n                                (SELECT SUM(Amount) FROM Bank_Facility_Payment BFP\r\n                                WHERE ISNULL(IsVoid,'False') = 'False' AND TransactionSysDocID = (SELECT TransactionSysDocID FROM Bank_Facility_Payment WHERE SysDocID = '" + paymentSysDocID + "' AND VoucherID = '" + paymentVoucherID + "') \r\n\t\t\t\t\t\t\t\tAND TransactionVoucherID = (SELECT TransactionVoucherID FROM Bank_Facility_Payment WHERE SysDocID = '" + paymentSysDocID + "' AND VoucherID = '" + paymentVoucherID + "'))\r\n\t\t\t\t\t\t\t\tFROM Bank_Facility_Transaction BFT\r\n                                WHERE ISNULL(IsVoid,'False') = 'False' AND SysDocID = (SELECT TransactionSysDocID FROM Bank_Facility_Payment WHERE SysDocID = '" + paymentSysDocID + "' AND VoucherID = '" + paymentVoucherID + "') \r\n\t\t\t\t\t\t\t\t AND VOucherID = (SELECT TransactionVoucherID FROM Bank_Facility_Payment WHERE SysDocID = '" + paymentSysDocID + "' AND VoucherID = '" + paymentVoucherID + "')";
				return ExecuteNonQuery(exp) > 0;
			}
			catch
			{
				throw;
			}
		}

		private bool UpdatePaidBillDiscountAmount(string paymentSysDocID, string paymentVoucherID, SqlTransaction sqlTransaction)
		{
			try
			{
				string exp = "UPDATE BFT SET PaidAmount =\r\n                                (SELECT SUM(Amount) FROM Bank_Facility_Payment BFP\r\n                                WHERE ISNULL(IsVoid,'False') = 'False' AND TransactionSysDocID = (SELECT TransactionSysDocID FROM Bank_Facility_Payment WHERE SysDocID = '" + paymentSysDocID + "' AND VoucherID = '" + paymentVoucherID + "') \r\n\t\t\t\t\t\t\t\tAND TransactionVoucherID = (SELECT TransactionVoucherID FROM Bank_Facility_Payment WHERE SysDocID = '" + paymentSysDocID + "' AND VoucherID = '" + paymentVoucherID + "'))\r\n\t\t\t\t\t\t\t\tFROM Bill_Discount BFT\r\n                                WHERE ISNULL(IsVoid,'False') = 'False' AND SysDocID = (SELECT TransactionSysDocID FROM Bank_Facility_Payment WHERE SysDocID = '" + paymentSysDocID + "' AND VoucherID = '" + paymentVoucherID + "') \r\n\t\t\t\t\t\t\t\t AND VoucherID = (SELECT TransactionVoucherID FROM Bank_Facility_Payment WHERE SysDocID = '" + paymentSysDocID + "' AND VoucherID = '" + paymentVoucherID + "')";
				return ExecuteNonQuery(exp) > 0;
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

		private GLData CreateGLData(BankFacilityPaymentData paymentData)
		{
			GLData gLData = new GLData();
			DataRow dataRow = paymentData.BankFacilityPaymentTable.Rows[0];
			_ = paymentData.BankFacilityPaymentDetailsTable.Rows[0];
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
			DataRow dataRow3 = gLData.JournalDetailsTable.NewRow();
			dataRow3.BeginEdit();
			dataRow3["JournalID"] = 0;
			dataRow3["PayeeType"] = dataRow["PayeeType"];
			dataRow3["PayeeID"] = dataRow["PayeeID"];
			dataRow3["AccountID"] = dataRow["PayFromAccountID"];
			if (dataRow["PayeeType"] != DBNull.Value)
			{
				dataRow3["IsARAP"] = true;
			}
			if (sysDocTypes == SysDocTypes.TRPayment)
			{
				dataRow3["Debit"] = DBNull.Value;
				dataRow3["DebitFC"] = DBNull.Value;
				dataRow3["CreditFC"] = dataRow["AmountFC"];
				dataRow3["Credit"] = dataRow["Amount"];
			}
			dataRow3["Description"] = dataRow["Description"];
			dataRow3["Reference"] = dataRow["Reference"];
			dataRow3["CostCenterID"] = dataRow["CostCenterID"];
			dataRow3["AnalysisID"] = dataRow["AnalysisID"];
			dataRow3["RowIndex"] = -1;
			dataRow3["CompanyID"] = value;
			dataRow3["DivisionID"] = value2;
			dataRow3.EndEdit();
			gLData.JournalDetailsTable.Rows.Add(dataRow3);
			foreach (DataRow row in paymentData.BankFacilityPaymentDetailsTable.Rows)
			{
				dataRow3 = gLData.JournalDetailsTable.NewRow();
				dataRow3.BeginEdit();
				dataRow3["JournalID"] = 0;
				dataRow3["PayeeType"] = dataRow["PayeeType"];
				dataRow3["PayeeID"] = dataRow["PayeeID"];
				dataRow3["AccountID"] = row["AccountID"];
				if (sysDocTypes == SysDocTypes.TRPayment)
				{
					dataRow3["DebitFC"] = row["AmountFC"];
					dataRow3["Debit"] = row["Amount"];
					dataRow3["Credit"] = DBNull.Value;
					dataRow3["CreditFC"] = DBNull.Value;
				}
				dataRow3["Description"] = dataRow["Description"].ToString() + "-" + row["Description"].ToString();
				dataRow3["Reference"] = row["Reference"];
				dataRow3["CostCenterID"] = row["CostCenterID"];
				dataRow3["AnalysisID"] = row["AnalysisID"];
				dataRow3["RowIndex"] = row["RowIndex"];
				dataRow3["CompanyID"] = value;
				dataRow3["DivisionID"] = value2;
				dataRow3.EndEdit();
				gLData.JournalDetailsTable.Rows.Add(dataRow3);
			}
			foreach (DataRow row2 in paymentData.BankFeeDetailsTable.Rows)
			{
				dataRow3 = gLData.JournalDetailsTable.NewRow();
				dataRow3.BeginEdit();
				dataRow3["JournalID"] = 0;
				dataRow3["AccountID"] = row2["ExpenseAccountID"];
				dataRow3["DebitFC"] = DBNull.Value;
				dataRow3["Debit"] = row2["Amount"];
				dataRow3["IsBaseOnly"] = true;
				dataRow3["Credit"] = DBNull.Value;
				dataRow3["CreditFC"] = DBNull.Value;
				dataRow3["Description"] = row2["Description"];
				dataRow3["Reference"] = row2["Reference"];
				dataRow3["RowIndex"] = row2["RowIndex"];
				dataRow3["CompanyID"] = value;
				dataRow3["DivisionID"] = value2;
				row2["BankAccountID"].ToString();
				dataRow3.EndEdit();
				gLData.JournalDetailsTable.Rows.Add(dataRow3);
				dataRow3 = gLData.JournalDetailsTable.NewRow();
				dataRow3.BeginEdit();
				dataRow3["JournalID"] = 0;
				dataRow3["IsBaseOnly"] = true;
				dataRow3["AccountID"] = row2["BankAccountID"];
				dataRow3["DebitFC"] = DBNull.Value;
				dataRow3["Debit"] = DBNull.Value;
				dataRow3["Credit"] = row2["Amount"];
				dataRow3["CreditFC"] = DBNull.Value;
				dataRow3["Description"] = row2["Description"];
				dataRow3["Reference"] = row2["Reference"];
				dataRow3["RowIndex"] = row2["RowIndex"];
				dataRow3["CompanyID"] = value;
				dataRow3["DivisionID"] = value2;
				dataRow3.EndEdit();
				gLData.JournalDetailsTable.Rows.Add(dataRow3);
			}
			return gLData;
		}

		internal bool DeletePaymentDetailsRows(string sysDocID, string voucherID, SqlTransaction sqlTransaction)
		{
			bool flag = true;
			try
			{
				string commandText = "DELETE FROM Bank_Facility_Payment_Details WHERE SysDocID = '" + sysDocID + "' AND VoucherID = '" + voucherID + "'";
				flag &= Delete(commandText, sqlTransaction);
				commandText = "DELETE FROM Bank_Fee_Details WHERE GLTransactionSysDocID = '" + sysDocID + "' AND GLTransactionVoucherID = '" + voucherID + "'";
				return flag & Delete(commandText, sqlTransaction);
			}
			catch
			{
				throw;
			}
		}

		public bool VoidPayment(string sysDocID, string voucherID, bool isVoid)
		{
			bool flag = true;
			try
			{
				SqlTransaction sqlTransaction = base.DBConfig.StartNewTransaction();
				string exp = "SELECT IsVoid FROM Bank_Facility_Payment WHERE SysDocID='" + sysDocID + "' AND VoucherID='" + voucherID + "'";
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
						throw new Exception("This payment is already voided.");
					}
					throw new Exception("This payment is not voided.");
				}
				exp = "UPDATE Bank_Facility_Payment_Details SET IsVoid='" + isVoid.ToString() + "'  WHERE SysDocID='" + sysDocID + "' ";
				exp = exp + " AND VoucherID='" + voucherID + "'";
				flag = (ExecuteNonQuery(exp, sqlTransaction) >= 0);
				if (flag)
				{
					exp = "UPDATE Bank_Facility_Payment SET IsVoid = '" + isVoid.ToString() + "' WHERE SysDocID='" + sysDocID + "' ";
					exp = exp + " AND VoucherID='" + voucherID + "'";
					flag &= (ExecuteNonQuery(exp, sqlTransaction) > 0);
				}
				flag &= new Journal(base.DBConfig).VoidJournal(sysDocID, voucherID, isVoid, sqlTransaction);
				flag &= UpdatePaidAmount(sysDocID, voucherID, sqlTransaction);
				if (!flag)
				{
					return flag;
				}
				if (!isVoid)
				{
					AddActivityLog("Payment", voucherID, sysDocID, ActivityTypes.Unvoid, sqlTransaction);
					return flag;
				}
				AddActivityLog("Payment", voucherID, sysDocID, ActivityTypes.Void, sqlTransaction);
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

		public bool VoidPayment(string sysDocID, string voucherID, bool isVoid, BankFacilityTypes facilityType)
		{
			int num = 2;
			num = (int)facilityType;
			bool flag = true;
			try
			{
				SqlTransaction sqlTransaction = base.DBConfig.StartNewTransaction();
				string exp = "SELECT IsVoid FROM Bank_Facility_Payment WHERE SysDocID='" + sysDocID + "' AND VoucherID='" + voucherID + "'";
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
						throw new Exception("This payment is already voided.");
					}
					throw new Exception("This payment is not voided.");
				}
				exp = "UPDATE Bank_Facility_Payment_Details SET IsVoid='" + isVoid.ToString() + "'  WHERE SysDocID='" + sysDocID + "' ";
				exp = exp + " AND VoucherID='" + voucherID + "'";
				flag = (ExecuteNonQuery(exp, sqlTransaction) >= 0);
				if (flag)
				{
					exp = "UPDATE Bank_Facility_Payment SET IsVoid = '" + isVoid.ToString() + "' WHERE SysDocID='" + sysDocID + "' ";
					exp = exp + " AND VoucherID='" + voucherID + "'";
					flag &= (ExecuteNonQuery(exp, sqlTransaction) > 0);
				}
				flag &= new Journal(base.DBConfig).VoidJournal(sysDocID, voucherID, isVoid, sqlTransaction);
				switch (num)
				{
				case 2:
					flag &= UpdatePaidAmount(sysDocID, voucherID, sqlTransaction);
					break;
				case 4:
					flag &= UpdatePaidBillDiscountAmount(sysDocID, voucherID, sqlTransaction);
					break;
				}
				if (!flag)
				{
					return flag;
				}
				if (!isVoid)
				{
					AddActivityLog("Payment", voucherID, sysDocID, ActivityTypes.Unvoid, sqlTransaction);
					return flag;
				}
				AddActivityLog("Payment", voucherID, sysDocID, ActivityTypes.Void, sqlTransaction);
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

		public bool DeletePayment(string sysDocID, string voucherID)
		{
			bool flag = true;
			try
			{
				SqlTransaction sqlTransaction = base.DBConfig.StartNewTransaction();
				new SystemDocuments(base.DBConfig).GetSystemDocumentType(sysDocID, sqlTransaction);
				flag &= DeletePaymentDetailsRows(sysDocID, voucherID, sqlTransaction);
				string exp = "DELETE FROM Bank_Facility_Payment WHERE SysDocID='" + sysDocID + "' AND VoucherID='" + voucherID + "'";
				flag &= (ExecuteNonQuery(exp, sqlTransaction) > 0);
				flag &= new Journal(base.DBConfig).DeleteJournal(sysDocID, voucherID, sqlTransaction);
				flag &= new SystemDocuments(base.DBConfig).UpdateNextToDeletedDocumentNumber(sysDocID, voucherID, "Bank_Facility_Payment", "VoucherID", sqlTransaction);
				flag &= UpdatePaidAmount(sysDocID, voucherID, sqlTransaction);
				if (!flag)
				{
					return flag;
				}
				AddActivityLog("Payment", voucherID, sysDocID, ActivityTypes.Delete, sqlTransaction);
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

		public bool DeletePayment(string sysDocID, string voucherID, BankFacilityTypes ftype)
		{
			bool flag = true;
			try
			{
				SqlTransaction sqlTransaction = base.DBConfig.StartNewTransaction();
				new SystemDocuments(base.DBConfig).GetSystemDocumentType(sysDocID, sqlTransaction);
				flag &= DeletePaymentDetailsRows(sysDocID, voucherID, sqlTransaction);
				string exp = "DELETE FROM Bank_Facility_Payment WHERE SysDocID='" + sysDocID + "' AND VoucherID='" + voucherID + "'";
				flag &= (ExecuteNonQuery(exp, sqlTransaction) > 0);
				flag &= new Journal(base.DBConfig).DeleteJournal(sysDocID, voucherID, sqlTransaction);
				flag &= new SystemDocuments(base.DBConfig).UpdateNextToDeletedDocumentNumber(sysDocID, voucherID, "Bank_Facility_Payment", "VoucherID", sqlTransaction);
				switch (ftype)
				{
				case BankFacilityTypes.TR:
					flag &= UpdatePaidAmount(sysDocID, voucherID, sqlTransaction);
					break;
				case BankFacilityTypes.BillDiscount:
					flag &= UpdatePaidBillDiscountAmount(sysDocID, voucherID, sqlTransaction);
					break;
				}
				if (!flag)
				{
					return flag;
				}
				AddActivityLog("Payment", voucherID, sysDocID, ActivityTypes.Delete, sqlTransaction);
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

		public BankFacilityPaymentData GetPaymentByID(string sysDocID, string voucherID)
		{
			try
			{
				BankFacilityPaymentData bankFacilityPaymentData = new BankFacilityPaymentData();
				string textCommand = "SELECT BFP.*,BFT.BankFacilityID,BFT.Amount AS TRAmount,BFT.AmountFC AS TRAmountFC,(BFT.Amount- ISNULL(BFT.PaidAmount,0)) AS TRBalance ,\r\n                            BFT.Reference AS TRReference, BD.Amount AS BDAmount, BD.AmountFC AS BDAmountFC,(BD.Amount- ISNULL(BD.PaidAmount,0)) AS BDBalance\r\n                            FROM Bank_Facility_Payment  BFP \r\n                            LEFT OUTER JOIN Bank_Facility_Transaction BFT ON BFP.TransactionSysDocID = BFT.SysDocID AND BFP.TransactionVoucherID = BFT.VoucherID\r\n                            LEFT OUTER JOIN Bill_Discount BD ON BFP.TransactionSysDocID = BD.SysDocID AND BFP.TransactionVoucherID = BD.VoucherID\r\n                                         WHERE BFP.VoucherID = '" + voucherID + "' AND BFP.SysDocID='" + sysDocID + "'";
				FillDataSet(bankFacilityPaymentData, "Bank_Facility_Payment", textCommand);
				if (bankFacilityPaymentData == null || bankFacilityPaymentData.Tables.Count == 0 || bankFacilityPaymentData.Tables["Bank_Facility_Payment"].Rows.Count == 0)
				{
					return null;
				}
				bankFacilityPaymentData.BankFacilityPaymentTable.Rows[0]["TransactionID"].ToString();
				textCommand = "SELECT TD.*\r\n                        FROM BANK_FACILITY_Payment_DETAILS TD \r\n                        WHERE VoucherID='" + voucherID + "' AND SysDocID='" + sysDocID + "'";
				FillDataSet(bankFacilityPaymentData, "Bank_Facility_Payment_Details", textCommand);
				new SystemDocuments(base.DBConfig).GetSystemDocumentType(sysDocID, null);
				textCommand = "SELECT BFD.*,Cur.RateType FROM Bank_Fee_Details BFD LEFT OUTER JOIN Currency Cur ON BFD.CurrencyID = Cur.CurrencyID WHERE \r\n                                GLTransactionSysDocID='" + sysDocID + "' AND GLTransactionVoucherID = '" + voucherID + "'";
				FillDataSet(bankFacilityPaymentData, "Bank_Fee_Details", textCommand);
				return bankFacilityPaymentData;
			}
			catch
			{
				throw;
			}
		}

		public DataSet GetPaymentToPrint(string sysDocID, string[] voucherID)
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
				string textCommand = "SELECT BFP.*,BFT.BankFacilityID,BFT.Amount AS TRAmount,BFT.AmountFC AS TRAmountFC,(BFT.Amount- ISNULL(BFT.PaidAmount,0)) AS TRBalance ,\r\n                                BFT.Reference AS TRReference\r\n                                 FROM Bank_Facility_Payment  BFP LEFT JOIN Bank_Facility_Transaction BFT ON BFP.TransactionSysDocID = BFT.SysDocID \r\n                                 AND BFP.TransactionVoucherID = BFT.VoucherID\r\n                                  WHERE BFP.SysDocID= '" + sysDocID + "' AND BFP.VoucherID IN (" + text + ")";
				FillDataSet(dataSet, "Bank_Facility_Payment", textCommand);
				if (dataSet == null || dataSet.Tables.Count == 0 || dataSet.Tables["Bank_Facility_Payment"].Rows.Count == 0)
				{
					return null;
				}
				dataSet.Tables[0].Rows[0]["TransactionVoucherID"].ToString();
				textCommand = "SELECT TD.*,Bank.BankName,\r\n                        (CASE PayeeType\r\n                        WHEN 'C' THEN Customer.CustomerName\r\n                        WHEN 'V' THEN Vendor.VendorName\r\n                        WHEN 'E' THEN Employee.FirstName + ' ' + Employee.LastName\r\n                        ELSE Account.AccountName END) AS AccountName\r\n                        FROM BANK_FACILITY_TRANSACTION_DETAILS TD LEFt OUTER JOIN \r\n                        Account ON TD.AccountID=Account.AccountID LEFt OUTER JOIN\r\n                        Customer ON TD.PayeeID=Customer.CustomerID LEFt OUTER JOIN\r\n                        Vendor ON TD.PayeeID=Vendor.VendorID LEFt OUTER JOIN\r\n                        Employee ON TD.PayeeID=Employee.EmployeeID\r\n                        LEFT OUTER JOIN Bank ON Bank.BankID=TD.BankID\r\n                        WHERE SysDocID = '" + sysDocID + "' AND VoucherID IN (" + text + ")";
				FillDataSet(dataSet, "Bank_Facility_Payment_Details", textCommand);
				dataSet.Relations.Add("PaymentDetails", new DataColumn[2]
				{
					dataSet.Tables["Bank_Facility_Payment"].Columns["SysDocID"],
					dataSet.Tables["Bank_Facility_Payment"].Columns["VoucherID"]
				}, new DataColumn[2]
				{
					dataSet.Tables["Bank_Facility_Payment_Details"].Columns["SysDocID"],
					dataSet.Tables["Bank_Facility_Payment_Details"].Columns["VoucherID"]
				}, createConstraints: false);
				dataSet.Tables["Bank_Facility_Payment"].Columns.Add("AmountInWords", typeof(string));
				foreach (DataRow row in dataSet.Tables["Bank_Facility_Payment"].Rows)
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
			string text3 = "SELECT SysDocID,VoucherID,TransactionVoucherID [TR Number],TransactionDate,BankFacilityID,Amount,AmountFC FROM Bank_Facility_Payment\r\n                                WHERE  (FacilityType = " + (int)facilityType + " OR  FacilityType=4 )";
			if (from != DateTime.MinValue)
			{
				text3 = text3 + " AND TransactionDate Between '" + text + "' AND '" + text2 + "'";
			}
			if (!showVoid)
			{
				text3 += " AND ISNULL(IsVoid,'False')='False' ";
			}
			SqlCommand sqlCommand = new SqlCommand(text3);
			FillDataSet(dataSet, "Bank_Facility_Payment", sqlCommand);
			return dataSet;
		}

		public DataSet GetOpenPayments(BankFacilityTypes type)
		{
			DataSet dataSet = new DataSet();
			string textCommand = "SELECT  SysDocID,VoucherID,BFT.FacilityType,BankFacilityID,Reference,BF.CurrentAccountID,BF.PayableAccountID,CurrencyID,Amount,AmountFC,Amount-ISNULL(PaidAmount,0) AS Balance\r\n                                    FROM Bank_Facility_Payment BFT INNER JOIN Bank_Facility BF  ON BF.FacilityID=BFT.BankFacilityID WHERE Amount-ISNULL(PaidAmount,0)>0  AND BFT.FacilityType = " + (int)type;
			FillDataSet(dataSet, "Bank_Facility_Payment", textCommand);
			return dataSet;
		}
	}
}
