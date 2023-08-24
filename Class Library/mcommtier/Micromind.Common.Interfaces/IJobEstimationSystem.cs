using Micromind.Common.Data;
using System;
using System.Data;

namespace Micromind.Common.Interfaces
{
	public interface IJobEstimationSystem
	{
		bool CreateJobEstimation(JobEstimationData inventoryAdjustmentData, bool isUpdate, bool IsRevised);

		JobEstimationData GetJobEstimationByID(string sysDocID, string voucherID, bool isRevised, int RevisedNo);

		DataSet GetJobEstimationToPrint(string sysDocID, string[] voucherID);

		DataSet GetJobEstimationToPrint(string sysDocID, string voucherID);

		DataSet GetJobEstimationRevToPrint(string sysDocID, string voucherID, int RevisedNo);

		bool DeleteJobEstimation(string sysDocID, string voucherID);

		DataSet GetList(DateTime from, DateTime to, bool showVoid);

		DataSet GetLoadRevisionCombo(string sysDocID, string voucherID);

		int GetNextRevsionNo(string sysDocID, string voucherID);

		bool VoidJobEstimation(string sysDocID, string voucherID, bool isVoid);
	}
}
