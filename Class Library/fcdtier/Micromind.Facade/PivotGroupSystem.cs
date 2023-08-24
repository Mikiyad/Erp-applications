using Micromind.Common.Data;
using Micromind.Common.Interfaces;
using Micromind.Data;
using System;
using System.Data;

namespace Micromind.Facade
{
	public sealed class PivotGroupSystem : MarshalByRefObject, IPivotGroupSystem, IDisposable
	{
		private Config config;

		public PivotGroupSystem(Config config)
		{
			this.config = config;
		}

		public void Dispose()
		{
		}

		public bool CreatePivotGroup(PivotGroupData data)
		{
			return new PivotGroup(config).InsertPivotGroup(data);
		}

		public bool UpdatePivotGroup(PivotGroupData data)
		{
			return UpdatePivotGroup(data, checkConcurrency: false);
		}

		public bool UpdatePivotGroup(PivotGroupData data, bool checkConcurrency)
		{
			return new PivotGroup(config).UpdatePivotGroup(data);
		}

		public PivotGroupData GetPivotGroup()
		{
			using (PivotGroup pivotGroup = new PivotGroup(config))
			{
				return pivotGroup.GetPivotGroup();
			}
		}

		public bool DeletePivotGroup(string groupID)
		{
			using (PivotGroup pivotGroup = new PivotGroup(config))
			{
				return pivotGroup.DeletePivotGroup(groupID);
			}
		}

		public PivotGroupData GetPivotGroupByID(string id)
		{
			using (PivotGroup pivotGroup = new PivotGroup(config))
			{
				return pivotGroup.GetPivotGroupByID(id);
			}
		}

		public DataSet GetPivotGroupByFields(params string[] columns)
		{
			using (PivotGroup pivotGroup = new PivotGroup(config))
			{
				return pivotGroup.GetPivotGroupByFields(columns);
			}
		}

		public DataSet GetPivotGroupByFields(string[] ids, params string[] columns)
		{
			using (PivotGroup pivotGroup = new PivotGroup(config))
			{
				return pivotGroup.GetPivotGroupByFields(ids, columns);
			}
		}

		public DataSet GetPivotGroupByFields(string[] ids, bool isInactive, params string[] columns)
		{
			using (PivotGroup pivotGroup = new PivotGroup(config))
			{
				return pivotGroup.GetPivotGroupByFields(ids, isInactive, columns);
			}
		}

		public DataSet GetPivotGroupList()
		{
			using (PivotGroup pivotGroup = new PivotGroup(config))
			{
				return pivotGroup.GetPivotGroupList();
			}
		}

		public DataSet GetPivotGroupComboList()
		{
			using (PivotGroup pivotGroup = new PivotGroup(config))
			{
				return pivotGroup.GetPivotGroupComboList();
			}
		}

		public int CreateGroup(string groupName)
		{
			using (PivotGroup pivotGroup = new PivotGroup(config))
			{
				return pivotGroup.CreateGroup(groupName);
			}
		}
	}
}
