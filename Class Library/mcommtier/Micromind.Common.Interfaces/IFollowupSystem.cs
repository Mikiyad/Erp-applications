using Micromind.Common.Data;
using System;
using System.Data;

namespace Micromind.Common.Interfaces
{
	public interface IFollowupSystem
	{
		bool CreateFollowup(CRMFollowupData crmfollowupData);

		bool UpdateFollowup(CRMFollowupData crmfollowupData);

		CRMFollowupData GetFollowup();

		bool DeleteFollowup(string ID);

		CRMFollowupData GetFollowupByID(string id, string SourceSysDocID, string sourceVoucherID);

		DataSet GetFollowupList();

		DataSet GetFollowupComboList();

		DataSet GetFollowupListByActivityID(CRMRelatedTypes followupType, string followupID, string sourceVoucherID, string SourceSysDocID, DateTime from, DateTime to);
	}
}
