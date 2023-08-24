using Micromind.Common;
using Micromind.Common.Data;
using Micromind.Common.Libraries;
using System;
using System.Data;
using System.Data.SqlClient;

namespace Micromind.Data
{
	public sealed class Letters : StoreObject
	{
		private const string LETTERID_PARM = "@LetterID";

		private const string LETTERNAME_PARM = "@LetterName";

		private const string COMPLIMENTARYCLOSING_PARM = "@ComplimentaryClosing";

		private const string BODY_PARM = "@Body";

		public const string SUBJECT_PARM = "@Subject";

		private const string ISRIGHTTOLEFT_PARM = "@IsRightToLeft";

		private const string SALUTATION_PARM = "@Sautation";

		private const string ISINACTIVE_PARM = "@IsInactive";

		private const string DATETIMESTAMP_PARM = "@DateTimeStamp";

		public bool CheckConcurrency = true;

		public Letters(Config config)
			: base(config)
		{
		}

		private string GetInsertText()
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddInsertUpdateParameters("Letters", new FieldValue("LetterName", "@LetterName"), new FieldValue("IsInactive", "@IsInactive"), new FieldValue("IsRightToLeft", "@IsRightToLeft"), new FieldValue("Subject", "@Subject"), new FieldValue("Salutation", "@Sautation"), new FieldValue("ComplimentaryClosing", "@ComplimentaryClosing"), new FieldValue("Body", "@Body"));
			return sqlBuilder.GetInsertExpression();
		}

