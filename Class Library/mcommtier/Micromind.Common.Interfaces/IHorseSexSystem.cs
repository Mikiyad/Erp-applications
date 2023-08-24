using Micromind.Common.Data;
using System.Data;

namespace Micromind.Common.Interfaces
{
	public interface IHorseSexSystem
	{
		bool CreateHorseSex(HorseSexData HorseSexData);

		bool UpdateHorseSex(HorseSexData HorseSexData);

		HorseSexData GetHorseSex();

		bool DeleteHorseSex(string ID);

		HorseSexData GetHorseSexByID(string id);

		DataSet GetHorseSexByFields(params string[] columns);

		DataSet GetHorseSexByFields(string[] ids, params string[] columns);

		DataSet GetHorseSexByFields(string[] ids, bool isInactive, params string[] columns);

		DataSet GetHorseSexList();

		DataSet GetHorseSexComboList();
	}
}
