using Micromind.Common.Data;
using System;
using System.Data;

namespace Micromind.Common.Interfaces
{
	public interface IPurchaseOrderNISystem
	{
		bool CreatePurchaseOrder(PurchaseOrderNIData inventoryAdjustmentData, bool isUpdate);

		PurchaseOrderNIData GetPurchaseOrderByID(string sysDocID, string voucherID);

		PurchaseOrderNIData GetPurchaseOrderServiceItemByID(string sysDocID, string voucherID);

		bool DeletePurchaseOrder(string sysDocID, string voucherID);

		bool VoidPurchaseOrder(string sysDocID, string voucherID, bool isVoid);

		DataSet GetOpenOrdersSummary(string vendorID, bool isImport);

		DataSet GetOpenOrderServiceItemSummary();

		DataSet GetPOsForPackingList(string vendorID, bool isImport);

		DataSet GetPOsItemsToShip(string sysDocID, string voucherID);

		DataSet GetOpenOrderListReport();

		DataSet GetPurchaseOrderToPrint(string sysDocID, string voucherID);

		DataSet GetPurchaseOrderToPrint(string sysDocID, string[] voucherID);

		DataSet GetList(DateTime from, DateTime to, bool isImport, bool showVoid);

		DataSet GetPurchaseOrderDetailReport(DateTime from, DateTime to, string jobID, string vendorID, string fromItem, string toItem, string fromClass, string toClass, string fromCategory, string toCategory);

		bool CanUpdate(string sysDocID, string voucherNumber);

		bool SetOrderStatus(string sysDocID, string voucherID, PurchaseOrderStatus status);

		DataSet GetOpenPOComboData(string vendorID);

		DataSet GetPOPaymentSummary(string sysDocID, string voucherID);

		DataSet GetPurchaseOrderAll();

		DataSet GetPOListForPayment(string vendorID);

		decimal GetPODueAmount(string sysDocID, string voucherID);
	}
}
