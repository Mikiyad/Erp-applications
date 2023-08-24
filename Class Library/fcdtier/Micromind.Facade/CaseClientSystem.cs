using Micromind.Common.Data;
using Micromind.Common.Interfaces;
using Micromind.Data;
using System;
using System.Data;

namespace Micromind.Facade
{
	public sealed class CaseClientSystem : MarshalByRefObject, ICaseClientSystem, IDisposable
	{
		private Config config;

		public CaseClientSystem(Config config)
		{
			this.config = config;
		}

		public void Dispose()
		{
		}

		public bool CreateCustomer(CaseClientData data)
		{
			return new CaseClient(config).InsertUpdateCustomer(data, isUpdate: false);
		}

		public bool UpdateCustomer(CaseClientData data)
		{
			return UpdateCustomer(data, checkConcurrency: false);
		}

		public bool UpdateCustomer(CaseClientData data, bool checkConcurrency)
		{
			return new CaseClient(config).InsertUpdateCustomer(data, isUpdate: true);
		}

		public CaseClientData GetCustomer()
		{
			using (CaseClient caseClient = new CaseClient(config))
			{
				return caseClient.GetCustomer();
			}
		}

		public bool DeleteCustomer(string groupID)
		{
			using (CaseClient caseClient = new CaseClient(config))
			{
				return caseClient.DeleteCustomer(groupID);
			}
		}

		public CaseClientData GetCustomerByID(string id)
		{
			using (CaseClient caseClient = new CaseClient(config))
			{
				return caseClient.GetCustomerByID(id);
			}
		}

		public DataSet GetTransactionCustomerList()
		{
			using (CaseClient caseClient = new CaseClient(config))
			{
				return caseClient.GetTransactionCustomerList();
			}
		}

		public DataSet GetCustomerList()
		{
			using (CaseClient caseClient = new CaseClient(config))
			{
				return caseClient.GetCustomerList();
			}
		}

		public DataSet GetCustomerByFields(params string[] columns)
		{
			using (CaseClient caseClient = new CaseClient(config))
			{
				return caseClient.GetCustomerByFields(columns);
			}
		}

		public DataSet GetCustomerByFields(string[] ids, params string[] columns)
		{
			using (CaseClient caseClient = new CaseClient(config))
			{
				return caseClient.GetCustomerByFields(ids, columns);
			}
		}

		public DataSet GetCustomerByFields(string[] ids, bool isInactive, params string[] columns)
		{
			using (CaseClient caseClient = new CaseClient(config))
			{
				return caseClient.GetCustomerByFields(ids, isInactive, columns);
			}
		}

		public DataSet GetCustomerList(bool showInactive)
		{
			using (CaseClient caseClient = new CaseClient(config))
			{
				return caseClient.GetCustomerList(showInactive);
			}
		}

		public DataSet GetTenantList(bool showInactive)
		{
			using (CaseClient caseClient = new CaseClient(config))
			{
				return caseClient.GetTenantList(showInactive);
			}
		}

		public DataSet GetCustomerComboList()
		{
			using (CaseClient caseClient = new CaseClient(config))
			{
				return caseClient.GetCaseClientComboList();
			}
		}

		public DataSet GetCustomerComboList(string id)
		{
			using (CaseClient caseClient = new CaseClient(config))
			{
				return caseClient.GetCaseClientComboList(id);
			}
		}

		public DataSet GetTransactiondetails(string CustomerID)
		{
			using (CaseClient caseClient = new CaseClient(config))
			{
				return caseClient.GetTransactionDetails(CustomerID);
			}
		}

		public DataSet GetCustomerBalanceSummary(string fromCustomer, string toCustomer, string fromClass, string toClass, string fromGroup, string toGroup, string fromSalesperson, string toSalesperson, string fromCountry, string toCountry, bool showZeroBalance, bool isFC, string customerIDs)
		{
			using (CaseClient caseClient = new CaseClient(config))
			{
				return caseClient.GetCustomerBalanceSummary(fromCustomer, toCustomer, fromClass, toClass, fromGroup, toGroup, fromSalesperson, toSalesperson, fromCountry, toCountry, showZeroBalance, isFC, customerIDs);
			}
		}

