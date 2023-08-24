using Micromind.Common.Data;
using System.Data;

namespace Micromind.Common.Interfaces
{
	public interface IProductCategorySystem
	{
		bool CreateProductCategory(ProductCategoryData productCategoryData);

		bool UpdateProductCategory(ProductCategoryData productCategoryData);

		ProductCategoryData GetProductCategory();

		bool DeleteProductCategory(string ID);

		ProductCategoryData GetProductCategoryByID(string id);

		DataSet GetProductCategoryByFields(params string[] columns);

		DataSet GetProductCategoryByFields(string[] ids, params string[] columns);

		DataSet GetProductCategoryByFields(string[] ids, bool isInactive, params string[] columns);

		DataSet GetProductCategoryList();

		DataSet GetProductCategoryComboList();
	}
}
