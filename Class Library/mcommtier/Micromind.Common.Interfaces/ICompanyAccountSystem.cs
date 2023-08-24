using Micromind.Common.Data;
using System.Data;

namespace Micromind.Common.Interfaces
{
	public interface ICompanyAccountSystem
	{
		bool CreateCompanyAccount(CompanyAccountData companyAccountData);

		bool UpdateCompanyAccount(CompanyAccountData companyAccountData);

		bool UpdateCompanyAccount(CompanyAccountData companyAccountData, CustomFieldData customFieldData);

		DataSet GetAccounts();

		bool DeleteAccount(string accountID);

		DataSet GetAccountsByFields(params string[] columns);

		DataSet GetAccountsByFields(int[] accountsID, params string[] columns);

		DataSet GetARAccounts();

		DataSet GetBankAccounts();

		DataSet GetAPAccounts();

		DataSet GetCOGSAccounts();

		DataSet GetIncomeAccounts();

		DataSet GetOtherCurrentAssetAccounts();

		DataSet GetAccounts(params string[] accountTypeFields);

		DataSet GetAccountsName();

		bool ExistAccountName(string shortName);

		bool ExistAccountID(int accountID);

		DataSet GetExpenseIncomeAccounts();

		bool ActivateAccount(object id, bool activate);

		CompanyAccountData GetAccountByID(string id);

		DataSet GetExpenseAccounts();

		bool IsParent(object parentAccount, object subAccount);

		bool CreateUpdateCompanyAccountBatch(DataSet listData, bool checkConcurrency);

		bool ExistAccountName(string accountName, int parentID);

		bool ExistAccountNumber(string accountNumber);

		int GetParentLevel(object parentID);

		DataSet GetSystemAccounts();

		DataSet GetAccountsList(bool includeInactive, bool isHierarchy);

		DataSet GetAccountsComboList();

		string GetNextAccountNumber(string groupID);

		DataSet GetAccountsListHierarchy(bool includeInactive);

		DataSet GetFavoriteAccounts();

		DataSet GetAccountListReport();

		decimal GetAccountBalance(string accountID, bool includeOD);
	}
}
