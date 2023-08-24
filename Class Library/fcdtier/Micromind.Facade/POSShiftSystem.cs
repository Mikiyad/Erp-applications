using Micromind.Common.Data;
using Micromind.Common.Interfaces;
using Micromind.Data;
using System;
using System.Data;

namespace Micromind.Facade
{
	public sealed class POSShiftSystem : MarshalByRefObject, IPOSShiftSystem, IDisposable
	{
		private Config config;

		public POSShiftSystem(Config config)
		{
			this.config = config;
		}

		public void Dispose()
		{
		}

		public bool CreatePOSShift(POSShiftData data)
		{
			return new POSShift(config).CreatePOSShift(data);
		}

		public bool UpdatePOSShift(POSShiftData data)
		{
			return UpdatePOSShift(data, checkConcurrency: false);
		}

		public bool UpdatePOSShift(POSShiftData data, bool checkConcurrency)
		{
			return new POSShift(config).UpdatePOSShift(data);
		}

		public POSShiftData GetPOSShift()
		{
			using (POSShift pOSShift = new POSShift(config))
			{
				return pOSShift.GetPOSShift();
			}
		}

		public bool DeletePOSShift(string groupID)
		{
			using (POSShift pOSShift = new POSShift(config))
			{
				return pOSShift.DeletePOSShift(groupID);
			}
		}

		public POSShiftData GetPOSShiftByID(string id)
		{
			using (POSShift pOSShift = new POSShift(config))
			{
				return pOSShift.GetPOSShiftByID(id);
			}
		}

		public DataSet GetPOSShiftByFields(params string[] columns)
		{
			using (POSShift pOSShift = new POSShift(config))
			{
				return pOSShift.GetPOSShiftByFields(columns);
			}
		}

		public DataSet GetPOSShiftByFields(string[] ids, params string[] columns)
		{
			using (POSShift pOSShift = new POSShift(config))
			{
				return pOSShift.GetPOSShiftByFields(ids, columns);
			}
		}

		public DataSet GetPOSShiftByFields(string[] ids, bool isInactive, params string[] columns)
		{
			using (POSShift pOSShift = new POSShift(config))
			{
				return pOSShift.GetPOSShiftByFields(ids, isInactive, columns);
			}
		}

		public DataSet GetPOSShiftList()
		{
			using (POSShift pOSShift = new POSShift(config))
			{
				return pOSShift.GetPOSShiftList();
			}
		}

		public DataSet GetPOSShiftComboList()
		{
			using (POSShift pOSShift = new POSShift(config))
			{
				return pOSShift.GetPOSShiftComboList();
			}
		}

		public int GetCurrentOpenShiftID(string userID)
		{
			using (POSShift pOSShift = new POSShift(config))
			{
				return pOSShift.GetCurrentOpenShiftID(userID);
			}
		}

		public bool ClosePOSShift(int batchID, int shiftID, string registerID, decimal closingCash)
		{
			using (POSShift pOSShift = new POSShift(config))
			{
				return pOSShift.ClosePOSShift(batchID, shiftID, registerID, closingCash);
			}
		}
	}
}
