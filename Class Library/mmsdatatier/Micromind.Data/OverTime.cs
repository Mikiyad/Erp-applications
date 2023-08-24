using Micromind.Common;
using Micromind.Common.Data;
using Micromind.Common.Libraries;
using System;
using System.Data;
using System.Data.SqlClient;

namespace Micromind.Data
{
	public sealed class OverTime : StoreObject
	{
		private const string OVERTIMEID_PARM = "@OverTimeID";

		private const string OVERTIMENAME_PARM = "@OverTimeName";

		private const string ISFIXED_PARM = "@IsFixed";

		private const string FIXEDAMOUNT_PARM = "@FixedAmount";

		private const string FACTOR_PARM = "@Factor";

		private const string FACTORTYPE_PARM = "@FactorType";

		private const string ACCOUNTID_PARM = "@AccountID";

		private const string INACTIVE_PARM = "@Inactive";

		private const string NOTE_PARM = "@Note";

		private const string OVERTIME_TABLE = "Employee_OverTime";

		private const string CREATEDBY_PARM = "@CreatedBy";

		private const string DATECREATED_PARM = "@DateCreated";

		private const string UPDATEDBY_PARM = "@UpdatedBy";

		private const string DATEUPDATED_PARM = "@DateUpdated";

		public OverTime(Config config)
			: base(config)
		{
		}

