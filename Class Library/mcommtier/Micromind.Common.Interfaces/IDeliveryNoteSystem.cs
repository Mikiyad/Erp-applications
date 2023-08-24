using Micromind.Common.Data;
using System;
using System.Data;

namespace Micromind.Common.Interfaces
{
	public interface IDeliveryNoteSystem
	{
		bool CreateDeliveryNote(DeliveryNoteData deliveryNoteData, bool isUpdate, bool TempSave);

		DeliveryNoteData GetDeliveryNoteByID(string sysDocID, string voucherID);

		bool DeleteDeliveryNote(string sysDocID, string voucherID);

		bool VoidDeliveryNote(string sysDocID, string voucherID, bool isVoid);

		DataSet GetUninvoicedDeliveryNotes(string customerID, bool isExport);

		DataSet GetDeliveryNoteToPrint(string sysDocID, string voucherID, bool showLotDetail);

		DataSet GetDeliveryNoteToPrint(string sysDocID, string[] voucherID, bool showLotDetail);

		DataSet GetDeliveryNoteToPrint(string sysDocID, string voucherID, bool showLotDetail, bool exclueZeroQtyInDN);

		DataSet GetDeliveryNoteToPrint(string sysDocID, string[] voucherID, bool showLotDetail, bool exclueZeroQtyInDN);

		DataSet GetList(DateTime from, DateTime to, bool showVoid, string SysDocID);

		bool AllowModify(string sysDocID, string voucherNumber);

		DataSet GetDOsForPackingList(string customerID, bool isExport);

		DataSet GetDOsForShipment(string customerID);

		DataSet GetDOsForShipment(string sysDocID, string customerID, string locationID);

		DataSet GetDOItemsToShip(string sysDocID, string voucherID);

		DataSet GetPackingListItemsToInvoice(string sysDocID, string voucherID);

		DataSet GetUninvoicedDeliveryNotes(string sysDocID, string customerID, bool isExport);

		DataSet GetUninvoicedDeliveryNotesOnLocation(string sysDocID, string customerID, bool isExport, string locationID);

		bool ModifyTransactions(string sysDocID, string voucherNumber, string userID, bool isModify, string toUpdate);

		bool ReCostTransaction(string sysDocID, string voucherID);
	}
}
