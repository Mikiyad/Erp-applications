using Micromind.Common.Data;
using Micromind.Common.Interfaces;
using Micromind.Data;
using System;
using System.Data;

namespace Micromind.Facade
{
	public sealed class FixedAssetSaleSystem : MarshalByRefObject, IFixedAssetSaleSystem, IDisposable
	{
		private Config config;

		public FixedAssetSaleSystem(Config config)
		{
			this.config = config;
		}

		public void Dispose()
		{
		}

		public bool CreateFixedAssetSale(FixedAssetSaleData data, bool isUpdate)
		{
			return new FixedAssetSale(config).InsertUpdateFixedAssetSale(data, isUpdate);
		}

		public FixedAssetSaleData GetFixedAssetSaleByID(string sysDocID, string voucherID)
		{
			return new FixedAssetSale(config).GetFixedAssetSaleByID(sysDocID, voucherID);
		}

		public bool DeleteFixedAssetSale(string sysDocID, string voucherID)
		{
			return new FixedAssetSale(config).DeleteFixedAssetSale(sysDocID, voucherID);
		}

		public bool VoidFixedAssetSale(string sysDocID, string voucherID, bool isVoid)
		{
			return new FixedAssetSale(config).VoidFixedAssetSale(sysDocID, voucherID, isVoid);
		}

		public DataSet GetFixedAssetSaleToPrint(string sysDocID, string voucherID)
		{
			return new FixedAssetSale(config).GetFixedAssetSaleToPrint(sysDocID, voucherID);
		}

		public DataSet GetFixedAssetSaleToPrint(string sysDocID, string[] voucherID)
		{
			return new FixedAssetSale(config).GetFixedAssetSaleToPrint(sysDocID, voucherID);
		}

		public DataSet GetList(DateTime from, DateTime to, bool showVoid)
		{
			return new FixedAssetSale(config).GetList(from, to, showVoid);
		}
	}
}
