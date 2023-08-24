using Micromind.Common;
using Micromind.Common.Data;
using Micromind.Common.Libraries;
using System.Data;
using System.Data.SqlClient;

namespace Micromind.Data
{
	public sealed class UserGroup : StoreObject
	{
		private const string USERGROUPID_PARM = "@GroupID";

		private const string USERGROUPNAME_PARM = "@GroupName";

		private const string INACTIVE_PARM = "@Inactive";

		public const string NOTE_PARM = "@Note";

		public const string USERGROUP_TABLE = "User_Group";

		private const string CREATEDBY_PARM = "@CreatedBy";

		private const string DATECREATED_PARM = "@DateCreated";

		private const string UPDATEDBY_PARM = "@UpdatedBy";

		private const string DATEUPDATED_PARM = "@DateUpdated";

		private const string USERID_PARM = "@UserID";

		public UserGroup(Config config)
			: base(config)
		{
		}

		private string GetInsertUpdateText(bool isUpdate)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddInsertUpdateParameters("User_Group", new FieldValue("GroupID", "@GroupID", isUpdateConditionField: true), new FieldValue("GroupName", "@GroupName"), new FieldValue("Inactive", "@Inactive"), new FieldValue("Note", "@Note"));
			if (isUpdate)
			{
				sqlBuilder.AddInsertUpdateParameters("User_Group", new FieldValue("DateUpdated", "@DateUpdated", isUpdateConditionField: true, checkForNullValue: true));
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
			parameters.Add("@GroupID", SqlDbType.NVarChar);
			parameters.Add("@GroupName", SqlDbType.NVarChar);
			parameters.Add("@Inactive", SqlDbType.Bit);
			parameters.Add("@Note", SqlDbType.NVarChar);
			parameters["@GroupID"].SourceColumn = "GroupID";
			parameters["@GroupName"].SourceColumn = "GroupName";
			parameters["@Inactive"].SourceColumn = "Inactive";
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

		private string GetDetailsInsertUpdateText(bool isUpdate)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddInsertUpdateParameters("User_Group_Detail", new FieldValue("GroupID", "@GroupID"), new FieldValue("UserID", "@UserID"));
			if (isUpdate)
			{
				return sqlBuilder.GetUpdateExpression();
			}
			return sqlBuilder.GetInsertExpression();
		}

		private SqlCommand GetDetailsInsertUpdateCommand(bool isUpdate)
		{
			SqlParameterCollection parameters;
			if (isUpdate)
			{
				if (updateCommand != null)
				{
					return updateCommand;
				}
				updateCommand = new SqlCommand(GetDetailsInsertUpdateText(isUpdate: true), base.DBConfig.Connection);
				updateCommand.CommandType = CommandType.Text;
				parameters = updateCommand.Parameters;
			}
			else
			{
				if (insertCommand != null)
				{
					return insertCommand;
				}
				insertCommand = new SqlCommand(GetDetailsInsertUpdateText(isUpdate: false), base.DBConfig.Connection);
				insertCommand.CommandType = CommandType.Text;
				parameters = insertCommand.Parameters;
			}
			parameters.Add("@GroupID", SqlDbType.NVarChar);
			parameters.Add("@UserID", SqlDbType.NVarChar);
			parameters["@GroupID"].SourceColumn = "GroupID";
			parameters["@UserID"].SourceColumn = "UserID";
			if (isUpdate)
			{
				return updateCommand;
			}
			return insertCommand;
		}

