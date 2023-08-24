using Micromind.Common;
using Micromind.Common.Data;
using Micromind.Common.Libraries;
using Microsoft.SqlServer.Management.Smo;
using System;
using System.Data;
using System.Data.SqlClient;

namespace Micromind.Data
{
	public sealed class User : StoreObject
	{
		private const string USERID_PARM = "@UserID";

		private const string USERNAME_PARM = "@UserName";

		private const string EMPLOYEEID_PARM = "@EmployeeID";

		private const string LOCATIONID_PARM = "@LocationID";

		private const string DEFAULTSALESPERSON_PARM = "@DefaultSalesPersonID";

		private const string DEFAULTINVENTORYLOCATION_PARM = "@DefaultInventoryLocationID";

		private const string DEFAULTTRANSACTIONLOCATION_PARM = "@DefaultTransactionLocationID";

		private const string DEFAULTTRANSACTIONREGISTER_PARM = "@DefaultTransactionRegisterID";

		private const string PHONE_PARM = "@Phone";

		private const string EMAIL_PARM = "@Email";

		private const string INACTIVE_PARM = "@Inactive";

		private const string ISCLUSER_PARM = "@IsCLUser";

		private const string CLUSERPASS_PARM = "@CLUserPass";

		private const string ISADMIN_PARM = "@IsAdmin";

		private const string NOTE_PARM = "@Note";

		private const string USER_TABLE = "Users";

		private const string CREATEDBY_PARM = "@CreatedBy";

		private const string DATECREATED_PARM = "@DateCreated";

		private const string UPDATEDBY_PARM = "@UpdatedBy";

		private const string DATEUPDATED_PARM = "@DateUpdated";

		public User(Config config)
			: base(config)
		{
		}

		private string GetInsertUpdateText(bool isUpdate)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddInsertUpdateParameters("Users", new FieldValue("UserID", "@UserID", isUpdateConditionField: true), new FieldValue("UserName", "@UserName"), new FieldValue("EmployeeID", "@EmployeeID"), new FieldValue("LocationID", "@LocationID"), new FieldValue("DefaultSalesPersonID", "@DefaultSalesPersonID"), new FieldValue("DefaultInventoryLocationID", "@DefaultInventoryLocationID"), new FieldValue("DefaultTransactionLocationID", "@DefaultTransactionLocationID"), new FieldValue("DefaultTransactionRegisterID", "@DefaultTransactionRegisterID"), new FieldValue("Phone", "@Phone"), new FieldValue("Email", "@Email"), new FieldValue("IsCLUser", "@IsCLUser"), new FieldValue("CLUserPass", "@CLUserPass"), new FieldValue("Inactive", "@Inactive"), new FieldValue("IsAdmin", "@IsAdmin"), new FieldValue("Note", "@Note"));
			if (isUpdate)
			{
				sqlBuilder.AddInsertUpdateParameters("Users", new FieldValue("DateUpdated", "@DateUpdated", isUpdateConditionField: true, checkForNullValue: true));
			}
			if (isUpdate)
			{
				return sqlBuilder.GetUpdateExpression();
			}
			return sqlBuilder.GetInsertExpression();
		}

