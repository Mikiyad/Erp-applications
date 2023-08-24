using Micromind.Common.Data;
using System;
using System.Data;
using System.Windows.Forms;

namespace Micromind.ClientLibraries
{
	public sealed class Security
	{
		private static SecurityData securityData;

		private static SecurityData UserSecurityData
		{
			get
			{
				if (Global.ConStatus == ConnectionStatus.DisConnected)
				{
					return null;
				}
				return securityData;
			}
		}

		internal static string CurrentUser
		{
			set
			{
				securityData = null;
				securityData = UserSecurityData;
			}
		}

		public static string DefaultSalespersonID
		{
			get
			{
				if (securityData == null || securityData.Tables["Defaults"].Rows.Count == 0)
				{
					return "";
				}
				return securityData.Tables["Defaults"].Rows[0]["DefaultSalesPersonID"].ToString();
			}
		}

		public static string DefaultInventoryLocationID
		{
			get
			{
				if (securityData == null || securityData.Tables["Defaults"].Rows.Count == 0)
				{
					return "";
				}
				return securityData.Tables["Defaults"].Rows[0]["DefaultInventoryLocationID"].ToString();
			}
		}

		public static string DefaultTransactionLocationID
		{
			get
			{
				if (securityData == null || securityData.Tables["Defaults"].Rows.Count == 0)
				{
					return "";
				}
				return securityData.Tables["Defaults"].Rows[0]["DefaultTransactionLocationID"].ToString();
			}
		}

		public static string DefaultTransactionRegisterID
		{
			get
			{
				if (securityData == null || securityData.Tables["Defaults"].Rows.Count == 0)
				{
					return "";
				}
				return securityData.Tables["Defaults"].Rows[0]["DefaultTransactionRegisterID"].ToString();
			}
		}

		private Security()
		{
		}

		internal static void Reset()
		{
			securityData = null;
		}

		private static DataRow GetScreenAreaRow(ScreenAreas screenArea)
		{
			_ = UserSecurityData;
			return null;
		}

		private static DataRow GetScreenAreaRow(int screenID)
		{
			_ = UserSecurityData;
			return null;
		}

		public static bool HasComboAccessRight(ScreenAreas screenArea, bool suppressMessage)
		{
			AccessRigths accessRigths = AccessRigths.NoAccess;
			try
			{
				accessRigths = GetCurrentUserAccessRight(screenArea);
			}
			catch (Exception ex)
			{
				ErrorHelper.WarningMessage(ex);
				return true;
			}
			if (accessRigths == AccessRigths.NoAccess)
			{
				if (!suppressMessage)
				{
					ErrorHelper.StopMessage("No Access Permission", "You do not have access permission to perform this task.", "Please consult your manager.");
				}
				return false;
			}
			return true;
		}

		public static bool HasComboAccessRight(int screenID, bool suppressMessage)
		{
			AccessRigths accessRigths = AccessRigths.NoAccess;
			try
			{
				accessRigths = GetCurrentUserAccessRight(screenID);
			}
			catch (Exception ex)
			{
				ErrorHelper.WarningMessage(ex);
				return true;
			}
			if (accessRigths == AccessRigths.NoAccess)
			{
				if (!suppressMessage)
				{
					ErrorHelper.StopMessage("No Access Permission", "You do not have access permission to perform this task.", "Please consult your manager.");
				}
				return false;
			}
			return true;
		}

		public static bool HasAccessRight(ScreenAreas screenArea, bool suppressMessage)
		{
			return true;
		}

		public static bool HasAccessRight(int screenID, bool suppressMessage)
		{
			return true;
		}

		public static bool HasAccessRight(Form dataForm, bool suppressMessage)
		{
			bool flag = false;
			try
			{
				if (UIRefelector.GetScreenArea(dataForm) == null)
				{
					return true;
				}
				if (flag)
				{
					int screenID = UIRefelector.GetScreenID(dataForm);
					if (screenID != -1)
					{
						return HasAccessRight(screenID, suppressMessage);
					}
					return true;
				}
				return flag;
			}
			catch (Exception ex)
			{
				ErrorHelper.WarningMessage(ex);
				return false;
			}
		}

		public static AccessRigths GetCurrentUserAccessRight(ScreenAreas screenArea)
		{
			return AccessRigths.NoAccess;
		}

		public static AccessRigths GetCurrentUserAccessRight(int screenID)
		{
			return AccessRigths.NoAccess;
		}

