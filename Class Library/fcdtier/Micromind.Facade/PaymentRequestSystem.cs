using Micromind.Common.Data;
using Micromind.Common.Interfaces;
using Micromind.Data;
using System;
using System.Data;

namespace Micromind.Facade
{
	public sealed class PaymentRequestSystem : MarshalByRefObject, IPaymentRequestSystem, IDisposable
	{
		private Config config;

		public PaymentRequestSystem(Config config)
		{
			this.config = config;
		}

		public void Dispose()
		{
		}

		public bool DeletePaymentRequest(string sysDocID, string voucherID)
		{
			return new PaymentRequest(config).DeletePaymentRequest(sysDocID, voucherID);
		}

		public bool VoidPaymentRequest(string sysDocID, string voucherID, bool isVoid)
		{
			return new PaymentRequest(config).VoidPaymentRequest(sysDocID, voucherID, isVoid);
		}

		public bool CreatePaymentRequest(PaymentRequestData data, bool isUpdate)
		{
			return new PaymentRequest(config).InsertUpdatePaymentRequest(data, isUpdate);
		}

		public PaymentRequestData GetPaymentRequestByID(string sysDocID, string voucherID)
		{
			return new PaymentRequest(config).GetPaymentRequestByID(sysDocID, voucherID);
		}

		public DataSet GetOpenOrdersSummary(string customerID, bool isExport)
		{
			return new PaymentRequest(config).GetOpenOrdersSummary(customerID, isExport);
		}

		public DataSet GetOpenOrderListReport()
		{
			return new PaymentRequest(config).GetOpenOrderListReport();
		}

		public DataSet GetPaymentRequestToPrint(string sysDocID, string voucherID)
		{
			return new PaymentRequest(config).GetPaymentRequestToPrint(sysDocID, voucherID);
		}

		public DataSet GetPaymentRequestToPrint(string sysDocID, string[] voucherID)
		{
			return new PaymentRequest(config).GetPaymentRequestToPrint(sysDocID, voucherID);
		}

		public DataSet GetList(DateTime from, DateTime to, bool showVoid)
		{
			return new PaymentRequest(config).GetList(from, to, showVoid);
		}

		public bool OrderHasShippedQuantity(string sysDocID, string voucherNumber)
		{
			return new PaymentRequest(config).OrderHasShippedQuantity(sysDocID, voucherNumber, null);
		}

		public DataSet GetPaymentRequestToPrintTR(string sysDocID, string voucherID)
		{
			return new PaymentRequest(config).GetPaymentRequestToPrintTR(sysDocID, voucherID);
		}

		public DataSet GetOpenPaymentRequests(int typeID)
		{
			return new PaymentRequest(config).GetOpenPaymentRequests(typeID);
		}
	}
}
