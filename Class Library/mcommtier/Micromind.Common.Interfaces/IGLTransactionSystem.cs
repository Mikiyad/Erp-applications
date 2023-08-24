using Micromind.Common.Data;
using Micromind.Common.Libraries;
using System;
using System.Data;

namespace Micromind.Common.Interfaces
{
	public interface IGLTransactionSystem
	{
		bool CreateGLTransaction(GLData glData, string closingPassword);

		bool CreateGLTransaction(GLData glData);

		bool UpdateGL(GLData glData, string closingPassword);

		bool UpdateGL(GLData glData);

		DataSet GetGLDataByGLID(int glID);

		DataSet[] GetGLDataByGLID(int[] glsID);

		DataSet GetJournalReport();

		DataSet GetJournalReport(DateTime from, DateTime to);

		DataSet GetGLReport();

		DataSet GetGLReport(DateTime from, DateTime to);

		DataSet GetTrialBalance();

		DataSet GetJournalReport(int accountID);

		DataSet GetJournalReport(int accountID, DateTime from, DateTime to);

		DataSet GetJournalReport(int[] accountsID, DateTime from, DateTime to);

		DataSet GetJournalReport(int[] accountsID, int[] partnersID, DateTime from, DateTime to);

		DataSet GetGLReport(int accountID);

		DataSet GetTrialBalance(DateTime from, DateTime to);

		DataSet GetIncomeExpenseBalance(int[] jobID);

		DataSet GetIncomeExpenseTransactions(int[] jobID, bool jobsOnly);

		DataSet GetIncomeExpenseTransactions(int[] jobID, DateTime from, DateTime to, bool jobsOnly);

		DataSet GetIncomeExpenseBalance(int[] jobID, DateTime from, DateTime to);

		DataSet GetAssetAccountBalance();

		DataSet GetAssetAccountBalance(int[] accountsID, bool isInactive, params string[] columns);

		DataSet GetEquityLiabilityAccountBalance(int[] accountsID, bool isInactive, params string[] columns);

		DataSet GetAccountsBalance();

		DataSet GetAccountsBalance(int[] accountsID, bool isInactive, params string[] columns);

		DataSet GetGLDataByRefernce(string reference);

		DataSet GetItemByCriteria(string reference, string[] names, DateTime from, DateTime to, decimal amount, AmountCriteria amountCriteria, string description);

		DataSet GetJournals();

		DataSet GetJournals(DateTime from, DateTime to);

		DataSet GetJournals(int[] accountsID, DateTime from, DateTime to);

		bool DeleteJournal(int glID);

		bool DeleteJournal(int glID, string closingPassword);

		DataSet GetBankBalance(int[] accountsID, DateTime from, DateTime to);

		DataSet GetBankBalanceDetail(int[] accountsID, DateTime from, DateTime to);

		DataSet GetAccountsBalance(CompanyAccountTypes[] accountTypes, DateTime from, DateTime to);

		DataSet GetJobProfitabilitySummary(int[] jobID, DateTime from, DateTime to);

		DataSet GetJobTypeProfitabilitySummary(int[] jobTypesID, DateTime from, DateTime to);

		DataSet GetIncomeExpenseTransactionsByJobType(int[] jobTypeID, DateTime from, DateTime to);

		bool CreateGLTransactionBatch(DataSet listData);

		SysDocTypes GetGLTypeByName(string glTypeName);

		DataSet GetGLTransactionsSummary(SysDocTypes[] glTypes, DateTime from, DateTime to);

		DataSet GetGLTypes();

		DataSet GetGLReport(int[] accountsID, DateTime from, DateTime to);

		DataSet GetGLReport(int[] accountsID, int[] partnersID, DateTime from, DateTime to);

		DataSet[] GetTwoTrialBalance(DateTime from1, DateTime to1, DateTime from2, DateTime to2);

		DataSet[] GetTwoIncomeExpenseBalance(int[] jobID1, DateTime from1, DateTime to1, int[] jobID2, DateTime from2, DateTime to2);

		string GetAutoReferenceNumber();
	}
}
