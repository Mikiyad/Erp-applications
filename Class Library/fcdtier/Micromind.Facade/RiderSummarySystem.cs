using Micromind.Common.Data;
using Micromind.Common.Interfaces;
using Micromind.Data;
using System;
using System.Data;

namespace Micromind.Facade
{
	public sealed class RiderSummarySystem : MarshalByRefObject, IRiderSummarySystem, IDisposable
	{
		private Config config;

		public RiderSummarySystem(Config config)
		{
			this.config = config;
		}

		public void Dispose()
		{
		}

		public bool CreateRiderSummary(RiderSummaryData data)
		{
			return new RiderSummary(config).InsertRiderSummary(data);
		}

		public bool UpdateRiderSummary(RiderSummaryData data)
		{
			return UpdateRiderSummary(data, checkConcurrency: false);
		}

		public bool UpdateRiderSummary(RiderSummaryData data, bool checkConcurrency)
		{
			return new RiderSummary(config).UpdateRiderSummary(data);
		}

		public RiderSummaryData GetRiderSummary()
		{
			using (RiderSummary riderSummary = new RiderSummary(config))
			{
				return riderSummary.GetRiderSummary();
			}
		}

		public bool DeleteRiderSummary(string groupID)
		{
			using (RiderSummary riderSummary = new RiderSummary(config))
			{
				return riderSummary.DeleteRiderSummary(groupID);
			}
		}

		public RiderSummaryData GetRiderSummaryByID(string id)
		{
			using (RiderSummary riderSummary = new RiderSummary(config))
			{
				return riderSummary.GetRiderSummaryByID(id);
			}
		}

		public DataSet GetRiderSummaryList(bool isInactive)
		{
			using (RiderSummary riderSummary = new RiderSummary(config))
			{
				return riderSummary.GetRiderSummaryList(isInactive);
			}
		}

		public DataSet GetRiderSummaryComboList()
		{
			using (RiderSummary riderSummary = new RiderSummary(config))
			{
				return riderSummary.GetRiderSummaryComboList();
			}
		}
	}
}
