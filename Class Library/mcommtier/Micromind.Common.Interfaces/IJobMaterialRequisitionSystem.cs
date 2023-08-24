using Micromind.Common.Data;
using System;
using System.Data;

namespace Micromind.Common.Interfaces
{
	public interface IJobMaterialRequisitionSystem
	{
		bool CreateJobMaterialRequisition(JobMaterialRequisitionData materialRequistiontData, bool isUpdate);

		JobMaterialRequisitionData GetJobMaterialRequisitionByID(string sysDocID, string voucherID);

		DataSet GetJobMaterialRequisitionAll();

		DataSet GetJobMaterialRequisition(int mrFlag);

		DataSet GetJobMaterialRequisitionToPrint(string sysDocID, string[] voucherID);

		DataSet GetJobMaterialRequisitionToPrint(string sysDocID, string voucherID);

		bool DeleteJobMaterialRequisition(string sysDocID, string voucherID);

		bool SetOrderStatus(string sysDocID, string voucherID, int status);

		DataSet GetList(DateTime from, DateTime to, bool showVoid);

		DataSet AllowDelete(string sysDocID, string voucherNumber);

		DataSet GetRequisitionList(string SysDocIDS, DateTime from, DateTime to);

		DataSet GetMateralRequisitionFlowReport(DateTime fromDate, DateTime toDate, string fromItem, string toItem, string fromItemClass, string toItemClass, string fromItemCategory, string toItemCategory, string fromVendor, string toVendor, string fromVendorClass, string toVendorClass, string fromVendorGroup, string toVendorGroup, string fromLocation, string toLocation, string fromJob, string toJob, string SysDocID, string VoucherID, string fromManufacturer, string toManufacturer, string fromStyle, string toStyle, string fromOrigin, string toOrigin);
	}
}
