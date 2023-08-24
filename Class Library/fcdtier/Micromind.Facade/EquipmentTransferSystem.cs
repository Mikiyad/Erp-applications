using Micromind.Common.Data;
using Micromind.Common.Interfaces;
using Micromind.Data;
using System;
using System.Data;

namespace Micromind.Facade
{
	public sealed class EquipmentTransferSystem : MarshalByRefObject, IEquipmentTransferSystem, IDisposable
	{
		private Config config;

		public EquipmentTransferSystem(Config config)
		{
			this.config = config;
		}

		public void Dispose()
		{
		}

		public bool CreateEquipmentTransfer(EquipmentTransferData data, bool isUpdate)
		{
			return new EquipmentTransfer(config).InsertUpdateEquipmentTransfer(data, isUpdate);
		}

		public EquipmentTransferData GetEquipmentTransferByID(string sysDocID, string voucherID)
		{
			return new EquipmentTransfer(config).GetEquipmentTransferByID(sysDocID, voucherID);
		}

		public bool DeleteEquipmentTransfer(string sysDocID, string voucherID)
		{
			return new EquipmentTransfer(config).DeleteEquipmentTransfer(sysDocID, voucherID);
		}

		public bool VoidEquipmentTransfer(string sysDocID, string voucherID, bool isVoid)
		{
			return new EquipmentTransfer(config).VoidEquipmentTransfer(sysDocID, voucherID, isVoid);
		}

		public DataSet GetEquipmentTransferReport(DateTime from, DateTime to, string warehouseCode, bool isTransferOut)
		{
			return new EquipmentTransfer(config).GetEquipmentTransferReport(from, to, warehouseCode, isTransferOut);
		}

		public DataSet GetList(DateTime from, DateTime to, bool showVoid)
		{
			return new EquipmentTransfer(config).GetList(from, to, showVoid);
		}

		public DataSet GetEquipmentTransferToPrint(string sysDocID, string[] voucherID)
		{
			return new EquipmentTransfer(config).GetEquipmentTransferToPrint(sysDocID, voucherID);
		}

		public DataSet GetEquipmentTransferToPrint(string sysDocID, string voucherID)
		{
			return new EquipmentTransfer(config).GetEquipmentTransferToPrint(sysDocID, new string[1]
			{
				voucherID
			});
		}

		public DataSet GetEquipmentTransferReport(DateTime fromDate, DateTime toDate, string fromEquipment, string toEquipment, string fromType, string toType, string fromCategory, string toCategory)
		{
			return new EquipmentTransfer(config).GetEquipmentTransferReport(fromDate, toDate, fromEquipment, toEquipment, fromType, toType, fromCategory, toCategory);
		}
	}
}
