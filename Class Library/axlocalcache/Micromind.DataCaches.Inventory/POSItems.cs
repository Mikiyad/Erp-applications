using Micromind.ClientLibraries;
using Micromind.Common.Libraries;
using Micromind.DataCaches.Libraries;
using System;
using System.Data;
using System.Data.SqlClient;

namespace Micromind.DataCaches.Inventory
{
	public sealed class POSItems
	{
		private static DataSet itemList;

		private static object syncRoot;

		private static DateTime dateTimeStamp;

		public static event Delegates.RefereshHandler Refereshed;

		public static event EventHandler CacheItemUpdated;

		static POSItems()
		{
			POSItems.Refereshed = null;
			POSItems.CacheItemUpdated = null;
			itemList = null;
			syncRoot = new object();
			dateTimeStamp = DateTime.MinValue;
			ChangeEvents.ItemChanged += ChangeEvents_Item;
		}

		internal static void Reset()
		{
			dateTimeStamp = DateTime.MinValue;
			if (itemList != null)
			{
				itemList.Dispose();
				itemList = null;
			}
		}

		public static DataSet GetItemsWithQtyInStock(bool isReferesh)
		{
			if (Micromind.ClientLibraries.Global.ConStatus == ConnectionStatus.Connected)
			{
				if ((itemList == null) | isReferesh)
				{
					lock (syncRoot)
					{
						if ((itemList == null) | isReferesh)
						{
							if (itemList != null && isReferesh)
							{
								DateTime tableLastDateTimeStamp = Factory.DatabaseSystem.GetTableLastDateTimeStamp("Product", "[Store Products]");
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
			else if (itemList == null)
			{
				throw new DBNotConnectedException();
			}
			return itemList;
		}

		private static void SetData()
		{
			if (itemList != null)
			{
				itemList.Dispose();
				itemList = null;
			}
			OnRefereshed();
		}

		private static void OnRefereshed()
		{
			if (POSItems.Refereshed != null)
			{
				POSItems.Refereshed();
			}
		}

		private static void OnCacheItemUpdated(object sender, int id, ChangeTypes changeType)
		{
			if (POSItems.CacheItemUpdated != null)
			{
				POSItems.CacheItemUpdated(sender, new ChangeEventArg(id, changeType));
			}
		}

		private static void ChangeEvents_Item(object sender, EventArgs e)
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
			if (itemList != null)
			{
				DataSet dataSet = null;
				try
				{
					if (dataSet != null && dataSet.Tables.Count != 0 && dataSet.Tables[0].Rows.Count != 0)
					{
						DataRow[] array = itemList.Tables["Product"].Select("ProductID=" + id.ToString());
						if (array.Length != 0)
						{
							itemList.Tables["Product"].Rows.Remove(array[0]);
						}
						itemList.Merge(dataSet, preserveChanges: true, MissingSchemaAction.Add);
						_ = dataSet.Tables["Product"].Rows[0];
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
			if (itemList != null)
			{
				DataRow[] array = itemList.Tables["Product"].Select("ProductID=" + id.ToString());
				if (array.Length != 0)
				{
					itemList.Tables["Product"].Rows.Remove(array[0]);
					OnCacheItemUpdated(null, id, ChangeTypes.DELETE);
				}
			}
		}
	}
}
