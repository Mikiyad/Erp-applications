using Micromind.Common.Data;
using Micromind.Common.Interfaces;
using Micromind.Data;
using System;
using System.Data;

namespace Micromind.Facade
{
	public sealed class InventoryTransferSystem : MarshalByRefObject, IInventoryTransferSystem, IDisposable
	{
		private Config config;

		public InventoryTransferSystem(Config config)
		{
			this.config = config;
		}

		public void Dispose()
		{
		}

		public bool CreateInventoryTransfer(InventoryTransferData data, bool isUpdate)
		{
			return new InventoryTransfer(config).InsertUpdateInventoryTransfer(data, isUpdate);
		}

		public InventoryTransferData GetInventoryTransferByID(string sysDocID, string voucherID)
		{
			return new InventoryTransfer(config).GetInventoryTransferByID(sysDocID, voucherID);
		}

		public InventoryTransferData GetInventoryTransferAcceptanceByID(string acceptSysDocID, string voucherID)
		{
			return new InventoryTransfer(config).GetInventoryTransferAcceptanceByID(acceptSysDocID, voucherID);
		}

		public InventoryTransferData GetInventoryTransferRejectionByID(string rejectSysDocID, string voucherID)
		{
			return new InventoryTransfer(config).GetInventoryTransferRejectionByID(rejectSysDocID, voucherID);
		}

		public bool DeleteInventoryTransfer(string sysDocID, string voucherID)
		{
			return new InventoryTransfer(config).DeleteInventoryTransfer(sysDocID, voucherID);
		}

		public DataSet GetInventoryTransfersToAccept(string locationID)
		{
			return new InventoryTransfer(config).GetInventoryTransfersToAccept(locationID);
		}

		public bool AcceptInventoryTransfer(InventoryTransferData inventoryTransferData, bool isUpdate)
		{
			return new InventoryTransfer(config).AcceptInventoryTransfer(inventoryTransferData, isUpdate);
		}

		public bool VoidInventoryTransfer(string sysDocID, string voucherID, bool isVoid)
		{
			return new InventoryTransfer(config).VoidInventoryTransfer(sysDocID, voucherID, isVoid);
		}

		public DataSet GetInventoryTransfersToReturn(string locationID)
		{
			return new InventoryTransfer(config).GetInventoryTransfersToReturn(locationID);
		}

		public bool RejectInventoryTransfer(InventoryTransferData inventoryTransferData)
		{
			return new InventoryTransfer(config).RejectInventoryTransfer(inventoryTransferData);
		}

		public bool AcceptRejectedInventoryTransfer(InventoryTransferData inventoryTransferData, bool isUpdate)
		{
			return new InventoryTransfer(config).AcceptRejectedInventoryTransfer(inventoryTransferData, isUpdate);
		}

		public bool InsertUpdateDirectInventoryTransfer(InventoryTransferData inventoryTransferData, bool isUpdate)
		{
			return new InventoryTransfer(config).InsertUpdateDirectInventoryTransfer(inventoryTransferData, isUpdate);
		}

		public DataSet GetInventoryTransferReport(DateTime from, DateTime to, string warehouseCode, bool isTransferOut)
		{
			return new InventoryTransfer(config).GetInventoryTransferReport(from, to, warehouseCode, isTransferOut);
		}

		public DataSet GetInventoryTransferToPrint(string sysDocID, string voucherID)
		{
			return new InventoryTransfer(config).GetInventoryTransferToPrint(sysDocID, voucherID);
		}

		public DataSet GetRejectedInventoryTransferToPrint(string sysDocID, string voucherID)
		{
			return new InventoryTransfer(config).GetRejectedInventoryTransferToPrint(sysDocID, voucherID);
		}

		public DataSet GetReceivedInventoryTransferToPrint(string sysDocID, string voucherID)
		{
			return new InventoryTransfer(config).GetReceivedInventoryTransferToPrint(sysDocID, voucherID);
		}

		public string GetNextAcceptVoucherID(string sysDocID, string voucherID)
		{
			return new InventoryTransfer(config).GetNextAcceptVoucherID(sysDocID, voucherID, 1);
		}

		public string GetPreviousAcceptVoucherID(string sysDocID, string voucherID)
		{
			return new InventoryTransfer(config).GetNextAcceptVoucherID(sysDocID, voucherID, 2);
		}

		public string GetLastAcceptVoucherID(string sysDocID)
		{
			return new InventoryTransfer(config).GetNextAcceptVoucherID(sysDocID, "", 3);
		}

		public string GetFirstAcceptVoucherID(string sysDocID)
		{
			return new InventoryTransfer(config).GetNextAcceptVoucherID(sysDocID, "", 4);
		}

		public DataSet GetListInventoryTransfer(DateTime from, DateTime to, bool showVoid, string sysDocID)
		{
			return new InventoryTransfer(config).GetListInventoryTransfer(from, to, showVoid, sysDocID);
		}

		public DataSet GetListInventoryTransfersToAccept(DateTime from, DateTime to, bool showVoid)
		{
			return new InventoryTransfer(config).GetListInventoryTransfersToAccept(from, to, showVoid);
		}

		public DataSet GetListInventoryTransferReturn(DateTime from, DateTime to, bool showVoid)
		{
			return new InventoryTransfer(config).GetListInventoryTransferReturn(from, to, showVoid);
		}

		public DataSet GetListDirectInventoryTransfer(DateTime from, DateTime to, bool showVoid)
		{
			return new InventoryTransfer(config).GetListDirectInventoryTransfer(from, to, showVoid);
		}

		public bool ReCostTransferTransaction(string sysDocID, string voucherID, SysDocTypes docType)
		{
			return new InventoryTransfer(config).ReCostTransferTransaction(sysDocID, voucherID, docType);
		}

		public string FindDocumentByNumber(string tableName, string fieldName, string sysDocID, string numberQuery)
		{
			return new InventoryTransfer(config).FindDocumentByNumber(tableName, fieldName, sysDocID, "", null, numberQuery);
		}
	}
}
