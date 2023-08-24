using Micromind.Common.Data;
using System;
using System.Data;

namespace Micromind.Common.Interfaces
{
	public interface IServiceCallTrackSystem
	{
		bool CreateJobMaterialRequisition(ServiceCallTrackData materialRequistiontData, bool isUpdate);

		ServiceCallTrackData GetJobMaterialRequisitionByID(string sysDocID, string voucherID);

		DataSet GetJobMaterialRequisitionAll();

		DataSet GetJobMaterialRequisitionToPrint(string sysDocID, string[] voucherID);

		DataSet GetJobMaterialRequisitionToPrint(string sysDocID, string voucherID);

		bool DeleteJobMaterialRequisition(string sysDocID, string voucherID);

		bool SetOrderStatus(string sysDocID, string voucherID, int status);

		DataSet GetList(DateTime from, DateTime to, bool showVoid);

		DataSet AllowDelete(string sysDocID, string voucherNumber);
	}
}
