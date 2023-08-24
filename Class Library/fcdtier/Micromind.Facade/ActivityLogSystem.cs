using Micromind.Common.Data;
using Micromind.Common.Interfaces;
using Micromind.Data;
using System;
using System.Data;

namespace Micromind.Facade
{
	public sealed class ActivityLogSystem : MarshalByRefObject, IActivityLogSystem, IDisposable
	{
		private Config config;

		public ActivityLogSystem(Config config)
		{
			this.config = config;
		}

		public void Dispose()
		{
		}

		public DataSet GetActivityLogsByFields(params string[] columns)
		{
			using (ActivityLogs activityLogs = new ActivityLogs(config))
			{
				return activityLogs.GetActivityLogsByFields(columns);
			}
		}

		public DataSet GetActivityLogsByFields(int[] activityLogID, params string[] columns)
		{
			using (ActivityLogs activityLogs = new ActivityLogs(config))
			{
				return activityLogs.GetActivityLogsByFields(activityLogID, columns);
			}
		}

		public DataSet GetActivityLogsByFields(ActivityTypes[] activityTypes, string[] machines, string[] users, DateTime from, DateTime to, params string[] columns)
		{
			using (ActivityLogs activityLogs = new ActivityLogs(config))
			{
				return activityLogs.GetActivityLogsByFields(activityTypes, machines, users, from, to, columns);
			}
		}

		public ActivityLogData GetActivityLogs()
		{
			using (ActivityLogs activityLogs = new ActivityLogs(config))
			{
				return activityLogs.GetActivityLogs();
			}
		}

		public bool DeleteActivityLog(int[] activityLogID)
		{
			using (ActivityLogs activityLogs = new ActivityLogs(config))
			{
				return activityLogs.DeleteActivityLog(activityLogID);
			}
		}

		public ActivityLogData GetActivityLogByID(int activityLogID)
		{
			using (ActivityLogs activityLogs = new ActivityLogs(config))
			{
				return activityLogs.GetActivityLogByID(activityLogID);
			}
		}

		public DataSet GetActivityLogList(DateTime from, DateTime to)
		{
			using (ActivityLogs activityLogs = new ActivityLogs(config))
			{
				return activityLogs.GetActivityLogList(from, to);
			}
		}

		public DataSet GetDocumentActivityLog(string entityID, string sysDocID)
		{
			using (ActivityLogs activityLogs = new ActivityLogs(config))
			{
				return activityLogs.GetDocumentActivityLog(entityID, sysDocID);
			}
		}

		public DataSet GetDocumentActivityLog(string entityID, string sysDocID, int comboType)
		{
			using (ActivityLogs activityLogs = new ActivityLogs(config))
			{
				return activityLogs.GetDocumentActivityLog(entityID, sysDocID, comboType);
			}
		}

		public DataSet GetDocumentVersionsList(int screenType, int screenID, string sysDocID, string docNumber)
		{
			using (ActivityLogs activityLogs = new ActivityLogs(config))
			{
				return activityLogs.GetDocumentVersionsList(screenType, screenID, sysDocID, docNumber);
			}
		}

		public DataSet GetDocumentVersionsByID(int versionID)
		{
			using (ActivityLogs activityLogs = new ActivityLogs(config))
			{
				return activityLogs.GetDocumentVersionsByID(versionID);
			}
		}
	}
}
