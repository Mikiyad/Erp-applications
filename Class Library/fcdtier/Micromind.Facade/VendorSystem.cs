using Micromind.Common.Data;
using Micromind.Common.Interfaces;
using Micromind.Data;
using System;
using System.Data;

namespace Micromind.Facade
{
	public sealed class VendorSystem : MarshalByRefObject, IVendorSystem, IDisposable
	{
		private Config config;

		public VendorSystem(Config config)
		{
			this.config = config;
		}

		public void Dispose()
		{
		}

		public bool CreateVendor(VendorData data)
		{
			return new Vendors(config).InsertUpdateVendor(data, isUpdate: false);
		}

		public bool CreateServiceProvider(VendorData data)
		{
			return new Vendors(config).InsertUpdateVendor(data, isUpdate: false);
		}

		public bool UpdateVendor(VendorData data)
		{
			return UpdateVendor(data, checkConcurrency: false);
		}

		public bool UpdateVendor(VendorData data, bool checkConcurrency)
		{
			return new Vendors(config).InsertUpdateVendor(data, isUpdate: true);
		}

		public bool UpdateServiceProvider(VendorData data)
		{
			return UpdateServiceProvider(data, checkConcurrency: false);
		}

		public bool UpdateServiceProvider(VendorData data, bool checkConcurrency)
		{
			return new Vendors(config).InsertUpdateVendor(data, isUpdate: true);
		}

		public VendorData GetVendor()
		{
			using (Vendors vendors = new Vendors(config))
			{
				return vendors.GetVendor();
			}
		}

		public VendorData GetServiceProvider()
		{
			using (Vendors vendors = new Vendors(config))
			{
				return vendors.GetServiceProvider();
			}
		}

		public bool DeleteVendor(string groupID)
		{
			using (Vendors vendors = new Vendors(config))
			{
				return vendors.DeleteVendor(groupID);
			}
		}

		public bool DeleteServiceProvider(string groupID)
		{
			using (Vendors vendors = new Vendors(config))
			{
				return vendors.DeleteVendor(groupID);
			}
		}

		public VendorData GetVendorByID(string id)
		{
			using (Vendors vendors = new Vendors(config))
			{
				return vendors.GetVendorByID(id);
			}
		}

		public VendorData GetServiceProviderByID(string id)
		{
			using (Vendors vendors = new Vendors(config))
			{
				return vendors.GetServiceProviderByID(id);
			}
		}

		public DataSet GetVendorByFields(params string[] columns)
		{
			using (Vendors vendors = new Vendors(config))
			{
				return vendors.GetVendorByFields(columns);
			}
		}

		public DataSet GetVendorByFields(string[] ids, params string[] columns)
		{
			using (Vendors vendors = new Vendors(config))
			{
				return vendors.GetVendorByFields(ids, columns);
			}
		}

		public DataSet GetVendorByFields(string[] ids, bool isInactive, params string[] columns)
		{
			using (Vendors vendors = new Vendors(config))
			{
				return vendors.GetVendorByFields(ids, isInactive, columns);
			}
		}

		public DataSet GetVendorList(bool showInactive)
		{
			using (Vendors vendors = new Vendors(config))
			{
				return vendors.GetVendorList(showInactive);
			}
		}

		public DataSet GetServiceProviderList(bool showInactive)
		{
			using (Vendors vendors = new Vendors(config))
			{
				return vendors.GetServiceProviderList(showInactive);
			}
		}

		public DataSet GetServiceProviderList()
		{
			using (Vendors vendors = new Vendors(config))
			{
				return vendors.GetServiceProviderList();
			}
		}

		public DataSet GetVendorComboList()
		{
			using (Vendors vendors = new Vendors(config))
			{
				return vendors.GetVendorComboList();
			}
		}

		public DataSet GetVendorSelectionList()
		{
			using (Vendors vendors = new Vendors(config))
			{
				return vendors.GetVendorSelectionList();
			}
		}

		public DataSet GetServiceProviderComboList()
		{
			using (Vendors vendors = new Vendors(config))
			{
				return vendors.GetServiceProviderComboList();
			}
		}

		public DataSet GetVendorBalanceSummary(string fromVendor, string toVendor, string fromClass, string toClass, string fromGroup, string toGroup, bool showZeroBalance, string vendorIDs, bool isFc)
		{
			using (Vendors vendors = new Vendors(config))
			{
				return vendors.GetVendorBalanceSummary(fromVendor, toVendor, fromClass, toClass, fromGroup, toGroup, showZeroBalance, vendorIDs, isFc);
			}
		}

