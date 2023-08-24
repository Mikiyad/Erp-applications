using Micromind.Common.Data;
using Micromind.Common.Interfaces;
using Micromind.Data;
using System;
using System.Data;

namespace Micromind.Facade
{
	public sealed class ProjectSubContractPOSystem : MarshalByRefObject, IProjectSubContractPOSystem, IDisposable
	{
		private Config config;

		public ProjectSubContractPOSystem(Config config)
		{
			this.config = config;
		}

		public void Dispose()
		{
		}

		public bool CreateProjectSubContractPO(ProjectSubcontractPOData data, bool isUpdate)
		{
			return new ProjectSubContractPO(config).InsertUpdateProjectSubContractPO(data, isUpdate);
		}

		public ProjectSubcontractPOData GetProjectSubContractPOByID(string sysDocID, string voucherID)
		{
			return new ProjectSubContractPO(config).GetProjectSubContractPOByID(sysDocID, voucherID);
		}

		public ProjectSubcontractPOData GetProjectSubContractOrderByID(string sysDocID, string voucherID)
		{
			return new ProjectSubContractPO(config).GetProjectSubContractOrderByID(sysDocID, voucherID);
		}

		public bool DeleteProjectSubContractPO(string sysDocID, string voucherID)
		{
			return new ProjectSubContractPO(config).DeleteProjectSubContractPO(sysDocID, voucherID);
		}

		public bool VoidProjectSubContractPO(string sysDocID, string voucherID, bool isVoid)
		{
			return new ProjectSubContractPO(config).VoidProjectSubContractPO(sysDocID, voucherID, isVoid);
		}

		public DataSet GetOpenOrdersSummary(string vendorID, bool isImport)
		{
			return new ProjectSubContractPO(config).GetOpenOrdersSummary(vendorID, isImport);
		}

		public DataSet GetProjectSubContractPOAll()
		{
			return new ProjectSubContractPO(config).GetProjectSubContractPOAll();
		}

		public DataSet GetPOsForPackingList(string vendorID, bool isImport)
		{
			return new ProjectSubContractPO(config).GetPOsForPackingList(vendorID, isImport);
		}

		public DataSet GetPOsItemsToShip(string sysDocID, string voucherID)
		{
			return new ProjectSubContractPO(config).GetPOsItemsToShip(sysDocID, voucherID);
		}

		public DataSet GetOpenOrderListReport()
		{
			return new ProjectSubContractPO(config).GetOpenOrderListReport();
		}

		public DataSet GetProjectSubContractPOToPrint(string sysDocID, string[] voucherID)
		{
			return new ProjectSubContractPO(config).GetProjectSubContractPOToPrint(sysDocID, voucherID);
		}

		public DataSet GetProjectSubContractPOToPrint(string sysDocID, string voucherID)
		{
			return new ProjectSubContractPO(config).GetProjectSubContractPOToPrint(sysDocID, new string[1]
			{
				voucherID
			});
		}

		public DataSet GetProjectSubContractPODetailReport(DateTime from, DateTime to, string jobID, string vendorID, string fromItem, string toItem, string fromClass, string toClass, string fromCategory, string toCategory)
		{
			return new ProjectSubContractPO(config).GetProjectSubContractPODetailReport(from, to, jobID, vendorID, fromItem, toItem, fromClass, toClass, fromCategory, toCategory);
		}

		public DataSet GetList(DateTime from, DateTime to, bool isImport, bool showVoid)
		{
			return new ProjectSubContractPO(config).GetList(from, to, isImport, showVoid);
		}

		public bool CanUpdate(string sysDocID, string voucherNumber)
		{
			return new ProjectSubContractPO(config).CanUpdate(sysDocID, voucherNumber, null);
		}

		public bool SetOrderStatus(string sysDocID, string voucherID, PurchaseOrderStatus status)
		{
			return new ProjectSubContractPO(config).SetOrderStatus(sysDocID, voucherID, status);
		}

		public DataSet GetOpenPOComboData(string vendorID)
		{
			return new ProjectSubContractPO(config).GetOpenPOComboData(vendorID);
		}

		public DataSet GetPOPaymentSummary(string sysDocID, string voucherID)
		{
			return new ProjectSubContractPO(config).GetPOPaymentSummary(sysDocID, voucherID);
		}

		public DataSet GetPOListForPayment(string vendorID)
		{
			return new ProjectSubContractPO(config).GetPOListForPayment(vendorID);
		}

		public ProjectSubcontractPOData GetProjectSubContractPIByID(string sysDocID, string voucherID)
		{
			return new ProjectSubContractPO(config).GetProjectSubContractPIByID(sysDocID, voucherID);
		}
	}
}
