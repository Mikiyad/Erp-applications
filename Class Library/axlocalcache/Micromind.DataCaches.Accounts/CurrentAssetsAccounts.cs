using Micromind.DataCaches.Libraries;
using System.Data;

namespace Micromind.DataCaches.Accounts
{
	public sealed class CurrentAssetsAccounts
	{
		public static event Delegates.RefereshHandler Refereshed;

		static CurrentAssetsAccounts()
		{
		}

		public static DataSet GetCurrentAssetsAccounts(bool isReferesh)
		{
			return AllAccounts.GetCurrentAssetsAccounts(isReferesh);
		}

		private static void OnRefereshed()
		{
			if (CurrentAssetsAccounts.Refereshed != null)
			{
				CurrentAssetsAccounts.Refereshed();
			}
		}
	}
}