		public DataSet GetVendorBalanceDetailFCReport(DateTime from, DateTime to, string fromVendor, string toVendor, string fromClass, string toClass, string fromGroup, string toGroup, string fromJob, string toJob, bool showZeroBalance, string currencyID, string vendorIDs)
		{
			using (Vendors vendors = new Vendors(config))
			{
				return vendors.GetVendorBalanceDetailFCReport(from, to, fromVendor, toVendor, fromClass, toClass, fromGroup, toGroup, fromJob, toJob, showZeroBalance, currencyID, vendorIDs);
			}
		}

		public DataSet GetVendorBalanceDetailReport(DateTime from, DateTime to, string fromVendor, string toVendor, string fromClass, string toClass, string fromGroup, string toGroup, string fromJob, string toJob, bool showZeroBalance, string currencyID)
		{
			using (Vendors vendors = new Vendors(config))
			{
				return vendors.GetVendorBalanceDetailReport(from, to, fromVendor, toVendor, fromClass, toClass, fromGroup, toGroup, fromJob, toJob, showZeroBalance, currencyID);
			}
		}

		public DataSet GetVendorBalanceDetailReport(DateTime from, DateTime to, string fromVendor, string toVendor, string fromClass, string toClass, string fromGroup, string toGroup, string fromJob, string toJob, bool showZeroBalance, string currencyID, string vendorIDs)
		{
			using (Vendors vendors = new Vendors(config))
			{
				return vendors.GetVendorBalanceDetailReport(from, to, fromVendor, toVendor, fromClass, toClass, fromGroup, toGroup, fromJob, toJob, showZeroBalance, currencyID, vendorIDs);
			}
		}

		public DataSet GetVendorDocumentAddress(string vendorID, string addressField)
		{
			using (Vendors vendors = new Vendors(config))
			{
				return vendors.GetVendorDocumentAddress(vendorID, addressField);
			}
		}

		public DataSet GetPurchaseByVendorDetailReport(DateTime from, DateTime to, string fromVendor, string toVendor, string fromClass, string toClass, string fromGroup, string toGroup, string vendorIDs)
		{
			using (Vendors vendors = new Vendors(config))
			{
				return vendors.GetPurchaseByVendorDetailReport(from, to, fromVendor, toVendor, fromClass, toClass, fromGroup, toGroup, vendorIDs);
			}
		}

		public DataSet GetPurchaseByVendorSummaryReport(DateTime from, DateTime to, string fromVendor, string toVendor, string fromClass, string toClass, string fromGroup, string toGroup, string vendorIDs)
		{
			using (Vendors vendors = new Vendors(config))
			{
				return vendors.GetPurchaseByVendorSummaryReport(from, to, fromVendor, toVendor, fromClass, toClass, fromGroup, toGroup, vendorIDs);
			}
		}

		public DataSet GetPurchaseCostEntryByVendorDetailReport(DateTime from, DateTime to, string fromVendor, string toVendor, string fromClass, string toClass, string fromGroup, string toGroup, string vendorIDs)
		{
			using (Vendors vendors = new Vendors(config))
			{
				return vendors.GetPurchaseCostEntryByVendorDetailReport(from, to, fromVendor, toVendor, fromClass, toClass, fromGroup, toGroup, vendorIDs);
			}
		}

		public DataSet GetPurchaseCostEntryByVendorSummaryReport(DateTime from, DateTime to, string fromVendor, string toVendor, string fromClass, string toClass, string fromGroup, string toGroup, string vendorIDs)
		{
			using (Vendors vendors = new Vendors(config))
			{
				return vendors.GetPurchaseCostEntryByVendorSummaryReport(from, to, fromVendor, toVendor, fromClass, toClass, fromGroup, toGroup, vendorIDs);
			}
		}

		public DataSet GetPurchaseByVendorGroupDetailReport(DateTime from, DateTime to, string fromGroup, string toGroup)
		{
			using (Vendors vendors = new Vendors(config))
			{
				return vendors.GetPurchaseByVendorGroupDetailReport(from, to, fromGroup, toGroup);
			}
		}

		public DataSet GetPurchaseByVendorGroupSummaryReport(DateTime from, DateTime to, string fromGroup, string toGroup)
		{
			using (Vendors vendors = new Vendors(config))
			{
				return vendors.GetPurchaseByVendorGroupSummaryReport(from, to, fromGroup, toGroup);
			}
		}