		public DataSet GetCustomerBalanceDetailFCReport(DateTime from, DateTime to, string fromCustomer, string toCustomer, string fromClass, string toClass, string fromGroup, string toGroup, string fromJob, string toJob, bool showZeroBalance, string currencyID, string customerIDs)
		{
			using (CaseClient caseClient = new CaseClient(config))
			{
				return caseClient.GetCustomerBalanceDetailFCReport(from, to, fromCustomer, toCustomer, fromClass, toClass, fromGroup, toGroup, fromJob, toJob, showZeroBalance, currencyID, customerIDs);
			}
		}

		public DataSet GetCustomerBalanceDetailReport(DateTime from, DateTime to, string fromCustomer, string toCustomer, string fromClass, string toClass, string fromGroup, string toGroup, string fromJob, string toJob, bool showZeroBalance, string currencyID, string customerIDs)
		{
			using (CaseClient caseClient = new CaseClient(config))
			{
				return caseClient.GetCustomerBalanceDetailReport(from, to, fromCustomer, toCustomer, fromClass, toClass, fromGroup, toGroup, fromJob, toJob, showZeroBalance, currencyID, customerIDs);
			}
		}

		public DataSet GetSalesByCustomerDetailReport(DateTime from, DateTime to, string fromCustomer, string toCustomer, string fromClass, string toClass, string fromGroup, string toGroup, string customerIDs)
		{
			using (CaseClient caseClient = new CaseClient(config))
			{
				return caseClient.GetSalesByCustomerDetailReport(from, to, fromCustomer, toCustomer, fromClass, toClass, fromGroup, toGroup, customerIDs);
			}
		}

		public DataSet GetSalesByCustomerSummaryReport(DateTime from, DateTime to, string fromCustomer, string toCustomer, string fromClass, string toClass, string fromGroup, string toGroup, string customerIDs)
		{
			using (CaseClient caseClient = new CaseClient(config))
			{
				return caseClient.GetSalesByCustomerSummaryReport(from, to, fromCustomer, toCustomer, fromClass, toClass, fromGroup, toGroup, customerIDs);
			}
		}

		public DataSet GetSalesEnquiryDetailReport(DateTime fromDate, DateTime toDate, string fromItem, string toItem, string fromItemClass, string toItemClass, string fromItemCategory, string toItemCategory, string fromCustomer, string toCustomer, string fromCustomerClass, string toCustomerClass, string fromCustomerGroup, string toCustomerGroup, string fromSalesperson, string toSalesperson, string fromLocation, string toLocation, string customerIDs)
		{
			using (CaseClient caseClient = new CaseClient(config))
			{
				return caseClient.GetSalesEnquiryDetailReport(fromDate, toDate, fromItem, toItem, fromItemClass, toItemClass, fromItemCategory, toItemCategory, fromCustomer, toCustomer, fromCustomerClass, toCustomerClass, fromCustomerGroup, toCustomerGroup, fromSalesperson, toSalesperson, fromLocation, toLocation, customerIDs);
			}
		}

		public DataSet GetSalesEnquirySummaryReport(DateTime from, DateTime to, string fromCustomer, string toCustomer, string fromClass, string toClass, string fromGroup, string toGroup, string fromSalesperson, string toSalesperson, string customerIDs)
		{
			using (CaseClient caseClient = new CaseClient(config))
			{
				return caseClient.GetSalesEnquirySummaryReport(from, to, fromCustomer, toCustomer, fromClass, toClass, fromGroup, toGroup, fromSalesperson, toSalesperson, customerIDs);
			}
		}

		public DataSet GetSalesQuoteDetailReport(DateTime fromDate, DateTime toDate, string fromItem, string toItem, string fromItemClass, string toItemClass, string fromItemCategory, string toItemCategory, string fromCustomer, string toCustomer, string fromCustomerClass, string toCustomerClass, string fromCustomerGroup, string toCustomerGroup, string fromSalesperson, string toSalesperson, string fromLocation, string toLocation, string customerIDs)
		{
			using (CaseClient caseClient = new CaseClient(config))
			{
				return caseClient.GetSalesQuoteDetailReport(fromDate, toDate, fromItem, toItem, fromItemClass, toItemClass, fromItemCategory, toItemCategory, fromCustomer, toCustomer, fromCustomerClass, toCustomerClass, fromCustomerGroup, toCustomerGroup, fromSalesperson, toSalesperson, fromLocation, toLocation, customerIDs);
			}
		}

