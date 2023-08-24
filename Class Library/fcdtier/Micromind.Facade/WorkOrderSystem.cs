using Micromind.Common.Data;
using Micromind.Common.Interfaces;
using Micromind.Data;
using System;
using System.Data;

namespace Micromind.Facade
{
	public sealed class WorkOrderSystem : MarshalByRefObject, IWorkOrderSystem, IDisposable
	{
		private Config config;

		public WorkOrderSystem(Config config)
		{
			this.config = config;
		}

		public void Dispose()
		{
		}

		public bool CreateWorkOrder(WorkOrderData data, bool isUpdate)
		{
			return new WorkOrder(config).InsertUpdateWorkOrder(data, isUpdate);
		}

		public WorkOrderData GetWorkOrderByID(string sysDocID, string voucherID)
		{
			return new WorkOrder(config).GetWorkOrderByID(sysDocID, voucherID);
		}

		public bool DeleteWorkOrder(string sysDocID, string voucherID)
		{
			return new WorkOrder(config).DeleteWorkOrder(sysDocID, voucherID);
		}

		public DataSet GetWorkOrderToPrint(string sysDocID, string[] voucherID)
		{
			return new WorkOrder(config).GetWorkOrderToPrint(sysDocID, voucherID);
		}

		public DataSet GetWorkOrderToPrint(string sysDocID, string voucherID)
		{
			return new WorkOrder(config).GetWorkOrderToPrint(sysDocID, new string[1]
			{
				voucherID
			});
		}

		public DataSet GetWorkOrderAll()
		{
			return new WorkOrder(config).GetWorkOrderAll();
		}

		public DataSet GetDetails(string tableName, string DocID, string Value)
		{
			return new WorkOrder(config).getDetails(tableName, DocID, Value);
		}

		public DataSet GetReference(string Table, string ColumnName, string SysdocId, string VoucherNo)
		{
			return new WorkOrder(config).getReference(Table, ColumnName, SysdocId, VoucherNo);
		}

		public DataSet GetList(DateTime fromDate, DateTime toDate, bool Isvoid)
		{
			return new WorkOrder(config).GetWorkOrderList(fromDate, toDate, Isvoid: true);
		}
	}
}
