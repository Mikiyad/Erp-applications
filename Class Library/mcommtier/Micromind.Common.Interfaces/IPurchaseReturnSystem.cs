using Micromind.Common.Data;
using System;
using System.Data;

namespace Micromind.Common.Interfaces
{
	public interface IPurchaseReturnSystem
	{
		bool CreatePurchaseReturn(PurchaseReturnData inventoryAdjustmentData, bool isUpdate);

		PurchaseReturnData GetPurchaseReturnByID(string sysDocID, string voucherID);

		bool DeletePurchaseReturn(string sysDocID, string voucherID);

		bool VoidPurchaseReturn(string sysDocID, string voucherID, bool isVoid);

		DataSet GetPurchaseReturnToPrint(string sysDocID, string[] voucherID);

		DataSet GetPurchaseReturnToPrint(string sysDocID, string voucherID);

		DataSet GetList(DateTime from, DateTime to, bool showVoid);

		DataSet GetInvoicesToReturn(string customerID, bool cashOnly, DateTime fromDate, DateTime toDate);
	}
}
