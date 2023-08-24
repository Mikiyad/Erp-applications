using Micromind.Common.Data;
using System.Data;

namespace Micromind.Common.Interfaces
{
	public interface IPropertyTenantDocumentSystem
	{
		bool CreatePropertyTenantDocument(PropertyTenantDocumentData employeeDocumentData);

		bool UpdatePropertyTenantDocument(PropertyTenantDocumentData employeeDocumentData);

		PropertyTenantDocumentData GetPropertyTenantDocument();

		bool DeletePropertyTenantDocument(string ID);

		PropertyTenantDocumentData GetPropertyTenantDocumentByID(string id);

		PropertyTenantDocumentData GetPropertyTenantDocumentsByTenantID(string employeeID);

		DataSet GetPropertyTenantDocumentByFields(params string[] columns);

		DataSet GetPropertyTenantDocumentByFields(string[] ids, params string[] columns);

		DataSet GetPropertyTenantDocumentByFields(string[] ids, bool isInactive, params string[] columns);

		DataSet GetPropertyTenantDocumentList();

		DataSet GetPropertyTenantDocumentComboList();
	}
}
