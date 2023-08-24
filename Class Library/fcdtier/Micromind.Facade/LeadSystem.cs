using Micromind.Common.Data;
using Micromind.Common.Interfaces;
using Micromind.Data;
using System;
using System.Data;

namespace Micromind.Facade
{
	public sealed class LeadSystem : MarshalByRefObject, ILeadSystem, IDisposable
	{
		private Config config;

		public LeadSystem(Config config)
		{
			this.config = config;
		}

		public void Dispose()
		{
		}

		public bool CreateLead(LeadData data)
		{
			return new Leads(config).InsertUpdateLead(data, isUpdate: false);
		}

		public bool UpdateLead(LeadData data)
		{
			return UpdateLead(data, checkConcurrency: false);
		}

		public bool UpdateLead(LeadData data, bool checkConcurrency)
		{
			return new Leads(config).InsertUpdateLead(data, isUpdate: true);
		}

		public LeadData GetLead()
		{
			using (Leads leads = new Leads(config))
			{
				return leads.GetLead();
			}
		}

		public bool DeleteLead(string groupID)
		{
			using (Leads leads = new Leads(config))
			{
				return leads.DeleteLead(groupID);
			}
		}

		public LeadData GetLeadByID(string id)
		{
			using (Leads leads = new Leads(config))
			{
				return leads.GetLeadByID(id);
			}
		}

		public DataSet GetLeadByFields(params string[] columns)
		{
			using (Leads leads = new Leads(config))
			{
				return leads.GetLeadByFields(columns);
			}
		}

		public DataSet GetLeadByFields(string[] ids, params string[] columns)
		{
			using (Leads leads = new Leads(config))
			{
				return leads.GetLeadByFields(ids, columns);
			}
		}

		public DataSet GetLeadByFields(string[] ids, bool isInactive, params string[] columns)
		{
			using (Leads leads = new Leads(config))
			{
				return leads.GetLeadByFields(ids, isInactive, columns);
			}
		}

		public DataSet GetLeadList(bool showInactive)
		{
			using (Leads leads = new Leads(config))
			{
				return leads.GetLeadList(showInactive);
			}
		}

		public DataSet GetLeadComboList()
		{
			using (Leads leads = new Leads(config))
			{
				return leads.GetLeadComboList();
			}
		}

		public DataSet GetLeadSourceComboList()
		{
			using (Leads leads = new Leads(config))
			{
				return leads.GetLeadSourceComboList();
			}
		}

		public DataSet GetLeadBalanceSummary(string fromLead, string toLead, string fromClass, string toClass, string fromGroup, string toGroup, bool showZeroBalance, bool isFC)
		{
			using (Leads leads = new Leads(config))
			{
				return leads.GetLeadBalanceSummary(fromLead, toLead, fromClass, toClass, fromGroup, toGroup, showZeroBalance, isFC);
			}
		}

		public DataSet GetLeadBalanceDetailFCReport(DateTime from, DateTime to, string fromLead, string toLead, string fromClass, string toClass, string fromGroup, string toGroup, bool showZeroBalance, string currencyID)
		{
			using (Leads leads = new Leads(config))
			{
				return leads.GetLeadBalanceDetailFCReport(from, to, fromLead, toLead, fromClass, toClass, fromGroup, toGroup, showZeroBalance, currencyID);
			}
		}

		public DataSet GetLeadBalanceDetailReport(DateTime from, DateTime to, string fromLead, string toLead, string fromClass, string toClass, string fromGroup, string toGroup, bool showZeroBalance, string currencyID)
		{
			using (Leads leads = new Leads(config))
			{
				return leads.GetLeadBalanceDetailReport(from, to, fromLead, toLead, fromClass, toClass, fromGroup, toGroup, showZeroBalance, currencyID);
			}
		}

		public DataSet GetSalesByLeadDetailReport(DateTime from, DateTime to, string fromLead, string toLead, string fromClass, string toClass, string fromGroup, string toGroup)
		{
			using (Leads leads = new Leads(config))
			{
				return leads.GetSalesByLeadDetailReport(from, to, fromLead, toLead, fromClass, toClass, fromGroup, toGroup);
			}
		}

