using Micromind.Common.Data;
using Micromind.Common.Interfaces;
using Micromind.Data;
using System;
using System.Data;

namespace Micromind.Facade
{
	public sealed class HolidayCalendarSystem : MarshalByRefObject, IHolidayCalendarSystem, IDisposable
	{
		private Config config;

		public HolidayCalendarSystem(Config config)
		{
			this.config = config;
		}

		public void Dispose()
		{
		}

		public bool CreateHolidayCalendar(HolidayCalendarData data, bool isUpdate)
		{
			return new HolidayCalendar(config).InsertUpdateHolidayCalendar(data, isUpdate);
		}

		public HolidayCalendarData GetHolidayCalendarByID(string CalendarID)
		{
			return new HolidayCalendar(config).GetHolidayCalendarByID(CalendarID);
		}

		public bool DeleteHolidayCalendar(string voucherID)
		{
			return new HolidayCalendar(config).DeleteHolidayCalendar(voucherID);
		}

		public DataSet GetHolidayCalendarToPrint(string sysDocID)
		{
			return new HolidayCalendar(config).GetHolidayCalendarToPrint(sysDocID);
		}

		public DataSet GetHolidayComboList()
		{
			using (HolidayCalendar holidayCalendar = new HolidayCalendar(config))
			{
				return holidayCalendar.GetHolidayComboList();
			}
		}

		public DataSet GetList()
		{
			return new HolidayCalendar(config).GetHolidayCalendarList();
		}
	}
}
