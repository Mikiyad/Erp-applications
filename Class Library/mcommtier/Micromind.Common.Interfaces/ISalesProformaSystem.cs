using Micromind.Common.Data;
using System;
using System.Data;

namespace Micromind.Common.Interfaces
{
	public interface ISalesProformaSystem
	{
		bool CreateSalesOrder(SalesProformaInvoiceData inventoryAdjustmentData, bool isUpdate);

		SalesProformaInvoiceData GetSalesOrderByID(string sysDocID, string voucherID);

		bool DeleteSalesOrder(string sysDocID, string voucherID);

		bool VoidSalesOrder(string sysDocID, string voucherID, bool isVoid);

		DataSet GetOpenOrdersSummary(string customerID, bool isExport);

		DataSet GetOpenOrderListReport();

		DataSet GetSalesOrderToPrint(string sysDocID, string voucherID);

		DataSet GetSalesOrderToPrint(string sysDocID, string[] voucherID);

		DataSet GetList(DateTime from, DateTime to, bool showVoid, bool isExport);

		bool OrderHasShippedQuantity(string sysDocID, string voucherNumber);
	}
}
