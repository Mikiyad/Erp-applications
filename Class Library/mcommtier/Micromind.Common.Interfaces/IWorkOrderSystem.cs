using Micromind.Common.Data;
using System;
using System.Data;

namespace Micromind.Common.Interfaces
{
	public interface IWorkOrderSystem
	{
		bool CreateWorkOrder(WorkOrderData assemblyBuildData, bool isUpdate);

		WorkOrderData GetWorkOrderByID(string sysDocID, string voucherID);

		DataSet GetWorkOrderToPrint(string sysDocID, string[] voucherID);

		DataSet GetWorkOrderToPrint(string sysDocID, string voucherID);

		bool DeleteWorkOrder(string sysDocID, string voucherID);

		DataSet GetWorkOrderAll();

		DataSet GetDetails(string tableName, string DocID, string Value);

		DataSet GetReference(string Table, string ColumnName, string SysdocId, string VoucherNo);

		DataSet GetList(DateTime fromDate, DateTime toDate, bool Isvoid);
	}
}
