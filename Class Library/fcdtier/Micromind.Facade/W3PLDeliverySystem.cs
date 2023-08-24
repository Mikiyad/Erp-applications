using Micromind.Common.Data;
using Micromind.Common.Interfaces;
using Micromind.Data;
using System;
using System.Data;

namespace Micromind.Facade
{
	public sealed class W3PLDeliverySystem : MarshalByRefObject, IW3PLDeliverySystem, IDisposable
	{
		private Config config;

		public W3PLDeliverySystem(Config config)
		{
			this.config = config;
		}

		public void Dispose()
		{
		}

		public bool CreateDelivery(W3PLDeliveryData data, bool isUpdate)
		{
			return new W3PLDelivery(config).InsertUpdateDelivery(data, isUpdate);
		}

		public W3PLDeliveryData GetDeliveryByID(string sysDocID, string voucherID)
		{
			return new W3PLDelivery(config).GetDeliveryByID(sysDocID, voucherID);
		}

		public bool DeleteDelivery(string sysDocID, string voucherID)
		{
			return new W3PLDelivery(config).DeleteDelivery(sysDocID, voucherID);
		}

		public bool VoidDelivery(string sysDocID, string voucherID, bool isVoid)
		{
			return new W3PLDelivery(config).VoidDelivery(sysDocID, voucherID, isVoid);
		}

		public DataSet GetUninvoicedDeliverys(string customerID, bool isExport)
		{
			return new W3PLDelivery(config).GetUninvoicedDeliverys(customerID, isExport);
		}

		public DataSet GetDeliveryToPrint(string sysDocID, string voucherID)
		{
			return new W3PLDelivery(config).GetDeliveryToPrint(sysDocID, voucherID);
		}

		public DataSet GetDeliveryToPrint(string sysDocID, string[] voucherID)
		{
			return new W3PLDelivery(config).GetDeliveryToPrint(sysDocID, voucherID);
		}

		public DataSet GetList(DateTime from, DateTime to, bool showVoid)
		{
			return new W3PLDelivery(config).GetList(from, to, showVoid);
		}

		public DataSet GetDOsForPackingList(string customerID, bool isExport)
		{
			return new W3PLDelivery(config).GetDOsForPackingList(customerID, isExport);
		}

		public bool AllowModify(string sysDocID, string voucherNumber)
		{
			return new W3PLDelivery(config).AllowModify(sysDocID, voucherNumber);
		}

		public DataSet GetDOItemsToShip(string sysDocID, string voucherID)
		{
			return new W3PLDelivery(config).GetDOItemsToShip(sysDocID, voucherID);
		}

		public DataSet GetPackingListItemsToInvoice(string sysDocID, string voucherID)
		{
			return new W3PLDelivery(config).GetPackingListItemsToInvoice(sysDocID, voucherID);
		}

		public DataSet GetUninvoicedDeliverys(string sysDocID, string customerID, bool isExport)
		{
			return new W3PLDelivery(config).GetUninvoicedDeliverys(sysDocID, customerID, isExport);
		}
	}
}
