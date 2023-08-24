using Micromind.Common.Data;
using Micromind.Common.Interfaces;
using Micromind.Data;
using System;
using System.Data;

namespace Micromind.Facade
{
	public sealed class SendChequeSystem : MarshalByRefObject, ISendChequeSystem, IDisposable
	{
		private Config config;

		public SendChequeSystem(Config config)
		{
			this.config = config;
		}

		public void Dispose()
		{
		}

		public bool CreateSendCheque(SendChequeData data, bool isUpdate)
		{
			return new SendCheques(config).InsertUpdateSendCheque(data, isUpdate);
		}

		public SendChequeData GetSendChequeByID(string sysDocID, string voucherID)
		{
			return new SendCheques(config).GetSendChequeByID(sysDocID, voucherID);
		}

		public bool DeleteSendCheque(string sysDocID, string voucherID)
		{
			return new SendCheques(config).DeleteSendCheque(sysDocID, voucherID);
		}

		public DataSet GetSendChequeToPrint(string sysDocID, string voucherID)
		{
			return new SendCheques(config).GetSendChequeToPrint(sysDocID, voucherID);
		}

		public DataSet GetSendChequeToPrint(string sysDocID, string[] voucherID)
		{
			return new SendCheques(config).GetSendChequeToPrint(sysDocID, voucherID);
		}

		public DataSet GetList(DateTime from, DateTime to, bool showVoid)
		{
			return new SendCheques(config).GetList(from, to, showVoid);
		}
	}
}
