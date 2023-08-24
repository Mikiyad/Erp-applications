using Micromind.Common.Data;
using System;
using System.Data;

namespace Micromind.Common.Interfaces
{
	public interface IMaintenanceSchedulerSystem
	{
		bool CreateMaintenanceScheduler(MaintenanceSchedulerData expenseAdjustmentData, bool isUpdate);

		bool UpdateMaintenanceScheduler(MaintenanceSchedulerData expenseAdjustmentData, bool isUpdate);

		MaintenanceSchedulerData GetMaintenanceSchedulerByID(string sysDocID, string voucherID);

		DataSet GetMaintenanceSchedulerToPrint(string sysDocID, string[] voucherID);

		DataSet GetMaintenanceSchedulerToPrint(string sysDocID, string voucherID);

		DataSet GetMaintenanceSchedulerSummary(string vehicleID);

		bool VoidMaintenance(string sysDocID, string voucherID, bool isVoid);

		bool UpdateMaintenaceStatus(string sysDocID, string voucherID, int status);

		bool DeleteMaintenanceScheduler(string voucherID);

		DataSet GetMaintenanceSchedulerReport(DateTime fromDate, DateTime ToDate, string FromVehicle, string ToVehicle);

		DataSet GetList(DateTime from, DateTime to, bool showVoid);
	}
}
