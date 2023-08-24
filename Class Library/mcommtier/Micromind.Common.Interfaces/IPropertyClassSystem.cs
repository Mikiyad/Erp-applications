using Micromind.Common.Data;
using System.Data;

namespace Micromind.Common.Interfaces
{
	public interface IPropertyClassSystem
	{
		bool CreatePropertyClass(PropertyClassData areaData);

		bool UpdatePropertyClass(PropertyClassData areaData);

		PropertyClassData GetPropertyClass();

		bool DeletePropertyClass(string ID);

		PropertyClassData GetPropertyClassByID(string id);

		DataSet GetPropertyClassByFields(params string[] columns);

		DataSet GetPropertyClassByFields(string[] ids, params string[] columns);

		DataSet GetPropertyClassByFields(string[] ids, bool isInactive, params string[] columns);

		DataSet GetPropertyClassList();

		DataSet GetPropertyClassComboList();
	}
}
