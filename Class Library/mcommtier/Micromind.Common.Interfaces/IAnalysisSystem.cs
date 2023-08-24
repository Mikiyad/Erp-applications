using Micromind.Common.Data;
using System.Data;

namespace Micromind.Common.Interfaces
{
	public interface IAnalysisSystem
	{
		bool CreateAnalysis(AnalysisData analysisData);

		bool UpdateAnalysis(AnalysisData analysisData);

		AnalysisData GetAnalysis();

		bool DeleteAnalysis(string ID);

		AnalysisData GetAnalysisByID(string id);

		DataSet GetAnalysisByFields(params string[] columns);

		DataSet GetAnalysisByFields(string[] ids, params string[] columns);

		DataSet GetAnalysisByFields(string[] ids, bool isInactive, params string[] columns);

		DataSet GetAnalysisList();

		DataSet GetAnalysisComboList();

		DataSet GetAnalysisNonAccountComboList();
	}
}
