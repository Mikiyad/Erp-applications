using Micromind.Common.Data;
using Micromind.Common.Interfaces;
using Micromind.Data;
using System;
using System.Data;

namespace Micromind.Facade
{
	public sealed class DestinationSystem : MarshalByRefObject, IDestinationSystem, IDisposable
	{
		private Config config;

		public DestinationSystem(Config config)
		{
			this.config = config;
		}

		public void Dispose()
		{
		}

		public bool CreateDestination(DestinationData data)
		{
			return new Destination(config).InsertDestination(data);
		}

		public bool UpdateDestination(DestinationData data)
		{
			return UpdateDestination(data, checkConcurrency: false);
		}

		public bool UpdateDestination(DestinationData data, bool checkConcurrency)
		{
			return new Destination(config).UpdateDestination(data);
		}

		public DestinationData GetDestination()
		{
			using (Destination destination = new Destination(config))
			{
				return destination.GetDestination();
			}
		}

		public bool DeleteDestination(string groupID)
		{
			using (Destination destination = new Destination(config))
			{
				return destination.DeleteDestination(groupID);
			}
		}

		public DestinationData GetDestinationByID(string id)
		{
			using (Destination destination = new Destination(config))
			{
				return destination.GetDestinationByID(id);
			}
		}

		public DataSet GetDestinationByFields(params string[] columns)
		{
			using (Destination destination = new Destination(config))
			{
				return destination.GetDestinationByFields(columns);
			}
		}

		public DataSet GetDestinationByFields(string[] ids, params string[] columns)
		{
			using (Destination destination = new Destination(config))
			{
				return destination.GetDestinationByFields(ids, columns);
			}
		}

		public DataSet GetDestinationByFields(string[] ids, bool isInactive, params string[] columns)
		{
			using (Destination destination = new Destination(config))
			{
				return destination.GetDestinationByFields(ids, isInactive, columns);
			}
		}

		public DataSet GetDestinationList()
		{
			using (Destination destination = new Destination(config))
			{
				return destination.GetDestinationList();
			}
		}

		public DataSet GetDestinationComboList()
		{
			using (Destination destination = new Destination(config))
			{
				return destination.GetDestinationComboList();
			}
		}
	}
}
