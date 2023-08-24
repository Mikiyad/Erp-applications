using Micromind.Common.Data;
using Micromind.Common.Interfaces;
using Micromind.Data;
using System;
using System.Data;

namespace Micromind.Facade
{
	public sealed class PurchaseReturnSystem : MarshalByRefObject, IPurchaseReturnSystem, IDisposable
	{
		private Config config;

		public PurchaseReturnSystem(Config config)
		{
			this.config = config;
		}

		public void Dispose()
		{
		}

		public bool CreatePurchaseReturn(PurchaseReturnData data, bool isUpdate)
		{
			return new PurchaseReturn(config).InsertUpdatePurchaseReturn(data, isUpdate);
		}

		public PurchaseReturnData GetPurchaseReturnByID(string sysDocID, string voucherID)
		{
			return new PurchaseReturn(config).GetPurchaseReturnByID(sysDocID, voucherID);
		}

		public bool DeletePurchaseReturn(string sysDocID, string voucherID)
		{
			return new PurchaseReturn(config).DeletePurchaseReturn(sysDocID, voucherID);
		}

		public bool VoidPurchaseReturn(string sysDocID, string voucherID, bool isVoid)
		{
			return new PurchaseReturn(config).VoidPurchaseReturn(sysDocID, voucherID, isVoid);
		}

		public DataSet GetPurchaseReturnToPrint(string sysDocID, string[] voucherID)
		{
			return new PurchaseReturn(config).GetPurchaseReturnToPrint(sysDocID, voucherID);
		}

		public DataSet GetPurchaseReturnToPrint(string sysDocID, string voucherID)
		{
			return new PurchaseReturn(config).GetPurchaseReturnToPrint(sysDocID, new string[1]
			{
				voucherID
			});
		}

		public DataSet GetList(DateTime from, DateTime to, bool showVoid)
		{
			return new PurchaseReturn(config).GetList(from, to, showVoid);
		}

		public DataSet GetInvoicesToReturn(string customerID, bool cashOnly, DateTime fromDate, DateTime toDate)
		{
			return new PurchaseReturn(config).GetInvoicesToReturn(customerID, cashOnly, fromDate, toDate);
		}
	}
}
