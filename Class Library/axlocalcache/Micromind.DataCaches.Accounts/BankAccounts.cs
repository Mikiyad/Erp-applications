using Micromind.DataCaches.Libraries;
using System.Data;

namespace Micromind.DataCaches.Accounts
{
	public sealed class BankAccounts
	{
		public static event Delegates.RefereshHandler Refereshed;

		static BankAccounts()
		{
		}

		public static DataSet GetBankAccounts(bool isReferesh)
		{
			return AllAccounts.GetBankAccounts(isReferesh);
		}

		private static void OnRefereshed()
		{
			if (BankAccounts.Refereshed != null)
			{
				BankAccounts.Refereshed();
			}
		}
	}
}
