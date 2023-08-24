using Micromind.Common.Data;
using System.Data;

namespace Micromind.Common.Interfaces
{
	public interface IFixedAssetGroupSystem
	{
		bool CreateAssetGroup(FixedAssetGroupData assetGroupData);

		bool UpdateAssetGroup(FixedAssetGroupData assetGroupData);

		FixedAssetGroupData GetAssetGroup();

		bool DeleteAssetGroup(string ID);

		FixedAssetGroupData GetAssetGroupByID(string id);

		DataSet GetAssetGroupByFields(params string[] columns);

		DataSet GetAssetGroupByFields(string[] ids, params string[] columns);

		DataSet GetAssetGroupByFields(string[] ids, bool isInactive, params string[] columns);

		DataSet GetAssetGroupList();

		DataSet GetAssetGroupComboList();
	}
}
