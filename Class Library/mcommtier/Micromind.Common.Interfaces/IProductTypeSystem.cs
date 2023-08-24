using Micromind.Common.Data;
using System.Data;

namespace Micromind.Common.Interfaces
{
	public interface IProductTypeSystem
	{
		bool CreateProductType(ProductTypeData productTypeData);

		bool UpdateProductType(ProductTypeData productTypeData);

		ProductTypeData GetProductType();

		bool DeleteProductType(string ID);

		ProductTypeData GetProductTypeByID(string id);

		DataSet GetProductTypeByFields(params string[] columns);

		DataSet GetProductTypeByFields(string[] ids, params string[] columns);

		DataSet GetProductTypeByFields(string[] ids, bool isInactive, params string[] columns);

		DataSet GetProductTypeList();

		DataSet GetProductTypeComboList();
	}
}
