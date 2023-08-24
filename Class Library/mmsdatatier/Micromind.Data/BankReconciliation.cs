using Micromind.Common;
using Micromind.Common.Data;
using Micromind.Common.Libraries;
using System;
using System.Data;
using System.Data.SqlClient;

namespace Micromind.Data
{
	public sealed class BankReconciliation : StoreObject
	{
		public const string BANKRECONCILIATION_TABLE = "Bank_Reconciliation";

		public const string BANKRECID_PARM = "@BankReconciliationID";

		public const string ACCOUNTID_PARM = "@AccountID";

		public const string SYSDOCID_PARM = "@SysDocID";

		public const string VOUCHERID_PARM = "@VoucherID";

		public const string DESCRIPTION_PARM = "@Description";

		public const string TRANSACTIONDATE_PARM = "@TransactionDate";

		public const string RECONCILEDATE_PARM = "@ReconcileDate";

		public const string DEBIT_PARM = "@Debit";

		public const string CREDIT_PARM = "@Credit";

		public const string DEBITFC_PARM = "@DebitFC";

		public const string CREDITFC_PARM = "@CreditFC";

		public const string ROWINDEX_PARM = "@RowIndex";

		public const string CREATEDBY_PARM = "@CreatedBy";

		public const string DATECREATED_PARM = "@DateCreated";

		public const string UPDATEDBY_PARM = "@UpdatedBy";

		public const string DATEUPDATED_PARM = "@DateUpdated";

		public BankReconciliation(Config config)
			: base(config)
		{
		}

		private string GetInsertUpdateBankReconciliationText(bool isUpdate)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddInsertUpdateParameters("Bank_Reconciliation", new FieldValue("AccountID", "@AccountID", isUpdateConditionField: true), new FieldValue("SysDocID", "@SysDocID", isUpdateConditionField: true), new FieldValue("VoucherID", "@VoucherID", isUpdateConditionField: true), new FieldValue("Description", "@Description"), new FieldValue("TransactionDate", "@TransactionDate"), new FieldValue("ReconcileDate", "@ReconcileDate"), new FieldValue("Debit", "@Debit"), new FieldValue("Credit", "@Credit"), new FieldValue("RowIndex", "@RowIndex"));
			if (isUpdate && isUpdate)
			{
				return sqlBuilder.GetUpdateExpression();
			}
			return sqlBuilder.GetInsertExpression();
		}

