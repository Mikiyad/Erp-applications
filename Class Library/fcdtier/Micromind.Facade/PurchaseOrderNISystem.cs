using Micromind.Common.Data;
using Micromind.Common.Interfaces;
using Micromind.Data;
using System;
using System.Data;

namespace Micromind.Facade
{
	public sealed class PurchaseOrderNISystem : MarshalByRefObject, IPurchaseOrderNISystem, IDisposable
	{
		private Config config;

		public PurchaseOrderNISystem(Config config)
		{
			this.config = config;
		}

		public void Dispose()
		{
		}

		public bool CreatePurchaseOrder(PurchaseOrderNIData data, bool isUpdate)
		{
			return new PurchaseOrderNI(config).InsertUpdatePurchaseOrder(data, isUpdate);
		}

		public PurchaseOrderNIData GetPurchaseOrderByID(string sysDocID, string voucherID)
		{
			return new PurchaseOrderNI(config).GetPurchaseOrderByID(sysDocID, voucherID);
		}

		public PurchaseOrderNIData GetPurchaseOrderServiceItemByID(string sysDocID, string voucherID)
		{
			return new PurchaseOrderNI(config).GetPurchaseOrderServiceItemByID(sysDocID, voucherID);
		}

		public bool DeletePurchaseOrder(string sysDocID, string voucherID)
		{
			return new PurchaseOrderNI(config).DeletePurchaseOrder(sysDocID, voucherID);
		}

		public bool VoidPurchaseOrder(string sysDocID, string voucherID, bool isVoid)
		{
			return new PurchaseOrderNI(config).VoidPurchaseOrder(sysDocID, voucherID, isVoid);
		}

		public DataSet GetOpenOrdersSummary(string vendorID, bool isImport)
		{
			return new PurchaseOrderNI(config).GetOpenOrdersSummary(vendorID, isImport);
		}

		public DataSet GetOpenOrderServiceItemSummary()
		{
			return new PurchaseOrderNI(config).GetOpenOrderServiceItemSummary();
		}

		public DataSet GetPurchaseOrderAll()
		{
			return new PurchaseOrderNI(config).GetPurchaseOrderAll();
		}

		public DataSet GetPOsForPackingList(string vendorID, bool isImport)
		{
			return new PurchaseOrderNI(config).GetPOsForPackingList(vendorID, isImport);
		}

		public DataSet GetPOsItemsToShip(string sysDocID, string voucherID)
		{
			return new PurchaseOrderNI(config).GetPOsItemsToShip(sysDocID, voucherID);
		}

		public DataSet GetOpenOrderListReport()
		{
			return new PurchaseOrderNI(config).GetOpenOrderListReport();
		}

		public DataSet GetPurchaseOrderToPrint(string sysDocID, string[] voucherID)
		{
			return new PurchaseOrderNI(config).GetPurchaseOrderToPrint(sysDocID, voucherID);
		}

		public DataSet GetPurchaseOrderToPrint(string sysDocID, string voucherID)
		{
			return new PurchaseOrderNI(config).GetPurchaseOrderToPrint(sysDocID, new string[1]
			{
				voucherID
			});
		}

		public DataSet GetPurchaseOrderDetailReport(DateTime from, DateTime to, string jobID, string vendorID, string fromItem, string toItem, string fromClass, string toClass, string fromCategory, string toCategory)
		{
			return new PurchaseOrderNI(config).GetPurchaseOrderDetailReport(from, to, jobID, vendorID, fromItem, toItem, fromClass, toClass, fromCategory, toCategory);
		}

		public DataSet GetList(DateTime from, DateTime to, bool isImport, bool showVoid)
		{
			return new PurchaseOrderNI(config).GetList(from, to, isImport, showVoid);
		}

		public bool CanUpdate(string sysDocID, string voucherNumber)
		{
			return new PurchaseOrderNI(config).CanUpdate(sysDocID, voucherNumber, null);
		}

		public bool SetOrderStatus(string sysDocID, string voucherID, PurchaseOrderStatus status)
		{
			return new PurchaseOrderNI(config).SetOrderStatus(sysDocID, voucherID, status);
		}

		public DataSet GetOpenPOComboData(string vendorID)
		{
			return new PurchaseOrderNI(config).GetOpenPOComboData(vendorID);
		}

		public DataSet GetPOPaymentSummary(string sysDocID, string voucherID)
		{
			return new PurchaseOrderNI(config).GetPOPaymentSummary(sysDocID, voucherID);
		}

		public DataSet GetPOListForPayment(string vendorID)
		{
			return new PurchaseOrderNI(config).GetPOListForPayment(vendorID);
		}

		public decimal GetPODueAmount(string sysDocID, string voucherID)
		{
			return new PurchaseOrderNI(config).GetPODueAmount(sysDocID, voucherID);
		}
	}
}
