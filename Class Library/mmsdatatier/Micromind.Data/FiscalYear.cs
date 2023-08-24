using Micromind.Common;
using Micromind.Common.Data;
using Micromind.Common.Libraries;
using System;
using System.Data;
using System.Data.SqlClient;

namespace Micromind.Data
{
	public sealed class FiscalYear : StoreObject
	{
		private const string FISCALYEARID_PARM = "@FiscalYearID";

		private const string FISCALYEARNAME_PARM = "@FiscalYearName";

		private const string STARTDATE_PARM = "@StartDate";

		private const string ENDDATE_PARM = "@ndDate";

		private const string PERIODSCOUNT_PARM = "@PeriodsCount";

		private const string STATUS_PARM = "@Status";

		private const string CREATEDBY_PARM = "@CreatedBy";

		private const string DATECREATED_PARM = "@DateCreated";

		private const string UPDATEDBY_PARM = "@UpdatedBy";

		private const string DATEUPDATED_PARM = "@DateUpdated";

		public FiscalYear(Config config)
			: base(config)
		{
		}

		private string GetInsertUpdateText(bool isUpdate)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddInsertUpdateParameters("FiscalYear", new FieldValue("FiscalYearID", "@FiscalYearID", isUpdateConditionField: true), new FieldValue("FiscalYearName", "@FiscalYearName"), new FieldValue("StartDate", "@StartDate"), new FieldValue("EndDate", "@ndDate"), new FieldValue("Status", "@Status"), new FieldValue("PeriodsCount", "@PeriodsCount"));
			if (isUpdate)
			{
				sqlBuilder.AddInsertUpdateParameters("FiscalYear", new FieldValue("DateUpdated", "@DateUpdated", isUpdateConditionField: true, checkForNullValue: true));
			}
			if (isUpdate)
			{
				return sqlBuilder.GetUpdateExpression();
			}
			return sqlBuilder.GetInsertExpression();
		}

