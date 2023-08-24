using Micromind.Common.Data;
using Micromind.Common.Interfaces;
using Micromind.Data;
using System;
using System.Data;

namespace Micromind.Facade
{
	public sealed class LeaveTypeSystem : MarshalByRefObject, ILeaveTypeSystem, IDisposable
	{
		private Config config;

		public LeaveTypeSystem(Config config)
		{
			this.config = config;
		}

		public void Dispose()
		{
		}

		public bool CreateLeaveType(LeaveTypeData data)
		{
			return new LeaveType(config).InsertLeaveType(data);
		}

		public bool UpdateLeaveType(LeaveTypeData data)
		{
			return UpdateLeaveType(data, checkConcurrency: false);
		}

		public bool UpdateLeaveType(LeaveTypeData data, bool checkConcurrency)
		{
			return new LeaveType(config).UpdateLeaveType(data);
		}

		public LeaveTypeData GetLeaveType()
		{
			using (LeaveType leaveType = new LeaveType(config))
			{
				return leaveType.GetLeaveType();
			}
		}

		public bool DeleteLeaveType(string groupID)
		{
			using (LeaveType leaveType = new LeaveType(config))
			{
				return leaveType.DeleteLeaveType(groupID);
			}
		}

		public LeaveTypeData GetLeaveTypeByID(string id)
		{
			using (LeaveType leaveType = new LeaveType(config))
			{
				return leaveType.GetLeaveTypeByID(id);
			}
		}

		public DataSet GetLeaveTypeByFields(params string[] columns)
		{
			using (LeaveType leaveType = new LeaveType(config))
			{
				return leaveType.GetLeaveTypeByFields(columns);
			}
		}

		public DataSet GetLeaveTypeByFields(string[] ids, params string[] columns)
		{
			using (LeaveType leaveType = new LeaveType(config))
			{
				return leaveType.GetLeaveTypeByFields(ids, columns);
			}
		}

		public DataSet GetLeaveTypeByFields(string[] ids, bool isInactive, params string[] columns)
		{
			using (LeaveType leaveType = new LeaveType(config))
			{
				return leaveType.GetLeaveTypeByFields(ids, isInactive, columns);
			}
		}

		public DataSet GetLeaveTypeList()
		{
			using (LeaveType leaveType = new LeaveType(config))
			{
				return leaveType.GetLeaveTypeList();
			}
		}

		public DataSet GetLeaveTypeComboList()
		{
			using (LeaveType leaveType = new LeaveType(config))
			{
				return leaveType.GetLeaveTypeComboList();
			}
		}

		public double GetDeductionProportion(string ID)
		{
			using (LeaveType leaveType = new LeaveType(config))
			{
				if (string.IsNullOrWhiteSpace(ID))
				{
					return 0.0;
				}
				return leaveType.GetDeductionProportion(ID);
			}
		}

		public DataSet GetEmployeeLeaveTypeComboList(string ID)
		{
			using (LeaveType leaveType = new LeaveType(config))
			{
				return leaveType.GetEmployeeLeaveTypeComboList(ID);
			}
		}
	}
}