		public DataSet GetVendorListReport(string fromVendor, string toVendor, string fromClass, string toClass, string fromGroup, string toGroup, bool showInactive, string VendorIDs)
		{
			using (Vendors vendors = new Vendors(config))
			{
				return vendors.GetVendorListReport(fromVendor, toVendor, fromClass, toClass, fromGroup, toGroup, showInactive, VendorIDs);
			}
		}

		public DataSet GetVendorProfileReport(string fromVendor, string toVendor, string fromClass, string toClass, string fromGroup, string toGroup, bool showInactive)
		{
			using (Vendors vendors = new Vendors(config))
			{
				return vendors.GetVendorProfileReport(fromVendor, toVendor, fromClass, toClass, fromGroup, toGroup, showInactive);
			}
		}

		public DataSet GetVendorProfileReport(string fromVendor, string toVendor, string fromClass, string toClass, string fromGroup, string toGroup, bool showInactive, string vendorIDs)
		{
			using (Vendors vendors = new Vendors(config))
			{
				return vendors.GetVendorProfileReport(fromVendor, toVendor, fromClass, toClass, fromGroup, toGroup, showInactive, vendorIDs);
			}
		}

		public DataSet GetVendorActivityReport(DateTime from, DateTime to, string fromVendor, string toVendor, string fromClass, string toClass, string fromGroup, string toGroup, string vendorIDs)
		{
			using (Vendors vendors = new Vendors(config))
			{
				return vendors.GetVendorActivityReport(from, to, fromVendor, toVendor, fromClass, toClass, fromGroup, toGroup, vendorIDs);
			}
		}

		public DataSet GetVendorPrimaryContactListReport(string fromVendor, string toVendor, string fromClass, string toClass, string fromGroup, string toGroup, bool showInactive, string VendorIDs)
		{
			using (Vendors vendors = new Vendors(config))
			{
				return vendors.GetVendorPrimaryContactListReport(fromVendor, toVendor, fromClass, toClass, fromGroup, toGroup, showInactive, VendorIDs);
			}
		}

		public DataSet GetVendorAgingSummary(string fromVendor, string toVendor, string fromClass, string toClass, string fromGroup, string toGroup, DateTime asOfDate, bool showZeroBalance, string vendorIDs)
		{
			using (Vendors vendors = new Vendors(config))
			{
				return vendors.GetVendorAgingSummary(fromVendor, toVendor, fromClass, toClass, fromGroup, toGroup, asOfDate, showZeroBalance, isFC: false, vendorIDs);
			}
		}

		public DataSet GetVendorAgingDetail(string fromVendor, string toVendor, string fromClass, string toClass, string fromGroup, string toGroup, DateTime asOfDate, bool showZeroBalance, string vendorIDs)
		{
			using (Vendors vendors = new Vendors(config))
			{
				return vendors.GetVendorAgingDetail(fromVendor, toVendor, fromClass, toClass, fromGroup, toGroup, asOfDate, showZeroBalance, vendorIDs);
			}
		}

		public DataSet GetVendorStatement(DateTime from, DateTime to, string fromVendor, string toVendor, string fromClass, string toClass, string fromGroup, string toGroup, bool isFC, bool showZeroBalance, string vendorIDs)
		{
			using (Vendors vendors = new Vendors(config))
			{
				return vendors.GetVendorStatement(from, to, fromVendor, toVendor, fromClass, toClass, fromGroup, toGroup, isFC, showZeroBalance, vendorIDs);
			}
		}

		public DataSet GetVendorBalanceAmount(string vendorID)
		{
			using (Vendors vendors = new Vendors(config))
			{
				return vendors.GetVendorBalanceAmount(vendorID);
			}
		}

		public DataSet GetTopVendorBalanceList(int count)
		{
			using (Vendors vendors = new Vendors(config))
			{
				return vendors.GetTopVendorBalanceList(count);
			}
		}

		public DataSet GetVendorOutstandingInvoicesReport(DateTime reportDate, string fromCustomer, string toCustomer, string fromClass, string toClass, string fromGroup, string toGroup, string fromJob, string toJob, bool isFC, string vendorIDs)
		{
			using (Vendors vendors = new Vendors(config))
			{
				return vendors.GetVendorOutstandingInvoicesReport(reportDate, fromCustomer, toCustomer, fromClass, toClass, fromGroup, toGroup, fromJob, toJob, isFC, vendorIDs);
			}
		}