		private SqlCommand GetInsertUpdateCommand(bool isUpdate)
		{
			SqlParameterCollection parameters;
			if (isUpdate)
			{
				if (updateCommand != null)
				{
					return updateCommand;
				}
				updateCommand = new SqlCommand(GetInsertUpdateText(isUpdate: true), base.DBConfig.Connection);
				updateCommand.CommandType = CommandType.Text;
				parameters = updateCommand.Parameters;
			}
			else
			{
				if (insertCommand != null)
				{
					return insertCommand;
				}
				insertCommand = new SqlCommand(GetInsertUpdateText(isUpdate: false), base.DBConfig.Connection);
				insertCommand.CommandType = CommandType.Text;
				parameters = insertCommand.Parameters;
			}
			parameters.Add("@FiscalYearID", SqlDbType.NVarChar);
			parameters.Add("@FiscalYearName", SqlDbType.NVarChar);
			parameters.Add("@StartDate", SqlDbType.DateTime);
			parameters.Add("@ndDate", SqlDbType.DateTime);
			parameters.Add("@PeriodsCount", SqlDbType.TinyInt);
			parameters.Add("@Status", SqlDbType.TinyInt);
			parameters["@FiscalYearID"].SourceColumn = "FiscalYearID";
			parameters["@FiscalYearName"].SourceColumn = "FiscalYearName";
			parameters["@StartDate"].SourceColumn = "StartDate";
			parameters["@ndDate"].SourceColumn = "EndDate";
			parameters["@PeriodsCount"].SourceColumn = "PeriodsCount";
			parameters["@Status"].SourceColumn = "Status";
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

		public bool InsertFiscalYear(FiscalYearData accountFiscalYearData)
		{
			bool result = true;
			SqlCommand insertUpdateCommand = GetInsertUpdateCommand(isUpdate: false);
			SqlTransaction sqlTransaction = null;
			try
			{
				sqlTransaction = (insertUpdateCommand.Transaction = base.DBConfig.StartNewTransaction());
				result = Insert(accountFiscalYearData, "FiscalYear", insertUpdateCommand);
				string text = accountFiscalYearData.FiscalYearTable.Rows[0]["FiscalYearID"].ToString();
				AddActivityLog("Fiscal Year", text, ActivityTypes.Add, sqlTransaction);
				UpdateTableRowInsertUpdateInfo("FiscalYear", "FiscalYearID", text, sqlTransaction, isInsert: true);
				return result;
			}
			catch
			{
				result = false;
				throw;
			}
			finally
			{
				base.DBConfig.EndTransaction(result);
			}
		}

		public bool UpdateFiscalYear(FiscalYearData accountFiscalYearData)
		{
			bool flag = true;
			SqlCommand insertUpdateCommand = GetInsertUpdateCommand(isUpdate: true);
			SqlTransaction sqlTransaction = null;
			try
			{
				sqlTransaction = (insertUpdateCommand.Transaction = base.DBConfig.StartNewTransaction());
				flag = Update(accountFiscalYearData, "FiscalYear", insertUpdateCommand);
				if (!flag)
				{
					return flag;
				}
				object obj = accountFiscalYearData.FiscalYearTable.Rows[0]["FiscalYearID"];
				UpdateTableRowByID("FiscalYear", "FiscalYearID", "DateUpdated", obj, DateTime.Now, sqlTransaction);
				string entiyID = accountFiscalYearData.FiscalYearTable.Rows[0]["FiscalYearName"].ToString();
				AddActivityLog("Fiscal Year", entiyID, ActivityTypes.Update, sqlTransaction);
				UpdateTableRowInsertUpdateInfo("FiscalYear", "FiscalYearID", obj, sqlTransaction, isInsert: false);
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

		public FiscalYearData GetFiscalYear()
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddTable("FiscalYear");
			FiscalYearData fiscalYearData = new FiscalYearData();
			sqlBuilder.UseDistinct = false;
			FillDataSet(fiscalYearData, "FiscalYear", sqlBuilder);
			return fiscalYearData;
		}

		public bool DeleteFiscalYear(string fiscalYearID)
		{
			bool flag = true;
			try
			{
				object fieldValue = new Databases(base.DBConfig).GetFieldValue("FiscalYear", "Status", "FiscalYearID", fiscalYearID, null);
				if (fieldValue != null && fieldValue.ToString() != "")
				{
					bool result = false;
					bool.TryParse(fieldValue.ToString(), out result);
					if (result)
					{
						throw new CompanyException("This year is closed and cannot be deleted.");
					}
				}
				string commandText = "DELETE FROM FiscalYear WHERE FiscalYearID = '" + fiscalYearID + "'";
				flag = Delete(commandText, null);
				if (!flag)
				{
					return flag;
				}
				AddActivityLog("Fiscal Year", fiscalYearID, ActivityTypes.Delete, null);
				return flag;
			}
			catch
			{
				throw;
			}
		}

		public bool ReopenFiscalYear(string fiscalYearID)
		{
			bool flag = true;
			try
			{
				object fieldValue = new Databases(base.DBConfig).GetFieldValue("FiscalYear", "Status", "FiscalYearID", fiscalYearID, null);
				if (fieldValue != null && fieldValue.ToString() != "")
				{
					bool result = false;
					bool.TryParse(fieldValue.ToString(), out result);
					if (result)
					{
						throw new CompanyException("This year is not closed.");
					}
				}
				string exp = "Select COUNT(*) FROM FiscalYear\r\n                                WHERE StartDate > (SELECT EndDate FROM FiscalYear WHERE FiscalYearID = 'Y2012') AND  Status = 2";
				fieldValue = ExecuteScalar(exp);
				if (fieldValue != null && fieldValue.ToString() != "" && int.Parse(fieldValue.ToString()) > 0)
				{
					throw new CompanyException("There is a future year which is already closed. You must reopen all future dated years before reopening this year.");
				}
				DataSet fiscalYearByID = GetFiscalYearByID(fiscalYearID);
				if (fiscalYearByID == null || fiscalYearByID.Tables.Count == 0 || fiscalYearByID.Tables[0].Rows.Count == 0)
				{
					throw new CompanyException("Fiscal year informatin not found.");
				}
				DataRow dataRow = fiscalYearByID.Tables[0].Rows[0];
				string sysDocID = dataRow["ClosingSysDocID"].ToString();
				string voucherID = dataRow["ClosingVoucherID"].ToString();
				SqlTransaction sqlTransaction = base.DBConfig.StartNewTransaction();
				string commandText = "UPDATE FiscalYear \r\n                                    SET Status=1 WHERE FiscalYearID = '" + fiscalYearID + "'";
				flag &= Update(commandText, sqlTransaction);
				flag = new Journal(base.DBConfig).DeleteJournal(sysDocID, voucherID, sqlTransaction);
				if (!flag)
				{
					return flag;
				}
				commandText = "UPDATE FiscalYear SET ClosingSysDocID = NULL, ClosingVoucherID = NULL WHERE FiscalYearID = '" + fiscalYearID + "'";
				flag &= Update(commandText, sqlTransaction);
				return flag;
			}
			catch
			{
				throw;
			}
			finally
			{
				base.DBConfig.EndTransaction(flag);
			}
		}

		public FiscalYearData GetFiscalYearByID(string id)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			CommandHelper commandHelper = null;
			commandHelper = new CommandHelper();
			commandHelper.FieldName = "FiscalYearID";
			commandHelper.SqlFieldType = SqlDbType.NVarChar;
			commandHelper.FieldValue = id;
			commandHelper.TableName = "FiscalYear";
			sqlBuilder.AddCommandHelper(commandHelper);
			sqlBuilder.IsComparing = true;
			FiscalYearData fiscalYearData = new FiscalYearData();
			sqlBuilder.UseDistinct = false;
			FillDataSet(fiscalYearData, "FiscalYear", sqlBuilder);
			return fiscalYearData;
		}

		public DataSet GetFiscalYearByFields(params string[] columns)
		{
			return GetFiscalYearByFields(null, isInactive: true, columns);
		}

		public DataSet GetFiscalYearByFields(string[] fiscalYearID, params string[] columns)
		{
			return GetFiscalYearByFields(fiscalYearID, isInactive: true, columns);
		}

		public DataSet GetFiscalYearByFields(string[] ids, bool isInactive, params string[] columns)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddTable("FiscalYear");
			foreach (string text in columns)
			{
				ApplicationAssert.CheckCondition(text.IndexOf(".") >= 0, "A table name with the column name must be specified. eg. table.column.", 0);
				string tableName = text.Substring(0, text.IndexOf("."));
				string columnName = text.Substring(text.IndexOf(".") + 1);
				sqlBuilder.AddColumn(tableName, columnName);
			}
			if (ids != null && ids.Length != 0)
			{
				CommandHelper commandHelper = new CommandHelper();
				commandHelper.FieldName = "FiscalYearID";
				commandHelper.FieldValue = ids;
				commandHelper.SqlFieldType = SqlDbType.NVarChar;
				commandHelper.TableName = "FiscalYear";
				sqlBuilder.AddCommandHelper(commandHelper);
			}
			DataSet dataSet = new DataSet();
			dataSet.EnforceConstraints = false;
			sqlBuilder.UseDistinct = false;
			FillDataSet(dataSet, "FiscalYear", sqlBuilder);
			return dataSet;
		}

