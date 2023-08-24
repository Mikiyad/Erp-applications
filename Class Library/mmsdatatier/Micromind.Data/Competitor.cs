using Micromind.Common;
using Micromind.Common.Data;
using Micromind.Common.Libraries;
using System;
using System.Data;
using System.Data.SqlClient;

namespace Micromind.Data
{
	public sealed class Competitor : StoreObject
	{
		private const string COMPETITORID_PARM = "@CompetitorID";

		private const string COMPETITORNAME_PARM = "@CompetitorName";

		public const string NOTE_PARM = "@Note";

		public const string COMPETITOR_TABLE = "Competitor";

		private const string CREATEDBY_PARM = "@CreatedBy";

		private const string DATECREATED_PARM = "@DateCreated";

		private const string UPDATEDBY_PARM = "@UpdatedBy";

		private const string DATEUPDATED_PARM = "@DateUpdated";

		public Competitor(Config config)
			: base(config)
		{
		}

		private string GetInsertUpdateText(bool isUpdate)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddInsertUpdateParameters("Competitor", new FieldValue("CompetitorID", "@CompetitorID", isUpdateConditionField: true), new FieldValue("CompetitorName", "@CompetitorName"), new FieldValue("Note", "@Note"));
			if (isUpdate)
			{
				sqlBuilder.AddInsertUpdateParameters("Competitor", new FieldValue("DateUpdated", "@DateUpdated", isUpdateConditionField: true, checkForNullValue: true));
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
			parameters.Add("@CompetitorID", SqlDbType.NVarChar);
			parameters.Add("@CompetitorName", SqlDbType.NVarChar);
			parameters.Add("@Note", SqlDbType.NVarChar);
			parameters["@CompetitorID"].SourceColumn = "CompetitorID";
			parameters["@CompetitorName"].SourceColumn = "CompetitorName";
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

		public bool InsertCompetitor(CompetitorData competitorData)
		{
			bool result = true;
			SqlCommand insertUpdateCommand = GetInsertUpdateCommand(isUpdate: false);
			SqlTransaction sqlTransaction = null;
			try
			{
				sqlTransaction = (insertUpdateCommand.Transaction = base.DBConfig.StartNewTransaction());
				result = Insert(competitorData, "Competitor", insertUpdateCommand);
				string text = competitorData.CompetitorTable.Rows[0]["CompetitorID"].ToString();
				AddActivityLog("Competitor", text, ActivityTypes.Add, sqlTransaction);
				UpdateTableRowInsertUpdateInfo("Competitor", "CompetitorID", text, sqlTransaction, isInsert: true);
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

		public bool UpdateCompetitor(CompetitorData accountCompetitorData)
		{
			bool flag = true;
			SqlCommand insertUpdateCommand = GetInsertUpdateCommand(isUpdate: true);
			SqlTransaction sqlTransaction = null;
			try
			{
				sqlTransaction = (insertUpdateCommand.Transaction = base.DBConfig.StartNewTransaction());
				flag = Update(accountCompetitorData, "Competitor", insertUpdateCommand);
				if (!flag)
				{
					return flag;
				}
				object obj = accountCompetitorData.CompetitorTable.Rows[0]["CompetitorID"];
				UpdateTableRowByID("Competitor", "CompetitorID", "DateUpdated", obj, DateTime.Now, sqlTransaction);
				string entiyID = accountCompetitorData.CompetitorTable.Rows[0]["CompetitorName"].ToString();
				AddActivityLog("Competitor", entiyID, ActivityTypes.Update, sqlTransaction);
				UpdateTableRowInsertUpdateInfo("Competitor", "CompetitorID", obj, sqlTransaction, isInsert: false);
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

		public CompetitorData GetCompetitor()
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddTable("Competitor");
			CompetitorData competitorData = new CompetitorData();
			sqlBuilder.UseDistinct = false;
			FillDataSet(competitorData, "Competitor", sqlBuilder);
			return competitorData;
		}

		public bool DeleteCompetitor(string competitorID)
		{
			bool flag = true;
			try
			{
				string commandText = "DELETE FROM Competitor WHERE CompetitorID = '" + competitorID + "'";
				flag = Delete(commandText, null);
				if (!flag)
				{
					return flag;
				}
				AddActivityLog("Competitor", competitorID, ActivityTypes.Delete, null);
				return flag;
			}
			catch
			{
				throw;
			}
		}

		public CompetitorData GetCompetitorByID(string id)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			CommandHelper commandHelper = null;
			commandHelper = new CommandHelper();
			commandHelper.FieldName = "CompetitorID";
			commandHelper.SqlFieldType = SqlDbType.NVarChar;
			commandHelper.FieldValue = id;
			commandHelper.TableName = "Competitor";
			sqlBuilder.AddCommandHelper(commandHelper);
			sqlBuilder.IsComparing = true;
			CompetitorData competitorData = new CompetitorData();
			sqlBuilder.UseDistinct = false;
			FillDataSet(competitorData, "Competitor", sqlBuilder);
			return competitorData;
		}

		public DataSet GetCompetitorByFields(params string[] columns)
		{
			return GetCompetitorByFields(null, isInactive: true, columns);
		}

		public DataSet GetCompetitorByFields(string[] competitorID, params string[] columns)
		{
			return GetCompetitorByFields(competitorID, isInactive: true, columns);
		}

		public DataSet GetCompetitorByFields(string[] ids, bool isInactive, params string[] columns)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddTable("Competitor");
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
				commandHelper.FieldName = "CompetitorID";
				commandHelper.FieldValue = ids;
				commandHelper.SqlFieldType = SqlDbType.NVarChar;
				commandHelper.TableName = "Competitor";
				sqlBuilder.AddCommandHelper(commandHelper);
			}
			DataSet dataSet = new DataSet();
			dataSet.EnforceConstraints = false;
			sqlBuilder.UseDistinct = false;
			FillDataSet(dataSet, "Competitor", sqlBuilder);
			return dataSet;
		}

		public DataSet GetCompetitorList()
		{
			DataSet dataSet = new DataSet();
			string textCommand = "SELECT CompetitorID [Competitor Code], \r\n                               CompetitorName [Competitor Name], Note\r\n                           FROM Competitor";
			FillDataSet(dataSet, "Competitor", textCommand);
			return dataSet;
		}

		public DataSet GetCompetitorComboList()
		{
			DataSet dataSet = new DataSet();
			string textCommand = "SELECT CompetitorID [Code], CompetitorName [Name]\r\n                           FROM Competitor ORDER BY CompetitorID, CompetitorName";
			FillDataSet(dataSet, "Competitor", textCommand);
			return dataSet;
		}
	}
}
