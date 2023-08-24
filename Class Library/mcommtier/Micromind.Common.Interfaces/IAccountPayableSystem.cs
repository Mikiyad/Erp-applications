using Micromind.Common.Data;
using System;
using System.Data;

namespace Micromind.Common.Interfaces
{
	public interface IAccountPayableSystem
	{
		bool CreateAccountPayable(APJournalData accountPayableData);

		bool CreateAccountPayable(APJournalData accountPayableData, string closingPassword);

		bool CreateAccountPayable(TransactionData transactionData, APJournalData accountPayableData, int accountID);

		bool CreateAccountPayable(TransactionData transactionData, APJournalData accountPayableData, int accountID, string closingPassword);

		bool UpdateAccountPayable(TransactionData transactionData, APJournalData accountPayableData, int accountID);

		bool UpdateAccountPayable(TransactionData transactionData, APJournalData accountPayableData, int accountID, string closingPassword);

		DataSet GetUnPaidInvoices(int vendorID, int apAccountID);

		DataSet GetUnusedCredits(int vendorID, int apAccountID, int excludedTransactionID);

		DataSet GetUnusedCredits(int[] vendorsID, int apAccountID, int excludedTransactionID, DateTime from, DateTime to);

		DataSet GetAPInvoiceBalance(int invoiceID);

		DataSet GetInvoicesBalance();

		DataSet GetInvoicesBalance(int vendorID);

		DataSet GetInvoicesBalance(DateTime from, DateTime to);

		DataSet GetInvoicesBalance(int vendorID, DateTime from, DateTime to);

		DataSet GetInvoicesBalance(int[] vendorsID, DateTime from, DateTime to);

		DataSet GetVendorsBalance();

		DataSet GetVendorsBalance(int vendorID, DateTime from, DateTime to);

		DataSet GetVendorsBalance(int[] vendorsID, DateTime from, DateTime to);

		DataSet GetVendorsBalance(DateTime from, DateTime to);

		DataSet GetVendorsBalance(int vendorID);

		DataSet GetAPTransactions();

		DataSet GetAPTransactions(int apAccountID);

		DataSet GetAPTransactions(int apAccountID, DateTime from, DateTime to);

		string GetTransactionIDByAPID(string apID);

		string GetLetterOfCreditIDByAPID(string apID);

		string GetAPAccountIDByTransactionID(string transactionID);

		DataSet GetPaymentsByTransactionID(int transactionID);

		DataSet GetVendorTransactions();

		DataSet GetVendorTransactions(DateTime from, DateTime to);

		DataSet GetVendorTransactions(int vendorID);

		DataSet GetVendorTransactions(int vendorID, DateTime from, DateTime to);

		DataSet GetVendorTransactions(int[] vendorsID, DateTime from, DateTime to);

		DataSet GetInvoiceTransactions(int invoiceID);
	}
}
