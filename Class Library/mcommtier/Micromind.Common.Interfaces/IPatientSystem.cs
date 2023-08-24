using Micromind.Common.Data;
using System.Data;

namespace Micromind.Common.Interfaces
{
	public interface IPatientSystem
	{
		bool CreateCustomer(PatientData customerData);

		bool UpdateCustomer(PatientData customerData);

		PatientData GetCustomer();

		bool DeleteCustomer(string ID);

		PatientData GetCustomerByID(string id);

		DataSet GetCustomerList(bool inactive);

		DataSet GetCustomerComboList();

		DataSet GetCustomerComboList(string id);

		string GetCustomerAddressPrintFormat(string customerID, string addressID);

		DataSet GetCustomerLeadsComboList();

		DataSet GetCustomerList();

		bool AddPatientPhoto(string patientID, byte[] image);

		bool RemovePatientPhoto(string patientID);

		byte[] GetPatientThumbnailImage(string patientID);

		DataSet GetCustomerDataToPrint(string fromcustomerID, string toCustomerID);
	}
}
