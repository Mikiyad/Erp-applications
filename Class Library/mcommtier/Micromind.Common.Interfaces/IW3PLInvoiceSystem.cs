using Micromind.Common.Data;
using System;
using System.Data;

namespace Micromind.Common.Interfaces
{
	public interface IW3PLInvoiceSystem
	{
		bool CreateInvoice(W3PLInvoiceData inventoryAdjustmentData, bool isUpdate);

		W3PLInvoiceData GetInvoiceByID(string sysDocID, string voucherID);

		bool DeleteInvoice(string sysDocID, string voucherID);

		bool VoidInvoice(string sysDocID, string voucherID, bool isVoid);

		DataSet GetInvoicesForDelivery(string customerID);

		bool IsBelowMinPrice(string productID, string unitID, string currencyID, decimal currencyRate, decimal price);

		DataSet GetInvoiceToPrint(string sysDocID, string[] voucherID);

		DataSet GetInvoiceToPrint(string sysDocID, string voucherID);

		DataSet GetList(DateTime from, DateTime to, bool showVoid);

		DataSet GetConsignInSoldItems(string sysDocID, string voucherID);

		DataSet GetConsignInReceiptReport(DateTime fromrec, DateTime torec, DateTime fromset, DateTime toset, string fromvendor, string tovendor, string sysDocID, string voucherID);
	}
}
