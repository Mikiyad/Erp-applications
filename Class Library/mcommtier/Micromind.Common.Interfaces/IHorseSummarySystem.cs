using Micromind.Common.Data;
using System.Data;

namespace Micromind.Common.Interfaces
{
	public interface IHorseSummarySystem
	{
		bool CreateHorseSummary(HorseSummaryData HorseSummaryData);

		bool UpdateHorseSummary(HorseSummaryData HorseSummaryData);

		HorseSummaryData GetHorseSummary();

		bool RemoveHorsePhoto(string horseID);

		bool DeleteHorseSummary(string ID);

		HorseSummaryData GetHorseSummaryByID(string id);

		bool AddHorsePhoto(string horseID, byte[] image);

		byte[] GetHorseThumbnailImage(string horseID);

		DataSet GetHorseSummaryList(bool isactive);

		DataSet GetHorseSummaryComboList();

		DataSet GetHorseSummaryReport(string from, string to, string fromTrainer, string totrainer, string fromLocation, string toLocation, bool showInactive);
	}
}
