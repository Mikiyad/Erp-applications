using Micromind.Common.Data;
using Micromind.Common.Interfaces;
using Micromind.Data;
using System;
using System.Data;

namespace Micromind.Facade
{
	public sealed class PropertyDocumentSystem : MarshalByRefObject, IPropertyDocumentSystem, IDisposable
	{
		private Config config;

		public PropertyDocumentSystem(Config config)
		{
			this.config = config;
		}

		public void Dispose()
		{
		}

		public bool CreatePropertyDocument(PropertyDocumentData data)
		{
			return new PropertyDocuments(config).InsertPropertyDocument(data);
		}

		public bool UpdatePropertyDocument(PropertyDocumentData data)
		{
			return UpdatePropertyDocument(data, checkConcurrency: false);
		}

		public bool UpdatePropertyDocument(PropertyDocumentData data, bool checkConcurrency)
		{
			return new PropertyDocuments(config).UpdatePropertyDocument(data);
		}

		public PropertyDocumentData GetPropertyDocument()
		{
			using (PropertyDocuments propertyDocuments = new PropertyDocuments(config))
			{
				return propertyDocuments.GetPropertyDocument();
			}
		}

		public bool DeletePropertyDocument(string groupID)
		{
			using (PropertyDocuments propertyDocuments = new PropertyDocuments(config))
			{
				return propertyDocuments.DeletePropertyDocument(groupID);
			}
		}

		public PropertyDocumentData GetPropertyDocumentByID(string id)
		{
			using (PropertyDocuments propertyDocuments = new PropertyDocuments(config))
			{
				return propertyDocuments.GetPropertyDocumentByID(id);
			}
		}

		public PropertyDocumentData GetPropertyDocumentsByPropertyID(string employeeID)
		{
			using (PropertyDocuments propertyDocuments = new PropertyDocuments(config))
			{
				return propertyDocuments.GetPropertyDocumentsByPropertyID(employeeID);
			}
		}

		public DataSet GetPropertyDocumentByFields(params string[] columns)
		{
			using (PropertyDocuments propertyDocuments = new PropertyDocuments(config))
			{
				return propertyDocuments.GetPropertyDocumentByFields(columns);
			}
		}

		public DataSet GetPropertyDocumentByFields(string[] ids, params string[] columns)
		{
			using (PropertyDocuments propertyDocuments = new PropertyDocuments(config))
			{
				return propertyDocuments.GetPropertyDocumentByFields(ids, columns);
			}
		}

		public DataSet GetPropertyDocumentByFields(string[] ids, bool isInactive, params string[] columns)
		{
			using (PropertyDocuments propertyDocuments = new PropertyDocuments(config))
			{
				return propertyDocuments.GetPropertyDocumentByFields(ids, isInactive, columns);
			}
		}

		public DataSet GetPropertyDocumentList()
		{
			using (PropertyDocuments propertyDocuments = new PropertyDocuments(config))
			{
				return propertyDocuments.GetPropertyDocumentList();
			}
		}

		public DataSet GetPropertyDocumentComboList()
		{
			using (PropertyDocuments propertyDocuments = new PropertyDocuments(config))
			{
				return propertyDocuments.GetPropertyDocumentComboList();
			}
		}
	}
}
