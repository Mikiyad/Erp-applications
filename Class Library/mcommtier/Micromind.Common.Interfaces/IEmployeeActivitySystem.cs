using Micromind.Common.Data;
using System;
using System.Data;

namespace Micromind.Common.Interfaces
{
	public interface IEmployeeActivitySystem
	{
		bool CreateEmployeeActivity(EmployeeActivityData inventoryAdjustmentData, EmployeeActivityTypes activityType, bool isUpdate);

		EmployeeActivityData GetEmployeeActivityByID(int activityID);

		bool DeleteActivity(string activityID);

		bool DeleteActivity(string activityID, EmployeeActivityTypes activityType);

		DataSet GetUnapprovedLeaveRequests();

		bool ApproveRejectLeaveRequest(int activityID, string employeeID, DateTime startDate, DateTime endDate, bool isApprove, string Remarks);

		DataSet GetEmployeesOnLeave();

		DataSet GetEmployeeLeaveHistory(string employeeId);

		DataSet GetEmployeeAppraisalHistory(string employeeId);

		DataSet GetAllEmployeeApprovedLeaves(string employeeID);

		DataSet GetEmployeeActivityToPrint(int activityID);

		DataSet GetEmployeeResumptionDataToPrint(int activityID);

		DataSet GetEmployeesLeavesToResume();

		DataSet GetEmployeeLeaveByID(string activityID);

		DataSet GetEmployeeLeaveDetailList();

		DataSet GetEmployeeLeaveList(DateTime from, DateTime to, bool showVoid);

		DataSet GetEmployeeLeaveResumptionDetailList();

		DataSet GetEmployeePassportControlList();

		string EncashmentAmount(string employeeID);

		bool IsPassportAllocated(string employeeId);

		DataSet GetActivityList();

		DataSet GetEmployeeAbscondingEntryList();

		EmployeeActivityData GetEmployeeGeneralActivityByID(string sysDocID, string voucherID);

		DataSet GetGeneralActivityList(DateTime from, DateTime to, bool showVoid);

		bool DeleteGeneralActivity(string sysDocID, string voucherID);

		DataSet GetEmployeeGeneralActivityToPrint(string sysDocID, string voucherID);

		DataSet AllowDelete(string employeeID, DateTime LeaveDate);

		EmployeeActivityData GetEmployeeLeavePaymentByID(string sysDocID, string voucherID);

		DataSet GetEmployeeLeavePaymentList(DateTime from, DateTime to, bool showVoid);

		DataSet GetEmployeeLeaveList(string employeeID);
	}
}
