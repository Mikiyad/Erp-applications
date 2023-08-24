using Micromind.Common;
using Micromind.Common.Libraries;
using System;
using System.Collections.Generic;
using System.Data;

namespace Micromind.Data
{
	public sealed class JournalReports : StoreObject
	{
		public JournalReports(Config config)
			: base(config)
		{
		}

		public DataSet GetAccountCostCenterReport(DateTime from, DateTime to, string fromAccountID, string toAccountID, string fromLocationID, string toLocationID, bool isFC, string costCenterID, string fromDivisionID, string toDivisionID)
		{
			_ = string.Empty;
			string text = CommonLib.ToSqlDateTimeString(from);
			string text2 = CommonLib.ToSqlDateTimeString(to);
			new Currencies(base.DBConfig).GetBaseCurrencyID();
			string str = " SELECT * FROM Cost_Center CC  WHERE  1=1";
			if (costCenterID != "")
			{
				str = str + " AND CostCenterID ='" + costCenterID + "' ";
			}
			str += " AND CC.Inactive<>1 OR CC.Inactive IS NULL  ORDER BY CostCenterID,CostCenterName";
			DataSet dataSet = new DataSet();
			FillDataSet(dataSet, "CC", str);
			DataSet dataSet2 = new DataSet();
			str = " SELECT Account_Group.TypeID, JournalDate AS [Journal Date],JD.CostCenterID,J.VoucherID AS [Number],J.SysDocType, J.CurrencyID AS [Currency],Acc.Alias,\r\n                    JD.AccountID [Account Code],JD.Reference,Description,\r\n                    PayeeID + '-' + (CASE PayeeType WHEN 'C' THEN Customer.CustomerName WHEN 'V' THEN Vendor.VendorName WHEN 'E' THEN Employee.FirstName + ' ' + Employee.LastName END) AS Payee\r\n                    ,AnalysisID ,J.CurrencyID,J.CurrencyRate,Debit,Credit, DebitFC,CreditFC\r\n                    FROM JOURNAL_DETAILS JD INNER JOIN Journal J ON J.SysDocID=JD.SysDocID AND J.VoucherID = JD.VoucherID INNER JOIN\r\n                    Account Acc ON JD.AccountID=Acc.AccountID\r\n                    INNER JOIN Account_Group ON Acc.GroupID = Account_Group.GroupID \r\n                    LEFT OUTER JOIN Customer ON JD.PayeeID=Customer.CustomerID LEFt OUTER JOIN\r\n                    Vendor ON JD.PayeeID=Vendor.VendorID LEFT OUTER JOIN Employee ON Employee.EmployeeID=JD.PayeeID";
			if (fromLocationID != "" || fromDivisionID != "")
			{
				str += " LEFT JOIN System_Document SD ON SD.SysDocID=JD.SysDocID";
			}
			str = str + " WHERE ISNULL(JD.CostCenterID,'') <> '' AND J.JournalDate BETWEEN '" + text + "' AND '" + text2 + "'";
			str += " AND (JD.IsVoid IS NULL OR JD.IsVoid='False') ";
			if (fromAccountID != "")
			{
				str = str + " AND JD.AccountID>='" + fromAccountID + "'";
			}
			if (toAccountID != "")
			{
				str = str + " AND JD.AccountID<='" + toAccountID + "' ";
			}
			if (costCenterID != "")
			{
				str = str + " AND JD.CostCenterID ='" + costCenterID + "' ";
			}
			if (fromLocationID != "")
			{
				str = str + " AND SD.LocationID >='" + fromLocationID + "' AND SD.LocationID <='" + toLocationID + "'";
			}
			if (fromDivisionID != "")
			{
				str = str + " AND SD.DivisionID >='" + fromDivisionID + "' AND SD.DivisionID <='" + toDivisionID + "'";
			}
			str += "  ORDER BY CONVERT(DATE, JournalDate, 103), J.VoucherID ";
			FillDataSet(dataSet2, "GL Details", str);
			dataSet.Merge(dataSet2);
			dataSet.Relations.Add("GL Details", dataSet.Tables["CC"].Columns["CostCenterID"], dataSet.Tables["GL Details"].Columns["CostCenterID"], createConstraints: false);
			return dataSet;
		}

		public DataSet GetAccountLedgerReport(DateTime from, DateTime to, string fromAccountID, string toAccountID, string groupID, string fromLocationID, string toLocationID, string fromDivisionID, string toDivisionID, bool isFC, string costCenterID, bool allowposting)
		{
			string empty = string.Empty;
			string text = CommonLib.ToSqlDateTimeString(from);
			string text2 = CommonLib.ToSqlDateTimeString(to);
			new Currencies(base.DBConfig).GetBaseCurrencyID();
			string text3 = string.Empty;
			if (groupID != "")
			{
				empty = " WITH ACCGroups \r\n                                AS\r\n                                (\r\n\t                                SELECT GroupID, GroupName, TypeID, ParentID , 0 AS L FROM Account_Group AG\r\n\t                                WHERE  GroupID = '" + groupID + "'\r\n\r\n\t\t\t\t\t\t\t\t\tUNION ALL\r\n\t                                \r\n\t\t\t\t\t\t\t\t\tSELECT AG.GroupID, AG.GroupName, AG.TypeID, AG.ParentID, L + 1 AS L\r\n                                    FROM Account_Group AG\r\n\t\t\t\t\t\t\t\t\tINNER JOIN ACCGroups AS d\r\n                                        ON AG.ParentID = d.GroupID\r\n                                )\r\n\r\n                                SELECT GroupID FROM ACCGroups ";
				DataSet dataSet = new DataSet();
				FillDataSet(dataSet, "AccountGroup", empty);
				for (int i = 0; i < dataSet.Tables["AccountGroup"].Rows.Count; i++)
				{
					DataRow dataRow = dataSet.Tables["AccountGroup"].Rows[i];
					text3 = text3 + "'" + dataRow["GroupID"].ToString() + "'";
					if (i < dataSet.Tables["AccountGroup"].Rows.Count - 1)
					{
						text3 += ",";
					}
				}
			}
			string str = "SELECT JD.AccountID AS [Account Code],AccountName AS [Account Name],Account.Alias, ";
			str = (isFC ? (str + " ISNULL((SELECT SUM(ISNULL(DebitFC,0)- ISNULL(CreditFC,0)) ") : (str + " ISNULL((SELECT SUM(ISNULL(Debit,0)- ISNULL(Credit,0)) "));
			str += " FROM Journal_Details JD2 INNER JOIN \r\n                            Journal J2 ON J2.JournalID=JD2.JournalID";
			if (fromLocationID != "")
			{
				str += " LEFT JOIN System_Document SD ON SD.SysDocID=JD2.SysDocID";
			}
			str = str + " WHERE JD.AccountID = JD2.AccountID AND J2.JournalDate < '" + text + "' AND ISNULL(J2.IsVoid, 'False')= 'False'";
			if (fromLocationID != "")
			{
				str = str + " AND SD.LocationID >='" + fromLocationID + "' AND SD.LocationID <='" + toLocationID + "'";
			}
			if (fromDivisionID != "")
			{
				str = str + " AND JD2.DivisionID >='" + fromDivisionID + "' AND JD2.DivisionID <='" + toDivisionID + "'";
			}
			str += "  ),0)AS[Opening Balance], ";
			str = (isFC ? (str + " ISNULL((SELECT SUM(ISNULL(DebitFC,0)- ISNULL(CreditFC,0)) ") : (str + " ISNULL((SELECT SUM(ISNULL(Debit,0)- ISNULL(Credit,0)) "));
			str += " FROM Journal_Details JD2 INNER JOIN \r\n                    Journal J2 ON J2.JournalID=JD2.JournalID";
			if (fromLocationID != "")
			{
				str += " LEFT JOIN System_Document SD ON SD.SysDocID=JD2.SysDocID";
			}
			str = str + "  WHERE JD.AccountID=JD2.AccountID AND \r\n                    J2.JournalDate<='" + text2 + "' AND ISNULL(J2.IsVoid,'False')='False'";
			if (fromLocationID != "")
			{
				str = str + " AND SD.LocationID >='" + fromLocationID + "' AND SD.LocationID <='" + toLocationID + "'";
			}
			if (fromDivisionID != "")
			{
				str = str + " AND JD2.DivisionID >='" + fromDivisionID + "' AND JD2.DivisionID <='" + toDivisionID + "'";
			}
			str += " ),0) AS [Ending Balance]\r\n                    FROM Journal_Details JD INNER JOIN Journal J ON J.SysDocID=JD.SysDocID AND J.VoucherID = JD.VoucherID INNER JOIN Account \r\n                    ON Account.AccountID= JD.AccountID LEFT JOIN System_Document SD ON SD.SysDocID=JD.SysDocID WHERE 1=1 ";
			if (isFC)
			{
				str += " AND ISNULL(DebitFC,0) + ISNULL(CreditFC,0) <> 0 ";
			}
			if (fromAccountID != "")
			{
				str = str + " AND JD.AccountID>='" + fromAccountID + "'";
			}
			if (toAccountID != "")
			{
				str = str + " AND JD.AccountID <='" + toAccountID + "' ";
			}
			if (costCenterID != "")
			{
				str = str + " AND JD.CostCenterID ='" + costCenterID + "' ";
			}
			if (fromLocationID != "")
			{
				str = str + " AND SD.LocationID >='" + fromLocationID + "' AND SD.LocationID <='" + toLocationID + "'";
			}
			if (fromDivisionID != "")
			{
				str = str + " AND JD.DivisionID >='" + fromDivisionID + "' AND JD.DivisionID <='" + toDivisionID + "'";
			}
			if (groupID != "")
			{
				str = str + " AND Account.GroupID IN (" + text3 + ") ";
			}
			if (allowposting)
			{
				str += " AND ISNULL(J.ApprovalStatus,10)=10 ";
			}
			str += " GROUP BY JD.AccountID,AccountName,Account.Alias";
			DataSet dataSet2 = new DataSet();
			FillDataSet(dataSet2, "GL", str);
			DataSet dataSet3 = new DataSet();
			str = " SELECT JournalDate AS [Journal Date],J.VoucherID AS [Number],J.SysDocType,JD.JobID,JB.JobName,  ISNULL(JD.CurrencyID,J.CurrencyID) AS [Currency],JD.CheckNumber,JD.CheckDate,JD.CheckbookID,JD.CheckID,\r\n                    (SELECT TOP 1 JD1.AccountID+'-'+AC1.AccountName FROM Journal_Details JD1  \r\n                    INNER JOIN Account AC1 ON JD1.AccountID=AC1.AccountID\r\n                    WHERE JD1.VoucherID=J.VoucherID AND JD1.SysDocID=J.SysDocID\r\n                    AND ISNULL(JD1.CheckID,0)=ISNULL(JD.CheckID,0) AND JD1.AccountID<> JD.AccountID) AS [OTH_ACT],\r\n                    (SELECT TOP 1 JD1.PayeeID + '-' + (CASE JD1.PayeeType WHEN 'C' THEN Customer.CustomerName WHEN 'V' THEN Vendor.VendorName  WHEN 'E' THEN  Employee.FirstName + ' ' + Employee.LastName  END) FROM Journal_Details JD1  \r\n                    INNER JOIN Account AC1 ON JD1.AccountID=AC1.AccountID\r\n                    LEFT OUTER JOIN Customer ON JD1.PayeeID=Customer.CustomerID LEFT OUTER JOIN\r\n                    Vendor ON JD1.PayeeID=Vendor.VendorID LEFT OUTER JOIN Employee ON Employee.EmployeeID=JD1.PayeeID                   \r\n                    WHERE JD1.VoucherID=J.VoucherID AND JD1.SysDocID=J.SysDocID\r\n                    AND ISNULL(JD1.CheckID,0)=ISNULL(JD1.CheckID,0) AND JD1.AccountID<> JD.AccountID)  AS [OTH_ACTName],\r\n                    JD.AccountID [Account Code],JD.Reference,Description,\r\n                    PayeeID + '-' + (CASE PayeeType WHEN 'C' THEN Customer.CustomerName WHEN 'V' THEN Vendor.VendorName  ELSE  Employee.FirstName + ' ' + Employee.LastName END) AS Name,\r\n                    PayeeID + '-' + (CASE PayeeType WHEN 'C' THEN Customer.CustomerName WHEN 'V' THEN Vendor.VendorName WHEN 'E' THEN Employee.FirstName + ' ' + Employee.LastName END) AS Payee\r\n                    ,AnalysisID [Analysis],ISNULL(JD.CurrencyID,J.CurrencyID) AS CurrencyID, ISNULL(JD.CurrencyRate, J.CurrencyRate) CurrencyRate,";
			str = (isFC ? (str + " DebitFC AS Debit,CreditFC AS Credit") : (str + " Debit,Credit,DebitFC,CreditFC "));
			str = str + " FROM JOURNAL_DETAILS JD INNER JOIN Journal J ON J.SysDocID=JD.SysDocID AND J.VoucherID = JD.VoucherID INNER JOIN\r\n                    Account Acc ON JD.AccountID=Acc.AccountID \r\n                    LEFT OUTER JOIN Customer ON JD.PayeeID=Customer.CustomerID LEFT OUTER JOIN\r\n                    Vendor ON JD.PayeeID=Vendor.VendorID LEFT OUTER JOIN Employee ON Employee.EmployeeID=JD.PayeeID\r\n                    LEFT OUTER JOIN Job JB ON JD.JobID=JB.JobID\r\n                    LEFT JOIN System_Document SD ON SD.SysDocID=JD.SysDocID\r\n                    WHERE J.JournalDate BETWEEN '" + text + "' AND '" + text2 + "'";
			str += " AND (JD.IsVoid IS NULL OR JD.IsVoid='False') ";
			if (fromAccountID != "")
			{
				str = str + " AND JD.AccountID>='" + fromAccountID + "'";
			}
			if (toAccountID != "")
			{
				str = str + " AND JD.AccountID<='" + toAccountID + "' ";
			}
			if (costCenterID != "")
			{
				str = str + " AND JD.CostCenterID ='" + costCenterID + "' ";
			}
			if (groupID != "")
			{
				str = str + " AND Acc.GroupID IN (" + text3 + ") ";
			}
			if (fromLocationID != "")
			{
				str = str + " AND SD.LocationID >='" + fromLocationID + "' AND SD.LocationID <='" + toLocationID + "'";
			}
			if (fromDivisionID != "")
			{
				str = str + " AND JD.DivisionID >='" + fromDivisionID + "' AND JD.DivisionID <='" + toDivisionID + "'";
			}
			if (isFC)
			{
				str += " AND ISNULL(DebitFC,0) + ISNULL(CreditFC,0) <> 0 ";
			}
			if (allowposting)
			{
				str += " AND ISNULL(J.Approvalstatus,10)=10 ";
			}
			str += " ORDER BY CONVERT(DATE, JournalDate, 103), JD.VoucherID,JD.CheckNumber,JD.CheckDate,JD.CheckbookID,JD.CheckID ";
			FillDataSet(dataSet3, "GL Details", str);
			dataSet2.Merge(dataSet3);
			dataSet2.Relations.Add("GL Details", dataSet2.Tables["GL"].Columns["Account Code"], dataSet2.Tables["GL Details"].Columns["Account Code"], createConstraints: false);
			return dataSet2;
		}

		public DataSet GetAccountLedgerReportSummary(DateTime from, DateTime to, string fromAccountID, string toAccountID, string groupID, string fromLocationID, string toLocationID, string fromDivisionID, string toDivisionID, bool isFC, string costCenterID, bool allowposting)
		{
			string empty = string.Empty;
			string str = CommonLib.ToSqlDateTimeString(from);
			string str2 = CommonLib.ToSqlDateTimeString(to);
			new Currencies(base.DBConfig).GetBaseCurrencyID();
			string text = string.Empty;
			if (groupID != "")
			{
				empty = " WITH ACCGroups \r\n                                AS\r\n                                (\r\n\t                                SELECT GroupID, GroupName, TypeID, ParentID , 0 AS L FROM Account_Group AG\r\n\t                                WHERE  GroupID = '" + groupID + "'\r\n\r\n\t\t\t\t\t\t\t\t\tUNION ALL\r\n\t                                \r\n\t\t\t\t\t\t\t\t\tSELECT AG.GroupID, AG.GroupName, AG.TypeID, AG.ParentID, L + 1 AS L\r\n                                    FROM Account_Group AG\r\n\t\t\t\t\t\t\t\t\tINNER JOIN ACCGroups AS d\r\n                                        ON AG.ParentID = d.GroupID\r\n                                )\r\n\r\n                                SELECT GroupID FROM ACCGroups ";
				DataSet dataSet = new DataSet();
				FillDataSet(dataSet, "AccountGroup", empty);
				for (int i = 0; i < dataSet.Tables["AccountGroup"].Rows.Count; i++)
				{
					DataRow dataRow = dataSet.Tables["AccountGroup"].Rows[i];
					text = text + "'" + dataRow["GroupID"].ToString() + "'";
					if (i < dataSet.Tables["AccountGroup"].Rows.Count - 1)
					{
						text += ",";
					}
				}
			}
			string str3 = "SELECT AG.GroupID,AG.GroupName,JD.AccountID AS [Account Code],AccountName AS [Account Name],Account.Alias, ";
			str3 = (isFC ? (str3 + " ISNULL((SELECT SUM(ISNULL(DebitFC,0)- ISNULL(CreditFC,0)) ") : (str3 + " ISNULL((SELECT SUM(ISNULL(Debit,0)- ISNULL(Credit,0)) "));
			str3 += " FROM Journal_Details JD2 INNER JOIN \r\n                            Journal J2 ON J2.JournalID=JD2.JournalID";
			if (fromLocationID != "")
			{
				str3 += " LEFT JOIN System_Document SD ON SD.SysDocID=JD2.SysDocID";
			}
			str3 = str3 + " WHERE JD.AccountID = JD2.AccountID AND J2.JournalDate < '" + str + "' AND ISNULL(J2.IsVoid, 'False')= 'False'";
			if (fromLocationID != "")
			{
				str3 = str3 + " AND SD.LocationID >='" + fromLocationID + "' AND SD.LocationID <='" + toLocationID + "'";
			}
			if (fromDivisionID != "")
			{
				str3 = str3 + " AND JD2.DivisionID >='" + fromDivisionID + "' AND JD2.DivisionID <='" + toDivisionID + "'";
			}
			str3 += "  ),0)AS[Opening Balance], ";
			str3 = (isFC ? (str3 + " ISNULL((SELECT SUM(ISNULL(DebitFC,0)- ISNULL(CreditFC,0)) ") : (str3 + " ISNULL((SELECT SUM(ISNULL(Debit,0)- ISNULL(Credit,0)) "));
			str3 += " FROM Journal_Details JD2 INNER JOIN \r\n                    Journal J2 ON J2.JournalID=JD2.JournalID";
			if (fromLocationID != "")
			{
				str3 += " LEFT JOIN System_Document SD ON SD.SysDocID=JD2.SysDocID";
			}
			str3 = str3 + "  WHERE JD.AccountID=JD2.AccountID AND \r\n                    J2.JournalDate<='" + str2 + "' AND ISNULL(J2.IsVoid,'False')='False'";
			if (fromLocationID != "")
			{
				str3 = str3 + " AND SD.LocationID >='" + fromLocationID + "' AND SD.LocationID <='" + toLocationID + "'";
			}
			if (fromDivisionID != "")
			{
				str3 = str3 + " AND JD2.DivisionID >='" + fromDivisionID + "' AND JD2.DivisionID <='" + toDivisionID + "'";
			}
			str3 = str3 + "  AND  JD2.JournalID NOT IN(SELECT JD1.JournalID FROM Journal J1 INNER JOIN Journal_Details JD1   \r\n                    ON J1.JournalID = JD1.JournalID WHERE JournalDate = '" + str2 + "'  AND ISNULL(JD1.Reference, '') = 'SYS_YEND') ),0) AS [Ending Balance]\r\n                    FROM Journal_Details JD INNER JOIN Journal J ON J.SysDocID=JD.SysDocID AND J.VoucherID = JD.VoucherID INNER JOIN Account \r\n                    ON Account.AccountID= JD.AccountID \r\n                    INNER JOIN Account_Group AG  ON Account.GroupID= AG.GroupID\r\n                    LEFT JOIN System_Document SD ON SD.SysDocID=JD.SysDocID WHERE 1=1 ";
			if (isFC)
			{
				str3 += " AND ISNULL(DebitFC,0) + ISNULL(CreditFC,0) <> 0 ";
			}
			if (fromAccountID != "")
			{
				str3 = str3 + " AND JD.AccountID>='" + fromAccountID + "'";
			}
			if (toAccountID != "")
			{
				str3 = str3 + " AND JD.AccountID <='" + toAccountID + "' ";
			}
			if (costCenterID != "")
			{
				str3 = str3 + " AND JD.CostCenterID ='" + costCenterID + "' ";
			}
			if (fromLocationID != "")
			{
				str3 = str3 + " AND SD.LocationID >='" + fromLocationID + "' AND SD.LocationID <='" + toLocationID + "'";
			}
			if (fromDivisionID != "")
			{
				str3 = str3 + " AND JD.DivisionID >='" + fromDivisionID + "' AND JD.DivisionID <='" + toDivisionID + "'";
			}
			if (groupID != "")
			{
				str3 = str3 + " AND Account.GroupID IN (" + text + ") ";
			}
			if (allowposting)
			{
				str3 += " AND ISNULL(J.ApprovalStatus,10)=10 ";
			}
			str3 += " GROUP BY JD.AccountID,AccountName,Account.Alias,AG.GroupID,AG.GroupName";
			DataSet dataSet2 = new DataSet();
			FillDataSet(dataSet2, "GL", str3);
			return dataSet2;
		}

		public DataSet GetAnalysisTransactionsPivotReport(DateTime from, DateTime to, string fromaccountID, string toaccountID, string fromAnalysis, string toAnalysis, string fromGroup, string toGroup, string fromLocationID, string toLocationID, string fromDivisionID, string toDivisionID)
		{
			string text = CommonLib.ToSqlDateTimeString(from);
			string text2 = CommonLib.ToSqlDateTimeString(to);
			string text3 = "SELECT AnalysisID,AnalysisName, GroupID\r\n                            FROM Analysis \r\n                            WHERE AnalysisID IN (SELECT AnalysisID FROM Analysis \r\n                                            INNER JOIN Account_Analysis_Detail AAD\r\n                                            ON Analysis.GroupID=AAD.AnalysisGroupID WHERE 1=1 )";
			if (fromAnalysis != "")
			{
				text3 = text3 + " AND AnalysisID>='" + fromAnalysis + "' ";
			}
			if (toAnalysis != "")
			{
				text3 = text3 + " AND AnalysisID <='" + toAnalysis + "' ";
			}
			if (fromGroup != "")
			{
				text3 = text3 + " AND GroupID>='" + fromGroup + "' ";
			}
			if (toGroup != "")
			{
				text3 = text3 + " AND GroupID<='" + toGroup + "' ";
			}
			DataSet dataSet = new DataSet();
			FillDataSet(dataSet, "Analysis", text3);
			DataSet dataSet2 = new DataSet();
			text3 = " SELECT RIGHT('0' +CONVERT(VARCHAR, DATEPART(MONTH, J.JournalDate)), 2) + '/' + SUBSTRING(DATENAME(MONTH, J.JournalDate), 1, 3)+ '/' + SUBSTRING(DATENAME(YEAR, J.JournalDate), 1, 4)\r\n\t\t                    AS [Month],SUBSTRING(DATENAME(YEAR, J.JournalDate), 1, 4) + '' + RIGHT('0' +CONVERT(VARCHAR, DATEPART(MONTH, J.JournalDate)), 2)   AS Monthval,JD.AccountID,Acc.AccountName,ACC.Alias, Account_Group.TypeID, JournalDate AS [Journal Date],J.VoucherID AS [Number],SysDocType, J.CurrencyID AS [Currency],\r\n                    JD.AccountID [Account Code],JD.Reference,JD.Description,\r\n                    PayeeID + '-' + (CASE PayeeType WHEN 'C' THEN Customer.CustomerName WHEN 'V' THEN Vendor.VendorName WHEN 'E' THEN Employee.FirstName + ' ' + Employee.LastName END) AS Payee\r\n                    ,A.AnalysisID,A.AnalysisName ,J.CurrencyID,J.CurrencyRate,Debit,Credit, DebitFC,CreditFC\r\n                    FROM JOURNAL_DETAILS JD INNER JOIN Journal J ON J.SysDocID=JD.SysDocID AND J.VoucherID = JD.VoucherID INNER JOIN\r\n                    Account Acc ON JD.AccountID=Acc.AccountID\r\n                    INNER JOIN Account_Group ON Acc.GroupID = Account_Group.GroupID \r\n                    INNER JOIN Analysis A ON A.AnalysisID=JD.AnalysisID\r\n                    LEFT OUTER JOIN Customer ON JD.PayeeID=Customer.CustomerID LEFt OUTER JOIN\r\n                    Vendor ON JD.PayeeID=Vendor.VendorID LEFT OUTER JOIN Employee ON Employee.EmployeeID=JD.PayeeID";
			if (fromLocationID != "")
			{
				text3 += " LEFT JOIN System_Document SD ON SD.SysDocID=JD.SysDocID";
			}
			text3 = text3 + " WHERE ISNULL(JD.AnalysisID,'') <> '' AND J.JournalDate BETWEEN '" + text + "' AND '" + text2 + "' ";
			text3 += " AND (JD.IsVoid IS NULL OR JD.IsVoid='False') AND Account_Group.TypeID in (3,4)";
			if (fromaccountID != "")
			{
				text3 = text3 + " AND JD.AccountID>='" + fromaccountID + "'";
			}
			if (toaccountID != "")
			{
				text3 = text3 + " AND JD.AccountID<='" + toaccountID + "'";
			}
			if (fromAnalysis != "")
			{
				text3 = text3 + " AND JD.AnalysisID>='" + fromAnalysis + "' ";
			}
			if (toAnalysis != "")
			{
				text3 = text3 + " AND JD.AnalysisID <='" + toAnalysis + "' ";
			}
			if (fromGroup != "")
			{
				text3 = text3 + " AND JD.AnalysisID IN (SELECT AnalysisID FROM Analysis WHERE GroupID BETWEEN '" + fromGroup + "' AND '" + toGroup + "') ";
			}
			if (fromLocationID != "")
			{
				text3 = text3 + " AND SD.LocationID >='" + fromLocationID + "' AND SD.LocationID <='" + toLocationID + "'";
			}
			if (fromDivisionID != "")
			{
				text3 = text3 + " AND JD.DivisionID >='" + fromDivisionID + "' AND JD.DivisionID <='" + toDivisionID + "'";
			}
			text3 += " ORDER BY YEAR(J.JournalDate) DESC, MONTH(J.JournalDate) DESC, DAY(J.JournalDate) DESC, J.VoucherID";
			FillDataSet(dataSet2, "GL Details", text3);
			dataSet.Merge(dataSet2);
			dataSet.Relations.Add("Rel", dataSet.Tables["Analysis"].Columns["AnalysisID"], dataSet.Tables["GL Details"].Columns["AnalysisID"], createConstraints: false);
			return dataSet;
		}

