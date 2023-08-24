using Micromind.Common.Data;
using Micromind.Common.Interfaces;
using Micromind.Data;
using System;
using System.Data;

namespace Micromind.Facade
{
	public sealed class SalesReceiptSystem : MarshalByRefObject, ISalesReceiptSystem, IDisposable
	{
		private Config config;

		public SalesReceiptSystem(Config config)
		{
			this.config = config;
		}

		public void Dispose()
		{
		}

		public bool CreateSalesReceipt(SalesReceiptData data, bool isUpdate)
		{
			return new SalesReceipt(config).InsertUpdateSalesReceipt(data, isUpdate);
		}

		public SalesReceiptData GetSalesReceiptByID(string sysDocID, string voucherID)
		{
			return new SalesReceipt(config).GetSalesReceiptByID(sysDocID, voucherID);
		}

		public bool DeleteSalesReceipt(string sysDocID, string voucherID)
		{
			return new SalesReceipt(config).DeleteSalesReceipt(sysDocID, voucherID);
		}

		public bool VoidSalesReceipt(string sysDocID, string voucherID, bool isVoid)
		{
			return new SalesReceipt(config).VoidSalesReceipt(sysDocID, voucherID, isVoid);
		}

		public DataSet GetReceiptsForDelivery(string customerID)
		{
			return new SalesReceipt(config).GetReceiptsForDelivery(customerID);
		}

		public decimal GetProductSalesPrice(string productID, string customerID, string locationID, string UnitID)
		{
			using (SalesReceipt salesReceipt = new SalesReceipt(config))
			{
				return salesReceipt.GetProductSalesPrice(productID, customerID, locationID, UnitID);
			}
		}
	}
}
