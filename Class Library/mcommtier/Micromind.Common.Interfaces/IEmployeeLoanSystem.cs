using Micromind.Common.Data;
using System;
using System.Data;

namespace Micromind.Common.Interfaces
{
	public interface IEmployeeLoanSystem
	{
		bool CreateEmployeeLoan(EmployeeLoanData loanData, bool isUpdate);

		bool InsertUpdateLoanPayment(EmployeeLoanData loanData, bool isUpdate);

		EmployeeLoanData GetEmployeeLoanByID(string sysDocID, string voucherID);

		EmployeeLoanData GetEmployeeLoanPaymentByID(string sysDocID, string voucherID);

		bool DeleteLoan(string sysDocID, string voucherID);

		bool DeleteLoanPayment(string sysDocID, string voucherID);

		DataSet GetEmployeeLoanComboList();

		DataSet GetEmployeeLoanAllComboList();

		bool VoidLoan(string sysDocID, string voucherID, bool isVoid);

		decimal GetNextLoanInstallmentAmount(string voucherID, string employeeID);

		DataSet GetListEmployeeLoan(DateTime from, DateTime to, bool showVoid);

		DataSet GetListEmployeeLoanPayment(DateTime from, DateTime to, bool showVoid);

		DataSet GetEmployeeLoanReport(string fromEmployee, string toEmployee, string fromDepartment, string toDepartment, string fromLocation, string toLocation, DateTime from, DateTime to, string fromType, string toType, string fromDivision, string toDivision, string fromSponsor, string toSponsor, string fromGroup, string toGroup, string fromGrade, string toGrade, string fromPosition, string toPosition, string fromBank, string toBank, string fromAccount, string toAccount, string SysDocID, string VoucehrID, string EmployeeIDs);

		DataSet GetEmployeeLoanList(string sysDocID);

		DataSet GetEmployeeLoanToPrint(string sysDocID, string voucherID);

		DataSet GetEmployeeLoanReportSummary(DateTime from, DateTime to, string fromEmployee, string toEmployee, string fromDepartment, string toDepartment, string fromLocation, string toLocation, string SysDocID, string VoucehrID);

		DataSet GetEmployeeLoanReportSummary(string fromEmployee, string toEmployee, string fromDepartment, string toDepartment, string fromLocation, string toLocation, DateTime from, DateTime to, string fromType, string toType, string fromDivision, string toDivision, string fromSponsor, string toSponsor, string fromGroup, string toGroup, string fromGrade, string toGrade, string fromPosition, string toPosition, string fromBank, string toBank, string fromAccount, string toAccount, string SysDocID, string VoucehrID, string EmployeeIDs);

		bool InsertUpdateLoanSettlement(EmployeeLoanData loanData, bool isUpdate);

		EmployeeLoanData GetEmployeeLoanSettlementByID(string sysDocID, string voucherID);

		DataSet GetListEmployeeLoanSettlement(DateTime from, DateTime to, bool showVoid);

		bool DeleteLoanSettlement(string sysDocID, string voucherID);

		bool VoidLoanSettlement(string sysDocID, string voucherID, bool isVoid);

		DataSet GetEmployeeLoanSettlementToPrint(string sysDocID, string voucherID);

		DataSet GetEmployeePendingLoanList(string employeeID);
	}
}
