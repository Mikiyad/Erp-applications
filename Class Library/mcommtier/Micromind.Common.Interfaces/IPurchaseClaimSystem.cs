using Micromind.Common.Data;
using System;
using System.Data;

namespace Micromind.Common.Interfaces
{
	public interface IPurchaseClaimSystem
	{
		bool CreatePurchaseClaim(PurchaseClaimData inventoryAdjustmentData, bool isUpdate);

		PurchaseClaimData GetPurchaseClaimByID(string sysDocID, string voucherID);

		bool VoidPurchaseClaim(string sysDocID, string voucherID, bool isVoid);

		bool DeletePurchaseClaim(string sysDocID, string voucherID);

		DataSet GetPurchaseClaimToPrint(string sysDocID, string voucherID);

		DataSet GetPurchaseClaimToPrint(string sysDocID, string[] voucherID);

		DataSet GetList(DateTime from, DateTime to, bool showVoid);

		bool OrderHasShippedQuantity(string sysDocID, string voucherNumber);

		DataSet GetPurchaseClaimToPrintTR(string sysDocID, string voucherID);

		DataSet GetOpenPurchaseClaims(string vendorID);
	}
}
