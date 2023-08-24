using Micromind.Common.Data;
using Micromind.Common.Interfaces;
using Micromind.Data;
using System;
using System.Data;

namespace Micromind.Facade
{
	public sealed class OpportunitySystem : MarshalByRefObject, IOpportunitySystem, IDisposable
	{
		private Config config;

		public OpportunitySystem(Config config)
		{
			this.config = config;
		}

		public void Dispose()
		{
		}

		public bool CreateOpportunity(OpportunityData data)
		{
			return new Opportunity(config).InsertOpportunity(data);
		}

		public bool UpdateOpportunity(OpportunityData data)
		{
			return UpdateOpportunity(data, checkConcurrency: false);
		}

		public bool UpdateOpportunity(OpportunityData data, bool checkConcurrency)
		{
			return new Opportunity(config).UpdateOpportunity(data);
		}

		public OpportunityData GetOpportunity()
		{
			using (Opportunity opportunity = new Opportunity(config))
			{
				return opportunity.GetOpportunity();
			}
		}

		public bool DeleteOpportunity(string groupID)
		{
			using (Opportunity opportunity = new Opportunity(config))
			{
				return opportunity.DeleteOpportunity(groupID);
			}
		}

		public OpportunityData GetOpportunityByID(string id)
		{
			using (Opportunity opportunity = new Opportunity(config))
			{
				return opportunity.GetOpportunityByID(id);
			}
		}

		public DataSet GetOpportunityByFields(params string[] columns)
		{
			using (Opportunity opportunity = new Opportunity(config))
			{
				return opportunity.GetOpportunityByFields(columns);
			}
		}

		public DataSet GetOpportunityByFields(string[] ids, params string[] columns)
		{
			using (Opportunity opportunity = new Opportunity(config))
			{
				return opportunity.GetOpportunityByFields(ids, columns);
			}
		}

		public DataSet GetOpportunityByFields(string[] ids, bool isInactive, params string[] columns)
		{
			using (Opportunity opportunity = new Opportunity(config))
			{
				return opportunity.GetOpportunityByFields(ids, isInactive, columns);
			}
		}

		public DataSet GetOpportunityList()
		{
			using (Opportunity opportunity = new Opportunity(config))
			{
				return opportunity.GetOpportunityList();
			}
		}

		public DataSet GetOpportunityListByLeadID(CRMRelatedTypes leadType, string leadID, bool includeClosed)
		{
			using (Opportunity opportunity = new Opportunity(config))
			{
				return opportunity.GetOpportunityListByLeadID(leadType, leadID, includeClosed);
			}
		}

		public DataSet GetUpcomingOpportunitiesReport(string fromLead, string toLead, DateTime dtFrom, DateTime dtTo)
		{
			using (Opportunity opportunity = new Opportunity(config))
			{
				return opportunity.GetUpcomingOpportunitiesReport(fromLead, toLead, dtFrom, dtTo);
			}
		}

		public DataSet GetOpportunityComboList()
		{
			using (Opportunity opportunity = new Opportunity(config))
			{
				return opportunity.GetOpportunityComboList();
			}
		}
	}
}
