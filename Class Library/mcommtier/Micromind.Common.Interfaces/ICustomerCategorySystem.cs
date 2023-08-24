using Micromind.Common.Data;
using System.Data;

namespace Micromind.Common.Interfaces
{
	public interface ICustomerCategorySystem
	{
		bool CreateCustomerCategory(CustomerCategoryData departmentData);

		bool UpdateCustomerCategory(CustomerCategoryData departmentData);

		CustomerCategoryData GetCustomerCategory();

		bool DeleteCustomerCategory(string ID);

		CustomerCategoryData GetCustomerCategoryByID(string id);

		DataSet GetCustomerCategoryByFields(params string[] columns);

		DataSet GetCustomerCategoryByFields(string[] ids, params string[] columns);

		DataSet GetCustomerCategoryByFields(string[] ids, bool isInactive, params string[] columns);

		DataSet GetCustomerCategoryList();

		DataSet GetCustomerCategoryComboList();

		DataSet GetCustomerAssignedCategorysList(string entityID);

		bool InsertCustomerCategoryAssignment(CustomerCategoryData data, string customerID);
	}
}
