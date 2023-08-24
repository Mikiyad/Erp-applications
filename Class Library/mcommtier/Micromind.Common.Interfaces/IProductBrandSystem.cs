using Micromind.Common.Data;
using System.Data;

namespace Micromind.Common.Interfaces
{
	public interface IProductBrandSystem
	{
		bool CreateProductBrand(ProductBrandData productBrandData);

		bool UpdateProductBrand(ProductBrandData productBrandData);

		ProductBrandData GetProductBrand();

		bool DeleteProductBrand(string ID);

		ProductBrandData GetProductBrandByID(string id);

		DataSet GetProductBrandByFields(params string[] columns);

		DataSet GetProductBrandByFields(string[] ids, params string[] columns);

		DataSet GetProductBrandByFields(string[] ids, bool isInactive, params string[] columns);

		DataSet GetProductBrandList();

		DataSet GetProductBrandComboList();
	}
}
