using Micromind.Common;
using Micromind.Common.Data;
using Micromind.Common.Libraries;
using System;
using System.Data;
using System.Data.SqlClient;

namespace Micromind.Data
{
	public sealed class CustomFieldGroups : StoreObject
	{
		private const string CUSTOMFIELDGROUPID_PARM = "@CustomFieldGroupID";

		private const string CUSTOMFIELDGROUPNAME_PARM = "@CustomFieldGroupName";

		private const string DATETIMESTAMP_PARM = "@DateTimeStamp";

		public bool CheckConcurrency = true;

		public CustomFieldGroups(Config config)
			: base(config)
		{
		}

		private string GetInsertText()
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddInsertUpdateParameters("[Custom Field Groups]", new FieldValue("CustomFieldGroupName", "@CustomFieldGroupName"));
			return sqlBuilder.GetInsertExpression();
		}

		private string GetUpdateText()
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddInsertUpdateParameters("[Custom Field Groups]", new FieldValue("CustomFieldGroupID", "@CustomFieldGroupID", isUpdateConditionField: true), new FieldValue("CustomFieldGroupName", "@CustomFieldGroupName"));
			if (CheckConcurrency)
			{
				sqlBuilder.AddInsertUpdateParameters("[Custom Field Groups]", new FieldValue("DateTimeStamp", "@DateTimeStamp", isUpdateConditionField: true, checkForNullValue: true));
			}
			return sqlBuilder.GetUpdateExpression();
		}

		private SqlCommand GetInsertCommand()
		{
			if (insertCommand == null)
			{
				insertCommand = new SqlCommand(GetInsertText(), base.DBConfig.Connection);
				insertCommand.CommandType = CommandType.Text;
				SqlParameterCollection parameters = insertCommand.Parameters;
				parameters.Add("@CustomFieldGroupName", SqlDbType.NVarChar);
				parameters["@CustomFieldGroupName"].SourceColumn = "CustomFieldGroupName";
			}
			return insertCommand;
		}

		private SqlCommand GetUpdateCommand()
		{
			if (updateCommand == null)
			{
				updateCommand = new SqlCommand(GetUpdateText(), base.DBConfig.Connection);
				updateCommand.CommandType = CommandType.Text;
				SqlParameterCollection parameters = updateCommand.Parameters;
				parameters.Add("@CustomFieldGroupID", SqlDbType.Int);
				parameters.Add("@CustomFieldGroupName", SqlDbType.NVarChar);
				parameters.Add("@DateTimeStamp", SqlDbType.DateTime);
				parameters["@CustomFieldGroupID"].SourceColumn = "CustomFieldGroupID";
				parameters["@CustomFieldGroupName"].SourceColumn = "CustomFieldGroupName";
				parameters["@DateTimeStamp"].SourceColumn = "DateTimeStamp";
			}
			return updateCommand;
		}

		public bool InsertCustomFieldGroup(CustomFieldGroupData customFieldGroupData, SqlTransaction sqlTransaction)
		{
			bool result = true;
			bool flag = true;
			SqlCommand insertCommand = GetInsertCommand();
			if (sqlTransaction == null)
			{
				sqlTransaction = base.DBConfig.StartNewTransaction();
				flag = false;
			}
			try
			{
				insertCommand.Transaction = sqlTransaction;
				result = Insert(customFieldGroupData, "[Custom Field Groups]", insertCommand);
				object insertedRowIdentity = GetInsertedRowIdentity("[Custom Field Groups]", insertCommand);
				customFieldGroupData.CustomFieldGroupsTable.Rows[0]["CustomFieldGroupID"] = insertedRowIdentity;
				UpdateTableRowByID("[Custom Field Groups]", "CustomFieldGroupID", "DateTimeStamp", insertedRowIdentity, DateTime.Now, sqlTransaction);
				string entiyID = customFieldGroupData.CustomFieldGroupsTable.Rows[0]["CustomFieldGroupName"].ToString();
				AddActivityLog("Custom Field Group", entiyID, ActivityTypes.Add, sqlTransaction);
				UpdateTableRowInsertUpdateInfo("[Custom Field Groups]", "CustomFieldGroupID", insertedRowIdentity, sqlTransaction, isInsert: true);
				return result;
			}
			catch
			{
				result = false;
				throw;
			}
			finally
			{
				if (!flag)
				{
					base.DBConfig.EndTransaction(result);
				}
			}
		}

		public bool InsertCustomFieldGroup(CustomFieldGroupData customFieldGroupData)
		{
			return InsertCustomFieldGroup(customFieldGroupData, null);
		}

		public bool UpdateCustomFieldGroup(CustomFieldGroupData customFieldGroupData)
		{
			bool result = true;
			SqlCommand updateCommand = GetUpdateCommand();
			SqlTransaction sqlTransaction = null;
			try
			{
				sqlTransaction = (updateCommand.Transaction = base.DBConfig.StartNewTransaction());
				result = Update(customFieldGroupData, "[Custom Field Groups]", updateCommand);
				object obj = customFieldGroupData.CustomFieldGroupsTable.Rows[0]["CustomFieldGroupID"];
				UpdateTableRowByID("[Custom Field Groups]", "CustomFieldGroupID", "DateTimeStamp", obj, DateTime.Now, sqlTransaction);
				string entiyID = customFieldGroupData.CustomFieldGroupsTable.Rows[0]["CustomFieldGroupName"].ToString();
				AddActivityLog("Custom Field Group", entiyID, ActivityTypes.Update, sqlTransaction);
				UpdateTableRowInsertUpdateInfo("[Custom Field Groups]", "CustomFieldGroupID", obj, sqlTransaction, isInsert: false);
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

		public DataSet GetGroups()
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddColumn("[Custom Field Groups]", "CustomFieldGroupID");
			sqlBuilder.AddColumn("[Custom Field Groups]", "CustomFieldGroupName");
			sqlBuilder.IsComparing = false;
			DataSet dataSet = new DataSet();
			FillDataSet(dataSet, "[Custom Field Groups]", sqlBuilder);
			return dataSet;
		}

		public CustomFieldGroupData GetGroupByID(int groupID)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			CommandHelper commandHelper = null;
			commandHelper = new CommandHelper();
			commandHelper.FieldName = "CustomFieldGroupID";
			commandHelper.SqlFieldType = SqlDbType.Int;
			commandHelper.FieldValue = groupID;
			commandHelper.TableName = "[Custom Field Groups]";
			sqlBuilder.AddCommandHelper(commandHelper);
			sqlBuilder.IsComparing = true;
			CustomFieldGroupData customFieldGroupData = new CustomFieldGroupData();
			sqlBuilder.UseDistinct = false;
			FillDataSet(customFieldGroupData, "[Custom Field Groups]", sqlBuilder);
			return customFieldGroupData;
		}

		public CustomFieldGroupData GetGroupByID(int[] groupsID)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			if (groupsID != null && groupsID.Length != 0)
			{
				CommandHelper commandHelper = null;
				commandHelper = new CommandHelper();
				commandHelper.FieldName = "CustomFieldGroupID";
				commandHelper.SqlFieldType = SqlDbType.Int;
				commandHelper.FieldValue = groupsID;
				commandHelper.TableName = "[Custom Field Groups]";
				sqlBuilder.AddCommandHelper(commandHelper);
				sqlBuilder.IsComparing = true;
			}
			else
			{
				sqlBuilder.AddTable("[Custom Field Groups]");
			}
			CustomFieldGroupData customFieldGroupData = new CustomFieldGroupData();
			sqlBuilder.UseDistinct = false;
			FillDataSet(customFieldGroupData, "[Custom Field Groups]", sqlBuilder);
			return customFieldGroupData;
		}
	}
}
