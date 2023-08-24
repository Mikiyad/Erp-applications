using Micromind.Common.Data;
using System;
using System.Data;

namespace Micromind.Common.Interfaces
{
	public interface IJobMaterialEstimateSystem
	{
		bool CreateJobMaterialEstimate(JobMaterialEstimateData materialEstimatetData, bool isUpdate);

		JobMaterialEstimateData GetJobMaterialEstimateByID(string sysDocID, string voucherID);

		DataSet GetJobMaterialEstimateAll();

		DataSet GetJobMaterialEstimateToPrint(string sysDocID, string[] voucherID);

		DataSet GetJobMaterialEstimateToPrint(string sysDocID, string voucherID);

		bool DeleteJobMaterialEstimate(string sysDocID, string voucherID);

		DataSet GetList(DateTime from, DateTime to, bool showVoid);

		JobMaterialEstimateData GetJobMaterialEstimateByJobIDCostCategoryID(string sysDocID, string voucherID, string jobID, string costCategoryID);

		DataSet GetJobMaterialEstimationList();

		decimal GetJobMaterialEstimationQty(string jobID);
	}
}
