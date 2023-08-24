using Micromind.Common.Data;
using Micromind.Common.Interfaces;
using Micromind.Data;
using System;
using System.Data;

namespace Micromind.Facade
{
	public sealed class EmployeeActivitySystem : MarshalByRefObject, IEmployeeActivitySystem, IDisposable
	{
		private Config config;

		public EmployeeActivitySystem(Config config)
		{
			this.config = config;
		}

		public void Dispose()
		{
		}

		public bool CreateEmployeeActivity(EmployeeActivityData data, EmployeeActivityTypes activityType, bool isUpdate)
		{
			return new EmployeeActivity(config).InsertUpdateEmployeeActivity(data, activityType, isUpdate);
		}

		public EmployeeActivityData GetEmployeeActivityByID(int activityID)
		{
			return new EmployeeActivity(config).GetEmployeeActivityByID(activityID);
		}

		public bool DeleteActivity(string activityID)
		{
			return new EmployeeActivity(config).DeleteActivity(activityID);
		}

		public bool DeleteActivity(string activityID, EmployeeActivityTypes activityType)
		{
			return new EmployeeActivity(config).DeleteActivity(activityID, activityType);
		}

		public DataSet GetUnapprovedLeaveRequests()
		{
			return new EmployeeActivity(config).GetUnapprovedLeaveRequests();
		}

		public bool ApproveRejectLeaveRequest(int activityID, string employeeID, DateTime startDate, DateTime endDate, bool isApprove, string remarks)
		{
			return new EmployeeActivity(config).ApproveRejectLeaveRequest(activityID, employeeID, startDate, endDate, isApprove, remarks);
		}

		public DataSet GetEmployeesOnLeave()
		{
			return new EmployeeActivity(config).GetEmployeesOnLeave();
		}

		public DataSet GetEmployeeLeaveHistory(string employeeId)
		{
			return new EmployeeActivity(config).GetEmployeeLeaveHistory(employeeId);
		}

		public DataSet GetEmployeeAppraisalHistory(string employeeId)
		{
			return new EmployeeActivity(config).GetEmployeeAppraisalHistory(employeeId);
		}

		public DataSet GetAllEmployeeApprovedLeaves(string employeeID)
		{
			return new EmployeeActivity(config).GetAllEmployeeApprovedLeaves(employeeID);
		}

		public DataSet GetEmployeeActivityToPrint(int activityID)
		{
			return new EmployeeActivity(config).GetEmployeeActivityToPrint(activityID);
		}

		public DataSet GetEmployeeResumptionDataToPrint(int activityID)
		{
			return new EmployeeActivity(config).GetEmployeeResumptionDataToPrint(activityID);
		}

		public DataSet GetEmployeesLeavesToResume()
		{
			return new EmployeeActivity(config).GetEmployeesLeavesToResume();
		}

		public DataSet GetEmployeeLeaveByID(string activityID)
		{
			return new EmployeeActivity(config).GetEmployeeLeaveByID(activityID);
		}

		public DataSet GetEmployeeLeaveDetailList()
		{
			using (EmployeeActivity employeeActivity = new EmployeeActivity(config))
			{
				return employeeActivity.GetEmployeeLeaveList();
			}
		}

		public DataSet GetEmployeeLeaveList(DateTime from, DateTime to, bool showVoid)
		{
			using (EmployeeActivity employeeActivity = new EmployeeActivity(config))
			{
				return employeeActivity.GetEmployeeLeaveList(from, to, showVoid);
			}
		}

		public DataSet GetEmployeeLeaveResumptionDetailList()
		{
			using (EmployeeActivity employeeActivity = new EmployeeActivity(config))
			{
				return employeeActivity.GetEmployeeLeaveResumptionDetailList();
			}
		}

		public string EncashmentAmount(string employeeID)
		{
			using (EmployeeActivity employeeActivity = new EmployeeActivity(config))
			{
				return employeeActivity.EncashmentAmount(employeeID);
			}
		}

		public DataSet GetEmployeeLeaveList(string employeeID)
		{
			using (EmployeeActivity employeeActivity = new EmployeeActivity(config))
			{
				return employeeActivity.GetEmployeeLeaveList(employeeID);
			}
		}

		public DataSet GetEmployeePassportControlList()
		{
			using (EmployeeActivity employeeActivity = new EmployeeActivity(config))
			{
				return employeeActivity.GetEmployeePassportControlList();
			}
		}

		public bool IsPassportAllocated(string employeeId)
		{
			using (EmployeeActivity employeeActivity = new EmployeeActivity(config))
			{
				return employeeActivity.IsPassportAllocated(employeeId);
			}
		}

		public DataSet GetActivityList()
		{
			using (EmployeeActivity employeeActivity = new EmployeeActivity(config))
			{
				return employeeActivity.GetActivityList();
			}
		}

		public DataSet GetEmployeeAbscondingEntryList()
		{
			using (EmployeeActivity employeeActivity = new EmployeeActivity(config))
			{
				return employeeActivity.GetEmployeeAbscondingEntryList();
			}
		}

		public EmployeeActivityData GetEmployeeGeneralActivityByID(string sysDocID, string voucherID)
		{
			return new EmployeeActivity(config).GetEmployeeGeneralActivityByID(sysDocID, voucherID);
		}

		public EmployeeActivityData GetEmployeeLeavePaymentByID(string sysDocID, string voucherID)
		{
			return new EmployeeActivity(config).GetEmployeeLeavePaymentActivityByID(sysDocID, voucherID);
		}

		public DataSet GetGeneralActivityList(DateTime from, DateTime to, bool showVoid)
		{
			return new EmployeeActivity(config).GetGeneralActivityList(from, to, showVoid);
		}

		public bool DeleteGeneralActivity(string sysDocID, string voucherID)
		{
			return new EmployeeActivity(config).DeleteEmployeeGeneralActivity(sysDocID, voucherID);
		}

		public DataSet GetEmployeeGeneralActivityToPrint(string sysDocID, string voucherID)
		{
			return new EmployeeActivity(config).GetEmployeeGeneralActivityToPrint(sysDocID, voucherID);
		}

		public DataSet AllowDelete(string EmpID, DateTime startDate)
		{
			return new EmployeeActivity(config).AllowDelete(EmpID, startDate);
		}

		public DataSet GetEmployeeLeavePaymentList(DateTime from, DateTime to, bool showVoid)
		{
			using (EmployeeActivity employeeActivity = new EmployeeActivity(config))
			{
				return employeeActivity.GetEmployeeLeavePaymentList(from, to, showVoid);
			}
		}
	}
}
