using Micromind.ClientLibraries;
using Micromind.Common.Libraries;
using Micromind.DataCaches.Libraries;
using System;
using System.Data;
using System.Data.SqlClient;

namespace Micromind.DataCaches.Bankings
{
	public sealed class Banks
	{
		private static DataSet banks;

		private static object syncRoot;

		private static DateTime dateTimeStamp;

		public static event Delegates.RefereshHandler Refereshed;

		public static event EventHandler CacheItemUpdated;

		static Banks()
		{
			Banks.Refereshed = null;
			Banks.CacheItemUpdated = null;
			banks = null;
			syncRoot = new object();
			dateTimeStamp = DateTime.MinValue;
			ChangeEvents.BankChanged += ChangeEvents_Bank;
		}

		internal static void Reset()
		{
			dateTimeStamp = DateTime.MinValue;
			if (banks != null)
			{
				banks.Dispose();
				banks = null;
			}
		}

		public static DataSet GetBanks(bool isReferesh)
		{
			if (Factory.IsDBConnected)
			{
				if ((banks == null) | isReferesh)
				{
					lock (syncRoot)
					{
						if ((banks == null) | isReferesh)
						{
							if (banks != null && isReferesh)
							{
								DateTime tableLastDateTimeStamp = Factory.DatabaseSystem.GetTableLastDateTimeStamp("Bank");
								if (tableLastDateTimeStamp > dateTimeStamp)
								{
									SetData();
									dateTimeStamp = tableLastDateTimeStamp;
								}
							}
							else
							{
								SetData();
							}
						}
					}
				}
			}
			else if (banks == null)
			{
				throw new DBNotConnectedException();
			}
			return banks;
		}

		private static void SetData()
		{
			if (banks != null)
			{
				banks.Dispose();
				banks = null;
			}
			banks = GetData(new string[0]);
			OnRefereshed();
		}

		private static DataSet GetData(string[] ids)
		{
			try
			{
				return Factory.BankSystem.GetBanksByFields(ids, true, "Bank.BankID", "Bank.BankName", "Bank.IsInactive");
			}
			catch (Exception e)
			{
				ErrorHelper.ProcessError(e);
				return null;
			}
		}

		private static void OnRefereshed()
		{
			if (Banks.Refereshed != null)
			{
				Banks.Refereshed();
			}
		}

		private static void OnCacheItemUpdated(object sender, int id, ChangeTypes changeType)
		{
			if (Banks.CacheItemUpdated != null)
			{
				Banks.CacheItemUpdated(sender, new ChangeEventArg(id, changeType));
			}
		}

		private static void ChangeEvents_Bank(object sender, EventArgs e)
		{
		}

		private static void LoadChangedItem(string id, ChangeTypes changeType)
		{
			if (banks != null)
			{
				DataSet dataSet = null;
				try
				{
					dataSet = GetData(new string[1]
					{
						id
					});
					if (dataSet != null && dataSet.Tables.Count != 0 && dataSet.Tables[0].Rows.Count != 0)
					{
						DataRow[] array = banks.Tables["Bank"].Select("BankID=" + id.ToString());
						if (array.Length != 0)
						{
							banks.Tables["Bank"].Rows.Remove(array[0]);
						}
						banks.Merge(dataSet, preserveChanges: true, MissingSchemaAction.Add);
						_ = dataSet.Tables["Bank"].Rows[0];
					}
				}
				catch (SqlException ex)
				{
					ErrorHelper.ProcessError(ex);
				}
				catch (Exception e)
				{
					ErrorHelper.ProcessError(e);
				}
			}
		}

		public static void DeleteItem(int id)
		{
			if (banks != null)
			{
				DataRow[] array = banks.Tables["Bank"].Select("BankID=" + id.ToString());
				if (array.Length != 0)
				{
					banks.Tables["Bank"].Rows.Remove(array[0]);
					OnCacheItemUpdated(null, id, ChangeTypes.DELETE);
				}
			}
		}
	}
}
