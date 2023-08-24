using Micromind.Common.Data;
using Micromind.Common.Interfaces;
using Micromind.Data;
using System;
using System.Data;

namespace Micromind.Facade
{
	public sealed class POSBatchSystem : MarshalByRefObject, IPOSBatchSystem, IDisposable
	{
		private Config config;

		public POSBatchSystem(Config config)
		{
			this.config = config;
		}

		public void Dispose()
		{
		}

		public bool CreatePOSBatch(POSBatchData data)
		{
			return new POSBatch(config).InsertPOSBatch(data);
		}

		public bool UpdatePOSBatch(POSBatchData data)
		{
			return UpdatePOSBatch(data, checkConcurrency: false);
		}

		public bool UpdatePOSBatch(POSBatchData data, bool checkConcurrency)
		{
			return new POSBatch(config).UpdatePOSBatch(data);
		}

		public POSBatchData GetPOSBatch()
		{
			using (POSBatch pOSBatch = new POSBatch(config))
			{
				return pOSBatch.GetPOSBatch();
			}
		}

		public bool DeletePOSBatch(string groupID)
		{
			using (POSBatch pOSBatch = new POSBatch(config))
			{
				return pOSBatch.DeletePOSBatch(groupID);
			}
		}

		public POSBatchData GetPOSBatchByID(string id)
		{
			using (POSBatch pOSBatch = new POSBatch(config))
			{
				return pOSBatch.GetPOSBatchByID(id);
			}
		}

		public DataSet GetPOSBatchByFields(params string[] columns)
		{
			using (POSBatch pOSBatch = new POSBatch(config))
			{
				return pOSBatch.GetPOSBatchByFields(columns);
			}
		}

		public DataSet GetPOSBatchByFields(string[] ids, params string[] columns)
		{
			using (POSBatch pOSBatch = new POSBatch(config))
			{
				return pOSBatch.GetPOSBatchByFields(ids, columns);
			}
		}

		public DataSet GetPOSBatchByFields(string[] ids, bool isInactive, params string[] columns)
		{
			using (POSBatch pOSBatch = new POSBatch(config))
			{
				return pOSBatch.GetPOSBatchByFields(ids, isInactive, columns);
			}
		}

		public DataSet GetPOSBatchList()
		{
			using (POSBatch pOSBatch = new POSBatch(config))
			{
				return pOSBatch.GetPOSBatchList();
			}
		}

		public DataSet GetPOSBatchComboList()
		{
			using (POSBatch pOSBatch = new POSBatch(config))
			{
				return pOSBatch.GetPOSBatchComboList();
			}
		}

		public int GetCurrentOpenBatchID(string locationID)
		{
			using (POSBatch pOSBatch = new POSBatch(config))
			{
				return pOSBatch.GetCurrentOpenBatchID(locationID);
			}
		}

		public bool ClosePOSBatch(int batchID, string locationID)
		{
			using (POSBatch pOSBatch = new POSBatch(config))
			{
				return pOSBatch.ClosePOSBatch(batchID, locationID);
			}
		}
	}
}
