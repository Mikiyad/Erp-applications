using Micromind.Common.Data;
using System;
using System.Data;

namespace Micromind.Common.Interfaces
{
	public interface ILPOReceiptSystem
	{
		bool CreateLPOReceipt(LPOReceiptData inventoryAdjustmentData, bool isUpdate);

		LPOReceiptData GetLPOReceiptByID(string sysDocID, string voucherID);

		bool DeleteLPOReceipt(string sysDocID, string voucherID);

		bool VoidLPOReceipt(string sysDocID, string voucherID, bool isVoid);

		DataSet GetLPOReceiptToPrint(string sysDocID, string voucherID);

		DataSet GetLPOReceiptToPrint(string sysDocID, string[] voucherID);

		DataSet GetInvoicesToReturn(string customerID, bool cashOnly, DateTime fromDate, DateTime toDate);

		DataSet GetList(DateTime from, DateTime to, bool showVoid);
	}
}
