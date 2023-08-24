using Micromind.Common;
using Micromind.Common.Data;
using Micromind.Common.Libraries;
using System;
using System.Data;
using System.Data.SqlClient;

namespace Micromind.Data
{
	public sealed class PayrollTransaction : StoreObject
	{
		private const string PAYROLLTRANSACTION_TABLE = "Payroll_Transaction";

		private const string PAYROLLTRANSACTIONDETAIL_TABLE = "Payroll_Transaction_Detail";

		private const string SYSDOCID_PARM = "@SysDocID";

		private const string VOUCHERID_PARM = "@VoucherID";

		private const string COMPANYID_PARM = "@CompanyID";

		private const string DIVISIONID_PARM = "@DivisionID";

		private const string REGISTERID_PARM = "@RegisterID";

		private const string MONTH_PARM = "@Month";

		private const string STARTDATE_PARM = "@StartDate";

		private const string ENDDATE_PARM = "@EndDate";

		private const string AMOUNT_PARM = "@Amount";

		private const string AMOUNTFC_PARM = "@AmountFC";

		private const string TRANSACTIONDATE_PARM = "@TransactionDate";

		private const string ISVOID_PARM = "@IsVoid";

		private const string CURRENCYID_PARM = "@CurrencyID";

		private const string CURRENCYRATE_PARM = "@CurrencyRate";

		private const string GLTYPE_PARM = "@GLType";

		private const string JOURNALID_PARM = "@JournalID";

		private const string CHEQUEID_PARM = "@ChequeID";

		private const string CHEQUEBOOKID_PARM = "@ChequebookID";

		private const string CHECKNUMBER_PARM = "@CheckNumber";

		private const string CHECKDATE_PARM = "@CheckDate";

		private const string REFERENCE_PARM = "@Reference";

		private const string BANKACCOUNTID_PARM = "@BankAccountID";

		private const string DESCRIPTION_PARM = "@Description";

		private const string TYPEID_PARM = "@TypeID";

		private const string PAYMENTMETHODTYPE_PARM = "@PaymentMethodType";

		private const string PAYTYPE_PARM = "@PayType";

		private const string CHEQUETOTAL_PARM = "@ChequeTotal";

		private const string OTHERCHARGES_PARM = "@OtherCharges";

		private const string OTHERACCOUNTID_PARM = "@OtherAccountID";

		private const string TAXGROUPID_PARM = "@TaxGroupID";

		private const string TAXAMOUNT_PARM = "@TaxAmount";

		private const string EMPLOYEEID_PARM = "@EmployeeID";

		private const string PAYROLLITEMID_PARM = "@PayrollItemID";

		private const string ACCOUNTID_PARM = "@AccountID";

		private const string ROWINDEX_PARM = "@RowIndex";

		private const string ANALYSISID_PARM = "@AnalysisID";

		private const string COSTCENTERID_PARM = "@CostCenterID";

		private const string DAYS_PARM = "@Days";

		private const string SHEETSYSDOCID_PARM = "@SheetSysDocID";

		private const string SHEETVOUCHERID_PARM = "@SheetVoucherID";

		private const string SHEETROWINDEX_PARM = "@SheetRowIndex";

		public PayrollTransaction(Config config)
			: base(config)
		{
		}

		private string GetInsertUpdatePayrollTransactionText(bool isUpdate)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddInsertUpdateParameters("Payroll_Transaction", new FieldValue("SysDocID", "@SysDocID", isUpdateConditionField: true), new FieldValue("VoucherID", "@VoucherID", isUpdateConditionField: true), new FieldValue("CompanyID", "@CompanyID"), new FieldValue("DivisionID", "@DivisionID"), new FieldValue("RegisterID", "@RegisterID"), new FieldValue("Month", "@Month"), new FieldValue("Amount", "@Amount"), new FieldValue("AmountFC", "@AmountFC"), new FieldValue("TransactionDate", "@TransactionDate"), new FieldValue("IsVoid", "@IsVoid"), new FieldValue("CurrencyID", "@CurrencyID"), new FieldValue("CurrencyRate", "@CurrencyRate"), new FieldValue("GLType", "@GLType"), new FieldValue("ChequeID", "@ChequeID"), new FieldValue("ChequebookID", "@ChequebookID"), new FieldValue("CheckNumber", "@CheckNumber"), new FieldValue("CheckDate", "@CheckDate"), new FieldValue("Reference", "@Reference"), new FieldValue("BankAccountID", "@BankAccountID"), new FieldValue("Description", "@Description"), new FieldValue("PaymentMethodType", "@PaymentMethodType"), new FieldValue("ChequeTotal", "@ChequeTotal"), new FieldValue("OtherCharges", "@OtherCharges"), new FieldValue("OtherAccountID", "@OtherAccountID"), new FieldValue("TaxGroupID", "@TaxGroupID"), new FieldValue("TaxAmount", "@TaxAmount"), new FieldValue("TypeID", "@TypeID"));
			if (isUpdate)
			{
				return sqlBuilder.GetUpdateExpression();
			}
			return sqlBuilder.GetInsertExpression();
		}

		private SqlCommand GetInsertUpdatePayrollTransactionCommand(bool isUpdate)
		{
			SqlParameterCollection parameters;
			if (isUpdate)
			{
				if (updateCommand != null)
				{
					updateCommand = null;
				}
				updateCommand = new SqlCommand(GetInsertUpdatePayrollTransactionText(isUpdate: true), base.DBConfig.Connection);
				updateCommand.CommandType = CommandType.Text;
				parameters = updateCommand.Parameters;
			}
			else
			{
				if (insertCommand != null)
				{
					insertCommand = null;
				}
				insertCommand = new SqlCommand(GetInsertUpdatePayrollTransactionText(isUpdate: false), base.DBConfig.Connection);
				insertCommand.CommandType = CommandType.Text;
				parameters = insertCommand.Parameters;
			}
			parameters.Add("@SysDocID", SqlDbType.NVarChar);
			parameters.Add("@VoucherID", SqlDbType.NVarChar);
			parameters.Add("@CompanyID", SqlDbType.TinyInt);
			parameters.Add("@DivisionID", SqlDbType.NVarChar);
			parameters.Add("@RegisterID", SqlDbType.NVarChar);
			parameters.Add("@Month", SqlDbType.TinyInt);
			parameters.Add("@Amount", SqlDbType.Decimal);
			parameters.Add("@AmountFC", SqlDbType.Decimal);
			parameters.Add("@TransactionDate", SqlDbType.DateTime);
			parameters.Add("@IsVoid", SqlDbType.Bit);
			parameters.Add("@CurrencyID", SqlDbType.NVarChar);
			parameters.Add("@CurrencyRate", SqlDbType.Decimal);
			parameters.Add("@GLType", SqlDbType.TinyInt);
			parameters.Add("@ChequeID", SqlDbType.Int);
			parameters.Add("@ChequebookID", SqlDbType.NVarChar);
			parameters.Add("@CheckNumber", SqlDbType.NVarChar);
			parameters.Add("@CheckDate", SqlDbType.DateTime);
			parameters.Add("@Reference", SqlDbType.NVarChar);
			parameters.Add("@BankAccountID", SqlDbType.NVarChar);
			parameters.Add("@Description", SqlDbType.NVarChar);
			parameters.Add("@TypeID", SqlDbType.TinyInt);
			parameters.Add("@PaymentMethodType", SqlDbType.TinyInt);
			parameters.Add("@ChequeTotal", SqlDbType.Decimal);
			parameters.Add("@OtherCharges", SqlDbType.Decimal);
			parameters.Add("@OtherAccountID", SqlDbType.NVarChar);
			parameters.Add("@TaxGroupID", SqlDbType.NVarChar);
			parameters.Add("@TaxAmount", SqlDbType.Decimal);
			parameters["@SysDocID"].SourceColumn = "SysDocID";
			parameters["@VoucherID"].SourceColumn = "VoucherID";
			parameters["@CompanyID"].SourceColumn = "CompanyID";
			parameters["@DivisionID"].SourceColumn = "DivisionID";
			parameters["@RegisterID"].SourceColumn = "RegisterID";
			parameters["@Month"].SourceColumn = "Month";
			parameters["@Amount"].SourceColumn = "Amount";
			parameters["@AmountFC"].SourceColumn = "AmountFC";
			parameters["@TransactionDate"].SourceColumn = "TransactionDate";
			parameters["@IsVoid"].SourceColumn = "IsVoid";
			parameters["@CurrencyID"].SourceColumn = "CurrencyID";
			parameters["@CurrencyRate"].SourceColumn = "CurrencyRate";
			parameters["@GLType"].SourceColumn = "GLType";
			parameters["@ChequeID"].SourceColumn = "ChequeID";
			parameters["@ChequebookID"].SourceColumn = "ChequebookID";
			parameters["@CheckNumber"].SourceColumn = "CheckNumber";
			parameters["@CheckDate"].SourceColumn = "CheckDate";
			parameters["@Reference"].SourceColumn = "Reference";
			parameters["@BankAccountID"].SourceColumn = "BankAccountID";
			parameters["@Description"].SourceColumn = "Description";
			parameters["@TypeID"].SourceColumn = "TypeID";
			parameters["@PaymentMethodType"].SourceColumn = "PaymentMethodType";
			parameters["@ChequeTotal"].SourceColumn = "ChequeTotal";
			parameters["@OtherCharges"].SourceColumn = "OtherCharges";
			parameters["@OtherAccountID"].SourceColumn = "OtherAccountID";
			parameters["@TaxGroupID"].SourceColumn = "TaxGroupID";
			parameters["@TaxAmount"].SourceColumn = "TaxAmount";
			if (isUpdate)
			{
				return updateCommand;
			}
			return insertCommand;
		}

