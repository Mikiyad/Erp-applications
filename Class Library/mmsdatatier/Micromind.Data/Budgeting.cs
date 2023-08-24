using Micromind.Common;
using Micromind.Common.Data;
using Micromind.Common.Libraries;
using System;
using System.Data;
using System.Data.SqlClient;

namespace Micromind.Data
{
	public sealed class Budgeting : StoreObject
	{
		private const string SYSDOCID_PARM = "@SysDocID";

		private const string VOUCHERID_PARM = "@VoucherID";

		private const string CURRENCYID_PARM = "@CurrencyID";

		private const string CURRENCYRATE_PARM = "@CurrencyRate";

		private const string REFERENCE_PARM = "@Reference";

		private const string REFERENCE2_PARM = "@Reference2";

		private const string TRANSACTIONDATE_PARM = "@TransactionDate";

		private const string DATEFROM_PARM = "@DateFrom";

		private const string DATETO_PARM = "@DateTo";

		private const string BUDGETTYPE_PARM = "@BudgetType";

		public const string CREATEDBY_PARM = "@CreatedBy";

		public const string DATECREATED_PARM = "@DateCreated";

		public const string UPDATEDBY_PARM = "@UpdatedBy";

		public const string DATEUPDATED_PARM = "@DateUpdated";

		private const string GROUPID_PARM = "@GroupID";

		private const string ACCOUNTID_PARM = "@AccountID";

		private const string DESCRIPTION_PARM = "@Description";

		private const string ANALYSISID_PARM = "@AnalysisID";

		private const string PAYEEID_PARM = "@PayeeID";

		private const string PAYEETYPE_PARM = "@PayeeType";

		private const string COSTCENTERID_PARM = "@CostCenterID";

		private const string JOBID_PARM = "@JobID";

		private const string CREDIT_PARM = "@Credit";

		private const string DEBIT_PARM = "@Debit";

		private const string ROWINDEX_PARM = "@RowIndex";

		public Budgeting(Config config)
			: base(config)
		{
		}

		private string GetInsertUpdateBudgetingText(bool isUpdate)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddInsertUpdateParameters("Budget", new FieldValue("SysDocID", "@SysDocID", isUpdateConditionField: true), new FieldValue("VoucherID", "@VoucherID", isUpdateConditionField: true), new FieldValue("CurrencyID", "@CurrencyID"), new FieldValue("CurrencyRate", "@CurrencyRate"), new FieldValue("Reference", "@Reference"), new FieldValue("Reference2", "@Reference2"), new FieldValue("TransactionDate", "@TransactionDate"), new FieldValue("DateFrom", "@DateFrom"), new FieldValue("DateTo", "@DateTo"), new FieldValue("BudgetType", "@BudgetType"));
			if (isUpdate)
			{
				sqlBuilder.AddInsertUpdateParameters("Budget", new FieldValue("DateUpdated", "@DateUpdated", isUpdateConditionField: true, checkForNullValue: true));
			}
			if (isUpdate)
			{
				return sqlBuilder.GetUpdateExpression();
			}
			return sqlBuilder.GetInsertExpression();
		}

