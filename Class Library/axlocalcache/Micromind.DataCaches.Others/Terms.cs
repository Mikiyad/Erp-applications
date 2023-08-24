using Micromind.ClientLibraries;
using Micromind.Common.Data;
using Micromind.Common.Libraries;
using Micromind.DataCaches.Libraries;
using System;
using System.Data;
using System.Data.SqlClient;

namespace Micromind.DataCaches.Others
{
	public sealed class Terms
	{
		private static PaymentTermData terms;

		private static object syncRoot;

		private static DateTime dateTimeStamp;

		public static event Delegates.RefereshHandler Refereshed;

		public static event EventHandler CacheItemUpdated;

		static Terms()
		{
			Terms.Refereshed = null;
			Terms.CacheItemUpdated = null;
			terms = null;
			syncRoot = new object();
			dateTimeStamp = DateTime.MinValue;
			ChangeEvents.TermChanged += ChangeEvents_TermChanged;
		}

		internal static void Reset()
		{
			dateTimeStamp = DateTime.MinValue;
			if (terms != null)
			{
				terms.Dispose();
				terms = null;
			}
		}

		public static PaymentTermData GetTerms(bool isReferesh)
		{
			if (Micromind.ClientLibraries.Global.ConStatus == ConnectionStatus.Connected)
			{
				if ((terms == null) | isReferesh)
				{
					lock (syncRoot)
					{
						if ((terms == null) | isReferesh)
						{
							if (terms != null && isReferesh)
							{
								DateTime tableLastDateTimeStamp = Factory.DatabaseSystem.GetTableLastDateTimeStamp("Payment_Term");
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
			else if (terms == null)
			{
				throw new DBNotConnectedException();
			}
			return terms;
		}

		private static void SetData()
		{
			if (terms != null)
			{
				terms.Dispose();
				terms = null;
			}
			terms = GetData(new int[0]);
			OnRefereshed();
		}

		private static PaymentTermData GetData(int[] ids)
		{
			try
			{
				return Factory.TermSystem.GetTermsByFields(ids, "Payment_Term.PaymentTermID", "Payment_Term.TermName", "Payment_Term.NetDays");
			}
			catch (Exception e)
			{
				ErrorHelper.ProcessError(e);
				return null;
			}
		}

		private static void OnRefereshed()
		{
			if (Terms.Refereshed != null)
			{
				Terms.Refereshed();
			}
		}

		private static void OnCacheItemUpdated(object sender, int id, ChangeTypes changeType)
		{
			if (Terms.CacheItemUpdated != null)
			{
				Terms.CacheItemUpdated(sender, new ChangeEventArg(id, changeType));
			}
		}

		private static void ChangeEvents_TermChanged(object sender, EventArgs e)
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
			if (terms != null)
			{
				PaymentTermData paymentTermData = null;
				try
				{
					paymentTermData = GetData(new int[1]
					{
						id
					});
					if (paymentTermData != null && paymentTermData.Tables.Count != 0 && paymentTermData.Tables[0].Rows.Count != 0)
					{
						DataRow[] array = terms.Tables["Payment_Term"].Select("PaymentTermID=" + id.ToString());
						if (array.Length != 0)
						{
							terms.Tables["Payment_Term"].Rows.Remove(array[0]);
						}
						terms.Merge(paymentTermData, preserveChanges: true, MissingSchemaAction.Add);
						_ = paymentTermData.Tables["Payment_Term"].Rows[0];
						OnCacheItemUpdated(paymentTermData, id, changeType);
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
			if (terms != null)
			{
				DataRow[] array = terms.Tables["Payment_Term"].Select("PaymentTermID=" + id.ToString());
				if (array.Length != 0)
				{
					terms.Tables["Payment_Term"].Rows.Remove(array[0]);
					OnCacheItemUpdated(null, id, ChangeTypes.DELETE);
				}
			}
		}
	}
}
