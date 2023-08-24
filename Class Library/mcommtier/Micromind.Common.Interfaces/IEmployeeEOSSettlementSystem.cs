using Micromind.Common.Data;
using System;
using System.Data;

namespace Micromind.Common.Interfaces
{
	public interface IEmployeeEOSSettlementSystem
	{
		bool CreateEmployeeLoan(EmployeeEOSSettlementData loanData, bool isUpdate);

		EmployeeEOSSettlementData GetEmployeeLoanPaymentByID(string sysDocID, string voucherID);

		bool DeleteEOS(string EmployeeID);

		bool DeleteLoanPayment(string sysDocID, string voucherID);

		DataSet GetEmployeeLoanComboList();

		DataSet GetEmployeeLoanAllComboList();

		decimal GetNextLoanInstallmentAmount(string voucherID, string employeeID);

		DataSet GetListEmployeeLoan(DateTime from, DateTime to, bool showVoid);

		DataSet GetListEmployeeLoanPayment(DateTime from, DateTime to, bool showVoid);

		DataSet GetEmployeeLoanReport(DateTime from, DateTime to, string fromEmployee, string toEmployee, string fromDepartment, string toDepartment, string fromLocation, string toLocation, string SysDocID, string VoucehrID);

		DataSet GetEmployeeLoanList(string sysDocID);

		DataSet GetEOSSettlementToPrint(string EmployeeID);

		DataSet GetEmployeeLoanReportSummary(DateTime from, DateTime to, string fromEmployee, string toEmployee, string fromDepartment, string toDepartment, string fromLocation, string toLocation, string SysDocID, string VoucehrID);

		EmployeeEOSSettlementData GetEmployeeLoanSettlementByID(string sysDocID, string voucherID);

		DataSet GetListEmployeeLoanSettlement(DateTime from, DateTime to, bool showVoid);

		bool DeleteLoanSettlement(string sysDocID, string voucherID);

		bool VoidLoanSettlement(string sysDocID, string voucherID, bool isVoid);

		DataSet GetEmployeeLoanSettlementToPrint(string sysDocID, string voucherID);

		DataSet GetEmployeeFinalSettlement(string EmployeeID, DateTime to, bool Isregular);

		DataSet GetEmployeeLoanByID(string EmployeeID);

		DataSet GetEmployeeBriefInfo(string employeeID);

		bool CreateEmployeeEOS(EmployeeEOSSettlementData data, bool isUpdate);

		DataSet GetEmployeeEOSList(DateTime from, DateTime to);

		EmployeeEOSSettlementData GetEmployeeEOSByID(string sysDocID, string voucherID);

		bool DeleteEOS(string sysDocID, string voucherID, string employeeID);

		DataSet GetEmployeeEOSToPrint(string sysDocID, string voucherID);

		DataSet GetEmployeeEOSRule(string employeeID, bool isResigned);
	}
}
