using Micromind.Common.Data;
using Micromind.Common.Interfaces;
using Micromind.Data;
using System;
using System.Data;

namespace Micromind.Facade
{
	public sealed class ConsignOutSettlementSystem : MarshalByRefObject, IConsignOutSettlementSystem, IDisposable
	{
		private Config config;

		public ConsignOutSettlementSystem(Config config)
		{
			this.config = config;
		}

		public void Dispose()
		{
		}

		public bool CreateSettlement(ConsignOutSettlementData data, bool isUpdate)
		{
			return new ConsignOutSettlement(config).InsertUpdateSettlement(data, isUpdate);
		}

		public bool ReCostTransaction(string sysDocID, string voucherID)
		{
			return new ConsignOutSettlement(config).ReCostTransaction(sysDocID, voucherID);
		}

		public ConsignOutSettlementData GetSettlementByID(string sysDocID, string voucherID)
		{
			return new ConsignOutSettlement(config).GetSettlementByID(sysDocID, voucherID);
		}

		public bool DeleteSettlement(string sysDocID, string voucherID)
		{
			return new ConsignOutSettlement(config).DeleteSettlement(sysDocID, voucherID);
		}

		public bool VoidSettlement(string sysDocID, string voucherID, bool isVoid)
		{
			return new ConsignOutSettlement(config).VoidSettlement(sysDocID, voucherID, isVoid);
		}

		public DataSet GetInvoicesForDelivery(string customerID)
		{
			return new ConsignOutSettlement(config).GetInvoicesForDelivery(customerID);
		}

		public bool IsBelowMinPrice(string productID, string unitID, string currencyID, decimal currencyRate, decimal price)
		{
			return new ConsignOutSettlement(config).IsBelowMinPrice(productID, unitID, currencyID, currencyRate, price);
		}

		public DataSet GetSettlementToPrint(string sysDocID, string[] voucherID)
		{
			return new ConsignOutSettlement(config).GetSettlementToPrint(sysDocID, voucherID);
		}

		public DataSet GetSettlementToPrint(string sysDocID, string voucherID)
		{
			return new ConsignOutSettlement(config).GetSettlementToPrint(sysDocID, voucherID);
		}

		public DataSet GetList(DateTime from, DateTime to, bool showVoid)
		{
			return new ConsignOutSettlement(config).GetList(from, to, showVoid);
		}
	}
}
