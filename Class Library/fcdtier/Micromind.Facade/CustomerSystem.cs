using Micromind.Common.Data;
using Micromind.Common.Interfaces;
using Micromind.Data;
using System;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;

namespace Micromind.Facade
{
	public sealed class CustomerSystem : MarshalByRefObject, ICustomerSystem, IDisposable
	{
		private Config config;

		public CustomerSystem(Config config)
		{
			this.config = config;
		}

		public void Dispose()
		{
		}

		public bool CreateCustomer(CustomerData data)
		{
			return new Customers(config).InsertUpdateCustomer(data, isUpdate: false);
		}

		public bool UpdateCustomer(CustomerData data)
		{
			return UpdateCustomer(data, checkConcurrency: false);
		}

		public bool UpdateCustomer(CustomerData data, bool checkConcurrency)
		{
			return new Customers(config).InsertUpdateCustomer(data, isUpdate: true);
		}

		public CustomerData GetCustomer()
		{
			using (Customers customers = new Customers(config))
			{
				return customers.GetCustomer();
			}
		}

		public bool DeleteCustomer(string groupID)
		{
			using (Customers customers = new Customers(config))
			{
				return customers.DeleteCustomer(groupID);
			}
		}

		public CustomerData GetCustomerByID(string id)
		{
			using (Customers customers = new Customers(config))
			{
				return customers.GetCustomerByID(id);
			}
		}

		public DataSet GetTransactionCustomerList()
		{
			using (Customers customers = new Customers(config))
			{
				return customers.GetTransactionCustomerList();
			}
		}

		public DataSet GetCustomerList()
		{
			using (Customers customers = new Customers(config))
			{
				return customers.GetCustomerList();
			}
		}

		public DataSet GetCustomerByFields(params string[] columns)
		{
			using (Customers customers = new Customers(config))
			{
				return customers.GetCustomerByFields(columns);
			}
		}

		public DataSet GetCustomerByFields(string[] ids, params string[] columns)
		{
			using (Customers customers = new Customers(config))
			{
				return customers.GetCustomerByFields(ids, columns);
			}
		}

		public DataSet GetCustomerByFields(string[] ids, bool isInactive, params string[] columns)
		{
			using (Customers customers = new Customers(config))
			{
				return customers.GetCustomerByFields(ids, isInactive, columns);
			}
		}

		public DataSet GetCustomerList(bool showInactive)
		{
			using (Customers customers = new Customers(config))
			{
				return customers.GetCustomerList(showInactive);
			}
		}

		public DataSet GetTenantList(bool showInactive)
		{
			using (Customers customers = new Customers(config))
			{
				return customers.GetTenantList(showInactive);
			}
		}

		public DataSet GetCustomerComboList()
		{
			using (Customers customers = new Customers(config))
			{
				return customers.GetCustomerComboList();
			}
		}

		public DataSet GetCustomerComboList(string id)
		{
			using (Customers customers = new Customers(config))
			{
				return customers.GetCustomerComboList(id);
			}
		}

		public DataSet GetTransactiondetails(string CustomerID)
		{
			using (Customers customers = new Customers(config))
			{
				return customers.GetTransactionDetails(CustomerID);
			}
		}

		public DataSet GetCustomerBalanceSummary(string fromCustomer, string toCustomer, string fromClass, string toClass, string fromGroup, string toGroup, string fromSalesperson, string toSalesperson, string fromCountry, string toCountry, string fromArea, string toArea, string fromSalespersonDivision, string toSalespersonDivision, string fromSalespersonGroup, string toSalespersonGroup, string fromSalespersonArea, string toSalespersonArea, string fromSalespersonCountry, string toSalespersonCountry, bool showZeroBalance, bool isFC, string customerIDs)
		{
			using (Customers customers = new Customers(config))
			{
				return customers.GetCustomerBalanceSummary(fromCustomer, toCustomer, fromClass, toClass, fromGroup, toGroup, fromSalesperson, toSalesperson, fromCountry, toCountry, fromArea, toArea, fromSalespersonDivision, toSalespersonDivision, fromSalespersonGroup, toSalespersonGroup, fromSalespersonArea, toSalespersonArea, fromSalespersonCountry, toSalespersonCountry, showZeroBalance, isFC, customerIDs);
			}
		}

		public DataSet GetCustomerBalanceDetailFCReport(DateTime from, DateTime to, string fromCustomer, string toCustomer, string fromClass, string toClass, string fromGroup, string toGroup, string fromArea, string toArea, string fromCountry, string toCountry, string fromJob, string toJob, bool showZeroBalance, string currencyID, string customerIDs)
		{
			using (Customers customers = new Customers(config))
			{
				return customers.GetCustomerBalanceDetailFCReport(from, to, fromCustomer, toCustomer, fromClass, toClass, fromGroup, toGroup, fromArea, toArea, fromCountry, toCountry, fromJob, toJob, showZeroBalance, currencyID, customerIDs);
			}
		}

		public DataSet GetCustomerBalanceDetailReport(DateTime from, DateTime to, string fromCustomer, string toCustomer, string fromClass, string toClass, string fromGroup, string toGroup, string fromArea, string toArea, string fromCountry, string toCountry, string fromJob, string toJob, bool showZeroBalance, string currencyID, string customerIDs)
		{
			using (Customers customers = new Customers(config))
			{
				return customers.GetCustomerBalanceDetailReport(from, to, fromCustomer, toCustomer, fromClass, toClass, fromGroup, toGroup, fromArea, toArea, fromCountry, toCountry, fromJob, toJob, showZeroBalance, currencyID, customerIDs);
			}
		}

		public DataSet GetSalesByCustomerDetailReport(DateTime from, DateTime to, string fromCustomer, string toCustomer, string fromClass, string toClass, string fromGroup, string toGroup, string fromArea, string toArea, string fromCountry, string toCountry, string customerIDs)
		{
			using (Customers customers = new Customers(config))
			{
				return customers.GetSalesByCustomerDetailReport(from, to, fromCustomer, toCustomer, fromClass, toClass, fromGroup, toGroup, fromArea, toArea, fromCountry, toCountry, customerIDs);
			}
		}

