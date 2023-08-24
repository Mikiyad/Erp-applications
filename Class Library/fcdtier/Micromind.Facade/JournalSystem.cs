using Micromind.Common.Data;
using Micromind.Common.Interfaces;
using Micromind.Data;
using System;
using System.Data;

namespace Micromind.Facade
{
	public sealed class JournalSystem : MarshalByRefObject, IJournalSystem, IDisposable
	{
		private Config config;

		public JournalSystem(Config config)
		{
			this.config = config;
		}

		public void Dispose()
		{
		}

		public bool CreateJournalVoucher(GLData journalData)
		{
			return new Journal(config).InsertUpdateJournalVoucher(journalData, isUpdate: false);
		}

		public bool UpdateJournalVoucher(GLData journalData)
		{
			return new Journal(config).InsertUpdateJournalVoucher(journalData, isUpdate: true);
		}

		public bool DeleteJournalVoucher(string sysDocID, string voucherID)
		{
			using (Journal journal = new Journal(config))
			{
				return journal.DeleteJournalVoucher(sysDocID, voucherID);
			}
		}

		public bool VoidJournalVoucher(string sysDocID, string voucherID, bool isVoid)
		{
			using (Journal journal = new Journal(config))
			{
				return journal.VoidJournalVoucher(sysDocID, voucherID, isVoid);
			}
		}

		public DataSet GetJournalReport(DateTime from, DateTime to, string fromLocation, string toLocation, string fromDivisionID, string toDivisionID, bool isVoid)
		{
			using (JournalReports journalReports = new JournalReports(config))
			{
				return journalReports.GetJournalReport(from, to, fromLocation, toLocation, fromDivisionID, toDivisionID, isVoid);
			}
		}

		public GLData GetJournalVoucherByID(string sysDocID, string voucherID)
		{
			using (Journal journal = new Journal(config))
			{
				return journal.GetJournalVoucherByID(sysDocID, voucherID);
			}
		}

		public DataSet GetGLReport(DateTime from, DateTime to, string fromLocationID, string toLocationID, string fromDivisionID, string toDivisionID)
		{
			using (JournalReports journalReports = new JournalReports(config))
			{
				return journalReports.GetGLReport(from, to, fromLocationID, toLocationID, fromDivisionID, toDivisionID);
			}
		}

		public DataSet GetTrialBalanceReport(DateTime from, DateTime to, string fromLocation, string toLocation, string fromDivision, string toDivision, bool showZeroBalance, bool allowposting)
		{
			using (JournalReports journalReports = new JournalReports(config))
			{
				return journalReports.GetTrialBalanceReport(from, to, fromLocation, toLocation, fromDivision, toDivision, showZeroBalance, allowposting);
			}
		}

		public DataSet GetTrialBalanceReportConsolidated(DateTime from, DateTime to, string fromLocation, string toLocation, string fromDivision, string toDivision, bool showZeroBalance, bool allowposting)
		{
			using (JournalReports journalReports = new JournalReports(config))
			{
				return journalReports.GetTrialBalanceReportConsolidated(from, to, fromLocation, toLocation, fromDivision, toDivision, showZeroBalance, allowposting);
			}
		}

		public DataSet GetBalanceSheetReport(DateTime to, string fromLocationID, string toLocationID, string fromDivisionID, string toDivisionID, int level, bool showAccounts, bool allowPosting)
		{
			using (JournalReports journalReports = new JournalReports(config))
			{
				return journalReports.GetBalanceSheetReport(to, fromLocationID, toLocationID, fromDivisionID, toDivisionID, level, showAccounts, allowPosting);
			}
		}

		public DataSet GetProfitAndLossReport(DateTime from, DateTime to, string fromLocationID, string toLocationID, string fromDivisionID, string toDivisionID, int level, bool allowposting)
		{
			using (JournalReports journalReports = new JournalReports(config))
			{
				return journalReports.GetProfitAndLossReport(from, to, fromLocationID, toLocationID, fromDivisionID, toDivisionID, level, allowposting);
			}
		}

