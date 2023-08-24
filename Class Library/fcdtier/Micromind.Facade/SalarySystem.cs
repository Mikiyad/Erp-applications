using Micromind.Common.Data;
using Micromind.Common.Interfaces;
using Micromind.Data;
using System;
using System.Data;

namespace Micromind.Facade
{
	public sealed class SalarySystem : MarshalByRefObject, ISalarySystem, IDisposable
	{
		private Config config;

		public SalarySystem(Config config)
		{
			this.config = config;
		}

		public void Dispose()
		{
		}

		public bool CreateSalaryDeduction(SalaryData data, bool isUpdate)
		{
			return new Salary(config).InsertUpdateSalaryDeduction(data, isUpdate);
		}

		public bool CreateSalaryAddition(SalaryData data, bool isUpdate)
		{
			return new Salary(config).InsertUpdateSalaryAddition(data, isUpdate);
		}

		public SalaryData GetSalaryDeductionByID(string voucherID)
		{
			return new Salary(config).GetSalaryDeductionByID(voucherID);
		}

		public SalaryData GetSalaryAdditionByID(string voucherID)
		{
			return new Salary(config).GetSalaryAdditionByID(voucherID);
		}

		public bool DeleteSalaryDeduction(string voucherID)
		{
			return new Salary(config).DeleteSalaryDeduction(voucherID);
		}

		public bool DeleteSalaryAddition(string voucherID)
		{
			return new Salary(config).DeleteSalaryAddition(voucherID);
		}

		public DataSet GetSalaryDeductionToPrint(string[] voucherID)
		{
			return new Salary(config).GetSalaryDeductionToPrint(voucherID);
		}

		public DataSet GetSalaryDeductionToPrint(string voucherID)
		{
			return new Salary(config).GetSalaryDeductionToPrint(new string[1]
			{
				voucherID
			});
		}

		public DataSet GetSalaryAdditionToPrint(string[] voucherID)
		{
			return new Salary(config).GetSalaryAdditionToPrint(voucherID);
		}

		public DataSet GetSalaryAdditionToPrint(string voucherID)
		{
			return new Salary(config).GetSalaryAdditionToPrint(new string[1]
			{
				voucherID
			});
		}

		public DataSet LoadTicketAmount(string fromEmployee, string toEmployee, string fromDepartment, string toDepartment, string fromLocation, string toLocation, string fromType, string toType, string fromDivision, string toDivision, string fromSponsor, string toSponsor, string fromGroup, string toGroup, string fromGrade, string toGrade, string fromPosition, string toPostion, string fromBank, string toBank, string fromAccount, string toAccount, DateTime from, string multipleEmployees)
		{
			return new Salary(config).LoadTicketAmount(fromEmployee, toEmployee, fromDepartment, toDepartment, fromLocation, toLocation, fromType, toType, fromDivision, toDivision, fromSponsor, toSponsor, fromGroup, toGroup, fromGrade, toGrade, fromPosition, toPostion, fromBank, toBank, fromAccount, toAccount, from, multipleEmployees);
		}

		public DataSet LoadDeductionAmount(string fromEmployee, string toEmployee, string fromDepartment, string toDepartment, string fromLocation, string toLocation, string fromType, string toType, string fromDivision, string toDivision, string fromSponsor, string toSponsor, string fromGroup, string toGroup, string fromGrade, string toGrade, string fromPosition, string toPostion, string fromBank, string toBank, string fromAccount, string toAccount, DateTime from, string multipleEmployees, int percent)
		{
			return new Salary(config).LoadDeductionAmount(fromEmployee, toEmployee, fromDepartment, toDepartment, fromLocation, toLocation, fromType, toType, fromDivision, toDivision, fromSponsor, toSponsor, fromGroup, toGroup, fromGrade, toGrade, fromPosition, toPostion, fromBank, toBank, fromAccount, toAccount, from, multipleEmployees, percent);
		}

		public DataSet LoadPayrollItem(string fromEmployee, string toEmployee, string fromDepartment, string toDepartment, string fromLocation, string toLocation, string fromType, string toType, string fromDivision, string toDivision, string fromSponsor, string toSponsor, string fromGroup, string toGroup, string fromGrade, string toGrade, string fromPosition, string toPostion, string fromBank, string toBank, string fromAccount, string toAccount, DateTime from, string multipleEmployees, string basedon)
		{
			return new Salary(config).LoadPayrollItem(fromEmployee, toEmployee, fromDepartment, toDepartment, fromLocation, toLocation, fromType, toType, fromDivision, toDivision, fromSponsor, toSponsor, fromGroup, toGroup, fromGrade, toGrade, fromPosition, toPostion, fromBank, toBank, fromAccount, toAccount, from, multipleEmployees, basedon);
		}

		public decimal GetTotalLeaveSalary(string EmployeeID)
		{
			return new Salary(config).TotalLeaveSalary(EmployeeID);
		}

		public DataSet GetAdditionList(DateTime from, DateTime to, bool showVoid)
		{
			return new Salary(config).GetAdditionList(from, to, showVoid);
		}

		public DataSet GetDeductionList(DateTime from, DateTime to, bool showVoid)
		{
			return new Salary(config).GetDeductionList(from, to, showVoid);
		}
	}
}
