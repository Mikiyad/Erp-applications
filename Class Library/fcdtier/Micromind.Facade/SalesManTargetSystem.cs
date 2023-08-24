using Micromind.Common.Data;
using Micromind.Common.Interfaces;
using Micromind.Data;
using System;
using System.Data;

namespace Micromind.Facade
{
	public sealed class SalesManTargetSystem : MarshalByRefObject, ISalesManTargetSystem, IDisposable
	{
		private Config config;

		public SalesManTargetSystem(Config config)
		{
			this.config = config;
		}

		public void Dispose()
		{
		}

		public bool CreateSalesManTarget(SalesManTargetData data, bool isUpdate)
		{
			return new SalesManTarget(config).InsertUpdateSalesManTarget(data, isUpdate);
		}

		public SalesManTargetData GetSalesManTargetByID(string sysDocID, string voucherID)
		{
			return new SalesManTarget(config).GetSalesManTargetByID(sysDocID, voucherID);
		}

		public bool DeleteSalesManTarget(string voucherID)
		{
			return new SalesManTarget(config).DeleteSalesManTarget(voucherID);
		}

		public DataSet GetList(DateTime from, DateTime to, bool showVoid)
		{
			return new SalesManTarget(config).GetList(from, to, showVoid);
		}

		public DataSet GetSalesManTargetToPrint(string sysDocID, string voucherID)
		{
			return new SalesManTarget(config).GetSalesManTargetToPrint(sysDocID, voucherID);
		}
	}
}
