using Micromind.Common.Data;
using Micromind.Common.Interfaces;
using Micromind.Data;
using System;
using System.Data;

namespace Micromind.Facade
{
	public sealed class WorkOrderInventoryReturnSystem : MarshalByRefObject, IWorkOrderInventoryReturnSystem, IDisposable
	{
		private Config config;

		public WorkOrderInventoryReturnSystem(Config config)
		{
			this.config = config;
		}

		public void Dispose()
		{
		}

		public bool CreateWorkOrderInventoryReturn(WorkOrderInventoryReturnData data, bool isUpdate)
		{
			return new WorkOrderInventoryReturn(config).InsertUpdateWorkOrderInventoryReturn(data, isUpdate);
		}

		public WorkOrderInventoryReturnData GetWorkOrderInventoryReturnByID(string sysDocID, string voucherID)
		{
			return new WorkOrderInventoryReturn(config).GetWorkOrderInventoryReturnByID(sysDocID, voucherID);
		}

		public bool DeleteWorkOrderInventoryReturn(string sysDocID, string voucherID)
		{
			return new WorkOrderInventoryReturn(config).DeleteWorkOrderInventoryReturn(sysDocID, voucherID);
		}

		public DataSet GetWorkOrderInventoryReturnToPrint(string sysDocID, string[] voucherID)
		{
			return new WorkOrderInventoryReturn(config).GetWorkOrderInventoryReturnToPrint(sysDocID, voucherID);
		}

		public DataSet GetWorkOrderInventoryReturnToPrint(string sysDocID, string voucherID)
		{
			return new WorkOrderInventoryReturn(config).GetWorkOrderInventoryReturnToPrint(sysDocID, new string[1]
			{
				voucherID
			});
		}

		public DataSet GetList(DateTime fromDate, DateTime toDate, bool Isvoid)
		{
			return new WorkOrderInventoryReturn(config).GetWorkOrderInventoryReturnList(fromDate, toDate, Isvoid: true);
		}
	}
}
