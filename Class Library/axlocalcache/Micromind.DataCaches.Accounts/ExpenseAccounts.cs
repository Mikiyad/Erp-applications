using Micromind.DataCaches.Libraries;
using System.Data;

namespace Micromind.DataCaches.Accounts
{
	public sealed class ExpenseAccounts
	{
		public static event Delegates.RefereshHandler Refereshed;

		static ExpenseAccounts()
		{
		}

		public static DataSet GetExpenseAccounts(bool isReferesh)
		{
			return AllAccounts.GetExpenseAccounts(isReferesh);
		}

		private static void OnRefereshed()
		{
			if (ExpenseAccounts.Refereshed != null)
			{
				ExpenseAccounts.Refereshed();
			}
		}
	}
}
