using Micromind.Common.Data;
using Micromind.Common.Interfaces;
using Micromind.Data;
using System;
using System.Data;

namespace Micromind.Facade
{
	public sealed class ServiceCallTrackSystem : MarshalByRefObject, IServiceCallTrackSystem, IDisposable
	{
		private Config config;

		public ServiceCallTrackSystem(Config config)
		{
			this.config = config;
		}

		public void Dispose()
		{
		}

		public bool CreateJobMaterialRequisition(ServiceCallTrackData data, bool isUpdate)
		{
			return new ServiceCallTrack(config).InsertUpdateJobMaterialRequisition(data, isUpdate);
		}

		public ServiceCallTrackData GetJobMaterialRequisitionByID(string sysDocID, string voucherID)
		{
			return new ServiceCallTrack(config).GetJobMaterialRequisitionByID(sysDocID, voucherID);
		}

		public DataSet GetJobMaterialRequisitionAll()
		{
			return new ServiceCallTrack(config).GetJobMaterialRequisitionAll();
		}

		public bool DeleteJobMaterialRequisition(string sysDocID, string voucherID)
		{
			return new ServiceCallTrack(config).DeleteJobMaterialRequisition(sysDocID, voucherID);
		}

		public DataSet GetJobMaterialRequisitionToPrint(string sysDocID, string[] voucherID)
		{
			return new ServiceCallTrack(config).GetJobMaterialRequisitionToPrint(sysDocID, voucherID);
		}

		public DataSet GetJobMaterialRequisitionToPrint(string sysDocID, string voucherID)
		{
			return new ServiceCallTrack(config).GetJobMaterialRequisitionToPrint(sysDocID, new string[1]
			{
				voucherID
			});
		}

		public bool SetOrderStatus(string sysDocID, string voucherID, int status)
		{
			return new ServiceCallTrack(config).SetOrderStatus(sysDocID, voucherID, status);
		}

		public DataSet GetList(DateTime fromDate, DateTime toDate, bool Isvoid)
		{
			return new ServiceCallTrack(config).GetJobMaterialRequisitionList(fromDate, toDate, Isvoid: true);
		}

		public DataSet AllowDelete(string sysDocID, string voucherNumber)
		{
			return new ServiceCallTrack(config).AllowDelete(sysDocID, voucherNumber);
		}
	}
}
