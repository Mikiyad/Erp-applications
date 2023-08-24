using Micromind.Common.Data;
using Micromind.Common.Interfaces;
using Micromind.Data;
using System;
using System.Data;

namespace Micromind.Facade
{
	public sealed class OpeningBalanceBatchSystem : MarshalByRefObject, IOpeningBalanceBatchSystem, IDisposable
	{
		private Config config;

		public OpeningBalanceBatchSystem(Config config)
		{
			this.config = config;
		}

		public void Dispose()
		{
		}

		public bool CreateOpeningBalanceBatch(OpeningBalanceBatchData data, bool isUpdate)
		{
			return new OpeningBalanceBatch(config).InsertUpdateOpeningBalanceBatch(data, isUpdate);
		}

		public OpeningBalanceBatchData GetOpeningBalanceBatchByID(string sysDocID, string voucherID)
		{
			return new OpeningBalanceBatch(config).GetOpeningBalanceBatchByID(sysDocID, voucherID);
		}

		public bool DeleteOpeningBalanceBatch(string sysDocID, string voucherID)
		{
			return new OpeningBalanceBatch(config).DeleteOpeningBalanceBatch(sysDocID, voucherID);
		}

		public DataSet GetOpeningBalanceBatchToPrint(string sysDocID, string[] voucherID)
		{
			return new OpeningBalanceBatch(config).GetOpeningBalanceBatchToPrint(sysDocID, voucherID);
		}

		public DataSet GetOpeningBalanceBatchToPrint(string sysDocID, string voucherID)
		{
			return new OpeningBalanceBatch(config).GetOpeningBalanceBatchToPrint(sysDocID, new string[1]
			{
				voucherID
			});
		}

		public string GetNextBatchNumber(string sysDocID)
		{
			return new OpeningBalanceBatch(config).GetNextBatchNumber(sysDocID);
		}

		public DataSet GetList(DateTime from, DateTime to, bool showVoid, string SysDocID)
		{
			return new OpeningBalanceBatch(config).GetList(from, to, showVoid, SysDocID);
		}
	}
}
