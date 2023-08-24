using Micromind.Common.Data;
using System.Data;

namespace Micromind.Common.Interfaces
{
	public interface IProductParentSystem
	{
		bool CreateProductParent(ProductParentData customerParentData);

		bool UpdateProductParent(ProductParentData customerParentData);

		ProductParentData GetProductParent();

		bool DeleteProductParent(string ID, bool deleteComponents);

		bool DeleteComponents(string[] productIDs);

		ProductParentData GetProductParentByID(string id);

		DataSet GetProductParentByFields(params string[] columns);

		DataSet GetProductParentByFields(string[] ids, params string[] columns);

		DataSet GetProductParentByFields(string[] ids, bool isInactive, params string[] columns);

		DataSet GetProductParentCatalogReport(string fromItem, string toItem, string fromCategory, string toCategory, bool isInactive, bool showZeroQuantity, string fromManufacturer, string toManufacturer, string fromStyle, string toStyle, string fromOrigin, string toOrigin);

		DataSet GetProductParentList();

		DataSet GetProductParentComboList();

		DataSet GetMatrixTable(string productParentID, bool showAllComponents);

		DataSet GetMatrixQuantityTable(string productParentID, bool showAllComponents);

		bool AddProductPhoto(string productParentID, byte[] image);

		bool RemoveProductPhoto(string productParentID);

		bool AddProductParentPhoto(string productParentID, byte[] image);

		byte[] GetProductThumbnailImage(string productParentID);

		bool RemoveProductFromMatrix(string matrixID, string productID);

		bool AddProductToMatrix(string matrixID, string productID, string attribute1, string attribute2, string attribute3);
	}
}
