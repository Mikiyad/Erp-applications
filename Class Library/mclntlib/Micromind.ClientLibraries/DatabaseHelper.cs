using System;
using System.Text;

namespace Micromind.ClientLibraries
{
	public class DatabaseHelper
	{
		public static bool IsDBExist(string instanceName, string dbName, string userName, string password)
		{
			try
			{
				return Factory.IsDBExist(instanceName, dbName, userName, GetEncyptedPassword(password));
			}
			catch
			{
				throw;
			}
		}

		public static string GetEncyptedPassword(string password)
		{
			try
			{
				return Factory.Encrypt(password);
			}
			catch (Exception e)
			{
				ErrorHelper.ProcessError(e);
				return null;
			}
		}

		public static bool SaveCompany(string companyName, string instanceName, string databaseName, string userName, string portNumber)
		{
			try
			{
				RegistryHelper registryHelper = new RegistryHelper("Companies", writable: true);
				StringBuilder stringBuilder = new StringBuilder();
				stringBuilder.Append(instanceName).Append(";");
				stringBuilder.Append(databaseName).Append(";");
				stringBuilder.Append(userName).Append(";");
				stringBuilder.Append(portNumber).Append(";");
				registryHelper.SetValue(companyName, stringBuilder.ToString());
				registryHelper.Dispose();
				return true;
			}
			catch (Exception ex)
			{
				ErrorHelper.WarningMessage("Unable to save company.", ex.Message);
				return false;
			}
		}

		public static void RemoveCompanyHistory(string companyName)
		{
			try
			{
				RegistryHelper registryHelper = new RegistryHelper("Companies", writable: true);
				registryHelper.DeleteValue(companyName);
				registryHelper.DeleteSubKey(companyName);
				registryHelper.Dispose();
			}
			catch
			{
			}
		}

		public static string GetNextID(string tableName, string fieldName, string currentID)
		{
			try
			{
				object[] nextFieldsID = Factory.DatabaseSystem.GetNextFieldsID(tableName, fieldName, currentID);
				if (nextFieldsID != null && nextFieldsID.Length != 0)
				{
					return nextFieldsID[0].ToString();
				}
				return "";
			}
			catch (Exception e)
			{
				ErrorHelper.ProcessError(e);
				return "";
			}
		}

		public static string GetNextID(string tableName, string fieldName1, string fieldValue1, string fieldName2, string fieldValue2)
		{
			try
			{
				object[] nextFieldsID = Factory.DatabaseSystem.GetNextFieldsID(tableName, fieldName1, fieldValue1, fieldName2, fieldValue2);
				if (nextFieldsID != null && nextFieldsID.Length != 0)
				{
					return nextFieldsID[0].ToString();
				}
				return "";
			}
			catch (Exception e)
			{
				ErrorHelper.ProcessError(e);
				return "";
			}
		}

		public static string GetNextID(string tableName, string fieldName1, string fieldValue1, string fieldName2, string fieldValue2, string fieldName3, object fieldValue3)
		{
			try
			{
				object[] nextFieldsID = Factory.DatabaseSystem.GetNextFieldsID(tableName, fieldName1, fieldValue1, fieldName2, fieldValue2, fieldName3, fieldValue3);
				if (nextFieldsID != null && nextFieldsID.Length != 0)
				{
					return nextFieldsID[0].ToString();
				}
				return "";
			}
			catch (Exception e)
			{
				ErrorHelper.ProcessError(e);
				return "";
			}
		}

		public static string GetPreviousID(string tableName, string fieldName, string currentID)
		{
			try
			{
				object[] previousFieldsID = Factory.DatabaseSystem.GetPreviousFieldsID(tableName, fieldName, currentID);
				if (previousFieldsID != null && previousFieldsID.Length != 0)
				{
					return previousFieldsID[0].ToString();
				}
				return "";
			}
			catch (Exception e)
			{
				ErrorHelper.ProcessError(e);
				return "";
			}
		}

		public static string GetPreviousID(string tableName, string fieldName1, string fieldValue1, string fieldName2, string fieldValue2)
		{
			try
			{
				object[] previousFieldsID = Factory.DatabaseSystem.GetPreviousFieldsID(tableName, fieldName1, fieldValue1, fieldName2, fieldValue2);
				if (previousFieldsID != null && previousFieldsID.Length != 0)
				{
					return previousFieldsID[0].ToString();
				}
				return "";
			}
			catch (Exception e)
			{
				ErrorHelper.ProcessError(e);
				return "";
			}
		}

		public static string GetPreviousID(string tableName, string fieldName1, string fieldValue1, string fieldName2, string fieldValue2, string fieldName3, object fieldValue3)
		{
			try
			{
				object[] previousFieldsID = Factory.DatabaseSystem.GetPreviousFieldsID(tableName, fieldName1, fieldValue1, fieldName2, fieldValue2, fieldName3, fieldValue3);
				if (previousFieldsID != null && previousFieldsID.Length != 0)
				{
					return previousFieldsID[0].ToString();
				}
				return "";
			}
			catch (Exception e)
			{
				ErrorHelper.ProcessError(e);
				return "";
			}
		}

