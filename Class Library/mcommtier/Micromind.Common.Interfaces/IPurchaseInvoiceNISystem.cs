using Micromind.Common.Data;
using System;
using System.Data;

namespace Micromind.Common.Interfaces
{
	public interface IPurchaseInvoiceNISystem
	{
		bool CreatePurchaseInvoice(PurchaseInvoiceNIData inventoryAdjustmentData, bool isUpdate);

		PurchaseInvoiceNIData GetPurchaseInvoiceByID(string sysDocID, string voucherID);

		bool DeletePurchaseInvoice(string sysDocID, string voucherID);

		bool VoidPurchaseInvoice(string sysDocID, string voucherID, bool isVoid);

		DataSet GetInvoicesForDelivery(string customerID);

		DataSet GetPurchaseInvoiceToPrint(string sysDocID, string voucherID);

		DataSet GetPurchaseInvoiceToPrint(string sysDocID, string[] voucherID);

		DataSet GetPurchaseExpenseAllocationReport(DateTime fromDate, DateTime toDate, string fromItem, string toItem, string fromClass, string toClass, string fromCategory, string toCategory, string sysDocID, string voucherID);

		DataSet GetList(DateTime from, DateTime to, bool showVoid);

		DataSet GetPurchaseInvoiceList(string sysDocID);

		DataSet GetPurchaseList(string sysDocID, DateTime from, DateTime to);

		bool IsAlreadyExisting(string sysDocID, string voucherID, string vendorID, string supplDocNo);
	}
}
