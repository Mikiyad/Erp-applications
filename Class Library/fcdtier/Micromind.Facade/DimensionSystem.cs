using Micromind.Common.Data;
using Micromind.Common.Interfaces;
using Micromind.Data;
using System;
using System.Data;

namespace Micromind.Facade
{
	public sealed class DimensionSystem : MarshalByRefObject, IDimensionSystem, IDisposable
	{
		private Config config;

		public DimensionSystem(Config config)
		{
			this.config = config;
		}

		public void Dispose()
		{
		}

		public bool CreateDimension(DimensionData data)
		{
			return new Dimensions(config).InsertUpdateDimension(data, isUpdate: false);
		}

		public bool UpdateDimension(DimensionData data)
		{
			return UpdateDimension(data, checkConcurrency: false);
		}

		public bool UpdateDimension(DimensionData data, bool checkConcurrency)
		{
			return new Dimensions(config).InsertUpdateDimension(data, isUpdate: true);
		}

		public DimensionData GetDimension()
		{
			using (Dimensions dimensions = new Dimensions(config))
			{
				return dimensions.GetDimension();
			}
		}

		public bool DeleteDimension(string groupID)
		{
			using (Dimensions dimensions = new Dimensions(config))
			{
				return dimensions.DeleteDimension(groupID);
			}
		}

		public DimensionData GetDimensionByID(string id)
		{
			using (Dimensions dimensions = new Dimensions(config))
			{
				return dimensions.GetDimensionByID(id);
			}
		}

		public DataSet GetDimensionByFields(params string[] columns)
		{
			using (Dimensions dimensions = new Dimensions(config))
			{
				return dimensions.GetDimensionByFields(columns);
			}
		}

		public DataSet GetDimensionByFields(string[] ids, params string[] columns)
		{
			using (Dimensions dimensions = new Dimensions(config))
			{
				return dimensions.GetDimensionByFields(ids, columns);
			}
		}

		public DataSet GetDimensionByFields(string[] ids, bool isInactive, params string[] columns)
		{
			using (Dimensions dimensions = new Dimensions(config))
			{
				return dimensions.GetDimensionByFields(ids, isInactive, columns);
			}
		}

		public DataSet GetDimensionList()
		{
			using (Dimensions dimensions = new Dimensions(config))
			{
				return dimensions.GetDimensionList();
			}
		}

		public DataSet GetDimensionComboList()
		{
			using (Dimensions dimensions = new Dimensions(config))
			{
				return dimensions.GetDimensionComboList();
			}
		}

		public DataSet GetDimensionAttributes(string dimensionID)
		{
			using (Dimensions dimensions = new Dimensions(config))
			{
				return dimensions.GetDimensionAttributes(dimensionID);
			}
		}
	}
}
