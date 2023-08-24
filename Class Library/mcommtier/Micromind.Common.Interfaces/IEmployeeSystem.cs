using Micromind.Common.Data;
using System;
using System.Data;

namespace Micromind.Common.Interfaces
{
	public interface IEmployeeSystem
	{
		bool CreateEmployee(EmployeeData areaData);

		bool UpdateEmployee(EmployeeData areaData);

		EmployeeData GetEmployee();

		bool DeleteEmployee(string ID);

		EmployeeData GetEmployeeByID(string id);

		bool UpdateEmployeeSalaryDetails(DataSet data, bool isRevised);

		DataSet GetEmployeeSalaryDetailsByEmployeeID(string employeeID);

		DataSet GetEmployeeByFields(params string[] columns);

		DataSet GetEmployeeByFields(string[] ids, params string[] columns);

		DataSet GetEmployeeByFields(string[] ids, bool isInactive, params string[] columns);

		DataSet GetEmployeeList();

		DataSet GetEmployeeComboList();

		DataSet GetEmployeeFilterComboList();

		DataSet GetEmployeeBriefInfo(string employeeID);

		DataSet GetEmployeeBriefInfoAbsconding(string employeeID);

		DataSet GetEmployeeCancellationInfo(string employeeID, string activityID);

		DataSet GetEmployeeBalanceSummary(string fromEmployee, string toEmployee, string fromDepartment, string toDepartment, string fromLocation, string toLocation, string fromType, string toType, string fromDivision, string toDivision, string fromSponsor, string toSponsor, string fromGroup, string toGroup, string fromGrade, string toGrade, string fromPosition, string toPosition, string fromBank, string toBank, string fromAccount, string toAccount, bool showZeroBalance, DateTime to, string EmployeeIDs);

		DataSet GetEmployeeBalanceDetailReport(string fromEmployee, string toEmployee, string fromDepartment, string toDepartment, string fromLocation, string toLocation, DateTime from, DateTime to, string fromType, string toType, string fromDivision, string toDivision, string fromSponsor, string toSponsor, string fromGroup, string toGroup, string fromGrade, string toGrade, string fromPosition, string toPosition, string fromBank, string toBank, string fromAccount, string toAccount, bool showZeroBalance, string EmployeeIDs);

		DataSet GetEmployeeListReport(string fromEmployee, string toEmployee, string fromDepartment, string toDepartment, string fromLocation, string toLocation, string fromType, string toType, string fromDivision, string toDivision, string fromSponsor, string toSponsor, string fromGroup, string toGroup, string fromGrade, string toGrade, string fromPosition, string toPosition, string fromBank, string toBank, string fromAccount, string toAccount, bool showInactive, string EmployeeIDs);

		DataSet GetEmployeeList(string fromEmployee, string toEmployee, string fromDepartment, string toDepartment, string fromLocation, string toLocation, string fromType, string toType, string fromDivision, string toDivision, string fromSponsor, string toSponsor, string fromGroup, string toGroup, string fromGrade, string toGrade, string fromPosition, string toPosition, string fromBank, string toBank, string fromAccount, string toAccount, bool showInactive, string EmployeeIDs);

		DataSet GetEmployeeProfileReport(string fromEmployee, string toEmployee, string fromDepartment, string toDepartment, string fromLocation, string toLocation, bool showInactive);

		DataSet GetEmployeeProfileReport(string fromEmployee, string toEmployee, string fromDepartment, string toDepartment, string fromLocation, string toLocation, string fromType, string toType, string fromDivision, string toDivision, string fromSponsor, string toSponsor, string fromGroup, string toGroup, string fromGrade, string toGrade, string fromPosition, string toPosition, string fromBank, string toBank, string fromAccount, string toAccount, bool showInactive, string EmployeeIDs);

		DataSet GetEmployeeActivityReport(string fromEmployee, string toEmployee, string fromDepartment, string toDepartment, string fromLocation, string toLocation, string fromType, string toType, string fromDivision, string toDivision, string fromSponsor, string toSponsor, string fromGroup, string toGroup, string fromGrade, string toGrade, string fromPosition, string toPosition, string fromBank, string toBank, string fromAccount, string toAccount, bool showInactive, string EmployeeIDs);

		DataSet GetEmployeeLeaveReport(DateTime from, DateTime to, string fromEmployee, string toEmployee, string fromDepartment, string toDepartment, string fromLocation, string toLocation, string fromLeave, string toLeave, LeaveApprovalType approvalType);

		DataSet GetEmployeeLeaveReport(string fromEmployee, string toEmployee, string fromDepartment, string toDepartment, string fromLocation, string toLocation, DateTime from, DateTime to, string fromType, string toType, string fromDivision, string toDivision, string fromSponsor, string toSponsor, string fromGroup, string toGroup, string fromGrade, string toGrade, string fromPosition, string toPosition, string fromBank, string toBank, string fromAccount, string toAccount, string fromLeave, string toLeave, LeaveApprovalType approvalType, string EmployeeIDs);

