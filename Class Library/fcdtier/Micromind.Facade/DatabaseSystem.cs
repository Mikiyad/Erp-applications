using Micromind.Common.Data;
using Micromind.Common.Interfaces;
using Micromind.Data;
using System;
using System.Data;
using System.IO;
using System.Reflection;

namespace Micromind.Facade
{
	public sealed class DatabaseSystem : MarshalByRefObject, IDatabaseSystem, IDisposable
	{
		private Config config;

		public DatabaseSystem(Config config)
		{
			this.config = config;
		}

		public void Dispose()
		{
		}

		public bool CreateNewCompany(string companyName, string destinationFileName, string databaseName)
		{
			using (Databases databases = new Databases(config))
			{
				return databases.CreateNewCompany(companyName, destinationFileName, databaseName);
			}
		}

		public bool IsTrialLimitReached()
		{
			using (Databases databases = new Databases(config))
			{
				return databases.IsTrialLimitReached();
			}
		}

		public bool ChangeUserPassword(string oldPassword, string newPassword, string loginName)
		{
			using (Databases databases = new Databases(config))
			{
				return databases.ChangeUserPassword(oldPassword, newPassword, loginName);
			}
		}

		public bool RemoveUser(string adminLogin, string adminPassword, string loginName)
		{
			using (Databases databases = new Databases(config))
			{
				return databases.RemoveUser(adminLogin, adminPassword, loginName);
			}
		}

		public bool DetachDatabase(string adminLogin, string adminPassword, string databaseName)
		{
			using (Databases databases = new Databases(config))
			{
				return databases.DetachDatabase(adminLogin, adminPassword, databaseName);
			}
		}

		public bool BackupDatabaseToDisk(string fileName, string description, string databaseName, string serverName, string adminLogin, string adminPassword)
		{
			using (Databases databases = new Databases(config))
			{
				return databases.BackupDatabaseToDisk(fileName, description, databaseName, serverName, adminLogin, adminPassword);
			}
		}

		public bool RestoreDatabaseToDisk(string fileName, string destinationFileName, string databaseName, bool replace)
		{
			using (Databases databases = new Databases(config))
			{
				return databases.RestoreDatabaseToDisk(fileName, destinationFileName, databaseName, replace);
			}
		}

		public DataSet GetDatabases(string serverName, string adminUser, string adminPass, int i)
		{
			using (Databases databases = new Databases(config))
			{
				return databases.GetDatabases(serverName, adminUser, adminPass);
			}
		}

		public DataSet GetDatabases(string serverName, string userName, string password)
		{
			using (Databases databases = new Databases(config))
			{
				return databases.GetDatabases(serverName, userName, password);
			}
		}

		public string GetCompanyName(string databaseName, string serverName, string userName, string password)
		{
			using (Databases databases = new Databases(config))
			{
				return databases.GetCompanyName(databaseName, serverName, userName, password);
			}
		}

		public bool IsCorrectDB()
		{
			using (Databases databases = new Databases(config))
			{
				return databases.IsCorrectDB();
			}
		}

		public bool IsCorrectDBDataVersion()
		{
			using (Databases databases = new Databases(config))
			{
				return databases.IsCorrectDBDataVersion();
			}
		}

		public DataSet GetPendingDataPatches()
		{
			using (Databases databases = new Databases(config))
			{
				return databases.GetPendingDataPatches();
			}
		}

		public bool HasPendingDataPatches()
		{
			using (Databases databases = new Databases(config))
			{
				return databases.HasPendingDataPatches();
			}
		}

		public bool ExecuteDataPatch(string patchID)
		{
			using (Databases databases = new Databases(config))
			{
				return databases.ExecuteDataPatch(patchID);
			}
		}

		public bool IsCorrectDBVersion()
		{
			using (Databases databases = new Databases(config))
			{
				return databases.IsCorrectDBVersion();
			}
		}

		public string GetCurrentDBVersion()
		{
			using (Databases databases = new Databases(config))
			{
				return databases.GetCurrentDBVersion();
			}
		}

		public string GetRequiredDBVersion()
		{
			return Databases.RequiredDBVersion;
		}

