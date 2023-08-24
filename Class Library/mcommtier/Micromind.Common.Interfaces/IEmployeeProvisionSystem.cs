using Micromind.Common.Data;
using System;
using System.Data;

namespace Micromind.Common.Interfaces
{
	public interface IEmployeeProvisionSystem
	{
		bool CreateEmployeeProvision(EmployeeProvisionData data, bool isUpdate);

		EmployeeProvisionData GetEmployeeProvisionByID(string sysDocID, string voucherID);

		bool DeleteEmployeeProvision(string sysDocID, string voucherID);

		DataSet GetList(DateTime from, DateTime to, bool showVoid);

		DataSet GetEmployeeList(string fromEmployee, string toEmployee, string fromDepartment, string toDepartment, string fromLocation, string toLocation, string fromType, string toType, string fromDivision, string toDivision, string fromSponsor, string toSponsor, string fromGroup, string toGroup, string fromGrade, string toGrade, string fromPosition, string toPosition, string fromBank, string toBank, string fromAccount, string toAccount, bool showInactive, string EmployeeIDs, DateTime asOfDate, string voucherID);

		DataSet GetEmployeeTicketDetails(string fromEmployee, string toEmployee, string fromDepartment, string toDepartment, string fromLocation, string toLocation, string fromType, string toType, string fromDivision, string toDivision, string fromSponsor, string toSponsor, string fromGroup, string toGroup, string fromGrade, string toGrade, string fromPosition, string toPosition, string fromBank, string toBank, string fromAccount, string toAccount, bool showInactive, string EmployeeIDs, DateTime asOfDate, string voucherID);
	}
}
