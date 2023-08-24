using Micromind.Common.Data;
using System.Data;

namespace Micromind.Common.Interfaces
{
	public interface IEquipmentTypeSystem
	{
		bool CreateEquipmentType(EquipmentTypeData EquipmentTypeData);

		bool UpdateEquipmentType(EquipmentTypeData EquipmentTypeData);

		EquipmentTypeData GetEquipmentType();

		bool DeleteEquipmentType(string ID);

		EquipmentTypeData GetEquipmentTypeByID(string id);

		DataSet GetEquipmentTypeByFields(params string[] columns);

		DataSet GetEquipmentTypeByFields(string[] ids, params string[] columns);

		DataSet GetEquipmentTypeByFields(string[] ids, bool isInactive, params string[] columns);

		DataSet GetEquipmentTypeList();

		DataSet GetEquipmentTypeComboList();
	}
}
