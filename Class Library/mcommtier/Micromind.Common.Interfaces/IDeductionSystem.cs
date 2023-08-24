using Micromind.Common.Data;
using System.Data;

namespace Micromind.Common.Interfaces
{
	public interface IDeductionSystem
	{
		bool CreateDeduction(DeductionData deductionData);

		bool UpdateDeduction(DeductionData deductionData);

		DeductionData GetDeduction();

		bool DeleteDeduction(string ID);

		DeductionData GetDeductionByID(string id);

		DataSet GetDeductionByFields(params string[] columns);

		DataSet GetDeductionByFields(string[] ids, params string[] columns);

		DataSet GetDeductionByFields(string[] ids, bool isInactive, params string[] columns);

		DataSet GetDeductionList();

		DataSet GetDeductionComboList();
	}
}
