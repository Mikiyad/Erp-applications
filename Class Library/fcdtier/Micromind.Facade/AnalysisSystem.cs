using Micromind.Common.Data;
using Micromind.Common.Interfaces;
using Micromind.Data;
using System;
using System.Data;

namespace Micromind.Facade
{
	public sealed class AnalysisSystem : MarshalByRefObject, IAnalysisSystem, IDisposable
	{
		private Config config;

		public AnalysisSystem(Config config)
		{
			this.config = config;
		}

		public void Dispose()
		{
		}

		public bool CreateAnalysis(AnalysisData data)
		{
			return new Analysis(config).InsertAnalysis(data);
		}

		public bool UpdateAnalysis(AnalysisData data)
		{
			return UpdateAnalysis(data, checkConcurrency: false);
		}

		public bool UpdateAnalysis(AnalysisData data, bool checkConcurrency)
		{
			return new Analysis(config).UpdateAnalysis(data);
		}

		public AnalysisData GetAnalysis()
		{
			using (Analysis analysis = new Analysis(config))
			{
				return analysis.GetAnalysis();
			}
		}

		public bool DeleteAnalysis(string groupID)
		{
			using (Analysis analysis = new Analysis(config))
			{
				return analysis.DeleteAnalysis(groupID);
			}
		}

		public AnalysisData GetAnalysisByID(string id)
		{
			using (Analysis analysis = new Analysis(config))
			{
				return analysis.GetAnalysisByID(id);
			}
		}

		public DataSet GetAnalysisByFields(params string[] columns)
		{
			using (Analysis analysis = new Analysis(config))
			{
				return analysis.GetAnalysisByFields(columns);
			}
		}

		public DataSet GetAnalysisByFields(string[] ids, params string[] columns)
		{
			using (Analysis analysis = new Analysis(config))
			{
				return analysis.GetAnalysisByFields(ids, columns);
			}
		}

		public DataSet GetAnalysisByFields(string[] ids, bool isInactive, params string[] columns)
		{
			using (Analysis analysis = new Analysis(config))
			{
				return analysis.GetAnalysisByFields(ids, isInactive, columns);
			}
		}

		public DataSet GetAnalysisList()
		{
			using (Analysis analysis = new Analysis(config))
			{
				return analysis.GetAnalysisList();
			}
		}

		public DataSet GetAnalysisComboList()
		{
			using (Analysis analysis = new Analysis(config))
			{
				return analysis.GetAnalysisComboList();
			}
		}

		public DataSet GetAnalysisNonAccountComboList()
		{
			using (Analysis analysis = new Analysis(config))
			{
				return analysis.GetAnalysisNonAccountComboList();
			}
		}
	}
}