		public DataSet GetSalesByCustomerSummaryReport(DateTime from, DateTime to, string fromCustomer, string toCustomer, string fromClass, string toClass, string fromGroup, string toGroup, string fromArea, string toArea, string fromCountry, string toCountry, string customerIDs)
		{
			using (Customers customers = new Customers(config))
			{
				return customers.GetSalesByCustomerSummaryReport(from, to, fromCustomer, toCustomer, fromClass, toClass, fromGroup, toGroup, fromArea, toArea, fromCountry, toCountry, customerIDs);
			}
		}

		public DataSet GetSalesEnquiryDetailReport(DateTime fromDate, DateTime toDate, string fromItem, string toItem, string fromItemClass, string toItemClass, string fromItemCategory, string toItemCategory, string fromCustomer, string toCustomer, string fromCustomerClass, string toCustomerClass, string fromCustomerGroup, string toCustomerGroup, string fromCustomerArea, string toCustomerArea, string fromCustomerCountry, string toCustomerCountry, string fromSalesperson, string toSalesperson, string fromSalespersonDivision, string toSalespersonDivision, string fromSalespersonGroup, string toSalespersonGroup, string fromSalespersonArea, string toSalespersonArea, string fromSalespersonCountry, string toSalespersonCountry, string fromLocation, string toLocation, string customerIDs, string fromManufacturer, string toManufacturer, string fromStyle, string toStyle, string fromOrigin, string toOrigin)
		{
			using (Customers customers = new Customers(config))
			{
				return customers.GetSalesEnquiryDetailReport(fromDate, toDate, fromItem, toItem, fromItemClass, toItemClass, fromItemCategory, toItemCategory, fromCustomer, toCustomer, fromCustomerClass, toCustomerClass, fromCustomerGroup, toCustomerGroup, fromCustomerArea, toCustomerArea, fromCustomerCountry, toCustomerCountry, fromSalesperson, toSalesperson, fromSalespersonDivision, toSalespersonDivision, fromSalespersonGroup, toSalespersonGroup, fromSalespersonArea, toSalespersonArea, fromSalespersonCountry, toSalespersonCountry, fromLocation, toLocation, customerIDs, fromManufacturer, toManufacturer, fromStyle, toStyle, fromOrigin, toOrigin);
			}
		}

		public DataSet GetSalesEnquirySummaryReport(DateTime from, DateTime to, string fromCustomer, string toCustomer, string fromClass, string toClass, string fromGroup, string toGroup, string fromArea, string toArea, string fromCountry, string toCountry, string fromSalesperson, string toSalesperson, string fromSalespersonDivision, string toSalespersonDivision, string fromSalespersonGroup, string toSalespersonGroup, string fromSalespersonArea, string toSalespersonArea, string fromSalespersonCountry, string toSalespersonCountry, string customerIDs)
		{
			using (Customers customers = new Customers(config))
			{
				return customers.GetSalesEnquirySummaryReport(from, to, fromCustomer, toCustomer, fromClass, toClass, fromGroup, toGroup, fromArea, toArea, fromCountry, toCountry, fromSalesperson, toSalesperson, fromSalespersonDivision, toSalespersonDivision, fromSalespersonGroup, toSalespersonGroup, fromSalespersonArea, toSalespersonArea, fromSalespersonCountry, toSalespersonCountry, customerIDs);
			}
		}

		public DataSet GetSalesQuoteDetailReport(DateTime fromDate, DateTime toDate, string fromItem, string toItem, string fromItemClass, string toItemClass, string fromItemCategory, string toItemCategory, string fromCustomer, string toCustomer, string fromCustomerClass, string toCustomerClass, string fromCustomerGroup, string toCustomerGroup, string fromCustomerArea, string toCustomerArea, string fromCustomerCountry, string toCustomerCountry, string fromSalesperson, string toSalesperson, string fromSalespersonDivision, string toSalespersonDivision, string fromSalespersonGroup, string toSalespersonGroup, string fromSalespersonArea, string toSalespersonArea, string fromSalespersonCountry, string toSalespersonCountry, string fromLocation, string toLocation, string customerIDs)
		{
			using (Customers customers = new Customers(config))
			{
				return customers.GetSalesQuoteDetailReport(fromDate, toDate, fromItem, toItem, fromItemClass, toItemClass, fromItemCategory, toItemCategory, fromCustomer, toCustomer, fromCustomerClass, toCustomerClass, fromCustomerGroup, toCustomerGroup, fromCustomerArea, toCustomerArea, fromCustomerCountry, toCustomerCountry, fromSalesperson, toSalesperson, fromSalespersonDivision, toSalespersonDivision, fromSalespersonGroup, toSalespersonGroup, fromSalespersonArea, toSalespersonArea, fromSalespersonCountry, toSalespersonCountry, fromLocation, toLocation, customerIDs);
			}
		}

		public DataSet GetSalesQuoteSummaryReport(DateTime from, DateTime to, string fromCustomer, string toCustomer, string fromClass, string toClass, string fromGroup, string toGroup, string fromArea, string toArea, string fromCountry, string toCountry, string fromSalesperson, string toSalesperson, string fromSalespersonDivision, string toSalespersonDivision, string fromSalespersonGroup, string toSalespersonGroup, string fromSalespersonArea, string toSalespersonArea, string fromSalespersonCountry, string toSalespersonCountry, string customerIDs)
		{
			using (Customers customers = new Customers(config))
			{
				return customers.GetSalesQuoteSummaryReport(from, to, fromCustomer, toCustomer, fromClass, toClass, fromGroup, toGroup, fromArea, toArea, fromCountry, toCountry, fromSalesperson, toSalesperson, fromSalespersonDivision, toSalespersonDivision, fromSalespersonGroup, toSalespersonGroup, fromSalespersonArea, toSalespersonArea, fromSalespersonCountry, toSalespersonCountry, customerIDs);
			}
		}

