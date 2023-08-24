using Micromind.Common;
using Micromind.Common.Data;
using Micromind.Common.Libraries;
using System;
using System.Data;
using System.Data.SqlClient;

namespace Micromind.Data
{
	public sealed class AccountGroups : StoreObject
	{
		private const string GROUPID_PARM = "@GroupID";

		private const string GROUPNAME_PARM = "@GroupName";

		private const string PARENTID_PARM = "@ParentID";

		private const string NOTE_PARM = "@NOTE";

		private const string INACTIVE_PARM = "@Inactive";

		private const string TYPEID_PARM = "@TypeID";

		private const string CREATEDBY_PARM = "@CreatedBy";

		private const string DATECREATED_PARM = "@DateCreated";

		private const string UPDATEDBY_PARM = "@UpdatedBy";

		private const string DATEUPDATED_PARM = "@DateUpdated";

		public AccountGroups(Config config)
			: base(config)
		{
		}

		private string GetInsertUpdateText(bool isUpdate)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddInsertUpdateParameters("Account_Group", new FieldValue("GroupID", "@GroupID", isUpdateConditionField: true), new FieldValue("GroupName", "@GroupName"), new FieldValue("ParentID", "@ParentID"), new FieldValue("Note", "@NOTE"), new FieldValue("Inactive", "@Inactive"), new FieldValue("TypeID", "@TypeID"));
			if (isUpdate)
			{
				sqlBuilder.AddInsertUpdateParameters("Account_Group", new FieldValue("DateUpdated", "@DateUpdated", isUpdateConditionField: true, checkForNullValue: true));
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
			parameters.Add("@ParentID", SqlDbType.NVarChar);
			parameters.Add("@NOTE", SqlDbType.NVarChar);
			parameters.Add("@TypeID", SqlDbType.Int);
			parameters.Add("@Inactive", SqlDbType.Bit);
			parameters["@GroupID"].SourceColumn = "GroupID";
			parameters["@GroupName"].SourceColumn = "GroupName";
			parameters["@ParentID"].SourceColumn = "ParentID";
			parameters["@NOTE"].SourceColumn = "Note";
			parameters["@TypeID"].SourceColumn = "TypeID";
			parameters["@Inactive"].SourceColumn = "Inactive";
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

		public bool InsertAccountGroup(AccountGroupsData accountGroupsData)
		{
			bool result = true;
			SqlCommand insertUpdateCommand = GetInsertUpdateCommand(isUpdate: false);
			SqlTransaction sqlTransaction = null;
			try
			{
				sqlTransaction = (insertUpdateCommand.Transaction = base.DBConfig.StartNewTransaction());
				result = Insert(accountGroupsData, "Account_Group", insertUpdateCommand);
				string text = accountGroupsData.AccountGroupsTable.Rows[0]["GroupID"].ToString();
				AddActivityLog("Account Group", text, ActivityTypes.Add, sqlTransaction);
				UpdateTableRowInsertUpdateInfo("Account_Group", "GroupID", text, sqlTransaction, isInsert: true);
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

		public bool UpdateAccountGroup(AccountGroupsData accountGroupsData)
		{
			bool flag = true;
			SqlCommand insertUpdateCommand = GetInsertUpdateCommand(isUpdate: true);
			SqlTransaction sqlTransaction = null;
			try
			{
				sqlTransaction = (insertUpdateCommand.Transaction = base.DBConfig.StartNewTransaction());
				flag = Update(accountGroupsData, "Account_Group", insertUpdateCommand);
				if (!flag)
				{
					return flag;
				}
				object obj = accountGroupsData.AccountGroupsTable.Rows[0]["GroupID"];
				UpdateTableRowByID("Account_Group", "GroupID", "DateUpdated", obj, DateTime.Now, sqlTransaction);
				string entiyID = accountGroupsData.AccountGroupsTable.Rows[0]["GroupName"].ToString();
				AddActivityLog("Account Group", entiyID, ActivityTypes.Update, sqlTransaction);
				UpdateTableRowInsertUpdateInfo("Account_Group", "GroupID", obj, sqlTransaction, isInsert: false);
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

		public AccountGroupsData GetAccountGroups()
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddTable("Account_Group");
			AccountGroupsData accountGroupsData = new AccountGroupsData();
			sqlBuilder.UseDistinct = false;
			FillDataSet(accountGroupsData, "Account_Group", sqlBuilder);
			return accountGroupsData;
		}

		public bool DeleteAccountGroup(string groupID)
		{
			bool flag = true;
			try
			{
				string commandText = "DELETE FROM Account_Group WHERE GroupID = '" + groupID + "'";
				flag = Delete(commandText, null);
				if (!flag)
				{
					return flag;
				}
				AddActivityLog("Account Group", groupID, ActivityTypes.Delete, null);
				return flag;
			}
			catch
			{
				throw;
			}
		}

		public AccountGroupsData GetAccountGroupByID(string id)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			CommandHelper commandHelper = null;
			commandHelper = new CommandHelper();
			commandHelper.FieldName = "GroupID";
			commandHelper.SqlFieldType = SqlDbType.NVarChar;
			commandHelper.FieldValue = id;
			commandHelper.TableName = "Account_Group";
			sqlBuilder.AddCommandHelper(commandHelper);
			sqlBuilder.IsComparing = true;
			AccountGroupsData accountGroupsData = new AccountGroupsData();
			sqlBuilder.UseDistinct = false;
			FillDataSet(accountGroupsData, "Account_Group", sqlBuilder);
			return accountGroupsData;
		}

		public int GetGroupTypeID(string groupID)
		{
			try
			{
				object obj = ExecuteSelectScalar("Account_Group", "GroupID", groupID, "TypeID");
				if (obj != null && obj != DBNull.Value)
				{
					return int.Parse(obj.ToString());
				}
			}
			catch
			{
				throw;
			}
			return -1;
		}

		public DataSet GetAccountGroupsByFields(params string[] columns)
		{
			return GetAccountGroupsByFields(null, isInactive: true, columns);
		}

		public DataSet GetAccountGroupsByFields(string[] groupID, params string[] columns)
		{
			return GetAccountGroupsByFields(groupID, isInactive: true, columns);
		}

		public DataSet GetAccountGroupsByFields(string[] ids, bool isInactive, params string[] columns)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddTable("Account_Group");
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
				commandHelper.TableName = "Account_Group";
				sqlBuilder.AddCommandHelper(commandHelper);
			}
			if (!isInactive)
			{
				CommandHelper commandHelper2 = new CommandHelper();
				commandHelper2.FieldName = "Inactive";
				commandHelper2.FieldValue = 0;
				commandHelper2.SqlFieldType = SqlDbType.NVarChar;
				commandHelper2.TableName = "Account_Group";
				sqlBuilder.AddCommandHelper(commandHelper2);
			}
			DataSet dataSet = new DataSet();
			dataSet.EnforceConstraints = false;
			sqlBuilder.UseDistinct = false;
			FillDataSet(dataSet, "Account_Group", sqlBuilder);
			return dataSet;
		}

		public DataSet GetAccountGroupsList()
		{
			DataSet dataSet = new DataSet();
			string textCommand = "SELECT GroupID [Code],GroupName [Name],AT.AccountTypeName AS [Type],ParentID [Parent],Note,Inactive [Inactive]\r\n                           FROM Account_Group LEFT OUTER JOIN Account_Type AT ON \r\n                            Account_Group.TypeID=AT.TypeID ORDER BY Type,Code,Name";
			FillDataSet(dataSet, "Account_Group", textCommand);
			return dataSet;
		}

		public DataSet GetAccountGroupsComboList()
		{
			DataSet dataSet = new DataSet();
			string textCommand = "SELECT GroupID [Code],GroupName [Name],AT.AccountTypeName AS [Type],Account_Group.TypeID FROM Account_Group LEFT OUTER JOIN Account_Type AT ON \r\n                            Account_Group.TypeID=AT.TypeID WHERE \r\n                                 Inactive <> 1  ORDER BY GroupID,GroupName";
			FillDataSet(dataSet, "Account_Group", textCommand);
			return dataSet;
		}
	}
}
