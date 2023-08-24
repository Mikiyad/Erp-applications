using Micromind.Common.Data;
using System;
using System.Data;

namespace Micromind.Common.Interfaces
{
	public interface ICaseClientSystem
	{
		bool CreateCustomer(CaseClientData customerData);

		bool UpdateCustomer(CaseClientData customerData);

		CaseClientData GetCustomer();

		bool DeleteCustomer(string ID);

		CaseClientData GetCustomerByID(string id);

		DataSet GetCustomerByFields(params string[] columns);

		DataSet GetCustomerByFields(string[] ids, params string[] columns);

		DataSet GetCustomerByFields(string[] ids, bool isInactive, params string[] columns);

		DataSet GetCustomerList(bool inactive);

		DataSet GetTransactiondetails(string id);

		DataSet GetTenantList(bool inactive);

		DataSet GetCustomerComboList();

		DataSet GetCustomerComboList(string id);

		DataSet GetCustomerBalanceSummary(string fromCustomer, string toCustomer, string fromClass, string toClass, string fromGroup, string toGroup, string fromSalesperson, string toSalesperson, string fromCountry, string toCountry, bool showZeroBalance, bool isFC, string customerIDs);

		DataSet GetCustomerBalanceDetailReport(DateTime from, DateTime to, string fromCustomer, string toCustomer, string fromClass, string toClass, string fromGroup, string toGroup, string fromJob, string toJob, bool showZeroBalance, string currencyID, string customerIDs);

		DataSet GetCustomerBalanceDetailFCReport(DateTime from, DateTime to, string fromCustomer, string toCustomer, string fromClass, string toClass, string fromGroup, string toGroup, string fromJob, string toJob, bool showZeroBalance, string currencyID, string customerIDs);

		DataSet GetSalesByCustomerDetailReport(DateTime from, DateTime to, string fromCustomer, string toCustomer, string fromClass, string toClass, string fromGroup, string toGroup, string customerIDs);

		DataSet GetSalesByCustomerSummaryReport(DateTime from, DateTime to, string fromCustomer, string toCustomer, string fromClass, string toClass, string fromGroup, string toGroup, string customerIDs);

		DataSet GetSalesByCustomerGroupDetailReport(DateTime from, DateTime to, string fromGroup, string toGroup);

		DataSet GetSalesByCustomerGroupSummaryReport(DateTime from, DateTime to, string fromGroup, string toGroup);

		DataSet GetCustomerDocumentAddress(string customerID, string addressField);

		DataSet GetCustomerListReport(string fromCustomer, string toCustomer, string fromClass, string toClass, string fromGroup, string toGroup, string fromArea, string toArea, bool showInactive, string customerIDs);

		DataSet GetCustomerPrimaryContactListReport(string fromCustomer, string toCustomer, string fromClass, string toClass, string fromGroup, string toGroup, string fromArea, string toArea, bool showInactive, string customerIDs);

		DataSet GetCustomerProfileReport(string fromCustomer, string toCustomer, string fromClass, string toClass, string fromGroup, string toGroup, string fromArea, string toArea, bool showInactive, string customerIDs);

		DataSet GetCustomerActivityReport(DateTime from, DateTime to, string fromCustomer, string toCustomer, string fromClass, string toClass, string fromGroup, string toGroup, string customerIDs);

		DataSet RequestForCreditLimitReport(DateTime from, DateTime to, string fromCustomer, string toCustomer, string fromClass, string toClass, string fromGroup, string toGroup, string multipleCustomerIDs);

		DataSet GetCustomerYearMonthPaymentReport(string fromCustomer, string toCustomer, string fromClass, string toClass, string fromGroup, string toGroup, DateTime fromYear, DateTime toYear, DateTime fromMonth, DateTime toMonth, DateTime LPfromdate, DateTime LPtoDate, string customerIDs);

		DataSet GetCustomerStatement(DateTime from, DateTime to, string fromCustomer, string toCustomer, string fromClass, string toClass, string fromGroup, string toGroup, bool isFC, bool showZeroBalance, bool isConsolidated, bool includeHold, bool onlyEmailMethod, bool showopenInvoices, string customerIDs);

		DataSet GetSecondCustomerStatement(DateTime from, DateTime to, string fromCustomer, string toCustomer, string fromClass, string toClass, string fromGroup, string toGroup, bool isFC, bool showZeroBalance, bool isConsolidated, bool includeHold, bool onlyEmailMethod, bool showopenInvoices, string customerIDs);

		DataSet GetCustomerAgingSummary(string fromCustomer, string toCustomer, string fromClass, string toClass, string fromGroup, string toGroup, DateTime asOfDate, bool showZeroBalance, string fromSalesperson, string toSalesperson, string customerIDs);

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

		DataSet GetCustomerOutstandingInvoicesList(string fromCustomer, bool showZeroBalance, bool isFC);

		DataSet GetCustomerAgingDetail(string fromCustomer, string toCustomer, string fromClass, string toClass, string fromGroup, string toGroup, DateTime asOfDate, bool showZeroBalance, string fromSalesperson, string toSalesperson, string customerIDs);

		DataSet GetDailySalesReport(int year, int month);

		CaseClientData GetCustomerChildren(string parentID);

		bool UpdateCustomerParentChildRelation(CaseClientData data);

		bool DeleteCustomerChildren(string parentID);

		bool UpdateDueDates(string fromCustomer, string toCustomer);

		DataSet GetCustomerRelationshipList();

		DataSet GetCustomerSnapBalance(string customerID);

		bool HasBalance(string customerID);

		string GetCustomerAddressPrintFormat(string customerID, string addressID);

