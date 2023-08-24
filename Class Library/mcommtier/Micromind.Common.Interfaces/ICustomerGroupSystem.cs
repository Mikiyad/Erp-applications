using Micromind.Common.Data;
using System.Data;

namespace Micromind.Common.Interfaces
{
	public interface ICustomerGroupSystem
	{
		bool CreateCustomerGroup(CustomerGroupData departmentData);

		bool UpdateCustomerGroup(CustomerGroupData departmentData);

		CustomerGroupData GetCustomerGroup();

		bool DeleteCustomerGroup(string ID);

		CustomerGroupData GetCustomerGroupByID(string id);

		DataSet GetCustomerGroupByFields(params string[] columns);

		DataSet GetCustomerGroupByFields(string[] ids, params string[] columns);

		DataSet GetCustomerGroupByFields(string[] ids, bool isInactive, params string[] columns);

		DataSet GetCustomerGroupList();

		DataSet GetCustomerGroupComboList();

		DataSet GetCustomerAssignedGroupsList(string customerID);
	}
}
