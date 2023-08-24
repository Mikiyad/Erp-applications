using Micromind.Common.Data;
using System.Data;

namespace Micromind.Common.Interfaces
{
	public interface IPositionSystem
	{
		bool CreatePosition(PositionData positionData);

		bool UpdatePosition(PositionData positionData);

		PositionData GetPosition();

		bool DeletePosition(string ID);

		PositionData GetPositionByID(string id);

		DataSet GetPositionByFields(params string[] columns);

		DataSet GetPositionByFields(string[] ids, params string[] columns);

		DataSet GetPositionByFields(string[] ids, bool isInactive, params string[] columns);

		DataSet GetPositionList();

		DataSet GetPositionComboList();
	}
}
