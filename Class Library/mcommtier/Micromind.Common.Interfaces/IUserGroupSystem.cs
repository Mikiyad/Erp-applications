using Micromind.Common.Data;
using System.Data;

namespace Micromind.Common.Interfaces
{
	public interface IUserGroupSystem
	{
		bool CreateUserGroup(UserGroupData userGroupData);

		bool UpdateUserGroup(UserGroupData userGroupData);

		UserGroupData GetUserGroup();

		bool DeleteUserGroup(string ID);

		UserGroupData GetUserGroupByID(string id);

		DataSet GetUserGroupByFields(params string[] columns);

		DataSet GetUserGroupByFields(string[] ids, params string[] columns);

		DataSet GetUserGroupByFields(string[] ids, bool isInactive, params string[] columns);

		DataSet GetUserGroupList();

		DataSet GetUserGroupComboList();

		DataSet GetGroupsByUser(string userID);

		bool AssignGroupsToUser(string userID, UserGroupData data);

		bool AssignUsersToGroup(string groupID, UserGroupData data);

		DataSet GetUsersByGroup(string groupID);
	}
}
