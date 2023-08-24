using Micromind.Common.Data;
using System.Data;

namespace Micromind.Common.Interfaces
{
	public interface IProductManufacturerSystem
	{
		bool CreateProductManufacturer(ProductManufacturerData productManufacturerData);

		bool UpdateProductManufacturer(ProductManufacturerData productManufacturerData);

		ProductManufacturerData GetProductManufacturer();

		bool DeleteProductManufacturer(string ID);

		ProductManufacturerData GetProductManufacturerByID(string id);

		DataSet GetProductManufacturerByFields(params string[] columns);

		DataSet GetProductManufacturerByFields(string[] ids, params string[] columns);

		DataSet GetProductManufacturerByFields(string[] ids, bool isInactive, params string[] columns);

		DataSet GetProductManufacturerList();

		DataSet GetProductManufacturerComboList();
	}
}
