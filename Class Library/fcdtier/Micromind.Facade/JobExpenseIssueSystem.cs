using Micromind.Common.Data;
using Micromind.Common.Interfaces;
using Micromind.Data;
using System;
using System.Data;

namespace Micromind.Facade
{
	public sealed class JobExpenseIssueSystem : MarshalByRefObject, IJobExpenseIssueSystem, IDisposable
	{
		private Config config;

		public JobExpenseIssueSystem(Config config)
		{
			this.config = config;
		}

		public void Dispose()
		{
		}

		public bool CreateJobExpenseIssue(JobExpenseIssueData data, bool isUpdate)
		{
			return new JobExpenseIssue(config).InsertUpdateJobExpenseIssue(data, isUpdate);
		}

		public JobExpenseIssueData GetJobExpenseIssueByID(string sysDocID, string voucherID)
		{
			return new JobExpenseIssue(config).GetJobExpenseIssueByID(sysDocID, voucherID);
		}

		public bool DeleteJobExpenseIssue(string sysDocID, string voucherID)
		{
			return new JobExpenseIssue(config).DeleteJobExpenseIssue(sysDocID, voucherID);
		}

		public DataSet GetJobExpenseIssueToPrint(string sysDocID, string[] voucherID)
		{
			return new JobExpenseIssue(config).GetJobExpenseIssueToPrint(sysDocID, voucherID);
		}

		public DataSet GetJobExpenseIssueToPrint(string sysDocID, string voucherID)
		{
			return new JobExpenseIssue(config).GetJobExpenseIssueToPrint(sysDocID, new string[1]
			{
				voucherID
			});
		}

		public DataSet GetList(DateTime fromDate, DateTime toDate, bool Isvoid)
		{
			return new JobExpenseIssue(config).GetJobExpenseIssueList(fromDate, toDate, Isvoid: true);
		}
	}
}
