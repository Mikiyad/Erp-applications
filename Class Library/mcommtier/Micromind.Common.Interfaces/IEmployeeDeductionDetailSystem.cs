using Micromind.Common.Data;
using System.Data;

namespace Micromind.Common.Interfaces
{
	public interface IEmployeeDeductionDetailSystem
	{
		bool CreateEmployeeDeductionDetail(EmployeeDeductionDetailData employeeDeductionDetailData);

		bool UpdateEmployeeDeductionDetail(EmployeeDeductionDetailData employeeDeductionDetailData);

		EmployeeDeductionDetailData GetEmployeeDeductionDetail();

		bool DeleteEmployeeDeductionDetail(string ID);

		EmployeeDeductionDetailData GetEmployeeDeductionDetailByID(string id);

		EmployeeDeductionDetailData GetEmployeeDeductionDetailsByEmployeeID(string employeeID);

		DataSet GetEmployeeDeductionDetailByFields(params string[] columns);

		DataSet GetEmployeeDeductionDetailByFields(string[] ids, params string[] columns);

		DataSet GetEmployeeDeductionDetailByFields(string[] ids, bool isInactive, params string[] columns);

		DataSet GetEmployeeDeductionDetailList();

		DataSet GetEmployeeDeductionDetailComboList();
	}
}
