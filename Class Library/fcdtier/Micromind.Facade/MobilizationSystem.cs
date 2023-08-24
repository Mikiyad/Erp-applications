using Micromind.Common.Data;
using Micromind.Common.Interfaces;
using Micromind.Data;
using System;
using System.Data;

namespace Micromind.Facade
{
	public sealed class MobilizationSystem : MarshalByRefObject, IMobilizationSystem, IDisposable
	{
		private Config config;

		public MobilizationSystem(Config config)
		{
			this.config = config;
		}

		public void Dispose()
		{
		}

		public bool CreateMobilization(MobilizationData data, bool isUpdate)
		{
			return new Mobilization(config).InsertUpdateMobilization(data, isUpdate);
		}

		public MobilizationData GetMobilizationByID(string sysDocID, string voucherID)
		{
			return new Mobilization(config).GetMobilizationByID(sysDocID, voucherID);
		}

		public bool DeleteMobilization(string sysDocID, string voucherID)
		{
			return new Mobilization(config).DeleteMobilization(sysDocID, voucherID);
		}

		public bool VoidMobilization(string sysDocID, string voucherID, bool isVoid)
		{
			return new Mobilization(config).VoidMobilization(sysDocID, voucherID, isVoid);
		}

		public DataSet GetInvoicesForDelivery(string customerID)
		{
			return new Mobilization(config).GetInvoicesForDelivery(customerID);
		}

		public DataSet GetMobilizationToPrint(string sysDocID, string voucherID)
		{
			return new Mobilization(config).GetMobilizationToPrint(sysDocID, voucherID);
		}

		public DataSet GetMobilizationToPrint(string sysDocID, string[] voucherID)
		{
			return new Mobilization(config).GetMobilizationToPrint(sysDocID, voucherID);
		}

		public DataSet GetList(DateTime from, DateTime to, bool showVoid)
		{
			return new Mobilization(config).GetList(from, to, showVoid);
		}

		public DataSet GetMobilizationList(string sysDocID)
		{
			return new Mobilization(config).GetMobilizationList(sysDocID);
		}

		public DataSet GetMobilizationList()
		{
			return new Mobilization(config).GetMobilizationList();
		}

		public DataSet GetPurchaseList(string sysDocID, DateTime fromDate, DateTime endDate)
		{
			return new Mobilization(config).GetPurchaseList(sysDocID, fromDate, endDate);
		}

		public DataSet GetMobilizationByEquipmentLocationProjectReport(DateTime fromDate, DateTime toDate, string fromItem, string toItem, string fromItemClass, string toItemClass, string fromItemCategory, string toItemCategory, string fromEquipment, string toEquipment, string fromType, string toType, string fromCategory, string toCategory, string fromLocation, string toLocation, string fromJob, string toJob, string fromManufacturer, string toManufacturer, string fromStyle, string toStyle, string fromOrigin, string toOrigin)
		{
			return new Mobilization(config).GetMobilizationByEquipmentLocationProjectReport(fromDate, toDate, fromItem, toItem, fromItemClass, toItemClass, fromItemCategory, toItemCategory, fromEquipment, toEquipment, fromType, toType, fromCategory, toCategory, fromLocation, toLocation, fromJob, toJob, fromManufacturer, toManufacturer, fromStyle, toStyle, fromOrigin, toOrigin);
		}
	}
}
