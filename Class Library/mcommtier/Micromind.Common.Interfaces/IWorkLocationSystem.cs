using Micromind.Common.Data;
using System;
using System.Data;

namespace Micromind.Common.Interfaces
{
	public interface IWorkLocationSystem
	{
		bool CreateWorkLocation(WorkLocationData workLocationData);

		bool UpdateWorkLocation(WorkLocationData workLocationData);

		WorkLocationData GetWorkLocation();

		bool DeleteWorkLocation(string ID);

		WorkLocationData GetWorkLocationByID(string id, bool isPOS);

		WorkLocationData GetWorkLocationByID(string id);

		DataSet GetWorkLocationByFields(params string[] columns);

		DataSet GetWorkLocationByFields(string[] ids, params string[] columns);

		DataSet GetWorkLocationByFields(string[] ids, bool isInactive, params string[] columns);

		DataSet GetWorkLocationList();

		DataSet GetWorkLocationComboList();

		DataSet GetSalesByWorkLocationSummaryReport(DateTime from, DateTime to, string fromWorkLocation, string toWorkLocation);

		DataSet GetSalesByWorkLocationDetailReport(DateTime from, DateTime to, string fromWorkLocation, string toWorkLocation);

		DataSet GetPurchaseByWorkLocationSummaryReport(DateTime from, DateTime to, string fromWorkLocation, string toWorkLocation);

		DataSet GetPurchaseByWorkLocationDetailReport(DateTime from, DateTime to, string fromWorkLocation, string toWorkLocation);
	}
}
