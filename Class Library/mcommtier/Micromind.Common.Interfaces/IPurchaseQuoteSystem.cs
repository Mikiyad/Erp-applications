using Micromind.Common.Data;
using System;
using System.Data;

namespace Micromind.Common.Interfaces
{
	public interface IPurchaseQuoteSystem
	{
		bool CreatePurchaseQuote(PurchaseQuoteData inventoryAdjustmentData, bool isUpdate);

		PurchaseQuoteData GetPurchaseQuoteByID(string sysDocID, string voucherID);

		bool DeletePurchaseQuote(string sysDocID, string voucherID);

		bool VoidPurchaseQuote(string sysDocID, string voucherID, bool isVoid);

		DataSet GetPurchaseQuoteToPrint(string sysDocID, string voucherID, bool mergeMatrixItems);

		DataSet GetPurchaseQuoteToPrint(string sysDocID, string[] voucherID, bool mergeMatrixItems);

		DataSet GetList(DateTime from, DateTime to, bool isImport, bool showVoid);

		DataSet GetOpenQuotesSummary(string vendorID, bool isImport);

		DataSet GetPurchaseComparisonReport(string refNumber);

		DataSet GettPurchaseQuoteList();
	}
}