		private string GetUpdateText()
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddInsertUpdateParameters("Letters", new FieldValue("LetterID", "@LetterID", isUpdateConditionField: true), new FieldValue("LetterName", "@LetterName"), new FieldValue("IsRightToLeft", "@IsRightToLeft"), new FieldValue("Subject", "@Subject"), new FieldValue("Salutation", "@Sautation"), new FieldValue("IsInactive", "@IsInactive"), new FieldValue("ComplimentaryClosing", "@ComplimentaryClosing"), new FieldValue("Body", "@Body"));
			if (CheckConcurrency)
			{
				sqlBuilder.AddInsertUpdateParameters("Letters", new FieldValue("DateTimeStamp", "@DateTimeStamp", isUpdateConditionField: true, checkForNullValue: true));
			}
			return sqlBuilder.GetUpdateExpression();
		}

		private SqlCommand GetInsertCommand()
		{
			if (insertCommand == null)
			{
				insertCommand = new SqlCommand(GetInsertText(), base.DBConfig.Connection);
				insertCommand.CommandType = CommandType.Text;
				SqlParameterCollection parameters = insertCommand.Parameters;
				parameters.Add("@LetterName", SqlDbType.NVarChar);
				parameters.Add("@Sautation", SqlDbType.NVarChar);
				parameters.Add("@ComplimentaryClosing", SqlDbType.NVarChar);
				parameters.Add("@IsRightToLeft", SqlDbType.Bit);
				parameters.Add("@Subject", SqlDbType.NVarChar);
				parameters.Add("@Body", SqlDbType.NText);
				parameters.Add("@IsInactive", SqlDbType.Bit);
				parameters["@LetterName"].SourceColumn = "LetterName";
				parameters["@ComplimentaryClosing"].SourceColumn = "ComplimentaryClosing";
				parameters["@IsRightToLeft"].SourceColumn = "IsRightToLeft";
				parameters["@Sautation"].SourceColumn = "Salutation";
				parameters["@Subject"].SourceColumn = "Subject";
				parameters["@Body"].SourceColumn = "Body";
				parameters["@IsInactive"].SourceColumn = "IsInactive";
			}
			return insertCommand;
		}

		private SqlCommand GetUpdateCommand()
		{
			if (updateCommand == null)
			{
				updateCommand = new SqlCommand(GetUpdateText(), base.DBConfig.Connection);
				updateCommand.CommandType = CommandType.Text;
				SqlParameterCollection parameters = updateCommand.Parameters;
				parameters.Add("@LetterID", SqlDbType.Int);
				parameters.Add("@Sautation", SqlDbType.NVarChar);
				parameters.Add("@LetterName", SqlDbType.NVarChar);
				parameters.Add("@ComplimentaryClosing", SqlDbType.NVarChar);
				parameters.Add("@Subject", SqlDbType.NVarChar);
				parameters.Add("@Body", SqlDbType.NText);
				parameters.Add("@IsRightToLeft", SqlDbType.Bit);
				parameters.Add("@DateTimeStamp", SqlDbType.DateTime);
				parameters.Add("@IsInactive", SqlDbType.Bit);
				parameters["@LetterID"].SourceColumn = "LetterID";
				parameters["@LetterName"].SourceColumn = "LetterName";
				parameters["@Sautation"].SourceColumn = "Salutation";
				parameters["@ComplimentaryClosing"].SourceColumn = "ComplimentaryClosing";
				parameters["@Subject"].SourceColumn = "Subject";
				parameters["@Body"].SourceColumn = "Body";
				parameters["@IsRightToLeft"].SourceColumn = "IsRightToLeft";
				parameters["@DateTimeStamp"].SourceColumn = "DateTimeStamp";
				parameters["@IsInactive"].SourceColumn = "IsInactive";
			}
			return updateCommand;
		}

		public bool InsertLetter(LetterData letterData)
		{
			bool result = true;
			SqlCommand insertCommand = GetInsertCommand();
			SqlTransaction sqlTransaction = null;
			try
			{
				sqlTransaction = (insertCommand.Transaction = base.DBConfig.StartNewTransaction());
				result = Insert(letterData, "Letters", insertCommand);
				object insertedRowIdentity = GetInsertedRowIdentity("Letters", insertCommand);
				letterData.LetterTable.Rows[0]["LetterID"] = insertedRowIdentity;
				UpdateTableRowByID("Letters", "LetterID", "DateTimeStamp", insertedRowIdentity, DateTime.Now, sqlTransaction);
				string entiyID = letterData.LetterTable.Rows[0]["LetterName"].ToString();
				AddActivityLog("Letter", entiyID, ActivityTypes.Add, sqlTransaction);
				UpdateTableRowInsertUpdateInfo("Letters", "LetterID", insertedRowIdentity, sqlTransaction, isInsert: true);
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

		public bool UpdateLetter(LetterData letterData)
		{
			bool flag = true;
			SqlCommand updateCommand = GetUpdateCommand();
			SqlTransaction sqlTransaction = null;
			try
			{
				sqlTransaction = (updateCommand.Transaction = base.DBConfig.StartNewTransaction());
				flag = Update(letterData, "Letters", updateCommand);
				if (!flag)
				{
					return flag;
				}
				object obj = letterData.LetterTable.Rows[0]["LetterID"];
				UpdateTableRowByID("Letters", "LetterID", "DateTimeStamp", obj, DateTime.Now, sqlTransaction);
				string entiyID = letterData.LetterTable.Rows[0]["LetterName"].ToString();
				AddActivityLog("Letter", entiyID, ActivityTypes.Update, sqlTransaction);
				UpdateTableRowInsertUpdateInfo("Letters", "LetterID", obj, sqlTransaction, isInsert: false);
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

		public DataSet GetLettersByFields(params string[] columns)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddTable("Letters");
			foreach (string text in columns)
			{
				ApplicationAssert.CheckCondition(text.IndexOf(".") >= 0, "A table name with the column name must be specified. eg. table.column.", 0);
				string tableName = text.Substring(0, text.IndexOf("."));
				string columnName = text.Substring(text.IndexOf(".") + 1);
				sqlBuilder.AddColumn(tableName, columnName);
			}
			sqlBuilder.IsComparing = false;
			sqlBuilder.UseDistinct = false;
			DataSet dataSet = new DataSet();
			dataSet.EnforceConstraints = false;
			FillDataSet(dataSet, "Letters", sqlBuilder);
			return dataSet;
		}

		public DataSet GetLettersByFields(int[] letterID, params string[] columns)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddTable("Letters");
			foreach (string text in columns)
			{
				ApplicationAssert.CheckCondition(text.IndexOf(".") >= 0, "A table name with the column name must be specified. eg. table.column.", 0);
				string tableName = text.Substring(0, text.IndexOf("."));
				string columnName = text.Substring(text.IndexOf(".") + 1);
				sqlBuilder.AddColumn(tableName, columnName);
			}
			if (letterID != null && letterID.Length != 0)
			{
				CommandHelper commandHelper = new CommandHelper();
				commandHelper.FieldName = "LetterID";
				commandHelper.FieldValue = letterID;
				commandHelper.TableName = "Letters";
				sqlBuilder.AddCommandHelper(commandHelper);
			}
			sqlBuilder.UseDistinct = false;
			DataSet dataSet = new DataSet();
			dataSet.EnforceConstraints = false;
			FillDataSet(dataSet, "Letters", sqlBuilder);
			return dataSet;
		}

		public DataSet GetLettersByFields(int[] letterID, bool isInactive, params string[] columns)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddTable("Letters");
			foreach (string text in columns)
			{
				ApplicationAssert.CheckCondition(text.IndexOf(".") >= 0, "A table name with the column name must be specified. eg. table.column.", 0);
				string tableName = text.Substring(0, text.IndexOf("."));
				string columnName = text.Substring(text.IndexOf(".") + 1);
				sqlBuilder.AddColumn(tableName, columnName);
			}
			if (letterID != null && letterID.Length != 0)
			{
				CommandHelper commandHelper = new CommandHelper();
				commandHelper.FieldName = "LetterID";
				commandHelper.FieldValue = letterID;
				commandHelper.TableName = "Letters";
				sqlBuilder.AddCommandHelper(commandHelper);
			}
			if (!isInactive)
			{
				CommandHelper commandHelper2 = new CommandHelper();
				commandHelper2.FieldName = "IsInactive";
				commandHelper2.FieldValue = 0;
				commandHelper2.TableName = "Letters";
				sqlBuilder.AddCommandHelper(commandHelper2);
			}
			sqlBuilder.UseDistinct = false;
			DataSet dataSet = new DataSet();
			dataSet.EnforceConstraints = false;
			FillDataSet(dataSet, "Letters", sqlBuilder);
			return dataSet;
		}

		public LetterData GetLetters()
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddTable("Letters");
			sqlBuilder.UseDistinct = false;
			LetterData letterData = new LetterData();
			FillDataSet(letterData, "Letters", sqlBuilder);
			return letterData;
		}

		public bool DeleteLetter(int letterID)
		{
			bool flag = true;
			try
			{
				string letterNameByID = GetLetterNameByID(letterID);
				flag = DeleteTableRowByID("Letters", "LetterID", letterID.ToString());
				if (!flag)
				{
					return flag;
				}
				AddActivityLog("Letter", letterNameByID, ActivityTypes.Delete, null);
				return flag;
			}
			catch
			{
				throw;
			}
		}

		public string GetLetterNameByID(object id)
		{
			try
			{
				object obj = ExecuteSelectScalar("Letters", "LetterID", id, "LetterName");
				if (obj != null && obj != DBNull.Value)
				{
					return obj.ToString();
				}
			}
			catch
			{
				throw;
			}
			return "";
		}

		public LetterData GetLetterByID(int letterID)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			CommandHelper commandHelper = null;
			commandHelper = new CommandHelper();
			commandHelper.FieldName = "LetterID";
			commandHelper.SqlFieldType = SqlDbType.Int;
			commandHelper.FieldValue = letterID;
			commandHelper.TableName = "Letters";
			sqlBuilder.AddCommandHelper(commandHelper);
			LetterData letterData = new LetterData();
			sqlBuilder.UseDistinct = false;
			try
			{
				FillDataSet(letterData, "Letters", sqlBuilder);
				return letterData;
			}
			catch
			{
				throw;
			}
		}

		public bool ExistLetter(string shortName)
		{
			try
			{
				return IsTableFieldValueExist("Letters", "LetterName", shortName);
			}
			catch
			{
				throw;
			}
		}
	}
}