		public DataSet GetSalesQuoteSummaryReport(DateTime from, DateTime to, string fromCustomer, string toCustomer, string fromClass, string toClass, string fromGroup, string toGroup, string fromSalesperson, string toSalesperson, string customerIDs)
		{
			using (CaseClient caseClient = new CaseClient(config))
			{
				return caseClient.GetSalesQuoteSummaryReport(from, to, fromCustomer, toCustomer, fromClass, toClass, fromGroup, toGroup, fromSalesperson, toSalesperson, customerIDs);
			}
		}

		public DataSet GetSalesOrderDetailReport(DateTime fromDate, DateTime toDate, string fromItem, string toItem, string fromItemClass, string toItemClass, string fromItemCategory, string toItemCategory, string fromCustomer, string toCustomer, string fromCustomerClass, string toCustomerClass, string fromCustomerGroup, string toCustomerGroup, string fromSalesperson, string toSalesperson, string fromLocation, string toLocation, bool isExport, string customerIDs)
		{
			using (CaseClient caseClient = new CaseClient(config))
			{
				return caseClient.GetSalesOrderDetailReport(fromDate, toDate, fromItem, toItem, fromItemClass, toItemClass, fromItemCategory, toItemCategory, fromCustomer, toCustomer, fromCustomerClass, toCustomerClass, fromCustomerGroup, toCustomerGroup, fromSalesperson, toSalesperson, fromLocation, toLocation, isExport, customerIDs);
			}
		}

		public DataSet GetSalesOrderSummaryReport(DateTime from, DateTime to, string fromCustomer, string toCustomer, string fromClass, string toClass, string fromGroup, string toGroup, string fromSalesperson, string toSalesperson, bool isExport, string customerIDs)
		{
			using (CaseClient caseClient = new CaseClient(config))
			{
				return caseClient.GetSalesOrderSummaryReport(from, to, fromCustomer, toCustomer, fromClass, toClass, fromGroup, toGroup, fromSalesperson, toSalesperson, isExport, customerIDs);
			}
		}

		public DataSet GetSalesByCustomerGroupDetailReport(DateTime from, DateTime to, string fromGroup, string toGroup)
		{
			using (CaseClient caseClient = new CaseClient(config))
			{
				return caseClient.GetSalesByCustomerGroupDetailReport(from, to, fromGroup, toGroup);
			}
		}

		public DataSet GetProformaInvoiceDetailReport(DateTime fromDate, DateTime toDate, string fromItem, string toItem, string fromItemClass, string toItemClass, string fromItemCategory, string toItemCategory, string fromCustomer, string toCustomer, string fromCustomerClass, string toCustomerClass, string fromCustomerGroup, string toCustomerGroup, string fromSalesperson, string toSalesperson, string fromLocation, string toLocation)
		{
			using (CaseClient caseClient = new CaseClient(config))
			{
				return caseClient.GetProformaInvoiceDetailReport(fromDate, toDate, fromItem, toItem, fromItemClass, toItemClass, fromItemCategory, toItemCategory, fromCustomer, toCustomer, fromCustomerClass, toCustomerClass, fromCustomerGroup, toCustomerGroup, fromSalesperson, toSalesperson, fromLocation, toLocation);
			}
		}

		public DataSet GetProformaInvoiceSummaryReport(DateTime from, DateTime to, string fromCustomer, string toCustomer, string fromClass, string toClass, string fromGroup, string toGroup, string fromSalesperson, string toSalesperson)
		{
			using (CaseClient caseClient = new CaseClient(config))
			{
				return caseClient.GetProformaInvoiceSummaryReport(from, to, fromCustomer, toCustomer, fromClass, toClass, fromGroup, toGroup, fromSalesperson, toSalesperson);
			}
		}

