using Micromind.Common.Data;
using System;
using System.Data;

namespace Micromind.Common.Interfaces
{
	public interface IJobManHrsBudgetingSystem
	{
		bool CreateJobManHrsBudgeting(JobManHrsBudgetingData jobManHrsBudgetingData, bool isUpdate);

		JobManHrsBudgetingData GetJobManHrsBudgetingByID(string sysDocID, string voucherID);

		DataSet GetJobManHrsBudgetingAll();

		DataSet GetJobManHrsBudgetingToPrint(string sysDocID, string[] voucherID);

		DataSet GetJobManHrsBudgetingToPrint(string sysDocID, string voucherID);

		bool DeleteJobManHrsBudgeting(string sysDocID, string voucherID);

		DataSet GetList(DateTime from, DateTime to, bool showVoid);
	}
}
