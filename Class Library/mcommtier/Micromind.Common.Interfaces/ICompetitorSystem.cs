using Micromind.Common.Data;
using System.Data;

namespace Micromind.Common.Interfaces
{
	public interface ICompetitorSystem
	{
		bool CreateCompetitor(CompetitorData competitorData);

		bool UpdateCompetitor(CompetitorData competitorData);

		CompetitorData GetCompetitor();

		bool DeleteCompetitor(string ID);

		CompetitorData GetCompetitorByID(string id);

		DataSet GetCompetitorByFields(params string[] columns);

		DataSet GetCompetitorByFields(string[] ids, params string[] columns);

		DataSet GetCompetitorByFields(string[] ids, bool isInactive, params string[] columns);

		DataSet GetCompetitorList();

		DataSet GetCompetitorComboList();
	}
}
