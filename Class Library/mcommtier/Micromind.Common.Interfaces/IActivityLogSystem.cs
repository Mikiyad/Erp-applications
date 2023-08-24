using Micromind.Common.Data;
using System;
using System.Data;

namespace Micromind.Common.Interfaces
{
	public interface IActivityLogSystem
	{
		DataSet GetActivityLogsByFields(params string[] columns);

		DataSet GetActivityLogsByFields(int[] activityLogID, params string[] columns);

		DataSet GetActivityLogsByFields(ActivityTypes[] activityTypes, string[] machines, string[] users, DateTime from, DateTime to, params string[] columns);

		ActivityLogData GetActivityLogs();

		bool DeleteActivityLog(int[] activityLogID);

		ActivityLogData GetActivityLogByID(int activityLogID);

		DataSet GetActivityLogList(DateTime from, DateTime to);

		DataSet GetDocumentActivityLog(string entityID, string sysDocID);

		DataSet GetDocumentActivityLog(string entityID, string sysDocID, int comboType);

		DataSet GetDocumentVersionsList(int screenType, int screenID, string sysDocID, string docNumber);

		DataSet GetDocumentVersionsByID(int versionID);
	}
}
