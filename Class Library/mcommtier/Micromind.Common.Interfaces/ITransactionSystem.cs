using Micromind.Common.Data;
using System;
using System.Data;

namespace Micromind.Common.Interfaces
{
	public interface ITransactionSystem
	{
		bool UpdateTransaction(TransactionData transactionData);

		bool UpdateTransaction(TransactionData transactionData, string closingPasword);

		bool CreateTransaction(TransactionData transactionData);

		bool CreateTransaction(TransactionData transactionData, string closingPasword);

		bool InsertUpdateFundTransferTransaction(TransactionData transactionData, bool isUpdate);

		bool DepositCheques(TransactionData transactionData, int[] chequeIDs, bool isUpdate);

		TransactionData GetChequeDepositTransactionByID(string sysDocID, string voucherID);

		TransactionData GetIssuedChequeClearanceTransactionByID(string sysDocID, string voucherID);

		TransactionData GetTransactionByID(string sysDocID, string voucherID);

		bool VoidTransaction(string sysDocID, string voucherID, bool isVoid);

		bool DeleteTransaction(string sysDocID, string voucherID);

		bool DeleteChequeDepositTransaction(string sysDocID, string voucherID);

		bool VoidChequeDepositTransaction(string sysDocID, string voucherID);

		bool InsertUpdateExpenseTransaction(TransactionData transactionData, bool isUpdate);

		bool InsertUpdateDebitCreditNoteTransaction(TransactionData transactionData, bool isUpdate);

		bool InsertUpdateReturnedCheque(TransactionData transactionData);

		bool InsertUpdateCancelledReceivedCheque(TransactionData transactionData);

		bool InsertUpdateChequeChangeStatus(TransactionData transactionData);

		bool ClearIssuedCheques(TransactionData transactionData, int[] chequeIDs);

		bool CancellIssuedCheque(TransactionData transactionData, bool isUpdate);

		bool ReturnIssuedCheque(TransactionData transactionData, bool isUpdate);

		DataSet GetTransactionToPrint(string sysDocID, string[] voucherID);

		DataSet GetTransactionToPrint(string sysDocID, string voucherID);

		DataSet GetExpenseList(DateTime from, DateTime to, bool showVoid, string SysDocID);

		DataSet GetDebitNoteList(DateTime from, DateTime to, bool showVoid);

		DataSet GetCreditNoteList(DateTime from, DateTime to, bool showVoid);

		DataSet GetReceiptVoucherList(DateTime from, DateTime to, bool showVoid);

		DataSet GetReceiptVoucherMultipleList(DateTime from, DateTime to, bool showVoid);

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

		bool InsertUpdateChequeReceiptMultipleTransaction(TransactionData transactionData, bool isUpdate);

		DataSet GetChequeReceiptMultipleList(DateTime from, DateTime to, bool showVoid);
	}
}
