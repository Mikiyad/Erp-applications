using Micromind.Common.Data;
using Micromind.Common.Interfaces;
using Micromind.Data;
using System;
using System.Data;

namespace Micromind.Facade
{
	public sealed class HorseSexSystem : MarshalByRefObject, IHorseSexSystem, IDisposable
	{
		private Config config;

		public HorseSexSystem(Config config)
		{
			this.config = config;
		}

		public void Dispose()
		{
		}

		public bool CreateHorseSex(HorseSexData data)
		{
			return new HorseSex(config).InsertHorseSex(data);
		}

		public bool UpdateHorseSex(HorseSexData data)
		{
			return UpdateHorseSex(data, checkConcurrency: false);
		}

		public bool UpdateHorseSex(HorseSexData data, bool checkConcurrency)
		{
			return new HorseSex(config).UpdateHorseSex(data);
		}

		public HorseSexData GetHorseSex()
		{
			using (HorseSex horseSex = new HorseSex(config))
			{
				return horseSex.GetHorseSex();
			}
		}

		public bool DeleteHorseSex(string groupID)
		{
			using (HorseSex horseSex = new HorseSex(config))
			{
				return horseSex.DeleteHorseSex(groupID);
			}
		}

		public HorseSexData GetHorseSexByID(string id)
		{
			using (HorseSex horseSex = new HorseSex(config))
			{
				return horseSex.GetHorseSexByID(id);
			}
		}

		public DataSet GetHorseSexByFields(params string[] columns)
		{
			using (HorseSex horseSex = new HorseSex(config))
			{
				return horseSex.GetHorseSexByFields(columns);
			}
		}

		public DataSet GetHorseSexByFields(string[] ids, params string[] columns)
		{
			using (HorseSex horseSex = new HorseSex(config))
			{
				return horseSex.GetHorseSexByFields(ids, columns);
			}
		}

		public DataSet GetHorseSexByFields(string[] ids, bool isInactive, params string[] columns)
		{
			using (HorseSex horseSex = new HorseSex(config))
			{
				return horseSex.GetHorseSexByFields(ids, isInactive, columns);
			}
		}

		public DataSet GetHorseSexList()
		{
			using (HorseSex horseSex = new HorseSex(config))
			{
				return horseSex.GetHorseSexList();
			}
		}

		public DataSet GetHorseSexComboList()
		{
			using (HorseSex horseSex = new HorseSex(config))
			{
				return horseSex.GetHorseSexComboList();
			}
		}
	}
}
