using Micromind.Common.Data;
using System;
using System.Data;

namespace Micromind.Common.Interfaces
{
	public interface IOverTimeEntrySystem
	{
		bool CreateOverTimeEntry(OverTimeEntryData expenseAdjustmentData, bool isUpdate);

		OverTimeEntryData GetOverTimeEntryByID(string sysDocID, string voucherID);

		DataSet GetOverTimeEntryGroupByJob(string sysDocID, string voucherID);

		OverTimeEntryData GetOverTimeEntry(string employeeID, int year, int month);

		DataSet GetOverTimeEntryToPrint(string sysDocID, string[] voucherID);

		DataSet GetOverTimeEntryToPrint(string sysDocID, string voucherID);

		bool DeleteOverTimeEntry(string voucherID);

		DataSet GetOverTimeEntryReport(string fromEmployee, string toEmployee, string fromDepartment, string toDepartment, string fromLocation, string toLocation, DateTime from, DateTime to, string fromType, string toType, string fromDivision, string toDivision, string fromSponsor, string toSponsor, string fromGroup, string toGroup, string fromGrade, string toGrade, string fromPosition, string toPosition, string fromBank, string toBank, string fromAccount, string toAccount, string EmployeeIDs);

		DataSet GetList(DateTime from, DateTime to, bool showVoid, string sysDocID);

		DataSet GetOverTimeByPeriod(int month, int Year);

		DataSet GetApprovedList();

		bool IsValidEntry(string empno, DateTime date);
	}
}
