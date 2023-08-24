using Micromind.Common.Data;
using System;
using System.Data;

namespace Micromind.Common.Interfaces
{
	public interface ISalesPOSSystem
	{
		bool CreateSalesPOS(SalesPOSData inventoryAdjustmentData, bool isUpdate);

		SalesPOSData GetSalesPOSByID(string sysDocID, string voucherID);

		bool DeleteSalesPOS(string sysDocID, string voucherID);

		bool VoidSalesPOS(string sysDocID, string voucherID, bool isVoid);

		DataSet GetInvoicesForDelivery(string customerID);

		bool IsBelowMinPrice(string productID, string unitID, string currencyID, decimal currencyRate, decimal price);

		DataSet GetSalesPOSToPrint(string sysDocID, string[] voucherID);

		DataSet GetSalesPOSToPrint(string sysDocID, string voucherID);

		DataSet GetList(DateTime from, DateTime to, bool showVoid);

		DataSet GetSalesReceiptLookupList(string SysDocID, DateTime from, DateTime to);

		DataSet GetPOSXReport(DateTime from, DateTime to, int shifID, int batchID, string registerID);

		DataSet GetPOSYReport(DateTime from, DateTime to, int shifID, int batchID, string registerID);

		DataSet GetPOSZReport(DateTime from, DateTime to, int shifID, int batchID, string registerID);

		bool ReCostTransaction(string sysDocID, string voucherID);
	}
}
