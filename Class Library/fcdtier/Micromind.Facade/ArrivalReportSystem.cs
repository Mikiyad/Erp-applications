using Micromind.Common.Data;
using Micromind.Common.Interfaces;
using Micromind.Data;
using System;
using System.Data;

namespace Micromind.Facade
{
	public sealed class ArrivalReportSystem : MarshalByRefObject, IArrivalReportSystem, IDisposable
	{
		private Config config;

		public ArrivalReportSystem(Config config)
		{
			this.config = config;
		}

		public void Dispose()
		{
		}

		public bool CreateArrivalReport(ArrivalReportData data, bool isUpdate)
		{
			return new ArrivalReport(config).InsertUpdateArrivalReport(data, isUpdate);
		}

		public ArrivalReportData GetArrivalReportByID(string sysDocID, string voucherID)
		{
			return new ArrivalReport(config).GetArrivalReportByID(sysDocID, voucherID);
		}

		public bool DeleteArrivalReport(string sysDocID, string voucherID)
		{
			return new ArrivalReport(config).DeleteArrivalReport(sysDocID, voucherID);
		}

		public DataSet GetClaimableArrivalReports()
		{
			return new ArrivalReport(config).GetClaimableArrivalReports();
		}

		public DataSet GetArrivalReportToPrint(string sysDocID, string[] voucherID)
		{
			return new ArrivalReport(config).GetArrivalReportToPrint(sysDocID, voucherID);
		}

		public DataSet GetArrivalReportToPrint(string sysDocID, string voucherID)
		{
			return new ArrivalReport(config).GetArrivalReportToPrint(sysDocID, new string[1]
			{
				voucherID
			});
		}

		public DataSet GetList(DateTime from, DateTime to, bool isImport, bool showVoid)
		{
			return new ArrivalReport(config).GetList(from, to, isImport, showVoid);
		}

		public bool CanUpdate(string sysDocID, string voucherNumber)
		{
			return new ArrivalReport(config).CanUpdate(sysDocID, voucherNumber, null);
		}
	}
}
