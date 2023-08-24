using Micromind.Common.Data;
using Micromind.Common.Interfaces;
using Micromind.Data;
using System;
using System.Data;

namespace Micromind.Facade
{
	public sealed class PurchaseClaimSystem : MarshalByRefObject, IPurchaseClaimSystem, IDisposable
	{
		private Config config;

		public PurchaseClaimSystem(Config config)
		{
			this.config = config;
		}

		public void Dispose()
		{
		}

		public bool DeletePurchaseClaim(string sysDocID, string voucherID)
		{
			return new PurchaseClaim(config).DeletePurchaseClaim(sysDocID, voucherID);
		}

		public bool VoidPurchaseClaim(string sysDocID, string voucherID, bool isVoid)
		{
			return new PurchaseClaim(config).VoidPurchaseClaim(sysDocID, voucherID, isVoid);
		}

		public bool CreatePurchaseClaim(PurchaseClaimData data, bool isUpdate)
		{
			return new PurchaseClaim(config).InsertUpdatePurchaseClaim(data, isUpdate);
		}

		public PurchaseClaimData GetPurchaseClaimByID(string sysDocID, string voucherID)
		{
			return new PurchaseClaim(config).GetPurchaseClaimByID(sysDocID, voucherID);
		}

		public DataSet GetPurchaseClaimToPrint(string sysDocID, string voucherID)
		{
			return new PurchaseClaim(config).GetPurchaseClaimToPrint(sysDocID, voucherID);
		}

		public DataSet GetPurchaseClaimToPrint(string sysDocID, string[] voucherID)
		{
			return new PurchaseClaim(config).GetPurchaseClaimToPrint(sysDocID, voucherID);
		}

		public DataSet GetList(DateTime from, DateTime to, bool showVoid)
		{
			return new PurchaseClaim(config).GetList(from, to, showVoid);
		}

		public bool OrderHasShippedQuantity(string sysDocID, string voucherNumber)
		{
			return new PurchaseClaim(config).OrderHasShippedQuantity(sysDocID, voucherNumber, null);
		}

		public DataSet GetPurchaseClaimToPrintTR(string sysDocID, string voucherID)
		{
			return new PurchaseClaim(config).GetPurchaseClaimToPrintTR(sysDocID, voucherID);
		}

		public DataSet GetOpenPurchaseClaims(string vendorID)
		{
			return new PurchaseClaim(config).GetOpenPurchaseClaims(vendorID);
		}
	}
}
