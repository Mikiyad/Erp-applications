using Micromind.Common.Data;
using Micromind.Common.Interfaces;
using Micromind.Data;
using System;
using System.Data;

namespace Micromind.Facade
{
	public sealed class AccountGroupsSystem : MarshalByRefObject, IAccountGroupsSystem, IDisposable
	{
		private Config config;

		public AccountGroupsSystem(Config config)
		{
			this.config = config;
		}

		public void Dispose()
		{
		}

		public bool CreateAccountGroup(AccountGroupsData data)
		{
			return new AccountGroups(config).InsertAccountGroup(data);
		}

		public bool UpdateAccountGroup(AccountGroupsData data)
		{
			return UpdateAccountGroup(data, checkConcurrency: false);
		}

		public bool UpdateAccountGroup(AccountGroupsData data, bool checkConcurrency)
		{
			return new AccountGroups(config).UpdateAccountGroup(data);
		}

		public AccountGroupsData GetAccountGroups()
		{
			using (AccountGroups accountGroups = new AccountGroups(config))
			{
				return accountGroups.GetAccountGroups();
			}
		}

		public bool DeleteAccountGroup(string groupID)
		{
			using (AccountGroups accountGroups = new AccountGroups(config))
			{
				return accountGroups.DeleteAccountGroup(groupID);
			}
		}

		public AccountGroupsData GetAccountGroupByID(string id)
		{
			using (AccountGroups accountGroups = new AccountGroups(config))
			{
				return accountGroups.GetAccountGroupByID(id);
			}
		}

		public int GetGroupTypeID(string groupID)
		{
			using (AccountGroups accountGroups = new AccountGroups(config))
			{
				return accountGroups.GetGroupTypeID(groupID);
			}
		}

		public DataSet GetAccountGroupsByFields(params string[] columns)
		{
			using (AccountGroups accountGroups = new AccountGroups(config))
			{
				return accountGroups.GetAccountGroupsByFields(columns);
			}
		}

		public DataSet GetAccountGroupsByFields(string[] ids, params string[] columns)
		{
			using (AccountGroups accountGroups = new AccountGroups(config))
			{
				return accountGroups.GetAccountGroupsByFields(ids, columns);
			}
		}

		public DataSet GetAccountGroupsByFields(string[] ids, bool isInactive, params string[] columns)
		{
			using (AccountGroups accountGroups = new AccountGroups(config))
			{
				return accountGroups.GetAccountGroupsByFields(ids, isInactive, columns);
			}
		}

		public DataSet GetAccountGroupsList()
		{
			using (AccountGroups accountGroups = new AccountGroups(config))
			{
				return accountGroups.GetAccountGroupsList();
			}
		}

		public DataSet GetAccountGroupsComboList()
		{
			using (AccountGroups accountGroups = new AccountGroups(config))
			{
				return accountGroups.GetAccountGroupsComboList();
			}
		}
	}
}