		public bool HasBalance(string vendorID)
		{
			using (Vendors vendors = new Vendors(config))
			{
				return vendors.HasBalance(vendorID);
			}
		}

		public DataSet GetVendorAgingBalanceList(bool showZeroBalance, bool isFC)
		{
			using (Vendors vendors = new Vendors(config))
			{
				return vendors.GetVendorAgingBalanceList(showZeroBalance, isFC);
			}
		}

		public DataSet GetVendorDueBalanceSummary(string vendorID, string currencyID, DateTime asOfDate)
		{
			using (Vendors vendors = new Vendors(config))
			{
				return vendors.GetVendorDueBalanceSummary(vendorID, currencyID, asOfDate);
			}
		}

		public bool SetFlag(string vendorID, byte flagID)
		{
			using (Vendors vendors = new Vendors(config))
			{
				return vendors.SetFlag(vendorID, flagID);
			}
		}

		public DataSet GetInventoryPurchaseDetail(DateTime from, DateTime to, string productID)
		{
			return new Vendors(config).GetInventoryPurchaseDetail(from, to, productID);
		}

		public DataSet GetInventoryPurchaseDetailByVendor(DateTime from, DateTime to, string vendorID)
		{
			return new Vendors(config).GetInventoryPurchaseDetailByVendor(from, to, vendorID);
		}

		public DataSet GetPurchaseLogReport(string sysDocID, string ContainerNo, string fromVendor, string toVendor, string fromClass, string toClass, string fromGroup, string toGroup, string vendorIDs)
		{
			using (Vendors vendors = new Vendors(config))
			{
				return vendors.GetPurchaseLogReport(sysDocID, ContainerNo, fromVendor, toVendor, fromClass, toClass, fromGroup, toGroup, vendorIDs);
			}
		}

		public DataSet GetInventoryPurchaseItemDetail(string vendorID, string productID)
		{
			return new Vendors(config).GetInventoryPurchaseItemDetail(vendorID, productID);
		}

		public DataSet GetInventoryPurchaseItemDetail(string vendorID, string productID, bool loadAll)
		{
			return new Vendors(config).GetInventoryPurchaseItemDetail(vendorID, productID, loadAll);
		}

		public DataSet GetVendorOutstandingInvoicesDetailReport(DateTime reportDate, string fromCustomer, string toCustomer, string fromClass, string toClass, string fromGroup, string toGroup, string fromJob, string toJob, bool isFC, string vendorIDs)
		{
			using (Vendors vendors = new Vendors(config))
			{
				return vendors.GetVendorOutstandingInvoicesDetailReport(reportDate, fromCustomer, toCustomer, fromClass, toClass, fromGroup, toGroup, fromJob, toJob, isFC, vendorIDs);
			}
		}

		public DataSet GetTransactiondetails(string VendorID)
		{
			using (Vendors vendors = new Vendors(config))
			{
				return vendors.GetTransactionDetails(VendorID);
			}
		}

		public DataSet GetPurchaseQuoteDetailReport(DateTime fromDate, DateTime toDate, string fromItem, string toItem, string fromItemClass, string toItemClass, string fromItemCategory, string toItemCategory, string fromBrand, string toBrand, string fromVendor, string toVendor, string fromClass, string toClass, string fromGroup, string toGroup, string fromBuyer, string toBuyer, string fromLocation, string toLocation, string fromManufacturer, string toManufacturer, string fromStyle, string toStyle, string fromOrigin, string toOrigin)
		{
			using (Vendors vendors = new Vendors(config))
			{
				return vendors.GetPurchaseQuoteDetailReport(fromDate, toDate, fromItem, toItem, fromItemClass, toItemClass, fromItemCategory, toItemCategory, fromBrand, toBrand, fromVendor, toVendor, fromClass, toClass, fromGroup, toGroup, fromBuyer, toBuyer, fromLocation, toLocation, fromManufacturer, toManufacturer, fromStyle, toStyle, fromOrigin, toOrigin);
			}
		}

		public DataSet GetPurchaseQuoteSummaryReport(DateTime from, DateTime to, string fromVendor, string toVendor, string fromClass, string toClass, string fromGroup, string toGroup, string fromBuyer, string toBuyer)
		{
			using (Vendors vendors = new Vendors(config))
			{
				return vendors.GetPurchaseQuoteSummaryReport(from, to, fromVendor, toVendor, fromClass, toClass, fromGroup, toGroup, fromBuyer, toBuyer);
			}
		}

