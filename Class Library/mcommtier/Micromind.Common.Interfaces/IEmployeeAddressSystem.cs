using Micromind.Common.Data;
using System.Data;

namespace Micromind.Common.Interfaces
{
	public interface IEmployeeAddressSystem
	{
		bool CreateEmployeeAddress(EmployeeAddressData employeeAddressData);

		bool UpdateEmployeeAddress(EmployeeAddressData employeeAddressData);

		EmployeeAddressData GetEmployeeAddress();

		bool DeleteEmployeeAddress(string addressID, string employeeID);

		EmployeeAddressData GetEmployeeAddressByID(string employeeID, string addressID);

		DataSet GetEmployeeAddressByFields(params string[] columns);

		DataSet GetEmployeeAddressByFields(string[] ids, params string[] columns);

		DataSet GetEmployeeAddressByFields(string[] ids, bool isInactive, params string[] columns);

		DataSet GetEmployeeAddressList();

		bool IsPrimaryAddress(string addresssID, string employeeID);
	}
}
