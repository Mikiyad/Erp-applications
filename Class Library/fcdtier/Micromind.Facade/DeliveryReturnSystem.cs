using Micromind.Common.Data;
using Micromind.Common.Interfaces;
using Micromind.Data;
using System;
using System.Data;

namespace Micromind.Facade
{
	public sealed class DeliveryReturnSystem : MarshalByRefObject, IDeliveryReturnSystem, IDisposable
	{
		private Config config;

		public DeliveryReturnSystem(Config config)
		{
			this.config = config;
		}

		public void Dispose()
		{
		}

		public bool CreateDeliveryReturn(DeliveryReturnData data, bool isUpdate)
		{
			return new DeliveryReturn(config).InsertUpdateDeliveryReturn(data, isUpdate);
		}

		public DeliveryReturnData GetDeliveryReturnByID(string sysDocID, string voucherID)
		{
			return new DeliveryReturn(config).GetDeliveryReturnByID(sysDocID, voucherID);
		}

		public bool DeleteDeliveryReturn(string sysDocID, string voucherID)
		{
			return new DeliveryReturn(config).DeleteDeliveryReturn(sysDocID, voucherID);
		}

		public bool VoidDeliveryReturn(string sysDocID, string voucherID, bool isVoid)
		{
			return new DeliveryReturn(config).VoidDeliveryReturn(sysDocID, voucherID, isVoid);
		}

		public DataSet GetUninvoicedDeliveryReturns(string customerID)
		{
			return new DeliveryReturn(config).GetUninvoicedDeliveryReturns(customerID);
		}

		public DataSet GetDeliveryReturnToPrint(string sysDocID, string voucherID)
		{
			return new DeliveryReturn(config).GetDeliveryReturnToPrint(sysDocID, voucherID);
		}

		public DataSet GetDeliveryReturnToPrint(string sysDocID, string[] voucherID)
		{
			return new DeliveryReturn(config).GetDeliveryReturnToPrint(sysDocID, voucherID);
		}

		public DataSet GetList(DateTime from, DateTime to, bool showVoid)
		{
			return new DeliveryReturn(config).GetList(from, to, showVoid);
		}
	}
}
