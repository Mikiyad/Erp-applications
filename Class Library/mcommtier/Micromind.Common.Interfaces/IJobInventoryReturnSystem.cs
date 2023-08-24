using Micromind.Common.Data;
using System;
using System.Data;

namespace Micromind.Common.Interfaces
{
	public interface IJobInventoryReturnSystem
	{
		bool CreateJobInventoryReturn(JobInventoryReturnData inventoryAdjustmentData, bool isUpdate);

		JobInventoryReturnData GetJobInventoryReturnByID(string sysDocID, string voucherID);

		DataSet GetJobInventoryReturnToPrint(string sysDocID, string[] voucherID);

		DataSet GetJobInventoryReturnToPrint(string sysDocID, string voucherID);

		bool DeleteJobInventoryReturn(string sysDocID, string voucherID);

		DataSet GetList(DateTime from, DateTime to, bool showVoid);
	}
}
