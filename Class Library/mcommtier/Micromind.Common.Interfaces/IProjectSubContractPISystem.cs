using Micromind.Common.Data;
using System;
using System.Data;

namespace Micromind.Common.Interfaces
{
	public interface IProjectSubContractPISystem
	{
		bool CreateProjectSubContractPI(ProjectSubContractPIData inventoryAdjustmentData, bool isUpdate);

		DataSet GetOpenOrdersSummary(string vendorID, bool isImport);

		ProjectSubContractPIData GetProjectSubContractPIByID(string sysDocID, string voucherID);

		bool DeleteProjectSubContractPI(string sysDocID, string voucherID);

		bool VoidProjectSubContractPI(string sysDocID, string voucherID, bool isVoid);

		DataSet GetInvoicesForDelivery(string customerID);

		DataSet GetProjectSubContractPIToPrint(string sysDocID, string voucherID);

		DataSet GetProjectSubContractPIToPrint(string sysDocID, string[] voucherID);

		DataSet GetPurchaseExpenseAllocationReport(DateTime fromDate, DateTime toDate, string fromItem, string toItem, string fromClass, string toClass, string fromCategory, string toCategory, string sysDocID, string voucherID);

		DataSet GetList(DateTime from, DateTime to, bool showVoid);

		DataSet GetProjectSubContractPIList(string sysDocID);

		DataSet GetPurchaseList(string sysDocID, DateTime from, DateTime to);
	}
}