		public static AccessRigths GetUserAccessRight(ScreenAreas screenArea)
		{
			return AccessRigths.FullAccess;
		}

		public static bool HasCurrentUserChangeTransactionRight(ScreenAreas screenArea)
		{
			_ = (Global.CurrentUser.ToLower() == "sa");
			return true;
		}

		public static bool HasCurrentUserDeleteTransactionRight(ScreenAreas screenArea)
		{
			_ = (Global.CurrentUser.ToLower() == "sa");
			return true;
		}

		public static bool HasCurrentUserImportRight(ScreenAreas screenArea)
		{
			_ = (Global.CurrentUser.ToLower() == "sa");
			return true;
		}

		public static bool HasCurrentUserExportRight(ScreenAreas screenArea)
		{
			_ = (Global.CurrentUser.ToLower() == "sa");
			return true;
		}

		public static void LoadUserSecurityData()
		{
			try
			{
				if (!(Global.CurrentUser.ToLower() == "sa"))
				{
					securityData = Factory.SecuritySystem.GetUserSecurityData(Global.CurrentUser);
				}
			}
			catch
			{
				throw;
			}
		}

		public static bool IsAllowedSecurityRole(GeneralSecurityRoles role)
		{
			return IsAllowedSecurityRole((int)role);
		}

		public static bool IsAllowedSecurityRole(int roleID)
		{
			if (Global.ConStatus != 0)
			{
				return false;
			}
			if (Global.IsUserAdmin)
			{
				return true;
			}
			if (securityData == null)
			{
				return true;
			}
			DataRow[] array = securityData.GeneralSecurityTable.Select("SecurityRoleID=" + roleID.ToString());
			bool flag = false;
			foreach (DataRow dataRow in array)
			{
				flag |= bool.Parse(dataRow["IsAllowed"].ToString());
			}
			return flag;
		}

		public static int AllowedDays(GeneralSecurityRoles role)
		{
			return AllowedDays((int)role);
		}

		public static int AllowedDays(int roleID)
		{
			int result = 0;
			decimal result2 = default(decimal);
			if (Global.ConStatus != 0 || !Global.IsUserAdmin || securityData != null)
			{
				string text = securityData.GeneralSecurityTable.Rows[0][2].ToString();
				DataRow[] array = securityData.GeneralSecurityTable.Select("SecurityRoleID=" + roleID.ToString() + " AND UserID = '" + text + "'");
				if (array.Length == 0)
				{
					array = securityData.GeneralSecurityTable.Select("SecurityRoleID=" + roleID.ToString(), "UserID DESC");
				}
				foreach (DataRow dataRow in array)
				{
					string a = dataRow["UserID"].ToString();
					if (a != "")
					{
						int.TryParse(dataRow["intVal"].ToString(), out result);
						if (result != 0)
						{
							return result;
						}
					}
					else
					{
						int.TryParse(dataRow["intVal"].ToString(), out result);
					}
					if (a != "")
					{
						decimal.TryParse(dataRow["intVal"].ToString(), out result2);
						result = (int)result2;
						if (result2 != 0m)
						{
							return result;
						}
					}
					else
					{
						decimal.TryParse(dataRow["intVal"].ToString(), out result2);
						result = (int)result2;
						if (result != 0)
						{
							return result;
						}
					}
				}
			}
			return result;
		}

		public static decimal AllowedDiscount(GeneralSecurityRoles role)
		{
			return AllowedDiscount((int)role);
		}

		public static decimal AllowedDiscount(int roleID)
		{
			decimal result = default(decimal);
			if (Global.ConStatus != 0 || !Global.IsUserAdmin || securityData != null)
			{
				string text = securityData.GeneralSecurityTable.Rows[0][2].ToString();
				DataRow[] array = securityData.GeneralSecurityTable.Select("SecurityRoleID=" + roleID.ToString() + " AND UserID = '" + text + "'");
				if (array.Length == 0)
				{
					array = securityData.GeneralSecurityTable.Select("SecurityRoleID=" + roleID.ToString(), "UserID DESC");
				}
				foreach (DataRow dataRow in array)
				{
					if (dataRow["UserID"].ToString() != "")
					{
						decimal.TryParse(dataRow["intVal"].ToString(), out result);
						if (result != 0m)
						{
							return result;
						}
					}
					else
					{
						decimal.TryParse(dataRow["intVal"].ToString(), out result);
					}
				}
			}
			return result;
		}

