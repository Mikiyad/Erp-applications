using Micromind.Common.Data;
using Micromind.Common.Interfaces;
using Micromind.Data;
using System;
using System.Data;

namespace Micromind.Facade
{
	public sealed class ProductModelSystem : MarshalByRefObject, IProductModelSystem, IDisposable
	{
		private Config config;

		public ProductModelSystem(Config config)
		{
			this.config = config;
		}

		public void Dispose()
		{
		}

		public bool CreateProductModel(ProductModelData data)
		{
			return new ProductModel(config).InsertProductModel(data);
		}

		public bool UpdateProductModel(ProductModelData data)
		{
			return UpdateProductModel(data, checkConcurrency: false);
		}

		public bool UpdateProductModel(ProductModelData data, bool checkConcurrency)
		{
			return new ProductModel(config).UpdateProductModel(data);
		}

		public ProductModelData GetProductModel()
		{
			using (ProductModel productModel = new ProductModel(config))
			{
				return productModel.GetProductModel();
			}
		}

		public bool DeleteProductModel(string groupID)
		{
			using (ProductModel productModel = new ProductModel(config))
			{
				return productModel.DeleteProductModel(groupID);
			}
		}

		public ProductModelData GetProductModelByID(string id)
		{
			using (ProductModel productModel = new ProductModel(config))
			{
				return productModel.GetProductModelByID(id);
			}
		}

		public DataSet GetProductModelByFields(params string[] columns)
		{
			using (ProductModel productModel = new ProductModel(config))
			{
				return productModel.GetModelByFields(columns);
			}
		}

		public DataSet GetProductModelByFields(string[] ids, params string[] columns)
		{
			using (ProductModel productModel = new ProductModel(config))
			{
				return productModel.GetModelByFields(ids, columns);
			}
		}

		public DataSet GetProductModelByFields(string[] ids, bool isInactive, params string[] columns)
		{
			using (ProductModel productModel = new ProductModel(config))
			{
				return productModel.GetModelByFields(ids, isInactive, columns);
			}
		}

		public DataSet GetProductModelList()
		{
			using (ProductModel productModel = new ProductModel(config))
			{
				return productModel.GetProductModelList();
			}
		}

		public DataSet GetProductModelComboList()
		{
			using (ProductModel productModel = new ProductModel(config))
			{
				return productModel.GetProductModelComboList();
			}
		}
	}
}
