using Micromind.Common.Data;
using Micromind.Common.Interfaces;
using Micromind.Data;
using System;
using System.Data;

namespace Micromind.Facade
{
	public sealed class ConsignOutSystem : MarshalByRefObject, IConsignOutSystem, IDisposable
	{
		private Config config;

		public ConsignOutSystem(Config config)
		{
			this.config = config;
		}

		public void Dispose()
		{
		}

		public bool ReCostTransaction(string sysDocID, string voucherID)
		{
			return new ConsignOut(config).ReCostTransaction(sysDocID, voucherID);
		}

		public bool CreateConsignOut(ConsignOutData data, bool isUpdate)
		{
			return new ConsignOut(config).InsertUpdateConsignOut(data, isUpdate);
		}

		public ConsignOutData GetConsignOutByID(string sysDocID, string voucherID)
		{
			return new ConsignOut(config).GetConsignOutByID(sysDocID, voucherID);
		}

		public bool DeleteConsignOut(string sysDocID, string voucherID)
		{
			return new ConsignOut(config).DeleteConsignOut(sysDocID, voucherID);
		}

		public bool VoidConsignOut(string sysDocID, string voucherID, bool isVoid)
		{
			return new ConsignOut(config).VoidConsignOut(sysDocID, voucherID, isVoid);
		}

		public DataSet GetUninvoicedConsignOuts(string customerID)
		{
			return new ConsignOut(config).GetUninvoicedConsignOuts(customerID);
		}

		public DataSet GetConsignOutToPrint(string sysDocID, string voucherID)
		{
			return new ConsignOut(config).GetConsignOutToPrint(sysDocID, voucherID);
		}

		public DataSet GetConsignOutToPrint(string sysDocID, string[] voucherID)
		{
			return new ConsignOut(config).GetConsignOutToPrint(sysDocID, voucherID);
		}

		public DataSet GetList(DateTime from, DateTime to, bool showVoid)
		{
			return new ConsignOut(config).GetList(from, to, showVoid);
		}

		public DataSet GetOpenConsignments(string customerID)
		{
			return new ConsignOut(config).GetOpenConsignments(customerID);
		}

		public bool AllowModify(string sysDocID, string voucherNumber)
		{
			return new ConsignOut(config).AllowModify(sysDocID, voucherNumber, null);
		}

		public DataSet GetPendingConsignmentsReport(string fromCustomer, string toCustomer, string customerID)
		{
			return new ConsignOut(config).GetPendingConsignmentsReport(fromCustomer, toCustomer, customerID);
		}

		public DataSet GetConsignmentOutSettlementReport(DateTime settleto, string fromCustomer, string toCustomer, string customerIDs)
		{
			return new ConsignOut(config).GetConsignmentOutSettlementReport(settleto, fromCustomer, toCustomer, customerIDs);
		}

		public DataSet GetConsignmentOutIssuedReport(DateTime settleto, DateTime from, DateTime to, string fromCustomer, string toCustomer, int intstatus, string customerIDs, string fromItem, string toItem, string fromClass, string toClass, string fromCategory, string toCategory, string fromBrand, string toBrand, string fromManufacturer, string toManufacturer, string fromStyle, string toStyle, string fromOrigin, string toOrigin)
		{
			return new ConsignOut(config).GetConsignmentOutIssuedReport(settleto, from, to, fromCustomer, toCustomer, intstatus, customerIDs, fromItem, toItem, fromClass, toClass, fromCategory, toCategory, fromBrand, toBrand, fromManufacturer, toManufacturer, fromStyle, toStyle, fromOrigin, toOrigin);
		}

		public DataSet GetConsignOutList(string sysDocID, DateTime fromDate, DateTime endDate)
		{
			using (ConsignOut consignOut = new ConsignOut(config))
			{
				return consignOut.GetConsignOutList(sysDocID, fromDate, endDate);
			}
		}

		public DataSet GetConsignOutSummaryReport(string sysDocID, string voucherID, DateTime from, DateTime to, string fromCustomer, string toCustomer, string fromClass, string toClass, string fromGroup, string toGroup)
		{
			using (ConsignOut consignOut = new ConsignOut(config))
			{
				return consignOut.GetConsignOutSummaryReport(sysDocID, voucherID, from, to, fromCustomer, toCustomer, fromClass, toClass, fromGroup, toGroup);
			}
		}
	}
}
