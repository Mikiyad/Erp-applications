using Micromind.Common.Data;
using Micromind.Common.Interfaces;
using Micromind.Data;
using System;
using System.Data;

namespace Micromind.Facade
{
	public sealed class SecuritySystem : MarshalByRefObject, ISecuritySystem, IDisposable
	{
		private Config config;

		public SecuritySystem(Config config)
		{
			this.config = config;
		}

		public void Dispose()
		{
		}

		public bool CreateSecurity(SecurityData securityData, string userID, string groupID)
		{
			return new Security(config).InsertSecurity(securityData, userID, groupID);
		}

		public DataSet GetSecurityDataByID(string userID, bool isGroupLevel)
		{
			return new Security(config).GetSecurityDataByID(userID, isGroupLevel);
		}

		public SecurityData GetUserSecurityData(string userID)
		{
			return new Security(config).GetUserSecurityData(userID);
		}

		public string GetUserEmployeeID(string userID)
		{
			return new Security(config).GetUserEmployeeID(userID);
		}

		public DataSet GetReportSecurityDataByID(string userID, bool isGroupLevel, bool IsFullView)
		{
			return new Security(config).GetReportSecurityDataByID(userID, isGroupLevel, IsFullView);
		}
	}
}
