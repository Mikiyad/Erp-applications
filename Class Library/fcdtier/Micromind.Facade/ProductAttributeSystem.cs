using Micromind.Common.Data;
using Micromind.Common.Interfaces;
using Micromind.Data;
using System;
using System.Data;

namespace Micromind.Facade
{
	public sealed class ProductAttributeSystem : MarshalByRefObject, IProductAttributeSystem, IDisposable
	{
		private Config config;

		public ProductAttributeSystem(Config config)
		{
			this.config = config;
		}

		public void Dispose()
		{
		}

		public bool CreateProductAttribute(ProductAttributeData data)
		{
			return new ProductAttributes(config).InsertProductAttribute(data);
		}

		public bool UpdateProductAttribute(ProductAttributeData data)
		{
			return UpdateProductAttribute(data, checkConcurrency: false);
		}

		public bool UpdateProductAttribute(ProductAttributeData data, bool checkConcurrency)
		{
			return new ProductAttributes(config).UpdateProductAttribute(data);
		}

		public ProductAttributeData GetProductAttribute()
		{
			using (ProductAttributes productAttributes = new ProductAttributes(config))
			{
				return productAttributes.GetProductAttribute();
			}
		}

		public bool DeleteProductAttribute(string groupID)
		{
			using (ProductAttributes productAttributes = new ProductAttributes(config))
			{
				return productAttributes.DeleteProductAttribute(groupID);
			}
		}

		public ProductAttributeData GetProductAttributeByID(string id)
		{
			using (ProductAttributes productAttributes = new ProductAttributes(config))
			{
				return productAttributes.GetProductAttributeByID(id);
			}
		}

		public DataSet GetProductAttributeByFields(params string[] columns)
		{
			using (ProductAttributes productAttributes = new ProductAttributes(config))
			{
				return productAttributes.GetProductAttributeByFields(columns);
			}
		}

		public DataSet GetProductAttributeByFields(string[] ids, params string[] columns)
		{
			using (ProductAttributes productAttributes = new ProductAttributes(config))
			{
				return productAttributes.GetProductAttributeByFields(ids, columns);
			}
		}

		public DataSet GetProductAttributeByFields(string[] ids, bool isInactive, params string[] columns)
		{
			using (ProductAttributes productAttributes = new ProductAttributes(config))
			{
				return productAttributes.GetProductAttributeByFields(ids, isInactive, columns);
			}
		}

		public DataSet GetProductAttributeList()
		{
			using (ProductAttributes productAttributes = new ProductAttributes(config))
			{
				return productAttributes.GetProductAttributeList();
			}
		}

		public DataSet GetProductAttributeComboList()
		{
			using (ProductAttributes productAttributes = new ProductAttributes(config))
			{
				return productAttributes.GetProductAttributeComboList();
			}
		}
	}
}
