using Micromind.Common.Data;
using Micromind.Common.Interfaces;
using Micromind.Data;
using System;
using System.Data;

namespace Micromind.Facade
{
	public sealed class CampaignSystem : MarshalByRefObject, ICampaignSystem, IDisposable
	{
		private Config config;

		public CampaignSystem(Config config)
		{
			this.config = config;
		}

		public void Dispose()
		{
		}

		public bool CreateCampaign(CampaignData data)
		{
			return new Campaign(config).InsertCampaign(data);
		}

		public bool UpdateCampaign(CampaignData data)
		{
			return UpdateCampaign(data, checkConcurrency: false);
		}

		public bool UpdateCampaign(CampaignData data, bool checkConcurrency)
		{
			return new Campaign(config).UpdateCampaign(data);
		}

		public CampaignData GetCampaign()
		{
			using (Campaign campaign = new Campaign(config))
			{
				return campaign.GetCampaign();
			}
		}

		public bool DeleteCampaign(string groupID)
		{
			using (Campaign campaign = new Campaign(config))
			{
				return campaign.DeleteCampaign(groupID);
			}
		}

		public CampaignData GetCampaignByID(string id)
		{
			using (Campaign campaign = new Campaign(config))
			{
				return campaign.GetCampaignByID(id);
			}
		}

		public DataSet GetCampaignByFields(params string[] columns)
		{
			using (Campaign campaign = new Campaign(config))
			{
				return campaign.GetCampaignByFields(columns);
			}
		}

		public DataSet GetCampaignByFields(string[] ids, params string[] columns)
		{
			using (Campaign campaign = new Campaign(config))
			{
				return campaign.GetCampaignByFields(ids, columns);
			}
		}

		public DataSet GetCampaignByFields(string[] ids, bool isInactive, params string[] columns)
		{
			using (Campaign campaign = new Campaign(config))
			{
				return campaign.GetCampaignByFields(ids, isInactive, columns);
			}
		}

		public DataSet GetCampaignList()
		{
			using (Campaign campaign = new Campaign(config))
			{
				return campaign.GetCampaignList();
			}
		}

		public DataSet GetCampaignComboList()
		{
			using (Campaign campaign = new Campaign(config))
			{
				return campaign.GetCampaignComboList();
			}
		}
	}
}