		public DataSet GetDeliveryNoteDetailReport(DateTime fromDate, DateTime toDate, string fromItem, string toItem, string fromItemClass, string toItemClass, string fromItemCategory, string toItemCategory, string fromCustomer, string toCustomer, string fromCustomerClass, string toCustomerClass, string fromCustomerGroup, string toCustomerGroup, string fromSalesperson, string toSalesperson, string fromLocation, string toLocation)
		{
			using (CaseClient caseClient = new CaseClient(config))
			{
				return caseClient.GetDeliveryNoteDetailReport(fromDate, toDate, fromItem, toItem, fromItemClass, toItemClass, fromItemCategory, toItemCategory, fromCustomer, toCustomer, fromCustomerClass, toCustomerClass, fromCustomerGroup, toCustomerGroup, fromSalesperson, toSalesperson, fromLocation, toLocation);
			}
		}

		public DataSet GetDeliveryNoteSummaryReport(DateTime from, DateTime to, string fromCustomer, string toCustomer, string fromClass, string toClass, string fromGroup, string toGroup, string fromSalesperson, string toSalesperson)
		{
			using (CaseClient caseClient = new CaseClient(config))
			{
				return caseClient.GetDeliveryNoteSummaryReport(from, to, fromCustomer, toCustomer, fromClass, toClass, fromGroup, toGroup, fromSalesperson, toSalesperson);
			}
		}

		public DataSet GetSalesReceiptDetailReport(DateTime fromDate, DateTime toDate, string fromItem, string toItem, string fromItemClass, string toItemClass, string fromItemCategory, string toItemCategory, string fromCustomer, string toCustomer, string fromCustomerClass, string toCustomerClass, string fromCustomerGroup, string toCustomerGroup, string fromSalesperson, string toSalesperson, string fromLocation, string toLocation, string customerIDs)
		{
			using (CaseClient caseClient = new CaseClient(config))
			{
				return caseClient.GetSalesReceiptDetailReport(fromDate, toDate, fromItem, toItem, fromItemClass, toItemClass, fromItemCategory, toItemCategory, fromCustomer, toCustomer, fromCustomerClass, toCustomerClass, fromCustomerGroup, toCustomerGroup, fromSalesperson, toSalesperson, fromLocation, toLocation, customerIDs);
			}
		}

		public DataSet GetSalesReceiptSummaryReport(DateTime from, DateTime to, string fromCustomer, string toCustomer, string fromClass, string toClass, string fromGroup, string toGroup, string fromSalesperson, string toSalesperson, string customerIDs)
		{
			using (CaseClient caseClient = new CaseClient(config))
			{
				return caseClient.GetSalesReceiptSummaryReport(from, to, fromCustomer, toCustomer, fromClass, toClass, fromGroup, toGroup, fromSalesperson, toSalesperson, customerIDs);
			}
		}

		public DataSet GetSalesInvoiceDetailReport(DateTime fromDate, DateTime toDate, string fromItem, string toItem, string fromItemClass, string toItemClass, string fromItemCategory, string toItemCategory, string fromCustomer, string toCustomer, string fromCustomerClass, string toCustomerClass, string fromCustomerGroup, string toCustomerGroup, string fromSalesperson, string toSalesperson, string fromLocation, string toLocation, string customerIDs)
		{
			using (CaseClient caseClient = new CaseClient(config))
			{
				return caseClient.GetSalesInvoiceDetailReport(fromDate, toDate, fromItem, toItem, fromItemClass, toItemClass, fromItemCategory, toItemCategory, fromCustomer, toCustomer, fromCustomerClass, toCustomerClass, fromCustomerGroup, toCustomerGroup, fromSalesperson, toSalesperson, fromLocation, toLocation, customerIDs);
			}
		}

		public DataSet GetSalesInvoiceSummaryReport(DateTime from, DateTime to, string fromCustomer, string toCustomer, string fromClass, string toClass, string fromGroup, string toGroup, string fromSalesperson, string toSalesperson, string customerIDs)
		{
			using (CaseClient caseClient = new CaseClient(config))
			{
				return caseClient.GetSalesInvoiceSummaryReport(from, to, fromCustomer, toCustomer, fromClass, toClass, fromGroup, toGroup, fromSalesperson, toSalesperson, customerIDs);
			}
		}

