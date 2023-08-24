using Micromind.Common.Data;
using System.Data;

namespace Micromind.Common.Interfaces
{
	public interface IPropertyDocTypeSystem
	{
		bool CreatePropertyDocType(PropertyDocTypeData typeData);

		bool UpdatePropertyDocType(PropertyDocTypeData typeData);

		PropertyDocTypeData GetPropertyDocType();

		bool DeletePropertyDocType(string ID);

		PropertyDocTypeData GetPropertyDocTypeByID(string id);

		DataSet GetPropertyDocTypeByFields(params string[] columns);

		DataSet GetPropertyDocTypeByFields(string[] ids, params string[] columns);

		DataSet GetPropertyDocTypeByFields(string[] ids, bool isInactive, params string[] columns);

		DataSet GetPropertyDocTypeList();

		DataSet GetPropertyDocTypeComboList();
	}
}
