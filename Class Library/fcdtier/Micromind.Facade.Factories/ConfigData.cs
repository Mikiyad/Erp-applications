using Micromind.Data;
using Micromind.Securities;
using System;

namespace Micromind.Facade.Factories
{
	internal class ConfigData
	{
		public string ApplicationName;

		public string ClientMachineName;

		public string ServerName;

		public string DbName;

		public string UserID;

		public string SystemID;

		public string ProductKey;

		public string Password;

		public DateTime loginDateTime = DateTime.Now;

		public DateTime lastLogDate = DateTime.Now;

		public string ID;

		public ConnectionTypes connectionType;

		public Config config;

		public ConfigData(string applicationName, string clientMachineName, string serverName, string systemID, string productKey, string dbName, string userID, string password, Config config)
		{
			ApplicationName = applicationName;
			ClientMachineName = clientMachineName;
			ServerName = serverName;
			SystemID = systemID;
			ProductKey = productKey;
			DbName = dbName;
			UserID = userID;
			Password = password;
			this.config = config;
		}
	}
}
