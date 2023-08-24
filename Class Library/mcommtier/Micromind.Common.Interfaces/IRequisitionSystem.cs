using Micromind.Common.Data;
using System;
using System.Data;

namespace Micromind.Common.Interfaces
{
	public interface IRequisitionSystem
	{
		bool CreateRequisition(RequisitionData RequisitionData, bool isUpdate);

		RequisitionData GetRequisitionByID(string sysDocID, string voucherID);

		DataSet GetRequisition();

		DataSet GetRequisitionToPrint(string sysDocID, string[] voucherID);

		DataSet GetRequisitionToPrint(string sysDocID, string voucherID);

		bool DeleteRequisition(string sysDocID, string voucherID);

		DataSet GetRequisitionList(string SysDocID);

		DataSet GetRequisitionReport(DateTime from, DateTime to, string fromWarehouse, string toWarehouse, string fromItem, string toItem, string fromClass, string toClass, string fromCategory, string toCategory);

		DataSet GetList(DateTime from, DateTime to, bool showVoid);

		DataSet GetRequisitionByEquipmentLocationProjectReport(DateTime fromDate, DateTime toDate, string fromEquipment, string toEquipment, string fromType, string toType, string fromCategory, string toCategory, string fromLocation, string toLocation, string fromJob, string toJob);

		bool UpdateRequisitionStatus(string sysDocID, string voucherID);
	}
}
