using Micromind.Common.Data;
using System;
using System.Data;

namespace Micromind.Common.Interfaces
{
	public interface IBankFacilityTransactionSystem
	{
		bool UpdateBankFacilityTransaction(BankFacilityTransactionData transactionData);

		bool UpdateBankFacilityTransaction(BankFacilityTransactionData transactionData, string closingPasword);

		bool CreateBankFacilityTransaction(BankFacilityTransactionData transactionData);

		bool CreateBankFacilityTransaction(BankFacilityTransactionData transactionData, string closingPasword);

		BankFacilityTransactionData GetBankFacilityTransactionByID(string sysDocID, string voucherID);

		bool VoidBankFacilityTransaction(string sysDocID, string voucherID, bool isVoid);

		bool DeleteBankFacilityTransaction(string sysDocID, string voucherID);

		DataSet GetBankFacilityTransactionToPrint(string sysDocID, string[] voucherID);

		DataSet GetBankFacilityTransactionToPrint(string sysDocID, string voucherID);

		DataSet GetOpenTransactions(BankFacilityTypes type);

		DataSet GetList(DateTime from, DateTime to, BankFacilityTypes facilityType, bool showVoid);

		DataSet GetList(BankFacilityTypes facilityType, bool showVoid);

		bool UpdateTREntryTransaction(BankFacilityTransactionData transactionData);
	}
}
