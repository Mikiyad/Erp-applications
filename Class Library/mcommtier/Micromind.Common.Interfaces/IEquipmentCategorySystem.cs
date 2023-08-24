using Micromind.Common.Data;
using System.Data;

namespace Micromind.Common.Interfaces
{
	public interface IEquipmentCategorySystem
	{
		bool CreateEquipmentCategory(EquipmentCategoryData EquipmentCategoryData);

		bool UpdateEquipmentCategory(EquipmentCategoryData EquipmentCategoryData);

		EquipmentCategoryData GetEquipmentCategory();

		bool DeleteEquipmentCategory(string ID);

		EquipmentCategoryData GetEquipmentCategoryByID(string id);

		DataSet GetEquipmentCategoryByFields(params string[] columns);

		DataSet GetEquipmentCategoryByFields(string[] ids, params string[] columns);

		DataSet GetEquipmentCategoryByFields(string[] ids, bool isInactive, params string[] columns);

		DataSet GetEquipmentCategoryList();

		DataSet GetEquipmentCategoryComboList();
	}
}
