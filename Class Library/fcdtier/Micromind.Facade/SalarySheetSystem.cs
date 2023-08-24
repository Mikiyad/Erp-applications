using Micromind.Common.Data;
using Micromind.Common.Interfaces;
using Micromind.Data;
using System;
using System.Data;

namespace Micromind.Facade
{
	public sealed class SalarySheetSystem : MarshalByRefObject, ISalarySheetSystem, IDisposable
	{
		private Config config;

		public SalarySheetSystem(Config config)
		{
			this.config = config;
		}

		public void Dispose()
		{
		}

		public bool CreateSalarySheet(SalarySheetData data, bool isUpdate)
		{
			return new SalarySheet(config).InsertUpdateSalarySheet(data, isUpdate);
		}

		public SalarySheetData GetSalarySheetByID(string sysDocID, string voucherID)
		{
			return new SalarySheet(config).GetSalarySheetByID(sysDocID, voucherID);
		}

		public bool DeleteSalarySheet(string sysDocID, string voucherID)
		{
			return new SalarySheet(config).DeleteSalarySheet(sysDocID, voucherID);
		}

		public bool VoidSalarySheet(string sysDocID, string voucherID, bool isVoid)
		{
			return new SalarySheet(config).VoidSalarySheet(sysDocID, voucherID, isVoid);
		}

		public DataSet CalculateSalarySheet(string fromEmployee, string toEmployee, string fromDepartment, string toDepartment, string fromLocation, string toLocation, string fromType, string toType, string fromDivision, string toDivision, string fromSponsor, string toSponsor, string fromGroup, string toGroup, string fromGrade, string toGrade, string fromPosition, string toPostion, string fromBank, string toBank, string fromAccount, string toAccount, DateTime startDate, DateTime endDate, int periodYear, int periodMonth, string EmployeeIDs)
		{
			return new SalarySheet(config).CalculateSalarySheet(fromEmployee, toEmployee, fromDepartment, toDepartment, fromLocation, toLocation, fromType, toType, fromDivision, toDivision, fromSponsor, toSponsor, fromGroup, toGroup, fromGrade, toGrade, fromPosition, toPostion, fromBank, toBank, fromAccount, toAccount, startDate, endDate, periodYear, periodMonth, EmployeeIDs);
		}

		public bool PostSalarySheet(string sysDocID, string voucherID)
		{
			return new SalarySheet(config).PostSalarySheet(sysDocID, voucherID);
		}

		public DataSet GetUnpostedSalarySheets()
		{
			return new SalarySheet(config).GetUnpostedSalarySheets();
		}

		public DataSet GetOpenSalarySheets()
		{
			return new SalarySheet(config).GetOpenSalarySheets();
		}

		public DataSet GetSalarySheetEmployees(string docID, string voucherID, byte paymentMethodID)
		{
			return new SalarySheet(config).GetSalarySheetEmployees(docID, voucherID, paymentMethodID);
		}

		public DataSet GetSalarySheetItems(string sysDocID, string voucherID, string[] employeeIDs)
		{
			return new SalarySheet(config).GetSalarySheetItems(sysDocID, voucherID, employeeIDs);
		}

		public DataSet GetSelectedSalaryBankTransfer(string fromEmployee, string toEmployee, string fromDepartment, string toDepartment, string fromLocation, string toLocation, string fromType, string toType, string fromDivision, string toDivision, string fromSponsor, string toSponsor, string fromGroup, string toGroup, string fromGrade, string toGrade, string fromPosition, string toPostion, string fromBank, string toBank, string fromAccount, string toAccount, string sysdocid, string voucherid)
		{
			return new SalarySheet(config).GetSelectedSalaryBankTransfer(fromEmployee, toEmployee, fromDepartment, toDepartment, fromLocation, toLocation, fromType, toType, fromDivision, toDivision, fromSponsor, toSponsor, fromGroup, toGroup, fromGrade, toGrade, fromPosition, toPostion, fromBank, toBank, fromAccount, toAccount, sysdocid, voucherid);
		}

		public DataSet GetSelectedSalaryBankTransfer(string fromEmployee, string toEmployee, string fromDepartment, string toDepartment, string fromLocation, string toLocation, string fromType, string toType, string fromDivision, string toDivision, string fromSponsor, string toSponsor, string fromGroup, string toGroup, string fromGrade, string toGrade, string fromPosition, string toPostion, string fromBank, string toBank, string fromAccount, string toAccount, string[] sysdocid, string[] voucherid)
		{
			return new SalarySheet(config).GetSelectedSalaryBankTransfer(fromEmployee, toEmployee, fromDepartment, toDepartment, fromLocation, toLocation, fromType, toType, fromDivision, toDivision, fromSponsor, toSponsor, fromGroup, toGroup, fromGrade, toGrade, fromPosition, toPostion, fromBank, toBank, fromAccount, toAccount, sysdocid, voucherid);
		}

		public DataSet GeProjectExpenseSalarySheets(string fromEmployee, string toEmployee, string fromDepartment, string toDepartment, string fromLocation, string toLocation, string fromType, string toType, string fromDivision, string toDivision, string fromSponsor, string toSponsor, string fromGroup, string toGroup, string fromGrade, string toGrade, string fromPosition, string toPostion, string fromBank, string toBank, string fromAccount, string toAccount, string sysdocid, string voucherid)
		{
			return new SalarySheet(config).GeProjectExpenseSalarySheets(fromEmployee, toEmployee, fromDepartment, toDepartment, fromLocation, toLocation, fromType, toType, fromDivision, toDivision, fromSponsor, toSponsor, fromGroup, toGroup, fromGrade, toGrade, fromPosition, toPostion, fromBank, toBank, fromAccount, toAccount, sysdocid, voucherid);
		}

		public DataSet GetList(DateTime from, DateTime to, bool showVoid)
		{
			return new SalarySheet(config).GetList(from, to, showVoid);
		}

		public bool AllowDelete(string sysDocID, string voucherNumber)
		{
			return new SalarySheet(config).AllowDelete(sysDocID, voucherNumber);
		}

		public DataSet GetSalaryEmployeeSheetDetails(string Month, string Year, string EmployeeID)
		{
			return new SalarySheet(config).GetSalaryEmployeeSheetDetails(Month, Year, EmployeeID);
		}

		public DataSet ReCalculateSalarySheet(string fromEmployee, string toEmployee, string fromDepartment, string toDepartment, string fromLocation, string toLocation, string fromType, string toType, string fromDivision, string toDivision, string fromSponsor, string toSponsor, string fromGroup, string toGroup, string fromGrade, string toGrade, string fromPosition, string toPostion, string fromBank, string toBank, string fromAccount, string toAccount, DateTime startDate, DateTime endDate, int periodYear, int periodMonth, string EmployeeIDs, DataSet dsSSD, string strSysDocID, string strVoucherID)
		{
			return new SalarySheet(config).ReCalculateSalarySheet(fromEmployee, toEmployee, fromDepartment, toDepartment, fromLocation, toLocation, fromType, toType, fromDivision, toDivision, fromSponsor, toSponsor, fromGroup, toGroup, fromGrade, toGrade, fromPosition, toPostion, fromBank, toBank, fromAccount, toAccount, startDate, endDate, periodYear, periodMonth, EmployeeIDs, dsSSD, strSysDocID, strVoucherID);
		}
	}
}
