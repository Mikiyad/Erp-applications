using Micromind.Common.Data;
using Micromind.Common.Interfaces;
using Micromind.Data;
using System;
using System.Data;

namespace Micromind.Facade
{
	public sealed class AdjustmentTypeSystem : MarshalByRefObject, IAdjustmentTypeSystem, IDisposable
	{
		private Config config;

		public AdjustmentTypeSystem(Config config)
		{
			this.config = config;
		}

		public void Dispose()
		{
		}

		public bool CreateAdjustmentType(AdjustmentTypeData data)
		{
			return new AdjustmentType(config).InsertAdjustmentType(data);
		}

		public bool UpdateAdjustmentType(AdjustmentTypeData data)
		{
			return UpdateAdjustmentType(data, checkConcurrency: false);
		}

		public bool UpdateAdjustmentType(AdjustmentTypeData data, bool checkConcurrency)
		{
			return new AdjustmentType(config).UpdateAdjustmentType(data);
		}

		public AdjustmentTypeData GetAdjustmentType()
		{
			using (AdjustmentType adjustmentType = new AdjustmentType(config))
			{
				return adjustmentType.GetAdjustmentType();
			}
		}

		public bool DeleteAdjustmentType(string groupID)
		{
			using (AdjustmentType adjustmentType = new AdjustmentType(config))
			{
				return adjustmentType.DeleteAdjustmentType(groupID);
			}
		}

		public AdjustmentTypeData GetAdjustmentTypeByID(string id)
		{
			using (AdjustmentType adjustmentType = new AdjustmentType(config))
			{
				return adjustmentType.GetAdjustmentTypeByID(id);
			}
		}

		public DataSet GetAdjustmentTypeByFields(params string[] columns)
		{
			using (AdjustmentType adjustmentType = new AdjustmentType(config))
			{
				return adjustmentType.GetAdjustmentTypeByFields(columns);
			}
		}

		public DataSet GetAdjustmentTypeByFields(string[] ids, params string[] columns)
		{
			using (AdjustmentType adjustmentType = new AdjustmentType(config))
			{
				return adjustmentType.GetAdjustmentTypeByFields(ids, columns);
			}
		}

		public DataSet GetAdjustmentTypeByFields(string[] ids, bool isInactive, params string[] columns)
		{
			using (AdjustmentType adjustmentType = new AdjustmentType(config))
			{
				return adjustmentType.GetAdjustmentTypeByFields(ids, isInactive, columns);
			}
		}

		public DataSet GetAdjustmentTypeList()
		{
			using (AdjustmentType adjustmentType = new AdjustmentType(config))
			{
				return adjustmentType.GetAdjustmentTypeList();
			}
		}

		public DataSet GetAdjustmentTypeComboList()
		{
			using (AdjustmentType adjustmentType = new AdjustmentType(config))
			{
				return adjustmentType.GetAdjustmentTypeComboList();
			}
		}
	}
}
