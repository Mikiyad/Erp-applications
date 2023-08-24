using Micromind.Common;
using Micromind.Common.Data;
using Micromind.Common.Libraries;
using System;
using System.Data;
using System.Data.SqlClient;

namespace Micromind.Data
{
	public sealed class EOSRule : StoreObject
	{
		private const string EOSRULEID_PARM = "@EOSRuleID";

		private const string EOSRULENAME_PARM = "@EOSRuleName";

		private const string INACTIVE_PARM = "@Inactive";

		private const string ACCOUNTID_PARM = "@AccountID";

		private const string EOSSYSTEM_PARM = "@EOSSystem";

		private const string NOTE_PARM = "@Note";

		private const string EOSRULE_TABLE = "EOSRule";

		private const string CREATEDBY_PARM = "@CreatedBy";

		private const string DATECREATED_PARM = "@DateCreated";

		private const string UPDATEDBY_PARM = "@UpdatedBy";

		private const string DATEUPDATED_PARM = "@DateUpdated";

		public const string EOSRULEDETAIL_TABLE = "Employee_EOSRule_Detail";

		public const string ROWINDEX_PARM = "@RowIndex";

		public const string YEARFROM_PARM = "@YearFrom";

		public const string YEARTO_PARM = "@YearTo";

		public const string EOSDAY_PARM = "@EOSDay";

		public const string RESIGNEDTYPE_PARM = "@ResignedType";

		public EOSRule(Config config)
			: base(config)
		{
		}

		private string GetInsertUpdateText(bool isUpdate)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddInsertUpdateParameters("Employee_EOSRule", new FieldValue("EOSRuleID", "@EOSRuleID", isUpdateConditionField: true), new FieldValue("EOSRuleName", "@EOSRuleName"), new FieldValue("AccountID", "@AccountID"), new FieldValue("EOSSystem", "@EOSSystem"), new FieldValue("Inactive", "@Inactive"), new FieldValue("Note", "@Note"));
			if (isUpdate)
			{
				sqlBuilder.AddInsertUpdateParameters("Employee_EOSRule", new FieldValue("DateUpdated", "@DateUpdated", isUpdateConditionField: true, checkForNullValue: true));
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
			parameters.Add("@EOSRuleID", SqlDbType.NVarChar);
			parameters.Add("@EOSRuleName", SqlDbType.NVarChar);
			parameters.Add("@AccountID", SqlDbType.NVarChar);
			parameters.Add("@EOSSystem", SqlDbType.TinyInt);
			parameters.Add("@Inactive", SqlDbType.Bit);
			parameters.Add("@Note", SqlDbType.NVarChar);
			parameters["@EOSRuleID"].SourceColumn = "EOSRuleID";
			parameters["@EOSRuleName"].SourceColumn = "EOSRuleName";
			parameters["@AccountID"].SourceColumn = "AccountID";
			parameters["@EOSSystem"].SourceColumn = "EOSSystem";
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

		private string GetInsertUpdateDetailText(bool isUpdate)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddInsertUpdateParameters("Employee_EOSRule_Detail", new FieldValue("EOSRuleID", "@EOSRuleID", isUpdateConditionField: true), new FieldValue("RowIndex", "@RowIndex"), new FieldValue("ResignedType", "@ResignedType"), new FieldValue("YearFrom", "@YearFrom"), new FieldValue("YearTo", "@YearTo"), new FieldValue("EOSDay", "@EOSDay"));
			if (isUpdate)
			{
				sqlBuilder.AddInsertUpdateParameters("Events", new FieldValue("DateUpdated", "@DateUpdated", isUpdateConditionField: true, checkForNullValue: true));
			}
			if (isUpdate)
			{
				return sqlBuilder.GetUpdateExpression();
			}
			return sqlBuilder.GetInsertExpression();
		}

