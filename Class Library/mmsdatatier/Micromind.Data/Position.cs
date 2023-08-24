using Micromind.Common;
using Micromind.Common.Data;
using Micromind.Common.Libraries;
using System;
using System.Data;
using System.Data.SqlClient;

namespace Micromind.Data
{
	public sealed class Position : StoreObject
	{
		private const string POSITIONID_PARM = "@PositionID";

		private const string POSITIONNAME_PARM = "@PositionName";

		private const string REPORTTO_PARM = "@ReportTo";

		private const string INACTIVE_PARM = "@Inactive";

		private const string JOBDESCRIPTION_PARM = "@JobDescription";

		private const string NOTE_PARM = "@Note";

		private const string APPRAISALINTERVAL_PARM = "@AppraisalInterval";

		private const string APPRAISALFROMDATE_PARM = "@AppraisalFromDate";

		private const string APPRAISALTODATE_PARM = "@AppraisalToDate";

		private const string POSITION_TABLE = "Position";

		private const string CREATEDBY_PARM = "@CreatedBy";

		private const string DATECREATED_PARM = "@DateCreated";

		private const string UPDATEDBY_PARM = "@UpdatedBy";

		private const string DATEUPDATED_PARM = "@DateUpdated";

		private const string ROWINDEX_PARM = "@RowIndex";

		private const string KPIPARAMETER_PARM = "@KPIParameter";

		private const string WEIGHTAGE_PARM = "@Weightage";

		private const string SCALE_PARM = "@Scale";

		private const string TARGET_PARM = "@Target";

		private const string POSITIONDETAILS_TABLE = "PositionDetails";

		private const string PERFORMANCEPARAMETER_PARM = "@PerformanceParameter";

		private const string SCORE_PARM = "@Score";

		private const string PERFORMANCEDETAILS_TABLE = "Performance_Details";

		public Position(Config config)
			: base(config)
		{
		}

