using Micromind.Common.Data;
using Micromind.Common.Interfaces;
using Micromind.Data;
using System;
using System.Data;

namespace Micromind.Facade
{
	public sealed class ProductTypeSystem : MarshalByRefObject, IProductTypeSystem, IDisposable
	{
		private Config config;

		public ProductTypeSystem(Config config)
		{
			this.config = config;
		}

		public void Dispose()
		{
		}

		public bool CreateProductType(ProductTypeData data)
		{
			return new ProductType(config).InsertProductType(data);
		}

		public bool UpdateProductType(ProductTypeData data)
		{
			return UpdateProductType(data, checkConcurrency: false);
		}

		public bool UpdateProductType(ProductTypeData data, bool checkConcurrency)
		{
			return new ProductType(config).UpdateProductType(data);
		}

		public ProductTypeData GetProductType()
		{
			using (ProductType productType = new ProductType(config))
			{
				return productType.GetProductType();
			}
		}

		public bool DeleteProductType(string groupID)
		{
			using (ProductType productType = new ProductType(config))
			{
				return productType.DeleteProductType(groupID);
			}
		}

		public ProductTypeData GetProductTypeByID(string id)
		{
			using (ProductType productType = new ProductType(config))
			{
				return productType.GetProductTypeByID(id);
			}
		}

		public DataSet GetProductTypeByFields(params string[] columns)
		{
			using (ProductType productType = new ProductType(config))
			{
				return productType.GetTypeByFields(columns);
			}
		}

		public DataSet GetProductTypeByFields(string[] ids, params string[] columns)
		{
			using (ProductType productType = new ProductType(config))
			{
				return productType.GetTypeByFields(ids, columns);
			}
		}

		public DataSet GetProductTypeByFields(string[] ids, bool isInactive, params string[] columns)
		{
			using (ProductType productType = new ProductType(config))
			{
				return productType.GetTypeByFields(ids, isInactive, columns);
			}
		}

		public DataSet GetProductTypeList()
		{
			using (ProductType productType = new ProductType(config))
			{
				return productType.GetProductTypeList();
			}
		}

		public DataSet GetProductTypeComboList()
		{
			using (ProductType productType = new ProductType(config))
			{
				return productType.GetProductTypeComboList();
			}
		}
	}
}
