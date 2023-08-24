using Micromind.Common.Data;
using Micromind.Common.Interfaces;
using Micromind.Data;
using System;
using System.Data;

namespace Micromind.Facade
{
	public sealed class FollowupSystem : MarshalByRefObject, IFollowupSystem, IDisposable
	{
		private Config config;

		public FollowupSystem(Config config)
		{
			this.config = config;
		}

		public void Dispose()
		{
		}

		public bool CreateFollowup(CRMFollowupData data)
		{
			return new Followup(config).InsertCRMFollowup(data);
		}

		public bool UpdateFollowup(CRMFollowupData data)
		{
			return UpdateFollowup(data, checkConcurrency: false);
		}

		public bool UpdateFollowup(CRMFollowupData data, bool checkConcurrency)
		{
			return new Followup(config).UpdateCRMFollowup(data);
		}

		public CRMFollowupData GetFollowup()
		{
			using (Followup followup = new Followup(config))
			{
				return followup.GetCRMFollowup();
			}
		}

		public bool DeleteFollowup(string groupID)
		{
			using (Followup followup = new Followup(config))
			{
				return followup.DeleteCRMFollowup(groupID);
			}
		}

		public CRMFollowupData GetFollowupByID(string id, string SourceSysDocID, string sourceVoucherID)
		{
			using (Followup followup = new Followup(config))
			{
				return followup.GetCRMFollowupByID(id, SourceSysDocID, sourceVoucherID);
			}
		}

		public DataSet GetFollowupByFields(string[] ids, bool isInactive, params string[] columns)
		{
			using (Followup followup = new Followup(config))
			{
				return followup.GetCRMFollowupByFields(ids, isInactive, columns);
			}
		}

		public DataSet GetFollowupList()
		{
			using (Followup followup = new Followup(config))
			{
				return followup.GetFollowupList();
			}
		}

		public DataSet GetFollowupListByID(string id, string SourceSysDocID, string sourceVoucherID)
		{
			using (Followup followup = new Followup(config))
			{
				return followup.GetCRMFollowupListByID(id, SourceSysDocID, sourceVoucherID);
			}
		}

		public DataSet GetFollowupComboList()
		{
			using (Followup followup = new Followup(config))
			{
				return followup.GetCRMFollowupComboList();
			}
		}

		public DataSet GetFollowupListByActivityID(CRMRelatedTypes followupType, string followupID, string sourceVoucherID, string SourceSysDocID, DateTime from, DateTime to)
		{
			using (Followup followup = new Followup(config))
			{
				return followup.GetFollowupListByActivityID(followupType, followupID, sourceVoucherID, SourceSysDocID, from, to);
			}
		}
	}
}
