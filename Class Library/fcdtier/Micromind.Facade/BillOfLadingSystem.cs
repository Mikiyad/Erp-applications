using Micromind.Common.Data;
using Micromind.Common.Interfaces;
using Micromind.Data;
using System;
using System.Data;

namespace Micromind.Facade
{
	public sealed class BillOfLadingSystem : MarshalByRefObject, IBillOfLadingSystem, IDisposable
	{
		private Config config;

		public BillOfLadingSystem(Config config)
		{
			this.config = config;
		}

		public void Dispose()
		{
		}

		public bool CreateBillOfLading(BillOfLadingData data, bool isUpdate)
		{
			return new BillOfLading(config).InsertUpdateBillOfLading(data, isUpdate);
		}

		public BillOfLadingData GetBillOfLadingByID(string sysDocID, string voucherID)
		{
			return new BillOfLading(config).GetBillOfLadingByID(sysDocID, voucherID);
		}

		public bool DeleteBillOfLading(string sysDocID, string voucherID)
		{
			return new BillOfLading(config).DeleteBillOfLading(sysDocID, voucherID);
		}

		public bool VoidBillOfLading(string sysDocID, string voucherID, bool isVoid)
		{
			return new BillOfLading(config).VoidBillOfLading(sysDocID, voucherID, isVoid);
		}

		public DataSet GetOpenOrdersSummary(string vendorID, bool isImport)
		{
			return new BillOfLading(config).GetOpenOrdersSummary(vendorID, isImport);
		}

		public DataSet GetBillOfLadingAll()
		{
			return new BillOfLading(config).GetBillOfLadingAll();
		}

		public DataSet GetPOsForPackingList(bool isImport)
		{
			return new BillOfLading(config).GetPOsForPackingList(isImport);
		}

		public DataSet GetPOsItemsToShip()
		{
			return new BillOfLading(config).GetPOsItemsToShip();
		}

		public DataSet GetBillOfLadingListReport()
		{
			return new BillOfLading(config).GetBillOfLadingListReport();
		}

		public DataSet GetBOLListForPayment()
		{
			return new BillOfLading(config).GetBOLListForPayment();
		}

		public DataSet GetBillOfLadingToPrint(string sysDocID, string[] voucherID)
		{
			return new BillOfLading(config).GetBillOfLadingToPrint(sysDocID, voucherID);
		}

		public DataSet GetBillOfLadingToPrint(string sysDocID, string voucherID)
		{
			return new BillOfLading(config).GetBillOfLadingToPrint(sysDocID, new string[1]
			{
				voucherID
			});
		}

		public DataSet GetBillOfLadingDetailReport(DateTime from, DateTime to, string jobID, string vendorID, string fromItem, string toItem, string fromClass, string toClass, string fromCategory, string toCategory)
		{
			return new BillOfLading(config).GetBillOfLadingDetailReport(from, to, jobID, vendorID, fromItem, toItem, fromClass, toClass, fromCategory, toCategory);
		}

		public DataSet GetList(DateTime from, DateTime to, bool showVoid)
		{
			return new BillOfLading(config).GetList(from, to, showVoid);
		}

		public bool CanUpdate(string sysDocID, string voucherNumber)
		{
			return new BillOfLading(config).CanUpdate(sysDocID, voucherNumber, null);
		}

		public bool SetOrderStatus(string sysDocID, string voucherID, PurchaseOrderStatus status)
		{
			return new BillOfLading(config).SetOrderStatus(sysDocID, voucherID, status);
		}

		public DataSet GetOpenPOComboData(string vendorID)
		{
			return new BillOfLading(config).GetOpenPOComboData(vendorID);
		}

		public DataSet GetPOListForPayment(string vendorID)
		{
			return new BillOfLading(config).GetPOListForPayment(vendorID);
		}

		public DataSet GetCostEntryList()
		{
			return new BillOfLading(config).GetCostEntryList();
		}

		public BillOfLadingData GetCostEntryByID(string sysDocID, string vendorID)
		{
			return new BillOfLading(config).GetCostEntryByID(sysDocID, vendorID);
		}

		public DataSet GetOpenExpenseDetails(string sysDocID, string vendorID, string ID, bool toChk, bool isupdate, string invoiceSysDocID, string invoicevoucherID)
		{
			return new BillOfLading(config).GetOpenExpenseDetails(sysDocID, vendorID, ID, toChk, isupdate, invoiceSysDocID, invoicevoucherID);
		}

		public DataSet GetCostEntryBOLList(string BOL)
		{
			return new BillOfLading(config).GetCostEntryBOLList(BOL);
		}

		public DataSet GetVendorExpenseList(string VendorID)
		{
			return new BillOfLading(config).GetVendorExpenseList(VendorID);
		}

		public DataSet GetBOLForPackingList(string VendorID)
		{
			return new BillOfLading(config).GetBOLForPackingList(VendorID);
		}

		public DataSet GetBOLList(string BOLNumber)
		{
			return new BillOfLading(config).GetBOLList(BOLNumber);
		}
	}
}