		public DataSet GetSalesByCustomerGroupSummaryReport(DateTime from, DateTime to, string fromGroup, string toGroup)
		{
			using (CaseClient caseClient = new CaseClient(config))
			{
				return caseClient.GetSalesByCustomerGroupSummaryReport(from, to, fromGroup, toGroup);
			}
		}

		public DataSet GetCustomerDocumentAddress(string customerID, string addressField)
		{
			using (CaseClient caseClient = new CaseClient(config))
			{
				return caseClient.GetCustomerDocumentAddress(customerID, addressField);
			}
		}

		public DataSet GetCustomerListReport(string fromCustomer, string toCustomer, string fromClass, string toClass, string fromGroup, string toGroup, string fromArea, string toArea, bool showInactive, string customerIDs)
		{
			using (CaseClient caseClient = new CaseClient(config))
			{
				return caseClient.GetCustomerListReport(fromCustomer, toCustomer, fromClass, toClass, fromGroup, toGroup, fromArea, toArea, showInactive, customerIDs);
			}
		}

		public DataSet GetCustomerPrimaryContactListReport(string fromCustomer, string toCustomer, string fromClass, string toClass, string fromGroup, string toGroup, string fromArea, string toArea, bool showInactive, string customerIDs)
		{
			using (CaseClient caseClient = new CaseClient(config))
			{
				return caseClient.GetCustomerPrimaryContactListReport(fromCustomer, toCustomer, fromClass, toClass, fromGroup, toGroup, fromArea, toArea, showInactive, customerIDs);
			}
		}

		public DataSet GetCustomerProfileReport(string fromCustomer, string toCustomer, string fromClass, string toClass, string fromGroup, string toGroup, string fromArea, string toArea, bool showInactive, string customerIDs)
		{
			using (CaseClient caseClient = new CaseClient(config))
			{
				return caseClient.GetCustomerProfileReport(fromCustomer, toCustomer, fromClass, toClass, fromGroup, toGroup, fromArea, toArea, showInactive, customerIDs);
			}
		}

		public DataSet GetCustomerActivityReport(DateTime from, DateTime to, string fromCustomer, string toCustomer, string fromClass, string toClass, string fromGroup, string toGroup, string customerIDs)
		{
			using (CaseClient caseClient = new CaseClient(config))
			{
				return caseClient.GetCustomerActivityReport(from, to, fromCustomer, toCustomer, fromClass, toClass, fromGroup, toGroup, customerIDs);
			}
		}

		public DataSet RequestForCreditLimitReport(DateTime from, DateTime to, string fromCustomer, string toCustomer, string fromClass, string toClass, string fromGroup, string toGroup, string multipleCustomerIDs)
		{
			using (CaseClient caseClient = new CaseClient(config))
			{
				return caseClient.RequestForCreditLimitReport(from, to, fromCustomer, toCustomer, fromClass, toClass, fromGroup, toGroup, multipleCustomerIDs);
			}
		}

		public DataSet GetCustomerYearMonthPaymentReport(string fromCustomer, string toCustomer, string fromClass, string toClass, string fromGroup, string toGroup, DateTime fromYear, DateTime toYear, DateTime fromMonth, DateTime toMonth, DateTime LPfromDate, DateTime LPtodate, string customerIDs)
		{
			using (CaseClient caseClient = new CaseClient(config))
			{
				return caseClient.GetCustomerYearMonthPaymentReport(fromCustomer, toCustomer, fromClass, toClass, fromGroup, toGroup, fromYear, toYear, fromMonth, toMonth, LPfromDate, LPtodate, customerIDs);
			}
		}

		public DataSet GetCustomerStatement(DateTime from, DateTime to, string fromCustomer, string toCustomer, string fromClass, string toClass, string fromGroup, string toGroup, bool isFC, bool showZeroBalance, bool isConsolidated, bool includeHold, bool onlyEmailMethod, bool showopenInvoices, string customerIDs)
		{
			using (CaseClient caseClient = new CaseClient(config))
			{
				return caseClient.GetCustomerStatement(from, to, fromCustomer, toCustomer, fromClass, toClass, fromGroup, toGroup, isFC, showZeroBalance, isConsolidated, includeHold, onlyEmailMethod, showopenInvoices, customerIDs);
			}
		}

