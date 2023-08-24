using System.Data;

namespace Micromind.Common.Interfaces
{
	public interface ISettingSystem
	{
		bool SaveSetting(string id, string appName, string key, string val, string data);

		bool SaveSetting(string userID, string key, object value);

		bool SaveSettingStream(string key, string userID, byte[] data);

		bool SaveSettingStreamForDashBoard(string key, string groupName, string userID, byte[] data, string sysDocID, string voucherID, int autoKeyID, DataSet ds, bool isNewRecord);

		bool SaveSettingStream(string key, string userID, string groupName, byte[] data);

		object GetData(string id, string appName, string key, string val, object defaultValue);

		object GetUserSetting(string userID, string key, object defaultValue);

		object GetGlobalData(string val);

		bool SaveGlobalSetting(string val, string data);

		byte[] GetBinaryData(string userID, string key);

		byte[] GetBinaryTemporaryData(string userID, string groupName, string key);

		byte[] GetBinaryData(string userID, string groupName, string key);

		DataSet GetSettingsList(string userID, string groupName);

		DataSet GetSettingsListData(string userID, string groupName);

		bool DeleteSetting(string key, string userID, string groupName);

		bool DeleteSettingTemporary(string key, string userID, string groupName);

		string GetTempTransactionByKey(string key);

		int GetTemporaryAutoKeyID(string key);
	}
}
