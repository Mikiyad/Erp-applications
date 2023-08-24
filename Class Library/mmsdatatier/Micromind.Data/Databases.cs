using Micromind.Common;
using Micromind.Common.Data;
using Micromind.Common.Libraries;
using Micromind.Data.Properties;
using Microsoft.SqlServer.Dac;
using Microsoft.SqlServer.Management.Common;
using Microsoft.SqlServer.Management.Smo;
using System;
using System.Collections;
using System.Collections.Specialized;
using System.Data;
using System.Data.SqlClient;
using System.IO;

using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace Micromind.Data
{
	
	
	public sealed class Databases : StoreObject
	{
		private string deviceName = "AxolonBackupDevice";

		private static object syncRoot = new object();

		public static string RequiredDBVersion => Micromind.Data.Properties.Settings.Default.DBVersion;

		public static string RequiredDBDataVersion => Micromind.Data.Properties.Settings.Default.DBDataVersion;

		public Databases(Config config)
			: base(config)
		{
		}

		public bool IsCorrectDB()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("SELECT ").Append("CompanyName");
			stringBuilder.Append(" FROM ").Append("Company");
			try
			{
				if (ExecuteScalar(stringBuilder.ToString()) == null)
				{
					return false;
				}
			}
			catch
			{
				return false;
			}
			return true;
		}

		public bool IsCorrectDB(SqlConnection connection)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("SELECT ").Append("CompanyName");
			stringBuilder.Append(" FROM ").Append("Company");
			try
			{
				if (ExecuteScalar(stringBuilder.ToString(), connection) == null)
				{
					return false;
				}
			}
			catch
			{
				return false;
			}
			return true;
		}

		public bool IsTrialLimitReached()
		{
			try
			{
				string exp = "SELECT COUNT(*) FROM Journal";
				object obj = ExecuteScalar(exp);
				if (obj != null && int.Parse(obj.ToString()) > 1000)
				{
					return true;
				}
				exp = "SELECT COUNT(*) FROM Customer";
				if (obj != null && int.Parse(obj.ToString()) > 50)
				{
					return true;
				}
				exp = "SELECT COUNT(*) FROM Vendor";
				if (obj != null && int.Parse(obj.ToString()) > 50)
				{
					return true;
				}
				exp = "SELECT COUNT(*) FROM Employee";
				if (obj != null && int.Parse(obj.ToString()) > 20)
				{
					return true;
				}
				return false;
			}
			catch
			{
				return false;
			}
		}

		public bool IsCorrectDBVersion()
		{
			return IsCorrectDBVersion(null);
		}

		public bool IsCorrectDBVersion(SqlConnection connection)
		{
			try
			{
				string exp = "SELECT MAX(DBVersion) AS DBVersion  FROM Company";
				object obj = null;
				obj = ((connection != null) ? ExecuteScalar(exp, connection) : ExecuteScalar(exp));
				if (obj == null || obj.ToString().Length == 0)
				{
					return false;
				}
				return obj.ToString() == RequiredDBVersion;
			}
			catch
			{
				return false;
			}
		}

		public bool IsCorrectDBDataVersion()
		{
			return IsCorrectDBDataVersion(null);
		}

		public bool IsCorrectDBDataVersion(SqlConnection connection)
		{
			bool flag = true;
			try
			{
				object obj = null;
				string exp = "SELECT CASE WHEN  EXISTS(SELECT * FROM sys.columns\r\n                            WHERE Name = N'DBDataVersion' AND OBJECT_ID = OBJECT_ID(N'Company'))\r\n                            THEN 1 ELSE 0 END FROM Company ";
				obj = ((connection != null) ? ExecuteScalar(exp, connection) : ExecuteScalar(exp));
				if (int.Parse(obj.ToString()) == 1)
				{
					exp = "SELECT ISNULL(MAX(DBDataVersion),'1.8.10.280') AS DBVersion  FROM Company";
					obj = ((connection != null) ? ExecuteScalar(exp, connection) : ExecuteScalar(exp));
					if (obj != null && obj.ToString().Length != 0)
					{
						string a = obj.ToString();
						flag &= (a == RequiredDBDataVersion);
					}
				}
				return flag;
			}
			catch
			{
				return false;
			}
		}

		public string GetCurrentDBVersion()
		{
			return GetCurrentDBVersion(null);
		}

		public string GetCurrentDBVersion(SqlConnection connection)
		{
			string exp = "SELECT MAX(DBVersion) FROM Company";
			object obj = null;
			obj = ((connection != null) ? ExecuteScalar(exp, connection) : ExecuteScalar(exp));
			if (obj == null || obj.ToString().Length < 7)
			{
				return "1.0.0.0";
			}
			return obj.ToString();
		}

		public DataSet GetPendingDataPatches()
		{
			try
			{
				DataSet dataSet = new DataSet();
				string textCommand = "SELECT PatchID,Status,PatchDescription FROM Data_Patch WHERE ISNULL(Status,1)= 1";
				FillDataSet(dataSet, "Data_Patch", textCommand);
				return dataSet;
			}
			catch
			{
				throw;
			}
		}

		public string GetCurrentDBDataVersion()
		{
			return GetCurrentDBDataVersion(null);
		}

		public string GetCurrentDBDataVersion(SqlConnection connection)
		{
			string exp = "SELECT MAX(ISNULL(DBDataVersion,'1.8.10.28')) FROM Company";
			object obj = null;
			obj = ((connection != null) ? ExecuteScalar(exp, connection) : ExecuteScalar(exp));
			if (obj == null || obj.ToString().Length < 7)
			{
				return "1.0.0.0";
			}
			return obj.ToString();
		}

		private bool DropDatabase(string serverName, string adminLogin, string adminPassword, string databaseName)
		{
			SqlConnection sqlConnection = null;
			try
			{
				sqlConnection = base.DBConfig.GetConnection(serverName, adminLogin, adminPassword);
				if (sqlConnection != null)
				{
					sqlConnection.Open();
					SqlCommand sqlCommand = new SqlCommand("DROP DATABASE " + databaseName, sqlConnection);
					sqlCommand.CommandType = CommandType.Text;
					sqlCommand.ExecuteNonQuery();
				}
			}
			catch (Exception ex)
			{
				throw ex;
			}
			finally
			{
				if (sqlConnection != null)
				{
					sqlConnection.Dispose();
					sqlConnection = null;
				}
			}
			return true;
		}

		public bool DetachDatabase(string adminLogin, string adminPassword, string databaseName)
		{
			if (adminPassword.Length != 0)
			{
				adminPassword = base.DBConfig.ConfigHelper.Cryptor.Decrypt(adminPassword);
			}
			SqlConnection sqlConnection = null;
			try
			{
				sqlConnection = base.DBConfig.GetConnection(base.DBConfig.ServerName, adminLogin, adminPassword);
				if (sqlConnection != null)
				{
					sqlConnection.Open();
					SqlCommand obj = new SqlCommand("sp_detach_db", sqlConnection)
					{
						CommandType = CommandType.StoredProcedure
					};
					SqlParameterCollection parameters = obj.Parameters;
					parameters.Add("@dbname", SqlDbType.NVarChar);
					parameters["@dbname"].Value = databaseName;
					parameters.Add("@skipchecks", SqlDbType.NVarChar);
					parameters["@skipchecks"].Value = true;
					obj.ExecuteNonQuery();
				}
			}
			catch
			{
				throw;
			}
			finally
			{
				if (sqlConnection != null)
				{
					sqlConnection.Dispose();
					sqlConnection = null;
				}
			}
			return true;
		}

		public bool ChangeUserPassword(string oldPassword, string newPassword, string loginName)
		{
			return ChangeUserPassword(oldPassword, newPassword, loginName, isResetting: false);
		}

		internal bool ChangeUserPassword(string oldPassword, string newPassword, string loginName, bool isResetting)
		{
			try
			{
				using (new Security(base.DBConfig))
				{
					base.DBConfig.ResetPassword(loginName, base.DBConfig.ConfigHelper.Cryptor.Decrypt(newPassword));
				}
			}
			catch
			{
			}
			if (loginName.ToLower() == "sa" && !isResetting)
			{
				if (oldPassword.Length != 0)
				{
					oldPassword = base.DBConfig.ConfigHelper.Cryptor.Decrypt(oldPassword);
				}
				if (newPassword.Length != 0)
				{
					newPassword = base.DBConfig.ConfigHelper.Cryptor.Decrypt(newPassword);
				}
				base.DBConfig.ResetPassword(loginName, newPassword);
				SqlConnection sqlConnection = null;
				try
				{
					sqlConnection = base.DBConfig.GetConnection(base.DBConfig.ServerName, loginName, oldPassword, base.DBConfig.DatabaseName);
					sqlConnection.Open();
					SqlCommand obj2 = new SqlCommand("sp_password", sqlConnection)
					{
						CommandType = CommandType.StoredProcedure
					};
					SqlParameterCollection parameters = obj2.Parameters;
					parameters.Add("@old", SqlDbType.NVarChar);
					parameters.Add("@new", SqlDbType.NVarChar);
					parameters.Add("@loginame", SqlDbType.NVarChar);
					if (oldPassword.Length <= 0)
					{
						parameters["@old"].Value = null;
					}
					else
					{
						parameters["@old"].Value = oldPassword;
					}
					parameters["@new"].Value = newPassword;
					parameters["@loginame"].Value = loginName;
					obj2.ExecuteNonQuery();
				}
				catch (Exception ex)
				{
					throw ex;
				}
				finally
				{
					if (sqlConnection != null)
					{
						if (sqlConnection.State == ConnectionState.Open)
						{
							sqlConnection.Close();
						}
						sqlConnection.Dispose();
					}
				}
			}
			return true;
		}

		public bool RemoveUser(string adminLogin, string adminPassword, string loginName)
		{
			if (adminPassword.Length != 0)
			{
				adminPassword = base.DBConfig.ConfigHelper.Cryptor.Decrypt(adminPassword);
			}
			SqlConnection sqlConnection = null;
			try
			{
				sqlConnection = base.DBConfig.GetConnection(base.DBConfig.ServerName, adminLogin, adminPassword, base.DBConfig.DatabaseName);
				if (sqlConnection != null)
				{
					sqlConnection.Open();
					SqlCommand obj = new SqlCommand("sp_revokedbaccess", sqlConnection)
					{
						CommandType = CommandType.StoredProcedure
					};
					SqlParameterCollection parameters = obj.Parameters;
					parameters.Add("@name_in_db", SqlDbType.NVarChar);
					parameters["@name_in_db"].Value = loginName;
					obj.ExecuteNonQuery();
				}
			}
			catch (SqlException ex)
			{
				if (ex.Number != 15008)
				{
					throw ex;
				}
			}
			catch (Exception ex2)
			{
				throw ex2;
			}
			finally
			{
				if (sqlConnection != null)
				{
					if (sqlConnection.State == ConnectionState.Open)
					{
						sqlConnection.Close();
					}
					sqlConnection.Dispose();
				}
			}
			return true;
		}

		private void GetSubdirFiles(string dir, string searchPattern, StringCollection strCol)
		{
			string[] directories = Directory.GetDirectories(dir);
			if (directories.Length != 0)
			{
				string[] array = directories;
				foreach (string text in array)
				{
					string[] files = Directory.GetFiles(text, searchPattern);
					strCol.AddRange(files);
					GetSubdirFiles(text, searchPattern, strCol);
				}
			}
		}

		private void AddPrintTemplates(string templateDir, Config config)
		{
			if (config == null)
			{
				config = base.DBConfig;
			}
			if (Directory.Exists(templateDir))
			{
				StringCollection stringCollection = new StringCollection();
				string[] files;
				try
				{
					files = Directory.GetFiles(templateDir, "*.dat");
				}
				catch (Exception ex)
				{
					throw new ApplicationException(ex.Message + "\nMake sure " + templateDir + " exists.");
				}
				stringCollection.AddRange(files);
				GetSubdirFiles(templateDir, "*.dat", stringCollection);
				new PrintTemplates(config).LoadTemplates(stringCollection, null);
			}
		}

		private string GetAlterDatabaseString(string databaseName)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("ALTER DATABASE ").Append(databaseName);
			stringBuilder.Append(" SET RECOVERY SIMPLE,AUTO_CLOSE ON ,AUTO_SHRINK ON");
			return stringBuilder.ToString();
		}

		private string GetCreateDatabaseString(string databaseName, string dbPath)
		{
			string originalDiectory = StoreConfiguration.OriginalDiectory;
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("Create DATABASE ").Append(databaseName).Append(" ON (NAME='");
			if (dbPath != null && dbPath.Trim().Length > 0)
			{
				originalDiectory = dbPath;
				stringBuilder.Append(databaseName).Append("',FILENAME ='").Append(originalDiectory);
				stringBuilder.Append("',SIZE=");
				stringBuilder.Append("10)");

				string value = dbPath.Replace(".mdf", "_log.ldf");
					//(dbPath.LastIndexOf("_") > 0) ? (dbPath.Substring(0, dbPath.LastIndexOf("_")) + "_log.ldf") : ((dbPath.LastIndexOf(".") <= 0) ? (dbPath + "_log.ldf") : (dbPath.Substring(0, dbPath.LastIndexOf(".")) + "_log.ldf"));
				stringBuilder.Append(" LOG ON (NAME =  '").Append(databaseName + "_log'");
				stringBuilder.Append(",FILENAME = '").Append(value).Append("',");
				stringBuilder.Append("SIZE = 10)");
			}
			else
			{
				originalDiectory = Directory.CreateDirectory(originalDiectory + "\\Data").FullName;
				stringBuilder.Append(databaseName).Append("',FILENAME ='").Append(originalDiectory);
				stringBuilder.Append("\\").Append(databaseName).Append(".mdf'")
					.Append(",SIZE=");
				stringBuilder.Append("10)");
			}
			return stringBuilder.ToString();
		}

		private string[] GetCreateUpdateFileContent(string newDbFileName)
		{
			string text = "";
			TextReader textReader = null;
			try
			{
				textReader = new StreamReader(newDbFileName);
			}
			catch
			{
				throw;
			}
			try
			{
				text = textReader.ReadToEnd();
			}
			catch (EndOfStreamException)
			{
			}
			catch (Exception ex2)
			{
				throw ex2;
			}
			textReader?.Close();
            string[] array = text.Split('#');
            for (int i = 0; i < array.Length; i++)
            {
                array[i] = array[i].Replace("\r\n", " ");
            }
            return array;
		}

		private string GetInsertDataString(string newDataFileName)
		{
			StringBuilder stringBuilder = new StringBuilder();
			string originalDiectory = StoreConfiguration.OriginalDiectory;
			string text = "";
			text = ((!File.Exists(originalDiectory + "\\SQL\\" + newDataFileName)) ? (originalDiectory + "\\" + newDataFileName) : (originalDiectory + "\\SQL\\" + newDataFileName));
			StreamReader streamReader = null;
			try
			{
				streamReader = new StreamReader(text);
			}
			catch (FileNotFoundException)
			{
				StringBuilder stringBuilder2 = new StringBuilder();
				stringBuilder2.Append("File ").Append(text).Append(" not found.");
				stringBuilder2.Append("\nThis file is used for creating database.");
				stringBuilder2.Append("\nPlease add this file to the directory.");
				stringBuilder2.Append("\nDatabase creation aborted.");
				throw new ApplicationException(stringBuilder2.ToString());
			}
			catch (Exception ex2)
			{
				throw ex2;
			}
			streamReader.BaseStream.Seek(0L, SeekOrigin.Begin);
			bool flag = true;
			try
			{
				while (streamReader.Peek() > -1 && flag)
				{
					stringBuilder.Append(streamReader.ReadLine()).Append("\n");
				}
			}
			catch (EndOfStreamException)
			{
				flag = false;
			}
			catch (Exception ex4)
			{
				throw ex4;
			}
			finally
			{
				streamReader?.Close();
			}
			return stringBuilder.ToString();
		}

		private string GetInsertCountries()
		{
			StringBuilder stringBuilder = new StringBuilder();
			string path = StoreConfiguration.OriginalDiectory + "\\country.txt";
			StreamReader streamReader = null;
			try
			{
				streamReader = new StreamReader(path);
				string text = streamReader.ReadLine();
				while (text.Trim().Length == 0)
				{
					text = streamReader.ReadLine();
				}
				string[] array = text.Split(',');
				for (string text2 = streamReader.ReadLine(); text2 != null; text2 = streamReader.ReadLine())
				{
					string[] array2 = text2.Split(',');
					if (array2.Length == array.Length)
					{
						stringBuilder.Append("INSERT ").Append("Country").Append("(");
						for (int i = 0; i < array.Length; i++)
						{
							stringBuilder.Append(array[i]).Append(",");
						}
						stringBuilder.Remove(stringBuilder.Length - 1, 1);
						stringBuilder.Append(") VALUES(");
						for (int j = 0; j < array2.Length; j++)
						{
							stringBuilder.Append("'").Append(array2[j]).Append("',");
						}
						stringBuilder.Remove(stringBuilder.Length - 1, 1);
						stringBuilder.Append(") ");
					}
				}
			}
			catch
			{
			}
			finally
			{
				streamReader?.Close();
			}
			return stringBuilder.ToString();
		}

		public bool BackupDatabaseToDisk(string fileName, string description, string databaseName, string serverName, string adminLogin, string adminPassword)
		{
			try
			{
                Server srv = new Server(new ServerConnection(base.DBConfig.Connection));
                BackupDeviceItem item = new BackupDeviceItem(fileName, DeviceType.File);
                Backup backup = new Backup();
                backup.Devices.Add(item);
                backup.Action = BackupActionType.Database;
                backup.BackupSetDescription = description;
                backup.BackupSetName = databaseName;
                backup.Database = databaseName;
                backup.LogTruncation = BackupTruncateLogType.Truncate;
                backup.SqlBackup(srv);
                return true;
			}
			catch (FailedOperationException)
			{
				throw new FailedOperationException();
			}
			catch
			{
				throw;
			}
		}

		public bool RestoreDatabaseToDisk(string backupFileName, string destinationFileName, string databaseName, bool replace)
		{
			SqlConnection connection = base.DBConfig.Connection;
			string text = "";
			if (replace)
			{
				connection.ChangeDatabase("msdb");
				text = "SELECT FileName FROM SYS.SysDatabases WHERE Name='" + databaseName + "'";
				object obj = ExecuteScalar(text);
				if (obj != null)
				{
					destinationFileName = obj.ToString();
				}
				if (destinationFileName == "")
				{
					throw new CompanyException("Cannot find the database location. The database may not exists or access denied.", 1015);
				}
			}
			else if (IsDBExist(databaseName))
			{
				throw new CompanyException("This database name is already exist.", 1014);
			}
			connection = base.DBConfig.Connection;
			connection.ChangeDatabase("master");
			Server srv = new Server(new ServerConnection(connection));
			//BackupDeviceItem item = new BackupDeviceItem(backupFileName, DeviceType.File);
			Restore obj2 = new Restore
			{
				Action = RestoreActionType.Database,
				Devices = 
				{
				//	item
				},
				NoRecovery = false,
				ReplaceDatabase = replace
			};
			DataTable dataTable = obj2.ReadFileList(srv);
			string logicalFileName = dataTable.Rows[0][0].ToString();
			string logicalFileName2 = dataTable.Rows[1][0].ToString();
			obj2.RelocateFiles.Add(new RelocateFile(logicalFileName, destinationFileName));
			string text2 = Path.GetDirectoryName(destinationFileName);
			if (!text2.EndsWith("\\"))
			{
				text2 += "\\";
			}
			text2 = text2 + Path.GetFileNameWithoutExtension(destinationFileName) + "_log.ldf";
			obj2.RelocateFiles.Add(new RelocateFile(logicalFileName2, text2));
			obj2.Database = databaseName;
			obj2.UnloadTapeAfter = true;
			obj2.RestrictedUser = false;
			obj2.NoRecovery = false;
			obj2.Partial = false;
			obj2.SqlRestore(srv);
			return true;
		}

		public bool IsUserAllowedToConnect(string userID)
		{
			string exp = "SELECT COUNT(*) FROM Users WHERE UserID='" + userID + "' AND ISNULL(Inactive,'False')='False'";
			object obj = ExecuteScalar(exp);
			if (obj != null)
			{
				return int.Parse(obj.ToString()) > 0;
			}
			return false;
		}

		public bool CreateNewCompany(string companyName, string destinationFileName, string databaseName)
		{
			try
			{
				var con = base.DBConfig.Connection;
				string text = "";
				bool flag = true;
				string text2 = Path.GetDirectoryName(Application.ExecutablePath) + "\\dbschem.dsc";
				if (!File.Exists(text2))
				{
					throw new CompanyException("Cannot find the file: dbschem.dsc which is required to create a new company. The file must exists in the installation directory.");
				}
				int num = 0;
				text = GetCreateDatabaseString(databaseName, destinationFileName);
				num = ExecuteNonQuery(text);
				flag = (flag && num > -2);
				if (flag)
				{
					try
					{
						base.DBConfig.DatabaseName = databaseName;
						string[] createUpdateFileContent = GetCreateUpdateFileContent(text2);
						string script = File.ReadAllText(text2);
						//for (int i = 0; i < createUpdateFileContent.Length; i++)
						//{
						//	flag &= (ExecuteNonQuery(createUpdateFileContent[i]) >= 0);
						//}
						//var query = Regex.Split(script, @"^\w+GO$", RegexOptions.Multiline);
						System.Collections.Generic.IEnumerable<string> query = Regex.Split(script, @"^\s*GO\s*$",
										RegexOptions.Multiline | RegexOptions.IgnoreCase);
						foreach (string c in query)
						{
							if (string.IsNullOrEmpty(c)) continue;
							
							flag &= (ExecuteNonQuery(c) >= 0);
						}
						

						text = "INSERT INTO Company (CompanyID,CompanyName,DBVersion,PrimaryAddressID) VALUES (1,'" + companyName + "','" + RequiredDBVersion + "','PRIMARY')";
						flag &= (ExecuteNonQuery(text) >= 0);
						flag &= InsertInitialRecords();
						return true;
					}
					catch (Exception ex)
					{
						var exc = ex.Message.ToString();
					}
				}
				return flag;
			}
			catch (Exception ex)
			{
				var exce = ex.Message.ToString();
				return
				false;
			}
		}

		public bool InsertInitialRecords()
		{
			bool flag = true;
			try
			{
				string exp = "INSERT INTO Users (UserID,UserName,CanCreateCard,CanDeleteCard,CanCreateTransaction,CanEditTransaction,CanDeleteTransaction,IsAdmin,\r\n                    CreatedBy,DateCreated) VALUES('sa','System Administrator','True','True','True','True','True','True','sa','" + DateTime.Now.ToString(StoreConfiguration.CurrentCulture) + "')";
				flag &= (ExecuteNonQuery(exp) > 0);
				exp = "INSERT INTO System_Document (SysDocID,SysDocType,DocName,PrintTitle,NextNumber,NumberPrefix)\r\n                    VALUES('ICC01',14,'Issued Cheque Clearance','Issued Cheque Clearance',1,'')";
				flag &= (ExecuteNonQuery(exp) > 0);
				exp = "INSERT INTO System_Document (SysDocID,SysDocType,DocName,PrintTitle,NextNumber,NumberPrefix)\r\n                    VALUES('ICV01',15,'Issued Cheque Cancellation','Issued Cheque Cancellation',1,'')";
				flag &= (ExecuteNonQuery(exp) > 0);
				exp = "INSERT INTO System_Document (SysDocID,SysDocType,DocName,PrintTitle,NextNumber,NumberPrefix)\r\n                    VALUES('ICR01',16,'Issued Cheque Return','Issued Cheque Return',1,'')";
				flag &= (ExecuteNonQuery(exp) > 0);
				exp = "INSERT INTO System_Document (SysDocID,SysDocType,DocName,PrintTitle,NextNumber,NumberPrefix)\r\n                    VALUES('SCQ',17,'Issue Security Cheque','Issue Security Cheque',1,'')";
				exp = "INSERT INTO System_Document (SysDocID,SysDocType,DocName,PrintTitle,NextNumber,NumberPrefix)\r\n                    VALUES('RTCQ01',12,'Returned Cheque','Returned Cheque',1,'')";
				flag &= (ExecuteNonQuery(exp) > 0);
				exp = "INSERT INTO System_Document (SysDocID,SysDocType,DocName,PrintTitle,NextNumber,NumberPrefix)\r\n                    VALUES('RCC01',13,'Received Cheque Cancellation','Received Cheque Cancellation',1,'')";
				flag &= (ExecuteNonQuery(exp) > 0);
				exp = "INSERT INTO System_Document (SysDocID,SysDocType,DocName,PrintTitle,NextNumber,NumberPrefix)\r\n                    VALUES('JVE001',1,'General Journal Voucher','General Journal Voucher',1,'')";
				flag &= (ExecuteNonQuery(exp) > 0);
				exp = "INSERT INTO Company_Address (AddressID)\r\n                    VALUES('PRIMARY')";
				flag &= (ExecuteNonQuery(exp) > 0);
				exp = "INSERT INTO Account_Type (TypeID,AccountTypeName)\r\n                    VALUES(1,'Asset')\r\n\r\n                    INSERT INTO Account_Type (TypeID,AccountTypeName)\r\n                    VALUES(2,'Liability')\r\n                    \r\n                    INSERT INTO Account_Type (TypeID,AccountTypeName)\r\n                    VALUES(3,'Income')\r\n                    \r\n                       INSERT INTO Account_Type (TypeID,AccountTypeName)\r\n                    VALUES(4,'Expense')\r\n                    \r\n                       INSERT INTO Account_Type (TypeID,AccountTypeName)\r\n                    VALUES(5,'Capital') ";
				return flag & (ExecuteNonQuery(exp) > 0);
			}
			catch
			{
				throw;
			}
		}

		public bool AttachDatabase(string databaseFileName, string databaseName)
		{
			try
			{
				_ = base.DBConfig.Connection;
				if (IsDBExist(databaseName))
				{
					throw new CompanyException("This database name is already exist.", 1014);
				}
				new Server(new ServerConnection(base.DBConfig.Connection)).AttachDatabase(databaseName, new StringCollection
				{
					databaseFileName
				});
				return true;
			}
			catch
			{
				throw;
			}
		}

		public bool DetachDatabase(string databaseName)
		{
			try
			{
				_ = base.DBConfig.Connection;
				if (!IsDBExist(databaseName))
				{
					throw new CompanyException("This database does not exist.", 1016);
				}
				new Server(new ServerConnection(base.DBConfig.Connection)).DetachDatabase(databaseName, updateStatistics: false);
				return true;
			}
			catch
			{
				throw;
			}
		}

		public bool IsDBExist(string dbName)
		{
			string text = "";
			base.DBConfig.Connection.ChangeDatabase("msdb");
			text = "SELECT COUNT(Name) FROM SYS.SysDatabases WHERE Name='" + dbName + "'";
			object obj = ExecuteScalar(text);
			if (obj != null)
			{
				if (int.Parse(obj.ToString()) > 0)
				{
					return true;
				}
				return false;
			}
			return false;
		}

		public bool HasDatabaseAccessRight(string serverName, string databaseName, string loginName, string password)
		{
			bool result = false;
			SqlConnection sqlConnection = null;
			try
			{
				if (!ExistDatabase(serverName, databaseName, loginName, password))
				{
					return result;
				}
				sqlConnection = base.DBConfig.GetSqlConnection(serverName, databaseName, base.DBConfig.ID);
				return result;
			}
			catch
			{
				throw;
			}
			finally
			{
				if (sqlConnection != null)
				{
					if (sqlConnection.State == ConnectionState.Open)
					{
						sqlConnection.Close();
					}
					sqlConnection.Dispose();
				}
			}
		}

		public bool ExistDatabase(string serverName, string databaseName, string userName, string password)
		{
			bool result = false;
			foreach (DataRow row in GetDatabaseList(serverName, userName, password, null).Tables["Databases"].Rows)
			{
				if (row["Database_name"].ToString().ToLower() == databaseName)
				{
					return true;
				}
			}
			return result;
		}

		internal DataSet GetDatabaseList(string serverName, string userName, string password, SqlConnection sqlConnection)
		{
			bool flag = false;
			if (sqlConnection == null)
			{
				sqlConnection = base.DBConfig.GetConnection(serverName, userName, password);
				flag = true;
			}
			if (sqlConnection.State != ConnectionState.Open)
			{
				sqlConnection.Open();
			}
			SqlCommand sqlCommand = new SqlCommand("sp_databases");
			sqlCommand.CommandType = CommandType.StoredProcedure;
			DataSet dataSet = new DataSet();
			try
			{
				if (sqlConnection.State != ConnectionState.Open)
				{
					return null;
				}
				FillDataSet(dataSet, "Databases", sqlCommand, sqlConnection);
				return dataSet;
			}
			catch
			{
				throw;
			}
			finally
			{
				if (flag && sqlConnection != null)
				{
					if (sqlConnection.State == ConnectionState.Open)
					{
						sqlConnection.Close();
					}
					sqlConnection.Dispose();
				}
			}
		}

		public DataSet GetDatabases(string serverName, string adminUser, string adminPass)
		{
			SqlConnection sqlConnection = base.DBConfig.GetSqlConnection(serverName, adminUser, adminPass, base.DBConfig.ID);
			return GetDatabases(serverName, adminUser, adminPass, sqlConnection);
		}

		public DataSet GetDatabases(string serverName, string userName, string password, int i)
		{
			SqlConnection sqlConnection = base.DBConfig.GetSqlConnection(serverName, base.DBConfig.ID);
			return GetDatabases(serverName, userName, password, sqlConnection);
		}

		private DataSet GetDatabases(string serverName, string userName, string password, SqlConnection sqlConnection)
		{
			try
			{
				DataSet databaseList = GetDatabaseList(serverName, userName, password, sqlConnection);
				if (databaseList == null)
				{
					return null;
				}
				databaseList.Tables["Databases"].Columns.Add("CompanyName");
				databaseList.Tables["Databases"].Columns.Add("DBVersion");
				databaseList.Tables["Databases"].Columns.Add("DBDataVersion");
				if (sqlConnection.State != ConnectionState.Open)
				{
					sqlConnection.Open();
				}
				foreach (DataRow row in databaseList.Tables["Databases"].Rows)
				{
					string text = row["Database_name"].ToString();
					if (text != "master" && text != "msdb" && text != "model" && text != "tempdb")
					{
						row["CompanyName"] = GetCompanyName(text, sqlConnection);
						row["DBVersion"] = GetDBVersion(text, sqlConnection);
						row["DBDataVersion"] = GetDBDataVersion(text, sqlConnection);
					}
				}
				if (sqlConnection != null)
				{
					if (sqlConnection.State == ConnectionState.Open)
					{
						sqlConnection.Close();
					}
					sqlConnection.Dispose();
				}
				return databaseList;
			}
			catch
			{
				throw;
			}
		}

		public string GetCompanyName(string databaseName, string serverName, string userName, string password)
		{
			if (password.Length != 0)
			{
				password = base.DBConfig.ConfigHelper.Cryptor.Decrypt(password);
			}
			SqlConnection sqlConnection = null;
			try
			{
				sqlConnection = base.DBConfig.GetConnection(serverName, userName, password, databaseName);
			}
			catch
			{
				throw;
			}
			sqlConnection.Open();
			SqlCommand sqlCommand = new SqlCommand("SELECT CompanyName FROM Company", sqlConnection);
			sqlCommand.CommandType = CommandType.Text;
			string result = "";
			try
			{
				result = sqlCommand.ExecuteScalar().ToString();
				return result;
			}
			catch
			{
				return result;
			}
			finally
			{
				if (sqlConnection != null)
				{
					if (sqlConnection.State != ConnectionState.Open)
					{
						sqlConnection.Open();
					}
					sqlConnection.ChangeDatabase("master");
					sqlConnection.Close();
					sqlConnection.Dispose();
				}
			}
		}

		public string GetCompanyName(string databaseName, SqlConnection sqlConnection)
		{
			sqlConnection.ChangeDatabase(databaseName);
			SqlCommand sqlCommand = new SqlCommand("SELECT CompanyName FROM Company", sqlConnection);
			sqlCommand.CommandType = CommandType.Text;
			string result = "";
			try
			{
				object obj = sqlCommand.ExecuteScalar();
				if (obj == null)
				{
					return result;
				}
				result = obj.ToString();
				return result;
			}
			catch
			{
				return result;
			}
		}

		public string GetDBVersion(string databaseName, SqlConnection sqlConnection)
		{
			sqlConnection.ChangeDatabase(databaseName);
			SqlCommand sqlCommand = new SqlCommand("SELECT DBVersion FROM Company", sqlConnection);
			sqlCommand.CommandType = CommandType.Text;
			string text = "";
			try
			{
				object obj = sqlCommand.ExecuteScalar();
				if (obj != null)
				{
					text = obj.ToString();
				}
				if (text == "" || text.Length < 7)
				{
					return "1.0.0.0";
				}
				return text;
			}
			catch
			{
				return "1.0.0.0";
			}
		}

		public string GetDBDataVersion(string databaseName, SqlConnection sqlConnection)
		{
			sqlConnection.ChangeDatabase(databaseName);
			SqlCommand sqlCommand = new SqlCommand("SELECT ISNULL(DBDataVersion,'1.8.11.140) AS DBDataVersion FROM Company", sqlConnection);
			sqlCommand.CommandType = CommandType.Text;
			string text = "";
			try
			{
				object obj = sqlCommand.ExecuteScalar();
				if (obj != null)
				{
					text = obj.ToString();
				}
				if (text == "" || text.Length < 7)
				{
					return "1.0.0.0";
				}
				return text;
			}
			catch
			{
				return "1.0.0.0";
			}
		}

		private StringCollection GetDBUpdateIndex(string fileName)
		{
			if (fileName == null || fileName.Length == 0)
			{
				fileName = "UDB.ind";
			}
			new StringBuilder();
			string originalDiectory = StoreConfiguration.OriginalDiectory;
			string text = "";
			text = ((!File.Exists(originalDiectory + "\\SQL\\" + fileName)) ? (originalDiectory + "\\" + fileName) : (originalDiectory + "\\SQL\\" + fileName));
			StreamReader streamReader = null;
			StringCollection stringCollection = new StringCollection();
			try
			{
				streamReader = new StreamReader(text, Encoding.ASCII);
				for (string text2 = streamReader.ReadLine(); text2 != null; text2 = streamReader.ReadLine())
				{
					stringCollection.Add(text2);
				}
				return stringCollection;
			}
			catch (FileNotFoundException)
			{
				StringBuilder stringBuilder = new StringBuilder();
				stringBuilder.Append("File ").Append(text).Append(" not found.");
				stringBuilder.Append("\nThis file is used for upgrading the database.");
				stringBuilder.Append("\nPlease add this file to the directory.");
				stringBuilder.Append("\nDatabase upgrade aborted.");
				throw new ApplicationException(stringBuilder.ToString());
			}
			catch (Exception ex2)
			{
				throw ex2;
			}
		}

		private bool AddAdminUser(Config config, string userName)
		{
			return true;
		}

		public bool UpgradeDatabase(string databaseName, FileStream dacPackageFile, string userName, string password, bool backup)
		{
			SqlConnection sqlConnection = null;
			Version result = new Version();
			Version.TryParse(GetCurrentDBVersion(), out result);
			try
			{
				DacPackage dacPackage = DacPackage.Load(dacPackageFile);
				bool flag = true;
				string connectionString = ConnectionString(base.DBConfig.ServerName, userName, password, databaseName);
				try
				{
					sqlConnection = new SqlConnection(connectionString);
					sqlConnection.Open();
					sqlConnection.Close();
				}
				catch
				{
					throw new CompanyException("Cannot connect to database.", 1042);
				}
				DacServices dacServices = new DacServices(connectionString);
				DacDeployOptions dacDeployOptions = new DacDeployOptions
				{
					BlockOnPossibleDataLoss = false,
					DropConstraintsNotInSource = true,
					DropObjectsNotInSource = false,
					DropPermissionsNotInSource = false,
					DropRoleMembersNotInSource = false,
					DropIndexesNotInSource = false
				};
				if (backup)
				{
					dacDeployOptions.BackupDatabaseBeforeChanges = true;
				}
				else
				{
					dacDeployOptions.BackupDatabaseBeforeChanges = false;
				}
				dacServices.Deploy(dacPackage, databaseName, upgradeExisting: true, dacDeployOptions);
				if (flag)
				{
					sqlConnection = new SqlConnection(connectionString);
					sqlConnection.Open();
					SqlCommand sqlCommand = new SqlCommand("UPDATE Company SET DBVersion = '" + dacPackage.Version + "'", sqlConnection);
					flag &= (sqlCommand.ExecuteNonQuery() > 0);
				}
				return flag;
			}
			catch
			{
				throw;
			}
			finally
			{
				if (sqlConnection != null && sqlConnection.State == ConnectionState.Open)
				{
					sqlConnection.Close();
				}
			}
		}

		public bool UpgradeDatabaseData(string databaseName, string userName, string password)
		{
			bool flag = true;
			SqlConnection sqlConnection = null;
			new Version();
			Version result = new Version();
			string connectionString = ConnectionString(base.DBConfig.ServerName, userName, password, databaseName);
			Version.TryParse(GetCurrentDBDataVersion(), out result);
			try
			{
				DataPatchData patchData = new DataPatchData();
				string text = "";
				if (result.CompareTo(Version.Parse("1.8.10.291")) <= 0)
				{
					text = "UPDATE Inventory_Transactions SET RefRowIndex = DRD.DNRowIndex,RefSysDocID = DR.DNoteSysDocID,RefVoucherID = DR.DNoteVoucherID\r\n                            FROM Inventory_Transactions IT \r\n                            INNER JOIN Delivery_Return_Detail DRD ON DRD.SysDocID = IT.SysDocID  AND DRD.VoucherID = IT.VoucherID AND DRD.RowIndex = IT.RowIndex\r\n                            INNER JOIN Delivery_Return DR ON DR.SysDocID = DRD.SysDocID AND DR.VoucherID = DRD.VoucherID\r\n                             WHERE SysDocType = 29";
					AddPatchRow(ref patchData, 10010, "Update delivery returns in inventory transactions", text, "1.8.10.291");
				}
				if (result.CompareTo(Version.Parse("1.8.10.291")) <= 0)
				{
					text = "UPDATE PID SET ITRowID = (SELECT TransactionID FROM Inventory_Transactions IT WHERE IT.SysDocID = PID.SysDocID AND IT.VoucherID = PID.VoucherID AND IT.RowIndex = PID.RowIndex) \r\n                                    FROM Purchase_Return_Detail PID INNER JOIN Purchase_Return PI ON PI.SysDocID = PID.SysDocID AND PI.VoucherID = PID.VoucherID";
					AddPatchRow(ref patchData, 10011, "Update ITRowID for purchase return", text, "1.8.10.291");
					text = "UPDATE PID SET ITRowID = CASE WHEN ISNULL(RowSource,1) = 2  THEN  (SELECT TransactionID FROM Inventory_Transactions IT \r\n                                WHERE IT.SysDocID = PID.OrderSysDocID AND IT.VoucherID = PID.OrderVoucherID AND IT.RowIndex = PID.OrderRowIndex) \r\n                                    ELSE(SELECT TransactionID FROM Inventory_Transactions IT WHERE IT.SysDocID = PID.SysDocID AND IT.VoucherID = PID.VoucherID AND IT.RowIndex = PID.RowIndex) END\r\n                                    FROM Purchase_Invoice_Detail PID INNER JOIN Purchase_Invoice PI ON PI.SysDocID = PID.SysDocID AND PI.VoucherID = PID.VoucherID";
					AddPatchRow(ref patchData, 10012, "Update ITRowID for purchase invoice", text, "1.8.10.291");
					text = "UPDATE SID SET ITRowID = CASE WHEN ISNULL(IsDNRow,'False') = 'True' THEN  (SELECT TransactionID FROM Inventory_Transactions IT WHERE IT.SysDocID = SID.OrderSysDocID AND IT.VoucherID = SID.OrderVoucherID AND IT.RowIndex = SID.OrderRowIndex) \r\n                                    ELSE(SELECT TransactionID FROM Inventory_Transactions IT WHERE IT.SysDocID = SID.SysDocID AND IT.VoucherID = SID.VoucherID AND IT.RowIndex = SID.RowIndex) END\r\n                                    FROM Sales_Invoice_Detail SID INNER JOIN Sales_Invoice SI ON SI.SysDocID = SID.SysDocID AND SI.VoucherID = SID.VoucherID";
					AddPatchRow(ref patchData, 10013, "Update ITRowID for sales invoice", text, "1.8.10.291");
					text = "UPDATE SID SET ITRowID = (SELECT TransactionID FROM Inventory_Transactions IT WHERE IT.SysDocID = SID.SysDocID AND IT.VoucherID = SID.VoucherID AND IT.RowIndex = SID.RowIndex) \r\n                                    FROM Sales_Return_Detail SID INNER JOIN Sales_Return SI ON SI.SysDocID = SID.SysDocID AND SI.VoucherID = SID.VoucherID";
					AddPatchRow(ref patchData, 10014, "Update ITRowID for sales return", text, "1.8.10.291");
					text = "UPDATE CSD SET ITRowID = (SELECT TOP 1 TransactionID FROM Inventory_Transactions IT WHERE IT.SysDocID = CSD.SysDocID AND IT.VoucherID = CSD.VoucherID AND IT.RowIndex = CSD.RowIndex AND IT.ProductID = CSD.ProductID AND IT.Quantity = -1* CSD.Quantity)\r\n                            FROM ConsignOut_Settlement_Detail CSD INNER JOIN ConsignOut_Settlement CS ON CS.SysDocID = CSD.SysDocID AND CS.VoucherID = CSD.VoucherID";
					AddPatchRow(ref patchData, 10015, "Update ITRowID for consignment out settlment", text, "1.8.10.291");
					text = "UPDATE SID SET ITRowID = (SELECT TransactionID FROM Inventory_Transactions IT WHERE IT.SysDocID = SID.SysDocID AND IT.VoucherID = SID.VoucherID AND IT.RowIndex = SID.RowIndex) \r\n                                    FROM Sales_POS_Detail SID INNER JOIN Sales_POS SI ON SI.SysDocID = SID.SysDocID AND SI.VoucherID = SID.VoucherID";
					AddPatchRow(ref patchData, 10016, "Update ITRowID for pos sales", text, "1.8.10.291");
					text = "update JD  set jd.JDDate =j.JournalDate  from Journal_Details JD  inner Join Journal J on j.JournalID = jd.JournalID";
					AddPatchRow(ref patchData, 10017, "Update journalDate on  Journal_Details", text, "1.8.10.291");
					text = "update JD  set jd.DocType=sd.SysDocType  from Journal_Details JD  inner Join System_Document SD on sd.SysDocID = jd.SysDocID";
					AddPatchRow(ref patchData, 10018, "Update SysDocType on  Journal_Details", text, "1.8.10.291");
				}
				if (result.CompareTo(Version.Parse("1.8.11.210")) <= 0)
				{
					text = "UPDATE Inventory_Transactions SET RefRowIndex = DRD.DNRowIndex,RefSysDocID = DR.DNoteSysDocID,RefVoucherID = DR.DNoteVoucherID,\r\n                            RefTransactionID = (SELECT TransactionID FROM Inventory_Transactions IT2 WHERE IT2.SysDocID = DR.DNoteSysDocID AND IT2.VoucherID = DR.DNoteVoucherID AND IT2.RowIndex = DRD.DNRowIndex)\r\n                            FROM Inventory_Transactions IT \r\n                            INNER JOIN Delivery_Return_Detail DRD ON DRD.SysDocID = IT.SysDocID  AND DRD.VoucherID = IT.VoucherID AND DRD.RowIndex = IT.RowIndex\r\n                            INNER JOIN Delivery_Return DR ON DR.SysDocID = DRD.SysDocID AND DR.VoucherID = DRD.VoucherID\r\n                             WHERE SysDocType = 29";
					AddPatchRow(ref patchData, 10019, "Update delivery returns in inventory transactions", text, "1.8.11.210");
				}
				if (result.CompareTo(Version.Parse("1.8.11.270")) <= 0)
				{
					text = "UPDATE SID SET ITRowID = (SELECT TransactionID FROM Inventory_Transactions IT WHERE IT.SysDocID = SID.SysDocID AND IT.VoucherID = SID.VoucherID AND IT.RowIndex = SID.RowIndex) \r\n                                    FROM Inventory_Adjustment_Detail SID  ";
					AddPatchRow(ref patchData, 10020, "Update ITRowID for Inventory Adjustments", text, "1.8.11.270");
					text = "UPDATE SID SET ITRowID = (SELECT TransactionID FROM Inventory_Transactions IT WHERE IT.SysDocID = SID.SysDocID AND IT.VoucherID = SID.VoucherID AND IT.RowIndex = SID.RowIndex) \r\n                                    FROM Delivery_Note_Detail SID  ";
					AddPatchRow(ref patchData, 10021, "Update ITRowID for Delivery Notes", text, "1.8.11.270");
					text = "UPDATE SID SET ITRowID = (SELECT TransactionID FROM Inventory_Transactions IT WHERE IT.SysDocID = SID.SysDocID AND IT.VoucherID = SID.VoucherID AND IT.RowIndex = SID.RowIndex) \r\n                                    FROM Delivery_Return_Detail SID  ";
					AddPatchRow(ref patchData, 10022, "Update ITRowID for Delivery Returns", text, "1.8.11.270");
					text = "UPDATE SID SET ITRowID = (SELECT TransactionID FROM Inventory_Transactions IT WHERE IT.SysDocID = SID.SysDocID AND IT.VoucherID = SID.VoucherID AND IT.RowIndex = SID.RowIndex) \r\n                                    FROM Purchase_Receipt_Detail SID  ";
					AddPatchRow(ref patchData, 10023, "Update ITRowID for GRN", text, "1.8.11.270");
					text = "UPDATE SID SET ITRowID = (SELECT TransactionID FROM Inventory_Transactions IT WHERE IT.SysDocID = SID.SysDocID AND IT.VoucherID = SID.VoucherID AND IT.RowIndex = SID.RowIndex) \r\n                                    FROM GRN_Return_Detail SID  ";
					AddPatchRow(ref patchData, 10024, "Update ITRowID for GRN Return", text, "1.8.11.270");
					text = "UPDATE SID SET ITRowID = (SELECT TransactionID FROM Inventory_Transactions IT WHERE IT.SysDocID = SID.SysDocID AND IT.VoucherID = SID.VoucherID AND IT.RowIndex = SID.RowIndex) \r\n                                    FROM Consign_Out_Detail SID  ";
					AddPatchRow(ref patchData, 10025, "Update ITRowID for Consign Out", text, "1.8.11.270");
					text = "UPDATE SID SET ITRowID = (SELECT TransactionID FROM Inventory_Transactions IT WHERE IT.SysDocID = SID.SysDocID AND IT.VoucherID = SID.VoucherID AND IT.RowIndex = SID.RowIndex) \r\n                                    FROM ConsignOut_Return_Detail SID  ";
					AddPatchRow(ref patchData, 10026, "Update ITRowID for Consign Out Return", text, "1.8.11.270");
					text = "UPDATE SID SET ITRowID = (SELECT TransactionID FROM Inventory_Transactions IT WHERE IT.SysDocID = SID.SysDocID AND IT.VoucherID = SID.VoucherID AND IT.RowIndex = SID.RowIndex) \r\n                                    FROM Inventory_Damage_Detail SID  ";
					AddPatchRow(ref patchData, 10027, "Update ITRowID for Non-Sale Issue", text, "1.8.11.270");
					text = "UPDATE SID SET ITRowID = (SELECT TransactionID FROM Inventory_Transactions IT WHERE IT.SysDocID = SID.SysDocID AND IT.VoucherID = SID.VoucherID AND IT.RowIndex = SID.RowIndex) \r\n                                    FROM Inventory_Repacking_Detail SID  ";
					AddPatchRow(ref patchData, 10028, "Update ITRowID for Inventory Repacking", text, "1.8.11.270");
					text = "UPDATE SID SET ITRowID = (SELECT TransactionID FROM Inventory_Transactions IT WHERE IT.SysDocID = SID.SysDocID AND IT.VoucherID = SID.VoucherID AND IT.RowIndex = SID.RowIndex) \r\n                                    FROM Inventory_Dismantle_Detail SID  ";
					AddPatchRow(ref patchData, 10029, "Update ITRowID for Inventory Repacking", text, "1.8.11.270");
				}
				if (result.CompareTo(Version.Parse("1.8.12.150")) <= 0)
				{
					text = "UPDATE PID SET ITRowID = CASE WHEN ISNULL(RowSource,1) = 2  THEN  (SELECT TransactionID FROM Inventory_Transactions IT \r\n                                WHERE IT.SysDocID = PID.OrderSysDocID AND IT.VoucherID = PID.OrderVoucherID AND IT.RowIndex = PID.OrderRowIndex) \r\n                                    ELSE(SELECT TransactionID FROM Inventory_Transactions IT WHERE IT.SysDocID = PID.SysDocID AND IT.VoucherID = PID.VoucherID AND IT.RowIndex = PID.RowIndex) END\r\n                                    FROM Purchase_Invoice_Detail PID INNER JOIN Purchase_Invoice PI ON PI.SysDocID = PID.SysDocID AND PI.VoucherID = PID.VoucherID";
					AddPatchRow(ref patchData, 10030, "Update ITRowID for purchase invoice", text, "1.8.12.150");
					text = " update Inventory_Transactions  set IsNonCostedGRN ='false'\r\n                                    where isnull(IsNonCostedGRN,'false')='true'\r\n                                    and sysdocid+voucherid in (select distinct Ordersysdocid+ordervoucherid  FROM Purchase_Invoice_Detail PID INNER JOIN Purchase_Invoice PI ON PI.SysDocID = PID.SysDocID AND PID.VoucherID = PI.VoucherID where isnull(isvoid,'false')='false' )  ";
					AddPatchRow(ref patchData, 10031, "Update IsNonCostedGRN for some rows", text, "1.8.12.150");
				}
				if (result.CompareTo(Version.Parse("1.8.12.220")) <= 0)
				{
					text = "UPDATE  Inventory_Transactions  SET ReturnedQuantity = Quantity WHERE SysDocType = 95";
					AddPatchRow(ref patchData, 10032, "Update GRN Return Quantity", text, "1.8.12.220");
					text = " Update IT Set RefSysDocID = GRD.SourceSysDocID, RefVoucherID = GRD.SourceVoucherID,RefRowIndex = GRD.SourceRowIndex FROM Inventory_Transactions IT\r\n                                INNER JOIN GRN_Return_Detail GRD ON GRD.SysDocID = IT.SysDocID AND GRD.VoucherID = IT.VoucherID AND GRD.RowIndex = IT.RowIndex\r\n                                WHERE IT.SysDocType = 95 \r\n\r\n\r\n                                UPDATE Inventory_Transactions SET  \r\n                                RefTransactionID = (SELECT TransactionID FROM Inventory_Transactions IT2 WHERE IT2.SysDocID = IT.RefSysDocID AND IT2.VoucherID = IT.RefVoucherID AND IT2.RowIndex = IT.RefRowIndex)\r\n                                FROM Inventory_Transactions IT\r\n                                 WHERE SysDocType=95 ";
					AddPatchRow(ref patchData, 10033, "Update RefTransactionID for GRN Returns", text, "1.8.12.220");
					text = "UPDATE  Inventory_Transactions  SET AssetValue = ROUND((AssetValue/Quantity) * (Quantity-ReturnedQuantity),5)  WHERE SysDocType in (50,32) AND ISNULL(ReturnedQuantity,0)>0";
					AddPatchRow(ref patchData, 10034, "Update GRN assetvalue", text, "1.8.12.220");
					text = "UPDATE  Inventory_Transactions  SET AssetValue = 0, AverageCost =0  WHERE SysDocType =95 ";
					AddPatchRow(ref patchData, 10035, "Update GRN Return assetvalue", text, "1.8.12.220");
				}
				if (patchData.DataPatchTable.Rows.Count > 0)
				{
					flag = new DataPatches(base.DBConfig).InsertDataPatch(patchData);
				}
				if (flag)
				{
					string requiredDBDataVersion = RequiredDBDataVersion;
					sqlConnection = new SqlConnection(connectionString);
					sqlConnection.Open();
					SqlCommand sqlCommand = new SqlCommand("UPDATE Company SET DBDataVersion = '" + requiredDBDataVersion + "'", sqlConnection);
					flag &= (sqlCommand.ExecuteNonQuery() > 0);
				}
				return flag;
			}
			catch
			{
				throw;
			}
			finally
			{
				if (sqlConnection != null && sqlConnection.State == ConnectionState.Open)
				{
					sqlConnection.Close();
				}
			}
		}

		private void AddPatchRow(ref DataPatchData patchData, int id, string description, string query, string version)
		{
			DataRow dataRow = patchData.DataPatchTable.NewRow();
			dataRow["PatchID"] = id;
			dataRow["PatchDescription"] = description;
			dataRow["PatchQuery"] = query;
			dataRow["DataVersion"] = version;
			patchData.DataPatchTable.Rows.Add(dataRow);
		}

		private string[] GetUpgradeDatabaseObjectsString(string upgradeFileName)
		{
			string text = "";
			TextReader textReader = null;
			try
			{
				textReader = new StreamReader(upgradeFileName, Encoding.Unicode);
			}
			catch (FileNotFoundException)
			{
				StringBuilder stringBuilder = new StringBuilder();
				stringBuilder.Append("File ").Append(upgradeFileName).Append(" not found.");
				stringBuilder.Append("\nThis file is used for upgrading the database.");
				stringBuilder.Append("\nPlease add this file to the directory.");
				stringBuilder.Append("\nDatabase upgrade aborted.");
				throw new ApplicationException(stringBuilder.ToString());
			}
			catch (Exception ex2)
			{
				throw ex2;
			}
			try
			{
				text = textReader.ReadToEnd();
			}
			catch (EndOfStreamException)
			{
			}
			catch (Exception ex4)
			{
				throw ex4;
			}
			finally
			{
				textReader?.Close();
			}
			return text.Split('$');
		}

		internal SqlConnection GetConnection(string serverName, string userName, string password)
		{
			return new SqlConnection(ConnectionString(serverName, userName, password));
		}

		internal SqlConnection GetConnection(string serverName, string userName, string password, string dbName)
		{
			return new SqlConnection(ConnectionString(serverName, userName, password, dbName));
		}

		private string ConnectionString(string serverName, string userName, string password)
		{
			return "User ID=" + userName + ";Password=" + password + ";Server=" + serverName + ";Pooling=false;Connection Lifetime=1;Connection Timeout=30;Integrated Security=false;Database='master'";
		}

		private string ConnectionString(string serverName, string userName, string password, string dbName)
		{
			return "User ID=" + userName + ";Password=" + password + ";Server=" + serverName + ";Pooling=false;Connection Lifetime=1;Connection Timeout=30;Integrated Security=false;Database=" + dbName;
		}

		public bool ScheduleDatabaseBackup(string jobName, string scheduleName, string serverName, string dbName, string loginName, string password, int freqType, int freqInterval, int freqRecurrenceFactor, DateTime startDate, DateTime startTime, DateTime endDate, bool hasEndDate, string backupPath)
		{
			if (password.Length != 0)
			{
				password = base.DBConfig.ConfigHelper.Cryptor.Decrypt(password);
			}
			SqlCommand sqlCommand = null;
			SqlConnection sqlConnection = null;
			SqlTransaction sqlTransaction = null;
			try
			{
				sqlConnection = GetConnection(serverName, loginName, password, "msdb");
				sqlConnection.Open();
				sqlTransaction = sqlConnection.BeginTransaction();
				StringBuilder stringBuilder = new StringBuilder();
				stringBuilder.Append("Select Name From sysjobs Where Name ='" + jobName + "'");
				sqlCommand = new SqlCommand();
				sqlCommand.CommandType = CommandType.Text;
				sqlCommand.CommandText = stringBuilder.ToString();
				sqlCommand.Connection = sqlConnection;
				sqlCommand.Transaction = sqlTransaction;
				object obj = sqlCommand.ExecuteScalar();
				sqlCommand.Dispose();
				sqlCommand = null;
				SqlParameterCollection parameters;
				if (obj != null)
				{
					sqlCommand = new SqlCommand();
					sqlCommand.Connection = sqlConnection;
					sqlCommand.CommandText = "sp_delete_job";
					sqlCommand.CommandType = CommandType.StoredProcedure;
					sqlCommand.Transaction = sqlTransaction;
					parameters = sqlCommand.Parameters;
					parameters.Add("@job_name", SqlDbType.NVarChar);
					parameters["@job_name"].Value = jobName;
					sqlCommand.ExecuteNonQuery();
					sqlCommand.Dispose();
					sqlCommand = null;
				}
				sqlCommand = new SqlCommand();
				sqlCommand.Connection = sqlConnection;
				sqlCommand.CommandType = CommandType.StoredProcedure;
				sqlCommand.CommandText = "sp_add_job";
				sqlCommand.Transaction = sqlTransaction;
				parameters = sqlCommand.Parameters;
				parameters.Add("@job_name", SqlDbType.NVarChar);
				parameters["@job_name"].Value = jobName;
				parameters.Add("@enabled", SqlDbType.TinyInt);
				parameters["@enabled"].Value = 1;
				parameters.Add("@description", SqlDbType.NVarChar);
				parameters["@description"].Value = "Job created by Axolon for the database '" + dbName + "'. This job has schedules to backup the specified database.";
				parameters.Add("@owner_login_name", SqlDbType.NVarChar);
				parameters["@owner_login_name"].Value = loginName;
				parameters.Add("@notify_level_eventlog", SqlDbType.TinyInt);
				parameters["@notify_level_eventlog"].Value = 3;
				sqlCommand.ExecuteNonQuery();
				sqlCommand.Dispose();
				sqlCommand = null;
				sqlCommand = new SqlCommand();
				sqlCommand.CommandType = CommandType.Text;
				sqlCommand.CommandText = "Select job_id From sysjobs Where Name ='" + jobName + "'";
				sqlCommand.Connection = sqlConnection;
				sqlCommand.Transaction = sqlTransaction;
				obj = sqlCommand.ExecuteScalar();
				sqlCommand.Dispose();
				sqlCommand = null;
				string value = obj.ToString();
				sqlCommand = new SqlCommand();
				sqlCommand.Connection = sqlConnection;
				sqlCommand.CommandText = "sp_add_jobstep";
				sqlCommand.Transaction = sqlTransaction;
				sqlCommand.CommandType = CommandType.StoredProcedure;
				parameters = sqlCommand.Parameters;
				parameters.Add("@job_id", SqlDbType.NVarChar);
				parameters["@job_id"].Value = value;
				parameters.Add("@step_name", SqlDbType.NVarChar);
				parameters["@step_name"].Value = "Backup database '" + dbName + "'";
				parameters.Add("@subsystem", SqlDbType.NVarChar);
				parameters["@subsystem"].Value = "TSQL";
				parameters.Add("@command", SqlDbType.NVarChar);
				parameters["@command"].Value = "BACKUP DATABASE " + dbName + " TO DISK = '" + backupPath + "'";
				parameters.Add("@retry_attempts", SqlDbType.Int);
				parameters["@retry_attempts"].Value = 5;
				parameters.Add("@retry_interval", SqlDbType.Int);
				parameters["@retry_interval"].Value = 5;
				sqlCommand.ExecuteNonQuery();
				sqlCommand.Dispose();
				sqlCommand = null;
				sqlCommand = new SqlCommand();
				sqlCommand.Connection = sqlConnection;
				sqlCommand.CommandText = "sp_add_jobserver";
				sqlCommand.Transaction = sqlTransaction;
				sqlCommand.CommandType = CommandType.StoredProcedure;
				parameters = sqlCommand.Parameters;
				parameters.Add("@job_id", SqlDbType.NVarChar);
				parameters["@job_id"].Value = value;
				parameters.Add("@server_name", SqlDbType.NVarChar);
				parameters["@server_name"].Value = serverName;
				sqlCommand.ExecuteNonQuery();
				sqlCommand.Dispose();
				sqlCommand = null;
				sqlCommand = new SqlCommand();
				sqlCommand.Connection = sqlConnection;
				sqlCommand.Transaction = sqlTransaction;
				sqlCommand.CommandText = "sp_add_jobschedule";
				sqlCommand.CommandType = CommandType.StoredProcedure;
				parameters = sqlCommand.Parameters;
				parameters.Add("@job_id", SqlDbType.NVarChar);
				parameters["@job_id"].Value = value;
				parameters.Add("@name", SqlDbType.NVarChar);
				parameters["@name"].Value = scheduleName;
				parameters.Add("@freq_type", SqlDbType.Int);
				parameters["@freq_type"].Value = freqType;
				parameters.Add("@freq_interval", SqlDbType.Int);
				parameters["@freq_interval"].Value = freqInterval;
				parameters.Add("@freq_recurrence_factor", SqlDbType.Int);
				parameters["@freq_recurrence_factor"].Value = freqRecurrenceFactor;
				parameters.Add("@enabled", SqlDbType.TinyInt);
				parameters["@enabled"].Value = 1;
				string str = startDate.Year.ToString();
				str = ((startDate.Month >= 10) ? (str + startDate.Month.ToString()) : (str + "0" + startDate.Month.ToString()));
				str = ((startDate.Day >= 10) ? (str + startDate.Day.ToString()) : (str + "0" + startDate.Day.ToString()));
				parameters.Add("@active_start_date", SqlDbType.Int);
				parameters["@active_start_date"].Value = str;
				string str2 = "";
				str2 = ((startTime.Hour >= 10) ? (str2 + startTime.Hour.ToString()) : (str2 + "0" + startTime.Hour.ToString()));
				str2 = ((startTime.Minute >= 10) ? (str2 + startTime.Minute.ToString()) : (str2 + "0" + startTime.Minute.ToString()));
				str2 = ((startTime.Second >= 10) ? (str2 + startTime.Second.ToString()) : (str2 + "0" + startTime.Second.ToString()));
				parameters.Add("@active_start_time", SqlDbType.Int);
				parameters["@active_start_time"].Value = str2;
				if (hasEndDate)
				{
					str = endDate.Year.ToString();
					str = ((endDate.Month >= 10) ? (str + endDate.Month.ToString()) : (str + "0" + endDate.Month.ToString()));
					str = ((startDate.Day >= 10) ? (str + endDate.Day.ToString()) : (str + "0" + endDate.Day.ToString()));
					parameters.Add("@active_end_date", SqlDbType.Int);
					parameters["@active_end_date"].Value = str;
				}
				parameters.Add("@freq_subday_type", SqlDbType.Int);
				parameters["@freq_subday_type"].Value = 1;
				sqlCommand.ExecuteNonQuery();
				sqlCommand.Dispose();
				sqlCommand = null;
			}
			catch (SqlException ex)
			{
				if (sqlTransaction != null)
				{
					sqlTransaction.Rollback();
					sqlTransaction.Dispose();
					sqlTransaction = null;
				}
				throw ex;
			}
			catch (Exception ex2)
			{
				if (sqlTransaction != null)
				{
					sqlTransaction.Rollback();
					sqlTransaction.Dispose();
					sqlTransaction = null;
				}
				throw ex2;
			}
			finally
			{
				if (sqlTransaction != null)
				{
					sqlTransaction.Commit();
					sqlTransaction.Dispose();
					sqlTransaction = null;
				}
				if (sqlConnection != null)
				{
					sqlConnection.Close();
					sqlConnection.Dispose();
					sqlConnection = null;
				}
			}
			return true;
		}

		public DateTime GetTableLastDateTimeStamp(params string[] tablesName)
		{
			DateTime result = StoreConfiguration.LongDBDateTimeMin;
			if (tablesName == null || tablesName.Length == 0)
			{
				return result;
			}
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("SELECT TOP 1 DateTimeStamp FROM [Table TimeStamps] WHERE TableName IN ('");
			StringBuilder stringBuilder2 = new StringBuilder();
			for (int i = 0; i < tablesName.Length; i++)
			{
				stringBuilder2.Append(tablesName[i].ToString()).Append("','");
			}
			stringBuilder2.Remove(stringBuilder2.Length - 2, 2);
			stringBuilder2.Append(") ");
			stringBuilder.Append(stringBuilder2.ToString()).Append(" ");
			stringBuilder.Append("ORDER BY DateTimeStamp DESC");
			try
			{
				object obj = ExecuteScalar(stringBuilder.ToString());
				if (obj == null)
				{
					return result;
				}
				result = DateTime.Parse(obj.ToString());
				return result;
			}
			catch
			{
				return result;
			}
		}

		public string GetMachineName()
		{
			return Environment.MachineName;
		}

		public string GetCurrentDBPath()
		{
			return GetDBPath(base.DBConfig.DatabaseName);
		}

		public string GetDBPath(string dbName)
		{
			StringBuilder stringBuilder = new StringBuilder();
			string result = "";
			stringBuilder.Append("SELECT filename FROM sysdatabases WHERE name='").Append(dbName + "'");
			Config config = new Config(base.DBConfig.ID);
			SqlConnection sqlConnection = null;
			try
			{
				sqlConnection = config.GetSqlConnection(base.DBConfig.ServerName, "master", base.DBConfig.ID);
				object obj = ExecuteScalar(stringBuilder.ToString(), sqlConnection);
				if (obj == null)
				{
					return result;
				}
				result = obj.ToString();
				return result;
			}
			catch
			{
				return result;
			}
			finally
			{
				if (sqlConnection != null)
				{
					sqlConnection.Close();
					sqlConnection.Dispose();
					sqlConnection = null;
				}
				if (config != null)
				{
					config.CloseConnection();
					config.Dispose();
					config = null;
				}
			}
		}

		public object[] GetNextFieldsID(string tableName, string fieldID, string fieldIDName, object fieldNameValue, uint nextCount)
		{
			if (fieldNameValue != null)
			{
				fieldNameValue = AddSingleQuote(fieldNameValue.ToString());
			}
			if (fieldNameValue == null)
			{
				fieldNameValue = "";
			}
			if (fieldNameValue.ToString() == "")
			{
				return new object[1]
				{
					GetMinFieldID(tableName, fieldID, null, null, null, null)
				};
			}
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("SELECT TOP ").Append(nextCount.ToString()).Append(" ")
				.Append(fieldID)
				.Append(" FROM ");
			stringBuilder.Append(tableName).Append(" WHERE ").Append(fieldIDName)
				.Append(">");
			stringBuilder.Append("'").Append(fieldNameValue.ToString()).Append("'")
				.Append(" ");
			stringBuilder.Append("ORDER BY ").Append(fieldIDName).Append(" ASC");
			DataSet dataSet = new DataSet();
			ArrayList arrayList = new ArrayList();
			FillDataSet(dataSet, tableName, stringBuilder.ToString());
			foreach (DataRow row in dataSet.Tables[tableName].Rows)
			{
				arrayList.Add(row[fieldID].ToString());
			}
			dataSet.Dispose();
			dataSet = null;
			return arrayList.ToArray();
		}

		public object[] GetPreviousFieldsID(string tableName, string fieldID, string fieldIDName, object fieldNameValue, uint prevCount)
		{
			if (fieldNameValue == null)
			{
				fieldNameValue = "";
			}
			if (fieldNameValue.ToString() == "")
			{
				return new object[1]
				{
					GetMaxFieldID(tableName, fieldID, null, null, null, null)
				};
			}
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("SELECT TOP ").Append(prevCount.ToString()).Append(" ")
				.Append(fieldID)
				.Append(" FROM ");
			stringBuilder.Append(tableName).Append(" WHERE ").Append(fieldIDName)
				.Append("<");
			stringBuilder.Append("'").Append(fieldNameValue.ToString()).Append("'")
				.Append(" ");
			stringBuilder.Append("ORDER BY ").Append(fieldIDName).Append(" DESC");
			DataSet dataSet = new DataSet();
			ArrayList arrayList = new ArrayList();
			FillDataSet(dataSet, tableName, stringBuilder.ToString());
			foreach (DataRow row in dataSet.Tables[tableName].Rows)
			{
				arrayList.Add(row[fieldID].ToString());
			}
			dataSet.Dispose();
			dataSet = null;
			return arrayList.ToArray();
		}

		public object[] GetNextFieldsID(string tableName, string fieldID, string fieldIDName, object fieldNameValue, string field2Name, object field2Value, string field3Name, object field3Value, uint nextCount)
		{
			if (fieldNameValue == null)
			{
				fieldNameValue = "";
			}
			if (fieldNameValue.ToString() == "")
			{
				return new object[1]
				{
					GetMinFieldID(tableName, fieldID, field2Name, field2Value, null, null)
				};
			}
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("SELECT TOP ").Append(nextCount.ToString()).Append(" ")
				.Append(fieldID)
				.Append(" FROM ");
			stringBuilder.Append(tableName).Append(" WHERE ").Append(fieldIDName)
				.Append(">");
			stringBuilder.Append("'").Append(fieldNameValue.ToString()).Append("'")
				.Append(" AND ");
			stringBuilder.Append(field2Name).Append("='").Append(field2Value)
				.Append("' ");
			if (field3Name != "")
			{
				stringBuilder.Append(" AND " + field3Name + " = '" + field3Value + "' ");
			}
			stringBuilder.Append("ORDER BY ").Append(fieldIDName).Append(" ASC");
			DataSet dataSet = new DataSet();
			ArrayList arrayList = new ArrayList();
			FillDataSet(dataSet, tableName, stringBuilder.ToString());
			foreach (DataRow row in dataSet.Tables[tableName].Rows)
			{
				arrayList.Add(row[fieldID].ToString());
			}
			dataSet.Dispose();
			dataSet = null;
			return arrayList.ToArray();
		}

		public object[] GetPreviousFieldsID(string tableName, string fieldID, string fieldIDName, object fieldNameValue, string field2Name, object field2Value, string field3Name, object field3Value, uint prevCount)
		{
			if (fieldNameValue == null)
			{
				fieldNameValue = "";
			}
			if (fieldNameValue.ToString() == "")
			{
				return new object[1]
				{
					GetMaxFieldID(tableName, fieldID, field2Name, field2Value, null, null)
				};
			}
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("SELECT TOP ").Append(prevCount.ToString()).Append(" ")
				.Append(fieldID)
				.Append(" FROM ");
			stringBuilder.Append(tableName).Append(" WHERE ").Append(fieldIDName)
				.Append("<");
			stringBuilder.Append("'").Append(fieldNameValue.ToString()).Append("'")
				.Append(" AND ");
			stringBuilder.Append(field2Name).Append("='").Append(field2Value)
				.Append("' ");
			if (field3Name != "")
			{
				stringBuilder.Append(" AND " + field3Name + " = '" + field3Value.ToString() + "' ");
			}
			stringBuilder.Append("ORDER BY ").Append(fieldIDName).Append(" DESC");
			DataSet dataSet = new DataSet();
			ArrayList arrayList = new ArrayList();
			FillDataSet(dataSet, tableName, stringBuilder.ToString());
			foreach (DataRow row in dataSet.Tables[tableName].Rows)
			{
				arrayList.Add(row[fieldID].ToString());
			}
			dataSet.Dispose();
			dataSet = null;
			return arrayList.ToArray();
		}

		public object GetMinFieldID(string tableName, string fieldID, string field2Name, object field2Value, string field3Name, object field3Value)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("SELECT MIN(").Append(fieldID).Append(") FROM ")
				.Append(tableName);
			if (field2Name != null && field2Name != "")
			{
				stringBuilder.Append(" WHERE ").Append(field2Name).Append("='")
					.Append(field2Value)
					.Append("' ");
			}
			if (field3Name != null && field3Name != "")
			{
				stringBuilder.Append(" AND ").Append(field3Name).Append("='")
					.Append(field3Value)
					.Append("' ");
			}
			return ExecuteScalar(stringBuilder.ToString());
		}

		public object GetMaxFieldID(string tableName, string fieldID, string field2Name, object field2Value, string field3Name, object field3Value)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("SELECT MAX(").Append(fieldID).Append(") FROM ")
				.Append(tableName);
			if (field2Name != null && field2Name != "")
			{
				stringBuilder.Append(" WHERE ").Append(field2Name).Append("='")
					.Append(field2Value)
					.Append("' ");
			}
			if (field3Name != null && field3Name != "")
			{
				stringBuilder.Append(" AND ").Append(field3Name).Append("='")
					.Append(field3Value)
					.Append("' ");
			}
			return ExecuteScalar(stringBuilder.ToString());
		}

		public DatabaseData GetTableUserStats(string tableName, string tableFieldName, object tableFieldID)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			CommandHelper commandHelper = null;
			commandHelper = new CommandHelper();
			commandHelper.FieldName = tableFieldName;
			commandHelper.SqlFieldType = SqlDbType.Int;
			commandHelper.FieldValue = tableFieldID;
			commandHelper.TableName = tableName;
			sqlBuilder.AddCommandHelper(commandHelper);
			sqlBuilder.AddColumn(tableName, "DateTimeStamp");
			sqlBuilder.AddColumn(tableName, "DateCreated");
			sqlBuilder.AddColumn("Users", "CreatedByTable", "", "CreatedByLoginName");
			sqlBuilder.AddColumn("Users", "UpdatedByTable", "", "UpdatedByLoginName");
			sqlBuilder.AddJointer(tableName, "CreatedBy", "Users", "CreatedByTable", "UserID");
			sqlBuilder.AddJointer(tableName, "UpdatedBy", "Users", "UpdatedByTable", "UserID");
			sqlBuilder.JointerSourceTable = tableName;
			DatabaseData databaseData = new DatabaseData();
			FillDataSet(databaseData, "Databases", sqlBuilder);
			return databaseData;
		}

		public bool ExistFieldValue(string tableName, string fieldName, string fieldValue)
		{
			try
			{
				return IsTableFieldValueExist(tableName, fieldName, fieldValue);
			}
			catch
			{
				throw;
			}
		}

		public bool ExistFieldValue(string tableName, string fieldName, string fieldValue, string fieldName2, object fieldValue2)
		{
			try
			{
				return IsTableFieldValueExist(tableName, fieldName, fieldName2, fieldValue, fieldValue2);
			}
			catch
			{
				throw;
			}
		}

		public bool ExistFieldValue(string tableName1, string tableName2, string fieldName, string fieldValue, string fieldName2, object fieldValue2, string fieldName3, object fieldValue3)
		{
			try
			{
				return IsTableFieldValueExist(tableName1, tableName2, fieldName, fieldName2, fieldName3, fieldValue, fieldValue2, fieldValue3);
			}
			catch
			{
				throw;
			}
		}

		public int UpdateFieldValue(string tableName, string fieldName, decimal fieldValue, string idFieldName, object idFieldValue, SqlTransaction sqlTransaction)
		{
			return UpdateFieldValue(tableName, fieldName, fieldValue, typeof(decimal), idFieldName, idFieldValue, updateUserDetails: false, sqlTransaction);
		}

		public int UpdateFieldValue(string tableName, string fieldName, object fieldValue, Type fieldType, string idFieldName, object idFieldValue, bool updateUserDetails, SqlTransaction sqlTransaction)
		{
			try
			{
				string text = "UPDATE " + tableName + " SET ";
				text = ((!(fieldValue.ToString() != "")) ? (text + fieldName + " = NULL ") : ((!(fieldType == typeof(decimal))) ? (text + fieldName + " = '" + fieldValue.ToString() + "' ") : (text + fieldName + " = " + fieldValue.ToString())));
				if (updateUserDetails)
				{
					string text2 = StoreConfiguration.ToSqlDateTimeString(DateTime.Now);
					text = text + " , UpdatedBy = '" + base.DBConfig.UserID + "', DateUpdated = '" + text2 + "' ";
				}
				text = text + " WHERE " + idFieldName + "='" + idFieldValue.ToString() + "'";
				return ExecuteNonQuery(text, sqlTransaction);
			}
			catch
			{
				throw;
			}
		}

		public int UpdateFieldValue(string tableName, string updateFieldName, object updateFieldValue, string checkFieldName1, object checkFieldValue1, string checkFieldName2, object checkFieldValue2, string checkFieldName3, object checkFieldValue3, SqlTransaction sqlTransaction)
		{
			return UpdateFieldValue(tableName, updateFieldName, updateFieldValue, checkFieldName1, checkFieldValue1, checkFieldName2, checkFieldValue2, checkFieldName3, checkFieldValue3, updateUserDetails: false, sqlTransaction);
		}

		public int UpdateFieldValue(string tableName, string updateFieldName, object updateFieldValue, string checkFieldName1, object checkFieldValue1, string checkFieldName2, object checkFieldValue2, SqlTransaction sqlTransaction)
		{
			return UpdateFieldValue(tableName, updateFieldName, updateFieldValue, checkFieldName1, checkFieldValue1, checkFieldName2, checkFieldValue2, "", "", updateUserDetails: false, sqlTransaction);
		}

		public int UpdateFieldValue(string tableName, string updateFieldName, object updateFieldValue, string checkFieldName1, object checkFieldValue1, string checkFieldName2, object checkFieldValue2, string checkFieldName3, object checkFieldValue3, bool updateUserDetails, SqlTransaction sqlTransaction)
		{
			try
			{
				string text = "UPDATE " + tableName + " SET " + updateFieldName + "=" + updateFieldValue.ToString();
				if (updateUserDetails)
				{
					string text2 = StoreConfiguration.ToSqlDateTimeString(DateTime.Now);
					text = text + " , UpdatedBy = '" + base.DBConfig.UserID + "', DateUpdated = '" + text2 + "' ";
				}
				text = text + "  WHERE " + checkFieldName1 + "='" + checkFieldValue1.ToString() + "' AND " + checkFieldName2 + "='" + checkFieldValue2.ToString() + "' ";
				if (checkFieldName3 != "")
				{
					text = text + " AND " + checkFieldName3 + " = '" + checkFieldValue3.ToString() + "' ";
				}
				return ExecuteNonQuery(text, sqlTransaction);
			}
			catch
			{
				throw;
			}
		}

		public object GetFieldValue(string tableName, string fieldName, string idFieldName, object idFieldValue, SqlTransaction sqlTransaction)
		{
			try
			{
				string exp = "SELECT " + fieldName + " FROM " + tableName + " WHERE " + idFieldName + " = '" + idFieldValue + "'";
				return ExecuteScalar(exp, sqlTransaction);
			}
			catch
			{
				throw;
			}
		}

		public object GetFieldValue(string tableName, string requiredFieldName, string idFieldName, object idFieldValue, string checkFieldName, object checkFieldValue, SqlTransaction sqlTransaction)
		{
			try
			{
				string exp = "SELECT " + requiredFieldName + " FROM " + tableName + " WHERE " + idFieldName + " = '" + idFieldValue + "' AND " + checkFieldName + "='" + checkFieldValue + "'";
				return ExecuteScalar(exp, sqlTransaction);
			}
			catch
			{
				throw;
			}
		}

		public DataSet GetDataByFields(string tableName, string idFieldName, string idFieldValue, params string[] columns)
		{
			try
			{
				string text = "SELECT ";
				for (int i = 0; i < columns.Length; i++)
				{
					text += columns.GetValue(i).ToString();
					if (i < columns.Length - 1)
					{
						text += ",";
					}
				}
				text = text + " FROM " + tableName + " WHERE " + idFieldName + " = '" + idFieldValue + "'";
				DataSet dataSet = new DataSet();
				FillDataSet(dataSet, tableName, text);
				return dataSet;
			}
			catch
			{
				throw;
			}
		}

		public object GetFieldValue(string tableName, string requiredFieldName, string idFieldName, object idFieldValue, string checkFieldName, object checkFieldValue, string checkFieldName2, object checkFieldValue2, SqlTransaction sqlTransaction)
		{
			try
			{
				string exp = "SELECT " + requiredFieldName + " FROM " + tableName + " WHERE " + idFieldName + " = '" + idFieldValue + "' AND " + checkFieldName + "='" + checkFieldValue + "' AND " + checkFieldName2 + "='" + checkFieldValue2 + "'";
				return ExecuteScalar(exp, sqlTransaction);
			}
			catch
			{
				throw;
			}
		}

		public string FindDocumentByNumber(string tableName, string fieldName, string sysDocID, string numberQuery)
		{
			return FindDocumentByNumber(tableName, fieldName, sysDocID, "", null, numberQuery);
		}

		public string FindDocumentByNumber(string tableName, string fieldName, string sysDocID, string filterFieldName, object filterFieldValue, string numberQuery)
		{
			numberQuery = AddSingleQuote(numberQuery.ToString());
			string text = "";
			int result = 0;
			text = ((!int.TryParse(numberQuery, out result)) ? ("SELECT TOP 1 " + fieldName + " FROM " + tableName + " WHERE " + fieldName + " = '" + numberQuery + "' ") : ("SELECT TOP 1 " + fieldName + " FROM " + tableName + " WHERE " + fieldName + " LIKE '%" + numberQuery + "' "));
			if (sysDocID != "")
			{
				text = text + " AND SysDocID = '" + sysDocID + "'";
			}
			if (filterFieldName != "" && filterFieldValue != null)
			{
				text = text + " AND " + filterFieldName + " = '" + filterFieldValue.ToString() + "' ";
			}
			text = text + " ORDER BY " + fieldName + " DESC ";
			object obj = ExecuteScalar(text);
			if (obj == null)
			{
				return "";
			}
			if (int.TryParse(numberQuery, out result))
			{
				int num = 0;
				for (int i = 0; i < obj.ToString().Length; i++)
				{
					if (int.TryParse(obj.ToString().Substring(i, 1), out result))
					{
						num = i;
						break;
					}
				}
				if (long.Parse(obj.ToString().Substring(num, obj.ToString().Length - num)) != long.Parse(numberQuery))
				{
					return "";
				}
			}
			if (obj != null)
			{
				return obj.ToString();
			}
			return "";
		}

		public static DataSet GetGeneralComboItem(string TableName, string IDField, string NameField)
		{
			return null;
		}

		public DataSet GetComboRowByID(string tableName, string idFieldName, string idFieldValue, string nameFieldName)
		{
			return GetComboRowByID(tableName, idFieldName, idFieldValue, "", "", nameFieldName);
		}

		public DataSet GetComboRowByID(string tableName, string idFieldName, string idFieldValue, string idFieldName2, string idFieldValue2, string nameFieldName)
		{
			DataSet dataSet = new DataSet();
			string text = "SELECT " + idFieldName + " [Code]," + nameFieldName + " [Name]\r\n                           FROM " + tableName + " WHERE " + idFieldName + "='" + idFieldValue + "' ";
			if (idFieldName2 != "")
			{
				text = text + " AND " + idFieldName2 + " = '" + idFieldValue2 + "' ";
			}
			text = text + " ORDER BY " + idFieldName + "," + nameFieldName;
			FillDataSet(dataSet, tableName, text);
			return dataSet;
		}

		public bool ExecuteDataPatch(string patchID)
		{
			bool flag = true;
			try
			{
				DataPatchData dataPatchByID = new DataPatches(base.DBConfig).GetDataPatchByID(patchID);
				if (dataPatchByID == null || dataPatchByID.DataPatchTable.Rows.Count == 0)
				{
					return false;
				}
				int num = 1;
				if (!dataPatchByID.DataPatchTable.Rows[0]["Status"].IsDBNullOrEmpty())
				{
					num = int.Parse(dataPatchByID.DataPatchTable.Rows[0]["Status"].ToString());
				}
				if (num != 1)
				{
					throw new CompanyException("Patch is already applied.");
				}
				string exp = dataPatchByID.DataPatchTable.Rows[0]["PatchQuery"].ToString();
				SqlTransaction sqlTransaction = base.DBConfig.StartNewTransaction();
				flag = (ExecuteNonQuery(exp, sqlTransaction) >= 0);
				if (flag)
				{
					string text = CommonLib.ToSqlDateTimeString(DateTime.Now);
					exp = "UPDATE Data_Patch SET Status = 2,DateExecuted = '" + text + "' WHERE PatchID = '" + patchID + "'";
					flag &= (ExecuteNonQuery(exp, sqlTransaction) > 0);
				}
				return flag;
			}
			catch
			{
				flag = false;
				throw;
			}
			finally
			{
				base.DBConfig.EndTransaction(flag);
			}
		}

		public void ChangeDatabaseName(string databaseName)
		{
			base.DBConfig.DatabaseName = databaseName;
		}

		public int PerformExecuteNonQuery(string exp)
		{
			bool result = true;
			try
			{
				SqlTransaction sqlTransaction = base.DBConfig.StartNewTransaction();
				int num = ExecuteNonQuery(exp, sqlTransaction);
				result = (num >= 0);
				return num;
			}
			catch
			{
				result = false;
				throw;
			}
			finally
			{
				base.DBConfig.EndTransaction(result);
			}
		}

		public void BulkCopy(ArrayList productIDs, string tableName, string fieldName, SqlTransaction sqlTransaction)
		{
			try
			{
				DataTable dataTable = new DataTable();
				dataTable.Columns.Add("ProductID", typeof(string));
				foreach (string productID in productIDs)
				{
					dataTable.Rows.Add(productID);
				}
				new DataSet();
				SqlBulkCopy sqlBulkCopy = new SqlBulkCopy(base.DBConfig.Connection, SqlBulkCopyOptions.Default, sqlTransaction);
				string exp = "  CREATE TABLE " + tableName + " (" + fieldName + " nvarchar(64)) ";
				ExecuteNonQuery(exp, sqlTransaction);
				sqlBulkCopy.DestinationTableName = tableName;
				sqlBulkCopy.WriteToServer(dataTable);
			}
			catch
			{
				throw;
			}
		}

		public bool HasPendingDataPatches()
		{
			string exp = "SELECT COUNT(PatchID) AS C FROM   Data_Patch WHERE ISNULL(Status,1) = 1";
			return int.Parse(ExecuteScalar(exp).ToString()) > 0;
		}

		public object PerformExecuteScalar(string exp)
		{
			try
			{
				return ExecuteScalar(exp);
			}
			catch
			{
				throw;
			}
		}

		public bool CanConnect()
		{
			try
			{
				base.DBConfig.CloseConnection();
				return base.DBConfig.IsConnected();
			}
			catch
			{
				return false;
			}
		}

		public object[] GetPreviousFieldsIDByCardSecurity(string tableName, string fieldID, string fieldIDName, object fieldNameValue, uint prevCount)
		{
			string cardSecurityQuery = cardSecurityQuery = new Security(base.DBConfig).GetCardSecurityQuery();
			if (fieldNameValue == null)
			{
				fieldNameValue = "";
			}
			if (fieldNameValue.ToString() == "")
			{
				return new object[1]
				{
					GetMaxFieldIDByCardSecurity(tableName, fieldID, null, null, null, null)
				};
			}
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("SELECT TOP ").Append(prevCount.ToString()).Append(" ")
				.Append(fieldID)
				.Append(" FROM ");
			stringBuilder.Append(tableName).Append(" WHERE ").Append(fieldIDName)
				.Append("<");
			stringBuilder.Append("'").Append(fieldNameValue.ToString()).Append("'")
				.Append(" ");
			if (cardSecurityQuery != "")
			{
				stringBuilder.Append(" AND " + cardSecurityQuery);
			}
			stringBuilder.Append(" ORDER BY ").Append(fieldIDName).Append(" DESC");
			DataSet dataSet = new DataSet();
			ArrayList arrayList = new ArrayList();
			FillDataSet(dataSet, tableName, stringBuilder.ToString());
			foreach (DataRow row in dataSet.Tables[tableName].Rows)
			{
				arrayList.Add(row[fieldID].ToString());
			}
			dataSet.Dispose();
			dataSet = null;
			return arrayList.ToArray();
		}

		public object[] GetNextFieldsIDByCardSecurity(string tableName, string fieldID, string fieldIDName, object fieldNameValue, uint nextCount)
		{
			string cardSecurityQuery = cardSecurityQuery = new Security(base.DBConfig).GetCardSecurityQuery();
			if (fieldNameValue != null)
			{
				fieldNameValue = AddSingleQuote(fieldNameValue.ToString());
			}
			if (fieldNameValue == null)
			{
				fieldNameValue = "";
			}
			if (fieldNameValue.ToString() == "")
			{
				return new object[1]
				{
					GetMinFieldIDByCardSecurity(tableName, fieldID, null, null, null, null)
				};
			}
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("SELECT TOP ").Append(nextCount.ToString()).Append(" ")
				.Append(fieldID)
				.Append(" FROM ");
			stringBuilder.Append(tableName).Append(" WHERE ").Append(fieldIDName)
				.Append(">");
			stringBuilder.Append("'").Append(fieldNameValue.ToString()).Append("'")
				.Append(" ");
			if (cardSecurityQuery != "")
			{
				stringBuilder.Append(" AND " + cardSecurityQuery);
			}
			stringBuilder.Append(" ORDER BY ").Append(fieldIDName).Append(" ASC");
			DataSet dataSet = new DataSet();
			ArrayList arrayList = new ArrayList();
			FillDataSet(dataSet, tableName, stringBuilder.ToString());
			foreach (DataRow row in dataSet.Tables[tableName].Rows)
			{
				arrayList.Add(row[fieldID].ToString());
			}
			dataSet.Dispose();
			dataSet = null;
			return arrayList.ToArray();
		}

		public object GetMaxFieldIDByCardSecurity(string tableName, string fieldID, string field2Name, object field2Value, string field3Name, object field3Value)
		{
			string cardSecurityQuery = cardSecurityQuery = new Security(base.DBConfig).GetCardSecurityQuery();
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("SELECT MAX(").Append(fieldID).Append(") FROM ")
				.Append(tableName)
				.Append(" Where 1=1 ");
			if (cardSecurityQuery != "")
			{
				stringBuilder.Append(" AND " + cardSecurityQuery);
			}
			if (field2Name != null && field2Name != "")
			{
				stringBuilder.Append(" AND ").Append(field2Name).Append("='")
					.Append(field2Value)
					.Append("' ");
			}
			if (field3Name != null && field3Name != "")
			{
				stringBuilder.Append(" AND ").Append(field3Name).Append("='")
					.Append(field3Value)
					.Append("' ");
			}
			return ExecuteScalar(stringBuilder.ToString());
		}

		public object GetMinFieldIDByCardSecurity(string tableName, string fieldID, string field2Name, object field2Value, string field3Name, object field3Value)
		{
			string cardSecurityQuery = cardSecurityQuery = new Security(base.DBConfig).GetCardSecurityQuery();
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("SELECT MIN(").Append(fieldID).Append(") FROM ")
				.Append(tableName)
				.Append(" Where 1=1 ");
			if (cardSecurityQuery != "")
			{
				stringBuilder.Append(" AND " + cardSecurityQuery);
			}
			if (field2Name != null && field2Name != "")
			{
				stringBuilder.Append(" AND ").Append(field2Name).Append("='")
					.Append(field2Value)
					.Append("' ");
			}
			if (field3Name != null && field3Name != "")
			{
				stringBuilder.Append(" AND ").Append(field3Name).Append("='")
					.Append(field3Value)
					.Append("' ");
			}
			return ExecuteScalar(stringBuilder.ToString());
		}
	}
}
