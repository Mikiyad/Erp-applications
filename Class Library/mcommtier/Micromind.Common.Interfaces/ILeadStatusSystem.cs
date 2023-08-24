using Micromind.Common.Data;
using System.Data;

namespace Micromind.Common.Interfaces
{
	public interface ILeadStatusSystem
	{
		bool CreateLeadStatus(LeadStatusData LeadStatusData);

		bool UpdateLeadStatus(LeadStatusData LeadStatusData);

		LeadStatusData GetLeadStatus();

		bool DeleteLeadStatus(string ID);

		LeadStatusData GetLeadStatusByID(string id);

		DataSet GetLeadStatusByFields(params string[] columns);

		DataSet GetLeadStatusByFields(string[] ids, params string[] columns);

		DataSet GetLeadStatusByFields(string[] ids, bool isInactive, params string[] columns);

		DataSet GetLeadStatusList();

		DataSet GetLeadStatusComboList();
	}
}
