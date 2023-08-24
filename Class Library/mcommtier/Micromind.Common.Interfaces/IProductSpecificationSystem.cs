using Micromind.Common.Data;
using System.Data;

namespace Micromind.Common.Interfaces
{
	public interface IProductSpecificationSystem
	{
		bool CreateProductSpecification(ProductSpecificationData productSpecificationData);

		bool UpdateProductSpecification(ProductSpecificationData productSpecificationData);

		ProductSpecificationData GetProductSpecification();

		bool DeleteProductSpecification(string ID);

		ProductSpecificationData GetProductSpecificationByID(string id);

		DataSet GetProductSpecificationByFields(params string[] columns);

		DataSet GetProductSpecificationByFields(string[] ids, params string[] columns);

		DataSet GetProductSpecificationByFields(string[] ids, bool isInactive, params string[] columns);

		DataSet GetProductSpecificationList();

		DataSet GetProductSpecificationComboList();
	}
}
