using Micromind.Common;
using Micromind.Common.Data;
using Micromind.Common.Libraries;
using System;
using System.Data;
using System.Data.SqlClient;

namespace Micromind.Data
{
	public sealed class DisciplineActionType : StoreObject
	{
		private const string ACTIONTYPEID_PARM = "@ActionTypeID";

		private const string ACTIONTYPENAME_PARM = "@ActionTypeName";

		private const string INACTIVE_PARM = "@Inactive";

		public const string NOTE_PARM = "@Note";

		public const string ACTIONTYPE_TABLE = "Discipline_Action_Type";

		private const string CREATEDBY_PARM = "@CreatedBy";

		private const string DATECREATED_PARM = "@DateCreated";

		private const string UPDATEDBY_PARM = "@UpdatedBy";

		private const string DATEUPDATED_PARM = "@DateUpdated";

		public DisciplineActionType(Config config)
			: base(config)
		{
		}

		private string GetInsertUpdateText(bool isUpdate)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddInsertUpdateParameters("Discipline_Action_Type", new FieldValue("ActionTypeID", "@ActionTypeID", isUpdateConditionField: true), new FieldValue("ActionTypeName", "@ActionTypeName"), new FieldValue("Inactive", "@Inactive"), new FieldValue("Note", "@Note"));
			if (isUpdate)
			{
				sqlBuilder.AddInsertUpdateParameters("Discipline_Action_Type", new FieldValue("DateUpdated", "@DateUpdated", isUpdateConditionField: true, checkForNullValue: true));
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
			parameters.Add("@ActionTypeID", SqlDbType.NVarChar);
			parameters.Add("@ActionTypeName", SqlDbType.NVarChar);
			parameters.Add("@Inactive", SqlDbType.Bit);
			parameters.Add("@Note", SqlDbType.NVarChar);
			parameters["@ActionTypeID"].SourceColumn = "ActionTypeID";
			parameters["@ActionTypeName"].SourceColumn = "ActionTypeName";
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

		public bool InsertActionType(DisciplineActionTypeData accountDisciplineActionTypeData)
		{
			bool result = true;
			SqlCommand insertUpdateCommand = GetInsertUpdateCommand(isUpdate: false);
			SqlTransaction sqlTransaction = null;
			try
			{
				sqlTransaction = (insertUpdateCommand.Transaction = base.DBConfig.StartNewTransaction());
				result = Insert(accountDisciplineActionTypeData, "Discipline_Action_Type", insertUpdateCommand);
				string text = accountDisciplineActionTypeData.ActionTypeTable.Rows[0]["ActionTypeID"].ToString();
				AddActivityLog("ActionType", text, ActivityTypes.Add, sqlTransaction);
				UpdateTableRowInsertUpdateInfo("Discipline_Action_Type", "ActionTypeID", text, sqlTransaction, isInsert: true);
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

		public bool UpdateActionType(DisciplineActionTypeData accountDisciplineActionTypeData)
		{
			bool flag = true;
			SqlCommand insertUpdateCommand = GetInsertUpdateCommand(isUpdate: true);
			SqlTransaction sqlTransaction = null;
			try
			{
				sqlTransaction = (insertUpdateCommand.Transaction = base.DBConfig.StartNewTransaction());
				flag = Update(accountDisciplineActionTypeData, "Discipline_Action_Type", insertUpdateCommand);
				if (!flag)
				{
					return flag;
				}
				object obj = accountDisciplineActionTypeData.ActionTypeTable.Rows[0]["ActionTypeID"];
				UpdateTableRowByID("Discipline_Action_Type", "ActionTypeID", "DateUpdated", obj, DateTime.Now, sqlTransaction);
				string entiyID = accountDisciplineActionTypeData.ActionTypeTable.Rows[0]["ActionTypeName"].ToString();
				AddActivityLog("ActionType", entiyID, ActivityTypes.Update, sqlTransaction);
				UpdateTableRowInsertUpdateInfo("Discipline_Action_Type", "ActionTypeID", obj, sqlTransaction, isInsert: false);
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

		public DisciplineActionTypeData GetActionType()
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddTable("Discipline_Action_Type");
			DisciplineActionTypeData disciplineActionTypeData = new DisciplineActionTypeData();
			sqlBuilder.UseDistinct = false;
			FillDataSet(disciplineActionTypeData, "Discipline_Action_Type", sqlBuilder);
			return disciplineActionTypeData;
		}

		public bool DeleteActionType(string actionTypeID)
		{
			bool flag = true;
			try
			{
				string commandText = "DELETE FROM Discipline_Action_Type WHERE ActionTypeID = '" + actionTypeID + "'";
				flag = Delete(commandText, null);
				if (!flag)
				{
					return flag;
				}
				AddActivityLog("ActionType", actionTypeID, ActivityTypes.Delete, null);
				return flag;
			}
			catch
			{
				throw;
			}
		}

		public DisciplineActionTypeData GetActionTypeByID(string id)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			CommandHelper commandHelper = null;
			commandHelper = new CommandHelper();
			commandHelper.FieldName = "ActionTypeID";
			commandHelper.SqlFieldType = SqlDbType.NVarChar;
			commandHelper.FieldValue = id;
			commandHelper.TableName = "Discipline_Action_Type";
			sqlBuilder.AddCommandHelper(commandHelper);
			sqlBuilder.IsComparing = true;
			DisciplineActionTypeData disciplineActionTypeData = new DisciplineActionTypeData();
			sqlBuilder.UseDistinct = false;
			FillDataSet(disciplineActionTypeData, "Discipline_Action_Type", sqlBuilder);
			return disciplineActionTypeData;
		}

		public DataSet GetActionTypeByFields(params string[] columns)
		{
			return GetActionTypeByFields(null, isInactive: true, columns);
		}

		public DataSet GetActionTypeByFields(string[] actionTypeID, params string[] columns)
		{
			return GetActionTypeByFields(actionTypeID, isInactive: true, columns);
		}

		public DataSet GetActionTypeByFields(string[] ids, bool isInactive, params string[] columns)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddTable("Discipline_Action_Type");
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
				commandHelper.FieldName = "ActionTypeID";
				commandHelper.FieldValue = ids;
				commandHelper.SqlFieldType = SqlDbType.NVarChar;
				commandHelper.TableName = "Discipline_Action_Type";
				sqlBuilder.AddCommandHelper(commandHelper);
			}
			DataSet dataSet = new DataSet();
			dataSet.EnforceConstraints = false;
			sqlBuilder.UseDistinct = false;
			FillDataSet(dataSet, "Discipline_Action_Type", sqlBuilder);
			return dataSet;
		}

		public DataSet GetActionTypeList()
		{
			DataSet dataSet = new DataSet();
			string textCommand = "SELECT ActionTypeID [Type Code],ActionTypeName [Action Type Name],Note,Inactive  \r\n                           FROM Discipline_Action_Type ";
			FillDataSet(dataSet, "Discipline_Action_Type", textCommand);
			return dataSet;
		}

		public DataSet GetActionTypeComboList()
		{
			DataSet dataSet = new DataSet();
			string textCommand = "SELECT ActionTypeID [Code],ActionTypeName [Name]\r\n                           FROM Discipline_Action_Type ORDER BY ActionTypeID,ActionTypeName";
			FillDataSet(dataSet, "Discipline_Action_Type", textCommand);
			return dataSet;
		}
	}
}
