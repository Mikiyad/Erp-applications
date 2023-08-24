using Micromind.Common.Data;
using Micromind.Common.Interfaces;
using Micromind.Data;
using System;
using System.Data;

namespace Micromind.Facade
{
	public sealed class PurchaseReceiptSystem : MarshalByRefObject, IPurchaseReceiptSystem, IDisposable
	{
		private Config config;

		public PurchaseReceiptSystem(Config config)
		{
			this.config = config;
		}

		public void Dispose()
		{
		}

		public bool CreatePurchaseReceipt(PurchaseReceiptData data, bool isUpdate)
		{
			return new PurchaseReceipt(config).InsertUpdatePurchaseReceipt(data, isUpdate);
		}

		public PurchaseReceiptData GetPurchaseReceiptByID(string sysDocID, string voucherID)
		{
			return new PurchaseReceipt(config).GetPurchaseReceiptByID(sysDocID, voucherID);
		}

		public bool DeletePurchaseReceipt(string sysDocID, string voucherID)
		{
			return new PurchaseReceipt(config).DeletePurchaseReceipt(sysDocID, voucherID);
		}

		public bool VoidPurchaseReceipt(string sysDocID, string voucherID, bool isVoid)
		{
			return new PurchaseReceipt(config).VoidPurchaseReceipt(sysDocID, voucherID, isVoid);
		}

		public DataSet GetUninvoicedGRNs(string vendorID, bool isImport)
		{
			return new PurchaseReceipt(config).GetUninvoicedGRNs(vendorID, isImport);
		}

		public DataSet GetUninvoicedGRNsOnLocation(string vendorID, bool isImport, string locationID)
		{
			return new PurchaseReceipt(config).GetUninvoicedGRNsOnLocation(vendorID, isImport, locationID);
		}

		public DataSet GetUninvoicedGRNsReport(DateTime from, DateTime to, string fromItem, string toItem, string fromItemClass, string toItemClass, string fromCategory, string toCategory, string fromBrand, string toBrand, string fromLocation, string toLocation, string fromVendor, string toVendor, string fromClass, string toClass, string fromGroup, string toGroup, string vendorIDs, string fromManufacturer, string toManufacturer, string fromStyle, string toStyle, string fromOrigin, string toOrigin)
		{
			return new PurchaseReceipt(config).GetUninvoicedGRNsReport(from, to, fromItem, toItem, fromItemClass, toItemClass, fromCategory, toCategory, fromBrand, toBrand, fromLocation, toLocation, fromVendor, toVendor, fromClass, toClass, fromGroup, toGroup, vendorIDs, fromManufacturer, toManufacturer, fromStyle, toStyle, fromOrigin, toOrigin);
		}

		public DataSet GetPurchaseReceiptToPrint(string sysDocID, string[] voucherID, bool showLotDetail, bool showOrderandShipmentinGRN)
		{
			return new PurchaseReceipt(config).GetPurchaseReceiptToPrint(sysDocID, voucherID, showLotDetail, showOrderandShipmentinGRN);
		}

		public DataSet GetPurchaseReceiptToPrint(string sysDocID, string voucherID, bool showLotDetail, bool showOrderandShipmentinGRN)
		{
			return new PurchaseReceipt(config).GetPurchaseReceiptToPrint(sysDocID, new string[1]
			{
				voucherID
			}, showLotDetail, showOrderandShipmentinGRN);
		}

		public DataSet GetList(DateTime fromDate, DateTime toDate, bool showVoid, bool includeLocal, bool includeImport, string sysDocID)
		{
			return new PurchaseReceipt(config).GetList(fromDate, toDate, showVoid, includeLocal, includeImport, sysDocID);
		}

		public DataSet GetPurchaseReceiptAll()
		{
			return new PurchaseReceipt(config).GetPurchaseReceiptAll();
		}

		public DataSet GetGRNList(string sysDocID, DateTime fromDate, DateTime toDate)
		{
			return new PurchaseReceipt(config).GetGRNList(sysDocID, fromDate, toDate, includeInvoiced: true);
		}

		public DataSet GetGRNList(string sysDocID, bool includeInvoiced)
		{
			return new PurchaseReceipt(config).GetGRNList(sysDocID, includeInvoiced);
		}

		public DataSet GetPurchaseClaimGRNList(string sysDocID)
		{
			return new PurchaseReceipt(config).GetPurchaseClaimGRNList(sysDocID);
		}

