using Micromind.Common.Data;
using System;
using System.Data;

namespace Micromind.Common.Interfaces
{
	public interface ICustomerSystem
	{
		bool CreateCustomer(CustomerData customerData);

		bool UpdateCustomer(CustomerData customerData);

		CustomerData GetCustomer();

		bool DeleteCustomer(string ID);

		CustomerData GetCustomerByID(string id);

		DataSet GetCustomerByFields(params string[] columns);

		DataSet GetCustomerByFields(string[] ids, params string[] columns);

		DataSet GetCustomerByFields(string[] ids, bool isInactive, params string[] columns);

		DataSet GetCustomerList(bool inactive);

		DataSet GetTransactiondetails(string id);

		DataSet GetTenantList(bool inactive);

		DataSet GetCustomerComboList();

		DataSet GetCustomerComboList(string id);

		DataSet GetCustomerBalanceSummary(string fromCustomer, string toCustomer, string fromClass, string toClass, string fromGroup, string toGroup, string fromSalesperson, string toSalesperson, string fromCountry, string toCountry, string fromArea, string toArea, string fromSalespersonDivision, string toSalespersonDivision, string fromSalespersonGroup, string toSalespersonGroup, string fromSalespersonArea, string toSalespersonArea, string fromSalespersonCountry, string toSalespersonCountry, bool showZeroBalance, bool isFC, string customerIDs);

		DataSet GetCustomerBalanceDetailReport(DateTime from, DateTime to, string fromCustomer, string toCustomer, string fromClass, string toClass, string fromGroup, string toGroup, string fromArea, string toArea, string fromCountry, string toCountry, string fromJob, string toJob, bool showZeroBalance, string currencyID, string customerIDs);

		DataSet GetCustomerBalanceDetailFCReport(DateTime from, DateTime to, string fromCustomer, string toCustomer, string fromClass, string toClass, string fromGroup, string toGroup, string fromArea, string toArea, string fromCountry, string toCountry, string fromJob, string toJob, bool showZeroBalance, string currencyID, string customerIDs);

		DataSet GetSalesByCustomerDetailReport(DateTime from, DateTime to, string fromCustomer, string toCustomer, string fromClass, string toClass, string fromGroup, string toGroup, string fromArea, string toArea, string fromCountry, string toCountry, string customerIDs);

		DataSet GetSalesByCustomerSummaryReport(DateTime from, DateTime to, string fromCustomer, string toCustomer, string fromClass, string toClass, string fromGroup, string toGroup, string fromArea, string toArea, string fromCountry, string toCountry, string customerIDs);

		DataSet GetSalesByCustomerGroupDetailReport(DateTime from, DateTime to, string fromGroup, string toGroup);

		DataSet GetSalesByCustomerGroupSummaryReport(DateTime from, DateTime to, string fromGroup, string toGroup);

		DataSet GetCustomerDocumentAddress(string customerID, string addressField);

		bool IsOverCreditLimit(string customerID, string sysDocID, string voucherID, decimal amount, bool checkOpenDN, DateTime transactionDate);

		DataSet GetCustomerListReport(string fromCustomer, string toCustomer, string fromClass, string toClass, string fromGroup, string toGroup, string fromArea, string toArea, string fromCountry, string toCountry, bool showInactive, string customerIDs);

		DataSet GetCustomerPrimaryContactListReport(string fromCustomer, string toCustomer, string fromClass, string toClass, string fromGroup, string toGroup, string fromArea, string toArea, string fromCountry, string toCountry, bool showInactive, string customerIDs);

		DataSet GetCustomerProfileReport(string fromCustomer, string toCustomer, string fromClass, string toClass, string fromGroup, string toGroup, string fromArea, string toArea, string fromCountry, string toCountry, bool showInactive, string customerIDs);

		DataSet GetCustomerActivityReport(DateTime from, DateTime to, string fromCustomer, string toCustomer, string fromClass, string toClass, string fromGroup, string toGroup, string fromArea, string toArea, string fromCountry, string toCountry, string customerIDs);

		DataSet GetCustomerTopList(DateTime from, DateTime to, int Top, string mode);

		DataSet GetProductTopList(DateTime from, DateTime to, int Top, string mode);

		DataSet RequestForCreditLimitReport(DateTime from, DateTime to, string fromCustomer, string toCustomer, string fromClass, string toClass, string fromGroup, string toGroup, string fromArea, string toArea, string fromCountry, string toCountry, string multipleCustomerIDs);

