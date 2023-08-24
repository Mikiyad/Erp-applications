using Micromind.Common.Data;
using System;
using System.Data;

namespace Micromind.Common.Interfaces
{
	public interface IVendorSystem
	{
		bool CreateVendor(VendorData vendorData);

		bool UpdateVendor(VendorData vendorData);

		bool CreateServiceProvider(VendorData vendorData);

		bool UpdateServiceProvider(VendorData vendorData);

		VendorData GetVendor();

		bool DeleteVendor(string ID);

		bool DeleteServiceProvider(string ID);

		VendorData GetVendorByID(string id);

		VendorData GetServiceProviderByID(string id);

		DataSet GetVendorDueBalanceSummary(string vendorID, string currencyID, DateTime asOfDate);

		DataSet GetVendorByFields(params string[] columns);

		DataSet GetVendorByFields(string[] ids, params string[] columns);

		DataSet GetVendorByFields(string[] ids, bool isInactive, params string[] columns);

		DataSet GetTransactiondetails(string VendorID);

		DataSet GetVendorList(bool showInactive);

		DataSet GetVendorComboList();

		DataSet GetVendorSelectionList();

		DataSet GetServiceProviderComboList();

		DataSet GetServiceProviderList(bool showInactive);

		DataSet GetServiceProviderList();

		DataSet GetVendorBalanceSummary(string fromVendor, string toVendor, string fromClass, string toClass, string fromGroup, string toGroup, bool showZeroBalance, string vendorIDs, bool isFC);

		DataSet GetVendorBalanceDetailReport(DateTime from, DateTime to, string fromVendor, string toVendor, string fromClass, string toClass, string fromGroup, string toGroup, string fromJob, string toJob, bool showZeroBalance, string currencyID);

		DataSet GetVendorBalanceDetailReport(DateTime from, DateTime to, string fromVendor, string toVendor, string fromClass, string toClass, string fromGroup, string toGroup, string fromJob, string toJob, bool showZeroBalance, string currencyID, string vendorIDs);

		DataSet GetVendorBalanceDetailFCReport(DateTime from, DateTime to, string fromVendor, string toVendor, string fromClass, string toClass, string fromGroup, string toGroup, string fromJob, string toJob, bool showZeroBalance, string currencyID, string vendorIDs);

		DataSet GetVendorDocumentAddress(string vendorID, string addressField);

		DataSet GetVendorActivityReport(DateTime from, DateTime to, string fromVendor, string toVendor, string fromClass, string toClass, string fromGroup, string toGroup, string vendorID);

		DataSet GetVendorProfileReport(string fromVendor, string toVendor, string fromClass, string toClass, string fromGroup, string toGroup, bool showInactive);

		DataSet GetVendorProfileReport(string fromVendor, string toVendor, string fromClass, string toClass, string fromGroup, string toGroup, bool showInactive, string vendorIDs);

		DataSet GetPurchaseByVendorDetailReport(DateTime from, DateTime to, string fromVendor, string toVendor, string fromClass, string toClass, string fromGroup, string toGroup, string vendorIDs);

		DataSet GetPurchaseCostEntryByVendorDetailReport(DateTime from, DateTime to, string fromVendor, string toVendor, string fromClass, string toClass, string fromGroup, string toGroup, string vendorIDs);

		DataSet GetPurchaseCostEntryByVendorSummaryReport(DateTime from, DateTime to, string fromVendor, string toVendor, string fromClass, string toClass, string fromGroup, string toGroup, string vendorIDs);

		DataSet GetPurchaseByVendorSummaryReport(DateTime from, DateTime to, string fromVendor, string toVendor, string fromClass, string toClass, string fromGroup, string toGroup, string vendorIDs);

		DataSet GetPurchaseByVendorGroupDetailReport(DateTime from, DateTime to, string fromGroup, string toGroup);

		DataSet GetPurchaseByVendorGroupSummaryReport(DateTime from, DateTime to, string fromGroup, string toGroup);

		DataSet GetVendorListReport(string fromVendor, string toVendor, string fromClass, string toClass, string fromGroup, string toGroup, bool showInactive, string VendorIDs);

		DataSet GetVendorPrimaryContactListReport(string fromVendor, string toVendor, string fromClass, string toClass, string fromGroup, string toGroup, bool showInactive, string vendorIDs);

		DataSet GetVendorAgingSummary(string fromVendor, string toVendor, string fromClass, string toClass, string fromGroup, string toGroup, DateTime asOfDate, bool showZeroBalance, string vendorIDs);

		DataSet GetVendorAgingDetail(string fromVendor, string toVendor, string fromClass, string toClass, string fromGroup, string toGroup, DateTime asOfDate, bool showZeroBalance, string vendorIDs);

		DataSet GetVendorStatement(DateTime from, DateTime to, string fromVendor, string toVendor, string fromClass, string toClass, string fromGroup, string toGroup, bool isFC, bool showZeroBalance, string vendorIDs);

		DataSet GetVendorBalanceAmount(string vendorID);

		DataSet GetTopVendorBalanceList(int count);

		DataSet GetVendorAgingBalanceList(bool showZeroBalance, bool isFC);

		bool HasBalance(string vendorID);

		DataSet GetVendorOutstandingInvoicesReport(DateTime reportDate, string fromCustomer, string toCustomer, string fromClass, string toClass, string fromGroup, string toGroup, string fromJob, string toJob, bool isFC, string vendorIDs);

		bool SetFlag(string vendorID, byte flagID);

		DataSet GetInventoryPurchaseDetail(DateTime from, DateTime to, string productID);

		DataSet GetInventoryPurchaseDetailByVendor(DateTime from, DateTime to, string vendorID);