		private SqlCommand GetInsertUpdateBudgetingCommand(bool isUpdate)
		{
			SqlParameterCollection parameters;
			if (isUpdate)
			{
				if (updateCommand != null)
				{
					updateCommand = null;
				}
				updateCommand = new SqlCommand(GetInsertUpdateBudgetingText(isUpdate: true), base.DBConfig.Connection);
				updateCommand.CommandType = CommandType.Text;
				parameters = updateCommand.Parameters;
			}
			else
			{
				if (insertCommand != null)
				{
					insertCommand = null;
				}
				insertCommand = new SqlCommand(GetInsertUpdateBudgetingText(isUpdate: false), base.DBConfig.Connection);
				insertCommand.CommandType = CommandType.Text;
				parameters = insertCommand.Parameters;
			}
			parameters.Add("@SysDocID", SqlDbType.NVarChar);
			parameters.Add("@VoucherID", SqlDbType.NVarChar);
			parameters.Add("@CurrencyID", SqlDbType.NVarChar);
			parameters.Add("@CurrencyRate", SqlDbType.Decimal);
			parameters.Add("@Reference", SqlDbType.NVarChar);
			parameters.Add("@Reference2", SqlDbType.NVarChar);
			parameters.Add("@TransactionDate", SqlDbType.DateTime);
			parameters.Add("@DateFrom", SqlDbType.DateTime);
			parameters.Add("@DateTo", SqlDbType.DateTime);
			parameters.Add("@BudgetType", SqlDbType.NVarChar);
			parameters["@SysDocID"].SourceColumn = "SysDocID";
			parameters["@VoucherID"].SourceColumn = "VoucherID";
			parameters["@CurrencyID"].SourceColumn = "CurrencyID";
			parameters["@CurrencyRate"].SourceColumn = "CurrencyRate";
			parameters["@Reference"].SourceColumn = "Reference";
			parameters["@Reference2"].SourceColumn = "Reference2";
			parameters["@TransactionDate"].SourceColumn = "TransactionDate";
			parameters["@DateFrom"].SourceColumn = "DateFrom";
			parameters["@DateTo"].SourceColumn = "DateTo";
			parameters["@BudgetType"].SourceColumn = "BudgetType";
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

		private string GetInsertUpdateBudgetingDetailsText(bool isUpdate)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddInsertUpdateParameters("Budget_Details", new FieldValue("SysDocID", "@SysDocID"), new FieldValue("VoucherID", "@VoucherID"), new FieldValue("GroupID", "@GroupID"), new FieldValue("AccountID", "@AccountID"), new FieldValue("JobID", "@JobID"), new FieldValue("Description", "@Description"), new FieldValue("PayeeID", "@PayeeID"), new FieldValue("PayeeType", "@PayeeType"), new FieldValue("AnalysisID", "@AnalysisID"), new FieldValue("CostCenterID", "@CostCenterID"), new FieldValue("RowIndex", "@RowIndex"), new FieldValue("Credit", "@Credit"), new FieldValue("Debit", "@Debit"));
			if (isUpdate)
			{
				return sqlBuilder.GetUpdateExpression();
			}
			return sqlBuilder.GetInsertExpression();
		}

		private SqlCommand GetInsertUpdateBudgetingDetailsCommand(bool isUpdate)
		{
			updateCommand = null;
			insertCommand = null;
			SqlParameterCollection parameters;
			if (isUpdate)
			{
				updateCommand = new SqlCommand(GetInsertUpdateBudgetingDetailsText(isUpdate: true), base.DBConfig.Connection);
				updateCommand.CommandType = CommandType.Text;
				parameters = updateCommand.Parameters;
			}
			else
			{
				insertCommand = new SqlCommand(GetInsertUpdateBudgetingDetailsText(isUpdate: false), base.DBConfig.Connection);
				insertCommand.CommandType = CommandType.Text;
				parameters = insertCommand.Parameters;
			}
			parameters.Add("@SysDocID", SqlDbType.NVarChar);
			parameters.Add("@VoucherID", SqlDbType.NVarChar);
			parameters.Add("@GroupID", SqlDbType.NVarChar);
			parameters.Add("@AccountID", SqlDbType.NVarChar);
			parameters.Add("@JobID", SqlDbType.NVarChar);
			parameters.Add("@Description", SqlDbType.NVarChar);
			parameters.Add("@PayeeID", SqlDbType.NVarChar);
			parameters.Add("@PayeeType", SqlDbType.NVarChar);
			parameters.Add("@AnalysisID", SqlDbType.NVarChar);
			parameters.Add("@CostCenterID", SqlDbType.NVarChar);
			parameters.Add("@RowIndex", SqlDbType.Int);
			parameters.Add("@Credit", SqlDbType.Decimal);
			parameters.Add("@Debit", SqlDbType.Decimal);
			parameters["@SysDocID"].SourceColumn = "SysDocID";
			parameters["@VoucherID"].SourceColumn = "VoucherID";
			parameters["@GroupID"].SourceColumn = "GroupID";
			parameters["@AccountID"].SourceColumn = "AccountID";
			parameters["@JobID"].SourceColumn = "JobID";
			parameters["@Description"].SourceColumn = "Description";
			parameters["@PayeeID"].SourceColumn = "PayeeID";
			parameters["@PayeeType"].SourceColumn = "PayeeType";
			parameters["@AnalysisID"].SourceColumn = "AnalysisID";
			parameters["@CostCenterID"].SourceColumn = "CostCenterID";
			parameters["@Credit"].SourceColumn = "Credit";
			parameters["@Debit"].SourceColumn = "Debit";
			parameters["@RowIndex"].SourceColumn = "RowIndex";
			if (isUpdate)
			{
				return updateCommand;
			}
			return insertCommand;
		}

