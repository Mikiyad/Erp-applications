using Micromind.Common.Data;
using System.Data;

namespace Micromind.Common.Interfaces
{
	public interface ILCSystem
	{
		int CreateLC(LCData lcData);

		int CreateLC(LCData lcData, string closingPassword);

		bool UpdateLC(LCData lcData);

		bool UpdateLC(LCData lcData, string closingPassword);

		bool UpdateLC(LCData lcData, bool checkConcurrency);

		bool UpdateLC(LCData lcData, string closingPassword, bool checkConcurrency);

		LCData GetLCs();

		bool DeleteLC(int lcID);

		bool DeleteLC(int lcID, string closingPassword);

		LCData GetLCsByFields(params string[] columns);

		LCData GetLCsByFields(int[] lcsID, params string[] columns);

		LCData GetLCsByFields(int[] lcsID, bool isInactive, params string[] columns);

		LCData GetLCByID(int lcID);

		bool ExistLC(string lcName);

		bool CreateUpdateLCBatch(DataSet listData, bool checkConcurrency);
	}
}
