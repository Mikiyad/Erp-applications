using Micromind.Common.Data;
using Micromind.Common.Interfaces;
using Micromind.Data;
using System;
using System.Data;

namespace Micromind.Facade
{
	public sealed class PropertyDocTypeSystem : MarshalByRefObject, IPropertyDocTypeSystem, IDisposable
	{
		private Config config;

		public PropertyDocTypeSystem(Config config)
		{
			this.config = config;
		}

		public void Dispose()
		{
		}

		public bool CreatePropertyDocType(PropertyDocTypeData data)
		{
			return new PropertyDocType(config).InsertPropertyDocType(data);
		}

		public bool UpdatePropertyDocType(PropertyDocTypeData data)
		{
			return UpdatePropertyDocType(data, checkConcurrency: false);
		}

		public bool UpdatePropertyDocType(PropertyDocTypeData data, bool checkConcurrency)
		{
			return new PropertyDocType(config).UpdatePropertyDocType(data);
		}

		public PropertyDocTypeData GetPropertyDocType()
		{
			using (PropertyDocType propertyDocType = new PropertyDocType(config))
			{
				return propertyDocType.GetPropertyDocType();
			}
		}

		public bool DeletePropertyDocType(string groupID)
		{
			using (PropertyDocType propertyDocType = new PropertyDocType(config))
			{
				return propertyDocType.DeletePropertyDocType(groupID);
			}
		}

		public PropertyDocTypeData GetPropertyDocTypeByID(string id)
		{
			using (PropertyDocType propertyDocType = new PropertyDocType(config))
			{
				return propertyDocType.GetPropertyDocTypeByID(id);
			}
		}

		public DataSet GetPropertyDocTypeByFields(params string[] columns)
		{
			using (PropertyDocType propertyDocType = new PropertyDocType(config))
			{
				return propertyDocType.GetPropertyDocTypeByFields(columns);
			}
		}

		public DataSet GetPropertyDocTypeByFields(string[] ids, params string[] columns)
		{
			using (PropertyDocType propertyDocType = new PropertyDocType(config))
			{
				return propertyDocType.GetPropertyDocTypeByFields(ids, columns);
			}
		}

		public DataSet GetPropertyDocTypeByFields(string[] ids, bool isInactive, params string[] columns)
		{
			using (PropertyDocType propertyDocType = new PropertyDocType(config))
			{
				return propertyDocType.GetPropertyDocTypeByFields(ids, isInactive, columns);
			}
		}

		public DataSet GetPropertyDocTypeList()
		{
			using (PropertyDocType propertyDocType = new PropertyDocType(config))
			{
				return propertyDocType.GetPropertyDocTypeList();
			}
		}

		public DataSet GetPropertyDocTypeComboList()
		{
			using (PropertyDocType propertyDocType = new PropertyDocType(config))
			{
				return propertyDocType.GetPropertyDocTypeComboList();
			}
		}
	}
}
