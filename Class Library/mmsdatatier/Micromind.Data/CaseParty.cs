using Micromind.Common;
using Micromind.Common.Data;
using Micromind.Common.Libraries;
using System;
using System.Data;
using System.Data.SqlClient;

namespace Micromind.Data
{
	public sealed class CaseParty : StoreObject
	{
		private const string CASEPARTY_TABLE = "Case_Party";

		private const string CASEPARTYID_PARM = "@CasePartyID";

		private const string CASEPARTYNAME_PARM = "@CasePartyName";

		private const string ISINACTIVE_PARM = "@IsInactive";

		private const string NOTE_PARM = "@Note";

		private const string CREATEDBY_PARM = "@CreatedBy";

		private const string DATECREATED_PARM = "@DateCreated";

		private const string UPDATEDBY_PARM = "@UpdatedBy";

		private const string DATEUPDATED_PARM = "@DateUpdated";

		public CaseParty(Config config)
			: base(config)
		{
		}

		private string GetInsertUpdateText(bool isUpdate)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddInsertUpdateParameters("Case_Party", new FieldValue("CasePartyID", "@CasePartyID", isUpdateConditionField: true), new FieldValue("CasePartyName", "@CasePartyName"), new FieldValue("IsInactive", "@IsInactive"), new FieldValue("Note", "@Note"));
			if (isUpdate)
			{
				sqlBuilder.AddInsertUpdateParameters("Case_Party", new FieldValue("DateUpdated", "@DateUpdated", isUpdateConditionField: true, checkForNullValue: true));
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
			parameters.Add("@CasePartyID", SqlDbType.NVarChar);
			parameters.Add("@CasePartyName", SqlDbType.NVarChar);
			parameters.Add("@Note", SqlDbType.NVarChar);
			parameters.Add("@IsInactive", SqlDbType.Bit);
			parameters["@CasePartyID"].SourceColumn = "CasePartyID";
			parameters["@CasePartyName"].SourceColumn = "CasePartyName";
			parameters["@IsInactive"].SourceColumn = "IsInactive";
			parameters["@Note"].SourceColumn = "Note";
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

		public bool InsertCaseParty(CasePartyData accountCasePartyData)
		{
			bool result = true;
			SqlCommand insertUpdateCommand = GetInsertUpdateCommand(isUpdate: false);
			SqlTransaction sqlTransaction = null;
			try
			{
				sqlTransaction = (insertUpdateCommand.Transaction = base.DBConfig.StartNewTransaction());
				result = Insert(accountCasePartyData, "Case_Party", insertUpdateCommand);
				string text = accountCasePartyData.CasePartyTable.Rows[0]["CasePartyID"].ToString();
				AddActivityLog("CaseParty", text, ActivityTypes.Add, sqlTransaction);
				UpdateTableRowInsertUpdateInfo("Case_Party", "CasePartyID", text, sqlTransaction, isInsert: true);
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

		public bool UpdateCaseParty(CasePartyData accountCasePartyData)
		{
			bool flag = true;
			SqlCommand insertUpdateCommand = GetInsertUpdateCommand(isUpdate: true);
			SqlTransaction sqlTransaction = null;
			try
			{
				sqlTransaction = (insertUpdateCommand.Transaction = base.DBConfig.StartNewTransaction());
				flag = Update(accountCasePartyData, "Case_Party", insertUpdateCommand);
				if (!flag)
				{
					return flag;
				}
				object obj = accountCasePartyData.CasePartyTable.Rows[0]["CasePartyID"];
				UpdateTableRowByID("Case_Party", "CasePartyID", "DateUpdated", obj, DateTime.Now, sqlTransaction);
				string entiyID = accountCasePartyData.CasePartyTable.Rows[0]["CasePartyName"].ToString();
				AddActivityLog("CaseParty", entiyID, ActivityTypes.Update, sqlTransaction);
				UpdateTableRowInsertUpdateInfo("Case_Party", "CasePartyID", obj, sqlTransaction, isInsert: false);
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

		public CasePartyData GetCaseParty()
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddTable("Case_Party");
			CasePartyData casePartyData = new CasePartyData();
			sqlBuilder.UseDistinct = false;
			FillDataSet(casePartyData, "Case_Party", sqlBuilder);
			return casePartyData;
		}

		public bool DeleteCaseParty(string CASEPARTYID)
		{
			bool flag = true;
			try
			{
				string commandText = "DELETE FROM Case_Party WHERE CasePartyID = '" + CASEPARTYID + "'";
				flag = Delete(commandText, null);
				if (!flag)
				{
					return flag;
				}
				AddActivityLog("CaseParty", CASEPARTYID, ActivityTypes.Delete, null);
				return flag;
			}
			catch
			{
				throw;
			}
		}

		public CasePartyData GetCasePartyByID(string id)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			CommandHelper commandHelper = null;
			commandHelper = new CommandHelper();
			commandHelper.FieldName = "CasePartyID";
			commandHelper.SqlFieldType = SqlDbType.NVarChar;
			commandHelper.FieldValue = id;
			commandHelper.TableName = "Case_Party";
			sqlBuilder.AddCommandHelper(commandHelper);
			sqlBuilder.IsComparing = true;
			CasePartyData casePartyData = new CasePartyData();
			sqlBuilder.UseDistinct = false;
			FillDataSet(casePartyData, "Case_Party", sqlBuilder);
			return casePartyData;
		}

		public DataSet GetCasePartyByFields(params string[] columns)
		{
			return GetCasePartyByFields(null, isInactive: true, columns);
		}

		public DataSet GetCasePartyByFields(string[] CASEPARTYID, params string[] columns)
		{
			return GetCasePartyByFields(CASEPARTYID, isInactive: true, columns);
		}

		public DataSet GetCasePartyByFields(string[] ids, bool isInactive, params string[] columns)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddTable("Case_Party");
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
				commandHelper.FieldName = "CasePartyID";
				commandHelper.FieldValue = ids;
				commandHelper.SqlFieldType = SqlDbType.NVarChar;
				commandHelper.TableName = "Case_Party";
				sqlBuilder.AddCommandHelper(commandHelper);
			}
			DataSet dataSet = new DataSet();
			dataSet.EnforceConstraints = false;
			sqlBuilder.UseDistinct = false;
			FillDataSet(dataSet, "Case_Party", sqlBuilder);
			return dataSet;
		}

		public DataSet GetCasePartyList()
		{
			DataSet dataSet = new DataSet();
			string textCommand = "SELECT CasePartyID [Code],CasePartyName [Name],Note,IsInactive [Inactive]\r\n                           FROM Case_Party  ";
			FillDataSet(dataSet, "Case_Party", textCommand);
			return dataSet;
		}

		public DataSet GetCasePartyComboList()
		{
			DataSet dataSet = new DataSet();
			string textCommand = "SELECT CasePartyID [Code],CasePartyName [Name]\r\n                           FROM Case_Party \r\n                            WHERE IsInactive<>1  ORDER BY CasePartyID,CasePartyName";
			FillDataSet(dataSet, "Case_Party", textCommand);
			return dataSet;
		}
	}
}
