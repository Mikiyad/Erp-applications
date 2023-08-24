using Micromind.Common.Data;
using Micromind.Common.Interfaces;
using Micromind.Data;
using System;
using System.Data;

namespace Micromind.Facade
{
	public sealed class SalesOrderSystem : MarshalByRefObject, ISalesOrderSystem, IDisposable
	{
		private Config config;

		public SalesOrderSystem(Config config)
		{
			this.config = config;
		}

		public void Dispose()
		{
		}

		public bool CreateSalesOrder(SalesOrderData data, bool isUpdate)
		{
			return new SalesOrder(config).InsertUpdateSalesOrder(data, isUpdate);
		}

		public SalesOrderData GetSalesOrderByID(string sysDocID, string voucherID)
		{
			return new SalesOrder(config).GetSalesOrderByID(sysDocID, voucherID);
		}

		public bool DeleteSalesOrder(string sysDocID, string voucherID, bool IsjobID)
		{
			return new SalesOrder(config).DeleteSalesOrder(sysDocID, voucherID, IsjobID);
		}

		public bool VoidSalesOrder(string sysDocID, string voucherID, bool isVoid)
		{
			return new SalesOrder(config).VoidSalesOrder(sysDocID, voucherID, isVoid);
		}

		public DataSet GetOpenOrdersSummary(string customerID, bool isExport)
		{
			return new SalesOrder(config).GetOpenOrdersSummary(customerID, isExport);
		}

		public DataSet GetOpenOrdersForPurchase(bool isExport)
		{
			return new SalesOrder(config).GetOpenOrdersForPurchase(isExport);
		}

		public DataSet GetOpenOrderListReport()
		{
			return new SalesOrder(config).GetOpenOrderListReport();
		}

		public DataSet GetSalesOrderToPrint(string sysDocID, string voucherID)
		{
			return new SalesOrder(config).GetSalesOrderToPrint(sysDocID, voucherID);
		}

		public DataSet GetSalesOrderToPrint(string sysDocID, string[] voucherID)
		{
			return new SalesOrder(config).GetSalesOrderToPrint(sysDocID, voucherID);
		}

		public DataSet GetList(DateTime from, DateTime to, bool isExport, bool showVoid, string sysDocID)
		{
			return new SalesOrder(config).GetList(from, to, isExport, showVoid, sysDocID);
		}

		public bool OrderHasShippedQuantity(string sysDocID, string voucherNumber)
		{
			return new SalesOrder(config).OrderHasShippedQuantity(sysDocID, voucherNumber, null);
		}

		public bool IsPOOrder(string sysDocID, string voucherNumber)
		{
			return new SalesOrder(config).IsPOOrder(sysDocID, voucherNumber, null);
		}

		public DataSet GetSalesOrderDetailReport(DateTime from, DateTime to, string jobID, string customerID, string fromItem, string toItem, string fromClass, string toClass, string fromCategory, string toCategory, string fromBrand, string toBrand, string fromLocation, string toLocation, bool openOrders, string fromManufacturer, string toManufacturer, string fromStyle, string toStyle, string fromOrigin, string toOrigin)
		{
			return new SalesOrder(config).GetSalesOrderDetailReport(from, to, jobID, customerID, fromItem, toItem, fromClass, toClass, fromCategory, toCategory, fromBrand, toBrand, fromLocation, toLocation, openOrders, fromManufacturer, toManufacturer, fromStyle, toStyle, fromOrigin, toOrigin);
		}

		public bool SetOrderStatus(string sysDocID, string voucherID, int status)
		{
			return new SalesOrder(config).SetOrderStatus(sysDocID, voucherID, status);
		}

		public DataSet GetReservationDetails(string sysDocID, string voucherID, string productID, int rowIndex)
		{
			return new SalesOrder(config).GetReservationDetails(sysDocID, voucherID, productID, rowIndex);
		}

		public SalesOrderData GetPurchaseFromSalesOrderByID(string sysDocID, string voucherID)
		{
			return new SalesOrder(config).GetPurchaseFromSalesOrderByID(sysDocID, voucherID);
		}
	}
}