		public bool UpgradeDatabase(string databaseName, FileStream dacPackageFile, string userName, string password, bool backup)
		{
			using (Databases databases = new Databases(config))
			{
				return databases.UpgradeDatabase(databaseName, dacPackageFile, userName, password, backup);
			}
		}

		public bool UpgradeDatabaseData(string databaseName, string userName, string password)
		{
			using (Databases databases = new Databases(config))
			{
				return databases.UpgradeDatabaseData(databaseName, userName, password);
			}
		}

		public bool ScheduleDatabaseBackup(string jobName, string scheduleName, string serverName, string dbName, string loginName, string password, int freqType, int freqInterval, int freqRecurrenceFactor, DateTime startDate, DateTime startTime, DateTime endDate, bool hasEndDate, string backupPath)
		{
			using (Databases databases = new Databases(config))
			{
				return databases.ScheduleDatabaseBackup(jobName, scheduleName, serverName, dbName, loginName, password, freqType, freqInterval, freqRecurrenceFactor, startDate, startTime, endDate, hasEndDate, backupPath);
			}
		}

		public bool IsDBExist(string dbName)
		{
			using (Databases databases = new Databases(config))
			{
				return databases.IsDBExist(dbName);
			}
		}

		public DateTime GetTableLastDateTimeStamp(params string[] tablesName)
		{
			using (Databases databases = new Databases(config))
			{
				return databases.GetTableLastDateTimeStamp(tablesName);
			}
		}

		public string GetMachineName()
		{
			using (Databases databases = new Databases(config))
			{
				return databases.GetMachineName();
			}
		}

		public bool ExistDatabase(string serverName, string databaseName, string userName, string password)
		{
			using (Databases databases = new Databases(config))
			{
				return databases.ExistDatabase(serverName, databaseName, userName, password);
			}
		}

		public bool HasDatabaseAccessRight(string serverName, string databaseName, string loginName, string password)
		{
			using (Databases databases = new Databases(config))
			{
				return databases.HasDatabaseAccessRight(serverName, databaseName, loginName, password);
			}
		}

		public string GetCurrentDBPath()
		{
			using (Databases databases = new Databases(config))
			{
				return databases.GetCurrentDBPath();
			}
		}

		public string GetDBPath(string dbName)
		{
			using (Databases databases = new Databases(config))
			{
				return databases.GetDBPath(dbName);
			}
		}

		public object[] GetNextFieldsID(string tableName, string fieldID, string fieldIDName, object fieldNameValue, uint nextCount)
		{
			using (Databases databases = new Databases(config))
			{
				return databases.GetNextFieldsID(tableName, fieldID, fieldIDName, fieldNameValue, nextCount);
			}
		}

		public object[] GetNextFieldsID(string tableName, string fieldIDName, object fieldNameValue)
		{
			using (Databases databases = new Databases(config))
			{
				return databases.GetNextFieldsID(tableName, fieldIDName, fieldIDName, fieldNameValue, 1u);
			}
		}

		public object[] GetNextFieldsID(string tableName, string fieldIDName, object fieldNameValue, string fieldIDName2, object fieldNameValue2)
		{
			using (Databases databases = new Databases(config))
			{
				return databases.GetNextFieldsID(tableName, fieldIDName, fieldIDName, fieldNameValue, fieldIDName2, fieldNameValue2, "", "", 1u);
			}
		}

		public object[] GetNextFieldsID(string tableName, string fieldIDName, object fieldNameValue, string fieldIDName2, object fieldNameValue2, string fieldIDName3, object fieldNameValue3)
		{
			using (Databases databases = new Databases(config))
			{
				return databases.GetNextFieldsID(tableName, fieldIDName, fieldIDName, fieldNameValue, fieldIDName2, fieldNameValue2, fieldIDName3, fieldNameValue3, 1u);
			}
		}

		public object[] GetPreviousFieldsID(string tableName, string fieldID, string fieldIDName, object fieldNameValue, uint prevCount)
		{
			using (Databases databases = new Databases(config))
			{
				return databases.GetPreviousFieldsID(tableName, fieldID, fieldIDName, fieldNameValue, prevCount);
			}
		}