		DataSet GetCustomerYearMonthPaymentReport(string fromCustomer, string toCustomer, string fromClass, string toClass, string fromGroup, string toGroup, string fromArea, string toArea, string fromCountry, string toCountry, DateTime fromYear, DateTime toYear, DateTime fromMonth, DateTime toMonth, DateTime LPfromdate, DateTime LPtoDate, string customerIDs);

		DataSet GetCustomerStatement(DateTime from, DateTime to, string fromCustomer, string toCustomer, string fromClass, string toClass, string fromGroup, string toGroup, string fromArea, string toArea, string fromCountry, string toCountry, bool isFC, bool showZeroBalance, bool isConsolidated, bool includeHold, bool onlyEmailMethod, bool showopenInvoices, string customerIDs);

		DataSet GetSecondCustomerStatement(DateTime from, DateTime to, string fromCustomer, string toCustomer, string fromClass, string toClass, string fromGroup, string toGroup, string fromArea, string toArea, string fromCountry, string toCountry, bool isFC, bool showZeroBalance, bool isConsolidated, bool includeHold, bool onlyEmailMethod, bool showopenInvoices, string customerIDs);

		DataSet GetCustomerAgingSummary(string fromCustomer, string toCustomer, string fromClass, string toClass, string fromGroup, string toGroup, string fromArea, string toArea, string fromCountry, string toCountry, DateTime asOfDate, bool showZeroBalance, string fromSalesperson, string toSalesperson, string fromSalespersonDivision, string toSalespersonDivision, string fromSalespersonGroup, string toSalespersonGroup, string fromSalespersonArea, string toSalespersonArea, string fromSalespersonCountry, string toSalespersonCountry, string customerIDs);

		DataSet GetCustomerBalanceAmount(string customerCode);

		DataSet GetPOSCustomerList();

		DataSet GetTopCustomers(DateTime from, DateTime to, int count);

		DataSet GetTopSalesperson(DateTime from, DateTime to, int count);

		DataSet GetMonthlySalesReport(DateTime from, DateTime to);

		DataSet GetTopInvoicesReport(DateTime from, DateTime to, int count);

		DataSet GetCustomerAgingBalanceList(bool showZeroBalance, bool isFC, bool inActive);

		bool SetFlag(string customerID, byte flagID);

		DataSet GetSalesHistory(DateTime from, DateTime to);

		DataSet GetCustomerOutstandingInvoicesReport(DateTime reportDate, string fromCustomer, string toCustomer, string fromClass, string toClass, string fromGroup, string toGroup, bool showZeroBalance, bool isFC, string customerIDs);

		DataSet GetCustomerOutstandingSummaryReport(DateTime reportDate, string fromCustomer, string toCustomer, string fromClass, string toClass, string fromGroup, string toGroup, string fromArea, string toArea, string fromCountry, string toCountry, string fromLocationId, string toLocationID, string fromSalesperson, string toSalesperson, string fromSalespersonDivision, string toSalespersonDivision, string fromSalespersonGroup, string toSalespersonGroup, string fromSalespersonArea, string toSalespersonArea, string fromSalespersonCountry, string toSalespersonCountry, bool showZeroBalance, bool isFC, string customerIDs);

		DataSet GetCustomerOutstandingInvoicesReport(DateTime reportDate, string fromCustomer, string toCustomer, string fromClass, string toClass, string fromGroup, string toGroup, string fromArea, string toArea, string fromCountry, string toCountry, string fromLocationId, string toLocationID, bool showZeroBalance, bool isFC, string customerIDs);

		DataSet GetCustomerDueReport(DateTime reportDate, string fromCustomer, string toCustomer, string fromClass, string toClass, string fromGroup, string toGroup, string fromArea, string toArea, string fromCountry, string toCountry, string fromLocationID, string toLocationID, bool showZeroBalance, bool isFC, string customerIDs, string strGroupBy);

		DataSet GetCustomerOutstandingInvoicesList(string fromCustomer, bool showZeroBalance, bool isFC);

		DataSet GetCustomerAgingDetail(string fromCustomer, string toCustomer, string fromClass, string toClass, string fromGroup, string toGroup, string fromArea, string toArea, string fromCountry, string toCountry, DateTime asOfDate, bool showZeroBalance, string fromSalesperson, string toSalesperson, string fromSalespersonDivision, string toSalespersonDivision, string fromSalespersonGroup, string toSalespersonGroup, string fromSalespersonArea, string toSalespersonArea, string fromSalespersonCountry, string toSalespersonCountry, string customerIDs);

		DataSet GetDailySalesReport(int year, int month);

