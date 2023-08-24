using Micromind.Common.Data;
using System;
using System.Collections;
using System.Data;

namespace Micromind.Common.Interfaces
{
	public interface IBankReconciliationSystem
	{
		bool InsertUpdateBankReconciliation(BankReconciliationData currentData);

		BankReconciliationData GetBankReconciliationOpeningList(string accountId);

		bool DeleteBankReconciliationOpening(string accountId);

		DataSet GetBankReconciliationToPrint(string accountId);

		DataSet GetBankReconciliationsReport(string fromAccount, string toAccount, string fromLocationID, string toLocationID, string fromDivisionID, string toDivisionID, DateTime asOfDate);

		DataSet GetBankNotReconciledReport(string fromAccount, string toAccount, string fromLocationID, string toLOcationID, string fromDivisionID, string toDivisionID, DateTime asOfDate);

		int ReconcileAccount(int accountID, decimal interestAmount, int interestAccount, decimal serviceChargesAmount, int serviceChargesAccount, decimal beginningBalance, decimal endingBalance, decimal clearedBalance, DateTime statementDate, DateTime reconciliationDate, int currencyID, decimal currencyRate, ArrayList reconciledTransactionsIDList);

		int ReconcileAccount(int accountID, decimal interestAmount, int interestAccount, decimal serviceChargesAmount, int serviceChargesAccount, decimal beginningBalance, decimal endingBalance, decimal clearedBalance, DateTime statementDate, DateTime reconciliationDate, int currencyID, decimal currencyRate, ArrayList reconciledTransactionIDList, string closingPassword);

		DataSet GetUnreconciledAccountTransactions(int accountID);

		decimal GetLastEndingBalance(int accountID);

		DateTime GetLastReconciledDate(int accountID);
	}
}