		public DataSet GetSalesOrderDetailReport(DateTime fromDate, DateTime toDate, string fromItem, string toItem, string fromItemClass, string toItemClass, string fromItemCategory, string toItemCategory, string fromCustomer, string toCustomer, string fromCustomerClass, string toCustomerClass, string fromCustomerGroup, string toCustomerGroup, string fromCustomerArea, string toCustomerArea, string fromCustomerCountry, string toCustomerCountry, string fromSalesperson, string toSalesperson, string fromSalespersonDivision, string toSalespersonDivision, string fromSalespersonGroup, string toSalespersonGroup, string fromSalespersonArea, string toSalespersonArea, string fromSalespersonCountry, string toSalespersonCountry, string fromLocation, string toLocation, bool isExport, string customerIDs, string fromManufacturer, string toManufacturer, string fromStyle, string toStyle, string fromOrigin, string toOrigin)
		{
			using (Customers customers = new Customers(config))
			{
				return customers.GetSalesOrderDetailReport(fromDate, toDate, fromItem, toItem, fromItemClass, toItemClass, fromItemCategory, toItemCategory, fromCustomer, toCustomer, fromCustomerClass, toCustomerClass, fromCustomerGroup, toCustomerGroup, fromCustomerArea, toCustomerArea, fromCustomerCountry, toCustomerCountry, fromSalesperson, toSalesperson, fromSalespersonDivision, toSalespersonDivision, fromSalespersonGroup, toSalespersonGroup, fromSalespersonArea, toSalespersonArea, fromSalespersonCountry, toSalespersonCountry, fromLocation, toLocation, isExport, customerIDs, fromManufacturer, toManufacturer, fromStyle, toStyle, fromOrigin, toOrigin);
			}
		}

		public DataSet GetSalesOrderSummaryReport(DateTime from, DateTime to, string fromCustomer, string toCustomer, string fromClass, string toClass, string fromGroup, string toGroup, string fromArea, string toArea, string fromCountry, string toCountry, string fromSalesperson, string toSalesperson, string fromSalespersonDivision, string toSalespersonDivision, string fromSalespersonGroup, string toSalespersonGroup, string fromSalespersonArea, string toSalespersonArea, string fromSalespersonCountry, string toSalespersonCountry, bool isExport, string customerIDs)
		{
			using (Customers customers = new Customers(config))
			{
				return customers.GetSalesOrderSummaryReport(from, to, fromCustomer, toCustomer, fromClass, toClass, fromGroup, toGroup, fromArea, toArea, fromCountry, toCountry, fromSalesperson, toSalesperson, fromSalespersonDivision, toSalespersonDivision, fromSalespersonGroup, toSalespersonGroup, fromSalespersonArea, toSalespersonArea, fromSalespersonCountry, toSalespersonCountry, isExport, customerIDs);
			}
		}

		public DataSet GetSalesByCustomerGroupDetailReport(DateTime from, DateTime to, string fromGroup, string toGroup)
		{
			using (Customers customers = new Customers(config))
			{
				return customers.GetSalesByCustomerGroupDetailReport(from, to, fromGroup, toGroup);
			}
		}

		public DataSet GetProformaInvoiceDetailReport(DateTime fromDate, DateTime toDate, string fromItem, string toItem, string fromItemClass, string toItemClass, string fromItemCategory, string toItemCategory, string fromCustomer, string toCustomer, string fromCustomerClass, string toCustomerClass, string fromCustomerGroup, string toCustomerGroup, string fromCustomerArea, string toCustomerArea, string fromCustomerCountry, string toCustomerCountry, string fromSalesperson, string toSalesperson, string fromSalespersonDivision, string toSalespersonDivision, string fromSalespersonGroup, string toSalespersonGroup, string fromSalespersonArea, string toSalespersonArea, string fromSalespersonCountry, string toSalespersonCountry, string fromLocation, string toLocation, string fromManufacturer, string toManufacturer, string fromStyle, string toStyle, string fromOrigin, string toOrigin)
		{
			using (Customers customers = new Customers(config))
			{
				return customers.GetProformaInvoiceDetailReport(fromDate, toDate, fromItem, toItem, fromItemClass, toItemClass, fromItemCategory, toItemCategory, fromCustomer, toCustomer, fromCustomerClass, toCustomerClass, fromCustomerGroup, toCustomerGroup, fromCustomerArea, toCustomerArea, fromCustomerCountry, toCustomerCountry, fromSalesperson, toSalesperson, fromSalespersonDivision, toSalespersonDivision, fromSalespersonGroup, toSalespersonGroup, fromSalespersonArea, toSalespersonArea, fromSalespersonCountry, toSalespersonCountry, fromLocation, toLocation, fromManufacturer, toManufacturer, fromStyle, toStyle, fromOrigin, toOrigin);
			}
		}

		public DataSet GetProformaInvoiceSummaryReport(DateTime from, DateTime to, string fromCustomer, string toCustomer, string fromClass, string toClass, string fromGroup, string toGroup, string fromArea, string toArea, string fromCountry, string toCountry, string fromSalesperson, string toSalesperson, string fromSalespersonDivision, string toSalespersonDivision, string fromSalespersonGroup, string toSalespersonGroup, string fromSalespersonArea, string toSalespersonArea, string fromSalespersonCountry, string toSalespersonCountry)
		{
			using (Customers customers = new Customers(config))
			{
				return customers.GetProformaInvoiceSummaryReport(from, to, fromCustomer, toCustomer, fromClass, toClass, fromGroup, toGroup, fromArea, toArea, fromCountry, toCountry, fromSalesperson, toSalesperson, fromSalespersonDivision, toSalespersonDivision, fromSalespersonGroup, toSalespersonGroup, fromSalespersonArea, toSalespersonArea, fromSalespersonCountry, toSalespersonCountry);
			}
		}

		public DataSet GetDeliveryNoteDetailReport(DateTime fromDate, DateTime toDate, string fromItem, string toItem, string fromItemClass, string toItemClass, string fromItemCategory, string toItemCategory, string fromCustomer, string toCustomer, string fromCustomerClass, string toCustomerClass, string fromCustomerGroup, string toCustomerGroup, string fromCustomerArea, string toCustomerArea, string fromCustomerCountry, string toCustomerCountry, string fromSalesperson, string toSalesperson, string fromSalespersonDivision, string toSalespersonDivision, string fromSalespersonGroup, string toSalespersonGroup, string fromSalespersonArea, string toSalespersonArea, string fromSalespersonCountry, string toSalespersonCountry, string fromLocation, string toLocation, string fromManufacturer, string toManufacturer, string fromStyle, string toStyle, string fromOrigin, string toOrigin)
		{
			using (Customers customers = new Customers(config))
			{
				return customers.GetDeliveryNoteDetailReport(fromDate, toDate, fromItem, toItem, fromItemClass, toItemClass, fromItemCategory, toItemCategory, fromCustomer, toCustomer, fromCustomerClass, toCustomerClass, fromCustomerGroup, toCustomerGroup, fromCustomerArea, toCustomerArea, fromCustomerCountry, toCustomerCountry, fromSalesperson, toSalesperson, fromSalespersonDivision, toSalespersonDivision, fromSalespersonGroup, toSalespersonGroup, fromSalespersonArea, toSalespersonArea, fromSalespersonCountry, toSalespersonCountry, fromLocation, toLocation, fromManufacturer, toManufacturer, fromStyle, toStyle, fromOrigin, toOrigin);
			}
		}

