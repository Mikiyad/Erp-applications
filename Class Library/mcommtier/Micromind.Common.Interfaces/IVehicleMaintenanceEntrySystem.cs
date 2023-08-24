using Micromind.Common.Data;
using System;
using System.Data;

namespace Micromind.Common.Interfaces
{
	public interface IVehicleMaintenanceEntrySystem
	{
		bool CreateMaintenanceEntry(VehicleMaintenanceEntryData expenseAdjustmentData, bool isUpdate);

		bool UpdateMaintenanceEntry(VehicleMaintenanceEntryData expenseAdjustmentData, bool isUpdate);

		VehicleMaintenanceEntryData GetMaintenanceEntryByID(string sysDocID, string voucherID);

		VehicleMaintenanceEntryData GetMaintenanceScheduleBySourceID(string sysDocID, string voucherID);

		DataSet GetMaintenanceEntryToPrint(string sysDocID, string[] voucherID);

		DataSet GetMaintenanceEntryToPrint(string sysDocID, string voucherID);

		DataSet GetMaintenanceEntrySummary(string vehicleID);

		bool VoidMaintenance(string sysDocID, string voucherID, bool isVoid);

		bool DeleteMaintenanceEntry(string sysDocID, string voucherID);

		DataSet GetPreviousOdometerValue(string VehicleID);

		DataSet GetMaintenanceEntryReport(DateTime from, DateTime to, string fromvehicle, string tovehicle, string fromserviceitem, string toserviceitem);

		DataSet GetList(DateTime from, DateTime to, bool showVoid);
	}
}
