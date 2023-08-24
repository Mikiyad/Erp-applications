using Micromind.Common.Data;
using Micromind.Common.Interfaces;
using Micromind.Data;
using System;
using System.Data;

namespace Micromind.Facade
{
	public sealed class FixedAssetLocationSystem : MarshalByRefObject, IFixedAssetLocationSystem, IDisposable
	{
		private Config config;

		public FixedAssetLocationSystem(Config config)
		{
			this.config = config;
		}

		public void Dispose()
		{
		}

		public bool CreateAssetLocation(FixedAssetLocationData data)
		{
			return new FixedAssetLocation(config).InsertAssetLocation(data);
		}

		public bool UpdateAssetLocation(FixedAssetLocationData data)
		{
			return UpdateAssetLocation(data, checkConcurrency: false);
		}

		public bool UpdateAssetLocation(FixedAssetLocationData data, bool checkConcurrency)
		{
			return new FixedAssetLocation(config).UpdateAssetLocation(data);
		}

		public FixedAssetLocationData GetAssetLocation()
		{
			using (FixedAssetLocation fixedAssetLocation = new FixedAssetLocation(config))
			{
				return fixedAssetLocation.GetAssetLocation();
			}
		}

		public bool DeleteAssetLocation(string groupID)
		{
			using (FixedAssetLocation fixedAssetLocation = new FixedAssetLocation(config))
			{
				return fixedAssetLocation.DeleteAssetLocation(groupID);
			}
		}

		public FixedAssetLocationData GetAssetLocationByID(string id)
		{
			using (FixedAssetLocation fixedAssetLocation = new FixedAssetLocation(config))
			{
				return fixedAssetLocation.GetAssetLocationByID(id);
			}
		}

		public DataSet GetAssetLocationByFields(params string[] columns)
		{
			using (FixedAssetLocation fixedAssetLocation = new FixedAssetLocation(config))
			{
				return fixedAssetLocation.GetAssetLocationByFields(columns);
			}
		}

		public DataSet GetAssetLocationByFields(string[] ids, params string[] columns)
		{
			using (FixedAssetLocation fixedAssetLocation = new FixedAssetLocation(config))
			{
				return fixedAssetLocation.GetAssetLocationByFields(ids, columns);
			}
		}

		public DataSet GetAssetLocationByFields(string[] ids, bool isInactive, params string[] columns)
		{
			using (FixedAssetLocation fixedAssetLocation = new FixedAssetLocation(config))
			{
				return fixedAssetLocation.GetAssetLocationByFields(ids, isInactive, columns);
			}
		}

		public DataSet GetAssetLocationList()
		{
			using (FixedAssetLocation fixedAssetLocation = new FixedAssetLocation(config))
			{
				return fixedAssetLocation.GetAssetLocationList();
			}
		}

		public DataSet GetAssetLocationComboList()
		{
			using (FixedAssetLocation fixedAssetLocation = new FixedAssetLocation(config))
			{
				return fixedAssetLocation.GetAssetLocationComboList();
			}
		}
	}
}
