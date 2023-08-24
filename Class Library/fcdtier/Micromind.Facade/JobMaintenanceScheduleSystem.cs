using Micromind.Common.Data;
using Micromind.Common.Interfaces;
using Micromind.Data;
using System;
using System.Data;

namespace Micromind.Facade
{
	public sealed class JobMaintenanceScheduleSystem : MarshalByRefObject, IJobMaintenanceScheduleSystem, IDisposable
	{
		private Config config;

		public JobMaintenanceScheduleSystem(Config config)
		{
			this.config = config;
		}

		public void Dispose()
		{
		}

		public bool CreateJobMaintenanceSchedule(JobMaintenanceScheduleData data, bool isUpdate)
		{
			return new JobMaintenanceSchedule(config).InsertUpdateJobMaintenanceSchedule(data, isUpdate);
		}

		public JobMaintenanceScheduleData GetJobMaintenanceScheduleByID(string sysDocID, string voucherID)
		{
			return new JobMaintenanceSchedule(config).GetJobMaintenanceScheduleByID(sysDocID, voucherID);
		}

		public DataSet GetJobMaintenanceScheduleAll()
		{
			return new JobMaintenanceSchedule(config).GetJobMaintenanceScheduleAll();
		}

		public bool DeleteJobMaintenanceSchedule(string sysDocID, string voucherID)
		{
			return new JobMaintenanceSchedule(config).DeleteJobMaintenanceSchedule(sysDocID, voucherID);
		}

		public DataSet GetJobMaintenanceScheduleToPrint(string sysDocID, string[] voucherID)
		{
			return new JobMaintenanceSchedule(config).GetJobMaintenanceScheduleToPrint(sysDocID, voucherID);
		}

		public DataSet GetJobMaintenanceScheduleToPrint(string sysDocID, string voucherID)
		{
			return new JobMaintenanceSchedule(config).GetJobMaintenanceScheduleToPrint(sysDocID, new string[1]
			{
				voucherID
			});
		}

		public DataSet GetList(DateTime fromDate, DateTime toDate, bool Isvoid)
		{
			return new JobMaintenanceSchedule(config).GetJobMaintenanceScheduleList(fromDate, toDate, Isvoid: true);
		}

		public DataSet GetOpenSchedules()
		{
			return new JobMaintenanceSchedule(config).GetOpenSchedules();
		}

		public DataSet GetOpenSchedulesDetails(string scheduleSysDocID, string schedulevoucherID, string RowIndexStr, bool tochk)
		{
			return new JobMaintenanceSchedule(config).GetOpenSchedulesDetails(scheduleSysDocID, schedulevoucherID, RowIndexStr, tochk);
		}
	}
}
