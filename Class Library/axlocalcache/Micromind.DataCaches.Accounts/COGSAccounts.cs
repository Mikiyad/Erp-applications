using Micromind.DataCaches.Libraries;
using System.Data;

namespace Micromind.DataCaches.Accounts
{
	public sealed class COGSAccounts
	{
		public static event Delegates.RefereshHandler Refereshed;

		static COGSAccounts()
		{
		}

		public static DataSet GetCOGSAccounts(bool isReferesh)
		{
			return AllAccounts.GetCOGSAccounts(isReferesh);
		}

		private static void OnRefereshed()
		{
			if (COGSAccounts.Refereshed != null)
			{
				COGSAccounts.Refereshed();
			}
		}
	}
}