		public DataSet GetFiscalYearList()
		{
			DataSet dataSet = new DataSet();
			string textCommand = "SELECT FiscalYearID [FiscalYear Code],FiscalYearName [FiscalYear Name]\r\n                           FROM FiscalYear ";
			FillDataSet(dataSet, "FiscalYear", textCommand);
			return dataSet;
		}

		public DataSet GetFiscalYearComboList()
		{
			DataSet dataSet = new DataSet();
			string textCommand = "SELECT FiscalYearID [Code],FiscalYearName [Name],StartDate,EndDate,Status\r\n                           FROM FiscalYear ORDER BY FiscalYearID,FiscalYearName";
			FillDataSet(dataSet, "FiscalYear", textCommand);
			return dataSet;
		}

		public int CanCloseFiscalYear(string fiscalYearID)
		{
			string exp = "Select COUNT (* )\r\n                            FROM FiscalYear WHERE StartDate < (Select StartDate FROM FiscalYear WHERE FiscalYearID = '" + fiscalYearID + "') AND ISNULL(Status,1) <> 2";
			object obj = ExecuteScalar(exp);
			if (obj != null && obj.ToString() != "" && int.Parse(obj.ToString()) > 0)
			{
				return -1;
			}
			exp = "SELECT COUNT(*) FROM Journal WHERE Journal.JournalDate < (SELECT MIN(StartDate)FROM FiscalYear)";
			obj = ExecuteScalar(exp);
			if (obj != null && obj.ToString() != "" && int.Parse(obj.ToString()) > 0)
			{
				return -2;
			}
			return 0;
		}

