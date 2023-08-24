using Micromind.Common.Data;
using System.Data;

namespace Micromind.Common.Interfaces
{
	public interface IOverTimeSystem
	{
		bool CreateOverTime(OverTimeData overTimeData);

		bool UpdateOverTime(OverTimeData overTimeData);

		OverTimeData GetOverTime();

		bool DeleteOverTime(string ID);

		OverTimeData GetOverTimeByID(string id);

		DataSet GetOverTimeByFields(params string[] columns);

		DataSet GetOverTimeByFields(string[] ids, params string[] columns);

		DataSet GetOverTimeByFields(string[] ids, bool isInactive, params string[] columns);

		DataSet GetOverTimeList();

		DataSet GetOverTimeComboList();
	}
}
