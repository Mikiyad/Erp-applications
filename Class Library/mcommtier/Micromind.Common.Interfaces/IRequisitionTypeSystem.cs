using Micromind.Common.Data;
using System.Data;

namespace Micromind.Common.Interfaces
{
	public interface IRequisitionTypeSystem
	{
		bool CreateRequisitionType(RequisitionTypeData RequisitionTypeData);

		bool UpdateRequisitionType(RequisitionTypeData RequisitionTypeData);

		RequisitionTypeData GetRequisitionType();

		bool DeleteRequisitionType(string ID);

		RequisitionTypeData GetRequisitionTypeByID(string id);

		DataSet GetRequisitionTypeByFields(params string[] columns);

		DataSet GetRequisitionTypeByFields(string[] ids, params string[] columns);

		DataSet GetRequisitionTypeByFields(string[] ids, bool isInactive, params string[] columns);

		DataSet GetRequisitionTypeList();

		DataSet GetRequisitionTypeComboList();
	}
}
