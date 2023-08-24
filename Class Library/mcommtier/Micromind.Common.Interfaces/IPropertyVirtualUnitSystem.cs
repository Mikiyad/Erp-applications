using Micromind.Common.Data;
using System.Data;

namespace Micromind.Common.Interfaces
{
	public interface IPropertyVirtualUnitSystem
	{
		bool CreatePropertyVirtualUnit(PropertyVirtualUnitData PropertyVirtualUnitData);

		bool UpdatePropertyVirtualUnit(PropertyVirtualUnitData PropertyVirtualUnitData);

		PropertyVirtualUnitData GetPropertyVirtualUnit();

		bool DeletePropertyVirtualUnit(string ID);

		PropertyVirtualUnitData GetPropertyVirtualUnitByID(string id);

		DataSet GetPropertyVirtualUnitByFields(params string[] columns);

		DataSet GetPropertyVirtualUnitByFields(string[] ids, params string[] columns);

		DataSet GetPropertyVirtualUnitByFields(string[] ids, bool isInactive, params string[] columns);

		DataSet GetPropertyVirtualUnitList(bool showInactive);

		DataSet GetPropertyVirtualUnitComboList();
	}
}