		private string GetInsertUpdatePayrollTransactionDetailsText(bool isUpdate)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddInsertUpdateParameters("Payroll_Transaction_Detail", new FieldValue("SysDocID", "@SysDocID"), new FieldValue("VoucherID", "@VoucherID"), new FieldValue("EmployeeID", "@EmployeeID"), new FieldValue("AccountID", "@AccountID"), new FieldValue("RowIndex", "@RowIndex"), new FieldValue("Days", "@Days"), new FieldValue("SheetSysDocID", "@SheetSysDocID"), new FieldValue("SheetVoucherID", "@SheetVoucherID"), new FieldValue("SheetRowIndex", "@SheetRowIndex"), new FieldValue("Amount", "@Amount"));
			if (isUpdate)
			{
				return sqlBuilder.GetUpdateExpression();
			}
			return sqlBuilder.GetInsertExpression();
		}

		private SqlCommand GetInsertUpdatePayrollTransactionDetailsCommand(bool isUpdate)
		{
			SqlParameterCollection parameters;
			if (isUpdate)
			{
				if (updateCommand != null)
				{
					updateCommand = null;
				}
				updateCommand = new SqlCommand(GetInsertUpdatePayrollTransactionDetailsText(isUpdate: true), base.DBConfig.Connection);
				updateCommand.CommandType = CommandType.Text;
				parameters = updateCommand.Parameters;
			}
			else
			{
				if (insertCommand != null)
				{
					insertCommand = null;
				}
				insertCommand = new SqlCommand(GetInsertUpdatePayrollTransactionDetailsText(isUpdate: false), base.DBConfig.Connection);
				insertCommand.CommandType = CommandType.Text;
				parameters = insertCommand.Parameters;
			}
			parameters.Add("@SysDocID", SqlDbType.NVarChar);
			parameters.Add("@VoucherID", SqlDbType.NVarChar);
			parameters.Add("@EmployeeID", SqlDbType.NVarChar);
			parameters.Add("@AccountID", SqlDbType.NVarChar);
			parameters.Add("@RowIndex", SqlDbType.TinyInt);
			parameters.Add("@Amount", SqlDbType.Decimal);
			parameters.Add("@Days", SqlDbType.NVarChar);
			parameters.Add("@SheetSysDocID", SqlDbType.NVarChar);
			parameters.Add("@SheetVoucherID", SqlDbType.NVarChar);
			parameters.Add("@SheetRowIndex", SqlDbType.Int);
			parameters["@SysDocID"].SourceColumn = "SysDocID";
			parameters["@VoucherID"].SourceColumn = "VoucherID";
			parameters["@EmployeeID"].SourceColumn = "EmployeeID";
			parameters["@AccountID"].SourceColumn = "AccountID";
			parameters["@RowIndex"].SourceColumn = "RowIndex";
			parameters["@Amount"].SourceColumn = "Amount";
			parameters["@Days"].SourceColumn = "Days";
			parameters["@SheetSysDocID"].SourceColumn = "SheetSysDocID";
			parameters["@SheetVoucherID"].SourceColumn = "SheetVoucherID";
			parameters["@SheetRowIndex"].SourceColumn = "SheetRowIndex";
			if (isUpdate)
			{
				return updateCommand;
			}
			return insertCommand;
		}

		private bool ValidateData(PayrollTransactionData journalData)
		{
			return true;
		}

		public bool InsertUpdatePayrollTransactionOld(PayrollTransactionData payrollTransactionData, bool isUpdate)
		{
			bool flag = true;
			SqlCommand insertUpdatePayrollTransactionCommand = GetInsertUpdatePayrollTransactionCommand(isUpdate);
			try
			{
				DataRow dataRow = payrollTransactionData.PayrollTransactionTable.Rows[0];
				SqlTransaction sqlTransaction = base.DBConfig.StartNewTransaction();
				string text = dataRow["VoucherID"].ToString();
				string text2 = dataRow["SysDocID"].ToString();
				SalaryPaymentMethods salaryPaymentMethods = (SalaryPaymentMethods)byte.Parse(dataRow["PaymentMethodType"].ToString());
				string registerID = "";
				if (dataRow["RegisterID"] != DBNull.Value)
				{
					registerID = dataRow["RegisterID"].ToString();
				}
				switch (salaryPaymentMethods)
				{
				case SalaryPaymentMethods.Cash:
				{
					string text4 = (string)(dataRow["BankAccountID"] = new Register(base.DBConfig).GetRegisterAccountID(registerID, "CashAccountID"));
					break;
				}
				case SalaryPaymentMethods.Cheque:
				{
					dataRow["ChequebookID"].ToString();
					object fieldValue = new Databases(base.DBConfig).GetFieldValue("Chequebook", "PDCIssuedAccountID", "ChequebookID", dataRow["ChequebookID"].ToString(), sqlTransaction);
					if (fieldValue == null || fieldValue.ToString() == "")
					{
						throw new CompanyException("'PDC Issued Account' is not assigned to this chequebook. Please select an account for this chequebook.");
					}
					string text3 = fieldValue.ToString();
					if (text3 == "")
					{
						throw new CompanyException("PDC Issued account is empty", 1002);
					}
					dataRow["BankAccountID"] = text3;
					if (isUpdate && new IssuedCheques(base.DBConfig).TransactionHasProcessedCheques(text2, text, sqlTransaction))
					{
						throw new CompanyException("You cannot edit this transaction because the cheque is in use by another transaction.", 999);
					}
					break;
				}
				case SalaryPaymentMethods.Transfer:
					new Register(base.DBConfig).GetRegisterAccountID(registerID, "CashAccountID");
					dataRow["BankAccountID"].ToString();
					break;
				}
				if (!isUpdate && new SystemDocuments(base.DBConfig).ExistDocumentNumber("Payroll_Transaction", "VoucherID", dataRow["SysDocID"].ToString(), text, sqlTransaction))
				{
					throw new CompanyException("Document number already exist.", 1046);
				}
				if (payrollTransactionData.PayrollTransactionDetailTable.Rows[0]["SheetSysDocID"].ToString() != "" && payrollTransactionData.PayrollTransactionDetailTable.Rows[0]["SheetVoucherID"].ToString() != "")
				{
					payrollTransactionData.PayrollTransactionDetailTable.Rows[0]["SheetSysDocID"].ToString();
					payrollTransactionData.PayrollTransactionDetailTable.Rows[0]["SheetVoucherID"].ToString();
				}
				string textCommand = "SELECT SD.LocationID,EmployeeAccountID  FROM System_Document SD INNER JOIN Location LOC ON SD.LocationID = LOC.LocationID \r\n                                                                     \r\n                                WHERE SysDocID = '" + text2 + "'";
				string text5 = "";
				DataSet dataSet = new DataSet();
				FillDataSet(dataSet, "AccountDtls", textCommand, sqlTransaction);
				if (dataSet != null || dataSet.Tables.Count != 0 || dataSet.Tables[0].Rows.Count != 0)
				{
					DataRow dataRow2 = dataSet.Tables["AccountDtls"].Rows[0];
					dataRow2["LocationID"].ToString();
					text5 = dataRow2["EmployeeAccountID"].ToString();
				}
				string commaSeperatedIDs = GetCommaSeperatedIDs(payrollTransactionData, "Payroll_Transaction_Detail", "EmployeeID");
				string textCommand2 = "SELECT EmployeeID,ISNULL(EMP.AccountID,ET.AccountID) AS AccountID \r\n                                FROM Employee EMP LEFT OUTER JOIN Employee_Type ET ON EMP.ContractType = ET.TypeID\r\n                                WHERE EMP.EmployeeID IN (" + commaSeperatedIDs + ") ";
				DataSet dataSet2 = new DataSet();
				FillDataSet(dataSet2, "Employee", textCommand2, sqlTransaction);
				foreach (DataRow row in payrollTransactionData.PayrollTransactionDetailTable.Rows)
				{
					row["SysDocID"] = dataRow["SysDocID"];
					row["VoucherID"] = dataRow["VoucherID"];
					string text6 = row["EmployeeID"].ToString();
					string text7 = "";
					object fieldValue2 = new Databases(base.DBConfig).GetFieldValue("Employee", "AccountID", "EmployeeID", text6, sqlTransaction);
					if (fieldValue2 != null && fieldValue2.ToString() != "")
					{
						text7 = fieldValue2.ToString();
					}
					else if (text5 != "")
					{
						text7 = text5;
					}
					else
					{
						if (dataSet2 == null || dataSet2.Tables.Count <= 0 || dataSet2.Tables["Employee"].Rows.Count <= 0)
						{
							throw new CompanyException("Account is not set for employee " + text6 + "'", 1022);
						}
						DataRow[] array = dataSet2.Tables[0].Select("EmployeeID = '" + text6 + "'");
						if (array.Length != 0 && array[0]["AccountID"] != DBNull.Value)
						{
							text5 = array[0]["AccountID"].ToString();
						}
						text7 = text5;
					}
					row["AccountID"] = text7;
				}
				insertUpdatePayrollTransactionCommand.Transaction = sqlTransaction;
				flag = (isUpdate ? (flag & Update(payrollTransactionData, "Payroll_Transaction", insertUpdatePayrollTransactionCommand)) : (flag & Insert(payrollTransactionData, "Payroll_Transaction", insertUpdatePayrollTransactionCommand)));
				insertUpdatePayrollTransactionCommand = GetInsertUpdatePayrollTransactionDetailsCommand(isUpdate: false);
				insertUpdatePayrollTransactionCommand.Transaction = sqlTransaction;
				if (isUpdate)
				{
					flag &= DeletePayrollTransactionDetailsRows(text2, text, sqlTransaction);
				}
				if (payrollTransactionData.Tables["Payroll_Transaction_Detail"].Rows.Count > 0)
				{
					flag &= Insert(payrollTransactionData, "Payroll_Transaction_Detail", insertUpdatePayrollTransactionCommand);
				}
				string text8 = "";
				decimal num = default(decimal);
				string text9 = "";
				string text10 = "";
				foreach (DataRow row2 in payrollTransactionData.PayrollTransactionDetailTable.Rows)
				{
					text8 = row2["EmployeeID"].ToString();
					num = decimal.Parse(row2["Amount"].ToString());
					text9 = row2["SheetSysDocID"].ToString();
					text10 = row2["SheetVoucherID"].ToString();
					decimal result = default(decimal);
					string text11 = "";
					if (!isUpdate)
					{
						text11 = "UPDATE SalarySheet_Detail SET PaidAmount= ISNULL(PaidAmount, 0)   +   " + num + " WHERE SysDocID='" + text9 + "' AND VoucherID = '" + text10 + "' AND EmployeeID = '" + text8 + "' ";
						flag &= Update(text11, sqlTransaction);
					}
					else
					{
						text11 = "Select SUM(Amount) from Payroll_Transaction_Detail where SheetSysDocID='" + text9 + "' AND SheetVoucherID='" + text10 + "' and EmployeeID= '" + text8 + "'";
						object obj3 = ExecuteScalar(text11);
						if (obj3 != null)
						{
							decimal.TryParse(obj3.ToString(), out result);
						}
						text11 = "UPDATE SalarySheet_Detail SET PaidAmount=  " + result + " WHERE SysDocID='" + text9 + "' AND VoucherID = '" + text10 + "' AND EmployeeID = '" + text8 + "' ";
						flag &= Update(text11, sqlTransaction);
					}
				}
				text8 = payrollTransactionData.PayrollTransactionDetailTable.Rows[0]["EmployeeID"].ToString();
				if (text8 == "")
				{
					throw new CompanyException("Employee cannot be empty.");
				}
				if (salaryPaymentMethods == SalaryPaymentMethods.Cheque)
				{
					IssuedChequeData issuedChequeData = new IssuedChequeData();
					DataRow dataRow3 = issuedChequeData.IssuedChequeTable.NewRow();
					dataRow3["SysDocID"] = text2;
					dataRow3["VoucherID"] = text;
					dataRow3["PayeeType"] = "E";
					dataRow3["PayeeID"] = text8;
					dataRow3["ChequeNumber"] = dataRow["CheckNumber"];
					dataRow3["ChequebookID"] = dataRow["ChequebookID"];
					dataRow3["ChequeDate"] = dataRow["CheckDate"];
					dataRow3["Amount"] = dataRow["Amount"];
					dataRow3["AmountFC"] = dataRow["AmountFC"];
					dataRow3["ExchangeRate"] = dataRow["CurrencyRate"];
					dataRow3["Note"] = dataRow["Description"];
					dataRow3["IssueDate"] = dataRow["TransactionDate"];
					dataRow3["Reference"] = dataRow["Reference"];
					dataRow3["PDCAccountID"] = dataRow["BankAccountID"];
					dataRow3["Status"] = (byte)2;
					dataRow3.EndEdit();
					issuedChequeData.IssuedChequeTable.Rows.Add(dataRow3);
					if (issuedChequeData.IssuedChequeTable.Rows.Count > 0)
					{
						flag &= new IssuedCheques(base.DBConfig).InsertUpdateIssuedCheque(issuedChequeData, isUpdate, sqlTransaction);
					}
				}
				if (payrollTransactionData.Tables.Contains("Tax_Detail") && payrollTransactionData.Tables["Tax_Detail"].Rows.Count > 0)
				{
					flag &= new TaxTransaction(base.DBConfig).InsertUpdateTaxTransaction(payrollTransactionData, text2, text, isUpdate, sqlTransaction);
				}
				GLData journalData = CreatePayrollGLData(payrollTransactionData);
				flag &= new Journal(base.DBConfig).InsertUpdateJournal(journalData, isUpdate, sqlTransaction);
				if (!flag)
				{
					throw new Exception("Failed to save the transaction because of an unexpected error. Please try again.");
				}
				flag &= UpdateTableRowInsertUpdateInfo("Payroll_Transaction", "SysDocID", dataRow["SysDocID"].ToString(), "VoucherID", dataRow["VoucherID"].ToString(), sqlTransaction, !isUpdate);
				string entityName = "Salary Payment";
				flag = (isUpdate ? (flag & AddActivityLog(entityName, text, text2, ActivityTypes.Update, sqlTransaction)) : (flag & AddActivityLog(entityName, text, text2, ActivityTypes.Add, sqlTransaction)));
				if (!isUpdate)
				{
					flag &= new SystemDocuments(base.DBConfig).UpdateNextDocumentNumber(dataRow["SysDocID"].ToString(), dataRow["VoucherID"].ToString(), "Payroll_Transaction", "VoucherID", sqlTransaction);
				}
				switch (salaryPaymentMethods)
				{
				case SalaryPaymentMethods.Cash:
					new Approval(base.DBConfig).CreateTransactionApprovalTasks(SysDocTypes.SalaryPaymentCash, text2, text, "Payroll_Transaction", sqlTransaction);
					return flag;
				case SalaryPaymentMethods.Cheque:
					new Approval(base.DBConfig).CreateTransactionApprovalTasks(SysDocTypes.SalaryPaymentCheque, text2, text, "Payroll_Transaction", sqlTransaction);
					return flag;
				case SalaryPaymentMethods.Transfer:
					new Approval(base.DBConfig).CreateTransactionApprovalTasks(SysDocTypes.SalaryPaymentBank, text2, text, "Payroll_Transaction", sqlTransaction);
					return flag;
				default:
					return flag;
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

		public bool InsertUpdatePayrollTransaction(PayrollTransactionData payrollTransactionData, bool isUpdate, bool isManual)
		{
			bool flag = true;
			SqlCommand insertUpdatePayrollTransactionCommand = GetInsertUpdatePayrollTransactionCommand(isUpdate);
			try
			{
				DataRow dataRow = payrollTransactionData.PayrollTransactionTable.Rows[0];
				SqlTransaction sqlTransaction = base.DBConfig.StartNewTransaction();
				string text = dataRow["VoucherID"].ToString();
				string text2 = dataRow["SysDocID"].ToString();
				SalaryPaymentMethods salaryPaymentMethods = (SalaryPaymentMethods)byte.Parse(dataRow["PaymentMethodType"].ToString());
				string registerID = "";
				if (dataRow["RegisterID"] != DBNull.Value)
				{
					registerID = dataRow["RegisterID"].ToString();
				}
				switch (salaryPaymentMethods)
				{
				case SalaryPaymentMethods.Cash:
				{
					string text4 = (string)(dataRow["BankAccountID"] = new Register(base.DBConfig).GetRegisterAccountID(registerID, "CashAccountID"));
					break;
				}
				case SalaryPaymentMethods.Cheque:
					if (!string.IsNullOrEmpty(dataRow["ChequebookID"].ToString()))
					{
						object fieldValue = new Databases(base.DBConfig).GetFieldValue("Chequebook", "PDCIssuedAccountID", "ChequebookID", dataRow["ChequebookID"].ToString(), sqlTransaction);
						if (fieldValue == null || fieldValue.ToString() == "")
						{
							throw new CompanyException("'PDC Issued Account' is not assigned to this chequebook. Please select an account for this chequebook.");
						}
						string text3 = fieldValue.ToString();
						if (text3 == "")
						{
							throw new CompanyException("PDC Issued account is empty", 1002);
						}
						dataRow["BankAccountID"] = text3;
					}
					if (isUpdate && new IssuedCheques(base.DBConfig).TransactionHasProcessedCheques(text2, text, sqlTransaction))
					{
						throw new CompanyException("You cannot edit this transaction because the cheque is in use by another transaction.", 999);
					}
					break;
				case SalaryPaymentMethods.Transfer:
					new Register(base.DBConfig).GetRegisterAccountID(registerID, "CashAccountID");
					dataRow["BankAccountID"].ToString();
					break;
				}
				if (!isUpdate && new SystemDocuments(base.DBConfig).ExistDocumentNumber("Payroll_Transaction", "VoucherID", dataRow["SysDocID"].ToString(), text, sqlTransaction))
				{
					throw new CompanyException("Document number already exist.", 1046);
				}
				if (payrollTransactionData.PayrollTransactionDetailTable.Rows[0]["SheetSysDocID"].ToString() != "" && payrollTransactionData.PayrollTransactionDetailTable.Rows[0]["SheetVoucherID"].ToString() != "")
				{
					payrollTransactionData.PayrollTransactionDetailTable.Rows[0]["SheetSysDocID"].ToString();
					payrollTransactionData.PayrollTransactionDetailTable.Rows[0]["SheetVoucherID"].ToString();
				}
				string textCommand = "SELECT SD.LocationID,EmployeeAccountID  FROM System_Document SD INNER JOIN Location LOC ON SD.LocationID = LOC.LocationID \r\n                                                                     \r\n                                WHERE SysDocID = '" + text2 + "'";
				string text5 = "";
				DataSet dataSet = new DataSet();
				FillDataSet(dataSet, "AccountDtls", textCommand, sqlTransaction);
				if (dataSet != null || dataSet.Tables.Count != 0 || dataSet.Tables[0].Rows.Count != 0)
				{
					DataRow dataRow2 = dataSet.Tables["AccountDtls"].Rows[0];
					dataRow2["LocationID"].ToString();
					text5 = dataRow2["EmployeeAccountID"].ToString();
				}
				string commaSeperatedIDs = GetCommaSeperatedIDs(payrollTransactionData, "Payroll_Transaction_Detail", "EmployeeID");
				string textCommand2 = "SELECT EmployeeID,ISNULL(EMP.AccountID,ET.AccountID) AS AccountID \r\n                                FROM Employee EMP LEFT OUTER JOIN Employee_Type ET ON EMP.ContractType = ET.TypeID\r\n                                WHERE EMP.EmployeeID IN (" + commaSeperatedIDs + ") ";
				DataSet dataSet2 = new DataSet();
				FillDataSet(dataSet2, "Employee", textCommand2, sqlTransaction);
				foreach (DataRow row in payrollTransactionData.PayrollTransactionDetailTable.Rows)
				{
					row["SysDocID"] = dataRow["SysDocID"];
					row["VoucherID"] = dataRow["VoucherID"];
					string text6 = row["EmployeeID"].ToString();
					string text7 = "";
					object fieldValue2 = new Databases(base.DBConfig).GetFieldValue("Employee", "AccountID", "EmployeeID", text6, sqlTransaction);
					if (fieldValue2 != null && fieldValue2.ToString() != "")
					{
						text7 = fieldValue2.ToString();
					}
					else if (text5 != "")
					{
						text7 = text5;
					}
					else
					{
						if (dataSet2 == null || dataSet2.Tables.Count <= 0 || dataSet2.Tables["Employee"].Rows.Count <= 0)
						{
							throw new CompanyException("Account is not set for employee " + text6 + "'", 1022);
						}
						DataRow[] array = dataSet2.Tables[0].Select("EmployeeID = '" + text6 + "'");
						if (array.Length != 0 && array[0]["AccountID"] != DBNull.Value)
						{
							text5 = array[0]["AccountID"].ToString();
						}
						text7 = text5;
					}
					row["AccountID"] = text7;
				}
				insertUpdatePayrollTransactionCommand.Transaction = sqlTransaction;
				flag = (isUpdate ? (flag & Update(payrollTransactionData, "Payroll_Transaction", insertUpdatePayrollTransactionCommand)) : (flag & Insert(payrollTransactionData, "Payroll_Transaction", insertUpdatePayrollTransactionCommand)));
				insertUpdatePayrollTransactionCommand = GetInsertUpdatePayrollTransactionDetailsCommand(isUpdate: false);
				insertUpdatePayrollTransactionCommand.Transaction = sqlTransaction;
				if (isUpdate)
				{
					flag &= DeletePayrollTransactionDetailsRows(text2, text, sqlTransaction);
				}
				if (payrollTransactionData.Tables["Payroll_Transaction_Detail"].Rows.Count > 0)
				{
					flag &= Insert(payrollTransactionData, "Payroll_Transaction_Detail", insertUpdatePayrollTransactionCommand);
				}
				string text8 = "";
				decimal num = default(decimal);
				string text9 = "";
				string text10 = "";
				foreach (DataRow row2 in payrollTransactionData.PayrollTransactionDetailTable.Rows)
				{
					text8 = row2["EmployeeID"].ToString();
					num = decimal.Parse(row2["Amount"].ToString());
					text9 = row2["SheetSysDocID"].ToString();
					text10 = row2["SheetVoucherID"].ToString();
					decimal result = default(decimal);
					string text11 = "";
					if (!isUpdate)
					{
						text11 = "UPDATE SalarySheet_Detail SET PaidAmount= ISNULL(PaidAmount, 0)   +   " + num + " WHERE SysDocID='" + text9 + "' AND VoucherID = '" + text10 + "' AND EmployeeID = '" + text8 + "' ";
						flag &= Update(text11, sqlTransaction);
					}
					else
					{
						text11 = "Select SUM(Amount) from Payroll_Transaction_Detail where SheetSysDocID='" + text9 + "' AND SheetVoucherID='" + text10 + "' and EmployeeID= '" + text8 + "'";
						object obj3 = ExecuteScalar(text11);
						if (obj3 != null)
						{
							decimal.TryParse(obj3.ToString(), out result);
						}
						text11 = "UPDATE SalarySheet_Detail SET PaidAmount=  " + result + " WHERE SysDocID='" + text9 + "' AND VoucherID = '" + text10 + "' AND EmployeeID = '" + text8 + "' ";
						flag &= Update(text11, sqlTransaction);
					}
				}
				text8 = payrollTransactionData.PayrollTransactionDetailTable.Rows[0]["EmployeeID"].ToString();
				if (text8 == "")
				{
					throw new CompanyException("Employee cannot be empty.");
				}
				if (salaryPaymentMethods == SalaryPaymentMethods.Cheque)
				{
					IssuedChequeData issuedChequeData = new IssuedChequeData();
					DataRow dataRow3 = issuedChequeData.IssuedChequeTable.NewRow();
					dataRow3["SysDocID"] = text2;
					dataRow3["VoucherID"] = text;
					dataRow3["PayeeType"] = "E";
					dataRow3["PayeeID"] = text8;
					dataRow3["ChequeNumber"] = dataRow["CheckNumber"];
					dataRow3["ChequebookID"] = dataRow["ChequebookID"];
					dataRow3["ChequeDate"] = dataRow["CheckDate"];
					dataRow3["Amount"] = dataRow["Amount"];
					dataRow3["AmountFC"] = dataRow["AmountFC"];
					dataRow3["ExchangeRate"] = dataRow["CurrencyRate"];
					dataRow3["Note"] = dataRow["Description"];
					dataRow3["IssueDate"] = dataRow["TransactionDate"];
					dataRow3["Reference"] = dataRow["Reference"];
					dataRow3["PDCAccountID"] = dataRow["BankAccountID"];
					dataRow3["Status"] = (byte)2;
					dataRow3.EndEdit();
					issuedChequeData.IssuedChequeTable.Rows.Add(dataRow3);
					if (issuedChequeData.IssuedChequeTable.Rows.Count > 0 && !string.IsNullOrEmpty(dataRow3["ChequeNumber"].ToString()))
					{
						flag &= new IssuedCheques(base.DBConfig).InsertUpdateIssuedCheque(issuedChequeData, isUpdate, sqlTransaction);
					}
				}
				if (payrollTransactionData.Tables.Contains("Tax_Detail") && payrollTransactionData.Tables["Tax_Detail"].Rows.Count > 0)
				{
					flag &= new TaxTransaction(base.DBConfig).InsertUpdateTaxTransaction(payrollTransactionData, text2, text, isUpdate, sqlTransaction);
				}
				if (!isManual)
				{
					GLData journalData = CreatePayrollGLData(payrollTransactionData);
					flag &= new Journal(base.DBConfig).InsertUpdateJournal(journalData, isUpdate, sqlTransaction);
				}
				if (!flag)
				{
					throw new Exception("Failed to save the transaction because of an unexpected error. Please try again.");
				}
				flag &= UpdateTableRowInsertUpdateInfo("Payroll_Transaction", "SysDocID", dataRow["SysDocID"].ToString(), "VoucherID", dataRow["VoucherID"].ToString(), sqlTransaction, !isUpdate);
				string entityName = "Salary Payment";
				flag = (isUpdate ? (flag & AddActivityLog(entityName, text, text2, ActivityTypes.Update, sqlTransaction)) : (flag & AddActivityLog(entityName, text, text2, ActivityTypes.Add, sqlTransaction)));
				if (!isUpdate)
				{
					flag &= new SystemDocuments(base.DBConfig).UpdateNextDocumentNumber(dataRow["SysDocID"].ToString(), dataRow["VoucherID"].ToString(), "Payroll_Transaction", "VoucherID", sqlTransaction);
				}
				switch (salaryPaymentMethods)
				{
				case SalaryPaymentMethods.Cash:
					new Approval(base.DBConfig).CreateTransactionApprovalTasks(SysDocTypes.SalaryPaymentCash, text2, text, "Payroll_Transaction", sqlTransaction);
					return flag;
				case SalaryPaymentMethods.Cheque:
					new Approval(base.DBConfig).CreateTransactionApprovalTasks(SysDocTypes.SalaryPaymentCheque, text2, text, "Payroll_Transaction", sqlTransaction);
					return flag;
				case SalaryPaymentMethods.Transfer:
					new Approval(base.DBConfig).CreateTransactionApprovalTasks(SysDocTypes.SalaryPaymentBank, text2, text, "Payroll_Transaction", sqlTransaction);
					return flag;
				default:
					return flag;
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

		private bool UpdatePaidLoans(ref PayrollTransactionData data, SqlTransaction sqlTransaction)
		{
			bool flag = true;
			try
			{
				if (data == null || data.Tables.Count == 0 || data.Tables["Payroll_Transaction_Detail"].Rows.Count == 0)
				{
					return true;
				}
				foreach (DataRow row in data.Tables["Payroll_Transaction_Detail"].Rows)
				{
					if (byte.Parse(row["PayType"].ToString()) == 3)
					{
						string text = row["LoanVoucherID"].ToString();
						string text2 = row["EmployeeID"].ToString();
						decimal d = decimal.Parse(row["Amount"].ToString());
						if (!(d == 0m))
						{
							DataSet firstEmployeeLoanByID = new EmployeeLoan(base.DBConfig).GetFirstEmployeeLoanByID(text, text2, sqlTransaction);
							if (firstEmployeeLoanByID == null || firstEmployeeLoanByID.Tables.Count == 0 || firstEmployeeLoanByID.Tables[0].Rows.Count == 0)
							{
								throw new CompanyException("The loan number you have entered does not exist. Employee:" + text2);
							}
							DataRow dataRow2 = firstEmployeeLoanByID.Tables[0].Rows[0];
							row["LoanSysDocID"] = dataRow2["SysDocID"].ToString();
							row.EndEdit();
							decimal d2 = decimal.Parse(dataRow2["Balance"].ToString());
							if (d > d2)
							{
								throw new CompanyException("The deduction amount is greater than the loan balance. Loan No:" + text + "  Employee:" + text2);
							}
							string commandText = "UPDATE Employee_Loan SET DeductedAmount= ISNULL(DeductedAmount,0) + " + d.ToString() + " WHERE SysDocID='" + row["LoanSysDocID"].ToString() + "'\r\n                                  AND VoucherID='" + text + "' AND EmployeeID='" + text2 + "'";
							flag &= Update(commandText, sqlTransaction);
						}
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

		public PayrollTransactionData GetPayrollTransactionByID(string sysDocID, string voucherID)
		{
			return GetPayrollTransactionByID(sysDocID, voucherID, null);
		}

		public PayrollTransactionData GetPayrollTransactionByID(string sysDocID, string voucherID, SqlTransaction sqlTransaction)
		{
			try
			{
				PayrollTransactionData payrollTransactionData = new PayrollTransactionData();
				string textCommand = "SELECT * FROM Payroll_Transaction WHERE VoucherID='" + voucherID + "' AND SysDocID='" + sysDocID + "'";
				FillDataSet(payrollTransactionData, "Payroll_Transaction", textCommand, sqlTransaction);
				if (payrollTransactionData == null || payrollTransactionData.Tables.Count == 0 || payrollTransactionData.Tables["Payroll_Transaction"].Rows.Count == 0)
				{
					return null;
				}
				textCommand = "SELECT TD.*,Employee.FirstName + ' ' + Employee.LastName AS EmployeeName, SD.NetSalary, SD.PaidAmount, SD.NetSalary - ISNULL(SD.PaidAmount, 0) AS Balance, SS.Month, SS.Year, SS.SheetName\r\n                        FROM Payroll_Transaction_Detail TD \r\n                            INNER JOIN Employee ON TD.EmployeeID=Employee.EmployeeID\r\n                            INNER JOIN SalarySheet_Detail SD ON SD.SysDocID = TD.SheetSysDocID AND SD.VoucherID = TD.SheetVoucherID AND SD.RowIndex = TD.SheetRowIndex\r\n                            INNER JOIN SalarySheet SS ON SS.SysDocID = TD.SheetSysDocID AND SS.VoucherID = TD.SheetVoucherID\r\n                            WHERE TD.VoucherID='" + voucherID + "' AND TD.SysDocID='" + sysDocID + "'";
				FillDataSet(payrollTransactionData, "Payroll_Transaction_Detail", textCommand, sqlTransaction);
				textCommand = "SELECT * FROM   Tax_Detail\r\n\t\t\t\t\t\tWHERE VoucherID='" + voucherID + "' AND SysDocID='" + sysDocID + "'";
				FillDataSet(payrollTransactionData, "Tax_Detail", textCommand);
				return payrollTransactionData;
			}
			catch
			{
				throw;
			}
		}

		private bool ReverseLoanDeduction(string sysDocID, string voucherID, SqlTransaction sqlTransaction)
		{
			bool flag = true;
			try
			{
				object fieldValue = new Databases(base.DBConfig).GetFieldValue("Payroll_Transaction", "IsVoid", "SysDocID", sysDocID, "VoucherID", voucherID, sqlTransaction);
				bool flag2 = false;
				if (fieldValue != null && fieldValue.ToString() != "")
				{
					flag2 = bool.Parse(fieldValue.ToString());
				}
				if (!flag2)
				{
					foreach (DataRow row in GetPayrollTransactionByID(sysDocID, voucherID, sqlTransaction).Tables["Payroll_Transaction_Detail"].Rows)
					{
						if (byte.Parse(row["PayType"].ToString()) == 3)
						{
							string text = row["LoanVoucherID"].ToString();
							string text2 = row["LoanSysDocID"].ToString();
							string text3 = row["EmployeeID"].ToString();
							decimal d = decimal.Parse(row["Amount"].ToString());
							if (!(d == 0m))
							{
								DataSet employeeLoanByID = new EmployeeLoan(base.DBConfig).GetEmployeeLoanByID(text2, text, sqlTransaction);
								if (employeeLoanByID == null || employeeLoanByID.Tables.Count == 0 || employeeLoanByID.Tables[0].Rows.Count == 0)
								{
									throw new CompanyException("The loan number you have entered does not exist. Employee:" + text3);
								}
								string commandText = "UPDATE Employee_Loan SET DeductedAmount= ISNULL(DeductedAmount,0) - " + d.ToString() + " WHERE SysDocID='" + text2 + "'\r\n                                  AND VoucherID='" + text + "' AND EmployeeID='" + text3 + "'";
								flag &= Update(commandText, sqlTransaction);
							}
						}
					}
					return flag;
				}
				return flag;
			}
			catch
			{
				return false;
			}
		}

		internal bool DeletePayrollTransactionDetailsRows(string sysDocID, string voucherID, SqlTransaction sqlTransaction)
		{
			bool flag = true;
			try
			{
				object fieldValue = new Databases(base.DBConfig).GetFieldValue("Payroll_Transaction", "IsVoid", "SysDocID", sysDocID, "VoucherID", voucherID, sqlTransaction);
				if (fieldValue != null && fieldValue.ToString() != "")
				{
					bool.Parse(fieldValue.ToString());
				}
				string commandText = "DELETE FROM Payroll_Transaction_Detail WHERE SysDocID = '" + sysDocID + "' AND VoucherID = '" + voucherID + "'";
				return flag & Delete(commandText, sqlTransaction);
			}
			catch
			{
				throw;
			}
		}

		public bool VoidPayrollTransaction(string sysDocID, string voucherID, bool isVoid)
		{
			bool flag = true;
			try
			{
				SqlTransaction sqlTransaction = base.DBConfig.StartNewTransaction();
				bool flag2 = false;
				object fieldValue = new Databases(base.DBConfig).GetFieldValue("Payroll_Transaction", "IsVoid", "SysDocID", sysDocID, "VoucherID", voucherID, sqlTransaction);
				if (fieldValue != null && fieldValue.ToString() != "")
				{
					flag2 = bool.Parse(fieldValue.ToString());
				}
				if (flag2 == isVoid)
				{
					throw new CompanyException("The transaction is already voided.");
				}
				flag &= ReverseSalarySheetChanges(sysDocID, voucherID, sqlTransaction);
				string exp = "UPDATE Payroll_Transaction SET IsVoid = '" + isVoid.ToString() + "' WHERE SysDocID='" + sysDocID + "' AND VoucherID = '" + voucherID + "'";
				flag &= (ExecuteNonQuery(exp, sqlTransaction) > 0);
				exp = "UPDATE Payroll_Transaction_Detail SET Amount = 0 WHERE SysDocID='" + sysDocID + "' AND VoucherID = '" + voucherID + "'";
				flag &= (ExecuteNonQuery(exp, sqlTransaction) > 0);
				flag &= new Journal(base.DBConfig).VoidJournal(sysDocID, voucherID, isVoid, sqlTransaction);
				flag &= new IssuedCheques(base.DBConfig).VoidChequeTransaction(sysDocID, voucherID, isVoid, sqlTransaction);
				if (!flag)
				{
					return flag;
				}
				ActivityTypes activityType = ActivityTypes.Void;
				if (!isVoid)
				{
					activityType = ActivityTypes.Unvoid;
				}
				AddActivityLog("Payroll Transaction", voucherID, sysDocID, activityType, sqlTransaction);
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

		private bool ReverseSalarySheetChanges(string sysDocID, string voucherID, SqlTransaction sqlTransaction)
		{
			bool flag = true;
			try
			{
				string textCommand = "SELECT DISTINCT SheetSysDocID,SheetVoucherID,EmployeeID, Amount FROM Payroll_Transaction_Detail PTD WHERE\r\n                           SysDocID='" + sysDocID + "' AND VoucherID='" + voucherID + "'";
				DataSet dataSet = new DataSet();
				FillDataSet(dataSet, "Payroll", textCommand);
				if (dataSet == null)
				{
					return flag;
				}
				if (dataSet.Tables.Count <= 0)
				{
					return flag;
				}
				if (dataSet.Tables[0].Rows.Count > 0)
				{
					foreach (DataRow row in dataSet.Tables[0].Rows)
					{
						string text = "";
						string text2 = "";
						string text3 = "";
						text = row["SheetSysDocID"].ToString();
						text2 = row["SheetVoucherID"].ToString();
						text3 = row["EmployeeID"].ToString();
						decimal num = decimal.Parse(row["Amount"].ToString());
						if (text2 != "")
						{
							textCommand = "UPDATE SalarySheet_Detail SET  PaidAmount = PaidAmount - " + num + " WHERE SysDocID='" + text + "' AND VoucherID = '" + text2 + "' AND EmployeeID='" + text3 + "'";
							flag &= Update(textCommand, sqlTransaction);
						}
					}
					return flag;
				}
				return flag;
			}
			catch
			{
				flag = false;
				throw;
			}
		}

		public bool DeletePayrollTransaction(string sysDocID, string voucherID)
		{
			bool flag = true;
			try
			{
				string text = "";
				SqlTransaction sqlTransaction = base.DBConfig.StartNewTransaction();
				flag &= ReverseSalarySheetChanges(sysDocID, voucherID, sqlTransaction);
				text = "DELETE FROM Payroll_Transaction WHERE SysDocID='" + sysDocID + "' AND VoucherID = '" + voucherID + "'";
				flag &= (ExecuteNonQuery(text, sqlTransaction) >= 0);
				flag &= DeletePayrollTransactionDetailsRows(sysDocID, voucherID, sqlTransaction);
				flag &= new Journal(base.DBConfig).DeleteJournal(sysDocID, voucherID, sqlTransaction);
				flag &= new IssuedCheques(base.DBConfig).DeleteChequeRows(sysDocID, voucherID, sqlTransaction);
				flag &= new TaxTransaction(base.DBConfig).DeleteTaxTransactionDetailsRows(sysDocID, voucherID, sqlTransaction);
				if (!flag)
				{
					return flag;
				}
				ActivityTypes activityType = ActivityTypes.Delete;
				AddActivityLog("Payroll Transaction", voucherID, sysDocID, activityType, sqlTransaction);
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

		private GLData CreatePayrollGLData(PayrollTransactionData transactionData)
		{
			GLData gLData = new GLData();
			int currencyDecimalPoints = new CompanyInformations(base.DBConfig).CurrencyDecimalPoints;
			DataRow dataRow = transactionData.PayrollTransactionTable.Rows[0];
			string value = dataRow["DivisionID"].ToString();
			string value2 = dataRow["CompanyID"].ToString();
			DataRow dataRow2 = gLData.JournalTable.NewRow();
			SysDocTypes sysDocTypes = SysDocTypes.PayrollTransaction;
			dataRow2["JournalID"] = 0;
			dataRow2["JournalDate"] = dataRow["TransactionDate"];
			dataRow2["SysDocID"] = dataRow["SysDocID"];
			dataRow2["SysDocType"] = (byte)sysDocTypes;
			dataRow2["VoucherID"] = dataRow["VoucherID"];
			dataRow2["Reference"] = dataRow["Reference"];
			dataRow2["CurrencyID"] = dataRow["CurrencyID"];
			dataRow2["CurrencyRate"] = dataRow["CurrencyRate"];
			dataRow2["Note"] = dataRow["Description"];
			dataRow2.EndEdit();
			gLData.JournalTable.Rows.Add(dataRow2);
			decimal d = default(decimal);
			decimal result = default(decimal);
			decimal result2 = default(decimal);
			DataRow dataRow3 = null;
			foreach (DataRow row in transactionData.PayrollTransactionDetailTable.Rows)
			{
				dataRow3 = gLData.JournalDetailsTable.NewRow();
				dataRow3.BeginEdit();
				dataRow3["JournalID"] = 0;
				dataRow3["IsARAP"] = true;
				dataRow3["PayeeType"] = "E";
				dataRow3["PayeeID"] = row["EmployeeID"];
				dataRow3["AccountID"] = row["AccountID"];
				dataRow3["Debit"] = row["Amount"];
				dataRow3["DebitFC"] = row["AmountFC"];
				dataRow3["Credit"] = DBNull.Value;
				dataRow3["CreditFC"] = DBNull.Value;
				d += decimal.Parse(row["Amount"].ToString());
				dataRow3["Description"] = row["Description"];
				dataRow3["Reference"] = dataRow["Reference"];
				dataRow3["RowIndex"] = row["RowIndex"];
				dataRow3["DivisionID"] = value;
				dataRow3["CompanyID"] = value2;
				dataRow3.EndEdit();
				gLData.JournalDetailsTable.Rows.Add(dataRow3);
			}
			decimal.TryParse(dataRow["OtherCharges"].ToString(), out result);
			decimal.TryParse(dataRow["TaxAmount"].ToString(), out result2);
			if (result != 0m)
			{
				dataRow3 = gLData.JournalDetailsTable.NewRow();
				dataRow3.BeginEdit();
				dataRow3["JournalID"] = 0;
				dataRow3["PayeeType"] = "A";
				dataRow3["AccountID"] = dataRow["OtherAccountID"];
				dataRow3["Debit"] = Math.Round(result, currencyDecimalPoints, MidpointRounding.AwayFromZero);
				dataRow3["DebitFC"] = DBNull.Value;
				dataRow3["Credit"] = DBNull.Value;
				dataRow3["CreditFC"] = DBNull.Value;
				dataRow3["Description"] = dataRow["Description"];
				dataRow3["Reference"] = dataRow["Reference"];
				dataRow3["RowIndex"] = -1;
				dataRow3["DivisionID"] = value;
				dataRow3["CompanyID"] = value2;
				dataRow3.EndEdit();
				gLData.JournalDetailsTable.Rows.Add(dataRow3);
			}
			if (result2 != 0m)
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
					dataRow3 = gLData.JournalDetailsTable.NewRow();
					dataRow3.BeginEdit();
					dataRow3["JournalID"] = 0;
					string text = "";
					text = obj["TaxItemID"].ToString();
					string text2 = "";
					string exp = "SELECT PurchaseTaxAccountID FROM Tax WHERE  TaxCode = '" + text.Trim() + "'";
					object obj2 = ExecuteScalar(exp);
					if (obj2 != null)
					{
						text2 = obj2.ToString();
					}
					if (text2 == "")
					{
						throw new CompanyException("AccountID is not set for tax item: " + text + ".");
					}
					decimal.TryParse(obj["TaxAmount"].ToString(), out num);
					dataRow3["AccountID"] = text2;
					dataRow3["PayeeType"] = "A";
					dataRow3["Debit"] = Math.Round(num, currencyDecimalPoints, MidpointRounding.AwayFromZero);
					dataRow3["Credit"] = DBNull.Value;
					dataRow3["Reference"] = dataRow["Reference"];
					dataRow3["DivisionID"] = value;
					dataRow3["CompanyID"] = value2;
					dataRow3.EndEdit();
					gLData.JournalDetailsTable.Rows.Add(dataRow3);
				}
			}
			if (d < 0m)
			{
				throw new CompanyException("Total amount cannot be negative.");
			}
			dataRow3 = gLData.JournalDetailsTable.NewRow();
			dataRow3.BeginEdit();
			dataRow3["JournalID"] = 0;
			dataRow3["PayeeType"] = "A";
			dataRow3["AccountID"] = dataRow["BankAccountID"];
			dataRow3["Debit"] = DBNull.Value;
			dataRow3["DebitFC"] = DBNull.Value;
			dataRow3["Credit"] = d + result + result2;
			dataRow3["Description"] = dataRow["Description"];
			dataRow3["Reference"] = dataRow["Reference"];
			dataRow3["RowIndex"] = -1;
			dataRow3["DivisionID"] = value;
			dataRow3["CompanyID"] = value2;
			dataRow3.EndEdit();
			gLData.JournalDetailsTable.Rows.Add(dataRow3);
			return gLData;
		}

		public DataSet GetEmployeeSalaryReport(DateTime from, DateTime to, string fromEmployee, string toEmployee, string fromDepartment, string toDepartment, string fromLocation, string toLocation, bool showZeroBalance)
		{
			string str = "";
			CommonLib.ToSqlDateTimeString(from);
			string str2 = CommonLib.ToSqlDateTimeString(to);
			string text = "SELECT DISTINCT PTD.EmployeeID,FirstName + ' ' + LastName AS [EmployeeName]\r\n                            FROM Employee    \r\n                            INNER JOIN Payroll_Transaction_Detail PTD ON Employee.EmployeeID=PTD.EmployeeID\r\n                            INNER JOIN Payroll_Transaction PT ON PT.SysDocID=PTD.SysDocID AND PT.VoucherID=PTD.VoucherID ";
			if (!showZeroBalance)
			{
				text = text + " AND TransactionDate < '" + str2 + "' ";
				text += " AND ISNULL(PT.IsVoid,'False')='False' ";
			}
			if (fromEmployee != "")
			{
				text = text + " AND PTD.EmployeeID BETWEEN '" + fromEmployee + "' AND '" + toEmployee + "' ";
			}
			if (fromEmployee != "")
			{
				str = str + " AND PTD.EmployeeID >= '" + fromEmployee + "' ";
			}
			if (toEmployee != "")
			{
				str = str + " AND PTD.EmployeeID <= '" + toEmployee + "' ";
			}
			if (fromDepartment != "")
			{
				str = str + " AND PTD.EmployeeID IN (SELECT EmployeeID FROM Employee WHERE DepartmentID >= '" + fromDepartment + "') ";
			}
			if (toDepartment != "")
			{
				str = str + " AND PTD.EmployeeID IN (SELECT EmployeeID FROM Employee WHERE DepartmentID <= '" + toDepartment + "') ";
			}
			if (fromLocation != "")
			{
				str = str + " AND PTD.EmployeeID IN (SELECT EmployeeID FROM Employee WHERE LocationID >= '" + fromLocation + "') ";
			}
			if (toLocation != "")
			{
				str = str + " AND PTD.EmployeeID IN (SELECT EmployeeID FROM Employee WHERE LocationID <= '" + toLocation + "') ";
			}
			text += " ORDER BY PTD.EmployeeID";
			DataSet dataSet = new DataSet();
			FillDataSet(dataSet, "Employee", text);
			DataSet dataSet2 = new DataSet();
			text = "SELECT PTD.SysDocID,PTD.VoucherID,PTD.EmployeeID,FirstName + ' ' + LastName AS [EmployeeName],PayType,[Month],StartDate,EndDate,PT.PaymentMethodType,TransactionDate,\r\n                        ISNULL(PayrollItemID,LoanVoucherID) AS PayrollItemID,Days,\r\n                        CASE ISNULL(PayType,1) WHEN 1 THEN PTD.Amount WHEN 2 THEN PTD.Amount * -1 WHEN 3 THEN PTD.Amount * -1 END AS Amount\r\n                        FROM Payroll_Transaction_Detail PTD \r\n                        INNER JOIN Payroll_Transaction PT ON PT.SysDocID=PTD.SysDocID AND PT.VoucherID=PTD.VoucherID\r\n                        INNER JOIN Employee ON Employee.EmployeeID=PTD.EmployeeID ";
			text += " AND ISNULL(PT.IsVoid,'False')='False' ";
			if (fromEmployee != "")
			{
				text = text + " AND PTD.EmployeeID BETWEEN '" + fromEmployee + "' AND '" + toEmployee + "' ";
			}
			if (fromDepartment != "")
			{
				str = str + " AND PTD.EmployeeID IN (SELECT EmployeeID FROM Employee WHERE DepartmentID >= '" + fromDepartment + "') ";
			}
			if (toDepartment != "")
			{
				str = str + " AND PTD.EmployeeID IN (SELECT EmployeeID FROM Employee WHERE DepartmentID <= '" + toDepartment + "') ";
			}
			if (fromLocation != "")
			{
				str = str + " AND PTD.EmployeeID IN (SELECT EmployeeID FROM Employee WHERE LocationID >= '" + fromLocation + "') ";
			}
			if (toLocation != "")
			{
				str = str + " AND PTD.EmployeeID IN (SELECT EmployeeID FROM Employee WHERE LocationID <= '" + toLocation + "') ";
			}
			text += " ORDER BY PTD.EmployeeID,TransactionDate";
			FillDataSet(dataSet2, "Payroll_Transaction", text);
			dataSet.Merge(dataSet2);
			dataSet.Relations.Add("Balance Detail", dataSet.Tables["Employee"].Columns["EmployeeID"], dataSet.Tables["Payroll_Transaction"].Columns["EmployeeID"], createConstraints: false);
			return dataSet;
		}

		public DataSet GetSalaryCashList(DateTime from, DateTime to, bool isImport, bool showVoid)
		{
			DataSet dataSet = new DataSet();
			string text = StoreConfiguration.ToSqlDateTimeString(from);
			string text2 = StoreConfiguration.ToSqlDateTimeString(to);
			string text3 = "SELECT   P.SysDocID,P.VoucherID,CAST(DATENAME(month,SS.StartDate )  AS CHAR(10))AS SalMonth, SS.Year, SS.SheetName\r\n                               FROM Payroll_Transaction P\r\n                                INNER JOIN Payroll_Transaction_Detail TD ON P.SysDocID=TD.SysDocID AND P.VoucherID=TD.VoucherID\r\n                                INNER JOIN SalarySheet SS ON SS.SysDocID = TD.SheetSysDocID AND SS.VoucherID = TD.SheetVoucherID \r\n                             WHERE P.PaymentMethodType=1  ";
			if (from != DateTime.MinValue)
			{
				text3 = text3 + " AND P.TransactionDate Between '" + text + "' AND '" + text2 + "'";
			}
			SqlCommand sqlCommand = new SqlCommand(text3);
			FillDataSet(dataSet, "Payroll_Transaction", sqlCommand);
			return dataSet;
		}

		public DataSet GetSalaryChequeList(DateTime from, DateTime to, bool isImport, bool showVoid)
		{
			DataSet dataSet = new DataSet();
			string text = StoreConfiguration.ToSqlDateTimeString(from);
			string text2 = StoreConfiguration.ToSqlDateTimeString(to);
			string text3 = "SELECT   P.SysDocID,P.VoucherID,CAST(DATENAME(month,SS.StartDate )  AS CHAR(10))AS SalMonth, SS.Year, SS.SheetName\r\n                               FROM Payroll_Transaction P\r\n                                INNER JOIN Payroll_Transaction_Detail TD ON P.SysDocID=TD.SysDocID AND P.VoucherID=TD.VoucherID\r\n                                INNER JOIN SalarySheet SS ON SS.SysDocID = TD.SheetSysDocID AND SS.VoucherID = TD.SheetVoucherID \r\n                             WHERE P.PaymentMethodType=2  ";
			if (from != DateTime.MinValue)
			{
				text3 = text3 + " AND P.TransactionDate Between '" + text + "' AND '" + text2 + "'";
			}
			SqlCommand sqlCommand = new SqlCommand(text3);
			FillDataSet(dataSet, "Payroll_Transaction", sqlCommand);
			return dataSet;
		}

		public DataSet GetSalaryBankList(DateTime from, DateTime to, bool isImport, bool showVoid)
		{
			DataSet dataSet = new DataSet();
			string text = StoreConfiguration.ToSqlDateTimeString(from);
			string text2 = StoreConfiguration.ToSqlDateTimeString(to);
			string text3 = "SELECT DISTINCT  P.SysDocID,P.VoucherID,CAST(DATENAME(month,SS.StartDate )  AS CHAR(10))AS SalMonth, SS.Year, SS.SheetName\r\n                               FROM Payroll_Transaction P\r\n                                INNER JOIN Payroll_Transaction_Detail TD ON P.SysDocID=TD.SysDocID AND P.VoucherID=TD.VoucherID\r\n                                INNER JOIN SalarySheet SS ON SS.SysDocID = TD.SheetSysDocID AND SS.VoucherID = TD.SheetVoucherID \r\n                             WHERE P.PaymentMethodType=4  ";
			if (from != DateTime.MinValue)
			{
				text3 = text3 + " AND P.TransactionDate Between '" + text + "' AND '" + text2 + "'";
			}
			SqlCommand sqlCommand = new SqlCommand(text3);
			FillDataSet(dataSet, "Payroll_Transaction", sqlCommand);
			return dataSet;
		}

		public DataSet GetEmployeeSalaryToPrint(string sysDocID, string voucherID)
		{
			try
			{
				PayrollTransactionData payrollTransactionData = new PayrollTransactionData();
				string cmdText = "SELECT PT.*,A.AccountName,(SELECT BankName FROM Chequebook C LEFT JOIN Bank B ON C.BankID=B.BankID WHERE C.ChequebookID=PT.ChequebookID) AS 'PayeeBank' FROM Payroll_Transaction PT LEFT JOIN Account A ON PT.BankAccountID=A.AccountID  WHERE PT.VoucherID='" + voucherID + "' AND PT.SysDocID='" + sysDocID + "'";
				SqlCommand sqlCommand = new SqlCommand(cmdText);
				FillDataSet(payrollTransactionData, "Payroll_Transaction", sqlCommand);
				if (payrollTransactionData == null || payrollTransactionData.Tables.Count == 0 || payrollTransactionData.Tables["Payroll_Transaction"].Rows.Count == 0)
				{
					return null;
				}
				cmdText = "SELECT TD.*,Employee.FirstName + ' ' + Employee.LastName AS EmployeeName, SD.NetSalary, SD.PaidAmount, SD.NetSalary - ISNULL(SD.PaidAmount, 0) AS Balance, SS.Month, SS.Year, SS.SheetName\r\n                        ,ISNULL((Select Sum(Amount) AS MonthSalary from Employee_PayrollItem_Detail where EmployeeID=TD.EmployeeID),0) AS [Monthly Salary]\r\n                            FROM Payroll_Transaction_Detail TD \r\n                            INNER JOIN Employee ON TD.EmployeeID=Employee.EmployeeID\r\n                            INNER JOIN SalarySheet_Detail SD ON SD.SysDocID = TD.SheetSysDocID AND SD.VoucherID = TD.SheetVoucherID AND SD.RowIndex = TD.SheetRowIndex\r\n                            INNER JOIN SalarySheet SS ON SS.SysDocID = TD.SheetSysDocID AND SS.VoucherID = TD.SheetVoucherID\r\n                            WHERE TD.VoucherID='" + voucherID + "' AND TD.SysDocID='" + sysDocID + "'";
				FillDataSet(payrollTransactionData, "Payroll_Transaction_Detail", cmdText);
				payrollTransactionData.Relations.Add("PayrollDetail", new DataColumn[2]
				{
					payrollTransactionData.Tables["Payroll_Transaction"].Columns["SysDocID"],
					payrollTransactionData.Tables["Payroll_Transaction"].Columns["VoucherID"]
				}, new DataColumn[2]
				{
					payrollTransactionData.Tables["Payroll_Transaction_Detail"].Columns["SysDocID"],
					payrollTransactionData.Tables["Payroll_Transaction_Detail"].Columns["VoucherID"]
				}, createConstraints: false);
				return payrollTransactionData;
			}
			catch
			{
				throw;
			}
		}

		public DataSet GeWPFToPrint(string sysDocID, string voucherID)
		{
			DataSet dataSet = new DataSet();
			string text = "SELECT TD.SysDocID,TD.VoucherID,TD.EmployeeID,TD.AccountID,TD.Days,TD.Amount,TD.RowIndex,TD.SheetSysDocID,TD.SheetVoucherID,TD.SheetRowIndex,\r\n                           E.FirstName + ' ' + E.LastName AS EmployeeName, SD.NetSalary, SD.PaidAmount,E.IBAN,B.RoutingCode,E.LabourID,S.MOLId,SS.StartDate,SS.EndDate,\r\n                           SD.NetSalary - ISNULL(SD.PaidAmount, 0) AS Balance, SS.Month, SS.Year, SS.SheetName,E.BankID,B.BankName,\r\n                           (SELECT SUM(SDI.PayableAmount) FROM SalarySheet_Detail_Item SDI INNER JOIN PayrollItem P ON P.PayrollItemID=SDI.PayrollItemID  AND ISNULL(P.InFixed,0)=1  WHERE SDI.SysDocID= TD.SheetSysDocID AND  SDI.VoucherID=TD.SheetVoucherID AND SDI.EmployeeID=TD.EmployeeID ) [Fixed Salary],\r\n                           (SELECT SUM(SDI.PayableAmount) FROM SalarySheet_Detail_Item SDI INNER JOIN PayrollItem P ON P.PayrollItemID=SDI.PayrollItemID AND ISNULL(P.InFixed,0)=0 WHERE SDI.SysDocID= TD.SheetSysDocID AND  SDI.VoucherID=TD.SheetVoucherID AND SDI.EmployeeID=TD.EmployeeID ) [Variable Salary],\r\n                           (DAY(EOMONTH(SS.StartDate))-TD.Days) AS AbscentDay\r\n                           FROM Payroll_Transaction_Detail TD \r\n                           INNER JOIN Employee E ON TD.EmployeeID=E.EmployeeID\r\n                           LEFT OUTER JOIN Sponsor S ON E.SponsorID=S.SponsorID\r\n                           LEFT OUTER JOIN Bank B ON E.BankID=B.BankID\r\n                           INNER JOIN SalarySheet_Detail SD ON SD.SysDocID = TD.SheetSysDocID AND SD.VoucherID = TD.SheetVoucherID AND SD.RowIndex = TD.SheetRowIndex\r\n                           INNER JOIN SalarySheet SS ON SS.SysDocID = TD.SheetSysDocID AND SS.VoucherID = TD.SheetVoucherID";
			text = text + " WHERE TD.SysDocID= '" + sysDocID + "' AND TD.VoucherID= '" + voucherID + "'";
			SqlCommand sqlCommand = new SqlCommand(text);
			FillDataSet(dataSet, "Payroll_Transaction", sqlCommand);
			return dataSet;
		}

		public decimal GetPaidSalaryAmount(string sheetSysDocID, string sheetVoucherID, string employeeID)
		{
			decimal result = default(decimal);
			string exp = "Select SUM(Amount) from Payroll_Transaction_Detail where SheetSysDocID='" + sheetSysDocID + "' AND SheetVoucherID='" + sheetVoucherID + "' and EmployeeID= '" + employeeID + "'";
			object obj = ExecuteScalar(exp);
			if (obj != null)
			{
				decimal.TryParse(obj.ToString(), out result);
			}
			return result;
		}
	}
}
