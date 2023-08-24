using Micromind.Common.Data;
using System;
using System.Data;

namespace Micromind.Common.Interfaces
{
	public interface IEmployeeLeaveDetailSystem
	{
		bool CreateEmployeeLeaveDetail(EmployeeLeaveDetailData employeeLeaveDetailData);

		bool UpdateEmployeeLeaveDetail(EmployeeLeaveDetailData employeeLeaveDetailData);

		EmployeeLeaveDetailData GetEmployeeLeaveDetail();

		bool DeleteEmployeeLeaveDetail(string ID);

		EmployeeLeaveDetailData GetEmployeeLeaveDetailByID(string id);

		EmployeeLeaveDetailData GetEmployeeLeaveDetailsByEmployeeID(string employeeID);

		DataSet GetEmployeeLeaveDetailByFields(params string[] columns);

		DataSet GetEmployeeLeaveDetailByFields(string[] ids, params string[] columns);

		DataSet GetEmployeeLeaveDetailByFields(string[] ids, bool isInactive, params string[] columns);

		DataSet GetEmployeeLeaveDetailComboList();

		bool IsLeaveExist(string employeeId, DateTime startDate, DateTime endDate);

		bool DeleteEmployeeLeaveDetail(string activityID, string voucherID);

		bool VoidEmployeeLeaveDetail(string activityID, string voucherID, bool isVoid);

		bool DeleteEmployeePassportDetail(string activityID, string voucherID);

		DataSet GetEmployeeLeaveAvailability(string EmployeeID, DateTime AsonDate, DateTime ToDate, string LeaveTypeID);

		bool IsLeaveProcessDateExists(DateTime OnDate, string employeeId);

		bool IsDocNoExist(string DocNo);
	}
}
