using Micromind.Common.Data;
using System.Data;

namespace Micromind.Common.Interfaces
{
	public interface IBenefitSystem
	{
		bool CreateBenefit(BenefitData benefitData);

		bool UpdateBenefit(BenefitData benefitData);

		BenefitData GetBenefit();

		bool DeleteBenefit(string ID);

		BenefitData GetBenefitByID(string id);

		DataSet GetBenefitByFields(params string[] columns);

		DataSet GetBenefitByFields(string[] ids, params string[] columns);

		DataSet GetBenefitByFields(string[] ids, bool isInactive, params string[] columns);

		DataSet GetBenefitList();

		DataSet GetBenefitComboList();
	}
}
