using Micromind.Common.Data;
using Micromind.Common.Interfaces;
using Micromind.Data;
using System;
using System.Data;

namespace Micromind.Facade
{
	public sealed class TaskTransactionStatusSystem : MarshalByRefObject, ITaskTransactionStatusSystem, IDisposable
	{
		private Config config;

		public TaskTransactionStatusSystem(Config config)
		{
			this.config = config;
		}

		public void Dispose()
		{
		}

		public bool CreateTaskTransactionStatus(TaskTransactionStatusData data, bool isUpdate)
		{
			return new TaskTransactionStatus(config).InsertUpdateTaskTransactionStatus(data, isUpdate);
		}

		public TaskTransactionStatusData GetTaskTransactionStatusByID(string sysDocID, string voucherID)
		{
			return new TaskTransactionStatus(config).GetTaskTransactionStatusByID(sysDocID, voucherID);
		}

		public DataSet GetTaskTransactionStatus()
		{
			return new TaskTransactionStatus(config).GetTaskTransactionStatus();
		}

		public bool DeleteTaskTransactionStatus(string sysDocID, string voucherID)
		{
			return new TaskTransactionStatus(config).DeleteTaskTransactionStatus(sysDocID, voucherID);
		}

		public DataSet GetTaskTransactionStatusToPrint(string sysDocID, string[] voucherID)
		{
			return new TaskTransactionStatus(config).GetTaskTransactionStatusToPrint(sysDocID, voucherID);
		}

		public DataSet GetTaskTransactionStatusToPrint(string sysDocID, string voucherID)
		{
			return new TaskTransactionStatus(config).GetTaskTransactionStatusToPrint(sysDocID, new string[1]
			{
				voucherID
			});
		}

		public DataSet GetTaskTransactionStatusReport(DateTime from, DateTime to, string fromWarehouse, string toWarehouse, string fromItem, string toItem, string fromClass, string toClass, string fromCategory, string toCategory)
		{
			return new TaskTransactionStatus(config).GetTaskTransactionStatusReport(from, to, fromWarehouse, toWarehouse, fromItem, toItem, fromClass, toClass, fromCategory, toCategory);
		}

		public DataSet GetTaskTransactionStatusList(string sysDocID)
		{
			return new TaskTransactionStatus(config).GetTaskTransactionStatusList(sysDocID);
		}

		public DataSet GetList(DateTime from, DateTime to, bool showVoid)
		{
			return new TaskTransactionStatus(config).GetList(from, to, showVoid);
		}

		public DataSet GetTaskTransactionStatusByEquipmentLocationProjectReport(DateTime fromDate, DateTime toDate, string fromEquipment, string toEquipment, string fromType, string toType, string fromCategory, string toCategory, string fromLocation, string toLocation, string fromJob, string toJob)
		{
			return new TaskTransactionStatus(config).GetTaskTransactionStatusByEquipmentLocationProjectReport(fromDate, toDate, fromEquipment, toEquipment, fromType, toType, fromCategory, toCategory, fromLocation, toLocation, fromJob, toJob);
		}

		public bool UpdateTaskTransactionStatusStatus(string sysDocID, string voucherID)
		{
			return new TaskTransactionStatus(config).UpdateTaskTransactionStatusStatus(sysDocID, voucherID, null);
		}
	}
}
