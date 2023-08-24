using Micromind.Common.Data;
using Micromind.Common.Interfaces;
using Micromind.Data;
using System;
using System.Data;

namespace Micromind.Facade
{
	public sealed class SalesPOSSystem : MarshalByRefObject, ISalesPOSSystem, IDisposable
	{
		private Config config;

		public SalesPOSSystem(Config config)
		{
			this.config = config;
		}

		public void Dispose()
		{
		}

		public bool CreateSalesPOS(SalesPOSData data, bool isUpdate)
		{
			return new SalesPOS(config).InsertUpdateSalesPOS(data, isUpdate);
		}

		public SalesPOSData GetSalesPOSByID(string sysDocID, string voucherID)
		{
			return new SalesPOS(config).GetSalesPOSByID(sysDocID, voucherID);
		}

		public bool DeleteSalesPOS(string sysDocID, string voucherID)
		{
			return new SalesPOS(config).DeleteSalesPOS(sysDocID, voucherID);
		}

		public bool VoidSalesPOS(string sysDocID, string voucherID, bool isVoid)
		{
			return new SalesPOS(config).VoidSalesPOS(sysDocID, voucherID, isVoid);
		}

		public DataSet GetInvoicesForDelivery(string customerID)
		{
			return new SalesPOS(config).GetInvoicesForDelivery(customerID);
		}

		public bool IsBelowMinPrice(string productID, string unitID, string currencyID, decimal currencyRate, decimal price)
		{
			return new SalesPOS(config).IsBelowMinPrice(productID, unitID, currencyID, currencyRate, price);
		}

		public DataSet GetSalesPOSToPrint(string sysDocID, string[] voucherID)
		{
			return new SalesPOS(config).GetSalesPOSToPrint(sysDocID, voucherID);
		}

		public DataSet GetSalesPOSToPrint(string sysDocID, string voucherID)
		{
			return new SalesPOS(config).GetSalesPOSToPrint(sysDocID, voucherID);
		}

		public DataSet GetList(DateTime from, DateTime to, bool showVoid)
		{
			return new SalesPOS(config).GetList(from, to, showVoid);
		}

		public DataSet GetSalesReceiptLookupList(string sysDocID, DateTime from, DateTime to)
		{
			return new SalesPOS(config).GetSalesReceiptLookupList(sysDocID, from, to);
		}

		public DataSet GetPOSXReport(DateTime from, DateTime to, int shifID, int batchID, string registerID)
		{
			return new SalesPOS(config).GetPOSXReport(from, to, shifID, batchID, registerID);
		}

		public DataSet GetPOSYReport(DateTime from, DateTime to, int shifID, int batchID, string registerID)
		{
			return new SalesPOS(config).GetPOSYReport(from, to, shifID, batchID, registerID);
		}

		public bool ReCostTransaction(string sysDocID, string voucherID)
		{
			return new SalesPOS(config).ReCostTransaction(sysDocID, voucherID);
		}

		public DataSet GetPOSZReport(DateTime from, DateTime to, int shifID, int batchID, string registerID)
		{
			return new SalesPOS(config).GetPOSZReport(from, to, shifID, batchID, registerID);
		}
	}
}
