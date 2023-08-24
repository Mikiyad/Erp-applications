using Micromind.Common.Data;
using Micromind.Common.Interfaces;
using Micromind.Data;
using System;
using System.Data;

namespace Micromind.Facade
{
	public sealed class OverTimeSystem : MarshalByRefObject, IOverTimeSystem, IDisposable
	{
		private Config config;

		public OverTimeSystem(Config config)
		{
			this.config = config;
		}

		public void Dispose()
		{
		}

		public bool CreateOverTime(OverTimeData data)
		{
			return new OverTime(config).InsertOverTime(data);
		}

		public bool UpdateOverTime(OverTimeData data)
		{
			return UpdateOverTime(data, checkConcurrency: false);
		}

		public bool UpdateOverTime(OverTimeData data, bool checkConcurrency)
		{
			return new OverTime(config).UpdateOverTime(data);
		}

		public OverTimeData GetOverTime()
		{
			using (OverTime overTime = new OverTime(config))
			{
				return overTime.GetOverTime();
			}
		}

		public bool DeleteOverTime(string groupID)
		{
			using (OverTime overTime = new OverTime(config))
			{
				return overTime.DeleteOverTime(groupID);
			}
		}

		public OverTimeData GetOverTimeByID(string id)
		{
			using (OverTime overTime = new OverTime(config))
			{
				return overTime.GetOverTimeByID(id);
			}
		}

		public DataSet GetOverTimeByFields(params string[] columns)
		{
			using (OverTime overTime = new OverTime(config))
			{
				return overTime.GetOverTimeByFields(columns);
			}
		}

		public DataSet GetOverTimeByFields(string[] ids, params string[] columns)
		{
			using (OverTime overTime = new OverTime(config))
			{
				return overTime.GetOverTimeByFields(ids, columns);
			}
		}

		public DataSet GetOverTimeByFields(string[] ids, bool isInactive, params string[] columns)
		{
			using (OverTime overTime = new OverTime(config))
			{
				return overTime.GetOverTimeByFields(ids, isInactive, columns);
			}
		}

		public DataSet GetOverTimeList()
		{
			using (OverTime overTime = new OverTime(config))
			{
				return overTime.GetOverTimeList();
			}
		}

		public DataSet GetOverTimeComboList()
		{
			using (OverTime overTime = new OverTime(config))
			{
				return overTime.GetOverTimeComboList();
			}
		}
	}
}
