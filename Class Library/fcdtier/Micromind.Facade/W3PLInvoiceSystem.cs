using Micromind.Common.Data;
using Micromind.Common.Interfaces;
using Micromind.Data;
using System;
using System.Data;

namespace Micromind.Facade
{
	public sealed class W3PLInvoiceSystem : MarshalByRefObject, IW3PLInvoiceSystem, IDisposable
	{
		private Config config;

		public W3PLInvoiceSystem(Config config)
		{
			this.config = config;
		}

		public void Dispose()
		{
		}

		public bool CreateInvoice(W3PLInvoiceData data, bool isUpdate)
		{
			return new W3PLInvoice(config).InsertUpdateInvoice(data, isUpdate);
		}

		public W3PLInvoiceData GetInvoiceByID(string sysDocID, string voucherID)
		{
			return new W3PLInvoice(config).GetInvoiceByID(sysDocID, voucherID);
		}

		public bool DeleteInvoice(string sysDocID, string voucherID)
		{
			return new W3PLInvoice(config).DeleteInvoice(sysDocID, voucherID);
		}

		public bool VoidInvoice(string sysDocID, string voucherID, bool isVoid)
		{
			return new W3PLInvoice(config).VoidInvoice(sysDocID, voucherID, isVoid);
		}

		public DataSet GetInvoicesForDelivery(string customerID)
		{
			return new W3PLInvoice(config).GetInvoicesForDelivery(customerID);
		}

		public bool IsBelowMinPrice(string productID, string unitID, string currencyID, decimal currencyRate, decimal price)
		{
			return new W3PLInvoice(config).IsBelowMinPrice(productID, unitID, currencyID, currencyRate, price);
		}

		public DataSet GetInvoiceToPrint(string sysDocID, string[] voucherID)
		{
			return new W3PLInvoice(config).GetInvoiceToPrint(sysDocID, voucherID);
		}

		public DataSet GetInvoiceToPrint(string sysDocID, string voucherID)
		{
			return new W3PLInvoice(config).GetInvoiceToPrint(sysDocID, voucherID);
		}

		public DataSet GetList(DateTime from, DateTime to, bool showVoid)
		{
			return new W3PLInvoice(config).GetList(from, to, showVoid);
		}

		public DataSet GetConsignInSoldItems(string sysDocID, string voucherID)
		{
			return new W3PLInvoice(config).GetConsignInSoldItems(sysDocID, voucherID);
		}

		public DataSet GetConsignInReceiptReport(DateTime fromrec, DateTime torec, DateTime fromset, DateTime toset, string fromVendor, string toVendor, string sysDocID, string voucherID)
		{
			return new W3PLInvoice(config).GetConsignInReceiptReport(fromrec, torec, fromrec, torec, fromVendor, toVendor, sysDocID, voucherID);
		}
	}
}
