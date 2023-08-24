using Micromind.Common.Data;
using System;
using System.Data;

namespace Micromind.Common.Interfaces
{
	public interface IInventoryTransferSystem
	{
		bool CreateInventoryTransfer(InventoryTransferData inventoryTransferData, bool isUpdate);

		InventoryTransferData GetInventoryTransferByID(string sysDocID, string voucherID);

		InventoryTransferData GetInventoryTransferAcceptanceByID(string acceptanceSysDocID, string voucherID);

		InventoryTransferData GetInventoryTransferRejectionByID(string rejectSysDocID, string voucherID);

		bool DeleteInventoryTransfer(string sysDocID, string voucherID);

		DataSet GetInventoryTransfersToAccept(string locationID);

		bool AcceptInventoryTransfer(InventoryTransferData inventoryTransferData, bool isUpdate);

		bool VoidInventoryTransfer(string sysDocID, string voucherID, bool isVoid);

		DataSet GetInventoryTransfersToReturn(string locationID);

		bool RejectInventoryTransfer(InventoryTransferData inventoryTransferData);

		bool AcceptRejectedInventoryTransfer(InventoryTransferData inventoryTransferData, bool isUpdate);

		bool InsertUpdateDirectInventoryTransfer(InventoryTransferData inventoryTransferData, bool isUpdate);

		DataSet GetInventoryTransferReport(DateTime from, DateTime to, string warehouseCode, bool isTransferOut);

		DataSet GetInventoryTransferToPrint(string sysDocID, string voucherID);

		DataSet GetRejectedInventoryTransferToPrint(string sysDocID, string voucherID);

		DataSet GetReceivedInventoryTransferToPrint(string sysDocID, string voucherID);

		string GetNextAcceptVoucherID(string acceptSysDocID, string acceptVoucherID);

		string GetPreviousAcceptVoucherID(string acceptSysDocID, string acceptVoucherID);

		string GetLastAcceptVoucherID(string acceptSysDocID);

		string GetFirstAcceptVoucherID(string acceptSysDocID);

		DataSet GetListInventoryTransfer(DateTime from, DateTime to, bool showVoid, string sysDocID);

		DataSet GetListDirectInventoryTransfer(DateTime from, DateTime to, bool showVoid);

		DataSet GetListInventoryTransfersToAccept(DateTime from, DateTime to, bool showVoid);

		DataSet GetListInventoryTransferReturn(DateTime from, DateTime to, bool showVoid);

		bool ReCostTransferTransaction(string sysDocID, string voucherID, SysDocTypes docType);

		string FindDocumentByNumber(string tableName, string fieldName, string sysDocID, string numberQuery);
	}
}
