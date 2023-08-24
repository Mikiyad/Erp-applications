using Micromind.Common.Data;
using Micromind.Common.Interfaces;
using Micromind.Data;
using System;
using System.Data;

namespace Micromind.Facade
{
	public sealed class PropertyAgentSystem : MarshalByRefObject, IPropertyAgentSystem, IDisposable
	{
		private Config config;

		public PropertyAgentSystem(Config config)
		{
			this.config = config;
		}

		public void Dispose()
		{
		}

		public bool CreatePropertyAgent(PropertyAgentData data)
		{
			return new PropertyAgent(config).InsertPropertyAgent(data);
		}

		public bool UpdatePropertyAgent(PropertyAgentData data)
		{
			return UpdatePropertyAgent(data, checkConcurrency: false);
		}

		public bool UpdatePropertyAgent(PropertyAgentData data, bool checkConcurrency)
		{
			return new PropertyAgent(config).UpdatePropertyAgent(data);
		}

		public PropertyAgentData GetPropertyAgent()
		{
			using (PropertyAgent propertyAgent = new PropertyAgent(config))
			{
				return propertyAgent.GetPropertyAgent();
			}
		}

		public bool DeletePropertyAgent(string groupID)
		{
			using (PropertyAgent propertyAgent = new PropertyAgent(config))
			{
				return propertyAgent.DeletePropertyAgent(groupID);
			}
		}

		public PropertyAgentData GetPropertyAgentByID(string id)
		{
			using (PropertyAgent propertyAgent = new PropertyAgent(config))
			{
				return propertyAgent.GetPropertyAgentByID(id);
			}
		}

		public DataSet GetPropertyAgentByFields(params string[] columns)
		{
			using (PropertyAgent propertyAgent = new PropertyAgent(config))
			{
				return propertyAgent.GetPropertyAgentByFields(columns);
			}
		}

		public DataSet GetPropertyAgentByFields(string[] ids, params string[] columns)
		{
			using (PropertyAgent propertyAgent = new PropertyAgent(config))
			{
				return propertyAgent.GetPropertyAgentByFields(ids, columns);
			}
		}

		public DataSet GetPropertyAgentByFields(string[] ids, bool isInactive, params string[] columns)
		{
			using (PropertyAgent propertyAgent = new PropertyAgent(config))
			{
				return propertyAgent.GetPropertyAgentByFields(ids, isInactive, columns);
			}
		}

		public DataSet GetPropertyAgentList()
		{
			using (PropertyAgent propertyAgent = new PropertyAgent(config))
			{
				return propertyAgent.GetPropertyAgentList();
			}
		}

		public DataSet GetPropertyAgentComboList()
		{
			using (PropertyAgent propertyAgent = new PropertyAgent(config))
			{
				return propertyAgent.GetPropertyAgentComboList();
			}
		}
	}
}
