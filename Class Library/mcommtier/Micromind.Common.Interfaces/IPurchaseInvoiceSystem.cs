using Micromind.Common.Data;
using System;
using System.Data;

namespace Micromind.Common.Interfaces
{
	public interface IPurchaseInvoiceSystem
	{
		bool CreatePurchaseInvoice(PurchaseInvoiceData inventoryAdjustmentData, bool isUpdate);

		PurchaseInvoiceData GetPurchaseInvoiceByID(string sysDocID, string voucherID);

		bool DeletePurchaseInvoice(string sysDocID, string voucherID);

		bool VoidPurchaseInvoice(string sysDocID, string voucherID, bool isVoid);

		bool HoldPurchaseInvoice(string sysDocID, string voucherID, bool isVoid);

		DataSet GetInvoicesForDelivery(string customerID);

		DataSet GetPurchaseInvoiceToPrint(string sysDocID, string voucherID);

		DataSet GetPurchaseInvoiceToPrint(string sysDocID, string[] voucherID);

		DataSet GetPurchaseExpenseAllocationReport(DateTime fromDate, DateTime toDate, string fromItem, string toItem, string fromClass, string toClass, string fromCategory, string toCategory, string fromBrand, string toBrand, string sysDocID, string voucherID, string fromManufacturer, string toManufacturer, string fromStyle, string toStyle, string fromOrigin, string toOrigin);

		DataSet GetList(DateTime from, DateTime to, bool showVoid, string sysDocID);

		DataSet GetPurchaseInvoiceList(string sysDocID);

		DataSet GetPurchaseList(string sysDocID, DateTime from, DateTime to);

		DataSet GetPaymentAllocationDetails(string sysDocID, string voucherID);

		bool IsAlreadyExisting(string sysDocID, string voucherID, string vendorID, string vendorRefNo);

		DataSet GetPaymentAadviceDetails(string sysDocID, string voucherID);

		DataSet FindPrePaymentSourceDetails(string sysDocID, string voucherID, bool isInvoiced);
	}
}
