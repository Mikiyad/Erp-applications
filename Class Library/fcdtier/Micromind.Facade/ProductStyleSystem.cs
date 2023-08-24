using Micromind.Common.Data;
using Micromind.Common.Interfaces;
using Micromind.Data;
using System;
using System.Data;

namespace Micromind.Facade
{
	public sealed class ProductStyleSystem : MarshalByRefObject, IProductStyleSystem, IDisposable
	{
		private Config config;

		public ProductStyleSystem(Config config)
		{
			this.config = config;
		}

		public void Dispose()
		{
		}

		public bool CreateProductStyle(ProductStyleData data)
		{
			return new ProductStyles(config).InsertProductStyle(data);
		}

		public bool UpdateProductStyle(ProductStyleData data)
		{
			return UpdateProductStyle(data, checkConcurrency: false);
		}

		public bool UpdateProductStyle(ProductStyleData data, bool checkConcurrency)
		{
			return new ProductStyles(config).UpdateProductStyle(data);
		}

		public ProductStyleData GetProductStyle()
		{
			using (ProductStyles productStyles = new ProductStyles(config))
			{
				return productStyles.GetProductStyle();
			}
		}

		public bool DeleteProductStyle(string groupID)
		{
			using (ProductStyles productStyles = new ProductStyles(config))
			{
				return productStyles.DeleteProductStyle(groupID);
			}
		}

		public ProductStyleData GetProductStyleByID(string id)
		{
			using (ProductStyles productStyles = new ProductStyles(config))
			{
				return productStyles.GetProductStyleByID(id);
			}
		}

		public DataSet GetProductStyleByFields(params string[] columns)
		{
			using (ProductStyles productStyles = new ProductStyles(config))
			{
				return productStyles.GetProductStyleByFields(columns);
			}
		}

		public DataSet GetProductStyleByFields(string[] ids, params string[] columns)
		{
			using (ProductStyles productStyles = new ProductStyles(config))
			{
				return productStyles.GetProductStyleByFields(ids, columns);
			}
		}

		public DataSet GetProductStyleByFields(string[] ids, bool isInactive, params string[] columns)
		{
			using (ProductStyles productStyles = new ProductStyles(config))
			{
				return productStyles.GetProductStyleByFields(ids, isInactive, columns);
			}
		}

		public DataSet GetProductStyleList()
		{
			using (ProductStyles productStyles = new ProductStyles(config))
			{
				return productStyles.GetProductStyleList();
			}
		}

		public DataSet GetProductStyleComboList()
		{
			using (ProductStyles productStyles = new ProductStyles(config))
			{
				return productStyles.GetProductStyleComboList();
			}
		}
	}
}
