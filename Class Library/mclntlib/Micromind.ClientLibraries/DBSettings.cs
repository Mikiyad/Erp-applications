using System.IO;

namespace Micromind.ClientLibraries
{
	public sealed class DBSettings
	{
		private static string APPNAME = "Axolon";

		private static string KEY = "E4C9AB0B1E954fe8A4839E8540290203";

		public static string ChecksToDepositReminder = "DaysChecksToDepositReminder";

		public static string PaidChecksDueReminder = "DaysPaidChecksDueReminder";

		public static string IsChecksToDepositReminder = "ISChecksToDepositReminder";

		public static string IsPaidChecksDueReminder = "ISPaidChecksDueReminder";

		public static string FixedPriceDecimals = "FixedPriceDecimals";

		public static string VariablePriceDecimals = "VariablePriceDecimals";

		public static string FixedQuantityDecimals = "FixedQuantityDecimals";

		public static string VariableQuantityDecimals = "VariableQuantityDecimals";

		public static string DecimalSymbol = "DecimalSymbol";

		public static string DigitGroupingSymbol = "DigitGroupingSymbol";

		public static string NegativeNumberFormat = "NegativeNumberFormat";

		public static string LeadingZerosFormat = "LeadingZerosFormat";

		public static bool SaveSetting(string id, string name, string key, string val, string data)
		{
			return Factory.SettingSystem.SaveSetting(id, name, key, val, data);
		}

		public static bool SaveSetting(string name, string key, string val, string data)
		{
			return Factory.SettingSystem.SaveSetting(Global.CurrentUser, name, key, val, data);
		}

		public static bool SaveSetting(string key, string val, string data)
		{
			return Factory.SettingSystem.SaveSetting(Global.CurrentUser, APPNAME, key, val, data);
		}

		public static bool SaveCurrentUserSetting(string key, string val)
		{
			return Factory.SettingSystem.SaveSetting(Global.CurrentUser, APPNAME, key, val, null);
		}

		public static bool SaveSetting(string val, string data)
		{
			return Factory.SettingSystem.SaveSetting(Global.CurrentUser, APPNAME, KEY, val, data);
		}

		public static bool SaveSetting(string key, MemoryStream data)
		{
			return Factory.SettingSystem.SaveSetting(Global.CurrentUser, key, data);
		}

		public static object GetSetting(string id, string name, string key, string val, object defaultValue)
		{
			return Factory.SettingSystem.GetData(id, name, key, val, defaultValue);
		}

		public static object GetSetting(string name, string key, string val, object defaultValue)
		{
			return Factory.SettingSystem.GetData(Global.CurrentUser, name, key, val, defaultValue);
		}

		public static object GetSetting(string key, string val)
		{
			return Factory.SettingSystem.GetData(Global.CurrentUser, APPNAME, key, val, null);
		}

		public static object GetSetting(string val)
		{
			return Factory.SettingSystem.GetData(Global.CurrentUser, APPNAME, KEY, val, null);
		}

		public static MemoryStream GetBinarySetting(string key)
		{
			byte[] binaryData = Factory.SettingSystem.GetBinaryData(Global.CurrentUser, key);
			MemoryStream memoryStream = new MemoryStream();
			memoryStream.Write(binaryData, 0, binaryData.Length);
			return memoryStream;
		}

		public static object GetCurrentUserSetting(string key, object defaultValue)
		{
			return Factory.SettingSystem.GetUserSetting(Global.CurrentUser, key, defaultValue);
		}
	}
}