		private SqlCommand GetInsertUpdateCommand(bool isUpdate)
		{
			SqlParameterCollection parameters;
			if (isUpdate)
			{
				if (updateCommand != null)
				{
					return updateCommand;
				}
				updateCommand = new SqlCommand(GetInsertUpdateText(isUpdate: true), base.DBConfig.Connection);
				updateCommand.CommandType = CommandType.Text;
				parameters = updateCommand.Parameters;
			}
			else
			{
				if (insertCommand != null)
				{
					return insertCommand;
				}
				insertCommand = new SqlCommand(GetInsertUpdateText(isUpdate: false), base.DBConfig.Connection);
				insertCommand.CommandType = CommandType.Text;
				parameters = insertCommand.Parameters;
			}
			parameters.Add("@UserID", SqlDbType.NVarChar);
			parameters.Add("@UserName", SqlDbType.NVarChar);
			parameters.Add("@EmployeeID", SqlDbType.NVarChar);
			parameters.Add("@LocationID", SqlDbType.NVarChar);
			parameters.Add("@DefaultSalesPersonID", SqlDbType.NVarChar);
			parameters.Add("@DefaultInventoryLocationID", SqlDbType.NVarChar);
			parameters.Add("@DefaultTransactionLocationID", SqlDbType.NVarChar);
			parameters.Add("@DefaultTransactionRegisterID", SqlDbType.NVarChar);
			parameters.Add("@Phone", SqlDbType.NVarChar);
			parameters.Add("@IsCLUser", SqlDbType.Bit);
			parameters.Add("@CLUserPass", SqlDbType.NVarChar);
			parameters.Add("@Email", SqlDbType.NVarChar);
			parameters.Add("@Inactive", SqlDbType.Bit);
			parameters.Add("@IsAdmin", SqlDbType.Bit);
			parameters.Add("@Note", SqlDbType.NVarChar);
			parameters["@UserID"].SourceColumn = "UserID";
			parameters["@UserName"].SourceColumn = "UserName";
			parameters["@EmployeeID"].SourceColumn = "EmployeeID";
			parameters["@LocationID"].SourceColumn = "LocationID";
			parameters["@DefaultSalesPersonID"].SourceColumn = "DefaultSalesPersonID";
			parameters["@DefaultInventoryLocationID"].SourceColumn = "DefaultInventoryLocationID";
			parameters["@DefaultTransactionLocationID"].SourceColumn = "DefaultTransactionLocationID";
			parameters["@DefaultTransactionRegisterID"].SourceColumn = "DefaultTransactionRegisterID";
			parameters["@Phone"].SourceColumn = "Phone";
			parameters["@Email"].SourceColumn = "Email";
			parameters["@IsCLUser"].SourceColumn = "IsCLUser";
			parameters["@CLUserPass"].SourceColumn = "CLUserPass";
			parameters["@Inactive"].SourceColumn = "Inactive";
			parameters["@IsAdmin"].SourceColumn = "IsAdmin";
			parameters["@Note"].SourceColumn = "Note";
			if (isUpdate)
			{
				parameters.Add("@DateUpdated", SqlDbType.DateTime);
				parameters["@DateUpdated"].SourceColumn = "DateUpdated";
			}
			if (isUpdate)
			{
				return updateCommand;
			}
			return insertCommand;
		}

