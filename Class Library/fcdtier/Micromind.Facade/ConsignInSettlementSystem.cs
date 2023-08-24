using Micromind.Common.Data;
using Micromind.Common.Interfaces;
using Micromind.Data;
using System;
using System.Data;

namespace Micromind.Facade
{
	public sealed class ConsignInSettlementSystem : MarshalByRefObject, IConsignInSettlementSystem, IDisposable
	{
		private Config config;

		public ConsignInSettlementSystem(Config config)
		{
			this.config = config;
		}

		public void Dispose()
		{
		}

		public bool CreateSettlement(ConsignInSettlementData data, bool isUpdate)
		{
			return new ConsignInSettlement(config).InsertUpdateSettlement(data, isUpdate);
		}

		public ConsignInSettlementData GetSettlementByID(string sysDocID, string voucherID)
		{
			return new ConsignInSettlement(config).GetSettlementByID(sysDocID, voucherID);
		}

		public bool DeleteSettlement(string sysDocID, string voucherID)
		{
			return new ConsignInSettlement(config).DeleteSettlement(sysDocID, voucherID);
		}

		public bool VoidSettlement(string sysDocID, string voucherID, bool isVoid)
		{
			return new ConsignInSettlement(config).VoidSettlement(sysDocID, voucherID, isVoid);
		}

		public DataSet GetInvoicesForDelivery(string customerID)
		{
			return new ConsignInSettlement(config).GetInvoicesForDelivery(customerID);
		}

		public bool IsBelowMinPrice(string productID, string unitID, string currencyID, decimal currencyRate, decimal price)
		{
			return new ConsignInSettlement(config).IsBelowMinPrice(productID, unitID, currencyID, currencyRate, price);
		}

		public DataSet GetSettlementToPrint(string sysDocID, string[] voucherID)
		{
			return new ConsignInSettlement(config).GetSettlementToPrint(sysDocID, voucherID);
		}

		public DataSet GetSettlementToPrint(string sysDocID, string voucherID)
		{
			return new ConsignInSettlement(config).GetSettlementToPrint(sysDocID, voucherID);
		}

		public DataSet GetList(DateTime from, DateTime to, bool showVoid)
		{
			return new ConsignInSettlement(config).GetList(from, to, showVoid);
		}

		public DataSet GetConsignInSoldItems(string sysDocID, string voucherID)
		{
			return new ConsignInSettlement(config).GetConsignInSoldItems(sysDocID, voucherID);
		}

		public DataSet GetConsignInReceiptReport(DateTime fromrec, DateTime torec, DateTime fromset, DateTime toset, string fromVendor, string toVendor, string sysDocID, string voucherID, string vendorIDs)
		{
			return new ConsignInSettlement(config).GetConsignInReceiptReport(fromrec, torec, fromrec, torec, fromVendor, toVendor, sysDocID, voucherID, vendorIDs);
		}
	}
}
