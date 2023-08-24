using Micromind.Common.Data;
using System.Data;

namespace Micromind.Common.Interfaces
{
	public interface IPropertyTenantDocTypeSystem
	{
		bool CreatePropertyTenantDocType(PropertyTenantDocTypeData typeData);

		bool UpdatePropertyTenantDocType(PropertyTenantDocTypeData typeData);

		PropertyTenantDocTypeData GetPropertyTenantDocType();

		bool DeletePropertyTenantDocType(string ID);

		PropertyTenantDocTypeData GetPropertyTenantDocTypeByID(string id);

		DataSet GetPropertyTenantDocTypeByFields(params string[] columns);

		DataSet GetPropertyTenantDocTypeByFields(string[] ids, params string[] columns);

		DataSet GetPropertyTenantDocTypeByFields(string[] ids, bool isInactive, params string[] columns);

		DataSet GetPropertyTenantDocTypeList();

		DataSet GetPropertyTenantDocTypeComboList();
	}
}
