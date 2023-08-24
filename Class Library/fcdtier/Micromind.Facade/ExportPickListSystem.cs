using Micromind.Common.Data;
using Micromind.Common.Interfaces;
using Micromind.Data;
using System;
using System.Data;

namespace Micromind.Facade
{
	public sealed class ExportPickListSystem : MarshalByRefObject, IExportPickListSystem, IDisposable
	{
		private Config config;

		public ExportPickListSystem(Config config)
		{
			this.config = config;
		}

		public void Dispose()
		{
		}

		public bool CreateExportPickList(ExportPickListData data, bool isUpdate)
		{
			return new ExportPickList(config).InsertUpdateExportPickList(data, isUpdate);
		}

		public ExportPickListData GetExportPickListByID(string sysDocID, string voucherID)
		{
			return new ExportPickList(config).GetExportPickListByID(sysDocID, voucherID);
		}

		public bool DeleteExportPickList(string sysDocID, string voucherID)
		{
			return new ExportPickList(config).DeleteExportPickList(sysDocID, voucherID);
		}

		public bool VoidExportPickList(string sysDocID, string voucherID, bool isVoid)
		{
			return new ExportPickList(config).VoidExportPickList(sysDocID, voucherID, isVoid);
		}

		public DataSet GetUninvoicedExportPickLists(string customerID, bool isExport)
		{
			return new ExportPickList(config).GetUninvoicedExportPickLists(customerID, isExport);
		}

		public DataSet GetExportPickListToPrint(string sysDocID, string voucherID, bool showLotDetail)
		{
			return new ExportPickList(config).GetExportPickListToPrint(sysDocID, voucherID, showLotDetail);
		}

		public DataSet GetExportPickListToPrint(string sysDocID, string[] voucherID, bool showLotDetail)
		{
			return new ExportPickList(config).GetExportPickListToPrint(sysDocID, voucherID, showLotDetail);
		}

		public DataSet GetList(DateTime from, DateTime to, bool showVoid)
		{
			return new ExportPickList(config).GetList(from, to, showVoid);
		}

		public DataSet GetDOsForPackingList(string customerID, bool isExport)
		{
			return new ExportPickList(config).GetDOsForPackingList(customerID, isExport);
		}

		public bool AllowModify(string sysDocID, string voucherNumber)
		{
			return new ExportPickList(config).AllowModify(sysDocID, voucherNumber);
		}

		public DataSet GetDOItemsToShip(string sysDocID, string voucherID)
		{
			return new ExportPickList(config).GetDOItemsToShip(sysDocID, voucherID);
		}

		public DataSet GetPackingListItemsToInvoice(string sysDocID, string voucherID)
		{
			return new ExportPickList(config).GetPackingListItemsToInvoice(sysDocID, voucherID);
		}

		public DataSet GetUninvoicedExportPickLists(string sysDocID, string customerID, bool isExport)
		{
			return new ExportPickList(config).GetUninvoicedExportPickLists(sysDocID, customerID, isExport);
		}

		public bool AllowDelete(string sysDocID, string voucherNumber)
		{
			return new ExportPickList(config).AllowDelete(sysDocID, voucherNumber);
		}
	}
}
