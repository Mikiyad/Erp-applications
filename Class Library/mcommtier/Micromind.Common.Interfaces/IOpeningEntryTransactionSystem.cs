using Micromind.Common.Data;
using System;
using System.Data;

namespace Micromind.Common.Interfaces
{
	public interface IOpeningEntryTransactionSystem
	{
		bool UpdateTransaction(OpenEntryTransactionData transactionData);

		bool UpdateTransaction(OpenEntryTransactionData transactionData, string closingPasword);

		bool CreateTransaction(OpenEntryTransactionData transactionData);

		bool CreateTransaction(OpenEntryTransactionData transactionData, string closingPasword);

		OpenEntryTransactionData GetChequeDepositTransactionByID(string sysDocID, string voucherID);

		OpenEntryTransactionData GetIssuedChequeClearanceTransactionByID(string sysDocID, string voucherID);

		OpenEntryTransactionData GetTransactionByID(string sysDocID, string voucherID);

		bool VoidTransaction(string sysDocID, string voucherID, bool isVoid);

		bool DeleteTransaction(string sysDocID, string voucherID);

		bool DeleteChequeDepositTransaction(string sysDocID, string voucherID);

		bool VoidChequeDepositTransaction(string sysDocID, string voucherID);

		DataSet GetTransactionToPrint(string sysDocID, string[] voucherID);

		DataSet GetTransactionToPrint(string sysDocID, string voucherID);

		DataSet GetExpenseList(DateTime from, DateTime to, bool showVoid);

		DataSet GetDebitNoteList(DateTime from, DateTime to, bool showVoid);

		DataSet GetCreditNoteList(DateTime from, DateTime to, bool showVoid);

		DataSet GetReceiptVoucherList(DateTime from, DateTime to, bool showVoid);

		DataSet GetReturnVoucherList(DateTime from, DateTime to, bool showVoid);

		DataSet GetPaymentVoucherList(DateTime from, DateTime to, bool showVoid, TransactionPaymentType paymentType);

		DataSet GetTransferList(DateTime from, DateTime to, bool showVoid);

		DataSet GetChequeDepositList(DateTime from, DateTime to, bool showVoid);

		DataSet GetReceivedChequeReturnList(DateTime from, DateTime to, bool showVoid);

		bool DeleteIssuedChequeClearanceTransaction(string sysDocID, string voucherID);

		bool VoidIssuedChequeClearanceTransaction(string sysDocID, string voucherID);

		DataSet GetChequeDepositTransactionToPrint(string sysDocID, string voucherID);

		DataSet GetChequeDiscountTransactionToPrint(string sysDocID, string voucherID);

		DataSet GetChequeSentToBankTransactionToPrint(string sysDocID, string voucherID);

		DataSet GetIssuedChequeClearanceTransactionToPrint(string sysDocID, string voucherID);

		DataSet GetChequeReceiptList(DateTime from, DateTime to, bool showVoid);

		DataSet GetChequePaymentList(DateTime from, DateTime to, bool showVoid);

		DataSet GetChequeMaturityReport(DateTime from, DateTime to, string fromBank, string toBank);
	}
}
