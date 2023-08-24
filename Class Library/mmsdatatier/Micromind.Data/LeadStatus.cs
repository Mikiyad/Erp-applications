using Micromind.Common;
using Micromind.Common.Data;
using Micromind.Common.Libraries;
using System;
using System.Data;
using System.Data.SqlClient;

namespace Micromind.Data
{
	public sealed class LeadStatus : StoreObject
	{
		private const string LEADSTATUSID_PARM = "@LeadStatusID";

		private const string LEADSTATUSDESCRIPTION_PARM = "@LeadStatusDescription";

		public const string ISINACTIVE_PARM = "@IsInactive";

		public const string NOTE_PARM = "@Note";

		public const string LEADSTATUS_TABLE = "Release_Type";

		private const string CREATEDBY_PARM = "@CreatedBy";

		private const string DATECREATED_PARM = "@DateCreated";

		private const string UPDATEDBY_PARM = "@UpdatedBy";

		private const string DATEUPDATED_PARM = "@DateUpdated";

		public LeadStatus(Config config)
			: base(config)
		{
		}

		private string GetInsertUpdateText(bool isUpdate)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddInsertUpdateParameters("Lead_Status", new FieldValue("LeadStatusID", "@LeadStatusID", isUpdateConditionField: true), new FieldValue("Name", "@LeadStatusDescription"), new FieldValue("IsInactive", "@IsInactive"), new FieldValue("Note", "@Note"));
			if (isUpdate)
			{
				sqlBuilder.AddInsertUpdateParameters("Lead_Status", new FieldValue("DateUpdated", "@DateUpdated", isUpdateConditionField: true, checkForNullValue: true));
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
			parameters.Add("@LeadStatusID", SqlDbType.NVarChar);
			parameters.Add("@LeadStatusDescription", SqlDbType.NVarChar);
			parameters.Add("@IsInactive", SqlDbType.Bit);
			parameters.Add("@Note", SqlDbType.NVarChar);
			parameters["@LeadStatusID"].SourceColumn = "LeadStatusID";
			parameters["@LeadStatusDescription"].SourceColumn = "Name";
			parameters["@IsInactive"].SourceColumn = "IsInactive";
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

		public bool InsertLeadStatus(LeadStatusData accountLeadStatusData)
		{
			bool result = true;
			SqlCommand insertUpdateCommand = GetInsertUpdateCommand(isUpdate: false);
			SqlTransaction sqlTransaction = null;
			try
			{
				sqlTransaction = (insertUpdateCommand.Transaction = base.DBConfig.StartNewTransaction());
				result = Insert(accountLeadStatusData, "Lead_Status", insertUpdateCommand);
				string text = accountLeadStatusData.LeadStatusTable.Rows[0]["LeadStatusID"].ToString();
				AddActivityLog("Lead Status", text, ActivityTypes.Add, sqlTransaction);
				UpdateTableRowInsertUpdateInfo("Lead_Status", "LeadStatusID", text, sqlTransaction, isInsert: true);
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

		public bool UpdateLeadStatus(LeadStatusData accountLeadStatusData)
		{
			bool flag = true;
			SqlCommand insertUpdateCommand = GetInsertUpdateCommand(isUpdate: true);
			SqlTransaction sqlTransaction = null;
			try
			{
				sqlTransaction = (insertUpdateCommand.Transaction = base.DBConfig.StartNewTransaction());
				flag = Update(accountLeadStatusData, "Lead_Status", insertUpdateCommand);
				if (!flag)
				{
					return flag;
				}
				object obj = accountLeadStatusData.LeadStatusTable.Rows[0]["LeadStatusID"];
				UpdateTableRowByID("Lead_Status", "LeadStatusID", "DateUpdated", obj, DateTime.Now, sqlTransaction);
				string entiyID = accountLeadStatusData.LeadStatusTable.Rows[0]["Name"].ToString();
				AddActivityLog("Lead Status", entiyID, ActivityTypes.Update, sqlTransaction);
				UpdateTableRowInsertUpdateInfo("Lead_Status", "LeadStatusID", obj, sqlTransaction, isInsert: false);
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

		public LeadStatusData GetLeadStatus()
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddTable("Lead_Status");
			LeadStatusData leadStatusData = new LeadStatusData();
			sqlBuilder.UseDistinct = false;
			FillDataSet(leadStatusData, "Lead_Status", sqlBuilder);
			return leadStatusData;
		}

		public bool DeleteLeadStatus(string LeadStatusID)
		{
			bool flag = true;
			try
			{
				string commandText = "DELETE FROM Lead_Status WHERE LeadStatusID = '" + LeadStatusID + "'";
				flag = Delete(commandText, null);
				if (!flag)
				{
					return flag;
				}
				AddActivityLog("Lead Status", LeadStatusID, ActivityTypes.Delete, null);
				return flag;
			}
			catch
			{
				throw;
			}
		}

		public LeadStatusData GetLeadStatusByID(string id)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			CommandHelper commandHelper = null;
			commandHelper = new CommandHelper();
			commandHelper.FieldName = "LeadStatusID";
			commandHelper.SqlFieldType = SqlDbType.NVarChar;
			commandHelper.FieldValue = id;
			commandHelper.TableName = "Lead_Status";
			sqlBuilder.AddCommandHelper(commandHelper);
			sqlBuilder.IsComparing = true;
			LeadStatusData leadStatusData = new LeadStatusData();
			sqlBuilder.UseDistinct = false;
			FillDataSet(leadStatusData, "Lead_Status", sqlBuilder);
			return leadStatusData;
		}

		public DataSet GetLeadStatusByFields(params string[] columns)
		{
			return GetLeadStatusByFields(null, isInactive: true, columns);
		}

		public DataSet GetLeadStatusByFields(string[] LeadStatusID, params string[] columns)
		{
			return GetLeadStatusByFields(LeadStatusID, isInactive: true, columns);
		}

		public DataSet GetLeadStatusByFields(string[] ids, bool isInactive, params string[] columns)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddTable("Lead_Status");
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
				commandHelper.FieldName = "LeadStatusID";
				commandHelper.FieldValue = ids;
				commandHelper.SqlFieldType = SqlDbType.NVarChar;
				commandHelper.TableName = "Lead_Status";
				sqlBuilder.AddCommandHelper(commandHelper);
			}
			DataSet dataSet = new DataSet();
			dataSet.EnforceConstraints = false;
			sqlBuilder.UseDistinct = false;
			FillDataSet(dataSet, "Lead_Status", sqlBuilder);
			return dataSet;
		}

		public DataSet GetLeadStatusList()
		{
			DataSet dataSet = new DataSet();
			string textCommand = "SELECT LeadStatusID [Code],Name [Name],Note,IsInactive [Inactive]\r\n                           FROM Lead_Status ";
			FillDataSet(dataSet, "Lead_Status", textCommand);
			return dataSet;
		}

		public DataSet GetLeadStatusComboList()
		{
			DataSet dataSet = new DataSet();
			string textCommand = "SELECT LeadStatusID [Code],Name [Name]\r\n                           FROM Lead_Status ORDER BY LeadStatusID,Name";
			FillDataSet(dataSet, "Lead_Status", textCommand);
			return dataSet;
		}
	}
}
