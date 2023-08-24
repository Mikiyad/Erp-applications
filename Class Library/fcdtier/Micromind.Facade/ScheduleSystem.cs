using Micromind.Common.Data;
using Micromind.Common.Interfaces;
using Micromind.Data;
using System;
using System.Data;

namespace Micromind.Facade
{
	public sealed class ScheduleSystem : MarshalByRefObject, IScheduleSystem, IDisposable
	{
		private Config config;

		public ScheduleSystem(Config config)
		{
			this.config = config;
		}

		public void Dispose()
		{
		}

		public bool UpdateSchedule(ScheduleData scheduleData)
		{
			return UpdateSchedule(scheduleData, null);
		}

		public bool UpdateSchedule(ScheduleData scheduleData, ScheduleUserData scheduleUserData)
		{
			return new Schedules(config).UpdateSchedule(scheduleData, scheduleUserData);
		}

		public int CreateSchedule(ScheduleData scheduleData)
		{
			return CreateSchedule(scheduleData, null);
		}

		public int CreateSchedule(ScheduleData scheduleData, ScheduleUserData scheduleUserData)
		{
			int result = -1;
			if (new Schedules(config).InsertSchedule(scheduleData, scheduleUserData))
			{
				try
				{
					return int.Parse(scheduleData.ScheduleTable.Rows[0]["ScheduleID"].ToString());
				}
				catch
				{
					return -1;
				}
			}
			return result;
		}

		public bool SetStatus(int scheduleID, ScheduleStatus status)
		{
			using (Schedules schedules = new Schedules(config))
			{
				return schedules.SetStatus(scheduleID, status);
			}
		}

		public bool SetStatus(int scheduleID, ScheduleStatus status, DateTime dateCompleted)
		{
			using (Schedules schedules = new Schedules(config))
			{
				return schedules.SetStatus(scheduleID, status, dateCompleted);
			}
		}

		public DataSet GetSchedulesByFields(int[] schedulesID, params string[] columns)
		{
			using (Schedules schedules = new Schedules(config))
			{
				return schedules.GetSchedulesByFields(schedulesID, columns);
			}
		}

		public bool DeleteSchedule(int scheduleID)
		{
			using (Schedules schedules = new Schedules(config))
			{
				return schedules.DeleteSchedule(scheduleID);
			}
		}

		public bool AssignScheduleUsers(int scheduleID, int[] usersID)
		{
			using (Schedules schedules = new Schedules(config))
			{
				return schedules.AssignScheduleUsers(scheduleID, usersID);
			}
		}

		public ScheduleData GetScheduleByID(int scheduleID)
		{
			using (Schedules schedules = new Schedules(config))
			{
				return schedules.GetScheduleByID(scheduleID);
			}
		}

		public DataSet GetScheduleByUserByFields(int userID, params string[] columns)
		{
			using (Schedules schedules = new Schedules(config))
			{
				return schedules.GetScheduleByUserByFields(userID, columns);
			}
		}
	}
}
