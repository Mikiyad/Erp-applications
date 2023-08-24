using Micromind.Common;
using Micromind.Common.Data;
using Micromind.Common.Libraries;
using System;
using System.Data;
using System.Data.SqlClient;

namespace Micromind.Data
{
	public sealed class Campaign : StoreObject
	{
		public const string CAMPAIGN_TABLE = "Campaign";

		public const string CAMPAIGNID_PARM = "@CampaignID";

		public const string CAMPAIGNNAME_PARM = "@CampaignName";

		public const string CAMPAIGNTYPE_PARM = "@Type";

		public const string CAMPAIGNSTATUS_PARM = "@Status";

		public const string STARTDATE_PARM = "@StartDate";

		public const string ENDDATE_PARM = "@EndDate";

		public const string NUMSENT_PARM = "@NumberSent";

		public const string EXPRESPONSE_PARM = "@ExpectedResponse";

		public const string BUDGCOST_PARM = "@BudgetedCost";

		public const string ACTCOST_PARM = "@ActualCost";

		public const string EXPREVENUE_PARM = "@ExpectedRevenue";

		public const string INACTIVE_PARM = "@IsInactive";

		public const string NOTE_PARM = "@Note";

		private const string CREATEDBY_PARM = "@CreatedBy";

		private const string DATECREATED_PARM = "@DateCreated";

		private const string UPDATEDBY_PARM = "@UpdatedBy";

		private const string DATEUPDATED_PARM = "@DateUpdated";

		public Campaign(Config config)
			: base(config)
		{
		}

