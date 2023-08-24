using Micromind.ClientLibraries;
using System;
using System.Data;

namespace Micromind.DataCaches.Accounts
{
	public sealed class AccountGroups
	{
		private static DataSet accountGroups;

		static AccountGroups()
		{
		}

		internal static void Reset()
		{
			if (accountGroups != null)
			{
				accountGroups.Dispose();
				accountGroups = null;
			}
		}

		public static DataSet GetAccountGroups(bool isReferesh)
		{
			if (Factory.IsDBConnected)
			{
				if ((accountGroups == null) | isReferesh)
				{
					SetData();
				}
				return accountGroups;
			}
			return null;
		}

		private static void SetData()
		{
			if (accountGroups != null)
			{
				accountGroups.Dispose();
				accountGroups = null;
			}
			accountGroups = GetData();
		}

		private static DataSet GetData()
		{
			try
			{
				return Factory.AccountGroupsSystem.GetAccountGroupsComboList();
			}
			catch (Exception e)
			{
				ErrorHelper.ProcessError(e);
				return null;
			}
		}
	}
}
