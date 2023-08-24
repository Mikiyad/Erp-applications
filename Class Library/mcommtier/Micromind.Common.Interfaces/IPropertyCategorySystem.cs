using Micromind.Common.Data;
using System.Data;

namespace Micromind.Common.Interfaces
{
	public interface IPropertyCategorySystem
	{
		bool CreatePropertyCategory(PropertyCategoryData departmentData);

		bool UpdatePropertyCategory(PropertyCategoryData departmentData);

		PropertyCategoryData GetPropertyCategory();

		bool DeletePropertyCategory(string ID);

		PropertyCategoryData GetPropertyCategoryByID(string id);

		DataSet GetPropertyCategoryByFields(params string[] columns);

		DataSet GetPropertyCategoryByFields(string[] ids, params string[] columns);

		DataSet GetPropertyCategoryByFields(string[] ids, bool isInactive, params string[] columns);

		DataSet GetPropertyCategoryList();

		DataSet GetPropertyCategoryComboList();

		DataSet GetPropertyAssignedCategorysList(string entityID, EntityTypesEnum entityType);

		bool InsertPropertyCategoryAssignment(PropertyCategoryData data, string customerID);
	}
}
