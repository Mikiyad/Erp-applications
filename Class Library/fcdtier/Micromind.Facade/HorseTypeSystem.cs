using Micromind.Common.Data;
using Micromind.Common.Interfaces;
using Micromind.Data;
using System;
using System.Data;

namespace Micromind.Facade
{
	public sealed class HorseTypeSystem : MarshalByRefObject, IHorseTypeSystem, IDisposable
	{
		private Config config;

		public HorseTypeSystem(Config config)
		{
			this.config = config;
		}

		public void Dispose()
		{
		}

		public bool CreateHorseType(HorseTypeData data)
		{
			return new HorseType(config).InsertHorseType(data);
		}

		public bool UpdateHorseType(HorseTypeData data)
		{
			return UpdateHorseType(data, checkConcurrency: false);
		}

		public bool UpdateHorseType(HorseTypeData data, bool checkConcurrency)
		{
			return new HorseType(config).UpdateHorseType(data);
		}

		public HorseTypeData GetHorseType()
		{
			using (HorseType horseType = new HorseType(config))
			{
				return horseType.GetHorseType();
			}
		}

		public bool DeleteHorseType(string groupID)
		{
			using (HorseType horseType = new HorseType(config))
			{
				return horseType.DeleteHorseType(groupID);
			}
		}

		public HorseTypeData GetHorseTypeByID(string id)
		{
			using (HorseType horseType = new HorseType(config))
			{
				return horseType.GetHorseTypeByID(id);
			}
		}

		public DataSet GetHorseTypeByFields(params string[] columns)
		{
			using (HorseType horseType = new HorseType(config))
			{
				return horseType.GetHorseTypeByFields(columns);
			}
		}

		public DataSet GetHorseTypeByFields(string[] ids, params string[] columns)
		{
			using (HorseType horseType = new HorseType(config))
			{
				return horseType.GetHorseTypeByFields(ids, columns);
			}
		}

		public DataSet GetHorseTypeByFields(string[] ids, bool isInactive, params string[] columns)
		{
			using (HorseType horseType = new HorseType(config))
			{
				return horseType.GetHorseTypeByFields(ids, isInactive, columns);
			}
		}

		public DataSet GetHorseTypeList()
		{
			using (HorseType horseType = new HorseType(config))
			{
				return horseType.GetHorseTypeList();
			}
		}

		public DataSet GetHorseTypeComboList()
		{
			using (HorseType horseType = new HorseType(config))
			{
				return horseType.GetHorseTypeComboList();
			}
		}
	}
}
