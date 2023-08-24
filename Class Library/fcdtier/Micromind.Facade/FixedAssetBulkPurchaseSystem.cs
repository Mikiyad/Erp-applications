using Micromind.Common.Data;
using Micromind.Common.Interfaces;
using Micromind.Data;
using System;
using System.Data;

namespace Micromind.Facade
{
	public sealed class FixedAssetBulkPurchaseSystem : MarshalByRefObject, IFixedAssetBulkPurchaseSystem, IDisposable
	{
		private Config config;

		public FixedAssetBulkPurchaseSystem(Config config)
		{
			this.config = config;
		}

		public void Dispose()
		{
		}

		public bool CreateFixedAssetPurchase(FixedAssetBulkPurchaseData data, bool isUpdate)
		{
			return new FixedAssetBulkPurchase(config).InsertUpdateFixedAssetPurchase(data, isUpdate);
		}

		public FixedAssetBulkPurchaseData GetFixedAssetPurchaseByID(string sysDocID, string voucherID)
		{
			return new FixedAssetBulkPurchase(config).GetFixedAssetPurchaseByID(sysDocID, voucherID);
		}

		public bool DeleteFixedAssetPurchase(string sysDocID, string voucherID, DataTable dsres)
		{
			return new FixedAssetBulkPurchase(config).DeleteFixedAssetPurchase(sysDocID, voucherID, dsres);
		}

		public bool VoidFixedAssetPurchase(string sysDocID, string voucherID, bool isVoid)
		{
			return new FixedAssetBulkPurchase(config).VoidFixedAssetPurchase(sysDocID, voucherID, isVoid);
		}

		public DataSet GetFixedAssetPurchaseToPrint(string sysDocID, string voucherID)
		{
			return new FixedAssetBulkPurchase(config).GetFixedAssetPurchaseToPrint(sysDocID, voucherID);
		}

		public DataSet GetFixedAssetPurchaseToPrint(string sysDocID, string[] voucherID)
		{
			return new FixedAssetBulkPurchase(config).GetFixedAssetPurchaseToPrint(sysDocID, voucherID);
		}

		public DataSet GetList(DateTime from, DateTime to, bool showVoid)
		{
			return new FixedAssetBulkPurchase(config).GetList(from, to, showVoid);
		}
	}
}
