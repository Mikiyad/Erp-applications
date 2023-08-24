using Micromind.Common;
using Micromind.Common.Data;
using Micromind.Common.Libraries;
using System;
using System.Data;
using System.Data.SqlClient;

namespace Micromind.Data
{
	public sealed class EmployeeLoan : StoreObject
	{
		private const string LOANID_PARM = "@LoanID";

		private const string SYSDOCID_PARM = "@SysDocID";

		private const string VOUCHERID_PARM = "@VoucherID";

		private const string EMPLOYEEID_PARM = "@EmployeeID";

		private const string TRANSACTIONDATE_PARM = "@TransactionDate";

		private const string LOANTYPE_PARM = "@LoanType";

		private const string AMOUNT_PARM = "@Amount";

		private const string INSTALLMENTAMOUNT_PARM = "@InstallmentAmount";

		private const string DEDSTARTDATE_PARM = "@DedStartDate";

		private const string DEDUCTEDAMOUNT_PARM = "@DeductedAmount";

		private const string DISCOUNTDATE_PARM = "@DiscountDate";

		private const string REASON_PARM = "@Reason";

		private const string REFERENCE_PARM = "@Reference";

		private const string NOTE_PARM = "@Note";

		private const string CREATEDBY_PARM = "@CreatedBy";

		private const string DATECREATED_PARM = "@DateCreated";

		private const string UPDATEDBY_PARM = "@UpdatedBy";

		private const string DATEUPDATED_PARM = "@DateUpdated";

		private const string LOANACCOUNTID_PARM = "@LoanAccountID";

		private const string EMPLOYEEACCOUNTID_PARM = "@EmployeeAccountID";

		private const string COMPANYID_PARM = "@CompanyID";

		private const string DIVISIONID_PARM = "@DivisionID";

		private const string EMPLOYEELOAN_TABLE = "Employee_Loan";

		private const string EMPLOYEELOANDETAIL_TABLE = "Employee_Loan_Detail";

		private const string LOANSYSDOCID_PARM = "@LoanSysDocID";

		private const string LOANVOUCHERID_PARM = "@LoanVoucherID";

		private const string PAYMENTSYSDOCID_PARM = "@PaymentSysDocID";

		private const string PAYMENTVOUCHERID_PARM = "@PaymentVoucherID";

		private const string DESCRIPTION_PARM = "@Description";

		private const string DEBIT_PARM = "@Debit";

		private const string CREDIT_PARM = "@Credit";

		private const string EMPLOYEELOANPAYMENT_TABLE = "Employee_Loan_Payment";

		private const string SETTLEMENTAMOUNT_PARM = "@SettlementAmount";

		private const string EMPLOYEELOANSETTLEMENT_TABLE = "Employee_Loan_Settlement";

		public EmployeeLoan(Config config)
			: base(config)
		{
		}

		private string GetInsertUpdateEmployeeLoanText(bool isUpdate)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddInsertUpdateParameters("Employee_Loan", new FieldValue("SysDocID", "@SysDocID", isUpdateConditionField: true), new FieldValue("VoucherID", "@VoucherID", isUpdateConditionField: true), new FieldValue("DivisionID", "@DivisionID"), new FieldValue("CompanyID", "@CompanyID"), new FieldValue("EmployeeID", "@EmployeeID"), new FieldValue("TransactionDate", "@TransactionDate"), new FieldValue("LoanType", "@LoanType"), new FieldValue("Amount", "@Amount"), new FieldValue("InstallmentAmount", "@InstallmentAmount"), new FieldValue("DedStartDate", "@DedStartDate"), new FieldValue("Reason", "@Reason"), new FieldValue("Reference", "@Reference"), new FieldValue("LoanAccountID", "@LoanAccountID"), new FieldValue("EmployeeAccountID", "@EmployeeAccountID"), new FieldValue("Note", "@Note"));
			if (isUpdate)
			{
				sqlBuilder.AddInsertUpdateParameters("Employee_Loan", new FieldValue("DateUpdated", "@DateUpdated", isUpdateConditionField: true, checkForNullValue: true));
			}
			if (isUpdate)
			{
				return sqlBuilder.GetUpdateExpression();
			}
			return sqlBuilder.GetInsertExpression();
		}

