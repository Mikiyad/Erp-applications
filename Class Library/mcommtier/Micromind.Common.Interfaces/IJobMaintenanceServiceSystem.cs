using Micromind.Common.Data;
using System;
using System.Data;

namespace Micromind.Common.Interfaces
{
	public interface IJobMaintenanceServiceSystem
	{
		bool CreateJobMaintenanceService(JobMaintenanceServiceData materialEstimatetData, bool isUpdate);

		JobMaintenanceServiceData GetJobMaintenanceServiceByID(string sysDocID, string voucherID);

		DataSet GetJobMaintenanceServiceAll();

		DataSet GetJobMaintenanceServiceToPrint(string sysDocID, string[] voucherID);

		DataSet GetJobMaintenanceServiceToPrint(string sysDocID, string voucherID);

		bool DeleteJobMaintenanceService(string sysDocID, string voucherID);

		DataSet GetList(DateTime from, DateTime to, bool showVoid);
	}
}
