using Micromind.Common.Data;
using Micromind.Common.Interfaces;
using Micromind.Data;
using System;
using System.Data;

namespace Micromind.Facade
{
	public sealed class FreightChargeSystem : MarshalByRefObject, IFreightChargeSystem, IDisposable
	{
		private Config config;

		public FreightChargeSystem(Config config)
		{
			this.config = config;
		}

		public void Dispose()
		{
		}

		public bool CreateFreightCharge(FreightChargeData data, bool isUpdate)
		{
			return new FreightCharge(config).InsertUpdateFreightCharge(data, isUpdate);
		}

		public FreightChargeData GetFreightChargeByID(string sysDocID, string voucherID)
		{
			return new FreightCharge(config).GetFreightChargeByID(sysDocID, voucherID);
		}

		public bool DeleteFreightCharge(string sysDocID, string voucherID)
		{
			return new FreightCharge(config).DeleteFreightCharge(sysDocID, voucherID);
		}

		public bool VoidFreightCharge(string sysDocID, string voucherID, bool isVoid)
		{
			return new FreightCharge(config).VoidFreightCharge(sysDocID, voucherID, isVoid);
		}

		public DataSet GetFreightChargeToPrint(string sysDocID, string voucherID)
		{
			return new FreightCharge(config).GetFreightChargeToPrint(sysDocID, voucherID);
		}

		public DataSet GetFreightChargeToPrint(string sysDocID, string[] voucherID)
		{
			return new FreightCharge(config).GetFreightChargeToPrint(sysDocID, voucherID);
		}

		public DataSet GetList(DateTime from, DateTime to, bool showVoid)
		{
			return new FreightCharge(config).GetList(from, to, showVoid);
		}

		public DataSet GetFreightChargeAll()
		{
			return new FreightCharge(config).GetFreightChargeAll();
		}

		public DataSet GetFreightChargeByCustomerID(string customerID)
		{
			return new FreightCharge(config).GetFreightChargeByCustomerID(customerID);
		}

		public DataSet GetActiveFreightChargeByCustomerID(string customerID)
		{
			return new FreightCharge(config).GetActiveFreightChargeByCustomerID(customerID);
		}

		public DataSet GetFreightChargeReport(DateTime fromDate, DateTime toDate, string fromPort, string toPort, string fromVendor, string toVendor, string fromClass, string toClass, string fromGroup, string toGroup, bool showInactive, string vendorIDs)
		{
			return new FreightCharge(config).GetFreightChargeReport(fromDate, toDate, fromPort, toPort, fromVendor, toVendor, fromClass, toClass, fromGroup, toGroup, showInactive, vendorIDs);
		}
	}
}
