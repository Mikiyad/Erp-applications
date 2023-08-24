using Micromind.Common.Data;
using Micromind.Common.Interfaces;
using Micromind.Data;
using System;
using System.Data;

namespace Micromind.Facade
{
	public sealed class PropertyVirtualUnitSystem : MarshalByRefObject, IPropertyVirtualUnitSystem, IDisposable
	{
		private Config config;

		public PropertyVirtualUnitSystem(Config config)
		{
			this.config = config;
		}

		public void Dispose()
		{
		}

		public bool CreatePropertyVirtualUnit(PropertyVirtualUnitData data)
		{
			return new PropertyVirtualUnit(config).InsertPropertyVirtualUnit(data);
		}

		public bool UpdatePropertyVirtualUnit(PropertyVirtualUnitData data)
		{
			return UpdatePropertyVirtualUnit(data, checkConcurrency: false);
		}

		public bool UpdatePropertyVirtualUnit(PropertyVirtualUnitData data, bool checkConcurrency)
		{
			return new PropertyVirtualUnit(config).UpdatePropertyVirtualUnit(data);
		}

		public PropertyVirtualUnitData GetPropertyVirtualUnit()
		{
			using (PropertyVirtualUnit propertyVirtualUnit = new PropertyVirtualUnit(config))
			{
				return propertyVirtualUnit.GetPropertyVirtualUnit();
			}
		}

		public bool DeletePropertyVirtualUnit(string groupID)
		{
			using (PropertyVirtualUnit propertyVirtualUnit = new PropertyVirtualUnit(config))
			{
				return propertyVirtualUnit.DeletePropertyVirtualUnit(groupID);
			}
		}

		public PropertyVirtualUnitData GetPropertyVirtualUnitByID(string id)
		{
			using (PropertyVirtualUnit propertyVirtualUnit = new PropertyVirtualUnit(config))
			{
				return propertyVirtualUnit.GetPropertyVirtualUnitByID(id);
			}
		}

		public DataSet GetPropertyVirtualUnitByFields(params string[] columns)
		{
			using (PropertyVirtualUnit propertyVirtualUnit = new PropertyVirtualUnit(config))
			{
				return propertyVirtualUnit.GetPropertyVirtualUnitByFields(columns);
			}
		}

		public DataSet GetPropertyVirtualUnitByFields(string[] ids, params string[] columns)
		{
			using (PropertyVirtualUnit propertyVirtualUnit = new PropertyVirtualUnit(config))
			{
				return propertyVirtualUnit.GetPropertyVirtualUnitByFields(ids, columns);
			}
		}

		public DataSet GetPropertyVirtualUnitByFields(string[] ids, bool isInactive, params string[] columns)
		{
			using (PropertyVirtualUnit propertyVirtualUnit = new PropertyVirtualUnit(config))
			{
				return propertyVirtualUnit.GetPropertyVirtualUnitByFields(ids, isInactive, columns);
			}
		}

		public DataSet GetPropertyVirtualUnitList(bool showInactive)
		{
			using (PropertyVirtualUnit propertyVirtualUnit = new PropertyVirtualUnit(config))
			{
				return propertyVirtualUnit.GetPropertyVirtualUnitList(showInactive);
			}
		}

		public DataSet GetPropertyVirtualUnitComboList()
		{
			using (PropertyVirtualUnit propertyVirtualUnit = new PropertyVirtualUnit(config))
			{
				return propertyVirtualUnit.GetPropertyVirtualUnitComboList();
			}
		}
	}
}
