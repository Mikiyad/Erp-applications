using Micromind.Common.Data;
using System;
using System.Data;
using System.IO;

namespace Micromind.Common.Interfaces
{
	public interface IDatabaseSystem
	{
		bool CreateNewCompany(string companyName, string destinationFileName, string databaseName);

		bool IsTrialLimitReached();

		bool ChangeUserPassword(string oldPassword, string newPassword, string loginName);

		bool RemoveUser(string adminLogin, string adminPassword, string loginName);

		bool DetachDatabase(string adminLogin, string adminPassword, string databaseName);

		bool BackupDatabaseToDisk(string fileName, string description, string databaseName, string serverName, string adminLogin, string adminPassword);

		bool RestoreDatabaseToDisk(string fileName, string destinationFileName, string databaseName, bool replace);

		DataSet GetPendingDataPatches();

		DataSet GetDatabases(string serverName, string adminUser, string adminPass);

		DataSet GetDatabases(string serverName, string userName, string password, int i);

		string GetCompanyName(string databaseName, string serverName, string userName, string password);

		bool IsCorrectDB();

		bool IsCorrectDBVersion();

		bool IsCorrectDBDataVersion();

		bool ExecuteDataPatch(string patchID);

		bool HasPendingDataPatches();

		string GetCurrentDBVersion();

		string GetRequiredDBVersion();

		bool UpgradeDatabase(string databaseName, FileStream dacPackageFile, string userName, string password, bool backup);

		bool UpgradeDatabaseData(string databaseName, string userName, string password);

		bool ScheduleDatabaseBackup(string jobName, string scheduleName, string serverName, string dbName, string loginName, string password, int freqType, int freqInterval, int freqRecurrenceFactor, DateTime startDate, DateTime startTime, DateTime endDate, bool hasEndDate, string backupPath);

		bool IsDBExist(string dbName);

		DateTime GetTableLastDateTimeStamp(params string[] tablesName);

		string GetMachineName();

		bool ExistDatabase(string serverName, string databaseName, string userName, string password);

		bool HasDatabaseAccessRight(string serverName, string databaseName, string loginName, string password);

		string GetCurrentDBPath();

		string GetDBPath(string dbName);

		object[] GetNextFieldsID(string tableName, string fieldID, string fieldIDName, object fieldNameValue, uint nextCount);

		object[] GetPreviousFieldsID(string tableName, string fieldID, string fieldIDName, object fieldNameValue, uint prevCount);

		object[] GetPreviousFieldsID(string tableName, string fieldIDName, object fieldNameValue, string fieldIDName2, object fieldIDNameValue2, string fieldIDName3, object fieldIDNameValue3);

		object[] GetNextFieldsID(string tableName, string fieldID, string fieldIDName, object fieldNameValue, string field2Name, object field2Value, uint nextCount);

		object[] GetNextFieldsID(string tableName, string fieldIDName, object fieldNameValue);

		object[] GetNextFieldsID(string tableName, string fieldIDName, object fieldNameValue, string fieldIDName2, object fieldNameValue2);

		object[] GetNextFieldsID(string tableName, string fieldIDName, object fieldNameValue, string fieldIDName2, object fieldNameValue2, string fieldIDName3, object fieldNameValue3);

		object[] GetPreviousFieldsID(string tableName, string fieldID, string fieldIDName, object fieldNameValue, string field2Name, object field2Value, uint prevCount);

		object[] GetPreviousFieldsID(string tableName, string fieldIDName, object fieldNameValue);

		object[] GetPreviousFieldsID(string tableName, string fieldIDName, object fieldNameValue, string fieldIDName2, object fieldNameValue2);

		object GetLastFieldsID(string tableName, string fieldIDName);

		object GetLastFieldsID(string tableName, string fieldIDName, string fieldIDName2, string fieldIDNameValue2);

		object GetLastFieldsID(string tableName, string fieldIDName, string fieldIDName2, string fieldIDNameValue2, string fieldIDName3, string fieldIDNameValue3);

		object GetFirstFieldsID(string tableName, string fieldIDName);

		object GetFirstFieldsID(string tableName, string fieldIDName, string fieldIDName2, string fieldIDNameValue2);

		object GetFirstFieldsID(string tableName, string fieldIDName, string fieldIDName2, string fieldIDNameValue2, string fieldIDName3, string fieldIDNameValue3);

		string GetServerPath();

		string GetDefaultBackupDir();

		DatabaseData GetTableUserStats(string tableName, string tableFieldName, object tableFieldID);

		bool ExistFieldValue(string tableName, string fieldName, string fieldValue);

		string FindDocumentByNumber(string tableName, string fieldName, string sysDocID, string numberQuery);

		string FindDocumentByNumber(string tableName, string fieldName, string sysDocID, string filterFieldName, object filterFieldValue, string numberQuery);

		DataSet GetComboRowByID(string tableName, string idFieldName, string idFieldValue, string nameFieldName);

		DataSet GetComboRowByID(string tableName, string idFieldName, string idFieldValue, string idFieldName2, string idFieldValue2, string nameFieldName);

		object GetFieldValue(string tableName, string fieldName, string idFieldName, object idFieldValue);

		object GetFieldValue(string tableName, string requiredFieldName, string idFieldName, object idFieldValue, string checkFieldName, object checkFieldValue);

		object GetFieldValue(string tableName, string requiredFieldName, string idFieldName, object idFieldValue, string checkFieldName, object checkFieldValue, string checkFieldName1, object checkFieldValue1);

		DataSet GetDataByFields(string tableName, string idFieldName, string idFieldValue, params string[] columns);

		bool AttachDatabase(string databaseFileName, string databaseName);

		bool DetachDatabase(string databaseName);

		void ChangeDatabaseName(string databaseName);

		bool ExistFieldValue(string tableName, string fieldName, string fieldValue, string fieldName2, object fieldValue2);

		bool ExistFieldValue(string tableName1, string tableName2, string fieldName, string fieldValue, string fieldName2, object fieldValue2, string fieldName3, object fieldValue3);

		bool IsUserAllowedToConnect(string userID);

		int UpdateFieldValue(string tableName, string fieldName, object fieldValue, Type fieldType, string idFieldName, object idFieldValue);

		int UpdateFieldValue(string tableName, string updateFieldName, object updateFieldValue, string checkFieldName1, object checkFieldValue1, string checkFieldName2, object checkFieldValue2, string checkFieldName3, object checkFieldValue3);

		int PerformExecuteNonQuery(string exp);

		object PerformExecuteScalar(string exp);

		bool CanConnect();

		object GetLastFieldsIDByCardSecurity(string tableName, string fieldIDName);

		object GetFirstFieldsIDByCardSecurity(string tableName, string fieldIDName);

		object[] GetPreviousFieldsIDByCardSecurity(string tableName, string fieldIDName, object fieldNameValue);

		object[] GetNextFieldsIDByCardSecurity(string tableName, string fieldIDName, object fieldNameValue);
	}
}
