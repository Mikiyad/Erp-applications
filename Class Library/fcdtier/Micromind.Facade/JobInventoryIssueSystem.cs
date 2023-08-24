using Micromind.Common.Data;
using Micromind.Common.Interfaces;
using Micromind.Data;
using System;
using System.Data;

namespace Micromind.Facade
{
	public sealed class JobInventoryIssueSystem : MarshalByRefObject, IJobInventoryIssueSystem, IDisposable
	{
		private Config config;

		public JobInventoryIssueSystem(Config config)
		{
			this.config = config;
		}

		public void Dispose()
		{
		}

		public bool CreateJobInventoryIssue(JobInventoryIssueData data, bool isUpdate)
		{
			return new JobInventoryIssue(config).InsertUpdateJobInventoryIssue(data, isUpdate);
		}

		public JobInventoryIssueData GetJobInventoryIssueByID(string sysDocID, string voucherID)
		{
			return new JobInventoryIssue(config).GetJobInventoryIssueByID(sysDocID, voucherID);
		}

		public bool DeleteJobInventoryIssue(string sysDocID, string voucherID)
		{
			return new JobInventoryIssue(config).DeleteJobInventoryIssue(sysDocID, voucherID);
		}

		public DataSet GetJobInventoryIssueToPrint(string sysDocID, string[] voucherID)
		{
			return new JobInventoryIssue(config).GetJobInventoryIssueToPrint(sysDocID, voucherID);
		}

		public DataSet GetJobInventoryIssueToPrint(string sysDocID, string voucherID)
		{
			return new JobInventoryIssue(config).GetJobInventoryIssueToPrint(sysDocID, new string[1]
			{
				voucherID
			});
		}

		public DataSet GetList(DateTime fromDate, DateTime toDate, bool Isvoid)
		{
			return new JobInventoryIssue(config).GetJobInventoryIssueList(fromDate, toDate, Isvoid: true);
		}
	}
}
