using Micromind.Common.Data;
using Micromind.Common.Interfaces;
using Micromind.Data;
using System;
using System.Data;

namespace Micromind.Facade
{
	public sealed class RackSystem : MarshalByRefObject, IRackSystem, IDisposable
	{
		private Config config;

		public RackSystem(Config config)
		{
			this.config = config;
		}

		public void Dispose()
		{
		}

		public bool CreateRack(RackData data)
		{
			return new Rack(config).InsertRack(data);
		}

		public bool UpdateRack(RackData data)
		{
			return UpdateRack(data, checkConcurrency: false);
		}

		public bool UpdateRack(RackData data, bool checkConcurrency)
		{
			return new Rack(config).UpdateRack(data);
		}

		public RackData GetRack()
		{
			using (Rack rack = new Rack(config))
			{
				return rack.GetRack();
			}
		}

		public bool DeleteRack(string groupID)
		{
			using (Rack rack = new Rack(config))
			{
				return rack.DeleteRack(groupID);
			}
		}

		public RackData GetRackByID(string id)
		{
			using (Rack rack = new Rack(config))
			{
				return rack.GetRackByID(id);
			}
		}

		public DataSet GetJobTaskByFields(params string[] columns)
		{
			using (Rack rack = new Rack(config))
			{
				return rack.GetJobTaskByFields(columns);
			}
		}

		public DataSet GetJobTaskByFields(string[] ids, params string[] columns)
		{
			using (Rack rack = new Rack(config))
			{
				return rack.GetJobTaskByFields(ids, columns);
			}
		}

		public DataSet GetJobTaskByFields(string[] ids, bool isInactive, params string[] columns)
		{
			using (Rack rack = new Rack(config))
			{
				return rack.GetJobTaskByFields(ids, isInactive, columns);
			}
		}

		public DataSet GetRackList(bool isInactive)
		{
			using (Rack rack = new Rack(config))
			{
				return rack.GetRackList(isInactive);
			}
		}

		public DataSet GetRackComboList()
		{
			using (Rack rack = new Rack(config))
			{
				return rack.GetRackComboList();
			}
		}
	}
}
