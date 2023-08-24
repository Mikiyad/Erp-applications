using Micromind.Common.Data;
using Micromind.Common.Interfaces;
using Micromind.Data;
using System;
using System.Data;

namespace Micromind.Facade
{
	public sealed class SalesQuoteSystem : MarshalByRefObject, ISalesQuoteSystem, IDisposable
	{
		private Config config;

		public SalesQuoteSystem(Config config)
		{
			this.config = config;
		}

		public void Dispose()
		{
		}

		public bool CreateSalesQuote(SalesQuoteData data, bool isUpdate, bool IsRevised)
		{
			return new SalesQuote(config).InsertUpdateSalesQuote(data, isUpdate, IsRevised);
		}

		public SalesQuoteData GetSalesQuoteByID(string sysDocID, string voucherID)
		{
			return new SalesQuote(config).GetSalesQuoteByID(sysDocID, voucherID);
		}

		public bool DeleteSalesQuote(string sysDocID, string voucherID)
		{
			return new SalesQuote(config).DeleteSalesQuote(sysDocID, voucherID);
		}

		public bool VoidSalesQuote(string sysDocID, string voucherID, bool isVoid)
		{
			return new SalesQuote(config).VoidSalesQuote(sysDocID, voucherID, isVoid);
		}

		public DataSet GetSalesQuoteToPrint(string sysDocID, string voucherID)
		{
			return new SalesQuote(config).GetSalesQuoteToPrint(sysDocID, voucherID);
		}

		public DataSet GetSalesQuoteToPrint(string sysDocID, string[] voucherID)
		{
			return new SalesQuote(config).GetSalesQuoteToPrint(sysDocID, voucherID);
		}

		public DataSet GetList(DateTime from, DateTime to, bool showVoid, string sysDocID)
		{
			return new SalesQuote(config).GetList(from, to, showVoid, sysDocID);
		}

		public DataSet GetOpenQuotesSummary(string customerID)
		{
			return new SalesQuote(config).GetOpenQuotesSummary(customerID);
		}

		public DataSet GetOpenQuotesSummary(string customerID, DateTime fromDate, DateTime toDate)
		{
			return new SalesQuote(config).GetOpenQuotesSummary(customerID, fromDate, toDate);
		}

		public DataSet GetOpenQuotesSummary(string sysDocID, string customerID, string locationID)
		{
			return new SalesQuote(config).GetOpenQuotesSummary(sysDocID, customerID, locationID);
		}

		public SalesQuoteData GetSalesQuoteByRevID(string sysDocID, string voucherID, int RevisedNo)
		{
			return new SalesQuote(config).GetSalesQuoteRevByID(sysDocID, voucherID, RevisedNo);
		}

		public DataSet GetLoadRevisionCombo(string sysDocID, string voucherID)
		{
			return new SalesQuote(config).GetLoadRevisionCombo(sysDocID, voucherID);
		}

		public DataSet GetSalesQuoteRevToPrint(string sysDocID, string voucherID, int RevisedNo)
		{
			return new SalesQuote(config).GetSalesQuoteRevToPrint(sysDocID, voucherID, RevisedNo);
		}

		public bool UpdateSalesQuoteStatus(string sysDocID, string voucherNumber)
		{
			return new SalesQuote(config).UpdateSalesQuoteStatus(sysDocID, voucherNumber, null);
		}

		public bool ReactivateSalesQuote(string sysDocID, string voucherNumber)
		{
			return new SalesQuote(config).ReactivateSalesQuote(sysDocID, voucherNumber, null);
		}
	}
}
