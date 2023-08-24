using Micromind.Common.Data;
using System.Data;

namespace Micromind.Common.Interfaces
{
	public interface ICampaignSystem
	{
		bool CreateCampaign(CampaignData campaignData);

		bool UpdateCampaign(CampaignData campaignData);

		CampaignData GetCampaign();

		bool DeleteCampaign(string ID);

		CampaignData GetCampaignByID(string id);

		DataSet GetCampaignByFields(params string[] columns);

		DataSet GetCampaignByFields(string[] ids, params string[] columns);

		DataSet GetCampaignByFields(string[] ids, bool isInactive, params string[] columns);

		DataSet GetCampaignList();

		DataSet GetCampaignComboList();
	}
}
