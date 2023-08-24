using Micromind.Common.Data;
using Micromind.Common.Interfaces;
using Micromind.Data;
using System;
using System.Data;

namespace Micromind.Facade
{
	public sealed class ConsignOutReturnSystem : MarshalByRefObject, IConsignOutReturnSystem, IDisposable
	{
		private Config config;

		public ConsignOutReturnSystem(Config config)
		{
			this.config = config;
		}

		public void Dispose()
		{
		}

		public bool CreateConsignOutReturn(ConsignOutReturnData data, bool isUpdate)
		{
			return new ConsignOutReturn(config).InsertUpdateConsignOutReturn(data, isUpdate);
		}

		public ConsignOutReturnData GetConsignOutReturnByID(string sysDocID, string voucherID)
		{
			return new ConsignOutReturn(config).GetConsignOutReturnByID(sysDocID, voucherID);
		}

		public bool DeleteConsignOutReturn(string sysDocID, string voucherID)
		{
			return new ConsignOutReturn(config).DeleteConsignOutReturn(sysDocID, voucherID);
		}

		public bool VoidConsignOutReturn(string sysDocID, string voucherID, bool isVoid)
		{
			return new ConsignOutReturn(config).VoidConsignOutReturn(sysDocID, voucherID, isVoid);
		}

		public DataSet GetConsignOutReturnToPrint(string sysDocID, string voucherID)
		{
			return new ConsignOutReturn(config).GetConsignOutReturnToPrint(sysDocID, voucherID);
		}

		public DataSet GetConsignOutReturnToPrint(string sysDocID, string[] voucherID)
		{
			return new ConsignOutReturn(config).GetConsignOutReturnToPrint(sysDocID, voucherID);
		}

		public DataSet GetList(DateTime from, DateTime to, bool showVoid)
		{
			return new ConsignOutReturn(config).GetList(from, to, showVoid);
		}

		public DataSet GetOpenConsignments(string customerID)
		{
			return new ConsignOutReturn(config).GetOpenConsignments(customerID);
		}

		public bool ConsignmentHasSettlement(string sysDocID, string voucherNumber)
		{
			return new ConsignOutReturn(config).ConsignmentHasSettlement(sysDocID, voucherNumber, null);
		}
	}
}