		private SqlCommand GetInsertUpdateDetailCommand(bool isUpdate)
		{
			SqlParameterCollection parameters;
			if (isUpdate)
			{
				if (updateCommand != null)
				{
					updateCommand = null;
				}
				updateCommand = new SqlCommand(GetInsertUpdateDetailText(isUpdate: true), base.DBConfig.Connection);
				updateCommand.CommandType = CommandType.Text;
				parameters = updateCommand.Parameters;
			}
			else
			{
				if (insertCommand != null)
				{
					insertCommand = null;
				}
				insertCommand = new SqlCommand(GetInsertUpdateDetailText(isUpdate: false), base.DBConfig.Connection);
				insertCommand.CommandType = CommandType.Text;
				parameters = insertCommand.Parameters;
			}
			parameters.Add("@EOSRuleID", SqlDbType.NVarChar);
			parameters.Add("@RowIndex", SqlDbType.Int);
			parameters.Add("@ResignedType", SqlDbType.Int);
			parameters.Add("@YearFrom", SqlDbType.Int);
			parameters.Add("@YearTo", SqlDbType.Int);
			parameters.Add("@EOSDay", SqlDbType.Int);
			parameters["@EOSRuleID"].SourceColumn = "EOSRuleID";
			parameters["@RowIndex"].SourceColumn = "RowIndex";
			parameters["@ResignedType"].SourceColumn = "ResignedType";
			parameters["@YearFrom"].SourceColumn = "YearFrom";
			parameters["@YearTo"].SourceColumn = "YearTo";
			parameters["@EOSDay"].SourceColumn = "EOSDay";
			if (isUpdate)
			{
				return updateCommand;
			}
			return insertCommand;
		}

