using Micromind.Common.Data;
using System.Data;

namespace Micromind.Common.Interfaces
{
	public interface ICollateralSystem
	{
		bool CreateCollateral(CollateralData collateralData);

		bool UpdateCollateral(CollateralData collateralData);

		CollateralData GetCollateral();

		bool DeleteCollateral(string ID);

		CollateralData GetCollateralByID(string id);

		DataSet GetCollateralByFields(params string[] columns);

		DataSet GetCollateralByFields(string[] ids, params string[] columns);

		DataSet GetCollateralByFields(string[] ids, bool isInactive, params string[] columns);

		DataSet GetCollateralList();

		DataSet GetCollateralComboList();

		DataSet GetCollteralToPrint(string fromColID, string toColID, string fromDepartment, string toDepartment, string fromLocation, string toLocation, bool showInactive);
	}
}
