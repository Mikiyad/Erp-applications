using Micromind.Common.Data;
using System;
using System.Data;

namespace Micromind.Common.Interfaces
{
	public interface IPurchaseReceiptSystem
	{
		bool CreatePurchaseReceipt(PurchaseReceiptData inventoryAdjustmentData, bool isUpdate);

		PurchaseReceiptData GetPurchaseReceiptByID(string sysDocID, string voucherID);

		bool DeletePurchaseReceipt(string sysDocID, string voucherID);

		bool VoidPurchaseReceipt(string sysDocID, string voucherID, bool isVoid);

		DataSet GetUninvoicedGRNs(string vendorID, bool isImport);

		DataSet GetUninvoicedGRNsOnLocation(string vendorID, bool isImport, string locationID);

		DataSet GetUninvoicedGRNsReport(DateTime from, DateTime to, string fromItem, string toItem, string fromItemClass, string toItemClass, string fromCategory, string toCategory, string fromBrand, string toBrand, string fromLocation, string toLocation, string fromVendor, string toVendor, string fromClass, string toClass, string fromGroup, string toGroup, string vendorIDs, string fromManufacturer, string toManufacturer, string fromStyle, string toStyle, string fromOrigin, string toOrigin);

		DataSet GetPurchaseReceiptToPrint(string sysDocID, string[] voucherID, bool showLotDetail, bool showOrderandShipmentinGRN);

		DataSet GetList(DateTime fromDate, DateTime toDate, bool showVoid, bool includeLocal, bool includeImport, string sysDocID);

		DataSet GetPurchaseReceiptToPrint(string sysDocID, string voucherID, bool showLotDetail, bool showOrderandShipmentinGRN);

		DataSet GetGRNListToQC(string sysDocID, DateTime fromDate, DateTime toDate, bool includeLocal);

		DataSet GetPurchaseReceiptAll();

		DataSet GetSalesByGRNSummaryReport(DateTime from, DateTime to, string fromCategory, string toCategory, string fromVendor, string toVendor, string fromClass, string toClass, string fromGroup, string toGroup, string sysDocID, string voucherID, string ContainerNo, string vendorIDs);

		DataSet GetGRNList(string sysDocID, DateTime fromDate, DateTime toDate);

		DataSet GetGRNList(string sysDocID, bool includeInvoiced);

		DataSet GetPurchaseClaimGRNList(string sysDocID);

		bool CanEdit(string sysDocID, string voucherID);

		DataSet GetItemMovementGRNReport(DateTime from, DateTime to, string fromItem, string toItem, string fromItemClass, string toItemClass, string fromCategory, string toCategory, string fromBrand, string toBrand, string fromVendor, string toVendor, string fromClass, string toClass, string fromGroup, string toGroup, string sysDocID, string voucherID, string fromManufacturer, string toManufacturer, string fromStyle, string toStyle, string fromOrigin, string toOrigin);

		DataSet GetBarCodeReport(DateTime from, DateTime to, string fromItem, string toItem, string fromItemClass, string toItemClass, string fromCategory, string toCategory, string fromBrand, string toBrand, string sysDocID, string voucherID, string tableName, string fromManufacturer, string toManufacturer, string fromStyle, string toStyle, string fromOrigin, string toOrigin);

		DataSet GetBarCodeReport(DataSet DS);

		DataSet GetSalesByGRNDetailReport(string sysDocID, string voucherID);

		DataSet GetVoucherNumbersFromTransaction(string tableName, string sysDocID, string voucherID, DateTime fromDate, DateTime toDate);

		DataSet GetGRNItemTaxDetails(DataTable dtItems);

		DataSet GetContainerData(string fromClass, string toClass, int status, DateTime fromDate, DateTime toDate);

		DataSet GetContainerLotData(string sysDocID, string voucherID);

		bool UpdateContainerDetails(PurchaseReceiptData purchaseReceiptData);

		bool ValidateProcessedDocumentEdit(string sysDocID, string voucherID, DateTime transactionDate);
	}
}
