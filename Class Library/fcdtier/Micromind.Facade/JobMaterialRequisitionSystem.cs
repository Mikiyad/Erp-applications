using Micromind.Common.Data;
using Micromind.Common.Interfaces;
using Micromind.Data;
using System;
using System.Data;

namespace Micromind.Facade
{
	public sealed class JobMaterialRequisitionSystem : MarshalByRefObject, IJobMaterialRequisitionSystem, IDisposable
	{
		private Config config;

		public JobMaterialRequisitionSystem(Config config)
		{
			this.config = config;
		}

		public void Dispose()
		{
		}

		public bool CreateJobMaterialRequisition(JobMaterialRequisitionData data, bool isUpdate)
		{
			return new JobMaterialRequisition(config).InsertUpdateJobMaterialRequisition(data, isUpdate);
		}

		public JobMaterialRequisitionData GetJobMaterialRequisitionByID(string sysDocID, string voucherID)
		{
			return new JobMaterialRequisition(config).GetJobMaterialRequisitionByID(sysDocID, voucherID);
		}

		public DataSet GetJobMaterialRequisitionAll()
		{
			return new JobMaterialRequisition(config).GetJobMaterialRequisitionAll();
		}

		public DataSet GetJobMaterialRequisition(int mrFlag)
		{
			return new JobMaterialRequisition(config).GetJobMaterialRequisition(mrFlag);
		}

		public bool DeleteJobMaterialRequisition(string sysDocID, string voucherID)
		{
			return new JobMaterialRequisition(config).DeleteJobMaterialRequisition(sysDocID, voucherID);
		}

		public DataSet GetJobMaterialRequisitionToPrint(string sysDocID, string[] voucherID)
		{
			return new JobMaterialRequisition(config).GetJobMaterialRequisitionToPrint(sysDocID, voucherID);
		}

		public DataSet GetJobMaterialRequisitionToPrint(string sysDocID, string voucherID)
		{
			return new JobMaterialRequisition(config).GetJobMaterialRequisitionToPrint(sysDocID, new string[1]
			{
				voucherID
			});
		}

		public bool SetOrderStatus(string sysDocID, string voucherID, int status)
		{
			return new JobMaterialRequisition(config).SetOrderStatus(sysDocID, voucherID, status);
		}

		public DataSet GetList(DateTime fromDate, DateTime toDate, bool Isvoid)
		{
			return new JobMaterialRequisition(config).GetJobMaterialRequisitionList(fromDate, toDate, Isvoid: true);
		}

		public DataSet AllowDelete(string sysDocID, string voucherNumber)
		{
			return new JobMaterialRequisition(config).AllowDelete(sysDocID, voucherNumber);
		}

		public DataSet GetRequisitionList(string sysDocID, DateTime fromDate, DateTime endDate)
		{
			using (new ConsignIn(config))
			{
				return new JobMaterialRequisition(config).GetRequisitionList(sysDocID, fromDate, endDate);
			}
		}

		public DataSet GetMateralRequisitionFlowReport(DateTime fromDate, DateTime toDate, string fromItem, string toItem, string fromItemClass, string toItemClass, string fromItemCategory, string toItemCategory, string fromVendor, string toVendor, string fromVendorClass, string toVendorClass, string fromVendorGroup, string toVendorGroup, string fromLocation, string toLocation, string toJob, string fromJob, string SysDocId, string VoucherID, string fromManufacturer, string toManufacturer, string fromStyle, string toStyle, string fromOrigin, string toOrigin)
		{
			using (new Products(config))
			{
				return new JobMaterialRequisition(config).GetMateralRequisitionFlowReport(fromDate, toDate, fromItem, toItem, fromItemClass, toItemClass, fromItemCategory, toItemCategory, fromVendor, toVendor, fromVendorClass, toVendorClass, fromVendorGroup, toVendorGroup, fromLocation, toLocation, fromJob, toJob, SysDocId, VoucherID, fromManufacturer, toManufacturer, fromStyle, toStyle, fromOrigin, toOrigin);
			}
		}
	}
}