		public DataSet GetSecondCustomerStatement(DateTime from, DateTime to, string fromCustomer, string toCustomer, string fromClass, string toClass, string fromGroup, string toGroup, bool isFC, bool showZeroBalance, bool isConsolidated, bool includeHold, bool onlyEmailMethod, bool showopenInvoices, string customerIDs)
		{
			using (CaseClient caseClient = new CaseClient(config))
			{
				return caseClient.GetSecondCustomerStatement(from, to, fromCustomer, toCustomer, fromClass, toClass, fromGroup, toGroup, isFC, showZeroBalance, isConsolidated, includeHold, onlyEmailMethod, showopenInvoices, customerIDs);
			}
		}

		public DataSet GetCustomerAgingSummary(string fromCustomer, string toCustomer, string fromClass, string toClass, string fromGroup, string toGroup, DateTime asOfDate, bool showZeroBalance, string fromSalesperson, string toSalesperson, string customerIDs)
		{
			using (CaseClient caseClient = new CaseClient(config))
			{
				return caseClient.GetCustomerAgingSummary(fromCustomer, toCustomer, fromClass, toClass, fromGroup, toGroup, asOfDate, showZeroBalance, isFC: false, fromSalesperson, toSalesperson, customerIDs);
			}
		}

		public DataSet GetCustomerBalanceAmount(string customerCode)
		{
			using (CaseClient caseClient = new CaseClient(config))
			{
				return caseClient.GetCustomerBalanceAmount(customerCode);
			}
		}

		public DataSet GetPOSCustomerList()
		{
			using (CaseClient caseClient = new CaseClient(config))
			{
				return caseClient.GetPOSCustomerList();
			}
		}

		public DataSet GetTopCustomers(DateTime from, DateTime to, int count)
		{
			using (CaseClient caseClient = new CaseClient(config))
			{
				return caseClient.GetTopCustomers(from, to, count);
			}
		}

		public DataSet GetTopSalesperson(DateTime from, DateTime to, int count)
		{
			using (CaseClient caseClient = new CaseClient(config))
			{
				return caseClient.GetTopSalesperson(from, to, count);
			}
		}

		public DataSet GetMonthlySalesReport(DateTime from, DateTime to)
		{
			using (CaseClient caseClient = new CaseClient(config))
			{
				return caseClient.GetMonthlySalesReport(from, to);
			}
		}

		public DataSet GetTopInvoicesReport(DateTime from, DateTime to, int count)
		{
			using (CaseClient caseClient = new CaseClient(config))
			{
				return caseClient.GetTopInvoicesReport(from, to, count);
			}
		}

		public DataSet GetCustomerAgingBalanceList(bool showZeroBalance, bool isFC, bool inActive)
		{
			using (CaseClient caseClient = new CaseClient(config))
			{
				return caseClient.GetCustomerAgingBalanceList(showZeroBalance, isFC, inActive);
			}
		}

		public bool SetFlag(string customerID, byte flagID)
		{
			using (CaseClient caseClient = new CaseClient(config))
			{
				return caseClient.SetFlag(customerID, flagID);
			}
		}

		public DataSet GetSalesHistory(DateTime from, DateTime to)
		{
			using (CaseClient caseClient = new CaseClient(config))
			{
				return caseClient.GetSalesHistory(from, to);
			}
		}

		public DataSet GetCustomerOutstandingInvoicesReport(DateTime reportDate, string fromCustomer, string toCustomer, string fromClass, string toClass, string fromGroup, string toGroup, bool showZeroBalance, bool isFC, string customerIDs)
		{
			using (CaseClient caseClient = new CaseClient(config))
			{
				return caseClient.GetCustomerOutstandingInvoicesReport(reportDate, fromCustomer, toCustomer, fromClass, toClass, fromGroup, toGroup, showZeroBalance, isFC, customerIDs);
			}
		}

		public DataSet GetCustomerOutstandingInvoicesList(string fromCustomer, bool showZeroBalance, bool isFC)
		{
			using (CaseClient caseClient = new CaseClient(config))
			{
				return caseClient.GetCustomerOutstandingInvoicesList(fromCustomer, showZeroBalance, isFC);
			}
		}