		public static string GetFirstID(string tableName, string fieldName)
		{
			try
			{
				object firstFieldsID = Factory.DatabaseSystem.GetFirstFieldsID(tableName, fieldName);
				if (firstFieldsID != null)
				{
					return firstFieldsID.ToString();
				}
				return "";
			}
			catch (Exception e)
			{
				ErrorHelper.ProcessError(e);
				return "";
			}
		}

		public static string GetFirstID(string tableName, string fieldName, string fieldName2, string fieldValue2)
		{
			try
			{
				object firstFieldsID = Factory.DatabaseSystem.GetFirstFieldsID(tableName, fieldName, fieldName2, fieldValue2);
				if (firstFieldsID != null)
				{
					return firstFieldsID.ToString();
				}
				return "";
			}
			catch (Exception e)
			{
				ErrorHelper.ProcessError(e);
				return "";
			}
		}

		public static string GetFirstID(string tableName, string fieldName, string fieldName2, string fieldValue2, string fieldName3, object fieldValue3)
		{
			try
			{
				object firstFieldsID = Factory.DatabaseSystem.GetFirstFieldsID(tableName, fieldName, fieldName2, fieldValue2, fieldName3, fieldValue3.ToString());
				if (firstFieldsID != null)
				{
					return firstFieldsID.ToString();
				}
				return "";
			}
			catch (Exception e)
			{
				ErrorHelper.ProcessError(e);
				return "";
			}
		}

		public static string GetLastID(string tableName, string fieldName)
		{
			try
			{
				object lastFieldsID = Factory.DatabaseSystem.GetLastFieldsID(tableName, fieldName);
				if (lastFieldsID != null)
				{
					return lastFieldsID.ToString();
				}
				return "";
			}
			catch (Exception e)
			{
				ErrorHelper.ProcessError(e);
				return "";
			}
		}

		public static string GetLastID(string tableName, string fieldName, string fieldName2, string fieldValue2)
		{
			try
			{
				object lastFieldsID = Factory.DatabaseSystem.GetLastFieldsID(tableName, fieldName, fieldName2, fieldValue2);
				if (lastFieldsID != null)
				{
					return lastFieldsID.ToString();
				}
				return "";
			}
			catch (Exception e)
			{
				ErrorHelper.ProcessError(e);
				return "";
			}
		}

		public static string GetLastID(string tableName, string fieldName, string fieldName2, string fieldValue2, string fieldName3, object fieldValue3)
		{
			try
			{
				object lastFieldsID = Factory.DatabaseSystem.GetLastFieldsID(tableName, fieldName, fieldName2, fieldValue2, fieldName3, fieldValue3.ToString());
				if (lastFieldsID != null)
				{
					return lastFieldsID.ToString();
				}
				return "";
			}
			catch (Exception e)
			{
				ErrorHelper.ProcessError(e);
				return "";
			}
		}

		public static string GetLastIDByCardSecurity(string tableName, string fieldName)
		{
			try
			{
				object lastFieldsIDByCardSecurity = Factory.DatabaseSystem.GetLastFieldsIDByCardSecurity(tableName, fieldName);
				if (lastFieldsIDByCardSecurity != null)
				{
					return lastFieldsIDByCardSecurity.ToString();
				}
				return "";
			}
			catch (Exception e)
			{
				ErrorHelper.ProcessError(e);
				return "";
			}
		}

		public static string GetFirstIDByCardSecurity(string tableName, string fieldName)
		{
			try
			{
				object firstFieldsIDByCardSecurity = Factory.DatabaseSystem.GetFirstFieldsIDByCardSecurity(tableName, fieldName);
				if (firstFieldsIDByCardSecurity != null)
				{
					return firstFieldsIDByCardSecurity.ToString();
				}
				return "";
			}
			catch (Exception e)
			{
				ErrorHelper.ProcessError(e);
				return "";
			}
		}

		public static string GetPreviousIDByCardSecurity(string tableName, string fieldName, string currentID)
		{
			try
			{
				object[] previousFieldsIDByCardSecurity = Factory.DatabaseSystem.GetPreviousFieldsIDByCardSecurity(tableName, fieldName, currentID);
				if (previousFieldsIDByCardSecurity != null && previousFieldsIDByCardSecurity.Length != 0)
				{
					return previousFieldsIDByCardSecurity[0].ToString();
				}
				return "";
			}
			catch (Exception e)
			{
				ErrorHelper.ProcessError(e);
				return "";
			}
		}

		public static string GetNextIDByCardSecurity(string tableName, string fieldName, string currentID)
		{
			try
			{
				object[] nextFieldsIDByCardSecurity = Factory.DatabaseSystem.GetNextFieldsIDByCardSecurity(tableName, fieldName, currentID);
				if (nextFieldsIDByCardSecurity != null && nextFieldsIDByCardSecurity.Length != 0)
				{
					return nextFieldsIDByCardSecurity[0].ToString();
				}
				return "";
			}
			catch (Exception e)
			{
				ErrorHelper.ProcessError(e);
				return "";
			}
		}
	}
}
