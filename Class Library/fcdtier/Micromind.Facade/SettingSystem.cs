using Micromind.Common.Interfaces;
using Micromind.Data;
using System;
using System.Data;

namespace Micromind.Facade
{
	public sealed class SettingSystem : MarshalByRefObject, ISettingSystem
	{
		private Config config;

		public SettingSystem(Config config)
		{
			this.config = config;
		}

		public bool SaveSetting(string id, string appName, string key, string val, string data)
		{
			using (Settings settings = new Settings(config))
			{
				return settings.SaveSetting(id, appName, key, val, data);
			}
		}

		public bool SaveSetting(string key, string userID, object value)
		{
			using (Settings settings = new Settings(config))
			{
				return settings.SaveSetting(key, userID, value);
			}
		}

		public bool SaveSettingStream(string key, string userID, byte[] data)
		{
			using (Settings settings = new Settings(config))
			{
				return settings.SaveSettingStream(userID, key, "", data);
			}
		}

		public bool SaveSettingStream(string key, string userID, string groupName, byte[] data)
		{
			using (Settings settings = new Settings(config))
			{
				return settings.SaveSettingStream(userID, key, groupName, data);
			}
		}

		public bool SaveSettingStreamForDashBoard(string key, string userID, string groupName, byte[] data, string sysDocID, string VoucherID, int AutoKeyID, DataSet ds, bool isUpdate)
		{
			using (Settings settings = new Settings(config))
			{
				return settings.SaveSettingStreamForDashBoard(userID, key, groupName, data, sysDocID, VoucherID, AutoKeyID, ds, isUpdate);
			}
		}

		public bool SaveGlobalSetting(string val, string data)
		{
			using (Settings settings = new Settings(config))
			{
				return settings.SaveSetting("E4C9AB0B1E954fe8A4839E8540290204I", "E4C9AB0B1E954fe8A4839E8540290205N", "E4C9AB0B1E954fe8A4839E8540290203K", val, data);
			}
		}

		public object GetData(string id, string appName, string key, string val, object defaultValue)
		{
			using (Settings settings = new Settings(config))
			{
				object data = settings.GetData(id, appName, key, val);
				if (data == null)
				{
					return defaultValue;
				}
				return data;
			}
		}

		public object GetUserSetting(string userID, string key, object defaultValue)
		{
			using (Settings settings = new Settings(config))
			{
				object userSetting = settings.GetUserSetting(userID, key);
				if (userSetting == null)
				{
					return defaultValue;
				}
				return userSetting;
			}
		}

		public object GetGlobalData(string val)
		{
			using (Settings settings = new Settings(config))
			{
				return settings.GetData("E4C9AB0B1E954fe8A4839E8540290204I", "E4C9AB0B1E954fe8A4839E8540290205N", "E4C9AB0B1E954fe8A4839E8540290203K", val);
			}
		}

		public byte[] GetBinaryData(string userID, string key)
		{
			using (Settings settings = new Settings(config))
			{
				return settings.GetBinaryData(userID, "", key);
			}
		}

		public byte[] GetBinaryTemporaryData(string userID, string groupName, string key)
		{
			using (Settings settings = new Settings(config))
			{
				return settings.GetBinaryTemporaryData(userID, "", key);
			}
		}

		public byte[] GetBinaryData(string userID, string groupName, string key)
		{
			using (Settings settings = new Settings(config))
			{
				return settings.GetBinaryData(userID, groupName, key);
			}
		}

		public DataSet GetSettingsList(string userID, string groupName)
		{
			using (Settings settings = new Settings(config))
			{
				return settings.GetSettingsList(userID, groupName);
			}
		}

		public DataSet GetSettingsListData(string userID, string groupName)
		{
			using (Settings settings = new Settings(config))
			{
				return settings.GetSettingsListData(userID, groupName);
			}
		}

		public bool DeleteSetting(string key, string userID, string groupName)
		{
			using (Settings settings = new Settings(config))
			{
				return settings.DeleteSetting(key, userID, groupName);
			}
		}

		public bool DeleteSettingTemporary(string key, string userID, string groupName)
		{
			using (Settings settings = new Settings(config))
			{
				return settings.DeleteSettingTemporary(key, userID, groupName);
			}
		}

		public string GetTempTransactionByKey(string key)
		{
			using (Settings settings = new Settings(config))
			{
				return settings.GetTempTransactionByKey(key);
			}
		}

		public int GetTemporaryAutoKeyID(string key)
		{
			using (Settings settings = new Settings(config))
			{
				return settings.GetTemporaryAutoKeyID(key);
			}
		}
	}
}
