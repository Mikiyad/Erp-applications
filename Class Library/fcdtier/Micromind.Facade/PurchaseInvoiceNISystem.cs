using Micromind.Common.Data;
using Micromind.Common.Interfaces;
using Micromind.Data;
using System;
using System.Data;

namespace Micromind.Facade
{
	public sealed class PurchaseInvoiceNISystem : MarshalByRefObject, IPurchaseInvoiceNISystem, IDisposable
	{
		private Config config;

		public PurchaseInvoiceNISystem(Config config)
		{
			this.config = config;
		}

		public void Dispose()
		{
		}

		public bool CreatePurchaseInvoice(PurchaseInvoiceNIData data, bool isUpdate)
		{
			return new PurchaseInvoiceNI(config).InsertUpdatePurchaseInvoice(data, isUpdate);
		}

		public PurchaseInvoiceNIData GetPurchaseInvoiceByID(string sysDocID, string voucherID)
		{
			return new PurchaseInvoiceNI(config).GetPurchaseInvoiceByID(sysDocID, voucherID);
		}

		public bool DeletePurchaseInvoice(string sysDocID, string voucherID)
		{
			return new PurchaseInvoiceNI(config).DeletePurchaseInvoice(sysDocID, voucherID);
		}

		public bool VoidPurchaseInvoice(string sysDocID, string voucherID, bool isVoid)
		{
			return new PurchaseInvoiceNI(config).VoidPurchaseInvoice(sysDocID, voucherID, isVoid);
		}

		public DataSet GetInvoicesForDelivery(string customerID)
		{
			return new PurchaseInvoiceNI(config).GetInvoicesForDelivery(customerID);
		}

		public DataSet GetPurchaseInvoiceToPrint(string sysDocID, string voucherID)
		{
			return new PurchaseInvoiceNI(config).GetPurchaseInvoiceToPrint(sysDocID, voucherID);
		}

		public DataSet GetPurchaseInvoiceToPrint(string sysDocID, string[] voucherID)
		{
			return new PurchaseInvoiceNI(config).GetPurchaseInvoiceToPrint(sysDocID, voucherID);
		}

		public DataSet GetList(DateTime from, DateTime to, bool showVoid)
		{
			return new PurchaseInvoiceNI(config).GetList(from, to, showVoid);
		}

		public DataSet GetPurchaseExpenseAllocationReport(DateTime fromDate, DateTime toDate, string fromItem, string toItem, string fromClass, string toClass, string fromCategory, string toCategory, string sysDocID, string voucherID)
		{
			return new PurchaseInvoiceNI(config).GetPurchaseExpenseAllocationReport(fromDate, toDate, fromItem, toItem, fromClass, toClass, fromCategory, toCategory, sysDocID, voucherID);
		}

		public DataSet GetPurchaseInvoiceList(string sysDocID)
		{
			return new PurchaseInvoiceNI(config).GetPurchaseInvoiceList(sysDocID);
		}

		public DataSet GetPurchaseList(string sysDocID, DateTime fromDate, DateTime endDate)
		{
			return new PurchaseInvoiceNI(config).GetPurchaseList(sysDocID, fromDate, endDate);
		}

		public bool IsAlreadyExisting(string sysDocID, string voucherID, string vendorID, string supplDocNo)
		{
			return new PurchaseInvoiceNI(config).IsAlreadyExisting(sysDocID, voucherID, vendorID, supplDocNo);
		}
	}
}
