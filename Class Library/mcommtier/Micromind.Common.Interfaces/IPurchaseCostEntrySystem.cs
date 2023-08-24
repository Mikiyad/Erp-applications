using Micromind.Common.Data;
using System;
using System.Data;

namespace Micromind.Common.Interfaces
{
	public interface IPurchaseCostEntrySystem
	{
		bool CreatePurchaseCostEntry(PurchaseCostEntryData inventoryAdjustmentData, bool isUpdate);

		PurchaseCostEntryData GetPurchaseCostEntryByID(string sysDocID, string voucherID);

		bool DeletePurchaseCostEntry(string sysDocID, string voucherID);

		bool VoidPurchaseCostEntry(string sysDocID, string voucherID, bool isVoid);

		DataSet GetOpenOrdersSummary(string vendorID, bool isImport);

		DataSet GetPOsForPackingList(bool isImport);

		DataSet GetPOsItemsToShip();

		DataSet GetOpenOrderListReport();

		DataSet GetPurchaseCostEntryToPrint(string sysDocID, string voucherID);

		DataSet GetPurchaseCostEntryToPrint(string sysDocID, string[] voucherID);

		DataSet GetList(DateTime from, DateTime to, bool showVoid);

		DataSet GetPurchaseCostEntryDetailReport(DateTime from, DateTime to, string jobID, string vendorID, string fromItem, string toItem, string fromClass, string toClass, string fromCategory, string toCategory);

		bool CanUpdate(string sysDocID, string voucherNumber);

		bool SetOrderStatus(string sysDocID, string voucherID, PurchaseOrderStatus status);

		DataSet GetOpenPOComboData(string vendorID);

		DataSet GetPOPaymentSummary(string sysDocID, string voucherID, string RequestsysDocID, string RequestvoucherID);

		DataSet GetPurchaseCostEntryAll();

		DataSet GetPOListForPayment(string vendorID);

		DataSet GetBOLListForPayment();

		DataSet GetCostEntryList();

		PurchaseCostEntryData GetCostEntryByID(string sysDocID, string voucherID);

		DataSet GetOpenExpenseDetails(string sysDocID, string voucherID, string ID, bool toChk, bool isupdate, string invoiceSysDocID, string invoicevoucherID);

		DataSet GetCostEntryBOLList(string BOL);

		DataSet GetVendorExpenseList(string VendorID);

		bool IsInvoicedEntry(string sysDocID, string voucherNumber);
	}
}
