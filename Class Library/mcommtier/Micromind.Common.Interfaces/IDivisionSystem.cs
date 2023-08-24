using Micromind.Common.Data;
using System.Data;

namespace Micromind.Common.Interfaces
{
	public interface IDivisionSystem
	{
		bool CreateDivision(DivisionData divisionData);

		bool UpdateDivision(DivisionData divisionData);

		DivisionData GetDivision();

		bool DeleteDivision(string ID);

		DivisionData GetDivisionByID(string id);

		DataSet GetDivisionByFields(params string[] columns);

		DataSet GetDivisionByFields(string[] ids, params string[] columns);

		DataSet GetDivisionByFields(string[] ids, bool isInactive, params string[] columns);

		DataSet GetDivisionList();

		DataSet GetDivisionComboList();
	}
}
