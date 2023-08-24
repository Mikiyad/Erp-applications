using Micromind.Common.Data;
using Micromind.Common.Interfaces;
using Micromind.Data;
using System;
using System.Data;

namespace Micromind.Facade
{
	public sealed class PriceListSystem : MarshalByRefObject, IPriceListSystem, IDisposable
	{
		private Config config;

		public PriceListSystem(Config config)
		{
			this.config = config;
		}

		public void Dispose()
		{
		}

		public bool CreatePriceList(PriceListData data, bool isUpdate)
		{
			return new PriceList(config).InsertUpdatePriceList(data, isUpdate);
		}

		public bool CreateVendorPriceList(PriceListData data, bool isUpdate)
		{
			return new PriceList(config).InsertUpdateVendorPriceList(data, isUpdate);
		}

		public PriceListData GetPriceListByID(string sysDocID, string voucherID)
		{
			return new PriceList(config).GetPriceListByID(sysDocID, voucherID);
		}

		public PriceListData GetVendorPriceListByID(string sysDocID, string voucherID)
		{
			return new PriceList(config).GetVendorPriceListByID(sysDocID, voucherID);
		}

		public bool DeletePriceList(string sysDocID, string voucherID)
		{
			return new PriceList(config).DeletePriceList(sysDocID, voucherID);
		}

		public bool DeleteVendorPriceList(string sysDocID, string voucherID)
		{
			return new PriceList(config).DeleteVendorPriceList(sysDocID, voucherID);
		}

		public bool VoidPriceList(string sysDocID, string voucherID, bool isVoid)
		{
			return new PriceList(config).VoidPriceList(sysDocID, voucherID, isVoid);
		}

		public bool VoidVendorPriceList(string sysDocID, string voucherID, bool isVoid)
		{
			return new PriceList(config).VoidVendorPriceList(sysDocID, voucherID, isVoid);
		}

		public DataSet GetPriceListToPrint(string sysDocID, string voucherID)
		{
			return new PriceList(config).GetPriceListToPrint(sysDocID, voucherID);
		}

		public DataSet GetVendorPriceListToPrint(string sysDocID, string voucherID)
		{
			return new PriceList(config).GetVendorPriceListToPrint(sysDocID, voucherID);
		}

		public DataSet GetPriceListToPrint(string sysDocID, string[] voucherID)
		{
			return new PriceList(config).GetPriceListToPrint(sysDocID, voucherID);
		}

		public DataSet GetVendorPriceListToPrint(string sysDocID, string[] voucherID)
		{
			return new PriceList(config).GetVendorPriceListToPrint(sysDocID, voucherID);
		}

		public DataSet GetList(DateTime from, DateTime to, bool showVoid)
		{
			return new PriceList(config).GetList(from, to, showVoid);
		}

		public DataSet GetVendorList(DateTime from, DateTime to, bool showVoid)
		{
			return new PriceList(config).GetVendorPriceList(from, to, showVoid);
		}

		public DataSet GetPriceListAll()
		{
			return new PriceList(config).GetPriceListAll();
		}

		public DataSet GetPriceListByCustomerID(string customerID)
		{
			return new PriceList(config).GetPriceListByCustomerID(customerID);
		}

		public DataSet GetPriceListByVendorID(string customerID)
		{
			return new PriceList(config).GetPriceListByVendorID(customerID);
		}

		public DataSet GetActivePriceListByCustomerID(string customerID)
		{
			return new PriceList(config).GetActivePriceListByCustomerID(customerID);
		}
	}
}
