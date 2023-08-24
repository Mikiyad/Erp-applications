using Micromind.Common;
using Micromind.Common.Data;
using Micromind.Common.Libraries;
using System;
using System.Data;
using System.Data.SqlClient;

namespace Micromind.Data
{
	public sealed class Opportunity : StoreObject
	{
		private const string OPPORTUNITYID_PARM = "@OpportunityID";

		private const string OPPORTUNITYNAME_PARM = "@OpportunityName";

		public const string CLOSINGDATE_PARM = "@ClosingDate";

		public const string DUEDATE_PARM = "@DueDate";

		public const string PROBABILITY_PARM = "@Probability";

		public const string AMOUNT_PARM = "@Amount";

		private const string STATUS_PARM = "@Status";

		private const string OWNERID_PARM = "@OwnerID";

		private const string RELATEDID_PARM = "@RelatedID";

		private const string RELATEDTYPE_PARM = "@RelatedType";

		public const string NOTE_PARM = "@Note";

		public const string OPPORTUNITY_TABLE = "Opportunity";

		private const string CREATEDBY_PARM = "@CreatedBy";

		private const string DATECREATED_PARM = "@DateCreated";

		private const string UPDATEDBY_PARM = "@UpdatedBy";

		private const string DATEUPDATED_PARM = "@DateUpdated";

		public Opportunity(Config config)
			: base(config)
		{
		}

