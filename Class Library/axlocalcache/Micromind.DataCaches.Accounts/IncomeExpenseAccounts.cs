using Micromind.DataCaches.Libraries;
using System.Data;

namespace Micromind.DataCaches.Accounts
{
	public sealed class IncomeExpenseAccounts
	{
		public static event Delegates.RefereshHandler Refereshed;

		static IncomeExpenseAccounts()
		{
		}

		public static DataSet GetIncomeExpenseAccounts(bool isReferesh)
		{
			return AllAccounts.GetIncomeExpenseAccounts(isReferesh);
		}

		private static void OnRefereshed()
		{
			if (IncomeExpenseAccounts.Refereshed != null)
			{
				IncomeExpenseAccounts.Refereshed();
			}
		}
	}
}
