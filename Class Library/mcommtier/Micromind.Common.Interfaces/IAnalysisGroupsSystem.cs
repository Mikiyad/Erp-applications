using Micromind.Common.Data;
using System.Data;

namespace Micromind.Common.Interfaces
{
	public interface IAnalysisGroupsSystem
	{
		bool CreateAnalysisGroup(AnalysisGroupsData accountGroupData);

		bool UpdateAnalysisGroup(AnalysisGroupsData accountGroupData);

		AnalysisGroupsData GetAnalysisGroups();

		bool DeleteAnalysisGroup(string groupID);

		AnalysisGroupsData GetAnalysisGroupByID(string id);

		DataSet GetAnalysisGroupsByFields(params string[] columns);

		DataSet GetAnalysisGroupsByFields(string[] ids, params string[] columns);

		DataSet GetAnalysisGroupsByFields(string[] ids, bool isInactive, params string[] columns);

		DataSet GetAnalysisGroupsList();

		DataSet GetAnalysisGroupsComboList();
	}
}
