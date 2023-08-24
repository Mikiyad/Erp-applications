using Micromind.Common.Data;
using System.Data;

namespace Micromind.Common.Interfaces
{
	public interface IPropertyAgentSystem
	{
		bool CreatePropertyAgent(PropertyAgentData areaData);

		bool UpdatePropertyAgent(PropertyAgentData areaData);

		PropertyAgentData GetPropertyAgent();

		bool DeletePropertyAgent(string ID);

		PropertyAgentData GetPropertyAgentByID(string id);

		DataSet GetPropertyAgentByFields(params string[] columns);

		DataSet GetPropertyAgentByFields(string[] ids, params string[] columns);

		DataSet GetPropertyAgentByFields(string[] ids, bool isInactive, params string[] columns);

		DataSet GetPropertyAgentList();

		DataSet GetPropertyAgentComboList();
	}
}
