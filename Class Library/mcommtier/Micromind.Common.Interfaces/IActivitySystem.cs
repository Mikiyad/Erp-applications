using Micromind.Common.Data;
using System;
using System.Data;

namespace Micromind.Common.Interfaces
{
	public interface IActivitySystem
	{
		bool CreateActivity(CRMActivityData crmactivityData);

		bool UpdateActivity(CRMActivityData crmactivityData);

		CRMActivityData GetActivity();

		bool DeleteActivity(string sysDocID, string voucherID);

		CRMActivityData GetActivityByID(string sysDocID, string voucherID);

		CRMActivityData GetCustomerActivityByID(string sysDocID, string voucherID);

		DataSet GetActivityList(DateTime from, DateTime to);

		DataSet GetCustomerActivityList(DateTime from, DateTime to);

		DataSet GetActivityListByLeadID(CRMRelatedTypes leadType, string leadID, DateTime from, DateTime to);

		DataSet GetActivityComboList();
	}
}
