using Micromind.Common.Data;
using System.Data;

namespace Micromind.Common.Interfaces
{
	public interface IPriceLevelSystem
	{
		bool CreatePriceLevel(string levelName);

		bool CreatePriceLevel(PriceLevelData priceLevelData);

		bool UpdatePriceLevel(PriceLevelData priceLevelData);

		bool CreateUpdatePriceLevelBatch(DataSet listData, bool checkConcurrency);

		PriceLevelData GetPriceLevelByID(string priceLevelID);

		string GetPriceLevelIDByName(string levelName);

		PriceLevelData GetPriceLevels();

		bool DeletePriceLevel(string priceLevelID);

		bool ExistPriceLevel(string levelName);

		DataSet GetPriceLevelsByFields(params string[] columns);

		DataSet GetPriceLevelsByFields(int[] levelID, params string[] columns);

		DataSet GetPriceLevelsByFields(bool isInactive, int[] levelID, params string[] columns);

		DataSet GetPriceLevelComboList();

		DataSet GetPriceLevelList();
	}
}