		public object[] GetPreviousFieldsID(string tableName, string fieldIDName, object fieldNameValue)
		{
			using (Databases databases = new Databases(config))
			{
				return databases.GetPreviousFieldsID(tableName, fieldIDName, fieldIDName, fieldNameValue, 1u);
			}
		}

		public object[] GetPreviousFieldsID(string tableName, string fieldIDName, object fieldNameValue, string fieldIDName2, object fieldIDNameValue2)
		{
			using (Databases databases = new Databases(config))
			{
				return databases.GetPreviousFieldsID(tableName, fieldIDName, fieldIDName, fieldNameValue, fieldIDName2, fieldIDNameValue2, "", "", 1u);
			}
		}

		public object[] GetPreviousFieldsID(string tableName, string fieldIDName, object fieldNameValue, string fieldIDName2, object fieldIDNameValue2, string fieldIDName3, object fieldIDNameValue3)
		{
			using (Databases databases = new Databases(config))
			{
				return databases.GetPreviousFieldsID(tableName, fieldIDName, fieldIDName, fieldNameValue, fieldIDName2, fieldIDNameValue2, fieldIDName3, fieldIDNameValue3, 1u);
			}
		}

		public object[] GetNextFieldsID(string tableName, string fieldID, string fieldIDName, object fieldNameValue, string field2Name, object field2Value, uint nextCount)
		{
			using (Databases databases = new Databases(config))
			{
				return databases.GetNextFieldsID(tableName, fieldID, fieldIDName, fieldNameValue, field2Name, field2Value, "", "", nextCount);
			}
		}

		public object[] GetNextFieldsID(string tableName, string fieldID, string fieldIDName, object fieldNameValue, string field2Name, object field2Value, string field3Name, object field3Value)
		{
			using (Databases databases = new Databases(config))
			{
				return databases.GetNextFieldsID(tableName, fieldID, fieldIDName, fieldNameValue, field2Name, field2Value, field3Name, field3Value, 1u);
			}
		}

		public object[] GetPreviousFieldsID(string tableName, string fieldID, string fieldIDName, object fieldNameValue, string field2Name, object field2Value, uint prevCount)
		{
			using (Databases databases = new Databases(config))
			{
				return databases.GetPreviousFieldsID(tableName, fieldID, fieldIDName, fieldNameValue, field2Name, field2Value, "", "", prevCount);
			}
		}

		public object[] GetPreviousFieldsID(string tableName, string fieldID, string fieldIDName, object fieldNameValue, string field2Name, object field2Value, string field3Name, object field3Value, uint prevCount)
		{
			using (Databases databases = new Databases(config))
			{
				return databases.GetPreviousFieldsID(tableName, fieldID, fieldIDName, fieldNameValue, field2Name, field2Value, field3Name, field3Value, prevCount);
			}
		}

		public object GetLastFieldsID(string tableName, string fieldIDName)
		{
			using (Databases databases = new Databases(config))
			{
				return databases.GetMaxFieldID(tableName, fieldIDName, "", "", "", "");
			}
		}

		public object GetLastFieldsID(string tableName, string fieldIDName, string fieldIDName2, string fieldIDNameValue2)
		{
			using (Databases databases = new Databases(config))
			{
				return databases.GetMaxFieldID(tableName, fieldIDName, fieldIDName2, fieldIDNameValue2, "", "");
			}
		}

		public object GetLastFieldsID(string tableName, string fieldIDName, string fieldIDName2, string fieldIDNameValue2, string fieldIDName3, string fieldIDNameValue3)
		{
			using (Databases databases = new Databases(config))
			{
				return databases.GetMaxFieldID(tableName, fieldIDName, fieldIDName2, fieldIDNameValue2, fieldIDName3, fieldIDNameValue3);
			}
		}

		public object GetFirstFieldsID(string tableName, string fieldIDName)
		{
			using (Databases databases = new Databases(config))
			{
				return databases.GetMinFieldID(tableName, fieldIDName, "", "", "", "");
			}
		}

