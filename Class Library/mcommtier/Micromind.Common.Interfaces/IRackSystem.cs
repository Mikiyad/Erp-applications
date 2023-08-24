using Micromind.Common.Data;
using System.Data;

namespace Micromind.Common.Interfaces
{
	public interface IRackSystem
	{
		bool CreateRack(RackData binData);

		bool UpdateRack(RackData binData);

		RackData GetRack();

		bool DeleteRack(string ID);

		RackData GetRackByID(string id);

		DataSet GetJobTaskByFields(params string[] columns);

		DataSet GetJobTaskByFields(string[] ids, params string[] columns);

		DataSet GetJobTaskByFields(string[] ids, bool isInactive, params string[] columns);

		DataSet GetRackList(bool isactive);

		DataSet GetRackComboList();
	}
}
