using Micromind.Common.Data;
using System;

namespace Micromind.Common.Interfaces
{
	public interface IUserManagerSystem
	{
		bool AddUser(string userID, string machineID, DateTime loginDate, string dbName, string appName);

		void RemoveUser(string loginName, string machineName);

		ActivityData GetUsers();

		int GetTotalUsers();
	}
}