		public DataSet GetPurchaseInvoiceDetailReport(DateTime fromDate, DateTime toDate, string fromItem, string toItem, string fromItemClass, string toItemClass, string fromItemCategory, string toItemCategory, string fromBrand, string toBrand, string fromVendor, string toVendor, string fromClass, string toClass, string fromGroup, string toGroup, string fromBuyer, string toBuyer, string fromLocation, string toLocation, string vendorIDs, string fromManufacturer, string toManufacturer, string fromStyle, string toStyle, string fromOrigin, string toOrigin)
		{
			using (Vendors vendors = new Vendors(config))
			{
				return vendors.GetPurchaseInvoiceDetailReport(fromDate, toDate, fromItem, toItem, fromItemClass, toItemClass, fromItemCategory, toItemCategory, fromBrand, toBrand, fromVendor, toVendor, fromClass, toClass, fromGroup, toGroup, fromBuyer, toBuyer, fromLocation, toLocation, vendorIDs, fromManufacturer, toManufacturer, fromStyle, toStyle, fromOrigin, toOrigin);
			}
		}

		public DataSet GetPurchaseInvoiceSummaryReport(DateTime from, DateTime to, string fromVendor, string toVendor, string fromClass, string toClass, string fromGroup, string toGroup, string fromBuyer, string toBuyer, string vendorIDs)
		{
			using (Vendors vendors = new Vendors(config))
			{
				return vendors.GetPurchaseInvoiceSummaryReport(from, to, fromVendor, toVendor, fromClass, toClass, fromGroup, toGroup, fromBuyer, toBuyer, vendorIDs);
			}
		}

		public DataSet GetPurchasePLDetailReport(DateTime fromDate, DateTime toDate, string fromItem, string toItem, string fromItemClass, string toItemClass, string fromItemCategory, string toItemCategory, string fromVendor, string toVendor, string fromClass, string toClass, string fromGroup, string toGroup, string fromBuyer, string toBuyer, string vendorIDs, string fromManufacturer, string toManufacturer, string fromStyle, string toStyle, string fromOrigin, string toOrigin)
		{
			using (Vendors vendors = new Vendors(config))
			{
				return vendors.GetPurchasePLDetailReport(fromDate, toDate, fromItem, toItem, fromItemClass, toItemClass, fromItemCategory, toItemCategory, fromVendor, toVendor, fromClass, toClass, fromGroup, toGroup, fromBuyer, toBuyer, vendorIDs, fromManufacturer, toManufacturer, fromStyle, toStyle, fromOrigin, toOrigin);
			}
		}

		public DataSet GetPurchasePLSummaryReport(DateTime from, DateTime to, string fromVendor, string toVendor, string fromClass, string toClass, string fromGroup, string toGroup, string fromBuyer, string toBuyer, string vendorIDs)
		{
			using (Vendors vendors = new Vendors(config))
			{
				return vendors.GetPurchasePLSummaryReport(from, to, fromVendor, toVendor, fromClass, toClass, fromGroup, toGroup, fromBuyer, toBuyer, vendorIDs);
			}
		}

		public DataSet GetcashPurchaseDetailReport(DateTime fromDate, DateTime toDate, string fromItem, string toItem, string fromItemClass, string toItemClass, string fromItemCategory, string toItemCategory, string fromBrand, string toBrand, string fromVendor, string toVendor, string fromClass, string toClass, string fromGroup, string toGroup, string fromBuyer, string toBuyer, string vendorIDs)
		{
			using (Vendors vendors = new Vendors(config))
			{
				return vendors.GetcashPurchaseDetailReport(fromDate, toDate, fromItem, toItem, fromItemClass, toItemClass, fromItemCategory, toItemCategory, fromBrand, toBrand, fromVendor, toVendor, fromClass, toClass, fromGroup, toGroup, fromBuyer, toBuyer, vendorIDs);
			}
		}

		public DataSet GetCashPurchaseSummaryReport(DateTime from, DateTime to, string fromVendor, string toVendor, string fromClass, string toClass, string fromGroup, string toGroup, string fromBuyer, string toBuyer, string vendorIDs)
		{
			using (Vendors vendors = new Vendors(config))
			{
				return vendors.GetCashPurchaseSummaryReport(from, to, fromVendor, toVendor, fromClass, toClass, fromGroup, toGroup, fromBuyer, toBuyer, vendorIDs);
			}
		}

