using Micromind.Common.Data;
using Micromind.Common.Interfaces;
using Micromind.Data;
using System;
using System.Data;

namespace Micromind.Facade
{
	public sealed class DriverSystem : MarshalByRefObject, IDriverSystem, IDisposable
	{
		private Config config;

		public DriverSystem(Config config)
		{
			this.config = config;
		}

		public void Dispose()
		{
		}

		public bool CreateDriver(DriverData data)
		{
			return new Driver(config).InsertDriver(data);
		}

		public bool UpdateDriver(DriverData data)
		{
			return UpdateDriver(data, checkConcurrency: false);
		}

		public bool UpdateDriver(DriverData data, bool checkConcurrency)
		{
			return new Driver(config).UpdateDriver(data);
		}

		public DriverData GetDriver()
		{
			using (Driver driver = new Driver(config))
			{
				return driver.GetDriver();
			}
		}

		public bool DeleteDriver(string groupID)
		{
			using (Driver driver = new Driver(config))
			{
				return driver.DeleteDriver(groupID);
			}
		}

		public DriverData GetDriverByID(string id)
		{
			using (Driver driver = new Driver(config))
			{
				return driver.GetDriverByID(id);
			}
		}

		public DataSet GetDriverByFields(params string[] columns)
		{
			using (Driver driver = new Driver(config))
			{
				return driver.GetDriverByFields(columns);
			}
		}

		public DataSet GetDriverByFields(string[] ids, params string[] columns)
		{
			using (Driver driver = new Driver(config))
			{
				return driver.GetDriverByFields(ids, columns);
			}
		}

		public DataSet GetDriverByFields(string[] ids, bool isInactive, params string[] columns)
		{
			using (Driver driver = new Driver(config))
			{
				return driver.GetDriverByFields(ids, isInactive, columns);
			}
		}

		public DataSet GetDriverList()
		{
			using (Driver driver = new Driver(config))
			{
				return driver.GetDriverList();
			}
		}

		public DataSet GetDriverComboList()
		{
			using (Driver driver = new Driver(config))
			{
				return driver.GetDriverComboList();
			}
		}
	}
}
