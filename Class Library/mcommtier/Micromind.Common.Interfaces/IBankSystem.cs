using Micromind.Common.Data;
using System.Data;

namespace Micromind.Common.Interfaces
{
	public interface IBankSystem
	{
		bool CreateBank(BankData bankData);

		bool CreateBank(string bankName, string contactName, string contactTitle, string address, string city, string postalCode, string country, string phone, string fax);

		bool CreateBank(string bankName);

		bool UpdateBank(BankData bankData);

		bool CreateUpdateBankBatch(DataSet listData, bool checkConcurrency);

		BankData GetBanks();

		DataSet GetBankList();

		bool DeleteBank(string bankID);

		DataSet GetBanksByFields(params string[] columns);

		DataSet GetBanksByFields(string[] banksID, params string[] columns);

		DataSet GetBanksByFields(string[] banksID, bool isInactive, params string[] columns);

		BankData GetBankByID(string bankID);

		bool ExistBank(string bankName);

		DataSet GetBankComboList();
	}
}
