using Micromind.Common.Data;
using Micromind.Common.Interfaces;
using Micromind.Data;
using System;
using System.Data;

namespace Micromind.Facade
{
	public sealed class PropertyCategorySystem : MarshalByRefObject, IPropertyCategorySystem, IDisposable
	{
		private Config config;

		public PropertyCategorySystem(Config config)
		{
			this.config = config;
		}

		public void Dispose()
		{
		}

		public bool CreatePropertyCategory(PropertyCategoryData data)
		{
			return new PropertyCategory(config).InsertPropertyCategory(data);
		}

		public bool UpdatePropertyCategory(PropertyCategoryData data)
		{
			return UpdatePropertyCategory(data, checkConcurrency: false);
		}

		public bool UpdatePropertyCategory(PropertyCategoryData data, bool checkConcurrency)
		{
			return new PropertyCategory(config).UpdatePropertyCategory(data);
		}

		public PropertyCategoryData GetPropertyCategory()
		{
			using (PropertyCategory propertyCategory = new PropertyCategory(config))
			{
				return propertyCategory.GetPropertyCategory();
			}
		}

		public bool DeletePropertyCategory(string categoryID)
		{
			using (PropertyCategory propertyCategory = new PropertyCategory(config))
			{
				return propertyCategory.DeletePropertyCategory(categoryID);
			}
		}

		public PropertyCategoryData GetPropertyCategoryByID(string id)
		{
			using (PropertyCategory propertyCategory = new PropertyCategory(config))
			{
				return propertyCategory.GetPropertyCategoryByID(id);
			}
		}

		public DataSet GetPropertyCategoryByFields(params string[] columns)
		{
			using (PropertyCategory propertyCategory = new PropertyCategory(config))
			{
				return propertyCategory.GetPropertyCategoryByFields(columns);
			}
		}

		public DataSet GetPropertyCategoryByFields(string[] ids, params string[] columns)
		{
			using (PropertyCategory propertyCategory = new PropertyCategory(config))
			{
				return propertyCategory.GetPropertyCategoryByFields(ids, columns);
			}
		}

		public DataSet GetPropertyCategoryByFields(string[] ids, bool isInactive, params string[] columns)
		{
			using (PropertyCategory propertyCategory = new PropertyCategory(config))
			{
				return propertyCategory.GetPropertyCategoryByFields(ids, isInactive, columns);
			}
		}

		public DataSet GetPropertyCategoryList()
		{
			using (PropertyCategory propertyCategory = new PropertyCategory(config))
			{
				return propertyCategory.GetPropertyCategoryList();
			}
		}

		public DataSet GetPropertyAssignedCategorysList(string entityID, EntityTypesEnum entityType)
		{
			using (PropertyCategory propertyCategory = new PropertyCategory(config))
			{
				return propertyCategory.GetPropertyAssignedCategorysList(entityID, entityType);
			}
		}

		public DataSet GetPropertyCategoryComboList()
		{
			using (PropertyCategory propertyCategory = new PropertyCategory(config))
			{
				return propertyCategory.GetPropertyCategoryComboList();
			}
		}

		public bool InsertPropertyCategoryAssignment(PropertyCategoryData data, string customerID)
		{
			using (PropertyCategory propertyCategory = new PropertyCategory(config))
			{
				return propertyCategory.InsertPropertyCategoryAssignment(data, customerID);
			}
		}
	}
}
