using Micromind.Common.Data;
using Micromind.Common.Interfaces;
using Micromind.Data;
using System;
using System.Data;

namespace Micromind.Facade
{
	public sealed class EmployeeLeaveProcessSystem : MarshalByRefObject, IEmployeeLeaveProcessSystem, IDisposable
	{
		private Config config;

		public EmployeeLeaveProcessSystem(Config config)
		{
			this.config = config;
		}

		public void Dispose()
		{
		}

		public bool CreateEmployeeLeaveDetail(EmployeeLeaveProcessData data)
		{
			return new EmployeeLeaveProcess(config).InsertEmployeeLeave(data);
		}

		public bool UpdateEmployeeLeaveDetail(EmployeeLeaveProcessData data)
		{
			return UpdateEmployeeLeaveDetail(data, checkConcurrency: false);
		}

		public bool UpdateEmployeeLeaveDetail(EmployeeLeaveProcessData data, bool checkConcurrency)
		{
			return new EmployeeLeaveProcess(config).UpdateEmployeeLeave(data);
		}

		public EmployeeLeaveProcessData GetEmployeeLeaveDetail()
		{
			using (EmployeeLeaveProcess employeeLeaveProcess = new EmployeeLeaveProcess(config))
			{
				return employeeLeaveProcess.GetEmployeeLeave();
			}
		}

		public bool DeleteEmployeeLeaveDetail(string groupID)
		{
			using (EmployeeLeaveProcess employeeLeaveProcess = new EmployeeLeaveProcess(config))
			{
				return employeeLeaveProcess.DeleteEmployeeLeave(groupID);
			}
		}

		public EmployeeLeaveProcessData GetEmployeeLeaveDetailByID(string id)
		{
			using (EmployeeLeaveProcess employeeLeaveProcess = new EmployeeLeaveProcess(config))
			{
				return employeeLeaveProcess.GetEmployeeLeaveByID(id);
			}
		}

		public EmployeeLeaveProcessData GetEmployeeLeaveDetailsByEmployeeID(string employeeID)
		{
			using (EmployeeLeaveProcess employeeLeaveProcess = new EmployeeLeaveProcess(config))
			{
				return employeeLeaveProcess.GetEmployeeLeavesByEmployeeID(employeeID);
			}
		}

		public DataSet GetEmployeeLeaveDetailByFields(params string[] columns)
		{
			using (EmployeeLeaveProcess employeeLeaveProcess = new EmployeeLeaveProcess(config))
			{
				return employeeLeaveProcess.GetEmployeeLeaveByFields(columns);
			}
		}

		public DataSet GetEmployeeLeaveDetailByFields(string[] ids, params string[] columns)
		{
			using (EmployeeLeaveProcess employeeLeaveProcess = new EmployeeLeaveProcess(config))
			{
				return employeeLeaveProcess.GetEmployeeLeaveByFields(ids, columns);
			}
		}

		public DataSet GetEmployeeLeaveDetailByFields(string[] ids, bool isInactive, params string[] columns)
		{
			using (EmployeeLeaveProcess employeeLeaveProcess = new EmployeeLeaveProcess(config))
			{
				return employeeLeaveProcess.GetEmployeeLeaveByFields(ids, isInactive, columns);
			}
		}

		public DataSet GetEmployeeLeaveDetailComboList()
		{
			using (EmployeeLeaveProcess employeeLeaveProcess = new EmployeeLeaveProcess(config))
			{
				return employeeLeaveProcess.GetEmployeeLeaveComboList();
			}
		}

		public bool IsLeaveExist(string employeeId, DateTime startDate, DateTime endDate)
		{
			using (EmployeeLeaveProcess employeeLeaveProcess = new EmployeeLeaveProcess(config))
			{
				return employeeLeaveProcess.IsLeaveExist(employeeId, startDate, endDate);
			}
		}

		public bool DeleteEmployeeLeaveDetail(string sysDocID, string voucherID)
		{
			return new EmployeeLeaveProcess(config).DeleteEmployeeLeaveDetail(sysDocID, voucherID);
		}

		public bool VoidEmployeeLeaveDetail(string sysDocID, string voucherID, bool isVoid)
		{
			return new EmployeeLeaveProcess(config).VoidEmployeeLeaveDetail(sysDocID, voucherID, isVoid);
		}

		public bool DeleteEmployeePassportDetail(string sysDocID, string voucherID)
		{
			return new EmployeeLeaveProcess(config).DeleteEmployeePassportDetail(sysDocID, voucherID);
		}

		public DataSet GetEmployeeLeaveAvailability(string ID)
		{
			using (EmployeeLeaveProcess employeeLeaveProcess = new EmployeeLeaveProcess(config))
			{
				return employeeLeaveProcess.GetEmployeeLeaveAvailability(ID);
			}
		}

		public int GetEmployeeLeaveDays(string EmployeeID)
		{
			using (EmployeeLeaveProcess employeeLeaveProcess = new EmployeeLeaveProcess(config))
			{
				return employeeLeaveProcess.GetEmployeeLeaveDays(EmployeeID);
			}
		}

		public DataSet GetList(DateTime from, DateTime to, bool showVoid)
		{
			using (EmployeeLeaveProcess employeeLeaveProcess = new EmployeeLeaveProcess(config))
			{
				return employeeLeaveProcess.GetList(from, to, showVoid);
			}
		}
	}
}