		public DataSet GetProfitAndLossReportSummary(DateTime from, DateTime to, string fromLocationID, string toLocationID, string fromDivisionID, string toDivisionID, int level, bool allowposting)
		{
			using (JournalReports journalReports = new JournalReports(config))
			{
				return journalReports.GetProfitAndLossReportSummary(from, to, fromLocationID, toLocationID, fromDivisionID, toDivisionID, level, allowposting);
			}
		}

		public DataSet GetProfitAndLossMonthwiseReport(DateTime from, DateTime to, string fromLocationID, string toLocationID, string fromDivisionID, string toDivisionID)
		{
			using (JournalReports journalReports = new JournalReports(config))
			{
				return journalReports.GetProfitAndLossMonthwiseReport(from, to, fromLocationID, toLocationID, fromDivisionID, toDivisionID);
			}
		}

		public DataSet GetCashFlowReport(DateTime from, DateTime to, string fromLocationID, string toLocationID)
		{
			using (JournalReports journalReports = new JournalReports(config))
			{
				return journalReports.GetCashFlowReport(from, to, fromLocationID, toLocationID);
			}
		}

		public DataSet GetAccountTransactionsReport(DateTime from, DateTime to, string fromAccountID, string toAccountID, string fromLocationID, string toLocationID, string groupID, bool isFC, string costCenter, bool allowposting, string fromJob, string toJob, string fromCostCategory, string toCostCategory, string fromProperty, string toProperty, string fromPropertyUnit, string toProprertyUnit, string fromAnalysis, string toAnalysis, string fromDivision, string toDivision)
		{
			using (JournalReports journalReports = new JournalReports(config))
			{
				return journalReports.GetAccountTransactionsReport(from, to, fromAccountID, toAccountID, fromLocationID, toLocationID, groupID, isFC, costCenter, allowposting, fromJob, toJob, fromCostCategory, toCostCategory, fromProperty, toProperty, fromPropertyUnit, toProprertyUnit, fromAnalysis, toAnalysis, fromDivision, toDivision);
			}
		}

		public DataSet GetProjectAccountTransactionsReport(DateTime from, DateTime to, string fromAccountID, string toAccountID, string fromJob, string toJob, string fromCostCategory, string toCostCategory, string fromLocationID, string toLocationID, bool isFC)
		{
			using (JournalReports journalReports = new JournalReports(config))
			{
				return journalReports.GetProjectAccountTransactionsReport(from, to, fromAccountID, toAccountID, fromJob, toJob, fromCostCategory, toCostCategory, fromLocationID, toLocationID, isFC);
			}
		}

		public DataSet GetAnalysisTransactionsReport(DateTime from, DateTime to, string accountID, string fromAnalysis, string toAnalysis, string fromGroup, string toGroup, string fromLocation, string toLocation, string fromDivision, string toDivision)
		{
			using (JournalReports journalReports = new JournalReports(config))
			{
				return journalReports.GetAnalysisTransactionsReport(from, to, accountID, fromAnalysis, toAnalysis, fromGroup, toGroup, fromLocation, toLocation, fromDivision, toDivision);
			}
		}

		public DataSet GetJournalToPrint(string sysDocID, string[] voucherID)
		{
			return new Journal(config).GetJournalVoucherToPrint(sysDocID, voucherID);
		}

		public DataSet GetJournalToPrint(string sysDocID, string voucherID)
		{
			return new Journal(config).GetJournalVoucherToPrint(sysDocID, new string[1]
			{
				voucherID
			});
		}

		public GLData GetBankReconciliationList(string accountID, DateTime fromDate, DateTime toDate, bool reconciled)
		{
			return new Journal(config).GetBankReconciliationList(accountID, fromDate, toDate, reconciled);
		}

		public DataSet GetJVList(DateTime from, DateTime to, bool showVoid)
		{
			return new Journal(config).GetJVList(from, to, showVoid);
		}

		public DataSet GetList(DateTime from, DateTime to, bool showVoid)
		{
			return new Journal(config).GetList(from, to, showVoid);
		}

		public DataSet GetTrialBalanceList(DateTime from, DateTime to, bool showVoid)
		{
			return new Journal(config).GetTrialBalanceList(from, to, showVoid);
		}

		public DataSet GetClosingIncomeExpenseList(string fiscalYearID)
		{
			return new Journal(config).GetClosingIncomeExpenseList(fiscalYearID);
		}

