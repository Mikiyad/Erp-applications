using Micromind.Common.Data;
using Micromind.Common.Interfaces;
using Micromind.Data;
using System;
using System.Data;

namespace Micromind.Facade
{
	public sealed class PayrollTransactionSystem : MarshalByRefObject, IPayrollTransactionSystem, IDisposable
	{
		private Config config;

		public PayrollTransactionSystem(Config config)
		{
			this.config = config;
		}

		public void Dispose()
		{
		}

		public bool CreatePayrollTransaction(PayrollTransactionData data, bool isUpdate, bool isManual)
		{
			return new PayrollTransaction(config).InsertUpdatePayrollTransaction(data, isUpdate, isManual);
		}

		public PayrollTransactionData GetPayrollTransactionByID(string sysDocID, string voucherID)
		{
			return new PayrollTransaction(config).GetPayrollTransactionByID(sysDocID, voucherID);
		}

		public bool DeletePayrollTransaction(string sysDocID, string voucherID)
		{
			return new PayrollTransaction(config).DeletePayrollTransaction(sysDocID, voucherID);
		}

		public bool VoidPayrollTransaction(string sysDocID, string voucherID, bool isVoid)
		{
			return new PayrollTransaction(config).VoidPayrollTransaction(sysDocID, voucherID, isVoid);
		}

		public DataSet GetEmployeeSalaryReport(DateTime from, DateTime to, string fromEmployee, string toEmployee, string fromDepartment, string toDepartment, string fromLocation, string toLocation, bool showZeroBalance)
		{
			return new PayrollTransaction(config).GetEmployeeSalaryReport(from, to, fromEmployee, toEmployee, fromDepartment, toDepartment, fromLocation, toLocation, showZeroBalance);
		}

		public DataSet GetSalaryCashList(DateTime from, DateTime to, bool isImport, bool showVoid)
		{
			return new PayrollTransaction(config).GetSalaryCashList(from, to, isImport, showVoid);
		}

		public DataSet GetSalaryChequeList(DateTime from, DateTime to, bool isImport, bool showVoid)
		{
			return new PayrollTransaction(config).GetSalaryChequeList(from, to, isImport, showVoid);
		}

		public DataSet GetSalaryBankList(DateTime from, DateTime to, bool isImport, bool showVoid)
		{
			return new PayrollTransaction(config).GetSalaryBankList(from, to, isImport, showVoid);
		}

		public DataSet GetEmployeeSalaryToPrint(string sysDocID, string voucherID)
		{
			return new PayrollTransaction(config).GetEmployeeSalaryToPrint(sysDocID, voucherID);
		}

		public DataSet GeWPFToPrint(string sysDocID, string voucherID)
		{
			return new PayrollTransaction(config).GeWPFToPrint(sysDocID, voucherID);
		}

		public decimal GetPaidSalaryAmount(string sheetSysDocID, string sheetVoucherID, string employeeID)
		{
			return new PayrollTransaction(config).GetPaidSalaryAmount(sheetSysDocID, sheetVoucherID, employeeID);
		}
	}
}