		CustomerData GetCustomerChildren(string parentID);

		bool UpdateCustomerParentChildRelation(CustomerData data);

		bool DeleteCustomerChildren(string parentID);

		bool UpdateDueDates(string fromCustomer, string toCustomer);

		DataSet GetCustomerRelationshipList();

		DataSet GetCustomerSnapBalance(string customerID);

		bool HasBalance(string customerID);

		string GetCustomerAddressPrintFormat(string customerID, string addressID);

		DataSet GetCustomerSalesDetail(DateTime from, DateTime to, string customerID);

		DataSet GetInventorySalesDetail(DateTime from, DateTime to, string productID, bool includeNonSale);

		DataSet GetInventorySalesDetailByCategory(DateTime from, DateTime to, string productID, bool includeNonSale);

		DataSet GetInventorySalesItemDetail(string customerID, string productID, string LocationID = "");

		DataSet GetInventorySalesItemDetail(string customerID, string productID, bool loadAll, string LocationID = "");

		DataSet GetInventorySalesDetailByCustomer(DateTime from, DateTime to, string customerID);

		DataSet GetSalesQuoteDetail(string customerID, string productID);

		DataSet GetDNDetail(string customerID, string productID);

		DataSet GetCustomerOutstandingInvoicesDetailReport(DateTime reportDate, string fromCustomer, string toCustomer, string fromClass, string toClass, string fromGroup, string toGroup, string fromArea, string toArea, string fromCountry, string toCountry, string fromLocationID, string toLocationID, bool showZeroBalance, bool isFC, string customerIDs);

		DataSet GetCustomerLeadsComboList();

		DataSet GetSMSCustomerList();

		DataSet GetTransactionCustomerList();

		DataSet GetCustomerList();

		DataSet GetCustomerMobileNo(string CustomerID);

		DataSet GetSalesEnquiryDetailReport(DateTime fromDate, DateTime toDate, string fromItem, string toItem, string fromItemClass, string toItemClass, string fromItemCategory, string toItemCategory, string fromCustomer, string toCustomer, string fromCustomerClass, string toCustomerClass, string fromCustomerGroup, string toCustomerGroup, string fromCustomerArea, string toCustomerArea, string fromCustomerCountry, string toCustomerCountry, string fromSalesperson, string toSalesperson, string fromSalespersonDivision, string toSalespersonDivision, string fromSalespersonGroup, string toSalespersonGroup, string fromSalespersonArea, string toSalespersonArea, string fromSalespersonCountry, string toSalespersonCountry, string fromLocation, string toLocation, string customerIDs, string fromManufacturer, string toManufacturer, string fromStyle, string toStyle, string fromOrigin, string toOrigin);

		DataSet GetSalesEnquirySummaryReport(DateTime from, DateTime to, string fromCustomer, string toCustomer, string fromClass, string toClass, string fromGroup, string toGroup, string fromArea, string toArea, string fromCountry, string toCountry, string fromSalesperson, string toSalesperson, string fromSalespersonDivision, string toSalespersonDivision, string fromSalespersonGroup, string toSalespersonGroup, string fromSalespersonArea, string toSalespersonArea, string fromSalespersonCountry, string toSalespersonCountry, string customerIDs);

		DataSet GetSalesQuoteDetailReport(DateTime fromDate, DateTime toDate, string fromItem, string toItem, string fromItemClass, string toItemClass, string fromItemCategory, string toItemCategory, string fromCustomer, string toCustomer, string fromCustomerClass, string toCustomerClass, string fromCustomerGroup, string toCustomerGroup, string fromCustomerArea, string toCustomerArea, string fromCustomerCountry, string toCustomerCountry, string fromSalesperson, string toSalesperson, string fromSalespersonDivision, string toSalespersonDivision, string fromSalespersonGroup, string toSalespersonGroup, string fromSalespersonArea, string toSalespersonArea, string fromSalespersonCountry, string toSalespersonCountry, string fromLocation, string toLocation, string customerIDs);

		DataSet GetSalesQuoteSummaryReport(DateTime from, DateTime to, string fromCustomer, string toCustomer, string fromClass, string toClass, string fromGroup, string toGroup, string fromArea, string toArea, string fromCountry, string toCountry, string fromSalesperson, string toSalesperson, string fromSalespersonDivision, string toSalespersonDivision, string fromSalespersonGroup, string toSalespersonGroup, string fromSalespersonArea, string toSalespersonArea, string fromSalespersonCountry, string toSalespersonCountry, string customerIDs);

