using Micromind.Common.Data;
using System;
using System.Data;

namespace Micromind.Common.Interfaces
{
	public interface ISalesOrderSystem
	{
		bool CreateSalesOrder(SalesOrderData inventoryAdjustmentData, bool isUpdate);

		SalesOrderData GetSalesOrderByID(string sysDocID, string voucherID);

		bool DeleteSalesOrder(string sysDocID, string voucherID, bool isJobID);

		bool VoidSalesOrder(string sysDocID, string voucherID, bool isVoid);

		DataSet GetOpenOrdersSummary(string customerID, bool isExport);

		DataSet GetOpenOrderListReport();

		DataSet GetOpenOrdersForPurchase(bool isExport);

		DataSet GetSalesOrderToPrint(string sysDocID, string voucherID);

		DataSet GetSalesOrderToPrint(string sysDocID, string[] voucherID);

		DataSet GetList(DateTime from, DateTime to, bool isExport, bool showVoid, string sysDocID);

		bool OrderHasShippedQuantity(string sysDocID, string voucherNumber);

		bool IsPOOrder(string sysDocID, string voucherNumber);

		DataSet GetSalesOrderDetailReport(DateTime from, DateTime to, string jobID, string customerID, string fromItem, string toItem, string fromClass, string toClass, string fromCategory, string toCategory, string fromBrand, string toBrand, string fromLocation, string toLocation, bool openOrders, string fromManufacturer, string toManufacturer, string fromStyle, string toStyle, string fromOrigin, string toOrigin);

		bool SetOrderStatus(string sysDocID, string voucherID, int status);

		DataSet GetReservationDetails(string sysDocID, string voucherID, string productID, int rowIndex);

		SalesOrderData GetPurchaseFromSalesOrderByID(string sysDocID, string voucherID);
	}
}
