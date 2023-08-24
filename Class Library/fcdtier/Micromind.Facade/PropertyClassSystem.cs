using Micromind.Common.Data;
using Micromind.Common.Interfaces;
using Micromind.Data;
using System;
using System.Data;

namespace Micromind.Facade
{
	public sealed class PropertyClassSystem : MarshalByRefObject, IPropertyClassSystem, IDisposable
	{
		private Config config;

		public PropertyClassSystem(Config config)
		{
			this.config = config;
		}

		public void Dispose()
		{
		}

		public bool CreatePropertyClass(PropertyClassData data)
		{
			return new PropertyClass(config).InsertPropertyClass(data);
		}

		public bool UpdatePropertyClass(PropertyClassData data)
		{
			return UpdatePropertyClass(data, checkConcurrency: false);
		}

		public bool UpdatePropertyClass(PropertyClassData data, bool checkConcurrency)
		{
			return new PropertyClass(config).UpdatePropertyClass(data);
		}

		public PropertyClassData GetPropertyClass()
		{
			using (PropertyClass propertyClass = new PropertyClass(config))
			{
				return propertyClass.GetPropertyClass();
			}
		}

		public bool DeletePropertyClass(string groupID)
		{
			using (PropertyClass propertyClass = new PropertyClass(config))
			{
				return propertyClass.DeletePropertyClass(groupID);
			}
		}

		public PropertyClassData GetPropertyClassByID(string id)
		{
			using (PropertyClass propertyClass = new PropertyClass(config))
			{
				return propertyClass.GetPropertyClassByID(id);
			}
		}

		public DataSet GetPropertyClassByFields(params string[] columns)
		{
			using (PropertyClass propertyClass = new PropertyClass(config))
			{
				return propertyClass.GetPropertyClassByFields(columns);
			}
		}

		public DataSet GetPropertyClassByFields(string[] ids, params string[] columns)
		{
			using (PropertyClass propertyClass = new PropertyClass(config))
			{
				return propertyClass.GetPropertyClassByFields(ids, columns);
			}
		}

		public DataSet GetPropertyClassByFields(string[] ids, bool isInactive, params string[] columns)
		{
			using (PropertyClass propertyClass = new PropertyClass(config))
			{
				return propertyClass.GetPropertyClassByFields(ids, isInactive, columns);
			}
		}

		public DataSet GetPropertyClassList()
		{
			using (PropertyClass propertyClass = new PropertyClass(config))
			{
				return propertyClass.GetPropertyClassList();
			}
		}

		public DataSet GetPropertyClassComboList()
		{
			using (PropertyClass propertyClass = new PropertyClass(config))
			{
				return propertyClass.GetPropertyClassComboList();
			}
		}
	}
}
