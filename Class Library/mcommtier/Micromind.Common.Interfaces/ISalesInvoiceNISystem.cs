using Micromind.Common.Data;
using System;
using System.Data;

namespace Micromind.Common.Interfaces
{
	public interface ISalesInvoiceNISystem
	{
		bool CreateSalesInvoice(SalesInvoiceNIData inventoryAdjustmentData, bool isUpdate);

		SalesInvoiceNIData GetSalesInvoiceByID(string sysDocID, string voucherID);

		bool DeleteSalesInvoice(string sysDocID, string voucherID);

		bool VoidSalesInvoice(string sysDocID, string voucherID, bool isVoid);

		bool IsBelowMinPrice(string productID, string unitID, string currencyID, decimal currencyRate, decimal price, string locationID = "");

		DataSet GetSalesInvoiceToPrint(string sysDocID, string[] voucherID, bool mergeMatrixItems, bool showLotDetail, NonInventoryInvoiceType invoiceType);

		DataSet GetSalesInvoiceToPrint(string sysDocID, string voucherID, bool mergeMatrixItems, bool showLotDetail, NonInventoryInvoiceType invoiceType);

		DataSet GetList(DateTime from, DateTime to, bool isExport, bool showVoid, string sysDocID, bool IsCash, NonInventoryInvoiceType invoiceType);

		bool InvoiceHasShippedQuantity(string sysDocID, string voucherNumber);

		bool IsBelowAverageCost(string productID, string unitID, string currencyID, decimal currencyRate, decimal price);

		bool ModifyTransactions(string sysDocID, string voucherNumber, string userID, bool isModify, string toUpdate);

		bool IsBelowMinAllowedPrice(string productID, string unitID, string currencyID, decimal currencyRate, decimal price, string locationID = "");

		bool AllowModify(string sysDocID, string voucherNumber);

		SalesInvoiceNIData GetServiceInvoiceByID(string sysDocID, string voucherID);
	}
}
