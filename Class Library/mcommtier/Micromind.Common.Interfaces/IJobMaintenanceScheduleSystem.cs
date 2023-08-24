using Micromind.Common.Data;
using System;
using System.Data;

namespace Micromind.Common.Interfaces
{
	public interface IJobMaintenanceScheduleSystem
	{
		bool CreateJobMaintenanceSchedule(JobMaintenanceScheduleData materialEstimatetData, bool isUpdate);

		JobMaintenanceScheduleData GetJobMaintenanceScheduleByID(string sysDocID, string voucherID);

		DataSet GetJobMaintenanceScheduleAll();

		DataSet GetJobMaintenanceScheduleToPrint(string sysDocID, string[] voucherID);

		DataSet GetJobMaintenanceScheduleToPrint(string sysDocID, string voucherID);

		bool DeleteJobMaintenanceSchedule(string sysDocID, string voucherID);

		DataSet GetList(DateTime from, DateTime to, bool showVoid);

		DataSet GetOpenSchedules();

		DataSet GetOpenSchedulesDetails(string scheduleSysDocID, string schedulevoucherID, string productIDStr, bool tochk);
	}
}