		public bool InsertUpdateUserGroup(UserGroupData userGroupData, bool isUpdate)
		{
			bool result = true;
			SqlCommand insertUpdateCommand = GetInsertUpdateCommand(isUpdate);
			SqlTransaction sqlTransaction = null;
			try
			{
				sqlTransaction = (insertUpdateCommand.Transaction = base.DBConfig.StartNewTransaction());
				result = ((!isUpdate) ? Insert(userGroupData, "User_Group", insertUpdateCommand) : Update(userGroupData, "User_Group", insertUpdateCommand));
				string text = userGroupData.UserGroupTable.Rows[0]["GroupID"].ToString();
				AddActivityLog("User Group", text, ActivityTypes.Add, sqlTransaction);
				UpdateTableRowInsertUpdateInfo("User_Group", "GroupID", text, sqlTransaction, !isUpdate);
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

		public bool InsertUserGroupDetails(string entityID, UserGroupData userGroupData, bool byUser)
		{
			bool flag = true;
			SqlCommand detailsInsertUpdateCommand = GetDetailsInsertUpdateCommand(isUpdate: false);
			SqlTransaction sqlTransaction = null;
			try
			{
				if (entityID == "")
				{
					throw new CompanyException("User or Group is not provided.");
				}
				sqlTransaction = base.DBConfig.StartNewTransaction();
				string text = "";
				if (byUser)
				{
					text = "DELETE FROM User_Group_Detail WHERE UserID = '" + entityID + "'";
					flag &= Delete(text, sqlTransaction);
				}
				else
				{
					text = "DELETE FROM User_Group_Detail WHERE GroupID = '" + entityID + "'";
					flag &= Delete(text, sqlTransaction);
				}
				if (userGroupData.UserGroupDetailTable.Rows.Count <= 0)
				{
					return flag;
				}
				detailsInsertUpdateCommand.Transaction = sqlTransaction;
				flag &= Insert(userGroupData, "User_Group_Detail", detailsInsertUpdateCommand);
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

		internal bool DeleteUserGroupDetailsRows(SqlTransaction sqlTransaction, string userID)
		{
			bool flag = true;
			try
			{
				string commandText = "DELETE FROM User_Group_Detail WHERE UserID = '" + userID + "'";
				flag = Delete(commandText, sqlTransaction);
				if (!flag)
				{
					return flag;
				}
				AddActivityLog("User Group", userID, ActivityTypes.Delete, null);
				return flag;
			}
			catch
			{
				throw;
			}
		}

		public UserGroupData GetUserGroup()
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddTable("User_Group");
			UserGroupData userGroupData = new UserGroupData();
			sqlBuilder.UseDistinct = false;
			FillDataSet(userGroupData, "User_Group", sqlBuilder);
			return userGroupData;
		}

		public bool DeleteUserGroup(string userGroupID)
		{
			bool flag = true;
			try
			{
				string commandText = "DELETE FROM User_Group WHERE GroupID = '" + userGroupID + "'";
				flag = Delete(commandText, null);
				if (!flag)
				{
					return flag;
				}
				AddActivityLog("User Group", userGroupID, ActivityTypes.Delete, null);
				return flag;
			}
			catch
			{
				throw;
			}
		}

		public UserGroupData GetUserGroupByID(string id)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			CommandHelper commandHelper = null;
			commandHelper = new CommandHelper();
			commandHelper.FieldName = "GroupID";
			commandHelper.SqlFieldType = SqlDbType.NVarChar;
			commandHelper.FieldValue = id;
			commandHelper.TableName = "User_Group";
			sqlBuilder.AddCommandHelper(commandHelper);
			sqlBuilder.IsComparing = true;
			UserGroupData userGroupData = new UserGroupData();
			sqlBuilder.UseDistinct = false;
			FillDataSet(userGroupData, "User_Group", sqlBuilder);
			return userGroupData;
		}

		public DataSet GetUserGroupByFields(params string[] columns)
		{
			return GetUserGroupByFields(null, isInactive: true, columns);
		}

		public DataSet GetUserGroupByFields(string[] userGroupID, params string[] columns)
		{
			return GetUserGroupByFields(userGroupID, isInactive: true, columns);
		}

		public DataSet GetUserGroupByFields(string[] ids, bool isInactive, params string[] columns)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddTable("User_Group");
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
				commandHelper.FieldName = "GroupID";
				commandHelper.FieldValue = ids;
				commandHelper.SqlFieldType = SqlDbType.NVarChar;
				commandHelper.TableName = "User_Group";
				sqlBuilder.AddCommandHelper(commandHelper);
			}
			DataSet dataSet = new DataSet();
			dataSet.EnforceConstraints = false;
			sqlBuilder.UseDistinct = false;
			FillDataSet(dataSet, "User_Group", sqlBuilder);
			return dataSet;
		}

		public DataSet GetUserGroupList()
		{
			DataSet dataSet = new DataSet();
			string textCommand = "SELECT GroupID [Group Code],GroupName [Group Name],Note,Inactive  \r\n                           FROM User_Group ";
			FillDataSet(dataSet, "User_Group", textCommand);
			return dataSet;
		}

		public DataSet GetUserGroupComboList()
		{
			DataSet dataSet = new DataSet();
			string textCommand = "SELECT GroupID [Code],GroupName [Name]\r\n                           FROM User_Group ORDER BY GroupID,GroupName";
			FillDataSet(dataSet, "User_Group", textCommand);
			return dataSet;
		}

		public DataSet GetGroupsByUser(string userID)
		{
			DataSet dataSet = new DataSet();
			string textCommand = "SELECT UG.GroupID,GroupName, \r\n                                CASE (SELECT COUNT(GroupID) FROM User_Group_Detail UGD2 WHERE UserID='" + userID + "' AND \r\n                                UGD2.GroupID=UGD.GroupID) WHEN  0 THEN 'False' ELSE 'True' END  AS IsMember\r\n                                FROM User_Group UG LEFT OUTER JOIN User_Group_Detail UGD \r\n                                ON UG.GroupID=UGD.GroupID AND UGD.UserID='" + userID + "' ORDER BY UG.GroupID";
			FillDataSet(dataSet, "User_Group_Detail", textCommand);
			return dataSet;
		}

		public DataSet GetUsersByGroup(string groupID)
		{
			DataSet dataSet = new DataSet();
			string textCommand = "SELECT UG.UserID,UserName, \r\n                                CASE (SELECT COUNT(GroupID) FROM User_Group_Detail UGD2 WHERE GroupID='" + groupID + "' AND \r\n                                UGD2.GroupID=UGD.GroupID) WHEN  0 THEN 'False' ELSE 'True' END  AS IsMember\r\n                                FROM Users UG LEFT OUTER JOIN User_Group_Detail UGD \r\n                                ON UG.UserID = UGD.UserID AND UGD.GroupID= '" + groupID + "'\r\n\t\t\t\t\t\t\t\tORDER BY IsMember DESC,UserID ";
			FillDataSet(dataSet, "User_Group_Detail", textCommand);
			return dataSet;
		}
	}
}