		internal int GetJournalID(string sysDocID, string voucherID, SqlTransaction sqlTransaction)
		{
			string exp = "SELECT JournalID FROM Journal WHERE SysDocID='" + sysDocID + "' AND VoucherID='" + voucherID + "'";
			object obj = ExecuteScalar(exp, sqlTransaction);
			if (obj != null && obj.ToString() != "")
			{
				return int.Parse(obj.ToString());
			}
			return -1;
		}

		public bool VoidJournal(string sysDocID, string voucherID, bool isVoid, SqlTransaction sqlTransaction)
		{
			bool flag = true;
			try
			{
				string exp = "SELECT JournalDate FROM Journal WHERE SysDocID = '" + sysDocID + "' AND VoucherID = '" + voucherID + "'";
				object obj = ExecuteScalar(exp, sqlTransaction);
				DateTime t = DateTime.MaxValue;
				if (obj != null && obj.ToString() != "")
				{
					t = DateTime.Parse(obj.ToString());
				}
				obj = new CompanyInformations(base.DBConfig).GetLastClosingDate();
				if (obj != null)
				{
					DateTime t2 = DateTime.Parse(obj.ToString());
					if (t <= t2)
					{
						throw new CompanyException("Cannot delete a transaction in a closed period.", 1038);
					}
				}
				string text = GetJournalID(sysDocID, voucherID, sqlTransaction).ToString();
				if (text == "-1")
				{
					return false;
				}
				exp = "UPDATE JOURNAL_Details SET IsVoid='" + isVoid.ToString() + "'  WHERE JournalID=" + text;
				flag &= (ExecuteNonQuery(exp, sqlTransaction) >= 0);
				if (flag)
				{
					exp = "UPDATE JOURNAL SET IsVoid = '" + isVoid.ToString() + "' WHERE JournalID=" + text;
					flag &= (ExecuteNonQuery(exp, sqlTransaction) > 0);
				}
				flag &= new ARJournal(base.DBConfig).VoidARJournal(sysDocID, voucherID, isVoid, sqlTransaction);
				flag &= new APJournal(base.DBConfig).VoidAPJournal(sysDocID, voucherID, isVoid, sqlTransaction);
				return flag & new EmployeeJournal(base.DBConfig).VoidEmployeeJournal(sysDocID, voucherID, isVoid, sqlTransaction);
			}
			catch
			{
				throw;
			}
		}

