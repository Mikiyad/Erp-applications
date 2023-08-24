using Micromind.Common.Data;
using System.Data;

namespace Micromind.Common.Interfaces
{
	public interface IProductAttributeSystem
	{
		bool CreateProductAttribute(ProductAttributeData productAttributeData);

		bool UpdateProductAttribute(ProductAttributeData productAttributeData);

		ProductAttributeData GetProductAttribute();

		bool DeleteProductAttribute(string ID);

		ProductAttributeData GetProductAttributeByID(string id);

		DataSet GetProductAttributeByFields(params string[] columns);

		DataSet GetProductAttributeByFields(string[] ids, params string[] columns);

		DataSet GetProductAttributeByFields(string[] ids, bool isInactive, params string[] columns);

		DataSet GetProductAttributeList();

		DataSet GetProductAttributeComboList();
	}
}
