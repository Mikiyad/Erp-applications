using Micromind.Common.Data;
using Micromind.Common.Interfaces;
using Micromind.Data;
using System;
using System.Data;

namespace Micromind.Facade
{
	public sealed class ReminderSystem : MarshalByRefObject, IReminderSystem, IDisposable
	{
		private Config config;

		public ReminderSystem(Config config)
		{
			this.config = config;
		}

		public void Dispose()
		{
		}

		public bool UpdateReminderSetting(ReminderData data)
		{
			return new Reminders(config).InsertUpdateSetting(data);
		}

		public ReminderData GetReminderSettingByUser(string userID)
		{
			return new Reminders(config).GetReminderSettingByUser(userID);
		}

		public DataSet GetReminderCount(string userID)
		{
			return new Reminders(config).GetReminderCount(userID);
		}

		public DataSet GetReminders(string userID)
		{
			return new Reminders(config).GetReminders(userID);
		}
	}
}
