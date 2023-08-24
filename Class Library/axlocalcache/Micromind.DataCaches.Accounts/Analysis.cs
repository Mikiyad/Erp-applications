using Micromind.ClientLibraries;
using System;
using System.Data;

namespace Micromind.DataCaches.Accounts
{
	public sealed class Analysis
	{
		private static DataSet analysis;

		static Analysis()
		{
		}

		internal static void Reset()
		{
			if (analysis != null)
			{
				analysis.Dispose();
				analysis = null;
			}
		}

		public static DataSet GetAnalysis(bool isReferesh)
		{
			if (Factory.IsDBConnected)
			{
				if ((analysis == null) | isReferesh)
				{
					SetData();
				}
				return analysis;
			}
			return null;
		}

		private static void SetData()
		{
			if (analysis != null)
			{
				analysis.Dispose();
				analysis = null;
			}
			analysis = GetData();
		}

		private static DataSet GetData()
		{
			try
			{
				return Factory.AnalysisSystem.GetAnalysisComboList();
			}
			catch (Exception e)
			{
				ErrorHelper.ProcessError(e);
				return null;
			}
		}
	}
}
