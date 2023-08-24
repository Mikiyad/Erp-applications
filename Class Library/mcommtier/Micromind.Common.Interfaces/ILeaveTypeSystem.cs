using Micromind.Common.Data;
using System.Data;

namespace Micromind.Common.Interfaces
{
	public interface ILeaveTypeSystem
	{
		bool CreateLeaveType(LeaveTypeData leaveTypeData);

		bool UpdateLeaveType(LeaveTypeData leaveTypeData);

		LeaveTypeData GetLeaveType();

		bool DeleteLeaveType(string ID);

		LeaveTypeData GetLeaveTypeByID(string id);

		DataSet GetLeaveTypeByFields(params string[] columns);

		DataSet GetLeaveTypeByFields(string[] ids, params string[] columns);

		DataSet GetLeaveTypeByFields(string[] ids, bool isInactive, params string[] columns);

		DataSet GetLeaveTypeList();

		DataSet GetLeaveTypeComboList();

		double GetDeductionProportion(string ID);

		DataSet GetEmployeeLeaveTypeComboList(string ID);
	}
}