		public DataSet GetDeliveryNoteSummaryReport(DateTime from, DateTime to, string fromCustomer, string toCustomer, string fromClass, string toClass, string fromGroup, string toGroup, string fromArea, string toArea, string fromCountry, string toCountry, string fromSalesperson, string toSalesperson, string fromSalespersonDivision, string toSalespersonDivision, string fromSalespersonGroup, string toSalespersonGroup, string fromSalespersonArea, string toSalespersonArea, string fromSalespersonCountry, string toSalespersonCountry)
		{
			using (Customers customers = new Customers(config))
			{
				return customers.GetDeliveryNoteSummaryReport(from, to, fromCustomer, toCustomer, fromClass, toClass, fromGroup, toGroup, fromArea, toArea, fromCountry, toCountry, fromSalesperson, toSalesperson, fromSalespersonDivision, toSalespersonDivision, fromSalespersonGroup, toSalespersonGroup, fromSalespersonArea, toSalespersonArea, fromSalespersonCountry, toSalespersonCountry);
			}
		}

		public DataSet GetSalesReceiptDetailReport(DateTime fromDate, DateTime toDate, string fromItem, string toItem, string fromItemClass, string toItemClass, string fromItemCategory, string toItemCategory, string fromBrand, string toBrand, string fromCustomer, string toCustomer, string fromCustomerClass, string toCustomerClass, string fromCustomerGroup, string toCustomerGroup, string fromCustomerArea, string toCustomerArea, string fromCustomerCountry, string toCustomerCountry, string fromSalesperson, string toSalesperson, string fromSalespersonDivision, string toSalespersonDivision, string fromSalespersonGroup, string toSalespersonGroup, string fromSalespersonArea, string toSalespersonArea, string fromSalespersonCountry, string toSalespersonCountry, string fromLocation, string toLocation, string customerIDs, string fromManufacturer, string toManufacturer, string fromStyle, string toStyle, string fromOrigin, string toOrigin)
		{
			using (Customers customers = new Customers(config))
			{
				return customers.GetSalesReceiptDetailReport(fromDate, toDate, fromItem, toItem, fromItemClass, toItemClass, fromItemCategory, toItemCategory, fromBrand, toBrand, fromCustomer, toCustomer, fromCustomerClass, toCustomerClass, fromCustomerGroup, toCustomerGroup, fromCustomerArea, toCustomerArea, fromCustomerCountry, toCustomerCountry, fromSalesperson, toSalesperson, fromSalespersonDivision, toSalespersonDivision, fromSalespersonGroup, toSalespersonGroup, fromSalespersonArea, toSalespersonArea, fromSalespersonCountry, toSalespersonCountry, fromLocation, toLocation, customerIDs, fromManufacturer, toManufacturer, fromStyle, toStyle, fromOrigin, toOrigin);
			}
		}

		public DataSet GetSalesReceiptSummaryReport(DateTime from, DateTime to, string fromCustomer, string toCustomer, string fromClass, string toClass, string fromGroup, string toGroup, string fromArea, string toArea, string fromCountry, string toCountry, string fromSalesperson, string toSalesperson, string fromSalespersonDivision, string toSalespersonDivision, string fromSalespersonGroup, string toSalespersonGroup, string fromSalespersonArea, string toSalespersonArea, string fromSalespersonCountry, string toSalespersonCountry, string fromLocation, string toLocation, string customerIDs)
		{
			using (Customers customers = new Customers(config))
			{
				return customers.GetSalesReceiptSummaryReport(from, to, fromCustomer, toCustomer, fromClass, toClass, fromGroup, toGroup, fromArea, toArea, fromCountry, toCountry, fromSalesperson, toSalesperson, fromSalespersonDivision, toSalespersonDivision, fromSalespersonGroup, toSalespersonGroup, fromSalespersonArea, toSalespersonArea, fromSalespersonCountry, toSalespersonCountry, fromLocation, toLocation, customerIDs);
			}
		}

		public DataSet GetSalesInvoiceDetailReport(DateTime fromDate, DateTime toDate, string fromItem, string toItem, string fromItemClass, string toItemClass, string fromItemCategory, string toItemCategory, string fromBrand, string toBrand, string fromCustomer, string toCustomer, string fromCustomerClass, string toCustomerClass, string fromCustomerGroup, string toCustomerGroup, string fromCustomerArea, string toCustomerArea, string fromCustomerCountry, string toCustomerCountry, string fromSalesperson, string toSalesperson, string fromSalespersonDivision, string toSalespersonDivision, string fromSalespersonGroup, string toSalespersonGroup, string fromSalespersonArea, string toSalespersonArea, string fromSalespersonCountry, string toSalespersonCountry, string fromLocation, string toLocation, string customerIDs)
		{
			using (Customers customers = new Customers(config))
			{
				return customers.GetSalesInvoiceDetailReport(fromDate, toDate, fromItem, toItem, fromItemClass, toItemClass, fromItemCategory, toItemCategory, fromBrand, toBrand, fromCustomer, toCustomer, fromCustomerClass, toCustomerClass, fromCustomerGroup, toCustomerGroup, fromCustomerArea, toCustomerArea, fromCustomerCountry, toCustomerCountry, fromSalesperson, toSalesperson, fromSalespersonDivision, toSalespersonDivision, fromSalespersonGroup, toSalespersonGroup, fromSalespersonArea, toSalespersonArea, fromSalespersonCountry, toSalespersonCountry, fromLocation, toLocation, customerIDs);
			}
		}

