using Micromind.Common.Data;
using System.Data;

namespace Micromind.Common.Interfaces
{
	public interface IPatientDocTypeSystem
	{
		bool CreatePatientDocType(PatientDocTypeData typeData);

		bool UpdatePatientDocType(PatientDocTypeData typeData);

		PatientDocTypeData GetPatientDocType();

		bool DeletePatientDocType(string ID);

		PatientDocTypeData GetPatientDocTypeByID(string id);

		DataSet GetPatientDocTypeByFields(params string[] columns);

		DataSet GetPatientDocTypeByFields(string[] ids, params string[] columns);

		DataSet GetPatientDocTypeByFields(string[] ids, bool isInactive, params string[] columns);

		DataSet GetPatientDocTypeList();

		DataSet GetPatientDocTypeComboList();
	}
}
