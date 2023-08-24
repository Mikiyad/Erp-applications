using Micromind.ClientLibraries;
using Micromind.Common.Libraries;
using Micromind.DataCaches.Libraries;
using System;
using System.Data;

namespace Micromind.DataCaches.Accounts
{
	public sealed class AllAccounts
	{
		private static DataSet allAccounts;

		private static object syncRoot;

		private static DateTime dateTimeStamp;

		public static event Delegates.RefereshHandler Refereshed;

		public static event EventHandler CacheItemUpdated;

		static AllAccounts()
		{
			AllAccounts.Refereshed = null;
			AllAccounts.CacheItemUpdated = null;
			allAccounts = null;
			syncRoot = new object();
			dateTimeStamp = DateTime.MinValue;
		}

		internal static void Reset()
		{
			dateTimeStamp = DateTime.MinValue;
			if (allAccounts != null)
			{
				allAccounts.Dispose();
				allAccounts = null;
			}
		}

		public static DataSet GetAllAccounts(bool isReferesh)
		{
			CheckForData(isReferesh);
			return allAccounts;
		}

		private static void SetData()
		{
			if (allAccounts != null)
			{
				allAccounts.Dispose();
				allAccounts = null;
			}
			allAccounts = Factory.CompanyAccountSystem.GetAccounts();
			OnRefereshed();
		}

		private static void OnRefereshed()
		{
			if (AllAccounts.Refereshed != null)
			{
				AllAccounts.Refereshed();
			}
		}

		private static void CheckForData(bool isReferesh)
		{
			if (Factory.IsDBConnected)
			{
				if ((allAccounts == null) | isReferesh)
				{
					lock (syncRoot)
					{
						if ((allAccounts == null) | isReferesh)
						{
							if (allAccounts != null && isReferesh)
							{
								GetLatestData();
							}
							else
							{
								SetData();
							}
						}
					}
				}
			}
			else if (allAccounts == null)
			{
				throw new DBNotConnectedException();
			}
		}

		private static void GetLatestData()
		{
			DateTime tableLastDateTimeStamp = Factory.DatabaseSystem.GetTableLastDateTimeStamp("Account", "Journal");
			if (tableLastDateTimeStamp > dateTimeStamp)
			{
				SetData();
				dateTimeStamp = tableLastDateTimeStamp;
			}
		}

		public static DataSet GetAPAccounts(bool isReferesh)
		{
			CheckForData(isReferesh);
			if (allAccounts == null)
			{
				return null;
			}
			DataRow[] array = allAccounts.Tables["Account"].Select("TypeID=" + ((byte)11).ToString());
			DataSet dataSet = new DataSet();
			if (array != null && array.Length != 0)
			{
				dataSet.Merge(array);
			}
			else
			{
				dataSet.Tables.Add(new DataTable("Account"));
			}
			return dataSet;
		}

		public static DataSet GetARAccounts(bool isReferesh)
		{
			CheckForData(isReferesh);
			if (allAccounts == null)
			{
				return null;
			}
			DataRow[] array = allAccounts.Tables["Account"].Select("TypeID=" + ((byte)2).ToString());
			DataSet dataSet = new DataSet();
			if (array != null && array.Length != 0)
			{
				dataSet.Merge(array);
			}
			else
			{
				dataSet.Tables.Add(new DataTable("Account"));
			}
			return dataSet;
		}

		public static DataSet GetAssetAccounts(bool isReferesh)
		{
			CheckForData(isReferesh);
			if (allAccounts == null)
			{
				return null;
			}
			DataRow[] array = allAccounts.Tables["Account"].Select("TypeID=" + ((byte)8).ToString() + " OR TypeID=" + ((byte)10).ToString() + " OR TypeID=" + ((byte)22).ToString() + " OR TypeID=" + ((byte)1).ToString() + " OR TypeID=" + ((byte)9).ToString());
			DataSet dataSet = new DataSet();
			if (array != null && array.Length != 0)
			{
				dataSet.Merge(array);
			}
			else
			{
				dataSet.Tables.Add(new DataTable("Account"));
			}
			return dataSet;
		}

		public static DataSet GetBankAccounts(bool isReferesh)
		{
			CheckForData(isReferesh);
			if (allAccounts == null)
			{
				return null;
			}
			DataRow[] array = allAccounts.Tables["Account"].Select("TypeID=" + ((byte)22).ToString() + " OR TypeID=" + ((byte)1).ToString());
			DataSet dataSet = new DataSet();
			if (array != null && array.Length != 0)
			{
				dataSet.Merge(array);
			}
			else
			{
				dataSet.Tables.Add(new DataTable("Account"));
			}
			return dataSet;
		}

		public static DataSet GetCOGSAccounts(bool isReferesh)
		{
			CheckForData(isReferesh);
			if (allAccounts == null)
			{
				return null;
			}
			DataRow[] array = allAccounts.Tables["Account"].Select("TypeID=" + ((byte)18).ToString());
			DataSet dataSet = new DataSet();
			if (array != null && array.Length != 0)
			{
				dataSet.Merge(array);
			}
			else
			{
				dataSet.Tables.Add(new DataTable("Account"));
			}
			return dataSet;
		}

		public static DataSet GetCurrentAssetsAccounts(bool isReferesh)
		{
			CheckForData(isReferesh);
			if (allAccounts == null)
			{
				return null;
			}
			DataRow[] array = allAccounts.Tables["Account"].Select("TypeID=" + ((byte)8).ToString() + " OR TypeID=" + ((byte)22).ToString() + " OR TypeID=" + ((byte)1).ToString());
			DataSet dataSet = new DataSet();
			if (array != null && array.Length != 0)
			{
				dataSet.Merge(array);
			}
			else
			{
				dataSet.Tables.Add(new DataTable("Account"));
			}
			return dataSet;
		}

		public static DataSet GetExpenseAccounts(bool isReferesh)
		{
			CheckForData(isReferesh);
			if (allAccounts == null)
			{
				return null;
			}
			DataRow[] array = allAccounts.Tables["Account"].Select("TypeID=" + ((byte)19).ToString() + " OR TypeID=" + ((byte)21).ToString());
			DataSet dataSet = new DataSet();
			if (array != null && array.Length != 0)
			{
				dataSet.Merge(array);
			}
			else
			{
				dataSet.Tables.Add(new DataTable("Account"));
			}
			return dataSet;
		}

		public static DataSet GetIncomeAccounts(bool isReferesh)
		{
			CheckForData(isReferesh);
			if (allAccounts == null)
			{
				return null;
			}
			DataRow[] array = allAccounts.Tables["Account"].Select("TypeID=" + ((byte)17).ToString() + " OR TypeID=" + ((byte)20).ToString());
			DataSet dataSet = new DataSet();
			if (array != null && array.Length != 0)
			{
				dataSet.Merge(array);
			}
			else
			{
				dataSet.Tables.Add(new DataTable("Account"));
			}
			return dataSet;
		}

		public static DataSet GetIncomeExpenseAccounts(bool isReferesh)
		{
			CheckForData(isReferesh);
			if (allAccounts == null)
			{
				return null;
			}
			DataSet incomeAccounts = GetIncomeAccounts(isReferesh);
			incomeAccounts.Merge(GetExpenseAccounts(isReferesh));
			return incomeAccounts;
		}
	}
}
