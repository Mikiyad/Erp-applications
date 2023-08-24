using Micromind.ClientLibraries;
using Micromind.Common.Libraries;
using Micromind.DataCaches.Libraries;
using System;
using System.Data;
using System.Data.SqlClient;

namespace Micromind.DataCaches.Inventory
{
	public sealed class Categories
	{
		private static DataSet categories;

		private static object syncRoot;

		private static DateTime dateTimeStamp;

		public static event Delegates.RefereshHandler Refereshed;

		public static event EventHandler CacheItemUpdated;

		static Categories()
		{
			Categories.Refereshed = null;
			Categories.CacheItemUpdated = null;
			categories = null;
			syncRoot = new object();
			dateTimeStamp = DateTime.MinValue;
			ChangeEvents.CategoryChanged += ChangeEvents_CategoryChanged;
		}

		internal static void Reset()
		{
			dateTimeStamp = DateTime.MinValue;
			if (categories != null)
			{
				categories.Dispose();
				categories = null;
			}
		}

		public static DataSet GetCategories(bool isReferesh)
		{
			if (Micromind.ClientLibraries.Global.ConStatus == ConnectionStatus.Connected)
			{
				if ((categories == null) | isReferesh)
				{
					lock (syncRoot)
					{
						if ((categories == null) | isReferesh)
						{
							if (categories != null && isReferesh)
							{
								DateTime tableLastDateTimeStamp = Factory.DatabaseSystem.GetTableLastDateTimeStamp("Product_Category");
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
			else if (categories == null)
			{
				throw new DBNotConnectedException();
			}
			return categories;
		}

		private static void SetData()
		{
			if (categories != null)
			{
				categories.Dispose();
				categories = null;
			}
			categories = GetData(new int[0]);
			OnRefereshed();
		}

		private static DataSet GetData(int[] ids)
		{
			try
			{
				return new DataSet();
			}
			catch (Exception e)
			{
				ErrorHelper.ProcessError(e);
				return null;
			}
		}

		private static void OnRefereshed()
		{
			if (Categories.Refereshed != null)
			{
				Categories.Refereshed();
			}
		}

		private static void OnCacheItemUpdated(object sender, int id, ChangeTypes changeType)
		{
			if (Categories.CacheItemUpdated != null)
			{
				Categories.CacheItemUpdated(sender, new ChangeEventArg(id, changeType));
			}
		}

		private static void ChangeEvents_CategoryChanged(object sender, EventArgs e)
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
			if (categories != null)
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
						DataRow[] array = categories.Tables["Product_Category"].Select("CategoryID=" + id.ToString());
						if (array.Length != 0)
						{
							categories.Tables["Product_Category"].Rows.Remove(array[0]);
						}
						categories.Merge(dataSet, preserveChanges: true, MissingSchemaAction.Add);
						_ = dataSet.Tables["Product_Category"].Rows[0];
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
			if (categories != null)
			{
				DataRow[] array = categories.Tables["Product_Category"].Select("CategoryID=" + id.ToString());
				if (array.Length != 0)
				{
					categories.Tables["Product_Category"].Rows.Remove(array[0]);
					OnCacheItemUpdated(null, id, ChangeTypes.DELETE);
				}
			}
		}
	}
}
