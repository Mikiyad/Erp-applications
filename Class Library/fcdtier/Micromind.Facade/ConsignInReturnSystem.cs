using Micromind.Common.Data;
using Micromind.Common.Interfaces;
using Micromind.Data;
using System;
using System.Data;

namespace Micromind.Facade
{
	public sealed class ConsignInReturnSystem : MarshalByRefObject, IConsignInReturnSystem, IDisposable
	{
		private Config config;

		public ConsignInReturnSystem(Config config)
		{
			this.config = config;
		}

		public void Dispose()
		{
		}

		public bool CreateConsignInReturn(ConsignInReturnData data, bool isUpdate)
		{
			return new ConsignInReturn(config).InsertUpdateConsignInReturn(data, isUpdate);
		}

		public ConsignInReturnData GetConsignInReturnByID(string sysDocID, string voucherID)
		{
			return new ConsignInReturn(config).GetConsignInReturnByID(sysDocID, voucherID);
		}

		public bool DeleteConsignInReturn(string sysDocID, string voucherID)
		{
			return new ConsignInReturn(config).DeleteConsignInReturn(sysDocID, voucherID);
		}

		public bool VoidConsignInReturn(string sysDocID, string voucherID, bool isVoid)
		{
			return new ConsignInReturn(config).VoidConsignInReturn(sysDocID, voucherID, isVoid);
		}

		public DataSet GetConsignInReturnToPrint(string sysDocID, string voucherID)
		{
			return new ConsignInReturn(config).GetConsignInReturnToPrint(sysDocID, voucherID);
		}

		public DataSet GetConsignInReturnToPrint(string sysDocID, string[] voucherID)
		{
			return new ConsignInReturn(config).GetConsignInReturnToPrint(sysDocID, voucherID);
		}

		public DataSet GetList(DateTime from, DateTime to, bool showVoid)
		{
			return new ConsignInReturn(config).GetList(from, to, showVoid);
		}

		public DataSet GetOpenConsignments(string customerID)
		{
			return new ConsignInReturn(config).GetOpenConsignments(customerID);
		}

		public bool ConsignmentHasSettlement(string sysDocID, string voucherNumber)
		{
			return new ConsignInReturn(config).ConsignmentHasSettlement(sysDocID, voucherNumber, null);
		}
	}
}
