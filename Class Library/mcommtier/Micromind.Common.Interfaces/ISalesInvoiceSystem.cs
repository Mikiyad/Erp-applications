using Micromind.Common.Data;
using System;
using System.Data;

namespace Micromind.Common.Interfaces
{
	public interface ISalesInvoiceSystem
	{
		bool CreateSalesInvoice(SalesInvoiceData inventoryAdjustmentData, bool isUpdate, bool TempSave);

		SalesInvoiceData GetSalesInvoiceByID(string sysDocID, string voucherID);

		bool DeleteSalesInvoice(string sysDocID, string voucherID);

		bool VoidSalesInvoice(string sysDocID, string voucherID, bool isVoid);

		DataSet GetInvoicesForDelivery(string customerID, bool isExport);

		DataSet GetInvoicesForDeliveryOnLocation(string customerID, bool isExport, string locationID);

		bool IsBelowMinPrice(string productID, string unitID, string currencyID, decimal currencyRate, decimal price, string locationID = "");

		DataSet GetSalesInvoiceToPrint(string sysDocID, string[] voucherID, bool mergeMatrixItems, bool showLotDetail);

		DataSet GetSalesInvoiceToPrint(string sysDocID, string voucherID, bool mergeMatrixItems, bool showLotDetail);

		DataSet GetList(DateTime from, DateTime to, bool isExport, bool showVoid, string sysDocID, bool IsCash);

		bool InvoiceHasShippedQuantity(string sysDocID, string voucherNumber);

		bool IsBelowAverageCost(string productID, string unitID, string currencyID, decimal currencyRate, decimal price);

		bool ModifyTransactions(string sysDocID, string voucherNumber, string userID, bool isModify, string toUpdate);

		bool IsBelowMinAllowedPrice(string productID, string unitID, string currencyID, decimal currencyRate, decimal price, string locationID = "");

		bool AllowModify(string sysDocID, string voucherNumber);

		DataSet GetPaymentAllocationDetails(string sysDocID, string voucherID);

		bool ReCostTransaction(string sysDocID, string voucherID);

		DataSet GetInvoicesForBillDiscountOnLocation(bool isExport, DateTime from, DateTime to);

		DataSet GetSalesTransactionsbyID(string sysDocID, string voucherID);
	}
}
