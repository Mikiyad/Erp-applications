using Micromind.ClientLibraries;
using Micromind.Common.Libraries;
using Micromind.DataCaches.Libraries;
using System;
using System.Data;
using System.Data.SqlClient;

namespace Micromind.DataCaches.Others
{
	public sealed class PriceLevels
	{
		private static DataSet priceLevels;

		private static object syncRoot;

		private static DateTime dateTimeStamp;

		public static event Delegates.RefereshHandler Refereshed;

		public static event EventHandler CacheItemUpdated;

		static PriceLevels()
		{
			PriceLevels.Refereshed = null;
			PriceLevels.CacheItemUpdated = null;
			priceLevels = null;
			syncRoot = new object();
			dateTimeStamp = DateTime.MinValue;
			ChangeEvents.PriceLevelChanged += ChangeEvents_PriceLevelChanged;
		}

		internal static void Reset()
		{
			dateTimeStamp = DateTime.MinValue;
			if (priceLevels != null)
			{
				priceLevels.Dispose();
				priceLevels = null;
			}
		}

		public static DataSet GetPriceLevels(bool isReferesh)
		{
			if (Micromind.ClientLibraries.Global.ConStatus == ConnectionStatus.Connected)
			{
				if ((priceLevels == null) | isReferesh)
				{
					lock (syncRoot)
					{
						if ((priceLevels == null) | isReferesh)
						{
							if (priceLevels != null && isReferesh)
							{
								DateTime tableLastDateTimeStamp = Factory.DatabaseSystem.GetTableLastDateTimeStamp("Price_Level");
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
			else if (priceLevels == null)
			{
				throw new DBNotConnectedException();
			}
			return priceLevels;
		}

		private static void SetData()
		{
			if (priceLevels != null)
			{
				priceLevels.Dispose();
				priceLevels = null;
			}
			priceLevels = GetData(new int[0]);
			OnRefereshed();
		}

		private static DataSet GetData(int[] ids)
		{
			try
			{
				return Factory.PriceLevelSystem.GetPriceLevelsByFields(true, ids, "Price_Level.PriceLevelID", "Price_Level.PriceLevelName");
			}
			catch (Exception e)
			{
				ErrorHelper.ProcessError(e);
				return null;
			}
		}

		private static void OnRefereshed()
		{
			if (PriceLevels.Refereshed != null)
			{
				PriceLevels.Refereshed();
			}
		}

		private static void OnCacheItemUpdated(object sender, int id, ChangeTypes changeType)
		{
			if (PriceLevels.CacheItemUpdated != null)
			{
				PriceLevels.CacheItemUpdated(sender, new ChangeEventArg(id, changeType));
			}
		}

		private static void ChangeEvents_PriceLevelChanged(object sender, EventArgs e)
		{
			ChangeEventArg changeEventArg = e as ChangeEventArg;
			if (changeEventArg != null)
			{
				if (changeEventArg.ChangeType == ChangeTypes.DELETE)
				{
					DeleteItem(changeEventArg.ID);
				}
				else if (changeEventArg.ID != -1)
				{
					LoadChangedItem(changeEventArg.ID, changeEventArg.ChangeType);
				}
			}
		}

		private static void LoadChangedItem(int id, ChangeTypes changeType)
		{
			if (priceLevels != null)
			{
				DataSet dataSet = null;
				try
				{
					dataSet = GetData(new int[1]
					{
						id
					});
					if (dataSet != null && dataSet.Tables.Count != 0 && dataSet.Tables[0].Rows.Count != 0)
					{
						DataRow[] array = priceLevels.Tables["Price_Level"].Select("PriceLevelID=" + id.ToString());
						if (array.Length != 0)
						{
							priceLevels.Tables["Price_Level"].Rows.Remove(array[0]);
						}
						priceLevels.Merge(dataSet, preserveChanges: true, MissingSchemaAction.Add);
						_ = dataSet.Tables["Price_Level"].Rows[0];
						OnCacheItemUpdated(dataSet, id, changeType);
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
			if (priceLevels != null)
			{
				DataRow[] array = priceLevels.Tables["Price_Level"].Select("PriceLevelID=" + id.ToString());
				if (array.Length != 0)
				{
					priceLevels.Tables["Price_Level"].Rows.Remove(array[0]);
					OnCacheItemUpdated(null, id, ChangeTypes.DELETE);
				}
			}
		}
	}
}
