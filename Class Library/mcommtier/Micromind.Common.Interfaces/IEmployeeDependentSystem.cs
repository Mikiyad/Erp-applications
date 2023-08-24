using Micromind.Common.Data;
using System.Data;

namespace Micromind.Common.Interfaces
{
	public interface IEmployeeDependentSystem
	{
		bool CreateEmployeeDependent(EmployeeDependentData employeeDependentData);

		bool UpdateEmployeeDependent(EmployeeDependentData employeeDependentData);

		EmployeeDependentData GetEmployeeDependent();

		bool DeleteEmployeeDependent(string dependentID, string employeeID);

		EmployeeDependentData GetEmployeeDependentByID(string employeeID, string dependentID);

		DataSet GetEmployeeDependentByFields(params string[] columns);

		DataSet GetEmployeeDependentByFields(string[] ids, params string[] columns);

		DataSet GetEmployeeDependentByFields(string[] ids, bool isInactive, params string[] columns);

		DataSet GetEmployeeDependentList();
	}
}
