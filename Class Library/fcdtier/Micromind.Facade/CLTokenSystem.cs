using Micromind.Common.Data;
using Micromind.Common.Interfaces;
using Micromind.Data;
using System;
using System.Data;

namespace Micromind.Facade
{
	public sealed class CLTokenSystem : MarshalByRefObject, ICLTokenSystem, IDisposable
	{
		private Config config;

		public CLTokenSystem(Config config)
		{
			this.config = config;
		}

		public void Dispose()
		{
		}

		public bool CreateCLToken(CLTokenData data)
		{
			return new CLToken(config).InsertCLToken(data);
		}

		public bool UpdateCLToken(CLTokenData data)
		{
			return UpdateCLToken(data, checkConcurrency: false);
		}

		public bool UpdateCLToken(CLTokenData data, bool checkConcurrency)
		{
			return new CLToken(config).UpdateCLToken(data);
		}

		public CLTokenData GetCLToken()
		{
			using (CLToken cLToken = new CLToken(config))
			{
				return cLToken.GetCLToken();
			}
		}

		public bool DeleteCLToken(string groupID)
		{
			using (CLToken cLToken = new CLToken(config))
			{
				return cLToken.DeleteCLToken(groupID);
			}
		}

		public CLTokenData GetCLTokenByID(string id)
		{
			using (CLToken cLToken = new CLToken(config))
			{
				return cLToken.GetCLTokenByID(id);
			}
		}

		public DataSet GetCLTokenList()
		{
			using (CLToken cLToken = new CLToken(config))
			{
				return cLToken.GetCLTokenList();
			}
		}

		public DataSet GetCLTokenComboList()
		{
			using (CLToken cLToken = new CLToken(config))
			{
				return cLToken.GetCLTokenComboList();
			}
		}

		public bool RequestCLToken(CLTokenData clTokenData)
		{
			using (CLToken cLToken = new CLToken(config))
			{
				return cLToken.RequestCLToken(clTokenData);
			}
		}
	}
}
