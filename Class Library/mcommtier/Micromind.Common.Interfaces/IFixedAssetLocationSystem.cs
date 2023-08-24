using Micromind.Common.Data;
using System.Data;

namespace Micromind.Common.Interfaces
{
	public interface IFixedAssetLocationSystem
	{
		bool CreateAssetLocation(FixedAssetLocationData assetLocationData);

		bool UpdateAssetLocation(FixedAssetLocationData assetLocationData);

		FixedAssetLocationData GetAssetLocation();

		bool DeleteAssetLocation(string ID);

		FixedAssetLocationData GetAssetLocationByID(string id);

		DataSet GetAssetLocationByFields(params string[] columns);

		DataSet GetAssetLocationByFields(string[] ids, params string[] columns);

		DataSet GetAssetLocationByFields(string[] ids, bool isInactive, params string[] columns);

		DataSet GetAssetLocationList();

		DataSet GetAssetLocationComboList();
	}
}
