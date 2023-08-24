using Micromind.Common.Data;
using Micromind.Common.Interfaces;
using Micromind.Data;
using System;
using System.Data;

namespace Micromind.Facade
{
	public sealed class W3PLGRNSystem : MarshalByRefObject, IW3PLGRNSystem, IDisposable
	{
		private Config config;

		public W3PLGRNSystem(Config config)
		{
			this.config = config;
		}

		public void Dispose()
		{
		}

		public bool CreateGRN(W3PLGRNData data, bool isUpdate)
		{
			return new W3PLGRN(config).InsertUpdateGRN(data, isUpdate);
		}

		public W3PLGRNData GetGRNByID(string sysDocID, string voucherID)
		{
			return new W3PLGRN(config).GetGRNByID(sysDocID, voucherID);
		}

		public bool DeleteGRN(string sysDocID, string voucherID)
		{
			return new W3PLGRN(config).DeleteGRN(sysDocID, voucherID);
		}

		public bool VoidGRN(string sysDocID, string voucherID, bool isVoid)
		{
			return new W3PLGRN(config).VoidGRN(sysDocID, voucherID, isVoid);
		}

		public DataSet GetUninvoicedGRNs(string customerID)
		{
			return new W3PLGRN(config).GetUninvoicedGRNs(customerID);
		}

		public DataSet GetGRNToPrint(string sysDocID, string voucherID)
		{
			return new W3PLGRN(config).GetGRNToPrint(sysDocID, voucherID);
		}

		public DataSet GetGRNToPrint(string sysDocID, string[] voucherID)
		{
			return new W3PLGRN(config).GetGRNToPrint(sysDocID, voucherID);
		}

		public DataSet GetList(DateTime from, DateTime to, bool showVoid)
		{
			return new W3PLGRN(config).GetList(from, to, showVoid);
		}

		public DataSet GetOpenGRNs(string vendorID)
		{
			return new W3PLGRN(config).GetConsignmentsByStatus(vendorID, W3PLGRNStatusEnum.Open);
		}

		public DataSet GetSettledConsignments(string vendorID)
		{
			return new W3PLGRN(config).GetConsignmentsByStatus(vendorID, W3PLGRNStatusEnum.Settled);
		}

		public bool AllowModify(string sysDocID, string voucherNumber)
		{
			return new W3PLGRN(config).AllowModify(sysDocID, voucherNumber, null);
		}

		public DataSet GetPendingConsignmentsReport(string fromVendor, string toVendor)
		{
			return new W3PLGRN(config).GetPendingConsignmentsReport(fromVendor, toVendor);
		}

		public DataSet GetConsignmentInSettlementReport(string fromVendor, string toVendor)
		{
			return new W3PLGRN(config).GetConsignmentInSettlementReport(fromVendor, toVendor);
		}

		public DataSet GetBillableItems(string consignSysDocID, string consignVoucherID)
		{
			return new W3PLGRN(config).GetBillableItems(consignSysDocID, consignVoucherID);
		}

		public DataSet GetGRNComboList()
		{
			return new W3PLGRN(config).GetGRNComboList();
		}

		public DataSet GetGRNClosingSummary(string sysDocID, string voucherID)
		{
			return new W3PLGRN(config).GetGRNClosingSummary(sysDocID, voucherID);
		}

		public bool CloseGRN(W3PLGRNData w3PLGRNData)
		{
			return new W3PLGRN(config).CloseGRN(w3PLGRNData);
		}

		public DataSet GetGRNReport(DateTime from, DateTime to, string fromVendor, string toVendor, string fromClass, string toClass, string fromGroup, string toGroup)
		{
			using (W3PLGRN w3PLGRN = new W3PLGRN(config))
			{
				return w3PLGRN.GetGRNReport(from, to, fromVendor, toVendor, fromClass, toClass, fromGroup, toGroup);
			}
		}

		public DataSet GetGRNList(string sysDocID, DateTime fromDate, DateTime endDate)
		{
			using (W3PLGRN w3PLGRN = new W3PLGRN(config))
			{
				return w3PLGRN.GetGRNList(sysDocID, fromDate, endDate);
			}
		}

		public DataSet GetGRNItemMovementReport(DateTime from, DateTime to, string fromItem, string toItem, string fromItemClass, string toItemClass, string fromCategory, string toCategory, string fromLocationID, string toLocationID, string sysDocID, string voucherID)
		{
			return new W3PLGRN(config).GetGRNItemMovementReport(from, to, fromItem, toItem, fromItemClass, toItemClass, fromCategory, toCategory, fromLocationID, toLocationID, sysDocID, voucherID);
		}

		public DataSet GetGRNSummaryReport(string sysDocID, string voucherID)
		{
			using (W3PLGRN w3PLGRN = new W3PLGRN(config))
			{
				return w3PLGRN.GetGRNSummaryReport(sysDocID, voucherID);
			}
		}

		public DataSet GetGRNsItemsToInvoice(string sysDocID, string voucherID, DateTime date)
		{
			using (W3PLGRN w3PLGRN = new W3PLGRN(config))
			{
				return w3PLGRN.GetGRNsItemsToInvoice(sysDocID, voucherID, date);
			}
		}
	}
}
