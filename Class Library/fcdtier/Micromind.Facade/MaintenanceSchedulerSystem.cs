using Micromind.Common.Data;
using Micromind.Common.Interfaces;
using Micromind.Data;
using System;
using System.Data;

namespace Micromind.Facade
{
	public sealed class MaintenanceSchedulerSystem : MarshalByRefObject, IMaintenanceSchedulerSystem, IDisposable
	{
		private Config config;

		public MaintenanceSchedulerSystem(Config config)
		{
			this.config = config;
		}

		public void Dispose()
		{
		}

		public bool CreateMaintenanceScheduler(MaintenanceSchedulerData data, bool isUpdate)
		{
			return new MaintenanceScheduler(config).InsertUpdateMaintenanceScheduler(data, isUpdate);
		}

		public bool UpdateMaintenanceScheduler(MaintenanceSchedulerData data, bool isUpdate)
		{
			return new MaintenanceScheduler(config).InsertUpdateMaintenanceScheduler(data, isUpdate);
		}

		public MaintenanceSchedulerData GetMaintenanceSchedulerByID(string sysDocID, string voucherID)
		{
			return new MaintenanceScheduler(config).GetMaintenanceSchedulerByID(sysDocID, voucherID);
		}

		public DataSet GetMaintenanceSchedulerToPrint(string sysDocID, string[] voucherID)
		{
			return new MaintenanceScheduler(config).GetMaintenanceSchedulerToPrint(sysDocID, voucherID);
		}

		public DataSet GetMaintenanceSchedulerToPrint(string sysDocID, string voucherID)
		{
			return new MaintenanceScheduler(config).GetMaintenanceSchedulerToPrint(sysDocID, new string[1]
			{
				voucherID
			});
		}

		public DataSet GetMaintenanceSchedulerReport(DateTime FromDate, DateTime ToDate, string FromVehicle, string ToVehicle)
		{
			return new MaintenanceScheduler(config).GetMaintenanceSchedulerReport(FromDate, ToDate, FromVehicle, ToVehicle);
		}

		public DataSet GetMaintenanceSchedulerSummary(string vehicleID)
		{
			return new MaintenanceScheduler(config).GetMaintenanceSchedulerSummary(vehicleID);
		}

		public bool DeleteMaintenanceScheduler(string voucherID)
		{
			return new MaintenanceScheduler(config).DeleteMaintenanceScheduler(voucherID);
		}

		public DataSet GetList(DateTime from, DateTime to, bool showVoid)
		{
			return new MaintenanceScheduler(config).GetList(from, to, showVoid);
		}

		public bool VoidMaintenance(string sysDocID, string voucherID, bool isVoid)
		{
			return new MaintenanceScheduler(config).VoidMaintenance(sysDocID, voucherID, isVoid);
		}

		public bool UpdateMaintenaceStatus(string sysDocID, string voucherID, int status)
		{
			return new MaintenanceScheduler(config).UpdateMaintenanceStatus(sysDocID, voucherID, status);
		}
	}
}
