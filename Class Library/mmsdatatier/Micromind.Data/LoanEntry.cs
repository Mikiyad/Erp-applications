using Micromind.Common;
using Micromind.Common.Data;
using Micromind.Common.Libraries;
using System;
using System.Data;
using System.Data.SqlClient;

namespace Micromind.Data
{
	public sealed class LoanEntry : StoreObject
	{
		public const string VOUCHERID_PARM = "@VoucherID";

		public const string SYSDOCID_PARM = "@SysDocID";

		public const string LOANACCOUNTID_PARM = "@LoanAccountID";

		public const string INTERESTACCOUNTID_PARM = "@InterestAccountID";

		public const string LOANREPAYMENTACCOUNTID_PARM = "@LoanRepaymentAccountID";

		public const string LOANDATE_PARM = "@LoanDate";

		public const string INSTALLMENTNUMBER_PARM = "@InstallmentNumber";

		public const string LOANAMOUNT_PARM = "@LoanAmount";

		public const string DEDSTARTDATE_PARM = "@DedStartDate";

		public const string INTERESTRATE_PARM = "@InterestRate";

		public const string DESCRIPTION_PARM = "@Description";

		public const string NOTE_PARM = "@Note";

		public const string LOANTERMTYPE_PARM = "@LoanTermType";

		public const string MONTHLYEMI_PARM = "@MonthlyEMI";

		public const string CREATEDBY_PARM = "@CreatedBy";

		public const string DATECREATED_PARM = "@DateCreated";

		public const string UPDATEDBY_PARM = "@UpdatedBy";

		public const string DATEUPDATED_PARM = "@DateUpdated";

		public const string ISVOID_PARM = "@IsVoid";

		public const string ISCLOSED_PARM = "@IsClosed";

		private const string LOANENTRY_TABLE = "Loan_Entry";

		public const string LOANENTRYDETAIL_TABLE = "Loan_Entry_Detail";

		public const string LOANSYSDOCID_PARM = "@LoanSysDocID";

		public const string LOANVOUCHERID_PARM = "@LoanVoucherID";

		public const string INSTALLMENT_PARM = "@Installment";

		public const string TRANSACTIONDATE_PARM = "@TransactionDate";

		public const string INSTALLMENTAMOUNT_PARM = "@InstallmentAmount";

		public const string PRINCIPLE_PARM = "@Principle";

		public const string INTEREST_PARM = "@Interest";

		public const string OUTSTANDINGPAYMENT_PARM = "@OutStandingPayment";

		public LoanEntry(Config config)
			: base(config)
		{
		}

		private string GetInsertUpdateLoanEntryText(bool isUpdate)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddInsertUpdateParameters("Loan_Entry", new FieldValue("SysDocID", "@SysDocID", isUpdateConditionField: true), new FieldValue("VoucherID", "@VoucherID", isUpdateConditionField: true), new FieldValue("LoanAccountID", "@LoanAccountID"), new FieldValue("LoanDate", "@LoanDate"), new FieldValue("InterestAccountID", "@InterestAccountID"), new FieldValue("LoanRepaymentAccountID", "@LoanRepaymentAccountID"), new FieldValue("DedStartDate", "@DedStartDate"), new FieldValue("InterestRate", "@InterestRate"), new FieldValue("Description", "@Description"), new FieldValue("LoanAmount", "@LoanAmount"), new FieldValue("InstallmentNumber", "@InstallmentNumber"), new FieldValue("LoanTermType", "@LoanTermType"), new FieldValue("MonthlyEMI", "@MonthlyEMI"), new FieldValue("Note", "@Note"));
			if (isUpdate)
			{
				sqlBuilder.AddInsertUpdateParameters("Loan_Entry", new FieldValue("DateUpdated", "@DateUpdated", isUpdateConditionField: true, checkForNullValue: true));
			}
			if (isUpdate)
			{
				return sqlBuilder.GetUpdateExpression();
			}
			return sqlBuilder.GetInsertExpression();
		}

		private SqlCommand GetInsertUpdateLoanEntryCommand(bool isUpdate)
		{
			SqlParameterCollection parameters;
			if (isUpdate)
			{
				if (updateCommand != null)
				{
					updateCommand = null;
				}
				updateCommand = new SqlCommand(GetInsertUpdateLoanEntryText(isUpdate: true), base.DBConfig.Connection);
				updateCommand.CommandType = CommandType.Text;
				parameters = updateCommand.Parameters;
			}
			else
			{
				if (insertCommand != null)
				{
					insertCommand = null;
				}
				insertCommand = new SqlCommand(GetInsertUpdateLoanEntryText(isUpdate: false), base.DBConfig.Connection);
				insertCommand.CommandType = CommandType.Text;
				parameters = insertCommand.Parameters;
			}
			parameters.Add("@SysDocID", SqlDbType.NVarChar);
			parameters.Add("@VoucherID", SqlDbType.NVarChar);
			parameters.Add("@LoanAccountID", SqlDbType.NVarChar);
			parameters.Add("@LoanDate", SqlDbType.DateTime);
			parameters.Add("@InterestAccountID", SqlDbType.NVarChar);
			parameters.Add("@LoanRepaymentAccountID", SqlDbType.NVarChar);
			parameters.Add("@DedStartDate", SqlDbType.DateTime);
			parameters.Add("@InterestRate", SqlDbType.NVarChar);
			parameters.Add("@Description", SqlDbType.NVarChar);
			parameters.Add("@LoanAmount", SqlDbType.Money);
			parameters.Add("@InstallmentNumber", SqlDbType.NVarChar);
			parameters.Add("@LoanTermType", SqlDbType.TinyInt);
			parameters.Add("@MonthlyEMI", SqlDbType.Money);
			parameters.Add("@Note", SqlDbType.NVarChar);
			parameters["@SysDocID"].SourceColumn = "SysDocID";
			parameters["@VoucherID"].SourceColumn = "VoucherID";
			parameters["@LoanAccountID"].SourceColumn = "LoanAccountID";
			parameters["@LoanDate"].SourceColumn = "LoanDate";
			parameters["@InterestAccountID"].SourceColumn = "InterestAccountID";
			parameters["@LoanRepaymentAccountID"].SourceColumn = "LoanRepaymentAccountID";
			parameters["@DedStartDate"].SourceColumn = "DedStartDate";
			parameters["@InterestRate"].SourceColumn = "InterestRate";
			parameters["@Description"].SourceColumn = "Description";
			parameters["@LoanAccountID"].SourceColumn = "LoanAccountID";
			parameters["@LoanAmount"].SourceColumn = "LoanAmount";
			parameters["@InstallmentNumber"].SourceColumn = "InstallmentNumber";
			parameters["@LoanTermType"].SourceColumn = "LoanTermType";
			parameters["@MonthlyEMI"].SourceColumn = "MonthlyEMI";
			parameters["@Note"].SourceColumn = "Note";
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

		private string GetInsertUpdateLoanEntryDetailText(bool isUpdate)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddInsertUpdateParameters("Loan_Entry_Detail", new FieldValue("LoanSysDocID", "@LoanSysDocID"), new FieldValue("LoanVoucherID", "@LoanVoucherID"), new FieldValue("Installment", "@Installment"), new FieldValue("TransactionDate", "@TransactionDate"), new FieldValue("InstallmentAmount", "@InstallmentAmount"), new FieldValue("Principle", "@Principle"), new FieldValue("Interest", "@Interest"), new FieldValue("OutStandingPayment", "@OutStandingPayment"));
			if (isUpdate)
			{
				return sqlBuilder.GetUpdateExpression();
			}
			return sqlBuilder.GetInsertExpression();
		}

