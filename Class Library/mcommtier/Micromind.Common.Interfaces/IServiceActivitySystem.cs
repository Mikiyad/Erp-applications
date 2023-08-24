using Micromind.Common.Data;
using System.Data;

namespace Micromind.Common.Interfaces
{
	public interface IServiceActivitySystem
	{
		bool CreateServiceActivity(ServiceActivityData jobTypeData);

		bool UpdateServiceActivity(ServiceActivityData jobTypeData);

		ServiceActivityData GetServiceActivity();

		bool DeleteServiceActivity(string ID);

		ServiceActivityData GetServiceActivityByID(string id);

		DataSet GetServiceActivityByFields(params string[] columns);

		DataSet GetServiceActivityByFields(string[] ids, params string[] columns);

		DataSet GetServiceActivityByFields(string[] ids, bool isInactive, params string[] columns);

		DataSet GetServiceActivityList();

		DataSet GetServiceActivityComboList();
	}
}
