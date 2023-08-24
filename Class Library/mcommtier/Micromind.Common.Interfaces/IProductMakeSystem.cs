using Micromind.Common.Data;
using System.Data;

namespace Micromind.Common.Interfaces
{
	public interface IProductMakeSystem
	{
		bool CreateProductMake(ProductMakeData productMakeData);

		bool UpdateProductMake(ProductMakeData productMakeData);

		ProductMakeData GetProductMake();

		bool DeleteProductMake(string ID);

		ProductMakeData GetProductMakeByID(string id);

		DataSet GetProductMakeByFields(params string[] columns);

		DataSet GetProductMakeByFields(string[] ids, params string[] columns);

		DataSet GetProductMakeByFields(string[] ids, bool isInactive, params string[] columns);

		DataSet GetProductMakeList();

		DataSet GetProductMakeComboList();
	}
}
