using Micromind.Common.Data;
using System;
using System.Data;

namespace Micromind.Common.Interfaces
{
	public interface IEmployeeLeaveProcessSystem
	{
		bool CreateEmployeeLeaveDetail(EmployeeLeaveProcessData EmployeeLeaveProcessData);

		bool UpdateEmployeeLeaveDetail(EmployeeLeaveProcessData EmployeeLeaveProcessData);

		EmployeeLeaveProcessData GetEmployeeLeaveDetail();

		bool DeleteEmployeeLeaveDetail(string ID);

		EmployeeLeaveProcessData GetEmployeeLeaveDetailByID(string id);

		EmployeeLeaveProcessData GetEmployeeLeaveDetailsByEmployeeID(string employeeID);

		DataSet GetEmployeeLeaveDetailByFields(params string[] columns);

		DataSet GetEmployeeLeaveDetailByFields(string[] ids, params string[] columns);

		DataSet GetEmployeeLeaveDetailByFields(string[] ids, bool isInactive, params string[] columns);

		DataSet GetEmployeeLeaveDetailComboList();

		bool IsLeaveExist(string employeeId, DateTime startDate, DateTime endDate);

		bool DeleteEmployeeLeaveDetail(string activityID, string voucherID);

		bool VoidEmployeeLeaveDetail(string activityID, string voucherID, bool isVoid);

		bool DeleteEmployeePassportDetail(string activityID, string voucherID);

		DataSet GetEmployeeLeaveAvailability(string EmployeeID);

		int GetEmployeeLeaveDays(string EmployeeID);

		DataSet GetList(DateTime from, DateTime to, bool showVoid);
	}
}