		private string GetInsertUpdateText(bool isUpdate)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddInsertUpdateParameters("Position", new FieldValue("PositionID", "@PositionID", isUpdateConditionField: true), new FieldValue("PositionName", "@PositionName"), new FieldValue("AppraisalInterval", "@AppraisalInterval"), new FieldValue("AppraisalFromDate", "@AppraisalFromDate"), new FieldValue("AppraisalToDate", "@AppraisalToDate"), new FieldValue("ReportTo", "@ReportTo"), new FieldValue("Inactive", "@Inactive"), new FieldValue("JobDescription", "@JobDescription"), new FieldValue("Note", "@Note"));
			if (isUpdate)
			{
				sqlBuilder.AddInsertUpdateParameters("Position", new FieldValue("DateUpdated", "@DateUpdated", isUpdateConditionField: true, checkForNullValue: true));
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
			parameters.Add("@PositionID", SqlDbType.NVarChar);
			parameters.Add("@PositionName", SqlDbType.NVarChar);
			parameters.Add("@Note", SqlDbType.NVarChar);
			parameters.Add("@ReportTo", SqlDbType.NVarChar);
			parameters.Add("@AppraisalInterval", SqlDbType.Int);
			parameters.Add("@AppraisalFromDate", SqlDbType.DateTime);
			parameters.Add("@AppraisalToDate", SqlDbType.DateTime);
			parameters.Add("@Inactive", SqlDbType.Bit);
			parameters.Add("@JobDescription", SqlDbType.NVarChar);
			parameters["@PositionID"].SourceColumn = "PositionID";
			parameters["@PositionName"].SourceColumn = "PositionName";
			parameters["@Note"].SourceColumn = "Note";
			parameters["@AppraisalInterval"].SourceColumn = "AppraisalInterval";
			parameters["@ReportTo"].SourceColumn = "ReportTo";
			parameters["@AppraisalFromDate"].SourceColumn = "AppraisalFromDate";
			parameters["@AppraisalToDate"].SourceColumn = "AppraisalToDate";
			parameters["@Inactive"].SourceColumn = "Inactive";
			parameters["@JobDescription"].SourceColumn = "JobDescription";
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

		internal bool DeletePositionDetailsRows(string postionID, SqlTransaction sqlTransaction)
		{
			bool flag = true;
			try
			{
				string commandText = "DELETE FROM Position_Details WHERE PositionID = '" + postionID + "'";
				return Delete(commandText, sqlTransaction);
			}
			catch
			{
				throw;
			}
		}

		internal bool DeletePerformanceDetailsRows(string postionID, SqlTransaction sqlTransaction)
		{
			bool flag = true;
			try
			{
				string commandText = "DELETE FROM Performance_Details WHERE PositionID = '" + postionID + "'";
				return Delete(commandText, sqlTransaction);
			}
			catch
			{
				throw;
			}
		}

		public bool InsertPosition(PositionData accountPositionData)
		{
			bool flag = true;
			SqlCommand insertUpdateCommand = GetInsertUpdateCommand(isUpdate: false);
			SqlTransaction sqlTransaction = null;
			try
			{
				sqlTransaction = base.DBConfig.StartNewTransaction();
				DataRow dataRow = accountPositionData.PositionTable.Rows[0];
				dataRow["PositionID"].ToString();
				foreach (DataRow row in accountPositionData.PositionDetailTable.Rows)
				{
					row["PositionID"] = dataRow["PositionID"];
				}
				foreach (DataRow row2 in accountPositionData.PerformanceDetailTable.Rows)
				{
					row2["PositionID"] = dataRow["PositionID"];
				}
				insertUpdateCommand.Transaction = sqlTransaction;
				flag = Insert(accountPositionData, "Position", insertUpdateCommand);
				if (accountPositionData.Tables["Position_Details"].Rows.Count > 0)
				{
					insertUpdateCommand = GetInsertUpdatePositionDetailsCommand(isUpdate: false);
					insertUpdateCommand.Transaction = sqlTransaction;
					flag &= Insert(accountPositionData, "Position_Details", insertUpdateCommand);
				}
				if (accountPositionData.Tables["Performance_Details"].Rows.Count > 0)
				{
					insertUpdateCommand = GetInsertUpdatePerformanceDetailsCommand(isUpdate: false);
					insertUpdateCommand.Transaction = sqlTransaction;
					flag &= Insert(accountPositionData, "Performance_Details", insertUpdateCommand);
				}
				string text = accountPositionData.PositionTable.Rows[0]["PositionID"].ToString();
				AddActivityLog("Position", text, ActivityTypes.Add, sqlTransaction);
				UpdateTableRowInsertUpdateInfo("Position", "PositionID", text, sqlTransaction, isInsert: true);
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

		public bool UpdatePosition(PositionData accountPositionData)
		{
			bool flag = true;
			SqlCommand insertUpdateCommand = GetInsertUpdateCommand(isUpdate: true);
			SqlTransaction sqlTransaction = null;
			try
			{
				sqlTransaction = base.DBConfig.StartNewTransaction();
				DataRow dataRow = accountPositionData.PositionTable.Rows[0];
				string postionID = dataRow["PositionID"].ToString();
				foreach (DataRow row in accountPositionData.PositionDetailTable.Rows)
				{
					row["PositionID"] = dataRow["PositionID"];
				}
				foreach (DataRow row2 in accountPositionData.PerformanceDetailTable.Rows)
				{
					row2["PositionID"] = dataRow["PositionID"];
				}
				insertUpdateCommand.Transaction = sqlTransaction;
				flag &= DeletePositionDetailsRows(postionID, sqlTransaction);
				flag &= DeletePerformanceDetailsRows(postionID, sqlTransaction);
				flag = Update(accountPositionData, "Position", insertUpdateCommand);
				if (accountPositionData.Tables["Position_Details"].Rows.Count > 0)
				{
					insertUpdateCommand = GetInsertUpdatePositionDetailsCommand(isUpdate: false);
					insertUpdateCommand.Transaction = sqlTransaction;
					flag &= Insert(accountPositionData, "Position_Details", insertUpdateCommand);
				}
				if (accountPositionData.Tables["Performance_Details"].Rows.Count > 0)
				{
					insertUpdateCommand = GetInsertUpdatePerformanceDetailsCommand(isUpdate: false);
					insertUpdateCommand.Transaction = sqlTransaction;
					flag &= Insert(accountPositionData, "Performance_Details", insertUpdateCommand);
				}
				if (!flag)
				{
					return flag;
				}
				object obj = accountPositionData.PositionTable.Rows[0]["PositionID"];
				UpdateTableRowByID("Position", "PositionID", "DateUpdated", obj, DateTime.Now, sqlTransaction);
				string entiyID = accountPositionData.PositionTable.Rows[0]["PositionName"].ToString();
				AddActivityLog("Position", entiyID, ActivityTypes.Update, sqlTransaction);
				UpdateTableRowInsertUpdateInfo("Position", "PositionID", obj, sqlTransaction, isInsert: false);
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

		public PositionData GetPosition()
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddTable("Position");
			PositionData positionData = new PositionData();
			sqlBuilder.UseDistinct = false;
			FillDataSet(positionData, "Position", sqlBuilder);
			return positionData;
		}

		public bool DeletePosition(string positionID)
		{
			bool flag = true;
			try
			{
				SqlTransaction sqlTransaction = base.DBConfig.StartNewTransaction();
				flag &= DeletePositionDetailsRows(positionID, sqlTransaction);
				string exp = "DELETE FROM Position WHERE PositionID = '" + positionID + "'";
				flag &= (ExecuteNonQuery(exp, sqlTransaction) >= 0);
				if (!flag)
				{
					return flag;
				}
				AddActivityLog("Position", positionID, ActivityTypes.Delete, sqlTransaction);
				return flag;
			}
			catch
			{
				throw;
			}
		}

		public PositionData GetPositionByID(string id)
		{
			PositionData positionData = new PositionData();
			string textCommand = "SELECT * FROM Position WHERE PositionID='" + id + "'";
			FillDataSet(positionData, "Position", textCommand);
			if (positionData == null || positionData.Tables.Count == 0 || positionData.Tables["Position"].Rows.Count == 0)
			{
				return null;
			}
			textCommand = "SELECT *  FROM Position_Details WHERE PositionID='" + id + "'";
			FillDataSet(positionData, "Position_Details", textCommand);
			textCommand = "SELECT *  FROM Performance_Details WHERE PositionID='" + id + "'";
			FillDataSet(positionData, "Performance_Details", textCommand);
			return positionData;
		}

		public DataSet GetPositionByFields(params string[] columns)
		{
			return GetPositionByFields(null, isInactive: true, columns);
		}

		public DataSet GetPositionByFields(string[] positionID, params string[] columns)
		{
			return GetPositionByFields(positionID, isInactive: true, columns);
		}

		public DataSet GetPositionByFields(string[] ids, bool isInactive, params string[] columns)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddTable("Position");
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
				commandHelper.FieldName = "PositionID";
				commandHelper.FieldValue = ids;
				commandHelper.SqlFieldType = SqlDbType.NVarChar;
				commandHelper.TableName = "Position";
				sqlBuilder.AddCommandHelper(commandHelper);
			}
			DataSet dataSet = new DataSet();
			dataSet.EnforceConstraints = false;
			sqlBuilder.UseDistinct = false;
			FillDataSet(dataSet, "Position", sqlBuilder);
			return dataSet;
		}

		public DataSet GetPositionList()
		{
			DataSet dataSet = new DataSet();
			string textCommand = "SELECT PositionID [Position Code],PositionName [Position Name],Note,Inactive\r\n                           FROM Position ";
			FillDataSet(dataSet, "Position", textCommand);
			return dataSet;
		}

		public DataSet GetPositionComboList()
		{
			DataSet dataSet = new DataSet();
			string textCommand = "SELECT PositionID [Code],PositionName [Name]\r\n                           FROM Position ORDER BY PositionID,PositionName";
			FillDataSet(dataSet, "Position", textCommand);
			return dataSet;
		}

		private SqlCommand GetInsertUpdatePositionDetailsCommand(bool isUpdate)
		{
			SqlParameterCollection parameters;
			if (isUpdate)
			{
				if (updateCommand != null)
				{
					updateCommand = null;
				}
				updateCommand = new SqlCommand(GetInsertUpdatePositionDetailsText(isUpdate: true), base.DBConfig.Connection);
				updateCommand.CommandType = CommandType.Text;
				parameters = updateCommand.Parameters;
			}
			else
			{
				if (insertCommand != null)
				{
					insertCommand = null;
				}
				insertCommand = new SqlCommand(GetInsertUpdatePositionDetailsText(isUpdate: false), base.DBConfig.Connection);
				insertCommand.CommandType = CommandType.Text;
				parameters = insertCommand.Parameters;
			}
			parameters.Add("@PositionID", SqlDbType.NVarChar);
			parameters.Add("@KPIParameter", SqlDbType.NText);
			parameters.Add("@Weightage", SqlDbType.Decimal);
			parameters.Add("@Scale", SqlDbType.NText);
			parameters.Add("@Target", SqlDbType.Decimal);
			parameters.Add("@RowIndex", SqlDbType.Int);
			parameters["@PositionID"].SourceColumn = "PositionID";
			parameters["@KPIParameter"].SourceColumn = "KPIParameter";
			parameters["@Weightage"].SourceColumn = "Weightage";
			parameters["@RowIndex"].SourceColumn = "RowIndex";
			parameters["@Scale"].SourceColumn = "Scale";
			parameters["@Target"].SourceColumn = "Target";
			if (isUpdate)
			{
				return updateCommand;
			}
			return insertCommand;
		}

		private string GetInsertUpdatePositionDetailsText(bool isUpdate)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddInsertUpdateParameters("Position_Details", new FieldValue("PositionID", "@PositionID"), new FieldValue("Weightage", "@Weightage"), new FieldValue("KPIParameter", "@KPIParameter"), new FieldValue("Scale", "@Scale"), new FieldValue("Target", "@Target"), new FieldValue("RowIndex", "@RowIndex"));
			if (isUpdate)
			{
				return sqlBuilder.GetUpdateExpression();
			}
			return sqlBuilder.GetInsertExpression();
		}

		private SqlCommand GetInsertUpdatePerformanceDetailsCommand(bool isUpdate)
		{
			SqlParameterCollection parameters;
			if (isUpdate)
			{
				if (updateCommand != null)
				{
					updateCommand = null;
				}
				updateCommand = new SqlCommand(GetInsertUpdatePerformanceDetailsText(isUpdate: true), base.DBConfig.Connection);
				updateCommand.CommandType = CommandType.Text;
				parameters = updateCommand.Parameters;
			}
			else
			{
				if (insertCommand != null)
				{
					insertCommand = null;
				}
				insertCommand = new SqlCommand(GetInsertUpdatePerformanceDetailsText(isUpdate: false), base.DBConfig.Connection);
				insertCommand.CommandType = CommandType.Text;
				parameters = insertCommand.Parameters;
			}
			parameters.Add("@PositionID", SqlDbType.NVarChar);
			parameters.Add("@PerformanceParameter", SqlDbType.NText);
			parameters.Add("@Score", SqlDbType.Decimal);
			parameters.Add("@RowIndex", SqlDbType.Int);
			parameters["@PositionID"].SourceColumn = "PositionID";
			parameters["@PerformanceParameter"].SourceColumn = "PerformanceParameter";
			parameters["@RowIndex"].SourceColumn = "RowIndex";
			parameters["@Score"].SourceColumn = "Score";
			if (isUpdate)
			{
				return updateCommand;
			}
			return insertCommand;
		}

		private string GetInsertUpdatePerformanceDetailsText(bool isUpdate)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddInsertUpdateParameters("Performance_Details", new FieldValue("PositionID", "@PositionID"), new FieldValue("PerformanceParameter", "@PerformanceParameter"), new FieldValue("Score", "@Score"), new FieldValue("RowIndex", "@RowIndex"));
			if (isUpdate)
			{
				return sqlBuilder.GetUpdateExpression();
			}
			return sqlBuilder.GetInsertExpression();
		}
	}
}
