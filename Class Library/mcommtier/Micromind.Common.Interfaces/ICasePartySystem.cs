using Micromind.Common.Data;
using System.Data;

namespace Micromind.Common.Interfaces
{
	public interface ICasePartySystem
	{
		bool CreateCaseParty(CasePartyData vendorClassData);

		bool UpdateCaseParty(CasePartyData vendorClassData);

		CasePartyData GetCaseParty();

		bool DeleteCaseParty(string ID);

		CasePartyData GetCasePartyByID(string id);

		DataSet GetCasePartyByFields(params string[] columns);

		DataSet GetCasePartyByFields(string[] ids, params string[] columns);

		DataSet GetCasePartyByFields(string[] ids, bool isInactive, params string[] columns);

		DataSet GetCasePartyList();

		DataSet GetCasePartyComboList();
	}
}
