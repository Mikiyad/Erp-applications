using Micromind.Common;
using Micromind.Common.Data;
using Micromind.Common.Libraries;
using System.Data;
using System.Data.SqlClient;

namespace Micromind.Data
{
	public sealed class Security : StoreObject
	{
		private const string MENUSECURITY_TABLE = "Menu_Security";

		private const string MENUID_PARM = "@MenuID";

		private const string SUBMENUID_PARM = "@SubMenuID";

		private const string DROPDOWNID_PARM = "@DropDownID";

		private const string ENABLE_PARM = "@Enable";

		private const string VISIBLE_PARM = "@Visible";

		private const string USERID_PARM = "@UserID";

		private const string GROUPID_PARM = "@GroupID";

		private const string REPORTTYPE_PARM = "@ReportType";

		private const string TABSSECURITY_TABLE = "Tabs_Security";

		private const string TABID_PARM = "@TabID";

		private const string LINKGROUPID_PARM = "@LinkGroupID";

		private const string LINKID_PARM = "@LinkID";

		private const string SCREENSECURITY_TABLE = "Screen_Security";

		private const string SCREENID_PARM = "@ScreenID";

		private const string VIEW_PARM = "@ViewRight";

		private const string NEW_PARM = "@NewRight";

		private const string EDIT_PARM = "@EditRight";

		private const string DELETE_PARM = "@DeleteRight";

		private const string SECURITYROLEID_PARM = "@SecurityRoleID";

		private const string ISALLOWED_PARM = "@IsAllowed";

		private const string INTVAL_PARM = "@intVal";

		private const string CARDSECURITY_TABLE = "Card_Security";

		private const string CARDID_PARM = "@CardID";

		private const string CONDITIONALQUERY_PARM = "@ConditionalQuery";

		private const string FILTERCONTROL_PARM = "@FilterControl";

		private const string FILTERFROM_PARM = "@FilterFrom";

		private const string FILTERTO_PARM = "@FilterTo";

		public Security(Config config)
			: base(config)
		{
		}

		private string GetGeneralSecurityInsertUpdateText(bool isUpdate)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddInsertUpdateParameters("General_Security", new FieldValue("SecurityRoleID", "@SecurityRoleID", isUpdateConditionField: true), new FieldValue("IsAllowed", "@IsAllowed"), new FieldValue("intVal", "@intVal"), new FieldValue("UserID", "@UserID"), new FieldValue("GroupID", "@GroupID"));
			if (isUpdate)
			{
				return sqlBuilder.GetUpdateExpression();
			}
			return sqlBuilder.GetInsertExpression();
		}

		private SqlCommand GetGeneralSecurityInsertUpdateCommand(bool isUpdate)
		{
			SqlParameterCollection parameters;
			if (isUpdate)
			{
				updateCommand = new SqlCommand(GetGeneralSecurityInsertUpdateText(isUpdate: true), base.DBConfig.Connection);
				updateCommand.CommandType = CommandType.Text;
				parameters = updateCommand.Parameters;
			}
			else
			{
				insertCommand = new SqlCommand(GetGeneralSecurityInsertUpdateText(isUpdate: false), base.DBConfig.Connection);
				insertCommand.CommandType = CommandType.Text;
				parameters = insertCommand.Parameters;
			}
			parameters.Add("@SecurityRoleID", SqlDbType.SmallInt);
			parameters.Add("@IsAllowed", SqlDbType.Bit);
			parameters.Add("@UserID", SqlDbType.NVarChar);
			parameters.Add("@GroupID", SqlDbType.NVarChar);
			parameters.Add("@intVal", SqlDbType.Decimal);
			parameters["@SecurityRoleID"].SourceColumn = "SecurityRoleID";
			parameters["@IsAllowed"].SourceColumn = "IsAllowed";
			parameters["@UserID"].SourceColumn = "UserID";
			parameters["@GroupID"].SourceColumn = "GroupID";
			parameters["@intVal"].SourceColumn = "intVal";
			if (isUpdate)
			{
				return updateCommand;
			}
			return insertCommand;
		}

