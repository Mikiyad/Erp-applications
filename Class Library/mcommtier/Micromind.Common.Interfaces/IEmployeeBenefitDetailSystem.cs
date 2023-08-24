using Micromind.Common.Data;
using System.Data;

namespace Micromind.Common.Interfaces
{
	public interface IEmployeeBenefitDetailSystem
	{
		bool CreateEmployeeBenefitDetail(EmployeeBenefitDetailData employeeBenefitDetailData);

		bool UpdateEmployeeBenefitDetail(EmployeeBenefitDetailData employeeBenefitDetailData);

		EmployeeBenefitDetailData GetEmployeeBenefitDetail();

		bool DeleteEmployeeBenefitDetail(string ID);

		EmployeeBenefitDetailData GetEmployeeBenefitDetailByID(string id);

		EmployeeBenefitDetailData GetEmployeeBenefitDetailsByEmployeeID(string employeeID);

		DataSet GetEmployeeBenefitDetailByFields(params string[] columns);

		DataSet GetEmployeeBenefitDetailByFields(string[] ids, params string[] columns);

		DataSet GetEmployeeBenefitDetailByFields(string[] ids, bool isInactive, params string[] columns);

		DataSet GetEmployeeBenefitDetailList();

		DataSet GetEmployeeBenefitDetailComboList();
	}
}
