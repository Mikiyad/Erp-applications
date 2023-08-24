using Micromind.Common.Data;
using System;
using System.Data;

namespace Micromind.Common.Interfaces
{
	public interface IItemTransactionSystem
	{
		bool CreateItemTransaction(ItemTransactionData ItemTransactionData, bool isUpdate);

		ItemTransactionData GetItemTransactionByID(string sysDocID, string voucherID);

		bool DeleteItemTransaction(string sysDocID, string voucherID);

		bool VoidItemTransaction(string sysDocID, string voucherID, bool isVoid);

		DataSet GetUninvoicedItemTransactions(string customerID, bool isExport);

		DataSet GetItemTransactionToPrint(string sysDocID, string voucherID, bool showLotDetail);

		DataSet GetItemTransactionToPrint(string sysDocID, string[] voucherID, bool showLotDetail);

		DataSet GetList(DateTime from, DateTime to, bool showVoid);

		bool AllowModify(string sysDocID, string voucherNumber);

		DataSet GetItemTransactionList(string customerID, SysDocTypes docType);

		DataSet GetDOItemsToShip(string sysDocID, string voucherID);

		DataSet GetPackingListItemsToInvoice(string sysDocID, string voucherID);

		DataSet GetUninvoicedItemTransactions(string sysDocID, string customerID, bool isExport);

		string GetNextDocNumber(string SysDocID);

		DataSet GetMaxVoucherID();
	}
}