		public DataSet GetProfitAndLossComparisonReport(DateTime periodfrom1, DateTime periodto1, DateTime periodfrom2, DateTime periodto2, string fromLocationID, string toLocationID, string fromDivisionID, string toDivisionID)
		{
			string text = CommonLib.ToSqlDateTimeString(periodfrom1);
			string text2 = CommonLib.ToSqlDateTimeString(periodto1);
			string text3 = CommonLib.ToSqlDateTimeString(periodfrom2);
			string text4 = CommonLib.ToSqlDateTimeString(periodto2);
			DataSet dataSet = new DataSet();
			string text5 = "select Accountid, AccountName ,Alias, TYPEid,  sum(Balance1) balance1 , sum(Balance2) balance2  from (Select A.AccountID,AccountName,Alias,TypeID,\r\n                            SUM(ISNULL(JD.Credit,0))- SUM(ISNULL(JD.Debit,0))  AS Balance1,0.0 AS Balance2\r\n                            FROM Account  A\r\n                            INNER JOIN Account_Group AG ON A.GroupID=AG.GroupID\r\n                            INNER JOIN Journal_Details JD ON JD.AccountID=A.AccountID\r\n                            INNER JOIN Journal J ON J.SysDocID=JD.SysDocID AND J.VoucherID = JD.VoucherID";
			if (fromLocationID != "")
			{
				text5 += " LEFT JOIN System_Document SD ON SD.SysDocID=JD.SysDocID";
			}
			text5 = text5 + " WHERE TypeID IN (3) AND ISNULL(JD.IsVoid,'False')='False' AND  ISNULL(JD.Reference,'')<>'SYS_YEND'\r\n                            AND JournalDate BETWEEN '" + text + "' AND '" + text2 + "'";
			if (fromLocationID != "")
			{
				text5 = text5 + " AND (SD.LocationID >='" + fromLocationID + "' AND SD.LocationID <='" + toLocationID + "' OR JD.CostCenterID >='" + fromLocationID + "' AND JD.CostCenterID <='" + toLocationID + "')";
			}
			if (fromDivisionID != "")
			{
				text5 = text5 + " AND (JD.DivisionID >='" + fromDivisionID + "' AND JD.DivisionID  <='" + toDivisionID + "' OR JD.CostCenterID >='" + fromDivisionID + "' AND JD.CostCenterID <='" + toDivisionID + "')";
			}
			text5 += " GROUP BY A.AccountID,AccountName,Alias,TypeID";
			text5 += " UNION  Select A.AccountID,AccountName,Alias,TypeID,0.0 AS Balance1,\r\n                            SUM(ISNULL(JD.Credit,0))- SUM(ISNULL(JD.Debit,0))  AS Balance2\r\n                            FROM Account  A\r\n                            INNER JOIN Account_Group AG ON A.GroupID=AG.GroupID\r\n                            INNER JOIN Journal_Details JD ON JD.AccountID=A.AccountID\r\n                            INNER JOIN Journal J ON J.SysDocID=JD.SysDocID AND J.VoucherID = JD.VoucherID";
			if (fromLocationID != "")
			{
				text5 += " LEFT JOIN System_Document SD ON SD.SysDocID=JD.SysDocID";
			}
			text5 = text5 + " WHERE TypeID IN (3) AND ISNULL(JD.IsVoid,'False')='False' AND  ISNULL(JD.Reference,'')<>'SYS_YEND'\r\n                            AND JournalDate BETWEEN '" + text3 + "' AND '" + text4 + "'";
			if (fromLocationID != "")
			{
				text5 = text5 + " AND (SD.LocationID >='" + fromLocationID + "' AND SD.LocationID <='" + toLocationID + "' OR JD.CostCenterID >='" + fromLocationID + "' AND JD.CostCenterID <='" + toLocationID + "')";
			}
			if (fromDivisionID != "")
			{
				text5 = text5 + " AND (JD.DivisionID >='" + fromDivisionID + "' AND JD.DivisionID  <='" + toDivisionID + "' OR JD.CostCenterID >='" + fromDivisionID + "' AND JD.CostCenterID <='" + toDivisionID + "')";
			}
			text5 += " GROUP BY A.AccountID,AccountName, Alias,TypeID) s\r\n                            group by Accountid, AccountName ,Alias, TYPEid \r\n                            ";
			DataSet dataSet2 = new DataSet();
			FillDataSet(dataSet2, "Income", text5);
			dataSet = new DataSet();
			text5 = "select Accountid, AccountName ,Alias, TYPEid,  sum(Balance1) balance1 , sum(Balance2) balance2  from (Select A.AccountID,AccountName,Alias,TypeID,\r\n                            SUM(ISNULL(JD.Debit,0))- SUM(ISNULL(JD.Credit,0))  AS Balance1,0.0 AS Balance2\r\n                            FROM Account  A\r\n                            INNER JOIN Account_Group AG ON A.GroupID=AG.GroupID\r\n                            INNER JOIN Journal_Details JD ON JD.AccountID=A.AccountID\r\n                            INNER JOIN Journal J ON J.SysDocID=JD.SysDocID AND J.VoucherID = JD.VoucherID";
			if (fromLocationID != "")
			{
				text5 += " LEFT JOIN System_Document SD ON SD.SysDocID=JD.SysDocID";
			}
			text5 = text5 + " WHERE TypeID IN (4)AND SubType=8 AND ISNULL(JD.IsVoid,'False')='False' AND  ISNULL(JD.Reference,'')<>'SYS_YEND'\r\n                            AND JournalDate BETWEEN '" + text + "' AND '" + text2 + "'";
			if (fromLocationID != "")
			{
				text5 = text5 + " AND (SD.LocationID >='" + fromLocationID + "' AND SD.LocationID <='" + toLocationID + "' OR JD.CostCenterID >='" + fromLocationID + "' AND JD.CostCenterID <='" + toLocationID + "')";
			}
			if (fromDivisionID != "")
			{
				text5 = text5 + " AND (JD.DivisionID >='" + fromDivisionID + "' AND JD.DivisionID  <='" + toDivisionID + "' OR JD.CostCenterID >='" + fromDivisionID + "' AND JD.CostCenterID <='" + toDivisionID + "')";
			}
			text5 += " GROUP BY A.AccountID,AccountName,Alias,TypeID";
			text5 += " UNION Select A.AccountID,AccountName,Alias,TypeID,0.0 AS Balance1,\r\n                            SUM(ISNULL(JD.Debit,0))- SUM(ISNULL(JD.Credit,0))  AS Balance2\r\n                            FROM Account  A\r\n                            INNER JOIN Account_Group AG ON A.GroupID=AG.GroupID\r\n                            INNER JOIN Journal_Details JD ON JD.AccountID=A.AccountID\r\n                            INNER JOIN Journal J ON J.SysDocID=JD.SysDocID AND J.VoucherID = JD.VoucherID";
			if (fromLocationID != "" || fromDivisionID != "")
			{
				text5 += " LEFT JOIN System_Document SD ON SD.SysDocID=JD.SysDocID";
			}
			text5 = text5 + "  WHERE TypeID IN (4)AND SubType=8 AND ISNULL(JD.IsVoid,'False')='False' AND  ISNULL(JD.Reference,'')<>'SYS_YEND'\r\n                            AND JournalDate BETWEEN '" + text3 + "' AND '" + text4 + "'";
			if (fromLocationID != "")
			{
				text5 = text5 + " AND (SD.LocationID >='" + fromLocationID + "' AND SD.LocationID <='" + toLocationID + "' OR JD.CostCenterID >='" + fromLocationID + "' AND JD.CostCenterID <='" + toLocationID + "')";
			}
			if (fromDivisionID != "")
			{
				text5 = text5 + " AND (JD.DivisionID >='" + fromDivisionID + "' AND JD.DivisionID  <='" + toDivisionID + "' OR JD.CostCenterID >='" + fromDivisionID + "' AND JD.CostCenterID <='" + toDivisionID + "')";
			}
			text5 += " GROUP BY A.AccountID,AccountName,Alias,TypeID) s\r\n                            group by Accountid, AccountName , Alias,TYPEid \r\n                            ";
			FillDataSet(dataSet, "COGS", text5);
			dataSet2.Merge(dataSet.Tables[0]);
			dataSet = new DataSet();
			text5 = "select Accountid, AccountName ,Alias, TYPEid,  sum(Balance1) balance1 , sum(Balance2) balance2  from (Select A.AccountID,AccountName,Alias,TypeID,\r\n                            SUM(ISNULL(JD.Debit,0))- SUM(ISNULL(JD.Credit,0)) AS Balance1,0.0 AS Balance2\r\n                            FROM Account  A\r\n                            INNER JOIN Account_Group AG ON A.GroupID=AG.GroupID\r\n                            INNER JOIN Journal_Details JD ON JD.AccountID=A.AccountID\r\n                            INNER JOIN Journal J ON J.SysDocID=JD.SysDocID AND J.VoucherID = JD.VoucherID";
			if (fromLocationID != "")
			{
				text5 += " LEFT JOIN System_Document SD ON SD.SysDocID=JD.SysDocID";
			}
			text5 = text5 + " WHERE TypeID IN (4) AND SubType <> 8 AND ISNULL(JD.IsVoid,'False')='False' AND  ISNULL(JD.Reference,'')<>'SYS_YEND'\r\n                            AND JournalDate BETWEEN '" + text + "' AND '" + text2 + "'";
			if (fromLocationID != "")
			{
				text5 = text5 + " AND (SD.LocationID >='" + fromLocationID + "' AND SD.LocationID <='" + toLocationID + "' OR JD.CostCenterID >='" + fromLocationID + "' AND JD.CostCenterID <='" + toLocationID + "')";
			}
			if (fromDivisionID != "")
			{
				text5 = text5 + " AND (JD.DivisionID >='" + fromDivisionID + "' AND JD.DivisionID  <='" + toDivisionID + "' OR JD.CostCenterID >='" + fromDivisionID + "' AND JD.CostCenterID <='" + toDivisionID + "')";
			}
			text5 += " GROUP BY A.AccountID,AccountName,Alias,TypeID";
			text5 += " UNION Select A.AccountID,AccountName,Alias,TypeID,0.0 AS Balance1,\r\n                            SUM(ISNULL(JD.Debit,0))- SUM(ISNULL(JD.Credit,0)) AS Balance2\r\n                            FROM Account  A\r\n                            INNER JOIN Account_Group AG ON A.GroupID=AG.GroupID\r\n                            INNER JOIN Journal_Details JD ON JD.AccountID=A.AccountID\r\n                            INNER JOIN Journal J ON J.SysDocID=JD.SysDocID AND J.VoucherID = JD.VoucherID";
			if (fromLocationID != "")
			{
				text5 += " LEFT JOIN System_Document SD ON SD.SysDocID=JD.SysDocID";
			}
			text5 = text5 + " WHERE TypeID IN (4) AND SubType <> 8 AND ISNULL(JD.IsVoid,'False')='False' AND  ISNULL(JD.Reference,'')<>'SYS_YEND'\r\n                            AND JournalDate BETWEEN '" + text3 + "' AND '" + text4 + "'";
			if (fromLocationID != "")
			{
				text5 = text5 + " AND (SD.LocationID >='" + fromLocationID + "' AND SD.LocationID <='" + toLocationID + "' OR JD.CostCenterID >='" + fromLocationID + "' AND JD.CostCenterID <='" + toLocationID + "')";
			}
			if (fromDivisionID != "")
			{
				text5 = text5 + " AND (JD.DivisionID >='" + fromDivisionID + "' AND JD.DivisionID  <='" + toDivisionID + "' OR JD.CostCenterID >='" + fromDivisionID + "' AND JD.CostCenterID <='" + toDivisionID + "')";
			}
			text5 += " GROUP BY A.AccountID,AccountName,Alias,TypeID) s\r\n                            group by Accountid, AccountName ,Alias, TYPEid \r\n                            ";
			FillDataSet(dataSet, "Expense", text5);
			dataSet2.Merge(dataSet.Tables[0]);
			dataSet2.Tables.Add("Summary");
			dataSet2.Tables["Summary"].Columns.Add("GrossProfit1", typeof(decimal));
			dataSet2.Tables["Summary"].Columns.Add("NetIncome1", typeof(decimal));
			dataSet2.Tables["Summary"].Columns.Add("GrossProfit2", typeof(decimal));
			dataSet2.Tables["Summary"].Columns.Add("NetIncome2", typeof(decimal));
			decimal num = default(decimal);
			decimal num2 = default(decimal);
			decimal num3 = default(decimal);
			decimal num4 = default(decimal);
			foreach (DataRow row in dataSet2.Tables["Income"].Rows)
			{
				decimal result = default(decimal);
				decimal.TryParse(row["Balance1"].ToString(), out result);
				num += result;
			}
			foreach (DataRow row2 in dataSet2.Tables["Income"].Rows)
			{
				decimal result2 = default(decimal);
				decimal.TryParse(row2["Balance2"].ToString(), out result2);
				num3 += result2;
			}
			foreach (DataRow row3 in dataSet2.Tables["COGS"].Rows)
			{
				decimal result3 = default(decimal);
				decimal.TryParse(row3["Balance1"].ToString(), out result3);
				num -= result3;
			}
			foreach (DataRow row4 in dataSet2.Tables["COGS"].Rows)
			{
				decimal result4 = default(decimal);
				decimal.TryParse(row4["Balance2"].ToString(), out result4);
				num3 -= result4;
			}
			num2 = num;
			foreach (DataRow row5 in dataSet2.Tables["Expense"].Rows)
			{
				decimal result5 = default(decimal);
				decimal.TryParse(row5["Balance1"].ToString(), out result5);
				num2 -= result5;
			}
			num4 = num3;
			foreach (DataRow row6 in dataSet2.Tables["Expense"].Rows)
			{
				decimal result6 = default(decimal);
				decimal.TryParse(row6["Balance2"].ToString(), out result6);
				num4 -= result6;
			}
			dataSet2.Tables["Summary"].Rows.Add(num, num2, num3, num4);
			return dataSet2;
		}

		public DataSet GetBalanceSheetReport(DateTime to, string fromLocationID, string toLocationID, string fromDivisionID, string toDivisionID, int level, bool showAccounts, bool allowposting)
		{
			string str = CommonLib.ToSqlDateTimeString(to);
			DataSet dataSet = new DataSet();
			string textCommand = "WITH ACCGroups \r\n                        AS\r\n                        (\r\n\t                        SELECT GroupID, GroupName,TypeID,ParentID ,0 AS L,0.0 AS Balance FROM Account_Group AG\r\n\t                        WHERE ParentID IS NULL AND TypeID=1\r\n\t                        UNION ALL\r\n\t                        SELECT AG.GroupID, AG.GroupName,AG.TypeID,AG.ParentID,L+1 AS L,0.0 AS Balance\r\n                            FROM Account_Group AG\r\n                            INNER JOIN ACCGroups AS d\r\n                                ON AG.ParentID = d.GroupID\r\n                        )\r\n\r\n                        SELECT  GroupID, GroupName,TypeID,ParentID ,L , Balance FROM ACCGroups";
			DataSet dataSet2 = new DataSet();
			FillDataSet(dataSet2, "Asset", textCommand);
			dataSet = new DataSet();
			textCommand = "WITH ACCGroups \r\n                        AS\r\n                        (\r\n\t                        SELECT GroupID, GroupName,TypeID,ParentID ,0 AS L ,0.0 AS Balance FROM Account_Group AG\r\n\t                        WHERE ParentID IS NULL AND TypeID=2\r\n\t                        UNION ALL\r\n\t                        SELECT AG.GroupID, AG.GroupName,AG.TypeID,AG.ParentID,L+1 AS L,0.0 AS Balance\r\n                            FROM Account_Group AG\r\n                            INNER JOIN ACCGroups AS d\r\n                                ON AG.ParentID = d.GroupID\r\n                        )\r\n\r\n                        SELECT  GroupID, GroupName,TypeID,ParentID ,L , Balance FROM ACCGroups";
			FillDataSet(dataSet, "Liability", textCommand);
			dataSet.Tables["Liability"].PrimaryKey = new DataColumn[1]
			{
				dataSet.Tables["Liability"].Columns["GroupID"]
			};
			dataSet2.Merge(dataSet.Tables["Liability"]);
			dataSet = new DataSet();
			textCommand = "WITH ACCGroups \r\n                        AS\r\n                        (\r\n\t                        SELECT GroupID, GroupName,TypeID,ParentID ,0 AS L ,0.0 AS Balance FROM Account_Group AG\r\n\t                        WHERE ParentID IS NULL AND TypeID=5\r\n\t                        UNION ALL\r\n\t                        SELECT AG.GroupID, AG.GroupName,AG.TypeID,AG.ParentID,L+1 AS L,0.0 AS Balance\r\n                            FROM Account_Group AG\r\n                            INNER JOIN ACCGroups AS d\r\n                                ON AG.ParentID = d.GroupID\r\n                        )\r\n\r\n                        SELECT  GroupID, GroupName,TypeID,ParentID ,L , Balance FROM ACCGroups";
			FillDataSet(dataSet, "Equity", textCommand);
			dataSet.Tables["Equity"].PrimaryKey = new DataColumn[1]
			{
				dataSet.Tables["Equity"].Columns["GroupID"]
			};
			dataSet2.Merge(dataSet.Tables["Equity"]);
			dataSet = new DataSet();
			textCommand = "Select A.AccountID,AccountName,A.GroupID,TypeID,A.Alias,\r\n                        CASE WHEN TypeID=1 THEN -1 * (SUM(ISNULL(JD.Credit,0))- SUM(ISNULL(JD.Debit,0)))\r\n\t\t\t\t\t\tELSE SUM(ISNULL(JD.Credit,0))- SUM(ISNULL(JD.Debit,0)) END AS Balance\r\n                        FROM Account  A\r\n                        INNER JOIN Account_Group AG ON A.GroupID=AG.GroupID\r\n                        INNER JOIN Journal_Details JD ON JD.AccountID=A.AccountID\r\n                        INNER JOIN Journal J ON J.SysDocID=JD.SysDocID AND J.VoucherID = JD.VoucherID";
			if (fromLocationID != "")
			{
				textCommand += " LEFT JOIN System_Document SD ON SD.SysDocID=JD.SysDocID ";
			}
			textCommand = textCommand + " WHERE ISNULL(JD.IsVoid,'False')='False'\r\n                            AND JournalDate <='" + str + "'\r\n                        AND TypeID IN (1,2,5)";
			if (allowposting)
			{
				textCommand += " AND ISNULL(J.ApprovalStatus,10)=10 ";
			}
			if (fromLocationID != "")
			{
				textCommand = textCommand + "AND SD.LocationID >='" + fromLocationID + "' AND SD.LocationID <='" + toLocationID + "'";
			}
			if (fromDivisionID != "")
			{
				textCommand = textCommand + "AND JD.DivisionID >='" + fromDivisionID + "' AND JD.DivisionID <='" + toDivisionID + "'";
			}
			textCommand += " GROUP BY A.AccountID,AccountName,TypeID,A.Alias,A.GroupID";
			FillDataSet(dataSet, "Accounts", textCommand);
			dataSet.Tables["Accounts"].PrimaryKey = new DataColumn[1]
			{
				dataSet.Tables["Accounts"].Columns["AccountID"]
			};
			dataSet2.Merge(dataSet.Tables["Accounts"]);
			dataSet = new DataSet();
			textCommand = "Select 'Period' AS GroupID,'Retained Earning' AS GroupName,6 AS TypeID,'' AS ParentID,0 AS L,\r\n                    SUM(ISNULL(JD.Credit,0))- SUM(ISNULL(JD.Debit,0)) AS Balance\r\n                    FROM Account  A\r\n                    INNER JOIN Account_Group AG ON A.GroupID=AG.GroupID\r\n                    INNER JOIN Journal_Details JD ON JD.AccountID=A.AccountID\r\n                    INNER JOIN Journal J ON J.SysDocID=JD.SysDocID AND J.VoucherID = JD.VoucherID";
			if (fromLocationID != "")
			{
				textCommand += " LEFT JOIN System_Document SD ON SD.SysDocID=JD.SysDocID ";
			}
			textCommand = textCommand + " WHERE TypeID IN (3,4) AND ISNULL(JD.IsVoid,'False')='False'\r\n                            AND JournalDate <='" + str + "'";
			if (allowposting)
			{
				textCommand += " AND ISNULL(J.ApprovalStatus,10)=10 ";
			}
			if (fromLocationID != "")
			{
				textCommand = textCommand + "AND SD.LocationID >='" + fromLocationID + "' AND SD.LocationID <='" + toLocationID + "'";
			}
			if (fromDivisionID != "")
			{
				textCommand = textCommand + "AND JD.DivisionID >='" + fromDivisionID + "' AND JD.DivisionID <='" + toDivisionID + "'";
			}
			FillDataSet(dataSet, "RetainedEarning", textCommand);
			dataSet2.Tables["Equity"].Merge(dataSet.Tables["RetainedEarning"]);
			foreach (DataRow row in dataSet2.Tables["Asset"].Rows)
			{
				string str2 = row["GroupID"].ToString();
				DataRow[] array = dataSet2.Tables["Accounts"].Select("GroupID='" + str2 + "'");
				decimal num = default(decimal);
				for (int i = 0; i < array.Length; i++)
				{
					num += decimal.Parse(array[i]["Balance"].ToString());
				}
				row["Balance"] = num;
			}
			foreach (DataRow row2 in dataSet2.Tables["Liability"].Rows)
			{
				string str3 = row2["GroupID"].ToString();
				DataRow[] array2 = dataSet2.Tables["Accounts"].Select("GroupID='" + str3 + "'");
				decimal num2 = default(decimal);
				for (int j = 0; j < array2.Length; j++)
				{
					num2 += decimal.Parse(array2[j]["Balance"].ToString());
				}
				row2["Balance"] = num2;
			}
			foreach (DataRow row3 in dataSet2.Tables["Equity"].Rows)
			{
				string str4 = row3["GroupID"].ToString();
				DataRow[] array3 = dataSet2.Tables["Accounts"].Select("GroupID='" + str4 + "'");
				decimal num3 = default(decimal);
				if (array3.Length != 0)
				{
					for (int k = 0; k < array3.Length; k++)
					{
						num3 += decimal.Parse(array3[k]["Balance"].ToString());
					}
					row3["Balance"] = num3;
				}
			}
			foreach (DataRow row4 in dataSet2.Tables["Asset"].Rows)
			{
				string groupID = row4["GroupID"].ToString();
				decimal num4 = default(decimal);
				if (row4["Balance"] != DBNull.Value)
				{
					num4 = decimal.Parse(row4["Balance"].ToString());
				}
				num4 += GetGroupBalance(dataSet2, "Asset", groupID);
				row4["Balance"] = num4;
			}
			foreach (DataRow row5 in dataSet2.Tables["Liability"].Rows)
			{
				string groupID2 = row5["GroupID"].ToString();
				decimal num5 = default(decimal);
				if (row5["Balance"] != DBNull.Value)
				{
					num5 = decimal.Parse(row5["Balance"].ToString());
				}
				num5 += GetGroupBalance(dataSet2, "Liability", groupID2);
				row5["Balance"] = num5;
			}
			foreach (DataRow row6 in dataSet2.Tables["Equity"].Rows)
			{
				string groupID3 = row6["GroupID"].ToString();
				decimal num6 = default(decimal);
				if (row6["Balance"] != DBNull.Value)
				{
					num6 = decimal.Parse(row6["Balance"].ToString());
				}
				num6 += GetGroupBalance(dataSet2, "Equity", groupID3);
				row6["Balance"] = num6;
			}
			if (!showAccounts)
			{
				dataSet2.Tables["Accounts"].Rows.Clear();
			}
			for (int num7 = 6; num7 >= level; num7--)
			{
				for (int l = 0; l < dataSet2.Tables["Asset"].Rows.Count; l++)
				{
					if (dataSet2.Tables["Asset"].Rows[l]["L"].ToString() == num7.ToString())
					{
						dataSet2.Tables["Asset"].Rows.RemoveAt(l);
						l--;
					}
				}
				for (int m = 0; m < dataSet2.Tables["Liability"].Rows.Count; m++)
				{
					if (dataSet2.Tables["Liability"].Rows[m]["L"].ToString() == num7.ToString())
					{
						dataSet2.Tables["Liability"].Rows.RemoveAt(m);
						m--;
					}
				}
				for (int n = 0; n < dataSet2.Tables["Equity"].Rows.Count; n++)
				{
					if (dataSet2.Tables["Equity"].Rows[n]["L"].ToString() == num7.ToString())
					{
						dataSet2.Tables["Equity"].Rows.RemoveAt(n);
						n--;
					}
				}
			}
			return dataSet2;
		}

		public DataSet GetBalanceSheetComparisonReport(DateTime to, string fromLocationID, string toLocationID, string fromDivisionID, string toDivisionID, int level, bool showAccounts, bool allowposting)
		{
			string str = CommonLib.ToSqlDateTimeString(to);
			DataSet dataSet = new DataSet();
			string textCommand = "WITH ACCGroups \r\n                        AS\r\n                        (\r\n\t                        SELECT GroupID, GroupName,TypeID,ParentID ,0 AS L,0.0 AS Balance FROM Account_Group AG\r\n\t                        WHERE ParentID IS NULL AND TypeID=1\r\n\t                        UNION ALL\r\n\t                        SELECT AG.GroupID, AG.GroupName,AG.TypeID,AG.ParentID,L+1 AS L,0.0 AS Balance\r\n                            FROM Account_Group AG\r\n                            INNER JOIN ACCGroups AS d\r\n                                ON AG.ParentID = d.GroupID\r\n                        )\r\n\r\n                        SELECT  GroupID, GroupName,TypeID,ParentID ,L , Balance FROM ACCGroups";
			DataSet dataSet2 = new DataSet();
			FillDataSet(dataSet2, "Asset", textCommand);
			dataSet = new DataSet();
			textCommand = "WITH ACCGroups \r\n                        AS\r\n                        (\r\n\t                        SELECT GroupID, GroupName,TypeID,ParentID ,0 AS L ,0.0 AS Balance FROM Account_Group AG\r\n\t                        WHERE ParentID IS NULL AND TypeID=2\r\n\t                        UNION ALL\r\n\t                        SELECT AG.GroupID, AG.GroupName,AG.TypeID,AG.ParentID,L+1 AS L,0.0 AS Balance\r\n                            FROM Account_Group AG\r\n                            INNER JOIN ACCGroups AS d\r\n                                ON AG.ParentID = d.GroupID\r\n                        )\r\n\r\n                        SELECT  GroupID, GroupName,TypeID,ParentID ,L , Balance FROM ACCGroups";
			FillDataSet(dataSet, "Liability", textCommand);
			dataSet.Tables["Liability"].PrimaryKey = new DataColumn[1]
			{
				dataSet.Tables["Liability"].Columns["GroupID"]
			};
			dataSet2.Merge(dataSet.Tables["Liability"]);
			dataSet = new DataSet();
			textCommand = "WITH ACCGroups \r\n                        AS\r\n                        (\r\n\t                        SELECT GroupID, GroupName,TypeID,ParentID ,0 AS L ,0.0 AS Balance FROM Account_Group AG\r\n\t                        WHERE ParentID IS NULL AND TypeID=5\r\n\t                        UNION ALL\r\n\t                        SELECT AG.GroupID, AG.GroupName,AG.TypeID,AG.ParentID,L+1 AS L,0.0 AS Balance\r\n                            FROM Account_Group AG\r\n                            INNER JOIN ACCGroups AS d\r\n                                ON AG.ParentID = d.GroupID\r\n                        )\r\n\r\n                        SELECT  GroupID, GroupName,TypeID,ParentID ,L , Balance FROM ACCGroups";
			FillDataSet(dataSet, "Equity", textCommand);
			dataSet.Tables["Equity"].PrimaryKey = new DataColumn[1]
			{
				dataSet.Tables["Equity"].Columns["GroupID"]
			};
			dataSet2.Merge(dataSet.Tables["Equity"]);
			dataSet = new DataSet();
			textCommand = "Select A.AccountID,AccountName,A.GroupID,TypeID,A.Alias,\r\n                        CASE WHEN TypeID=1 THEN -1 * (SUM(ISNULL(JD.Credit,0))- SUM(ISNULL(JD.Debit,0)))\r\n\t\t\t\t\t\tELSE SUM(ISNULL(JD.Credit,0))- SUM(ISNULL(JD.Debit,0)) END AS Balance\r\n                        FROM Account  A\r\n                        INNER JOIN Account_Group AG ON A.GroupID=AG.GroupID\r\n                        INNER JOIN Journal_Details JD ON JD.AccountID=A.AccountID\r\n                        INNER JOIN Journal J ON J.SysDocID=JD.SysDocID AND J.VoucherID = JD.VoucherID";
			if (fromLocationID != "")
			{
				textCommand += " LEFT JOIN System_Document SD ON SD.SysDocID=JD.SysDocID ";
			}
			textCommand = textCommand + " WHERE ISNULL(JD.IsVoid,'False')='False'\r\n                            AND JournalDate <='" + str + "'\r\n                     AND TypeID IN (1,2,5)";
			if (allowposting)
			{
				textCommand += " AND ISNULL(J.ApprovalStatus,10)=10 ";
			}
			if (fromLocationID != "")
			{
				textCommand = textCommand + "AND SD.LocationID >='" + fromLocationID + "' AND SD.LocationID <='" + toLocationID + "'";
			}
			if (fromDivisionID != "")
			{
				textCommand = textCommand + "AND JD.DivisionID >='" + fromDivisionID + "' AND JD.DivisionID <='" + toDivisionID + "'";
			}
			textCommand += " GROUP BY A.AccountID,AccountName,TypeID,A.Alias,A.GroupID";
			FillDataSet(dataSet, "Accounts", textCommand);
			dataSet.Tables["Accounts"].PrimaryKey = new DataColumn[1]
			{
				dataSet.Tables["Accounts"].Columns["AccountID"]
			};
			dataSet2.Merge(dataSet.Tables["Accounts"]);
			dataSet = new DataSet();
			textCommand = "Select 'Period' AS GroupID,'Retained Earning' AS GroupName,6 AS TypeID,'' AS ParentID,0 AS L,\r\n                    SUM(ISNULL(JD.Credit,0))- SUM(ISNULL(JD.Debit,0)) AS Balance\r\n                    FROM Account  A\r\n                    INNER JOIN Account_Group AG ON A.GroupID=AG.GroupID\r\n                    INNER JOIN Journal_Details JD ON JD.AccountID=A.AccountID\r\n                    INNER JOIN Journal J ON J.SysDocID=JD.SysDocID AND J.VoucherID = JD.VoucherID";
			if (fromLocationID != "")
			{
				textCommand += " LEFT JOIN System_Document SD ON SD.SysDocID=JD.SysDocID ";
			}
			textCommand = textCommand + " WHERE TypeID IN (3,4) AND ISNULL(JD.IsVoid,'False')='False'\r\n                            AND JournalDate <='" + str + "'";
			if (allowposting)
			{
				textCommand += " AND ISNULL(J.ApprovalStatus,10)=10 ";
			}
			if (fromLocationID != "")
			{
				textCommand = textCommand + "AND SD.LocationID >='" + fromLocationID + "' AND SD.LocationID <='" + toLocationID + "'";
			}
			if (fromDivisionID != "")
			{
				textCommand = textCommand + "AND JD.DivisionID >='" + fromDivisionID + "' AND JD.DivisionID <='" + toDivisionID + "'";
			}
			FillDataSet(dataSet, "RetainedEarning", textCommand);
			dataSet2.Tables["Equity"].Merge(dataSet.Tables["RetainedEarning"]);
			foreach (DataRow row in dataSet2.Tables["Asset"].Rows)
			{
				string str2 = row["GroupID"].ToString();
				DataRow[] array = dataSet2.Tables["Accounts"].Select("GroupID='" + str2 + "'");
				decimal num = default(decimal);
				for (int i = 0; i < array.Length; i++)
				{
					num += decimal.Parse(array[i]["Balance"].ToString());
				}
				row["Balance"] = num;
			}
			foreach (DataRow row2 in dataSet2.Tables["Liability"].Rows)
			{
				string str3 = row2["GroupID"].ToString();
				DataRow[] array2 = dataSet2.Tables["Accounts"].Select("GroupID='" + str3 + "'");
				decimal num2 = default(decimal);
				for (int j = 0; j < array2.Length; j++)
				{
					num2 += decimal.Parse(array2[j]["Balance"].ToString());
				}
				row2["Balance"] = num2;
			}
			foreach (DataRow row3 in dataSet2.Tables["Equity"].Rows)
			{
				string str4 = row3["GroupID"].ToString();
				DataRow[] array3 = dataSet2.Tables["Accounts"].Select("GroupID='" + str4 + "'");
				decimal num3 = default(decimal);
				if (array3.Length != 0)
				{
					for (int k = 0; k < array3.Length; k++)
					{
						num3 += decimal.Parse(array3[k]["Balance"].ToString());
					}
					row3["Balance"] = num3;
				}
			}
			foreach (DataRow row4 in dataSet2.Tables["Asset"].Rows)
			{
				string groupID = row4["GroupID"].ToString();
				decimal num4 = default(decimal);
				if (row4["Balance"] != DBNull.Value)
				{
					num4 = decimal.Parse(row4["Balance"].ToString());
				}
				num4 += GetGroupBalance(dataSet2, "Asset", groupID);
				row4["Balance"] = num4;
			}
			foreach (DataRow row5 in dataSet2.Tables["Liability"].Rows)
			{
				string groupID2 = row5["GroupID"].ToString();
				decimal num5 = default(decimal);
				if (row5["Balance"] != DBNull.Value)
				{
					num5 = decimal.Parse(row5["Balance"].ToString());
				}
				num5 += GetGroupBalance(dataSet2, "Liability", groupID2);
				row5["Balance"] = num5;
			}
			foreach (DataRow row6 in dataSet2.Tables["Equity"].Rows)
			{
				string groupID3 = row6["GroupID"].ToString();
				decimal num6 = default(decimal);
				if (row6["Balance"] != DBNull.Value)
				{
					num6 = decimal.Parse(row6["Balance"].ToString());
				}
				num6 += GetGroupBalance(dataSet2, "Equity", groupID3);
				row6["Balance"] = num6;
			}
			if (!showAccounts)
			{
				dataSet2.Tables["Accounts"].Rows.Clear();
			}
			for (int num7 = 6; num7 >= level; num7--)
			{
				for (int l = 0; l < dataSet2.Tables["Asset"].Rows.Count; l++)
				{
					if (dataSet2.Tables["Asset"].Rows[l]["L"].ToString() == num7.ToString())
					{
						dataSet2.Tables["Asset"].Rows.RemoveAt(l);
						l--;
					}
				}
				for (int m = 0; m < dataSet2.Tables["Liability"].Rows.Count; m++)
				{
					if (dataSet2.Tables["Liability"].Rows[m]["L"].ToString() == num7.ToString())
					{
						dataSet2.Tables["Liability"].Rows.RemoveAt(m);
						m--;
					}
				}
				for (int n = 0; n < dataSet2.Tables["Equity"].Rows.Count; n++)
				{
					if (dataSet2.Tables["Equity"].Rows[n]["L"].ToString() == num7.ToString())
					{
						dataSet2.Tables["Equity"].Rows.RemoveAt(n);
						n--;
					}
				}
			}
			return dataSet2;
		}

