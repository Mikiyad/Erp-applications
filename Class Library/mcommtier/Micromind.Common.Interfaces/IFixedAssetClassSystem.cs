using Micromind.Common.Data;
using System.Data;

namespace Micromind.Common.Interfaces
{
	public interface IFixedAssetClassSystem
	{
		bool CreateAssetClass(FixedAssetClassData assetClassData);

		bool UpdateAssetClass(FixedAssetClassData assetClassData);

		FixedAssetClassData GetAssetClass();

		bool DeleteAssetClass(string ID);

		FixedAssetClassData GetAssetClassByID(string id);

		DataSet GetAssetClassByFields(params string[] columns);

		DataSet GetAssetClassByFields(string[] ids, params string[] columns);

		DataSet GetAssetClassByFields(string[] ids, bool isInactive, params string[] columns);

		DataSet GetAssetClassList();

		DataSet GetAssetClassComboList();
	}
}
