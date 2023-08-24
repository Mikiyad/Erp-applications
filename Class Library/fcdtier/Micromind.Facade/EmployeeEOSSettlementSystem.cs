using Micromind.Common.Data;
using Micromind.Common.Interfaces;
using Micromind.Data;
using System;
using System.Data;

namespace Micromind.Facade
{
	public sealed class EmployeeEOSSettlementSystem : MarshalByRefObject, IEmployeeEOSSettlementSystem, IDisposable
	{
		private Config config;

		public EmployeeEOSSettlementSystem(Config config)
		{
			this.config = config;
		}

		public void Dispose()
		{
		}

		public bool CreateEmployeeLoan(EmployeeEOSSettlementData data, bool isUpdate)
		{
			return new EmployeeEOSSettlement(config).InsertUpdateEmployeeLoan(data, isUpdate);
		}

		public bool CreateEmployeeEOS(EmployeeEOSSettlementData data, bool isUpdate)
		{
			return new EmployeeEOSSettlement(config).InsertUpdateEmployeeEOS(data, isUpdate);
		}

		public EmployeeEOSSettlementData GetEmployeeLoanPaymentByID(string sysDocID, string voucherID)
		{
			return new EmployeeEOSSettlement(config).GetEmployeeLoanPaymentByID(sysDocID, voucherID);
		}

		public bool DeleteEOS(string EmployeeID)
		{
			return new EmployeeEOSSettlement(config).DeleteEOS(EmployeeID);
		}

		public bool DeleteLoanPayment(string sysDocID, string voucherID)
		{
			return new EmployeeEOSSettlement(config).DeleteLoanPayment(sysDocID, voucherID);
		}

		public DataSet GetEmployeeLoanComboList()
		{
			return new EmployeeEOSSettlement(config).GetEmployeeLoanComboList();
		}

		public DataSet GetEmployeeLoanAllComboList()
		{
			return new EmployeeEOSSettlement(config).GetEmployeeLoanComboAllList();
		}

		public decimal GetNextLoanInstallmentAmount(string voucherID, string employeeID)
		{
			return new EmployeeEOSSettlement(config).GetNextLoanInstallmentAmount(voucherID, employeeID);
		}

		public DataSet GetListEmployeeLoan(DateTime from, DateTime to, bool showVoid)
		{
			return new EmployeeEOSSettlement(config).GetListEmployeeLoan(from, to, showVoid);
		}

		public DataSet GetListEmployeeLoanPayment(DateTime from, DateTime to, bool showVoid)
		{
			return new EmployeeEOSSettlement(config).GetListEmployeeLoanPayment(from, to, showVoid);
		}

		public DataSet GetEmployeeLoanReport(DateTime from, DateTime to, string fromEmployee, string toEmployee, string fromDepartment, string toDepartment, string fromLocation, string toLocation, string SysDocID, string VoucehrID)
		{
			return new EmployeeEOSSettlement(config).GetEmployeeLoanReport(from, to, fromEmployee, toEmployee, fromDepartment, toDepartment, fromLocation, toLocation, SysDocID, VoucehrID);
		}

		public DataSet GetEmployeeLoanList(string sysDocID)
		{
			return new EmployeeEOSSettlement(config).GetEmployeeLoanList(sysDocID);
		}

		public DataSet GetEOSSettlementToPrint(string EmployeeID)
		{
			return new EmployeeEOSSettlement(config).GetEOSSettlementToPrint(EmployeeID);
		}

		public DataSet GetEmployeeEOSToPrint(string sysDocID, string voucherID)
		{
			return new EmployeeEOSSettlement(config).GetEmployeeEOSToPrint(sysDocID, voucherID);
		}

		public DataSet GetEmployeeLoanReportSummary(DateTime from, DateTime to, string fromEmployee, string toEmployee, string fromDepartment, string toDepartment, string fromLocation, string toLocation, string SysDocID, string VoucehrID)
		{
			return new EmployeeEOSSettlement(config).GetEmployeeLoanReportSummary(from, to, fromEmployee, toEmployee, fromDepartment, toDepartment, fromLocation, toLocation, SysDocID, VoucehrID);
		}

		public EmployeeEOSSettlementData GetEmployeeLoanSettlementByID(string sysDocID, string voucherID)
		{
			return new EmployeeEOSSettlement(config).GetEmployeeLoanSettlementByID(sysDocID, voucherID);
		}

		public DataSet GetListEmployeeLoanSettlement(DateTime from, DateTime to, bool showVoid)
		{
			return new EmployeeEOSSettlement(config).GetListEmployeeLoanSettlement(from, to, showVoid);
		}

		public bool DeleteLoanSettlement(string sysDocID, string voucherID)
		{
			return new EmployeeEOSSettlement(config).DeleteLoanSettlement(sysDocID, voucherID);
		}

		public bool VoidLoanSettlement(string sysDocID, string voucherID, bool isVoid)
		{
			return new EmployeeEOSSettlement(config).VoidLoanSettlement(sysDocID, voucherID, isVoid);
		}

		public DataSet GetEmployeeLoanSettlementToPrint(string sysDocID, string voucherID)
		{
			return new EmployeeEOSSettlement(config).GetEmployeeLoanSettlementToPrint(sysDocID, voucherID);
		}

		public DataSet GetEmployeeFinalSettlement(string fromEmployee, DateTime asOfDate, bool Isregular)
		{
			return new EmployeeEOSSettlement(config).GetEmployeeFinalSettlement(fromEmployee, asOfDate, Isregular);
		}

		public DataSet GetEmployeeEOSRule(string employeeID, bool isResigned)
		{
			return new EmployeeEOSSettlement(config).GetEmployeeEOSRule(employeeID, isResigned);
		}

		public DataSet GetEmployeeLoanByID(string EmployeeID)
		{
			return new EmployeeEOSSettlement(config).GetEmployeeLoanByID(EmployeeID);
		}

		public DataSet GetEmployeeBriefInfo(string employeeID)
		{
			using (EmployeeEOSSettlement employeeEOSSettlement = new EmployeeEOSSettlement(config))
			{
				return employeeEOSSettlement.GetEmployeeBriefInfo(employeeID);
			}
		}

		public bool DeleteEOS(string sysDocID, string voucherID, string employeeID)
		{
			return new EmployeeEOSSettlement(config).DeleteEOS(sysDocID, voucherID, employeeID);
		}

		public EmployeeEOSSettlementData GetEmployeeEOSByID(string sysDocID, string voucherID)
		{
			return new EmployeeEOSSettlement(config).GetEOSByID(sysDocID, voucherID);
		}

		public DataSet GetEmployeeEOSList(DateTime from, DateTime to)
		{
			using (EmployeeEOSSettlement employeeEOSSettlement = new EmployeeEOSSettlement(config))
			{
				return employeeEOSSettlement.GetEmployeeEOSList(from, to);
			}
		}
	}
}
