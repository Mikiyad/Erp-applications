using Micromind.Common.Data;
using System;
using System.Data;

namespace Micromind.Common.Interfaces
{
	public interface ISalarySystem
	{
		bool CreateSalaryDeduction(SalaryData data, bool isUpdate);

		bool CreateSalaryAddition(SalaryData data, bool isUpdate);

		SalaryData GetSalaryDeductionByID(string voucherID);

		SalaryData GetSalaryAdditionByID(string voucherID);

		DataSet GetSalaryDeductionToPrint(string[] voucherID);

		DataSet GetSalaryDeductionToPrint(string voucherID);

		DataSet GetSalaryAdditionToPrint(string[] voucherID);

		DataSet GetSalaryAdditionToPrint(string voucherID);

		bool DeleteSalaryDeduction(string voucherID);

		bool DeleteSalaryAddition(string voucherID);

		DataSet LoadTicketAmount(string fromEmployee, string toEmployee, string fromDepartment, string toDepartment, string fromLocation, string toLocation, string fromType, string toType, string fromDivision, string toDivision, string fromSponsor, string toSponsor, string fromGroup, string toGroup, string fromGrade, string toGrade, string fromPosition, string toPostion, string fromBank, string toBank, string fromAccount, string toAccount, DateTime from, string multipleEmployees);

		DataSet LoadPayrollItem(string fromEmployee, string toEmployee, string fromDepartment, string toDepartment, string fromLocation, string toLocation, string fromType, string toType, string fromDivision, string toDivision, string fromSponsor, string toSponsor, string fromGroup, string toGroup, string fromGrade, string toGrade, string fromPosition, string toPostion, string fromBank, string toBank, string fromAccount, string toAccount, DateTime from, string multipleEmployees, string basedon);

		DataSet LoadDeductionAmount(string fromEmployee, string toEmployee, string fromDepartment, string toDepartment, string fromLocation, string toLocation, string fromType, string toType, string fromDivision, string toDivision, string fromSponsor, string toSponsor, string fromGroup, string toGroup, string fromGrade, string toGrade, string fromPosition, string toPostion, string fromBank, string toBank, string fromAccount, string toAccount, DateTime from, string multipleEmployees, int percent);

		decimal GetTotalLeaveSalary(string EmployeeID);

		DataSet GetAdditionList(DateTime from, DateTime to, bool showVoid);

		DataSet GetDeductionList(DateTime from, DateTime to, bool showVoid);
	}
}
