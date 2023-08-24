using Micromind.Common.Data;
using Micromind.Common.Interfaces;
using Micromind.Data;
using System;
using System.Data;

namespace Micromind.Facade
{
	public sealed class PurchaseOrderSystem : MarshalByRefObject, IPurchaseOrderSystem, IDisposable
	{
		private Config config;

		public PurchaseOrderSystem(Config config)
		{
			this.config = config;
		}

		public void Dispose()
		{
		}

		public bool CreatePurchaseOrder(PurchaseOrderData data, bool isUpdate)
		{
			return new PurchaseOrder(config).InsertUpdatePurchaseOrder(data, isUpdate);
		}

		public PurchaseOrderData GetPurchaseOrderByID(string sysDocID, string voucherID)
		{
			return new PurchaseOrder(config).GetPurchaseOrderByID(sysDocID, voucherID);
		}

		public decimal GetPODueAmount(string sysDocID, string voucherID)
		{
			return new PurchaseOrder(config).GetPODueAmount(sysDocID, voucherID);
		}

		public bool DeletePurchaseOrder(string sysDocID, string voucherID)
		{
			return new PurchaseOrder(config).DeletePurchaseOrder(sysDocID, voucherID);
		}

		public bool VoidPurchaseOrder(string sysDocID, string voucherID, bool isVoid)
		{
			return new PurchaseOrder(config).VoidPurchaseOrder(sysDocID, voucherID, isVoid);
		}

		public DataSet GetOpenOrdersSummary(string vendorID, bool includeImport, bool includeLocal)
		{
			return new PurchaseOrder(config).GetOpenOrdersSummary(vendorID, includeImport, includeLocal);
		}

		public DataSet GetOpenOrdersSummary(string vendorID, bool includeImport, bool includeLocal, bool showAll)
		{
			return new PurchaseOrder(config).GetOpenOrdersSummary(vendorID, includeImport, includeLocal, showAll);
		}

		public DataSet GetPurchaseOrderAll()
		{
			return new PurchaseOrder(config).GetPurchaseOrderAll();
		}

		public DataSet GetPOsForPackingList(string vendorID, bool isImport)
		{
			return new PurchaseOrder(config).GetPOsForPackingList(vendorID, isImport);
		}

		public DataSet GetPOsItemsToShip(string sysDocID, string voucherID)
		{
			return new PurchaseOrder(config).GetPOsItemsToShip(sysDocID, voucherID);
		}

		public DataSet GetOpenOrderListReport()
		{
			return new PurchaseOrder(config).GetOpenOrderListReport();
		}

		public DataSet GetPurchaseOrderToPrint(string sysDocID, string[] voucherID)
		{
			return new PurchaseOrder(config).GetPurchaseOrderToPrint(sysDocID, voucherID);
		}

		public DataSet GetPurchaseOrderToPrint(string sysDocID, string voucherID)
		{
			return new PurchaseOrder(config).GetPurchaseOrderToPrint(sysDocID, new string[1]
			{
				voucherID
			});
		}

		public DataSet GetPurchaseOrderDetailReport(DateTime from, DateTime to, string jobID, string vendorID, string fromItem, string toItem, string fromClass, string toClass, string fromCategory, string toCategory, string fromBrand, string toBrand, string fromLocation, string toLocation, bool openOrders, string fromManufacturer, string toManufacturer, string fromStyle, string toStyle, string fromOrigin, string toOrigin)
		{
			return new PurchaseOrder(config).GetPurchaseOrderDetailReport(from, to, jobID, vendorID, fromItem, toItem, fromClass, toClass, fromCategory, toCategory, fromBrand, toBrand, fromLocation, toLocation, openOrders, fromManufacturer, toManufacturer, fromStyle, toStyle, fromOrigin, toOrigin);
		}

		public DataSet GetList(DateTime from, DateTime to, bool isImport, bool showVoid, string sysDocID)
		{
			return new PurchaseOrder(config).GetList(from, to, isImport, showVoid, sysDocID);
		}

		public bool CanUpdate(string sysDocID, string voucherNumber)
		{
			return new PurchaseOrder(config).CanUpdate(sysDocID, voucherNumber, null) == 0;
		}

		public bool SetOrderStatus(string sysDocID, string voucherID, PurchaseOrderStatus status)
		{
			return new PurchaseOrder(config).SetOrderStatus(sysDocID, voucherID, status);
		}

		public DataSet GetOpenPOComboData(string vendorID)
		{
			return new PurchaseOrder(config).GetOpenPOComboData(vendorID);
		}

		public DataSet GetPOPaymentSummary(string sysDocID, string voucherID, string RequestsysDocID, string RequestvoucherID)
		{
			return new PurchaseOrder(config).GetPOPaymentSummary(sysDocID, voucherID, RequestsysDocID, RequestvoucherID);
		}

		public DataSet GetPOListForPayment(string vendorID)
		{
			return new PurchaseOrder(config).GetPOListForPayment(vendorID);
		}

		public DataSet GetLastVendorDeliveryAddress(string vendorID)
		{
			return new PurchaseOrder(config).GetLastVendorDeliveryAddress(vendorID);
		}

		public ItemTypes GetItemType(string ItemCode)
		{
			return new PurchaseOrder(config).GetItemType(ItemCode);
		}

		public bool ValidateOrder(string ItemCode, string jobID, decimal OrderQty)
		{
			return new PurchaseOrder(config).ValidateOrder(ItemCode, jobID, OrderQty);
		}

		public bool AllowModify(string sysDocID, string voucherNumber)
		{
			return new PurchaseOrder(config).AllowModify(sysDocID, voucherNumber);
		}

		public DataSet GetOpenOrdersSummaryWithNonInv(string vendorID, bool includeImport, bool includeLocal, bool showAll)
		{
			return new PurchaseOrder(config).GetOpenOrdersSummaryWithNonInv(vendorID, includeImport, includeLocal, showAll);
		}
	}
}
