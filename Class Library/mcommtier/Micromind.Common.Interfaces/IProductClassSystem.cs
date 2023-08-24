using Micromind.Common.Data;
using System.Data;

namespace Micromind.Common.Interfaces
{
	public interface IProductClassSystem
	{
		bool CreateProductClass(ProductClassData customerClassData);

		bool UpdateProductClass(ProductClassData customerClassData);

		ProductClassData GetProductClass();

		bool DeleteProductClass(string ID);

		ProductClassData GetProductClassByID(string id);

		DataSet GetProductClassByFields(params string[] columns);

		DataSet GetProductClassByFields(string[] ids, params string[] columns);

		DataSet GetProductClassByFields(string[] ids, bool isInactive, params string[] columns);

		DataSet GetProductClassList();

		DataSet GetProductClassComboList();
	}
}
