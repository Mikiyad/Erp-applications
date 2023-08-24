using Micromind.Common.Data;
using System.Data;

namespace Micromind.Common.Interfaces
{
	public interface IPropertyUnitSystem
	{
		bool CreatePropertyUnit(PropertyUnitData propertyunitData);

		bool UpdatePropertyUnit(PropertyUnitData propertyunitData);

		PropertyUnitData GetPropertyUnit();

		bool DeletePropertyUnit(string ID);

		PropertyUnitData GetPropertyUnitByID(string id);

		DataSet GetPropertyUnitByFields(params string[] columns);

		DataSet GetPropertyUnitByFields(string[] ids, params string[] columns);

		DataSet GetPropertyUnitByFields(string[] ids, bool isInactive, params string[] columns);

		DataSet GetPropertyUnitList(bool showInactive);

		DataSet GetPropertyUnitComboList();

		byte[] GetPropertyUnitThumbnailImage(string unitID);

		bool AddPropertyUnitPhoto(string unitID, byte[] pictureByte);

		bool RemovePropertyUnitPhoto(string unitID);

		DataSet GetPropertyUnitCurrentTenant(string propertyUnitID);

		DataSet GetPropertyUnitHistoryReport(string propertyUnitID);
	}
}
