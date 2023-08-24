using Micromind.Common.Data;
using Micromind.Common.Interfaces;
using Micromind.Data;
using System;
using System.Data;

namespace Micromind.Facade
{
	public sealed class WorkOrderInventoryIssueSystem : MarshalByRefObject, IWorkOrderInventoryIssueSystem, IDisposable
	{
		private Config config;

		public WorkOrderInventoryIssueSystem(Config config)
		{
			this.config = config;
		}

		public void Dispose()
		{
		}

		public bool CreateWorkOrderInventoryIssue(WorkOrderInventoryIssueData data, bool isUpdate)
		{
			return new WorkOrderInventoryIssue(config).InsertUpdateWorkOrderInventoryIssue(data, isUpdate);
		}

		public WorkOrderInventoryIssueData GetWorkOrderInventoryIssueByID(string sysDocID, string voucherID)
		{
			return new WorkOrderInventoryIssue(config).GetWorkOrderInventoryIssueByID(sysDocID, voucherID);
		}

		public bool DeleteWorkOrderInventoryIssue(string sysDocID, string voucherID)
		{
			return new WorkOrderInventoryIssue(config).DeleteWorkOrderInventoryIssue(sysDocID, voucherID);
		}

		public DataSet GetWorkOrderInventoryIssueToPrint(string sysDocID, string[] voucherID)
		{
			return new WorkOrderInventoryIssue(config).GetWorkOrderInventoryIssueToPrint(sysDocID, voucherID);
		}

		public DataSet GetWorkOrderInventoryIssueToPrint(string sysDocID, string voucherID)
		{
			return new WorkOrderInventoryIssue(config).GetWorkOrderInventoryIssueToPrint(sysDocID, new string[1]
			{
				voucherID
			});
		}

		public DataSet GetList(DateTime fromDate, DateTime toDate, bool Isvoid)
		{
			return new WorkOrderInventoryIssue(config).GetWorkOrderInventoryIssueList(fromDate, toDate, Isvoid: true);
		}
	}
}
