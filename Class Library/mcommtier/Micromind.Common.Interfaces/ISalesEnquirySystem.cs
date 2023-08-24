using Micromind.Common.Data;
using System;
using System.Data;

namespace Micromind.Common.Interfaces
{
	public interface ISalesEnquirySystem
	{
		bool CreateSalesOrder(SalesEnquiryData inventoryAdjustmentData, bool isUpdate);

		SalesEnquiryData GetSalesOrderByID(string sysDocID, string voucherID);

		bool DeleteSalesOrder(string sysDocID, string voucherID);

		bool VoidSalesOrder(string sysDocID, string voucherID, bool isVoid);

		DataSet GetOpenOrdersSummary(string customerID, bool isExport);

		DataSet GetOpenOrderListReport();

		DataSet GetSalesOrderToPrint(string sysDocID, string voucherID);

		DataSet GetSalesOrderToPrint(string sysDocID, string[] voucherID);

		DataSet GetList(DateTime from, DateTime to, bool showVoid);

		bool OrderHasShippedQuantity(string sysDocID, string voucherNumber);
	}
}
