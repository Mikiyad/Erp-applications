using Micromind.Common.Data;
using Micromind.Common.Interfaces;
using Micromind.Data;
using System;
using System.Data;

namespace Micromind.Facade
{
	public sealed class EmployeeLoanSystem : MarshalByRefObject, IEmployeeLoanSystem, IDisposable
	{
		private Config config;

		public EmployeeLoanSystem(Config config)
		{
			this.config = config;
		}

		public void Dispose()
		{
		}

		public bool CreateEmployeeLoan(EmployeeLoanData data, bool isUpdate)
		{
			return new EmployeeLoan(config).InsertUpdateEmployeeLoan(data, isUpdate);
		}

		public bool InsertUpdateLoanPayment(EmployeeLoanData data, bool isUpdate)
		{
			return new EmployeeLoan(config).InsertUpdateEmployeeLoanPayment(data, isUpdate);
		}

		public bool InsertUpdateLoanSettlement(EmployeeLoanData data, bool isUpdate)
		{
			return new EmployeeLoan(config).InsertUpdateEmployeeLoanSettlement(data, isUpdate);
		}

		public EmployeeLoanData GetEmployeeLoanByID(string sysDocID, string voucherID)
		{
			return new EmployeeLoan(config).GetEmployeeLoanByID(sysDocID, voucherID);
		}

		public EmployeeLoanData GetEmployeeLoanPaymentByID(string sysDocID, string voucherID)
		{
			return new EmployeeLoan(config).GetEmployeeLoanPaymentByID(sysDocID, voucherID);
		}

		public bool DeleteLoan(string sysDocID, string voucherID)
		{
			return new EmployeeLoan(config).DeleteLoan(sysDocID, voucherID);
		}

		public bool DeleteLoanPayment(string sysDocID, string voucherID)
		{
			return new EmployeeLoan(config).DeleteLoanPayment(sysDocID, voucherID);
		}

		public DataSet GetEmployeeLoanComboList()
		{
			return new EmployeeLoan(config).GetEmployeeLoanComboList();
		}

		public DataSet GetEmployeeLoanAllComboList()
		{
			return new EmployeeLoan(config).GetEmployeeLoanComboAllList();
		}

		public bool VoidLoan(string sysDocID, string voucherID, bool isVoid)
		{
			return new EmployeeLoan(config).VoidLoan(sysDocID, voucherID, isVoid);
		}

		public decimal GetNextLoanInstallmentAmount(string voucherID, string employeeID)
		{
			return new EmployeeLoan(config).GetNextLoanInstallmentAmount(voucherID, employeeID);
		}

		public DataSet GetListEmployeeLoan(DateTime from, DateTime to, bool showVoid)
		{
			return new EmployeeLoan(config).GetListEmployeeLoan(from, to, showVoid);
		}

		public DataSet GetListEmployeeLoanPayment(DateTime from, DateTime to, bool showVoid)
		{
			return new EmployeeLoan(config).GetListEmployeeLoanPayment(from, to, showVoid);
		}

		public DataSet GetEmployeeLoanReport(string fromEmployee, string toEmployee, string fromDepartment, string toDepartment, string fromLocation, string toLocation, DateTime from, DateTime to, string fromType, string toType, string fromDivision, string toDivision, string fromSponsor, string toSponsor, string fromGroup, string toGroup, string fromGrade, string toGrade, string fromPosition, string toPosition, string fromBank, string toBank, string fromAccount, string toAccount, string SysDocID, string VoucehrID, string EmployeeIDs)
		{
			return new EmployeeLoan(config).GetEmployeeLoanReport(fromEmployee, toEmployee, fromDepartment, toDepartment, fromLocation, toLocation, from, to, fromType, toType, fromDivision, toDivision, fromSponsor, toSponsor, fromGroup, toGroup, fromGrade, toGrade, fromPosition, toPosition, fromBank, toBank, fromAccount, toAccount, SysDocID, VoucehrID, EmployeeIDs);
		}

		public DataSet GetEmployeeLoanList(string sysDocID)
		{
			return new EmployeeLoan(config).GetEmployeeLoanList(sysDocID);
		}

		public DataSet GetEmployeeLoanToPrint(string sysDocID, string voucherID)
		{
			return new EmployeeLoan(config).GetEmployeeLoanToPrint(sysDocID, voucherID);
		}

		public DataSet GetEmployeeLoanReportSummary(DateTime from, DateTime to, string fromEmployee, string toEmployee, string fromDepartment, string toDepartment, string fromLocation, string toLocation, string SysDocID, string VoucehrID)
		{
			return new EmployeeLoan(config).GetEmployeeLoanReportSummary(from, to, fromEmployee, toEmployee, fromDepartment, toDepartment, fromLocation, toLocation, SysDocID, VoucehrID);
		}

		public DataSet GetEmployeeLoanReportSummary(string fromEmployee, string toEmployee, string fromDepartment, string toDepartment, string fromLocation, string toLocation, DateTime from, DateTime to, string fromType, string toType, string fromDivision, string toDivision, string fromSponsor, string toSponsor, string fromGroup, string toGroup, string fromGrade, string toGrade, string fromPosition, string toPosition, string fromBank, string toBank, string fromAccount, string toAccount, string SysDocID, string VoucehrID, string EmployeeIDs)
		{
			return new EmployeeLoan(config).GetEmployeeLoanReportSummary(fromEmployee, toEmployee, fromDepartment, toDepartment, fromLocation, toLocation, from, to, fromType, toType, fromDivision, toDivision, fromSponsor, toSponsor, fromGroup, toGroup, fromGrade, toGrade, fromPosition, toPosition, fromBank, toBank, fromAccount, toAccount, SysDocID, VoucehrID, EmployeeIDs);
		}

		public EmployeeLoanData GetEmployeeLoanSettlementByID(string sysDocID, string voucherID)
		{
			return new EmployeeLoan(config).GetEmployeeLoanSettlementByID(sysDocID, voucherID);
		}

		public DataSet GetListEmployeeLoanSettlement(DateTime from, DateTime to, bool showVoid)
		{
			return new EmployeeLoan(config).GetListEmployeeLoanSettlement(from, to, showVoid);
		}

		public bool DeleteLoanSettlement(string sysDocID, string voucherID)
		{
			return new EmployeeLoan(config).DeleteLoanSettlement(sysDocID, voucherID);
		}

		public bool VoidLoanSettlement(string sysDocID, string voucherID, bool isVoid)
		{
			return new EmployeeLoan(config).VoidLoanSettlement(sysDocID, voucherID, isVoid);
		}

		public DataSet GetEmployeeLoanSettlementToPrint(string sysDocID, string voucherID)
		{
			return new EmployeeLoan(config).GetEmployeeLoanSettlementToPrint(sysDocID, voucherID);
		}

		public DataSet GetEmployeePendingLoanList(string employeeID)
		{
			return new EmployeeLoan(config).GetEmployeePendingLoanList(employeeID);
		}
	}
}