		private SqlCommand GetInsertUpdateLoanEntryDetailCommand(bool isUpdate)
		{
			SqlParameterCollection parameters;
			if (isUpdate)
			{
				if (updateCommand != null)
				{
					updateCommand = null;
				}
				updateCommand = new SqlCommand(GetInsertUpdateLoanEntryDetailText(isUpdate: true), base.DBConfig.Connection);
				updateCommand.CommandType = CommandType.Text;
				parameters = updateCommand.Parameters;
			}
			else
			{
				if (insertCommand != null)
				{
					insertCommand = null;
				}
				insertCommand = new SqlCommand(GetInsertUpdateLoanEntryDetailText(isUpdate: false), base.DBConfig.Connection);
				insertCommand.CommandType = CommandType.Text;
				parameters = insertCommand.Parameters;
			}
			parameters.Add("@LoanSysDocID", SqlDbType.NVarChar);
			parameters.Add("@LoanVoucherID", SqlDbType.NVarChar);
			parameters.Add("@Installment", SqlDbType.NVarChar);
			parameters.Add("@TransactionDate", SqlDbType.DateTime);
			parameters.Add("@InstallmentAmount", SqlDbType.Money);
			parameters.Add("@Principle", SqlDbType.Money);
			parameters.Add("@Interest", SqlDbType.Money);
			parameters.Add("@OutStandingPayment", SqlDbType.Money);
			parameters["@LoanSysDocID"].SourceColumn = "LoanSysDocID";
			parameters["@LoanVoucherID"].SourceColumn = "LoanVoucherID";
			parameters["@Installment"].SourceColumn = "Installment";
			parameters["@TransactionDate"].SourceColumn = "TransactionDate";
			parameters["@InstallmentAmount"].SourceColumn = "InstallmentAmount";
			parameters["@Principle"].SourceColumn = "Principle";
			parameters["@Interest"].SourceColumn = "Interest";
			parameters["@OutStandingPayment"].SourceColumn = "OutStandingPayment";
			if (isUpdate)
			{
				return updateCommand;
			}
			return insertCommand;
		}

