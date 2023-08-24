using Micromind.Common.Data;
using Micromind.Common.Interfaces;
using Micromind.Data;
using System;
using System.Data;

namespace Micromind.Facade
{
	public sealed class SalesEnquirySystem : MarshalByRefObject, ISalesEnquirySystem, IDisposable
	{
		private Config config;

		public SalesEnquirySystem(Config config)
		{
			this.config = config;
		}

		public void Dispose()
		{
		}

		public bool CreateSalesOrder(SalesEnquiryData data, bool isUpdate)
		{
			return new SalesEnquiry(config).InsertUpdateSalesOrder(data, isUpdate);
		}

		public SalesEnquiryData GetSalesOrderByID(string sysDocID, string voucherID)
		{
			return new SalesEnquiry(config).GetSalesOrderByID(sysDocID, voucherID);
		}

		public bool DeleteSalesOrder(string sysDocID, string voucherID)
		{
			return new SalesEnquiry(config).DeleteSalesOrder(sysDocID, voucherID);
		}

		public bool VoidSalesOrder(string sysDocID, string voucherID, bool isVoid)
		{
			return new SalesEnquiry(config).VoidSalesOrder(sysDocID, voucherID, isVoid);
		}

		public DataSet GetOpenOrdersSummary(string customerID, bool isExport)
		{
			return new SalesEnquiry(config).GetOpenOrdersSummary(customerID, isExport);
		}

		public DataSet GetOpenOrderListReport()
		{
			return new SalesEnquiry(config).GetOpenOrderListReport();
		}

		public DataSet GetSalesOrderToPrint(string sysDocID, string voucherID)
		{
			return new SalesEnquiry(config).GetSalesOrderToPrint(sysDocID, voucherID);
		}

		public DataSet GetSalesOrderToPrint(string sysDocID, string[] voucherID)
		{
			return new SalesEnquiry(config).GetSalesOrderToPrint(sysDocID, voucherID);
		}

		public DataSet GetList(DateTime from, DateTime to, bool showVoid)
		{
			return new SalesEnquiry(config).GetList(from, to, showVoid);
		}

		public bool OrderHasShippedQuantity(string sysDocID, string voucherNumber)
		{
			return new SalesEnquiry(config).OrderHasShippedQuantity(sysDocID, voucherNumber, null);
		}
	}
}