		public DataSet GetCustomerAgingDetail(string fromCustomer, string toCustomer, string fromClass, string toClass, string fromGroup, string toGroup, DateTime asOfDate, bool showZeroBalance, string fromSalesperson, string toSalesperson, string customerIDs)
		{
			using (CaseClient caseClient = new CaseClient(config))
			{
				return caseClient.GetCustomerAgingDetail(fromCustomer, toCustomer, fromClass, toClass, fromGroup, toGroup, asOfDate, showZeroBalance, fromSalesperson, toSalesperson, customerIDs);
			}
		}

		public DataSet GetDailySalesReport(int year, int month)
		{
			using (CaseClient caseClient = new CaseClient(config))
			{
				return caseClient.GetDailySalesReport(year, month);
			}
		}

		public CaseClientData GetCustomerChildren(string parentID)
		{
			return new CaseClient(config).GetCustomerParentChildRelation(parentID);
		}

		public bool UpdateCustomerParentChildRelation(CaseClientData data)
		{
			return new CaseClient(config).UpdateCustomerParentChildRelation(data, isUpdate: true);
		}

		public bool DeleteCustomerChildren(string parentID)
		{
			return new CaseClient(config).DeleteCustomerParentChildRelation(parentID);
		}

		public bool UpdateDueDates(string fromCustomer, string toCustomer)
		{
			return new CaseClient(config).UpdateDueDates(fromCustomer, toCustomer);
		}

		public DataSet GetCustomerRelationshipList()
		{
			return new CaseClient(config).GetCustomerRelationshipList();
		}

		public DataSet GetCustomerSnapBalance(string customerID)
		{
			return new CaseClient(config).GetCustomerSnapBalance(customerID, DateTime.Now);
		}

		public DataSet GetInventorySalesDetail(DateTime from, DateTime to, string productID)
		{
			return new CaseClient(config).GetInventorySalesDetail(from, to, productID);
		}

		public DataSet GetInventorySalesDetailByCategory(DateTime from, DateTime to, string productID)
		{
			return new CaseClient(config).GetInventorySalesDetailByCategory(from, to, productID);
		}

		public DataSet GetCustomerSalesDetail(DateTime from, DateTime to, string customerID)
		{
			return new CaseClient(config).GetCustomerSalesDetail(from, to, customerID);
		}

		public bool HasBalance(string customerID)
		{
			return new CaseClient(config).HasBalance(customerID);
		}

		public DataSet GetInventorySalesItemDetail(string customerID, string ProductID)
		{
			return new CaseClient(config).GetInventorySalesItemDetail(customerID, ProductID);
		}

		public DataSet GetCustomerOutstandingInvoicesDetailReport(DateTime reportDate, string fromCustomer, string toCustomer, string fromClass, string toClass, string fromGroup, string toGroup, bool showZeroBalance, bool isFC, string customerIDs)
		{
			using (CaseClient caseClient = new CaseClient(config))
			{
				return caseClient.GetCustomerOutstandingInvoicesDetailReport(reportDate, fromCustomer, toCustomer, fromClass, toClass, fromGroup, toGroup, showZeroBalance, isFC, customerIDs);
			}
		}

		public DataSet GetCustomerLeadsComboList()
		{
			using (CaseClient caseClient = new CaseClient(config))
			{
				return caseClient.GetCustomerLeadsComboList();
			}
		}

		public string GetCustomerAddressPrintFormat(string customerID, string addressID)
		{
			using (CaseClient caseClient = new CaseClient(config))
			{
				return caseClient.GetCustomerAddressPrintFormat(customerID, addressID);
			}
		}

		public DataSet GetSMSCustomerList()
		{
			using (CaseClient caseClient = new CaseClient(config))
			{
				return caseClient.GetSMSCustomerList();
			}
		}

		public DataSet GetCustomerMobileNo(string CustomerID)
		{
			using (CaseClient caseClient = new CaseClient(config))
			{
				return caseClient.GetCustomerMobileNo(CustomerID);
			}
		}

		public DataSet GetCaseClientList(bool isDefendant, bool plantiff)
		{
			using (CaseClient caseClient = new CaseClient(config))
			{
				return caseClient.GetCaseClientList(isDefendant, plantiff);
			}
		}
	}
}
