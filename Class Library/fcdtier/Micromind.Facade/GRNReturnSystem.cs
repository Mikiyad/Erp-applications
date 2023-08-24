using Micromind.Common.Data;
using Micromind.Common.Interfaces;
using Micromind.Data;
using System;
using System.Data;

namespace Micromind.Facade
{
	public sealed class GRNReturnSystem : MarshalByRefObject, IGRNReturnSystem, IDisposable
	{
		private Config config;

		public GRNReturnSystem(Config config)
		{
			this.config = config;
		}

		public void Dispose()
		{
		}

		public bool CreateGRNReturn(GRNReturnData data, bool isUpdate)
		{
			return new GRNReturn(config).InsertUpdateGRNReturn(data, isUpdate);
		}

		public GRNReturnData GetGRNReturnByID(string sysDocID, string voucherID)
		{
			return new GRNReturn(config).GetGRNReturnByID(sysDocID, voucherID);
		}

		public bool DeleteGRNReturn(string sysDocID, string voucherID)
		{
			return new GRNReturn(config).DeleteGRNReturn(sysDocID, voucherID);
		}

		public bool VoidGRNReturn(string sysDocID, string voucherID, bool isVoid)
		{
			return new GRNReturn(config).VoidGRNReturn(sysDocID, voucherID, isVoid);
		}

		public DataSet GetGRNReturnToPrint(string sysDocID, string[] voucherID)
		{
			return new GRNReturn(config).GetGRNReturnToPrint(sysDocID, voucherID);
		}

		public DataSet GetGRNReturnToPrint(string sysDocID, string voucherID)
		{
			return new GRNReturn(config).GetGRNReturnToPrint(sysDocID, new string[1]
			{
				voucherID
			});
		}

		public DataSet GetList(DateTime from, DateTime to, bool showVoid)
		{
			return new GRNReturn(config).GetList(from, to, showVoid);
		}

		public DataSet GetGRNsToReturn(string customerID, DateTime fromDate, DateTime toDate)
		{
			return new GRNReturn(config).GetGRNsToReturn(customerID, fromDate, toDate);
		}
	}
}
