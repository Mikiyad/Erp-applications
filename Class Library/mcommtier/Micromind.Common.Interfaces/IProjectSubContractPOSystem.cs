using Micromind.Common.Data;
using System;
using System.Data;

namespace Micromind.Common.Interfaces
{
	public interface IProjectSubContractPOSystem
	{
		bool CreateProjectSubContractPO(ProjectSubcontractPOData inventoryAdjustmentData, bool isUpdate);

		ProjectSubcontractPOData GetProjectSubContractPOByID(string sysDocID, string voucherID);

		ProjectSubcontractPOData GetProjectSubContractOrderByID(string sysDocID, string voucherID);

		bool DeleteProjectSubContractPO(string sysDocID, string voucherID);

		bool VoidProjectSubContractPO(string sysDocID, string voucherID, bool isVoid);

		DataSet GetOpenOrdersSummary(string vendorID, bool isImport);

		DataSet GetPOsForPackingList(string vendorID, bool isImport);

		DataSet GetPOsItemsToShip(string sysDocID, string voucherID);

		DataSet GetOpenOrderListReport();

		DataSet GetProjectSubContractPOToPrint(string sysDocID, string voucherID);

		DataSet GetProjectSubContractPOToPrint(string sysDocID, string[] voucherID);

		DataSet GetList(DateTime from, DateTime to, bool isImport, bool showVoid);

		DataSet GetProjectSubContractPODetailReport(DateTime from, DateTime to, string jobID, string vendorID, string fromItem, string toItem, string fromClass, string toClass, string fromCategory, string toCategory);

		bool CanUpdate(string sysDocID, string voucherNumber);

		bool SetOrderStatus(string sysDocID, string voucherID, PurchaseOrderStatus status);

		DataSet GetOpenPOComboData(string vendorID);

		DataSet GetPOPaymentSummary(string sysDocID, string voucherID);

		DataSet GetProjectSubContractPOAll();

		DataSet GetPOListForPayment(string vendorID);
	}
}