		private SqlCommand GetInsertUpdateBankReconciliationCommand(bool isUpdate)
		{
			SqlParameterCollection parameters;
			if (isUpdate)
			{
				if (updateCommand != null)
				{
					updateCommand = null;
				}
				updateCommand = new SqlCommand(GetInsertUpdateBankReconciliationText(isUpdate: true), base.DBConfig.Connection);
				updateCommand.CommandType = CommandType.Text;
				parameters = updateCommand.Parameters;
			}
			else
			{
				if (insertCommand != null)
				{
					insertCommand = null;
				}
				insertCommand = new SqlCommand(GetInsertUpdateBankReconciliationText(isUpdate: false), base.DBConfig.Connection);
				insertCommand.CommandType = CommandType.Text;
				parameters = insertCommand.Parameters;
			}
			parameters.Add("@AccountID", SqlDbType.NVarChar);
			parameters.Add("@SysDocID", SqlDbType.NVarChar);
			parameters.Add("@VoucherID", SqlDbType.NVarChar);
			parameters.Add("@Description", SqlDbType.NVarChar);
			parameters.Add("@TransactionDate", SqlDbType.DateTime);
			parameters.Add("@ReconcileDate", SqlDbType.DateTime);
			parameters.Add("@Debit", SqlDbType.Money);
			parameters.Add("@Credit", SqlDbType.Money);
			parameters.Add("@RowIndex", SqlDbType.TinyInt);
			parameters["@AccountID"].SourceColumn = "AccountID";
			parameters["@SysDocID"].SourceColumn = "SysDocID";
			parameters["@VoucherID"].SourceColumn = "VoucherID";
			parameters["@Description"].SourceColumn = "Description";
			parameters["@TransactionDate"].SourceColumn = "TransactionDate";
			parameters["@ReconcileDate"].SourceColumn = "ReconcileDate";
			parameters["@Debit"].SourceColumn = "Debit";
			parameters["@Credit"].SourceColumn = "Credit";
			parameters["@RowIndex"].SourceColumn = "RowIndex";
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

		public bool InsertUpdateBankReconciliation(BankReconciliationData bankReconciliationData)
		{
			bool flag = true;
			SqlCommand insertUpdateBankReconciliationCommand = GetInsertUpdateBankReconciliationCommand(isUpdate: false);
			try
			{
				DataRow dataRow = bankReconciliationData.BankReconciliationTable.Rows[0];
				SqlTransaction sqlTransaction = base.DBConfig.StartNewTransaction();
				string text = dataRow["AccountID"].ToString();
				flag &= DeleteBankReconciliationDetailsRows(text, sqlTransaction);
				if (bankReconciliationData.BankReconciliationTable.Rows.Count > 0)
				{
					flag &= Insert(bankReconciliationData, "Bank_Reconciliation", insertUpdateBankReconciliationCommand, sqlTransaction);
				}
				if (!flag)
				{
					return flag;
				}
				flag &= UpdateTableRowInsertUpdateInfo("Bank_Reconciliation", "SysDocID", dataRow["SysDocID"].ToString(), "VoucherID", dataRow["VoucherID"].ToString(), sqlTransaction, isInsert: true);
				string entityName = "Bank Reconciliation Opening";
				flag &= AddActivityLog(entityName, text, ActivityTypes.Add, sqlTransaction);
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

		public BankReconciliationData GetBankReconciliationOpeningList(string accountId)
		{
			try
			{
				BankReconciliationData bankReconciliationData = new BankReconciliationData();
				string textCommand = "SELECT * FROM Bank_Reconciliation WHERE AccountID='" + accountId + "'";
				FillDataSet(bankReconciliationData, "Bank_Reconciliation", textCommand);
				return bankReconciliationData;
			}
			catch
			{
				throw;
			}
		}

		public BankReconciliationData GetBankReconciliationByID(string sysDocID, string voucherID)
		{
			try
			{
				BankReconciliationData bankReconciliationData = new BankReconciliationData();
				string textCommand = "SELECT * FROM Job_Expense_Issue WHERE VoucherID='" + voucherID + "' AND SysDocID='" + sysDocID + "'";
				FillDataSet(bankReconciliationData, "Bank_Reconciliation", textCommand);
				if (bankReconciliationData == null || bankReconciliationData.Tables.Count == 0 || bankReconciliationData.Tables["Bank_Reconciliation"].Rows.Count == 0)
				{
					return null;
				}
				textCommand = "SELECT TD.*\r\n                        FROM Job_Expense_Issue_Detail TD INNER JOIN Expense_Code P ON P.ExpenseID = TD.ExpenseID\r\n                        WHERE VoucherID='" + voucherID + "' AND SysDocID='" + sysDocID + "'";
				FillDataSet(bankReconciliationData, "Bank_Reconciliation", textCommand);
				return bankReconciliationData;
			}
			catch
			{
				throw;
			}
		}

		internal bool DeleteBankReconciliationDetailsRows(string accountID, SqlTransaction sqlTransaction)
		{
			bool flag = true;
			try
			{
				string commandText = "DELETE FROM Bank_Reconciliation WHERE AccountID ='" + accountID + "'";
				return Delete(commandText, sqlTransaction);
			}
			catch
			{
				throw;
			}
		}

		private bool VoidBankReconciliation(string sysDocID, string voucherID, bool isVoid, SqlTransaction sqlTransaction)
		{
			return false;
		}

		public bool DeleteBankReconciliationOpening(string accountID)
		{
			bool flag = true;
			try
			{
				SqlTransaction sqlTransaction = base.DBConfig.StartNewTransaction();
				string commandText = "DELETE FROM Bank_Reconciliation WHERE AccountID ='" + accountID + "'";
				flag = Delete(commandText, sqlTransaction);
				if (!flag)
				{
					return flag;
				}
				ActivityTypes activityType = ActivityTypes.Delete;
				AddActivityLog("Bank Reconciliation Opening", accountID, activityType, sqlTransaction);
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

		public DataSet GetBankReconciliationToPrint(string accountId)
		{
			try
			{
				DataSet dataSet = new DataSet();
				string cmdText = "SELECT DISTINCT AccountID, AccountName FROM Account WHERE AccountID = '" + accountId + "'";
				SqlCommand sqlCommand = new SqlCommand(cmdText);
				FillDataSet(dataSet, "Account", sqlCommand);
				if (dataSet == null || dataSet.Tables.Count == 0 || dataSet.Tables["Account"].Rows.Count == 0)
				{
					return null;
				}
				cmdText = "SELECT * FROM Bank_Reconciliation WHERE AccountID = '" + accountId + "'";
				FillDataSet(dataSet, "Bank_Reconciliation", cmdText);
				dataSet.Relations.Add("BankReconciliationRelation", dataSet.Tables["Account"].Columns["AccountID"], dataSet.Tables["Bank_Reconciliation"].Columns["AccountID"], createConstraints: false);
				return dataSet;
			}
			catch
			{
				throw;
			}
		}

		public DataSet GetBankReconciliationsReport(string fromAccount, string toAccount, string fromLocationID, string toLocationID, string fromDivisionID, string toDivisionID, DateTime asOfDate)
		{
			string str = StoreConfiguration.ToSqlDateTimeString(asOfDate);
			try
			{
				DataSet dataSet = new DataSet();
				string str2 = "SELECT 'Book Transaction' AS Type, JD.SysDocID, JD.VoucherID, JD.AccountID, (SELECT AccountName FROM Account WHERE AccountID = JD.AccountID) AS AccountName,(SELECT Alias FROM Account WHERE AccountID = JD.AccountID) AS Alias,\r\n                                J.JournalDate, ReconcileDate, ISNULL(Debit, 0) Debit, ISNULL(Credit, 0) Credit \r\n                                FROM Journal_Details JD\r\n                                 INNER JOIN Journal J ON JD.JournalID = J.JournalID ";
				if (fromLocationID != "")
				{
					str2 += " LEFT JOIN System_Document SD ON SD.SysDocID=JD.SysDocID";
				}
				str2 = str2 + " WHERE JournalDate <= '" + str + "'";
				if (fromAccount != "")
				{
					str2 = str2 + " AND AccountID Between '" + fromAccount + "' AND '" + toAccount + "'";
				}
				if (fromLocationID != "")
				{
					str2 = str2 + "AND SD.LocationID >='" + fromLocationID + "' AND SD.LocationID <='" + toLocationID + "'";
				}
				if (fromDivisionID != "")
				{
					str2 = str2 + "AND JD.DivisionID >='" + fromDivisionID + "' AND JD.DivisionID <='" + toDivisionID + "'";
				}
				str2 = str2 + " UNION ALL\r\n                         SELECT 'Book Transaction' AS Type, SysDocID, VoucherID, BR.AccountID, (SELECT AccountName FROM Account WHERE AccountID = BR.AccountID) AS AccountName,(SELECT Alias FROM Account WHERE AccountID = BR.AccountID) AS Alias, \r\n                         TransactionDate, ReconcileDate, ISNULL(Debit, 0) Debit, ISNULL(Credit, 0) Credit \r\n                         FROM Bank_Reconciliation BR WHERE TransactionDate <= '" + str + "'";
				if (fromAccount != "")
				{
					str2 = str2 + " AND AccountID Between '" + fromAccount + "' AND '" + toAccount + "'";
				}
				str2 += " UNION ALL\r\n                         SELECT 'Not Reconciled' AS Type, JD.SysDocID, JD.VoucherID, JD.AccountID, (SELECT AccountName FROM Account WHERE AccountID = JD.AccountID) AS AccountName, (SELECT Alias FROM Account WHERE AccountID = JD.AccountID) AS Alias,\r\n                         J.JournalDate, ReconcileDate, ISNULL(Credit, 0) Credit,  ISNULL(Debit, 0) Debit\r\n                         FROM Journal_Details JD\r\n                         INNER JOIN Journal J ON JD.JournalID = J.JournalID ";
				if (fromLocationID != "")
				{
					str2 += " LEFT JOIN System_Document SD ON SD.SysDocID=JD.SysDocID";
				}
				str2 = str2 + " WHERE (ReconcileDate  IS NULL  AND  JournalDate < '" + str + "')";
				if (fromAccount != "")
				{
					str2 = str2 + " AND AccountID Between '" + fromAccount + "' AND '" + toAccount + "'";
				}
				if (fromLocationID != "")
				{
					str2 = str2 + "AND SD.LocationID >='" + fromLocationID + "' AND SD.LocationID <='" + toLocationID + "'";
				}
				if (fromDivisionID != "")
				{
					str2 = str2 + "AND JD.DivisionID >='" + fromDivisionID + "' AND JD.DivisionID <='" + toDivisionID + "'";
				}
				str2 = str2 + " UNION ALL\r\n                         SELECT 'Not Reconciled' AS Type, SysDocID, VoucherID, AccountID, (SELECT AccountName FROM Account WHERE AccountID = BR.AccountID) AS AccountName, (SELECT Alias FROM Account WHERE AccountID = BR.AccountID) AS Alias,\r\n                        TransactionDate, ReconcileDate,  ISNULL(Credit, 0)  Credit, ISNULL(Debit, 0) Debit\r\n                         FROM Bank_Reconciliation BR WHERE ReconcileDate < '" + str + "'";
				if (fromAccount != "")
				{
					str2 = str2 + " AND AccountID Between '" + fromAccount + "' AND '" + toAccount + "'";
				}
				new SqlCommand(str2);
				FillDataSet(dataSet, "Bank_Reconciliation", str2);
				return dataSet;
			}
			catch
			{
				throw;
			}
		}

		public DataSet GetBankNotReconciledReport(string fromAccount, string toAccount, string fromLocationID, string toLocationID, string fromDivisionID, string toDivisionID, DateTime asOfDate)
		{
			string str = StoreConfiguration.ToSqlDateTimeString(asOfDate);
			try
			{
				DataSet dataSet = new DataSet();
				string str2 = "SELECT  JD.SysDocID, JD.VoucherID, JD.AccountID, (SELECT AccountName FROM Account WHERE AccountID = JD.AccountID) AS AccountName,(SELECT Alias FROM Account WHERE AccountID = JD.AccountID) AS Alias, \r\n                                J.JournalDate, ReconcileDate, ISNULL(Credit, 0) Credit,  ISNULL(Debit, 0) Debit\r\n                                ,CheckNumber, CheckDate,\r\n                                ISNULL((SELECT DISTINCT PayeeID FROM Cheque_Issued CI WHERE CI.ClearanceSysDocID=J.SysDocID AND CI.ClearanceVoucherID=J.VoucherID AND CI.ChequeID=JD.CheckID),(SELECT DISTINCT PayeeID FROM Cheque_Received CR WHERE CR.DepositSysDocID=J.SysDocID AND CR.DepositVoucherID=J.VoucherID AND CR.ChequeID=JD.CheckID ) ) AS PayeeAccount,\r\n                                ISNULL((SELECT DISTINCT CASE PayeeType WHEN 'C' THEN C.CustomerName WHEN 'V' THEN V.VendorName  ELSE  E.FirstName + ' ' + E.LastName END\r\n                                FROM Cheque_Issued CI  \r\n                                LEFT OUTER JOIN Customer C ON CI.PayeeID=C.CustomerID LEFT OUTER JOIN\r\n                                Vendor V ON CI.PayeeID=V.VendorID LEFT OUTER JOIN Employee E ON E.EmployeeID=CI.PayeeID WHERE CI.ClearanceSysDocID=J.SysDocID AND CI.ClearanceVoucherID=J.VoucherID AND CI.ChequeID=JD.CheckID),\r\n                                (SELECT DISTINCT CASE PayeeType WHEN 'C' THEN C.CustomerName WHEN 'V' THEN V.VendorName  ELSE  E.FirstName + ' ' + E.LastName END \r\n                                FROM Cheque_Received CR \r\n                                LEFT OUTER JOIN Customer C ON CR.PayeeID=C.CustomerID LEFT OUTER JOIN\r\n                                Vendor V ON CR.PayeeID=V.VendorID LEFT OUTER JOIN Employee E ON E.EmployeeID=CR.PayeeID WHERE CR.DepositSysDocID=J.SysDocID AND CR.DepositVoucherID=J.VoucherID AND CR.ChequeID=JD.CheckID) ) AS PayeeName\r\n                                FROM Journal_Details JD\r\n                                INNER JOIN Journal J ON JD.JournalID = J.JournalID   ";
				if (fromLocationID != "")
				{
					str2 += " LEFT JOIN System_Document SD ON SD.SysDocID=JD.SysDocID";
				}
				str2 = str2 + " WHERE isnull( ReconcileDate,'') =  '' AND ISNULL(J.IsVoid,'False') = 'False'   \r\n                               AND J.JournalDate <= '" + str + "'";
				if (fromAccount != "")
				{
					str2 = str2 + " AND AccountID Between '" + fromAccount + "' AND '" + toAccount + "'";
				}
				if (fromLocationID != "")
				{
					str2 = str2 + "AND SD.LocationID >='" + fromLocationID + "' AND SD.LocationID <='" + toLocationID + "'";
				}
				if (fromDivisionID != "")
				{
					str2 = str2 + "AND JD.DivisionID >='" + fromDivisionID + "' AND JD.DivisionID <='" + toDivisionID + "'";
				}
				str2 += "order by  J.JournalDate";
				new SqlCommand(str2);
				FillDataSet(dataSet, "Bank_Reconciliation", str2);
				return dataSet;
			}
			catch
			{
				throw;
			}
		}
	}
}