		DataSet GetSalesOrderDetailReport(DateTime fromDate, DateTime toDate, string fromItem, string toItem, string fromItemClass, string toItemClass, string fromItemCategory, string toItemCategory, string fromCustomer, string toCustomer, string fromCustomerClass, string toCustomerClass, string fromCustomerGroup, string toCustomerGroup, string fromCustomerArea, string toCustomerArea, string fromCustomerCountry, string toCustomerCountry, string fromSalesperson, string toSalesperson, string fromSalespersonDivision, string toSalespersonDivision, string fromSalespersonGroup, string toSalespersonGroup, string fromSalespersonArea, string toSalespersonArea, string fromSalespersonCountry, string toSalespersonCountry, string fromLocation, string toLocation, bool isExport, string customerIDs, string fromManufacturer, string toManufacturer, string fromStyle, string toStyle, string fromOrigin, string toOrigin);

		DataSet GetSalesOrderSummaryReport(DateTime from, DateTime to, string fromCustomer, string toCustomer, string fromClass, string toClass, string fromGroup, string toGroup, string fromArea, string toArea, string fromCountry, string toCountry, string fromSalesperson, string toSalesperson, string fromSalespersonDivision, string toSalespersonDivision, string fromSalespersonGroup, string toSalespersonGroup, string fromSalespersonArea, string toSalespersonArea, string fromSalespersonCountry, string toSalespersonCountry, bool isExport, string customerIDs);

		DataSet GetProformaInvoiceDetailReport(DateTime fromDate, DateTime toDate, string fromItem, string toItem, string fromItemClass, string toItemClass, string fromItemCategory, string toItemCategory, string fromCustomer, string toCustomer, string fromCustomerClass, string toCustomerClass, string fromCustomerGroup, string toCustomerGroup, string fromCustomerArea, string toCustomerArea, string fromCustomerCountry, string toCustomerCountry, string fromSalesperson, string toSalesperson, string fromSalespersonDivision, string toSalespersonDivision, string fromSalespersonGroup, string toSalespersonGroup, string fromSalespersonArea, string toSalespersonArea, string fromSalespersonCountry, string toSalespersonCountry, string fromLocation, string toLocation, string fromManufacturer, string toManufacturer, string fromStyle, string toStyle, string fromOrigin, string toOrigin);

		DataSet GetProformaInvoiceSummaryReport(DateTime from, DateTime to, string fromCustomer, string toCustomer, string fromClass, string toClass, string fromGroup, string toGroup, string fromArea, string toArea, string fromCountry, string toCountry, string fromSalesperson, string toSalesperson, string fromSalespersonDivision, string toSalespersonDivision, string fromSalespersonGroup, string toSalespersonGroup, string fromSalespersonArea, string toSalespersonArea, string fromSalespersonCountry, string toSalespersonCountry);

		DataSet GetDeliveryNoteDetailReport(DateTime fromDate, DateTime toDate, string fromItem, string toItem, string fromItemClass, string toItemClass, string fromItemCategory, string toItemCategory, string fromCustomer, string toCustomer, string fromCustomerClass, string toCustomerClass, string fromCustomerGroup, string toCustomerGroup, string fromCustomerArea, string toCustomerArea, string fromCustomerCountry, string toCustomerCountry, string fromSalesperson, string toSalesperson, string fromSalespersonDivision, string toSalespersonDivision, string fromSalespersonGroup, string toSalespersonGroup, string fromSalespersonArea, string toSalespersonArea, string fromSalespersonCountry, string toSalespersonCountry, string fromLocation, string toLocation, string fromManufacturer, string toManufacturer, string fromStyle, string toStyle, string fromOrigin, string toOrigin);

		DataSet GetDeliveryNoteSummaryReport(DateTime from, DateTime to, string fromCustomer, string toCustomer, string fromClass, string toClass, string fromGroup, string toGroup, string fromArea, string toArea, string fromCountry, string toCountry, string fromSalesperson, string toSalesperson, string fromSalespersonDivision, string toSalespersonDivision, string fromSalespersonGroup, string toSalespersonGroup, string fromSalespersonArea, string toSalespersonArea, string fromSalespersonCountry, string toSalespersonCountry);

