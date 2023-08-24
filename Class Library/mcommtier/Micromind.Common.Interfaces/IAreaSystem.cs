using Micromind.Common.Data;
using System.Data;

namespace Micromind.Common.Interfaces
{
	public interface IAreaSystem
	{
		bool CreateArea(AreaData areaData);

		bool UpdateArea(AreaData areaData);

		AreaData GetArea();

		bool DeleteArea(string ID);

		AreaData GetAreaByID(string id);

		DataSet GetAreaByFields(params string[] columns);

		DataSet GetAreaByFields(string[] ids, params string[] columns);

		DataSet GetAreaByFields(string[] ids, bool isInactive, params string[] columns);

		DataSet GetAreaList();

		DataSet GetAreaComboList();
	}
}
