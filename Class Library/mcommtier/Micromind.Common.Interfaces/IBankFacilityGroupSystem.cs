using Micromind.Common.Data;
using System.Data;

namespace Micromind.Common.Interfaces
{
	public interface IBankFacilityGroupSystem
	{
		bool CreateBankFacilityGroup(BankFacilityGroupData bankFacilityGroupData);

		bool UpdateBankFacilityGroup(BankFacilityGroupData bankFacilityGroupData);

		BankFacilityGroupData GetBankFacilityGroup();

		bool DeleteBankFacilityGroup(string ID);

		BankFacilityGroupData GetBankFacilityGroupByID(string id);

		DataSet GetBankFacilityGroupByFields(params string[] columns);

		DataSet GetBankFacilityGroupByFields(string[] ids, params string[] columns);

		DataSet GetBankFacilityGroupByFields(string[] ids, bool isInactive, params string[] columns);

		DataSet GetBankFacilityGroupList();

		DataSet GetBankFacilityGroupComboList();
	}
}
