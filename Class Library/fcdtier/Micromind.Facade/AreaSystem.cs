using Micromind.Common.Data;
using Micromind.Common.Interfaces;
using Micromind.Data;
using System;
using System.Data;

namespace Micromind.Facade
{
	public sealed class AreaSystem : MarshalByRefObject, IAreaSystem, IDisposable
	{
		private Config config;

		public AreaSystem(Config config)
		{
			this.config = config;
		}

		public void Dispose()
		{
		}

		public bool CreateArea(AreaData data)
		{
			return new Area(config).InsertArea(data);
		}

		public bool UpdateArea(AreaData data)
		{
			return UpdateArea(data, checkConcurrency: false);
		}

		public bool UpdateArea(AreaData data, bool checkConcurrency)
		{
			return new Area(config).UpdateArea(data);
		}

		public AreaData GetArea()
		{
			using (Area area = new Area(config))
			{
				return area.GetArea();
			}
		}

		public bool DeleteArea(string groupID)
		{
			using (Area area = new Area(config))
			{
				return area.DeleteArea(groupID);
			}
		}

		public AreaData GetAreaByID(string id)
		{
			using (Area area = new Area(config))
			{
				return area.GetAreaByID(id);
			}
		}

		public DataSet GetAreaByFields(params string[] columns)
		{
			using (Area area = new Area(config))
			{
				return area.GetAreaByFields(columns);
			}
		}

		public DataSet GetAreaByFields(string[] ids, params string[] columns)
		{
			using (Area area = new Area(config))
			{
				return area.GetAreaByFields(ids, columns);
			}
		}

		public DataSet GetAreaByFields(string[] ids, bool isInactive, params string[] columns)
		{
			using (Area area = new Area(config))
			{
				return area.GetAreaByFields(ids, isInactive, columns);
			}
		}

		public DataSet GetAreaList()
		{
			using (Area area = new Area(config))
			{
				return area.GetAreaList();
			}
		}

		public DataSet GetAreaComboList()
		{
			using (Area area = new Area(config))
			{
				return area.GetAreaComboList();
			}
		}
	}
}
