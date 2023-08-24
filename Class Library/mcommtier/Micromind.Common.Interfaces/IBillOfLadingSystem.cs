using Micromind.Common.Data;
using System;
using System.Data;

namespace Micromind.Common.Interfaces
{
	public interface IBillOfLadingSystem
	{
		bool CreateBillOfLading(BillOfLadingData inventoryAdjustmentData, bool isUpdate);

		BillOfLadingData GetBillOfLadingByID(string sysDocID, string voucherID);

		bool DeleteBillOfLading(string sysDocID, string voucherID);

		bool VoidBillOfLading(string sysDocID, string voucherID, bool isVoid);

		DataSet GetOpenOrdersSummary(string vendorID, bool isImport);

		DataSet GetPOsForPackingList(bool isImport);

		DataSet GetPOsItemsToShip();

		DataSet GetBillOfLadingListReport();

		DataSet GetBillOfLadingToPrint(string sysDocID, string voucherID);

		DataSet GetBillOfLadingToPrint(string sysDocID, string[] voucherID);

		DataSet GetList(DateTime from, DateTime to, bool showVoid);

		DataSet GetBillOfLadingDetailReport(DateTime from, DateTime to, string jobID, string vendorID, string fromItem, string toItem, string fromClass, string toClass, string fromCategory, string toCategory);

		bool CanUpdate(string sysDocID, string voucherNumber);

		bool SetOrderStatus(string sysDocID, string voucherID, PurchaseOrderStatus status);

		DataSet GetOpenPOComboData(string vendorID);

		DataSet GetBillOfLadingAll();

		DataSet GetPOListForPayment(string vendorID);

		DataSet GetBOLListForPayment();

		DataSet GetCostEntryList();

		BillOfLadingData GetCostEntryByID(string sysDocID, string voucherID);

		DataSet GetOpenExpenseDetails(string sysDocID, string voucherID, string ID, bool toChk, bool isupdate, string invoiceSysDocID, string invoicevoucherID);

		DataSet GetCostEntryBOLList(string BOL);

		DataSet GetVendorExpenseList(string VendorID);

		DataSet GetBOLForPackingList(string VendorID);

		DataSet GetBOLList(string BOLNumber);
	}
}
