using Micromind.Common.Data;
using System;
using System.Data;

namespace Micromind.Common.Interfaces
{
	public interface IConsignInSettlementSystem
	{
		bool CreateSettlement(ConsignInSettlementData inventoryAdjustmentData, bool isUpdate);

		ConsignInSettlementData GetSettlementByID(string sysDocID, string voucherID);

		bool DeleteSettlement(string sysDocID, string voucherID);

		bool VoidSettlement(string sysDocID, string voucherID, bool isVoid);

		DataSet GetInvoicesForDelivery(string customerID);

		bool IsBelowMinPrice(string productID, string unitID, string currencyID, decimal currencyRate, decimal price);

		DataSet GetSettlementToPrint(string sysDocID, string[] voucherID);

		DataSet GetSettlementToPrint(string sysDocID, string voucherID);

		DataSet GetList(DateTime from, DateTime to, bool showVoid);

		DataSet GetConsignInSoldItems(string sysDocID, string voucherID);

		DataSet GetConsignInReceiptReport(DateTime fromrec, DateTime torec, DateTime fromset, DateTime toset, string fromvendor, string tovendor, string sysDocID, string voucherID, string vendorIDs);
	}
}
