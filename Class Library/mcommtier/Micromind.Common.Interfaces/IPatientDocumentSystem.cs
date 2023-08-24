using Micromind.Common.Data;
using System.Data;

namespace Micromind.Common.Interfaces
{
	public interface IPatientDocumentSystem
	{
		bool CreatePatientDocument(PatientDocumentData PatientDocumentData);

		bool UpdatePatientDocument(PatientDocumentData PatientDocumentData);

		PatientDocumentData GetPatientDocument();

		bool DeletePatientDocument(string ID);

		PatientDocumentData GetPatientDocumentByID(string id);

		PatientDocumentData GetPatientDocumentsByPatientID(string PatientID);

		DataSet GetPatientDocumentByFields(params string[] columns);

		DataSet GetPatientDocumentByFields(string[] ids, params string[] columns);

		DataSet GetPatientDocumentByFields(string[] ids, bool isInactive, params string[] columns);

		DataSet GetPatientDocumentList();

		DataSet GetPatientDocumentComboList();
	}
}
