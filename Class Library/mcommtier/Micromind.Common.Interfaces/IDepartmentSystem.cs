using Micromind.Common.Data;
using System.Data;

namespace Micromind.Common.Interfaces
{
	public interface IDepartmentSystem
	{
		bool CreateDepartment(DepartmentData departmentData);

		bool UpdateDepartment(DepartmentData departmentData);

		DepartmentData GetDepartment();

		bool DeleteDepartment(string ID);

		DepartmentData GetDepartmentByID(string id);

		DataSet GetDepartmentByFields(params string[] columns);

		DataSet GetDepartmentByFields(string[] ids, params string[] columns);

		DataSet GetDepartmentByFields(string[] ids, bool isInactive, params string[] columns);

		DataSet GetDepartmentList();

		DataSet GetDepartmentComboList();
	}
}
