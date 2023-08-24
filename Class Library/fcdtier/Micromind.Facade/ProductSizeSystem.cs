using Micromind.Common.Data;
using Micromind.Common.Interfaces;
using Micromind.Data;
using System;
using System.Data;

namespace Micromind.Facade
{
	public sealed class ProductSizeSystem : MarshalByRefObject, IProductSizeSystem, IDisposable
	{
		private Config config;

		public ProductSizeSystem(Config config)
		{
			this.config = config;
		}

		public void Dispose()
		{
		}

		public bool CreateProductSize(ProductSizeData data)
		{
			return new ProductSizes(config).InsertProductSize(data);
		}

		public bool UpdateProductSize(ProductSizeData data)
		{
			return UpdateProductSize(data, checkConcurrency: false);
		}

		public bool UpdateProductSize(ProductSizeData data, bool checkConcurrency)
		{
			return new ProductSizes(config).UpdateProductSize(data);
		}

		public ProductSizeData GetProductSize()
		{
			using (ProductSizes productSizes = new ProductSizes(config))
			{
				return productSizes.GetProductSize();
			}
		}

		public bool DeleteProductSize(string groupID)
		{
			using (ProductSizes productSizes = new ProductSizes(config))
			{
				return productSizes.DeleteProductSize(groupID);
			}
		}

		public ProductSizeData GetProductSizeByID(string id)
		{
			using (ProductSizes productSizes = new ProductSizes(config))
			{
				return productSizes.GetProductSizeByID(id);
			}
		}

		public DataSet GetProductSizeByFields(params string[] columns)
		{
			using (ProductSizes productSizes = new ProductSizes(config))
			{
				return productSizes.GetProductSizeByFields(columns);
			}
		}

		public DataSet GetProductSizeByFields(string[] ids, params string[] columns)
		{
			using (ProductSizes productSizes = new ProductSizes(config))
			{
				return productSizes.GetProductSizeByFields(ids, columns);
			}
		}

		public DataSet GetProductSizeByFields(string[] ids, bool isInactive, params string[] columns)
		{
			using (ProductSizes productSizes = new ProductSizes(config))
			{
				return productSizes.GetProductSizeByFields(ids, isInactive, columns);
			}
		}

		public DataSet GetProductSizeList()
		{
			using (ProductSizes productSizes = new ProductSizes(config))
			{
				return productSizes.GetProductSizeList();
			}
		}

		public DataSet GetProductSizeComboList()
		{
			using (ProductSizes productSizes = new ProductSizes(config))
			{
				return productSizes.GetProductSizeComboList();
			}
		}
	}
}
