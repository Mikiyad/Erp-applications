using Micromind.Common.Data;
using System.Data;

namespace Micromind.Common.Interfaces
{
	public interface IEquipmentSystem
	{
		bool CreateEquipment(EquipmentData equipmentData);

		bool UpdateEquipment(EquipmentData equipmentData);

		EquipmentData GetEquipment();

		bool DeleteEquipment(string ID);

		EquipmentData GetEquipmentByID(string id);

		DataSet GetEquipmentByFields(params string[] columns);

		DataSet GetEquipmentByFields(string[] ids, params string[] columns);

		DataSet GetEquipmentByFields(string[] ids, bool isInactive, params string[] columns);

		DataSet GetEquipmentList();

		DataSet GetEquipmentComboList();
	}
}
