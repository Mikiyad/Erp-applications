using Micromind.Common.Data;
using Micromind.Common.Interfaces;
using Micromind.Data;
using System;
using System.Data;

namespace Micromind.Facade
{
	public sealed class JobMaintenanceServiceSystem : MarshalByRefObject, IJobMaintenanceServiceSystem, IDisposable
	{
		private Config config;

		public JobMaintenanceServiceSystem(Config config)
		{
			this.config = config;
		}

		public void Dispose()
		{
		}

		public bool CreateJobMaintenanceService(JobMaintenanceServiceData data, bool isUpdate)
		{
			return new JobMaintenanceService(config).InsertUpdateJobMaintenanceService(data, isUpdate);
		}

		public JobMaintenanceServiceData GetJobMaintenanceServiceByID(string sysDocID, string voucherID)
		{
			return new JobMaintenanceService(config).GetJobMaintenanceServiceByID(sysDocID, voucherID);
		}

		public DataSet GetJobMaintenanceServiceAll()
		{
			return new JobMaintenanceService(config).GetJobMaintenanceServiceAll();
		}

		public bool DeleteJobMaintenanceService(string sysDocID, string voucherID)
		{
			return new JobMaintenanceService(config).DeleteJobMaintenanceService(sysDocID, voucherID);
		}

		public DataSet GetJobMaintenanceServiceToPrint(string sysDocID, string[] voucherID)
		{
			return new JobMaintenanceService(config).GetJobMaintenanceServiceToPrint(sysDocID, voucherID);
		}

		public DataSet GetJobMaintenanceServiceToPrint(string sysDocID, string voucherID)
		{
			return new JobMaintenanceService(config).GetJobMaintenanceServiceToPrint(sysDocID, new string[1]
			{
				voucherID
			});
		}

		public DataSet GetList(DateTime fromDate, DateTime toDate, bool Isvoid)
		{
			return new JobMaintenanceService(config).GetJobMaintenanceServiceList(fromDate, toDate, Isvoid: true);
		}

		public DataSet GetOpenSchedules()
		{
			return new JobMaintenanceService(config).GetOpenSchedules();
		}

		public DataSet GetOpenSchedulesDetails(string scheduleSysDocID, string schedulevoucherID)
		{
			return new JobMaintenanceService(config).GetOpenSchedulesDetails(scheduleSysDocID, schedulevoucherID);
		}
	}
}
