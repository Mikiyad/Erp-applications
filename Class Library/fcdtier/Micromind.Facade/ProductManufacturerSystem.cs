using Micromind.Common.Data;
using Micromind.Common.Interfaces;
using Micromind.Data;
using System;
using System.Data;

namespace Micromind.Facade
{
	public sealed class ProductManufacturerSystem : MarshalByRefObject, IProductManufacturerSystem, IDisposable
	{
		private Config config;

		public ProductManufacturerSystem(Config config)
		{
			this.config = config;
		}

		public void Dispose()
		{
		}

		public bool CreateProductManufacturer(ProductManufacturerData data)
		{
			return new ProductManufacturers(config).InsertProductManufacturer(data);
		}

		public bool UpdateProductManufacturer(ProductManufacturerData data)
		{
			return UpdateProductManufacturer(data, checkConcurrency: false);
		}

		public bool UpdateProductManufacturer(ProductManufacturerData data, bool checkConcurrency)
		{
			return new ProductManufacturers(config).UpdateProductManufacturer(data);
		}

		public ProductManufacturerData GetProductManufacturer()
		{
			using (ProductManufacturers productManufacturers = new ProductManufacturers(config))
			{
				return productManufacturers.GetProductManufacturer();
			}
		}

		public bool DeleteProductManufacturer(string groupID)
		{
			using (ProductManufacturers productManufacturers = new ProductManufacturers(config))
			{
				return productManufacturers.DeleteProductManufacturer(groupID);
			}
		}

		public ProductManufacturerData GetProductManufacturerByID(string id)
		{
			using (ProductManufacturers productManufacturers = new ProductManufacturers(config))
			{
				return productManufacturers.GetProductManufacturerByID(id);
			}
		}

		public DataSet GetProductManufacturerByFields(params string[] columns)
		{
			using (ProductManufacturers productManufacturers = new ProductManufacturers(config))
			{
				return productManufacturers.GetProductManufacturerByFields(columns);
			}
		}

		public DataSet GetProductManufacturerByFields(string[] ids, params string[] columns)
		{
			using (ProductManufacturers productManufacturers = new ProductManufacturers(config))
			{
				return productManufacturers.GetProductManufacturerByFields(ids, columns);
			}
		}

		public DataSet GetProductManufacturerByFields(string[] ids, bool isInactive, params string[] columns)
		{
			using (ProductManufacturers productManufacturers = new ProductManufacturers(config))
			{
				return productManufacturers.GetProductManufacturerByFields(ids, isInactive, columns);
			}
		}

		public DataSet GetProductManufacturerList()
		{
			using (ProductManufacturers productManufacturers = new ProductManufacturers(config))
			{
				return productManufacturers.GetProductManufacturerList();
			}
		}

		public DataSet GetProductManufacturerComboList()
		{
			using (ProductManufacturers productManufacturers = new ProductManufacturers(config))
			{
				return productManufacturers.GetProductManufacturerComboList();
			}
		}
	}
}
