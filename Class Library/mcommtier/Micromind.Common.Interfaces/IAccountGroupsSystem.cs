using Micromind.Common.Data;
using System.Data;

namespace Micromind.Common.Interfaces
{
	public interface IAccountGroupsSystem
	{
		bool CreateAccountGroup(AccountGroupsData accountGroupData);

		bool UpdateAccountGroup(AccountGroupsData accountGroupData);

		AccountGroupsData GetAccountGroups();

		bool DeleteAccountGroup(string groupID);

		AccountGroupsData GetAccountGroupByID(string id);

		int GetGroupTypeID(string groupID);

		DataSet GetAccountGroupsByFields(params string[] columns);

		DataSet GetAccountGroupsByFields(string[] ids, params string[] columns);

		DataSet GetAccountGroupsByFields(string[] ids, bool isInactive, params string[] columns);

		DataSet GetAccountGroupsList();

		DataSet GetAccountGroupsComboList();
	}
}
