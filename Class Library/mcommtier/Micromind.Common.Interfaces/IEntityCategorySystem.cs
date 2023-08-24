using Micromind.Common.Data;
using System.Data;

namespace Micromind.Common.Interfaces
{
	public interface IEntityCategorySystem
	{
		bool CreateEntityCategory(EntityCategoryData departmentData);

		bool UpdateEntityCategory(EntityCategoryData departmentData);

		EntityCategoryData GetEntityCategory();

		bool DeleteEntityCategory(string ID, EntityTypesEnum entityType);

		EntityCategoryData GetEntityCategoryByID(string id);

		DataSet GetEntityCategoryByFields(params string[] columns);

		DataSet GetEntityCategoryByFields(string[] ids, params string[] columns);

		DataSet GetEntityCategoryByFields(string[] ids, bool isInactive, params string[] columns);

		DataSet GetActiveEntityCategoryList(EntityTypesEnum entityType);

		DataSet GetEntityCategoryList(EntityTypesEnum entityType);

		DataSet GetEntityCategoryComboList();

		DataSet GetEntityAssignedCategorysList(string entityID, EntityTypesEnum entityType);

		DataSet GetEntityAssignedCategorysTreeViewList(string entityID, EntityTypesEnum entityType);

		bool InsertEntityCategoryAssignment(EntityCategoryData data, string entityID);

		DataSet GetEntityCategoryTree(bool includeInactive, bool isHierarchy);

		DataSet GetEntityCategoryCombosDataList();
	}
}
