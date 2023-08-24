using Micromind.Common.Data;
using Micromind.Common.Interfaces;
using Micromind.Data;
using System;
using System.Data;

namespace Micromind.Facade
{
	public sealed class CasePartySystem : MarshalByRefObject, ICasePartySystem, IDisposable
	{
		private Config config;

		public CasePartySystem(Config config)
		{
			this.config = config;
		}

		public void Dispose()
		{
		}

		public bool CreateCaseParty(CasePartyData data)
		{
			return new CaseParty(config).InsertCaseParty(data);
		}

		public bool UpdateCaseParty(CasePartyData data)
		{
			return UpdateCaseParty(data, checkConcurrency: false);
		}

		public bool UpdateCaseParty(CasePartyData data, bool checkConcurrency)
		{
			return new CaseParty(config).UpdateCaseParty(data);
		}

		public CasePartyData GetCaseParty()
		{
			using (CaseParty caseParty = new CaseParty(config))
			{
				return caseParty.GetCaseParty();
			}
		}

		public bool DeleteCaseParty(string groupID)
		{
			using (CaseParty caseParty = new CaseParty(config))
			{
				return caseParty.DeleteCaseParty(groupID);
			}
		}

		public CasePartyData GetCasePartyByID(string id)
		{
			using (CaseParty caseParty = new CaseParty(config))
			{
				return caseParty.GetCasePartyByID(id);
			}
		}

		public DataSet GetCasePartyByFields(params string[] columns)
		{
			using (CaseParty caseParty = new CaseParty(config))
			{
				return caseParty.GetCasePartyByFields(columns);
			}
		}

		public DataSet GetCasePartyByFields(string[] ids, params string[] columns)
		{
			using (CaseParty caseParty = new CaseParty(config))
			{
				return caseParty.GetCasePartyByFields(ids, columns);
			}
		}

		public DataSet GetCasePartyByFields(string[] ids, bool isInactive, params string[] columns)
		{
			using (CaseParty caseParty = new CaseParty(config))
			{
				return caseParty.GetCasePartyByFields(ids, isInactive, columns);
			}
		}

		public DataSet GetCasePartyList()
		{
			using (CaseParty caseParty = new CaseParty(config))
			{
				return caseParty.GetCasePartyList();
			}
		}

		public DataSet GetCasePartyComboList()
		{
			using (CaseParty caseParty = new CaseParty(config))
			{
				return caseParty.GetCasePartyComboList();
			}
		}
	}
}