		public static bool IsAllowedSecurityUser(GeneralSecurityRoles role)
		{
			return IsAllowedSecurityUser((int)role);
		}

		public static bool IsAllowedSecurityUser(int roleID)
		{
			if (Global.ConStatus != 0)
			{
				return false;
			}
			if (Global.IsUserAdmin)
			{
				return true;
			}
			if (securityData == null)
			{
				return true;
			}
			DataRow[] array = securityData.GeneralSecurityTable.Select("SecurityRoleID=" + roleID.ToString() + " AND UserID='" + Global.CurrentUser + "'");
			bool flag = false;
			foreach (DataRow dataRow in array)
			{
				flag |= bool.Parse(dataRow["IsAllowed"].ToString());
			}
			return flag;
		}

		public static MenuAccessRight GetMenuAccessRight(string menuID)
		{
			MenuAccessRight result = default(MenuAccessRight);
			result.Visible = false;
			result.Enable = false;
			if (Global.IsUserAdmin)
			{
				result.Visible = true;
				result.Enable = true;
				return result;
			}
			if (securityData == null)
			{
				return result;
			}
			DataRow[] array = securityData.MenuSecurityTable.Select("MenuID='" + menuID + "'");
			foreach (DataRow dataRow in array)
			{
				result.Enable |= bool.Parse(dataRow["Enable"].ToString());
				result.Visible |= bool.Parse(dataRow["Visible"].ToString());
			}
			return result;
		}

		public static MenuAccessRight GetCustomReportAccessRight(CustomReportTypes reportType, string reportID)
		{
			MenuAccessRight result = default(MenuAccessRight);
			result.Visible = false;
			result.Enable = false;
			if (Global.IsUserAdmin)
			{
				result.Visible = true;
				result.Enable = true;
				return result;
			}
			if (securityData == null)
			{
				return result;
			}
			DataRow[] array = securityData.CustomReportSecurityTable.Select("ReportType = " + (int)reportType + " AND  MenuID = '" + reportID + "'");
			foreach (DataRow dataRow in array)
			{
				if (dataRow["Enable"] != DBNull.Value)
				{
					result.Enable |= bool.Parse(dataRow["Enable"].ToString());
				}
				else
				{
					ref bool enable = ref result.Enable;
					enable = enable;
				}
				if (dataRow["Visible"] != DBNull.Value)
				{
					result.Visible |= bool.Parse(dataRow["Visible"].ToString());
					continue;
				}
				ref bool visible = ref result.Visible;
				visible = visible;
			}
			return result;
		}

		public static MenuAccessRight GetTabPageAccessRight(string tabID)
		{
			MenuAccessRight result = default(MenuAccessRight);
			result.Visible = false;
			result.Enable = false;
			if (Global.IsUserAdmin)
			{
				result.Visible = true;
				return result;
			}
			if (securityData == null)
			{
				return result;
			}
			DataRow[] array = securityData.TabSecurityTable.Select("TabID='" + tabID + "'");
			foreach (DataRow dataRow in array)
			{
				result.Visible |= bool.Parse(dataRow["Visible"].ToString());
			}
			return result;
		}

		public static ScreenAccessRight GetScreenAccessRight(string screenID)
		{
			ScreenAccessRight result = default(ScreenAccessRight);
			result.View = false;
			result.Edit = false;
			result.New = false;
			result.Delete = false;
			DataRow[] array = null;
			if (Global.IsUserAdmin)
			{
				result.View = true;
				result.Edit = true;
				result.New = true;
				result.Delete = true;
				if (Global.IsTrialLimitReached)
				{
					result.Edit = false;
					result.New = false;
					result.Delete = false;
				}
				return result;
			}
			if (securityData == null)
			{
				return result;
			}
			array = securityData.ScreenSecurityTable.Select("ScreenID='" + screenID + "'");
			foreach (DataRow dataRow in array)
			{
				result.View |= bool.Parse(dataRow["ViewRight"].ToString());
				result.Edit |= bool.Parse(dataRow["EditRight"].ToString());
				result.New |= bool.Parse(dataRow["NewRight"].ToString());
				result.Delete |= bool.Parse(dataRow["DeleteRight"].ToString());
			}
			if (Global.IsTrialLimitReached)
			{
				result.Edit = false;
				result.New = false;
				result.Delete = false;
			}
			return result;
		}
	}
}
