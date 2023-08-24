using Micromind.Common.Data;
using System;
using System.Data;

namespace Micromind.Common.Interfaces
{
	public interface IJobTimesheetSystem
	{
		bool CreateJobTimesheet(JobTimesheetData expenseAdjustmentData, bool isUpdate);

		JobTimesheetData GetJobTimesheetByID(string sysDocID, string voucherID);

		DataSet GetJobTimesheetToPrint(string sysDocID, string[] voucherID);

		DataSet GetJobTimesheetToPrint(string sysDocID, string voucherID);

		bool DeleteJobTimesheet(string sysDocID, string voucherID);

		DataSet GetList(DateTime from, DateTime to, bool showVoid);
	}
}
