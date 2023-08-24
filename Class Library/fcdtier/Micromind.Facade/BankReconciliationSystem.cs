using Micromind.Common.Data;
using Micromind.Common.Interfaces;
using Micromind.Data;
using System;
using System.Collections;
using System.Data;

namespace Micromind.Facade
{
	public sealed class BankReconciliationSystem : MarshalByRefObject, IBankReconciliationSystem, IDisposable
	{
		private Config config;

		public BankReconciliationSystem(Config config)
		{
			this.config = config;
		}

		public bool InsertUpdateBankReconciliation(BankReconciliationData currentData)
		{
			using (BankReconciliation bankReconciliation = new BankReconciliation(config))
			{
				return bankReconciliation.InsertUpdateBankReconciliation(currentData);
			}
		}

		public BankReconciliationData GetBankReconciliationOpeningList(string accountId)
		{
			using (BankReconciliation bankReconciliation = new BankReconciliation(config))
			{
				return bankReconciliation.GetBankReconciliationOpeningList(accountId);
			}
		}

		public bool DeleteBankReconciliationOpening(string accountId)
		{
			using (BankReconciliation bankReconciliation = new BankReconciliation(config))
			{
				return bankReconciliation.DeleteBankReconciliationOpening(accountId);
			}
		}

		public DataSet GetBankReconciliationToPrint(string accountId)
		{
			using (BankReconciliation bankReconciliation = new BankReconciliation(config))
			{
				return bankReconciliation.GetBankReconciliationToPrint(accountId);
			}
		}

		public DataSet GetBankReconciliationsReport(string fromAccount, string toAccount, string fromLocationID, string toLocationID, string fromDivisionID, string toDivisionID, DateTime asOfDate)
		{
			using (BankReconciliation bankReconciliation = new BankReconciliation(config))
			{
				return bankReconciliation.GetBankReconciliationsReport(fromAccount, toAccount, fromLocationID, toLocationID, fromDivisionID, toDivisionID, asOfDate);
			}
		}

		public DataSet GetBankNotReconciledReport(string fromAccount, string toAccount, string fromLocationID, string toLOcationID, string fromDivisionID, string toDivisionID, DateTime asOfDate)
		{
			using (BankReconciliation bankReconciliation = new BankReconciliation(config))
			{
				return bankReconciliation.GetBankNotReconciledReport(fromAccount, toAccount, fromLocationID, toLOcationID, fromDivisionID, toDivisionID, asOfDate);
			}
		}

		public void Dispose()
		{
		}

		public int ReconcileAccount(int accountID, decimal interestAmount, int interestAccount, decimal serviceChargesAmount, int serviceChargesAccount, decimal beginningBalance, decimal endingBalance, decimal clearedBalance, DateTime statementDate, DateTime reconciliationDate, int currencyID, decimal currencyRate, ArrayList reconciledTransactionIDList, string closingPassword)
		{
			if (interestAmount < 0m)
			{
				throw new ApplicationException("Interest earned amount cannot be negative.");
			}
			if (serviceChargesAmount < 0m)
			{
				throw new ApplicationException("Service charge amount cannot be negative.");
			}
			return 0;
		}

		public int ReconcileAccount(int accountID, decimal interestAmount, int interestAccount, decimal serviceChargesAmount, int serviceChargesAccount, decimal beginningBalance, decimal endingBalance, decimal clearedBalance, DateTime statementDate, DateTime reconciliationDate, int currencyID, decimal currencyRate, ArrayList reconciledTransactionIDList)
		{
			return ReconcileAccount(accountID, interestAmount, interestAccount, serviceChargesAmount, serviceChargesAccount, beginningBalance, endingBalance, clearedBalance, statementDate, reconciliationDate, currencyID, currencyRate, reconciledTransactionIDList, string.Empty);
		}

		public DataSet GetUnreconciledAccountTransactions(int accountID)
		{
			return null;
		}

		public decimal GetLastEndingBalance(int accountID)
		{
			return 0m;
		}

		public DateTime GetLastReconciledDate(int accountID)
		{
			return DateTime.Now;
		}
	}
}