		public bool InsertEOSRule(EOSRuleData accountEOSRuleData)
		{
			bool flag = true;
			SqlCommand insertUpdateCommand = GetInsertUpdateCommand(isUpdate: false);
			SqlTransaction sqlTransaction = null;
			try
			{
				sqlTransaction = (insertUpdateCommand.Transaction = base.DBConfig.StartNewTransaction());
				flag = Insert(accountEOSRuleData, "Employee_EOSRule", insertUpdateCommand);
				if (accountEOSRuleData.Tables["Employee_EOSRule_Detail"].Rows.Count > 0)
				{
					insertUpdateCommand = GetInsertUpdateDetailCommand(isUpdate: false);
					insertUpdateCommand.Transaction = sqlTransaction;
					flag &= Insert(accountEOSRuleData, "Employee_EOSRule_Detail", insertUpdateCommand);
				}
				string text = accountEOSRuleData.EOSRuleTable.Rows[0]["EOSRuleID"].ToString();
				AddActivityLog("EOSRule", text, ActivityTypes.Add, sqlTransaction);
				UpdateTableRowInsertUpdateInfo("Employee_EOSRule", "EOSRuleID", text, sqlTransaction, isInsert: true);
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

		public bool UpdateEOSRule(EOSRuleData accountEOSRuleData)
		{
			bool flag = true;
			SqlCommand insertUpdateCommand = GetInsertUpdateCommand(isUpdate: true);
			SqlTransaction sqlTransaction = null;
			try
			{
				sqlTransaction = (insertUpdateCommand.Transaction = base.DBConfig.StartNewTransaction());
				flag = Update(accountEOSRuleData, "Employee_EOSRule", insertUpdateCommand);
				DeleteEOSRuleDetailsRows(accountEOSRuleData.EOSRuleTable.Rows[0]["EOSRuleID"].ToString(), sqlTransaction);
				if (accountEOSRuleData.Tables["Employee_EOSRule_Detail"].Rows.Count > 0)
				{
					insertUpdateCommand = GetInsertUpdateDetailCommand(isUpdate: false);
					insertUpdateCommand.Transaction = sqlTransaction;
					flag &= Insert(accountEOSRuleData, "Employee_EOSRule_Detail", insertUpdateCommand);
				}
				if (!flag)
				{
					return flag;
				}
				object obj = accountEOSRuleData.EOSRuleTable.Rows[0]["EOSRuleID"];
				UpdateTableRowByID("Employee_EOSRule", "EOSRuleID", "DateUpdated", obj, DateTime.Now, sqlTransaction);
				string entiyID = accountEOSRuleData.EOSRuleTable.Rows[0]["EOSRuleName"].ToString();
				AddActivityLog("EOSRule", entiyID, ActivityTypes.Update, sqlTransaction);
				UpdateTableRowInsertUpdateInfo("Employee_EOSRule", "EOSRuleID", obj, sqlTransaction, isInsert: false);
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

		public EOSRuleData GetEOSRule()
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddTable("Employee_EOSRule");
			EOSRuleData eOSRuleData = new EOSRuleData();
			sqlBuilder.UseDistinct = false;
			FillDataSet(eOSRuleData, "Employee_EOSRule", sqlBuilder);
			return eOSRuleData;
		}

		public bool DeleteEOSRule(string EOSRuleID)
		{
			bool flag = true;
			SqlTransaction sqlTransaction = null;
			try
			{
				sqlTransaction = base.DBConfig.StartNewTransaction();
				DeleteEOSRuleDetailsRows(EOSRuleID, sqlTransaction);
				string commandText = "DELETE FROM Employee_EOSRule WHERE EOSRuleID = '" + EOSRuleID + "'";
				flag &= Delete(commandText, null);
				if (!flag)
				{
					return flag;
				}
				AddActivityLog("EOSRule", EOSRuleID, ActivityTypes.Delete, null);
				return flag;
			}
			catch
			{
				throw;
			}
		}

		internal bool DeleteEOSRuleDetailsRows(string EOSRuleID, SqlTransaction sqlTransaction)
		{
			bool result = true;
			try
			{
				string commandText = "DELETE FROM Employee_EOSRule_Detail WHERE EOSRuleID = '" + EOSRuleID + "'";
				result = Delete(commandText, sqlTransaction);
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

		public EOSRuleData GetEOSRuleByID(string id)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			CommandHelper commandHelper = null;
			commandHelper = new CommandHelper();
			commandHelper.FieldName = "EOSRuleID";
			commandHelper.SqlFieldType = SqlDbType.NVarChar;
			commandHelper.FieldValue = id;
			commandHelper.TableName = "Employee_EOSRule";
			sqlBuilder.AddCommandHelper(commandHelper);
			sqlBuilder.IsComparing = true;
			EOSRuleData eOSRuleData = new EOSRuleData();
			sqlBuilder.UseDistinct = false;
			FillDataSet(eOSRuleData, "Employee_EOSRule", sqlBuilder);
			string textCommand = "SELECT * FROM Employee_EOSRule_Detail                       \r\n                            WHERE EOSRuleID='" + id + "' order by ResignedType,RowIndex";
			FillDataSet(eOSRuleData, "Employee_EOSRule_Detail", textCommand);
			return eOSRuleData;
		}

		public DataSet GetEOSRuleByFields(params string[] columns)
		{
			return GetEOSRuleByFields(null, isInactive: true, columns);
		}

		public DataSet GetEOSRuleByFields(string[] EOSRuleID, params string[] columns)
		{
			return GetEOSRuleByFields(EOSRuleID, isInactive: true, columns);
		}

		public DataSet GetEOSRuleByFields(string[] ids, bool isInactive, params string[] columns)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddTable("Employee_EOSRule");
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
				commandHelper.FieldName = "EOSRuleID";
				commandHelper.FieldValue = ids;
				commandHelper.SqlFieldType = SqlDbType.NVarChar;
				commandHelper.TableName = "Employee_EOSRule";
				sqlBuilder.AddCommandHelper(commandHelper);
			}
			DataSet dataSet = new DataSet();
			dataSet.EnforceConstraints = false;
			sqlBuilder.UseDistinct = false;
			FillDataSet(dataSet, "Employee_EOSRule", sqlBuilder);
			return dataSet;
		}

		public DataSet GetEOSRuleList()
		{
			DataSet dataSet = new DataSet();
			string textCommand = "SELECT EOSRuleID [EOS Code],EOSRuleName [EOS Name],Case When EOSSystem=1 Then 'UAE' Else 'Others' End As 'EOS System',Note,Inactive\r\n                           FROM Employee_EOSRule ";
			FillDataSet(dataSet, "Employee_EOSRule", textCommand);
			return dataSet;
		}

		public DataSet GetEOSRuleComboList()
		{
			DataSet dataSet = new DataSet();
			string textCommand = "SELECT EOSRuleID [Code],EOSRuleName [Name]\r\n                           FROM Employee_EOSRule ORDER BY EOSRuleID,EOSRuleName";
			FillDataSet(dataSet, "Employee_EOSRule", textCommand);
			return dataSet;
		}

		public DataSet GetEOSRuleList(bool inActive)
		{
			DataSet dataSet = new DataSet();
			string textCommand = "SELECT EOSRuleID [EOS Code],EOSRuleName [EOS Name],Case EOSSystem=1 Then 'UAE' Else 'Others' End As 'EOS System',AccountID,Note,Inactive\r\n                           FROM Employee_EOSRule ";
			FillDataSet(dataSet, "Employee_EOSRule", textCommand);
			return dataSet;
		}
	}
}
