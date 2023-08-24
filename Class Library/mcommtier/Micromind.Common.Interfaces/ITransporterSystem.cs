using Micromind.Common.Data;
using System.Data;

namespace Micromind.Common.Interfaces
{
	public interface ITransporterSystem
	{
		bool CreateTransporter(TransporterData transporterData);

		bool UpdateTransporter(TransporterData transporterData);

		TransporterData GetTransporter();

		bool DeleteTransporter(string ID);

		TransporterData GetTransporterByID(string id);

		DataSet GetTransporterByFields(params string[] columns);

		DataSet GetTransporterByFields(string[] ids, params string[] columns);

		DataSet GetTransporterByFields(string[] ids, bool isInactive, params string[] columns);

		DataSet GetTransporterList();

		DataSet GetTransporterComboList();
	}
}
