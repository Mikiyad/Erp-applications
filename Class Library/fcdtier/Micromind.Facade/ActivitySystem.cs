using Micromind.Common.Data;
using Micromind.Common.Interfaces;
using Micromind.Data;
using System;
using System.Data;

namespace Micromind.Facade
{
	public sealed class ActivitySystem : MarshalByRefObject, IActivitySystem, IDisposable
	{
		private Config config;

		public ActivitySystem(Config config)
		{
			this.config = config;
		}

		public void Dispose()
		{
		}

		public bool CreateActivity(CRMActivityData data)
		{
			return new Activity(config).InsertUpdateCRMActivity(data, isUpdate: false);
		}

		public bool UpdateActivity(CRMActivityData data)
		{
			return UpdateActivity(data, checkConcurrency: true);
		}

		public bool UpdateActivity(CRMActivityData data, bool checkConcurrency)
		{
			return new Activity(config).InsertUpdateCRMActivity(data, isUpdate: true);
		}

		public CRMActivityData GetActivity()
		{
			using (Activity activity = new Activity(config))
			{
				return activity.GetCRMActivity();
			}
		}

		public bool DeleteActivity(string sysDocID, string voucherID)
		{
			using (Activity activity = new Activity(config))
			{
				return activity.DeleteCRMActivity(sysDocID, voucherID);
			}
		}

		public CRMActivityData GetActivityByID(string sysDocID, string voucherID)
		{
			using (Activity activity = new Activity(config))
			{
				return activity.GetCRMActivityByID(sysDocID, voucherID);
			}
		}

		public CRMActivityData GetCustomerActivityByID(string sysDocID, string voucherID)
		{
			using (Activity activity = new Activity(config))
			{
				return activity.GetCustomerActivityByID(sysDocID, voucherID);
			}
		}

		public DataSet GetActivityList(DateTime from, DateTime to)
		{
			using (Activity activity = new Activity(config))
			{
				return activity.GetCRMActivityList(CRMRelatedTypes.Lead, "", from, to);
			}
		}

		public DataSet GetCustomerActivityList(DateTime from, DateTime to)
		{
			using (Activity activity = new Activity(config))
			{
				return activity.GetCustomerActivityList(CRMRelatedTypes.Customer, "", from, to);
			}
		}

		public DataSet GetActivityListByLeadID(CRMRelatedTypes leadType, string leadID, DateTime from, DateTime to)
		{
			using (Activity activity = new Activity(config))
			{
				return activity.GetCRMActivityListByLeadID(leadType, leadID, from, to);
			}
		}

		public DataSet GetActivityComboList()
		{
			using (Activity activity = new Activity(config))
			{
				return activity.GetCRMActivityComboList();
			}
		}
	}
}
