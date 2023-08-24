using Micromind.Common.Data;
using System.Data;

namespace Micromind.Common.Interfaces
{
	public interface IReminderSystem
	{
		bool UpdateReminderSetting(ReminderData data);

		ReminderData GetReminderSettingByUser(string userID);

		DataSet GetReminderCount(string userID);

		DataSet GetReminders(string userID);
	}
}
