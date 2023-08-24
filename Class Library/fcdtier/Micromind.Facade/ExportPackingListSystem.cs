using Micromind.Common.Data;
using Micromind.Common.Interfaces;
using Micromind.Data;
using System;
using System.Data;

namespace Micromind.Facade
{
	public sealed class ExportPackingListSystem : MarshalByRefObject, IExportPackingListSystem, IDisposable
	{
		private Config config;

		public ExportPackingListSystem(Config config)
		{
			this.config = config;
		}

		public void Dispose()
		{
		}

		public bool CreateExportPackingList(ExportPackingListData data, bool isUpdate)
		{
			return new ExportPackingList(config).InsertUpdateExportPackingList(data, isUpdate);
		}

		public ExportPackingListData GetExportPackingListByID(string sysDocID, string voucherID)
		{
			return new ExportPackingList(config).GetExportPackingListByID(sysDocID, voucherID);
		}

		public bool DeleteExportPackingList(string sysDocID, string voucherID)
		{
			return new ExportPackingList(config).DeleteExportPackingList(sysDocID, voucherID);
		}

		public bool VoidExportPackingList(string sysDocID, string voucherID, bool isVoid)
		{
			return new ExportPackingList(config).VoidExportPackingList(sysDocID, voucherID, isVoid);
		}

		public DataSet GetOpenShipmentsSummary(string customerID)
		{
			return new ExportPackingList(config).GetOpenShipmentsSummary(customerID);
		}

		public DataSet GetOpenShipmentsReport()
		{
			return new ExportPackingList(config).GetOpenShipmentsReport();
		}

		public DataSet GetExportPackingListToPrint(string sysDocID, string[] voucherID)
		{
			return new ExportPackingList(config).GetExportPackingListToPrint(sysDocID, voucherID);
		}

		public DataSet GetExportPackingListToPrint(string sysDocID, string voucherID)
		{
			return new ExportPackingList(config).GetExportPackingListToPrint(sysDocID, new string[1]
			{
				voucherID
			});
		}

		public DataSet GetList(DateTime from, DateTime to, bool showVoid, bool isShipment)
		{
			return new ExportPackingList(config).GetList(from, to, showVoid, isShipment);
		}

		public DataSet GetPackingListsForInvoice(string customerID)
		{
			return new ExportPackingList(config).GetPackingListsForInvoice(customerID);
		}
	}
}
