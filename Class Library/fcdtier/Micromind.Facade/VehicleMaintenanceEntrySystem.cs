using Micromind.Common.Data;
using Micromind.Common.Interfaces;
using Micromind.Data;
using System;
using System.Data;

namespace Micromind.Facade
{
	public sealed class VehicleMaintenanceEntrySystem : MarshalByRefObject, IVehicleMaintenanceEntrySystem, IDisposable
	{
		private Config config;

		public VehicleMaintenanceEntrySystem(Config config)
		{
			this.config = config;
		}

		public void Dispose()
		{
		}

		public bool CreateMaintenanceEntry(VehicleMaintenanceEntryData data, bool isUpdate)
		{
			return new MaintenanceEntry(config).InsertUpdateMaintenanceEntry(data, isUpdate);
		}

		public bool UpdateMaintenanceEntry(VehicleMaintenanceEntryData data, bool isUpdate)
		{
			return new MaintenanceEntry(config).InsertUpdateMaintenanceEntry(data, isUpdate);
		}

		public VehicleMaintenanceEntryData GetMaintenanceEntryByID(string sysDocID, string voucherID)
		{
			return new MaintenanceEntry(config).GetMaintenanceEntryByID(sysDocID, voucherID);
		}

		public DataSet GetPreviousOdometerValue(string VehicleID)
		{
			return new MaintenanceEntry(config).GetPreviousOdometerValue(VehicleID);
		}

		public VehicleMaintenanceEntryData GetMaintenanceScheduleBySourceID(string sysDocID, string voucherID)
		{
			return new MaintenanceEntry(config).GetMaintenanceScheduleBySourceID(sysDocID, voucherID);
		}

		public DataSet GetMaintenanceEntryToPrint(string sysDocID, string[] voucherID)
		{
			return new MaintenanceEntry(config).GetMaintenanceEntryToPrint(sysDocID, voucherID);
		}

		public DataSet GetMaintenanceEntryToPrint(string sysDocID, string voucherID)
		{
			return new MaintenanceEntry(config).GetMaintenanceEntryToPrint(sysDocID, new string[1]
			{
				voucherID
			});
		}

		public DataSet GetMaintenanceEntryReport(DateTime from, DateTime to, string fromvehicle, string tovehicle, string fromserviceitem, string toserviceiem)
		{
			return new MaintenanceEntry(config).GetMaintenanceEntryReport(from, to, fromvehicle, tovehicle, fromserviceitem, toserviceiem);
		}

		public DataSet GetMaintenanceEntrySummary(string vehicleID)
		{
			return new MaintenanceEntry(config).GetMaintenanceEntrySummary(vehicleID);
		}

		public bool DeleteMaintenanceEntry(string sysdocid, string voucherID)
		{
			return new MaintenanceEntry(config).DeleteMaintenanceEntry(sysdocid, voucherID);
		}

		public DataSet GetList(DateTime from, DateTime to, bool showVoid)
		{
			return new MaintenanceEntry(config).GetList(from, to, showVoid);
		}

		public bool VoidMaintenance(string sysDocID, string voucherID, bool isVoid)
		{
			return new MaintenanceEntry(config).VoidMaintenance(sysDocID, voucherID, isVoid);
		}
	}
}