		public DataSet GetSalesInvoiceSummaryReport(DateTime from, DateTime to, string fromCustomer, string toCustomer, string fromClass, string toClass, string fromGroup, string toGroup, string fromArea, string toArea, string fromCountry, string toCountry, string fromSalesperson, string toSalesperson, string fromSalespersonDivision, string toSalespersonDivision, string fromSalespersonGroup, string toSalespersonGroup, string fromSalespersonArea, string toSalespersonArea, string fromSalespersonCountry, string toSalespersonCountry, string fromLocation, string toLocation, string customerIDs)
		{
			using (Customers customers = new Customers(config))
			{
				return customers.GetSalesInvoiceSummaryReport(from, to, fromCustomer, toCustomer, fromClass, toClass, fromGroup, toGroup, fromArea, toArea, fromCountry, toCountry, fromSalesperson, toSalesperson, fromSalespersonDivision, toSalespersonDivision, fromSalespersonGroup, toSalespersonGroup, fromSalespersonArea, toSalespersonArea, fromSalespersonCountry, toSalespersonCountry, fromLocation, toLocation, customerIDs);
			}
		}

		public DataSet GetSalesByCustomerGroupSummaryReport(DateTime from, DateTime to, string fromGroup, string toGroup)
		{
			using (Customers customers = new Customers(config))
			{
				return customers.GetSalesByCustomerGroupSummaryReport(from, to, fromGroup, toGroup);
			}
		}

		public DataSet GetCustomerDocumentAddress(string customerID, string addressField)
		{
			using (Customers customers = new Customers(config))
			{
				return customers.GetCustomerDocumentAddress(customerID, addressField);
			}
		}

		public bool IsOverCreditLimit(string customerID, string sysDocID, string voucherID, decimal amount, bool checkOpenDN, DateTime transactionDate)
		{
			using (Customers customers = new Customers(config))
			{
				return customers.IsOverCreditLimit(customerID, sysDocID, voucherID, amount, checkOpenDN, transactionDate);
			}
		}

		public DataSet GetCustomerListReport(string fromCustomer, string toCustomer, string fromClass, string toClass, string fromGroup, string toGroup, string fromArea, string toArea, string fromCountry, string toCountry, bool showInactive, string customerIDs)
		{
			using (Customers customers = new Customers(config))
			{
				return customers.GetCustomerListReport(fromCustomer, toCustomer, fromClass, toClass, fromGroup, toGroup, fromArea, toArea, fromCountry, toCountry, showInactive, customerIDs);
			}
		}

		public DataSet GetCustomerPrimaryContactListReport(string fromCustomer, string toCustomer, string fromClass, string toClass, string fromGroup, string toGroup, string fromArea, string toArea, string fromCountry, string toCountry, bool showInactive, string customerIDs)
		{
			using (Customers customers = new Customers(config))
			{
				return customers.GetCustomerPrimaryContactListReport(fromCustomer, toCustomer, fromClass, toClass, fromGroup, toGroup, fromArea, toArea, fromCountry, toCountry, showInactive, customerIDs);
			}
		}

		public DataSet GetCustomerProfileReport(string fromCustomer, string toCustomer, string fromClass, string toClass, string fromGroup, string toGroup, string fromArea, string toArea, string fromCountry, string toCountry, bool showInactive, string customerIDs)
		{
			using (Customers customers = new Customers(config))
			{
				return customers.GetCustomerProfileReport(fromCustomer, toCustomer, fromClass, toClass, fromGroup, toGroup, fromArea, toArea, fromCountry, toCountry, showInactive, customerIDs);
			}
		}

		public DataSet GetCustomerActivityReport(DateTime from, DateTime to, string fromCustomer, string toCustomer, string fromClass, string toClass, string fromGroup, string toGroup, string fromArea, string toArea, string fromCountry, string toCountry, string customerIDs)
		{
			using (Customers customers = new Customers(config))
			{
				return customers.GetCustomerActivityReport(from, to, fromCustomer, toCustomer, fromClass, toClass, fromGroup, toGroup, fromArea, toArea, fromCountry, toCountry, customerIDs);
			}
		}

		public DataSet GetCustomerTopList(DateTime from, DateTime to, int Top, string mode)
		{
			using (Customers customers = new Customers(config))
			{
				return customers.GetCustomerTopList(from, to, Top, mode);
			}
		}

		public DataSet GetProductTopList(DateTime from, DateTime to, int Top, string mode)
		{
			using (Customers customers = new Customers(config))
			{
				return customers.GetProductTopList(from, to, Top, mode);
			}
		}

		public DataSet RequestForCreditLimitReport(DateTime from, DateTime to, string fromCustomer, string toCustomer, string fromClass, string toClass, string fromGroup, string toGroup, string fromArea, string toArea, string fromCountry, string toCountry, string multipleCustomerIDs)
		{
			using (Customers customers = new Customers(config))
			{
				return customers.RequestForCreditLimitReport(from, to, fromCustomer, toCustomer, fromClass, toClass, fromGroup, toGroup, fromArea, toArea, fromCountry, toCountry, multipleCustomerIDs);
			}
		}

		public DataSet GetCustomerYearMonthPaymentReport(string fromCustomer, string toCustomer, string fromClass, string toClass, string fromGroup, string toGroup, string fromArea, string toArea, string fromCountry, string toCountry, DateTime fromYear, DateTime toYear, DateTime fromMonth, DateTime toMonth, DateTime LPfromDate, DateTime LPtodate, string customerIDs)
		{
			using (Customers customers = new Customers(config))
			{
				return customers.GetCustomerYearMonthPaymentReport(fromCustomer, toCustomer, fromClass, toClass, fromGroup, toGroup, fromArea, toArea, fromCountry, toCountry, fromYear, toYear, fromMonth, toMonth, LPfromDate, LPtodate, customerIDs);
			}
		}

		public DataSet GetCustomerStatement(DateTime from, DateTime to, string fromCustomer, string toCustomer, string fromClass, string toClass, string fromGroup, string toGroup, string fromArea, string toArea, string fromCountry, string toCountry, bool isFC, bool showZeroBalance, bool isConsolidated, bool includeHold, bool onlyEmailMethod, bool showopenInvoices, string customerIDs)
		{
			using (Customers customers = new Customers(config))
			{
				return customers.GetCustomerStatement(from, to, fromCustomer, toCustomer, fromClass, toClass, fromGroup, toGroup, fromArea, toArea, fromCountry, toCountry, isFC, showZeroBalance, isConsolidated, includeHold, onlyEmailMethod, showopenInvoices, customerIDs);
			}
		}

