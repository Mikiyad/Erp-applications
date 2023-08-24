using Micromind.Common.Data;
using System.Data;

namespace Micromind.Common.Interfaces
{
	public interface IContactSystem
	{
		bool CreateContact(ContactData contactData);

		bool UpdateContact(ContactData contactData);

		ContactData GetContact();

		bool DeleteContact(string ID);

		ContactData GetContactByID(string id);

		bool AddContactPhoto(string employeeID, byte[] image);

		bool RemoveContactPhoto(string contactID);

		byte[] GetContactThumbnailImage(string contactID);

		DataSet GetContactByFields(params string[] columns);

		DataSet GetContactByFields(string[] ids, params string[] columns);

		DataSet GetContactByFields(string[] ids, bool isInactive, params string[] columns);

		DataSet GetContactList();

		DataSet GetContactComboList();
	}
}
