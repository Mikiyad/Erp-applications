using Micromind.Common.Data;
using Micromind.Common.Interfaces;
using Micromind.Data;
using System;
using System.Data;

namespace Micromind.Facade
{
	public sealed class FixedAssetDepSystem : MarshalByRefObject, IFixedAssetDepSystem, IDisposable
	{
		private Config config;

		public FixedAssetDepSystem(Config config)
		{
			this.config = config;
		}

		public void Dispose()
		{
		}

		public bool CreateFixedAssetDep(FixedAssetDepData data, bool isUpdate)
		{
			return new FixedAssetDep(config).InsertUpdateFixedAssetDep(data, isUpdate);
		}

		public FixedAssetDepData GetFixedAssetDepByID(string sysDocID, string voucherID)
		{
			return new FixedAssetDep(config).GetFixedAssetDepByID(sysDocID, voucherID);
		}

		public bool DeleteFixedAssetDep(string sysDocID, string voucherID)
		{
			return new FixedAssetDep(config).DeleteFixedAssetDep(sysDocID, voucherID);
		}

		public DataSet GetList(DateTime from, DateTime to, bool showVoid)
		{
			return new FixedAssetDep(config).GetList(from, to, showVoid);
		}

		public DataSet GetFixedAssetDepToPrint(string sysDocID, string voucherID)
		{
			return new FixedAssetDep(config).GetFixedAssetDepToPrint(sysDocID, voucherID);
		}

		public DataSet GetFixedAssetDepToPrint(string sysDocID, string[] voucherID)
		{
			return new FixedAssetDep(config).GetFixedAssetDepToPrint(sysDocID, voucherID);
		}
	}
}
