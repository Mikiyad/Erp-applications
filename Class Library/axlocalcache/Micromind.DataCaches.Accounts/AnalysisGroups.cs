using Micromind.ClientLibraries;
using System;
using System.Data;

namespace Micromind.DataCaches.Accounts
{
	public sealed class AnalysisGroups
	{
		private static DataSet analysisGroups;

		static AnalysisGroups()
		{
		}

		internal static void Reset()
		{
			if (analysisGroups != null)
			{
				analysisGroups.Dispose();
				analysisGroups = null;
			}
		}

		public static DataSet GetAnalysisGroups(bool isReferesh)
		{
			if (Factory.IsDBConnected)
			{
				if ((analysisGroups == null) | isReferesh)
				{
					SetData();
				}
				return analysisGroups;
			}
			return null;
		}

		private static void SetData()
		{
			if (analysisGroups != null)
			{
				analysisGroups.Dispose();
				analysisGroups = null;
			}
			analysisGroups = GetData();
		}

		private static DataSet GetData()
		{
			try
			{
				return Factory.AnalysisGroupsSystem.GetAnalysisGroupsComboList();
			}
			catch (Exception e)
			{
				ErrorHelper.ProcessError(e);
				return null;
			}
		}
	}
}
