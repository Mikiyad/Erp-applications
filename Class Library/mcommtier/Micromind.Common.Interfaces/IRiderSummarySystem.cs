using Micromind.Common.Data;
using System.Data;

namespace Micromind.Common.Interfaces
{
	public interface IRiderSummarySystem
	{
		bool CreateRiderSummary(RiderSummaryData RiderSummaryData);

		bool UpdateRiderSummary(RiderSummaryData RiderSummaryData);

		RiderSummaryData GetRiderSummary();

		bool DeleteRiderSummary(string ID);

		RiderSummaryData GetRiderSummaryByID(string id);

		DataSet GetRiderSummaryList(bool isactive);

		DataSet GetRiderSummaryComboList();
	}
}
