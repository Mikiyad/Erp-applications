using Micromind.Common.Data;
using System.Data;

namespace Micromind.Common.Interfaces
{
	public interface IPOSBatchSystem
	{
		bool CreatePOSBatch(POSBatchData posBatchData);

		bool UpdatePOSBatch(POSBatchData posBatchData);

		POSBatchData GetPOSBatch();

		bool DeletePOSBatch(string ID);

		POSBatchData GetPOSBatchByID(string id);

		DataSet GetPOSBatchByFields(params string[] columns);

		DataSet GetPOSBatchByFields(string[] ids, params string[] columns);

		DataSet GetPOSBatchByFields(string[] ids, bool isInactive, params string[] columns);

		DataSet GetPOSBatchList();

		DataSet GetPOSBatchComboList();

		int GetCurrentOpenBatchID(string locationID);

		bool ClosePOSBatch(int batchID, string locationID);
	}
}
