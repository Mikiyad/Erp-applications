using Micromind.Common.Data;
using System;
using System.Data;

namespace Micromind.Common.Interfaces
{
	public interface IPurchasePrepaymentInvoiceSystem
	{
		bool CreatePurchasePrepaymentInvoice(PurchasePrepaymentInvoiceData inventoryAdjustmentData, bool isUpdate);

		PurchasePrepaymentInvoiceData GetPurchasePrepaymentInvoiceByID(string sysDocID, string voucherID);

		bool VoidPurchasePrepaymentInvoice(string sysDocID, string voucherID, bool isVoid);

		string HasPendingPrepayments(DataSet data);

		bool DeletePurchasePrepaymentInvoice(string sysDocID, string voucherID);

		bool ClosePrepaymentInvoice(DataSet data);

		DataSet GetUnallocatedPurchasePrePayments(bool includeApplied);

		DataSet GetInvoicesToAllocate(string prepaymentSysDocID, string prepaymentVoucherID);

		DataSet GetPurchasePrepaymentInvoiceToPrint(string sysDocID, string voucherID);

		DataSet GetPurchasePrepaymentInvoiceToPrint(string sysDocID, string[] voucherID);

		DataSet GetList(DateTime from, DateTime to, bool showVoid);

		bool OrderHasShippedQuantity(string sysDocID, string voucherNumber);

		DataSet GetPurchasePrepaymentInvoiceToPrintTR(string sysDocID, string voucherID);

		DataSet GetOpenPurchasePrepaymentInvoices(string vendorID);

		DataSet GetInvoicePrepayments(string invoiceSysDocID, string invoiceVoucherID);
	}
}
