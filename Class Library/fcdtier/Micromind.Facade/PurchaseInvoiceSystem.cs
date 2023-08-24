using Micromind.Common.Data;
using Micromind.Common.Interfaces;
using Micromind.Data;
using System;
using System.Data;

namespace Micromind.Facade
{
	public sealed class PurchaseInvoiceSystem : MarshalByRefObject, IPurchaseInvoiceSystem, IDisposable
	{
		private Config config;

		public PurchaseInvoiceSystem(Config config)
		{
			this.config = config;
		}

		public void Dispose()
		{
		}

		public bool CreatePurchaseInvoice(PurchaseInvoiceData data, bool isUpdate)
		{
			return new PurchaseInvoice(config).InsertUpdatePurchaseInvoice(data, isUpdate);
		}

		public PurchaseInvoiceData GetPurchaseInvoiceByID(string sysDocID, string voucherID)
		{
			return new PurchaseInvoice(config).GetPurchaseInvoiceByID(sysDocID, voucherID);
		}

		public bool DeletePurchaseInvoice(string sysDocID, string voucherID)
		{
			return new PurchaseInvoice(config).DeletePurchaseInvoice(sysDocID, voucherID);
		}

		public bool VoidPurchaseInvoice(string sysDocID, string voucherID, bool isVoid)
		{
			return new PurchaseInvoice(config).VoidPurchaseInvoice(sysDocID, voucherID, isVoid);
		}

		public bool HoldPurchaseInvoice(string sysDocID, string voucherID, bool isVoid)
		{
			return new PurchaseInvoice(config).HoldPurchaseInvoice(sysDocID, voucherID, isVoid);
		}

		public DataSet GetInvoicesForDelivery(string customerID)
		{
			return new PurchaseInvoice(config).GetInvoicesForDelivery(customerID);
		}

		public DataSet GetPurchaseInvoiceToPrint(string sysDocID, string voucherID)
		{
			return new PurchaseInvoice(config).GetPurchaseInvoiceToPrint(sysDocID, voucherID);
		}

		public DataSet GetPurchaseInvoiceToPrint(string sysDocID, string[] voucherID)
		{
			return new PurchaseInvoice(config).GetPurchaseInvoiceToPrint(sysDocID, voucherID);
		}

		public DataSet GetList(DateTime from, DateTime to, bool showVoid, string sysDocID)
		{
			return new PurchaseInvoice(config).GetList(from, to, showVoid, sysDocID);
		}

		public DataSet GetPurchaseExpenseAllocationReport(DateTime fromDate, DateTime toDate, string fromItem, string toItem, string fromClass, string toClass, string fromCategory, string toCategory, string fromBrand, string toBrand, string sysDocID, string voucherID, string fromManufacturer, string toManufacturer, string fromStyle, string toStyle, string fromOrigin, string toOrigin)
		{
			return new PurchaseInvoice(config).GetPurchaseExpenseAllocationReport(fromDate, toDate, fromItem, toItem, fromClass, toClass, fromCategory, toCategory, fromBrand, toBrand, sysDocID, voucherID, fromManufacturer, toManufacturer, fromStyle, toStyle, fromOrigin, toOrigin);
		}

		public DataSet GetPurchaseInvoiceList(string sysDocID)
		{
			return new PurchaseInvoice(config).GetPurchaseInvoiceList(sysDocID);
		}

		public DataSet GetPurchaseList(string sysDocID, DateTime fromDate, DateTime endDate)
		{
			return new PurchaseInvoice(config).GetPurchaseList(sysDocID, fromDate, endDate);
		}

		public DataSet GetPaymentAllocationDetails(string sysDocID, string voucherID)
		{
			return new PurchaseInvoice(config).GetPaymentAllocationDetails(sysDocID, voucherID);
		}

		public bool IsAlreadyExisting(string sysDocID, string voucherID, string vendorID, string supplDocNo)
		{
			return new PurchaseInvoice(config).IsAlreadyExisting(sysDocID, voucherID, vendorID, supplDocNo);
		}

		public DataSet GetPaymentAadviceDetails(string sysDocID, string voucherID)
		{
			return new PurchaseInvoice(config).GetPaymentAadviceDetails(sysDocID, voucherID);
		}

		public DataSet FindPrePaymentSourceDetails(string sysDocID, string voucherID, bool isInvoiced)
		{
			return new PurchaseInvoice(config).FindPrePaymentSourceDetails(sysDocID, voucherID, isInvoiced);
		}
	}
}
