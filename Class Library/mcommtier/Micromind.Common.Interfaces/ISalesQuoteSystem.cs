using Micromind.Common.Data;
using System;
using System.Data;

namespace Micromind.Common.Interfaces
{
	public interface ISalesQuoteSystem
	{
		bool CreateSalesQuote(SalesQuoteData inventoryAdjustmentData, bool isUpdate, bool IsRevised);

		SalesQuoteData GetSalesQuoteByID(string sysDocID, string voucherID);

		SalesQuoteData GetSalesQuoteByRevID(string sysDocID, string voucherID, int RevisedNo);

		bool DeleteSalesQuote(string sysDocID, string voucherID);

		bool VoidSalesQuote(string sysDocID, string voucherID, bool isVoid);

		DataSet GetSalesQuoteToPrint(string sysDocID, string voucherID);

		DataSet GetSalesQuoteToPrint(string sysDocID, string[] voucherID);

		DataSet GetList(DateTime from, DateTime to, bool showVoid, string sysDocID);

		DataSet GetOpenQuotesSummary(string customerID, DateTime fromDate, DateTime toDate);

		DataSet GetOpenQuotesSummary(string customerID);

		DataSet GetOpenQuotesSummary(string sysDocID, string customerID, string locationID);

		DataSet GetLoadRevisionCombo(string sysDocID, string voucherID);

		DataSet GetSalesQuoteRevToPrint(string sysDocID, string voucherID, int RevisedNo);

		bool UpdateSalesQuoteStatus(string sysDocID, string voucherID);

		bool ReactivateSalesQuote(string sysDocID, string voucherID);
	}
}
