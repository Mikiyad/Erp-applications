using Micromind.Common.Data;
using System.Data;

namespace Micromind.Common.Interfaces
{
	public interface IBankFacilitySystem
	{
		bool CreateBankFacility(BankFacilityData bankFacilityData);

		bool UpdateBankFacility(BankFacilityData bankFacilityData);

		BankFacilityData GetBankFacility();

		bool DeleteBankFacility(string ID);

		BankFacilityData GetBankFacilityByID(string id);

		DataSet GetBankFacilityByFields(params string[] columns);

		DataSet GetBankFacilityByFields(string[] ids, params string[] columns);

		DataSet GetBankFacilityByFields(string[] ids, bool isInactive, params string[] columns);

		DataSet GetBankFacilityList();

		DataSet GetBankFacilityListByID(string id);

		DataSet GetBankFacilityComboList();

		decimal GetBankFacilityAvailableLimit(string facilityID);
	}
}
