using Micromind.Common.Data;
using System;
using System.Data;

namespace Micromind.Common.Interfaces
{
	public interface ISalarySheetSystem
	{
		bool CreateSalarySheet(SalarySheetData salarySheetData, bool isUpdate);

		SalarySheetData GetSalarySheetByID(string sysDocID, string voucherID);

		bool DeleteSalarySheet(string sysDocID, string voucherID);

		bool VoidSalarySheet(string sysDocID, string voucherID, bool isVoid);

		DataSet CalculateSalarySheet(string fromEmployee, string toEmployee, string fromDepartment, string toDepartment, string fromLocation, string toLocation, string fromType, string toType, string fromDivision, string toDivision, string fromSponsor, string toSponsor, string fromGroup, string toGroup, string fromGrade, string toGrade, string fromPosition, string toPostion, string fromBank, string toBank, string fromAccount, string toAccount, DateTime startDate, DateTime endDate, int periodYear, int periodMonth, string EmployeeIDs);

		bool PostSalarySheet(string sysDocID, string voucherID);

		DataSet GetUnpostedSalarySheets();

		DataSet GetOpenSalarySheets();

		DataSet GetSalarySheetEmployees(string docID, string voucherID, byte paymentMethodID);

		DataSet GetSalarySheetItems(string sysDocID, string voucherID, string[] employeeIDs);

		DataSet GetSelectedSalaryBankTransfer(string fromEmployee, string toEmployee, string fromDepartment, string toDepartment, string fromLocation, string toLocation, string fromType, string toType, string fromDivision, string toDivision, string fromSponsor, string toSponsor, string fromGroup, string toGroup, string fromGrade, string toGrade, string fromPosition, string toPostion, string fromBank, string toBank, string fromAccount, string toAccount, string sysdocid, string voucherid);

		DataSet GetSelectedSalaryBankTransfer(string fromEmployee, string toEmployee, string fromDepartment, string toDepartment, string fromLocation, string toLocation, string fromType, string toType, string fromDivision, string toDivision, string fromSponsor, string toSponsor, string fromGroup, string toGroup, string fromGrade, string toGrade, string fromPosition, string toPostion, string fromBank, string toBank, string fromAccount, string toAccount, string[] sysdocid, string[] voucherid);

		DataSet GeProjectExpenseSalarySheets(string fromEmployee, string toEmployee, string fromDepartment, string toDepartment, string fromLocation, string toLocation, string fromType, string toType, string fromDivision, string toDivision, string fromSponsor, string toSponsor, string fromGroup, string toGroup, string fromGrade, string toGrade, string fromPosition, string toPostion, string fromBank, string toBank, string fromAccount, string toAccount, string sysdocid, string voucherid);

		DataSet GetList(DateTime from, DateTime to, bool showVoid);

		bool AllowDelete(string sysDocID, string voucherNumber);

		DataSet GetSalaryEmployeeSheetDetails(string Month, string Year, string EmployeeID);

		DataSet ReCalculateSalarySheet(string fromEmployee, string toEmployee, string fromDepartment, string toDepartment, string fromLocation, string toLocation, string fromType, string toType, string fromDivision, string toDivision, string fromSponsor, string toSponsor, string fromGroup, string toGroup, string fromGrade, string toGrade, string fromPosition, string toPostion, string fromBank, string toBank, string fromAccount, string toAccount, DateTime startDate, DateTime endDate, int periodYear, int periodMonth, string EmployeeIDs, DataSet dsSSD, string strSysDocID, string strVoucherID);
	}
}
