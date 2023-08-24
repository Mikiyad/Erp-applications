using Micromind.Common.Data;
using System.Data;

namespace Micromind.Common.Interfaces
{
	public interface IEmployeeLoanTypeSystem
	{
		bool CreateEmployeeLoanType(EmployeeLoanTypeData loanTypeData);

		bool UpdateEmployeeLoanType(EmployeeLoanTypeData loanTypeData);

		EmployeeLoanTypeData GetEmployeeLoanType();

		bool DeleteEmployeeLoanType(string ID);

		EmployeeLoanTypeData GetEmployeeLoanTypeByID(string id);

		DataSet GetEmployeeLoanTypeByFields(params string[] columns);

		DataSet GetEmployeeLoanTypeByFields(string[] ids, params string[] columns);

		DataSet GetEmployeeLoanTypeByFields(string[] ids, bool isInactive, params string[] columns);

		DataSet GetEmployeeLoanTypeList();

		DataSet GetEmployeeLoanTypeComboList();
	}
}
