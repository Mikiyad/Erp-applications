using Micromind.Common.Data;
using Micromind.Common.Interfaces;
using Micromind.Data;
using System;
using System.Data;

namespace Micromind.Facade
{
	public sealed class PropertyTenantDocumentSystem : MarshalByRefObject, IPropertyTenantDocumentSystem, IDisposable
	{
		private Config config;

		public PropertyTenantDocumentSystem(Config config)
		{
			this.config = config;
		}

		public void Dispose()
		{
		}

		public bool CreatePropertyTenantDocument(PropertyTenantDocumentData data)
		{
			return new PropertyTenantDocuments(config).InsertPropertyTenantDocument(data);
		}

		public bool UpdatePropertyTenantDocument(PropertyTenantDocumentData data)
		{
			return UpdatePropertyTenantDocument(data, checkConcurrency: false);
		}

		public bool UpdatePropertyTenantDocument(PropertyTenantDocumentData data, bool checkConcurrency)
		{
			return new PropertyTenantDocuments(config).UpdatePropertyTenantDocument(data);
		}

		public PropertyTenantDocumentData GetPropertyTenantDocument()
		{
			using (PropertyTenantDocuments propertyTenantDocuments = new PropertyTenantDocuments(config))
			{
				return propertyTenantDocuments.GetPropertyTenantDocument();
			}
		}

		public bool DeletePropertyTenantDocument(string groupID)
		{
			using (PropertyTenantDocuments propertyTenantDocuments = new PropertyTenantDocuments(config))
			{
				return propertyTenantDocuments.DeletePropertyTenantDocument(groupID);
			}
		}

		public PropertyTenantDocumentData GetPropertyTenantDocumentByID(string id)
		{
			using (PropertyTenantDocuments propertyTenantDocuments = new PropertyTenantDocuments(config))
			{
				return propertyTenantDocuments.GetPropertyTenantDocumentByID(id);
			}
		}

		public PropertyTenantDocumentData GetPropertyTenantDocumentsByTenantID(string employeeID)
		{
			using (PropertyTenantDocuments propertyTenantDocuments = new PropertyTenantDocuments(config))
			{
				return propertyTenantDocuments.GetPropertyTenantDocumentsByTenantID(employeeID);
			}
		}

		public DataSet GetPropertyTenantDocumentByFields(params string[] columns)
		{
			using (PropertyTenantDocuments propertyTenantDocuments = new PropertyTenantDocuments(config))
			{
				return propertyTenantDocuments.GetPropertyTenantDocumentByFields(columns);
			}
		}

		public DataSet GetPropertyTenantDocumentByFields(string[] ids, params string[] columns)
		{
			using (PropertyTenantDocuments propertyTenantDocuments = new PropertyTenantDocuments(config))
			{
				return propertyTenantDocuments.GetPropertyTenantDocumentByFields(ids, columns);
			}
		}

		public DataSet GetPropertyTenantDocumentByFields(string[] ids, bool isInactive, params string[] columns)
		{
			using (PropertyTenantDocuments propertyTenantDocuments = new PropertyTenantDocuments(config))
			{
				return propertyTenantDocuments.GetPropertyTenantDocumentByFields(ids, isInactive, columns);
			}
		}

		public DataSet GetPropertyTenantDocumentList()
		{
			using (PropertyTenantDocuments propertyTenantDocuments = new PropertyTenantDocuments(config))
			{
				return propertyTenantDocuments.GetPropertyTenantDocumentList();
			}
		}

		public DataSet GetPropertyTenantDocumentComboList()
		{
			using (PropertyTenantDocuments propertyTenantDocuments = new PropertyTenantDocuments(config))
			{
				return propertyTenantDocuments.GetPropertyTenantDocumentComboList();
			}
		}
	}
}