		public DataSet GetSalesByLeadSummaryReport(DateTime from, DateTime to, string fromLead, string toLead, string fromClass, string toClass, string fromGroup, string toGroup)
		{
			using (Leads leads = new Leads(config))
			{
				return leads.GetSalesByLeadSummaryReport(from, to, fromLead, toLead, fromClass, toClass, fromGroup, toGroup);
			}
		}

		public DataSet GetSalesByLeadGroupDetailReport(DateTime from, DateTime to, string fromGroup, string toGroup)
		{
			using (Leads leads = new Leads(config))
			{
				return leads.GetSalesByLeadGroupDetailReport(from, to, fromGroup, toGroup);
			}
		}

		public DataSet GetSalesByLeadGroupSummaryReport(DateTime from, DateTime to, string fromGroup, string toGroup)
		{
			using (Leads leads = new Leads(config))
			{
				return leads.GetSalesByLeadGroupSummaryReport(from, to, fromGroup, toGroup);
			}
		}

		public DataSet GetLeadDocumentAddress(string leadID, string addressField)
		{
			using (Leads leads = new Leads(config))
			{
				return leads.GetLeadDocumentAddress(leadID, addressField);
			}
		}

		public DataSet GetLeadListReport(string fromLead, string toLead, string fromClass, string toClass, string fromGroup, string toGroup, string fromArea, string toArea, bool showInactive)
		{
			using (Leads leads = new Leads(config))
			{
				return leads.GetLeadListReport(fromLead, toLead, fromClass, toClass, fromGroup, toGroup, fromArea, toArea, showInactive);
			}
		}

		public DataSet GetLeadPrimaryContactListReport(string fromLead, string toLead, string fromClass, string toClass, string fromGroup, string toGroup, string fromArea, string toArea, bool showInactive)
		{
			using (Leads leads = new Leads(config))
			{
				return leads.GetLeadPrimaryContactListReport(fromLead, toLead, fromClass, toClass, fromGroup, toGroup, fromArea, toArea, showInactive);
			}
		}

		public DataSet GetLeadProfileReport(string fromLead, string toLead, bool showInactive)
		{
			using (Leads leads = new Leads(config))
			{
				return leads.GetLeadProfileReport(fromLead, toLead, showInactive);
			}
		}

		public DataSet GetLeadBySourceReport(string fromSource, string toSource, string fromArea, string toArea, bool showInactive)
		{
			using (Leads leads = new Leads(config))
			{
				return leads.GetLeadBySourceReport(fromSource, toSource, fromArea, toArea, showInactive);
			}
		}

		public DataSet GetLeadActivityReport(DateTime from, DateTime to, string fromLead, string toLead, string fromClass, string toClass, string fromGroup, string toGroup)
		{
			using (Leads leads = new Leads(config))
			{
				return leads.GetLeadActivityReport(from, to, fromLead, toLead, fromClass, toClass, fromGroup, toGroup);
			}
		}

		public DataSet GetLeadBalanceAmount(string leadCode)
		{
			using (Leads leads = new Leads(config))
			{
				return leads.GetLeadBalanceAmount(leadCode);
			}
		}

		public DataSet GetTopLeads(DateTime from, DateTime to, int count)
		{
			using (Leads leads = new Leads(config))
			{
				return leads.GetTopLeads(from, to, count);
			}
		}

		public DataSet GetTopSalesperson(DateTime from, DateTime to, int count)
		{
			using (Leads leads = new Leads(config))
			{
				return leads.GetTopSalesperson(from, to, count);
			}
		}

		public DataSet GetMonthlySalesReport(DateTime from, DateTime to)
		{
			using (Leads leads = new Leads(config))
			{
				return leads.GetMonthlySalesReport(from, to);
			}
		}

		public bool SetFlag(string leadID, byte flagID)
		{
			using (Leads leads = new Leads(config))
			{
				return leads.SetFlag(leadID, flagID);
			}
		}
	}
}
