using Micromind.Common.Data;
using System.Data;

namespace Micromind.Common.Interfaces
{
	public interface IPOSShiftSystem
	{
		bool CreatePOSShift(POSShiftData posShiftData);

		bool UpdatePOSShift(POSShiftData posShiftData);

		POSShiftData GetPOSShift();

		bool DeletePOSShift(string ID);

		POSShiftData GetPOSShiftByID(string id);

		DataSet GetPOSShiftByFields(params string[] columns);

		DataSet GetPOSShiftByFields(string[] ids, params string[] columns);

		DataSet GetPOSShiftByFields(string[] ids, bool isInactive, params string[] columns);

		DataSet GetPOSShiftList();

		DataSet GetPOSShiftComboList();

		int GetCurrentOpenShiftID(string userID);

		bool ClosePOSShift(int batchID, int shiftID, string registerID, decimal closingCash);
	}
}
