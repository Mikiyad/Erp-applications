using Micromind.ClientLibraries;
using Micromind.Common.Libraries;
using Micromind.DataCaches.Libraries;
using System;
using System.Data;
using System.Data.SqlClient;

namespace Micromind.DataCaches.Inventory
{
	public sealed class Manufacturers
	{
		private static DataSet manufacturers;

		private static object syncRoot;

		private static DateTime dateTimeStamp;

		public static event Delegates.RefereshHandler Refereshed;

		public static event EventHandler CacheItemUpdated;

		static Manufacturers()
		{
			Manufacturers.Refereshed = null;
			Manufacturers.CacheItemUpdated = null;
			manufacturers = null;
			syncRoot = new object();
			dateTimeStamp = DateTime.MinValue;
			ChangeEvents.ManufacturerChanged += ChangeEvents_ManufacturerChanged;
		}

		internal static void Reset()
		{
			dateTimeStamp = DateTime.MinValue;
			if (manufacturers != null)
			{
				manufacturers.Dispose();
				manufacturers = null;
			}
		}

		public static DataSet GetManufacturers(bool isReferesh)
		{
			if (Micromind.ClientLibraries.Global.ConStatus == ConnectionStatus.Connected)
			{
				if ((manufacturers == null) | isReferesh)
				{
					lock (syncRoot)
					{
						if ((manufacturers == null) | isReferesh)
						{
							if (manufacturers != null && isReferesh)
							{
								DateTime tableLastDateTimeStamp = Factory.DatabaseSystem.GetTableLastDateTimeStamp("Product_Manufacturer");
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
			else if (manufacturers == null)
			{
				throw new DBNotConnectedException();
			}
			return manufacturers;
		}

		private static void SetData()
		{
			if (manufacturers != null)
			{
				manufacturers.Dispose();
				manufacturers = null;
			}
			manufacturers = GetData(new int[0]);
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
			if (Manufacturers.Refereshed != null)
			{
				Manufacturers.Refereshed();
			}
		}

		private static void OnCacheItemUpdated(object sender, int id, ChangeTypes changeType)
		{
			if (Manufacturers.CacheItemUpdated != null)
			{
				Manufacturers.CacheItemUpdated(sender, new ChangeEventArg(id, changeType));
			}
		}

		private static void ChangeEvents_ManufacturerChanged(object sender, EventArgs e)
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
			if (manufacturers != null)
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
						DataRow[] array = manufacturers.Tables["Product_Manufacturer"].Select("ManufacturerID=" + id.ToString());
						if (array.Length != 0)
						{
							manufacturers.Tables["Product_Manufacturer"].Rows.Remove(array[0]);
						}
						manufacturers.Merge(dataSet, preserveChanges: true, MissingSchemaAction.Add);
						_ = dataSet.Tables["Product_Manufacturer"].Rows[0];
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
			if (manufacturers != null)
			{
				DataRow[] array = manufacturers.Tables["Product_Manufacturer"].Select("ManufacturerID=" + id.ToString());
				if (array.Length != 0)
				{
					manufacturers.Tables["Product_Manufacturer"].Rows.Remove(array[0]);
					OnCacheItemUpdated(null, id, ChangeTypes.DELETE);
				}
			}
		}
	}
}
