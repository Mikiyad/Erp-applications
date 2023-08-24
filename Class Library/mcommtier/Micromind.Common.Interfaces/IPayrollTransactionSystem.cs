using Micromind.Common.Data;
using System;
using System.Data;

namespace Micromind.Common.Interfaces
{
	public interface IPayrollTransactionSystem
	{
		bool CreatePayrollTransaction(PayrollTransactionData inventoryAdjustmentData, bool isUpdate, bool isManual);

		PayrollTransactionData GetPayrollTransactionByID(string sysDocID, string voucherID);

		bool DeletePayrollTransaction(string sysDocID, string voucherID);

		bool VoidPayrollTransaction(string sysDocID, string voucherID, bool isVoid);

		DataSet GetEmployeeSalaryReport(DateTime from, DateTime to, string fromEmployee, string toEmployee, string fromDepartment, string toDepartment, string fromLocation, string toLocation, bool showZeroBalance);

		DataSet GetSalaryCashList(DateTime from, DateTime to, bool isImport, bool showVoid);

		DataSet GetSalaryChequeList(DateTime from, DateTime to, bool isImport, bool showVoid);

		DataSet GetSalaryBankList(DateTime from, DateTime to, bool isImport, bool showVoid);

		DataSet GetEmployeeSalaryToPrint(string sysDocID, string voucherID);

		DataSet GeWPFToPrint(string sysDocID, string voucherID);

		decimal GetPaidSalaryAmount(string sheetSysDocID, string sheetVoucherID, string employeeID);
	}
}
