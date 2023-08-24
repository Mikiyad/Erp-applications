using Micromind.Common.Data;
using System.Data;

namespace Micromind.Common.Interfaces
{
	public interface IHorseTypeSystem
	{
		bool CreateHorseType(HorseTypeData HorseTypeData);

		bool UpdateHorseType(HorseTypeData HorseTypeData);

		HorseTypeData GetHorseType();

		bool DeleteHorseType(string ID);

		HorseTypeData GetHorseTypeByID(string id);

		DataSet GetHorseTypeByFields(params string[] columns);

		DataSet GetHorseTypeByFields(string[] ids, params string[] columns);

		DataSet GetHorseTypeByFields(string[] ids, bool isInactive, params string[] columns);

		DataSet GetHorseTypeList();

		DataSet GetHorseTypeComboList();
	}
}
