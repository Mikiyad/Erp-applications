using Micromind.Common.Data;
using System.Data;

namespace Micromind.Common.Interfaces
{
	public interface IEmployeeTypeSystem
	{
		bool CreateEmployeeType(EmployeeTypeData departmentData);

		bool UpdateEmployeeType(EmployeeTypeData departmentData);

		EmployeeTypeData GetEmployeeType();

		bool DeleteEmployeeType(string ID);

		EmployeeTypeData GetEmployeeTypeByID(string id);

		DataSet GetEmployeeTypeByFields(params string[] columns);

		DataSet GetEmployeeTypeByFields(string[] ids, params string[] columns);

		DataSet GetEmployeeTypeByFields(string[] ids, bool isInactive, params string[] columns);

		DataSet GetEmployeeTypeList();

		DataSet GetEmployeeTypeComboList();
	}
}
