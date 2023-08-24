using Micromind.Common.Data;
using System.Data;

namespace Micromind.Common.Interfaces
{
	public interface IEOSRuleSystem
	{
		bool CreateEOSRule(EOSRuleData eosRuleData);

		bool UpdateEOSRule(EOSRuleData eosRuleData);

		EOSRuleData GetEOSRule();

		bool DeleteEOSRule(string ID);

		EOSRuleData GetEOSRuleByID(string id);

		DataSet GetEOSRuleByFields(params string[] columns);

		DataSet GetEOSRuleByFields(string[] ids, params string[] columns);

		DataSet GetEOSRuleByFields(string[] ids, bool isInactive, params string[] columns);

		DataSet GetEOSRuleList();

		DataSet GetEOSRuleComboList();
	}
}
