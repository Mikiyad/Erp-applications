using Micromind.Common.Data;
using System.Data;

namespace Micromind.Common.Interfaces
{
	public interface IDestinationSystem
	{
		bool CreateDestination(DestinationData destinationData);

		bool UpdateDestination(DestinationData destinationData);

		DestinationData GetDestination();

		bool DeleteDestination(string ID);

		DestinationData GetDestinationByID(string id);

		DataSet GetDestinationByFields(params string[] columns);

		DataSet GetDestinationByFields(string[] ids, params string[] columns);

		DataSet GetDestinationByFields(string[] ids, bool isInactive, params string[] columns);

		DataSet GetDestinationList();

		DataSet GetDestinationComboList();
	}
}
