using Micromind.Common.Data;
using Micromind.Common.Interfaces;
using Micromind.Data;
using System;
using System.Data;

namespace Micromind.Facade
{
	public sealed class LeadStatusSystem : MarshalByRefObject, ILeadStatusSystem, IDisposable
	{
		private Config config;

		public LeadStatusSystem(Config config)
		{
			this.config = config;
		}

		public void Dispose()
		{
		}

		public bool CreateLeadStatus(LeadStatusData data)
		{
			return new LeadStatus(config).InsertLeadStatus(data);
		}

		public bool UpdateLeadStatus(LeadStatusData data)
		{
			return UpdateLeadStatus(data, checkConcurrency: false);
		}

		public bool UpdateLeadStatus(LeadStatusData data, bool checkConcurrency)
		{
			return new LeadStatus(config).UpdateLeadStatus(data);
		}

		public LeadStatusData GetLeadStatus()
		{
			using (LeadStatus leadStatus = new LeadStatus(config))
			{
				return leadStatus.GetLeadStatus();
			}
		}

		public bool DeleteLeadStatus(string groupID)
		{
			using (LeadStatus leadStatus = new LeadStatus(config))
			{
				return leadStatus.DeleteLeadStatus(groupID);
			}
		}

		public LeadStatusData GetLeadStatusByID(string id)
		{
			using (LeadStatus leadStatus = new LeadStatus(config))
			{
				return leadStatus.GetLeadStatusByID(id);
			}
		}

		public DataSet GetLeadStatusByFields(params string[] columns)
		{
			using (LeadStatus leadStatus = new LeadStatus(config))
			{
				return leadStatus.GetLeadStatusByFields(columns);
			}
		}

		public DataSet GetLeadStatusByFields(string[] ids, params string[] columns)
		{
			using (LeadStatus leadStatus = new LeadStatus(config))
			{
				return leadStatus.GetLeadStatusByFields(ids, columns);
			}
		}

		public DataSet GetLeadStatusByFields(string[] ids, bool isInactive, params string[] columns)
		{
			using (LeadStatus leadStatus = new LeadStatus(config))
			{
				return leadStatus.GetLeadStatusByFields(ids, isInactive, columns);
			}
		}

		public DataSet GetLeadStatusList()
		{
			using (LeadStatus leadStatus = new LeadStatus(config))
			{
				return leadStatus.GetLeadStatusList();
			}
		}

		public DataSet GetLeadStatusComboList()
		{
			using (LeadStatus leadStatus = new LeadStatus(config))
			{
				return leadStatus.GetLeadStatusComboList();
			}
		}
	}
}
