using Micromind.Common.Data;
using System;
using System.Collections.Generic;

namespace Micromind.ClientLibraries
{
	public static class UserPreferences
	{
		private static Dictionary<UserOptionsEnum, object> userPreferences = new Dictionary<UserOptionsEnum, object>();

		public static bool OpenMDI
		{
			get
			{
				object value = null;
				userPreferences.TryGetValue(UserOptionsEnum.OpenMDIForms, out value);
				if (value == null)
				{
					return false;
				}
				return bool.Parse(value.ToString());
			}
		}

		public static bool ResizeColumnsToFit
		{
			get
			{
				object value = null;
				userPreferences.TryGetValue(UserOptionsEnum.ResizeListColumnsToFit, out value);
				if (value == null)
				{
					return true;
				}
				return bool.Parse(value.ToString());
			}
		}

		public static bool IgnoreSpaceInComboSearch
		{
			get
			{
				object value = null;
				userPreferences.TryGetValue(UserOptionsEnum.IgnoreSpaceInComboSearch, out value);
				if (value == null)
				{
					return false;
				}
				return bool.Parse(value.ToString());
			}
		}

		public static bool ShowNavigationBar
		{
			get
			{
				object value = null;
				userPreferences.TryGetValue(UserOptionsEnum.ShowNavigationBar, out value);
				if (value == null)
				{
					return true;
				}
				return bool.Parse(value.ToString());
			}
		}

		public static bool PrintDocumentOnLetterHead
		{
			get
			{
				object value = null;
				userPreferences.TryGetValue(UserOptionsEnum.PrintDocumentOnLetterHead, out value);
				if (value == null)
				{
					return true;
				}
				return bool.Parse(value.ToString());
			}
		}

		public static bool PrintReportOnLetterHead
		{
			get
			{
				object value = null;
				userPreferences.TryGetValue(UserOptionsEnum.PrintReportOnLetterHead, out value);
				if (value == null)
				{
					return true;
				}
				return bool.Parse(value.ToString());
			}
		}

		public static void LoadUserPreferences()
		{
			try
			{
				userPreferences = new Dictionary<UserOptionsEnum, object>();
				object userSetting = Factory.SettingSystem.GetUserSetting(Global.CurrentUser, UserOptionsEnum.OpenMDIForms.ToString(), false);
				userPreferences.Add(UserOptionsEnum.OpenMDIForms, userSetting);
				userSetting = Factory.SettingSystem.GetUserSetting(Global.CurrentUser, UserOptionsEnum.ResizeListColumnsToFit.ToString(), true);
				userPreferences.Add(UserOptionsEnum.ResizeListColumnsToFit, userSetting);
				userSetting = Factory.SettingSystem.GetUserSetting(Global.CurrentUser, UserOptionsEnum.ShowNavigationBar.ToString(), true);
				userPreferences.Add(UserOptionsEnum.ShowNavigationBar, userSetting);
				userSetting = Factory.SettingSystem.GetUserSetting(Global.CurrentUser, UserOptionsEnum.PrintDocumentOnLetterHead.ToString(), false);
				userPreferences.Add(UserOptionsEnum.PrintDocumentOnLetterHead, userSetting);
				userSetting = Factory.SettingSystem.GetUserSetting(Global.CurrentUser, UserOptionsEnum.PrintReportOnLetterHead.ToString(), false);
				userPreferences.Add(UserOptionsEnum.PrintReportOnLetterHead, userSetting);
				userSetting = Factory.SettingSystem.GetUserSetting(Global.CurrentUser, UserOptionsEnum.IgnoreSpaceInComboSearch.ToString(), false);
				userPreferences.Add(UserOptionsEnum.IgnoreSpaceInComboSearch, userSetting);
			}
			catch (Exception)
			{
			}
		}

		public static T GetCurrentUserSetting<T>(string key, T defaultValue)
		{
			try
			{
				object userSetting = Factory.SettingSystem.GetUserSetting(Global.CurrentUser, key, defaultValue);
				if (userSetting != null && userSetting.ToString() != "")
				{
					return (T)Convert.ChangeType(userSetting, typeof(T));
				}
				return defaultValue;
			}
			catch
			{
				return defaultValue;
			}
		}

		public static bool SaveCurrentUserSetting(string key, object value)
		{
			try
			{
				return Factory.SettingSystem.SaveSetting(Global.CurrentUser, key, value);
			}
			catch
			{
				throw;
			}
		}
	}
}
