using Micromind.Common;
using Micromind.Common.Data;
using Micromind.Common.Libraries;
using System;
using System.Data;
using System.Data.SqlClient;

namespace Micromind.Data
{
	public sealed class AnalysisGroups : StoreObject
	{
		private const string GROUPID_PARM = "@GroupID";

		private const string GROUPNAME_PARM = "@GroupName";

		private const string DESCRIPTION_PARM = "@Description";

		private const string INACTIVE_PARM = "@Inactive";

		private const string CREATEDBY_PARM = "@CreatedBy";

		private const string DATECREATED_PARM = "@DateCreated";

		private const string UPDATEDBY_PARM = "@UpdatedBy";

		private const string DATEUPDATED_PARM = "@DateUpdated";

		public AnalysisGroups(Config config)
			: base(config)
		{
		}

		private string GetInsertUpdateText(bool isUpdate)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddInsertUpdateParameters("Analysis_Group", new FieldValue("GroupID", "@GroupID", isUpdateConditionField: true), new FieldValue("GroupName", "@GroupName"), new FieldValue("Description", "@Description"), new FieldValue("Inactive", "@Inactive"));
			if (isUpdate)
			{
				sqlBuilder.AddInsertUpdateParameters("Analysis_Group", new FieldValue("DateUpdated", "@DateUpdated", isUpdateConditionField: true, checkForNullValue: true));
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
			parameters.Add("@Description", SqlDbType.NVarChar);
			parameters.Add("@Inactive", SqlDbType.Bit);
			parameters["@GroupID"].SourceColumn = "GroupID";
			parameters["@GroupName"].SourceColumn = "GroupName";
			parameters["@Description"].SourceColumn = "Description";
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

		public bool InsertAnalysisGroup(AnalysisGroupsData analysisGroupsData)
		{
			bool result = true;
			SqlCommand insertUpdateCommand = GetInsertUpdateCommand(isUpdate: false);
			SqlTransaction sqlTransaction = null;
			try
			{
				sqlTransaction = (insertUpdateCommand.Transaction = base.DBConfig.StartNewTransaction());
				result = Insert(analysisGroupsData, "Analysis_Group", insertUpdateCommand);
				string text = analysisGroupsData.AnalysisGroupsTable.Rows[0]["GroupID"].ToString();
				AddActivityLog("Analysis Group", text, ActivityTypes.Add, sqlTransaction);
				UpdateTableRowInsertUpdateInfo("Analysis_Group", "GroupID", text, sqlTransaction, isInsert: true);
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

		public bool UpdateAnalysisGroup(AnalysisGroupsData analysisGroupsData)
		{
			bool flag = true;
			SqlCommand insertUpdateCommand = GetInsertUpdateCommand(isUpdate: true);
			SqlTransaction sqlTransaction = null;
			try
			{
				sqlTransaction = (insertUpdateCommand.Transaction = base.DBConfig.StartNewTransaction());
				flag = Update(analysisGroupsData, "Analysis_Group", insertUpdateCommand);
				if (!flag)
				{
					return flag;
				}
				object obj = analysisGroupsData.AnalysisGroupsTable.Rows[0]["GroupID"];
				UpdateTableRowByID("Analysis_Group", "GroupID", "DateUpdated", obj, DateTime.Now, sqlTransaction);
				string entiyID = analysisGroupsData.AnalysisGroupsTable.Rows[0]["GroupName"].ToString();
				AddActivityLog("Analysis Group", entiyID, ActivityTypes.Update, sqlTransaction);
				UpdateTableRowInsertUpdateInfo("Analysis_Group", "GroupID", obj, sqlTransaction, isInsert: false);
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

		public AnalysisGroupsData GetAnalysisGroups()
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddTable("Analysis_Group");
			AnalysisGroupsData analysisGroupsData = new AnalysisGroupsData();
			sqlBuilder.UseDistinct = false;
			FillDataSet(analysisGroupsData, "Analysis_Group", sqlBuilder);
			return analysisGroupsData;
		}

		public bool DeleteAnalysisGroup(string groupID)
		{
			bool flag = true;
			try
			{
				string commandText = "DELETE FROM Analysis_Group WHERE GroupID = '" + groupID + "'";
				flag = Delete(commandText, null);
				if (!flag)
				{
					return flag;
				}
				AddActivityLog("Analysis Group", groupID, ActivityTypes.Delete, null);
				return flag;
			}
			catch
			{
				throw;
			}
		}

		public AnalysisGroupsData GetAnalysisGroupByID(string id)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			CommandHelper commandHelper = null;
			commandHelper = new CommandHelper();
			commandHelper.FieldName = "GroupID";
			commandHelper.SqlFieldType = SqlDbType.NVarChar;
			commandHelper.FieldValue = id;
			commandHelper.TableName = "Analysis_Group";
			sqlBuilder.AddCommandHelper(commandHelper);
			sqlBuilder.IsComparing = true;
			AnalysisGroupsData analysisGroupsData = new AnalysisGroupsData();
			sqlBuilder.UseDistinct = false;
			FillDataSet(analysisGroupsData, "Analysis_Group", sqlBuilder);
			return analysisGroupsData;
		}

		public DataSet GetAnalysisGroupsByFields(params string[] columns)
		{
			return GetAnalysisGroupsByFields(null, isInactive: true, columns);
		}

		public DataSet GetAnalysisGroupsByFields(string[] groupID, params string[] columns)
		{
			return GetAnalysisGroupsByFields(groupID, isInactive: true, columns);
		}

		public DataSet GetAnalysisGroupsByFields(string[] ids, bool isInactive, params string[] columns)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddTable("Analysis_Group");
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
				commandHelper.TableName = "Analysis_Group";
				sqlBuilder.AddCommandHelper(commandHelper);
			}
			if (!isInactive)
			{
				CommandHelper commandHelper2 = new CommandHelper();
				commandHelper2.FieldName = "Inactive";
				commandHelper2.FieldValue = 0;
				commandHelper2.SqlFieldType = SqlDbType.NVarChar;
				commandHelper2.TableName = "Analysis_Group";
				sqlBuilder.AddCommandHelper(commandHelper2);
			}
			DataSet dataSet = new DataSet();
			dataSet.EnforceConstraints = false;
			sqlBuilder.UseDistinct = false;
			FillDataSet(dataSet, "Analysis_Group", sqlBuilder);
			return dataSet;
		}

		public DataSet GetAnalysisGroupsList()
		{
			DataSet dataSet = new DataSet();
			string textCommand = "SELECT GroupID [Code],GroupName [Name],Description, Inactive\r\n                           FROM Analysis_Group ORDER BY GroupID,GroupName";
			FillDataSet(dataSet, "Analysis_Group", textCommand);
			return dataSet;
		}

		public DataSet GetAnalysisGroupsComboList()
		{
			DataSet dataSet = new DataSet();
			string textCommand = "SELECT GroupID [Code],GroupName [Name]\r\n                           FROM Analysis_Group WHERE Inactive<>1 ORDER BY GroupID,GroupName";
			FillDataSet(dataSet, "Analysis_Group", textCommand);
			return dataSet;
		}
	}
}
