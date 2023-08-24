using Micromind.Common.Data;
using System.Data;

namespace Micromind.Common.Interfaces
{
	public interface IChequebookSystem
	{
		bool CreateChequebook(ChequebookData chequebookData);

		bool UpdateChequebook(ChequebookData chequebookData);

		ChequebookData GetChequebook();

		bool DeleteChequebook(string ID);

		ChequebookData GetChequebookByID(string id);

		DataSet GetChequebookByFields(params string[] columns);

		DataSet GetChequebookByFields(string[] ids, params string[] columns);

		DataSet GetChequebookByFields(string[] ids, bool isInactive, params string[] columns);

		DataSet GetChequebookList();

		DataSet GetChequebookComboList();

		int ExistChequeNumber(string chequebookID, int from, int to);

		bool RegisterCheque(string chequebookID, int from, int to);

		int GetLastChequeNumber(string chequebookID);

		decimal GetChequebookBalance(string chequebookID, bool includeOD);

		int GetNextChequeNumber(string chequebookID);
	}
}