		private string GetInsertUpdateText(bool isUpdate)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddInsertUpdateParameters("Campaign", new FieldValue("CampaignID", "@CampaignID", isUpdateConditionField: true), new FieldValue("CampaignName", "@CampaignName"), new FieldValue("Type", "@Type"), new FieldValue("Status", "@Status"), new FieldValue("StartDate", "@StartDate"), new FieldValue("EndDate", "@EndDate"), new FieldValue("NumberSent", "@NumberSent"), new FieldValue("ExpectedResponse", "@ExpectedResponse"), new FieldValue("BudgetedCost", "@BudgetedCost"), new FieldValue("ActualCost", "@ActualCost"), new FieldValue("ExpectedRevenue", "@ExpectedRevenue"), new FieldValue("IsInactive", "@IsInactive"), new FieldValue("Note", "@Note"));
			if (isUpdate)
			{
				sqlBuilder.AddInsertUpdateParameters("Campaign", new FieldValue("DateUpdated", "@DateUpdated", isUpdateConditionField: true, checkForNullValue: true));
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
			parameters.Add("@CampaignID", SqlDbType.NVarChar);
			parameters.Add("@CampaignName", SqlDbType.NVarChar);
			parameters.Add("@Type", SqlDbType.TinyInt);
			parameters.Add("@Status", SqlDbType.TinyInt);
			parameters.Add("@StartDate", SqlDbType.DateTime);
			parameters.Add("@EndDate", SqlDbType.DateTime);
			parameters.Add("@NumberSent", SqlDbType.Int);
			parameters.Add("@ExpectedResponse", SqlDbType.TinyInt);
			parameters.Add("@BudgetedCost", SqlDbType.Decimal);
			parameters.Add("@ActualCost", SqlDbType.Decimal);
			parameters.Add("@ExpectedRevenue", SqlDbType.Money);
			parameters.Add("@IsInactive", SqlDbType.Bit);
			parameters.Add("@Note", SqlDbType.NVarChar);
			parameters["@CampaignID"].SourceColumn = "CampaignID";
			parameters["@CampaignName"].SourceColumn = "CampaignName";
			parameters["@Type"].SourceColumn = "Type";
			parameters["@Status"].SourceColumn = "Status";
			parameters["@StartDate"].SourceColumn = "StartDate";
			parameters["@EndDate"].SourceColumn = "EndDate";
			parameters["@NumberSent"].SourceColumn = "NumberSent";
			parameters["@ExpectedResponse"].SourceColumn = "ExpectedResponse";
			parameters["@BudgetedCost"].SourceColumn = "BudgetedCost";
			parameters["@ActualCost"].SourceColumn = "ActualCost";
			parameters["@ExpectedRevenue"].SourceColumn = "ExpectedResponse";
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

		public bool InsertCampaign(CampaignData campaignData)
		{
			bool result = true;
			SqlCommand insertUpdateCommand = GetInsertUpdateCommand(isUpdate: false);
			SqlTransaction sqlTransaction = null;
			try
			{
				sqlTransaction = (insertUpdateCommand.Transaction = base.DBConfig.StartNewTransaction());
				result = Insert(campaignData, "Campaign", insertUpdateCommand);
				string text = campaignData.CampaignTable.Rows[0]["CampaignID"].ToString();
				AddActivityLog("Campaign", text, ActivityTypes.Add, sqlTransaction);
				UpdateTableRowInsertUpdateInfo("Campaign", "CampaignID", text, sqlTransaction, isInsert: true);
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

		public bool UpdateCampaign(CampaignData accountCampaignData)
		{
			bool flag = true;
			SqlCommand insertUpdateCommand = GetInsertUpdateCommand(isUpdate: true);
			SqlTransaction sqlTransaction = null;
			try
			{
				sqlTransaction = (insertUpdateCommand.Transaction = base.DBConfig.StartNewTransaction());
				flag = Update(accountCampaignData, "Campaign", insertUpdateCommand);
				if (!flag)
				{
					return flag;
				}
				object obj = accountCampaignData.CampaignTable.Rows[0]["CampaignID"];
				UpdateTableRowByID("Campaign", "CampaignID", "DateUpdated", obj, DateTime.Now, sqlTransaction);
				string entiyID = accountCampaignData.CampaignTable.Rows[0]["CampaignName"].ToString();
				AddActivityLog("Campaign", entiyID, ActivityTypes.Update, sqlTransaction);
				UpdateTableRowInsertUpdateInfo("Campaign", "CampaignID", obj, sqlTransaction, isInsert: false);
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

		public CampaignData GetCampaign()
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddTable("Campaign");
			CampaignData campaignData = new CampaignData();
			sqlBuilder.UseDistinct = false;
			FillDataSet(campaignData, "Campaign", sqlBuilder);
			return campaignData;
		}

		public bool DeleteCampaign(string campaignID)
		{
			bool flag = true;
			try
			{
				string commandText = "DELETE FROM Campaign WHERE CampaignID = '" + campaignID + "'";
				flag = Delete(commandText, null);
				if (!flag)
				{
					return flag;
				}
				AddActivityLog("Campaign", campaignID, ActivityTypes.Delete, null);
				return flag;
			}
			catch
			{
				throw;
			}
		}

		public CampaignData GetCampaignByID(string id)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			CommandHelper commandHelper = null;
			commandHelper = new CommandHelper();
			commandHelper.FieldName = "CampaignID";
			commandHelper.SqlFieldType = SqlDbType.NVarChar;
			commandHelper.FieldValue = id;
			commandHelper.TableName = "Campaign";
			sqlBuilder.AddCommandHelper(commandHelper);
			sqlBuilder.IsComparing = true;
			CampaignData campaignData = new CampaignData();
			sqlBuilder.UseDistinct = false;
			FillDataSet(campaignData, "Campaign", sqlBuilder);
			return campaignData;
		}

		public DataSet GetCampaignByFields(params string[] columns)
		{
			return GetCampaignByFields(null, isInactive: true, columns);
		}

		public DataSet GetCampaignByFields(string[] campaignID, params string[] columns)
		{
			return GetCampaignByFields(campaignID, isInactive: true, columns);
		}

		public DataSet GetCampaignByFields(string[] ids, bool isInactive, params string[] columns)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddTable("Campaign");
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
				commandHelper.FieldName = "CampaignID";
				commandHelper.FieldValue = ids;
				commandHelper.SqlFieldType = SqlDbType.NVarChar;
				commandHelper.TableName = "Campaign";
				sqlBuilder.AddCommandHelper(commandHelper);
			}
			DataSet dataSet = new DataSet();
			dataSet.EnforceConstraints = false;
			sqlBuilder.UseDistinct = false;
			FillDataSet(dataSet, "Campaign", sqlBuilder);
			return dataSet;
		}

		public DataSet GetCampaignList()
		{
			DataSet dataSet = new DataSet();
			string textCommand = "SELECT CampaignID [Campaign Code], \r\n                               CampaignName [Campaign Name], StartDate [Start Date], EndDate [End Date],\r\n                                NumberSent [Sent], ExpectedResponse [Response], BudgetedCost [Budget], ActualCost [Actual], \r\n                                ExpectedRevenue [Revenue], Note\r\n                           FROM Campaign";
			FillDataSet(dataSet, "Campaign", textCommand);
			return dataSet;
		}

		public DataSet GetCampaignComboList()
		{
			DataSet dataSet = new DataSet();
			string textCommand = "SELECT CampaignID [Code], CampaignName [Name]\r\n                           FROM Campaign ORDER BY CampaignID, CampaignName";
			FillDataSet(dataSet, "Campaign", textCommand);
			return dataSet;
		}
	}
}
