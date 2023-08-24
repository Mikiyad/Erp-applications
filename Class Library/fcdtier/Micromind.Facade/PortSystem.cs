using Micromind.Common.Data;
using Micromind.Common.Interfaces;
using Micromind.Data;
using System;
using System.Data;

namespace Micromind.Facade
{
	public sealed class PortSystem : MarshalByRefObject, IPortSystem, IDisposable
	{
		private Config config;

		public PortSystem(Config config)
		{
			this.config = config;
		}

		public void Dispose()
		{
		}

		public bool CreatePort(PortData data)
		{
			return new Port(config).InsertPort(data);
		}

		public bool UpdatePort(PortData data)
		{
			return UpdatePort(data, checkConcurrency: false);
		}

		public bool UpdatePort(PortData data, bool checkConcurrency)
		{
			return new Port(config).UpdatePort(data);
		}

		public PortData GetPort()
		{
			using (Port port = new Port(config))
			{
				return port.GetPort();
			}
		}

		public bool DeletePort(string groupID)
		{
			using (Port port = new Port(config))
			{
				return port.DeletePort(groupID);
			}
		}

		public PortData GetPortByID(string id)
		{
			using (Port port = new Port(config))
			{
				return port.GetPortByID(id);
			}
		}

		public DataSet GetPortByFields(params string[] columns)
		{
			using (Port port = new Port(config))
			{
				return port.GetPortByFields(columns);
			}
		}

		public DataSet GetPortByFields(string[] ids, params string[] columns)
		{
			using (Port port = new Port(config))
			{
				return port.GetPortByFields(ids, columns);
			}
		}

		public DataSet GetPortByFields(string[] ids, bool isInactive, params string[] columns)
		{
			using (Port port = new Port(config))
			{
				return port.GetPortByFields(ids, isInactive, columns);
			}
		}

		public DataSet GetPortList()
		{
			using (Port port = new Port(config))
			{
				return port.GetPortList();
			}
		}

		public DataSet GetPortComboList()
		{
			using (Port port = new Port(config))
			{
				return port.GetPortComboList();
			}
		}
	}
}
