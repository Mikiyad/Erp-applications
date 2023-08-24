using Micromind.Common.Data;
using System.Data;

namespace Micromind.Common.Interfaces
{
	public interface IEmployeeDocTypeSystem
	{
		bool CreateEmployeeDocType(EmployeeDocTypeData typeData);

		bool UpdateEmployeeDocType(EmployeeDocTypeData typeData);

		EmployeeDocTypeData GetEmployeeDocType();

		bool DeleteEmployeeDocType(string ID);

		EmployeeDocTypeData GetEmployeeDocTypeByID(string id);

		DataSet GetEmployeeDocTypeByFields(params string[] columns);

		DataSet GetEmployeeDocTypeByFields(string[] ids, params string[] columns);

		DataSet GetEmployeeDocTypeByFields(string[] ids, bool isInactive, params string[] columns);

		DataSet GetEmployeeDocTypeList();

		DataSet GetEmployeeDocTypeComboList();
	}
}
