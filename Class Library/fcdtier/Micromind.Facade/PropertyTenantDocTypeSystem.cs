using Micromind.Common.Data;
using Micromind.Common.Interfaces;
using Micromind.Data;
using System;
using System.Data;

namespace Micromind.Facade
{
	public sealed class PropertyTenantDocTypeSystem : MarshalByRefObject, IPropertyTenantDocTypeSystem, IDisposable
	{
		private Config config;

		public PropertyTenantDocTypeSystem(Config config)
		{
			this.config = config;
		}

		public void Dispose()
		{
		}

		public bool CreatePropertyTenantDocType(PropertyTenantDocTypeData data)
		{
			return new PropertyTenantDocType(config).InsertPropertyTenantDocType(data);
		}

		public bool UpdatePropertyTenantDocType(PropertyTenantDocTypeData data)
		{
			return UpdatePropertyTenantDocType(data, checkConcurrency: false);
		}

		public bool UpdatePropertyTenantDocType(PropertyTenantDocTypeData data, bool checkConcurrency)
		{
			return new PropertyTenantDocType(config).UpdatePropertyTenantDocType(data);
		}

		public PropertyTenantDocTypeData GetPropertyTenantDocType()
		{
			using (PropertyTenantDocType propertyTenantDocType = new PropertyTenantDocType(config))
			{
				return propertyTenantDocType.GetPropertyTenantDocType();
			}
		}

		public bool DeletePropertyTenantDocType(string groupID)
		{
			using (PropertyTenantDocType propertyTenantDocType = new PropertyTenantDocType(config))
			{
				return propertyTenantDocType.DeletePropertyTenantDocType(groupID);
			}
		}

		public PropertyTenantDocTypeData GetPropertyTenantDocTypeByID(string id)
		{
			using (PropertyTenantDocType propertyTenantDocType = new PropertyTenantDocType(config))
			{
				return propertyTenantDocType.GetPropertyTenantDocTypeByID(id);
			}
		}

		public DataSet GetPropertyTenantDocTypeByFields(params string[] columns)
		{
			using (PropertyTenantDocType propertyTenantDocType = new PropertyTenantDocType(config))
			{
				return propertyTenantDocType.GetPropertyTenantDocTypeByFields(columns);
			}
		}

		public DataSet GetPropertyTenantDocTypeByFields(string[] ids, params string[] columns)
		{
			using (PropertyTenantDocType propertyTenantDocType = new PropertyTenantDocType(config))
			{
				return propertyTenantDocType.GetPropertyTenantDocTypeByFields(ids, columns);
			}
		}

		public DataSet GetPropertyTenantDocTypeByFields(string[] ids, bool isInactive, params string[] columns)
		{
			using (PropertyTenantDocType propertyTenantDocType = new PropertyTenantDocType(config))
			{
				return propertyTenantDocType.GetPropertyTenantDocTypeByFields(ids, isInactive, columns);
			}
		}

		public DataSet GetPropertyTenantDocTypeList()
		{
			using (PropertyTenantDocType propertyTenantDocType = new PropertyTenantDocType(config))
			{
				return propertyTenantDocType.GetPropertyTenantDocTypeList();
			}
		}

		public DataSet GetPropertyTenantDocTypeComboList()
		{
			using (PropertyTenantDocType propertyTenantDocType = new PropertyTenantDocType(config))
			{
				return propertyTenantDocType.GetPropertyTenantDocTypeComboList();
			}
		}
	}
}