		public object GetFirstFieldsID(string tableName, string fieldIDName, string fieldIDName2, string fieldIDNameValue2)
		{
			using (Databases databases = new Databases(config))
			{
				return databases.GetMinFieldID(tableName, fieldIDName, fieldIDName2, fieldIDNameValue2, "", "");
			}
		}

		public object GetFirstFieldsID(string tableName, string fieldIDName, string fieldIDName2, string fieldIDNameValue2, string fieldIDName3, string fieldIDNameValue3)
		{
			using (Databases databases = new Databases(config))
			{
				return databases.GetMinFieldID(tableName, fieldIDName, fieldIDName2, fieldIDNameValue2, fieldIDName3, fieldIDNameValue3);
			}
		}

		public string GetServerPath()
		{
			return Path.GetDirectoryName(Assembly.GetExecutingAssembly().GetModules()[0].FullyQualifiedName);
		}

		public string GetDefaultBackupDir()
		{
			string text = GetServerPath() + Path.DirectorySeparatorChar.ToString() + "Backup";
			if (!Directory.Exists(text))
			{
				Directory.CreateDirectory(text);
			}
			return text;
		}

		public DatabaseData GetTableUserStats(string tableName, string tableFieldName, object tableFieldID)
		{
			using (Databases databases = new Databases(config))
			{
				return databases.GetTableUserStats(tableName, tableFieldName, tableFieldID);
			}
		}

		public bool ExistFieldValue(string tableName, string fieldName, string fieldValue)
		{
			using (Databases databases = new Databases(config))
			{
				return databases.ExistFieldValue(tableName, fieldName, fieldValue);
			}
		}

		public string FindDocumentByNumber(string tableName, string fieldName, string sysDocID, string numberQuery)
		{
			using (Databases databases = new Databases(config))
			{
				return databases.FindDocumentByNumber(tableName, fieldName, sysDocID, "", null, numberQuery);
			}
		}

		public string FindDocumentByNumber(string tableName, string fieldName, string sysDocID, string filterFieldName, object filterFieldValue, string numberQuery)
		{
			using (Databases databases = new Databases(config))
			{
				return databases.FindDocumentByNumber(tableName, fieldName, sysDocID, filterFieldName, filterFieldValue, numberQuery);
			}
		}

		public DataSet GetComboRowByID(string tableName, string idFieldName, string idFieldValue, string nameFieldName)
		{
			using (Databases databases = new Databases(config))
			{
				return databases.GetComboRowByID(tableName, idFieldName, idFieldValue, "", "", nameFieldName);
			}
		}

		public DataSet GetComboRowByID(string tableName, string idFieldName, string idFieldValue, string idFieldName2, string idFieldValue2, string nameFieldName)
		{
			using (Databases databases = new Databases(config))
			{
				return databases.GetComboRowByID(tableName, idFieldName, idFieldValue, idFieldName2, idFieldValue2, nameFieldName);
			}
		}

		public object GetFieldValue(string tableName, string fieldName, string idFieldName, object idFieldValue)
		{
			using (Databases databases = new Databases(config))
			{
				return databases.GetFieldValue(tableName, fieldName, idFieldName, idFieldValue, null);
			}
		}

		public object GetFieldValue(string tableName, string requiredFieldName, string idFieldName, object idFieldValue, string checkFieldName, object checkFieldValue)
		{
			using (Databases databases = new Databases(config))
			{
				return databases.GetFieldValue(tableName, requiredFieldName, idFieldName, idFieldValue, checkFieldName, checkFieldValue, null);
			}
		}

		public object GetFieldValue(string tableName, string requiredFieldName, string idFieldName, object idFieldValue, string checkFieldName, object checkFieldValue, string checkFieldName1, object checkFieldValue1)
		{
			using (Databases databases = new Databases(config))
			{
				return databases.GetFieldValue(tableName, requiredFieldName, idFieldName, idFieldValue, checkFieldName, checkFieldValue, checkFieldName1, checkFieldValue1, null);
			}
		}

		public DataSet GetDataByFields(string tableName, string idFieldName, string idFieldValue, params string[] columns)
		{
			using (Databases databases = new Databases(config))
			{
				return databases.GetDataByFields(tableName, idFieldName, idFieldValue, columns);
			}
		}

