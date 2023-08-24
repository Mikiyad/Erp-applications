using Micromind.Common.Data;
using Micromind.Common.Interfaces;
using Micromind.Data;
using System;
using System.Data;

namespace Micromind.Facade
{
	public sealed class PurchasePrepaymentInvoiceSystem : MarshalByRefObject, IPurchasePrepaymentInvoiceSystem, IDisposable
	{
		private Config config;

		public PurchasePrepaymentInvoiceSystem(Config config)
		{
			this.config = config;
		}

		public void Dispose()
		{
		}

		public bool DeletePurchasePrepaymentInvoice(string sysDocID, string voucherID)
		{
			return new PurchasePrepaymentInvoice(config).DeletePurchasePrepaymentInvoice(sysDocID, voucherID);
		}

		public bool VoidPurchasePrepaymentInvoice(string sysDocID, string voucherID, bool isVoid)
		{
			return new PurchasePrepaymentInvoice(config).VoidPurchasePrepaymentInvoice(sysDocID, voucherID, isVoid);
		}

		public string HasPendingPrepayments(DataSet data)
		{
			return new PurchasePrepaymentInvoice(config).HasPendingPrepayments(data);
		}

		public bool ClosePrepaymentInvoice(DataSet data)
		{
			return new PurchasePrepaymentInvoice(config).ClosePrepaymentInvoice(data);
		}

		public DataSet GetUnallocatedPurchasePrePayments(bool includeApplied)
		{
			return new PurchasePrepaymentInvoice(config).GetUnallocatedPurchasePrePayments(includeApplied);
		}

		public bool CreatePurchasePrepaymentInvoice(PurchasePrepaymentInvoiceData data, bool isUpdate)
		{
			return new PurchasePrepaymentInvoice(config).InsertUpdatePurchasePrepaymentInvoice(data, isUpdate);
		}

		public PurchasePrepaymentInvoiceData GetPurchasePrepaymentInvoiceByID(string sysDocID, string voucherID)
		{
			return new PurchasePrepaymentInvoice(config).GetPurchasePrepaymentInvoiceByID(sysDocID, voucherID);
		}

		public DataSet GetInvoicePrepayments(string invoiceSysDocID, string invoiceVoucherID)
		{
			return new PurchasePrepaymentInvoice(config).GetInvoicePrepayments(invoiceSysDocID, invoiceVoucherID);
		}

		public DataSet GetInvoicesToAllocate(string prepaymentSysDocID, string prepaymentVoucherID)
		{
			return new PurchasePrepaymentInvoice(config).GetInvoicesToAllocate(prepaymentSysDocID, prepaymentVoucherID);
		}

		public DataSet GetPurchasePrepaymentInvoiceToPrint(string sysDocID, string voucherID)
		{
			return new PurchasePrepaymentInvoice(config).GetPurchasePrepaymentInvoiceToPrint(sysDocID, voucherID);
		}

		public DataSet GetPurchasePrepaymentInvoiceToPrint(string sysDocID, string[] voucherID)
		{
			return new PurchasePrepaymentInvoice(config).GetPurchasePrepaymentInvoiceToPrint(sysDocID, voucherID);
		}

		public DataSet GetList(DateTime from, DateTime to, bool showVoid)
		{
			return new PurchasePrepaymentInvoice(config).GetList(from, to, showVoid);
		}

		public bool OrderHasShippedQuantity(string sysDocID, string voucherNumber)
		{
			return new PurchasePrepaymentInvoice(config).OrderHasShippedQuantity(sysDocID, voucherNumber, null);
		}

		public DataSet GetPurchasePrepaymentInvoiceToPrintTR(string sysDocID, string voucherID)
		{
			return new PurchasePrepaymentInvoice(config).GetPurchasePrepaymentInvoiceToPrintTR(sysDocID, voucherID);
		}

		public DataSet GetOpenPurchasePrepaymentInvoices(string vendorID)
		{
			return new PurchasePrepaymentInvoice(config).GetOpenPurchasePrepaymentInvoices(vendorID);
		}
	}
}
