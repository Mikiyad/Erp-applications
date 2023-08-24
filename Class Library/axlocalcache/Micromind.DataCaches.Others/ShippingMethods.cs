using Micromind.ClientLibraries;
using Micromind.Common.Libraries;
using Micromind.DataCaches.Libraries;
using System;
using System.Data;
using System.Data.SqlClient;

namespace Micromind.DataCaches.Others
{
	public sealed class ShippingMethods
	{
		private static DataSet shippers;

		private static object syncRoot;

		private static DateTime dateTimeStamp;

		public static event Delegates.RefereshHandler Refereshed;

		public static event EventHandler CacheItemUpdated;

		static ShippingMethods()
		{
			ShippingMethods.Refereshed = null;
			ShippingMethods.CacheItemUpdated = null;
			shippers = null;
			syncRoot = new object();
			dateTimeStamp = DateTime.MinValue;
		}

		internal static void Reset()
		{
			dateTimeStamp = DateTime.MinValue;
			if (shippers != null)
			{
				shippers.Dispose();
				shippers = null;
			}
		}

		public static DataSet GetShippingMethods(bool isReferesh)
		{
			if (Micromind.ClientLibraries.Global.ConStatus == ConnectionStatus.Connected)
			{
				if ((shippers == null) | isReferesh)
				{
					lock (syncRoot)
					{
						if ((shippers == null) | isReferesh)
						{
							if (shippers != null && isReferesh)
							{
								DateTime tableLastDateTimeStamp = Factory.DatabaseSystem.GetTableLastDateTimeStamp("Shipping_Method");
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
			else if (shippers == null)
			{
				throw new DBNotConnectedException();
			}
			return shippers;
		}

		private static void SetData()
		{
			if (shippers != null)
			{
				shippers.Dispose();
				shippers = null;
			}
			shippers = GetData(new int[0]);
			OnRefereshed();
		}

		private static DataSet GetData(int[] ids)
		{
			try
			{
				return Factory.ShippingMethodSystem.GetShippingMethodsByFields(ids, "Shipping_Method.ShippingMethodID", "Shipping_Method.ShippingMethodName", "Shipping_Method.Inactive");
			}
			catch (Exception e)
			{
				ErrorHelper.ProcessError(e);
				return null;
			}
		}

		private static void OnRefereshed()
		{
			if (ShippingMethods.Refereshed != null)
			{
				ShippingMethods.Refereshed();
			}
		}

		private static void OnCacheItemUpdated(object sender, int id, ChangeTypes changeType)
		{
			if (ShippingMethods.CacheItemUpdated != null)
			{
				ShippingMethods.CacheItemUpdated(sender, new ChangeEventArg(id, changeType));
			}
		}

		private static void ChangeEvents_ShippingMethodChanged(object sender, EventArgs e)
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
			if (shippers != null)
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
						DataRow[] array = shippers.Tables["Shipping_Method"].Select("ShippingMethodID=" + id.ToString());
						if (array.Length != 0)
						{
							shippers.Tables["Shipping_Method"].Rows.Remove(array[0]);
						}
						shippers.Merge(dataSet, preserveChanges: true, MissingSchemaAction.Add);
						_ = dataSet.Tables["Shipping_Method"].Rows[0];
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
			if (shippers != null)
			{
				DataRow[] array = shippers.Tables["Shipping_Method"].Select("ShippingMethodID=" + id.ToString());
				if (array.Length != 0)
				{
					shippers.Tables["Shipping_Method"].Rows.Remove(array[0]);
					OnCacheItemUpdated(null, id, ChangeTypes.DELETE);
				}
			}
		}
	}
}
