using Micromind.Common.Data;
using Micromind.Common.Interfaces;
using Micromind.Data;
using System;
using System.Data;

namespace Micromind.Facade
{
	public sealed class UserSystem : MarshalByRefObject, IUserSystem, IDisposable
	{
		private Config config;

		public UserSystem(Config config)
		{
			this.config = config;
		}

		public void Dispose()
		{
		}

		public bool CreateUser(UserData data)
		{
			return new User(config).InsertUpdateUser(data, isUpdate: false);
		}

		public bool UpdateUser(UserData data)
		{
			return new User(config).InsertUpdateUser(data, isUpdate: true);
		}

		public UserData GetUser()
		{
			using (User user = new User(config))
			{
				return user.GetUser();
			}
		}

		public bool DeleteUser(string groupID)
		{
			using (User user = new User(config))
			{
				return user.DeleteUser(groupID);
			}
		}

		public UserData GetUserByID(string id)
		{
			using (User user = new User(config))
			{
				return user.GetUserByID(id);
			}
		}

		public DataSet GetUserByFields(params string[] columns)
		{
			using (User user = new User(config))
			{
				return user.GetUserByFields(columns);
			}
		}

		public DataSet GetUserByFields(string[] ids, params string[] columns)
		{
			using (User user = new User(config))
			{
				return user.GetUserByFields(ids, columns);
			}
		}

		public DataSet GetUserByFields(string[] ids, bool isInactive, params string[] columns)
		{
			using (User user = new User(config))
			{
				return user.GetUserByFields(ids, isInactive, columns);
			}
		}

		public DataSet GetUserList()
		{
			using (User user = new User(config))
			{
				return user.GetUserList();
			}
		}

		public DataSet GetUserComboList()
		{
			using (User user = new User(config))
			{
				return user.GetUserComboList();
			}
		}

		public bool ExistSQLLogin(string loginID)
		{
			using (User user = new User(config))
			{
				return user.ExistSQLLogin(loginID);
			}
		}

		public bool ChangePassword(string userID, string newPassword, string oldPassword)
		{
			using (User user = new User(config))
			{
				return user.ChangePassword(userID, newPassword, oldPassword);
			}
		}

		public string GetUserLocationByID(string userID)
		{
			using (User user = new User(config))
			{
				return user.GetUserLocationByID(userID);
			}
		}

		public string GetUserByCLPass(string pass)
		{
			using (User user = new User(config))
			{
				return user.GetUserByCLPass(pass);
			}
		}

		public DataSet GetCLUserList()
		{
			using (User user = new User(config))
			{
				return user.GetCLUserList();
			}
		}
	}
}
