using Micromind.Common.Data;
using Micromind.Common.Interfaces;
using Micromind.Data;
using System;
using System.Data;

namespace Micromind.Facade
{
	public sealed class SalesProformaSystem : MarshalByRefObject, ISalesProformaSystem, IDisposable
	{
		private Config config;

		public SalesProformaSystem(Config config)
		{
			this.config = config;
		}

		public void Dispose()
		{
		}

		public bool CreateSalesOrder(SalesProformaInvoiceData data, bool isUpdate)
		{
			return new SalesProformaInvoice(config).InsertUpdateSalesOrder(data, isUpdate);
		}

		public SalesProformaInvoiceData GetSalesOrderByID(string sysDocID, string voucherID)
		{
			return new SalesProformaInvoice(config).GetSalesOrderByID(sysDocID, voucherID);
		}

		public bool DeleteSalesOrder(string sysDocID, string voucherID)
		{
			return new SalesProformaInvoice(config).DeleteSalesOrder(sysDocID, voucherID);
		}

		public bool VoidSalesOrder(string sysDocID, string voucherID, bool isVoid)
		{
			return new SalesProformaInvoice(config).VoidSalesOrder(sysDocID, voucherID, isVoid);
		}

		public DataSet GetOpenOrdersSummary(string customerID, bool isExport)
		{
			return new SalesProformaInvoice(config).GetOpenOrdersSummary(customerID, isExport);
		}

		public DataSet GetOpenOrderListReport()
		{
			return new SalesProformaInvoice(config).GetOpenOrderListReport();
		}

		public DataSet GetSalesOrderToPrint(string sysDocID, string voucherID)
		{
			return new SalesProformaInvoice(config).GetSalesOrderToPrint(sysDocID, voucherID);
		}

		public DataSet GetSalesOrderToPrint(string sysDocID, string[] voucherID)
		{
			return new SalesProformaInvoice(config).GetSalesOrderToPrint(sysDocID, voucherID);
		}

		public DataSet GetList(DateTime from, DateTime to, bool showVoid, bool isExport)
		{
			return new SalesProformaInvoice(config).GetList(from, to, showVoid, isExport);
		}

		public bool OrderHasShippedQuantity(string sysDocID, string voucherNumber)
		{
			return new SalesProformaInvoice(config).OrderHasShippedQuantity(sysDocID, voucherNumber, null);
		}
	}
}
