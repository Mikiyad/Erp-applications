using Micromind.Common.Data;
using System.Data;

namespace Micromind.Common.Interfaces
{
	public interface IPropertyDocumentSystem
	{
		bool CreatePropertyDocument(PropertyDocumentData employeeDocumentData);

		bool UpdatePropertyDocument(PropertyDocumentData employeeDocumentData);

		PropertyDocumentData GetPropertyDocument();

		bool DeletePropertyDocument(string ID);

		PropertyDocumentData GetPropertyDocumentByID(string id);

		PropertyDocumentData GetPropertyDocumentsByPropertyID(string employeeID);

		DataSet GetPropertyDocumentByFields(params string[] columns);

		DataSet GetPropertyDocumentByFields(string[] ids, params string[] columns);

		DataSet GetPropertyDocumentByFields(string[] ids, bool isInactive, params string[] columns);

		DataSet GetPropertyDocumentList();

		DataSet GetPropertyDocumentComboList();
	}
}
