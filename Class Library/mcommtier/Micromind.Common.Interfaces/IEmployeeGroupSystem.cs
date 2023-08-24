using Micromind.Common.Data;
using System.Data;

namespace Micromind.Common.Interfaces
{
	public interface IEmployeeGroupSystem
	{
		bool CreateEmployeeGroup(EmployeeGroupData departmentData);

		bool UpdateEmployeeGroup(EmployeeGroupData departmentData);

		EmployeeGroupData GetEmployeeGroup();

		bool DeleteEmployeeGroup(string ID);

		EmployeeGroupData GetEmployeeGroupByID(string id);

		DataSet GetEmployeeGroupByFields(params string[] columns);

		DataSet GetEmployeeGroupByFields(string[] ids, params string[] columns);

		DataSet GetEmployeeGroupByFields(string[] ids, bool isInactive, params string[] columns);

		DataSet GetEmployeeGroupList();

		DataSet GetEmployeeGroupComboList();
	}
}
