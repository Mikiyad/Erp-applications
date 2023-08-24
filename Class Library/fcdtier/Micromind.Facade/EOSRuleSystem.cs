using Micromind.Common.Data;
using Micromind.Common.Interfaces;
using Micromind.Data;
using System;
using System.Data;

namespace Micromind.Facade
{
	public sealed class EOSRuleSystem : MarshalByRefObject, IEOSRuleSystem, IDisposable
	{
		private Config config;

		public EOSRuleSystem(Config config)
		{
			this.config = config;
		}

		public void Dispose()
		{
		}

		public bool CreateEOSRule(EOSRuleData data)
		{
			return new EOSRule(config).InsertEOSRule(data);
		}

		public bool UpdateEOSRule(EOSRuleData data)
		{
			return UpdateEOSRule(data, checkConcurrency: false);
		}

		public bool UpdateEOSRule(EOSRuleData data, bool checkConcurrency)
		{
			return new EOSRule(config).UpdateEOSRule(data);
		}

		public EOSRuleData GetEOSRule()
		{
			using (EOSRule eOSRule = new EOSRule(config))
			{
				return eOSRule.GetEOSRule();
			}
		}

		public bool DeleteEOSRule(string groupID)
		{
			using (EOSRule eOSRule = new EOSRule(config))
			{
				return eOSRule.DeleteEOSRule(groupID);
			}
		}

		public EOSRuleData GetEOSRuleByID(string id)
		{
			using (EOSRule eOSRule = new EOSRule(config))
			{
				return eOSRule.GetEOSRuleByID(id);
			}
		}

		public DataSet GetEOSRuleByFields(params string[] columns)
		{
			using (EOSRule eOSRule = new EOSRule(config))
			{
				return eOSRule.GetEOSRuleByFields(columns);
			}
		}

		public DataSet GetEOSRuleByFields(string[] ids, params string[] columns)
		{
			using (EOSRule eOSRule = new EOSRule(config))
			{
				return eOSRule.GetEOSRuleByFields(ids, columns);
			}
		}

		public DataSet GetEOSRuleByFields(string[] ids, bool isInactive, params string[] columns)
		{
			using (EOSRule eOSRule = new EOSRule(config))
			{
				return eOSRule.GetEOSRuleByFields(ids, isInactive, columns);
			}
		}

		public DataSet GetEOSRuleList()
		{
			using (EOSRule eOSRule = new EOSRule(config))
			{
				return eOSRule.GetEOSRuleList();
			}
		}

		public DataSet GetEOSRuleComboList()
		{
			using (EOSRule eOSRule = new EOSRule(config))
			{
				return eOSRule.GetEOSRuleComboList();
			}
		}
	}
}
