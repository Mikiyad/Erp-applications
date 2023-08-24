using Micromind.Common.Data;
using Micromind.Common.Interfaces;
using Micromind.Data;
using System;
using System.Data;

namespace Micromind.Facade
{
	public sealed class EmployeeLeaveDetailSystem : MarshalByRefObject, IEmployeeLeaveDetailSystem, IDisposable
	{
		private Config config;

		public EmployeeLeaveDetailSystem(Config config)
		{
			this.config = config;
		}

		public void Dispose()
		{
		}

		public bool CreateEmployeeLeaveDetail(EmployeeLeaveDetailData data)
		{
			return new EmployeeLeaveDetails(config).InsertEmployeeLeave(data);
		}

		public bool UpdateEmployeeLeaveDetail(EmployeeLeaveDetailData data)
		{
			return UpdateEmployeeLeaveDetail(data, checkConcurrency: false);
		}

		public bool UpdateEmployeeLeaveDetail(EmployeeLeaveDetailData data, bool checkConcurrency)
		{
			return new EmployeeLeaveDetails(config).UpdateEmployeeLeave(data);
		}

		public EmployeeLeaveDetailData GetEmployeeLeaveDetail()
		{
			using (EmployeeLeaveDetails employeeLeaveDetails = new EmployeeLeaveDetails(config))
			{
				return employeeLeaveDetails.GetEmployeeLeave();
			}
		}

		public bool DeleteEmployeeLeaveDetail(string groupID)
		{
			using (EmployeeLeaveDetails employeeLeaveDetails = new EmployeeLeaveDetails(config))
			{
				return employeeLeaveDetails.DeleteEmployeeLeave(groupID);
			}
		}

		public EmployeeLeaveDetailData GetEmployeeLeaveDetailByID(string id)
		{
			using (EmployeeLeaveDetails employeeLeaveDetails = new EmployeeLeaveDetails(config))
			{
				return employeeLeaveDetails.GetEmployeeLeaveByID(id);
			}
		}

		public EmployeeLeaveDetailData GetEmployeeLeaveDetailsByEmployeeID(string employeeID)
		{
			using (EmployeeLeaveDetails employeeLeaveDetails = new EmployeeLeaveDetails(config))
			{
				return employeeLeaveDetails.GetEmployeeLeavesByEmployeeID(employeeID);
			}
		}

		public DataSet GetEmployeeLeaveDetailByFields(params string[] columns)
		{
			using (EmployeeLeaveDetails employeeLeaveDetails = new EmployeeLeaveDetails(config))
			{
				return employeeLeaveDetails.GetEmployeeLeaveByFields(columns);
			}
		}

		public DataSet GetEmployeeLeaveDetailByFields(string[] ids, params string[] columns)
		{
			using (EmployeeLeaveDetails employeeLeaveDetails = new EmployeeLeaveDetails(config))
			{
				return employeeLeaveDetails.GetEmployeeLeaveByFields(ids, columns);
			}
		}

		public DataSet GetEmployeeLeaveDetailByFields(string[] ids, bool isInactive, params string[] columns)
		{
			using (EmployeeLeaveDetails employeeLeaveDetails = new EmployeeLeaveDetails(config))
			{
				return employeeLeaveDetails.GetEmployeeLeaveByFields(ids, isInactive, columns);
			}
		}

		public DataSet GetEmployeeLeaveDetailComboList()
		{
			using (EmployeeLeaveDetails employeeLeaveDetails = new EmployeeLeaveDetails(config))
			{
				return employeeLeaveDetails.GetEmployeeLeaveComboList();
			}
		}

		public bool IsLeaveExist(string employeeId, DateTime startDate, DateTime endDate)
		{
			using (EmployeeLeaveDetails employeeLeaveDetails = new EmployeeLeaveDetails(config))
			{
				return employeeLeaveDetails.IsLeaveExist(employeeId, startDate, endDate);
			}
		}

		public bool DeleteEmployeeLeaveDetail(string sysDocID, string voucherID)
		{
			return new EmployeeLeaveDetails(config).DeleteEmployeeLeaveDetail(sysDocID, voucherID);
		}

		public bool VoidEmployeeLeaveDetail(string sysDocID, string voucherID, bool isVoid)
		{
			return new EmployeeLeaveDetails(config).VoidEmployeeLeaveDetail(sysDocID, voucherID, isVoid);
		}

		public bool DeleteEmployeePassportDetail(string sysDocID, string voucherID)
		{
			return new EmployeeLeaveDetails(config).DeleteEmployeePassportDetail(sysDocID, voucherID);
		}

		public DataSet GetEmployeeLeaveAvailability(string ID, DateTime AsonDate, DateTime ToDate, string LeaveTypeID)
		{
			using (EmployeeLeaveDetails employeeLeaveDetails = new EmployeeLeaveDetails(config))
			{
				return employeeLeaveDetails.GetEmployeeLeaveAvailability(ID, AsonDate, ToDate, LeaveTypeID);
			}
		}

		public bool IsLeaveProcessDateExists(DateTime OnDate, string employeeId)
		{
			using (EmployeeLeaveDetails employeeLeaveDetails = new EmployeeLeaveDetails(config))
			{
				return employeeLeaveDetails.IsLeaveProcessDateExists(OnDate, employeeId);
			}
		}

		public bool IsDocNoExist(string DocNo)
		{
			using (EmployeeLeaveDetails employeeLeaveDetails = new EmployeeLeaveDetails(config))
			{
				return employeeLeaveDetails.IsDocNoExist(DocNo);
			}
		}
	}
}
