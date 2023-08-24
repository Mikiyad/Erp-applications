using Micromind.Common.Data;
using System.Data;

namespace Micromind.Common.Interfaces
{
	public interface ISalesReceiptSystem
	{
		bool CreateSalesReceipt(SalesReceiptData inventoryAdjustmentData, bool isUpdate);

		SalesReceiptData GetSalesReceiptByID(string sysDocID, string voucherID);

		bool DeleteSalesReceipt(string sysDocID, string voucherID);

		bool VoidSalesReceipt(string sysDocID, string voucherID, bool isVoid);

		DataSet GetReceiptsForDelivery(string customerID);

		decimal GetProductSalesPrice(string productID, string customerID, string locationID, string UnitID);
	}
}
