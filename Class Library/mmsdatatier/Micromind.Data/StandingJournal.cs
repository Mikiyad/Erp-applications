using Micromind.Common;
using Micromind.Common.Data;
using Micromind.Common.Libraries;
using System;
using System.Data;
using System.Data.SqlClient;

namespace Micromind.Data
{
	public sealed class StandingJournal : StoreObject
	{
		private const string STANDINGJOURNALID_PARM = "@StandingJournalID";

		private const string CURRENCYID_PARM = "@CurrencyID";

		private const string CURRENCYRATE_PARM = "@CurrencyRate";

		private const string REFERENCE_PARM = "@Reference";

		private const string STARTYEAR_PARM = "@StartYear";

		private const string ENDYEAR_PARM = "@EndYear";

		private const string STARTMONTH_PARM = "@StartMonth";

		private const string ENDMONTH_PARM = "@EndMonth";

		private const string STATUS_PARM = "@Status";

		private const string NARRATION_PARM = "@Narration";

		private const string TRANSACTIONSYSDOCID_PARM = "@TransactionSysDocID";

		private const string NOTE_PARM = "@Note";

		private const string ACCOUNTID_PARM = "@AccountID";

		private const string DEBIT_PARM = "@Debit";

		private const string CREDIT_PARM = "@Credit";

		private const string ANALYSISID_PARM = "@AnalysisID";

		private const string PAYEEID_PARM = "@PayeeID";

		private const string PAYEETYPE_PARM = "@PayeeType";

		private const string ROWINDEX_PARM = "@RowIndex";

		private const string COSTCENTERID_PARM = "@CostCenterID";

		private const string DESCRIPTION_PARM = "@Description";

		public StandingJournal(Config config)
			: base(config)
		{
		}

		private string GetInsertUpdateStandingJournalText(bool isUpdate)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddInsertUpdateParameters("Standing_Journal", new FieldValue("StandingJournalID", "@StandingJournalID", isUpdateConditionField: true), new FieldValue("CurrencyID", "@CurrencyID"), new FieldValue("CurrencyRate", "@CurrencyRate"), new FieldValue("Reference", "@Reference"), new FieldValue("StartYear", "@StartYear"), new FieldValue("StartMonth", "@StartMonth"), new FieldValue("EndYear", "@EndYear"), new FieldValue("EndMonth", "@EndMonth"), new FieldValue("Status", "@Status"), new FieldValue("TransactionSysDocID", "@TransactionSysDocID"), new FieldValue("Narration", "@Narration"), new FieldValue("Note", "@Note"));
			if (isUpdate)
			{
				return sqlBuilder.GetUpdateExpression();
			}
			return sqlBuilder.GetInsertExpression();
		}

		private SqlCommand GetInsertUpdateStandingJournalCommand(bool isUpdate)
		{
			SqlParameterCollection parameters;
			if (isUpdate)
			{
				if (updateCommand != null)
				{
					updateCommand = null;
				}
				updateCommand = new SqlCommand(GetInsertUpdateStandingJournalText(isUpdate: true), base.DBConfig.Connection);
				updateCommand.CommandType = CommandType.Text;
				parameters = updateCommand.Parameters;
			}
			else
			{
				if (insertCommand != null)
				{
					insertCommand = null;
				}
				insertCommand = new SqlCommand(GetInsertUpdateStandingJournalText(isUpdate: false), base.DBConfig.Connection);
				insertCommand.CommandType = CommandType.Text;
				parameters = insertCommand.Parameters;
			}
			parameters.Add("@StandingJournalID", SqlDbType.NVarChar);
			parameters.Add("@CurrencyID", SqlDbType.NVarChar);
			parameters.Add("@CurrencyRate", SqlDbType.Decimal);
			parameters.Add("@TransactionSysDocID", SqlDbType.NVarChar);
			parameters.Add("@Reference", SqlDbType.NVarChar);
			parameters.Add("@StartYear", SqlDbType.SmallInt);
			parameters.Add("@StartMonth", SqlDbType.TinyInt);
			parameters.Add("@EndYear", SqlDbType.SmallInt);
			parameters.Add("@EndMonth", SqlDbType.TinyInt);
			parameters.Add("@Status", SqlDbType.TinyInt);
			parameters.Add("@Narration", SqlDbType.NVarChar);
			parameters.Add("@Note", SqlDbType.NVarChar);
			parameters["@StandingJournalID"].SourceColumn = "StandingJournalID";
			parameters["@TransactionSysDocID"].SourceColumn = "TransactionSysDocID";
			parameters["@CurrencyID"].SourceColumn = "CurrencyID";
			parameters["@CurrencyRate"].SourceColumn = "CurrencyRate";
			parameters["@Reference"].SourceColumn = "Reference";
			parameters["@StartYear"].SourceColumn = "StartYear";
			parameters["@StartMonth"].SourceColumn = "StartMonth";
			parameters["@EndYear"].SourceColumn = "EndYear";
			parameters["@EndMonth"].SourceColumn = "EndMonth";
			parameters["@Status"].SourceColumn = "Status";
			parameters["@Narration"].SourceColumn = "Narration";
			parameters["@Note"].SourceColumn = "Note";
			if (isUpdate)
			{
				return updateCommand;
			}
			return insertCommand;
		}

