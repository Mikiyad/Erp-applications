using Micromind.Common.Data;
using Micromind.Common.Interfaces;
using Micromind.Data;
using System;
using System.Data;

namespace Micromind.Facade
{
	public sealed class ProjectSubContractPISystem : MarshalByRefObject, IProjectSubContractPISystem, IDisposable
	{
		private Config config;

		public ProjectSubContractPISystem(Config config)
		{
			this.config = config;
		}

		public void Dispose()
		{
		}

		public bool CreateProjectSubContractPI(ProjectSubContractPIData data, bool isUpdate)
		{
			return new ProjectSubContractPI(config).InsertUpdateProjectSubContractPI(data, isUpdate);
		}

		public ProjectSubContractPIData GetProjectSubContractPIByID(string sysDocID, string voucherID)
		{
			return new ProjectSubContractPI(config).GetProjectSubContractPIByID(sysDocID, voucherID);
		}

		public bool DeleteProjectSubContractPI(string sysDocID, string voucherID)
		{
			return new ProjectSubContractPI(config).DeleteProjectSubContractPI(sysDocID, voucherID);
		}

		public bool VoidProjectSubContractPI(string sysDocID, string voucherID, bool isVoid)
		{
			return new ProjectSubContractPI(config).VoidProjectSubContractPI(sysDocID, voucherID, isVoid);
		}

		public DataSet GetInvoicesForDelivery(string customerID)
		{
			return new ProjectSubContractPI(config).GetInvoicesForDelivery(customerID);
		}

		public DataSet GetProjectSubContractPIToPrint(string sysDocID, string voucherID)
		{
			return new ProjectSubContractPI(config).GetProjectSubContractPIToPrint(sysDocID, voucherID);
		}

		public DataSet GetProjectSubContractPIToPrint(string sysDocID, string[] voucherID)
		{
			return new ProjectSubContractPI(config).GetProjectSubContractPIToPrint(sysDocID, voucherID);
		}

		public DataSet GetList(DateTime from, DateTime to, bool showVoid)
		{
			return new ProjectSubContractPI(config).GetList(from, to, showVoid);
		}

		public DataSet GetPurchaseExpenseAllocationReport(DateTime fromDate, DateTime toDate, string fromItem, string toItem, string fromClass, string toClass, string fromCategory, string toCategory, string sysDocID, string voucherID)
		{
			return new ProjectSubContractPI(config).GetPurchaseExpenseAllocationReport(fromDate, toDate, fromItem, toItem, fromClass, toClass, fromCategory, toCategory, sysDocID, voucherID);
		}

		public DataSet GetProjectSubContractPIList(string sysDocID)
		{
			return new ProjectSubContractPI(config).GetProjectSubContractPIList(sysDocID);
		}

		public DataSet GetPurchaseList(string sysDocID, DateTime fromDate, DateTime endDate)
		{
			return new ProjectSubContractPI(config).GetPurchaseList(sysDocID, fromDate, endDate);
		}

		public DataSet GetOpenOrdersSummary(string vendorID, bool isImport)
		{
			return new ProjectSubContractPI(config).GetOpenOrdersSummary(vendorID, isImport);
		}
	}
}