		public bool UpdateJournalBankReconciliation(GLData journalData, bool isUpdate = true)
		{
			return new Journal(config).UpdateJournalBankReconciliation(journalData, isUpdate);
		}

		public DataSet GetBankReconciliationToPrint(string accountId, DateTime fromDate, DateTime toDate, bool reconciled)
		{
			return new Journal(config).GetBankReconciliationToPrint(accountId, fromDate, toDate, reconciled);
		}

		public DataSet GetJournalDistributionList(string sysDocID, string voucherID)
		{
			using (Journal journal = new Journal(config))
			{
				return journal.GetJournalDistributionList(sysDocID, voucherID);
			}
		}

		public GLData GetJournalDistributionSummary(string sysDocID, string voucherID)
		{
			using (Journal journal = new Journal(config))
			{
				return journal.GetJournalDistributionSummary(sysDocID, voucherID);
			}
		}

		public GLData GetDocumentInformation(string sysDocID, string voucherID)
		{
			using (Journal journal = new Journal(config))
			{
				return journal.GetDocumentInformation(sysDocID, voucherID);
			}
		}

		public DataSet GetDailyCashReport(DateTime from, DateTime to, string RegisterID)
		{
			using (JournalReports journalReports = new JournalReports(config))
			{
				return journalReports.GetDailyCashReport(from, to, RegisterID);
			}
		}

		public DataSet GetDailyCashSaleReport(DateTime from, DateTime to, string RegisterID)
		{
			using (JournalReports journalReports = new JournalReports(config))
			{
				return journalReports.GetDailyCashSaleReport(from, to, RegisterID);
			}
		}

		public DataSet GetAccountStatement(string accountID, bool includeReconciled, DateTime from, DateTime to)
		{
			using (JournalReports journalReports = new JournalReports(config))
			{
				return journalReports.GetAccountStatement(accountID, includeReconciled, from, to);
			}
		}

		public bool MergeJournalDetailsForCOGSUpdate(int journalID, bool mergeSysWithRegular)
		{
			using (Journal journal = new Journal(config))
			{
				return journal.MergeJournalDetailsForCOGSUpdate(journalID, mergeSysWithRegular);
			}
		}

		public DataSet GetTrialBalanceReportwithAccountHead(DateTime from, DateTime to, string fromLocation, string toLocation, string fromDivision, string toDivision, bool showZeroBalance, bool allowposting)
		{
			using (JournalReports journalReports = new JournalReports(config))
			{
				return journalReports.GetTrialBalanceReportwithAccountHead(from, to, fromLocation, toLocation, fromDivision, toDivision, showZeroBalance, allowposting);
			}
		}

		public DataSet GetAccountLedgerReport(DateTime from, DateTime to, string fromAccountID, string toAccountID, string fromLocation, string toLocation, string fromDivisionID, string toDivisionID, string groupID, bool isFC, string costCenter, bool allowposting)
		{
			using (JournalReports journalReports = new JournalReports(config))
			{
				return journalReports.GetAccountLedgerReport(from, to, fromAccountID, toAccountID, fromLocation, toLocation, fromDivisionID, toDivisionID, groupID, isFC, costCenter, allowposting);
			}
		}

		public DataSet GetAccountLedgerReportSummary(DateTime from, DateTime to, string fromAccountID, string toAccountID, string fromLocation, string toLocation, string fromDivisionID, string toDivisionID, string groupID, bool isFC, string costCenter, bool allowposting)
		{
			using (JournalReports journalReports = new JournalReports(config))
			{
				return journalReports.GetAccountLedgerReportSummary(from, to, fromAccountID, toAccountID, fromLocation, toLocation, fromDivisionID, toDivisionID, groupID, isFC, costCenter, allowposting);
			}
		}

		public DataSet GetAccountCostCenterReport(DateTime from, DateTime to, string fromAccountID, string toAccountID, string fromLocation, string toLocation, bool isFC, string costCenter, string fromDivisionID, string toDivisionID)
		{
			using (JournalReports journalReports = new JournalReports(config))
			{
				return journalReports.GetAccountCostCenterReport(from, to, fromAccountID, toAccountID, fromLocation, toLocation, isFC, costCenter, fromDivisionID, toDivisionID);
			}
		}

