using Micromind.Common;
using Micromind.Common.Data;
using Micromind.Common.Libraries;
using System;
using System.Data;
using System.Data.SqlClient;

namespace Micromind.Data
{
	public sealed class Religion : StoreObject
	{
		private const string RELIGIONID_PARM = "@ReligionID";

		private const string RELIGIONNAME_PARM = "@ReligionName";

		public const string RELIGION_TABLE = "Religion";

		private const string CREATEDBY_PARM = "@CreatedBy";

		private const string DATECREATED_PARM = "@DateCreated";

		private const string UPDATEDBY_PARM = "@UpdatedBy";

		private const string DATEUPDATED_PARM = "@DateUpdated";

		public Religion(Config config)
			: base(config)
		{
		}

		private string GetInsertUpdateText(bool isUpdate)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddInsertUpdateParameters("Religion", new FieldValue("ReligionID", "@ReligionID", isUpdateConditionField: true), new FieldValue("ReligionName", "@ReligionName"));
			if (isUpdate)
			{
				sqlBuilder.AddInsertUpdateParameters("Religion", new FieldValue("DateUpdated", "@DateUpdated", isUpdateConditionField: true, checkForNullValue: true));
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
			parameters.Add("@ReligionID", SqlDbType.NVarChar);
			parameters.Add("@ReligionName", SqlDbType.NVarChar);
			parameters["@ReligionID"].SourceColumn = "ReligionID";
			parameters["@ReligionName"].SourceColumn = "ReligionName";
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

		public bool InsertReligion(ReligionData accountReligionData)
		{
			bool result = true;
			SqlCommand insertUpdateCommand = GetInsertUpdateCommand(isUpdate: false);
			SqlTransaction sqlTransaction = null;
			try
			{
				sqlTransaction = (insertUpdateCommand.Transaction = base.DBConfig.StartNewTransaction());
				result = Insert(accountReligionData, "Religion", insertUpdateCommand);
				string text = accountReligionData.ReligionTable.Rows[0]["ReligionID"].ToString();
				AddActivityLog("Religion", text, ActivityTypes.Add, sqlTransaction);
				UpdateTableRowInsertUpdateInfo("Religion", "ReligionID", text, sqlTransaction, isInsert: true);
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

		public bool UpdateReligion(ReligionData accountReligionData)
		{
			bool flag = true;
			SqlCommand insertUpdateCommand = GetInsertUpdateCommand(isUpdate: true);
			SqlTransaction sqlTransaction = null;
			try
			{
				sqlTransaction = (insertUpdateCommand.Transaction = base.DBConfig.StartNewTransaction());
				flag = Update(accountReligionData, "Religion", insertUpdateCommand);
				if (!flag)
				{
					return flag;
				}
				object obj = accountReligionData.ReligionTable.Rows[0]["ReligionID"];
				UpdateTableRowByID("Religion", "ReligionID", "DateUpdated", obj, DateTime.Now, sqlTransaction);
				string entiyID = accountReligionData.ReligionTable.Rows[0]["ReligionName"].ToString();
				AddActivityLog("Religion", entiyID, ActivityTypes.Update, sqlTransaction);
				UpdateTableRowInsertUpdateInfo("Religion", "ReligionID", obj, sqlTransaction, isInsert: false);
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

		public ReligionData GetReligion()
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddTable("Religion");
			ReligionData religionData = new ReligionData();
			sqlBuilder.UseDistinct = false;
			FillDataSet(religionData, "Religion", sqlBuilder);
			return religionData;
		}

		public bool DeleteReligion(string religionID)
		{
			bool flag = true;
			try
			{
				string commandText = "DELETE FROM Religion WHERE ReligionID = '" + religionID + "'";
				flag = Delete(commandText, null);
				if (!flag)
				{
					return flag;
				}
				AddActivityLog("Religion", religionID, ActivityTypes.Delete, null);
				return flag;
			}
			catch
			{
				throw;
			}
		}

		public ReligionData GetReligionByID(string id)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			CommandHelper commandHelper = null;
			commandHelper = new CommandHelper();
			commandHelper.FieldName = "ReligionID";
			commandHelper.SqlFieldType = SqlDbType.NVarChar;
			commandHelper.FieldValue = id;
			commandHelper.TableName = "Religion";
			sqlBuilder.AddCommandHelper(commandHelper);
			sqlBuilder.IsComparing = true;
			ReligionData religionData = new ReligionData();
			sqlBuilder.UseDistinct = false;
			FillDataSet(religionData, "Religion", sqlBuilder);
			return religionData;
		}

		public DataSet GetReligionByFields(params string[] columns)
		{
			return GetReligionByFields(null, isInactive: true, columns);
		}

		public DataSet GetReligionByFields(string[] religionID, params string[] columns)
		{
			return GetReligionByFields(religionID, isInactive: true, columns);
		}

		public DataSet GetReligionByFields(string[] ids, bool isInactive, params string[] columns)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddTable("Religion");
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
				commandHelper.FieldName = "ReligionID";
				commandHelper.FieldValue = ids;
				commandHelper.SqlFieldType = SqlDbType.NVarChar;
				commandHelper.TableName = "Religion";
				sqlBuilder.AddCommandHelper(commandHelper);
			}
			DataSet dataSet = new DataSet();
			dataSet.EnforceConstraints = false;
			sqlBuilder.UseDistinct = false;
			FillDataSet(dataSet, "Religion", sqlBuilder);
			return dataSet;
		}

		public DataSet GetReligionList()
		{
			DataSet dataSet = new DataSet();
			string textCommand = "SELECT ReligionID [Religion Code],ReligionName [Religion Name]\r\n                           FROM Religion ";
			FillDataSet(dataSet, "Religion", textCommand);
			return dataSet;
		}

		public DataSet GetReligionComboList()
		{
			DataSet dataSet = new DataSet();
			string textCommand = "SELECT ReligionID [Code],ReligionName [Name]\r\n                           FROM Religion ORDER BY ReligionID,ReligionName";
			FillDataSet(dataSet, "Religion", textCommand);
			return dataSet;
		}
	}
}