		public DataSet GetSecondCustomerStatement(DateTime from, DateTime to, string fromCustomer, string toCustomer, string fromClass, string toClass, string fromGroup, string toGroup, string fromArea, string toArea, string fromCountry, string toCountry, bool isFC, bool showZeroBalance, bool isConsolidated, bool includeHold, bool onlyEmailMethod, bool showopenInvoices, string customerIDs)
		{
			using (Customers customers = new Customers(config))
			{
				return customers.GetSecondCustomerStatement(from, to, fromCustomer, toCustomer, fromClass, toClass, fromGroup, toGroup, fromArea, toArea, fromCountry, toCountry, isFC, showZeroBalance, isConsolidated, includeHold, onlyEmailMethod, showopenInvoices, customerIDs);
			}
		}

		public DataSet GetCustomerAgingSummary(string fromCustomer, string toCustomer, string fromClass, string toClass, string fromGroup, string toGroup, string fromArea, string toArea, string fromCountry, string toCountry, DateTime asOfDate, bool showZeroBalance, string fromSalesperson, string toSalesperson, string fromSalespersonDivision, string toSalespersonDivision, string fromSalespersonGroup, string toSalespersonGroup, string fromSalespersonArea, string toSalespersonArea, string fromSalespersonCountry, string toSalespersonCountry, string customerIDs)
		{
			using (Customers customers = new Customers(config))
			{
				return customers.GetCustomerAgingSummary(fromCustomer, toCustomer, fromClass, toClass, fromGroup, toGroup, fromArea, toArea, fromCountry, toCountry, asOfDate, showZeroBalance, isFC: false, fromSalesperson, toSalesperson, fromSalespersonDivision, toSalespersonDivision, fromSalespersonGroup, toSalespersonGroup, fromSalespersonArea, toSalespersonArea, fromSalespersonCountry, toSalespersonCountry, customerIDs);
			}
		}

		public DataSet GetCustomerBalanceAmount(string customerCode)
		{
			using (Customers customers = new Customers(config))
			{
				return customers.GetCustomerBalanceAmount(customerCode);
			}
		}

		public DataSet GetPOSCustomerList()
		{
			using (Customers customers = new Customers(config))
			{
				return customers.GetPOSCustomerList();
			}
		}

		public DataSet GetTopCustomers(DateTime from, DateTime to, int count)
		{
			using (Customers customers = new Customers(config))
			{
				return customers.GetTopCustomers(from, to, count);
			}
		}

		public DataSet GetTopSalesperson(DateTime from, DateTime to, int count)
		{
			using (Customers customers = new Customers(config))
			{
				return customers.GetTopSalesperson(from, to, count);
			}
		}

		public DataSet GetMonthlySalesReport(DateTime from, DateTime to)
		{
			using (Customers customers = new Customers(config))
			{
				return customers.GetMonthlySalesReport(from, to);
			}
		}

		public DataSet GetTopInvoicesReport(DateTime from, DateTime to, int count)
		{
			using (Customers customers = new Customers(config))
			{
				return customers.GetTopInvoicesReport(from, to, count);
			}
		}

		public DataSet GetCustomerAgingBalanceList(bool showZeroBalance, bool isFC, bool inActive)
		{
			using (Customers customers = new Customers(config))
			{
				return customers.GetCustomerAgingBalanceList(showZeroBalance, isFC, inActive);
			}
		}

		public bool SetFlag(string customerID, byte flagID)
		{
			using (Customers customers = new Customers(config))
			{
				return customers.SetFlag(customerID, flagID);
			}
		}

		public DataSet GetSalesHistory(DateTime from, DateTime to)
		{
			using (Customers customers = new Customers(config))
			{
				return customers.GetSalesHistory(from, to);
			}
		}

		public DataSet GetCustomerOutstandingInvoicesReport(DateTime reportDate, string fromCustomer, string toCustomer, string fromClass, string toClass, string fromGroup, string toGroup, bool showZeroBalance, bool isFC, string customerIDs)
		{
			using (Customers customers = new Customers(config))
			{
				return customers.GetCustomerOutstandingInvoicesReport(reportDate, fromCustomer, toCustomer, fromClass, toClass, fromGroup, toGroup, showZeroBalance, isFC, customerIDs);
			}
		}

		public DataSet GetCustomerOutstandingInvoicesReport(DateTime reportDate, string fromCustomer, string toCustomer, string fromClass, string toClass, string fromGroup, string toGroup, string fromArea, string toArea, string fromCountry, string toCountry, string fromLocationID, string toLocationID, bool showZeroBalance, bool isFC, string customerIDs)
		{
			using (Customers customers = new Customers(config))
			{
				return customers.GetCustomerOutstandingInvoicesReport(reportDate, fromCustomer, toCustomer, fromClass, toClass, fromGroup, toGroup, fromArea, toArea, fromCountry, toCountry, fromLocationID, toLocationID, showZeroBalance, isFC, customerIDs);
			}
		}

		public DataSet GetCustomerDueReport(DateTime reportDate, string fromCustomer, string toCustomer, string fromClass, string toClass, string fromGroup, string toGroup, string fromArea, string toArea, string fromCountry, string toCountry, string fromLocationID, string toLocationID, bool showZeroBalance, bool isFC, string customerIDs, string strGroupBy)
		{
			using (Customers customers = new Customers(config))
			{
				return customers.GetCustomerDueReport(reportDate, fromCustomer, toCustomer, fromClass, toClass, fromGroup, toGroup, fromArea, toArea, fromCountry, toCountry, fromLocationID, toLocationID, showZeroBalance, isFC, customerIDs, strGroupBy);
			}
		}

