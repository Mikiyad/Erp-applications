using Micromind.Common;
using Micromind.Common.Data;
using Micromind.Common.Libraries;
using System;
using System.Data;
using System.Data.SqlClient;

namespace Micromind.Data
{
	public sealed class LegalActionStatus : StoreObject
	{
		private const string LEGALACTIONSTATUSID_PARM = "@LegalActionStatusID";

		private const string LEGALACTIONSTATUSNAME_PARM = "@LegalActionStatusName";

		private const string ISINACTIVE_PARM = "@IsInactive";

		private const string ISFINALIZED_PARM = "@IsFinalized";

		private const string NOTE_PARM = "@Note";

		private const string LEGALACTIONSTATUS_TABLE = "Legal_Action_Status";

		private const string CREATEDBY_PARM = "@CreatedBy";

		private const string DATECREATED_PARM = "@DateCreated";

		private const string UPDATEDBY_PARM = "@UpdatedBy";

		private const string DATEUPDATED_PARM = "@DateUpdated";

		public LegalActionStatus(Config config)
			: base(config)
		{
		}

		private string GetInsertUpdateText(bool isUpdate)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddInsertUpdateParameters("Legal_Action_Status", new FieldValue("LegalActionStatusID", "@LegalActionStatusID", isUpdateConditionField: true), new FieldValue("LegalActionStatusName", "@LegalActionStatusName"), new FieldValue("IsInactive", "@IsInactive"), new FieldValue("IsFinalized", "@IsFinalized"), new FieldValue("Note", "@Note"));
			if (isUpdate)
			{
				sqlBuilder.AddInsertUpdateParameters("Legal_Action_Status", new FieldValue("DateUpdated", "@DateUpdated", isUpdateConditionField: true, checkForNullValue: true));
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
			parameters.Add("@LegalActionStatusID", SqlDbType.NVarChar);
			parameters.Add("@LegalActionStatusName", SqlDbType.NVarChar);
			parameters.Add("@IsInactive", SqlDbType.Bit);
			parameters.Add("@IsFinalized", SqlDbType.Bit);
			parameters.Add("@Note", SqlDbType.NVarChar);
			parameters["@LegalActionStatusID"].SourceColumn = "LegalActionStatusID";
			parameters["@LegalActionStatusName"].SourceColumn = "LegalActionStatusName";
			parameters["@IsInactive"].SourceColumn = "IsInactive";
			parameters["@IsFinalized"].SourceColumn = "IsFinalized";
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

		public bool InsertLegalActionStatus(LegalActionStatusData accountLegalActionStatusData)
		{
			bool result = true;
			SqlCommand insertUpdateCommand = GetInsertUpdateCommand(isUpdate: false);
			SqlTransaction sqlTransaction = null;
			try
			{
				sqlTransaction = (insertUpdateCommand.Transaction = base.DBConfig.StartNewTransaction());
				result = Insert(accountLegalActionStatusData, "Legal_Action_Status", insertUpdateCommand);
				string text = accountLegalActionStatusData.LegalActionStatusTable.Rows[0]["LegalActionStatusID"].ToString();
				AddActivityLog("Legal Action Status", text, ActivityTypes.Add, sqlTransaction);
				UpdateTableRowInsertUpdateInfo("Legal_Action_Status", "LegalActionStatusID", text, sqlTransaction, isInsert: true);
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

		public bool UpdateLegalActionStatus(LegalActionStatusData accountLegalActionStatusData)
		{
			bool flag = true;
			SqlCommand insertUpdateCommand = GetInsertUpdateCommand(isUpdate: true);
			SqlTransaction sqlTransaction = null;
			try
			{
				sqlTransaction = (insertUpdateCommand.Transaction = base.DBConfig.StartNewTransaction());
				flag = Update(accountLegalActionStatusData, "Legal_Action_Status", insertUpdateCommand);
				if (!flag)
				{
					return flag;
				}
				object obj = accountLegalActionStatusData.LegalActionStatusTable.Rows[0]["LegalActionStatusID"];
				UpdateTableRowByID("Legal_Action_Status", "LegalActionStatusID", "DateUpdated", obj, DateTime.Now, sqlTransaction);
				string entiyID = accountLegalActionStatusData.LegalActionStatusTable.Rows[0]["LegalActionStatusName"].ToString();
				AddActivityLog("Legal Action Status", entiyID, ActivityTypes.Update, sqlTransaction);
				UpdateTableRowInsertUpdateInfo("Legal_Action_Status", "LegalActionStatusID", obj, sqlTransaction, isInsert: false);
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

		public LegalActionStatusData GetLegalActionStatus()
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddTable("Legal_Action_Status");
			LegalActionStatusData legalActionStatusData = new LegalActionStatusData();
			sqlBuilder.UseDistinct = false;
			FillDataSet(legalActionStatusData, "Legal_Action_Status", sqlBuilder);
			return legalActionStatusData;
		}

		public bool DeleteLegalActionStatus(string LegalActionStatusID)
		{
			bool flag = true;
			try
			{
				string commandText = "DELETE FROM Legal_Action_Status WHERE LegalActionStatusID = '" + LegalActionStatusID + "'";
				flag = Delete(commandText, null);
				if (!flag)
				{
					return flag;
				}
				AddActivityLog("Legal Action Status", LegalActionStatusID, ActivityTypes.Delete, null);
				return flag;
			}
			catch
			{
				throw;
			}
		}

		public LegalActionStatusData GetLegalActionStatusByID(string id)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			CommandHelper commandHelper = null;
			commandHelper = new CommandHelper();
			commandHelper.FieldName = "LegalActionStatusID";
			commandHelper.SqlFieldType = SqlDbType.NVarChar;
			commandHelper.FieldValue = id;
			commandHelper.TableName = "Legal_Action_Status";
			sqlBuilder.AddCommandHelper(commandHelper);
			sqlBuilder.IsComparing = true;
			LegalActionStatusData legalActionStatusData = new LegalActionStatusData();
			sqlBuilder.UseDistinct = false;
			FillDataSet(legalActionStatusData, "Legal_Action_Status", sqlBuilder);
			return legalActionStatusData;
		}

		public DataSet GetLegalActionStatusByFields(params string[] columns)
		{
			return GetLegalActionStatusByFields(null, isInactive: true, columns);
		}

		public DataSet GetLegalActionStatusByFields(string[] releaseTypeID, params string[] columns)
		{
			return GetLegalActionStatusByFields(releaseTypeID, isInactive: true, columns);
		}

		public DataSet GetLegalActionStatusByFields(string[] ids, bool isInactive, params string[] columns)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddTable("Legal_Action_Status");
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
				commandHelper.FieldName = "LegalActionStatusID";
				commandHelper.FieldValue = ids;
				commandHelper.SqlFieldType = SqlDbType.NVarChar;
				commandHelper.TableName = "Legal_Action_Status";
				sqlBuilder.AddCommandHelper(commandHelper);
			}
			DataSet dataSet = new DataSet();
			dataSet.EnforceConstraints = false;
			sqlBuilder.UseDistinct = false;
			FillDataSet(dataSet, "Legal_Action_Status", sqlBuilder);
			return dataSet;
		}

		public DataSet GetLegalActionStatusList()
		{
			DataSet dataSet = new DataSet();
			string textCommand = "SELECT LegalActionStatusID [LegalActionStatus Code],LegalActionStatusName [LegalActionStatus Name],Note,IsInactive [Inactive], IsFinalized\r\n                           FROM Legal_Action_Status ";
			FillDataSet(dataSet, "Legal_Action_Status", textCommand);
			return dataSet;
		}

		public DataSet GetLegalActionStatusComboList()
		{
			DataSet dataSet = new DataSet();
			string textCommand = "SELECT LegalActionStatusID [Code],LegalActionStatusName [Name]\r\n                           FROM Legal_Action_Status ORDER BY LegalActionStatusID,LegalActionStatusName";
			FillDataSet(dataSet, "Legal_Action_Status", textCommand);
			return dataSet;
		}
	}
}
