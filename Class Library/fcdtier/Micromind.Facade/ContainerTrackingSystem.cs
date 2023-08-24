using Micromind.Common.Data;
using Micromind.Common.Interfaces;
using Micromind.Data;
using System;
using System.Data;

namespace Micromind.Facade
{
	public sealed class ContainerTrackingSystem : MarshalByRefObject, IContainerTrackingSystem
	{
		private Config config;

		public ContainerTrackingSystem(Config config)
		{
			this.config = config;
		}

		public void Dispose()
		{
		}

		public bool CreateContainerTracking(ContainerTrackingData data, bool isUpdate)
		{
			return new ContainerTrackings(config).InsertUpdateContainerTracking(data, isUpdate);
		}

		public bool UpdateContainerTracking(ContainerTrackingData data)
		{
			return UpdateContainerTracking(data, checkConcurrency: false);
		}

		public bool UpdateContainerTracking(ContainerTrackingData data, bool checkConcurrency)
		{
			return new ContainerTrackings(config).InsertUpdateContainerTracking(data, isUpdate: true);
		}

		public ContainerTrackingData GetContainerTracking()
		{
			using (ContainerTrackings containerTrackings = new ContainerTrackings(config))
			{
				return containerTrackings.GetContainerTracking();
			}
		}

		public bool DeleteContainerTracking(string VoucherNumber)
		{
			using (ContainerTrackings containerTrackings = new ContainerTrackings(config))
			{
				return containerTrackings.DeleteContainerTracking(VoucherNumber);
			}
		}

		public ContainerTrackingData GetContainerTrackingByID(string id)
		{
			using (ContainerTrackings containerTrackings = new ContainerTrackings(config))
			{
				return containerTrackings.GetContainerTrackingByID(id);
			}
		}

		public DataSet GetContainerTrackingByFields(params string[] columns)
		{
			using (ContainerTrackings containerTrackings = new ContainerTrackings(config))
			{
				return containerTrackings.GetContainerTrackingByFields(columns);
			}
		}

		public DataSet GetContainerTrackingByFields(string[] ids, params string[] columns)
		{
			using (ContainerTrackings containerTrackings = new ContainerTrackings(config))
			{
				return containerTrackings.GetContainerTrackingByFields(ids, columns);
			}
		}

		public DataSet GetContainerTrackingByFields(string[] ids, bool isInactive, params string[] columns)
		{
			using (ContainerTrackings containerTrackings = new ContainerTrackings(config))
			{
				return containerTrackings.GetContainerTrackingByFields(ids, isInactive, columns);
			}
		}

		public DataSet GetContainerTrackingList(DateTime from, DateTime to, bool showVoid)
		{
			using (ContainerTrackings containerTrackings = new ContainerTrackings(config))
			{
				return containerTrackings.GetContainerTrackingList(from, to, showVoid);
			}
		}

		public DataSet GetContainerTrackingComboList()
		{
			using (ContainerTrackings containerTrackings = new ContainerTrackings(config))
			{
				return containerTrackings.GetContainerTrackingComboList();
			}
		}

		public DataSet GetContainerTrackingFilterComboList()
		{
			using (ContainerTrackings containerTrackings = new ContainerTrackings(config))
			{
				return containerTrackings.GetContainerTrackingFilterComboList();
			}
		}

		public DataSet GetContainerTrackingToPrint(string sysDocID, string[] voucherID)
		{
			return new ContainerTrackings(config).GetContainerTrackingToPrint(sysDocID, voucherID);
		}

		public DataSet GetContainerTrackingToPrint(string sysDocID, string voucherID)
		{
			return new ContainerTrackings(config).GetContainerTrackingToPrint(sysDocID, new string[1]
			{
				voucherID
			});
		}

		public DataSet GetContainerTrackingReport(string container)
		{
			return new ContainerTrackings(config).GetContainerTrackingReport(container);
		}

		public DataSet GetContainerDetailsNew(string containerNumber)
		{
			return new ContainerTrackings(config).GetContainerDetailsNew(containerNumber);
		}

		public DataSet GetContainerDetailsOnStatus(string containerNumber, int status)
		{
			return new ContainerTrackings(config).GetContainerDetailsOnStatus(containerNumber, status);
		}

		public ContainerTrackingData GetContainerDetailsOnStatusChange(string containerNumber)
		{
			return new ContainerTrackings(config).GetContainerDetailsOnStatusChange(containerNumber);
		}

		public bool VoidTracking(string sysDocID, string voucherID, bool isVoid)
		{
			return new ContainerTrackings(config).VoidTracking(sysDocID, voucherID, isVoid);
		}
	}
}
