using Micromind.Common.Data;
using System.Data;

namespace Micromind.Common.Interfaces
{
	public interface IProductModelSystem
	{
		bool CreateProductModel(ProductModelData productModelData);

		bool UpdateProductModel(ProductModelData productModelData);

		ProductModelData GetProductModel();

		bool DeleteProductModel(string ID);

		ProductModelData GetProductModelByID(string id);

		DataSet GetProductModelByFields(params string[] columns);

		DataSet GetProductModelByFields(string[] ids, params string[] columns);

		DataSet GetProductModelByFields(string[] ids, bool isInactive, params string[] columns);

		DataSet GetProductModelList();

		DataSet GetProductModelComboList();
	}
}
