using Micromind.Common.Data;
using System.Data;

namespace Micromind.Common.Interfaces
{
	public interface IArrivalReportTemplateSystem
	{
		bool CreateArrivalReportTemplate(ArrivalReportTemplateData templateData);

		bool UpdateArrivalReportTemplate(ArrivalReportTemplateData templateData);

		ArrivalReportTemplateData GetArrivalReportTemplate();

		bool DeleteArrivalReportTemplate(string ID);

		ArrivalReportTemplateData GetArrivalReportTemplateByID(string id);

		DataSet GetArrivalReportTemplateByFields(params string[] columns);

		DataSet GetArrivalReportTemplateByFields(string[] ids, params string[] columns);

		DataSet GetArrivalReportTemplateByFields(string[] ids, bool isInactive, params string[] columns);

		DataSet GetArrivalReportTemplateList();

		DataSet GetArrivalReportTemplateComboList();
	}
}
