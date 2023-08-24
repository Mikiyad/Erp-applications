using Micromind.Common.Data;
using System.Data;

namespace Micromind.Common.Interfaces
{
	public interface IHolidayCalendarSystem
	{
		bool CreateHolidayCalendar(HolidayCalendarData expenseAdjustmentData, bool isUpdate);

		HolidayCalendarData GetHolidayCalendarByID(string sysDocID);

		DataSet GetHolidayCalendarToPrint(string CalendarID);

		bool DeleteHolidayCalendar(string CalendarID);

		DataSet GetHolidayComboList();

		DataSet GetList();
	}
}
