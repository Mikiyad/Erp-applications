using Micromind.Common.Data;
using System;
using System.Data;

namespace Micromind.Common.Interfaces
{
	public interface IEAEquipmentSystem
	{
		bool CreateEquipment(EAEquipmentData EquipmentData);

		bool UpdateEquipment(EAEquipmentData EquipmentData);

		EAEquipmentData GetEquipment();

		bool DeleteEquipment(string ID);

		EAEquipmentData GetEquipmentByID(string id);

		DataSet GetEquipmentByCategoryID(string id);

		DataSet GetEquipmentByFields(params string[] columns);

		DataSet GetEquipmentByFields(string[] ids, params string[] columns);

		DataSet GetEquipmentByFields(string[] ids, bool isInactive, params string[] columns);

		DataSet GetEquipmentReport(string fromEquipment, string toEquipment, string fromClass, string toClass, string fromGroup, string toGroup, bool showInactive);

		DataSet GetEquipmentList();

		DataSet GetEquipmentComboList();

		DataSet GetEquipmentListReport(string fromEquipment, string toEquipment, string fromType, string toType, string fromCategory, string toCategory, string fromLocation, string toLocation, string fromJob, string toJob, bool showInactive);

		DataSet GetEquipmentFlowReport(DateTime fromDate, DateTime toDate, string fromItem, string toItem, string fromItemClass, string toItemClass, string fromItemCategory, string toItemCategory, string fromEquipment, string toEquipment, string fromType, string toType, string fromCategory, string toCategory, string fromLocation, string toLocation, string fromJob, string toJob, string sysDocID, string voucherID);
	}
}