		DataSet GetCustomerSalesDetail(DateTime from, DateTime to, string customerID);

		DataSet GetInventorySalesDetail(DateTime from, DateTime to, string productID);

		DataSet GetInventorySalesDetailByCategory(DateTime from, DateTime to, string productID);

		DataSet GetInventorySalesItemDetail(string customerID, string productID);

		DataSet GetCustomerOutstandingInvoicesDetailReport(DateTime reportDate, string fromCustomer, string toCustomer, string fromClass, string toClass, string fromGroup, string toGroup, bool showZeroBalance, bool isFC, string customerIDs);

		DataSet GetCustomerLeadsComboList();

		DataSet GetSMSCustomerList();

		DataSet GetTransactionCustomerList();

		DataSet GetCustomerList();

		DataSet GetCustomerMobileNo(string CustomerID);

		DataSet GetSalesEnquiryDetailReport(DateTime fromDate, DateTime toDate, string fromItem, string toItem, string fromItemClass, string toItemClass, string fromItemCategory, string toItemCategory, string fromCustomer, string toCustomer, string fromCustomerClass, string toCustomerClass, string fromCustomerGroup, string toCustomerGroup, string fromSalesperson, string toSalesperson, string fromLocation, string toLocation, string customerIDs);

		DataSet GetSalesEnquirySummaryReport(DateTime from, DateTime to, string fromCustomer, string toCustomer, string fromClass, string toClass, string fromGroup, string toGroup, string fromSalesperson, string toSalesperson, string customerIDs);

		DataSet GetSalesQuoteDetailReport(DateTime fromDate, DateTime toDate, string fromItem, string toItem, string fromItemClass, string toItemClass, string fromItemCategory, string toItemCategory, string fromCustomer, string toCustomer, string fromCustomerClass, string toCustomerClass, string fromCustomerGroup, string toCustomerGroup, string fromSalesperson, string toSalesperson, string fromLocation, string toLocation, string customerIDs);

		DataSet GetSalesQuoteSummaryReport(DateTime from, DateTime to, string fromCustomer, string toCustomer, string fromClass, string toClass, string fromGroup, string toGroup, string fromSalesperson, string toSalesperson, string customerIDs);

		DataSet GetSalesOrderDetailReport(DateTime fromDate, DateTime toDate, string fromItem, string toItem, string fromItemClass, string toItemClass, string fromItemCategory, string toItemCategory, string fromCustomer, string toCustomer, string fromCustomerClass, string toCustomerClass, string fromCustomerGroup, string toCustomerGroup, string fromSalesperson, string toSalesperson, string fromLocation, string toLocation, bool isExport, string customerIDs);

		DataSet GetSalesOrderSummaryReport(DateTime from, DateTime to, string fromCustomer, string toCustomer, string fromClass, string toClass, string fromGroup, string toGroup, string fromSalesperson, string toSalesperson, bool isExport, string customerIDs);

		DataSet GetProformaInvoiceDetailReport(DateTime fromDate, DateTime toDate, string fromItem, string toItem, string fromItemClass, string toItemClass, string fromItemCategory, string toItemCategory, string fromCustomer, string toCustomer, string fromCustomerClass, string toCustomerClass, string fromCustomerGroup, string toCustomerGroup, string fromSalesperson, string toSalesperson, string fromLocation, string toLocation);

		DataSet GetProformaInvoiceSummaryReport(DateTime from, DateTime to, string fromCustomer, string toCustomer, string fromClass, string toClass, string fromGroup, string toGroup, string fromSalesperson, string toSalesperson);

		DataSet GetDeliveryNoteDetailReport(DateTime fromDate, DateTime toDate, string fromItem, string toItem, string fromItemClass, string toItemClass, string fromItemCategory, string toItemCategory, string fromCustomer, string toCustomer, string fromCustomerClass, string toCustomerClass, string fromCustomerGroup, string toCustomerGroup, string fromSalesperson, string toSalesperson, string fromLocation, string toLocation);

		DataSet GetDeliveryNoteSummaryReport(DateTime from, DateTime to, string fromCustomer, string toCustomer, string fromClass, string toClass, string fromGroup, string toGroup, string fromSalesperson, string toSalesperson);

		DataSet GetSalesReceiptDetailReport(DateTime fromDate, DateTime toDate, string fromItem, string toItem, string fromItemClass, string toItemClass, string fromItemCategory, string toItemCategory, string fromCustomer, string toCustomer, string fromCustomerClass, string toCustomerClass, string fromCustomerGroup, string toCustomerGroup, string fromSalesperson, string toSalesperson, string fromLocation, string toLocation, string customerIDs);

		DataSet GetSalesReceiptSummaryReport(DateTime from, DateTime to, string fromCustomer, string toCustomer, string fromClass, string toClass, string fromGroup, string toGroup, string fromSalesperson, string toSalesperson, string customerIDs);

		DataSet GetSalesInvoiceDetailReport(DateTime fromDate, DateTime toDate, string fromItem, string toItem, string fromItemClass, string toItemClass, string fromItemCategory, string toItemCategory, string fromCustomer, string toCustomer, string fromCustomerClass, string toCustomerClass, string fromCustomerGroup, string toCustomerGroup, string fromSalesperson, string toSalesperson, string fromLocation, string toLocation, string customerIDs);

		DataSet GetSalesInvoiceSummaryReport(DateTime from, DateTime to, string fromCustomer, string toCustomer, string fromClass, string toClass, string fromGroup, string toGroup, string fromSalesperson, string toSalesperson, string customerIDs);

		DataSet GetCaseClientList(bool isDefendant, bool plantiff);
	}
}
