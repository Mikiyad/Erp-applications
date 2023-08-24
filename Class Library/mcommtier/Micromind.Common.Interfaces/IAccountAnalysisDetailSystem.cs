using System.Data;

namespace Micromind.Common.Interfaces
{
	public interface IAccountAnalysisDetailSystem
	{
		bool CreateAccountAnalysisDetail(DataSet accountAnalysisData);

		bool UpdateAccountAnalysisDetail(DataSet accountAnalysisData);

		bool DeleteAccountAnalysis(string accountID);

		DataSet GetAccountAnalysisGroupsByAccountID(string accountID);
	}
}
