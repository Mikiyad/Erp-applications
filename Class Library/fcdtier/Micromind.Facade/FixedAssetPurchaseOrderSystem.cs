using Micromind.Common.Data;
using Micromind.Common.Interfaces;
using Micromind.Data;
using System;
using System.Data;

namespace Micromind.Facade
{
	public sealed class FixedAssetPurchaseOrderSystem : MarshalByRefObject, IFixedAssetPurchaseOrderSystem, IDisposable
	{
		private Config config;

		public FixedAssetPurchaseOrderSystem(Config config)
		{
			this.config = config;
		}

		public void Dispose()
		{
		}

		public bool CreatePurchaseOrder(FixedAssetPurchaseOrderData data, bool isUpdate)
		{
			return new FixedAssetPurchaseOrder(config).InsertUpdatePurchaseOrder(data, isUpdate);
		}

		public FixedAssetPurchaseOrderData GetPurchaseOrderByID(string sysDocID, string voucherID)
		{
			return new FixedAssetPurchaseOrder(config).GetPurchaseOrderByID(sysDocID, voucherID);
		}

		public bool CanUpdate(string sysDocID, string voucherNumber)
		{
			return new FixedAssetPurchaseOrder(config).CanUpdate(sysDocID, voucherNumber, null);
		}

		public bool DeletePurchaseOrder(string sysDocID, string voucherID)
		{
			return new FixedAssetPurchaseOrder(config).DeletePurchaseOrder(sysDocID, voucherID);
		}

		public bool VoidPurchaseOrder(string sysDocID, string voucherID, bool isVoid)
		{
			return new FixedAssetPurchaseOrder(config).VoidPurchaseOrder(sysDocID, voucherID, isVoid);
		}

		public DataSet GetPurchaseOrderToPrint(string sysDocID, string[] voucherID)
		{
			return new FixedAssetPurchaseOrder(config).GetPurchaseOrderToPrint(sysDocID, voucherID);
		}

		public DataSet GetPurchaseOrderToPrint(string sysDocID, string voucherID)
		{
			return new FixedAssetPurchaseOrder(config).GetPurchaseOrderToPrint(sysDocID, new string[1]
			{
				voucherID
			});
		}

		public DataSet GetList(DateTime from, DateTime to, bool showVoid)
		{
			return new FixedAssetPurchaseOrder(config).GetList(from, to, showVoid);
		}
	}
}
