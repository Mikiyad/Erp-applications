using Micromind.Common.Data;
using System.Data;

namespace Micromind.Common.Interfaces
{
	public interface ISurveyorSystem
	{
		bool CreateSurveyor(SurveyorData qualityTaskData);

		bool UpdateSurveyor(SurveyorData qualityTaskData);

		SurveyorData GetSurveyor();

		bool DeleteSurveyor(string ID);

		SurveyorData GetSurveyorByID(string id);

		DataSet GetSurveyorByFields(params string[] columns);

		DataSet GetSurveyorByFields(string[] ids, params string[] columns);

		DataSet GetSurveyorByFields(string[] ids, bool isInactive, params string[] columns);

		DataSet GetSurveyorList();

		DataSet GetSurveyorComboList();
	}
}
