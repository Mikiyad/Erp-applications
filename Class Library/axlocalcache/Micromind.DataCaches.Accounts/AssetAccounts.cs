using Micromind.DataCaches.Libraries;
using System.Data;

namespace Micromind.DataCaches.Accounts
{
	public sealed class AssetAccounts
	{
		public static event Delegates.RefereshHandler Refereshed;

		static AssetAccounts()
		{
		}

		public static DataSet GetAssetAccounts(bool isReferesh)
		{
			return AllAccounts.GetAssetAccounts(isReferesh);
		}

		private static void OnRefereshed()
		{
			if (AssetAccounts.Refereshed != null)
			{
				AssetAccounts.Refereshed();
			}
		}
	}
}
