using Micromind.Common.Data;
using Micromind.Common.Interfaces;
using Micromind.Data;
using System;
using System.Data;

namespace Micromind.Facade
{
	public sealed class TaskTransactionSystem : MarshalByRefObject, ITaskTransactionSystem, IDisposable
	{
		private Config config;

		public TaskTransactionSystem(Config config)
		{
			this.config = config;
		}

		public void Dispose()
		{
		}

		public bool CreateTaskTransaction(TaskTransactionData data, bool isUpdate)
		{
			return new TaskTransaction(config).InsertUpdateTaskTransaction(data, isUpdate);
		}

		public bool UpdateTaskTransaction(TaskTransactionData data, bool isUpdate)
		{
			return new TaskTransaction(config).InsertUpdateTaskTransaction(data, isUpdate);
		}

		public TaskTransactionData GetTaskTransactionByID(string sysDocID, string voucherID)
		{
			return new TaskTransaction(config).GetTaskTransactionByID(sysDocID, voucherID);
		}

		public DataSet GetTaskTransactionToPrint(string sysDocID, string[] voucherID)
		{
			return new TaskTransaction(config).GetTaskTransactionToPrint(sysDocID, voucherID);
		}

		public DataSet GetTaskTransactionToPrint(string sysDocID, string voucherID)
		{
			return new TaskTransaction(config).GetTaskTransactionToPrint(sysDocID, new string[1]
			{
				voucherID
			});
		}

		public DataSet GetTaskTransactionReport(DateTime FromDate, DateTime ToDate, string FromVehicle, string ToVehicle)
		{
			return new TaskTransaction(config).GetTaskTransactionReport(FromDate, ToDate, FromVehicle, ToVehicle);
		}

		public DataSet GetTaskTransactionSummary(string vehicleID)
		{
			return new TaskTransaction(config).GetTaskTransactionSummary(vehicleID);
		}

		public bool DeleteTaskTransaction(string sysDocID, string voucherID)
		{
			return new TaskTransaction(config).DeleteTaskTransaction(sysDocID, voucherID);
		}

		public DataSet GetList(DateTime from, DateTime to, bool showVoid)
		{
			return new TaskTransaction(config).GetList(from, to, showVoid);
		}

		public bool VoidMaintenance(string sysDocID, string voucherID, bool isVoid)
		{
			return new TaskTransaction(config).VoidMaintenance(sysDocID, voucherID, isVoid);
		}

		public bool UpdateMaintenaceStatus(string sysDocID, string voucherID, int status)
		{
			return new TaskTransaction(config).UpdateMaintenanceStatus(sysDocID, voucherID, status);
		}

		public bool SetAssignee(string sysDocID, string voucherID, string Assignee)
		{
			return new TaskTransaction(config).SetAssignee(sysDocID, voucherID, Assignee);
		}

		public bool SetTaskStep(string sysDocID, string voucherID, string Step)
		{
			return new TaskTransaction(config).SetTaskStep(sysDocID, voucherID, Step);
		}
	}
}