		public bool DeleteBudgeting(string sysDocID, string voucherID)
		{
			bool flag = true;
			try
			{
				string text = "";
				SqlTransaction sqlTransaction = base.DBConfig.StartNewTransaction();
				flag &= DeleteBudgetDetailsRows(sysDocID, voucherID, sqlTransaction);
				text = "DELETE FROM Budget WHERE SysDocID = '" + sysDocID + "' AND VoucherID = '" + voucherID + "'";
				flag &= (ExecuteNonQuery(text, sqlTransaction) >= 0);
				if (!flag)
				{
					return flag;
				}
				ActivityTypes activityType = ActivityTypes.Delete;
				AddActivityLog("Budget", voucherID, activityType, sqlTransaction);
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

		internal bool DeleteBudgetDetailsRows(string sysDocID, string voucherID, SqlTransaction sqlTransaction)
		{
			bool flag = true;
			try
			{
				string commandText = "DELETE FROM Budget_Details WHERE SysDocID = '" + sysDocID + "' AND VoucherID = '" + voucherID + "'";
				return flag & Delete(commandText, sqlTransaction);
			}
			catch
			{
				throw;
			}
		}

		public bool InsertUpdateBudgeting(BudgetingData budgetingData, bool isUpdate)
		{
			bool flag = true;
			SqlCommand insertUpdateBudgetingCommand = GetInsertUpdateBudgetingCommand(isUpdate);
			try
			{
				DataRow dataRow = budgetingData.BudgetTable.Rows[0];
				_ = new CompanyInformations(base.DBConfig).CurrencyDecimalPoints;
				SqlTransaction sqlTransaction = base.DBConfig.StartNewTransaction();
				string text = dataRow["VoucherID"].ToString();
				string sysDocID = dataRow["SysDocID"].ToString();
				dataRow["BudgetType"].ToString();
				if (!isUpdate && new SystemDocuments(base.DBConfig).ExistDocumentNumber("Budget", "VoucherID", dataRow["SysDocID"].ToString(), text, sqlTransaction))
				{
					throw new CompanyException("Document number already exist.", 1046);
				}
				foreach (DataRow row in budgetingData.BudgetDetailsTable.Rows)
				{
					row["SysDocID"] = dataRow["SysDocID"];
					row["VoucherID"] = dataRow["VoucherID"];
				}
				insertUpdateBudgetingCommand.Transaction = sqlTransaction;
				flag = (isUpdate ? (flag & Update(budgetingData, "Budget", insertUpdateBudgetingCommand)) : (flag & Insert(budgetingData, "Budget", insertUpdateBudgetingCommand)));
				insertUpdateBudgetingCommand = GetInsertUpdateBudgetingDetailsCommand(isUpdate: false);
				insertUpdateBudgetingCommand.Transaction = sqlTransaction;
				if (isUpdate)
				{
					flag &= DeleteBudgetDetailsRows(sysDocID, text, sqlTransaction);
				}
				if (budgetingData.Tables["Budget_Details"].Rows.Count > 0)
				{
					flag &= Insert(budgetingData, "Budget_Details", insertUpdateBudgetingCommand);
				}
				if (!flag)
				{
					throw new Exception("Failed to save the transaction because of an unexpected error. Please try again.");
				}
				flag &= UpdateTableRowInsertUpdateInfo("Budget", "SysDocID", dataRow["SysDocID"].ToString(), "VoucherID", dataRow["VoucherID"].ToString(), sqlTransaction, !isUpdate);
				string entityName = "Budget";
				flag = (isUpdate ? (flag & AddActivityLog(entityName, text, sysDocID, ActivityTypes.Update, sqlTransaction)) : (flag & AddActivityLog(entityName, text, sysDocID, ActivityTypes.Add, sqlTransaction)));
				if (isUpdate)
				{
					return flag;
				}
				flag &= new SystemDocuments(base.DBConfig).UpdateNextDocumentNumber(dataRow["SysDocID"].ToString(), dataRow["VoucherID"].ToString(), "Budget", "VoucherID", sqlTransaction);
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

		public bool VoidBudgeting(string sysDocID, string voucherID, bool isVoid)
		{
			bool flag = true;
			try
			{
				SqlTransaction sqlTransaction = base.DBConfig.StartNewTransaction();
				flag &= VoidJournal(sysDocID, voucherID, isVoid, sqlTransaction);
				if (!flag)
				{
					return flag;
				}
				if (!isVoid)
				{
					AddActivityLog("Budget", voucherID, sysDocID, ActivityTypes.Unvoid, sqlTransaction);
					return flag;
				}
				AddActivityLog("Budget", voucherID, sysDocID, ActivityTypes.Void, sqlTransaction);
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

		public BudgetingData GetJournalVoucherByID(string sysDocID, string voucherID)
		{
			try
			{
				BudgetingData budgetingData = new BudgetingData();
				string cmdText = "SELECT * FROM Budget WHERE VoucherID='" + voucherID + "' AND SysDocID='" + sysDocID + "'";
				SqlCommand sqlCommand = new SqlCommand(cmdText);
				FillDataSet(budgetingData, "Budget", sqlCommand);
				if (budgetingData == null || budgetingData.Tables.Count == 0 || budgetingData.Tables["Budget"].Rows.Count == 0)
				{
					return null;
				}
				cmdText = "SELECT BD.*,\r\n\t\t\t\t\t\t(CASE PayeeType\r\n\t\t\t\t\t\tWHEN 'C' THEN Customer.CustomerName\r\n\t\t\t\t\t\tWHEN 'V' THEN Vendor.VendorName\r\n\t\t\t\t\t\tWHEN 'E' THEN Employee.FirstName\r\n\t\t\t\t\t\tELSE Account.AccountName END) AS AccountName\r\n\t\t\t\t\t\tFROM Budget_Details BD LEFt OUTER JOIN \r\n\t\t\t\t\t\tAccount ON BD.AccountID=Account.AccountID LEFt OUTER JOIN\r\n\t\t\t\t\t\tCustomer ON BD.PayeeID=Customer.CustomerID LEFt OUTER JOIN\r\n\t\t\t\t\t\tVendor ON BD.PayeeID=Vendor.VendorID LEFt OUTER JOIN\r\n\t\t\t\t\t\tEmployee ON BD.PayeeID=Employee.EmployeeID \r\n                        WHERE VoucherID='" + voucherID + "' AND SysDocID='" + sysDocID + "'";
				FillDataSet(budgetingData, "Budget_Details", cmdText);
				return budgetingData;
			}
			catch
			{
				throw;
			}
		}

		public DataSet GetBudgetingToPrint(string sysDocID, string[] voucherID)
		{
			try
			{
				string text = "";
				for (int i = 0; i < voucherID.Length; i++)
				{
					text = "'" + voucherID[i] + "'";
					if (i < voucherID.Length - 1)
					{
						text += ",";
					}
				}
				DataSet dataSet = new DataSet();
				string textCommand = " SELECT  BDT.* FROM Budget BDT  WHERE VoucherID IN (" + text + ") AND SysDocID='" + sysDocID + "'";
				FillDataSet(dataSet, "Budget", textCommand);
				if (dataSet == null || dataSet.Tables.Count == 0 || dataSet.Tables["Budget"].Rows.Count == 0)
				{
					return null;
				}
				textCommand = "\tSELECT *\r\n                           FROM Budget_Details WHERE VoucherID IN (" + text + ") AND SysDocID='" + sysDocID + "' ";
				FillDataSet(dataSet, "Budget_Details", textCommand);
				dataSet.Relations.Add("Budgeting_Details", new DataColumn[2]
				{
					dataSet.Tables["Budget"].Columns["SysDocID"],
					dataSet.Tables["Budget"].Columns["VoucherID"]
				}, new DataColumn[2]
				{
					dataSet.Tables["Budget_Details"].Columns["SysDocID"],
					dataSet.Tables["Budget_Details"].Columns["VoucherID"]
				}, createConstraints: false);
				return dataSet;
			}
			catch
			{
				throw;
			}
		}

		public DataSet GetJournalReport(DateTime from, DateTime to, string fromLocationID, string toLocationID, bool isVoid)
		{
			string text = CommonLib.ToSqlDateTimeString(from);
			string text2 = CommonLib.ToSqlDateTimeString(to);
			string text3 = "SELECT JournalID ,JournalDate AS [Journal Date],Journal.SysDocType,VoucherID [Number],CurrencyID,CurrencyRate,IsVoid FROM Journal";
			if (fromLocationID != "")
			{
				text3 += " LEFT JOIN System_Document SD ON SD.SysDocID=Journal.SysDocID";
			}
			text3 = text3 + " WHERE JournalDate BETWEEN '" + text + "' AND '" + text2 + "'";
			if (!isVoid)
			{
				text3 += " AND (IsVoid IS NULL OR IsVoid='False')";
			}
			if (fromLocationID != "")
			{
				text3 = text3 + " AND SD.LocationID >='" + fromLocationID + "' AND SD.LocationID <='" + toLocationID + "'";
			}
			text3 += " ORDER BY CONVERT(DATE, JournalDate, 103), JournalID, VoucherID ";
			DataSet dataSet = new DataSet();
			FillDataSet(dataSet, "Budget", text3);
			DataSet dataSet2 = new DataSet();
			text3 = "SELECT JD.JournalID,\r\n                    JD.AccountID + '-' + Acc.AccountName AS [Account],JD.Reference,Description,Acc.Alias,\r\n                    PayeeID + '-' + (CASE PayeeType WHEN 'C' THEN Customer.CustomerName WHEN 'V' THEN Vendor.VendorName WHEN 'E' THEN Employee.FirstName + ' ' + Employee.LastName END) AS Payee\r\n                    ,AnalysisID [Analysis],Debit,Credit, DebitFC,CreditFC,ISNULL(JD.CurrencyID,J.CurrencyID) AS CurrencyID, ISNULL(JD.CurrencyRate,J.CurrencyRate) AS CurrencyRate ,CASE JD.IsVoid WHEN 1 THEN 'Yes' END AS Void\r\n                    FROM JOURNAL_DETAILS JD INNER JOIN Journal J ON J.SysDocID=JD.SysDocID AND J.VoucherID = JD.VoucherID INNER JOIN\r\n                    Account Acc ON JD.AccountID=Acc.AccountID\r\n                    LEFt OUTER JOIN Customer ON JD.PayeeID=Customer.CustomerID LEFt OUTER JOIN\r\n                    Vendor ON JD.PayeeID=Vendor.VendorID LEFT OUTER JOIN Employee ON Employee.EmployeeID=JD.PayeeID";
			if (fromLocationID != "")
			{
				text3 += " LEFT JOIN System_Document SD ON SD.SysDocID=JD.SysDocID";
			}
			text3 = text3 + " WHERE J.JournalDate BETWEEN '" + text + "' AND '" + text2 + "'";
			if (!isVoid)
			{
				text3 += " AND (JD.IsVoid IS NULL OR JD.IsVoid='False')";
			}
			if (fromLocationID != "")
			{
				text3 = text3 + " AND SD.LocationID >='" + fromLocationID + "' AND SD.LocationID <='" + toLocationID + "'";
			}
			text3 += " ORDER BY CONVERT(DATE, JournalDate, 103), JD.JournalID, JD.VoucherID ";
			FillDataSet(dataSet2, "Budget_Details", text3);
			dataSet.Merge(dataSet2);
			dataSet.Relations.Add("Journal Details", dataSet.Tables["Budget"].Columns["JournalID"], dataSet.Tables["Budget_Details"].Columns["JournalID"], createConstraints: false);
			return dataSet;
		}

		public DataSet GetGLReport(DateTime from, DateTime to, string fromLocationID, string toLocationID)
		{
			string text = CommonLib.ToSqlDateTimeString(from);
			string text2 = CommonLib.ToSqlDateTimeString(to);
			string text3 = "SELECT JD.AccountID AS [Account Code],AccountName AS [Account Name],\r\n                            ISNULL((SELECT SUM(ISNULL(Debit,0)- ISNULL(Credit,0)) FROM Journal_Details JD2 INNER JOIN \r\n                            Journal J2 ON J2.JournalID=JD2.JournalID\r\n                            WHERE JD.AccountID=JD2.AccountID AND J2.JournalDate<'" + text + "' AND ISNULL(J2.IsVoid,'False')='False'";
			if (fromLocationID != "")
			{
				text3 = text3 + " AND SD.LocationID >='" + fromLocationID + "' AND SD.LocationID <='" + toLocationID + "'";
			}
			text3 += " ),0)AS [Opening Balance],\r\n \r\n                        ISNULL((SELECT SUM(ISNULL(Debit,0)- ISNULL(Credit,0)) FROM Journal_Details JD2 INNER JOIN \r\n                        Journal J2 ON J2.JournalID=JD2.JournalID";
			if (fromLocationID != "")
			{
				text3 += " LEFT JOIN System_Document SD ON SD.SysDocID=JD2.SysDocID";
			}
			text3 = text3 + " WHERE JD.AccountID=JD2.AccountID AND J2.JournalDate<='" + text2 + "' AND ISNULL(J2.IsVoid,'False')='False'";
			if (fromLocationID != "")
			{
				text3 = text3 + " AND SD.LocationID >='" + fromLocationID + "' AND SD.LocationID <='" + toLocationID + "'";
			}
			text3 += "),0)\r\n                        AS [Ending Balance]\r\n                        FROM Journal_Details JD INNER JOIN Journal J ON J.SysDocID=JD.SysDocID AND J.VoucherID = JD.VoucherID INNER JOIN Account ON Account.AccountID= JD.AccountID   LEFT JOIN System_Document SD  ON  SD.SysDocID=JD.SysDocID WHERE 1=1";
			if (fromLocationID != "")
			{
				text3 = text3 + " AND SD.LocationID >='" + fromLocationID + "' AND SD.LocationID <='" + toLocationID + "'";
			}
			text3 += " GROUP BY JD.AccountID,AccountName, SD.LocationID";
			DataSet dataSet = new DataSet();
			FillDataSet(dataSet, "GL", text3);
			DataSet dataSet2 = new DataSet();
			text3 = " SELECT JournalDate AS [Journal Date],J.VoucherID AS [Number],J.SysDocType, J.CurrencyID AS [Currency],\r\n                    JD.AccountID [Account Code],JD.Reference,Description,\r\n                    PayeeID + '-' + (CASE PayeeType WHEN 'C' THEN Customer.CustomerName WHEN 'V' THEN Vendor.VendorName WHEN 'E' THEN Employee.FirstName + ' ' + Employee.LastName END) AS Payee\r\n                    ,AnalysisID [Analysis],J.CurrencyID,J.CurrencyRate,Debit,Credit, DebitFC,CreditFC\r\n                    FROM JOURNAL_DETAILS JD INNER JOIN Journal J ON J.SysDocID=JD.SysDocID AND J.VoucherID = JD.VoucherID INNER JOIN\r\n                    Account Acc ON JD.AccountID=Acc.AccountID \r\n                    LEFt OUTER JOIN Customer ON JD.PayeeID=Customer.CustomerID LEFt OUTER JOIN\r\n                    Vendor ON JD.PayeeID=Vendor.VendorID LEFT OUTER JOIN Employee ON Employee.EmployeeID=JD.PayeeID";
			if (fromLocationID != "")
			{
				text3 += " LEFT JOIN System_Document SD ON SD.SysDocID=JD.SysDocID";
			}
			text3 = text3 + " WHERE J.JournalDate BETWEEN '" + text + "' AND '" + text2 + "'";
			text3 += " AND (JD.IsVoid IS NULL OR JD.IsVoid='False')";
			if (fromLocationID != "")
			{
				text3 = text3 + " AND SD.LocationID >='" + fromLocationID + "' AND SD.LocationID <='" + toLocationID + "'";
			}
			FillDataSet(dataSet2, "GL Details", text3);
			dataSet.Merge(dataSet2);
			dataSet.Relations.Add("GL Details", dataSet.Tables["GL"].Columns["Account Code"], dataSet.Tables["GL Details"].Columns["Account Code"], createConstraints: false);
			return dataSet;
		}

		private int GetNextJournalID(SqlTransaction sqlTransaction)
		{
			string exp = "SELECT Max(JournalID) FROM Journal";
			object obj = ExecuteScalar(exp, sqlTransaction);
			if (obj != null && obj != DBNull.Value)
			{
				return int.Parse(obj.ToString()) + 1;
			}
			return 1;
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

		public BudgetingData GetDocumentInformation(string sysDocID, string voucherID)
		{
			try
			{
				BudgetingData budgetingData = new BudgetingData();
				SqlCommand sqlCommand = new SqlCommand("SELECT * FROM JOURNAL WHERE VoucherID='" + voucherID + "' AND SysDocID='" + sysDocID + "'");
				FillDataSet(budgetingData, "Budget", sqlCommand);
				if (budgetingData == null || budgetingData.Tables.Count == 0 || budgetingData.Tables["Budget"].Rows.Count == 0)
				{
					return null;
				}
				return budgetingData;
			}
			catch
			{
				throw;
			}
		}

		public DataSet GetBudgetingList(DateTime from, DateTime to, bool showVoid)
		{
			DataSet dataSet = new DataSet();
			string text = StoreConfiguration.ToSqlDateTimeString(from);
			string text2 = StoreConfiguration.ToSqlDateTimeString(to);
			string text3 = "SELECT  DISTINCT ISNULL(J.IsVoid,'False') AS V,J.SysDocID [Doc ID],J.VoucherID [Doc Number],JournalDate [Date],J.Reference Ref,Note,\r\n                            SUM(ISNULL(Debit,0)) AS Amount\r\n                            FROM Journal J LEFT OUTER JOIN Journal_Details JD\r\n                            ON J.SysDocID=JD.SysDocID AND J.VoucherID=JD.VoucherID\r\n                            WHERE J.SysDocID='JVE001' ";
			if (from != DateTime.MinValue)
			{
				text3 = text3 + " AND JournalDate Between '" + text + "' AND '" + text2 + "'";
			}
			if (!showVoid)
			{
				text3 += " AND ISNULL(J.IsVoid,'False')='False'";
			}
			text3 += " GROUP BY J.IsVoid,JournalDate,J.SysDocID,J.VoucherID,SysDocType,J.Reference,J.CurrencyID,J.CurrencyRate,Narration,Note";
			SqlCommand sqlCommand = new SqlCommand(text3);
			FillDataSet(dataSet, "Purchase_Return", sqlCommand);
			return dataSet;
		}

		public DataSet GetList(DateTime from, DateTime to, bool showVoid)
		{
			DataSet dataSet = new DataSet();
			string text = StoreConfiguration.ToSqlDateTimeString(from);
			string text2 = StoreConfiguration.ToSqlDateTimeString(to);
			string text3 = "SELECT SysDocID,VoucherID,Reference,Reference2,TransactionDate,BudgetType,CurrencyRate,DateFrom,DateTo\r\n                                FROM Budget\r\n                                WHERE 1=1";
			if (from != DateTime.MinValue)
			{
				text3 = text3 + " AND TransactionDate Between '" + text + "' AND '" + text2 + "'";
			}
			SqlCommand sqlCommand = new SqlCommand(text3);
			FillDataSet(dataSet, "Budget", sqlCommand);
			return dataSet;
		}

		public DataSet GetJournalReference(string AccountID)
		{
			try
			{
				DataSet dataSet = new DataSet();
				string textCommand = "  SELECT DISTINCT Reference                                                              \r\n                                FROM Journal_Details WHERE ISNULL(Reference,'')<>''   AND AccountID='" + AccountID + "'\r\n                                ORDER BY Reference DESC";
				FillDataSet(dataSet, "Reference", textCommand);
				return dataSet;
			}
			catch
			{
				throw;
			}
		}
	}
}
