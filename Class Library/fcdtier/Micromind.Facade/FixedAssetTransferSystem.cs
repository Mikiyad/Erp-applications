using Micromind.Common.Data;
using Micromind.Common.Interfaces;
using Micromind.Data;
using System;
using System.Data;

namespace Micromind.Facade
{
	public sealed class FixedAssetTransferSystem : MarshalByRefObject, IFixedAssetTransferSystem, IDisposable
	{
		private Config config;

		public FixedAssetTransferSystem(Config config)
		{
			this.config = config;
		}

		public void Dispose()
		{
		}

		public bool CreateFixedAssetTransfer(FixedAssetTransferData data, bool isUpdate)
		{
			return new FixedAssetTransfer(config).InsertUpdateFixedAssetTransfer(data, isUpdate);
		}

		public FixedAssetTransferData GetFixedAssetTransferByID(string sysDocID, string voucherID)
		{
			return new FixedAssetTransfer(config).GetFixedAssetTransferByID(sysDocID, voucherID);
		}

		public bool DeleteFixedAssetTransfer(string sysDocID, string voucherID)
		{
			return new FixedAssetTransfer(config).DeleteFixedAssetTransfer(sysDocID, voucherID);
		}

		public bool VoidFixedAssetTransfer(string sysDocID, string voucherID, bool isVoid)
		{
			return new FixedAssetTransfer(config).VoidFixedAssetTransfer(sysDocID, voucherID, isVoid);
		}

		public DataSet GetFixedAssetTransferReport(DateTime from, DateTime to, string warehouseCode, bool isTransferOut)
		{
			return new FixedAssetTransfer(config).GetFixedAssetTransferReport(from, to, warehouseCode, isTransferOut);
		}

		public DataSet GetList(DateTime from, DateTime to, bool showVoid)
		{
			return new FixedAssetTransfer(config).GetList(from, to, showVoid);
		}
	}
}
