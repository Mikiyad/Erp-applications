using Micromind.Common.Data;
using System;
using System.Data;

namespace Micromind.Common.Interfaces
{
	public interface IJobInventoryIssueSystem
	{
		bool CreateJobInventoryIssue(JobInventoryIssueData inventoryAdjustmentData, bool isUpdate);

		JobInventoryIssueData GetJobInventoryIssueByID(string sysDocID, string voucherID);

		DataSet GetJobInventoryIssueToPrint(string sysDocID, string[] voucherID);

		DataSet GetJobInventoryIssueToPrint(string sysDocID, string voucherID);

		bool DeleteJobInventoryIssue(string sysDocID, string voucherID);

		DataSet GetList(DateTime from, DateTime to, bool showVoid);
	}
}
