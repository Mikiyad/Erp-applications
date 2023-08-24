using Micromind.Common.Data;
using System.Data;

namespace Micromind.Common.Interfaces
{
	public interface IPartySystem
	{
		bool CreateParty(PartyData partyData, bool isUpdate);

		PartyData GetParty();

		bool DeleteParty(string ID);

		PartyData GetPartyByID(string id);

		DataSet GetPartyByFields(params string[] columns);

		DataSet GetPartyByFields(string[] ids, params string[] columns);

		DataSet GetPartyByFields(string[] ids, bool isInactive, params string[] columns);

		DataSet GetPartyList();

		DataSet GetPartyComboList();
	}
}
