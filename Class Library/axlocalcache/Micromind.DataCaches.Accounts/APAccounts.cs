using Micromind.DataCaches.Libraries;
using System;
using System.Data;

namespace Micromind.DataCaches.Accounts
{
	public sealed class APAccounts
	{
		private static object syncRoot;

		private static DateTime dateTimeStamp;

		public static event Delegates.RefereshHandler Refereshed;

		static APAccounts()
		{
			APAccounts.Refereshed = null;
			syncRoot = new object();
			dateTimeStamp = DateTime.MinValue;
		}

		internal static void Reset()
		{
		}

		public static DataSet GetAPAccounts(bool isReferesh)
		{
			return AllAccounts.GetAPAccounts(isReferesh);
		}

		private static void OnRefereshed()
		{
			if (APAccounts.Refereshed != null)
			{
				APAccounts.Refereshed();
			}
		}
	}
}
