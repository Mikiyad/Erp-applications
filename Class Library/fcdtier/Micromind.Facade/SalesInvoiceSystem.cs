using Micromind.Common.Data;
using Micromind.Common.Interfaces;
using Micromind.Data;
using System;
using System.Data;

namespace Micromind.Facade
{
	public sealed class SalesInvoiceSystem : MarshalByRefObject, ISalesInvoiceSystem, IDisposable
	{
		private Config config;

		public SalesInvoiceSystem(Config config)
		{
			this.config = config;
		}

		public void Dispose()
		{
		}

		public bool CreateSalesInvoice(SalesInvoiceData data, bool isUpdate, bool TempSave = false)
		{
			return new SalesInvoice(config).InsertUpdateSalesInvoice(data, isUpdate, TempSave);
		}

		public SalesInvoiceData GetSalesInvoiceByID(string sysDocID, string voucherID)
		{
			return new SalesInvoice(config).GetSalesInvoiceByID(sysDocID, voucherID);
		}

		public bool ReCostTransaction(string sysDocID, string voucherID)
		{
			return new SalesInvoice(config).ReCostTransaction(sysDocID, voucherID);
		}

		public bool DeleteSalesInvoice(string sysDocID, string voucherID)
		{
			return new SalesInvoice(config).DeleteSalesInvoice(sysDocID, voucherID);
		}

		public bool VoidSalesInvoice(string sysDocID, string voucherID, bool isVoid)
		{
			return new SalesInvoice(config).VoidSalesInvoice(sysDocID, voucherID, isVoid);
		}

		public DataSet GetInvoicesForDelivery(string customerID, bool isExport)
		{
			return new SalesInvoice(config).GetInvoicesForDelivery(customerID, isExport);
		}

		public DataSet GetInvoicesForDeliveryOnLocation(string customerID, bool isExport, string locationID)
		{
			return new SalesInvoice(config).GetInvoicesForDeliveryOnLocation(customerID, isExport, locationID);
		}

		public bool IsBelowMinPrice(string productID, string unitID, string currencyID, decimal currencyRate, decimal price, string locationID)
		{
			return new SalesInvoice(config).IsBelowMinPrice(productID, unitID, currencyID, currencyRate, price, locationID);
		}

		public DataSet GetSalesInvoiceToPrint(string sysDocID, string[] voucherID, bool mergeMatrixItems, bool showLotDetail)
		{
			return new SalesInvoice(config).GetSalesInvoiceToPrint(sysDocID, voucherID, mergeMatrixItems, showLotDetail);
		}

		public DataSet GetSalesInvoiceToPrint(string sysDocID, string voucherID, bool mergeMatrixItems, bool showLotDetail)
		{
			return new SalesInvoice(config).GetSalesInvoiceToPrint(sysDocID, voucherID, mergeMatrixItems, showLotDetail);
		}

		public DataSet GetList(DateTime from, DateTime to, bool isExport, bool showVoid, string sysDocID, bool isCash)
		{
			return new SalesInvoice(config).GetList(from, to, isExport, showVoid, sysDocID, isCash);
		}

		public bool InvoiceHasShippedQuantity(string sysDocID, string voucherNumber)
		{
			return new SalesInvoice(config).InvoiceHasShippedQuantity(sysDocID, voucherNumber, null);
		}

		public bool IsBelowAverageCost(string productID, string unitID, string currencyID, decimal currencyRate, decimal price)
		{
			return new SalesInvoice(config).IsBelowAverageCost(productID, unitID, currencyID, currencyRate, price);
		}

		public bool ModifyTransactions(string sysDocID, string voucherNumber, string userID, bool isModify, string toUpdate)
		{
			return new SalesInvoice(config).ModifyTransactions(sysDocID, voucherNumber, userID, isModify, toUpdate, null);
		}

		public bool IsBelowMinAllowedPrice(string productID, string unitID, string currencyID, decimal currencyRate, decimal price, string locationID)
		{
			return new SalesInvoice(config).IsBelowMinAllowedPrice(productID, unitID, currencyID, currencyRate, price, locationID);
		}

		public bool AllowModify(string sysDocID, string voucherNumber)
		{
			return new SalesInvoice(config).AllowModify(sysDocID, voucherNumber);
		}

		public DataSet GetPaymentAllocationDetails(string sysDocID, string voucherID)
		{
			return new SalesInvoice(config).GetPaymentAllocationDetails(sysDocID, voucherID);
		}

		public DataSet GetInvoicesForBillDiscountOnLocation(bool isExport, DateTime from, DateTime to)
		{
			return new SalesInvoice(config).GetInvoicesForBillDiscountOnLocation(isExport, from, to);
		}

		public DataSet GetSalesTransactionsbyID(string sysDocID, string voucherID)
		{
			return new SalesInvoice(config).GetSalesTransactionsbyID(sysDocID, voucherID);
		}
	}
}
