using Micromind.Common.Data;
using System;
using System.Data;

namespace Micromind.Common.Interfaces
{
	public interface ITaskTransactionSystem
	{
		bool CreateTaskTransaction(TaskTransactionData expenseAdjustmentData, bool isUpdate);

		bool UpdateTaskTransaction(TaskTransactionData expenseAdjustmentData, bool isUpdate);

		TaskTransactionData GetTaskTransactionByID(string sysDocID, string voucherID);

		DataSet GetTaskTransactionToPrint(string sysDocID, string[] voucherID);

		DataSet GetTaskTransactionToPrint(string sysDocID, string voucherID);

		DataSet GetTaskTransactionSummary(string vehicleID);

		bool VoidMaintenance(string sysDocID, string voucherID, bool isVoid);

		bool UpdateMaintenaceStatus(string sysDocID, string voucherID, int status);

		bool DeleteTaskTransaction(string sysDocID, string voucherID);

		DataSet GetTaskTransactionReport(DateTime fromDate, DateTime ToDate, string FromVehicle, string ToVehicle);

		DataSet GetList(DateTime from, DateTime to, bool showVoid);

		bool SetAssignee(string sysDocID, string voucherID, string Assignee);

		bool SetTaskStep(string sysDocID, string voucherID, string step);
	}
}
