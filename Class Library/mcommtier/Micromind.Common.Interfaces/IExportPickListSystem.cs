using Micromind.Common.Data;
using System;
using System.Data;

namespace Micromind.Common.Interfaces
{
	public interface IExportPickListSystem
	{
		bool CreateExportPickList(ExportPickListData deliveryNoteData, bool isUpdate);

		ExportPickListData GetExportPickListByID(string sysDocID, string voucherID);

		bool DeleteExportPickList(string sysDocID, string voucherID);

		bool VoidExportPickList(string sysDocID, string voucherID, bool isVoid);

		DataSet GetUninvoicedExportPickLists(string customerID, bool isExport);

		DataSet GetExportPickListToPrint(string sysDocID, string voucherID, bool showLotDetail);

		DataSet GetExportPickListToPrint(string sysDocID, string[] voucherID, bool showLotDetail);

		DataSet GetList(DateTime from, DateTime to, bool showVoid);

		bool AllowModify(string sysDocID, string voucherNumber);

		DataSet GetDOsForPackingList(string customerID, bool isExport);

		DataSet GetDOItemsToShip(string sysDocID, string voucherID);

		DataSet GetPackingListItemsToInvoice(string sysDocID, string voucherID);

		DataSet GetUninvoicedExportPickLists(string sysDocID, string customerID, bool isExport);

		bool AllowDelete(string sysDocID, string voucherNumber);
	}
}
