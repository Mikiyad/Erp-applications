using Micromind.Common.Data;
using Micromind.Common.Interfaces;
using Micromind.Data;
using System;
using System.Data;

namespace Micromind.Facade
{
	public sealed class ItemTransactionSystem : MarshalByRefObject, IItemTransactionSystem, IDisposable
	{
		private Config config;

		public ItemTransactionSystem(Config config)
		{
			this.config = config;
		}

		public void Dispose()
		{
		}

		public bool CreateItemTransaction(ItemTransactionData data, bool isUpdate)
		{
			return new ItemTransaction(config).InsertUpdateItemTransaction(data, isUpdate);
		}

		public ItemTransactionData GetItemTransactionByID(string sysDocID, string voucherID)
		{
			return new ItemTransaction(config).GetItemTransactionByID(sysDocID, voucherID);
		}

		public bool DeleteItemTransaction(string sysDocID, string voucherID)
		{
			return new ItemTransaction(config).DeleteItemTransaction(sysDocID, voucherID);
		}

		public bool VoidItemTransaction(string sysDocID, string voucherID, bool isVoid)
		{
			return new ItemTransaction(config).VoidItemTransaction(sysDocID, voucherID, isVoid);
		}

		public DataSet GetUninvoicedItemTransactions(string customerID, bool isExport)
		{
			return new ItemTransaction(config).GetUninvoicedItemTransactions(customerID, isExport);
		}

		public DataSet GetItemTransactionToPrint(string sysDocID, string voucherID, bool showLotDetail)
		{
			return new ItemTransaction(config).GetItemTransactionToPrint(sysDocID, voucherID, showLotDetail);
		}

		public DataSet GetItemTransactionToPrint(string sysDocID, string[] voucherID, bool showLotDetail)
		{
			return new ItemTransaction(config).GetItemTransactionToPrint(sysDocID, voucherID, showLotDetail);
		}

		public DataSet GetList(DateTime from, DateTime to, bool showVoid)
		{
			return new ItemTransaction(config).GetList(from, to, showVoid);
		}

		public DataSet GetItemTransactionList(string customerID, SysDocTypes docType)
		{
			return new ItemTransaction(config).GetItemTransactionList(customerID, docType);
		}

		public bool AllowModify(string sysDocID, string voucherNumber)
		{
			return new ItemTransaction(config).AllowModify(sysDocID, voucherNumber);
		}

		public DataSet GetDOItemsToShip(string sysDocID, string voucherID)
		{
			return new ItemTransaction(config).GetDOItemsToShip(sysDocID, voucherID);
		}

		public DataSet GetPackingListItemsToInvoice(string sysDocID, string voucherID)
		{
			return new ItemTransaction(config).GetPackingListItemsToInvoice(sysDocID, voucherID);
		}

		public DataSet GetUninvoicedItemTransactions(string sysDocID, string customerID, bool isExport)
		{
			return new ItemTransaction(config).GetUninvoicedItemTransactions(sysDocID, customerID, isExport);
		}

		public string GetNextDocNumber(string SysDocID)
		{
			using (ItemTransaction itemTransaction = new ItemTransaction(config))
			{
				return itemTransaction.GetNextDocNumber(SysDocID);
			}
		}

		public DataSet GetMaxVoucherID()
		{
			return new ItemTransaction(config).GetMaxVoucherID();
		}
	}
}
