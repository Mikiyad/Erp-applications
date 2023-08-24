using Micromind.Common.Data;
using System;
using System.Data;

namespace Micromind.Common.Interfaces
{
	public interface IPaymentRequestSystem
	{
		bool CreatePaymentRequest(PaymentRequestData inventoryAdjustmentData, bool isUpdate);

		PaymentRequestData GetPaymentRequestByID(string sysDocID, string voucherID);

		bool VoidPaymentRequest(string sysDocID, string voucherID, bool isVoid);

		bool DeletePaymentRequest(string sysDocID, string voucherID);

		DataSet GetOpenOrdersSummary(string customerID, bool isExport);

		DataSet GetOpenOrderListReport();

		DataSet GetPaymentRequestToPrint(string sysDocID, string voucherID);

		DataSet GetPaymentRequestToPrint(string sysDocID, string[] voucherID);

		DataSet GetList(DateTime from, DateTime to, bool showVoid);

		bool OrderHasShippedQuantity(string sysDocID, string voucherNumber);

		DataSet GetPaymentRequestToPrintTR(string sysDocID, string voucherID);

		DataSet GetOpenPaymentRequests(int typeID);
	}
}
