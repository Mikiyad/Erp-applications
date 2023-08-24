using Micromind.Common.Data;
using Micromind.Common.Interfaces;
using Micromind.Data;
using System;
using System.Data;

namespace Micromind.Facade
{
	public sealed class PurchaseQuoteSystem : MarshalByRefObject, IPurchaseQuoteSystem, IDisposable
	{
		private Config config;

		public PurchaseQuoteSystem(Config config)
		{
			this.config = config;
		}

		public void Dispose()
		{
		}

		public bool CreatePurchaseQuote(PurchaseQuoteData data, bool isUpdate)
		{
			return new PurchaseQuote(config).InsertUpdatePurchaseQuote_New(data, isUpdate);
		}

		public PurchaseQuoteData GetPurchaseQuoteByID(string sysDocID, string voucherID)
		{
			return new PurchaseQuote(config).GetPurchaseQuoteByID(sysDocID, voucherID);
		}

		public bool DeletePurchaseQuote(string sysDocID, string voucherID)
		{
			return new PurchaseQuote(config).DeletePurchaseQuote(sysDocID, voucherID);
		}

		public bool VoidPurchaseQuote(string sysDocID, string voucherID, bool isVoid)
		{
			return new PurchaseQuote(config).VoidPurchaseQuote(sysDocID, voucherID, isVoid);
		}

		public DataSet GetPurchaseQuoteToPrint(string sysDocID, string[] voucherID, bool mergeMatrixItems)
		{
			return new PurchaseQuote(config).GetPurchaseQuoteToPrint(sysDocID, voucherID, mergeMatrixItems);
		}

		public DataSet GetPurchaseQuoteToPrint(string sysDocID, string voucherID, bool mergeMatrixItems)
		{
			return new PurchaseQuote(config).GetPurchaseQuoteToPrint(sysDocID, new string[1]
			{
				voucherID
			}, mergeMatrixItems);
		}

		public DataSet GetList(DateTime from, DateTime to, bool isImport, bool showVoid)
		{
			return new PurchaseQuote(config).GetList(from, to, isImport, showVoid);
		}

		public DataSet GetOpenQuotesSummary(string vendorID, bool isImport)
		{
			return new PurchaseQuote(config).GetOpenQuotesSummary(vendorID, isImport);
		}

		public DataSet GetPurchaseComparisonReport(string refNumber)
		{
			return new PurchaseQuote(config).GetPurchaseComparisonReport(refNumber);
		}

		public DataSet GettPurchaseQuoteList()
		{
			return new PurchaseQuote(config).GettPurchaseQuoteList();
		}
	}
}
