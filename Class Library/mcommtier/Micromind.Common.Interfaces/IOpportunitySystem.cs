using Micromind.Common.Data;
using System;
using System.Data;

namespace Micromind.Common.Interfaces
{
	public interface IOpportunitySystem
	{
		bool CreateOpportunity(OpportunityData opportunityData);

		bool UpdateOpportunity(OpportunityData opportunityData);

		OpportunityData GetOpportunity();

		bool DeleteOpportunity(string ID);

		OpportunityData GetOpportunityByID(string id);

		DataSet GetOpportunityByFields(params string[] columns);

		DataSet GetOpportunityByFields(string[] ids, params string[] columns);

		DataSet GetOpportunityByFields(string[] ids, bool isInactive, params string[] columns);

		DataSet GetOpportunityList();

		DataSet GetOpportunityListByLeadID(CRMRelatedTypes leadType, string leadID, bool includeClosed);

		DataSet GetOpportunityComboList();

		DataSet GetUpcomingOpportunitiesReport(string fromLead, string toLead, DateTime dtFrom, DateTime dtTo);
	}
}
