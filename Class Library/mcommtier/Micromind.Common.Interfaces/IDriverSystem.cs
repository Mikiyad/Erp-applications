using Micromind.Common.Data;
using System.Data;

namespace Micromind.Common.Interfaces
{
	public interface IDriverSystem
	{
		bool CreateDriver(DriverData driverData);

		bool UpdateDriver(DriverData driverData);

		DriverData GetDriver();

		bool DeleteDriver(string ID);

		DriverData GetDriverByID(string id);

		DataSet GetDriverByFields(params string[] columns);

		DataSet GetDriverByFields(string[] ids, params string[] columns);

		DataSet GetDriverByFields(string[] ids, bool isInactive, params string[] columns);

		DataSet GetDriverList();

		DataSet GetDriverComboList();
	}
}
