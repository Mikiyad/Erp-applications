using Micromind.DataCaches.Libraries;
using System.Data;

namespace Micromind.DataCaches.Accounts
{
	public sealed class IncomeAccounts
	{
		public static event Delegates.RefereshHandler Refereshed;

		static IncomeAccounts()
		{
		}

		public static DataSet GetIncomeAccounts(bool isReferesh)
		{
			return AllAccounts.GetIncomeAccounts(isReferesh);
		}

		private static void OnRefereshed()
		{
			if (IncomeAccounts.Refereshed != null)
			{
				IncomeAccounts.Refereshed();
			}
		}
	}
}
