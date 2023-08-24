using Micromind.Common.Data;
using Micromind.Common.Interfaces;
using Micromind.Data;
using System;
using System.Data;

namespace Micromind.Facade
{
	public sealed class PurchaseCostEntrySystem : MarshalByRefObject, IPurchaseCostEntrySystem, IDisposable
	{
		private Config config;

		public PurchaseCostEntrySystem(Config config)
		{
			this.config = config;
		}

		public void Dispose()
		{
		}

		public bool CreatePurchaseCostEntry(PurchaseCostEntryData data, bool isUpdate)
		{
			return new PurchaseCostEntry(config).InsertUpdatePurchaseCostEntry(data, isUpdate);
		}

		public PurchaseCostEntryData GetPurchaseCostEntryByID(string sysDocID, string voucherID)
		{
			return new PurchaseCostEntry(config).GetPurchaseCostEntryByID(sysDocID, voucherID);
		}

		public bool DeletePurchaseCostEntry(string sysDocID, string voucherID)
		{
			return new PurchaseCostEntry(config).DeletePurchaseCostEntry(sysDocID, voucherID);
		}

		public bool VoidPurchaseCostEntry(string sysDocID, string voucherID, bool isVoid)
		{
			return new PurchaseCostEntry(config).VoidPurchaseCostEntry(sysDocID, voucherID, isVoid);
		}

		public DataSet GetOpenOrdersSummary(string vendorID, bool isImport)
		{
			return new PurchaseCostEntry(config).GetOpenOrdersSummary(vendorID, isImport);
		}

		public DataSet GetPurchaseCostEntryAll()
		{
			return new PurchaseCostEntry(config).GetPurchaseCostEntryAll();
		}

		public DataSet GetPOsForPackingList(bool isImport)
		{
			return new PurchaseCostEntry(config).GetPOsForPackingList(isImport);
		}

		public DataSet GetPOsItemsToShip()
		{
			return new PurchaseCostEntry(config).GetPOsItemsToShip();
		}

		public DataSet GetOpenOrderListReport()
		{
			return new PurchaseCostEntry(config).GetOpenOrderListReport();
		}

		public DataSet GetBOLListForPayment()
		{
			return new PurchaseCostEntry(config).GetBOLListForPayment();
		}

		public DataSet GetPurchaseCostEntryToPrint(string sysDocID, string[] voucherID)
		{
			return new PurchaseCostEntry(config).GetPurchaseCostEntryToPrint(sysDocID, voucherID);
		}

		public DataSet GetPurchaseCostEntryToPrint(string sysDocID, string voucherID)
		{
			return new PurchaseCostEntry(config).GetPurchaseCostEntryToPrint(sysDocID, new string[1]
			{
				voucherID
			});
		}

		public DataSet GetPurchaseCostEntryDetailReport(DateTime from, DateTime to, string jobID, string vendorID, string fromItem, string toItem, string fromClass, string toClass, string fromCategory, string toCategory)
		{
			return new PurchaseCostEntry(config).GetPurchaseCostEntryDetailReport(from, to, jobID, vendorID, fromItem, toItem, fromClass, toClass, fromCategory, toCategory);
		}

		public DataSet GetList(DateTime from, DateTime to, bool showVoid)
		{
			return new PurchaseCostEntry(config).GetList(from, to, showVoid);
		}

		public bool CanUpdate(string sysDocID, string voucherNumber)
		{
			return new PurchaseCostEntry(config).CanUpdate(sysDocID, voucherNumber, null);
		}

		public bool SetOrderStatus(string sysDocID, string voucherID, PurchaseOrderStatus status)
		{
			return new PurchaseCostEntry(config).SetOrderStatus(sysDocID, voucherID, status);
		}

		public DataSet GetOpenPOComboData(string vendorID)
		{
			return new PurchaseCostEntry(config).GetOpenPOComboData(vendorID);
		}

		public DataSet GetPOPaymentSummary(string sysDocID, string voucherID, string RequestsysDocID, string RequestvoucherID)
		{
			return new PurchaseCostEntry(config).GetPOPaymentSummary(sysDocID, voucherID, RequestsysDocID, RequestvoucherID);
		}

		public DataSet GetPOListForPayment(string vendorID)
		{
			return new PurchaseCostEntry(config).GetPOListForPayment(vendorID);
		}

		public DataSet GetCostEntryList()
		{
			return new PurchaseCostEntry(config).GetCostEntryList();
		}

		public PurchaseCostEntryData GetCostEntryByID(string sysDocID, string vendorID)
		{
			return new PurchaseCostEntry(config).GetCostEntryByID(sysDocID, vendorID);
		}

		public DataSet GetOpenExpenseDetails(string sysDocID, string vendorID, string ID, bool toChk, bool isupdate, string invoiceSysDocID, string invoicevoucherID)
		{
			return new PurchaseCostEntry(config).GetOpenExpenseDetails(sysDocID, vendorID, ID, toChk, isupdate, invoiceSysDocID, invoicevoucherID);
		}

		public DataSet GetCostEntryBOLList(string BOL)
		{
			return new PurchaseCostEntry(config).GetCostEntryBOLList(BOL);
		}

		public DataSet GetVendorExpenseList(string VendorID)
		{
			return new PurchaseCostEntry(config).GetVendorExpenseList(VendorID);
		}

		public bool IsInvoicedEntry(string sysDocID, string voucherNumber)
		{
			return new PurchaseCostEntry(config).IsInvoicedEntry(sysDocID, voucherNumber, null);
		}
	}
}
