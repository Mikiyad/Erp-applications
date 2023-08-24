using Micromind.Common.Data;
using System;
using System.Data;

namespace Micromind.Facade.Factories
{
	public abstract class UserManagerSystem : MarshalByRefObject
	{
		private static ActivityData userData;

		private static int count;

		private UserManagerSystem()
		{
			userData = new ActivityData();
		}

		~UserManagerSystem()
		{
			checked
			{
				count--;
			}
		}

		public static bool AddUser(string userID, string machineID, DateTime loginDate, string dbName, string appName)
		{
			if (IsUserExists(userID, machineID))
			{
				return true;
			}
			DataRow dataRow = userData.ActivityTable.NewRow();
			dataRow[ActivityData.USERID_FIELD] = userID;
			dataRow[ActivityData.MACHINEID_FIELD] = machineID;
			dataRow[ActivityData.LOGINDATE_FIELD] = loginDate;
			dataRow[ActivityData.DBNAME_FIELD] = dbName;
			dataRow[ActivityData.APPNAME_FIELD] = appName;
			userData.ActivityTable.Rows.Add(dataRow);
			checked
			{
				count++;
				return true;
			}
		}

		private static bool IsUserExists(string userID, string machineID)
		{
			foreach (DataRow row in userData.ActivityTable.Rows)
			{
				if (row[ActivityData.USERID_FIELD].ToString().ToLower() == userID.ToLower() && row[ActivityData.MACHINEID_FIELD].ToString().ToLower() == machineID.ToLower())
				{
					return true;
				}
			}
			return false;
		}

		public static void RemoveUser(string userID, string machineID)
		{
			foreach (DataRow row in userData.ActivityTable.Rows)
			{
				if (row[ActivityData.USERID_FIELD].ToString().ToLower() == userID.ToLower() && row[ActivityData.MACHINEID_FIELD].ToString().ToLower() == machineID.ToLower())
				{
					userData.ActivityTable.Rows.Remove(row);
				}
			}
		}

		public static ActivityData GetUsers()
		{
			return userData;
		}

		public static int GetTotalUsers()
		{
			return count;
		}
	}
}
