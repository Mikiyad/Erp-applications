using Micromind.Common.Data;
using Micromind.Common.Interfaces;
using Micromind.Data;
using System;
using System.Data;

namespace Micromind.Facade
{
	public sealed class OverTimeEntrySystem : MarshalByRefObject, IOverTimeEntrySystem, IDisposable
	{
		private Config config;

		public OverTimeEntrySystem(Config config)
		{
			this.config = config;
		}

		public void Dispose()
		{
		}

		public bool CreateOverTimeEntry(OverTimeEntryData data, bool isUpdate)
		{
			return new OverTimeEntry(config).InsertUpdateOverTimeEntry(data, isUpdate);
		}

		public OverTimeEntryData GetOverTimeEntryByID(string sysDocID, string voucherID)
		{
			return new OverTimeEntry(config).GetOverTimeEntryByID(sysDocID, voucherID);
		}

		public DataSet GetOverTimeEntryGroupByJob(string sysDocID, string voucherID)
		{
			return new OverTimeEntry(config).GetOverTimeEntryGroupByJob(sysDocID, voucherID);
		}

		public OverTimeEntryData GetOverTimeEntry(string employeeId, int year, int month)
		{
			return new OverTimeEntry(config).GetOverTimeEntry(employeeId, year, month);
		}

		public bool DeleteOverTimeEntry(string voucherID)
		{
			return new OverTimeEntry(config).DeleteOverTimeEntry(voucherID);
		}

		public DataSet GetOverTimeEntryToPrint(string sysDocID, string[] voucherID)
		{
			return new OverTimeEntry(config).GetOverTimeEntryToPrint(sysDocID, voucherID);
		}

		public DataSet GetOverTimeEntryToPrint(string sysDocID, string voucherID)
		{
			return new OverTimeEntry(config).GetOverTimeEntryToPrint(sysDocID, new string[1]
			{
				voucherID
			});
		}

		public DataSet GetOverTimeEntryReport(string fromEmployee, string toEmployee, string fromDepartment, string toDepartment, string fromLocation, string toLocation, DateTime from, DateTime to, string fromType, string toType, string fromDivision, string toDivision, string fromSponsor, string toSponsor, string fromGroup, string toGroup, string fromGrade, string toGrade, string fromPosition, string toPosition, string fromBank, string toBank, string fromAccount, string toAccount, string EmployeeIDs)
		{
			return new OverTimeEntry(config).GetOverTimeEntryReport(fromEmployee, toEmployee, fromDepartment, toDepartment, fromLocation, toLocation, from, to, fromType, toType, fromDivision, toDivision, fromSponsor, toSponsor, fromGroup, toGroup, fromGrade, toGrade, fromPosition, toPosition, fromBank, toBank, fromAccount, toAccount, EmployeeIDs);
		}

		public DataSet GetList(DateTime from, DateTime to, bool showVoid, string sysDocID)
		{
			return new OverTimeEntry(config).GetList(from, to, showVoid, sysDocID);
		}

		public DataSet GetOverTimeByPeriod(int month, int year)
		{
			return new OverTimeEntry(config).GetOverTimeByPeriod(month, year);
		}

		public DataSet GetApprovedList()
		{
			return new OverTimeEntry(config).GetApprovedList();
		}

		public bool IsValidEntry(string empNo, DateTime date)
		{
			return new OverTimeEntry(config).IsValidEntry(empNo, date);
		}
	}
}
