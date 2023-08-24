using Micromind.Common.Data;
using System;
using System.Data;

namespace Micromind.Common.Interfaces
{
	public interface IEquipmentTransferSystem
	{
		bool CreateEquipmentTransfer(EquipmentTransferData EquipmentTransferData, bool isUpdate);

		EquipmentTransferData GetEquipmentTransferByID(string sysDocID, string voucherID);

		bool DeleteEquipmentTransfer(string sysDocID, string voucherID);

		bool VoidEquipmentTransfer(string sysDocID, string voucherID, bool isVoid);

		DataSet GetEquipmentTransferToPrint(string sysDocID, string[] voucherID);

		DataSet GetEquipmentTransferToPrint(string sysDocID, string voucherID);

		DataSet GetEquipmentTransferReport(DateTime from, DateTime to, string warehouseCode, bool isTransferOut);

		DataSet GetList(DateTime from, DateTime to, bool showVoid);

		DataSet GetEquipmentTransferReport(DateTime fromDate, DateTime toDate, string fromEquipment, string toEquipment, string fromType, string toType, string fromCategory, string toCategory);
	}
}
