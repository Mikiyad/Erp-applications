using Micromind.Common.Data;
using Micromind.Common.Interfaces;
using Micromind.Data;
using System;
using System.Data;

namespace Micromind.Facade
{
	public sealed class ConsignInSystem : MarshalByRefObject, IConsignInSystem, IDisposable
	{
		private Config config;

		public ConsignInSystem(Config config)
		{
			this.config = config;
		}

		public void Dispose()
		{
		}

		public bool CreateConsignIn(ConsignInData data, bool isUpdate)
		{
			return new ConsignIn(config).InsertUpdateConsignIn(data, isUpdate);
		}

		public ConsignInData GetConsignInByID(string sysDocID, string voucherID)
		{
			return new ConsignIn(config).GetConsignInByID(sysDocID, voucherID);
		}

		public bool DeleteConsignIn(string sysDocID, string voucherID)
		{
			return new ConsignIn(config).DeleteConsignIn(sysDocID, voucherID);
		}

		public bool VoidConsignIn(string sysDocID, string voucherID, bool isVoid)
		{
			return new ConsignIn(config).VoidConsignIn(sysDocID, voucherID, isVoid);
		}

		public DataSet GetUninvoicedConsignIns(string customerID)
		{
			return new ConsignIn(config).GetUninvoicedConsignIns(customerID);
		}

		public DataSet GetConsignInToPrint(string sysDocID, string voucherID)
		{
			return new ConsignIn(config).GetConsignInToPrint(sysDocID, voucherID);
		}

		public DataSet GetConsignInToPrint(string sysDocID, string[] voucherID)
		{
			return new ConsignIn(config).GetConsignInToPrint(sysDocID, voucherID);
		}

		public DataSet GetList(DateTime from, DateTime to, bool showVoid)
		{
			return new ConsignIn(config).GetList(from, to, showVoid);
		}

		public DataSet GetOpenConsignments(string vendorID)
		{
			return new ConsignIn(config).GetConsignmentsByStatus(vendorID, ConsignInStatusEnum.Open);
		}

		public DataSet GetSettledConsignments(string vendorID)
		{
			return new ConsignIn(config).GetConsignmentsByStatus(vendorID, ConsignInStatusEnum.Settled);
		}

		public bool AllowModify(string sysDocID, string voucherNumber)
		{
			return new ConsignIn(config).AllowModify(sysDocID, voucherNumber, null);
		}

		public DataSet GetPendingConsignmentsReport(string fromVendor, string toVendor, string vendorIDs)
		{
			return new ConsignIn(config).GetPendingConsignmentsReport(fromVendor, toVendor, vendorIDs);
		}

		public DataSet GetConsignmentInSettlementSummaryReport(string fromVendor, string toVendor, string vendorID)
		{
			return new ConsignIn(config).GetConsignmentInSettlementSummaryReport(fromVendor, toVendor, vendorID);
		}

		public DataSet GetConsignmentInSettlementDetailReport(string fromVendor, string toVendor, string VendorIDs)
		{
			return new ConsignIn(config).GetConsignmentInSettlementDetailReport(fromVendor, toVendor, VendorIDs);
		}

		public DataSet GetBillableItems(string consignSysDocID, string consignVoucherID)
		{
			return new ConsignIn(config).GetBillableItems(consignSysDocID, consignVoucherID);
		}

		public DataSet GetConsignInComboList()
		{
			return new ConsignIn(config).GetConsignInComboList();
		}

		public DataSet GetConsignInClosingSummary(string sysDocID, string voucherID)
		{
			return new ConsignIn(config).GetConsignInClosingSummary(sysDocID, voucherID);
		}

		public bool CloseConsignIn(ConsignInData consignInData)
		{
			return new ConsignIn(config).CloseConsignIn(consignInData);
		}

		public DataSet GetConsignInReport(DateTime from, DateTime to, string fromVendor, string toVendor, string fromClass, string toClass, string fromGroup, string toGroup)
		{
			using (ConsignIn consignIn = new ConsignIn(config))
			{
				return consignIn.GetConsignInReport(from, to, fromVendor, toVendor, fromClass, toClass, fromGroup, toGroup);
			}
		}

		public DataSet GetConsignInList(string sysDocID, DateTime fromDate, DateTime endDate)
		{
			using (ConsignIn consignIn = new ConsignIn(config))
			{
				return consignIn.GetConsignInList(sysDocID, fromDate, endDate);
			}
		}

		public DataSet GetConsignInItemMovementReport(DateTime from, DateTime to, string fromItem, string toItem, string fromItemClass, string toItemClass, string fromCategory, string toCategory, string fromLocationID, string toLocationID, string sysDocID, string voucherID, string fromManufacturer, string toManufacturer, string fromStyle, string toStyle, string fromOrigin, string toOrigin)
		{
			return new ConsignIn(config).GetConsignInItemMovementReport(from, to, fromItem, toItem, fromItemClass, toItemClass, fromCategory, toCategory, fromLocationID, toLocationID, sysDocID, voucherID, fromManufacturer, toManufacturer, fromStyle, toStyle, fromOrigin, toOrigin);
		}

		public DataSet GetConsignInSummaryReport(string sysDocID, string voucherID)
		{
			using (ConsignIn consignIn = new ConsignIn(config))
			{
				return consignIn.GetConsignInSummaryReport(sysDocID, voucherID);
			}
		}
	}
}
