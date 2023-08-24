using Micromind.Common.Data;
using Micromind.Common.Interfaces;
using Micromind.Data;
using System;
using System.Data;

namespace Micromind.Facade
{
	public sealed class UserGroupSystem : MarshalByRefObject, IUserGroupSystem, IDisposable
	{
		private Config config;

		public UserGroupSystem(Config config)
		{
			this.config = config;
		}

		public void Dispose()
		{
		}

		public bool AssignGroupsToUser(string userID, UserGroupData data)
		{
			return new UserGroup(config).InsertUserGroupDetails(userID, data, byUser: true);
		}

		public bool AssignUsersToGroup(string groupID, UserGroupData data)
		{
			return new UserGroup(config).InsertUserGroupDetails(groupID, data, byUser: false);
		}

		public bool CreateUserGroup(UserGroupData data)
		{
			return new UserGroup(config).InsertUpdateUserGroup(data, isUpdate: false);
		}

		public bool UpdateUserGroup(UserGroupData data)
		{
			return new UserGroup(config).InsertUpdateUserGroup(data, isUpdate: true);
		}

		public UserGroupData GetUserGroup()
		{
			using (UserGroup userGroup = new UserGroup(config))
			{
				return userGroup.GetUserGroup();
			}
		}

		public bool DeleteUserGroup(string groupID)
		{
			using (UserGroup userGroup = new UserGroup(config))
			{
				return userGroup.DeleteUserGroup(groupID);
			}
		}

		public UserGroupData GetUserGroupByID(string id)
		{
			using (UserGroup userGroup = new UserGroup(config))
			{
				return userGroup.GetUserGroupByID(id);
			}
		}

		public DataSet GetUserGroupByFields(params string[] columns)
		{
			using (UserGroup userGroup = new UserGroup(config))
			{
				return userGroup.GetUserGroupByFields(columns);
			}
		}

		public DataSet GetUserGroupByFields(string[] ids, params string[] columns)
		{
			using (UserGroup userGroup = new UserGroup(config))
			{
				return userGroup.GetUserGroupByFields(ids, columns);
			}
		}

		public DataSet GetUserGroupByFields(string[] ids, bool isInactive, params string[] columns)
		{
			using (UserGroup userGroup = new UserGroup(config))
			{
				return userGroup.GetUserGroupByFields(ids, isInactive, columns);
			}
		}

		public DataSet GetUserGroupList()
		{
			using (UserGroup userGroup = new UserGroup(config))
			{
				return userGroup.GetUserGroupList();
			}
		}

		public DataSet GetUserGroupComboList()
		{
			using (UserGroup userGroup = new UserGroup(config))
			{
				return userGroup.GetUserGroupComboList();
			}
		}

		public DataSet GetGroupsByUser(string userID)
		{
			using (UserGroup userGroup = new UserGroup(config))
			{
				return userGroup.GetGroupsByUser(userID);
			}
		}

		public DataSet GetUsersByGroup(string groupID)
		{
			using (UserGroup userGroup = new UserGroup(config))
			{
				return userGroup.GetUsersByGroup(groupID);
			}
		}
	}
}