		DataSet GetEmployeeGraduityEligibilityReport(string fromEmployee, string toEmployee, string fromDepartment, string toDepartment, string fromLocation, string toLocation, string fromType, string toType, string fromDivision, string toDivision, string fromSponsor, string toSponsor, string fromGroup, string toGroup, string fromGrade, string toGrade, string fromPosition, string toPosition, string fromBank, string toBank, string fromAccount, string toAccount, DateTime asOfDate, string EmployeeIDs);

		DataSet GetEmployeeHistoryReport(string fromEmployee, string toEmployee, string fromDepartment, string toDepartment, string fromLocation, string toLocation, string fromType, string toType, string fromDivision, string toDivision, string fromSponsor, string toSponsor, string fromGroup, string toGroup, string fromGrade, string toGrade, string fromPosition, string toPosition, string fromBank, string toBank, string fromAccount, string toAccount, DateTime asOfDate, string EmployeeIDs);

		DataSet GetEmployeeSalaryReport(string fromEmployee, string toEmployee, string fromDepartment, string toDepartment, string fromLocation, string toLocation, DateTime from, DateTime to, string fromType, string toType, string fromDivision, string toDivision, string fromSponsor, string toSponsor, string fromGroup, string toGroup, string fromGrade, string toGrade, string fromPosition, string toPosition, string fromBank, string toBank, string fromAccount, string toAccount, int periodYear, int periodMonth, string EmployeeIDs);

		DataSet GetEmployeeLeaveStatusReport(string fromEmployee, string toEmployee, string fromDepartment, string toDepartment, string fromLocation, string toLocation, DateTime from, DateTime to, string fromType, string toType, string fromDivision, string toDivision, string fromSponsor, string toSponsor, string fromGroup, string toGroup, string fromGrade, string toGrade, string fromPosition, string toPosition, string fromBank, string toBank, string fromAccount, string toAccount, string fromLeave, string toLeave, string EmployeeIDs);

		DataSet GetEmployeeAnnualDueReport(string fromEmployee, string toEmployee, string fromDepartment, string toDepartment, string fromLocation, string toLocation, DateTime from, DateTime to, object BasedOn, string fromType, string toType, string fromDivision, string toDivision, string fromSponsor, string toSponsor, string fromGroup, string toGroup, string fromGrade, string toGrade, string fromPosition, string toPosition, string fromBank, string toBank, string fromAccount, string toAccount, string EmployeeIDs);

		DataSet GetEmployeeLeaveInfo(string employeeID);

		DataSet GetEmployeeSalarySlipReport(string fromEmployee, string toEmployee, string fromDepartment, string toDepartment, string fromLocation, string toLocation, int month, int year);

		DataSet GetEmployeeSalarySlipReport(string fromEmployee, string toEmployee, string fromDepartment, string toDepartment, string fromLocation, string toLocation, string fromType, string toType, string fromDivision, string toDivision, string fromSponsor, string toSponsor, string fromGroup, string toGroup, string fromGrade, string toGrade, string fromPosition, string toPosition, string fromBank, string toBank, string fromAccount, string toAccount, int month, int year, string EmployeeIDs);

		DataSet GetEmployeeSalarySlipReportWeb(string fromEmployee, string toEmployee, string fromDepartment, string toDepartment, string fromLocation, string toLocation, string fromType, string toType, string fromDivision, string toDivision, string fromSponsor, string toSponsor, string fromGroup, string toGroup, string fromGrade, string toGrade, string fromPosition, string toPosition, string fromBank, string toBank, string fromAccount, string toAccount, int month, int year, string EmployeeIDs);

		DataSet GetList(DateTime from, DateTime to, bool showVoid);

		DataSet GetEmployeeList(bool includePhoto);

		bool AddEmployeePhoto(string employeeID, byte[] image);

		bool RemoveEmployeePhoto(string employeeID);

		byte[] GetEmployeeThumbnailImage(string employeeID);

		DataSet GetEmployeeFinalSettlement(string fromEmployee, string toEmployee, string fromDepartment, string toDepartment, string fromLocation, string toLocation, string fromType, string toType, string fromDivision, string toDivision, string fromSponsor, string toSponsor, string fromGroup, string toGroup, string fromGrade, string toGrade, string fromPosition, string toPosition, string fromBank, string toBank, string fromAccount, string toAccount, DateTime to, string EmployeeIDs);

		DataSet GetEmployeeSnapBalance(string employeeID);

		DataSet GetEventEmployeeList();

		bool IsEmployeeSettled(string employeeID);

		DataSet GetActiveEmployeeList();

		DataSet GetHRLetterReport(string fromEmployee, string toEmployee, string fromDepartment, string toDepartment, string fromLocation, string toLocation, string fromType, string toType, string fromDivision, string toDivision, string fromSponsor, string toSponsor, string fromGroup, string toGroup, string fromGrade, string toGrade, string fromPosition, string toPosition, string fromBank, string toBank, string fromAccount, string toAccount, int month, int year, string EmployeeIDs, string strGroupBy);
	}
}
