using Micromind.Common.Data;
using Micromind.Common.Interfaces;
using Micromind.Data;
using System;
using System.Data;

namespace Micromind.Facade
{
	public sealed class RequisitionSystem : MarshalByRefObject, IRequisitionSystem, IDisposable
	{
		private Config config;

		public RequisitionSystem(Config config)
		{
			this.config = config;
		}

		public void Dispose()
		{
		}

		public bool CreateRequisition(RequisitionData data, bool isUpdate)
		{
			return new Requisition(config).InsertUpdateRequisition(data, isUpdate);
		}

		public RequisitionData GetRequisitionByID(string sysDocID, string voucherID)
		{
			return new Requisition(config).GetRequisitionByID(sysDocID, voucherID);
		}

		public DataSet GetRequisition()
		{
			return new Requisition(config).GetRequisition();
		}

		public bool DeleteRequisition(string sysDocID, string voucherID)
		{
			return new Requisition(config).DeleteRequisition(sysDocID, voucherID);
		}

		public DataSet GetRequisitionToPrint(string sysDocID, string[] voucherID)
		{
			return new Requisition(config).GetRequisitionToPrint(sysDocID, voucherID);
		}

		public DataSet GetRequisitionToPrint(string sysDocID, string voucherID)
		{
			return new Requisition(config).GetRequisitionToPrint(sysDocID, new string[1]
			{
				voucherID
			});
		}

		public DataSet GetRequisitionReport(DateTime from, DateTime to, string fromWarehouse, string toWarehouse, string fromItem, string toItem, string fromClass, string toClass, string fromCategory, string toCategory)
		{
			return new Requisition(config).GetRequisitionReport(from, to, fromWarehouse, toWarehouse, fromItem, toItem, fromClass, toClass, fromCategory, toCategory);
		}

		public DataSet GetRequisitionList(string sysDocID)
		{
			return new Requisition(config).GetRequisitionList(sysDocID);
		}

		public DataSet GetList(DateTime from, DateTime to, bool showVoid)
		{
			return new Requisition(config).GetList(from, to, showVoid);
		}

		public DataSet GetRequisitionByEquipmentLocationProjectReport(DateTime fromDate, DateTime toDate, string fromEquipment, string toEquipment, string fromType, string toType, string fromCategory, string toCategory, string fromLocation, string toLocation, string fromJob, string toJob)
		{
			return new Requisition(config).GetRequisitionByEquipmentLocationProjectReport(fromDate, toDate, fromEquipment, toEquipment, fromType, toType, fromCategory, toCategory, fromLocation, toLocation, fromJob, toJob);
		}

		public bool UpdateRequisitionStatus(string sysDocID, string voucherID)
		{
			return new Requisition(config).UpdateRequisitionStatus(sysDocID, voucherID, null);
		}
	}
}
