using Micromind.Common.Data;
using System;
using System.Data;

namespace Micromind.Common.Interfaces
{
	public interface IJobExpenseIssueSystem
	{
		bool CreateJobExpenseIssue(JobExpenseIssueData expenseAdjustmentData, bool isUpdate);

		JobExpenseIssueData GetJobExpenseIssueByID(string sysDocID, string voucherID);

		DataSet GetJobExpenseIssueToPrint(string sysDocID, string[] voucherID);

		DataSet GetJobExpenseIssueToPrint(string sysDocID, string voucherID);

		bool DeleteJobExpenseIssue(string sysDocID, string voucherID);

		DataSet GetList(DateTime from, DateTime to, bool showVoid);
	}
}
