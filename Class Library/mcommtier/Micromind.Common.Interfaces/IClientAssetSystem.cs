using Micromind.Common.Data;
using System.Data;

namespace Micromind.Common.Interfaces
{
	public interface IClientAssetSystem
	{
		bool CreateClientAsset(ClientAssetData clientAssetData);

		bool UpdateClientAsset(ClientAssetData clientAssetData);

		ClientAssetData GetClientAsset();

		bool DeleteClientAsset(string ID);

		ClientAssetData GetClientAssetByID(string id);

		DataSet GetJobTaskByFields(params string[] columns);

		DataSet GetJobTaskByFields(string[] ids, params string[] columns);

		DataSet GetJobTaskByFields(string[] ids, bool isInactive, params string[] columns);

		DataSet GetClientAssetList(bool isactive);

		DataSet GetClientAssetComboList();
	}
}
