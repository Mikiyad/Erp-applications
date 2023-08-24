using Micromind.Common.Data;
using System;
using System.Data;

namespace Micromind.Common.Interfaces
{
	public interface IPurchaseOrderSystem
	{
		bool CreatePurchaseOrder(PurchaseOrderData inventoryAdjustmentData, bool isUpdate);

		PurchaseOrderData GetPurchaseOrderByID(string sysDocID, string voucherID);

		bool DeletePurchaseOrder(string sysDocID, string voucherID);

		bool VoidPurchaseOrder(string sysDocID, string voucherID, bool isVoid);

		DataSet GetOpenOrdersSummary(string vendorID, bool includeImport, bool includeLocal);

		DataSet GetOpenOrdersSummary(string vendorID, bool includeImport, bool includeLocal, bool showAll);

		DataSet GetPOsForPackingList(string vendorID, bool isImport);

		DataSet GetPOsItemsToShip(string sysDocID, string voucherID);

		DataSet GetOpenOrderListReport();

		decimal GetPODueAmount(string sysDocID, string voucherID);

		DataSet GetPurchaseOrderToPrint(string sysDocID, string voucherID);

		DataSet GetPurchaseOrderToPrint(string sysDocID, string[] voucherID);

		DataSet GetList(DateTime from, DateTime to, bool isImport, bool showVoid, string sysDocID);

		DataSet GetPurchaseOrderDetailReport(DateTime from, DateTime to, string jobID, string vendorID, string fromItem, string toItem, string fromClass, string toClass, string fromCategory, string toCategory, string fromBrand, string toBrand, string fromLocation, string toLocation, bool openOrders, string fromManufacturer, string toManufacturer, string fromStyle, string toStyle, string fromOrigin, string toOrigin);

		bool CanUpdate(string sysDocID, string voucherNumber);

		bool SetOrderStatus(string sysDocID, string voucherID, PurchaseOrderStatus status);

		DataSet GetOpenPOComboData(string vendorID);

		DataSet GetPOPaymentSummary(string sysDocID, string voucherID, string RequestsysDocID, string RequestvoucherID);

		DataSet GetPurchaseOrderAll();

		DataSet GetPOListForPayment(string vendorID);

		DataSet GetLastVendorDeliveryAddress(string vendorID);

		ItemTypes GetItemType(string ItemCode);

		bool ValidateOrder(string ItemCode, string jobID, decimal OrderQty);

		bool AllowModify(string sysDocID, string voucherNumber);

		DataSet GetOpenOrdersSummaryWithNonInv(string vendorID, bool includeImport, bool includeLocal, bool showAll);
	}
}