		DataSet GetSalesReceiptDetailReport(DateTime fromDate, DateTime toDate, string fromItem, string toItem, string fromItemClass, string toItemClass, string fromItemCategory, string toItemCategory, string fromBrand, string toBrand, string fromCustomer, string toCustomer, string fromCustomerClass, string toCustomerClass, string fromCustomerGroup, string toCustomerGroup, string fromCustomerArea, string toCustomerArea, string fromCustomerCountry, string toCustomerCountry, string fromSalesperson, string toSalesperson, string fromSalespersonDivision, string toSalespersonDivision, string fromSalespersonGroup, string toSalespersonGroup, string fromSalespersonArea, string toSalespersonArea, string fromSalespersonCountry, string toSalespersonCountry, string fromLocation, string toLocation, string customerIDs, string fromManufacturer, string toManufacturer, string fromStyle, string toStyle, string fromOrigin, string toOrigin);

		DataSet GetSalesReceiptSummaryReport(DateTime from, DateTime to, string fromCustomer, string toCustomer, string fromClass, string toClass, string fromGroup, string toGroup, string fromArea, string toArea, string fromCountry, string toCountry, string fromSalesperson, string toSalesperson, string fromSalespersonDivision, string toSalespersonDivision, string fromSalespersonGroup, string toSalespersonGroup, string fromSalespersonArea, string toSalespersonArea, string fromSalespersonCountry, string toSalespersonCountry, string fromLocation, string toLocation, string customerIDs);

		DataSet GetSalesInvoiceDetailReport(DateTime fromDate, DateTime toDate, string fromItem, string toItem, string fromItemClass, string toItemClass, string fromItemCategory, string toItemCategory, string fromBrand, string toBrand, string fromCustomer, string toCustomer, string fromCustomerClass, string toCustomerClass, string fromCustomerGroup, string toCustomerGroup, string fromCustomerArea, string toCustomerArea, string fromCustomerCountry, string toCustomerCountry, string fromSalesperson, string toSalesperson, string fromSalespersonDivision, string toSalespersonDivision, string fromSalespersonGroup, string toSalespersonGroup, string fromSalespersonArea, string toSalespersonArea, string fromSalespersonCountry, string toSalespersonCountry, string fromLocation, string toLocation, string customerIDs);

		DataSet GetSalesInvoiceSummaryReport(DateTime from, DateTime to, string fromCustomer, string toCustomer, string fromClass, string toClass, string fromGroup, string toGroup, string fromArea, string toArea, string fromCountry, string toCountry, string fromSalesperson, string toSalesperson, string fromSalespersonDivision, string toSalespersonDivision, string fromSalespersonGroup, string toSalespersonGroup, string fromSalespersonArea, string toSalespersonArea, string fromSalespersonCountry, string toSalespersonCountry, string fromLocation, string toLocation, string customerIDs);

		DataSet GetSalesReservedDetailReport(DateTime fromDate, DateTime toDate, string fromItem, string toItem, string fromItemClass, string toItemClass, string fromItemCategory, string toItemCategory, string fromCustomer, string toCustomer, string fromCustomerClass, string toCustomerClass, string fromCustomerGroup, string toCustomerGroup, string fromCustomerArea, string toCustomerArea, string fromCustomerCountry, string toCustomerCountry, string fromSalesperson, string toSalesperson, string fromSalespersonDivision, string toSalespersonDivision, string fromSalespersonGroup, string toSalespersonGroup, string fromSalespersonArea, string toSalespersonArea, string fromSalespersonCountry, string toSalespersonCountry, string fromLocation, string toLocation, bool isExport, string customerIDs, string fromManufacturer, string toManufacturer, string fromStyle, string toStyle, string fromOrigin, string toOrigin);

		DataSet GetMultipleInvoices(DateTime from, DateTime to, string fromCustomer, string toCustomer, string fromClass, string toClass, string fromGroup, string toGroup, string fromArea, string toArea, string fromCountry, string toCountry, string customerIDs, string sysDocID, string voucherIDs);

		bool AddCustomerSignature(string customerID, byte[] image);

		bool RemoveCustomerSignature(string customerID);

		byte[] GetCustomerSignatureThumbnailImage(string customerID);

		DataSet GetSalesByCustomerSummaryReport(DateTime from, DateTime to, string fromCustomer, string toCustomer, string fromClass, string toClass, string fromGroup, string toGroup, string fromArea, string toArea, string fromCountry, string toCountry, string customerIDs, string classIDs, string groupIDs, string areaIDs, string countryIDs);

		DataSet GetSalesByCustomerDetailReport(DateTime from, DateTime to, string fromCustomer, string toCustomer, string fromClass, string toClass, string fromGroup, string toGroup, string fromArea, string toArea, string fromCountry, string toCountry, string customerIDs, string classIDs, string groupIDs, string areaIDs, string countryIDs);
	}
}
