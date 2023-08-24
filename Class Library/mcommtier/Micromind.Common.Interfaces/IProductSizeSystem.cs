using Micromind.Common.Data;
using System.Data;

namespace Micromind.Common.Interfaces
{
	public interface IProductSizeSystem
	{
		bool CreateProductSize(ProductSizeData productSizeData);

		bool UpdateProductSize(ProductSizeData productSizeData);

		ProductSizeData GetProductSize();

		bool DeleteProductSize(string ID);

		ProductSizeData GetProductSizeByID(string id);

		DataSet GetProductSizeByFields(params string[] columns);

		DataSet GetProductSizeByFields(string[] ids, params string[] columns);

		DataSet GetProductSizeByFields(string[] ids, bool isInactive, params string[] columns);

		DataSet GetProductSizeList();

		DataSet GetProductSizeComboList();
	}
}
