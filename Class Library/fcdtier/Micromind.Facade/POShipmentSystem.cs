using Micromind.Common.Data;
using Micromind.Common.Interfaces;
using Micromind.Data;
using System;
using System.Data;

namespace Micromind.Facade
{
	public sealed class POShipmentSystem : MarshalByRefObject, IPOShipmentSystem, IDisposable
	{
		private Config config;

		public POShipmentSystem(Config config)
		{
			this.config = config;
		}

		public void Dispose()
		{
		}

		public bool CreatePOShipment(POShipmentData data, bool isUpdate)
		{
			return new POShipment(config).InsertUpdatePOShipment(data, isUpdate);
		}

		public POShipmentData GetPOShipmentByID(string sysDocID, string voucherID)
		{
			return new POShipment(config).GetPOShipmentByID(sysDocID, voucherID);
		}

		public bool DeletePOShipment(string sysDocID, string voucherID)
		{
			return new POShipment(config).DeletePOShipment(sysDocID, voucherID);
		}

		public bool VoidPOShipment(string sysDocID, string voucherID, bool isVoid)
		{
			return new POShipment(config).VoidPOShipment(sysDocID, voucherID, isVoid);
		}

		public DataSet GetOpenShipmentsSummary(string customerID)
		{
			return new POShipment(config).GetOpenShipmentsSummary(customerID);
		}

		public DataSet GetOpenShipmentsReport()
		{
			return new POShipment(config).GetOpenShipmentsReport();
		}

		public DataSet GetPOShipmentToPrint(string sysDocID, string[] voucherID)
		{
			return new POShipment(config).GetPOShipmentToPrint(sysDocID, voucherID);
		}

		public DataSet GetPOShipmentToPrint(string sysDocID, string voucherID)
		{
			return new POShipment(config).GetPOShipmentToPrint(sysDocID, new string[1]
			{
				voucherID
			});
		}

		public DataSet GetList(DateTime from, DateTime to, bool showVoid)
		{
			return new POShipment(config).GetList(from, to, showVoid);
		}

		public DataSet GetLastPOShipment(string poSysDocID, string poVoucherID)
		{
			return new POShipment(config).GetLastPOShipment(poSysDocID, poVoucherID);
		}

		public bool SetOrderStatus(string sysDocID, string voucherID, PurchaseOrderStatus status)
		{
			return new POShipment(config).SetOrderStatus(sysDocID, voucherID, status);
		}

		public DataSet GetBOLListForPayment(bool POMultipleBOL)
		{
			return new POShipment(config).GetBOLListForPayment(POMultipleBOL);
		}

		public DataSet GetPackingList()
		{
			return new POShipment(config).GetPackingList();
		}

		public DataSet GetBOLList()
		{
			return new POShipment(config).GetBOLList();
		}

		public DataSet GetContainerStatusList()
		{
			return new POShipment(config).GetContainerStatusList();
		}

		public DataSet GetBOLContainerList()
		{
			return new POShipment(config).GetBOLContainerList();
		}

		public DataSet GetContainerList()
		{
			return new POShipment(config).GetContainerList();
		}

		public DataSet GetContainerComboList()
		{
			return new POShipment(config).GetContainerComboList();
		}

		public POShipmentData GetContainerDetails(string containerNumber)
		{
			return new POShipment(config).GetContainerDetails(containerNumber);
		}
	}
}