		private decimal GetGroupBalance(DataSet data, string tableName, string groupID)
		{
			decimal result = default(decimal);
			DataRow[] array = data.Tables[tableName].Select("ParentID='" + groupID + "'");
			for (int i = 0; i < array.Length; i++)
			{
				result += decimal.Parse(array[i]["Balance"].ToString());
				result += GetGroupBalance(data, tableName, array[i]["GroupID"].ToString());
			}
			return result;
		}

		public DataSet GetCashFlowReport(DateTime from, DateTime to, string fromLocationID, string toLocationID)
		{
			string text = CommonLib.ToSqlDateTimeString(from);
			string text2 = CommonLib.ToSqlDateTimeString(to);
			DataSet dataSet = new DataSet();
			DataSet dataSet2 = new DataSet();
			string str = "SELECT ISNULL(SUM(Debit) - SUM(Credit),0) AS OpeningBalance ,\r\n                            ISNULL((SELECT SUM(Debit) - SUM(Credit) AS OpeningBalance \r\n\r\n                            FROM Journal_Details JD INNER JOIN Journal J ON J.SysDocID=JD.SysDocID AND J.VoucherID = JD.VoucherID\r\n                            WHERE AccountID IN (SELECT AccountID FROM Account WHERE SubType IN (2,3))\r\n                            AND ISNULL(JD.IsVoid,'False')='False'\r\n                            AND JournalDate < '" + text2 + "'),0) AS EndingBalance\r\n                            FROM Journal_Details JD INNER JOIN Journal J ON J.SysDocID=JD.SysDocID AND J.VoucherID = JD.VoucherID";
			if (fromLocationID != "")
			{
				str += " LEFT JOIN System_Document SD ON SD.SysDocID=JD.SysDocID";
			}
			str = str + " WHERE AccountID IN (SELECT AccountID FROM Account WHERE SubType IN (2,3))\r\n                            AND ISNULL(JD.IsVoid,'False')='False'\r\n                            AND JournalDate < '" + text + "'";
			if (fromLocationID != "")
			{
				str = str + "AND SD.LocationID >='" + fromLocationID + "' AND SD.LocationID <='" + toLocationID + "'";
			}
			FillDataSet(dataSet2, "Summary", str);
			dataSet = new DataSet();
			str = "SELECT J.SysDocID,J.SysDocType,  SUM(Debit) AS Inflow, SUM(Credit) AS Outflow \r\n                        FROM Journal_Details JD INNER JOIN Journal J ON J.SysDocID=JD.SysDocID AND J.VoucherID = JD.VoucherID\r\n                        LEFT OUTER JOIN System_Document SD ON SD.SysDocID=J.SysDocID\r\n                        WHERE AccountID IN (SELECT AccountID FROM Account WHERE SubType IN (2,3))\r\n                        AND ISNULL(JD.IsVoid,'False')='False'\r\n                        AND JournalDate BETWEEN '" + text + "' AND '" + text2 + "'";
			if (fromLocationID != "")
			{
				str = str + "AND SD.LocationID >='" + fromLocationID + "' AND SD.LocationID <='" + toLocationID + "'";
			}
			str += " GROUP BY J.SysDocType,DocName,J.SYsDocID";
			FillDataSet(dataSet, "CashFlow", str);
			dataSet2.Merge(dataSet.Tables[0]);
			return dataSet2;
		}

		public DataSet GetBankLedgerReport(DateTime from, DateTime to, string fromAccountID, string toAccountID, string fromLocationID, string toLocationID, string fromDivisionID, string toDivisionID)
		{
			string empty = string.Empty;
			string text = CommonLib.ToSqlDateTimeString(from);
			string text2 = CommonLib.ToSqlDateTimeString(to);
			new Currencies(base.DBConfig).GetBaseCurrencyID();
			_ = string.Empty;
			empty = " SELECT JD.AccountID AS [Account Code],AccountName AS [Account Name],Account.Alias,  ISNULL((SELECT SUM(ISNULL(Debit,0)- ISNULL(Credit,0))  FROM Journal_Details JD2 INNER JOIN \r\n                        Journal J2 ON J2.JournalID=JD2.JournalID";
			if (fromLocationID != "")
			{
				empty += " LEFT JOIN System_Document SD ON SD.SysDocID=JD2.SysDocID";
			}
			empty = empty + "  WHERE JD.AccountID = JD2.AccountID AND J2.JournalDate < '" + text + "'AND ISNULL(J2.IsVoid, 'False')= 'False' ";
			if (fromLocationID != "")
			{
				empty = empty + "AND SD.LocationID >='" + fromLocationID + "' AND SD.LocationID <='" + toLocationID + "'";
			}
			if (fromDivisionID != "")
			{
				empty = empty + "AND JD2.DivisionID >='" + fromDivisionID + "' AND JD2.DivisionID <='" + toDivisionID + "'";
			}
			empty += " ), 0) AS[Opening Balance],  ISNULL((SELECT SUM(ISNULL(Debit, 0) - ISNULL(Credit, 0))  FROM Journal_Details JD2 INNER JOIN  \r\n                                Journal J2 ON J2.JournalID = JD2.JournalID";
			if (fromLocationID != "")
			{
				empty += " LEFT JOIN System_Document SD ON SD.SysDocID=JD2.SysDocID";
			}
			empty = empty + " WHERE JD.AccountID = JD2.AccountID AND J2.JournalDate < '" + text2 + "' AND ISNULL(J2.IsVoid, 'False')= 'False' ";
			if (fromLocationID != "")
			{
				empty = empty + "AND SD.LocationID >='" + fromLocationID + "' AND SD.LocationID <='" + toLocationID + "'";
			}
			if (fromDivisionID != "")
			{
				empty = empty + "AND JD2.DivisionID >='" + fromDivisionID + "' AND JD2.DivisionID <='" + toLocationID + "'";
			}
			empty += "), 0) AS[Ending Balance]\r\n                        FROM Journal_Details JD INNER JOIN Journal J ON J.SysDocID = JD.SysDocID AND J.VoucherID = JD.VoucherID INNER JOIN Account\r\n                        ON Account.AccountID = JD.AccountID  LEFT JOIN System_Document SD ON SD.SysDocID=JD.SysDocID  WHERE 1 = 1 ";
			if (fromAccountID != "")
			{
				empty = empty + " AND JD.AccountID>='" + fromAccountID + "' ";
			}
			if (toAccountID != "")
			{
				empty = empty + " AND JD.AccountID <='" + toAccountID + "' ";
			}
			if (fromLocationID != "")
			{
				empty = empty + "AND SD.LocationID >='" + fromLocationID + "' AND SD.LocationID <='" + toLocationID + "'";
			}
			if (fromDivisionID != "")
			{
				empty = empty + "AND JD.DivisionID >='" + fromDivisionID + "' AND JD.DivisionID <='" + toDivisionID + "'";
			}
			empty += " GROUP BY JD.AccountID,AccountName,Account.Alias";
			DataSet dataSet = new DataSet();
			FillDataSet(dataSet, "BankLedger", empty);
			string text3 = " SELECT JournalDate AS [Journal Date],J.SysDocID,J.VoucherID AS [Number],J.SysDocType, ISNULL(JD.CurrencyID,J.CurrencyID) AS [Currency],JD.CheckNumber,JD.CheckDate,JD.CheckbookID,JD.CheckID,\r\n                                JD.AccountID [Account Code],JD.Reference,Description,\r\n                                ISNULL((SELECT DISTINCT PayeeID FROM Cheque_Issued CI WHERE CI.ClearanceSysDocID=J.SysDocID AND CI.ClearanceVoucherID=J.VoucherID AND CI.ChequeID=JD.CheckID),(SELECT DISTINCT PayeeID FROM Cheque_Received CR WHERE CR.DepositSysDocID=J.SysDocID AND CR.DepositVoucherID=J.VoucherID AND CR.ChequeID=JD.CheckID ) ) AS PayeeAccount,\r\n                                ISNULL((SELECT DISTINCT CASE PayeeType WHEN 'C' THEN C.CustomerName WHEN 'V' THEN V.VendorName  WHEN 'E' THEN  E.FirstName + ' ' + E.LastName ELSE AC.AccountName END\r\n                                FROM Cheque_Issued CI  \r\n                                LEFT OUTER JOIN Customer C ON CI.PayeeID=C.CustomerID LEFT OUTER JOIN\r\n                                Vendor V ON CI.PayeeID=V.VendorID LEFT OUTER JOIN Employee E ON E.EmployeeID=CI.PayeeID LEFT OUTER JOIN Account AC ON AC.AccountID=CI.PayeeID WHERE CI.ClearanceSysDocID=J.SysDocID AND CI.ClearanceVoucherID=J.VoucherID AND CI.ChequeID=JD.CheckID),\r\n                                (SELECT DISTINCT CASE PayeeType WHEN 'C' THEN C.CustomerName WHEN 'V' THEN V.VendorName  WHEN 'E' THEN  E.FirstName + ' ' + E.LastName ELSE AC.AccountName END\r\n                                FROM Cheque_Received CR \r\n                                LEFT OUTER JOIN Customer C ON CR.PayeeID=C.CustomerID LEFT OUTER JOIN\r\n                                Vendor V ON CR.PayeeID=V.VendorID LEFT OUTER JOIN Employee E ON E.EmployeeID=CR.PayeeID LEFT OUTER JOIN Account AC ON AC.AccountID=CR.PayeeID WHERE CR.DepositSysDocID=J.SysDocID AND CR.DepositVoucherID=J.VoucherID AND CR.ChequeID=JD.CheckID) ) AS PayeeName,\r\n                                PayeeID + '-' + (CASE PayeeType WHEN 'C' THEN Customer.CustomerName WHEN 'V' THEN Vendor.VendorName  ELSE  Employee.FirstName + ' ' + Employee.LastName END) AS Name,\r\n                                PayeeID + '-' + (CASE PayeeType WHEN 'C' THEN Customer.CustomerName WHEN 'V' THEN Vendor.VendorName WHEN 'E' THEN Employee.FirstName + ' ' + Employee.LastName END) AS Payee\r\n                                ,(SELECT SUBSTRING((SELECT ', '+ CustomerName+ ' ' FROM Cheque_Received CR \r\n                                INNER JOIN Customer C ON CR.PayeeID=C.CustomerID WHERE DiscountSysDocID=J.SysDocID AND DiscountVoucherID=J.VoucherID AND ChequeNumber=JD.CheckNumber FOR XML PATH('')),2,20000)) AS [DiscPayee],AnalysisID [Analysis],ISNULL(JD.CurrencyID,J.CurrencyID) AS CurrencyID, ISNULL(JD.CurrencyRate, J.CurrencyRate) CurrencyRate, Debit,Credit,DebitFC,CreditFC,ISNULL((SELECT DISTINCT CI.Note FROM Cheque_Issued CI WHERE CI.ClearanceSysDocID=J.SysDocID \r\n                                AND CI.ClearanceVoucherID=J.VoucherID AND CI.ChequeID=JD.CheckID ),(SELECT DISTINCT CR.Note FROM Cheque_Received CR WHERE CR.DepositSysDocID=J.SysDocID AND CR.DepositVoucherID=J.VoucherID AND CR.ChequeID=JD.CheckID ) ) AS ChequeDescription \r\n                                        \r\n                                    FROM JOURNAL_DETAILS JD INNER JOIN Journal J ON J.SysDocID=JD.SysDocID AND J.VoucherID = JD.VoucherID INNER JOIN\r\n                                Account Acc ON JD.AccountID=Acc.AccountID \r\n                                LEFT OUTER JOIN Customer ON JD.PayeeID=Customer.CustomerID LEFT OUTER JOIN\r\n                                Vendor ON JD.PayeeID=Vendor.VendorID LEFT OUTER JOIN Employee ON Employee.EmployeeID=JD.PayeeID\r\n\r\n                                LEFT JOIN System_Document SD ON SD.SysDocID=JD.SysDocID \r\n                                WHERE J.JournalDate BETWEEN  '" + text + "' AND '" + text2 + "'AND (JD.IsVoid IS NULL OR JD.IsVoid='False') ";
			if (fromAccountID != "")
			{
				text3 = text3 + " AND JD.AccountID>='" + fromAccountID + "' ";
			}
			if (toAccountID != "")
			{
				text3 = text3 + " AND JD.AccountID <='" + toAccountID + "' ";
			}
			if (fromLocationID != "")
			{
				text3 = text3 + "AND SD.LocationID >='" + fromLocationID + "' AND SD.LocationID <='" + toLocationID + "'";
			}
			if (fromDivisionID != "")
			{
				text3 = text3 + "AND JD.DivisionID >='" + fromDivisionID + "' AND JD.DivisionID <='" + toDivisionID + "'";
			}
			text3 += " ORDER BY CONVERT(DATE, JournalDate, 103),J.SysDocType DESC, JD.VoucherID,JD.CheckNumber,JD.CheckDate,JD.CheckbookID,JD.CheckID  ";
			FillDataSet(dataSet, "BankLedgerDetails", text3);
			string text4 = " SELECT JD.AccountID [Account Code],SD.DocName,COUNT(SD.DocName) AS [Transaction],SUM(ISNULL(Debit,0)) Debit,SUM(ISNULL(Credit,0)) AS Credit,(SELECT COUNT(ChequeNumber) FROM Cheque_Received WHERE DepositDate\r\n                                     BETWEEN  '" + text + "' AND '" + text2 + "' AND DepositAccountID=JD.AccountID AND SD.SysDocType=7 ) AS [Cheque Deposite],\r\n                                     (SELECT COUNT(ChequeNumber) FROM Cheque_Issued WHERE ClearanceDate\r\n                                     BETWEEN  '" + text + "' AND '" + text2 + "' AND BankAccountID=JD.AccountID AND SD.SysDocType=14) AS [Cheque Clear]\r\n                                     FROM JOURNAL_DETAILS JD INNER JOIN Journal J ON J.SysDocID=JD.SysDocID AND J.VoucherID = JD.VoucherID\r\n                                    LEFT JOIN System_Document SD ON SD.SysDocID=JD.SysDocID \r\n                                    WHERE  ISNULL(JD.IsVoid,'False')='False'   AND   J.JournalDate BETWEEN  '" + text + "' AND '" + text2 + "' ";
			if (fromAccountID != "")
			{
				text4 = text4 + " AND JD.AccountID>='" + fromAccountID + "' ";
			}
			if (toAccountID != "")
			{
				text4 = text4 + " AND JD.AccountID <='" + toAccountID + "' ";
			}
			if (fromLocationID != "")
			{
				text4 = text4 + " AND SD.LocationID >='" + fromLocationID + "' AND SD.LocationID <='" + toLocationID + "'";
			}
			if (fromDivisionID != "")
			{
				text4 = text4 + " AND JD.DivisionID >='" + fromDivisionID + "' AND JD.DivisionID <='" + toDivisionID + "'";
			}
			text4 += " GROUP  BY SD.DocName,JD.AccountID,SD.SysDocType  ";
			FillDataSet(dataSet, "Summary", text4);
			dataSet.Relations.Add("GL Details", dataSet.Tables["BankLedger"].Columns["Account Code"], dataSet.Tables["BankLedgerDetails"].Columns["Account Code"], createConstraints: false);
			dataSet.Relations.Add("GL Summary", dataSet.Tables["BankLedger"].Columns["Account Code"], dataSet.Tables["Summary"].Columns["Account Code"], createConstraints: false);
			return dataSet;
		}

		public DataSet GetJournalReport(DateTime from, DateTime to, string fromLocationID, string toLocationID, string fromDivisionID, string toDivisionID, bool isVoid)
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
			FillDataSet(dataSet, "Journal", text3);
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
			if (fromDivisionID != "")
			{
				text3 = text3 + " AND JD.DivisionID >='" + fromDivisionID + "' AND JD.DivisionID <='" + toDivisionID + "'";
			}
			text3 += " ORDER BY CONVERT(DATE, JournalDate, 103), JD.JournalID, JD.VoucherID ";
			FillDataSet(dataSet2, "Journal_Details", text3);
			dataSet.Merge(dataSet2);
			dataSet.Relations.Add("Journal Details", dataSet.Tables["Journal"].Columns["JournalID"], dataSet.Tables["Journal_Details"].Columns["JournalID"], createConstraints: false);
			return dataSet;
		}

