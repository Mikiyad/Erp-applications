using Micromind.Common.Data;
using System.Data;

namespace Micromind.Common.Interfaces
{
	public interface IProductStyleSystem
	{
		bool CreateProductStyle(ProductStyleData productStyleData);

		bool UpdateProductStyle(ProductStyleData productStyleData);

		ProductStyleData GetProductStyle();

		bool DeleteProductStyle(string ID);

		ProductStyleData GetProductStyleByID(string id);

		DataSet GetProductStyleByFields(params string[] columns);

		DataSet GetProductStyleByFields(string[] ids, params string[] columns);

		DataSet GetProductStyleByFields(string[] ids, bool isInactive, params string[] columns);

		DataSet GetProductStyleList();

		DataSet GetProductStyleComboList();
	}
}