		private string GetMenuSecurityInsertUpdateText(bool isUpdate)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddInsertUpdateParameters("Menu_Security", new FieldValue("MenuID", "@MenuID", isUpdateConditionField: true), new FieldValue("Enable", "@Enable"), new FieldValue("Visible", "@Visible"), new FieldValue("UserID", "@UserID"), new FieldValue("GroupID", "@GroupID"));
			if (isUpdate)
			{
				return sqlBuilder.GetUpdateExpression();
			}
			return sqlBuilder.GetInsertExpression();
		}

		private SqlCommand GetMenuSecurityInsertUpdateCommand(bool isUpdate)
		{
			SqlParameterCollection parameters;
			if (isUpdate)
			{
				updateCommand = new SqlCommand(GetMenuSecurityInsertUpdateText(isUpdate: true), base.DBConfig.Connection);
				updateCommand.CommandType = CommandType.Text;
				parameters = updateCommand.Parameters;
			}
			else
			{
				insertCommand = new SqlCommand(GetMenuSecurityInsertUpdateText(isUpdate: false), base.DBConfig.Connection);
				insertCommand.CommandType = CommandType.Text;
				parameters = insertCommand.Parameters;
			}
			parameters.Add("@MenuID", SqlDbType.NVarChar);
			parameters.Add("@Enable", SqlDbType.Bit);
			parameters.Add("@Visible", SqlDbType.Bit);
			parameters.Add("@UserID", SqlDbType.NVarChar);
			parameters.Add("@GroupID", SqlDbType.NVarChar);
			parameters["@MenuID"].SourceColumn = "MenuID";
			parameters["@Enable"].SourceColumn = "Enable";
			parameters["@Visible"].SourceColumn = "Visible";
			parameters["@UserID"].SourceColumn = "UserID";
			parameters["@GroupID"].SourceColumn = "GroupID";
			if (isUpdate)
			{
				return updateCommand;
			}
			return insertCommand;
		}

		private string GetCustomReportSecurityInsertUpdateText(bool isUpdate)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddInsertUpdateParameters("Custom_Report_Security", new FieldValue("MenuID", "@MenuID", isUpdateConditionField: true), new FieldValue("Enable", "@Enable"), new FieldValue("Visible", "@Visible"), new FieldValue("UserID", "@UserID"), new FieldValue("ReportType", "@ReportType"), new FieldValue("GroupID", "@GroupID"));
			if (isUpdate)
			{
				return sqlBuilder.GetUpdateExpression();
			}
			return sqlBuilder.GetInsertExpression();
		}

		private SqlCommand GetCustomReportSecurityInsertUpdateCommand(bool isUpdate)
		{
			SqlParameterCollection parameters;
			if (isUpdate)
			{
				updateCommand = new SqlCommand(GetCustomReportSecurityInsertUpdateText(isUpdate: true), base.DBConfig.Connection);
				updateCommand.CommandType = CommandType.Text;
				parameters = updateCommand.Parameters;
			}
			else
			{
				insertCommand = new SqlCommand(GetCustomReportSecurityInsertUpdateText(isUpdate: false), base.DBConfig.Connection);
				insertCommand.CommandType = CommandType.Text;
				parameters = insertCommand.Parameters;
			}
			parameters.Add("@MenuID", SqlDbType.NVarChar);
			parameters.Add("@Enable", SqlDbType.Bit);
			parameters.Add("@Visible", SqlDbType.Bit);
			parameters.Add("@UserID", SqlDbType.NVarChar);
			parameters.Add("@GroupID", SqlDbType.NVarChar);
			parameters.Add("@ReportType", SqlDbType.NVarChar);
			parameters["@MenuID"].SourceColumn = "MenuID";
			parameters["@Enable"].SourceColumn = "Enable";
			parameters["@Visible"].SourceColumn = "Visible";
			parameters["@UserID"].SourceColumn = "UserID";
			parameters["@GroupID"].SourceColumn = "GroupID";
			parameters["@ReportType"].SourceColumn = "ReportType";
			if (isUpdate)
			{
				return updateCommand;
			}
			return insertCommand;
		}

		private string GetTabsSecurityInsertUpdateText(bool isUpdate)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddInsertUpdateParameters("Tabs_Security", new FieldValue("TabID", "@TabID", isUpdateConditionField: true), new FieldValue("Visible", "@Visible"), new FieldValue("UserID", "@UserID"), new FieldValue("GroupID", "@GroupID"));
			if (isUpdate)
			{
				return sqlBuilder.GetUpdateExpression();
			}
			return sqlBuilder.GetInsertExpression();
		}

		private SqlCommand GetTabSecurityInsertUpdateCommand(bool isUpdate)
		{
			SqlParameterCollection parameters;
			if (isUpdate)
			{
				updateCommand = new SqlCommand(GetTabsSecurityInsertUpdateText(isUpdate: true), base.DBConfig.Connection);
				updateCommand.CommandType = CommandType.Text;
				parameters = updateCommand.Parameters;
			}
			else
			{
				insertCommand = new SqlCommand(GetTabsSecurityInsertUpdateText(isUpdate: false), base.DBConfig.Connection);
				insertCommand.CommandType = CommandType.Text;
				parameters = insertCommand.Parameters;
			}
			parameters.Add("@TabID", SqlDbType.NVarChar);
			parameters.Add("@Visible", SqlDbType.Bit);
			parameters.Add("@UserID", SqlDbType.NVarChar);
			parameters.Add("@GroupID", SqlDbType.NVarChar);
			parameters["@TabID"].SourceColumn = "TabID";
			parameters["@Visible"].SourceColumn = "Visible";
			parameters["@UserID"].SourceColumn = "UserID";
			parameters["@GroupID"].SourceColumn = "GroupID";
			if (isUpdate)
			{
				return updateCommand;
			}
			return insertCommand;
		}

		private string GetScreenSecurityInsertUpdateText(bool isUpdate)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddInsertUpdateParameters("Screen_Security", new FieldValue("ScreenID", "@ScreenID"), new FieldValue("ViewRight", "@ViewRight"), new FieldValue("NewRight", "@NewRight"), new FieldValue("EditRight", "@EditRight"), new FieldValue("DeleteRight", "@DeleteRight"), new FieldValue("UserID", "@UserID"), new FieldValue("GroupID", "@GroupID"));
			if (isUpdate)
			{
				return sqlBuilder.GetUpdateExpression();
			}
			return sqlBuilder.GetInsertExpression();
		}

		private SqlCommand GetScreenSecurityInsertUpdateCommand(bool isUpdate)
		{
			SqlParameterCollection parameters;
			if (isUpdate)
			{
				updateCommand = new SqlCommand(GetScreenSecurityInsertUpdateText(isUpdate: true), base.DBConfig.Connection);
				updateCommand.CommandType = CommandType.Text;
				parameters = updateCommand.Parameters;
			}
			else
			{
				insertCommand = new SqlCommand(GetScreenSecurityInsertUpdateText(isUpdate: false), base.DBConfig.Connection);
				insertCommand.CommandType = CommandType.Text;
				parameters = insertCommand.Parameters;
			}
			parameters.Add("@ScreenID", SqlDbType.NVarChar);
			parameters.Add("@ViewRight", SqlDbType.Bit);
			parameters.Add("@NewRight", SqlDbType.Bit);
			parameters.Add("@EditRight", SqlDbType.Bit);
			parameters.Add("@DeleteRight", SqlDbType.Bit);
			parameters.Add("@UserID", SqlDbType.NVarChar);
			parameters.Add("@GroupID", SqlDbType.NVarChar);
			parameters["@ScreenID"].SourceColumn = "ScreenID";
			parameters["@ViewRight"].SourceColumn = "ViewRight";
			parameters["@NewRight"].SourceColumn = "NewRight";
			parameters["@EditRight"].SourceColumn = "EditRight";
			parameters["@DeleteRight"].SourceColumn = "DeleteRight";
			parameters["@UserID"].SourceColumn = "UserID";
			parameters["@GroupID"].SourceColumn = "GroupID";
			if (isUpdate)
			{
				return updateCommand;
			}
			return insertCommand;
		}

		private string GetCardSecurityInsertUpdateText(bool isUpdate)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddInsertUpdateParameters("Card_Security", new FieldValue("CardID", "@CardID", isUpdateConditionField: true), new FieldValue("ConditionalQuery", "@ConditionalQuery"), new FieldValue("FilterControl", "@FilterControl"), new FieldValue("FilterFrom", "@FilterFrom"), new FieldValue("FilterTo", "@FilterTo"), new FieldValue("UserID", "@UserID"), new FieldValue("GroupID", "@GroupID"));
			if (isUpdate)
			{
				return sqlBuilder.GetUpdateExpression();
			}
			return sqlBuilder.GetInsertExpression();
		}

		private SqlCommand GetCardSecurityInsertUpdateCommand(bool isUpdate)
		{
			SqlParameterCollection parameters;
			if (isUpdate)
			{
				updateCommand = new SqlCommand(GetCardSecurityInsertUpdateText(isUpdate: true), base.DBConfig.Connection);
				updateCommand.CommandType = CommandType.Text;
				parameters = updateCommand.Parameters;
			}
			else
			{
				insertCommand = new SqlCommand(GetCardSecurityInsertUpdateText(isUpdate: false), base.DBConfig.Connection);
				insertCommand.CommandType = CommandType.Text;
				parameters = insertCommand.Parameters;
			}
			parameters.Add("@CardID", SqlDbType.Int);
			parameters.Add("@ConditionalQuery", SqlDbType.NVarChar);
			parameters.Add("@FilterControl", SqlDbType.NVarChar);
			parameters.Add("@FilterFrom", SqlDbType.NVarChar);
			parameters.Add("@FilterTo", SqlDbType.NVarChar);
			parameters.Add("@UserID", SqlDbType.NVarChar);
			parameters.Add("@GroupID", SqlDbType.NVarChar);
			parameters["@CardID"].SourceColumn = "CardID";
			parameters["@ConditionalQuery"].SourceColumn = "ConditionalQuery";
			parameters["@FilterControl"].SourceColumn = "FilterControl";
			parameters["@FilterFrom"].SourceColumn = "FilterFrom";
			parameters["@FilterTo"].SourceColumn = "FilterTo";
			parameters["@UserID"].SourceColumn = "UserID";
			parameters["@GroupID"].SourceColumn = "GroupID";
			if (isUpdate)
			{
				return updateCommand;
			}
			return insertCommand;
		}

		public bool InsertSecurity(SecurityData securityData, string userID, string groupID)
		{
			bool flag = true;
			SqlTransaction sqlTransaction = null;
			try
			{
				sqlTransaction = base.DBConfig.StartNewTransaction();
				SqlCommand generalSecurityInsertUpdateCommand = GetGeneralSecurityInsertUpdateCommand(isUpdate: false);
				flag &= DeleteGeneralSecurityRows(sqlTransaction, userID, groupID);
				if (securityData.GeneralSecurityTable.Rows.Count > 0 && securityData.GeneralSecurityTable.Rows.Count > 0)
				{
					generalSecurityInsertUpdateCommand.Transaction = sqlTransaction;
					if (securityData.Tables["General_Security"].Rows.Count > 0)
					{
						flag &= Insert(securityData, "General_Security", generalSecurityInsertUpdateCommand);
					}
				}
				generalSecurityInsertUpdateCommand = GetMenuSecurityInsertUpdateCommand(isUpdate: false);
				flag &= DeleteMenuSecurityRows(sqlTransaction, userID, groupID);
				if (securityData.MenuSecurityTable.Rows.Count > 0 && securityData.MenuSecurityTable.Rows.Count > 0)
				{
					generalSecurityInsertUpdateCommand.Transaction = sqlTransaction;
					if (securityData.Tables["Menu_Security"].Rows.Count > 0)
					{
						flag &= Insert(securityData, "Menu_Security", generalSecurityInsertUpdateCommand);
					}
				}
				generalSecurityInsertUpdateCommand = GetCustomReportSecurityInsertUpdateCommand(isUpdate: false);
				flag &= DeleteCustomReportSecurityRows(sqlTransaction, userID, groupID);
				if (securityData.CustomReportSecurityTable.Rows.Count > 0 && securityData.CustomReportSecurityTable.Rows.Count > 0)
				{
					generalSecurityInsertUpdateCommand.Transaction = sqlTransaction;
					if (securityData.Tables["Custom_Report_Security"].Rows.Count > 0)
					{
						flag &= Insert(securityData, "Custom_Report_Security", generalSecurityInsertUpdateCommand);
					}
				}
				generalSecurityInsertUpdateCommand = GetTabSecurityInsertUpdateCommand(isUpdate: false);
				flag &= DeleteTabSecurityRows(sqlTransaction, userID, groupID);
				if (securityData.TabSecurityTable.Rows.Count > 0 && securityData.TabSecurityTable.Rows.Count > 0)
				{
					generalSecurityInsertUpdateCommand.Transaction = sqlTransaction;
					if (securityData.Tables["Tabs_Security"].Rows.Count > 0)
					{
						flag &= Insert(securityData, "Tabs_Security", generalSecurityInsertUpdateCommand);
					}
				}
				generalSecurityInsertUpdateCommand = GetScreenSecurityInsertUpdateCommand(isUpdate: false);
				flag &= DeleteScreenSecurityRows(sqlTransaction, userID, groupID);
				if (securityData.ScreenSecurityTable.Rows.Count > 0 && securityData.ScreenSecurityTable.Rows.Count > 0)
				{
					generalSecurityInsertUpdateCommand.Transaction = sqlTransaction;
					if (securityData.Tables["Screen_Security"].Rows.Count > 0)
					{
						flag &= Insert(securityData, "Screen_Security", generalSecurityInsertUpdateCommand);
					}
				}
				generalSecurityInsertUpdateCommand = GetCardSecurityInsertUpdateCommand(isUpdate: false);
				flag &= DeleteCardSecurityRows(sqlTransaction, userID, groupID);
				if (securityData.CardSecurityTable.Rows.Count > 0)
				{
					generalSecurityInsertUpdateCommand.Transaction = sqlTransaction;
					if (securityData.Tables["Card_Security"].Rows.Count > 0)
					{
						flag &= Insert(securityData, "Card_Security", generalSecurityInsertUpdateCommand);
					}
				}
				AddActivityLog("Access Right", userID, ActivityTypes.Add, sqlTransaction);
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

		internal bool DeleteMenuSecurityRows(SqlTransaction sqlTransaction, string userID, string groupID)
		{
			bool flag = true;
			try
			{
				string commandText = (!(userID != "")) ? ("DELETE FROM Menu_Security WHERE GroupID = '" + groupID + "'") : ("DELETE FROM Menu_Security WHERE UserID = '" + userID + "'");
				return flag & Delete(commandText, sqlTransaction);
			}
			catch
			{
				throw;
			}
		}

		internal bool DeleteCustomReportSecurityRows(SqlTransaction sqlTransaction, string userID, string groupID)
		{
			bool flag = true;
			try
			{
				string commandText = (!(userID != "")) ? ("DELETE FROM Custom_Report_Security WHERE GroupID = '" + groupID + "'") : ("DELETE FROM Custom_Report_Security WHERE UserID = '" + userID + "'");
				return flag & Delete(commandText, sqlTransaction);
			}
			catch
			{
				throw;
			}
		}

		internal bool DeleteTabSecurityRows(SqlTransaction sqlTransaction, string userID, string groupID)
		{
			bool flag = true;
			try
			{
				string commandText = (!(userID != "")) ? ("DELETE FROM Tabs_Security WHERE GroupID = '" + groupID + "'") : ("DELETE FROM Tabs_Security WHERE UserID = '" + userID + "'");
				return flag & Delete(commandText, sqlTransaction);
			}
			catch
			{
				throw;
			}
		}

		internal bool DeleteScreenSecurityRows(SqlTransaction sqlTransaction, string userID, string groupID)
		{
			bool flag = true;
			try
			{
				string commandText = (!(userID != "")) ? ("DELETE FROM Screen_Security WHERE GroupID = '" + groupID + "'") : ("DELETE FROM Screen_Security WHERE UserID = '" + userID + "'");
				return flag & Delete(commandText, sqlTransaction);
			}
			catch
			{
				throw;
			}
		}

		internal bool DeleteCardSecurityRows(SqlTransaction sqlTransaction, string userID, string groupID)
		{
			bool flag = true;
			try
			{
				string commandText = (!(userID != "")) ? ("DELETE FROM Card_Security WHERE GroupID = '" + groupID + "'") : ("DELETE FROM Card_Security WHERE UserID = '" + userID + "'");
				return flag & Delete(commandText, sqlTransaction);
			}
			catch
			{
				throw;
			}
		}

		internal bool DeleteGeneralSecurityRows(SqlTransaction sqlTransaction, string userID, string groupID)
		{
			bool flag = true;
			try
			{
				string commandText = (!(userID != "")) ? ("DELETE FROM General_Security WHERE GroupID = '" + groupID + "'") : ("DELETE FROM General_Security WHERE UserID = '" + userID + "'");
				return flag & Delete(commandText, sqlTransaction);
			}
			catch
			{
				throw;
			}
		}

		public DataSet GetSecurityDataByID(string userID, bool isGroupLevel)
		{
			DataSet dataSet = new DataSet();
			string text = "UserID";
			if (isGroupLevel)
			{
				text = "GroupID";
			}
			string textCommand = "SELECT SecurityRoleID,IsAllowed,UserID,GroupID,IsNull(intVal,0)AS intVal FROM General_Security WHERE " + text + "='" + userID + "'";
			FillDataSet(dataSet, "General_Security", textCommand);
			textCommand = "SELECT * FROM Menu_Security WHERE " + text + "='" + userID + "'";
			FillDataSet(dataSet, "Menu_Security", textCommand);
			textCommand = "SELECT * FROM Custom_Report_Security WHERE " + text + "='" + userID + "'";
			FillDataSet(dataSet, "Custom_Report_Security", textCommand);
			textCommand = "SELECT * FROM Tabs_Security WHERE " + text + "='" + userID + "'";
			FillDataSet(dataSet, "Tabs_Security", textCommand);
			textCommand = "SELECT * FROM Screen_Security WHERE " + text + "='" + userID + "'";
			FillDataSet(dataSet, "Screen_Security", textCommand);
			textCommand = "SELECT * FROM Card_Security WHERE " + text + "='" + userID + "'";
			FillDataSet(dataSet, "Card_Security", textCommand);
			return dataSet;
		}

		public DataSet GetReportSecurityDataByID(string userID, bool isGroupLevel, bool IsFullView)
		{
			DataSet dataSet = new DataSet();
			string text = "UserID";
			if (isGroupLevel)
			{
				text = "GroupID";
			}
			string textCommand = "select GroupID from User_Group_Detail WHERE " + text + "='" + userID + "'";
			FillDataSet(dataSet, "Groups", textCommand);
			string text2 = "null";
			for (int i = 0; i < dataSet.Tables["Groups"].Rows.Count; i++)
			{
				text2 = "'" + dataSet.Tables["Groups"].Rows[i]["GroupID"].ToString() + "'";
				if (i < dataSet.Tables["Groups"].Rows.Count - 1)
				{
					text2 += ",";
				}
			}
			textCommand = "SELECT SecurityRoleID,IsAllowed,UserID,GroupID,IsNull(intVal,0)AS intVal FROM General_Security WHERE (" + text + "='" + userID + "' OR GroupID IN (" + text2 + "))";
			textCommand += "order by SecurityRoleID,IsAllowed";
			FillDataSet(dataSet, "General_Security", textCommand);
			textCommand = "SELECT * FROM Menu_Security WHERE (" + text + "='" + userID + "' OR GroupID IN (" + text2 + ")) ";
			textCommand += "order by MenuID,Enable,Visible";
			FillDataSet(dataSet, "Menu_Security", textCommand);
			textCommand = "SELECT * FROM Custom_Report_Security WHERE (" + text + "='" + userID + "' OR GroupID IN (" + text2 + "))";
			textCommand += "order by MenuID,Enable,Visible";
			FillDataSet(dataSet, "Custom_Report_Security", textCommand);
			textCommand = "SELECT * FROM Tabs_Security WHERE (" + text + "='" + userID + "' OR GroupID IN (" + text2 + "))";
			textCommand += " order by TabID,Visible";
			FillDataSet(dataSet, "Tabs_Security", textCommand);
			textCommand = "SELECT * FROM Screen_Security WHERE (" + text + "='" + userID + "' OR GroupID IN (" + text2 + "))";
			textCommand += "order by ScreenID,ViewRight,DeleteRight,EditRight,NewRight";
			FillDataSet(dataSet, "Screen_Security", textCommand);
			textCommand = "SELECT * FROM Card_Security WHERE (" + text + "='" + userID + "' OR GroupID IN (" + text2 + "))";
			textCommand += "order by CardID";
			FillDataSet(dataSet, "Card_Security", textCommand);
			return dataSet;
		}

		public string GetUserEmployeeID(string userID)
		{
			return null;
		}

		public SecurityData GetUserSecurityData(string userID)
		{
			SecurityData securityData = new SecurityData();
			string textCommand = "SELECT SecurityRoleID, IsAllowed, UserID,GroupID,IsNull(intVal,0)AS intVal FROM General_Security WHERE UserID = '" + userID + "' OR\r\n                            GroupID IN (SELECT GroupID FROM User_Group_Detail WHERE UserID='" + userID + "')";
			FillDataSet(securityData, "General_Security", textCommand);
			textCommand = "SELECT MenuID,Enable,Visible FROM Menu_Security WHERE UserID = '" + userID + "' OR\r\n                            GroupID IN (SELECT GroupID FROM User_Group_Detail WHERE UserID='" + userID + "')";
			FillDataSet(securityData, "Menu_Security", textCommand);
			textCommand = "SELECT TabID,Visible FROM Tabs_Security WHERE UserID = '" + userID + "' OR\r\n                            GroupID IN (SELECT GroupID FROM User_Group_Detail WHERE UserID='" + userID + "')";
			FillDataSet(securityData, "Tabs_Security", textCommand);
			textCommand = "SELECT ScreenID,ViewRight,NewRight,EditRight,DeleteRight FROM Screen_Security WHERE UserID ='" + userID + "' OR\r\n                      GroupID IN (SELECT GroupID FROM User_Group_Detail WHERE UserID='" + userID + "')";
			FillDataSet(securityData, "Screen_Security", textCommand);
			textCommand = "SELECT MenuID,Enable,Visible,ReportType FROM Custom_Report_Security WHERE UserID = '" + userID + "' OR\r\n                            GroupID IN (SELECT GroupID FROM User_Group_Detail WHERE UserID='" + userID + "')";
			FillDataSet(securityData, "Custom_Report_Security", textCommand);
			textCommand = "SELECT DefaultSalespersonID,DefaultInventoryLocationID,DefaultTransactionLocationID, DefaultTransactionRegisterID FROM Users WHERE UserID ='" + userID + "'";
			FillDataSet(securityData, "Defaults", textCommand);
			return securityData;
		}

		public string GetCurrentUserID()
		{
			return base.DBConfig.UserID;
		}

		public string GetCardSecurityQuery()
		{
			string result = "";
			string exp = "SELECT ConditionalQuery FROM Card_Security WHERE UserID = '" + base.DBConfig.UserID + "' OR GroupID IN (SELECT GroupID FROM User_Group_Detail WHERE UserID = '" + base.DBConfig.UserID + "') Order by GroupID Desc";
			object obj = ExecuteScalar(exp);
			if (obj != null)
			{
				result = obj.ToString();
			}
			return result;
		}
	}
}