		public DataSet GetGLReport(DateTime from, DateTime to, string fromLocationID, string toLocationID, string fromDivisionID, string toDivisionID)
		{
			string text = CommonLib.ToSqlDateTimeString(from);
			string text2 = CommonLib.ToSqlDateTimeString(to);
			string text3 = "SELECT JD.AccountID AS [Account Code],AccountName AS [Account Name],\r\n                            ISNULL((SELECT SUM(ISNULL(Debit,0)- ISNULL(Credit,0)) FROM Journal_Details JD2 INNER JOIN \r\n                            Journal J2 ON J2.JournalID=JD2.JournalID\r\n                            WHERE JD.AccountID=JD2.AccountID AND J2.JournalDate<'" + text + "' AND ISNULL(J2.IsVoid,'False')='False'";
			if (fromLocationID != "")
			{
				text3 = text3 + " AND SD.LocationID >='" + fromLocationID + "' AND SD.LocationID <='" + toLocationID + "'";
			}
			if (fromDivisionID != "")
			{
				text3 = text3 + " AND JD2.DivisionID >='" + fromDivisionID + "' AND JD2.DivisionID <='" + toDivisionID + "'";
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
			if (fromDivisionID != "")
			{
				text3 = text3 + " AND JD2.DivisionID >='" + fromDivisionID + "' AND JD2.DivisionID <='" + toDivisionID + "'";
			}
			text3 += "),0)\r\n                        AS [Ending Balance]\r\n                        FROM Journal_Details JD INNER JOIN Journal J ON J.SysDocID=JD.SysDocID AND J.VoucherID = JD.VoucherID INNER JOIN Account ON Account.AccountID= JD.AccountID   LEFT JOIN System_Document SD  ON  SD.SysDocID=JD.SysDocID WHERE 1=1";
			if (fromLocationID != "")
			{
				text3 = text3 + " AND SD.LocationID >='" + fromLocationID + "' AND SD.LocationID <='" + toLocationID + "'";
			}
			if (fromDivisionID != "")
			{
				text3 = text3 + " AND JD.DivisionID >='" + fromDivisionID + "' AND JD.DivisionID <='" + toDivisionID + "'";
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
			if (fromDivisionID != "")
			{
				text3 = text3 + " AND JD.DivisionID >='" + fromDivisionID + "' AND JD.DivisionID <='" + toDivisionID + "'";
			}
			FillDataSet(dataSet2, "GL Details", text3);
			dataSet.Merge(dataSet2);
			dataSet.Relations.Add("GL Details", dataSet.Tables["GL"].Columns["Account Code"], dataSet.Tables["GL Details"].Columns["Account Code"], createConstraints: false);
			return dataSet;
		}

		public DataSet GetAnalysisTransactionsReport(DateTime from, DateTime to, string accountID, string fromAnalysis, string toAnalysis, string fromGroup, string toGroup, string fromLocationID, string toLocationID, string fromDivisionID, string toDivisionID)
		{
			string text = CommonLib.ToSqlDateTimeString(from);
			string text2 = CommonLib.ToSqlDateTimeString(to);
			string str = "SELECT AnalysisID,AnalysisName,GroupID\r\n                            FROM Analysis \r\n                            WHERE AnalysisID IN (SELECT AnalysisID FROM Analysis \r\n                                            INNER JOIN Account_Analysis_Detail AAD\r\n                                            ON Analysis.GroupID=AAD.AnalysisGroupID WHERE 1=1 ";
			if (accountID != "")
			{
				str = str + " AND AAD.AccountID='" + accountID + "'";
			}
			str += " )";
			if (fromAnalysis != "")
			{
				str = str + " AND AnalysisID>='" + fromAnalysis + "' ";
			}
			if (toAnalysis != "")
			{
				str = str + " AND AnalysisID <='" + toAnalysis + "' ";
			}
			if (fromGroup != "")
			{
				str = str + " AND GroupID>='" + fromGroup + "' ";
			}
			if (toGroup != "")
			{
				str = str + " AND GroupID<='" + toGroup + "' ";
			}
			DataSet dataSet = new DataSet();
			FillDataSet(dataSet, "Analysis", str);
			DataSet dataSet2 = new DataSet();
			str = " SELECT Account_Group.TypeID, JournalDate AS [Journal Date],J.VoucherID AS [Number],J.SysDocType, J.CurrencyID AS [Currency],Acc.Alias,\r\n                    JD.AccountID [Account Code],JD.Reference,Description,\r\n                    PayeeID + '-' + (CASE PayeeType WHEN 'C' THEN Customer.CustomerName WHEN 'V' THEN Vendor.VendorName WHEN 'E' THEN Employee.FirstName + ' ' + Employee.LastName END) AS Payee\r\n                    ,AnalysisID ,J.CurrencyID,J.CurrencyRate,Debit,Credit, DebitFC,CreditFC,JD.CostCenterID,CC.CostCenterName\r\n                    FROM JOURNAL_DETAILS JD INNER JOIN Journal J ON J.SysDocID=JD.SysDocID AND J.VoucherID = JD.VoucherID INNER JOIN\r\n                    Account Acc ON JD.AccountID=Acc.AccountID\r\n                    INNER JOIN Account_Group ON Acc.GroupID = Account_Group.GroupID \r\n                    LEFT OUTER JOIN Cost_Center CC ON CC.CostCenterID=JD.CostCenterID\r\n                    LEFT OUTER JOIN Customer ON JD.PayeeID=Customer.CustomerID LEFt OUTER JOIN\r\n                    Vendor ON JD.PayeeID=Vendor.VendorID LEFT OUTER JOIN Employee ON Employee.EmployeeID=JD.PayeeID";
			if (fromLocationID != "")
			{
				str += " LEFT JOIN System_Document SD ON SD.SysDocID=JD.SysDocID";
			}
			str = str + " WHERE ISNULL(JD.AnalysisID,'') <> '' AND J.JournalDate BETWEEN '" + text + "' AND '" + text2 + "' ";
			str += " AND (JD.IsVoid IS NULL OR JD.IsVoid='False') ";
			if (accountID != "")
			{
				str = str + " AND JD.AccountID='" + accountID + "'";
			}
			if (fromAnalysis != "")
			{
				str = str + " AND AnalysisID>='" + fromAnalysis + "' ";
			}
			if (toAnalysis != "")
			{
				str = str + " AND AnalysisID <='" + toAnalysis + "' ";
			}
			if (fromGroup != "")
			{
				str = str + " AND AnalysisID IN (SELECT AnalysisID FROM Analysis WHERE GroupID BETWEEN '" + fromGroup + "' AND '" + toGroup + "') ";
			}
			if (fromLocationID != "")
			{
				str = str + " AND (SD.LocationID>='" + fromLocationID + "' or JD.CostCenterID ='" + fromLocationID + "')";
			}
			if (toLocationID != "")
			{
				str = str + " AND ( SD.LocationID <='" + toLocationID + "'  or JD.CostCenterID ='" + toLocationID + "')";
			}
			if (fromDivisionID != "")
			{
				str = str + " AND (JD.DivisionID>='" + fromDivisionID + "' or JD.CostCenterID ='" + fromDivisionID + "')";
			}
			if (toDivisionID != "")
			{
				str = str + " AND ( JD.DivisionID <='" + toDivisionID + "'  or JD.CostCenterID ='" + toDivisionID + "')";
			}
			str += " ORDER BY CONVERT(DATE, JournalDate, 103), J.VoucherID  ";
			FillDataSet(dataSet2, "GL Details", str);
			dataSet.Merge(dataSet2);
			dataSet.Relations.Add("Rel", dataSet.Tables["Analysis"].Columns["AnalysisID"], dataSet.Tables["GL Details"].Columns["AnalysisID"], createConstraints: false);
			return dataSet;
		}

		public DataSet GetDailyCashReport(DateTime from, DateTime to, string RegisterID)
		{
			string text = CommonLib.ToSqlDateTimeString(from);
			string text2 = CommonLib.ToSqlDateTimeString(to);
			to.ToShortDateString();
			new DataSet();
			DataSet dataSet = new DataSet();
			string text3 = "";
			if (RegisterID != "" && RegisterID != string.Empty)
			{
				text3 = "SELECT 'OPENING' T,  SUM(ISNULL(JD.Debit, 0) - ISNULL(JD.Credit, 0)) AS Total, A.AccountName AS LocationID,A.Alias,  1 SNo\r\n                           FROM Journal_Details JD INNER JOIN\r\n                                Journal J ON JD.SysDocID = J.SysDocID AND JD.VoucherID = J.VoucherID INNER JOIN\r\n                                Account A ON JD.AccountID = A.AccountID INNER JOIN\r\n                                System_Document SD ON J.SysDocID = SD.SysDocID\r\n                           WHERE A.SubType = 2 AND ISNULL(J.IsVoid,'False')='False'  AND JournalDate < '" + text + "'  AND A.AccountID= '" + RegisterID + "'";
				text3 = text3 + " GROUP BY  A.SubType, A.AccountID, A.AccountName,A.Alias\r\n\r\n                           UNION\r\n\r\n                          \r\n                           SELECT  'CASH RECEIPTS' T,  SUM(ISNULL(JD.Debit, 0) - ISNULL(JD.Credit, 0)) AS Total, A.AccountName AS LocationID,A.Alias,  2 SNo\r\n                           FROM Journal_Details JD INNER JOIN\r\n                                Journal J ON JD.SysDocID = J.SysDocID AND JD.VoucherID = J.VoucherID INNER JOIN\r\n                                Account A ON JD.AccountID = A.AccountID INNER JOIN\r\n                                System_Document SD ON J.SysDocID = SD.SysDocID\r\n                           WHERE A.SubType = 2  AND ISNULL(J.IsVoid,'False')='False'  AND SD.SysDocType = '3'  \r\n                           AND JournalDate BETWEEN '" + text + "' AND '" + text2 + "' AND A.AccountID= '" + RegisterID + "'";
				text3 = text3 + " GROUP BY  A.SubType, A.AccountID, A.AccountName,A.Alias\r\n\r\n                           UNION\r\n\r\n\r\n                           SELECT  'CASH PAYMENTS' T,  SUM(ISNULL(JD.Debit, 0) - ISNULL(JD.Credit, 0)) AS Total, A.AccountName AS LocationID,A.Alias,  3 SNo\r\n                           FROM Journal_Details JD INNER JOIN\r\n                                Journal J ON JD.SysDocID = J.SysDocID AND JD.VoucherID = J.VoucherID INNER JOIN\r\n                                Account A ON JD.AccountID = A.AccountID INNER JOIN\r\n                                System_Document SD ON J.SysDocID = SD.SysDocID\r\n                           WHERE A.SubType = 2  AND ISNULL(J.IsVoid,'False')='False' AND SD.SysDocType IN ( '4', '8' ) \r\n                           AND JournalDate BETWEEN '" + text + "' AND '" + text2 + "' AND A.AccountID= '" + RegisterID + "'";
				text3 = text3 + " GROUP BY  A.SubType, A.AccountID, A.AccountName ,A.Alias\r\n\r\n                           UNION\r\n\r\n\r\n                           SELECT  'CASH SALES' T,  SUM(ISNULL(JD.Debit, 0) - ISNULL(JD.Credit, 0)) AS Total,  A.AccountName AS LocationID,A.Alias,  4 SNo \r\n                           FROM Journal_Details JD INNER JOIN\r\n                                Journal J ON JD.SysDocID = J.SysDocID AND JD.VoucherID = J.VoucherID INNER JOIN\r\n                                 Account A ON JD.AccountID = A.AccountID INNER JOIN\r\n                                 System_Document SD ON J.SysDocID = SD.SysDocID\r\n                           WHERE A.SubType = 2  AND ISNULL(J.IsVoid,'False')='False' AND SD.SysDocType = '26'  \r\n                           AND JournalDate BETWEEN '" + text + "' AND '" + text2 + "' AND A.AccountID= '" + RegisterID + "'";
				text3 = text3 + "  GROUP BY  A.SubType, A.AccountID, A.AccountName,A.Alias\r\n\r\n                           UNION\r\n\r\n\r\n                          SELECT  'CASH SALE RETURNS' T,  SUM(ISNULL(JD.Debit, 0) - ISNULL(JD.Credit, 0)) AS Total, A.AccountName AS LocationID,A.Alias,  5 SNo\r\n                          FROM Journal_Details JD INNER JOIN\r\n                                Journal J ON JD.SysDocID = J.SysDocID AND JD.VoucherID = J.VoucherID INNER JOIN\r\n                                Account A ON JD.AccountID = A.AccountID INNER JOIN\r\n                                System_Document SD ON J.SysDocID = SD.SysDocID\r\n                          WHERE A.SubType = 2  AND ISNULL(J.IsVoid,'False')='False' AND SD.SysDocType = '28'  \r\n                          AND JournalDate BETWEEN '" + text + "' AND '" + text2 + "' AND A.AccountID= '" + RegisterID + "'";
				text3 = text3 + "  GROUP BY  A.SubType, A.AccountID, A.AccountName,A.Alias\r\n\r\n\r\n                          UNION\r\n\r\n                          SELECT  'CASH PURCHASE' T,  SUM(ISNULL(JD.Debit, 0) - ISNULL(JD.Credit, 0)) AS Total, A.AccountName AS LocationID,A.Alias,  6 SNo\r\n                          FROM Journal_Details JD INNER JOIN\r\n                                Journal J ON JD.SysDocID = J.SysDocID AND JD.VoucherID = J.VoucherID INNER JOIN\r\n                                Account A ON JD.AccountID = A.AccountID INNER JOIN\r\n                                System_Document SD ON J.SysDocID = SD.SysDocID\r\n                          WHERE A.SubType = 2  AND ISNULL(J.IsVoid,'False')='False' AND SD.SysDocType = '34'  \r\n                          AND J.JournalDate BETWEEN '" + text + "' AND '" + text2 + "'  AND A.AccountID= '" + RegisterID + "'";
				text3 = text3 + " GROUP BY  A.SubType,a.AccountName,A.Alias\r\n\r\n                          UNION\r\n\r\n                          SELECT  'CASH PURCHASE RETURN' T,  SUM(ISNULL(JD.Debit, 0) - ISNULL(JD.Credit, 0)) AS Total, A.AccountName AS LocationID,A.Alias,  7 SNo\r\n                          FROM Journal_Details JD INNER JOIN\r\n                                Journal J ON JD.SysDocID = J.SysDocID AND JD.VoucherID = J.VoucherID INNER JOIN\r\n                                Account A ON JD.AccountID = A.AccountID INNER JOIN\r\n                                System_Document SD ON J.SysDocID = SD.SysDocID\r\n                          WHERE A.SubType = 2  AND ISNULL(J.IsVoid,'False')='False' AND SD.SysDocType = '35'  \r\n                          AND J.JournalDate BETWEEN '" + text + "' AND '" + text2 + "'  AND A.AccountID= '" + RegisterID + "'";
				text3 = text3 + " GROUP BY  A.SubType,a.AccountName,A.Alias\r\n  \r\n                          UNION\r\n\r\n                          SELECT  'FUND TRANSFER' T,  SUM(ISNULL(JD.Debit, 0) - ISNULL(JD.Credit, 0)) AS Total, A.AccountName AS LocationID,A.Alias,  8 SNo\r\n                          FROM Journal_Details JD INNER JOIN\r\n                                Journal J ON JD.SysDocID = J.SysDocID AND JD.VoucherID = J.VoucherID INNER JOIN\r\n                                Account A ON JD.AccountID = A.AccountID INNER JOIN\r\n                                System_Document SD ON J.SysDocID = SD.SysDocID\r\n                          WHERE A.SubType = 2  AND ISNULL(J.IsVoid,'False')='False' AND SD.SysDocType = '6'  \r\n                          AND J.JournalDate BETWEEN '" + text + "' AND '" + text2 + "' AND A.AccountID= '" + RegisterID + "'  ";
				text3 = text3 + "  GROUP BY  A.SubType,a.AccountName,A.Alias\r\n\r\n                          UNION\r\n\r\n                          SELECT 'OTHERS' T,  SUM(ISNULL(JD.Debit, 0) - ISNULL(JD.Credit, 0)) AS Total, A.AccountName AS LocationID,A.Alias,  9 SNo\r\n                          FROM Journal_Details JD INNER JOIN\r\n                                Journal J ON JD.SysDocID = J.SysDocID AND JD.VoucherID = J.VoucherID INNER JOIN\r\n                                Account A ON JD.AccountID = A.AccountID INNER JOIN\r\n                                System_Document SD ON J.SysDocID = SD.SysDocID\r\n                          WHERE A.SubType = 2  AND ISNULL(J.IsVoid,'False')='False' AND SD.SysDocType NOT IN ('3','4','6','8','26','28','34','35') \r\n                          AND JournalDate BETWEEN '" + text + "' AND '" + text2 + "' AND A.AccountID= '" + RegisterID + "'";
				text3 += " GROUP BY A.SubType, A.AccountID, A.AccountName,A.Alias ORDER BY SNo";
			}
			else
			{
				text3 = "SELECT 'OPENING' T,  SUM(ISNULL(JD.Debit, 0) - ISNULL(JD.Credit, 0)) AS Total, A.AccountName AS LocationID,A.Alias,  1 SNo\r\n                           FROM Journal_Details JD INNER JOIN\r\n                                Journal J ON JD.SysDocID = J.SysDocID AND JD.VoucherID = J.VoucherID INNER JOIN\r\n                                Account A ON JD.AccountID = A.AccountID INNER JOIN\r\n                                System_Document SD ON J.SysDocID = SD.SysDocID\r\n                           WHERE A.SubType = 2 AND ISNULL(J.IsVoid,'False')='False'  AND JournalDate < '" + text + "' ";
				text3 = text3 + " GROUP BY  A.SubType, A.AccountID, A.AccountName,A.Alias\r\n\r\n                           UNION\r\n\r\n                          \r\n                           SELECT  'CASH RECEIPTS' T,  SUM(ISNULL(JD.Debit, 0) - ISNULL(JD.Credit, 0)) AS Total, A.AccountName AS LocationID,A.Alias,  2 SNo\r\n                           FROM Journal_Details JD INNER JOIN\r\n                                Journal J ON JD.SysDocID = J.SysDocID AND JD.VoucherID = J.VoucherID INNER JOIN\r\n                                Account A ON JD.AccountID = A.AccountID INNER JOIN\r\n                                System_Document SD ON J.SysDocID = SD.SysDocID\r\n                           WHERE A.SubType = 2  AND ISNULL(J.IsVoid,'False')='False'  AND SD.SysDocType = '3'  \r\n                           AND JournalDate BETWEEN '" + text + "' AND '" + text2 + "'";
				text3 = text3 + "  GROUP BY  A.SubType, A.AccountID, A.AccountName,A.Alias\r\n\r\n                           UNION\r\n\r\n\r\n                           SELECT  'CASH PAYMENTS' T,  SUM(ISNULL(JD.Debit, 0) - ISNULL(JD.Credit, 0)) AS Total, A.AccountName AS LocationID,A.Alias,  3 SNo\r\n                           FROM Journal_Details JD INNER JOIN\r\n                                Journal J ON JD.SysDocID = J.SysDocID AND JD.VoucherID = J.VoucherID INNER JOIN\r\n                                Account A ON JD.AccountID = A.AccountID INNER JOIN\r\n                                System_Document SD ON J.SysDocID = SD.SysDocID\r\n                           WHERE A.SubType = 2  AND ISNULL(J.IsVoid,'False')='False' AND SD.SysDocType IN ( '4', '8' ) \r\n                           AND JournalDate BETWEEN '" + text + "' AND '" + text2 + "'";
				text3 = text3 + " GROUP BY  A.SubType, A.AccountID, A.AccountName ,A.Alias\r\n\r\n                           UNION\r\n\r\n\r\n                           SELECT  'CASH SALES' T,  SUM(ISNULL(JD.Debit, 0) - ISNULL(JD.Credit, 0)) AS Total,  A.AccountName AS LocationID,A.Alias,  4 SNo \r\n                           FROM Journal_Details JD INNER JOIN\r\n                                Journal J ON JD.SysDocID = J.SysDocID AND JD.VoucherID = J.VoucherID INNER JOIN\r\n                                 Account A ON JD.AccountID = A.AccountID INNER JOIN\r\n                                 System_Document SD ON J.SysDocID = SD.SysDocID\r\n                           WHERE A.SubType = 2  AND ISNULL(J.IsVoid,'False')='False' AND SD.SysDocType = '26'  \r\n                           AND JournalDate BETWEEN '" + text + "' AND '" + text2 + "'";
				text3 = text3 + " GROUP BY  A.SubType, A.AccountID, A.AccountName,A.Alias\r\n\r\n                           UNION\r\n\r\n\r\n                          SELECT  'CASH SALE RETURNS' T,  SUM(ISNULL(JD.Debit, 0) - ISNULL(JD.Credit, 0)) AS Total, A.AccountName AS LocationID,A.Alias,  5 SNo\r\n                          FROM Journal_Details JD INNER JOIN\r\n                                Journal J ON JD.SysDocID = J.SysDocID AND JD.VoucherID = J.VoucherID INNER JOIN\r\n                                Account A ON JD.AccountID = A.AccountID INNER JOIN\r\n                                System_Document SD ON J.SysDocID = SD.SysDocID\r\n                          WHERE A.SubType = 2  AND ISNULL(J.IsVoid,'False')='False' AND SD.SysDocType = '28'  \r\n                          AND JournalDate BETWEEN '" + text + "' AND '" + text2 + "'";
				text3 = text3 + " GROUP BY  A.SubType, A.AccountID, A.AccountName,A.Alias\r\n\r\n\r\n                          UNION\r\n\r\n                          SELECT  'CASH PURCHASE' T,  SUM(ISNULL(JD.Debit, 0) - ISNULL(JD.Credit, 0)) AS Total, A.AccountName AS LocationID,A.Alias,  6 SNo\r\n                          FROM Journal_Details JD INNER JOIN\r\n                                Journal J ON JD.SysDocID = J.SysDocID AND JD.VoucherID = J.VoucherID INNER JOIN\r\n                                Account A ON JD.AccountID = A.AccountID INNER JOIN\r\n                                System_Document SD ON J.SysDocID = SD.SysDocID\r\n                          WHERE A.SubType = 2  AND ISNULL(J.IsVoid,'False')='False' AND SD.SysDocType = '34'  \r\n                          AND J.JournalDate BETWEEN '" + text + "' AND '" + text2 + "' ";
				text3 = text3 + " GROUP BY  A.SubType,a.AccountName,A.Alias\r\n\r\n                          UNION\r\n\r\n                          SELECT  'CASH PURCHASE RETURN' T,  SUM(ISNULL(JD.Debit, 0) - ISNULL(JD.Credit, 0)) AS Total, A.AccountName AS LocationID,A.Alias,  7 SNo\r\n                          FROM Journal_Details JD INNER JOIN\r\n                                Journal J ON JD.SysDocID = J.SysDocID AND JD.VoucherID = J.VoucherID INNER JOIN\r\n                                Account A ON JD.AccountID = A.AccountID INNER JOIN\r\n                                System_Document SD ON J.SysDocID = SD.SysDocID\r\n                          WHERE A.SubType = 2  AND ISNULL(J.IsVoid,'False')='False' AND SD.SysDocType = '35'  \r\n                          AND J.JournalDate BETWEEN '" + text + "' AND '" + text2 + "' ";
				text3 = text3 + " GROUP BY  A.SubType,a.AccountName,A.Alias\r\n  \r\n                          UNION\r\n\r\n                          SELECT  'FUND TRANSFER' T,  SUM(ISNULL(JD.Debit, 0) - ISNULL(JD.Credit, 0)) AS Total, A.AccountName AS LocationID,A.Alias,  8 SNo\r\n                          FROM Journal_Details JD INNER JOIN\r\n                                Journal J ON JD.SysDocID = J.SysDocID AND JD.VoucherID = J.VoucherID INNER JOIN\r\n                                Account A ON JD.AccountID = A.AccountID INNER JOIN\r\n                                System_Document SD ON J.SysDocID = SD.SysDocID\r\n                          WHERE A.SubType = 2  AND ISNULL(J.IsVoid,'False')='False' AND SD.SysDocType = '6'  \r\n                          AND J.JournalDate BETWEEN '" + text + "' AND '" + text2 + "' ";
				text3 = text3 + " GROUP BY  A.SubType,a.AccountName,A.Alias\r\n\r\n                          UNION\r\n\r\n                          SELECT 'OTHERS' T,  SUM(ISNULL(JD.Debit, 0) - ISNULL(JD.Credit, 0)) AS Total, A.AccountName AS LocationID,A.Alias,  9 SNo\r\n                          FROM Journal_Details JD INNER JOIN\r\n                                Journal J ON JD.SysDocID = J.SysDocID AND JD.VoucherID = J.VoucherID INNER JOIN\r\n                                Account A ON JD.AccountID = A.AccountID INNER JOIN\r\n                                System_Document SD ON J.SysDocID = SD.SysDocID\r\n                          WHERE A.SubType = 2  AND ISNULL(J.IsVoid,'False')='False' AND SD.SysDocType NOT IN ('3','4','6','8','26','28','34','35') \r\n                          AND JournalDate BETWEEN '" + text + "' AND '" + text2 + "'";
				text3 += " GROUP BY A.SubType, A.AccountID, A.AccountName,A.Alias ORDER BY SNo";
			}
			FillDataSet(dataSet, "Summary", text3);
			return dataSet;
		}

		public DataSet GetDailyCashSaleReport(DateTime from, DateTime to, string RegisterID)
		{
			string text = CommonLib.ToSqlDateTimeString(from);
			string text2 = CommonLib.ToSqlDateTimeString(to);
			to.ToShortDateString();
			DataSet dataSet = new DataSet();
			string text3 = "";
			text3 = " SELECT JD.AccountID AS [Account Code],AccountName AS [Account Name],R.RegisterID,R.RegisterName,  ISNULL((SELECT SUM(ISNULL(Debit,0)- ISNULL(Credit,0))  \r\n                    FROM Journal_Details JD2 INNER JOIN\r\n                    Journal J2 ON J2.JournalID = JD2.JournalID WHERE JD.AccountID = JD2.AccountID AND\r\n                    J2.JournalDate <'" + text + "' AND ISNULL(J2.IsVoid, 'False')= 'False' ),0) AS[Opening Balance],  \r\n                    ISNULL((SELECT SUM(ISNULL(Debit, 0) - ISNULL(Credit, 0))  FROM Journal_Details JD2 INNER JOIN\r\n                    Journal J2 ON J2.JournalID = JD2.JournalID WHERE JD.AccountID = JD2.AccountID AND\r\n                    J2.JournalDate <= '" + text2 + "' AND ISNULL(J2.IsVoid, 'False') = 'False'),0)AS[Ending Balance]\r\n                    FROM Journal_Details JD INNER JOIN Journal J ON J.SysDocID = JD.SysDocID AND J.VoucherID = JD.VoucherID INNER JOIN Account\r\n                        INNER JOIN Register R ON R.CashAccountID = Account.AccountID\r\n                    ON Account.AccountID = JD.AccountID LEFT OUTER JOIN System_Document SD ON SD.SysDocID = J.SysDocID\r\n                    WHERE 1 = 1";
			if (RegisterID != "")
			{
				text3 = text3 + " AND R.RegisterID = '" + RegisterID + "'";
			}
			text3 += " GROUP BY JD.AccountID,AccountName,R.RegisterID,R.RegisterName";
			FillDataSet(dataSet, "OpeningBalance", text3);
			DataSet dataSet2 = new DataSet();
			text3 = " SELECT SI.RegisterID,COUNT(SI.VoucherID) AS [No],SUM(SI.Total-SI.Discount+SI.TaxAmount+SI.RoundOff) AS Total FROM Sales_Invoice SI \r\n                        LEFT JOIN Customer C ON SI.CustomerID=C.CustomerID WHERE ISNULL(SI.IsCash,'False') ='True' \r\n                        AND TransactionDate BETWEEN '" + text + "' AND '" + text2 + "'  AND ISNULL(IsVoid,'False')='False'";
			if (RegisterID != "")
			{
				text3 = text3 + " AND SI.RegisterID = '" + RegisterID + "'";
			}
			text3 += " GROUP BY SI.RegisterID";
			FillDataSet(dataSet2, "TotalSale", text3);
			dataSet.Merge(dataSet2);
			DataSet dataSet3 = new DataSet();
			text3 = " SELECT SI.TransactionDate,SI.SysDocID,SI.VoucherID,C.CustomerName,SI.CustomerAddress,SI.PONumber,SI.RegisterID,\r\n                    CASE WHEN SI.PaymentMethodType=1 THEN 'Cash' WHEN SI.PaymentMethodType=3 THEN 'Credit Card'\r\n                    WHEN SI.PaymentMethodType=5 THEN 'Recevables' ELSE 'Transfer' END AS [Payment Type]\r\n                    ,(SI.Total-SI.Discount+SI.TaxAmount+SI.RoundOff) AS Total FROM Sales_Invoice SI \r\n                    INNER JOIN Customer C ON SI.CustomerID=C.CustomerID\r\n                     WHERE TransactionDate BETWEEN '" + text + "' AND '" + text2 + "'  AND ISNULL(IsVoid,'False')='False'  AND SI.PaymentMethodType<>1 ";
			if (RegisterID != "")
			{
				text3 = text3 + " AND SI.RegisterID = '" + RegisterID + "'";
			}
			FillDataSet(dataSet3, "OtherSales", text3);
			dataSet.Merge(dataSet3);
			DataSet dataSet4 = new DataSet();
			text3 = " SELECT SI.TransactionDate,SI.SysDocID,SI.VoucherID,C.CustomerName,SI.CustomerAddress,SI.PONumber,SI.RegisterID,SI.Reference\r\n                    ,(SI.Total-SI.Discount+SI.TaxAmount+SI.RoundOff) AS Total FROM Sales_Return SI \r\n                    INNER JOIN Customer C ON SI.CustomerID=C.CustomerID WHERE TransactionDate BETWEEN '" + text + "' AND '" + text2 + "'  AND ISNULL(IsVoid,'False')='False'";
			if (RegisterID != "")
			{
				text3 = text3 + " AND SI.RegisterID = '" + RegisterID + "'";
			}
			text3 += " GROUP BY SI.RegisterID,SI.Reference,SI.TransactionDate,SI.SysDocID,SI.VoucherID,C.CustomerName,SI.CustomerAddress,SI.PONumber,SI.Total, SI.Discount, SI.TaxAmount,SI.RoundOff";
			FillDataSet(dataSet4, "SalesReturn", text3);
			dataSet.Merge(dataSet4);
			DataSet dataSet5 = new DataSet();
			text3 = " SELECT GLT.SysDocID [Doc ID],VoucherID [Doc Number],TransactionDate [Date],PayeeID [Payee Code],\r\n                    (CASE PayeeType WHEN 'C' THEN Customer.CustomerName\r\n                    WHEN 'V' THEN Vendor.VendorName\r\n                    WHEN 'E' THEN Employee.FirstName\r\n                    ELSE Account.AccountName END) AS [Payee Name],Reference [Ref],\r\n                    RegisterID,GLT.Description [Desc],ISNULL(AmountFC,Amount) AS [Amount]\r\n                    FROM GL_Transaction GLT    LEFt OUTER JOIN Account ON GLT.PayeeID=Account.AccountID LEFt OUTER JOIN\r\n                    Customer ON GLT.PayeeID=Customer.CustomerID LEFt OUTER JOIN\r\n                    Vendor ON GLT.PayeeID=Vendor.VendorID LEFt OUTER JOIN\r\n                    Employee ON GLT.PayeeID=Employee.EmployeeID\r\n                    INNER JOIN System_Document SD ON SD.SysDocID=GLT.SysDocID where  SysDocType IN (3)\r\n                     AND TransactionDate BETWEEN '" + text + "' AND '" + text2 + "'  AND ISNULL(IsVoid,'False')='False'";
			if (RegisterID != "")
			{
				text3 = text3 + " AND GLT.RegisterID = '" + RegisterID + "'";
			}
			text3 += " GROUP BY GLT.RegisterID, GLT.SysDocID ,VoucherID,TransactionDate,PayeeID,PayeeType,Customer.CustomerName,Vendor.VendorName,Employee.FirstName,Account.AccountName,Reference,GLT.Description,AmountFC,Amount";
			FillDataSet(dataSet5, "Receipt", text3);
			dataSet.Merge(dataSet5);
			DataSet dataSet6 = new DataSet();
			text3 = "SELECT GLT.SysDocID [Doc ID],VoucherID [Doc Number],TransactionDate [Date],R.RegisterID,\r\n                    Reference [Ref],Acc1.AccountName [From],Acc2.AccountName [To],\r\n                    GLT.Description [Desc],ISNULL(AmountFC,Amount) AS [Amount]\r\n                    FROM GL_Transaction GLT\r\n                    LEFt OUTER JOIN Account ACC1 ON GLT.FirstAccountID=ACC1.AccountID LEFt OUTER JOIN\r\n                    Account ACC2 ON GLT.SecondAccountID=ACC2.AccountID \r\n                    INNER JOIN System_Document SD ON SD.SysDocID=GLT.SysDocID\r\n                    INNER JOIN Register R ON R.CashAccountID=ACC1.AccountID\r\n                    WHERE SysDocType IN (6) AND TransactionDate BETWEEN '" + text + "' AND '" + text2 + "'  AND ISNULL(IsVoid,'False')='False'";
			if (RegisterID != "")
			{
				text3 = text3 + " AND R.RegisterID = '" + RegisterID + "'";
			}
			text3 += " GROUP BY R.RegisterID,GLT.SysDocID,VoucherID ,TransactionDate,R.RegisterID,Reference, Acc1.AccountName,Acc2.AccountName, GLT.Description,AmountFC,Amount";
			FillDataSet(dataSet6, "FundTransfer", text3);
			dataSet.Merge(dataSet6);
			dataSet.Relations.Add("BalanceSaleRel", dataSet.Tables["OpeningBalance"].Columns["RegisterID"], dataSet.Tables["TotalSale"].Columns["RegisterID"], createConstraints: false);
			dataSet.Relations.Add("BalanceOtherSaleRel", dataSet.Tables["OpeningBalance"].Columns["RegisterID"], dataSet.Tables["OtherSales"].Columns["RegisterID"], createConstraints: false);
			dataSet.Relations.Add("BalanceReturnRel", dataSet.Tables["OpeningBalance"].Columns["RegisterID"], dataSet.Tables["SalesReturn"].Columns["RegisterID"], createConstraints: false);
			dataSet.Relations.Add("BalanceReceiptRel", dataSet.Tables["OpeningBalance"].Columns["RegisterID"], dataSet.Tables["Receipt"].Columns["RegisterID"], createConstraints: false);
			dataSet.Relations.Add("BalanceFundTransferRel", dataSet.Tables["OpeningBalance"].Columns["RegisterID"], dataSet.Tables["FundTransfer"].Columns["RegisterID"], createConstraints: false);
			return dataSet;
		}

		public DataSet GetAccountStatement(string accountID, bool includeReconciled, DateTime fromDateTime, DateTime toDateTime)
		{
			try
			{
				DataSet dataSet = new DataSet();
				StoreConfiguration.ToSqlDateTimeString(fromDateTime);
				string text = StoreConfiguration.ToSqlDateTimeString(toDateTime);
				string textCommand = "SELECT SUM(ISNULL(Debit, 0) - ISNULL(Credit, 0)) AS Value\r\n                             FROM Journal J INNER JOIN Journal_Details JD\r\n                             ON J.SysDocID=JD.SysDocID AND J.VoucherID=JD.VoucherID\r\n                             WHERE ISNULL(J.IsVoid,'False') = 'False' AND AccountID = '" + accountID + "' AND JournalDate <= '" + text + "'";
				FillDataSet(dataSet, "BookValue", textCommand);
				DataSet dataSet2 = new DataSet();
				textCommand = "SELECT ";
				textCommand = (includeReconciled ? (textCommand + " SUM(CASE WHEN ReconcileDate IS NULL THEN 0 ELSE 0 END) AS Value  ") : (textCommand + " SUM(ISNULL(Debit, 0) - ISNULL(Credit, 0)) AS Value "));
				textCommand = textCommand + "  FROM Journal J INNER JOIN Journal_Details JD\r\n                             ON J.SysDocID=JD.SysDocID AND J.VoucherID=JD.VoucherID\r\n                             WHERE  ISNULL(J.IsVoid,'False') = 'False' AND AccountID = '" + accountID + "'  AND JournalDate <= '" + text + "' ";
				if (!includeReconciled)
				{
					textCommand += " AND ReconcileDate IS NOT NULL";
				}
				FillDataSet(dataSet2, "BankStatement", textCommand);
				dataSet.Merge(dataSet2);
				return dataSet;
			}
			catch
			{
				throw;
			}
		}

		public DataSet GetProfitAndLossMonthwiseReport(DateTime from, DateTime to, string fromLocationID, string toLocationID, string fromDivisionID, string toDivisionID)
		{
			string text = CommonLib.ToSqlDateTimeString(from);
			string text2 = CommonLib.ToSqlDateTimeString(to);
			DataSet dataSet = new DataSet();
			string text3 = "SELECT  RIGHT('0' +CONVERT(VARCHAR, DATEPART(MONTH, J.JournalDate)), 2) + ') ' + SUBSTRING(DATENAME(MONTH, J.JournalDate), 1, 3)+ '/' + SUBSTRING(DATENAME(YEAR, J.JournalDate), 1, 4)\r\n\t\t                    AS [Month],\r\n                           A.AccountID,AccountName,TypeID,Alias,\r\n                            SUM(ISNULL(JD.Credit,0))- SUM(ISNULL(JD.Debit,0))  AS Balance\r\n                            FROM Account  A\r\n                            INNER JOIN Account_Group AG ON A.GroupID=AG.GroupID\r\n                            INNER JOIN Journal_Details JD ON JD.AccountID=A.AccountID\r\n                            INNER JOIN Journal J ON J.SysDocID=JD.SysDocID AND J.VoucherID = JD.VoucherID";
			if (fromLocationID != "")
			{
				text3 += " LEFT JOIN System_Document SD ON SD.SysDocID=JD.SysDocID";
			}
			text3 = text3 + " WHERE TypeID IN (3) AND ISNULL(JD.IsVoid,'False')='False'\r\n                            AND JournalDate BETWEEN '" + text + "' AND '" + text2 + "'";
			if (fromDivisionID != "")
			{
				text3 = text3 + " AND (JD.DivisionID >='" + fromDivisionID + "' AND JD.DivisionID <='" + toDivisionID + "' OR JD.CostCenterID >='" + fromDivisionID + "' AND JD.CostCenterID <='" + toDivisionID + "')";
			}
			text3 += " GROUP BY A.AccountID,AccountName,Alias,TypeID, DATEPART(MONTH, J.JournalDate), DATENAME(MONTH, J.JournalDate),DATENAME(YEAR, J.JournalDate) ORDER BY DATEPART(MONTH, J.JournalDate)";
			DataSet dataSet2 = new DataSet();
			FillDataSet(dataSet2, "Income", text3);
			dataSet = new DataSet();
			text3 = "SELECT RIGHT('0' +CONVERT(VARCHAR, DATEPART(MONTH, J.JournalDate)), 2)+ ') ' + SUBSTRING(DATENAME(MONTH, J.JournalDate), 1, 3)+ '/' + SUBSTRING(DATENAME(YEAR, J.JournalDate), 1, 4)\r\n\t\t                    AS [Month],\r\n                            A.AccountID,AccountName,Alias,TypeID,\r\n                            SUM(ISNULL(JD.Debit,0))- SUM(ISNULL(JD.Credit,0))  AS Balance\r\n                            FROM Account  A\r\n                            INNER JOIN Account_Group AG ON A.GroupID=AG.GroupID\r\n                            INNER JOIN Journal_Details JD ON JD.AccountID=A.AccountID\r\n                            INNER JOIN Journal J ON J.SysDocID=JD.SysDocID AND J.VoucherID = JD.VoucherID";
			if (fromLocationID != "")
			{
				text3 += " LEFT JOIN System_Document SD ON SD.SysDocID=JD.SysDocID";
			}
			text3 = text3 + " WHERE TypeID IN (4)AND SubType=8 AND ISNULL(JD.IsVoid,'False')='False'\r\n                            AND JournalDate BETWEEN '" + text + "' AND '" + text2 + "'";
			if (fromDivisionID != "")
			{
				text3 = text3 + " AND (JD.DivisionID >='" + fromDivisionID + "' AND JD.DivisionID <='" + toDivisionID + "' OR JD.CostCenterID >='" + fromDivisionID + "' AND JD.CostCenterID <='" + toDivisionID + "')";
			}
			text3 += " GROUP BY A.AccountID,AccountName,Alias,TypeID, DATEPART(MONTH, J.JournalDate), DATENAME(MONTH, J.JournalDate),DATENAME(YEAR, J.JournalDate)\r\n\t\t\t                ORDER BY DATEPART(MONTH, J.JournalDate)";
			FillDataSet(dataSet, "COGS", text3);
			dataSet2.Merge(dataSet.Tables[0]);
			dataSet = new DataSet();
			text3 = "SELECT RIGHT('0' +CONVERT(VARCHAR, DATEPART(MONTH, J.JournalDate)), 2) + ') ' + SUBSTRING(DATENAME(MONTH, J.JournalDate), 1, 3)+ '/' + SUBSTRING(DATENAME(YEAR, J.JournalDate), 1, 4)\r\n\t\t                    AS [Month],\r\n                            A.AccountID,AccountName,Alias,TypeID,\r\n                            SUM(ISNULL(JD.Debit,0))- SUM(ISNULL(JD.Credit,0)) AS Balance\r\n                            FROM Account  A\r\n                            INNER JOIN Account_Group AG ON A.GroupID=AG.GroupID\r\n                            INNER JOIN Journal_Details JD ON JD.AccountID=A.AccountID\r\n                            INNER JOIN Journal J ON J.SysDocID=JD.SysDocID AND J.VoucherID = JD.VoucherID";
			if (fromLocationID != "")
			{
				text3 += " LEFT JOIN System_Document SD ON SD.SysDocID=JD.SysDocID";
			}
			text3 = text3 + " WHERE TypeID IN (4) AND SubType <> 8 AND ISNULL(JD.IsVoid,'False')='False'\r\n                            AND JournalDate BETWEEN '" + text + "' AND '" + text2 + "'";
			if (fromDivisionID != "")
			{
				text3 = text3 + " AND (JD.DivisionID >='" + fromDivisionID + "' AND JD.DivisionID <='" + toDivisionID + "' OR JD.CostCenterID >='" + fromDivisionID + "' AND JD.CostCenterID <='" + toDivisionID + "')";
			}
			text3 += "GROUP BY A.AccountID,AccountName,Alias,TypeID, DATEPART(MONTH, J.JournalDate), DATENAME(MONTH, J.JournalDate),DATENAME(YEAR, J.JournalDate)\r\n\t\t\t\t\t\t\t,DATEPART(m,JournalDate) \r\n                            ORDER BY DATEPART(MONTH, J.JournalDate)";
			FillDataSet(dataSet, "Expense", text3);
			dataSet2.Merge(dataSet.Tables[0]);
			dataSet2.Tables.Add("Summary");
			dataSet2.Tables["Summary"].Columns.Add("GrossProfit", typeof(decimal));
			dataSet2.Tables["Summary"].Columns.Add("NetIncome", typeof(decimal));
			decimal num = default(decimal);
			decimal num2 = default(decimal);
			Dictionary<string, decimal> dictionary = new Dictionary<string, decimal>();
			string empty = string.Empty;
			foreach (DataRow row in dataSet2.Tables["Income"].Rows)
			{
				decimal result = default(decimal);
				decimal.TryParse(row["Balance"].ToString(), out result);
				num += result;
				empty = row["Month"].ToString();
				if (dictionary.ContainsKey(empty))
				{
					dictionary[empty] += result;
				}
				else
				{
					dictionary.Add(empty, result);
				}
			}
			foreach (DataRow row2 in dataSet2.Tables["COGS"].Rows)
			{
				decimal result2 = default(decimal);
				decimal.TryParse(row2["Balance"].ToString(), out result2);
				num -= result2;
				empty = row2["Month"].ToString();
				if (dictionary.ContainsKey(empty))
				{
					dictionary[empty] -= result2;
				}
				else
				{
					dictionary.Add(empty, -result2);
				}
			}
			dataSet2.Tables.Add("GrossProfit");
			dataSet2.Tables["GrossProfit"].Columns.Add("T");
			dataSet2.Tables["GrossProfit"].Columns.Add("Month");
			dataSet2.Tables["GrossProfit"].Columns.Add("Balance", typeof(decimal));
			foreach (KeyValuePair<string, decimal> item in dictionary)
			{
				DataRow dataRow = dataSet2.Tables["GrossProfit"].NewRow();
				dataRow["T"] = "Gross Profit";
				dataRow["Month"] = item.Key;
				dataRow["Balance"] = item.Value;
				dataSet2.Tables["GrossProfit"].Rows.Add(dataRow);
			}
			dataSet2.Tables.Add("NetIncome");
			dataSet2.Tables["NetIncome"].Columns.Add("T");
			dataSet2.Tables["NetIncome"].Columns.Add("Month");
			dataSet2.Tables["NetIncome"].Columns.Add("Balance", typeof(decimal));
			num2 = num;
			foreach (DataRow row3 in dataSet2.Tables["Expense"].Rows)
			{
				decimal result3 = default(decimal);
				decimal.TryParse(row3["Balance"].ToString(), out result3);
				num2 -= result3;
				empty = row3["Month"].ToString();
				if (dictionary.ContainsKey(empty))
				{
					dictionary[empty] -= result3;
				}
				else
				{
					dictionary.Add(empty, -result3);
				}
			}
			foreach (KeyValuePair<string, decimal> item2 in dictionary)
			{
				DataRow dataRow2 = dataSet2.Tables["NetIncome"].NewRow();
				dataRow2["T"] = "Net Income";
				dataRow2["Month"] = item2.Key;
				dataRow2["Balance"] = item2.Value;
				dataSet2.Tables["NetIncome"].Rows.Add(dataRow2);
			}
			return dataSet2;
		}

		public DataSet GetProfitAndLossReport(DateTime from, DateTime to, string fromLocationID, string toLocationID, string fromDivisionID, string toDivisionID, int level, bool allowposting)
		{
			string text = CommonLib.ToSqlDateTimeString(to);
			string text2 = CommonLib.ToSqlDateTimeString(from);
			DataSet dataSet = new DataSet();
			string textCommand = "WITH ACCGroups \r\n                            AS\r\n                            (\r\n                            SELECT GroupID, GroupName,TypeID,ParentID ,0 AS L,0.0 AS Balance,0 AS SubType FROM Account_Group AG\r\n                            WHERE ParentID IS NULL AND TypeID IN (3) \r\n                            UNION ALL\r\n                            SELECT AG.GroupID, AG.GroupName,AG.TypeID,AG.ParentID,L+1 AS L,0.0 AS Balance,0 AS SubType\r\n                               FROM Account_Group AG\r\n                               INNER JOIN ACCGroups AS d\r\n                                   ON AG.ParentID = d.GroupID\r\n                            )\r\n                    SELECT  ACG.GroupID,ACG.GroupName,ACG.TypeID,ACG.ParentID,ACG.L,ACG.Balance,ACG.SubType FROM ACCGroups ACG                    \r\n                    WHERE GroupID IN ( SELECT GroupID FROM Account WHERE Account.SubType<>1 ) \r\n                    OR GroupID IN ( SELECT ParentID  FROM Account_Group WHERE GroupID IN ( SELECT GroupID FROM Account WHERE Account.SubType = '8' ) )";
			DataSet dataSet2 = new DataSet();
			FillDataSet(dataSet2, "Income", textCommand);
			dataSet = new DataSet();
			textCommand = "WITH ACCGroups \r\n                            AS\r\n                            (\r\n                            SELECT GroupID, GroupName,TypeID,ParentID ,0 AS L,0.0 AS Balance,0 AS SubType FROM Account_Group AG\r\n                            WHERE ParentID IS NULL AND TypeID IN (3)  \r\n                            UNION ALL\r\n                            SELECT AG.GroupID, AG.GroupName,AG.TypeID,AG.ParentID,L+1 AS L,0.0 AS Balance,0 AS SubType\r\n                               FROM Account_Group AG\r\n                               INNER JOIN ACCGroups AS d\r\n                                   ON AG.ParentID = d.GroupID\r\n                            )\r\n\r\n                           SELECT  ACG.GroupID,ACG.GroupName,ACG.TypeID,ACG.ParentID,ACG.L,ACG.Balance,ACG.SubType FROM ACCGroups ACG                    \r\n                    WHERE GroupID IN ( SELECT GroupID FROM Account WHERE Account.SubType=1 ) \r\n                    OR GroupID IN ( SELECT ParentID  FROM Account_Group WHERE GroupID IN ( SELECT GroupID FROM Account WHERE Account.SubType = '8' ) )";
			FillDataSet(dataSet, "OtherIncome", textCommand);
			dataSet.Tables["OtherIncome"].PrimaryKey = new DataColumn[1]
			{
				dataSet.Tables["OtherIncome"].Columns["GroupID"]
			};
			dataSet2.Merge(dataSet.Tables["OtherIncome"]);
			dataSet = new DataSet();
			textCommand = "WITH ACCGroups \r\n                       AS\r\n                       (\r\n                           SELECT GroupID, GroupName,TypeID,ParentID ,0 AS L,0.0 AS Balance,0 AS SubType FROM Account_Group AG\r\n                           WHERE ParentID IS NULL AND TypeID=4 \r\n                           UNION ALL\r\n                           SELECT AG.GroupID, AG.GroupName,AG.TypeID,AG.ParentID,L+1 AS L,0.0 AS Balance,0 AS SubType\r\n                           FROM Account_Group AG\r\n                           INNER JOIN ACCGroups AS D ON AG.ParentID = D.GroupID\r\n                       )\r\n                    SELECT  ACG.GroupID,ACG.GroupName,ACG.TypeID,ACG.ParentID,ACG.L,ACG.Balance,ACG.SubType FROM ACCGroups ACG                    \r\n                    WHERE GroupID IN ( SELECT GroupID FROM Account WHERE Account.SubType = '8' ) \r\n                    OR GroupID IN ( SELECT ParentID  FROM Account_Group WHERE GroupID IN ( SELECT GroupID FROM Account WHERE Account.SubType = '8' ) )";
			FillDataSet(dataSet, "COGS", textCommand);
			dataSet.Tables["COGS"].PrimaryKey = new DataColumn[1]
			{
				dataSet.Tables["COGS"].Columns["GroupID"]
			};
			dataSet2.Merge(dataSet.Tables["COGS"]);
			dataSet = new DataSet();
			textCommand = "WITH ACCGroups \r\n                       AS\r\n                       (\r\n                           SELECT GroupID, GroupName,TypeID,ParentID ,0 AS L,0.0 AS Balance,0 AS SubType FROM Account_Group AG\r\n                           WHERE ParentID IS NULL AND TypeID=4 \r\n                           UNION ALL\r\n                           SELECT AG.GroupID, AG.GroupName,AG.TypeID,AG.ParentID,L+1 AS L,0.0 AS Balance,0 AS SubType\r\n                           FROM Account_Group AG\r\n                           INNER JOIN ACCGroups AS D ON AG.ParentID = D.GroupID\r\n                       )\r\n                    SELECT  ACG.GroupID,ACG.GroupName,ACG.TypeID,ACG.ParentID,ACG.L,ACG.Balance,ACG.SubType FROM ACCGroups ACG                    \r\n                    WHERE GroupID IN ( SELECT GroupID FROM Account WHERE Account.SubType <> '8' ) \r\n                    OR GroupID IN ( SELECT ParentID  FROM Account_Group WHERE GroupID IN ( SELECT GroupID FROM Account WHERE Account.SubType <> '8' ) )";
			FillDataSet(dataSet, "Expense", textCommand);
			dataSet.Tables["Expense"].PrimaryKey = new DataColumn[1]
			{
				dataSet.Tables["Expense"].Columns["GroupID"]
			};
			dataSet2.Merge(dataSet.Tables["Expense"]);
			dataSet = new DataSet();
			textCommand = "Select A.AccountID,AccountName,A.Alias,A.GroupID,TypeID,A.SubType,                     \r\n                       CASE  WHEN TypeID= 4  AND SubType=1 THEN SUM(ISNULL(JD.Debit,0))- SUM(ISNULL(JD.Credit,0))\r\n\t\t\t\t\t\tWHEN TypeID= 4  AND SubType=8 THEN SUM(ISNULL(JD.Credit,0))- SUM(ISNULL(JD.Debit,0))                      \r\n\t\t\t\t\t\tWHEN TypeID<> 4  AND SubType<>8 THEN SUM(ISNULL(JD.Credit,0))- SUM(ISNULL(JD.Debit,0))\r\n\t\t\t\t\t\tELSE SUM(ISNULL(JD.Debit,0))- SUM(ISNULL(JD.Credit,0))\r\n\t\t\t\t\t\tEND AS Balance\r\n                        FROM Account  A\r\n                        INNER JOIN Account_Group AG ON A.GroupID=AG.GroupID\r\n                        INNER JOIN Journal_Details JD ON JD.AccountID=A.AccountID\r\n                        INNER JOIN Journal J ON J.SysDocID=JD.SysDocID AND J.VoucherID = JD.VoucherID";
			if (fromLocationID != "")
			{
				textCommand += " LEFT JOIN System_Document SD ON SD.SysDocID=JD.SysDocID";
			}
			textCommand = textCommand + " WHERE ISNULL(JD.IsVoid,'False')='False' AND JournalDate BETWEEN '" + text2 + "' AND '" + text + "'\r\n                        AND TypeID IN (3,4)";
			if (fromLocationID != "")
			{
				textCommand = textCommand + " AND SD.LocationID >='" + fromLocationID + "' AND SD.LocationID <='" + toLocationID + "'";
			}
			if (fromDivisionID != "")
			{
				textCommand = textCommand + " AND JD.DivisionID >='" + fromDivisionID + "' AND JD.DivisionID <='" + toDivisionID + "'";
			}
			if (allowposting)
			{
				textCommand += " AND ISNULL(J.ApprovalStatus, 1)= 10 ";
			}
			textCommand += " GROUP BY A.AccountID,AccountName,A.Alias,TypeID,A.GroupID,A.SubType";
			FillDataSet(dataSet, "Accounts", textCommand);
			dataSet.Tables["Accounts"].PrimaryKey = new DataColumn[1]
			{
				dataSet.Tables["Accounts"].Columns["AccountID"]
			};
			dataSet2.Merge(dataSet.Tables["Accounts"]);
			foreach (DataRow row in dataSet2.Tables["Income"].Rows)
			{
				string str = row["GroupID"].ToString();
				DataRow[] array = dataSet2.Tables["Accounts"].Select("GroupID='" + str + "' ");
				decimal num = default(decimal);
				for (int i = 0; i < array.Length; i++)
				{
					num += decimal.Parse(array[i]["Balance"].ToString());
				}
				row["Balance"] = num;
			}
			foreach (DataRow row2 in dataSet2.Tables["OtherIncome"].Rows)
			{
				string str2 = row2["GroupID"].ToString();
				DataRow[] array2 = dataSet2.Tables["Accounts"].Select("GroupID='" + str2 + "' ");
				decimal num2 = default(decimal);
				for (int j = 0; j < array2.Length; j++)
				{
					num2 += decimal.Parse(array2[j]["Balance"].ToString());
				}
				row2["Balance"] = num2;
			}
			foreach (DataRow row3 in dataSet2.Tables["COGS"].Rows)
			{
				string str3 = row3["GroupID"].ToString();
				DataRow[] array3 = dataSet2.Tables["Accounts"].Select("GroupID='" + str3 + "' AND SubType = 8");
				decimal num3 = default(decimal);
				int num4 = 0;
				for (int k = 0; k < array3.Length; k++)
				{
					num3 += decimal.Parse(array3[k]["Balance"].ToString());
					num4 = int.Parse(array3[k]["SubType"].ToString());
					row3["SubType"] = num4;
				}
				row3["Balance"] = num3;
			}
			foreach (DataRow row4 in dataSet2.Tables["Expense"].Rows)
			{
				string str4 = row4["GroupID"].ToString();
				DataRow[] array4 = dataSet2.Tables["Accounts"].Select("GroupID='" + str4 + "' AND SubType <> 8");
				decimal num5 = default(decimal);
				int num6 = 0;
				if (array4.Length != 0)
				{
					for (int l = 0; l < array4.Length; l++)
					{
						num5 += decimal.Parse(array4[l]["Balance"].ToString());
						num6 = int.Parse(array4[l]["SubType"].ToString());
						row4["SubType"] = num6;
					}
					row4["Balance"] = num5;
				}
			}
			foreach (DataRow row5 in dataSet2.Tables["Income"].Rows)
			{
				string groupID = row5["GroupID"].ToString();
				decimal num7 = default(decimal);
				if (row5["Balance"] != DBNull.Value)
				{
					num7 = decimal.Parse(row5["Balance"].ToString());
				}
				num7 += GetGroupBalance(dataSet2, "Income", groupID);
				row5["Balance"] = num7;
			}
			foreach (DataRow row6 in dataSet2.Tables["OtherIncome"].Rows)
			{
				string groupID2 = row6["GroupID"].ToString();
				decimal num8 = default(decimal);
				if (row6["Balance"] != DBNull.Value)
				{
					num8 = decimal.Parse(row6["Balance"].ToString());
				}
				num8 += GetGroupBalance(dataSet2, "OtherIncome", groupID2);
				row6["Balance"] = num8;
			}
			foreach (DataRow row7 in dataSet2.Tables["COGS"].Rows)
			{
				string groupID3 = row7["GroupID"].ToString();
				decimal num9 = default(decimal);
				if (row7["Balance"] != DBNull.Value)
				{
					num9 = decimal.Parse(row7["Balance"].ToString());
				}
				num9 += GetGroupBalance(dataSet2, "COGS", groupID3);
				row7["Balance"] = num9;
			}
			foreach (DataRow row8 in dataSet2.Tables["Expense"].Rows)
			{
				string groupID4 = row8["GroupID"].ToString();
				decimal num10 = default(decimal);
				if (row8["Balance"] != DBNull.Value)
				{
					num10 = decimal.Parse(row8["Balance"].ToString());
				}
				num10 += GetGroupBalance(dataSet2, "Expense", groupID4);
				row8["Balance"] = num10;
			}
			if (1 == 0)
			{
				dataSet2.Tables["Accounts"].Rows.Clear();
			}
			for (int num11 = 6; num11 >= level; num11--)
			{
				for (int m = 0; m < dataSet2.Tables["Income"].Rows.Count; m++)
				{
					if (dataSet2.Tables["Income"].Rows[m]["L"].ToString() == num11.ToString())
					{
						dataSet2.Tables["Income"].Rows.RemoveAt(m);
						m--;
					}
				}
				for (int n = 0; n < dataSet2.Tables["OtherIncome"].Rows.Count; n++)
				{
					if (dataSet2.Tables["OtherIncome"].Rows[n]["L"].ToString() == num11.ToString())
					{
						dataSet2.Tables["OtherIncome"].Rows.RemoveAt(n);
						n--;
					}
				}
				for (int num12 = 0; num12 < dataSet2.Tables["COGS"].Rows.Count; num12++)
				{
					if (dataSet2.Tables["COGS"].Rows[num12]["L"].ToString() == num11.ToString())
					{
						dataSet2.Tables["COGS"].Rows.RemoveAt(num12);
						num12--;
					}
				}
				for (int num13 = 0; num13 < dataSet2.Tables["Expense"].Rows.Count; num13++)
				{
					if (dataSet2.Tables["Expense"].Rows[num13]["L"].ToString() == num11.ToString())
					{
						dataSet2.Tables["Expense"].Rows.RemoveAt(num13);
						num13--;
					}
				}
			}
			return dataSet2;
		}

		public DataSet GetProfitAndLossReportSummary(DateTime from, DateTime to, string fromLocationID, string toLocationID, string fromDivisionID, string toDivisionID, int level, bool allowposting)
		{
			try
			{
				string text = CommonLib.ToSqlDateTimeString(from);
				string text2 = CommonLib.ToSqlDateTimeString(to);
				DataSet dataSet = new DataSet();
				string text3 = "Select A.AccountID,AccountName,TypeID,\r\n                            SUM(ISNULL(JD.Credit,0))- SUM(ISNULL(JD.Debit,0))  AS Balance\r\n                            FROM Account  A\r\n                            INNER JOIN Account_Group AG ON A.GroupID=AG.GroupID\r\n                            INNER JOIN Journal_Details JD ON JD.AccountID=A.AccountID\r\n                            INNER JOIN Journal J ON J.JournalID = JD.JournalID ";
				if (fromLocationID != "")
				{
					text3 += " LEFT JOIN System_Document SD ON SD.SysDocID=JD.SysDocID";
				}
				text3 = text3 + " WHERE TypeID IN (3) AND  SubType <>1 AND ISNULL(JD.IsVoid,'False')='False' AND  ISNULL(JD.Reference,'')<>'SYS_YEND' \r\n                            AND JournalDate BETWEEN '" + text + "' AND '" + text2 + "'";
				if (allowposting)
				{
					text3 += " AND ISNULL(J.ApprovalStatus,10)=10 ";
				}
				if (fromLocationID != "")
				{
					text3 = text3 + " AND (SD.LocationID >='" + fromLocationID + "' AND SD.LocationID <='" + toLocationID + "' OR JD.CostCenterID >='" + fromLocationID + "' AND JD.CostCenterID <='" + toLocationID + "')";
				}
				if (fromDivisionID != "")
				{
					text3 = text3 + " AND (JD.DivisionID >='" + fromDivisionID + "' AND JD.DivisionID <='" + toDivisionID + "' OR JD.CostCenterID >='" + fromDivisionID + "' AND JD.CostCenterID <='" + toDivisionID + "')";
				}
				text3 += "GROUP BY A.AccountID,AccountName,TypeID";
				DataSet dataSet2 = new DataSet();
				FillDataSet(dataSet2, "Income", text3);
				dataSet2.Tables[0].PrimaryKey = new DataColumn[1]
				{
					dataSet2.Tables["Income"].Columns["AccountID"]
				};
				dataSet = new DataSet();
				text3 = "Select A.AccountID,AccountName,TypeID,\r\n                            SUM(ISNULL(JD.Credit,0))- SUM(ISNULL(JD.Debit,0))  AS Balance\r\n                            FROM Account  A\r\n                            INNER JOIN Account_Group AG ON A.GroupID=AG.GroupID\r\n                            INNER JOIN Journal_Details JD ON JD.AccountID=A.AccountID\r\n                            INNER JOIN Journal J ON J.JournalID = JD.JournalID ";
				if (fromLocationID != "")
				{
					text3 += " LEFT JOIN System_Document SD ON SD.SysDocID=JD.SysDocID";
				}
				text3 = text3 + "  WHERE TypeID IN (3)AND SubType=1 AND ISNULL(JD.IsVoid,'False')='False' AND  ISNULL(JD.Reference,'')<>'SYS_YEND'\r\n                            AND JournalDate BETWEEN '" + text + "' AND '" + text2 + "'";
				if (allowposting)
				{
					text3 += " AND ISNULL(J.ApprovalStatus, 1)= 10 ";
				}
				if (fromLocationID != "")
				{
					text3 = text3 + " AND (SD.LocationID >='" + fromLocationID + "' AND SD.LocationID <='" + toLocationID + "' OR JD.CostCenterID >='" + fromLocationID + "' AND JD.CostCenterID <='" + toLocationID + "')";
				}
				if (fromDivisionID != "")
				{
					text3 = text3 + " AND (JD.DivisionID >='" + fromDivisionID + "' AND JD.DivisionID <='" + toDivisionID + "' OR JD.CostCenterID >='" + fromDivisionID + "' AND JD.CostCenterID <='" + toDivisionID + "')";
				}
				text3 += "GROUP BY A.AccountID,AccountName,TypeID";
				FillDataSet(dataSet, "OtherIncome", text3);
				dataSet.Tables[0].PrimaryKey = new DataColumn[1]
				{
					dataSet.Tables["OtherIncome"].Columns["AccountID"]
				};
				dataSet2.Merge(dataSet.Tables[0]);
				dataSet = new DataSet();
				text3 = "Select A.AccountID,AccountName,TypeID,\r\n                            SUM(ISNULL(JD.Debit,0))- SUM(ISNULL(JD.Credit,0))  AS Balance\r\n                            FROM Account  A\r\n                            INNER JOIN Account_Group AG ON A.GroupID=AG.GroupID\r\n                            INNER JOIN Journal_Details JD ON JD.AccountID=A.AccountID\r\n                            INNER JOIN Journal J ON J.JournalID = JD.JournalID ";
				if (fromLocationID != "")
				{
					text3 += " LEFT JOIN System_Document SD ON SD.SysDocID=JD.SysDocID";
				}
				text3 = text3 + " WHERE TypeID IN (4)AND SubType=8 AND ISNULL(JD.IsVoid,'False')='False' AND  ISNULL(JD.Reference,'')<>'SYS_YEND'\r\n                            AND JournalDate BETWEEN '" + text + "' AND '" + text2 + "'";
				if (allowposting)
				{
					text3 += " AND ISNULL(J.ApprovalStatus,10)=10 ";
				}
				if (fromLocationID != "")
				{
					text3 = text3 + " AND (SD.LocationID >='" + fromLocationID + "' AND SD.LocationID <='" + toLocationID + "' OR JD.CostCenterID >='" + fromLocationID + "' AND JD.CostCenterID <='" + toLocationID + "')";
				}
				if (fromDivisionID != "")
				{
					text3 = text3 + " AND (JD.DivisionID >='" + fromDivisionID + "' AND JD.DivisionID <='" + toDivisionID + "' OR JD.CostCenterID >='" + fromDivisionID + "' AND JD.CostCenterID <='" + fromDivisionID + "')";
				}
				text3 += "GROUP BY A.AccountID,AccountName,TypeID";
				FillDataSet(dataSet, "COGS", text3);
				dataSet.Tables[0].PrimaryKey = new DataColumn[1]
				{
					dataSet.Tables["COGS"].Columns["AccountID"]
				};
				dataSet2.Merge(dataSet.Tables[0]);
				dataSet = new DataSet();
				text3 = "Select A.AccountID,AccountName,TypeID,\r\n                            SUM(ISNULL(JD.Debit,0))- SUM(ISNULL(JD.Credit,0)) AS Balance\r\n                            FROM Account  A\r\n                            INNER JOIN Account_Group AG ON A.GroupID=AG.GroupID\r\n                            INNER JOIN Journal_Details JD ON JD.AccountID=A.AccountID\r\n                            INNER JOIN Journal J ON J.JournalID = JD.JournalID  ";
				if (fromLocationID != "")
				{
					text3 += " LEFT JOIN System_Document SD ON SD.SysDocID=JD.SysDocID";
				}
				text3 = text3 + " WHERE TypeID IN (4) AND SubType <> 8 AND ISNULL(JD.IsVoid,'False')='False' AND  ISNULL(JD.Reference,'')<>'SYS_YEND'\r\n                            AND JournalDate BETWEEN '" + text + "' AND '" + text2 + "'   ";
				if (allowposting)
				{
					text3 += " AND ISNULL(J.ApprovalStatus,10)=10 ";
				}
				if (fromLocationID != "")
				{
					text3 = text3 + " AND (SD.LocationID >='" + fromLocationID + "' AND SD.LocationID <='" + toLocationID + "' OR JD.CostCenterID >='" + fromLocationID + "' AND JD.CostCenterID <='" + toLocationID + "')";
				}
				if (fromDivisionID != "")
				{
					text3 = text3 + " AND (JD.DivisionID >='" + fromDivisionID + "' AND JD.DivisionID <='" + toDivisionID + "' OR JD.CostCenterID >='" + fromDivisionID + "' AND JD.CostCenterID <='" + toDivisionID + "')";
				}
				text3 += "GROUP BY A.AccountID,AccountName,TypeID ORDER BY  AccountName  ASC ";
				FillDataSet(dataSet, "Expense", text3);
				dataSet.Tables[0].PrimaryKey = new DataColumn[1]
				{
					dataSet.Tables["Expense"].Columns["AccountID"]
				};
				dataSet2.Merge(dataSet.Tables[0]);
				dataSet2.Tables.Add("Summary");
				dataSet2.Tables["Summary"].Columns.Add("GrossProfit", typeof(decimal));
				dataSet2.Tables["Summary"].Columns.Add("NetIncome", typeof(decimal));
				dataSet2.Tables["Summary"].Columns.Add("OtherIncome", typeof(decimal));
				dataSet2.Tables["Summary"].Columns.Add("TotalIncome", typeof(decimal));
				decimal num = default(decimal);
				decimal num2 = default(decimal);
				decimal num3 = default(decimal);
				decimal num4 = default(decimal);
				foreach (DataRow row in dataSet2.Tables["Income"].Rows)
				{
					decimal result = default(decimal);
					decimal.TryParse(row["Balance"].ToString(), out result);
					num += result;
				}
				foreach (DataRow row2 in dataSet2.Tables["OtherIncome"].Rows)
				{
					decimal result2 = default(decimal);
					decimal.TryParse(row2["Balance"].ToString(), out result2);
					num3 += result2;
				}
				foreach (DataRow row3 in dataSet2.Tables["COGS"].Rows)
				{
					decimal result3 = default(decimal);
					decimal.TryParse(row3["Balance"].ToString(), out result3);
					num -= result3;
				}
				num4 = num + num3;
				num2 = num + num3;
				foreach (DataRow row4 in dataSet2.Tables["Expense"].Rows)
				{
					decimal result4 = default(decimal);
					decimal.TryParse(row4["Balance"].ToString(), out result4);
					num2 -= result4;
				}
				dataSet2.Tables["Summary"].Rows.Add(num, num2, num3, num4);
				return dataSet2;
			}
			catch
			{
				throw;
			}
		}

		public DataSet GetTrialBalanceReportwithAccountHead(DateTime from, DateTime to, string fromLocationID, string toLocationID, string fromDivisionID, string toDivisionID, bool showZeroBalance, bool allowposting)
		{
			try
			{
				string text = CommonLib.ToSqlDateTimeString(from);
				string text2 = CommonLib.ToSqlDateTimeString(to);
				int num = 5;
				bool flag = true;
				DataSet dataSet = new DataSet();
				string text3 = " AND JD.JournalID NOT IN (SELECT JD1.JournalID FROM Journal J1 INNER JOIN Journal_Details JD1 ON J1.JournalID=JD1.JournalID WHERE JournalDate = '" + text2 + "'\r\n                            AND ISNULL(JD1.Reference, '')= 'SYS_YEND')";
				string text4 = " AND JD.JournalID NOT IN (SELECT JD1.JournalID FROM Journal J1 INNER JOIN Journal_Details JD1 ON J1.JournalID=JD1.JournalID WHERE JournalDate <=  '" + text2 + "'\r\n                            AND ISNULL(JD1.Reference, '')= 'SYS_YEND')";
				string text5 = " AND JD.JournalID NOT IN (SELECT JD1.JournalID FROM Journal J1 INNER JOIN Journal_Details JD1 ON J1.JournalID=JD1.JournalID WHERE JournalDate ='" + text2 + "'\r\n                            AND ISNULL(JD1.Reference, '')= 'SYS_YEND')";
				string textCommand = "WITH ACCGroups \r\n                        AS\r\n                        (\r\n\t                        SELECT GroupID, GroupName,TypeID,ParentID ,0 AS L,0.0 AS Balance FROM Account_Group AG\r\n\t                        WHERE ParentID IS NULL AND TypeID IN (1,3)\r\n\t                        UNION ALL\r\n\t                        SELECT AG.GroupID, AG.GroupName,AG.TypeID,AG.ParentID,L+1 AS L,0.0 AS Balance\r\n                            FROM Account_Group AG\r\n                            INNER JOIN ACCGroups AS d\r\n                                ON AG.ParentID = d.GroupID\r\n                        )\r\n\r\n                        SELECT  GroupID, GroupName,TypeID,ParentID ,L , Balance FROM ACCGroups";
				DataSet dataSet2 = new DataSet();
				FillDataSet(dataSet2, "Asset", textCommand);
				dataSet = new DataSet();
				textCommand = "WITH ACCGroups \r\n                        AS\r\n                        (\r\n\t                        SELECT GroupID, GroupName,TypeID,ParentID ,0 AS L ,0.0 AS Balance FROM Account_Group AG\r\n\t                        WHERE ParentID IS NULL AND TypeID IN (2,4)\r\n\t                        UNION ALL\r\n\t                        SELECT AG.GroupID, AG.GroupName,AG.TypeID,AG.ParentID,L+1 AS L,0.0 AS Balance\r\n                            FROM Account_Group AG\r\n                            INNER JOIN ACCGroups AS d\r\n                                ON AG.ParentID = d.GroupID\r\n                        )\r\n\r\n                        SELECT  GroupID, GroupName,TypeID,ParentID ,L , Balance FROM ACCGroups";
				FillDataSet(dataSet, "Liability", textCommand);
				dataSet.Tables["Liability"].PrimaryKey = new DataColumn[1]
				{
					dataSet.Tables["Liability"].Columns["GroupID"]
				};
				dataSet2.Merge(dataSet.Tables["Liability"]);
				dataSet = new DataSet();
				textCommand = "WITH ACCGroups \r\n                        AS\r\n                        (\r\n\t                        SELECT GroupID, GroupName,TypeID,ParentID ,0 AS L ,0.0 AS Balance FROM Account_Group AG\r\n\t                        WHERE ParentID IS NULL AND TypeID=5\r\n\t                        UNION ALL\r\n\t                        SELECT AG.GroupID, AG.GroupName,AG.TypeID,AG.ParentID,L+1 AS L,0.0 AS Balance\r\n                            FROM Account_Group AG\r\n                            INNER JOIN ACCGroups AS d\r\n                                ON AG.ParentID = d.GroupID\r\n                        )\r\n\r\n                        SELECT  GroupID, GroupName,TypeID,ParentID ,L , Balance FROM ACCGroups";
				FillDataSet(dataSet, "Equity", textCommand);
				dataSet.Tables["Equity"].PrimaryKey = new DataColumn[1]
				{
					dataSet.Tables["Equity"].Columns["GroupID"]
				};
				dataSet2.Merge(dataSet.Tables["Equity"]);
				dataSet = new DataSet();
				textCommand = "Select JD.AccountID, AccountName, AC.Alias, AC.GroupID,TypeID,\r\n                        CASE  WHEN SUM(ISNULL(Debit,0))-SUM(ISNULL(Credit,0)) > 0 THEN SUM(ISNULL(Debit,0))-SUM(ISNULL(Credit,0)) ELSE NULL END AS Debit,\r\n                        CASE  WHEN SUM(ISNULL(Debit,0))-SUM(ISNULL(Credit,0)) < 0 THEN SUM(ISNULL(Credit,0))-SUM(ISNULL(Debit,0)) ELSE NULL END AS Credit,\r\n                        CASE WHEN TypeID=1 THEN -1 * (SUM(ISNULL(JD.Credit,0))- SUM(ISNULL(JD.Debit,0)))\r\n\t\t\t\t\t\tELSE SUM(ISNULL(JD.Credit,0))- SUM(ISNULL(JD.Debit,0)) END AS Balance\r\n                        FROM  Account AC (nolock) INNER JOIN Journal_Details JD (nolock) ON JD.AccountID=AC.AccountID\r\n                        INNER JOIN Account_Group AG ON AC.GroupID=AG.GroupID\r\n                        INNER JOIN Journal J (nolock) ON J.JournalID = JD.JournalID AND J.SysDocID=JD.SysDocID AND J.VoucherID = JD.VoucherID";
				if (fromLocationID != "")
				{
					textCommand += " LEFT JOIN System_Document SD ON SD.SysDocID=JD.SysDocID";
				}
				textCommand = textCommand + " WHERE JournalDate <= '" + text2 + "' AND ISNULL(J.IsVoid,'False') = 'False'" + text3;
				if (allowposting)
				{
					textCommand += " AND ISNULL(J.ApprovalStatus,10)=10 ";
				}
				if (fromLocationID != "")
				{
					textCommand = textCommand + " AND SD.LocationID >='" + fromLocationID + "' AND SD.LocationID <='" + toLocationID + "'";
				}
				if (fromDivisionID != "")
				{
					textCommand = textCommand + " AND JD.DivisionID >='" + fromDivisionID + "' AND JD.DivisionID <='" + toDivisionID + "'";
				}
				textCommand += "GROUP BY JD.AccountID,AccountName,AC.Alias, TypeID,AC.GroupID";
				FillDataSet(dataSet, "Accounts", textCommand);
				dataSet.Tables["Accounts"].PrimaryKey = new DataColumn[1]
				{
					dataSet.Tables["Accounts"].Columns["AccountID"]
				};
				dataSet2.Merge(dataSet.Tables["Accounts"]);
				dataSet = new DataSet();
				textCommand = " Select JD.AccountID, \r\n                    SUM(Debit)AS TransactionDebit,\r\n                    SUM(Credit)AS TransactionCredit\r\n\r\n                    FROM Journal_Details JD \r\n                    INNER JOIN Journal J ON J.JournalID = JD.JournalID AND J.SysDocID=JD.SysDocID AND J.VoucherID = JD.VoucherID";
				if (fromLocationID != "")
				{
					textCommand += " LEFT JOIN System_Document SD ON SD.SysDocID=JD.SysDocID";
				}
				textCommand = textCommand + " WHERE JournalDate >= '" + text + "' AND JournalDate <= '" + text2 + "'  AND ISNULL(J.IsVoid,'False') = 'False'" + text4;
				if (allowposting)
				{
					textCommand += " AND ISNULL(J.ApprovalStatus,10)=10 ";
				}
				if (fromLocationID != "")
				{
					textCommand = textCommand + " AND SD.LocationID >='" + fromLocationID + "' AND SD.LocationID <='" + toLocationID + "'";
				}
				if (fromDivisionID != "")
				{
					textCommand = textCommand + " AND JD.DivisionID >='" + fromDivisionID + "' AND JD.DivisionID <='" + toDivisionID + "'";
				}
				textCommand += " GROUP BY JD.AccountID ";
				FillDataSet(dataSet, "TB", textCommand);
				dataSet.Tables["TB"].PrimaryKey = new DataColumn[1]
				{
					dataSet.Tables[0].Columns["AccountID"]
				};
				dataSet2.Tables[3].Merge(dataSet.Tables[0]);
				dataSet = new DataSet();
				textCommand = " Select JD.AccountID, \r\n                    CASE  WHEN SUM(ISNULL(Debit,0))-SUM(ISNULL(Credit,0)) > 0 THEN SUM(ISNULL(Debit,0))-SUM(ISNULL(Credit,0)) ELSE NULL END AS OpeningDebit,\r\n                    CASE  WHEN SUM(ISNULL(Debit,0))-SUM(ISNULL(Credit,0)) < 0 THEN SUM(ISNULL(Credit,0))-SUM(ISNULL(Debit,0)) ELSE NULL END AS OpeningCredit\r\n                    FROM Journal_Details JD \r\n                    INNER JOIN Journal J ON J.JournalID = JD.JournalID AND   J.SysDocID=JD.SysDocID AND J.VoucherID = JD.VoucherID";
				if (fromLocationID != "")
				{
					textCommand += " LEFT JOIN System_Document SD ON SD.SysDocID=JD.SysDocID";
				}
				textCommand = textCommand + " WHERE JournalDate < '" + text + "'  AND ISNULL(J.IsVoid,'False') = 'False'" + text5;
				if (allowposting)
				{
					textCommand += " AND ISNULL(J.ApprovalStatus,10)=10 ";
				}
				if (fromLocationID != "")
				{
					textCommand = textCommand + " AND SD.LocationID >='" + fromLocationID + "' AND SD.LocationID <='" + toLocationID + "'";
				}
				if (fromDivisionID != "")
				{
					textCommand = textCommand + " AND JD.DivisionID >='" + fromDivisionID + "' AND JD.DivisionID <='" + toDivisionID + "'";
				}
				textCommand += " GROUP BY JD.AccountID ";
				FillDataSet(dataSet, "TB", textCommand);
				dataSet.Tables["TB"].PrimaryKey = new DataColumn[1]
				{
					dataSet.Tables[0].Columns["AccountID"]
				};
				dataSet2.Tables[3].Merge(dataSet.Tables[0]);
				foreach (DataRow row in dataSet2.Tables["Asset"].Rows)
				{
					string str = row["GroupID"].ToString();
					DataRow[] array = dataSet2.Tables["Accounts"].Select("GroupID='" + str + "'");
					decimal num2 = default(decimal);
					for (int i = 0; i < array.Length; i++)
					{
						num2 += decimal.Parse(array[i]["Balance"].ToString());
					}
					row["Balance"] = num2;
				}
				foreach (DataRow row2 in dataSet2.Tables["Liability"].Rows)
				{
					string str2 = row2["GroupID"].ToString();
					DataRow[] array2 = dataSet2.Tables["Accounts"].Select("GroupID='" + str2 + "'");
					decimal num3 = default(decimal);
					for (int j = 0; j < array2.Length; j++)
					{
						num3 += decimal.Parse(array2[j]["Balance"].ToString());
					}
					row2["Balance"] = num3;
				}
				foreach (DataRow row3 in dataSet2.Tables["Equity"].Rows)
				{
					string str3 = row3["GroupID"].ToString();
					DataRow[] array3 = dataSet2.Tables["Accounts"].Select("GroupID='" + str3 + "'");
					decimal num4 = default(decimal);
					if (array3.Length != 0)
					{
						for (int k = 0; k < array3.Length; k++)
						{
							num4 += decimal.Parse(array3[k]["Balance"].ToString());
						}
						row3["Balance"] = num4;
					}
				}
				foreach (DataRow row4 in dataSet2.Tables["Asset"].Rows)
				{
					string groupID = row4["GroupID"].ToString();
					decimal num5 = default(decimal);
					if (row4["Balance"] != DBNull.Value)
					{
						num5 = decimal.Parse(row4["Balance"].ToString());
					}
					num5 += GetGroupBalance(dataSet2, "Asset", groupID);
					row4["Balance"] = num5;
				}
				foreach (DataRow row5 in dataSet2.Tables["Liability"].Rows)
				{
					string groupID2 = row5["GroupID"].ToString();
					decimal num6 = default(decimal);
					if (row5["Balance"] != DBNull.Value)
					{
						num6 = decimal.Parse(row5["Balance"].ToString());
					}
					num6 += GetGroupBalance(dataSet2, "Liability", groupID2);
					row5["Balance"] = num6;
				}
				foreach (DataRow row6 in dataSet2.Tables["Equity"].Rows)
				{
					string groupID3 = row6["GroupID"].ToString();
					decimal num7 = default(decimal);
					if (row6["Balance"] != DBNull.Value)
					{
						num7 = decimal.Parse(row6["Balance"].ToString());
					}
					num7 += GetGroupBalance(dataSet2, "Equity", groupID3);
					row6["Balance"] = num7;
				}
				if (!flag)
				{
					dataSet2.Tables["Accounts"].Rows.Clear();
				}
				for (int num8 = 6; num8 >= num; num8--)
				{
					for (int l = 0; l < dataSet2.Tables["Asset"].Rows.Count; l++)
					{
						if (dataSet2.Tables["Asset"].Rows[l]["L"].ToString() == num8.ToString())
						{
							dataSet2.Tables["Asset"].Rows.RemoveAt(l);
							l--;
						}
					}
					for (int m = 0; m < dataSet2.Tables["Liability"].Rows.Count; m++)
					{
						if (dataSet2.Tables["Liability"].Rows[m]["L"].ToString() == num8.ToString())
						{
							dataSet2.Tables["Liability"].Rows.RemoveAt(m);
							m--;
						}
					}
					for (int n = 0; n < dataSet2.Tables["Equity"].Rows.Count; n++)
					{
						if (dataSet2.Tables["Equity"].Rows[n]["L"].ToString() == num8.ToString())
						{
							dataSet2.Tables["Equity"].Rows.RemoveAt(n);
							n--;
						}
					}
				}
				DataSet dataSet3 = new DataSet();
				dataSet3 = GetTrialBalanceReport(from, to, showZeroBalance, allowposting);
				dataSet2.Merge(dataSet3);
				return dataSet2;
			}
			catch
			{
				throw;
			}
		}

		public DataSet GetTrialBalanceReport(DateTime from, DateTime to, bool showZeroBalance, bool allowposting)
		{
			try
			{
				string text = CommonLib.ToSqlDateTimeString(from);
				string text2 = CommonLib.ToSqlDateTimeString(to);
				string str = "Select JD.AccountID, AccountName,\r\n                        CASE  WHEN SUM(ISNULL(Debit,0))-SUM(ISNULL(Credit,0)) > 0 THEN SUM(ISNULL(Debit,0))-SUM(ISNULL(Credit,0)) ELSE NULL END AS Debit,\r\n                        CASE  WHEN SUM(ISNULL(Debit,0))-SUM(ISNULL(Credit,0)) < 0 THEN SUM(ISNULL(Credit,0))-SUM(ISNULL(Debit,0)) ELSE NULL END AS Credit\r\n                        FROM  Account AC (nolock) INNER JOIN Journal_Details JD (nolock) ON JD.AccountID=AC.AccountID\r\n                        INNER JOIN Journal J (nolock) ON J.JournalID = JD.JournalID AND J.SysDocID=JD.SysDocID AND J.VoucherID = JD.VoucherID\r\n                        WHERE JournalDate <= '" + text2 + "' AND ISNULL(J.IsVoid,'False') = 'False'";
				if (allowposting)
				{
					str += " AND ISNULL(J.ApprovalStatus,10)=10 ";
				}
				str += "GROUP BY JD.AccountID,AccountName";
				DataSet dataSet = new DataSet();
				FillDataSet(dataSet, "TB", str);
				dataSet.Tables["TB"].PrimaryKey = new DataColumn[1]
				{
					dataSet.Tables[0].Columns["AccountID"]
				};
				DataSet dataSet2 = new DataSet();
				str = " Select JD.AccountID, \r\n                    SUM(Debit)AS TransactionDebit,\r\n                    SUM(Credit)AS TransactionCredit\r\n\r\n                    FROM Journal_Details JD \r\n                    INNER JOIN Journal J ON J.JournalID = JD.JournalID AND J.SysDocID=JD.SysDocID AND J.VoucherID = JD.VoucherID\r\n                    WHERE JournalDate >= '" + text + "' AND JournalDate <= '" + text2 + "'  AND ISNULL(J.IsVoid,'False') = 'False'";
				if (allowposting)
				{
					str += " AND ISNULL(J.ApprovalStatus,10)=10 ";
				}
				str += " GROUP BY JD.AccountID ";
				FillDataSet(dataSet2, "TB", str);
				dataSet2.Tables["TB"].PrimaryKey = new DataColumn[1]
				{
					dataSet2.Tables[0].Columns["AccountID"]
				};
				dataSet.Tables[0].Merge(dataSet2.Tables[0]);
				dataSet2 = new DataSet();
				str = " Select JD.AccountID, \r\n                    CASE  WHEN SUM(ISNULL(Debit,0))-SUM(ISNULL(Credit,0)) > 0 THEN SUM(ISNULL(Debit,0))-SUM(ISNULL(Credit,0)) ELSE NULL END AS OpeningDebit,\r\n                    CASE  WHEN SUM(ISNULL(Debit,0))-SUM(ISNULL(Credit,0)) < 0 THEN SUM(ISNULL(Credit,0))-SUM(ISNULL(Debit,0)) ELSE NULL END AS OpeningCredit\r\n\r\n                    FROM Journal_Details JD \r\n                    INNER JOIN Journal J ON J.JournalID = JD.JournalID AND   J.SysDocID=JD.SysDocID AND J.VoucherID = JD.VoucherID\r\n                    WHERE JournalDate < '" + text + "'  AND ISNULL(J.IsVoid,'False') = 'False'";
				if (allowposting)
				{
					str += " AND ISNULL(J.ApprovalStatus,10)=10 ";
				}
				str += " GROUP BY JD.AccountID ";
				FillDataSet(dataSet2, "TB", str);
				dataSet2.Tables["TB"].PrimaryKey = new DataColumn[1]
				{
					dataSet2.Tables[0].Columns["AccountID"]
				};
				dataSet.Tables[0].Merge(dataSet2.Tables[0]);
				return dataSet;
			}
			catch
			{
				throw;
			}
		}

		public DataSet GetProfitAndLossReportSummaryRevised(DateTime from, DateTime to, string fromLocationID, string toLocationID, string fromDivisionID, string toDivisionID, int level, bool allowposting)
		{
			string text = CommonLib.ToSqlDateTimeString(to);
			string text2 = CommonLib.ToSqlDateTimeString(from);
			DataSet dataSet = new DataSet();
			bool flag = true;
			string text3 = "Select A.AccountID,AccountName,TypeID,\r\n                            SUM(ISNULL(JD.Credit,0))- SUM(ISNULL(JD.Debit,0))  AS Balance\r\n                            FROM Account  A\r\n                            INNER JOIN Account_Group AG ON A.GroupID=AG.GroupID\r\n                            INNER JOIN Journal_Details JD ON JD.AccountID=A.AccountID\r\n                            INNER JOIN Journal J ON J.SysDocID=JD.SysDocID AND J.VoucherID = JD.VoucherID";
			if (fromLocationID != "")
			{
				text3 += " LEFT JOIN System_Document SD ON SD.SysDocID=JD.SysDocID";
			}
			text3 = text3 + " WHERE TypeID IN (3) AND  SubType <>1 AND ISNULL(JD.IsVoid,'False')='False' AND  ISNULL(JD.Reference,'')<>'SYS_YEND' \r\n                            AND JournalDate BETWEEN '" + text2 + "' AND '" + text + "'";
			if (allowposting)
			{
				text3 += " AND ISNULL(J.ApprovalStatus,10)=10 ";
			}
			if (fromLocationID != "")
			{
				text3 = text3 + " AND SD.LocationID >='" + fromLocationID + "' AND SD.LocationID <='" + toLocationID + "'";
			}
			if (fromDivisionID != "")
			{
				text3 = text3 + " AND JD.DivisionID >='" + fromDivisionID + "' AND JD.DivisionID <='" + toDivisionID + "'";
			}
			text3 += "GROUP BY A.AccountID,AccountName,TypeID";
			DataSet dataSet2 = new DataSet();
			FillDataSet(dataSet2, "Income", text3);
			dataSet2.Tables[0].PrimaryKey = new DataColumn[1]
			{
				dataSet2.Tables["Income"].Columns["AccountID"]
			};
			dataSet = new DataSet();
			text3 = "Select A.AccountID,AccountName,TypeID,\r\n                            SUM(ISNULL(JD.Credit,0))- SUM(ISNULL(JD.Debit,0))  AS Balance\r\n                            FROM Account  A\r\n                            INNER JOIN Account_Group AG ON A.GroupID=AG.GroupID\r\n                            INNER JOIN Journal_Details JD ON JD.AccountID=A.AccountID\r\n                            INNER JOIN Journal J ON J.SysDocID=JD.SysDocID AND J.VoucherID = JD.VoucherID";
			if (fromLocationID != "")
			{
				text3 += " LEFT JOIN System_Document SD ON SD.SysDocID=JD.SysDocID";
			}
			text3 = text3 + "  WHERE TypeID IN (3)AND SubType=1 AND ISNULL(JD.IsVoid,'False')='False' AND  ISNULL(JD.Reference,'')<>'SYS_YEND'\r\n                            AND JournalDate BETWEEN '" + text2 + "' AND '" + text + "'";
			if (allowposting)
			{
				text3 += " AND ISNULL(J.ApprovalStatus, 1)= 10 ";
			}
			if (fromLocationID != "")
			{
				text3 = text3 + " AND SD.LocationID >='" + fromLocationID + "' AND SD.LocationID <='" + toLocationID + "'";
			}
			if (fromDivisionID != "")
			{
				text3 = text3 + " AND JD.DivisionID >='" + fromDivisionID + "' AND JD.DivisionID <='" + toDivisionID + "'";
			}
			text3 += "GROUP BY A.AccountID,AccountName,TypeID";
			FillDataSet(dataSet, "OtherIncome", text3);
			dataSet.Tables[0].PrimaryKey = new DataColumn[1]
			{
				dataSet.Tables["OtherIncome"].Columns["AccountID"]
			};
			dataSet2.Merge(dataSet.Tables[0]);
			dataSet = new DataSet();
			text3 = "Select A.AccountID,AccountName,TypeID,\r\n                            SUM(ISNULL(JD.Debit,0))- SUM(ISNULL(JD.Credit,0))  AS Balance\r\n                            FROM Account  A\r\n                            INNER JOIN Account_Group AG ON A.GroupID=AG.GroupID\r\n                            INNER JOIN Journal_Details JD ON JD.AccountID=A.AccountID\r\n                            INNER JOIN Journal J ON J.SysDocID=JD.SysDocID AND J.VoucherID = JD.VoucherID";
			if (fromLocationID != "")
			{
				text3 += " LEFT JOIN System_Document SD ON SD.SysDocID=JD.SysDocID";
			}
			text3 = text3 + " WHERE TypeID IN (4)AND SubType=8 AND ISNULL(JD.IsVoid,'False')='False' AND  ISNULL(JD.Reference,'')<>'SYS_YEND'\r\n                            AND JournalDate BETWEEN '" + text2 + "' AND '" + text + "'";
			if (allowposting)
			{
				text3 += " AND ISNULL(J.ApprovalStatus,10)=10 ";
			}
			if (fromLocationID != "")
			{
				text3 = text3 + " AND SD.LocationID >='" + fromLocationID + "' AND SD.LocationID <='" + toLocationID + "'";
			}
			if (fromDivisionID != "")
			{
				text3 = text3 + " AND JD.DivisionID >='" + fromDivisionID + "' AND JD.DivisionID <='" + toDivisionID + "'";
			}
			text3 += "GROUP BY A.AccountID,AccountName,TypeID";
			FillDataSet(dataSet, "COGS", text3);
			dataSet.Tables[0].PrimaryKey = new DataColumn[1]
			{
				dataSet.Tables["COGS"].Columns["AccountID"]
			};
			dataSet2.Merge(dataSet.Tables[0]);
			dataSet = new DataSet();
			text3 = "WITH ACCGroups \r\n                        AS\r\n                        (\r\n\t                        SELECT AG.GroupID, GroupName,TypeID,SubType,ParentID ,0 AS L,0.0 AS Balance FROM Account_Group AG\r\n                            LEFT JOIN Account A ON A.GroupID=AG.GroupID\r\n                            WHERE ParentID IS NULL AND TypeID=4 --AND SubType<>8\r\n                            UNION ALL\r\n                            SELECT AG.GroupID, AG.GroupName,AG.TypeID,A.SubType,AG.ParentID,L+1 AS L,0.0 AS Balance\r\n                            FROM Account_Group AG\r\n                            INNER JOIN Account A ON A.GroupID=AG.GroupID\r\n                            INNER JOIN ACCGroups AS d\r\n                            ON AG.ParentID = d.GroupID\r\n                        )\r\n\r\n                        SELECT DISTINCT  GroupID, GroupName,TypeID,SubType,ParentID ,L , Balance FROM ACCGroups";
			FillDataSet(dataSet, "Expenses", text3);
			dataSet.Tables["Expenses"].DefaultView.RowFilter = "ISNULL(SubType,0)<>8 ";
			DataTable dataTable = new DataTable();
			dataTable = dataSet.Tables["Expenses"].DefaultView.ToTable();
			if (dataSet.Tables.Contains("Expenses"))
			{
				dataSet.Tables.Remove("Expenses");
				dataSet.Merge(dataTable);
			}
			dataSet.Tables["Expenses"].PrimaryKey = new DataColumn[1]
			{
				dataSet.Tables["Expenses"].Columns["GroupID"]
			};
			dataSet2.Merge(dataSet.Tables["Expenses"]);
			dataSet = new DataSet();
			text3 = "Select A.AccountID,AccountName,A.GroupID,TypeID,A.Alias,\r\n                        CASE WHEN TypeID=1 THEN -1 * (SUM(ISNULL(JD.Debit,0))- SUM(ISNULL(JD.Credit,0)))\r\n\t\t\t\t\t\tELSE SUM(ISNULL(JD.Debit,0))- SUM(ISNULL(JD.Credit,0)) END AS Balance\r\n                        FROM Account  A\r\n                        INNER JOIN Account_Group AG ON A.GroupID=AG.GroupID\r\n                        INNER JOIN Journal_Details JD ON JD.AccountID=A.AccountID\r\n                        INNER JOIN Journal J ON J.SysDocID=JD.SysDocID AND J.VoucherID = JD.VoucherID";
			if (fromLocationID != "")
			{
				text3 += " LEFT JOIN System_Document SD ON SD.SysDocID=JD.SysDocID ";
			}
			text3 = text3 + " WHERE ISNULL(JD.IsVoid,'False')='False'\r\n                            AND JournalDate Between '" + text2 + "' AND '" + text + "'\r\n                        AND TypeID IN (4) AND ISNULL(SubType,0)<>8";
			if (allowposting)
			{
				text3 += " AND ISNULL(J.ApprovalStatus,10)=10 ";
			}
			if (fromLocationID != "")
			{
				text3 = text3 + "AND SD.LocationID >='" + fromLocationID + "' AND SD.LocationID <='" + toLocationID + "'";
			}
			if (fromDivisionID != "")
			{
				text3 = text3 + " AND JD.DivisionID >='" + fromDivisionID + "' AND JD.DivisionID <='" + toDivisionID + "'";
			}
			text3 += " GROUP BY A.AccountID,AccountName,TypeID,A.Alias,A.GroupID";
			FillDataSet(dataSet, "Accounts", text3);
			dataSet.Tables["Accounts"].PrimaryKey = new DataColumn[1]
			{
				dataSet.Tables["Accounts"].Columns["AccountID"]
			};
			dataSet2.Merge(dataSet.Tables["Accounts"]);
			foreach (DataRow row in dataSet2.Tables["Expenses"].Rows)
			{
				string str = row["GroupID"].ToString();
				DataRow[] array = dataSet2.Tables["Accounts"].Select("GroupID='" + str + "'");
				decimal num = default(decimal);
				if (array.Length != 0)
				{
					for (int i = 0; i < array.Length; i++)
					{
						num += decimal.Parse(array[i]["Balance"].ToString());
					}
					row["Balance"] = num;
				}
			}
			foreach (DataRow row2 in dataSet2.Tables["Expenses"].Rows)
			{
				string groupID = row2["GroupID"].ToString();
				decimal num2 = default(decimal);
				if (row2["Balance"] != DBNull.Value)
				{
					num2 = decimal.Parse(row2["Balance"].ToString());
				}
				num2 += GetGroupBalance(dataSet2, "Expenses", groupID);
				row2["Balance"] = num2;
			}
			for (int num3 = 6; num3 >= level; num3--)
			{
				for (int j = 0; j < dataSet2.Tables["Expenses"].Rows.Count; j++)
				{
					if (dataSet2.Tables["Expenses"].Rows[j]["L"].ToString() == num3.ToString())
					{
						dataSet2.Tables["Expenses"].Rows.RemoveAt(j);
						j--;
					}
				}
			}
			dataSet2.Tables.Add("Summary");
			dataSet2.Tables["Summary"].Columns.Add("GrossProfit", typeof(decimal));
			dataSet2.Tables["Summary"].Columns.Add("NetIncome", typeof(decimal));
			dataSet2.Tables["Summary"].Columns.Add("OtherIncome", typeof(decimal));
			dataSet2.Tables["Summary"].Columns.Add("TotalIncome", typeof(decimal));
			dataSet2.Tables["Summary"].Columns.Add("TotalExpense", typeof(decimal));
			decimal num4 = default(decimal);
			decimal num5 = default(decimal);
			decimal num6 = default(decimal);
			decimal num7 = default(decimal);
			decimal num8 = default(decimal);
			foreach (DataRow row3 in dataSet2.Tables["Income"].Rows)
			{
				decimal result = default(decimal);
				decimal.TryParse(row3["Balance"].ToString(), out result);
				num4 += result;
			}
			foreach (DataRow row4 in dataSet2.Tables["OtherIncome"].Rows)
			{
				decimal result2 = default(decimal);
				decimal.TryParse(row4["Balance"].ToString(), out result2);
				num6 += result2;
			}
			foreach (DataRow row5 in dataSet2.Tables["COGS"].Rows)
			{
				decimal result3 = default(decimal);
				decimal.TryParse(row5["Balance"].ToString(), out result3);
				num4 -= result3;
			}
			num7 = num4 + num6;
			num5 = num4 + num6;
			foreach (DataRow row6 in dataSet2.Tables["Expenses"].Rows)
			{
				decimal result4 = default(decimal);
				decimal.TryParse(row6["Balance"].ToString(), out result4);
				num8 += result4;
				num5 -= result4;
			}
			dataSet2.Tables["Summary"].Rows.Add(num4, num5, num6, num7, num8);
			if (flag)
			{
				dataSet2.Tables["Accounts"].Rows.Clear();
			}
			return dataSet2;
		}

		public DataSet GetTrialBalanceReport(DateTime from, DateTime to, string fromLocationID, string toLocationID, string fromDivisionID, string toDivisionID, bool showZeroBalance, bool allowposting)
		{
			try
			{
				string text = CommonLib.ToSqlDateTimeString(from);
				string text2 = CommonLib.ToSqlDateTimeString(to);
				string text3 = " AND JD.JournalID NOT IN (SELECT JD1.JournalID FROM Journal J1 INNER JOIN Journal_Details JD1 ON J1.JournalID=JD1.JournalID WHERE JournalDate = '" + text2 + "'\r\n                            AND ISNULL(JD1.Reference, '')= 'SYS_YEND')";
				string text4 = " AND JD.JournalID NOT IN (SELECT JD1.JournalID FROM Journal J1 INNER JOIN Journal_Details JD1 ON J1.JournalID=JD1.JournalID WHERE JournalDate <=  '" + text2 + "'\r\n                            AND ISNULL(JD1.Reference, '')= 'SYS_YEND')";
				string text5 = " AND JD.JournalID NOT IN (SELECT JD1.JournalID FROM Journal J1 INNER JOIN Journal_Details JD1 ON J1.JournalID=JD1.JournalID WHERE JournalDate ='" + text2 + "'\r\n                            AND ISNULL(JD1.Reference, '')= 'SYS_YEND')";
				string text6 = "Select JD.AccountID, AccountName,AC.Alias,\r\n                        CASE  WHEN SUM(ISNULL(Debit,0))-SUM(ISNULL(Credit,0)) > 0 THEN SUM(ISNULL(Debit,0))-SUM(ISNULL(Credit,0)) ELSE NULL END AS Debit,\r\n                        CASE  WHEN SUM(ISNULL(Debit,0))-SUM(ISNULL(Credit,0)) < 0 THEN SUM(ISNULL(Credit,0))-SUM(ISNULL(Debit,0)) ELSE NULL END AS Credit\r\n                        FROM  Account AC (nolock) INNER JOIN Journal_Details JD (nolock) ON JD.AccountID=AC.AccountID\r\n                        INNER JOIN Journal J (nolock) ON J.JournalID = JD.JournalID AND J.SysDocID=JD.SysDocID AND J.VoucherID = JD.VoucherID";
				if (fromLocationID != "")
				{
					text6 += " LEFT JOIN System_Document SD ON SD.SysDocID=JD.SysDocID";
				}
				text6 = text6 + " WHERE JournalDate <= '" + text2 + "' AND ISNULL(J.IsVoid,'False') = 'False' " + text3;
				if (fromLocationID != "")
				{
					text6 = text6 + " AND SD.LocationID >='" + fromLocationID + "' AND SD.LocationID <='" + toLocationID + "'";
				}
				if (fromDivisionID != "")
				{
					text6 = text6 + " AND JD.DivisionID >='" + fromDivisionID + "' AND JD.DivisionID <='" + toDivisionID + "'";
				}
				if (allowposting)
				{
					text6 += " AND ISNULL(J.ApprovalStatus,10)=10 ";
				}
				text6 += "GROUP BY JD.AccountID,AccountName, AC.Alias";
				DataSet dataSet = new DataSet();
				FillDataSet(dataSet, "TB", text6);
				dataSet.Tables["TB"].PrimaryKey = new DataColumn[1]
				{
					dataSet.Tables[0].Columns["AccountID"]
				};
				DataSet dataSet2 = new DataSet();
				text6 = " Select JD.AccountID, \r\n                    SUM(Debit)AS TransactionDebit,\r\n                    SUM(Credit)AS TransactionCredit\r\n                    FROM Journal_Details JD \r\n                    INNER JOIN Journal J ON J.JournalID = JD.JournalID AND J.SysDocID=JD.SysDocID AND J.VoucherID = JD.VoucherID";
				if (fromLocationID != "")
				{
					text6 += " LEFT JOIN System_Document SD ON SD.SysDocID=JD.SysDocID";
				}
				text6 = text6 + " WHERE JournalDate >= '" + text + "' AND JournalDate <= '" + text2 + "'  AND ISNULL(J.IsVoid,'False') = 'False'" + text4;
				if (fromLocationID != "")
				{
					text6 = text6 + " AND SD.LocationID >='" + fromLocationID + "' AND SD.LocationID <='" + toLocationID + "'";
				}
				if (fromDivisionID != "")
				{
					text6 = text6 + " AND JD.DivisionID >='" + fromDivisionID + "' AND JD.DivisionID <='" + toDivisionID + "'";
				}
				if (allowposting)
				{
					text6 += " AND ISNULL(J.ApprovalStatus,10)=10 ";
				}
				text6 += " GROUP BY JD.AccountID ";
				FillDataSet(dataSet2, "TB", text6);
				dataSet2.Tables["TB"].PrimaryKey = new DataColumn[1]
				{
					dataSet2.Tables[0].Columns["AccountID"]
				};
				dataSet.Tables[0].Merge(dataSet2.Tables[0]);
				dataSet2 = new DataSet();
				text6 = " Select JD.AccountID, \r\n                    CASE  WHEN SUM(ISNULL(Debit,0))-SUM(ISNULL(Credit,0)) > 0 THEN SUM(ISNULL(Debit,0))-SUM(ISNULL(Credit,0)) ELSE NULL END AS OpeningDebit,\r\n                    CASE  WHEN SUM(ISNULL(Debit,0))-SUM(ISNULL(Credit,0)) < 0 THEN SUM(ISNULL(Credit,0))-SUM(ISNULL(Debit,0)) ELSE NULL END AS OpeningCredit\r\n\r\n                    FROM Journal_Details JD \r\n                    INNER JOIN Journal J ON J.JournalID = JD.JournalID AND   J.SysDocID=JD.SysDocID AND J.VoucherID = JD.VoucherID";
				if (fromLocationID != "")
				{
					text6 += " LEFT JOIN System_Document SD ON SD.SysDocID=JD.SysDocID";
				}
				text6 = text6 + " WHERE JournalDate < '" + text + "'  AND ISNULL(J.IsVoid,'False') = 'False' " + text5;
				if (allowposting)
				{
					text6 += " AND ISNULL(J.ApprovalStatus,10)=10 ";
				}
				if (fromLocationID != "")
				{
					text6 = text6 + " AND SD.LocationID >='" + fromLocationID + "' AND SD.LocationID <='" + toLocationID + "'";
				}
				if (fromDivisionID != "")
				{
					text6 = text6 + " AND JD.DivisionID >='" + fromDivisionID + "' AND JD.DivisionID <='" + toDivisionID + "'";
				}
				text6 += " GROUP BY JD.AccountID ";
				FillDataSet(dataSet2, "TB", text6);
				dataSet2.Tables["TB"].PrimaryKey = new DataColumn[1]
				{
					dataSet2.Tables[0].Columns["AccountID"]
				};
				dataSet.Tables[0].Merge(dataSet2.Tables[0]);
				return dataSet;
			}
			catch
			{
				throw;
			}
		}

		public DataSet GetTrialBalanceReportConsolidated(DateTime from, DateTime to, string fromLocationID, string toLocationID, string fromDivisionID, string toDivisionID, bool showZeroBalance, bool allowposting)
		{
			try
			{
				string text = CommonLib.ToSqlDateTimeString(from);
				string text2 = CommonLib.ToSqlDateTimeString(to);
				string text3 = " AND JD.JournalID NOT IN (SELECT JD1.JournalID FROM Journal J1 INNER JOIN Journal_Details JD1 ON J1.JournalID=JD1.JournalID WHERE JournalDate = '" + text2 + "'\r\n                            AND ISNULL(JD1.Reference, '')= 'SYS_YEND')";
				string text4 = " AND JD.JournalID NOT IN (SELECT JD1.JournalID FROM Journal J1 INNER JOIN Journal_Details JD1 ON J1.JournalID=JD1.JournalID WHERE JournalDate <=  '" + text2 + "'\r\n                            AND ISNULL(JD1.Reference, '')= 'SYS_YEND')";
				string text5 = " AND JD.JournalID NOT IN (SELECT JD1.JournalID FROM Journal J1 INNER JOIN Journal_Details JD1 ON J1.JournalID=JD1.JournalID WHERE JournalDate ='" + text2 + "'\r\n                            AND ISNULL(JD1.Reference, '')= 'SYS_YEND')";
				string text6 = "Select AC.GroupID, GroupName,\r\n                        CASE  WHEN SUM(ISNULL(Debit,0))-SUM(ISNULL(Credit,0)) > 0 THEN SUM(ISNULL(Debit,0))-SUM(ISNULL(Credit,0)) ELSE NULL END AS Debit,\r\n                        CASE  WHEN SUM(ISNULL(Debit,0))-SUM(ISNULL(Credit,0)) < 0 THEN SUM(ISNULL(Credit,0))-SUM(ISNULL(Debit,0)) ELSE NULL END AS Credit\r\n                        FROM  Account AC (nolock) \r\n                        INNER JOIN Account_Group AG (nolock) ON AG.GroupID=AC.GroupID\r\n                        INNER JOIN Journal_Details JD (nolock) ON JD.AccountID=AC.AccountID\r\n                        INNER JOIN Journal J (nolock) ON J.JournalID = JD.JournalID AND J.SysDocID=JD.SysDocID AND J.VoucherID = JD.VoucherID";
				if (fromLocationID != "" || fromDivisionID != "")
				{
					text6 += " LEFT JOIN System_Document SD ON SD.SysDocID=JD.SysDocID";
				}
				text6 = text6 + " WHERE JournalDate <= '" + text2 + "' AND ISNULL(J.IsVoid,'False') = 'False'" + text3;
				if (fromLocationID != "")
				{
					text6 = text6 + " AND SD.LocationID >='" + fromLocationID + "' AND SD.LocationID <='" + toLocationID + "'";
				}
				if (fromDivisionID != "")
				{
					text6 = text6 + " AND JD.DivisionID >='" + fromDivisionID + "' AND JD.DivisionID <='" + toDivisionID + "'";
				}
				if (allowposting)
				{
					text6 += " AND ISNULL(J.ApprovalStatus,10)=10 ";
				}
				text6 += "GROUP BY AC.GroupID, GroupName ORDER BY GroupID";
				DataSet dataSet = new DataSet();
				FillDataSet(dataSet, "TB", text6);
				dataSet.Tables["TB"].PrimaryKey = new DataColumn[1]
				{
					dataSet.Tables[0].Columns["GroupID"]
				};
				DataSet dataSet2 = new DataSet();
				text6 = " Select AC.GroupID, GroupName, \r\n                    SUM(Debit)AS TransactionDebit,\r\n                    SUM(Credit)AS TransactionCredit\r\n                    FROM Journal_Details JD \r\n                    INNER JOIN Journal J ON J.JournalID = JD.JournalID AND J.SysDocID=JD.SysDocID AND J.VoucherID = JD.VoucherID\r\n                    INNER JOIN Account AC ON AC.AccountID=JD.AccountID \r\n                    INNER JOIN Account_Group AG (nolock) ON AG.GroupID=AC.GroupID  ";
				if (fromLocationID != "" || fromDivisionID != "")
				{
					text6 += " LEFT JOIN System_Document SD ON SD.SysDocID=JD.SysDocID";
				}
				text6 = text6 + " WHERE JournalDate >= '" + text + "' AND JournalDate <= '" + text2 + "'  AND ISNULL(J.IsVoid,'False') = 'False'" + text4;
				if (fromLocationID != "")
				{
					text6 = text6 + " AND SD.LocationID >='" + fromLocationID + "' AND SD.LocationID <='" + toLocationID + "'";
				}
				if (fromDivisionID != "")
				{
					text6 = text6 + " AND JD.DivisionID >='" + fromDivisionID + "' AND JD.DivisionID <='" + toDivisionID + "'";
				}
				if (allowposting)
				{
					text6 += " AND ISNULL(J.ApprovalStatus,10)=10 ";
				}
				text6 += " GROUP BY AC.GroupID, GroupName ORDER BY GroupID";
				FillDataSet(dataSet2, "TB", text6);
				dataSet2.Tables["TB"].PrimaryKey = new DataColumn[1]
				{
					dataSet2.Tables[0].Columns["GroupID"]
				};
				dataSet.Tables[0].Merge(dataSet2.Tables[0]);
				dataSet2 = new DataSet();
				text6 = " Select AC.GroupID,GroupName, \r\n                    CASE  WHEN SUM(ISNULL(Debit,0))-SUM(ISNULL(Credit,0)) > 0 THEN SUM(ISNULL(Debit,0))-SUM(ISNULL(Credit,0)) ELSE NULL END AS OpeningDebit,\r\n                    CASE  WHEN SUM(ISNULL(Debit,0))-SUM(ISNULL(Credit,0)) < 0 THEN SUM(ISNULL(Credit,0))-SUM(ISNULL(Debit,0)) ELSE NULL END AS OpeningCredit\r\n\r\n                    FROM Journal_Details JD \r\n                    INNER JOIN Journal J ON J.JournalID = JD.JournalID AND   J.SysDocID=JD.SysDocID AND J.VoucherID = JD.VoucherID\r\n                    INNER JOIN Account AC ON AC.AccountID=JD.AccountID\r\n                    INNER JOIN Account_Group AG (nolock) ON AG.GroupID=AC.GroupID\r\n                    ";
				if (fromLocationID != "" || fromDivisionID != "")
				{
					text6 += " LEFT JOIN System_Document SD ON SD.SysDocID=JD.SysDocID";
				}
				text6 = text6 + " WHERE JournalDate < '" + text + "'  AND ISNULL(J.IsVoid,'False') = 'False'" + text5;
				if (allowposting)
				{
					text6 += " AND ISNULL(J.ApprovalStatus,10)=10 ";
				}
				if (fromLocationID != "")
				{
					text6 = text6 + " AND SD.LocationID >='" + fromLocationID + "' AND SD.LocationID <='" + toLocationID + "'";
				}
				if (fromDivisionID != "")
				{
					text6 = text6 + " AND JD.DivisionID >='" + fromDivisionID + "' AND JD.DivisionID <='" + toDivisionID + "'";
				}
				text6 += " GROUP BY AC.GroupID,GroupName ORDER BY GroupID";
				FillDataSet(dataSet2, "TB", text6);
				dataSet2.Tables["TB"].PrimaryKey = new DataColumn[1]
				{
					dataSet2.Tables[0].Columns["GroupID"]
				};
				dataSet.Tables[0].Merge(dataSet2.Tables[0]);
				return dataSet;
			}
			catch
			{
				throw;
			}
		}

		public DataSet GetAccountTransactionsReport(DateTime from, DateTime to, string fromAccountID, string toAccountID, string fromLocationID, string toLocationID, string groupID, bool isFC, string costCenterID, bool allowposting, string fromJob, string toJob, string fromCostCategory, string toCostCategory, string fromProperty, string toProperty, string fromPropertyUnit, string toProprertyUnit, string fromAnalysis, string toAnalysis, string fromDivision, string toDivision)
		{
			string empty = string.Empty;
			string text = CommonLib.ToSqlDateTimeString(from);
			string text2 = CommonLib.ToSqlDateTimeString(to);
			new Currencies(base.DBConfig).GetBaseCurrencyID();
			string text3 = string.Empty;
			if (groupID != "")
			{
				empty = " WITH ACCGroups \r\n                                AS\r\n                                (\r\n\t                                SELECT GroupID, GroupName, TypeID, ParentID , 0 AS L FROM Account_Group AG\r\n\t                                WHERE  GroupID = '" + groupID + "'\r\n\r\n\t\t\t\t\t\t\t\t\tUNION ALL\r\n\t                                \r\n\t\t\t\t\t\t\t\t\tSELECT AG.GroupID, AG.GroupName, AG.TypeID, AG.ParentID, L + 1 AS L\r\n                                    FROM Account_Group AG\r\n\t\t\t\t\t\t\t\t\tINNER JOIN ACCGroups AS d\r\n                                        ON AG.ParentID = d.GroupID\r\n                                )\r\n\r\n                                SELECT GroupID FROM ACCGroups ";
				DataSet dataSet = new DataSet();
				FillDataSet(dataSet, "AccountGroup", empty);
				for (int i = 0; i < dataSet.Tables["AccountGroup"].Rows.Count; i++)
				{
					DataRow dataRow = dataSet.Tables["AccountGroup"].Rows[i];
					text3 = text3 + "'" + dataRow["GroupID"].ToString() + "'";
					if (i < dataSet.Tables["AccountGroup"].Rows.Count - 1)
					{
						text3 += ",";
					}
				}
			}
			string str = "SELECT JD.AccountID AS [Account Code],AccountName AS [Account Name],Account.Alias, ";
			str = (isFC ? (str + " ISNULL((SELECT SUM(ISNULL(DebitFC,0)- ISNULL(CreditFC,0)) ") : (str + " ISNULL((SELECT SUM(ISNULL(Debit,0)- ISNULL(Credit,0)) "));
			str += " FROM Journal_Details JD2 INNER JOIN \r\n                            Journal J2 ON J2.JournalID=JD2.JournalID";
			if (fromLocationID != "")
			{
				str += " LEFT JOIN System_Document SD ON SD.SysDocID=JD2.SysDocID";
			}
			str = str + " WHERE JD.AccountID=JD2.AccountID AND J2.JournalDate<'" + text + "' AND ISNULL(J2.IsVoid,'False')='False' ";
			if (fromLocationID != "")
			{
				str = str + "AND SD.LocationID >='" + fromLocationID + "' AND SD.LocationID <='" + toLocationID + "'";
			}
			if (fromDivision != "")
			{
				str = str + "AND JD2.DivisionID >='" + fromDivision + "' AND JD2.DivisionID <='" + toDivision + "'";
			}
			str += "),0) AS [Opening Balance], ";
			str = (isFC ? (str + " ISNULL((SELECT SUM(ISNULL(DebitFC,0)- ISNULL(CreditFC,0)) ") : (str + " ISNULL((SELECT SUM(ISNULL(Debit,0)- ISNULL(Credit,0)) "));
			str += " FROM Journal_Details JD2 INNER JOIN \r\n                    Journal J2 ON J2.JournalID=JD2.JournalID";
			if (fromLocationID != "" || fromDivision != "")
			{
				str += " LEFT JOIN System_Document SD ON SD.SysDocID=JD2.SysDocID ";
			}
			str = str + " WHERE JD.AccountID=JD2.AccountID AND \r\n                    J2.JournalDate<='" + text2 + "' AND ISNULL(J2.IsVoid,'False')='False' AND  JD2.JournalID NOT IN(SELECT JD1.JournalID \r\n                    FROM Journal J1 INNER JOIN Journal_Details JD1   \r\n                    ON J1.JournalID = JD1.JournalID WHERE JournalDate = '" + text2 + "'  AND ISNULL(JD1.Reference, '') = 'SYS_YEND') ";
			if (fromLocationID != "")
			{
				str = str + "AND SD.LocationID >='" + fromLocationID + "' AND SD.LocationID <='" + toLocationID + "'";
			}
			if (fromDivision != "")
			{
				str = str + "AND JD2.DivisionID >='" + fromDivision + "' AND JD2.DivisionID <='" + toDivision + "'";
			}
			str += "),0)AS [Ending Balance]\r\n                    FROM Journal_Details JD INNER JOIN Journal J ON J.SysDocID=JD.SysDocID AND J.VoucherID = JD.VoucherID INNER JOIN Account \r\n                    ON Account.AccountID= JD.AccountID LEFT OUTER JOIN System_Document SD ON SD.SysDocID=J.SysDocID WHERE 1=1 ";
			if (isFC)
			{
				str += " AND ISNULL(DebitFC,0) + ISNULL(CreditFC,0) <> 0 ";
			}
			if (fromAccountID != "")
			{
				str = str + " AND JD.AccountID>='" + fromAccountID + "'";
			}
			if (toAccountID != "")
			{
				str = str + " AND JD.AccountID <='" + toAccountID + "' ";
			}
			if (fromLocationID != "")
			{
				str = str + " AND (SD.LocationID>='" + fromLocationID + "' or JD.CostCenterID ='" + fromLocationID + "')";
			}
			if (toLocationID != "")
			{
				str = str + " AND ( SD.LocationID <='" + toLocationID + "'  or JD.CostCenterID ='" + toLocationID + "')";
			}
			if (fromDivision != "")
			{
				str = str + " AND (JD.DivisionID>='" + fromDivision + "' or JD.CostCenterID ='" + fromDivision + "')";
			}
			if (toDivision != "")
			{
				str = str + " AND ( JD.DivisionID <='" + toDivision + "'  or JD.CostCenterID ='" + toDivision + "')";
			}
			if (costCenterID != "")
			{
				str = str + " AND JD.CostCenterID ='" + costCenterID + "' ";
			}
			if (groupID != "")
			{
				str = str + " AND Account.GroupID IN (" + text3 + ") ";
			}
			if (fromJob != "")
			{
				str = str + " AND JD.JobID >='" + fromJob + "'";
			}
			if (toJob != "")
			{
				str = str + " AND JD.JobID<='" + toJob + "'";
			}
			if (fromCostCategory != "")
			{
				str = str + " AND JD.CostCategoryID >='" + fromCostCategory + "'";
			}
			if (toCostCategory != "")
			{
				str = str + " AND JD.CostCategoryID <='" + toCostCategory + "'";
			}
			if (fromProperty != "")
			{
				str = str + " AND JD.AttributeID1 >='" + fromProperty + "'";
			}
			if (toProperty != "")
			{
				str = str + " AND JD.AttributeID1 <='" + toProperty + "'";
			}
			if (fromPropertyUnit != "")
			{
				str = str + " AND JD.AttributeID2 >='" + fromPropertyUnit + "'";
			}
			if (toProprertyUnit != "")
			{
				str = str + " AND JD.AttributeID2 >='" + toProprertyUnit + "'";
			}
			if (allowposting)
			{
				str += " AND ISNULL(J.ApprovalStatus,10)=10 ";
			}
			str += " GROUP BY JD.AccountID,AccountName, Account.Alias";
			DataSet dataSet2 = new DataSet();
			FillDataSet(dataSet2, "GL", str);
			DataSet dataSet3 = new DataSet();
			str = " SELECT JournalDate AS [Journal Date],J.SysDocID AS [SysDocID],J.VoucherID AS [Number],J.SysDocType, ISNULL(JD.CurrencyID,J.CurrencyID) AS [Currency],JD.CheckNumber,\r\n                    ISNULL(JD.CheckDate,(SELECT TD.DueDate FROM BANK_FACILITY_TRANSACTION_DETAILS TD WHERE VoucherID=J.VoucherID AND SysDocID=J.SysDocID)) AS CheckDate,JD.CheckbookID,JD.CheckID,\r\n                    JD.AccountID [Account Code],JD.Reference,Description,CC.CostCenterID,CC.CostCenterName,\r\n                    ISNULL( (SELECT CustomerName FROM Sales_Invoice S INNER JOIN Customer C ON S.CustomerID=C.CustomerID WHERE S.SysDocID=J.SysDocID AND S.VoucherID=J.VoucherID), \r\n                    (SELECT VendorName FROM Purchase_Invoice P INNER JOIN Vendor V ON P.VendorID=V.VendorID WHERE P.SysDocID=J.SysDocID AND P.VoucherID=J.VoucherID)) AS [Party],\r\n                    ISNULL((SELECT SUBSTRING((SELECT  ',' + CI.ChequeNumber + '' FROM  Cheque_Issued CI WHERE CI.SysDocID = J.SysDocID AND CI.VoucherID = J.VoucherID  FOR XML PATH('')),2,20000)),\r\n                    (SELECT SUBSTRING((SELECT  ',' + CR.ChequeNumber + '' FROM  Cheque_Received CR WHERE CR.SysDocID = J.SysDocID AND CR.VoucherID = J.VoucherID  FOR XML PATH('')),2,20000))) AS [ChqueNumber],\r\n                    (SELECT SUBSTRING((SELECT  DISTINCT ',' +  TD.Description + ''  FROM  Transaction_Details TD \r\n                    WHERE TD.SysDocID = JD.SysDocID AND TD.VoucherID = JD.VoucherID  FOR XML PATH('')),2,20000)) AS [SubDescription],\r\n                    ISNULL((SELECT TOP 1 Status FROM Cheque_Issued CR WHERE CR.SysDocID=JD.SysDocID AND\r\n                    CR.VoucherID=JD.VoucherID AND CR.PayeeID=JD.PayeeID AND CR.Status=2 AND ISNULL(IsVoid,'False')='False'),\r\n                    ISNULL((SELECT TOP 1 Status FROM Cheque_Received CR WHERE CR.SysDocID=JD.SysDocID AND\r\n                    CR.VoucherID=JD.VoucherID AND CR.PayeeID=JD.PayeeID  AND ISNULL(IsVoid,'False')='False'),(SELECT TOP 1 Status FROM Cheque_Issued Ci WHERE CI.SysDocID=JD.SysDocID AND\r\n                    CI.VoucherID=JD.VoucherID AND CI.PayeeID=JD.PayeeID  AND ISNULL(IsVoid,'False')='False'))) AS [ChkStatus],\r\n                    ISNULL((SELECT SUBSTRING((SELECT ', '+ CustomerName+ ' ' FROM Cheque_Received CR \r\n                    INNER JOIN Customer C ON CR.PayeeID=C.CustomerID WHERE DepositSysDocID=J.SysDocID \r\n                    AND DepositVoucherID=J.VoucherID AND ChequeNumber=JD.CheckNumber FOR XML PATH('')),2,20000)),(SELECT SUBSTRING((SELECT ', '+ CustomerName+ ' ' FROM Cheque_Received CR \r\n                    INNER JOIN Customer C ON CR.PayeeID=C.CustomerID WHERE DiscountSysDocID=J.SysDocID \r\n                    AND DiscountVoucherID=J.VoucherID AND ChequeNumber=JD.CheckNumber FOR XML PATH('')),2,20000))) AS [DiscPayee],\r\n                    ISNULL((SELECT SUBSTRING((SELECT  ', ' + convert(varchar,  ( CI.ChequeDate) , 106) + ' ' FROM  Cheque_Issued CI WHERE CI.SysDocID = J.SysDocID AND CI.VoucherID = J.VoucherID  FOR XML PATH('')),2,20000)),\r\n                    (SELECT SUBSTRING((SELECT  ', ' + convert(varchar,  ( CR.ChequeDate) , 106) + ' ' FROM  Cheque_Received CR WHERE CR.SysDocID = J.SysDocID AND CR.VoucherID = J.VoucherID  FOR XML PATH('')),2,20000))) AS [ChqueDate],              \r\n                    PayeeID + '-' + (CASE PayeeType WHEN 'C' THEN Customer.CustomerName WHEN 'V' THEN Vendor.VendorName  ELSE  Employee.FirstName + ' ' + Employee.LastName END) AS Name,\r\n                    PayeeID + '-' + (CASE PayeeType WHEN 'C' THEN Customer.CustomerName WHEN 'V' THEN Vendor.VendorName WHEN 'E' THEN Employee.FirstName + ' ' + Employee.LastName END) AS Payee,Job.JobID,Job.JobName [Job]\r\n                    ,AnalysisID [Analysis],ISNULL(JD.CurrencyID,J.CurrencyID) AS CurrencyID, ISNULL(JD.CurrencyRate, J.CurrencyRate) CurrencyRate,  JD.AttributeID1, JD.AttributeID2, PR.PropertyName,PU.PropertyUnitName, ";
			str = (isFC ? (str + " DebitFC AS Debit,CreditFC AS Credit") : (str + " Debit,Credit,DebitFC,CreditFC "));
			str = str + " FROM JOURNAL_DETAILS JD INNER JOIN Journal J ON J.SysDocID=JD.SysDocID AND J.VoucherID = JD.VoucherID INNER JOIN\r\n                    Account Acc ON JD.AccountID=Acc.AccountID \r\n                    LEFT OUTER JOIN System_Document SD ON SD.SysDocID=J.SysDocID\r\n                    LEFT OUTER JOIN Customer ON JD.PayeeID=Customer.CustomerID LEFT OUTER JOIN\r\n                    Vendor ON JD.PayeeID=Vendor.VendorID LEFT OUTER JOIN Employee ON Employee.EmployeeID=JD.PayeeID\r\n                    LEFT OUTER JOIN Cost_Center CC ON CC.CostCenterID=JD.CostCenterID\r\n                    LEFT OUTER JOIN Property PR ON JD.AttributeID1=PR.PropertyID\r\n                    LEFT OUTER JOIN Property_Unit PU ON JD.AttributeID2=PU.PropertyUnitID\r\n\t                LEFT OUTER JOIN Job ON Job.JobID=JD.JobID\r\n                    WHERE J.JournalDate BETWEEN '" + text + "' AND '" + text2 + "'";
			str = str + " AND (JD.IsVoid IS NULL OR JD.IsVoid='False')  AND     JD.JournalID NOT IN(SELECT JD1.JournalID FROM Journal J1 INNER JOIN Journal_Details JD1   \r\n                 ON J1.JournalID = JD1.JournalID WHERE JournalDate <= '" + text2 + "'  AND ISNULL(JD1.Reference, '') = 'SYS_YEND')";
			if (fromAccountID != "")
			{
				str = str + " AND JD.AccountID>='" + fromAccountID + "'";
			}
			if (toAccountID != "")
			{
				str = str + " AND JD.AccountID<='" + toAccountID + "' ";
			}
			if (fromLocationID != "")
			{
				str = str + " AND (SD.LocationID>='" + fromLocationID + "' or JD.CostCenterID ='" + fromLocationID + "')";
			}
			if (toLocationID != "")
			{
				str = str + " AND ( SD.LocationID <='" + toLocationID + "'  or JD.CostCenterID ='" + toLocationID + "')";
			}
			if (fromDivision != "")
			{
				str = str + " AND (JD.DivisionID>='" + fromDivision + "' or JD.CostCenterID ='" + fromDivision + "')";
			}
			if (toDivision != "")
			{
				str = str + " AND ( JD.DivisionID <='" + toDivision + "'  or JD.CostCenterID ='" + toDivision + "')";
			}
			if (costCenterID != "")
			{
				str = str + " AND JD.CostCenterID ='" + costCenterID + "' ";
			}
			if (groupID != "")
			{
				str = str + " AND Acc.GroupID IN (" + text3 + ") ";
			}
			if (fromJob != "")
			{
				str = str + " AND JD.JobID >='" + fromJob + "'";
			}
			if (toJob != "")
			{
				str = str + " AND JD.JobID<='" + toJob + "'";
			}
			if (fromCostCategory != "")
			{
				str = str + " AND JD.CostCategoryID >='" + fromCostCategory + "'";
			}
			if (toCostCategory != "")
			{
				str = str + " AND JD.CostCategoryID <='" + toCostCategory + "'";
			}
			if (fromProperty != "")
			{
				str = str + " AND JD.AttributeID1 >='" + fromProperty + "'";
			}
			if (toProperty != "")
			{
				str = str + " AND JD.AttributeID1 <='" + toProperty + "'";
			}
			if (fromPropertyUnit != "")
			{
				str = str + " AND JD.AttributeID2 >='" + fromPropertyUnit + "'";
			}
			if (toProprertyUnit != "")
			{
				str = str + " AND JD.AttributeID2 >='" + toProprertyUnit + "'";
			}
			if (isFC)
			{
				str += " AND ISNULL(DebitFC,0) + ISNULL(CreditFC,0) <> 0 ";
			}
			if (allowposting)
			{
				str += " AND ISNULL(J.ApprovalStatus,10)=10 ";
			}
			str += " ORDER BY CONVERT(DATE, JournalDate, 103), JD.VoucherID,JD.CheckNumber,JD.CheckDate,JD.CheckbookID,JD.CheckID ";
			FillDataSet(dataSet3, "GL Details", str);
			dataSet2.Merge(dataSet3);
			dataSet2.Relations.Add("GL Details", dataSet2.Tables["GL"].Columns["Account Code"], dataSet2.Tables["GL Details"].Columns["Account Code"], createConstraints: false);
			DataSet dataSet4 = new DataSet();
			str = "SELECT SysDocID,VoucherID,CASE WHEN Status<>8 THEN ChequeNumber ELSE '(R)' + ChequeNumber END AS ChequeNumber,ChqRec.ReceiptDate,\r\n                    ChqRec.BankID,Bank.BankName,PayeeID as [Account Code],ChequeDate,ISNULL(AmountFC,Amount) AS Amount,ChqRec.Note\r\n                    FROM Cheque_Received ChqRec\r\n                    LEFT OUTER JOIN Bank ON Bank.BankID=ChqRec.BankID\r\n                    WHERE Status IN (1,3,4,8) AND ISNULL(IsVoid,'False')='False'\r\n                    AND PayeeType='A'  ";
			if (fromAccountID != "")
			{
				str = str + " AND PayeeID>='" + fromAccountID + "'";
			}
			if (toAccountID != "")
			{
				str = str + " AND PayeeID<='" + toAccountID + "' ";
			}
			FillDataSet(dataSet4, "PDC Rec", str);
			dataSet2.Merge(dataSet4);
			dataSet2.Relations.Add("PDCRecReln", dataSet2.Tables["GL"].Columns["Account Code"], dataSet2.Tables["PDC Rec"].Columns["Account Code"], createConstraints: false);
			DataSet dataSet5 = new DataSet();
			str = "SELECT SysDocID,VoucherID,CASE WHEN Status<>8 THEN ChequeNumber ELSE '(R)' + ChequeNumber END AS ChequeNumber,ChqRec.IssueDate,\r\n                    ChqRec.BankID,Bank.BankName,PayeeID as [Account Code],ChequeDate,ISNULL(AmountFC,Amount) AS Amount,ChqRec.note\r\n                    FROM Cheque_Issued ChqRec\r\n                    LEFT OUTER JOIN Bank ON Bank.BankID=ChqRec.BankID\r\n                    WHERE Status=2 AND ISNULL(IsVoid,'False')='False'\r\n                    AND PayeeType='A'";
			if (fromAccountID != "")
			{
				str = str + " AND PayeeID>='" + fromAccountID + "'";
			}
			if (toAccountID != "")
			{
				str = str + " AND PayeeID<='" + toAccountID + "' ";
			}
			FillDataSet(dataSet5, "PDC Iss", str);
			dataSet2.Merge(dataSet5);
			dataSet2.Relations.Add("PDCIssReln", dataSet2.Tables["GL"].Columns["Account Code"], dataSet2.Tables["PDC Iss"].Columns["Account Code"], createConstraints: false);
			return dataSet2;
		}

		public DataSet GetProjectAccountTransactionsReport(DateTime from, DateTime to, string fromAccountID, string toAccountID, string fromJobID, string toJobID, string toCostCategory, string fromCostCategory, string fromLocationID, string toLocationID, bool isFC)
		{
			try
			{
				string text = CommonLib.ToSqlDateTimeString(from);
				string text2 = CommonLib.ToSqlDateTimeString(to);
				string baseCurrencyID = new Currencies(base.DBConfig).GetBaseCurrencyID();
				DataSet dataSet = new DataSet();
				string empty = string.Empty;
				empty = " SELECT JournalDate AS [Journal Date],J.VoucherID AS [Number],J.SysDocType, ISNULL(JD.CurrencyID,J.CurrencyID) AS [Currency],\r\n                    JD.AccountID [Account Code], AccountName [Account Name],Acc.Alias, JD.Reference,JD.Description,\r\n                    PayeeID + '-' + (CASE PayeeType WHEN 'C' THEN Customer.CustomerName WHEN 'V' THEN Vendor.VendorName WHEN 'E' THEN Employee.FirstName + ' ' + Employee.LastName END) AS Payee\r\n                    ,AnalysisID [Analysis],ISNULL(JD.CurrencyID,J.CurrencyID) AS CurrencyID, ISNULL(JD.CurrencyRate, J.CurrencyRate) CurrencyRate, JD.JobID, JB.JobName, JD.CostCategoryID, JCC.CostCategoryName, ";
				empty = (isFC ? (empty + " ISNULL(DebitFC, 0) AS Debit, ISNULL(CreditFC, 0) AS Credit") : (empty + " ISNULL(Debit, 0) Debit, ISNULL(Credit, 0) Credit, ISNULL(DebitFC, 0) DebitFC, ISNULL(CreditFC, 0) CreditFC"));
				empty += " FROM JOURNAL_DETAILS JD INNER JOIN Journal J ON J.SysDocID=JD.SysDocID AND J.VoucherID = JD.VoucherID INNER JOIN\r\n                    Account Acc ON JD.AccountID=Acc.AccountID \r\n                    LEFT OUTER JOIN Customer ON JD.PayeeID=Customer.CustomerID LEFT OUTER JOIN\r\n                    Vendor ON JD.PayeeID=Vendor.VendorID LEFT OUTER JOIN Employee ON Employee.EmployeeID=JD.PayeeID\r\n                    LEFT OUTER JOIN Job JB ON JD.JobID = JB.JobID                     \r\n                    LEFT OUTER JOIN Job_Cost_Category JCC ON JD.CostCategoryID = JCC.CostCategoryID\r\n                    INNER JOIN Account_Group AG ON Acc.GroupID = AG.GroupID";
				if (fromLocationID != "")
				{
					empty += " LEFT JOIN System_Document SD ON SD.SysDocID=JD.SysDocID";
				}
				empty = empty + " WHERE JD.JobID <> NULL OR JD.JobID <> ''  AND (AG.TypeID = 4 AND  Acc.SubType <> 9) \r\n                    AND J.JournalDate BETWEEN '" + text + "' AND '" + text2 + "'";
				empty += " AND (JD.IsVoid IS NULL OR JD.IsVoid='False') ";
				if (fromAccountID != "")
				{
					empty = empty + " AND JD.AccountID>='" + fromAccountID + "'";
				}
				if (toAccountID != "")
				{
					empty = empty + " AND JD.AccountID<='" + toAccountID + "' ";
				}
				if (fromJobID != "")
				{
					empty = empty + " AND JD.JobID >='" + fromJobID + "'";
				}
				if (toJobID != "")
				{
					empty = empty + " AND JD.JobID<='" + toJobID + "'";
				}
				if (fromCostCategory != "")
				{
					empty = empty + " AND JD.CostCategoryID >='" + fromCostCategory + "'";
				}
				if (toCostCategory != "")
				{
					empty = empty + " AND JD.CostCategoryID <='" + toCostCategory + "'";
				}
				if (fromLocationID != "")
				{
					empty = empty + " AND SD.LocationID >='" + fromLocationID + "' AND SD.LocationID <='" + toLocationID + "'";
				}
				if (isFC)
				{
					empty = empty + " AND (J.CurrencyID IS NOT NULL AND J.CUrrencyID <> '" + baseCurrencyID + "' )";
				}
				empty += " UNION ";
				empty = empty + "SELECT  '" + text + "' , 'OPENING', J.SysDocType,'', JD.AccountID [Account Code], AccountName [Account Name],Acc.Alias, 'OPENING' ,'',\r\n                    '','', '', 1 AS CurrencyRate, JD.JobID , JB.JobName, JD.CostCategoryID , JCC.CostCategoryName, \r\n                    CASE WHEN SUM(ISNULL( Debit, 0) -  ISNULL(Credit, 0) )  > 0 then ABS(SUM(ISNULL(Debit, 0) - ISNULL(Credit,0))) ELSE 0 END,\r\n                    CASE WHEN SUM(ISNULL( Debit,0) -  ISNULL(Credit,0))  <= 0 then  ABS(SUM(ISNULL(Debit, 0) - ISNULL(Credit,0))) ELSE  0 END \r\n                    , 0 , 0  FROM JOURNAL_DETAILS JD INNER JOIN Journal J ON J.SysDocID=JD.SysDocID AND J.VoucherID = JD.VoucherID INNER JOIN\r\n                    Account Acc ON JD.AccountID=Acc.AccountID\r\n                    LEFT OUTER JOIN Customer ON JD.PayeeID=Customer.CustomerID LEFT OUTER JOIN\r\n                    Vendor ON JD.PayeeID=Vendor.VendorID LEFT OUTER JOIN Employee ON Employee.EmployeeID=JD.PayeeID\r\n                    LEFT OUTER JOIN Job JB ON JD.JobID = JB.JobID                     \r\n                    LEFT OUTER JOIN Job_Cost_Category JCC ON JD.CostCategoryID = JCC.CostCategoryID\r\n                    INNER JOIN Account_Group AG ON Acc.GroupID = AG.GroupID  ";
				if (fromLocationID != "")
				{
					empty += " LEFT JOIN System_Document SD ON SD.SysDocID=JD.SysDocID";
				}
				empty = empty + " WHERE JD.JobID <> NULL OR JD.JobID <> ''  AND (AG.TypeID = 4 AND  Acc.SubType <> 9) \r\n                    AND J.JournalDate < '" + text + "'";
				if (fromAccountID != "")
				{
					empty = empty + " AND JD.AccountID>='" + fromAccountID + "'";
				}
				if (toAccountID != "")
				{
					empty = empty + " AND JD.AccountID<='" + toAccountID + "' ";
				}
				if (fromJobID != "")
				{
					empty = empty + " AND JD.JobID >='" + fromJobID + "'";
				}
				if (toJobID != "")
				{
					empty = empty + " AND JD.JobID<='" + toJobID + "'";
				}
				if (fromCostCategory != "")
				{
					empty = empty + " AND JD.CostCategoryID >='" + fromCostCategory + "'";
				}
				if (toCostCategory != "")
				{
					empty = empty + " AND JD.CostCategoryID <='" + toCostCategory + "'";
				}
				if (fromLocationID != "")
				{
					empty = empty + " AND SD.LocationID >='" + fromLocationID + "' AND SD.LocationID <='" + toLocationID + "'";
				}
				if (isFC)
				{
					empty = empty + " AND (J.CurrencyID IS NOT NULL AND J.CUrrencyID <> '" + baseCurrencyID + "' )";
				}
				empty += " GROUP BY JD.JobID, JB.JobName, JD.CostCategoryID, JCC.CostCategoryName, J.SysDocType, JD.AccountID, AccountName,Acc.Alias ";
				empty += " ORDER BY J.JournalDate";
				FillDataSet(dataSet, "GL Details", empty);
				return dataSet;
			}
			catch
			{
				throw;
			}
		}

		public DataSet GetPropertyAccountTransactionsReport(DateTime from, DateTime to, string fromAccountID, string toAccountID, string fromLocationID, string toLocationID, string fromProperty, string toProperty, string fromPropertyClass, string toPropertyClass, string fromUnit, string toUnit, bool isFC)
		{
			try
			{
				string text = CommonLib.ToSqlDateTimeString(from);
				string text2 = CommonLib.ToSqlDateTimeString(to);
				string baseCurrencyID = new Currencies(base.DBConfig).GetBaseCurrencyID();
				DataSet dataSet = new DataSet();
				string empty = string.Empty;
				empty = "  SELECT JournalDate AS [Journal Date],J.VoucherID AS [Number],J.SysDocType, ISNULL(JD.CurrencyID,J.CurrencyID) AS [Currency],\r\n                            JD.AccountID [Account Code], AccountName [Account Name],Acc.Alias, JD.Reference,JD.Description,\r\n                            PayeeID + '-' + (CASE PayeeType WHEN 'C' THEN Customer.CustomerName WHEN 'V' THEN Vendor.VendorName WHEN 'E' THEN Employee.FirstName + ' ' + Employee.LastName END) AS Payee\r\n                            ,AnalysisID [Analysis],ISNULL(JD.CurrencyID,J.CurrencyID) AS CurrencyID, ISNULL(JD.CurrencyRate, J.CurrencyRate) CurrencyRate, JD.AttributeID1, PR.PropertyName,JD.AttributeID2, PU.PropertyUnitName, ";
				empty = (isFC ? (empty + " ISNULL(DebitFC, 0) AS Debit, ISNULL(CreditFC, 0) AS Credit") : (empty + " ISNULL(Debit, 0) Debit, ISNULL(Credit, 0) Credit, ISNULL(DebitFC, 0) DebitFC, ISNULL(CreditFC, 0) CreditFC"));
				empty += " FROM JOURNAL_DETAILS JD INNER JOIN Journal J ON J.SysDocID=JD.SysDocID AND J.VoucherID = JD.VoucherID INNER JOIN\r\n                    Account Acc ON JD.AccountID=Acc.AccountID \r\n                    LEFT OUTER JOIN Customer ON JD.PayeeID=Customer.CustomerID LEFT OUTER JOIN\r\n                    Vendor ON JD.PayeeID=Vendor.VendorID LEFT OUTER JOIN Employee ON Employee.EmployeeID=JD.PayeeID\r\n                    \r\n                    LEFT OUTER JOIN Property PR ON JD.AttributeID1=PR.PropertyID\r\n                    LEFT OUTER JOIN Property_Unit PU ON JD.AttributeID2=PU.PropertyUnitID\r\n                    INNER JOIN Account_Group AG ON Acc.GroupID = AG.GroupID";
				if (fromLocationID != "")
				{
					empty += " LEFT JOIN System_Document SD ON SD.SysDocID=JD.SysDocID";
				}
				empty = empty + " WHERE JD.AttributeID1 <> NULL OR JD.AttributeID1 <> ''  AND (AG.TypeID = 4 AND  Acc.SubType <> 9) \r\n                    AND J.JournalDate BETWEEN '" + text + "' AND '" + text2 + "'";
				empty += " AND (JD.IsVoid IS NULL OR JD.IsVoid='False') ";
				if (fromAccountID != "")
				{
					empty = empty + " AND JD.AccountID>='" + fromAccountID + "'";
				}
				if (toAccountID != "")
				{
					empty = empty + " AND JD.AccountID<='" + toAccountID + "' ";
				}
				if (fromProperty != "")
				{
					empty = empty + " AND JD.AttributeID1 >='" + fromProperty + "'";
				}
				if (toProperty != "")
				{
					empty = empty + " AND JD.AttributeID1 <='" + toProperty + "'";
				}
				if (fromPropertyClass != "")
				{
					empty = empty + " AND PR.PropertyClassID >='" + fromPropertyClass + "'";
				}
				if (toPropertyClass != "")
				{
					empty = empty + " AND PR.PropertyClassID >='" + toPropertyClass + "'";
				}
				if (fromUnit != "")
				{
					empty = empty + " AND JD.AttributeID2 >='" + fromPropertyClass + "'";
				}
				if (toUnit != "")
				{
					empty = empty + " AND JD.AttributeID2 >='" + toPropertyClass + "'";
				}
				if (fromLocationID != "")
				{
					empty = empty + " AND SD.LocationID >='" + fromLocationID + "' AND SD.LocationID <='" + toLocationID + "'";
				}
				if (isFC)
				{
					empty = empty + " AND (J.CurrencyID IS NOT NULL AND J.CUrrencyID <> '" + baseCurrencyID + "' )";
				}
				empty += " UNION ";
				empty = empty + "SELECT  '" + text + "' , 'OPENING', J.SysDocType,'', JD.AccountID [Account Code], AccountName [Account Name],Acc.Alias, 'OPENING' ,'',\r\n                    '','', '', 1 AS CurrencyRate, JD.AttributeID1, PR.PropertyName,JD.AttributeID2, PU.PropertyUnitName,\r\n                    CASE WHEN SUM(ISNULL( Debit, 0) -  ISNULL(Credit, 0) )  > 0 then ABS(SUM(ISNULL(Debit, 0) - ISNULL(Credit,0))) ELSE 0 END,\r\n                    CASE WHEN SUM(ISNULL( Debit,0) -  ISNULL(Credit,0))  <= 0 then  ABS(SUM(ISNULL(Debit, 0) - ISNULL(Credit,0))) ELSE  0 END \r\n                    , 0 , 0  FROM JOURNAL_DETAILS JD INNER JOIN Journal J ON J.SysDocID=JD.SysDocID AND J.VoucherID = JD.VoucherID INNER JOIN\r\n                    Account Acc ON JD.AccountID=Acc.AccountID\r\n                    LEFT OUTER JOIN Customer ON JD.PayeeID=Customer.CustomerID LEFT OUTER JOIN\r\n                    Vendor ON JD.PayeeID=Vendor.VendorID LEFT OUTER JOIN Employee ON Employee.EmployeeID=JD.PayeeID\r\n                  \r\n                    LEFT OUTER JOIN Property PR ON JD.AttributeID1=PR.PropertyID\r\n                    LEFT OUTER JOIN Property_Unit PU ON JD.AttributeID2=PU.PropertyUnitID\r\n                    INNER JOIN Account_Group AG ON Acc.GroupID = AG.GroupID  ";
				if (fromLocationID != "")
				{
					empty += " LEFT JOIN System_Document SD ON SD.SysDocID=JD.SysDocID";
				}
				empty = empty + " WHERE JD.AttributeID1 <> NULL OR JD.AttributeID1 <> ''   AND (AG.TypeID = 4 AND  Acc.SubType <> 9) \r\n                    AND J.JournalDate < '" + text + "'";
				if (fromAccountID != "")
				{
					empty = empty + " AND JD.AccountID>='" + fromAccountID + "'";
				}
				if (toAccountID != "")
				{
					empty = empty + " AND JD.AccountID<='" + toAccountID + "' ";
				}
				if (fromProperty != "")
				{
					empty = empty + " AND JD.AttributeID1 >='" + fromProperty + "'";
				}
				if (toProperty != "")
				{
					empty = empty + " AND JD.AttributeID1 <='" + toProperty + "'";
				}
				if (fromPropertyClass != "")
				{
					empty = empty + " AND PR.PropertyClassID >='" + fromPropertyClass + "'";
				}
				if (toPropertyClass != "")
				{
					empty = empty + " AND PR.PropertyClassID <='" + toPropertyClass + "'";
				}
				if (fromUnit != "")
				{
					empty = empty + " AND JD.AttributeID2 >='" + fromPropertyClass + "'";
				}
				if (toUnit != "")
				{
					empty = empty + " AND JD.AttributeID2 <='" + toPropertyClass + "'";
				}
				if (fromLocationID != "")
				{
					empty = empty + " AND SD.LocationID >='" + fromLocationID + "' AND SD.LocationID <='" + toLocationID + "'";
				}
				if (isFC)
				{
					empty = empty + " AND (J.CurrencyID IS NOT NULL AND J.CUrrencyID <> '" + baseCurrencyID + "' )";
				}
				empty += " GROUP BY  J.SysDocType, JD.AccountID, AccountName,Acc.Alias,JD.AttributeID1 ,PR.PropertyName,JD.AttributeID2, PU.PropertyUnitName  ";
				empty += " ORDER BY J.JournalDate";
				FillDataSet(dataSet, "GL Details", empty);
				return dataSet;
			}
			catch
			{
				throw;
			}
		}
	}
}
