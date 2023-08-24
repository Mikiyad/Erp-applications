using Micromind.Common.Data;
using Micromind.Common.Interfaces;
using Micromind.Data;
using System;
using System.Data;

namespace Micromind.Facade
{
	public sealed class WorkLocationSystem : MarshalByRefObject, IWorkLocationSystem, IDisposable
	{
		private Config config;

		public WorkLocationSystem(Config config)
		{
			this.config = config;
		}

		public void Dispose()
		{
		}

		public bool CreateWorkLocation(WorkLocationData data)
		{
			return new WorkLocation(config).InsertWorkLocation(data);
		}

		public bool UpdateWorkLocation(WorkLocationData data)
		{
			return UpdateWorkLocation(data, checkConcurrency: false);
		}

		public bool UpdateWorkLocation(WorkLocationData data, bool checkConcurrency)
		{
			return new WorkLocation(config).UpdateWorkLocation(data);
		}

		public WorkLocationData GetWorkLocation()
		{
			using (WorkLocation workLocation = new WorkLocation(config))
			{
				return workLocation.GetWorkLocation();
			}
		}

		public bool DeleteWorkLocation(string groupID)
		{
			using (WorkLocation workLocation = new WorkLocation(config))
			{
				return workLocation.DeleteWorkLocation(groupID);
			}
		}

		public WorkLocationData GetWorkLocationByID(string id)
		{
			using (WorkLocation workLocation = new WorkLocation(config))
			{
				return workLocation.GetWorkLocationByID(id, isPOS: false);
			}
		}

		public WorkLocationData GetWorkLocationByID(string id, bool isPOS)
		{
			using (WorkLocation workLocation = new WorkLocation(config))
			{
				return workLocation.GetWorkLocationByID(id, isPOS);
			}
		}

		public DataSet GetWorkLocationByFields(params string[] columns)
		{
			using (WorkLocation workLocation = new WorkLocation(config))
			{
				return workLocation.GetWorkLocationByFields(columns);
			}
		}

		public DataSet GetWorkLocationByFields(string[] ids, params string[] columns)
		{
			using (WorkLocation workLocation = new WorkLocation(config))
			{
				return workLocation.GetWorkLocationByFields(ids, columns);
			}
		}

		public DataSet GetWorkLocationByFields(string[] ids, bool isInactive, params string[] columns)
		{
			using (WorkLocation workLocation = new WorkLocation(config))
			{
				return workLocation.GetWorkLocationByFields(ids, isInactive, columns);
			}
		}

		public DataSet GetWorkLocationList()
		{
			using (WorkLocation workLocation = new WorkLocation(config))
			{
				return workLocation.GetWorkLocationList();
			}
		}

		public DataSet GetWorkLocationComboList()
		{
			using (WorkLocation workLocation = new WorkLocation(config))
			{
				return workLocation.GetWorkLocationComboList();
			}
		}

		public DataSet GetSalesByWorkLocationSummaryReport(DateTime from, DateTime to, string fromWorkLocation, string toWorkLocation)
		{
			using (WorkLocation workLocation = new WorkLocation(config))
			{
				return workLocation.GetSalesByWorkLocationSummaryReport(from, to, fromWorkLocation, toWorkLocation);
			}
		}

		public DataSet GetSalesByWorkLocationDetailReport(DateTime from, DateTime to, string fromWorkLocation, string toWorkLocation)
		{
			using (WorkLocation workLocation = new WorkLocation(config))
			{
				return workLocation.GetSalesByWorkLocationDetailReport(from, to, fromWorkLocation, toWorkLocation);
			}
		}

		public DataSet GetPurchaseByWorkLocationSummaryReport(DateTime from, DateTime to, string fromWorkLocation, string toWorkLocation)
		{
			using (WorkLocation workLocation = new WorkLocation(config))
			{
				return workLocation.GetPurchaseByWorkLocationSummaryReport(from, to, fromWorkLocation, toWorkLocation);
			}
		}

		public DataSet GetPurchaseByWorkLocationDetailReport(DateTime from, DateTime to, string fromWorkLocation, string toWorkLocation)
		{
			using (WorkLocation workLocation = new WorkLocation(config))
			{
				return workLocation.GetPurchaseByWorkLocationDetailReport(from, to, fromWorkLocation, toWorkLocation);
			}
		}
	}
}
