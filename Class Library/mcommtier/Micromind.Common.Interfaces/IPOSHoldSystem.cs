using Micromind.Common.Data;
using System.Data;

namespace Micromind.Common.Interfaces
{
	public interface IPOSHoldSystem
	{
		bool HoldSalesReceipt(SalesPOSData receiptData, bool isUpdate);

		POSHoldData GetPOSHoldByID(string sysDocID, string voucherID);

		bool DeletePOSHold(string voucherID);

		bool VoidPOSHold(string sysDocID, string voucherID, bool isVoid);

		DataSet GetInvoicesForDelivery(string customerID);

		bool IsBelowMinPrice(string productID, string unitID, string currencyID, decimal currencyRate, decimal price);

		DataSet GetPOSHoldToPrint(string sysDocID, string[] voucherID);

		DataSet GetPOSHoldToPrint(string sysDocID, string voucherID);

		DataSet GetHoldDocumentList(string registerID);

		bool SetSearchValue(string sysDocID, string voucherID, string shiftID);
	}
}