		private SqlCommand GetInsertUpdateEmployeeLoanCommand(bool isUpdate)
		{
			SqlParameterCollection parameters;
			if (isUpdate)
			{
				if (updateCommand != null)
				{
					updateCommand = null;
				}
				updateCommand = new SqlCommand(GetInsertUpdateEmployeeLoanText(isUpdate: true), base.DBConfig.Connection);
				updateCommand.CommandType = CommandType.Text;
				parameters = updateCommand.Parameters;
			}
			else
			{
				if (insertCommand != null)
				{
					insertCommand = null;
				}
				insertCommand = new SqlCommand(GetInsertUpdateEmployeeLoanText(isUpdate: false), base.DBConfig.Connection);
				insertCommand.CommandType = CommandType.Text;
				parameters = insertCommand.Parameters;
			}
			parameters.Add("@SysDocID", SqlDbType.NVarChar);
			parameters.Add("@VoucherID", SqlDbType.NVarChar);
			parameters.Add("@DivisionID", SqlDbType.NVarChar);
			parameters.Add("@CompanyID", SqlDbType.TinyInt);
			parameters.Add("@EmployeeID", SqlDbType.NVarChar);
			parameters.Add("@TransactionDate", SqlDbType.DateTime);
			parameters.Add("@LoanType", SqlDbType.NVarChar);
			parameters.Add("@Amount", SqlDbType.Money);
			parameters.Add("@InstallmentAmount", SqlDbType.Money);
			parameters.Add("@DedStartDate", SqlDbType.DateTime);
			parameters.Add("@Reason", SqlDbType.NVarChar);
			parameters.Add("@Reference", SqlDbType.NVarChar);
			parameters.Add("@LoanAccountID", SqlDbType.NVarChar);
			parameters.Add("@EmployeeAccountID", SqlDbType.NVarChar);
			parameters.Add("@Note", SqlDbType.NVarChar);
			parameters["@SysDocID"].SourceColumn = "SysDocID";
			parameters["@VoucherID"].SourceColumn = "VoucherID";
			parameters["@DivisionID"].SourceColumn = "DivisionID";
			parameters["@CompanyID"].SourceColumn = "CompanyID";
			parameters["@EmployeeID"].SourceColumn = "EmployeeID";
			parameters["@TransactionDate"].SourceColumn = "TransactionDate";
			parameters["@LoanType"].SourceColumn = "LoanType";
			parameters["@Amount"].SourceColumn = "Amount";
			parameters["@InstallmentAmount"].SourceColumn = "InstallmentAmount";
			parameters["@DedStartDate"].SourceColumn = "DedStartDate";
			parameters["@Reason"].SourceColumn = "Reason";
			parameters["@Reference"].SourceColumn = "Reference";
			parameters["@LoanAccountID"].SourceColumn = "LoanAccountID";
			parameters["@EmployeeAccountID"].SourceColumn = "EmployeeAccountID";
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

		private string GetInsertUpdateEmployeeLoanDetailText(bool isUpdate)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddInsertUpdateParameters("Employee_Loan_Detail", new FieldValue("LoanSysDocID", "@LoanSysDocID"), new FieldValue("LoanVoucherID", "@LoanVoucherID"), new FieldValue("PaymentSysDocID", "@PaymentSysDocID"), new FieldValue("PaymentVoucherID", "@PaymentVoucherID"), new FieldValue("Description", "@Description"), new FieldValue("EmployeeID", "@EmployeeID"), new FieldValue("Debit", "@Debit"), new FieldValue("Credit", "@Credit"), new FieldValue("TransactionDate", "@TransactionDate"), new FieldValue("Reference", "@Reference"));
			if (isUpdate)
			{
				return sqlBuilder.GetUpdateExpression();
			}
			return sqlBuilder.GetInsertExpression();
		}

		private SqlCommand GetInsertUpdateEmployeeLoanDetailCommand(bool isUpdate)
		{
			SqlParameterCollection parameters;
			if (isUpdate)
			{
				if (updateCommand != null)
				{
					updateCommand = null;
				}
				updateCommand = new SqlCommand(GetInsertUpdateEmployeeLoanDetailText(isUpdate: true), base.DBConfig.Connection);
				updateCommand.CommandType = CommandType.Text;
				parameters = updateCommand.Parameters;
			}
			else
			{
				if (insertCommand != null)
				{
					insertCommand = null;
				}
				insertCommand = new SqlCommand(GetInsertUpdateEmployeeLoanDetailText(isUpdate: false), base.DBConfig.Connection);
				insertCommand.CommandType = CommandType.Text;
				parameters = insertCommand.Parameters;
			}
			parameters.Add("@LoanSysDocID", SqlDbType.NVarChar);
			parameters.Add("@LoanVoucherID", SqlDbType.NVarChar);
			parameters.Add("@PaymentSysDocID", SqlDbType.NVarChar);
			parameters.Add("@PaymentVoucherID", SqlDbType.NVarChar);
			parameters.Add("@Description", SqlDbType.NVarChar);
			parameters.Add("@EmployeeID", SqlDbType.NVarChar);
			parameters.Add("@Debit", SqlDbType.Money);
			parameters.Add("@Credit", SqlDbType.Money);
			parameters.Add("@TransactionDate", SqlDbType.DateTime);
			parameters.Add("@Reference", SqlDbType.NVarChar);
			parameters["@LoanSysDocID"].SourceColumn = "LoanSysDocID";
			parameters["@LoanVoucherID"].SourceColumn = "LoanVoucherID";
			parameters["@PaymentSysDocID"].SourceColumn = "PaymentSysDocID";
			parameters["@PaymentVoucherID"].SourceColumn = "PaymentVoucherID";
			parameters["@EmployeeID"].SourceColumn = "EmployeeID";
			parameters["@Description"].SourceColumn = "Description";
			parameters["@Debit"].SourceColumn = "Debit";
			parameters["@Credit"].SourceColumn = "Credit";
			parameters["@TransactionDate"].SourceColumn = "TransactionDate";
			parameters["@Reference"].SourceColumn = "Reference";
			if (isUpdate)
			{
				return updateCommand;
			}
			return insertCommand;
		}

