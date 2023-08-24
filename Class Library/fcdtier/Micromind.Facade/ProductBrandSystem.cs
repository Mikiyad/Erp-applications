using Micromind.Common.Data;
using Micromind.Common.Interfaces;
using Micromind.Data;
using System;
using System.Data;

namespace Micromind.Facade
{
	public sealed class ProductBrandSystem : MarshalByRefObject, IProductBrandSystem, IDisposable
	{
		private Config config;

		public ProductBrandSystem(Config config)
		{
			this.config = config;
		}

		public void Dispose()
		{
		}

		public bool CreateProductBrand(ProductBrandData data)
		{
			return new ProductBrands(config).InsertProductBrand(data);
		}

		public bool UpdateProductBrand(ProductBrandData data)
		{
			return UpdateProductBrand(data, checkConcurrency: false);
		}

		public bool UpdateProductBrand(ProductBrandData data, bool checkConcurrency)
		{
			return new ProductBrands(config).UpdateProductBrand(data);
		}

		public ProductBrandData GetProductBrand()
		{
			using (ProductBrands productBrands = new ProductBrands(config))
			{
				return productBrands.GetProductBrand();
			}
		}

		public bool DeleteProductBrand(string groupID)
		{
			using (ProductBrands productBrands = new ProductBrands(config))
			{
				return productBrands.DeleteProductBrand(groupID);
			}
		}

		public ProductBrandData GetProductBrandByID(string id)
		{
			using (ProductBrands productBrands = new ProductBrands(config))
			{
				return productBrands.GetProductBrandByID(id);
			}
		}

		public DataSet GetProductBrandByFields(params string[] columns)
		{
			using (ProductBrands productBrands = new ProductBrands(config))
			{
				return productBrands.GetProductBrandByFields(columns);
			}
		}

		public DataSet GetProductBrandByFields(string[] ids, params string[] columns)
		{
			using (ProductBrands productBrands = new ProductBrands(config))
			{
				return productBrands.GetProductBrandByFields(ids, columns);
			}
		}

		public DataSet GetProductBrandByFields(string[] ids, bool isInactive, params string[] columns)
		{
			using (ProductBrands productBrands = new ProductBrands(config))
			{
				return productBrands.GetProductBrandByFields(ids, isInactive, columns);
			}
		}

		public DataSet GetProductBrandList()
		{
			using (ProductBrands productBrands = new ProductBrands(config))
			{
				return productBrands.GetProductBrandList();
			}
		}

		public DataSet GetProductBrandComboList()
		{
			using (ProductBrands productBrands = new ProductBrands(config))
			{
				return productBrands.GetProductBrandComboList();
			}
		}
	}
}
