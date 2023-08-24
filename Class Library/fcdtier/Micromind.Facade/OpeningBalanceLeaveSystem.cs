using Micromind.Common.Data;
using Micromind.Common.Interfaces;
using Micromind.Data;
using System;
using System.Data;

namespace Micromind.Facade
{
	public sealed class OpeningBalanceLeaveSystem : MarshalByRefObject, IOpeningBalanceLeaveSystem, IDisposable
	{
		private Config config;

		public OpeningBalanceLeaveSystem(Config config)
		{
			this.config = config;
		}

		public void Dispose()
		{
		}

		public bool CreateOpeningBalanceLeave(OpeningBalanceLeaveData data, bool isUpdate)
		{
			return new OpeningBalanceLeave(config).InsertUpdateOpeningBalanceLeave(data, isUpdate);
		}

		public OpeningBalanceLeaveData GetOpeningBalanceLeaveByID(string sysDocID, string voucherID)
		{
			return new OpeningBalanceLeave(config).GetOpeningBalanceLeaveByID(sysDocID, voucherID);
		}

		public bool DeleteOpeningBalanceLeave(string sysDocID, string voucherID)
		{
			return new OpeningBalanceLeave(config).DeleteOpeningBalanceLeave(sysDocID, voucherID);
		}

		public DataSet GetOpeningBalanceLeaveToPrint(string sysDocID, string[] voucherID)
		{
			return new OpeningBalanceLeave(config).GetOpeningBalanceLeaveToPrint(sysDocID, voucherID);
		}

		public DataSet GetOpeningBalanceLeaveToPrint(string sysDocID, string voucherID)
		{
			return new OpeningBalanceLeave(config).GetOpeningBalanceLeaveToPrint(sysDocID, new string[1]
			{
				voucherID
			});
		}

		public string GetNextLeaveNumber(string sysDocID)
		{
			return new OpeningBalanceLeave(config).GetNextBatchNumber(sysDocID);
		}

		public DataSet GetList(DateTime from, DateTime to, bool showVoid)
		{
			return new OpeningBalanceLeave(config).GetList(from, to, showVoid);
		}
	}
}
