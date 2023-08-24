using Micromind.Common.Data;
using System;
using System.Data;

namespace Micromind.Common.Interfaces
{
	public interface IJournalSystem
	{
		bool CreateJournalVoucher(GLData journalData);

		bool UpdateJournalVoucher(GLData journalData);

		bool DeleteJournalVoucher(string sysDocID, string journalID);

		bool VoidJournalVoucher(string sysDocID, string journalID, bool isVoid);

		GLData GetJournalVoucherByID(string sysDocID, string voucherID);

		DataSet GetJournalReport(DateTime from, DateTime to, string fromLocationID, string toLocationID, string fromDivision, string toDivision, bool showVoid);

		DataSet GetGLReport(DateTime from, DateTime to, string fromLocationID, string toLocationID, string fromDivisionID, string toDivisionID);

		DataSet GetTrialBalanceReport(DateTime from, DateTime to, string fromLocation, string toLocation, string fromDivision, string toDivision, bool showZeroBalance, bool allowposting);

		DataSet GetTrialBalanceReportConsolidated(DateTime from, DateTime to, string fromLocation, string toLocation, string fromDivision, string toDivision, bool showZeroBalance, bool allowposting);

		DataSet GetBalanceSheetReport(DateTime to, string fromLocationID, string toLocationID, string fromDivisionID, string toDivisionID, int level, bool showAccounts, bool allowPosting);

		DataSet GetProfitAndLossReport(DateTime from, DateTime to, string fromLocationID, string toLocationID, string fromDivisionID, string toDivisionID, int level, bool allowposting);

		DataSet GetProfitAndLossReportSummary(DateTime from, DateTime to, string fromLocationID, string toLocationID, string fromDivisionID, string toDivisionID, int level, bool allowposting);

		DataSet GetProfitAndLossMonthwiseReport(DateTime from, DateTime to, string fromLocation, string toLocation, string fromDivisionID, string toDivisionID);

		DataSet GetJournalDistributionList(string sysDocID, string voucherID);

		GLData GetJournalDistributionSummary(string sysDocID, string voucherID);

		GLData GetDocumentInformation(string sysDocID, string voucherID);

		DataSet GetCashFlowReport(DateTime from, DateTime to, string fromLocationID, string toLocationID);

		DataSet GetDailyCashReport(DateTime from, DateTime to, string RegisterID);

		DataSet GetDailyCashSaleReport(DateTime from, DateTime to, string RegisterID);

		DataSet GetAccountTransactionsReport(DateTime from, DateTime to, string fromAccountID, string toAccountID, string fromLocationID, string toLocationID, string groupID, bool isFC, string costCenter, bool allowposting, string fromJob, string toJob, string fromCostCategory, string toCostCategory, string fromProperty, string toProperty, string fromPropertyUnit, string toProprertyUnit, string fromAnalysis, string toAnalysis, string fromDivision, string toDivision);

		DataSet GetProjectAccountTransactionsReport(DateTime from, DateTime to, string fromAccountID, string toAccountID, string fromJob, string toJob, string fromCostCategory, string toCostCategory, string fromLocationID, string toLocationID, bool isFC);

		DataSet GetAnalysisTransactionsReport(DateTime from, DateTime to, string accountID, string fromAnalysis, string toAnalysis, string fromGroup, string toGroup, string fromLocation, string toLocation, string fromDivisionID, string toDivisionID);

		DataSet GetJournalToPrint(string sysDocID, string[] voucherID);

		DataSet GetJournalToPrint(string sysDocID, string voucherID);

		DataSet GetJVList(DateTime from, DateTime to, bool showVoid);

		DataSet GetList(DateTime from, DateTime to, bool showVoid);

		DataSet GetTrialBalanceList(DateTime from, DateTime to, bool showVoid);

		DataSet GetClosingIncomeExpenseList(string fiscalYearID);

		GLData GetBankReconciliationList(string accountId, DateTime fromDate, DateTime toDate, bool reconciled);

		DataSet GetBankReconciliationToPrint(string accountId, DateTime fromDate, DateTime toDate, bool reconciled);

		bool UpdateJournalBankReconciliation(GLData journalData, bool isUpdate = true);

		DataSet GetAccountStatement(string accountID, bool includeReconciled, DateTime from, DateTime to);

		bool MergeJournalDetailsForCOGSUpdate(int journalID, bool mergeSysWithNormal);

		DataSet GetTrialBalanceReportwithAccountHead(DateTime from, DateTime to, string fromLocation, string toLocation, string fromDivisionID, string toDivisionID, bool showZeroBalance, bool allowposting);

		DataSet GetAccountLedgerReport(DateTime from, DateTime to, string fromAccountID, string toAccountID, string groupID, string fromLocationID, string toLocationID, string fromDivisionID, string toDivisionID, bool isFC, string CostCenter, bool allowposting);

		DataSet GetAccountLedgerReportSummary(DateTime from, DateTime to, string fromAccountID, string toAccountID, string groupID, string fromLocationID, string toLocationID, string fromDivisionID, string toDivisionID, bool isFC, string CostCenter, bool allowposting);

		DataSet GetAccountCostCenterReport(DateTime from, DateTime to, string fromAccountID, string toAccountID, string fromLocation, string toLocation, bool isFC, string CostCenter, string fromDivisionID, string toDivisionID);

		DataSet GetBankLedgerReport(DateTime from, DateTime to, string fromAccountID, string toAccountID, string fromLocation, string toLocation, string fromDivision, string toDivision);

		DataSet GetProfitAndLossComparisonReport(DateTime periodfrom1, DateTime periodto1, DateTime periodfrom2, DateTime periodto2, string fromLocation, string toLocation, string fromDivision, string toDivision);

		DataSet GetAccountSnapBalance(string accountID);

		DataSet GetAnalysisTransactionsPivotReport(DateTime from, DateTime to, string fromaccountID, string toaccountID, string fromAnalysis, string toAnalysis, string fromGroup, string toGroup, string fromLocationID, string toLocationID, string fromDivisionID, string toDivisionID);

		DataSet GetJournalReference(string accountID);

		DataSet GetProfitAndLossReportSummaryRevised(DateTime from, DateTime to, string fromLocationID, string toLocationID, string fromDivisionID, string toDivisionID, int level, bool allowposting);

		DataSet GetTrialBalanceAccountList(string Param1, string Param2, DateTime from, DateTime to);

		DataSet GetDetails(string tableName, string DocID, string Value);

		DataSet GetReference(string Table, string ColumnName, string SysdocId, string VoucherNo);

		DataSet GetPropertyAccountTransactionsReport(DateTime from, DateTime to, string fromAccountID, string toAccountID, string fromLocationID, string toLocationID, string fromProperty, string toProperty, string fromPropertyClass, string toPropertyClass, string fromUnit, string toUnit, bool isFC);

		DataSet GetBalanceSheetComparisonReport(DateTime periodto2, string fromLocation, string toLocation, string fromDivision, string toDivision, int level, bool showAccounts, bool allowPosting);
	}
}
