using Micromind.Common.Data;
using Micromind.Common.Libraries;
using Micromind.Securities;
using Micromind.Securities.Cryptography;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Micromind.Data
{
	
	public sealed class Config : MarshalByRefObject, IDisposable
	{
		private ConfigHelper configHelper;

		private bool checkIntegrity = true;

		private string dbServer = "";

		private string dbName = "";

		private string applicationName = "";

		private string clientMachineName = "";

		private string dbAdminPassword = "";

		private bool boolCloseConnection = true;

		private SqlTransaction currentTransaction;

		private bool mustCommitRollbackTransaction = true;

		private SqlConnection sqlConnection;

		private string id = "";

		private ICryptor cryptor;

		private int currentLoginID = -1;

		private bool closeAssertionBeenChecked;

		internal ConfigHelper ConfigHelper => configHelper;

		internal string ID
		{
			get
			{
				return id;
			}
			set
			{
				id = value;
			}
		}

		public bool IsAsync
		{
			get;
			set;
		}

		internal int CurrentLoginID => currentLoginID;

		internal bool CheckIntegrity
		{
			set
			{
				checkIntegrity = value;
			}
		}

		internal string ApplicationName => applicationName;

		internal string ClientMachineName => clientMachineName;

		public string UserID
		{
			get
			{
				return configHelper.UserID;
			}
			set
			{
				configHelper.UserID = value;
			}
		}

		internal string AccessUser => configHelper.AccessUser;

		internal string Password
		{
			set
			{
				configHelper.Password = value;
			}
		}

		internal string ServerName
		{
			get
			{
				return dbServer;
			}
			set
			{
				dbServer = value;
			}
		}

		internal string DatabaseName
		{
			get
			{
				return dbName;
			}
			set
			{
				dbName = value;
			}
		}

		internal string ConnectionString => new SqlConnectionStringBuilder
		{
			UserID = configHelper.UserID==""?"sa":configHelper.UserID, //sa
			Password = configHelper.Password==""?"123456":configHelper.Password,//123456
			DataSource = dbServer,//desktop-h9ghner\\sqlexpress
			InitialCatalog = dbName,
			Pooling = true,
			ConnectionReset = false
		}.ConnectionString;

		internal SqlConnection Connection
		{
			get
			{
				if (sqlConnection == null)
				{
					sqlConnection = new SqlConnection(ConnectionString);
				}
				CloseConnection();
				OpenConnection();
				return sqlConnection;
			}
		}

		internal SqlTransaction SqlTransaction => currentTransaction;

		internal bool MustCommitRollbackTransaction
		{
			get
			{
				return mustCommitRollbackTransaction;
			}
			set
			{
				mustCommitRollbackTransaction = value;
			}
		}

		internal SqlTransaction CurrentTransaction => currentTransaction;

		internal bool ConnectionMustClose
		{
			get
			{
				return boolCloseConnection;
			}
			set
			{
				boolCloseConnection = value;
			}
		}

		internal bool CloseAssertionBeenChecked
		{
			get
			{
				return closeAssertionBeenChecked;
			}
			set
			{
				closeAssertionBeenChecked = value;
			}
		}

		internal string BUILTIN_USER_NAME
		{
			get
			{
				ConnectionTypes connectionType = configHelper.ConnectionType;
				if (connectionType == ConnectionTypes.LocalServer || connectionType != ConnectionTypes.ExternalServer)
				{
					return "AXOLON";
				}
				return configHelper.AdminUserName;
			}
		}

		internal string ENCRYPTED_BUILTIN_USER_PASSWORD
		{
			get
			{
				ConnectionTypes connectionType = configHelper.ConnectionType;
				if (connectionType == ConnectionTypes.LocalServer || connectionType != ConnectionTypes.ExternalServer)
				{
					return "";
				}
				return dbAdminPassword;
			}
			set
			{
				dbAdminPassword = value;
			}
		}

		public Config(string id)
		{
			CreateConfigHelper(id, GetDefaultCryptor(), ConnectionTypes.LocalServer);
		}

		public Config(string id, ConnectionTypes connectionType)
		{
			CreateConfigHelper(id, GetDefaultCryptor(), connectionType);
		}

		private ICryptor GetDefaultCryptor()
		{
			if (cryptor == null)
			{
				cryptor = new AESCryptor(new CryptoHelper());
			}
			return cryptor;
		}

		private void CreateConfigHelper(string id, ICryptor cryptor, ConnectionTypes connectionType)
		{
			if (configHelper != null)
			{
				configHelper.Dispose();
				configHelper = null;
			}
			try
			{
				configHelper = new ConfigHelper(id);
				configHelper.Cryptor = cryptor;
				configHelper.ConnectionType = connectionType;
				ID = id;
				if (!string.IsNullOrEmpty(configHelper.Password))
					configHelper.Password = "123456";

			}
			catch
			{
				Dispose();
			}
		}

		internal bool IsCurrentUserAdministrator()
		{
			return configHelper.IsCurrentUserAdministrator();
		}

		internal void ResetPassword(string userID, string password)
		{
			configHelper.ResetPassword(userID, password);
		}

		public bool IsConnected()
		{
			try
			{
				OpenConnection();
			}
			catch
			{
				return false;
			}
			return true;
		}

		public bool LogClientSignIn()
		{
			if (!IsConnected())
			{
				return false;
			}
			try
			{
				using (ActivityLogs activityLogs = new ActivityLogs(this))
				{
					return activityLogs.InsertActivityLog(null, null, null, ActivityTypes.LogIn, null, null, SysDocTypes.None, DataComboType.None, null, null);
				}
			}
			catch
			{
			}
			return false;
		}

		public bool LogClientSignOut()
		{
			if (!IsConnected())
			{
				return false;
			}
			try
			{
				using (ActivityLogs activityLogs = new ActivityLogs(this))
				{
					return activityLogs.InsertActivityLog(null, null, null, ActivityTypes.LogOut, null, null, SysDocTypes.None, DataComboType.None, null, null);
				}
			}
			catch
			{
			}
			return false;
		}

		public bool IsUserAdmin()
		{
			if (configHelper == null)
			{
				return false;
			}
			return configHelper.UserID == configHelper.AdminUserName.ToLower();
		}

		public void SetLoginInfo(string applicationName, string clientMachineName, string serverName, string systemID, string productKey, string databaseName, string userID, string password, string dbAdminUser, string dbAdminPassword, ICryptor cryptor, string id, ConnectionTypes connectionType)
		{
			if (userID == null || userID.Trim() == string.Empty)
			{
				throw new NullReferenceException("User id cannot be empty.");
			}
			if (cryptor == null)
			{
				throw new NullReferenceException("Cryptor cannot be null.");
			}
			if (configHelper == null)
			{
				CreateConfigHelper(id, cryptor, connectionType);
			}
			if (connectionType == ConnectionTypes.ExternalServer)
			{
				configHelper.AdminUserName = dbAdminUser;
				ENCRYPTED_BUILTIN_USER_PASSWORD = dbAdminPassword;
			}
			if (password != null && password.Length != 0)
			{
				password = configHelper.Cryptor.Decrypt(password);
			}
			if (userID.ToLower() == configHelper.AdminUserName.ToLower() || (dbAdminUser != null && dbAdminUser.ToLower() == userID.ToLower()))
			{
				configHelper.UserID = userID;
				configHelper.Password = password;
			}
			else
			{
				configHelper.UserID = userID;
				configHelper.Password = password;
			}
			configHelper.AccessUser = userID;
			configHelper.AccessPassword = password;
			dbServer = serverName;
			dbName = databaseName;
			this.applicationName = applicationName;
			this.clientMachineName = clientMachineName;
			_ = checkIntegrity;
		}

		public void SetLoginInfo(string applicationName, string clientMachineName, string serverName, string systemID, string productKey, string databaseName, string userID, string password)
		{
			SetLoginInfo(applicationName, clientMachineName, serverName, systemID, productKey, databaseName, userID, password, null, null, GetDefaultCryptor(), "", ConnectionTypes.LocalServer);
		}

		internal string GetConnectionString(string serverName, string userName, string password)
		{
			return "Workstation ID=" + clientMachineName + ";Application Name=" + applicationName + ";User ID=" + configHelper.UserID + ";Password=" + configHelper.Password + ";Server=" + dbServer + ";Pooling=false;Connection Timeout=5;Integrated Security=false;Database='master'";
		}

		internal string GetConnectionString(string serverName, string userName, string password, string dbName)
		{
			return "Workstation ID=" + clientMachineName + ";Application Name=" + applicationName + ";User ID=" + configHelper.UserID + ";Password=" + configHelper.Password + ";Server=" + dbServer + ";Pooling=false;Connection Timeout=5;Integrated Security=false;Database=" + dbName;
		}

		internal SqlConnection GetSqlConnection(string serverName, string dbName, string p)
		{
			string password = configHelper.Cryptor.Decrypt(ENCRYPTED_BUILTIN_USER_PASSWORD);
			return new SqlConnection(GetConnectionString(serverName, BUILTIN_USER_NAME, password, dbName));
		}

		internal SqlConnection GetSqlConnection(string serverName, string p)
		{
			string password = configHelper.Cryptor.Decrypt(ENCRYPTED_BUILTIN_USER_PASSWORD);
			return new SqlConnection(GetConnectionString(serverName, BUILTIN_USER_NAME, password));
		}

		internal SqlConnection GetSqlConnection(string serverName, string adminUser, string encryptedPass, string p)
		{
			string password = configHelper.Cryptor.Decrypt(ENCRYPTED_BUILTIN_USER_PASSWORD);
			return new SqlConnection(GetConnectionString(serverName, adminUser, password));
		}

		internal SqlConnection GetConnection(string serverName, string userName, string password)
		{
			return new SqlConnection(GetConnectionString(serverName, userName, password));
		}

		internal SqlConnection GetConnection(string serverName, string userName, string password, string dbName)
		{
			return new SqlConnection(GetConnectionString(serverName, userName, password, dbName));
		}

		internal void Connect()
		{
			if (sqlConnection == null)
			{
				sqlConnection = Connection;
			}
		}

		public void Disconnect()
		{
			Dispose();
		}

		public void OpenConnection()
		{
			if (sqlConnection == null || sqlConnection.State != ConnectionState.Open)
			{
				try
				{
					if (sqlConnection == null)
					{
						try
						{
							sqlConnection = new SqlConnection(ConnectionString);
						}
						catch (Exception ex)
						{
							throw ex;
						}
						try
						{
							_ = "User '" + UserID + "' connected to database '" + dbName + "' from client machine " + clientMachineName;
						}
						catch
						{
						}
					}
					if (sqlConnection.State != ConnectionState.Open)
					{
						try
						{
							sqlConnection.ConnectionString = ConnectionString;
							sqlConnection.Open();
						}
						catch (Exception ex)
						{
							//MessageBox.Show(ex.ToString()) ;
							throw;
						}
					}
				}
				catch
				{
					throw;
				}
			}
		}

		internal void CloseConnection()
		{
			if (sqlConnection == null || sqlConnection.State != 0)
			{
				try
				{
					if (ConnectionMustClose && mustCommitRollbackTransaction && sqlConnection != null && sqlConnection.State != 0)
					{
						sqlConnection.Close();
						currentTransaction = null;
					}
				}
				catch
				{
					if (currentTransaction != null)
					{
						currentTransaction = null;
					}
					throw;
				}
			}
		}

		public SqlTransaction StartNewTransaction()
		{
			ConnectionMustClose = false;
			MustCommitRollbackTransaction = false;
			try
			{
				Connect();
				OpenConnection();
			}
			catch
			{
				throw;
			}
			return BeginTransaction();
		}

		internal SqlTransaction BeginTransaction()
		{
			if (currentTransaction == null || currentTransaction.Connection == null || currentTransaction.Connection.State == ConnectionState.Closed)
			{
				OpenConnection();
				currentTransaction = sqlConnection.BeginTransaction();
			}
			return currentTransaction;
		}

		public void EndTransaction(bool result)
		{
			if (currentTransaction != null)
			{
				MustCommitRollbackTransaction = true;
				if (result)
				{
					CommitTransaction();
				}
				else
				{
					RollbackTransaction();
				}
				ForceCloseConnection();
			}
		}

		internal void CommitTransaction()
		{
			if (currentTransaction == null)
			{
				throw new NullReferenceException("Transaction is null.");
			}
			try
			{
				if (mustCommitRollbackTransaction)
				{
					currentTransaction.Commit();
					currentTransaction = null;
				}
			}
			catch
			{
				throw;
			}
		}

		internal void RollbackTransaction()
		{
			if (currentTransaction == null)
			{
				throw new NullReferenceException("Transaction is null.");
			}
			try
			{
				if (mustCommitRollbackTransaction)
				{
					try
					{
						currentTransaction.Rollback();
					}
					catch (Exception ex)
					{
						throw ex;
					}
					currentTransaction = null;
				}
			}
			catch
			{
				throw;
			}
		}

		internal void ForceCloseConnection()
		{
			if (mustCommitRollbackTransaction)
			{
				boolCloseConnection = true;
				CloseConnection();
			}
		}

		internal bool AddBuiltinLogin(SqlConnection sqlConnection, string defaultDB)
		{
			if (configHelper.IsAllwedDBSystemModification())
			{
				string password = configHelper.Cryptor.Decrypt(ENCRYPTED_BUILTIN_USER_PASSWORD);
				return AddLogin(sqlConnection, BUILTIN_USER_NAME, password, defaultDB, "db_datareader", "db_datawriter");
			}
			return true;
		}

		private bool AddLogin(SqlConnection sqlConnection, string loginName, string password, string defaultDB, params string[] roleNames)
		{
			if (!configHelper.IsAllwedDBSystemModification())
			{
				return true;
			}
			bool result = true;
			ConnectionMustClose = false;
			if (sqlConnection.State != ConnectionState.Open)
			{
				sqlConnection.Open();
			}
			SqlCommand sqlCommand = new SqlCommand("sp_helpuser", sqlConnection);
			sqlCommand.CommandType = CommandType.StoredProcedure;
			SqlParameterCollection parameters = sqlCommand.Parameters;
			parameters.Add("@name_in_db", SqlDbType.NVarChar);
			parameters["@name_in_db"].Value = loginName;
			try
			{
				using (StoreObject storeObject = new StoreObject(this))
				{
					object obj = storeObject.ExecuteScalar(sqlCommand);
					if (obj != DBNull.Value && obj != null)
					{
						sqlCommand = new SqlCommand("sp_revokedbaccess", sqlConnection);
						sqlCommand.CommandType = CommandType.StoredProcedure;
						parameters = sqlCommand.Parameters;
						parameters.Add("@name_in_db", SqlDbType.NVarChar);
						parameters["@name_in_db"].Value = loginName;
						try
						{
							using (StoreObject storeObject2 = new StoreObject(this))
							{
								storeObject2.ExecuteNonQuery(sqlCommand);
							}
						}
						catch (Exception ex)
						{
							throw ex;
						}
					}
				}
			}
			catch
			{
			}
			sqlCommand = new SqlCommand("sp_addlogin", sqlConnection);
			sqlCommand.CommandType = CommandType.StoredProcedure;
			parameters = sqlCommand.Parameters;
			parameters.Add("@loginame", SqlDbType.NVarChar);
			parameters.Add("@passwd", SqlDbType.NVarChar);
			parameters.Add("@defdb", SqlDbType.NVarChar);
			parameters["@loginame"].Value = loginName;
			parameters["@passwd"].Value = password;
			parameters["@defdb"].Value = defaultDB;
			try
			{
				using (StoreObject storeObject3 = new StoreObject(this))
				{
					storeObject3.ExecuteNonQuery(sqlCommand);
				}
			}
			catch
			{
			}
			sqlCommand = new SqlCommand("sp_grantdbaccess", sqlConnection);
			if (sqlConnection.State != ConnectionState.Open)
			{
				sqlConnection.Open();
			}
			sqlCommand.CommandType = CommandType.StoredProcedure;
			parameters = sqlCommand.Parameters;
			parameters.Add("@loginame", SqlDbType.NVarChar);
			parameters["@loginame"].Value = loginName;
			try
			{
				using (StoreObject storeObject4 = new StoreObject(this))
				{
					storeObject4.ExecuteNonQuery(sqlCommand);
				}
			}
			catch (SqlException ex2)
			{
				if (ex2.Number != 15023)
				{
					throw ex2;
				}
			}
			catch (Exception ex3)
			{
				throw ex3;
			}
			sqlCommand = new SqlCommand("sp_addrolemember", sqlConnection);
			if (sqlConnection.State != ConnectionState.Open)
			{
				sqlConnection.Open();
			}
			sqlCommand.CommandType = CommandType.StoredProcedure;
			parameters = sqlCommand.Parameters;
			parameters.Add("@rolename", SqlDbType.NVarChar);
			parameters.Add("@membername", SqlDbType.NVarChar);
			parameters["@membername"].Value = loginName;
			try
			{
				using (StoreObject storeObject5 = new StoreObject(this))
				{
					foreach (string value in roleNames)
					{
						parameters["@rolename"].Value = value;
						storeObject5.ExecuteNonQuery(sqlCommand);
					}
					return result;
				}
			}
			catch (SqlException ex4)
			{
				throw ex4;
			}
			catch (Exception ex5)
			{
				throw ex5;
			}
		}

		internal void AssertHasClosingDate(DateTime date, string closingPassword)
		{
			if (closeAssertionBeenChecked)
			{
				return;
			}
			DateTime minValue = DateTime.MinValue;
			CompanyInformations companyInformations = new CompanyInformations(this);
			minValue = companyInformations.GetClosingBookDate();
			if (minValue != DateTime.MinValue && minValue != DateTime.MaxValue && minValue >= date)
			{
				if (closingPassword == null)
				{
					closingPassword = string.Empty;
				}
				string closingBookPassword = companyInformations.GetClosingBookPassword();
				if (!configHelper.Cryptor.Decrypt(closingBookPassword).Equals(configHelper.Cryptor.Decrypt(closingPassword)))
				{
					throw new ClosingBookException();
				}
			}
			companyInformations = null;
		}

		public void Dispose()
		{
			if (sqlConnection != null)
			{
				if (sqlConnection.State != 0)
				{
					sqlConnection.Close();
				}
				sqlConnection.Dispose();
				sqlConnection = null;
			}
			if (currentTransaction != null)
			{
				currentTransaction.Dispose();
				currentTransaction = null;
			}
			dbServer = (dbName = (applicationName = (clientMachineName = "")));
			if (configHelper != null)
			{
				configHelper.Dispose();
				configHelper = null;
			}
		}
	}
}