		private string GetInsertUpdateStandingJournalDetailsText(bool isUpdate)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddInsertUpdateParameters("Standing_Journal_Detail", new FieldValue("StandingJournalID", "@StandingJournalID", isUpdateConditionField: true), new FieldValue("Reference", "@Reference"), new FieldValue("Description", "@Description"), new FieldValue("Debit", "@Debit"), new FieldValue("Credit", "@Credit"), new FieldValue("CurrencyID", "@CurrencyID"), new FieldValue("CurrencyRate", "@CurrencyRate"), new FieldValue("AccountID", "@AccountID"), new FieldValue("CostCenterID", "@CostCenterID"), new FieldValue("AnalysisID", "@AnalysisID"), new FieldValue("RowIndex", "@RowIndex"), new FieldValue("PayeeID", "@PayeeID"), new FieldValue("PayeeType", "@PayeeType"));
			if (isUpdate)
			{
				return sqlBuilder.GetUpdateExpression();
			}
			return sqlBuilder.GetInsertExpression();
		}

		private SqlCommand GetInsertUpdateStandingJournalDetailsCommand(bool isUpdate)
		{
			updateCommand = null;
			insertCommand = null;
			SqlParameterCollection parameters;
			if (isUpdate)
			{
				updateCommand = new SqlCommand(GetInsertUpdateStandingJournalDetailsText(isUpdate: true), base.DBConfig.Connection);
				updateCommand.CommandType = CommandType.Text;
				parameters = updateCommand.Parameters;
			}
			else
			{
				insertCommand = new SqlCommand(GetInsertUpdateStandingJournalDetailsText(isUpdate: false), base.DBConfig.Connection);
				insertCommand.CommandType = CommandType.Text;
				parameters = insertCommand.Parameters;
			}
			parameters.Add("@StandingJournalID", SqlDbType.NVarChar);
			parameters.Add("@AccountID", SqlDbType.NVarChar);
			parameters.Add("@Debit", SqlDbType.Money);
			parameters.Add("@Credit", SqlDbType.Money);
			parameters.Add("@Description", SqlDbType.NVarChar);
			parameters.Add("@CurrencyID", SqlDbType.NVarChar);
			parameters.Add("@CurrencyRate", SqlDbType.Decimal);
			parameters.Add("@PayeeID", SqlDbType.NVarChar);
			parameters.Add("@PayeeType", SqlDbType.NVarChar);
			parameters.Add("@Reference", SqlDbType.NVarChar);
			parameters.Add("@RowIndex", SqlDbType.NVarChar);
			parameters.Add("@AnalysisID", SqlDbType.NVarChar);
			parameters.Add("@CostCenterID", SqlDbType.NVarChar);
			parameters["@StandingJournalID"].SourceColumn = "StandingJournalID";
			parameters["@AccountID"].SourceColumn = "AccountID";
			parameters["@Debit"].SourceColumn = "Debit";
			parameters["@Credit"].SourceColumn = "Credit";
			parameters["@Description"].SourceColumn = "Description";
			parameters["@CurrencyID"].SourceColumn = "CurrencyID";
			parameters["@CurrencyRate"].SourceColumn = "CurrencyRate";
			parameters["@RowIndex"].SourceColumn = "RowIndex";
			parameters["@PayeeID"].SourceColumn = "PayeeID";
			parameters["@PayeeType"].SourceColumn = "PayeeType";
			parameters["@Reference"].SourceColumn = "Reference";
			parameters["@AnalysisID"].SourceColumn = "AnalysisID";
			parameters["@CostCenterID"].SourceColumn = "CostCenterID";
			if (isUpdate)
			{
				return updateCommand;
			}
			return insertCommand;
		}

		public bool InsertUpdateStandingJournal(StandingJournalData standingJournalData, bool isUpdate)
		{
			bool flag = true;
			SqlCommand insertUpdateStandingJournalCommand = GetInsertUpdateStandingJournalCommand(isUpdate);
			try
			{
				DataRow dataRow = standingJournalData.StandingJournalTable.Rows[0];
				SqlTransaction sqlTransaction = base.DBConfig.StartNewTransaction();
				string text = dataRow["StandingJournalID"].ToString();
				insertUpdateStandingJournalCommand.Transaction = sqlTransaction;
				flag = (isUpdate ? (flag & Update(standingJournalData, "Standing_Journal", insertUpdateStandingJournalCommand)) : (flag & Insert(standingJournalData, "Standing_Journal", insertUpdateStandingJournalCommand)));
				insertUpdateStandingJournalCommand = GetInsertUpdateStandingJournalDetailsCommand(isUpdate: false);
				insertUpdateStandingJournalCommand.Transaction = sqlTransaction;
				if (isUpdate)
				{
					flag &= DeleteStandingJournalDetailsRows(text, sqlTransaction);
				}
				if (standingJournalData.Tables["Standing_Journal_Detail"].Rows.Count > 0)
				{
					flag &= Insert(standingJournalData, "Standing_Journal_Detail", insertUpdateStandingJournalCommand);
				}
				if (!flag)
				{
					throw new Exception("Failed to save the transaction because of an unexpected error. Please try again.");
				}
				flag &= UpdateTableRowInsertUpdateInfo("Standing_Journal", "StandingJournalID", dataRow["StandingJournalID"].ToString(), sqlTransaction, !isUpdate);
				string entityName = "Stranding Journal";
				if (isUpdate)
				{
					flag &= AddActivityLog(entityName, text, ActivityTypes.Update, sqlTransaction);
					return flag;
				}
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

		public bool DeleteStandingJournal(string journalID)
		{
			bool flag = true;
			try
			{
				string text = "";
				SqlTransaction sqlTransaction = base.DBConfig.StartNewTransaction();
				text = "SELECT COUNT(*) FROM Journal WHERE STJournalID = '" + journalID + "'";
				if (Convert.ToInt16(ExecuteScalar(text, sqlTransaction)) > 0)
				{
					throw new CompanyException("Transaction cannot be deleted because it is in use.", 1049);
				}
				flag &= DeleteStandingJournalDetailsRows(journalID, sqlTransaction);
				text = "DELETE FROM Standing_Journal WHERE StandingJournalID = '" + journalID + "'";
				flag &= (ExecuteNonQuery(text, sqlTransaction) >= 0);
				if (!flag)
				{
					return flag;
				}
				ActivityTypes activityType = ActivityTypes.Delete;
				AddActivityLog("Standing Journal", journalID, activityType, sqlTransaction);
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

		internal bool DeleteStandingJournalDetailsRows(string journalID, SqlTransaction sqlTransaction)
		{
			bool flag = true;
			try
			{
				string commandText = "DELETE FROM Standing_Journal_Detail WHERE StandingJournalID = '" + journalID + "'";
				return Delete(commandText, sqlTransaction);
			}
			catch
			{
				throw;
			}
		}

		private bool ValidateStandingJournalDetailsData(StandingJournalData standingJournalData)
		{
			decimal d = default(decimal);
			decimal d2 = default(decimal);
			foreach (DataRow row in standingJournalData.StandingJournalDetailsTable.Rows)
			{
				decimal result = default(decimal);
				decimal result2 = default(decimal);
				decimal.TryParse(row["Debit"].ToString(), out result);
				decimal.TryParse(row["Credit"].ToString(), out result2);
				if (row["AccountID"].ToString() == "")
				{
					throw new Exception("Account is not enterd for journal row.");
				}
				if (result > 0m && result2 > 0m)
				{
					throw new Exception("Either debit or credit should be zero");
				}
				if (result < 0m || result2 < 0m)
				{
					throw new Exception("Amount cannot be negative.");
				}
				d += result;
				d2 += result2;
			}
			if (d != d2)
			{
				throw new Exception("Total debit must equal total credit.");
			}
			return true;
		}

		public StandingJournalData GetStandingJournalVoucherByID(string journalID)
		{
			try
			{
				StandingJournalData standingJournalData = new StandingJournalData();
				string cmdText = "SELECT * FROM Standing_JOURNAL WHERE StandingJournalID = '" + journalID + "'";
				SqlCommand sqlCommand = new SqlCommand(cmdText);
				FillDataSet(standingJournalData, "Standing_Journal", sqlCommand);
				if (standingJournalData == null || standingJournalData.Tables.Count == 0 || standingJournalData.Tables["Standing_Journal"].Rows.Count == 0)
				{
					return null;
				}
				cmdText = "SELECT JD.*,\r\n                        (CASE PayeeType\r\n                        WHEN 'C' THEN Customer.CustomerName\r\n                        WHEN 'V' THEN Vendor.VendorName\r\n                        WHEN 'E' THEN Employee.FirstName\r\n                        ELSE Account.AccountName END) AS AccountName\r\n                        FROM Standing_JOURNAL_DETAIL JD LEFt OUTER JOIN \r\n                        Account ON JD.AccountID=Account.AccountID LEFt OUTER JOIN\r\n                        Customer ON JD.PayeeID=Customer.CustomerID LEFt OUTER JOIN\r\n                        Vendor ON JD.PayeeID=Vendor.VendorID LEFt OUTER JOIN\r\n                        Employee ON JD.PayeeID=Employee.EmployeeID  \r\n                        WHERE StandingJournalID = '" + journalID + "'";
				FillDataSet(standingJournalData, "Standing_Journal_Detail", cmdText);
				return standingJournalData;
			}
			catch
			{
				throw;
			}
		}

		public StandingJournalData GetStandingJournalVoucherToPrint(string[] voucherID)
		{
			try
			{
				StandingJournalData standingJournalData = new StandingJournalData();
				string text = "";
				for (int i = 0; i < voucherID.Length; i++)
				{
					text = "'" + voucherID[i] + "'";
					if (i < voucherID.Length - 1)
					{
						text += ",";
					}
				}
				string cmdText = "SELECT StandingJournalID,   Reference,   ISNULL(CurrencyRate, 1),StartYear,StartMonth,EndYear,EndMonth,Note,\r\n                                ISNULL(CurrencyID,(SELECT CurrencyID FROM Currency WHERE IsBase='True')) AS CurrencyID FROM Standing_JOURNAL \r\n                               WHERE  StandingJournalID IN (" + text + ")";
				SqlCommand sqlCommand = new SqlCommand(cmdText);
				FillDataSet(standingJournalData, "Standing_Journal", sqlCommand);
				if (standingJournalData == null || standingJournalData.Tables.Count == 0 || standingJournalData.Tables["Standing_Journal"].Rows.Count == 0)
				{
					return null;
				}
				cmdText = "SELECT JD.*,\r\n                        (CASE PayeeType\r\n                        WHEN 'C' THEN Customer.CustomerName\r\n                        WHEN 'V' THEN Vendor.VendorName\r\n                        WHEN 'E' THEN Employee.FirstName\r\n                        ELSE Account.AccountName END) AS AccountName\r\n                        FROM Standing_JOURNAL_DETAIL JD LEFt OUTER JOIN \r\n                        Account ON JD.AccountID=Account.AccountID LEFt OUTER JOIN\r\n                        Customer ON JD.PayeeID=Customer.CustomerID LEFt OUTER JOIN\r\n                        Vendor ON JD.PayeeID=Vendor.VendorID LEFt OUTER JOIN\r\n                        Employee ON JD.PayeeID=Employee.EmployeeID  \r\n                        WHERE  StandingJournalID IN (" + text + ")";
				FillDataSet(standingJournalData, "Standing_Journal_Detail", cmdText);
				standingJournalData.Relations.Add("StandingJournalEntry", standingJournalData.Tables["Standing_Journal"].Columns["StandingJournalID"], standingJournalData.Tables["Standing_Journal_Detail"].Columns["StandingJournalID"], createConstraints: false);
				return standingJournalData;
			}
			catch
			{
				throw;
			}
		}

		public DataSet GetList(DateTime from, DateTime to, bool showVoid)
		{
			DataSet dataSet = new DataSet();
			StoreConfiguration.ToSqlDateTimeString(from);
			StoreConfiguration.ToSqlDateTimeString(to);
			SqlCommand sqlCommand = new SqlCommand("SELECT DISTINCT TransactionSysDocID AS [Doc ID], J.StandingJournalID AS [Journal ID], StartMonth [Start Month], StartYear [Start Year],EndMonth [End Month],EndYear [End Year], \r\n                            J.Reference,Note,\r\n                            SUM(ISNULL(Debit,0)) AS Amount\r\n                            FROM Standing_JOURNAL J LEFT OUTER JOIN Standing_Journal_Detail JD\r\n                            ON  J.StandingJournalID=JD.StandingJournalID WHERE 1=1  " + " GROUP BY J.StandingJournalID, TransactionSysDocID,  J.Reference,J.CurrencyID,J.CurrencyRate,Note,StartMonth,StartYear,EndMonth,EndYear\r\n                            ORDER BY J.StandingJournalID");
			FillDataSet(dataSet, "StandingJournal", sqlCommand);
			sqlCommand = new SqlCommand("Select JD.StandingJournalID, JD.AccountID, AC.AccountName, JD.Description,JD.Reference,PayeeID + ' - ' +\r\n                        CASE PayeeType WHEN 'V' THEN (SELECT VendorName FROM Vendor WHERE VendorID = PayeeID)\r\n\t\t\t                           WHEN 'C' THEN (SELECT CustomerName FROM Customer WHERE CustomerID = PayeeID) \r\n\t\t\t                           WHEN 'A' THEN (SELECT AccountName FROM Account WHERE Account.AccountID = PayeeID) \r\n\t\t\t                           WHEN 'E' THEN (SELECT FirstName + ' ' + LastName FROM Employee WHERE EmployeeID = PayeeID) END AS Payee, Debit,Credit\r\n                        FROM Standing_Journal_Detail JD INNER JOIN Account AC ON JD.AccountID = AC.AccountID  INNER JOIN Standing_Journal J ON J.StandingJournalID = JD.StandingJournalID WHERE 1=1 ");
			FillDataSet(dataSet, "Standing_Journal_Detail", sqlCommand);
			return dataSet;
		}

		public DataSet GetStandingJournalToProcess_New(int year, int month, bool status)
		{
			DataSet dataSet = new DataSet();
			SqlCommand sqlCommand = new SqlCommand("SELECT CASE WHEN (SELECT COUNT(*) FROM Journal WHERE STJYear = " + year + " AND STJMonth = " + month + ") > 0 THEN 'True' ELSE 'False' END AS P,\r\n                                SJ.StandingJournalID, StartMonth,StartYear,EndMonth,EndYear,Narration,SUM(ISNULL(Credit,0)) AS Amount,SJ_Status\r\n                                FROM Standing_Journal SJ INNER JOIN Standing_Journal_Detail SJD ON SJ.StandingJournalID = SJD.StandingJournalID\r\n                                LEFT OUTER JOIN (SELECT Reference2,CASE WHEN Reference2 IS NOT NULL THEN 'True' ELSE 'False' END AS SJ_Status FROM Journal WHERE STJYear = " + year + " AND STJMonth = " + month + ") \r\n                                AS SJ_Processed on SJ_Processed.Reference2=SJ.StandingJournalID\r\n                                WHERE ISNULL(Status,1)= 1 AND ((" + year + " * 12 + " + month + ") BETWEEN StartYear * 12 + StartMonth AND EndYear * 12 + EndMonth) AND ISNULL(SJ_Status,'False')='" + status.ToString() + "' \r\n                                GROUP BY  SJ.StandingJournalID, StartMonth,StartYear,EndMonth,EndYear,Narration,SJ_Status ");
			FillDataSet(dataSet, "StandingJournal", sqlCommand);
			return dataSet;
		}

		public DataSet GetStandingJournalToProcess(int year, int month, bool status)
		{
			DataSet dataSet = new DataSet();
			SqlCommand sqlCommand = new SqlCommand("SELECT CASE WHEN (SELECT COUNT(*) FROM Journal WHERE STJYear = " + year + " AND STJMonth = " + month + ") > 0 THEN 'True' ELSE 'False' END AS P,\r\n                                SJ.StandingJournalID, StartMonth,StartYear,EndMonth,EndYear,Narration,SUM(ISNULL(Credit,0)) AS Amount\r\n                                FROM Standing_Journal SJ INNER JOIN Standing_Journal_Detail SJD ON SJ.StandingJournalID = SJD.StandingJournalID\r\n                                WHERE ISNULL(Status,1)= 1 AND ((" + year + " * 12 + " + month + ") BETWEEN StartYear * 12 + StartMonth AND EndYear * 12 + EndMonth)\r\n                                GROUP BY  SJ.StandingJournalID, StartMonth,StartYear,EndMonth,EndYear,Narration ");
			FillDataSet(dataSet, "StandingJournal", sqlCommand);
			return dataSet;
		}

		public bool ProcessStandingJournal(string standingJournalID, int year, int month)
		{
			bool flag = true;
			try
			{
				string exp = "SELECT COUNT(*) FROM Journal WHERE Reference2 = '" + standingJournalID + "' AND STJYear = " + year + " AND STJMonth = " + month;
				if (Convert.ToInt16(ExecuteScalar(exp)) > 0)
				{
					throw new CompanyException("Standing journal '" + standingJournalID + "' for selected period is already created.", 1050);
				}
				StandingJournalData standingJournalVoucherByID = GetStandingJournalVoucherByID(standingJournalID);
				if (standingJournalVoucherByID == null || standingJournalVoucherByID.Tables.Count == 0 || standingJournalVoucherByID.StandingJournalTable.Rows.Count == 0)
				{
					throw new CompanyException("Transaction not found.");
				}
				DataRow dataRow = standingJournalVoucherByID.StandingJournalTable.Rows[0];
				int year2 = Convert.ToInt16(dataRow["StartYear"]);
				int month2 = Convert.ToInt16(dataRow["StartMonth"]);
				int year3 = Convert.ToInt16(dataRow["EndYear"]);
				int month3 = Convert.ToInt16(dataRow["EndMonth"]);
				DateTime t = new DateTime(year2, month2, 1);
				DateTime t2 = new DateTime(year3, month3, 1);
				DateTime dateTime = new DateTime(year, month, 1);
				if (dateTime < t || dateTime > t2)
				{
					throw new CompanyException("Selected period is out of range of standing journal.");
				}
				string baseCurrencyID = new Currencies(base.DBConfig).GetBaseCurrencyID();
				GLData gLData = new GLData();
				DataRow dataRow2 = gLData.JournalTable.NewRow();
				dataRow2["JournalID"] = 0;
				dataRow2["JournalDate"] = dateTime;
				dataRow2["SysDocID"] = dataRow["TransactionSysDocID"];
				dataRow2["VoucherID"] = new SystemDocuments(base.DBConfig).GetNextDocumentNumber(dataRow["TransactionSysDocID"].ToString());
				dataRow2["SysDocType"] = ((byte)1).ToString();
				dataRow2["Reference"] = dataRow["Reference"];
				dataRow2["Reference2"] = standingJournalID;
				dataRow2["CurrencyID"] = dataRow["CurrencyID"];
				dataRow2["CurrencyRate"] = dataRow["CurrencyRate"];
				dataRow2["Narration"] = dataRow["Narration"];
				dataRow2["STJournalID"] = dataRow2["VoucherID"];
				dataRow2["STJMonth"] = month;
				dataRow2["STJYear"] = year;
				dataRow2["Note"] = dataRow["Note"];
				dataRow2.EndEdit();
				gLData.JournalTable.Rows.Add(dataRow2);
				gLData.JournalDetailsTable.Rows.Clear();
				foreach (DataRow row in standingJournalVoucherByID.StandingJournalDetailsTable.Rows)
				{
					DataRow dataRow4 = gLData.JournalDetailsTable.NewRow();
					dataRow4.BeginEdit();
					dataRow4["JournalID"] = 0;
					dataRow4["PayeeType"] = row["PayeeType"];
					dataRow4["AccountID"] = row["AccountID"];
					dataRow4["PayeeID"] = row["PayeeID"];
					string a = "";
					if (row["CurrencyID"] != DBNull.Value)
					{
						a = row["CurrencyID"].ToString();
					}
					if (a == "" || a == baseCurrencyID)
					{
						dataRow4["Debit"] = row["Debit"];
						dataRow4["Credit"] = row["Credit"];
						dataRow4["DebitFC"] = DBNull.Value;
						dataRow4["CreditFC"] = DBNull.Value;
					}
					else
					{
						dataRow4["DebitFC"] = row["Debit"];
						dataRow4["CreditFC"] = row["Credit"];
					}
					dataRow4["Description"] = row["Description"];
					dataRow4["Reference"] = row["Reference"];
					dataRow4["CostCenterID"] = row["CostCenterID"];
					dataRow4["AnalysisID"] = row["AnalysisID"];
					dataRow4["RowIndex"] = row["RowIndex"];
					dataRow4.EndEdit();
					gLData.JournalDetailsTable.Rows.Add(dataRow4);
				}
				return flag & new Journal(base.DBConfig).InsertUpdateJournalVoucher(gLData, isUpdate: false);
			}
			catch
			{
				flag = false;
				throw;
			}
		}

		public GLData GetJournalVoucherToPrint(int year, int month)
		{
			try
			{
				GLData gLData = new GLData();
				string cmdText = "SELECT JournalID, JournalDate, JOURNAL.SysDocID, VoucherID, JOURNAL.SysDocType, Reference, Reference2, ISNULL(CurrencyRate, 1),Narration,Note,StoreID,IsVoid,SD.DocName AS [Form Name],\r\n                                ISNULL(CurrencyID,(SELECT CurrencyID FROM Currency WHERE IsBase='True')) AS CurrencyID FROM JOURNAL \r\n                                LEFT JOIN System_Document SD On SD.SysDocID=JOURNAL.SysDocID\r\n                               WHERE STJYear = " + year + " AND STJMonth = " + month;
				SqlCommand sqlCommand = new SqlCommand(cmdText);
				FillDataSet(gLData, "Journal", sqlCommand);
				if (gLData == null || gLData.Tables.Count == 0 || gLData.Tables["Journal"].Rows.Count == 0)
				{
					return null;
				}
				cmdText = "SELECT JD.*,\r\n                        (CASE PayeeType\r\n                        WHEN 'C' THEN Customer.CustomerName\r\n                        WHEN 'V' THEN Vendor.VendorName\r\n                        WHEN 'E' THEN Employee.FirstName\r\n                        ELSE Account.AccountName END) AS AccountName,Account.Alias\r\n                        FROM JOURNAL_DETAILS JD \r\n                        INNER JOIN JOURNAL J ON J.SysDocID=JD.SysDocID AND J.VoucherID=JD.VoucherID\r\n                        LEFt OUTER JOIN \r\n                        Account ON JD.AccountID=Account.AccountID LEFt OUTER JOIN\r\n                        Customer ON JD.PayeeID=Customer.CustomerID LEFt OUTER JOIN\r\n                        Vendor ON JD.PayeeID=Vendor.VendorID LEFt OUTER JOIN\r\n                        Employee ON JD.PayeeID=Employee.EmployeeID  \r\n                        \r\n                        WHERE STJYear = " + year + " AND STJMonth = " + month;
				FillDataSet(gLData, "Journal_Details", cmdText);
				gLData.Relations.Add("JournalEntry", new DataColumn[2]
				{
					gLData.Tables["Journal"].Columns["SysDocID"],
					gLData.Tables["Journal"].Columns["VoucherID"]
				}, new DataColumn[2]
				{
					gLData.Tables["Journal_Details"].Columns["SysDocID"],
					gLData.Tables["Journal_Details"].Columns["VoucherID"]
				}, createConstraints: false);
				return gLData;
			}
			catch
			{
				throw;
			}
		}
	}
}
