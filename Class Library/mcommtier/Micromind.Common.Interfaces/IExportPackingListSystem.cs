using Micromind.Common.Data;
using System;
using System.Data;

namespace Micromind.Common.Interfaces
{
	public interface IExportPackingListSystem
	{
		bool CreateExportPackingList(ExportPackingListData inventoryAdjustmentData, bool isUpdate);

		ExportPackingListData GetExportPackingListByID(string sysDocID, string voucherID);

		bool DeleteExportPackingList(string sysDocID, string voucherID);

		bool VoidExportPackingList(string sysDocID, string voucherID, bool isVoid);

		DataSet GetOpenShipmentsSummary(string vendorID);

		DataSet GetOpenShipmentsReport();

		DataSet GetExportPackingListToPrint(string sysDocID, string[] voucherID);

		DataSet GetExportPackingListToPrint(string sysDocID, string voucherID);

		DataSet GetList(DateTime from, DateTime to, bool showVoid, bool isShipment);

		DataSet GetPackingListsForInvoice(string customerID);
	}
}
