using Micromind.Common.Data;
using Micromind.Common.Interfaces;
using Micromind.Data;
using System;
using System.Data;

namespace Micromind.Facade
{
	public sealed class FixedAssetGroupSystem : MarshalByRefObject, IFixedAssetGroupSystem, IDisposable
	{
		private Config config;

		public FixedAssetGroupSystem(Config config)
		{
			this.config = config;
		}

		public void Dispose()
		{
		}

		public bool CreateAssetGroup(FixedAssetGroupData data)
		{
			return new FixedAssetGroup(config).InsertAssetGroup(data);
		}

		public bool UpdateAssetGroup(FixedAssetGroupData data)
		{
			return UpdateAssetGroup(data, checkConcurrency: false);
		}

		public bool UpdateAssetGroup(FixedAssetGroupData data, bool checkConcurrency)
		{
			return new FixedAssetGroup(config).UpdateAssetGroup(data);
		}

		public FixedAssetGroupData GetAssetGroup()
		{
			using (FixedAssetGroup fixedAssetGroup = new FixedAssetGroup(config))
			{
				return fixedAssetGroup.GetAssetGroup();
			}
		}

		public bool DeleteAssetGroup(string groupID)
		{
			using (FixedAssetGroup fixedAssetGroup = new FixedAssetGroup(config))
			{
				return fixedAssetGroup.DeleteAssetGroup(groupID);
			}
		}

		public FixedAssetGroupData GetAssetGroupByID(string id)
		{
			using (FixedAssetGroup fixedAssetGroup = new FixedAssetGroup(config))
			{
				return fixedAssetGroup.GetAssetGroupByID(id);
			}
		}

		public DataSet GetAssetGroupByFields(params string[] columns)
		{
			using (FixedAssetGroup fixedAssetGroup = new FixedAssetGroup(config))
			{
				return fixedAssetGroup.GetAssetGroupByFields(columns);
			}
		}

		public DataSet GetAssetGroupByFields(string[] ids, params string[] columns)
		{
			using (FixedAssetGroup fixedAssetGroup = new FixedAssetGroup(config))
			{
				return fixedAssetGroup.GetAssetGroupByFields(ids, columns);
			}
		}

		public DataSet GetAssetGroupByFields(string[] ids, bool isInactive, params string[] columns)
		{
			using (FixedAssetGroup fixedAssetGroup = new FixedAssetGroup(config))
			{
				return fixedAssetGroup.GetAssetGroupByFields(ids, isInactive, columns);
			}
		}

		public DataSet GetAssetGroupList()
		{
			using (FixedAssetGroup fixedAssetGroup = new FixedAssetGroup(config))
			{
				return fixedAssetGroup.GetAssetGroupList();
			}
		}

		public DataSet GetAssetGroupComboList()
		{
			using (FixedAssetGroup fixedAssetGroup = new FixedAssetGroup(config))
			{
				return fixedAssetGroup.GetAssetGroupComboList();
			}
		}
	}
}
