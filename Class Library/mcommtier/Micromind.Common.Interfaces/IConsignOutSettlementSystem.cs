using Micromind.Common.Data;
using System;
using System.Data;

namespace Micromind.Common.Interfaces
{
	public interface IConsignOutSettlementSystem
	{
		bool CreateSettlement(ConsignOutSettlementData inventoryAdjustmentData, bool isUpdate);

		bool ReCostTransaction(string sysDocID, string voucherID);

		ConsignOutSettlementData GetSettlementByID(string sysDocID, string voucherID);

		bool DeleteSettlement(string sysDocID, string voucherID);

		bool VoidSettlement(string sysDocID, string voucherID, bool isVoid);

		DataSet GetInvoicesForDelivery(string customerID);

		bool IsBelowMinPrice(string productID, string unitID, string currencyID, decimal currencyRate, decimal price);

		DataSet GetSettlementToPrint(string sysDocID, string[] voucherID);

		DataSet GetSettlementToPrint(string sysDocID, string voucherID);

		DataSet GetList(DateTime from, DateTime to, bool showVoid);
	}
}
