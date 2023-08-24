using Micromind.Common.Data;
using Micromind.Common.Interfaces;
using Micromind.Data;
using System;
using System.Data;

namespace Micromind.Facade
{
	public sealed class SalesInvoiceNISystem : MarshalByRefObject, ISalesInvoiceNISystem, IDisposable
	{
		private Config config;

		public SalesInvoiceNISystem(Config config)
		{
			this.config = config;
		}

		public void Dispose()
		{
		}

		public bool CreateSalesInvoice(SalesInvoiceNIData data, bool isUpdate)
		{
			return new SalesInvoiceNI(config).InsertUpdateSalesInvoice(data, isUpdate);
		}

		public SalesInvoiceNIData GetSalesInvoiceByID(string sysDocID, string voucherID)
		{
			return new SalesInvoiceNI(config).GetSalesInvoiceByID(sysDocID, voucherID);
		}

		public SalesInvoiceNIData GetServiceInvoiceByID(string sysDocID, string voucherID)
		{
			return new SalesInvoiceNI(config).GetServiceInvoiceByID(sysDocID, voucherID);
		}

		public bool DeleteSalesInvoice(string sysDocID, string voucherID)
		{
			return new SalesInvoiceNI(config).DeleteSalesInvoice(sysDocID, voucherID);
		}

		public bool VoidSalesInvoice(string sysDocID, string voucherID, bool isVoid)
		{
			return new SalesInvoiceNI(config).VoidSalesInvoice(sysDocID, voucherID, isVoid);
		}

		public bool IsBelowMinPrice(string productID, string unitID, string currencyID, decimal currencyRate, decimal price, string locationID)
		{
			return new SalesInvoiceNI(config).IsBelowMinPrice(productID, unitID, currencyID, currencyRate, price, locationID);
		}

		public DataSet GetSalesInvoiceToPrint(string sysDocID, string[] voucherID, bool mergeMatrixItems, bool showLotDetail, NonInventoryInvoiceType invoiceType)
		{
			return new SalesInvoiceNI(config).GetSalesInvoiceToPrint(sysDocID, voucherID, mergeMatrixItems, showLotDetail, invoiceType);
		}

		public DataSet GetSalesInvoiceToPrint(string sysDocID, string voucherID, bool mergeMatrixItems, bool showLotDetail, NonInventoryInvoiceType invoiceType)
		{
			return new SalesInvoiceNI(config).GetSalesInvoiceToPrint(sysDocID, voucherID, mergeMatrixItems, showLotDetail, invoiceType);
		}

		public DataSet GetList(DateTime from, DateTime to, bool isExport, bool showVoid, string sysDocID, bool isCash, NonInventoryInvoiceType invoiceType)
		{
			return new SalesInvoiceNI(config).GetList(from, to, isExport, showVoid, sysDocID, isCash, invoiceType);
		}

		public bool InvoiceHasShippedQuantity(string sysDocID, string voucherNumber)
		{
			return new SalesInvoiceNI(config).InvoiceHasShippedQuantity(sysDocID, voucherNumber, null);
		}

		public bool IsBelowAverageCost(string productID, string unitID, string currencyID, decimal currencyRate, decimal price)
		{
			return new SalesInvoiceNI(config).IsBelowAverageCost(productID, unitID, currencyID, currencyRate, price);
		}

		public bool ModifyTransactions(string sysDocID, string voucherNumber, string userID, bool isModify, string toUpdate)
		{
			return new SalesInvoiceNI(config).ModifyTransactions(sysDocID, voucherNumber, userID, isModify, toUpdate);
		}

		public bool IsBelowMinAllowedPrice(string productID, string unitID, string currencyID, decimal currencyRate, decimal price, string locationID)
		{
			return new SalesInvoice(config).IsBelowMinAllowedPrice(productID, unitID, currencyID, currencyRate, price, locationID);
		}

		public bool AllowModify(string sysDocID, string voucherNumber)
		{
			return new SalesInvoiceNI(config).AllowModify(sysDocID, voucherNumber);
		}
	}
}
