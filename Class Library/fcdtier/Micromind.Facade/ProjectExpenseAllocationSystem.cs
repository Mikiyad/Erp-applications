using Micromind.Common.Data;
using Micromind.Common.Interfaces;
using Micromind.Data;
using System;
using System.Data;

namespace Micromind.Facade
{
	public sealed class ProjectExpenseAllocationSystem : MarshalByRefObject, IProjectExpenseAllocationSystem, IDisposable
	{
		private Config config;

		public ProjectExpenseAllocationSystem(Config config)
		{
			this.config = config;
		}

		public void Dispose()
		{
		}

		public bool CreateProjectExpenseAllocation(ProjectExpenseAllocationData data, bool isUpdate)
		{
			return new ProjectExpenseAllocation(config).InsertUpdateProjectExpenseAllocation(data, isUpdate);
		}

		public ProjectExpenseAllocationData GetProjectExpenseAllocationByID(string sysDocID, string voucherID)
		{
			return new ProjectExpenseAllocation(config).GetProjectExpenseAllocationByID(sysDocID, voucherID);
		}

		public bool DeleteProjectExpenseAllocation(string sysDocID, string voucherID)
		{
			return new ProjectExpenseAllocation(config).DeleteProjectExpenseAllocation(sysDocID, voucherID);
		}

		public bool VoidProjectExpenseAllocation(string sysDocID, string voucherID, bool isVoid)
		{
			return new ProjectExpenseAllocation(config).VoidProjectExpenseAllocation(sysDocID, voucherID, isVoid);
		}

		public DataSet GetEmployeeSalaryReport(DateTime from, DateTime to, string fromEmployee, string toEmployee, string fromDepartment, string toDepartment, string fromLocation, string toLocation, bool showZeroBalance)
		{
			return new ProjectExpenseAllocation(config).GetEmployeeSalaryReport(from, to, fromEmployee, toEmployee, fromDepartment, toDepartment, fromLocation, toLocation, showZeroBalance);
		}

		public DataSet GetSalaryCashList(DateTime from, DateTime to, bool isImport, bool showVoid)
		{
			return new ProjectExpenseAllocation(config).GetSalaryCashList(from, to, isImport, showVoid);
		}

		public DataSet GetSalaryChequeList(DateTime from, DateTime to, bool isImport, bool showVoid)
		{
			return new ProjectExpenseAllocation(config).GetSalaryChequeList(from, to, isImport, showVoid);
		}

		public DataSet GetSalaryBankList(DateTime from, DateTime to, bool isImport, bool showVoid)
		{
			return new ProjectExpenseAllocation(config).GetSalaryBankList(from, to, isImport, showVoid);
		}

		public DataSet GetEmployeeSalaryToPrint(string sysDocID, string voucherID)
		{
			return new ProjectExpenseAllocation(config).GetEmployeeSalaryToPrint(sysDocID, voucherID);
		}

		public DataSet GeWPFToPrint(string sysDocID, string voucherID)
		{
			return new ProjectExpenseAllocation(config).GeWPFToPrint(sysDocID, voucherID);
		}
	}
}
