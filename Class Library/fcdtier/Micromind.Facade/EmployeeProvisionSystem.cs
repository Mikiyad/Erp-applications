using Micromind.Common.Data;
using Micromind.Common.Interfaces;
using Micromind.Data;
using System;
using System.Data;

namespace Micromind.Facade
{
	public sealed class EmployeeProvisionSystem : MarshalByRefObject, IEmployeeProvisionSystem, IDisposable
	{
		private Config config;

		public EmployeeProvisionSystem(Config config)
		{
			this.config = config;
		}

		public void Dispose()
		{
		}

		public bool CreateEmployeeProvision(EmployeeProvisionData data, bool isUpdate)
		{
			return new EmployeeProvision(config).InsertUpdateEmployeeProvision(data, isUpdate);
		}

		public EmployeeProvisionData GetEmployeeProvisionByID(string sysDocID, string voucherID)
		{
			return new EmployeeProvision(config).GetEmployeeProvisionByID(sysDocID, voucherID);
		}

		public bool DeleteEmployeeProvision(string sysDocID, string voucherID)
		{
			return new EmployeeProvision(config).DeleteEmployeeProvision(sysDocID, voucherID);
		}

		public DataSet GetList(DateTime from, DateTime to, bool showVoid)
		{
			using (EmployeeProvision employeeProvision = new EmployeeProvision(config))
			{
				return employeeProvision.GetList(from, to, showVoid);
			}
		}

		public DataSet GetEmployeeList(string fromEmployee, string toEmployee, string fromDepartment, string toDepartment, string fromLocation, string toLocation, string fromType, string toType, string fromDivision, string toDivision, string fromSponsor, string toSponsor, string fromGroup, string toGroup, string fromGrade, string toGrade, string fromPosition, string toPosition, string fromBank, string toBank, string fromAccount, string toAccount, bool showInactive, string EmployeeIDs, DateTime asOfDate, string voucherID)
		{
			using (EmployeeProvision employeeProvision = new EmployeeProvision(config))
			{
				return employeeProvision.GetEmployeeList(fromEmployee, toEmployee, fromDepartment, toDepartment, fromLocation, toLocation, fromType, toType, fromDivision, toDivision, fromSponsor, toSponsor, fromGroup, toGroup, fromGrade, toGrade, fromPosition, toPosition, fromBank, toBank, fromAccount, toAccount, showInactive, EmployeeIDs, asOfDate, voucherID);
			}
		}

		public DataSet GetEmployeeTicketDetails(string fromEmployee, string toEmployee, string fromDepartment, string toDepartment, string fromLocation, string toLocation, string fromType, string toType, string fromDivision, string toDivision, string fromSponsor, string toSponsor, string fromGroup, string toGroup, string fromGrade, string toGrade, string fromPosition, string toPosition, string fromBank, string toBank, string fromAccount, string toAccount, bool showInactive, string EmployeeIDs, DateTime asOfDate, string voucherID)
		{
			using (EmployeeProvision employeeProvision = new EmployeeProvision(config))
			{
				return employeeProvision.GetEmployeeTicketDetails(fromEmployee, toEmployee, fromDepartment, toDepartment, fromLocation, toLocation, fromType, toType, fromDivision, toDivision, fromSponsor, toSponsor, fromGroup, toGroup, fromGrade, toGrade, fromPosition, toPosition, fromBank, toBank, fromAccount, toAccount, showInactive, EmployeeIDs, asOfDate, voucherID);
			}
		}
	}
}