		private string GetInsertUpdateText(bool isUpdate)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddInsertUpdateParameters("Employee_OverTime", new FieldValue("OverTimeID", "@OverTimeID", isUpdateConditionField: true), new FieldValue("OverTimeName", "@OverTimeName"), new FieldValue("IsFixed", "@IsFixed"), new FieldValue("FixedAmount", "@FixedAmount"), new FieldValue("Factor", "@Factor"), new FieldValue("FactorType", "@FactorType"), new FieldValue("AccountID", "@AccountID"), new FieldValue("Inactive", "@Inactive"), new FieldValue("Note", "@Note"));
			if (isUpdate)
			{
				sqlBuilder.AddInsertUpdateParameters("Employee_OverTime", new FieldValue("DateUpdated", "@DateUpdated", isUpdateConditionField: true, checkForNullValue: true));
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
			parameters.Add("@OverTimeID", SqlDbType.NVarChar);
			parameters.Add("@OverTimeName", SqlDbType.NVarChar);
			parameters.Add("@IsFixed", SqlDbType.NVarChar);
			parameters.Add("@FixedAmount", SqlDbType.NVarChar);
			parameters.Add("@Factor", SqlDbType.NVarChar);
			parameters.Add("@FactorType", SqlDbType.NVarChar);
			parameters.Add("@AccountID", SqlDbType.NVarChar);
			parameters.Add("@Inactive", SqlDbType.Bit);
			parameters.Add("@Note", SqlDbType.NVarChar);
			parameters["@OverTimeID"].SourceColumn = "OverTimeID";
			parameters["@OverTimeName"].SourceColumn = "OverTimeName";
			parameters["@IsFixed"].SourceColumn = "IsFixed";
			parameters["@FixedAmount"].SourceColumn = "FixedAmount";
			parameters["@Factor"].SourceColumn = "Factor";
			parameters["@FactorType"].SourceColumn = "FactorType";
			parameters["@AccountID"].SourceColumn = "AccountID";
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

		public bool InsertOverTime(OverTimeData accountOverTimeData)
		{
			bool result = true;
			SqlCommand insertUpdateCommand = GetInsertUpdateCommand(isUpdate: false);
			SqlTransaction sqlTransaction = null;
			try
			{
				sqlTransaction = (insertUpdateCommand.Transaction = base.DBConfig.StartNewTransaction());
				result = Insert(accountOverTimeData, "Employee_OverTime", insertUpdateCommand);
				string text = accountOverTimeData.OverTimeTable.Rows[0]["OverTimeID"].ToString();
				AddActivityLog("OverTime", text, ActivityTypes.Add, sqlTransaction);
				UpdateTableRowInsertUpdateInfo("Employee_OverTime", "OverTimeID", text, sqlTransaction, isInsert: true);
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

		public bool UpdateOverTime(OverTimeData accountOverTimeData)
		{
			bool flag = true;
			SqlCommand insertUpdateCommand = GetInsertUpdateCommand(isUpdate: true);
			SqlTransaction sqlTransaction = null;
			try
			{
				sqlTransaction = (insertUpdateCommand.Transaction = base.DBConfig.StartNewTransaction());
				flag = Update(accountOverTimeData, "Employee_OverTime", insertUpdateCommand);
				if (!flag)
				{
					return flag;
				}
				object obj = accountOverTimeData.OverTimeTable.Rows[0]["OverTimeID"];
				UpdateTableRowByID("Employee_OverTime", "OverTimeID", "DateUpdated", obj, DateTime.Now, sqlTransaction);
				string entiyID = accountOverTimeData.OverTimeTable.Rows[0]["OverTimeName"].ToString();
				AddActivityLog("OverTime", entiyID, ActivityTypes.Update, sqlTransaction);
				UpdateTableRowInsertUpdateInfo("Employee_OverTime", "OverTimeID", obj, sqlTransaction, isInsert: false);
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

		public OverTimeData GetOverTime()
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddTable("Employee_OverTime");
			OverTimeData overTimeData = new OverTimeData();
			sqlBuilder.UseDistinct = false;
			FillDataSet(overTimeData, "Employee_OverTime", sqlBuilder);
			return overTimeData;
		}

		public bool DeleteOverTime(string overtimeID)
		{
			bool flag = true;
			try
			{
				string commandText = "DELETE FROM Employee_OverTime WHERE OverTimeID = '" + overtimeID + "'";
				flag = Delete(commandText, null);
				if (!flag)
				{
					return flag;
				}
				AddActivityLog("OverTime", overtimeID, ActivityTypes.Delete, null);
				return flag;
			}
			catch
			{
				throw;
			}
		}

		public OverTimeData GetOverTimeByID(string id)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			CommandHelper commandHelper = null;
			commandHelper = new CommandHelper();
			commandHelper.FieldName = "OverTimeID";
			commandHelper.SqlFieldType = SqlDbType.NVarChar;
			commandHelper.FieldValue = id;
			commandHelper.TableName = "Employee_OverTime";
			sqlBuilder.AddCommandHelper(commandHelper);
			sqlBuilder.IsComparing = true;
			OverTimeData overTimeData = new OverTimeData();
			sqlBuilder.UseDistinct = false;
			FillDataSet(overTimeData, "Employee_OverTime", sqlBuilder);
			return overTimeData;
		}

		public DataSet GetOverTimeByFields(params string[] columns)
		{
			return GetOverTimeByFields(null, isInactive: true, columns);
		}

		public DataSet GetOverTimeByFields(string[] overtimeID, params string[] columns)
		{
			return GetOverTimeByFields(overtimeID, isInactive: true, columns);
		}

		public DataSet GetOverTimeByFields(string[] ids, bool isInactive, params string[] columns)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddTable("Employee_OverTime");
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
				commandHelper.FieldName = "OverTimeID";
				commandHelper.FieldValue = ids;
				commandHelper.SqlFieldType = SqlDbType.NVarChar;
				commandHelper.TableName = "Employee_OverTime";
				sqlBuilder.AddCommandHelper(commandHelper);
			}
			DataSet dataSet = new DataSet();
			dataSet.EnforceConstraints = false;
			sqlBuilder.UseDistinct = false;
			FillDataSet(dataSet, "Employee_OverTime", sqlBuilder);
			return dataSet;
		}

		public DataSet GetOverTimeList()
		{
			DataSet dataSet = new DataSet();
			string textCommand = "SELECT OverTimeID [OverTime Code],OverTimeName [OverTime Name],Note,Inactive\r\n                           FROM Employee_OverTime ";
			FillDataSet(dataSet, "Employee_OverTime", textCommand);
			return dataSet;
		}

		public DataSet GetOverTimeComboList()
		{
			DataSet dataSet = new DataSet();
			string textCommand = "SELECT OverTimeID [Code],OverTimeName [Name], IsFixed, FixedAmount, Factor \r\n                           FROM Employee_OverTime ORDER BY OverTimeID,OverTimeName";
			FillDataSet(dataSet, "Employee_OverTime", textCommand);
			return dataSet;
		}
	}
}