		DataSet GetPurchaseLogReport(string SysDocID, string ContainerNo, string fromVendor, string toVendor, string fromClass, string toClass, string fromGroup, string toGroup, string vendorIDs);

		DataSet GetInventoryPurchaseItemDetail(string vendorID, string productID);

		DataSet GetInventoryPurchaseItemDetail(string vendorID, string productID, bool loadAll);

		DataSet GetVendorOutstandingInvoicesDetailReport(DateTime reportDate, string fromCustomer, string toCustomer, string fromClass, string toClass, string fromGroup, string toGroup, string fromJob, string toJob, bool isFC, string vendorIDs);

		DataSet GetPurchaseQuoteDetailReport(DateTime fromDate, DateTime toDate, string fromItem, string toItem, string fromItemClass, string toItemClass, string fromItemCategory, string toItemCategory, string fromBrand, string toBrand, string fromVendor, string toVendor, string fromClass, string toClass, string fromGroup, string toGroup, string fromBuyer, string toBuyer, string fromLocation, string toLocation, string fromManufacturer, string toManufacturer, string fromStyle, string toStyle, string fromOrigin, string toOrigin);

		DataSet GetPurchaseQuoteSummaryReport(DateTime from, DateTime to, string fromVendor, string toVendor, string fromClass, string toClass, string fromGroup, string toGroup, string fromBuyer, string toBuyer);

		DataSet GetPurchaseInvoiceDetailReport(DateTime fromDate, DateTime toDate, string fromItem, string toItem, string fromItemClass, string toItemClass, string fromItemCategory, string toItemCategory, string fromBrand, string toBrand, string fromVendor, string toVendor, string fromClass, string toClass, string fromGroup, string toGroup, string fromBuyer, string toBuyer, string fromLocation, string toLocation, string vendorIDs, string fromManufacturer, string toManufacturer, string fromStyle, string toStyle, string fromOrigin, string toOrigin);

		DataSet GetPurchaseInvoiceSummaryReport(DateTime from, DateTime to, string fromVendor, string toVendor, string fromClass, string toClass, string fromGroup, string toGroup, string fromBuyer, string toBuyer, string vendorIDs);

		DataSet GetPurchaseGRNDetailReport(DateTime fromDate, DateTime toDate, string fromItem, string toItem, string fromItemClass, string toItemClass, string fromItemCategory, string toItemCategory, string fromBrand, string toBrand, string fromVendor, string toVendor, string fromClass, string toClass, string fromGroup, string toGroup, string fromBuyer, string toBuyer, string fromLocation, string toLocation, string vendorIDs, string fromManufacturer, string toManufacturer, string fromStyle, string toStyle, string fromOrigin, string toOrigin);

		DataSet GetPurchaseGRNSummaryReport(DateTime from, DateTime to, string fromVendor, string toVendor, string fromClass, string toClass, string fromGroup, string toGroup, string fromBuyer, string toBuyer, string vendorIDs, string fromManufacturer, string toManufacturer, string fromStyle, string toStyle, string fromOrigin, string toOrigin);

		DataSet GetPurchasePLDetailReport(DateTime fromDate, DateTime toDate, string fromItem, string toItem, string fromItemClass, string toItemClass, string fromItemCategory, string toItemCategory, string fromVendor, string toVendor, string fromClass, string toClass, string fromGroup, string toGroup, string fromBuyer, string toBuyer, string vendorIDs, string fromManufacturer, string toManufacturer, string fromStyle, string toStyle, string fromOrigin, string toOrigin);

		DataSet GetPurchasePLSummaryReport(DateTime from, DateTime to, string fromVendor, string toVendor, string fromClass, string toClass, string fromGroup, string toGroup, string fromBuyer, string toBuyer, string vendorIDs);

		DataSet GetcashPurchaseDetailReport(DateTime fromDate, DateTime toDate, string fromItem, string toItem, string fromItemClass, string toItemClass, string fromItemCategory, string toItemCategory, string fromBrand, string toBrand, string fromVendor, string toVendor, string fromClass, string toClass, string fromGroup, string toGroup, string fromBuyer, string toBuyer, string vendorIDs);

		DataSet GetCashPurchaseSummaryReport(DateTime from, DateTime to, string fromVendor, string toVendor, string fromClass, string toClass, string fromGroup, string toGroup, string fromBuyer, string toBuyer, string vendorIDs);

		DataSet GetPurchaseOrderDetailReport(DateTime fromDate, DateTime toDate, string fromItem, string toItem, string fromItemClass, string toItemClass, string fromItemCategory, string toItemCategory, string fromBrand, string toBrand, string fromVendor, string toVendor, string fromClass, string toClass, string fromGroup, string toGroup, string fromBuyer, string toBuyer, string fromJob, string toJob, string fromLocation, string toLocation, bool IsImport, string vendorIDs, string fromManufacturer, string toManufacturer, string fromStyle, string toStyle, string fromOrigin, string toOrigin);

		DataSet GetPurchaseOrderSummaryReport(DateTime from, DateTime to, string fromVendor, string toVendor, string fromClass, string toClass, string fromGroup, string toGroup, string fromBuyer, string toBuyer, string fromJob, string toJob, bool IsImport, string vendorIDs);

		DataSet GetVendorOutstandingSummaryReport(DateTime reportDate, string fromVendor, string toVendor, string fromClass, string toClass, string fromGroup, string toGroup, bool isFC, string vendorIDs);

		string GetVendorAddressPrintFormat(string customerID, string addressID);

		DataSet GetVendorOutstandingInvoicesList(string fromVendor, bool isFC);
	}
}