		public bool InsertUpdateUser(UserData userData, bool isUpdate)
		{
			bool flag = true;
			SqlCommand insertUpdateCommand = GetInsertUpdateCommand(isUpdate);
			SqlTransaction sqlTransaction = null;
			try
			{
				string text = "";
				string text2 = "";
				if (!isUpdate)
				{
					text2 = userData.Tables["Users"].Rows[0]["UserID"].ToString();
					text = userData.Tables["Users"].Rows[0]["Password"].ToString();
					if (new Databases(base.DBConfig).ExistFieldValue("Users", "UserID", text2))
					{
						throw new CompanyException("User ID already exists. Please enter another user ID.");
					}
					string exp = "SELECT COUNT(*) from master.dbo.syslogins WHERE LoginName = '" + text2 + "'";
					object obj = ExecuteScalar(exp);
					bool flag2 = false;
					if (obj != null && int.Parse(obj.ToString()) > 0)
					{
						flag2 = true;
					}
					if (!flag2)
					{
						if (text2.ToLower().Trim() != "sa")
						{
							text = CommonLib.GetAxolonPassword(base.CryptorID, text, isEncrypted: false);
						}
						exp = "CREATE LOGIN " + text2 + "  WITH PASSWORD = '" + text + "' , Check_Policy = OFF";
						flag &= (ExecuteNonQuery(exp) >= 0);
					}
					exp = " USE " + base.DBConfig.DatabaseName + " SELECT COUNT(*) FROM sys.database_principals WHERE Name = '" + text2 + "'";
					obj = ExecuteScalar(exp);
					if (obj == null || int.Parse(obj.ToString()) == 0)
					{
						exp = " USE " + base.DBConfig.DatabaseName + " EXEC SP_ADDUSER '" + text2 + "','" + text2 + "','db_owner'";
						flag &= (ExecuteNonQuery(exp) >= 0);
					}
					exp = " USE master SELECT COUNT(*) FROM sys.database_principals WHERE Name = '" + text2 + "'";
					obj = ExecuteScalar(exp);
					if (obj == null || int.Parse(obj.ToString()) == 0)
					{
						exp = "USE master  CREATE USER " + text2 + " FOR LOGIN " + text2;
						exp = exp + " EXEC sp_addrolemember 'db_owner', '" + text2 + "' ";
						flag &= (ExecuteNonQuery(exp) >= 0);
					}
				}
				string text3 = "";
				bool num = bool.Parse(userData.Tables["Users"].Rows[0]["IsAdmin"].ToString());
				text2 = userData.Tables["Users"].Rows[0]["UserID"].ToString();
				if (num)
				{
					text3 = " USE " + base.DBConfig.DatabaseName + " EXEC sp_addsrvrolemember @loginame ='" + text2 + "', @rolename ='sysadmin'";
					flag &= (ExecuteNonQuery(text3) >= 0);
				}
				else
				{
					text3 = " USE " + base.DBConfig.DatabaseName + " EXEC sp_dropsrvrolemember @loginame ='" + text2 + "', @rolename ='sysadmin'";
					flag &= (ExecuteNonQuery(text3) >= 0);
				}
				sqlTransaction = (insertUpdateCommand.Transaction = base.DBConfig.StartNewTransaction());
				userData.UserTable.Rows[0]["Password"] = text;
				flag = ((!isUpdate) ? Insert(userData, "Users", insertUpdateCommand) : Update(userData, "Users", insertUpdateCommand));
				string text4 = userData.UserTable.Rows[0]["UserID"].ToString();
				if (isUpdate)
				{
					AddActivityLog("User", text4, ActivityTypes.Update, sqlTransaction);
				}
				else
				{
					AddActivityLog("User", text4, ActivityTypes.Add, sqlTransaction);
				}
				UpdateTableRowInsertUpdateInfo("Users", "UserID", text4, sqlTransaction, !isUpdate);
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

		public bool ExistSQLLogin(string loginID)
		{
			try
			{
				Server server = new Server(base.DBConfig.ServerName);
				new Login(server, loginID);
				if (server.Logins.Contains(loginID))
				{
					return true;
				}
				return false;
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		public bool ChangePassword(string loginId, string newPassword, string oldPassword)
		{
			bool result = true;
			SqlTransaction transaction = base.DBConfig.StartNewTransaction();
			try
			{
				if (loginId.ToLower() != "sa")
				{
					newPassword = CommonLib.GetAxolonPassword(base.CryptorID, newPassword, isEncrypted: false);
				}
				if (loginId.ToLower() != "sa" && oldPassword != "")
				{
					oldPassword = CommonLib.GetAxolonPassword(base.CryptorID, oldPassword, isEncrypted: false);
				}
				string text = "ALTER LOGIN " + loginId + " WITH PASSWORD = N'" + newPassword + "'";
				if (oldPassword != "")
				{
					text = text + " OLD_PASSWORD = N'" + oldPassword + "'";
				}
				SqlCommand sqlCommand = new SqlCommand(text);
				sqlCommand.Transaction = transaction;
				ExecuteNonQuery(sqlCommand);
				return result;
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

		public UserData GetUser()
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddTable("Users");
			UserData userData = new UserData();
			sqlBuilder.UseDistinct = false;
			FillDataSet(userData, "Users", sqlBuilder);
			return userData;
		}

		public bool DeleteUser(string userID)
		{
			bool flag = true;
			try
			{
				string commandText = "DELETE FROM Users WHERE UserID = '" + userID + "'";
				flag = Delete(commandText, null);
				if (!flag)
				{
					return flag;
				}
				AddActivityLog("User", userID, ActivityTypes.Delete, null);
				return flag;
			}
			catch
			{
				throw;
			}
		}

		public UserData GetUserByID(string id)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			CommandHelper commandHelper = null;
			commandHelper = new CommandHelper();
			commandHelper.FieldName = "UserID";
			commandHelper.SqlFieldType = SqlDbType.NVarChar;
			commandHelper.FieldValue = id;
			commandHelper.TableName = "Users";
			sqlBuilder.AddCommandHelper(commandHelper);
			sqlBuilder.IsComparing = true;
			UserData userData = new UserData();
			sqlBuilder.UseDistinct = false;
			FillDataSet(userData, "Users", sqlBuilder);
			return userData;
		}

		public DataSet GetUserByFields(params string[] columns)
		{
			return GetUserByFields(null, isInactive: true, columns);
		}

		public DataSet GetUserByFields(string[] userID, params string[] columns)
		{
			return GetUserByFields(userID, isInactive: true, columns);
		}

		public DataSet GetUserByFields(string[] ids, bool isInactive, params string[] columns)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddTable("Users");
			foreach (string text in columns)
			{
				ApplicationAssert.CheckCondition(text.IndexOf(".") >= 0, "A table name with the column name must be specified. eg. table.column.", 0);
				string tableName = text.Substring(0, text.IndexOf("."));
				string columnName = text.Substring(text.IndexOf(".") + 1);
				sqlBuilder.AddColumn(tableName, columnName);
			}
			if (ids != null && ids.Length != 0)
			{
				CommandHelper commandHelper = new CommandHelper();
				commandHelper.FieldName = "UserID";
				commandHelper.FieldValue = ids;
				commandHelper.SqlFieldType = SqlDbType.NVarChar;
				commandHelper.TableName = "Users";
				sqlBuilder.AddCommandHelper(commandHelper);
			}
			DataSet dataSet = new DataSet();
			dataSet.EnforceConstraints = false;
			sqlBuilder.UseDistinct = false;
			FillDataSet(dataSet, "Users", sqlBuilder);
			return dataSet;
		}

		public DataSet GetCLUserList()
		{
			DataSet dataSet = new DataSet();
			string textCommand = "SELECT UserID ,CLUserPass,Email\r\n                           FROM Users WHERE ISNULL(IsCLUser,'False')='True' ";
			FillDataSet(dataSet, "Users", textCommand);
			return dataSet;
		}

		public string GetUserByCLPass(string pass)
		{
			string exp = "SELECT UserID \r\n                           FROM Users WHERE ISNULL(IsCLUser,'False')='True' AND CLUserPass = '" + CommonLib.Encrypt(base.CryptorID, pass) + "'";
			object obj = ExecuteScalar(exp);
			if (obj.IsNullOrEmpty())
			{
				return "";
			}
			return obj.ToString();
		}

		public DataSet GetUserList()
		{
			DataSet dataSet = new DataSet();
			string textCommand = "SELECT UserID [User Code],UserName [User Name],Note,Inactive  \r\n                           FROM Users ";
			FillDataSet(dataSet, "Users", textCommand);
			return dataSet;
		}

		public DataSet GetUserComboList()
		{
			DataSet dataSet = new DataSet();
			string textCommand = "SELECT UPPER(UserID) AS [Code],UserName [Name]\r\n                           FROM Users ORDER BY UserID,UserName";
			FillDataSet(dataSet, "Users", textCommand);
			return dataSet;
		}

		public string GetUserLocationByID(string userID)
		{
			object fieldValue = new Databases(base.DBConfig).GetFieldValue("Users", "DefaultTransactionLocationID", "UserID", userID, null);
			if (fieldValue == null)
			{
				return string.Empty;
			}
			return fieldValue.ToString();
		}
	}
}
