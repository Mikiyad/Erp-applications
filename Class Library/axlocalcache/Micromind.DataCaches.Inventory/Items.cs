using Micromind.ClientLibraries;
using Micromind.Common.Libraries;
using Micromind.DataCaches.Libraries;
using System;
using System.Data;
using System.Data.SqlClient;

namespace Micromind.DataCaches.Inventory
{
	public sealed class Items
	{
		private static DataSet itemsWithQtyInStock;

		private static object syncRoot;

		private static DateTime dateTimeStamp;

		public static event Delegates.RefereshHandler Refereshed;

		public static event EventHandler CacheItemUpdated;

		static Items()
		{
			Items.Refereshed = null;
			Items.CacheItemUpdated = null;
			itemsWithQtyInStock = null;
			syncRoot = new object();
			dateTimeStamp = DateTime.MinValue;
			ChangeEvents.ItemChanged += ChangeEvents_Item;
		}

		internal static void Reset()
		{
			dateTimeStamp = DateTime.MinValue;
			if (itemsWithQtyInStock != null)
			{
				itemsWithQtyInStock.Dispose();
				itemsWithQtyInStock = null;
			}
		}

		public static DataSet GetItemsWithQtyInStock(bool isReferesh)
		{
			if (Micromind.ClientLibraries.Global.ConStatus == ConnectionStatus.Connected)
			{
				if ((itemsWithQtyInStock == null) | isReferesh)
				{
					lock (syncRoot)
					{
						if ((itemsWithQtyInStock == null) | isReferesh)
						{
							if (itemsWithQtyInStock != null && isReferesh)
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
			else if (itemsWithQtyInStock == null)
			{
				throw new DBNotConnectedException();
			}
			return itemsWithQtyInStock;
		}

		private static void SetData()
		{
			if (itemsWithQtyInStock != null)
			{
				itemsWithQtyInStock.Dispose();
				itemsWithQtyInStock = null;
			}
			OnRefereshed();
		}

		private static void OnRefereshed()
		{
			if (Items.Refereshed != null)
			{
				Items.Refereshed();
			}
		}

		private static void OnCacheItemUpdated(object sender, int id, ChangeTypes changeType)
		{
			if (Items.CacheItemUpdated != null)
			{
				Items.CacheItemUpdated(sender, new ChangeEventArg(id, changeType));
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
			if (itemsWithQtyInStock != null)
			{
				DataSet dataSet = null;
				try
				{
					if (dataSet != null && dataSet.Tables.Count != 0 && dataSet.Tables[0].Rows.Count != 0)
					{
						DataRow[] array = itemsWithQtyInStock.Tables["Product"].Select("ProductID=" + id.ToString());
						if (array.Length != 0)
						{
							itemsWithQtyInStock.Tables["Product"].Rows.Remove(array[0]);
						}
						itemsWithQtyInStock.Merge(dataSet, preserveChanges: true, MissingSchemaAction.Add);
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
			if (itemsWithQtyInStock != null)
			{
				DataRow[] array = itemsWithQtyInStock.Tables["Product"].Select("ProductID=" + id.ToString());
				if (array.Length != 0)
				{
					itemsWithQtyInStock.Tables["Product"].Rows.Remove(array[0]);
					OnCacheItemUpdated(null, id, ChangeTypes.DELETE);
				}
			}
		}
	}
}
