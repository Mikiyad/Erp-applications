using Micromind.Common.Data;
using Micromind.Common.Interfaces;
using Micromind.Data;
using System;
using System.Data;

namespace Micromind.Facade
{
	public sealed class LPOReceiptSystem : MarshalByRefObject, ILPOReceiptSystem, IDisposable
	{
		private Config config;

		public LPOReceiptSystem(Config config)
		{
			this.config = config;
		}

		public void Dispose()
		{
		}

		public bool CreateLPOReceipt(LPOReceiptData data, bool isUpdate)
		{
			return new LPOReceipt(config).InsertUpdateLPOReceipt(data, isUpdate);
		}

		public LPOReceiptData GetLPOReceiptByID(string sysDocID, string voucherID)
		{
			return new LPOReceipt(config).GetLPOReceiptByID(sysDocID, voucherID);
		}

		public bool DeleteLPOReceipt(string sysDocID, string voucherID)
		{
			return new LPOReceipt(config).DeleteLPOReceipt(sysDocID, voucherID);
		}

		public bool VoidLPOReceipt(string sysDocID, string voucherID, bool isVoid)
		{
			return new LPOReceipt(config).VoidLPOReceipt(sysDocID, voucherID, isVoid);
		}

		public DataSet GetLPOReceiptToPrint(string sysDocID, string voucherID)
		{
			return new LPOReceipt(config).GetLPOReceiptToPrint(sysDocID, voucherID);
		}

		public DataSet GetLPOReceiptToPrint(string sysDocID, string[] voucherID)
		{
			return new LPOReceipt(config).GetLPOReceiptToPrint(sysDocID, voucherID);
		}

		public DataSet GetInvoicesToReturn(string customerID, bool cashOnly, DateTime fromDate, DateTime toDate)
		{
			return new LPOReceipt(config).GetInvoicesToReturn(customerID, cashOnly, fromDate, toDate);
		}

		public DataSet GetList(DateTime from, DateTime to, bool showVoid)
		{
			return new LPOReceipt(config).GetList(from, to, showVoid);
		}
	}
}
