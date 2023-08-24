using Micromind.Common;
using Micromind.Common.Data;
using Micromind.Common.Libraries;
using System;
using System.Data;
using System.Data.SqlClient;

namespace Micromind.Data
{
	public sealed class Surveyor : StoreObject
	{
		private const string SURVEYORID_PARM = "@SurveyorID";

		private const string SURVEYORNAME_PARM = "@SurveyorName";

		private const string TEL_PARM = "@Tel";

		private const string MOBILE_PARM = "@Mobile";

		private const string EMAIL_PARM = "@Email";

		private const string WEBSITE_PARM = "@Website";

		private const string CONTACTNAME_PARM = "@ContactName";

		public const string SURVEYOR_TABLE = "Surveyor";

		private const string CREATEDBY_PARM = "@CreatedBy";

		private const string DATECREATED_PARM = "@DateCreated";

		private const string UPDATEDBY_PARM = "@UpdatedBy";

		private const string DATEUPDATED_PARM = "@DateUpdated";

		public Surveyor(Config config)
			: base(config)
		{
		}

		private string GetInsertUpdateText(bool isUpdate)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddInsertUpdateParameters("Surveyor", new FieldValue("SurveyorID", "@SurveyorID", isUpdateConditionField: true), new FieldValue("SurveyorName", "@SurveyorName"), new FieldValue("Tel", "@Tel"), new FieldValue("Mobile", "@Mobile"), new FieldValue("Email", "@Email"), new FieldValue("Website", "@Website"), new FieldValue("ContactName", "@ContactName"));
			if (isUpdate)
			{
				sqlBuilder.AddInsertUpdateParameters("Surveyor", new FieldValue("DateUpdated", "@DateUpdated", isUpdateConditionField: true, checkForNullValue: true));
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
			parameters.Add("@SurveyorID", SqlDbType.NVarChar);
			parameters.Add("@SurveyorName", SqlDbType.NVarChar);
			parameters.Add("@Tel", SqlDbType.NVarChar);
			parameters.Add("@Mobile", SqlDbType.NVarChar);
			parameters.Add("@Email", SqlDbType.NVarChar);
			parameters.Add("@Website", SqlDbType.NVarChar);
			parameters.Add("@ContactName", SqlDbType.NVarChar);
			parameters["@SurveyorID"].SourceColumn = "SurveyorID";
			parameters["@SurveyorName"].SourceColumn = "SurveyorName";
			parameters["@Tel"].SourceColumn = "Tel";
			parameters["@Mobile"].SourceColumn = "Mobile";
			parameters["@Email"].SourceColumn = "Email";
			parameters["@Website"].SourceColumn = "Website";
			parameters["@ContactName"].SourceColumn = "ContactName";
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

		public bool InsertSurveyor(SurveyorData accountSurveyorData)
		{
			bool result = true;
			SqlCommand insertUpdateCommand = GetInsertUpdateCommand(isUpdate: false);
			SqlTransaction sqlTransaction = null;
			try
			{
				sqlTransaction = (insertUpdateCommand.Transaction = base.DBConfig.StartNewTransaction());
				result = Insert(accountSurveyorData, "Surveyor", insertUpdateCommand);
				string text = accountSurveyorData.SurveyorTable.Rows[0]["SurveyorID"].ToString();
				AddActivityLog("Product Category", text, ActivityTypes.Add, sqlTransaction);
				UpdateTableRowInsertUpdateInfo("Surveyor", "SurveyorID", text, sqlTransaction, isInsert: true);
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

		public bool UpdateSurveyor(SurveyorData accountSurveyorData)
		{
			bool flag = true;
			SqlCommand insertUpdateCommand = GetInsertUpdateCommand(isUpdate: true);
			SqlTransaction sqlTransaction = null;
			try
			{
				sqlTransaction = (insertUpdateCommand.Transaction = base.DBConfig.StartNewTransaction());
				flag = Update(accountSurveyorData, "Surveyor", insertUpdateCommand);
				if (!flag)
				{
					return flag;
				}
				object obj = accountSurveyorData.SurveyorTable.Rows[0]["SurveyorID"];
				UpdateTableRowByID("Surveyor", "SurveyorID", "DateUpdated", obj, DateTime.Now, sqlTransaction);
				string entiyID = accountSurveyorData.SurveyorTable.Rows[0]["SurveyorName"].ToString();
				AddActivityLog("Surveyor", entiyID, ActivityTypes.Update, sqlTransaction);
				UpdateTableRowInsertUpdateInfo("Surveyor", "SurveyorID", obj, sqlTransaction, isInsert: false);
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

		public SurveyorData GetSurveyor()
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddTable("Surveyor");
			SurveyorData surveyorData = new SurveyorData();
			sqlBuilder.UseDistinct = false;
			FillDataSet(surveyorData, "Surveyor", sqlBuilder);
			return surveyorData;
		}

		public bool DeleteSurveyor(string TaskID)
		{
			bool flag = true;
			try
			{
				string commandText = "DELETE FROM Surveyor WHERE SurveyorID = '" + TaskID + "'";
				flag = Delete(commandText, null);
				if (!flag)
				{
					return flag;
				}
				AddActivityLog("Quality Task", TaskID, ActivityTypes.Delete, null);
				return flag;
			}
			catch
			{
				throw;
			}
		}

		public SurveyorData GetSurveyorByID(string id)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			CommandHelper commandHelper = null;
			commandHelper = new CommandHelper();
			commandHelper.FieldName = "SurveyorID";
			commandHelper.SqlFieldType = SqlDbType.NVarChar;
			commandHelper.FieldValue = id;
			commandHelper.TableName = "Surveyor";
			sqlBuilder.AddCommandHelper(commandHelper);
			sqlBuilder.IsComparing = true;
			SurveyorData surveyorData = new SurveyorData();
			sqlBuilder.UseDistinct = false;
			FillDataSet(surveyorData, "Surveyor", sqlBuilder);
			return surveyorData;
		}

		public DataSet GetSurveyorByFields(params string[] columns)
		{
			return GetSurveyorByFields(null, isInactive: true, columns);
		}

		public DataSet GetSurveyorByFields(string[] taskID, params string[] columns)
		{
			return GetSurveyorByFields(taskID, isInactive: true, columns);
		}

		public DataSet GetSurveyorByFields(string[] ids, bool isInactive, params string[] columns)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddTable("Surveyor");
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
				commandHelper.FieldName = "SurveyorID";
				commandHelper.FieldValue = ids;
				commandHelper.SqlFieldType = SqlDbType.NVarChar;
				commandHelper.TableName = "Surveyor";
				sqlBuilder.AddCommandHelper(commandHelper);
			}
			DataSet dataSet = new DataSet();
			dataSet.EnforceConstraints = false;
			sqlBuilder.UseDistinct = false;
			FillDataSet(dataSet, "Surveyor", sqlBuilder);
			return dataSet;
		}

		public DataSet GetSurveyorList()
		{
			DataSet dataSet = new DataSet();
			string textCommand = "SELECT SurveyorID [Surveyor Code],SurveyorName [Surveyor Name],ContactName\r\n                           FROM Surveyor";
			FillDataSet(dataSet, "Surveyor", textCommand);
			return dataSet;
		}

		public DataSet GetSurveyorComboList()
		{
			DataSet dataSet = new DataSet();
			string textCommand = "SELECT SurveyorID [Code],SurveyorName [Name]\r\n                           FROM Surveyor ORDER BY SurveyorID,SurveyorName";
			FillDataSet(dataSet, "Surveyor", textCommand);
			return dataSet;
		}
	}
}
