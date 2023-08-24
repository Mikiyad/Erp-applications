using Micromind.Common.Data;
using System;
using System.Data;

namespace Micromind.Common.Interfaces
{
	public interface ILocationSystem
	{
		bool CreateLocation(LocationData locationData);

		bool UpdateLocation(LocationData locationData);

		LocationData GetLocation();

		bool DeleteLocation(string ID);

		LocationData GetLocationByID(string id, bool isPOS);

		LocationData GetLocationByID(string id);

		DataSet GetLocationByFields(params string[] columns);

		DataSet GetLocationByFields(string[] ids, params string[] columns);

		DataSet GetLocationByFields(string[] ids, bool isInactive, params string[] columns);

		DataSet GetLocationList();

		DataSet GetLocationComboList();

		DataSet GetSalesByLocationSummaryReport(DateTime from, DateTime to, string fromLocation, string toLocation);

		DataSet GetDailySalesAnalysisReport(DateTime from, DateTime to, string fromLocation, string toLocation);

		DataSet GetSalesByLocationDetailReport(DateTime from, DateTime to, string fromLocation, string toLocation);

		DataSet GetPurchaseByLocationSummaryReport(DateTime from, DateTime to, string fromLocation, string toLocation);

		DataSet GetPurchaseByLocationDetailReport(DateTime from, DateTime to, string fromLocation, string toLocation);

		DataSet GetLocationsByUser(string userID);

		bool AssignLocationsToUser(string userID, LocationData data);

		DataSet GetUserLocationComboList(string userID);
	}
}
