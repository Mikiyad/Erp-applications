using Micromind.Common.Data;
using System;
using System.Data;

namespace Micromind.Common.Interfaces
{
	public interface IWorkOrderInventoryReturnSystem
	{
		bool CreateWorkOrderInventoryReturn(WorkOrderInventoryReturnData inventoryAdjustmentData, bool isUpdate);

		WorkOrderInventoryReturnData GetWorkOrderInventoryReturnByID(string sysDocID, string voucherID);

		DataSet GetWorkOrderInventoryReturnToPrint(string sysDocID, string[] voucherID);

		DataSet GetWorkOrderInventoryReturnToPrint(string sysDocID, string voucherID);

		bool DeleteWorkOrderInventoryReturn(string sysDocID, string voucherID);

		DataSet GetList(DateTime from, DateTime to, bool showVoid);
	}
}
