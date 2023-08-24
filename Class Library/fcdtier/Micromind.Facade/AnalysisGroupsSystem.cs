using Micromind.Common.Data;
using Micromind.Common.Interfaces;
using Micromind.Data;
using System;
using System.Data;

namespace Micromind.Facade
{
	public sealed class AnalysisGroupsSystem : MarshalByRefObject, IAnalysisGroupsSystem, IDisposable
	{
		private Config config;

		public AnalysisGroupsSystem(Config config)
		{
			this.config = config;
		}

		public void Dispose()
		{
		}

		public bool CreateAnalysisGroup(AnalysisGroupsData data)
		{
			return new AnalysisGroups(config).InsertAnalysisGroup(data);
		}

		public bool UpdateAnalysisGroup(AnalysisGroupsData data)
		{
			return UpdateAnalysisGroup(data, checkConcurrency: false);
		}

		public bool UpdateAnalysisGroup(AnalysisGroupsData data, bool checkConcurrency)
		{
			return new AnalysisGroups(config).UpdateAnalysisGroup(data);
		}

		public AnalysisGroupsData GetAnalysisGroups()
		{
			using (AnalysisGroups analysisGroups = new AnalysisGroups(config))
			{
				return analysisGroups.GetAnalysisGroups();
			}
		}

		public bool DeleteAnalysisGroup(string groupID)
		{
			using (AnalysisGroups analysisGroups = new AnalysisGroups(config))
			{
				return analysisGroups.DeleteAnalysisGroup(groupID);
			}
		}

		public AnalysisGroupsData GetAnalysisGroupByID(string id)
		{
			using (AnalysisGroups analysisGroups = new AnalysisGroups(config))
			{
				return analysisGroups.GetAnalysisGroupByID(id);
			}
		}

		public DataSet GetAnalysisGroupsByFields(params string[] columns)
		{
			using (AnalysisGroups analysisGroups = new AnalysisGroups(config))
			{
				return analysisGroups.GetAnalysisGroupsByFields(columns);
			}
		}

		public DataSet GetAnalysisGroupsByFields(string[] ids, params string[] columns)
		{
			using (AnalysisGroups analysisGroups = new AnalysisGroups(config))
			{
				return analysisGroups.GetAnalysisGroupsByFields(ids, columns);
			}
		}

		public DataSet GetAnalysisGroupsByFields(string[] ids, bool isInactive, params string[] columns)
		{
			using (AnalysisGroups analysisGroups = new AnalysisGroups(config))
			{
				return analysisGroups.GetAnalysisGroupsByFields(ids, isInactive, columns);
			}
		}

		public DataSet GetAnalysisGroupsList()
		{
			using (AnalysisGroups analysisGroups = new AnalysisGroups(config))
			{
				return analysisGroups.GetAnalysisGroupsList();
			}
		}

		public DataSet GetAnalysisGroupsComboList()
		{
			using (AnalysisGroups analysisGroups = new AnalysisGroups(config))
			{
				return analysisGroups.GetAnalysisGroupsComboList();
			}
		}
	}
}
