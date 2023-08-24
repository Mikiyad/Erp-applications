using Micromind.Common.Data;
using System.Data;

namespace Micromind.Common.Interfaces
{
	public interface IEmployeePayrollItemDetailSystem
	{
		bool CreateEmployeePayrollItemDetail(EmployeePayrollItemDetailData employeePayrollItemDetailData);

		bool UpdateEmployeePayrollItemDetail(EmployeePayrollItemDetailData employeePayrollItemDetailData);

		EmployeePayrollItemDetailData GetEmployeePayrollItemDetail();

		bool DeleteEmployeePayrollItemDetail(string ID);

		EmployeePayrollItemDetailData GetEmployeePayrollItemDetailByID(string id);

		EmployeePayrollItemDetailData GetEmployeePayrollItemDetailsByEmployeeID(string employeeID);

		DataSet GetEmployeePayrollItemDetailByFields(params string[] columns);

		DataSet GetEmployeePayrollItemDetailByFields(string[] ids, params string[] columns);

		DataSet GetEmployeePayrollItemDetailByFields(string[] ids, bool isInactive, params string[] columns);

		DataSet GetEmployeePayrollItemDetailList();

		DataSet GetEmployeePayrollItemDetailComboList();
	}
}
