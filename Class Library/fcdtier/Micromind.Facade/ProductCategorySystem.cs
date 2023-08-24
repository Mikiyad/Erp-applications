using Micromind.Common.Data;
using Micromind.Common.Interfaces;
using Micromind.Data;
using System;
using System.Data;

namespace Micromind.Facade
{
	public sealed class ProductCategorySystem : MarshalByRefObject, IProductCategorySystem, IDisposable
	{
		private Config config;

		public ProductCategorySystem(Config config)
		{
			this.config = config;
		}

		public void Dispose()
		{
		}

		public bool CreateProductCategory(ProductCategoryData data)
		{
			return new ProductCategories(config).InsertProductCategory(data);
		}

		public bool UpdateProductCategory(ProductCategoryData data)
		{
			return UpdateProductCategory(data, checkConcurrency: false);
		}

		public bool UpdateProductCategory(ProductCategoryData data, bool checkConcurrency)
		{
			return new ProductCategories(config).UpdateProductCategory(data);
		}

		public ProductCategoryData GetProductCategory()
		{
			using (ProductCategories productCategories = new ProductCategories(config))
			{
				return productCategories.GetProductCategory();
			}
		}

		public bool DeleteProductCategory(string groupID)
		{
			using (ProductCategories productCategories = new ProductCategories(config))
			{
				return productCategories.DeleteProductCategory(groupID);
			}
		}

		public ProductCategoryData GetProductCategoryByID(string id)
		{
			using (ProductCategories productCategories = new ProductCategories(config))
			{
				return productCategories.GetProductCategoryByID(id);
			}
		}

		public DataSet GetProductCategoryByFields(params string[] columns)
		{
			using (ProductCategories productCategories = new ProductCategories(config))
			{
				return productCategories.GetProductCategoryByFields(columns);
			}
		}

		public DataSet GetProductCategoryByFields(string[] ids, params string[] columns)
		{
			using (ProductCategories productCategories = new ProductCategories(config))
			{
				return productCategories.GetProductCategoryByFields(ids, columns);
			}
		}

		public DataSet GetProductCategoryByFields(string[] ids, bool isInactive, params string[] columns)
		{
			using (ProductCategories productCategories = new ProductCategories(config))
			{
				return productCategories.GetProductCategoryByFields(ids, isInactive, columns);
			}
		}

		public DataSet GetProductCategoryList()
		{
			using (ProductCategories productCategories = new ProductCategories(config))
			{
				return productCategories.GetProductCategoryList();
			}
		}

		public DataSet GetProductCategoryComboList()
		{
			using (ProductCategories productCategories = new ProductCategories(config))
			{
				return productCategories.GetProductCategoryComboList();
			}
		}
	}
}
