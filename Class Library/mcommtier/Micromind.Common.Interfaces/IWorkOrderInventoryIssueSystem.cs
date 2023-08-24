using Micromind.Common.Data;
using System;
using System.Data;

namespace Micromind.Common.Interfaces
{
	public interface IWorkOrderInventoryIssueSystem
	{
		bool CreateWorkOrderInventoryIssue(WorkOrderInventoryIssueData inventoryAdjustmentData, bool isUpdate);

		WorkOrderInventoryIssueData GetWorkOrderInventoryIssueByID(string sysDocID, string voucherID);

		DataSet GetWorkOrderInventoryIssueToPrint(string sysDocID, string[] voucherID);

		DataSet GetWorkOrderInventoryIssueToPrint(string sysDocID, string voucherID);

		bool DeleteWorkOrderInventoryIssue(string sysDocID, string voucherID);

		DataSet GetList(DateTime from, DateTime to, bool showVoid);
	}
}
