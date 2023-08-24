using Micromind.Common.Data;
using System.Data;

namespace Micromind.Common.Interfaces
{
	public interface IPrintTemplateMapSystem
	{
		bool CreatePrintTemplateMap(PrintTemplateMapData PrintTemplateMapData);

		bool UpdatePrintTemplateMap(PrintTemplateMapData PrintTemplateMapData);

		PrintTemplateMapData GetPrintTemplateMap();

		bool DeletePrintTemplateMap(string ID);

		PrintTemplateMapData GetPrintTemplateMapByID(string id);

		DataSet GetPrintTemplateMapByFields(params string[] columns);

		DataSet GetPrintTemplateMapByFields(string[] ids, params string[] columns);

		DataSet GetPrintTemplateMapByFields(string[] ids, bool isInactive, params string[] columns);

		DataSet GetPrintTemplateMapList();

		DataSet GetPrintTemplateMapComboList();
	}
}