		public DataSet GetPurchaseGRNDetailReport(DateTime fromDate, DateTime toDate, string fromItem, string toItem, string fromItemClass, string toItemClass, string fromItemCategory, string toItemCategory, string fromBrand, string toBrand, string fromVendor, string toVendor, string fromClass, string toClass, string fromGroup, string toGroup, string fromBuyer, string toBuyer, string fromLocation, string toLocation, string vendorIDs, string fromManufacturer, string toManufacturer, string fromStyle, string toStyle, string fromOrigin, string toOrigin)
		{
			using (Vendors vendors = new Vendors(config))
			{
				return vendors.GetPurchaseGRNDetailReport(fromDate, toDate, fromItem, toItem, fromItemClass, toItemClass, fromItemCategory, toItemCategory, fromBrand, toBrand, fromVendor, toVendor, fromClass, toClass, fromGroup, toGroup, fromBuyer, toBuyer, fromLocation, toLocation, vendorIDs, fromManufacturer, toManufacturer, fromStyle, toStyle, fromOrigin, toOrigin);
			}
		}

		public DataSet GetPurchaseGRNSummaryReport(DateTime from, DateTime to, string fromVendor, string toVendor, string fromClass, string toClass, string fromGroup, string toGroup, string fromBuyer, string toBuyer, string vendorIDs, string fromManufacturer, string toManufacturer, string fromStyle, string toStyle, string fromOrigin, string toOrigin)
		{
			using (Vendors vendors = new Vendors(config))
			{
				return vendors.GetPurchaseGRNSummaryReport(from, to, fromVendor, toVendor, fromClass, toClass, fromGroup, toGroup, fromBuyer, toBuyer, vendorIDs, fromManufacturer, toManufacturer, fromStyle, toStyle, fromOrigin, toOrigin);
			}
		}

		public DataSet GetPurchaseOrderDetailReport(DateTime fromDate, DateTime toDate, string fromItem, string toItem, string fromItemClass, string toItemClass, string fromItemCategory, string toItemCategory, string fromBrand, string toBrand, string fromVendor, string toVendor, string fromClass, string toClass, string fromGroup, string toGroup, string fromBuyer, string toBuyer, string fromJob, string toJob, string fromLocation, string toLocation, bool IsImport, string vendorIDs, string fromManufacturer, string toManufacturer, string fromStyle, string toStyle, string fromOrigin, string toOrigin)
		{
			using (Vendors vendors = new Vendors(config))
			{
				return vendors.GetPurchaseOrderDetailReport(fromDate, toDate, fromItem, toItem, fromItemClass, toItemClass, fromItemCategory, toItemCategory, fromBrand, toBrand, fromVendor, toVendor, fromClass, toClass, fromGroup, toGroup, fromBuyer, toBuyer, fromJob, toJob, fromLocation, toLocation, IsImport, vendorIDs, fromManufacturer, toManufacturer, fromStyle, toStyle, fromOrigin, toOrigin);
			}
		}

		public DataSet GetPurchaseOrderSummaryReport(DateTime from, DateTime to, string fromVendor, string toVendor, string fromClass, string toClass, string fromGroup, string toGroup, string fromBuyer, string toBuyer, string fromJob, string toJob, bool IsImport, string vendorIDs)
		{
			using (Vendors vendors = new Vendors(config))
			{
				return vendors.GetPurchaseOrderSummaryReport(from, to, fromVendor, toVendor, fromClass, toClass, fromGroup, toGroup, fromBuyer, toBuyer, fromJob, toJob, IsImport, vendorIDs);
			}
		}

		public DataSet GetVendorOutstandingSummaryReport(DateTime reportDate, string fromVendor, string toVendor, string fromClass, string toClass, string fromGroup, string toGroup, bool isFC, string vendorIDs)
		{
			using (Vendors vendors = new Vendors(config))
			{
				return vendors.GetVendorOutstandingSummaryReport(reportDate, fromVendor, toVendor, fromClass, toClass, fromGroup, toGroup, isFC, vendorIDs);
			}
		}

		public string GetVendorAddressPrintFormat(string vendorID, string addressID)
		{
			using (Vendors vendors = new Vendors(config))
			{
				return vendors.GetVendorAddressPrintFormat(vendorID, addressID);
			}
		}

		public DataSet GetVendorOutstandingInvoicesList(string fromVendor, bool isFC)
		{
			using (Vendors vendors = new Vendors(config))
			{
				return vendors.GetVendorOutstandingInvoicesList(fromVendor, isFC);
			}
		}
	}
}
