using Micromind.Common.Data;
using Micromind.Common.Interfaces;
using Micromind.Data;
using System;
using System.Data;

namespace Micromind.Facade
{
	public sealed class DeliveryNoteSystem : MarshalByRefObject, IDeliveryNoteSystem, IDisposable
	{
		private Config config;

		public DeliveryNoteSystem(Config config)
		{
			this.config = config;
		}

		public void Dispose()
		{
		}

		public bool CreateDeliveryNote(DeliveryNoteData data, bool isUpdate, bool TempSave)
		{
			return new DeliveryNote(config).InsertUpdateDeliveryNote(data, isUpdate, TempSave);
		}

		public DeliveryNoteData GetDeliveryNoteByID(string sysDocID, string voucherID)
		{
			return new DeliveryNote(config).GetDeliveryNoteByID(sysDocID, voucherID);
		}

		public bool DeleteDeliveryNote(string sysDocID, string voucherID)
		{
			return new DeliveryNote(config).DeleteDeliveryNote(sysDocID, voucherID);
		}

		public bool VoidDeliveryNote(string sysDocID, string voucherID, bool isVoid)
		{
			return new DeliveryNote(config).VoidDeliveryNote(sysDocID, voucherID, isVoid);
		}

		public DataSet GetUninvoicedDeliveryNotes(string customerID, bool isExport)
		{
			return new DeliveryNote(config).GetUninvoicedDeliveryNotes(customerID, isExport);
		}

		public DataSet GetDeliveryNoteToPrint(string sysDocID, string voucherID, bool showLotDetail)
		{
			return new DeliveryNote(config).GetDeliveryNoteToPrint(sysDocID, voucherID, showLotDetail);
		}

		public DataSet GetDeliveryNoteToPrint(string sysDocID, string voucherID, bool showLotDetail, bool excludeZeroQtyInDN)
		{
			return new DeliveryNote(config).GetDeliveryNoteToPrint(sysDocID, voucherID, showLotDetail, excludeZeroQtyInDN);
		}

		public DataSet GetDeliveryNoteToPrint(string sysDocID, string[] voucherID, bool showLotDetail, bool excludeZeroQtyInDN)
		{
			return new DeliveryNote(config).GetDeliveryNoteToPrint(sysDocID, voucherID, showLotDetail, excludeZeroQtyInDN);
		}

		public DataSet GetDeliveryNoteToPrint(string sysDocID, string[] voucherID, bool showLotDetail)
		{
			return new DeliveryNote(config).GetDeliveryNoteToPrint(sysDocID, voucherID, showLotDetail);
		}

		public DataSet GetList(DateTime from, DateTime to, bool showVoid, string SysDocID)
		{
			return new DeliveryNote(config).GetList(from, to, showVoid, SysDocID);
		}

		public DataSet GetDOsForPackingList(string customerID, bool isExport)
		{
			return new DeliveryNote(config).GetDOsForPackingList(customerID, isExport);
		}

		public DataSet GetDOsForShipment(string customerID)
		{
			return new DeliveryNote(config).GetDOsForShipment(customerID);
		}

		public DataSet GetDOsForShipment(string sysDocID, string customerID, string locationID)
		{
			return new DeliveryNote(config).GetDOsForShipment(sysDocID, customerID, locationID);
		}

		public bool AllowModify(string sysDocID, string voucherNumber)
		{
			return new DeliveryNote(config).AllowModify(sysDocID, voucherNumber);
		}

		public DataSet GetDOItemsToShip(string sysDocID, string voucherID)
		{
			return new DeliveryNote(config).GetDOItemsToShip(sysDocID, voucherID);
		}

		public DataSet GetPackingListItemsToInvoice(string sysDocID, string voucherID)
		{
			return new DeliveryNote(config).GetPackingListItemsToInvoice(sysDocID, voucherID);
		}

		public DataSet GetUninvoicedDeliveryNotes(string sysDocID, string customerID, bool isExport)
		{
			return new DeliveryNote(config).GetUninvoicedDeliveryNotes(sysDocID, customerID, isExport);
		}

		public DataSet GetUninvoicedDeliveryNotesOnLocation(string sysDocID, string customerID, bool isExport, string location)
		{
			return new DeliveryNote(config).GetUninvoicedDeliveryNotesOnLocation(sysDocID, customerID, isExport, location);
		}

		public bool ModifyTransactions(string sysDocID, string voucherNumber, string userID, bool isModify, string toUpdate)
		{
			return new DeliveryNote(config).ModifyTransactions(sysDocID, voucherNumber, userID, isModify, toUpdate);
		}

		public bool ReCostTransaction(string sysDocID, string voucherID)
		{
			return new DeliveryNote(config).ReCostTransaction(sysDocID, voucherID);
		}
	}
}
