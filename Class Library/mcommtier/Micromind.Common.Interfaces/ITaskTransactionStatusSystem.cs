using Micromind.Common.Data;
using System;
using System.Data;

namespace Micromind.Common.Interfaces
{
	public interface ITaskTransactionStatusSystem
	{
		bool CreateTaskTransactionStatus(TaskTransactionStatusData TaskTransactionStatusData, bool isUpdate);

		TaskTransactionStatusData GetTaskTransactionStatusByID(string sysDocID, string voucherID);

		DataSet GetTaskTransactionStatus();

		DataSet GetTaskTransactionStatusToPrint(string sysDocID, string[] voucherID);

		DataSet GetTaskTransactionStatusToPrint(string sysDocID, string voucherID);

		bool DeleteTaskTransactionStatus(string sysDocID, string voucherID);

		DataSet GetTaskTransactionStatusList(string SysDocID);

		DataSet GetTaskTransactionStatusReport(DateTime from, DateTime to, string fromWarehouse, string toWarehouse, string fromItem, string toItem, string fromClass, string toClass, string fromCategory, string toCategory);

		DataSet GetList(DateTime from, DateTime to, bool showVoid);

		DataSet GetTaskTransactionStatusByEquipmentLocationProjectReport(DateTime fromDate, DateTime toDate, string fromEquipment, string toEquipment, string fromType, string toType, string fromCategory, string toCategory, string fromLocation, string toLocation, string fromJob, string toJob);

		bool UpdateTaskTransactionStatusStatus(string sysDocID, string voucherID);
	}
}