		public bool InsertUpdateLoanEntry(LoanEntryData loanEntryData, bool isUpdate)
		{
			bool flag = true;
			string text = "";
			try
			{
				DataRow dataRow = loanEntryData.LoanEntryTable.Rows[0];
				string text2 = dataRow["VoucherID"].ToString();
				string text3 = dataRow["SysDocID"].ToString();
				SqlCommand insertUpdateLoanEntryCommand = GetInsertUpdateLoanEntryCommand(isUpdate);
				SqlTransaction sqlTransaction = base.DBConfig.StartNewTransaction();
				if (isUpdate)
				{
					text = "SELECT CASE WHEN ISNULL(PaidAmount,0) + ISNULL(DiscountAmount,0)=0 THEN 'True' ELSE 'False' END AS CanEdit FROM Employee_Loan WHERE SysDocID = '" + text3 + "' AND VoucherID='" + text2 + "'";
					object obj = ExecuteScalar(text, sqlTransaction);
					if (obj != null && obj.ToString() != "" && !bool.Parse(obj.ToString()))
					{
						throw new CompanyException("This loan is already partially or fully paid and cannot be edited.", 1023);
					}
				}
				insertUpdateLoanEntryCommand.Transaction = sqlTransaction;
				flag = (isUpdate ? (flag & Update(loanEntryData, "Loan_Entry", insertUpdateLoanEntryCommand)) : (flag & Insert(loanEntryData, "Loan_Entry", insertUpdateLoanEntryCommand)));
				if (isUpdate)
				{
					DeleteLoanDetailsRows(sqlTransaction, text3, text2, isPaymentRow: false);
				}
				flag &= InsertLoanEntryDetail(loanEntryData, sqlTransaction);
				if (!flag)
				{
					throw new Exception("Failed to save the transaction because of an unexpected error. Please try again.");
				}
				flag &= UpdateTableRowInsertUpdateInfo("Loan_Entry", "SysDocID", dataRow["SysDocID"].ToString(), "VoucherID", dataRow["VoucherID"].ToString(), sqlTransaction, !isUpdate);
				string entityName = "Loan Entry";
				flag = (isUpdate ? (flag & AddActivityLog(entityName, text2, text3, ActivityTypes.Update, sqlTransaction)) : (flag & AddActivityLog(entityName, text2, text3, ActivityTypes.Add, sqlTransaction)));
				if (!isUpdate)
				{
					flag &= new SystemDocuments(base.DBConfig).UpdateNextDocumentNumber(dataRow["SysDocID"].ToString(), dataRow["VoucherID"].ToString(), "Loan_Entry", "VoucherID", sqlTransaction);
				}
				if (!flag)
				{
					return flag;
				}
				flag &= new Approval(base.DBConfig).CreateTransactionApprovalTasks(SysDocTypes.LoanEntry, text3, text2, "Loan_Entry", sqlTransaction);
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

		internal bool InsertLoanEntryDetail(LoanEntryData loanEntryData, SqlTransaction sqlTransaction)
		{
			bool flag = true;
			try
			{
				SqlCommand insertUpdateLoanEntryDetailCommand = GetInsertUpdateLoanEntryDetailCommand(isUpdate: false);
				insertUpdateLoanEntryDetailCommand.Transaction = sqlTransaction;
				if (loanEntryData.Tables["Loan_Entry_Detail"].Rows.Count > 0)
				{
					return flag & Insert(loanEntryData, "Loan_Entry_Detail", insertUpdateLoanEntryDetailCommand);
				}
				return flag;
			}
			catch
			{
				flag = false;
				throw;
			}
		}

		internal bool UpdateLoanPaidAmount(string loanSysDocID, string loanVoucherID, SqlTransaction sqlTransaction)
		{
			try
			{
				string exp = "UPDATE Employee_Loan SET PaidAmount = (SELECT SUM(ISNULL(Credit,0)) AS AMT FROM Employee_Loan_Detail \r\n                                WHERE LoanSysDocID = '" + loanSysDocID + "' AnD LoanVoucherID = '" + loanVoucherID + "') \r\n\t\t\t\t\t\t        WHERE SysDocID = '" + loanSysDocID + "' AND VoucherID = '" + loanVoucherID + "'";
				return ExecuteNonQuery(exp, sqlTransaction) > 0;
			}
			catch
			{
				throw;
			}
		}

		internal bool UpdateLoanPaidAmountWhileDelete(string loanSysDocID, string loanVoucherID, SqlTransaction sqlTransaction)
		{
			try
			{
				string exp = "update Employee_Loan  set PaidAmount  = (select sum(isnull(ed.Credit,0)) from   Employee_Loan_Detail ED\r\n                                WHERE  ED.LoanSysDocID = '" + loanSysDocID + "' AND ED.LoanVoucherID= '" + loanVoucherID + "') \r\n\t\t\t\t\t\t        WHERE SysDocID = '" + loanSysDocID + "' AND VoucherID = '" + loanVoucherID + "'";
				return ExecuteNonQuery(exp, sqlTransaction) > 0;
			}
			catch
			{
				throw;
			}
		}

		internal bool DeleteLoanDetailsRows(SqlTransaction sqlTransaction, string sysDocID, string voucherID, bool isPaymentRow)
		{
			bool flag = true;
			try
			{
				string commandText = "DELETE FROM Loan_Entry_Detail WHERE LoanSysDocID = '" + sysDocID + "' AND LoanVoucherID = '" + voucherID + "'";
				return flag & Delete(commandText, sqlTransaction);
			}
			catch
			{
				flag = false;
				throw;
			}
		}

		internal bool DeleteLoanPaidAmountWhileDelete(string SysDocID, string VoucherID, SqlTransaction sqlTransaction)
		{
			try
			{
				string exp = "UPDATE Employee_Loan  SET PaidAmount=(SELECT SUM(ISNULL(Credit,0)) FROM Employee_Loan_Detail ELD \r\n                                WHERE ELD.LoanSysDocID=Employee_Loan.SysDocID AND ELD.LoanVoucherID=Employee_Loan.VoucherID)";
				return ExecuteNonQuery(exp, sqlTransaction) >= 0;
			}
			catch
			{
				throw;
			}
		}

		public DataSet GetListLoanEntry(DateTime from, DateTime to, bool showVoid)
		{
			DataSet dataSet = new DataSet();
			string text = StoreConfiguration.ToSqlDateTimeString(from);
			string text2 = StoreConfiguration.ToSqlDateTimeString(to);
			string text3 = "SELECT    SysDocID [Doc ID],VoucherID [Doc Number],LoanDate [Loan Date]                                         \r\n                            FROM         Loan_Entry LE ";
			if (from != DateTime.MinValue)
			{
				text3 = text3 + " WHERE LoanDate Between '" + text + "' AND '" + text2 + "'";
			}
			SqlCommand sqlCommand = new SqlCommand(text3);
			FillDataSet(dataSet, "Loan_Entry", sqlCommand);
			return dataSet;
		}

		public LoanEntryData GetLoanEntryByID(string sysDocID, string voucherID)
		{
			return GetLoanEntryByID(sysDocID, voucherID, null);
		}

		public LoanEntryData GetLoanEntryByID(string sysDocID, string voucherID, SqlTransaction sqlTransaction)
		{
			try
			{
				LoanEntryData loanEntryData = new LoanEntryData();
				string textCommand = "SELECT *\r\n                                FROM Loan_Entry LE WHERE SysDocID = '" + sysDocID + "' AND VoucherID='" + voucherID + "'";
				FillDataSet(loanEntryData, "Loan_Entry", textCommand, sqlTransaction);
				if (loanEntryData == null || loanEntryData.Tables.Count == 0 || loanEntryData.Tables["Loan_Entry"].Rows.Count == 0)
				{
					return null;
				}
				textCommand = "SELECT LED.* FROM Loan_Entry_Detail LED\r\n                                    WHERE LoanSysDocID = '" + sysDocID + "' AND LoanVoucherID='" + voucherID + "'";
				FillDataSet(loanEntryData, "Loan_Entry_Detail", textCommand, sqlTransaction);
				return loanEntryData;
			}
			catch
			{
				throw;
			}
		}

		public EmployeeLoanData GetEmployeeLoanPaymentByID(string sysDocID, string voucherID)
		{
			try
			{
				return new EmployeeLoanData();
			}
			catch
			{
				throw;
			}
		}

		public DataSet GetFirstEmployeeLoanByID(string voucherID, string employeeID, SqlTransaction sqlTransaction)
		{
			return null;
		}

		public bool CanEditLoan(string sysDocID, string voucherID)
		{
			try
			{
				bool flag = true;
				string exp = "SELECT CASE WHEN Amount=0 THEN 'True' \r\n                                WHEN (Amount<>0) AND ISNULL(PaidAmount,0)+ ISNULL(DiscountAmount,0)=0 THEN 'True' ELSE 'False' END\r\n                                FROM Employee_Loan WHERE SysDocID='" + sysDocID + "' AND VoucherID='" + voucherID + "'";
				object obj = ExecuteScalar(exp);
				if (obj != null && obj.ToString() != "")
				{
					flag &= bool.Parse(obj.ToString());
				}
				return flag;
			}
			catch
			{
				throw;
			}
		}

		public bool DeleteLoanEntry(string sysDocID, string voucherID)
		{
			bool flag = true;
			try
			{
				SqlTransaction sqlTransaction = base.DBConfig.StartNewTransaction();
				string commandText = "DELETE FROM Loan_Entry WHERE SysDocID = '" + sysDocID + "' AND VoucherID='" + voucherID + "'";
				flag &= Delete(commandText, sqlTransaction);
				flag &= DeleteLoanDetailsRows(sqlTransaction, sysDocID, voucherID, isPaymentRow: false);
				if (!flag)
				{
					return flag;
				}
				ActivityTypes activityType = ActivityTypes.Delete;
				AddActivityLog("Employee Loan", voucherID, sysDocID, activityType, sqlTransaction);
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

		public bool DeleteLoanPayment(string sysDocID, string voucherID)
		{
			bool flag = true;
			try
			{
				SqlTransaction sqlTransaction = base.DBConfig.StartNewTransaction();
				string commandText = "DELETE FROM Employee_Loan_Payment WHERE SysDocID = '" + sysDocID + "' AND VoucherID='" + voucherID + "'";
				flag &= Delete(commandText, sqlTransaction);
				flag &= DeleteLoanDetailsRows(sqlTransaction, sysDocID, voucherID, isPaymentRow: true);
				flag &= new Journal(base.DBConfig).DeleteJournal(sysDocID, voucherID, sqlTransaction);
				DataSet dataSet = new DataSet();
				string textCommand = " SELECT LoanSysDocID,LoanVoucherID FROM Employee_Loan_Payment WHERE SysDocID = '" + sysDocID + "' AND VOucherID = '" + voucherID + "'";
				FillDataSet(dataSet, "Loan", textCommand, sqlTransaction);
				string loanSysDocID = dataSet.Tables["Loan"].Rows[0]["LoanSysDocID"].ToString();
				string loanVoucherID = dataSet.Tables["Loan"].Rows[0]["LoanVoucherID"].ToString();
				flag &= UpdateLoanPaidAmount(loanSysDocID, loanVoucherID, sqlTransaction);
				if (!flag)
				{
					return flag;
				}
				ActivityTypes activityType = ActivityTypes.Delete;
				AddActivityLog("Employee Loan Payment", voucherID, sysDocID, activityType, sqlTransaction);
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

		private GLData CreateLoanGLEntry(EmployeeLoanData employeeLoanData, SqlTransaction sqlTransaction)
		{
			try
			{
				GLData gLData = new GLData();
				DataRow dataRow = employeeLoanData.EmployeeLoanTable.Rows[0];
				string text = "";
				string text2 = "";
				string text3 = dataRow["SysDocID"].ToString();
				string str = dataRow["VoucherID"].ToString();
				string text4 = dataRow["EmployeeID"].ToString();
				string textCommand = "SELECT ISNULL(Emp.AccountID, ISNULL(CLS.AccountID,LOC.EmployeeAccountID )) AS EmployeeAccountID\r\n                                FROM System_Document SD INNER JOIN Location LOC ON SD.LocationID = LOC.LocationID\r\n                                LEFT OUTER JOIN Employee EMP ON EmployeeID = '" + text4 + "'\r\n                                LEFT OUTER JOIN Employee_Type CLS ON EMP.ContractType = CLS.TypeID\r\n                                WHERE SysDocID = '" + text3 + "'";
				DataSet dataSet = new DataSet();
				FillDataSet(dataSet, "Accounts", textCommand, sqlTransaction);
				if (dataSet == null || dataSet.Tables.Count == 0 || dataSet.Tables[0].Rows.Count == 0)
				{
					throw new CompanyException("There is no location assigned to this system document or location record is missing.");
				}
				text = dataSet.Tables["Accounts"].Rows[0]["EmployeeAccountID"].ToString();
				string idFieldValue = dataRow["LoanType"].ToString();
				text2 = (string)(dataRow["LoanAccountID"] = new Databases(base.DBConfig).GetFieldValue("Employee_Loan_Type", "AccountID", "LoanTypeID", idFieldValue, sqlTransaction).ToString());
				DataRow dataRow2 = gLData.JournalTable.NewRow();
				SysDocTypes sysDocTypes = SysDocTypes.EmployeeLoan;
				dataRow2["JournalID"] = 0;
				dataRow2["JournalDate"] = dataRow["TransactionDate"];
				dataRow2["SysDocID"] = dataRow["SysDocID"];
				dataRow2["SysDocType"] = (byte)sysDocTypes;
				dataRow2["VoucherID"] = dataRow["VoucherID"];
				dataRow2["Reference"] = dataRow["Reference"];
				dataRow2["Narration"] = "Employee Loan - " + str;
				dataRow2.EndEdit();
				gLData.JournalTable.Rows.Add(dataRow2);
				decimal num = default(decimal);
				num = decimal.Parse(dataRow["Amount"].ToString());
				DataRow dataRow3 = gLData.JournalDetailsTable.NewRow();
				dataRow3.BeginEdit();
				dataRow3["JournalID"] = 0;
				dataRow3["AccountID"] = text2;
				dataRow3["PayeeID"] = text4;
				dataRow3["Debit"] = num;
				dataRow3["Credit"] = DBNull.Value;
				dataRow3["Description"] = "Employee Loan Granted";
				dataRow3["Reference"] = dataRow["Reference"];
				dataRow3.EndEdit();
				gLData.JournalDetailsTable.Rows.Add(dataRow3);
				dataRow3 = gLData.JournalDetailsTable.NewRow();
				dataRow3.BeginEdit();
				dataRow3["JournalID"] = 0;
				dataRow3["AccountID"] = text;
				dataRow3["PayeeID"] = text4;
				dataRow3["PayeeType"] = "E";
				dataRow3["IsARAP"] = true;
				dataRow3["Description"] = "Loan Disbursement-No:" + str;
				dataRow3["Debit"] = DBNull.Value;
				dataRow3["Credit"] = num;
				dataRow3["Reference"] = dataRow["Reference"];
				dataRow3.EndEdit();
				gLData.JournalDetailsTable.Rows.Add(dataRow3);
				return gLData;
			}
			catch
			{
				throw;
			}
		}

		private GLData CreateLoanPaymentGLEntry(EmployeeLoanData employeeLoanData, SqlTransaction sqlTransaction)
		{
			try
			{
				GLData gLData = new GLData();
				DataRow dataRow = employeeLoanData.EmployeeLoanPaymentTable.Rows[0];
				string text = "";
				string text2 = "";
				string idFieldValue = dataRow["LoanSysDocID"].ToString();
				string checkFieldValue = dataRow["LoanVoucherID"].ToString();
				string text3 = dataRow["EmployeeID"].ToString();
				object fieldValue = new Databases(base.DBConfig).GetFieldValue("Employee", "AccountID", "EmployeeID", text3, sqlTransaction);
				if (fieldValue == null || !(fieldValue.ToString() != ""))
				{
					throw new CompanyException("Account is not set for the employee '" + text3 + "'.", 1021);
				}
				text = fieldValue.ToString();
				string idFieldValue2 = new Databases(base.DBConfig).GetFieldValue("Employee_Loan", "LoanType", "SysDocID", idFieldValue, "VoucherID", checkFieldValue, sqlTransaction).ToString();
				text2 = new Databases(base.DBConfig).GetFieldValue("Employee_Loan_Type", "AccountID", "LoanTypeID", idFieldValue2, sqlTransaction).ToString();
				dataRow["SysDocID"].ToString();
				string str = dataRow["VoucherID"].ToString();
				DataRow dataRow2 = gLData.JournalTable.NewRow();
				SysDocTypes sysDocTypes = SysDocTypes.EmployeeLoanPayment;
				dataRow2["JournalID"] = 0;
				dataRow2["JournalDate"] = dataRow["TransactionDate"];
				dataRow2["SysDocID"] = dataRow["SysDocID"];
				dataRow2["SysDocType"] = (byte)sysDocTypes;
				dataRow2["VoucherID"] = dataRow["VoucherID"];
				dataRow2["Reference"] = dataRow["Reference"];
				dataRow2["Narration"] = "Loan Recovery - " + str;
				dataRow2.EndEdit();
				gLData.JournalTable.Rows.Add(dataRow2);
				decimal num = default(decimal);
				num = decimal.Parse(dataRow["Amount"].ToString());
				DataRow dataRow3 = gLData.JournalDetailsTable.NewRow();
				dataRow3.BeginEdit();
				dataRow3["JournalID"] = 0;
				dataRow3["AccountID"] = text2;
				dataRow3["PayeeID"] = text3;
				dataRow3["Debit"] = DBNull.Value;
				dataRow3["Credit"] = num;
				dataRow3["Description"] = "Loan Recovery";
				dataRow3["Reference"] = dataRow["Reference"];
				dataRow3.EndEdit();
				gLData.JournalDetailsTable.Rows.Add(dataRow3);
				dataRow3 = gLData.JournalDetailsTable.NewRow();
				dataRow3.BeginEdit();
				dataRow3["JournalID"] = 0;
				dataRow3["AccountID"] = text;
				dataRow3["PayeeID"] = text3;
				dataRow3["PayeeType"] = "E";
				dataRow3["IsARAP"] = true;
				dataRow3["Description"] = "Loan Recovery-No:" + str;
				dataRow3["Debit"] = num;
				dataRow3["Credit"] = DBNull.Value;
				dataRow3["Reference"] = dataRow["Reference"];
				dataRow3.EndEdit();
				gLData.JournalDetailsTable.Rows.Add(dataRow3);
				return gLData;
			}
			catch
			{
				throw;
			}
		}

		private GLData CreateLoanSettlementGLEntry(EmployeeLoanData employeeLoanData, SqlTransaction sqlTransaction)
		{
			try
			{
				GLData gLData = new GLData();
				DataRow dataRow = employeeLoanData.EmployeeLoanSettlementTable.Rows[0];
				string text = "";
				string text2 = "";
				string idFieldValue = dataRow["LoanSysDocID"].ToString();
				string checkFieldValue = dataRow["LoanVoucherID"].ToString();
				string text3 = dataRow["EmployeeID"].ToString();
				object fieldValue = new Databases(base.DBConfig).GetFieldValue("Employee", "AccountID", "EmployeeID", text3, sqlTransaction);
				if (fieldValue == null || !(fieldValue.ToString() != ""))
				{
					throw new CompanyException("Account is not set for the employee '" + text3 + "'.", 1021);
				}
				text = fieldValue.ToString();
				string idFieldValue2 = new Databases(base.DBConfig).GetFieldValue("Employee_Loan", "LoanType", "SysDocID", idFieldValue, "VoucherID", checkFieldValue, sqlTransaction).ToString();
				text2 = (string)(dataRow["LoanAccountID"] = new Databases(base.DBConfig).GetFieldValue("Employee_Loan_Type", "AccountID", "LoanTypeID", idFieldValue2, sqlTransaction).ToString());
				dataRow["SysDocID"].ToString();
				string str = dataRow["VoucherID"].ToString();
				DataRow dataRow2 = gLData.JournalTable.NewRow();
				SysDocTypes sysDocTypes = SysDocTypes.EmployeeLoanSettlement;
				dataRow2["JournalID"] = 0;
				dataRow2["JournalDate"] = dataRow["TransactionDate"];
				dataRow2["SysDocID"] = dataRow["SysDocID"];
				dataRow2["SysDocType"] = (byte)sysDocTypes;
				dataRow2["VoucherID"] = dataRow["VoucherID"];
				dataRow2["Reference"] = dataRow["Reference"];
				dataRow2["Narration"] = "Loan Settlement - " + str;
				dataRow2.EndEdit();
				gLData.JournalTable.Rows.Add(dataRow2);
				decimal num = default(decimal);
				num = decimal.Parse(dataRow["SettlementAmount"].ToString());
				DataRow dataRow3 = gLData.JournalDetailsTable.NewRow();
				dataRow3.BeginEdit();
				dataRow3["JournalID"] = 0;
				dataRow3["AccountID"] = text2;
				dataRow3["PayeeID"] = text3;
				dataRow3["Debit"] = DBNull.Value;
				dataRow3["Credit"] = num;
				dataRow3["Description"] = "Loan settlement";
				dataRow3["Reference"] = dataRow["Reference"];
				dataRow3.EndEdit();
				gLData.JournalDetailsTable.Rows.Add(dataRow3);
				dataRow3 = gLData.JournalDetailsTable.NewRow();
				dataRow3.BeginEdit();
				dataRow3["JournalID"] = 0;
				dataRow3["AccountID"] = text;
				dataRow3["PayeeID"] = text3;
				dataRow3["PayeeType"] = "E";
				dataRow3["IsARAP"] = true;
				dataRow3["Description"] = "Loan settlement-No:" + str;
				dataRow3["Debit"] = num;
				dataRow3["Credit"] = DBNull.Value;
				dataRow3["Reference"] = dataRow["Reference"];
				dataRow3.EndEdit();
				gLData.JournalDetailsTable.Rows.Add(dataRow3);
				return gLData;
			}
			catch
			{
				throw;
			}
		}

		public bool VoidLoan(string sysDocID, string voucherID, bool isVoid)
		{
			bool flag = true;
			try
			{
				if (!CanEditLoan(sysDocID, voucherID))
				{
					throw new CompanyException("This loan transaction is already paid and cannot be deleted.");
				}
				SqlTransaction sqlTransaction = base.DBConfig.StartNewTransaction();
				bool flag2 = false;
				object fieldValue = new Databases(base.DBConfig).GetFieldValue("Employee_Loan", "IsVoid", "SysDocID", sysDocID, "VoucherID", voucherID, sqlTransaction);
				if (fieldValue != null && fieldValue.ToString() != "")
				{
					flag2 = bool.Parse(fieldValue.ToString());
				}
				if (flag2 == isVoid)
				{
					throw new CompanyException("The transaction is already voided.");
				}
				string exp = "UPDATE Employee_Loan SET IsVoid = '" + isVoid.ToString() + "' WHERE SysDocID='" + sysDocID + "' AND VoucherID = '" + voucherID + "'";
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
				AddActivityLog("Employee Loan", voucherID, sysDocID, activityType, sqlTransaction);
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

		public DataSet GetEmployeeLoanComboList()
		{
			DataSet dataSet = new DataSet();
			string textCommand = "SELECT VoucherID [Code],Reason [Name], SysDocID,EmployeeID\r\n                                FROM Employee_Loan EL  \r\n                                WHERE Amount- ISNULL(PaidAmount,0) - ISNULL(DiscountAmount,0) >0 AND ISNULL(IsVoid,'False')='False'";
			FillDataSet(dataSet, "Employee_Loan", textCommand);
			return dataSet;
		}

		public DataSet GetEmployeeLoanComboAllList()
		{
			DataSet dataSet = new DataSet();
			string textCommand = "SELECT VoucherID [Code],Reason [Name], SysDocID,EmployeeID\r\n                                FROM Employee_Loan EL  \r\n                                WHERE ISNULL(IsVoid,'False')='False'";
			FillDataSet(dataSet, "Employee_Loan", textCommand);
			return dataSet;
		}

		public decimal GetNextLoanInstallmentAmount(string voucherID, string employeeID)
		{
			DataSet firstEmployeeLoanByID = GetFirstEmployeeLoanByID(voucherID, employeeID, null);
			if (firstEmployeeLoanByID == null || firstEmployeeLoanByID.Tables.Count == 0 || firstEmployeeLoanByID.Tables[0].Rows.Count == 0)
			{
				return 0m;
			}
			string text = firstEmployeeLoanByID.Tables[0].Rows[0]["SysDocID"].ToString();
			string exp = "SELECT CASE WHEN ISNULL(IsVoid,'False')='True' THEN 0\r\n                        WHEN ISNULL(PaidAmount,0) + ISNULL(DiscountAmount ,0) >= Amount THEN 0 \r\n                        WHEN ISNULL(PaidAmount,0) + ISNULL(DiscountAmount ,0) + InstallmentAmount > Amount THEN Amount-ISNULL(PaidAmount,0) + ISNULL(DiscountAmount ,0) \r\n                        ELSE ISNULL(InstallmentAmount,Amount) END AS [InstallmentAmount]\r\n                        FROM Employee_Loan WHERE \r\n                        DedStartDate <= GetDate() AND \r\n                        SysDocID='" + text + "' AND VoucherID='" + voucherID + "' AND ISNULL(IsVoid,'False')='False'";
			object obj = ExecuteScalar(exp);
			if (obj == null || obj.ToString() == "")
			{
				return 0m;
			}
			return decimal.Parse(obj.ToString());
		}

		public DataSet GetListEmployeeLoan(DateTime from, DateTime to, bool showVoid)
		{
			DataSet dataSet = new DataSet();
			string text = StoreConfiguration.ToSqlDateTimeString(from);
			string text2 = StoreConfiguration.ToSqlDateTimeString(to);
			string text3 = "SELECT    SysDocID [Doc ID],VoucherID [Doc Number]\t,TransactionDate[Loan Date]\t,FirstName+' '+LastName AS [Name],Amount as [Loan Amount]\r\n                        , (SELECT SUM(ISNULL(Debit,0)-ISNULL(Credit,0)) FROM Employee_Loan_Detail ELD WHERE ELD.LoanSysDocID = EL.SysDocID AND ELD.LoanVoucherID = EL.VoucherID)  AS [Balance],DedStartDate\t\t\t\r\n\t\t\t\t\t\tFROM Employee_Loan EL INNER JOIN Employee E ON EL.EmployeeID=E.EmployeeID";
			if (from != DateTime.MinValue)
			{
				text3 = text3 + " WHERE TransactionDate Between '" + text + "' AND '" + text2 + "'";
			}
			if (!showVoid)
			{
				text3 += " AND ISNULL(IsVoid,'False')='False'";
			}
			SqlCommand sqlCommand = new SqlCommand(text3);
			FillDataSet(dataSet, "Employee_Loan", sqlCommand);
			return dataSet;
		}

		public DataSet GetListEmployeeLoanPayment(DateTime from, DateTime to, bool showVoid)
		{
			DataSet dataSet = new DataSet();
			string text = StoreConfiguration.ToSqlDateTimeString(from);
			string text2 = StoreConfiguration.ToSqlDateTimeString(to);
			string text3 = "SELECT   SysDocID [Doc ID],VoucherID [Doc Number]\t\t\t\t\t\t\r\n\t\t\t\t\t\tFROM Employee_Loan_Payment";
			if (from != DateTime.MinValue)
			{
				text3 = text3 + " WHERE TransactionDate Between '" + text + "' AND '" + text2 + "'";
			}
			if (!showVoid)
			{
				text3 += " AND ISNULL(IsVoid,'False')='False'";
			}
			SqlCommand sqlCommand = new SqlCommand(text3);
			FillDataSet(dataSet, "Employee_Loan_Payment", sqlCommand);
			return dataSet;
		}

		public DataSet GetListEmployeeLoanSettlement(DateTime from, DateTime to, bool showVoid)
		{
			DataSet dataSet = new DataSet();
			string text = StoreConfiguration.ToSqlDateTimeString(from);
			string text2 = StoreConfiguration.ToSqlDateTimeString(to);
			string text3 = "SELECT   SysDocID [Doc ID],VoucherID [Doc Number]\t\t\t\t\t\t\r\n\t\t\t\t\t\tFROM Employee_Loan_Settlement";
			if (from != DateTime.MinValue)
			{
				text3 = text3 + " WHERE TransactionDate Between '" + text + "' AND '" + text2 + "'";
			}
			if (!showVoid)
			{
				text3 += " AND ISNULL(IsVoid,'False')='False'";
			}
			SqlCommand sqlCommand = new SqlCommand(text3);
			FillDataSet(dataSet, "Employee_Loan_Settlement", sqlCommand);
			return dataSet;
		}

		public DataSet GetEmployeeLoanReport(string fromEmployee, string toEmployee, string fromDepartment, string toDepartment, string fromLocation, string toLocation, DateTime from, DateTime to, string fromType, string toType, string fromDivision, string toDivision, string fromSponsor, string toSponsor, string fromGroup, string toGroup, string fromGrade, string toGrade, string fromPosition, string toPosition, string fromBank, string toBank, string fromAccount, string toAccount, string SysDocID, string VoucehrID, string EmployeeIDs)
		{
			string text = CommonLib.ToSqlDateTimeString(from);
			string text2 = CommonLib.ToSqlDateTimeString(to);
			string str = "SELECT FirstName+' '+LastName AS [Name] ,E.SponsorID, S.SponsorName, EL.*,(EL.Amount/EL.InstallmentAmount) AS [Count],LT.LoanTypeName,\r\n                            (EL.Amount-ISNULL((SELECT SUM(Credit) FROM Employee_Loan_Detail WHERE LoanSysDocID=EL.SysDocID AND LoanVoucherID=EL.VoucherID),0)) AS Balance\r\n                             FROM Employee_Loan EL LEFT JOIN Employee E ON EL.EmployeeID=E.EmployeeID\r\n                            LEFT JOIN Employee_Loan_Type LT ON LT.LoanTypeID=EL.LoanType\r\n                            LEFT JOIN Sponsor S ON E.SponsorID=S.SponsorID\r\n                            WHERE  ISNULL(EL.IsVoid,'False')='False' AND El.TransactionDate BETWEEN '" + text + "' AND '" + text2 + "'";
			if (EmployeeIDs != "")
			{
				str = str + " AND E.EmployeeID IN(" + EmployeeIDs + ")";
			}
			if (fromEmployee != "")
			{
				str = str + " AND EL.EmployeeID>='" + fromEmployee + "' ";
			}
			if (toEmployee != "")
			{
				str = str + " AND EL.EmployeeID<='" + toEmployee + "' ";
			}
			if (fromDepartment != "")
			{
				str = str + " AND E.DepartmentID>='" + fromDepartment + "' ";
			}
			if (toDepartment != "")
			{
				str = str + " AND E.DepartmentID<='" + toDepartment + "' ";
			}
			if (fromLocation != "")
			{
				str = str + " AND E.LocationID>='" + fromLocation + "' ";
			}
			if (toLocation != "")
			{
				str = str + " AND E.LocationID<='" + toLocation + "' ";
			}
			if (fromType != "")
			{
				str = str + " AND E.ContractType >='" + fromType + "' ";
			}
			if (toType != "")
			{
				str = str + " AND E.ContractType <='" + toType + "' ";
			}
			if (fromDivision != "")
			{
				str = str + " AND E.DivisionID >='" + fromDivision + "' ";
			}
			if (toDivision != "")
			{
				str = str + " AND E.DivisionID <='" + toDivision + "' ";
			}
			if (fromSponsor != "")
			{
				str = str + " AND E.SponsorID >='" + fromSponsor + "' ";
			}
			if (toSponsor != "")
			{
				str = str + " AND E.SponsorID <='" + toSponsor + "' ";
			}
			if (fromGroup != "")
			{
				str = str + " AND E.GroupID >='" + fromGroup + "' ";
			}
			if (toGroup != "")
			{
				str = str + " AND E.GroupID <='" + toGroup + "' ";
			}
			if (fromGrade != "")
			{
				str = str + " AND E.GradeID >='" + fromGrade + "' ";
			}
			if (toGrade != "")
			{
				str = str + " AND E.GradeID <='" + toGrade + "' ";
			}
			if (fromPosition != "")
			{
				str = str + " AND E.PositionID >='" + fromPosition + "' ";
			}
			if (toPosition != "")
			{
				str = str + " AND E.PositionID <='" + toPosition + "' ";
			}
			if (fromBank != "")
			{
				str = str + " AND E.BankID >='" + fromBank + "' ";
			}
			if (toBank != "")
			{
				str = str + " AND E.BankID <='" + toBank + "' ";
			}
			if (fromAccount != "")
			{
				str = str + " AND E.AccountID >='" + fromAccount + "' ";
			}
			if (toAccount != "")
			{
				str = str + " AND E.AccountID <='" + toAccount + "' ";
			}
			if (SysDocID != "")
			{
				str = str + " AND EL.SysDocID='" + SysDocID + "'";
			}
			if (VoucehrID != "")
			{
				str = str + " AND EL.VoucherID='" + VoucehrID + "'";
			}
			str += " ORDER BY EL.EmployeeID";
			DataSet dataSet = new DataSet();
			FillDataSet(dataSet, "Employee_Loan", str);
			str = "SELECT ELD.*,SD.DocName FROM Employee_Loan_Detail ELD INNER JOIN Employee_Loan EL ON ELD.LoanVoucherID=EL.VoucherID\r\n                    LEFT JOIN Employee E ON EL.EmployeeID=E.EmployeeID\r\n                    LEFT JOIN System_Document SD ON SD.SysDocID=ELD.PaymentSysDocID\r\n                    WHERE ELD.PaymentSysDocID IS NOT NULL AND \r\n                    EL.TransactionDate BETWEEN'" + text + "' AND '" + text2 + "'";
			if (fromEmployee != "")
			{
				str = str + " AND EL.EmployeeID>='" + fromEmployee + "' ";
			}
			if (toEmployee != "")
			{
				str = str + " AND EL.EmployeeID<='" + toEmployee + "' ";
			}
			if (fromDepartment != "")
			{
				str = str + " AND E.DepartmentID>='" + fromDepartment + "' ";
			}
			if (toDepartment != "")
			{
				str = str + " AND E.DepartmentID<='" + toDepartment + "' ";
			}
			if (fromLocation != "")
			{
				str = str + " AND E.LocationID>='" + fromLocation + "' ";
			}
			if (toLocation != "")
			{
				str = str + " AND E.LocationID<='" + toLocation + "' ";
			}
			if (fromType != "")
			{
				str = str + " AND E.ContractType >='" + fromType + "' ";
			}
			if (toType != "")
			{
				str = str + " AND E.ContractType <='" + toType + "' ";
			}
			if (fromDivision != "")
			{
				str = str + " AND E.DivisionID >='" + fromDivision + "' ";
			}
			if (toDivision != "")
			{
				str = str + " AND E.DivisionID <='" + toDivision + "' ";
			}
			if (fromSponsor != "")
			{
				str = str + " AND E.SponsorID >='" + fromSponsor + "' ";
			}
			if (toSponsor != "")
			{
				str = str + " AND E.SponsorID <='" + toSponsor + "' ";
			}
			if (fromGroup != "")
			{
				str = str + " AND E.GroupID >='" + fromGroup + "' ";
			}
			if (toGroup != "")
			{
				str = str + " AND E.GroupID <='" + toGroup + "' ";
			}
			if (fromGrade != "")
			{
				str = str + " AND E.GradeID >='" + fromGrade + "' ";
			}
			if (toGrade != "")
			{
				str = str + " AND E.GradeID <='" + toGrade + "' ";
			}
			if (fromPosition != "")
			{
				str = str + " AND E.PositionID >='" + fromPosition + "' ";
			}
			if (toPosition != "")
			{
				str = str + " AND E.PositionID <='" + toPosition + "' ";
			}
			if (fromBank != "")
			{
				str = str + " AND E.BankID >='" + fromBank + "' ";
			}
			if (toBank != "")
			{
				str = str + " AND E.BankID <='" + toBank + "' ";
			}
			if (fromAccount != "")
			{
				str = str + " AND E.AccountID >='" + fromAccount + "' ";
			}
			if (toAccount != "")
			{
				str = str + " AND E.AccountID <='" + toAccount + "' ";
			}
			if (SysDocID != "")
			{
				str = str + " AND EL.SysDocID='" + SysDocID + "'";
			}
			if (VoucehrID != "")
			{
				str = str + " AND EL.VoucherID='" + VoucehrID + "'";
			}
			str += " ORDER BY EL.EmployeeID";
			FillDataSet(dataSet, "Employee_Loan_Detail", str);
			dataSet.Relations.Add("EMPLOAN_Rel", new DataColumn[2]
			{
				dataSet.Tables["Employee_Loan"].Columns["SysDocID"],
				dataSet.Tables["Employee_Loan"].Columns["VoucherID"]
			}, new DataColumn[2]
			{
				dataSet.Tables["Employee_Loan_Detail"].Columns["LoanSysDocID"],
				dataSet.Tables["Employee_Loan_Detail"].Columns["LoanVoucherID"]
			}, createConstraints: false);
			return dataSet;
		}

		public DataSet GetEmployeeLoanReportSummary(DateTime from, DateTime to, string fromEmployee, string toEmployee, string fromDepartment, string toDepartment, string fromLocation, string toLocation, string SysDocID, string VoucehrID)
		{
			string str = "";
			string text = CommonLib.ToSqlDateTimeString(from);
			string text2 = CommonLib.ToSqlDateTimeString(to);
			string text3 = "SELECT FirstName+' '+LastName AS [Name] ,EL.*,(EL.Amount/EL.InstallmentAmount) AS [Count],LT.LoanTypeName,\r\n                            (EL.Amount-ISNULL((SELECT SUM(Credit) FROM Employee_Loan_Detail WHERE LoanSysDocID=EL.SysDocID AND LoanVoucherID=EL.VoucherID),0)) AS Balance\r\n                            FROM Employee_Loan EL LEFT JOIN Employee E ON EL.EmployeeID=E.EmployeeID\r\n                            LEFT JOIN Employee_Loan_Type LT ON LT.LoanTypeID=EL.LoanType                            \r\n                            WHERE EL.Amount > 0  AND ISNULL(EL.IsVoid,'False')='False' AND EL.TransactionDate BETWEEN '" + text + "' AND '" + text2 + "'";
			if (fromEmployee != "")
			{
				text3 = text3 + " AND EL.EmployeeID BETWEEN '" + fromEmployee + "' AND '" + toEmployee + "' ";
			}
			if (fromDepartment != "")
			{
				str = str + " AND EL.EmployeeID IN (SELECT EmployeeID FROM Employee WHERE DepartmentID >= '" + fromDepartment + "') ";
			}
			if (toDepartment != "")
			{
				str = str + " AND EL.EmployeeID IN (SELECT EmployeeID FROM Employee WHERE DepartmentID <= '" + toDepartment + "') ";
			}
			if (fromLocation != "")
			{
				str = str + " AND EL.EmployeeID IN (SELECT EmployeeID FROM Employee WHERE LocationID >= '" + fromLocation + "') ";
			}
			if (toLocation != "")
			{
				str = str + " AND EL.EmployeeID IN (SELECT EmployeeID FROM Employee WHERE LocationID <= '" + toLocation + "') ";
			}
			if (SysDocID != "")
			{
				text3 = text3 + " AND EL.SysDocID='" + SysDocID + "'";
			}
			if (VoucehrID != "")
			{
				text3 = text3 + " AND EL.VoucherID='" + VoucehrID + "'";
			}
			text3 += " ORDER BY EL.EmployeeID";
			DataSet dataSet = new DataSet();
			FillDataSet(dataSet, "Employee_Loan", text3);
			return dataSet;
		}

		public DataSet GetEmployeeLoanReportSummary(string fromEmployee, string toEmployee, string fromDepartment, string toDepartment, string fromLocation, string toLocation, DateTime from, DateTime to, string fromType, string toType, string fromDivision, string toDivision, string fromSponsor, string toSponsor, string fromGroup, string toGroup, string fromGrade, string toGrade, string fromPosition, string toPosition, string fromBank, string toBank, string fromAccount, string toAccount, string SysDocID, string VoucehrID, string EmployeeIDs)
		{
			string text = CommonLib.ToSqlDateTimeString(from);
			string text2 = CommonLib.ToSqlDateTimeString(to);
			string str = "SELECT FirstName+' '+LastName AS [Name] ,E.SponsorID, S.SponsorName, EL.*,(EL.Amount/EL.InstallmentAmount) AS [Count],LT.LoanTypeName,\r\n                            (EL.Amount-ISNULL((SELECT SUM(Credit) FROM Employee_Loan_Detail WHERE LoanSysDocID=EL.SysDocID AND LoanVoucherID=EL.VoucherID),0)) AS Balance\r\n                            FROM Employee_Loan EL LEFT JOIN Employee E ON EL.EmployeeID=E.EmployeeID\r\n                            LEFT JOIN Employee_Loan_Type LT ON LT.LoanTypeID=EL.LoanType     \r\n\t\t\t\t\t\t\tLEFT JOIN Sponsor S ON E.SponsorID=s.SponsorID                             \r\n                            WHERE EL.Amount > 0  AND ISNULL(EL.IsVoid,'False')='False' AND EL.TransactionDate BETWEEN '" + text + "' AND '" + text2 + "'";
			if (EmployeeIDs != "")
			{
				str = str + " AND E.EmployeeID IN(" + EmployeeIDs + ")";
			}
			if (fromEmployee != "")
			{
				str = str + " AND EL.EmployeeID>='" + fromEmployee + "' ";
			}
			if (toEmployee != "")
			{
				str = str + " AND EL.EmployeeID<='" + toEmployee + "' ";
			}
			if (fromDepartment != "")
			{
				str = str + " AND E.DepartmentID>='" + fromDepartment + "' ";
			}
			if (toDepartment != "")
			{
				str = str + " AND E.DepartmentID<='" + toDepartment + "' ";
			}
			if (fromLocation != "")
			{
				str = str + " AND E.LocationID>='" + fromLocation + "' ";
			}
			if (toLocation != "")
			{
				str = str + " AND E.LocationID<='" + toLocation + "' ";
			}
			if (fromType != "")
			{
				str = str + " AND E.ContractType >='" + fromType + "' ";
			}
			if (toType != "")
			{
				str = str + " AND E.ContractType <='" + toType + "' ";
			}
			if (fromDivision != "")
			{
				str = str + " AND E.DivisionID >='" + fromDivision + "' ";
			}
			if (toDivision != "")
			{
				str = str + " AND E.DivisionID <='" + toDivision + "' ";
			}
			if (fromSponsor != "")
			{
				str = str + " AND E.SponsorID >='" + fromSponsor + "' ";
			}
			if (toSponsor != "")
			{
				str = str + " AND E.SponsorID <='" + toSponsor + "' ";
			}
			if (fromGroup != "")
			{
				str = str + " AND E.GroupID >='" + fromGroup + "' ";
			}
			if (toGroup != "")
			{
				str = str + " AND E.GroupID <='" + toGroup + "' ";
			}
			if (fromGrade != "")
			{
				str = str + " AND E.GradeID >='" + fromGrade + "' ";
			}
			if (toGrade != "")
			{
				str = str + " AND E.GradeID <='" + toGrade + "' ";
			}
			if (fromPosition != "")
			{
				str = str + " AND E.PositionID >='" + fromPosition + "' ";
			}
			if (toPosition != "")
			{
				str = str + " AND E.PositionID <='" + toPosition + "' ";
			}
			if (fromBank != "")
			{
				str = str + " AND E.BankID >='" + fromBank + "' ";
			}
			if (toBank != "")
			{
				str = str + " AND E.BankID <='" + toBank + "' ";
			}
			if (fromAccount != "")
			{
				str = str + " AND E.AccountID >='" + fromAccount + "' ";
			}
			if (toAccount != "")
			{
				str = str + " AND E.AccountID <='" + toAccount + "' ";
			}
			if (SysDocID != "")
			{
				str = str + " AND EL.SysDocID='" + SysDocID + "'";
			}
			if (VoucehrID != "")
			{
				str = str + " AND EL.VoucherID='" + VoucehrID + "'";
			}
			str += " ORDER BY EL.EmployeeID";
			DataSet dataSet = new DataSet();
			FillDataSet(dataSet, "Employee_Loan", str);
			return dataSet;
		}

		public DataSet GetEmployeeLoanList(string sysDocID)
		{
			DataSet dataSet = new DataSet();
			string str = "SELECT SysDocID [Doc ID], VoucherID [Number],EmployeeID,TransactionDate,Amount  FROM Employee_Loan WHERE SysDocID='" + sysDocID + "'";
			str += " ORDER BY TransactionDate, VoucherID ";
			FillDataSet(dataSet, "Employee_Loan", str);
			return dataSet;
		}

		public DataSet GetLoanEntryToPrint(string SysDocID, string VoucehrID)
		{
			string text = "SELECT *\r\n                            FROM Loan_Entry LE\r\n                            WHERE 1=1 ";
			if (SysDocID != "")
			{
				text = text + " AND LE.SysDocID='" + SysDocID + "'";
			}
			if (VoucehrID != "")
			{
				text = text + " AND LE.VoucherID='" + VoucehrID + "'";
			}
			DataSet dataSet = new DataSet();
			FillDataSet(dataSet, "Loan_Entry", text);
			text = "SELECT * FROM Loan_Entry_Detail LED \r\n                    WHERE 1= 1";
			if (SysDocID != "")
			{
				text = text + " AND EL.SysDocID='" + SysDocID + "'";
			}
			if (VoucehrID != "")
			{
				text = text + " AND EL.VoucherID='" + VoucehrID + "'";
			}
			FillDataSet(dataSet, "Loan_Entry_Detail", text);
			dataSet.Relations.Add("LOANENTRY_Rel", new DataColumn[2]
			{
				dataSet.Tables["Loan_Entry"].Columns["SysDocID"],
				dataSet.Tables["Loan_Entry"].Columns["VoucherID"]
			}, new DataColumn[2]
			{
				dataSet.Tables["Loan_Entry_Detail"].Columns["LoanSysDocID"],
				dataSet.Tables["Loan_Entry_Detail"].Columns["LoanVoucherID"]
			}, createConstraints: false);
			return dataSet;
		}

		public EmployeeLoanData GetEmployeeLoanSettlementByID(string sysDocID, string voucherID)
		{
			return GetEmployeeLoanSettlementByID(sysDocID, voucherID, null);
		}

		public EmployeeLoanData GetEmployeeLoanSettlementByID(string sysDocID, string voucherID, SqlTransaction sqlTransaction)
		{
			try
			{
				EmployeeLoanData employeeLoanData = new EmployeeLoanData();
				string text = "SELECT EL.SysDocID,EL.VoucherID,EL.LoanSysDocID,EL.LoanVoucherID,EL.Note,EL.SettlementAmount,EL.TransactionDate,EM.LoanType,EM.Amount,EM.InstallmentAmount,EM.DedStartDate,EM.PaidAmount,EM.Reference,EM.EmployeeID,EM.LoanType, (SELECT SUM(ISNULL(Debit,0)-ISNULL(Credit,0)) FROM Employee_Loan_Detail ELD WHERE ELD.LoanSysDocID = EL.LoanSysDocID AND ELD.LoanVoucherID = EL.LoanVoucherID)  AS Balance \r\n                               \r\n                                FROM Employee_Loan_Settlement EL INNER JOIN Employee_Loan EM ON EM.SysDocID=EL.LoanSysDocID AND EM.VoucherID=EL.LoanVoucherID WHERE EL.SysDocID = '" + sysDocID + "' AND EL.VoucherID='" + voucherID + "'";
				text = "SELECT ELD.*  FROM Employee_Loan_Detail ELD\r\n                                    WHERE LoanSysDocID IN (SELECT LoanSysDocID FROM Employee_Loan_Settlement WHERE SysDocID= '" + sysDocID + "') AND LoanVoucherID IN (SELECT LoanVoucherID FROM Employee_Loan_Settlement WHERE VoucherID='" + voucherID + "')";
				FillDataSet(employeeLoanData, "Employee_Loan_Detail", text, sqlTransaction);
				return employeeLoanData;
			}
			catch
			{
				throw;
			}
		}

		public bool DeleteLoanSettlement(string sysDocID, string voucherID)
		{
			bool flag = true;
			try
			{
				SqlTransaction sqlTransaction = base.DBConfig.StartNewTransaction();
				DataSet dataSet = new DataSet();
				string textCommand = " SELECT LoanSysDocID,LoanVoucherID FROM Employee_Loan_Settlement WHERE SysDocID = '" + sysDocID + "' AND VOucherID = '" + voucherID + "'";
				FillDataSet(dataSet, "Loan", textCommand, sqlTransaction);
				string commandText = "DELETE FROM Employee_Loan_Settlement WHERE SysDocID = '" + sysDocID + "' AND VoucherID='" + voucherID + "'";
				flag &= Delete(commandText, sqlTransaction);
				flag &= DeleteLoanDetailsRows(sqlTransaction, sysDocID, voucherID, isPaymentRow: true);
				flag &= new Journal(base.DBConfig).DeleteJournal(sysDocID, voucherID, sqlTransaction);
				string loanSysDocID = dataSet.Tables["Loan"].Rows[0]["LoanSysDocID"].ToString();
				string loanVoucherID = dataSet.Tables["Loan"].Rows[0]["LoanVoucherID"].ToString();
				flag &= UpdateLoanPaidAmount(loanSysDocID, loanVoucherID, sqlTransaction);
				if (!flag)
				{
					return flag;
				}
				ActivityTypes activityType = ActivityTypes.Delete;
				AddActivityLog("Employee Loan Settlement", voucherID, sysDocID, activityType, sqlTransaction);
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

		public bool VoidLoanSettlement(string sysDocID, string voucherID, bool isVoid)
		{
			bool flag = true;
			try
			{
				if (!CanEditLoan(sysDocID, voucherID))
				{
					throw new CompanyException("This loan transaction is already paid and cannot be deleted.");
				}
				SqlTransaction sqlTransaction = base.DBConfig.StartNewTransaction();
				bool flag2 = false;
				object fieldValue = new Databases(base.DBConfig).GetFieldValue("Employee_Loan_Settlement", "IsVoid", "SysDocID", sysDocID, "VoucherID", voucherID, sqlTransaction);
				if (fieldValue != null && fieldValue.ToString() != "")
				{
					flag2 = bool.Parse(fieldValue.ToString());
				}
				if (flag2 == isVoid)
				{
					throw new CompanyException("The transaction is already voided.");
				}
				string exp = "UPDATE Employee_Loan_Settlement SET IsVoid = '" + isVoid.ToString() + "' WHERE SysDocID='" + sysDocID + "' AND VoucherID = '" + voucherID + "'";
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
				AddActivityLog("Employee Loan Settlement", voucherID, sysDocID, activityType, sqlTransaction);
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

		public DataSet GetEmployeeLoanSettlementToPrint(string SysDocID, string VoucehrID)
		{
			string text = "SELECT FirstName+' '+LastName AS [Name] ,EL.*,(EL.Amount/EL.InstallmentAmount) AS [Count],LT.LoanTypeName,\r\n                            (EL.Amount-ISNULL((SELECT SUM(Credit) FROM Employee_Loan_Detail WHERE LoanSysDocID=EL.SysDocID AND LoanVoucherID=EL.VoucherID),0)) AS Balance,ELS.SettlementAmount\r\n                            FROM Employee_Loan EL LEFT JOIN Employee E ON EL.EmployeeID=E.EmployeeID\r\n                            LEFT JOIN Employee_Loan_Type LT ON LT.LoanTypeID=EL.LoanType\r\n                            INNER JOIN Employee_Loan_Settlement ELS ON ELS.LoanSysDocID=EL.SysDocID AND ELS.LoanVoucherID=EL.VoucherID\r\n                            WHERE 1=1 ";
			if (SysDocID != "")
			{
				text = text + " AND ELS.SysDocID='" + SysDocID + "'";
			}
			if (VoucehrID != "")
			{
				text = text + " AND ELS.VoucherID='" + VoucehrID + "'";
			}
			DataSet dataSet = new DataSet();
			FillDataSet(dataSet, "Employee_Loan_Settlement", text);
			text = "SELECT ELD.*,SD.DocName FROM Employee_Loan_Detail ELD INNER JOIN Employee_Loan EL ON ELD.LoanVoucherID=EL.VoucherID\r\n                    LEFT JOIN Employee E ON EL.EmployeeID=E.EmployeeID\r\n                    LEFT JOIN System_Document SD ON SD.SysDocID=ELD.PaymentSysDocID\r\n                    WHERE ELD.PaymentSysDocID IS NOT NULL";
			if (SysDocID != "")
			{
				text = text + " AND EL.SysDocID='" + SysDocID + "'";
			}
			if (VoucehrID != "")
			{
				text = text + " AND EL.VoucherID='" + VoucehrID + "'";
			}
			FillDataSet(dataSet, "Employee_Loan_Detail", text);
			dataSet.Relations.Add("EmplLoanSettle_Rel", new DataColumn[2]
			{
				dataSet.Tables["Employee_Loan_Settlement"].Columns["SysDocID"],
				dataSet.Tables["Employee_Loan_Settlement"].Columns["VoucherID"]
			}, new DataColumn[2]
			{
				dataSet.Tables["Employee_Loan_Detail"].Columns["LoanSysDocID"],
				dataSet.Tables["Employee_Loan_Detail"].Columns["LoanVoucherID"]
			}, createConstraints: false);
			return dataSet;
		}
	}
}