		public bool AttachDatabase(string databaseFileName, string databaseName)
		{
			using (Databases databases = new Databases(config))
			{
				return databases.AttachDatabase(databaseFileName, databaseName);
			}
		}

		public bool DetachDatabase(string databaseName)
		{
			using (Databases databases = new Databases(config))
			{
				return databases.DetachDatabase(databaseName);
			}
		}

		public bool ExistFieldValue(string tableName, string fieldName, string fieldValue, string fieldName2, object fieldValue2)
		{
			using (Databases databases = new Databases(config))
			{
				return databases.ExistFieldValue(tableName, fieldName, fieldValue, fieldName2, fieldValue2);
			}
		}

		public bool ExistFieldValue(string tableName1, string tableName2, string fieldName, string fieldValue, string fieldName2, object fieldValue2, string fieldName3, object fieldValue3)
		{
			using (Databases databases = new Databases(config))
			{
				return databases.ExistFieldValue(tableName1, tableName2, fieldName, fieldValue, fieldName2, fieldValue2, fieldName3, fieldValue3);
			}
		}

		public bool IsUserAllowedToConnect(string userID)
		{
			using (Databases databases = new Databases(config))
			{
				return databases.IsUserAllowedToConnect(userID);
			}
		}

		public void ChangeDatabaseName(string databaseName)
		{
			using (Databases databases = new Databases(config))
			{
				databases.ChangeDatabaseName(databaseName);
			}
		}

		public int UpdateFieldValue(string tableName, string fieldName, object fieldValue, Type fieldType, string idFieldName, object idFieldValue)
		{
			using (Databases databases = new Databases(config))
			{
				return databases.UpdateFieldValue(tableName, fieldName, fieldValue, fieldType, idFieldName, idFieldValue, updateUserDetails: false, null);
			}
		}

		public int UpdateFieldValue(string tableName, string updateFieldName, object updateFieldValue, string checkFieldName1, object checkFieldValue1, string checkFieldName2, object checkFieldValue2, string checkFieldName3, object checkFieldValue3)
		{
			using (Databases databases = new Databases(config))
			{
				return databases.UpdateFieldValue(tableName, updateFieldName, updateFieldValue, checkFieldName1, checkFieldValue1, checkFieldName2, checkFieldValue2, checkFieldName3, checkFieldValue3, updateUserDetails: false, null);
			}
		}

		public int PerformExecuteNonQuery(string exp)
		{
			using (Databases databases = new Databases(config))
			{
				return databases.PerformExecuteNonQuery(exp);
			}
		}

		public object PerformExecuteScalar(string exp)
		{
			using (Databases databases = new Databases(config))
			{
				return databases.PerformExecuteScalar(exp);
			}
		}

		public bool CanConnect()
		{
			using (Databases databases = new Databases(config))
			{
				return databases.CanConnect();
			}
		}

		public object GetLastFieldsIDByCardSecurity(string tableName, string fieldIDName)
		{
			using (Databases databases = new Databases(config))
			{
				return databases.GetMaxFieldIDByCardSecurity(tableName, fieldIDName, "", "", "", "");
			}
		}

		public object GetFirstFieldsIDByCardSecurity(string tableName, string fieldIDName)
		{
			using (Databases databases = new Databases(config))
			{
				return databases.GetMinFieldIDByCardSecurity(tableName, fieldIDName, "", "", "", "");
			}
		}

		public object[] GetPreviousFieldsIDByCardSecurity(string tableName, string fieldIDName, object fieldNameValue)
		{
			using (Databases databases = new Databases(config))
			{
				return databases.GetPreviousFieldsIDByCardSecurity(tableName, fieldIDName, fieldIDName, fieldNameValue, 1u);
			}
		}

		public object[] GetNextFieldsIDByCardSecurity(string tableName, string fieldIDName, object fieldNameValue)
		{
			using (Databases databases = new Databases(config))
			{
				return databases.GetNextFieldsIDByCardSecurity(tableName, fieldIDName, fieldIDName, fieldNameValue, 1u);
			}
		}
	}
}
