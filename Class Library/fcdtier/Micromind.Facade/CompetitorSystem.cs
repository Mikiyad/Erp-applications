using Micromind.Common.Data;
using Micromind.Common.Interfaces;
using Micromind.Data;
using System;
using System.Data;

namespace Micromind.Facade
{
	public sealed class CompetitorSystem : MarshalByRefObject, ICompetitorSystem, IDisposable
	{
		private Config config;

		public CompetitorSystem(Config config)
		{
			this.config = config;
		}

		public void Dispose()
		{
		}

		public bool CreateCompetitor(CompetitorData data)
		{
			return new Competitor(config).InsertCompetitor(data);
		}

		public bool UpdateCompetitor(CompetitorData data)
		{
			return UpdateCompetitor(data, checkConcurrency: false);
		}

		public bool UpdateCompetitor(CompetitorData data, bool checkConcurrency)
		{
			return new Competitor(config).UpdateCompetitor(data);
		}

		public CompetitorData GetCompetitor()
		{
			using (Competitor competitor = new Competitor(config))
			{
				return competitor.GetCompetitor();
			}
		}

		public bool DeleteCompetitor(string groupID)
		{
			using (Competitor competitor = new Competitor(config))
			{
				return competitor.DeleteCompetitor(groupID);
			}
		}

		public CompetitorData GetCompetitorByID(string id)
		{
			using (Competitor competitor = new Competitor(config))
			{
				return competitor.GetCompetitorByID(id);
			}
		}

		public DataSet GetCompetitorByFields(params string[] columns)
		{
			using (Competitor competitor = new Competitor(config))
			{
				return competitor.GetCompetitorByFields(columns);
			}
		}

		public DataSet GetCompetitorByFields(string[] ids, params string[] columns)
		{
			using (Competitor competitor = new Competitor(config))
			{
				return competitor.GetCompetitorByFields(ids, columns);
			}
		}

		public DataSet GetCompetitorByFields(string[] ids, bool isInactive, params string[] columns)
		{
			using (Competitor competitor = new Competitor(config))
			{
				return competitor.GetCompetitorByFields(ids, isInactive, columns);
			}
		}

		public DataSet GetCompetitorList()
		{
			using (Competitor competitor = new Competitor(config))
			{
				return competitor.GetCompetitorList();
			}
		}

		public DataSet GetCompetitorComboList()
		{
			using (Competitor competitor = new Competitor(config))
			{
				return competitor.GetCompetitorComboList();
			}
		}
	}
}
