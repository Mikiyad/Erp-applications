using Micromind.Common.Data;
using Micromind.Common.Interfaces;
using Micromind.Data;
using System;
using System.Data;

namespace Micromind.Facade
{
	public sealed class POSHoldSystem : MarshalByRefObject, IPOSHoldSystem, IDisposable
	{
		private Config config;

		public POSHoldSystem(Config config)
		{
			this.config = config;
		}

		public void Dispose()
		{
		}

		public bool HoldSalesReceipt(SalesPOSData data, bool isUpdate)
		{
			return new POSHold(config).InsertUpdatePOSHold(data, isUpdate);
		}

		public POSHoldData GetPOSHoldByID(string sysDocID, string voucherID)
		{
			return new POSHold(config).GetPOSHoldByID(sysDocID, voucherID);
		}

		public bool DeletePOSHold(string voucherID)
		{
			return new POSHold(config).DeletePOSHold(voucherID);
		}

		public bool VoidPOSHold(string sysDocID, string voucherID, bool isVoid)
		{
			return false;
		}

		public DataSet GetInvoicesForDelivery(string customerID)
		{
			return new POSHold(config).GetInvoicesForDelivery(customerID);
		}

		public bool IsBelowMinPrice(string productID, string unitID, string currencyID, decimal currencyRate, decimal price)
		{
			return new POSHold(config).IsBelowMinPrice(productID, unitID, currencyID, currencyRate, price);
		}

		public DataSet GetPOSHoldToPrint(string sysDocID, string[] voucherID)
		{
			return new POSHold(config).GetPOSHoldToPrint(sysDocID, voucherID);
		}

		public DataSet GetPOSHoldToPrint(string sysDocID, string voucherID)
		{
			return new POSHold(config).GetPOSHoldToPrint(sysDocID, voucherID);
		}

		public DataSet GetHoldDocumentList(string registerID)
		{
			return new POSHold(config).GetHoldDocumentList(registerID);
		}

		public bool SetSearchValue(string sysDocID, string voucherID, string searchValue)
		{
			return new POSHold(config).SetSearchValue(sysDocID, voucherID, searchValue);
		}
	}
}