		public bool CloseFiscalYear(string fiscalYearID, GLData closingData, string retainedEarningAccountID)
		{
			bool flag = true;
			try
			{
				GetFiscalYearByID(fiscalYearID);
				if (CanCloseFiscalYear(fiscalYearID) < 0)
				{
					throw new CompanyException("Cannot close this fiscal year. Some of conditions are not met.");
				}
				DataRow dataRow = closingData.JournalTable.Rows[0];
				string text = dataRow["SysDocID"].ToString();
				string text2 = dataRow["VoucherID"].ToString();
				SqlTransaction sqlTransaction = null;
				if (sqlTransaction == null)
				{
					sqlTransaction = base.DBConfig.StartNewTransaction();
				}
				GLData journalData = CreateYearEndClosingGLData(fiscalYearID, closingData, retainedEarningAccountID, sqlTransaction);
				flag &= new Journal(base.DBConfig).InsertUpdateJournal(journalData, isUpdate: false, sqlTransaction);
				flag &= CreateProductEOYSummary(text, text2, fiscalYearID, sqlTransaction);
				if (flag)
				{
					string exp = "UPDATE FiscalYear SET Status = " + (byte)2 + " , ClosingSysDocID = '" + text + "',\r\n                                ClosingVoucherID = '" + text2 + "'  WHERE FiscalYearID = '" + fiscalYearID + "'";
					flag &= (ExecuteNonQuery(exp, sqlTransaction) > 0);
				}
				flag &= new SystemDocuments(base.DBConfig).UpdateNextDocumentNumber(text, text2, "Journal", "VoucherID", sqlTransaction);
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

		private bool CreateProductEOYSummary(string sysDocID, string voucherID, string fiscalYearID, SqlTransaction sqlTransaction)
		{
			bool flag = true;
			try
			{
				DateTime date = DateTime.Parse(GetFiscalYearByID(fiscalYearID).FiscalYearTable.Rows[0]["EndDate"].ToString());
				int year = date.Year;
				string text = CommonLib.ToSqlDateTimeString(date);
				string exp = "   INSERT INTO EOY_Product (SysDocID,VoucherID,FiscalYearID,FiscalYear,ProductID,LocationID,Quantity,AvgCost,AssetValue,EndDate)\r\n                              SELECT DISTINCT  '" + sysDocID + "' AS SysDocID,'" + voucherID + "' AS VoucherID, '" + fiscalYearID + "' AS FiscalYearID," + year + " AS FiscalYear, IT.ProductID,IT.LocationID ,SUM(IT.Quantity) AS Quantity,\r\n                               AG.AverageCost AS AvgCost , AG.AverageCost * SUM(IT.Quantity)  AS AssetValue , '" + text + "' AS EndDate\r\n                            FROM Inventory_Transactions IT INNER JOIN Product P ON P.ProductID = IT.ProductID\r\n                            INNER JOIN Location Loc ON Loc.LocationID = IT.LocationID  LEFT OUTER JOIN   (SELECT T.ProductID,ISNULL(T.AverageCost,0) AS AverageCost FROM (\r\n\t\t\t\t\t\t     SELECT   ProductID, AverageCost, Row_Number() OVER (Partition by ProductID ORDER BY TransactionDate DESC,TransactionID desc) AS RN FROM Inventory_Transactions WHERE TransactionDate <= '" + text + "' ) T WHERE RN =1\r\n\t\t\t\t\t\t\t\t\t\t\t\t ) AG ON AG.ProductID = IT.ProductID\r\n                            WHERE  TransactionDate <= '" + text + "' AND ItemType IN(1, 7) AND IT.ProductID IN (SELECT ProductID FROM Product WHERE ItemType NOT IN ('3'))  \r\n\t\t\t\t\t\t\t GROUP BY IT.ProductID,IT.LocationID,AG.AverageCost   HAVING SUM(IT.Quantity)<>0  ORDER BY IT.ProductID ";
				return flag & (ExecuteNonQuery(exp, sqlTransaction) >= 0);
			}
			catch
			{
				flag = false;
				throw;
			}
		}

		public bool CreateSummaryTable(string sysDocID, string voucherID, string fiscalYearID)
		{
			bool result = true;
			try
			{
				SqlTransaction sqlTransaction = null;
				if (sqlTransaction == null)
				{
					sqlTransaction = base.DBConfig.StartNewTransaction();
				}
				return CreateProductEOYSummary(sysDocID, voucherID, fiscalYearID, sqlTransaction);
			}
			catch
			{
				result = false;
				throw;
			}
			finally
			{
				base.DBConfig.EndTransaction(result);
			}
		}

		private GLData CreateYearEndClosingGLData(string fiscalYearID, GLData closingData, string retainedEarningAccountID, SqlTransaction sqlTransaction)
		{
			try
			{
				GLData gLData = new GLData();
				if (retainedEarningAccountID == "")
				{
					throw new CompanyException("Retained Earning account is not provided.");
				}
				FiscalYearData fiscalYearByID = GetFiscalYearByID(fiscalYearID);
				DataRow dataRow = closingData.Tables[0].Rows[0];
				DateTime dateTime = DateTime.Parse(fiscalYearByID.FiscalYearTable.Rows[0]["EndDate"].ToString());
				DateTime dateTime2 = new DateTime(dateTime.Year, dateTime.Month, dateTime.Day, 23, 59, 59);
				DataRow dataRow2 = gLData.JournalTable.NewRow();
				SysDocTypes sysDocTypes = SysDocTypes.GJournal;
				dataRow2["JournalID"] = 0;
				dataRow2["JournalDate"] = dateTime2;
				dataRow2["SysDocID"] = dataRow["SysDocID"];
				dataRow2["SysDocType"] = (byte)sysDocTypes;
				dataRow2["VoucherID"] = dataRow["VoucherID"];
				dataRow2["CurrencyID"] = new Currencies(base.DBConfig).GetBaseCurrencyID();
				dataRow2["CurrencyRate"] = DBNull.Value;
				dataRow2["Reference"] = dataRow["Reference"];
				dataRow2["Narration"] = "Year End Closing for Fiscal Year: " + fiscalYearID;
				dataRow2["Note"] = dataRow["Note"];
				dataRow2.EndEdit();
				gLData.JournalTable.Rows.Add(dataRow2);
				DataSet closingIncomeExpenseList = new Journal(base.DBConfig).GetClosingIncomeExpenseList(fiscalYearID, sqlTransaction);
				decimal num = default(decimal);
				DataRow dataRow3 = null;
				foreach (DataRow row in closingIncomeExpenseList.Tables[0].Rows)
				{
					decimal num2 = default(decimal);
					string text = "";
					if (row["Debit"] != DBNull.Value)
					{
						num += decimal.Parse(row["Debit"].ToString());
						num2 += decimal.Parse(row["Debit"].ToString());
					}
					if (row["Credit"] != DBNull.Value)
					{
						num -= decimal.Parse(row["Credit"].ToString());
						num2 -= decimal.Parse(row["Credit"].ToString());
					}
					if (!(num2 == 0m))
					{
						dataRow3 = gLData.JournalDetailsTable.NewRow();
						dataRow3.BeginEdit();
						text = row["AccountID"].ToString();
						dataRow3["JournalID"] = 0;
						dataRow3["AccountID"] = text;
						if (num2 > 0m)
						{
							dataRow3["Debit"] = DBNull.Value;
							dataRow3["Credit"] = num2;
						}
						else
						{
							dataRow3["Debit"] = Math.Abs(num2);
							dataRow3["Credit"] = DBNull.Value;
						}
						dataRow3["IsBaseOnly"] = true;
						dataRow3["Description"] = "Year End Closing:" + fiscalYearID;
						dataRow3["Reference"] = dataRow["Reference"];
						dataRow3.EndEdit();
						gLData.JournalDetailsTable.Rows.Add(dataRow3);
					}
				}
				dataRow3 = gLData.JournalDetailsTable.NewRow();
				dataRow3.BeginEdit();
				dataRow3["JournalID"] = 0;
				dataRow3["AccountID"] = retainedEarningAccountID;
				if (num >= 0m)
				{
					dataRow3["Debit"] = num;
					dataRow3["Credit"] = DBNull.Value;
				}
				else
				{
					dataRow3["Debit"] = DBNull.Value;
					dataRow3["Credit"] = Math.Abs(num);
				}
				dataRow3["IsBaseOnly"] = true;
				dataRow3["Reference"] = dataRow["Reference"];
				dataRow3["Description"] = "Retained Earning for Year:" + fiscalYearID;
				dataRow3.EndEdit();
				gLData.JournalDetailsTable.Rows.Add(dataRow3);
				return gLData;
			}
			catch
			{
				throw;
			}
		}

		public int GetPendingDeliveryNotesCount(DateTime toDate)
		{
			try
			{
				string str = StoreConfiguration.ToSqlDateTimeString(toDate);
				string exp = " SELECT COUNT (*) AS C FROM (\r\n                             SELECT DISTINCT DN.VoucherID\r\n                              FROM Delivery_Note DN\r\n                              INNER JOIN Delivery_Note_Detail DND ON DND.SysDocID = DN.SysDocID AND DND.VoucherID = DN.VoucherID \r\n                              WHERE TransactionDate < '" + str + "' AND ISNULL(DN.IsVoid,'False')='False' AND ISNULL(DN.IsInvoiced,'False')='False' AND ISNULL(DN.SalesFlow,0) = 2    \r\n\t\t\t\t\t\t\t  GROUP BY DN.VoucherID\r\n\t\t\t\t\t\t\t  HAVING SUM(Quantity - ISNULL(QuantityReturned,0)) > 0  ) AS T";
				object obj = ExecuteScalar(exp);
				if (obj.IsNullOrEmpty())
				{
					return 0;
				}
				return int.Parse(obj.ToString());
			}
			catch
			{
				throw;
			}
		}

		public int GetPendingGRNCount(DateTime toDate)
		{
			try
			{
				string str = StoreConfiguration.ToSqlDateTimeString(toDate);
				string exp = "  SELECT COUNT (Number) FROM (\r\n                                 SELECT PR.SysDocID [Doc ID],PR.VoucherID [Number] FROM Purchase_Receipt PR\r\n                                INNER JOIN Purchase_Receipt_Detail PRD ON PRD.SysDocID = PR.SysDocID AND PRD.VoucherID = PR.VoucherID \r\n                              WHERE  TransactionDate < '" + str + "' AND ISNULL(IsVoid,'False')='False' AND ISNULL(IsInvoiced,'False')='False'  \r\n\t\t\t\t\t\t\t  GROUP BY PR.SysDocID  ,PR.VoucherID\r\n\t\t\t\t\t\t\t  HAVING SUM(Quantity - ISNULL(QuantityReturned,0)) > 0 ) T";
				object obj = ExecuteScalar(exp);
				if (obj.IsNullOrEmpty())
				{
					return 0;
				}
				return int.Parse(obj.ToString());
			}
			catch
			{
				throw;
			}
		}

		public int GetPendingTransferCount(DateTime toDate)
		{
			try
			{
				string str = StoreConfiguration.ToSqlDateTimeString(toDate);
				string exp = " SELECT COUNT(*) FROM (\r\n                                SELECT SysDocID,VoucherID AS Number,TransferTypeID [Type], TransactionDate [Date],LocationFromID [From],LocationToID [To],1 AS Reason,Description\r\n                               FROM         Inventory_Transfer\r\n                                WHERE ISNULL(IsAccepted,'False')='False' AND  ISNULL(IsRejected,'False') = 'False' AND ISNULL(IsVoid,'False')='False'\r\n                                UNION \r\n                                SELECT SysDocID,VoucherID AS Number,TransferTypeID [Type], TransactionDate [Date],LocationToID [From],LocationFromID [To],2 AS Reason,Description\r\n                                FROM         Inventory_Transfer\r\n                                WHERE ISNULL(IsAccepted,'False')='False' AND ISNULL(IsVoid,'False')='False' AND ISNULL(IsRejected,'False') = 'True' AND ISNULL(IsRejectAccepted,'False')='False' ) T\r\n\t\t\t\t\t\t\t\tWHERE T.Date < '" + str + "'";
				object obj = ExecuteScalar(exp);
				if (obj.IsNullOrEmpty())
				{
					return 0;
				}
				return int.Parse(obj.ToString());
			}
			catch
			{
				throw;
			}
		}

		public int GetNegativeStockCount(DateTime toDate)
		{
			try
			{
				string str = StoreConfiguration.ToSqlDateTimeString(toDate);
				string exp = " SELECT COUNT (ProductID) FROM (\r\n                                SELECT DISTINCT IT.ProductID, ISNULL((SELECT ROUND(SUM(IT2.Quantity),2)\r\n\t\t\t\t\t\t\t\t FROM Inventory_Transactions IT2  LEFT JOIN Product P ON P.ProductID=IT2.ProductID   WHERE IT.ProductID = IT2.ProductID AND P.ItemType NOT IN (2,3)  \r\n\t\t\t\t\t\t\t\t\t  AND TransactionDate < '" + str + "'),0)  AS Quantity\r\n\t\t\t\t\t\t\t\t FROM Inventory_Transactions IT) T  WHERE T.Quantity<0\r\n\t\t\t\t\t\t\t\t \r\n\t\t\t\t\t\t\t ";
				object obj = ExecuteScalar(exp);
				if (obj.IsNullOrEmpty())
				{
					return 0;
				}
				return int.Parse(obj.ToString());
			}
			catch
			{
				throw;
			}
		}

		public bool IsTBCorrect(DateTime toDate)
		{
			try
			{
				string str = StoreConfiguration.ToSqlDateTimeString(toDate);
				string exp = " SELECT sum(isnull(debit,0)) - sum(isnull(credit,0)) from Journal_Details JD INNER JOIN Journal J ON J.JournalID = JD.JournalID\r\n                                WHERE ISNULL(J.ISVoid,'False') = 'False' AND J.JournalDate < '" + str + "'";
				object obj = ExecuteScalar(exp);
				if (obj.IsNullOrEmpty())
				{
					return true;
				}
				return decimal.Parse(obj.ToString()) == 0m;
			}
			catch
			{
				throw;
			}
		}

		public int GetUnbalancedJournalsCount(DateTime toDate)
		{
			try
			{
				string str = StoreConfiguration.ToSqlDateTimeString(toDate);
				string exp = " select COUNT(VoucherID) FROM (\r\n                                    select  sum(isnull(debit,0)) - sum(isnull(credit,0)) as diff, j.journalid,j.sysdocid,j.voucherid,j.JournalDate FROM Journal J \r\n                                    INNER JOIN Journal_Details JD ON J.journalID = JD.JournalID  WHERE ISNULL(J.IsVoid,'False')='False' AND J.JournalDate < '" + str + "'\r\n                                    group by j.sysdocid,j.voucherid,j.JournalDate,j.journalid) as t \r\n                                    where ABS(t.diff) > 0.01 ";
				object obj = ExecuteScalar(exp);
				if (obj.IsNullOrEmpty())
				{
					return 0;
				}
				return int.Parse(obj.ToString());
			}
			catch
			{
				throw;
			}
		}

		public int GetUncostedItemsCount(DateTime toDate)
		{
			try
			{
				string text = StoreConfiguration.ToSqlDateTimeString(toDate);
				string str = "select  COUNT (*) from  (\r\n                                    SELECT *,  \r\n                                    ROW_NUMBER() OVER( PARTITION BY Productid ORDER BY transactiondate,TransactionID ) AS Row\r\n                                    from Inventory_Transactions WHERE TransactionDate < '" + text + "' ";
				str = str + " AND ProductID IN  ( SELECT DISTINCT IT.ProductID FROM (\r\n                                 SELECT TransactionID,SysdocID,VoucherID,TransactionType Y,ProductID,SysDocType,ISNONCostedGRN as G,TransactionDate,Quantity,UnitPrice,\r\n                                 AverageCost,AssetValue, \r\n                                ROW_NUMBER()  OVER ( Partition BY ProductID Order by TransactionDate, TransactionID ) RowNumber,\r\n                                SUM(Quantity)  OVER ( Partition BY ProductID Order by TransactionDate, TransactionID ROWS between unbounded preceding and current ROW) AS TotalQuantity,\r\n                                SUM(AssetValue)  OVER ( Partition BY ProductID Order by TransactionDate, TransactionID ROWS between unbounded preceding and current ROW ) AS TotalValue\r\n                                 FROM Inventory_Transactions IT WHERE TransactionDate < '" + text + "') AS IT INNER JOIN Product P ON P.ProductID = IT.ProductID\r\n                                 WHERE totalquantity >= 0 AND (SysDocType <> 20 OR (SysDocType = 20 AND TransactionDate > '10-1-2015')) --  --Old Stock Transfer system\r\n                                   AND IT.quantity <> 0 AND ABS(ROUND(totalvalue- TotalQuantity * ISNULL(it.AverageCost,0),2)) > ISNULL(P.IgnoreCostDiffAmount,0.1) ) ";
				str += ") s where row = 1  ";
				object obj = ExecuteScalar(str);
				if (obj.IsNullOrEmpty())
				{
					return 0;
				}
				return int.Parse(obj.ToString());
			}
			catch
			{
				throw;
			}
		}
	}
}
