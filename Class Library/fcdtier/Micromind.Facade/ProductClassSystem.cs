using Micromind.Common.Data;
using Micromind.Common.Interfaces;
using Micromind.Data;
using System;
using System.Data;

namespace Micromind.Facade
{
	public sealed class ProductClassSystem : MarshalByRefObject, IProductClassSystem, IDisposable
	{
		private Config config;

		public ProductClassSystem(Config config)
		{
			this.config = config;
		}

		public void Dispose()
		{
		}

		public bool CreateProductClass(ProductClassData data)
		{
			return new ProductClass(config).InsertProductClass(data);
		}

		public bool UpdateProductClass(ProductClassData data)
		{
			return UpdateProductClass(data, checkConcurrency: false);
		}

		public bool UpdateProductClass(ProductClassData data, bool checkConcurrency)
		{
			return new ProductClass(config).UpdateProductClass(data);
		}

		public ProductClassData GetProductClass()
		{
			using (ProductClass productClass = new ProductClass(config))
			{
				return productClass.GetProductClass();
			}
		}

		public bool DeleteProductClass(string groupID)
		{
			using (ProductClass productClass = new ProductClass(config))
			{
				return productClass.DeleteProductClass(groupID);
			}
		}

		public ProductClassData GetProductClassByID(string id)
		{
			using (ProductClass productClass = new ProductClass(config))
			{
				return productClass.GetProductClassByID(id);
			}
		}

		public DataSet GetProductClassByFields(params string[] columns)
		{
			using (ProductClass productClass = new ProductClass(config))
			{
				return productClass.GetProductClassByFields(columns);
			}
		}

		public DataSet GetProductClassByFields(string[] ids, params string[] columns)
		{
			using (ProductClass productClass = new ProductClass(config))
			{
				return productClass.GetProductClassByFields(ids, columns);
			}
		}

		public DataSet GetProductClassByFields(string[] ids, bool isInactive, params string[] columns)
		{
			using (ProductClass productClass = new ProductClass(config))
			{
				return productClass.GetProductClassByFields(ids, isInactive, columns);
			}
		}

		public DataSet GetProductClassList()
		{
			using (ProductClass productClass = new ProductClass(config))
			{
				return productClass.GetProductClassList();
			}
		}

		public DataSet GetProductClassComboList()
		{
			using (ProductClass productClass = new ProductClass(config))
			{
				return productClass.GetProductClassComboList();
			}
		}
	}
}