		public bool CanEdit(string sysDocID, string voucherID)
		{
			return new PurchaseReceipt(config).CanEdit(sysDocID, voucherID);
		}

		public DataSet GetSalesByGRNSummaryReport(DateTime from, DateTime to, string fromCategory, string toCategory, string fromVendor, string toVendor, string fromClass, string toClass, string fromGroup, string toGroup, string sysDocID, string voucherID, string ContainerNo, string vendorIDs)
		{
			return new PurchaseReceipt(config).GetSalesByGRNSummaryReport(from, to, fromCategory, toCategory, fromVendor, toVendor, fromClass, toClass, fromGroup, toGroup, sysDocID, voucherID, ContainerNo, vendorIDs);
		}

		public DataSet GetGRNListToQC(string sysDocID, DateTime fromDate, DateTime toDate, bool includeLocal)
		{
			return new PurchaseReceipt(config).GetGRNListToQC(sysDocID, fromDate, toDate, includeLocal);
		}

		public DataSet GetItemMovementGRNReport(DateTime from, DateTime to, string fromItem, string toItem, string fromItemClass, string toItemClass, string fromCategory, string toCategory, string fromBrand, string toBrand, string fromVendor, string toVendor, string fromClass, string toClass, string fromGroup, string toGroup, string sysDocID, string voucherID, string fromManufacturer, string toManufacturer, string fromStyle, string toStyle, string fromOrigin, string toOrigin)
		{
			return new PurchaseReceipt(config).GetItemMovementGRNReport(from, to, fromItem, toItem, fromItemClass, toItemClass, fromCategory, toCategory, fromBrand, toBrand, fromVendor, toVendor, fromClass, toClass, fromGroup, toGroup, sysDocID, voucherID, fromManufacturer, toManufacturer, fromStyle, toStyle, fromOrigin, toOrigin);
		}

		public DataSet GetBarCodeReport(DateTime from, DateTime to, string fromItem, string toItem, string fromItemClass, string toItemClass, string fromCategory, string toCategory, string fromBrand, string toBrand, string sysDocID, string voucherID, string tableName, string fromManufacturer, string toManufacturer, string fromStyle, string toStyle, string fromOrigin, string toOrigin)
		{
			return new PurchaseReceipt(config).GetBarCodeReport(from, to, fromItem, toItem, fromItemClass, toItemClass, fromCategory, toCategory, fromBrand, toBrand, sysDocID, voucherID, tableName, fromManufacturer, toManufacturer, fromStyle, toStyle, fromOrigin, toOrigin);
		}

		public DataSet GetBarCodeReport(DataSet ds)
		{
			return new PurchaseReceipt(config).GetBarCodeReport(ds);
		}

		public DataSet GetSalesByGRNDetailReport(string sysDocID, string voucherID)
		{
			return new PurchaseReceipt(config).GetSalesByGRNDetailReport(sysDocID, voucherID);
		}

		public DataSet GetVoucherNumbersFromTransaction(string tableName, string sysDocID, string voucherID, DateTime fromDate, DateTime toDate)
		{
			return new PurchaseReceipt(config).GetVoucherNumbersFromTransaction(tableName, sysDocID, voucherID, fromDate, toDate);
		}

		public DataSet GetGRNItemTaxDetails(DataTable dtItems)
		{
			return new PurchaseReceipt(config).GetGRNItemTaxDetails(dtItems);
		}

		public DataSet GetContainerData(string fromClass, string toClass, int status, DateTime fromDate, DateTime toDate)
		{
			return new PurchaseReceipt(config).GetContainerData(fromClass, toClass, status, fromDate, toDate);
		}

		public DataSet GetContainerLotData(string sysDocID, string voucherID)
		{
			return new PurchaseReceipt(config).GetContainerLotData(sysDocID, voucherID);
		}

		public bool UpdateContainerDetails(PurchaseReceiptData purchaseReceiptData)
		{
			return new PurchaseReceipt(config).UpdateContainerDetails(purchaseReceiptData);
		}

		public bool ValidateProcessedDocumentEdit(string sysDocID, string voucherID, DateTime transactionDate)
		{
			return new PurchaseReceipt(config).ValidateProcessedDocumentEdit(sysDocID, voucherID, transactionDate);
		}
	}
}