		public DataSet GetCustomerOutstandingSummaryReport(DateTime reportDate, string fromCustomer, string toCustomer, string fromClass, string toClass, string fromGroup, string toGroup, string fromArea, string toArea, string fromCountry, string toCountry, string fromLocationID, string toLocationID, string fromSalesperson, string toSalesperson, string fromSalespersonDivision, string toSalespersonDivision, string fromSalespersonGroup, string toSalespersonGroup, string fromSalespersonArea, string toSalespersonArea, string fromSalespersonCountry, string toSalespersonCountry, bool showZeroBalance, bool isFC, string customerIDs)
		{
			using (Customers customers = new Customers(config))
			{
				return customers.GetCustomerOutstandingSummaryReport(reportDate, fromCustomer, toCustomer, fromClass, toClass, fromGroup, toGroup, fromArea, toArea, fromCountry, toCountry, fromLocationID, toLocationID, fromSalesperson, toSalesperson, fromSalespersonDivision, toSalespersonDivision, fromSalespersonGroup, toSalespersonGroup, fromSalespersonArea, toSalespersonArea, fromSalespersonCountry, toSalespersonCountry, showZeroBalance, isFC, customerIDs);
			}
		}

		public DataSet GetCustomerOutstandingInvoicesList(string fromCustomer, bool showZeroBalance, bool isFC)
		{
			using (Customers customers = new Customers(config))
			{
				return customers.GetCustomerOutstandingInvoicesList(fromCustomer, showZeroBalance, isFC);
			}
		}

		public DataSet GetCustomerAgingDetail(string fromCustomer, string toCustomer, string fromClass, string toClass, string fromGroup, string toGroup, string fromArea, string toArea, string fromCountry, string toCountry, DateTime asOfDate, bool showZeroBalance, string fromSalesperson, string toSalesperson, string fromSalespersonDivision, string toSalespersonDivision, string fromSalespersonGroup, string toSalespersonGroup, string fromSalespersonArea, string toSalespersonArea, string fromSalespersonCountry, string toSalespersonCountry, string customerIDs)
		{
			using (Customers customers = new Customers(config))
			{
				return customers.GetCustomerAgingDetail(fromCustomer, toCustomer, fromClass, toClass, fromGroup, toGroup, fromArea, toArea, fromCountry, toCountry, asOfDate, showZeroBalance, fromSalesperson, toSalesperson, fromSalespersonDivision, toSalespersonDivision, fromSalespersonGroup, toSalespersonGroup, fromSalespersonArea, toSalespersonArea, fromSalespersonCountry, toSalespersonCountry, customerIDs);
			}
		}

		public DataSet GetDailySalesReport(int year, int month)
		{
			using (Customers customers = new Customers(config))
			{
				return customers.GetDailySalesReport(year, month);
			}
		}

		public CustomerData GetCustomerChildren(string parentID)
		{
			return new Customers(config).GetCustomerParentChildRelation(parentID);
		}

		public bool UpdateCustomerParentChildRelation(CustomerData data)
		{
			return new Customers(config).UpdateCustomerParentChildRelation(data, isUpdate: true);
		}

		public bool DeleteCustomerChildren(string parentID)
		{
			return new Customers(config).DeleteCustomerParentChildRelation(parentID);
		}

		public bool UpdateDueDates(string fromCustomer, string toCustomer)
		{
			return new Customers(config).UpdateDueDates(fromCustomer, toCustomer);
		}

		public DataSet GetCustomerRelationshipList()
		{
			return new Customers(config).GetCustomerRelationshipList();
		}

		public DataSet GetCustomerSnapBalance(string customerID)
		{
			return new Customers(config).GetCustomerSnapBalance(customerID, DateTime.Now);
		}

		public DataSet GetInventorySalesDetail(DateTime from, DateTime to, string productID, bool includeNonSale)
		{
			return new Customers(config).GetInventorySalesDetail(from, to, productID, includeNonSale);
		}

		public DataSet GetInventorySalesDetailByCategory(DateTime from, DateTime to, string productID, bool includeNonSale)
		{
			return new Customers(config).GetInventorySalesDetailByCategory(from, to, productID, includeNonSale);
		}

		public DataSet GetInventorySalesDetailByCustomer(DateTime from, DateTime to, string customerID)
		{
			return new Customers(config).GetInventorySalesDetailByCustomer(from, to, customerID);
		}

		public DataSet GetCustomerSalesDetail(DateTime from, DateTime to, string customerID)
		{
			return new Customers(config).GetCustomerSalesDetail(from, to, customerID);
		}

		public bool HasBalance(string customerID)
		{
			return new Customers(config).HasBalance(customerID);
		}

		public DataSet GetInventorySalesItemDetail(string customerID, string ProductID, string LocationID)
		{
			return new Customers(config).GetInventorySalesItemDetail(customerID, ProductID, LocationID);
		}

		public DataSet GetInventorySalesItemDetail(string customerID, string ProductID, bool loadAll, string LocationID)
		{
			return new Customers(config).GetInventorySalesItemDetail(customerID, ProductID, loadAll, LocationID);
		}

		public DataSet GetSalesQuoteDetail(string customerID, string ProductID)
		{
			return new Customers(config).GetSalesQuoteDetail(customerID, ProductID);
		}

		public DataSet GetDNDetail(string customerID, string ProductID)
		{
			return new Customers(config).GetDNDetail(customerID, ProductID);
		}

		public DataSet GetCustomerOutstandingInvoicesDetailReport(DateTime reportDate, string fromCustomer, string toCustomer, string fromClass, string toClass, string fromGroup, string toGroup, string fromArea, string toArea, string fromCountry, string toCountry, string fromLocationID, string toLocationID, bool showZeroBalance, bool isFC, string customerIDs)
		{
			using (Customers customers = new Customers(config))
			{
				return customers.GetCustomerOutstandingInvoicesDetailReport(reportDate, fromCustomer, toCustomer, fromClass, toClass, fromGroup, toGroup, fromArea, toArea, fromCountry, toCountry, fromLocationID, toLocationID, showZeroBalance, isFC, customerIDs);
			}
		}

		public DataSet GetCustomerLeadsComboList()
		{
			using (Customers customers = new Customers(config))
			{
				return customers.GetCustomerLeadsComboList();
			}
		}

		public string GetCustomerAddressPrintFormat(string customerID, string addressID)
		{
			using (Customers customers = new Customers(config))
			{
				return customers.GetCustomerAddressPrintFormat(customerID, addressID);
			}
		}

		public DataSet GetSMSCustomerList()
		{
			using (Customers customers = new Customers(config))
			{
				return customers.GetSMSCustomerList();
			}
		}