		public DataSet GetBankLedgerReport(DateTime from, DateTime to, string fromAccountID, string toAccountID, string fromLocationID, string toLocationID, string fromDivisionID, string toDivisionID)
		{
			using (JournalReports journalReports = new JournalReports(config))
			{
				return journalReports.GetBankLedgerReport(from, to, fromAccountID, toAccountID, fromLocationID, toLocationID, fromDivisionID, toDivisionID);
			}
		}

		public DataSet GetProfitAndLossComparisonReport(DateTime periodfrom1, DateTime periodto1, DateTime periodfrom2, DateTime periodto2, string fromLocationID, string toLocationID, string fromDivisionID, string toDivisionID)
		{
			using (JournalReports journalReports = new JournalReports(config))
			{
				return journalReports.GetProfitAndLossComparisonReport(periodfrom1, periodto1, periodfrom2, periodto2, fromLocationID, toLocationID, fromDivisionID, toDivisionID);
			}
		}

		public DataSet GetBalanceSheetComparisonReport(DateTime periodto2, string fromLocationID, string toLocationID, string fromDivisionID, string toDivisionID, int level, bool showAccounts, bool allowPosting)
		{
			using (JournalReports journalReports = new JournalReports(config))
			{
				return journalReports.GetBalanceSheetComparisonReport(periodto2, fromLocationID, toLocationID, fromDivisionID, toDivisionID, level, showAccounts, allowPosting);
			}
		}

		public DataSet GetAccountSnapBalance(string accountID)
		{
			return new Journal(config).GetAccountSnapBalance(accountID);
		}

		public DataSet GetAnalysisTransactionsPivotReport(DateTime from, DateTime to, string fromaccountID, string toaccountID, string fromAnalysis, string toAnalysis, string fromGroup, string toGroup, string fromLocationID, string toLocationID, string fromDivisionID, string toDivisionID)
		{
			using (JournalReports journalReports = new JournalReports(config))
			{
				return journalReports.GetAnalysisTransactionsPivotReport(from, to, fromaccountID, toaccountID, fromAnalysis, toAnalysis, fromGroup, toGroup, fromLocationID, toLocationID, fromDivisionID, toDivisionID);
			}
		}

		public DataSet GetJournalReference(string AccountID)
		{
			return new Journal(config).GetJournalReference(AccountID);
		}

		public DataSet GetProfitAndLossReportSummaryRevised(DateTime from, DateTime to, string fromLocationID, string toLocationID, string fromDivisionID, string toDivisionID, int level, bool allowposting)
		{
			using (JournalReports journalReports = new JournalReports(config))
			{
				return journalReports.GetProfitAndLossReportSummaryRevised(from, to, fromLocationID, toLocationID, fromDivisionID, toDivisionID, level, allowposting);
			}
		}

		public DataSet GetTrialBalanceAccountList(string ParamValue1, string ParamValue2, DateTime from, DateTime to)
		{
			using (Journal journal = new Journal(config))
			{
				return journal.GetTrialBalanceAccountList(ParamValue1, ParamValue2, from, to);
			}
		}

		public DataSet GetDetails(string tableName, string DocID, string Value)
		{
			using (Journal journal = new Journal(config))
			{
				return journal.getDetails(tableName, DocID, Value);
			}
		}

		public DataSet GetReference(string Table, string ColumnName, string SysdocId, string VoucherNo)
		{
			using (Journal journal = new Journal(config))
			{
				return journal.getReference(Table, ColumnName, SysdocId, VoucherNo);
			}
		}

		public DataSet GetPropertyAccountTransactionsReport(DateTime from, DateTime to, string fromAccountID, string toAccountID, string fromLocationID, string toLocationID, string fromProperty, string toProperty, string fromPropertyClass, string toPropertyClass, string fromUnit, string toUnit, bool isFC)
		{
			using (JournalReports journalReports = new JournalReports(config))
			{
				return journalReports.GetPropertyAccountTransactionsReport(from, to, fromAccountID, toAccountID, fromLocationID, toLocationID, fromProperty, toProperty, fromPropertyClass, toPropertyClass, fromUnit, toUnit, isFC);
			}
		}
	}
}
