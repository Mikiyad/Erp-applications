using Micromind.Common.Data;
using Micromind.Common.Interfaces;
using Micromind.Data;
using System;
using System.Data;

namespace Micromind.Facade
{
	public sealed class ProductSpecificationSystem : MarshalByRefObject, IProductSpecificationSystem, IDisposable
	{
		private Config config;

		public ProductSpecificationSystem(Config config)
		{
			this.config = config;
		}

		public void Dispose()
		{
		}

		public bool CreateProductSpecification(ProductSpecificationData data)
		{
			return new ProductSpecification(config).InsertProductSpecification(data);
		}

		public bool UpdateProductSpecification(ProductSpecificationData data)
		{
			return UpdateProductSpecification(data, checkConcurrency: false);
		}

		public bool UpdateProductSpecification(ProductSpecificationData data, bool checkConcurrency)
		{
			return new ProductSpecification(config).UpdateProductSpecification(data);
		}

		public ProductSpecificationData GetProductSpecification()
		{
			using (ProductSpecification productSpecification = new ProductSpecification(config))
			{
				return productSpecification.GetProductSpecification();
			}
		}

		public bool DeleteProductSpecification(string groupID)
		{
			using (ProductSpecification productSpecification = new ProductSpecification(config))
			{
				return productSpecification.DeleteProductSpecification(groupID);
			}
		}

		public ProductSpecificationData GetProductSpecificationByID(string id)
		{
			using (ProductSpecification productSpecification = new ProductSpecification(config))
			{
				return productSpecification.GetProductSpecificationByID(id);
			}
		}

		public DataSet GetProductSpecificationByFields(params string[] columns)
		{
			using (ProductSpecification productSpecification = new ProductSpecification(config))
			{
				return productSpecification.GetProductSpecificationByFields(columns);
			}
		}

		public DataSet GetProductSpecificationByFields(string[] ids, params string[] columns)
		{
			using (ProductSpecification productSpecification = new ProductSpecification(config))
			{
				return productSpecification.GetProductSpecificationByFields(ids, columns);
			}
		}

		public DataSet GetProductSpecificationByFields(string[] ids, bool isInactive, params string[] columns)
		{
			using (ProductSpecification productSpecification = new ProductSpecification(config))
			{
				return productSpecification.GetProductSpecificationByFields(ids, isInactive, columns);
			}
		}

		public DataSet GetProductSpecificationList()
		{
			using (ProductSpecification productSpecification = new ProductSpecification(config))
			{
				return productSpecification.GetProductSpecificationList();
			}
		}

		public DataSet GetProductSpecificationComboList()
		{
			using (ProductSpecification productSpecification = new ProductSpecification(config))
			{
				return productSpecification.GetProductSpecificationComboList();
			}
		}
	}
}
