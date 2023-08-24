using Micromind.Common.Data;
using System;
using System.Data;

namespace Micromind.Common.Interfaces
{
	public interface IScheduleSystem
	{
		bool UpdateSchedule(ScheduleData scheduleData);

		bool UpdateSchedule(ScheduleData scheduleData, ScheduleUserData scheduleUserData);

		int CreateSchedule(ScheduleData scheduleData);

		int CreateSchedule(ScheduleData scheduleData, ScheduleUserData scheduleUserData);

		bool SetStatus(int scheduleID, ScheduleStatus status);

		bool SetStatus(int scheduleID, ScheduleStatus status, DateTime dateCompleted);

		DataSet GetSchedulesByFields(int[] schedulesID, params string[] columns);

		bool DeleteSchedule(int scheduleID);

		bool AssignScheduleUsers(int scheduleID, int[] usersID);

		ScheduleData GetScheduleByID(int scheduleID);

		DataSet GetScheduleByUserByFields(int userID, params string[] columns);
	}
}
