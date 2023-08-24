using Micromind.Common.Data;
using Micromind.Common.Libraries;
using System;
using System.Data;

namespace Micromind.Common.Interfaces
{
	public interface ITransactionCheck
	{
		bool UpdateTransactionCheck(TransactionCheckData transactionCheckData);

		bool CreateTransactionCheck(TransactionCheckData transactionCheckData);

		DataSet GetReceivedChecks(int partnerID);

		DataSet GetReceivedChecks();

		DataSet GetReceivedChecks(int partnerID, DateTime from, DateTime to);

		DataSet GetReceivedChecks(DateTime from, DateTime to);

		DataSet GetPaidChecks(int partnerID);

		DataSet GetPaidChecks();

		DataSet GetPaidChecks(int partnerID, DateTime from, DateTime to);

		DataSet GetPaidChecks(DateTime from, DateTime to);

		DataSet GetPaidChecks(int[] partnersID, DateTime from, DateTime to);

		bool BounceCustomerCheck(string checkID, bool isReasonBankCredit, string description);

		bool VoidCheck(string checkID, string description);

		bool UnBounceCustomerCheck(string checkID);

		bool ClearVoidCheck(string checkID);

		bool UpdateCheckStatus(string checkID, CheckStatus statu);

		DataSet GetItemByNumber(CheckTypes checkType, string number);

		DataSet GetItemByCriteria(CheckTypes checkType, string number, string[] names, DateTime from, DateTime to, decimal amount, AmountCriteria amountCriteria, string reference, string description);

		DataSet GetUndepositedReceivedChecks(DateTime from, DateTime to);

		DataSet GetUndepositedPaidChecks(DateTime from, DateTime to);

		DataSet GetReceivedChecks(int[] partnersID, DateTime from, DateTime to);
	}
}
