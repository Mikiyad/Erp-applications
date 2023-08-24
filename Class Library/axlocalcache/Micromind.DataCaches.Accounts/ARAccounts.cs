using Micromind.DataCaches.Libraries;
using System.Data;

namespace Micromind.DataCaches.Accounts
{
	public sealed class ARAccounts
	{
		public static event Delegates.RefereshHandler Refereshed;

		static ARAccounts()
		{
		}

		internal static void Reset()
		{
		}

		public static DataSet GetARAccounts(bool isReferesh)
		{
			return AllAccounts.GetARAccounts(isReferesh);
		}

		private static void OnRefereshed()
		{
			if (ARAccounts.Refereshed != null)
			{
				ARAccounts.Refereshed();
			}
		}
	}
}
