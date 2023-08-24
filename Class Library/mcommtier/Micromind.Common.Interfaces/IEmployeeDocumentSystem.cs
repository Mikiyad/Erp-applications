using Micromind.Common.Data;
using System.Data;

namespace Micromind.Common.Interfaces
{
	public interface IEmployeeDocumentSystem
	{
		bool CreateEmployeeDocument(EmployeeDocumentData employeeDocumentData);

		bool UpdateEmployeeDocument(EmployeeDocumentData employeeDocumentData);

		EmployeeDocumentData GetEmployeeDocument();

		bool DeleteEmployeeDocument(string ID);

		EmployeeDocumentData GetEmployeeDocumentByID(string id);

		EmployeeDocumentData GetEmployeeDocumentsByEmployeeID(string employeeID);

		DataSet GetEmployeeDocumentByFields(params string[] columns);

		DataSet GetEmployeeDocumentByFields(string[] ids, params string[] columns);

		DataSet GetEmployeeDocumentByFields(string[] ids, bool isInactive, params string[] columns);

		DataSet GetEmployeeDocumentList();

		DataSet GetEmployeeDocumentComboList();
	}
}
