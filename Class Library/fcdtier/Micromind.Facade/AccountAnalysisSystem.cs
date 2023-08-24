using Micromind.Common.Data;
using Micromind.Common.Interfaces;
using Micromind.Data;
using System;
using System.Data;

namespace Micromind.Facade
{
	public sealed class AccountAnalysisSystem : MarshalByRefObject, IAccountAnalysisDetailSystem, IDisposable
	{
		private Config config;

		public AccountAnalysisSystem(Config config)
		{
			this.config = config;
		}

		public void Dispose()
		{
		}

		public bool CreateAccountAnalysisDetail(DataSet data)
		{
			return new AccountAnalysisDetail(config).InsertAccountAnalysisDetail(data);
		}

		public bool UpdateAccountAnalysisDetail(DataSet data)
		{
			return UpdateAccountAnalysisDetail(data);
		}

		public bool UpdateAccountAnalysisDetail(AccountAnalysisData data, bool checkConcurrency)
		{
			return new AccountAnalysisDetail(config).UpdateAccountAnalysisDetail(data);
		}

		public bool DeleteAccountAnalysis(string accountID)
		{
			return new AccountAnalysisDetail(config).DeleteAccountAnalysis(accountID);
		}

		public DataSet GetAccountAnalysisGroupsByAccountID(string id)
		{
			return new AccountAnalysisDetail(config).GetAccountAnalysisGroupByAccountID(id);
		}
	}
}
