using Micromind.Common.Data;
using Micromind.Common.Interfaces;
using Micromind.Data;
using System;
using System.Data;

namespace Micromind.Facade
{
	public sealed class UnitSystem : MarshalByRefObject, IUnitSystem, IDisposable
	{
		private Config config;

		public UnitSystem(Config config)
		{
			this.config = config;
		}

		public void Dispose()
		{
		}

		public bool CreateUnit(UnitData data)
		{
			return new Unit(config).InsertUnit(data);
		}

		public bool UpdateUnit(UnitData data)
		{
			return UpdateUnit(data, checkConcurrency: false);
		}

		public bool UpdateUnit(UnitData data, bool checkConcurrency)
		{
			return new Unit(config).UpdateUnit(data);
		}

		public UnitData GetUnit()
		{
			using (Unit unit = new Unit(config))
			{
				return unit.GetUnit();
			}
		}

		public bool DeleteUnit(string groupID)
		{
			using (Unit unit = new Unit(config))
			{
				return unit.DeleteUnit(groupID);
			}
		}

		public UnitData GetUnitByID(string id)
		{
			using (Unit unit = new Unit(config))
			{
				return unit.GetUnitByID(id);
			}
		}

		public DataSet GetUnitByFields(params string[] columns)
		{
			using (Unit unit = new Unit(config))
			{
				return unit.GetUnitByFields(columns);
			}
		}

		public DataSet GetUnitByFields(string[] ids, params string[] columns)
		{
			using (Unit unit = new Unit(config))
			{
				return unit.GetUnitByFields(ids, columns);
			}
		}

		public DataSet GetUnitByFields(string[] ids, bool isInactive, params string[] columns)
		{
			using (Unit unit = new Unit(config))
			{
				return unit.GetUnitByFields(ids, isInactive, columns);
			}
		}

		public DataSet GetUnitList()
		{
			using (Unit unit = new Unit(config))
			{
				return unit.GetUnitList();
			}
		}

		public DataSet GetUnitComboList()
		{
			using (Unit unit = new Unit(config))
			{
				return unit.GetUnitComboList();
			}
		}

		public DataSet GetProductUnitDetailComboList()
		{
			using (Unit unit = new Unit(config))
			{
				return unit.GetProductUnitDetailComboList();
			}
		}
	}
}
