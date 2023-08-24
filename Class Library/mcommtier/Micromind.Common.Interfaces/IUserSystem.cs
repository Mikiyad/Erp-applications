using Micromind.Common.Data;
using System.Data;

namespace Micromind.Common.Interfaces
{
	public interface IUserSystem
	{
		bool CreateUser(UserData userData);

		bool UpdateUser(UserData userData);

		UserData GetUser();

		bool DeleteUser(string ID);

		UserData GetUserByID(string id);

		DataSet GetUserByFields(params string[] columns);

		DataSet GetUserByFields(string[] ids, params string[] columns);

		DataSet GetUserByFields(string[] ids, bool isInactive, params string[] columns);

		DataSet GetUserList();

		DataSet GetUserComboList();

		bool ExistSQLLogin(string loginID);

		bool ChangePassword(string userID, string newPassword, string oldPassword);

		string GetUserLocationByID(string userID);

		DataSet GetCLUserList();

		string GetUserByCLPass(string pass);
	}
}
