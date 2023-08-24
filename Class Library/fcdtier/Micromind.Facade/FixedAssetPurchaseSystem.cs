using Micromind.Common.Data;
using Micromind.Common.Interfaces;
using Micromind.Data;
using System;
using System.Data;

namespace Micromind.Facade
{
	public sealed class FixedAssetPurchaseSystem : MarshalByRefObject, IFixedAssetPurchaseSystem, IDisposable
	{
		private Config config;

		public FixedAssetPurchaseSystem(Config config)
		{
			this.config = config;
		}

		public void Dispose()
		{
		}

		public bool CreateFixedAssetPurchase(FixedAssetPurchaseData data, bool isUpdate)
		{
			return new FixedAssetPurchase(config).InsertUpdateFixedAssetPurchase(data, isUpdate);
		}

		public FixedAssetPurchaseData GetFixedAssetPurchaseByID(string sysDocID, string voucherID)
		{
			return new FixedAssetPurchase(config).GetFixedAssetPurchaseByID(sysDocID, voucherID);
		}

		public bool DeleteFixedAssetPurchase(string sysDocID, string voucherID)
		{
			return new FixedAssetPurchase(config).DeleteFixedAssetPurchase(sysDocID, voucherID);
		}

		public bool VoidFixedAssetPurchase(string sysDocID, string voucherID, bool isVoid)
		{
			return new FixedAssetPurchase(config).VoidFixedAssetPurchase(sysDocID, voucherID, isVoid);
		}

		public DataSet GetFixedAssetPurchaseToPrint(string sysDocID, string voucherID)
		{
			return new FixedAssetPurchase(config).GetFixedAssetPurchaseToPrint(sysDocID, voucherID);
		}

		public DataSet GetFixedAssetPurchaseToPrint(string sysDocID, string[] voucherID)
		{
			return new FixedAssetPurchase(config).GetFixedAssetPurchaseToPrint(sysDocID, voucherID);
		}

		public DataSet GetList(DateTime from, DateTime to, bool showVoid)
		{
			return new FixedAssetPurchase(config).GetList(from, to, showVoid);
		}
	}
}
