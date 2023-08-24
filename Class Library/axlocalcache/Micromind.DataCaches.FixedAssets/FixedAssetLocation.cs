using Micromind.ClientLibraries;
using Micromind.Common.Libraries;
using Micromind.DataCaches.Libraries;
using System;
using System.Data;
using System.Data.SqlClient;

namespace Micromind.DataCaches.FixedAssets
{
	public sealed class FixedAssetLocation
	{
		private static DataSet assets;

		private static object syncRoot;

		private static DateTime dateTimeStamp;

		public static event Delegates.RefereshHandler Refereshed;

		public static event EventHandler CacheItemUpdated;

		static FixedAssetLocation()
		{
			FixedAssetLocation.Refereshed = null;
			FixedAssetLocation.CacheItemUpdated = null;
			assets = null;
			syncRoot = new object();
			dateTimeStamp = DateTime.MinValue;
			ChangeEvents.FixedAssetLocationChanged += ChangeEvents_AssetChanged;
		}

		internal static void Reset()
		{
			dateTimeStamp = DateTime.MinValue;
			if (assets != null)
			{
				assets.Dispose();
				assets = null;
			}
		}

		public static DataSet GetAssetsLocation(bool isReferesh)
		{
			if (Micromind.ClientLibraries.Global.ConStatus == ConnectionStatus.Connected)
			{
				if ((assets == null) | isReferesh)
				{
					lock (syncRoot)
					{
						if ((assets == null) | isReferesh)
						{
							if (assets != null && isReferesh)
							{
								DateTime tableLastDateTimeStamp = Factory.DatabaseSystem.GetTableLastDateTimeStamp("AssetLocationID");
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
			else if (assets == null)
			{
				throw new DBNotConnectedException();
			}
			return assets;
		}

		private static void SetData()
		{
			if (assets != null)
			{
				assets.Dispose();
				assets = null;
			}
			assets = GetData(new int[0]);
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
			if (FixedAssetLocation.Refereshed != null)
			{
				FixedAssetLocation.Refereshed();
			}
		}

		private static void OnCacheItemUpdated(object sender, int id, ChangeTypes changeType)
		{
			if (FixedAssetLocation.CacheItemUpdated != null)
			{
				FixedAssetLocation.CacheItemUpdated(sender, new ChangeEventArg(id, changeType));
			}
		}

		private static void ChangeEvents_AssetChanged(object sender, EventArgs e)
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
			if (assets != null)
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
						DataRow[] array = assets.Tables["FixedAsset_Location"].Select("AssetLocationID=" + id.ToString());
						if (array.Length != 0)
						{
							assets.Tables["FixedAsset_Location"].Rows.Remove(array[0]);
						}
						assets.Merge(dataSet, preserveChanges: true, MissingSchemaAction.Add);
						_ = dataSet.Tables["FixedAsset_Location"].Rows[0];
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
			if (assets != null)
			{
				DataRow[] array = assets.Tables["FixedAsset_Location"].Select("AssetLocationID=" + id.ToString());
				if (array.Length != 0)
				{
					assets.Tables["FixedAsset_Location"].Rows.Remove(array[0]);
					OnCacheItemUpdated(null, id, ChangeTypes.DELETE);
				}
			}
		}
	}
}