		private string GetInsertUpdateEmployeeLoanPaymentText(bool isUpdate)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddInsertUpdateParameters("Employee_Loan_Payment", new FieldValue("SysDocID", "@SysDocID", isUpdateConditionField: true), new FieldValue("VoucherID", "@VoucherID", isUpdateConditionField: true), new FieldValue("DivisionID", "@DivisionID"), new FieldValue("CompanyID", "@CompanyID"), new FieldValue("LoanSysDocID", "@LoanSysDocID"), new FieldValue("LoanVoucherID", "@LoanVoucherID"), new FieldValue("Description", "@Description"), new FieldValue("EmployeeID", "@EmployeeID"), new FieldValue("Amount", "@Amount"), new FieldValue("TransactionDate", "@TransactionDate"), new FieldValue("Note", "@Note"), new FieldValue("Reference", "@Reference"));
			if (isUpdate)
			{
				return sqlBuilder.GetUpdateExpression();
			}
			return sqlBuilder.GetInsertExpression();
		}

		private SqlCommand GetInsertUpdateEmployeeLoanPaymentCommand(bool isUpdate)
		{
			SqlParameterCollection parameters;
			if (isUpdate)
			{
				if (updateCommand != null)
				{
					updateCommand = null;
				}
				updateCommand = new SqlCommand(GetInsertUpdateEmployeeLoanPaymentText(isUpdate: true), base.DBConfig.Connection);
				updateCommand.CommandType = CommandType.Text;
				parameters = updateCommand.Parameters;
			}
			else
			{
				if (insertCommand != null)
				{
					insertCommand = null;
				}
				insertCommand = new SqlCommand(GetInsertUpdateEmployeeLoanPaymentText(isUpdate: false), base.DBConfig.Connection);
				insertCommand.CommandType = CommandType.Text;
				parameters = insertCommand.Parameters;
			}
			parameters.Add("@SysDocID", SqlDbType.NVarChar);
			parameters.Add("@VoucherID", SqlDbType.NVarChar);
			parameters.Add("@DivisionID", SqlDbType.NVarChar);
			parameters.Add("@CompanyID", SqlDbType.TinyInt);
			parameters.Add("@LoanSysDocID", SqlDbType.NVarChar);
			parameters.Add("@LoanVoucherID", SqlDbType.NVarChar);
			parameters.Add("@Description", SqlDbType.NVarChar);
			parameters.Add("@EmployeeID", SqlDbType.NVarChar);
			parameters.Add("@Amount", SqlDbType.Money);
			parameters.Add("@TransactionDate", SqlDbType.DateTime);
			parameters.Add("@Reference", SqlDbType.NVarChar);
			parameters.Add("@Note", SqlDbType.NVarChar);
			parameters["@SysDocID"].SourceColumn = "SysDocID";
			parameters["@VoucherID"].SourceColumn = "VoucherID";
			parameters["@DivisionID"].SourceColumn = "DivisionID";
			parameters["@CompanyID"].SourceColumn = "CompanyID";
			parameters["@LoanSysDocID"].SourceColumn = "LoanSysDocID";
			parameters["@LoanVoucherID"].SourceColumn = "LoanVoucherID";
			parameters["@EmployeeID"].SourceColumn = "EmployeeID";
			parameters["@Description"].SourceColumn = "Description";
			parameters["@Amount"].SourceColumn = "Amount";
			parameters["@TransactionDate"].SourceColumn = "TransactionDate";
			parameters["@Reference"].SourceColumn = "Reference";
			parameters["@Note"].SourceColumn = "Note";
			if (isUpdate)
			{
				return updateCommand;
			}
			return insertCommand;
		}

		private string GetInsertUpdateEmployeeLoanSettlementText(bool isUpdate)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddInsertUpdateParameters("Employee_Loan_Settlement", new FieldValue("SysDocID", "@SysDocID", isUpdateConditionField: true), new FieldValue("VoucherID", "@VoucherID", isUpdateConditionField: true), new FieldValue("DivisionID", "@DivisionID"), new FieldValue("CompanyID", "@CompanyID"), new FieldValue("LoanSysDocID", "@LoanSysDocID"), new FieldValue("LoanVoucherID", "@LoanVoucherID"), new FieldValue("EmployeeID", "@EmployeeID"), new FieldValue("TransactionDate", "@TransactionDate"), new FieldValue("LoanType", "@LoanType"), new FieldValue("Amount", "@Amount"), new FieldValue("SettlementAmount", "@SettlementAmount"), new FieldValue("Note", "@Note"), new FieldValue("LoanAccountID", "@LoanAccountID"), new FieldValue("EmployeeAccountID", "@EmployeeAccountID"));
			if (isUpdate)
			{
				return sqlBuilder.GetUpdateExpression();
			}
			return sqlBuilder.GetInsertExpression();
		}

		private SqlCommand GetInsertUpdateEmployeeLoanSettlementCommand(bool isUpdate)
		{
			SqlParameterCollection parameters;
			if (isUpdate)
			{
				if (updateCommand != null)
				{
					updateCommand = null;
				}
				updateCommand = new SqlCommand(GetInsertUpdateEmployeeLoanSettlementText(isUpdate: true), base.DBConfig.Connection);
				updateCommand.CommandType = CommandType.Text;
				parameters = updateCommand.Parameters;
			}
			else
			{
				if (insertCommand != null)
				{
					insertCommand = null;
				}
				insertCommand = new SqlCommand(GetInsertUpdateEmployeeLoanSettlementText(isUpdate: false), base.DBConfig.Connection);
				insertCommand.CommandType = CommandType.Text;
				parameters = insertCommand.Parameters;
			}
			parameters.Add("@SysDocID", SqlDbType.NVarChar);
			parameters.Add("@VoucherID", SqlDbType.NVarChar);
			parameters.Add("@DivisionID", SqlDbType.NVarChar);
			parameters.Add("@CompanyID", SqlDbType.TinyInt);
			parameters.Add("@LoanSysDocID", SqlDbType.NVarChar);
			parameters.Add("@LoanVoucherID", SqlDbType.NVarChar);
			parameters.Add("@EmployeeID", SqlDbType.NVarChar);
			parameters.Add("@LoanAccountID", SqlDbType.NVarChar);
			parameters.Add("@EmployeeAccountID", SqlDbType.NVarChar);
			parameters.Add("@TransactionDate", SqlDbType.DateTime);
			parameters.Add("@LoanType", SqlDbType.NVarChar);
			parameters.Add("@Amount", SqlDbType.Money);
			parameters.Add("@SettlementAmount", SqlDbType.Money);
			parameters.Add("@Note", SqlDbType.NVarChar);
			parameters["@SysDocID"].SourceColumn = "SysDocID";
			parameters["@VoucherID"].SourceColumn = "VoucherID";
			parameters["@DivisionID"].SourceColumn = "DivisionID";
			parameters["@CompanyID"].SourceColumn = "CompanyID";
			parameters["@LoanSysDocID"].SourceColumn = "LoanSysDocID";
			parameters["@LoanVoucherID"].SourceColumn = "LoanVoucherID";
			parameters["@EmployeeID"].SourceColumn = "EmployeeID";
			parameters["@TransactionDate"].SourceColumn = "TransactionDate";
			parameters["@LoanType"].SourceColumn = "LoanType";
			parameters["@Amount"].SourceColumn = "Amount";
			parameters["@SettlementAmount"].SourceColumn = "SettlementAmount";
			parameters["@Note"].SourceColumn = "Note";
			parameters["@LoanAccountID"].SourceColumn = "LoanAccountID";
			parameters["@EmployeeAccountID"].SourceColumn = "EmployeeAccountID";
			if (isUpdate)
			{
				return updateCommand;
			}
			return insertCommand;
		}

		public bool InsertUpdateEmployeeLoan(EmployeeLoanData employeeLoanData, bool isUpdate)
		{
			bool flag = true;
			string text = "";
			try
			{
				DataRow dataRow = employeeLoanData.EmployeeLoanTable.Rows[0];
				string text2 = dataRow["VoucherID"].ToString();
				string text3 = dataRow["SysDocID"].ToString();
				SqlCommand insertUpdateEmployeeLoanCommand = GetInsertUpdateEmployeeLoanCommand(isUpdate);
				SqlTransaction sqlTransaction = base.DBConfig.StartNewTransaction();
				dataRow["EmployeeID"].ToString();
				if (isUpdate)
				{
					text = "SELECT CASE WHEN ISNULL(PaidAmount,0) + ISNULL(DiscountAmount,0)=0 THEN 'True' ELSE 'False' END AS CanEdit FROM Employee_Loan WHERE SysDocID = '" + text3 + "' AND VoucherID='" + text2 + "'";
					object obj = ExecuteScalar(text, sqlTransaction);
					if (obj != null && obj.ToString() != "" && !bool.Parse(obj.ToString()))
					{
						throw new CompanyException("This loan is already partially or fully paid and cannot be edited.", 1023);
					}
				}
				insertUpdateEmployeeLoanCommand.Transaction = sqlTransaction;
				flag = (isUpdate ? (flag & Update(employeeLoanData, "Employee_Loan", insertUpdateEmployeeLoanCommand)) : (flag & Insert(employeeLoanData, "Employee_Loan", insertUpdateEmployeeLoanCommand)));
				if (isUpdate)
				{
					DeleteLoanDetailsRows(sqlTransaction, text3, text2, isPaymentRow: false);
				}
				flag &= InsertEmployeeLoanDetail(employeeLoanData, sqlTransaction);
				GLData journalData = CreateLoanGLEntry(employeeLoanData, sqlTransaction);
				flag &= new Journal(base.DBConfig).InsertUpdateJournal(journalData, isUpdate, sqlTransaction);
				flag &= UpdateLoanPaidAmount(text3, text2, sqlTransaction);
				if (!flag)
				{
					throw new Exception("Failed to save the transaction because of an unexpected error. Please try again.");
				}
				flag &= UpdateTableRowInsertUpdateInfo("Employee_Loan", "SysDocID", dataRow["SysDocID"].ToString(), "VoucherID", dataRow["VoucherID"].ToString(), sqlTransaction, !isUpdate);
				string entityName = "Employee Loan";
				flag = (isUpdate ? (flag & AddActivityLog(entityName, text2, text3, ActivityTypes.Update, sqlTransaction)) : (flag & AddActivityLog(entityName, text2, text3, ActivityTypes.Add, sqlTransaction)));
				if (!isUpdate)
				{
					flag &= new SystemDocuments(base.DBConfig).UpdateNextDocumentNumber(dataRow["SysDocID"].ToString(), dataRow["VoucherID"].ToString(), "Employee_Loan", "VoucherID", sqlTransaction);
				}
				if (!flag)
				{
					return flag;
				}
				flag &= new Approval(base.DBConfig).CreateTransactionApprovalTasks(SysDocTypes.EmployeeLoan, text3, text2, "Employee_Loan", sqlTransaction);
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

		internal bool InsertEmployeeLoanDetail(EmployeeLoanData employeeLoanData, SqlTransaction sqlTransaction)
		{
			bool flag = true;
			try
			{
				SqlCommand insertUpdateEmployeeLoanDetailCommand = GetInsertUpdateEmployeeLoanDetailCommand(isUpdate: false);
				insertUpdateEmployeeLoanDetailCommand.Transaction = sqlTransaction;
				if (employeeLoanData.Tables["Employee_Loan_Detail"].Rows.Count > 0)
				{
					flag &= Insert(employeeLoanData, "Employee_Loan_Detail", insertUpdateEmployeeLoanDetailCommand);
				}
				foreach (DataRow row in employeeLoanData.EmployeeLoanDetailTable.Rows)
				{
					string text = row["LoanSysDocID"].ToString();
					string text2 = row["LoanVoucherID"].ToString();
					string exp = " SELECT SUM(ISNULL(Debit,0) - ISNULL(Credit,0)) AS Balance FROM Employee_Loan_Detail\r\n                                        WHERE LoanSysDocID = '" + text + "' AND LoanVoucherID = '" + text2 + "' ";
					object obj2 = ExecuteScalar(exp, sqlTransaction);
					if (obj2 != null && obj2.ToString() != "" && decimal.Parse(obj2.ToString()) < 0m)
					{
						throw new CompanyException("Loan deduction is more than the loan amount.");
					}
					flag &= UpdateLoanPaidAmount(text, text2, sqlTransaction);
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
				string commandText = isPaymentRow ? ("DELETE FROM Employee_Loan_Detail WHERE PaymentSysDocID = '" + sysDocID + "' AND PaymentVoucherID = '" + voucherID + "' AND Debit IS NULL") : ("DELETE FROM Employee_Loan_Detail WHERE LoanSysDocID = '" + sysDocID + "' AND LoanVoucherID = '" + voucherID + "' AND Credit IS NULL");
				flag &= Delete(commandText, sqlTransaction);
				if (flag)
				{
					return flag & DeleteLoanPaidAmountWhileDelete(sysDocID, voucherID, sqlTransaction);
				}
				return flag;
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

		public bool InsertUpdateEmployeeLoanPayment(EmployeeLoanData employeeLoanData, bool isUpdate)
		{
			bool flag = true;
			try
			{
				DataRow dataRow = employeeLoanData.EmployeeLoanPaymentTable.Rows[0];
				string sysDocID = dataRow["SysDocID"].ToString();
				string text = dataRow["VoucherID"].ToString();
				string loanSysDocID = dataRow["LoanSysDocID"].ToString();
				string loanVoucherID = dataRow["LoanVoucherID"].ToString();
				SqlCommand insertUpdateEmployeeLoanPaymentCommand = GetInsertUpdateEmployeeLoanPaymentCommand(isUpdate);
				SqlTransaction sqlTransaction2 = insertUpdateEmployeeLoanPaymentCommand.Transaction = base.DBConfig.StartNewTransaction();
				if (!isUpdate)
				{
					if (employeeLoanData.Tables["Employee_Loan_Payment"].Rows.Count > 0)
					{
						flag &= Insert(employeeLoanData, "Employee_Loan_Payment", insertUpdateEmployeeLoanPaymentCommand);
					}
				}
				else
				{
					flag &= Update(employeeLoanData, "Employee_Loan_Payment", insertUpdateEmployeeLoanPaymentCommand);
				}
				insertUpdateEmployeeLoanPaymentCommand = GetInsertUpdateEmployeeLoanDetailCommand(isUpdate: false);
				insertUpdateEmployeeLoanPaymentCommand.Transaction = sqlTransaction2;
				if (isUpdate)
				{
					flag &= DeleteLoanDetailsRows(sqlTransaction2, sysDocID, text, isPaymentRow: true);
				}
				if (employeeLoanData.Tables["Employee_Loan_Detail"].Rows.Count > 0)
				{
					flag &= Insert(employeeLoanData, "Employee_Loan_Detail", insertUpdateEmployeeLoanPaymentCommand);
				}
				GLData journalData = CreateLoanPaymentGLEntry(employeeLoanData, sqlTransaction2);
				flag &= new Journal(base.DBConfig).InsertUpdateJournal(journalData, isUpdate, sqlTransaction2);
				flag &= UpdateLoanPaidAmount(loanSysDocID, loanVoucherID, sqlTransaction2);
				if (!flag)
				{
					throw new Exception("Failed to save the transaction because of an unexpected error. Please try again.");
				}
				flag &= UpdateTableRowInsertUpdateInfo("Employee_Loan_Payment", "SysDocID", dataRow["SysDocID"].ToString(), "VoucherID", dataRow["VoucherID"].ToString(), sqlTransaction2, !isUpdate);
				string entityName = "Employee Loan Recovery";
				flag = (isUpdate ? (flag & AddActivityLog(entityName, text, sysDocID, ActivityTypes.Update, sqlTransaction2)) : (flag & AddActivityLog(entityName, text, sysDocID, ActivityTypes.Add, sqlTransaction2)));
				if (isUpdate)
				{
					return flag;
				}
				flag &= new SystemDocuments(base.DBConfig).UpdateNextDocumentNumber(dataRow["SysDocID"].ToString(), dataRow["VoucherID"].ToString(), "Employee_Loan", "VoucherID", sqlTransaction2);
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

		public bool InsertUpdateEmployeeLoanSettlement(EmployeeLoanData employeeLoanData, bool isUpdate)
		{
			bool flag = true;
			try
			{
				DataRow dataRow = employeeLoanData.EmployeeLoanSettlementTable.Rows[0];
				string sysDocID = dataRow["SysDocID"].ToString();
				string text = dataRow["VoucherID"].ToString();
				string text2 = dataRow["LoanSysDocID"].ToString();
				string text3 = dataRow["LoanVoucherID"].ToString();
				SqlCommand insertUpdateEmployeeLoanSettlementCommand = GetInsertUpdateEmployeeLoanSettlementCommand(isUpdate);
				SqlTransaction sqlTransaction = base.DBConfig.StartNewTransaction();
				string str = dataRow["EmployeeID"].ToString();
				string text4 = "";
				string text5 = "";
				string textCommand = "SELECT EmployeeID,ISNULL(EMP.AccountID,ET.AccountID) AS AccountID \r\n                                FROM Employee EMP LEFT OUTER JOIN Employee_Type ET ON EMP.ContractType = ET.TypeID\r\n                                WHERE EMP.EmployeeID IN ('" + str + "') ";
				DataSet dataSet = new DataSet();
				FillDataSet(dataSet, "Employee", textCommand, sqlTransaction);
				textCommand = "SELECT  LOC.EmployeeAccountID AS AccountID  FROM System_Document SD INNER JOIN Location LOC ON SD.LocationID = LOC.LocationID WHERE SysDocID = '" + text2 + "'";
				DataSet dataSet2 = new DataSet();
				FillDataSet(dataSet2, "Accounts", textCommand, sqlTransaction);
				DataRow[] array = dataSet.Tables[0].Select("EmployeeID = '" + str + "'");
				if (array.Length != 0 && array[0]["AccountID"] != DBNull.Value)
				{
					text4 = array[0]["AccountID"].ToString();
				}
				if (text4 == "")
				{
					text4 = dataSet2.Tables[0].Rows[0]["AccountID"].ToString();
				}
				if (dataSet2 == null || dataSet2.Tables.Count == 0 || dataSet2.Tables[0].Rows.Count == 0)
				{
					throw new CompanyException("There is no location assigned to this system document or location record is missing.");
				}
				if (text4 == "")
				{
					throw new CompanyException("Account is not set for the employee '" + str + "'.", 1021);
				}
				dataRow["EmployeeAccountID"] = text4;
				string idFieldValue = new Databases(base.DBConfig).GetFieldValue("Employee_Loan", "LoanType", "SysDocID", text2, "VoucherID", text3, sqlTransaction).ToString();
				text5 = (string)(dataRow["LoanAccountID"] = new Databases(base.DBConfig).GetFieldValue("Employee_Loan_Type", "AccountID", "LoanTypeID", idFieldValue, sqlTransaction).ToString());
				insertUpdateEmployeeLoanSettlementCommand.Transaction = sqlTransaction;
				if (!isUpdate)
				{
					if (employeeLoanData.Tables["Employee_Loan_Settlement"].Rows.Count > 0)
					{
						flag &= Insert(employeeLoanData, "Employee_Loan_Settlement", insertUpdateEmployeeLoanSettlementCommand);
					}
				}
				else
				{
					flag &= Update(employeeLoanData, "Employee_Loan_Settlement", insertUpdateEmployeeLoanSettlementCommand);
				}
				insertUpdateEmployeeLoanSettlementCommand = GetInsertUpdateEmployeeLoanDetailCommand(isUpdate: false);
				insertUpdateEmployeeLoanSettlementCommand.Transaction = sqlTransaction;
				if (isUpdate)
				{
					flag &= DeleteLoanDetailsRows(sqlTransaction, sysDocID, text, isPaymentRow: true);
				}
				if (employeeLoanData.Tables["Employee_Loan_Detail"].Rows.Count > 0)
				{
					flag &= Insert(employeeLoanData, "Employee_Loan_Detail", insertUpdateEmployeeLoanSettlementCommand);
				}
				GLData journalData = CreateLoanSettlementGLEntry(employeeLoanData, sqlTransaction);
				flag &= new Journal(base.DBConfig).InsertUpdateJournal(journalData, isUpdate, sqlTransaction);
				flag &= UpdateLoanPaidAmount(text2, text3, sqlTransaction);
				if (!flag)
				{
					throw new Exception("Failed to save the transaction because of an unexpected error. Please try again.");
				}
				flag &= UpdateTableRowInsertUpdateInfo("Employee_Loan_Payment", "SysDocID", dataRow["SysDocID"].ToString(), "VoucherID", dataRow["VoucherID"].ToString(), sqlTransaction, !isUpdate);
				string entityName = "Employee Loan Settlement";
				flag = (isUpdate ? (flag & AddActivityLog(entityName, text, sysDocID, ActivityTypes.Update, sqlTransaction)) : (flag & AddActivityLog(entityName, text, sysDocID, ActivityTypes.Add, sqlTransaction)));
				if (isUpdate)
				{
					return flag;
				}
				flag &= new SystemDocuments(base.DBConfig).UpdateNextDocumentNumber(dataRow["SysDocID"].ToString(), dataRow["VoucherID"].ToString(), "Employee_Loan", "VoucherID", sqlTransaction);
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

		public EmployeeLoanData GetEmployeeLoanByID(string sysDocID, string voucherID)
		{
			return GetEmployeeLoanByID(sysDocID, voucherID, null);
		}

		public EmployeeLoanData GetEmployeeLoanByID(string sysDocID, string voucherID, SqlTransaction sqlTransaction)
		{
			try
			{
				EmployeeLoanData employeeLoanData = new EmployeeLoanData();
				string textCommand = "SELECT *, (SELECT SUM(ISNULL(Debit,0)-ISNULL(Credit,0)) FROM Employee_Loan_Detail ELD WHERE ELD.LoanSysDocID = EL.SysDocID AND ELD.LoanVoucherID = EL.VoucherID)  AS Balance ,\r\n                                CASE WHEN Amount=0 THEN 'True' \r\n                                WHEN (Amount<>0) AND ISNULL(PaidAmount,0)+ ISNULL(DiscountAmount,0)=0 THEN 'True' ELSE 'False' END AS CanEdit\r\n                                FROM Employee_Loan EL WHERE SysDocID = '" + sysDocID + "' AND VoucherID='" + voucherID + "'";
				FillDataSet(employeeLoanData, "Employee_Loan", textCommand, sqlTransaction);
				if (employeeLoanData == null || employeeLoanData.Tables.Count == 0 || employeeLoanData.Tables["Employee_Loan"].Rows.Count == 0)
				{
					return null;
				}
				textCommand = "SELECT ELD.* FROM Employee_Loan_Detail ELD\r\n                                    WHERE LoanSysDocID = '" + sysDocID + "' AND LoanVoucherID='" + voucherID + "'";
				FillDataSet(employeeLoanData, "Employee_Loan_Detail", textCommand, sqlTransaction);
				return employeeLoanData;
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
				EmployeeLoanData employeeLoanData = new EmployeeLoanData();
				string textCommand = "SELECT * FROM Employee_Loan_Payment\r\n                                WHERE SysDocID = '" + sysDocID + "' AND VoucherID='" + voucherID + "'";
				FillDataSet(employeeLoanData, "Employee_Loan_Payment", textCommand);
				return employeeLoanData;
			}
			catch
			{
				throw;
			}
		}

		public DataSet GetFirstEmployeeLoanByID(string voucherID, string employeeID, SqlTransaction sqlTransaction)
		{
			try
			{
				DataSet dataSet = new DataSet();
				string text = "SELECT TOP(1) *,ISNULL(Amount,0)-ISNULL(PaidAmount,0) - ISNULL(DiscountAmount,0) AS Balance \r\n                                FROM Employee_Loan WHERE EmployeeID = '" + employeeID + "' AND VoucherID='" + voucherID + "' \r\n                                AND ISNULL(IsVoid,'False')='False'\r\n                                AND ( SELECT ISNULL(Amount,0)-ISNULL(PaidAmount,0) - ISNULL(DiscountAmount,0) AS Balance \r\n                                FROM Employee_Loan WHERE EmployeeID = '" + employeeID + "' AND VoucherID='" + voucherID + "') > 0";
				new SqlCommand(text);
				FillDataSet(dataSet, "Employee_Loan", text, sqlTransaction);
				if (dataSet == null || dataSet.Tables.Count == 0 || dataSet.Tables["Employee_Loan"].Rows.Count == 0)
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

		public bool DeleteLoan(string sysDocID, string voucherID)
		{
			bool flag = true;
			try
			{
				if (!CanEditLoan(sysDocID, voucherID))
				{
					throw new CompanyException("This loan transaction is already paid and cannot be deleted.");
				}
				SqlTransaction sqlTransaction = base.DBConfig.StartNewTransaction();
				flag &= UpdateLoanPaidAmount(sysDocID, voucherID, sqlTransaction);
				string commandText = "DELETE FROM Employee_Loan WHERE SysDocID = '" + sysDocID + "' AND VoucherID='" + voucherID + "'";
				flag &= Delete(commandText, sqlTransaction);
				flag &= DeleteLoanDetailsRows(sqlTransaction, sysDocID, voucherID, isPaymentRow: false);
				flag &= new Journal(base.DBConfig).DeleteJournal(sysDocID, voucherID, sqlTransaction);
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
				string value = dataRow["DivisionID"].ToString();
				string value2 = dataRow["CompanyID"].ToString();
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
				dataRow3["DivisionID"] = value;
				dataRow3["CompanyID"] = value2;
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
				dataRow3["DivisionID"] = value;
				dataRow3["CompanyID"] = value2;
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
				string text3 = dataRow["LoanSysDocID"].ToString();
				string checkFieldValue = dataRow["LoanVoucherID"].ToString();
				string value = dataRow["DivisionID"].ToString();
				string value2 = dataRow["CompanyID"].ToString();
				string text4 = dataRow["EmployeeID"].ToString();
				string textCommand = "SELECT EmployeeID,ISNULL(EMP.AccountID,ET.AccountID) AS AccountID \r\n                                FROM Employee EMP LEFT OUTER JOIN Employee_Type ET ON EMP.ContractType = ET.TypeID\r\n                                WHERE EMP.EmployeeID IN ('" + text4 + "') ";
				DataSet dataSet = new DataSet();
				FillDataSet(dataSet, "Employee", textCommand, sqlTransaction);
				textCommand = "SELECT  LOC.EmployeeAccountID AS AccountID  FROM System_Document SD INNER JOIN Location LOC ON SD.LocationID = LOC.LocationID WHERE SysDocID = '" + text3 + "'";
				DataSet dataSet2 = new DataSet();
				FillDataSet(dataSet2, "Accounts", textCommand, sqlTransaction);
				DataRow[] array = dataSet.Tables[0].Select("EmployeeID = '" + text4 + "'");
				if (array.Length != 0 && array[0]["AccountID"] != DBNull.Value)
				{
					text = array[0]["AccountID"].ToString();
				}
				if (text == "")
				{
					text = dataSet2.Tables[0].Rows[0]["AccountID"].ToString();
				}
				if (dataSet2 == null || dataSet2.Tables.Count == 0 || dataSet2.Tables[0].Rows.Count == 0)
				{
					throw new CompanyException("There is no location assigned to this system document or location record is missing.");
				}
				if (text == "")
				{
					throw new CompanyException("Account is not set for the employee '" + text4 + "'.", 1021);
				}
				string idFieldValue = new Databases(base.DBConfig).GetFieldValue("Employee_Loan", "LoanType", "SysDocID", text3, "VoucherID", checkFieldValue, sqlTransaction).ToString();
				text2 = new Databases(base.DBConfig).GetFieldValue("Employee_Loan_Type", "AccountID", "LoanTypeID", idFieldValue, sqlTransaction).ToString();
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
				dataRow3["PayeeID"] = text4;
				dataRow3["Debit"] = DBNull.Value;
				dataRow3["Credit"] = num;
				dataRow3["Description"] = "Loan Recovery";
				dataRow3["Reference"] = dataRow["Reference"];
				dataRow3["DivisionID"] = value;
				dataRow3["CompanyID"] = value2;
				dataRow3.EndEdit();
				gLData.JournalDetailsTable.Rows.Add(dataRow3);
				dataRow3 = gLData.JournalDetailsTable.NewRow();
				dataRow3.BeginEdit();
				dataRow3["JournalID"] = 0;
				dataRow3["AccountID"] = text;
				dataRow3["PayeeID"] = text4;
				dataRow3["PayeeType"] = "E";
				dataRow3["IsARAP"] = true;
				dataRow3["Description"] = "Loan Recovery-No:" + str;
				dataRow3["Debit"] = num;
				dataRow3["Credit"] = DBNull.Value;
				dataRow3["Reference"] = dataRow["Reference"];
				dataRow3["DivisionID"] = value;
				dataRow3["CompanyID"] = value2;
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
				string text3 = dataRow["LoanSysDocID"].ToString();
				string checkFieldValue = dataRow["LoanVoucherID"].ToString();
				string value = dataRow["DivisionID"].ToString();
				string value2 = dataRow["CompanyID"].ToString();
				string text4 = dataRow["EmployeeID"].ToString();
				string textCommand = "SELECT EmployeeID,ISNULL(EMP.AccountID,ET.AccountID) AS AccountID \r\n                                FROM Employee EMP LEFT OUTER JOIN Employee_Type ET ON EMP.ContractType = ET.TypeID\r\n                                WHERE EMP.EmployeeID IN ('" + text4 + "') ";
				DataSet dataSet = new DataSet();
				FillDataSet(dataSet, "Employee", textCommand, sqlTransaction);
				textCommand = "SELECT  LOC.EmployeeAccountID AS AccountID  FROM System_Document SD INNER JOIN Location LOC ON SD.LocationID = LOC.LocationID WHERE SysDocID = '" + text3 + "'";
				DataSet dataSet2 = new DataSet();
				FillDataSet(dataSet2, "Accounts", textCommand, sqlTransaction);
				DataRow[] array = dataSet.Tables[0].Select("EmployeeID = '" + text4 + "'");
				if (array.Length != 0 && array[0]["AccountID"] != DBNull.Value)
				{
					text = array[0]["AccountID"].ToString();
				}
				if (text == "")
				{
					text = dataSet2.Tables[0].Rows[0]["AccountID"].ToString();
				}
				if (dataSet2 == null || dataSet2.Tables.Count == 0 || dataSet2.Tables[0].Rows.Count == 0)
				{
					throw new CompanyException("There is no location assigned to this system document or location record is missing.");
				}
				if (text == "")
				{
					throw new CompanyException("Account is not set for the employee '" + text4 + "'.", 1021);
				}
				string idFieldValue = new Databases(base.DBConfig).GetFieldValue("Employee_Loan", "LoanType", "SysDocID", text3, "VoucherID", checkFieldValue, sqlTransaction).ToString();
				text2 = (string)(dataRow["LoanAccountID"] = new Databases(base.DBConfig).GetFieldValue("Employee_Loan_Type", "AccountID", "LoanTypeID", idFieldValue, sqlTransaction).ToString());
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
				dataRow3["PayeeID"] = text4;
				dataRow3["Debit"] = DBNull.Value;
				dataRow3["Credit"] = num;
				dataRow3["Description"] = "Loan settlement";
				dataRow3["Reference"] = dataRow["Reference"];
				dataRow3["DivisionID"] = value;
				dataRow3["CompanyID"] = value2;
				dataRow3.EndEdit();
				gLData.JournalDetailsTable.Rows.Add(dataRow3);
				dataRow3 = gLData.JournalDetailsTable.NewRow();
				dataRow3.BeginEdit();
				dataRow3["JournalID"] = 0;
				dataRow3["AccountID"] = text;
				dataRow3["PayeeID"] = text4;
				dataRow3["PayeeType"] = "E";
				dataRow3["IsARAP"] = true;
				dataRow3["Description"] = "Loan settlement-No:" + str;
				dataRow3["Debit"] = num;
				dataRow3["Credit"] = DBNull.Value;
				dataRow3["Reference"] = dataRow["Reference"];
				dataRow3["DivisionID"] = value;
				dataRow3["CompanyID"] = value2;
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

		public DataSet GetEmployeeLoanToPrint(string SysDocID, string VoucehrID)
		{
			string text = "SELECT FirstName+' '+LastName AS [Name] ,EL.*,(EL.Amount/EL.InstallmentAmount) AS [Count],LT.LoanTypeName,\r\n                            (EL.Amount-ISNULL((SELECT SUM(Credit) FROM Employee_Loan_Detail WHERE LoanSysDocID=EL.SysDocID AND LoanVoucherID=EL.VoucherID),0)) AS Balance\r\n                            FROM Employee_Loan EL LEFT JOIN Employee E ON EL.EmployeeID=E.EmployeeID\r\n                            LEFT JOIN Employee_Loan_Type LT ON LT.LoanTypeID=EL.LoanType\r\n                            WHERE 1=1 ";
			if (SysDocID != "")
			{
				text = text + " AND EL.SysDocID='" + SysDocID + "'";
			}
			if (VoucehrID != "")
			{
				text = text + " AND EL.VoucherID='" + VoucehrID + "'";
			}
			DataSet dataSet = new DataSet();
			FillDataSet(dataSet, "Employee_Loan", text);
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

		public EmployeeLoanData GetEmployeeLoanSettlementByID(string sysDocID, string voucherID)
		{
			return GetEmployeeLoanSettlementByID(sysDocID, voucherID, null);
		}

		public EmployeeLoanData GetEmployeeLoanSettlementByID(string sysDocID, string voucherID, SqlTransaction sqlTransaction)
		{
			try
			{
				EmployeeLoanData employeeLoanData = new EmployeeLoanData();
				string textCommand = "SELECT EL.SysDocID,EL.VoucherID,EL.LoanSysDocID,EL.LoanVoucherID,EL.Note,EL.SettlementAmount,EL.TransactionDate,EM.LoanType,EM.Amount,EM.InstallmentAmount,EM.DedStartDate,EM.PaidAmount,EM.Reference,EM.EmployeeID,EM.LoanType, (SELECT SUM(ISNULL(Debit,0)-ISNULL(Credit,0)) FROM Employee_Loan_Detail ELD WHERE ELD.LoanSysDocID = EL.LoanSysDocID AND ELD.LoanVoucherID = EL.LoanVoucherID)  AS Balance \r\n                               \r\n                                FROM Employee_Loan_Settlement EL INNER JOIN Employee_Loan EM ON EM.SysDocID=EL.LoanSysDocID AND EM.VoucherID=EL.LoanVoucherID WHERE EL.SysDocID = '" + sysDocID + "' AND EL.VoucherID='" + voucherID + "'";
				FillDataSet(employeeLoanData, "Employee_Loan_Settlement", textCommand, sqlTransaction);
				if (employeeLoanData == null || employeeLoanData.Tables.Count == 0 || employeeLoanData.Tables["Employee_Loan_Settlement"].Rows.Count == 0)
				{
					return null;
				}
				textCommand = "SELECT ELD.*  FROM Employee_Loan_Detail ELD\r\n                                    WHERE LoanSysDocID IN (SELECT LoanSysDocID FROM Employee_Loan_Settlement WHERE SysDocID= '" + sysDocID + "') AND LoanVoucherID IN (SELECT LoanVoucherID FROM Employee_Loan_Settlement WHERE VoucherID='" + voucherID + "')";
				FillDataSet(employeeLoanData, "Employee_Loan_Detail", textCommand, sqlTransaction);
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

		public DataSet GetEmployeePendingLoanList(string employeeID)
		{
			DataSet dataSet = new DataSet();
			string textCommand = "SELECT SysDocID,VoucherID,TransactionDate [Date],ELT.LoanTypeName [Loan Type],ELT.AccountID,Reason ,Amount,ISNULL(PaidAmount,0)[Paid Amount],ISNULL(DiscountAmount,0) [Discount Amount],Amount-ISNULL(PaidAmount,0)-ISNULL(DiscountAmount,0) [Balance] \r\n                                FROM Employee_Loan EL Left Join Employee_Loan_Type ELT ON ELT.LoanTypeID=EL.LoanType\r\n                                WHERE Amount- ISNULL(PaidAmount,0) - ISNULL(DiscountAmount,0) >0 AND ISNULL(IsVoid,'False')='False' AND EmployeeID='" + employeeID + "'";
			FillDataSet(dataSet, "Employee_Loan", textCommand);
			textCommand = "SELECT ISnull(Sum(Amount-ISNULL(PaidAmount,0)-ISNULL(DiscountAmount,0)),0) [Balance] \r\n                                FROM Employee_Loan EL \r\n                                WHERE Amount- ISNULL(PaidAmount,0) - ISNULL(DiscountAmount,0) >0 AND ISNULL(IsVoid,'False')='False' AND EmployeeID='" + employeeID + "'";
			FillDataSet(dataSet, "Employee_Loan_Sum", textCommand);
			return dataSet;
		}
	}
}
