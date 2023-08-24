using Micromind.Common.Data;
using Micromind.Common.Interfaces;
using Micromind.Data;
using System;
using System.Data;

namespace Micromind.Facade
{
	public sealed class ProductMakeSystem : MarshalByRefObject, IProductMakeSystem, IDisposable
	{
		private Config config;

		public ProductMakeSystem(Config config)
		{
			this.config = config;
		}

		public void Dispose()
		{
		}

		public bool CreateProductMake(ProductMakeData data)
		{
			return new ProductMake(config).InsertProductMake(data);
		}

		public bool UpdateProductMake(ProductMakeData data)
		{
			return UpdateProductMake(data, checkConcurrency: false);
		}

		public bool UpdateProductMake(ProductMakeData data, bool checkConcurrency)
		{
			return new ProductMake(config).UpdateProductMake(data);
		}

		public ProductMakeData GetProductMake()
		{
			using (ProductMake productMake = new ProductMake(config))
			{
				return productMake.GetProductMake();
			}
		}

		public bool DeleteProductMake(string groupID)
		{
			using (ProductMake productMake = new ProductMake(config))
			{
				return productMake.DeleteProductMake(groupID);
			}
		}

		public ProductMakeData GetProductMakeByID(string id)
		{
			using (ProductMake productMake = new ProductMake(config))
			{
				return productMake.GetProductMakeByID(id);
			}
		}

		public DataSet GetProductMakeByFields(params string[] columns)
		{
			using (ProductMake productMake = new ProductMake(config))
			{
				return productMake.GetMakeByFields(columns);
			}
		}

		public DataSet GetProductMakeByFields(string[] ids, params string[] columns)
		{
			using (ProductMake productMake = new ProductMake(config))
			{
				return productMake.GetMakeByFields(ids, columns);
			}
		}

		public DataSet GetProductMakeByFields(string[] ids, bool isInactive, params string[] columns)
		{
			using (ProductMake productMake = new ProductMake(config))
			{
				return productMake.GetMakeByFields(ids, isInactive, columns);
			}
		}

		public DataSet GetProductMakeList()
		{
			using (ProductMake productMake = new ProductMake(config))
			{
				return productMake.GetProductMakeList();
			}
		}

		public DataSet GetProductMakeComboList()
		{
			using (ProductMake productMake = new ProductMake(config))
			{
				return productMake.GetProductMakeComboList();
			}
		}
	}
}
