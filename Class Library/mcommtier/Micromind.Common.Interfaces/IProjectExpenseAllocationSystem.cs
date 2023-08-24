using Micromind.Common.Data;
using System;
using System.Data;

namespace Micromind.Common.Interfaces
{
	public interface IProjectExpenseAllocationSystem
	{
		bool CreateProjectExpenseAllocation(ProjectExpenseAllocationData inventoryAdjustmentData, bool isUpdate);

		ProjectExpenseAllocationData GetProjectExpenseAllocationByID(string sysDocID, string voucherID);

		bool DeleteProjectExpenseAllocation(string sysDocID, string voucherID);

		bool VoidProjectExpenseAllocation(string sysDocID, string voucherID, bool isVoid);

		DataSet GetEmployeeSalaryReport(DateTime from, DateTime to, string fromEmployee, string toEmployee, string fromDepartment, string toDepartment, string fromLocation, string toLocation, bool showZeroBalance);

		DataSet GetSalaryCashList(DateTime from, DateTime to, bool isImport, bool showVoid);

		DataSet GetSalaryChequeList(DateTime from, DateTime to, bool isImport, bool showVoid);

		DataSet GetSalaryBankList(DateTime from, DateTime to, bool isImport, bool showVoid);

		DataSet GetEmployeeSalaryToPrint(string sysDocID, string voucherID);

		DataSet GeWPFToPrint(string sysDocID, string voucherID);
	}
}
