using Micromind.Common.Data;
using System.Data;

namespace Micromind.Common.Interfaces
{
	public interface IBinSystem
	{
		bool CreateBin(BinData binData);

		bool UpdateBin(BinData binData);

		BinData GetBin();

		bool DeleteBin(string ID);

		BinData GetBinByID(string id);

		DataSet GetJobTaskByFields(params string[] columns);

		DataSet GetJobTaskByFields(string[] ids, params string[] columns);

		DataSet GetJobTaskByFields(string[] ids, bool isInactive, params string[] columns);

		DataSet GetBinList(bool isactive);

		DataSet GetBinComboList();
	}
}
