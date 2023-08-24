using Micromind.Common;
using Micromind.Common.Data;
using Micromind.Common.Libraries;
using System;
using System.Data;
using System.Data.SqlClient;

namespace Micromind.Data
{
	public sealed class Skill : StoreObject
	{
		private const string SKILLID_PARM = "@SkillID";

		private const string SKILLNAME_PARM = "@SkillName";

		public const string SKILL_TABLE = "Skill";

		private const string CREATEDBY_PARM = "@CreatedBy";

		private const string DATECREATED_PARM = "@DateCreated";

		private const string UPDATEDBY_PARM = "@UpdatedBy";

		private const string DATEUPDATED_PARM = "@DateUpdated";

		public Skill(Config config)
			: base(config)
		{
		}

		private string GetInsertUpdateText(bool isUpdate)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddInsertUpdateParameters("Skill", new FieldValue("SkillID", "@SkillID", isUpdateConditionField: true), new FieldValue("SkillName", "@SkillName"));
			if (isUpdate)
			{
				sqlBuilder.AddInsertUpdateParameters("Skill", new FieldValue("DateUpdated", "@DateUpdated", isUpdateConditionField: true, checkForNullValue: true));
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
			parameters.Add("@SkillID", SqlDbType.NVarChar);
			parameters.Add("@SkillName", SqlDbType.NVarChar);
			parameters["@SkillID"].SourceColumn = "SkillID";
			parameters["@SkillName"].SourceColumn = "SkillName";
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

		public bool InsertSkill(SkillData accountSkillData)
		{
			bool result = true;
			SqlCommand insertUpdateCommand = GetInsertUpdateCommand(isUpdate: false);
			SqlTransaction sqlTransaction = null;
			try
			{
				sqlTransaction = (insertUpdateCommand.Transaction = base.DBConfig.StartNewTransaction());
				result = Insert(accountSkillData, "Skill", insertUpdateCommand);
				string text = accountSkillData.SkillTable.Rows[0]["SkillID"].ToString();
				AddActivityLog("Skill", text, ActivityTypes.Add, sqlTransaction);
				UpdateTableRowInsertUpdateInfo("Skill", "SkillID", text, sqlTransaction, isInsert: true);
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

		public bool UpdateSkill(SkillData accountSkillData)
		{
			bool flag = true;
			SqlCommand insertUpdateCommand = GetInsertUpdateCommand(isUpdate: true);
			SqlTransaction sqlTransaction = null;
			try
			{
				sqlTransaction = (insertUpdateCommand.Transaction = base.DBConfig.StartNewTransaction());
				flag = Update(accountSkillData, "Skill", insertUpdateCommand);
				if (!flag)
				{
					return flag;
				}
				object obj = accountSkillData.SkillTable.Rows[0]["SkillID"];
				UpdateTableRowByID("Skill", "SkillID", "DateUpdated", obj, DateTime.Now, sqlTransaction);
				string entiyID = accountSkillData.SkillTable.Rows[0]["SkillName"].ToString();
				AddActivityLog("Skill", entiyID, ActivityTypes.Update, sqlTransaction);
				UpdateTableRowInsertUpdateInfo("Skill", "SkillID", obj, sqlTransaction, isInsert: false);
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

		public SkillData GetSkill()
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddTable("Skill");
			SkillData skillData = new SkillData();
			sqlBuilder.UseDistinct = false;
			FillDataSet(skillData, "Skill", sqlBuilder);
			return skillData;
		}

		public bool DeleteSkill(string skillID)
		{
			bool flag = true;
			try
			{
				string commandText = "DELETE FROM Skill WHERE SkillID = '" + skillID + "'";
				flag = Delete(commandText, null);
				if (!flag)
				{
					return flag;
				}
				AddActivityLog("Skill", skillID, ActivityTypes.Delete, null);
				return flag;
			}
			catch
			{
				throw;
			}
		}

		public SkillData GetSkillByID(string id)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			CommandHelper commandHelper = null;
			commandHelper = new CommandHelper();
			commandHelper.FieldName = "SkillID";
			commandHelper.SqlFieldType = SqlDbType.NVarChar;
			commandHelper.FieldValue = id;
			commandHelper.TableName = "Skill";
			sqlBuilder.AddCommandHelper(commandHelper);
			sqlBuilder.IsComparing = true;
			SkillData skillData = new SkillData();
			sqlBuilder.UseDistinct = false;
			FillDataSet(skillData, "Skill", sqlBuilder);
			return skillData;
		}

		public DataSet GetSkillByFields(params string[] columns)
		{
			return GetSkillByFields(null, isInactive: true, columns);
		}

		public DataSet GetSkillByFields(string[] skillID, params string[] columns)
		{
			return GetSkillByFields(skillID, isInactive: true, columns);
		}

		public DataSet GetSkillByFields(string[] ids, bool isInactive, params string[] columns)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddTable("Skill");
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
				commandHelper.FieldName = "SkillID";
				commandHelper.FieldValue = ids;
				commandHelper.SqlFieldType = SqlDbType.NVarChar;
				commandHelper.TableName = "Skill";
				sqlBuilder.AddCommandHelper(commandHelper);
			}
			DataSet dataSet = new DataSet();
			dataSet.EnforceConstraints = false;
			sqlBuilder.UseDistinct = false;
			FillDataSet(dataSet, "Skill", sqlBuilder);
			return dataSet;
		}

		public DataSet GetSkillList()
		{
			DataSet dataSet = new DataSet();
			string textCommand = "SELECT SkillID [Skill Code],SkillName [Skill Name]\r\n                           FROM Skill ";
			FillDataSet(dataSet, "Skill", textCommand);
			return dataSet;
		}

		public DataSet GetSkillComboList()
		{
			DataSet dataSet = new DataSet();
			string textCommand = "SELECT SkillID [Code],SkillName [Name]\r\n                           FROM Skill ORDER BY SkillID,SkillName";
			FillDataSet(dataSet, "Skill", textCommand);
			return dataSet;
		}
	}
}
