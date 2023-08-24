using Micromind.ClientLibraries;
using Micromind.Common.Libraries;
using Micromind.DataCaches.Libraries;
using System;
using System.Data;
using System.Data.SqlClient;

namespace Micromind.DataCaches.Inventory
{
	public sealed class Brands
	{
		private static DataSet brands;

		private static object syncRoot;

		private static DateTime dateTimeStamp;

		public static event Delegates.RefereshHandler Refereshed;

		public static event EventHandler CacheItemUpdated;

		static Brands()
		{
			Brands.Refereshed = null;
			Brands.CacheItemUpdated = null;
			brands = null;
			syncRoot = new object();
			dateTimeStamp = DateTime.MinValue;
			ChangeEvents.BrandChanged += ChangeEvents_BrandChanged;
		}

		internal static void Reset()
		{
			dateTimeStamp = DateTime.MinValue;
			if (brands != null)
			{
				brands.Dispose();
				brands = null;
			}
		}

		public static DataSet GetBrands(bool isReferesh)
		{
			if (Micromind.ClientLibraries.Global.ConStatus == ConnectionStatus.Connected)
			{
				if ((brands == null) | isReferesh)
				{
					lock (syncRoot)
					{
						if ((brands == null) | isReferesh)
						{
							if (brands != null && isReferesh)
							{
								DateTime tableLastDateTimeStamp = Factory.DatabaseSystem.GetTableLastDateTimeStamp("Product_Brand");
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
			else if (brands == null)
			{
				throw new DBNotConnectedException();
			}
			return brands;
		}

		private static void SetData()
		{
			if (brands != null)
			{
				brands.Dispose();
				brands = null;
			}
			brands = GetData(new int[0]);
			OnRefereshed();
		}

		private static DataSet GetData(int[] ids)
		{
			try
			{
				return null;
			}
			catch (Exception e)
			{
				ErrorHelper.ProcessError(e);
				return null;
			}
		}

		private static void OnRefereshed()
		{
			if (Brands.Refereshed != null)
			{
				Brands.Refereshed();
			}
		}

		private static void OnCacheItemUpdated(object sender, int id, ChangeTypes changeType)
		{
			if (Brands.CacheItemUpdated != null)
			{
				Brands.CacheItemUpdated(sender, new ChangeEventArg(id, changeType));
			}
		}

		private static void ChangeEvents_BrandChanged(object sender, EventArgs e)
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
			if (brands != null)
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
						DataRow[] array = brands.Tables["Product_Brand"].Select("BrandID=" + id.ToString());
						if (array.Length != 0)
						{
							brands.Tables["Product_Brand"].Rows.Remove(array[0]);
						}
						brands.Merge(dataSet, preserveChanges: true, MissingSchemaAction.Add);
						_ = dataSet.Tables["Product_Brand"].Rows[0];
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
			if (brands != null)
			{
				DataRow[] array = brands.Tables["Product_Brand"].Select("BrandID=" + id.ToString());
				if (array.Length != 0)
				{
					brands.Tables["Product_Brand"].Rows.Remove(array[0]);
					OnCacheItemUpdated(null, id, ChangeTypes.DELETE);
				}
			}
		}
	}
}
