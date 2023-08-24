using Micromind.Common.Data;
using System.Data;

namespace Micromind.Common.Interfaces
{
	public interface IPortSystem
	{
		bool CreatePort(PortData portData);

		bool UpdatePort(PortData portData);

		PortData GetPort();

		bool DeletePort(string ID);

		PortData GetPortByID(string id);

		DataSet GetPortByFields(params string[] columns);

		DataSet GetPortByFields(string[] ids, params string[] columns);

		DataSet GetPortByFields(string[] ids, bool isInactive, params string[] columns);

		DataSet GetPortList();

		DataSet GetPortComboList();
	}
}
