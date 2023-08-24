using Micromind.Common.Data;
using System.Data;

namespace Micromind.Common.Interfaces
{
	public interface IBOMSystem
	{
		bool CreateBOM(BOMData bomData);

		bool UpdateBOM(BOMData bomData);

		BOMData GetBOM();

		bool DeleteBOM(string ID);

		BOMData GetBOMByID(string id);

		BOMData GetBOMItemsByID(string id);

		DataSet GetBOMByFields(params string[] columns);

		DataSet GetBOMByFields(string[] ids, params string[] columns);

		DataSet GetBOMByFields(string[] ids, bool isInactive, params string[] columns);

		DataSet GetBOMList(bool isInactive);

		DataSet GetBOMComboList();

		DataSet GetPackageComboList();

		DataSet GetPackageList(bool isInactive);

		BOMData GetPackageByID(string id);
	}
}
