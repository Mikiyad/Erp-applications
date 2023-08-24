using Micromind.Common.Data;
using System;
using System.Data;

namespace Micromind.Common.Interfaces
{
	public interface IW3PLDeliverySystem
	{
		bool CreateDelivery(W3PLDeliveryData deliveryData, bool isUpdate);

		W3PLDeliveryData GetDeliveryByID(string sysDocID, string voucherID);

		bool DeleteDelivery(string sysDocID, string voucherID);

		bool VoidDelivery(string sysDocID, string voucherID, bool isVoid);

		DataSet GetUninvoicedDeliverys(string customerID, bool isExport);

		DataSet GetDeliveryToPrint(string sysDocID, string voucherID);

		DataSet GetDeliveryToPrint(string sysDocID, string[] voucherID);

		DataSet GetList(DateTime from, DateTime to, bool showVoid);

		bool AllowModify(string sysDocID, string voucherNumber);

		DataSet GetDOsForPackingList(string customerID, bool isExport);

		DataSet GetDOItemsToShip(string sysDocID, string voucherID);

		DataSet GetPackingListItemsToInvoice(string sysDocID, string voucherID);

		DataSet GetUninvoicedDeliverys(string sysDocID, string customerID, bool isExport);
	}
}
