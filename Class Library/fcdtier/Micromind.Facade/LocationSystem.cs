using Micromind.Common.Data;
using Micromind.Common.Interfaces;
using Micromind.Data;
using System;
using System.Data;

namespace Micromind.Facade
{
	public sealed class LocationSystem : MarshalByRefObject, ILocationSystem, IDisposable
	{
		private Config config;

		public LocationSystem(Config config)
		{
			this.config = config;
		}

		public void Dispose()
		{
		}

		public bool CreateLocation(LocationData data)
		{
			return new Location(config).InsertLocation(data);
		}

		public bool UpdateLocation(LocationData data)
		{
			return UpdateLocation(data, checkConcurrency: false);
		}

		public bool UpdateLocation(LocationData data, bool checkConcurrency)
		{
			return new Location(config).UpdateLocation(data);
		}

		public LocationData GetLocation()
		{
			using (Location location = new Location(config))
			{
				return location.GetLocation();
			}
		}

		public bool DeleteLocation(string groupID)
		{
			using (Location location = new Location(config))
			{
				return location.DeleteLocation(groupID);
			}
		}

		public LocationData GetLocationByID(string id)
		{
			using (Location location = new Location(config))
			{
				return location.GetLocationByID(id, isPOS: false);
			}
		}

		public LocationData GetLocationByID(string id, bool isPOS)
		{
			using (Location location = new Location(config))
			{
				return location.GetLocationByID(id, isPOS);
			}
		}

		public DataSet GetLocationByFields(params string[] columns)
		{
			using (Location location = new Location(config))
			{
				return location.GetLocationByFields(columns);
			}
		}

		public DataSet GetLocationByFields(string[] ids, params string[] columns)
		{
			using (Location location = new Location(config))
			{
				return location.GetLocationByFields(ids, columns);
			}
		}

		public DataSet GetLocationByFields(string[] ids, bool isInactive, params string[] columns)
		{
			using (Location location = new Location(config))
			{
				return location.GetLocationByFields(ids, isInactive, columns);
			}
		}

		public DataSet GetLocationList()
		{
			using (Location location = new Location(config))
			{
				return location.GetLocationList();
			}
		}

		public DataSet GetLocationComboList()
		{
			using (Location location = new Location(config))
			{
				return location.GetLocationComboList();
			}
		}

		public DataSet GetSalesByLocationSummaryReport(DateTime from, DateTime to, string fromLocation, string toLocation)
		{
			using (Location location = new Location(config))
			{
				return location.GetSalesByLocationSummaryReport(from, to, fromLocation, toLocation);
			}
		}

		public DataSet GetDailySalesAnalysisReport(DateTime from, DateTime to, string fromLocation, string toLocation)
		{
			using (Location location = new Location(config))
			{
				return location.GetDailySalesAnalysisReport(from, to, fromLocation, toLocation);
			}
		}

		public DataSet GetSalesByLocationDetailReport(DateTime from, DateTime to, string fromLocation, string toLocation)
		{
			using (Location location = new Location(config))
			{
				return location.GetSalesByLocationDetailReport(from, to, fromLocation, toLocation);
			}
		}

		public DataSet GetPurchaseByLocationSummaryReport(DateTime from, DateTime to, string fromLocation, string toLocation)
		{
			using (Location location = new Location(config))
			{
				return location.GetPurchaseByLocationSummaryReport(from, to, fromLocation, toLocation);
			}
		}

		public DataSet GetPurchaseByLocationDetailReport(DateTime from, DateTime to, string fromLocation, string toLocation)
		{
			using (Location location = new Location(config))
			{
				return location.GetPurchaseByLocationDetailReport(from, to, fromLocation, toLocation);
			}
		}

		public DataSet GetLocationsByUser(string userID)
		{
			using (Location location = new Location(config))
			{
				return location.GetLocationsByUser(userID);
			}
		}

		public bool AssignLocationsToUser(string userID, LocationData data)
		{
			return new Location(config).InsertUserLocationDetails(userID, data, byUser: true);
		}

		public DataSet GetUserLocationComboList(string userID)
		{
			using (Location location = new Location(config))
			{
				return location.GetUserLocationComboList(userID);
			}
		}
	}
}
