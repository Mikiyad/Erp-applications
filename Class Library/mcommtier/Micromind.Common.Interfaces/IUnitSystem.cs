using Micromind.Common.Data;
using System.Data;

namespace Micromind.Common.Interfaces
{
	public interface IUnitSystem
	{
		bool CreateUnit(UnitData unitData);

		bool UpdateUnit(UnitData unitData);

		UnitData GetUnit();

		bool DeleteUnit(string ID);

		UnitData GetUnitByID(string id);

		DataSet GetUnitByFields(params string[] columns);

		DataSet GetUnitByFields(string[] ids, params string[] columns);

		DataSet GetUnitByFields(string[] ids, bool isInactive, params string[] columns);

		DataSet GetUnitList();

		DataSet GetUnitComboList();

		DataSet GetProductUnitDetailComboList();
	}
}