		private string GetInsertUpdateText(bool isUpdate)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddInsertUpdateParameters("Opportunity", new FieldValue("OpportunityID", "@OpportunityID", isUpdateConditionField: true), new FieldValue("OpportunityName", "@OpportunityName"), new FieldValue("Status", "@Status"), new FieldValue("ClosingDate", "@ClosingDate"), new FieldValue("DueDate", "@DueDate"), new FieldValue("Probability", "@Probability"), new FieldValue("OwnerID", "@OwnerID"), new FieldValue("Amount", "@Amount"), new FieldValue("RelatedID", "@RelatedID"), new FieldValue("RelatedType", "@RelatedType"), new FieldValue("Note", "@Note"));
			if (isUpdate)
			{
				sqlBuilder.AddInsertUpdateParameters("Opportunity", new FieldValue("DateUpdated", "@DateUpdated", isUpdateConditionField: true, checkForNullValue: true));
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
			parameters.Add("@OpportunityID", SqlDbType.NVarChar);
			parameters.Add("@OpportunityName", SqlDbType.NVarChar);
			parameters.Add("@Status", SqlDbType.TinyInt);
			parameters.Add("@ClosingDate", SqlDbType.DateTime);
			parameters.Add("@DueDate", SqlDbType.DateTime);
			parameters.Add("@Probability", SqlDbType.TinyInt);
			parameters.Add("@Amount", SqlDbType.Decimal);
			parameters.Add("@RelatedID", SqlDbType.NVarChar);
			parameters.Add("@OwnerID", SqlDbType.NVarChar);
			parameters.Add("@RelatedType", SqlDbType.TinyInt);
			parameters.Add("@Note", SqlDbType.NVarChar);
			parameters["@OpportunityID"].SourceColumn = "OpportunityID";
			parameters["@OpportunityName"].SourceColumn = "OpportunityName";
			parameters["@Status"].SourceColumn = "Status";
			parameters["@ClosingDate"].SourceColumn = "ClosingDate";
			parameters["@DueDate"].SourceColumn = "DueDate";
			parameters["@Probability"].SourceColumn = "Probability";
			parameters["@Amount"].SourceColumn = "Amount";
			parameters["@RelatedID"].SourceColumn = "RelatedID";
			parameters["@OwnerID"].SourceColumn = "OwnerID";
			parameters["@RelatedType"].SourceColumn = "RelatedType";
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

		public bool InsertOpportunity(OpportunityData opportunityData)
		{
			bool result = true;
			SqlCommand insertUpdateCommand = GetInsertUpdateCommand(isUpdate: false);
			SqlTransaction sqlTransaction = null;
			try
			{
				sqlTransaction = (insertUpdateCommand.Transaction = base.DBConfig.StartNewTransaction());
				result = Insert(opportunityData, "Opportunity", insertUpdateCommand);
				string text = opportunityData.OpportunityTable.Rows[0]["OpportunityID"].ToString();
				AddActivityLog("Opportunity", text, ActivityTypes.Add, sqlTransaction);
				UpdateTableRowInsertUpdateInfo("Opportunity", "OpportunityID", text, sqlTransaction, isInsert: true);
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

		public bool UpdateOpportunity(OpportunityData accountOpportunityData)
		{
			bool flag = true;
			SqlCommand insertUpdateCommand = GetInsertUpdateCommand(isUpdate: true);
			SqlTransaction sqlTransaction = null;
			try
			{
				sqlTransaction = (insertUpdateCommand.Transaction = base.DBConfig.StartNewTransaction());
				flag = Update(accountOpportunityData, "Opportunity", insertUpdateCommand);
				if (!flag)
				{
					return flag;
				}
				object obj = accountOpportunityData.OpportunityTable.Rows[0]["OpportunityID"];
				UpdateTableRowByID("Opportunity", "OpportunityID", "DateUpdated", obj, DateTime.Now, sqlTransaction);
				string entiyID = accountOpportunityData.OpportunityTable.Rows[0]["OpportunityName"].ToString();
				AddActivityLog("Opportunity", entiyID, ActivityTypes.Update, sqlTransaction);
				UpdateTableRowInsertUpdateInfo("Opportunity", "OpportunityID", obj, sqlTransaction, isInsert: false);
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

		public OpportunityData GetOpportunity()
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddTable("Opportunity");
			OpportunityData opportunityData = new OpportunityData();
			sqlBuilder.UseDistinct = false;
			FillDataSet(opportunityData, "Opportunity", sqlBuilder);
			return opportunityData;
		}

		public bool DeleteOpportunity(string opportunityID)
		{
			bool flag = true;
			try
			{
				string commandText = "DELETE FROM Opportunity WHERE OpportunityID = '" + opportunityID + "'";
				flag = Delete(commandText, null);
				if (!flag)
				{
					return flag;
				}
				AddActivityLog("Opportunity", opportunityID, ActivityTypes.Delete, null);
				return flag;
			}
			catch
			{
				throw;
			}
		}

		public OpportunityData GetOpportunityByID(string id)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			CommandHelper commandHelper = null;
			commandHelper = new CommandHelper();
			commandHelper.FieldName = "OpportunityID";
			commandHelper.SqlFieldType = SqlDbType.NVarChar;
			commandHelper.FieldValue = id;
			commandHelper.TableName = "Opportunity";
			sqlBuilder.AddCommandHelper(commandHelper);
			sqlBuilder.IsComparing = true;
			OpportunityData opportunityData = new OpportunityData();
			sqlBuilder.UseDistinct = false;
			FillDataSet(opportunityData, "Opportunity", sqlBuilder);
			return opportunityData;
		}

		public DataSet GetOpportunityByFields(params string[] columns)
		{
			return GetOpportunityByFields(null, isInactive: true, columns);
		}

		public DataSet GetOpportunityByFields(string[] opportunityID, params string[] columns)
		{
			return GetOpportunityByFields(opportunityID, isInactive: true, columns);
		}

		public DataSet GetOpportunityByFields(string[] ids, bool isInactive, params string[] columns)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddTable("Opportunity");
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
				commandHelper.FieldName = "OpportunityID";
				commandHelper.FieldValue = ids;
				commandHelper.SqlFieldType = SqlDbType.NVarChar;
				commandHelper.TableName = "Opportunity";
				sqlBuilder.AddCommandHelper(commandHelper);
			}
			DataSet dataSet = new DataSet();
			dataSet.EnforceConstraints = false;
			sqlBuilder.UseDistinct = false;
			FillDataSet(dataSet, "Opportunity", sqlBuilder);
			return dataSet;
		}

		public DataSet GetOpportunityList()
		{
			DataSet dataSet = new DataSet();
			string textCommand = "SELECT OpportunityID [Opportunity Code], \r\n                               OpportunityName [Opportunity Name], \r\n                        (CASE RelatedType\r\n\t\t\t\t\t\tWHEN 1 THEN 'Lead'\r\n\t\t\t\t\t\tWHEN 2 THEN 'Customer'\r\n\t\t\t\t\t\tELSE '' END) AS [Account Type], (CASE RelatedType\r\n\t\t\t\t\t\tWHEN 1 THEN Lead.LeadName\r\n\t\t\t\t\t\tWHEN 2 THEN CUS.CustomerName\r\n\t\t\t\t\t\tELSE '' END) AS [Account Name],CASE Status WHEN 0 THEN 'None' \r\n                                WHEN 1 THEN 'Prospecting' WHEN 2 THEN 'Qualification' \r\n                                WHEN 3 THEN 'Need Analysis' WHEN 4 THEN 'Value Proposition' \r\n                                WHEN 5 THEN 'Id. Decision Makers' WHEN 6 THEN 'Perception Analysis'\r\n                                WHEN 7 THEN 'Proposal/Price Quote' WHEN 8 THEN 'Negotiation'\r\n                                WHEN 9 THEN 'Closed Won' WHEN 10 THEN 'Closed Lost'   \r\n                                END AS Status, ClosingDate [Closing Date],  \r\n                                Probability, Amount, Opportunity.Note\r\n                           FROM Opportunity LEFT OUTER JOIN Lead ON Lead.LeadID = Opportunity.RelatedID\r\n                            LEFT OUTER JOIN Customer CUS ON CUS.CustomerID = Opportunity.RelatedID";
			FillDataSet(dataSet, "Opportunity", textCommand);
			return dataSet;
		}

		public DataSet GetUpcomingOpportunitiesReport(string fromLead, string toLead, DateTime dtFrom, DateTime dtTo)
		{
			DataSet dataSet = new DataSet();
			string text = CommonLib.ToSqlDateTimeString(dtFrom);
			string text2 = CommonLib.ToSqlDateTimeString(dtTo);
			string text3 = "SELECT OpportunityID [Opportunity Code], \r\n                               OpportunityName [Opportunity Name], CASE Status WHEN 0 THEN 'None' \r\n                                WHEN 1 THEN 'Prospecting' WHEN 2 THEN 'Qualification' \r\n                                WHEN 3 THEN 'Need Analysis' WHEN 4 THEN 'Value Proposition' \r\n                                WHEN 5 THEN 'Id. Decision Makers' WHEN 6 THEN 'Perception Analysis'\r\n                                WHEN 7 THEN 'Proposal/Price Quote' WHEN 8 THEN 'Negotiation'\r\n                                WHEN 9 THEN 'Closed Won' WHEN 10 THEN 'Closed Lost'   \r\n                                END AS Status, ClosingDate [Closing Date],  \r\n                                Probability, Amount, LeadID [Lead Code], Note\r\n                           FROM Opportunity";
			text3 = text3 + " WHERE DateCreated BETWEEN '" + text + "' AND '" + text2 + "' ";
			if (fromLead != "")
			{
				text3 = text3 + " AND LeadID >='" + fromLead + "'";
			}
			if (toLead != "")
			{
				text3 = text3 + " AND LeadID <='" + toLead + "'";
			}
			text3 += " ORDER BY OpportunityID, OpportunityName";
			FillDataSet(dataSet, "Opportunity", text3);
			return dataSet;
		}

		public DataSet GetOpportunityListByLeadID(CRMRelatedTypes leadType, string leadID, bool includeClosed)
		{
			DataSet dataSet = new DataSet();
			string text = "SELECT OpportunityID [Opportunity Code], \r\n                               OpportunityName [Opportunity Name], CASE Status WHEN 0 THEN 'None' \r\n                                WHEN 1 THEN 'Prospecting' WHEN 2 THEN 'Qualification' \r\n                                WHEN 3 THEN 'Need Analysis' WHEN 4 THEN 'Value Proposition' \r\n                                WHEN 5 THEN 'Id. Decision Makers' WHEN 6 THEN 'Perception Analysis'\r\n                                WHEN 7 THEN 'Proposal/Price Quote' WHEN 8 THEN 'Negotiation'\r\n                                WHEN 9 THEN 'Closed Won' WHEN 10 THEN 'Closed Lost'   \r\n                                END AS Status,DueDate [Due Date], ClosingDate [Closing Date],  \r\n                                Probability, Amount, RelatedID [Lead Code], Note\r\n                           FROM Opportunity\r\n                           WHERE RelatedType = " + (int)leadType + " AND RelatedID = '" + leadID + "' ";
			if (!includeClosed)
			{
				text += " AND Status NOT IN (9,10)";
			}
			FillDataSet(dataSet, "Opportunity", text);
			return dataSet;
		}

		public DataSet GetOpportunityComboList()
		{
			DataSet dataSet = new DataSet();
			string textCommand = "SELECT OpportunityID [Code], OpportunityName [Name]\r\n                           FROM Opportunity ORDER BY OpportunityID, OpportunityName";
			FillDataSet(dataSet, "Opportunity", textCommand);
			return dataSet;
		}
	}
}