		public DataSet GetCustomerMobileNo(string CustomerID)
		{
			using (Customers customers = new Customers(config))
			{
				return customers.GetCustomerMobileNo(CustomerID);
			}
		}

		public DataSet GetSalesReservedDetailReport(DateTime fromDate, DateTime toDate, string fromItem, string toItem, string fromItemClass, string toItemClass, string fromItemCategory, string toItemCategory, string fromCustomer, string toCustomer, string fromCustomerClass, string toCustomerClass, string fromCustomerGroup, string toCustomerGroup, string fromCustomerArea, string toCustomerArea, string fromCustomerCountry, string toCustomerCountry, string fromSalesperson, string toSalesperson, string fromSalespersonDivision, string toSalespersonDivision, string fromSalespersonGroup, string toSalespersonGroup, string fromSalespersonArea, string toSalespersonArea, string fromSalespersonCountry, string toSalespersonCountry, string fromLocation, string toLocation, bool isExport, string customerIDs, string fromManufacturer, string toManufacturer, string fromStyle, string toStyle, string fromOrigin, string toOrigin)
		{
			using (Customers customers = new Customers(config))
			{
				return customers.GetSalesReservedDetailReport(fromDate, toDate, fromItem, toItem, fromItemClass, toItemClass, fromItemCategory, toItemCategory, fromCustomer, toCustomer, fromCustomerClass, toCustomerClass, fromCustomerGroup, toCustomerGroup, fromCustomerArea, toCustomerArea, fromCustomerCountry, toCustomerCountry, fromSalesperson, toSalesperson, fromSalespersonDivision, toSalespersonDivision, fromSalespersonGroup, toSalespersonGroup, fromSalespersonArea, toSalespersonArea, fromSalespersonCountry, toSalespersonCountry, fromLocation, toLocation, isExport, customerIDs, fromManufacturer, toManufacturer, fromStyle, toStyle, fromOrigin, toOrigin);
			}
		}

		public DataSet GetMultipleInvoices(DateTime from, DateTime to, string fromCustomer, string toCustomer, string fromClass, string toClass, string fromGroup, string toGroup, string fromArea, string toArea, string fromCountry, string toCountry, string customerIDs, string sysDocID, string voucherIDs)
		{
			using (Customers customers = new Customers(config))
			{
				return customers.GetMultipleInvoices(from, to, fromCustomer, toCustomer, fromClass, toClass, fromGroup, toGroup, fromArea, toArea, fromCountry, toCountry, customerIDs, sysDocID, voucherIDs);
			}
		}

		public bool AddCustomerSignature(string customerID, byte[] image)
		{
			using (Customers customers = new Customers(config))
			{
				return customers.AddCustomerSignature(customerID, image);
			}
		}

		public bool RemoveCustomerSignature(string customerID)
		{
			using (Customers customers = new Customers(config))
			{
				return customers.RemoveCustomerSignature(customerID);
			}
		}

		private static bool ThumbnailImageAbort()
		{
			return true;
		}

		private byte[] CreateThumbnail(byte[] image, int width, int height)
		{
			if (image == null)
			{
				return null;
			}
			ImageCodecInfo encoder = GetEncoder(ImageFormat.Jpeg);
			MemoryStream memoryStream = new MemoryStream();
			memoryStream.Write(image, 0, image.Length);
			Bitmap bitmap = new Bitmap(memoryStream);
			int num = width;
			int num2 = height;
			int width2 = bitmap.Width;
			int height2 = bitmap.Height;
			float num3 = 0f;
			float num4 = 0f;
			float num5 = 0f;
			num4 = (float)num / (float)width2;
			num5 = (float)num2 / (float)height2;
			num3 = ((!(num5 < num4)) ? num4 : num5);
			checked
			{
				num = (int)((float)width2 * num3);
				num2 = (int)((float)height2 * num3);
				Image thumbnailImage = bitmap.GetThumbnailImage(num, num2, ThumbnailImageAbort, IntPtr.Zero);
				Encoder quality = Encoder.Quality;
				EncoderParameters encoderParameters = new EncoderParameters(1);
				EncoderParameter encoderParameter = new EncoderParameter(quality, 50L);
				encoderParameters.Param[0] = encoderParameter;
				memoryStream = new MemoryStream();
				thumbnailImage.Save(memoryStream, encoder, encoderParameters);
				return memoryStream.ToArray();
			}
		}

		private ImageCodecInfo GetEncoder(ImageFormat format)
		{
			ImageCodecInfo[] imageDecoders = ImageCodecInfo.GetImageDecoders();
			foreach (ImageCodecInfo imageCodecInfo in imageDecoders)
			{
				if (imageCodecInfo.FormatID == format.Guid)
				{
					return imageCodecInfo;
				}
			}
			return null;
		}

		public byte[] GetCustomerSignatureThumbnailImage(string customerID)
		{
			using (Customers customers = new Customers(config))
			{
				return customers.GetCustomerSignThumbnailImage(customerID);
			}
		}

		public DataSet GetSalesByCustomerSummaryReport(DateTime from, DateTime to, string fromCustomer, string toCustomer, string fromClass, string toClass, string fromGroup, string toGroup, string fromArea, string toArea, string fromCountry, string toCountry, string customerIDs, string classIDs, string groupIDs, string areaIDs, string countryIDs)
		{
			using (Customers customers = new Customers(config))
			{
				return customers.GetSalesByCustomerSummaryReport(from, to, fromCustomer, toCustomer, fromClass, toClass, fromGroup, toGroup, fromArea, toArea, fromCountry, toCountry, customerIDs, classIDs, groupIDs, areaIDs, countryIDs);
			}
		}

		public DataSet GetSalesByCustomerDetailReport(DateTime from, DateTime to, string fromCustomer, string toCustomer, string fromClass, string toClass, string fromGroup, string toGroup, string fromArea, string toArea, string fromCountry, string toCountry, string customerIDs, string classIDs, string groupIDs, string areaIDs, string countryIDs)
		{
			using (Customers customers = new Customers(config))
			{
				return customers.GetSalesByCustomerSummaryReport(from, to, fromCustomer, toCustomer, fromClass, toClass, fromGroup, toGroup, fromArea, toArea, fromCountry, toCountry, customerIDs, classIDs, groupIDs, areaIDs, countryIDs);
			}
		}
	}
}
